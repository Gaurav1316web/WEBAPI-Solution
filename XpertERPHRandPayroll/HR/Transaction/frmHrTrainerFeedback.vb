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
Public Class FrmHrTrainerFeedback
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Const colDocCode As String = "colDocCode"
    Const colEmployeeCode As String = "colEmployeeCode"
    Const colEmployeeName As String = "colEmployeeName"
    Const colFeedback As String = "colFeedback"
    Dim isFlag As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Public Sub LoadBlankSchedule()

        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim DocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DocCode = New GridViewTextBoxColumn()
        DocCode.FormatString = ""
        DocCode.HeaderText = "Document Code"
        DocCode.Name = colDocCode
        DocCode.Width = 100
        DocCode.IsVisible = False
        gv.MasterTemplate.Columns.Add(DocCode)

        Dim EmployeeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        EmployeeCode = New GridViewTextBoxColumn()
        EmployeeCode.FormatString = ""
        EmployeeCode.HeaderText = "Employee Code"
        EmployeeCode.Name = colEmployeeCode
        EmployeeCode.Width = 100
        EmployeeCode.ReadOnly = True
      
        gv.MasterTemplate.Columns.Add(EmployeeCode)


        Dim EmployeeName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        EmployeeName = New GridViewTextBoxColumn()
        EmployeeName.FormatString = ""
        EmployeeName.HeaderText = "Employee Name"
        EmployeeName.Name = colEmployeeName
        EmployeeName.Width = 100
        EmployeeName.ReadOnly = True
       
        gv.MasterTemplate.Columns.Add(EmployeeName)


        Dim Feedback As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Feedback = New GridViewTextBoxColumn()
        Feedback.FormatString = ""
        Feedback.HeaderText = "Feedback"
        Feedback.Name = colFeedback
        Feedback.Width = 100
        Feedback.ReadOnly = False
       
        gv.MasterTemplate.Columns.Add(Feedback) '0

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40

    End Sub
    Sub LoadShedule()
        LoadBlankSchedule()
        Dim qry As String = "select TSPL_Schedule_Training_Employee_DETAIL.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name   from TSPL_Schedule_Training_Employee_DETAIL "
        qry += "left outer join TSPL_Schedule_Training_HEAD "
        qry += " on TSPL_Schedule_Training_Employee_DETAIL.DOC_CODE =TSPL_Schedule_Training_HEAD.Doc_Code left outer join "
        qry += " TSPL_EMPLOYEE_MASTER on TSPL_Schedule_Training_Employee_DETAIL.EMP_CODE =TSPL_EMPLOYEE_MASTER.EMP_CODE "
        qry += "where TSPL_Schedule_Training_HEAD.Doc_Code ='" + fndSchedule.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        For Each dr As DataRow In dt.Rows

            gv.Rows.AddNew()
            gv.Rows(gv.Rows.Count - 1).Cells(colEmployeeCode).Value = clsCommon.myCstr(dr("EMP_CODE"))
            gv.Rows(gv.Rows.Count - 1).Cells(colEmployeeName).Value = clsCommon.myCstr(dr("Emp_Name"))
        Next
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If

    End Sub


    Private Sub txtSchedule__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSchedule._MYValidating

        Dim qry As String

        qry = "select TSPL_Schedule_Training_Head .Doc_Code  from TSPL_Schedule_Training_Head "
        fndSchedule.Value = clsCommon.ShowSelectForm("TrainerM", qry, "Doc_Code", "isPosted='1'", fndSchedule.Value, "", isButtonClicked)

        If clsCommon.myLen(fndSchedule.Value) > 0 Then
            schedule_date.Text = clsDBFuncationality.getSingleValue("select Doc_Date from TSPL_Schedule_Training_Head where Doc_Code ='" + fndSchedule.Value + "' ")
        Else
            schedule_date.Text = ""
        End If
        If clsCommon.myLen(fndSchedule.Value) > 0 Then
            LoadShedule()
        End If
    End Sub
   
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHrTraineeFeedBack)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        Btnsave.Visible = MyBase.isModifyFlag
        BtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub FrmHrTrainerFeedback_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankSchedule()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(Btnsave, "Press Alt+S for Save/Update ")

        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(BtnDelete, "Press Alt+D Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post")
        addnew()
        If clsCommon.myLen(Me.Tag) > 0 Then
            getdata(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Function allowtosave() As Boolean
        Try


            If clsCommon.myLen(fndSchedule.Value) <= 0 Or clsCommon.myLen(clsCommon.myCstr(fndSchedule.Value)) > 30 Then
                myMessages.blankValue("Schedule Code")

                fndSchedule.Focus()
                fndSchedule.Select()
                Errorcontrol.SetError(fndSchedule, "Schedule Code")
                Return False
            Else
                Errorcontrol.ResetError(fndSchedule)
            End If
            Dim GridRow As Integer = 0
            For Each grow As GridViewRowInfo In gv.Rows
                If clsCommon.myLen(grow.Cells(colFeedback).Value) <= 0 Then
                    Throw New Exception("Feedback Cannot Be Blank For'" + clsCommon.myCstr(grow.Cells(colEmployeeCode).Value) + "'")
                    GridRow = GridRow + 1
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub savedata()
        Try
            If (allowtosave()) Then

                'Dim obj As clsTrainerfeedbackDetail
                Dim arr As New List(Of clsTrainerfeedbackDetail)
                Dim entry As String
                Dim count As Integer = 0
                Dim i As Integer = 0
                Dim qry As String = "select count(*) from TSPL_HR_TRAINER_FEEDBACK_Head  where Doc_Code ='" + txtCode.Value + "'"
                count = clsDBFuncationality.getSingleValue(qry)
                If count = 0 Then
                    isNewEntry = True
                Else
                    isNewEntry = False

                End If
                Dim Trainer As New clsTrainerfeedbackHead
                Trainer.DocCode = txtCode.Value
                Trainer.DocDate = txtDate.Text
                Trainer.Description = txtDescription.Text
                Trainer.ScheduleCode = fndSchedule.Value
                'Trainer .SchDate =
                For i = 0 To gv.Rows.Count - 1
                    Dim Traner As New clsTrainerfeedbackDetail
                    'Traner.DocCode = clsCommon.myCstr(gv.Rows(i).Cells(colDocCode).Value)
                    Traner.EmployeeCode = clsCommon.myCstr(gv.Rows(i).Cells(colEmployeeCode).Value)
                    Traner.Feedback = clsCommon.myCstr(gv.Rows(i).Cells(colFeedback).Value)
                    If clsCommon.myLen(Traner.EmployeeCode) > 0 Then
                        arr.Add(Traner)
                    End If
                    'End If
                Next
                If (clsTrainerfeedbackHead.savedata(Trainer, isNewEntry, arr)) Then

                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        entry = Trainer.DocCode
                        getdata(Trainer.DocCode, NavigatorType.Current)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                    End If
                    'LoadData(obj.Job_Title_Code, NavigatorType.Current)
                    '    Btnsave.Text = "Update"
                    '    BtnDelete.Enabled = True
                    'Else
                    '    Btnsave.Text = "Save"
                    '    BtnDelete.Enabled = False
                End If
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub
    Sub getdata(ByVal entry As String, ByVal navigatortype As NavigatorType)
        Try
            gv.Rows.Clear()

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim obj As clsTrainerfeedbackHead = clsTrainerfeedbackHead.getdata(entry, navigatortype, trans)
            If obj IsNot Nothing Then


                Dim ScheduleDate As String
                txtCode.Value = obj.DocCode
                txtDate.Text = obj.DocDate
                txtDescription.Text = obj.Description
                fndSchedule.Value = obj.ScheduleCode
                ScheduleDate = clsDBFuncationality.getSingleValue("select Doc_Date from TSPL_Schedule_Training_Head  where Doc_Code='" + fndSchedule.Value + "'", trans)
                schedule_date.Text = ScheduleDate
                For Each objt As clsTrainerfeedbackDetail In obj.arr
                    gv.Rows.AddNew()
                    gv.Rows(gv.RowCount - 1).Cells(colDocCode).Value = objt.DocCode
                    gv.Rows(gv.RowCount - 1).Cells(colEmployeeCode).Value = objt.EmployeeCode
                    gv.Rows(gv.RowCount - 1).Cells(colFeedback).Value = objt.Feedback
                    gv.Rows(gv.RowCount - 1).Cells(colEmployeeName).Value = objt.EmployeeName

                    txtCode.MyReadOnly = True
                    Btnsave.Text = "Update"
                    BtnDelete.Enabled = True

                    btnPost.Enabled = True

                    If obj.Posted = ERPTransactionStatus.Approved Then
                        Btnsave.Enabled = False
                        btnPost.Enabled = False
                        BtnDelete.Enabled = False
                    End If
                    UsLock1.Status = obj.Posted
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub addnew()
        txtCode.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE()
        txtDescription.Text = ""
        fndSchedule.Value = ""
        LoadBlankSchedule()
        schedule_date.Text = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        Btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        Btnsave.Enabled = True
        btnPost.Enabled = False
        BtnDelete.Enabled = False
    End Sub

    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        savedata()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        addnew()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        getdata(txtCode.Value, NavType)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim Qry As String
        
        Qry = "select * from TSPL_HR_TRAINER_FEEDBACK_Head "
        txtCode.Value = clsCommon.ShowSelectForm("TRAINER_FEEDBACK", Qry, "Doc_Code", "", txtCode.Value, "", isButtonClicked)
        If clsCommon.myLen(txtCode.Value) > 0 Then
            getdata(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Doc_Code As String = ""
            isFlag = True
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Doc_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS Doc_Code from TSPL_HR_TRAINER_FEEDBACK_Head  where Doc_Code='" + txtCode.Value + "'"))
                If Doc_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        savedata()
                        If (clsTrainerfeedbackHead.PostData(MyBase.Form_ID, txtCode.Value)) Then
                            'msg = "Successfully Posted"
                            'common.clsCommon.MyMessageBoxShow(msg)
                            getdata(txtCode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering Document code")
                End If

            Else
                Throw New Exception("Document code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If clsCommon.myLen(txtCode.Value) > 0 Then
            'If (myMessages.postConfirm()) Then
            PostData()
            'End If
        Else
            clsCommon.MyMessageBoxShow(Me, "code not found to post", Me.Text)
        End If
    End Sub

    Private Sub FrmHrTrainerFeedback_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            addnew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso Btnsave.Enabled Then
            savedata()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso BtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsTrainerfeedbackHead.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    addnew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "code not found to delete", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        DeleteData()

    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()

    End Sub
End Class
