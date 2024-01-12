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

Public Class frmScheduleForTraining
    Inherits FrmMainTranScreen
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()


#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String

    Const colCode As String = "COLCODE"

    Const Col_DOC_Date As String = "Col_DOC_Date"
    Const Col_Emp_Code As String = "Col_Emp_Code"
    Const Col_Emp_Name As String = "Col_Emp_Name"
    Const Col_Remark As String = "Col_Remark"
   
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim is_Entered_Manually As Boolean = False
    
#End Region

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmMilkPurchaseInvoice)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub SaveData()
        Dim trans As SqlTransaction = Nothing
        Try
            If (AllowToSave()) Then
                trans = clsDBFuncationality.GetTransactin()
                Dim objHead As clsScheduleForTraining
                '' asign screen vaules in objHead
                objHead = New clsScheduleForTraining
                objHead.Doc_Code = clsCommon.myCstr(txtCode.Value)
                objHead.Doc_Date = clsCommon.myCDate(dtpDocDate.Value)
                objHead.Remarks = clsCommon.myCstr(Me.txtRemark.Text)
                objHead.Trainer_Code = clsCommon.myCstr(Me.fndTraininer.Value)
                objHead.Schedule_End_Date = clsCommon.myCDate(Format(DtpScheduleEndDate.Value, "dd-MMM-yyyy ") & Format(DtpScheduleEndTime.Value, "hh:mm:ss tt"))
                objHead.Schedule_Start_Date = clsCommon.myCDate(Format(DtpScheduleStartDate.Value, "dd-MMM-yyyy ") & Format(DtpScheduleStartTime.Value, "hh:mm:ss tt"))
                objHead.Training_Course = clsCommon.myCstr(Me.fndTrainingCourse.Value)
                objHead.Training_Mode = clsCommon.myCstr(Me.CmbTrainingMode.SelectedValue)
                objHead.Venue = clsCommon.myCstr(Me.CmbVenue.SelectedValue)


                Dim objList As New List(Of clsScheduleForTrainingDetail)

                Dim obj As clsScheduleForTrainingDetail
                For Each grow As GridViewRowInfo In gv1.Rows
                    obj = New clsScheduleForTrainingDetail
                    obj.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    obj.DOC_Request_Code = clsCommon.myCstr(grow.Cells(colCode).Value)
                    obj.Emp_Code = clsCommon.myCstr(grow.Cells(Col_Emp_Code).Value)
                    obj.Remark = clsCommon.myCstr(grow.Cells(Col_Remark).Value)
                    objList.Add(obj)
                Next

                Dim objList_Resource As New List(Of clsScheduleForTrainingResourceDetail)

                Dim obj_Resource As clsScheduleForTrainingResourceDetail
                For Each grow As String In cbgResource.CheckedValue
                    obj_Resource = New clsScheduleForTrainingResourceDetail
                    obj_Resource.DOC_CODE = clsCommon.myCstr(txtCode.Value)
                    obj_Resource.DOC_Resource_Code = clsCommon.myCstr(grow)
                    objList_Resource.Add(obj_Resource)
                Next

                If clsScheduleForTraining.SaveData(objHead, objList, objList_Resource, trans) Then
                    trans.Commit()
                    UcAttachment1.SaveData(objHead.Doc_Code)
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(objHead.Doc_Code)
                End If

            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If btnsave.Text = "Update" Then
                Dim strchk As String = "select isPOSTED from TSPL_Schedule_Training_HEAD where DOC_COde='" + txtCode.Value + "'"
                Dim chkpost As String = clsDBFuncationality.getSingleValue(strchk)
                If chkpost = "1" Then
                    clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                    Return False
                End If
            End If
            If clsCommon.myLen(fndTraininer.Value) <= 0 Then
                myMessages.blankValue("Trainer Code")

                fndTraininer.Focus()
                fndTraininer.Select()
                Errorcontrol.SetError(fndTraininer, "Trainer Code")
                Return False
            Else
                Errorcontrol.ResetError(fndTraininer)
            End If

            If clsCommon.myLen(fndTrainingCourse.Value) <= 0 Then
                myMessages.blankValue("Training Course")

                fndTrainingCourse.Focus()
                fndTrainingCourse.Select()
                Errorcontrol.SetError(fndTrainingCourse, "Training Course")
                Return False
            Else
                Errorcontrol.ResetError(fndTrainingCourse)
            End If
            If (DtpScheduleStartDate.Value) > (DtpScheduleEndDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "StartDate should not be greater than EndDate ", Me.Text)
                Return False
            End If
            Dim GridRow As Integer = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colCode).Value) <= 0 Then
                    Throw New Exception("Code can't be left blank")
                    'GridRow = GridRow + 1
                End If

            Next

            'If clsCommon.myCDate(DtpScheduleStartTime.Value) = "" Then
            '    clsCommon.MyMessageBoxShow("Please fill the StartTime ")
            '    Return False
            'End If

            'If clsCommon.myCDate(DtpScheduleEndTime.Value) = "" Then
            '    clsCommon.MyMessageBoxShow("Please fill the EndTime ")
            '    Return False
            'End If

            'DtpScheduleStartDate.Value = obj.Schedule_Start_Date
            'DtpScheduleEndDate.Value = obj.Schedule_End_Date
            'DtpScheduleStartTime.Value = obj.Schedule_Start_Time
            'DtpScheduleEndTime.Value = obj.Schedule_End_TIme


            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        Me.DtpScheduleEndTime.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm:ss tt")
        Me.DtpScheduleStartTime.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "hh:mm:ss tt")
        Me.txtRemark.Text = ""
        Me.fndTraininer.Value = ""
        fndTrainingCourse.Value = ""
        lblTrainingCourseDesc.Text = ""
        lblTrainerName.Text = ""
        btnsave.Enabled = True
        BtnPost.Enabled = False
        btndelete.Enabled = False
        CmbVenue.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        Load_Resource_Type()
        Load_Training_Mode_Type()
        Load_Venue_Type()
        LoadBlankGrid()
        gv1.Rows.AddNew()
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
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        'gv1.AllowAddNewRow = True
        'gv1.AllowDeleteRow = False
        'gv1.AllowEditRow = True
        'gv1.MasterTemplate.AllowCellContextMenu = True
        'gv1.MasterTemplate.AllowColumnHeaderContextMenu = True
        'gv1.MasterTemplate.AllowDeleteRow = True
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

            LoadBlankGrid()
            btnsave.Enabled = True
            BtnPost.Enabled = True
            btndelete.Enabled = True
            Dim obj As clsScheduleForTraining = clsScheduleForTraining.GetData(strDoc, navType)
            btnsave.Text = "Update"
            txtCode.Value = obj.Doc_Code
            dtpDocDate.Value = obj.Doc_Date

            DtpScheduleStartDate.Value = obj.Schedule_Start_Date
            DtpScheduleEndDate.Value = obj.Schedule_End_Date
            DtpScheduleStartTime.Text = obj.Schedule_Start_Time
            DtpScheduleEndTime.Text = obj.Schedule_End_TIme

            fndTraininer.Value = obj.Trainer_Code
            lblTrainerName.Text = clsDBFuncationality.getSingleValue("select Doc_Name from Tspl_Trainer_Master where Doc_Code='" & obj.Trainer_Code & "'")

            fndTrainingCourse.Value = obj.Training_Course
            lblTrainingCourseDesc.Text = clsDBFuncationality.getSingleValue("select Name from Tspl_Training_Master where Code='" & obj.Training_Course & "'")


            txtRemark.Text = obj.Remarks
            CmbTrainingMode.SelectedValue = obj.Training_Mode
            CmbVenue.Text = obj.Venue
            UsLock1.Status = obj.POSTED


            If obj.POSTED = ERPTransactionStatus.Approved Then
                btnsave.Enabled = False
                BtnPost.Enabled = False
                btndelete.Enabled = False
            End If

            cbgResource.CheckedValue = obj.objResource
            If obj.Doc_Code = Nothing Then
                LoadBlankGrid()
            Else
                If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                    For Each obj1 As clsScheduleForTrainingDetail In obj.ObjList
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_Request_Code
                        If obj1.DOC_R_Date <> "" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(Col_DOC_Date).Value = obj1.DOC_R_Date
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Code).Value = obj1.Emp_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Name).Value = obj1.Emp_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Remark).Value = obj1.Remark
                    Next
                Else
                    gv1.Rows.AddNew()
                End If
            End If
            UcAttachment1.LoadData(obj.Doc_Code)
            isInsideLoadData = False
            'AddNew() 
            'gv1.Rows.AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Code"
        repoCode.Name = colCode
        repoCode.Width = 150
        repoCode.IsVisible = True
        repoCode.ReadOnly = True
        repoCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoTaskDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoTaskDate.Format = DateTimePickerFormat.Custom
        repoTaskDate.CustomFormat = "dd-MMM-yyyy"
        repoTaskDate.HeaderText = "Doc Date"
        repoTaskDate.FormatString = "{0:d}"
        repoTaskDate.Name = Col_DOC_Date
        repoTaskDate.WrapText = True
        repoTaskDate.ReadOnly = True
        repoTaskDate.Width = 150
        gv1.MasterTemplate.Columns.Add(repoTaskDate)

        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Emp Code"
        repoEmpCode.Name = Col_Emp_Code
        repoEmpCode.Width = 150
        repoEmpCode.IsVisible = True
        repoEmpCode.ReadOnly = True
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoEmpCode)


        Dim repoProjectCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectCode.FormatString = ""
        repoProjectCode.HeaderText = "Emp Name"
        repoProjectCode.Name = Col_Emp_Name
        repoProjectCode.Width = 200
        repoProjectCode.ReadOnly = True
        repoProjectCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectCode)

        Dim repoProjectDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProjectDesc.FormatString = ""
        repoProjectDesc.HeaderText = "Remark"
        repoProjectDesc.Name = Col_Remark
        repoProjectDesc.Width = 350
        repoProjectDesc.ReadOnly = True
        repoProjectDesc.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repoProjectDesc)

        gv1.Columns.Distinct() '.DistinctValues(3)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        'gv1.AllowDeleteRow = False

        'gv1.AllowEditRow = True
        'gv1.ShowGroupPanel = False
        'gv1.AllowColumnReorder = True
        'gv1.AllowRowReorder = False
        'gv1.EnableSorting = True
        'gv1.EnableFiltering = True
        'gv1.EnableAlternatingRowColor = True
        'gv1.AutoSizeRows = False
        'gv1.AllowRowResize = True
        ''gv1.VerticalScrollState = ScrollState.AlwaysShow
        ''gv1.HorizontalScrollState = ScrollState.AlwaysShow
        ''gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        'gv1.MasterTemplate.ShowRowHeaderColumn = False
        'gv1.TableElement.TableHeaderHeight = 40
        ''gv1.ShowFilteringRow = True
        'ReStoreGridLayout()

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub
    '' ------------------------------------ Requested Data Acc. To Trianing Course -------------------------------------------------------------
    Sub LoadReqData(ByVal strCode As String)

        Try
            Dim obj As New ClsRequestForTrainingMaster()
            obj = ClsRequestForTrainingMaster.GetRDData(strCode)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Training_Code) > 0) Then

                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    isInsideLoadData = True
                    LoadBlankGrid()

                    For Each objTr As ClsRequestForTrainingMaster In obj.ObjList
                        gv1.Rows.AddNew()
                        ii = ii + 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = objTr.Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Code).Value = objTr.Employee_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Remark).Value = objTr.Remark
                        gv1.Rows(gv1.Rows.Count - 1).Cells(Col_DOC_Date).Value = objTr.Doc_Date
                        If clsCommon.myLen(objTr.Employee_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Name).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE ='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Code).Value) + "'"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(Col_Emp_Name).Value = ""
                        End If
                    Next
                End If
            Else
                isNewEntry = True
                Me.gv1.Rows.Clear()
                Me.gv1.Rows.AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    '' -----------------------------------------------------------------------------------------------------------------------------------------
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

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False And e.Column Is gv1.Columns(colCode) Then
                Dim obj As New clsScheduleForTrainingDetail
                obj = obj.Open_request_Code_finder(gv1.CurrentRow.Cells(colCode).Value)
                If obj IsNot Nothing Then
                    isInsideLoadData = True
                    gv1.CurrentRow.Cells(Col_Remark).Value = ""
                    gv1.CurrentRow.Cells(Col_Emp_Name).Value = obj.Emp_Name
                    gv1.CurrentRow.Cells(Col_Emp_Code).Value = obj.Emp_Code
                    gv1.CurrentRow.Cells(colCode).Value = obj.DOC_Request_Code
                    gv1.CurrentRow.Cells(Col_DOC_Date).Value = obj.DOC_R_Date
                    isInsideLoadData = False
                End If
            ElseIf isInsideLoadData = False And e.Column Is gv1.Columns(Col_Emp_Code) Then
                Dim obj As New clsScheduleForTrainingDetail
                obj = obj.Open_Employee_Code_finder(gv1.CurrentRow.Cells(Col_Emp_Code).Value)
                If obj IsNot Nothing Then
                    isInsideLoadData = True
                    gv1.CurrentRow.Cells(Col_Emp_Name).Value = obj.Emp_Name
                    gv1.CurrentRow.Cells(Col_Emp_Code).Value = obj.Emp_Code
                    gv1.CurrentRow.Cells(Col_Remark).Value = ""
                    isInsideLoadData = False
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do You want to delete this Row Permanently . Are You Sure.?", "Delete Row", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim sQuery As String = "delete from Tspl_Schedule_Training_Employee_Detail where DOC_Code='" & gv1.CurrentRow.Cells("DOC CODE").Value & "' and Request_Code='" & gv1.CurrentRow.Cells(Col_Emp_Code).Value & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery)
            Else
                e.Cancel = True
            End If
        Catch ex As Exception
        End Try
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
                If (clsScheduleForTraining.PostData(txtCode.Value, True)) Then
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
                common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                LoadData(txtCode.Value)
                'If (common.clsCommon.MyMessageBoxShow("Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                '    PrintDataNew()
                'End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Private Sub fndTraininer__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTraininer._MYValidating
        Try
            Dim sQuery As String = "select * from Tspl_Trainer_Master"
            fndTraininer.Value = clsCommon.ShowSelectForm("Trainer", sQuery, "Doc_Code", " coalesce(Is_Applicable,0)=1 ", fndTraininer.Value, "Doc_Code", isButtonClicked)
            lblTrainerName.Text = clsDBFuncationality.getSingleValue("select Doc_Name from Tspl_Trainer_Master where Doc_Code='" & fndTraininer.Value & "'")
            Load_Venue_Type()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub fndTrainingCourse__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTrainingCourse._MYValidating
        Try
            'Dim sQuery As String = "select code,name from Tspl_Training_Master"
            If clsCommon.myLen(fndTraininer.Value) > 0 Then
                Dim sQuery As String = "SELECT Course_Code AS Code,Name  From Tspl_Trainer_Master_Course LEFT OUTER JOIN Tspl_Training_Master ON Tspl_Trainer_Master_Course.Course_Code = Tspl_Training_Master.Code "
                fndTrainingCourse.Value = clsCommon.ShowSelectForm("Training", sQuery, "Code", " Tspl_Trainer_Master_Course.DOC_Code  ='" + fndTraininer.Value + "' ", fndTrainingCourse.Value, "Code", isButtonClicked)
                lblTrainingCourseDesc.Text = clsDBFuncationality.getSingleValue("select Name from Tspl_Training_Master where Code='" & fndTrainingCourse.Value & "'")
                LoadReqData(fndTrainingCourse.Value)
            Else
                clsCommon.MyMessageBoxShow("Please fill trainer code first")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

   
End Class
