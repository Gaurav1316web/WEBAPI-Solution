Imports System.Data.SqlClient
Imports common
Public Class FrmDispatchTransfer
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocNo As String = ""
    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Reset()
    End Sub

    Sub Reset()
        fndFromLocationOld.Enabled = True
        fndNewToLocCode.Enabled = True
        fndDocNo.Value = ""
        fndDocNo.MyReadOnly = False
        dtpDate.Value = clsCommon.GETSERVERDATE()
        fndFromLocationOld.Value = clsGateEntry.getUsersDefaultLocation()
        txtFromLocationNameOld.Text = clsCommon.myCstr(clsLocation.GetName(fndFromLocationOld.Value, Nothing))
        fndTankerNo.Value = ""
        txtChallanNo.Text = ""
        txtOLDToLocCode.Text = ""
        txtOLDToLocName.Text = ""
        ddlTransferTo.Text = "MCC"
        fndNewToLocCode.Value = ""
        txtNewToLocationName.Text = ""
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSave.Enabled = True
        btnClose.Enabled = True
        lblPending.Status = ERPTransactionStatus.Pending
        FindAndSetTabStopFalse(Me)
    End Sub


    Private Sub fndNewToLocCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndNewToLocCode._MYValidating
        Dim qry As String = String.Empty
        If clsCommon.myLen(fndFromLocationOld.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select MCC  from which Dispatch is being made, First", Me.Text)
            Exit Sub
        End If
        Dim blnInterstate As Boolean = False
        Dim strState As String = ""
        If clsLocation.GetGSTLocationInterState(fndFromLocationOld.Value, txtOLDToLocCode.Text, "T", Nothing) Then
            blnInterstate = True
        End If
        Dim FRomLocState = clsDBFuncationality.getSingleValue("Select state from tspl_location_master where location_code='" & fndFromLocationOld.Value & "'")
        If blnInterstate Then
            strState = ""
        Else
            strState = " and state='" & FRomLocState & "'"
        End If
        If clsCommon.myLen(ddlTransferTo.Text) > 0 Then
            If clsCommon.CompairString(ddlTransferTo.Text, "PLANT") = CompairStringResult.Equal Then
                fndNewToLocCode.Value = clsLocation.getFinder("Type='" & ddlTransferTo.Text & "'" & strState, fndNewToLocCode.Value, isButtonClicked)
            ElseIf clsCommon.CompairString(ddlTransferTo.Text, "MCC") = CompairStringResult.Equal Then
                fndNewToLocCode.Value = clsLocation.getFinder("Location_Category='" & ddlTransferTo.Text & "' and location_code <>'" & fndFromLocationOld.Value & "'" & strState, fndNewToLocCode.Value, isButtonClicked)
            End If
            Dim ToLocState = clsDBFuncationality.getSingleValue("Select state from tspl_location_master where location_code='" & fndNewToLocCode.Value & "'")
            If blnInterstate AndAlso clsCommon.CompairString(FRomLocState, ToLocState) = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("From Location State and To location State should be different in case of interstate. ", Me.Text)
                fndNewToLocCode.Value = ""
            End If
            txtNewToLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_desc from tspl_location_master where Location_code='" & fndNewToLocCode.Value & "'"))
        Else
            clsCommon.MyMessageBoxShow("Please Select ' Tanker Dispatch To  ' type First ", Me.Text)
        End If
    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        If clsCommon.myLen(fndFromLocationOld.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Location First.", Me.Text)
            fndFromLocationOld.Focus()
            Exit Sub
        End If
        Dim whr As String = ""
        whr = "  TSPL_MCC_Dispatch_Challan.mcc_code='" & fndFromLocationOld.Value & "'"
        txtChallanNo.Text = clsMccDispatch.getTankerFinder(whr & " and isPosted=1 and Chalan_NO not in (select distinct challan_no from tspl_gate_entry_details )  and Chalan_NO not in (select distinct challan_no from TSPL_MCC_DISPATCH_TRANSFER  where Doc_No<> '" & fndDocNo.Value & "' and isPosted='0')  ", "")
        If clsCommon.myLen(txtChallanNo.Text) > 0 Then
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Mcc_Or_Plant_Code,Tanker_No  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & txtChallanNo.Text & "'")
            fndTankerNo.Value = dt.Rows(0)("Tanker_No")
            txtOLDToLocCode.Text = dt.Rows(0)("Mcc_Or_Plant_Code")
            txtOLDToLocName.Text = clsCommon.myCstr(clsLocation.GetName(txtOLDToLocCode.Text, Nothing))
            ddlTransferTo.Text = "MCC"
            fndNewToLocCode.Value = ""
            txtNewToLocationName.Text = ""
            dt = Nothing
        Else
            txtChallanNo.Text = ""
            fndTankerNo.Value = ""
            txtOLDToLocCode.Text = ""
            txtOLDToLocName.Text = ""
            ddlTransferTo.Text = "MCC"
            fndNewToLocCode.Value = ""
            txtNewToLocationName.Text = ""
        End If
    End Sub

    Private Sub FrmDispatchTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rbtnReset.Enabled Then
            rbtnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose.PerformClick()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                          "========Table Name=========" + Environment.NewLine + _
                          "TSPL_MCC_DISPATCH_TRANSFER" + Environment.NewLine + _
                          "tspl_mcc_dispatch_challan")
        End If
    End Sub
    

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmDispatchTransfer)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmDispatchTransfer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(rbtnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(strDocNo) > 0 Then
            fndDocNo.Value = strDocNo
            LoadData(fndDocNo.Value, Nothing)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndDocNo.Value = clsCommon.myCstr(Me.Tag)
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        GC.Collect()
        Me.Close()
    End Sub
    Function AllowToSave() As Boolean

        'KUNAL > TICKET : BM00000009575 =============
        If AllowFutureDateTransaction(dtpDate.Value, Nothing) = False Then
            dtpDate.Focus()
            Return False
        End If

        If clsCommon.myLen(fndFromLocationOld.Value) <= 0 Then
            Throw New Exception("Please select MCC from which dispatch is being made, first")
        End If
        If clsCommon.myLen(ddlTransferTo.Text) = 0 Then
            Throw New Exception("Please Select Tanker Transfer To Either PLANT/MCC")
        End If
        If clsCommon.myLen(fndNewToLocCode.Value) = 0 Then
            Throw New Exception("Please Select Dispatch To Which Plant/MCC (New Location) ")
        End If
        Dim chlnDate As Date = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select Dispatch_Date   from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & txtChallanNo.Text & "'"))
        If (clsCommon.myCDate(clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy"))) < (clsCommon.myCDate(clsCommon.GetPrintDate(chlnDate, "dd/MMM/yyyy"))) Then
            Throw New Exception(" Transfer Date can not be less than Actual Dispatch Date")
        End If
        Return True
    End Function
    Sub SaveData(ByVal isPostbtnClick As Boolean)
        Try
            Dim trans As SqlTransaction = Nothing
            Dim obj As clsDispatchTransfer = New clsDispatchTransfer()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
                obj.Doc_No = fndDocNo.Value
            End If
            obj.Doc_Date = clsCommon.GetPrintDate(dtpDate.Value, "dd/MMM/yyyy hh:mm:ss tt")
            trans = clsDBFuncationality.GetTransactin()
            obj.OLD_FROM_Loc_Code = clsCommon.myCstr(fndFromLocationOld.Value)
            obj.OLD_TO_LOC_TYPE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Tanker_Dispatch_To    from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & txtChallanNo.Text & "'", trans))
            obj.OLD_TO_Loc_Code = clsCommon.myCstr(txtOLDToLocCode.Text)
            obj.NEW_TO_LOC_TYPE = clsCommon.myCstr(ddlTransferTo.Text)
            obj.NEW_TO_Loc_Code = clsCommon.myCstr(fndNewToLocCode.Value)
            obj.Challan_No = clsCommon.myCstr(txtChallanNo.Text)
            If clsDispatchTransfer.SaveData(obj, trans) Then
                trans.Commit()
                If Not isPostbtnClick Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                End If
                LoadData(obj.Doc_No, NavigatorType.Current)
                btnSave.Text = "Update"
                fndDocNo.MyReadOnly = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                Exit Sub
            Else
                btnSave.Text = "Save"
                btnDelete.Enabled = False
                btnPost.Enabled = False
                fndDocNo.MyReadOnly = False
                trans.Rollback()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub deleteData()
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Want To Delete The Doc No : " & fndDocNo.Value & " ?", "Confirm", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    If clsDispatchTransfer.deleteData(fndDocNo.Value, tran) Then
                        tran.Commit()
                        clsCommon.MyMessageBoxShow("Deleted successFully", Me.Text)
                        Reset()
                    End If
                End If
            Else
                Throw New Exception("Doc No not Found to delete")
            End If
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Try
            Dim obj As clsDispatchTransfer = clsDispatchTransfer.getData(strCode, navType)
            If obj IsNot Nothing Then
                Reset()
                fndFromLocationOld.Enabled = False
                fndNewToLocCode.Enabled = False
                fndDocNo.Value = obj.Doc_No
                dtpDate.Value = obj.Doc_Date
                fndFromLocationOld.Value = obj.OLD_FROM_Loc_Code
                txtFromLocationNameOld.Text = clsCommon.myCstr(clsLocation.GetName(obj.OLD_FROM_Loc_Code, Nothing))
                txtChallanNo.Text = obj.Challan_No
                fndTankerNo.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Tanker_No  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & txtChallanNo.Text & "'"))
                txtOLDToLocCode.Text = obj.OLD_TO_Loc_Code
                txtOLDToLocName.Text = clsCommon.myCstr(clsLocation.GetName(obj.OLD_TO_Loc_Code, Nothing))
                ddlTransferTo.Text = obj.NEW_TO_LOC_TYPE
                fndNewToLocCode.Value = obj.NEW_TO_Loc_Code
                txtNewToLocationName.Text = clsCommon.myCstr(clsLocation.GetName(obj.NEW_TO_Loc_Code, Nothing))

                If obj.isPosted = 0 Then
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                Else
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                End If
                fndDocNo.Focus()
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            SaveData(False)
        End If
    End Sub

    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        LoadData(fndDocNo.Value, NavType)

    End Sub

    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Dim whrCls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = "  OLD_FROM_Loc_Code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        fndDocNo.Value = clsDispatchTransfer.getFinder(whrCls, fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            LoadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deleteData()
    End Sub
    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                SaveData(True)
               
                If (clsDispatchTransfer.PostData(Me.Form_ID, fndDocNo.Value)) Then
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
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub fndFromLocationOld__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndFromLocationOld._MYValidating
        Dim qry As String = String.Empty
        Dim whrCls As String = ""
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = " location_code in (" & objCommonVar.strCurrUserLocations & " )"
            End If
        End If
        fndFromLocationOld.Value = clsCommon.myCstr(clsLocation.getFinder(whrCls, fndFromLocationOld.Value, isButtonClicked))
        txtFromLocationNameOld.Text = clsCommon.myCstr(clsLocation.GetName(fndFromLocationOld.Value, Nothing))
    End Sub
End Class
