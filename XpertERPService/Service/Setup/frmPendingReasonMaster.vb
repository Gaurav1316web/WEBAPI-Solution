
'Created By---> Pankaj Jha
'Created Date--->28/mar/2014

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports common

Imports System.Threading
Imports XpertERPEngine

Public Class frmPendingReasonMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim PendingReasonCode, Description As String
    Dim obj As New clsPendingReasonMaster
    Private Sub frmPendingReasonMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rbtnSave.Enabled Then
                saveData()

            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rbtnDelete.Enabled Then
                deleteData()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Close()
            ElseIf e.Alt And e.KeyCode = Keys.N Then
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub frmPendingReasonMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fndPendingReasonID.MyMaxLength = 30
            txtDescription.MaxLength = 500
            ButtonToolTip.SetToolTip(rbtnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(rbtnDelete, "Press Alt+D  for Delete ")
            ButtonToolTip.SetToolTip(rbtnClose, "Press Alt+C Close the Window")
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub Reset()
        Try
            fndPendingReasonID.Value = ""
            txtDescription.Text = ""
            rbtnDelete.Enabled = False
            fndPendingReasonID.Focus()
            rbtnSave.Text = "Save"
            fndPendingReasonID.MyReadOnly = False
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmPendingReasonMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        rbtnSave.Visible = MyBase.isModifyFlag
        rbtnDelete.Visible = MyBase.isDeleteFlag
    End Sub


    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSave.Click
        If AllowToSave() Then saveData()
    End Sub
    Public Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(fndPendingReasonID.Value) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Pending Reason Code")
                Return False
            End If
            If clsCommon.myLen(txtDescription.Text) = 0 Then
                common.clsCommon.MyMessageBoxShow("Please Enter Description")
                Return False
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function
    Sub saveData()
        Try
            obj = New clsPendingReasonMaster()
            obj.Pending_Reason_code = fndPendingReasonID.Value
            obj.description = txtDescription.Text.Trim

            Dim isSaved As Boolean = obj.SaveData(obj, IIf(rbtnSave.Text = "Save", True, False), Nothing)
            If isSaved Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                rbtnSave.Text = "Update"
                rbtnDelete.Enabled = True
            Else
                rbtnSave.Text = "Save"
                rbtnDelete.Enabled = False
                common.clsCommon.MyMessageBoxShow("Data Could Not Saved")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub deleteData()
        Try
            If clsCommon.myLen(fndPendingReasonID.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Code found to Delete", Me.Name)
                'Return False
            ElseIf Not (common.clsCommon.MyMessageBoxShow("Delete the Pending Reason Code " + fndPendingReasonID.Value + Environment.NewLine + " Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
                'Return False
            End If
            If (clsPendingReasonMaster.DeleteData(fndPendingReasonID.Value)) Then
                common.clsCommon.MyMessageBoxShow("Data Deleted Sucessfully", Me.Name)
                Reset()
                'Return True
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub loadData()
        Try
            obj = New clsPendingReasonMaster()
            obj = clsPendingReasonMaster.GetData(fndPendingReasonID.Value, NavigatorType.Current)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Pending_Reason_code) > 0 Then
                fndPendingReasonID.Value = obj.Pending_Reason_code
                txtDescription.Text = obj.description
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndPendingReasonID__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndPendingReasonID._MYNavigator
        Try
            obj = New clsPendingReasonMaster()
            obj = clsPendingReasonMaster.GetData(fndPendingReasonID.Value, NavType)
            If obj IsNot Nothing Then
                fndPendingReasonID.Value = obj.Pending_Reason_code
                txtDescription.Text = obj.description
                rbtnSave.Text = "Update"
                rbtnDelete.Enabled = True
                fndPendingReasonID.MyReadOnly = True

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub fndPendingReasonID__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPendingReasonID._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_PENDING_REASON_MASTER where pending_reason_code ='" + fndPendingReasonID.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                fndPendingReasonID.MyReadOnly = False
            Else
                fndPendingReasonID.MyReadOnly = True
            End If
            If fndPendingReasonID.MyReadOnly OrElse isButtonClicked Then

                'Dim qry As String = "select Pending_reason_Code as 'Code' ,Description from   TSPL_PENDING_REASON_MASTER "
                'fndPendingReasonID.Value = clsCommon.ShowSelectForm("PNDNGIDFND", qry, "Code", "", fndPendingReasonID.Value, "", isButtonClicked)
                fndPendingReasonID.Value = clsPendingReasonMaster.getFinder("", fndPendingReasonID.Value, isButtonClicked)
                txtDescription.Text = clsDBFuncationality.getSingleValue("Select description from TSPL_PENDING_REASON_MASTER where pending_Reason_Code='" + fndPendingReasonID.Value + "'")
                If clsCommon.myLen(fndPendingReasonID.Value) > 0 Then
                    rbtnDelete.Enabled = True
                    rbtnSave.Text = "Update"
                    fndPendingReasonID.MyReadOnly = True
                Else
                    rbtnSave.Text = "Save"
                    rbtnDelete.Enabled = False
                    fndPendingReasonID.MyReadOnly = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rbtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelete.Click
        deleteData()
    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Reset()
    End Sub


    Private Sub rdmenuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexit.Click
        Me.Close()
    End Sub

    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click
        Try
            Dim str As String
            str = "select Pending_Reason_Code as 'Pending Reason Code' ,Description from   TSPL_Pending_Reason_MASTER "
            transportSql.ExporttoExcel(str, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Pending Reason Code", "Description") Then
            ' Dim trans As SqlTransaction
            Try
                'connectSql.OpenConnection()
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsPendingReasonMaster()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception(" Pending Reason Code can not be blank or incorrect.")
                    End If
                    obj.Pending_Reason_code = strCode

                    Dim strDec As String = clsCommon.myCstr(grow.Cells(1).Value)

                    obj.description = strDec


                    obj.SaveData(obj, clsPendingReasonMaster.CheckNewEntry(obj.Pending_Reason_code), Nothing)
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub
End Class
