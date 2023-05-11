'==Created By Monika 28/01/2015
Imports System.Data
Imports System.Data.SqlClient
Imports common


Public Class frmDeliveryTermsMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim ErrorControl As New clsErrorControl()
#End Region
    
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDeliveryTermsMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btndelete.Enabled = False
            btnsave.Enabled = True
            btnsave.Text = "Save"
            txtCode.MyReadOnly = False

            Dim obj As clsDeliveryTermsMaster = clsDeliveryTermsMaster.GetData(strCode, NavTyep)
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SaveData()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmDeliveryTermsMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim obj As New clsDeliveryTermsMaster()
                obj.Code = txtCode.Value
                obj.Description = txtdes.Text.Replace("'", "`")

                If (clsDeliveryTermsMaster.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtdes.Text) <= 0 Then
            clsCommon.MyMessageBoxShow("Fill description.")
            txtdes.Focus()
            txtdes.Select()
            ErrorControl.SetError(txtdes, "Fill Description")
        Else
            ErrorControl.ResetError(txtdes)
        End If

        Return True
    End Function

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
                If clsDeliveryTermsMaster.DeleteData(txtCode.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub AddNew()
        txtCode.Value = ""
        txtdes.Text = ""

        txtCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False

        isNewEntry = True

        txtdes.Focus()
        txtdes.Select()
    End Sub
#End Region

#Region "Events"
    Private Sub frmStageMasters_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmStageMasters_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        AddNew()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N New Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete Transaction")
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_DELIVERY_TERMS_MASTER where code='" + txtCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check > 0 Then
            txtCode.MyReadOnly = True
        Else
            txtCode.MyReadOnly = False
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsDeliveryTermsMaster.GetFinder("", txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub
#End Region

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim qry As String = "select count(*) from TSPL_DELIVERY_TERMS_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code,Description from TSPL_DELIVERY_TERMS_MASTER"
        Else
            qry = "select '' as Code,'' as Description"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Code", "Description") Then
            Dim obj As New clsDeliveryTermsMaster()

            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv_Import.Rows
                    obj = New clsDeliveryTermsMaster()

                    obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.Code) > 50 Then
                        Throw New Exception("Code has max. length 50 see at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If

                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.Description) <= 0 Then
                        Throw New Exception("Fill description at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Description) > 200 Then
                        obj.Description = obj.Description.Substring(0, 200)
                    End If

                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_DELIVERY_TERMS_MASTER where code='" + obj.Code + "'")
                    isNewEntry = True
                    If qry > 0 Then
                        isNewEntry = False
                    End If

                    If (clsDeliveryTermsMaster.SaveData(obj, isNewEntry)) Then

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
