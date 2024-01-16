'Created By Anubhooti 
'Created On Date-29/12/2014
'Tables Used TSPL_HR_PERFORMANCE_GROUP
'Class Used-ClsHRPerformanceGroup
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Public Class FrmHRPerformanceGroup
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "PerformGrp"

    Private Const colSelect As String = "colSelect"
    Private Const colCode As String = "colCode"
    Private Const colName As String = "colName"
    Private Const colType As String = "colType"
    Private Const colPersent As String = "colPersent"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Public USER_CODE As String
    Dim LoadPerf As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#Region "Functions"
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRPerformanceGroup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
#End Region
    Private Sub FrmPerformanceGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AddNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim Total As Double = 0
        Dim dt As DataTable = ClsHRPerformanceGroup.GetData(strCode, NavTyep, Nothing)
        Dim StrAllCode As String = ""
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            AddNew()
            txtCode.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtCategory.Value = clsCommon.myCstr(dt.Rows(0)("PER_CAT"))
            TxtDesp.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            lblCategory.Text = ClsHRPerformanceCategory.GetDescription(txtCategory.Value, Nothing)
            LoadAllPerformance(txtCategory.Value, False)
            For Each gvdr As GridViewRowInfo In gv1.Rows
                For Each dr As DataRow In dt.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(gvdr.Cells(colCode).Value), (clsCommon.myCstr(dr("PerformanceCode")))) = CompairStringResult.Equal Then
                        gvdr.Cells(colSelect).Value = True
                        gvdr.Cells(colPersent).Value = clsCommon.myCdbl(dr("Persent"))

                        Total += clsCommon.myCdbl(gvdr.Cells(colPersent).Value)
                        Continue For
                    End If
                Next
            Next
            LblTotal.Text = Total
            txtCode.MyReadOnly = True
            btnSave.Text = "Update"
            btnDelete.Enabled = True
        End If
    End Sub

    Sub AddNew()
        txtCode.Value = ""
        txtCategory.Value = ""
        lblCategory.Text = ""
        'isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtCode.MyReadOnly = False
        btnSave.Text = "Save"
        btnDelete.Enabled = False
        LblTotal.Text = ""
        TxtDesp.Text = ""

        LoadBlankGrid()
        For Each grow As GridViewRowInfo In gv1.Rows
            grow.Cells(colSelect).Value = False
        Next
    End Sub
    Sub OpenPerfCode(ByVal isButtonClick As Boolean)
        Dim StrCode As String = ""
        Dim strtotal As String = ""
        gv1.CurrentRow.Cells(colName).Value = ""
        gv1.CurrentRow.Cells(colType).Value = ""
        'For Each grow As GridViewRowInfo In gv1.Rows
        '    strCode = clsCommon.myCstr(grow.Cells(colCode).Value)
        '    If clsCommon.myLen(StrCode) > 0 Then
        '        strtotal = strtotal + "," + "'" + StrCode + "'"
        '    End If
        'Next
        'If strtotal.Length > 0 Then
        '    If strtotal.Substring(0, 1) = "," Then
        '        strtotal = strtotal.Substring(1, strtotal.Length - 1)
        '    End If
        'End If
        ' Dim qry As String = "SELECT TSPL_HR_Performance_Master.Code,Name FROM TSPL_HR_Performance_Master LEFT OUTER JOIN TSPL_HR_PERFORMANCE_GROUP ON TSPL_HR_PERFORMANCE_GROUP.PER_CAT = TSPL_HR_Performance_Master.PERCAT_CODE"
        Dim qry As String = "select Code,name,PERCAT_CODE  from TSPL_HR_Performance_Master   "
        gv1.CurrentRow.Cells(colCode).Value = clsCommon.ShowSelectForm("HRPerGrp", qry, "Code", "   PERCAT_CODE ='" & txtCategory.Value & "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colCode).Value) > 0 Then
            qry = "select code,name,PERCAT_CODE  from TSPL_HR_Performance_Master Where Code='" & clsCommon.myCstr(gv1.CurrentRow.Cells(colCode).Value) & "' AND PERCAT_CODE ='" & txtCategory.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                gv1.CurrentRow.Cells(colName).Value = clsCommon.myCstr(dt.Rows(0)("Name"))
                gv1.CurrentRow.Cells(colType).Value = clsCommon.myCstr(dt.Rows(0)("PERCAT_CODE"))
            End If
        End If
    End Sub
    Sub LoadAllPerformance(ByVal StrCategory As String, ByVal IsAllPerf As Boolean)
        LoadBlankGrid()
        Dim dt As DataTable
        isInsideLoadData = True
        If IsAllPerf = True Then
            dt = clsDBFuncationality.GetDataTable("select Code,Name,PERCAT_CODE from TSPL_HR_Performance_Master where PERCAT_CODE='" + StrCategory + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'")
        Else
            dt = clsDBFuncationality.GetDataTable("select TSPL_HR_Performance_Master.Code as Code,TSPL_HR_Performance_Master.Name,PERCAT_CODE from TSPL_HR_Performance_Master LEFT OUTER JOIN TSPL_HR_PERFORMANCE_GROUP ON TSPL_HR_PERFORMANCE_GROUP.PerformanceCode  = TSPL_HR_Performance_Master.Code  where  TSPL_HR_PERFORMANCE_GROUP.Code='" & txtCode.Value & "' and TSPL_HR_Performance_Master.Comp_Code = '" + objCommonVar.CurrentCompanyCode + "'")
            'dt = clsDBFuncationality.GetDataTable("Select Code, From TSPL_HR_PERFORMANCE_GROUP WHERE PerformanceCode='" & txtCode.Value & "'")
        End If

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = clsCommon.myCstr(dr("Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colName).Value = clsCommon.myCstr(dr("Name"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colType).Value = clsCommon.myCstr(dr("PERCAT_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colPersent).Value = 0
            Next
            gv1.CurrentRow = gv1.Rows(0)
        End If
        isInsideLoadData = False
        ReStoreGridLayout()
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoType As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Status"
        repoType.Name = colSelect
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoType.WrapText = True
        repoType.Width = 50
        repoType.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoType)

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "KRA Code"
        repoCode.Name = colCode
        repoCode.Width = 100
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoName.FormatString = ""
        repoName.HeaderText = "KRA Name"
        repoName.Name = colName
        repoName.Width = 100
        repoName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoName)

        Dim Type As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Type.FormatString = ""
        Type.HeaderText = "Category"
        Type.Name = colType
        Type.Width = 100
        Type.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Type)

        Dim Persent As GridViewDecimalColumn = New GridViewDecimalColumn()
        Persent.FormatString = ""
        Persent.HeaderText = "Percent"
        Persent.Name = colPersent
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
        ' Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim Arr As List(Of ClsHRPerformanceGroup) = New List(Of ClsHRPerformanceGroup)
                Dim obj As New ClsHRPerformanceGroup()
                For Each grow As GridViewRowInfo In gv1.Rows
                    If CBool(grow.Cells(colSelect).Value = True) AndAlso clsCommon.myLen(clsCommon.myCstr((grow.Cells(colCode).Value))) > 0 Then
                        obj = New ClsHRPerformanceGroup()
                        obj.Code = txtCode.Value
                        obj.PER_CAT = txtCategory.Value
                        obj.Description = TxtDesp.Text
                        obj.PerformanceCode = clsCommon.myCstr(grow.Cells(colCode).Value)
                        obj.Persent = clsCommon.myCstr(grow.Cells(colPersent).Value)
                        Arr.Add(obj)
                    End If
                Next
                If (ClsHRPerformanceGroup.SaveData(txtCode.Value, Arr)) Then
                    '  trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            '    trans.Rollback()
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try

            btnSave.Focus()
            Dim GridRow As Integer = 0
            If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
                txtCode.Focus()
                txtCode.Select()
                myMessages.blankValue("Group code")
                Errorcontrol.SetError(txtCode, "Group code")
                Return False
            Else
                Errorcontrol.ResetError(txtCode)
            End If

            If clsCommon.myLen(clsCommon.myCstr(TxtDesp.Text)) <= 0 Then
                TxtDesp.Focus()
                TxtDesp.Select()
                myMessages.blankValue("Description")
                Errorcontrol.SetError(TxtDesp, "Description")
                Return False
            Else
                Errorcontrol.ResetError(TxtDesp)
            End If

            If clsCommon.myLen(clsCommon.myCstr(txtCategory.Value)) <= 0 Then
                txtCategory.Focus()
                txtCategory.Select()
                myMessages.blankValue("Group category")
                Errorcontrol.SetError(txtCategory, "Group category")
                Return False
            Else
                Errorcontrol.ResetError(txtCategory)
            End If
            Dim Per As Double
            For Each grow As GridViewRowInfo In gv1.Rows
                If CBool(grow.Cells(colSelect).Value = True) Then
                    If clsCommon.myCdbl(grow.Cells(colPersent).Value) <= 0 AndAlso clsCommon.myLen(clsCommon.myCstr((grow.Cells(colCode).Value))) > 0 Then
                        Throw New Exception("Please fill correct percentage for KRA (" & clsCommon.myCstr(grow.Cells(colCode).Value) & ")")
                    End If
                    If clsCommon.myLen(clsCommon.myCstr((grow.Cells(colCode).Value))) > 0 Then
                        Per = Per + clsCommon.myCdbl(grow.Cells(colPersent).Value)
                    End If

                End If
            Next
            If Per <> 100 Then
                Throw New Exception("Total Of Percentage Must Be 100 for selected Performance.")
            End If
            For i As Integer = 0 To gv1.Rows.Count - 1
                GridRow += 1
                If clsCommon.myLen(gv1.Rows(i).Cells(colCode).Value) > 0 Then
                    Dim KRACode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colCode).Value)
                    For j As Integer = i + 1 To gv1.Rows.Count - 1
                        Dim SecKRACode As String = clsCommon.myCstr(gv1.Rows(j).Cells(colCode).Value)
                        If clsCommon.CompairString(KRACode, SecKRACode) = CompairStringResult.Equal Then
                            Throw New Exception("Please check ! Duplicate KRA code (" + KRACode + ") in grid")
                        End If
                    Next
                End If

            Next
            If GridRow <= 0 Then
                Throw New Exception("Enter at least one group detail")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub DeleteData()
        Try
            Dim GrpMap As Double = 0
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Group code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Do you want to delete group '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                GrpMap = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_HR_PERFORMANCE_GROUP_MAPPING WHERE Performance_Group_Code='" & txtCode.Value & "'"))
                If GrpMap > 0 Then
                    Throw New Exception("Group code is used in group mapping")
                End If
                Dim qry As String = "delete from TSPL_HR_PERFORMANCE_GROUP where Code='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Group code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Group code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Private Sub FrmPerformanceGroup_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    'Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
    '    AddNew()
    'End Sub

    Private Sub txtCode__MYNavigator_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        'If txtCode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "select Distinct Code from  TSPL_HR_PERFORMANCE_GROUP "
        '    Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        '    txtCode.Value = clsCommon.ShowSelectForm("P_GRP", qry, "Code", "", txtCode.Value, "", isButtonClicked)
        '    LoadData(txtCode.Value, NavigatorType.Current)
        'End If

        Dim str As String = "select count(*) from TSPL_HR_PERFORMANCE_GROUP where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If

        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Code ,PerformanceCode ,Persent ,PER_CAT  From TSPL_HR_PERFORMANCE_GROUP"
            txtCode.Value = clsCommon.ShowSelectForm("PG", qry, "Code", "", txtCode.Value, "TSPL_HR_PERFORMANCE_GROUP.Code", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                'Dim objOT As ClsHRPerformanceCategory
                'objOT = ClsHRPerformanceCategory.GetData(txtCode.Value, NavigatorType.Current)
                'If Not objOT Is Nothing Then
                LoadData(txtCode.Value, NavigatorType.Current)
                'End If
            Else
                AddNew()
            End If
        End If
    End Sub

    Private Sub txtCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCategory._MYValidating
        Dim qry As String = "select Code,Description,IsKRA from TSPL_HR_PERFORMANCE_CATEGORY "
        Dim whrClas As String = " TSPL_HR_PERFORMANCE_CATEGORY.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
        txtCategory.Value = clsCommon.ShowSelectForm("PCatGrp", qry, "Code", whrClas, txtCategory.Value, "Code", isButtonClicked)
        If clsCommon.myLen(txtCategory.Value) > 0 Then
            lblCategory.Text = ClsHRPerformanceCategory.GetDescription(txtCategory.Value, Nothing)
            LoadAllPerformance(txtCategory.Value, True)
        Else
            lblCategory.Text = ""
        End If
    End Sub

    'Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
    '    Dim gv As New RadGridView()
    '    Dim IsNewEntry As Boolean
    '    Me.Controls.Add(gv)
    '    Dim KRACount As String
    '    If transportSql.importExcel(gv, "Group Code", "Performance Code", "Percent", "Category Code") Then
    '        Dim linno As Integer = 0
    '        Dim trans As SqlTransaction = Nothing
    '        'Try
    '        '    connectSql.OpenConnection()
    '        '    clsCommon.ProgressBarShow()
    '        '    trans = clsDBFuncationality.GetTransactin()
    '        '    For Each grow As GridViewRowInfo In gv.Rows

    '        '        Dim obj As New ClsHRPerformanceCategory()
    '        '        Dim strGCode As String = clsCommon.myCstr(grow.Cells("Group Code").Value)
    '        '        Dim strPerCode As String = clsCommon.myCstr(grow.Cells("Performance Code").Value)
    '        '        Dim strPercent As String = clsCommon.myCstr(grow.Cells("Percent").Value)
    '        '        Dim strCatCode As String = clsCommon.myCstr(grow.Cells("Category Code").Value)
    '        '        linno += 1
    '        '        If clsCommon.myLen(strGCode) <= 0 Then
    '        '            Throw New Exception("Group code should not be left blank at line no." + clsCommon.myCstr(linno) + ".")
    '        '        ElseIf clsCommon.myLen(strGCode) > 30 Then
    '        '            Throw New Exception("Length of code should not be greater than 30 at line no." + clsCommon.myCstr(linno) + ".")
    '        '        End If
    '        '        obj.Code = strCode

    '        '        If clsCommon.myLen(strDescription) <= 0 Then
    '        '            Throw New Exception("Description should not be left blank" + clsCommon.myCstr(linno) + ".")
    '        '        ElseIf clsCommon.myLen(strDescription) > 500 Then
    '        '            Throw New Exception("Length of description should not be greater than 500 at line no." + clsCommon.myCstr(linno) + ".")
    '        '        End If
    '        '        obj.Description = strDescription

    '        '        If clsCommon.myLen(strIsKRA) > 0 Then
    '        '            If (clsCommon.CompairString(strIsKRA, "0") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(strIsKRA, "1") <> CompairStringResult.Equal) Then
    '        '                Throw New Exception("Is KRA should be 0 or 1 at line no." + clsCommon.myCstr(linno) + ".")
    '        '            End If
    '        '        Else
    '        '            strIsKRA = "0"
    '        '        End If
    '        '        obj.IsKRA = strIsKRA

    '        '        If clsCommon.myLen(strCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_HR_PERFORMANCE_CATEGORY where Code='" + strCode + "' ", trans) > 0 Then
    '        '            IsNewEntry = False
    '        '        Else
    '        '            IsNewEntry = True

    '        '        End If

    '        '        ClsHRPerformanceGroup.SaveData(obj, IsNewEntry, trans)

    '        '    Next
    '        '    KRACount = "select IsKRA,SUM(1) as Repeated from TSPL_HR_PERFORMANCE_CATEGORY group by IsKRA having SUM(1) > 1 and iskra=1"
    '        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(KRACount, trans)
    '        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        '        Throw New Exception("Please check ! IsKRA repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
    '        '    End If
    '        '    trans.Commit()
    '        '    clsCommon.ProgressBarHide()
    '        '    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
    '        'Catch ex As Exception
    '        '    trans.Rollback()
    '        '    clsCommon.ProgressBarHide()
    '        '    myMessages.myExceptions(ex)
    '        'End Try
    '    End If
    '    Me.Controls.Remove(gv)
    'End Sub

    'Private Sub rmExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rmExport.Click
    '    Dim str As String
    '    str = "select Code As [Group Code],PerformanceCode AS[Performance Code],Persent AS [Percent],PER_CAT AS [Category Code] From TSPL_HR_PERFORMANCE_GROUP "
    '    transportSql.ExporttoExcel(str, Me)
    'End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            Dim Total As Double = 0
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(colPersent) Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Total += clsCommon.myCdbl(grow.Cells(colPersent).Value)
                        LblTotal.Text = Total
                    Next
                ElseIf e.Column Is gv1.Columns(colCode) Then
                    OpenPerfCode(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
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
End Class
