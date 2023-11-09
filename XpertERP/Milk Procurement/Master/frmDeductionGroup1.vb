'========Created By Preeti Gupta ticket no[BM00000004545]
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
Public Class FrmDeductionGroup1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmdeductionGroup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmDeductionGroup1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub FrmDeductionGroup1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub fndCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCode._MYValidating
        Dim str As String = "select count(*) from TSPL_DEDUCTION_GROUP where Ded_Code ='" + fndCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndCode.MyReadOnly = False
        Else
            fndCode.MyReadOnly = True
        End If

        If fndCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Ded_Code As [Code],Ded_Description As [Name] from TSPL_DEDUCTION_GROUP"
            fndCode.Value = clsCommon.ShowSelectForm("DEDUCTION", qry, "Ded_Code", "", fndCode.Value, "TSPL_DEDUCTION_GROUP.Ded_Code", isButtonClicked)
            If clsCommon.myLen(fndCode.Value) > 0 Then
                Dim objOT As clsDeductionGroup
                objOT = clsDeductionGroup.GetData(fndCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(fndCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub

    Private Sub rmIMport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmIMport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Description") Then
            Dim linno As Integer = 1
            Try
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New clsDeductionGroup()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If


                    If clsCommon.myLen(strDescription) <= 0 Then
                        Throw New Exception("Description should not be left blank" + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_DEDUCTION_GROUP where Ded_Code='" + strCode + "' ") > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    obj.Code = strCode
                    obj.Description = strDescription
                    clsDeductionGroup.SaveData(obj, IsNewEntry)

                Next

                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception

                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select Ded_Code as [Code],Ded_Description As [Description]  from TSPL_DEDUCTION_GROUP "
        transportSql.ExporttoExcel(str, Me)
    End Sub
    Sub AddNew()
        fndCode.Value = ""
        txtName.Text = ""
        fndCode.MyReadOnly = False
        fndCode.Focus()
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
                If clsCommon.myLen(clsCommon.myCstr(fndCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(fndCode.Value)) > 30 Then
                    myMessages.blankValue("Code")

                    fndCode.Focus()
                    fndCode.Select()
                    Errorcontrol.SetError(fndCode, "Code")
                    Return False
                Else
                    Errorcontrol.ResetError(fndCode)
                End If
            End If
            
            If clsCommon.myLen(clsCommon.myCstr(txtName.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtName.Text)) > 100 Then
                myMessages.blankValue("Description")

                txtName.Focus()
                txtName.Select()
                Errorcontrol.SetError(txtName, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtName)
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Sub SaveData()

        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmdeductionGroup, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New clsDeductionGroup()
                obj.Code = fndCode.Value
                obj.Description = txtName.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Ded_Code) from TSPL_DEDUCTION_GROUP where Ded_Code='" + obj.Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsDeductionGroup.SaveData(obj, isNewEntry)) Then
                    'trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            'trans.Rollback()
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsDeductionGroup = clsDeductionGroup.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            fndCode.Value = obj.Code
            txtName.Text = obj.Description
            fndCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
        End If
    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("are you sure? do you want to delete this Code ('" + fndCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_DEDUCTION_GROUP WHERE Ded_Code='" + fndCode.Value + "'"
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
End Class
