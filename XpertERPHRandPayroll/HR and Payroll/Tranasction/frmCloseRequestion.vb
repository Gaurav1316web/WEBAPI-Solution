Imports XpertERPEngine
Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.Apoc
Imports System.Data.SqlClient
Imports System.IO
'--Preeti gupta--ticket no[BM00000003467][BM00000003730]
Public Class FrmCloseRequestion
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Const ReportID As String = "Req_Close"
    Const colChk As String = "Select"
    Const colHideChk As String = "hide"
    Const colReqCode As String = "colReqCode"
    Const colReqDescription As String = "colReqDescription"
    Const colInitiatedBy As String = "colInitiatedBy"
    Const colRecomBy As String = "colRecomBy"
    Const colDepName As String = "colDepName"
    Const colLocation As String = "colLocation"
    Const colJobTitle As String = "colJobTitle"
    Const colNoOfPost As String = "colNoOfPost"
    Dim isFlag As Boolean = False
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
        repReqDescription.Width = 100
        repReqDescription.IsVisible = True
        repReqDescription.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repReqDescription)



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
        repoRecomBy.HeaderText = "Recommended"
        repoRecomBy.Name = colRecomBy
        repoRecomBy.Width = 100
        repoRecomBy.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRecomBy)

        Dim repoDepName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDepName.FormatString = ""
        repoDepName.HeaderText = "Department Code"
        repoDepName.Name = colDepName
        repoDepName.Width = 200
        repoDepName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDepName)

        Dim repoLocation As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLocation.FormatString = ""
        repoLocation.HeaderText = "Location"
        repoLocation.Name = colLocation
        repoLocation.Width = 200
        repoLocation.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLocation)


        Dim repoJobTitle As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoJobTitle.FormatString = ""
        repoJobTitle.HeaderText = "Job Title"
        repoJobTitle.Name = colJobTitle
        repoJobTitle.Width = 200
        repoJobTitle.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoJobTitle)

        Dim repoNOOfPost As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNOOfPost.FormatString = ""
        repoNOOfPost.HeaderText = "No of Post"
        repoNOOfPost.Name = colNoOfPost
        repoNOOfPost.Width = 200
        repoNOOfPost.IsVisible = True
        repoNOOfPost.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNOOfPost)

        Dim repoHiddnChk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoHiddnChk.FormatString = "hide"
        repoHiddnChk.Name = colHideChk
        repoHiddnChk.Width = 10
        repoHiddnChk.IsVisible = False
        repoHiddnChk.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoHiddnChk)

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
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
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
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
        End If

        ''richa agarwal regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        ''---------------
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Deleted successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub FrmCloseRequestion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R AndAlso btnRefresh.Enabled Then
            LoadData(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnApprove.Enabled Then
            savedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmCloseRequestion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnApprove, "Press Alt+S for Save Trasnaction")
        ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+R for refresh")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadData(True)
    End Sub
    Sub LoadData(ByVal isShowMsg As Boolean)
        LoadBlankGrid()
        Try
            If txtFromDate.Value > txtToDate.Value Then
                clsCommon.MyMessageBoxShow(Me, "FromDate should not be  greater than ToDate", Me.Text)
                Exit Sub
            End If
            Dim qry As String = ""
            qry += "select TSPL_HR_REQUISITION.Requisition_Code ,TSPL_HR_REQUISITION.Requisition_Description ,TSPL_HR_REQUISITION.Approved_Status,TSPL_HR_REQUISITION.Closed_Status,TSPL_HR_REQUISITION.Initiated_By ,TSPL_HR_REQUISITION.Recommended_By ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME  ,TSPL_LOCATION_MASTER.Location_Desc  ,TSPL_HR_REQUISITION.Job_Title_Code  ,TSPL_HR_REQUISITION.NoOfPost   from TSPL_HR_REQUISITION	"
            qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_HR_REQUISITION.DEPARTMENT_CODE =TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE "
            qry += " left outer join TSPL_LOCATION_MASTER on TSPL_HR_REQUISITION.Location_Code =TSPL_LOCATION_MASTER.Location_Code "
            qry += " left outer join TSPL_HR_JOB_TITLE on TSPL_HR_REQUISITION.Job_Title_Code =TSPL_HR_JOB_TITLE.Job_Title_Code "

            qry += "where TSPL_HR_REQUISITION.Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyy hh:mm tt") + "' and TSPL_HR_REQUISITION.Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyy hh:mm tt") + "'"
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
                gv1.Rows(gv1.Rows.Count - 1).Cells(colDepName).Value = clsCommon.myCstr(dr("DEPARTMENT_NAME"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colLocation).Value = clsCommon.myCstr(dr("Location_Desc"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colJobTitle).Value = clsCommon.myCstr(dr("Job_Title_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfPost).Value = clsCommon.myCstr(dr("NoOfPost"))
                If clsCommon.myCstr(dr("Approved_Status")) = "True" Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHideChk).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHideChk).ReadOnly = True

                End If
                If clsCommon.myCstr(dr("Closed_Status")) = "True" Then
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colChk).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colChk).ReadOnly = True

                End If

            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(True)
    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        savedata()
    End Sub
    Public Sub savedata()
        gv1.EndEdit()
        Dim qry As String
        Dim qry1 As String
        Dim ApprovedBy As String = objCommonVar.CurrentUserCode
        Dim Approveddate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
        Dim isOneChecked As Boolean = False
        For Each grow As GridViewRowInfo In gv1.Rows
            If grow.Cells(colChk).Value = True Then
                isOneChecked = True
            End If
        Next
        If isOneChecked = False Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast one requisition ", Me.Text)
            Exit Sub
        End If
        For i As Integer = 0 To gv1.Rows.Count - 1
            If gv1.Rows(i).Cells(colChk).Value = True Then

                qry = "update TSPL_HR_REQUISITION set Closed_By='" + ApprovedBy + "' , Closed_Date='" + Approveddate + "',Closed_Status='1' where TSPL_HR_REQUISITION.Requisition_Code='" + gv1.Rows(i).Cells(colReqCode).Value + "' "
                clsDBFuncationality.ExecuteNonQuery(qry)
                qry1 = "update TSPL_HR_REQUISITION set Approved_By='" + ApprovedBy + "' , Approved_Date='" + Approveddate + "',Approved_Status='1' where TSPL_HR_REQUISITION.Requisition_Code='" + gv1.Rows(i).Cells(colReqCode).Value + "' "
                clsDBFuncationality.ExecuteNonQuery(qry1)
            End If
        Next

        clsCommon.MyMessageBoxShow(Me, "Requisition closed successfully", Me.Text)
        LoadData(True)

    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv1.RowFormatting
        Try
           
            If (clsCommon.myCBool(e.RowElement.RowInfo.Cells(colHideChk).Value) = True And clsCommon.myCBool(e.RowElement.RowInfo.Cells(colChk).ReadOnly) = False) Then
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

            If (clsCommon.myCBool(e.RowElement.RowInfo.Cells(colChk).ReadOnly) = True And clsCommon.myCBool(e.RowElement.RowInfo.Cells(colHideChk).ReadOnly) = True) Then
                e.RowElement.DrawFill = True
                e.RowElement.GradientStyle = GradientStyles.Solid
                e.RowElement.ForeColor = Color.Black
                e.RowElement.BackColor = Color.Pink

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

