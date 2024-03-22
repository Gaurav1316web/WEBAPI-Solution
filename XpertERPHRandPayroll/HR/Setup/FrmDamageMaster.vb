'============Created by Shivani Tyagi============[BM00000006309]
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine
Public Class FrmDamageMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmDamageMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag

    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then

                Dim obj As New ClsDamageMaster()
                obj.Damage_Code = txtCode.Value
                obj.Damage_Description = txtDescription.Text
                obj.Damage_Type = cboDocType.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Damage_Code) from TSPL_HR_DAMAGE_MASTER where Damage_Code='" + obj.Damage_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsDamageMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)

                    LoadData(obj.Damage_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) > 30 Then
                myMessages.blankValue(Me, "Code", Me.Text)

                txtCode.Focus()
                txtcode.Select()
                Errorcontrol.SetError(txtcode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtcode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtDescription.Text)) > 150 Then
                myMessages.blankValue(Me, "Description", Me.Text)

                txtDescription.Focus()
                txtDescription.Select()
                Errorcontrol.SetError(txtDescription, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtDescription)
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "are you sure? do you want to delete this Code ('" + txtCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_HR_DAMAGE_MASTER WHERE Damage_Code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtDescription.Text = ""
        cboDocType.Text = "Damage"
        txtcode.MyReadOnly = False
        txtcode.Focus()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_HR_DAMAGE_MASTER where Damage_Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Damage_Code As [Code],Damage_Description As [Damage Description] from TSPL_HR_DAMAGE_MASTER"
            txtCode.Value = clsCommon.ShowSelectForm("DAM", qry, "Code", "", txtCode.Value, "TSPL_HR_DAMAGE_MASTER.Damage_Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Dim objOT As ClsDamageMaster
                objOT = ClsDamageMaster.GetData(txtCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub

    Private Sub RmImport_Click(sender As Object, e As EventArgs) Handles RmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Description", "Type") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsDamageMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    Dim strType As String = clsCommon.myCstr(grow.Cells("Type").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If


                    If clsCommon.myLen(strDescription) <= 0 Then
                        Throw New Exception("Description should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strType) <= 0 Then
                        Throw New Exception("Type should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_HR_DAMAGE_MASTER where Damage_Code='" + strCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Damage_Code = strCode
                    obj.Damage_Description = strDescription
                    obj.Damage_Type = strType
                    ClsDamageMaster.SaveData(obj, IsNewEntry)

                Next

                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception

                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RmExport_Click(sender As Object, e As EventArgs) Handles RmExport.Click
        Dim str As String
        str = "select Damage_Code as [Code],Damage_Description As [Description],Damage_Type as [Type]  from TSPL_HR_DAMAGE_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmDamageMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        AddNew()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsDamageMaster = ClsDamageMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            txtCode.Value = obj.Damage_Code
            txtDescription.Text = obj.Damage_Description
            cboDocType.Text = obj.Damage_Type
            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub
    Private Sub FrmDamageMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub
End Class