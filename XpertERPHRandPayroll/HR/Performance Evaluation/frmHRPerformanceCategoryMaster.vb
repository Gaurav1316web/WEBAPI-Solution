' ----------------- Created By Anubhooti On 26-Dec-2014 Against -------------------- '
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

Public Class frmHRPerformanceCategoryMaster
    Inherits FrmMainTranScreen
    Public Code As String
    Private isNewEntry As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRPerformanceCategoryMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As ClsHRPerformanceCategory = ClsHRPerformanceCategory.GetData(strCode, NavTyep, Nothing)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            txtCode.Value = obj.Code
            txtProject.Text = obj.Description
            ChkIsKRA.Checked = obj.IsKRA
            txtCode.MyReadOnly = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        End If
    End Sub
    Private Sub AddNew()
        txtCode.Value = ""
        txtProject.Text = ""
        ChkIsKRA.Checked = False
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtCode.MyReadOnly = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
    End Sub
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New ClsHRPerformanceCategory()
                obj.Code = txtCode.Value
                obj.Description = txtProject.Text
                obj.IsKRA = ChkIsKRA.Checked
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Code) from TSPL_HR_PERFORMANCE_CATEGORY where Code='" + obj.Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsHRPerformanceCategory.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) > 30 Then
                myMessages.blankValue("Code")
                txtcode.Focus()
                txtcode.Select()
                Errorcontrol.SetError(txtCode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtCode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtProject.Text)) <= 0 Then
                myMessages.blankValue("Description")
                txtProject.Focus()
                txtProject.Select()
                Errorcontrol.SetError(txtProject, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtProject)
            End If
            If ChkIsKRA.Checked Then
                Dim str As String = " Select count(*) from TSPL_HR_PERFORMANCE_CATEGORY where Iskra =1 and Code <> '" + txtCode.Value + "' "
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str)) > 0 Then
                    Throw New Exception("More than one category can not be of type KRA.")
                End If
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
            If clsCommon.MyMessageBoxShow("are you sure? do you want to delete this Code ('" + txtCode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_HR_PERFORMANCE_CATEGORY WHERE Code='" + txtCode.Value + "'"
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
#End Region

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_HR_PERFORMANCE_CATEGORY where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Code As [Code],Description,isKra AS [Is KRA] from TSPL_HR_PERFORMANCE_CATEGORY"
            txtCode.Value = clsCommon.ShowSelectForm("HRPerf", qry, "Code", "", txtCode.Value, "TSPL_HR_PERFORMANCE_CATEGORY.Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Dim objOT As ClsHRPerformanceCategory
                objOT = ClsHRPerformanceCategory.GetData(txtCode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub frmPerformanceCategoryMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub frmPerformanceCategoryMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New ")
        AddNew()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        Dim KRACount As String
        If transportSql.importExcel(gv, "Code", "Description", "Is KRA") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                clsCommon.ProgressBarShow()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsHRPerformanceCategory()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    Dim strIsKRA As String = clsCommon.myCstr(grow.Cells("Is KRA").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank at line no." + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strCode) > 30 Then
                        Throw New Exception("Length of code should not be greater than 30 at line no." + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Code = strCode

                    If clsCommon.myLen(strDescription) <= 0 Then
                        Throw New Exception("Description should not be left blank" + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strDescription) > 500 Then
                        Throw New Exception("Length of description should not be greater than 500 at line no." + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Description = strDescription

                    If clsCommon.myLen(strIsKRA) > 0 Then
                        If (clsCommon.CompairString(strIsKRA, "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strIsKRA, "1") <> CompairStringResult.Equal) Then
                            Throw New Exception("Is KRA should be 0 or 1 at line no." + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        strIsKRA = "0"
                    End If
                    obj.IsKRA = strIsKRA

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_HR_PERFORMANCE_CATEGORY where Code='" + strCode + "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    ClsHRPerformanceCategory.SaveData(obj, IsNewEntry, trans)

                Next
                KRACount = "select IsKRA,SUM(1) as Repeated from TSPL_HR_PERFORMANCE_CATEGORY group by IsKRA having SUM(1) > 1 and iskra=1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(KRACount, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Please check ! IsKRA repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                End If
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = "select Code,Description,case when iskra='False' then 0 else 1 End As [Is KRA] from tspl_hr_performance_category"
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
