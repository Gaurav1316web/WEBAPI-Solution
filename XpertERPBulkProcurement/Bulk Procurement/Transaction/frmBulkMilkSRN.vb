Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmBulkMilkSRN
    Inherits FrmMainTranScreen
#Region "Varibales"
    Dim settIncludeInceAndDedInFATSNFRate As Boolean = False
    Dim SettDeductionOnStandardQty As Boolean = True
    Dim CreateBulkMilkSRNItemwise As Integer = 0
    Dim AllowBulkProcTransDateSameasGateEntryDate As Integer = 0
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim AllowManualRateonPO As Integer = 0
    Dim BulkProcPriceChartStandardRateWithZero As Integer = 0
    Dim dtpTareWeight As DateTime?
    Public AllowModifcationByApprovalUser As Boolean = False
    Dim AllowRandomOnlyOneSecondaryQC As Integer = 0
    Dim AllowNLevel As Boolean = False
    Public isInsideLoadData As Boolean = False
    Dim IsItemMilkType As Integer = 0
    Dim IsPriceChartGradeWise As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Dim AllowTruncateAmount As Boolean = False
    Dim isDocPosted As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Public fatColName As String = String.Empty
    Public snfColName As String = String.Empty
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsBulkMilkSRN = Nothing
    Public DocumentNo As String = ""
    Public IsAutoMilkRGP As Boolean = False
    Dim gv_qc As New RadGridView
    Dim BulkProcPricePostedData As Boolean
    '=============================================
    Dim RunBulkProcOnAdjustFATCLR As Integer = 0
    Dim SettBulkProcurementApplyTotalSoidRate As Boolean = False ''ERO/20/12/18-000446 by balwinder on 27/12/2018
    Dim SettCalculateLtrQtyFromKGQtyByCLR As Boolean = False
    Dim SettApplyCalculateWeightInLtr As Boolean = False
    Public Const colFatQTy As String = "colFatQTy"
    Public Const colSNFQty As String = "colSNFQty"
    Public Const colTotalStdQTy As String = "colTotalStdQTy"
    Public Const colIncentiveAmt As String = "colIncentiveAmt"
    Public Const colDeductionAmt As String = "colDeductionAmt"
    Public Const colSlNo As String = "SLNO"
    Public Const colpriceCode As String = "colpriceCode"
    Public Const colMilkGradeCode As String = "colMilkGradeCode"
    Public Const colMilkTtypeCode As String = "colMilkTtypeCode"
    Public Const colChamberDesc As String = "colChamberDesc"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colItemHSNCode As String = "ItemHSNcode"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colCLR As String = "colCLR"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colNetWeightCalculated As String = "colNetWeightCalculated"
    Public Const colNetWeightLTR As String = "colNetWeightLTR"

    Public Const colUOMCalculated As String = "colUOMCalculated"
    Public Const colFatRate As String = "colFATRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colFatAmt As String = "colFatAmt"
    Public Const colSNFAmt As String = "colSNFAmt"
    Public Const colAmt As String = "colAmt"
    Public Const colMilkRate As String = "colMilkRate"
    Public Const colStandardRate As String = "colStandardRate"
    Public Const colNetRate As String = "colNetRate"
    Public Const colDeducRate As String = "colDeducRate"
    Public Const colIncenRate As String = "colIncenRate"
    Public Const colFinalMilkRate As String = "colFinalMilkRate"
    Public Const colDeduc As String = "colDeduc"
    Public Const colIncentive As String = "colIncentive"
    Public Const colSpecialDeduction As String = "colSpecialDeduction"
    Public Const colTranspoterCharge As String = "colTranspoterCharge"
    Public Const colMilkAmount As String = "colMilkAmount"
    Public Const colActAmt As String = "colActAmt"

    '=============QC===================
    Const colQCsno As String = "SNO"
    Const colQCloc_code As String = "QCLoc_Code"
    Const colQCloc_name As String = "QCLoc_Name"
    Const colQCToloc_code As String = "QCTOLoc_Code"
    Const colQCToloc_name As String = "QCSTOLoc_Name"
    Const colQCitemcode As String = "qcitemcode"
    Const colQCiname As String = "iname"
    Const colQCparamcode As String = "paramcode"
    Const colQCparam_desc As String = "param_desc"
    Const colQCparam_type As String = "paramtype"
    Const colQCparam_nature As String = "paramnature"
    Const colQCrange1 As String = "range1"
    Const colQCrange2 As String = "range2"
    Const colQCstatus As String = "status"
    Const colQCvalue1 As String = "value1"
    Const colQCvalue2 As String = "value2"
    Const colQCRange As String = "Range"
    Const colQCValue As String = "OUTPUTVALUE"
    Const colQCOutStatus As String = "OutStatus"
    Const colQCremarks As String = "remarkS"
    Const colQCHistort As String = "History"
    Dim ApplyBothtsrateAndFatRateinBulkProcurement As Boolean = False
    Dim ApplyTransportChargeAddInActualAmount As Boolean = False
    Dim SettBulkMilkFATSNFKGDecimalPlaces As Integer = 0
    Dim SettBulkMilkFATSNFAmtDecimalPlaces As Integer = 0
    Dim settConsiderAllParametersForIncetive As Boolean = False
#End Region

    Private Sub FrmBulkMilkSRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        settIncludeInceAndDedInFATSNFRate = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IncludeInceAndDedInFATSNFRate, clsFixedParameterCode.IncludeInceAndDedInFATSNFRate, Nothing)) = 1)
        SettDeductionOnStandardQty = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateDeductionByStdQtyinBulkMilkSRN, clsFixedParameterCode.CalculateDeductionByStdQtyinBulkMilkSRN, Nothing)) = 1)
        CreateBulkMilkSRNItemwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, Nothing))
        AllowBulkProcTransDateSameasGateEntryDate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcTransDateSameasGateEntryDate, clsFixedParameterCode.AllowBulkProcTransDateSameasGateEntryDate, Nothing))
        Panel3.Enabled = False
        BulkProcPriceChartStandardRateWithZero = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, Nothing))
        AllowManualRateonPO = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualPriceONBulkPO, clsFixedParameterCode.AllowManualPriceONBulkPO, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        AllowRandomOnlyOneSecondaryQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowRandomOnlyOneSecondaryQC, clsFixedParameterCode.AllowRandomOnlyOneSecondaryQC, Nothing))
        AllowNLevel = clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmBulkMilkSRN)
        SetUserMgmtNew()
        IsItemMilkType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
        IsPriceChartGradeWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        AllowTruncateAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_AmountTruncate_BulkMilkSRN, clsFixedParameterCode.Allow_AmountTruncate_BulkMilkSRN, Nothing)) = "1", True, False)
        RunBulkProcOnAdjustFATCLR = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBulkProcOnAdjustedFATCLR, clsFixedParameterCode.RunBulkProcOnAdjustedFATCLR, Nothing))
        SettBulkProcurementApplyTotalSoidRate = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementApplyTotalSoidRate, clsFixedParameterCode.BulkProcurementApplyTotalSoidRate, Nothing)) > 0)
        ApplyBothtsrateAndFatRateinBulkProcurement = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBothtsrateAndFatRateinBulkProcurement, clsFixedParameterCode.ApplyBothtsrateAndFatRateinBulkProcurement, Nothing)) = 1, True, False)
        SplitContainer6.Panel1Collapsed = SettBulkProcurementApplyTotalSoidRate
        SplitContainer6.Panel2Collapsed = Not SettBulkProcurementApplyTotalSoidRate
        SettCalculateLtrQtyFromKGQtyByCLR = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateLtrQtyFromKGQtyByCLR, clsFixedParameterCode.CalculateLtrQtyFromKGQtyByCLR, Nothing)) > 0)
        SettApplyCalculateWeightInLtr = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCalculateWeightInLtr, clsFixedParameterCode.ApplyCalculateWeightInLtr, Nothing)) > 0)
        ApplyTransportChargeAddInActualAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTransportChargeAddInActualAmount, clsFixedParameterCode.ApplyTransportChargeAddInActualAmount, Nothing)) = "1", True, False)
        SettBulkMilkFATSNFKGDecimalPlaces = clsFixedParameter.GetData(clsFixedParameterType.BulkMilkFATSNFKGDecimalPlaces, clsFixedParameterCode.BulkMilkFATSNFKGDecimalPlaces, Nothing)
        SettBulkMilkFATSNFAmtDecimalPlaces = clsFixedParameter.GetData(clsFixedParameterType.BulkMilkFATSNFAmtDecimalPlaces, clsFixedParameterCode.BulkMilkFATSNFAmtDecimalPlaces, Nothing)
        settConsiderAllParametersForIncetive = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkMilkConsiderAllParametersForIncetive, clsFixedParameterCode.BulkMilkConsiderAllParametersForIncetive, Nothing)) > 0)
        txtUOM.Visible = False
        MyLabel13.Visible = False
        If SettBulkProcurementApplyTotalSoidRate OrElse SettApplyCalculateWeightInLtr Then
            txtUOM.Visible = True
            MyLabel13.Visible = True
        End If

        Reset()
        MyBase.ReStoreGridLayoutMain(Me)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        IsAutoMilkRGP = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateAutoMilkRGPinBulkSRN, clsFixedParameterCode.CreateAutoMilkRGPinBulkSRN, Nothing)) = 1, True, False)
        If TankerFromMaster = 1 And SettBulkProcurementApplyTotalSoidRate = False Then
            GroupBox1.Visible = False
        End If
        
        BulkProcPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcItemPostedData, clsFixedParameterCode.AllowBulkProcItemPostedData, Nothing)) = 1, True, False)
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub

    Sub Reset()
        If AllowNLevel Then
            btnPost.Visible = MyBase.isPostFlag
        End If
        fndReferenceNo.Value = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        txtLocation.Enabled = True
        fndTankerNo.Enabled = True
        fndSRNNo.Value = ""
        'fndWeighmentNo.Enabled = True
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
        txtGateEntryNo.Text = ""
        dtpSRNDATE.Value = dt
        fndWeighmentNo.Value = ""
        dtpWeighmentDate.Value = dt
        txtVendor.Text = ""
        lblVendorName.Text = ""
        txtLocation.Text = clsGateEntry.getUsersDefaultLocation()
        lblLocationDesc.Text = clsLocation.GetName(txtLocation.Text, Nothing)
        txtChallanNo.Text = ""
        dtpChallanDate.Value = dt
        fndTankerNo.Value = ""
        lblTankerTransporterName.Text = ""
        fndPriceChart.Value = ""
        TxtFatWeightage.Text = "0"
        TxtSNFWeightage.Text = "0"
        txtfatPercentage.Text = "0"
        txtSNFPercentage.Text = "0"
        txtStanadardrate.Text = "0"
        txtTolerance.Text = "0"
        loadBlankItemGrid()
        txtQCNo.Text = ""
        dtpQCInTime.Value = dt
        dtpQCOutTime.Text = dt
        txtDipValue.Text = ""
        txtRemarks.Text = ""
        loadBlankParameterGrid()
        ''richa Against Ticket no .BM00000003725 on 05/08/2014
        loadBlankParameterGridwithRange()
        ''=============================================
        lblFATKg.Text = ""
        lblSNFKG.Text = ""
        lblFATRate.Text = ""
        lblSNFRate.Text = ""
        lblFatAmount.Text = ""
        lblSnfAmount.Text = ""
        lblTotalAmount.Text = ""
        lblTotalQtyValue.Text = ""
        lblStandardRate.Text = ""
        lblIncentive.Text = ""
        lblSpecialDeduction.Text = ""
        lblDeduction.Text = ""
        lblActualAmount.Text = ""
        btnSave.Enabled = True
        btnPrint.Enabled = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSave.Text = "Save"
        fndSRNNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = RadPageViewPage1
        fatColName = String.Empty
        snfColName = String.Empty
        FindAndRestoreGridLayout(Me)
        FindAndSetTabStopFalse(Me)
        isDocPosted = False
        lblReverse.Visible = False
        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpSRNDATE.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpWeighmentDate.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpSRNDATE.CustomFormat = "dd/MM/yyyy"
            dtpWeighmentDate.CustomFormat = "dd/MM/yyyy"

        End If
        '==========================================================
        txtTotalSolidRate.Value = 0
        txtTransportCharges.Value = 0
        txtUOM.Value = ""
        ApplyUOM()
    End Sub

    Sub ApplyUOM()
        If SettApplyCalculateWeightInLtr Then
            'txtUOM.Value = "LTR" ''MIL/02/04/19-000059 by balwinder on 05/04/2019
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

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
                    ''richa Against Ticket no .BM00000003725 on 05/08/2014
                    Dim obj2 As New clsGridLayout()
                    obj2.ReportID = MyBase.Form_ID & "gvRange"
                    obj2.UserID = objCommonVar.CurrentUserCode
                    obj2.GridLayout = New MemoryStream()
                    gvRange.SaveLayout(obj2.GridLayout)
                    obj2.GridColumns = gvRange.ColumnCount
                    obj2.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                    If obj2.SaveData() Then
                        common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
                    End If
                    ''stuti regarding memory leakage
                    obj2.GridLayout.Close()
                    obj2.GridLayout.Dispose()
                    ''==================================================
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
        ''richa Against Ticket no .BM00000003725 on 05/08/2014
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvRange", objCommonVar.CurrentUserCode)
        ''=====================================================
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
                    For ii = 0 To gvParam.Columns.Count - 1 Step ii + 1
                        gvParam.Columns(ii).IsVisible = False
                        gvParam.Columns(ii).VisibleInColumnChooser = True
                    Next
                    If obj.GridLayout IsNot Nothing Then
                        gvParam.LoadLayout(obj.GridLayout)
                    End If
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            ''richa Against Ticket no .BM00000003725 on 05/08/2014
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvRange", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvRange.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvRange.Columns.Count - 1 Step ii + 1
                        gvRange.Columns(ii).IsVisible = False
                        gvRange.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvRange.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            ''======================================================

        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub loadBlankItemGrid()

        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing

        gvItem.Columns.Add(colSlNo, "SL. NO.")
        gvItem.Columns(colSlNo).Width = 60
        gvItem.Columns(colSlNo).ReadOnly = True

        gvItem.Columns.Add(colItemCode, "Item Code")
        gvItem.Columns(colItemCode).Width = 100
        gvItem.Columns(colItemCode).ReadOnly = True

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colItemHSNCode, "HSN Code")
        gvItem.Columns(colItemHSNCode).Width = 100
        gvItem.Columns(colItemHSNCode).ReadOnly = True

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = True

        gvItem.Columns.Add(colpriceCode, "Price Code")
        gvItem.Columns(colpriceCode).Width = 150
        gvItem.Columns(colpriceCode).HeaderImage = My.Resources.search4
        gvItem.Columns(colpriceCode).TextImageRelation = TextImageRelation.TextBeforeImage
        gvItem.Columns(colpriceCode).ReadOnly = False
        gvItem.Columns(colpriceCode).IsVisible = False

        gvItem.Columns.Add(colChamberDesc, "Chamber Desc")
        gvItem.Columns(colChamberDesc).Width = 150
        gvItem.Columns(colChamberDesc).ReadOnly = True
        gvItem.Columns(colChamberDesc).IsVisible = False

        gvItem.Columns.Add(colMilkTtypeCode, "Milk Type")
        gvItem.Columns(colMilkTtypeCode).Width = 150
        gvItem.Columns(colMilkTtypeCode).ReadOnly = True
        gvItem.Columns(colMilkTtypeCode).IsVisible = False

        gvItem.Columns.Add(colMilkGradeCode, "Milk grade")
        gvItem.Columns(colMilkGradeCode).Width = 150
        gvItem.Columns(colMilkGradeCode).ReadOnly = True
        gvItem.Columns(colMilkGradeCode).IsVisible = False

        gvItem.Columns.Add(colGrossWeight, "Gross Weight")
        gvItem.Columns(colGrossWeight).Width = 120
        gvItem.Columns(colGrossWeight).ReadOnly = True
        gvItem.Columns(colGrossWeight).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colTareWeight, "Tare Weight")
        gvItem.Columns(colTareWeight).Width = 120
        gvItem.Columns(colTareWeight).ReadOnly = True
        gvItem.Columns(colTareWeight).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colNetWeight, "Net Weight")
        gvItem.Columns(colNetWeight).Width = 120
        gvItem.Columns(colNetWeight).ReadOnly = True
        gvItem.Columns(colNetWeight).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colNetWeightCalculated, "Net Weight Calculate")
        gvItem.Columns(colNetWeightCalculated).Width = 120
        gvItem.Columns(colNetWeightCalculated).ReadOnly = True
        gvItem.Columns(colNetWeightCalculated).TextAlignment = ContentAlignment.MiddleRight
        gvItem.Columns(colNetWeightCalculated).IsVisible = (SettBulkProcurementApplyTotalSoidRate OrElse SettApplyCalculateWeightInLtr)
        'ApplyTransportChargeAddInActualAmount
        gvItem.Columns.Add(colNetWeightLTR, "Net Weight Ltr")
        gvItem.Columns(colNetWeightLTR).Width = 120
        gvItem.Columns(colNetWeightLTR).ReadOnly = True
        gvItem.Columns(colNetWeightLTR).TextAlignment = ContentAlignment.MiddleRight
        gvItem.Columns(colNetWeightLTR).IsVisible = (SettBulkProcurementApplyTotalSoidRate OrElse SettApplyCalculateWeightInLtr)

        gvItem.Columns.Add(colUOMCalculated, "UOM Calculated")
        gvItem.Columns(colUOMCalculated).IsVisible = False
        gvItem.Columns(colUOMCalculated).ReadOnly = True

        gvItem.Columns.Add(colFat, "FAT (%)")
        gvItem.Columns(colFat).Width = 75
        gvItem.Columns(colFat).ReadOnly = True
        gvItem.Columns(colFat).IsVisible = True
        gvItem.Columns(colFat).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNF, "SNF (%)")
        gvItem.Columns(colSNF).Width = 75
        gvItem.Columns(colSNF).ReadOnly = True
        gvItem.Columns(colSNF).IsVisible = True
        gvItem.Columns(colSNF).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colCLR, "CLR")
        gvItem.Columns(colCLR).Width = 75
        gvItem.Columns(colCLR).ReadOnly = True
        gvItem.Columns(colCLR).IsVisible = SettCalculateLtrQtyFromKGQtyByCLR
        gvItem.Columns(colCLR).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFatKG, "FAT (KG)")
        gvItem.Columns(colFatKG).Width = 75
        gvItem.Columns(colFatKG).ReadOnly = True
        'gvItem.Columns(colFatKG).IsVisible = False
        gvItem.Columns(colFatKG).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNFKG, "SNF (KG)")
        gvItem.Columns(colSNFKG).Width = 75
        gvItem.Columns(colSNFKG).ReadOnly = True
        'gvItem.Columns(colSNFKG).IsVisible = False
        gvItem.Columns(colSNFKG).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFatRate, "FAT Rate")
        gvItem.Columns(colFatRate).Width = 100
        gvItem.Columns(colFatRate).ReadOnly = True
        gvItem.Columns(colFatRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNFRate, "SNF Rate")
        gvItem.Columns(colSNFRate).Width = 100
        gvItem.Columns(colSNFRate).ReadOnly = True
        gvItem.Columns(colSNFRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFatQTy, "FAT Qty")
        gvItem.Columns(colFatQTy).Width = 100
        gvItem.Columns(colFatQTy).ReadOnly = True
        gvItem.Columns(colFatQTy).IsVisible = False
        gvItem.Columns(colFatQTy).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNFQty, "SNF Qty")
        gvItem.Columns(colSNFQty).Width = 100
        gvItem.Columns(colSNFQty).ReadOnly = True
        gvItem.Columns(colSNFQty).IsVisible = False
        gvItem.Columns(colSNFQty).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colTotalStdQTy, "Total Std Qty")
        gvItem.Columns(colTotalStdQTy).Width = 100
        gvItem.Columns(colTotalStdQTy).ReadOnly = True
        gvItem.Columns(colTotalStdQTy).IsVisible = False
        gvItem.Columns(colTotalStdQTy).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFatAmt, "FAT Amt")
        gvItem.Columns(colFatAmt).Width = 100
        gvItem.Columns(colFatAmt).ReadOnly = True
        gvItem.Columns(colFatAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNFAmt, "SNF Amt")
        gvItem.Columns(colSNFAmt).Width = 100
        gvItem.Columns(colSNFAmt).ReadOnly = True
        gvItem.Columns(colSNFAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colMilkRate, "Basic Rate")
        gvItem.Columns(colMilkRate).Width = 150
        gvItem.Columns(colMilkRate).ReadOnly = False
        gvItem.Columns(colMilkRate).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colStandardRate, "Standard Rate")
        gvItem.Columns(colStandardRate).Width = 150
        gvItem.Columns(colStandardRate).ReadOnly = True
        gvItem.Columns(colStandardRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colDeducRate, "Deduction Per Unit")
        gvItem.Columns(colDeducRate).Width = 150
        gvItem.Columns(colDeducRate).ReadOnly = True
        gvItem.Columns(colDeducRate).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colIncenRate, "Incentive Per Unit")
        gvItem.Columns(colIncenRate).Width = 150
        gvItem.Columns(colIncenRate).ReadOnly = True
        gvItem.Columns(colIncenRate).TextAlignment = ContentAlignment.MiddleRight



        gvItem.Columns.Add(colAmt, "Amount")
        gvItem.Columns(colAmt).Width = 150
        gvItem.Columns(colAmt).ReadOnly = True
        gvItem.Columns(colAmt).IsVisible = False
        gvItem.Columns(colAmt).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colDeduc, "Deduction")
        gvItem.Columns(colDeduc).Width = 100
        gvItem.Columns(colDeduc).ReadOnly = True
        gvItem.Columns(colDeduc).IsVisible = False
        gvItem.Columns(colDeduc).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colDeductionAmt, "Deduction Amt")
        gvItem.Columns(colDeductionAmt).Width = 100
        gvItem.Columns(colDeductionAmt).ReadOnly = True
        gvItem.Columns(colDeductionAmt).IsVisible = False
        gvItem.Columns(colDeductionAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colIncentive, "Incentive")
        gvItem.Columns(colIncentive).Width = 100
        gvItem.Columns(colIncentive).ReadOnly = True
        gvItem.Columns(colIncentive).IsVisible = False
        gvItem.Columns(colIncentive).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colIncentiveAmt, "Incentive Amt")
        gvItem.Columns(colIncentiveAmt).Width = 100
        gvItem.Columns(colIncentiveAmt).ReadOnly = True
        gvItem.Columns(colIncentiveAmt).IsVisible = False
        gvItem.Columns(colIncentiveAmt).TextAlignment = ContentAlignment.MiddleRight

        If TankerFromMaster = 1 Then
            gvItem.Columns(colpriceCode).IsVisible = True
            gvItem.Columns(colChamberDesc).IsVisible = True
            gvItem.Columns(colMilkTtypeCode).IsVisible = True
            gvItem.Columns(colMilkGradeCode).IsVisible = True
            gvItem.Columns(colFatQTy).IsVisible = True
            gvItem.Columns(colSNFQty).IsVisible = True
            gvItem.Columns(colTotalStdQTy).IsVisible = True
            gvItem.Columns(colIncentiveAmt).IsVisible = True
            gvItem.Columns(colDeductionAmt).IsVisible = True
        End If

        ''richa Against Ticket No.BM00000003719 on 04/09/2014
        gvItem.Columns.Add(colSpecialDeduction, "Special Deduction")
        gvItem.Columns(colSpecialDeduction).Width = 100
        gvItem.Columns(colSpecialDeduction).ReadOnly = True
        gvItem.Columns(colSpecialDeduction).TextAlignment = ContentAlignment.MiddleRight
        '-------------------------------------------------

        gvItem.Columns.Add(colNetRate, "Net Rate")
        gvItem.Columns(colNetRate).Width = 150
        gvItem.Columns(colNetRate).ReadOnly = True
        gvItem.Columns(colNetRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFinalMilkRate, "Final Milk Rate")
        gvItem.Columns(colFinalMilkRate).Width = 150
        gvItem.Columns(colFinalMilkRate).ReadOnly = True
        gvItem.Columns(colFinalMilkRate).TextAlignment = ContentAlignment.MiddleRight

        'colMilkAmount
        gvItem.Columns.Add(colMilkAmount, "Milk Amount")
        gvItem.Columns(colMilkAmount).Width = 75
        gvItem.Columns(colMilkAmount).IsVisible = ApplyTransportChargeAddInActualAmount
        gvItem.Columns(colMilkAmount).ReadOnly = True
        gvItem.Columns(colMilkAmount).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colTranspoterCharge, "Transpoter Charge")
        gvItem.Columns(colTranspoterCharge).Width = 75
        gvItem.Columns(colTranspoterCharge).IsVisible = ApplyTransportChargeAddInActualAmount
        gvItem.Columns(colTranspoterCharge).ReadOnly = True
        gvItem.Columns(colTranspoterCharge).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colActAmt, "Amount")
        gvItem.Columns(colActAmt).Width = 75
        gvItem.Columns(colActAmt).ReadOnly = True
        gvItem.Columns(colActAmt).TextAlignment = ContentAlignment.MiddleRight

        If TankerFromMaster = 0 Then
            gvItem.Rows.AddNew()
            gvItem.Rows(0).Cells(colSlNo).Value = "1"
        End If

        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = True
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AllowColumnChooser = True
        gvItem.AllowColumnReorder = True
        'gvItem.AllowSearchRow = True
        'gvItem.MasterTemplate.EnableCustomFiltering = True
        ReStoreGridLayout()

    End Sub

    Sub loadBlankParameterGrid()
        If clsCommon.myLen(txtLocation.Text) <= 0 Then
            gvParam.Rows.Clear()
            gvParam.Columns.Clear()
            gvParam.DataSource = Nothing
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
            whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        Else
            whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        End If
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master " & whrCls & "  order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        gvParam.Columns.Add("colSLNO", "SL. No.")
        gvParam.Columns("colSLNO").Width = 60
        gvParam.Columns("colSLNO").ReadOnly = True
        gvParam.Columns("colSLNO").Tag = "SLNO"
        If pFields Then

            For i As Integer = 0 To dt.Rows.Count() - 1
                gvParam.Columns.Add(dt.Rows(i)("Code"), dt.Rows(i)("Description"))
                gvParam.Columns(dt.Rows(i)("Code")).Width = 120
                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = True
                gvParam.Columns(dt.Rows(i)("Code")).Tag = dt.Rows(i)("Type")
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    fatColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    snfColName = dt.Rows(i)("Code")
                End If
            Next
        Else
            Throw New Exception("There is No parameter defined in Parameter Master. Please Define atleast FAT and SNF")
        End If

        If TankerFromMaster = 0 Then
            gvParam.Rows.AddNew()
            gvParam.Rows(0).Cells("colSLNO").Value = "1"
        ElseIf TankerFromMaster = 1 Then
            Dim ii As Integer = gvItem.Rows.Count
            Dim intCount As Integer = 0
            For intCount = 0 To ii - 1
                gvParam.Rows.AddNew()
                gvParam.Rows(intCount).Cells("colSLNO").Value = intCount + 1
            Next
        End If


        gvParam.AllowAddNewRow = False
        gvParam.AllowDeleteRow = False
        gvParam.AllowRowReorder = False
        gvParam.ShowGroupPanel = False
        gvParam.EnableFiltering = False
        gvParam.EnableSorting = False
        gvParam.EnableGrouping = False
        gvParam.AllowColumnChooser = True
        ReStoreGridLayout()
    End Sub
    ''richa Against Ticket no .BM00000003725 on 05/08/2014
    Sub loadBlankParameterGridwithRange()
        gvRange.Rows.Clear()
        gvRange.Columns.Clear()
        gvRange.DataSource = Nothing

        gvRange.Columns.Add("Code", "Parameter")
        gvRange.Columns("Code").Width = 250
        gvRange.Columns("Code").ReadOnly = True

        gvRange.Columns.Add("LowerRange", "Lower Range")
        gvRange.Columns("LowerRange").Width = 90
        gvRange.Columns("LowerRange").ReadOnly = True

        gvRange.Columns.Add("UpperRange", "Upper Range")
        gvRange.Columns("UpperRange").Width = 90
        gvRange.Columns("UpperRange").ReadOnly = True

        gvRange.Columns.Add("Value", "Value")
        gvRange.Columns("Value").Width = 90
        gvRange.Columns("Value").ReadOnly = True

        gvRange.Columns.Add("QcValue", "QC Value")
        gvRange.Columns("QcValue").Width = 90
        gvRange.Columns("QcValue").ReadOnly = True


        gvRange.Columns.Add("IncentiveDeduction", "Incentive/Deduction")
        gvRange.Columns("IncentiveDeduction").Width = 200
        gvRange.Columns("IncentiveDeduction").ReadOnly = True


        'gvRange.Rows.AddNew()
        'gvRange.Rows(0).Cells("colSLNO").Value = "1"
        gvRange.AllowAddNewRow = False
        gvRange.AllowDeleteRow = False
        gvRange.AllowRowReorder = False
        gvRange.ShowGroupPanel = False
        gvRange.EnableFiltering = False
        gvRange.EnableSorting = False
        gvRange.EnableGrouping = False
        ReStoreGridLayout()
    End Sub
    ''=====================================================================
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBulkMilkSRN)
        'If Not (MyBase.isReadFlag) Then
        '    If MDI.blnShowAllMenu = False Then
        '        Throw New Exception("Permission Denied")
        '    Else
        '        Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

        '    End If
        'End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnPrintPO.Visible = MyBase.isPrintFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub

    Private Sub FrmBulkMilkSRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                             "TSPL_Bulk_MILK_SRN " + Environment.NewLine + _
                                             "TSPL_Bulk_MILK_SRN_Chember_Details (  Only in case of chamber wise setting ON) " + Environment.NewLine + _
                                             "tspl_bulk_milk_srn_History ( For History) " + Environment.NewLine + _
                                             "TSPL_SRN_Parameter_Range_Detail ( For SRN Parameter.) ")
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                If TankerFromMaster = 1 Then
                    btnReverse.Visible = True
                Else
                    btnReverseRec.Visible = True
                End If
            End If
        End If
    End Sub

    Sub SaveData(ByVal isPost As Boolean, ByVal isAutoSRN As Boolean)
        Dim trans As SqlTransaction = Nothing
        Try
            Dim totalqty As Decimal = 0

            trans = clsDBFuncationality.GetTransactin()
            Dim objApproval As New clsApply_Approval()
            If AllowNLevel Then
                If Not AllowModifcationByApprovalUser Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(fndSRNNo.Value), trans)
                End If
            End If
            trans.Commit()
            trans = Nothing

            obj = New clsBulkMilkSRN()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                If TankerFromMaster = 1 Then
                    If Not IsDBNull(dtpTareWeight) = True Then
                        dtpSRNDATE.Value = dtpTareWeight
                    Else
                        dtpSRNDATE.Value = dtpWeighmentDate.Value
                    End If
                End If
                If AllowBulkProcTransDateSameasGateEntryDate = 1 Then
                    dtpSRNDATE.Value = clsDBFuncationality.getSingleValue("Select Date_And_Time from Tspl_Gate_Entry_Details where Gate_Entry_No='" & txtGateEntryNo.Text & "'", trans)
                End If
                'Dim Isjobwork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(IsAgainstJobWork,0) from tspl_gate_entry_details where gate_entry_no='" & txtGateEntryNo.Text & "'", trans))
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementCounterOnEntryType, clsFixedParameterCode.BulkProcurementCounterOnEntryType, trans)) = 0, False, True) ''Make Setting Balwinder
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select Gate_Entry_Type,Doc_Type,location_code  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + txtGateEntryNo.Text + "'", trans)
                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                    Throw New Exception("Gate Entry No" + txtGateEntryNo.Text + " Not found ")
                End If
                Dim isBulkPro As Boolean = clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Doc_Type")), "BulkProc") = CompairStringResult.Equal
                Dim strLoc As String = clsCommon.myCstr(dtTemp.Rows(0)("location_code"))
                If isPODocumentTypeWise AndAlso isBulkPro Then
                    Dim strGateEntryType As String = clsCommon.myCstr(dtTemp.Rows(0)("Gate_Entry_Type"))
                    If clsCommon.myLen(strGateEntryType) <= 0 Then
                        Throw New Exception("Gate Entry Type not found on Gate Entry screen")
                    End If
                    If clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                        obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.BulkMilkSRN, clsDocTransactionType.BulkProcPurchase, txtLocation.Text)
                    ElseIf clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                        obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.BulkMilkSRN, clsDocTransactionType.BulkProcJobWork, txtLocation.Text)
                    Else
                        Throw New Exception("Wrong Gate Entry Type")
                    End If
                Else
                    If chkJobWork.Checked Then
                        obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.BulkMilkSRN, clsDocTransactionType.BulkProcJobWorkOutward, txtSubLocation.Value)
                    Else
                        obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.BulkMilkSRN, clsDocTransactionType.NA, txtLocation.Text)
                    End If
                End If
                If clsCommon.myLen(obj.SRN_NO) <= 0 Then
                    Throw New Exception("Error In SRN  No Genertion")
                End If
                obj.PO_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.BulkMilkPO, "", txtLocation.Text)
                If clsCommon.myLen(obj.PO_NO) <= 0 Then
                    Throw New Exception("Error In PO  No Genertion")
                End If
                obj.PO_Date = dtpSRNDATE.Value
            Else
                obj.SRN_NO = clsCommon.myCstr(fndSRNNo.Value)
            End If

            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            obj.isApproved = 0
            fndSRNNo.Value = obj.SRN_NO
            obj.SRN_Date = dtpSRNDATE.Value
            obj.Gate_Entry_No = txtGateEntryNo.Text
            obj.Weighment_No = fndWeighmentNo.Value
            obj.Weighment_Date = dtpWeighmentDate.Value
            obj.Vendor_Code = txtVendor.Text
            obj.Loc_Code = txtLocation.Text
            obj.Challan_No = txtChallanNo.Text
            obj.Challan_Date = dtpChallanDate.Value
            obj.Tanker_No = fndTankerNo.Value
            obj.Price_Code = fndPriceChart.Value
            obj.QC_No = txtQCNo.Text
            obj.Qc_Date = dtpQCInTime.Value
            If Not isPost Then
                obj.isPosted = 0
            End If
            obj.Item_Code = gvItem.Rows(0).Cells(colItemCode).Value
            obj.Item_Desc = gvItem.Rows(0).Cells(colItemDesc).Value
            obj.UOM = gvItem.Rows(0).Cells(colUOM).Value
            obj.Gross_Weight = gvItem.Rows(0).Cells(colGrossWeight).Value
            obj.Tare_Weight = gvItem.Rows(0).Cells(colTareWeight).Value
            obj.Net_Weight = gvItem.Rows(0).Cells(colNetWeight).Value
            obj.Net_Weight_Calculate = gvItem.Rows(0).Cells(colNetWeightCalculated).Value
            obj.CLR_Per = clsCommon.myCdbl(gvItem.Rows(0).Cells(colCLR).Value)
            obj.snf_Per = gvItem.Rows(0).Cells(colSNF).Value
            obj.fat_per = gvItem.Rows(0).Cells(colFat).Value
            obj.fat_KG = gvItem.Rows(0).Cells(colFatKG).Value
            obj.SNF_KG = gvItem.Rows(0).Cells(colSNFKG).Value
            obj.fat_Rate = gvItem.Rows(0).Cells(colFatRate).Value
            obj.SNF_Rate = gvItem.Rows(0).Cells(colSNFRate).Value
            obj.Amount = gvItem.Rows(0).Cells(colAmt).Value
            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            obj.SpecialDeduction = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value)
            ''================================================
            obj.Deduction = gvItem.Rows(0).Cells(colDeducRate).Value
            obj.Incentive = gvItem.Rows(0).Cells(colIncenRate).Value
            obj.Actual_Amount = gvItem.Rows(0).Cells(colActAmt).Value
            obj.BasicRate = gvItem.Rows(0).Cells(colMilkRate).Value
            obj.Standardrate = gvItem.Rows(0).Cells(colStandardRate).Value
            obj.NetRate = gvItem.Rows(0).Cells(colNetRate).Value

            obj.FatAmt = gvItem.Rows(0).Cells(colFatAmt).Value
            obj.SnfAmt = gvItem.Rows(0).Cells(colSNFAmt).Value
            obj.FinalMilkRate = gvItem.Rows(0).Cells(colFinalMilkRate).Value
            obj.Transport_Charges = txtTransportCharges.Value
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            obj.Net_Weight_LTR = gvItem.Rows(0).Cells(colNetWeightLTR).Value
            obj.Milk_Amount = gvItem.Rows(0).Cells(colMilkAmount).Value

            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If

            ''richa agarwal remove settings as per Ranjana Mam 
            'If (TankerFromMaster = 1) Then

            obj.Arr = New List(Of clsBulkMilkSRNChemberNoDetails)
            For Each grow As GridViewRowInfo In gvItem.Rows
                Dim objTr As New clsBulkMilkSRNChemberNoDetails()
                objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                objTr.UOM_Calculate = clsCommon.myCstr(grow.Cells(colUOMCalculated).Value)
                objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                objTr.Gross_Weight = grow.Cells(colGrossWeight).Value
                objTr.Tare_Weight = grow.Cells(colTareWeight).Value
                objTr.Net_Weight = grow.Cells(colNetWeight).Value
                objTr.Net_Weight_Calculate = grow.Cells(colNetWeightCalculated).Value
                objTr.snf_Per = grow.Cells(colSNF).Value
                objTr.fat_per = grow.Cells(colFat).Value
                objTr.fat_KG = grow.Cells(colFatKG).Value
                objTr.SNF_KG = grow.Cells(colSNFKG).Value
                objTr.fat_Rate = grow.Cells(colFatRate).Value
                objTr.SNF_Rate = grow.Cells(colSNFRate).Value
                objTr.Amount = grow.Cells(colAmt).Value
                objTr.SpecialDeduction = clsCommon.myCdbl(grow.Cells(colSpecialDeduction).Value)
                objTr.Deduction = grow.Cells(colDeducRate).Value
                objTr.Incentive = grow.Cells(colIncenRate).Value
                objTr.Actual_Amount = grow.Cells(colActAmt).Value
                objTr.BasicRate = grow.Cells(colMilkRate).Value
                objTr.Standardrate = grow.Cells(colStandardRate).Value
                objTr.NetRate = grow.Cells(colNetRate).Value
                objTr.FatAmt = grow.Cells(colFatAmt).Value
                objTr.SnfAmt = grow.Cells(colSNFAmt).Value
                objTr.FinalMilkRate = grow.Cells(colFinalMilkRate).Value
                objTr.Price_Code = grow.Cells(colpriceCode).Value
                objTr.MILK_GRADE_CODE = grow.Cells(colMilkGradeCode).Value
                objTr.MIKL_TYPE_CODE = grow.Cells(colMilkTtypeCode).Value
                objTr.fat_Qty = grow.Cells(colFatQTy).Value
                objTr.SNF_Qty = grow.Cells(colSNFQty).Value
                objTr.TotalStandardQty = grow.Cells(colTotalStdQTy).Value
                objTr.Incentive_Amt = grow.Cells(colIncentiveAmt).Value
                objTr.Deduction_Amt = grow.Cells(colDeductionAmt).Value
                objTr.Net_Weight_LTR = grow.Cells(colNetWeightLTR).Value
                objTr.Transport_Charges = grow.Cells(colTranspoterCharge).Value
                objTr.Milk_Amount = grow.Cells(colMilkAmount).Value
                'If (clsCommon.myLen(objTr.Chamber_Desc) > 0) Then
                '    obj.Arr.Add(objTr)
                'End If
                obj.Arr.Add(objTr)
            Next
            'End If
            If (TankerFromMaster = 0) Then

                If gvRange IsNot Nothing AndAlso gvRange.Rows.Count > 0 Then
                    Dim i As Integer = 0
                    Dim objParam As New clsSRNParam
                    obj.arrObj = New List(Of clsSRNParam)
                    For i = 0 To gvRange.Rows.Count - 1
                        objParam = New clsSRNParam
                        objParam.SRN_No = clsCommon.myCstr(obj.SRN_NO)
                        objParam.Parameter = clsCommon.myCstr(gvRange.Rows(i).Cells("Code").Value)
                        objParam.Lower_Range = clsCommon.myCstr(gvRange.Rows(i).Cells("LowerRange").Value)
                        objParam.Upper_Range = clsCommon.myCstr(gvRange.Rows(i).Cells("UpperRange").Value)
                        objParam.value = clsCommon.myCstr(gvRange.Rows(i).Cells("Value").Value)
                        objParam.QCValue = clsCommon.myCstr(gvRange.Rows(i).Cells("QcValue").Value)
                        objParam.Incen_Deduc = clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value)
                        obj.arrObj.Add(objParam)
                    Next
                End If
            End If
            If clsBulkMilkSRN.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If isAutoSRN = False Then
                        If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                        End If
                    End If
                End If
                If AllowNLevel Then
                    clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(obj.SRN_NO), dtpSRNDATE.Text, "", "", clsCommon.myCdbl(obj.Actual_Amount), clsCommon.myCdbl(totalqty), "", objApproval)
                End If
                LoadData(obj.SRN_NO, NavigatorType.Current)
                Exit Sub
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Function SaveMILKRGPData()
        Try
            Dim strSiloNo As String = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & txtGateEntryNo.Text & "'")
            Dim JobworkLocaion As DataTable = clsDBFuncationality.GetDataTable("select * from tspl_location_Master inner join TSPL_LOCATION_MASTER_Jobwork_Item on TSPL_LOCATION_MASTER_Jobwork_Item.Main_Location_Code=Location_Code where location_Code='" & txtLocation.Text & "' and coalesce(is_Jobwork,'1')='1' and coalesce(jobwork_vendor,'')<>'' and coalesce(TSPL_LOCATION_MASTER_Jobwork_Item.jobwork_Item,'')<>''")
            If JobworkLocaion.Rows.Count <= 0 Then
                Return True
            End If
            For Each row As GridViewRowInfo In gvItem.Rows
                For Each rows As DataRow In JobworkLocaion.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(rows.Item("JObwork_Item1")), clsCommon.myCstr(row.Cells(colItemCode).Value)) = CompairStringResult.Equal Then 'clsCommon.CompairString(clsCommon.myCstr(JobworkLocaion.Rows(0).Item("JObwork_Vendor")), txtVendor.Text) = CompairStringResult.Equal And
                        Dim obj As New clsMilkRGPHead()
                        obj.chklocstion = "N"
                        obj.srnlocation = ""
                        obj.RGP_No = ""
                        obj.RGP_Date = dtpSRNDATE.Value
                        obj.Doc_Type = "RGP"
                        ''richa Ticket No BM00000003061 on 01/08/2014
                        '-------------------------------------------
                        obj.Mode_Of_Transport = ""
                        obj.Cash_Memo_Detail = ""
                        obj.Vendor_Code = clsCommon.myCstr(JobworkLocaion.Rows(0).Item("JObwork_Vendor")) 'txtVendor.Text
                        obj.Vendor_Name = clsDBFuncationality.getSingleValue("select vendor_name from tspl_Vendor_Master where Vendor_Code='" & clsCommon.myCstr(JobworkLocaion.Rows(0).Item("JObwork_Vendor")) & "'") 'lblVendorName.Text
                        obj.VehicleNo = ""
                        obj.GPNo = ""
                        obj.GPDate = Nothing
                        obj.Remarks = txtRemarks.Text
                        obj.Reason = Nothing

                        obj.Document_Amount = clsCommon.myCdbl(lblActualAmount.Text)
                        obj.Delivered_ByName = clsCommon.myCstr(objCommonVar.CurrentUser)
                        obj.Location = txtLocation.Text
                        obj.Delivered_By = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select emp_COde from tspl_EMployee_Master where User_Code='" & objCommonVar.CurrentUserCode & "'"))
                        If clsCommon.myLen(obj.Delivered_By) <= 0 Then
                            obj.Delivered_By = objCommonVar.CurrentUserCode
                        End If
                        obj.Department = Nothing

                        obj.CostCentre = Nothing
                        obj.CostCentreDesc = Nothing
                        '' Anubhooti 09-Oct-2014 BM00000003663
                        ''
                        '' Anubhooti 10-Dec-2014 BM00000003662
                        obj.Item_Conversion_Type = "N"
                        ''

                        obj.PO_Id = ""
                        obj.Against_As_It_Is = 0


                        obj.Arr = New List(Of clsMilkRGPDetail)
                        For Each grow As GridViewRowInfo In gvItem.Rows
                            FillQCGrid(clsCommon.myCstr(grow.Cells(colItemCode).Value), txtLocation.Text, txtLocation.Text)
                            obj.ArrQC = New List(Of clsMilkRGPIssueQCDetail)
                            For Each grow_qc As GridViewRowInfo In gv_qc.Rows
                                Dim objtr_qc As New clsMilkRGPIssueQCDetail()

                                objtr_qc.sno = CInt(grow_qc.Cells(colQCsno).Value)
                                objtr_qc.frm_loc_code = clsCommon.myCstr(grow_qc.Cells(colQCloc_code).Value)
                                objtr_qc.to_loc_code = clsCommon.myCstr(grow_qc.Cells(colQCToloc_code).Value)
                                objtr_qc.itemcode = clsCommon.myCstr(grow_qc.Cells(colQCitemcode).Value)
                                objtr_qc.param_code = clsCommon.myCstr(grow_qc.Cells(colQCparamcode).Value)
                                objtr_qc.lrange = clsCommon.myCdbl(grow_qc.Cells(colQCrange1).Value)
                                objtr_qc.urange = clsCommon.myCdbl(grow_qc.Cells(colQCrange2).Value)
                                objtr_qc.value1 = clsCommon.myCstr(grow_qc.Cells(colQCvalue1).Value)
                                objtr_qc.value2 = clsCommon.myCstr(grow_qc.Cells(colQCvalue2).Value)
                                objtr_qc.status_grid = clsCommon.myCstr(grow_qc.Cells(colQCstatus).Value)
                                objtr_qc.QCRange = clsCommon.myCdbl(grow_qc.Cells(colQCRange).Value)
                                objtr_qc.QCStatus = clsCommon.myCstr(grow_qc.Cells(colQCOutStatus).Value)
                                If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select type from tspl_parameter_master where code='" & objtr_qc.param_code & "'")), "FAT") = CompairStringResult.Equal Then
                                    objtr_qc.QCValue = clsCommon.myCdbl(grow.Cells(colFat).Value)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select type from tspl_parameter_master where code='" & objtr_qc.param_code & "'")), "SNF") = CompairStringResult.Equal Then
                                    objtr_qc.QCValue = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                                Else
                                    objtr_qc.QCValue = clsCommon.myCstr(grow_qc.Cells(colQCValue).Value)
                                End If


                                If objtr_qc.status_grid = "None" Then
                                    objtr_qc.status_grid = ""
                                End If
                                If objtr_qc.QCStatus = "None" Then
                                    objtr_qc.QCStatus = ""
                                End If

                                objtr_qc.remarks = clsCommon.myCstr(grow_qc.Cells(colQCremarks).Value).Replace("'", "`")

                                If clsCommon.myLen(objtr_qc.param_code) > 0 Then
                                    obj.ArrQC.Add(objtr_qc)
                                End If
                            Next

                            Dim objTr As New clsMilkRGPDetail()
                            objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                            objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                            objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemDesc).Value)
                            objTr.RGP_Qty = clsCommon.myCdbl(grow.Cells(colNetWeight).Value)
                            objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUOM).Value)
                            objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colFinalMilkRate).Value) 'clsCommon.myCdbl(grow.Cells(colRate).Value)
                            objTr.Amount = clsCommon.myCdbl(grow.Cells(colActAmt).Value)
                            objTr.FAT_pers = clsCommon.myCdbl(grow.Cells(colFat).Value)
                            objTr.FAT_KG = clsCommon.myCdbl(grow.Cells(colFatKG).Value)
                            objTr.FAT_Price = clsCommon.myCdbl(grow.Cells(colFatAmt).Value)
                            objTr.FAT_Cost = clsCommon.myCdbl(grow.Cells(colFatRate).Value)
                            objTr.SNF_Cost = clsCommon.myCdbl(grow.Cells(colSNFRate).Value)
                            objTr.SNF_Pers = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                            objTr.SNF_KG = clsCommon.myCdbl(grow.Cells(colSNFKG).Value)
                            objTr.SNF_Price = clsCommon.myCdbl(grow.Cells(colSNFAmt).Value)
                            objTr.TanKer_No = clsCommon.myCstr(fndTankerNo.Value)
                            objTr.Bulk_Milk_Srn_No = clsCommon.myCstr(fndSRNNo.Value)
                            objTr.Location_Code = strSiloNo ' clsCommon.myCstr(txtLocation.Text)
                            objTr.Location_Type = clsDBFuncationality.getSingleValue("select case when coalesce(is_section,'N')='Y' then '3' when  coalesce(Is_Sub_Location,'N')='Y' then '2' else '1' end from TSPL_LOCATION_MASTER where Location_Code='" & strSiloNo & "'")
                            objTr.Specification = ""
                            '' Anubhooti 06-Feb-2015(Unit Cost)
                            objTr.Approx_Cost = clsCommon.myCdbl(grow.Cells(colFinalMilkRate).Value)
                            If clsCommon.myLen(objTr.Item_Code) > 0 Then
                                obj.Arr.Add(objTr)
                            End If
                        Next





                        If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                            Return False
                        End If

                        '=======================================================

                        '======================================================


                        If (obj.SaveData(obj, True, True)) Then
                            ' clsMilkRGPHead.PostData(obj.RGP_No)
                            clsCommon.MyMessageBoxShow("RGP [" & obj.RGP_No & "] has been created.", Me.Text)
                        End If
                    End If
                Next
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Sub FillQCGrid(ByVal CurrentIcode As String, ByVal Frm_Loc_Code As String, ByVal To_Loc_Code As String)
        Dim qry As String = String.Empty
        Dim check As Integer = 0
        Try

            LoadQCBlankGrid()
            If clsCommon.myLen(gvItem.Rows(0).Cells(colItemCode).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gvItem.Focus()
                gvItem.Select()
                Throw New Exception("Fill item detail first.")
            End If
            Dim allicode As String = ""

            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
                clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)")
            Else
                clsDBFuncationality.ExecuteNonQuery("create table TEMP_LOC_QC_PARAM (Item_Code varchar(50) null,Frm_Loc varchar(50) null,To_Loc varchar(50) null)")
            End If

            If CurrentIcode IsNot Nothing AndAlso clsCommon.myLen(CurrentIcode) > 0 Then
                allicode = CurrentIcode
                clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + CurrentIcode + "','" + Frm_Loc_Code + "','" + To_Loc_Code + "'")
            Else
                For Each grow As GridViewRowInfo In gvItem.Rows
                    allicode = allicode + "','" + clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "','" + clsCommon.myCstr(txtLocation.Text) + "','" + clsCommon.myCstr(txtLocation.Text) + "'")
                Next
            End If

            If clsCommon.myLen(allicode) > 0 AndAlso allicode.Substring(0, 3) = "','" Then
                allicode = allicode.Substring(3, allicode.Length - 3)
            End If

            qry = "select ROW_NUMBER() over(order by TSPL_ITEM_QC_PARAMETER_MASTER.Code) as Sno,TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description as parameterdesc,TSPL_PARAMETER_MASTER.Type,(Case when TSPL_PARAMETER_MASTER.Nature='A' then 'Alphanumeric' else case when TSPL_PARAMETER_MASTER.Nature='B' then 'Boolean' else case when TSPL_PARAMETER_MASTER.Nature='R' then 'Range' end end end) as Nature,sum(TSPL_ITEM_QC_PARAMETER_MASTER.actual_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Lower_range,sum(TSPL_ITEM_QC_PARAMETER_MASTER.Upper_range)/count(TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code) as Upper_range,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_value) as Value1,max(TSPL_ITEM_QC_PARAMETER_MASTER.Value2) as Value2,max(TSPL_ITEM_QC_PARAMETER_MASTER.actual_status) as Status from TSPL_ITEM_QC_PARAMETER_MASTER "
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code "
            qry += " left outer join TEMP_LOC_QC_PARAM on TEMP_LOC_QC_PARAM.item_code=TSPL_ITEM_QC_PARAMETER_MASTER.item_code "
            qry += " where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code in ('" + allicode + "') group by TEMP_LOC_QC_PARAM.frm_loc,TEMP_LOC_QC_PARAM.to_loc,TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_QC_PARAMETER_MASTER.Code,TSPL_PARAMETER_MASTER.Description,TSPL_PARAMETER_MASTER.Type,TSPL_PARAMETER_MASTER.Nature"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If gv_qc.Rows.Count > 0 AndAlso clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value) <= 0 Then
                gv_qc.Rows.RemoveAt(gv_qc.Rows.Count - 1)
            End If

            Dim found As Boolean = False
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    found = False
                    For Each grow As GridViewRowInfo In gv_qc.Rows
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCparamcode).Value), clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCitemcode).Value), clsCommon.myCstr(dr("item_code"))) = CompairStringResult.Equal Then
                            found = True
                            GoTo a
                        End If
                    Next
a:
                    If Not found And clsCommon.myLen(clsCommon.myCstr(dr("frm_loc"))) > 0 Then
                        gv_qc.Rows.AddNew()
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCsno).Value = CInt(dr("sno"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_code).Value = clsCommon.myCstr(dr("frm_loc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_name).Value = clsLocation.GetName(clsCommon.myCstr(dr("frm_loc")), Nothing)
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_code).Value = clsCommon.myCstr(dr("to_loc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_name).Value = clsLocation.GetName(clsCommon.myCstr(dr("to_loc")), Nothing)
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCiname).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparamcode).Value = clsCommon.myCstr(dr("Code"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_desc).Value = clsCommon.myCstr(dr("parameterdesc"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value = clsCommon.myCstr(dr("Type"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value = clsCommon.myCstr(dr("Nature"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value = clsCommon.myCdbl(dr("Lower_range"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange2).Value = clsCommon.myCdbl(dr("Upper_range"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value = clsCommon.myCstr(dr("Status"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value = clsCommon.myCstr(dr("Value1"))
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue2).Value = clsCommon.myCstr(dr("Value2"))

                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Range") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Boolean") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Alphanumeric") = CompairStringResult.Equal Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                    End If ''found cond.

                Next
            Else
                'Throw New Exception("Mapped first QC parameter with items in Item Master screen")
            End If

            '===========refresh sno==
            For Each grow As GridViewRowInfo In gv_qc.Rows
                grow.Cells(colQCsno).Value = grow.Index + 1
            Next

            dt = Nothing
        Catch ex As Exception
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If

        End Try
    End Sub

    Private Sub LoadQCBlankGrid()
        gv_qc.Rows.Clear()
        gv_qc.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colQCsno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reposno)
        reposno = Nothing

        Dim bomcodeFrom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeFrom.FormatString = ""
        bomcodeFrom.Name = colQCloc_code
        bomcodeFrom.Width = 80
        bomcodeFrom.HeaderText = "From Location Code"
        bomcodeFrom.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeFrom)
        bomcodeFrom = Nothing

        Dim bomcodeFromName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeFromName.FormatString = ""
        bomcodeFromName.Name = colQCloc_name
        bomcodeFromName.Width = 130
        bomcodeFromName.HeaderText = "From Location"
        bomcodeFromName.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeFromName)
        bomcodeFromName = Nothing

        Dim bomcodeTO As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeTO.FormatString = ""
        bomcodeTO.Name = colQCToloc_code
        bomcodeTO.Width = 80
        bomcodeTO.HeaderText = "To Location Code"
        bomcodeTO.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeTO)
        bomcodeTO = Nothing

        Dim bomcodeTOName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeTOName.FormatString = ""
        bomcodeTOName.Name = colQCToloc_name
        bomcodeTOName.Width = 130
        bomcodeTOName.HeaderText = "To Location"
        bomcodeTOName.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcodeTOName)
        bomcodeTOName = Nothing

        Dim bomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode.FormatString = ""
        bomcode.Name = colQCitemcode
        bomcode.Width = 100
        bomcode.HeaderText = "Item Code"
        bomcode.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(bomcode)
        bomcode = Nothing

        Dim repolocname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname.FormatString = ""
        repolocname.Name = colQCiname
        repolocname.Width = 200
        repolocname.HeaderText = "Item Name"
        repolocname.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolocname)
        repolocname = Nothing

        Dim bomcode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode1.FormatString = ""
        bomcode1.Name = colQCparamcode
        bomcode1.Width = 100
        bomcode1.HeaderText = "Parameter Code"
        bomcode1.ReadOnly = True
        'bomcode1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        'bomcode1.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_qc.MasterTemplate.Columns.Add(bomcode1)
        bomcode1 = Nothing

        Dim repolocname1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname1.FormatString = ""
        repolocname1.Name = colQCparam_desc
        repolocname1.Width = 200
        repolocname1.HeaderText = "Parameter Description"
        repolocname1.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolocname1)
        repolocname1 = Nothing

        Dim repotype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repotype.FormatString = ""
        repotype.Name = colQCparam_type
        repotype.Width = 80
        repotype.HeaderText = "Parameter Type"
        repotype.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repotype)
        repotype = Nothing

        Dim reponature As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponature.FormatString = ""
        reponature.Name = colQCparam_nature
        reponature.Width = 80
        reponature.HeaderText = "Nature"
        reponature.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(reponature)
        reponature = Nothing

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colQCrange1
        repolower.Width = 80
        repolower.HeaderText = "Std. Range"
        repolower.DecimalPlaces = 2
        repolower.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper.Name = colQCrange2
        repoupper.Width = 80
        repoupper.HeaderText = "Upper Range"
        repoupper.DecimalPlaces = 2
        repoupper.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(repoupper)
        repoupper = Nothing

        Dim repovalue1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue1.Name = colQCvalue1
        repovalue1.Width = 80
        repovalue1.HeaderText = "Std. Value"
        repovalue1.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repovalue1)
        repovalue1 = Nothing

        Dim repovalue2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue2.Name = colQCvalue2
        repovalue2.Width = 80
        repovalue2.HeaderText = "Value-2"
        repovalue2.MaxLength = 30
        repovalue2.IsVisible = False
        gv_qc.MasterTemplate.Columns.Add(repovalue2)
        repovalue2 = Nothing

        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.Name = colQCstatus
        repostatus.Width = 80
        repostatus.HeaderText = "Std. Status(Yes/No)"
        repostatus.DataSource = LoadCombobox()
        repostatus.ValueMember = "Code"
        repostatus.DisplayMember = "Name"
        repostatus.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repostatus)
        repostatus = Nothing

        Dim repoupper1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper1.Name = colQCRange
        repoupper1.Width = 80
        repoupper1.HeaderText = "Actual Range"
        repoupper1.DecimalPlaces = 2
        gv_qc.MasterTemplate.Columns.Add(repoupper1)
        repoupper1 = Nothing

        Dim repovalue21 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue21.Name = colQCValue
        repovalue21.Width = 80
        repovalue21.HeaderText = "Actual Value"
        repovalue21.MaxLength = 30
        repovalue21.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repovalue21)
        repovalue21 = Nothing

        Dim repostatus1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus1.Name = colQCOutStatus
        repostatus1.Width = 80
        repostatus1.HeaderText = "Actual Status(Yes/No)"
        repostatus1.DataSource = LoadCombobox()
        repostatus1.ValueMember = "Code"
        repostatus1.DisplayMember = "Name"
        'repostatus.ReadOnly = True
        gv_qc.MasterTemplate.Columns.Add(repostatus1)
        repostatus1 = Nothing

        Dim repoHis As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHis.FormatString = ""
        repoHis.Name = colQCHistort
        repoHis.Width = 80
        repoHis.ReadOnly = True
        repoHis.HeaderText = "History"
        gv_qc.MasterTemplate.Columns.Add(repoHis)
        repoHis = Nothing

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.Name = colQCremarks
        reporem.Width = 150
        reporem.MaxLength = 200
        reporem.HeaderText = "Remarks"
        gv_qc.MasterTemplate.Columns.Add(reporem)
        reporem = Nothing

        gv_qc.AllowDeleteRow = True
        gv_qc.AllowAddNewRow = False
        gv_qc.ShowGroupPanel = False
        gv_qc.AllowColumnReorder = True
        gv_qc.AllowRowReorder = False
        gv_qc.EnableSorting = False
        gv_qc.EnableFiltering = False
        gv_qc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_qc.MasterTemplate.ShowRowHeaderColumn = False
        gv_qc.Rows.AddNew()
    End Sub

    Function LoadCombobox() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Function allowToSave() As Boolean
        Try
            ' = KUNAL > TICKET : BM00000009575 =====
            If AllowFutureDateTransaction(dtpSRNDATE.Value, Nothing) = False Then
                dtpSRNDATE.Focus()
                Return False
            End If

            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                Throw New Exception("Please Select Tanker No")
                errorControl.SetError(fndTankerNo, "Please Select Tanker No")
            Else
                errorControl.ResetError(fndTankerNo)
            End If
            If TankerFromMaster = 0 Then
                If clsCommon.myLen(fndPriceChart.Value) <= 0 Then
                    Throw New Exception("Please Select a Price Chart")
                    errorControl.SetError(fndPriceChart, "Please Select a Price Chart")
                Else
                    errorControl.ResetError(fndPriceChart)
                End If
            End If


            If clsCommon.myLen(gvItem.Rows(0).Cells(colMilkRate)) <= 0 Then
                Throw New Exception("Please enter Basic Rate")
            End If

            ' done by priti KDI/12/06/18-000355
            Dim GateEntryDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Tspl_Gate_Entry_Details.Date_And_Time from Tspl_Gate_Entry_Details where Tspl_Gate_Entry_Details.Gate_Entry_No='" & txtGateEntryNo.Text & "'", Nothing))
            If clsCommon.myCDate(GateEntryDate, "dd/MMM/yyyy") > clsCommon.myCDate(dtpSRNDATE.Value, "dd/MMM/yyyy") Then
                Throw New Exception("SRN Date should not be smaller than Gate entry date")
                errorControl.SetError(dtpSRNDATE, "SRN Date should not be smaller than Gate entry date")
            Else
                errorControl.ResetError(dtpSRNDATE)
            End If
            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from TSPL_Bulk_MILK_SRN where gate_entry_no='" & txtGateEntryNo.Text & "' and srn_no <>'" & fndSRNNo.Value & "' and isnull(srn_return_no,'')='' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other SRN.")
            End If
            If TankerFromMaster = 1 And AllowManualRateonPO = 0 Then
                For ii As Integer = 0 To gvItem.Rows.Count - 1
                    Dim strpriceCode As String = gvItem.Rows(ii).Cells(colpriceCode).Value
                    If clsCommon.myLen(strpriceCode) = 0 Then
                        Throw New Exception("SRN Cannot be created.Price chart is not mapped on Gate entry At Line No" + clsCommon.myCstr(ii + 1))
                        Exit Function
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then
            SaveData(False, False)
        End If

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(fndSRNNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Enter SRN No To delete ", Me.Text)
        Else
            'Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNO.Value & "' union all select COUNT(*) as row_Count from tspl_quality_check where gate_entry_no='" & fndGateEntryNO.Value & "') xx ")
            'If isUsed > 0 Then
            'clsCommon.MyMessageBoxShow("Gate Entry No is in use")
            'Exit Sub
            'End If
            If myMessages.deleteConfirm() Then

                If AllowNLevel Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(fndSRNNo.Value))
                End If

                If clsBulkMilkSRN.deleteData(fndSRNNo.Value, Nothing) Then
                    Reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub

    ''Updateed by Preeti Gupta ticket no[BM00000004915,BHA/03/07/18-000126]
    Sub printData(ByVal SRNNo As String)
        Dim PrintTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToPrintTimeWithDocumentDate, clsFixedParameterCode.AllowToPrintTimeWithDocumentDate, Nothing)
        If clsCommon.myLen(SRNNo) > 0 Then
            Dim strQuery As String = "select Convert (varchar,TSPL_Bulk_MILK_SRN.Challan_Date ,103) as Challan_Date,TSPL_COMPANY_MASTER.Add1 as Comp_Add1, TSPL_COMPANY_MASTER.Add2 as Comp_Add2,TSPL_COMPANY_MASTER.Add3 as Comp_Add3,TSPL_COMPANY_MASTER.CINNo as Comp_CINNo,TSPL_COMPANY_MASTER.Access_Officer as Comp_Access_Officer,TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTReg_No, TSPL_COMPANY_MASTER.Email as Comp_Email, TSPL_COMPANY_MASTER.Phone1 as Comp_Phone1,TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2,TSPL_COMPANY_MASTER.PAN_No as Comp_PAN_No, TSPL_Bulk_MILK_SRN.Challan_No,TSPL_COMPANY_MASTER.Logo_Img ,TSPL_Bulk_MILK_SRN.Net_weight_calculate as QtyInLtr,TSPL_Bulk_MILK_SRN.finalmilkrate,TSPL_Bulk_MILK_SRN.Actual_Amount,tspl_Bulk_price_master.Total_Solid_Rate,  TSPL_ITEM_MASTER.HSN_Code,tspl_state_master_for_location_state.GST_STATE_Code as LOC_GST_State_Code,TSPL_LOCATION_MASTER.GSTNO as Loc_GstInNo ,TSPL_VENDOR_MASTER.GSTFinalNo AS Vendor_GSTIN_NO,TSPL_STATE_MASTER.GST_STATE_Code AS Vendor_GST_StateCode, TSPL_Bulk_MILK_SRN.Created_By,TSPL_Bulk_MILK_SRN.Modify_By , TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name  as comp_Name,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_add3,  concat('Tin No. : ' ,TSPL_LOCATION_MASTER.TIN_No) AS Loc_Tin_No, concat('Pin : ',TSPL_LOCATION_MASTER.Pin_Code) AS Loc_Pin_Code, concat('Ph. : ',CASE WHEN ISNULL(TSPL_LOCATION_MASTER.Phone1, '') = '(+__)__________' THEN '' ELSE TSPL_LOCATION_MASTER.Phone1 END + CASE WHEN ISNULL(TSPL_LOCATION_MASTER.Phone2, '') <> '(+__)__________' THEN ', ' + TSPL_LOCATION_MASTER.Phone2 ELSE '' END) AS Loc_Phn ,TSPL_Bulk_MILK_SRN.SRN_NO "
            If PrintTime = "1" Then
                strQuery += " ,TSPL_Bulk_MILK_SRN.SRN_Date "
            Else
                strQuery += " ,convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) as SRN_Date"
            End If
            strQuery += " ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_Bulk_MILK_SRN.Tanker_No,TSPL_ITEM_MASTER.Item_Desc ,TSPL_Bulk_MILK_SRN.Net_Weight ,TSPL_Bulk_MILK_SRN.fat_per ,TSPL_Bulk_MILK_SRN.snf_Per" + Environment.NewLine +
            ",case when len(isnull(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.UOM_Calculate,''))>0 and isnull(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Net_Weight_Calculate,0)>0 then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.UOM_Calculate else TSPL_Bulk_MILK_SRN.UOM end as UOM " + Environment.NewLine +
            ",t_Acidity.Param_Field_Value as Acidity  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_LOCATION_MASTER.City_Code)>0 then ', '+TSPL_LOCATION_MASTER.City_Code else ' ' end + case when len(TSPL_LOCATION_MASTER.State )>0 then TSPL_LOCATION_MASTER.State else '' end  as Loc_address " &
            " ,tSPL_Bulk_MILK_SRN_Chember_Details.line_no,TSPL_Bulk_MILK_SRN_Chember_Details.Item_code as Det_Item_code,tspl_item_master_Det.item_desc as Det_Item_Desc,TSPL_Bulk_MILK_SRN_Chember_Details.uom as Det_Uom," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.chamber_desc, TSPL_Bulk_MILK_SRN_Chember_Details.chamber_Qty, TSPL_Bulk_MILK_SRN_Chember_Details.Gross_Weight as det_Gross_Weight," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.Tare_Weight as Det_Tare_Weight" + Environment.NewLine +
           ",case when len(isnull(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.UOM_Calculate,''))>0 and isnull(TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Net_Weight_Calculate,0)>0 then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Net_Weight_Calculate else TSPL_Bulk_MILK_SRN_Chember_Details.Net_Weight end as det_Net_Weight " + Environment.NewLine +
           ", TSPL_Bulk_MILK_SRN_Chember_Details.snf_per as Det_snf_per," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.Fat_per as Det_Fat_per, TSPL_Bulk_MILK_SRN_Chember_Details.Fat_KG as Det_Fat_KG, TSPL_Bulk_MILK_SRN_Chember_Details.SNF_KG as Det_SNF_KG," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.Fat_Rate as det_Fat_Rate, TSPL_Bulk_MILK_SRN_Chember_Details.SNF_Rate as Det_SNF_Rate, TSPL_Bulk_MILK_SRN_Chember_Details.Actual_Amount as Det_Actual_Amount," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.BasicRate as Det_BasicRate, TSPL_Bulk_MILK_SRN_Chember_Details.StandardRate as det_StandardRate, TSPL_Bulk_MILK_SRN_Chember_Details.NetRate as Det_NetRate," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.FatAmt as Det_FatAmt, TSPL_Bulk_MILK_SRN_Chember_Details.SnfAmt as Det_SnfAmt, TSPL_Bulk_MILK_SRN_Chember_Details.FinalMilkRate as Det_FinalMilkRate," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.Price_Code, TSPL_Bulk_MILK_SRN_Chember_Details.Fat_Qty as Det_Fat_Qty, TSPL_Bulk_MILK_SRN_Chember_Details.SNF_Qty as Det_SNF_Qty," &
           " TSPL_Bulk_MILK_SRN_Chember_Details.TotalStandardQty,TSPL_Bulk_MILK_SRN.Transport_Charges as Transport_Charges_HT ,TSPL_Bulk_MILK_SRN.Milk_Amount as Milk_Amount_HT, TSPL_Bulk_MILK_SRN.Net_Weight_LTR as Net_Weight_LTR_HT ,TSPL_Bulk_MILK_SRN_Chember_Details.Transport_Charges as Transport_Charges_DT  ,TSPL_Bulk_MILK_SRN_Chember_Details.Milk_Amount as Milk_Amount_DT, TSPL_Bulk_MILK_SRN_Chember_Details.Net_Weight_LTR as Net_Weight_LTR_DT,t_CLR.Param_Field_Value as CLR " &
            " from TSPL_Bulk_MILK_SRN" &
            " left join TSPL_Bulk_MILK_SRN_Chember_Details on TSPL_Bulk_MILK_SRN_Chember_Details.SRN_No=TSPL_Bulk_MILK_SRN.SRN_No" &
            " left join tspl_item_master as tspl_item_master_Det on tspl_item_master_Det.item_code=TSPL_Bulk_MILK_SRN_Chember_Details.item_code"
            strQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_Bulk_MILK_SRN.Loc_Code "
            strQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code "
            strQuery += " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_Bulk_MILK_SRN.Vendor_Code "
            strQuery += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_Bulk_MILK_SRN.Item_Code  "
            strQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.*    From TSPL_Bulk_MILK_SRN Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No        = TSPL_Bulk_MILK_SRN.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'Acidity') t_Acidity On t_Acidity.QC_No = TSPL_Bulk_MILK_SRN.QC_No and isnull(t_Acidity.line_no,0)=isnull(TSPL_Bulk_MILK_SRN_Chember_Details.Line_No,0) left outer join tspl_state_master as tspl_state_master_for_location_state on  tspl_state_master_for_location_state.state_code=tspl_location_master.state left outer join TSPL_STATE_MASTER on TSPL_VENDOR_MASTER.State_Code= TSPL_STATE_MASTER.State_Code "
            strQuery += "  left join tspl_Bulk_price_master on tspl_Bulk_price_master.Price_Code =TSPL_Bulk_MILK_SRN.Price_Code"
            strQuery += "  left outer join TSPL_QC_Parameter_Detail t_CLR on t_CLR.QC_No=TSPL_Bulk_MILK_SRN.QC_No  and t_CLR .Param_Type='CLR' and TSPL_Bulk_MILK_SRN_Chember_Details.Line_No=t_CLR.line_no "
            strQuery += " where  TSPL_Bulk_MILK_SRN.SRN_NO='" & SRNNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkSRN", "Milk SRN", clsCommon.myCDate(dtpSRNDATE.Value))
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an invoice to print", Me.Text)
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData(fndSRNNo.Value)
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Sub postData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                '=======Added by preeti gupta Against ticket no[MIL/04/06/19-000091]
                If Not allowToSave() Then
                    Exit Sub
                End If
                '=================================================================
                SaveData(True, False)
                If IsAutoMilkRGP Then
                    If Not SaveMILKRGPData() Then
                        Exit Sub
                    End If
                End If
                If (clsBulkMilkSRN.postData(fndSRNNo.Value, Me.Form_ID)) Then
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
                LoadData(fndSRNNo.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strSrnNo As String, ByVal nav As NavigatorType)
        Try
            isInsideLoadData = True
            Dim objApproval As New clsApply_Approval()
            obj = clsBulkMilkSRN.getData(strSrnNo, nav)

            If obj IsNot Nothing Then
                chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
                txtSubLocation.Value = obj.Joblocation_Code
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                End If
                isDocPosted = IIf(obj.isPosted = 0, False, True)
                txtLocation.Enabled = False
                fndSRNNo.Value = obj.SRN_NO
                dtpSRNDATE.Value = obj.SRN_Date
                fndWeighmentNo.Value = obj.Weighment_No
                dtpWeighmentDate.Value = obj.Weighment_Date
                txtQCNo.Text = obj.QC_No
                txtGateEntryNo.Text = obj.Gate_Entry_No
                txtVendor.Text = obj.Vendor_Code
                lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
                txtLocation.Text = obj.Loc_Code
                lblLocationDesc.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
                txtChallanNo.Text = obj.Challan_No
                dtpChallanDate.Value = obj.Challan_Date
                fndPriceChart.Value = obj.Price_Code
                txtTransportCharges.Value = obj.Transport_Charges
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select * from tspl_Bulk_price_master where Price_Code ='" & fndPriceChart.Value & "'  ")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    TxtFatWeightage.Text = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                    TxtSNFWeightage.Text = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                    txtfatPercentage.Text = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                    txtSNFPercentage.Text = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                    txtStanadardrate.Text = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                    txtTolerance.Text = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                    txtTotalSolidRate.Value = clsCommon.myCdbl(dt.Rows(0)("Total_Solid_Rate"))
                    txtUOM.Value = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                End If
                ApplyUOM()

                fndTankerNo.Value = obj.Tanker_No
                fndReferenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
                Dim objQ As clsQualityCheck = clsQualityCheck.getData(txtQCNo.Text, "BulkProc", NavigatorType.Current)
                dtpQCInTime.Value = objQ.QC_In_Date_Time
                dtpQCOutTime.Value = objQ.QC_Out_Date_Time
                txtRemarks.Text = objQ.Remarks
                txtDipValue.Text = objQ.Dip_Value
                loadBlankItemGrid()
                If TankerFromMaster = 0 Then
                    gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                    gvItem.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                    If clsCommon.myLen(obj.Item_Code) > 0 Then
                        gvItem.Rows(0).Cells(colItemHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where Item_Code ='" + obj.Item_Code + "'"))
                    End If

                    gvItem.Rows(0).Cells(colUOM).Value = obj.UOM
                    gvItem.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.Gross_Weight)
                    gvItem.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(obj.Tare_Weight)
                    gvItem.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(obj.Net_Weight)
                    gvItem.Rows(0).Cells(colNetWeightCalculated).Value = clsCommon.myFormat(obj.Net_Weight_Calculate)
                    gvItem.Rows(0).Cells(colCLR).Value = clsCommon.myFormat(MyMath.RoundDown(obj.CLR_Per, 2))
                    If obj.isPosted = 0 Then
                        gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_per, 2))
                        gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(obj.snf_Per, 2))
                        gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_KG, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                        gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.SNF_KG, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                        gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_Rate, 2))
                        gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.SNF_Rate, 2))
                    Else
                        gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(obj.fat_per)
                        gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(obj.snf_Per)
                        gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(obj.fat_KG, False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                        gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(obj.SNF_KG, False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                        gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(obj.fat_Rate)
                        gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(obj.SNF_Rate)
                    End If
                    gvItem.Rows(0).Cells(colAmt).Value = clsCommon.myFormat(obj.Amount)
                    gvItem.Rows(0).Cells(colDeducRate).Value = clsCommon.myFormat(obj.Deduction)
                    gvItem.Rows(0).Cells(colIncenRate).Value = clsCommon.myFormat(obj.Incentive)
                    ''richa Against Ticket No.BM00000003719 on 04/09/2014
                    gvItem.Rows(0).Cells(colSpecialDeduction).Value = clsCommon.myFormat(obj.SpecialDeduction)
                    gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat(obj.Actual_Amount)
                    gvItem.Rows(0).Cells(colStandardRate).Value = clsCommon.myFormat(obj.Standardrate)
                    gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat(obj.NetRate)
                    gvItem.Rows(0).Cells(colMilkRate).Value = clsCommon.myFormat(obj.BasicRate)

                    gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat(obj.FatAmt)
                    gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat(obj.SnfAmt)
                    gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat(obj.FinalMilkRate)
                    gvItem.Rows(0).Cells(colNetWeightLTR).Value = clsCommon.myFormat(obj.Net_Weight_LTR)
                    gvItem.Rows(0).Cells(colMilkAmount).Value = clsCommon.myFormat(obj.Milk_Amount)
                    gvItem.Rows(0).Cells(colTranspoterCharge).Value = clsCommon.myFormat(obj.Transport_Charges)
                    loadBlankParameterGrid()
                    If objQ.arrQcParam IsNot Nothing Then
                        For i As Integer = 0 To objQ.arrQcParam.Count - 1
                            Try
                                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, fatColName) = CompairStringResult.Equal OrElse clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, snfColName) = CompairStringResult.Equal Then
                                    If obj.isPosted = 0 Then
                                        gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value, 2))
                                    Else
                                        gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value)
                                    End If

                                End If
                            Catch ex1 As Exception
                            End Try
                        Next
                    End If

                    loadBlankParameterGridwithRange()
                    If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                        For j As Integer = 0 To obj.arrObj.Count - 1
                            gvRange.Rows.AddNew()
                            gvRange.Rows(j).Cells("Code").Value = obj.arrObj(j).Parameter
                            gvRange.Rows(j).Cells("LowerRange").Value = clsCommon.myFormat(obj.arrObj(j).Lower_Range)
                            gvRange.Rows(j).Cells("UpperRange").Value = clsCommon.myFormat(obj.arrObj(j).Upper_Range)
                            gvRange.Rows(j).Cells("Value").Value = obj.arrObj(j).value
                            gvRange.Rows(j).Cells("QcValue").Value = obj.arrObj(j).QCValue
                            gvRange.Rows(j).Cells("IncentiveDeduction").Value = clsCommon.myFormat(obj.arrObj(j).Incen_Deduc)
                        Next
                    Else
                        Dim whrCls As String = String.Empty
                        If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
                            whrCls = " and Param_for='MCC' or Param_for='BOTH'"
                        Else
                            whrCls = " and Param_for='PLANT' or Param_for='BOTH'"
                        End If
                        Dim strlocation As String = ""
                        If chkJobWork.Checked Then
                            strlocation = txtSubLocation.Value
                        Else
                            strlocation = txtLocation.Text
                        End If
                        Dim paramName As String = String.Empty
                        Dim qry1 As String = " select code from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
                        Dim qry2 As String = String.Empty
                        Dim qry3 As String = String.Empty
                        Dim paramValue As Double = 0
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For i As Integer = 0 To dt1.Rows.Count - 1
                                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                                paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3))
                                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range, '" & paramValue & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & strlocation & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "
                                If i <> dt1.Rows.Count - 1 Then
                                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                                End If
                            Next

                            qry2 = " select * from ( " & qry2 & " ) yyy"
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                                For j As Integer = 0 To dt2.Rows.Count - 1
                                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                                    gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                                Next
                            End If
                        End If

                        qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
                        qry2 = ""
                        Dim paramValue1 As String = ""
                        dt1 = clsDBFuncationality.GetDataTable(qry1)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For i As Integer = 0 To dt1.Rows.Count - 1
                                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                                paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value , '" & paramValue1 & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end)  and loc_code='" & strlocation & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
                                If i <> dt1.Rows.Count - 1 Then
                                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                                End If
                            Next

                            qry2 = " select * from ( " & qry2 & " ) yyy"
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                                For j As Integer = 0 To dt2.Rows.Count - 1
                                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                                    gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                                Next
                            End If
                        End If


                        qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
                        qry2 = ""
                        paramValue1 = ""
                        dt1 = clsDBFuncationality.GetDataTable(qry1)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For i As Integer = 0 To dt1.Rows.Count - 1
                                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                                paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status , '" & paramValue1 & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end) and loc_code='" & strlocation & "'   and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "
                                If i <> dt1.Rows.Count - 1 Then
                                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                                End If
                            Next

                            qry2 = " select * from ( " & qry2 & " ) yyy"
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                                For j As Integer = 0 To dt2.Rows.Count - 1
                                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                                    gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                                Next
                            End If
                        End If
                    End If
                End If

                'Else

                '    If TankerFromMaster = 1 Then ERO/31/07/19-000974
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    gvItem.Rows.Clear()
                    For Each objTr As clsBulkMilkSRNChemberNoDetails In obj.Arr
                        gvItem.Rows.AddNew()

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        If clsCommon.myLen(objTr.Item_Code) > 0 Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "'"))
                        End If

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOMCalculated).Value = objTr.UOM_Calculate
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colGrossWeight).Value = objTr.Gross_Weight
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTareWeight).Value = objTr.Tare_Weight
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetWeight).Value = objTr.Net_Weight
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetWeightCalculated).Value = objTr.Net_Weight_Calculate
                        If obj.isPosted = 0 Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.fat_per, 2))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.snf_Per, 2))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.fat_KG, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.SNF_KG, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.fat_Rate, 2))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.SNF_Rate, 2))
                        Else
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objTr.fat_per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objTr.snf_Per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat(objTr.fat_KG, False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat(objTr.SNF_KG, False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(objTr.fat_Rate)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(objTr.SNF_Rate)
                        End If
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colAmt).Value = objTr.Amount
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSpecialDeduction).Value = objTr.SpecialDeduction
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDeducRate).Value = objTr.Deduction
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIncenRate).Value = objTr.Incentive
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = objTr.Actual_Amount
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkRate).Value = objTr.BasicRate
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colStandardRate).Value = objTr.Standardrate
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetRate).Value = objTr.NetRate
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatAmt).Value = objTr.FatAmt
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFAmt).Value = objTr.SnfAmt
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFinalMilkRate).Value = objTr.FinalMilkRate
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colpriceCode).Value = objTr.Price_Code
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkGradeCode).Value = objTr.MILK_GRADE_CODE
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkTtypeCode).Value = objTr.MIKL_TYPE_CODE
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatQTy).Value = objTr.fat_Qty
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFQty).Value = objTr.SNF_Qty
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTotalStdQTy).Value = objTr.TotalStandardQty
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIncentiveAmt).Value = objTr.Incentive_Amt
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDeductionAmt).Value = objTr.Deduction_Amt
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colCLR).Value = clsCommon.myFormat(MyMath.RoundDown(obj.CLR_Per, 2))
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetWeightLTR).Value = objTr.Net_Weight_LTR
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTranspoterCharge).Value = objTr.Transport_Charges
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkAmount).Value = objTr.Milk_Amount
                    Next

                    loadBlankParameterGrid()
                    If objQ.arrQcParam IsNot Nothing Then
                        For i As Integer = 0 To objQ.arrQcParam.Count - 1
                            Try
                                gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                                If clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, fatColName) = CompairStringResult.Equal OrElse clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, snfColName) = CompairStringResult.Equal Then
                                    If obj.isPosted = 0 Then
                                        gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value, 2))
                                    Else
                                        gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value)
                                    End If

                                End If
                            Catch ex1 As Exception
                            End Try
                        Next
                    End If
                End If
                'End If

                btnSave.Text = "Update"
                Dim qry As String = " select isnull(srn_return_no,'')  as srn_return from tspl_bulk_milk_srn where srn_no='" & fndSRNNo.Value & "'"
                If clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry), "") = CompairStringResult.Equal Then
                    lblReverse.Visible = False
                Else
                    lblReverse.Visible = True
                End If
                If obj.isPosted = 1 Then
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
                btnPrint.Enabled = True
            Else
                Reset()
            End If
            '=====================if document go for approval then no post button visible or if document contain related setting
            If AllowNLevel Then
                btnPost.Visible = MyBase.isPostFlag

                If Not clsApply_Approval.Visibility_PostButtonForApproval(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(fndSRNNo.Value), clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value), 0, "", objApproval) Then
                    btnPost.Visible = False
                    If lblPending.Status = ERPTransactionStatus.Pending Then
                        lblPending.Status = clsApply_Approval.ApprovalCondCheck_Doc(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(fndSRNNo.Value), Nothing)
                    End If
                End If
            End If
            '============================================
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub loadWeighmentData(ByVal strWeighmentNo As String)
        Try
            Dim srnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select srn_no from tspl_bulk_milk_srn where weighment_no='" & strWeighmentNo & "' and isnull(srn_return_no,'')=''"))
            If clsCommon.myLen(srnNo) > 0 Then
                LoadData(srnNo, NavigatorType.Current)
                Exit Sub
            End If
            Dim objW As clsWeighment = clsWeighment.getData(strWeighmentNo, "BulkProc", NavigatorType.Current)
            Dim strQcNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_QUALITY_CHECK.qc_no from TSPL_Weighment_Detail left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.gate_entry_no=TSPL_Weighment_Detail.gate_entry_no where TSPL_Weighment_Detail.weighment_no='" & fndWeighmentNo.Value & "'"))
            Dim objQ As clsQualityCheck = clsQualityCheck.getData(strQcNo, "BulkProc", NavigatorType.Current)
            If objW IsNot Nothing AndAlso objQ IsNot Nothing Then
                'Reset()
                If TankerFromMaster = 1 Then
                    dtpTareWeight = objW.Tare_Weight_date
                End If
                txtSubLocation.Value = objW.Joblocation_Code
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                chkJobWork.Checked = IIf(objW.IsAgainstJobWork = 1, True, False)
                fndWeighmentNo.Value = objW.Weighment_No
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(objW.Weighment_Date, "dd/MM/yyyy hh:mm:ss tt")
                txtVendor.Text = objW.Vendor_Code
                lblVendorName.Text = objW.Vendor_Desc
                txtLocation.Text = objW.location_Code
                lblLocationDesc.Text = objW.Location_Desc
                txtChallanNo.Text = objW.Challan_No
                dtpChallanDate.Value = objW.Challan_Date
                fndTankerNo.Value = objW.Tanker_No

                txtDipValue.Text = objW.Dip_Value
                txtGateEntryNo.Text = objW.Gate_Entry_No
                txtQCNo.Text = objQ.QC_No
                dtpQCInTime.Value = objQ.QC_In_Date_Time
                dtpQCOutTime.Value = objQ.QC_Out_Date_Time
                txtRemarks.Text = objQ.Remarks
                fndReferenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + objW.Gate_Entry_No + "' "))
                loadBlankItemGrid()
                If TankerFromMaster = 0 Then
                    If True Then
                        loadBlankParameterGrid()
                        gvItem.Rows(0).Cells(colItemCode).Value = objW.Item_Code
                        gvItem.Rows(0).Cells(colItemDesc).Value = objW.Item_Desc
                        If clsCommon.myLen(objW.Item_Code) > 0 Then
                            gvItem.Rows(0).Cells(colItemHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where Item_Code ='" + objW.Item_Code + "'"))
                        End If
                        gvItem.Rows(0).Cells(colUOM).Value = objW.UOM
                        gvItem.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Gross_Weight, 0))
                        gvItem.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Tare_Weight, 0))
                        gvItem.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Net_Weight, 0))
                        'gvItem.Rows(0).Cells(colFat).Value = objW.fat_per
                        'gvItem.Rows(0).Cells(colSNF).Value = objW.snf_Per
                        'gvItem.Rows(0).Cells(colFatKG).Value = objW.Net_Weight * objW.fat_per / 100
                        'gvItem.Rows(0).Cells(colSNFKG).Value = objW.Net_Weight * objW.snf_Per / 100

                        If objQ.arrQcParam IsNot Nothing Then
                            For i As Integer = 0 To objQ.arrQcParam.Count - 1
                                Try
                                    gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                                    If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                                        If RunBulkProcOnAdjustFATCLR = 0 Then
                                            gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colSNF).Value)
                                        Else
                                            Dim AdjustedSNF As Decimal = 0
                                            AdjustedSNF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT ISNULL(Adjusted_SNF,0) as Adjusted_SNF  FROM TSPL_QUALITY_CHECK where 1=1 AND QC_No='" & objQ.QC_No & "'"))
                                            gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(AdjustedSNF, 2))
                                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colSNF).Value)
                                        End If
                                    End If
                                    If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                                        If RunBulkProcOnAdjustFATCLR = 0 Then
                                            gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colFat).Value)
                                        Else
                                            Dim AdjustedFAT As Decimal = 0
                                            AdjustedFAT = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT ISNULL(Adjusted_FAT,0) AS Adjusted_FAT  FROM TSPL_QUALITY_CHECK where 1=1 AND QC_No='" & objQ.QC_No & "'"))
                                            gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(AdjustedFAT, 2))
                                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colFat).Value)
                                        End If
                                    End If
                                    If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "CLR") = CompairStringResult.Equal Then
                                        If RunBulkProcOnAdjustFATCLR = 0 Then
                                            gvItem.Rows(0).Cells(colCLR).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colCLR).Value)
                                        Else
                                            Dim AdjustedCLR As Decimal = 0
                                            AdjustedCLR = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT ISNULL(Adjusted_CLR,0) AS Adjusted_CLR  FROM TSPL_QUALITY_CHECK where 1=1 AND QC_No='" & objQ.QC_No & "'"))
                                            gvItem.Rows(0).Cells(colCLR).Value = clsCommon.myFormat(MyMath.RoundDown(AdjustedCLR, 2))
                                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colCLR).Value)
                                        End If
                                    End If
                                Catch exxx As Exception
                                End Try
                            Next
                        End If

                        Try
                            gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Net_Weight * MyMath.RoundDown(gvParam.Rows(0).Cells(fatColName).Value, 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                            gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Net_Weight * MyMath.RoundDown(gvParam.Rows(0).Cells(snfColName).Value, 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                        Catch ex4 As Exception
                        End Try

                        ''richa Against Ticket no .BM00000003725 on 05/08/2014
                        loadBlankParameterGridwithRange()
                        Dim whrCls As String = String.Empty
                        If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
                            whrCls = " and Param_for='MCC' or Param_for='BOTH'"
                        Else
                            whrCls = " and (Param_for='PLANT' or Param_for='BOTH')"
                        End If
                        Dim strlocation As String = ""
                        If chkJobWork.Checked Then
                            strlocation = txtSubLocation.Value
                        Else
                            strlocation = txtLocation.Text
                        End If
                        Dim paramName As String = String.Empty
                        Dim qry1 As String = " select code,Description from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
                        Dim qry2 As String = String.Empty
                        Dim qry3 As String = String.Empty
                        Dim paramValue As Double = 0
                        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For i As Integer = 0 To dt1.Rows.Count - 1
                                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                                paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3))
                                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end) and loc_code='" & strlocation & "'    and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "
                                If i <> dt1.Rows.Count - 1 Then
                                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                                End If
                            Next

                            qry2 = " select * from ( " & qry2 & " ) yyy"
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                                For j As Integer = 0 To dt2.Rows.Count - 1
                                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                                    gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                                Next
                            End If
                        End If

                        qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
                        qry2 = ""
                        Dim paramValue1 As String = ""
                        dt1 = clsDBFuncationality.GetDataTable(qry1)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For i As Integer = 0 To dt1.Rows.Count - 1
                                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                                paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end) and loc_code='" & strlocation & "'   and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "' order by Effective_Date desc  "

                                If i <> dt1.Rows.Count - 1 Then
                                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                                End If
                            Next

                            qry2 = " select * from ( " & qry2 & " ) yyy"
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                                For j As Integer = 0 To dt2.Rows.Count - 1
                                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                                    gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                                Next
                            End If
                        End If


                        qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
                        qry2 = ""
                        paramValue1 = ""
                        dt1 = clsDBFuncationality.GetDataTable(qry1)
                        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                            For i As Integer = 0 To dt1.Rows.Count - 1
                                qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                                paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                                qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end) and loc_code='" & strlocation & "'  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "

                                If i <> dt1.Rows.Count - 1 Then
                                    qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                                End If
                            Next

                            qry2 = " select * from ( " & qry2 & " ) yyy"
                            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                            If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                                For j As Integer = 0 To dt2.Rows.Count - 1
                                    paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                                    gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                                Next
                            End If
                        End If
                        ''=====================================================

                        'gvItem.Rows(0).Cells(colDeducRate).Value = getDeduction()
                        'gvItem.Rows(0).Cells(colIncenRate).Value = getIncentive()

                        '''''richa Against Ticket No.BM00000003719 on 04/09/2014
                        gvItem.Rows(0).Cells(colIncenRate).Value = clsCommon.myFormat(getIncentivePerUnit())
                        gvItem.Rows(0).Cells(colDeducRate).Value = clsCommon.myFormat(getDeductionPerUnit(clsCommon.myCstr(gvItem.Rows(0).Cells(colpriceCode).Value), clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value), clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNF).Value)))
                        gvItem.Rows(0).Cells(colSpecialDeduction).Value = clsCommon.myFormat(objQ.DeductionAmount)
                        gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat(clsCommon.myCdbl(txtStanadardrate.Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value))
                        gvItem.Rows(0).Cells(colNetRate).EndEdit()
                        gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
                        ''=======================================================
                        Dim strQry As String = ""
                        Dim ZeroRate As String = ""
                        If BulkProcPriceChartStandardRateWithZero = 1 Then
                            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select Gate_Entry_Type,Doc_Type,location_code  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + txtGateEntryNo.Text + "'", Nothing)
                            If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                                Throw New Exception("Gate Entry No" + txtGateEntryNo.Text + " Not found ")
                            End If
                            Dim isBulkPro As Boolean = clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Doc_Type")), "BulkProc") = CompairStringResult.Equal
                            If isBulkPro Then
                                Dim strGateEntryType As String = clsCommon.myCstr(dtTemp.Rows(0)("Gate_Entry_Type"))
                                If clsCommon.myLen(strGateEntryType) <= 0 Then
                                    Throw New Exception("Gate Entry Type not found on Gate Entry screen")
                                Else
                                    If clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                                        ZeroRate = " and Standard_Rate = 0 "
                                    End If
                                End If
                            End If
                        End If
                        strQry = " select  * from TSPL_Bulk_Price_MASTER  where   TSPL_Bulk_Price_MASTER.Price_Code in (select PriceCode  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & txtVendor.Text & "' " & ZeroRate & " and isDefault=1 ) "
                        Dim FatW As Double = 0
                        Dim SNfW As Double = 0
                        Dim FATRatio As Double = 0
                        Dim SNFRatio As Double = 0
                        Dim StdRate As Double = 0
                        Dim fatKG As Double = 0
                        Dim snfKG As Double = 0
                        Dim totalSolidRate As Double = 0
                        Dim dt As DataTable
                        dt = clsDBFuncationality.GetDataTable(strQry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            fndPriceChart.Value = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                            FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                            SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                            FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                            SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                            StdRate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                            totalSolidRate = clsCommon.myCdbl(dt.Rows(0)("Total_Solid_Rate"))
                            TxtFatWeightage.Text = FatW
                            TxtSNFWeightage.Text = SNfW
                            txtfatPercentage.Text = FATRatio
                            txtSNFPercentage.Text = SNFRatio
                            txtStanadardrate.Text = clsCommon.myFormat(StdRate)
                            txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                            txtTotalSolidRate.Value = totalSolidRate
                            'Tieckt No : ERO/15/01/19-000469 by prabhakar
                            txtUOM.Value = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                        Else
                            TxtFatWeightage.Text = "0"
                            TxtSNFWeightage.Text = "0"
                            txtfatPercentage.Text = "0"
                            txtSNFPercentage.Text = "0"
                            txtStanadardrate.Text = "0"
                            txtTolerance.Value = "0"
                            txtTotalSolidRate.Value = "0"
                            fndTankerNo.Enabled = True
                        End If
                        OpenPriceChart(True)
                    End If
                Else
                    If True Then
                        If objW.Arr IsNot Nothing AndAlso objW.Arr.Count > 0 Then
                            isInsideLoadData = True
                            For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
                                gvItem.Rows.AddNew()
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                                If clsCommon.myLen(objTr.Item_Code) > 0 Then
                                    gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "'"))
                                End If

                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colGrossWeight).Value = clsCommon.myFormat(objTr.Gross_Weight)
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTareWeight).Value = clsCommon.myFormat(objTr.Tare_Weight)
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetWeight).Value = clsCommon.myFormat(objTr.Net_Weight)
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIncenRate).Value = clsCommon.myFormat(getIncentivePerUnit())
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDeducRate).Value = clsCommon.myFormat(getDeductionPerUnit(clsCommon.myCstr(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colpriceCode).Value), clsCommon.myCstr(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value), clsCommon.myCdbl(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value)))
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSpecialDeduction).Value = clsCommon.myFormat(objQ.DeductionAmount)
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetRate).Value = clsCommon.myFormat(clsCommon.myCdbl(txtStanadardrate.Value) + clsCommon.myCdbl(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colIncenRate).Value) + clsCommon.myCdbl(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSpecialDeduction).Value))
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkGradeCode).Value = ""
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colMilkTtypeCode).Value = ""
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetRate).EndEdit()
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colActAmt).Value = clsCommon.myFormat("0")
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                                gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
                            Next

                            If objQ.arrQcParam IsNot Nothing Then
                                loadBlankParameterGrid()
                                For i As Integer = 0 To objQ.arrQcParam.Count - 1
                                    Try
                                        gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                                        If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                                            '======Sanjeet(Check For Adjusted FAT & SNF)================
                                            If RunBulkProcOnAdjustFATCLR = 0 Then
                                                gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                                                gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNF).Value)
                                            Else
                                                Dim AdjustedSNF As Decimal = 0
                                                Dim strqry As String = "select ISNULL(Adjusted_SNF,0) AS Adjusted_SNF from TSPL_Quality_Chember_Details where 1=1 AND QC_No='" & objQ.QC_No & "' AND Line_No=" & objQ.arrQcParam(i).LINE_NO & " "
                                                AdjustedSNF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strqry))
                                                gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(AdjustedSNF, 2))
                                                gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colSNF).Value)
                                            End If
                                            '========================================================
                                        End If
                                        If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                                            '======Sanjeet(Check For Adjusted FAT & SNF)================
                                            If RunBulkProcOnAdjustFATCLR = 0 Then
                                                gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                                                gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFat).Value)
                                            Else
                                                Dim AdjustedFAT As Decimal = 0
                                                AdjustedFAT = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select ISNULL(Adjusted_FAT,0) AS Adjusted_FAT from TSPL_Quality_Chember_Details where 1=1 AND QC_No='" & objQ.QC_No & "' AND Line_No=" & objQ.arrQcParam(i).LINE_NO & " "))
                                                gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(AdjustedFAT, 2))
                                                gvParam.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(objQ.arrQcParam(i).LINE_NO - 1).Cells(colFat).Value)
                                            End If
                                        End If
                                    Catch exxx As Exception
                                    End Try
                                Next
                            End If

                            Try
                                If objW.Arr IsNot Nothing Then
                                    For ii As Integer = 0 To objW.Arr.Count - 1
                                        gvItem.Rows(ii).Cells(colMilkTtypeCode).Value = clsCommon.myCstr(objQ.Arr(ii).MIKL_TYPE_CODE)
                                        gvItem.Rows(ii).Cells(colMilkGradeCode).Value = clsCommon.myCstr(objQ.Arr(ii).MILK_GRADE_CODE)
                                        SetParameterRange(ii)
                                        gvItem.Rows(ii).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Arr(ii).Net_Weight * MyMath.RoundDown(gvParam.Rows(ii).Cells(fatColName).Value, 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                                        gvItem.Rows(ii).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Arr(ii).Net_Weight * MyMath.RoundDown(gvParam.Rows(ii).Cells(snfColName).Value, 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                                        OpenPriceChartItemWise(False, ii)
                                        Dim intPriceType As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsPrice_GradeWise from TSPL_Bulk_Price_MASTER where Price_Code='" & gvItem.Rows(ii).Cells(colpriceCode).Value & "'"))
                                        If intPriceType = 0 Then
                                            gvItem.Rows(ii).Cells(colIncenRate).Value = clsCommon.myFormat(getIncentivePerUnit())
                                            gvItem.Rows(ii).Cells(colDeducRate).Value = clsCommon.myFormat(getDeductionPerUnit(clsCommon.myCstr(gvItem.Rows(ii).Cells(colpriceCode).Value), clsCommon.myCstr(gvItem.Rows(ii).Cells(colItemCode).Value), clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNF).Value)))
                                            gvItem.Rows(ii).Cells(colNetRate).Value = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetRate).Value)
                                            OpenPriceChartItemWise(False, ii)
                                        End If
                                    Next
                                End If
                                isInsideLoadData = False
                            Catch ex4 As Exception
                                clsCommon.MyMessageBoxShow(Me, ex4.Message, Me.Text)
                            End Try
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function getDeductionPerUnit(ByVal PriceCode As String, ByVal ItemCode As String, ByVal SNFPer As Decimal) As Double
        Dim rValue As Double = 0
        For i As Integer = 0 To gvRange.Rows.Count - 1
            If clsCommon.myLen(gvRange.Rows(i).Cells("Code").Value) > 0 And clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value) < 0 Then
                rValue = rValue + clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value)
            End If
        Next
        Dim qry = "select ABS(TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION.Ded_Amount) as Ded_Amount from TSPL_BULK_PRICE_DETAIL_ITEM_WISE " +
        " left outer join  TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION on TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION.SNo=TSPL_BULK_PRICE_DETAIL_ITEM_WISE.line_no and TSPL_BULK_PRICE_DETAIL_ITEM_WISE_SNF_DEDUCTION.Price_Code=TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Price_Code" +
        " where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Price_Code='" + PriceCode + "' and TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" + ItemCode + "' and SNF_Per='" + clsCommon.myCstr(Math.Round(SNFPer, 1)) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            rValue += clsCommon.myCdbl(dt.Rows(0)("Ded_Amount"))
        End If
        Return rValue
    End Function

    Function getIncentivePerUnit() As Double
        Dim rValue As Double = 0
        For i As Integer = 0 To gvRange.Rows.Count - 1
            If clsCommon.myLen(gvRange.Rows(i).Cells("Code").Value) > 0 And clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value) > 0 Then
                rValue = rValue + clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value)
            End If
        Next
        Return rValue
    End Function

    Function getDeduction(ByVal paramCode As String, ByVal value As Double, ByVal docDate As Date) As Double
        Dim strlocation As String = ""
        If chkJobWork.Checked Then
            strlocation = txtSubLocation.Value
        Else
            strlocation = txtLocation.Text
        End If
        Dim strQry As String = "select top 1  coalesce(Value,0.0) as value   from TSPL_PARAMETER_RANGE_MASTER where Code='" & paramCode & "' and " & value & ">=Lower_range  and " & value & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "' then 2 else 3 end) end)   and coalesce(Value,0.0)<0.0   and loc_code='" & strlocation & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
        Dim deducValue As Double = 0
        deducValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return deducValue
    End Function

    Function getDeduction() As Double
        Dim totDeducValue As Double = 0
        For i As Integer = 1 To gvParam.Columns.Count - 1
            totDeducValue = totDeducValue + getDeduction(gvParam.Columns(i).Name, clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value), dtpSRNDATE.Value)
        Next
        Return totDeducValue
    End Function

    Function getIncentive(ByVal paramCode As String, ByVal value As Double, ByVal docDate As Date) As Double
        Dim strlocation As String = ""
        If chkJobWork.Checked Then
            strlocation = txtSubLocation.Value
        Else
            strlocation = txtLocation.Text
        End If
        Dim strQry As String = "select top 1  coalesce(Value,0.0) as value   from TSPL_PARAMETER_RANGE_MASTER where Code='" & paramCode & "' and " & value & ">=Lower_range  and " & value & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "' then 2 else 3 end) end)  and coalesce(Value,0.0)>0.0   and loc_code='" & strlocation & "'  and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
        Dim IncenValue As Double = 0
        IncenValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return IncenValue
    End Function

    Function getIncentive() As Double
        Dim totIncenValue As Double = 0
        For i As Integer = 1 To gvParam.Columns.Count - 1
            totIncenValue = totIncenValue + getIncentive(gvParam.Columns(i).Name, clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value), dtpSRNDATE.Value)
        Next
        Return totIncenValue
    End Function

    Private Sub fndWeighmentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndWeighmentNo._MYValidating

    End Sub

    Private Sub fndPriceChart__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPriceChart._MYValidating
        Try
            Dim whrcls As String = String.Empty
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

            isDocPosted = False
            Dim ZeroRate As String = ""
            If BulkProcPriceChartStandardRateWithZero = 1 Then
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select Gate_Entry_Type,Doc_Type,location_code  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + txtGateEntryNo.Text + "'", Nothing)
                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                    Throw New Exception("Gate Entry No" + txtGateEntryNo.Text + " Not found ")
                End If
                Dim isBulkPro As Boolean = clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Doc_Type")), "BulkProc") = CompairStringResult.Equal
                If isBulkPro Then
                    Dim strGateEntryType As String = clsCommon.myCstr(dtTemp.Rows(0)("Gate_Entry_Type"))
                    If clsCommon.myLen(strGateEntryType) <= 0 Then
                        Throw New Exception("Gate Entry Type not found on Gate Entry screen")
                    Else
                        If clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                            ZeroRate = " and Standard_Rate = 0 "
                        End If
                    End If
                End If
            End If
            If clsCommon.myLen(txtVendor.Text) > 0 Then
                whrcls = " TSPL_Bulk_Price_MASTER.Price_Code in (select PriceCode  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & txtVendor.Text & "') "
                If BulkProcPricePostedData = True Then
                    whrcls += " and Posted='1'"
                End If
                whrcls += ZeroRate
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select a weighment no ", Me.Text)
                Exit Sub
            End If

            fndPriceChart.Value = clsPriceChartBulkProc.getFinder(whrcls, fndPriceChart.Value, isButtonClicked)
            Dim strQry As String = String.Empty
            Dim dt As DataTable
            If clsCommon.myLen(fndPriceChart.Value) > 0 Then
                strQry = " select  * from TSPL_Bulk_Price_MASTER  where Price_Code='" & fndPriceChart.Value & "' "

                dt = clsDBFuncationality.GetDataTable(strQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                    SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                    FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                    SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                    StdRate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                    TxtFatWeightage.Text = FatW
                    TxtSNFWeightage.Text = SNfW
                    txtfatPercentage.Text = FATRatio
                    txtSNFPercentage.Text = SNFRatio
                    txtStanadardrate.Text = StdRate
                    txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                    txtStanadardrate.Text = StdRate
                    txtTotalSolidRate.Value = clsCommon.myCdbl(dt.Rows(0)("Total_Solid_Rate"))
                    txtUOM.Value = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                Else
                    TxtFatWeightage.Text = "0"
                    TxtSNFWeightage.Text = "0"
                    txtfatPercentage.Text = "0"
                    txtSNFPercentage.Text = "0"
                    txtStanadardrate.Text = "0"
                    txtTolerance.Value = "0"
                    fndTankerNo.Enabled = True
                    txtTotalSolidRate.Value = 0
                    txtUOM.Value = ""
                End If
                ApplyUOM()
            End If
            OpenPriceChart(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenPriceChart(ByVal IsPriceChartSelected As Boolean)
        If Not isDocPosted Then
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
            Dim strQry As String = String.Empty
            Dim whrcls As String = String.Empty
            If clsCommon.myLen(fndPriceChart.Value) > 0 Then
                ''richa agarwal 07 Feb,2020 ERO/23/01/20-001178
                Dim objQ As clsQualityCheck = clsQualityCheck.getData(txtQCNo.Text, "BulkProc", NavigatorType.Current)
                If objQ.arrQcParam IsNot Nothing Then
                    For i As Integer = 0 To objQ.arrQcParam.Count - 1
                        Try
                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                            If clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, fatColName) = CompairStringResult.Equal OrElse clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, snfColName) = CompairStringResult.Equal Then
                                If obj.isPosted = 0 Then
                                    gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value, 2))
                                Else
                                    gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value)
                                End If

                            End If
                        Catch ex1 As Exception
                        End Try
                    Next
                End If
                If SettBulkProcurementApplyTotalSoidRate OrElse SettCalculateLtrQtyFromKGQtyByCLR Then
                    If clsCommon.myLen(gvItem.Rows(0).Cells(colItemCode).Value) > 0 Then
                        Dim CalQty As Decimal = clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeight).Value)
                        Dim dblNetWeightLtr As Decimal = clsItemMaster.GetQtyInLtrFromKgByCLR(CalQty, clsCommon.myCdbl(gvItem.Rows(0).Cells(colCLR).Value))
                        'ERO/07/01/19-000458,ERO/07/01/19-000456 by balwinder on 08/01/2018
                        If SettCalculateLtrQtyFromKGQtyByCLR _
                             AndAlso clsCommon.CompairString("KG", clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value)) = CompairStringResult.Equal _
                             AndAlso clsCommon.CompairString("LTR", txtUOM.Value) = CompairStringResult.Equal Then
                            CalQty = clsItemMaster.GetQtyInLtrFromKgByCLR(CalQty, clsCommon.myCdbl(gvItem.Rows(0).Cells(colCLR).Value))
                        Else
                            CalQty = clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeight).Value) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value), clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value), Nothing)
                            Dim convFact As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value), txtUOM.Value, Nothing)
                            If convFact = 0 Then
                                Throw New Exception("Unit [" + txtUOM.Value + "] is not for item [" + clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value) + "]")
                            End If
                            CalQty = CalQty / convFact
                        End If
                        gvItem.Rows(0).Cells(colNetWeightCalculated).Value = clsCommon.myFormat(Math.Round(CalQty, 3), False, True, False, 3, True)
                        gvItem.Rows(0).Cells(colNetWeightLTR).Value = clsCommon.myFormat(Math.Round(dblNetWeightLtr, 3), False, True, False, 3, True)
                        gvItem.Rows(0).Cells(colFatKG).Value = Math.Round(CalQty * Math.Round(clsCommon.myCdbl(gvParam.Rows(0).Cells(fatColName).Value), 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces)
                        gvItem.Rows(0).Cells(colSNFKG).Value = Math.Round(CalQty * Math.Round(clsCommon.myCdbl(gvParam.Rows(0).Cells(snfColName).Value), 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces)

                        fatKG = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value), 3), False, True, False, 3, True)
                        snfKG = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value), 3), False, True, False, 3, True)

                        FATRate = txtTotalSolidRate.Value
                        SNFRate = txtTotalSolidRate.Value

                        FATValue = Math.Round(fatKG * FATRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                        SNfValue = Math.Round(snfKG * SNFRate, SettBulkMilkFATSNFAmtDecimalPlaces)

                        gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(Math.Round(FATRate, 2))
                        gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(Math.Round(SNFRate, 2))
                        gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat(FATValue)
                        gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat(SNfValue)
                        gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 2))
                        If AllowTruncateAmount Then
                            Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                            If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                            End If
                            gvItem.Rows(0).Cells(colActAmt).Value = CInt(xNewAmt)
                        End If
                        gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeightCalculated).Value), 2))
                        'Dim isTranspoterChargeAddInActAmount As Boolean = True
                        If ApplyTransportChargeAddInActualAmount Then
                            Dim dblActamt As Double = gvItem.Rows(0).Cells(colActAmt).Value
                            gvItem.Rows(0).Cells(colActAmt).Value = dblActamt + txtTransportCharges.Value
                            gvItem.Rows(0).Cells(colTranspoterCharge).Value = txtTransportCharges.Value
                            gvItem.Rows(0).Cells(colMilkAmount).Value = dblActamt
                        Else
                            gvItem.Rows(0).Cells(colFinalMilkRate).Value += txtTransportCharges.Value
                        End If



                        ''richa recalculate fatrate and snfrate,fatamt and snf amt  when Transportation charges is greater than 0 as discussed it with Ranjana Mam ERO/31/07/19-000974
                        If clsCommon.myCdbl(txtTransportCharges.Text) > 0 AndAlso ApplyTransportChargeAddInActualAmount = False Then
                            gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colFinalMilkRate).Value) * clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeightCalculated).Value), 2))
                            FATValue = Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value) * fatKG / (fatKG + snfKG), SettBulkMilkFATSNFAmtDecimalPlaces)
                            SNfValue = Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value) - FATValue, SettBulkMilkFATSNFAmtDecimalPlaces)
                            gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat(Math.Round(FATValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                            gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat(Math.Round(SNfValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                            FATRate = FATValue / fatKG
                            SNFRate = SNfValue / snfKG

                            gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(Math.Round(FATRate, 2))
                            gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(Math.Round(SNFRate, 2))
                        End If

                        fndTankerNo.Enabled = False
                    End If
                ElseIf SettApplyCalculateWeightInLtr Then
                    Dim CalQty As Decimal = clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeight).Value)
                    CalQty = clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeight).Value) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value), clsCommon.myCstr(gvItem.Rows(0).Cells(colUOM).Value), Nothing)
                    Dim convFact As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value), txtUOM.Value, Nothing)
                    If convFact = 0 Then
                        Throw New Exception("Price Unit [" + txtUOM.Value + "] is not for item [" + clsCommon.myCstr(gvItem.Rows(0).Cells(colItemCode).Value) + "]")
                    End If
                    CalQty = CalQty / convFact


                    ''richa agarwal 1 Aug,2019 apply AllowTruncateAmount setting below condition
                    Dim xNewAmt As Double = 0
                    If AllowTruncateAmount Then
                        xNewAmt = CalQty ''MIL/01/03/19-000051 by balwinder on 01/03/2019
                        If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                            xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                        End If
                        CalQty = CInt(xNewAmt)
                    End If

                    gvItem.Rows(0).Cells(colFatKG).Value = Math.Round(CalQty * Math.Round(clsCommon.myCdbl(gvParam.Rows(0).Cells(fatColName).Value), 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces)
                    gvItem.Rows(0).Cells(colSNFKG).Value = Math.Round(CalQty * Math.Round(clsCommon.myCdbl(gvParam.Rows(0).Cells(snfColName).Value), 2) / 100, SettBulkMilkFATSNFKGDecimalPlaces)
                    gvItem.Rows(0).Cells(colNetWeightCalculated).Value = clsCommon.myFormat(Math.Round(CalQty, 3), False, True, False, 3, True)
                    gvItem.Rows(0).Cells(colNetWeightLTR).Value = clsCommon.myFormat(Math.Round(CalQty, 3), False, True, False, 3, True)

                    If IsPriceChartSelected Then
                        gvItem.Rows(0).Cells(colMilkRate).Value = clsCommon.myFormat(txtStanadardrate.Text)
                    End If
                    FatW = clsCommon.myCdbl(TxtFatWeightage.Text)
                    SNfW = clsCommon.myCdbl(TxtSNFWeightage.Text)
                    gvItem.Rows(0).Cells(colStandardRate).Value = txtStanadardrate.Text
                    fatKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                    snfKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                    FATRatio = clsCommon.myCdbl(txtfatPercentage.Text)
                    SNFRatio = clsCommon.myCdbl(txtSNFPercentage.Text)
                    gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(gvItem.Rows(0).Cells(colMilkRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value)))
                    gvItem.Rows(0).Cells(colNetRate).EndEdit()
                    FATRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * FatW / FATRatio, 2)
                    SNFRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * SNfW / SNFRatio, 2)
                    FATValue = MyMath.RoundDown(fatKG * FATRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                    SNfValue = MyMath.RoundDown(snfKG * SNFRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                    gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                    gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                    gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat(MyMath.RoundDown(FATValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                    gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat(MyMath.RoundDown(SNfValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                    gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                    If AllowTruncateAmount Then ''BM00000010118
                        xNewAmt = clsCommon.myCdbl(FATValue + SNfValue)
                        If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                            xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                        End If
                        gvItem.Rows(0).Cells(colActAmt).Value = CInt(xNewAmt)
                    End If
                    gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeightCalculated).Value), 2))
                    fndTankerNo.Enabled = False
                Else
                    If IsPriceChartSelected Then
                        gvItem.Rows(0).Cells(colMilkRate).Value = clsCommon.myFormat(txtStanadardrate.Text)
                    End If
                    FatW = clsCommon.myCdbl(TxtFatWeightage.Text)
                    SNfW = clsCommon.myCdbl(TxtSNFWeightage.Text)
                    gvItem.Rows(0).Cells(colStandardRate).Value = txtStanadardrate.Text
                    fatKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                    snfKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                    FATRatio = clsCommon.myCdbl(txtfatPercentage.Text)
                    SNFRatio = clsCommon.myCdbl(txtSNFPercentage.Text)
                    gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(gvItem.Rows(0).Cells(colMilkRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value)))
                    gvItem.Rows(0).Cells(colNetRate).EndEdit()
                    FATRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * FatW / FATRatio, 2)
                    SNFRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * SNfW / SNFRatio, 2)
                    FATValue = MyMath.RoundDown(fatKG * FATRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                    SNfValue = MyMath.RoundDown(snfKG * SNFRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                    gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                    gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                    gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat(MyMath.RoundDown(FATValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                    gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat(MyMath.RoundDown(SNfValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                    fndTankerNo.Enabled = False
                    gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                    If AllowTruncateAmount Then ''BM00000010118
                        Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                        If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                            xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                        End If
                        gvItem.Rows(0).Cells(colActAmt).Value = CInt(xNewAmt)
                    End If
                    gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeight).Value), 2))

                End If
            Else
                gvItem.Rows(0).Cells(colMilkRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colStandardRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
            End If
        End If
    End Sub

    Sub OpenPriceChartItemWise(ByVal IsPriceChartSelected As Boolean, ByVal intRow As Integer)
        Try
            isInsideLoadData = True
            Dim qry As String = Nothing
            If AllowManualRateonPO = 0 Then
                Dim IsItemMilkType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
                'Dim whrcls As String   
                Dim strPriceCode As String = ""
                Dim isPriceGradewise As String = ""
                Dim strMilkType As String = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colMilkTtypeCode).Value)
                Dim strItem As String = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemCode).Value)
                Dim strMilkGradeCode As String = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colMilkGradeCode).Value)
                Dim strGateEntryDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue("Select Date_And_Time from Tspl_Gate_Entry_Details where Gate_Entry_No='" & txtGateEntryNo.Text & "'"))
                Dim strMilkGradeCodeWhrCls As String = ""
                Dim intPriceType As Integer = 0
                Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

                If clsCommon.myLen(strMilkGradeCode) > 0 Then
                    strMilkGradeCodeWhrCls = " and Milk_Grade_Code='" & strMilkGradeCode & "'"
                End If
                If IsPriceChartSelected Then
                    If clsCommon.myLen(txtVendor.Text) > 0 Then
                        isPriceGradewise = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 Milk_Grade_Code from TSPL_Gate_Entry_Price_Chart where GE_Code ='" & txtGateEntryNo.Text & "' "))
                        If clsCommon.myLen(isPriceGradewise) > 0 Then
                            strPriceCode = clsDBFuncationality.getSingleValue("Select top 1 price_code from TSPL_Gate_Entry_Price_Chart where GE_Code ='" & txtGateEntryNo.Text & "' " & strMilkGradeCodeWhrCls & " ")
                        Else
                            strPriceCode = clsDBFuncationality.getSingleValue("Select top 1 price_code from TSPL_Gate_Entry_Price_Chart where GE_Code ='" & txtGateEntryNo.Text & "' ")
                        End If
                        If clsCommon.myLen(strPriceCode) > 0 Then
                            gvItem.Rows(intRow).Cells(colpriceCode).Value = strPriceCode
                        Else
                            If DateTime = "1" Then
                                ' done by priti BHA/30/05/18-000040 for bharat to map price chart o bulk srn
                                If CreateBulkMilkSRNItemwise = 0 Then
                                    strPriceCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                "where  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' " & _
                                                "and  expirydate >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' and " & _
                                                "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc")
                                Else
                                    ' Price chart Item wise based on setting for bharat.
                                    qry = "select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                "left join TSPL_BULK_PRICE_DETAIL_ITEM_WISE on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Price_Code " & _
                                                "where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" & strItem & "' and  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' " & _
                                                "and  expirydate >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' and " & _
                                                "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc,Price_Code desc"
                                    strPriceCode = clsDBFuncationality.getSingleValue(qry)
                                End If

                            Else
                                If CreateBulkMilkSRNItemwise = 0 Then
                                    strPriceCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                "where  TSPL_Bulk_Price_MASTER.Posted=1  and " & _
                                                "convert(date,effective_Date,103)<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' " & _
                                                "and  convert(date,expirydate,103) >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' and " & _
                                                "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc")
                                Else
                                    ' Price chart Item wise based on setting for bharat.
                                    qry = "select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                "left join TSPL_BULK_PRICE_DETAIL_ITEM_WISE on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Price_Code " & _
                                                "where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" & strItem & "' and  TSPL_Bulk_Price_MASTER.Posted=1 " & _
                                                "and convert(date,effective_Date,103)<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' " & _
                                                "and  convert(date,expirydate,103) >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' and " & _
                                                "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc"
                                    strPriceCode = clsDBFuncationality.getSingleValue(qry)
                                End If
                            End If


                            If clsCommon.myLen(strPriceCode) > 0 Then
                                gvItem.Rows(intRow).Cells(colpriceCode).Value = strPriceCode
                            End If
                        End If
                    End If
                Else
                    If clsCommon.myLen(txtVendor.Text) > 0 Then
                        isPriceGradewise = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select top 1 Milk_Grade_Code from TSPL_Gate_Entry_Price_Chart where GE_Code ='" & txtGateEntryNo.Text & "' "))
                        If clsCommon.myLen(isPriceGradewise) > 0 Then
                            strPriceCode = clsDBFuncationality.getSingleValue("Select top 1 price_code from TSPL_Gate_Entry_Price_Chart where GE_Code ='" & txtGateEntryNo.Text & "' " & strMilkGradeCodeWhrCls & " ")
                        Else
                            strPriceCode = clsDBFuncationality.getSingleValue("Select top 1 price_code from TSPL_Gate_Entry_Price_Chart where GE_Code ='" & txtGateEntryNo.Text & "' ")
                        End If
                        If clsCommon.myLen(strPriceCode) > 0 Then
                            gvItem.Rows(intRow).Cells(colpriceCode).Value = strPriceCode
                        Else
                            If DateTime = "1" Then
                                If clsCommon.myLen(strMilkGradeCode) > 0 Then
                                    qry = "select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                 "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                 "where  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' " & _
                                                 "and  expirydate >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' and " & _
                                                 "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "'   and  TSPL_BULK_PRICE_DETAIL.Milk_Grade_Code='" & strMilkGradeCode & "' and " & _
                                                 "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                 "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                 "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc"
                                    strPriceCode = clsDBFuncationality.getSingleValue(qry)
                                Else
                                    If CreateBulkMilkSRNItemwise = 0 Then
                                        strPriceCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                    "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                    "where  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' " & _
                                                    "and  expirydate >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' and " & _
                                                    "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                    "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                    "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                    "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc")
                                    Else
                                        ' Price chart Item wise based on setting for bharat.
                                        qry = "select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                    "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                    "left join TSPL_BULK_PRICE_DETAIL_ITEM_WISE on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Price_Code " & _
                                                    "where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" & strItem & "' and  TSPL_Bulk_Price_MASTER.Posted=1  and effective_Date<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' " & _
                                                    "and  expirydate >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy hh:mm tt") & "' and " & _
                                                    "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                    "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                    "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                    "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc,Price_Code desc"
                                        strPriceCode = clsDBFuncationality.getSingleValue(qry)
                                    End If
                                End If
                            Else
                                If clsCommon.myLen(strMilkGradeCode) > 0 Then
                                    qry = "select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                 "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                 "where  TSPL_Bulk_Price_MASTER.Posted=1  and " & _
                                                 "convert(date,effective_Date,103)<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' " & _
                                                 "and  convert(date,expirydate,103) >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' and " & _
                                                 "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "'   and  TSPL_BULK_PRICE_DETAIL.Milk_Grade_Code='" & strMilkGradeCode & "' and " & _
                                                 "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                 "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                 "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc"
                                    strPriceCode = clsDBFuncationality.getSingleValue(qry)
                                Else
                                    If CreateBulkMilkSRNItemwise = 0 Then
                                        strPriceCode = clsDBFuncationality.getSingleValue("select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                    "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                    "where  TSPL_Bulk_Price_MASTER.Posted=1  and " & _
                                                    "convert(date,effective_Date,103)<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' " & _
                                                    "and  convert(date,expirydate,103) >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' and " & _
                                                    "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                    "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                    "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                    "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc")
                                    Else
                                        ' Price chart Item wise based on setting for bharat.
                                        qry = "select top 1 TSPL_Bulk_Price_MASTER.Price_Code from  TSPL_Bulk_Price_MASTER left outer join " & _
                                                    "TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code " & _
                                                    "left join TSPL_BULK_PRICE_DETAIL_ITEM_WISE on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Price_Code " & _
                                                    "where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" & strItem & "' and  " & _
                                                    "TSPL_Bulk_Price_MASTER.Posted=1  and " & _
                                                    "convert(date,effective_Date,103)<='" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' " & _
                                                    "and  convert(date,expirydate,103) >= '" & clsCommon.GetPrintDate(strGateEntryDate, "dd/MMM/yyyy") & "' and " & _
                                                    "TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' and  " & _
                                                    "TSPL_Bulk_Price_MASTER.Price_Code in (select Tspl_Vendor_Price_Chart_mapping.PriceCode from " & _
                                                    "Tspl_Vendor_Price_Chart_mapping where TSPL_VENDOR_PRICE_CHART_MAPPING.Posted=1 and " & _
                                                    "Tspl_Vendor_Price_Chart_mapping.VendorCode='" & txtVendor.Text & "'  )  order by Price_Date desc"
                                        strPriceCode = clsDBFuncationality.getSingleValue(qry)
                                    End If
                                End If
                            End If

                            If clsCommon.myLen(strPriceCode) > 0 Then
                                gvItem.Rows(intRow).Cells(colpriceCode).Value = strPriceCode
                            End If
                        End If
                    End If
                End If


                If Not isDocPosted Then
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
                    Dim FatQty As Double = 0
                    Dim SNFQty As Double = 0
                    Dim TotalStdQty As Double = 0
                    Dim IncentiveAmt As Double = 0
                    Dim DeductionAmt As Double = 0
                    Dim strQry As String = String.Empty
                    Dim strUOMCalculated As String
                    Dim TotalSolidrate As Double = 0
                    Dim TotalSolidUOM As String = String.Empty
                    Dim PriceType As String = String.Empty
                    Dim strGateEntryJobWorkType As String = String.Empty
                    Dim dt As DataTable
                    If clsCommon.myLen(gvItem.Rows(intRow).Cells(colpriceCode).Value) > 0 Then
                        intPriceType = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsPrice_GradeWise from TSPL_Bulk_Price_MASTER where Price_Code='" & strPriceCode & "'"))
                        strGateEntryJobWorkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_Type from Tspl_Gate_Entry_Details where Gate_Entry_No ='" & txtGateEntryNo.Text & "'"))
                        If intPriceType = 1 OrElse clsCommon.CompairString(strGateEntryJobWorkType, "J") = CompairStringResult.Equal Then
                            strQry = " select  TSPL_Bulk_Price_MASTER.Total_Solid_Unit_Code,TSPL_Bulk_Price_MASTER.Fat_Weightage,TSPL_Bulk_Price_MASTER.Snf_Weightage,TSPL_Bulk_Price_MASTER.Fat_Percentage, " &
                          "TSPL_Bulk_Price_MASTER.Snf_Percentage,TSPL_Bulk_Price_MASTER.Standard_Rate,TSPL_Bulk_Price_MASTER.Tolerance, " &
                          "TSPL_BULK_PRICE_DETAIL.Fat_Weightage as DFat_Weightage,TSPL_BULK_PRICE_DETAIL.Snf_Weightage as DSnf_Weightage,TSPL_BULK_PRICE_DETAIL.Fat_Percentage as DFat_Percentage, " &
                          "TSPL_BULK_PRICE_DETAIL.Snf_Percentage as DSnf_Percentage,TSPL_BULK_PRICE_DETAIL.Standard_Rate as DStandard_Rate,TSPL_BULK_PRICE_DETAIL.Tolerance as DTolerance from TSPL_Bulk_Price_MASTER left outer join TSPL_BULK_PRICE_DETAIL on  " &
                          "TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code  where TSPL_Bulk_Price_MASTER.Price_Code='" & clsCommon.myCstr(gvItem.Rows(intRow).Cells(colpriceCode).Value) & "' and TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "' " & IIf(intPriceType = 1, strMilkGradeCodeWhrCls, "") & "   "
                        Else
                            If CreateBulkMilkSRNItemwise = 0 Then
                                strQry = " select  TSPL_Bulk_Price_MASTER.Total_Solid_Unit_Code,TSPL_Bulk_Price_MASTER.Fat_Weightage,TSPL_Bulk_Price_MASTER.Snf_Weightage,TSPL_Bulk_Price_MASTER.Fat_Percentage, " & _
                                  "TSPL_Bulk_Price_MASTER.Snf_Percentage,TSPL_Bulk_Price_MASTER.Standard_Rate,TSPL_Bulk_Price_MASTER.Tolerance, " & _
                                  "TSPL_BULK_PRICE_DETAIL.Fat_Weightage as DFat_Weightage,TSPL_BULK_PRICE_DETAIL.Snf_Weightage as DSnf_Weightage,TSPL_BULK_PRICE_DETAIL.Fat_Percentage as DFat_Percentage, " & _
                                  "TSPL_BULK_PRICE_DETAIL.Snf_Percentage as DSnf_Percentage,TSPL_BULK_PRICE_DETAIL.Standard_Rate as DStandard_Rate,TSPL_BULK_PRICE_DETAIL.Tolerance as DTolerance from TSPL_Bulk_Price_MASTER left outer join TSPL_BULK_PRICE_DETAIL on  " & _
                                  "TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_Code  where TSPL_Bulk_Price_MASTER.Price_Code='" & clsCommon.myCstr(gvItem.Rows(intRow).Cells(colpriceCode).Value) & "' and TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "'  "
                            Else
                                strQry = " select  TSPL_Bulk_Price_MASTER.Total_Solid_Unit_Code,TSPL_Bulk_Price_MASTER.Fat_Weightage,TSPL_Bulk_Price_MASTER.Snf_Weightage,TSPL_Bulk_Price_MASTER.Fat_Percentage, " & _
                                 "TSPL_Bulk_Price_MASTER.Snf_Percentage,TSPL_Bulk_Price_MASTER.Standard_Rate,TSPL_Bulk_Price_MASTER.Tolerance, " & _
                                 "tspl_bulk_price_detail_item_wise.Fat_Weightage as DFat_Weightage,tspl_bulk_price_detail_item_wise.Snf_Weightage as DSnf_Weightage,tspl_bulk_price_detail_item_wise.Fat_Percentage as DFat_Percentage, " & _
                                 "tspl_bulk_price_detail_item_wise.Snf_Percentage as DSnf_Percentage,tspl_bulk_price_detail_item_wise.Standard_Rate as DStandard_Rate,tspl_bulk_price_detail_item_wise.Tolerance as DTolerance,tspl_bulk_price_detail_item_wise.PriceType,tspl_bulk_price_detail_item_wise.TotalSolidRate,tspl_bulk_price_detail_item_wise.TotalSolidUom from TSPL_Bulk_Price_MASTER left outer join tspl_bulk_price_detail_item_wise on  " & _
                                 "TSPL_Bulk_Price_MASTER.Price_Code=tspl_bulk_price_detail_item_wise.Price_Code  where TSPL_BULK_PRICE_DETAIL_ITEM_WISE.Item_code='" & strItem & "'  and TSPL_Bulk_Price_MASTER.Price_Code='" & clsCommon.myCstr(gvItem.Rows(intRow).Cells(colpriceCode).Value) & "' and TSPL_Bulk_Price_MASTER.Milk_Type_Code='" & strMilkType & "'  "
                            End If
                        End If

                        dt = clsDBFuncationality.GetDataTable(strQry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            ''CHANGE pick values from detailtable instead of header table done by richa on 14 Sep 2020
                            If (intPriceType = 1 OrElse clsCommon.CompairString(strGateEntryJobWorkType, "J") = CompairStringResult.Equal) And CreateBulkMilkSRNItemwise = 0 Then
                                'FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                                'SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                                'FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                                'SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                                'StdRate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                                'txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                                'strUOMCalculated = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                                FatW = clsCommon.myCdbl(dt.Rows(0)("DFat_Weightage"))
                                SNfW = clsCommon.myCdbl(dt.Rows(0)("DSnf_Weightage"))
                                FATRatio = clsCommon.myCdbl(dt.Rows(0)("DFat_Percentage"))
                                SNFRatio = clsCommon.myCdbl(dt.Rows(0)("DSnf_Percentage"))
                                StdRate = clsCommon.myCdbl(dt.Rows(0)("DStandard_Rate"))
                                txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("DTolerance"))
                                strUOMCalculated = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                            Else
                                If CreateBulkMilkSRNItemwise <> 0 Then
                                    PriceType = clsCommon.myCstr(dt.Rows(0)("PriceType"))
                                End If
                                If clsCommon.CompairString(PriceType, "Total Solid") = CompairStringResult.Equal Then
                                    TotalSolidrate = clsCommon.myCdbl(dt.Rows(0)("TotalSolidRate"))
                                    TotalSolidUOM = clsCommon.myCstr(dt.Rows(0)("TotalSolidUom"))
                                    strUOMCalculated = clsCommon.myCstr(dt.Rows(0)("TotalSolidUom"))
                                Else
                                    FatW = clsCommon.myCdbl(dt.Rows(0)("DFat_Weightage"))
                                    SNfW = clsCommon.myCdbl(dt.Rows(0)("DSnf_Weightage"))
                                    FATRatio = clsCommon.myCdbl(dt.Rows(0)("DFat_Percentage"))
                                    SNFRatio = clsCommon.myCdbl(dt.Rows(0)("DSnf_Percentage"))
                                    StdRate = clsCommon.myCdbl(dt.Rows(0)("DStandard_Rate"))
                                    txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("DTolerance"))
                                    strUOMCalculated = clsCommon.myCstr(dt.Rows(0)("Total_Solid_Unit_Code"))
                                End If

                            End If
                            TxtFatWeightage.Text = FatW
                            TxtSNFWeightage.Text = SNfW
                            txtfatPercentage.Text = FATRatio
                            txtSNFPercentage.Text = SNFRatio
                            txtStanadardrate.Text = StdRate
                            txtTotalSolidRate.Value = TotalSolidrate
                            'txtUOM.Value = TotalSolidUOM

                            ''BHA/19/11/19-000937 By Balwinder on 20/11/2019

                            If clsCommon.myLen(strUOMCalculated) > 0 AndAlso SettApplyCalculateWeightInLtr Then
                                gvItem.Rows(intRow).Cells(colUOMCalculated).Value = strUOMCalculated
                                Dim CalQty As Decimal = clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetWeight).Value) * clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemCode).Value), clsCommon.myCstr(gvItem.Rows(intRow).Cells(colUOM).Value), Nothing)
                                Dim convFact As Decimal = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemCode).Value), strUOMCalculated, Nothing)
                                If convFact = 0 Then
                                    Throw New Exception("Unit [" + strUOMCalculated + "] is not for item [" + clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemCode).Value) + "]")
                                End If
                                CalQty = CalQty / convFact
                                gvItem.Rows(intRow).Cells(colNetWeightCalculated).Value = clsCommon.myFormat(Math.Round(CalQty, 3), False, True, False, 3, True)
                                gvItem.Rows(intRow).Cells(colFatKG).Value = Math.Round(CalQty * Math.Round(clsCommon.myCdbl(gvParam.Rows(intRow).Cells(fatColName).Value), SettBulkMilkFATSNFKGDecimalPlaces) / 100, SettBulkMilkFATSNFKGDecimalPlaces)
                                gvItem.Rows(intRow).Cells(colSNFKG).Value = Math.Round(CalQty * Math.Round(clsCommon.myCdbl(gvParam.Rows(intRow).Cells(snfColName).Value), SettBulkMilkFATSNFKGDecimalPlaces) / 100, SettBulkMilkFATSNFKGDecimalPlaces)
                            End If

                            gvItem.Rows(intRow).Cells(colMilkRate).Value = clsCommon.myFormat(StdRate)

                            gvItem.Rows(intRow).Cells(colStandardRate).Value = StdRate
                            fatKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colFatKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                            snfKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colSNFKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)


                            If settIncludeInceAndDedInFATSNFRate Then
                                gvItem.Rows(intRow).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(StdRate) - clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colSpecialDeduction).Value) + clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colDeducRate).Value) + clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colIncenRate).Value)))
                            Else
                                gvItem.Rows(intRow).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(StdRate) - clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colSpecialDeduction).Value)))
                            End If

                            gvItem.Rows(intRow).Cells(colNetRate).EndEdit()
                            If clsCommon.CompairString(PriceType, "Total Solid") = CompairStringResult.Equal Then
                                FATRate = txtTotalSolidRate.Value
                                SNFRate = txtTotalSolidRate.Value

                                FATValue = Math.Round(fatKG * FATRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                                SNfValue = Math.Round(snfKG * SNFRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                            Else
                                FATRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetRate).Value) * FatW / FATRatio, 2)
                                SNFRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetRate).Value) * SNfW / SNFRatio, 2)
                                FATValue = MyMath.RoundDown(fatKG * FATRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                                SNfValue = MyMath.RoundDown(snfKG * SNFRate, SettBulkMilkFATSNFAmtDecimalPlaces)
                            End If

                            FatQty = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colFatKG).Value) * FatW / FATRatio, 2)
                            SNFQty = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colSNFKG).Value) * SNfW / SNFRatio, 2)
                            TotalStdQty = MyMath.RoundDown(FatQty + SNFQty, 2)

                            If SettDeductionOnStandardQty Then
                                DeductionAmt = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colDeducRate).Value) * clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colTotalStdQTy).Value), 2)
                                IncentiveAmt = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colIncenRate).Value) * clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colTotalStdQTy).Value), 2)
                            Else
                                DeductionAmt = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colDeducRate).Value) * clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetWeight).Value), 2)
                                IncentiveAmt = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colIncenRate).Value) * clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetWeight).Value), 2)
                            End If


                            gvItem.Rows(intRow).Cells(colFatQTy).Value = clsCommon.myFormat(MyMath.RoundDown(FatQty, 2))
                            gvItem.Rows(intRow).Cells(colSNFQty).Value = clsCommon.myFormat(MyMath.RoundDown(SNFQty, 2))
                            gvItem.Rows(intRow).Cells(colTotalStdQTy).Value = clsCommon.myFormat(MyMath.RoundDown(TotalStdQty, 2))
                            gvItem.Rows(intRow).Cells(colIncentiveAmt).Value = clsCommon.myFormat(MyMath.RoundDown(IncentiveAmt, 2))
                            gvItem.Rows(intRow).Cells(colDeductionAmt).Value = clsCommon.myFormat(MyMath.RoundDown(DeductionAmt, 2))

                            gvItem.Rows(intRow).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                            gvItem.Rows(intRow).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                            gvItem.Rows(intRow).Cells(colFatAmt).Value = clsCommon.myFormat(MyMath.RoundDown(FATValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                            gvItem.Rows(intRow).Cells(colSNFAmt).Value = clsCommon.myFormat(MyMath.RoundDown(SNfValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                            fndTankerNo.Enabled = False
                            gvItem.Rows(intRow).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue + IncentiveAmt + DeductionAmt, 0))
                            If settIncludeInceAndDedInFATSNFRate Then
                                gvItem.Rows(intRow).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                            End If
                            If AllowTruncateAmount Then ''BM00000010118
                                Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue + IncentiveAmt + DeductionAmt)
                                If settIncludeInceAndDedInFATSNFRate Then
                                    xNewAmt = clsCommon.myCdbl(FATValue + SNfValue)
                                End If
                                If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                    xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                                End If
                                gvItem.Rows(intRow).Cells(colActAmt).Value = CInt(xNewAmt)
                            End If

                            If clsCommon.myLen(strUOMCalculated) > 0 AndAlso SettApplyCalculateWeightInLtr Then
                                gvItem.Rows(intRow).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetWeightCalculated).Value), 2))
                            Else
                                gvItem.Rows(intRow).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetWeight).Value), 2))
                            End If
                            gvItem.Rows(intRow).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(StdRate) + clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colSpecialDeduction).Value)))
                        End If
                    Else
                        gvItem.Rows(intRow).Cells(colMilkRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colNetRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colStandardRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colFatRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colSNFRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colActAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colFatQTy).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colSNFQty).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colTotalStdQTy).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colIncentiveAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(intRow).Cells(colDeductionAmt).Value = clsCommon.myFormat("0")
                    End If
                End If
            Else
                Dim dblRate As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select top 1 ManualRate from TSPL_Gate_Entry_Chember_Details where GE_COde='" & txtGateEntryNo.Text & "'"))

                If Not isDocPosted Then
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
                    Dim FatQty As Double = 0
                    Dim SNFQty As Double = 0
                    Dim TotalStdQty As Double = 0
                    Dim IncentiveAmt As Double = 0
                    Dim DeductionAmt As Double = 0
                    Dim strQry As String = String.Empty
                    Dim Fat As Double = 0
                    Dim SNF As Double = 0

                    FatW = 0
                    SNfW = 0
                    FATRatio = 0
                    SNFRatio = 0
                    StdRate = 0
                    FATRate = 0
                    SNFRate = 0
                    FATValue = 0
                    SNfValue = 0
                    FatQty = 0
                    SNFQty = 0
                    fatKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colFatKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                    snfKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colSNFKG).Value), SettBulkMilkFATSNFKGDecimalPlaces), False, True, False, SettBulkMilkFATSNFKGDecimalPlaces, True)
                    Fat = clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colFat).Value)
                    SNF = clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colSNF).Value)
                    Dim TotalSolid As Double = Fat + SNF
                    Dim Basicrate As Double = (dblRate * TotalSolid) / 100
                    gvItem.Rows(intRow).Cells(colStandardRate).Value = Basicrate
                    gvItem.Rows(intRow).Cells(colMilkRate).Value = Basicrate
                    gvItem.Rows(intRow).Cells(colNetRate).Value = Basicrate
                    gvItem.Rows(intRow).Cells(colNetRate).EndEdit()
                    TotalStdQty = MyMath.RoundDown(FatQty + SNFQty, 2)
                    IncentiveAmt = 0
                    DeductionAmt = 0
                    gvItem.Rows(intRow).Cells(colFatQTy).Value = clsCommon.myFormat(MyMath.RoundDown(FatQty, 2))
                    gvItem.Rows(intRow).Cells(colSNFQty).Value = clsCommon.myFormat(MyMath.RoundDown(SNFQty, 2))
                    gvItem.Rows(intRow).Cells(colTotalStdQTy).Value = clsCommon.myFormat(MyMath.RoundDown(TotalStdQty, 2))
                    gvItem.Rows(intRow).Cells(colIncentiveAmt).Value = clsCommon.myFormat(MyMath.RoundDown(IncentiveAmt, 2))
                    gvItem.Rows(intRow).Cells(colDeductionAmt).Value = clsCommon.myFormat(MyMath.RoundDown(DeductionAmt, 2))
                    gvItem.Rows(intRow).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                    gvItem.Rows(intRow).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                    gvItem.Rows(intRow).Cells(colFatAmt).Value = clsCommon.myFormat(MyMath.RoundDown(FATValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                    gvItem.Rows(intRow).Cells(colSNFAmt).Value = clsCommon.myFormat(MyMath.RoundDown(SNfValue, SettBulkMilkFATSNFAmtDecimalPlaces))
                    fndTankerNo.Enabled = False
                    gvItem.Rows(intRow).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(Basicrate * clsCommon.myCdbl(gvItem.Rows(intRow).Cells(colNetWeight).Value), 0))
                    If AllowTruncateAmount Then ''BM00000010118
                        Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue + IncentiveAmt + DeductionAmt)
                        If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                            xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                        End If
                        gvItem.Rows(intRow).Cells(colActAmt).Value = CInt(xNewAmt)
                    End If
                    gvItem.Rows(intRow).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(Basicrate, 2))
                    gvItem.Rows(intRow).Cells(colNetRate).Value = clsCommon.myFormat(clsCommon.myCdbl(StdRate))
                Else
                    gvItem.Rows(intRow).Cells(colMilkRate).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colNetRate).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colStandardRate).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colFatRate).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colSNFRate).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colActAmt).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colFatQTy).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colSNFQty).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colTotalStdQTy).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colIncentiveAmt).Value = clsCommon.myFormat("0")
                    gvItem.Rows(intRow).Cells(colDeductionAmt).Value = clsCommon.myFormat("0")
                End If
            End If
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub gvItem_CellValidated(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatedEventArgs) Handles gvItem.CellValidated
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvItem.Columns(colMilkRate) Then
                    If TankerFromMaster = 0 Then
                        Dim stdRate As Double = clsCommon.myCdbl(gvItem.Rows(0).Cells(colStandardRate).Value)
                        Dim BasicRate As Double = clsCommon.myCdbl(gvItem.Rows(0).Cells(colMilkRate).Value)
                        Dim tolRance As Double = clsCommon.myCdbl(txtTolerance.Value)
                        If BasicRate <= (stdRate + (stdRate * tolRance / 100)) AndAlso BasicRate >= (stdRate - (stdRate * tolRance / 100)) Then
                            'gvItem.Rows(0).Cells(colNetRate).Value = (BasicRate + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value))
                            'gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeight).Value)
                            OpenPriceChart(False)
                        Else
                            gvItem.Rows(0).Cells(colMilkRate).Value = gvItem.Rows(0).Cells(colStandardRate).Value
                            clsCommon.MyMessageBoxShow(Me, "Invalid Basic Rate.It must be within tolerance of standard rate", Me.Text)
                            gvItem.Rows(0).Cells(colMilkRate).EndEdit()
                        End If

                    Else
                        Dim stdRate As Double = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colStandardRate).Value)
                        Dim BasicRate As Double = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colMilkRate).Value)
                        Dim tolRance As Double = clsCommon.myCdbl(txtTolerance.Value)
                        If BasicRate <= (stdRate + (stdRate * tolRance / 100)) AndAlso BasicRate >= (stdRate - (stdRate * tolRance / 100)) Then
                            '' commented by priti to ask ranjana for this.
                            'OpenPriceChartGridWise(True, e.RowIndex)
                        Else
                            gvItem.CurrentRow.Cells(colMilkRate).Value = gvItem.CurrentRow.Cells(colStandardRate).Value
                            clsCommon.MyMessageBoxShow(Me, "Invalid Basic Rate.It must be within tolerance of standard rate", Me.Text)
                            gvItem.CurrentRow.Cells(colMilkRate).EndEdit()
                        End If
                    End If

                End If
            End If
            isCellValueChangedOpen = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvItem_CellValidating(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles gvItem.CellValidating

    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colMilkRate) Then
                        If TankerFromMaster = 0 Then
                            OpenPriceChart(False)
                        End If
                    ElseIf e.Column Is gvItem.Columns(colpriceCode) Then
                        OpenPriceChartItemWise(True, e.RowIndex)
                    End If
                End If
            End If
            isCellValueChangedOpen = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsBulkMilkSRN.ReverseAndUnpost(fndSRNNo.Value, True) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndSRNNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndSRNNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndSRNNo._MYNavigator
        LoadData(fndSRNNo.Value, NavType)
    End Sub

    Private Sub fndSRNNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSRNNo._MYValidating
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "  loc_code in  (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            fndSRNNo.Value = clsBulkMilkSRN.getFinder(whrCls, fndSRNNo.Value, isButtonClicked)
            If clsCommon.myLen(fndSRNNo.Value) > 0 Then
                LoadData(fndSRNNo.Value, NavigatorType.Current)
            Else
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndWeighmentNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndWeighmentNo.Load

    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        'Query Modified by pankaj jha against Ticket No BM00000007877
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "   TSPL_Weighment_Detail.location_Code  in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If

            Dim qry As String = ""

            Dim strVendorType As String = ""
            If AllowRandomOnlyOneSecondaryQC = 0 Then
                strVendorType = " and TSPL_VENDOR_MASTER.Vendor_Type in ('A','B','C') "
            Else
                strVendorType = ""
            End If

            If clsCommon.myLen(whrCls) > 0 Then
                whrCls = whrCls & " and ((TSPL_Weighment_Detail.isPosted=1 and TSPL_Weighment_Detail.doc_type='BulkProc' " & strVendorType & " 	and isnull(TSPL_Bulk_MILK_SRN.Weighment_No,'')='' ) 	 	 or (select case when coalesce(Min(coalesce(BSRN.SRN_Return_NO,'')) ,'')='' then 1 else 0 end from TSPL_Bulk_MILK_SRN as BSRN where BSRN.Weighment_No=TSPL_Weighment_Detail.Weighment_No group by Weighment_No )=0) and is_Param_Accepted <> 0"
            Else
                whrCls = whrCls & "  ((TSPL_Weighment_Detail.isPosted=1 and TSPL_Weighment_Detail.doc_type='BulkProc' " & strVendorType & "	and isnull(TSPL_Bulk_MILK_SRN.Weighment_No,'')='' ) 	 	 or (select case when coalesce(Min(coalesce(BSRN.SRN_Return_NO,'')) ,'')='' then 1 else 0 end from TSPL_Bulk_MILK_SRN as BSRN where BSRN.Weighment_No=TSPL_Weighment_Detail.Weighment_No group by Weighment_No)=0) and is_Param_Accepted <> 0"
            End If
            If (TankerFromMaster = 0 OrElse AllowRandomOnlyOneSecondaryQC = 1) Then
                qry = "   select TSPL_Weighment_Detail.Tanker_No as [TankerNo],TSPL_Weighment_Detail.Weighment_No as WeighmentNo ,TSPL_Weighment_Detail.Weighment_date as [Weighment Date] ,TSPL_Weighment_Detail.Gate_Entry_No as [Gate Entry No] ,TSPL_Weighment_Detail.Doc_Type as [Doc Type] ,TSPL_Weighment_Detail.Date_And_Time as [Gate Entry Date And Time] ,TSPL_Weighment_Detail.Challan_No as [Challan No] ,TSPL_Weighment_Detail.Challan_Date as [Challan Date]  ,case when isnull(TSPL_Weighment_Detail.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_Weighment_Detail.Posting_Date as [Posting Date]  ,TSPL_Weighment_Detail.location_Code as [Location Code] ,TSPL_Weighment_Detail.Location_Desc as [Location Desc] ,TSPL_Weighment_Detail.Vendor_Code as [Vendor Code] ,TSPL_Weighment_Detail.Vendor_Desc as [Vendor Desc] ,TSPL_Weighment_Detail.Item_Code as [Item Code] ,TSPL_Weighment_Detail.Item_Desc as [Item Desc] ,TSPL_Weighment_Detail.Qty_In_Kg as [Qty] ,TSPL_Weighment_Detail.snf_Per as [SNF(%)] ,TSPL_Weighment_Detail.fat_per as [FAT(%)] ,TSPL_Weighment_Detail.Created_By as [Created By] ,TSPL_Weighment_Detail.Created_Date as [Created Date] ,TSPL_Weighment_Detail.Modify_By as [Modify By] ,TSPL_Weighment_Detail.Modify_Date as [Modify Date] ,TSPL_Weighment_Detail.comp_code as [Company Code] ,TSPL_Weighment_Detail.Gross_Weight as [Gross Weight] ,TSPL_Weighment_Detail.Dip_Value as [Dip Value] ,TSPL_Weighment_Detail.Tare_Weight as [Tare Weight] ,TSPL_Weighment_Detail.Net_Weight as [Net Weight]   From TSPL_Weighment_Detail   left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Weighment_Detail.vendor_code   left join TSPL_Bulk_MILK_SRN ON TSPL_Bulk_MILK_SRN.Weighment_No= TSPL_Weighment_Detail.Weighment_No " & _
                    "left outer join TSPL_QUALITY_CHECK on TSPL_Weighment_Detail.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No "
                fndWeighmentNo.Value = clsCommon.ShowSelectForm("BulkMilkSRNTnkrFND", qry, "WeighmentNo", whrCls, fndWeighmentNo.Value, "WeighmentNo", isButtonClicked, "TSPL_Weighment_Detail.Date_And_Time")
            Else
                qry = "  select * from (  select TSPL_Weighment_Detail.Tanker_No as [TankerNo],TSPL_Weighment_Detail.Weighment_No as WeighmentNo ,TSPL_Weighment_Detail.Weighment_date as [Weighment Date] ,TSPL_Weighment_Detail.Gate_Entry_No as [Gate Entry No] ,TSPL_Weighment_Detail.Doc_Type as [Doc Type] ,TSPL_Weighment_Detail.Date_And_Time as [Gate Entry Date And Time] ,TSPL_Weighment_Detail.Challan_No as [Challan No] ,TSPL_Weighment_Detail.Challan_Date as [Challan Date]  ,case when isnull(TSPL_Weighment_Detail.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_Weighment_Detail.Posting_Date as [Posting Date]  ,TSPL_Weighment_Detail.location_Code as [Location Code] ,TSPL_Weighment_Detail.Location_Desc as [Location Desc] ,TSPL_Weighment_Detail.Vendor_Code as [Vendor Code] ,TSPL_Weighment_Detail.Vendor_Desc as [Vendor Desc] ,TSPL_Weighment_Detail.Item_Code as [Item Code] ,TSPL_Weighment_Detail.Item_Desc as [Item Desc] ,TSPL_Weighment_Detail.Qty_In_Kg as [Qty] ,TSPL_Weighment_Detail.snf_Per as [SNF(%)] ,TSPL_Weighment_Detail.fat_per as [FAT(%)] ,TSPL_Weighment_Detail.Created_By as [Created By] ,TSPL_Weighment_Detail.Created_Date as [Created Date] ,TSPL_Weighment_Detail.Modify_By as [Modify By] ,TSPL_Weighment_Detail.Modify_Date as [Modify Date] ,TSPL_Weighment_Detail.comp_code as [Company Code] ,TSPL_Weighment_Detail.Gross_Weight as [Gross Weight] ,TSPL_Weighment_Detail.Dip_Value as [Dip Value] ,TSPL_Weighment_Detail.Tare_Weight as [Tare Weight] ,TSPL_Weighment_Detail.Net_Weight as [Net Weight] , " & _
                    "case when (select count(TSPL_MILK_GRADE_MASTER.GRADE_TYPE) from TSPL_QUALITY_CHEMBER_DETAILS left outer join TSPL_MILK_GRADE_MASTER on  TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE=TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE  where TSPL_QUALITY_CHEMBER_DETAILS.QC_No=TSPL_QUALITY_CHECK.QC_No  and TSPL_MILK_GRADE_MASTER.GRADE_TYPE='A' )> 0 then " & _
                    "( select count(Gate_Entry_No) from TSPL_SECONDARY_SETTING_QC_HEAD where TSPL_SECONDARY_SETTING_QC_HEAD.Gate_Entry_No=TSPL_Weighment_Detail.Gate_Entry_No and TSPL_SECONDARY_SETTING_QC_HEAD.Posted=1 ) else 1 end  as Grade  " & _
                    " From TSPL_Weighment_Detail   left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Weighment_Detail.vendor_code   left join TSPL_Bulk_MILK_SRN ON TSPL_Bulk_MILK_SRN.Weighment_No= TSPL_Weighment_Detail.Weighment_No " & _
                    "left outer join TSPL_QUALITY_CHECK on TSPL_Weighment_Detail.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No  " & _
                    " where " & whrCls & " ) xx"
                fndWeighmentNo.Value = clsCommon.ShowSelectForm("BulkMilkSRNTnkrFND", qry, "WeighmentNo", "", fndWeighmentNo.Value, "WeighmentNo", isButtonClicked, "xx.[Weighment Date]")
            End If

            'Gate Entry No
            If clsCommon.myLen(fndWeighmentNo.Value) > 0 Then
                Dim chk As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_transaction_approval where document_no=(select top 1 QC_No from tspl_quality_check where Weighment_No='" & clsCommon.myCstr(fndWeighmentNo.Value) & "') and approve=0"))
                'If clsCommon.myLen(fndWeighmentNo.Value) <= 0 Then
                '    fndWeighmentNo.Value = PrevCode
                'End If
                If chk > 0 Then
                    Throw New Exception("You can not process SRN of this tanker. It is pending for special Deduction")
                End If
                If TankerFromMaster = 1 AndAlso AllowRandomOnlyOneSecondaryQC = 0 Then
                    Dim strGateEntryNo = clsDBFuncationality.getSingleValue("select Gate_Entry_No from TSPL_Weighment_Detail where Weighment_no='" & fndWeighmentNo.Value & "'")
                    chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_SECONDARY_SETTING_QC_HEAD where TSPL_SECONDARY_SETTING_QC_HEAD.Posted=1 and Gate_Entry_No='" & strGateEntryNo & "'"))
                    If chk = 0 Then
                        Throw New Exception("You can not process SRN of this tanker. It is pending for Secondary QC")
                    End If
                End If
            End If
            If clsCommon.myLen(fndWeighmentNo.Value) > 0 Then
                isDocPosted = False
                loadWeighmentData(fndWeighmentNo.Value)
            Else
                Reset()

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvItem_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles gvItem.Validating

    End Sub

    Private Sub btnPrintPO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintPO.Click
        printPoData()
    End Sub

    Sub printPoData()
        clsCommon.MyMessageBoxShow(Me, "No Print Format Found", Me.Text)
    End Sub

    Sub SetParameterRange(ByVal intRow As Integer)
        Try
            loadBlankParameterGridwithRange()
            Dim whrCls As String = String.Empty
            If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
                whrCls = " and Param_for='MCC' or Param_for='BOTH'"
            Else
                whrCls = " and (Param_for='PLANT' or Param_for='BOTH')"
            End If
            Dim strlocation As String = ""
            If chkJobWork.Checked Then
                strlocation = txtSubLocation.Value
            Else
                strlocation = txtLocation.Text
            End If
            Dim paramName As String = String.Empty
            Dim qry1 As String = " select code,Description from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
            Dim qry2 As String = String.Empty
            Dim qry3 As String = String.Empty
            Dim paramValue As Double = 0
            intRow = intRow + 1

            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For i As Integer = 0 To dt1.Rows.Count - 1
                    qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & txtQCNo.Text & "' and line_No='" & intRow & "'  and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                    paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3))
                    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "'  and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end) and loc_code='" & strlocation & "'   and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "' " &
                        " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "
                    If i <> dt1.Rows.Count - 1 Then
                        qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                    End If
                Next

                qry2 = " select * from ( " & qry2 & " ) yyy"
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                    For j As Integer = 0 To dt2.Rows.Count - 1
                        paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                        gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                    Next
                End If
            End If

            qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
            qry2 = ""
            Dim paramValue1 As String = ""
            dt1 = clsDBFuncationality.GetDataTable(qry1)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For i As Integer = 0 To dt1.Rows.Count - 1
                    qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & txtQCNo.Text & "'  and line_No='" & intRow & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                    paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and 2=(case when End_Date is null then 2 else (case when End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end)  and  loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "' " &
                      " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "

                    If i <> dt1.Rows.Count - 1 Then
                        qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                    End If
                Next

                qry2 = " select * from ( " & qry2 & " ) yyy"
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                    For j As Integer = 0 To dt2.Rows.Count - 1
                        paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                        gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                    Next
                End If
            End If


            qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
            qry2 = ""
            paramValue1 = ""
            dt1 = clsDBFuncationality.GetDataTable(qry1)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                For i As Integer = 0 To dt1.Rows.Count - 1
                    qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & txtQCNo.Text & "'  and line_No='" & intRow & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                    paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'" &
                         " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & strlocation & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "

                    If i <> dt1.Rows.Count - 1 Then
                        qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                    End If
                Next

                qry2 = " select * from ( " & qry2 & " ) yyy"
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                    For j As Integer = 0 To dt2.Rows.Count - 1
                        paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                        gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                    Next
                End If
            End If
            If settConsiderAllParametersForIncetive Then
                Dim qry As String = "   select TSPL_PARAMETER_RANGE_MASTER.Code+' ( ' +max(TSPL_PARAMETER_MASTER.Description)+ ')' as Code from TSPL_PARAMETER_RANGE_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_PARAMETER_RANGE_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.loc_code='" & strlocation & "'  and TSPL_PARAMETER_RANGE_MASTER.MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'  and TSPL_PARAMETER_RANGE_MASTER.effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "'  and 2=(case when TSPL_PARAMETER_RANGE_MASTER.End_Date is null then 2 else (case when TSPL_PARAMETER_RANGE_MASTER.End_Date>='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' then 2 else 3 end) end) group by TSPL_PARAMETER_RANGE_MASTER.Code"
                Dim dtMandatoryParameter As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtMandatoryParameter IsNot Nothing AndAlso dtMandatoryParameter.Rows.Count > 0 Then
                    For Each dr As DataRow In dtMandatoryParameter.Rows
                        Dim flag As Boolean = False
                        For ii As Integer = 0 To gvRange.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(dr("Code")), clsCommon.myCstr(gvRange.Rows(ii).Cells("Code").Value)) = CompairStringResult.Equal Then
                                flag = True
                                Exit For
                            End If
                        Next
                        If Not flag Then
                            loadBlankParameterGridwithRange()
                            Exit For
                        End If
                    Next
                End If
                Dim MinValue As Decimal = 0
                If gvRange.Rows.Count > 0 Then
                    MinValue = clsCommon.myCdbl(gvRange.Rows(0).Cells("IncentiveDeduction").Value)
                    For ii As Integer = 0 To gvRange.Rows.Count - 1
                        If MinValue > clsCommon.myCdbl(gvRange.Rows(ii).Cells("IncentiveDeduction").Value) Then
                            MinValue = clsCommon.myCdbl(gvRange.Rows(ii).Cells("IncentiveDeduction").Value)
                        End If
                    Next
                End If
                If MinValue <> 0 Then
                    Dim dclTotal As Decimal
                    Dim dclRatio As Decimal = Math.Round(clsCommon.myCDivide(MinValue, gvRange.Rows.Count), 2)
                    For ii As Integer = 0 To gvRange.Rows.Count - 1
                        If ii = gvRange.Rows.Count - 1 Then
                            gvRange.Rows(ii).Cells("IncentiveDeduction").Value = MinValue - dclTotal
                        Else
                            gvRange.Rows(ii).Cells("IncentiveDeduction").Value = dclRatio
                            dclTotal += dclRatio
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvParam_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gvParam.CurrentRowChanged
        If TankerFromMaster = 1 Then
            ''richa agarwal UDL/24/01/19-000262 on 24 JAn,2019 replace from gvitem to gvParam
            If gvParam.CurrentRow IsNot Nothing Then
                SetParameterRange(gvParam.CurrentRow.Index)
            End If
        End If

    End Sub
    ' done by priti GKD/08/06/18-000147
    Private Sub btnReverseRec_Click(sender As Object, e As EventArgs) Handles btnReverseRec.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsBulkMilkSRN.ReverseAndUnpost(fndSRNNo.Value, False) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndSRNNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ' Ticket : TEC/29/10/18-000351 By Sanjay
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(fndSRNNo.Value)
    End Sub

    Private Sub txtTransportCharges_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtTransportCharges.Validating
        OpenPriceChart(True)
    End Sub
End Class
