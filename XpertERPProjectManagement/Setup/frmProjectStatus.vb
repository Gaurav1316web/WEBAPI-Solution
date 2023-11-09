Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class FrmProjectStatus
    Inherits FrmMainTranScreen
    Dim strQuery As String
    Dim strColumn As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub FrmProjectStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub FrmProjectStatus_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub


    Private Sub FrmProjectStatus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New ")
        lblUser.Text = objCommonVar.CurrentUserCode
        lblDate.Text = clsCommon.GETSERVERDATE()
        LoadProjectStatus()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmProjectStatus)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Function AllowToSave() As Boolean
        If  clsCommon.myLen(fndProject.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Project Code")
            fndProject.Focus()
            Return False
        ElseIf clsCommon.myLen(ddlProjStatus.Text) = 0 Then
            clsCommon.MyMessageBoxShow("Please enter Status")
            ddlProjStatus.Focus()
            Return False
        End If
        Return True
    End Function
    Sub LoadProjectStatus()
        ddlProjStatus.DataSource = clsProjectStatus.GetProjectStatus
        ddlProjStatus.DisplayMember = "Name"
        ddlProjStatus.ValueMember = "Code"
        ddlProjStatus.SelectedIndex = -1
    End Sub
    Function CheckPermision() As Boolean
        Try
            strColumn = CurrentStatus()
            Dim strPermission As String = clsDBFuncationality.getSingleValue("Select " & strColumn & " from tspl_user_approval where  user_code='" & objCommonVar.CurrentUserCode & "' ")
            If clsCommon.CompairString(strPermission, "N") = CompairStringResult.Equal Then
                clsCommon.MyMessageBoxShow("You have no permission to change " & ddlProjStatus.Text & " Status")
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Function CurrentStatus() As String
        Try
            If ddlProjStatus.Text = "Estimated" Then
                strColumn = "Created"
            ElseIf ddlProjStatus.Text = "Approve" Then
                strColumn = "Approved"
            ElseIf ddlProjStatus.Text = "Open" Then
                strColumn = "Opened"
            ElseIf ddlProjStatus.Text = "On Hold" Then
                strColumn = "OnHold"
            ElseIf ddlProjStatus.Text = "Close" Then
                strColumn = "Closed"
            ElseIf ddlProjStatus.Text = "Complete" Then
                strColumn = "Completed"
            ElseIf ddlProjStatus.Text = "InActive" Then
                strColumn = "InActive"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return strColumn
    End Function
    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating

        'strQuery = "select PROJECT_CODE,SPECIFICATION,PROJECT_STATUS,Cust_Code from TSPL_PJC_PROJECT "
        'fndProject.Value = clsCommon.ShowSelectForm("Project", strQuery, "PROJECT_CODE", "", fndProject.Value.ToString, "PROJECT_CODE", isButtonClicked)
        fndProject.Value = clsProjectStatus.getFinder("", fndProject.Value, isButtonClicked)
        txtProjectdesc.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" & fndProject.Value & "'")
        ddlProjStatus.Text = clsDBFuncationality.getSingleValue("select PROJECT_STATUS from TSPL_PJC_PROJECT where PROJECT_CODE='" & fndProject.Value & "'")
        lblUser.Text = objCommonVar.CurrentUserCode
        lblDate.Text = clsCommon.GETSERVERDATE()
    End Sub
   

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If AllowToSave() = True AndAlso CheckPermision() = True Then
                strColumn = CurrentStatus()
                If clsCommon.CompairString(strColumn, "Opened") = CompairStringResult.Equal Then
                    strQuery = "Update tspl_pjc_project set PROJECT_STATUS='" & ddlProjStatus.Text & "', Open_By='" & objCommonVar.CurrentUserCode & "',Open_Date_Actual='" & clsCommon.GetPrintDate(lblDate.Text, "dd/MMM/yyyy") & "' where project_Code='" & fndProject.Value & "' "
                ElseIf clsCommon.CompairString(strColumn, "Approved") = CompairStringResult.Equal Then
                    strQuery = "Update tspl_pjc_project set PROJECT_STATUS='" & ddlProjStatus.Text & "', Approve_By='" & objCommonVar.CurrentUserCode & "',Approve_Date_Actual='" & clsCommon.GetPrintDate(lblDate.Text, "dd/MMM/yyyy") & "' where project_Code='" & fndProject.Value & "' "
                ElseIf clsCommon.CompairString(strColumn, "OnHold") = CompairStringResult.Equal Then
                    strQuery = "Update tspl_pjc_project set PROJECT_STATUS='" & ddlProjStatus.Text & "', On_Hold_By='" & objCommonVar.CurrentUserCode & "',On_Hold_Date='" & clsCommon.GetPrintDate(lblDate.Text, "dd/MMM/yyyy") & "' where project_Code='" & fndProject.Value & "' "
                ElseIf clsCommon.CompairString(strColumn, "Closed") = CompairStringResult.Equal Then
                    strQuery = "Update tspl_pjc_project set PROJECT_STATUS='" & ddlProjStatus.Text & "', Close_By='" & objCommonVar.CurrentUserCode & "',Close_Date='" & clsCommon.GetPrintDate(lblDate.Text, "dd/MMM/yyyy") & "' where project_Code='" & fndProject.Value & "' "
                ElseIf clsCommon.CompairString(strColumn, "Completed") = CompairStringResult.Equal Then
                    strQuery = "Update tspl_pjc_project set PROJECT_STATUS='" & ddlProjStatus.Text & "', Completed_By='" & objCommonVar.CurrentUserCode & "',Completed_Date_Actual='" & clsCommon.GetPrintDate(lblDate.Text, "dd/MMM/yyyy") & "' where project_Code='" & fndProject.Value & "' "
                ElseIf clsCommon.CompairString(strColumn, "InActive") = CompairStringResult.Equal Then
                    strQuery = "Update tspl_pjc_project set PROJECT_STATUS='" & ddlProjStatus.Text & "', Inactive_By='" & objCommonVar.CurrentUserCode & "',Inactive_Date='" & clsCommon.GetPrintDate(lblDate.Text, "dd/MMM/yyyy") & "' where project_Code='" & fndProject.Value & "' "
                End If
                clsDBFuncationality.ExecuteNonQuery(strQuery)
                clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        Reset()
    End Sub
    Sub Reset()
        fndProject.Value = Nothing
        LoadProjectStatus()
        txtProjectdesc.Text = ""
        lblUser.Text = ""
        lblDate.Text = ""
    End Sub
End Class
