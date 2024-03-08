Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports Telerik.WinControls
Imports System.Windows.Forms
Imports Telerik.WinControls.UI

Public Class FrmMccDispatch
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isdipmarkingmendatory As Boolean = False
    Public isLoadingBulkSaleTanker As Boolean = False
    Public strDocNo As String = ""
    Public Const colSlNo As String = "SlNo"
    Public Const colSealNo As String = "SealNo"
    Public Const colAutoFat As String = "AutoFat"
    Public Const colAutoSnf As String = "AutoSnf"
    Public Const colAutoClr As String = "AutoClr"
    Const colACCode As String = "NAME"
    Public Const colFatKG As String = "colFatKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colFatRate As String = "colFatRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colAmount As String = "colAmount"
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
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim AllowFractionInMCCTankerDispatchGrossQty As Boolean = False
#End Region

    Private Sub FrmMccDispatch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_FIXED_PARAMETER_PROGRAM_MAPPING where FP_Type='Manualy Enter Transfer Price'")
        clsDBFuncationality.ExecuteNonQuery("delete from TSPL_FIXED_PARAMETER where type='Manualy Enter Transfer Price'")
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
        pnlGTWeight.Enabled = True

        ''=====Sanjeet========
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            chkJobWork.Visible = True
        End If
        '=======
        AllowFractionInMCCTankerDispatchGrossQty = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowFractionInMCCTankerDispatchGrossQty, clsFixedParameterCode.AllowFractionInMCCTankerDispatchGrossQty, Nothing)) = 1, True, False)
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
        '   repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
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
        '   repoCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
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
        fndMCCCode.Enabled = True
        isLoad = False
        fndMCCCode.Value = clsGateEntry.getUsersDefaultLocation()
        txtMCCName.Text = clsLocation.GetName(clsGateEntry.getUsersDefaultLocation(), Nothing)
        txtTotalAmount.TextAlign = HorizontalAlignment.Right
        'btnRefresh.Anchor = AnchorStyles.Left
        'fndMCCCode.Value = getCurrentLoggedInMccCode()
        'txtMCCName.Text = getCurrentLoggedInMccName()
        'If clsCommon.CompairString(txtMCCName.Text, "NA") = CompairStringResult.Equal Then
        '    clsCommon.MyMessageBoxShow("Current User is not of type MCC, Contact to administrator and Map current User Default Location to any MCC")
        '    Exit Sub
        'End If
        GetECOPro()
        txtTransferPrice.Text = ""
        dtpDateAndTime.Value = clsCommon.GETSERVERDATE()

        txtEWayBillNo.Text = ""
        txtEWayBillDate.Value = dtpDateAndTime.Value
        txtEWayBillDate.Checked = False
        txtElectronicRefNo.Text = ""

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
        ddlControlSample.Text = "YES"
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
        resetSealNo()
        ResetParameterGrid()
        loadBlankGridManualSeal()
        loadBlankGridPaperSeal()
        ''=============Richa Ticket No. BM00000003712 on 08/09/2014
        txtTankerKMReading.Text = 0
        ''===============================================
        resetQualityParameters()
        txtTareWeight.Text = ""
        txtGrossWeight.Text = ""
        txtNetQty.Text = ""

        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnReverse.Enabled = False
        btnPrint.Enabled = False
        btnSave.Enabled = True
        btnClose.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        fndItemCode.Value = ""
        txtItemDesc.Text = ""
        fndTnakerNo.Focus()
        objSr.SetPortNameValues(cboComPort)
        cboECOPro.SelectedValue = 0 'Nothing
        clsPortSetting.GetMachineType(CboMachine)
        fndItemCode.Value = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing))
        txtItemDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & fndItemCode.Value & "'"))
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
        txtRemarks.Text = ""
        txtRemarks.Enabled = True
        txtRemarks.ReadOnly = False
        fndUOM.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItemCode.Value & "' and Default_UOM='1' "))

        If clsCommon.myLen(fndUOM.Value) <= 0 Then
            fndUOM.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Code   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItemCode.Value & "' and Stocking_Unit='Y' "))
            txtUOMdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select UOM_Description   from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndItemCode.Value & "' and Stocking_Unit='Y' "))
        End If
        txtUOMdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Unit_Desc  FROM TSPL_UNIT_MASTER  WHERE Unit_Code='" & fndUOM.Value & "'"))
        Dim isUOMEditable As Boolean = IIf(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.IsUOMSelectableOnMCCDispatch, Nothing) = "1", True, False)
        fndUOM.Enabled = isUOMEditable
        fndPriceChart.Value = ""
        TxtFatWeightage.Text = ""
        TxtSNFWeightage.Text = ""
        txtfatPercentage.Text = ""
        txtSNFPercentage.Text = ""
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
        txtTareWeight.ReadOnly = False
        txtTareWeight.Enabled = True
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
            UcCustomFields1.SetDefaultValues()
        End If

        fndPriceChart.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Price_Code  from TSPL_Bulk_Price_MASTER  where   isdefaultForTankerDispatch=1 "))
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

            End If
        Else
            TxtFatWeightage.Text = ""
            TxtSNFWeightage.Text = ""
            txtfatPercentage.Text = ""
            txtSNFPercentage.Text = ""
        End If
        chkJobWork.Checked = False
        Panel3.Visible = False
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        calculateAmount()



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
        If clsCommon.myLen(fndPriceChart.Value) > 0 AndAlso clsCommon.myLen(txtTransferPrice.Text) > 0 AndAlso clsCommon.myCdbl(txtTransferPrice.Text) > 0 AndAlso (clsCommon.myCdbl(TxtFatWeightage.Text) + clsCommon.myCdbl(TxtSNFWeightage.Text)) = 100 AndAlso clsCommon.myCdbl(txtSNFPercentage.Text) > 0 AndAlso clsCommon.myCdbl(txtfatPercentage.Text) > 0 AndAlso clsCommon.myLen(FATColName) > 0 AndAlso clsCommon.myLen(SNFColName) > 0 And clsCommon.myCdbl(txtNetQty.Text) >= 0 Then
            Try
                gv.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(txtNetQty.Text) * clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value) / 100
                gv.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(txtNetQty.Text) * clsCommon.myCdbl(gv.Rows(0).Cells(SNFColName).Value) / 100
                FatW = clsCommon.myCdbl(TxtFatWeightage.Text)
                SNfW = clsCommon.myCdbl(TxtSNFWeightage.Text)
                FATRatio = clsCommon.myCdbl(txtfatPercentage.Text)
                SNFRatio = clsCommon.myCdbl(txtSNFPercentage.Text)
                fatKG = clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value)
                snfKG = clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value)
                'sanjay
                'FATRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Text) * FatW / FATRatio, 2)
                'SNFRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Text) * SNfW / SNFRatio, 2)
                'FATValue = Math.Round(fatKG * FATRate, 2)
                'SNfValue = Math.Round(snfKG * SNFRate, 2)
                'gv.Rows(0).Cells(colFatRate).Value = Math.Round(FATRate, 2)
                'gv.Rows(0).Cells(colSNFRate).Value = Math.Round(SNFRate, 2)

                FATRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Text) * FatW / FATRatio, 4)
                SNFRate = Math.Round(clsCommon.myCdbl(txtTransferPrice.Text) * SNfW / SNFRatio, 4)
                FATValue = Math.Round(fatKG * FATRate, 4)
                SNfValue = Math.Round(snfKG * SNFRate, 4)
                gv.Rows(0).Cells(colFatRate).Value = Math.Round(FATRate, 4)
                gv.Rows(0).Cells(colSNFRate).Value = Math.Round(SNFRate, 4)
                'sanjay
                gv.Rows(0).Cells(colAmount).Value = Math.Round((FATRate * fatKG) + (SNFRate * snfKG), 2)
            Catch ex As Exception
            End Try
        End If
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

    Sub ResetParameterGrid()
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
        gv.Columns.Add("QtyInKg", "Qty In Kg")
        gv.Columns("QtyInKg").Width = 60
        gv.Columns("QtyInKg").ReadOnly = True
        gv.Columns("QtyInKg").Tag = "QtyInKg"
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
                        repoDecimalColumn.FormatString = "{0:n2}"
                        repoDecimalColumn.DecimalPlaces = 2
                    End If

                    repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                    repoDecimalColumn.Tag = dt.Rows(i)("Type")
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal AndAlso (Not clsERPFuncationality.isLocationMcc(fndMCCCode.Value)) Then
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
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    '    repoComboColumn.ReadOnly = True
                    'Else
                    repoComboColumn.ReadOnly = False
                    'End If
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
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    '    repoComboColumn.ReadOnly = True
                    'Else
                    repoComboColumn.ReadOnly = False
                    'End If
                    gv.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    'If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    '    repoTextColumn.ReadOnly = True
                    'Else
                    repoTextColumn.ReadOnly = False
                    'End If
                    gv.MasterTemplate.Columns.Add(repoTextColumn)
                End If

                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    gv.Columns.Add(colAutoFat, "Auto FAT")
                    gv.Columns(colAutoFat).Width = 120
                    gv.Columns(colAutoFat).ReadOnly = True
                    gv.Columns(colAutoFat).Tag = "AutoFAT"
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    gv.Columns.Add(colAutoSnf, "Auto SNF")
                    gv.Columns(colAutoSnf).Width = 120
                    gv.Columns(colAutoSnf).ReadOnly = True
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
            gv.Columns.Add("colRemarks", "Remarks")
            gv.Columns("colRemarks").Width = 300
            gv.Columns("colRemarks").ReadOnly = False
            gv.Columns("colRemarks").Tag = "REM"
            gv.Columns("colRemarks").WrapText = True
        Else
            Throw New Exception("There is No parameter defined in Parameter Master. Please Define It First")
        End If
        gv.Rows.AddNew()
        'gv.Rows(0).Cells("colSLNO").Value = "1"
        If clsCommon.myLen(CfColName) > 0 Then
            Try
                gv.Rows(0).Cells(CfColName).Value = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
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
        'clsCustomFieldGrid.LoadBlankGrid(gv, MyBase.ArrDetailFields)
        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.AllowRowReorder = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = False
        gv.EnableSorting = False
        gv.EnableGrouping = False
        'gv.AutoSizeRows = True
        gv.AllowColumnChooser = True
        gv.AllowColumnReorder = True
        ReStoreGridLayout()
        isCellValueChangedOpen = False

        repoTextColumn = Nothing
        repoDecimalColumn = Nothing
        repoComboColumn = Nothing
    End Sub

    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim dt As DataTable

        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "

        dt = clsDBFuncationality.GetDataTable(qry)


        Return dt
    End Function

    Sub resetSealNo()
        txtSealNo1.Text = ""
        txtSealNo2.Text = ""
        txtSealNo3.Text = ""
        txtSealNo4.Text = ""
        txtSealNo5.Text = ""
        txtSealNo6.Text = ""
        txtSealNo7.Text = ""
        txtSealNo8.Text = ""
        txtSealNo9.Text = ""
        txtSealNo10.Text = ""
    End Sub

    Sub resetQualityParameters()
        'txtQtyInKG.Text = ""
        'txtFATPer.Text = ""
        'txtSNFPer.Text = ""
        'txtAcidity.Text = ""
        'txtALTest.Text = ""
        'txtFlavour.Text = ""
        'txtMBRT.Text = ""
        'txtRM.Text = ""
        'txtOtherTest.Text = ""
        'txtTempDegreeC.Text = ""
        'txtTaste.Text = ""
    End Sub

    Private Sub fndPlantOrMCCCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndPlantOrMCCCode._MYValidating
        Dim qry As String = String.Empty
        Dim TaxType As String = ""
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select MCC Name from which dispatch is being made, First", Me.Text)
            Exit Sub
        End If
        If clsCommon.myLen(fndChalanNo.Value) > 0 Then
            If clsERPFuncationality.GetGSTStatus(dtpDateAndTime.Value) Then
                Dim strMcc_Or_Plant_Code = clsDBFuncationality.getSingleValue("select  Mcc_Or_Plant_Code from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & fndChalanNo.Value & "'")
                qry = "select State  from TSPL_LOCATION_MASTER where Location_Code='" + fndMCCCode.Value + "'"
                Dim strMCCState = clsDBFuncationality.getSingleValue(qry)

                If clsLocation.GetGSTLocationInterState(fndMCCCode.Value, strMcc_Or_Plant_Code, "T", Nothing) Then
                    TaxType = "I"
                    TaxType = " and TSPL_Location_MASTER.State <> '" & strMCCState & "'"
                Else
                    TaxType = "L"
                    TaxType = " and TSPL_Location_MASTER.State = '" & strMCCState & "'"
                End If
            End If
        End If

        If clsCommon.myLen(ddlTankerDispatchTo.Text) > 0 Then
            If clsCommon.CompairString(ddlTankerDispatchTo.Text, "PLANT") = CompairStringResult.Equal Then
                fndPlantOrMCCCode.Value = clsLocation.getFinder("Type='" & ddlTankerDispatchTo.Text & "' and location_code <>'" & fndMCCCode.Value & "' " & TaxType & "", fndPlantOrMCCCode.Value, isButtonClicked)
            ElseIf clsCommon.CompairString(ddlTankerDispatchTo.Text, "MCC") = CompairStringResult.Equal Then
                fndPlantOrMCCCode.Value = clsLocation.getFinder("Location_Category='" & ddlTankerDispatchTo.Text & "' and location_code <>'" & fndMCCCode.Value & "'  " & TaxType & "", fndPlantOrMCCCode.Value, isButtonClicked)
            End If
            If clsCommon.myLen(fndPlantOrMCCCode.Value) > 0 Then
                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please Select ' Tanker Dispatch To  ' type First ", Me.Text)
        End If
    End Sub

    Private Sub fndTnakerNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTnakerNo._MYValidating
        If Not pnlGTWeight.Enabled Then
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
                    txtGrossWeight.Text = clsCommon.myCstr(clsCommon.myCdbl(dr("Gross_Weight")))
                    txtTareWeight.Text = clsCommon.myCstr(clsCommon.myCdbl(dr("Tare_Weight")))
                    txtNetQty.Text = clsCommon.myCstr(clsCommon.myCdbl(dr("Net_Weight")))
                    lblWeighmentNo.Text = clsCommon.myCstr(dr("Document_No"))
                End If
            End If


        ElseIf Not isLoadingBulkSaleTanker Then
            Dim qry As String = " select distinct TankerNo  ,[Tanker Transporter Code],[Tanker Transporter Name]  from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where isGateOut=1  " & _
                "union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='" & fndTnakerNo.Value & "'  AND isGateOut=1 AND ISNULL(Ref_Doc_No,'')='' " & _
                "union all select TSPL_MCC_Dispatch_Challan.Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master LEFT OUTER JOIN TSPL_MCC_Dispatch_Challan ON  tspl_tanker_master.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No where TSPL_MCC_Dispatch_Challan.Chalan_NO='" & fndChalanNo.Value & "' )xx  "
            Dim whrCls As String = "  "
            If chkShowIntermittentOnly.Checked = True Then
                qry = "  select distinct TankerNo  ,[Tanker Transporter Code],[Tanker Transporter Name]  from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where isGateOut=1 and Tanker_No in (select Tanker_No  from TSPL_MCC_Dispatch_Challan where  isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1)  " & _
                "union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='" & fndTnakerNo.Value & "'  AND isGateOut=1 AND ISNULL(Ref_Doc_No,'')='' " & _
                "union all select TSPL_MCC_Dispatch_Challan.Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master LEFT OUTER JOIN TSPL_MCC_Dispatch_Challan ON  tspl_tanker_master.Tanker_No=TSPL_MCC_Dispatch_Challan.Tanker_No where TSPL_MCC_Dispatch_Challan.Chalan_NO='" & fndChalanNo.Value & "' )xx  "
            End If
            fndTnakerNo.Value = clsCommon.ShowSelectForm("TNKRNO", qry, "TankerNo", whrCls, fndTnakerNo.Value, "TankerNo", isButtonClicked)
            txtTankerTransporterName.Text = getTransporterName(fndTnakerNo.Value)
            ''richa Against Ticket No. BM00000003713 on 03/09/2014
            '' Pankaj jha against ticket no: BM00000003870 on 11-09-2014
            SetIntermittentDetails(fndTnakerNo.Value)
        End If
        If clsCommon.myLen(fndTnakerNo.Value) > 0 AndAlso clsCommon.myCdbl(txtNetQty.Text) > 0 Then
            Dim Storagecapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select Storage_Capacity  from TSPL_TANKER_MASTER WHERE Tanker_No ='" + fndTnakerNo.Value + "'"))
            Dim nq As Double = clsCommon.myCdbl(txtNetQty.Text)
            Dim totPer As Double = nq * 100 / Storagecapacity
            If totPer >= 95 Then
                ddlTankerFull.Text = "Yes"
            Else
                ddlTankerFull.Text = "No"
            End If
        Else
            ddlTankerFull.Text = "No"
        End If


    End Sub

    Sub SetIntermittentDetails(ByVal strTankerNo As String)
        If Not isLoad Then
            Dim qry As String = ""
            Dim isInterMittent As Boolean = False
            Dim nextLevel As Double = 0
            Dim FinalLoc As String = ""
            Dim Level1ChallanNo As String = ""

            qry = " select count(*) from  ( select distinct TankerNo    from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where isGateOut=1 and Tanker_No in (select Tanker_No  from TSPL_MCC_Dispatch_Challan where  isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1) union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='')xx  ) yyyy where yyyy.TankerNo='" & strTankerNo & "' "
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0 Then
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

                qry = "  select MAX(ISNULL(currentlevel,0))+1 from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' "
                nextLevel = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

                ddlLevel.SelectedValue = CInt(nextLevel).ToString()
                If nextLevel >= 2 Then
                    txtTareWeight.ReadOnly = True
                    'ddlLevel.Enabled = False
                    'fndFinalLoc.Enabled = False
                    'fndLevel1ChallanNo.Enabled = False
                    txtTareWeight.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select tare_weight  from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1'"))
                    ''richa agarwal 07 Sep, 2016
                    Dim Strqry As String = " select Mcc_Or_Plant_Code  from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' "

                    If nextLevel = 2 Then
                        Strqry += " and CurrentLevel='1' "
                    ElseIf nextLevel = 3 Then
                        Strqry += " and CurrentLevel='2' "
                    Else
                        Strqry += " and CurrentLevel='3' "
                    End If
                    fndMCCCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Strqry))
                    fndMCCCode.Enabled = False
                    txtMCCName.Text = clsLocation.GetName(fndMCCCode.Value, Nothing)

                    ''----------------------
                Else
                    txtTareWeight.ReadOnly = False
                    'ddlLevel.Enabled = True
                    'fndFinalLoc.Enabled = True
                    'fndLevel1ChallanNo.Enabled = True
                    txtTareWeight.Text = ""
                End If


                qry = "  select finalLoc from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1'"
                FinalLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                fndFinalLoc.Value = FinalLoc
                txtFinalLoc.Text = clsLocation.GetName(FinalLoc, Nothing)



                qry = "  select Chalan_NO  from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1'"
                Level1ChallanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                fndLevel1ChallanNo.Value = Level1ChallanNo
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
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F3 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "sirc"
            frm.strCode = "sireversandcreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        ElseIf e.KeyCode = Keys.F8 Then
            If gv.Rows.Count > 0 Then
                gv.Rows(0).Cells(colAutoFat).Value = LblSnf.Text
                gv.Rows(0).Cells(colAutoSnf).Value = LblFAT.Text
                Try
                    If clsCommon.myLen(CfColName) > 0 Then

                        gv.Rows(0).Cells(colAutoClr).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(LblFAT.Text), clsCommon.myCdbl(LblSnf.Text), clsCommon.myCdbl(gv.Rows(0).Cells(CfColName).Value))
                    Else
                        gv.Rows(0).Cells(colAutoClr).Value = "0"

                    End If
                Catch exx As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMCCDispatch)
        If Not (MyBase.isReadFlag) Then
            '  If MDI.blnShowAllMenu = False Then
            Throw New Exception("Permission Denied")
            '   Else
            'Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
            'End If
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
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
            ' KUNAL > TICKET : BM00000009575 =======
            If AllowFutureDateTransaction(dtpDateAndTime.Value, Nothing) = False Then
                dtpDateAndTime.Focus()
                Return False
            End If

            'If clsCommon.CompairString(txtMCCName.Text, "NA") = CompairStringResult.Equal Or clsCommon.myLen(txtMCCName.Text) = 0 Then
            '    errorControl.SetError(txtMCCName, "You are not Logged in As MCC ")
            '    Throw New Exception("You are not Logged in As MCC   ")
            'Else
            '    errorControl.SetError(txtMCCName, "")
            'End If
            '' check for tanker
            If clsMccDispatch.CheckTankerGateOut(fndTnakerNo.Value, fndChalanNo.Value) = False Then
                Throw New Exception("Tanker No- " & fndTnakerNo.Value & " is not available for new dispatch.")
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

            If clsCommon.myCdbl(txtGrossWeight.Text) < clsCommon.myCdbl(txtTareWeight.Text) Then
                Throw New Exception("Tare Weight must be less than gross weight")
                txtTareWeight.Focus()
            End If

            If clsCommon.myCdbl(txtNetQty.Text) <= 0 Then
                Throw New Exception("Net Weight Must Not be Negative or Zero")
                txtTareWeight.Focus()
            End If

            If clsCommon.myLen(fndPriceChart.Value) <= 0 Then
                errorControl.SetError(fndPriceChart, "Please  select Price Chart")
                Throw New Exception("Please select Price Chart ")
            Else
                errorControl.SetError(fndPriceChart, "")
            End If



            'If isControlSampleMandatory AndAlso clsCommon.CompairString(ddlControlSample.Text, "NO") = CompairStringResult.Equal Then
            '    errorControl.SetError(ddlControlSample, "Please set Control Sample to Yes" & Environment.NewLine & " Due to Control Sample is set to  Mandatory")
            '    Throw New Exception("Please set Control Sample to Yes" & Environment.NewLine & " Due to Control Sample is set to  Mandatory")
            'Else
            '    errorControl.SetError(ddlControlSample, "")
            'End If

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

                'fracValue = clsCommon.myCdbl(txtControlSampleSNF.Text)
                'fracValue = Math.Round((fracValue - CInt(fracValue)) * 100, 2)
                'If CInt(fracValue) Mod 5 <> 0 Then
                '    Throw New Exception("Control Sample SNF value, must have its decimal part multiple of 5")
                '    txtControlSampleSNF.Focus()
                'End If


            End If
            Dim fracValue1 As Double = 0
            If clsCommon.myLen(FATColName) > 0 Then
                fracValue1 = clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value)
                fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                If CInt(fracValue1) Mod 5 <> 0 Then
                    Throw New Exception(" FAT value in Grid, must have its decimal part multiple of 5")
                End If
            End If

            'If clsCommon.myLen(SNFColName) > 0 Then
            '    fracValue1 = clsCommon.myCdbl(gv.Rows(0).Cells(SNFColName).Value)
            '    fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
            '    If CInt(fracValue1) Mod 5 <> 0 Then
            '        Throw New Exception(" SNF value in Grid, must have its decimal part multiple of 5")
            '    End If
            'End If
            ''=============Richa Ticket No. BM00000003712 on 08/09/2014
            If clsCommon.myCdbl(txtTankerKMReading.Text) <= 0 Then
                txtTankerKMReading.Focus()
                errorControl.SetError(txtTankerKMReading, "Please Enter Tanker KM Reading")
                Throw New Exception("Please Enter Tanker KM Reading")
            Else
                errorControl.SetError(txtTankerKMReading, "")
            End If
            If isdipmarkingmendatory Then
                If clsCommon.myLen(txtDripMarking.Text) = 0 Then
                    txtDripMarking.Focus()
                    errorControl.SetError(txtDripMarking, "Please Enter Dip Marking")
                    Throw New Exception("Please Enter Dip Marking")
                Else
                    errorControl.SetError(txtDripMarking, "")
                End If
            End If

            ''===============================================
            If clsCommon.myCdbl(txtNetQty.Text) < 0 Then
                txtNetQty.Focus()
                errorControl.SetError(txtNetQty, "Please Fill Valid Vehicle Gross Weight and Vehicle Tare Weight ")
                Throw New Exception("Please Fill Valid Vehicle Gross Weight and Vehicle Tare Weight")
            Else
                errorControl.SetError(txtNetQty, "")
            End If

            If clsCommon.myCdbl(txtTareWeight.Text) <= 0 Then
                txtTareWeight.Focus()
                errorControl.SetError(txtTareWeight, "Please Fill Vehicle Tare Weight It Can't be Zero Or Negative  ")
                Throw New Exception("Please Fill Vehicle Tare Weight It Can't be Zero or Negative ")
            Else
                errorControl.SetError(txtTareWeight, "")
            End If
            If clsCommon.myCdbl(txtGrossWeight.Text) <= 0 Then
                txtGrossWeight.Focus()
                errorControl.SetError(txtGrossWeight, "Please Fill Vehicle Gross Weight It Can't be Zero Or Negative  ")
                Throw New Exception("Please Fill Vehicle Gross Weight It Can't be Zero Or Negative ")
            Else
                errorControl.SetError(txtGrossWeight, "")
            End If

            If clsCommon.myCdbl(txtTransferPrice.Text) <= 0 Then
                txtTransferPrice.Focus()
                errorControl.SetError(txtTransferPrice, "Please Fill Transfer Price It Can't be Zero Or negative  ")
                Throw New Exception("Please Fill Transfer Price It Can't be Zero or Negaive ")
            Else
                errorControl.SetError(txtTransferPrice, "")
            End If

            If clsCommon.myLen(fndItemCode.Value) = 0 Then
                fndItemCode.Focus()
                errorControl.SetError(fndItemCode, "Please Select an Item. It Is manadatory  ")
                Throw New Exception("Please Select an Item. It Is manadatory  ")
            Else
                errorControl.SetError(fndItemCode, "")
            End If

            '======Sanjeet=====================
            If chkJobWork.Checked Then
                If clsCommon.myLen(txtSubLocation.Value) = 0 Then
                    Throw New Exception("Please Select Sub Location for Location - " & fndPlantOrMCCCode.Value & " ")
                End If
            End If
            '================================
            'Dim fatCnt As Integer = 0
            'For i As Integer = 1 To gv.Columns.Count - 1
            '    If clsCommon.CompairString(gv.Columns(i).Tag, "FAT") = CompairStringResult.Equal Then
            '        fatCnt = fatCnt + 1
            '    End If
            'Next
            'If fatCnt = 0 Then
            '    Throw New Exception("There Must be One Parameter of type FAT. Please Set It From Parameter Master")
            'End If
            'Dim snfCnt As Integer = 0
            'For i As Integer = 1 To gv.Columns.Count - 1
            '    If clsCommon.CompairString(gv.Columns(i).Tag, "SNF") = CompairStringResult.Equal Then
            '        snfCnt = snfCnt + 1
            '    End If
            'Next
            'If snfCnt = 0 Then
            '    Throw New Exception("There Must be One Parameter of type SNF. Please Set It From Parameter Master")
            'End If

            'For i As Integer = 1 To gv.Columns.Count - 1
            '    If clsCommon.myLen(gv.Rows(0).Cells(i).Value) = 0 AndAlso (clsCommon.CompairString(gv.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gv.Columns(i).Tag, "SNF") = CompairStringResult.Equal Or clsCommon.CompairString(gv.Columns(i).Tag, "CLR") = CompairStringResult.Equal) Then
            '        Throw New Exception("In Parameter Grid the Column Named  " & gv.Columns(i).HeaderText & " is of '" & gv.Columns(i).Tag & "'  Type, Therefor It Is Manadatory..Please Fill It")
            '    End If
            'Next

            ''richa Ticket No BM00000003713 on 04/09/2014
            For i As Integer = 1 To gv.Columns.Count - 1
                Dim ismandatoryvalue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select IsMandatory from TSPL_PARAMETER_MASTER where code='" + gv.Columns(i).Name + "'"))
                If ismandatoryvalue > 0 AndAlso clsCommon.myLen(gv.Rows(0).Cells(i).Value) <= 0 AndAlso clsCommon.CompairString(gv.Columns(i).Name, CLRColName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(gv.Columns(i).Name, CfColName) <> CompairStringResult.Equal Then
                    Throw New Exception("" & gv.Columns(i).HeaderText & " is Mandatory, please fill it.")
                End If
            Next
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

            'If clsCommon.CompairString(btnSave.Text, "Update") = CompairStringResult.Equal AndAlso isReversed(fndChalanNo.Value) AndAlso clsCommon.myLen(txtRemarks.Text) <= 0 Then
            '    txtRemarks.Focus()
            '    Throw New Exception(" Please Fill Remarks ")

            'End If
            Dim CurBal As Double = 0
            Dim CurBalOFVL As Double = 0
            Dim CurBalOFML As Double = 0

            Dim CurBal_SNF As Double = 0
            Dim CurBalOFVL_SNF As Double = 0
            Dim CurBalOFML_SNF As Double = 0

            Dim CurBal_FAT As Double = 0
            Dim CurBalOFVL_FAT As Double = 0
            Dim CurBalOFML_FAT As Double = 0

            'If clsCommon.myLen(fndLevel1ChallanNo.Value) > 0 AndAlso clsCommon.myCdbl(ddlLevel.SelectedValue) > 1 Then
            '    ' Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "')"))

            '    'Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No in (Select Chalan_NO  from TSPL_MCC_Dispatch_Challan where Level1ChallanNo  ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "'  and CurrentLevel =" & clsCommon.myCdbl(ddlLevel.SelectedValue) - 1 & " and isnull(isIntermittent,0) =1))"))
            '    'CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))

            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No in (Select Chalan_NO  from TSPL_MCC_Dispatch_Challan where Level1ChallanNo  ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "'  and CurrentLevel =" & clsCommon.myCdbl(ddlLevel.SelectedValue) - 1 & " and isnull(isIntermittent,0) =1))")
            '    Dim strsublocation As String = String.Empty
            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '        For i As Integer = 0 To dt.Rows.Count - 1
            '            strsublocation = clsCommon.myCstr(dt.Rows(i)("Location_Code"))
            '            If clsCommon.myLen(strsublocation) > 0 Then
            '                CurBalOFVL = CurBalOFVL + clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
            '            End If
            '        Next
            '    End If


            '    CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
            '    CurBal = CurBalOFVL + CurBalOFML
            '    ''TO CHECK FAT AND SNF
            '    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing), "1") = CompairStringResult.Equal Then
            '        'Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & strsublocation & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
            '        'If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
            '        '    CurBalOFVL_SNF = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG"))
            '        '    CurBalOFVL_FAT = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG"))
            '        'End If

            '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '            For i As Integer = 0 To dt.Rows.Count - 1
            '                strsublocation = clsCommon.myCstr(dt.Rows(i)("Location_Code"))
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
            '    ''-----------------------


            'Else
            ' ''richa agarwal 17/08/2016
            '' CurBal = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
            '' Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' and Location_Type='Physical'"))
            'Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' "))
            '' CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
            'If clsCommon.myLen(strsublocation) > 0 Then
            '    CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
            'End If
            'CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
            'CurBal = CurBalOFVL + CurBalOFML

            ' ''TO CHECK FAT AND SNF
            'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing), "1") = CompairStringResult.Equal Then
            '    If clsCommon.myLen(strsublocation) > 0 Then
            '        Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & strsublocation & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
            '        If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
            '            CurBalOFVL_SNF = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG"))
            '            CurBalOFVL_FAT = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG"))
            '        End If
            '    End If

            '    Dim DTSNFFAT_MAIN As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & fndMCCCode.Value & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
            '    If DTSNFFAT_MAIN IsNot Nothing And DTSNFFAT_MAIN.Rows.Count > 0 Then
            '        CurBalOFML_SNF = clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_SNF_KG"))
            '        CurBalOFML_FAT = clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_FAT_KG"))
            '    End If
            '    CurBal_SNF = CurBalOFVL_SNF + CurBalOFML_SNF
            '    CurBal_FAT = CurBalOFVL_FAT + CurBalOFML_FAT

            '    If CurBal_SNF < clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) Then
            '        Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) & " ")
            '    End If
            '    If CurBal_FAT < clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) Then
            '        Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) & " ")
            '    End If
            'End If

            ''richa agarwal 02/01/2018
            If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, Nothing), "0") = CompairStringResult.Equal Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' order by TSPL_Location_MASTER.Location_Code ")
                Dim strsublocation As String = String.Empty
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                        If clsCommon.myLen(strsublocation) > 0 Then
                            CurBalOFVL = CurBalOFVL + clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                        End If
                    Next
                End If


                CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                CurBal = CurBalOFVL + CurBalOFML

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

                    If CurBal_SNF < clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) Then
                        Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) & " ")
                    End If
                    If CurBal_FAT < clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) Then
                        Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) & " ")
                    End If
                End If


                'End If
                ''-----------------------



                ''richa agarwal 30/03/2016 BM00000007217
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                    If CurBal > 0 Then
                        CurBal = ClsLoadingTanker.GetTolerane(CurBal, clsCommon.myCdbl(txtNetQty.Text))
                    End If
                End If

                ''--------------------------------

                If CurBal < clsCommon.myCdbl(txtNetQty.Text) Then
                    Throw New Exception("Available Qty is :     " & CurBal & Environment.NewLine & "Required Qty :     " & txtNetQty.Text & " ")

                    'If clsCommon.MyMessageBoxShow("Available Qty is :     " & CurBal & Environment.NewLine & "Required Qty :     " & txtNetQty.Text & Environment.NewLine & "Want To Continue ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                    'Else
                    '    Return False
                    'End If
                End If
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
            calculateAmount()
            If clsCommon.myCdbl(gv.Rows(0).Cells(colFatRate).Value) <= 0 Then
                Throw New Exception("FAT rate is 0 in Grid, Please reselect the price chart or Re-enter FAT % in Grid  ")
            End If
            If clsCommon.myCdbl(gv.Rows(0).Cells(colSNFRate).Value) <= 0 Then
                Throw New Exception("SNF rate is 0 in Grid, Please reselect the price chart or Re-enter SNF/CLR % in Grid  ")
            End If

            If clsCommon.myCdbl(gv.Rows(0).Cells(colAmount).Value) <= 0 Then
                Throw New Exception("Amount is 0 in grid, Please check Price chart, FAT, SNF/CLR in Grid or Re-Enter it")
            End If

            ''----------- richa agarwal 07 Sep, 2016
            If chkIntermittent.Checked = True Then
                If clsCommon.CompairString(fndPlantOrMCCCode.Value, fndFinalLoc.Value) <> CompairStringResult.Equal Then
                    If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select isnull(location_category,'') from tspl_location_master where location_code='" & clsCommon.myCstr(fndPlantOrMCCCode.Value) & "' ")), "MCC") <> CompairStringResult.Equal Then
                        fndPlantOrMCCCode.Focus()
                        Throw New Exception(" Select Plant/MCC should be MCC Type because final Location and Plant/MCC location should not be same.")
                    End If
                End If

                If clsCommon.CompairString(clsCommon.myCstr(ddlLevel.SelectedValue), "3") = CompairStringResult.Equal Then
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
        Dim trans As SqlTransaction = Nothing
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
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            'If obj.isNewEntry Then
            '    obj.Chalan_NO = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dtpDateAndTime.Value, "dd/MMM/yyyy hh:mm tt"), clsDocType.MccDispatchChallan, "", obj.MCC_Code)
            '    If clsCommon.myLen(obj.Chalan_NO) <= 0 Then
            '        Throw New Exception("Error In Challan No Genertion")
            '        ' Exit Sub
            '    End If
            'Else
            '    obj.Chalan_NO = clsCommon.myCstr(fndChalanNo.Value)
            'End If
            'fndChalanNo.Value = obj.Chalan_NO

            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Sublocation_Code = txtSubLocation.Value

            obj.Chalan_NO = fndChalanNo.Value
            obj.MCC_Name = clsCommon.myCstr(txtMCCName.Text)
            obj.Dispatch_Date = clsCommon.myCDate(dtpDateAndTime.Value, "dd/MMM/yyyy hh:mm tt")
            obj.Tanker_Dispatch_To = clsCommon.myCstr(ddlTankerDispatchTo.Text)
            obj.Mcc_Or_Plant_Code = clsCommon.myCstr(fndPlantOrMCCCode.Value)
            obj.Tanker_No = clsCommon.myCstr(fndTnakerNo.Value)
            ''=============Richa Ticket No. BM00000003712 on 08/09/2014
            obj.Tanker_KM_Reading = clsCommon.myCdbl(txtTankerKMReading.Text)
            ''===============================================

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
            obj.Tare_Weight = clsCommon.myCdbl(txtTareWeight.Text)
            obj.Gross_Weight = clsCommon.myCdbl(txtGrossWeight.Text)

            obj.control_sample_fat = clsCommon.myCdbl(txtControlSampleFAT.Text)
            obj.control_sample_snf = clsCommon.myCdbl(txtControlSampleSNF.Text)

            obj.Net_Qty = clsCommon.myCdbl(txtNetQty.Text)
            obj.Transfer_Price = clsCommon.myCdbl(txtTransferPrice.Text)
            obj.Item_Code = clsCommon.myCstr(fndItemCode.Value)
            obj.Item_Desc = clsCommon.myCstr(txtItemDesc.Text)

            obj.Tanker_Transporter_Name = clsCommon.myCstr(txtTankerTransporterName.Text)
            obj.Payment_Type = clsCommon.myCstr(txtPaymentType.Text)
            obj.Payment_Rate = clsCommon.myCstr(txtPaymentRate.Text)
            obj.Charge_For = clsCommon.myCstr(txtChargesFor.Text)
            obj.Payment_Amount = clsCommon.myCdbl(txtTotalAmount.Text)
            obj.Chemist_Code = clsCommon.myCstr(fndChemistCode.Value)
            obj.Chemist_Name = clsCommon.myCstr(txtChemistName.Text)

            obj.UOM_Code = clsCommon.myCstr(fndUOM.Value)
            obj.UOM_desc = clsCommon.myCstr(txtUOMdesc.Text)

            obj.Remarks = clsCommon.myCstr(txtRemarks.Text)



            obj.PriceCode = clsCommon.myCstr(fndPriceChart.Value)
            obj.FAT_W = clsCommon.myCdbl(TxtFatWeightage.Text)
            obj.SNF_W = clsCommon.myCdbl(TxtSNFWeightage.Text)
            obj.FAT_R = clsCommon.myCdbl(txtfatPercentage.Text)
            obj.SNF_R = clsCommon.myCdbl(txtSNFPercentage.Text)
            obj.FAT_RATE = clsCommon.myCdbl(gv.Rows(0).Cells(colFatRate).Value)
            obj.SNF_RATE = clsCommon.myCdbl(gv.Rows(0).Cells(colSNFRate).Value)
            obj.FAT_KG = clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value)
            obj.SNF_KG = clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value)
            obj.Amount = clsCommon.myCdbl(gv.Rows(0).Cells(colAmount).Value)
            obj.MCC_Weighment_No = lblWeighmentNo.Text
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
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.DisAllowIntermittentTankerForPlantDispatch, trans)) = 1 Then
                        Throw New Exception(" Intermittent Tanker dispatch to Plant is not allowed")
                    End If
                    If clsCommon.MyMessageBoxShow(Me, "Do you want to dispatch this tanker as intermittent on Plant ? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                        trans.Rollback()
                        Exit Sub
                    End If
                End If

            Else
                obj.CurrentLevel = 0
                obj.FinalLoc = ""
                fndLevel1ChallanNo.Value = ""
            End If



            If (Not obj.isNewEntry) AndAlso isReversed(obj.Chalan_NO, trans) Then
                obj.isReversed = 1
            Else
                obj.isReversed = 0
            End If


            Dim i As Integer = 0
            Dim objParam As New Mcc_Dispatch_Chalan_Parameter
            obj.arrParmValue = New List(Of Mcc_Dispatch_Chalan_Parameter)
            For i = 0 To gv.Columns.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv.Rows(0).Cells(i).Value).Trim) > 0 Then
                    objParam = New Mcc_Dispatch_Chalan_Parameter
                    objParam.Chalan_No = clsCommon.myCstr(obj.Chalan_NO)
                    objParam.Param_Field_Code = clsCommon.myCstr(gv.Columns(i).Name)
                    objParam.Param_Field_Desc = clsCommon.myCstr(gv.Columns(i).HeaderText)
                    objParam.Param_Field_Value = clsCommon.myCstr(gv.Rows(0).Cells(i).Value)
                    objParam.Param_Type = IIf(clsCommon.CompairString(clsCommon.myCstr(gv.Columns(i).Tag), "") = CompairStringResult.Equal, "NA", clsCommon.myCstr(gv.Columns(i).Tag))
                    obj.arrParmValue.Add(objParam)
                End If
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
            If clsMccDispatch.SaveData(obj, trans) Then
                trans.Commit()
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

            'clsCommon.MyMessageBoxShow("Data Not Saved ")

        Catch ex As Exception

            'If Not isPostbtnClick Then
            If trans IsNot Nothing Then
                trans.Rollback()
            End If
            'End If
            'Reset()
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
                'gv.Rows.Clear()
                'gv.Columns.Clear()
                'gv.DataSource = Nothing
                'Dim pFields As Boolean = True
                'Dim gridWidth As Integer = 60
                ''gv.Columns.Add("QtyInKg", "Qty In Kg")
                ''gv.Columns("QtyInKg").Width = 60
                ''gv.Columns("QtyInKg").ReadOnly = True

                'For i As Integer = 0 To obj.arrParmValue.Count - 1
                '    gv.Columns.Add(obj.arrParmValue(i).Param_Field_Code, obj.arrParmValue(i).Param_Field_Desc)
                '    gv.Columns(obj.arrParmValue(i).Param_Field_Code).Width = 60
                '    gv.Columns(obj.arrParmValue(i).Param_Field_Code).ReadOnly = False
                '    gv.Columns(obj.arrParmValue(i).Param_Field_Code).Tag = obj.arrParmValue(i).Param_Type
                '    gridWidth += 60
                '    If clsCommon.CompairString(obj.arrParmValue(i).Param_Field_Code, "QtyInKg") = CompairStringResult.Equal Then
                '        gv.Columns(obj.arrParmValue(i).Param_Field_Code).ReadOnly = True
                '    End If
                'Next
                'gv.Rows.AddNew()
                'gv.Width = gridWidth + 20
                'gv.Height = 70
                'gv.AllowAddNewRow = False
                'gv.AllowColumnReorder = False
                'gv.AllowDeleteRow = False
                'gv.AllowRowReorder = False
                'gv.ShowGroupPanel = False
                'gv.AllowColumnChooser = False
                'gv.EnableFiltering = False
                'gv.EnableSorting = False
                'gv.EnableGrouping = False

                chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
                txtSubLocation.Value = obj.Sublocation_Code
                If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                    lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
                Else
                    lblSubLocation.Text = ""
                End If
                fndMCCCode.Enabled = False
                fndMCCCode.Value = obj.MCC_Code
                txtMCCName.Text = obj.MCC_Name
                dtpDateAndTime.Value = clsCommon.myCDate(obj.Dispatch_Date, "dd/MM/yyyy hh:mm tt")
                fndChalanNo.Value = clsCommon.myCstr(obj.Chalan_NO)
                ddlTankerDispatchTo.SelectedValue = clsCommon.myCstr(obj.Tanker_Dispatch_To)
                fndPlantOrMCCCode.Value = clsCommon.myCstr(obj.Mcc_Or_Plant_Code)
                txtPlantOrMccName.Text = clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndPlantOrMCCCode.Value & "'")
                isLoadingBulkSaleTanker = obj.isBulkSaleData
                fndTnakerNo.Value = clsCommon.myCstr(obj.Tanker_No)
                ''=============Richa Ticket No. BM00000003712 on 08/09/2014
                txtTankerKMReading.Text = clsCommon.myCdbl(obj.Tanker_KM_Reading)
                ''===============================================

                txtDripMarking.Text = clsCommon.myCstr(obj.Drip_Marking)
                ''richa Against Ticket No. BM00000003713 on 03/09/2014
                ddlTankerFull.Text = clsCommon.myCstr(obj.Tanker_Full)
                ''----------------------------------------------------
                ddlControlSample.SelectedValue = clsCommon.myCstr(obj.Control_Sample)
                txtNameOfCustodian.Text = clsCommon.myCstr(obj.Name_Of_Custodian)
                'txtSealNo1.Text = clsCommon.myCstr(obj.Seal_No1)
                'txtSealNo2.Text = clsCommon.myCstr(obj.Seal_No2)
                'txtSealNo3.Text = clsCommon.myCstr(obj.Seal_No3)
                'txtSealNo4.Text = clsCommon.myCstr(obj.Seal_No4)
                'txtSealNo5.Text = clsCommon.myCstr(obj.Seal_No5)
                'txtSealNo6.Text = clsCommon.myCstr(obj.Seal_No6)
                'txtSealNo7.Text = clsCommon.myCstr(obj.Seal_No7)
                'txtSealNo8.Text = clsCommon.myCstr(obj.Seal_No8)
                'txtSealNo9.Text = clsCommon.myCstr(obj.Seal_No9)
                'txtSealNo10.Text = clsCommon.myCstr(obj.Seal_No10)

                txtEWayBillNo.Text = obj.EWayBillNo
                If obj.EWayBillDate IsNot Nothing Then
                    txtEWayBillDate.Checked = True
                    txtEWayBillDate.Value = obj.EWayBillDate
                End If
                txtElectronicRefNo.Text = obj.Electronic_Ref_No


                ResetParameterGrid()
                For i As Integer = 0 To obj.arrParmValue.Count - 1
                    Try
                        gv.Rows(0).Cells(obj.arrParmValue(i).Param_Field_Code).Value = obj.arrParmValue(i).Param_Field_Value
                    Catch ex1 As Exception
                    End Try
                Next

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

                txtTareWeight.Text = clsCommon.myCdbl(obj.Tare_Weight)
                txtGrossWeight.Text = clsCommon.myCdbl(obj.Gross_Weight)
                txtNetQty.Text = clsCommon.myCdbl(obj.Net_Qty)

                txtControlSampleFAT.Text = clsCommon.myCdbl(obj.control_sample_fat)
                txtControlSampleSNF.Text = clsCommon.myCdbl(obj.control_sample_snf)

                txtTransferPrice.Text = clsCommon.myCdbl(obj.Transfer_Price)
                fndItemCode.Value = clsCommon.myCstr(obj.Item_Code)
                txtItemDesc.Text = clsCommon.myCstr(obj.Item_Desc)

                txtTankerTransporterName.Text = clsCommon.myCstr(obj.Tanker_Transporter_Name)
                txtPaymentType.Text = clsCommon.myCstr(obj.Payment_Type)
                txtPaymentRate.Text = clsCommon.myCstr(obj.Payment_Rate)
                txtChargesFor.Text = clsCommon.myCstr(obj.Charge_For)
                txtTotalAmount.Text = clsCommon.myCdbl(obj.Payment_Amount)

                fndChemistCode.Value = clsCommon.myCstr(obj.Chemist_Code)
                txtChemistName.Text = clsCommon.myCstr(clsEmployeeMaster.GetName(fndChemistCode.Value, Nothing))
                fndChalanNo.MyReadOnly = True
                txtRemarks.Text = obj.Remarks
                fndUOM.Value = obj.UOM_Code
                txtUOMdesc.Text = obj.UOM_desc
                If obj.isReversed = 1 Then
                    txtRemarks.MendatroryField = True
                End If
                lblWeighmentNo.Text = obj.MCC_Weighment_No
                fndPriceChart.Value = clsCommon.myCstr(obj.PriceCode)
                TxtFatWeightage.Text = clsCommon.myCdbl(obj.FAT_W)
                TxtSNFWeightage.Text = clsCommon.myCdbl(obj.SNF_W)
                txtfatPercentage.Text = clsCommon.myCdbl(obj.FAT_R)
                txtSNFPercentage.Text = clsCommon.myCdbl(obj.SNF_R)
                gv.Rows(0).Cells(colFatRate).Value = clsCommon.myCdbl(obj.FAT_RATE)
                gv.Rows(0).Cells(colSNFRate).Value = clsCommon.myCdbl(obj.SNF_RATE)
                gv.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(obj.FAT_KG)
                gv.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(obj.SNF_KG)
                gv.Rows(0).Cells(colAmount).Value = clsCommon.myCdbl(obj.Amount)

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
                'Dim strTankerNo As String = fndTnakerNo.Value
                'Dim qry As String = " select count(*) from  ( select distinct TankerNo    from (select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where isGateOut=1 and Tanker_No in (select Tanker_No  from TSPL_MCC_Dispatch_Challan where  isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1) union all select Tanker_No as TankerNo, tanker_transporter_Code as [Tanker Transporter Code],Description as [Tanker Transporter Name] from tspl_tanker_master where Tanker_No='')xx  ) yyyy where yyyy.TankerNo='" & strTankerNo & "' "
                'Dim isInterMittent As Boolean = False
                'Dim nextLevel As Double = 1
                'Dim FinalLoc As String = obj.FinalLoc

                'Dim Level1ChallanNo As String = ""
                ''If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 0 Then
                ''    isInterMittent = False
                ''    GroupBox2.Enabled = False
                ''    chkIntermittent.Enabled = True
                ''    chkIntermittent.Checked = False
                ''Else
                ''    isInterMittent = True
                ''    GroupBox2.Enabled = False
                ''    chkIntermittent.Enabled = False
                ''    chkIntermittent.Checked = True
                ''End If

                'If obj.isIntermittent = 1 Then

                '    qry = "  select MAX(ISNULL(currentlevel,0)) from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' "
                '    nextLevel = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

                '    ddlLevel.SelectedValue = CInt(nextLevel).ToString()
                If obj.CurrentLevel >= 2 Then
                    chkIntermittent.Enabled = False
                    txtTareWeight.ReadOnly = True
                    'ddlLevel.Enabled = False
                    'fndFinalLoc.Enabled = False
                    'fndLevel1ChallanNo.Enabled = False
                    'txtTareWeight.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select tare_weight  from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1'"))
                Else
                    chkIntermittent.Enabled = True
                    txtTareWeight.ReadOnly = False
                    'ddlLevel.Enabled = True
                    'fndFinalLoc.Enabled = True
                    'fndLevel1ChallanNo.Enabled = True
                    'txtTareWeight.Text = ""
                End If


                'qry = "  select finalLoc from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1'"
                'FinalLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                'fndFinalLoc.Value = FinalLoc
                'txtFinalLoc.Text = clsLocation.GetName(FinalLoc, Nothing)



                '    qry = "  select Chalan_NO  from TSPL_MCC_Dispatch_Challan where isnull(ReachedAtFinal,0)=0 and ISNULL ( isintermittent,0)=1 and Tanker_No='" & strTankerNo & "' and CurrentLevel='1'"
                '    Level1ChallanNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                '    fndLevel1ChallanNo.Value = Level1ChallanNo
                'End If
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
                Else
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPrint.Enabled = True
                    btnPost.Enabled = False
                    btnReverse.Enabled = True
                End If
                fndChalanNo.Focus()
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

    Private Sub txtNetQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNetQty.TextChanged
        Try
            gv.Rows(0).Cells("QtyInKg").Value = txtNetQty.Text
            If clsCommon.myLen(fndTnakerNo.Value) > 0 AndAlso clsCommon.myCdbl(txtNetQty.Text) > 0 Then
                Dim Storagecapacity As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" Select Storage_Capacity  from TSPL_TANKER_MASTER WHERE Tanker_No ='" + fndTnakerNo.Value + "'"))
                Dim nq As Double = clsCommon.myCdbl(txtNetQty.Text)
                Dim totPer As Double = nq * 100 / Storagecapacity
                If totPer >= 95 Then
                    ddlTankerFull.Text = "Yes"
                Else
                    ddlTankerFull.Text = "No"
                End If
            Else
                ddlTankerFull.Text = "No"

            End If
            If clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value) > 0 AndAlso clsCommon.myCdbl(txtNetQty.Text) > 0 Then
                gv.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(txtNetQty.Text) * clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value)
            End If
            If clsCommon.myCdbl(gv.Rows(0).Cells(SNFColName).Value) > 0 AndAlso clsCommon.myCdbl(txtNetQty.Text) > 0 Then
                gv.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(txtNetQty.Text) * clsCommon.myCdbl(gv.Rows(0).Cells(SNFColName).Value)
            End If
            calculateAmount()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub txtTareWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTareWeight.TextChanged
        Try
            If AllowFractionInMCCTankerDispatchGrossQty = False Then
                Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(txtTareWeight.Text))
                If (clsCommon.myCdbl(txtTareWeight.Text) - intPart) > 0 Then
                    txtTareWeight.Text = intPart
                    Throw New Exception("Tare Weight must be integer")

                End If
            End If
            ' txtTareWeight.Text = clsCommon.myFormat(txtTareWeight.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Exit Sub
        End Try

        Try
            txtNetQty.Text = clsCommon.myFormat(clsCommon.myCdbl(txtGrossWeight.Text) - clsCommon.myCdbl(txtTareWeight.Text))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtGrossWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGrossWeight.TextChanged
        Try
            If AllowFractionInMCCTankerDispatchGrossQty = False Then
                Dim intPart As Int64 = Math.Truncate(clsCommon.myCdbl(txtGrossWeight.Text))
                If (clsCommon.myCdbl(txtGrossWeight.Text) - intPart) > 0 Then
                    txtGrossWeight.Text = intPart
                    Throw New Exception("Gross Weight must be integer")

                End If
            End If
            ' txtGrossWeight.Text = clsCommon.myFormat(txtGrossWeight.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Exit Sub
        End Try

        Try
            txtNetQty.Text = clsCommon.myFormat(clsCommon.myCdbl(txtGrossWeight.Text) - clsCommon.myCdbl(txtTareWeight.Text))
        Catch ex As Exception
        End Try
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

    Sub printData(ByVal ChallanCode As String, ByVal MCCCode As String, ByVal PlantOrMCCCode As String)
        Dim ItemDesc As String = ""
        ItemDesc = clsFixedParameter.GetData(clsFixedParameterType.ItemDescForTankerdispatchPrint, clsFixedParameterCode.ItemDescForTankerDispatchPrint, Nothing)
        'MccAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select REPLACE ( replace (REPLACE ( Add1+ ',' + Add2 + ',' + Add3 +','+ Add4,',,',','),',,',','),',,',',')  from TSPL_LOCATION_MASTER where Location_Code='" & fndMCCCode.Value & "' "))
        'ToAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select REPLACE ( replace (REPLACE ( Add1+ ',' + Add2 + ',' + Add3 +','+ Add4,',,',','),',,',','),',,',',')  from TSPL_LOCATION_MASTER where Location_Code='" & fndPlantOrMCCCode.Value & "' "))
        'Dim strQuery As String = " select tspl_company_master.Logo_Img as img, TSPL_COMPANY_MASTER.Comp_Name as compname, '" & MccAddress & "' as 'address','" & ToAddress & "' as 'ToAddress', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, tspl_mcc_dispatch_challan.MCC_Code as MCC_Code,TSPL_LOCATION_MASTER.Location_Desc as [Plant or Mcc Name]  ,tspl_mcc_dispatch_challan.MCC_Name as MCC_Name ,tspl_mcc_dispatch_challan.Dispatch_Date as Dispatch_Date ,tspl_mcc_dispatch_challan.Chalan_NO as Chalan_NO ,tspl_mcc_dispatch_challan.Tanker_Dispatch_To as Tanker_Dispatch_To ,tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code as Mcc_Or_Plant_Code ,tspl_mcc_dispatch_challan.Tanker_No as Tanker_No ,tspl_mcc_dispatch_challan.Tanker_KM_Reading as Tanker_KM_Reading ,tspl_mcc_dispatch_challan.Drip_Marking as Drip_Marking ,tspl_mcc_dispatch_challan.Tanker_Full as Tanker_Full ,tspl_mcc_dispatch_challan.Control_Sample as Control_Sample ,tspl_mcc_dispatch_challan.Name_Of_Custodian as Name_Of_Custodian ,case when isnull(tspl_mcc_dispatch_challan.Seal_No1,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No1+','end + case when isnull(tspl_mcc_dispatch_challan.Seal_No2,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No2+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No3,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No3+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No4,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No4+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No5,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No5+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No6,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No6+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No7,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No7+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No8,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No8+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No9,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No9+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No10,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No10 end as seal_No,  tspl_mcc_dispatch_challan.isPosted as isPosted ,tspl_mcc_dispatch_challan.Posting_Date as Posting_Date ,tspl_mcc_dispatch_challan.item_code,'" & ItemDesc & "' item_desc,tspl_mcc_dispatch_challan.Tare_Weight as Tare_Weight ,tspl_mcc_dispatch_challan.Gross_Weight as Gross_Weight ,tspl_mcc_dispatch_challan.Net_Qty as Net_Qty ,tspl_mcc_dispatch_challan.Transfer_Price as Transfer_Price ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code as Param_Field_Code ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Desc as Param_Field_Desc ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Value as Param_Field_Value ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type as Param_Type, case when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='NA' then 1 when  TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='FAT' then 2 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='SNF' then 3 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='CLR' then 4 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='OTHERS' then 5 else 6 end as Ordering from tspl_mcc_dispatch_challan left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail on TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.chalan_no=tspl_mcc_dispatch_challan.chalan_no left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MCC_Dispatch_Challan.Comp_Code left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code inner join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code =TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code   where TSPL_MCC_Dispatch_Challan.chalan_no='" & fndChalanNo.Value & "' and TSPL_PARAMETER_MASTER.IsForPrintOnDispatch=1 order by Ordering "
        Dim chk_Heading As String = ""
        Dim MccAddress As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select REPLACE ( replace (REPLACE ( Add1+ ',' + Add2 + ',' + Add3 +','+ Add4,',,',','),',,',','),',,',',')  from TSPL_LOCATION_MASTER where Location_Code='" & MCCCode & "' "))
        Dim ToAddress As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select REPLACE ( replace (REPLACE ( Add1+ ',' + Add2 + ',' + Add3 +','+ Add4,',,',','),',,',','),',,',',')  from TSPL_LOCATION_MASTER where Location_Code='" & PlantOrMCCCode & "' "))
        Dim strGSTINFrom As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select TSPL_LOCATION_MASTER.GSTNO as GSTNO_From from TSPL_LOCATION_MASTER  left outer join tspl_state_master  on TSPL_LOCATION_MASTER.State = tspl_state_master.STATE_CODE where  Location_Code = '" & MCCCode & "' "))
        Dim strGSTINTo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select TSPL_LOCATION_MASTER.GSTNO as GSTNO_To from TSPL_LOCATION_MASTER  left outer join tspl_state_master  on TSPL_LOCATION_MASTER.State = tspl_state_master.STATE_CODE where  Location_Code = '" & PlantOrMCCCode & "' "))
        Dim strStateFrom As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tspl_state_master.GST_STATE_Code as GST_STATE_Code_From from TSPL_LOCATION_MASTER  left outer join tspl_state_master  on TSPL_LOCATION_MASTER.State = tspl_state_master.STATE_CODE where  Location_Code = '" & MCCCode & "' "))
        Dim strStateTo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select tspl_state_master.GST_STATE_Code as GST_STATE_Code_To from TSPL_LOCATION_MASTER  left outer join tspl_state_master  on TSPL_LOCATION_MASTER.State = tspl_state_master.STATE_CODE where  Location_Code = '" & PlantOrMCCCode & "' "))
        Dim strFromStateCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select State  from TSPL_LOCATION_MASTER where location_Code = '" & MCCCode & "' "))
        Dim strToStateCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select State  from TSPL_LOCATION_MASTER where location_Code = '" & PlantOrMCCCode & "' "))
        If clsCommon.CompairString(strFromStateCode, strToStateCode) = CompairStringResult.Equal Then
            chk_Heading = "Local"
        Else
            chk_Heading = "InterState"
        End If
        Dim strQuery As String = " select ISNULL(tspl_mcc_dispatch_challan.IsAgainstJobWork,0) AS IsAgainstJobWork,tspl_mcc_dispatch_challan.Sublocation_Code as Job_WorkLocation,JOB_WORK_LOCATION_MASTER.Location_Desc AS Job_Work_LocationDesc, " & _
             " JOB_WORK_VENDOR_MASTER.vendor_code as JobWork_VendorCode,JOB_WORK_VENDOR_MASTER.Vendor_Name as JobWork_VendorName, " & _
             " case when isnull(JOB_WORK_LOCATION_MASTER.Add1,'')<>'' then JOB_WORK_LOCATION_MASTER.Add1  else '' end + case when  isnull(JOB_WORK_LOCATION_MASTER.Add2,'')<>'' then  ','+JOB_WORK_LOCATION_MASTER.Add2 else '' end + case when isnull(JOB_WORK_LOCATION_MASTER.Add3,'')<>'' then ',' + JOB_WORK_LOCATION_MASTER.Add3   else '' end as JobWork_Location_Add,JOB_WORK_LOCATION_MASTER.GSTNO as JobWork_LocGSTIN,JobWork_LocationState_Master.GST_STATE_Code as JobWorkLoc_Gst_State, " & _
             " case when isnull(JOB_WORK_VENDOR_MASTER.Add1,'')<>'' then JOB_WORK_VENDOR_MASTER.Add1  else '' end + case when  isnull(JOB_WORK_VENDOR_MASTER.Add2,'')<>'' then  ','+JOB_WORK_VENDOR_MASTER.Add2 else '' end + case when isnull(JOB_WORK_VENDOR_MASTER.Add3,'')<>'' then ',' + JOB_WORK_VENDOR_MASTER.Add3   else '' end as JobWork_Vendor_Add,JOB_WORK_VENDOR_MASTER.GSTFinalNo as JobWork_Vendor_GSTIN,Job_Work_VendorState_Master.GST_STATE_Code as JobWork_Vendor_Gst_State, Parameter_Detail_for_Fat.Param_Field_Value AS FAT_PER_DETAIL,Parameter_Detail_for_SNF.Param_Field_Value AS SNF_PER_DETAIL, '" & chk_Heading & "' as chk_Heading,'' as E_waybillNo,'' as E_WayBillDate,'' as Dispatch_Through,'' as order_no,'' as order_date,'' as Other_Reference," & _
            " TSPL_CITY_MASTER.City_Name as To_CityName,TSPL_MCC_DISPATCH_CHALLAN.Amount,TSPL_STATE_MASTER.STATE_NAME as To_State,TSPL_Bulk_Price_MASTER.Standard_Rate,TSPL_Bulk_Price_MASTER.Fat_Percentage,TSPL_Bulk_Price_MASTER.Fat_Weightage,TSPL_Bulk_Price_MASTER.Snf_Percentage,TSPL_Bulk_Price_MASTER.Snf_Weightage,TSPL_MCC_DISPATCH_CHALLAN.Amount,TSPL_MCC_DISPATCH_CHALLAN.PriceCode,TSPL_MCC_DISPATCH_CHALLAN.Transfer_Price,TSPL_MCC_DISPATCH_CHALLAN.Net_Qty,tspl_item_master.hsn_code, '" & strGSTINFrom & "' as 'GSTNO_From', '" & strGSTINTo & "' as 'GSTNO_To','" & strStateFrom & "' as 'GST_STATE_Code_From', '" & strStateTo & "' as 'GST_STATE_Code_To', tspl_company_master.Logo_Img as img, TSPL_COMPANY_MASTER.Comp_Name as compname, '" & MccAddress & "' as 'address','" & ToAddress & "' as 'ToAddress', case when ISNULL(TSPL_COMPANY_MASTER.Phone1 ,'')<>'' then TSPL_COMPANY_MASTER.Phone1  else '' end + case when ISNULL(TSPL_COMPANY_MASTER.Phone2 ,'')<>'' then ', '+TSPL_COMPANY_MASTER.Phone2  else '' end as tel,TSPL_COMPANY_MASTER.Fax as fax, tspl_mcc_dispatch_challan.MCC_Code as MCC_Code,TSPL_LOCATION_MASTER.Location_Desc as [Plant or Mcc Name]  ,tspl_mcc_dispatch_challan.MCC_Name as MCC_Name ,tspl_mcc_dispatch_challan.Dispatch_Date as Dispatch_Date ,tspl_mcc_dispatch_challan.Chalan_NO as Chalan_NO ,tspl_mcc_dispatch_challan.Tanker_Dispatch_To as Tanker_Dispatch_To ,tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code as Mcc_Or_Plant_Code ,tspl_mcc_dispatch_challan.Tanker_No as Tanker_No ,tspl_mcc_dispatch_challan.Tanker_KM_Reading as Tanker_KM_Reading ,tspl_mcc_dispatch_challan.Drip_Marking as Drip_Marking ,tspl_mcc_dispatch_challan.Tanker_Full as Tanker_Full ,tspl_mcc_dispatch_challan.Control_Sample as Control_Sample ,tspl_mcc_dispatch_challan.Name_Of_Custodian as Name_Of_Custodian ,case when isnull(tspl_mcc_dispatch_challan.Seal_No1,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No1+','end + case when isnull(tspl_mcc_dispatch_challan.Seal_No2,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No2+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No3,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No3+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No4,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No4+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No5,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No5+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No6,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No6+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No7,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No7+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No8,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No8+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No9,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No9+','end+case when isnull(tspl_mcc_dispatch_challan.Seal_No10,'')='' then'' else tspl_mcc_dispatch_challan.Seal_No10 end as seal_No,  tspl_mcc_dispatch_challan.isPosted as isPosted ,tspl_mcc_dispatch_challan.Posting_Date as Posting_Date ,tspl_mcc_dispatch_challan.item_code,'" & ItemDesc & "' item_desc,tspl_mcc_dispatch_challan.Tare_Weight as Tare_Weight ,tspl_mcc_dispatch_challan.Gross_Weight as Gross_Weight ,tspl_mcc_dispatch_challan.Net_Qty as Net_Qty ,tspl_mcc_dispatch_challan.Transfer_Price as Transfer_Price ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code as Param_Field_Code ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Desc as Param_Field_Desc ," & _
            " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Value as Param_Field_Value ,TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type as Param_Type," & _
            "case when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='NA' then 1 when  TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='FAT' then 2 when " & _
            " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='SNF' then 3 when TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='CLR' then 4 when " & _
            " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type='OTHERS' then 5 else 6 end as Ordering, TSPL_MCC_Dispatch_Challan.EWayBillNo,case when TSPL_MCC_Dispatch_Challan.EWayBillDate is null then '' else convert (varchar,TSPL_MCC_Dispatch_Challan.EWayBillDate,103) end as EWayBillDate,TSPL_MCC_Dispatch_Challan.Electronic_Ref_No,tspl_mcc_dispatch_challan.Tanker_Transporter_Name from tspl_mcc_dispatch_challan " & _
            " left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail on TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.chalan_no=tspl_mcc_dispatch_challan.chalan_no and TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Desc not in ('FAT %','SNF %') " & _
            " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MCC_Dispatch_Challan.Comp_Code " & _
            " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code " & _
            " inner join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code =TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Field_Code " & _
            " left outer join tspl_item_master on tspl_item_master.item_code =tspl_mcc_dispatch_challan.item_code " & _
            " left join tspl_state_master on TSPL_LOCATION_MASTER.state=tspl_state_master.STATE_CODE " & _
            " left join TSPL_CITY_MASTER on TSPL_LOCATION_MASTER.City_Code=TSPL_CITY_MASTER.City_Code " & _
            " left join TSPL_Bulk_Price_MASTER on TSPL_MCC_DISPATCH_CHALLAN.PriceCode =TSPL_Bulk_Price_MASTER.Price_Code " & _
            " LEFT JOIN TSPL_Mcc_Dispatch_Chalan_Parameter_Detail AS Parameter_Detail_for_Fat on Parameter_Detail_for_Fat.chalan_no=tspl_mcc_dispatch_challan.chalan_no AND Parameter_Detail_for_Fat.Param_Field_DESC='FAT %'" & _
            " LEFT JOIN TSPL_Mcc_Dispatch_Chalan_Parameter_Detail AS Parameter_Detail_for_SNF on Parameter_Detail_for_SNF.chalan_no=tspl_mcc_dispatch_challan.chalan_no AND Parameter_Detail_for_SNF.Param_Field_DESC='SNF %' " & _
            " left outer join TSPL_LOCATION_MASTER AS JOB_WORK_LOCATION_MASTER ON tspl_mcc_dispatch_challan.Sublocation_Code=JOB_WORK_LOCATION_MASTER.Location_Code " & _
             " LEFT OUTER JOIN TSPL_STATE_MASTER AS JobWork_LocationState_Master on JOB_WORK_LOCATION_MASTER.State=JobWork_LocationState_Master.STATE_CODE " & _
             " LEFT OUTER JOIN TSPL_VENDOR_MASTER AS JOB_WORK_VENDOR_MASTER ON JOB_WORK_LOCATION_MASTER.Jobwork_Vendor=JOB_WORK_VENDOR_MASTER.Vendor_Code " & _
             " LEFT OUTER JOIN TSPL_STATE_MASTER AS Job_Work_VendorState_Master on JOB_WORK_VENDOR_MASTER.State_Code=Job_Work_VendorState_Master.STATE_CODE " & _
            " where TSPL_MCC_Dispatch_Challan.chalan_no='" & ChallanCode & "' and TSPL_PARAMETER_MASTER.IsForPrintOnDispatch=1 order by Ordering "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
        If dt.Rows.Count > 0 Then
            'MilkProcurementReportViewer.funreport(dt, "rptDispatchChallan", "Dispatch Challan")
            Dim frmCrystalReportViewer As New frmCrystalReportViewer()
            If clsERPFuncationality.GetGSTStatus(clsCommon.myCDate(dtpDateAndTime.Value)) Then

                'If clsCommon.CompairString(strFromStateCode, strToStateCode) = CompairStringResult.Equal Then
                '    frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDispatchChallan", "Dispatch Challan", clsCommon.myCDate(dtpDateAndTime.Value), "rptCompanyAddress.rpt")
                'Else
                frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDispatchChallan_Bill_of_Supply", "Dispatch Challan", clsCommon.myCDate(dtpDateAndTime.Value), "rptCompanyAddress.rpt")
                'End If
            Else
                frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDispatchChallan", "Dispatch Challan", "rptCompanyAddress.rpt")
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        printData(fndChalanNo.Value, fndMCCCode.Value, fndPlantOrMCCCode.Value)
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
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & strTankerNo & "'"))
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            'str = "Slab From " & getSlabLowerRange() & "  To " & getSlabUpperRange()
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            If getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value) = -1 Then
                Throw New Exception("Please Map Distance Between " & fndMCCCode.Value & " And " & fndPlantOrMCCCode.Value)
            End If
            str = " Total  " & getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value) & "  KM "
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            str = ""
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            str = ""
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
            Dim QtyApply As Double = 0
            If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
                QtyApply = tCap
            Else
                QtyApply = clsCommon.myCdbl(txtNetQty.Text)
            End If
            str = " Total  " & QtyApply & " KG "
        Else
            str = ""
        End If
        Return str
    End Function

    Function getChargesForInterMittent(ByVal strTankerNo As String) As String
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & strTankerNo & "'"))
        Dim Level1Mcc As String = clsDBFuncationality.getSingleValue(" select MCC_Code  from tspl_mcc_dispatch_challan where Chalan_NO='" & fndLevel1ChallanNo.Value & "' ")
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            str = "Slab From " & getSlabLowerRangeInterMittent() & "  To " & getSlabUpperRangeInterMittent()
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            If getDistance(Level1Mcc, fndPlantOrMCCCode.Value) = -1 Then
                Throw New Exception("Please Map Distance Between " & Level1Mcc & " And " & fndPlantOrMCCCode.Value)
            End If
            str = " Total  " & getDistance(Level1Mcc, fndPlantOrMCCCode.Value) & "  KM "
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            str = ""
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            str = ""
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
            Dim QtyApply As Double = 0
            If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
                QtyApply = tCap
            Else
                QtyApply = clsCommon.myCdbl(txtNetQty.Text)
            End If
            str = " Total  " & QtyApply & " KG "
        Else
            str = ""
        End If
        Return str
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
        Dim rValue As Double = 0
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & fndTnakerNo.Value & "'"))
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            rValue = getSlabPaymentAmount()
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            rValue = getRatePerKMPaymentAmount() * getDistance(fndMCCCode.Value, fndPlantOrMCCCode.Value)
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            rValue = 0
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            rValue = 0
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
            Dim QtyApply As Double = 0
            If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
                QtyApply = tCap
            Else
                QtyApply = clsCommon.myCdbl(txtNetQty.Text)
            End If
            rValue = getRatePerLTRKGPaymentAmount() * QtyApply
        Else
            rValue = 0
        End If
        Return rValue
    End Function

    Function getPaymentAmountIntermittent() As Double
        Dim rValue As Double = 0
        Dim Level1Mcc As String = clsDBFuncationality.getSingleValue(" select MCC_Code  from tspl_mcc_dispatch_challan where Chalan_NO='" & fndLevel1ChallanNo.Value & "' ")
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Status  from tspl_tanker_master where Tanker_No ='" & fndTnakerNo.Value & "'"))
        If clsCommon.CompairString(str, "KM_Range") = CompairStringResult.Equal Then
            rValue = getSlabPaymentAmountInterMittent()
        ElseIf clsCommon.CompairString(str, "Rate/K.M") = CompairStringResult.Equal Then
            rValue = getRatePerKMPaymentAmount() * getDistance(Level1Mcc, fndPlantOrMCCCode.Value)
        ElseIf clsCommon.CompairString(str, "Day/Diesel") = CompairStringResult.Equal Then
            rValue = 0
        ElseIf clsCommon.CompairString(str, "Rental") = CompairStringResult.Equal Then
            rValue = 0
        ElseIf clsCommon.CompairString(str, "Rate/Ltr") = CompairStringResult.Equal Then
            Dim tCap As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Storage_Capacity  from TSPL_TANKER_MASTER  where Tanker_No='" & fndTnakerNo.Value & "' "))
            Dim QtyApply As Double = 0
            If tCap > clsCommon.myCdbl(txtNetQty.Text) AndAlso clsCommon.myCdbl(txtNetQty.Text) <> 0 Then
                QtyApply = tCap
            Else
                QtyApply = clsCommon.myCdbl(txtNetQty.Text)
            End If
            rValue = getRatePerLTRKGPaymentAmount() * QtyApply
        Else
            rValue = 0
        End If
        Return rValue
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

                Dim CurBal As Double = 0
                Dim CurBalOFVL As Double = 0
                Dim CurBalOFML As Double = 0

                Dim CurBal_SNF As Double = 0
                Dim CurBalOFVL_SNF As Double = 0
                Dim CurBalOFML_SNF As Double = 0

                Dim CurBal_FAT As Double = 0
                Dim CurBalOFVL_FAT As Double = 0
                Dim CurBalOFML_FAT As Double = 0
                'If clsCommon.myLen(fndLevel1ChallanNo.Value) > 0 AndAlso clsCommon.myCdbl(ddlLevel.SelectedValue) > 1 Then
                '    'Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "')"))

                '    'Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No in (Select Chalan_NO  from TSPL_MCC_Dispatch_Challan where Level1ChallanNo  ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "'  and CurrentLevel =" & clsCommon.myCdbl(ddlLevel.SelectedValue) - 1 & " and isnull(isIntermittent,0) =1))"))
                '    'CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))

                '    dt = clsDBFuncationality.GetDataTable("Select Location_Code  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (Select Receipt_Challan_No  from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No in (Select Chalan_NO  from TSPL_MCC_Dispatch_Challan where Level1ChallanNo  ='" & clsCommon.myCstr(fndLevel1ChallanNo.Value) & "'  and CurrentLevel =" & clsCommon.myCdbl(ddlLevel.SelectedValue) - 1 & " and isnull(isIntermittent,0) =1))")
                '    Dim strsublocation As String = String.Empty
                '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '        For i As Integer = 0 To dt.Rows.Count - 1
                '            strsublocation = clsCommon.myCstr(dt.Rows(i)("Location_Code"))
                '            If clsCommon.myLen(strsublocation) > 0 Then
                '                CurBalOFVL = CurBalOFVL + clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '            End If
                '        Next
                '    End If


                '    CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                '    CurBal = CurBalOFVL + CurBalOFML

                '    ''TO CHECK FAT AND SNF
                '    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing), "1") = CompairStringResult.Equal Then
                '        'Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & strsublocation & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
                '        'If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                '        '    CurBalOFVL_SNF = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG"))
                '        '    CurBalOFVL_FAT = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG"))
                '        'End If

                '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '            For i As Integer = 0 To dt.Rows.Count - 1
                '                strsublocation = clsCommon.myCstr(dt.Rows(i)("Location_Code"))
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
                '    ''-----------------------
                'Else
                ''  CurBal = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                ' ''richa agarwal 17/08/2016
                ''Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' and Location_Type='Physical'"))
                'Dim strsublocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' "))
                'If clsCommon.myLen(strsublocation) > 0 Then
                '    CurBalOFVL = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                'End If
                'CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                'CurBal = CurBalOFVL + CurBalOFML

                ' ''TO CHECK FAT AND SNF
                'If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.UpdateInventorySummaryTable, clsFixedParameterCode.UpdateInventorySummaryTable, Nothing), "1") = CompairStringResult.Equal Then
                '    If clsCommon.myLen(strsublocation) > 0 Then
                '        Dim DTSNFFAT_VIR As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & strsublocation & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
                '        If DTSNFFAT_VIR IsNot Nothing And DTSNFFAT_VIR.Rows.Count > 0 Then
                '            CurBalOFVL_SNF = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_SNF_KG"))
                '            CurBalOFVL_FAT = clsCommon.myCdbl(DTSNFFAT_VIR.Rows(0)("CL_FAT_KG"))
                '        End If
                '    End If

                '    Dim DTSNFFAT_MAIN As DataTable = clsDBFuncationality.GetDataTable("SELECT CL_FAT_KG ,CL_SNF_KG FROM  TSPL_FUN_ITEM_LOC_BALANCE('" & fndItemCode.Value & "','" & fndMCCCode.Value & "','" & clsCommon.GetPrintDate(dtpDateAndTime.Value, "dd-MMM-yyyy") & "')", Nothing)
                '    If DTSNFFAT_MAIN IsNot Nothing And DTSNFFAT_MAIN.Rows.Count > 0 Then
                '        CurBalOFML_SNF = clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_SNF_KG"))
                '        CurBalOFML_FAT = clsCommon.myCdbl(DTSNFFAT_MAIN.Rows(0)("CL_FAT_KG"))
                '    End If
                '    CurBal_SNF = CurBalOFVL_SNF + CurBalOFML_SNF
                '    CurBal_FAT = CurBalOFVL_FAT + CurBalOFML_FAT

                '    If CurBal_SNF < clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) Then
                '        Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) & " ")
                '    End If
                '    If CurBal_FAT < clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) Then
                '        Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) & " ")
                '    End If
                'End If
                ' ''------------------


                ''richa agarwal 02/01/2018
                If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowtoNegativeStockInventoryAtTankerDispatch, clsFixedParameterCode.AllowtoNegativeStockInventoryAtTankerDispatch, Nothing), "0") = CompairStringResult.Equal Then
                    dt = clsDBFuncationality.GetDataTable("select Location_Code as [Code]  from TSPL_Location_MASTER  left join TSPL_GL_SEGMENT_CODE as Seg on TSPL_Location_MASTER.Loc_Segment_Code=Seg.Segment_Code where   is_sub_location='Y' and Main_Location_Code='" & clsCommon.myCstr(fndMCCCode.Value) & "' order by TSPL_Location_MASTER.Location_Code ")
                    Dim strsublocation As String = String.Empty
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            strsublocation = clsCommon.myCstr(dt.Rows(i)("Code"))
                            If clsCommon.myLen(strsublocation) > 0 Then
                                CurBalOFVL = CurBalOFVL + clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, strsublocation, fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                            End If
                        Next
                    End If



                    CurBalOFML = clsCommon.myCdbl(ClsLoadingTanker.getBalance(fndItemCode.Value, fndMCCCode.Value, "", fndChalanNo.Value, dtpDateAndTime.Value, Nothing, "KG"))
                    CurBal = CurBalOFVL + CurBalOFML

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

                        If CurBal_SNF < clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) Then
                            Throw New Exception("Available SNF KG is :     " & CurBal_SNF & Environment.NewLine & "Required SNF KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colSNFKG).Value) & " ")
                        End If
                        If CurBal_FAT < clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) Then
                            Throw New Exception("Available FAT KG is :     " & CurBal_FAT & Environment.NewLine & "Required FAT KG :     " & clsCommon.myCdbl(gv.Rows(0).Cells(colFatKG).Value) & " ")
                        End If
                    End If
                    ''------------------


                    ' End If

                    ''richa agarwal 30/03/2016
                    If clsCommon.CompairString(clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing), "1") = CompairStringResult.Equal Then
                        If CurBal > 0 Then
                            CurBal = ClsLoadingTanker.GetTolerane(CurBal, clsCommon.myCdbl(txtNetQty.Text))
                        End If
                    End If

                    ''--------------------------------

                    If CurBal < clsCommon.myCdbl(txtNetQty.Text) Then
                        Throw New Exception("Available Qty is :     " & CurBal & Environment.NewLine & "Required Qty :     " & txtNetQty.Text & " ")
                        'If clsCommon.MyMessageBoxShow("Available Qty is :     " & CurBal & Environment.NewLine & "Required Qty :     " & txtNetQty.Text & Environment.NewLine & "Want To Continue ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                        'Else
                        '    Exit Sub
                        'End If
                    End If
                End If

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
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(fndChalanNo.Value, NavigatorType.Current)
                If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                    printData(fndChalanNo.Value, fndMCCCode.Value, fndPlantOrMCCCode.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndItemCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndItemCode._MYValidating
        fndItemCode.Value = clsItemMaster.getFinder("Product_Type ='mi'", fndItemCode.Value, isButtonClicked)
        If clsCommon.myLen(fndItemCode.Value) > 0 Then
            txtItemDesc.Text = clsCommon.myCstr(clsItemMaster.GetItemName(fndItemCode.Value, Nothing))
        End If
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub RadGroupBox6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox6.Click

    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
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
        For i As Integer = 0 To gv.Columns.Count - 1
            If clsCommon.CompairString(gv.Columns(i).Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gv.Columns(i).Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gv.Columns(i).Tag, "CLR") = CompairStringResult.Equal Then
                If clsCommon.myLen(gv.Rows(0).Cells(i).Value) <= 0 Or (Not IsNumeric(gv.Rows(0).Cells(i).Value)) Then
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
            gv.Rows(0).Cells(snfField).Value = clsCommon.myFormat(clsEkoPro.getSnfOnCalculation(clsCommon.myCdbl(gv.Rows(0).Cells(fatField).Value), clsCommon.myCdbl(gv.Rows(0).Cells(clrField).Value), clsCommon.myCdbl(gv.Rows(0).Cells(cfField).Value)))
        Else
            gv.Rows(0).Cells(snfField).Value = clsCommon.myFormat(0)
        End If
    End Sub

    Private Sub gv_CellValidated(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatedEventArgs) Handles gv.CellValidated
        Try

            If e.Column Is gv.Columns(FATColName) Then
                Dim fracValue1 As Double = 0
                If clsCommon.myLen(FATColName) > 0 Then
                    fracValue1 = clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value)
                    fracValue1 = Math.Round((fracValue1 - CInt(fracValue1)) * 100, 2)
                    If CInt(fracValue1) Mod 5 <> 0 Then
                        Throw New Exception(" FAT value in Grid, must have its decimal part multiple of 5")
                        gv.Rows(0).Cells(FATColName).Value = 0
                        gv.CurrentRow = gv.Rows(0)
                        gv.CurrentColumn = gv.Columns(FATColName)
                        'gv.Rows(0).Cells(FATColName).BeginEdit()
                    Else
                        ' gv.Rows(0).Cells(FATColName).Value = Math.Truncate(clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value) * 100) / 100
                        gv.Rows(0).Cells(FATColName).Value = clsCommon.myFormat(gv.Rows(0).Cells(FATColName).Value)
                    End If
                End If
            End If
            If e.Column Is gv.Columns(SNFColName) Then
                'gv.Rows(0).Cells(SNFColName).Value = Math.Truncate(Math.Round(clsCommon.myCdbl(gv.Rows(0).Cells(SNFColName).Value) * 100, 0)) / 100
                gv.Rows(0).Cells(SNFColName).Value = clsCommon.myFormat(gv.Rows(0).Cells(SNFColName).Value)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv_CellValidating(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles gv.CellValidating

    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            'If clsCommon.CompairString(gv.CurrentColumn.Tag, "FAT") = CompairStringResult.Equal Or clsCommon.CompairString(gv.CurrentColumn.Tag, "CF") = CompairStringResult.Equal Or clsCommon.CompairString(gv.CurrentColumn.Tag, "CLR") = CompairStringResult.Equal Then
            '    calculateSNF()
            'End If
            If e.Column Is gv.Columns(FATColName) Then
                If clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value) > 0 AndAlso clsCommon.myCdbl(txtNetQty.Text) > 0 Then
                    gv.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(clsCommon.myCdbl(txtNetQty.Text) * clsCommon.myCdbl(gv.Rows(0).Cells(FATColName).Value) / 100, False, True, False, 3, True)
                End If
                calculateAmount()
            End If
            If e.Column Is gv.Columns(SNFColName) Then
                If clsCommon.myCdbl(gv.Rows(0).Cells(SNFColName).Value) > 0 AndAlso clsCommon.myCdbl(txtNetQty.Text) > 0 Then
                    gv.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(clsCommon.myCdbl(txtNetQty.Text) * clsCommon.myCdbl(gv.Rows(0).Cells(SNFColName).Value) / 100, False, True, False, 3, True)
                End If
                calculateAmount()
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
                'gvParam.AutoSizeRows = True
            End If
        End If
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
            clsCommon.MyMessageBoxShow(Me, "No Item Of Seal Type Found", Me.Text)
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

    Sub CalculateTransferPrice() Handles dtpDateAndTime.ValueChanged
        If clsCommon.myLen(fndMCCCode.Value) > 0 Then
            txtTransferPrice.Text = clsMccMilkTransferPrice.getTransferPrice(fndMCCCode.Value, dtpDateAndTime.Value)
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

    Private Sub BtnRead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRead.Click
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.text)
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

    Private Sub fndPriceChart__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPriceChart._MYValidating
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

    Private Sub txtTareWeight_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTareWeight.Validating
        Try
            If clsCommon.myCdbl(txtGrossWeight.Text) < clsCommon.myCdbl(txtTareWeight.Text) Then
                Throw New Exception("Tare Weight must be less than gross weight")
                txtTareWeight.Focus()
            End If
            If clsCommon.myCdbl(txtNetQty.Text) <= 0 Then
                Throw New Exception("Net Weight Must Not be Negative or Zero")
                txtTareWeight.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub fndMCCCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        'If clsCommon.myLen(fndMCCCode.Value) > 0 AndAlso clsCommon.CompairString(fndMCCCode.Value, "NA") = CompairStringResult.Equal Then
        '    txtMCCName.Text = "NA"
        '    Exit Sub
        'End If
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_code in(" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If

        fndMCCCode.Value = clsLocation.getFinder(whrCls, fndMCCCode.Value, isButtonClicked)
        txtMCCName.Text = clsLocation.GetName(fndMCCCode.Value, Nothing)
        ResetParameterGrid()
        CalculateTransferPrice()
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
            txtTareWeight.ReadOnly = True
        Else
            txtTareWeight.ReadOnly = False
        End If
    End Sub

    Private Sub fndLevel1ChallanNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLevel1ChallanNo._MYValidating
        Dim whrcls As String = " isIntermittent=1 and CurrentLevel=1 "
        fndLevel1ChallanNo.Value = clsMccDispatch.getFinder(whrcls, fndLevel1ChallanNo.Value, isButtonClicked)
        txtTareWeight.Text = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select tare_weight from tspl_mcc_dispatch_challan where chalan_no='" & fndLevel1ChallanNo.Value & "' "))
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
            'clsMccDispatch.UpdateTankerStatus(PrevTanker, 1)
            'clsMccDispatch.UpdateTankerStatus(fndTnakerNo.Value, 0)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

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

    Private Sub chkJobWork_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkJobWork.ToggleStateChanged
        If chkJobWork.Checked Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
            txtSubLocation.Value = ""
            lblSubLocation.Text = ""
        End If
    End Sub

    Private Sub txtSubLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSubLocation._MYValidating
        Dim strLocations = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strLocations = "and location_code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        If clsCommon.myLen(fndPlantOrMCCCode.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select location code before sub location", Me.Text)
            Exit Sub
        End If
        txtSubLocation.Value = clsLocation.getFinder("(Main_Location_Code='" & fndPlantOrMCCCode.Value & "' and Is_Jobwork=1 and isnull(Is_Sub_Location,'N')='Y')" & strLocations, txtSubLocation.Value, isButtonClicked)
        If clsCommon.myLen(txtSubLocation.Value) > 0 Then
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
        Else
            lblSubLocation.Text = ""
        End If
        strLocations = Nothing

    End Sub

    Private Sub btnReverseAfterMilkIn_Click(sender As Object, e As EventArgs) Handles btnReverseAfterMilkIn.Click

    End Sub
End Class
