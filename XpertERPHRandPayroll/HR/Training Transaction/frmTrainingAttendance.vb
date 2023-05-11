Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports System.IO
Imports common
Imports XpertERPEngine

Public Class frmTrainingAttendance
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const ColCode As String = "Col_Code"
    Const Col_Doc_Date As String = "Col_Doc_Code"
    Const Col_Emp_Code As String = "Col_Emp_Code"
    Const Col_Emp_Name As String = "Col_Emp_Name"
    Const Col_Dept As String = "Col_Dept"

    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim is_Entered_Manually As Boolean = False

#End Region

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMilkPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'BtnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Function GetNoOfDays() As Integer
        Dim NoOfDays As Integer = (DtpScheduleEndDate.Value.AddDays(1) - DtpScheduleStartDate.Value.AddDays(-1)).Days
        Return NoOfDays
    End Function

    Sub SaveData()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (AllowToSave()) Then
                Dim objT As clsTrainingAttendance = Nothing
                Dim objList As New List(Of clsTrainingAttendanceDetail)()
                Dim noofdays As Integer
                noofdays = GetNoOfDays()

                For Each grow As GridViewRowInfo In gv1.Rows
                    For i As Integer = 0 To noofdays - 1
                        If (Not String.IsNullOrEmpty(grow.Cells(Col_Emp_Code).Value)) Then
                            If (Not String.IsNullOrEmpty(grow.Cells("" + clsCommon.myCstr(DtpScheduleStartDate.Value.AddDays(i).Day & MonthName(DtpScheduleStartDate.Value.AddDays(i).Month, True)) + "").Value)) Then
                                Dim obj As New clsTrainingAttendanceDetail()
                                obj.Emp_Code = clsCommon.myCstr(grow.Cells(Col_Emp_Code).Value)
                                obj.Attendance = clsCommon.myCstr(DtpScheduleStartDate.Value.AddDays(i))
                                obj.USER_Status = clsCommon.myCstr(grow.Cells("" + clsCommon.myCstr(DtpScheduleStartDate.Value.AddDays(i).Day & MonthName(DtpScheduleStartDate.Value.AddDays(i).Month, True)) + "").Value)
                                objList.Add(obj)
                            End If
                        End If
                    Next
                Next
                If objList Is Nothing OrElse objList.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No Data found to save", Me.Text)
                ElseIf clsTrainingAttendance.SaveData(objT, objList, trans) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            'If btnsave.Text = "Update" Then
            '    Dim strchk As String = "select isPOSTED from TSPL_Training_Attendance where DOC_COde='" + txtCode.Value + "'"
            '    Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
            '    If chkpost = "1" Then
            '        clsCommon.MyMessageBoxShow("Transection already posted")
            '        Return False
            '    End If
            'End If


            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function

    Sub Load_Training_Mode_Type()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "On-Line"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Off-Line"
        dt.Rows.Add(dr)

        CmbTrainingMode.DataSource = dt
        CmbTrainingMode.ValueMember = "Code"
        CmbTrainingMode.DisplayMember = "Name"
    End Sub

    Sub Load_Venue_Type()
        Dim sQuery As String = "select cm.City_Code as code,City_Name as name from Tspl_Trainer_Master_City mc " _
        & " left join TSPL_CITY_MASTER cm on cm.City_Code=mc.City_code  where doc_code='" & fndTraininer.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        CmbVenue.DataSource = dt
        CmbVenue.ValueMember = "Code"
        CmbVenue.DisplayMember = "Name"
    End Sub

    Sub Load_Resource_Type()
        Dim sQuery As String = "select  code, name from Tspl_Training_Resource_Master"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        cbgResource.DataSource = dt
        cbgResource.ValueMember = "Code"
        cbgResource.DisplayMember = "Name"
    End Sub

    Sub AddNew()
        ' isNewEntry = True
        txtCode.Value = ""
        btnsave.Text = "Save"
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        txtRemark.Text = ""
        gv1.DataSource = Nothing
        Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        Me.DtpScheduleEndDate.Value = clsCommon.GETSERVERDATE()
        Me.DtpScheduleStartDate.Value = clsCommon.GETSERVERDATE()
        Me.DtpScheduleEndTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm:ss tt")
        Me.DtpScheduleStartTime.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm:ss tt")
        Me.txtRemark.Text = ""
        Me.fndTraininer.Value = ""
        fndTrainingCourse.Value = ""
        lblTrainingCourseDesc.Text = ""
        lblTrainerName.Text = ""
        btnsave.Enabled = True
        'BtnPost.Enabled = True
        'btndelete.Enabled = True

        Load_Resource_Type()
        Load_Training_Mode_Type()
        Load_Venue_Type()


        'For Custom Fields
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.BlankAllControls()
        End If
        UcAttachment1.BlankAllControls()
        ''End of For Custom Fields
    End Sub



#End Region

#Region "Events"

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsScheduleForTraining.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmMilkReceiptMCC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ' globalFunc.mandatoryText(fnddesig.txtValue, txtdes)

        ''For Custom Fields
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachment

        ''End of For Custom Fields
        AddNew()
        ' LoadData(Me.txtCode.Value)
        'LoadBlankGrid()
        gv1.AllowAddNewRow = True
        gv1.AllowDeleteRow = False
        gv1.AllowEditRow = True
        gv1.MasterTemplate.AllowCellContextMenu = True
        gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        gv1.MasterTemplate.AllowDeleteRow = True
        ReStoreGridLayout()
    End Sub


    Private Sub frmMilkReceiptMCC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
#End Region

    Public Sub LoadData(ByVal strDoc As String, Optional ByVal navType As NavigatorType = NavigatorType.Current)
        Try
            isInsideLoadData = True
            ' AddNew()
            Dim start_date As Date
            start_date = DtpScheduleStartDate.Value
            LoadBlankGrid()
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            Dim obj As clsTrainingAttendance = clsTrainingAttendance.GetData(strDoc, navType)
            btnsave.Text = "Update"
            txtCode.Value = obj.Doc_Code
            dtpDocDate.Value = obj.Doc_Date

            DtpScheduleStartDate.Value = obj.Schedule_Start_Date
            DtpScheduleEndDate.Value = obj.Schedule_End_Date
            DtpScheduleStartTime.Value = obj.Schedule_Start_Time
            DtpScheduleEndTime.Value = obj.Schedule_End_TIme

            fndTraininer.Value = obj.Trainer_Code
            lblTrainerName.Text = clsDBFuncationality.getSingleValue("select Doc_Name from Tspl_Trainer_Master where Doc_Code='" & obj.Trainer_Code & "'")

            fndTrainingCourse.Value = obj.Training_Course
            lblTrainingCourseDesc.Text = clsDBFuncationality.getSingleValue("select Name from Tspl_Training_Master where Code='" & obj.Training_Course & "'")


            txtRemark.Text = obj.Remarks
            CmbTrainingMode.SelectedValue = obj.Training_Mode
            CmbVenue.SelectedValue = obj.Venue
            UsLock1.Status = obj.POSTED


            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                BtnPost.Enabled = False
                btndelete.Enabled = False
            End If

            cbgResource.CheckedValue = clsTrainingAttendance.objResource
            DtpScheduleStartDate.Value = start_date
            If (clsTrainingAttendance.ObjList IsNot Nothing AndAlso clsTrainingAttendance.ObjList.Count > 0) Then
                For Each obj1 As clsTrainingAttendanceDetail In clsTrainingAttendance.ObjList
                    gv1.Rows.AddNew()
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(ColCode).Value = obj1.DOC_Request_Code
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Doc_Date).Value = obj1.DOC_R_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Code).Value = obj1.Emp_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Name).Value = obj1.Emp_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Dept).Value = obj1.Dept
                    ' gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Remark).Value = obj1.Remark
                    If (clsTrainingAttendance.dt_saved_Attendance.Rows.Count > 0) Then
                        If (clsTrainingAttendance.dt_saved_Attendance.Select("Emp_Code='" & obj1.Emp_Code & "'").Length > 0) Then
                            For ix As Integer = 0 To GetNoOfDays() - 1
                                Dim dr() As DataRow = clsTrainingAttendance.dt_saved_Attendance.Select("Emp_Code='" & obj1.Emp_Code & "' and attendance='" & Format(CDate(DtpScheduleStartDate.Value.AddDays(ix)), "dd-MMM-yyyy") & "'")
                                If dr.Length > 0 Then
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(DtpScheduleStartDate.Value.AddDays(ix).Day & MonthName(DtpScheduleStartDate.Value.AddDays(ix).Month, True)).Value = dr(0)("user_status")
                                End If
                            Next
                        End If
                    End If
a:              Next
            Else
                gv1.Rows.AddNew()
            End If
            DtpScheduleStartDate.Value = start_date
            UcAttachment1.LoadData(obj.Doc_Code)
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoUCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUCode.FormatString = ""
        repoUCode.HeaderText = "EMPLOYEE CODE"
        repoUCode.Name = Col_Emp_Code
        repoUCode.Width = 105
        repoUCode.IsVisible = True
        repoUCode.ReadOnly = True
        repoUCode.PinPosition = PinnedColumnPosition.Left
        gv1.MasterTemplate.Columns.Add(repoUCode)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "EMPLOYEE NAME"
        repoCode.Name = Col_Emp_Name
        repoCode.Width = 105
        repoCode.IsVisible = True
        repoCode.ReadOnly = True
        repoCode.PinPosition = PinnedColumnPosition.Left
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoDCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCode.FormatString = ""
        repoDCode.HeaderText = "DEPARTMENT"
        repoDCode.Name = Col_Dept
        repoDCode.Width = 105
        repoDCode.IsVisible = True
        repoDCode.ReadOnly = True
        repoDCode.PinPosition = PinnedColumnPosition.Left
        gv1.MasterTemplate.Columns.Add(repoDCode)

        For ix As Integer = 1 To GetNoOfDays() '(DtpScheduleEndDate.Value.AddDays(-1) - DtpScheduleStartDate.Value.AddDays(1)).Days
            Dim repoStatus1 As GridViewComboBoxColumn = New GridViewComboBoxColumn()
            repoStatus1.FormatString = ""
            repoStatus1.HeaderText = DtpScheduleStartDate.Value.AddDays(ix - 1).Day & MonthName(DtpScheduleStartDate.Value.AddDays(ix - 1).Month, True)
            repoStatus1.Name = DtpScheduleStartDate.Value.AddDays(ix - 1).Day & MonthName(DtpScheduleStartDate.Value.AddDays(ix - 1).Month, True)
            repoStatus1.Width = 90
            repoStatus1.ReadOnly = False
            repoStatus1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            repoStatus1.DataSource = GetStatus()
            repoStatus1.ValueMember = "Code"
            repoStatus1.DisplayMember = "Code"
            gv1.MasterTemplate.Columns.Add(repoStatus1)

        Next
        gv1.AddNewRowPosition = SystemRowPosition.Bottom
        gv1.AllowAddNewRow = False
    End Sub

    Public Shared Function GetStatus() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = " "
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "H"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Ist Half"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "IInd Half"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SLF"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "SLS"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "W"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = "select Doc_Code as Code,Doc_Date as Date from TSPL_Schedule_Training_HEAD"
            txtCode.Value = clsCommon.ShowSelectForm("SCH Training", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)


            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyUp
        'If e.KeyCode = Keys.Enter Then
        '    gv1.Rows.Add()
        'End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        'Try
        '    If clsCommon.MyMessageBoxShow("Do You want to delete this Row Permanently . Are You Sure.?", "Delete Row", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Dim sQuery As String = "delete from Tspl_Schedule_Training_Employee_Detail where DOC_Code='" & gv1.CurrentRow.Cells("DOC CODE").Value & "' and Request_Code='" & gv1.CurrentRow.Cells(Col_Emp_Code).Value & "'"
        '        clsDBFuncationality.ExecuteNonQuery(sQuery)
        '    Else
        '        e.Cancel = True
        '    End If
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing

            If (myMessages.postConfirm()) Then
                'SaveData()
                '              Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If (clsTrainingAttendance.PostData(txtCode.Value, True)) Then
                    '                   trans.Commit()
                    msg = "Successfully Posted"
                Else
                    'trans.Rollback()
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(txtCode.Value)
                'If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                '    PrintDataNew()
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub BtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnsaveLayout.Click
        '    If clsCommon.myLen(GetReportID()) > 0 Then
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = "HRTrainingSch"
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        End If

        ''richa agarwal regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        ''---------------
        'End If
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData("HRTrainingSch", objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData("HRTrainingSch", "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub fndTraininer__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Try
            Dim sQuery As String = "select * from Tspl_Trainer_Master"
            fndTraininer.Value = clsCommon.ShowSelectForm("Trainer", sQuery, "Doc_Code", "", fndTraininer.Value, "Doc_Code", isButtonClicked)
            lblTrainerName.Text = clsDBFuncationality.getSingleValue("select Doc_Name from Tspl_Trainer_Master where Doc_Code='" & fndTraininer.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub fndTrainingCourse__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
        Try
            Dim sQuery As String = "select code,name from Tspl_Training_Master"
            fndTrainingCourse.Value = clsCommon.ShowSelectForm("Training", sQuery, "Code", "", fndTrainingCourse.Value, "Code", isButtonClicked)
            lblTrainingCourseDesc.Text = clsDBFuncationality.getSingleValue("select Name from Tspl_Training_Master where Code='" & fndTrainingCourse.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

  
End Class
