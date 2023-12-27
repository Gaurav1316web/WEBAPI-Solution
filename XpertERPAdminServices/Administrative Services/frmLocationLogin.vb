Imports common
Imports System.Data.SqlClient

Public Class frmLocationLogin
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim dt As DataTable
    Dim count As Integer = 0
    Dim arrUser As New ArrayList()
    Dim arrSelectedUser As New ArrayList()
    Dim ButtonToolTip As New ToolTip()


    Private Sub frmLocationLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for Closing The Window")
        'arrUser = FrmUserMaster.GetSubbordinateUsers(objCommonVar.CurrentUserCode)
        SetUserMgmtNew()
        Reset()
        LoadUsers()
        LoadData()
        
    End Sub

    Private Sub SetUserMgmtNew()
        '--------richa Ticket no. BM00000003014 on 15/07/2014
        'MyBase.SetUserMgmt(clsUserMgtCode.frmLocationSetting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function

        End If
        '--------richa Ticket no. BM00000003014
        btnSave.Visible = MyBase.isModifyFlag
        '--------------------------------------------------
        'btnsave.Visible = MyBase.isModifyFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm()
    End Sub

    Sub closeForm()
        Me.Close()
    End Sub

    Public Sub LoadUsers()
        Dim qry As String = clsUserMaster.GetSubbordinateUsersQry(objCommonVar.CurrentUserCode)
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgUserLevel1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgUserLevel1.ValueMember = "User_Code"
        cbgUserLevel1.DisplayMember = "User_Name"
    End Sub

    Public Sub Reset()
        chkLocationLogin.Checked = False
        rdoUserAll.Enabled = False
        rdoUserSelect.Enabled = False
        rdoUserAll.IsChecked = False
        rdoUserSelect.IsChecked = False
        Dim lstUsers As New ArrayList()
        cbgUserLevel1.CheckedValue = lstUsers
        cbgUserLevel1.Enabled = False
    End Sub

    Sub LoadData()
        Try

            cbgUserLevel1.CheckedValue = clsLocationSetting.GetData()
            If cbgUserLevel1.CheckedValue.Count > 0 Then
                rdoUserSelect.IsChecked = True
                chkLocationLogin.Checked = True
                cbgUserLevel1.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim isSaved As Boolean = False
            If chkLocationLogin.Checked = False Then
                Dim qry As String = "delete from TSPL_LOCATION_SETTING"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
            Else
                If rdoUserSelect.IsChecked AndAlso cbgUserLevel1.CheckedValue.Count = 0 Then
                    Throw New Exception("Please Select Atleast 1 User ")
                End If

                Dim lstUsers As New List(Of String)
                If (rdoUserSelect.IsChecked) Then
                    For Each user As String In cbgUserLevel1.CheckedValue
                        If clsCommon.myLen(user) > 0 Then
                            lstUsers.Add(user)
                        End If
                    Next
                End If

                If (rdoUserAll.IsChecked) Then
                    For Each user As String In cbgUserLevel1.AllValue
                        If clsCommon.myLen(user) > 0 Then
                            lstUsers.Add(user)
                        End If
                    Next
                End If

                isSaved = clsLocationSetting.SaveUsersForLocationSettings(lstUsers)
                LoadData()
            End If
            If isSaved Then
                clsCommon.MyMessageBoxShow(Me, "Location Settings Saved Successfully", Me.Text)
            End If

           
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub chkLocationLogin_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationLogin.ToggleStateChanged
        If chkLocationLogin.Checked Then
            rdoUserAll.Enabled = True
            rdoUserSelect.Enabled = True
            cbgUserLevel1.Enabled = False
            
        Else
            rdoUserAll.Enabled = False
            rdoUserSelect.Enabled = False
            cbgUserLevel1.Enabled = False
        End If

    End Sub

    Private Sub rdoUserAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoUserAll.ToggleStateChanged
        cbgUserLevel1.Enabled = False
    End Sub

    Private Sub rdoUserSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdoUserSelect.ToggleStateChanged
        cbgUserLevel1.Enabled = True
    End Sub
End Class




