'Created By Anand @BM00000000815
'Created On Date-14/10/2013
'Teable Created TSPL_HR_PERFORMANCE_GROUP_MAPPING
'Class Created-ClsPerformanceGroupMapping
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Public Class FrmPerformanceGroupMapping
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "PerGrpMapp"
    Private Const colCode As String = "colCode"
    Private Const colcat As String = "colcat"
    Private Const colselect As String = "colselect"
    Private Const colisKRA As String = "colisKRA"
    Private Const colPercent As String = "colPercent"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False

    Public USER_CODE As String
    Private Sub FrmPerformanceGroupMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ' LoadData(USER_CODE, NavigatorType.Current)
        AddNew()
        LoadBlankGrid()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current, True)
        End If
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRPerformanceGroupMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType, ByVal IsAllGrpL As Boolean)
        Dim DBCount As Integer = 0
        AddNew()
        Dim dt As DataTable = ClsPerformanceGroupMapping.GetData(strCode, NavTyep)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtCode.Value = clsCommon.myCstr(dt.Rows(0)("Emp_Code"))
            If clsCommon.myLen(txtCode.Value) > 0 Then
                lblName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Emp_Name,'') AS Emp_Name From TSPL_EMPLOYEE_MASTER Where EMP_CODE='" & txtCode.Value & "'"))
            Else
                lblName.Text = ""
            End If

            DBCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) AS Row FROM TSPL_HR_PERFORMANCE_GROUP_MAPPING WHERE Emp_Code ='" & txtCode.Value & "'"))
            If DBCount > 0 Then
                IsAllGrpL = False
            Else
                IsAllGrpL = True
            End If

            LoadAllGroup(txtCode.Value, IsAllGrpL)
            For Each gvdr As GridViewRowInfo In gv1.Rows
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(gvdr.Cells(colCode).Value), (clsCommon.myCstr(dr("PERFORMANCE_GROUP_Code")))) = CompairStringResult.Equal Then
                        gvdr.Cells(colselect).Value = True
                        gvdr.Cells(colPercent).Value = clsCommon.myCdbl(dr("Persent"))
                        Continue For
                    End If
                Next
            Next
            txtCode.MyReadOnly = True
        End If
    End Sub

    Sub AddNew()
        LoadBlankGrid()
        'LoadAllGroup()
        txtCode.Value = ""
        lblName.Text = ""
        'txtCode.MyReadOnly = True
    End Sub

    Sub LoadAllGroup(ByVal GroupCode As String, ByVal IsAllGrp As Boolean)
        Dim DBCount As Integer = 0
        Dim Qry As String = ""
        LoadBlankGrid()
        Dim dt As DataTable
        isInsideLoadData = True
        If IsAllGrp = True Then
            dt = clsDBFuncationality.GetDataTable("select Distinct TSPL_HR_PERFORMANCE_GROUP.Code, TSPL_HR_PERFORMANCE_GROUP.PER_CAT as [Category], TSPL_HR_PERFORMANCE_CATEGORY.IsKRA from TSPL_HR_PERFORMANCE_GROUP Left outer join TSPL_HR_PERFORMANCE_CATEGORY on TSPL_HR_PERFORMANCE_GROUP.PER_CAT = TSPL_HR_PERFORMANCE_CATEGORY.Code where TSPL_HR_PERFORMANCE_GROUP.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'")
        Else
            Qry = "select Distinct TSPL_HR_PERFORMANCE_GROUP.Code, TSPL_HR_PERFORMANCE_GROUP.PER_CAT as [Category], TSPL_HR_PERFORMANCE_CATEGORY.IsKRA " & _
            " from TSPL_HR_PERFORMANCE_GROUP " & _
            " Left outer join TSPL_HR_PERFORMANCE_CATEGORY on TSPL_HR_PERFORMANCE_GROUP.PER_CAT = TSPL_HR_PERFORMANCE_CATEGORY.Code " & _
            " Left outer join TSPL_HR_PERFORMANCE_GROUP_MAPPING on TSPL_HR_PERFORMANCE_GROUP_MAPPING.Performance_Group_Code  = TSPL_HR_PERFORMANCE_GROUP.Code " & _
            " WHERE  TSPL_HR_PERFORMANCE_GROUP_MAPPING.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' AND EMP_CODE ='" & GroupCode & "'"

            dt = clsDBFuncationality.GetDataTable(Qry)

        End If
        ' Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Distinct TSPL_HR_PERFORMANCE_GROUP.Code, TSPL_HR_PERFORMANCE_GROUP.PER_CAT as [Category], TSPL_HR_PERFORMANCE_CATEGORY.IsKRA from TSPL_HR_PERFORMANCE_GROUP Left outer join TSPL_HR_PERFORMANCE_CATEGORY on TSPL_HR_PERFORMANCE_GROUP.PER_CAT = TSPL_HR_PERFORMANCE_CATEGORY.Code where TSPL_HR_PERFORMANCE_GROUP.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = clsCommon.myCstr(dr("Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colcat).Value = clsCommon.myCstr(dr("Category"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colisKRA).Value = clsCommon.myCdbl(dr("IsKRA"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPercent).Value = 0
                gv1.Rows(gv1.Rows.Count - 1).Cells(colselect).Value = False
            Next
            gv1.CurrentRow = gv1.Rows(0)
            gv1.Columns(colisKRA).ReadOnly = True
            gv1.Columns(colCode).ReadOnly = True
            gv1.Columns(colcat).ReadOnly = True
        End If
        isInsideLoadData = False
        ReStoreGridLayout()
    End Sub
    Sub OpenGrpCode(ByVal isButtonClick As Boolean)
        Dim StrCode As String = ""
        Dim strtotal As String = ""
        gv1.CurrentRow.Cells(colcat).Value = ""
        gv1.CurrentRow.Cells(colisKRA).Value = False

        Dim qry As String = "select Distinct TSPL_HR_PERFORMANCE_GROUP.Code, TSPL_HR_PERFORMANCE_GROUP.PER_CAT as [Category], TSPL_HR_PERFORMANCE_CATEGORY.IsKRA from TSPL_HR_PERFORMANCE_GROUP Left outer join TSPL_HR_PERFORMANCE_CATEGORY on TSPL_HR_PERFORMANCE_GROUP.PER_CAT = TSPL_HR_PERFORMANCE_CATEGORY.Code "
        gv1.CurrentRow.Cells(colCode).Value = clsCommon.ShowSelectForm("HRPerGrp", qry, "Code", " TSPL_HR_PERFORMANCE_GROUP.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colCode).Value) > 0 Then
            qry = "select Distinct TSPL_HR_PERFORMANCE_GROUP.Code, TSPL_HR_PERFORMANCE_GROUP.PER_CAT as [Category], TSPL_HR_PERFORMANCE_CATEGORY.IsKRA from TSPL_HR_PERFORMANCE_GROUP Left outer join TSPL_HR_PERFORMANCE_CATEGORY on TSPL_HR_PERFORMANCE_GROUP.PER_CAT = TSPL_HR_PERFORMANCE_CATEGORY.Code where TSPL_HR_PERFORMANCE_GROUP.Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colCode).Value) & "' AND TSPL_HR_PERFORMANCE_GROUP.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                gv1.CurrentRow.Cells(colcat).Value = clsCommon.myCstr(dt.Rows(0)("Category"))
                gv1.CurrentRow.Cells(colisKRA).Value = clsCommon.myCdbl(dt.Rows(0)("IsKRA"))
            End If
        End If
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoType As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Status"
        repoType.Name = colselect
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoType.WrapText = True
        repoType.Width = 50
        repoType.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoType)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Group Code"
        repoCode.Name = colCode
        repoCode.Width = 200
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoName.FormatString = ""
        repoName.HeaderText = "Group Category"
        repoName.Name = colcat
        repoName.Width = 200
        repoName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoName)

        Dim repoIsKRA As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsKRA.FormatString = ""
        repoIsKRA.HeaderText = "Is KRA"
        repoIsKRA.Name = colisKRA
        repoIsKRA.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsKRA.WrapText = True
        repoIsKRA.Width = 50
        repoIsKRA.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoIsKRA)

        Dim Persent As GridViewDecimalColumn = New GridViewDecimalColumn()
        Persent.FormatString = ""
        Persent.HeaderText = "Percent"
        Persent.Name = colPercent
        Persent.Width = 100
        Persent.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Persent)

        ReStoreGridLayout()
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.AllowDeleteRow = False
        gv1.EnableAlternatingRowColor = True
        gv1.MasterView.TableFilteringRow.IsCurrent = True
        gv1.Columns(0).IsCurrent = True
        gv1.Focus()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim Arr As List(Of ClsPerformanceGroupMapping) = New List(Of ClsPerformanceGroupMapping)
                Dim obj As New ClsPerformanceGroupMapping()
                For Each grow As GridViewRowInfo In gv1.Rows
                    If CBool(grow.Cells(colselect).Value = True) Then
                        obj = New ClsPerformanceGroupMapping()
                        obj.Emp_Code = txtCode.Value
                        obj.Percent = clsCommon.myCdbl(grow.Cells(colPercent).Value)
                        obj.PERFORMANCE_GROUP_Code = clsCommon.myCstr(grow.Cells(colCode).Value)
                        Arr.Add(obj)
                    End If
                Next
                If (ClsPerformanceGroupMapping.SaveData(txtCode.Value, Arr, trans)) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Emp_Code, NavigatorType.Current, False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            trans.Rollback()
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        btnSave.Focus()
        If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
            txtCode.Focus()
            Throw New Exception("Please Fill User Code")
        End If

        Dim Count As Int16 = 0
        Dim Per As Double
        For Each grow As GridViewRowInfo In gv1.Rows
            If CBool(grow.Cells(colselect).Value = True) Then
                Count += 1
                If clsCommon.myCdbl(grow.Cells(colPercent).Value) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr((grow.Cells(colCode).Value))) > 0 Then
                    Throw New Exception("Please fill correct percentage for group (" & clsCommon.myCstr(grow.Cells(colCode).Value) & ")")
                End If
                If clsCommon.myLen(clsCommon.myCstr((grow.Cells(colCode).Value))) > 0 Then
                    Per = Per + clsCommon.myCdbl(grow.Cells(colPercent).Value)
                End If
            End If
        Next
        If Count <= 0 Then
            Throw New Exception("Please Select at least one Group.")
        End If
        If Per <> 100 Then
            Throw New Exception("Total Of Percentage Must Be 100 for selected group.")
        End If

        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colCode).Value) > 0 Then
                Dim GrpCode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colCode).Value)
                For j As Integer = i + 1 To gv1.Rows.Count - 1
                    Dim SecGrpCode As String = clsCommon.myCstr(gv1.Rows(j).Cells(colCode).Value)
                    If clsCommon.CompairString(GrpCode, SecGrpCode) = CompairStringResult.Equal Then
                        Throw New Exception("Please check ! Duplicate group code (" + GrpCode + ") in grid")
                    End If
                Next
            End If

        Next
        Return True
    End Function

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception(" Code not found to delete")
            End If
            ' If clsCommon.MyMessageBoxShow("Do you want to delete Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim qry As String = "delete from TSPL_HR_PERFORMANCE_GROUP_MAPPING where Emp_Code='" + txtCode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
            AddNew()
            '  End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), " Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current  Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Private Sub FrmPerformanceGroupMapping_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub btnSave_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnDelete_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AddNew()
    End Sub

    Private Sub txtCode__MYNavigator_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator

        LoadData(txtCode.Value, NavType, True)

    End Sub

    Private Sub txtCode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        '  If txtCode.MyReadOnly OrElse isButtonClicked Then
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER "
        Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        txtCode.Value = clsCommon.ShowSelectForm("PerfEmp", qry, "Code", "", txtCode.Value, "", isButtonClicked)
        LoadData(txtCode.Value, NavigatorType.Current, True)
        ' End If
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            Dim Total As Double = 0
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(colPercent) Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Total += clsCommon.myCdbl(grow.Cells(colPercent).Value)
                        LblTotal.Text = Total
                    Next
                ElseIf e.Column Is gv1.Columns(colCode) Then
                    OpenGrpCode(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                gv1.Columns(colCode).ReadOnly = False
            End If
        End If
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value, NavigatorType.Current, False)
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select a user first.", Me.Text)
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub
End Class
