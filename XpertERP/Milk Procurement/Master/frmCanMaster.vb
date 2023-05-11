Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class frmCanMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        funReset()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsCanMaster()
            obj.Code = txtCode.Value
            obj.Description = txtDescription.Text
            obj.Tare_Weight = txtTareWeight.Value
            If (clsCanMaster.SaveData(obj)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Code, NavigatorType.Current)
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        rdbtndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsCanMaster()
        obj = clsCanMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            rdbtndelete.Enabled = True
            txtCode.Value = obj.Code
            txtDescription.Text = obj.Description
            txtTareWeight.Value = obj.Tare_Weight
        End If
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue("Code")
            txtCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDescription.Focus()
            Return False
        End If
        If clsCommon.myLen(txtCode.Value) > 20 Then
            clsCommon.MyMessageBoxShow("Length is greater then 20.")
            txtCode.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        ' Code Ends 
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsCanMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_CAN_MASTER where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsCanMaster.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtDescription.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        rdbtndelete.Enabled = False
        txtTareWeight.Value = 0
    End Sub

    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        Import.Enabled = MyBase.isModifyFlag
        Export.Enabled = MyBase.isModifyFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub

    Private Sub MenuItemExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Export.Click
        Dim str As String
        str = "select Code,Description,Tare_Weight as [Tare Weight] from TSPL_CAN_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub MenuItemImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Import.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Tare Weight") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsCanMaster()
                        obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.Code) <= 0 Then
                            Continue For
                        End If
                        If clsCommon.myLen(obj.Code) > 10 Then
                            Throw New Exception("length greater then 10.")
                        End If
                        obj.Description = clsCommon.myCstr(grow.Cells("Description").Value)
                        obj.Tare_Weight = clsCommon.myCdbl(grow.Cells("Tare Weight").Value)
                        If obj.Tare_Weight <= 0 Then
                            Throw New Exception("Invalid tare weight " + clsCommon.myCstr(obj.Tare_Weight))
                        End If
                        If clsCommon.myLen(obj.Description) <= 0 Then
                            Throw New Exception("Description can not be blank or incorrect.")
                        End If
                        clsCanMaster.SaveData(obj)
                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select code")
                Exit Sub
            End If
            clsERPFuncationality.ShowHistoryData(txtCode.Value, "Code", "TSPL_CAN_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
