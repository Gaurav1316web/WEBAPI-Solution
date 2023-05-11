' ----------------- Created By Anubhooti On 07-Aug-2014 Against -------------------- '
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

Public Class FrmHRBudgeting
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim IsLoadData As Boolean = False

#Region "Budgeting"
    Const ColBudgetCode As String = "Budget Code"
    Const ColDeptCode As String = "Dept Code"
    Const ColDeptName As String = "Dept Name"
    Const ColBudget As String = "Budget"
    Const ColIsApplied As String = "Applied"
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.HRBudgeting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub
    Function AllowToSave() As Boolean
        Dim GridRowQual As Integer = 0
        Dim HighQual As Integer = 0
        Dim DeptCode As String = ""
        Dim DeptName As String = ""
        Dim Budget As String = ""
        Dim lineno As Integer = 0

        btnSave.Focus()
        For Each grow As GridViewRowInfo In gvBudget.Rows
            lineno += 1
            DeptCode = clsCommon.myCstr(grow.Cells(ColDeptCode).Value)
            DeptName = clsCommon.myCstr(grow.Cells(ColDeptName).Value)
            Budget = clsCommon.myCstr(grow.Cells(ColBudget).Value)
           
            If clsCommon.myLen(DeptCode) > 0 Then
                If clsCommon.myCdbl(Budget) <= 0 Then
                    clsCommon.MyMessageBoxShow("Budget can not be remain zero at line no. " + clsCommon.myCstr(lineno) + ".")
                    Return False
                End If
            End If
        Next
        Return True
    End Function
    Sub LoadBudgetingGrid()
        gvBudget.Rows.Clear()
        gvBudget.Columns.Clear()

        Dim repoApp As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoApp.HeaderText = "Applied"
        repoApp.Name = ColIsApplied
        repoApp.ReadOnly = False
        repoApp.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvBudget.MasterTemplate.Columns.Add(repoApp)

        Dim repoDeptCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDeptCode = New GridViewTextBoxColumn()
        repoDeptCode.FormatString = ""
        repoDeptCode.HeaderText = "Department Code"
        repoDeptCode.Name = ColDeptCode
        repoDeptCode.Width = 200
        repoDeptCode.MaxLength = 200
        repoDeptCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBudget.MasterTemplate.Columns.Add(repoDeptCode)

        Dim repoDeptName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDeptName.FormatString = ""
        repoDeptName.HeaderText = "Department Name"
        repoDeptName.Name = ColDeptName
        repoDeptName.Width = 600
        repoDeptName.MaxLength = 50
        repoDeptName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoDeptName.ReadOnly = True
        gvBudget.MasterTemplate.Columns.Add(repoDeptName) '1

        Dim repoBudget As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBudget.FormatString = ""
        repoBudget.HeaderText = "Budget"
        repoBudget.Name = ColBudget
        repoBudget.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBudget.Width = 200
        gvBudget.MasterTemplate.Columns.Add(repoBudget) '2

        clsCustomFieldGrid.LoadBlankGrid(gvBudget, MyBase.ArrDetailFields)

        gvBudget.AllowDeleteRow = True
        gvBudget.AllowAddNewRow = False
        gvBudget.ShowGroupPanel = False
        gvBudget.AllowColumnReorder = False
        gvBudget.AllowRowReorder = False
        gvBudget.EnableSorting = False
        gvBudget.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvBudget.MasterTemplate.ShowRowHeaderColumn = False
        gvBudget.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvBudget.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvBudget.Columns.Count - 1 Step ii + 1
                        gvBudget.Columns(ii).IsVisible = False
                        gvBudget.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvBudget.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub OpenDeptCodeList(ByVal isButtonClick As Boolean)
        gvBudget.CurrentRow.Cells(ColDeptName).Value = ""
        Dim qry As String = "SELECT department_code AS Code,department_name AS [Department Name] FROM tspl_department_master"
        gvBudget.CurrentRow.Cells(ColDeptCode).Value = clsCommon.ShowSelectForm("HRDeptBud", qry, "Code", "", clsCommon.myCstr(gvBudget.CurrentRow.Cells(ColDeptCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gvBudget.CurrentRow.Cells(ColDeptCode).Value) > 0 Then
            qry = "select department_code,department_name from tspl_department_master WHERE department_code ='" + clsCommon.myCstr(gvBudget.CurrentRow.Cells(ColDeptCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvBudget.CurrentRow.Cells(ColDeptCode).Value = clsCommon.myCstr(dt.Rows(0)("department_code"))
                gvBudget.CurrentRow.Cells(ColDeptName).Value = clsCommon.myCstr(dt.Rows(0)("department_name"))
            End If
        End If
    End Sub
    Public Sub SaveData()
        Try
            If AllowToSave() Then
                Dim arr As New List(Of ClsHRBudgeting)
                Dim obj As ClsHRBudgeting = Nothing
                For Each grow As GridViewRowInfo In gvBudget.Rows
                    If clsCommon.myLen(grow.Cells(ColDeptCode).Value) > 0 Then
                        obj = New ClsHRBudgeting()
                        obj.Budget = clsCommon.myCdbl(grow.Cells(ColBudget).Value)
                        obj.Is_Applied = clsCommon.myCdbl(grow.Cells(ColIsApplied).Value)
                        obj.DeptCode = clsCommon.myCstr(grow.Cells(ColDeptCode).Value)
                        arr.Add(obj)
                    End If
                Next
                If obj Is Nothing Then
                    clsCommon.MyMessageBoxShow("No data found.")
                Else
                    If ClsHRBudgeting.SaveData(arr) Then
                        clsCommon.MyMessageBoxShow("Data saved successfully.")
                    End If
                End If

                LoadData()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub LoadData()
        Try
            LoadBudgetingGrid()
            IsLoadData = True
            Dim arr As New List(Of ClsHRBudgeting)
            arr = ClsHRBudgeting.GetData(Nothing)
            gvBudget.Rows.AddNew()
            For Each obj As ClsHRBudgeting In arr
                gvBudget.CurrentRow.Cells(ColBudget).Value = clsCommon.myCdbl(obj.Budget)
                gvBudget.CurrentRow.Cells(ColDeptCode).Value = clsCommon.myCstr(obj.DeptCode)
                If clsCommon.myLen(clsCommon.myCstr(gvBudget.CurrentRow.Cells(ColDeptCode).Value)) > 0 Then
                    gvBudget.CurrentRow.Cells(ColDeptName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(DEPARTMENT_NAME,'') As DEPARTMENT_NAME FROM tspl_department_master WHERE DEPARTMENT_CODE ='" & clsCommon.myCstr(gvBudget.CurrentRow.Cells(ColDeptCode).Value) & "'"))
                Else
                    gvBudget.CurrentRow.Cells(ColDeptName).Value = ""
                End If
                gvBudget.CurrentRow.Cells(ColIsApplied).Value = clsCommon.myCdbl(obj.Is_Applied)
                gvBudget.Rows.AddNew()
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub
    Private Sub gvBudget_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvBudget.CellValueChanged
        If Not IsLoadData Then
            If e.Column Is gvBudget.Columns(ColDeptCode) Then
                OpenDeptCodeList(False)
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub gvBudget_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvBudget.CurrentColumnChanged
        If gvBudget.RowCount > 0 Then
            Dim intCurrRow As Integer = gvBudget.CurrentRow.Index
            If intCurrRow = gvBudget.Rows.Count - 1 Then
                gvBudget.Rows.AddNew()
                gvBudget.CurrentRow = gvBudget.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvBudget_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvBudget.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If gvBudget.Rows.Count > 0 Then
            SaveData()
        Else
            clsCommon.MyMessageBoxShow("No data found")
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvBudget.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvBudget.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvBudget.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim CustMapEntry As Double = 0
        Dim VenMapEntry As Double = 0
        Dim DuplicateEntry As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Is Applied", "Department Code", "Department Name", "Budget") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                clsCommon.ProgressBarShow()

                Dim arr As New List(Of ClsHRBudgeting)
                trans = clsDBFuncationality.GetTransactin()
                For i As Integer = 0 To gv.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("DELETE FROM TSPL_HR_BUDGETING ", trans)
                Next
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsHRBudgeting()
                    linno += 1

                    Dim DeptCode As String = ""
                    DeptCode = clsCommon.myCstr(grow.Cells("Dept Code").Value)

                    If clsCommon.myLen(DeptCode) > 0 Then
                        Dim DeptQry As String = "SELECT Count(DEPARTMENT_CODE) AS Code FROM tspl_department_master  WHERE DEPARTMENT_CODE ='" + DeptCode + "'"
                        Dim checkDQry As Integer = clsDBFuncationality.getSingleValue(DeptQry, trans)
                        If checkDQry <= 0 Then
                            Throw New Exception("Please check ! From dept code (" & clsCommon.myCstr(DeptCode) & ") does not exists in department master at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Dept code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.DeptCode = DeptCode

                    Dim IsApplied As Integer = 0
                    IsApplied = clsCommon.myCstr(grow.Cells("Is Applied").Value)

                    If clsCommon.CompairString(IsApplied, "1") <> CompairStringResult.Equal OrElse clsCommon.CompairString(IsApplied, "0") <> CompairStringResult.Equal Then
                        Throw New Exception("Is Applied should be 1 or 0 at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    Dim Budget As Double = 0
                    Budget = clsCommon.myCstr(grow.Cells("Budget").Value)

                    If clsCommon.myLen(Budget) > 0 Then
                        If clsCommon.myCdbl(Budget) <= 0 Then
                            Throw New Exception("Budget should be numeric left blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    Else
                        Throw New Exception("Budget can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    'qry = "insert into TSPL_BRANCH_ACCOUNT_MAPPING values('" + From_Loc + "','" + To_Loc + "','" + Branch_Acc + "','" + objCommonVar.CurrentCompanyCode + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") + "')"
                    'clsDBFuncationality.ExecuteNonQuery(qry, trans)

                Next
                ClsHRBudgeting.SaveData(arr)
                DuplicateEntry = "SELECT DeptCode, SUM(1) AS Repeated FROM TSPL_HR_BUDGETING group by DeptCode HAVING SUM(1) > 1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Please check ! Dept code (" & clsCommon.myCstr(dt.Rows(0)("DeptCode")) & ") repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                End If

                trans.Commit()
                clsCommon.ProgressBarHide()

                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                LoadData()
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = " SELECT TSPL_HR_BUDGETING.Department_Code AS [Department Code],tspl_department_master.DEPARTMENT_NAME AS [Department Name],Is_Applied AS [Is Applied],Budget from TSPL_HR_BUDGETING " & _
              " LEFT OUTER JOIN tspl_department_master on tspl_department_master.DEPARTMENT_CODE = TSPL_HR_BUDGETING.DEPARTMENT_CODE "
         
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub FrmHRBudgeting_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        End If
    End Sub

    Private Sub FrmHRBudgeting_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        gvBudget.AllowAddNewRow = False
        LoadData()
    End Sub
End Class
