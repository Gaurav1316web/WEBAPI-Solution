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
'========Created By Preeti Gupta ticket no[BM00000004545]


Public Class FrmGroupOfDeduction
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmGroupOfDeduction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub fndDedGrp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndDedGrp._MYValidating
        Try
            Dim qry As String = "select Ded_Code as [Code] ,Ded_Description as [Description] from TSPL_DEDUCTION_GROUP"
            fndDedGrp.Value = clsCommon.ShowSelectForm("User", qry, "Code", "", fndDedGrp.Value, "Ded_Code", isButtonClicked)
            Refreshsub()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Public Sub MCCCode()
        Dim qry As String = "select MCC_Code as Code ,MCC_NAME  as  Name from TSPL_MCC_MASTER   "
        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"
    End Sub
    Private Sub FrmGroupOfDeduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        MCCCode()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save")

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R for Reset")
    End Sub
    Public Sub Reset()
        cbgMCC.UnCheckedAll()
        fndDedGrp.Value = ""

    End Sub
    Public Sub Save()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmGroupOfDeduction, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim arrMCC As New List(Of ClsGroupOfDeduction1)
        Dim obj As ClsGroupOfDeduction1 = Nothing
        Dim DedCode As New List(Of String)
        Dim i As Integer = 0
        Dim j As Integer = 0
        If (clsCommon.myLen(fndDedGrp.Value) <= 0) Then
            common.clsCommon.MyMessageBoxShow("Please select Deduction Group ")
            fndDedGrp.Focus()
            Return
        End If
        If ((cbgMCC.CheckedValue.Count) <= 0) Then
            common.clsCommon.MyMessageBoxShow("Please select atlist one MCC ")
            cbgMCC.Focus()
            Return
        End If
        If cbgMCC.CheckedValue.Count > 0 Then
            For i = 0 To cbgMCC.CheckedValue.Count - 1
                obj = New ClsGroupOfDeduction1()
                obj.DedCode = clsCommon.myCstr(fndDedGrp.Value)
                obj.MCCCode = clsCommon.myCstr(cbgMCC.CheckedValue(i))
                arrMCC.Add(obj)
            Next
            obj.SaveData(DedCode, arrMCC)
            common.clsCommon.MyMessageBoxShow("Data Saved Successfully")

        End If
    End Sub

    Public Sub Refreshsub()
        If (clsCommon.myLen(fndDedGrp.Value) <= 0) Then
            common.clsCommon.MyMessageBoxShow("Please select Deduction Group ")
            fndDedGrp.Focus()
            Return
        End If

        Dim isget As Boolean = True
        Dim qry As String = "select MCC_Code  from TSPL_MCC_GROUP_OF_DEDUCTION where Ded_Code = '" + fndDedGrp.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        Dim arr As New ArrayList()
        For Each dr As DataRow In dt.Rows
            arr.Add(dr("MCC_Code").ToString())
        Next
        cbgMCC.CheckedValue = arr

    End Sub

    Private Sub FrmGroupOfDeduction_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.Control AndAlso e.KeyCode = Keys.T Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.E Then
            Refreshsub()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If (clsCommon.myLen(fndDedGrp.Value) > 0) Then
                Dim qry As String = "delete from TSPL_MCC_GROUP_OF_DEDUCTION where Ded_Code='" + fndDedGrp.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Delete Successfuly")
                Reset()
            Else
                clsCommon.MyMessageBoxShow(" No data found to be Delete ")
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub

    

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
