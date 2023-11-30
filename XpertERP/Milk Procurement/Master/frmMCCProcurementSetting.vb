Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports common
'' CREATED BY : Rohit
''Start Date: 30-07-2014
Public Class frmMCCProcurementSetting
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim tableName As String = "TSPL_TAX_MASTER"
    Dim tableCode As String = "Tax_Code"
    Dim codePrefix As String = "TAX"
#End Region

    Private Sub frmMCCProcurementSetting_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MCCSetting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag

        If btnsave.Visible = True Then
            mnimport.Enabled = True
            mnexport.Enabled = True
        Else
            mnimport.Enabled = False
            mnexport.Enabled = False
        End If

    End Sub
    Private Sub frmMCCProcurementSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Funfill()
        
    End Sub
    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "INV-SETT-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            'rdbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
    ''insert function 
    Public Sub funinsert()
        Dim currentdate As Date = Date.Today
        Dim modify_by As String = objCommonVar.CurrentUserCode
        Dim modify_date As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String
            If fndMCCItemCode.Value <> "" Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & fndMCCItemCode.Value & "' where Type='" & clsFixedParameterType.MCCDefaultMilkItem & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myCdbl(TxtSampleRange.Text) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(TxtSampleRange.Text) & "' where Type='" & clsFixedParameterType.MCCSampleRange & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            If clsCommon.myCdbl(TxtReceiptRange.Text) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(TxtReceiptRange.Text) & "' where Type='" & clsFixedParameterType.MCCReceiptRange & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myCdbl(TxtMinKMDiff.Text) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(TxtMinKMDiff.Text) & "' where Type='" & clsFixedParameterType.MCCMinKmRange & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            '=====================SMS Setting=======================================
            If clsCommon.myLen(clsCommon.myCstr(TxtUserName.Text)) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCstr(TxtUserName.Text) & "' where Type='" & clsFixedParameterType.SMS_User_Name & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCstr(TxtPWD.Text) & "' where Type='" & clsFixedParameterType.SMS_User_PWD & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCstr(TxtSendorID.Text) & "' where Type='" & clsFixedParameterType.SMS_Sendor_ID & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCstr(CmbSMSProvider.Text) & "' where Type='" & clsFixedParameterType.SMS_Provider & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            '==================================================================================
            If clsCommon.myCdbl(TxtDaysForFssaiPopup.Text) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(TxtDaysForFssaiPopup.Text) & "' where Type='" & clsFixedParameterType.MCCFSSAI_DAYS & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myCdbl(TxtMilkWeight.Text) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(TxtMilkWeight.Text) & "' where Type='" & clsFixedParameterType.Milk_Can_Weight_Ratio & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myCdbl(Txttoleranceneg.Text) >= 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(Txttoleranceneg.Text) & "' where Type='" & clsFixedParameterType.Milk_Can_Weight_Tolerance_Neg & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myCdbl(TxttolerancePlus.Text) >= 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(TxttolerancePlus.Text) & "' where Type='" & clsFixedParameterType.Milk_Can_Weight_Tolerance_Positive & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myCdbl(txtStockTolerance.Text) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(txtStockTolerance.Text) & "' where Type='" & clsFixedParameterType.AllowStockToleranceNegative & " ' and Code='" & clsFixedParameterCode.AllowStockToleranceNegative & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myLen(DtpStartingDate.Value) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & DtpStartingDate.Value & "' where Type='" & clsFixedParameterType.MCCInvoiceScheduleDate & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If dtpBlockingDBTPeriod.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.GetPrintDate(dtpBlockingDBTPeriod.Value, "dd/MMM/yyyy") & "' where Type='" & clsFixedParameterType.AndroidAPP & " ' and Code='" & clsFixedParameterCode.StopDBTBefore & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If clsCommon.myLen(DtpTime.Value) > 0 Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & DtpTime.Value & "' where Type='" & clsFixedParameterType.MCCInvoiceScheduleTime & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If TxtInterval.Text <> "" Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & TxtInterval.Text & "' where Type='" & clsFixedParameterType.MCCInvoiceScheduleInterval & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            qry = "update TSPL_FIXED_PARAMETER set Description='" & IIf(ChkSendSMS.Checked, 1, 0) & "' where Type='" & clsFixedParameterType.Is_Send_Sms & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If ChkSendSMS.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & DtpSendSMSTime.Value & "' where Type='" & clsFixedParameterType.Send_Sms_Time & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='' where Type='" & clsFixedParameterType.Send_Sms_Time & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If


            qry = "update TSPL_FIXED_PARAMETER set Description='" & IIf(chkControlSampleMandatory.Checked, 1, 0) & "' where Type='" & clsFixedParameterType.ControlSampleMandatory & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" & IIf(chkAllowSkippingPrevDocOnPayProcess.Checked, 1, 0) & "' where Type='" & clsFixedParameterType.MilkProc & "' and Code='" & clsFixedParameterCode.AllowSkippingPrevDocumentsOnPaymentProcess & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" & IIf(chkDisAllowTankerDispatchTopalnt.Checked, 1, 0) & "' where Type='" & clsFixedParameterType.MilkProc & " ' and Code='" & clsFixedParameterCode.DisAllowIntermittentTankerForPlantDispatch & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" & IIf(chkAllowQcDateBeforeGateEntry.Checked, 1, 0) & "' where Type='" & clsFixedParameterType.MilkProc & " ' and Code='" & clsFixedParameterCode.AllowQcDateBeforeGateEntryDateTime & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" & txtCorrectionFactor.Text & "' where Type='" & clsFixedParameterType.defaultCorrectionFactor & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCstr(txtItemDescForTankerDisp.Text) & "' where Type='" & clsFixedParameterType.ItemDescForTankerdispatchPrint & " ' and Code='" & clsFixedParameterCode.ItemDescForTankerDispatchPrint & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If ChkAllSampleParameter.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" & clsFixedParameterType.MCCDisplay_All_Parameter & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" & clsFixedParameterType.MCCDisplay_All_Parameter & " ' and Code='" & clsFixedParameterCode.MilkSetting & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If ChkDisplayParameterinQualityCheck.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" & clsFixedParameterType.DisplayAllParameterinQualityCheck & " ' and Code='" & clsFixedParameterCode.DisplayAllParameterinQualityCheck & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" & clsFixedParameterType.DisplayAllParameterinQualityCheck & " ' and Code='" & clsFixedParameterCode.DisplayAllParameterinQualityCheck & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            '========Added by Rohit on Aug 3,2015============================
            If ChkDisplayTypeinMilkReceipt.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" & clsFixedParameterType.DisplayTypeInMilkReceipt & " ' and Code='" & clsFixedParameterCode.DisplayTypeInMilkReceipt & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" & clsFixedParameterType.DisplayTypeInMilkReceipt & " ' and Code='" & clsFixedParameterCode.DisplayTypeInMilkReceipt & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If ChkValidationofMilkType.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" & clsFixedParameterType.AddValidationofMilkTypeinsample & " ' and Code='" & clsFixedParameterCode.AddValidationofMilkTypeinsample & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" & clsFixedParameterType.AddValidationofMilkTypeinsample & " ' and Code='" & clsFixedParameterCode.AddValidationofMilkTypeinsample & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If ChkValidationofMilkType.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(0).Cells("FAT(Min)").Value) & "' where Type='" & clsFixedParameterType.FatMinCow & " ' and Code='" & clsFixedParameterCode.FatMinCow & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(0).Cells("FAT(Max)").Value) & "' where Type='" & clsFixedParameterType.FatMaxCow & " ' and Code='" & clsFixedParameterCode.FatMaxCow & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(0).Cells("SNF(Min)").Value) & "' where Type='" & clsFixedParameterType.SNFMinCow & " ' and Code='" & clsFixedParameterCode.SNFMinCow & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(0).Cells("SNF(Max)").Value) & "' where Type='" & clsFixedParameterType.SNFMaxCow & " ' and Code='" & clsFixedParameterCode.SNFMaxCow & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(1).Cells("FAT(Min)").Value) & "' where Type='" & clsFixedParameterType.FatMinBuff & " ' and Code='" & clsFixedParameterCode.FatMinBuff & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(1).Cells("FAT(Max)").Value) & "' where Type='" & clsFixedParameterType.FatMaxBuff & " ' and Code='" & clsFixedParameterCode.FatMaxBuff & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(1).Cells("SNF(Min)").Value) & "' where Type='" & clsFixedParameterType.SNFMinBuff & " ' and Code='" & clsFixedParameterCode.SNFMinBuff & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(1).Cells("SNF(Max)").Value) & "' where Type='" & clsFixedParameterType.SNFMaxBuff & " ' and Code='" & clsFixedParameterCode.SNFMaxBuff & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(2).Cells("FAT(Min)").Value) & "' where Type='" & clsFixedParameterType.FatMinMix & " ' and Code='" & clsFixedParameterCode.FatMinMix & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(2).Cells("FAT(Max)").Value) & "' where Type='" & clsFixedParameterType.FatMaxMix & " ' and Code='" & clsFixedParameterCode.FatMaxMix & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(2).Cells("SNF(Min)").Value) & "' where Type='" & clsFixedParameterType.SNFMinMix & " ' and Code='" & clsFixedParameterCode.SNFMinMix & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv1.Rows(2).Cells("SNF(Max)").Value) & "' where Type='" & clsFixedParameterType.SNFMaxMix & " ' and Code='" & clsFixedParameterCode.SNFMaxMix & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If


            qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv2.Rows(0).Cells("FAT").Value) & "' where Type='" & clsFixedParameterType.SubStdFatCow & " ' and Code='" & clsFixedParameterCode.SubStdFatCow & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv2.Rows(0).Cells("SNF").Value) & "' where Type='" & clsFixedParameterType.SubStdSNFCow & " ' and Code='" & clsFixedParameterCode.SubStdSNFCow & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv2.Rows(1).Cells("FAT").Value) & "' where Type='" & clsFixedParameterType.SubStdFatBuff & " ' and Code='" & clsFixedParameterCode.SubStdFatBuff & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv2.Rows(1).Cells("SNF").Value) & "' where Type='" & clsFixedParameterType.SubStdSNFBuff & " ' and Code='" & clsFixedParameterCode.SubStdSNFBuff & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv2.Rows(2).Cells("FAT").Value) & "' where Type='" & clsFixedParameterType.SubStdFatMix & " ' and Code='" & clsFixedParameterCode.SubStdFatMix & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "update TSPL_FIXED_PARAMETER set Description='" & clsCommon.myCdbl(gv2.Rows(2).Cells("SNF").Value) & "' where Type='" & clsFixedParameterType.SubStdSNFMix & " ' and Code='" & clsFixedParameterCode.SubStdSNFMix & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)


            If chkItemMilkType.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" & clsFixedParameterType.isItemMilkType & " ' and Code='" & clsFixedParameterCode.isItemMilkType & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" & clsFixedParameterType.isItemMilkType & " ' and Code='" & clsFixedParameterCode.isItemMilkType & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            If chkPriceChartGradeWise.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" & clsFixedParameterType.isPriceChartGradeWise & " ' and Code='" & clsFixedParameterCode.isPriceChartGradeWise & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" & clsFixedParameterType.isPriceChartGradeWise & " ' and Code='" & clsFixedParameterCode.isPriceChartGradeWise & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            ''richa agarwal 11 Jan,2017

            If ChkFarmerPaymentCycle.Checked Then
                qry = "update TSPL_FIXED_PARAMETER set Description='1' where Type='" & clsFixedParameterType.isFarmerPaymentCycle & " ' and Code='" & clsFixedParameterCode.isFarmerPaymentCycle & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Else
                qry = "update TSPL_FIXED_PARAMETER set Description='0' where Type='" & clsFixedParameterType.isFarmerPaymentCycle & " ' and Code='" & clsFixedParameterCode.isFarmerPaymentCycle & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If

            '========================================================================
            clsFixedParameter.UpdateData(clsFixedParameterType.ApplyStdFATSNFRate, clsFixedParameterCode.ApplyStdFATSNFRate, IIf(chkStdFATSNFRate.Checked, 1, 0), trans)

            trans.Commit()
            myMessages.insert()
            objCommonVar.RefreshCommonVar()
        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)

        End Try
    End Sub
    ''To Close the inventory setting screen
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub
    ''To Call The Insert function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        SaveData()

    End Sub
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.MCCSetting, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        funinsert()
        btnsave.Text = "Update"
        Funfill()
    End Sub
    ''Apply character validation on radgridview cell 1 and cell 2(numeric validation)
    '' To Update data on the table(tspl_inv_class)
    'Public Sub funupdate()
    '    Dim trans As SqlTransaction
    '    Try
    '        Dim strdelete As String = "truncate table tspl_inv_class"
    '        connectSql.RunSql(strdelete)

    '        strdelete = "delete from TSPL_INV_PARAMETERS"
    '        connectSql.RunSql(strdelete)


    '        Dim strallownegative As String
    '        Dim strallownonstock As String
    '        If chkallownegativeinventory.Checked = True Then
    '            strallownegative = "Y"
    '        Else
    '            strallownegative = "N"
    '        End If
    '        If chkallowreceipts.Checked = True Then
    '            strallownonstock = "Y"
    '        Else
    '            strallownonstock = "N"
    '        End If
    '        Dim currentdate As Date = Date.Today
    '        Dim modify_by As String = "suraj"
    '        Dim modify_date As Date = Date.Today
    '        connectSql.OpenConnection()
    '        trans = clsDBFuncationality.GetTransactin()
    '        For i As Integer = 0 To dgvclasss.Rows.Count - 1
    '            If Not String.IsNullOrEmpty(dgvclasss.Rows(i).Cells(1).Value) Then
    '                connectSql.RunSpTransaction(trans, "sp_tspl_inv_class_insert", New SqlParameter("@name", Me.dgvclasss.Rows(i).Cells(1).Value), New SqlParameter("@length", Me.dgvclasss.Rows(i).Cells(2).Value.ToString()), New SqlParameter("@classtype", dgvclasss.Rows(i).Cells(3).Value), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", modify_by), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
    '            End If
    '        Next
    '        connectSql.RunSpTransaction(trans, "sp_TSPL_inv_parameters_update", New SqlParameter("@allownegativestock", strallownegative), New SqlParameter("@allownonstock", strallownonstock), New SqlParameter("@created_by", userCode), New SqlParameter("@created_date", connectSql.serverDate(trans)), New SqlParameter("Modify_By", userCode), New SqlParameter("Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@IsMRNQtyEdiatableOnSRN", IIf(chkIsEnterQtyOnSRN.Checked, 1, 0)))
    '        trans.Commit()
    '        myMessages.update()
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
    '        trans.Rollback()
    '        myMessages.myExceptions(ex)

    '    End Try
    'End Sub
    ''To Retrieve data from the table(tspl_inv_class) and (TSPL_inv_parameters)
    Private Sub Funfill()
        fndMCCItemCode.Value = clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)

        lblItemDesc.Text = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_Master where Item_code='" & fndMCCItemCode.Value & "'")
        TxtSampleRange.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCSampleRange, clsFixedParameterCode.MilkSetting, Nothing)
        TxtReceiptRange.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCReceiptRange, clsFixedParameterCode.MilkSetting, Nothing)
        chkControlSampleMandatory.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.ControlSampleMandatory, clsFixedParameterCode.MilkSetting, Nothing) = "1", True, False)
        chkDisAllowTankerDispatchTopalnt.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.DisAllowIntermittentTankerForPlantDispatch, Nothing) = "1", True, False)
        chkAllowSkippingPrevDocOnPayProcess.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowSkippingPrevDocumentsOnPaymentProcess, Nothing) = "1", True, False)
        chkAllowQcDateBeforeGateEntry.Checked = IIf(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowQcDateBeforeGateEntryDateTime, Nothing) = "1", True, False)
        txtCorrectionFactor.Text = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
        TxtDaysForFssaiPopup.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCFSSAI_DAYS, clsFixedParameterCode.MilkSetting, Nothing)
        TxtMilkWeight.Text = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Ratio, clsFixedParameterCode.MilkSetting, Nothing)
        Txttoleranceneg.Text = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Tolerance_Neg, clsFixedParameterCode.MilkSetting, Nothing)
        TxttolerancePlus.Text = clsFixedParameter.GetData(clsFixedParameterType.Milk_Can_Weight_Tolerance_Positive, clsFixedParameterCode.MilkSetting, Nothing)
        txtStockTolerance.Text = clsFixedParameter.GetData(clsFixedParameterType.AllowStockToleranceNegative, clsFixedParameterCode.AllowStockToleranceNegative, Nothing)
        TxtMinKMDiff.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCMinKmRange, clsFixedParameterCode.MilkSetting, Nothing)
        '============Rohit Oct 29,2014===================
        TxtInterval.Text = clsFixedParameter.GetData(clsFixedParameterType.MCCInvoiceScheduleInterval, clsFixedParameterCode.MilkSetting, Nothing)
        If clsCommon.myLen(clsFixedParameter.GetData(clsFixedParameterType.MCCInvoiceScheduleTime, clsFixedParameterCode.MilkSetting, Nothing)) > 0 Then
            DtpTime.Value = clsFixedParameter.GetData(clsFixedParameterType.MCCInvoiceScheduleTime, clsFixedParameterCode.MilkSetting, Nothing)
        End If
        If clsCommon.myLen(clsFixedParameter.GetData(clsFixedParameterType.MCCInvoiceScheduleDate, clsFixedParameterCode.MilkSetting, Nothing)) > 0 Then
            DtpStartingDate.Value = clsFixedParameter.GetData(clsFixedParameterType.MCCInvoiceScheduleDate, clsFixedParameterCode.MilkSetting, Nothing)
        End If
        '====================================================
        txtItemDescForTankerDisp.Text = clsFixedParameter.GetData(clsFixedParameterType.ItemDescForTankerdispatchPrint, clsFixedParameterCode.ItemDescForTankerDispatchPrint, Nothing)
        '=========Rohit 31 Jan,2015=======
        ChkSendSMS.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.Is_Send_Sms, clsFixedParameterCode.MilkSetting, Nothing)) = 0, False, True)
        If clsCommon.myLen(clsFixedParameter.GetData(clsFixedParameterType.Send_Sms_Time, clsFixedParameterCode.MilkSetting, Nothing)) > 0 Then
            DtpSendSMSTime.Value = clsFixedParameter.GetData(clsFixedParameterType.Send_Sms_Time, clsFixedParameterCode.MilkSetting, Nothing)
        End If
        '=====================================
        '=========Rohit 19 Mar,2015=======
        TxtUserName.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_Name, clsFixedParameterCode.MilkSetting, Nothing))
        TxtPWD.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_User_PWD, clsFixedParameterCode.MilkSetting, Nothing))
        TxtSendorID.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Sendor_ID, clsFixedParameterCode.MilkSetting, Nothing))
        CmbSMSProvider.Text = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SMS_Provider, clsFixedParameterCode.MilkSetting, Nothing))
        '=====================================
        '==============Rohit 05 June,2015=============
        ChkAllSampleParameter.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCDisplay_All_Parameter, clsFixedParameterCode.MilkSetting, Nothing)) = 0, False, True)
        '=================================================
        '==============Rohit 30 July,2015=============
        ChkAllSampleParameter.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DisplayAllParameterinQualityCheck, clsFixedParameterCode.DisplayAllParameterinQualityCheck, Nothing)) = 0, False, True)
        ChkDisplayTypeinMilkReceipt.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DisplayTypeInMilkReceipt, clsFixedParameterCode.DisplayTypeInMilkReceipt, Nothing)) = 0, False, True)
        ChkValidationofMilkType.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AddValidationofMilkTypeinsample, clsFixedParameterCode.AddValidationofMilkTypeinsample, Nothing)) = 0, False, True)
        GetMilkTypeDt()
        GetMilkTypeSubStandardDt()
        chkItemMilkType.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing)) = 0, False, True)
        chkPriceChartGradeWise.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing)) = 0, False, True)
        If chkItemMilkType.Checked = True Then
            chkPriceChartGradeWise.Visible = True
        Else
            chkPriceChartGradeWise.Visible = False
        End If
        ChkFarmerPaymentCycle.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isFarmerPaymentCycle, clsFixedParameterCode.isFarmerPaymentCycle, Nothing)) = 0, False, True)
        '=================================================
        chkStdFATSNFRate.Checked = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyStdFATSNFRate, clsFixedParameterCode.ApplyStdFATSNFRate, Nothing)) = 1, True, False)


        dtpBlockingDBTPeriod.Checked = False
        Dim str As String = clsFixedParameter.GetData(clsFixedParameterType.AndroidAPP, clsFixedParameterCode.StopDBTBefore, Nothing)
        If clsCommon.myLen(str) > 0 Then
            dtpBlockingDBTPeriod.Checked = True
            dtpBlockingDBTPeriod.Value = clsCommon.myCDate(str)
        End If
    End Sub

    Sub GetMilkTypeDt()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Field")
            dt.Columns("Field").ReadOnly = True
            dt.Columns.Add("FAT(Min)")
            dt.Columns.Add("FAT(Max)")
            dt.Columns.Add("SNF(Min)")
            dt.Columns.Add("SNF(Max)")

            Dim dr As DataRow = dt.NewRow()
            dr("Field") = "Cow"
            dr("FAT(Min)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinCow, clsFixedParameterCode.FatMinCow, Nothing)), 0)
            dr("FAT(Max)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxCow, clsFixedParameterCode.FatMaxCow, Nothing)), 0)
            dr("SNF(Min)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinCow, clsFixedParameterCode.SNFMinCow, Nothing)), 0)
            dr("SNF(Max)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxCow, clsFixedParameterCode.SNFMaxCow, Nothing)), 0)
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Field") = "Buffalow"
            dr("FAT(Min)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinBuff, clsFixedParameterCode.FatMinBuff, Nothing)), 0)
            dr("FAT(Max)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxBuff, clsFixedParameterCode.FatMaxBuff, Nothing)), 0)
            dr("SNF(Min)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinBuff, clsFixedParameterCode.SNFMinBuff, Nothing)), 0)
            dr("SNF(Max)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxBuff, clsFixedParameterCode.SNFMaxBuff, Nothing)), 0)
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Field") = "Mix"
            dr("FAT(Min)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMinMix, clsFixedParameterCode.FatMinMix, Nothing)), 0)
            dr("FAT(Max)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FatMaxMix, clsFixedParameterCode.FatMaxMix, Nothing)), 0)
            dr("SNF(Min)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMinMix, clsFixedParameterCode.SNFMinMix, Nothing)), 0)
            dr("SNF(Max)") = IIf(ChkValidationofMilkType.Checked, clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SNFMaxMix, clsFixedParameterCode.SNFMaxMix, Nothing)), 0)
            dt.Rows.Add(dr)

            gv1.AllowAddNewRow = False
            gv1.DataSource = dt

            gv1.Columns("Field").Width = 120
            gv1.Columns("FAT(Min)").Width = 120
            gv1.Columns("FAT(Max)").Width = 120
            gv1.Columns("SNF(Min)").Width = 120
            gv1.Columns("SNF(Max)").Width = 120

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Sub GetMilkTypeSubStandardDt()
        Try
            Dim dt As New DataTable
            dt.Columns.Add("Field")
            dt.Columns("Field").ReadOnly = True
            dt.Columns.Add("FAT")
            dt.Columns.Add("SNF")

            Dim dr As DataRow = dt.NewRow()
            dr("Field") = "Cow"
            dr("FAT") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdFatCow, clsFixedParameterCode.SubStdFatCow, Nothing))
            dr("SNF") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdSNFCow, clsFixedParameterCode.SubStdSNFCow, Nothing))
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Field") = "Buffalow"
            dr("FAT") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdFatBuff, clsFixedParameterCode.SubStdFatBuff, Nothing))
            dr("SNF") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdSNFBuff, clsFixedParameterCode.SubStdSNFBuff, Nothing))
            dt.Rows.Add(dr)

            dr = dt.NewRow()
            dr("Field") = "Mix"
            dr("FAT") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdFatMix, clsFixedParameterCode.SubStdFatMix, Nothing))
            dr("SNF") = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SubStdSNFMix, clsFixedParameterCode.SubStdSNFMix, Nothing))
            dt.Rows.Add(dr)

            gv2.AllowAddNewRow = False
            gv2.DataSource = dt

            gv2.Columns("Field").Width = 120
            gv2.Columns("FAT").Width = 120
            gv2.Columns("SNF").Width = 120
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
    ''export function
    Private Sub mnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnexport.Click
        Dim str As String = "select distinct m.Inv_Class_No as[Invoice Class No], m.Inv_Class_Name as [Invoice Class Name], m.Inv_Class_Length AS [Invoice Class Length], m.Class_Type as [Class Type], d.Allow_Negative_Inv as [Allow Negative Inventory], d.Allow_Non_Stock as [Allow Non Stock], m.Created_By as [Created By], m.Created_Date  as [Created Date], m.Modify_By as [Modify By], m.Modify_Date as [Modify Date], m.Comp_Code as [Company Code] from TSPL_inv_parameters d join TSPL_INV_CLASS m on m.Comp_Code = d.Comp_Code "
        Dim orderByClause = "[Invoice Class No] "
        ListImpExpColumnsMandatory = New List(Of String)({"Invoice Class Name", "Invoice Class Length"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Invoice Class Name"})
        transportSql.ExporttoExcel(str, "", orderByClause, Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub
    ''To close the form
    Private Sub mnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnclose.Click
        Me.Close()
    End Sub
    ''import function
    Private Sub mnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Invoice Class Name", "Invoice Class Length", "Class Type", "Allow Negative Inventory", "Allow Non Stock") Then
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strinvoiceclass As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strinvoiceclasslength As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strclasstype As String = clsCommon.myCstr(grow.Cells(2).Value)
                    Dim strallownegative As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim strallownonstock As String = clsCommon.myCstr(grow.Cells(4).Value)
                    ' Dim str5 As String = grow.Cells(5).Value
                    If String.IsNullOrEmpty(strinvoiceclass) Then
                        Throw New Exception("Invoice Class Name has some error")

                    End If
                    If String.IsNullOrEmpty(strinvoiceclasslength) Then
                        Throw New Exception("Invoice Class Name has some error")

                    End If
                    Dim sql1 As String = "select count(*) from TSPL_INV_CLASS where Inv_Class_Name='" + strinvoiceclass + "'"
                    Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                    If (i = 0) Then
                        Dim Sql As String = "insert into TSPL_INV_CLASS(Inv_Class_Name, Inv_Class_Length, Class_Type,Created_By, Created_Date, Modify_By, Modify_Date, Comp_Code) values ('" + strinvoiceclass + "', '" + strinvoiceclasslength + "', '" + strclasstype + "','" + userCode + "', '" + connectSql.serverDate() + "', '" + userCode + "', '" + connectSql.serverDate() + "', '" + companyCode + "' )"
                        connectSql.RunSqlTransaction(trans, Sql)
                        Dim strupdate As String = "update TSPL_INV_PARAMETERS set Allow_Negative_Inv = '" + strallownegative + "', Allow_Non_Stock = '" + strallownonstock + "', Created_By = '" + userCode + "', Create_Date = '" + connectSql.serverDate(trans) + "', Modify_By = '" + userCode + "', Modify_Date = '" + connectSql.serverDate(trans) + "', Comp_Code = '" + companyCode + "'"
                        connectSql.RunSqlTransaction(trans, strupdate)

                    Else
                        Dim sql As String = "update TSPL_INV_CLASS set Inv_Class_Length = '" + strinvoiceclasslength + "', Class_Type = '" + strclasstype + "' where Inv_Class_Name = '" + strinvoiceclass + "' "
                        connectSql.RunSqlTransaction(trans, sql)
                        Dim strupdate As String = "update TSPL_INV_PARAMETERS set Allow_Negative_Inv = '" + strallownegative + "', Allow_Non_Stock = '" + strallownonstock + "', Created_By = '" + userCode + "', Create_Date = '" + connectSql.serverDate(trans) + "', Modify_By = '" + userCode + "', Modify_Date = '" + connectSql.serverDate(trans) + "', Comp_Code = '" + companyCode + "'"
                        connectSql.RunSqlTransaction(trans, strupdate)
                    End If

                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                'trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)

    End Sub
    ''To check segment is used or not 

    Private Sub fndMCCItemCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMCCItemCode._MYValidating
        Try
            Dim obj As New clsItemMaster
            obj = clsItemMaster.FinderForItem(fndMCCItemCode.Value, "", True, " Product_type='MI' ")
            fndMCCItemCode.Value = obj.Item_Code
            lblItemDesc.Text = obj.Item_Desc
        Catch ex As Exception
        End Try
    End Sub

    Private Sub chkItemMilkType_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkItemMilkType.ToggleStateChanged
        If chkItemMilkType.Checked Then
            chkPriceChartGradeWise.Visible = True
        Else
            chkPriceChartGradeWise.Visible = False
        End If
    End Sub
End Class
