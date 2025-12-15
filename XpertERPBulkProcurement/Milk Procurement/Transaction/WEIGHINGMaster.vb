Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class WEIGHINGMaster
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()

    Private Sub WEIGHINGMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Code", "varchar(30) Not NULL Primary Key")
        coll.Add("Name", "varchar(30) not null ")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "DateTime  Not NULL")
        coll.Add("Modified_By", "varchar(12)  Not NULL")
        coll.Add("Modified_Date", "datetime  Not NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_WEIGHING_MASTER", coll, "", False, False, Nothing, Nothing, Nothing, False)
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                '    If MyBase.isModifyonPasswordFlag Then
                '        If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBullRating, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                '        Else
                '            Return
                '        End If
                '    End If
                'End If
                Dim obj As New clsWEIGHINGMaster()
                obj.Code = txtCode.Value
                obj.Name = txtname.Text.Replace("'", "`")

                If (obj.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            txtCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Code can't be blank", Me.Text)
            Exit Function
            Return False
        End If
        Return True
    End Function

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
                If clsWEIGHINGMaster.DeleteData(txtCode.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
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

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim Sqlqry As String = "select Code,Name from TSPL_WEIGHING_MASTER where code='" + txtCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Name from TSPL_WEIGHING_MASTER"
            txtCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", whrClas, txtCode.Value, "TSPL_WEIGHING_MASTER.Code asc", isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_WEIGHING_MASTER where Code='" + txtCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = "select count(*) from TSPL_WEIGHING_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code,Name from TSPL_WEIGHING_MASTER"
        Else
            qry = "select '' as Code,'' as Name"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = False
            btnsave.Enabled = True
            btnsave.Text = "Save"
            txtCode.MyReadOnly = False

            Dim obj As clsWEIGHINGMaster = clsWEIGHINGMaster.GetData(strCode, NavTyep)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv_Import As New UserControls.MyRadGridView
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Code", "Name") Then
            Dim obj As New clsWEIGHINGMaster()

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
                        Throw New Exception("Fill description at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Name) > 200 Then
                        obj.Name = obj.Name.Substring(0, 200)
                    End If

                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_WEIGHING_MASTER where code='" + obj.Code + "'")
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
                    clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to transfer", Me.Text)
                End If


            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If

        isNewEntry = oldNewentry
        Me.Controls.Remove(gv_Import)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class