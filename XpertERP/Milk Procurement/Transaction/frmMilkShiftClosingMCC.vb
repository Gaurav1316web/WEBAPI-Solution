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

'''' <summary>
'''' ''''''''''''''''''''''''TicketNo='BM00000001540''''''''''''''''''''''''''''''''''''''''
'''' </summary>
'''' <remarks></remarks>
'Display Status(Accepted/rejected/Special Approval) Columns when Company Code is NATD.
''updation by richa agarwal against ticket no BM00000008361 send mail and sms to all vsps which are tagged at milk shift end,BM00000008565,BM00000008626
'UDL/17/04/18-000107
'UDL/05/04/18-000083 by balwinder on 28/05/2018
'' work done agianst ticket no.MIL/31/01/19-000037
Public Class frmMilkShiftClosingMCC
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const colvlc_Doc_Code As String = "colvlc_Doc_Code"
    Const colVlc_Code As String = "colVlc_Code"
    Const colVSP_Code As String = "colVSP_Code"
    Const colVehicle_Code As String = "colVehicle_Code"
    Const colVLC_Procurement_Data_MP As String = "colVLC_Procurement_Data_MP"
    Const col_Show_Attachment As String = "col_Show_Attachment"
    Const col_Attachment As String = "col_Attachment"
    Const col_Remarks As String = "col_Remarks"
    Const colAorC As String = "colAorc"
    Const colRejectedorAcccepted As String = "colRejectedorAcccepted"
    Const colDeduction_of_VSP As String = "colDeduction_of_VSP"
    Const col_Attachment_ID As String = "col_Attachment_ID"
    'Const col_Deduction_of_transporter As String = "col_Deduction_of_transporter"


    '====================Route Columns==========================
    Const colRoute_Name As String = "colRoute_Name"
    Const colRoute_Code As String = "colRoute_Code"
    Const col_truck_Sheet_of_Transporter As String = "col_truck_Sheet_of_Transporter"
    Const col_Opening_KM As String = "col_Opening_KM"
    Const col_Closing_KM As String = "col_Closing_KM"
    Const col_Total_KM As String = "col_Total_KM"
    Const col_Status As String = "col_Status"
    Const col_Actual_KM As String = "col_Actual_KM"
    Const col_Actual_Payable_KM As String = "col_Actual_Payable_KM"
    Const col_Difference As String = "col_Difference"
    Const col_Deduction_of_transporter As String = "col_Deduction_of_transporter"
    Const col_Reason_Deduction As String = "col_Reason_Deduction"
    Const col_reason As String = "col_reason"
    Const col_Head_Load As String = "col_Head_Load"
    Const col_charge_Amount As String = "col_charge_Amount"
    Const col_OwnAsset_Amount As String = "col_OwnAsset_Amount"
    Const col_Head_load_Amount As String = "col_Head_load_Amount"
    Const col_Attachment_Route_ID As String = "col_Attachment_ID"



    Const col_Vehicle_Code As String = "col_Vehicle_Code"
    Const col_Vehicle_Name As String = "col_Vehicle_Name"
    Const Col_Rate As String = "Col_Rate"
    Const col_Amount As String = "col_Amount"
    Const col_StdQty As String = "col_StdQty"
    Const col_Shift_Charge As String = "col_Shift_Charge"
    Const col_Weight As String = "col_Weight"
    Const col_MilkWeight As String = "col_MilkWeight"
    Const col_ActualWeight As String = "col_ActualWeight"
    Const col_Vehicle As String = "col_Vehicle"
    '======================================================================
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim is_Entered_Manually As Boolean = False
    Public DtMilkReceipt, DtSlab, DtCharge_detail As DataTable
    Dim objSr As New clsWeighingMachine
    Dim objSerial As New clsSerialPort
    Dim openFileDialog1 As New OpenFileDialog
    Dim MinKmRange As Double = 0
    Dim DtStock As DataTable
    Dim DtMilkShiftEnd, DtChilling_Vendor_Provision As DataTable
    Dim StdInterfaceSett As Boolean = True
    Dim AllowManualEntryOfDeduction As Boolean
    Dim AllowZeroQtyFATSNF As Boolean = False
    Dim isCLRInsteadOfSNF As Boolean = False
    Dim dclCorrectionFactor As Decimal = 0
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False
    Dim settSeprateDistanceMorningEveningShift As Boolean = False
#End Region
#Region "Functions"
    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmMilkShiftEndMCC)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '  btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        settSeprateDistanceMorningEveningShift = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SeprateDistanceMorningEveningShift, clsFixedParameterCode.SeprateDistanceMorningEveningShift, Nothing)) > 0, True, False)
        AllowManualEntryOfDeduction = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShiftEndAllowManualEntryOfDeduction, clsFixedParameterCode.ShiftEndAllowManualEntryOfDeduction, Nothing)) > 0, True, False)
        AllowZeroQtyFATSNF = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowZeroQtyFATSNFInCloseMCCShift, clsFixedParameterCode.AllowZeroQtyFATSNFInCloseMCCShift, Nothing)) > 0, True, False)

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
        ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
        ''End of For Custom Fields
        AddNew()
        ' LoadData(Me.txtCode.Value)
        Me.fndMcc.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join tspl_mcc_master on mcc_code=Default_Location  where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Me.fndMccCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_name from TSPL_MCC_MASTER where MCC_Code='" + clsCommon.myCstr(fndMcc.Value) + "' "))

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowEditRow = False
        gv1.MasterTemplate.AllowCellContextMenu = True
        gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv1.MasterTemplate.AllowDeleteRow = True
        GetshiftType()
        ReStoreGridLayout()
        If clsCommon.myCstr(fndMccCode.Text) <> "" Then
            Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMcc.Value)
            If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No shift is opened. one Shift Must be Opened..", Me.Text)
            ElseIf DTShift.Rows.Count > 1 Then
                clsCommon.MyMessageBoxShow(Me, "There are more then one shifts are opened.Only one Shift can be Opened..", Me.Text)
            Else
                cboShift.SelectedValue = DTShift.Rows(0).Item("Shift")
                dtpDocDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
                DtpMCCDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
                dtpDocDate.ReadOnly = True
                cboShift.Enabled = False
                DtpMCCDate.ReadOnly = True
                LoadMilk_receipt_Code()
            End If
        End If
        'Dim sQuery As String = "select * from tspl_slab_range_detail where Form_ID='PTV-MST'"
        Dim sQuery As String = "select tspl_slab_range_detail.*,coalesce(Is_Additional,'F') as Is_Additional from tspl_slab_range_detail inner join TSPL_Primary_Vehicle_Master on Vehicle_Code=Trans_ID order by slab_upto"
        DtSlab = clsDBFuncationality.GetDataTable(sQuery)
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), , NavigatorType.Current)
        End If
        ''RICHA BHA/25/10/18-000640 25 OCT,2018
        Dim qry As String = "select AskSiloatShiftEnd,AutoIn_Location,SILOIn_Location,MCC_in_Plant from TSPL_MCC_MASTER where MCC_Code='" + fndMcc.Value + "'"
        Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0 Then
            fndAutoInLoc.Value = clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))
            chkAskSiloatShiftEnd.Checked = IIf(clsCommon.myCdbl(dtMCC.Rows(0)("AskSiloatShiftEnd")) = 1, True, False)
            If chkAskSiloatShiftEnd.Checked = True Then
                fndSiloInLoc.Visible = True
                txtSiloInLoc.Visible = True
                lblSiloInLocation.Visible = True
            Else
                fndSiloInLoc.Visible = False
                txtSiloInLoc.Visible = False
                lblSiloInLocation.Visible = False
            End If
        End If
    End Sub

    Sub SaveData()
        Dim ArrGateEntryWithPenalty As ArrayList = Nothing
        Dim qry As String
        Try
            qry = clsMilkGateEntryIn.GetQeryForPenalty(fndMcc.Value, clsCommon.myCstr(cboShift.SelectedValue), dtpDocDate.Value, "")
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ArrGateEntryWithPenalty = clsCommon.ShowMultipleSelectForm(False, "MILRETP", qry, "Entry_Code", Nothing, ArrGateEntryWithPenalty, Nothing)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Exit Sub
        End Try

        Dim trans As SqlTransaction = Nothing
        Try
            If (AllowToSave()) Then
                If LCase(btnsave.Text) = "save" Then
                    Dim dt As DataTable = clsMilkSampleQCParameterDetail.GetExtraQCParameters()
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        If clsCommon.MyMessageBoxShow("Have you Filled Extra QC Parameter in Milk sample.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                            Exit Sub
                        End If
                    End If
                    If clsCommon.MyMessageBoxShow("Can't Do Any Modification/Editing After Shift Closer " & vbNewLine _
                                                                  & " Are you sure to Close the Current Shift.", "Save", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
                trans = clsDBFuncationality.GetTransactin()

                Dim objHead As clsMilkShiftEndMCC
                '' asign screen vaules in objHead
                objHead = New clsMilkShiftEndMCC
                objHead.ArrGateEntryWithPenalty = ArrGateEntryWithPenalty
                objHead.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                objHead.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                objHead.MCC_DATE = clsCommon.myCDate(DtpMCCDate.Value)
                objHead.SHIFT = clsCommon.myCstr(Me.cboShift.SelectedValue)
                '  objHead.COMM_PORT = clsCommon.myCstr(cboComPort.Text)
                'objHead.MCC_CODE = clsCommon.myCstr(fndMccCode.Text)
                objHead.MCC_CODE = clsCommon.myCstr(fndMcc.Value)
                objHead.Actual_Stock = clsCommon.myCdbl(TxtActualStock.Text)

                objHead.Manual_Stock = clsCommon.myCdbl(TxtManualStock.Text)
                objHead.Manual_FAT = clsCommon.myCdbl(TxtManualFAT.Text)
                objHead.Manual_SNF = clsCommon.myCdbl(TxtManualSNF.Text)
                objHead.Actual_FAT = clsCommon.myCdbl(TxtActualFat.Text)
                objHead.Actual_SNF = clsCommon.myCdbl(TxtActualSNF.Text)
                objHead.Manual_FAT_Per = clsCommon.myCdbl(TxtManualFat_Per.Text)
                objHead.Manual_SNF_Per = clsCommon.myCdbl(TxtManualSNF_Per.Text)
                objHead.Actual_FAT_Per = clsCommon.myCdbl(TxtBookFat_Per.Text)
                objHead.Actual_SNF_Per = clsCommon.myCdbl(TxtBookSNF_per.Text)
                objHead.CLR = txtCLR.Value
                ''RICHA BHA/25/10/18-000640 25 OCT,2018
                objHead.AutoIn_Location = clsCommon.myCstr(fndAutoInLoc.Value)
                objHead.SILOIn_Location = clsCommon.myCstr(fndSiloInLoc.Value)
                objHead.AskSiloatShiftEnd = chkAskSiloatShiftEnd.Checked
                ''---------------------
                Dim objList As New List(Of clsMilkShiftEndMCCDetail)
                Dim objList_Route As New List(Of clsMilkShiftEndMCC_Route_Detail)
                Dim objListAttachment As New List(Of clsMilkShiftEndAttachment)



                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim obj As clsMilkShiftEndMCCDetail

                    obj = New clsMilkShiftEndMCCDetail

                    If clsCommon.myLen(grow.Cells(colVLC_Procurement_Data_MP).Value) > 0 AndAlso clsCommon.myCstr(grow.Cells(colVLC_Procurement_Data_MP).Value).ToString.Contains("\") Then
                        Dim objAttachment As clsMilkShiftEndAttachment

                        objAttachment = New clsMilkShiftEndAttachment
                        objAttachment.Transaction_ID = clsCommon.myCstr(grow.Cells(colvlc_Doc_Code).Value)

                        objAttachment.Form_ID = Form_ID
                        objAttachment.ColCOMMENTS = clsCommon.myCstr(grow.Cells(col_Remarks).Value)
                        objAttachment.ColFileName = clsCommon.myCstr(grow.Cells(colVLC_Procurement_Data_MP).Value.ToString.Substring(grow.Cells(colVLC_Procurement_Data_MP).Value.ToString.LastIndexOf("\") + 1, grow.Cells(colVLC_Procurement_Data_MP).Value.ToString.Length - grow.Cells(colVLC_Procurement_Data_MP).Value.ToString.LastIndexOf("\") - 1))
                        objAttachment.ColFormId = Form_ID
                        objAttachment.ColPath = clsCommon.myCstr(grow.Cells(colVLC_Procurement_Data_MP).Value) 'IIf(clsCommon.myLen(grow.Cells(col_Attachment_ID).Value) <= 0, clsCommon.myCstr(grow.Cells(colVLC_Procurement_Data_MP).Value), "")
                        objAttachment.ColTransactionId = clsCommon.myCstr(grow.Cells(colvlc_Doc_Code).Value)
                        objListAttachment.Add(objAttachment)
                    End If

                    obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)

                    obj.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                    obj.SHIFT = clsCommon.myCstr(Me.cboShift.Text)
                    obj.MCC_CODE = clsCommon.myCstr(fndMcc.Value) ' clsCommon.myCstr(fndMccCode.Text)
                    obj.VLC_DOC_CODE = clsCommon.myCstr(grow.Cells(colvlc_Doc_Code).Value)
                    obj.VLC_CODE = clsCommon.myCstr(grow.Cells(colVlc_Code).Value)
                    obj.VSP_CODE = clsCommon.myCstr(grow.Cells(colVSP_Code).Value)
                    obj.VEHICLE_CODE = clsCommon.myCstr(grow.Cells(colVehicle_Code).Value)
                    obj.Remarks = clsCommon.myCstr(grow.Cells(col_Remarks).Value)
                    obj.deduction_of_VSP = clsCommon.myCdbl(grow.Cells(colDeduction_of_VSP).Value)
                    obj.Deduction_Reason = clsCommon.myCstr(grow.Cells(col_reason).Value)
                    obj.Amount_or_Rate = clsCommon.myCstr(grow.Cells(colAorC).Value)
                    obj.Rejected_or_Acccepted = clsCommon.myCstr(grow.Cells(colRejectedorAcccepted).Value)
                    objList.Add(obj)
                Next

                For Each grow As GridViewRowInfo In GvRoute.Rows
                    If clsCommon.myLen(grow.Cells(colRoute_Code).Value) > 0 Then
                        Dim obj As clsMilkShiftEndMCC_Route_Detail = New clsMilkShiftEndMCC_Route_Detail
                        If clsCommon.myLen(grow.Cells(col_truck_Sheet_of_Transporter).Value) > 0 And clsCommon.myCstr(grow.Cells(col_truck_Sheet_of_Transporter).Value).ToString.Contains("\") Then
                            Dim objAttachment As clsMilkShiftEndAttachment
                            objAttachment = New clsMilkShiftEndAttachment
                            objAttachment.Transaction_ID = clsCommon.myCstr(grow.Cells(colRoute_Code).Value)
                            objAttachment.Form_ID = Form_ID
                            objAttachment.ColCOMMENTS = "For Route"
                            objAttachment.ColFileName = clsCommon.myCstr(grow.Cells(col_truck_Sheet_of_Transporter).Value.ToString.Substring(grow.Cells(col_truck_Sheet_of_Transporter).Value.ToString.LastIndexOf("\") + 1, grow.Cells(col_truck_Sheet_of_Transporter).Value.ToString.Length - grow.Cells(col_truck_Sheet_of_Transporter).Value.ToString.LastIndexOf("\") - 1))
                            objAttachment.ColFormId = Form_ID
                            objAttachment.ColPath = clsCommon.myCstr(grow.Cells(col_truck_Sheet_of_Transporter).Value) 'IIf(clsCommon.myLen(grow.Cells(col_Attachment_ID).Value) <= 0, clsCommon.myCstr(grow.Cells(colVLC_Procurement_Data_MP).Value), "")
                            objAttachment.ColTransactionId = clsCommon.myCstr(grow.Cells(colRoute_Code).Value)
                            objListAttachment.Add(objAttachment)
                        End If

                        obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)

                        obj.DOC_DATE = clsCommon.myCDate(dtpDocDate.Value)
                        obj.Route_CODE = clsCommon.myCstr(grow.Cells(colRoute_Code).Value)
                        obj.Opening_KM = clsCommon.myCstr(grow.Cells(col_Opening_KM).Value)
                        obj.Closing_KM = clsCommon.myCstr(grow.Cells(col_Closing_KM).Value)
                        obj.Total_KM = clsCommon.myCstr(grow.Cells(col_Total_KM).Value)
                        obj.Status = clsCommon.myCstr(grow.Cells(col_Status).Value)
                        obj.Actual_KM = clsCommon.myCstr(grow.Cells(col_Actual_KM).Value)
                        obj.Actual_Payable_KM = clsCommon.myCstr(grow.Cells(col_Actual_Payable_KM).Value)
                        obj.Diff_Km = clsCommon.myCstr(grow.Cells(col_Difference).Value)
                        obj.Reason = clsCommon.myCstr(grow.Cells(col_reason).Value)
                        obj.Deduction_Reason = clsCommon.myCstr(grow.Cells(col_Reason_Deduction).Value)
                        obj.deduction_of_Transporter = clsCommon.myCdbl(grow.Cells(col_Deduction_of_transporter).Value)
                        obj.Milk_Weight = clsCommon.myCdbl(grow.Cells(col_MilkWeight).Value)
                        obj.Actual_Weight = clsCommon.myCdbl(grow.Cells(col_ActualWeight).Value)
                        obj.Vehicle_Code = clsCommon.myCstr(grow.Cells(col_Vehicle_Code).Value)
                        obj.Rate_KM = clsCommon.myCstr(grow.Cells(Col_Rate).Value)
                        obj.Amount = clsCommon.myCstr(grow.Cells(col_Amount).Value)
                        obj.Std_Qty = clsCommon.myCdbl(grow.Cells(col_StdQty).Value)
                        obj.Shift_Charge = clsCommon.myCdbl(grow.Cells(col_Shift_Charge).Value)
                        obj.Is_Head_Load = clsCommon.myCdbl(grow.Cells(col_Head_Load).Value)
                        obj.Charge_Amt = clsCommon.myCdbl(grow.Cells(col_charge_Amount).Value)
                        obj.Own_Asset_Amount = clsCommon.myCdbl(grow.Cells(col_OwnAsset_Amount).Value)
                        obj.Head_Load_Amt = clsCommon.myCdbl(grow.Cells(col_Head_load_Amount).Value)
                        objList_Route.Add(obj)
                    End If
                Next


                Dim dtshift As DataTable = clsMilkReceiptMCC.GetShift(objHead.MCC_CODE, trans)
                If dtshift IsNot Nothing AndAlso dtshift.Rows.Count > 0 Then
                    objHead.Irregular_Mcc_Code = clsCommon.myCstr(dtshift.Rows(0).Item("Irregular_Mcc_Code"))
                End If
                ''For Custom Fields
                objHead.Form_ID = MyBase.Form_ID
                objHead.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(objHead.arrCustomFields)
                End If
                If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                    clsCustomFieldGrid.GetData(objHead.arrCustomFields, gv1, MyBase.ArrDetailFields, colRoute_Code)
                End If
                ''End of For Custom Fields
                If isCheckNumberAndMailIdExistsForAllVSP(trans) Then
                    If clsMilkShiftEndMCC.SaveData(objHead, objList, objList_Route, trans) Then
                        clsMilkShiftEndMCC.SendSMSDataNew(objHead, trans)
                        trans.Commit()
                        'Comment code as per Balwinder sir
                        ''--------------------- For Kwality Only---------------------------
                        'Dim objsms As New FrmMccSMSSetting
                        'objsms.SendMail(objHead.DOC_CODE, objHead.DOC_DATE.ToString, objHead.MCC_CODE, Form_ID, cboShift.Text, LblShiftOpeningTime.Text, DtpTime.Value)
                        ' ''richa agarwal
                        'Dim objsmsVSP As New FrmMccSMSSetting
                        'objsmsVSP.SendMailForVSP(objHead.DOC_CODE, objHead.DOC_DATE.ToString, objHead.MCC_CODE, Form_ID + "VSP", cboShift.Text, LblShiftOpeningTime.Text, DtpTime.Value)
                        ''---------------------End of For Kwality Only---------------------------
                        'Comment code as per Balwinder sir
                        If clsMilkShiftEndAttachment.SaveData(objHead.DOC_CODE, objListAttachment, trans) Then
                            clsCommon.MyMessageBoxShow(Me, "Shift Ended Successfully", Me.Text)
                        End If
                        LoadData(objHead.DOC_CODE)
                    End If
                Else
                    trans.Rollback()
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Sub SendSMSDataNew(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
    '    CreateSMSContentEMP(obj, trans)
    '    CreateSMSContentVSP(obj, trans)
    '    CreateMailContent(obj, trans)
    'End Sub

    'Sub CreateSMSContentEMP(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
    '    Dim strSMSContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT SMS_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmMilkShiftEndMCC + "'", trans))
    '    If clsCommon.myLen(strSMSContent) > 0 Then
    '        Dim qry As String = "select max(State_Code) as State_Code,max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.MCC_CODE,max(MCC_NAME) as MCC_NAME,convert(date,DOC_DATE,103) as DOC_DATE,cast(round(sum(Qty),2,2) as float) as Qty,max(TSPL_Mcc_UOM_DETAIL.UOM_Code) as UOM_Code,count(distinct(TSPL_MILK_SRN_HEAD.ROUTE_CODE)) as Route_Count,count(distinct(TSPL_MILK_SRN_HEAD.VLC_CODE)) as VLC_Count,convert(decimal(18,2), case when sum(Qty)>0 then sum (FAT_KG)*100/sum(Qty) else 0 end) as FATPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(SNF_KG)*100/sum(Qty) else 0 end) as SNFPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(Amount)/sum(Qty) else 0 end) as Rate,convert(decimal(18,2), sum(Amount) ) as Amount " + Environment.NewLine + _
    '          " from TSPL_MILK_SRN_DETAIL " + Environment.NewLine + _
    '          " inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine + _
    '          " left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_MILK_SRN_HEAD.MCC_CODE and Stocking_Unit='Y' " + Environment.NewLine + _
    '          " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE " + Environment.NewLine + _
    '          " where  convert(date,doc_date,103)=convert(date,'" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and shift='" + obj.SHIFT + "' and TSPL_MILK_SRN_HEAD.MCC_CODE ='" + obj.MCC_CODE + "' " + Environment.NewLine + _
    '          " group by tspl_milk_srn_Head.mcc_code,convert(date,doc_date,103)"
    '        Dim dtData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
    '            Dim objSMSH As New clsSMSHead()
    '            objSMSH.Created_Date = obj.DOC_DATE
    '            objSMSH.SMS_Text = strSMSContent
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.State, clsCommon.myCstr(dtData.Rows(0)("State_Code")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dtData.Rows(0)("MCC_CODE")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dtData.Rows(0)("MCC_NAME")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dtData.Rows(0)("Mcc_Code_VLC_Uploader")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(clsCommon.myCDate(dtData.Rows(0)("DOC_DATE")), "dd/MMM/yyyy"))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCstr(dtData.Rows(0)("Qty")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.UOM, clsCommon.myCstr(dtData.Rows(0)("UOM_Code")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfVLC, clsCommon.myCstr(dtData.Rows(0)("VLC_Count")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfRoute, clsCommon.myCstr(dtData.Rows(0)("Route_Count")))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("FATPER"))))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("SNFPER"))))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderRate, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Rate"))))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderAmt, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Amount"))))
    '            objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderShift, obj.SHIFT)
    '            objSMSH.arrMobilNo = New List(Of String)()
    '            'objSMSH.arrMobilNo.Add(clsCommon.myCstr(dt.Rows(0)("MobileNo")))
    '            objSMSH.SaveData(clsUserMgtCode.frmMilkShiftEndMCC, objSMSH, trans)
    '            objSMSH = Nothing
    '        End If
    '    End If
    'End Sub

    'Shared Sub CreateSMSContentVSP(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
    '    Dim strSMSContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT SMS_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmMilkSample + "'", trans))
    '    If clsCommon.myLen(strSMSContent) > 0 Then
    '        Dim qry As String = "select TSPL_MILK_SRN_HEAD.SAMPLE_NO,TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,convert(Date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader" + Environment.NewLine + _
    '        " ,case when TSPL_MILK_SRN_DETAIL.FAT_PER<=5 then 'C' else 'B' end as MilkType,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,TSPL_MILK_SRN_DETAIL.RATE,TSPL_MILK_SRN_DETAIL.AMOUNT" + Environment.NewLine + _
    '        " ,substring (Phone1 ,6,10) as Phone1" + Environment.NewLine + _
    '        " from TSPL_MILK_SRN_DETAIL" + Environment.NewLine + _
    '        " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine + _
    '        " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE " + Environment.NewLine + _
    '        " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  " + Environment.NewLine + _
    '        " where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_CODE + "' and convert(Date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and  TSPL_MILK_SRN_HEAD.SHIFT='" + obj.SHIFT + "' and len(replace( isnull(substring(TSPL_VENDOR_MASTER.Phone1,6,10),''),'_',''))>0 order by TSPL_MILK_SRN_HEAD.SAMPLE_NO  "
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim objSMSH As New clsSMSHead()
    '                objSMSH.SMS_Text = strSMSContent
    '                objSMSH.Created_Date = obj.DOC_DATE
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(clsCommon.myCDate(dr("DOC_DATE")), "dd/MM/yyyy"))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderShift, obj.SHIFT)
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dr("MCC_CODE")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dr("MCC_NAME")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dr("Mcc_Code_VLC_Uploader")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSPCode, clsCommon.myCstr(dr("VSP_CODE")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSP, clsCommon.myCstr(dr("VLC_Name")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSPUploaderCode, clsCommon.myCstr(dr("VLC_Code_VLC_Uploader")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MilkTypeCB, clsCommon.myCstr(dr("MilkType")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.UOM, clsCommon.myCstr(dr("UOM_Code")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderQty, clsCommon.myCstr(dr("Qty")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myCstr(dr("FAT_PER")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myCstr(dr("SNF_PER")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderRate, clsCommon.myCstr(dr("RATE")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderAmt, clsCommon.myCstr(dr("AMOUNT")))

    '                objSMSH.arrMobilNo = New List(Of String)()
    '                objSMSH.arrMobilNo.Add(clsCommon.myCstr(dr("Phone1")))
    '                objSMSH.SaveData(clsUserMgtCode.frmMilkSample, objSMSH, trans)
    '                objSMSH = Nothing
    '            Next
    '        End If
    '    End If
    'End Sub
    'Sub CreateMailContent(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
    '    Dim strMailContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Email_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmMilkShiftEndMCC + "'", trans))
    '    If clsCommon.myLen(strMailContent) > 0 Then
    '        Dim qry As String = "select max(State_Code) as State_Code,max(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.MCC_CODE,max(MCC_NAME) as MCC_NAME,convert(date,DOC_DATE,103) as DOC_DATE,cast(round(sum(Qty),2,2) as float) as Qty,max(TSPL_Mcc_UOM_DETAIL.UOM_Code) as UOM_Code,count(distinct(TSPL_MILK_SRN_HEAD.ROUTE_CODE)) as Route_Count,count(distinct(TSPL_MILK_SRN_HEAD.VLC_CODE)) as VLC_Count,convert(decimal(18,2), case when sum(Qty)>0 then sum (FAT_KG)*100/sum(Qty) else 0 end) as FATPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(SNF_KG)*100/sum(Qty) else 0 end) as SNFPER,convert(decimal(18,2), case when sum(Qty)>0 then sum(Amount)/sum(Qty) else 0 end) as Rate,convert(decimal(18,2), sum(Amount) ) as Amount " + Environment.NewLine + _
    '          " from TSPL_MILK_SRN_DETAIL " + Environment.NewLine + _
    '          " inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine + _
    '          " left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE=TSPL_MILK_SRN_HEAD.MCC_CODE and Stocking_Unit='Y' " + Environment.NewLine + _
    '          " left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE " + Environment.NewLine + _
    '          " where  convert(date,doc_date,103)=convert(date,'" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and shift='" + obj.SHIFT + "' and TSPL_MILK_SRN_HEAD.MCC_CODE ='" + obj.MCC_CODE + "' " + Environment.NewLine + _
    '          " group by tspl_milk_srn_Head.mcc_code,convert(date,doc_date,103)"
    '        Dim dtData As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
    '            Dim objSMSH As New clsEMailHead()
    '            objSMSH.Created_Date = obj.DOC_DATE
    '            objSMSH.Email_Text = strMailContent
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.State, clsCommon.myCstr(dtData.Rows(0)("State_Code")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dtData.Rows(0)("MCC_CODE")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dtData.Rows(0)("MCC_NAME")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dtData.Rows(0)("Mcc_Code_VLC_Uploader")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(clsCommon.myCDate(dtData.Rows(0)("DOC_DATE")), "dd/MMM/yyyy"))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.TotalQty, clsCommon.myCstr(dtData.Rows(0)("Qty")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.UOM, clsCommon.myCstr(dtData.Rows(0)("UOM_Code")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfVLC, clsCommon.myCstr(dtData.Rows(0)("VLC_Count")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.NoOfRoute, clsCommon.myCstr(dtData.Rows(0)("Route_Count")))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderFat, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("FATPER"))))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderSNF, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("SNFPER"))))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderRate, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Rate"))))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderAmt, clsCommon.myFormat(clsCommon.myCdbl(dtData.Rows(0)("Amount"))))
    '            objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderShift, obj.SHIFT)

    '            objSMSH.SaveData(clsUserMgtCode.frmMilkShiftEndMCC, objSMSH, trans)
    '            objSMSH = Nothing
    '        End If
    '    End If
    'End Sub

    'Shared Sub CreateSMSContentVSP(ByVal obj As clsMilkShiftEndMCC, ByVal trans As SqlTransaction)
    '    Dim strSMSContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT SMS_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmMilkSample + "'", trans))
    '    If clsCommon.myLen(strSMSContent) > 0 Then
    '        Dim qry As String = " Select max(final.Date) as Date,max(final.Shift) as Shift " + Environment.NewLine + _
    '        " ,max(Mcc_Code_VLC_Uploader) as Mcc_Code_VLC_Uploader,max(final.MCC) as MCC, max(final.[MCC Name]) as [MCC Name]" + Environment.NewLine + _
    '        " ,max(final.[VLC Name] ) as [VLC Name], max(final.[VSP Code]) as VSP_Code,max(final.[Vlc Uploader Code]) as [Vlc Uploader Code],max(Phone1) as Phone1" + Environment.NewLine + _
    '        " ,max(final.UOM_Code) as UOM_Code" + Environment.NewLine + _
    '        " ,max (final.[Milk Type]) as [Milk Type]" + Environment.NewLine + _
    '        " ,sum(final.[Cow Milk Qty (KG)] ) as [Cow Milk Qty (KG)]" + Environment.NewLine + _
    '        " ,max(final.[Cow FAT(%)]) as [Cow FAT(%)]" + Environment.NewLine + _
    '        " ,sum(final.[FAT(KG)] * Case When  final.[FAT(%)] <= 5 Then 1 Else 0 End) as [Cow FAT (KG)]" + Environment.NewLine + _
    '        " ,max(final.[Cow SNF(%)]) as [Cow SNF(%)]" + Environment.NewLine + _
    '        " ,sum(final.[SNF(KG)] * Case When final.[FAT(%)]  <= 5 Then 1 Else 0 End) as [Cow SNF (KG)]" + Environment.NewLine + _
    '        " ,sum(final.[SRN Amount] * case when final.[Cow Milk Qty (KG)] > 0 then  1 else 0 end ) as [Cow Amount]" + Environment.NewLine + _
    '        " ,sum(final.[Buffalo Milk Qty (KG)] ) as [Buffalo Milk Qty (KG)]" + Environment.NewLine + _
    '        " ,max(final.[Buffalo FAT(%)]  ) as [Buffalo FAT(%)] " + Environment.NewLine + _
    '        " ,sum(final.[FAT(KG)] * Case When  final.[FAT(%)] > 5 Then 1 Else 0 End) as  [Buffalo FAT (KG)]" + Environment.NewLine + _
    '        " ,max(final.[Buffalo SNF(%)] ) as [Buffalo SNF(%)]" + Environment.NewLine + _
    '        " ,sum(final.[SNF(KG)] * Case When  final.[FAT(%)] > 5 Then 1 Else 0 End) as [Buffalo SNF (KG)]" + Environment.NewLine + _
    '        " ,cast(round(sum(final.[FAT(KG)] * Case When final.[FAT( % )] <= 5 Then 1 Else 0 End * 100)/sum(final.[Cow Milk Qty]),2,0) as float) as [cowfat],cast(round(sum(final.[SNF(KG)] * Case When final.[FAT( % )] <= 5 Then 1 Else 0 End * 100)/sum(final.[Cow Milk Qty]),2,0) as float) as [cowsnf],cast(round(sum(final.[FAT(KG)] * Case When final.[FAT( % )] > 5 Then 1 Else 0 End * 100)/sum(final.[Buffalo Milk Qty]),2,0) as float) as [buffalofat],cast(round(sum(final.[SNF(KG)] * Case When final.[FAT( % )] > 5 Then 1 Else 0 End * 100)/sum(final.[Buffalo Milk Qty]),2,0) as float) as [buffalosnf]" + Environment.NewLine + _
    '        " ,sum(final.[SRN Amount] * case when final.[Buffalo Milk Qty (KG)] > 0 then  1 else 0 end )  as [Buffalo Amount]" + Environment.NewLine + _
    '        " From (" + Environment.NewLine + _
    '        " Select  TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_VENDOR_MASTER.Phone1,TSPL_MILK_RECEIPT_DETAIL.UOM_Code,Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)],Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT Else 0 End [Cow Milk Qty (KG)],Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.Acc_Weight Else 0 End [Cow Milk Qty], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT Else 0 End [Buffalo Milk Qty (KG)],Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.Acc_Weight Else 0 End [Buffalo Milk Qty], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type],TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date],Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code],TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name],TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)],TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)],Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty],Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status] " + Environment.NewLine + _
    '        " From TSPL_MILK_RECEIPT_DETAIL" + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE" + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE" + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " + Environment.NewLine + _
    '        " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " + Environment.NewLine + _
    '        " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " + Environment.NewLine + _
    '        " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code" + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT " + Environment.NewLine + _
    '        " Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code" + Environment.NewLine + _
    '        " where TSPL_MILK_SRN_HEAD.MCC_CODE='" + obj.MCC_CODE + "' and convert(Date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=convert(date,'" + clsCommon.GetPrintDate(obj.DOC_DATE, "dd/MMM/yyyy") + "',103) and  TSPL_MILK_SRN_HEAD.SHIFT='" + obj.SHIFT + "'" + Environment.NewLine + _
    '        " and len(replace( isnull(substring(TSPL_VENDOR_MASTER.Phone1,6,10),''),'_',''))>0 " + Environment.NewLine + _
    '        " ) As final " + Environment.NewLine + _
    '        " group by [Vlc Uploader Code]  "
    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim objSMSH As New clsSMSHead()
    '                objSMSH.SMS_Text = strSMSContent
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderDate, clsCommon.GetPrintDate(clsCommon.myCDate(dr("Date")), "dd/MM/yyyy"))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VLCDataUploaderShift, obj.SHIFT)
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCCode, clsCommon.myCstr(dr("MCC")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCName, clsCommon.myCstr(dr("MCC Name")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.MCCUploaderCode, clsCommon.myCstr(dr("Mcc_Code_VLC_Uploader")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSPCode, clsCommon.myCstr(dr("VSP_Code")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSP, clsCommon.myCstr(dr("VLC Name")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.VSPUploaderCode, clsCommon.myCstr(dr("Vlc Uploader Code")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.UOM, clsCommon.myCstr(dr("UOM_Code")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CowQty, clsCommon.myCstr(dr("Cow Milk Qty (KG)")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CowFat, clsCommon.myCstr(dr("cowfat")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CowSNF, clsCommon.myCstr(dr("cowsnf")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.CowAmt, clsCommon.myCstr(dr("Cow Amount")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BuffaloQty, clsCommon.myCstr(dr("Buffalo Milk Qty (KG)")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BuffaloFat, clsCommon.myCstr(dr("buffalofat")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BuffaloSNF, clsCommon.myCstr(dr("buffalosnf")))
    '                objSMSH.SMS_Text = objSMSH.SMS_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.BuffaloAmt, clsCommon.myCstr(dr("Buffalo Amount")))

    '                objSMSH.arrMobilNo = New List(Of String)()
    '                objSMSH.arrMobilNo.Add(clsCommon.myCstr(dr("Phone1")))
    '                objSMSH.SaveData(clsUserMgtCode.frmMilkSample, objSMSH, trans)
    '                objSMSH = Nothing
    '            Next
    '        End If
    '    End If
    'End Sub

    

    Function isCheckNumberAndMailIdExistsForAllVSP(ByVal trans As SqlTransaction) As Boolean
        Dim is_Send_SMS As Boolean
        is_Send_SMS = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.Is_Send_Sms_ForVSP, clsFixedParameterCode.MilkSetting, trans)) = 0, False, True)
        If is_Send_SMS = True Then
            Dim qry As String = "Select max(final.[VSP Code]) as VSP_Code" & _
           " From (Select Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)]," & _
           " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)]," & _
           " Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], " & _
           " TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date, Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], " & _
           " Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], " & _
           " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], " & _
           " TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No], TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)]," & _
           " TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], " & _
           " Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty]," & _
           " Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no,convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date     From TSPL_MILK_RECEIPT_DETAIL " & _
           " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
           " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE " & _
           " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & _
           " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
           " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " & _
           " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code" & _
           " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)" & _
           " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE " & _
           " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code) As final where 2=2  and " & _
           " final.[Date] =convert(date,'" & clsCommon.myCDate(dtpDocDate.Value) & "',103) and MCC  IN ('" & clsCommon.myCstr(fndMcc.Value) & "') and  final.Shift ='" & IIf(clsCommon.myCstr(cboShift.SelectedValue) = "M", "Morning", "Evening") & "'  " & _
           " group by [Vlc Uploader Code] "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            Dim dtmail As DataTable = clsDBFuncationality.GetDataTable("select _Emailid from TSPL_MCC_MAIL_SMS_Setting where _name in (" & qry & ") and ISNull(_Emailid,'')<>'' ", trans)
            Dim dtNumber As DataTable = Nothing
            If IIf(clsFixedParameter.GetData(clsFixedParameterType.Is_Pick_No_from_Mail_Setting, clsFixedParameterCode.MilkSetting, trans) = 0, False, True) = True Then
                dtNumber = clsDBFuncationality.GetDataTable("select _MobileNo from TSPL_MCC_MAIL_SMS_Setting where _name in (" & qry & ") and  len(_MobileNo)>=10 ", trans)
            Else
                dtNumber = clsDBFuncationality.GetDataTable("Select Phone1 from TSPL_VENDOR_MASTER where Vendor_Code in (" & qry & ") and  len(phone1)>=10 ", trans)
            End If
            If dt.Rows.Count <> dtmail.Rows.Count Or dt.Rows.Count <> dtNumber.Rows.Count Then
                If clsCommon.MyMessageBoxShow("Mail Id and Number for all VSP's are not provided , Do you want to continue or not.", "Milk Shift End", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Function AllowToSave() As Boolean
        Try
            Dim strQryMilkReceiptCode As String = "(select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where MCC_CODE='" + fndMcc.Value + "' and convert(date, doc_date,103)='" + clsCommon.GetPrintDate(DtpMCCDate.Value, "dd/MMM/yyyy") + "' and shift='" + clsCommon.myCstr(cboShift.SelectedValue) + "')"
            ''Added By Balwinder Here so that MCC Date Never be come Wrong on 23/Oct/2017
            Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMcc.Value)
            If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
                Throw New Exception("No shift is opened. one Shift Must be Opened..")
            ElseIf DTShift.Rows.Count > 1 Then
                Throw New Exception("There are more then one shifts are opened.Only one Shift can be Opened..")
            Else
                cboShift.SelectedValue = clsCommon.myCstr(DTShift.Rows(0).Item("Shift"))
                dtpDocDate.Value = clsCommon.myCDate(DTShift.Rows(0).Item("MCC_Shift_date"))
                DtpMCCDate.Value = clsCommon.myCDate(DTShift.Rows(0).Item("MCC_Shift_date"))
            End If
            ''End of Added By Balwinder Here so that MCC Date Never be come Wrong

            ' KUNAL > TICKET : BM00000009575 ========
            If AllowFutureDateTransaction(dtpDocDate.Value, Nothing) = False Then
                dtpDocDate.Focus()
                Return False
            End If
            CalculateSNFFromCLR()
            Dim str_vlc As String = ""
            Dim str_Route As String = ""
            MinKmRange = clsFixedParameter.GetData(clsFixedParameterType.MCCMinKmRange, clsFixedParameterCode.MilkSetting, Nothing)
            If btnsave.Text = "Update" Then
                Dim strchk As String = "select Posted from TSPL_MILK_Shift_End_Head where DOC_COde='" + txtCode.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If
            Dim sQuery As String = "select * from TSPL_MILK_Receipt_detail where Doc_COde in " + strQryMilkReceiptCode + " and coalesce(is_sampleed,'F')='F'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            If dt.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow("All Receipt are not Sampled.Please do Samples First." & Environment.NewLine & _
                               " Do You want to Open Milk Receipt", "Validation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    sQuery = "select * from TSPL_PROGRAM_MASTER where Program_Code = 'M-RECEIPT'"
                    dt = clsDBFuncationality.GetDataTable(sQuery)
                    MDI.ShowForm(dt.Rows(0).Item("Program_Code"), dt.Rows(0).Item("Program_Name"), True)

                End If
                Return False
            End If


            sQuery = "select Posted from TSPL_MILK_Sample_HEAD where milk_receipt_COde in " + strQryMilkReceiptCode + "  and Posted='0'"
            dt = clsDBFuncationality.GetDataTable(sQuery)
            If dt.Rows.Count > 0 Then
                If clsCommon.MyMessageBoxShow("All Samples are not posted.Please Post Samples First." & Environment.NewLine & _
                               " Do You want to Open Milk Sample", "Validation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    sQuery = "select * from TSPL_PROGRAM_MASTER where Program_Code = 'M-SAMPLE'"
                    dt = clsDBFuncationality.GetDataTable(sQuery)
                    MDI.ShowForm(dt.Rows(0).Item("Program_Code"), dt.Rows(0).Item("Program_Name"), True)
                End If
                Return False
            End If

            ''ERO/25/02/19-000494 by balwinder on 27/02/2019
            Dim NoOfDock As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_DOCK_MASTER where MCC_Code='" + fndMcc.Value + "'"))
            sQuery = "select DOC_CODE from TSPL_MILK_RECEIPT_HEAD where Doc_COde in " + strQryMilkReceiptCode + ""
            dt = clsDBFuncationality.GetDataTable(sQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Milk Receipt found." + Environment.NewLine + "Please first do receipt and milk.After only you can end shift", Me.Text)
                Return False
            ElseIf NoOfDock > 0 Then
                If NoOfDock <> dt.Rows.Count Then
                    clsCommon.MyMessageBoxShow(Me, "Milk Receipt not found for All Dock.", Me.Text)
                    Return False
                End If
            End If
            sQuery = "select DOC_CODE from TSPL_MILK_Sample_HEAD where milk_receipt_COde in " + strQryMilkReceiptCode + " "
            dt = clsDBFuncationality.GetDataTable(sQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("No Milk Sample found." + Environment.NewLine + "Please first do milk sample.After only you can end shift", Me.Text)
                Return False
            ElseIf NoOfDock > 0 Then
                If NoOfDock <> dt.Rows.Count Then
                    clsCommon.MyMessageBoxShow(Me, "Milk Sample not found for All Dock.", Me.Text)
                    Return False
                End If
            End If
            ''End of ERO/25/02/19-000494  


            If clsCommon.myLen(Me.cboShift.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter Shift", Me.Text)
                Return False
            End If

            If clsCommon.myLen(Me.fndMccCode.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Enter MCC", Me.Text)
                fndMccCode.Focus()
                Return False
            End If

            If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) <= 0 Then
                RadPageView2.SelectedPage = Pg_StockDetail
                TxtManualStock.Focus()
                Throw New Exception("Please fill Closing Stock")
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtManualFat_Per.Text)) <= 0 Then
                RadPageView2.SelectedPage = Pg_StockDetail
                TxtManualFat_Per.Focus()
                Throw New Exception("Please fill Closing FAT(%)")
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtManualFAT.Text)) <= 0 Then
                RadPageView2.SelectedPage = Pg_StockDetail
                TxtManualFAT.Focus()
                Throw New Exception("Please fill Closing FAT")
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtManualSNF_Per.Text)) <= 0 Then
                RadPageView2.SelectedPage = Pg_StockDetail
                TxtManualSNF_Per.Focus()
                Throw New Exception("Please fill Closing SNF)(%)")
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtManualSNF.Text)) <= 0 Then
                RadPageView2.SelectedPage = Pg_StockDetail
                TxtManualSNF.Focus()
                Throw New Exception("Please fill Closing SNF")
            End If

            For Each grow As GridViewRowInfo In GvRoute.Rows
                If StdInterfaceSett = False Then
                    If clsCommon.myCdbl(grow.Cells(col_Opening_KM).Value) > clsCommon.myCdbl(grow.Cells(col_Closing_KM).Value) Then
                        clsCommon.MyMessageBoxShow("Opening KM should be Smaller than Closing KM of Route - [" & clsCommon.myCstr(grow.Cells(colRoute_Name).Value) & "].", "Validation")
                        GvRoute.CurrentRow = GvRoute.Rows(grow.Index)
                        GvRoute.CurrentColumn = GvRoute.Columns(col_Opening_KM)
                        RadPageView2.SelectedPage = RadPageViewPage2
                        Return False
                    End If
                End If

                sQuery = "select Posted from TSPL_MILK_Truck_Sheet_HEAD inner join tspl_Mcc_Master on tspl_Mcc_Master.mcc_code= " _
                & " TSPL_MILK_Truck_Sheet_HEAD.mcc_Code and coalesce(Is_truck_Sheet_Mandatory,1)=1 where milk_receipt_COde in " + strQryMilkReceiptCode + " " _
                & " and route_Code='" & clsCommon.myCstr(grow.Cells(colRoute_Code).Value) & "'"
                dt = clsDBFuncationality.GetDataTable(sQuery)
                If dt.Rows.Count <= 0 Then
                    Dim is_truck_sheet_mandatory = clsDBFuncationality.getSingleValue("select coalesce(Is_truck_Sheet_Mandatory,1) from tspl_Mcc_Master where Mcc_Code='" & fndMcc.Value & "'")
                    If is_truck_sheet_mandatory > 0 Then
                        If clsCommon.MyMessageBoxShow("Truck Sheet is not Created.Please Create Truck Sheet First." & Environment.NewLine & _
                                                       " Do You want to Create Truck Sheet", "Validation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                            sQuery = "select * from TSPL_PROGRAM_MASTER where Program_Code = 'MCC-TRUK-SHT'"
                            dt = clsDBFuncationality.GetDataTable(sQuery)
                            MDI.ShowForm(dt.Rows(0).Item("Program_Code"), dt.Rows(0).Item("Program_Name"), True)
                        End If
                        Return False
                    End If
                End If
                If StdInterfaceSett = False Then
                    If clsCommon.myCdbl(grow.Cells(col_Opening_KM).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Fill Opening KM. of Route - [" & clsCommon.myCstr(grow.Cells(colRoute_Name).Value) & "].", "Validation")
                        GvRoute.CurrentRow = GvRoute.Rows(grow.Index)
                        GvRoute.CurrentColumn = GvRoute.Columns(col_Opening_KM)
                        RadPageView2.SelectedPage = RadPageViewPage2
                        Return False
                    End If
                    If clsCommon.myCdbl(grow.Cells(col_Closing_KM).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Fill Closing KM. of Route - [" & clsCommon.myCstr(grow.Cells(colRoute_Name).Value) & "].", "Validation")
                        GvRoute.CurrentRow = GvRoute.Rows(grow.Index)
                        GvRoute.CurrentColumn = GvRoute.Columns(col_Closing_KM)
                        RadPageView2.SelectedPage = RadPageViewPage2
                        Return False
                    End If
                    If clsCommon.myCdbl(grow.Cells(col_Difference).Value) > MinKmRange And clsCommon.myLen(grow.Cells(col_reason).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Fill Reason in Route Grid For more then " & MinKmRange & " KM. Rows.", "Validation")
                        GvRoute.CurrentRow = GvRoute.Rows(grow.Index)
                        GvRoute.CurrentColumn = GvRoute.Columns(col_reason)
                        RadPageView2.SelectedPage = RadPageViewPage2
                        Return False
                    End If
                End If

                If clsCommon.myCdbl(grow.Cells(col_Deduction_of_transporter).Value) > 0 And clsCommon.myLen(grow.Cells(col_Reason_Deduction).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill [Deduction Reason] in Route Grid For Deduction Rows.", "Validation")
                    GvRoute.CurrentRow = GvRoute.Rows(grow.Index)
                    GvRoute.CurrentColumn = GvRoute.Columns(col_Reason_Deduction)
                    RadPageView2.SelectedPage = RadPageViewPage2
                    Return False
                End If
                If clsCommon.myCdbl(grow.Cells(col_Deduction_of_transporter).Value) <= 0 And clsCommon.myLen(grow.Cells(col_Reason_Deduction).Value) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Fill [Deduction] in Route Grid For Deduction Reason is Filled.", "Validation")
                    GvRoute.CurrentRow = GvRoute.Rows(grow.Index)
                    GvRoute.CurrentColumn = GvRoute.Columns(col_Deduction_of_transporter)
                    RadPageView2.SelectedPage = RadPageViewPage2
                    Return False
                End If
                sQuery = "select count(*) from TSPL_VLC_MASTER_HEAD where Route_Code='" & clsCommon.myCstr(grow.Cells(colRoute_Code).Value) & "'"
                Dim vlc_count As Integer = clsDBFuncationality.getSingleValue(sQuery)
                Dim grid_vlc_Count As Integer = 0
                Dim arr_vlc As New ArrayList
                For Each row As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCstr(row.Cells(colRoute_Code).Value) = clsCommon.myCstr(grow.Cells(colRoute_Code).Value) And Not arr_vlc.Contains(clsCommon.myCstr(row.Cells(colVlc_Code).Value)) Then
                        grid_vlc_Count += 1
                        arr_vlc.Add(clsCommon.myCstr(row.Cells(colVlc_Code).Value))
                    End If
                    If clsCommon.myCdbl(row.Cells(colDeduction_of_VSP).Value) > 0 And clsCommon.myLen(row.Cells(col_reason).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Fill [Deduction Reason] in Documents and Deduction Grid For Deduction Rows.", "Validation")
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(col_reason)
                        RadPageView2.SelectedPage = RadPageViewPage3
                        Return False
                    End If
                    If clsCommon.myCdbl(row.Cells(colDeduction_of_VSP).Value) <= 0 And clsCommon.myLen(row.Cells(col_reason).Value) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Fill [Deduction] in Documents and Deduction Grid For Deduction Rows.", "Validation")
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colDeduction_of_VSP)
                        RadPageView2.SelectedPage = RadPageViewPage3
                        Return False
                    End If
                Next
                If StdInterfaceSett = False Then
                    If grid_vlc_Count <> vlc_count And clsCommon.myLen(grow.Cells(col_reason).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Fill Reason in Route Grid for Route Code [" & clsCommon.myCstr(grow.Cells(colRoute_Code).Value) & "]. " & Environment.NewLine _
                                                   & " It has [" & vlc_count & "] .but you are  entering only [" & grid_vlc_Count & "] in this Shift.", "Validation")
                        GvRoute.CurrentRow = GvRoute.Rows(grow.Index)
                        GvRoute.CurrentColumn = GvRoute.Columns(col_reason)
                        RadPageView1.SelectedPage = RadPageViewPage1
                        Return False
                    End If
                End If

            Next
            sQuery = "select count(*) from TSPL_VLC_DATA_UPLOADER where convert(date,File_date,103)=convert(date,'" & DtpMCCDate.Value & "',103) and Shift='" + UCase(cboShift.Text) + "' and Mcc_Code='" + clsCommon.myCstr(fndMccCode.Tag) + "' "
            Dim Data_Count As Integer = clsDBFuncationality.getSingleValue(sQuery)
            If clsCommon.myCdbl(Data_Count) <= 0 Then
                'If clsCommon.MyMessageBoxShow("MP Data is not uploaded of VLC : [" & clsCommon.myCstr(row.Cells(colVlc_Code).Value) & "] and Route [" & clsCommon.myCstr(row.Cells(colRoute_Code).Value) & "]." & Environment.NewLine & _
                '" Do you want to upload data for this VLC and Route", "Validation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                '    sQuery = "select * from TSPL_PROGRAM_MASTER where Program_Code = 'VLC-D-UPL-MA'"
                '    dt = clsDBFuncationality.GetDataTable(sQuery)
                '    MDI.ShowForm(dt.Rows(0).Item("Program_Code"), dt.Rows(0).Item("Program_Name"))
                '    Return False
                'End If
                'Return False
                'str_vlc = IIf(str_vlc = "", clsCommon.myCstr(rw.Cells(colVlc_Code).Value), str_vlc & " , " & clsCommon.myCstr(row.Cells(colVlc_Code).Value))
                'str_Route = IIf(str_Route = "", clsCommon.myCstr(row.Cells(colRoute_Code).Value), str_Route & " , " & clsCommon.myCstr(row.Cells(colRoute_Code).Value))
                If clsCommon.MyMessageBoxShow("MP Data is not uploaded ." & Environment.NewLine & _
               " Do you want to upload data ", "Validation", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    sQuery = "select * from TSPL_PROGRAM_MASTER where Program_Code = 'VLC-D-UPL'"
                    dt = clsDBFuncationality.GetDataTable(sQuery)
                    MDI.ShowForm(dt.Rows(0).Item("Program_Code"), dt.Rows(0).Item("Program_Name"), True)
                    Return False
                End If
                'Return False
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

            ''RICHA BHA/25/10/18-000640 25 OCT,2018
            If chkAskSiloatShiftEnd.Checked = True Then
                If clsCommon.myLen(fndSiloInLoc.Value) <= 0 Then
                    fndSiloInLoc.Focus()
                    Throw New Exception("Please fill Silo " + fndSiloInLoc.Value)
                End If
            End If

            UcCustomFields1.AllowToSave()
            '=======================================================
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub AddNew()
        ' isNewEntry = True
        '' get value of standard setting
        '' done by Panch Raj against ticket No: BM00000009814
        StdInterfaceSett = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StandardInterfaceForMilkShiftEnd, clsFixedParameterCode.StandardInterfaceForMilkShiftEnd, Nothing)) = 0, False, True)
        fndMcc.Enabled = True
        txtCode.Value = ""
        dtpDocDate.MinDate = "01-Jan-0001"
        DtpTime.Value = clsCommon.GetPrintDate(DateTime.Now, "dd-MMM-yyyy hh:mm:ss")
        btnsave.Text = "Save"
        btnsave.Enabled = True
        is_Entered_Manually = False
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        GvRoute.Rows.Clear()
        GvRoute.Columns.Clear()
        GvRoute.DataSource = Nothing

        Me.cboShift.SelectedIndex = -1
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()

        Me.fndMcc.Value = ""
        Me.fndMccCode.Text = ""
        'Me.fndMcc.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join tspl_mcc_master on mcc_code=Default_Location  where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        'Me.fndMccCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_name from TSPL_MCC_MASTER where MCC_Code='" + clsCommon.myCstr(fndMcc.Value) + "' "))
        '  Me.fndVLCCode.Value = Nothing
        ' Me.fndVSPCode.Value = Nothing
        'Me.fndRouteCOde.Text = Nothing
        'txtOpening.Text = Nothing

        'lblRouteDesc.Text = Nothing

        ''lblVSPDesc.Text = Nothing
        ''lblVLCDesc.Text = Nothing

        'Me.txtOpening.Text = 0
        UsLock1.Status = ERPTransactionStatus.Pending
        'lblVLCCode.Tag = Nothing
        cboShift.Enabled = True
        cboShift.Enabled = True


        btnsave.Enabled = True
        btndelete.Enabled = True

        'For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
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
        ''End of For Custom Fields
        'objSerial.SetPortNameValues(cboComPort)
        DtStock = ClsOpenMCCShift.Getstock(dtpDocDate.Value, fndMcc.Value)
        If DtStock.Rows.Count > 0 Then
            TxtActualStock.Text = DtStock.Rows(0).Item("Qty")
            TxtActualFat.Text = DtStock.Rows(0).Item("FAT")
            TxtActualSNF.Text = DtStock.Rows(0).Item("SNF")
            'TxtBookFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / IIf(clsCommon.myCdbl(TxtActualStock.Text) > 0, clsCommon.myCdbl(TxtActualStock.Text), 1), 2)
            'TxtBookSNF_per.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / IIf(clsCommon.myCdbl(TxtActualStock.Text) > 0, clsCommon.myCdbl(TxtActualStock.Text), 1), 2)
            TxtBookFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            TxtBookSNF_per.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
        End If

        LblFirstWeightmentSample.Text = ""
        LblLastSampleFatTime.Text = ""
        LblShiftOpeningTime.Text = ""

        ''RICHA BHA/25/10/18-000640 25 OCT,2018
        chkAskSiloatShiftEnd.Checked = False
        fndAutoInLoc.Value = ""
        fndSiloInLoc.Value = ""
        txtSiloInLoc.Text = ""
        chkAskSiloatShiftEnd.Visible = False
        fndAutoInLoc.Visible = False
        MyLabel70.Visible = False
        fndSiloInLoc.Visible = False
        txtSiloInLoc.Visible = False
        lblSiloInLocation.Visible = False
        ''-----------------------
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
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

    Sub LoadDatainControls(ByVal vlccode As String)
        txtCode.Value = ""
        is_Entered_Manually = False

        Me.cboShift.SelectedIndex = -1
        Me.fndMcc.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER inner join tspl_mcc_master on mcc_code=Default_Location  where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        Me.fndMccCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_name from TSPL_MCC_MASTER where MCC_Code='" + clsCommon.myCstr(fndMcc.Value) + "' "))
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        'Me.fndVLCCode.Value = Nothing
        'Me.fndVSPCode.Value = Nothing
        'Me.fndRouteCOde.Text = Nothing
        'txtOpening.Text = Nothing

        'lblRouteDesc.Text = Nothing

        ''lblVSPDesc.Text = Nothing
        ''lblVLCDesc.Text = Nothing

        'Me.txtOpening.Text = 0
        'For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        ''End of For Custom Fields

    End Sub

#End Region
#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
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
                If (clsMilkShiftEndMCC.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Sub Set_Opening()
        Try
            Dim sQuery As String = "select TSPL_MILK_Shift_End_HEAD.MCC_CODE,TSPL_MILK_Shift_End_HEAD.SHIFT,convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_Shift_End_Route_DETAIL.* " _
       & " from TSPL_MILK_Shift_End_Route_DETAIL inner join TSPL_MILK_Shift_End_Head  on TSPL_MILK_Shift_End_Route_DETAIL  .DOC_CODE=TSPL_MILK_Shift_End_HEAD.DOC_CODE " _
       & " order by CONVERT(date,TSPL_MILK_Shift_End_HEAD.doc_date,103) desc,shift"
            DtMilkShiftEnd = clsDBFuncationality.GetDataTable(sQuery)
            For Each row As GridViewRowInfo In GvRoute.Rows
                Dim dr() As DataRow = DtMilkShiftEnd.Select("Route_Code='" & row.Cells(colRoute_Code).Value & "' and doc_date<='" & DtpMCCDate.Value & "'", "doc_date desc,shift")
                If dr.Length > 0 Then
                    row.Cells(col_Opening_KM).Value = clsCommon.myCdbl(dr(0)("Closing_KM"))
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso GvRoute.CurrentCell IsNot Nothing And RadPageView2.SelectedPage Is RadPageViewPage2 Then
            isInsideLoadData = True
            If GvRoute.CurrentColumn Is GvRoute.Columns(col_reason) Or GvRoute.CurrentColumn Is GvRoute.Columns(col_Reason_Deduction) Then
                GvRoute.CurrentRow.Cells(GvRoute.CurrentColumn.Index).Value = OpenReasonList(True) 'GvRoute.Columns(col_reason)
                GvRoute.CurrentColumn = GvRoute.Columns(col_ActualWeight)
            End If
            isInsideLoadData = False
        ElseIf e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            isInsideLoadData = True
            If gv1.CurrentColumn Is gv1.Columns(col_reason) Or gv1.CurrentColumn Is gv1.Columns(col_Reason_Deduction) Then
                gv1.CurrentRow.Cells(gv1.CurrentColumn.Index).Value = OpenReasonList(True) 'GvRoute.Columns(col_reason)
                gv1.CurrentColumn = gv1.Columns(colAorC)
            End If
            isInsideLoadData = False
        ElseIf e.KeyCode = Keys.H Then
            'Dim objsms As New FrmMccSMSSetting
            'objsms.SendMail(txtCode.Value, dtpDocDate.Value.ToString, fndMcc.Value, Form_ID, cboShift.Text)
            For Each row As GridViewRowInfo In GvRoute.Rows()
                row.Cells(col_Opening_KM).Value = 10
                row.Cells(col_Closing_KM).Value = 100
                row.Cells(col_Actual_Payable_KM).Value = 90
            Next
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
#End Region

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal ArrMilkReceiptcode As List(Of String) = Nothing, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            Dim sQuery As String = "select TSPL_MILK_SRN_DETAIL.ROUTE_CODE,charge_amount,Head_Load_amount,Own_Asset_Amount,Std_Qty from  (select  ROUTE_CODE," _
                & " sum(Head_load_amount) as Head_load_amount,sum(Own_Asset_Amount) as Own_Asset_Amount,sum(isnull(Std_Qty,0)) as Std_Qty from TSPL_MILK_SRN_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
                & " and TSPL_MILK_SRN_HEAD.MCC_CODE='" & fndMcc.Value & "' and convert(date,TSPL_MILK_SRN_Head.DOC_DATE,103)='" & clsCommon.GetPrintDate(DtpMCCDate.Value.Date, "dd-MMM-yyyy") & "' and TSPL_MILK_SRN_Head.SHIFT='" & cboShift.SelectedValue & "' " _
                & " group by ROUTE_CODE) TSPL_MILK_SRN_DETAIL  left join (select  ROUTE_CODE," _
                & " sum(Charge_Amount) as Charge_Amount from TSPL_MILK_SRN_Price_Charge_Detail inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=" _
                & " TSPL_MILK_SRN_Price_Charge_Detail.DOC_CODE and TSPL_MILK_SRN_HEAD.MCC_CODE='" & fndMcc.Value & "'  and convert(date,TSPL_MILK_SRN_Head.DOC_DATE,103)='" & clsCommon.GetPrintDate(DtpMCCDate.Value.Date, "dd-MMM-yyyy") & "'  and TSPL_MILK_SRN_Head.SHIFT='" & cboShift.SelectedValue & "' " _
                & " group by ROUTE_CODE) TSPL_MILK_SRN_Price_Charge_Detail on TSPL_MILK_SRN_Price_Charge_Detail.ROUTE_CODE=" _
                & " TSPL_MILK_SRN_DETAIL.ROUTE_CODE "
            DtCharge_detail = clsDBFuncationality.GetDataTable(sQuery)
            AddNew()
            LoadBlankGrid()
            LoadBlankRouteGrid()
            btnsave.Enabled = True
            GetshiftType()
            btnsave.Text = "Save"
            btnsave.Enabled = True
            isInsideLoadData = True
            If ArrMilkReceiptcode Is Nothing OrElse ArrMilkReceiptcode.Count <= 0 Then
                Dim obj As clsMilkShiftEndMCC = clsMilkShiftEndMCC.GetData(strDoc, navType)
                fndMcc.Enabled = False
                fndMcc.Value = obj.MCC_CODE
                fndMccCode.Text = obj.MCC_NAME
                fndMccCode.Tag = obj.MCC_NAME

                cboShift.SelectedValue = obj.SHIFT
                dtpDocDate.Value = obj.DOC_DATE
                DtpMCCDate.Value = obj.MCC_DATE
                TxtActualStock.Text = obj.Actual_Stock

                TxtManualStock.Text = obj.Manual_Stock
                TxtManualFAT.Text = obj.Manual_FAT
                TxtManualSNF.Text = obj.Manual_SNF
                TxtActualFat.Text = obj.Actual_FAT
                TxtActualSNF.Text = obj.Actual_SNF

                TxtManualFat_Per.Text = obj.Manual_FAT_Per
                TxtManualSNF_Per.Text = obj.Manual_SNF_Per
                TxtBookFat_Per.Text = obj.Actual_FAT_Per
                TxtBookSNF_per.Text = obj.Actual_SNF_Per
                ''RICHA BHA/25/10/18-000640 25 OCT,2018
                chkAskSiloatShiftEnd.Checked = obj.AskSiloatShiftEnd
                fndAutoInLoc.Value = obj.AutoIn_Location
                fndSiloInLoc.Value = obj.SILOIn_Location
                If clsCommon.myLen(fndSiloInLoc.Value) > 0 Then
                    txtSiloInLoc.Text = clsLocation.GetName(fndSiloInLoc.Value, Nothing)
                End If
                ''------------------
                UsLock1.Status = obj.POSTED
                txtCLR.Value = obj.CLR
                If obj.POSTED = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                End If
                gv1.Rows.Clear()
                If (clsMilkShiftEndMCC.ObjList IsNot Nothing AndAlso clsMilkShiftEndMCC.ObjList.Count > 0) Then
                    For Each obj1 As clsMilkShiftEndMCCDetail In clsMilkShiftEndMCC.ObjList
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAorC).Value = obj1.Amount_or_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRejectedorAcccepted).Value = obj1.Rejected_or_Acccepted
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction_of_VSP).Value = IIf(obj1.deduction_of_VSP > 0, obj1.deduction_of_VSP, "")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicle_Code).Value = obj1.VEHICLE_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(col_Remarks).Value = obj1.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVSP_Code).Value = obj1.VSP_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVlc_Code).Value = obj1.VLC_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colvlc_Doc_Code).Value = obj1.VLC_DOC_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_Procurement_Data_MP).Value = obj1.vlc_procurement_mp_data
                        gv1.Rows(gv1.Rows.Count - 1).Cells(col_Attachment_ID).Value = obj1.Attachment_Id
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRoute_Code).Value = obj1.Route_Code
                        isInsideLoadData = True
                        gv1.Rows(gv1.Rows.Count - 1).Cells(col_reason).Value = obj1.Deduction_Reason
                        isInsideLoadData = False
                        gv1.Rows(gv1.Rows.Count - 1).Cells(col_Attachment).Value = "Add Attachment"
                        gv1.Rows(gv1.Rows.Count - 1).Cells(col_Show_Attachment).Value = "Show Attachment"
                    Next
                Else
                    gv1.Rows.AddNew()
                End If
                '=========================Route detail===========================
                GvRoute.Rows.Clear()
                If (clsMilkShiftEndMCC.ObjList_Route IsNot Nothing AndAlso clsMilkShiftEndMCC.ObjList_Route.Count > 0) Then
                    For Each obj1 As clsMilkShiftEndMCC_Route_Detail In clsMilkShiftEndMCC.ObjList_Route
                        GvRoute.Rows.AddNew()
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Deduction_of_transporter).Value = obj1.deduction_of_Transporter
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_MilkWeight).Value = obj1.Milk_Weight
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_ActualWeight).Value = obj1.Actual_Weight
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(colRoute_Code).Value = obj1.Route_CODE
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(colRoute_Name).Value = obj1.Route_Name

                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Opening_KM).Value = obj1.Opening_KM
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Closing_KM).Value = obj1.Closing_KM
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Total_KM).Value = obj1.Total_KM
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Actual_KM).Value = obj1.Actual_KM
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Actual_Payable_KM).Value = obj1.Actual_Payable_KM
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Status).Value = obj1.Status
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Difference).Value = obj1.Diff_Km
                        isInsideLoadData = True
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_reason).Value = obj1.Reason
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Reason_Deduction).Value = obj1.Deduction_Reason
                        isInsideLoadData = False
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Attachment_ID).Value = obj1.Attachment_Id
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_truck_Sheet_of_Transporter).Value = obj1.Truck_Sheet_of_TransPorter
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Attachment).Value = "Add Attachment"
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Show_Attachment).Value = "Show Attachment"

                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Vehicle_Code).Value = obj1.Vehicle_Code
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Vehicle_Name).Value = obj1.Vehicle_Name
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(Col_Rate).Value = obj1.Rate_KM
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Amount).Value = obj1.Amount
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_StdQty).Value = obj1.Std_Qty
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Shift_Charge).Value = obj1.Shift_Charge
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Head_Load).Value = obj1.Is_Head_Load
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_charge_Amount).Value = obj1.Charge_Amt
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Head_load_Amount).Value = obj1.Head_Load_Amt
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_OwnAsset_Amount).Value = obj1.Own_Asset_Amount
                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Vehicle).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & clsCommon.myCstr(obj1.Vehicle_Code) & "'"))
                    Next
                Else
                    GvRoute.Rows.AddNew()
                End If
                '=========================================================================================
                txtCode.Value = obj.DOC_CODE
                btnsave.Text = "Update"
                btnsave.Enabled = False
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.BlankAllControls()
                    UcCustomFields1.LoadData(obj.DOC_CODE)
                End If
            Else
                gv1.Rows.Clear()
                GvRoute.Rows.Clear()
                For Each strMilkReceiptcode As String In ArrMilkReceiptcode
                    Dim obj As clsMilkReceiptMCC = clsMilkReceiptMCC.GetData(strMilkReceiptcode, NavigatorType.Current, False)
                    txtCode.Tag = obj.DOC_CODE
                    fndMcc.Value = obj.MCC_CODE
                    fndMccCode.Text = obj.MCC_NAME
                    fndMccCode.Tag = obj.MCC_NAME
                    dtpDocDate.Value = obj.DOC_DATE
                    cboShift.SelectedValue = obj.SHIFT
                    DtpMCCDate.Value = obj.DOC_DATE

                    If (clsMilkReceiptMCC.ObjList IsNot Nothing AndAlso clsMilkReceiptMCC.ObjList.Count > 0) Then
                        For ii As Integer = 0 To clsMilkReceiptMCC.ObjList.Count - 1
                            Dim isFound As Boolean = False
                            For jj As Integer = 0 To gv1.Rows.Count - 1
                                If clsCommon.CompairString(gv1.Rows(jj).Cells(colVlc_Code).Value, clsMilkReceiptMCC.ObjList(ii).VLC_CODE) = CompairStringResult.Equal Then
                                    isFound = True
                                    Exit For
                                End If
                            Next

                            If Not isFound Then
                                gv1.Rows.AddNew()
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colAorC).Value = ""
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colVehicle_Code).Value = clsMilkReceiptMCC.ObjList(ii).VEHICLE_CODE
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colVSP_Code).Value = clsMilkReceiptMCC.ObjList(ii).VSP_CODE
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colVlc_Code).Value = clsMilkReceiptMCC.ObjList(ii).VLC_CODE
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colvlc_Doc_Code).Value = clsMilkReceiptMCC.ObjList(ii).VLC_DOC_CODE
                                gv1.Rows(gv1.Rows.Count - 1).Cells(col_Attachment).Value = "Add Attachment"
                                gv1.Rows(gv1.Rows.Count - 1).Cells(col_Show_Attachment).Value = "Show Attachment"
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRoute_Code).Value = clsMilkReceiptMCC.ObjList(ii).ROUTE_CODE
                            End If
                        Next
                    Else
                        gv1.Rows.AddNew()
                    End If

                    '=========================Route detail===========================
                    Dim GraceMinute As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GraceTimeForTransporter, clsFixedParameterType.GraceTimeForTransporter, Nothing))
                    If (clsMilkReceiptMCC.ObjList_Route IsNot Nothing AndAlso clsMilkReceiptMCC.ObjList_Route.Count > 0) Then
                        For ii As Integer = 0 To clsMilkReceiptMCC.ObjList_Route.Count - 1
                            Dim isFound As Boolean = False
                            For jj As Integer = 0 To GvRoute.Rows.Count - 1
                                If clsCommon.CompairString(clsCommon.myCstr(GvRoute.Rows(jj).Cells(colRoute_Code).Value), clsMilkReceiptMCC.ObjList_Route(ii)) = CompairStringResult.Equal Then
                                    isFound = True
                                    Exit For
                                End If
                            Next
                            If Not isFound Then
                                GvRoute.Rows.AddNew()
                                sQuery = "select distinct rm.route_CODE,rm.ROUTE_NAME,rm.MCC_CODE,rm.KILOMETER,rm.Kilometer_Morning,rm.Kilometer_Evening,rd.VEHICLE_CODE,pvm.Description as Vehicle_Name," _
                                & " status,case when status='Day/Diesel' then Diesel_Rate/coalesce(Avg_km_Ltr,1) when status='Rate/K.M' then Price_KM when status='Rate/Ltr' then Price_Ltr_KG   " _
                                & " when status='Rental' then case when Rental_Type='Day' then RenTal_Amount/2 when Rental_Type='Month' then RenTal_Amount" _
                                & " /(2*" & Date.DaysInMonth(Today.Year, Today.Month) & ") when Rental_Type='Year' then RenTal_Amount/" _
                                & " (24*" & Date.DaysInMonth(Today.Year, Today.Month) & ") end end as Rate,case when status='Day/Diesel' then Shift_Charges else 0 " _
                                & " end as Shift_Charges,case when status='Rate/Ltr' then case when Rate_Type='KG' then ACC_Wgt when  Rate_Type='LTR' then " _
                                & " tt.Milk_Weight end else 0 end as Milk_Weight,ACC_Wgt as Actual_Wgt,tt.Milk_wg as Milk_wgt,coalesce(is_additional,'F') as " _
                                & " Is_Additional, pvm.Vendor_Code from tspl_mcc_route_master rm inner " _
                                & " join TSPL_MILK_RECEIPT_DETAIL rd on rm.Route_Code=rd.ROUTE_CODE inner join TSPL_Primary_Vehicle_Master pvm on pvm.Vehicle_Code=rd.VEHICLE_CODE " _
                                & " inner join (select SUM(ACC_Weight_LTR) as Milk_Weight,SUM(Acc_Weight) as ACC_Wgt,SUM(Milk_Weight) as Milk_Wg ,vehicle_code as vh from TSPL_MILK_RECEIPT_DETAIL where " _
                                & " TSPL_MILK_RECEIPT_DETAIL.doc_code='" & strMilkReceiptcode & "' group by VEHICLE_CODE) tt on tt.vh=rd.VEHICLE_CODE where rm.Route_Code='" & clsMilkReceiptMCC.ObjList_Route(ii) & "' and rd.doc_code='" & strMilkReceiptcode & "'"
                                Dim dtRoute As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                                '===========Charge Details and Head Load Amount=============
                                Dim dr() As DataRow = DtCharge_detail.Select("route_CODE='" & clsMilkReceiptMCC.ObjList_Route(ii) & "'")
                                '=================================================================
                                'sQuery = "select *,coalesce(Is_Additional,'F') as Is_Additional from tspl_slab_range_detail where Form_ID='PTV-MST'"
                                sQuery = "select tspl_slab_range_detail.*,coalesce(Is_Additional,'F') as Is_Additional from tspl_slab_range_detail inner join TSPL_Primary_Vehicle_Master on Vehicle_Code=Trans_ID order by slab_upto"
                                DtSlab = clsDBFuncationality.GetDataTable(sQuery)

                                ' gvRoute.Rows(gvRoute.Rows.Count - 1).Cells(col_Deduction_of_transporter).Value = obj1.Deduction_of_transporter
                                If dtRoute.Rows.Count > 0 Then
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Deduction_of_transporter).Value = 0
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(colRoute_Code).Value = clsMilkReceiptMCC.ObjList_Route(ii)
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(colRoute_Name).Value = clsCommon.myCstr(dtRoute.Rows(0).Item("Route_Name"))

                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Opening_KM).Value = 0
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Closing_KM).Value = 0
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Total_KM).Value = 0

                                    If settSeprateDistanceMorningEveningShift Then
                                        If clsCommon.CompairString(clsCommon.myCstr(cboShift.SelectedValue), "M") = CompairStringResult.Equal Then
                                            GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Actual_KM).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer_Morning"))
                                        Else
                                            GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Actual_KM).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer_Evening"))
                                        End If
                                    Else
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Actual_KM).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer"))
                                    End If


                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Status).Value = clsCommon.myCstr(dtRoute.Rows(0).Item("Status"))
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Difference).Value = 0
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Actual_Payable_KM).Value = 0

                                    isInsideLoadData = True
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_reason).Value = ""
                                    isInsideLoadData = False

                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Attachment_ID).Value = ""
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Attachment).Value = "Add Attachment"
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Show_Attachment).Value = "Show Attachment"

                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Vehicle_Code).Value = clsCommon.myCstr(dtRoute.Rows(0).Item("Vehicle_code"))
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Vehicle_Name).Value = clsCommon.myCstr(dtRoute.Rows(0).Item("Vehicle_Name"))
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Head_Load).Value = clsMilkShiftEndMCC.GetHeadLoadData(clsMilkReceiptMCC.ObjList_Route(ii), Nothing)
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Vehicle).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vehicle from TSPL_Primary_Vehicle_Master where vehicle_Code='" & clsCommon.myCstr(dtRoute.Rows(0).Item("Vehicle_code")) & "'"))
                                    '============Weight===================================
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_MilkWeight).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_wgt"))
                                    GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_ActualWeight).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Actual_wgt"))
                                    '=========================================================================
                                    If clsCommon.CompairString(GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Head_Load).Value, "False") = CompairStringResult.Equal Then
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(Col_Rate).Value = Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(dtRoute.Rows(0).Item("Rate"))), 2)
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Amount).Value = Math.Round((IIf(clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_weight")) > 0, clsCommon.myCdbl(dtRoute.Rows(0).Item("Rate")) * clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_Weight")), clsCommon.myCdbl(dtRoute.Rows(0).Item("Shift_Charges")) + clsCommon.myCdbl(dtRoute.Rows(0).Item("Rate")) * clsCommon.myCdbl(dtRoute.Rows(0).Item("Kilometer")))), 2)
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Shift_Charge).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Shift_Charges"))
                                    Else
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(Col_Rate).Value = 0
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Amount).Value = 0
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Shift_Charge).Value = 0
                                    End If

                                    If clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_weight")) > 0 Then
                                        GvRoute.Tag = dtRoute.Rows(0).Item("Milk_weight")
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Weight).Value = clsCommon.myCdbl(dtRoute.Rows(0).Item("Milk_weight"))
                                    End If
                                    If dr.Length > 0 Then
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_StdQty).Value = clsCommon.myCdbl(dr(0)("Std_Qty"))
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_charge_Amount).Value = clsCommon.myCdbl(dr(0)("Charge_amount"))
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Head_load_Amount).Value = clsCommon.myCdbl(dr(0)("Head_Load_amount"))
                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_OwnAsset_Amount).Value = clsCommon.myCdbl(dr(0)("Own_Asset_Amount"))
                                    End If

                                    If Not AllowManualEntryOfDeduction Then
                                        Dim qry As String = "select Entry_Date from TSPL_MILK_GATE_ENTRY_IN where MCC_CODE='" + fndMcc.Value + "' and Route_Code='" + clsMilkReceiptMCC.ObjList_Route(ii) + "' and  convert(date,Shift_Date,103)='" & clsCommon.GetPrintDate(DtpMCCDate.Value.Date, "dd-MMM-yyyy") & "'  and Entry_Shift='" + clsCommon.myCstr(cboShift.SelectedValue) + "'"
                                        Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry)
                                        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                            Dim dtActualEntryDate As DateTime = clsCommon.myCDate(dtTemp.Rows(0)(0))
                                            qry = "select " + IIf(clsCommon.CompairString(obj.SHIFT, "M") = CompairStringResult.Equal, "MCC_Reaching_Time_M", "MCC_Reaching_Time_E") + " as ReachingTime  from TSPL_MCC_ROUTE_MASTER where Route_Code='" + clsMilkReceiptMCC.ObjList_Route(ii) + "'"
                                            dtTemp = clsDBFuncationality.GetDataTable(qry)
                                            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                                If dtTemp.Rows(0)("ReachingTime") IsNot DBNull.Value Then
                                                    Dim dtRouteReachingTime As DateTime = clsCommon.myCDate(dtTemp.Rows(0)("ReachingTime"))
                                                    dtRouteReachingTime = New Date(dtActualEntryDate.Year, dtActualEntryDate.Month, dtActualEntryDate.Day, dtRouteReachingTime.Hour, dtRouteReachingTime.Minute, dtRouteReachingTime.Second).AddMinutes(GraceMinute)
                                                    If dtActualEntryDate > dtRouteReachingTime Then
                                                        Dim min As Integer = Math.Abs(DateDiff(DateInterval.Minute, dtActualEntryDate, dtRouteReachingTime))
                                                        qry = "select min(Amount) as Amount from TSPL_PTM_DEDCUTION_RANGE where PTM_Code='" + clsCommon.myCstr(dtRoute.Rows(0).Item("Vendor_Code")) + "' and  Minutes>=" + clsCommon.myCstr(min) + ""
                                                        GvRoute.Rows(GvRoute.Rows.Count - 1).Cells(col_Deduction_of_transporter).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Else
                        GvRoute.Rows.AddNew()
                    End If
                    Set_Opening()
                Next
            End If
            SetSampleDetails()
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "VLC No"
        repoCode.Name = colVlc_Code
        repoCode.Width = 120
        repoCode.IsVisible = True
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "VLC_DOC_Code"
        repoEmpCode.Name = colvlc_Doc_Code
        repoEmpCode.Width = 150
        ' repoEmpCode.IsVisible = False
        repoEmpCode.ReadOnly = True
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoEmpCode)


        Dim repoProjectCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectCode.FormatString = ""
        repoProjectCode.HeaderText = "VSP_Code"
        repoProjectCode.Name = colVSP_Code
        repoProjectCode.Width = 0
        repoProjectCode.ReadOnly = True
        repoProjectCode.IsVisible = False
        repoProjectCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectCode)

        Dim repoSelect As GridViewCommandColumn = New GridViewCommandColumn()
        repoSelect.FormatString = ""
        repoSelect.UseDefaultText = True
        repoSelect.DefaultText = "ADD"
        repoSelect.HeaderText = "ADD"
        repoSelect.Width = 70
        repoSelect.Name = col_Attachment
        'repoSelect.IsVisible = False
        repoSelect.FieldName = col_Attachment
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSelect)



        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Show"
        ShowBtn.Name = col_Show_Attachment
        ShowBtn.FieldName = col_Show_Attachment
        ShowBtn.Width = 70
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ShowBtn)


        Dim repoAttachment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachment.FormatString = ""
        repoAttachment.HeaderText = "VSP Procurement Data of MP"
        repoAttachment.Name = colVLC_Procurement_Data_MP
        repoAttachment.Width = 250
        repoAttachment.ReadOnly = True
        repoAttachment.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoAttachment)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = col_Remarks
        repoRemarks.Width = 300
        repoRemarks.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoRowType.FormatString = ""
        repoRowType.HeaderText = "Amount/Rate(%)"
        repoRowType.Name = colAorC
        repoRowType.Width = 100
        repoRowType.ReadOnly = False
        repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoRowType.DataSource = GetItemType()
        repoRowType.ValueMember = "Code"
        repoRowType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoRowType) '2

       



        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Shift Related Penalty/Deductions of VSP"
        repoCustCode.Name = colDeduction_of_VSP
        repoCustCode.Width = 250
        repoCustCode.ReadOnly = False
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoCustCode)

        'Dim repoCustDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoCustDesc.FormatString = ""
        'repoCustDesc.HeaderText = "Shift Related Penalty/Deduction of Transporter"
        'repoCustDesc.Name = col_Deduction_of_transporter
        'repoCustDesc.Width = 250
        'repoCustDesc.ReadOnly = True
        'gv1.MasterTemplate.Columns.Add(repoCustDesc)


        Dim repoReason As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoReason.FormatString = ""
        repoReason.HeaderText = "Deduction Reason"
        repoReason.Name = col_reason
        repoReason.Width = 300
        repoReason.ReadOnly = False
        repoReason.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoReason.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoReason)

        Dim repoDedType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoDedType.FormatString = ""
        repoDedType.HeaderText = "Status"
        repoDedType.Name = colRejectedorAcccepted
        repoDedType.ReadOnly = False
        repoDedType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDedType.DataSource = GetStatusType()
        repoDedType.ValueMember = "Code"
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "NATD") = CompairStringResult.Equal Then
            repoDedType.IsVisible = True
            repoDedType.Width = 100
        Else
            repoDedType.IsVisible = False
            repoDedType.Width = 0
        End If
        repoDedType.DisplayMember = "Name"
        gv1.MasterTemplate.Columns.Add(repoDedType) '2

        Dim repoVehicle As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVehicle.FormatString = ""
        repoVehicle.HeaderText = "Vehicle_Code"
        repoVehicle.Name = colVehicle_Code
        repoVehicle.Width = 0
        repoVehicle.ReadOnly = True
        repoVehicle.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoVehicle)


        Dim repoAttachment_Id As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachment_Id.FormatString = ""
        repoAttachment_Id.HeaderText = "Attachment_id"
        repoAttachment_Id.Name = col_Attachment_ID
        repoAttachment_Id.Width = 0
        repoAttachment_Id.ReadOnly = True
        repoAttachment_Id.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAttachment_Id)


        Dim repoRouteCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteCode.FormatString = ""
        repoRouteCode.HeaderText = "Route Code"
        repoRouteCode.Name = colRoute_Code
        repoRouteCode.Width = 0
        repoRouteCode.IsVisible = False
        repoRouteCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRouteCode)


        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
        gv1.AllowEditRow = True
        ReStoreGridLayout()
    End Sub

    Private Function GetItemType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Amount"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Rate(%)"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Function GetStatusType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Rejected"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "Accepted"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Special Approval"
        dt.Rows.Add(dr)

        Return dt
    End Function


    Sub LoadBlankRouteGrid()
        GvRoute.Rows.Clear()
        GvRoute.Columns.Clear()

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Route Code"
        repoCode.Name = colRoute_Code
        repoCode.Width = 150
        'repoCode.IsVisible = True
        repoCode.ReadOnly = True
        GvRoute.MasterTemplate.Columns.Add(repoCode)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Route Name"
        repoEmpCode.Name = colRoute_Name
        repoEmpCode.Width = 200
        ' repoEmpCode.IsVisible = False
        repoEmpCode.ReadOnly = True
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoEmpCode)

        Dim repoVehicleCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVehicleCode.FormatString = ""
        repoVehicleCode.HeaderText = "Vehicle Code"
        repoVehicleCode.Name = col_Vehicle_Code
        repoVehicleCode.Width = 150
        repoVehicleCode.ReadOnly = True
        repoVehicleCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoVehicleCode)

        Dim repoVehicleName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVehicleName.FormatString = ""
        repoVehicleName.HeaderText = "Vehicle Name"
        repoVehicleName.Name = col_Vehicle_Name
        repoVehicleName.Width = 150
        repoVehicleName.ReadOnly = True
        repoVehicleName.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoVehicleName)


        Dim repoSelect As GridViewCommandColumn = New GridViewCommandColumn()
        repoSelect.FormatString = ""
        repoSelect.UseDefaultText = True
        repoSelect.DefaultText = "ADD"
        repoSelect.HeaderText = "ADD"
        repoSelect.Width = 70
        repoSelect.Name = col_Attachment
        'repoSelect.IsVisible = False
        repoSelect.FieldName = col_Attachment
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        GvRoute.MasterTemplate.Columns.Add(repoSelect)



        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Show"
        ShowBtn.Name = col_Show_Attachment
        ShowBtn.FieldName = col_Show_Attachment
        ShowBtn.Width = 70
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        GvRoute.Columns.Add(ShowBtn)

        Dim repoAttachment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachment.FormatString = ""
        repoAttachment.HeaderText = "Truck Sheet of Transporter"
        repoAttachment.Name = col_truck_Sheet_of_Transporter
        repoAttachment.Width = 250
        repoAttachment.ReadOnly = True
        repoAttachment.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoAttachment)

        Dim repoStatus As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoStatus.FormatString = ""
        repoStatus.HeaderText = "Status"
        repoStatus.Name = col_Status
        repoStatus.Width = 0
        repoStatus.IsVisible = False
        repoStatus.ReadOnly = True
        repoStatus.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoStatus)

        If StdInterfaceSett = False Then

        End If
        Dim repoProjectCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoProjectCode.FormatString = ""
        repoProjectCode.HeaderText = "Opening KM."
        repoProjectCode.Name = col_Opening_KM
        repoProjectCode.Width = 80
        If StdInterfaceSett Then
            repoProjectCode.ReadOnly = True
            repoProjectCode.IsVisible = False
        End If       
        repoProjectCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoProjectCode)


        Dim repoClosingCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoClosingCode.FormatString = ""
        repoClosingCode.HeaderText = "Closing KM."
        repoClosingCode.Name = col_Closing_KM
        repoClosingCode.Width = 80
        If StdInterfaceSett Then
            repoClosingCode.ReadOnly = True
            repoClosingCode.IsVisible = False
        End If
        
        repoClosingCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoClosingCode)

        Dim repoTotalCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotalCode.FormatString = ""
        repoTotalCode.HeaderText = "Actual KM."
        repoTotalCode.Name = col_Total_KM
        repoTotalCode.Width = 100
        repoTotalCode.ReadOnly = True
        repoTotalCode.Minimum = 0
        If StdInterfaceSett Then
            repoTotalCode.ReadOnly = True
            repoTotalCode.IsVisible = False
        End If

        repoTotalCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoTotalCode)

        Dim repoPayableKM As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPayableKM.FormatString = ""
        repoPayableKM.HeaderText = "Actual Payable KM."
        repoPayableKM.Name = col_Actual_Payable_KM
        repoPayableKM.Width = 0
        repoPayableKM.Minimum = 0
        repoPayableKM.IsVisible = False
        repoPayableKM.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoPayableKM)

        Dim repoActualCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoActualCode.FormatString = ""
        repoActualCode.HeaderText = "Standard KM."
        repoActualCode.Name = col_Actual_KM
        repoActualCode.Width = 100
        repoActualCode.ReadOnly = True
        ' repoProjectCode.IsVisible = False
        repoActualCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoActualCode)

        Dim repoDiffCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiffCode.FormatString = ""
        repoDiffCode.HeaderText = "Difference KM."
        repoDiffCode.Name = col_Difference
        repoDiffCode.Width = 100
        repoDiffCode.ReadOnly = True
        If StdInterfaceSett Then
            repoDiffCode.IsVisible = False
        End If

        repoDiffCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoDiffCode)



        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Reason"
        repoRemarks.Name = col_reason
        repoRemarks.Width = 300
        repoRemarks.ReadOnly = False
        If StdInterfaceSett Then
            repoRemarks.ReadOnly = True
            repoRemarks.IsVisible = False
        End If
        repoRemarks.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoRemarks.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoRemarks)


        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Deduction of Transporter(Rs.)"
        repoCustCode.Name = col_Deduction_of_transporter
        repoCustCode.Width = 150
        repoCustCode.ReadOnly = Not AllowManualEntryOfDeduction
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoCustCode)


        Dim repoDedRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDedRemarks.FormatString = ""
        repoDedRemarks.HeaderText = "Deduction Reason"
        repoDedRemarks.Name = col_Reason_Deduction
        repoDedRemarks.Width = 300
        repoDedRemarks.ReadOnly = False
        repoDedRemarks.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoDedRemarks.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoDedRemarks)


        Dim repoAttachment_Id As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachment_Id.FormatString = ""
        repoAttachment_Id.HeaderText = "Attachment_id"
        repoAttachment_Id.Name = col_Attachment_Route_ID
        repoAttachment_Id.Width = 0
        repoAttachment_Id.ReadOnly = True
        repoAttachment_Id.IsVisible = False
        GvRoute.MasterTemplate.Columns.Add(repoAttachment_Id)

        Dim repoRate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = Col_Rate
        repoRate.Width = 0
        repoRate.IsVisible = False
        repoRate.ReadOnly = True
        repoRate.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoRate)

        Dim repoShift As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoShift.FormatString = ""
        repoShift.HeaderText = "Shift Charge"
        repoShift.Name = col_Shift_Charge
        repoShift.Width = 0
        repoShift.IsVisible = False
        repoShift.ReadOnly = True
        repoShift.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoShift)



        Dim repoAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = col_Amount
        repoAmount.Width = 0
        repoAmount.IsVisible = False
        repoAmount.ReadOnly = True
        repoAmount.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoAmount)

        repoAmount = New GridViewTextBoxColumn()
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Std Qty"
        repoAmount.Name = col_StdQty
        repoAmount.Width = 0
        repoAmount.IsVisible = False
        repoAmount.ReadOnly = True
        repoAmount.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoAmount)

        Dim repoWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoWeight.FormatString = ""
        repoWeight.HeaderText = "Milk Weight"
        repoWeight.Name = col_Weight
        repoWeight.Width = 0
        repoWeight.ReadOnly = True
        repoWeight.IsVisible = False
        repoWeight.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoWeight)

        Dim repoMilkWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMilkWeight.FormatString = ""
        repoMilkWeight.HeaderText = "Milk Wgt"
        repoMilkWeight.Name = col_MilkWeight
        repoMilkWeight.Width = 0
        repoMilkWeight.ReadOnly = True
        repoMilkWeight.IsVisible = False
        repoMilkWeight.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoMilkWeight)

        Dim repoActWeight As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoActWeight.FormatString = ""
        repoActWeight.HeaderText = "Actual Wgt"
        repoActWeight.Name = col_ActualWeight
        repoActWeight.Width = 0
        repoActWeight.ReadOnly = True
        repoActWeight.IsVisible = False
        repoActWeight.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoActWeight)

        Dim repoHeadLoad As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHeadLoad.FormatString = ""
        repoHeadLoad.HeaderText = "Is Head Load"
        repoHeadLoad.Name = col_Head_Load
        repoHeadLoad.Width = 0
        repoHeadLoad.ReadOnly = False
        repoHeadLoad.IsVisible = False
        GvRoute.MasterTemplate.Columns.Add(repoHeadLoad)

        Dim repoCharge As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCharge.FormatString = ""
        repoCharge.HeaderText = "Charge Amount"
        repoCharge.Name = col_charge_Amount
        repoCharge.Width = 0
        repoCharge.IsVisible = False
        repoCharge.ReadOnly = True
        repoCharge.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoCharge)

        Dim repoHeadLoadAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHeadLoadAmount.FormatString = ""
        repoHeadLoadAmount.HeaderText = "Head Load Amount"
        repoHeadLoadAmount.Name = col_Head_load_Amount
        repoHeadLoadAmount.Width = 0
        repoHeadLoadAmount.ReadOnly = True
        repoHeadLoadAmount.IsVisible = False
        repoHeadLoadAmount.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoHeadLoadAmount)

        Dim repoOwnAssetAmount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOwnAssetAmount.FormatString = ""
        repoOwnAssetAmount.HeaderText = "Own Asset Amount"
        repoOwnAssetAmount.Name = col_OwnAsset_Amount
        repoOwnAssetAmount.Width = 0
        repoOwnAssetAmount.ReadOnly = True
        repoOwnAssetAmount.IsVisible = False
        repoOwnAssetAmount.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoOwnAssetAmount)

        Dim repoVehicle As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVehicle.FormatString = ""
        repoVehicle.HeaderText = "Vehicle"
        repoVehicle.Name = col_Vehicle
        repoVehicle.Width = 150
        repoVehicle.ReadOnly = True
        repoVehicle.IsVisible = ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster
        repoVehicle.TextImageRelation = TextImageRelation.TextBeforeImage
        GvRoute.MasterTemplate.Columns.Add(repoVehicle)

        clsCustomFieldGrid.LoadBlankGrid(GvRoute, MyBase.ArrDetailFields)

        GvRoute.AllowDeleteRow = False
        GvRoute.AllowAddNewRow = False
        GvRoute.ShowGroupPanel = False
        GvRoute.AllowColumnReorder = True
        GvRoute.AllowRowReorder = False
        GvRoute.EnableSorting = True
        GvRoute.EnableFiltering = True
        GvRoute.EnableAlternatingRowColor = True
        GvRoute.AutoSizeRows = False
        GvRoute.AllowRowResize = True
        GvRoute.VerticalScrollState = ScrollState.AlwaysShow
        GvRoute.HorizontalScrollState = ScrollState.AlwaysShow
        GvRoute.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GvRoute.MasterTemplate.ShowRowHeaderColumn = False
        GvRoute.TableElement.TableHeaderHeight = 40
        GvRoute.ShowFilteringRow = True
        GvRoute.AllowEditRow = True
        ReStoreGridLayout()
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If e.Column Is gv1.Columns(colDeduction_of_VSP) Then
                If clsCommon.myLen(gv1.CurrentRow.Cells(colAorC).Value) <= 0 And clsCommon.myCdbl(gv1.CurrentRow.Cells(colDeduction_of_VSP).Value) > 0 Then
                    clsCommon.MyMessageBoxShow(Me, "Please select [Amount/Rate(%)] First.")
                    gv1.CurrentRow.Cells(colDeduction_of_VSP).Value = 0
                ElseIf clsCommon.myCstr(gv1.CurrentRow.Cells(colAorC).Value) = "R" And clsCommon.myCdbl(gv1.CurrentRow.Cells(colDeduction_of_VSP).Value) > 100 Then
                    clsCommon.MyMessageBoxShow(Me, "Rate can not be greater then 100.")
                    gv1.CurrentRow.Cells(colDeduction_of_VSP).Value = 0
                End If
            ElseIf Not isInsideLoadData And e.Column Is gv1.Columns(col_reason) Then
                isInsideLoadData = True
                gv1.CurrentRow.Cells(col_reason).Value = OpenReasonList(True)
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub


    Sub gv1_CommandCellClick(ByVal sender As Object, ByVal e As EventArgs) Handles gv1.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If gv1.CurrentColumn Is gv1.Columns(col_Show_Attachment) Then
                    Dim objAttachment As New ucAttachment
                    objAttachment.FunShow(gv1.CurrentRow.Cells(col_Attachment_ID).Value)
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub GvRoute_CellValidating(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles GvRoute.CellValidating
        Try
            If isInsideLoadData = False Then
                If e.Column Is GvRoute.Columns(col_Closing_KM) OrElse e.Column Is GvRoute.Columns(col_Opening_KM) Then
                    'If Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Opening_KM) Then
                    '    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Opening_KM).Value) > clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Closing_KM).Value) Then
                    '        GvRoute.CurrentRow.Cells(col_Opening_KM).Value = 0
                    '        Throw New Exception("Opening KM should me Smaller than Closing KM")
                    '    End If
                    'End If
                    'If Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Closing_KM) Then
                    '    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Opening_KM).Value) > clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Closing_KM).Value) Then
                    '        GvRoute.CurrentRow.Cells(col_Closing_KM).Value = 0
                    '        Throw New Exception("Closing KM should me Larger than Opening KM")
                    '    End If
                    'End If
                    GvRoute.CurrentRow.Cells(col_Total_KM).Value = clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Closing_KM).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Opening_KM).Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub GvRoute_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GvRoute.CellValueChanged
        Try
            If e.Column Is GvRoute.Columns(col_Opening_KM) Or e.Column Is GvRoute.Columns(col_Closing_KM) Or e.Column Is GvRoute.Columns(col_Deduction_of_transporter) Or e.Column Is GvRoute.Columns(col_Actual_Payable_KM) Then
                GvRoute.CurrentRow.Cells(col_Total_KM).Value = clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Closing_KM).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Opening_KM).Value)
                GvRoute.CurrentRow.Cells(col_Difference).Value = clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Total_KM).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value) 'clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_Payable_KM).Value) 
                If Not clsCommon.myCstr(GvRoute.CurrentRow.Cells(col_Status).Value).Contains("KG") And Not clsCommon.myCstr(GvRoute.CurrentRow.Cells(col_Status).Value).Contains("Ltr") Then
                    'GvRoute.CurrentRow.Cells(col_Amount).Value = Math.Round((clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Total_KM).Value) * clsCommon.myCdbl(GvRoute.CurrentRow.Cells(Col_Rate).Value)) + clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Shift_Charge).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Deduction_of_transporter).Value), 2)
                    GvRoute.CurrentRow.Cells(col_Amount).Value = Math.Round((clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value) * clsCommon.myCdbl(GvRoute.CurrentRow.Cells(Col_Rate).Value)) + clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Shift_Charge).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Deduction_of_transporter).Value), 2) 'IIf(clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_Payable_KM).Value) < clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value), clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_Payable_KM).Value), clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value)) * clsCommon.myCdbl(GvRoute.CurrentRow.Cells(Col_Rate).Value))
                    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Amount).Value) < 0 Then
                        GvRoute.CurrentRow.Cells(col_Amount).Value = 0
                    End If
                Else
                    GvRoute.CurrentRow.Cells(col_Amount).Value = Math.Round((clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Weight).Value) * clsCommon.myCdbl(GvRoute.CurrentRow.Cells(Col_Rate).Value)) + clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Shift_Charge).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Deduction_of_transporter).Value), 2)
                    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Amount).Value) < 0 Then
                        GvRoute.CurrentRow.Cells(col_Amount).Value = 0
                    End If
                End If
                'If Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Opening_KM) Then
                '    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Opening_KM).Value) > clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Closing_KM).Value) Then
                '        GvRoute.CurrentRow.Cells(col_Opening_KM).Value = 0
                '        Throw New Exception("Opening KM should me Smaller than Closing KM")
                '    End If
                'End If
                'If Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Closing_KM) Then
                '    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Opening_KM).Value) > clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Closing_KM).Value) Then
                '        GvRoute.CurrentRow.Cells(col_Closing_KM).Value = 0
                '        Throw New Exception("Closing KM should me Larger than Opening KM")
                '    End If
                'End If
                If Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Opening_KM) Then
                    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Opening_KM).Value) < 0 Then
                        GvRoute.CurrentRow.Cells(col_Opening_KM).Value = 0
                        Throw New Exception("Opening KM should be Greater then 0")
                    End If
                End If
                If Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Closing_KM) Then
                    If clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Closing_KM).Value) < 0 Then
                        GvRoute.CurrentRow.Cells(col_Closing_KM).Value = 0
                        Throw New Exception("Closing KM should be Greater then 0")
                    End If
                End If
                If clsCommon.myCstr(GvRoute.CurrentRow.Cells(col_Status).Value) = "KM_Range" Then
                    'Dim dr() As DataRow = DtSlab.Select("Slab_Upto >= " & GvRoute.CurrentRow.Cells(col_Total_KM).Value)
                    Dim dr() As DataRow = DtSlab.Select("Slab_Upto >= " & clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value)) 'IIf(clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_Payable_KM).Value) < clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value), clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_Payable_KM).Value), clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value))
                    If dr.Length > 0 Then
                        If clsCommon.CompairString(dr(0)("Is_Additional"), "F") = CompairStringResult.Equal Then
                            GvRoute.CurrentRow.Cells(Col_Rate).Value = clsCommon.myCdbl(dr(0)("Slab_rate"))
                            'GvRoute.CurrentRow.Cells(col_Amount).Value = Math.Round((clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Total_KM).Value) * clsCommon.myCdbl(dr(0)("Slab_Rate"))) + clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Shift_Charge).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Deduction_of_transporter).Value), 2)
                            GvRoute.CurrentRow.Cells(col_Amount).Value = Math.Round((clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value) * clsCommon.myCdbl(dr(0)("Slab_Rate"))) + clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Shift_Charge).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Deduction_of_transporter).Value), 2) 'IIf(clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_Payable_KM).Value) < clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value), clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_Payable_KM).Value), clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value)
                        Else
                            Dim totamt As Double = 0
                            Dim totkm As Double = 0
                            'For Each row As DataRow In DtSlab.Select("Slab_Upto < " & GvRoute.CurrentRow.Cells(col_Total_KM).Value)
                            For Each row As DataRow In DtSlab.Select("Slab_Upto < " & clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value))
                                totamt += Math.Round(clsCommon.myCdbl(clsCommon.myCdbl(row("Slab_Upto") - totkm) * clsCommon.myCdbl(row("Slab_Rate"))), 2)
                                totkm = clsCommon.myCdbl(clsCommon.myCdbl(row("Slab_Upto")))
                            Next
                            GvRoute.CurrentRow.Cells(Col_Rate).Value = 0 'clsCommon.myCdbl(dr(0)("Slab_rate"))
                            'GvRoute.CurrentRow.Cells(col_Amount).Value = totamt + Math.Round(((clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Total_KM).Value) - totkm) * clsCommon.myCdbl(dr(0)("Slab_Rate"))) + clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Shift_Charge).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Deduction_of_transporter).Value), 2)
                            GvRoute.CurrentRow.Cells(col_Amount).Value = totamt + Math.Round((clsCommon.myCdbl(clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Actual_KM).Value) - totkm) * clsCommon.myCdbl(dr(0)("Slab_Rate"))) + clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Shift_Charge).Value) - clsCommon.myCdbl(GvRoute.CurrentRow.Cells(col_Deduction_of_transporter).Value), 2)
                        End If
                    End If
                End If
            ElseIf Not isInsideLoadData And e.Column Is GvRoute.Columns(col_reason) Then
                isInsideLoadData = True
                GvRoute.CurrentRow.Cells(col_reason).Value = OpenReasonList(True)
                isInsideLoadData = False
            ElseIf Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Reason_Deduction) Then
                isInsideLoadData = True
                GvRoute.CurrentRow.Cells(col_Reason_Deduction).Value = OpenReasonList(True)
                isInsideLoadData = False
            ElseIf Not isInsideLoadData And e.Column Is GvRoute.Columns(col_Head_Load) Then
                isInsideLoadData = True
                If clsCommon.myCstr(GvRoute.CurrentRow.Cells(col_Head_Load).Value) = "1" Then
                    GvRoute.CurrentRow.Cells(col_Amount).Value = 0
                    GvRoute.CurrentRow.Cells(Col_Rate).Value = 0
                    GvRoute.CurrentRow.Cells(col_Shift_Charge).Value = 0
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub gvRoute_CommandCellClick(ByVal sender As Object, ByVal e As EventArgs) Handles GvRoute.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If GvRoute.CurrentColumn Is GvRoute.Columns(col_Show_Attachment) Then
                    Dim objAttachment As New ucAttachment
                    objAttachment.FunShow(GvRoute.CurrentRow.Cells(col_Attachment_ID).Value)
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub dtpDocDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDocDate.LostFocus
        Me.txtCode.Tag = clsMilkShiftEndMCC.GetDocCode(Me.dtpDocDate.Value, Me.fndMcc.Value, Me.cboShift.Text)
        SetSampleDetails()
    End Sub

    Public Sub LoadMilk_receipt_Code()
        Try
            Dim sQuery As String = "select * from tspl_Milk_Shift_End_Head where mcc_Code='" & fndMcc.Value & "' and convert(date,doc_date,103)='" & clsCommon.GetPrintDate(DtpMCCDate.Value, "dd-MMM-yyyy") & "' and shift='" & cboShift.SelectedValue & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
            If dt.Rows.Count <= 0 Then
                sQuery = "select Doc_Code from TSPL_MILK_RECEIPT_HEAD where mcc_Code='" & fndMcc.Value & "' and convert(date,doc_date,103)='" & clsCommon.GetPrintDate(DtpMCCDate.Value, "dd-MMM-yyyy") & "' and shift='" & cboShift.SelectedValue & "'"
                dt = clsDBFuncationality.GetDataTable(sQuery)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim arrMilkReceiptCode As New List(Of String)
                    For Each dr As DataRow In dt.Rows
                        arrMilkReceiptCode.Add(dr("Doc_Code"))
                    Next
                    LoadData("", arrMilkReceiptCode, NavigatorType.Current)
                End If
            Else
                LoadData(dt.Rows(0).Item("Doc_Code"), , NavigatorType.Current)
            End If
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Sub SetSampleDetails()
        LblFirstWeightmentSample.Text = clsMilkShiftEndMCC.GetFirstWeightmentSampleCode(Me.dtpDocDate.Value, Me.fndMcc.Value, Me.cboShift.SelectedValue)
        LblLastsampleFATTime.Text = clsMilkShiftEndMCC.GetLastSampleFATCode(Me.dtpDocDate.Value, Me.fndMcc.Value, Me.cboShift.SelectedValue)
        LblShiftOpeningTime.Text = clsMilkShiftEndMCC.GetShiftOpenTime(Me.dtpDocDate.Value, Me.fndMcc.Value, Me.cboShift.SelectedValue)
    End Sub

    Private Sub fndMCCCode_Validating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Me.txtCode.Tag = clsMilkShiftEndMCC.GetDocCode(Me.dtpDocDate.Value, Me.fndMcc.Value, Me.cboShift.Text)
        SetSampleDetails()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, , NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "SELECT DOC_CODE as Code,DOC_DATE as Date,MCC_CODE as [Mcc Code] FROM TSPL_MILK_Shift_End_HEAD"
            txtCode.Value = clsCommon.ShowSelectForm("MILK Shift End", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)


            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value)
                cboShift.Enabled = False
                ' gv1.Rows.Add(gv1.CurrentRow)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellClick
        Try
            If e.Column Is gv1.Columns(col_Attachment) Then
                openFileDialog1.ShowDialog()
                If openFileDialog1.FileName <> "" Then
                    gv1.CurrentRow.Cells(colVLC_Procurement_Data_MP).Value = openFileDialog1.FileName
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Function OpenReasonList(ByVal isButtonClick As Boolean)
        Dim sQuery As String = "select Code,Name from tspl_mcc_reason_master "
        sQuery = clsCommon.ShowSelectForm("Reason_List", sQuery, "Name", " Posted='1' ", "", "Code", isButtonClick)
        Return sQuery
    End Function


    Private Sub gvRoute_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles GvRoute.CellClick
        Try
            If e.Column Is GvRoute.Columns(col_Attachment) Then
                openFileDialog1.ShowDialog()
                If openFileDialog1.FileName <> "" Then
                    GvRoute.CurrentRow.Cells(col_truck_Sheet_of_Transporter).Value = openFileDialog1.FileName
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        'Try
        '    Dim Vlc_Code As String = gv1.CurrentRow.Cells("VLC DOC CODE").Value
        '    Dim trans As SqlTransaction
        '    Dim obj As clsMilkShiftEndMCC = clsMilkShiftEndMCC.LoadDataFromDetails(Vlc_Code, NavigatorType.Current, trans)
        '    If Not IsNothing(obj) Then
        '        For Each objtr As clsMilkShiftEndMCCDetail In obj.ObjList
        '            'txtCode.Value = objtr.DOC_CODE
        '            Me.dtpDocDate.Value = objtr.DOC_DATE
        '            Me.cboShift.SelectedValue = objtr.SHIFT
        '            Me.fndMCCCode.text = objtr.MCC_CODE

        '            cboShift.enabled=false
        '            btnsave.Text = "Update"
        '        Next
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub


    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            'If clsCommon.MyMessageBoxShow("Do You want to delete this Row Permanently . Are You Sure.?", "Delete Row", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
            '    Dim sQuery As String = "delete from TSPL_MILK_RECEIPT_DETAIL where DOC_Code='" & gv1.CurrentRow.Cells("DOC CODE").Value & "' and VLC_DOC_Code='" & gv1.CurrentRow.Cells("VLC DOC CODE").Value & "'"
            '    clsDBFuncationality.ExecuteNonQuery(sQuery)
            'Else
            e.Cancel = True
            'End If
        Catch ex As Exception
        End Try
    End Sub

 

    'Private Sub TxtClosing_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        txtTotalKM.Text = clsCommon.myCdbl(TxtClosing.Text) - clsCommon.myCdbl(txtOpening.Text)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtActualKm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        TxtDiff.Text = clsCommon.myCdbl(txtTotalKM.Text) - clsCommon.myCdbl(txtActualKm.Text)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveLayout.Click
        '    If clsCommon.myLen(GetReportID()) > 0 Then
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = "MilkShiftEndGrid"
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

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData("MilkShiftEndGrid", objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("MilkShiftEndGrid", "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    'Private Sub BtnMilkTruckSheet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMilkTruckSheet.Click
    '    Dim frm As New FrmMilkTruckSheet
    '    frm.SetUserMgmt(clsUserMgtCode.MilkTruckSheet)
    '    frm.Show()
    'End Sub

    Private Sub TxtManualFat_Per_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtManualFat_Per.TextChanged
        If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) > 0 Then

            TxtManualFAT.Text = Math.Round(clsCommon.myCdbl(TxtManualFat_Per.Text) * clsCommon.myCdbl(TxtManualStock.Text) / 100, 2)
        Else
            TxtManualFAT.Text = Nothing
        End If
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

    Private Sub TxtManualFAT_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtManualFAT.Leave
        If clsCommon.myLen(clsCommon.myCstr(TxtManualStock.Text)) > 0 Then
            TxtManualFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtManualFAT.Text) * 100 / IIf(clsCommon.myCdbl(TxtManualStock.Text) <= 0, 1, clsCommon.myCdbl(TxtManualStock.Text)), 2)
        Else
            TxtManualFat_Per.Text = Nothing
        End If
    End Sub

    Private Sub TxtManualFAT_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtManualFAT.KeyPress, TxtManualFat_Per.KeyPress, TxtManualSNF.KeyPress, TxtManualSNF_Per.KeyPress, TxtManualStock.KeyPress
        If Not IsNumeric(e.KeyChar) And Not e.KeyChar = "." Then
            e.Handled = True
        End If
    End Sub

    Private Sub fndMCC_Validating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndMcc._MYValidating
        Dim sQuery As String = "select Location_Category from tspl_location_master where Location_Code='" & fndMcc.Value & "'"
        'If clsDBFuncationality.getSingleValue(sQuery) = "HO" Then
        Dim qry As String = "select Location_Code as Location,Location_Desc as Description  from TSPL_LOCATION_MASTER "
        ' fndMCC.Value = clsCommon.ShowSelectForm("LocatMast", qry, "Location", "  upper(location_category)='MCC' ", fndMCC.Value, "Location_Code", isButtonClicked)
        fndMcc.Value = clsMccMaster.getFinder("", fndMcc.Value, isButtonClicked)
        Dim DTShift As DataTable = clsMilkReceiptMCC.GetShift(fndMcc.Value)
        If DTShift Is Nothing OrElse DTShift.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No shifts is opened.Atleats one Shift should be Opened..", Me.Text)
            btnsave.Enabled = False
        ElseIf DTShift.Rows.Count > 1 Then
            clsCommon.MyMessageBoxShow(Me, "There are more then one shifts are opened.Only one Shift can be Opened..", Me.Text)
            Me.Close()
        Else
            btnsave.Enabled = True
            dtpDocDate.Value = DTShift.Rows(0).Item("MCC_Shift_date")
            cboShift.SelectedValue = DTShift.Rows(0).Item("Shift")
            fndMccCode.Text = clsDBFuncationality.getSingleValue("select Mcc_name from tspl_mcc_master where mcc_code='" & fndMcc.Value & "'")
            DtpMCCDate.Value = dtpDocDate.Value
            LoadMilk_receipt_Code()
        End If
        ' End If
        DtStock = ClsOpenMCCShift.Getstock(dtpDocDate.Value, fndMcc.Value)
        If DtStock.Rows.Count > 0 Then
            TxtActualStock.Text = DtStock.Rows(0).Item("Qty")
            TxtActualFat.Text = DtStock.Rows(0).Item("FAT")
            TxtActualSNF.Text = DtStock.Rows(0).Item("SNF")
            TxtBookFat_Per.Text = Math.Round(clsCommon.myCdbl(TxtActualFat.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
            TxtBookSNF_per.Text = Math.Round(clsCommon.myCdbl(TxtActualSNF.Text) * 100 / clsCommon.myCdbl(TxtActualStock.Text), 2)
        End If
        Me.txtCode.Value = clsMilkReceiptMCC.GetDocCode(Me.dtpDocDate.Value, Me.fndMcc.Value, Me.cboShift.Text)
        SetSampleDetails()
        ''RICHA BHA/25/10/18-000640 25 OCT,2018
        qry = "select AskSiloatShiftEnd,AutoIn_Location,SILOIn_Location,MCC_in_Plant from TSPL_MCC_MASTER where MCC_Code='" + fndMcc.Value + "'"
        Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0 Then
            fndAutoInLoc.Value = clsCommon.myCstr(dtMCC.Rows(0)("AutoIn_Location"))
            chkAskSiloatShiftEnd.Checked = IIf(clsCommon.myCdbl(dtMCC.Rows(0)("AskSiloatShiftEnd")) = 1, True, False)
            If chkAskSiloatShiftEnd.Checked = True Then
                fndSiloInLoc.Visible = True
                txtSiloInLoc.Visible = True
                lblSiloInLocation.Visible = True
            Else
                fndSiloInLoc.Visible = False
                txtSiloInLoc.Visible = False
                lblSiloInLocation.Visible = False
            End If
        End If

    End Sub
    ''RICHA BHA/25/10/18-000640 25 OCT,2018
    Private Sub fndSiloInLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSiloInLoc._MYValidating
        If clsCommon.myLen(fndAutoInLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please select a In Location First ", Me.Text)
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        fndSiloInLoc.Value = clsLocation.getFinder("is_sub_location='Y' and Main_Location_Code='" & fndAutoInLoc.Value & "' " & whrCls, fndSiloInLoc.Value, isButtonClicked)
        If clsCommon.myLen(fndSiloInLoc.Value) > 0 Then
            txtSiloInLoc.Text = clsLocation.GetName(fndSiloInLoc.Value, Nothing)
        Else
            txtSiloInLoc.Text = ""
        End If
    End Sub
    Private Sub btnemailsetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnemailsetting.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmMilkShiftEndMCC
        frm.ShowDialog()
    End Sub

    Private Sub btnemailsmssettingforvsp_Click(sender As Object, e As EventArgs) Handles btnemailsmssettingforvsp.Click
        Dim frm As New FrmMailSMSSettingNew2()
        frm.FormId = clsUserMgtCode.frmMilkShiftEndMCC + "VSP"
        frm.ShowDialog()
    End Sub

    Private Sub txtCLR_Validating(sender As Object, e As CancelEventArgs) Handles txtCLR.Validating
        CalculateSNFFromCLR()
    End Sub

    Sub CalculateSNFFromCLR()
        If isCLRInsteadOfSNF Then
            txtCLR.Value = clsERPFuncationality.myDclInZeroPointFive(txtCLR.Value)
            TxtManualSNF_Per.Value = Math.Round(clsEkoPro.getSnfOnCalculation(TxtManualFat_Per.Value, txtCLR.Value, dclCorrectionFactor), 2, MidpointRounding.ToEven)
        End If
    End Sub
End Class
