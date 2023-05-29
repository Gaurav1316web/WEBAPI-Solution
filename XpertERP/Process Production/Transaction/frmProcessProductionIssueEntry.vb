'----------------------created by Monika 08/08/2014--BM00000003198-------BM00000003794--BM00000004866-----
Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class FrmProcessProductionIssueEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim DecimalPoint As Integer = 3
    Public strDocumentNo As String = ""
    Dim isSavedSuccess As Boolean = True
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim qry As String = ""
    Dim check As Integer = 0
    Dim isNewEntry As Boolean = Nothing
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim objSerial As clsSerialPort
    '

    Const colSno As String = "SNO"
    Const colFrm_Loc_Type As String = "Frm_loc_Type"
    Const colloc_code As String = "Loc_Code"
    Const colloc_name As String = "Loc_Name"
    Const colTo_Loc_Type As String = "To_Loc_Type"
    Const colToloc_code As String = "TOLoc_Code"
    Const colToloc_name As String = "TOLoc_Name"
    Const colitemcode As String = "itemcode"
    Const colitemname As String = "itemname"
    Const colTolerance As String = "colTolerance"
    Const colIsBatchItem As String = "colIsBatchItem"
    Const colitemtype As String = "itemtype"
    Const colitemproducttype As String = "producttype"
    Const coluom As String = "UOM"
    Const colUOMDesc As String = "UOM_Desc"
    Const colavailqty As String = "Avail_Qty"
    Const colavailfatpers As String = "Availfatpers"
    Const colavailfatkg As String = "Availfatkg"
    Const colavailsnfpers As String = "Availsnfpers"
    Const colavailsnfkg As String = "Availsnfkg"
    Const colrequrdqty As String = "ReqQty"
    Const colqty As String = "Qty"
    Const colfatpers As String = "fatpers"
    Const colfatkg As String = "fatkg"
    Const colsnfpers As String = "snfpers"
    Const colsnfkg As String = "snfkg"
    Const colrem As String = "Remarks"

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

    Dim arrLoc As String = Nothing

    Dim allowanybo As Boolean = False
    Dim allowchildSubchildbo As Boolean = False
    Public CheckStockServerDate As Boolean = True
    Private settAllowNegativeStockInDairyProduction As Boolean = False
    Dim SettingPickFATSNFPerFromStock As Boolean = False
    Dim settPickProductCostFromItemUOMDetail As Boolean = False
    Dim settProductionIssueQtyTollerance As Decimal = 0 ''BHA/06/12/18-000744 by balwinder on 12/12/2018

    Dim RunBatchFifowise As Boolean = False

    Dim MI_Consm_Type As Integer = 0 ''0: Issue Basis 1. BOM Basis
    Dim MP_Consm_Type As Integer = 0
    Dim Othr_Consm_Type As Integer = 0
    Dim settTankerDispatchAvgFATSNFPer As Boolean = False
    Dim UseProductionPlaningDateForWholeProductionCycle As Boolean = False
    Dim settProductionOnlyOneIssueAgainstBatch As Boolean = False
    Dim SettDisableToPickMainLocation As Boolean = False
#End Region

    Private Sub FrmProcessProductionIssueEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        DecimalPoint = CInt(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionQtyDecimalPoint, clsFixedParameterCode.ProductionQtyDecimalPoint, Nothing)))
        RunBatchFifowise = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RunBatchFifowise, clsFixedParameterCode.RunBatchFifowise, Nothing)) = 1)
        settAllowNegativeStockInDairyProduction = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, Nothing)) > 0)
        settProductionOnlyOneIssueAgainstBatch = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionOnlyOneIssueAgainstBatch, clsFixedParameterCode.ProductionOnlyOneIssueAgainstBatch, Nothing)) > 0)
        SettingPickFATSNFPerFromStock = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickFATSNFPerFromStock, clsFixedParameterCode.PickFATSNFPerFromStock, Nothing)) > 0)
        settPickProductCostFromItemUOMDetail = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, Nothing)) > 0)
        settProductionIssueQtyTollerance = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ProductionIssueQtyTollerance, clsFixedParameterCode.ProductionIssueQtyTollerance, Nothing))
        settTankerDispatchAvgFATSNFPer = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchAvgFATSNFPer, clsFixedParameterCode.TankerDispatchAvgFATSNFPer, Nothing)) = 1)
        SettDisableToPickMainLocation = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DisableToPickMainLocationType, clsFixedParameterCode.DisableToPickMainLocationType, Nothing)) = 1)
        MI_Consm_Type = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkStandardization, Nothing))
        MP_Consm_Type = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeMilkProduct, Nothing))
        Othr_Consm_Type = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ConsumptionType, clsFixedParameterCode.ConsumptionTypeOther, Nothing))
        UseProductionPlaningDateForWholeProductionCycle = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.UseProductionPlaningDateForWholeProductionCycle, clsFixedParameterCode.UseProductionPlaningDateForWholeProductionCycle, Nothing)) = 1, True, False)

        If DecimalPoint <= 0 Then
            DecimalPoint = 3
        End If
        FunReset()
        ButtonToolTip.SetToolTip(btnsave, "Alt+S for save/update data")
        ButtonToolTip.SetToolTip(btndelete, "Alt+D for deleting data")
        ButtonToolTip.SetToolTip(btnPost, "Alt+P for posting data")
        ButtonToolTip.SetToolTip(btnclose, "Alt+C for window close")
        ButtonToolTip.SetToolTip(btngo, "Alt+G for QC detail filling")

        allowanybo = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ALLOWANYBO, clsFixedParameterCode.ALLOWANYBO, Nothing)) = "0", False, True))
        allowchildSubchildbo = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ALLOWCBOSBO, clsFixedParameterCode.ALLOWCBOSBO, Nothing)) = "0", False, True))
        If allowanybo = True AndAlso allowchildSubchildbo = True Then 'when both setting is on then first one execute.
            allowchildSubchildbo = False
        End If
        If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCDisplay_All_Parameter, clsFixedParameterCode.MilkSetting, Nothing)) = 0, False, True) Then
            objSerial = New clsSerialPort
            objSerial.SetPortNameValues(cboComPort)
            objSerial.SetPortNameValues(cboComPortFM)
            clsPortSetting.GetWeighingMachineNames(CboMachine)
            'SplitContainer2.SplitterDistance = (SplitContainer2.Panel1.Height - SplitContainer4.Height) + 20
        Else
            SplitContainer3.Panel1Collapsed = True
            'SplitContainer2.SplitterDistance = SplitContainer4.Height + 20 '' SplitContainer2.Panel2.Height +''SplitContainer2.Panel1.Height -
        End If


        'SplitContainer4.Panel2Collapsed = True
        RadPageViewPage2.Text = "Quality Check"
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        cboComPort.Text = "ComPort 1"
        cboComPortFM.Text = "ComPort 3"
    End Sub

    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try
            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                txtmain_Loc_Code.Value = obj.Default_LocCode
                txtMain_Loc_Name.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub FunReset()
        isSavedSuccess = True
        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, Nothing)), "1") = CompairStringResult.Equal Then
            CheckStockServerDate = True
        Else
            CheckStockServerDate = False
        End If
        LoadBlankGrid()
        LoadQCBlankGrid()
        txtmain_Loc_Code.Value = ""
        txtMain_Loc_Name.Text = ""
        btnFrm_SubLoc.IsChecked = True
        MyLabel4.Text = "From Sub-Location"
        btnTo_SubLoc.IsChecked = True
        MyLabel6.Text = "To Sub-Location"
        TxtManualBatchNo.Text = ""
        lblLineNo.Text = ""
        LblCostCenterCode.Text = ""
        lblCostCenterName.Text = ""
        lblProfitCenterCode.Text = ""
        lblProfitCenterName.Text = ""
        txtCode.Value = ""
        txtbatch_desc.Text = ""
        txtdesc.Text = ""
        dtpDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtlocation.Text = ""
        txtlocationname.Text = ""
        txtfrmsub.Value = ""
        txtfrmsub_name.Text = ""
        txttosub.Value = ""
        txttosub_name.Text = ""
        txtbatchorder.Value = ""
        fndSection.Value = ""
        TxtSection.Text = ""
        cboStatus.Text = "Open"
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        chkJobWorkInward.Checked = False ''BHA/22/10/18-000632 by balwinder on 30/10/2018
        txtmain_Loc_Code.Enabled = True
        'txtbatchorder.Enabled = True
        fndSection.Enabled = True
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnPost.Enabled = False
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        RadPageView1.SelectedPage = RadPageViewPage1

        isNewEntry = True

        LOCATIONRIGTHS()
        chkBO.IsChecked = True
        chkWOBO.IsChecked = False
        chkBO.Enabled = True
        chkWOBO.Enabled = True
        fndSection.Enabled = True

        txtdesc.Focus()
        txtdesc.Select()

    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProcessProductionIssueEntry)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
        'btnPrint.Enabled = MyBase.isPrintFlag
        btnCancel.Visible = MyBase.isCancel_Flag_After_Posting
    End Sub

    Private Sub FrmProcessProductionIssueEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
                If AllowToSave() Then SaveData(False)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
                DeleteData()
            ElseIf e.Alt And e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                FunReset()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
                PostData()
                'ElseIf e.Alt AndAlso e.KeyCode = Keys.G Then
                '    btngo.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isReverse AndAlso btnunpost.Visible Then
                btnunpost.PerformClick()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                 "TSPL_PP_ISSUE_HEAD " + Environment.NewLine + _
                                                 "TSPL_PP_ISSUE_ITEM_DETAIL " + Environment.NewLine + _
                                                 "TSPL_PP_ISSUE_QC_DETAIL " + Environment.NewLine + _
                                                 "Press Alt+P for Post Trasnaction " + Environment.NewLine + _
                                                 "TSPL_PP_BATCH_ORDER_HEAD(Close) ")

                If btnunpost.Visible Then
                    btnunpost.Visible = False
                Else
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = "SIRC"
                    frm.strCode = "SIReversAndCreate"
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnunpost.Visible = True
                    End If
                End If
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colloc_code) Then
                isCellValueChanged = True
                OpenLocation(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colToloc_code) Then
                isCellValueChanged = True
                ToOpenLocation(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colitemcode) Then
                isCellValueChanged = True
                OpenIcode(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(coluom) Then
                isCellValueChanged = True
                OpenUOM(True)
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso ((gv.CurrentColumn Is gv.Columns(colqty)) Or (gv.CurrentColumn Is gv.Columns(colfatpers)) Or (gv.CurrentColumn Is gv.Columns(colfatkg))) Then
                isCellValueChanged = True
                Cal_FATSNF()
                isCellValueChanged = False
            End If

            If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso ((gv.CurrentColumn Is gv.Columns(colqty)) Or (gv.CurrentColumn Is gv.Columns(colsnfpers)) Or (gv.CurrentColumn Is gv.Columns(colsnfkg))) Then
                isCellValueChanged = True
                Cal_FATSNF()
                isCellValueChanged = False
            End If

            '>>>>>>>>>>>>>>>QC-----------------

            If e.KeyCode = Keys.F2 AndAlso gv_qc.CurrentColumn IsNot Nothing AndAlso gv_qc.CurrentColumn Is gv_qc.Columns(colQCRange) Then
                isCellValueChanged = True
                Dim Icode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCitemcode).Value)
                Dim gvIcode As String = ""

                For Each grow As GridViewRowInfo In gv.Rows
                    gvIcode = clsCommon.myCstr(grow.Cells(colitemcode).Value)

                    If clsCommon.CompairString(gvIcode, Icode) = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                            grow.Cells(colfatpers).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), 2)
                            grow.Cells(colfatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(gvIcode, clsCommon.myCstr(grow.Cells(coluom).Value), clsCommon.myCdbl(grow.Cells(colqty).Value), clsCommon.myCdbl(grow.Cells(colfatpers).Value), Nothing) ' Math.Round((clsCommon.myCdbl(grow.Cells(colqty).Value) * clsCommon.myCdbl(grow.Cells(colfatpers).Value)) / 100, DecimalPoint)
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                            grow.Cells(colsnfpers).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), 2)
                            grow.Cells(colsnfkg).Value = clsBOM.GetFatSNFKG_AfterConversion(gvIcode, clsCommon.myCstr(grow.Cells(coluom).Value), clsCommon.myCdbl(grow.Cells(colqty).Value), clsCommon.myCdbl(grow.Cells(colsnfpers).Value), Nothing) ' Math.Round((clsCommon.myCdbl(grow.Cells(colqty).Value) * clsCommon.myCdbl(grow.Cells(colfatpers).Value)) / 100, DecimalPoint)
                        End If
                    End If
                Next
                isCellValueChanged = False
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.FormatString = ""
        reposno.Name = colSno
        reposno.Width = 60
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposno)
        reposno = Nothing

        Dim repoicode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoicode.FormatString = ""
        repoicode.Name = colitemcode
        repoicode.Width = 100
        repoicode.HeaderText = "Item Code"
        repoicode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoicode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoicode)
        repoicode = Nothing

        Dim repoiname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoiname.FormatString = ""
        repoiname.Name = colitemname
        repoiname.Width = 200
        repoiname.HeaderText = "Description"
        repoiname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoiname)
        repoiname = Nothing

        Dim repoavail As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoavail.FormatString = ""
        repoavail.Name = colTolerance
        repoavail.IsVisible = False
        repoavail.ReadOnly = True
        repoavail.HeaderText = "Tolerance"
        repoavail.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoavail)
        repoavail = Nothing

        Dim repoitype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoitype.FormatString = ""
        repoitype.Name = colitemtype
        repoitype.Width = 100
        repoitype.HeaderText = "Item Type"
        repoitype.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoitype)
        repoitype = Nothing


        Dim repoIsSerItem As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsSerItem.HeaderText = "Is Batch Item"
        repoIsSerItem.Name = colIsBatchItem
        repoIsSerItem.ReadOnly = True
        repoIsSerItem.IsVisible = False
        repoIsSerItem.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoIsSerItem)


        Dim repoPtype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPtype.FormatString = ""
        repoPtype.Name = colitemproducttype
        repoPtype.Width = 100
        repoPtype.HeaderText = "Product Type"
        repoPtype.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoPtype)
        repoPtype = Nothing

        Dim repouom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouom.FormatString = ""
        repouom.Name = coluom
        repouom.Width = 80
        repouom.HeaderText = "UOM Code"
        repouom.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repouom.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repouom)
        repouom = Nothing

        Dim repouomname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repouomname.FormatString = ""
        repouomname.Name = colUOMDesc
        repouomname.Width = 100
        repouomname.HeaderText = "UOM"
        repouomname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repouomname)
        repouomname = Nothing

        Dim FrmlocType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        FrmlocType.FormatString = ""
        FrmlocType.Name = colFrm_Loc_Type
        FrmlocType.Width = 70
        FrmlocType.HeaderText = "From Location Type"
        FrmlocType.DataSource = clsProcessProductionIssueEntry.GetLocationType()
        FrmlocType.DisplayMember = "Name"
        FrmlocType.ValueMember = "Code"
        gv.MasterTemplate.Columns.Add(FrmlocType)
        FrmlocType = Nothing

        Dim bomcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcode.FormatString = ""
        bomcode.Name = colloc_code
        bomcode.Width = 100
        bomcode.HeaderText = "From Location Code"
        bomcode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        bomcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(bomcode)
        bomcode = Nothing

        Dim repolocname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocname.FormatString = ""
        repolocname.Name = colloc_name
        repolocname.Width = 200
        repolocname.HeaderText = "From Location"
        repolocname.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repolocname)
        repolocname = Nothing

        Dim FrmlocType1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        FrmlocType1.FormatString = ""
        FrmlocType1.Name = colTo_Loc_Type
        FrmlocType1.Width = 70
        FrmlocType1.HeaderText = "To Location Type"
        FrmlocType1.DataSource = clsProcessProductionIssueEntry.GetLocationType()
        FrmlocType1.DisplayMember = "Name"
        FrmlocType1.ValueMember = "Code"
        gv.MasterTemplate.Columns.Add(FrmlocType1)
        FrmlocType1 = Nothing

        Dim bomcodeto As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        bomcodeto.FormatString = ""
        bomcodeto.Name = colToloc_code
        bomcodeto.Width = 100
        bomcodeto.HeaderText = "To Location Code"
        bomcodeto.HeaderImage = Global.ERP.My.Resources.Resources.search4
        bomcodeto.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(bomcodeto)
        bomcodeto = Nothing

        Dim repolocnameto As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repolocnameto.FormatString = ""
        repolocnameto.Name = colToloc_name
        repolocnameto.Width = 200
        repolocnameto.HeaderText = "To Location"
        repolocnameto.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repolocnameto)
        repolocnameto = Nothing

        repoavail = New GridViewDecimalColumn()
        repoavail.FormatString = ""
        repoavail.Name = colavailqty
        repoavail.Width = 80
        repoavail.HeaderText = "Available Qty"
        repoavail.ReadOnly = True
        repoavail.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repoavail)
        repoavail = Nothing

        Dim repoavailfat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoavailfat.FormatString = "{0:n2}"
        repoavailfat.Name = colavailfatpers
        repoavailfat.Width = 80
        repoavailfat.HeaderText = "Avail. FAT%"
        repoavailfat.ReadOnly = True
        repoavailfat.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoavailfat)
        repoavailfat = Nothing

        Dim repoavailfatkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoavailfatkg.FormatString = ""
        repoavailfatkg.Name = colavailfatkg
        repoavailfatkg.Width = 80
        repoavailfatkg.HeaderText = "Avail. FAT KG"
        repoavailfatkg.ReadOnly = True
        repoavailfatkg.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repoavailfatkg)
        repoavailfatkg = Nothing

        Dim repoavailsnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoavailsnf.FormatString = "{0:n2}"
        repoavailsnf.Name = colavailsnfpers
        repoavailsnf.Width = 80
        repoavailsnf.HeaderText = "Avail. SNF%"
        repoavailsnf.ReadOnly = True
        repoavailsnf.DecimalPlaces = 2
        gv.MasterTemplate.Columns.Add(repoavailsnf)
        repoavailsnf = Nothing

        Dim repoavailsnfkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoavailsnfkg.FormatString = ""
        repoavailsnfkg.Name = colavailsnfkg
        repoavailsnfkg.Width = 80
        repoavailsnfkg.HeaderText = "Avail. SNF KG"
        repoavailsnfkg.ReadOnly = True
        repoavailsnfkg.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repoavailsnfkg)
        repoavailsnfkg = Nothing
        '-------------------------------------------------

        Dim reporeqqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        reporeqqty.FormatString = ""
        reporeqqty.Name = colrequrdqty
        reporeqqty.Width = 80
        reporeqqty.HeaderText = "Required Quantity"
        reporeqqty.DecimalPlaces = DecimalPoint
        reporeqqty.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reporeqqty)
        reporeqqty = Nothing

        Dim repoqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.Name = colqty
        repoqty.Width = 80
        repoqty.HeaderText = "Issue Quantity"
        repoqty.DecimalPlaces = DecimalPoint
        gv.MasterTemplate.Columns.Add(repoqty)
        repoqty = Nothing

        Dim repofat As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofat.FormatString = "{0:n2}"
        repofat.Name = colfatpers
        repofat.Width = 80
        repofat.HeaderText = "FAT%"
        repofat.DecimalPlaces = 2
        repofat.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repofat)
        repofat = Nothing

        Dim repofatkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        repofatkg.FormatString = ""
        repofatkg.Name = colfatkg
        repofatkg.Width = 80
        repofatkg.HeaderText = "FAT KG"
        repofatkg.DecimalPlaces = DecimalPoint
        repofatkg.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repofatkg)
        repofatkg = Nothing

        Dim reposnf As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnf.FormatString = "{0:n2}"
        reposnf.Name = colsnfpers
        reposnf.Width = 80
        reposnf.HeaderText = "SNF%"
        reposnf.DecimalPlaces = 2
        reposnf.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposnf)
        reposnf = Nothing

        Dim reposnfkg As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposnfkg.FormatString = ""
        reposnfkg.Name = colsnfkg
        reposnfkg.Width = 80
        reposnfkg.HeaderText = "SNF KG"
        reposnfkg.DecimalPlaces = DecimalPoint
        reposnfkg.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reposnfkg)
        reposnfkg = Nothing

        Dim reporem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporem.FormatString = ""
        reporem.Name = colrem
        reporem.Width = 150
        reporem.MaxLength = 200
        reporem.HeaderText = "Remarks"
        gv.MasterTemplate.Columns.Add(reporem)
        reporem = Nothing


        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.EnableFiltering = False
        gv.Rows.AddNew()

        isInsideLoadData = True
        gv.Rows(0).Cells(colFrm_Loc_Type).Value = "MAIN"
        gv.Rows(0).Cells(colTo_Loc_Type).Value = "MAIN"
        isInsideLoadData = False
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
        If settTankerDispatchAvgFATSNFPer Then
            repoupper1.DecimalPlaces = 10
            repoupper1.FormatString = "{0:n2}"
            repoupper1.ReadOnly = True
        Else
            repoupper1.DecimalPlaces = 2
            repoupper1.ReadOnly = False
        End If
        repoupper1.Maximum = 100
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

        gv_qc.AllowDeleteRow = False
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

    Function LoadComboboxOK() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'Ok' as Code,'Ok' as Name union all select 'Not Ok' as Code,'Not Ok' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData(False)
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
                Return False
            End If
            If clsCommon.myLen(fndSection.Value) <= 0 AndAlso chkBO.IsChecked Then
                Errorcontrol.SetError(TxtSection, "Select section detail.")
                RadPageView1.SelectedPage = RadPageViewPage1
                fndSection.Select()
                fndSection.Focus()
                Throw New Exception("Select section detail.")
            Else
                Errorcontrol.ResetError(TxtSection)
            End If

            If clsCommon.myLen(txtbatchorder.Value) <= 0 AndAlso chkBO.IsChecked Then
                Errorcontrol.SetError(txtbatchorder, "Select batch order detail.")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtbatchorder.Select()
                txtbatchorder.Focus()
                Throw New Exception("Select batch order detail.")
            Else
                Errorcontrol.ResetError(txtbatchorder)
            End If

            If clsCommon.myLen(txtmain_Loc_Code.Value) <= 0 Then
                Errorcontrol.SetError(txtmain_Loc_Code, "Select Location First.")
                RadPageView1.SelectedPage = RadPageViewPage1
                txtmain_Loc_Code.Select()
                txtmain_Loc_Code.Focus()
                Throw New Exception("Select Location First.")
            Else
                Errorcontrol.ResetError(txtMain_Loc_Name)
            End If

            Dim strBatchORderExistIntPIE As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Issue_Code from TSPL_PP_ISSUE_HEAD where TSPL_PP_ISSUE_HEAD.Batch_Code='" + txtbatchorder.Value + "' and TSPL_PP_ISSUE_HEAD.Issue_Code not in ('" + txtCode.Value + "')"))
            If clsCommon.myLen(clsCommon.myCstr(strBatchORderExistIntPIE)) > 0 Then
                Errorcontrol.SetError(txtbatchorder, "Please select different Batch Order, Same Batch exists with Production Issue Entry " & strBatchORderExistIntPIE & "  ")
                txtbatchorder.Select()
                txtbatchorder.Focus()
                Throw New Exception("Please select different Batch Order, Same Batch exists with Production Issue Entry " & strBatchORderExistIntPIE & "  ")
            Else
                Errorcontrol.ResetError(txtbatchorder)
            End If


            Dim loc_code As String = ""
            Dim itemcode As String = ""
            Dim oldloc_code As String = ""
            Dim olditemcode As String = ""
            Dim availqty As Decimal
            Dim ReqQty As Decimal
            Dim requrdqty As Decimal = Nothing
            Dim qty As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing
            Dim snf_kg As Decimal = Nothing
            Dim fat_pers As Decimal = Nothing
            Dim snf_pers As Decimal = Nothing
            Dim prdcttype As String = ""
            Dim arrIcode As New List(Of String)
            Dim arrItemLocation As New List(Of String)
            For ii As Integer = 0 To gv.Rows.Count - 1
                gv.Focus()
                gv.Select()

                loc_code = clsCommon.myCstr(gv.Rows(ii).Cells(colloc_code).Value)
                itemcode = clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value)
                '=============changes by preeti gupta[21/01/2017]========

                Dim loc_type As Integer = 0
                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                    loc_type = 2
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                    loc_type = 1
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                    loc_type = 0
                End If

                If SettDisableToPickMainLocation AndAlso loc_type = 2 Then
                    Throw New Exception("You are not authorize to Pick item from Main Location" + Environment.NewLine + "Please change Location Type - Main location at Row no" & clsCommon.myCstr(ii + 1) & "  ")
                End If

                If clsCommon.myLen(itemcode) > 0 Then
                    FillAvail_Stock(ii, clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value), clsCommon.myCstr(txtmain_Loc_Code.Value), clsCommon.myCstr(gv.Rows(ii).Cells(colloc_code).Value), clsCommon.myCstr(gv.Rows(ii).Cells(colitemproducttype).Value), clsCommon.myCstr(gv.Rows(ii).Cells(coluom).Value), loc_type)
                End If

                ReqQty = clsCommon.myCdbl(gv.Rows(ii).Cells(colrequrdqty).Value)
                availqty = clsCommon.myCdbl(gv.Rows(ii).Cells(colavailqty).Value)
                qty = clsCommon.myCdbl(gv.Rows(ii).Cells(colqty).Value)
                fat_kg = clsCommon.myCdbl(gv.Rows(ii).Cells(colfatkg).Value)
                fat_pers = clsCommon.myCdbl(gv.Rows(ii).Cells(colfatpers).Value)
                snf_kg = clsCommon.myCdbl(gv.Rows(ii).Cells(colsnfkg).Value)
                snf_pers = clsCommon.myCdbl(gv.Rows(ii).Cells(colsnfpers).Value)
                prdcttype = clsCommon.myCstr(gv.Rows(ii).Cells(colitemproducttype).Value)
                requrdqty = clsCommon.myCdbl(gv.Rows(ii).Cells(colrequrdqty).Value)

                If clsCommon.myLen(itemcode) > 0 AndAlso clsCommon.myLen(loc_code) <= 0 AndAlso qty > 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv.CurrentRow = gv.Rows(ii)
                    gv.CurrentColumn = gv.Columns(colloc_code)
                    Throw New Exception("Fill from location detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If

                If clsCommon.myLen(itemcode) > 0 AndAlso Not arrIcode.Contains(itemcode) Then
                    arrIcode.Add(itemcode)
                End If


                If clsCommon.myLen(itemcode) > 0 Then
                    If qty > availqty Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colqty)
                        If Not settAllowNegativeStockInDairyProduction Then
                            Throw New Exception("Filled qty cannot be more than available qty. at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If
                    If settProductionIssueQtyTollerance <> 0 Then
                        'BHA/17/12/18-000755 by balwinder on 17/12/2018
                        Dim tQty As Decimal = qty
                        For jj As Integer = 0 To gv.Rows.Count - 1
                            If ii <> jj Then
                                If clsCommon.CompairString(itemcode, clsCommon.myCstr(gv.Rows(jj).Cells(colitemcode).Value)) = CompairStringResult.Equal Then
                                    tQty += clsCommon.myCdbl(gv.Rows(jj).Cells(colqty).Value)
                                End If
                            End If
                        Next
                        Dim PerVal As Decimal = Math.Round(ReqQty * settProductionIssueQtyTollerance / 100, 2, MidpointRounding.ToEven)
                        If tQty > (ReqQty + PerVal) OrElse tQty < (ReqQty - PerVal) Then
                            Throw New Exception("Item - " + itemcode + " Total Issued Qty - " + clsCommon.myCstr(tQty) + " should exists between [" + clsCommon.myCstr(ReqQty - PerVal) + "-" + clsCommon.myCstr((ReqQty + PerVal)) + "] . at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    Else
                        'TEC/26/07/19-000963 by balwinder on 01/08/2019
                        Dim tQty As Decimal = qty
                        For jj As Integer = 0 To gv.Rows.Count - 1
                            If ii <> jj Then
                                If clsCommon.CompairString(itemcode, clsCommon.myCstr(gv.Rows(jj).Cells(colitemcode).Value)) = CompairStringResult.Equal Then
                                    tQty += clsCommon.myCdbl(gv.Rows(jj).Cells(colqty).Value)
                                End If
                            End If
                        Next
                        Dim ProdTolerance As Decimal = 0
                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                            If MI_Consm_Type = 0 Then
                                ProdTolerance = clsCommon.myCdbl(gv.Rows(ii).Cells(colTolerance).Value)
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colitemproducttype).Value), "Milk Product") = CompairStringResult.Equal Then
                            If MP_Consm_Type = 0 Then
                                ProdTolerance = clsCommon.myCdbl(gv.Rows(ii).Cells(colTolerance).Value)
                            End If
                        Else
                            If Othr_Consm_Type = 0 Then
                                ProdTolerance = clsCommon.myCdbl(gv.Rows(ii).Cells(colTolerance).Value)
                            End If
                        End If
                        If ProdTolerance <> 0 Then
                            Dim PerVal As Decimal = Math.Round(ReqQty * ProdTolerance / 100, 2, MidpointRounding.ToEven)
                            If tQty > (ReqQty + PerVal) OrElse tQty < (ReqQty - PerVal) Then
                                Throw New Exception("Item - " + itemcode + " Total Issued Qty - " + clsCommon.myCstr(tQty) + " should exists between [" + clsCommon.myCstr(ReqQty - PerVal) + "-" + clsCommon.myCstr((ReqQty + PerVal)) + "] . at row no. " + clsCommon.myCstr(ii + 1) + "")
                            End If
                        End If
                    End If

                    If qty > 0 AndAlso clsCommon.myCBool(clsCommon.myCdbl(gv.Rows(ii).Cells(colIsBatchItem).Value)) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                            Dim arrBatchNo As List(Of clsBatchInventoryNew) = TryCast(gv.Rows(ii).Cells(colitemcode).Tag, List(Of clsBatchInventoryNew))
                            If arrBatchNo Is Nothing Then
                                Throw New Exception("Please provide Batch no for item : " + itemcode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            Else
                                Dim tQty As Decimal = 0
                                For Each objBatch As clsBatchInventoryNew In arrBatchNo
                                    tQty += objBatch.Qty
                                Next
                                If Math.Abs(tQty - qty) > 0.01 Then
                                    Throw New Exception("Item : " + itemcode + " Entered Qty " + clsCommon.myCstr(qty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                End If
                            End If
                        Else
                            Dim arrBatchNo As List(Of clsBatchInventory) = TryCast(gv.Rows(ii).Cells(colitemcode).Tag, List(Of clsBatchInventory))
                            If arrBatchNo Is Nothing Then
                                Throw New Exception("Please provide Batch no for item : " + itemcode + " . At Line No" + clsCommon.myCstr(ii + 1))
                            Else
                                Dim tQty As Decimal = 0
                                For Each objBatch As clsBatchInventory In arrBatchNo
                                    tQty += objBatch.Qty
                                Next
                                If Math.Abs(tQty - qty) > 0.01 Then
                                    Throw New Exception("Item : " + itemcode + " Entered Qty " + clsCommon.myCstr(qty) + Environment.NewLine + "And Batchwise Qty " + clsCommon.myCstr(tQty) + " . At Line No" + clsCommon.myCstr(ii + 1))
                                End If
                            End If
                        End If

                    End If
                End If

                If clsCommon.myLen(itemcode) > 0 AndAlso fat_pers <= 0 Then
                    gv.Rows(ii).Cells(colfatpers).Value = clsCommon.myCdbl(clsBOM.GetFAT_PERS(itemcode))
                End If
                gv.Rows(ii).Cells(colfatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(itemcode, clsCommon.myCstr(gv.Rows(ii).Cells(coluom).Value), qty, clsCommon.myCdbl(gv.Rows(ii).Cells(colfatpers).Value), Nothing) ' (clsCommon.myCdbl(qty) * clsCommon.myCdbl(gv.Rows(ii).Cells(colfatpers).Value)) / 100

                If clsCommon.myLen(itemcode) > 0 AndAlso snf_pers <= 0 Then
                    gv.Rows(ii).Cells(colsnfpers).Value = clsCommon.myCdbl(clsBOM.GetSNF_PERS(itemcode))
                End If
                gv.Rows(ii).Cells(colsnfkg).Value = clsBOM.GetFatSNFKG_AfterConversion(itemcode, clsCommon.myCstr(gv.Rows(ii).Cells(coluom).Value), qty, clsCommon.myCdbl(gv.Rows(ii).Cells(colsnfpers).Value), Nothing) '' (clsCommon.myCdbl(qty) * clsCommon.myCdbl(gv.Rows(ii).Cells(colsnfpers).Value)) / 100
                ''BHA/19/03/19-000848 by balwinder on 08/04/2019.
                If clsCommon.myLen(itemcode) > 0 AndAlso clsCommon.myLen(gv.Rows(ii).Cells(colToloc_code).Value) <= 0 AndAlso qty > 0 Then ''do this validation at post
                    RadPageView1.SelectedPage = RadPageViewPage1
                    Dim sec_loc As String = ""
                    sec_loc = clsProcessProductionIssueEntry.GetBOSectionLocationCode(txtbatchorder.Value, txtmain_Loc_Code.Value, itemcode)
                    If clsCommon.myLen(sec_loc) > 0 Then
                        gv.Rows(ii).Cells(colToloc_code).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 location_code from tspl_location_master where location_code in (" + sec_loc + ")"))
                        gv.Rows(ii).Cells(colToloc_name).Value = clsCommon.myCstr(clsLocation.GetName(clsCommon.myCstr(gv.Rows(ii).Cells(colToloc_code).Value), Nothing))
                    End If
                End If

                For jj As Integer = ii + 1 To gv.Rows.Count - 1
                    oldloc_code = clsCommon.myCstr(gv.Rows(jj).Cells(colloc_code).Value)
                    olditemcode = clsCommon.myCstr(gv.Rows(jj).Cells(colitemcode).Value)

                    If clsCommon.myLen(loc_code) > 0 AndAlso clsCommon.myLen(itemcode) > 0 AndAlso clsCommon.CompairString(loc_code, oldloc_code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(itemcode, olditemcode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(coluom).Value), clsCommon.myCstr(gv.Rows(jj).Cells(coluom).Value)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colToloc_code).Value), clsCommon.myCstr(gv.Rows(jj).Cells(colToloc_code).Value)) = CompairStringResult.Equal Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(ii)
                        gv.CurrentColumn = gv.Columns(colitemcode)
                        Throw New Exception("Duplicate data found at row no. " + clsCommon.myCstr(jj + 1) + "")
                    End If
                Next
            Next

            If arrIcode Is Nothing OrElse arrIcode.Count <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gv.CurrentRow = gv.Rows(0)
                gv.CurrentColumn = gv.Columns(colitemcode)
                Throw New Exception("Fill atleast one item detail.")
            End If
            '---validation not check at save it check at post--------------qc grid----------------------------------------------
            Dim paramcode As String = ""
            Dim nature As String = ""
            Dim range1 As Decimal = Nothing
            Dim range2 As Decimal = Nothing
            Dim status As String = ""
            Dim value1 As String = ""
            Dim value2 As String = ""
            Dim QC_Range As Decimal = Nothing
            Dim QC_Value As String = ""
            Dim QC_Status As String = ""


            For ii As Integer = 0 To gv_qc.Rows.Count - 1
                paramcode = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparamcode).Value)
                nature = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_nature).Value)
                range1 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange1).Value)
                range2 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange2).Value)
                status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCstatus).Value)
                value1 = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCvalue1).Value)
                value2 = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCvalue2).Value)
                QC_Range = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCRange).Value)
                QC_Status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCOutStatus).Value)
                QC_Value = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCValue).Value)

                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Boolean") = CompairStringResult.Equal Then
                    gv_qc.Rows(ii).Cells(colQCRange).Value = Nothing
                    gv_qc.Rows(ii).Cells(colQCValue).Value = Nothing
                End If

                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Range") = CompairStringResult.Equal Then
                    gv_qc.Rows(ii).Cells(colQCOutStatus).Value = ""
                    gv_qc.Rows(ii).Cells(colQCValue).Value = Nothing
                    If QC_Range < 0 Then
                        gv_qc.Rows(ii).Cells(colQCRange).Value = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange1).Value)
                        gv_qc.CurrentRow = gv_qc.Rows(ii)
                        CorrectFATSNFFromQC()
                    End If

                End If

                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Alphanumeric") = CompairStringResult.Equal Then
                    gv_qc.Rows(ii).Cells(colQCRange).Value = Nothing
                    gv_qc.Rows(ii).Cells(colQCOutStatus).Value = Nothing
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Function PostAllowToSave() As Boolean
        Try
            Dim arrGVIcode As List(Of String) = Nothing
            arrGVIcode = New List(Of String)
            Dim arrMilkIcode As List(Of String) = Nothing
            arrMilkIcode = New List(Of String)
            Dim MilkProduct As String = Nothing


            For Each grow As GridViewRowInfo In gv.Rows
                Dim icode As String = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                Dim to_location_code As String = clsCommon.myCstr(grow.Cells(colToloc_code).Value)

                Dim loc_type As Integer = 0
                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                    loc_type = 2
                ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                    loc_type = 1
                ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                    loc_type = 0
                End If

                FillAvail_Stock(grow.Index, icode, txtmain_Loc_Code.Value, clsCommon.myCstr(grow.Cells(colloc_code).Value), clsCommon.myCstr(grow.Cells(colitemproducttype).Value), clsCommon.myCstr(grow.Cells(coluom).Value), loc_type)
                Dim availstck As Decimal = clsCommon.myCdbl(grow.Cells(colavailqty).Value)
                Dim issueqty As Decimal = clsCommon.myCdbl(grow.Cells(colqty).Value)

                If clsCommon.myLen(icode) > 0 Then

                    If issueqty > availstck Then
                        If Not settAllowNegativeStockInDairyProduction Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv.CurrentRow = gv.Rows(grow.Index)
                            gv.CurrentColumn = gv.Columns(colqty)
                            isInsideLoadData = False
                            Throw New Exception("Issue qty is more than available stock at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    MilkProduct = clsItemMaster.GetItemProductType(icode, Nothing)

                    Dim qry As String = "select count(*) from TSPL_ITEM_QC_PARAMETER_MASTER where item_code='" + icode + "'"
                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) > 0 AndAlso Not arrGVIcode.Contains(icode) Then
                        arrGVIcode.Add(icode)
                    End If

                    '==============if item is "Milk" of "Milk Product" and not have parameters then add this item to array of milk
                    If (clsCommon.CompairString(MilkProduct, "MI") = CompairStringResult.Equal OrElse clsCommon.CompairString(MilkProduct, "MP") = CompairStringResult.Equal) AndAlso Not arrGVIcode.Contains(icode) Then
                        If Not arrMilkIcode.Contains(icode) Then
                            arrMilkIcode.Add(icode)
                        End If
                    End If
                    '======end here==========================
                End If
                '========================fill section detail=========================
                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(to_location_code) <= 0 Then
                    isInsideLoadData = True
                    Dim sec_loc As String = ""
                    sec_loc = clsProcessProductionIssueEntry.GetBOSectionLocationCode(txtbatchorder.Value, txtmain_Loc_Code.Value, icode)

                    If clsCommon.myLen(sec_loc) > 0 Then
                        grow.Cells(colToloc_code).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 location_code from tspl_location_master where location_code in (" + sec_loc + ")"))
                        to_location_code = clsCommon.myCstr(grow.Cells(colToloc_code).Value)
                        grow.Cells(colToloc_name).Value = clsCommon.myCstr(clsLocation.GetName(to_location_code, Nothing))
                    End If
                    If clsCommon.myLen(to_location_code) <= 0 Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        gv.CurrentRow = gv.Rows(grow.Index)
                        gv.CurrentColumn = gv.Columns(colToloc_code)
                        isInsideLoadData = False
                        Throw New Exception("Consumption location not found at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    isInsideLoadData = False
                End If


            Next

            Dim paramcode As String = ""
            Dim nature As String = ""
            Dim range1 As Decimal = Nothing
            Dim range2 As Decimal = Nothing
            Dim status As String = ""
            Dim value1 As String = ""
            Dim value2 As String = ""
            Dim QC_Range As Decimal = Nothing
            Dim QC_Value As String = ""
            Dim QC_Status As String = ""
            Dim arrParameter As New List(Of String)
            arrParameter = New List(Of String)
            Dim arrParamIcode As New List(Of String)
            arrParamIcode = New List(Of String)
            MilkProduct = Nothing


            For ii As Integer = 0 To gv_qc.Rows.Count - 1
                paramcode = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparamcode).Value)

                If clsCommon.myLen(paramcode) > 0 AndAlso Not arrParameter.Contains(paramcode) Then
                    arrParameter.Add(paramcode)
                End If

                Dim icode As String = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value)

                If clsCommon.myLen(icode) > 0 AndAlso clsCommon.myLen(paramcode) > 0 Then
                    MilkProduct = clsItemMaster.GetItemProductType(icode, Nothing)


                    If Not arrParamIcode.Contains(icode) Then
                        arrParamIcode.Add(icode)
                    End If
                End If


                nature = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_nature).Value)
                range1 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange1).Value)
                range2 = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCrange2).Value)
                status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCstatus).Value)
                value1 = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCvalue1).Value)
                value2 = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCvalue2).Value)
                QC_Range = clsCommon.myCdbl(gv_qc.Rows(ii).Cells(colQCRange).Value)
                QC_Status = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCOutStatus).Value)
                QC_Value = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCValue).Value)

                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Boolean") = CompairStringResult.Equal AndAlso clsCommon.myLen(status) > 0 AndAlso clsCommon.CompairString(status, "None") <> CompairStringResult.Equal AndAlso (clsCommon.myLen(QC_Status) <= 0 Or QC_Status = "None") Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv_qc.Rows(ii).Cells(colQCRange).Value = Nothing
                    gv_qc.Rows(ii).Cells(colQCValue).Value = Nothing
                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                    gv_qc.CurrentColumn = gv_qc.Columns(colQCOutStatus)
                    Throw New Exception("Fill Status at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If

                '=====================range can be 0
                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Range") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(QC_Range) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv_qc.Rows(ii).Cells(colQCOutStatus).Value = ""
                    gv_qc.Rows(ii).Cells(colQCValue).Value = Nothing
                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                    gv_qc.CurrentColumn = gv_qc.Columns(colQCRange)
                    If Not clsCommon.MyMessageBoxShow("Actual Range at row no. " + clsCommon.myCstr(ii + 1) + " is 0,Do you want to continue?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Return False
                    End If
                End If

                If clsCommon.myLen(paramcode) > 0 AndAlso clsCommon.CompairString(nature, "Alphanumeric") = CompairStringResult.Equal AndAlso clsCommon.myLen(value1) > 0 AndAlso clsCommon.myLen(QC_Value) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv_qc.Rows(ii).Cells(colQCRange).Value = Nothing
                    gv_qc.Rows(ii).Cells(colQCOutStatus).Value = Nothing
                    gv_qc.CurrentRow = gv_qc.Rows(ii)
                    gv_qc.CurrentColumn = gv_qc.Columns(colQCValue)
                    Throw New Exception("Fill value at row no. " + clsCommon.myCstr(ii + 1) + "")
                End If
            Next
            '==============if item is "Milk" of "Milk Product" and have parameters but not fill in qc grid then add this item to array of milk
            If arrGVIcode IsNot Nothing AndAlso arrGVIcode.Count > 0 Then
                For Each Str As String In arrGVIcode
                    If Not arrParamIcode.Contains(Str) AndAlso Not arrMilkIcode.Contains(Str) Then
                        arrMilkIcode.Add(Str)
                    End If
                Next
            End If
            '============end here

            If (arrGVIcode IsNot Nothing AndAlso arrGVIcode.Count > 0) AndAlso (arrParameter Is Nothing OrElse arrParameter.Count <= 0) Then
                RadPageView1.SelectedPage = RadPageViewPage2
                btngo.PerformClick()
                gv_qc.Focus()
                gv_qc.Select()
                Throw New Exception("Fill QC item parameters.")
            End If

            '=================message send in case of milk item when no qc record found.
            MilkProduct = Nothing
            If arrMilkIcode IsNot Nothing AndAlso arrMilkIcode.Count > 0 Then
                For Each Str As String In arrMilkIcode
                    MilkProduct = MilkProduct + ", " + clsCommon.myCstr(clsItemMaster.GetItemName(Str, Nothing))
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                btngo.PerformClick()
                gv_qc.Focus()
                gv_qc.Select()
                Throw New Exception("Fill QC for items (" + MilkProduct + ").")
            End If
            ''ERO/11/06/21-001408 by balwinder on 14/06/2021
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select STAGE_PROCESS_CODE from TSPL_PP_STAGE_PROCESS_HEAD where Main_Batch_Code='" + txtbatchorder.Value + "'"))
            If clsCommon.myLen(qry) > 0 Then
                clsCommon.MyMessageBoxShow("Please Recreate Stage Process [" + qry + "]", Me.Text)
            End If
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PROD_ENTRY_CODE from TSPL_PP_PRODUCTION_ENTRY where Batch_Code='" + txtbatchorder.Value + "'"))
            If clsCommon.myLen(qry) > 0 Then
                clsCommon.MyMessageBoxShow("Please Recreate Production Entry [" + qry + "]", Me.Text)
            End If

            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Standardization_Code from TSPL_PP_STANDARDIZATION_HEAD where Main_Batch_Code='" + txtbatchorder.Value + "'"))
            If clsCommon.myLen(qry) > 0 Then
                clsCommon.MyMessageBoxShow("Please Recreate Production Std. [" + qry + "]", Me.Text)
            End If
            qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select QC_Code from TSPL_PP_STD_FINALQC_HEAD where Main_Batch_Code='" + txtbatchorder.Value + "'"))
            If clsCommon.myLen(qry) > 0 Then
                clsCommon.MyMessageBoxShow("Please Recreate Production Std. Final QC [" + qry + "]", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        Return True
    End Function

    Private Sub SaveData(ByVal isPost As Boolean)
        Dim obj As New clsProcessProductionIssueEntry()
        Try
            If clsCommon.myLen(txtfrmsub.Value) <= 0 Then
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value), "MAIN") <> CompairStringResult.Equal Then
                        txtfrmsub.Value = clsCommon.myCstr(grow.Cells(colloc_code).Value)
                        txtfrmsub_name.Text = clsCommon.myCstr(grow.Cells(colloc_name).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                            btnFrm_Sectn.IsChecked = True
                            btnFrm_SubLoc.IsChecked = False
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                            btnFrm_SubLoc.IsChecked = True
                            btnFrm_Sectn.IsChecked = False
                        End If
                        Exit For
                    End If
                Next
            End If
            If clsCommon.myLen(txttosub.Value) <= 0 Then
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colTo_Loc_Type).Value), "MAIN") <> CompairStringResult.Equal Then
                        txttosub.Value = clsCommon.myCstr(grow.Cells(colToloc_code).Value)
                        txttosub_name.Text = clsCommon.myCstr(grow.Cells(colToloc_name).Value)
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colTo_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                            btnTo_Sectn.IsChecked = True
                            btnTo_SubLoc.IsChecked = False
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colTo_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                            btnTo_SubLoc.IsChecked = True
                            btnTo_Sectn.IsChecked = False
                        End If
                        Exit For
                    End If
                Next
            End If
            '==when no sub-loc and section found then put main location.
            If clsCommon.myLen(txtfrmsub.Value) <= 0 And gv.Rows.Count > 0 Then
                txtfrmsub.Value = clsCommon.myCstr(gv.Rows(0).Cells(colloc_code).Value)
                txtfrmsub_name.Text = clsCommon.myCstr(gv.Rows(0).Cells(colloc_name).Value)

                btnFrm_SubLoc.IsChecked = True
                btnFrm_Sectn.IsChecked = False
            End If
            If clsCommon.myLen(txttosub.Value) <= 0 And gv.Rows.Count > 0 Then
                txttosub.Value = clsCommon.myCstr(gv.Rows(0).Cells(colToloc_code).Value)
                txttosub_name.Text = clsCommon.myCstr(gv.Rows(0).Cells(colToloc_name).Value)

                btnTo_SubLoc.IsChecked = True
                btnTo_Sectn.IsChecked = False
            End If
            '============================================================

            obj = New clsProcessProductionIssueEntry()

            obj.issuecode = clsCommon.myCstr(txtCode.Value)
            obj.issue_date = clsCommon.myCDate(dtpDate.Value)
            obj.issuedesc = clsCommon.myCstr(txtdesc.Text).Replace("'", "`")
            obj.status = clsCommon.myCstr(cboStatus.Text)
            obj.batch_code = clsCommon.myCstr(txtbatchorder.Value)
            obj.frm_loc_code = clsCommon.myCstr(txtfrmsub.Value)
            obj.to_loc_code = clsCommon.myCstr(txttosub.Value)
            obj.Main_Loc_Code = clsCommon.myCstr(txtmain_Loc_Code.Value)
            obj.Rbtn_Frm_Sub = IIf(btnFrm_Sectn.IsChecked = True, 0, 1)
            obj.Rbtn_To_Sub = IIf(btnTo_Sectn.IsChecked = True, 0, 1)
            obj.Against_BO = CInt(IIf(chkBO.IsChecked = True, 1, 0))
            obj.Section_Code = clsCommon.myCstr(fndSection.Value)
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            obj.ManualBatchNo = clsCommon.myCstr(TxtManualBatchNo.Text)
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            obj.LINE_NO = clsCommon.myCstr(lblLineNo.Text)
            obj.CostCenterCode = clsCommon.myCstr(LblCostCenterCode.Text)
            obj.ProfitCenterCode = clsCommon.myCstr(lblProfitCenterCode.Text)
            ''----------------
            obj.Is_Job_Work_Inward = chkJobWorkInward.Checked
            obj.is_post = "0"
            If clsCommon.CompairString(UsLock1.Status, ERPTransactionStatus.Approved) = CompairStringResult.Equal Then
                'obj.is_post = "1"
            End If

            obj.ArrItem = New List(Of clsProcessProductionIssueItemDetail)
            obj.ArrQC = New List(Of clsProcessProductionIssueQCDetail)

            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsProcessProductionIssueItemDetail()

                objtr.sno = CInt(grow.Cells(colSno).Value)
                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                    objtr.From_SubLocation_YN = 2
                Else
                    objtr.From_SubLocation_YN = CInt(clsCommon.myCdbl(IIf(clsCommon.myCstr(grow.Cells(colFrm_Loc_Type).Value) = "SUB", 1, 0)))
                End If

                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colTo_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                    objtr.To_SubLocation_YN = 2
                Else
                    objtr.To_SubLocation_YN = CInt(clsCommon.myCdbl(IIf(clsCommon.myCstr(grow.Cells(colTo_Loc_Type).Value) = "SUB", 1, 0)))
                End If

                objtr.frm_loc_code = clsCommon.myCstr(grow.Cells(colloc_code).Value)
                objtr.to_loc_code = clsCommon.myCstr(grow.Cells(colToloc_code).Value)
                objtr.itemcode = clsCommon.myCstr(grow.Cells(colitemcode).Value)
                objtr.uom_code = clsCommon.myCstr(grow.Cells(coluom).Value)
                objtr.avail_qty = clsCommon.myCdbl(grow.Cells(colavailqty).Value)
                objtr.avail_fat_kg = clsCommon.myCdbl(grow.Cells(colavailfatkg).Value)
                objtr.avail_fat_pers = clsCommon.myCdbl(grow.Cells(colavailfatpers).Value)
                objtr.avail_snf_kg = clsCommon.myCdbl(grow.Cells(colavailsnfkg).Value)
                objtr.avail_snf_pers = clsCommon.myCdbl(grow.Cells(colavailsnfpers).Value)
                objtr.req_qty = clsCommon.myCdbl(grow.Cells(colrequrdqty).Value)
                objtr.issue_qty = clsCommon.myCdbl(grow.Cells(colqty).Value)
                objtr.fat_kg = clsCommon.myCdbl(grow.Cells(colfatkg).Value)
                objtr.fat_pers = clsCommon.myCdbl(grow.Cells(colfatpers).Value)
                objtr.snf_kg = clsCommon.myCdbl(grow.Cells(colsnfkg).Value)
                objtr.snf_pers = clsCommon.myCdbl(grow.Cells(colsnfpers).Value)
                objtr.remarks = clsCommon.myCstr(grow.Cells(colrem).Value).Replace("'", "`")
                objtr.arrBatchItem = TryCast(grow.Cells(colitemcode).Tag, List(Of clsBatchInventory))
                objtr.arrBatchItemNew = TryCast(grow.Cells(colitemcode).Tag, List(Of clsBatchInventoryNew))
                If clsCommon.myLen(objtr.itemcode) > 0 Then
                    obj.ArrItem.Add(objtr)
                End If
            Next

            For Each grow As GridViewRowInfo In gv_qc.Rows
                Dim objtr As New clsProcessProductionIssueQCDetail()

                objtr.sno = CInt(grow.Cells(colQCsno).Value)
                objtr.frm_loc_code = clsCommon.myCstr(grow.Cells(colQCloc_code).Value)
                objtr.to_loc_code = clsCommon.myCstr(grow.Cells(colQCToloc_code).Value)
                objtr.itemcode = clsCommon.myCstr(grow.Cells(colQCitemcode).Value)
                objtr.param_code = clsCommon.myCstr(grow.Cells(colQCparamcode).Value)
                objtr.lrange = clsCommon.myCdbl(grow.Cells(colQCrange1).Value)
                objtr.urange = clsCommon.myCdbl(grow.Cells(colQCrange2).Value)
                objtr.value1 = clsCommon.myCstr(grow.Cells(colQCvalue1).Value)
                objtr.value2 = clsCommon.myCstr(grow.Cells(colQCvalue2).Value)
                objtr.status_grid = clsCommon.myCstr(grow.Cells(colQCstatus).Value)
                objtr.QCRange = clsCommon.myCdbl(grow.Cells(colQCRange).Value)
                objtr.QCStatus = clsCommon.myCstr(grow.Cells(colQCOutStatus).Value)
                objtr.QCValue = clsCommon.myCstr(grow.Cells(colQCValue).Value)

                If objtr.status_grid = "None" Then
                    objtr.status_grid = ""
                End If
                If objtr.QCStatus = "None" Then
                    objtr.QCStatus = ""
                End If

                objtr.remarks = clsCommon.myCstr(grow.Cells(colQCremarks).Value).Replace("'", "`")

                If clsCommon.myLen(objtr.param_code) > 0 Then
                    obj.ArrQC.Add(objtr)
                End If
            Next


            If clsProcessProductionIssueEntry.SaveData(isNewEntry, obj) Then
                If isPost = False Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                End If

                txtCode.Value = obj.issuecode

                UcAttachment1.SaveData(txtCode.Value)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
            isSavedSuccess = True

        Catch ex As Exception
            isSavedSuccess = False
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Select()
                txtCode.Focus()
                Errorcontrol.SetError(txtCode, "Select issue detail for deletion")
                Throw New Exception("Select issue detail for deletion")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            qry = "select count(*) from TSPL_PP_ISSUE_HEAD where issue_code='" + txtCode.Value + "'"
            check = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Select()
                txtCode.Focus()
                Errorcontrol.SetError(txtCode, "Issue code not found.")
                Throw New Exception("Issue code not found.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If clsProcessProductionIssueEntry.DeleteData(txtCode.Value) Then
                myMessages.delete()
                FunReset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub PostData()
        Try

            If clsCommon.myLen(arrLoc) <= 0 Then
                Throw New Exception("No location rights.")
            End If

            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Select()
                txtCode.Focus()
                Errorcontrol.SetError(txtCode, "Select issue detail for posting.")
                Throw New Exception("Select issue detail for posting.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            qry = "select count(*) from TSPL_PP_ISSUE_HEAD where issue_code='" + txtCode.Value + "'"
            check = clsDBFuncationality.getSingleValue(qry)

            If check <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Select()
                txtCode.Focus()
                Errorcontrol.SetError(txtCode, "Issue code not found.")
                Throw New Exception("Issue code not found.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If clsCommon.CompairString(cboStatus.Text, "Approved") <> CompairStringResult.Equal Then
                cboStatus.Text = "Approved"
                '    RadPageView1.SelectedPage = RadPageViewPage1
                '    Errorcontrol.SetError(cboStatus, "Select issue status Approved,before post.")
                '    Throw New Exception("Select issue status Approved,before post.")
                'Else
                '    Errorcontrol.ResetError(cboStatus)
            End If

            If PostAllowToSave() Then
                '================check if all qty of BO is issued only then it is closed from here otherwise no close==(req qty-posted qty-current qty)>unposted qty=======
                'qry = "select a.batch_code  from "
                'qry += "(select TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,(isnull(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,0))-sum(isnull(TSPL_PP_ISSUE_ITEM_DETAIL.qty,0))-(isnull(TSPL_PP_ISSUE_ITEM_DETAIL2.qty,0)) as Req_qty,sum(isnull(TSPL_PP_ISSUE_ITEM_DETAIL1.qty,0)) as qty from TSPL_PP_ISSUE_HEAD "
                'qry += " left outer join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code=TSPL_PP_ISSUE_HEAD.Issue_Code and TSPL_PP_ISSUE_HEAD.Is_post='1' left outer join TSPL_PP_ISSUE_ITEM_DETAIL as TSPL_PP_ISSUE_ITEM_DETAIL1 on TSPL_PP_ISSUE_ITEM_DETAIL1.Issue_Code=TSPL_PP_ISSUE_HEAD.Issue_Code and TSPL_PP_ISSUE_HEAD.Is_post='0' left outer join TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL on TSPL_PP_ISSUE_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code "
                'qry += " left outer join (select Issue_Code,Item_Code,SUM(isnull(qty,0)) as qty from TSPL_PP_ISSUE_ITEM_DETAIL where Issue_Code='" + txtCode.Value + "' group by Issue_Code,Item_Code) as TSPL_PP_ISSUE_ITEM_DETAIL2 on TSPL_PP_ISSUE_ITEM_DETAIL2.Issue_Code=TSPL_PP_ISSUE_HEAD.Issue_Code and TSPL_PP_ISSUE_ITEM_DETAIL2.Item_Code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code "
                'qry += " where TSPL_PP_ISSUE_HEAD.batch_code in ('" + txtbatchorder.Value + "') group by TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity,TSPL_PP_ISSUE_ITEM_DETAIL2.qty)a where a.req_qty>0"
                'Dim xbatchorder As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                Dim isclosebo As Boolean = True
                'If xbatchorder IsNot Nothing OrElse clsCommon.myLen(xbatchorder) > 0 Then
                '    isclosebo = False
                'End If

                isclosebo = False ''not close from issue screen,so that set it to as false
                '=================================================================================

                If Not myMessages.postConfirm() Then
                    Return
                End If

                If AllowToSave() Then
                    SaveData(True)

                    'If Not isSavedSuccess Then
                    '    Exit Sub
                    'End If

                    If clsProcessProductionIssueEntry.PostData(Me.Form_ID, isclosebo, arrLoc, txtCode.Value) Then
                        myMessages.post()
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                End If


            End If
        Catch ex As Exception
            btnsave.Enabled = True
            btndelete.Enabled = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtfrmsub__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtfrmsub._MYValidating
        If clsCommon.myLen(txtmain_Loc_Code.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location First.", Me.Text)
            txtmain_Loc_Code.Focus()
            txtmain_Loc_Code.Select()
            Errorcontrol.SetError(txtMain_Loc_Name, "Select Location First.")
            Exit Sub
        Else
            Errorcontrol.ResetError(txtMain_Loc_Name)
        End If

        Dim whrcls As String = ""
        If btnFrm_Sectn.IsChecked Then
            whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Section,'N')='Y')"
        ElseIf btnFrm_SubLoc.IsChecked Then
            whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Sub_Location,'N')='Y')"
        End If

        txtfrmsub.Value = clsCommon.myCstr(clsLocation.getFinder(whrcls, txtfrmsub.Value, isButtonClicked)) 'data comes whose type is sub-location

        If clsCommon.myLen(txtfrmsub.Value) > 0 Then
            'If clsCommon.myLen(txttosub.Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(txtfrmsub.Value), clsCommon.myCstr(txttosub.Value)) = CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow("From Sub-Location and To Sub-Location can-not be same", Me.Text)
            '    txtfrmsub.Value = ""
            '    txtfrmsub_name.Text = ""
            'End If
            txtfrmsub_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where location_code='" + txtfrmsub.Value + "'"))
        Else
            txtfrmsub.Value = ""
            txtfrmsub_name.Text = ""
        End If
    End Sub

    Private Sub txttosub__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txttosub._MYValidating
        If clsCommon.myLen(txtmain_Loc_Code.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select Location First.", Me.Text)
            txtmain_Loc_Code.Focus()
            txtmain_Loc_Code.Select()
            Errorcontrol.SetError(txtMain_Loc_Name, "Select Location First.")
            Exit Sub
        Else
            Errorcontrol.ResetError(txtMain_Loc_Name)
        End If

        Dim whrcls As String = ""
        If btnTo_Sectn.IsChecked Then
            whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Section,'N')='Y')"
        ElseIf btnTo_SubLoc.IsChecked Then
            whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Sub_Location,'N')='Y')"
        End If

        txttosub.Value = clsCommon.myCstr(clsLocation.getFinder(whrcls, txttosub.Value, isButtonClicked)) 'data comes whose type is sub-location or section'(isnull(Is_Sub_Location,'N')='Y' or isnull(Is_Section,'N')='Y')

        If clsCommon.myLen(txttosub.Value) > 0 Then
            'If clsCommon.myLen(txtfrmsub.Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(txtfrmsub.Value), clsCommon.myCstr(txttosub.Value)) = CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow("From Sub-Location and To Sub-Location can-not be same", Me.Text)
            '    txttosub.Value = ""
            '    txttosub_name.Text = ""
            'End If
            txttosub_name.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where location_code='" + txttosub.Value + "'"))
        Else
            txttosub.Value = ""
            txttosub_name.Text = ""
        End If
    End Sub

#Region "Batch Order"
    Private Sub txtbatchorder__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtbatchorder._MYValidating
        Try
            If clsCommon.myLen(txtmain_Loc_Code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtmain_Loc_Code.Select()
                txtmain_Loc_Code.Focus()
                Errorcontrol.SetError(txtMain_Loc_Name, "Select main location first.")
                Throw New Exception("Select main location first.")
            Else
                Errorcontrol.ResetError(txtMain_Loc_Name)
            End If

            If clsCommon.myLen(fndSection.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                fndSection.Select()
                fndSection.Focus()
                Errorcontrol.SetError(TxtSection, "Select section first.")
                Throw New Exception("Select section first.")
            Else
                Errorcontrol.ResetError(TxtSection)
            End If

            Dim whrcls As String = " isnull(TSPL_PP_BATCH_ORDER_HEAD.is_post,'1')='1' and isnull(TSPL_PP_BATCH_ORDER_HEAD.closed_yn,'0')='0' and TSPL_PP_BATCH_ORDER_HEAD.location_code in ('" + txtmain_Loc_Code.Value + "')"
            If settProductionOnlyOneIssueAgainstBatch Then
                whrcls += " and not exists (select 1 from TSPL_PP_ISSUE_HEAD where TSPL_PP_ISSUE_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_HEAD.Batch_Code and TSPL_PP_ISSUE_HEAD.Issue_Code not in ('" + txtCode.Value + "'))"
                If clsCommon.myLen(clsCommon.myCstr(fndSection.Value)) > 0 Then
                    whrcls += " and TSPL_PP_BATCH_ORDER_HEAD.SECTION_CODE= '" + fndSection.Value + "' "
                End If
            Else
                whrcls += " and TSPL_PP_BATCH_ORDER_HEAD.batch_code in (select axa.batch_code from (select a.Batch_Code,a.Item_Code,SUM(isnull(a.quantity,0)) as qty from ("
                whrcls += " select TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,(0-TSPL_PP_ISSUE_ITEM_DETAIL.Qty) as Quantity from TSPL_PP_ISSUE_HEAD left outer join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code=TSPL_PP_ISSUE_HEAD.Issue_Code where TSPL_PP_ISSUE_HEAD.Issue_Code<>'" + txtCode.Value + "' and TSPL_PP_ISSUE_HEAD.Main_Location_Code='" + txtmain_Loc_Code.Value + "' and TSPL_PP_ISSUE_HEAD.section_code='" + fndSection.Value + "'  "
                whrcls += " union all"
                whrcls += " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.batch_code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.batch_code left outer join tspl_pp_batch_order_bom_detail on tspl_pp_batch_order_bom_detail.batch_code=tspl_pp_batch_order_head.batch_code " + Environment.NewLine   ''''BHA/28/08/18-000485 by balwinder on 30/08/2018 
                whrcls += " where TSPL_PP_BATCH_ORDER_HEAD.location_code='" + txtmain_Loc_Code.Value + "' and TSPL_PP_BATCH_ORDER_bom_detail.section_code='" + fndSection.Value + "')a group by a.Batch_Code,a.Item_Code)axa where convert(decimal(18,3), axa.qty)>0)"
            End If

            txtbatchorder.Value = clsCommon.myCstr(clsProcessBatchOrder.GetFinder(whrcls, txtbatchorder.Value, isButtonClicked))
            txtbatch_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "'"))
            txtlocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "'"))
            txtlocationname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select location_desc from TSPL_LOCATION_MASTER where location_code='" + txtlocation.Text + "'"))
            ''richa agarwal BHA/02/07/18-000121 7 july,2018 
            TxtManualBatchNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ManualBatchNo from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "'"))
            chkJobWorkInward.Checked = clsProcessBatchOrder.IsJobWorkBatchOrder(txtbatchorder.Value, Nothing)
            ''richa agarwal againt ticket no BHA/02/07/18-000120
            If clsCommon.myLen(txtbatchorder.Value) > 0 Then
                If UseProductionPlaningDateForWholeProductionCycle = True Then
                    dtpDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Batch_Date from TSPL_PP_BATCH_ORDER_HEAD where Batch_Code='" + txtbatchorder.Value + "'"))
                End If
                Dim qry As String = " select TSPL_PP_BATCH_ORDER_HEAD.Line_No as [Line No],TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode as [Cost Center Code] , TSPL_CostCenter_MASTER.Cost_name as [Cost Center Name], TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode as [Profit Center Code]  ,TSPL_PROFIT_CENTER_MASTER.Name as [Profit Center Name]" & _
                " from TSPL_PP_BATCH_ORDER_HEAD left outer join TSPL_PROFIT_CENTER_MASTER on TSPL_PROFIT_CENTER_MASTER.Code =TSPL_PP_BATCH_ORDER_HEAD.ProfitCenterCode " & _
                " left outer join TSPL_CostCenter_MASTER on TSPL_CostCenter_MASTER.Cost_Code =TSPL_PP_BATCH_ORDER_HEAD.CostCenterCode " & _
                " where TSPL_PP_BATCH_ORDER_HEAD.batch_code='" & clsCommon.myCstr(txtbatchorder.Value) & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    lblLineNo.Text = clsCommon.myCstr(dt.Rows(0).Item("Line No"))
                    LblCostCenterCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Cost Center Code"))
                    lblCostCenterName.Text = clsCommon.myCstr(dt.Rows(0).Item("Cost Center Name"))
                    lblProfitCenterCode.Text = clsCommon.myCstr(dt.Rows(0).Item("Profit Center Code"))
                    lblProfitCenterName.Text = clsCommon.myCstr(dt.Rows(0).Item("Profit Center Name"))
                Else
                    lblLineNo.Text = ""
                    LblCostCenterCode.Text = ""
                    lblCostCenterName.Text = ""
                    lblProfitCenterCode.Text = ""
                    lblProfitCenterName.Text = ""
                End If
            End If
            ''----------------
            gv.Rows.Clear()
            If clsCommon.myLen(txtbatchorder.Value) > 0 Then
                If allowanybo = False Then
                    CheckChildBOStatus()
                End If
                isInsideLoadData = True
                AutoFillBatchDetail()
                isInsideLoadData = False
            Else
                txtbatch_desc.Text = ""
                txtlocation.Text = ""
                txtlocationname.Text = ""
                TxtManualBatchNo.Text = ""
                lblLineNo.Text = ""
                LblCostCenterCode.Text = ""
                lblCostCenterName.Text = ""
                lblProfitCenterCode.Text = ""
                lblProfitCenterName.Text = ""
                LoadBlankGrid()
                LoadQCBlankGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub CheckChildBOStatus()
        '-----------function check whether batch order has child batch order,if yes then it is used in issue process
        ',if not then first make issue entry of child batch
        Try
            '==============if allowchild setting is on then user can issue sub-child before child bo,but main bo cannot be issue before child and sub-child.
            If allowchildSubchildbo = True Then
                '====for this check selected bo is main or not========
                Dim Check_BO As Integer = CInt(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "' and isnull(main_batch_code,'')=''")))
                If Check_BO <= 0 Then '==if not main bo,then issue childs in any order
                    Exit Sub
                End If
            End If
            '=========================end=========================================================================

            Dim sub_BO As String = ""
            sub_BO = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sub_batch_code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "'"))

            sub_BO = sub_BO.Replace("'", "")
            sub_BO = sub_BO.Replace(",", "','")

            If clsCommon.myLen(sub_BO) > 0 Then
                qry = "select count(*) from "
                qry += "(select a.Batch_Code  from "
                qry += "(select TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,(TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity) as bo_qty,sum(isnull(TSPL_PP_ISSUE_ITEM_DETAIL.qty,0)) as qty from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Batch_Code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code left outer join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code=TSPL_PP_ISSUE_HEAD.Issue_Code where TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.batch_code in ('" + sub_BO + "') group by TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity)a where a.qty>0)b"
                check = clsDBFuncationality.getSingleValue(qry)

                If check <= 0 Then
                    txtbatchorder.Value = ""
                    txtbatch_desc.Text = ""
                    txtlocation.Text = ""
                    txtlocationname.Text = ""
                    txtfrmsub.Value = ""
                    txtfrmsub_name.Text = ""
                    txttosub.Value = ""
                    txttosub_name.Text = ""
                    LoadBlankGrid()
                    LoadQCBlankGrid()
                    Throw New Exception("First make issue entry for child batch order no. " + clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sub_batch_code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "'")) + "")
                Else
                    qry = "select count(*) from TSPL_PP_ISSUE_HEAD where Batch_Code in ('" + sub_BO + "')"
                    check = 0
                    check = clsDBFuncationality.getSingleValue(qry)

                    '==============check whether BO used even one time or not,if not then also send mgs========
                    If check <= 0 Then
                        txtbatchorder.Value = ""
                        txtbatch_desc.Text = ""
                        txtlocation.Text = ""
                        txtlocationname.Text = ""
                        txtfrmsub.Value = ""
                        txtfrmsub_name.Text = ""
                        txttosub.Value = ""
                        txttosub_name.Text = ""
                        LoadBlankGrid()
                        LoadQCBlankGrid()
                        Throw New Exception("First make issue entry for child batch order no. " + clsCommon.myCstr(clsDBFuncationality.getSingleValue("select sub_batch_code from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "'")) + "")
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function ItemType(ByVal itype As String) As String
        Dim values As String = Nothing
        If clsCommon.CompairString(itype, "R") = CompairStringResult.Equal Then
            values = "Raw Material"
        ElseIf clsCommon.CompairString(itype, "F") = CompairStringResult.Equal Then
            values = "Finished Good"
        ElseIf clsCommon.CompairString(itype, "S") = CompairStringResult.Equal Then
            values = "Semi Finished Good"
        ElseIf clsCommon.CompairString(itype, "A") = CompairStringResult.Equal Then
            values = "Asset"
        ElseIf clsCommon.CompairString(itype, "H") = CompairStringResult.Equal Then
            values = "Fresh"
        ElseIf clsCommon.CompairString(itype, "O") = CompairStringResult.Equal Then
            values = "Other"
        End If

        Return values
    End Function

    Private Function ProductType(ByVal Product_type As String) As String
        Return clsItemMaster.ProductType(Product_type)
    End Function

    Private Sub AutoFillBatchDetail()
        qry = "select ROW_NUMBER() over(order by TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.item_code) as Sno,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Product_Type,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Unit_Code,TSPL_UNIT_MASTER.unit_desc as UOM_Description,axa.qty from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Unit_Code "
        qry += " left outer join (select a.Batch_Code,a.Item_Code,SUM(isnull(a.quantity,0)) as qty from ("
        qry += " select TSPL_PP_ISSUE_HEAD.Batch_Code,TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code,(0-TSPL_PP_ISSUE_ITEM_DETAIL.Qty) as Quantity from TSPL_PP_ISSUE_HEAD left outer join TSPL_PP_ISSUE_ITEM_DETAIL on TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code=TSPL_PP_ISSUE_HEAD.Issue_Code where TSPL_PP_ISSUE_HEAD.Issue_Code<>'" + txtCode.Value + "' and TSPL_PP_ISSUE_HEAD.Main_Location_Code='" + txtmain_Loc_Code.Value + "' "
        qry += " union all"
        qry += " select TSPL_PP_BATCH_ORDER_HEAD.Batch_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Item_Code,TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Quantity from TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL left outer join TSPL_PP_BATCH_ORDER_HEAD on TSPL_PP_BATCH_ORDER_HEAD.batch_code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.batch_code where TSPL_PP_BATCH_ORDER_HEAD.Batch_Code='" + txtbatchorder.Value + "' and TSPL_PP_BATCH_ORDER_HEAD.location_code='" + txtmain_Loc_Code.Value + "')a group by a.Batch_Code,a.Item_Code)axa on axa.batch_code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.batch_code and axa.item_code=TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.item_code"
        qry += " where TSPL_PP_BATCH_ORDER_RAW_ITEM_DETAIL.Batch_Code='" + txtbatchorder.Value + "' and axa.qty>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv.Rows.Clear()
        gv.Rows.AddNew()
        gv_qc.Rows.Clear()
        gv.Columns(colTo_Loc_Type).ReadOnly = False
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv.Columns(colTo_Loc_Type).ReadOnly = True
            For Each dr As DataRow In dt.Rows
                gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = CInt(gv.Rows.Count)
                If clsCommon.CompairString(clsCommon.myCstr(dr("product_type")), "MI") = CompairStringResult.Equal Then
                    If Not settTankerDispatchAvgFATSNFPer Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value = "SUB"
                        gv.Rows(gv.Rows.Count - 1).Cells(colloc_code).Value = clsCommon.myCstr(txtfrmsub.Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colloc_name).Value = clsCommon.myCstr(txtfrmsub_name.Text)
                    End If
                Else
                    If Not settTankerDispatchAvgFATSNFPer Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value = "MAIN"
                        gv.Rows(gv.Rows.Count - 1).Cells(colloc_code).Value = clsCommon.myCstr(txtmain_Loc_Code.Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colloc_name).Value = clsCommon.myCstr(txtMain_Loc_Name.Text)
                    End If

                End If

                gv.Rows(gv.Rows.Count - 1).Cells(colTo_Loc_Type).Value = "SEC" 'in with bo to-loc is always section and auto fill section
                gv.Rows(gv.Rows.Count - 1).Cells(colToloc_code).Value = clsCommon.myCstr(txttosub.Value)
                gv.Rows(gv.Rows.Count - 1).Cells(colToloc_name).Value = clsCommon.myCstr(txttosub_name.Text)
                gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("item_code"))
                gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(dr("item_desc"))
                gv.Rows(gv.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(clsCommon.myCstr(dr("item_code")))
                gv.Rows(gv.Rows.Count - 1).Cells(colitemtype).Value = ItemType(clsCommon.myCstr(dr("item_type")))
                gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value = ProductType(clsCommon.myCstr(dr("product_type")))
                gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value = clsCommon.myCstr(dr("unit_code"))
                gv.Rows(gv.Rows.Count - 1).Cells(colUOMDesc).Value = clsCommon.myCstr(dr("uom_description"))
                gv.Rows(gv.Rows.Count - 1).Cells(colrequrdqty).Value = Math.Round(clsCommon.myCdbl(dr("qty")), DecimalPoint)

                '========================fill section detail=========================
                Dim sec_loc As String = ""
                sec_loc = clsProcessProductionIssueEntry.GetBOSectionLocationCode(txtbatchorder.Value, txtmain_Loc_Code.Value, clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value))
                If clsCommon.myLen(sec_loc) > 0 Then
                    gv.Rows(gv.Rows.Count - 1).Cells(colToloc_code).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 location_code from tspl_location_master where location_code in (" + sec_loc + ")"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colToloc_name).Value = clsCommon.myCstr(clsLocation.GetName(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colToloc_code).Value), Nothing))
                End If
                '============================================================================

                Dim loc_type As Integer = 0
                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                    loc_type = 2
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                    loc_type = 1
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                    loc_type = 0
                End If

                FillAvail_Stock(CInt(gv.Rows.Count - 1), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value), clsCommon.myCstr(txtmain_Loc_Code.Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colloc_code).Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value), loc_type)

                gv.Rows(gv.Rows.Count - 1).Cells(colqty).Value = Nothing
                'gv.Rows(gv.Rows.Count - 1).Cells(colrequrdqty).Value = Nothing
                gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).Value = clsBOM.GetFAT_PERS(clsCommon.myCstr(dr("item_code")))
                gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).Value = clsBOM.GetSNF_PERS(clsCommon.myCstr(dr("item_code")))
                gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).Value = Nothing
                gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).Value = Nothing
                gv.Rows(gv.Rows.Count - 1).Cells(colrem).Value = Nothing

                'BHA/19/07/18-000180 RICHA 
                ' FillQCGrid(clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colloc_code).Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colToloc_code).Value))
                FillQCGrid(clsCommon.myCstr(dr("item_code")), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colloc_code).Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colToloc_code).Value), clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value), loc_type)

                If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                    gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).ReadOnly = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).ReadOnly = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).ReadOnly = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).ReadOnly = False
                Else
                    'gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).Value = Nothing
                    'gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).Value = Nothing
                    gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).ReadOnly = True
                    gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).ReadOnly = True
                    gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).ReadOnly = True
                    gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).ReadOnly = True
                End If
                gv.Rows.AddNew()
            Next
        Else
            clsCommon.MyMessageBoxShow("No Data Found.", Me.Text)
        End If
        dt = Nothing
    End Sub

    Sub FillAvail_Stock(ByVal XR As Integer, ByVal Itemcode As String, ByVal Main_Loc_Code As String, ByVal Loc_Code As String, ByVal ProductType1 As String, ByVal UOM_CODE As String, ByVal is_sub_sec_main_location As Integer)
        gv.Rows(XR).Cells(colavailqty).Value = Nothing
        gv.Rows(XR).Cells(colavailfatkg).Value = Nothing
        gv.Rows(XR).Cells(colavailfatpers).Value = Nothing
        gv.Rows(XR).Cells(colavailsnfkg).Value = Nothing
        gv.Rows(XR).Cells(colavailsnfpers).Value = Nothing
        Dim dt As DataTable = XpertERPEngine.clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(Itemcode, Main_Loc_Code, Loc_Code, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, UOM_CODE, is_sub_sec_main_location)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv.Rows(XR).Cells(colavailqty).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("qty")), DecimalPoint)
            gv.Rows(XR).Cells(colavailfatkg).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("fat_kg")), DecimalPoint)
            gv.Rows(XR).Cells(colavailsnfkg).Value = Math.Round(clsCommon.myCdbl(dt.Rows(0)("snf_kg")), DecimalPoint)
            Dim fractnvalue As Decimal = 0
            Dim index As Integer = 0
            If clsCommon.myCdbl(dt.Rows(0)("qty")) = 0 Then
                gv.Rows(XR).Cells(colavailfatpers).Value = 0
                gv.Rows(XR).Cells(colavailsnfpers).Value = 0
            Else
                gv.Rows(XR).Cells(colavailfatpers).Value = clsBOM.GetFatSNFPercentage_AfterConversion(Itemcode, UOM_CODE, clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("fat_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                gv.Rows(XR).Cells(colavailsnfpers).Value = clsBOM.GetFatSNFPercentage_AfterConversion(Itemcode, UOM_CODE, clsCommon.myCdbl(dt.Rows(0)("qty")), clsCommon.myCdbl(dt.Rows(0)("snf_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
            End If
        End If
        dt = Nothing
    End Sub
#End Region

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow("No location rights.")
            Exit Sub
        End If

        qry = "select count(*) from TSPL_PP_ISSUE_HEAD where issue_code='" + txtCode.Value + "'"
        check = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsCommon.myCstr(clsProcessProductionIssueEntry.GetFinder(" TSPL_PP_ISSUE_HEAD.main_location_code in (" + arrLoc + ")", txtCode.Value, isButtonClicked))
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            FunReset()
        End If
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsProcessProductionIssueEntry()
        Try

            isNewEntry = True
            txtmain_Loc_Code.Enabled = True
            'txtbatchorder.Enabled = True
            chkBO.Enabled = True
            chkWOBO.Enabled = True
            fndSection.Enabled = True
            isInsideLoadData = True
            If clsCommon.myLen(arrLoc) <= 0 Then
                Throw New Exception("No location rights.")
            End If
            obj = clsProcessProductionIssueEntry.GetData(strCode, arrLoc, NavType, Nothing)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.issuecode) > 0 Then
                isNewEntry = False
                fndSection.Enabled = False
                chkBO.IsChecked = clsCommon.myCBool(IIf(obj.Against_BO = 1, True, False))
                chkWOBO.IsChecked = clsCommon.myCBool(IIf(obj.Against_BO = 0, True, False))
                chkBO.Enabled = False
                chkWOBO.Enabled = False
                txtCode.Value = obj.issuecode
                dtpDate.Value = obj.issue_date
                txtdesc.Text = obj.issuedesc
                cboStatus.Text = obj.status
                If obj.is_post = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnCancel.Enabled = True
                Else
                    btnCancel.Enabled = False
                End If
                txtbatchorder.Value = obj.batch_code
                txtbatch_desc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PP_BATCH_ORDER_HEAD where batch_code='" + txtbatchorder.Value + "'"))
                btnFrm_SubLoc.IsChecked = IIf(obj.Rbtn_Frm_Sub = 0, False, True)
                btnTo_SubLoc.IsChecked = IIf(obj.Rbtn_To_Sub = 0, False, True)
                btnFrm_Sectn.IsChecked = IIf(obj.Rbtn_Frm_Sub = 1, False, True)
                btnTo_Sectn.IsChecked = IIf(obj.Rbtn_To_Sub = 1, False, True)
                txtlocation.Text = obj.loc_code
                txtlocationname.Text = obj.loc_name
                txtfrmsub.Value = obj.frm_loc_code
                txtfrmsub_name.Text = obj.frm_loc_desc
                txttosub.Value = obj.to_loc_code
                txttosub_name.Text = obj.to_loc_name
                txtmain_Loc_Code.Value = obj.Main_Loc_Code
                fndSection.Value = obj.Section_Code
                TxtSection.Text = obj.Section_Name
                ''richa agarwal BHA/02/07/18-000121 7 july,2018 
                TxtManualBatchNo.Text = obj.ManualBatchNo
                ''richa agarwal againt ticket no BHA/02/07/18-000120
                lblLineNo.Text = obj.LINE_NO
                LblCostCenterCode.Text = obj.CostCenterCode
                lblCostCenterName.Text = obj.CostCenterName
                lblProfitCenterCode.Text = obj.ProfitCenterCode
                lblProfitCenterName.Text = obj.ProfitCenterName
                ''----------------
                txtMain_Loc_Name.Text = clsLocation.GetName(obj.Main_Loc_Code, Nothing)
                MyLabel6.Text = IIf(btnTo_Sectn.IsChecked = False, "To Sub-Location", "To Section")
                MyLabel4.Text = IIf(btnFrm_Sectn.IsChecked = False, "From Sub-Location", "From Section")
                chkJobWorkInward.Checked = obj.Is_Job_Work_Inward
                gv.Rows.Clear()
                gv.Rows.AddNew()
                gv_qc.Rows.Clear()
                gv_qc.Rows.AddNew()
                If obj.ArrItem IsNot Nothing AndAlso obj.ArrItem.Count > 0 Then
                    For Each objtr As clsProcessProductionIssueItemDetail In obj.ArrItem
                        gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = objtr.sno
                        If objtr.From_SubLocation_YN = 2 Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value = "MAIN"
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value = IIf(objtr.From_SubLocation_YN = 1, "SUB", "SEC")
                        End If
                        If objtr.To_SubLocation_YN = 2 Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colTo_Loc_Type).Value = "MAIN"
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colTo_Loc_Type).Value = IIf(objtr.To_SubLocation_YN = 1, "SUB", "SEC")
                        End If

                        gv.Rows(gv.Rows.Count - 1).Cells(colloc_code).Value = objtr.frm_loc_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colloc_name).Value = objtr.frm_loc_desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colToloc_code).Value = objtr.to_loc_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colToloc_name).Value = objtr.to_loc_desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = objtr.itemcode
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = objtr.itemname
                        gv.Rows(gv.Rows.Count - 1).Cells(colTolerance).Value = clsItemMaster.GetProductionTolerance(objtr.itemcode, Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colIsBatchItem).Value = clsItemMaster.IsBatchItem(objtr.itemcode)
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemtype).Value = ItemType(objtr.item_type)
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value = ProductType(objtr.product_type)
                        gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value = objtr.uom_code
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOMDesc).Value = objtr.uom_desc
                        gv.Rows(gv.Rows.Count - 1).Cells(colavailqty).Value = objtr.avail_qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colavailfatkg).Value = objtr.avail_fat_kg
                        gv.Rows(gv.Rows.Count - 1).Cells(colavailfatpers).Value = objtr.avail_fat_pers
                        gv.Rows(gv.Rows.Count - 1).Cells(colavailsnfkg).Value = objtr.avail_snf_kg
                        gv.Rows(gv.Rows.Count - 1).Cells(colavailsnfpers).Value = objtr.avail_snf_pers
                        gv.Rows(gv.Rows.Count - 1).Cells(colrequrdqty).Value = objtr.req_qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colqty).Value = objtr.issue_qty
                        gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).Value = objtr.fat_kg
                        gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).Value = objtr.fat_pers
                        gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).Value = objtr.snf_kg
                        gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).Value = objtr.snf_pers
                        gv.Rows(gv.Rows.Count - 1).Cells(colrem).Value = objtr.remarks

                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                            'gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).ReadOnly = False
                            'gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).ReadOnly = False
                            'gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).ReadOnly = False
                            'gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).ReadOnly = False
                            gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Tag = objtr.arrBatchItemnew
                        Else
                            gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Tag = objtr.arrBatchItem
                        End If

                        Dim loc_type As Integer = 0
                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                            loc_type = 2
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                            loc_type = 1
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                            loc_type = 0
                        End If

                        FillAvail_Stock(gv.Rows.Count - 1, objtr.itemcode, txtmain_Loc_Code.Value, objtr.frm_loc_code, ProductType(objtr.product_type), objtr.uom_code, loc_type)

                        gv.Rows.AddNew()
                    Next
                End If

                If obj.ArrQC IsNot Nothing AndAlso obj.ArrQC.Count > 0 Then
                    For Each objtr As clsProcessProductionIssueQCDetail In obj.ArrQC
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCsno).Value = objtr.sno
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_code).Value = objtr.frm_loc_code
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCloc_name).Value = objtr.frm_loc_desc
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_code).Value = objtr.to_loc_code
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCToloc_name).Value = objtr.to_loc_desc
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCitemcode).Value = objtr.itemcode
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCiname).Value = objtr.itemname
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparamcode).Value = objtr.param_code
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_desc).Value = objtr.param_desc
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value = objtr.param_type
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value = objtr.param_nature
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value = objtr.lrange
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange2).Value = objtr.urange
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value = objtr.status_grid
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value = objtr.value1
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue2).Value = objtr.value2
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCremarks).Value = objtr.remarks
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).Value = objtr.QCRange
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).Value = objtr.QCStatus
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).Value = objtr.QCValue

                        If clsCommon.myCdbl(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value) > 0 AndAlso (clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value) <= 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value), "None") = CompairStringResult.Equal) AndAlso clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value) <= 0 Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                        If clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value) > 0 AndAlso clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value) <= 0 AndAlso clsCommon.myCdbl(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value) <= 0 Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCValue).ReadOnly = True
                        End If
                        If clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCvalue1).Value) > 0 AndAlso (clsCommon.myLen(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value) <= 0 OrElse clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCstatus).Value), "None") = CompairStringResult.Equal) AndAlso clsCommon.myCdbl(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCrange1).Value) <= 0 Then
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).ReadOnly = True
                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCOutStatus).ReadOnly = True
                        End If
                        gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCHistort).Value = "Double Click"

                        gv_qc.Rows.AddNew()
                    Next
                End If

                If clsCommon.CompairString(obj.status, "Open") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                ElseIf clsCommon.CompairString(obj.status, "On Hold") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Pending
                ElseIf clsCommon.CompairString(obj.status, "Approved") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                ElseIf clsCommon.CompairString(obj.status, "In-Active") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Cancel
                End If

                txtmain_Loc_Code.Enabled = False
                'txtbatchorder.Enabled = False

                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                txtCode.MyReadOnly = True
                UcAttachment1.LoadData(txtCode.Value)

                If obj.is_post = "1" Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                End If
            Else
                FunReset()
            End If
            isSavedSuccess = True
        Catch ex As Exception
            isNewEntry = True
            isSavedSuccess = False
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
            obj = Nothing
        End Try

    End Sub

    Private Sub gv_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.CellFormatting
        If e.Row.Index >= 0 Then
            If e.Column Is gv.Columns(colitemcode) OrElse e.Column Is gv.Columns(coluom) Then
                If clsCommon.myCdbl(gv.CurrentRow.Cells(colrequrdqty).Value) > 0 Then
                    gv.CurrentRow.Cells(coluom).ReadOnly = True
                End If
            End If
        End If

    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If clsCommon.myLen(txtbatchorder.Value) <= 0 AndAlso chkBO.IsChecked Then
                        RadPageView1.SelectedPage = RadPageViewPage1
                        clsCommon.MyMessageBoxShow("Select Batch Order Detail", Me.Text)
                        txtbatchorder.Select()
                        txtbatchorder.Focus()
                        Errorcontrol.SetError(txtbatch_desc, "Select Batch Order Detail")
                        Return
                    Else
                        Errorcontrol.ResetError(txtbatch_desc)
                    End If

                    If e.Column Is gv.Columns(colFrm_Loc_Type) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(colloc_code).Value = Nothing
                        gv.CurrentRow.Cells(colloc_name).Value = Nothing
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colTo_Loc_Type) Then
                        isCellValueChanged = True
                        gv.CurrentRow.Cells(colToloc_code).Value = Nothing
                        gv.CurrentRow.Cells(colToloc_name).Value = Nothing
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colloc_code) Then
                        isCellValueChanged = True
                        OpenLocation(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colToloc_code) Then
                        isCellValueChanged = True
                        ToOpenLocation(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(colitemcode) Then
                        isCellValueChanged = True
                        OpenIcode(False)
                        Cal_FATSNF()
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv.Columns(coluom) Then
                        isCellValueChanged = True
                        OpenUOM(False)
                        Cal_FATSNF()
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gv.Columns(colqty)) Or (e.Column Is gv.Columns(colfatpers)) Or (e.Column Is gv.Columns(colfatkg)) Then
                        isCellValueChanged = True
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) > clsCommon.myCdbl(gv.CurrentRow.Cells(colavailqty).Value) Then
                            If Not settAllowNegativeStockInDairyProduction Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gv.CurrentRow.Cells(colqty).Value = Nothing
                                Throw New Exception("Filled quantity cannot be more than available quantity.")
                            End If

                        End If
                        Cal_FATSNF()
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gv.Columns(colqty)) Or (e.Column Is gv.Columns(colsnfpers)) Or (e.Column Is gv.Columns(colsnfkg)) Then
                        isCellValueChanged = True
                        If clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value) > clsCommon.myCdbl(gv.CurrentRow.Cells(colavailqty).Value) Then
                            If Not settAllowNegativeStockInDairyProduction Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gv.CurrentRow.Cells(colqty).Value = Nothing
                                Throw New Exception("Filled quantity cannot be more than available quantity.")
                            End If
                        End If
                        Cal_FATSNF()
                        CalculateRequiredQty()
                        isCellValueChanged = False
                    End If
                    If (e.Column Is gv.Columns(colqty)) Then
                        OpenBatchItem()
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub CalculateRequiredQty()
        Dim arrICode As New List(Of String)
        For ii As Integer = 0 To gv.Rows.Count - 1
            If Not arrICode.Contains(clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value)) Then
                arrICode.Add(clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value))
                Dim RequiredQty As Decimal = clsCommon.myCdbl(gv.Rows(ii).Cells(colrequrdqty).Value) - clsCommon.myCdbl(gv.Rows(ii).Cells(colqty).Value)
                If RequiredQty > 0 Then
                    For jj As Integer = ii + 1 To gv.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value), clsCommon.myCstr(gv.Rows(jj).Cells(colitemcode).Value)) = CompairStringResult.Equal Then
                            gv.Rows(jj).Cells(colrequrdqty).Value = RequiredQty
                            RequiredQty -= clsCommon.myCdbl(gv.Rows(jj).Cells(colqty).Value)
                            If RequiredQty < 0 Then
                                Exit For
                            End If
                        End If
                    Next
                End If
            End If
        Next
        arrICode = Nothing
    End Sub

    Sub OpenBatchItem()
        If clsCommon.myCBool(gv.CurrentRow.Cells(colIsBatchItem).Value) Then
            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                Dim frm As frmBatchItemOutNew = New frmBatchItemOutNew()
                frm.strItemCode = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
                frm.strItemName = clsCommon.myCstr(gv.CurrentRow.Cells(colitemname).Value)
                frm.strLocationCode = clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value)
                frm.strCurrDocNo = txtCode.Value
                frm.strCurrDocType = MyBase.Form_ID
                frm.strUOM = clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value)
                frm.dblqty = clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value)
                frm.arr = TryCast(gv.CurrentRow.Cells(colitemcode).Tag, List(Of clsBatchInventoryNew))
                If RunBatchFifowise Then
                    frm.OpenSerialList(0, "")
                    gv.CurrentRow.Cells(colitemcode).Tag = frm.arr
                Else
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv.CurrentRow.Cells(colitemcode).Tag = frm.arr
                    End If
                End If
            Else
                Dim frm As frmBatchItemOut = New frmBatchItemOut()
                frm.strItemCode = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
                frm.strItemName = clsCommon.myCstr(gv.CurrentRow.Cells(colitemname).Value)
                frm.strLocationCode = clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value)
                frm.strCurrDocNo = txtCode.Value
                frm.strCurrDocType = MyBase.Form_ID
                frm.strUOM = clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value)
                frm.dblMRP = 0
                frm.dblqty = clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value)
                frm.arr = TryCast(gv.CurrentRow.Cells(colitemcode).Tag, List(Of clsBatchInventory))
                If RunBatchFifowise Then
                    frm.OpenSerialList(0, "")
                    gv.CurrentRow.Cells(colitemcode).Tag = frm.arr
                Else
                    frm.ShowDialog()
                    If Not frm.isCencelButtonClicked Then
                        gv.CurrentRow.Cells(colitemcode).Tag = frm.arr
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv.KeyDown
        If e.KeyCode = Keys.F5 Then
            If RunBatchFifowise Then
                OpenBatchItemIfFIFIOSettingON()
            Else
                OpenBatchItem()
            End If
        ElseIf e.Control And e.KeyCode = Keys.Insert Then
            If clsCommon.myLen(gv.CurrentRow.Cells(colitemcode).Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Insert the selected Item " + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        isCellValueChanged = True
                        gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colFrm_Loc_Type).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colTo_Loc_Type).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colTo_Loc_Type).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colToloc_code).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colToloc_name).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_name).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemname).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colitemname).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemtype).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colitemtype).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colitemproducttype).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(coluom).Value = clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colUOMDesc).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colUOMDesc).Value)
                        gv.Rows(gv.Rows.Count - 1).Cells(colrequrdqty).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colrequrdqty).Value), DecimalPoint)
                        gv.Rows(gv.Rows.Count - 1).Cells(colqty).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value), DecimalPoint)
                        gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colfatpers).Value), 2)
                        gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colsnfpers).Value), 2)
                        gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colfatkg).Value), DecimalPoint)
                        gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).Value = Math.Round(clsCommon.myCdbl(gv.CurrentRow.Cells(colsnfkg).Value), DecimalPoint)
                        gv.Rows(gv.Rows.Count - 1).Cells(colrem).Value = clsCommon.myCstr(gv.CurrentRow.Cells(colrem).Value)
                        If Not clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).Value = Nothing
                            gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).Value = Nothing
                            gv.Rows(gv.Rows.Count - 1).Cells(colfatkg).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colfatpers).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colsnfpers).ReadOnly = True
                        End If
                        gv.Rows.AddNew()
                        CalculateRequiredQty()
                        isCellValueChanged = False
                    Catch ex As Exception
                        isCellValueChanged = False
                    End Try

                End If
            End If
        End If
    End Sub

    Public Sub OpenBatchItemIfFIFIOSettingON()
        If clsCommon.myCBool(gv.CurrentRow.Cells(colIsBatchItem).Value) Then
            Dim strBatchunion As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                Dim arr As List(Of clsBatchInventoryNew) = TryCast(gv.CurrentRow.Cells(colitemcode).Tag, List(Of clsBatchInventoryNew))
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each obj As clsBatchInventoryNew In arr
                        strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                    Next
                End If
            Else
                Dim arr As List(Of clsBatchInventory) = TryCast(gv.CurrentRow.Cells(colitemcode).Tag, List(Of clsBatchInventory))
                If arr IsNot Nothing AndAlso arr.Count > 0 Then
                    For Each obj As clsBatchInventory In arr
                        strBatchunion += " Batch No - " & clsCommon.myCstr(obj.Batch_No) & "         Qty - " & clsCommon.myCstr(obj.Qty) + Environment.NewLine
                    Next
                End If
            End If
            If clsCommon.myLen(strBatchunion) > 0 Then
                clsCommon.MyMessageBoxShow(strBatchunion, Me.Text)
            End If
        End If
    End Sub

    Private Sub ToOpenLocation(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(gv.CurrentRow.Cells(colTo_Loc_Type).Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            Throw New Exception("Select first To location type[Sub-Location or Section].")
        End If

        Dim oldIcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
        Dim oldFrmLoc As String = clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value)
        Dim OldToLoc As String = clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value)

        If clsCommon.myLen(oldIcode) > 0 Then
            RemoveCurrentItemQCRow(oldIcode, oldFrmLoc, OldToLoc)
        End If

        Dim whrcls As String = ""

        Dim sec_loc As String = ""
        sec_loc = clsProcessProductionIssueEntry.GetBOSectionLocationCode(txtbatchorder.Value, txtmain_Loc_Code.Value, clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value))


        If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colTo_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then 'btnTo_Sectn.IsChecked
            If clsCommon.myLen(sec_loc) > 0 Then
                whrcls = " location_code in (" + sec_loc + ")"
            Else
                'whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Section,'N')='Y')"
                clsCommon.MyMessageBoxShow("No consumption section found.")
                gv.CurrentRow.Cells(colToloc_code).Value = Nothing
                gv.CurrentRow.Cells(colToloc_name).Value = Nothing
                Exit Sub
            End If

            'If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), "Milk") <> CompairStringResult.Equal Then
            '    whrcls = " location_code in (Select location_code from tspl_location_master where (main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Section,'N')='Y') or (isnull(csa_type,'N')<>'Y' and ISNULL(Is_Sub_Location,'N')<>'Y'))"
            'End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colTo_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then 'btnTo_SubLoc.IsChecked
            whrcls = " location_code in (Select location_code from tspl_location_master where main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Sub_Location,'N')='Y')"
            'If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), "Milk") <> CompairStringResult.Equal Then
            '    whrcls = " location_code in (Select location_code from tspl_location_master where (main_location_code='" + txtmain_Loc_Code.Value + "' and isnull(Is_Sub_Location,'N')='Y') or (isnull(csa_type,'N')<>'Y' and ISNULL(Is_Section,'N')<>'Y'))"
            'End If
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colTo_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
            whrcls = " tspl_location_master.location_code = '" + txtmain_Loc_Code.Value + "' " '  and tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y'
        End If

        gv.CurrentRow.Cells(colToloc_code).Value = clsCommon.myCstr(clsLocation.getFinder(whrcls, clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value), isButtonClicked)) '" isnull(csa_type,'N')='N' and location_code in (" + arrLoc + ")"
        gv.CurrentRow.Cells(colToloc_name).Value = clsLocation.GetName(clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value), Nothing)

        'BHA/19/07/18-000180 RICHA 
        'FillQCGrid(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value))
        Dim loc_type As Integer = 0
        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
            loc_type = 2
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
            loc_type = 1
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(gv.Rows.Count - 1).Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
            loc_type = 0
        End If
        FillQCGrid(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), loc_type)
    End Sub

    Private Sub OpenLocation(ByVal isButtonClicked As Boolean)
        Dim intRow As Integer = gv.CurrentRow.Index
        If clsCommon.myLen(gv.Rows(intRow).Cells(colFrm_Loc_Type).Value) <= 0 Then
            RadPageView1.SelectedPage = RadPageViewPage1
            Throw New Exception("Select first location type[Sub-Location or Section].")
        End If
        Dim oldIcode As String = clsCommon.myCstr(gv.Rows(intRow).Cells(colitemcode).Value)
        Dim oldFrmLoc As String = clsCommon.myCstr(gv.Rows(intRow).Cells(colloc_code).Value)
        Dim OldToLoc As String = clsCommon.myCstr(gv.Rows(intRow).Cells(colToloc_code).Value)
        If clsCommon.myLen(oldIcode) > 0 Then
            RemoveCurrentItemQCRow(oldIcode, oldFrmLoc, OldToLoc)
        End If

        Dim loc_type As Integer = 0
        If clsCommon.CompairString(clsCommon.myCstr(gv.Rows(intRow).Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
            loc_type = 2
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(intRow).Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
            loc_type = 1
        ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.Rows(intRow).Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
            loc_type = 0
        End If
        If SettDisableToPickMainLocation AndAlso loc_type = 2 Then
            Throw New Exception("You are not authorize to Pick item from Main Location" + Environment.NewLine + "Please change Location Type - Main location at Row no" & clsCommon.myCstr(intRow + 1) & "  ")
        End If
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow("No location rights.")
            Exit Sub
        End If

        Dim frm As New FrmPPIssueChildScrren()
        frm.LoadData(dtpDate.Value, clsCommon.myCstr(gv.Rows(intRow).Cells(colitemcode).Value), clsCommon.myCstr(gv.Rows(intRow).Cells(coluom).Value), txtmain_Loc_Code.Value, clsCommon.myCstr(gv.Rows(intRow).Cells(colitemproducttype).Value), loc_type, arrLoc, dtpDate.Value)
        frm.ShowDialog()

        If frm.Arr_Loc IsNot Nothing AndAlso frm.Arr_Loc.Count > 0 Then
            Dim ii As Integer = intRow

            For Each Loc_Code As String In frm.Arr_Loc
                If intRow <> ii Then
                    gv.Rows(ii).Cells(colFrm_Loc_Type).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colFrm_Loc_Type).Value)
                    gv.Rows(ii).Cells(colTo_Loc_Type).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colTo_Loc_Type).Value)
                    gv.Rows(ii).Cells(colToloc_code).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colToloc_code).Value)
                    gv.Rows(ii).Cells(colToloc_name).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colToloc_name).Value)
                    gv.Rows(ii).Cells(colitemcode).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colitemcode).Value)
                    gv.Rows(ii).Cells(colitemname).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colitemname).Value)
                    gv.Rows(ii).Cells(colitemtype).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colitemtype).Value)
                    gv.Rows(ii).Cells(colitemproducttype).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colitemproducttype).Value)
                    gv.Rows(ii).Cells(coluom).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(coluom).Value)
                    gv.Rows(ii).Cells(colUOMDesc).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colUOMDesc).Value)
                    gv.Rows(ii).Cells(colrequrdqty).Value = Math.Round(clsCommon.myCdbl(gv.Rows(intRow).Cells(colrequrdqty).Value), DecimalPoint)
                    gv.Rows(ii).Cells(colqty).Value = Math.Round(clsCommon.myCdbl(gv.Rows(intRow).Cells(colqty).Value), DecimalPoint)
                    gv.Rows(ii).Cells(colfatpers).Value = Math.Round(clsCommon.myCdbl(gv.Rows(intRow).Cells(colfatpers).Value), 2)
                    gv.Rows(ii).Cells(colsnfpers).Value = Math.Round(clsCommon.myCdbl(gv.Rows(intRow).Cells(colsnfpers).Value), 2)
                    gv.Rows(ii).Cells(colfatkg).Value = Math.Round(clsCommon.myCdbl(gv.Rows(intRow).Cells(colfatkg).Value), DecimalPoint)
                    gv.Rows(ii).Cells(colsnfkg).Value = Math.Round(clsCommon.myCdbl(gv.Rows(intRow).Cells(colsnfkg).Value), DecimalPoint)
                    gv.Rows(ii).Cells(colrem).Value = clsCommon.myCstr(gv.Rows(intRow).Cells(colrem).Value)

                    If Not clsCommon.CompairString(clsCommon.myCstr(gv.Rows(ii).Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                        gv.Rows(ii).Cells(colfatpers).Value = Nothing
                        gv.Rows(ii).Cells(colsnfpers).Value = Nothing
                        gv.Rows(ii).Cells(colfatkg).ReadOnly = True
                        gv.Rows(ii).Cells(colfatpers).ReadOnly = True
                        gv.Rows(ii).Cells(colsnfkg).ReadOnly = True
                        gv.Rows(ii).Cells(colsnfpers).ReadOnly = True
                    End If
                End If
                gv.Rows(ii).Cells(colloc_code).Value = Loc_Code
                gv.Rows(ii).Cells(colloc_name).Value = clsLocation.GetName(Loc_Code, Nothing)
                FillAvail_Stock(ii, clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value), clsCommon.myCstr(txtmain_Loc_Code.Value), clsCommon.myCstr(gv.Rows(ii).Cells(colloc_code).Value), clsCommon.myCstr(gv.Rows(ii).Cells(colitemproducttype).Value), clsCommon.myCstr(gv.Rows(ii).Cells(coluom).Value), loc_type)
                FillQCGrid(clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value), clsCommon.myCstr(gv.Rows(ii).Cells(colloc_code).Value), clsCommon.myCstr(gv.Rows(ii).Cells(colToloc_code).Value), clsCommon.myCstr(gv.Rows(ii).Cells(coluom).Value), loc_type)
                If intRow <> ii Then
                    gv.Rows.Move(ii, intRow + 1)
                End If
                gv.Rows.AddNew()
                ii = gv.Rows.Count - 1
            Next
        Else
            gv.Rows(intRow).Cells(colloc_code).Value = ""
            gv.Rows(intRow).Cells(colloc_name).Value = ""
            gv.Rows(intRow).Cells(colavailqty).Value = Nothing
            gv.Rows(intRow).Cells(colavailfatkg).Value = Nothing
            gv.Rows(intRow).Cells(colavailfatpers).Value = Nothing
            gv.Rows(intRow).Cells(colavailsnfkg).Value = Nothing
            gv.Rows(intRow).Cells(colavailsnfpers).Value = Nothing
            RefreshQCSNo()
        End If
        'BHA/02/08/18-000215 by balwinder on 13/08/2018
        RefreshSerialNo()
        For ii As Integer = 0 To gv_qc.Rows.Count - 1
            gv_qc.CurrentRow = gv_qc.Rows(ii)
            CorrectFATSNFFromQC()
        Next


    End Sub

    Private Sub RefreshSerialNo()

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.myLen(grow.Cells(colitemcode).Value) > 0 Then
                grow.Cells(colSno).Value = grow.Index + 1
                gv.CurrentRow = grow
            End If
        Next

        '============remove extra rows===========
        For ii As Integer = gv.Rows.Count - 1 To 0 Step -1
            If clsCommon.myLen(clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value)) <= 0 Then
                gv.Rows.RemoveAt(ii)
            End If
        Next
        gv.Rows.AddNew()
    End Sub

    Private Sub OpenIcode(ByVal isButtonClicked As Boolean)
        If chkBO.IsChecked Then
            Exit Sub
        End If
        Dim oldIcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value)
        Dim oldFrmLoc As String = clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value)
        Dim OldToLoc As String = clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value)
        If clsCommon.myLen(oldIcode) > 0 Then
            RemoveCurrentItemQCRow(oldIcode, oldFrmLoc, OldToLoc)
        End If
        gv.CurrentRow.Cells(colitemcode).Value = clsItemMaster.getFinder("", clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), isButtonClicked)
        qry = "select item_desc,item_type,product_type,unit_code,Is_Batch_Item,Production_Tolerance from tspl_item_master where item_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv.CurrentRow.Cells(colitemname).Value = clsCommon.myCstr(dt.Rows(0)("item_desc"))
            gv.CurrentRow.Cells(colIsBatchItem).Value = (clsCommon.myCdbl(dt.Rows(0)("Is_Batch_Item")) = 1)
            gv.CurrentRow.Cells(colitemtype).Value = ItemType(clsCommon.myCstr(dt.Rows(0)("item_type")))
            gv.CurrentRow.Cells(colitemproducttype).Value = ProductType(clsCommon.myCstr(dt.Rows(0)("product_type")))
            gv.CurrentRow.Cells(coluom).Value = clsCommon.myCstr(dt.Rows(0)("unit_code"))
            gv.CurrentRow.Cells(colUOMDesc).Value = clsUnitMaster.GetName(clsCommon.myCstr(dt.Rows(0)("unit_code")))
            gv.CurrentRow.Cells(colTolerance).Value = clsCommon.myCdbl(dt.Rows(0)("Production_Tolerance"))
            gv.CurrentRow.Cells(colqty).Value = Nothing
            gv.CurrentRow.Cells(colrequrdqty).Value = Nothing
            gv.CurrentRow.Cells(colfatkg).Value = Nothing
            gv.CurrentRow.Cells(colfatpers).Value = clsBOM.GetFAT_PERS(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value))
            gv.CurrentRow.Cells(colsnfkg).Value = Nothing
            gv.CurrentRow.Cells(colsnfpers).Value = clsBOM.GetSNF_PERS(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value))
            gv.CurrentRow.Cells(colrem).Value = Nothing
            If Not clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), "Milk") = CompairStringResult.Equal Then
                gv.CurrentRow.Cells(colfatkg).Value = Nothing
                gv.CurrentRow.Cells(colfatpers).Value = Nothing
                gv.CurrentRow.Cells(colsnfkg).Value = Nothing
                gv.CurrentRow.Cells(colsnfpers).Value = Nothing
                gv.CurrentRow.Cells(colfatkg).ReadOnly = True
                gv.CurrentRow.Cells(colfatpers).ReadOnly = True
                gv.CurrentRow.Cells(colsnfkg).ReadOnly = True
                gv.CurrentRow.Cells(colsnfpers).ReadOnly = True
            End If

            Dim loc_type As Integer = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                loc_type = 2
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                loc_type = 1
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                loc_type = 0
            End If

            FillAvail_Stock(CInt(gv.CurrentCell.RowIndex), clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(txtmain_Loc_Code.Value), clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), loc_type)
            FillQCGrid(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), loc_type)
        Else
            gv.CurrentRow.Cells(colTolerance).Value = 0
            gv.CurrentRow.Cells(colitemcode).Value = Nothing
            gv.CurrentRow.Cells(colitemname).Value = Nothing
            gv.CurrentRow.Cells(colitemtype).Value = Nothing
            gv.CurrentRow.Cells(colitemproducttype).Value = Nothing
            gv.CurrentRow.Cells(coluom).Value = Nothing
            gv.CurrentRow.Cells(colUOMDesc).Value = Nothing
            gv.CurrentRow.Cells(colavailqty).Value = Nothing
            gv.CurrentRow.Cells(colavailfatkg).Value = Nothing
            gv.CurrentRow.Cells(colavailfatpers).Value = Nothing
            gv.CurrentRow.Cells(colavailsnfkg).Value = Nothing
            gv.CurrentRow.Cells(colavailsnfpers).Value = Nothing
            gv.CurrentRow.Cells(colqty).Value = Nothing
            gv.CurrentRow.Cells(colrequrdqty).Value = Nothing
            gv.CurrentRow.Cells(colfatkg).Value = Nothing
            gv.CurrentRow.Cells(colfatpers).Value = Nothing
            gv.CurrentRow.Cells(colsnfkg).Value = Nothing
            gv.CurrentRow.Cells(colsnfpers).Value = Nothing
            gv.CurrentRow.Cells(colrem).Value = Nothing
            gv.CurrentRow.Cells(colfatkg).ReadOnly = True
            gv.CurrentRow.Cells(colfatpers).ReadOnly = True
            gv.CurrentRow.Cells(colsnfkg).ReadOnly = True
            gv.CurrentRow.Cells(colsnfpers).ReadOnly = True
            gv.CurrentRow.Cells(colIsBatchItem).Value = False
        End If

    End Sub

    Private Sub RemoveCurrentItemQCRow(ByVal Item_Code As String, ByVal Frm_Loc As String, ByVal To_Loc As String)
        For ii As Integer = gv_qc.Rows.Count - 1 To 0 Step -1
            If clsCommon.myLen(gv_qc.Rows(ii).Cells(colQCitemcode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value), Item_Code) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCloc_code).Value), Frm_Loc) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCToloc_code).Value), To_Loc) = CompairStringResult.Equal Then
                gv_qc.Rows.RemoveAt(ii)
            End If
        Next
    End Sub

    Private Sub OpenUOM(ByVal isButtonClicked As Boolean)
        If clsCommon.myLen(gv.CurrentRow.Cells(colitemcode).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Select item code first", Me.Text)
            Exit Sub
        End If

        qry = "select TSPL_ITEM_UOM_DETAIL.UOM_Code as Code,TSPL_UNIT_MASTER.Unit_Desc as Description,TSPL_ITEM_UOM_DETAIL.Weight,TSPL_ITEM_UOM_DETAIL.Stocking_Unit as [Stocking Unit],TSPL_ITEM_UOM_DETAIL.Conversion_Factor as [Conversion Factor] from TSPL_ITEM_UOM_DETAIL left outer join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.Unit_Code=TSPL_ITEM_UOM_DETAIL.UOM_Code "
        gv.CurrentRow.Cells(coluom).Value = clsCommon.ShowSelectForm("UOMFND", qry, "Code", " TSPL_ITEM_UOM_DETAIL.Item_Code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value) + "'", clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), "Code", isButtonClicked)

        If clsCommon.myLen(gv.CurrentRow.Cells(coluom).Value) > 0 Then
            gv.CurrentRow.Cells(colUOMDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_desc from tspl_unit_master where unit_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value) + "'"))

            Dim loc_type As Integer = 0
            If clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFrm_Loc_Type).Value), "MAIN") = CompairStringResult.Equal Then
                loc_type = 2
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFrm_Loc_Type).Value), "SUB") = CompairStringResult.Equal Then
                loc_type = 1
            ElseIf clsCommon.CompairString(clsCommon.myCstr(gv.CurrentRow.Cells(colFrm_Loc_Type).Value), "SEC") = CompairStringResult.Equal Then
                loc_type = 0
            End If
            FillAvail_Stock(CInt(gv.CurrentCell.RowIndex), clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(txtmain_Loc_Code.Value), clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colitemproducttype).Value), clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), loc_type)
        Else
            gv.CurrentRow.Cells(coluom).Value = ""
            gv.CurrentRow.Cells(colUOMDesc).Value = ""
        End If
    End Sub

    Private Sub gv_qc_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_qc.CellDoubleClick
        Try
            If e.Column Is gv_qc.Columns(colQCHistort) Then
                Dim frm As New FrmPPQCCheckHistory()
                frm.Item_Code = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCitemcode).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
            End If
            If e.Column Is gv_qc.Columns(colQCValue) Then
                Dim arrValue As New List(Of String)
                Dim xvalue As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value)
                If clsCommon.myLen(xvalue) > 0 Then
                    Dim split() As String
                    split = xvalue.Split(",")

                    Dim ii As Integer = split.Length

                    While (ii > 0)
                        If clsCommon.myLen(split(ii)) > 0 Then
                            arrValue.Add(split(ii))
                        End If
                        ii -= 1
                    End While
                End If

                Dim frm As New FrmCheckBoxGrid()
                frm.qry = "select value from tspl_Parameter_value_master where parameter_code='" + clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparamcode).Value) + "'"
                frm.arrValue = arrValue
                frm.ShowDialog()

                arrValue = frm.arrValue
                gv_qc.CurrentRow.Cells(colQCValue).Value = ""
                If arrValue IsNot Nothing AndAlso arrValue.Count > 0 Then
                    For Each arr As String In arrValue
                        gv_qc.CurrentRow.Cells(colQCValue).Value = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value) + "," + clsCommon.myCstr(arr)

                        If clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value).Substring(0, 1) = "," Then
                            gv_qc.CurrentRow.Cells(colQCValue).Value = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value).Substring(1, clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCValue).Value).Length - 1)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_qc_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_qc.CellValueChanged
        If Not isInsideLoadData Then
            If Not isCellValueChanged Then
                'If (e.Column Is gv_qc.Columns(colQCparamcode)) Or (e.Column Is gv_qc.Columns(colQCparam_desc)) Or (e.Column Is gv_qc.Columns(colQCparam_type)) Or (e.Column Is gv_qc.Columns(colQCparam_nature)) Then
                isCellValueChanged = True
                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Range") = CompairStringResult.Equal Then
                    gv_qc.CurrentRow.Cells(colQCvalue1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCvalue2).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCstatus).Value = "None"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Alphanumeric") = CompairStringResult.Equal Then
                    gv_qc.CurrentRow.Cells(colQCrange1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCrange2).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCstatus).Value = "None"
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_nature).Value), "Boolean") = CompairStringResult.Equal Then
                    gv_qc.CurrentRow.Cells(colQCvalue1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCvalue2).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCrange1).Value = Nothing
                    gv_qc.CurrentRow.Cells(colQCrange2).Value = Nothing
                End If
                isCellValueChanged = False

                If e.Column Is gv_qc.Columns(colQCRange) Then
                    isCellValueChanged = True
                    CorrectFATSNFFromQC()
                    isCellValueChanged = False
                End If
                'End If
            End If
        End If
    End Sub

    Sub CorrectFATSNFFromQC()
        Dim Icode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCitemcode).Value)
        Dim frmloccode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCloc_code).Value)
        Dim toloccode As String = clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCToloc_code).Value)
        For ii As Integer = 0 To gv.Rows.Count - 1
            Dim gvIcode As String = clsCommon.myCstr(gv.Rows(ii).Cells(colitemcode).Value)
            Dim gvfrmloccode As String = clsCommon.myCstr(gv.Rows(ii).Cells(colloc_code).Value)
            Dim gvtoloccode As String = clsCommon.myCstr(gv.Rows(ii).Cells(colToloc_code).Value)
            gv.CurrentRow = gv.Rows(ii)
            If clsCommon.CompairString(gvIcode, Icode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvfrmloccode, frmloccode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gvtoloccode, toloccode) = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                    gv.Rows(ii).Cells(colfatpers).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), IIf(settTankerDispatchAvgFATSNFPer, 10, 2))
                    Cal_FATSNF()
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gv_qc.CurrentRow.Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                    gv.Rows(ii).Cells(colsnfpers).Value = Math.Round(clsCommon.myCdbl(gv_qc.CurrentRow.Cells(colQCRange).Value), IIf(settTankerDispatchAvgFATSNFPer, 10, 2))
                    Cal_FATSNF()
                End If
            End If
        Next
    End Sub

    Private Sub Cal_FATSNF()
        Try
            Dim qty As Decimal = clsCommon.myCdbl(gv.CurrentRow.Cells(colqty).Value)
            Dim fat As Decimal = clsCommon.myCdbl(gv.CurrentRow.Cells(colfatpers).Value)
            Dim Snf As Decimal = clsCommon.myCdbl(gv.CurrentRow.Cells(colsnfpers).Value)
            If settPickProductCostFromItemUOMDetail Then
                Dim obj As clsItemMasterQCParameter = clsItemMasterQCParameter.GetStandardFATSNFRate(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), Nothing)
                If fat > (1.5 * obj.FATPer) Then
                    fat = obj.FATPer
                    gv.CurrentRow.Cells(colfatpers).Value = obj.FATPer
                    changeQCFATSNFPer(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value), True, obj.FATPer)
                End If
                If Snf > (1.5 * obj.SNFPer) Then
                    Snf = obj.SNFPer
                    gv.CurrentRow.Cells(colsnfpers).Value = obj.SNFPer
                    changeQCFATSNFPer(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colloc_code).Value), clsCommon.myCstr(gv.CurrentRow.Cells(colToloc_code).Value), False, obj.SNFPer)
                End If
            End If
            gv.CurrentRow.Cells(colfatkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), qty, fat, Nothing)
            gv.CurrentRow.Cells(colsnfkg).Value = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gv.CurrentRow.Cells(colitemcode).Value), clsCommon.myCstr(gv.CurrentRow.Cells(coluom).Value), qty, Snf, Nothing)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub changeQCFATSNFPer(ByVal strICode As String, ByVal strFromLocation As String, ByVal strToLocation As String, ByVal TureForFATFalseForSNF As Boolean, ByVal FATSNFValue As Decimal)
        isCellValueChanged = True
        For ii As Integer = 0 To gv_qc.Rows.Count - 1
            If clsCommon.CompairString(strICode, clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value)) = CompairStringResult.Equal Then
                If clsCommon.CompairString(strFromLocation, clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCloc_code).Value)) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(strToLocation, clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCToloc_code).Value)) = CompairStringResult.Equal Then
                        If TureForFATFalseForSNF Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                                gv_qc.Rows(ii).Cells(colQCRange).Value = FATSNFValue
                                Exit For
                            End If
                        Else
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                                gv_qc.Rows(ii).Cells(colQCRange).Value = FATSNFValue
                                Exit For
                            End If
                        End If
                    End If
                End If
            End If
        Next
        isCellValueChanged = False
    End Sub

    Private Sub btngo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngo.Click
        'gv_qc.Rows.Clear()
        'BHA/19/07/18-000180 RICHA 
        'FillQCGrid(Nothing, Nothing, Nothing)
        FillQCGrid(Nothing, Nothing, Nothing, Nothing, Nothing)

    End Sub

    Private Sub FillQCGrid(ByVal CurrentIcode As String, ByVal Frm_Loc_Code As String, ByVal To_Loc_Code As String, ByVal UOM_CODE As String, ByVal is_sub_sec_main_location As Integer)
        Try
            If clsCommon.myLen(gv.Rows(0).Cells(colitemcode).Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                gv.Focus()
                gv.Select()
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
                For Each grow As GridViewRowInfo In gv.Rows
                    allicode = allicode + "','" + clsCommon.myCstr(grow.Cells(colitemcode).Value)
                    clsDBFuncationality.ExecuteNonQuery("insert into TEMP_LOC_QC_PARAM select '" + clsCommon.myCstr(grow.Cells(colitemcode).Value) + "','" + clsCommon.myCstr(grow.Cells(colloc_code).Value) + "','" + clsCommon.myCstr(grow.Cells(colToloc_code).Value) + "'")
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
                        If clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCToloc_code).Value), clsCommon.myCstr(dr("to_loc"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCloc_code).Value), clsCommon.myCstr(dr("frm_loc"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCparamcode).Value), clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colQCitemcode).Value), clsCommon.myCstr(dr("item_code"))) = CompairStringResult.Equal Then
                            found = True
                            Exit For
                        End If
                    Next

                    If Not found Then
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


                        ''richa 
                        If SettingPickFATSNFPerFromStock Or settTankerDispatchAvgFATSNFPer Then
                            If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_nature).Value), "Range") = CompairStringResult.Equal Then
                                'aa(Balwinder)
                                Dim dtFatSndClosing As DataTable = XpertERPEngine.clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(txtmain_Loc_Code.Value), clsCommon.myCstr(dr("frm_loc")), IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(Nothing), dtpDate.Value), Nothing, UOM_CODE, is_sub_sec_main_location) 'clsDBFuncationality.GetDataTable(qry)
                                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value), "FAT") = CompairStringResult.Equal Then
                                    If dtFatSndClosing IsNot Nothing AndAlso dtFatSndClosing.Rows.Count > 0 Then
                                        If clsCommon.myCdbl(dtFatSndClosing.Rows(0)("qty")) = 0 Then
                                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).Value = 0
                                        Else
                                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).Value = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(dr("Item_Code")), UOM_CODE, clsCommon.myCdbl(dtFatSndClosing.Rows(0)("qty")), clsCommon.myCdbl(dtFatSndClosing.Rows(0)("fat_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                                        End If
                                    End If
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCparam_type).Value), "SNF") = CompairStringResult.Equal Then
                                    If dtFatSndClosing IsNot Nothing AndAlso dtFatSndClosing.Rows.Count > 0 Then
                                        If clsCommon.myCdbl(dtFatSndClosing.Rows(0)("qty")) = 0 Then
                                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).Value = 0
                                        Else
                                            gv_qc.Rows(gv_qc.Rows.Count - 1).Cells(colQCRange).Value = clsBOM.GetFatSNFPercentage_AfterConversion(clsCommon.myCstr(dr("Item_Code")), UOM_CODE, clsCommon.myCdbl(dtFatSndClosing.Rows(0)("qty")), clsCommon.myCdbl(dtFatSndClosing.Rows(0)("snf_kg")), Nothing, settTankerDispatchAvgFATSNFPer)
                                        End If
                                    End If
                                End If
                                dtFatSndClosing = Nothing
                            End If
                        End If
                    End If
                Next
            Else
                'Throw New Exception("Mapped first QC parameter with items in Item Master screen")
            End If

            RefreshQCSNo()

            dt = Nothing
        Catch ex As Exception
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = "select count(*) from INFORMATION_SCHEMA.TABLES where TABLE_NAME='TEMP_LOC_QC_PARAM'"
            check = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                clsDBFuncationality.ExecuteNonQuery("drop table TEMP_LOC_QC_PARAM")
            End If

        End Try
    End Sub

    Sub RefreshQCSNo()
        For ii As Integer = gv_qc.Rows.Count - 1 To 0 Step -1
            Dim strICode As String = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCitemcode).Value)
            Dim strFromLocation As String = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCloc_code).Value)
            Dim strTo As String = clsCommon.myCstr(gv_qc.Rows(ii).Cells(colQCToloc_code).Value)
            Dim isFound As Boolean = False
            For jj As Integer = 0 To gv.Rows.Count - 1
                Dim strInnerICode As String = clsCommon.myCstr(gv.Rows(jj).Cells(colitemcode).Value)
                Dim strInnerFromLocation As String = clsCommon.myCstr(gv.Rows(jj).Cells(colloc_code).Value)
                Dim strInnerTo As String = clsCommon.myCstr(gv.Rows(jj).Cells(colToloc_code).Value)
                If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                    If clsCommon.CompairString(strInnerFromLocation, strFromLocation) = CompairStringResult.Equal Then
                        If clsCommon.CompairString(strInnerTo, strTo) = CompairStringResult.Equal Then
                            isFound = True
                            Exit For
                        End If
                    End If
                End If
            Next
            If Not isFound Then
                gv_qc.Rows.RemoveAt(ii)
            End If
        Next

        '===========refresh sno==
        For Each grow As GridViewRowInfo In gv_qc.Rows
            grow.Cells(colQCsno).Value = grow.Index + 1
        Next
    End Sub

    Private Sub cboStatus_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStatus.TextChanged
        'If btnPost.Enabled OrElse clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
        '    btnsave.Enabled = True
        'End If

        If clsCommon.CompairString(cboStatus.Text, "Open") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Pending
        ElseIf clsCommon.CompairString(cboStatus.Text, "On Hold") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Pending
        ElseIf clsCommon.CompairString(cboStatus.Text, "In-Active") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Cancel
        ElseIf clsCommon.CompairString(cboStatus.Text, "Approved") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Approved
            'btnsave.Enabled = False
        End If
        If btnsave.Enabled = False AndAlso btnPost.Enabled = False AndAlso clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
            UsLock1.Status = ERPTransactionStatus.Approved
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            gv.CurrentRow.Cells(colSno).Value = clsCommon.myCdbl(intCurrRow) + 1
            If intCurrRow = gv.Rows.Count - 1 AndAlso Not chkBO.IsChecked Then ''no row added when against bo
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnTo_Sectn_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles btnTo_Sectn.ToggleStateChanged, btnTo_SubLoc.ToggleStateChanged
        'MyLabel6.Text = IIf(btnTo_Sectn.IsChecked = False, "To Sub-Location", "To Section")
        'If gv.Columns.Count > 0 Then
        '    gv.Columns(colToloc_code).HeaderText = IIf(btnTo_Sectn.IsChecked = False, "To Sub-Location Code", "To Section Code")
        '    gv.Columns(colToloc_name).HeaderText = IIf(btnTo_Sectn.IsChecked = False, "To Sub-Location", "To Section")
        'End If
        'If gv_qc.Columns.Count > 0 Then
        '    gv_qc.Columns(colQCToloc_code).HeaderText = IIf(btnTo_Sectn.IsChecked = False, "To Sub-Location Code", "To Section Code")
        '    gv_qc.Columns(colQCToloc_name).HeaderText = IIf(btnTo_Sectn.IsChecked = False, "To Sub-Location", "To Section")
        'End If

        'txttosub.Value = ""
        'txttosub_name.Text = ""
    End Sub

    Private Sub btnFrm_Sectn_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles btnFrm_Sectn.ToggleStateChanged, btnFrm_SubLoc.ToggleStateChanged
        'MyLabel4.Text = IIf(btnFrm_Sectn.IsChecked = False, "From Sub-Location", "From Section")
        'If gv.Columns.Count > 0 Then
        '    gv.Columns(colloc_code).HeaderText = IIf(btnFrm_Sectn.IsChecked = False, "From Sub-Location Code", "From Section Code")
        '    gv.Columns(colloc_name).HeaderText = IIf(btnFrm_Sectn.IsChecked = False, "From Sub-Location", "From Section")
        'End If
        'If gv_qc.Columns.Count > 0 Then
        '    gv_qc.Columns(colQCloc_code).HeaderText = IIf(btnFrm_Sectn.IsChecked = False, "From Sub-Location Code", "From Section Code")
        '    gv_qc.Columns(colQCloc_name).HeaderText = IIf(btnFrm_Sectn.IsChecked = False, "From Sub-Location", "From Section")
        'End If

        'PS-FQ = ""
        'txtfrmsub_name.Text = ""
    End Sub

    Private Sub txtmain_Loc_Code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtmain_Loc_Code._MYValidating
        If clsCommon.myLen(arrLoc) <= 0 Then
            clsCommon.MyMessageBoxShow("No location rights.")
            Exit Sub
        End If

        txtmain_Loc_Code.Value = clsLocation.getFinder(" tspl_location_master.location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y'", txtmain_Loc_Code.Value, isButtonClicked)
        txtMain_Loc_Name.Text = clsLocation.GetName(txtmain_Loc_Code.Value, Nothing)


        txtfrmsub.Value = ""
        txtfrmsub_name.Text = ""
        txttosub.Value = ""
        txttosub_name.Text = ""
        txtbatchorder.Value = ""
        txtbatch_desc.Text = ""
        txtlocation.Text = ""
        txtlocationname.Text = ""
        gv.Rows.Clear()
        gv.Rows.AddNew()
        gv_qc.Rows.Clear()
        gv_qc.Rows.AddNew()

    End Sub

    Private Sub gv_qc_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_qc.CurrentColumnChanged
        If gv_qc.RowCount > 0 Then
            gv_qc.CurrentRow.Cells(colQCHistort).Value = "Double Click"
            'If intCurrRow = gv.Rows.Count - 1 Then
            '    gv.Rows.AddNew()
            '    gv.CurrentRow = gv.Rows(intCurrRow)
            'End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If gv.Rows.Count > 0 Then
            If Not clsCommon.MyMessageBoxShow("Are you sure,want to delete current row?", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
                Exit Sub
            End If
            Dim intRow As Integer = gv.CurrentRow.Index
            Dim oldIcode As String = clsCommon.myCstr(gv.Rows(intRow).Cells(colitemcode).Value)
            Dim oldFrmLoc As String = clsCommon.myCstr(gv.Rows(intRow).Cells(colloc_code).Value)
            Dim OldToLoc As String = clsCommon.myCstr(gv.Rows(intRow).Cells(colToloc_code).Value)
            If clsCommon.myLen(oldIcode) > 0 Then
                RemoveCurrentItemQCRow(oldIcode, oldFrmLoc, OldToLoc)
            End If
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub btnunpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnunpost.Click
        Try
            If common.clsCommon.MyMessageBoxShow("Amend and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' reason for reverse
                Dim Reason As String = ""
                'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Amendment"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Throw New Exception("Fill amendment remarks.")
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If


                If clsProcessProductionIssueEntry.UnpostData(txtCode.Value, Me.Form_ID) Then
                    '------------------
                    Dim obj As New clsCancelLog
                    obj.Program_Code = Me.Form_ID
                    obj.DOCUMENT_NO = clsCommon.myCstr(txtCode.Value)
                    obj.REASON = Reason
                    obj.ACTIVITY_TYPE = Nothing
                    If clsCancelLog.SaveData(obj, True, Nothing) Then
                        common.clsCommon.MyMessageBoxShow("Successfully Unpost and Recreated", Me.Text)
                        LoadData(txtCode.Value, NavigatorType.Current)
                    End If
                    '-----------------------------
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkBO_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkBO.ToggleStateChanged, chkWOBO.ToggleStateChanged
        txtbatchorder.Enabled = chkBO.IsChecked
        fndSection.Enabled = chkBO.IsChecked
        txtbatchorder.Value = ""
        txtbatch_desc.Text = ""
        txtlocation.Text = ""
        txtlocationname.Text = ""
        fndSection.Value = ""
        TxtSection.Text = ""
        gv.Rows.Clear()
        gv.Rows.AddNew()
        gv_qc.Rows.Clear()
        gv_qc.Rows.AddNew()

        gv.Columns(colitemcode).ReadOnly = chkBO.IsChecked
        gv.Columns(colTo_Loc_Type).ReadOnly = chkBO.IsChecked
        isCellValueChanged = True
        gv.Rows(0).Cells(colTo_Loc_Type).Value = "SEC"
        isCellValueChanged = False
    End Sub

    Private Sub fndSection__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSection._MYValidating
        Try
            Dim qry As String = "select Section_Code as Code,Description as Name from TSPL_SECTION_MASTER"
            fndSection.Value = clsCommon.ShowSelectForm("PBOSECFND", qry, "Code", " ", fndSection.Value, "", isButtonClicked)
            TxtSection.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_SECTION_MASTER where Section_Code='" + fndSection.Value + "'"))
            txtbatchorder.Value = ""
            txtbatch_desc.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub SplitContainer3_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer3.SplitterMoved

    End Sub

    Private Sub gv_Click(sender As Object, e As EventArgs) Handles gv.Click

    End Sub

    Private Sub MyLabel18_Click(sender As Object, e As EventArgs) Handles MyLabel18.Click

    End Sub

    Private Sub MyComboBox1_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles MyComboBox1.SelectedIndexChanged

    End Sub

    Private Sub MyLabel12_Click(sender As Object, e As EventArgs) Handles MyLabel12.Click

    End Sub

    Private Sub RadGroupBox3_Click(sender As Object, e As EventArgs) Handles RadGroupBox3.Click

    End Sub
    ' Ticket No : TEC/29/10/18-000347 By Prabhakar
    Private Sub btnShowInventory_Click(sender As Object, e As EventArgs) Handles btnShowInventory.Click
        clsOpenInventory.ShowInventoryDatails(txtCode.Value)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Issue Code")
                Exit Sub
            End If
            clsERPFuncationalityold.ShowTransHistoryData(txtCode.Value, "issue_code", "TSPL_PP_ISSUE_HEAD", "TSPL_PP_ISSUE_ITEM_DETAIL")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CancelData()
    End Sub

    Function CancelData() As Boolean
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code is empty")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure to Cancel the Record?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
            clsProcessProductionIssueEntry.CancelData(Me.Form_ID, txtCode.Value)
            clsCommon.MyMessageBoxShow("Successfully Cancelled", Me.Text)
            FunReset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub gv_FontChanged(sender As Object, e As EventArgs) Handles gv.FontChanged

    End Sub
End Class
