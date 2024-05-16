'Created By pradeep 
'Created On Date-12/02/2013
Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Public Class FrmPerformanceRating
    Inherits FrmMainTranScreen
    Private Const ReportID As String = "PerforRat"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False

    Public Emp_Code As String
    Public SelectedMonth As Date

    Private Sub FrmPerformanceRating_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        If clsCommon.myLen(Emp_Code) > 0 AndAlso SelectedMonth.Year > 1900 Then
            txtFromDate.Value = SelectedMonth
            txtCode.Value = Emp_Code
            LoadData(Emp_Code, NavigatorType.Current)
            btnSave.Enabled = False
            btnDelete.Enabled = False
            btnRefresh.Enabled = False
        End If
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHRPerformanceRating)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim QRY As String = ""
        LoadGridData()
        Dim DT As DataTable = clsPerformanceRating.GetData(txtCode.Value, txtFromDate.Value, Nothing)
        If DT IsNot Nothing AndAlso DT.Rows.Count > 0 Then
            isInsideLoadData = True
            txtCode.Value = clsCommon.myCstr(DT.Rows(0)("Emp_Code"))
            lblName.Text = objCommonVar.CurrentUser
            For Each gvdr As GridViewRowInfo In gv1.Rows
                For Each dr As DataRow In DT.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(gvdr.Cells("Performance Group").Value), (clsCommon.myCstr(dr("PERFORMANCE_GROUP")))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvdr.Cells("Performance").Value), (clsCommon.myCstr(dr("PerformanceCode")))) = CompairStringResult.Equal Then
                        'QRY = "select TSPL_HR_PERFORMANCE_GROUP_MAPPING.User_Code ,TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code as [Performance Group] ," & _
                        '      " TSPL_HR_PERFORMANCE_GROUP.PerformanceCode as [Performance], TSPL_HR_PERFORMANCE_GROUP.Persent as [Total], 0 as [Score],TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent As TotalPer ,(TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent * TSPL_HR_PERFORMANCE_GROUP.Persent ) /100 As [Actual Total],(TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent * TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_PERSENT_GAIN  ) /100 As [Actual Score] " & _
                        '      " from TSPL_HR_PERFORMANCE_GROUP_MAPPING " & _
                        '      " left outer join TSPL_HR_PERFORMANCE_GROUP on TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code = TSPL_HR_PERFORMANCE_GROUP.Code " & _
                        '      " LEFT OUTER JOIN TSPL_HR_PERFORMANCE_RATING ON TSPL_HR_PERFORMANCE_RATING.PerformanceCode  = TSPL_HR_PERFORMANCE_GROUP.PerformanceCode  " & _
                        '      " AND TSPL_HR_PERFORMANCE_GROUP.Code = TSPL_HR_PERFORMANCE_RATING.PERFORMANCE_GROUP"
                        'QRY += "  WHERE TSPL_HR_PERFORMANCE_GROUP_MAPPING.User_Code='" + txtCode.Value + "' and TSPL_HR_PERFORMANCE_RATING.Comp_Code='" + objCommonVar.CurrentCompanyCode + "' " & _
                        '    " AND DatePart(Month,MONTH_YEAR) =DatePart(Month,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "') and DatePart(year,MONTH_YEAR) =DatePart(year,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "')"
                        'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(QRY)
                        gvdr.Cells("Score").Value = clsCommon.myCdbl(dr("PERFORMANCE_PERSENT_GAIN"))
                        'If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        '    For Each dr1 As DataRow In dt1.Rows
                        '        gvdr.Cells("Actual Total").Value = clsCommon.myCdbl(dr1("Actual Total"))
                        '        gvdr.Cells("Actual Score").Value = clsCommon.myCdbl(dr1("Actual Score"))
                        '    Next
                        'End If
                        gvdr.Cells("Actual Total").Value = clsCommon.myCdbl(dr("Actual Total"))
                        gvdr.Cells("Actual Score").Value = Convert.ToDecimal(dr("Actual Score"))
                        gvdr.Cells("TotalPer").Value = Convert.ToDecimal(dr("TotalPer"))
                        Continue For
                    End If
                Next
            Next
            txtCode.MyReadOnly = True
        End If
        isInsideLoadData = False
    End Sub

    Sub AddNew()
        txtCode.Value = ""
        lblName.Text = ""
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        txtCode.MyReadOnly = False
    End Sub

    Sub LoadGridData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select a user First.", Me.Text)
                Return
            End If
            If clsCommon.myCDate(txtFromDate.Value).Year <= 1900 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Valid Month.", Me.Text)
                Return
            End If

            Dim StrQuy As String = "select TSPL_HR_PERFORMANCE_GROUP_MAPPING.Emp_Code  ,TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code as [Performance Group] , " & _
            " TSPL_HR_PERFORMANCE_GROUP.PerformanceCode as [Performance], TSPL_HR_PERFORMANCE_GROUP.Persent as [Total], 0 as [Score] ,(TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent * TSPL_HR_PERFORMANCE_GROUP.Persent ) /100 As [Actual Total],0.0 As [Actual Score],TSPL_HR_PERFORMANCE_GROUP_MAPPING.Persent  AS TotalPer " & _
            " from TSPL_HR_PERFORMANCE_GROUP_MAPPING " & _
            " left outer join TSPL_HR_PERFORMANCE_GROUP on TSPL_HR_PERFORMANCE_GROUP_MAPPING.PERFORMANCE_GROUP_Code = TSPL_HR_PERFORMANCE_GROUP.Code  " & _
            " where TSPL_HR_PERFORMANCE_GROUP_MAPPING.Emp_Code = '" + txtCode.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuy)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.Columns("Performance Group").AllowGroup = True
                gv1.GroupDescriptors.Add(New GridGroupByExpression("[Performance Group] as [Performance Group] format ""{0} : {1}"" Group By [Performance Group]"))
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).Width = 200
                Next
                gv1.Columns("Score").ReadOnly = False
                gv1.Columns("Emp_Code").IsVisible = False
                gv1.Columns("Actual Total").ReadOnly = True
                gv1.Columns("Actual Score").ReadOnly = True
                gv1.Columns("TotalPer").IsVisible = False
                gv1.Columns("TotalPer").ReadOnly = True
                gv1.MasterTemplate.ExpandAllGroups()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Total", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Score", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                Dim item3 As New GridViewSummaryItem("Actual Total", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item3)
                Dim item4 As New GridViewSummaryItem("Actual Score", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item4)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                gv1.EnableFiltering = True
                gv1.ShowFilteringRow = True
                gv1.AllowDeleteRow = False
                gv1.EnableAlternatingRowColor = True
                gv1.Columns("Score").IsCurrent = True
                gv1.Focus()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
        Try
            If AllowToSave() Then
                Dim Arr As List(Of clsPerformanceRating) = New List(Of clsPerformanceRating)
                Dim obj As New clsPerformanceRating()
                For Each grow As GridViewRowInfo In gv1.Rows
                    obj = New clsPerformanceRating()
                    obj.Emp_Code = txtCode.Value
                    obj.MONTH_YEAR = txtFromDate.Value
                    obj.PERFORMANCE_GROUP = clsCommon.myCstr(grow.Cells("Performance Group").Value)
                    obj.PerformanceCode = clsCommon.myCstr(grow.Cells("Performance").Value)
                    obj.PERFORMANCE_PERSENT = clsCommon.myCdbl(grow.Cells("Total").Value)
                    obj.PERFORMANCE_PERSENT_GAIN = clsCommon.myCdbl(grow.Cells("Score").Value)
                    Arr.Add(obj)
                Next
                If (clsPerformanceRating.SaveData(txtCode.Value, txtFromDate.Value, Arr)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    LoadData(obj.Emp_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Dim GridRow As Integer = 0
        If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
            txtCode.Focus()
            Throw New Exception("Please Fill User Code")
        End If
        For Each grow As GridViewRowInfo In gv1.Rows
            GridRow += 1
            If clsCommon.myCdbl(grow.Cells("Total").Value) < clsCommon.myCdbl(grow.Cells("Score").Value) Then
                ''gv1.Focus()
                Dim str As String = "Score can not greater than Total, for Performance Group : " + clsCommon.myCstr(grow.Cells("Performance Group").Value) + " and Performance : " + clsCommon.myCstr(grow.Cells("Performance").Value) + " "
                Throw New Exception(str)
            End If
        Next
        If GridRow <= 0 Then
            Throw New Exception("Please fill atleast one row")
        End If
        Return True

    End Function

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Do you want to delete Record of : '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim qry As String = " delete from TSPL_HR_PERFORMANCE_RATING where Emp_Code  = '" + txtCode.Value + "' and Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' " &
                                    " and DatePart(Month,MONTH_YEAR) =DatePart(Month,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "') and DatePart(year,MONTH_YEAR) =DatePart(year,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "') "
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow(Me, "Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), " Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow(Me, "Current Code is in use", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End If
        End Try
    End Sub

    Private Sub FrmPerformanceRating_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        Dim qry As String = "select EMP_CODE from TSPL_EMPLOYEE_MASTER where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select MIN(Emp_Code ) from TSPL_EMPLOYEE_MASTER WHERE 1=1 )"
            Case NavigatorType.Last
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select Max(Emp_Code ) from TSPL_EMPLOYEE_MASTER WHERE 1=1  )"
            Case NavigatorType.Current
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select top 1 Emp_Code  from TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + txtCode.Value + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select Min(Emp_Code ) from TSPL_EMPLOYEE_MASTER where EMP_CODE>'" + txtCode.Value + "' )"
            Case NavigatorType.Previous
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select Max(Emp_Code ) from TSPL_EMPLOYEE_MASTER where EMP_CODE<'" + txtCode.Value + "')"
        End Select
        txtCode.Value = clsDBFuncationality.getSingleValue(qry)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            lblName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(Emp_Name,'') AS Emp_Name From TSPL_EMPLOYEE_MASTER Where EMP_CODE='" & txtCode.Value & "'"))
        Else
            lblName.Text = ""
        End If
        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub

    Private Sub txtCode__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Emp_Code as Code,Emp_Name as Name from  TSPL_EMPLOYEE_MASTER "
            'Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "
            txtCode.Value = clsCommon.ShowSelectForm("PRateEmp", qry, "Code", "", txtCode.Value, "", isButtonClicked)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadGridData()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            Dim Score As Double = 0
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns("Score") Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        Score = clsCommon.myCdbl(grow.Cells("Score").Value)
                        ' LblTotal.Text = Total
                        grow.Cells("Actual Score").Value = clsCommon.myCdbl((Score * clsCommon.myCdbl(grow.Cells("TotalPer").Value)) / 100)
                    Next

                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
