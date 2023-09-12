Imports common
Imports System.Data.SqlClient

Public Class frmBreakDownEntry
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim Errorcontrol As New clsErrorControl()
    Dim isInsideLoadData As Boolean = False
    Dim isCellvaluechanged As Boolean = False
#End Region

    Private Sub frmBreakDownEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.Focus()
            btnsave.Select()
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                 "TSPL_BREAK_DOWN_ENTRY ")
        End If
    End Sub

    Private Sub FunReset()
        isNewEntry = True
        txtdesc.Text = ""
        txtCode.Value = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtBreakDowncode.Value = ""
        txtBreakDownname.Text = ""
        txtHours.Text = 0
        dtpDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtstart_time.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy hh:mm tt")
        txtend_time.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MM/yyyy hh:mm tt")

        btnsave.Enabled = True
        btndelete.Enabled = False
        btnsave.Text = "Save"
        txtCode.MyReadOnly = False

        txtstart_time.Enabled = True
        txtend_time.Enabled = True

        RadPageView1.SelectedPage = RadPageViewPage1
        txtdesc.Focus()
        txtdesc.Select()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmProcessProductionLogSheet)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        'btnPrint.Enabled = MyBase.isPainting
    End Sub

    Private Sub frmBreakDownEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()

        ButtonToolTip.SetToolTip(btnNew, "Alt+N for new window")
        ButtonToolTip.SetToolTip(btnNew, "Alt+S for save data")
        ButtonToolTip.SetToolTip(btnNew, "Alt+D for delete data")
        ButtonToolTip.SetToolTip(btnNew, "Alt+C for close window")
        ButtonToolTip.SetToolTip(btnNew, "Alt+G for fill QC grid")

    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtBreakDowncode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtBreakDowncode.Focus()
                txtBreakDowncode.Select()
                Errorcontrol.SetError(txtBreakDowncode, "Select Break Down Code")
                Throw New Exception("Select Break Down Code")
            Else
                Errorcontrol.ResetError(txtBreakDowncode)
            End If

            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtLocation.Focus()
                txtLocation.Select()
                Errorcontrol.SetError(txtLocation, "Select Location Code")
                Throw New Exception("Select Location Code")
            Else
                Errorcontrol.ResetError(txtLocation)
            End If

            If clsCommon.myLen(txtstart_time.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtstart_time.Focus()
                txtstart_time.Select()
                Errorcontrol.SetError(txtstart_time, "Fill start time")
                Throw New Exception("Fill start time")
            Else
                Errorcontrol.ResetError(txtstart_time)
            End If

            If clsCommon.myLen(txtend_time.Text) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtend_time.Focus()
                txtend_time.Select()
                Errorcontrol.SetError(txtend_time, "Fill end time")
                Throw New Exception("Fill end time")
            Else
                Errorcontrol.ResetError(txtend_time)
            End If

            If clsCommon.myCDate(txtend_time.Text) < clsCommon.myCDate(txtstart_time.Text) Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtend_time.Focus()
                txtend_time.Select()
                Errorcontrol.SetError(txtend_time, "End time should be greater than Start time")
                Throw New Exception("End time should be greater than Start time")
            Else
                Errorcontrol.ResetError(txtend_time)
            End If

            'If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
            '    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select doc_no from TSPL_BREAK_DOWN_ENTRY where TSPL_BREAK_DOWN_ENTRY.Location_Code='" + txtLocation.Value + "' and convert(date,doc_date,103)=convert(date,'" + dtpDate.Value + "',103)"))
            '    If clsCommon.myLen(strDocNo) > 0 Then
            '        Throw New Exception("Break Down Entry [" + strDocNo + "] is already exist.")
            '    End If
            'End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
    End Function
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub SaveData()
        Try
            Dim obj As New clsProductionBreakDown()

            obj.Doc_no = clsCommon.myCstr(txtCode.Value)
            obj.Doc_Date = clsCommon.myCDate(dtpDate.Text)
            obj.Description = clsCommon.myCstr(txtdesc.Text)
            obj.Break_Down_Code = clsCommon.myCstr(txtBreakDowncode.Value)
            obj.LOCATION_CODE = clsCommon.myCstr(txtLocation.Value)
            obj.start_time = clsCommon.myCDate(txtstart_time.Text)
            obj.end_time = clsCommon.myCDate(txtend_time.Text)


            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsProductionBreakDown.SaveData(txtCode.Value, obj, isNewEntry, trans) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If

                txtCode.Value = obj.Doc_no
                LoadData(txtCode.Value, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Select first Doc no.")
                Throw New Exception("Select first Doc no.")
            Else
                Errorcontrol.ResetError(txtCode)
            End If


            If myMessages.deleteConfirm() Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsProductionBreakDown.DeleteData(txtCode.Value, trans) Then
                    myMessages.delete()
                    FunReset()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As clsProductionBreakDown = clsProductionBreakDown.GetData(strCode, NavType)

            isNewEntry = True
            isInsideLoadData = False
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_no) > 0 Then
                isInsideLoadData = True
                isNewEntry = False
                txtCode.Value = obj.Doc_no
                dtpDate.Text = obj.Doc_Date
                txtdesc.Text = obj.Description
                txtBreakDowncode.Value = obj.Break_Down_Code
                txtBreakDownname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select name from TSPL_BREAK_DOWN_MASTER where code='" + obj.Break_Down_Code + "'"))
                txtLocation.Value = obj.LOCATION_CODE
                lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
                txtstart_time.Text = obj.start_time
                txtend_time.Text = obj.end_time
                'Dim startTime As DateTime = obj.start_time
                'Dim endTime As DateTime = obj.end_time
                'Dim duration As TimeSpan = endTime - startTime
                'Dim durationHour As Double = duration.TotalHours
                'txtHours.Text = durationHour.ToString()
                Dim startTime As DateTime = obj.start_time
                Dim endTime As DateTime = obj.end_time
                Dim duration As TimeSpan = endTime - startTime
                Dim durationHour As Double = duration.TotalHours
                Dim roundedDurationHour As Double = Math.Round(durationHour, 2)
                txtHours.Text = roundedDurationHour.ToString()

                btnsave.Enabled = True
                btndelete.Enabled = True
                btnsave.Text = "Update"
                txtCode.MyReadOnly = True
                'btngo.Enabled = False
                txtstart_time.Enabled = True
                txtend_time.Enabled = True
            Else
                FunReset()
            End If
            isInsideLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isInsideLoadData = False
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_BREAK_DOWN_ENTRY where doc_no='" + clsCommon.myCstr(txtCode.Value) + "'"

        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        Dim WhrCls As String = Nothing
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    Dim WhrCls As String = "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        'End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls = " TSPL_BREAK_DOWN_ENTRY.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly Or isButtonClicked Then
            txtCode.Value = clsProductionBreakDown.GetFinder(WhrCls, txtCode.Value, isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                FunReset()
            End If
        Else
            FunReset()
        End If

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub txtBreakDowncode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBreakDowncode._MYValidating
        Dim qry As String = "select Code,Name from TSPL_BREAK_DOWN_MASTER "
        txtBreakDowncode.Value = clsCommon.ShowSelectForm("BRCFND", qry, "Code", " ", txtBreakDowncode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtBreakDowncode.Value) > 0 Then
            txtBreakDownname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BREAK_DOWN_MASTER where Code='" + txtBreakDowncode.Value + "' "))
        Else
            txtBreakDownname.Text = ""
        End If
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name] from TSPL_Location_MASTER"
        Dim WhrCls As String = " TSPL_LOCATION_MASTER.IsMainPlant='0' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("MulBDELocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))

    End Sub

    Private Sub txtstart_time_ValueChanged(sender As Object, e As EventArgs) Handles txtstart_time.ValueChanged
        'Dim startTime As DateTime = txtstart_time.Text
        'Dim endTime As DateTime = txtend_time.Text
        'Dim duration As TimeSpan = endTime - startTime
        'Dim durationHour As Double = duration.TotalHours
        'txtHours.Text = durationHour
        Dim startTime As DateTime = txtstart_time.Text
        Dim endTime As DateTime = txtend_time.Text
        Dim duration As TimeSpan = endTime - startTime
        Dim durationHour As Double = duration.TotalHours
        Dim roundedDurationHour As Double = Math.Round(durationHour, 2)
        txtHours.Text = roundedDurationHour.ToString()
    End Sub

    Private Sub txtend_time_ValueChanged(sender As Object, e As EventArgs) Handles txtend_time.ValueChanged
        'Dim startTime As DateTime = txtstart_time.Text
        'Dim endTime As DateTime = txtend_time.Text
        'Dim duration As TimeSpan = endTime - startTime
        'Dim durationHour As Double = duration.TotalHours
        'txtHours.Text = durationHour
        Dim startTime As DateTime = txtstart_time.Text
        Dim endTime As DateTime = txtend_time.Text
        Dim duration As TimeSpan = endTime - startTime
        Dim durationHour As Double = duration.TotalHours
        Dim roundedDurationHour As Double = Math.Round(durationHour, 2)
        txtHours.Text = roundedDurationHour.ToString()
    End Sub
End Class
