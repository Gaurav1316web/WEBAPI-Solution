Imports System.Data.SqlClient
Imports common
Imports Telerik
Public Class frmDeductionTypeMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isNewEntry As Boolean = False
#End Region

    Private Sub frmDeductionTypeMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Addnew()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.MyReadOnly = False
        txtDocumentNo.Value = ""
        txtDescription.Text = ""
        txtDescriptionHindi.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            txtDescription.Focus()
            Throw New Exception("Description can't be blank.")
        End If

        Return True
    End Function

    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDeductionTypeMaster()
                obj.Document_No = txtDocumentNo.Value
                obj.Description = txtDescription.Text
                obj.Description_Hindi = txtDescriptionHindi.Text

                If (obj.SaveData(obj, isNewEntry, Nothing)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsDeductionTypeMaster()
            obj = clsDeductionTypeMaster.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_No)) > 0) Then

                txtDocumentNo.Value = obj.Document_No
                txtDescription.Text = obj.Description
                txtDescriptionHindi.Text = obj.Description_Hindi
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsDeductionTypeMaster.DeleteData(txtDocumentNo.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Addnew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Dim str As String = "select count(*) from TSPL_DEDUCTION_TYPE_MASTER where Document_No ='" + txtDocumentNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtDocumentNo.MyReadOnly = False
        Else
            txtDocumentNo.MyReadOnly = True
        End If
        If txtDocumentNo.MyReadOnly OrElse isButtonClicked Then
            txtDocumentNo.Value = clsDeductionTypeMaster.getFinder("", txtDocumentNo.Value, isButtonClicked)
            If txtDocumentNo.Value <> "" Then
                LoadData(txtDocumentNo.Value, NavigatorType.Current)
            Else
                Addnew()
            End If
        End If

        'If clsCommon.myLen(txtDocumentNo) <= 0 Then
        '    clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
        'End If
        'txtDocumentNo.Value = clsDeductionTypeMaster.getFinder(txtDocumentNo.Value, isButtonClicked)
        'LoadData(txtDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

        'Try
        '    Dim qry As String = "select count(*) from TSPL_DEDUCTION_TYPE_MASTER where Document_No='" + txtDocumentNo.Value + "' "
        '    Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
        '    If count = 0 Then
        '        txtDocumentNo.MyReadOnly = False
        '    Else
        '        txtDocumentNo.MyReadOnly = True
        '    End If
        '    LoadData(txtDocumentNo.Value, NavType)
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub
End Class