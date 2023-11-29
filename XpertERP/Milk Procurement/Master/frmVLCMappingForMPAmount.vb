Imports common

Public Class frmVLCMappingForMPAmount
    Inherits FrmMainTranScreen


    Private Sub frmVLCMappingForMPAmount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
    End Sub

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.VLCMappingForMPAmount)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SavingData()
    End Sub

    Sub SavingData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.VLCMappingForMPAmount, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                clsVLCMappingForMPMilkAmount.SaveData(txtMCC.Value, txtVLC.arrValueMember)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If txtVLC.arrValueMember Is Nothing OrElse txtVLC.arrValueMember.Count <= 0 Then
            Throw New Exception("Please select at least one VLC")
        End If
        Return True
    End Function

    Private Sub txtMCC__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtMCC._MYNavigator
        Try
            LoadData(txtMCC.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = "select MCC_Code,MCC_NAME  from TSPL_MCC_MASTER "
        Dim whrClas As String = ""
        LoadData(clsCommon.ShowSelectForm("VLCMPA", qry, "MCC_Code", whrClas, txtMCC.Value, "MCC_Code", isButtonClicked), NavigatorType.Current)
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim strMCC As String = ""
            txtVLC.arrValueMember = clsVLCMappingForMPMilkAmount.GetData(strCode, NavTyep, strMCC)
            txtMCC.Value = strMCC
            txtRoute.arrValueMember = Nothing
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                Dim qry As String = "select distinct Route_Code from TSPL_VLC_MASTER_HEAD where VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim arr As New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        arr.Add(clsCommon.myCstr(dr("Route_Code")))
                    Next
                    txtRoute.arrValueMember = arr
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where MCC_Code='" + txtMCC.Value + "'"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCMARoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, Nothing)
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                qry = "select VLC_Code from TSPL_VLC_MASTER_HEAD where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                txtVLC.arrValueMember = Nothing
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim arr As New ArrayList
                    For Each dr As DataRow In dt.Rows
                        arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                    Next
                    txtVLC.arrValueMember = arr
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            If txtRoute.arrValueMember Is Nothing OrElse txtRoute.arrValueMember.Count <= 0 Then
                txtRoute.Focus()
                Throw New Exception("Please select at least route")
            End If
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("VLCMAVLC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmVLCMappingForMPAmount_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SavingData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            clsVLCMappingForMPMilkAmount.DeleteData(txtMCC.Value)
            clsCommon.MyMessageBoxShow(Me, "Data Deleted successfully", Me.Text)
            LoadData("", NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub
End Class