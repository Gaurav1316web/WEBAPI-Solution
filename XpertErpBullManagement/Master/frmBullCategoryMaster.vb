Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine

Public Class frmBullCategoryMaster
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim Sqlqry As String = "select Code as Code,Name from TSPL_BULL_CATEGORY_MASTER where Code ='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code as Code,Name from TSPL_BULL_CATEGORY_MASTER"
            txtCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", whrClas, txtCode.Value, "TSPL_BULL_CATEGORY_MASTER.Code asc", isButtonClicked, Nothing)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = False
            btnsave.Enabled = True
            btnsave.Text = "Save"
            txtCode.MyReadOnly = False

            Dim obj As clsBullCategoryMaster = clsBullCategoryMaster.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtname.Text = obj.Name

                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtname.Text = ""

        txtCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False

        isNewEntry = True

        txtname.Focus()
        txtname.Select()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                ErrorControl.SetError(txtCode, "Code not found to delete.")
                Throw New Exception("Code not found to delete")
            Else
                ErrorControl.ResetError(txtCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsBullCategoryMaster.DeleteData(txtCode.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBullCategoryMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
            End If
            Dim obj As New clsBullCategoryMaster()
            obj.Code = txtCode.Value
            obj.Name = txtname.Text.Replace("'", "`")

            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtname.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Fill Name.")
            txtname.Focus()
            txtname.Select()
            ErrorControl.SetError(txtname, "Fill Name")
        Else
            ErrorControl.ResetError(txtname)
        End If

        Return True
    End Function

End Class