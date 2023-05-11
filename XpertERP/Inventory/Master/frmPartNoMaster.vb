Imports common
Imports System.Data.SqlClient


Public Class FrmPartNoMaster
    Inherits FrmMainTranScreen

#Region "variables"
    Dim isNewEntry As Boolean = True
    Dim ButtonToolTip As New ToolTip()
    Dim Errorcontrol As New clsErrorControl()
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPartNoMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag

        If MyBase.isModifyFlag Then
            btnExport.Visibility = ElementVisibility.Collapsed
            btnImport.Visibility = ElementVisibility.Collapsed
        End If
    End Sub

    Private Sub FrmPartNoMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        FunReset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
    End Sub

    Private Sub FunReset()
        isNewEntry = True

        txtPartNo.Value = ""
        txtDesc.Text = ""
        txtBrand.Text = ""
        txttype.Text = ""
        txtReleasedBy.Text = ""
        txtReleasedDate.Text = clsCommon.GETSERVERDATE()
        txtSubPart.Text = ""
        txtPartNo.MyReadOnly = False

        txtPartNo.Focus()
        txtPartNo.Select()

        btnsave.Enabled = True
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub

    Private Function AllowToSave() As Boolean
        Try

            If clsCommon.myLen(txtPartNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Fill part no.")
                txtPartNo.Focus()
                txtPartNo.Select()
                Errorcontrol.SetError(txtPartNo, "Fill part no.")
                Return False
            Else
                Errorcontrol.ResetError(txtPartNo)
            End If
            If clsCommon.myLen(txtDesc.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Fill description for part no.")
                txtDesc.Focus()
                txtDesc.Select()
                Errorcontrol.SetError(txtDesc, "Fill description for part no.")
                Return False
            Else
                Errorcontrol.ResetError(txtDesc)
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Dim count As Integer = 0

        Dim qry As String = "select count(*) from TSPL_PART_NO_MASTER  where Code ='" + txtPartNo.Value + "'"
        count = clsDBFuncationality.getSingleValue(qry)
        If count = 0 Then
            isNewEntry = True
        Else
            isNewEntry = False

        End If
        Dim obj As New clsPartNoMaster()
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPartNoMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                obj.Code = clsCommon.myCstr(txtPartNo.Value)
                obj.Description = clsCommon.myCstr(txtDesc.Text).Replace("'", "`")
                obj.Brand = clsCommon.myCstr(txtBrand.Text)
                obj.Type = clsCommon.myCstr(txttype.Text)
                obj.Released_By = clsCommon.myCstr(txtReleasedBy.Text)
                obj.Released_Date = txtReleasedDate.Value
                obj.Sub_Part = clsCommon.myCstr(txtSubPart.Text)
                If obj IsNot Nothing Then
                    If clsPartNoMaster.SaveData(obj, isNewEntry) Then
                        clsCommon.MyMessageBoxShow("Data saved successfully.")

                        LoadData(obj.Code, NavigatorType.Current)
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Dim obj As New clsPartNoMaster()
        Try
            FunReset()

            obj = clsPartNoMaster.GetData(strCode, NavType)

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                txtPartNo.Value = obj.Code
                txtDesc.Text = obj.Description
                txtBrand.Text = obj.Brand
                txttype.Text = obj.Type
                txtReleasedBy.Text = obj.Released_By
                txtReleasedDate.Text = obj.Released_Date
                txtSubPart.Text = obj.Sub_Part

                txtPartNo.MyReadOnly = True
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtPartNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select part no. for deletion.")
                txtPartNo.Focus()
                txtPartNo.Select()
                Errorcontrol.SetError(txtPartNo, "Select part no. for deletion.")
                Exit Sub
            Else
                Errorcontrol.ResetError(txtPartNo)
            End If

            If myMessages.deleteConfirm() Then
                If clsPartNoMaster.DeleteData(txtPartNo.Value) Then
                    myMessages.delete()
                    FunReset()
                End If
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim qry As String = "select count(*) from tspl_part_no_master"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code as [Part No],Description from tspl_part_no_master"
        Else
            qry = "select '' as [Part No],'' as Description from tspl_part_no_master"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        clsCommon.ProgressBarShow()
        Dim obj As New clsPartNoMaster()
        Try
            Dim qry As String = ""
            Dim check As Integer = 0

            If transportSql.importExcel(gv, "Part No", "Description") Then
                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsPartNoMaster()

                    obj.Code = clsCommon.myCstr(grow.Cells("Part No").Value)
                    obj.Description = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")

                    If clsCommon.myLen(obj.Code) > 100 Then
                        Throw New Exception("Part No have 100max. character length at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Description) <= 0 Then
                        Throw New Exception("Fill description at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Description) > 200 Then
                        Throw New Exception("Description have 200max. character length at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    End If

                    qry = "select count(*) from tspl_part_no_master where code='" + obj.Code + "'"
                    check = clsDBFuncationality.getSingleValue(qry)
                    If check > 0 Then
                        clsPartNoMaster.SaveData(obj, False)
                    Else
                        clsPartNoMaster.SaveData(obj, True)
                    End If
                Next

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            Me.Controls.Remove(gv)
            clsCommon.ProgressBarHide()
            obj = Nothing
        End Try
    End Sub

    Private Sub txtPartNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtPartNo._MYNavigator
        LoadData(txtPartNo.Value, NavType)
    End Sub

    Private Sub txtPartNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtPartNo._MYValidating
        Dim qry As String = "select count(*) from tspl_part_no_master where code='" + txtPartNo.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        txtPartNo.MyReadOnly = False
        If check > 0 Then
            txtPartNo.MyReadOnly = True
        End If

        If txtPartNo.MyReadOnly OrElse isButtonClicked Then
            txtPartNo.Value = clsPartNoMaster.GetFinder("", txtPartNo.Value, isButtonClicked)
            LoadData(txtPartNo.Value, NavigatorType.Current)
        End If
    End Sub

   
End Class
