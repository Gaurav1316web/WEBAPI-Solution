Imports common
Imports System.Xml

Public Class FrmPOSGRoupMaster
    Inherits FrmMainTranScreen
#Region "Variables"

#End Region
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.FrmPOSGRoupMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qst As String = "SELECT COUNT(GROUP_CODE) FROM TSPL_POS_GROUP_MASTER "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        btnSave.Enabled = True
        btnDelete.Enabled = True
        Dim obj As New clsPOSGroupMaster()
        obj = obj.GetData(strCode, NavTyep)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.GROUP_CODE) > 0 Then
            txtCode.Value = obj.GROUP_CODE
            txtdesc.Text = obj.DESCRIPTION
            If clsCommon.myLen(obj.DOC_DATE) > 0 Then
                dtDocDate.Value = obj.DOC_DATE
            End If
            txtLevel.Text = obj.LEVEL
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        Else
            FunReset()
        End If
        If obj Is Nothing OrElse clsCommon.myLen(txtCode.Value) <= 0 Then
            btnSave.Text = "Save"
            btnDelete.Enabled = False
        End If



    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Public Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsPOSGroupMaster()
                obj.GROUP_CODE = clsCommon.myCstr(txtCode.Value)
                obj.DESCRIPTION = clsCommon.myCstr(txtdesc.Text)
                obj.LEVEL = clsCommon.myCstr(txtLevel.Text)
                obj.DOC_DATE = clsCommon.GetPrintDate(dtDocDate.Text, "dd/MMM/yyyy")
                Dim IsNew As Boolean = IIf((clsCommon.CompairString(clsCommon.myCstr(txtCode.Value), "") = CompairStringResult.Equal), True, False)
                If (obj.SaveData(obj, IsNew)) Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully.")
                    obj.GetData(obj.GROUP_CODE, NavigatorType.Current)
                    txtCode.Value = obj.GROUP_CODE
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try


    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
                Exit Sub
            End If
            If (myMessages.deleteConfirm()) Then
                If (clsPOSGroupMaster.Delete(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    FunReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Me.Close()
    End Sub

    Private Sub FrmPOSGRoupMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FunReset()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "SELECT GROUP_CODE,[Description],[Level] FROM TSPL_POS_GROUP_MASTER "

        LoadData(clsCommon.ShowSelectForm("TSPL_POS_GROUP_MASTER", qry, "GROUP_CODE", "", txtCode.Value, "GROUP_CODE", isButtonClicked), NavigatorType.Current)
    End Sub

    Public Function AllowToSave() As Boolean
        If clsCommon.myLen(txtdesc.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Fill the Description")
            txtdesc.Focus()
            Return False
        End If
        If clsCommon.myLen(dtDocDate.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Fill the Doc Date")
            dtDocDate.Focus()
            Return False
        End If
        If clsCommon.CompairString(clsCommon.myCdbl(txtLevel.Value), 0) = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Level will be greater than 0.")
            txtLevel.Focus()
            Return False
        End If
        If clsCommon.myLen(txtLevel.Text) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Fill the Level")
            txtLevel.Focus()
            Return False
        End If
        Return True
    End Function

    Public Sub FunReset()
        txtCode.Value = Nothing
        txtdesc.Text = Nothing
        txtLevel.Text = Nothing
        dtDocDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
        btnSave.Text = "Save"
        btnDelete.Enabled = False
    End Sub
    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        FunReset()
    End Sub
End Class
