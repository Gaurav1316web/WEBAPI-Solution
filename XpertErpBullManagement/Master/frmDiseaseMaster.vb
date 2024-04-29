Imports common
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmDiseaseMaster
#Region "Variables"
    Dim Qry As String = ""
    Dim isNewEntry As Boolean = True
    Dim Check As Boolean = True
#End Region

    Private Sub frmDiseaseMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        fndCode.Value = Nothing
        txtDisease.Text = Nothing
        btnSave.Text = "Save"
        isNewEntry = True
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try
            Dim strCode As String = ""
            Qry = ""
            Qry = " Select Code,Name From TSPL_Disease_Master "
            strCode = clsCommon.ShowSelectForm("fndrcode", Qry, "Code", "", fndCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(strCode) > 0 Then
                LoadData(strCode, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try

            Dim obj As New clsDiseaseMaster()
            obj = clsDiseaseMaster.GetData(strCode, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                AddNew()
                isNewEntry = False
                fndCode.Value = obj.Code
                txtDisease.Text = obj.Name
                btnSave.Text = "Update"
            Else
                isNewEntry = True
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                Dim obj As New clsDiseaseMaster()
                obj.Code = fndCode.Value
                obj.Name = txtDisease.Text
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function AllowToSave() As Boolean
        If clsCommon.myLen(fndCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Code can't be black.", Me.Text)
            Check = False
            Exit Function
        End If
        If clsCommon.myLen(txtDisease.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Name can't be black.", Me.Text)
            txtDisease.Focus()
            Check = False
            Exit Function
        End If
        Return Check
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Are you sure to delete ?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.No Then
                Exit Sub
            End If

            If clsCommon.myLen(fndCode) > 0 Then
                Dim obj As New clsDiseaseMaster()
                If obj.DeleteData(fndCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successfully", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


End Class