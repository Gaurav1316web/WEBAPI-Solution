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

    Private Sub frmConfigureSynchronization_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        gvTargetTables.MasterTemplate.ReadOnly = False
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadData()
    End Sub

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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

        obj = clsHostSettings.GetData("BioMatric")
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.HOST_ID) > 0) Then
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.HOST_ID) > 0) Then
                txtServerNameIPBioMatric.Tag = obj.HOST_ID
                txtServerNameIPBioMatric.Text = obj.SERVER_NAME_IP
                txtServerDBNameBioMatric.Text = obj.DATABASE_NAME
                txtServerSchemaBioMatric.Text = obj.SCHEMA_NAME
                txtServerUserIdBioMatric.Text = obj.DB_USER_ID
                txtServerPasswordBioMatric.Text = obj.DB_PWD
                txtServerRemarksBioMatric.Text = obj.REMARKS
            End If
        End If

        Dim strStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.MCCBioSyncDate, clsFixedParameterCode.MCCBioSyncDate, Nothing)
        If clsCommon.myLen(strStartDate) > 0 Then
            txtMCCBioDate.Value = clsCommon.myCDate(strStartDate)
        Else
            txtMCCBioDate.Value = clsCommon.GETSERVERDATE()
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





    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.frmConfigureSynchronization)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Sub funReset()

        txtServerNameIP.Tag = ""
        txtServerNameIP.Text = ""
        txtServerDBName.Text = ""
        txtServerSchema.Text = ""
        txtServerUserId.Text = ""
        txtServerPassword.Text = ""
        txtServerRemarks.Text = ""

        txtServerNameIPBioMatric.Tag = ""
        txtServerNameIPBioMatric.Text = ""
        txtServerDBNameBioMatric.Text = ""
        txtServerSchemaBioMatric.Text = ""
        txtServerUserIdBioMatric.Text = ""
        txtServerPasswordBioMatric.Text = ""
        txtServerRemarksBioMatric.Text = ""

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
        cm.CommandText = "select top 1 * from INFORMATION_SCHEMA.TABLES "
        cm.Connection = conn
        Dim status As Boolean = False
        Try
            conn.Open()
            conn.Close()
            clsCommon.MyMessageBoxShow(Me, "Test connection succeed", Me.Text)
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return False
    End Function

    Private Sub btnTestServer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTestServer.Click
        Try
            Dim connStr As String = "Server=" & txtServerNameIP.Text & ";" & "Database=" & txtServerDBName.Text & ";" & "User Id=" & txtServerUserId.Text & ";" & "Password=" & txtServerPassword.Text
            If RadPageView1.SelectedPage Is RadPageViewPage2 Then
                connStr = "Server=" & txtServerNameIPBioMatric.Text & ";" & "Database=" & txtServerDBNameBioMatric.Text & ";" & "User Id=" & txtServerUserIdBioMatric.Text & ";" & "Password=" & txtServerPasswordBioMatric.Text
            End If
            TestConnection(connStr)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try



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

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = ""
            If clsCommon.myLen(txtServerNameIPBioMatric.Text) > 0 Then
                RadPageView1.SelectedPage = RadPageViewPage2
                If clsCommon.myLen(txtServerNameIPBioMatric.Text) <= 0 Then
                    txtServerNameIPBioMatric.Focus()
                    Throw New Exception("Bio Matric Server Name/IP")
                ElseIf clsCommon.myLen(txtServerDBNameBioMatric.Text) <= 0 Then
                    txtServerDBNameBioMatric.Focus()
                    Throw New Exception("Bio Matric Server Database Name")
                ElseIf clsCommon.myLen(txtServerSchemaBioMatric.Text) <= 0 Then
                    txtServerSchemaBioMatric.Focus()
                    Throw New Exception("Bio Matric Server Schema Name")
                ElseIf clsCommon.myLen(txtServerUserIdBioMatric.Text) <= 0 Then
                    txtServerUserIdBioMatric.Focus()
                    Throw New Exception("Bio Matric Server Database User Id")
                ElseIf clsCommon.myLen(txtServerPasswordBioMatric.Text) <= 0 Then
                    txtServerPasswordBioMatric.Focus()
                    Throw New Exception("Bio Matric Server Database Password")
                End If

                Dim obj As New clsHostSettings
                obj.SERVER_TYPE = "BioMatric"
                obj.SERVER_NAME_IP = txtServerNameIPBioMatric.Text
                obj.DATABASE_NAME = txtServerDBNameBioMatric.Text
                obj.SCHEMA_NAME = txtServerSchemaBioMatric.Text
                obj.DB_USER_ID = txtServerUserIdBioMatric.Text
                obj.DB_PWD = txtServerPasswordBioMatric.Text
                obj.REMARKS = txtServerRemarksBioMatric.Text
                obj.SaveData(obj, tran)

                qry = "delete from TSPL_BIOMETRIC_RAW_DATA where convert(date, In_Out_Date,103)>='" + clsCommon.GetPrintDate(txtMCCBioDate.Value, "dd/MMM/yyyy") + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
            End If



            qry = "update TSPL_BIOMETRIC_RAW_DATA set SYNC_STATUS=null where convert(date, In_Out_Date,103)>='" + clsCommon.GetPrintDate(txtMCCBioDate.Value, "dd/MMM/yyyy") + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, tran)
            clsFixedParameter.UpdateData(clsFixedParameterType.MCCBioSyncDate, clsFixedParameterCode.MCCBioSyncDate, clsCommon.GetPrintDate(txtMCCBioDate.Value, "dd/MMM/yyyy"), tran)

            tran.Commit()
            clsCommon.MyMessageBoxShow(Me, "MCC Bio Start Date successfully updated", Me.Text)
        Catch ex As Exception
            tran.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
