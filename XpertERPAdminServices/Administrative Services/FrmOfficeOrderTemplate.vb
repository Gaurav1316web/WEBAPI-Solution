Imports System.Data.SqlClient
Imports common

Public Class FrmOfficeOrderTemplate
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isNewEntry As Boolean = True
#End Region

#Region "Button Events"

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

#End Region

#Region "Core Methods"

    Private Sub SaveData()
        Try
            Dim obj As New ClsOfficeOrderTemplate()
            obj.Code = txtCode.Value
            obj.Subject = rtbSubject.Rtf
            obj.Description = txtDescription.Text

            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()

            Try
                ClsOfficeOrderTemplate.SaveData(obj, tran)
                Dim AttachmentCount As Integer = clsDBFuncationality.getSingleValue(
                    "SELECT COUNT(1) FROM TSPL_ATTACHMENTS WHERE TransactionId='" & obj.Code & "'", tran)

                Dim sql As String = "UPDATE TSPL_NOTIFICATIONS SET Attachment_Count = " & AttachmentCount &
                                    " WHERE Document_No = '" & obj.Code & "'"
                clsDBFuncationality.ExecuteNonQuery(sql, tran)

                tran.Commit()
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)

            Catch ex As Exception
                tran.Rollback()
                Throw
            End Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Addnew()
            txtCode.MyReadOnly = True

            Dim obj As ClsOfficeOrderTemplate = ClsOfficeOrderTemplate.GetData(strCode, NavType, Nothing)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                isNewEntry = False
                txtCode.Value = obj.Code
                rtbSubject.Rtf = obj.Subject
                txtDescription.Text = obj.Description
            Else
                Addnew()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub Addnew()
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        rtbSubject.Clear()
        txtDescription.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_OFFICE_ORDER_TEMPLATE where Document_No='" + txtCode.Value + "'"

            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qst As String = "select count(*) from TSPL_OFFICE_ORDER_TEMPLATE where Document_No='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        Dim whrClas As String = ""
        Dim qry As String = "select Document_No as Code from TSPL_OFFICE_ORDER_TEMPLATE"
        LoadData(clsCommon.ShowSelectForm("", qry, "Code", "", txtCode.Value, "TSPL_OFFICE_ORDER_TEMPLATE.Document_No  ", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsOfficeOrderTemplate.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                    Addnew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub


#End Region

End Class

'Imports System.Data.SqlClient
'Imports common

'Public Class FrmOfficeOrderTemplate
'    Inherits FrmMainTranScreen

'#Region "Variables"
'    Dim isNewEntry As Boolean = True
'#End Region

'#Region "Button Events"

'    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
'        SaveData()
'    End Sub

'    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
'        Me.Close()
'    End Sub

'#End Region

'#Region "Core Methods"

'    Private Sub SaveData()
'        Try
'            Dim obj As New ClsOfficeOrderTemplate()
'            obj.Code = txtCode.Value

'            obj.Subject = rtbSubject.Rtf

'            obj.Description = txtDescription.Text

'            Dim tran As SqlTransaction = clsDBFuncationality.GetTransactin()

'            Try
'                ClsOfficeOrderTemplate.SaveData(obj, tran)

'                Dim AttachmentCount As Integer =
'                    clsDBFuncationality.getSingleValue(
'                    "SELECT COUNT(1) FROM TSPL_ATTACHMENTS WHERE TransactionId='" & obj.Code & "'", tran)

'                Dim sql As String =
'                    "UPDATE TSPL_NOTIFICATIONS SET Attachment_Count = " & AttachmentCount &
'                    " WHERE Document_No = '" & obj.Code & "'"

'                clsDBFuncationality.ExecuteNonQuery(sql, tran)

'                tran.Commit()
'                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)

'                LoadData(obj.Code, NavigatorType.Current)

'            Catch ex As Exception
'                tran.Rollback()
'                Throw
'            End Try

'        Catch ex As Exception
'            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
'        End Try
'    End Sub


'    Public Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
'        Try
'            Addnew()
'            txtCode.MyReadOnly = True

'            Dim obj As ClsOfficeOrderTemplate =
'                ClsOfficeOrderTemplate.GetData(strCode, NavType, Nothing)

'            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
'                isNewEntry = False
'                txtCode.Value = obj.Code

'                rtbSubject.Rtf = obj.Subject

'                txtDescription.Text = obj.Description
'            Else
'                Addnew()
'            End If

'        Catch ex As Exception
'            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
'        End Try
'    End Sub


'    Public Sub Addnew()
'        txtCode.MyReadOnly = False
'        txtCode.Value = Nothing
'        txtCode.Focus()

'        rtbSubject.Clear()
'        txtDescription.Text = ""

'        btnSave.Text = "Save"
'        btnSave.Enabled = True
'        btnDelete.Enabled = True
'    End Sub


'    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
'        DeleteData()
'    End Sub


'    Sub DeleteData()
'        Try
'            If (myMessages.deleteConfirm()) Then
'                If (ClsOfficeOrderTemplate.DeleteData(txtCode.Value)) Then
'                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
'                    Addnew()
'                End If
'            End If
'        Catch ex As Exception
'            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
'        End Try
'    End Sub

'    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

'    End Sub

'    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType)

'    End Sub

'    Private Sub btnClose_Click_1(sender As Object, e As EventArgs)

'    End Sub

'#End Region

'End Class


