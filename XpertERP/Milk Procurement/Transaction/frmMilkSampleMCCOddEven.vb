Imports System.Data.SqlClient
Imports System.IO
Imports common


Public Class frmMilkSampleMCCOddEven
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False

    Const colSRNo As String = "SRNo"
    Const ColVlcDocCode As String = "ColVlcDocCode"
    Const ColVlcCode As String = "ColVlcCode"
    Const ColSampleNo As String = "ColSampleNo"
    Const colMilkType As String = "colMilkType"
    Const ColType As String = "ColType"
    Const ColVspCode As String = "ColVspCode"
    Const ColItemCode As String = "ColItemCode"
    Const ColQty As String = "ColQty"
    Const ColMILK_Qty As String = "ColMILK_Qty"
    Const colFAT As String = "ColFAT"
    Const colSNF As String = "ColSNF"
    Const colPrevFAT As String = "ColPrevFAT"
    Const colPrevSNF As String = "ColPrevSNF"
    Const colCOR As String = "ColCOR"
    Const colCLR As String = "ColCLR"
    Const ColRATE As String = "ColRATE"
    Const COLAMOUNT As String = "COLAMOUNT"
    Const COL_IS_MANUAL As String = "IS_MANUAL"
    Const colSRN_CODE As String = "SRN_CODE"
    Const colECOPro As String = "ColECoPro"
    Const ColUomCode As String = "ColUomCode"
    Const ColPriceCode As String = "ColPriceCode"
    Const ColIS_Saved As String = "ColIS_Saved"
    Const colFATOrg As String = "colFATOrg"
    Const colSNFOrg As String = "colSNFOrg"
    Const colFATCorrection As String = "colFATCorrection"
    Const colSNFCorrection As String = "colSNFCorrection"

    Dim isInsideLoadData As Boolean = False
    Dim isInsideImportData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim isCellValueChanged As Boolean = True
    Dim isForcellyStarted As Boolean = False
    Dim IsCanceled As Boolean = False
    Dim IsShowTwoGrid As Boolean = False
    Dim IsRoundOffPaiseAmount As Boolean
    Public DtMilkReceipt, DtVehicle, DtMilkReceiptEcoProWise, DtVSPChargeDetail, DtPriceChargeDetail As DataTable

    Public objEco As New clsEkoPro
    Public objSr As New clsSerialPort
    Public objSr2 As New clsSerialPort
    Dim oldValue As Double = 0.0
    Dim is_Manual As Boolean = False
    Dim irowIndex_Global_gv1 As Integer = -1
    Dim irowIndex_Global_gv2 As Integer = -1
    Dim Issaved As Boolean = False
    Public Shared isPortOpened As Boolean = False
    Public DocumentNo As String = Nothing
    Dim DtMilkTypeRange As New DataTable
    Dim Irregular_Mcc_Code As String = String.Empty

    Private isStopForRepeatedFATSNF As Boolean = False

    Private GridFontSize As Integer = 0
    Private GridFont As Font
    Dim settMaxReceiveSNFPer As Decimal = 0 ''BHA/21/06/18-000069 by balwinder on 22/06/2018
    Dim settMaxFATPerLimit As Decimal = 0
    Dim settMaxSNFPerLimit As Decimal = 0
    Dim SettMaintainLogForImporperSample As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = False 'MyBase.isDeleteFlag
    End Sub

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SettMaintainLogForImporperSample = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaintainLogForImporperSample, clsFixedParameterCode.MaintainLogForImporperSample, Nothing)) = 1)
        isStopForRepeatedFATSNF = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StopForRepeatedFATSNF, clsFixedParameterCode.StopForRepeatedFATSNF, Nothing)) = 0, False, True) ''Make Setting Balwinder
        GridFontSize = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SampleFONTSize, clsFixedParameterCode.SampleFONTSize, Nothing)) ''Make Setting Balwinder
        If GridFontSize < 8 Then
            GridFontSize = 15
        End If
        settMaxReceiveSNFPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxReceiveSNFPer, clsFixedParameterCode.MaxReceiveSNFPer, Nothing))

        settMaxFATPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxFATPerLimit, clsFixedParameterCode.MaxFATPerLimit, Nothing))
        settMaxSNFPerLimit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MaxSNFPerLimit, clsFixedParameterCode.MaxSNFPerLimit, Nothing))
        GridFont = New Font("Ariel", GridFontSize)
        IsShowTwoGrid = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkSamplShowOddEvenTwoGrid, clsFixedParameterCode.MilkSamplShowOddEvenTwoGrid, Nothing)) = 1, True, False)
        IsRoundOffPaiseAmount = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Nothing)) = 1
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Custom Fields

        cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(True)
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"

        GetshiftType()
        GetECOPro(cboECOPro)
        GetECOPro(CboEcoPro2)
        AddNew()

        TxtRangeTo.Text = Nothing
        txtRangeFrom.Text = Nothing


        Dim viewMilkReceiptSample As Boolean = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select View_Milk_Receipt_Sample from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' ")) = 1)
        Me.txtMilkReceiptNo.Tag = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtMilkReceiptNo.Tag) <= 0 Then
            If Not viewMilkReceiptSample Then
                Throw New Exception("Please set Default location of current user")
            End If
        End If

        If clsCommon.myLen(txtMilkReceiptNo.Tag) > 0 Then
            Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(txtMilkReceiptNo.Tag)
            If DTShift IsNot Nothing AndAlso DTShift.Rows.Count > 0 Then
                dtpDocDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
                cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))
                dtpDocDate.ReadOnly = True
                cboShift.Enabled = False
            Else
                If Not viewMilkReceiptSample Then
                    Throw New Exception("No Milk Collected. No Sample can be Done.")
                End If
            End If
            SetDocKCollectionMilkType(txtMilkReceiptNo.Tag)
            txtCode.Value = clsMilkSampleMCC.GetDocCode(clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy"), txtMilkReceiptNo.Tag, cboShift.SelectedValue, Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), lblDockCode.Text)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                txtRangeFrom.Text = SetFromRange(txtCode.Value, Nothing)
                TxtRangeTo.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
            End If
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtMilkReceiptNo.Value = clsMilkReceiptMCC.GetDocCode(clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy"), txtMilkReceiptNo.Tag, cboShift.SelectedValue, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), lblDockCode.Text)
                If clsCommon.myLen(txtMilkReceiptNo.Value) > 0 Then
                    txtRangeFrom.Text = clsCommon.myCdbl(gv1.Rows.Count) + 1
                    TxtRangeTo.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
                    Load_Receipt_From_Dt(gv1, False)
                    If IsShowTwoGrid Then
                        Load_Receipt_From_Dt(gv2, False)
                    End If
                End If
            End If
        End If

        txtRangeFrom.ReadOnly = False
        TxtRangeTo.ReadOnly = True
        If IsShowTwoGrid Then
            SplitContainer2.Panel2Collapsed = False
            GrpEcoPro2.Enabled = True
            CboMachine.SelectedValue = clsMccMaster.DefaultSampleMachine(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
            cboComPort.SelectedValue = clsMccMaster.DefaultSampleComport(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)

            CboMachine2.SelectedValue = clsMccMaster.DefaultSampleMachine2(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
            CboComport2.SelectedValue = clsMccMaster.DefaultSampleComport2(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
            TxtRangeTo.ReadOnly = False
            txtRangeFrom.ReadOnly = False
            RadGroupBox2.Text += "( ODD )"
            RadGroupBox4.Text += " ( Even ) "
        Else
            SplitContainer2.Panel2Collapsed = True
            CboMachine.SelectedValue = clsMccMaster.DefaultSampleMachine(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
            cboComPort.SelectedValue = clsMccMaster.DefaultSampleComport(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
        End If

        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, "", Nothing, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        GetMilkTypeDt()
    End Sub

    Sub SetDocKCollectionMilkType(ByVal strMCCCode As String)
        If clsCommon.myLen(strMCCCode) > 0 Then
            Dim qry As String = "select Is_Seprate_Dock_Cow_Buffalo from TSPL_MCC_MASTER where MCC_Code='" + strMCCCode + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
                Dim frm As New XpertERPEngine.FrmFreeComboBox()
                frm.ComboSource = clsMilkReceiptMCC.GetDockCollectionMilkType(False)
                frm.ComboValueMember = "Code"
                frm.ComboDisplayMember = "Name"
                frm.isAcceptOKOnlyMandatory = True
                frm.ShowDialog()
                cboDockCollectionMilkType.SelectedValue = frm.strRetValue
            Else
                cboDockCollectionMilkType.SelectedValue = "M"
            End If
            cboDockCollectionMilkType.Enabled = False

            qry = "select Code,Description from TSPL_DOCK_MASTER where MCC_Code='" + strMCCCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                pnlDock.Visible = True
                If dt.Rows.Count = 1 Then
                    lblDockCode.Text = clsCommon.myCstr(dt.Rows(0)("Code"))
                    lblDockName.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
                Else
                    Dim frm As New XpertERPEngine.FrmFreeComboBox()
                    frm.ComboSource = dt
                    frm.ComboValueMember = "Code"
                    frm.ComboDisplayMember = "Description"
                    frm.isAcceptOKOnlyMandatory = True
                    frm.ShowDialog()
                    lblDockCode.Text = frm.strRetValue
                    lblDockName.Text = clsDockMaster.GetName(lblDockCode.Text)
                End If
            Else
                pnlDock.Visible = False
            End If
        End If
    End Sub

    Sub GetMilkTypeDt()
        Try
            DtMilkTypeRange.Columns.Add("Field")
            DtMilkTypeRange.Columns("Field").ReadOnly = True
            DtMilkTypeRange.Columns.Add("FAT(Min)")
            DtMilkTypeRange.Columns.Add("FAT(Max)")
            DtMilkTypeRange.Columns.Add("SNF(Min)")
            DtMilkTypeRange.Columns.Add("SNF(Max)")

            Dim dr As DataRow = DtMilkTypeRange.NewRow()
            dr("Field") = "Cow"
            dr("FAT(Min)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinCow, clsFixedParameterCode.FatMinCow, Nothing))
            dr("FAT(Max)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxCow, clsFixedParameterCode.FatMaxCow, Nothing))
            dr("SNF(Min)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinCow, clsFixedParameterCode.FatMinCow, Nothing))
            dr("SNF(Max)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxCow, clsFixedParameterCode.SNFMaxCow, Nothing))
            DtMilkTypeRange.Rows.Add(dr)

            dr = DtMilkTypeRange.NewRow()
            dr("Field") = "Buffalow"
            dr("FAT(Min)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinBuff, clsFixedParameterCode.FatMinBuff, Nothing))
            dr("FAT(Max)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxBuff, clsFixedParameterCode.FatMaxBuff, Nothing))
            dr("SNF(Min)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinBuff, clsFixedParameterCode.SNFMinBuff, Nothing))
            dr("SNF(Max)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxBuff, clsFixedParameterCode.SNFMaxBuff, Nothing))
            DtMilkTypeRange.Rows.Add(dr)

            dr = DtMilkTypeRange.NewRow()
            dr("Field") = "Mix"
            dr("FAT(Min)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinMix, clsFixedParameterCode.FatMinMix, Nothing))
            dr("FAT(Max)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxMix, clsFixedParameterCode.FatMaxMix, Nothing))
            dr("SNF(Min)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinMix, clsFixedParameterCode.SNFMinMix, Nothing))
            dr("SNF(Max)") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxMix, clsFixedParameterCode.SNFMaxMix, Nothing))
            DtMilkTypeRange.Rows.Add(dr)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Public Sub GetECOPro(ByVal cmb As common.Controls.MyComboBox)
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


        cmb.DataSource = dt
        cmb.ValueMember = "Code"
        cmb.DisplayMember = "Name"
    End Sub

    Function SaveData(ByVal GGGrid As RadGridView, ByVal IsPostData As Boolean) As Boolean
        LoadDt()
        clsCommon.ProgressBarShow()
        Try
            isInsideImportData = True
            Dim totqty As Double = 0
            Dim totsnf As Double = 0
            Dim totfat As Double = 0
            Dim counter As Integer = 0

            For Each grow As GridViewRowInfo In GGGrid.Rows
                counter += 1
                If grow.Cells(colFAT).Value <= 0 Or grow.Cells(colSNF).Value <= 0 Then
                    Throw New Exception("Please Fill Fat And SNF in all Rows. sample no [" & clsCommon.myCstr(grow.Cells(colSRNo).Value) & "]")
                End If
                isCellValueChanged = False
                grow.Cells(colFAT).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(colFAT).Value) * 10) / 10
                grow.Cells(colSNF).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(colSNF).Value) * 10) / 10
                isCellValueChanged = True

                If settMaxFATPerLimit > 0 Then
                    If clsCommon.myCdbl(grow.Cells(colFAT).Value) > settMaxFATPerLimit Then
                        Throw New Exception("FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit) + ". sample no [" & clsCommon.myCstr(grow.Cells(colSRNo).Value) & "]")
                    End If
                End If
                If settMaxSNFPerLimit > 0 Then
                    If clsCommon.myCdbl(grow.Cells(colSNF).Value) > settMaxSNFPerLimit Then
                        Throw New Exception("SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit) + ". sample no [" & clsCommon.myCstr(grow.Cells(colSRNo).Value) & "]")
                    End If
                End If

                grow.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCdbl(grow.Cells(colCOR).Value))
                Dim strMilkType As String = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    strMilkType = clsCommon.myCstr(grow.Cells(ColType).Value)
                End If
                Dim strPriceCode As String = ""
                grow.Cells(ColRATE).Value = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCdbl(grow.Cells(ColQty).Value), strPriceCode, clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(txtMilkReceiptNo.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, strMilkType)
                grow.Cells(ColPriceCode).Value = strPriceCode
                grow.Cells(COLAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(ColRATE).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value), 2, MidpointRounding.AwayFromZero)
                totqty += clsCommon.myCdbl(grow.Cells(ColQty).Value)
                totsnf += clsCommon.myCdbl(grow.Cells(colSNF).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                totfat += clsCommon.myCdbl(grow.Cells(colFAT).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                clsCommon.ProgressBarUpdate(IIf(IsPostData, "Sample Checked : ", "Sample Saved : ") & counter & "/" & GGGrid.Rows.Count)
            Next
            txtTotalQty.Text = totqty
            txttotSNF.Text = totsnf
            txttotFAT.Text = totfat
            isInsideImportData = False
            If (AllowToSave(GGGrid, IsPostData)) Then
                Dim objHead As clsMilkSampleMCC
                objHead = New clsMilkSampleMCC
                objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
                objHead.MCC_CODE = clsCommon.myCstr(txtMilkReceiptNo.Tag)
                objHead.MILK_RECEIPT_CODE = clsCommon.myCstr(txtMilkReceiptNo.Value)
                objHead.TOTAL_QTY = clsCommon.myCdbl(txtTotalQty.Text)
                objHead.TOTAL_FAT = clsCommon.myCdbl(txttotFAT.Text)
                objHead.TOTAL_SNF = clsCommon.myCdbl(txttotSNF.Text)
                objHead.DOCK_Code = lblDockCode.Text
                objHead.Dock_Collection_Milk_Type = cboDockCollectionMilkType.SelectedValue
                Dim objList As New List(Of clsMilkSampleMCCDetail)
                Dim objListHistory As New List(Of clsMilkSampleMCCDetailHistory)

                Dim obj1 As clsMilkSampleMCCDetail
                Dim objH As clsMilkSampleMCCDetailHistory
                For Each grow As GridViewRowInfo In GGGrid.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)) > 0 Then
                        Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & txtMilkReceiptNo.Value & "' and sample_nO='" & clsCommon.myCstr(grow.Cells(colSRNo).Value) & "'")
                        obj1 = New clsMilkSampleMCCDetail()

                        obj1.DOC_CODE = txtCode.Value
                        obj1.SAMPLE_NO = clsCommon.myCdbl(grow.Cells(colSRNo).Value)
                        obj1.VLC_DOC_CODE = clsCommon.myCstr(dr(0)("VLC_DOC_CODE"))
                        obj1.MILK_TYPE = clsCommon.myCstr(dr(0)("Milk_Type"))
                        obj1.TYPE = clsCommon.myCstr(dr(0)("Type"))
                        obj1.VSP_CODE = clsCommon.myCstr(dr(0)("VSP_code"))
                        obj1.ITEm_CODE = clsCommon.myCstr(dr(0)("Item_code"))
                        obj1.MILK_Qty = clsCommon.myCdbl(dr(0)("Milk_Weight"))
                        obj1.ACC_Qty = clsCommon.myCdbl(dr(0)("ACC_Weight"))

                        dr = Nothing
                        obj1.FAT = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                        obj1.SNF = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                        obj1.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
                        obj1.Correction_Factor = clsCommon.myCdbl(grow.Cells(colCOR).Value)
                        obj1.RATE = clsCommon.myCdbl(grow.Cells(ColRATE).Value)
                        obj1.UOM_Code = clsCommon.myCstr(grow.Cells(ColUomCode).Value)
                        obj1.Price_Code = clsCommon.myCstr(grow.Cells(ColPriceCode).Value)
                        obj1.AMOUNT = clsCommon.myCdbl(obj1.RATE * obj1.MILK_Qty)
                        obj1.Is_Entered_Manualy = clsCommon.myCstr(grow.Cells(COL_IS_MANUAL).Value)

                        obj1.FAT_ORG = clsCommon.myCdbl(grow.Cells(colFATOrg).Value)
                        obj1.SNF_ORG = clsCommon.myCdbl(grow.Cells(colSNFOrg).Value)
                        obj1.FAT_CORRECTION = clsCommon.myCdbl(grow.Cells(colFATCorrection).Value)
                        obj1.SNF_CORRECTION = clsCommon.myCdbl(grow.Cells(colSNFCorrection).Value)

                        objList.Add(obj1)

                        If clsCommon.myCdbl(grow.Cells(colPrevFAT).Value) > 0 And ((clsCommon.myCdbl(grow.Cells(colFAT).Value) <> clsCommon.myCdbl(grow.Cells(colPrevFAT).Value)) Or (clsCommon.myCdbl(grow.Cells(colSNF).Value) <> clsCommon.myCdbl(grow.Cells(colPrevSNF).Value))) Then
                            objH = New clsMilkSampleMCCDetailHistory
                            objH.DOC_CODE = txtCode.Value
                            objH.DOC_Date = clsCommon.myCDate(dtpDocDate.Value)
                            objH.VLC_DOC_CODE = clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)
                            objH.New_FAT = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                            objH.New_SNF = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                            objH.Prev_FAT = clsCommon.myCdbl(grow.Cells(colPrevFAT).Value)
                            objH.Prev_SNF = clsCommon.myCdbl(grow.Cells(colPrevSNF).Value)

                            objListHistory.Add(objH)

                            grow.Cells(colPrevSNF).Value = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                            grow.Cells(colPrevFAT).Value = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                        End If
                    End If
                Next
                ''For Custom Fields
                objHead.Form_ID = MyBase.Form_ID
                objHead.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(objHead.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(objHead.arrCustomFields, GGGrid, MyBase.ArrDetailFields, ColItemCode)
                End If
                ''End of For Custom Fields
                If clsMilkSampleMCC.SaveData(objHead, objList, objListHistory) Then
                    ' trans.Commit()
                    UcAttachment1.SaveData(objHead.DOC_CODE)
                    txtCode.Value = objHead.DOC_CODE
                End If
            Else
                clsCommon.ProgressBarHide()
                GC.Collect()
                totfat = Nothing
                totsnf = Nothing
                totqty = Nothing
                GC.Collect()
                Return False
            End If
            clsCommon.ProgressBarHide()
            GC.Collect()
            totfat = Nothing
            totsnf = Nothing
            totqty = Nothing
            GC.Collect()
            Return True
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Function SaveData_Record(ByVal GGGrid As RadGridView)
        LoadDt()
        Dim trans As SqlTransaction = Nothing
        Try
            isInsideImportData = True
            Dim totqty As Double = 0
            Dim totsnf As Double = 0
            Dim totfat As Double = 0
            For Each grow As GridViewRowInfo In GGGrid.Rows
                If clsCommon.myCdbl(grow.Cells(colFAT).Value) > 0 And clsCommon.myCdbl(grow.Cells(colSNF).Value) > 0 Then
                    Try
                        Dim strMilkType As String = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                        If objCommonVar.DisplayTypeInMilkReceipt Then
                            strMilkType = clsCommon.myCstr(grow.Cells(ColType).Value)
                        End If
                        Dim strPriceCode As String = ""
                        grow.Cells(ColRATE).Value = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCdbl(grow.Cells(ColQty).Value), strPriceCode, clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(txtMilkReceiptNo.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, strMilkType)
                        grow.Cells(ColPriceCode).Value = strPriceCode
                        grow.Cells(COLAMOUNT).Value = clsCommon.myCdbl(grow.Cells(ColRATE).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value)
                        grow.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCdbl(grow.Cells(colCOR).Value))
                        totqty += clsCommon.myCdbl(grow.Cells(ColQty).Value)
                        totsnf += clsCommon.myCdbl(grow.Cells(colSNF).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                        totfat += clsCommon.myCdbl(grow.Cells(colFAT).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100

                    Catch ex As Exception
                        Dim x As String = ex.Message
                    End Try
                End If
            Next


            txtTotalQty.Text = totqty
            txttotSNF.Text = totsnf
            txttotFAT.Text = totfat

            trans = clsDBFuncationality.GetTransactin()
            Dim objHead As clsMilkSampleMCC
            objHead = New clsMilkSampleMCC
            objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
            objHead.DOC_DATE = clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy hh:mm:ss tt")
            objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
            objHead.MCC_CODE = clsCommon.myCstr(txtMilkReceiptNo.Tag)
            objHead.MILK_RECEIPT_CODE = clsCommon.myCstr(txtMilkReceiptNo.Value)
            objHead.TOTAL_QTY = clsCommon.myCdbl(txtTotalQty.Text)
            objHead.TOTAL_FAT = clsCommon.myCdbl(txttotFAT.Text)
            objHead.TOTAL_SNF = clsCommon.myCdbl(txttotSNF.Text)
            objHead.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
            objHead.DOCK_Code = lblDockCode.Text
            Dim objList As New List(Of clsMilkSampleMCCDetail)
            Dim objListHistory As New List(Of clsMilkSampleMCCDetailHistory)

            Dim obj1 As clsMilkSampleMCCDetail
            Dim objH As clsMilkSampleMCCDetailHistory

            For Each grow As GridViewRowInfo In GGGrid.Rows
                If clsCommon.myCdbl(grow.Cells(colFAT).Value) > 0 And clsCommon.myCdbl(grow.Cells(colSNF).Value) > 0 And clsCommon.myCstr(grow.Cells(colSNF).Value) <> "T" Then 'And irowIndex_Global_GGGrid = grow.Index Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)) > 0 Then
                        Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & txtMilkReceiptNo.Value & "' and sample_nO='" & clsCommon.myCstr(grow.Cells(colSRNo).Value) & "'")
                        obj1 = New clsMilkSampleMCCDetail()
                        obj1.DOC_CODE = txtCode.Value
                        obj1.SAMPLE_NO = clsCommon.myCdbl(grow.Cells(colSRNo).Value)
                        obj1.VLC_DOC_CODE = clsCommon.myCstr(dr(0)("VLC_DOC_CODE"))

                        obj1.MILK_TYPE = clsCommon.myCstr(dr(0)("Milk_Type"))
                        obj1.TYPE = clsCommon.myCstr(dr(0)("Type"))
                        obj1.VSP_CODE = clsCommon.myCstr(dr(0)("VSP_code"))
                        obj1.ITEm_CODE = clsCommon.myCstr(dr(0)("Item_code"))
                        obj1.MILK_Qty = clsCommon.myCdbl(dr(0)("Milk_Weight"))
                        obj1.ACC_Qty = clsCommon.myCdbl(dr(0)("ACC_Weight"))
                        obj1.FAT = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                        obj1.SNF = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                        obj1.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
                        obj1.Correction_Factor = clsCommon.myCdbl(grow.Cells(colCOR).Value)
                        obj1.RATE = clsCommon.myCdbl(grow.Cells(ColRATE).Value)
                        obj1.UOM_Code = clsCommon.myCstr(grow.Cells(ColUomCode).Value)
                        obj1.Price_Code = clsCommon.myCstr(grow.Cells(ColPriceCode).Value)
                        obj1.AMOUNT = clsCommon.myCdbl(obj1.RATE * obj1.MILK_Qty) 'clsCommon.myCdbl(grow.Cells(COLAMOUNT).Value)
                        obj1.Is_Entered_Manualy = clsCommon.myCstr(grow.Cells(COL_IS_MANUAL).Value)
                        objList.Add(obj1)
                        If GGGrid Is gv1 Then
                            irowIndex_Global_gv1 = grow.Index
                        ElseIf GGGrid Is gv2 Then
                            irowIndex_Global_gv2 = grow.Index
                        End If
                        If clsCommon.myCdbl(grow.Cells(colPrevFAT).Value) > 0 And ((clsCommon.myCdbl(grow.Cells(colFAT).Value) <> clsCommon.myCdbl(grow.Cells(colPrevFAT).Value)) Or (clsCommon.myCdbl(grow.Cells(colSNF).Value) <> clsCommon.myCdbl(grow.Cells(colPrevSNF).Value))) Then
                            objH = New clsMilkSampleMCCDetailHistory
                            objH.DOC_CODE = txtCode.Value
                            objH.DOC_Date = clsCommon.myCDate(dtpDocDate.Value)
                            objH.VLC_DOC_CODE = clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)
                            objH.New_FAT = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                            objH.New_SNF = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                            objH.Prev_FAT = clsCommon.myCdbl(grow.Cells(colPrevFAT).Value)
                            objH.Prev_SNF = clsCommon.myCdbl(grow.Cells(colPrevSNF).Value)
                            objListHistory.Add(objH)
                            grow.Cells(colPrevSNF).Value = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                            grow.Cells(colPrevFAT).Value = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                            grow.Cells(ColIS_Saved).Value = "T"

                        End If
                    End If
                End If
            Next
            If clsMilkSampleMCC.SaveData(objHead, objList, objListHistory, trans) Then
                trans.Commit()
                isInsideImportData = False
                UcAttachment1.SaveData(objHead.DOC_CODE)
                txtCode.Value = objHead.DOC_CODE
                Return True
            End If
            isInsideImportData = False
            Return True
        Catch ex As Exception
            trans.Rollback()
            If ex.Message.Contains("Milk Type [") Then
                If GGGrid Is gv1 Then
                    gv1.Rows(irowIndex_Global_gv1).Cells(colFAT).Value = 0
                    gv1.Rows(irowIndex_Global_gv1).Cells(colSNF).Value = 0
                    irowIndex_Global_gv1 = -1
                ElseIf GGGrid Is gv2 Then
                    gv2.Rows(irowIndex_Global_gv2).Cells(colFAT).Value = 0
                    gv2.Rows(irowIndex_Global_gv2).Cells(colSNF).Value = 0
                    irowIndex_Global_gv2 = -1
                End If
            End If
            isInsideImportData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Function SaveSRNData(ByVal gv As RadGridView) As Boolean
        Try
            Dim counter As Integer = 0
            Dim objHead As clsMilkSRNMCC
            Dim objList As New List(Of clsMilkSRNMCCDetail)
            Dim obj1 As clsMilkSRNMCCDetail
            Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
            Dim objVSP_Charge1 As clsMilkSRNVSpChargeDetail
            Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
            Dim objPrice_Charge1 As clsMilkSRNPriceChargeDetail
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            clsCommon.ProgressBarShow()
            For Each grow As GridViewRowInfo In gv.Rows
                counter += 1
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRN_CODE).Value)) <= 1 Then
                    objList = New List(Of clsMilkSRNMCCDetail)
                    objVSPChargeList = New List(Of clsMilkSRNVSpChargeDetail)
                    objPriceChargeList = New List(Of clsMilkSRNPriceChargeDetail)
                    Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & txtMilkReceiptNo.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value) & "'")
                    objHead = New clsMilkSRNMCC
                    objHead.MILK_SAMPLE_CODE = clsCommon.myCstr(txtCode.Value)
                    objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                    objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
                    objHead.COMM_PORT = clsCommon.myCstr(cboComPort.SelectedValue)
                    objHead.MCC_CODE = IIf(clsCommon.myLen(clsCommon.myCstr(dr(0)("Irregular_MCC_CODE"))) > 0, clsCommon.myCstr(dr(0)("Irregular_MCC_CODE")), clsCommon.myCstr(dr(0)("MCC_CODE"))) 'clsCommon.myCstr(dr(0)("MCC_CODE"))
                    objHead.SAMPLE_NO = clsCommon.myCdbl(grow.Cells(colSRNo).Value)
                    objHead.VLC_DOC_CODE = clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)
                    objHead.VEHICLE_CODE = clsCommon.myCstr(dr(0)("VEHICLE_CODE"))
                    objHead.VLC_CODE = clsCommon.myCstr(dr(0)("VLC_CODE"))
                    objHead.ROUTE_CODE = clsCommon.myCstr(dr(0)("ROUTE_CODE"))
                    objHead.VSP_CODE = clsCommon.myCstr(grow.Cells(ColVspCode).Value)
                    If DtVehicle.Select("Vehicle_Code='" & dr(0)("VEHICLE_CODE") & "'").Length > 0 Then
                        objHead.TransPorter = clsCommon.myCstr(DtVehicle.Select("Vehicle_Code='" & dr(0)("VEHICLE_CODE") & "'")(0)("Vendor_Code"))
                    End If
                    objHead.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)

                    obj1 = New clsMilkSRNMCCDetail()
                    obj1.Item_CODE = clsCommon.myCstr(grow.Cells(ColItemCode).Value)
                    obj1.MILK_Qty = clsCommon.myCdbl(grow.Cells(ColQty).Value)
                    obj1.ACC_Qty = clsCommon.myCdbl(grow.Cells(ColMILK_Qty).Value)
                    obj1.FAT = clsCommon.myCdbl(grow.Cells(colFAT).Value)
                    obj1.SNF = clsCommon.myCdbl(grow.Cells(colSNF).Value)
                    obj1.MCC_CODE = IIf(clsCommon.myLen(clsCommon.myCstr(dr(0)("Irregular_MCC_CODE"))) > 0, clsCommon.myCstr(dr(0)("Irregular_MCC_CODE")), clsCommon.myCstr(dr(0)("MCC_CODE")))
                    obj1.Correction_Factor = clsCommon.myCdbl(grow.Cells(colCOR).Value)
                    obj1.RATE = clsCommon.myCdbl(grow.Cells(ColRATE).Value)
                    obj1.UOM = clsCommon.myCstr(grow.Cells(ColUomCode).Value)
                    obj1.Price_Code = clsCommon.myCstr(grow.Cells(ColPriceCode).Value)
                    obj1.AMOUNT = clsCommon.myCdbl(grow.Cells(COLAMOUNT).Value)
                    obj1.CLR = clsCommon.myCdbl(grow.Cells(colCLR).Value)
                    obj1.Own_Asset_Rate = clsCommon.myCdbl(dr(0)("Rate_Own_Asset"))


                    obj1.Commission = 0 ' because nature is always E and it is never C 'clsCommon.myCdbl(dr(0)("Actual_charges"))
                    obj1.Commission_Amount = Math.Round(obj1.AMOUNT * obj1.Commission / 100, 2)
                    obj1.Std_Qty = clsInventoryMovementNew.GetStdQty(Nothing, Math.Round(obj1.ACC_Qty * obj1.FAT / 100, 2), Math.Round(obj1.ACC_Qty * obj1.SNF / 100, 2), objHead.DOC_DATE)

                    If clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
                        If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                            obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission / 100, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                            obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                            obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
                        Else
                            obj1.Emp_Amount = 0
                            'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FAFP") = CompairStringResult.Equal Then
                            obj1.Emp_Amount += clsCommon.myCdbl(dr(0)("EMP_Fixed_Amount"))
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "SWP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                        If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "%(Percentage)") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.AMOUNT >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges2"))
                            Else
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
                            End If
                            obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission / 100, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.ACC_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges2"))
                            Else
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
                            End If
                            obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab5")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges5"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab4")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges4"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab3")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges3"))
                            ElseIf clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) > 0 AndAlso obj1.MILK_Qty >= clsCommon.myCdbl(dr(0)("Actual_charges_Slab2")) Then
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges2"))
                            Else
                                obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
                            End If
                            obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
                        Else
                            obj1.Emp_Amount = 0
                            'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FASWP") = CompairStringResult.Equal Then
                            obj1.Emp_Amount += clsCommon.myCdbl(dr(0)("EMP_Fixed_Amount"))
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("EMP_Type")), "FPSP") = CompairStringResult.Equal Then
                        obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("Actual_charges"))
                        Dim objSPR As clsStandardPrice = clsStandardPrice.GetStandartPrice(obj1.Price_Code, Nothing)
                        If objSPR IsNot Nothing Then
                            If (objSPR.Std_Percent_FAT <> 0 AndAlso objSPR.Std_Percent_SNF <> 0) Then
                                If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Kg") = CompairStringResult.Equal Then
                                    obj1.Emp_Amount = Math.Round((Math.Round(obj1.ACC_Qty * obj1.FAT / 100, 3) * obj1.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(obj1.ACC_Qty * obj1.SNF / 100, 3) * obj1.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Charge_Type")), "Rate/Ltr") = CompairStringResult.Equal Then
                                    Dim conv_fac As Decimal = clsWeightConversionInfo.GetConversion_factor(obj1.UOM, IIf(clsCommon.CompairString(obj1.UOM, "KG") = CompairStringResult.Equal, "LTR", "KG"), Nothing)
                                    Dim qty As Decimal = obj1.ACC_Qty
                                    If conv_fac <> 0 Then
                                        qty = obj1.ACC_Qty / conv_fac
                                    End If
                                    obj1.Emp_Amount = Math.Round((Math.Round(qty * obj1.FAT / 100, 3) * obj1.Payment_Commission * objSPR.Weightage_FAT / objSPR.Std_Percent_FAT) + (Math.Round(qty * obj1.SNF / 100, 3) * obj1.Payment_Commission * objSPR.Weightage_SNF / objSPR.Std_Percent_SNF), 2)
                                Else
                                    obj1.Emp_Amount = 0
                                    'Throw New Exception("EMP Service Basis is Not valid of VSP " + objHead.VSP_CODE)
                                End If
                            End If
                        End If
                    Else
                        Throw New Exception("EMP Type is Not a valid ")
                    End If

                    Dim strMilkType As String = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                    If objCommonVar.DisplayTypeInMilkReceipt Then
                        strMilkType = clsCommon.myCstr(grow.Cells(ColType).Value)
                    End If
                    If clsCommon.CompairString(strMilkType, "C") = CompairStringResult.Equal Then
                        obj1.TIP_Amount = Math.Round(clsCommon.myCdbl(dr(0)("TIP_Cow")) * (obj1.FAT + obj1.SNF) * obj1.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                    ElseIf clsCommon.CompairString(strMilkType, "B") = CompairStringResult.Equal Then
                        obj1.TIP_Amount = Math.Round(clsCommon.myCdbl(dr(0)("TIP_Buffalo")) * obj1.FAT * obj1.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                    Else
                        obj1.TIP_Amount = Math.Round(clsCommon.myCdbl(dr(0)("TIP_Mix")) * obj1.FAT * obj1.ACC_Qty / 100, 2, MidpointRounding.AwayFromZero)
                    End If
                    obj1.Service_Charge_Type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))

                    '==================Head Load==========================
                    Dim MinimumQtyForHeadLoad As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.MinimumQtyForHeadLoad, clsFixedParameterCode.MinimumQtyForHeadLoad, Nothing))
                    Dim dclDistanceKM As Decimal = clsCommon.myCdbl(dr(0)("DistanceKM_Head_Load"))
                    If dclDistanceKM = 0 Then
                        dclDistanceKM = 1
                    End If

                    Dim objHeadLoad As New clsHeadLoadDCS()
                    objHeadLoad = clsHeadLoadDCS.GetDcsData(objHead.VLC_CODE, clsCommon.myCDate(dtpDocDate.Value), trans)
                    obj1.Head_Load_Rate = clsCommon.myCdbl(objHeadLoad.Head_Load_Rate)
                    If clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "K") = CompairStringResult.Equal Then
                        If obj1.ACC_Qty >= MinimumQtyForHeadLoad Then
                            obj1.Head_Load_Amount = Math.Round(obj1.ACC_Qty * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                        End If
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(objHeadLoad.Head_Load_Basis), "L") = CompairStringResult.Equal Then
                        If clsCommon.myCDecimal(dr(0)("ACC_WEIGHT_LTR")) >= MinimumQtyForHeadLoad Then
                            obj1.Head_Load_Amount = Math.Round(clsCommon.myCDecimal(dr(0)("ACC_WEIGHT_LTR")) * objHeadLoad.Head_Load_Rate * dclDistanceKM, 2)
                        End If
                        'ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Head_Load")), "W") = CompairStringResult.Equal Then
                        '    Dim qry As String = "select Ratio,SNF_Ratio,FAT_Pers,SNF_Pers from TSPL_MILK_PRICE_MASTER where Price_Code=(select top 1 Price_Code from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + obj1.Price_Code + "')"
                        '    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                        '    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        '        obj1.FAT_KG = Math.Round(obj1.ACC_Qty * obj1.FAT / 100, 2)
                        '        obj1.SNF_KG = Math.Round(obj1.ACC_Qty * obj1.SNF / 100, 2)
                        '        Dim dblFATRate As Decimal = obj1.Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("FAT_Pers"))
                        '        Dim dblSNFRate As Decimal = obj1.Head_Load_Rate * clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Ratio")) / clsCommon.myCdbl(dtTemp.Rows(0)("SNF_Pers"))
                        '        obj1.Head_Load_Amount = Math.Round(((obj1.FAT_KG * dblFATRate) + (obj1.SNF_KG * dblSNFRate)) * dclDistanceKM, 2)
                        '    End If
                    End If
                    obj1.Head_Load_Type = clsCommon.myCstr(objHeadLoad.Head_Load_Basis)
                    '============================================
                    '==================Own Asset==========================
                    If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                        obj1.Own_Asset_Amount = Math.Round(obj1.ACC_Qty * obj1.Own_Asset_Rate, 2)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                        obj1.Own_Asset_Amount = Math.Round(obj1.MILK_Qty * obj1.Own_Asset_Rate, 2)
                    End If
                    obj1.Own_Asset_Type = clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset"))
                    '============================================
                    obj1.Service_Charge_Amount = Math.Round(obj1.MILK_Qty * clsCommon.myCdbl(dr(0)("Service_Charge_Per_Unit")), 2)
                    obj1.COMM_PORT = clsCommon.myCstr(cboComPort.SelectedValue)
                    obj1.NET_AMOUNT = Math.Round(obj1.AMOUNT + obj1.Emp_Amount + obj1.TIP_Amount - obj1.Service_Charge_Amount, 2)
                    If IsRoundOffPaiseAmount Then
                        obj1.Round_Off = (obj1.NET_AMOUNT Mod 1)
                        obj1.NET_AMOUNT = obj1.NET_AMOUNT - (obj1.NET_AMOUNT Mod 1)
                    End If
                    objList.Add(obj1)

                    '============VSp Charge Detail=====================
                    For Each row_VSP_Charge As DataRow In DtVSPChargeDetail.Select("Vsp_Code='" & objHead.VSP_CODE & "'")
                        objVSP_Charge1 = New clsMilkSRNVSpChargeDetail()
                        objVSP_Charge1.Vsp_Code = clsCommon.myCstr(objHead.VSP_CODE)
                        objVSP_Charge1.Vlc_Doc_Code = clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)
                        objVSP_Charge1.Charge_Code = clsCommon.myCstr(row_VSP_Charge("Charge_Code"))
                        objVSP_Charge1.Charge_Rate = clsCommon.myCstr(row_VSP_Charge("Rate"))
                        objVSP_Charge1.Service_Type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
                        If clsCommon.CompairString(objVSP_Charge1.Service_Type, "%(Percentage)") = CompairStringResult.Equal Then
                            objVSP_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objVSP_Charge1.Charge_Rate / 100, 2)
                        ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Kg") = CompairStringResult.Equal Then
                            objVSP_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objVSP_Charge1.Charge_Rate, 2)
                        ElseIf clsCommon.CompairString(objVSP_Charge1.Service_Type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dr(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                            objVSP_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objVSP_Charge1.Charge_Rate, 2)
                        End If
                        objVSPChargeList.Add(objVSP_Charge1)
                    Next
                    '===========================================


                    '============Price Charge Detail=====================
                    For Each row_Price_Charge As DataRow In DtPriceChargeDetail.Select("Price_Code='" & obj1.Price_Code & "'")
                        objPrice_Charge1 = New clsMilkSRNPriceChargeDetail()
                        objPrice_Charge1.Price_Code = clsCommon.myCstr(obj1.Price_Code)
                        objPrice_Charge1.Vlc_Doc_Code = clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)
                        objPrice_Charge1.Charge_Code = clsCommon.myCstr(row_Price_Charge("Charge_Code"))
                        objPrice_Charge1.Charge_Rate = clsCommon.myCstr(row_Price_Charge("Rate"))
                        objPrice_Charge1.Service_type = clsCommon.myCstr(dr(0)("Service_Charge_Type"))
                        If clsCommon.CompairString(objPrice_Charge1.Service_type, "%(Percentage)") = CompairStringResult.Equal Then
                            objPrice_Charge1.AMOUNT = Math.Round(obj1.AMOUNT * objPrice_Charge1.Charge_Rate / 100, 2)
                        ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Kg") = CompairStringResult.Equal Then
                            objPrice_Charge1.AMOUNT = Math.Round(obj1.ACC_Qty * objPrice_Charge1.Charge_Rate, 2)
                        ElseIf clsCommon.CompairString(objPrice_Charge1.Service_type, "Rate/Ltr") = CompairStringResult.Equal And clsCommon.CompairString(dr(0)("UOM_Code"), "LTR") = CompairStringResult.Equal Then
                            objPrice_Charge1.AMOUNT = Math.Round(obj1.MILK_Qty * objPrice_Charge1.Charge_Rate, 2)
                        End If
                        objPriceChargeList.Add(objPrice_Charge1)
                    Next
                    '===========================================

                    If Not clsMilkSRNMCC.SaveData(objHead, objList, objVSPChargeList, objPriceChargeList) Then
                        Return False
                    Else
                        objHead = Nothing
                        objList = Nothing
                        obj1 = Nothing
                        objVSPChargeList = Nothing
                        objVSP_Charge1 = Nothing
                        objPriceChargeList = Nothing
                        objPrice_Charge1 = Nothing
                        clsCommon.ProgressBarUpdate("SRN Created : " & counter & "/" & gv.Rows.Count)
                    End If
                End If
            Next
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Function AllowToSave(ByVal GGGrid As RadGridView, ByVal IsPostData As Boolean) As Boolean
        Try
            If AllowFutureDateTransaction(dtpDocDate.Value, Nothing) = False Then
                dtpDocDate.Select()
                Return False
            End If
            If btnsave.Text = "Update" Then
                Dim strchk As String = "select POSTED from TSPL_MILK_Sample_HEAD where DOC_COde='" + txtCode.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    Throw New Exception("Transection already posted")
                End If
            End If
            Dim totqty As Double = 0
            Dim totsnf As Double = 0
            Dim totfat As Double = 0
            Dim vlc_arr As String = ""
            For Each grow As GridViewRowInfo In GGGrid.Rows
                totqty += clsCommon.myCdbl(grow.Cells(ColQty).Value)
                totsnf += clsCommon.myCdbl(grow.Cells(colSNF).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                totfat += clsCommon.myCdbl(grow.Cells(colFAT).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                If IsPostData Then
                    If clsCommon.myCdbl(grow.Cells(ColRATE).Value) <= 0 Then
                        Dim vlc_Name As String = clsfrmVLCMaster.getVLcNameOnVLcCode(clsCommon.myCstr(grow.Cells(ColVlcCode).Value), Nothing)
                        vlc_arr = IIf(vlc_arr = "", clsCommon.myCstr(vlc_Name), vlc_arr & "," & clsCommon.myCstr(vlc_Name))
                    End If
                End If
            Next
            txtTotalQty.Text = totqty
            txttotSNF.Text = totsnf
            txttotFAT.Text = totfat
            If clsCommon.myLen(Me.cboShift.Text) <= 0 Then
                Throw New Exception("Please Enter Shift")
            End If
            If clsCommon.myLen(Me.txtMilkReceiptNo.Value) <= 0 Then
                Throw New Exception("Please Enter MCC")
            End If
            If IsPostData And clsCommon.myLen(vlc_arr) > 0 Then
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "VLC having Zero Price. " & Environment.NewLine + vlc_arr, Me.Text)
                clsCommon.ProgressBarShow()
            End If
            UcCustomFields1.AllowToSave()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Sub AddNew()
        txtCode.Value = ""
        txtMilkReceiptNo.Enabled = True
        isCellValueChangedOpen = False
        btnsave.Text = "Save"
        dtpDocDate.MinDate = "01-Jan-2001"
        gv1.DataSource = Nothing
        gv2.DataSource = Nothing
        btnsave.Enabled = True
        BtnPost.Enabled = True
        btndelete.Enabled = True
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        dtpDocDate.Value = clsCommon.GETSERVERDATE()
        cboShift.SelectedIndex = -1
        txtMilkReceiptNo.Value = Nothing
        txtTotalQty.Text = Nothing
        txttotFAT.Text = Nothing
        txttotSNF.Text = Nothing
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        UcAttachment1.BlankAllControls()
        ''End of For Custom Fields
        objSr.SetPortNameValues(cboComPort)
        objSr.SetPortNameValues(CboComport2)
        LoadColumnReceiptDT()
        Load_Receipt_From_Dt(gv1, True)
        Load_Receipt_From_Dt(gv2, True)
        txtRangeFrom.ReadOnly = False
        txtRangeFrom.Text = 0
        TxtRangeTo.Text = 0
        cboECOPro.SelectedValue = 0

        GrpEcoPro2.Enabled = False
        CboEcoPro2.SelectedValue = 0
        BtnStart2.Text = "Start"
        clsPortSetting.GetMachineType(CboMachine)
        clsPortSetting.GetMachineType(CboMachine2)
    End Sub

    Public Sub GetshiftType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            Dim result As Boolean = SaveData(gv1, False)
            If IsShowTwoGrid Then
                result = result And SaveData(gv2, False)
            End If
            If result Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Load_Receipt_From_Dt(gv1, True)
        If IsShowTwoGrid Then
            Load_Receipt_From_Dt(gv2, True)
        End If

        Try
            objSr.ClosePort()
        Catch ex As Exception
        End Try
        Try
            objSr2.ClosePort()
        Catch ex As Exception
        End Try
        Me.Close()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsMilkSampleMCC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData(gv1, False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso BtnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='USERPWD' and TYPE='PWD'")
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = "USERPWD"
            pwd.strType = "PWD"
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                DeleteData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.U Then
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal And UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='USERPWD' and TYPE='PWD'")
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = "USERPWD"
                pwd.strType = "PWD"
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    SaveSRNData(gv1)
                    If IsShowTwoGrid Then
                        SaveSRNData(gv2)
                    End If
                End If
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.KeyCode = Keys.F8 Then
            If clsCommon.myCdbl(txtRangeFrom.Text) > 0 And clsCommon.myCdbl(TxtRangeTo.Text) > 0 Then
                If gv1.Rows.Count > 0 Then
                    Load_Receipt_From_Dt(gv1, True)
                End If
                Load_Receipt_From_Dt(gv1, False)
                irowIndex_Global_gv1 = -1
                txtRangeFrom.ReadOnly = False
                TxtRangeTo.ReadOnly = True
                If IsShowTwoGrid Then
                    If gv2.Rows.Count > 0 Then
                        Load_Receipt_From_Dt(gv2, True)
                    End If
                    Load_Receipt_From_Dt(gv2, False)
                    irowIndex_Global_gv2 = -1
                End If
            End If
        ElseIf e.KeyCode = Keys.F2 Then
            If gv1.Rows.Count > 0 Then
                If irowIndex_Global_gv1 < 0 And (clsCommon.myCdbl(gv1.Rows(0).Cells(colFAT).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(0).Cells(colSNF).Value) > 0) Then
                    For Each row As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Or clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Then
                            irowIndex_Global_gv1 += 1
                        Else
                            Exit For
                        End If
                    Next
                End If
                If gv1.Rows.Count > irowIndex_Global_gv1 Then
                    If irowIndex_Global_gv1 >= 0 Then
                        If clsCommon.myCdbl(LblFATPanel1.Text) = clsCommon.myCdbl(gv1.Rows(irowIndex_Global_gv1).Cells(colFAT).Value) And GetSNFReading(clsCommon.myCdbl(LblSNFPanel1.Text)) = clsCommon.myCdbl(gv1.Rows(irowIndex_Global_gv1).Cells(colSNF).Value) Then
                            If isStopForRepeatedFATSNF Then
                                Exit Sub
                            ElseIf clsCommon.MyMessageBoxShow(Me, "Previous Row have the same reading . Are you sure to Update this in this row ?", "Message", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                    End If
                    If settMaxFATPerLimit > 0 Then
                        If clsCommon.myCdbl(LblFATPanel1.Text) > settMaxFATPerLimit Then
                            clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit), Me.Text, MessageBoxButtons.OK, WinControls.RadMessageIcon.Error)
                            Exit Sub
                        End If
                    End If
                    If settMaxSNFPerLimit > 0 Then
                        If clsCommon.myCdbl(LblSNFPanel1.Text) > settMaxSNFPerLimit Then
                            clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit), Me.Text, MessageBoxButtons.OK, WinControls.RadMessageIcon.Error)
                            Exit Sub
                        End If
                    End If
                    Dim objVSP As clsVendorMaster = clsVendorMaster.GetCorrectionVSP(clsCommon.myCstr(gv1.Rows(irowIndex_Global_gv1 + 1).Cells(ColVspCode).Value), Nothing)
                    Dim TempIndx1 As Integer = irowIndex_Global_gv1 + 1
                    gv1.Rows(TempIndx1).Cells(colFATCorrection).Value = objVSP.CorrectionFat
                    gv1.Rows(TempIndx1).Cells(colSNFCorrection).Value = objVSP.CorrectionSNF
                    gv1.Rows(TempIndx1).Cells(colFATOrg).Value = clsCommon.myCdbl(LblFATPanel1.Text)
                    gv1.Rows(TempIndx1).Cells(colSNFOrg).Value = GetSNFReading(clsCommon.myCdbl(LblSNFPanel1.Text))
                    gv1.Rows(TempIndx1).Cells(colFAT).Value = clsCommon.myCdbl(LblFATPanel1.Text) + objVSP.CorrectionFat
                    gv1.Rows(TempIndx1).Cells(colSNF).Value = GetSNFReading(clsCommon.myCdbl(LblSNFPanel1.Text)) + objVSP.CorrectionSNF
                    gv1.Rows(TempIndx1).Cells(COL_IS_MANUAL).Value = "Auto"
                    LblSNFPanel1.Text = "00.00"
                    LblFATPanel1.Text = "00.00"
                    If irowIndex_Global_gv1 <> clsCommon.myCdbl(gv1.Rows.Count - 1) Then
                        If clsCommon.myCdbl(gv1.Rows(TempIndx1).Cells(colFAT).Value) > 0 And clsCommon.myCdbl(gv1.Rows(TempIndx1).Cells(colSNF).Value) > 0 Then
                            SaveData_Record(gv1)
                        End If
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.F9 Then
            If gv2.Rows.Count > 0 Then
                If gv2.Rows.Count > 0 Then
                    If irowIndex_Global_gv2 < 0 And (clsCommon.myCdbl(gv2.Rows(0).Cells(colFAT).Value) > 0 Or clsCommon.myCdbl(gv2.Rows(0).Cells(colSNF).Value) > 0) Then
                        For Each row As GridViewRowInfo In gv2.Rows
                            If clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Or clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Then
                                irowIndex_Global_gv2 += 1
                            Else
                                Exit For
                            End If
                        Next
                    End If
                    If gv2.Rows.Count > irowIndex_Global_gv2 Then
                        If irowIndex_Global_gv2 >= 0 Then
                            If clsCommon.myCdbl(LblFATPanel2.Text) = clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2).Cells(colFAT).Value) And GetSNFReading(clsCommon.myCdbl(LblSNFPanel2.Text)) = clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2).Cells(colSNF).Value) Then
                                If isStopForRepeatedFATSNF Then
                                    Exit Sub
                                ElseIf clsCommon.MyMessageBoxShow(Me, "Previous Row have the same reading . Are you sure to Update this in this row ?", "Message", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                                    Exit Sub
                                End If
                            End If
                        End If
                        If settMaxFATPerLimit > 0 Then
                            If clsCommon.myCdbl(LblFATPanel2.Text) > settMaxFATPerLimit Then
                                clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit), Me.Text, MessageBoxButtons.OK, WinControls.RadMessageIcon.Error)
                                Exit Sub
                            End If
                        End If
                        If settMaxSNFPerLimit > 0 Then
                            If clsCommon.myCdbl(LblSNFPanel2.Text) > settMaxSNFPerLimit Then
                                clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit), Me.Text, MessageBoxButtons.OK, WinControls.RadMessageIcon.Error)
                                Exit Sub
                            End If
                        End If
                        Dim objVSP As clsVendorMaster = clsVendorMaster.GetCorrectionVSP(clsCommon.myCstr(gv2.Rows(irowIndex_Global_gv2 + 1).Cells(ColVspCode).Value), Nothing)
                        Dim TempIndx2 As Integer = irowIndex_Global_gv2 + 1
                        gv2.Rows(TempIndx2).Cells(colFATCorrection).Value = objVSP.CorrectionFat
                        gv2.Rows(TempIndx2).Cells(colSNFCorrection).Value = objVSP.CorrectionSNF
                        gv2.Rows(TempIndx2).Cells(colFATOrg).Value = clsCommon.myCdbl(LblFATPanel2.Text)
                        gv2.Rows(TempIndx2).Cells(colSNFOrg).Value = GetSNFReading(clsCommon.myCdbl(LblSNFPanel2.Text))
                        gv2.Rows(TempIndx2).Cells(colFAT).Value = clsCommon.myCdbl(LblFATPanel2.Text) + objVSP.CorrectionFat
                        gv2.Rows(TempIndx2).Cells(colSNF).Value = GetSNFReading(clsCommon.myCdbl(LblSNFPanel2.Text)) + objVSP.CorrectionSNF
                        gv2.Rows(TempIndx2).Cells(COL_IS_MANUAL).Value = "Auto"
                        LblFATPanel2.Text = "00.00"
                        LblSNFPanel2.Text = "00.00"
                        If irowIndex_Global_gv2 <> clsCommon.myCdbl(gv2.Rows.Count - 1) Then
                            If clsCommon.myCdbl(gv2.Rows(TempIndx2).Cells(colFAT).Value) > 0 And clsCommon.myCdbl(gv2.Rows(TempIndx2).Cells(colSNF).Value) > 0 Then
                                SaveData_Record(gv2)
                            End If
                        End If
                    End If
                End If
            End If
        ElseIf e.Control And e.KeyCode = Keys.R Then
            txtRangeFrom.ReadOnly = False
            TxtRangeTo.ReadOnly = False
        ElseIf e.Control And e.KeyCode = Keys.D Then
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal And UsLock1.Status = ERPTransactionStatus.Pending Then
                If clsCommon.MyMessageBoxShow(Me, "Do You want to Delete Selected Row Data.", "Delete Row", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='USERPWD' and TYPE='PWD'")
                    Dim pwd As New FrmPWD(Nothing)
                    pwd.strCode = "USERPWD"
                    pwd.strType = "PWD"
                    pwd.ShowDialog()
                    If pwd.isPasswordCorrect Then
                        Dim sampleNo As Integer = clsCommon.myCdbl(gv1.CurrentRow.Cells(colSRNo).Value)
                        Dim sQuery As String = "delete from TSPL_MILK_SAMPLE_DETAIL where SAMPLE_NO=" & sampleNo & " and DOC_CODE='" & txtCode.Value & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery)
                        sQuery = "update TSPL_MILK_RECEIPT_DETAIL set IS_SAMPLEED='F' where DOC_CODE='" & txtMilkReceiptNo.Value & "' and SAMPLE_NO=" & sampleNo & ""
                        clsDBFuncationality.ExecuteNonQuery(sQuery)
                    End If
                End If
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                     "========Table Name=========" + Environment.NewLine +
                                     "TSPL_MILK_SAMPLE_HEAD" + Environment.NewLine +
                                     "TSPL_MILK_SAMPLE_DETAIL" + Environment.NewLine +
                                     "tspl_Milk_receipt_Detail" + Environment.NewLine +
                                     "TSPL_MILK_SAMPLE_Detail_History" + Environment.NewLine +
                                     "TSPL_CUSTOM_FIELD_VALUES" + Environment.NewLine +
                                     "=========Setting Name======" + Environment.NewLine +
                                     "AllowPurchaseAccounting" + Environment.NewLine +
                                     "PickPendingMilkSRNinNextPaymentCycle" + Environment.NewLine +
                                     "StopForRepeatedFATSNF" + Environment.NewLine +
                                     "SampleFONTSize" + Environment.NewLine +
                                     "AddValidationofMilkTypeinsample" + Environment.NewLine +
                                     "RoundOffPaiseAmount" + Environment.NewLine +
                                     "MilkSamplShowOddEvenTwoGrid")

        End If



    End Sub

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal strMilkReceiptcode As String = "", Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            Dim objR As New Random
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            txtMilkReceiptNo.Enabled = False
            If clsCommon.myLen(strMilkReceiptcode) <= 0 Then
                txtRangeFrom.Text = Nothing
                TxtRangeTo.Text = Nothing
                Dim obj As clsMilkSampleMCC = clsMilkSampleMCC.GetData(txtCode.Value, navType, trans)
                txtCode.Value = obj.DOC_CODE
                dtpDocDate.Value = obj.DOC_DATE
                btnsave.Text = "Update"
                txtMilkReceiptNo.Value = obj.MILK_RECEIPT_CODE
                txtMilkReceiptNo.Tag = obj.MCC_CODE
                cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type
                CboMachine.SelectedValue = clsMccMaster.DefaultSampleMachine(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
                cboComPort.SelectedValue = clsMccMaster.DefaultSampleComport(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
                txtTotalQty.Text = obj.TOTAL_QTY
                txttotFAT.Text = obj.TOTAL_FAT
                txttotSNF.Text = obj.TOTAL_SNF
                cboShift.SelectedValue = obj.SHIFT
                UsLock1.Status = obj.POSTED
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                End If
                LoadBlankGrid(gv1)
                LoadBlankGrid(gv2)
                If (clsMilkSampleMCC.ObjList IsNot Nothing AndAlso clsMilkSampleMCC.ObjList.Count > 0) Then
                    isInsideLoadData = True
                    For Each obj1 As clsMilkSampleMCCDetail In clsMilkSampleMCC.ObjList
                        If IsShowTwoGrid AndAlso (obj1.SAMPLE_NO Mod 2) = 0 Then
                            gv2.Rows.AddNew()
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colSRNo).Value = obj1.SAMPLE_NO
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColVlcDocCode).Value = obj1.VLC_DOC_CODE
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColVlcCode).Value = obj1.VLC_CODE
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColSampleNo).Value = obj1.sample_No_Value
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colMilkType).Value = obj1.MILK_TYPE
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColType).Value = obj1.TYPE
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColVspCode).Value = obj1.VSP_CODE
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColItemCode).Value = obj1.ITEm_CODE
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColQty).Value = obj1.MILK_Qty
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColMILK_Qty).Value = obj1.ACC_Qty
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colFAT).Value = obj1.FAT
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colSNF).Value = obj1.SNF

                            '=======Added For Save History=======================
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colPrevFAT).Value = obj1.FAT
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colPrevSNF).Value = obj1.SNF
                            If clsCommon.myCdbl(obj1.FAT) > 0 And clsCommon.myCdbl(obj1.SNF) > 0 Then
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colFAT).ReadOnly = True
                                gv2.Rows(gv2.Rows.Count - 1).Cells(colSNF).ReadOnly = True
                            End If
                            '========================================
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colCOR).Value = obj1.Correction_Factor
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColRATE).Value = obj1.RATE
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColPriceCode).Value = obj1.Price_Code
                            gv2.Rows(gv2.Rows.Count - 1).Cells(COLAMOUNT).Value = obj1.AMOUNT
                            gv2.Rows(gv2.Rows.Count - 1).Cells(COL_IS_MANUAL).Value = obj1.Is_Entered_Manualy
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColUomCode).Value = obj1.UOM_Code
                            gv2.Rows(gv2.Rows.Count - 1).Cells(ColPriceCode).Value = obj1.Price_Code
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.SRN_CODE

                            gv2.Rows(gv2.Rows.Count - 1).Cells(colFATOrg).Value = obj1.FAT_ORG
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colSNFOrg).Value = obj1.SNF_ORG
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colFATCorrection).Value = obj1.FAT_CORRECTION
                            gv2.Rows(gv2.Rows.Count - 1).Cells(colSNFCorrection).Value = obj1.SNF_CORRECTION
                            CboEcoPro2.SelectedValue = IIf(obj1.Eco_Pro_Name = "", "0", obj1.Eco_Pro_Name)
                        Else
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNo).Value = obj1.SAMPLE_NO
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColVlcDocCode).Value = obj1.VLC_DOC_CODE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColVlcCode).Value = obj1.VLC_CODE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColSampleNo).Value = obj1.sample_No_Value
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkType).Value = obj1.MILK_TYPE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColType).Value = obj1.TYPE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColVspCode).Value = obj1.VSP_CODE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColItemCode).Value = obj1.ITEm_CODE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColQty).Value = obj1.MILK_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColMILK_Qty).Value = obj1.ACC_Qty
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = obj1.FAT
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = obj1.SNF

                            '=======Added For Save History=======================
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPrevFAT).Value = obj1.FAT
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPrevSNF).Value = obj1.SNF
                            If clsCommon.myCdbl(obj1.FAT) > 0 And clsCommon.myCdbl(obj1.SNF) > 0 Then
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).ReadOnly = True
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).ReadOnly = True
                            End If
                            '========================================
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCOR).Value = obj1.Correction_Factor
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColRATE).Value = obj1.RATE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColPriceCode).Value = obj1.Price_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(COLAMOUNT).Value = obj1.AMOUNT
                            gv1.Rows(gv1.Rows.Count - 1).Cells(COL_IS_MANUAL).Value = obj1.Is_Entered_Manualy
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColUomCode).Value = obj1.UOM_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColPriceCode).Value = obj1.Price_Code
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.SRN_CODE
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATOrg).Value = obj1.FAT_ORG
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFOrg).Value = obj1.SNF_ORG
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFATCorrection).Value = obj1.FAT_CORRECTION
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNFCorrection).Value = obj1.SNF_CORRECTION
                            cboECOPro.SelectedValue = IIf(obj1.Eco_Pro_Name = "", "0", obj1.Eco_Pro_Name)
                        End If
                    Next
                    isInsideLoadData = False
                    UcAttachment1.LoadData(obj.DOC_CODE)
                    If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        UcCustomFields1.BlankAllControls()
                        UcCustomFields1.LoadData(obj.DOC_CODE)
                    End If
                Else
                    gv1.Rows.AddNew()
                End If
                gv1.CurrentRow = gv1.Rows(0)
            Else
                Dim obj As clsMilkReceiptMCC = clsMilkReceiptMCC.GetData(strMilkReceiptcode, NavigatorType.Current, trans, True)
                LoadColumnReceiptDT()
                txtMilkReceiptNo.Tag = obj.MCC_CODE
                txtTotalQty.Text = 0
                txttotFAT.Text = 0
                txttotSNF.Text = 0
                cboShift.SelectedValue = obj.SHIFT
                dtpDocDate.MinDate = obj.DOC_DATE
                dtpDocDate.Value = obj.DOC_DATE
                cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type
                If (clsMilkReceiptMCC.ObjList IsNot Nothing AndAlso clsMilkReceiptMCC.ObjList.Count > 0) Then
                    For Each obj1 As clsMilkReceiptMCCDetail In clsMilkReceiptMCC.ObjList
                        DtMilkReceiptEcoProWise.Rows.Add()
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colSRNo) = obj1.SAMPLE_NO
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColVlcDocCode) = obj1.VLC_DOC_CODE
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColVlcCode) = obj1.VLC_CODE
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColSampleNo) = obj1.SAMPLE_NO_VALUES
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colMilkType) = obj1.MILK_TYPE
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColType) = obj1.TYPE
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColVspCode) = obj1.VSP_CODE
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColItemCode) = obj1.Item_CODE
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColQty) = obj1.MILK_WEIGHT
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColMILK_Qty) = obj1.ACC_WEIGHT
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colFAT) = obj1.FAT
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colSNF) = obj1.SNF
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColRATE) = obj1.RATE
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(COLAMOUNT) = obj1.Amount
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colECOPro) = "" 'obj1.Eco_Pro_Name
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColUomCode) = obj1.UOM_Code
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colCOR) = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(COL_IS_MANUAL) = obj1.IS_ENTERED_MANUAL

                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colSRN_CODE) = clsBulkMilkSRN.GetSRNNoBySampleNo(txtCode.Value, obj1.SAMPLE_NO)
                        DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColPriceCode) = clsBulkMilkSRN.GetPriceCodeBySampleNo(txtCode.Value, obj1.SAMPLE_NO)
                    Next
                Else
                    gv1.Rows.AddNew()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadColumnReceiptDT()
        DtMilkReceiptEcoProWise = New DataTable
        DtMilkReceiptEcoProWise.Columns.Add(colSRNo, GetType(Integer))
        DtMilkReceiptEcoProWise.Columns.Add(ColVlcDocCode)
        DtMilkReceiptEcoProWise.Columns.Add(ColSampleNo)
        DtMilkReceiptEcoProWise.Columns.Add(colMilkType)
        DtMilkReceiptEcoProWise.Columns.Add(ColType)
        DtMilkReceiptEcoProWise.Columns.Add(ColVspCode)
        DtMilkReceiptEcoProWise.Columns.Add(ColItemCode)
        DtMilkReceiptEcoProWise.Columns.Add(ColQty)
        DtMilkReceiptEcoProWise.Columns.Add(ColMILK_Qty)
        DtMilkReceiptEcoProWise.Columns.Add(colFAT)
        DtMilkReceiptEcoProWise.Columns.Add(colSNF)
        DtMilkReceiptEcoProWise.Columns.Add(colECOPro)
        DtMilkReceiptEcoProWise.Columns.Add(ColUomCode)
        DtMilkReceiptEcoProWise.Columns.Add(colCOR)
        DtMilkReceiptEcoProWise.Columns.Add(ColRATE)
        DtMilkReceiptEcoProWise.Columns.Add(COLAMOUNT)
        DtMilkReceiptEcoProWise.Columns.Add(ColVlcCode)

        DtMilkReceiptEcoProWise.Columns.Add(colFATOrg)
        DtMilkReceiptEcoProWise.Columns.Add(colSNFOrg)
        DtMilkReceiptEcoProWise.Columns.Add(colFATCorrection)
        DtMilkReceiptEcoProWise.Columns.Add(colSNFCorrection)

        DtMilkReceiptEcoProWise.Columns.Add(COL_IS_MANUAL)

        DtMilkReceiptEcoProWise.Columns.Add(colSRN_CODE)
        DtMilkReceiptEcoProWise.Columns.Add(ColPriceCode)
    End Sub

    Sub Load_Receipt_From_Dt(ByVal gv As RadGridView, ByVal IsReset As Boolean)
        Load_Receipt_From_Dt(gv, IsReset, True)
    End Sub
    Sub Load_Receipt_From_Dt(ByVal gv As RadGridView, ByVal IsReset As Boolean, ByVal ShowMsg As Boolean)
        Try
            LoadBlankGrid(gv)
            If (clsCommon.myLen(cboECOPro.SelectedValue) <= 0 Or cboECOPro.SelectedValue = "0") And clsCommon.myLen(txtMilkReceiptNo.Value) <= 0 Then
                If Not IsReset Then
                    If ShowMsg Then
                        clsCommon.MyMessageBoxShow(Me, "Please Select Eco Pro Machine First.", Me.Text)
                    End If
                End If
                Exit Sub
            End If
            If Not IsReset Then LoadData("", txtMilkReceiptNo.Value)
            If DtMilkReceiptEcoProWise.Rows.Count > 0 Then
                Dim sQuery As String = ""
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If IsReset And btnsave.Text = "Save" Then
                    For Each row As DataRow In DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='" & cboECOPro.SelectedValue & "' and " & colSRNo & ">=" & clsCommon.myCdbl(txtRangeFrom.Text) & " and " & colSRNo & "<=" & clsCommon.myCdbl(TxtRangeTo.Text) & "")
                        sQuery = "update tspl_milk_receipt_detail set eco_pro_name=null where eco_pro_name='" & cboECOPro.SelectedValue & "' and DOC_CODE='" & txtMilkReceiptNo.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(row.Item(ColVlcDocCode)) & "' and SAMPLE_NO_VALUES='" & clsCommon.myCstr(row.Item(ColSampleNo)) & "' and coalesce(Is_Sampleed,'F')='F'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                Else
                    isCellValueChangedOpen = True
                    isInsideLoadData = True
                    For Each row As DataRow In DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='' and " & colSRNo & ">=" & clsCommon.myCdbl(txtRangeFrom.Text) & " and " & colSRNo & "<=" & clsCommon.myCdbl(TxtRangeTo.Text) & "", "SRNo")
                        If IsShowTwoGrid Then
                            If gv Is gv1 AndAlso clsCommon.myCdbl(row("" & colSRNo & "")) Mod 2 = 0 Then
                                Continue For
                            ElseIf gv Is gv2 AndAlso clsCommon.myCdbl(row("" & colSRNo & "")) Mod 2 = 1 Then
                                Continue For
                            End If
                        End If

                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSRNo).Value = row("" & colSRNo & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColVlcDocCode).Value = row("" & ColVlcDocCode & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColVlcCode).Value = row("" & ColVlcCode & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColSampleNo).Value = row("" & ColSampleNo & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(colMilkType).Value = row("" & colMilkType & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColType).Value = row("" & ColType & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColVspCode).Value = row("" & ColVspCode & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColItemCode).Value = row("" & ColItemCode & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColQty).Value = row("" & ColQty & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColMILK_Qty).Value = row("" & ColMILK_Qty & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(colFAT).Value = row("" & colFAT & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(colSNF).Value = row("" & colSNF & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(colCOR).Value = row("" & colCOR & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColUomCode).Value = row("" & ColUomCode & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(COL_IS_MANUAL).Value = row("" & COL_IS_MANUAL & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(colCLR).Value = 0
                        gv.Rows(gv.Rows.Count - 1).Cells(ColRATE).Value = IIf(clsCommon.myCdbl(row("" & ColRATE & "")) > 0, clsCommon.myCdbl(row("" & ColRATE & "")), 0)
                        gv.Rows(gv.Rows.Count - 1).Cells(COLAMOUNT).Value = IIf(clsCommon.myCdbl(row("" & COLAMOUNT & "")) > 0, clsCommon.myCdbl(row("" & COLAMOUNT & "")), 0)
                        gv.Rows(gv.Rows.Count - 1).Cells(colSRN_CODE).Value = row("" & colSRN_CODE & "")
                        gv.Rows(gv.Rows.Count - 1).Cells(ColPriceCode).Value = row("" & ColPriceCode & "")
                        If clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colFAT).Value) > 0 And clsCommon.myCdbl(gv.Rows(gv.Rows.Count - 1).Cells(colSNF).Value) > 0 Then
                            gv.Rows(gv.Rows.Count - 1).Cells(colFAT).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(colSNF).ReadOnly = True
                            gv.Rows(gv.Rows.Count - 1).Cells(ColIS_Saved).Value = "T"
                        End If
                        row("" & colECOPro & "") = cboECOPro.SelectedValue
                        sQuery = "update tspl_milk_receipt_detail set eco_pro_name='" & cboECOPro.SelectedValue & "' where eco_pro_name is null and DOC_CODE='" & txtMilkReceiptNo.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(row.Item(ColVlcDocCode)) & "' and SAMPLE_NO_VALUES='" & clsCommon.myCstr(row.Item(ColSampleNo)) & "' and coalesce(Is_Sampleed,'F')='F'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                    isCellValueChangedOpen = False
                    isInsideLoadData = False
                    is_Manual = clsMilkSampleMCC.GetShiftisManual(txtMilkReceiptNo.Tag, trans)
                    If is_Manual Then
                        For Each col As GridViewColumn In gv.Columns
                            If clsCommon.CompairString(col.Name, "ColFAT") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "ColSNF") = CompairStringResult.Equal Then
                                For Each row As GridViewRowInfo In gv.Rows
                                    If clsCommon.myCdbl(row.Cells(col.Name).Value) <= 0 Then
                                        col.ReadOnly = False
                                    End If
                                Next
                            End If
                        Next
                    Else
                        For Each col As GridViewColumn In gv.Columns
                            col.ReadOnly = True
                        Next
                    End If
                    If Not IsReset And gv.Rows.Count <= 0 And DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='' and " & colSRNo & ">='" & clsCommon.myCdbl(txtRangeFrom.Text) & "' and " & colSRNo & "<='" & clsCommon.myCdbl(TxtRangeTo.Text) & "'").Count <= 0 Then
                        If ShowMsg Then
                            clsCommon.MyMessageBoxShow(Me, "No data Found.", Me.Text)
                        End If
                    End If
                End If
                trans.Commit()
                If gv.Rows.Count > 0 Then
                    gv.CurrentRow = gv.Rows(0)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Sub LoadBlankGrid(ByVal GGGrid As RadGridView)
        GGGrid.Rows.Clear()
        GGGrid.Columns.Clear()

        Dim repoNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNo.FormatString = ""
        repoNo.HeaderText = "S No"
        repoNo.Name = colSRNo
        repoNo.Width = 50
        repoNo.ReadOnly = True
        GGGrid.MasterTemplate.Columns.Add(repoNo)

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Vlc Doc Code"
        repoCode.Name = ColVlcDocCode
        repoCode.Width = 0
        repoCode.ReadOnly = True
        repoCode.IsVisible = False
        repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoCode)

        Dim repoVlcCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVlcCode.FormatString = ""
        repoVlcCode.HeaderText = "Vlc Code"
        repoVlcCode.Name = ColVlcCode
        repoVlcCode.Width = 0
        repoVlcCode.ReadOnly = True
        repoVlcCode.IsVisible = False
        repoVlcCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoVlcCode)


        Dim repoSampleNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSampleNo.FormatString = ""
        repoSampleNo.HeaderText = "Sample No"
        repoSampleNo.Name = ColSampleNo
        repoSampleNo.Width = 0
        repoSampleNo.ReadOnly = True
        repoSampleNo.IsVisible = False
        repoSampleNo.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoSampleNo)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = ColItemCode
        repoItemCode.Width = 0
        repoItemCode.ReadOnly = True
        repoItemCode.IsVisible = False
        repoItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoMilkType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMilkType.FormatString = ""
        repoMilkType.HeaderText = "Milk Type"
        repoMilkType.Name = colMilkType
        repoMilkType.Width = 0
        repoMilkType.ReadOnly = True
        repoMilkType.IsVisible = False
        repoMilkType.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoMilkType)

        Dim repoType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Type"
        repoType.Name = ColType
        repoType.Width = 0
        repoType.ReadOnly = True
        repoType.IsVisible = False
        repoType.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoType)


        Dim repoVSPCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVSPCode.FormatString = ""
        repoVSPCode.HeaderText = "VSP Code"
        repoVSPCode.Name = ColVspCode
        repoVSPCode.Width = 0
        repoVSPCode.IsVisible = False
        repoVSPCode.ReadOnly = True
        repoVSPCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoVSPCode)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Qty"
        repoQty.Name = ColQty
        repoQty.Width = 0
        repoQty.ReadOnly = True
        repoQty.IsVisible = False
        GGGrid.MasterTemplate.Columns.Add(repoQty)


        Dim repoACCQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoACCQty.FormatString = ""
        repoACCQty.HeaderText = "Actual Qty(KG)"
        repoACCQty.Name = ColMILK_Qty
        repoACCQty.Width = 0
        repoACCQty.ReadOnly = True
        repoACCQty.IsVisible = False
        GGGrid.MasterTemplate.Columns.Add(repoACCQty)


        Dim repoFAT As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFAT = New GridViewTextBoxColumn()
        repoFAT.FormatString = ""
        repoFAT.HeaderText = "FAT(%)"
        repoFAT.Name = colFAT
        repoFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAT.ReadOnly = False
        repoFAT.Width = 70
        GGGrid.MasterTemplate.Columns.Add(repoFAT)

        Dim repoSNF As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNF = New GridViewTextBoxColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF(%)"
        repoSNF.Name = colSNF
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = False
        repoSNF.Width = 70
        GGGrid.MasterTemplate.Columns.Add(repoSNF)

        Dim repoPrevFAT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPrevFAT = New GridViewDecimalColumn()
        repoPrevFAT.FormatString = ""
        repoPrevFAT.HeaderText = "Prev. FAT"
        repoPrevFAT.Name = colPrevFAT
        repoPrevFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPrevFAT.ReadOnly = True
        repoPrevFAT.IsVisible = False
        repoPrevFAT.DecimalPlaces = 2
        repoPrevFAT.Width = 0
        GGGrid.MasterTemplate.Columns.Add(repoPrevFAT)

        Dim repoPrevSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPrevSNF = New GridViewDecimalColumn()
        repoPrevSNF.FormatString = ""
        repoPrevSNF.HeaderText = "Prev. SNF"
        repoPrevSNF.Name = colPrevSNF
        repoPrevSNF.DecimalPlaces = 2
        repoPrevSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPrevSNF.ReadOnly = True
        repoPrevSNF.IsVisible = False
        repoPrevSNF.Width = 70
        GGGrid.MasterTemplate.Columns.Add(repoPrevSNF)


        Dim repoCor As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCor = New GridViewDecimalColumn()
        repoCor.FormatString = ""
        repoCor.HeaderText = "Correction Factor"
        repoCor.Name = colCOR
        repoCor.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCor.ReadOnly = True
        repoCor.IsVisible = False
        repoCor.Width = 0
        GGGrid.MasterTemplate.Columns.Add(repoCor)


        Dim repoCLR As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCLR = New GridViewDecimalColumn()
        repoCLR.FormatString = ""
        repoCLR.HeaderText = "CLR"
        repoCLR.Name = colCLR
        repoCLR.DecimalPlaces = 2
        repoCLR.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCLR.ReadOnly = True
        repoCLR.IsVisible = False
        repoCLR.Width = 0
        GGGrid.MasterTemplate.Columns.Add(repoCLR)

        Dim repoRATE As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRATE = New GridViewDecimalColumn()
        repoRATE.FormatString = ""
        repoRATE.HeaderText = "Rate"
        repoRATE.Name = ColRATE
        repoRATE.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoRATE.ReadOnly = True
        repoRATE.IsVisible = False
        repoRATE.Width = 0
        GGGrid.MasterTemplate.Columns.Add(repoRATE)


        Dim repoAMOUNT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAMOUNT = New GridViewDecimalColumn()
        repoAMOUNT.FormatString = ""
        repoAMOUNT.HeaderText = "Amount"
        repoAMOUNT.Name = COLAMOUNT
        repoAMOUNT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAMOUNT.ReadOnly = True
        repoAMOUNT.IsVisible = False
        repoAMOUNT.Width = 0
        GGGrid.MasterTemplate.Columns.Add(repoAMOUNT)


        Dim repoManual As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoManual.FormatString = ""
        repoManual.HeaderText = "Entry Type"
        repoManual.Name = COL_IS_MANUAL
        repoManual.Width = 0
        repoManual.IsVisible = False
        repoManual.ReadOnly = True
        repoManual.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoManual)

        Dim repoSRN_CODE As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSRN_CODE.FormatString = ""
        repoSRN_CODE.HeaderText = "SRN_CODE"
        repoSRN_CODE.Name = colSRN_CODE
        repoSRN_CODE.Width = 0
        repoSRN_CODE.ReadOnly = True
        repoSRN_CODE.IsVisible = False
        GGGrid.MasterTemplate.Columns.Add(repoSRN_CODE)

        Dim repoUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUOM.FormatString = ""
        repoUOM.HeaderText = "UOM"
        repoUOM.Name = ColUomCode
        repoUOM.Width = 0
        repoUOM.ReadOnly = True
        repoUOM.IsVisible = False
        repoUOM.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoUOM)

        Dim repoPrice_code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPrice_code.FormatString = ""
        repoPrice_code.HeaderText = "Price Code"
        repoPrice_code.Name = ColPriceCode
        repoPrice_code.Width = 0
        repoPrice_code.ReadOnly = True
        repoPrice_code.IsVisible = False
        repoPrice_code.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoPrice_code)

        Dim repoISSaved As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoISSaved.FormatString = ""
        repoISSaved.HeaderText = "Is Saved"
        repoISSaved.Name = ColIS_Saved
        repoISSaved.Width = 0
        repoISSaved.IsVisible = False
        repoISSaved.ReadOnly = True
        repoISSaved.TextImageRelation = TextImageRelation.TextBeforeImage
        GGGrid.MasterTemplate.Columns.Add(repoISSaved)

        repoFAT = New GridViewTextBoxColumn()
        repoFAT.FormatString = ""
        repoFAT.HeaderText = "FAT ORG"
        repoFAT.Name = colFATOrg
        repoFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAT.ReadOnly = True
        repoFAT.Width = 70
        repoFAT.IsVisible = False
        GGGrid.MasterTemplate.Columns.Add(repoFAT)

        repoSNF = New GridViewTextBoxColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF ORG"
        repoSNF.Name = colSNFOrg
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = True
        repoSNF.Width = 70
        repoSNF.IsVisible = False
        GGGrid.MasterTemplate.Columns.Add(repoSNF)

        repoFAT = New GridViewTextBoxColumn()
        repoFAT.FormatString = ""
        repoFAT.HeaderText = "FAT Correction"
        repoFAT.Name = colFATCorrection
        repoFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoFAT.ReadOnly = True
        repoFAT.Width = 70
        repoFAT.IsVisible = False
        GGGrid.MasterTemplate.Columns.Add(repoFAT)

        repoSNF = New GridViewTextBoxColumn()
        repoSNF.FormatString = ""
        repoSNF.HeaderText = "SNF Correction"
        repoSNF.Name = colSNFCorrection
        repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoSNF.ReadOnly = True
        repoSNF.Width = 70
        repoSNF.IsVisible = False
        GGGrid.MasterTemplate.Columns.Add(repoSNF)

        GGGrid.AllowDeleteRow = False
        GGGrid.AllowAddNewRow = False
        GGGrid.ShowGroupPanel = False
        GGGrid.AllowColumnReorder = True
        GGGrid.AllowRowReorder = False
        GGGrid.EnableSorting = False
        GGGrid.EnableAlternatingRowColor = False
        GGGrid.AutoSizeRows = False
        GGGrid.AllowRowResize = True
        GGGrid.VerticalScrollState = ScrollState.AlwaysShow
        GGGrid.HorizontalScrollState = ScrollState.AlwaysShow
        GGGrid.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GGGrid.MasterTemplate.ShowRowHeaderColumn = False
        GGGrid.TableElement.TableHeaderHeight = 40
        GGGrid.ShowFilteringRow = True


        GGGrid.AllowEditRow = True
        GGGrid.MasterTemplate.AllowCellContextMenu = True
        GGGrid.MasterTemplate.AllowColumnHeaderContextMenu = True
        GGGrid.MasterTemplate.AllowDeleteRow = True

        ReStoreGridLayout(GGGrid)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Try
            Dim qry As String = "SELECT DOC_CODE as Code,DOC_DATE as Date,MCC_CODE as [Mcc Code] FROM TSPL_MILK_SAMPLE_HEAD"
            txtCode.Value = clsCommon.ShowSelectForm("MILK RECEIPT", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
        'TxtRange1Panal2.Text = Nothing
        TxtRangeTo.Text = Nothing
        txtRangeFrom.Text = Nothing
        'Txtrange2panal2.Text = Nothing
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        CellFormatNew(gv1, e)
    End Sub

    Sub CellFormatNew(ByVal GGGrid As RadGridView, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        Try
            e.CellElement.Font = GridFont
            If Not isInsideImportData Then
                If e.Column Is GGGrid.Columns(colFAT) Or e.Column Is GGGrid.Columns(colSNF) Then
                    If TypeOf e.CellElement.RowInfo Is GridViewDataRowInfo Then
                        Dim strFlatWithInvesterDealer As String = clsCommon.myCstr(GGGrid.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                        Dim strFlatCode As String = ""

                        If clsCommon.myLen(strFlatWithInvesterDealer) > 0 AndAlso strFlatWithInvesterDealer.Contains("/") Then
                            Dim intIndex As Integer = strFlatWithInvesterDealer.IndexOf("/")
                            strFlatCode = strFlatWithInvesterDealer.Substring(0, intIndex)
                        Else
                            strFlatCode = strFlatWithInvesterDealer
                        End If

                        e.CellElement.DrawFill = True
                        e.CellElement.GradientStyle = GradientStyles.Solid
                        e.CellElement.ForeColor = Color.Black

                        e.CellElement.BackColor = Color.LightGreen
                        If e.Column Is GGGrid.Columns(colFAT) Or e.Column Is GGGrid.Columns(colSNF) Then
                            If clsCommon.myCstr(e.Row.Cells(COL_IS_MANUAL).Value) = clsCommon.myCstr("Manual") Then
                                e.CellElement.BackColor = Color.Cyan
                            End If
                            If clsCommon.myCstr(e.Row.Cells(COL_IS_MANUAL).Value) = clsCommon.myCstr("Auto") Then
                                e.CellElement.BackColor = Color.LightGreen
                            End If
                        End If
                    Else
                        e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                        e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                        e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                        e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
                    End If
                End If

            End If

            'End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_CellValidating(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles gv1.CellValidating
        CellValidatingNew(gv1, e)
    End Sub

    Sub CellValidatingNew(ByVal GGGrid As RadGridView, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs)
        Try
            If Not isInsideImportData Then
                oldValue = 0.0
                IsCanceled = False
                If (GGGrid.CurrentColumn Is GGGrid.Columns(colFAT) Or GGGrid.CurrentColumn Is GGGrid.Columns(colSNF)) And Not isInsideLoadData Then
                    If e.Value <> e.OldValue And e.OldValue > 0 Then
                        isInsideLoadData = True
                        If clsCommon.myCdbl(GGGrid.Rows.Count - 1) > GGGrid.CurrentRow.Index Then
                            If clsCommon.myCdbl(GGGrid.Rows(GGGrid.CurrentRow.Index + 1).Cells(colFAT).Value) <= 0 And clsCommon.myCdbl(GGGrid.Rows(GGGrid.CurrentRow.Index + 1).Cells(colSNF).Value) <= 0 Then
                                Exit Sub
                            End If
                        End If
                        oldValue = e.OldValue
                        IsCanceled = True
                        isInsideLoadData = False
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        CellValueChangedNew(gv1, e)
    End Sub

    Sub CellValueChangedNew(ByVal GGGrid As RadGridView, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
        Try
            If Not isInsideImportData Then
                If Not isInsideLoadData Then
                    Dim irowIndex As Integer = e.RowIndex
                    If IsCanceled And isCellValueChanged Then
                        isCellValueChanged = False
                        e.Row.Cells(e.Column.Name).Value = oldValue
                        isCellValueChanged = True
                    End If
                    If isCellValueChanged And e.Column Is GGGrid.Columns(colFAT) Then
                        isCellValueChanged = False
                        GGGrid.Rows(irowIndex).Cells(colFAT).Value = Math.Truncate(clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colFAT).Value) * 10) / 10
                        If settMaxFATPerLimit > 0 Then
                            If clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colFAT).Value) > settMaxFATPerLimit Then
                                clsCommon.MyMessageBoxShow(Me, "FAT % Can't be more than " + clsCommon.myCstr(settMaxFATPerLimit), Me.Text, MessageBoxButtons.OK, WinControls.RadMessageIcon.Error)
                                GGGrid.Rows(irowIndex).Cells(colFAT).Value = 0
                            End If
                        End If
                        isCellValueChanged = True
                    End If
                    If isCellValueChanged And e.Column Is GGGrid.Columns(colSNF) Then
                        isCellValueChanged = False
                        GGGrid.Rows(irowIndex).Cells(colSNF).Value = GetSNFReading(Math.Truncate(clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colSNF).Value) * 10) / 10)
                        If settMaxSNFPerLimit > 0 Then
                            If clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colSNF).Value) > settMaxSNFPerLimit Then
                                clsCommon.MyMessageBoxShow(Me, "SNF % Can't be more than " + clsCommon.myCstr(settMaxSNFPerLimit), Me.Text, MessageBoxButtons.OK, WinControls.RadMessageIcon.Error)
                                GGGrid.Rows(irowIndex).Cells(colSNF).Value = 0
                            End If
                        End If
                        isCellValueChanged = True
                    End If

                    isCellValueChanged = True
                    If isCellValueChanged And irowIndex <> 0 And (clsCommon.myCdbl(GGGrid.CurrentRow.Cells(colSNF).Value) <> 0 Or clsCommon.myCdbl(GGGrid.CurrentRow.Cells(colFAT).Value) <> 0) Then
                        If clsCommon.myCdbl(GGGrid.Rows(irowIndex - 1).Cells(colFAT).Value) = 0 Or clsCommon.myCdbl(GGGrid.Rows(irowIndex - 1).Cells(colSNF).Value) = 0 Then
                            clsCommon.MyMessageBoxShow(Me, "You have to Enter FAT ,SNF of Previous Sample Before Entring in This Sample", "Valication")
                            isCellValueChanged = False
                            isInsideLoadData = True
                            GGGrid.Rows(irowIndex).Cells(colFAT).Value = 0
                            GGGrid.Rows(irowIndex).Cells(colSNF).Value = 0
                            isCellValueChanged = True
                            isInsideLoadData = False
                            Exit Sub
                        End If
                    End If
                    If Not isCellValueChangedOpen And IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AddValidationofMilkTypeinsample, clsFixedParameterCode.AddValidationofMilkTypeinsample, Nothing)) = 0, False, True) = True Then
                        isCellValueChangedOpen = True
                        Dim dr() As DataRow = DtMilkTypeRange.Select("Field='" & GGGrid.CurrentRow.Cells(ColType).Value & "'")
                        If dr.Length > 0 Then
                            If e.Column Is GGGrid.Columns(colSNF) Then
                                If clsCommon.myCdbl(dr(0)("SNF(Min)")) > clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colSNF).Value) Or clsCommon.myCdbl(dr(0)("SNF(Max)")) < clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colSNF).Value) Then
                                    clsCommon.MyMessageBoxShow(Me, "Please Enter SNF only from " & clsCommon.myCdbl(dr(0)("SNF(Min)")) & " to " & clsCommon.myCdbl(dr(0)("SNF(Max)")) & "")
                                    GGGrid.Rows(irowIndex).Cells(colSNF).Value = 0
                                End If
                            End If
                            If e.Column Is GGGrid.Columns(colFAT) Then
                                If clsCommon.myCdbl(dr(0)("FAT(Min)")) > clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colFAT).Value) Or clsCommon.myCdbl(dr(0)("FAT(Max)")) < clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colFAT).Value) Then
                                    clsCommon.MyMessageBoxShow(Me, "Please Enter FAT only from " & clsCommon.myCdbl(dr(0)("FAT(Min)")) & " to " & clsCommon.myCdbl(dr(0)("FAT(Max)")) & "")
                                    GGGrid.Rows(irowIndex).Cells(colFAT).Value = 0
                                End If
                            End If
                        End If
                        isCellValueChangedOpen = False
                    End If
                    If Not isCellValueChangedOpen Then
                        isCellValueChangedOpen = True
                        If clsCommon.myCdbl(GGGrid.CurrentRow.Cells(colSNF).Value) <> 0 And clsCommon.myCdbl(GGGrid.CurrentRow.Cells(colFAT).Value) <> 0 Then
                            isInsideLoadData = True
                            If BtnStart.Text = "Stop" Then
                                GGGrid.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                            Else
                                GGGrid.CurrentRow.Cells(COL_IS_MANUAL).Value = "Manual"
                            End If
                            Try
                                GGGrid.CurrentRow = GGGrid.Rows(irowIndex + 1)
                            Catch ex As Exception
                            End Try
                            isInsideLoadData = False
                        End If
                        isCellValueChangedOpen = False
                    End If
                    If isCellValueChanged And irowIndex <> 0 And clsCommon.CompairString(clsCommon.myCstr(e.Row.Cells(COL_IS_MANUAL).Value), clsCommon.myCstr("Auto")) = CompairStringResult.Equal Then
                        If (clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colSNF).Value) <> 0 And clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colFAT).Value) <> 0) Then
                            If clsCommon.myCdbl(GGGrid.Rows.Count - 1) > GGGrid.CurrentRow.Index Then
                                SaveData_Record(GGGrid)
                                isInsideImportData = False
                                Issaved = True
                                GGGrid.Rows(irowIndex).Cells(colFAT).ReadOnly = True
                                GGGrid.Rows(irowIndex).Cells(colSNF).ReadOnly = True
                                If irowIndex <> clsCommon.myCdbl(GGGrid.Rows.Count - 1) Then
                                    GGGrid.CurrentRow = GGGrid.Rows(irowIndex + 1)
                                    GGGrid.CurrentColumn = GGGrid.Columns(colFAT)
                                End If
                            End If
                        End If
                    ElseIf isCellValueChanged And irowIndex <> 0 And clsCommon.CompairString(clsCommon.myCstr(e.Row.Cells(COL_IS_MANUAL).Value), clsCommon.myCstr("Manual")) = CompairStringResult.Equal Then
                        If isCellValueChanged And irowIndex <> 0 And (clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colSNF).Value) <> 0 And clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colFAT).Value) <> 0) Then
                            If clsCommon.myCdbl(GGGrid.Rows.Count - 1) > GGGrid.CurrentRow.Index Then
                                SaveData_Record(GGGrid)
                                isInsideImportData = False
                                Issaved = True
                                GGGrid.Rows(irowIndex).Cells(colFAT).ReadOnly = True
                                GGGrid.Rows(irowIndex).Cells(colSNF).ReadOnly = True
                                If irowIndex <> clsCommon.myCdbl(GGGrid.Rows.Count - 1) Then
                                    GGGrid.CurrentRow = GGGrid.Rows(irowIndex + 1)
                                    GGGrid.CurrentColumn = GGGrid.Columns(colFAT)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub gv1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv1.KeyPress
        Try
            If Not IsNumeric(e.KeyChar) And Not e.KeyChar = "." Then
                e.Handled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyUp
        Try
            If Not IsNumeric(e.KeyCode) And Not e.KeyCode = Keys.Decimal Then
                e.Handled = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do You want to delete this Row Permanently . Are You Sure ?", "Delete Row", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim sQuery As String = "delete from TSPL_MILK_SAMPLE_DETAIL where DOC_Code='" & gv1.CurrentRow.Cells("DOC CODE").Value & "' and VLC_DOC_Code='" & gv1.CurrentRow.Cells("VLC DOC CODE").Value & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery)
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        PostData()
    End Sub

    Sub LoadDt()
        DtMilkReceipt = clsDBFuncationality.GetDataTable("select rd.*,rh.*,Service_Charge_Type,coalesce(Rate_Head_Load,0) as Rate_Head_Load" _
            & " ,coalesce(Rate_Own_Asset,0) as Rate_Own_Asset,Service_Basis_Head_Load,Service_Basis_Own_Asset,TSPL_VENDOR_MASTER.EMP_Type " _
            & " ,TSPL_VENDOR_MASTER.EMP_Fixed_Amount " _
            & " ,TSPL_VENDOR_MASTER.Actual_charges_Slab " _
            & " ,TSPL_VENDOR_MASTER.Actual_charges " _
            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab2" _
            & ",TSPL_VENDOR_MASTER.Actual_charges2" _
            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab3" _
            & ",TSPL_VENDOR_MASTER.Actual_charges3" _
            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab4" _
            & ",TSPL_VENDOR_MASTER.Actual_charges4" _
            & ",TSPL_VENDOR_MASTER.Actual_charges_Slab5" _
            & ",TSPL_VENDOR_MASTER.Actual_charges5,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,TSPL_VENDOR_MASTER.TIP_Buffalo,TSPL_VENDOR_MASTER.TIP_Cow,TSPL_VENDOR_MASTER.TIP_Mix,TSPL_VENDOR_MASTER.DistanceKM_Head_Load from TSPL_MILK_RECEIPT_HEAD rd Inner join TSPL_MILK_RECEIPT_DETAIL rh on rh.DOC_CODE=" _
            & " rd.DOC_CODE left join TSPL_VENDOR_MASTER on   Vendor_Code=VSP_CODE where rd.doc_code='" & txtMilkReceiptNo.Value & "'")
        DtVehicle = clsDBFuncationality.GetDataTable("SELECT vm.* FROM  TSPL_Primary_Vehicle_Master vm ")
        DtVSPChargeDetail = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING ")
        DtPriceChargeDetail = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail ")
    End Sub

    Sub PostData()
        Try

            Dim msg As String = ""

            If Not clsMCCMaterialSale.checkApprovalDocument("M-RECEIPT", txtMilkReceiptNo.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Approval required for this Document Milk Receipt [" & txtMilkReceiptNo.Value & "]")
                Exit Sub
            End If
            Dim qry As String = "select max(TSPL_MILK_RECEIPT_DETAIL.sample_No) as Sample_Receipt_No,max(tspl_milk_sample_detail.sample_No) as Sample_Sample_No from TSPL_MILK_RECEIPT_DETAIL" + Environment.NewLine +
            "left outer join TSPL_MILK_RECEIPT_head on TSPL_MILK_RECEIPT_head.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE" + Environment.NewLine +
            "left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_head.DOC_CODE" + Environment.NewLine +
            "left outer join tspl_milk_sample_detail on tspl_milk_sample_detail.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE" + Environment.NewLine +
            "where TSPL_MILK_SAMPLE_HEAD.doc_code='" + txtCode.Value + "'   "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then ''ERO/18/10/19-001068 by balwinder on 22/11/2019
                If clsCommon.myCdbl(dt.Rows(0)("Sample_Receipt_No")) <> clsCommon.myCdbl(dt.Rows(0)("Sample_Sample_No")) Then
                    clsCommon.MyMessageBoxShow(Me, "Milk Receipt have [" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("Sample_Receipt_No"))) + "] Samples and Milk Sample have [" + clsCommon.myCstr(clsCommon.myCdbl(dt.Rows(0)("Sample_Sample_No"))) + "] Samples.")
                    Exit Sub
                End If
            End If

            If (myMessages.postConfirm()) Then
                SaveData(gv1, True)
                If IsShowTwoGrid Then
                    SaveData(gv2, True)
                End If
                LoadDt()
                If SaveSRNData(gv1) Then
                    If IsShowTwoGrid Then
                        SaveSRNData(gv2)
                    End If
                    If clsMilkSRNMCC.ChecksavedTransaction(txtCode.Value) Then
                        If (clsMilkSampleMCC.PostData(txtCode.Value, True)) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                            UsLock1.Status = ERPTransactionStatus.Approved
                            gv1.DataSource = Nothing
                            gv1.Rows.Clear()
                            LoadData(txtCode.Value)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            LoadData(txtCode.Value)
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub fndMCCCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtMilkReceiptNo._MYValidating, txtMilkReceiptNo._MYValidating
        Try
            If clsCommon.myLen(cboECOPro.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Eco Pro Machine First.", Me.Text)
                Exit Sub
            End If
            Dim sQuery As String = "select distinct TSPL_MILK_RECEIPT_HEAD.DOC_CODE as [Code],TSPL_MILK_RECEIPT_HEAD.MCC_CODE as [MCC Code],TSPL_MILK_RECEIPT_HEAD.DOC_DATE as [Date],case when TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 'Morning' " _
            & "  else 'Evening' end as [Shift],TOTAL_WEIGHT as [Total Qty] from TSPL_MILK_RECEIPT_HEAD Left join TSPL_MILK_SAMPLE_HEAD on Milk_Receipt_Code=TSPL_MILK_RECEIPT_HEAD.Doc_Code  "
            txtMilkReceiptNo.Value = clsCommon.ShowSelectForm("MRFND", sQuery, "Code", " TSPL_MILK_RECEIPT_HEAD.Doc_Code in (select Distinct Doc_Code from tspl_Milk_receipt_Detail where coalesce(is_Sampleed,'F')='F') ", "", "Code", isButtonClicked)

            If clsCommon.myLen(txtMilkReceiptNo.Value) > 0 Then
                LoadData("", txtMilkReceiptNo.Value)
                txtRangeFrom.Text = 1
                TxtRangeTo.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
                CboMachine.SelectedValue = clsMccMaster.DefaultSampleMachine(txtMilkReceiptNo.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, , , NavType)
            txtRangeFrom.Text = SetFromRange(txtCode.Value, Nothing) '1 'clsCommon.myCdbl(gv1.Rows.Count) + 1
            TxtRangeTo.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim sQuery As String = "select * from (select tspl_milk_Sample_Head.Doc_code as [Code],Mcc_Code,Milk_Receipt_Code as [Milk Receipt Code],convert(varchar,Doc_date,103) as Date,Shift,SUM(Qty) as [Total Qty],SUM(FAT_KG) as [Total FAT] ,SUM(SNF_KG) as [Total SNF] from tspl_milk_Sample_Head left join TSPL_MILK_SAMPLE_DETAIL on tspl_milk_Sample_Head.doc_code=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE where 2=2 "
            If clsCommon.myLen(lblDockCode.Text) > 0 Then
                sQuery += " and tspl_milk_Sample_Head.DOCK_Code='" + lblDockCode.Text + "'"
            End If
            sQuery += " group by tspl_milk_Sample_Head.Doc_code,Mcc_Code,Milk_Receipt_Code,Doc_date,Shift) tt "
            txtCode.Value = clsCommon.ShowSelectForm("MRFND", sQuery, "Code", "", txtCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                txtRangeFrom.Text = SetFromRange(txtCode.Value, Nothing) '1 'clsCommon.myCdbl(gv1.Rows.Count) + 1
                TxtRangeTo.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
            End If
            sQuery = String.Empty
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSavelayout.Click
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = "MilkSampleGrid"
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeletelayout.Click
        clsGridLayout.DeleteData("MilkSampleGrid", objCommonVar.CurrentUserCode)
        clsCommon.MyMessageBoxShow(Me, "Layout Deleted Successfully.")
        LoadData(txtCode.Value)
    End Sub

    Private Sub ReStoreGridLayout(ByVal GGGrid As RadGridView)
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkSampleGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing Then
                If obj.GridColumns >= GGGrid.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To GGGrid.Columns.Count - 1 Step ii + 1
                        GGGrid.Columns(ii).IsVisible = False
                        GGGrid.Columns(ii).VisibleInColumnChooser = True
                    Next
                    GGGrid.LoadLayout(obj.GridLayout)
                End If
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub cboECOPro_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboECOPro.SelectedValueChanged
        Try
            If CboEcoPro2.Items.Contains(cboECOPro.SelectedValue.ToString) Then
                GetECOPro(CboEcoPro2)
                CboEcoPro2.Items.Remove(cboECOPro.SelectedValue)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function SetFromRange(ByVal doc_code As String, ByVal trans As SqlTransaction)
        Dim sQuery As String = "select count(*) from tspl_milk_sample_detail where doc_code='" & doc_code & "'"
        Dim range_from As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
        Return range_from
    End Function

    Private Sub txtRangeFrom_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRangeFrom.Leave
        Try
            TxtRangeTo.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
            'If clsCommon.myCdbl(TxtRange1Panal2.Text) > 0 Then
            '    If (clsCommon.myCdbl(txtRangeFrom.Text) <> (clsCommon.myCdbl(Txtrange2panal2.Text) + 1)) And (clsCommon.myCdbl(Txtrange2panal2.Text) <> (clsCommon.myCdbl(TxtRange2.Text) + 1)) Then
            '        txtRangeFrom.Text = 0
            '        clsCommon.MyMessageBoxShow(Me,"Please Fill correct range .it should be " & (clsCommon.myCdbl(Txtrange2panal2.Text) + 1))
            '    End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, "txtRangeFrom_Leave")
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
            objSr._LblFat = LblFATPanel1
            objSr._LblSNF = LblSNFPanel1
            objSr._LblWeight = Nothing
            objSr.MachineName = clsPortSetting.getMachineMakePrefix(CboMachine.Text, 0)
            objSr.OpenPort()
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub BtnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStart.Click
        Try
            If clsCommon.CompairString(BtnStart.Text, "Start") = CompairStringResult.Equal Then
                If clsCommon.CompairString(BtnStart2.Text, "Stop") = CompairStringResult.Equal And clsCommon.CompairString(cboComPort.Text, CboComport2.Text) = CompairStringResult.Equal Then
                    Throw New Exception("This port is in use. choose another port")
                End If
                objSr2.isEco2 = True
                Timer1_Start()
                cboComPort.Enabled = False
                CboMachine.Enabled = False
                cboECOPro.Enabled = False
                BtnStart.Text = "Stop"
                frmMilkSampleMCC.isPortOpened = True
            Else
                cboComPort.Enabled = True
                CboMachine.Enabled = True
                cboECOPro.Enabled = True
                BtnStart.Text = "Start"
                LblSNFPanel1.Text = "00.00"
                LblFATPanel1.Text = "00.00"
                objSr.ClosePort()
                objSr2.isEco2 = False
                frmMilkSampleMCC.isPortOpened = False
            End If
        Catch ex As Exception
            cboComPort.Enabled = True
            CboMachine.Enabled = True
            cboECOPro.Enabled = True
            BtnStart.Text = "Start"
            LblSNFPanel1.Text = "00.00"
            LblFATPanel1.Text = "00.00"
            objSr.ClosePort()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnStart2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnStart2.Click
        Try
            If clsCommon.CompairString(BtnStart2.Text, "Start") = CompairStringResult.Equal Then
                If clsCommon.CompairString(BtnStart.Text, "Stop") = CompairStringResult.Equal And clsCommon.CompairString(cboComPort.Text, CboComport2.Text) = CompairStringResult.Equal Then
                    Throw New Exception("This port is in use. choose another port")
                End If
                Timer2_Start()
                CboComport2.Enabled = False
                CboMachine2.Enabled = False
                CboEcoPro2.Enabled = False
                BtnStart2.Text = "Stop"
                frmMilkSampleMCC.isPortOpened = True
            Else
                BtnStart2.Text = "Start"
                CboComport2.Enabled = True
                CboMachine2.Enabled = True
                CboEcoPro2.Enabled = True
                LblFATPanel2.Text = "00.00"
                LblSNFPanel2.Text = "00.00"
                objSr2.ClosePort()
                frmMilkSampleMCC.isPortOpened = False
            End If
        Catch ex As Exception
            CboComport2.Enabled = True
            CboMachine2.Enabled = True
            CboEcoPro2.Enabled = True
            BtnStart2.Text = "Start"
            LblFATPanel2.Text = "00.00"
            LblSNFPanel2.Text = "00.00"
            objSr2.ClosePort()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv2_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv2.CellFormatting
        CellFormatNew(gv2, e)
    End Sub

    Private Sub gv2_CellValidating(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles gv2.CellValidating
        CellValidatingNew(gv2, e)
    End Sub

    Private Sub gv2_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        CellValueChangedNew(gv2, e)
    End Sub

    Private Sub gv2_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv2.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do You want to delete this Row Permanently . Are You Sure ?", "Delete Row", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim sQuery As String = "delete from TSPL_MILK_SAMPLE_DETAIL where DOC_Code='" & gv2.CurrentRow.Cells("DOC CODE").Value & "' and VLC_DOC_Code='" & gv2.CurrentRow.Cells("VLC DOC CODE").Value & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery)
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Timer2_Start()
        Try
            Dim obj As clsPortSetting = clsPortSetting.getData(0, CboMachine2.Text) ' 0 for Milk analyzer and 1 for weighing machine,
            If obj Is Nothing Then
                Throw New Exception("No setting found for device")
            End If
            objSr2.BaudRate = obj.baud_rate
            objSr2.Parity = obj.parity
            objSr2.PortName = CboComport2.Text
            objSr2.StopBits = obj.stop_bits
            objSr2.DataBits = obj.data_bits
            objSr2.DataForm = clsPortSetting.getMachineDataFormat(CboMachine2.Text)
            objSr2.isEkoProMachine = True
            objSr2.isWeighingMachine = False
            objSr2._LblFat = LblFATPanel2
            objSr2._LblSNF = LblSNFPanel2
            objSr2._LblWeight = Nothing
            objSr2.isEco2 = True
            objSr2.MachineName = clsPortSetting.getMachineMakePrefix(CboMachine2.Text, 0)
            objSr2.OpenPort()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub



    Private Sub MyNumBox2_TextChanged(sender As Object, e As EventArgs) Handles MyNumBox2.TextChanged
        LblFATPanel1.Text = clsCommon.myCstr(MyNumBox2.Value)
    End Sub

    Private Sub MyNumBox1_TextChanged(sender As Object, e As EventArgs) Handles MyNumBox1.TextChanged
        LblSNFPanel1.Text = clsCommon.myCstr(GetSNFReading(MyNumBox1.Value))

    End Sub

    Private Sub MyNumBox6_TextChanged(sender As Object, e As EventArgs) Handles MyNumBox6.TextChanged
        LblFatpanel2.Text = clsCommon.myCstr(MyNumBox6.Value)
    End Sub

    Private Sub MyNumBox5_TextChanged(sender As Object, e As EventArgs) Handles MyNumBox5.TextChanged
        LblSNFPanel2.Text = clsCommon.myCstr(GetSNFReading(MyNumBox5.Value))
    End Sub

    Private Sub MyLabel5_DoubleClick(sender As Object, e As EventArgs) Handles MyLabel8.DoubleClick, MyLabel11.DoubleClick
        Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='AllowMilkReceiptAfterSettingsisOn' and TYPE='AllowMilkReceiptAfterSettingsisOn'")
        Dim pwd As New FrmPWD(Nothing)
        pwd.strCode = "AllowMilkReceiptAfterSettingsisOn"
        pwd.strType = "AllowMilkReceiptAfterSettingsisOn"
        pwd.ShowDialog()
        If pwd.isPasswordCorrect Then
            Panel1.Visible = True
            Panel3.Visible = True
        End If
    End Sub

    Function GetSNFReading(ByVal reading As Decimal) As Decimal
        Dim readingNew As Decimal
        If settMaxReceiveSNFPer > 0 And reading > settMaxReceiveSNFPer Then
            readingNew = settMaxReceiveSNFPer
        Else
            readingNew = reading
        End If
        Return readingNew
    End Function



    Private Sub LblSNFPanel1_TextChanged(sender As Object, e As EventArgs) Handles LblSNFPanel1.TextChanged
        SaveLogDataForImporperSample(gv1, LblFATPanel1, LblSNFPanel1)
    End Sub

    Private Sub LblSNFPanel2_TextChanged(sender As Object, e As EventArgs) Handles LblSNFPanel2.TextChanged
        SaveLogDataForImporperSample(gv2, LblFATPanel2, LblSNFPanel2)
    End Sub

    Sub SaveLogDataForImporperSample(ByVal gv As RadGridView, ByVal FATLable As RadLabel, ByVal SNFLable As RadLabel)
        If SettMaintainLogForImporperSample Then
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Dim obj As New clsMilkSampleReadingLog
                Dim ii As Integer
                Dim flagExisted As Boolean = False
                For ii = 0 To gv.RowCount - 1
                    If Not clsCommon.myCdbl(gv.Rows(ii).Cells(colSNF).Value) > 0 Then
                        flagExisted = True
                        Exit For
                    End If
                Next
                If flagExisted Then
                    obj.Sample_No = clsCommon.myCdbl(gv.Rows(ii).Cells(colSRNo).Value)
                    obj.Sample_Code = txtCode.Value
                    obj.Dock_Code = lblDockCode.Text
                    obj.FAT = clsCommon.myCdbl(FATLable.Text)
                    obj.SNF = clsCommon.myCdbl(SNFLable.Text)
                    obj.SaveData(obj)
                End If
                obj = Nothing
            End If
        End If
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then
            Dim frm As New frmMilkSampleMCCExtraQCParameter
            frm.strSampleNo = txtCode.Value
            frm.ShowDialog()
        End If
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select '1' as [Sample No],'5.5' as Fat,'8.5' as SNF,'MCUP00001' as [MCC Code],'01-Nov-2014' as Date,'Morning' as Shift"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Dim counter As Integer = 0
        Dim totqty As Double = 0
        Dim totsnf As Double = 0
        Dim totfat As Double = 0
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Sample No", "Fat", "SNF", "MCC Code", "Date", "Shift") Then
            Dim linno As Integer = 1
            Try
                Dim issaved As Boolean = False
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    counter += 1
                    Dim obj As New clsMilkSampleMCC()
                    Dim strCode As Integer = clsCommon.myCstr(grow.Cells("Sample No").Value)
                    Dim strFat As Double = clsCommon.myCstr(grow.Cells("FAT").Value)
                    Dim strSNF As Double = clsCommon.myCstr(grow.Cells("SNF").Value)
                    Dim strMCCode As String = clsCommon.myCstr(grow.Cells("MCC Code").Value)
                    Dim strDate As DateTime = clsCommon.myCstr(grow.Cells("Date").Value)
                    Dim strShift As String = IIf(clsCommon.myCstr(grow.Cells("Shift").Value).Contains("M"), "M", IIf(clsCommon.myCstr(grow.Cells("Shift").Value).Contains("E"), "E", ""))
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Sample No should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strFat) <= 0 Then
                        Throw New Exception("FAT should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strSNF) <= 0 Then
                        Throw New Exception("SNF should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strMCCode) <= 0 Then
                        Throw New Exception("MCC Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strDate) <= 0 Then
                        Throw New Exception("Date should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strShift) <= 0 Then
                        Throw New Exception("Shift should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myCstr(strMCCode) <> "" Then
                        Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(strMCCode)
                        If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "No shift is opened. one Shift Must be Opened..", Me.Text)
                            dtpDocDate.ReadOnly = True
                            cboShift.Enabled = False
                            Irregular_Mcc_Code = ""
                            Exit Sub
                        ElseIf DTShift.Rows.Count > 1 Then
                            clsCommon.MyMessageBoxShow(Me, "There are more then one shifts are opened.Only one Shift can be Opened..", Me.Text)
                            dtpDocDate.ReadOnly = True
                            cboShift.Enabled = False
                            Irregular_Mcc_Code = ""
                            Exit Sub
                        Else
                            dtpDocDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
                            cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))

                            If clsCommon.CompairString(clsCommon.GetPrintDate(strDate, "dd-MMM-yyyy"), clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy")) <> CompairStringResult.Equal Then
                                Throw New Exception("Fill Shift Date Correctly At Line No. " + clsCommon.myCstr(counter) + "." & Environment.NewLine _
                                                    & "[" & dtpDocDate.Value & "] ,[" & cboShift.Text & "] Shift is Open. ")
                            End If

                            If clsCommon.CompairString(strShift, cboShift.SelectedValue) <> CompairStringResult.Equal Then
                                Throw New Exception("Fill Shift Correctly At Line No. " + clsCommon.myCstr(counter) + "." & Environment.NewLine _
                                                    & "[" & dtpDocDate.Value & "] ,[" & cboShift.Text & "] Shift is Open. ")
                            End If
                            Irregular_Mcc_Code = clsCommon.myCstr(DTShift.Rows(0).Item("Irregular_Mcc_Code"))
                            dtpDocDate.ReadOnly = True
                            cboShift.Enabled = False
                        End If
                    End If
                    Me.txtMilkReceiptNo.Value = clsMilkReceiptMCC.GetDocCode(strDate, strMCCode, strShift)
                    Dim IssampleExits As Boolean = clsMilkSampleMCC.GetDocArray(strDate, strMCCode, strShift, strCode)
                    If IssampleExits = False Then
                        isInsideImportData = True
                        If clsCommon.myLen(Me.txtMilkReceiptNo.Value) > 0 Then
                            txtRangeFrom.Text = strCode
                            TxtRangeTo.Text = strCode
                            If clsCommon.myCdbl(txtRangeFrom.Text) > 0 And clsCommon.myCdbl(TxtRangeTo.Text) > 0 Then
                                If gv1.Rows.Count > 0 Then
                                    Load_Receipt_From_Dt(gv1, True)
                                End If
                                Load_Receipt_From_Dt(gv1, False)
                            End If
                            isInsideLoadData = False
                            If gv1.Rows.Count > 0 Then
                                For Each row As GridViewRowInfo In gv1.Rows
                                    If row.Cells(colSRNo).Value = strCode Then
                                        row.Cells(colFAT).Value = Math.Truncate(strFat * 10) / 10
                                        row.Cells(colSNF).Value = Math.Truncate(strSNF * 10) / 10
                                        row.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(row.Cells(colFAT).Value), clsCommon.myCdbl(row.Cells(colSNF).Value), clsCommon.myCdbl(row.Cells(colCOR).Value))
                                        Dim strMilkType As String = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                                        If objCommonVar.DisplayTypeInMilkReceipt Then
                                            strMilkType = clsCommon.myCstr(grow.Cells(ColType).Value)
                                        End If
                                        Dim strPriceCode As String = ""
                                        row.Cells(ColRATE).Value = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCdbl(row.Cells(ColQty).Value), strPriceCode, clsCommon.myCdbl(row.Cells(colFAT).Value), clsCommon.myCdbl(row.Cells(colSNF).Value), clsCommon.myCstr(txtMilkReceiptNo.Tag), clsCommon.myCstr(row.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, strMilkType)
                                        row.Cells(ColPriceCode).Value = strPriceCode
                                        row.Cells(COLAMOUNT).Value = clsCommon.myCdbl(row.Cells(ColRATE).Value) * clsCommon.myCdbl(row.Cells(ColQty).Value) '* clsCommon.myCdbl(gv1.CurrentRow.Cells(ColMILK_Qty).Value)
                                        '=========Added For SUM=============
                                        totqty += clsCommon.myCdbl(row.Cells(ColQty).Value)
                                        totsnf += clsCommon.myCdbl(row.Cells(colSNF).Value) * clsCommon.myCdbl(row.Cells(ColQty).Value) / 100
                                        totfat += clsCommon.myCdbl(row.Cells(colFAT).Value) * clsCommon.myCdbl(row.Cells(ColQty).Value) / 100
                                        '====================================
                                        If BtnStart.Text = "Stop" Then
                                            gv1.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                                        Else
                                            gv1.CurrentRow.Cells(COL_IS_MANUAL).Value = "Manual"
                                        End If
                                        issaved = True
                                    End If
                                Next
                            End If
                            txtTotalQty.Text = totqty
                            txttotSNF.Text = totsnf
                            txttotFAT.Text = totfat
                            isInsideLoadData = True
                        Else
                            Throw New Exception("No Receipt Found of Selected MCC,date and Shift.")
                        End If
                        If issaved = True Then
                            SaveData_Record(gv1)
                            clsCommon.ProgressBarUpdate(counter & "/" & gv.Rows.Count)
                            isInsideImportData = False
                        Else
                            isInsideImportData = False
                            Throw New Exception("No Receipt Found of Selected MCC,date and Shift and Sample No [" & strCode & "]")
                        End If
                    End If
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                LoadData(txtCode.Value)
            Catch ex As Exception
                isInsideImportData = False
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            OpenFileDialog.FileName = ""
            OpenFileDialog.Filter = "BDF Files (*.BDF)|*.BDF"
            If OpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim fileDate As String = Nothing
                Dim fileShift As String = Nothing
                If OpenFileDialog.FileName.Contains(".BDF") = True Or OpenFileDialog.FileName.Contains(".bdf") = True Then
                    If Not File.Exists(Application.StartupPath + "\XpertBennyDecrptor.exe") Then
                        Throw New Exception("Please add File - XpertBennyDecrptor.exe.")
                    End If
                    If OpenFileDialog.FileName.Contains(" ") Then
                        Throw New Exception("Please Remove Blank space of your folder becuase it is having Blank Space")
                    End If
                    Dim strOPFile As String = "C:\\ERPTempFolder\BSPV.CSV"
                    If File.Exists(strOPFile) Then
                        File.Delete(strOPFile)
                    End If
                    Process.Start(Application.StartupPath + "\XpertBennyDecrptor.exe", "-i " + OpenFileDialog.FileName + " -o " + strOPFile + " -s , -f")
                    Dim dtRawData As DataTable = transportSql.GetExcelData(strOPFile, Path.GetFileName(OpenFileDialog.FileName))
                    'Dim strVLCCode As String = Nothing
                    Dim strMCCCode As String = Nothing
                    Dim ii As Integer = 0
                    'Dim qry As String
                    Dim dtMasterData As DataTable = Nothing
                    Dim totqty As Double = 0
                    Dim totsnf As Double = 0
                    Dim totfat As Double = 0
                    Dim intSuccess As Integer = 0
                    Try
                        Dim strMCCode As String = ""
                        Dim strShift As String = ""
                        Dim dtDate As Date
                        clsCommon.ProgressBarPercentShow()
                        For ii = 0 To dtRawData.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100 / (dtRawData.Rows.Count)), "" & clsCommon.myCstr(ii + 1) & "/" & clsCommon.myCstr(dtRawData.Rows.Count) & "")

                            Dim strSampleNo As String = clsCommon.myCstr(dtRawData.Rows(ii)("Local_Code"))
                            Dim intSampleNo As Integer = clsCommon.myCdbl(strSampleNo)
                            If intSampleNo > 0 Then
                                strShift = clsCommon.myCstr(dtRawData.Rows(ii)("SHIFT"))
                                Dim strDate As String = clsCommon.myCstr(dtRawData.Rows(ii)("Date"))
                                Dim arrDate As String() = strDate.Split(".")
                                If arrDate IsNot Nothing AndAlso arrDate.Count > 2 Then
                                    dtDate = New Date(clsCommon.myCdbl(arrDate(2)), clsCommon.myCdbl(arrDate(1)), clsCommon.myCdbl(arrDate(0)))
                                End If
                                strMCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code from TSPL_MCC_MASTER where Mcc_Code_VLC_Uploader='" + clsCommon.myCstr(dtRawData.Rows(ii)("CP_CODE")) + "'"))
                                If clsCommon.myLen(strMCCode) <= 0 Then
                                    Throw New Exception("Invalid MCC Uploader No [" + clsCommon.myCstr(dtRawData.Rows(ii)("CP_CODE")) + "]")
                                End If
                                If clsCommon.myLen(txtMilkReceiptNo.Value) <= 0 Then
                                    Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(strMCCode)
                                    If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                                        dtpDocDate.ReadOnly = True
                                        cboShift.Enabled = False
                                        Irregular_Mcc_Code = ""
                                        Throw New Exception("No shift is opened. one Shift Must be Opened..")
                                    ElseIf DTShift.Rows.Count > 1 Then
                                        dtpDocDate.ReadOnly = True
                                        cboShift.Enabled = False
                                        Irregular_Mcc_Code = ""
                                        Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
                                    Else
                                        dtpDocDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
                                        cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))

                                        Irregular_Mcc_Code = clsCommon.myCstr(DTShift.Rows(0).Item("Irregular_Mcc_Code"))
                                        dtpDocDate.ReadOnly = True
                                        cboShift.Enabled = False

                                        Me.txtMilkReceiptNo.Value = clsMilkReceiptMCC.GetDocCode(dtpDocDate.Value, strMCCode, strShift)
                                    End If
                                End If
                                If clsCommon.myLen(strMCCode) <= 0 Then
                                    Throw New Exception("Invalid MCC Uploader No [" + clsCommon.myCstr(dtRawData.Rows(ii)("CP_CODE")) + "]")
                                End If
                                If clsCommon.GetDateWithStartTime(dtpDocDate.Value) <> clsCommon.GetDateWithStartTime(dtDate) Then
                                    Continue For
                                    'Throw New Exception("Open Shift Date is [" + clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy") + "] and BDF File Date is [" + clsCommon.GetPrintDate(dtDate, "dd/MMM/yyyy") + "]")
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(cboShift.SelectedValue), strShift) <> CompairStringResult.Equal Then
                                    Continue For
                                    'Throw New Exception("Open Shift is [" + clsCommon.myCstr(cboShift.SelectedValue) + "] and BDF File Date is [" + strShift + "]")
                                End If

                                'qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD  where VLC_Code_VLC_Uploader='" + strSampleNo + "' and MCC='" + strMCCode + "'"
                                'strVLCCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                                'If clsCommon.myLen(strVLCCode) <= 0 Then
                                '    Throw New Exception("VLC Uploader No [" + strSampleNo + "] is not mapped with MCC [" + strMCCCode + "]")
                                'End If

                                'Dim Qty As Decimal = clsCommon.myCDecimal(dtRawData.Rows(ii)("Quantity"))
                                Dim FAT As Decimal = Math.Truncate(clsCommon.myCdbl(dtRawData.Rows(ii)("FAT")) * 10) / 10
                                Dim SNF As Decimal = Math.Truncate(clsCommon.myCdbl(dtRawData.Rows(ii)("SNF")) * 10) / 10

                                'Dim TotalReceipt As Integer = 0
                                ''Dim intSampleNo As Integer = clsMilkReceiptMCC.GetSampleNo(dtpDocDate.Value, strMCCode, strShift, strVLCCode, TotalReceipt)
                                'If TotalReceipt = 0 Then
                                '    Throw New Exception("No sample Found For VLC [" + strSampleNo + "]")
                                'End If
                                'If TotalReceipt > 1 Then
                                '    Throw New Exception("More Than one sample Found For VLC [" + strSampleNo + "]")
                                'End If
                                Dim IssampleExits As Boolean = clsMilkSampleMCC.GetDocArray(dtpDocDate.Value, strMCCode, strShift, intSampleNo)
                                If Not IssampleExits Then
                                    isInsideImportData = True
                                    If clsCommon.myLen(Me.txtMilkReceiptNo.Value) > 0 Then
                                        txtRangeFrom.Text = intSampleNo
                                        TxtRangeTo.Text = intSampleNo
                                        If clsCommon.myCdbl(txtRangeFrom.Text) > 0 And clsCommon.myCdbl(TxtRangeTo.Text) > 0 Then
                                            If gv1.Rows.Count > 0 Then
                                                Load_Receipt_From_Dt(gv1, True, False)
                                            End If
                                            Load_Receipt_From_Dt(gv1, False, False)
                                        End If
                                        isInsideLoadData = False
                                        If gv1.Rows.Count > 0 Then
                                            For Each row As GridViewRowInfo In gv1.Rows
                                                If row.Cells(colSRNo).Value = intSampleNo Then
                                                    row.Cells(colFAT).Value = FAT
                                                    row.Cells(colSNF).Value = SNF
                                                    row.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(row.Cells(colFAT).Value), clsCommon.myCdbl(row.Cells(colSNF).Value), clsCommon.myCdbl(row.Cells(colCOR).Value))
                                                    Dim strMilkType As String = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                                                    If objCommonVar.DisplayTypeInMilkReceipt Then
                                                        'strMilkType = clsCommon.myCstr(grow.Cells(ColType).Value)
                                                    End If
                                                    Dim strPriceCode As String = ""
                                                    row.Cells(ColRATE).Value = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCdbl(row.Cells(ColQty).Value), strPriceCode, clsCommon.myCdbl(row.Cells(colFAT).Value), clsCommon.myCdbl(row.Cells(colSNF).Value), clsCommon.myCstr(txtMilkReceiptNo.Tag), clsCommon.myCstr(row.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, strMilkType)
                                                    row.Cells(ColPriceCode).Value = strPriceCode
                                                    row.Cells(COLAMOUNT).Value = clsCommon.myCdbl(row.Cells(ColRATE).Value) * clsCommon.myCdbl(row.Cells(ColQty).Value)
                                                    '=========Added For SUM=============
                                                    totqty += clsCommon.myCdbl(row.Cells(ColQty).Value)
                                                    totsnf += clsCommon.myCdbl(row.Cells(colSNF).Value) * clsCommon.myCdbl(row.Cells(ColQty).Value) / 100
                                                    totfat += clsCommon.myCdbl(row.Cells(colFAT).Value) * clsCommon.myCdbl(row.Cells(ColQty).Value) / 100
                                                    '====================================
                                                    If clsCommon.CompairString(clsCommon.myCstr(dtRawData.Rows(ii)("Measurement_Mode")), "AUTOMATIC") = CompairStringResult.Equal Then
                                                        gv1.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                                                    Else
                                                        gv1.CurrentRow.Cells(COL_IS_MANUAL).Value = "Manual"
                                                    End If
                                                    Issaved = True
                                                End If
                                            Next
                                        End If
                                        txtTotalQty.Text = totqty
                                        txttotSNF.Text = totsnf
                                        txttotFAT.Text = totfat
                                        isInsideLoadData = True
                                        'Else
                                        '    Continue For
                                    End If

                                    If Issaved Then
                                        SaveData_Record(gv1)
                                        intSuccess += 1
                                        isInsideImportData = False
                                    Else
                                        isInsideImportData = False
                                        'Continue For
                                        'Throw New Exception("No Receipt Found of Selected MCC,date and Shift and Sample No")
                                    End If
                                    Issaved = False
                                End If
                            End If
                        Next
                        clsCommon.ProgressBarPercentHide()
                        clsCommon.MyMessageBoxShow(Me, clsCommon.myCstr(intSuccess) + " Samples Imported", Me.Text, MessageBoxButtons.OK)
                        LoadData(txtCode.Value)
                    Catch ex As Exception
                        clsCommon.ProgressBarPercentHide()
                        Throw New Exception("Error at Row No [" + clsCommon.myCstr(ii + 1) + "]" + Environment.NewLine + ex.Message)
                    End Try
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
