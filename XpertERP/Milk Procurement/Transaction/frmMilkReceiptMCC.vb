'SWA/16/11/18-000060 by balwinder on 16/11/2018 its not a coding related issue.
Imports System.Data.SqlClient
Imports System.IO
Imports common

Public Class frmMilkReceiptMCC
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Irregular_Mcc_Code As String = String.Empty
    Dim IsAllowSaveWithoutShifttime As Boolean = False
    Dim WeighmentNotMandatoryInMCC As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const colCode As String = "COLCODE"
    Const colEmpCode As String = "colEmpCode"
    Const colCustCode As String = "colCustCode"
    Const colCustDesc As String = "colCustDesc"
    Const colProjectCode As String = "colProjectCode"
    Const colProjectDesc As String = "COLPROJECTDESC"
    Const colTaskDate As String = "COLTASKDATE"
    Const colFromTime As String = "colFromTime"
    Const colToTime As String = "colToTime"
    Const colWorkHours As String = "colWorkHours"
    Const colWorkDone As String = "colWorkDone"
    Const colJobCode As String = "colJobCode"
    Const colTaskCode As String = "colTaskCode"
    Const colUnitCost As String = "colUnitCost"
    Const colTotalCost As String = "colTotalCost"
    Const colBillingRate As String = "colBillingRate"
    Const colTotalBilling As String = "colTotalBilling"
    Const colisEdited As String = "colisEdited"

    Const colUOM As String = "colUOM"
    Const colConversion As String = "colConversion"
    Const colChanged_Qty As String = "colChanged_Qty"


    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim is_Entered_Manually As Boolean = False
    Public DtMilkReceipt As DataTable
    Dim objSr As New clsWeighingMachine

    Dim frmRange As Integer = 0
    Dim DtSample As New DataTable
    Dim MilkWeight_Setting As Double
    Dim Tolerance_Neg_Setting As Double
    Dim Tolerance_Positive_Setting As Double
    Public Shared isPortOpened As Boolean = False
    Dim ShiftTiming As String = String.Empty
    Private GridFontSize As Integer = 0
    Private GridFont As Font
    Dim ApplyWeightTolerance As Boolean = False
    Dim WeightToleranceValue As Decimal = 0
    Dim MinimumReachedWeightValue As Decimal = 0
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
    Dim SettStopUpdateForWeigingMilkReceipt As Boolean = False
#End Region

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objCommonVar.RefreshCommonVar()
        SplitContainer2.Panel1Collapsed = True
        SetUserMgmtNew()
        WeighmentNotMandatoryInMCC = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.WeighmentNotMandatoryInMCC, clsFixedParameterCode.WeighmentNotMandatoryInMCC, Nothing)) = 1)
        SettStopUpdateForWeigingMilkReceipt = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StopUpdateForWeigingMilkReceipt, clsFixedParameterCode.StopUpdateForWeigingMilkReceipt, Nothing)) = 1)
        BtnExportImport.Visible = True
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
        GridFontSize = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SampleFONTSize, clsFixedParameterCode.SampleFONTSize, Nothing)) ''Make Setting Balwinder
        If GridFontSize < 8 Then
            GridFontSize = 15
        End If
        GridFont = New Font("Ariel", GridFontSize)
        ''End of For Custom Fields
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            lblVehicle.Visible = True
            txtVehicle.Visible = True
        Else
            lblVehicle.Visible = False
            txtVehicle.Visible = False
        End If

        AddNew()
        dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Dim viewMilkReceiptSample As Boolean = (clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select View_Milk_Receipt_Sample from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' ")) = 1)
        Me.fndMCCCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCCode.Value + "' "))
        If clsCommon.myLen(fndMCCCode.Value) <= 0 Then
            If Not viewMilkReceiptSample Then
                Throw New Exception("Please set Default location of current user")
            End If
        End If
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowEditRow = False
        gv1.MasterTemplate.AllowCellContextMenu = True
        gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv1.MasterTemplate.AllowDeleteRow = False
        GetshiftType()
        GetMilkCondition()
        cboDockCollectionMilkType.DataSource = clsMilkReceiptMCC.GetDockCollectionMilkType(True)
        cboDockCollectionMilkType.ValueMember = "Code"
        cboDockCollectionMilkType.DisplayMember = "Name"


        cboMilkType.SelectedValue = "Good"
        txtMilkWeight.Enabled = False
        If clsCommon.myLen(fndMCCCode.Value) > 0 Then
            Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMCCCode.Value)
            If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                Throw New Exception("No shift is opened. one Shift Must be Opened..")
            ElseIf DTShift.Rows.Count > 1 Then
                Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
            Else
                dtpDocDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
                cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))
                LblUom.Text = clsCommon.myCstr(DTShift.Rows(0).Item("Uom_Code"))
                If clsCommon.myCstr(DTShift.Rows(0).Item("Is_Allow_Manual_Entry_Weighment")) = "T" Then
                    txtMilkWeight.Enabled = True
                    is_Entered_Manually = True
                Else
                    is_Entered_Manually = False
                End If
                Irregular_Mcc_Code = clsCommon.myCstr(DTShift.Rows(0).Item("Irregular_Mcc_Code"))
                dtpDocDate.ReadOnly = True
                cboShift.Enabled = False
                SetDocKCollectionMilkType(fndMCCCode.Value)
            End If
        End If


        ReStoreGridLayout()
        frmRange = 0
        UcWeighing1.LoadPortAndMachine()
        UcWeighing1.Machine = clsMccMaster.DefaultWeighingMachine(fndMCCCode.Value, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)
        UcWeighing1.Port = clsMccMaster.DefaultWeighingComport(fndMCCCode.Value, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), Nothing)

        UcWeighing1.form_ID = MyBase.Form_ID
        UcWeighing1.LoadSettingAndStart()
        UcWeighing1.isSkipHighSecurity = True

        MilkWeight_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
        Tolerance_Neg_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Tolerance_Neg, clsFixedParameterCode.MilkSetting, Nothing)
        Tolerance_Positive_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Tolerance_Positive, clsFixedParameterCode.MilkSetting, Nothing)
        txtCode.Value = clsMilkReceiptMCC.GetDocCode(clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy"), fndMCCCode.Value, cboShift.SelectedValue, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue), lblDockCode.Text)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value)
            TxtFrmRange.Text = clsCommon.myCdbl(gv1.Rows.Count) + 1
            TxtToRange.Text = -1 + clsCommon.myCdbl(TxtFrmRange.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCReceiptRange, clsFixedParameterCode.MilkSetting, Nothing))
            frmRange = clsCommon.myCdbl(TxtFrmRange.Text)
        Else
            TxtFrmRange.Text = 1
            TxtToRange.Text = -1 + clsCommon.myCdbl(TxtFrmRange.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCReceiptRange, clsFixedParameterCode.MilkSetting, Nothing))
            frmRange = clsCommon.myCdbl(TxtFrmRange.Text)
        End If
        fndRouteCode.Focus()

        ShiftTiming = clsFixedParameter.GetData(clsFixedParameterType.ShiftTiming, clsFixedParameterCode.ShiftTiming, Nothing)
        cboType.DataSource = clsMilkReceiptMCC.GetMilkType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
        cboType.SelectedValue = "M"
        If objCommonVar.DisplayTypeInMilkReceipt Then
            cboType.Visible = True
            Lbl_Type.Visible = True
        End If
    End Sub

    Private Sub GetMilkCondition()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = "Good"
        dr("Name") = "Good"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "OK"
        dr("Name") = "OK"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Rejected"
        dr("Name") = "Rejected"
        dt.Rows.Add(dr)

        cboMilkType.DataSource = dt
        cboMilkType.ValueMember = "Code"
        cboMilkType.DisplayMember = "Name"

    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = False
    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim objHead As clsMilkReceiptMCC
                Dim conv_fac As Double
                Dim str As String = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & fndMCCCode.Value & "' "
                Dim Unit_Code As String = clsDBFuncationality.getSingleValue(str)
                If Unit_Code = "" Then
                    clsCommon.MyMessageBoxShow(Me, "Fill UOM of Current Mcc", Me.Text)
                    Exit Sub
                End If
                str = "select UOM_Code from TSPL_Item_UOM_DETAIL where Item_CODE='" & fndItem_Code.Text & "' and UOM_code='" & Unit_Code & "' "
                Dim Item_Unit_Code As String = clsDBFuncationality.getSingleValue(str)
                If Item_Unit_Code = "" Then
                    clsCommon.MyMessageBoxShow("Fill " & Unit_Code & " UOM of Current Item")
                    Exit Sub
                End If
                If Not clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
                    str = "select DOC_CODE from TSPL_MILK_SAMPLE_HEAD where MILK_RECEIPT_CODE='" + txtCode.Value + "' and Posted=1"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        clsCommon.MyMessageBoxShow("Sample No[" + clsCommon.myCstr(dt.Rows(0)(0)) + "] is Posted.Cannot add More milk receipts")
                        Exit Sub
                    End If
                End If
                Dim totqty As Double = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    totqty += grow.Cells("MILK WEIGHT").Value
                Next
                txtTotalWeight.Text = Math.Round(totqty, 2)

                objHead = New clsMilkReceiptMCC
                objHead.DOCK_Code = lblDockCode.Text
                objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
                objHead.COMM_PORT = UcWeighing1.Port
                objHead.MCC_CODE = clsCommon.myCstr(fndMCCCode.Value)
                objHead.Irregular_MCC_CODE = clsCommon.myCstr(Irregular_Mcc_Code)
                objHead.TOTAL_WEIGHT = clsCommon.myCdbl(txtTotalWeight.Text) + IIf(btnsave.Text = "Update", clsCommon.myCdbl(txtMilkWeight.Text) - clsCommon.myCdbl(txtMilkWeight.Tag), clsCommon.myCdbl(txtMilkWeight.Text))
                objHead.Dock_Collection_Milk_Type = clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue)
                Dim objList As New List(Of clsMilkReceiptMCCDetail)

                Dim obj As New clsMilkReceiptMCCDetail()
                obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)

                obj.VLC_DOC_CODE = IIf(IsNothing(lblVLCCode.Tag), "", lblVLCCode.Tag)
                If Not btnsave.Text = "Update" Then
                    obj.SAMPLE_NO = frmRange
                End If
                obj.VLC_CODE = clsCommon.myCstr(fndVLCCode.Tag)
                obj.ROUTE_CODE = clsCommon.myCstr(fndRouteCode.Value)
                obj.VSP_CODE = clsCommon.myCstr(fndVspCode.Text)
                obj.Item_CODE = clsCommon.myCstr(fndItem_Code.Text)
                obj.VEHICLE_CODE = clsCommon.myCstr(fndVehicleCode.Text)
                obj.Other_VEHICLE = IIf(clsCommon.myCBool(chkOther.Checked) = True, "T", "F")
                obj.Other_VLC = IIf(clsCommon.myCBool(ChkALLVLC.Checked) = True, 1, 0)
                obj.NO_OF_CANS = clsCommon.myCstr(txtNoOfCans.Text)
                obj.Can_Code = txtCan.Value
                obj.Gross_Weight = clsCommon.myCdbl(txtMilkWeight.Text)
                obj.Tare_Weight = clsCommon.myCdbl(txtCan.Tag) * obj.NO_OF_CANS
                obj.MILK_WEIGHT = clsCommon.myCdbl(txtMilkWeight.Text) - obj.Tare_Weight
                If obj.MILK_WEIGHT < 0 Then
                    obj.MILK_WEIGHT = 0
                End If
                MinimumReachedWeightValue = obj.MILK_WEIGHT

                Dim Unit_CodeApply As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Milk_Receive_UOM from TSPL_VLC_MASTER_HEAD where VLC_Code='" + obj.VLC_CODE + "'"))
                If clsCommon.myLen(Unit_CodeApply) > 0 Then
                    Unit_Code = Unit_CodeApply
                End If
                conv_fac = clsWeightConversionInfo.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), Nothing)

                If clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal Then
                    obj.ACC_WEIGHT = clsCommon.myCdbl(obj.MILK_WEIGHT)
                    obj.LTR_WEIGHT = clsCommon.myCdbl(obj.MILK_WEIGHT * conv_fac)
                Else
                    obj.LTR_WEIGHT = clsCommon.myCdbl(obj.MILK_WEIGHT)
                    obj.ACC_WEIGHT = clsCommon.myCdbl(obj.MILK_WEIGHT * conv_fac)
                End If
                obj.TYPE = clsCommon.myCstr(cboType.SelectedValue)
                obj.MILK_TYPE = clsCommon.myCstr(cboMilkType.SelectedValue)
                obj.SAMPLE_NO_VALUES = ""
                Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
                obj.DOC_DATE = New DateTime(dtpDocDate.Value.Year, dtpDocDate.Value.Month, dtpDocDate.Value.Day, dtCurr.Hour, dtCurr.Minute, dtCurr.Second)
                obj.SHIFT = clsCommon.myCstr(IIf(Me.cboShift.Text = "Morning", "M", "E"))
                obj.COMM_PORT = UcWeighing1.Port
                obj.MCC_CODE = clsCommon.myCstr(fndMCCCode.Value)
                obj.IS_ENTERED_MANUAL = IIf(is_Entered_Manually = True, "Y", "N")
                obj.UOM_Code = Unit_Code
                obj.Conversion_Factor = conv_fac


                objList.Add(obj)

                ''For Custom Fields
                objHead.Form_ID = MyBase.Form_ID
                objHead.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(objHead.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(objHead.arrCustomFields, gv1, MyBase.ArrDetailFields, colCode)
                End If
                ''End of For Custom Fields


                If clsMilkReceiptMCC.SaveData(objHead, objList) Then
                    If btnsave.Text = ">>" Then
                        frmRange += 1
                    End If
                    UcAttachment1.SaveData(objHead.DOC_CODE)
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(objHead.DOC_CODE)
                    fndVLCCode.Focus()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpDocDate.Value, Nothing) = False Then
                dtpDocDate.Select()
                Return False
            End If
            '=======================================================
            Dim strchk As String = "select Posted from TSPL_MILK_RECEIPT_HEAD where DOC_COde='" + txtCode.Value + "'"
            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
            Dim str As String = Checksaveddata(frmRange, frmRange)
            If clsCommon.myLen(str) > 0 And clsCommon.CompairString(btnsave.Text, "Update") <> CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(str & " have been Already saved")
                Return False
            End If
            If btnsave.Text <> ">>" Then
                If Checksavedsample(0, 0) = False Then
                    Return False
                End If
            End If
            If (clsCommon.myCdbl(TxtFrmRange.Text) <= 0 Or clsCommon.myCdbl(TxtToRange.Text) <= 0) And btnsave.Text = ">>" Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Range..", Me.Text)
                TxtFrmRange.Focus()
                Return False
            End If

            If clsCommon.myCdbl(TxtToRange.Text) < clsCommon.myCdbl(frmRange) Then
                clsCommon.MyMessageBoxShow("All Sample No Have been Saved.This is Over range sample No [" & frmRange & "].")
                TxtFrmRange.Focus()
                Return False
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboMilkType.SelectedValue), "Rejected") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow(Me, "Milk Type can not be Rejected.", Me.Text)
                cboMilkType.Focus()
                Return False
            End If
            If clsCommon.myLen(Me.cboShift.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Shift", Me.Text)
                Return False
            End If

            If clsCommon.myLen(Me.fndMCCCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter MCC", Me.Text)
                fndMCCCode.Focus()
                Return False
            End If

            If clsCommon.myLen(Me.fndVLCCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter VLC Code", Me.Text)
                fndVLCCode.Focus()
                Return False
            End If

            If clsCommon.myLen(Me.fndVspCode.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter VSP Code", Me.Text)
                fndVspCode.Focus()
                Return False
            End If


            If clsCommon.myLen(Me.fndVehicleCode.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Vehicle Code", Me.Text)
                fndVehicleCode.Focus()
                Return False
            End If

            If clsCommon.myCdbl(Me.txtMilkWeight.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Milk Weight", Me.Text)
                txtMilkWeight.Focus()
                Return False
            End If
            If clsCommon.myCdbl(Me.txtNoOfCans.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter No of Cans", Me.Text)
                txtNoOfCans.Focus()
                Return False
            End If
            If clsCommon.myLen(Me.cboType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Type", Me.Text)
                Return False
            End If

            If clsCommon.myLen(Me.cboMilkType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Milk Type", Me.Text)
                Return False
            End If
            If clsCommon.myLen(Me.fndRouteCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Route Code", Me.Text)
                fndRouteCode.Focus()
                Return False
            End If
            UcCustomFields1.AllowToSave()

            If IsAllowSaveWithoutShifttime = False Then
                If ShiftTiming = "1" Then
                    Dim IsOpenShift As Integer = ClsOpenMCCShift.CheckisShiftTimingAvailable(fndMCCCode.Value, cboShift.SelectedValue)
                    If IsOpenShift <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Shift Timing Excluded.Can't be Save.", Me.Text)
                        Return False
                    End If
                End If
            End If
            If SplitContainer2.Panel1Collapsed Then
                If clsCommon.myCdbl(txtMilkWeight.Text) >= (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting - (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Neg_Setting / 100)) And clsCommon.myCdbl(txtMilkWeight.Text) <= (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting + (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Positive_Setting / 100)) Then
                Else
                    clsCommon.MyMessageBoxShow("Please Check Milk Weight Its Value Should be Between [" & Math.Round((clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting - (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Neg_Setting / 100)), 2) & "] and [" & (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting + (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Positive_Setting / 100)) & "]")
                    If txtMilkWeight.Enabled = True Then
                        txtMilkWeight.Value = 0
                        txtMilkWeight.Focus()
                    Else
                        txtNoOfCans.Focus()
                    End If
                    Return False
                End If
            Else
                If clsCommon.myLen(txtCan.Value) <= 0 Then
                    txtCan.Focus()
                    clsCommon.MyMessageBoxShow(Me, "Please select Can", Me.Text)
                    Return False
                End If
                Dim dclNetWeight As Decimal = (txtMilkWeight.Value - (clsCommon.myCdbl(txtCan.Tag) * txtNoOfCans.Value))
                If dclNetWeight < 0 Then
                    txtMilkWeight.Focus()
                    clsCommon.MyMessageBoxShow(Me, "Net weight going -ve", Me.Text)
                    Return False
                End If

                If dclNetWeight >= (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting - (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Neg_Setting / 100)) And dclNetWeight <= (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting + (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Positive_Setting / 100)) Then
                Else
                    clsCommon.MyMessageBoxShow("Please Check Milk Weight Its Value Should be Between [" & Math.Round((clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting - (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Neg_Setting / 100)), 2) & "] and [" & (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting + (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Positive_Setting / 100)) & "] and net weight is [" + clsCommon.myCstr(dclNetWeight) + "]")
                    If txtMilkWeight.Enabled = True Then
                        txtMilkWeight.Value = 0
                        txtMilkWeight.Focus()
                    Else
                        txtNoOfCans.Focus()
                    End If
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub AddNew()
        isCellValueChangedOpen = False
        txtCode.Value = ""
        btnsave.Text = ">>"
        'is_Entered_Manually = False
        gv1.DataSource = Nothing
        ChkALLVLC.Checked = False
        Me.fndVLCCode.Value = Nothing
        Me.fndVLCCode.Tag = Nothing
        Me.fndVspCode.Text = Nothing
        txtMilkWeight.Text = Nothing
        lblVillageCode.Text = "" ''BHA/02/08/18-000216 by balwinder on 02/08/2018
        lblVillageName.Text = ""
        txtCan.Value = ""
        txtTotalWeight.Text = Nothing
        TxtTotalWeightallRoute.Text = Nothing
        TotalCansAllRoute.Text = Nothing
        lblVSPDesc.Text = Nothing
        lblVLCDesc.Text = Nothing
        Me.txtMilkWeight.Text = Nothing
        Me.txtNoOfCans.Text = Nothing
        cboMilkType.SelectedValue = "Good"
        lblVLCCode.Tag = Nothing
        cboShift.Enabled = True
        cboShift.Enabled = True
        fndItem_Code.Text = Nothing
        lblItemDesc.Text = Nothing
        TxtFrmRange.ReadOnly = False
        TxtToRange.ReadOnly = False
        btnsave.Enabled = True
        BtnPost.Enabled = True
        btndelete.Enabled = True
        'For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        fndItem_Code.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)
        lblItemDesc.Text = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & fndItem_Code.Text & "'")
        ''End of For Custom Fields
        Me.fndMCCCode.Enabled = True
        Me.fndVLCCode.Enabled = True
        Me.fndVspCode.Enabled = True
        Me.fndRouteCode.Enabled = True
        Me.fndVehicleCode.Enabled = True
        Me.chkOther.Checked = False
        Me.fndItem_Code.Enabled = True
        UcAttachment1.BlankAllControls()
        gv1.Rows.Clear()
        Me.gv1.DataSource = Nothing
        fndRouteCode.Focus()
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
        isInsideLoadData = True
        cboShift.DataSource = dt
        cboShift.ValueMember = "Code"
        cboShift.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub





    Sub LoadDatainControls(ByVal vlccode As String)
        txtCode.Value = ""
        'is_Entered_Manually = False
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Me.cboShift.SelectedIndex = -1
        Me.fndMCCCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Me.fndVLCCode.Value = Nothing
        Me.fndVLCCode.Tag = Nothing
        Me.fndVspCode.Text = Nothing
        txtMilkWeight.Text = Nothing
        txtTotalWeight.Text = Nothing
        TxtTotalWeightallRoute.Text = Nothing
        TotalCansAllRoute.Text = Nothing
        fndVehicleCode.Text = Nothing
        lblVSPDesc.Text = Nothing
        lblVLCDesc.Text = Nothing
        Me.fndVehicleCode.Text = Nothing
        Me.chkOther.Checked = False
        Me.txtMilkWeight.Text = 0
        Me.txtNoOfCans.Text = 0
        cboMilkType.SelectedValue = "Good"
        fndItem_Code.Text = Nothing
        lblItemDesc.Text = Nothing
        txtCan.Value = ""
        txtVehicle.Text = ""
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Try
            UcWeighing1.CloseCOMPORT()
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
                If (clsMilkReceiptMCC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso BtnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled AndAlso MyBase.isDeleteFlag Then
            Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='USERPWD' and TYPE='PWD'")
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = "USERPWD"
            pwd.strType = "PWD"
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                DeleteData()
            End If
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.KeyCode = Keys.F2 Then
            Try
                txtMilkWeight.Text = UcWeighing1.LiveReading
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try

        ElseIf e.Control And e.KeyCode = Keys.R Then
            TxtFrmRange.ReadOnly = False
            TxtToRange.ReadOnly = False
        ElseIf e.Control And e.KeyCode = Keys.Z Then
            RadButton1.Visible = True
            lbErrors.Visible = True
        ElseIf e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.A Then
            Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='AllowMilkReceiptAfterSettingsisOn' and TYPE='AllowMilkReceiptAfterSettingsisOn'")
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = "AllowMilkReceiptAfterSettingsisOn"
            pwd.strType = "AllowMilkReceiptAfterSettingsisOn"
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                IsAllowSaveWithoutShifttime = True
            Else
                IsAllowSaveWithoutShifttime = False
            End If
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                      "========Table Name=========" + Environment.NewLine + _
                      "TSPL_MILK_RECEIPT_HEAD" + Environment.NewLine + _
                      "=========Setting Name======" + Environment.NewLine + _
                      "WeighmentNotMandatoryInMCC" + Environment.NewLine + _
                      "SampleFONTSize" + Environment.NewLine + _
                      "DisplayTypeInMilkReceipt" + Environment.NewLine + _
                      "MilkReceiptRequiredApproval" + Environment.NewLine + _
                      "MilkSetting" + Environment.NewLine + _
                      "MCCReceiptRange")
        End If
    End Sub

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            AddNew()
            btnsave.Text = ">>"
            Dim strWhr As String = ""
            If clsCommon.myLen(lblDockCode.Text) > 0 Then
                strWhr += " TSPL_MILK_RECEIPT_HEAD.DOCK_Code='" + lblDockCode.Text + "'"
            End If

            Dim obj As clsMilkReceiptMCC = clsMilkReceiptMCC.GetData(strDoc, navType, Nothing, True, strWhr)
            gv1.DataSource = obj.Gettable(obj.DOC_CODE, NavigatorType.Current)
            gv1.BestFitColumns()
            UsLock1.Status = obj.POSTED
            txtCode.Value = obj.DOC_CODE
            dtpDocDate.Value = obj.DOC_DATE
            cboShift.SelectedValue = obj.SHIFT
            'lblDockCode.Text = obj.DOCK_Code
            'lblDockName.Text = clsDockMaster.GetName(obj.DOCK_Code)
            UcAttachment1.LoadData(obj.DOC_CODE)
            ReStoreGridLayout()
            txtCode.Value = obj.DOC_CODE
            cboDockCollectionMilkType.SelectedValue = obj.Dock_Collection_Milk_Type
            DtSample = clsDBFuncationality.GetDataTable("select VLC_DOC_CODE,MILK_RECEIPT_CODE,Posted from TSPL_MILK_SAMPLE_DETAIL inner join " _
                                                      & " TSPL_MILK_sample_HEAD on TSPL_MILK_sample_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE where MILK_RECEIPT_CODE ='" & clsCommon.myCstr(txtCode.Value) & "' ")
            gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
            gv1.AutoScroll = True
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.BlankAllControls()
                UcCustomFields1.LoadData(obj.DOC_CODE)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtpDocDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDocDate.LostFocus
        Me.txtCode.Value = clsMilkReceiptMCC.GetDocCode(Me.dtpDocDate.Value, Me.fndMCCCode.Value, Me.cboShift.Text, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
    End Sub

    Private Sub cboShift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboShift.SelectedIndexChanged
        If Not isInsideLoadData Then
            Me.txtCode.Value = clsMilkReceiptMCC.GetDocCode(Me.dtpDocDate.Value, Me.fndMCCCode.Value, Me.cboShift.SelectedValue, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
        End If
    End Sub

    Private Sub fndMCCCode_Validating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        Dim sQuery As String = "select Location_Category from tspl_location_master where Location_Code='" & fndMCCCode.Value & "'"
        Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
        fndMCCCode.Value = clsMccMaster.getFinder("", fndMCCCode.Value, isButtonClicked)
        Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMCCCode.Value)
        If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
            Irregular_Mcc_Code = ""
            clsCommon.MyMessageBoxShow(Me, "No shifts is opened.Atleats one Shift should be Opened..", Me.Text)
            btnsave.Enabled = False
        ElseIf DTShift.Rows.Count > 1 Then
            Irregular_Mcc_Code = ""
            clsCommon.MyMessageBoxShow(Me, "There are more then one shifts are opened.Only one Shift can be Opened..", Me.Text)
            Me.Close()
        Else
            btnsave.Enabled = True
            dtpDocDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
            cboShift.SelectedValue = DTShift.Rows(0).Item("Shift")
            If clsCommon.myCstr(DTShift.Rows(0).Item("Is_Allow_Manual_Entry_Weighment")) = "T" Then
                txtMilkWeight.Enabled = True
                is_Entered_Manually = True
            Else
                is_Entered_Manually = False
            End If
            Irregular_Mcc_Code = clsCommon.myCstr(DTShift.Rows(0).Item("Irregular_Mcc_Code"))
        End If
        Try
            SetDocKCollectionMilkType(fndMCCCode.Value)
            Me.txtCode.Value = clsMilkReceiptMCC.GetDocCode(Me.dtpDocDate.Value, Me.fndMCCCode.Value, Me.cboShift.Text, clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue))
        Catch ex As Exception
        End Try
        Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCCode.Value + "' "))
        UcWeighing1.Machine = clsMccMaster.DefaultWeighingMachine(fndMCCCode.Value, Nothing)
    End Sub

    Sub SetDocKCollectionMilkType(ByVal strMCCCode As String)
        If clsCommon.myLen(strMCCCode) > 0 Then
            Dim qry As String = "select Is_Seprate_Dock_Cow_Buffalo,Receipt_Weight_tolerance_Apply,Receipt_Weight_tolerance_Value,Collection_Method from TSPL_MCC_MASTER where MCC_Code='" + strMCCCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("Is_Seprate_Dock_Cow_Buffalo")) = 1 Then
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
                ApplyWeightTolerance = IIf(clsCommon.myCdbl(dt.Rows(0)("Receipt_Weight_tolerance_Apply")) = 1, True, False)
                WeightToleranceValue = clsCommon.myCdbl(dt.Rows(0)("Receipt_Weight_tolerance_Value"))
                SplitContainer2.Panel1Collapsed = IIf(clsCommon.myCdbl(dt.Rows(0)("Collection_Method")) = 1, False, True)
            Else
                cboDockCollectionMilkType.SelectedValue = "M"
                ApplyWeightTolerance = False
                WeightToleranceValue = 0
                SplitContainer2.Panel1Collapsed = True
            End If
            cboDockCollectionMilkType.Enabled = False

            qry = "select Code,Description from TSPL_DOCK_MASTER where MCC_Code='" + strMCCCode + "'"
            dt = clsDBFuncationality.GetDataTable(qry)
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

    Private Sub fndVLCCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVLCCode._MYValidating
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If ApplyWeightTolerance AndAlso Not txtMilkWeight.Enabled Then
                    If lbErrors.Visible Then
                        clsCommon.MyMessageBoxShow(clsCommon.myCstr(MinimumReachedWeightValue) + " > " + clsCommon.myCstr(WeightToleranceValue))
                    End If
                    If MinimumReachedWeightValue > WeightToleranceValue Then
                        Dim pwd As New FrmPWD(Nothing)
                        pwd.strAnyText = "Required password due to improper minimum weight"
                        pwd.strCode = clsFixedParameterCode.MilkReceiptTolerancePwd
                        pwd.strType = clsFixedParameterType.MilkReceiptTolerancePwd
                        pwd.ShowDialog()
                        If pwd.isPasswordCorrect Then
                            Dim objLog As New clsMilkReceiptImproperWeightLog()
                            objLog.Doc_Code = txtCode.Value
                            objLog.MCC_Code = fndMCCCode.Value
                            objLog.Sample_No = frmRange
                            objLog.Min_Weight_Value = MinimumReachedWeightValue
                            objLog.SaveData(objLog)
                        Else
                            fndVLCCode.Value = ""
                            isCellValueChangedOpen = False
                            Exit Sub
                        End If
                    End If
                End If

                Dim qry As String = ""
                Dim dt As DataTable
                If clsCommon.myLen(fndVLCCode.Value) > 0 Then
                    If clsCommon.myLen(fndRouteCode.Value) > 0 Then
                        qry = "select TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.vlc_code as [Vlc Code],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],Route_name as [Route Name], " _
                            & " TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehicle Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code]," _
                            & " TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name]," _
                            & " TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as " _
                            & " [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date]" _
                            & " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and " _
                            & " TSPL_VENDOR_MASTER.Form_Type='VSP' left join tspl_Mcc_Route_Master on tspl_Mcc_Route_Master.route_code=TSPL_VLC_MASTER_HEAD.route_code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc  where tspl_mcc_master.mcc_Code='" & fndMCCCode.Value & "' " & IIf(ChkALLVLC.Checked = True, "", " and TSPL_VLC_MASTER_HEAD.Route_Code='" & fndRouteCode.Value & "'") & " and TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader='" & fndVLCCode.Value & "' and TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0 "
                    End If

                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt.Rows.Count <= 0 Or dt Is Nothing Then
                        isCellValueChangedOpen = False
                        clsCommon.MyMessageBoxShow("No VLC [" & fndVLCCode.Value & "] Found .")
                        fndVLCCode.Value = ""
                        lblVLCDesc.Text = ""
                        fndVLCCode.Tag = ""
                        Exit Sub
                    End If
                End If
                'BHA/27/04/18-000004 by balwinder on 15/05/2018 add village code/Name in Grid and finder for bharat.
                qry = "select TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.vlc_code as [Vlc Code],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],Route_name as [Route Name]," _
                & " TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehicle Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code]," _
                & " TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name]," _
                & " TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as " _
                & " [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date]" _
                + " ,TSPL_VLC_MASTER_HEAD.Village_Code as VillageCode,TSPL_VILLAGE_MASTER.Village_Name as VillageName " _
                & " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and " _
                & " TSPL_VENDOR_MASTER.Form_Type='VSP' left join tspl_Mcc_Route_Master on tspl_Mcc_Route_Master.route_code=TSPL_VLC_MASTER_HEAD.route_code  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc left outer join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code "

                fndVLCCode.Value = clsCommon.ShowSelectForm("VLCFND_Rcpt", qry, "Uploader_Code", " TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0 and TSPL_VLC_MASTER_HEAD.Active=1 and tspl_mcc_master.mcc_Code='" & fndMCCCode.Value & "' " & IIf(ChkALLVLC.Checked = True, "", " and TSPL_VLC_MASTER_HEAD.Route_Code='" & fndRouteCode.Value & "'") & "", fndVLCCode.Value, "Uploader_Code", isButtonClicked)
                If clsCommon.myLen(fndVLCCode.Value) <= 0 Then
                    isCellValueChangedOpen = False
                    Exit Sub
                End If
                qry &= " where  tspl_mcc_master.mcc_Code='" & fndMCCCode.Value & "' " & IIf(ChkALLVLC.Checked = True, "", " and TSPL_VLC_MASTER_HEAD.Route_Code='" & fndRouteCode.Value & "'") & " and vlc_code_vlc_uploader='" & clsCommon.myCstr(fndVLCCode.Value) & "'"
                Dim dt_vlc As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim dr As DataRow = dt_vlc.Rows(0)
                If Not IsNothing(dr) Then
                    fndVLCCode.Value = clsCommon.myCstr(dr("Uploader_Code"))
                    fndVLCCode.Tag = clsCommon.myCstr(dr("Vlc Code"))
                    lblVillageCode.Text = clsCommon.myCstr(dr("VillageCode"))
                    lblVillageName.Text = clsCommon.myCstr(dr("VillageName"))
                    If clsCommon.myLen(fndVLCCode.Tag) > 0 Then
                        qry = ""
                        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VSP_Code,VSP.Vendor_Name AS VSP_NAME,TSPL_VLC_MASTER_HEAD.ROUTE_CODE,tspl_mcc_route_master.ROUTE_NAME from TSPL_VLC_MASTER_HEAD " &
                              " LEFT JOIN tspl_mcc_route_master ON TSPL_VLC_MASTER_HEAD.ROUTE_CODE=tspl_mcc_route_master.ROUTE_CODE " &
                              " LEFT JOIN TSPL_VENDOR_MASTER as VSP ON TSPL_VLC_MASTER_HEAD.VSP_Code=VSP.Vendor_Code " &
                              " WHERE TSPL_VLC_MASTER_HEAD.vlc_code='" & fndVLCCode.Tag & "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt.Rows.Count > 0 Then
                            lblVLCDesc.Text = dt.Rows(0).Item("VLC_Name")
                            fndVspCode.Text = dt.Rows(0).Item("VSP_Code")
                            lblVSPDesc.Text = dt.Rows(0).Item("VSP_NAME")
                        End If
                    End If
                End If
                txtNoOfCans.Focus()
                isCellValueChangedOpen = False
            End If
            txtNoOfCans.Focus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "SELECT DOC_CODE as Code,convert(varchar,DOC_DATE,103) as Date,MCC_CODE as [Mcc Code] FROM TSPL_MILK_RECEIPT_HEAD"
            Dim strWhr As String = ""
            If clsCommon.myLen(lblDockCode.Text) > 0 Then
                strWhr += "TSPL_MILK_RECEIPT_HEAD.DOCK_Code='" + lblDockCode.Text + "'"
            End If
            txtCode.Value = clsCommon.ShowSelectForm("MILK RECEIPT", qry, "Code", strWhr, txtCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                cboShift.Enabled = False
            End If
            qry = String.Empty
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If SettStopUpdateForWeigingMilkReceipt AndAlso Not is_Entered_Manually Then
                Exit Sub
            End If
            Me.fndMCCCode.Enabled = True
            Me.fndVLCCode.Enabled = True
            Me.fndRouteCode.Enabled = True
            If btnsave.Visible = False Then
                clsCommon.MyMessageBoxShow(Me, "You do Not have Permission to Update Any Record.", Me.Text)
                Exit Sub
            End If
            Dim Vlc_Code As String = gv1.CurrentRow.Cells("VLC DOC CODE").Value
            Dim trans As SqlTransaction = Nothing
            Dim totqty As Double
            Dim obj As clsMilkReceiptMCC = clsMilkReceiptMCC.LoadDataFromDetails(Vlc_Code, NavigatorType.Current, trans)
            If Not IsNothing(obj) Then
                For Each objtr As clsMilkReceiptMCCDetail In clsMilkReceiptMCC.ObjList
                    If DtSample.Select("Milk_receipt_Code='" & txtCode.Value & "' and vlc_Doc_Code='" & Vlc_Code & "'").Length > 0 Then
                        If DtSample.Select("Milk_receipt_Code='" & txtCode.Value & "' and vlc_Doc_Code='" & Vlc_Code & "'")(0)("Posted") = "1" Then
                            clsCommon.MyMessageBoxShow(Me, "This Code is Sampled and Posted can not be Edited.", Me.Text)
                            Exit Sub
                        End If
                    End If
                    If clsCommon.myCstr(objtr.IS_Sampled) = "1" Then
                        If clsCommon.MyMessageBoxShow("Do you want to change Milk Weight.its Sample Has Been Created ?", "Milk Weight", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                            clsCommon.MyMessageBoxShow(Me, "This Code is Sampled and Posted can not be Edited.", Me.Text)
                            Exit Sub
                        ElseIf clsCommon.myCstr(objtr.IS_Sampled) = "0" Then
                            Dim pwd As FrmPWD = New FrmPWD(Nothing)
                            pwd.strCode = "USERPWD"
                            pwd.strType = "PWD"
                            pwd.ShowDialog()
                            If pwd.isPasswordCorrect Then
                                Me.dtpDocDate.Value = objtr.DOC_DATE
                                Me.cboShift.SelectedValue = objtr.SHIFT
                                Me.fndMCCCode.Value = objtr.MCC_CODE
                                Me.fndVLCCode.Tag = objtr.VLC_CODE
                                Me.fndVLCCode.Value = objtr.VLC_CODE_Uploader_Code
                                Me.fndVspCode.Text = objtr.VSP_CODE
                                Me.fndRouteCode.Value = objtr.ROUTE_CODE
                                Me.fndMCCCode.Enabled = False
                                Me.fndVLCCode.Enabled = False
                                Me.fndRouteCode.Enabled = False
                                txtMilkWeight.Text = objtr.MILK_WEIGHT
                                txtMilkWeight.Tag = objtr.MILK_WEIGHT
                                fndItem_Code.Text = objtr.Item_CODE
                                txtTotalWeight.Text = Nothing
                                TxtTotalWeightallRoute.Text = Nothing
                                TotalCansAllRoute.Text = Nothing
                                lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCode.Value & "'")
                                lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVspCode.Text & "'")
                                lblVLCDesc.Text = clsDBFuncationality.getSingleValue("select vlc_name from TSPL_VLC_MASTER_HEAD where VLC_Code='" & fndVLCCode.Tag & "'")
                                lblItemDesc.Text = clsDBFuncationality.getSingleValue("select item_desc from TSPL_ITEM_MASTER where Item_Code='" & fndItem_Code.Text & "'")
                                Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCCode.Value + "' "))
                                Me.fndVehicleCode.Text = objtr.VEHICLE_CODE
                                txtVehicle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & fndVehicleCode.Text & "'"))
                                If clsCommon.myLen(objtr.Can_Code) > 0 Then
                                    Me.txtMilkWeight.Text = objtr.Gross_Weight
                                    txtCan.Value = objtr.Can_Code
                                    txtCan.Tag = objtr.CanTareWeightOne
                                Else
                                    Me.txtMilkWeight.Text = objtr.MILK_WEIGHT
                                    txtCan.Tag = 0
                                End If
                                Me.txtNoOfCans.Text = objtr.NO_OF_CANS
                                Me.txtNoOfCans.Tag = objtr.SAMPLE_NO
                                cboMilkType.SelectedValue = objtr.MILK_TYPE
                                Me.cboType.SelectedValue = objtr.TYPE
                                lblVLCCode.Tag = Vlc_Code
                                is_Entered_Manually = IIf(objtr.IS_ENTERED_MANUAL = "Y", True, False)
                                cboShift.Enabled = False
                                ChkALLVLC.Checked = objtr.Other_VLC
                                btnsave.Text = "Update"
                            Else
                                clsCommon.MyMessageBoxShow(Me, "Password is Wrong.", Me.Text)
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    End If
                    Me.dtpDocDate.Value = objtr.DOC_DATE
                    Me.cboShift.SelectedValue = objtr.SHIFT
                    Me.fndMCCCode.Value = objtr.MCC_CODE
                    Me.fndVLCCode.Tag = objtr.VLC_CODE
                    Me.fndVLCCode.Value = objtr.VLC_CODE_Uploader_Code
                    Me.fndVspCode.Text = objtr.VSP_CODE
                    Me.fndRouteCode.Value = objtr.ROUTE_CODE
                    Me.chkOther.Checked = IIf(objtr.Other_VEHICLE = "T", True, False)
                    Me.ChkALLVLC.Checked = IIf(objtr.Other_VLC = 1, True, False)
                    txtMilkWeight.Text = objtr.MILK_WEIGHT
                    txtMilkWeight.Tag = objtr.MILK_WEIGHT
                    fndItem_Code.Text = objtr.Item_CODE
                    txtTotalWeight.Text = Nothing
                    TxtTotalWeightallRoute.Text = Nothing
                    TotalCansAllRoute.Text = Nothing
                    lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCode.Value & "'")
                    lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVspCode.Text & "'")
                    lblVLCDesc.Text = clsDBFuncationality.getSingleValue("select vlc_name from TSPL_VLC_MASTER_HEAD where VLC_Code='" & fndVLCCode.Tag & "'")
                    lblItemDesc.Text = clsDBFuncationality.getSingleValue("select item_desc from TSPL_ITEM_MASTER where Item_Code='" & fndItem_Code.Text & "'")
                    lblVillageCode.Text = clsCommon.myCstr(gv1.CurrentRow.Cells("VillageCode").Value)
                    lblVillageName.Text = clsCommon.myCstr(gv1.CurrentRow.Cells("VillageName").Value)
                    Me.fndVehicleCode.Text = objtr.VEHICLE_CODE
                    txtVehicle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & fndVehicleCode.Text & "'"))
                    If clsCommon.myLen(objtr.Can_Code) > 0 Then
                        Me.txtMilkWeight.Text = objtr.Gross_Weight
                        txtCan.Value = objtr.Can_Code
                        txtCan.Tag = objtr.CanTareWeightOne
                    Else
                        Me.txtMilkWeight.Text = objtr.MILK_WEIGHT
                        txtCan.Tag = 0
                    End If
                    Me.txtNoOfCans.Text = objtr.NO_OF_CANS
                    Me.txtNoOfCans.Tag = objtr.SAMPLE_NO
                    cboMilkType.SelectedValue = objtr.MILK_TYPE
                    Me.cboType.SelectedValue = objtr.TYPE
                    lblVLCCode.Tag = Vlc_Code
                    is_Entered_Manually = IIf(objtr.IS_ENTERED_MANUAL = "Y", True, False)
                    cboShift.Enabled = False
                    btnsave.Text = "Update"
                Next
                Dim totcan As Double = 0
                Dim totcanAllRoute As Double = 0
                Dim totWgtAllRoute As Double = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCstr(grow.Cells("Route Code").Value) = clsCommon.myCstr(fndRouteCode.Value) Then
                        totqty += clsCommon.myCdbl(grow.Cells("MILK WEIGHT").Value)
                        totcan += clsCommon.myCdbl(grow.Cells("NO OF CANS").Value)
                    End If
                    totWgtAllRoute += clsCommon.myCdbl(grow.Cells("MILK WEIGHT").Value)
                    totcanAllRoute += clsCommon.myCdbl(grow.Cells("NO OF CANS").Value)
                Next
                txtTotalWeight.Text = Math.Round(totqty, 2)
                TxtTotalcans.Text = Math.Round(totcan, 2)
                TxtTotalWeightallRoute.Text = Math.Round(totWgtAllRoute, 2)
                TotalCansAllRoute.Text = Math.Round(totcanAllRoute, 2)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
        IsAllowSaveWithoutShifttime = False
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            e.CellElement.Font = GridFont
            If GridFontSize > 8 Then
                If e.Column Is gv1.Columns("VLC NAME") Or e.Column Is gv1.Columns("VLC CODE") Or e.Column Is gv1.Columns("VSP Name") Or e.Column Is gv1.Columns("VLC UPLOADER CODE") Or e.Column Is gv1.Columns("ROUTE CODE") Or e.Column Is gv1.Columns("ROUTE NAME") Or e.Column Is gv1.Columns("NO OF CANS") Then
                    e.CellElement.BackColor = Color.Yellow
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = GradientStyles.Solid
                    e.CellElement.ForeColor = Color.Black
                Else
                    e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                    e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                    e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                    e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
                End If

            End If
        Catch ex As Exception
        End Try

        Try
            Dim totqty As Double = 0
            Dim totcan As Double = 0
            Dim totqtyall As Double = 0
            Dim totcanall As Double = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCstr(grow.Cells("Route Code").Value) = clsCommon.myCstr(fndRouteCode.Value) Then
                    totqty += clsCommon.myCdbl(grow.Cells("MILK WEIGHT").Value)
                    totcan += clsCommon.myCdbl(grow.Cells("NO OF CANS").Value)
                End If
                totqtyall += clsCommon.myCdbl(grow.Cells("MILK WEIGHT").Value)
                totcanall += clsCommon.myCdbl(grow.Cells("NO OF CANS").Value)
            Next
            txtTotalWeight.Text = Math.Round(totqty, 2)
            TxtTotalcans.Text = Math.Round(totcan, 2)
            TxtTotalWeightallRoute.Text = Math.Round(totqtyall, 2)
            TotalCansAllRoute.Text = Math.Round(totcanall, 2)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If btndelete.Visible = False Then
                clsCommon.MyMessageBoxShow(Me, "You do Not have Permission to Delete Any Record.", Me.Text)
                Exit Sub
            End If
            Dim sQuery As String = "select * from TSPL_MILK_RECEIPT_DETAIL where DOC_Code='" & gv1.CurrentRow.Cells("DOC CODE").Value & "' and VLC_DOC_Code='" & gv1.CurrentRow.Cells("VLC DOC CODE").Value & "' and coalesce(is_sampleed,'F')='T'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            If dt.Rows.Count <= 0 Then
                If clsCommon.MyMessageBoxShow("Do You want to delete this Row Permanently . Are You Sure.?", "Delete Row", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    sQuery = "delete from TSPL_MILK_RECEIPT_DETAIL where DOC_Code='" & gv1.CurrentRow.Cells("DOC CODE").Value & "' and VLC_DOC_Code='" & gv1.CurrentRow.Cells("VLC DOC CODE").Value & "'"
                    clsDBFuncationality.ExecuteNonQuery(sQuery)
                Else
                    e.Cancel = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "This Row is Sampled and can not be Deleted..", Me.Text)
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                If (clsMilkReceiptMCC.PostData(txtCode.Value, True)) Then
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
                LoadData(txtCode.Value)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMilkWeight_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMilkWeight.TextChanged
        Try
            'is_Entered_Manually = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub fndRouteCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndRouteCode._MYValidating
        Try
            If Not isInsideLoadData Then
                isInsideLoadData = True
                TxtTotalcans.Text = 0
                txtTotalWeight.Text = 0

                Dim qry As String = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Description],Mcc_Name,TSPL_Primary_Vehicle_Master.* " _
                & " from TSPL_MCC_ROUTE_MASTER inner join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.vehicle_COde=TSPL_MCC_ROUTE_MASTER.vehicle_Code and TSPL_MCC_ROUTE_MASTER.mcc_code='" & fndMCCCode.Value & "' and active=1 inner join tspl_mcc_Master on tspl_mcc_Master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code" + Environment.NewLine + _
                " where 2=( case when isnull( tspl_mcc_Master.is_Reuired_Gate_Entry,0)=0 then 2 else case when exists "
                If WeighmentNotMandatoryInMCC Then
                    qry += " (select 1 from TSPL_MILK_GATE_ENTRY_IN " + Environment.NewLine + _
                    " where TSPL_MILK_GATE_ENTRY_IN.MCC_CODE='" + fndMCCCode.Value + "' and TSPL_MILK_GATE_ENTRY_IN.Route_Code=TSPL_MCC_ROUTE_MASTER.route_code and convert(date, Shift_Date,103)='" + clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy") + "' and Entry_Shift='" + clsCommon.myCstr(cboShift.SelectedValue) + "' and TSPL_MILK_GATE_ENTRY_IN.Status='1') then 2 else 3 end end)"
                Else
                    qry += " (select 1 from TSPL_MILK_GATE_ENTRY_WEIGHTMENT " + Environment.NewLine + _
                " left outer join TSPL_MILK_GATE_ENTRY_IN on TSPL_MILK_GATE_ENTRY_IN.Entry_Code=TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Against_Gate_Entry_No " + Environment.NewLine + _
                " where TSPL_MILK_GATE_ENTRY_IN.MCC_CODE='" + fndMCCCode.Value + "' and TSPL_MILK_GATE_ENTRY_IN.Route_Code=TSPL_MCC_ROUTE_MASTER.route_code and convert(date, Shift_Date,103)='" + clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy") + "' and Entry_Shift='" + clsCommon.myCstr(cboShift.SelectedValue) + "' and TSPL_MILK_GATE_ENTRY_WEIGHTMENT.GW_Status='1') then 2 else 3 end end)"
                End If

                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ROTFND2", qry)

                If dr IsNot Nothing Then
                    fndRouteCode.Value = clsCommon.myCstr(dr("code"))
                    lblRouteDesc.Text = clsCommon.myCstr(dr("Route Description"))
                    fndVehicleCode.Text = dr("Vehicle_Code")
                    txtVehicle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & fndVehicleCode.Text & "'"))
                Else
                    fndRouteCode.Value = ""
                    lblRouteDesc.Text = ""
                End If
                chkOther.Focus()
                isInsideLoadData = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveLayout.Click
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = "MilkReceiptGrid"
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        End If
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData("MilkReceiptGrid", objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkReceiptGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub TxtFrmRange_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFrmRange.Leave
        Try
            If gv1.Rows.Count <= 0 Then
                TxtToRange.Text = -1 + clsCommon.myCdbl(TxtFrmRange.Text) + clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCReceiptRange, clsFixedParameterCode.MilkSetting, Nothing))
                Dim str As String = Checksaveddata(clsCommon.myCdbl(TxtFrmRange.Text), clsCommon.myCdbl(TxtToRange.Text))
                If clsCommon.myLen(str) > 0 Then
                    clsCommon.MyMessageBoxShow(str & " have been Already saved")
                    TxtFrmRange.Text = Nothing
                    TxtToRange.Text = Nothing
                    Exit Sub
                End If
                frmRange = clsCommon.myCdbl(TxtFrmRange.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString, "txtRangeFrom_Leave")
        End Try
    End Sub

    Public Function Checksaveddata(ByVal str_frm_range As String, ByVal str_to_range As String, Optional ByVal trans As SqlTransaction = Nothing)
        Dim strserial As String = ""
        Dim qry As String = "select * from TSPL_MILK_RECEIPT_DETAIL left outer join TSPL_MILK_RECEIPT_HEAD on  TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE WHERE TSPL_MILK_RECEIPT_DETAIL.MCC_CODE='" & fndMCCCode.Value & "' and convert(date,TSPL_MILK_RECEIPT_DETAIL.DOC_DATE,103)=" _
           & " '" & clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy") & "'  and TSPL_MILK_RECEIPT_DETAIL.SHIFT='" & cboShift.SelectedValue & "' " _
           & " and TSPL_MILK_RECEIPT_DETAIL.sample_no>=" & clsCommon.myCdbl(str_frm_range) & " and TSPL_MILK_RECEIPT_DETAIL.sample_no<=" & clsCommon.myCdbl(str_to_range) & "  and TSPL_MILK_RECEIPT_HEAD.Dock_Collection_Milk_Type = '" + clsCommon.myCstr(cboDockCollectionMilkType.SelectedValue) + "'"
        If clsCommon.myLen(lblDockCode.Text) > 0 Then
            qry += "and TSPL_MILK_RECEIPT_HEAD.Dock_code='" + lblDockCode.Text + "'"
        End If

        Dim Dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If Dt.Rows.Count > 0 Then
            For Each row As DataRow In Dt.Rows()
                strserial = IIf(strserial = "", "Sample No -" & row.Item("sample_no"), strserial & "," & row.Item("sample_no"))
            Next
        End If
        Return strserial
    End Function

    Public Function Checksavedsample(ByVal str_frm_range As String, ByVal str_to_range As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim strserial As String = ""
        Dim qry As String = "select * from TSPL_MILK_sample_Head inner join tspl_milk_sample_Detail on tspl_milk_sample_Detail.doc_code=tspl_milk_sample_Head.doc_code WHERE MCC_CODE='" & fndMCCCode.Value & "' and convert(date,DOC_DATE,103)=" _
           & " '" & clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy") & "'  and SHIFT='" & cboShift.SelectedValue & "' and sample_No=" & Me.txtNoOfCans.Tag & " "
        Dim Dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If Dt.Rows.Count > 0 Then
            clsCommon.MyMessageBoxShow(Me, "This receipt have been sampled and can not be updated.", Me.Text)
            Return False
        End If
        Return True
    End Function

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim qry As String = ""
        qry = "select 'MCUP00001' as [MCC CODE],'MCC Budhana' as [Mcc Name],'01-Nov-2014' as [Shift Date],'Morning' as [Shift],'MCUP00001/MR1' as [Route Code],'ELAM' as [Route Name],1 as [Sample No]," _
        & " '0101' as [Vlc Code],'SOOP' as [Vlc Name],'' as [Vehicle is Other],0 as [No of Cans],0 as [Milk Weight]"
        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Dim totqty As Double = 0
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "MCC CODE", "Mcc Name", "Shift Date", "Shift", "Route Code", "Route Name", "Sample No", "Vlc Code", "Vlc Name", "Vehicle is Other", "No of Cans", "Milk Weight") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                Dim Mcccode As String = ""
                Dim MccName As String = ""
                Dim ShiftDate As DateTime = Nothing
                Dim Shift As String = ""
                Dim routecode As String = ""
                Dim routename As String = ""
                Dim SampleNo As Integer = 0
                Dim Vlc_Code As String = ""
                Dim Vlc_name As String = ""
                Dim Vlc_Is_Other As String = ""
                Dim No_of_cans As Integer = 0
                Dim Milk_Weight As Double = 0

                clsCommon.ProgressBarShow()
                Dim counter As Integer = 0
                Dim qry As String = ""
                Dim check As Integer = 0

                For Each grow As GridViewRowInfo In gv.Rows
                    'trans = clsDBFuncationality.GetTransactin()
                    counter += 1
                    Mcccode = clsCommon.myCstr(grow.Cells("Mcc code").Value)
                    If clsCommon.myLen(Mcccode) <= 0 Then
                        Throw New Exception("Fill VLC Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(Mcccode) > 30 Then
                        Throw New Exception("Length Of VLC Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(Mcccode) > 0 Then
                        qry = "select count(*) from TSPL_MCC_MASTER where MCC_code='" + Mcccode + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                    End If
                    '------------------------------------------------
                    fndMCCCode.Value = Mcccode
                    If clsCommon.myCstr(fndMCCCode.Value) <> "" Then
                        Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMCCCode.Value, trans)
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
                            Shift = IIf(clsCommon.myCstr(grow.Cells("Shift").Value).Contains("M"), "M", IIf(clsCommon.myCstr(grow.Cells("Shift").Value).Contains("E"), "E", ""))
                            If clsCommon.myLen(Shift) <= 0 Then
                                Throw New Exception("Fill Shift At Line No. " + clsCommon.myCstr(counter) + "")
                            End If
                            If clsCommon.myLen(grow.Cells("Shift Date").Value) >= 10 Then
                                If IsDate(grow.Cells("Shift Date").Value) Then
                                    ShiftDate = clsCommon.GetPrintDate(grow.Cells("Shift Date").Value, "dd-MMM-yyyy")
                                Else
                                    Throw New Exception("Fill Shift Date At Line No. " + clsCommon.myCstr(counter) + "")
                                End If
                            End If
                            dtpDocDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
                            isInsideLoadData = True
                            cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))
                            isInsideLoadData = False
                            If clsCommon.CompairString(clsCommon.GetPrintDate(ShiftDate, "dd-MMM-yyyy"), clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy")) <> CompairStringResult.Equal Then
                                Throw New Exception("Fill Shift Date Correctly At Line No. " + clsCommon.myCstr(counter) + "." & Environment.NewLine _
                                                    & "[" & dtpDocDate.Value & "] ,[" & cboShift.Text & "] Shift is Open. ")
                            End If

                            If clsCommon.CompairString(Shift, cboShift.SelectedValue) <> CompairStringResult.Equal Then
                                Throw New Exception("Fill Shift Correctly At Line No. " + clsCommon.myCstr(counter) + "." & Environment.NewLine _
                                                    & "[" & dtpDocDate.Value & "] ,[" & cboShift.Text & "] Shift is Open. ")
                            End If
                            LblUom.Text = clsCommon.myCstr(DTShift.Rows(0).Item("Uom_Code"))
                            If clsCommon.myCstr(DTShift.Rows(0).Item("Is_Allow_Manual_Entry_Weighment")) = "T" Then
                                txtMilkWeight.Enabled = True
                            End If
                            Irregular_Mcc_Code = clsCommon.myCstr(DTShift.Rows(0).Item("Irregular_Mcc_Code"))
                            dtpDocDate.ReadOnly = True
                            cboShift.Enabled = False
                        End If
                    End If
                    No_of_cans = CInt(clsCommon.myCdbl(grow.Cells("No of Cans").Value))
                    If clsCommon.myCdbl(No_of_cans) <= 0 Then
                        Throw New Exception("Fill No of Cans At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    Milk_Weight = Math.Round(clsCommon.myCdbl(grow.Cells("Milk Weight").Value), 2)
                    If clsCommon.myCdbl(Milk_Weight) <= 0 Then
                        Throw New Exception("Fill Milk Weight At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    totqty += Milk_Weight
                    Vlc_Is_Other = clsCommon.myCstr(grow.Cells("Vehicle is Other").Value)
                    SampleNo = clsCommon.myCstr(grow.Cells("Sample No").Value)
                    txtNoOfCans.Tag = SampleNo
                    Dim strSample_No As String = Checksaveddata(clsCommon.myCdbl(SampleNo), clsCommon.myCdbl(SampleNo), trans)
                    If clsCommon.myLen(strSample_No) > 0 Then
                        clsCommon.ProgressBarHide()
                        clsCommon.MyMessageBoxShow(strSample_No & " have been Already saved")
                        trans.Rollback()
                        Exit Sub
                    End If
                    If Checksavedsample(clsCommon.myCdbl(SampleNo), clsCommon.myCdbl(SampleNo), trans) = False Then
                        GoTo a
                    End If
                    Vlc_Code = clsCommon.myCstr(grow.Cells("Vlc code").Value).Replace("'", "`")
                    Vlc_name = clsCommon.myCstr(grow.Cells("Vlc name").Value)
                    If clsCommon.myLen(Vlc_Code) <= 0 Then 'AndAlso clsCommon.myLen(Vlc_name) <= 0 Then
                        Throw New Exception("Fill VLC Code At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(Vlc_Code) > 30 Then
                        Throw New Exception("Length Of VLC Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.myLen(Vlc_Code) > 0 Then
                        qry = "select count(*) from tspl_vlc_master_Head where vlc_code_Vlc_Uploader='" + Vlc_Code + "'"
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))

                        If check <= 0 Then
                            Throw New Exception("VLC Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Vlc Master")
                        End If
                    End If
                    qry = "select TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.vlc_code as [Vlc Code]," _
                            & " TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehicle Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code]," _
                            & " TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name]," _
                            & " TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as " _
                            & " [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date]" _
                            & " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and " _
                            & " TSPL_VENDOR_MASTER.Form_Type='VSP' left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc   where VLC_Code_vlc_uploader='" & Vlc_Code & "' and mcc_code='" & Mcccode & "'" ' " & IIf(ChkALLVLC.Checked = True, "", " and Route_Code='" & fndRouteCode.Value & "'") & ""

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt.Rows.Count > 0 Then
                        fndVLCCode.Value = clsCommon.myCstr(dt.Rows(0).Item("Uploader_Code"))
                        fndVLCCode.Tag = clsCommon.myCstr(dt.Rows(0).Item("Vlc Code"))
                        If clsCommon.myLen(fndVLCCode.Tag) > 0 Then
                            qry = ""
                            qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VSP_Code,VSP.Vendor_Name AS VSP_NAME,TSPL_VLC_MASTER_HEAD.ROUTE_CODE,tspl_mcc_route_master.ROUTE_NAME from TSPL_VLC_MASTER_HEAD " & _
                                  " LEFT JOIN tspl_mcc_route_master ON TSPL_VLC_MASTER_HEAD.ROUTE_CODE=tspl_mcc_route_master.ROUTE_CODE " & _
                                  " LEFT JOIN TSPL_VENDOR_MASTER as VSP ON TSPL_VLC_MASTER_HEAD.VSP_Code=VSP.Vendor_Code " & _
                                  " WHERE TSPL_VLC_MASTER_HEAD.vlc_code='" & fndVLCCode.Tag & "'"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt.Rows.Count > 0 Then
                                lblVLCDesc.Text = dt.Rows(0).Item("VLC_Name")
                                fndVspCode.Text = dt.Rows(0).Item("VSP_Code")
                                lblVSPDesc.Text = dt.Rows(0).Item("VSP_NAME")
                            End If
                        End If
                    End If
                    routecode = clsCommon.myCstr(grow.Cells("Route Code").Value)
                    routename = clsCommon.myCstr(grow.Cells("Route Name").Value)
                    If clsCommon.myLen(routecode) <= 0 Then 'AndAlso clsCommon.myLen(routename) <= 0 Then
                        Throw New Exception("Fill Route Code/Route Name At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(routecode) > 30 Then
                        Throw New Exception("Length Of Route Code Should Not Exceed 30 Characters,Check It At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If clsCommon.myLen(routecode) > 0 Then
                        qry = "select count(*) from TSPL_MCC_ROUTE_MASTER where route_code='" + routecode + "' "
                        check = CInt(clsDBFuncationality.getSingleValue(qry, trans))
                        If check <= 0 Then
                            Throw New Exception("Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Route Master And Mapped It With Correspondance State.")
                        End If
                    End If
                    qry = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Description],TSPL_Primary_Vehicle_Master.* " _
                     & " from TSPL_MCC_ROUTE_MASTER inner join TSPL_Primary_Vehicle_Master on TSPL_Primary_Vehicle_Master.vehicle_COde=TSPL_MCC_ROUTE_MASTER.vehicle_Code where TSPL_MCC_ROUTE_MASTER.route_code='" & routecode & "'" 'and TSPL_MCC_ROUTE_MASTER.mcc_code='" & fndMCCCode.Value & "' and active=1"
                    Dim dr As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                    If dr.Rows.Count > 0 Then
                        fndRouteCode.Value = clsCommon.myCstr(dr.Rows(0)("code")) 'gv.CurrentRow.Cells(colRoutecode).Value
                        lblRouteDesc.Text = clsCommon.myCstr(dr.Rows(0)("Route Description")) 'gv.CurrentRow.Cells(colRoutename).Value
                        fndVehicleCode.Text = dr.Rows(0)("Vehicle_Code")
                        txtVehicle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & fndVehicleCode.Text & "'"))
                    Else
                        Throw New Exception("Route Code Does Not Exist At Line No. " + clsCommon.myCstr(counter) + ",First Do Entry Of Route Master And Mapped It With Correspondance State.")
                    End If
                    Dim Other_VLc As Integer = 0
                    qry = "select * from tspl_vlc_master_Head where vlc_code_vlc_uploader='" & Vlc_Code & "' and route_code='" & routecode & "'"
                    Dim dtvlc As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtvlc.Rows.Count <= 0 Then
                        Other_VLc = 1
                    End If
                    '-------------------------------------------------
                    Dim isSaved As Boolean = True

                    Dim objHead As clsMilkReceiptMCC
                    Dim conv_fac As Double
                    Dim str As String = "select UOM_Code from TSPL_Mcc_UOM_DETAIL where stocking_unit='Y' and MCC_CODE='" & fndMCCCode.Value & "' "
                    Dim Unit_Code As String = clsDBFuncationality.getSingleValue(str, trans)
                    If Unit_Code = "" Then
                        clsCommon.ProgressBarHide()
                        clsCommon.MyMessageBoxShow(Me, "Fill UOM of Current Mcc", Me.Text)
                        Exit Sub
                    End If

                    txtTotalWeight.Text = Math.Round(totqty, 2)
                    conv_fac = clsWeightConversionInfo.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), trans)
                    objHead = New clsMilkReceiptMCC
                    objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    objHead.DOC_DATE = clsCommon.myCDate(ShiftDate)
                    objHead.SHIFT = clsCommon.myCstr(Shift)
                    objHead.COMM_PORT = UcWeighing1.Port
                    objHead.MCC_CODE = clsCommon.myCstr(Mcccode)
                    objHead.Irregular_MCC_CODE = clsCommon.myCstr(Irregular_Mcc_Code)
                    objHead.TOTAL_WEIGHT = clsCommon.myCdbl(txtTotalWeight.Text)
                    Dim objList As New List(Of clsMilkReceiptMCCDetail)

                    Dim obj As clsMilkReceiptMCCDetail
                    obj = New clsMilkReceiptMCCDetail
                    obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    obj.VLC_DOC_CODE = IIf(IsNothing(lblVLCCode.Tag), "", lblVLCCode.Tag)
                    obj.SAMPLE_NO = SampleNo
                    obj.VLC_CODE = clsCommon.myCstr(fndVLCCode.Tag)
                    obj.ROUTE_CODE = clsCommon.myCstr(routecode)
                    obj.VSP_CODE = clsCommon.myCstr(fndVspCode.Text)
                    obj.Item_CODE = clsCommon.myCstr(fndItem_Code.Text)
                    obj.VEHICLE_CODE = clsCommon.myCstr(fndVehicleCode.Text)
                    obj.Other_VEHICLE = IIf(clsCommon.CompairString(Vlc_Is_Other, "True") = CompairStringResult.Equal, "T", "F")
                    obj.Other_VLC = Other_VLc
                    obj.NO_OF_CANS = clsCommon.myCstr(No_of_cans)
                    obj.MILK_WEIGHT = clsCommon.myCdbl(Milk_Weight)
                    If clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal Then
                        obj.ACC_WEIGHT = clsCommon.myCdbl(Milk_Weight)
                        obj.LTR_WEIGHT = clsCommon.myCdbl(Milk_Weight * conv_fac)
                    Else
                        obj.LTR_WEIGHT = clsCommon.myCdbl(Milk_Weight)
                        obj.ACC_WEIGHT = clsCommon.myCdbl(Milk_Weight * conv_fac)
                    End If


                    obj.TYPE = clsCommon.myCstr(cboType.SelectedValue)
                    obj.MILK_TYPE = clsCommon.myCstr(cboMilkType.SelectedValue)
                    obj.SAMPLE_NO_VALUES = ""

                    obj.DOC_DATE = clsCommon.myCDate(ShiftDate)
                    obj.SHIFT = clsCommon.myCstr(Shift)
                    obj.COMM_PORT = UcWeighing1.Port
                    obj.MCC_CODE = clsCommon.myCstr(Mcccode)
                    obj.IS_ENTERED_MANUAL = "Y"
                    obj.UOM_Code = Unit_Code
                    obj.Conversion_Factor = conv_fac
                    objList.Add(obj)

                    If clsMilkReceiptMCC.SaveData(objHead, objList, trans) Then
                    End If
a:              Next
                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data Imported Successfully", Me.Text)
                LoadData(txtCode.Value)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub txtMilkWeight_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMilkWeight.Leave
        Try
            If txtMilkWeight.Enabled = True And clsCommon.myCdbl(txtMilkWeight.Text) > 0 And clsCommon.myCdbl(txtNoOfCans.Text) > 0 And (IIf(SplitContainer2.Panel1Collapsed, True, IIf(clsCommon.myLen(txtCan.Value) > 0, True, False))) Then
                SaveData()
            ElseIf clsCommon.myCdbl(txtNoOfCans.Text) <= 0 Then
                txtNoOfCans.Focus()
            ElseIf Not SplitContainer2.Panel1Collapsed AndAlso clsCommon.myLen(txtCan.Value) <= 0 Then
                txtCan.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub txtNoOfCans_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNoOfCans.Leave, txtCan.Leave
        Try
            If txtMilkWeight.Enabled = False And clsCommon.myCdbl(txtMilkWeight.Text) > 0 And clsCommon.myCdbl(txtNoOfCans.Text) > 0 And (IIf(SplitContainer2.Panel1Collapsed, True, IIf(clsCommon.myLen(txtCan.Value) > 0, True, False))) Then
                btnsave_Click("", e)
            ElseIf clsCommon.myLen(fndVLCCode.Value) <= 0 Then
                fndVLCCode.Focus()
            ElseIf clsCommon.myCdbl(txtNoOfCans.Text) <= 0 Then
                txtNoOfCans.Focus()
            ElseIf Not SplitContainer2.Panel1Collapsed AndAlso clsCommon.myLen(txtCan.Value) <= 0 Then
                txtCan.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        lbErrors.Items.Clear()
    End Sub

    Private Sub UcWeighing1__MYWeightChanged(Val As String) Handles UcWeighing1._MYWeightChanged
        Try
            txtMilkWeight.Text = Val
            lbErrors.Items.Add(Val)
            If MinimumReachedWeightValue > clsCommon.myCdbl(Val) Then
                MinimumReachedWeightValue = clsCommon.myCdbl(Val)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Try
            UcWeighing1.CloseCOMPORT()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtCan__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCan._MYValidating
        Dim obj As clsCanMaster = clsCanMaster.getFinderObeject("", txtCan.Value, isButtonClicked)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
            txtCan.Value = obj.Code
            txtCan.Tag = obj.Tare_Weight
        Else
            txtCan.Value = ""
            txtCan.Tag = 0
        End If
    End Sub

    
End Class
