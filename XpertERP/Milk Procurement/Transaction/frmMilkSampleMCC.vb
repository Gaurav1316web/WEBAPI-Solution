Imports System.Data.SqlClient
Imports System.IO
Imports System.Threading
Imports common
''SWA/14/05/18-000023 by balwinder on 18/05/2018

Public Class frmMilkSampleMCC
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Irregular_Mcc_Code As String = String.Empty
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

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
    Const colDEN As String = "ColDEN"
    Const colProtein As String = "colProtein"
    Const colAWM As String = "colAWM"

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


    Private isInsideLoadData As Boolean = False
    Private isInsideImportData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Public DtMilkReceipt, DtVehicle, DtMilkReceiptEcoProWise As DataTable
    Public objEco As New clsEkoPro
    Public objSr As New clsSerialPort
    Public objSr2 As New clsSerialPort
    Public objwg As New clsWeighingMachine
    Dim isCellValueChanged As Boolean = True
    Dim isForcellyStarted As Boolean = False
    Dim IsCanceled As Boolean = False
    Dim oldValue As Double = 0.0
    Public th1 As Thread
    Dim th2 As Thread
    Dim is_Manual As Boolean = False
    Dim irowIndex_Global_gv1 As Integer = -1
    Dim irowIndex_Global_gv2 As Integer = -1
    Dim Issaved As Boolean = False
    Dim DtVSPChargeDetail As DataTable
    Dim DtPriceChargeDetail As DataTable
    Public Shared isPortOpened As Boolean = False
    Public DocumentNo As String = Nothing
    Dim DisplayAllControls As String = String.Empty
    Dim DtMilkTypeRange As New DataTable
    Private GridFontSize As Integer = 0
    Private GridFont As Font
    Private isStopForRepeatedFATSNF As Boolean = False
#End Region

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isStopForRepeatedFATSNF = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StopForRepeatedFATSNF, clsFixedParameterCode.StopForRepeatedFATSNF, Nothing)) = 0, False, True) ''Make Setting Balwinder
        GridFontSize = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SampleFONTSize, clsFixedParameterCode.SampleFONTSize, Nothing)) ''Make Setting Balwinder
        If GridFontSize < 8 Then
            GridFontSize = 15
        End If
        GridFont = New Font("Ariel", GridFontSize)

        DisplayAllControls = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCDisplay_All_Parameter, clsFixedParameterCode.MilkSetting, Nothing)) = 0, False, True)
        If DisplayAllControls = "False" Then
            DempGRP1.Visible = False
            DemoGrp2.Visible = False
        End If
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
        If DisplayAllControls = "False" Then
            GetECOPro(cboECOPro)
            GetECOPro(CboEcoPro2)
        Else
            GetECOPro(DemoEcoPro1)
            GetECOPro(DemoEcoPro2)
        End If
        AddNew()
        TxtRange1Panal2.Text = Nothing
        TxtRange2.Text = Nothing
        txtRangeFrom.Text = Nothing
        Txtrange2panal2.Text = Nothing
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowEditRow = True
        gv1.MasterTemplate.AllowCellContextMenu = True
        gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv1.MasterTemplate.AllowDeleteRow = True
        ReStoreGridLayout()
        SplitContainer2.Panel2Collapsed = True

        gv2.AllowAddNewRow = False
        gv2.AllowDeleteRow = False
        gv2.AllowEditRow = True
        gv2.MasterTemplate.AllowCellContextMenu = True
        gv2.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv2.MasterTemplate.AllowDeleteRow = True

        ' Timer1.Start()
        Timer1.Interval = 1000
        Timer2.Interval = 1000
        Dim viewMilkReceiptSample As Boolean = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select View_Milk_Receipt_Sample from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' ")) = 1)
        Me.fndMCCCode.Tag = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(fndMCCCode.Tag) <= 0 Then
            If Not viewMilkReceiptSample Then
                Throw New Exception("Please set Default location of current user")
            End If
        End If

        If clsCommon.myLen(fndMCCCode.Tag) > 0 Then
            Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMCCCode.Tag)
            If DTShift IsNot Nothing AndAlso DTShift.Rows.Count > 0 Then
                dtpDocDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
                cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))
                dtpDocDate.ReadOnly = True
                cboShift.Enabled = False
            Else
                Throw New Exception("No Milk Collected. No Sample can be Done.")
            End If
            SetDocKCollectionMilkType(fndMCCCode.Tag)
            txtCode.Value = clsMilkSampleMCC.GetDocCode(clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy"), fndMCCCode.Tag, cboShift.SelectedValue, Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), "")
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                txtRangeFrom.Text = SetFromRange(txtCode.Value, Nothing)
                TxtRange2.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
            End If
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                fndMCCCode.Value = clsMilkReceiptMCC.GetDocCode(clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy"), fndMCCCode.Tag, cboShift.SelectedValue, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                If clsCommon.myLen(fndMCCCode.Value) > 0 Then
                    txtRangeFrom.Text = clsCommon.myCdbl(gv1.Rows.Count) + 1
                    TxtRange2.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
                    Load_Receipt_From_Dt(False)
                End If
            End If
        End If

        txtRangeFrom.ReadOnly = False ''Balwinder
        TxtRange2.ReadOnly = True
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, "", Nothing, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        LblDemoAWM.Text = 5.0
        LblDemodensity.Text = 1.032
        LblDemoFat.Text = 4.2
        LblDemoSNf.Text = 9.0
        LblDempProtein.Text = 3.06
        GetMilkTypeDt()

        ' added for drill down 
        Try
            If clsCommon.myLen(Me.Tag) > 0 Then
                txtCode.Value = Me.Tag
                LoadData(txtCode.Value, , , NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, ex.Message)
        End Try
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
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkSample)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = False 'MyBase.isDeleteFlag
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
        Try
            isInsideImportData = True
            Dim totqty As Double = 0
            Dim totsnf As Double = 0
            Dim totfat As Double = 0
            Dim counter As Integer = 0
            clsCommon.ProgressBarShow()
            For Each grow As GridViewRowInfo In GGGrid.Rows
                counter += 1
                If grow.Cells(colFAT).Value <= 0 Or grow.Cells(colSNF).Value <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please Fill Fat And SNF in all Rows. sample no [" & grow.Index + 1 & "]")
                    Return False
                End If
                isCellValueChanged = False
                grow.Cells(colFAT).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(colFAT).Value) * 10) / 10
                grow.Cells(colSNF).Value = Math.Truncate(clsCommon.myCdbl(grow.Cells(colSNF).Value) * 10) / 10
                isCellValueChanged = True
                grow.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCdbl(grow.Cells(colCOR).Value))
                grow.Cells(ColPriceCode).Value = clsEkoPro.getPriceCodeFromUploaderShiftwise(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                grow.Cells(ColRATE).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                grow.Cells(COLAMOUNT).Value = clsCommon.myCdbl(grow.Cells(ColRATE).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value)
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
                objHead.MCC_CODE = clsCommon.myCstr(fndMCCCode.Tag)
                objHead.MILK_RECEIPT_CODE = clsCommon.myCstr(fndMCCCode.Value)
                objHead.TOTAL_QTY = clsCommon.myCdbl(txtTotalQty.Text)
                objHead.TOTAL_FAT = clsCommon.myCdbl(txttotFAT.Text)
                objHead.TOTAL_SNF = clsCommon.myCdbl(txttotSNF.Text)
                objHead.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)

                Dim objList As New List(Of clsMilkSampleMCCDetail)
                Dim objListHistory As New List(Of clsMilkSampleMCCDetailHistory)
                Dim obj1 As clsMilkSampleMCCDetail
                Dim objH As clsMilkSampleMCCDetailHistory

                For Each grow As GridViewRowInfo In GGGrid.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)) > 0 Then
                        Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & fndMCCCode.Value & "' and sample_nO='" & clsCommon.myCstr(grow.Cells(colSRNo).Value) & "'")
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
                    clsCommon.ProgressBarHide()
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
            GC.Collect()
            totfat = Nothing
            totsnf = Nothing
            totqty = Nothing
            GC.Collect()
            Return True
        Catch ex As Exception
            ' trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Function SaveData_Record(ByVal GGGrid As RadGridView)
        LoadDt()
        Dim trans As SqlTransaction = Nothing
        Try
            'If (AllowToSave(IsPostData)) Then
            isInsideImportData = True
            Dim dtPriceChart As DataTable = clsEkoPro.getPriceCodeDatatableFromUploader(fndMCCCode.Tag, GGGrid.Rows(0).Cells(ColVlcCode).Value, IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
            Dim totqty As Double = 0
            Dim totsnf As Double = 0
            Dim totfat As Double = 0
            If dtPriceChart.Rows.Count > 0 Then
                For Each grow As GridViewRowInfo In GGGrid.Rows
                    If clsCommon.myCdbl(grow.Cells(colFAT).Value) > 0 And clsCommon.myCdbl(grow.Cells(colSNF).Value) > 0 Then 'clsCommon.myCdbl(grow.Cells(ColRATE).Value) <= 0 And
                        Dim dr() As DataRow = dtPriceChart.Select("fat=" & clsCommon.myCdbl(grow.Cells(colFAT).Value) & " and snf=" & clsCommon.myCdbl(grow.Cells(colSNF).Value) & "")
                        If Not IsNothing(dr) Then
                            grow.Cells(ColRATE).Value = dr(0)("Rate")
                            grow.Cells(ColPriceCode).Value = dr(0)("Code")
                        Else
                            grow.Cells(ColRATE).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                            grow.Cells(ColPriceCode).Value = clsEkoPro.getPriceCodeFromUploaderShiftwise(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                        End If
                        grow.Cells(COLAMOUNT).Value = clsCommon.myCdbl(grow.Cells(ColRATE).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value)
                        grow.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCdbl(grow.Cells(colCOR).Value))
                        totqty += clsCommon.myCdbl(grow.Cells(ColQty).Value)
                        totsnf += clsCommon.myCdbl(grow.Cells(colSNF).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                        totfat += clsCommon.myCdbl(grow.Cells(colFAT).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                    End If
                Next
            Else
                For Each grow As GridViewRowInfo In GGGrid.Rows
                    If clsCommon.myCdbl(grow.Cells(colFAT).Value) > 0 And clsCommon.myCdbl(grow.Cells(colSNF).Value) > 0 Then
                        grow.Cells(ColRATE).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                        grow.Cells(COLAMOUNT).Value = clsCommon.myCdbl(grow.Cells(ColRATE).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value)
                        grow.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCdbl(grow.Cells(colCOR).Value))
                        grow.Cells(ColPriceCode).Value = clsEkoPro.getPriceCodeFromUploaderShiftwise(clsCommon.myCdbl(grow.Cells(colFAT).Value), clsCommon.myCdbl(grow.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(grow.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                        totqty += clsCommon.myCdbl(grow.Cells(ColQty).Value)
                        totsnf += clsCommon.myCdbl(grow.Cells(colSNF).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                        totfat += clsCommon.myCdbl(grow.Cells(colFAT).Value) * clsCommon.myCdbl(grow.Cells(ColQty).Value) / 100
                    End If
                Next
            End If

            txtTotalQty.Text = totqty
            txttotSNF.Text = totsnf
            txttotFAT.Text = totfat

            trans = clsDBFuncationality.GetTransactin()
            Dim objHead As clsMilkSampleMCC
            '' asign screen vaules in objHead
            objHead = New clsMilkSampleMCC
            objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
            objHead.DOC_DATE = clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy hh:mm:ss tt")
            objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
            objHead.MCC_CODE = clsCommon.myCstr(fndMCCCode.Tag)
            objHead.MILK_RECEIPT_CODE = clsCommon.myCstr(fndMCCCode.Value)
            objHead.TOTAL_QTY = clsCommon.myCdbl(txtTotalQty.Text)
            objHead.TOTAL_FAT = clsCommon.myCdbl(txttotFAT.Text)
            objHead.TOTAL_SNF = clsCommon.myCdbl(txttotSNF.Text)
            objHead.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
            Dim objList As New List(Of clsMilkSampleMCCDetail)
            Dim objListHistory As New List(Of clsMilkSampleMCCDetailHistory)

            Dim obj1 As clsMilkSampleMCCDetail
            Dim objH As clsMilkSampleMCCDetailHistory

            For Each grow As GridViewRowInfo In GGGrid.Rows
                If clsCommon.myCdbl(grow.Cells(colFAT).Value) > 0 And clsCommon.myCdbl(grow.Cells(colSNF).Value) > 0 And clsCommon.myCstr(grow.Cells(colSNF).Value) <> "T" Then 'And irowIndex_Global_GGGrid = grow.Index Then
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)) > 0 Then
                        Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & fndMCCCode.Value & "' and sample_nO='" & clsCommon.myCstr(grow.Cells(colSRNo).Value) & "'")
                        obj1 = New clsMilkSampleMCCDetail()
                        obj1.DOC_CODE = txtCode.Value
                        obj1.SAMPLE_NO = clsCommon.myCdbl(grow.Cells(colSRNo).Value)
                        obj1.VLC_DOC_CODE = clsCommon.myCstr(dr(0)("VLC_DOC_CODE")) '  clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)

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
            isInsideImportData = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Function SaveSRNData() As Boolean
        Try
            Dim counter As Integer = 0
            Dim objHead As clsMilkSRNMCC
            '' asign screen vaules in objHead
            Dim objList As New List(Of clsMilkSRNMCCDetail)
            Dim obj1 As clsMilkSRNMCCDetail
            Dim objVSPChargeList As New List(Of clsMilkSRNVSpChargeDetail)
            Dim objVSP_Charge1 As clsMilkSRNVSpChargeDetail
            Dim objPriceChargeList As New List(Of clsMilkSRNPriceChargeDetail)
            Dim objPrice_Charge1 As clsMilkSRNPriceChargeDetail
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

            clsCommon.ProgressBarShow()
            For Each grow As GridViewRowInfo In gv1.Rows
                counter += 1
                If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRNo).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value)) > 0 And clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSRN_CODE).Value)) <= 1 Then
                    objList = New List(Of clsMilkSRNMCCDetail)
                    objVSPChargeList = New List(Of clsMilkSRNVSpChargeDetail)
                    objPriceChargeList = New List(Of clsMilkSRNPriceChargeDetail)
                    Dim dr() As DataRow = DtMilkReceipt.Select("DOC_CODE='" & fndMCCCode.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(grow.Cells(ColVlcDocCode).Value) & "'")
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
                    obj1.Commission = clsCommon.myCdbl(dr(0)("commision_pers"))
                    obj1.Own_Asset_Rate = clsCommon.myCdbl(dr(0)("Rate_Own_Asset"))
                    obj1.Payment_Commission = clsCommon.myCdbl(dr(0)("payment_commision_pers"))
                    If clsCommon.myCstr(dr(0)("Service_Charge_Type")) = "%(Percentage)" Then
                        obj1.Commission_Amount = Math.Round(obj1.AMOUNT * obj1.Commission / 100, 2)
                        obj1.Emp_Amount = Math.Round(obj1.AMOUNT * obj1.Payment_Commission / 100, 2)
                    ElseIf clsCommon.myCstr(dr(0)("Service_Charge_Type")) = "Rate/Kg" Then
                        obj1.Commission_Amount = Math.Round(obj1.ACC_Qty * obj1.Commission, 2)
                        obj1.Emp_Amount = Math.Round(obj1.ACC_Qty * obj1.Payment_Commission, 2)
                    ElseIf clsCommon.myCstr(dr(0)("Service_Charge_Type")) = "Rate/Ltr" And clsCommon.myCstr(dr(0)("UOM_Code")) = "LTR" Then
                        obj1.Commission_Amount = Math.Round(obj1.MILK_Qty * obj1.Commission, 2)
                        obj1.Emp_Amount = Math.Round(obj1.MILK_Qty * obj1.Payment_Commission, 2)
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
                    End If
                    obj1.Head_Load_Type = clsCommon.myCstr(objHeadLoad.Head_Load_Basis)
                    '============================================
                    '==================Own Asset==========================
                    If clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "K") = CompairStringResult.Equal Then
                        obj1.Own_Asset_Amount = Math.Round(obj1.ACC_Qty * obj1.Own_Asset_Rate, 2)
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset")), "L") = CompairStringResult.Equal Then
                        obj1.Own_Asset_Amount = Math.Round(obj1.MILK_Qty * obj1.Own_Asset_Rate, 2)
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
                    obj1.Own_Asset_Type = clsCommon.myCstr(dr(0)("Service_Basis_Own_Asset"))
                    '============================================
                    obj1.Service_Charge_Amount = Math.Round(obj1.MILK_Qty * clsCommon.myCdbl(dr(0)("Service_Charge_Per_Unit")), 2)
                    obj1.NET_AMOUNT = Math.Round(obj1.AMOUNT + obj1.Emp_Amount - obj1.Service_Charge_Amount, 2)
                    obj1.COMM_PORT = clsCommon.myCstr(cboComPort.SelectedValue)
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
                        clsCommon.ProgressBarUpdate("SRN Created : " & counter & "/" & gv1.Rows.Count)
                    End If
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Function AllowToSave(ByVal GGGrid As RadGridView, ByVal IsPostData As Boolean) As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpDocDate.Value, Nothing) = False Then
                dtpDocDate.Select()
                Return False
            End If
            '=======================================================
            If btnsave.Text = "Update" Then
                Dim strchk As String = "select POSTED from TSPL_MILK_Sample_HEAD where DOC_COde='" + txtCode.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted")
                    Return False
                End If
            End If
            '===========ADDED For Optimazation==============
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
            '======================================================

            If clsCommon.myLen(Me.cboShift.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Shift", Me.Text)
                Return False
            End If

            If clsCommon.myLen(Me.fndMCCCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter MCC", Me.Text)
                Return False
            End If


            'If Not isInsideImportData Then
            '    If clsCommon.myCdbl(Me.txtTotalQty.Text) <= 0 Then
            '        clsCommon.MyMessageBoxShow(Me,"Please Enter Milk Qty", Me.Text)
            '        Return False
            '    End If

            '    If clsCommon.myCdbl(Me.txttotFAT.Text) <= 0 Then
            '        clsCommon.MyMessageBoxShow(Me,"Please Enter Milk FAT", Me.Text)
            '        Return False
            '    End If

            '    If clsCommon.myCdbl(Me.txttotSNF.Text) <= 0 Then
            '        clsCommon.MyMessageBoxShow(Me,"Please Enter Milk SNF", Me.Text)
            '        Return False
            '    End If
            'End If
            If IsPostData And clsCommon.myLen(vlc_arr) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, vlc_arr & " have price 0. " & Environment.NewLine & " Do you still want to Post Data", "Post Data", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                    Return False
                End If
            End If

            'If UsLock1.Status = ERPTransactionStatus.Approved Then
            '    clsCommon.MyMessageBoxShow(Me,"This Document is Approved and can not take new entries..", Me.Text)
            '    Return False
            'End If

            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Function

    Sub AddNew()
        ' isNewEntry = True
        txtCode.Value = ""
        fndMCCCode.Enabled = True
        isCellValueChangedOpen = False
        btnsave.Text = "Save"
        dtpDocDate.MinDate = "01-Jan-0001"
        gv1.DataSource = Nothing
        gv2.DataSource = Nothing
        btnsave.Enabled = True
        BtnPost.Enabled = True
        btndelete.Enabled = True
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Me.cboShift.SelectedIndex = -1
        Me.fndMCCCode.Value = Nothing 'clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
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
        Load_Receipt_From_Dt(True)
        txtRangeFrom.ReadOnly = False ''Balwinder
        ' TxtRange2.ReadOnly = True
        txtRangeFrom.Text = 0
        TxtRange2.Text = 0
        cboECOPro.SelectedValue = 0 'Nothing
        SplitContainer2.Panel2Collapsed = True
        GrpEcoPro2.Enabled = False
        DemoGrp2.Enabled = False
        CboEcoPro2.SelectedValue = 0
        BtnStart2.Text = "Start"
        If DisplayAllControls = "False" Then
            clsPortSetting.GetMachineType(CboMachine)
            clsPortSetting.GetMachineType(CboMachine2)
        Else
            clsPortSetting.GetMachineType(DemoMachine1)
            clsPortSetting.GetMachineType(DemoMachine2)
        End If
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
        If SaveData(gv1, False) Then
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
        End If
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Load_Receipt_From_Dt(True)
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
            ' If clsCommon.MyMessageBoxShow(Me,"Do You want to Delete Selected Code Data.", "Delete Data", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='USERPWD' and TYPE='PWD'")
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = "USERPWD"
            pwd.strType = "PWD"
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                DeleteData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.U Then
            ' If clsCommon.MyMessageBoxShow(Me,"Do You want to Delete Selected Code Data.", "Delete Data", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            If clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal And UsLock1.Status = ERPTransactionStatus.Approved Then
                Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='USERPWD' and TYPE='PWD'")
                Dim pwd As New FrmPWD(Nothing)
                pwd.strCode = "USERPWD"
                pwd.strType = "PWD"
                pwd.ShowDialog()
                If pwd.isPasswordCorrect Then
                    SaveSRNData()
                    'Dim sQuery As String = "update tspl_milk_sample_Head set posted=0 where doc_code='" & txtCode.Value & "'"
                    'clsDBFuncationality.ExecuteNonQuery(sQuery)
                    'LoadData(txtCode.Value)
                End If
            End If
            'End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            BtnRead_Click("", e)
        ElseIf e.KeyCode = Keys.F8 Then
            If clsCommon.myCdbl(txtRangeFrom.Text) > 0 And clsCommon.myCdbl(TxtRange2.Text) > 0 Then
                If gv1.Rows.Count > 0 Then
                    Load_Receipt_From_Dt(True)
                End If
                'If gv1.Rows.Count <= 0 Then
                Load_Receipt_From_Dt(False)
                irowIndex_Global_gv1 = -1
                ' irowIndex_Global_gv2 = -1
                txtRangeFrom.ReadOnly = False ''Balwinder
                TxtRange2.ReadOnly = True
                'Else
                '    gv1.CurrentRow.Cells(colFAT).Value = objEco.FAT
                '    gv1.CurrentRow.Cells(colSNF).Value = objEco.SNF
                '    gv1.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                'End If
            End If
        ElseIf e.KeyCode = Keys.F2 Then
            If gv1.Rows.Count > 0 Then
                If irowIndex_Global_gv1 < 0 And (clsCommon.myCdbl(gv1.Rows(0).Cells(colSNF).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(0).Cells(colSNF).Value) > 0) Then
                    For Each row As GridViewRowInfo In gv1.Rows
                        If clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Or clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Then
                            irowIndex_Global_gv1 += 1
                        Else
                            Exit For
                        End If
                    Next
                End If
                'LblFAT.Text = "08.50"
                'LblSnf.Text = "05.50"
                ''gv1.CurrentRow.Cells(colFAT).Value = LblSnf.Text
                ''gv1.CurrentRow.Cells(colSNF).Value = LblFAT.Text
                'gv1.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                If gv1.Rows.Count > irowIndex_Global_gv1 Then
                    If irowIndex_Global_gv1 >= 0 Then
                        If clsCommon.myCdbl(LblSnf.Text) = clsCommon.myCdbl(gv1.Rows(irowIndex_Global_gv1).Cells(colFAT).Value) And clsCommon.myCdbl(LblFAT.Text) = clsCommon.myCdbl(gv1.Rows(irowIndex_Global_gv1).Cells(colSNF).Value) Then
                            If isStopForRepeatedFATSNF Then
                                Exit Sub
                            ElseIf clsCommon.MyMessageBoxShow(Me, "Previous Row have the same reading . Are you sure to Update this in this row ?", "Message", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                    End If
                    gv1.Rows(irowIndex_Global_gv1 + 1).Cells(colFAT).Value = clsCommon.myCdbl(LblSnf.Text)
                    gv1.Rows(irowIndex_Global_gv1 + 1).Cells(colSNF).Value = clsCommon.myCdbl(LblFAT.Text)
                    gv1.Rows(irowIndex_Global_gv1 + 1).Cells(COL_IS_MANUAL).Value = "Auto"
                    LblFAT.Text = "00.00"
                    LblSnf.Text = "00.00"
                    If irowIndex_Global_gv1 <> clsCommon.myCdbl(gv1.Rows.Count - 1) Then
                        If clsCommon.myCdbl(gv1.Rows(irowIndex_Global_gv1 + 1).Cells(colFAT).Value) > 0 And clsCommon.myCdbl(gv1.Rows(irowIndex_Global_gv1 + 1).Cells(colSNF).Value) > 0 Then
                            SaveData_Record(gv1)
                        End If
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.F12 Then
            If clsCommon.myCdbl(TxtRange1Panal2.Text) > 0 And clsCommon.myCdbl(Txtrange2panal2.Text) > 0 Then
                If gv1.Rows.Count > 0 Then
                    Load_Receipt_From_Dt_For_Eco_Pro_2(True)
                End If
                'If gv2.Rows.Count <= 0 Then
                Load_Receipt_From_Dt_For_Eco_Pro_2(False)
                TxtRange1Panal2.ReadOnly = False ''Balwinder
                Txtrange2panal2.ReadOnly = False ''Balwinder
                ' irowIndex_Global_gv1 = -1
                irowIndex_Global_gv2 = -1
                'Else
                '    gv2.CurrentRow.Cells(colFAT).Value = objEco.FAT
                '    gv2.CurrentRow.Cells(colSNF).Value = objEco.SNF
                '    gv2.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                'End If
            End If
        ElseIf e.KeyCode = Keys.F9 Then
            If gv2.Rows.Count > 0 Then
                'gv2.CurrentRow.Cells(colFAT).Value = LblFatpanel2.Text
                'gv2.CurrentRow.Cells(colSNF).Value = LblSNFPanel2.Text
                'gv2.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                'LblFatpanel2.Text = "00.00"
                'LblSNFPanel2.Text = "00.00"

                If gv2.Rows.Count > 0 Then
                    If irowIndex_Global_gv2 < 0 And (clsCommon.myCdbl(gv2.Rows(0).Cells(colSNF).Value) > 0 Or clsCommon.myCdbl(gv2.Rows(0).Cells(colSNF).Value) > 0) Then
                        For Each row As GridViewRowInfo In gv2.Rows
                            If clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Or clsCommon.myCdbl(row.Cells(colSNF).Value) > 0 Then
                                irowIndex_Global_gv2 += 1
                            Else
                                Exit For
                            End If
                        Next
                        'ElseIf irowIndex_Global_gv2 >= 0 Then
                        '    If clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2).Cells(colFAT).Value) > 0 Or clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2).Cells(colFAT).Value) > 0 Then
                        '        irowIndex_Global_gv2 = irowIndex_Global_gv2 + 1
                        '    End If
                    End If
                    'LblFatpanel2.Text = "08.50"
                    'LblSNFPanel2.Text = "05.50"
                    ''gv2.CurrentRow.Cells(colFAT).Value = LblSNFPanel2.Text
                    ''gv2.CurrentRow.Cells(colSNF).Value = LblFatpanel2.Text
                    'gv2.CurrentRow.Cells(COL_IS_MANUAL).Value = "Auto"
                    If gv2.Rows.Count > irowIndex_Global_gv2 Then
                        If irowIndex_Global_gv2 >= 0 Then
                            If clsCommon.myCdbl(LblSNFPanel2.Text) = clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2).Cells(colFAT).Value) And clsCommon.myCdbl(LblFatpanel2.Text) = clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2).Cells(colSNF).Value) Then
                                If isStopForRepeatedFATSNF Then
                                    Exit Sub
                                ElseIf clsCommon.MyMessageBoxShow(Me, "Previous Row have the same reading . Are you sure to Update this in this row ?", "Message", MessageBoxButtons.YesNo, RadMessageIcon.Question, MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
                                    Exit Sub
                                End If
                            End If
                        End If
                        gv2.Rows(irowIndex_Global_gv2 + 1).Cells(colFAT).Value = clsCommon.myCdbl(LblFatpanel2.Text)
                        gv2.Rows(irowIndex_Global_gv2 + 1).Cells(colSNF).Value = clsCommon.myCdbl(LblSNFPanel2.Text)
                        gv2.Rows(irowIndex_Global_gv2 + 1).Cells(COL_IS_MANUAL).Value = "Auto"
                        LblFatpanel2.Text = "00.00"
                        LblSNFPanel2.Text = "00.00"
                        If irowIndex_Global_gv2 <> clsCommon.myCdbl(gv2.Rows.Count - 1) Then
                            If clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2 + 1).Cells(colFAT).Value) > 0 And clsCommon.myCdbl(gv2.Rows(irowIndex_Global_gv2 + 1).Cells(colSNF).Value) > 0 Then
                                SaveData_Record(gv2)
                            End If
                        End If
                    End If
                End If
            End If
        ElseIf e.Control And e.KeyCode = Keys.R Then
            txtRangeFrom.ReadOnly = False
            TxtRange2.ReadOnly = False
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
                        sQuery = "update TSPL_MILK_RECEIPT_DETAIL set IS_SAMPLEED='F' where DOC_CODE='" & fndMCCCode.Value & "' and SAMPLE_NO=" & sampleNo & ""
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
                          "StopForRepeatedFATSNF" + Environment.NewLine +
                          "SampleFONTSize" + Environment.NewLine +
                          "AddValidationofMilkTypeinsample" + Environment.NewLine +
                          "PickPendingMilkSRNinNextPaymentCycle" + Environment.NewLine +
                          "AllowPurchaseAccounting (It will Effect All Purchase Transaction)" + Environment.NewLine +
                          "OpenODDEvenForm")
        End If
    End Sub

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal strMilkReceiptcode As String = "", Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            Dim objR As New Random
            'AddNew()
            'gv1.DataSource = Nothing
            'gv1.Rows.Clear()
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            fndMCCCode.Enabled = False
            If clsCommon.myLen(strMilkReceiptcode) <= 0 Then
                txtRangeFrom.Text = Nothing
                TxtRange2.Text = Nothing
                Dim obj As clsMilkSampleMCC = clsMilkSampleMCC.GetData(txtCode.Value, navType, trans)
                txtCode.Value = obj.DOC_CODE
                dtpDocDate.Value = obj.DOC_DATE
                btnsave.Text = "Update"
                fndMCCCode.Value = obj.MILK_RECEIPT_CODE
                fndMCCCode.Tag = obj.MCC_CODE
                cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type
                CboMachine.SelectedValue = clsMccMaster.DefaultSampleMachine(fndMCCCode.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
                cboComPort.SelectedValue = clsMccMaster.DefaultSampleComport(fndMCCCode.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
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
                If (clsMilkSampleMCC.ObjList IsNot Nothing AndAlso clsMilkSampleMCC.ObjList.Count > 0) Then
                    isInsideLoadData = True
                    For Each obj1 As clsMilkSampleMCCDetail In clsMilkSampleMCC.ObjList
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
                        If DisplayAllControls <> "False" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDEN).Value = objR.Next(1.028, 1.036)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colProtein).Value = objR.Next(3.02, 3.06)
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colAWM).Value = objR.Next(1, 10)
                        End If
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
                        gv1.Rows(gv1.Rows.Count - 1).Cells(COLAMOUNT).Value = obj1.AMOUNT
                        gv1.Rows(gv1.Rows.Count - 1).Cells(COL_IS_MANUAL).Value = obj1.Is_Entered_Manualy
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColUomCode).Value = obj1.UOM_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColPriceCode).Value = obj1.Price_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.SRN_CODE
                        cboECOPro.SelectedValue = IIf(obj1.Eco_Pro_Name = "", "0", obj1.Eco_Pro_Name)
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
                'txtCode.Text = obj.DOC_CODE
                'fndMCCCode.Value = obj.MCC_CODE
                LoadColumnReceiptDT()
                fndMCCCode.Tag = obj.MCC_CODE 'obj.DOC_CODE
                txtTotalQty.Text = 0
                txttotFAT.Text = 0
                txttotSNF.Text = 0
                cboShift.SelectedValue = obj.SHIFT
                dtpDocDate.MinDate = obj.DOC_DATE
                dtpDocDate.Value = obj.DOC_DATE
                'UsLock1.Status = obj.POSTED
                cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type
                If (clsMilkReceiptMCC.ObjList IsNot Nothing AndAlso clsMilkReceiptMCC.ObjList.Count > 0) Then
                    For Each obj1 As clsMilkReceiptMCCDetail In clsMilkReceiptMCC.ObjList
                        'gv1.Rows.AddNew()

                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNo).Value = obj1.SAMPLE_NO
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColVlcDocCode).Value = obj1.VLC_DOC_CODE
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColSampleNo).Value = obj1.SAMPLE_NO_VALUES
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkType).Value = obj1.MILK_TYPE
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColType).Value = obj1.TYPE
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColVspCode).Value = obj1.VSP_CODE
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColItemCode).Value = obj1.Item_CODE
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColQty).Value = obj1.MILK_WEIGHT
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = obj1.FAT
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = obj1.SNF
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colCOR).Value = 0
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = 0
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColRATE).Value = 0
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(COLAMOUNT).Value = 0
                        'gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = ""

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
                        'DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colCLR).Value = 0
                        'DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(ColRATE).Value = 0
                        'DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(COLAMOUNT).Value = 0
                        'DtMilkReceiptEcoProWise.Rows(DtMilkReceiptEcoProWise.Rows.Count - 1).Item(colSRN_CODE).Value = ""
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
        DtMilkReceiptEcoProWise.Columns.Add(colSRNo)

        DtMilkReceiptEcoProWise.Columns.Add(ColPriceCode)
    End Sub

    Sub Load_Receipt_From_Dt(ByVal IsReset As Boolean, Optional ByVal Fat_Per As Double = 0, Optional ByVal SNF_Per As Double = 0)
        Try
            LoadBlankGrid(gv1)
            If (clsCommon.myLen(cboECOPro.SelectedValue) <= 0 Or cboECOPro.SelectedValue = "0") And clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                If Not IsReset Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Eco Pro Machine First.", Me.Text)
                End If
                Exit Sub
            End If
            If Not IsReset Then LoadData("", fndMCCCode.Value)
            If DtMilkReceiptEcoProWise.Rows.Count > 0 Then
                Dim sQuery As String = ""
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If IsReset And btnsave.Text = "Save" Then
                    For Each row As DataRow In DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='" & cboECOPro.SelectedValue & "' and " & colSRNo & ">=" & clsCommon.myCdbl(txtRangeFrom.Text) & " and " & colSRNo & "<=" & clsCommon.myCdbl(TxtRange2.Text) & "")
                        sQuery = "update tspl_milk_receipt_detail set eco_pro_name=null where eco_pro_name='" & cboECOPro.SelectedValue & "' and DOC_CODE='" & fndMCCCode.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(row.Item(ColVlcDocCode)) & "' and SAMPLE_NO_VALUES='" & clsCommon.myCstr(row.Item(ColSampleNo)) & "' and coalesce(Is_Sampleed,'F')='F'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                Else
                    'For Each row As DataRow In DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='' and " & colSRNo & ">=" & clsCommon.myCdbl(txtRangeFrom.Text) & " and " & colSRNo & "<=" & clsCommon.myCdbl(TxtRange2.Text) & "", "ColSampleNo")
                    isCellValueChangedOpen = True
                    isInsideLoadData = True
                    For Each row As DataRow In DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='' and " & colSRNo & ">=" & clsCommon.myCdbl(txtRangeFrom.Text) & " and " & colSRNo & "<=" & clsCommon.myCdbl(TxtRange2.Text) & "", "SRNo")
                        gv1.Rows.AddNew()

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRNo).Value = row("" & colSRNo & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColVlcDocCode).Value = row("" & ColVlcDocCode & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColVlcCode).Value = row("" & ColVlcCode & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSampleNo).Value = row("" & ColSampleNo & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMilkType).Value = row("" & colMilkType & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColType).Value = row("" & ColType & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColVspCode).Value = row("" & ColVspCode & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColItemCode).Value = row("" & ColItemCode & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColQty).Value = row("" & ColQty & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColMILK_Qty).Value = row("" & ColMILK_Qty & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value = row("" & colFAT & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value = row("" & colSNF & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCOR).Value = row("" & colCOR & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColUomCode).Value = row("" & ColUomCode & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = 0
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRATE).Value = IIf(clsCommon.myCdbl(row("" & ColRATE & "")) > 0, clsCommon.myCdbl(row("" & ColRATE & "")), 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(COLAMOUNT).Value = IIf(clsCommon.myCdbl(row("" & COLAMOUNT & "")) > 0, clsCommon.myCdbl(row("" & COLAMOUNT & "")), 0)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = row("" & colSRN_CODE & "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColPriceCode).Value = row("" & ColPriceCode & "")
                        If clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).Value) > 0 And clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).Value) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF).ReadOnly = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColIS_Saved).Value = "T"
                        End If
                        row("" & colECOPro & "") = cboECOPro.SelectedValue
                        sQuery = "update tspl_milk_receipt_detail set eco_pro_name='" & cboECOPro.SelectedValue & "' where eco_pro_name is null and DOC_CODE='" & fndMCCCode.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(row.Item(ColVlcDocCode)) & "' and SAMPLE_NO_VALUES='" & clsCommon.myCstr(row.Item(ColSampleNo)) & "' and coalesce(Is_Sampleed,'F')='F'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                    isCellValueChangedOpen = False
                    isInsideLoadData = False
                    is_Manual = clsMilkSampleMCC.GetShiftisManual(fndMCCCode.Tag, trans)
                    If is_Manual = False Then
                        For Each col As GridViewColumn In gv1.Columns
                            col.ReadOnly = True
                        Next
                    Else
                        For Each col As GridViewColumn In gv1.Columns
                            If clsCommon.CompairString(col.Name, "ColFAT") = CompairStringResult.Equal Or clsCommon.CompairString(col.Name, "ColSNF") = CompairStringResult.Equal Then
                                For Each row As GridViewRowInfo In gv1.Rows
                                    If clsCommon.myCdbl(row.Cells(col.Name).Value) <= 0 Then
                                        col.ReadOnly = False
                                    End If
                                Next
                            End If
                        Next
                    End If
                    If Not IsReset And gv1.Rows.Count <= 0 And DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='' and " & colSRNo & ">='" & clsCommon.myCdbl(txtRangeFrom.Text) & "' and " & colSRNo & "<='" & clsCommon.myCdbl(TxtRange2.Text) & "'").Count <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "No data Found.", Me.Text)
                    End If
                End If

                trans.Commit()
                If gv1.Rows.Count > 0 Then
                    gv1.CurrentRow = gv1.Rows(0)
                End If
                TxtRange1Panal2.ReadOnly = False
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


        'Dim repoFAT As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoFAT = New GridViewDecimalColumn()
        'repoFAT.FormatString = ""
        'repoFAT.HeaderText = "FAT"
        'repoFAT.Name = colFAT
        'repoFAT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'repoFAT.ReadOnly = False
        'repoFAT.DecimalPlaces = 1
        'repoFAT.Width = 70
        'GGGrid.MasterTemplate.Columns.Add(repoFAT)

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
        If DisplayAllControls <> "False" Then
            Dim repoDEN As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoDEN = New GridViewDecimalColumn()
            repoDEN.FormatString = ""
            repoDEN.HeaderText = "DEN"
            repoDEN.Name = colDEN
            repoDEN.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoDEN.ReadOnly = False
            repoDEN.DecimalPlaces = 3
            repoDEN.Width = 70
            GGGrid.MasterTemplate.Columns.Add(repoDEN)

            Dim repoProtein As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoProtein = New GridViewDecimalColumn()
            repoProtein.FormatString = ""
            repoProtein.HeaderText = "Protein"
            repoProtein.Name = colProtein
            repoProtein.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoProtein.ReadOnly = False
            repoProtein.DecimalPlaces = 2
            repoProtein.Width = 70
            GGGrid.MasterTemplate.Columns.Add(repoProtein)

            Dim repoAWM As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoAWM = New GridViewDecimalColumn()
            repoAWM.FormatString = ""
            repoAWM.HeaderText = "AWM"
            repoAWM.Name = colAWM
            repoAWM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoAWM.ReadOnly = False
            repoAWM.Width = 70
            repoAWM.DecimalPlaces = 0
            GGGrid.MasterTemplate.Columns.Add(repoAWM)
        End If


        'Dim repoSNF As GridViewDecimalColumn = New GridViewDecimalColumn()
        'repoSNF = New GridViewDecimalColumn()
        'repoSNF.FormatString = ""
        'repoSNF.HeaderText = "SNF"
        'repoSNF.Name = colSNF
        'repoSNF.DecimalPlaces = 1
        'repoSNF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'repoSNF.ReadOnly = False
        'repoSNF.Width = 70
        'GGGrid.MasterTemplate.Columns.Add(repoSNF)

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
        'clsCustomFieldGrid.LoadBlankGrid(GGGrid, MyBase.ArrDetailFields)

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

        GGGrid.AllowDeleteRow = False
        GGGrid.AllowAddNewRow = False
        GGGrid.ShowGroupPanel = False
        GGGrid.AllowColumnReorder = True
        GGGrid.AllowRowReorder = False
        GGGrid.EnableSorting = False
        'GGGrid.EnableFiltering = True
        GGGrid.EnableAlternatingRowColor = False
        GGGrid.AutoSizeRows = False
        GGGrid.AllowRowResize = True
        GGGrid.VerticalScrollState = ScrollState.AlwaysShow
        GGGrid.HorizontalScrollState = ScrollState.AlwaysShow
        GGGrid.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GGGrid.MasterTemplate.ShowRowHeaderColumn = False
        GGGrid.TableElement.TableHeaderHeight = 40
        GGGrid.ShowFilteringRow = True
        ReStoreGridLayout()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Try
            Dim qry As String = "SELECT DOC_CODE as Code,DOC_DATE as Date,MCC_CODE as [Mcc Code] FROM TSPL_MILK_SAMPLE_HEAD"
            txtCode.Value = clsCommon.ShowSelectForm("MILK RECEIPT", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)


            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                ' gv1.Rows.Add(gv1.CurrentRow)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
        TxtRange1Panal2.Text = Nothing
        TxtRange2.Text = Nothing
        txtRangeFrom.Text = Nothing
        Txtrange2panal2.Text = Nothing
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
                        isCellValueChanged = True
                    End If
                    If isCellValueChanged And e.Column Is GGGrid.Columns(colSNF) Then
                        isCellValueChanged = False
                        GGGrid.Rows(irowIndex).Cells(colSNF).Value = Math.Truncate(clsCommon.myCdbl(GGGrid.Rows(irowIndex).Cells(colSNF).Value) * 10) / 10
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
        DtMilkReceipt = clsDBFuncationality.GetDataTable("select rd.*,rh.*,Case when Nature='C' then Actual_charges end as  commision_pers," _
              & " Case when Nature='E' then Actual_charges end as payment_commision_pers,Service_Charge_Type,coalesce(Rate_Head_Load,0) as Rate_Head_Load" _
              & " ,coalesce(Rate_Own_Asset,0) as Rate_Own_Asset,Service_Basis_Head_Load,Service_Basis_Own_Asset,TSPL_VENDOR_MASTER.Service_Charge_Per_Unit,TSPL_VENDOR_MASTER.DistanceKM_Head_Load 
              from TSPL_MILK_RECEIPT_HEAD rd 
              Inner join TSPL_MILK_RECEIPT_DETAIL rh on rh.DOC_CODE=" _
              & " rd.DOC_CODE left join TSPL_VENDOR_MASTER on   Vendor_Code=VSP_CODE where rd.doc_code='" & fndMCCCode.Value & "'")
        DtVehicle = clsDBFuncationality.GetDataTable("SELECT vm.* FROM  TSPL_Primary_Vehicle_Master vm ")
        DtVSPChargeDetail = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_MCC_VSP_ChargeCategory_MAPPING ")
        DtPriceChargeDetail = clsDBFuncationality.GetDataTable("SELECT * FROM  TSPL_FAT_SNF_UPLOADER_Chart_Detail ")
    End Sub

    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If Not clsMCCMaterialSale.checkApprovalDocument("M-RECEIPT", fndMCCCode.Value) Then
                clsCommon.MyMessageBoxShow(Me, "Approval required for this Document Milk Receipt [" & fndMCCCode.Value & "]")
                Exit Sub
            End If
            If (myMessages.postConfirm()) Then
                ' SaveData()
                ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If SaveData(gv1, True) Then
                    LoadDt()
                    If SaveSRNData() Then
                        If clsMilkSRNMCC.ChecksavedTransaction(txtCode.Value) Then
                            If (clsMilkSampleMCC.PostData(txtCode.Value, True)) Then
                                clsCommon.ProgressBarHide()
                                msg = "Successfully Posted"
                                common.clsCommon.MyMessageBoxShow(Me, msg)
                                UsLock1.Status = ERPTransactionStatus.Approved
                                gv1.DataSource = Nothing
                                gv1.Rows.Clear()
                                LoadData(txtCode.Value)
                            End If
                        End If
                    Else
                        '        qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                        '        dt = clsDBFuncationality.GetDataTable(qry)
                        '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        '            Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        '            Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        '            If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                        '                msg = "Level 1 Approval done. "
                        '                If NoOflevel = 1 Then
                        '                    msg += "Successfully Posted. "
                        '                Else
                        '                    msg += "Level 2 Approval Required."
                        '                End If
                        '            ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                        '                msg = "Level 2 Approval done. "
                        '                If NoOflevel = 2 Then
                        '                    msg += "Successfully Posted "
                        '                Else
                        '                    msg += "Level 3 Approval Required."
                        '                End If
                        '            Else
                        '                msg = "Level 3 Approval done. Successfully Posted. "
                        '            End If
                        '        End If
                    End If
                End If
                'common.clsCommon.MyMessageBoxShow(Me,msg)

                'If (common.clsCommon.MyMessageBoxShow(Me,"Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                '    PrintDataNew()
                'End If
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            LoadData(txtCode.Value)
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub fndMCCCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        Try
            If clsCommon.myLen(cboECOPro.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Eco Pro Machine First.", Me.Text)
                Exit Sub
            End If
            Dim sQuery As String = "select distinct TSPL_MILK_RECEIPT_HEAD.DOC_CODE as [Code],TSPL_MILK_RECEIPT_HEAD.MCC_CODE as [MCC Code],TSPL_MILK_RECEIPT_HEAD.DOC_DATE as [Date],case when TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 'Morning' " _
            & "  else 'Evening' end as [Shift],TOTAL_WEIGHT as [Total Qty] from TSPL_MILK_RECEIPT_HEAD Left join TSPL_MILK_SAMPLE_HEAD on Milk_Receipt_Code=TSPL_MILK_RECEIPT_HEAD.Doc_Code  "
            fndMCCCode.Value = clsCommon.ShowSelectForm("MRFND", sQuery, "Code", " TSPL_MILK_RECEIPT_HEAD.Doc_Code in (select Distinct Doc_Code from tspl_Milk_receipt_Detail where coalesce(is_Sampleed,'F')='F') ", "", "Code", isButtonClicked)
            If isUsedEcoPro(cboECOPro) Then
                If clsCommon.myLen(fndMCCCode.Value) > 0 Then
                    LoadData("", fndMCCCode.Value)
                    txtRangeFrom.Text = 1
                    TxtRange2.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
                    CboMachine.SelectedValue = clsMccMaster.DefaultSampleMachine(fndMCCCode.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, , , NavType)
            txtRangeFrom.Text = SetFromRange(txtCode.Value, Nothing) '1 'clsCommon.myCdbl(gv1.Rows.Count) + 1
            TxtRange2.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub txtCode__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim sQuery As String = "select * from (select tspl_milk_Sample_Head.Doc_code as [Code],Mcc_Code,Milk_Receipt_Code as [Milk Receipt Code],convert(varchar,Doc_date,103) as Date,Shift,SUM(Qty) as [Total Qty],SUM(FAT_KG) as [Total FAT] ,SUM(SNF_KG) as [Total SNF] from tspl_milk_Sample_Head left join TSPL_MILK_SAMPLE_DETAIL on tspl_milk_Sample_Head.doc_code=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE group by tspl_milk_Sample_Head.Doc_code,Mcc_Code,Milk_Receipt_Code,Doc_date,Shift) tt "
            txtCode.Value = clsCommon.ShowSelectForm("MRFND", sQuery, "Code", "", txtCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                txtRangeFrom.Text = SetFromRange(txtCode.Value, Nothing) '1 'clsCommon.myCdbl(gv1.Rows.Count) + 1
                TxtRange2.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
            End If
            sQuery = String.Empty
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub BtnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRead.Click
        Try
            If gv1.Rows.Count > 0 Then
                objEco.getDataMachineWise(cboComPort.Text, IIf(LCase(CboMachine.Text) = "kanha", "K", IIf(LCase(CboMachine.Text) = "everest new", "N", "E")))
                LblSnf.Text = clsEkoPro.FAT
                LblFAT.Text = clsEkoPro.SNF
                'If LblFAT.Text > 0 Or LblSnf.Text > 0 Then
                '    Timer1.Stop()
                '    btnsave.Text = "Save"
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSavelayout.Click
        '    If clsCommon.myLen(GetReportID()) > 0 Then
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
        'End If
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeletelayout.Click
        clsGridLayout.DeleteData("MilkSampleGrid", objCommonVar.CurrentUserCode)
        clsCommon.MyMessageBoxShow(Me, "Layout Deleted Successfully.", Me.Text)
        LoadData(txtCode.Value)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkSampleGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Function isUsedEcoPro(ByVal cmb As common.Controls.MyComboBox)
        'If clsCommon.myLen(cmb.SelectedValue) > 0 And cmb.SelectedValue <> "0" Then 'And clsCommon.myLen(fndMCCCode.Tag) > 0 And clsCommon.myLen(cboShift.SelectedValue) > 0 Then
        '    Dim sQuery As String = "select count(*) from tspl_Milk_receipt_detail where eco_pro_name='" & cmb.SelectedValue & "' and Doc_code='" & fndMCCCode.Value & "' and coalesce(is_sampleed,'F')='F'"
        '    Dim isUsingSameEcoPro As Integer = clsDBFuncationality.getSingleValue(sQuery)
        '    If isUsingSameEcoPro > 0 Then
        '        clsCommon.MyMessageBoxShow(Me,"This Eco Pro is Used and can not be Use Again.")
        '        cmb.SelectedValue = Nothing
        '        fndMCCCode.Value = Nothing
        '        Return False
        '    End If
        'End If
        Return True
    End Function

    Private Sub cboECOPro_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboECOPro.SelectedValueChanged
        Try
            isUsedEcoPro(cboECOPro)
            If CboEcoPro2.Items.Contains(cboECOPro.SelectedValue.ToString) Then
                GetECOPro(CboEcoPro2)
                CboEcoPro2.Items.Remove(cboECOPro.SelectedValue)
            End If
        Catch ex As Exception
            ' clsCommon.MyMessageBoxShow(Me,ex.ToString, "cboECOPro_SelectedValueChanged")
        End Try
    End Sub

    Public Function SetFromRange(ByVal doc_code As String, ByVal trans As SqlTransaction)
        Dim sQuery As String = "select count(*) from tspl_milk_sample_detail where doc_code='" & doc_code & "'"
        Dim range_from As Integer = clsDBFuncationality.getSingleValue(sQuery, trans)
        Return range_from
    End Function

    Private Sub txtRangeFrom_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRangeFrom.Leave
        Try
            TxtRange2.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
            If clsCommon.myCdbl(TxtRange1Panal2.Text) > 0 Then
                If (clsCommon.myCdbl(txtRangeFrom.Text) <> (clsCommon.myCdbl(Txtrange2panal2.Text) + 1)) And (clsCommon.myCdbl(Txtrange2panal2.Text) <> (clsCommon.myCdbl(TxtRange2.Text) + 1)) Then
                    txtRangeFrom.Text = 0
                    clsCommon.MyMessageBoxShow(Me, "Please Fill correct range .it should be " & (clsCommon.myCdbl(Txtrange2panal2.Text) + 1))

                    'txtRangeFrom.Text = (clsCommon.myCdbl(Txtrange2panal2.Text) + 1)
                    'TxtRange2.Text = -1 + clsCommon.myCdbl(txtRangeFrom.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, "txtRangeFrom_Leave")
        End Try
    End Sub

    Private Sub Timer1_Start()
        Try
            'If gv1.Rows.Count > 0 Then
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
                Timer1.Enabled = False
                'setSettings()
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
                LblFAT.Text = "00.00"
                LblSnf.Text = "00.00"
                '_objComport.Close()
                objSr.ClosePort()
                objSr2.isEco2 = False
                Timer1.Enabled = False
                frmMilkSampleMCC.isPortOpened = False
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

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        SplitContainer2.Panel2Collapsed = False
        GrpEcoPro2.Enabled = True
        DemoGrp2.Enabled = True
        CboMachine.SelectedValue = clsMccMaster.DefaultSampleMachine(fndMCCCode.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
        cboComPort.SelectedValue = clsMccMaster.DefaultSampleComport(fndMCCCode.Tag, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
        TxtRange2.ReadOnly = False
        txtRangeFrom.ReadOnly = False
        TxtRange1Panal2.ReadOnly = False
        Txtrange2panal2.ReadOnly = False
    End Sub

    Private Sub BtnStart2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnStart2.Click
        Try
            If clsCommon.CompairString(BtnStart2.Text, "Start") = CompairStringResult.Equal Then
                If clsCommon.CompairString(BtnStart.Text, "Stop") = CompairStringResult.Equal And clsCommon.CompairString(cboComPort.Text, CboComport2.Text) = CompairStringResult.Equal Then
                    Throw New Exception("This port is in use. choose another port")
                End If
                'setSettings2()
                Timer2_Start()
                CboComport2.Enabled = False
                CboMachine2.Enabled = False
                CboEcoPro2.Enabled = False
                BtnStart2.Text = "Stop"
                Timer2.Enabled = False
                frmMilkSampleMCC.isPortOpened = True
            Else
                BtnStart2.Text = "Start"
                CboComport2.Enabled = True
                CboMachine2.Enabled = True
                CboEcoPro2.Enabled = True
                LblFatpanel2.Text = "00.00"
                LblSNFPanel2.Text = "00.00"
                objSr2.ClosePort()
                frmMilkSampleMCC.isPortOpened = False
            End If
        Catch ex As Exception
            CboComport2.Enabled = True
            CboMachine2.Enabled = True
            CboEcoPro2.Enabled = True
            BtnStart2.Text = "Start"
            LblFatpanel2.Text = "00.00"
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

    Sub Load_Receipt_From_Dt_For_Eco_Pro_2(ByVal IsReset As Boolean)
        Try
            LoadBlankGrid(gv2)
            If (clsCommon.myLen(CboEcoPro2.SelectedValue) <= 0 Or CboEcoPro2.SelectedValue = "0") And clsCommon.myLen(fndMCCCode.Value) <= 0 Then
                If Not IsReset Then
                    clsCommon.MyMessageBoxShow(Me, "Please Select Eco Pro Machine First.", Me.Text)
                End If
                Exit Sub
            End If
            If Not IsReset Then LoadData("", fndMCCCode.Value)
            If DtMilkReceiptEcoProWise.Rows.Count > 0 Then
                Dim sQuery As String = ""
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If IsReset And btnsave.Text = "Save" Then
                    For Each row As DataRow In DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='" & CboEcoPro2.SelectedValue & "' and " & colSRNo & ">=" & clsCommon.myCdbl(TxtRange1Panal2.Text) & " and " & colSRNo & "<=" & clsCommon.myCdbl(Txtrange2panal2.Text) & "", "SRNo")
                        sQuery = "update tspl_milk_receipt_detail set eco_pro_name=null where eco_pro_name='" & cboECOPro.SelectedValue & "' and DOC_CODE='" & fndMCCCode.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(row.Item(ColVlcDocCode)) & "' and SAMPLE_NO_VALUES='" & clsCommon.myCstr(row.Item(ColSampleNo)) & "' and coalesce(Is_Sampleed,'F')='F'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                Else
                    isCellValueChangedOpen = True
                    isInsideLoadData = True
                    For Each row As DataRow In DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='' and " & colSRNo & ">=" & clsCommon.myCdbl(TxtRange1Panal2.Text) & " and " & colSRNo & "<=" & clsCommon.myCdbl(Txtrange2panal2.Text) & "")
                        gv2.Rows.AddNew()

                        gv2.Rows(gv2.Rows.Count - 1).Cells(colSRNo).Value = row("" & colSRNo & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColVlcDocCode).Value = row("" & ColVlcDocCode & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColVlcCode).Value = row("" & ColVlcCode & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColSampleNo).Value = row("" & ColSampleNo & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colMilkType).Value = row("" & colMilkType & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColType).Value = row("" & ColType & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColVspCode).Value = row("" & ColVspCode & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColItemCode).Value = row("" & ColItemCode & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColQty).Value = row("" & ColQty & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColMILK_Qty).Value = row("" & ColMILK_Qty & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colFAT).Value = row("" & colFAT & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colSNF).Value = row("" & colSNF & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colCOR).Value = row("" & colCOR & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColUomCode).Value = row("" & ColUomCode & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colCLR).Value = 0
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColRATE).Value = 0
                        gv2.Rows(gv2.Rows.Count - 1).Cells(COLAMOUNT).Value = 0
                        gv2.Rows(gv2.Rows.Count - 1).Cells(colSRN_CODE).Value = row("" & colSRN_CODE & "")
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColPriceCode).Value = row("" & ColPriceCode & "")
                        row("" & colECOPro & "") = CboEcoPro2.SelectedValue
                        sQuery = "update tspl_milk_receipt_detail set eco_pro_name='" & CboEcoPro2.SelectedValue & "' where eco_pro_name is null and DOC_CODE='" & fndMCCCode.Value & "' and vlc_DOC_Code='" & clsCommon.myCstr(row.Item(ColVlcDocCode)) & "' and SAMPLE_NO_VALUES='" & clsCommon.myCstr(row.Item(ColSampleNo)) & "' and coalesce(Is_Sampleed,'F')='F'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                    isCellValueChangedOpen = False
                    isInsideLoadData = False
                    is_Manual = clsMilkSampleMCC.GetShiftisManual(fndMCCCode.Tag, trans)
                    If is_Manual = False Then
                        For Each col As GridViewColumn In gv2.Columns
                            col.ReadOnly = True
                        Next
                    End If
                    txtRangeFrom.ReadOnly = False
                    If Not IsReset And gv2.Rows.Count <= 0 And DtMilkReceiptEcoProWise.Select("" & colECOPro & " ='' and " & colSRNo & ">='" & clsCommon.myCdbl(txtRangeFrom.Text) & "' and " & colSRNo & "<='" & clsCommon.myCdbl(TxtRange2.Text) & "'").Count <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "No data Found.", Me.Text)
                    End If
                End If

                trans.Commit()
                If gv2.Rows.Count > 0 Then
                    gv2.CurrentRow = gv2.Rows(0)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString)
        End Try
    End Sub

    Private Sub CboEcoPro2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboEcoPro2.SelectedValueChanged
        Try
            isUsedEcoPro(CboEcoPro2)
        Catch ex As Exception
            ' clsCommon.MyMessageBoxShow(Me,ex.ToString, "cboECOPro_SelectedValueChanged")
        End Try
    End Sub

    Private Sub TxtRange1Panal2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRange1Panal2.Leave
        Try
            Txtrange2panal2.Text = -1 + clsCommon.myCdbl(TxtRange1Panal2.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing))
            If (clsCommon.myCdbl(TxtRange1Panal2.Text) <> (clsCommon.myCdbl(TxtRange2.Text) + 1)) And (clsCommon.myCdbl(TxtRange2.Text) <> (clsCommon.myCdbl(Txtrange2panal2.Text) + 1)) Then
                TxtRange1Panal2.Text = 0
                clsCommon.MyMessageBoxShow(Me, "Please Fill correct range .it should be " & (clsCommon.myCdbl(TxtRange2.Text) + 1))
            End If
            TxtRange2.ReadOnly = True
            txtRangeFrom.ReadOnly = False ''Balwinder
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, "txtRangeFrom_Leave")
        End Try
    End Sub

    Private Sub Timer2_Start()
        Try
            'If gv2.Rows.Count > 0 Then
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
            objSr2._LblFat = LblFatpanel2
            objSr2._LblSNF = LblSNFPanel2
            objSr2._LblWeight = Nothing
            objSr2.isEco2 = True
            objSr2.MachineName = clsPortSetting.getMachineMakePrefix(CboMachine2.Text, 0)
            objSr2.OpenPort()
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Txtrange2panal2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txtrange2panal2.Leave
        'Load_Receipt_From_Dt_For_Eco_Pro_2(True)
    End Sub

    Private Sub TxtRange2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRange2.Leave
        'Load_Receipt_From_Dt(True)
    End Sub

    Private Sub BtnCloseEcoPro2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCloseEcoPro2.Click
        Load_Receipt_From_Dt_For_Eco_Pro_2(True)
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        SplitContainer2.Panel2Collapsed = True
        GrpEcoPro2.Enabled = False
        DemoGrp2.Enabled = False
        CboEcoPro2.SelectedValue = 0
        TxtRange1Panal2.Text = 0
        Txtrange2panal2.Text = 0
        BtnStart2.Text = "Start"
    End Sub

    Private Sub BtnSave2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave2.Click
        If SaveData(gv2, False) Then
            clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            'LoadData(txtCode.Value)
        End If
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select '1' as [Sample No],'5.5' as Fat,'8.5' as SNF,'MCUP00001' as [MCC Code],'01-Nov-2014' as Date,'Morning' as Shift"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        'Dim IsNewEntry As Boolean
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
                    Me.fndMCCCode.Value = clsMilkReceiptMCC.GetDocCode(strDate, strMCCode, strShift)
                    'Dim GetDocArray As ArrayList = clsMilkSampleMCC.GetDocArray(strDate, strMCCode, strShift)
                    'If Not IsNothing(GetDocArray) Then
                    '    For Each Str As String In GetDocArray
                    '        Dim sQuery As String = "select * from tspl_milk_sample_detail where doc_code='" & Str & "' and sample_no='" & strCode & "'"
                    '        Dim Dtsample As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    '        If Dtsample.Rows.Count > 0 Then
                    '            GoTo a
                    '        End If
                    '    Next
                    'End If
                    Dim IssampleExits As Boolean = clsMilkSampleMCC.GetDocArray(strDate, strMCCode, strShift, strCode)
                    If IssampleExits = True Then
                        GoTo a
                    End If
                    isInsideImportData = True
                    If clsCommon.myLen(Me.fndMCCCode.Value) > 0 Then
                        txtRangeFrom.Text = strCode
                        TxtRange2.Text = strCode
                        'SendKeys.Send("{F8}")
                        If clsCommon.myCdbl(txtRangeFrom.Text) > 0 And clsCommon.myCdbl(TxtRange2.Text) > 0 Then
                            If gv1.Rows.Count > 0 Then
                                Load_Receipt_From_Dt(True)
                            End If

                            Load_Receipt_From_Dt(False)

                        End If
                        isInsideLoadData = False

                        If gv1.Rows.Count > 0 Then
                            For Each row As GridViewRowInfo In gv1.Rows
                                If row.Cells(colSRNo).Value = strCode Then
                                    row.Cells(colFAT).Value = Math.Truncate(strFat * 10) / 10
                                    row.Cells(colSNF).Value = Math.Truncate(strSNF * 10) / 10
                                    row.Cells(colCLR).Value = clsEkoPro.getClrOnCalculation(clsCommon.myCdbl(row.Cells(colFAT).Value), clsCommon.myCdbl(row.Cells(colSNF).Value), clsCommon.myCdbl(row.Cells(colCOR).Value))
                                    row.Cells(ColRATE).Value = clsEkoPro.getRateFromUploaderShiftWise(clsCommon.myCdbl(row.Cells(colFAT).Value), clsCommon.myCdbl(row.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(row.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
                                    row.Cells(ColPriceCode).Value = clsEkoPro.getPriceCodeFromUploaderShiftwise(clsCommon.myCdbl(row.Cells(colFAT).Value), clsCommon.myCdbl(row.Cells(colSNF).Value), clsCommon.myCstr(fndMCCCode.Tag), clsCommon.myCstr(row.Cells(ColVlcCode).Value), IIf(cboShift.SelectedValue.ToString.Contains("M"), "M", "E"), clsCommon.myCDate(dtpDocDate.Value), Nothing, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
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

                        'For Each groww As GridViewRowInfo In gv1.Rows

                        'Next
                        txtTotalQty.Text = totqty
                        txttotSNF.Text = totsnf
                        txttotFAT.Text = totfat
                        isInsideLoadData = True

                    Else
                        Throw New Exception("No Receipt Found of Selected MCC,date and Shift.")
                    End If
                    If issaved = True Then
                        SaveData_Record(gv1)
                        'clsCommon.ProgressBarPercentShow()
                        '  clsCommon.ProgressBarPercentUpdate(counter & " /" & gv.Rows.Count, "Loaded Records")
                        clsCommon.ProgressBarUpdate(counter & "/" & gv.Rows.Count)
                        'Dim sQuery As String = "update tspl_milk_sample_head set Total_Qty=(select SUM(Qty)  from TSPL_MILK_SAMPLE_DETAIL tt where " _
                        '& " tt.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE group by tt.DOC_CODE),  Total_FAT=(select SUM(FAT_KG)  from TSPL_MILK_SAMPLE_DETAIL " _
                        '& " tt where tt.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE group by tt.DOC_CODE), Total_SNF=(select SUM(Qty)  from TSPL_MILK_SAMPLE_DETAIL tt" _
                        '& " where tt.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE group by tt.DOC_CODE)"""
                        'clsDBFuncationality.ExecuteNonQuery(sQuery)
                        isInsideImportData = False
                    Else
                        isInsideImportData = False
                        Throw New Exception("No Receipt Found of Selected MCC,date and Shift and Sample No [" & strCode & "]")
                    End If
a:              Next
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
End Class
