Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
Imports System.IO.Ports
Imports XpertERPCommanServices

Public Class frmMilkRejectEntry
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim WeighmentNotMandatoryInMCC As Boolean = False
    Dim Irregular_Mcc_Code As String = String.Empty
    Dim IsAllowSaveWithoutShifttime As Boolean = False

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Public DtMilkReceipt As DataTable
    Dim frmRange As Integer = 0
    Dim MilkWeight_Setting As Double
    Dim Tolerance_Neg_Setting As Double
    Dim Tolerance_Positive_Setting As Double
    Public Shared isPortOpened As Boolean = False
    Dim ShiftTiming As String = String.Empty
    Dim isDoubleClicked As Boolean = False

    Dim dblSNFDeductionPer As Double = 0
    Dim dblFATDeductionPer As Double = 0
    Dim dblRetrunPenaltyPerUnit As Double = 1.5
    Dim dblDrainPenaltyPerUnit As Double = 15
    Dim dblCOBPenaltyPerUnit As Double = 0

    Dim dclFATSNFDeductionMixMilkFATMinValue As Decimal = 3.0
    Dim dclFATSNFDeductionMixMilkFATMaxValue As Decimal = 4.5
    Dim dclFATSNFDeductionMixMilkSNFMinValue As Decimal = 7.0
    Dim dclFATSNFDeductionMixMilkSNFMaxValue As Decimal = 7.2
    Dim dclFATSNFDeductionMixMilkDeductionPer As Decimal = 20

    Dim isBackEntry As Boolean = False
    Dim settNoOfPreNxtDayToPickAvgFATSNF As Integer = 0
    Dim settAlwaysVSPDefaulter As Boolean = False
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkReject)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        
    End Sub

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        isBackEntry = False
        GetshiftType()
        LoadRejectType()
        AddNew()
        WeighmentNotMandatoryInMCC = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.WeighmentNotMandatoryInMCC, clsFixedParameterCode.WeighmentNotMandatoryInMCC, Nothing)) = 1)
        dblSNFDeductionPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFDeductionPercent, clsFixedParameterCode.SNFDeductionPercent, Nothing))
        dblFATDeductionPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATDeductionPercent, clsFixedParameterCode.FATDeductionPercent, Nothing))
        dblRetrunPenaltyPerUnit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectionReturnPenaltyPerUnit, clsFixedParameterCode.RejectionReturnPaneltyPerUnit, Nothing))
        dblDrainPenaltyPerUnit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectionDrainPenaltyPerUnit, clsFixedParameterCode.RejectionDrainPenaltyPerUnit, Nothing))
        dblCOBPenaltyPerUnit = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RejectionCOBPenaltyPerUnit, clsFixedParameterCode.RejectionCOBPenaltyPerUnit, Nothing))
        settAlwaysVSPDefaulter = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AlwaysVSPDefaulter, clsFixedParameterCode.AlwaysVSPDefaulter, Nothing)) = 1)

        dclFATSNFDeductionMixMilkFATMinValue = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFDeductionMixMilkFATMinValue, clsFixedParameterCode.FATSNFDeductionMixMilkFATMinValue, Nothing))
        dclFATSNFDeductionMixMilkFATMaxValue = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFDeductionMixMilkFATMaxValue, clsFixedParameterCode.FATSNFDeductionMixMilkFATMaxValue, Nothing))
        dclFATSNFDeductionMixMilkSNFMinValue = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFDeductionMixMilkSNFMinValue, clsFixedParameterCode.FATSNFDeductionMixMilkSNFMinValue, Nothing))
        dclFATSNFDeductionMixMilkSNFMaxValue = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFDeductionMixMilkSNFMaxValue, clsFixedParameterCode.FATSNFDeductionMixMilkSNFMaxValue, Nothing))
        dclFATSNFDeductionMixMilkDeductionPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FATSNFDeductionMixMilkDeductionPer, clsFixedParameterCode.FATSNFDeductionMixMilkDeductionPer, Nothing))

        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        If ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = True Then
            lblVehicle.Visible = True
            txtVehicle.Visible = True
        Else
            lblVehicle.Visible = False
            txtVehicle.Visible = False
        End If
        'BHA/09/05/18-000020 by balwinder on 28/05/2018
        settNoOfPreNxtDayToPickAvgFATSNF = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.NoOfPreNxtDayToPickAvgFATSNF, clsFixedParameterCode.NoOfPreNxtDayToPickAvgFATSNF, Nothing))
        dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Me.fndMCCCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join TSPL_Mcc_MASTER on TSPL_Mcc_MASTER.mcc_code=Default_Location where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCCode.Value + "' "))
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowEditRow = False
        gv1.MasterTemplate.AllowCellContextMenu = True
        gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv1.MasterTemplate.AllowDeleteRow = True
        If clsCommon.myCstr(fndMCCCode.Value) <> "" Then
            Dim DTShift As DataTable = clsMilkRejectHead.GetShift(fndMCCCode.Value)
            If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                Throw New Exception("No shift is opened. one Shift Must be Opened..")
            ElseIf DTShift.Rows.Count > 1 Then
                Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
            Else
                dtpDocDate.Value = clsCommon.myCstr(DTShift.Rows(0).Item("MCC_Shift_date"))
                cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))
                LblUom.Text = clsCommon.myCstr(DTShift.Rows(0).Item("Uom_Code"))
                Irregular_Mcc_Code = clsCommon.myCstr(DTShift.Rows(0).Item("Irregular_Mcc_Code"))
                dtpDocDate.ReadOnly = True
                'cboShift.Enabled = False
            End If
        End If
        If clsCommon.MyMessageBoxShow("Have you completed Normal weighment?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Me.Close()
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow("Are you Sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Me.Close()
            Exit Sub
        End If
        ReStoreGridLayout()
        frmRange = 0
        MilkWeight_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
        Tolerance_Neg_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Tolerance_Neg, clsFixedParameterCode.MilkSetting, Nothing)
        Tolerance_Positive_Setting = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Tolerance_Positive, clsFixedParameterCode.MilkSetting, Nothing)
        txtCode.Value = clsMilkRejectHead.GetDocCode(clsCommon.GetPrintDate(dtpDocDate.Value, "dd-MMM-yyyy"), fndMCCCode.Value, cboShift.SelectedValue, Nothing)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value)
        End If
        fndRouteCode.Focus()
        ShiftTiming = clsFixedParameter.GetData(clsFixedParameterType.ShiftTiming, clsFixedParameterCode.ShiftTiming, Nothing)
        txtMilkWeight.Enabled = True
        

    End Sub

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim objHead As clsMilkRejectHead
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
                Dim totqty As Double = 0
                For Each grow As GridViewRowInfo In gv1.Rows
                    totqty += grow.Cells("MILK WEIGHT").Value
                Next
                txtTotalWeight.Text = Math.Round(totqty, 2)
                conv_fac = clsMilkRejectHead.GetConversion_factor(Unit_Code, IIf(clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal, "LTR", "KG"), Nothing)
                objHead = New clsMilkRejectHead
                objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
                objHead.MCC_CODE = clsCommon.myCstr(fndMCCCode.Value)
                objHead.TOTAL_WEIGHT = clsCommon.myCdbl(txtTotalWeight.Text) + IIf(btnsave.Text = "Update", clsCommon.myCdbl(txtMilkWeight.Text) - clsCommon.myCdbl(txtMilkWeight.Tag), clsCommon.myCdbl(txtMilkWeight.Text))

                objHead.Arr = New List(Of clsMilkRejectDetail)

                Dim obj As clsMilkRejectDetail
                obj = New clsMilkRejectDetail
                obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                If clsCommon.CompairString(btnsave.Text, "Update") = CompairStringResult.Equal Then
                    obj.SAMPLE_NO = frmRange
                Else
                    Try
                        obj.SAMPLE_NO = gv1.Rows.Count + 1
                    Catch ex As Exception
                        obj.SAMPLE_NO = 1
                    End Try
                End If
                obj.VLC_CODE = clsCommon.myCstr(fndVLCCode.Tag)
                obj.ROUTE_CODE = clsCommon.myCstr(fndRouteCode.Value)
                obj.VSP_CODE = clsCommon.myCstr(fndVspCode.Text)
                obj.Item_CODE = clsCommon.myCstr(fndItem_Code.Text)
                obj.VEHICLE_CODE = clsCommon.myCstr(fndVehicleCode.Text)
                obj.Other_VEHICLE = chkOther.Checked
                obj.Other_VLC = ChkALLVLC.Checked
                obj.NO_OF_CANS = clsCommon.myCstr(txtNoOfCans.Text)
                obj.MILK_WEIGHT = clsCommon.myCdbl(txtMilkWeight.Text)
                obj.FAT = txtFatPer.Value
                obj.SNF = txtSNFPer.Value
                obj.Reject_Type = clsCommon.myCstr(cboRejectType.SelectedValue)
                If clsCommon.CompairString(Unit_Code, "KG") = CompairStringResult.Equal Then
                    obj.ACC_WEIGHT_KG = clsCommon.myCdbl(txtMilkWeight.Text)
                    obj.ACC_WEIGHT_LTR = clsCommon.myCdbl(txtMilkWeight.Text * conv_fac)
                Else
                    obj.ACC_WEIGHT_LTR = clsCommon.myCdbl(txtMilkWeight.Text)
                    obj.ACC_WEIGHT_KG = clsCommon.myCdbl(txtMilkWeight.Text * conv_fac)
                End If
                obj.SNF_Deduction_Per = dblSNFDeductionPer
                obj.FAT_Deduction_Per = dblFATDeductionPer

                If rbtnReturnTypeReturn.IsChecked Then
                    obj.Is_Return = 1
                    obj.dblPenaltyPerUnit = dblRetrunPenaltyPerUnit
                ElseIf rbtnReturnTypeDrain.IsChecked Then
                    obj.Is_Return = 2
                    obj.dblPenaltyPerUnit = dblDrainPenaltyPerUnit
                ElseIf rbtnReturnTypeCOB.IsChecked Then
                    obj.Is_Return = 3
                    obj.dblPenaltyPerUnit = dblCOBPenaltyPerUnit
                Else
                    obj.Is_Return = 0
                    obj.dblPenaltyPerUnit = 0
                End If
                obj.dclFATSNFDeductionMixMilkFATMinValue = dclFATSNFDeductionMixMilkFATMinValue
                obj.dclFATSNFDeductionMixMilkFATMaxValue = dclFATSNFDeductionMixMilkFATMaxValue
                obj.dclFATSNFDeductionMixMilkSNFMinValue = dclFATSNFDeductionMixMilkSNFMinValue
                obj.dclFATSNFDeductionMixMilkSNFMaxValue = dclFATSNFDeductionMixMilkSNFMaxValue
                obj.dclFATSNFDeductionMixMilkDeductionPer = dclFATSNFDeductionMixMilkDeductionPer


                obj.UOM_Code = Unit_Code
                obj.Conversion_Factor = conv_fac
                objHead.Arr.Add(obj)
                If clsMilkRejectHead.SaveData(objHead, isBackEntry) Then
                    If btnsave.Text = ">>" Then
                        frmRange += 1
                    End If
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
            'If btnsave.Text = "Update" Then
            Dim strchk As String = "select Posted from TSPL_MILK_REJECT_HEAD where DOC_COde='" + txtCode.Value + "'"
            Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
            ' Else
            'Dim str As String = Checksaveddata(frmRange, frmRange)
            'If clsCommon.myLen(str) > 0 And clsCommon.CompairString(btnsave.Text, "Update") <> CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow(str & " have been Already saved")
            '    Return False
            'End If
            'If btnsave.Text <> ">>" Then
            '    If Checksavedsample(0, 0) = False Then
            '        Return False
            '    End If
            'End If
            ' End If
            'If clsCommon.CompairString(clsCommon.myCstr(cboRejectType.Text), "Rejected") = CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow("Milk Type can not be Rejected.")
            '    cboRejectType.Focus()
            '    Return False
            'End If
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

            If clsCommon.myLen(cboRejectType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Reject Type", Me.Text)
                cboRejectType.Focus()
                Return False
            End If
            If rbtnReturnTypeNA.IsChecked Then
                If txtFatPer.Value <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter fat %", Me.Text)
                    txtFatPer.Focus()
                    Return False
                End If
                If txtFatPer.Value > 15 Then
                    clsCommon.MyMessageBoxShow(Me, "Please fat % cannot be more than 15 %", Me.Text)
                    txtFatPer.Focus()
                    Return False
                End If

                If txtSNFPer.Value <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please enter SNF %", Me.Text)
                    txtSNFPer.Focus()
                    Return False
                End If
                If txtSNFPer.Value > 15 Then
                    clsCommon.MyMessageBoxShow(Me, "Please SNF % cannot be more than 15 %", Me.Text)
                    txtSNFPer.Focus()
                    Return False
                End If
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


            If clsCommon.myLen(Me.cboRejectType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Milk Reject Type", Me.Text)
                Return False
            End If
            If clsCommon.myLen(Me.fndRouteCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Route Code", Me.Text)
                fndRouteCode.Focus()
                Return False
            End If

            If clsCommon.myCdbl(txtMilkWeight.Text) >= (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting - (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Neg_Setting / 100)) And clsCommon.myCdbl(txtMilkWeight.Text) <= (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting + (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Positive_Setting / 100)) Then
            Else
                clsCommon.MyMessageBoxShow("Please Check Milk Weight Its Value Should be Between [" & Math.Round((clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting - (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Neg_Setting / 100)), 2) & "] and [" & (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting + (clsCommon.myCdbl(txtNoOfCans.Text) * MilkWeight_Setting * Tolerance_Positive_Setting / 100)) & "]")
                If txtMilkWeight.Enabled = True Then
                    txtMilkWeight.Focus()
                Else
                    txtNoOfCans.Focus()
                End If
                Return False
            End If
            If IsAllowSaveWithoutShifttime = False Then
                If ShiftTiming = "1" Then
                    Dim IsOpenShift As Integer = ClsOpenMCCShift.CheckisShiftTimingAvailable(fndMCCCode.Value, cboShift.SelectedValue)
                    If IsOpenShift <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Shift Timing Excluded.Can't be Save.", Me.Text)
                        Return False
                    End If
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
        gv1.DataSource = Nothing
        ChkALLVLC.Checked = False
        Me.fndVLCCode.Value = Nothing
        Me.fndVLCCode.Tag = Nothing
        Me.fndVspCode.Text = Nothing
        txtMilkWeight.Text = Nothing
        txtTotalWeight.Text = Nothing
        TxtTotalWeightallRoute.Text = Nothing
        TotalCansAllRoute.Text = Nothing
        lblVSPDesc.Text = Nothing
        lblVLCDesc.Text = Nothing
        Me.txtMilkWeight.Text = Nothing
        Me.txtNoOfCans.Text = Nothing
        Me.cboRejectType.SelectedValue = 0
        txtFatPer.Value = 0
        txtSNFPer.Value = 0
        lblVLCCode.Tag = Nothing
        'cboShift.Enabled = True
        'cboShift.Enabled = True
        fndItem_Code.Text = Nothing
        lblItemDesc.Text = Nothing
        cboRejectType.SelectedValue = ""
        btnsave.Enabled = True
        fndItem_Code.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)
        lblItemDesc.Text = clsItemMaster.GetItemName(fndItem_Code.Text, Nothing)
        Me.fndMCCCode.Enabled = True
        Me.fndVLCCode.Enabled = True
        Me.fndVspCode.Enabled = True
        Me.fndRouteCode.Enabled = True
        Me.fndVehicleCode.Enabled = True
        Me.chkOther.Checked = False
        Me.fndItem_Code.Enabled = True

        gv1.Rows.Clear()
        Me.gv1.DataSource = Nothing
        fndRouteCode.Focus()
        dtpDocDate.Enabled = False
        cboShift.Enabled = False
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

    Public Sub LoadRejectType()
        isInsideLoadData = True
        cboRejectType.DataSource = clsMilkRejectType.GetCombo(True)
        cboRejectType.ValueMember = "Code"
        cboRejectType.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub

    Sub LoadDatainControls(ByVal vlccode As String)
        txtCode.Value = ""
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Me.cboShift.SelectedIndex = -1
        Me.fndMCCCode.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Me.fndVLCCode.Value = Nothing
        Me.fndVLCCode.Tag = Nothing
        Me.fndVspCode.Text = Nothing
        'Me.fndRouteCode.Value = Nothing
        txtMilkWeight.Text = Nothing
        txtTotalWeight.Text = Nothing
        TxtTotalWeightallRoute.Text = Nothing
        TotalCansAllRoute.Text = Nothing
        ' lblRouteDesc.Text = Nothing
        fndVehicleCode.Text = Nothing
        lblVSPDesc.Text = Nothing
        lblVLCDesc.Text = Nothing
        Me.fndVehicleCode.Text = Nothing
        Me.chkOther.Checked = False
        Me.txtMilkWeight.Text = 0
        Me.txtNoOfCans.Text = 0
        Me.cboRejectType.SelectedIndex = -1

        fndItem_Code.Text = Nothing
        lblItemDesc.Text = Nothing
        txtVehicle.Text = ""

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
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
        ElseIf e.Alt And e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = clsFixedParameterType.SIRC
            frm.strCode = clsFixedParameterCode.SIReversAndCreate
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                isBackEntry = True
                RadButton1.Visible = True
                RadButton2.Visible = True
                RadButton3.Visible = True
                RadButton4.Visible = True
            End If
        End If
    End Sub

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            btnsave.Enabled = True
            AddNew()
            btnsave.Text = ">>"
            Dim obj As clsMilkRejectHead = clsMilkRejectHead.GetData(strDoc, navType)
            gv1.DataSource = obj.Gettable(obj.DOC_CODE, NavigatorType.Current)
            gv1.BestFitColumns()
            UsLock1.Status = obj.POSTED
            txtCode.Value = obj.DOC_CODE
            dtpDocDate.Value = obj.DOC_DATE
            cboShift.SelectedValue = obj.SHIFT

            ReStoreGridLayout()
            txtCode.Value = obj.DOC_CODE
            gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
            gv1.AllowDeleteRow = True
            gv1.AutoScroll = True
            isDoubleClicked = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub dtpDocDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDocDate.LostFocus
        Me.txtCode.Value = clsMilkRejectHead.GetDocCode(Me.dtpDocDate.Value, Me.fndMCCCode.Value, Me.cboShift.Text)
    End Sub

    Private Sub cboShift_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboShift.SelectedIndexChanged
        If Not isInsideLoadData Then
            Me.txtCode.Value = clsMilkRejectHead.GetDocCode(Me.dtpDocDate.Value, Me.fndMCCCode.Value, Me.cboShift.SelectedValue)
        End If
    End Sub

    Private Sub fndMCCCode_Validating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMCCCode._MYValidating
        Dim sQuery As String = "select Location_Category from tspl_location_master where Location_Code='" & fndMCCCode.Value & "'"
        Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
        fndMCCCode.Value = clsMccMaster.getFinder("", fndMCCCode.Value, isButtonClicked)
        Dim DTShift As DataTable = clsMilkRejectHead.GetShift(fndMCCCode.Value)
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
            End If
            Irregular_Mcc_Code = clsCommon.myCstr(DTShift.Rows(0).Item("Irregular_Mcc_Code"))
        End If
        Try
            Me.txtCode.Value = clsMilkRejectHead.GetDocCode(Me.dtpDocDate.Value, Me.fndMCCCode.Value, Me.cboShift.Text)
        Catch ex As Exception
        End Try
        Me.LblMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Name from TSPL_Mcc_MASTER where MCC_Code='" + fndMCCCode.Value + "' "))
    End Sub

    Private Sub fndVLCCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVLCCode._MYValidating
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
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
                            & " TSPL_VENDOR_MASTER.Form_Type='VSP' left join tspl_Mcc_Route_Master on tspl_Mcc_Route_Master.route_code=TSPL_VLC_MASTER_HEAD.route_code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc where tspl_mcc_master.mcc_Code='" & fndMCCCode.Value & "' " & IIf(ChkALLVLC.Checked = True, "", " and TSPL_VLC_MASTER_HEAD.Route_Code='" & fndRouteCode.Value & "'") & " and TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader='" & fndVLCCode.Value & "' and TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0 "
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
                qry = "select TSPL_VLC_MASTER_HEAD.vlc_code_vlc_uploader as [Uploader_Code],TSPL_VLC_MASTER_HEAD.vlc_code as [Vlc Code],TSPL_VLC_MASTER_HEAD.route_code as [Route Code],Route_name as [Route Name]," _
                & " TSPL_VLC_MASTER_HEAD.vlc_name as [VLC Name],TSPL_VLC_MASTER_HEAD.vehical_name as [Vehicle Name],TSPL_VLC_MASTER_HEAD.vsp_code as [VSP Code]," _
                & " TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.mcc as [MCC Code],TSPL_MCC_MASTER.mcc_name as [MCC Name]," _
                & " TSPL_VLC_MASTER_HEAD.created_by as [Created By],TSPL_VLC_MASTER_HEAD.created_date as [Created Date],TSPL_VLC_MASTER_HEAD.modified_by as " _
                & " [Modified By],TSPL_VLC_MASTER_HEAD.modified_date as [Modified Date]" _
                & " from TSPL_VLC_MASTER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.vsp_code and " _
                & " TSPL_VENDOR_MASTER.Form_Type='VSP' left join tspl_Mcc_Route_Master on tspl_Mcc_Route_Master.route_code=TSPL_VLC_MASTER_HEAD.route_code  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_VLC_MASTER_HEAD.mcc  "

                fndVLCCode.Value = clsCommon.ShowSelectForm("MRVLCFND_Rej", qry, "Uploader_Code", " TSPL_VENDOR_MASTER.Is_Inactive_In_Milk_Procurement=0 and TSPL_VLC_MASTER_HEAD.Active=1 and tspl_mcc_master.mcc_Code='" & fndMCCCode.Value & "' " & IIf(ChkALLVLC.Checked = True, "", " and TSPL_VLC_MASTER_HEAD.Route_Code='" & fndRouteCode.Value & "'") & "", fndVLCCode.Value, "Uploader_Code", isButtonClicked)
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
                    If clsCommon.myLen(fndVLCCode.Tag) > 0 Then
                        qry = ""
                        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VSP_Code,VSP.Vendor_Name AS VSP_NAME,TSPL_VLC_MASTER_HEAD.ROUTE_CODE,tspl_mcc_route_master.ROUTE_NAME from TSPL_VLC_MASTER_HEAD " & _
                              " LEFT JOIN tspl_mcc_route_master ON TSPL_VLC_MASTER_HEAD.ROUTE_CODE=tspl_mcc_route_master.ROUTE_CODE " & _
                              " LEFT JOIN TSPL_VENDOR_MASTER as VSP ON TSPL_VLC_MASTER_HEAD.VSP_Code=VSP.Vendor_Code " & _
                              " WHERE TSPL_VLC_MASTER_HEAD.vlc_code='" & fndVLCCode.Tag & "'"
                        dt = clsDBFuncationality.GetDataTable(qry)
                        If dt.Rows.Count > 0 Then
                            lblVLCDesc.Text = dt.Rows(0).Item("VLC_Name")
                            fndVspCode.Text = dt.Rows(0).Item("VSP_Code")
                            lblVSPDesc.Text = dt.Rows(0).Item("VSP_NAME")
                        End If
                    End If
                End If
                If settNoOfPreNxtDayToPickAvgFATSNF > 0 Then
                    If clsCommon.myLen(fndVLCCode.Tag) > 0 Then
                        Dim obj As clsFatSnfRateCalculator = clsMilkRejectDetail.GetAvgFATSNF(fndVLCCode.Tag, dtpDocDate.Value, settNoOfPreNxtDayToPickAvgFATSNF, Nothing)
                        txtFatPer.Value = obj.fatR
                        txtSNFPer.Value = obj.snfR
                    End If
                End If
                cboRejectType.Focus()
                isCellValueChangedOpen = False
            End If
            cboRejectType.Focus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "SELECT DOC_CODE as Code,convert(varchar,DOC_DATE,103) as Date,MCC_CODE as [Mcc Code],case when Posted=1 then 'Posted' else 'Pending' end as Status  FROM TSPL_MILK_REJECT_HEAD"
            txtCode.Value = clsCommon.ShowSelectForm("MRMILKReje", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)


            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                'cboShift.Enabled = False
                ' gv1.Rows.Add(gv1.CurrentRow)
            End If
            qry = String.Empty
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Column Is gv1.Columns("Defaulter") Then
                If isBackEntry Then
                    Dim dt As DataTable = New DataTable()
                    dt.Columns.Add("Code", GetType(String))
                    dt.Columns.Add("Name", GetType(String))

                    Dim dr As DataRow = dt.NewRow()
                    dr("Code") = "VSP"
                    dr("Name") = "VSP"
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("Code") = "Transporter"
                    dr("Name") = "Transporter"
                    dt.Rows.Add(dr)

                    dr = dt.NewRow()
                    dr("Code") = "Company"
                    dr("Name") = "Company"
                    dt.Rows.Add(dr)

                    Dim frm As New FrmFreeComboBox()
                    frm.ComboSource = dt
                    frm.ComboValueMember = "Code"
                    frm.ComboDisplayMember = "Name"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRetValue) > 0 Then
                        gv1.CurrentRow.Cells("Defaulter").Value = frm.strRetValue
                    End If
                    Exit Sub
                End If
            End If


            Me.fndMCCCode.Enabled = True
            Me.fndVLCCode.Enabled = True
            Me.fndRouteCode.Enabled = True
            If btnsave.Visible = False Then
                clsCommon.MyMessageBoxShow(Me, "You do Not have Permission to Update Any Record.", Me.Text)
                Exit Sub
            End If
            Dim trans As SqlTransaction = Nothing
            Dim totqty As Double
            Dim obj As clsMilkRejectHead = clsMilkRejectHead.GetData(txtCode.Value, NavigatorType.Current, Nothing, clsCommon.myCdbl(gv1.CurrentRow.Cells("SAMPLE NO").Value))
            If obj IsNot Nothing AndAlso obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                For Each objtr As clsMilkRejectDetail In obj.Arr
                    Me.dtpDocDate.Value = obj.DOC_DATE
                    Me.cboShift.SelectedValue = obj.SHIFT
                    Me.fndMCCCode.Value = obj.MCC_CODE
                    Me.fndVLCCode.Tag = objtr.VLC_CODE
                    Me.fndVLCCode.Value = objtr.VLC_CODE_Uploader_Code
                    Me.fndVspCode.Text = objtr.VSP_CODE
                    Me.fndRouteCode.Value = objtr.ROUTE_CODE
                    Me.chkOther.Checked = objtr.Other_VEHICLE
                    Me.ChkALLVLC.Checked = objtr.Other_VLC
                    txtMilkWeight.Text = objtr.MILK_WEIGHT
                    txtMilkWeight.Tag = objtr.MILK_WEIGHT
                    fndItem_Code.Text = objtr.Item_CODE
                    txtTotalWeight.Text = Nothing
                    TxtTotalWeightallRoute.Text = Nothing
                    TotalCansAllRoute.Text = Nothing
                    lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCode.Value & "'")
                    lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVspCode.Text & "'")
                    lblVLCDesc.Text = clsDBFuncationality.getSingleValue("select vlc_name from TSPL_VLC_MASTER_HEAD where VLC_Code='" & fndVLCCode.Tag & "'")
                    lblItemDesc.Text = clsItemMaster.GetItemName(fndItem_Code.Text, Nothing)
                    Me.fndVehicleCode.Text = objtr.VEHICLE_CODE
                    txtVehicle.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & fndVehicleCode.Text & "'"))
                    Me.txtMilkWeight.Text = objtr.MILK_WEIGHT
                    Me.txtNoOfCans.Text = objtr.NO_OF_CANS
                    Me.txtNoOfCans.Tag = objtr.SAMPLE_NO
                    lblVLCCode.Tag = objtr.VLC_CODE
                    txtFatPer.Value = objtr.FAT
                    txtSNFPer.Value = objtr.SNF
                    cboRejectType.SelectedValue = objtr.Reject_Type
                    If objtr.Is_Return = 1 Then
                        rbtnReturnTypeReturn.IsChecked = True
                    ElseIf objtr.Is_Return = 2 Then
                        rbtnReturnTypeDrain.IsChecked = True
                    ElseIf objtr.Is_Return = 3 Then
                        rbtnReturnTypeCOB.IsChecked = True
                    Else
                        rbtnReturnTypeNA.IsChecked = True
                    End If
                    btnsave.Text = "Update"
                    frmRange = objtr.SAMPLE_NO
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
                cboRejectType.Focus()
                isDoubleClicked = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AddNew()
        IsAllowSaveWithoutShifttime = False
    End Sub

    Private Sub gv1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
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
                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("MRROTFND2", qry)

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
        obj.ReportID = "MilkRejGrid"
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData("MilkRejGrid", objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkRejGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Public Function Checksaveddata(ByVal str_frm_range As String, ByVal str_to_range As String, Optional ByVal trans As SqlTransaction = Nothing)
        Dim strserial As String = ""
        'Dim qry As String = "select * from TSPL_MILK_REJECT_DETAIL WHERE MCC_CODE='" & fndMCCCode.Value & "' and convert(date,DOC_DATE,103)=" _
        '   & " '" & clsCommon.GetPrintDate(dtpDocDate.Value, "dd/MMM/yyyy") & "'  and SHIFT='" & cboShift.SelectedValue & "' " _
        '   & " and sample_no>=" & clsCommon.myCdbl(str_frm_range) & " and sample_no<=" & clsCommon.myCdbl(str_to_range) & " "
        'Dim Dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'If Dt.Rows.Count > 0 Then
        '    For Each row As DataRow In Dt.Rows()
        '        strserial = IIf(strserial = "", "Sample No -" & row.Item("sample_no"), strserial & "," & row.Item("sample_no"))
        '    Next
        'End If
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

    Private Sub txtNoOfCans_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNoOfCans.Leave, txtMilkWeight.Leave
        Try
            Dim boolReturn As Boolean = IIf(rbtnReturnTypeReturn.IsChecked OrElse rbtnReturnTypeDrain.IsChecked OrElse rbtnReturnTypeCOB.IsChecked, True, IIf(txtSNFPer.Value > 0 And txtFatPer.Value > 0, True, False))
            If Not isDoubleClicked And txtMilkWeight.Value > 0 And txtNoOfCans.Value > 0 And boolReturn And clsCommon.myLen(clsCommon.myCstr(cboRejectType.SelectedValue)) > 0 Then
                btnsave_Click("", e)
            ElseIf clsCommon.myLen(fndVLCCode.Value) <= 0 Then
                fndVLCCode.Focus()
            ElseIf clsCommon.myLen(clsCommon.myCstr(cboRejectType.SelectedValue)) <= 0 Then
                cboRejectType.Focus()
            ElseIf txtFatPer.Value <= 0 Then
                txtFatPer.Focus()
            ElseIf txtSNFPer.Value <= 0 Then
                txtSNFPer.Focus()
            ElseIf clsCommon.myCdbl(txtNoOfCans.Text) <= 0 Then
                txtNoOfCans.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub cboRejectType_Validating(sender As Object, e As CancelEventArgs) Handles cboRejectType.Validating
        txtFatPer.Focus()
    End Sub

    Private Sub txtFatPer_Validating(sender As Object, e As CancelEventArgs) Handles txtFatPer.Validating
        If txtFatPer.Value > 15 Then
            clsCommon.MyMessageBoxShow(Me, "Max Fat% is 15", Me.Text)
            txtFatPer.Focus()
        Else
            txtSNFPer.Focus()
        End If
    End Sub

    Private Sub txtSNFPer_Validating(sender As Object, e As CancelEventArgs) Handles txtSNFPer.Validating
        If txtSNFPer.Value > 15 Then
            clsCommon.MyMessageBoxShow(Me, "Max SNF % is 15", Me.Text)
            txtSNFPer.Focus()
        Else
            txtNoOfCans.Focus()
        End If
    End Sub

    Private Sub txtNoOfCans_Validating(sender As Object, e As CancelEventArgs) Handles txtNoOfCans.Validating
        txtMilkWeight.Focus()
    End Sub

    Private Sub fndVLCCode_Leave(sender As Object, e As EventArgs) Handles fndVLCCode.Leave
        cboRejectType.Focus()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If UsLock1.Status = ERPTransactionStatus.Approved Then
                Throw New Exception("Posted transaction")
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells("Defaulter").Value) <= 0 Then
                    Throw New Exception("Please set defaulter at row no " + clsCommon.myCstr(ii + 1))
                End If
                Dim qry As String = "Update TSPL_MILK_REJECT_DETAIL Set Defaulter='" + clsCommon.myCstr(gv1.Rows(ii).Cells("Defaulter").Value) + "' where DOC_CODE ='" + txtCode.Value + "' and SAMPLE_NO = '" + clsCommon.myCstr(gv1.Rows(ii).Cells("SAMPLE NO").Value) + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
            Next
            If clsCommon.MyMessageBoxShow("Post the current document " + Environment.NewLine + "are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    clsMilkRejectHead.PostData(txtCode.Value, True, tran)
                    tran.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Posted successfully", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                Catch ex As Exception
                    tran.Rollback()
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Dim qry As String = "select MCC_SHIFT_CODE, MCC_CODE,MCC_SHIFT_DATE,SHIFT from TSPL_OPEN_MCC_SHIFT"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("dda112", qry)
        If dr IsNot Nothing AndAlso dr.ItemArray.Count > 0 Then
            qry = "select DOC_CODE from TSPL_MILK_REJECT_HEAD where convert(date, DOC_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(clsCommon.myCDate(dr("MCC_SHIFT_DATE")), "dd/MMM/yyyy") + "',103) and SHIFT='" + clsCommon.myCstr(dr("SHIFT")) + "' and MCC_CODE='" + clsCommon.myCstr(dr("MCC_CODE")) + "' and Posted=0"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                LoadData(clsCommon.myCstr(dt.Rows(0)("DOC_CODE")), NavigatorType.Current)
            Else
                dtpDocDate.Value = clsCommon.myCDate(dr("MCC_SHIFT_DATE"))
                cboShift.SelectedValue = clsCommon.myCstr(dr("SHIFT"))
                txtCode.Value = ""
                isNewEntry = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Try

            If clsCommon.MyMessageBoxShow("Reverse and unpost the document." + Environment.NewLine + "are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsMilkRejectHead.ReverseAndUnpost(txtCode.Value)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("No code found to update")
            End If

            Dim obj As clsMilkRejectHead = clsMilkRejectHead.GetData(txtCode.Value, NavigatorType.Current, tran)
            If obj IsNot Nothing AndAlso obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    Throw New Exception("Posted Transaction")
                End If
                For ii As Integer = 0 To obj.Arr.Count - 1
                    If obj.Arr(ii).Is_Return = 1 OrElse obj.Arr(ii).Is_Return = 2 Then
                        obj.Arr(ii).dblPenaltyPerUnit = IIf(obj.Arr(ii).Is_Return = 1, dblRetrunPenaltyPerUnit, IIf(obj.Arr(ii).Is_Return = 2, dblDrainPenaltyPerUnit, 0))
                        obj.Arr(ii).dclFATSNFDeductionMixMilkFATMinValue = dclFATSNFDeductionMixMilkFATMinValue
                        obj.Arr(ii).dclFATSNFDeductionMixMilkFATMaxValue = dclFATSNFDeductionMixMilkFATMaxValue
                        obj.Arr(ii).dclFATSNFDeductionMixMilkSNFMinValue = dclFATSNFDeductionMixMilkSNFMinValue
                        obj.Arr(ii).dclFATSNFDeductionMixMilkSNFMaxValue = dclFATSNFDeductionMixMilkSNFMaxValue
                        obj.Arr(ii).dclFATSNFDeductionMixMilkDeductionPer = dclFATSNFDeductionMixMilkDeductionPer
                    End If
                Next
                clsMilkRejectDetail.SaveData(obj.DOC_CODE, obj, settAlwaysVSPDefaulter, tran)
            End If
            tran.Commit()
            clsCommon.MyMessageBoxShow(Me, "Data updated successfully", Me.Text)
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnReturnTypeNA_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnReturnTypeNA.ToggleStateChanged, rbtnReturnTypeReturn.ToggleStateChanged, rbtnReturnTypeDrain.ToggleStateChanged, rbtnReturnTypeCOB.ToggleStateChanged
        If Not isInsideLoadData Then
            txtFatPer.Enabled = True
            txtSNFPer.Enabled = True
            If rbtnReturnTypeReturn.IsChecked OrElse rbtnReturnTypeDrain.IsChecked OrElse rbtnReturnTypeCOB.IsChecked OrElse settNoOfPreNxtDayToPickAvgFATSNF > 0 Then
                txtFatPer.Enabled = False
                txtSNFPer.Enabled = False
                txtFatPer.Value = 0
                txtSNFPer.Value = 0
            End If
        End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            Dim currSampleNo As String = clsCommon.myCstr(gv1.CurrentRow.Cells("SAMPLE NO").Value)
            If Not (RadButton3.Visible AndAlso UsLock1.Status = ERPTransactionStatus.Pending AndAlso clsCommon.myLen(txtCode.Value) > 0 AndAlso clsCommon.myCdbl(currSampleNo) > 0) Then
                e.Cancel = True
                Exit Sub
            End If
            If clsCommon.MyMessageBoxShow("Delete Sample no " + currSampleNo + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                e.Cancel = True
                Exit Sub
            End If
            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim qry As String = "delete from  TSPL_MILK_REJECT_DETAIL  where DOC_CODE='" + txtCode.Value + "' and  SAMPLE_NO='" + currSampleNo + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                qry = "update TSPL_MILK_REJECT_DETAIL set SAMPLE_NO=SAMPLE_NO-1  where DOC_CODE='" + txtCode.Value + "' and  SAMPLE_NO>'" + currSampleNo + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                tran.Commit()
                clsCommon.MyMessageBoxShow(Me, "Sample Deleted Successfully", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            Catch ex As Exception
                tran.Rollback()
                e.Cancel = True
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            e.Cancel = True
        End Try
    End Sub

    ''ERO/08/05/19-000592 by balwinder on 13/05/2019
    Private Sub cboRejectType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboRejectType.SelectedIndexChanged
        If settAlwaysVSPDefaulter Then
            fndItem_Code.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)
            Dim objMRT As clsMilkRejectType = clsMilkRejectType.GetData(clsCommon.myCstr(cboRejectType.SelectedValue), NavigatorType.Current, Nothing)
            If objMRT IsNot Nothing Then
                fndItem_Code.Text = objMRT.Item_Code
            End If
            lblItemDesc.Text = clsItemMaster.GetItemName(fndItem_Code.Text, Nothing)
        End If
    End Sub
End Class
