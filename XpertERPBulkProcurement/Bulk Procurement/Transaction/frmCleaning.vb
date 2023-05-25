''richa agarwal against ticket no BM00000006412 on 04/05/2015(Apply GL Security)
Imports common
Imports System.Data.SqlClient
Public Class FrmCleaning
    Inherits FrmMainTranScreen
    Dim AllowJobWorkonGateEntryBulkProc As Integer = 0
    Dim TankerFromMaster As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsCleaning = Nothing
    Public errorControl As clsErrorControl = New clsErrorControl()
    Public strDocCode As String = ""
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Sub reset()
        fndRefrenceNo.Value = ""
        lblVendorName.Text = ""
        txtSubLocation.Value = ""
        lblSubLocation.Text = ""
        chkJobWork.Checked = False
        txtInTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        txtOutTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        TxtRemarks.Text = ""
        fndDocNo.Value = ""
        fndGateEntryNo.Value = ""
        dtpStartDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpEndDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpWeighmentDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpQCDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpUnloadingDateTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        txtUnloadingNo.Text = ""
        fndTankerNo.Value = ""
        txtQCNo.Text = ""
        txtWeighmentNo.Text = ""
        fndCleaningDoneBy.Value = ""
        lblDoneByName.Text = ""
        fndCheckedBy.Value = ""
        lblCheckedByName.Text = ""
        ddlStatus.Text = ""
        'ddlStatus.Items.Clear()
        ddlStatus.DataSource = Nothing
        ddlStatus.DataSource = loadStatus()
        ddlStatus.DisplayMember = "status"
        ddlStatus.ValueMember = "status"
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPrint.Enabled = False
        btnPost.Enabled = False
        lblPending.Status = ERPTransactionStatus.Pending
        fndDocNo.MyReadOnly = False
        '=========Added by preeti gupta==================
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)

        If DateTime = "1" Then
            dtpStartDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpEndDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpWeighmentDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpQCDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
            dtpUnloadingDateTime.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpStartDateTime.CustomFormat = "dd/MM/yyyy"
            dtpEndDateTime.CustomFormat = "dd/MM/yyyy"
            dtpWeighmentDateTime.CustomFormat = "dd/MM/yyyy"
            dtpGateEntryDateTime.CustomFormat = "dd/MM/yyyy"
            dtpQCDateTime.CustomFormat = "dd/MM/yyyy"
            dtpUnloadingDateTime.CustomFormat = "dd/MM/yyyy"
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
            obj = New clsCleaning()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            Dim strLoc As String = clsDBFuncationality.getSingleValue("select location_code from Tspl_Gate_Entry_Details  where gate_entry_no='" & fndGateEntryNo.Value & "'", trans)
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                If chkJobWork.Checked Then
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.Cleaning, clsDocTransactionType.MCCProcJobWorkOutward, txtSubLocation.Value)
                Else
                    obj.Doc_No = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.Cleaning, clsDocTransactionType.NA, strLoc)
                End If
                If clsCommon.myLen(obj.Doc_No) <= 0 Then
                    Throw New Exception("Error In Document  No Genertion")
                End If
            Else
                obj.Doc_No = clsCommon.myCstr(fndDocNo.Value)
            End If
            obj.IsAgainstJobWork = IIf(chkJobWork.Checked, 1, 0)
            obj.Joblocation_Code = txtSubLocation.Value
            fndDocNo.Value = obj.Doc_No
            obj.Gate_Entry_No = clsCommon.myCstr(fndGateEntryNo.Value)
            obj.Start_Date_Time = clsCommon.GetPrintDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh: mm:ss tt")
            obj.End_Date_Time = clsCommon.GetPrintDate(dtpEndDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt")
            obj.Tanker_No = clsCommon.myCstr(fndTankerNo.Value)
            obj.Weighment_No = clsCommon.myCstr(txtWeighmentNo.Text)
            obj.QC_No = clsCommon.myCstr(txtQCNo.Text)
            obj.Done_by = clsCommon.myCstr(fndCleaningDoneBy.Value)
            obj.Checked_by = clsCommon.myCstr(fndCheckedBy.Value)
            obj.Status = clsCommon.myCstr(ddlStatus.Text)
            obj.Remarks = clsCommon.myCstr(TxtRemarks.Text)
            If Not isPost Then
                obj.isPosted = 0
            End If
            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            obj.InTime = txtInTime.Value
            obj.OutTime = txtOutTime.Value
            If clsCleaning.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully")
                    End If
                End If
                loadData(obj.Doc_No, NavigatorType.Current)
            Else
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btnPrint.Enabled = False
                fndDocNo.MyReadOnly = False
                Throw New Exception("Data Not Saved ")
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Dim arr As List(Of String) = New List(Of String)
        obj = clsCleaning.getData(fndDocNo.Value, NavigatorType.Current)
        trans = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If deleteConfirm() Then
                    Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
                    Dim settTankerDispatchIntermittentSingleGateIn As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, trans)) = 1)
                    Dim TempDocType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_weighment_detail.Doc_Type from tspl_weighment_detail where weighment_no='" & obj.Weighment_No & "'", trans))
                    Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isintermittent from TSPL_MCC_Dispatch_Challan  left outer join tspl_weighment_detail  on tspl_weighment_detail.challan_no=TSPL_MCC_Dispatch_Challan.chalan_no  where tspl_weighment_detail.Weighment_No ='" & obj.Weighment_No & "' ", trans))
                    If rValue = 1 AndAlso settTankerDispatchIntermittentSingleGateIn = True AndAlso MCCChamberwise = 1 AndAlso clsCommon.CompairString(TempDocType, "MccProc") = CompairStringResult.Equal Then
                        'Check not required because Cleaning manually
                    ElseIf obj IsNot Nothing Then
                        If clsWeighment.isWeighmentDone(obj.Gate_Entry_No, trans) Then
                            Throw New Exception("Document is in use. Can't Delete")
                        End If
                    End If
                    arr.Add(fndDocNo.Value)
                    If clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.frmCleaning, trans) Then
                        If clsCleaning.deleteData(fndDocNo.Value, trans) Then
                            trans.Commit()
                            reset()
                            clsCommon.MyMessageBoxShow(Me, "Deleted Successfully")
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Could Not Deleted. Try Again")
                            trans.Rollback()
                        End If
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select a Cleaning Document No To delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            trans.Rollback()
        End Try
    End Sub

    Sub PostData()
        Dim str As String = String.Empty
        Try

            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsCleaning.postData(fndDocNo.Value, Me.Form_ID)) Then
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
                loadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub frmCleaning_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverse.Visible = True
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PrintData()
        clsCommon.MyMessageBoxShow(Me, "No Print Format Found")
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub
    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateEntryNo._MYValidating
        '"TSPL_gate_entry_details.isPosted='1'  and TSPL_gate_entry_details.Gate_Entry_No not in (select TSPL_Cleaning.Gate_Entry_No from TSPL_Cleaning where TSPL_Cleaning.gate_entry_no<>'" & fndGateEntryNo.Value & "' )  and ISNULL(TSPL_Weighment_Detail.Weighment_No,'')<>''  and ISNULL(tspl_quality_check.QC_No,'')<>'' and ISNULL(tspl_quality_check.is_param_accepted,0)<>0 and  TSPL_gate_entry_details.gate_entry_no in (select gate_entry_no from tspl_milk_unloading where isposted=1) "
        'fndGateEntryNo.Value = clsCleaning.getGateEntryFinder("  ", fndGateEntryNo.Value, isButtonClicked)
        'If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
        '    LoadGateEntryData(fndGateEntryNo.Value)
        'Else
        '    reset()
        'End If
    End Sub
    Function allowToSave() As Boolean
        Try

            '===============Preeti Gupta==================================
            If AllowFutureDateTransaction(dtpUnloadingDateTime.Value, Nothing) = False Then
                dtpUnloadingDateTime.Select()
                Return False
            End If
            '=======================================================
            If clsCommon.myLen(fndTankerNo.Value) <= 0 Then
                Throw New Exception("Please Select tanker No")
                errorControl.SetError(fndTankerNo, "Please Select tanker No")
            Else
                errorControl.ResetError(fndTankerNo)
            End If
            If clsCommon.myLen(fndGateEntryNo.Value) <= 0 Then
                Throw New Exception("Please Select GateEntry No")
                errorControl.SetError(fndGateEntryNo, "Please Select Gate Entry No")
            Else
                errorControl.ResetError(fndGateEntryNo)
            End If

            If clsCommon.myLen(fndCleaningDoneBy.Value) <= 0 Then
                Throw New Exception("Please Enter Cleaning Done By")
                errorControl.SetError(fndCleaningDoneBy, "Please Enter Cleaning Done By")
            Else
                errorControl.ResetError(fndCleaningDoneBy)
            End If

            If clsCommon.myLen(fndCheckedBy.Value) <= 0 Then
                Throw New Exception("Please Enter Checked By")
                errorControl.SetError(fndCheckedBy, "Please Enter Checked By")
            Else
                errorControl.ResetError(fndCheckedBy)
            End If

            If clsCommon.myLen(ddlStatus.Text) <= 0 Then
                Throw New Exception("Please Enter Status")
                errorControl.SetError(ddlStatus, "Please Enter Status")
            Else
                errorControl.ResetError(ddlStatus)
            End If

            Dim WDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select weighment_date from TSPL_Weighment_Detail where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim QDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select Qc_out_date_time from TSPL_QUALITY_CHECK where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            Dim GDate As Date = clsCommon.GetPrintDate(clsCommon.myCDate(clsDBFuncationality.getSingleValue("select date_and_time from Tspl_Gate_Entry_Details where gate_entry_no='" & fndGateEntryNo.Value & "'")), "dd/MMM/yyyy hh:mm:ss tt")
            'If WDate > dtpStartDateTime.Value Then
            '    Throw New Exception("Cleaning start Date time should not be less than weighment date")
            'End If

            'If QDate > dtpStartDateTime.Value Then
            '    Throw New Exception("Cleaning start date time should not be less than Quality Check date")
            'End If

            'If GDate > dtpStartDateTime.Value Then
            '    Throw New Exception("Cleaning start date time should not be less than Gate Entry date")
            'End If
            'If dtpEndDateTime.Value < dtpStartDateTime.Value Then
            '    Throw New Exception("Cleaning end date time should not be less than Cleaning Start date time")
            'End If
            '==================Added by preeti Gupta Against ticket No[KDI/13/06/18-000363]
            If clsCommon.GetDateWithStartTime(dtpStartDateTime.Value) < clsCommon.GetDateWithStartTime(dtpUnloadingDateTime.Value) Then
                Throw New Exception("Cleaning start Date can not be less than Unloading Date")
            End If
            If clsCommon.GetDateWithStartTime(dtpEndDateTime.Value) < clsCommon.GetDateWithStartTime(dtpUnloadingDateTime.Value) Then
                Throw New Exception("Cleaning END Date can not be less than Unloading Date")
            End If
            '===================================================================================================================

            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from tspl_cleaning where gate_entry_no='" & fndGateEntryNo.Value & "' and doc_No <>'" & fndDocNo.Value & "' "))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other Document.")
            End If
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowcleaningDateAfterCurrentDate, Nothing)) = 0 Then
            '    Dim dt As Date = clsCommon.GETSERVERDATE()
            '    If clsCommon.myCDate(dtpStartDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
            '        dtpStartDateTime.Value = dt
            '        Throw New Exception("Cleaning Start Date should not be Larger than current date")
            '    End If
            'End If
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowcleaningDateAfterCurrentDate, Nothing)) = 0 Then
            '    Dim dt As Date = clsCommon.GETSERVERDATE()
            '    If clsCommon.myCDate(dtpEndDateTime.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
            '        dtpEndDateTime.Value = dt
            '        Throw New Exception("Cleaning End Date should not be Larger than current date")
            '    End If
            'End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub FrmCleaning_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Panel3.Enabled = False
        AllowJobWorkonGateEntryBulkProc = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, Nothing))
        TankerFromMaster = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
        SetUserMgmtNew()
        reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(strDocCode) > 0 Then
            loadData(strDocCode, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        If TankerFromMaster = 0 Then
            Panel1.Visible = False
        Else
            Panel1.Visible = True
        End If
        If AllowJobWorkonGateEntryBulkProc = 1 Then
            Panel3.Visible = True
        Else
            Panel3.Visible = False
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmCleaning)
        'If Not (MyBase.isReadFlag) Then
        '    If MDI.blnShowAllMenu = False Then
        '        Throw New Exception("Permission Denied")
        '    Else
        '        Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")
        '    End If
        'End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadGateEntryData(ByVal strGateEntryNo As String)
        Dim strDocNo As String = clsDBFuncationality.getSingleValue("select Doc_No from TSPL_Cleaning where gate_entry_no='" & strGateEntryNo & "'")
        If clsCommon.myLen(strDocNo) > 0 Then
            loadData(strDocNo, NavigatorType.Current)
            Exit Sub
        End If
        Dim objGt As New clsGateEntry()
        reset()
        objGt = clsGateEntry.getData(strGateEntryNo, NavigatorType.Current)
        If objGt IsNot Nothing Then
            lblVendorName.Text = objGt.Vendor_Desc
            txtSubLocation.Value = objGt.Sublocation_Code
            lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            chkJobWork.Checked = IIf(objGt.IsAgainstJobWork = 1, True, False)
            fndGateEntryNo.Value = objGt.Gate_Entry_No
            fndTankerNo.Value = objGt.Tanker_No
            fndRefrenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + objGt.Gate_Entry_No + "' "))
            Dim strWeighmentNo As String = clsDBFuncationality.getSingleValue("select Weighment_No from TSPL_weighment_detail where gate_entry_no='" & objGt.Gate_Entry_No & "' ")
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
            loadGateInQCWeighmentDateTime()
        End If
    End Sub
    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        obj = clsCleaning.getData(str, navtype)
        If obj IsNot Nothing Then
            reset()
            lblVendorName.Text = clsDBFuncationality.getSingleValue("select isnull(tspl_gate_entry_details.Vendor_Desc,'') as Vendor_Desc from tspl_gate_entry_details where gate_entry_no='" & obj.Gate_Entry_No & "' ")
            chkJobWork.Checked = IIf(obj.IsAgainstJobWork = 1, True, False)
            txtSubLocation.Value = obj.Joblocation_Code
            If clsCommon.myLen(txtSubLocation.Value) > 0 Then
                lblSubLocation.Text = clsLocation.GetName(txtSubLocation.Value, Nothing)
            End If
            fndDocNo.Value = obj.Doc_No
            fndGateEntryNo.Value = obj.Gate_Entry_No
            dtpStartDateTime.Value = obj.Start_Date_Time
            dtpEndDateTime.Value = obj.End_Date_Time
            fndTankerNo.Value = obj.Tanker_No
            fndRefrenceNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Reference_No from tspl_gate_entry_details where Gate_Entry_No = '" + obj.Gate_Entry_No + "' "))
            txtWeighmentNo.Text = obj.Weighment_No
            txtQCNo.Text = obj.QC_No
            fndCleaningDoneBy.Value = obj.Done_by
            lblDoneByName.Text = clsEmployeeMaster.GetName(obj.Done_by, Nothing)
            fndCheckedBy.Value = obj.Checked_by
            lblCheckedByName.Text = clsEmployeeMaster.GetName(obj.Checked_by, Nothing)
            ddlStatus.Text = obj.Status
            TxtRemarks.Text = clsCommon.myCstr(obj.Remarks)
            If obj.InTime IsNot Nothing Then
                txtInTime.Value = obj.InTime
            End If
            If obj.OutTime IsNot Nothing Then
                txtOutTime.Value = obj.OutTime
            End If
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            btnPrint.Enabled = True
            If obj.isPosted = 1 Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                lblPending.Status = ERPTransactionStatus.Approved
            Else
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                lblPending.Status = ERPTransactionStatus.Pending
            End If
            fndDocNo.MyReadOnly = True
            loadGateInQCWeighmentDateTime()
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
        fndDocNo.Value = clsCleaning.getFinder(strwhrcls, fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            loadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub fndCleaningDoneBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCleaningDoneBy._MYValidating
        fndCleaningDoneBy.Value = clsEmployeeMaster.getFinder("", fndCleaningDoneBy.Value, isButtonClicked)
        If clsCommon.myLen(fndCleaningDoneBy.Value) > 0 Then
            lblDoneByName.Text = clsEmployeeMaster.GetName(fndCleaningDoneBy.Value, Nothing)
        Else
            lblDoneByName.Text = ""
        End If
    End Sub

    Private Sub fndCheckedBy__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCheckedBy._MYValidating
        fndCheckedBy.Value = clsEmployeeMaster.getFinder("", fndCheckedBy.Value, isButtonClicked)
        If clsCommon.myLen(fndCheckedBy.Value) > 0 Then
            lblCheckedByName.Text = clsEmployeeMaster.GetName(fndCheckedBy.Value, Nothing)
        Else
            lblCheckedByName.Text = ""
        End If
    End Sub

    Function loadStatus() As DataTable
        'Dim qry As String = "select distinct  status from( select distinct status from tspl_cleaning union all select 'OK' as status union all select 'NOT OK' as status) xx "
        Dim qry As String = " select 'OK' as status union all select 'NOT OK' as status"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        fndGateEntryNo.Value = clsCleaning.getTankerFinder("  ", fndTankerNo.Value, isButtonClicked)
        If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
            LoadGateEntryData(fndGateEntryNo.Value)
        Else
            reset()
        End If
    End Sub

    Sub loadGateInQCWeighmentDateTime(Optional trans As SqlTransaction = Nothing)
        Try
            Dim qry As String = ""
            If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
                qry = " select Date_And_Time from tspl_gate_entry_details where gate_entry_no='" & fndGateEntryNo.Value & "'"
                dtpGateEntryDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
            If clsCommon.myLen(txtWeighmentNo.Text) > 0 Then
                qry = " select Weighment_Date from tspl_weighment_detail where weighment_no='" & txtWeighmentNo.Text & "'"
                dtpWeighmentDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
            If clsCommon.myLen(txtQCNo.Text) > 0 Then
                qry = "select Qc_In_Date_Time from tspl_quality_check where QC_No='" & txtQCNo.Text & "'"
                dtpQCDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
            If clsCommon.myLen(fndGateEntryNo.Value) > 0 Then
                qry = "select Unloading_no from tspl_milk_unloading where gate_entry_no='" & fndGateEntryNo.Value & "'"
                txtUnloadingNo.Text = clsDBFuncationality.getSingleValue(qry, trans)
            End If

            If clsCommon.myLen(txtUnloadingNo.Text) > 0 Then
                qry = "select unloading_date_time from tspl_milk_unloading where unloading_no='" & txtUnloadingNo.Text & "'"
                dtpUnloadingDateTime.Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue(qry, trans), "dd/MMM/yyyy hh:mm:ss tt")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                If clsCleaning.ReverseAndUnpost(fndDocNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    loadData(fndDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
