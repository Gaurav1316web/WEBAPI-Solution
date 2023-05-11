' ----------------- Created By Anubhooti On 16-Oct-2015 Against -------------------- '
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
Imports System.IO
Imports XpertERPEngine

Public Class FrmActivityType
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim IsLoadChildItems As Boolean = False
    Public errorControl As clsErrorControl = New clsErrorControl()

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmActivityType)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub AddNew()
        TxtCode.Value = ""
        TxtDesp.Text = ""
        TxtCode.MyReadOnly = False
        TxtCode.Focus()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(TxtCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(TxtCode.Value)) > 30 Then
                myMessages.blankValue("Code")
                TxtCode.Focus()
                TxtCode.Select()
                errorControl.SetError(TxtCode, "Code")
                Return False
            Else
                errorControl.ResetError(TxtCode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(TxtDesp.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(TxtDesp.Text)) > 150 Then
                myMessages.blankValue("Description")
                TxtDesp.Focus()
                TxtDesp.Select()
                errorControl.SetError(TxtDesp, "Description")
                Return False
            Else
                errorControl.ResetError(TxtDesp)
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            btnsave.Focus()
            If AllowToSave() Then
                Dim obj As New ClsActivityType()
                obj.Activity_Type_Code = TxtCode.Value
                obj.Activity_Type_Name = clsCommon.myCstr(TxtDesp.Text)

                Dim qry As Integer = clsDBFuncationality.getSingleValue("SELECT COUNT(Activity_Type_Code) FROM TSPL_SW_ACTIVITY_TYPE_MASTER WHERE Activity_Type_Code='" + obj.Activity_Type_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsActivityType.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Activity_Type_Code, NavigatorType.Current)
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
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsActivityType = ClsActivityType.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            TxtCode.Value = obj.Activity_Type_Code
            TxtDesp.Text = obj.Activity_Type_Name

            TxtCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(TxtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Are you sure? do you want to delete this Code ('" + TxtCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_SW_ACTIVITY_TYPE_MASTER WHERE Activity_Type_Code='" + TxtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub
#End Region

#Region "Events"
    Private Sub RmExport_Click(sender As Object, e As EventArgs) Handles RmExport.Click
        Dim str As String
        str = "SELECT Activity_Type_Code As [Code],Activity_Type_Name As [Description] FROM TSPL_SW_ACTIVITY_TYPE_MASTER "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RmImport_Click(sender As Object, e As EventArgs) Handles RmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Description") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsActivityType()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)

                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strCode) > 30 Then
                        Throw New Exception("Please check ! Length of code can not be more than 30 at line no." + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strDescription) <= 0 Then
                        Throw New Exception("Description should not be left blank" + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strDescription) > 150 Then
                        Throw New Exception("Please check ! Length of description can not be more than 150 at line no." + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_SW_ACTIVITY_TYPE_MASTER WHERE Activity_Type_Code='" + strCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Activity_Type_Code = strCode
                    obj.Activity_Type_Name = strDescription
                    ClsActivityType.SaveData(obj, IsNewEntry)

                Next
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub FrmActivityType_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmActivityType_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        AddNew()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub TxtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles TxtCode._MYNavigator
        Try
            LoadData(TxtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub TxtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_SW_ACTIVITY_TYPE_MASTER where Activity_Type_Code ='" + TxtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            TxtCode.MyReadOnly = False
        Else
            TxtCode.MyReadOnly = True
        End If

        If TxtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "SELECT Activity_Type_Code As [Code],Activity_Type_Name As [Activity Type Name] FROM TSPL_SW_ACTIVITY_TYPE_MASTER"
            TxtCode.Value = clsCommon.ShowSelectForm("SWActTF", qry, "Code", "", TxtCode.Value, "TSPL_SW_ACTIVITY_TYPE_MASTER.Activity_Type_Name", isButtonClicked)
            If clsCommon.myLen(TxtCode.Value) > 0 Then
                Dim objOT As ClsActivityType
                objOT = ClsActivityType.GetData(TxtCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(TxtCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub
#End Region
End Class