Imports common
Imports System.Data.SqlClient
Public Class FrmMCCTankerGateOutSecurity
    Inherits FrmMainTranScreen
#Region "Varibles"
    Dim IsNewEntry As Boolean = False
    Dim SettMCCBulkProcumentSecurityGateOut As Boolean = False
    Dim SettDateTimeFormat As String = "dd/MM/yyyy"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public errorControl As clsErrorControl = New clsErrorControl()
#End Region
    Private Sub FrmGateOut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetUserMgmtNew()
        AddNew()

        SettMCCBulkProcumentSecurityGateOut = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MCCBulkProcumentSecurityGateOut, clsFixedParameterCode.MCCBulkProcumentSecurityGateOut, Nothing)) = 1)
        If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing) = 1)) Then
            SettDateTimeFormat = "dd/MM/yyyy hh:mm tt"
        End If
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")

        If clsCommon.myLen(Me.Tag) > 0 Then
            loadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmGateOut)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Sub AddNew()
        fndGateOutNo.Value = ""
        txtGateOutDate.Text = ""
        txtTanker.Text = ""
        lblTankerNo.Text = ""
        txtMccCode.Text = ""
        lblMccCode.Text = ""
        txtLocationCode.Text = ""
        lblLocationName.Text = ""
        txt_Phone_No.Text = ""
        txtDriver.Text = ""
        txtRemarks.Text = ""

        fndDocNo.Value = ""
        dtpStartDateTime.CustomFormat = SettDateTimeFormat
        dtpStartDateTime.Value = clsCommon.GETSERVERDATE()
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
        fndDocNo.MyReadOnly = False
        IsNewEntry = True
        fndGateOutNo.Enabled = True
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        AddNew()
    End Sub
    Private Sub fndGateEntryNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndGateOutNo._MYValidating
        Dim qry As String = "select TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO,TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE,TSPL_MCC_TANKER_GATE_OUT.TANKER_NO,TSPL_MCC_TANKER_GATE_OUT.MCC_CODE,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_TANKER_GATE_OUT.PhoneNo,TSPL_MCC_TANKER_GATE_OUT.DriverName,TSPL_MCC_TANKER_GATE_OUT.Remarks " + Environment.NewLine +
        "From TSPL_MCC_TANKER_GATE_OUT " + Environment.NewLine +
        "left outer join TSPL_MCC_MASTER on  TSPL_MCC_MASTER.MCC_Code=TSPL_MCC_TANKER_GATE_OUT.MCC_CODE"
        Dim whrCls As String = " TSPL_MCC_TANKER_GATE_OUT.IS_POSTED=1 and TSPL_MCC_TANKER_GATE_OUT.IsCancel=0 and not exists(select 1 from TSPL_MCC_TANKER_GATE_OUT_SECURITY where TSPL_MCC_TANKER_GATE_OUT_SECURITY.Gate_Out_No=TSPL_MCC_TANKER_GATE_OUT.Gate_Out_No and TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No not in ('" + fndDocNo.Value + "'))"
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = whrCls & " and TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        fndGateOutNo.Value = clsCommon.ShowSelectForm("GtOtSecF", qry, "GATE_OUT_NO", whrCls, fndGateOutNo.Value, "GATE_OUT_NO", isButtonClicked)
        LoadGateOutData(fndGateOutNo.Value)
    End Sub
    Sub LoadGateOutData(ByVal strGateOutNo As String)
        Dim obj As clsMCCTankerGateOut = clsMCCTankerGateOut.GetData(strGateOutNo, NavigatorType.Current)
        If obj IsNot Nothing Then
            fndGateOutNo.Value = obj.GATE_OUT_NO
            txtGateOutDate.Text = clsCommon.GetPrintDate(obj.GATE_OUT_DATE, SettDateTimeFormat)
            txtTanker.Text = obj.TANKER_NO
            lblTankerNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name   from TSPL_vendor_master where Vendor_Code=(select Tanker_Transporter_Code  from TSPL_TANKER_MASTER where Tanker_No='" & txtTanker.Text & "' )"))
            txtMccCode.Text = obj.MCC_CODE
            lblMccCode.Text = obj.MCC_DESC
            txtLocationCode.Text = obj.LOCATION_CODE
            lblLocationName.Text = obj.LOCATION_DESC
            txtDriver.Text = clsCommon.myCstr(obj.DriverName)
            txt_Phone_No.Text = clsCommon.myCstr(obj.PhoneNo)
            txtRemarks.Text = clsCommon.myCstr(obj.Remarks)
        Else
            fndGateOutNo.Value = ""
            txtGateOutDate.Text = ""
            txtTanker.Text = ""
            lblTankerNo.Text = ""
            txtMccCode.Text = ""
            lblMccCode.Text = ""
            txtLocationCode.Text = ""
            lblLocationName.Text = ""
            txt_Phone_No.Text = ""
            txtDriver.Text = ""
            txtRemarks.Text = ""
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Function allowToSave() As Boolean
        Try
            If AllowFutureDateTransaction(dtpStartDateTime.Value, Nothing) = False Then
                dtpStartDateTime.Focus()
                Return False
            End If
            If clsCommon.myLen(fndGateOutNo.Value) <= 0 Then
                Throw New Exception("Please Select Gate out No")
                errorControl.SetError(fndGateOutNo, "Please Select Gate out No")
            Else
                errorControl.ResetError(fndGateOutNo)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Sub SaveData(ByVal isPost As Boolean)
        Try
            If allowToSave() Then
                Dim obj As clsMCCTankerGateOutSecurity = New clsMCCTankerGateOutSecurity()
                obj.Doc_No = fndDocNo.Value
                obj.Doc_Date = dtpStartDateTime.Value
                obj.GATE_OUT_NO = fndGateOutNo.Value
                clsMCCTankerGateOutSecurity.saveData(obj, IsNewEntry)
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                End If
                loadData(obj.Doc_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub loadData(ByVal str As String, ByVal navtype As NavigatorType)
        Dim obj As clsMCCTankerGateOutSecurity = clsMCCTankerGateOutSecurity.GetData(str, navtype)
        If obj IsNot Nothing Then
            AddNew()
            fndDocNo.Value = obj.Doc_No
            dtpStartDateTime.Value = obj.Doc_Date
            fndGateOutNo.Value = obj.GATE_OUT_NO
            LoadGateOutData(obj.GATE_OUT_NO)
            lblPending.Status = obj.IS_POSTED
            If obj.IS_POSTED = ERPTransactionStatus.Pending Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If
            btnSave.Text = "Update"
            btnDelete.Enabled = True
            fndDocNo.MyReadOnly = True
        Else
            AddNew()
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If deleteConfirm() Then
                    If clsMCCTankerGateOutSecurity.deleteData(fndDocNo.Value) Then
                        clsCommon.MyMessageBoxShow("Data Deleted Successfully")
                        AddNew()
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow("Please select Document No To delete")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If postConfirm() Then
                    clsMCCTankerGateOutSecurity.PostData(fndDocNo.Value)
                    clsCommon.MyMessageBoxShow("Successfully Posted")
                    loadData(fndDocNo.Value, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub frmGateOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
        End If
    End Sub
    Private Sub fndDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndDocNo._MYNavigator
        loadData(fndDocNo.Value, NavType)
    End Sub
    Private Sub fndDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Dim strwhrcls As String = String.Empty
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                strwhrcls = " TSPL_MCC_TANKER_GATE_OUT.LOCATION_CODE in (" & objCommonVar.strCurrUserLocations & ")"
            End If
        End If
        fndDocNo.Value = clsMCCTankerGateOutSecurity.getFinder(strwhrcls, fndDocNo.Value, isButtonClicked)
        If clsCommon.myLen(fndDocNo.Value) > 0 Then
            loadData(fndDocNo.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
End Class
