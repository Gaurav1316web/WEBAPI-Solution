''richa agarwal against ticket no BM00000006412 on 04/05/2015(Apply GL Security)
Imports common
Imports System.Data.SqlClient
Public Class FrmGateOut
    Inherits FrmMainTranScreen
    Dim AllocateToMandatoryonGateOut As Integer = 0
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim isCleaningMandatoryBeforeGateout
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsGateOut = Nothing
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim AutoMilkTransferInDateSameasWeighmentDate As Boolean = False
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    'Ticket no :  ERO/25/03/19-000519 by prabhakar 
    Sub reset()
        fndReferenceNo.Value = ""
        btnSave.Enabled = True
        txtDriver.Text = ""
        fndAllocate.Value = ""
        lblAllocateTo.Text = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        fndDocNo.Value = ""
        fndGateEntryNo.Value = ""
        dtpStartDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        fndTankerNo.Value = ""
        txtQCNo.Text = ""
        txtWeighmentNo.Text = ""
        txtMilkTransferIn.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        fndDocNo.MyReadOnly = False
        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpStartDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpStartDateTime.CustomFormat = "dd/MM/yyyy"

        End If
        '==========================================================
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If allowToSave() Then SaveData(False)
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            obj = New clsGateOut()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                'Dim Isjobwork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isnull(IsAgainstJobWork,0) from tspl_gate_entry_details where gate_entry_no='" & fndGateEntryNo.Value & "'", trans))
                Dim isPODocumentTypeWise As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementCounterOnEntryType, clsFixedParameterCode.BulkProcurementCounterOnEntryType, trans)) = 0, False, True) ''Make Setting Balwinder
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select Gate_Entry_Type,Doc_Type,location_code  from Tspl_Gate_Entry_Details where Gate_Entry_No='" + fndGateEntryNo.Value + "'", trans)
                If dtTemp Is Nothing OrElse dtTemp.Rows.Count <= 0 Then
                    Throw New Exception("Gate Entry No" + fndGateEntryNo.Value + " Not found ")
                End If
                Dim isBulkPro As Boolean = clsCommon.CompairString(clsCommon.myCstr(dtTemp.Rows(0)("Doc_Type")), "BulkProc") = CompairStringResult.Equal
                Dim strLoc As String = clsCommon.myCstr(dtTemp.Rows(0)("location_code"))
                If isPODocumentTypeWise AndAlso isBulkPro Then
                    Dim strGateEntryType As String = clsCommon.myCstr(dtTemp.Rows(0)("Gate_Entry_Type"))
                    If clsCommon.myLen(strGateEntryType) <= 0 Then
                        Throw New Exception("Gate Entry Type not found on Gate Entry screen")
                    End If
                    If clsCommon.CompairString(strGateEntryType, "P") = CompairStringResult.Equal Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateOut, clsDocTransactionType.BulkProcPurchase, strLoc)
                    ElseIf clsCommon.CompairString(strGateEntryType, "J") = CompairStringResult.Equal Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateOut, clsDocTransactionType.BulkProcJobWork, strLoc)
                    Else
                        Throw New Exception("Wrong Gate Entry Type")
                    End If
                Else
                    If chkJobWork.Checked Then
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateOut, IIf(isBulkPro, clsDocTransactionType.BulkProcJobWorkOutward, clsDocTransactionType.MCCProcJobWorkOutward), txtSubLocation.Value)
                    Else
                        obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateOut, clsDocTransactionType.NA, strLoc)
                    End If
                End If

                If clsCommon.myLen(obj.Doc_No) <= 0 Then
                    Throw New Exception("Error In Document  No Genertion")
                End If
            Else
                obj.Doc_No = clsCommon.myCstr(fndDocNo.Value)
            End If
            obj.DriverName = txtDriver.Text
            obj.AllocateToMCC = fndAllocate.Value
            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            fndDocNo.Value = obj.Doc_No
            obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNo.Value)
            obj.Doc_Date = clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(fndTankerNo.Value)
            obj.Weighment_No = clsCommon.myCstr(txtWeighmentNo.Text)
            obj.QC_No = clsCommon.myCstr(txtQCNo.Text)
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            clsGateOut.saveData(obj, trans)
            trans.Commit()
            If Not isPost Then
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                End If
            End If
            loadData(obj.Doc_No, NavigatorType.Current)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnSave.Text = "Save"
            btnDelete.Enabled = False
            fndDocNo.MyReadOnly = False
        End Try
    End Sub
    Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        obj = clsGateOut.getData(fndDocNo.Value, NavigatorType.Current)
        trans = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If deleteConfirm() Then

                    Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
                    Dim settTankerDispatchIntermittentSingleGateIn As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, trans)) = 1)
                    Dim TempDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_weighment_detail.Doc_Type from tspl_weighment_detail where weighment_no='" & obj.Weighment_No & "'", trans))
                    Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isintermittent from TSPL_MCC_Dispatch_Challan  left outer join tspl_weighment_detail  on tspl_weighment_detail.challan_no=TSPL_MCC_Dispatch_Challan.chalan_no  where tspl_weighment_detail.Weighment_No ='" & obj.Weighment_No & "' ", trans))
                    If rValue = 1 AndAlso settTankerDispatchIntermittentSingleGateIn = True AndAlso MCCChamberwise = 1 AndAlso clsCommon.CompairString(TempDocType, "MccProc") = CompairStringResult.Equal Then
                        'Check not required because Gateout cretaed from Cleaning
                    ElseIf obj IsNot Nothing Then
                        If clsWeighment.isWeighmentDone(obj.Gate_Entry_No, trans) Then
                            Throw New Exception("Document is in use. Can't Delete")
                        End If
                    End If
                    If clsGateOut.deleteData(fndDocNo.Value, trans) Then
                        trans.Commit()
                        reset()
                        clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Could Not Deleted. Try Again", Me.Text)
                        trans.Rollback()
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select a Cleaning Document No To delete", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    'Sub PostData()
    '    Dim str As String = String.Empty
    '    Try

    '        Dim qry As String = ""
    '        Dim msg As String = ""
    '        Dim dt As DataTable = Nothing
    '        If (myMessages.postConfirm()) Then
    '            SaveData(True)
    '            If (clsCleaning.postData(fndDocNo.Value, Me.Form_ID)) Then
    '                msg = "Successfully Posted"
    '            Else
    '                qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
    '                dt = clsDBFuncationality.GetDataTable(qry)
    '                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                    Dim level As String = dt.Rows(0)("LEVEL").ToString()
    '                    Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
    '                    If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
    '                        msg = "Level 1 Approval done. "
    '                        If NoOflevel = 1 Then
    '                            msg += "Successfully Posted. "
    '                        Else
    '                            msg += "Level 2 Approval Required."
    '                        End If
    '                    ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
    '                        msg = "Level 2 Approval done. "
    '                        If NoOflevel = 2 Then
    '                            msg += "Successfully Posted "
    '                        Else
    '                            msg += "Level 3 Approval Required."
    '                        End If
    '                    Else
    '                        msg = "Level 3 Approval done. Successfully Posted. "
    '                    End If
    '                End If
    '            End If
    '            common.clsCommon.MyMessageBoxShow(msg)
    '            loadData(fndDocNo.Value, NavigatorType.Current)
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Private Sub frmGateOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            '    btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub PrintData()
        clsCommon.MyMessageBoxShow("No Print Format Found")
    End Sub
    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        'Dim whrCls As String = " "
        'If Not clsMccMaster.isCurrentUserHO() Then
        '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '        whrCls = whrCls & " and xxx.[location code] in (" & objCommonVar.strCurrUserLocations & ") "
        '    End If
        'End If
        'fndGateEntryNo.Value = clsGateOut.getGateEntryFinder("  xxx.GateEntryNo not in (select Gate_Entry_No  from TSPL_Gate_Out  ) " & whrCls, fndGateEntryNo.Value, isButtonClicked)
        'If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
        '    LoadGateEntryData(fndGateEntryNo.Value)
        'Else
        '    reset()
        'End If
    End Sub
    Function allowToSave() As Boolean
        Try
            '= KUNAL > TICKET : BM00000009575 ==============
            If AllowFutureDateTransaction(dtpStartDateTime.Value, Nothing) = False Then
                dtpStartDateTime.Focus()
                Return False
            End If

            If AutoMilkTransferInDateSameasWeighmentDate = True Then
                Dim StrDocType As String = clsDBFuncationality.getSingleValue("select Tspl_Gate_Entry_Details.Doc_Type from Tspl_Gate_Entry_Details where TSPL_gate_entry_details.Gate_Entry_No='" & fndGateEntryNo.Value & "'")
                If clsCommon.CompairString(StrDocType, "MccProc") = CompairStringResult.Equal Then
                    Dim StrTranferIn As String = ""
                    Dim IntPost As Integer = 0
                    StrTranferIn = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_Challan_No from tspl_milk_transfer_in where gate_entry_no='" & fndGateEntryNo.Value & "'"))
                    If clsCommon.myLen(StrTranferIn) <= 0 Then
                        Throw New Exception("Milk Transfer In not found.")
                    Else
                        IntPost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isPosted from tspl_milk_transfer_in where gate_entry_no='" & fndGateEntryNo.Value & "'"))
                        If IntPost = 0 Then
                            Throw New Exception("Milk Transfer In [" + StrTranferIn + "] is unposted,Plz first post it.")
                        End If
                    End If
                End If
            End If

            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                Throw New Exception("Please Select Tanker No")
                errorControl.SetError(fndTankerNo, "Please Select Tanker No")
            Else
                errorControl.ResetError(fndTankerNo)
            End If
            If clsCommon.myLen(fndGateEntryNo.Value) <= 0 Then
                Throw New Exception("Please Select GateEntry No")
                errorControl.SetError(fndGateEntryNo, "Please Select Gate Entry No")
            Else
                errorControl.ResetError(fndGateEntryNo)
            End If


            Dim WDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim QDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Qc_out_date_time from TSPL_QUALITY_CHECK where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim GDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select date_and_time from Tspl_Gate_Entry_Details where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            '=============Added by preeti Gupta Against Ticket No[KDI/13/06/18-000363]
            If WDate > dtpStartDateTime.Value Then
                Throw New Exception("Gate Out Date time should not be less than weighment date")
            End If

            'If QDate > dtpStartDateTime.Value Then
            '    Throw New Exception("Gate Out Date time should not be less than Quality Check date")
            'End If

            'If GDate > dtpStartDateTime.Value Then
            '    Throw New Exception("Gate Out Date time should not be less than Gate Entry date")
            'End If
            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from tspl_gate_out where gate_entry_no='" & fndGateEntryNo.Value & "' and doc_No <>'" & fndDocNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowGateOutDateAfterCurrentDate, Nothing)) = 0 Then
            '    Dim dt As Date = clsCommon.GETSERVERDATE()
            '    If clsCommon.myCDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
            '        dtpStartDateTime.Value = dt
            '        Throw New Exception("Gate Out Date should not be Larger than current date")
            '    End If
            'End If

            If AllocateToMandatoryonGateOut = 1 Then
                Dim DocType As String = clsDBFuncationality.getSingleValue("select Tspl_Gate_Entry_Details.Doc_Type from Tspl_Gate_Entry_Details where TSPL_gate_entry_details.Gate_Entry_No='" & fndGateEntryNo.Value & "'")
                If clsCommon.CompairString(DocType, "MccProc") = CompairStringResult.Equal Then
                    If clsCommon.myLen(fndAllocate.Value) <= 0 Then
                        Throw New Exception("Allocate To Mandatory on Gate Out(MCC)")
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub FrmGateOut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Panel3.Enabled = False
        AllocateToMandatoryonGateOut = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllocateToMandatoryonGateOut, clsFixedParameterCode.AllocateToMandatoryonGateOut, Nothing))
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        SetUserMgmtNew()
        isCleaningMandatoryBeforeGateout = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCleaningMandatoryBeforeGateout, clsFixedParameterCode.isCleaningMandatoryBeforeGateout, Nothing))
        AutoMilkTransferInDateSameasWeighmentDate = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AutoMilkTransferInDateSameasWeighmentDate, clsFixedParameterCode.AutoMilkTransferInDateSameasWeighmentDate, Nothing)) = 1, True, False)
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
        If AllocateToMandatoryonGateOut = 1 Then
            btnPost.Visible = True
            btnAllocatePrint.Visible = True
        Else
            btnPost.Visible = False
            btnAllocatePrint.Visible = False
        End If
        If AutoMilkTransferInDateSameasWeighmentDate = True Then
            lblMilkTransferIn.Visible = True
            txtMilkTransferIn.Visible = True
        Else
            lblMilkTransferIn.Visible = False
            txtMilkTransferIn.Visible = False
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmGateOut)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
        btnPost.Visible = MyBase.isPostFlag
        btnAllocatePrint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadGateEntryData(ByVal strGateEntryNo As String)
        Dim strDocNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_Gate_Out where gate_entry_no='" & strGateEntryNo & "'")
        If clsCommon.myLen(strDocNo) > 0 Then
            loadData(strDocNo, NavigatorType.Current)
            Exit Sub
        End If
        Dim objGt As New clsGateEntry()
        reset()
        objGt = clsGateEntry.getData(strGateEntryNo, NavigatorType.Current)
        If objGt IsNot Nothing Then
            chkJobWork.Checked = IIf(objGt.IsAgainstJobWork = 1, True, False)
            txtSubLocation.Value = objGt.Sublocation_Code
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            End If
            fndGateEntryNo.Value = objGt.Gate_Entry_No
            fndTankerNo.Value = objGt.Tanker_No
            fndReferenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + objGt.Gate_Entry_No + "' "))

            Dim strWeighmentNo As String = ""  ' clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            If clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.GateOutTankerNoAfterGeneralWeighment, clsFixedParameterCode.GateOutTankerNoAfterGeneralWeighment, Nothing)) = "1", True, False)) Then
                strWeighmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_GENERAL_WEIGHMENT_DETAIL where Gate_Entry_No = '" + objGt.Gate_Entry_No + "' "))
            Else
                strWeighmentNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' "))
            End If
            If clsCommon.myLen(strWeighmentNo) > 0 Then
                txtWeighmentNo.Text = clsCommon.myCstr(strWeighmentNo)
            Else
                txtWeighmentNo.Text = ""
            End If
            Dim strQcNo As String = clsDBFuncationality.getSingleValue("select Qc_No from TSPL_Quality_Check where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
            If clsCommon.myLen(strQcNo) > 0 Then
                txtQCNo.Text = clsCommon.myCstr(strQcNo)
            Else
                txtQCNo.Text = ""
            End If

            If AutoMilkTransferInDateSameasWeighmentDate = True Then
                txtMilkTransferIn.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_Challan_No from tspl_milk_transfer_in where gate_entry_no='" & objGt.Gate_Entry_No & "'"))
                Dim StrWeighment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' "))
                If clsCommon.myLen(StrWeighment) > 0 Then
                    txtWeighmentNo.Text = StrWeighment
                End If
            End If

        End If
    End Sub
    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsGateOut.getData(str, navtype)
        If obj IsNot Nothing Then
            reset()
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            txtSubLocation.Value = obj.Joblocation_Code
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            End If
            fndDocNo.Value = obj.Doc_No
            fndGateEntryNo.Value = obj.Gate_Entry_No
            dtpStartDateTime.Value = obj.Doc_Date
            fndTankerNo.Value = obj.Tanker_No
            fndReferenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
            txtWeighmentNo.Text = obj.Weighment_No
            txtQCNo.Text = obj.QC_No
            fndAllocate.Value = obj.AllocateToMCC
            If AutoMilkTransferInDateSameasWeighmentDate = True Then
                txtMilkTransferIn.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Receipt_Challan_No from tspl_milk_transfer_in where gate_entry_no='" & obj.Gate_Entry_No & "'"))
                Dim StrWeighment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_weighment_detail where gate_entry_no='" & obj.Gate_Entry_No & "' "))
                If clsCommon.myLen(StrWeighment) > 0 Then
                    txtWeighmentNo.Text = StrWeighment
                End If
            End If
            If clsCommon.myLen(fndAllocate.Value) > 0 Then
                'lblAllocateTo.Text = clsMccMaster.GetName(fndAllocate.Value, Nothing)
                lblAllocateTo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select z.MCC_NAME  from(select MCC_Code,mcc_name from TSPL_MCC_MASTER union all select TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc  from TSPL_LOCATION_MASTER)z where z.MCC_Code='" & fndAllocate.Value & "'"))
            End If
            txtDriver.Text = obj.DriverName
            If obj.IsPosted = 1 Then
                btnSave.Enabled = False
            Else
                btnSave.Enabled = True
            End If
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            fndDocNo.MyReadOnly = True
        Else
            reset()
        End If
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        loadData(fndDocNo.Value, NavType)
    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Dim strwhrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strwhrcls = " Tspl_Gate_Entry_Details.location_Code in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndDocNo.Value = clsGateOut.getFinder(strwhrcls, fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            loadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub




    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        Dim whrCls As String = " "
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = whrCls & " and xxx.[location code] in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If

        Dim strBulkCleaningMandatory As String = ""
        Dim strMCCCleaningMandatory As String = ""
        Dim isCleaningMandatoryBeforeGateout As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCleaningMandatoryBeforeGateout, clsFixedParameterCode.isCleaningMandatoryBeforeGateout, Nothing))
        Dim ShowBothTankertypeOnCleaning As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowBothTankertypeOnCleaning, clsFixedParameterCode.ShowBothTankertypeOnCleaning, Nothing))
        If isCleaningMandatoryBeforeGateout = 1 Then
            If ShowBothTankertypeOnCleaning = 1 Then
                strBulkCleaningMandatory = "  and  case when is_param_accepted <> 0 then  (select count(*) from TSPL_Cleaning where TSPL_Cleaning.Gate_Entry_No=TSPL_gate_entry_details.Gate_Entry_No) else '1'  end  =1  "
            End If
            strMCCCleaningMandatory = "  and  case when is_param_accepted <> 0 then  (select count(*) from TSPL_Cleaning where TSPL_Cleaning.Gate_Entry_No=TSPL_gate_entry_details.Gate_Entry_No) else '1'  end  =1  "
        End If

        fndGateEntryNo.Value = clsGateOut.getTankerFinder("  xxx.GateEntryNo not in (select Gate_Entry_No  from TSPL_Gate_Out  union all select TSPL_QUALITY_CHECK.Gate_Entry_No   from TSPL_QUALITY_CHECK left outer join TSPL_TRANSACTION_APPROVAL on TSPL_TRANSACTION_APPROVAL.Document_No=TSPL_QUALITY_CHECK.QC_NO  where TSPL_QUALITY_CHECK.isPosted=1 and TSPL_QUALITY_CHECK.is_Param_Accepted=0 and ( ISNULL(TSPL_TRANSACTION_APPROVAL.Document_No ,'')<>'' and ISNULL(TSPL_TRANSACTION_APPROVAL.approve ,0)=0 ) ) " & whrCls, fndTankerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
            LoadGateEntryData(fndGateEntryNo.Value)
        Else
            reset()
        End If
    End Sub
    'KUNAL > TICKET : BM00000009843 > DATE : 21-NOV-2016                                                                 ssssssssssssssss
    Sub PrintData(ByVal GateEntryNo As String, ByVal DocumentType As String)

        Try
            If clsCommon.myLen(GateEntryNo) <= 0 Then
                Throw New Exception("Not found anything to print")
            Else
                'sanjay 12/July/2018 Client-Bharat change
                Dim qry As String = ""
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then

                    qry = " select g.Tanker_No ,g.Gate_Entry_No [Gate-In No] , isnull(o.Doc_No ,'') [Gate-Out No] , (CASE WHEN G.doc_type='MccProc' THEN G.Dispatched_From_Mcc ELSE g.Vendor_Code END) AS  [Vendor Code] ," & _
                     "(CASE WHEN G.Doc_Type='MccProc' then TSPL_MCC_MASTER.MCC_NAME else  g.Vendor_Desc end) as [Vendor Name] , COALESCE(G.Supplier_Code ,'')  AS Sub_Vendor_Code, COALESCE(S.DESCRIPTION ,'')  AS Sub_Vendor_Code_Desc, CONVERT (varchar, g.Date_And_Time,103)  [Gate-In Date] , " & _
                     "RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14) [Gate-In Time], CONVERT (varchar , O.Doc_Date ,103 ) [Gate-Out Date], RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14) [Gate-Out Time], CM.Item_Code [Item Code] , " & _
                     "TSPL_ITEM_MASTER.Item_Desc [Item Desc]  , g.location_Code [Location] , g.Location_Desc [Loc Desc] , g.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address] , " & _
                     "g.Doc_Type [Doc Type],G.snf_Per,g.fat_per,g.MIKL_TYPE_CODE,G.Gate_Entry_Type,G.Seal_Status,G.TotalQty_In_Kg ,CM.Chamber_Desc,tspl_item_master.HSN_Code as HSNCode,CM.UOM, CM.Chamber_Qty,CM.snf_Per as snf_Per_CM, CM.fat_per as fat_per_CM , CM.Line_No,C.Logo_img from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE   LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=G.Dispatched_From_Mcc LEFT JOIN TSPL_Gate_Entry_Chember_Details CM ON CM.GE_Code=G.Gate_Entry_No   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code=CM.item_code  where 1=1 and g.gate_entry_no in ('" + GateEntryNo + "')"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptGateOutMilkProc", "Milk Procurement Bulk Gate Out", clsCommon.myCDate(dtpStartDateTime.Value))

                Else

                    'Remove Doc Type Condition  Ticket No- ERO/09/05/18-000301
                    'Dim qry As String = " select  COALESCE(G.Supplier_Code ,'')  AS Sub_Vendor_Code, COALESCE(S.DESCRIPTION ,'')  AS Sub_Vendor_Code_Desc,g.Tanker_No ,g.Gate_Entry_No [Gate-In No] , isnull(o.Doc_No ,'') [Gate-Out No] , g.Vendor_Code [Vendor Code] , g.Vendor_Desc [Vendor Name] , CONVERT (varchar, g.Date_And_Time,103)  [Gate-In Date] , RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14) [Gate-In Time], CONVERT (varchar , O.Doc_Date ,103 ) [Gate-Out Date], RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14) [Gate-Out Time], g.Item_Code [Item Code] , g.Item_Desc [Item Desc]  , g.location_Code [Location] , g.Location_Desc [Loc Desc] , g.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address] , g.Doc_Type [Doc Type]  from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No where 1=1 and doc_type='" + DocumentType + "' and g.gate_entry_no in ('" + GateEntryNo + "')"
                    qry = " select  'Out' as PrintType,COALESCE(G.Supplier_Code ,'')  AS Sub_Vendor_Code, COALESCE(S.DESCRIPTION ,'')  AS Sub_Vendor_Code_Desc,g.Tanker_No ,g.Gate_Entry_No [Gate-In No] , isnull(o.Doc_No ,'') [Gate-Out No] , g.Vendor_Code [Vendor Code] , g.Vendor_Desc [Vendor Name] , CONVERT (varchar, g.Date_And_Time,103)  [Gate-In Date] , RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14) [Gate-In Time], CONVERT (varchar , O.Doc_Date ,103 ) [Gate-Out Date], RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14) [Gate-Out Time], g.Item_Code [Item Code] , g.Item_Desc [Item Desc]  , g.location_Code [Location] , g.Location_Desc [Loc Desc] , g.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address] , g.Doc_Type [Doc Type]  from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No where 1=1 and g.gate_entry_no in ('" + GateEntryNo + "')"
                    'Remove Doc Type Condition  Ticket No- ERO/09/05/18-000301
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptGateInMilkProc", "Milk Procurement Bulk Gate In", clsCommon.myCDate(dtpStartDateTime.Value))
                End If
                frmCRV = Nothing
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    'KUNAL > TICKET : BM00000009843 > DATE : 21-NOV-2016
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            PrintData(fndGateEntryNo.Value, "BulkProc")

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub fndAllocate__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndAllocate._MYValidating
        Try
            Dim WhrCls As String = ""

            Dim qry As String = "select * from (select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_NAME as [Mcc Name]  From tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=tspl_mcc_master.Plant_Code" & Environment.NewLine & _
          "          union all " & Environment.NewLine & _
          " select TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc  from TSPL_LOCATION_MASTER where isnull(type,'')='PLANT')z"

            fndAllocate.Value = clsCommon.ShowSelectForm("MCCMSTGateOut", qry, "Code", WhrCls, fndAllocate.Value, "Code", isButtonClicked)

            If clsCommon.myLen(fndAllocate.Value) > 0 Then
                lblAllocateTo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select z.MCC_NAME  from(select MCC_Code,mcc_name from TSPL_MCC_MASTER union all select TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc  from TSPL_LOCATION_MASTER)z where z.MCC_Code='" & fndAllocate.Value & "'"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                Dim strQry As String = "update TSPL_Gate_Out set IsPosted='1' where DOC_no='" & fndDocNo.Value & "'"
                clsDBFuncationality.ExecuteNonQuery(strQry)
                common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                loadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnAllocatePrint_Click(sender As Object, e As EventArgs) Handles btnAllocatePrint.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                Throw New Exception("Not found anything to print")
            Else

                Dim qry As String = ""
                Dim frmCRV As New frmCrystalReportViewer()
             
                qry = " select  O.Tanker_No  , isnull(o.Doc_No ,'') [Gate-Out No] , O.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address],c.Logo_Img2,O.DriverName,z.MCC_NAME as AllocateToName from TSPL_Gate_Out O LEFT JOIN TSPL_COMPANY_MASTER C on O.comp_code = c.Comp_Code LEFT JOIN (select MCC_Code,mcc_name from TSPL_MCC_MASTER union all select TSPL_LOCATION_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc  from TSPL_LOCATION_MASTER)z on z.MCC_Code=O.AllocateToMCC where O.Doc_No in ('" + fndDocNo.Value + "')"

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptGateOutAllocatePrint", "Gate Out", clsCommon.myCDate(dtpStartDateTime.Value))

                    frmCRV = Nothing
                End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

 
End Class
