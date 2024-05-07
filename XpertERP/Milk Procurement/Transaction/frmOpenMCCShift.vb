' ----------------- Created By Anubhooti On 09-July-2014 Against BM00000003111-------------------- '
'---------------BM00000003414-----------------------BM00000004025(for shift code=MCC Code+M/E)------------
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


Public Class FrmOpenMCCShift
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrLoc As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim DtStock As DataTable
    Public FormCode As String = ""
    Dim ShiftTiming As String = String.Empty
    Dim ShowSystemStock As Boolean = False
    Dim AllowZeroQtyFATSNF As Boolean = False

    Dim isCLRInsteadOfSNF As Boolean = False
    Dim dclCorrectionFactor As Decimal = 0
#End Region

    Private Sub FrmOpenMCCShift_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddNew()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ShowSystemStock = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ShowSystemStockinOpenMCC, clsFixedParameterCode.ShowSystemStockinOpenMCC, Nothing)) = 1, True, False))

        dclCorrectionFactor = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, Nothing)) > 0 Then
                isCLRInsteadOfSNF = True
            End If
        If isCLRInsteadOfSNF Then
            TxtManualSNF_Per.Enabled = False
            SplitContainer3.FixedPanel = FixedPanel.Panel1
            SplitContainer3.IsSplitterFixed = True
        Else
            SplitContainer3.Panel1Collapsed = True
        End If


        If ShowSystemStock Then
            split_systemstock.Panel2Collapsed = False
        Else
            split_systemstock.Panel2Collapsed = True
        End If

        If FormCode = "MCC-SHF" Then
            ChkManualEntry.Visible = False
            ChkManualWeighment.Visible = False
            chkAllowManualGeteEntryWeighment.Visible = False
        Else
            HideStockControls()
            ChkManualEntry.Visible = True
            ChkManualEntry.ReadOnly = False
            ChkManualWeighment.Visible = True
            ChkManualWeighment.ReadOnly = False
            chkAllowManualGeteEntryWeighment.Visible = True
            chkAllowManualGeteEntryWeighment.ReadOnly = False
        End If
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ShiftTiming = clsFixedParameter.GetData(clsFixedParameterType.ShiftTiming, clsFixedParameterCode.ShiftTiming, Nothing)
        AllowZeroQtyFATSNF = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowZeroQtyFATSNFInOpenMCCShift, clsFixedParameterCode.AllowZeroQtyFATSNFInOpenMCCShift, Nothing)) > 0, True, False)
    End Sub

    Private Sub MCCLOCATIONFINDER()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                If clsCommon.myLen(txtmccode.Value) <= 0 AndAlso Not obj.Default_HO AndAlso clsCommon.myLen(clsMCCCodes.checkisMcc(obj.Default_LocCode)) > 0 Then
                    txtmccode.Value = obj.Default_LocCode
                    lblmccname.Text = obj.Default_LocName
                End If
                arrLoc = obj.arrLocCodes
            Else
                txtmccode.Enabled = False
                Throw New Exception("Please Set Default Location Of LogIn User")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmOpenMCCShiftManual)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As ClsOpenMCCShift = ClsOpenMCCShift.GetData(strCode, arrLoc, NavTyep)
        If obj IsNot Nothing Then
            If clsCommon.CompairString(obj.Is_Holiday, "T") = CompairStringResult.Equal Then
                btnsave.Enabled = False
                btndelete.Enabled = False
            Else
                btndelete.Enabled = True
            End If
            dtpShiftDate.ReadOnly = True
            isNewEntry = False
            txtCode.Value = obj.MCC_SHIFT_CODE
            txtmccode.Value = obj.MCC_CODE
            lblmccname.Text = obj.MCC_NAME
            dtpShiftDate.Text = obj.MCC_SHIFT_DATE
            cmbShift.SelectedValue = obj.SHIFT
            If ShowSystemStock Then
                txtremarks.Text = obj.Remarks
            End If
            TxtActualStock.Text = obj.Actual_Stock
            TxtActualFat.Text = obj.Actual_FAT
            TxtActualSNF.Text = obj.Actual_SNF
            TxtBookFat_Per.Text = obj.Actual_FAT_Per
            TxtBookSNF_per.Text = obj.Actual_SNF_Per
            txtsystemstock.Text = obj.System_Stock
            txtsystemfat.Text = obj.System_FAT_Per
            txtsystemsnf.Text = obj.System_SNF_Per
            TxtManualStock.Text = obj.Manual_Stock
            TxtManualFAT.Text = obj.Manual_FAT
            TxtManualSNF.Text = obj.Manual_SNF
            TxtManualFat_Per.Text = obj.Manual_FAT_Per
            TxtManualSNF_Per.Text = obj.Manual_SNF_Per
            ChkManualEntry.Checked = IIf(obj.Is_Manual = "T", True, False)
            ChkManualWeighment.Checked = IIf(obj.Is_Manual_Weighment = "T", True, False)
            chkAllowManualGeteEntryWeighment.Checked = IIf(clsCommon.CompairString(obj.Is_Allow_Manual_Gate_Entry_Weighment, "T") = CompairStringResult.Equal, True, False)
            chkHoliday.Checked = IIf(obj.Is_Holiday = "T", True, False)
            FndIrregularMcc.Value = obj.Irregular_MCC_Code
            LblIrregularMccName.Text = obj.Irregular_MCC_NAME
            Chkregular.Checked = IIf(obj.Is_Regular = 1, True, False)
            txtCLR.Value = obj.CLR
            If ChkManualEntry.Checked = True Then
                ChkManualEntry.ReadOnly = True
            End If
            If ChkManualWeighment.Checked = True Then
                ChkManualWeighment.ReadOnly = True
            End If
            If chkAllowManualGeteEntryWeighment.Checked = True Then
                chkAllowManualGeteEntryWeighment.ReadOnly = True
            End If
            txtCode.MyReadOnly = True
            btnsave.Text = "Update"

            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.BlankAllControls()
                UcCustomFields1.LoadData(obj.MCC_SHIFT_CODE)
            End If
        End If
    End Sub

    Sub SaveData()
        'Dim trans As SqlTransaction = Nothing
        Try
            If chkHoliday.Checked Then
                If clsCommon.MyMessageBoxShow("Current shift is Holiday.You can't Mofdify after save" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmOpenMCCShiftManual, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New ClsOpenMCCShift()
                obj.MCC_SHIFT_CODE = clsCommon.myCstr(txtCode.Value)
                obj.MCC_CODE = clsCommon.myCstr(txtmccode.Value)
                obj.MCC_NAME = clsCommon.myCstr(lblmccname.Text)
                obj.MCC_SHIFT_DATE = dtpShiftDate.Value
                obj.SHIFT = clsCommon.myCstr(cmbShift.SelectedValue)
                If ShowSystemStock Then
                    obj.Remarks = clsCommon.myCstr(txtremarks.Text)
                Else
                    obj.Remarks = ""
                End If
                obj.Actual_Stock = clsCommon.myCdbl(TxtActualStock.Text)
                obj.Actual_FAT = clsCommon.myCdbl(TxtActualFat.Text)
                obj.Actual_SNF = clsCommon.myCdbl(TxtActualSNF.Text)
                obj.Actual_FAT_Per = clsCommon.myCdbl(TxtBookFat_Per.Text)
                obj.Actual_SNF_Per = clsCommon.myCdbl(TxtBookSNF_per.Text)
                obj.System_Stock = clsCommon.myCdbl(txtsystemstock.Text)
                obj.System_FAT_Per = clsCommon.myCdbl(txtsystemfat.Text)
                obj.System_SNF_Per = clsCommon.myCdbl(txtsystemsnf.Text)
                obj.Manual_Stock = clsCommon.myCdbl(TxtManualStock.Text)
                obj.Manual_FAT = clsCommon.myCdbl(TxtManualFAT.Text)
                obj.Manual_SNF = clsCommon.myCdbl(TxtManualSNF.Text)
                obj.Manual_FAT_Per = clsCommon.myCdbl(TxtManualFat_Per.Text)
                obj.Manual_SNF_Per = clsCommon.myCdbl(TxtManualSNF_Per.Text)
                obj.Irregular_MCC_Code = clsCommon.myCstr(FndIrregularMcc.Value)
                obj.Irregular_MCC_NAME = clsCommon.myCstr(LblIrregularMccName.Text)
                obj.Is_Regular = IIf(Chkregular.Checked = True, 1, 0)
                obj.CLR = txtCLR.Value

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT where MCC_SHIFT_CODE='" + obj.MCC_SHIFT_CODE + "'")
                If (qry = 0) Then
                    isNewEntry = True
                    NewMCCShiftCode()
                    obj.MCC_SHIFT_CODE = clsCommon.myCstr(txtCode.Value)
                Else
                    isNewEntry = False
                End If
                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields

                If (ClsOpenMCCShift.SaveData(obj, isNewEntry, Nothing)) Then
                    If clsCommon.myCBool(chkHoliday.Checked) = True Then
                        If clsCommon.CompairString(FormCode, clsUserMgtCode.frmOpenMCCShift) = CompairStringResult.Equal Then
                            Dim qry1 As String = "select (Chilling_Assure_Qty*Chilling_Rate) as MinProvAmt from TSPL_MCC_MASTER where MCC_Code='" + txtmccode.Value + "' and TSPL_MCC_MASTER.MCC_Type='Chilling Basis' and Chilling_Assure_Qty>0 and Chilling_Rate>0"
                            Dim ProvAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry1))
                            If ProvAmt > 0 Then
                                Dim dtmcc As DataTable = clsDBFuncationality.GetDataTable("select tspl_mcc_master.*,vendor_name from tspl_mcc_master inner join tspl_vendor_master on vendor_code=chilling_vendor where tspl_mcc_master.mcc_code='" & obj.MCC_CODE & "'")
                                If dtmcc.Rows.Count > 0 Then
                                    Dim objProv As New clsProvisionEntry()
                                    objProv.Doc_Date = dtpShiftDate.Value
                                    objProv.Vendor_Code = dtmcc.Rows(0)("Chilling_Vendor")
                                    objProv.Vendor_Desc = dtmcc.Rows(0)("Vendor_Name")
                                    objProv.Vendor_Type = "MCC Lease Vendor"
                                    objProv.Status = "No"
                                    objProv.Loc_Code = obj.MCC_CODE
                                    objProv.Loc_Desc = obj.MCC_NAME
                                    objProv.Ref_Doc_No = obj.MCC_SHIFT_CODE
                                    objProv.Prov_type = "Chilling Charge"
                                    objProv.Amount = ProvAmt
                                    objProv.Prog_Code = clsUserMgtCode.frmOpenMCCShift
                                    objProv.Prov_Month = Month(dtpShiftDate.Value)
                                    objProv.Prov_Year = Year(dtpShiftDate.Value)
                                    objProv.Modified_Date = clsCommon.GETSERVERDATE()
                                    objProv.isNewEntry = True
                                    clsProvisionEntry.SaveData(objProv, Nothing)
                                    clsProvisionEntry.PostData(objProv.Doc_No, Nothing, False)
                                End If
                            End If
                        End If
                        Dim sQuery As String = "update TSPL_OPEN_MCC_SHIFT set status='C',is_Milk_holiday='T' where mcc_Shift_COde='" & obj.MCC_SHIFT_CODE & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery)
                    End If
                    If clsCommon.myCBool(ChkManualEntry.Checked) = True Then
                        Dim sQuery As String = "update TSPL_OPEN_MCC_SHIFT set Is_Allow_Manual_Entry='T' where mcc_Shift_COde='" & obj.MCC_SHIFT_CODE & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery)
                    End If
                    If clsCommon.myCBool(ChkManualWeighment.Checked) = True Then
                        Dim sQuery As String = "update TSPL_OPEN_MCC_SHIFT set Is_Allow_Manual_Entry_Weighment='T' where mcc_Shift_COde='" & obj.MCC_SHIFT_CODE & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery)
                    End If
                    If clsCommon.myCBool(chkAllowManualGeteEntryWeighment.Checked) = True Then
                        Dim sQuery As String = "update TSPL_OPEN_MCC_SHIFT set Is_Allow_Manual_Gate_Entry_Weighment='T' where mcc_Shift_COde='" & obj.MCC_SHIFT_CODE & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery)
                    End If
                    If FormCode = "MCC-SHF" Then
                        clsCommon.MyMessageBoxShow(Me, "Shift Opened Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Shift Settings Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.MCC_SHIFT_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub NewMCCShiftCode()
        Try
            Dim code As String = ""
            code = clsCommon.myCstr(txtmccode.Value) + "/" + clsCommon.myCstr(cmbShift.SelectedValue)

            Dim qry As String = "select max(mcc_shift_code) as code from TSPL_OPEN_MCC_SHIFT where mcc_shift_code like '" + code + "%'"
            Dim xcode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

            If xcode IsNot Nothing AndAlso clsCommon.CompairString(xcode, """") <> CompairStringResult.Equal AndAlso clsCommon.myLen(xcode) > 0 Then
                txtCode.Value = clsCommon.myCstr(clsCommon.incval(xcode))
            Else
                txtCode.Value = clsCommon.myCstr(txtmccode.Value) + "/" + clsCommon.myCstr(cmbShift.SelectedValue) + "00001"
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        'If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
        '    txtCode.Focus()
        '    Throw New Exception("Please Fill Code")
        Dim Status As String = ""
        Dim Count As Integer
        ' Dim Shift As String
        Dim Qry As String
        Dim dt As DataTable
        Try

            '= KUNAL > TICKET : BM00000009575 =======
            If AllowFutureDateTransaction(dtpShiftDate.Value, Nothing) = False Then
                dtpShiftDate.Focus()
                Return False
            End If

            CalculateSNFFromCLR()
            If FormCode = "MCC-SHF" Then
                Qry = "select * from tspl_milk_receipt_Head  Where MCC_Code = '" + txtmccode.Value + "' and shift='" & cmbShift.SelectedValue & "'and convert(date,Doc_date,103)=convert(date,'" & dtpShiftDate.Value & "',103)"
                dt = clsDBFuncationality.GetDataTable(Qry)
                If dt.Rows.Count > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "This Shift is Used in Milk Receipt and can not be Updated.", Me.Text)
                    Return False
                End If
            End If
            Count = clsDBFuncationality.getSingleValue("Select COUNT (*) From TSPL_OPEN_MCC_SHIFT where Mcc_Code='" & txtmccode.Value & "'")

            If clsCommon.myLen(clsCommon.myCstr(txtmccode.Value)) <= 0 Then
                txtmccode.Focus()
                Throw New Exception("Please fill MCC Code")
                'ElseIf clsCommon.myLen(clsCommon.myCstr(txtmccode.Value)) <= 0 Then
                '    txtmccode.Focus()
                '    Throw New Exception("Please MCC Code")

            End If

            If Count > 0 Then
                'Status = clsDBFuncationality.getSingleValue("Select Status From TSPL_OPEN_MCC_SHIFT Where CONVERT(VARCHAR,MCC_SHIFT_DATE ,103) <= '" + dtpShiftDate.Value + "'")
                Qry = "Select Status From TSPL_OPEN_MCC_SHIFT Where mcc_Code='" & txtmccode.Value & "' "
                If dtpShiftDate.ReadOnly Then
                    Qry += " and CONVERT(VARCHAR,MCC_SHIFT_DATE ,103) <= '" + dtpShiftDate.Value + "'"
                End If

                dt = clsDBFuncationality.GetDataTable(Qry)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        Status = clsCommon.myCstr(dr("Status"))
                        If clsCommon.CompairString(Status, "O") = CompairStringResult.Equal AndAlso clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                            txtmccode.Focus()
                            Throw New Exception("Please Close the Previous Shift.")
                        End If
                    Next
                End If

                Dim MCCDate As String


                MCCDate = clsDBFuncationality.getSingleValue("Select CONVERT(VARCHAR,MCC_SHIFT_DATE ,103) From TSPL_OPEN_MCC_SHIFT Where mcc_Shift_code<>'" & txtCode.Value & "' and MCC_Code = '" + txtmccode.Value + "' and shift='" & cmbShift.SelectedValue & "'and convert(date,Mcc_Shift_date,103)=convert(date,'" & dtpShiftDate.Value & "',103)")
                If MCCDate <> Nothing Then
                    txtmccode.Focus()
                    Throw New Exception("This Shift is Already Opened .")
                End If

                If dtpShiftDate.ReadOnly Then
                    MCCDate = clsDBFuncationality.getSingleValue("Select CONVERT(VARCHAR,MCC_SHIFT_DATE ,103) From TSPL_OPEN_MCC_SHIFT Where MCC_Code = '" + txtmccode.Value + "'")
                    If MCCDate <> Nothing Then
                        If dtpShiftDate.Value < MCCDate Then
                            txtmccode.Focus()
                            Throw New Exception("Please Check ! Shift Date should not be Less .")
                        End If
                    End If

                    Dim dtt As DataTable = clsDBFuncationality.GetDataTable("Select CONVERT(date,MCC_SHIFT_DATE ,103) as [MCC SHIFT DATE],Min(shift) as [shift] " _
                                                                        & " From TSPL_OPEN_MCC_SHIFT Where MCC_Code = '" + txtmccode.Value + "' and mcc_shift_code <>'" & txtCode.Value & "' group by MCC_SHIFT_DATE order by [MCC SHIFT DATE] desc,[Shift]")
                    If dtt.Rows.Count > 0 Then
                        If LCase(cmbShift.Text) = "morning" Then
                            If clsCommon.myCDate(dtt.Rows(0).Item("MCC SHIFT DATE")) = clsCommon.myCDate(dtpShiftDate.Value.Date.AddDays(-1)) And dtt.Rows(0).Item("Shift") = "E" Then
                            Else
                                'If clsCommon.MyMessageBoxShow("Previous day shift is missing." _
                                '& " Are you sure to create next day shift directly ?", "Message", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                                '    Return False
                                'End If
                                clsCommon.MyMessageBoxShow("Previous day shift is missing." _
                                & " Please create it first .", "Message", MessageBoxButtons.OK)
                                Return False
                                'Throw New Exception("please check ! Shift Date should not be less .")
                            End If
                        Else
                            If clsCommon.myCDate(dtt.Rows(0).Item("MCC SHIFT DATE")) = clsCommon.myCDate(dtpShiftDate.Value.Date) And dtt.Rows(0).Item("Shift") = "M" Then
                            Else
                                'If clsCommon.MyMessageBoxShow("Previous day shift is missing." _
                                '& " Are you sure to create next day shift directly ?", "Message", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                                '    Return False
                                'End If
                                clsCommon.MyMessageBoxShow("Morning shift is missing." _
                                & " Please create it first .", "Message", MessageBoxButtons.OK)
                                Return False
                                'Throw New Exception("please check ! Shift Date should not be less .")
                            End If
                        End If

                    End If
                End If
            End If
            'If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) <= 0 Then
            '    TxtManualStock.Focus()
            '    Throw New Exception("Please fill Opening Stock")
            'End If
            'If clsCommon.myLen(clsCommon.myCstr(TxtManualFat_Per.Text)) <= 0 Then
            '    TxtManualFat_Per.Focus()
            '    Throw New Exception("Please fill Opening FAT(%)")
            'End If
            'If clsCommon.myLen(clsCommon.myCstr(TxtManualFAT.Text)) <= 0 Then
            '    TxtManualFAT.Focus()
            '    Throw New Exception("Please fill Opening FAT")
            'End If
            'If clsCommon.myLen(clsCommon.myCstr(TxtManualSNF_Per.Text)) <= 0 Then
            '    TxtManualSNF_Per.Focus()
            '    Throw New Exception("Please fill Opening SNF)(%)")
            'End If
            'If clsCommon.myLen(clsCommon.myCstr(TxtManualSNF.Text)) <= 0 Then
            '    TxtManualSNF.Focus()
            '    Throw New Exception("Please fill Opening SNF")
            'End If
            If ShowSystemStock Then
                If checkremarkstobefilledornot() Then
                    If clsCommon.myLen(clsCommon.myCstr(txtremarks.Text)) <= 0 Then
                        txtremarks.Focus()
                        Throw New Exception("Please fill Remarks")
                    End If
                End If
            End If
            If ShiftTiming = "1" Then
                Dim IsOpenShift As Integer = ClsOpenMCCShift.CheckisShiftTimingAvailable(txtmccode.Value, cmbShift.SelectedValue)
                If IsOpenShift <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Shift Timing Excluded.Can't be Save.", Me.Text)
                    Return False
                End If
            End If
            If Not AllowZeroQtyFATSNF Then
                If TxtManualStock.Value <= 0 Then
                    TxtManualStock.Focus()
                    Throw New Exception("Please fill " + TxtManualStock.MyLinkLable1.Text)
                End If
                If TxtManualFat_Per.Value <= 0 Then
                    TxtManualFat_Per.Focus()
                    Throw New Exception("Please fill " + TxtManualFat_Per.MyLinkLable1.Text)
                End If
                If TxtManualSNF_Per.Value <= 0 Then
                    TxtManualSNF_Per.Focus()
                    Throw New Exception("Please fill " + TxtManualSNF_Per.MyLinkLable1.Text)
                End If
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub DeleteData()
        Try

            Dim qry As String = ""
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMCCMilkProcurement, clsUserMgtCode.frmOpenMCCShift, txtmccode.Value, dtpShiftDate.Value, Nothing)

            If clsCommon.MyMessageBoxShow("Do you want to delete  Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                qry = "select * from tspl_milk_receipt_Head  Where MCC_Code = '" + txtmccode.Value + "' and shift='" & cmbShift.SelectedValue & "'and convert(date,Doc_date,103)=convert(date,'" & dtpShiftDate.Value & "',103)"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt.Rows.Count <= 0 Then
                    qry = "DELETE FROM TSPL_OPEN_MCC_SHIFT WHERE MCC_SHIFT_CODE='" + txtCode.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                    AddNew()
                Else
                    clsCommon.MyMessageBoxShow("This Mcc Code has been used in Receipt...Can not be Deleted..")
                End If

            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Transaction MCC-SHF[MMMProc] is Locked") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            ElseIf (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Cost Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)

            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Sub AddNew()
        ' Dim D_Loc As String
        isNewEntry = True
        txtCode.Value = ""
        txtmccode.Value = ""
        lblmccname.Text = ""
        cmbShift.Text = ""
        ChkManualEntry.ReadOnly = False
        ChkManualWeighment.ReadOnly = False
        chkAllowManualGeteEntryWeighment.ReadOnly = False
        chkHoliday.Checked = False
        ChkManualEntry.Checked = False
        ChkManualWeighment.Checked = False
        chkAllowManualGeteEntryWeighment.Checked = False
        dtpShiftDate.Value = clsCommon.GETSERVERDATE()
        Me.cmbShift.DataSource = ClsOpenMCCShift.GetShift
        Me.cmbShift.DisplayMember = "Name"
        Me.cmbShift.ValueMember = "Code"
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        txtCode.MyReadOnly = False
        'MCCLoc(False)
        TxtActualStock.Text = 0
        TxtActualFat.Text = 0
        TxtActualSNF.Text = 0
        TxtBookFat_Per.Text = 0
        TxtBookSNF_per.Text = 0
        TxtManualStock.Text = Nothing
        TxtManualFAT.Text = Nothing
        TxtManualSNF.Text = Nothing
        TxtManualFat_Per.Text = Nothing
        TxtManualSNF_Per.Text = Nothing
        txtCLR.Value = 0
        MCCLOCATIONFINDER()
        DtStock = ClsOpenMCCShift.Getstock(dtpShiftDate.Value, txtmccode.Value)
        If DtStock.Rows.Count > 0 Then
            TxtActualStock.Text = DtStock.Rows(0).Item("Qty")
            TxtActualFat.Text = DtStock.Rows(0).Item("FAT")
            TxtActualSNF.Text = DtStock.Rows(0).Item("SNF")
            'TxtBookFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / IIf(clsCommon.myCdbl(TxtActualStock.Text) > 0, clsCommon.myCdbl(TxtActualStock.Text), 1), 2)
            'TxtBookSNF_per.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / IIf(clsCommon.myCdbl(TxtActualStock.Text) > 0, clsCommon.myCdbl(TxtActualStock.Text), 1), 2)
            TxtBookFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            TxtBookSNF_per.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            If clsCommon.myLen(txtmccode.Value) > 0 Then
                txtsystemstock.Text = DtStock.Rows(0).Item("Qty")
                txtsystemfat.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
                txtsystemsnf.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            Else
                txtsystemstock.Text = 0
                txtsystemfat.Text = 0
                txtsystemsnf.Text = 0
            End If
        End If
        Chkregular.Checked = True
        FndIrregularMcc.Value = Nothing
        LblIrregularMccName.Text = ""
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
            UcCustomFields1.SetDefaultValues()
        End If
    End Sub

    Sub MCCLoc(ByVal isButtonClick As Boolean)
        Dim UserLoc As String
        Dim HOLoc As String
        Dim MCCLoc As String
        UserLoc = clsDBFuncationality.getSingleValue("Select ISNULL(Default_Location,'') As Default_Location from TSPL_USER_MASTER Where User_Code ='" + objCommonVar.CurrentUserCode + "'")

        ' txtmccode.Enabled = False
        If UserLoc <> "" Then
            HOLoc = clsDBFuncationality.getSingleValue("Select Location_Category From TSPL_LOCATION_MASTER  where  Location_Category  ='HO' AND Location_code ='" + UserLoc + "'")
            MCCLoc = clsDBFuncationality.getSingleValue("Select Location_Category From TSPL_LOCATION_MASTER  where  Location_Category  ='MCC' AND Location_code ='" + UserLoc + "'")
            If HOLoc <> "" Then
                txtmccode.Value = clsCommon.ShowSelectForm("MCCFND", "Select isnull(Location_Code,'') As Code, ISNULL(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER   ", "Code", "Location_Category ='MCC'", txtmccode.Value, "Code", isButtonClick)
                If clsCommon.myLen(txtmccode.Value) > 0 Then
                    lblmccname.Text = clsDBFuncationality.getSingleValue("Select Location_Desc From TSPL_LOCATION_MASTER  where  Location_Code  ='" + txtmccode.Value + "'")
                Else
                    lblmccname.Text = ""
                End If
            ElseIf MCCLoc <> "" Then
                'txtmccode.Value = clsCommon.ShowSelectForm("MCCFND", "Select isnull(Location_Code,'') As Code, ISNULL(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER   ", "Code", "Location_Category  ='MCC' AND Location_code ='" + UserLoc + "'", txtmccode.Value, "Code", isButtonClick)
                txtmccode.Value = clsDBFuncationality.getSingleValue("Select isnull(Location_Code,'') As Code From TSPL_LOCATION_MASTER  where  Location_Code  ='" + UserLoc + "'")
                If clsCommon.myLen(txtmccode.Value) > 0 Then
                    lblmccname.Text = clsDBFuncationality.getSingleValue("Select Location_Desc From TSPL_LOCATION_MASTER  where  Location_Code  ='" + txtmccode.Value + "'")
                Else
                    lblmccname.Text = ""
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "no location found", Me.Text)
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select isnull(Location_Code,'') As Location_Code, ISNULL(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER  Where Location_Code  ='" + UserLoc + "' AND Location_Category ='MCC'")
                'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                '    If dt.Rows.Count = 1 Then
                '        txtmccode.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                '        lblmccname.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                '        txtmccode.Enabled = False
                '    Else
                '        txtmccode.Enabled = True
                '        txtmccode.Value = clsCommon.ShowSelectForm("MCCFND", "Select isnull(Location_Code,'') As Code, ISNULL(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER   ", "Code", "Location_Code  ='" + UserLoc + "' AND Location_Category ='MCC'", txtmccode.Value, "Code", isButtonClick)
                '        If clsCommon.myLen(txtmccode.Value) > 0 Then
                '            lblmccname.Text = clsDBFuncationality.getSingleValue("Select Location_Desc From TSPL_LOCATION_MASTER  where  Location_Code  ='" + txtmccode.Value + "'")
                '        End If
                '    End If
                'Else
                '    txtmccode.Enabled = True
                '    txtmccode.Value = clsCommon.ShowSelectForm("MCCFND", "Select isnull(Location_Code,'') As Code, ISNULL(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER  ", "Code", "Location_Category ='MCC'", txtmccode.Value, "Code", isButtonClick)
                '    If clsCommon.myLen(txtmccode.Value) > 0 Then
                '        lblmccname.Text = clsDBFuncationality.getSingleValue("Select Location_Desc From TSPL_LOCATION_MASTER  where  Location_Code  ='" + txtmccode.Value + "'")
                '    End If
                '    End If
                'End If

                'Else
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select isnull(Location_Code,'') As Code, ISNULL(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER  Where Location_Category ='MCC'")
                'If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                '    If dt.Rows.Count = 1 Then
                '        txtmccode.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                '        lblmccname.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                '        txtmccode.Enabled = False
                '    Else
                '        txtmccode.Enabled = True
                '        txtmccode.Value = clsCommon.ShowSelectForm("MCCFND", "Select isnull(Location_Code,'') As Code, ISNULL(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER  ", "Code", "Location_Category ='MCC'", txtmccode.Value, "Code", isButtonClick)
                '        If clsCommon.myLen(txtmccode.Value) > 0 Then
                '            lblmccname.Text = clsDBFuncationality.getSingleValue("Select Location_Desc From TSPL_LOCATION_MASTER  where  Location_Code  ='" + txtmccode.Value + "'")
                '        End If
                '    End If
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "no location found", Me.Text)
        End If
    End Sub

    Private Sub txtmccode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtmccode._MYValidating
        '--------------------done by Monika-------------------
        Dim whrcls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls = " TSPL_MCC_MASTER.mcc_code in (" + arrLoc + ")"
        End If

        txtmccode.Value = clsMccMaster.getFinder(whrcls, txtmccode.Value, isButtonClicked)
        If clsCommon.myLen(txtmccode.Value) > 0 Then
            lblmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmccode.Value + "'"))
        Else
            txtmccode.Value = ""
            lblmccname.Text = ""
        End If
        DtStock = ClsOpenMCCShift.Getstock(dtpShiftDate.Value, txtmccode.Value)
        If DtStock.Rows.Count > 0 Then
            TxtActualStock.Text = DtStock.Rows(0).Item("Qty")
            TxtActualFat.Text = DtStock.Rows(0).Item("FAT")
            TxtActualSNF.Text = DtStock.Rows(0).Item("SNF")
            'TxtBookFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / IIf(clsCommon.myCdbl(TxtActualStock.Text) > 0, clsCommon.myCdbl(TxtActualStock.Text), 1), 2)
            'TxtBookSNF_per.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / IIf(clsCommon.myCdbl(TxtActualStock.Text) > 0, clsCommon.myCdbl(TxtActualStock.Text), 1), 2)
            TxtBookFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            TxtBookSNF_per.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            'done by stuti on 05/01/2017
            If clsCommon.myLen(txtmccode.Value) > 0 Then
                txtsystemstock.Text = DtStock.Rows(0).Item("Qty")
                txtsystemfat.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
                txtsystemsnf.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            Else
                'done by stuti on 05/01/2017
                txtsystemstock.Text = 0
                txtsystemfat.Text = 0
                txtsystemsnf.Text = 0
                '========end here============
            End If
            '========end here============
        Else
            TxtActualStock.Text = 0
            TxtActualFat.Text = 0
            TxtActualSNF.Text = 0
            TxtBookFat_Per.Text = 0
            TxtBookSNF_per.Text = 0
            'done by stuti on 05/01/2017
            txtsystemstock.Text = 0
            txtsystemfat.Text = 0
            txtsystemsnf.Text = 0
            '========end here============
        End If

        '-----end here---------------------------------------------
    End Sub

    Private Sub fndIrregularmccode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndIrregularMcc._MYValidating
        '--------------------done by Monika-------------------
        Dim whrcls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls = " TSPL_MCC_MASTER.mcc_code in (" + arrLoc + ") and TSPL_MCC_MASTER.mcc_code <> '" & clsCommon.myCstr(txtmccode.Value) & "'"
        End If

        FndIrregularMcc.Value = clsMccMaster.getFinder(whrcls, FndIrregularMcc.Value, isButtonClicked)
        If clsCommon.myLen(txtmccode.Value) > 0 Then
            LblIrregularMccName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + FndIrregularMcc.Value + "'"))
        Else
            FndIrregularMcc.Value = ""
            LblIrregularMccName.Text = ""
        End If
        '-----end here---------------------------------------------
    End Sub

    Private Sub FrmOpenMCCShift_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Control And e.Alt And e.KeyCode = Keys.D Then
            Dim dbpwd As String = clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.MCCDLTPWD & "' and TYPE='" & clsFixedParameterType.MCC_DLTDATA_PWD & "'")
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.MCCDLTPWD
            pwd.strType = clsFixedParameterType.MCC_DLTDATA_PWD
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                clsCommon.ProgressBarShow()
                clsDBFuncationality.ExecuteNonQuery("exec [dbo].[DeleteMccData]")
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Whole Data Deleted Successfully.", Me.Text)
            End If
        ElseIf e.Control And e.Alt And e.KeyCode = Keys.F12 Then
           Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                dtpShiftDate.ReadOnly = False
            End If
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                      "========Table Name=========" + Environment.NewLine + _
                                      "TSPL_OPEN_MCC_SHIFT" + Environment.NewLine + _
                                      "TSPL_CUSTOM_FIELD_VALUES" + Environment.NewLine + _
                                      "tspl_provision_entry" + Environment.NewLine + _
                                      "TSPL_INVENTORY_MOVEMENT_new" + Environment.NewLine + _
                                      "=========Setting Name======" + Environment.NewLine + _
                                      "AllowFutureDateTransaction" + Environment.NewLine + _
                                      "ShowSystemStockinOpenMCC (Show system stock in open MCC shift)" + Environment.NewLine + _
                                      "MilkProcuremntPickCLRInsteadOfSNF (Milk Procuremnt Pick CLR Instead Of SNF)" + Environment.NewLine + _
                                      "AllowZeroQtyFATSNFInOpenMCCShift (Allow Zero Qty FAT SNF In Open MCC Shift)" + Environment.NewLine + _
                                      "MilkSetting,ShiftTiming" + Environment.NewLine + _
                                      "=========Function======" + Environment.NewLine + _
                                      "Create Journal Entry")
        End If
    End Sub

    Public Sub New(ByVal Frm_Code As String)
        InitializeComponent()
        FormCode = Frm_Code
    End Sub

    Sub HideStockControls()
        LblManualStock.Visible = False
        LblManualSNF_Per.Visible = False
        LblManualSNF.Visible = False
        LblManualFAT_Per.Visible = False
        LblManualFAT.Visible = False
        LblBookStock.Visible = False
        LblBookSNF_Per.Visible = False
        LblBookSNF.Visible = False
        LblBookFat_Per.Visible = False
        LblBookFAT.Visible = False

        TxtActualStock.Visible = False
        TxtActualFat.Visible = False
        TxtActualSNF.Visible = False
        TxtBookFat_Per.Visible = False
        TxtBookSNF_per.Visible = False
        TxtManualStock.Visible = False
        TxtManualFAT.Visible = False
        TxtManualSNF.Visible = False
        TxtManualFat_Per.Visible = False
        TxtManualSNF_Per.Visible = False
        Chkregular.Visible = False
        MyLabel13.Visible = False
        txtCLR.Visible = False
        cmbShift.Enabled = False
        txtmccode.MyReadOnly = True
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Dim whrcls As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrcls = " and TSPL_OPEN_MCC_SHIFT.MCC_CODE IN (" + arrLoc + ")"
        End If

        Dim qry As String = "SELECT TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE,TSPL_OPEN_MCC_SHIFT.MCC_CODE,TSPL_OPEN_MCC_SHIFT.SHIFT,TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_DATE, TSPL_MCC_MASTER.MCC_NAME As [MCC NAME] FROM TSPL_OPEN_MCC_SHIFT LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code =TSPL_OPEN_MCC_SHIFT.MCC_CODE where 2=2 " + whrcls + ""


        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select MIN(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT WHERE 1=1 " + whrcls + " )"
            Case NavigatorType.Last
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select Max(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT WHERE 1=1 " + whrcls + " )"
            Case NavigatorType.Current
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select TOP 1 MCC_SHIFT_CODE from TSPL_OPEN_MCC_SHIFT WHERE 1=1 " + whrcls + " and MCC_SHIFT_CODE='" + txtCode.Value + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select Min(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT where MCC_SHIFT_CODE > '" + txtCode.Value + "' " + whrcls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_OPEN_MCC_SHIFT.MCC_SHIFT_CODE = (select Max(MCC_SHIFT_CODE) from TSPL_OPEN_MCC_SHIFT where MCC_SHIFT_CODE < '" + txtCode.Value + "' " + whrcls + ")"
        End Select
        txtCode.Value = clsDBFuncationality.getSingleValue(qry)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value, NavigatorType.Current)
            btnsave.Text = "Update"
            txtCode.MyReadOnly = True
            btndelete.Enabled = True
        Else
            btnsave.Text = "Save"
            btndelete.Enabled = False
            txtCode.MyReadOnly = False
            AddNew()
        End If
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim whrcls As String = ""
            If clsCommon.myLen(arrLoc) > 0 Then
                whrcls = " TSPL_OPEN_MCC_SHIFT.MCC_CODE in (" + arrLoc + ")"
            End If

            Dim qry As String = "select MCC_SHIFT_CODE As Code,TSPL_OPEN_MCC_SHIFT.MCC_CODE As [MCC CODE],Mcc_name as [Mcc Name], SHIFT,(CONVERT(varchar,MCC_SHIFT_DATE,103)+SUBSTRING(CONVERT(varchar,MCC_SHIFT_DATE,100),12,8)) As [Date],TSPL_OPEN_MCC_SHIFT.Status from TSPL_OPEN_MCC_SHIFT LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code =TSPL_OPEN_MCC_SHIFT.MCC_CODE "
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_OPEN_MCC_SHIFT", qry, "Code", whrcls, txtCode.Value, "", isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
                btnsave.Text = "Update"
                btndelete.Enabled = True
                txtCode.MyReadOnly = True
            Else
                AddNew()
                txtCode.MyReadOnly = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "SELECT MCC_SHIFT_CODE As [MCC SHIFT CODE],MCC_CODE As [MCC CODE],SHIFT,MCC_SHIFT_DATE As [DATE] FROM TSPL_OPEN_MCC_SHIFT "
        ListImpExpColumnsMandatory = New List(Of String)({"MCC SHIFT CODE", "MCC CODE", "SHIFT", "DATE"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"MCC SHIFT CODE"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        ' Dim trans As SqlTransaction
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "MCC SHIFT CODE", "MCC CODE", "SHIFT", "DATE") Then
            Dim linno As Integer = 0
            Try
                clsCommon.ProgressBarShow()
                'trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsOpenMCCShift()

                    linno += 1
                    Dim strcode As String = clsCommon.myCstr(grow.Cells("MCC SHIFT CODE").Value)
                    If (String.IsNullOrEmpty(strcode)) Or clsCommon.myLen(strcode) > 30 Then
                        Throw New Exception("Length of MCC Shift Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.MCC_SHIFT_CODE = strcode

                    Dim strname As String = clsCommon.myCstr(grow.Cells("MCC CODE").Value)
                    If (String.IsNullOrEmpty(strname)) Or clsCommon.myLen(strname) > 30 Then
                        Throw New Exception("Length of MCC Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strname) > 0 Then
                        Dim qry As String = "select MCC_CODE from TSPL_MCC_MASTER where MCC_CODE='" + strname + "'"
                        Dim MCC_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                        If clsCommon.myLen(MCC_Code) <= 0 Then
                            Throw New Exception("Please Fill MCC Code For OT Slab [" + strname + "] Or Make MCC Master Entry First")
                        End If
                    End If
                    obj.MCC_CODE = strname

                    Dim strType As String = clsCommon.myCstr(grow.Cells("SHIFT").Value)
                    'If (String.IsNullOrEmpty(strType)) Or clsCommon.myLen(strType) > 30 Then
                    '    Throw New Exception("Length of Cost Class should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    'End If

                    obj.SHIFT = strType
                    If clsCommon.myLen(strType) = 0 Then
                        Throw New Exception("Shift can not be left blank")
                    ElseIf clsCommon.CompairString(strType, "M") = CompairStringResult.Equal Or clsCommon.CompairString(strType, "E") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception("Cost Class should be amoung 'M','E' At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim MCCDate As Date = clsCommon.myCstr(grow.Cells("DATE").Value)
                    obj.MCC_SHIFT_DATE = MCCDate
                    If (String.IsNullOrEmpty(MCCDate)) Then
                        Throw New Exception("Length of MCC Date can not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strcode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_OPEN_MCC_SHIFT  Where MCC_SHIFT_CODE = '" & strcode & "'") Then
                        isNewEntry = False
                    Else
                        isNewEntry = True
                    End If
                    obj.SaveData(obj, isNewEntry)
                Next
                'trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                ' trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub TxtManualFat_Per_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtManualFat_Per.TextChanged
        If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) > 0 Then
            TxtManualFAT.Text = Math.Round(clsCommon.myCdbl(TxtManualFat_Per.Text) * clsCommon.myCdbl(TxtManualStock.Text) / 100, 2)
        Else
            TxtManualFAT.Text = Nothing
        End If
        CalculateSNFFromCLR()
    End Sub

    Private Sub TxtManualSNF_Per_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtManualSNF_Per.TextChanged
        If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) > 0 Then
            TxtManualSNF.Text = Math.Round(clsCommon.myCdbl(TxtManualSNF_Per.Text) * clsCommon.myCdbl(TxtManualStock.Text) / 100, 2)
        Else
            TxtManualSNF.Text = Nothing
        End If
    End Sub

    Private Sub TxtManualSNF_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtManualSNF.Leave
        If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) > 0 Then
            TxtManualSNF_Per.Text = Math.Round(clsCommon.myCdbl(TxtManualSNF.Text) * 100 / IIf(clsCommon.myCdbl(TxtManualStock.Text) <= 0, 1, clsCommon.myCdbl(TxtManualStock.Text)), 2)
        Else
            TxtManualSNF_Per.Text = Nothing
        End If
    End Sub

    Private Sub TxtManualFAT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtManualFAT.KeyPress, TxtManualFat_Per.KeyPress, TxtManualSNF.KeyPress, TxtManualSNF_Per.KeyPress, TxtManualStock.KeyPress
        If Not IsNumeric(e.KeyChar) And Not e.KeyChar = "." Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtManualFAT_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtManualFAT.Leave
        If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) > 0 Then
            TxtManualFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtManualFAT.Text) * 100 / IIf(clsCommon.myCdbl(TxtManualStock.Text) <= 0, 1, clsCommon.myCdbl(TxtManualStock.Text)), 2)
        Else
            TxtManualFat_Per.Text = Nothing
        End If
    End Sub

    Private Sub Chkregular_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chkregular.CheckStateChanged
        Try
            If Chkregular.Checked = False And FormCode = "MCC-SHF" Then
                GrpRegular.Visible = True
            Else : GrpRegular.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub TxtManualStock_TextChanged(sender As Object, e As EventArgs) Handles TxtManualStock.TextChanged, TxtManualFat_Per.TextChanged, TxtManualSNF_Per.TextChanged, txtsystemfat.TextChanged, txtsystemsnf.TextChanged, txtsystemstock.TextChanged
        checkremarkstobefilledornot()
    End Sub

    Private Function checkremarkstobefilledornot() As Boolean
        If clsCommon.myCdbl(TxtManualStock.Text) <> clsCommon.myCdbl(txtsystemstock.Text) Then
            txtremarks.Enabled = True
            Return True
        ElseIf clsCommon.myCdbl(TxtManualFat_Per.Text) <> clsCommon.myCdbl(txtsystemfat.Text) Then
            txtremarks.Enabled = True
            Return True
        ElseIf clsCommon.myCdbl(TxtManualSNF_Per.Text) <> clsCommon.myCdbl(txtsystemsnf.Text) Then
            txtremarks.Enabled = True
            Return True
        Else
            txtremarks.Text = ""
            txtremarks.Enabled = False
            Return False
        End If
    End Function


    Private Sub txtCLR_Validating(sender As Object, e As CancelEventArgs) Handles txtCLR.Validating
        CalculateSNFFromCLR()
    End Sub

    Sub CalculateSNFFromCLR()
        If isCLRInsteadOfSNF Then
            txtCLR.Value = clsERPFuncationality.myDclInZeroPointFive(txtCLR.Value)
            TxtManualSNF_Per.Value = Math.Round(clsEkoPro.getSnfOnCalculation(TxtManualFat_Per.Value, txtCLR.Value, dclCorrectionFactor), 2, MidpointRounding.ToEven) ''SHR/15/05/18-000022 by balwinder change to twwo decimal places on 16/05/2018
        End If
    End Sub
End Class
