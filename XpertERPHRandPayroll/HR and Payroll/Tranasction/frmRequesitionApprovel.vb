Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.Apoc
Imports System.Data.SqlClient
Imports System.IO
'--preeti gupta--ticket no[BM00000003466][BM00000003730]
Public Class FrmRequesitionApprovel
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Const ReportID As String = "Req_APR"
    Const colChk As String = "Select"
    Const colReqCode As String = "colReqCode"
    Const colReqDescription As String = "colReqDescription"
    Const colBudget As String = "colBudget"
    Const colInitiatedBy As String = "colInitiatedBy"
    Const colRecomBy As String = "colRecomBy"
    Const colRecomByName As String = "colRecomByName"
    Const colDepCode As String = "colDepCode"
    Const colDepName As String = "colDepName"
    Const colLocation As String = "colLocation"
    Const colJobTitle As String = "colJobTitle"
    Const colJobTitleDesp As String = "JobTitleDesp"
    Const colMaxSal As String = "Max Salary"
    Const colCTCRange As String = "CTCRange"
    Const ColDeptReqSal As String = "DeptReqSal"
    Dim isFlag As Boolean = False
    Const colNoOfPost As String = "colNoOfPost"

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoChk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoChk.FormatString = ""
        repoChk.Name = colChk
        repoChk.Width = 10
        repoChk.IsVisible = True
        repoChk.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoChk)

        Dim repoReqCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoReqCode.FormatString = ""
        repoReqCode.HeaderText = "Requisition Code"
        repoReqCode.Name = colReqCode
        repoReqCode.Width = 100
        repoReqCode.IsVisible = True
        repoReqCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoReqCode)

        Dim repReqDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repReqDescription.FormatString = ""
        repReqDescription.HeaderText = "Requisition Description"
        repReqDescription.Name = colReqDescription
        repReqDescription.Width = 150
        repReqDescription.IsVisible = True
        repReqDescription.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repReqDescription)

        Dim repoBudget As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBudget.FormatString = ""
        repoBudget.HeaderText = "Budget"
        repoBudget.Name = colBudget
        repoBudget.Width = 100
        repoBudget.IsVisible = True
        repoBudget.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBudget)

        Dim repoInitiatedBy As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoInitiatedBy.FormatString = ""
        repoInitiatedBy.HeaderText = "Initiated By"
        repoInitiatedBy.Name = colInitiatedBy
        repoInitiatedBy.Width = 100
        repoInitiatedBy.IsVisible = True
        repoInitiatedBy.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoInitiatedBy)

        Dim repoRecomBy As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRecomBy.FormatString = ""
        repoRecomBy.HeaderText = "Recommended Code"
        repoRecomBy.Name = colRecomBy
        repoRecomBy.Width = 140
        repoRecomBy.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRecomBy)

        Dim repoRecomName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRecomName.FormatString = ""
        repoRecomName.HeaderText = "Recommended Name"
        repoRecomName.Name = colRecomByName
        repoRecomName.Width = 170
        repoRecomName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRecomName)

        Dim repoDepCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepCode.FormatString = ""
        repoDepCode.HeaderText = "Department Code"
        repoDepCode.Name = colDepCode
        repoDepCode.Width = 150
        repoDepCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDepCode)

        Dim repoDepName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepName.FormatString = ""
        repoDepName.HeaderText = "Department Name"
        repoDepName.Name = colDepName
        repoDepName.Width = 200
        repoDepName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDepName)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colLocation
        repoLocation.Width = 150
        repoLocation.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocation)

        Dim repoJobTitle As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobTitle.FormatString = ""
        repoJobTitle.HeaderText = "Job Title"
        repoJobTitle.Name = colJobTitle
        repoJobTitle.Width = 100
        repoJobTitle.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoJobTitle)

        Dim repoJobTitleDesp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobTitleDesp.FormatString = ""
        repoJobTitleDesp.HeaderText = "Job Title Description"
        repoJobTitleDesp.Name = colJobTitleDesp
        repoJobTitleDesp.Width = 200
        repoJobTitleDesp.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoJobTitleDesp)

        Dim repoNOOfPost As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNOOfPost.FormatString = ""
        repoNOOfPost.HeaderText = "No of Post"
        repoNOOfPost.Name = colNoOfPost
        repoNOOfPost.Width = 70
        repoNOOfPost.IsVisible = True
        repoNOOfPost.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNOOfPost)


        Dim repoCTCRange As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCTCRange.FormatString = ""
        repoCTCRange.HeaderText = "CTCRange"
        repoCTCRange.Name = colCTCRange
        repoCTCRange.Width = 70
        repoCTCRange.IsVisible = True
        repoCTCRange.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCTCRange)

        Dim repoMaxSal As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMaxSal.FormatString = ""
        repoMaxSal.HeaderText = "Max Sal"
        repoMaxSal.Name = colMaxSal
        repoMaxSal.Width = 70
        repoMaxSal.IsVisible = True
        repoMaxSal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMaxSal)

        Dim repoDRSal As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDRSal.FormatString = ""
        repoDRSal.HeaderText = "DeptReqSal"
        repoDRSal.Name = ColDeptReqSal
        repoDRSal.Width = 70
        repoDRSal.IsVisible = True
        repoDRSal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDRSal)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True

        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
        
        ReStoreGridLayout()

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RequesitionApproval)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnApprove.Visible = MyBase.isModifyFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmRequesitionApprovel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnApprove, "Press Alt+S for Save Transaction")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+R for refresh")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadData(True)


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
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = ReportID
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridColumns = gv1.ColumnCount
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        End If

        ''richa agarwal regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        ''---------------
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow("Layout Deleted successfully", "Information")
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(True)
    End Sub
    Sub LoadData(ByVal isShowMsg As Boolean)
        LoadBlankGrid()
        Try
            If txtFromDate.Value > txtToDate.Value Then
                clsCommon.MyMessageBoxShow("FromDate should not be  greater than ToDate")
                Exit Sub
            End If
            Dim qry As String = ""
            qry += "select TSPL_HR_REQUISITION.Requisition_Code ,TSPL_HR_REQUISITION.Requisition_Description ,TSPL_HR_REQUISITION.CTCRange"
            qry += " ,CONVERT(FLOAT,RIGHT([CTCRange],LEN(CTCRange)- CHARINDEX('-', [CTCRange]) )) AS [MaxSal] , NoOfPost * CONVERT(FLOAT,RIGHT([CTCRange],LEN(CTCRange)- CHARINDEX('-', [CTCRange]) )) AS [DeptReqSal]  ,TSPL_HR_REQUISITION.Approved_Status,ISNULL(TSPL_HR_BUDGETING.Budget,0) As Budget,TSPL_HR_REQUISITION.Initiated_By ,TSPL_HR_REQUISITION.Recommended_By,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE  ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME  ,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_HR_REQUISITION.Job_Title_Code ,TSPL_HR_JOB_TITLE.Job_Title  ,TSPL_HR_REQUISITION.NoOfPost   from TSPL_HR_REQUISITION	"
            qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_HR_REQUISITION.DEPARTMENT_CODE =TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE "
            qry += " left outer join TSPL_HR_BUDGETING on TSPL_HR_BUDGETING.DEPARTMENT_CODE =TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_HR_REQUISITION.Location_Code =TSPL_LOCATION_MASTER.Location_Code "
            qry += " left outer join TSPL_HR_JOB_TITLE on TSPL_HR_REQUISITION.Job_Title_Code =TSPL_HR_JOB_TITLE.Job_Title_Code "
            qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_HR_REQUISITION.Recommended_By = TSPL_EMPLOYEE_MASTER.EMP_CODE "

            qry += "where TSPL_HR_REQUISITION.Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyy hh:mm tt") + "' and TSPL_HR_REQUISITION.Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyy hh:mm tt") + "' and Closed_Status<> 1"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            '    If isShowMsg Then
            '        clsCommon.MyMessageBoxShow("No Data found to Display", Me.Text)
            '    End If
            'End If
            'gv1.DataSource = dt
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colReqCode).Value = clsCommon.myCstr(dr("Requisition_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colReqDescription).Value = clsCommon.myCstr(dr("Requisition_Description"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colInitiatedBy).Value = clsCommon.myCstr(dr("Initiated_By"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRecomBy).Value = clsCommon.myCstr(dr("Recommended_By"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colRecomByName).Value = clsCommon.myCstr(dr("Emp_Name"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepCode).Value = clsCommon.myCstr(dr("DEPARTMENT_CODE"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepName).Value = clsCommon.myCstr(dr("DEPARTMENT_NAME"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation).Value = clsCommon.myCstr(dr("Location_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colJobTitle).Value = clsCommon.myCstr(dr("Job_Title_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colJobTitleDesp).Value = clsCommon.myCstr(dr("Job_Title"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfPost).Value = clsCommon.myCstr(dr("NoOfPost"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colBudget).Value = clsCommon.myCdbl(dr("Budget"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colCTCRange).Value = clsCommon.myCstr(dr("CTCRange"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colMaxSal).Value = clsCommon.myCdbl(dr("MaxSal"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(ColDeptReqSal).Value = clsCommon.myCdbl(dr("DeptReqSal"))


                If clsCommon.myCstr(dr("Approved_Status")) = "True" Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colChk).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colChk).ReadOnly = True
                End If
            Next
            ' Dim TotalDept As Double = clsCommon.myCdbl(dt.Compute("SUM(DeptReqSal)", " GROUP BY TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        savedata()
    End Sub
    Public Sub savedata()

        gv1.EndEdit()
        Dim qry As String
        Dim ApprovedBy As String = objCommonVar.CurrentUserCode
        Dim Approveddate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
        Dim requisitioncode As String
        Dim isOneChecked As Boolean = False

        Dim Budgetqry As String = ""
        Dim DeptCode As String = String.Empty
        Dim DeptName As String = String.Empty
        Dim TotalBudget As Double = 0
        Dim Budget As Double = 0
        Dim ChkReqCode As String = ""
        Dim strtotal As String = ""

        For J As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(J).Cells(colChk).Value = True Then
                ChkReqCode = clsCommon.myCstr(gv1.Rows(J).Cells(colReqCode).Value)
                If clsCommon.myLen(ChkReqCode) > 0 Then
                    strtotal = strtotal + "," + "'" + ChkReqCode + "'"
                End If
            End If
        Next
       
        If strtotal.Length > 0 Then
            If strtotal.Substring(0, 1) = "," Then
                strtotal = strtotal.Substring(1, strtotal.Length - 1)
            End If
        End If


        Budgetqry += " SELECT DEPARTMENT_CODE ,SUM(CONVERT(INTEGER,NOOFPOST)) AS NofPost ,SUM(DeptReqSal) AS DeptReqSal,SUM(Budget) Budget,MAX(DEPARTMENT_NAME) AS [DEPARTMENT_NAME] FROM ( "
        Budgetqry += " SELECT  TSPL_HR_REQUISITION.Requisition_Code ,TSPL_HR_REQUISITION.Requisition_Description ,TSPL_HR_REQUISITION.CTCRange , " & _
                     " RIGHT([CTCRange],LEN(CTCRange)- CHARINDEX('-', [CTCRange])) AS [MaxSal] , NoOfPost * CONVERT(FLOAT,RIGHT([CTCRange],LEN(CTCRange)- CHARINDEX('-', [CTCRange]) )) AS [DeptReqSal]  , " & _
                     " TSPL_HR_REQUISITION.Approved_Status,ISNULL(TSPL_HR_BUDGETING.Budget,0) As Budget,TSPL_HR_REQUISITION.Initiated_By ,TSPL_HR_REQUISITION.Recommended_By,TSPL_EMPLOYEE_MASTER.Emp_Name, " & _
                     " TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME  ,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_HR_REQUISITION.Job_Title_Code ,TSPL_HR_JOB_TITLE.Job_Title ,TSPL_HR_REQUISITION.NoOfPost " & _
                     " FROM TSPL_HR_REQUISITION  " & _
                     " LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER on TSPL_HR_REQUISITION.DEPARTMENT_CODE =TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE   " & _
                     " LEFT OUTER JOIN TSPL_HR_BUDGETING ON TSPL_HR_BUDGETING.DEPARTMENT_CODE =TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE  " & _
                     " LEFT OUTER JOIN TSPL_LOCATION_MASTER on TSPL_HR_REQUISITION.Location_Code =TSPL_LOCATION_MASTER.Location_Code   " & _
                     " LEFT OUTER JOIN TSPL_HR_JOB_TITLE on TSPL_HR_REQUISITION.Job_Title_Code =TSPL_HR_JOB_TITLE.Job_Title_Code   " & _
                     " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_HR_REQUISITION.Recommended_By = TSPL_EMPLOYEE_MASTER.EMP_CODE "
        Budgetqry += " WHERE TSPL_HR_REQUISITION.Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyy hh:mm tt") + "' and TSPL_HR_REQUISITION.Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyy hh:mm tt") + "' and Closed_Status<> 1 AND TSPL_HR_BUDGETING.Is_Applied =1 "
        Budgetqry += " AND TSPL_HR_REQUISITION.Requisition_Code IN (" & strtotal & ")"
        Budgetqry += "  ) XXX  "
        Budgetqry += " GROUP BY XXX.DEPARTMENT_CODE "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Budgetqry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            'DeptCode = clsCommon.myCstr(dt.Rows(0)("department_code"))
            'TotalBudget = clsCommon.myCdbl(dt.Rows(0)("DeptReqSal"))
            'Budget = clsCommon.myCdbl(dt.Rows(0)("Budget"))
            For Each dr As DataRow In dt.Rows
                DeptCode = clsCommon.myCstr(dr("department_code"))
                DeptName = clsCommon.myCstr(dr("department_name"))
                TotalBudget = clsCommon.myCdbl(dr("DeptReqSal"))
                Budget = clsCommon.myCdbl(dr("Budget"))
                If TotalBudget > Budget Then
                    clsCommon.MyMessageBoxShow("Please update budget according to requisition for department " & DeptName & "", Me.Text)
                    Exit Sub
                End If
            Next
        End If

        
        For Each grow As GridViewRowInfo In gv1.Rows
            If grow.Cells(colChk).Value = True Then
                isOneChecked = True
            End If
        Next
        If isOneChecked = False Then
            clsCommon.MyMessageBoxShow("Please select atleast one requisition")
            Exit Sub
        End If
        For i As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(i).Cells(colChk).Value = True Then
                '  requisitioncode = gv1.CurrentRow.Cells(colReqCode).Value
                requisitioncode = clsCommon.myCstr(gv1.Rows(i).Cells(colReqCode).Value)
                qry = "update TSPL_HR_REQUISITION set Approved_By='" + ApprovedBy + "' , Approved_Date='" + Approveddate + "',Approved_Status='1' where TSPL_HR_REQUISITION.Requisition_Code='" + gv1.Rows(i).Cells(colReqCode).Value + "' "
                clsDBFuncationality.ExecuteNonQuery(qry)
            End If

        Next
        clsCommon.MyMessageBoxShow("Data approved successfully", Me.Text)
        LoadData(True)
    End Sub

    Private Sub FrmRequesitionApprovel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnRefresh.Enabled Then
            LoadData(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnApprove.Enabled Then
            savedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

   

    Private Sub gv1_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        'If Not isInsideLoadData Then
        Try
            If (clsCommon.myCstr(e.RowElement.RowInfo.Cells(colChk).Value) = True And clsCommon.myCstr(e.RowElement.RowInfo.Cells(colChk).ReadOnly) = True) Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.ForeColor = Color.Black
                e.RowElement.BackColor = Color.LightGreen
               
            Else
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End If
        Catch ex As Exception

        End Try
        'End If
    End Sub

   
End Class
