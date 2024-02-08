' ----------------- Created By Sanjay Ticket No  VIJ/21/10/19-000036 -------------------- '
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


Public Class frmConsumptionTypeMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim AutoPrefix As Boolean = False

#Region "Functions"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmSectionMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsEngConsumptionTypeMaster = clsEngConsumptionTypeMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCode.Value = obj.Code
            txtdes.Text = obj.Description
            txtCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        Else
            AddNew()
        End If
    End Sub

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsEngConsumptionTypeMaster()
                obj.Code = txtCode.Value
                obj.Description = txtdes.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Code) from TSPL_ENG_CONSUMPTION_TYPE_MASTER where Code='" + obj.Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsEngConsumptionTypeMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If AutoPrefix = False Then
            If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                Throw New Exception("Please Fill Code")
            End If
        End If
        If clsCommon.myLen(clsCommon.myCstr(txtdes.Text)) <= 0 Then
            txtdes.Focus()
            txtdes.Select()
            Throw New Exception("Please Fill Description")
        End If
        Return True
    End Function
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If myMessages.deleteConfirm() Then
                If (clsEngConsumptionTypeMaster.DeleteData(txtCode.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "This Code is already used, so can’t delete", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtdes.Text = ""
        txtCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub
#End Region

#Region "EVENTS"
    Private Sub frmConsumptionTypeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AutoPrefix = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AutoGeneratePrefix, clsFixedParameterCode.AutoGeneratePrefix, Nothing)) = "1", True, False)
        AddNew()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
    End Sub
#End Region

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Code As Code,Description from  TSPL_ENG_CONSUMPTION_TYPE_MASTER"
            txtCode.Value = clsCommon.ShowSelectForm("TSPL_ENG_CONSUMPTION_TYPE_MASTER", qry, "Code", "", txtCode.Value, "", isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub frmConsumptionTypeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                             "TSPL_ENG_CONSUMPTION_TYPE_MASTER ")
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim qry As String = "select count(*) from TSPL_ENG_CONSUMPTION_TYPE_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code,Description from TSPL_ENG_CONSUMPTION_TYPE_MASTER"
        Else
            qry = "select '' as Code,'' as Description"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv_Import, "Code", "Description") Then
            Dim objList = New List(Of clsEngConsumptionTypeMaster)
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv_Import.Rows
                    Dim obj As New clsEngConsumptionTypeMaster()

                    obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If AutoPrefix = False Then
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Throw New Exception("Fill Consumption Type code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        ElseIf clsCommon.myLen(obj.Code) > 30 Then
                            Throw New Exception("Consumption Type code has max. length 30 see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.Description) <= 0 Then
                        Throw New Exception("Fill description at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Description) > 150 Then
                        obj.Description = obj.Description.Substring(0, 150)
                    End If
                    objList.Add(obj)
                Next
                clsEngConsumptionTypeMaster.SaveData(objList)
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If

        Me.Controls.Remove(gv_Import)
    End Sub
End Class
