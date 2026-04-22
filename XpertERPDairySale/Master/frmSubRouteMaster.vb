Imports common
Public Class frmSubRouteMaster
    Private Sub frmSubRouteMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ''
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Try
                If isButtonClicked Then
                    Dim qry As String = "select Code,Name from TSPL_Sub_Route_MASTER "
                    txtCode.Value = clsCommon.ShowSelectForm("@Route", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub AddNew()
        Try
            txtCode.Value = Nothing
            txtRouteSubName.Text = Nothing
            fndRouteCode.Value = Nothing
            lblRouteName.Text = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                Dim obj As New clsSubRouteMaster()
                obj.Code = txtCode.Value
                obj.Name = txtRouteSubName.Text
                obj.RouteCode = fndRouteCode.Value
                If obj.SaveData(obj, False) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully .", Me.Text)
                    LoadData(txtCode.Value, Nothing)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtRouteSubName.Text) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Name can't be blank !", Me.Text)
                Return False
            ElseIf clsCommon.myLen(fndRouteCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Route can't be blank !", Me.Text)
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Dim obj As New clsSubRouteMaster()
            obj = obj.GetData(strCode, NavType)
            If obj IsNot Nothing Then
                txtCode.Value = obj.Code
                txtRouteSubName.Text = obj.Name
                fndRouteCode.Value = obj.RouteCode
                lblRouteName.Text = obj.RouteName
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found !", Me.Text)
            End If
            obj = Nothing
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub fndRouteCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndRouteCode._MYValidating
        Try
            Dim qry As String = "select Route_No As Code,Route_Desc As Name from TSPL_Route_MASTER"
            fndRouteCode.Value = clsCommon.ShowSelectForm("@Route", qry, "Code", "", fndRouteCode.Value, "Code", isButtonClicked)
            lblRouteName.Text = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER Where Route_No='" & clsCommon.myCstr(fndRouteCode.Value) & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtCode.Value) > 0 AndAlso clsCommon.MyMessageBoxShow(Me, "Are you sure to delete ?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                Dim obj As New clsSubRouteMaster()
                If obj.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successully.", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class