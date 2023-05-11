'--23/05/2017--form Add By- Panch Raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class frmConfigureSynchronization
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"    
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub    
    Public Sub Save()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmConfigureSynchronization, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                clsSyncHeadTables.ConfigureSynchronization(fndLoc.Value, dtpStartDate.Value, chkLapseUnAvailed.Checked)
                clsCommon.MyMessageBoxShow("Configured Successfully")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        
    End Sub

    Sub LoadData()
        btnSave.Enabled = True
        'btnDelete.Enabled = True
        funReset()
        '' load client settings
        Dim obj As New clsHostSettings()
        obj = clsHostSettings.GetData("Server")
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.HOST_ID) > 0) Then
            btnSave.Text = "Save"           
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.HOST_ID) > 0) Then
                btnSave.Text = "Save"
                txtServerNameIP.Tag = obj.HOST_ID
                txtServerNameIP.Text = obj.SERVER_NAME_IP
                txtServerDBName.Text = obj.DATABASE_NAME
                txtServerSchema.Text = obj.SCHEMA_NAME
                txtServerUserId.Text = obj.DB_USER_ID
                txtServerPassword.Text = obj.DB_PWD
                'txtServerRetypePassword.Text = obj.DB_PWD
                txtServerRemarks.Text = obj.REMARKS

                '' update last Configuration
                FillLastConfig()
            End If
        End If
    End Sub
    Sub FillLastConfig()
        Dim qry As String = "select max(REINIT_DATE) as REINIT_DATE from TSPL_SYNC_REINIT where MCC_Code='" & fndLoc.Value & "'"
        Dim PrevConf As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        If clsCommon.myLen(PrevConf) <= 0 Then
            PrevConf = "Never Configured"
        End If
        txtPrevConfDate.Text = PrevConf
        dtpStartDate.Value = clsCommon.GETSERVERDATE
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtServerNameIP.Text) <= 0 Then
            myMessages.blankValue("Server Name/IP")
            txtServerNameIP.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerDBName.Text) <= 0 Then
            myMessages.blankValue("Server Database Name")
            txtServerDBName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerSchema.Text) <= 0 Then
            myMessages.blankValue("Server Schema Name")
            txtServerSchema.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerUserId.Text) <= 0 Then
            myMessages.blankValue("Server Database User Id")
            txtServerUserId.Focus()
            Return False
        ElseIf clsCommon.myLen(txtServerPassword.Text) <= 0 Then
            myMessages.blankValue("Server Database Password")
            txtServerPassword.Focus()
            Return False        
        End If
        Return True
    End Function


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funDelete()
    End Sub

    Sub funDelete()
        'Try
        '    If (myMessages.deleteConfirm()) Then
        '        If (clsHostSettings.DeleteData("Client")) AndAlso (clsHostSettings.DeleteData("Server")) Then
        '            common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
        '            funReset()
        '        End If
        '    End If
        'Catch ex As Exception
        '    myMessages.myExceptions(ex)
        'End Try

    End Sub

    Private Sub frmConfigureSynchronization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        gvTargetTables.MasterTemplate.ReadOnly = False       
        LoadData()

    End Sub
    
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.frmConfigureSynchronization)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag

        '--------------------------------------------------
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub funReset()
      
        '' clear server settings      
        txtServerNameIP.Tag = ""
        txtServerNameIP.Text = ""
        txtServerDBName.Text = ""
        txtServerSchema.Text = ""
        txtServerUserId.Text = ""
        txtServerPassword.Text = ""
        txtServerRemarks.Text = ""

        btnSave.Text = "Save"
        btnSave.Enabled = True

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmConfigureSynchronization_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            '    funDelete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Public Function TestConnection(ByVal connStr As String) As Boolean
        Dim cm As New SqlCommand
        Dim conn As New SqlConnection
        conn.ConnectionString = connStr
        cm.CommandText = "select Comp_Code from TSPL_COMPANY_MASTER "
        cm.Connection = conn
        Dim status As Boolean = False
        Try
            conn.Open()
            conn.Close()
            'status = cm.ExecuteNonQuery()
            clsCommon.MyMessageBoxShow("Test connection succeed")
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub btnTestServer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTestServer.Click
        Dim connStr As String = "Server=" & txtServerNameIP.Text & ";" & "Database=" & txtServerDBName.Text & ";" & "User Id=" & txtServerUserId.Text & ";" & "Password=" & txtServerPassword.Text
        If TestConnection(connStr) Then
            'clsHostSettings.AddLinkedServer(txtServerNameIP.Text, txtServerUserId.Text, txtServerPassword.Text)
        End If

    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on location_Code= mcc_Code " _
        & " and (loc_segment_Code in (" & arrLoc & ") or mcc_Code in (" & arrLoc & ")))xx "

        fndLoc.Value = clsCommon.ShowSelectForm("VSPPMCCFn", qry, "Code", "", fndLoc.Value, "", isButtonClicked)
        txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + fndLoc.Value + "'"))
        If clsCommon.myLen(fndLoc.Value) > 0 Then
            FillLastConfig()
        End If
    End Sub
End Class
