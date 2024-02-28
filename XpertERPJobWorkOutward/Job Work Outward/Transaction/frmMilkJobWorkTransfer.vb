Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

'=============changes by shivani(BM00000008668)
Public Class frmMilkJobWorkTransfer
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim AllowManualRateonPO As Integer = 0
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
    Public Const colSubLocationCode As String = "colSubLocationCode"
    Public Const colSubLocationName As String = "colSubLocationName"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colItemHSNCode As String = "ItemHSNcode"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"
    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
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
    Public Const colActAmt As String = "colActAmt"
    Dim isCellValueChangedOpen As Boolean
    Public fatColName As String = String.Empty
    Public snfColName As String = String.Empty
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsMilkJobworkTransfer = Nothing
    Public DocumentNo As String = ""
    Public IsAutoMilkRGP As Boolean = False


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
    Dim gv_qc As New RadGridView
    Dim BulkProcPricePostedData As Boolean
    '=============================================
    Dim RunBulkProcOnAdjustFATCLR As Integer = 0
    Dim allowMilkJobworkOutowordWithAvgFatSNFRate As Boolean = False
#End Region

    Private Sub FrmBulkMilkSRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim coll As New Dictionary(Of String, String)
        coll.Add("Receipt_Net_Weight", "decimal(18,3) NULL")
        coll.Add("Receipt_snf_Per", "decimal(18,2) NULL")
        coll.Add("Receipt_fat_per", "decimal(18,2) NULL")
        coll.Add("Receipt_fat_KG", "decimal(18,3) NULL")
        coll.Add("Receipt_SNF_KG", "decimal(18,3) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MILK_JOBWORK_TRANSFER_DETAILS", coll, Nothing, False, False, "TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", "")




        AllowManualRateonPO = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowManualPriceONBulkPO, clsFixedParameterCode.AllowManualPriceONBulkPO, Nothing))
        AllowRandomOnlyOneSecondaryQC = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowRandomOnlyOneSecondaryQC, clsFixedParameterCode.AllowRandomOnlyOneSecondaryQC, Nothing))
        AllowNLevel = clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmMilkJobWorkTransfer)
        SetUserMgmtNew()
        IsItemMilkType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
        IsPriceChartGradeWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        ''=======BM00000010118===============
        AllowTruncateAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_AmountTruncate_BulkMilkSRN, clsFixedParameterCode.Allow_AmountTruncate_BulkMilkSRN, Nothing)) = "1", True, False)
        ''========================
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

        BulkProcPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcItemPostedData, clsFixedParameterCode.AllowBulkProcItemPostedData, Nothing)) = 1, True, False)
        allowMilkJobworkOutowordWithAvgFatSNFRate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.allowMilkJWOutowordWithAvgFatSNFRate, clsFixedParameterCode.allowMilkJWOutowordWithAvgFatSNFRate, Nothing)) = 1, True, False)
        If allowMilkJobworkOutowordWithAvgFatSNFRate = True Then
            GroupBox1.Visible = False
        End If
    End Sub

    Sub Reset()
        txtLocation.MyReadOnly = False
        txtJobWork.MyReadOnly = False
        txtJobWork.Enabled = True
        txtLocation.Enabled = True
        txtManualPrice.Text = 0
        If AllowNLevel Then
            btnPost.Visible = MyBase.isPostFlag
        End If

        fndDocNo.Value = ""
        'fndWeighmentNo.Enabled = True
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
        txtSRNNo.Text = ""
        txtVirtual.Text = ""
        lblVirtual.Text = ""
        txtJobWork.Value = ""
        lblJobWork.Text = ""
        txtGateEntryNo.Text = ""
        dtpSRNDATE.Value = dt
        fndWeighmentNo.Text = ""
        dtpWeighmentDate.Value = dt
        txtVendor.Text = ""
        lblVendorName.Text = ""
        txtLocation.Value = clsGateEntry.getUsersDefaultLocation()
        lblLocationDesc.Text = clsLocation.GetName(txtLocation.Value, Nothing)
        txtChallanNo.Text = ""
        dtpChallanDate.Value = dt
        fndTankerNo.Text = ""
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
        gvReceipt.DataSource = Nothing
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
        fndDocNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        RadPageView1.Pages("QcDetails").Item.Visibility = ElementVisibility.Collapsed
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
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
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
        gvItem.Columns(colItemCode).ReadOnly = False

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colItemHSNCode, "HSN Code")
        gvItem.Columns(colItemHSNCode).Width = 100
        gvItem.Columns(colItemHSNCode).ReadOnly = True

        gvItem.Columns.Add(colSubLocationCode, "Sub Location")
        gvItem.Columns(colSubLocationCode).Width = 100
        gvItem.Columns(colSubLocationCode).ReadOnly = False

        gvItem.Columns.Add(colSubLocationName, "Sub Location Desc.")
        gvItem.Columns(colSubLocationName).Width = 150
        gvItem.Columns(colSubLocationName).ReadOnly = True

        gvItem.Columns.Add(colpriceCode, "Price Code")
        gvItem.Columns(colpriceCode).Width = 150
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

        Dim repoGross As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoGross = New GridViewDecimalColumn()
        repoGross.FormatString = ""
        repoGross.HeaderText = "Gross Weight"
        repoGross.Name = colGrossWeight
        repoGross.IsVisible = True
        repoGross.Minimum = 0
        repoGross.Width = 100
        repoGross.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoGross.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoGross)

        Dim repoTare As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTare = New GridViewDecimalColumn()
        repoTare.FormatString = ""
        repoTare.HeaderText = "Tare Weight"
        repoTare.Name = colTareWeight
        repoTare.IsVisible = True
        repoTare.Minimum = 0
        repoTare.Width = 100
        repoTare.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTare.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoTare)

        gvItem.Columns.Add(colNetWeight, "Net Weight")
        gvItem.Columns(colNetWeight).Width = 120
        gvItem.Columns(colNetWeight).ReadOnly = True
        gvItem.Columns(colNetWeight).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = False

        Dim repoFat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFat = New GridViewDecimalColumn()
        repoFat.FormatString = ""
        repoFat.HeaderText = "FAT (%)"
        repoFat.Name = colFat
        repoFat.IsVisible = True
        repoFat.Minimum = 0
        repoFat.Width = 75
        repoFat.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFat.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoFat)

        Dim repoSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNF = New GridViewDecimalColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF (%)"
        repoSNF.Name = colSNF
        repoSNF.IsVisible = True
        repoSNF.Minimum = 0
        repoSNF.Width = 100
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = False
        gvItem.MasterTemplate.Columns.Add(repoSNF)

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
        gvItem.Columns(colStandardRate).IsVisible = False
        gvItem.Columns(colStandardRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colDeducRate, "Deduction Per Unit")
        gvItem.Columns(colDeducRate).Width = 150
        gvItem.Columns(colDeducRate).ReadOnly = True
        gvItem.Columns(colDeducRate).IsVisible = False
        gvItem.Columns(colDeducRate).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colIncenRate, "Incentive Per Unit")
        gvItem.Columns(colIncenRate).Width = 150
        gvItem.Columns(colIncenRate).ReadOnly = True
        gvItem.Columns(colIncenRate).IsVisible = False
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

        ''richa Against Ticket No.BM00000003719 on 04/09/2014
        gvItem.Columns.Add(colSpecialDeduction, "Special Deduction")
        gvItem.Columns(colSpecialDeduction).Width = 100
        gvItem.Columns(colSpecialDeduction).ReadOnly = True
        gvItem.Columns(colSpecialDeduction).IsVisible = False
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


        gvItem.Columns.Add(colActAmt, "Amount")
        gvItem.Columns(colActAmt).Width = 75
        gvItem.Columns(colActAmt).ReadOnly = True
        gvItem.Columns(colActAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Rows.AddNew()
        gvItem.Rows(0).Cells(colSlNo).Value = "1"

        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = True
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = True
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AllowColumnChooser = True
        gvItem.AllowColumnReorder = True
        ReStoreGridLayout()
    End Sub

    Sub loadBlankParameterGrid()
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            gvParam.Rows.Clear()
            gvParam.Columns.Clear()
            gvParam.DataSource = Nothing
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        If clsERPFuncationality.isLocationMcc(txtLocation.Value) Then
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
        If Not (MyBase.isReadFlag) Then
            '  If MDI.blnShowAllMenu = False Then
            Throw New Exception("Permission Denied")
            'Else
            '    Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            'End If
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
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
                ' MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(fndDocNo.Value), trans)
                End If
            End If
            trans.Commit()
            trans = Nothing
            If allowToSave() Then
                obj = New clsMilkJobworkTransfer()
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    obj.isNewEntry = True
                Else
                    obj.isNewEntry = False
                End If
                trans = clsDBFuncationality.GetTransactin()
                Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")

                obj.Manual_Standard_Rate = txtManualPrice.Text
                obj.Doc_Type = txtDocType.Text
                obj.Milk_Transfer_In = txtMilkTransferIn.Text
                obj.isApproved = 0
                obj.Document_Code = fndDocNo.Value
                obj.Document_Date = dtpSRNDATE.Value
                obj.SRN_NO = txtSRNNo.Text
                obj.Virtual_location = txtVirtual.Text
                obj.JobWork_location = txtJobWork.Value
                obj.Gate_Entry_No = txtGateEntryNo.Text
                obj.Weighment_No = fndWeighmentNo.Text
                obj.Weighment_Date = dtpWeighmentDate.Value
                obj.Vendor_Code = txtVendor.Text
                obj.Loc_Code = txtLocation.Value
                obj.Challan_No = txtChallanNo.Text
                obj.Challan_Date = dtpChallanDate.Value
                obj.Tanker_No = fndTankerNo.Text
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

                obj.Modify_By = objCommonVar.CurrentUserCode
                obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                obj.comp_code = objCommonVar.CurrentCompanyCode
                If obj.isNewEntry Then
                    obj.Created_By = objCommonVar.CurrentUserCode
                    obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
                End If


                obj.Arr = New List(Of clsMilkJobworkTransferDetails)
                For Each grow As GridViewRowInfo In gvItem.Rows
                    Dim objTr As New clsMilkJobworkTransferDetails()
                    objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSlNo).Value)
                    objTr.Chamber_Desc = clsCommon.myCstr(grow.Cells(colChamberDesc).Value)
                    objTr.Sub_Location = clsCommon.myCstr(grow.Cells(colSubLocationCode).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells(colUOM).Value)
                    objTr.fat_per = clsCommon.myCdbl(grow.Cells(colFat).Value)
                    objTr.snf_Per = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                    objTr.Gross_Weight = grow.Cells(colGrossWeight).Value
                    objTr.Tare_Weight = grow.Cells(colTareWeight).Value
                    objTr.Net_Weight = grow.Cells(colNetWeight).Value
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
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                If clsMilkJobworkTransfer.saveData(obj, trans) Then
                    trans.Commit()
                    If Not isPost Then
                        If isAutoSRN = False Then
                            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                            Else
                                clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully")
                            End If
                        End If
                    End If

                    ''done by stuti approval work 13/12/2016
                    If AllowNLevel Then
                        clsApply_Approval.CheckApprovalRequired(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(obj.Document_Code), dtpSRNDATE.Text, "", "", clsCommon.myCdbl(obj.Actual_Amount), clsCommon.myCdbl(totalqty), "", objApproval)
                    End If
                    '================================================================

                    LoadData(obj.Document_Code, NavigatorType.Current)
                    Exit Sub
                End If
            End If
            'clsCommon.MyMessageBoxShow(Me,"Data Not Saved ")
            'btnSave.Text = "Save"
            'btnDelete.Enabled = False
            'btnPost.Enabled = False
            'btnPrint.Enabled = False
            'fndSRNNo.MyReadOnly = False
            'trans.Rollback()

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            If clsCommon.myLen(fndPriceChart.Value) <= 0 AndAlso allowMilkJobworkOutowordWithAvgFatSNFRate = False Then
                Throw New Exception("Please Select a Price Chart")
                errorControl.SetError(fndPriceChart, "Please Select a Price Chart")
            Else
                errorControl.ResetError(fndPriceChart)
            End If

            If clsCommon.myCdbl(txtManualPrice.Text) = 0 AndAlso allowMilkJobworkOutowordWithAvgFatSNFRate = False Then
                Throw New Exception("Please enter Manual Rate")
            End If
            For ii As Integer = 0 To gvItem.Rows.Count - 1
                Dim strItem As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colItemCode).Value)
                Dim strSubLocation As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colSubLocationCode).Value)
                If clsCommon.myLen(strItem) > 0 Then
                    Dim dblGross As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colGrossWeight).Value)
                    Dim dblTare As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTareWeight).Value)
                    Dim dblNetWeight As Decimal = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetWeight).Value)


                    Dim dblFat As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colFat).Value)
                    Dim dblSNF As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNF).Value)
                    Dim strUOM As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colUOM).Value)
                    If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dtpSRNDATE.Value)) Then
                        Dim HSNCode As String = clsItemMaster.GetItemHSNCode(gvItem.Rows(ii).Cells(colItemCode).Value, Nothing)
                        gvItem.Rows(ii).Cells(colItemHSNCode).Value = HSNCode
                        If clsCommon.myLen(HSNCode) <= 0 Then
                            Throw New Exception("HSN Code is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                        End If
                    End If
                    If dblGross = 0 Then
                        Throw New Exception("Please enter Gross Weight.")
                    ElseIf dblTare = 0 Then
                        Throw New Exception("Please enter Tare Weight.")
                    ElseIf dblGross <= dblTare Then
                        Throw New Exception("Gross Wt should be greater than Tare weight")
                    ElseIf dblFat = 0 Then
                        Throw New Exception("Please enter Fat %")
                    ElseIf dblSNF = 0 Then
                        Throw New Exception("Please enter SNF %")
                    ElseIf clsCommon.myLen(strSubLocation) <= 0 Then
                        Throw New Exception("Please Enter Sub Location")
                    End If

                    '====For Inner Check===================
                    For jj As Integer = 0 To gvItem.Rows.Count - 1
                        If jj = ii Then
                            Continue For
                        End If
                        Dim strInnerICode As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colItemCode).Value)
                        Dim strInnerSubLocation As String = clsCommon.myCstr(gvItem.Rows(jj).Cells(colSubLocationCode).Value)
                        If clsCommon.CompairString(strItem, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strSubLocation, strInnerSubLocation) = CompairStringResult.Equal Then
                            Dim Msg As String = "Same Item Code and Sub Location Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    Next
                    '=============================================
                    Dim dblSubLocbal As Double = 0
                    Dim dblBalQty As Double
                    Dim CurBalOFVL As Double = 0
                    Dim CurBalOFML As Double = 0
                    CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(strItem, txtLocation.Value, strSubLocation, fndDocNo.Value, dtpSRNDATE.Value, Nothing, "KG"))
                    dblBalQty = CurBalOFVL + CurBalOFML

                    If dblNetWeight <= 0 Then
                        Throw New Exception("Qty Can not be blank or 0 at Row No " + clsCommon.myCstr(ii + 1))
                    End If
                    If clsCommon.myCdbl(dblNetWeight) > dblBalQty Then
                        Throw New Exception("Item - " + strItem + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblNetWeight) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Sub UpdateTotal()
        For ii As Integer = 0 To gvItem.Rows.Count - 1
            Dim strItem As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colItemCode).Value)
            Dim strUOM As String = clsCommon.myCstr(gvItem.Rows(ii).Cells(colUOM).Value)
            Dim dblGross As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colGrossWeight).Value)
            Dim dblTare As Double = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colTareWeight).Value)
            Dim dblNet As Double = 0
            Dim dblFatPercentage As Double = 0
            Dim dblSNFPercentage As Double = 0
            If clsCommon.myLen(strItem) > 0 Then
                dblNet = dblGross - dblTare
                gvItem.Rows(ii).Cells(colNetWeight).Value = dblNet
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
                    'Dim dt As DataTable
                    If clsCommon.myLen(fndPriceChart.Value) > 0 Then
                        gvItem.Rows(ii).Cells(colMilkRate).Value = clsCommon.myFormat(txtManualPrice.Text)
                        dblFatPercentage = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colFat).Value)
                        dblSNFPercentage = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNF).Value)
                        gvItem.Rows(ii).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(dblNet * MyMath.RoundDown(dblFatPercentage, 2) / 100, 3), False, True, False, 3, True)
                        gvItem.Rows(ii).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(dblNet * MyMath.RoundDown(dblSNFPercentage, 2) / 100, 3), False, True, False, 3, True)
                        FatW = clsCommon.myCdbl(TxtFatWeightage.Text)
                        SNfW = clsCommon.myCdbl(TxtSNFWeightage.Text)
                        gvItem.Rows(ii).Cells(colStandardRate).Value = txtStanadardrate.Text
                        fatKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colFatKG).Value), 3), False, True, False, 3, True)
                        snfKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNFKG).Value), 3), False, True, False, 3, True)
                        FATRatio = clsCommon.myCdbl(txtfatPercentage.Text)
                        SNFRatio = clsCommon.myCdbl(txtSNFPercentage.Text)
                        gvItem.Rows(ii).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(gvItem.Rows(ii).Cells(colMilkRate).Value) + clsCommon.myCdbl(gvItem.Rows(ii).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value)))
                        gvItem.Rows(ii).Cells(colNetRate).EndEdit()
                        FATRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetRate).Value) * FatW / FATRatio, 2)
                        SNFRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetRate).Value) * SNfW / SNFRatio, 2)
                        FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
                        SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
                        gvItem.Rows(ii).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                        gvItem.Rows(ii).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                        gvItem.Rows(ii).Cells(colFatAmt).Value = clsCommon.myFormat(MyMath.RoundDown(FATValue, 2))
                        gvItem.Rows(ii).Cells(colSNFAmt).Value = clsCommon.myFormat(MyMath.RoundDown(SNfValue, 2))
                        fndTankerNo.Enabled = False
                        gvItem.Rows(ii).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                        If AllowTruncateAmount Then ''BM00000010118
                            Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                            If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                            End If
                            gvItem.Rows(ii).Cells(colActAmt).Value = CInt(xNewAmt)
                        End If
                        gvItem.Rows(ii).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetWeight).Value), 2))
                    ElseIf allowMilkJobworkOutowordWithAvgFatSNFRate = True Then
                        dblFatPercentage = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colFat).Value)
                        dblSNFPercentage = clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNF).Value)
                        gvItem.Rows(ii).Cells(colFatKG).Value = clsBOM.GetFatSNFKG_AfterConversion(strItem, strUOM, dblNet, dblFatPercentage, Nothing)
                        gvItem.Rows(ii).Cells(colSNFKG).Value = clsBOM.GetFatSNFKG_AfterConversion(strItem, strUOM, dblNet, dblSNFPercentage, Nothing)
                        gvItem.Rows(ii).Cells(colStandardRate).Value = txtStanadardrate.Text
                        fatKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colFatKG).Value), 3), False, True, False, 3, True)
                        snfKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colSNFKG).Value), 3), False, True, False, 3, True)
                        Dim objCost As New MIlkComponentType

                        objCost = clsInventoryMovementNew.GetAvgCost(False, False, False, "", "MI", strItem, clsCommon.myCstr(gvItem.Rows(ii).Cells(colSubLocationCode).Value), dblNet, strUOM, 1, 1, clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy"), clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy"), True, Nothing)
                        FATRatio = dblFatPercentage
                        SNFRatio = dblSNFPercentage
                        gvItem.Rows(ii).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(gvItem.Rows(ii).Cells(colMilkRate).Value) + clsCommon.myCdbl(gvItem.Rows(ii).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value)))
                        gvItem.Rows(ii).Cells(colNetRate).EndEdit()
                        FATRate = objCost.FAT_Cost
                        SNFRate = objCost.SNF_Cost
                        FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
                        SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
                        gvItem.Rows(ii).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                        gvItem.Rows(ii).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                        gvItem.Rows(ii).Cells(colFatAmt).Value = clsCommon.myFormat(MyMath.RoundDown(FATValue, 2))
                        gvItem.Rows(ii).Cells(colSNFAmt).Value = clsCommon.myFormat(MyMath.RoundDown(SNfValue, 2))
                        fndTankerNo.Enabled = True
                        gvItem.Rows(ii).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                        If AllowTruncateAmount Then
                            Dim xNewAmt As Double = clsCommon.myCdbl(FATValue + SNfValue)
                            If clsCommon.myLen(xNewAmt) > 0 AndAlso clsCommon.myCstr(xNewAmt).Contains(".") Then
                                xNewAmt = clsCommon.myCstr(xNewAmt).Substring(0, clsCommon.myCstr(xNewAmt).IndexOf("."))
                            End If
                            gvItem.Rows(ii).Cells(colActAmt).Value = CInt(xNewAmt)
                        End If
                        gvItem.Rows(ii).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetWeight).Value), 2))
                        gvItem.Rows(ii).Cells(colMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(ii).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(ii).Cells(colNetWeight).Value), 2))
                    Else
                        gvItem.Rows(ii).Cells(colMilkRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colNetRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colStandardRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colFatRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colSNFRate).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colActAmt).Value = clsCommon.myFormat("0")
                        gvItem.Rows(ii).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        SaveData(False, False)

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Enter Document No To delete ")
        Else

            If myMessages.deleteConfirm() Then

                If AllowNLevel Then
                    clsApply_Approval.CheckUpdate_Doc_Valid(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(fndDocNo.Value))
                End If

                If clsMilkJobworkTransfer.deleteData(fndDocNo.Value, Nothing) Then
                    Reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub
    ''Updateed by Preeti Gupta ticket no[BM00000004915]
    Sub printData(ByVal DocNo As String)
        Dim frmCrystalReportViewer As New frmCrystalReportViewer()
        Dim strQuery As String = Nothing
        If clsCommon.myLen(DocNo) > 0 Then

            strQuery = "select TSPL_VENDOR_MASTER.City_Code as Ven_city_code,Ven_City_Master.City_Name as Ven_City_Name ,TSPL_VENDOR_MASTER.State_Code as Vendor_State_code,Job_Work_VendorState_Master.STATE_NAME as Ven_State_Name, To_Locatoin.Location_Code as To_Location_Code,To_Locatoin.Location_Desc as To_Location_Name ,  case when isnull(TSPL_VENDOR_MASTER.Add1,'')<>'' then TSPL_VENDOR_MASTER.Add1  else '' end + case when  isnull(TSPL_VENDOR_MASTER.Add2,'')<>'' then  ','+TSPL_VENDOR_MASTER.Add2 else '' end + case when isnull(TSPL_VENDOR_MASTER.Add3,'')<>'' then ',' + TSPL_VENDOR_MASTER.Add3   else '' end as JobWork_Vendor_Add,TSPL_VENDOR_MASTER.GSTFinalNo as JobWork_Vendor_GSTIN,Job_Work_VendorState_Master.GST_STATE_Code as JobWork_Vendor_Gst_State, '' as Electronic_Ref_No,TSPL_QC_Parameter_Detail.Param_Field_Desc,TSPL_QC_Parameter_Detail.Param_Field_Code," &
                " TSPL_QC_Parameter_Detail.Param_Field_Value,TSPL_QC_Parameter_Detail.Param_Type,TSPL_COMPANY_MASTER.Comp_Name,TSPL_VENDOR_MASTER.Vendor_Name," &
                " TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code,CONVERT(VARCHAR(15),TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date,103) AS Document_Date ," &
                " TSPL_MILK_JOBWORK_TRANSFER_HEAD.Challan_No,CONVERT(VARCHAR(15),TSPL_MILK_JOBWORK_TRANSFER_HEAD.Challan_Date,103) AS Challan_Date  ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Tanker_No,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Vendor_Code,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Net_Weight,TSPL_MILK_JOBWORK_TRANSFER_HEAD.snf_Per,TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_per ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_KG ," &
                " TSPL_MILK_JOBWORK_TRANSFER_HEAD.SNF_KG,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Actual_Amount,TSPL_MILK_JOBWORK_TRANSFER_HEAD.StandardRate," &
                " TSPL_Bulk_Price_MASTER.Fat_Percentage,TSPL_Bulk_Price_MASTER.Snf_Percentage,TSPL_Bulk_Price_MASTER.Fat_Weightage,TSPL_Bulk_Price_MASTER.Snf_Weightage," &
                " TSPL_Weighment_Detail.Dip_Value,From_Location.GSTNO as From_GSTNO,From_State_Master.GST_STATE_Code as From_GST_State,From_State_Master.STATE_NAME as From_State_Name," &
                 " case when ISNULL(From_Location.Add1,'')<>'' then From_Location.Add1 else '' end + case when ISNULL(From_Location.Add2,'')<>'' then ', '+From_Location.Add2 else '' end +case when ISNULL(From_Location.Add3,'')<>'' then ','+From_Location.Add3 else '' end as FromLocAddress," &
                 " To_Locatoin.GSTNO as To_GSTNO,TO_State_Master.GST_STATE_Code as To_GST_State,TO_State_Master.STATE_NAME as To_State_Name,To_Locatoin.City_Code as To_City," &
                 " case when ISNULL(To_Locatoin.Add1,'')<>'' then To_Locatoin.Add1 else '' end + case when ISNULL(To_Locatoin.Add2,'')<>'' then ', '+To_Locatoin.Add2 else '' end +case when ISNULL(To_Locatoin.Add3,'')<>'' then ','+To_Locatoin.Add3 else '' end as TOLocAddress," &
                            " '' as E_waybillNo,'' as E_WayBillDate,'' as Dispatch_Through,'' as order_no,'' as order_date,'' as Other_Reference " &
                           " from TSPL_MILK_JOBWORK_TRANSFER_HEAD" &
                " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_MILK_JOBWORK_TRANSFER_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code " &
                " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_MILK_JOBWORK_TRANSFER_HEAD.Item_Code=TSPL_ITEM_MASTER.Item_Code " &
                " left outer join TSPL_Bulk_Price_MASTER on TSPL_MILK_JOBWORK_TRANSFER_HEAD.Price_Code=TSPL_Bulk_Price_MASTER.Price_Code" &
                " left outer join TSPL_Weighment_Detail on TSPL_MILK_JOBWORK_TRANSFER_HEAD.Weighment_No=TSPL_Weighment_Detail.Weighment_No" &
                " left outer join tspl_location_master as From_Location on TSPL_MILK_JOBWORK_TRANSFER_HEAD.Loc_Code=From_Location.Location_Code" &
                " LEFT OUTER JOIN TSPL_STATE_MASTER AS From_State_Master on From_Location.State=From_State_Master.STATE_CODE" &
                " left outer join tspl_location_master as To_Locatoin on TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location=To_Locatoin.Location_Code" &
                " LEFT OUTER JOIN TSPL_STATE_MASTER AS TO_State_Master on To_Locatoin.State=TO_State_Master.STATE_CODE" &
                " LEFT OUTER JOIN TSPL_CITY_MASTER AS To_City_Master on To_Locatoin.City_Code=To_City_Master.City_Code" &
                " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_MILK_JOBWORK_TRANSFER_HEAD.comp_code=TSPL_COMPANY_MASTER.Comp_Code" &
                " LEFT OUTER JOIN TSPL_STATE_MASTER AS Job_Work_VendorState_Master on TSPL_VENDOR_MASTER.State_Code=Job_Work_VendorState_Master.STATE_CODE " &
            " LEFT OUTER JOIN TSPL_CITY_MASTER AS Ven_City_Master on Ven_City_Master.City_Code=TSPL_VENDOR_MASTER.City_Code" &
                " LEFT OUTER JOIN TSPL_QC_Parameter_Detail ON TSPL_MILK_JOBWORK_TRANSFER_HEAD.QC_No=TSPL_QC_Parameter_Detail.QC_No" &
                 " AND ISNULL(TSPL_QC_Parameter_Detail.Param_Field_Value,0) NOT IN ('0','')  AND TSPL_QC_Parameter_Detail.Param_Field_Desc NOT IN('FAT %','SNF %','Detergent','Flavour','Vegetable Fat','Washing Diff')" &
                " where TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code='" & DocNo & "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
            If dt.Rows.Count > 0 Then
                If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dt.Rows(0)("Document_Date"))) Then
                    frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "RptMilkJobWorkTransfer", "Milk Job Work Transfer", clsCommon.myCDate(dt.Rows(0)("Document_Date")), "rptCompanyAddress.rpt")
                End If
            End If
            ' frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkSRN", "Milk SRN", clsCommon.myCDate(dtpSRNDATE.Value))
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select an invoice to print")
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData(fndDocNo.Value)
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
                SaveData(True, False)

                If (clsMilkJobworkTransfer.postData(fndDocNo.Value, Me.Form_ID)) Then
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
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strSrnNo As String, ByVal nav As NavigatorType)
        Dim objApproval As New clsApply_Approval()
        obj = clsMilkJobworkTransfer.getData(strSrnNo, nav)

        If obj IsNot Nothing Then
            isInsideLoadData = True
            txtJobWork.Enabled = False
            txtLocation.Enabled = False

            txtManualPrice.Text = obj.Manual_Standard_Rate
            isDocPosted = IIf(obj.isPosted = 0, False, True)
            txtLocation.Enabled = False
            txtMilkTransferIn.Text = obj.Milk_Transfer_In
            txtDocType.Text = obj.Doc_Type
            fndDocNo.Value = obj.Document_Code
            txtSRNNo.Text = obj.SRN_NO
            txtVirtual.Text = obj.Virtual_location
            txtJobWork.Value = obj.JobWork_location
            dtpSRNDATE.Value = obj.Document_Date
            fndWeighmentNo.Text = obj.Weighment_No
            dtpWeighmentDate.Value = obj.Weighment_Date
            txtQCNo.Text = obj.QC_No
            txtGateEntryNo.Text = obj.Gate_Entry_No
            txtVendor.Text = obj.Vendor_Code
            lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
            txtLocation.Value = obj.Loc_Code
            lblLocationDesc.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
            lblVirtual.Text = clsLocation.GetName(obj.Virtual_location, Nothing)
            lblJobWork.Text = clsLocation.GetName(obj.JobWork_location, Nothing)
            txtChallanNo.Text = obj.Challan_No
            dtpChallanDate.Value = obj.Challan_Date
            fndPriceChart.Value = obj.Price_Code
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select * from tspl_Bulk_price_master where Price_Code ='" & fndPriceChart.Value & "'  ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                TxtFatWeightage.Text = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                TxtSNFWeightage.Text = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                txtfatPercentage.Text = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                txtSNFPercentage.Text = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                txtStanadardrate.Text = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                txtTolerance.Text = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                '    gvItem.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            End If
            fndTankerNo.Text = obj.Tanker_No
            Dim objQ As clsQualityCheck = clsQualityCheck.getData(txtQCNo.Text, "BulkProc", NavigatorType.Current)
            dtpQCInTime.Value = objQ.QC_In_Date_Time
            dtpQCOutTime.Value = objQ.QC_Out_Date_Time
            txtRemarks.Text = objQ.Remarks
            txtDipValue.Text = objQ.Dip_Value
            loadBlankItemGrid()

            If (clsCommon.myLen(obj.SRN_NO) > 0 OrElse clsCommon.myLen(obj.Milk_Transfer_In) > 0) Then

                gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code
                gvItem.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
                If clsCommon.myLen(obj.Item_Code) > 0 Then
                    gvItem.Rows(0).Cells(colItemHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where Item_Code ='" + obj.Item_Code + "'"))
                End If

                gvItem.Rows(0).Cells(colUOM).Value = obj.UOM
                gvItem.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.Gross_Weight)
                gvItem.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(obj.Tare_Weight)
                gvItem.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(obj.Net_Weight)
                If obj.isPosted = 0 Then
                    gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_per, 2))
                    gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(obj.snf_Per, 2))
                    gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_KG, 3), False, True, False, 3, True)
                    gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.SNF_KG, 3), False, True, False, 3, True)
                    gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_Rate, 2))
                    gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.SNF_Rate, 2))
                Else
                    gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(obj.fat_per)
                    gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(obj.snf_Per)
                    gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(obj.fat_KG, False, True, False, 3, True)
                    gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(obj.SNF_KG, False, True, False, 3, True)
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

            Else

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    isInsideLoadData = True
                    gvItem.Rows.Clear()

                    For Each objTr As clsMilkJobworkTransferDetails In obj.Arr
                        gvItem.Rows.AddNew()

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSlNo).Value = objTr.Line_No
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colChamberDesc).Value = objTr.Chamber_Desc
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSubLocationCode).Value = objTr.Sub_Location
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSubLocationName).Value = clsLocation.GetName(objTr.Sub_Location, Nothing)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        If clsCommon.myLen(objTr.Item_Code) > 0 Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemHSNCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select HSN_Code from TSPL_ITEM_MASTER where Item_Code ='" + objTr.Item_Code + "'"))
                        End If

                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colItemDesc).Value = clsIntimation.getItemName(objTr.Item_Code, Nothing)
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colGrossWeight).Value = objTr.Gross_Weight
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colTareWeight).Value = objTr.Tare_Weight
                        gvItem.Rows(gvItem.Rows.Count - 1).Cells(colNetWeight).Value = objTr.Net_Weight

                        If obj.isPosted = 0 Then
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.fat_per, 2))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.snf_Per, 2))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.fat_KG, 3), False, True, False, 3, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.SNF_KG, 3), False, True, False, 3, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.fat_Rate, 2))
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(objTr.SNF_Rate, 2))
                        Else
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFat).Value = clsCommon.myFormat(objTr.fat_per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNF).Value = clsCommon.myFormat(objTr.snf_Per)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colFatKG).Value = clsCommon.myFormat(objTr.fat_KG, False, True, False, 3, True)
                            gvItem.Rows(gvItem.Rows.Count - 1).Cells(colSNFKG).Value = clsCommon.myFormat(objTr.SNF_KG, False, True, False, 3, True)
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
                    Next
                End If



            End If
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
                If clsERPFuncationality.isLocationMcc(txtLocation.Value) Then
                    whrCls = " and Param_for='MCC' or Param_for='BOTH'"
                Else
                    whrCls = " and Param_for='PLANT' or Param_for='BOTH'"
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
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range, '" & paramValue & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Value & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "
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
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value , '" & paramValue1 & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Value & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
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
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status , '" & paramValue1 & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Value & "'   and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "
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
            If isDocPosted Then
                Dim qry As String = "select TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Document_Code,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Line_No,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.UOM,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Net_Weight,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_Net_Weight,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.fat_per,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_fat_per,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.fat_KG,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_fat_KG,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.snf_Per,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_snf_Per,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.SNF_KG,TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Receipt_SNF_KG 
from TSPL_MILK_JOBWORK_TRANSFER_DETAILS 
left outer join tspl_item_master on TSPL_ITEM_MASTER.item_Code=TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Item_Code
where Document_Code='" + fndDocNo.Value + "' order by Line_No"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gvReceipt.DataSource = dt
                    For ii As Integer = 0 To gvReceipt.Columns.Count - 1
                        gvReceipt.Columns(ii).ReadOnly = True
                        gvReceipt.Columns(ii).IsVisible = True
                    Next

                    gvReceipt.Columns("Document_Code").HeaderText = "Document_Code"
                    gvReceipt.Columns("Document_Code").IsVisible = False

                    gvReceipt.Columns("Line_No").HeaderText = "Line No"
                    gvReceipt.Columns("Item_Code").HeaderText = "Item Code"
                    gvReceipt.Columns("Item_Desc").HeaderText = "Item"
                    gvReceipt.Columns("UOM").HeaderText = "UOM"
                    gvReceipt.Columns("Net_Weight").HeaderText = "Weight"
                    gvReceipt.Columns("Receipt_Net_Weight").HeaderText = "Receipt Weight"
                    gvReceipt.Columns("Receipt_Net_Weight").ReadOnly = False
                    gvReceipt.Columns("fat_per").HeaderText = "FAT %"
                    gvReceipt.Columns("Receipt_fat_per").HeaderText = "Receipt FAT %"
                    gvReceipt.Columns("Receipt_fat_per").ReadOnly = False
                    gvReceipt.Columns("fat_KG").HeaderText = "FAT Kg"
                    gvReceipt.Columns("Receipt_fat_KG").HeaderText = "Receipt FAT Kg"
                    gvReceipt.Columns("snf_Per").HeaderText = "SNF %"
                    gvReceipt.Columns("Receipt_snf_Per").HeaderText = "Receipt SNF %"
                    gvReceipt.Columns("Receipt_snf_Per").ReadOnly = False
                    gvReceipt.Columns("SNF_KG").HeaderText = "SNF Kg"
                    gvReceipt.Columns("Receipt_SNF_KG").HeaderText = "Receipt SNF Kg"

                    gvReceipt.BestFitColumns()
                End If
            End If
        Else
            Reset()
        End If
        isInsideLoadData = False

        btnSave.Text = "Update"

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



        '=====================if document go for approval then no post button visible or if document contain related setting
        If AllowNLevel Then
            btnPost.Visible = MyBase.isPostFlag

            If Not clsApply_Approval.Visibility_PostButtonForApproval(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(fndDocNo.Value), clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value), 0, "", objApproval) Then
                btnPost.Visible = False
                If lblPending.Status = ERPTransactionStatus.Pending Then
                    lblPending.Status = clsApply_Approval.ApprovalCondCheck_Doc(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(fndDocNo.Value), Nothing)
                End If
            End If
        End If
        '============================================

    End Sub


    Private Sub fndPriceChart__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPriceChart._MYValidating
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
        If clsCommon.myLen(txtVendor.Text) > 0 Then
            If clsCommon.myCdbl(txtManualPrice.Text) > 0 Then
                whrcls = " TSPL_Bulk_Price_MASTER.Price_Code in (select PriceCode  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & txtVendor.Text & "') "
                If BulkProcPricePostedData = True Then
                    whrcls += " and Posted='1'"
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please enter Manual Price ", Me.Text)
                Exit Sub
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select Job work Location ", Me.Text)
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
            Else
                TxtFatWeightage.Text = "0"
                TxtSNFWeightage.Text = "0"
                txtfatPercentage.Text = "0"
                txtSNFPercentage.Text = "0"
                txtStanadardrate.Text = "0"
                txtTolerance.Value = "0"
                fndTankerNo.Enabled = True
            End If
        End If
        UpdateTotal()
    End Sub


    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colItemCode) Then
                        gvItem.CurrentRow.Cells(colItemCode).Value = clsItemMaster.getFinder("Product_Type ='mi'", gvItem.CurrentRow.Cells(colItemCode).Value, False)
                        If clsCommon.myLen(gvItem.CurrentRow.Cells(colItemCode).Value) > 0 Then
                            gvItem.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsItemMaster.GetItemName(gvItem.CurrentRow.Cells(colItemCode).Value, Nothing))
                            gvItem.CurrentRow.Cells(colItemHSNCode).Value = clsItemMaster.GetItemHSNCode(gvItem.CurrentRow.Cells(colItemCode).Value, Nothing)
                            gvItem.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & gvItem.CurrentRow.Cells(colItemCode).Value & "' and Default_UOM='1' "))
                            If clsCommon.myLen(gvItem.CurrentRow.Cells(colUOM).Value) <= 0 Then
                                gvItem.CurrentRow.Cells(colUOM).Value = clsItemMaster.GetStockUnit(gvItem.CurrentRow.Cells(colItemCode).Value, Nothing)
                            End If
                        Else
                            gvItem.CurrentRow.Cells(colItemDesc).Value = ""
                            gvItem.CurrentRow.Cells(colUOM).Value = ""
                        End If
                    ElseIf e.Column Is gvItem.Columns(colSubLocationCode) Then
                        OpenLocation(False)
                        'Dim qry As String = "select * from ( select Location_Code as [Code],Location_Desc as [Name] from TSPL_Location_MASTER left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "' and Location_Type='Physical' and isnull(Is_Jobwork,0)=0)x "
                        'gvItem.CurrentRow.Cells(colSubLocationCode).Value = clsCommon.ShowSelectForm("sublc@jwtm", qry, "Code", "", clsCommon.myCstr(gvItem.CurrentRow.Cells(colSubLocationCode).Value), "Code", False)
                        'gvItem.CurrentRow.Cells(colSubLocationName).Value = clsLocation.GetName(clsCommon.myCstr(gvItem.CurrentRow.Cells(colSubLocationCode).Value), Nothing)
                    ElseIf e.Column Is gvItem.Columns(colTareWeight) OrElse e.Column Is gvItem.Columns(colGrossWeight) OrElse e.Column Is gvItem.Columns(colFat) OrElse e.Column Is gvItem.Columns(colSNF) Then
                        UpdateTotal()
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub OpenLocation(ByVal isButtonClicked As Boolean)
        Dim intRow As Integer = gvItem.CurrentRow.Index
        Dim oldIcode As String = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemCode).Value)
        Dim oldFrmLoc As String = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colSubLocationCode).Value)
        Dim loc_type As Integer = 1
        Dim frm As New FrmPPIssueChildScrren()
        frm.LoadData(dtpSRNDATE.Value, clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemCode).Value), clsCommon.myCstr(gvItem.Rows(intRow).Cells(colUOM).Value), txtLocation.Value, "Milk", loc_type, Nothing, dtpSRNDATE.Value)
        frm.ShowDialog()
        If frm.Arr_Loc IsNot Nothing AndAlso frm.Arr_Loc.Count > 0 Then
            Dim ii As Integer = intRow
            For Each Loc_Code As String In frm.Arr_Loc
                If intRow <> ii Then
                    gvItem.Rows(ii).Cells(colItemCode).Value = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemCode).Value)
                    gvItem.Rows(ii).Cells(colItemDesc).Value = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemDesc).Value)
                    gvItem.Rows(ii).Cells(colItemHSNCode).Value = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colItemHSNCode).Value)
                    gvItem.Rows(ii).Cells(colUOM).Value = clsCommon.myCstr(gvItem.Rows(intRow).Cells(colUOM).Value)
                End If
                gvItem.Rows(ii).Cells(colSubLocationCode).Value = Loc_Code
                gvItem.Rows(ii).Cells(colSubLocationName).Value = clsLocation.GetName(Loc_Code, Nothing)
                If intRow <> ii Then
                    gvItem.Rows.Move(ii, intRow + 1)
                End If
                gvItem.Rows.AddNew()
                ii = gvItem.Rows.Count - 1
            Next
        Else
            gvItem.Rows(intRow).Cells(colSubLocationCode).Value = ""
            gvItem.Rows(intRow).Cells(colSubLocationName).Value = ""
            gvItem.Rows(intRow).Cells(colFat).Value = Nothing
            gvItem.Rows(intRow).Cells(colFatKG).Value = Nothing
            gvItem.Rows(intRow).Cells(colFatRate).Value = Nothing
            gvItem.Rows(intRow).Cells(colFatAmt).Value = Nothing
            gvItem.Rows(intRow).Cells(colSNF).Value = Nothing
            gvItem.Rows(intRow).Cells(colSNFKG).Value = Nothing
            gvItem.Rows(intRow).Cells(colSNFRate).Value = Nothing
            gvItem.Rows(intRow).Cells(colSNFAmt).Value = Nothing
        End If
        For ii As Integer = 0 To gv_qc.Rows.Count - 1
            gv_qc.CurrentRow = gv_qc.Rows(ii)
        Next
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsMilkJobworkTransfer.ReverseAndUnpost(fndDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub gvItem_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvItem.CurrentColumnChanged
        If gvItem.RowCount > 0 Then
            Dim intCurrRow As Integer = gvItem.CurrentRow.Index
            gvItem.CurrentRow.Cells(colSlNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvItem.Rows.Count - 1 Then
                gvItem.Rows.AddNew()
                gvItem.CurrentRow = gvItem.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvItem_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles gvItem.Validating

    End Sub

    Private Sub btnPrintPO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintPO.Click
        printPoData()
    End Sub
    Sub printPoData()
        clsCommon.MyMessageBoxShow(Me, "No Print Format Found")
    End Sub


    Sub SetParameterRange(ByVal intRow As Integer)
        Try
            loadBlankParameterGridwithRange()
            Dim whrCls As String = String.Empty
            If clsERPFuncationality.isLocationMcc(txtLocation.Value) Then
                whrCls = " and Param_for='MCC' or Param_for='BOTH'"
            Else
                whrCls = " and (Param_for='PLANT' or Param_for='BOTH')"
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
                    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Value & "'   and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "' " &
                        " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & txtLocation.Value & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "
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
                    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Value & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "' " &
                      " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & txtLocation.Value & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "

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
                    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Value & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'" &
                         " AND Effective_Date IN ( select Effective_Date from (select  rank() over(order by effective_Date Desc) as SLNo,* from TSPL_PARAMETER_RANGE_MASTER where loc_code='" & txtLocation.Value & "'  and MIKL_TYPE_CODE='" & gvItem.Rows(intRow - 1).Cells(colMilkTtypeCode).Value & "'  and Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'   )xx where slno=1  ) order by Effective_Date desc  "

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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub gvParam_CurrentRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles gvParam.CurrentRowChanged
        If TankerFromMaster = 1 Then
            If gvItem.CurrentRow IsNot Nothing Then
                SetParameterRange(gvParam.CurrentRow.Index)
            End If
        End If

    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim strLocations = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strLocations = "and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        txtLocation.Value = clsLocation.getFinder("(type='PLANT' or Location_category='MCC')" & strLocations, txtLocation.Value, isButtonClicked)
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocationDesc.Text = clsLocation.GetName(txtLocation.Value, Nothing)
        Else
            lblLocationDesc.Text = ""
        End If
        strLocations = Nothing
    End Sub

    Private Sub txtJobWork__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtJobWork._MYValidating
        Dim strLocations = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strLocations = "and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        If clsCommon.myLen(txtLocation.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select location code before sub location", Me.Text)
            Exit Sub
        End If
        txtJobWork.Value = clsLocation.getFinder("(Main_Location_Code='" & txtLocation.Value & "' and Is_Jobwork=1 and isnull(Is_Sub_Location,'N')='Y')" & strLocations, txtJobWork.Value, isButtonClicked)
        If clsCommon.myLen(txtJobWork.Value) > 0 Then
            lblJobWork.Text = clsLocation.GetName(txtJobWork.Value, Nothing)
            txtVendor.Text = clsDBFuncationality.getSingleValue("select Jobwork_Vendor from TSPL_LOCATION_MASTER WHERE Location_Code='" & txtJobWork.Value & "'")
            If clsCommon.myLen(txtJobWork.Value) > 0 Then
                lblVendorName.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER WHERE Vendor_Code='" & txtVendor.Text & "'")
            Else
                lblVendorName.Text = ""
            End If
        Else
            lblJobWork.Text = ""
            txtVendor.Text = ""
        End If
        strLocations = Nothing


    End Sub

    Private Sub txtManualPrice_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtManualPrice.Validating
        UpdateTotal()
    End Sub

    Private Sub fndDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "  loc_code in  (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            fndDocNo.Value = clsMilkJobworkTransfer.getFinder(whrCls, fndDocNo.Value, isButtonClicked)
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                LoadData(fndDocNo.Value, NavigatorType.Current)
            Else
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(fndDocNo.Value)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            Dim Arr As New List(Of clsMilkJobworkTransferDetails)
            For ii As Integer = 0 To gvReceipt.Rows.Count - 1
                UpdateReciptRow(ii)
                Dim obj As New clsMilkJobworkTransferDetails
                obj.Line_No = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Line_No").Value)
                obj.Item_Code = clsCommon.myCstr(gvReceipt.Rows(ii).Cells("Item_Code").Value)
                obj.UOM = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("UOM").Value)
                obj.Receipt_Net_Weight = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_Net_Weight").Value)
                obj.Receipt_snf_Per = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_snf_Per").Value)
                obj.Receipt_fat_per = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_fat_per").Value)
                obj.Receipt_fat_KG = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_fat_KG").Value)
                obj.Receipt_SNF_KG = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_SNF_KG").Value)
                Arr.Add(obj)
            Next
            If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                clsMilkJobworkTransferDetails.UpdateReceiptDetail(fndDocNo.Value, Arr)
                clsCommon.MyMessageBoxShow(Me, "Receipt Data Updated successfully", Me.Text)
            Else
                Throw New Exception("No Receipt Data found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Dim isInsideLoadDataReceipt As Boolean = False
    Private Sub gvReceipt_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvReceipt.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isInsideLoadDataReceipt Then
                    isInsideLoadDataReceipt = True
                    If e.Column Is gvReceipt.Columns("Receipt_Net_Weight") OrElse e.Column Is gvReceipt.Columns("Receipt_fat_per") OrElse e.Column Is gvReceipt.Columns("Receipt_snf_Per") Then
                        UpdateReciptRow(gvReceipt.CurrentRow.Index)
                    End If
                    isInsideLoadDataReceipt = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isInsideLoadDataReceipt = False
        End Try
    End Sub

    Sub UpdateReciptRow(ByVal ii As Integer)
        Dim dblNet As Decimal = clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_Net_Weight").Value)
        gvReceipt.Rows(ii).Cells("Receipt_fat_KG").Value = MyMath.RoundDown(dblNet * MyMath.RoundDown(clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_fat_per").Value), 2) / 100, 3)
        gvReceipt.Rows(ii).Cells("Receipt_SNF_KG").Value = MyMath.RoundDown(dblNet * MyMath.RoundDown(clsCommon.myCdbl(gvReceipt.Rows(ii).Cells("Receipt_snf_Per").Value), 2) / 100, 3)
    End Sub
End Class
