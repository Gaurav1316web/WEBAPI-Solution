Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Public Class frmBullShedMaster
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    Private WithEvents RadDropDownList1 As New RadDropDownList()

    Private Sub frmBullShedMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateComboBox()

    End Sub
    Private Sub PopulateComboBox()
        cmbArea.Items.Clear()
        cmbArea.Items.Add("m²")
        cmbArea.Items.Add("ft²")

        ' Set default selections
        cmbArea.SelectedIndex = 0 ' Default selection for m²
        ' You can set cmbArea.SelectedIndex = 1 for default selection of ft² if needed
    End Sub
    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Dim Sqlqry As String = "select Code,Name,Area from tspl_bull_shed_master where code='" + fndCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            fndCode.MyReadOnly = False
        Else
            fndCode.MyReadOnly = True
        End If
        If fndCode.MyReadOnly OrElse isButtonClicked Then
            Dim whrClas As String = ""
            Dim qry As String = "select Code,Name,Area from tspl_bull_shed_master"
            fndCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", "", fndCode.Value, "tspl_bull_shed_master.Code asc", isButtonClicked, Nothing)
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

            Dim obj As clsBullShedMaster = clsBullShedMaster.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                fndCode.Value = obj.Code
                txtname.Text = obj.Name
                'cmbArea.Text = obj.Area
                'cmbArea.Text = cmbArea.SelectedItem.ToString()
                If obj.Area = 1.0 Then

                    RadDropDownList1.SelectedValue = "m²"
                    cmbArea.Text = "m²"
                ElseIf obj.Area = 2.0 Then
                    RadDropDownList1.SelectedValue = "ft²"
                    cmbArea.Text = "ft²"
                End If
                'cmbArea.SelectedValue = obj.Area

                fndCode.MyReadOnly = True
                btndelete.Enabled = True
            Else
                'AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(fndCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Fill Code.")
        End If
        If clsCommon.myLen(txtname.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Fill Name.")
            txtname.Focus()
            txtname.Select()
            cmbArea.Focus()
            cmbArea.Select()

            'ErrorControl.SetError(txtname, "Fill Name")
        Else
            'ErrorControl.ResetError(txtname)
        End If

        Return True
    End Function
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBullTestParameter, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
            End If
            Dim obj As New clsBullShedMaster()
            obj.Code = fndCode.Value
            obj.Name = txtname.Text.Replace("'", "`")
            obj.Area = Me.cmbArea.SelectedValue
            If cmbArea.SelectedText = "m²" Then
                obj.Area = 1
            Else cmbArea.SelectedText = "ft²"
                obj.Area = 2
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
                If clsBullShedMaster.DeleteData(fndCode.Value) Then
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
        cmbArea.Text = ""
        'txtPeriodcity.Text = ""

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

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = "select count(*) from tspl_bull_shed_master"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code,Name,Area from tspl_bull_shed_master"
        Else
            qry = "select '' as Code,'' as Name"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Code", "Name", "Area") Then
            Dim obj As New clsBullTestParameter()

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

                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_bull_shed_master where code='" + obj.Code + "'")
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
End Class