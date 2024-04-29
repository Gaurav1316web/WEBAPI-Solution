
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine

Public Class frmBullMovementType
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()


    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Dim Sqlqry As String = "select Code,Name,Peridocity from TSPL_BULL_MOVEMENT_TYPE where code='" + fndCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            fndCode.MyReadOnly = False
        Else
            fndCode.MyReadOnly = True
        End If
        If fndCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Name,Peridocity from TSPL_BULL_MOVEMENT_TYPE"
            fndCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", whrClas, fndCode.Value, "TSPL_BULL_MOVEMENT_TYPE.Code asc", isButtonClicked, Nothing)
            LoadData(fndCode.Value, NavigatorType.Current)
        End If
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = False
            btnsave.Enabled = True
            btnsave.Text = "Save"
            fndCode.MyReadOnly = False

            Dim obj As clsBullMovementType = clsBullMovementType.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                fndCode.Value = obj.Code
                txtname.Text = obj.Name
                txtPeriodcity.Text = obj.Peridocity
                fndCode.MyReadOnly = True
                'btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                'AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtname.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Fill Name.")
            txtname.Focus()
            txtname.Select()
            txtPeriodcity.Focus()
            txtPeriodcity.Select()

            'ErrorControl.SetError(txtname, "Fill Name")
        Else
            'ErrorControl.ResetError(txtname)
        End If

        Return True
    End Function
    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBullMovementType, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
            End If
            Dim obj As New clsBullMovementType()
            obj.Code = fndCode.Value
            obj.Name = txtname.Text.Replace("'", "`")
            'obj.Type = txtType.Text

            obj.Peridocity = txtPeriodcity.Text
            If IsNumeric(txtPeriodcity.Text) Then
                ' Convert the input to a number
                obj.Peridocity = clsCommon.myCdbl(txtPeriodcity.Text)
            Else
                ' Display an error message if the input is not a valid number
                clsCommon.MyMessageBoxShow("Please enter a valid number for Days.", Me.Text)
                txtPeriodcity.Focus()
                txtPeriodcity.Text = ""
                Exit Sub
            End If
            If (obj.SaveData(obj, isNewEntry)) Then
                clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
            End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Dim qry As String = "select count(*) from TSPL_BULL_MOVEMENT_TYPE"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code,Name from TSPL_BULL_MOVEMENT_TYPE"
        Else
            qry = "select '' as Code,'' as Name,'' AS Peridocity"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Code", "Name") Then
            Dim obj As New clsBullMovementType()

            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv_Import.Rows
                    'obj = New clsBullBreedMaster()

                    obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.Code) > 50 Then
                        Throw New Exception("Code has max. length 50 see at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If

                    obj.Name = clsCommon.myCstr(grow.Cells("Name").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.Name) <= 0 Then
                        Throw New Exception("Fill name at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Name) > 200 Then
                        obj.Name = obj.Name.Substring(0, 200)
                    End If
                    obj.Peridocity = clsCommon.myCstr(grow.Cells("Peridocity").Value).Replace("'", "`")

                    If clsCommon.myLen(obj.Peridocity) > 200 Then
                        obj.Peridocity = obj.Peridocity.Substring(0, 200)
                    End If
                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_BULL_MOVEMENT_TYPE where code='" + obj.Code + "'")
                    isNewEntry = True
                    If qry > 0 Then
                        isNewEntry = False
                    End If

                    If (obj.SaveData(obj, isNewEntry)) Then

                    End If
                    counter += 1
                Next

                clsCommon.ProgressBarHide()

                If counter >= 1 Then
                    clsCommon.MyMessageBoxShow("Data transfer successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("No data found to transfer", Me.Text)
                End If


            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If

        isNewEntry = oldNewentry
        Me.Controls.Remove(gv_Import)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                fndCode.Focus()
                fndCode.Select()
                ErrorControl.SetError(fndCode, "Code not found to delete.")
                Throw New Exception("Code not found to delete")
            Else
                ErrorControl.ResetError(fndCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsBullTestParameter.DeleteData(fndCode.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub AddNew()
        fndCode.Value = ""
        txtname.Text = ""
        txtPeriodcity.Text = ""

        fndCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False

        isNewEntry = True

        txtname.Focus()
        txtname.Select()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_MOVEMENT_TYPE where Code='" + fndCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class