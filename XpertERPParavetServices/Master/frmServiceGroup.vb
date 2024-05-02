Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common

Public Class FrmServiceGroup
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt("SERVGROP")
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub AddNew()
        txtServiceGroupCode.Value = ""
        txtGroupName.Text = ""
        txtDesc.Text = ""
        txtServiceGroupCode.MyReadOnly = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        txtServiceGroupCode.Focus()
    End Sub
    Private Sub FrmServiceGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        AddNew()
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsServiceGroup = clsServiceGroup.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtServiceGroupCode.Value = obj.Service_Group_Code
            txtGroupName.Text = obj.Service_Group_Name
            txtDesc.Text = obj.Service_Group_Desc
            txtServiceGroupCode.MyReadOnly = True
            btnSave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtServiceGroupCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtServiceGroupCode.Value)) > 30 Then
                myMessages.blankValue(Me, "Code", Me.Text)

                txtServiceGroupCode.Focus()
                txtServiceGroupCode.Select()
                Errorcontrol.SetError(txtServiceGroupCode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtServiceGroupCode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtGroupName.Text)) <= 0 Then
                myMessages.blankValue(Me, "Group Name", Me.Text)

                txtGroupName.Focus()
                txtGroupName.Select()
                Errorcontrol.SetError(txtGroupName, "Group Name")
                Return False
            Else
                Errorcontrol.ResetError(txtGroupName)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtDesc.Text)) <= 0 Then
                myMessages.blankValue(Me, "Description", Me.Text)

                txtDesc.Focus()
                txtDesc.Select()
                Errorcontrol.SetError(txtDesc, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtDesc)
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsServiceGroup()
                obj.Service_Group_Code = txtServiceGroupCode.Value
                obj.Service_Group_Name = txtGroupName.Text
                obj.Service_Group_Desc = txtDesc.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Service_Group_Code) from TSPL_Paravet_Service_Group WHERE Service_Group_Code ='" + obj.Service_Group_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsServiceGroup.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Service_Group_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtServiceGroupCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Code ('" + txtServiceGroupCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_Paravet_Service_Group WHERE Service_Group_Code='" + txtServiceGroupCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Private Sub txtServiceGroupCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtServiceGroupCode._MYNavigator
        Try
            LoadData(txtServiceGroupCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'select count(Service_Group_Code) from TSPL_Paravet_Service_Group WHERE Service_Group_Code
    Private Sub txtServiceGroupCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtServiceGroupCode._MYValidating
        Dim str As String = "select count(*) from TSPL_Paravet_Service_Group where Service_Group_Code ='" + txtServiceGroupCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtServiceGroupCode.MyReadOnly = False
        Else
            txtServiceGroupCode.MyReadOnly = True
        End If

        If txtServiceGroupCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Service_Group_Code As [Code],Service_Group_Name as [Name],Service_Group_Desc As [Description] from TSPL_Paravet_Service_Group "
            txtServiceGroupCode.Value = clsCommon.ShowSelectForm("TSPL_Paravet_Service_Group", qry, "Code", "", txtServiceGroupCode.Value, "TSPL_Paravet_Service_Group.Service_Group_Code", isButtonClicked)
            If clsCommon.myLen(txtServiceGroupCode.Value) > 0 Then
                Dim objOT As clsServiceGroup
                objOT = clsServiceGroup.GetData(txtServiceGroupCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtServiceGroupCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub FrmServiceGroup_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim str As String
        str = " select Service_Group_Code as [Service Group Code], Service_Group_Name as [Service Group Name],Service_Group_Desc as [Service Name] from TSPL_Paravet_Service_Group "
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Service Group Code", "Service Group Name", "Service Name") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsServiceGroup()
                    Dim strServiceGroupCode As String = clsCommon.myCstr(grow.Cells("Service Group Code").Value)
                    Dim strServiceGroupName As String = clsCommon.myCstr(grow.Cells("Service Group Name").Value)
                    Dim strServiceName As String = clsCommon.myCstr(grow.Cells("Service Name").Value)

                    linno += 1
                    If clsCommon.myLen(strServiceGroupCode) <= 0 Then
                        Throw New Exception("Service Group Code should not be left blankat line no. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strServiceGroupCode) > 30 Then
                        Throw New Exception("Please check ! length of Service Group Code not greter then 30 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strServiceGroupName) <= 0 Then
                        Throw New Exception("Service Group Name should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strServiceName) <= 0 Then
                        Throw New Exception("Service Name should not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strServiceName) > 0 Then
                        Dim NewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Paravet_Service_Group where Service_Group_Code='" + strServiceGroupCode + "' ")
                        If NewEntry <= 0 Then
                            Dim Values As String = clsDBFuncationality.getSingleValue(" Select count(*) from TSPL_Paravet_Service_Group where Service_Group_Name='" & strServiceGroupName & "' and Service_Group_Desc='" & strServiceName & "'  ")
                            If Values > 0 Then
                                Throw New Exception("Service Name already exist particular Group ,Line No " + clsCommon.myCstr(linno) + ".")
                            End If
                        End If
                    End If

                    If clsCommon.myLen(strServiceGroupCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Paravet_Service_Group where Service_Group_Code='" + strServiceGroupCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True
                    End If
                    obj.Service_Group_Code = strServiceGroupCode
                    obj.Service_Group_Name = strServiceGroupName
                    obj.Service_Group_Desc = strServiceName
                    clsServiceGroup.SaveData(obj, IsNewEntry)
                    'LoadData(obj.Service_Code, NavigatorType.Current)
                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
End Class
