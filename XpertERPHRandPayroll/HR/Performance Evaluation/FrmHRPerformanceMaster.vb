'Created By Anubhooti 
'Created On 29/12/2014 
'Tables Used: TSPL_HR_Performance_Master
'Class Used ClsHRPerformanceMaster
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

Public Class FrmHRPerformanceMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Public Code As String
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#Region "Functions"
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRPerformanceMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsHRPerformanceMaster = ClsHRPerformanceMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            AddNew()
            isNewEntry = False
            txtCode.Value = obj.Code
            txtName.Text = obj.Name
            txtCategory.Value = obj.PerCat_Code
            lblCategory.Text = ClsHRPerformanceCategory.GetDescription(obj.PerCat_Code, Nothing)
            txtCode.MyReadOnly = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        End If
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtName.Text = ""
        txtCategory.Value = ""
        lblCategory.Text = ""
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtCode.MyReadOnly = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
    End Sub
    Sub SaveData()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New ClsHRPerformanceMaster()
                obj.Code = txtCode.Value
                obj.Name = txtName.Text
                obj.PerCat_Code = txtCategory.Value
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Code) from TSPL_HR_Performance_Master where Code='" + obj.Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (ClsHRPerformanceMaster.SaveData(obj, isNewEntry)) Then
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
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean

        Try
            btnSave.Focus()
            If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) > 30 Then
                myMessages.blankValue("Code")
                txtCode.Focus()
                txtCode.Select()
                Errorcontrol.SetError(txtCode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtCode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtName.Text)) <= 0 Then
                myMessages.blankValue("Name")
                txtName.Focus()
                txtName.Select()
                Errorcontrol.SetError(txtName, "Name")
                Return False
            Else
                Errorcontrol.ResetError(txtName)
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
                Throw New Exception("Not Data Found To Be Deleted")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Do you want to delete Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim qry As String = "Delete From TSPL_HR_Performance_Master where Code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Record deleted successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to be deleted") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current code is in use, cannot be deleted", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub
#End Region
    Private Sub FrmKRAMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadData(Code, NavigatorType.Current)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_HR_Performance_Master where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "select TSPL_HR_Performance_Master.Code as Code,TSPL_HR_Performance_Master.Name as Name,TSPL_HR_Performance_Master.PerCat_Code as [Category], TSPL_HR_PERFORMANCE_CATEGORY.IsKRA from  TSPL_HR_Performance_Master " & _
            " Left outer join TSPL_HR_PERFORMANCE_CATEGORY on TSPL_HR_PERFORMANCE_CATEGORY.Code = TSPL_HR_Performance_Master.PerCat_Code "
            txtCode.Value = clsCommon.ShowSelectForm("HRPerf", qry, "Code", "", txtCode.Value, "TSPL_HR_Performance_Master.Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Dim objOT As ClsHRPerformanceMaster
                objOT = ClsHRPerformanceMaster.GetData(txtCode.Value, NavigatorType.Current)
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
    Private Sub FrmKRAMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub txtCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategory._MYValidating
        Dim qry As String = "select Code,Description,IsKRA from TSPL_HR_PERFORMANCE_CATEGORY "
        Dim whrClas As String = " TSPL_HR_PERFORMANCE_CATEGORY.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        txtCategory.Value = clsCommon.ShowSelectForm("PERF_CATEG", qry, "Code", whrClas, txtCategory.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtCategory.Value) > 0 Then
            lblCategory.Text = ClsHRPerformanceCategory.GetDescription(txtCategory.Value, Nothing)
        Else
            lblCategory.Text = ""
        End If
    End Sub

  
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Code", "Name", "Category Code") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                clsCommon.ProgressBarShow()
                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows

                    Dim obj As New ClsHRPerformanceMaster()
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim strName As String = clsCommon.myCstr(grow.Cells("Name").Value)
                    Dim strCategoryCode As String = clsCommon.myCstr(grow.Cells("Category Code").Value)
                    linno += 1
                    If clsCommon.myLen(strCode) <= 0 Then
                        Throw New Exception("Code should not be left blank at line no." + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strCode) > 30 Then
                        Throw New Exception("Length of code should not be greater than 30 at line no." + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Code = strCode

                    If clsCommon.myLen(strName) <= 0 Then
                        Throw New Exception("Name should not be left blank" + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.myLen(strName) > 500 Then
                        Throw New Exception("Length of name should not be greater than 500 at line no." + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Name = strName

                    If clsCommon.myLen(strCategoryCode) > 0 Then
                        Dim qryCatCode As String = "select Count(*) As Row from TSPL_HR_PERFORMANCE_CATEGORY where Code ='" & strCategoryCode & "'"
                        Dim checkCatCode As Integer = clsDBFuncationality.getSingleValue(qryCatCode, trans)
                        If checkCatCode <= 0 Then
                            Throw New Exception("Filled category code does not exist" + Environment.NewLine + ".First make the entry for performance category code at line no." + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.PerCat_Code = strCategoryCode

                    If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_HR_Performance_Master where Code='" + strCode + "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    ClsHRPerformanceMaster.SaveData(obj, IsNewEntry, trans)
                Next

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
        str = "select TSPL_HR_Performance_Master.Code as Code,TSPL_HR_Performance_Master.Name as Name,TSPL_HR_Performance_Master.PerCat_Code as [Category Code] from  TSPL_HR_Performance_Master "
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
