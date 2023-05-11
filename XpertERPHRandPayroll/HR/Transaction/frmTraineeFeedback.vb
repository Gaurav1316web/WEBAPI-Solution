Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports common
Imports XpertERPEngine
'---Preeti gupta Ticket No.[BM00000003517]
Public Class FrmTraineeFeedback
    Inherits FrmMainTranScreen
    Dim isnewentry As Boolean
    Dim tb_name As String = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim isFlag As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHrTraineeFeedBack)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        Btnsave.Visible = MyBase.isModifyFlag
        BtnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Function allowtosave()


        If clsCommon.myLen(clsCommon.myCstr(fndSchedule.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(fndSchedule.Value)) > 30 Then
            myMessages.blankValue("Schedule Code ")

            fndSchedule.Focus()
            fndSchedule.Select()
            Errorcontrol.SetError(fndSchedule, "Schedule Code ")
            Return False
        Else
            Errorcontrol.ResetError(fndSchedule)
        End If


        If clsCommon.myLen(clsCommon.myCstr(txtFeedback.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtFeedback.Text)) > 300 Then
            myMessages.blankValue("Feedback ")

            txtFeedback.Focus()
            txtFeedback.Select()
            Errorcontrol.SetError(txtFeedback, "Feedback ")
            Return False
        Else
            Errorcontrol.ResetError(txtFeedback)
        End If



        Return True
    End Function
    Sub savedata()
        Try
            If (allowtosave()) Then

                'Dim obj As clsTraineeFeedback

                Dim entry As String
                Dim count As Integer = 0
                Dim i As Integer = 0
                Dim qry As String = "select count(*) from TSPL_HR_TRAINEE_FEEDBACK  where Doc_Code ='" + txtCode.Value + "'"
                count = clsDBFuncationality.getSingleValue(qry)
                If count = 0 Then
                    isnewentry = True
                Else
                    isnewentry = False

                End If
                Dim obj As New clsTraineeFeedback
                obj.DocCode = txtCode.Value
                obj.DocDate = txtDate.Text
                obj.ScheduleCode = fndSchedule.Value
                obj.TrainerCode = txtTrainerCode.Text
                obj.FeedBack = txtFeedback.Text

               
                If clsTraineeFeedback.SaveData(obj, isnewentry) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                        entry = obj.DocCode
                        LoadData(obj.DocCode, NavigatorType.Current)
                    Else
                        clsCommon.MyMessageBoxShow("Data posted successfully")
                    End If
                End If

            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Dim obj As clsTraineeFeedback = clsTraineeFeedback.GetData(strCode, Nothing, NavTyep)
        Dim ScheduleDate, TrainerName As String

        If obj IsNot Nothing Then
            'AddNew()
            isnewentry = False
            txtCode.Value = obj.DocCode
            txtDate.Text = obj.DocDate
            fndSchedule.Value = obj.ScheduleCode
            ScheduleDate = clsDBFuncationality.getSingleValue("select Doc_Date  from TSPL_Schedule_Training_HEAD where Doc_Code ='" + fndSchedule.Value + "'")
            TrainerName = clsDBFuncationality.getSingleValue("select Tspl_Trainer_Master.DOC_Name  from TSPL_HR_TRAINEE_FEEDBACK left outer join Tspl_Trainer_Master on TSPL_HR_TRAINEE_FEEDBACK.Trainer_Code =Tspl_Trainer_Master.DOC_Code  where TSPL_HR_TRAINEE_FEEDBACK.Doc_Code ='" + obj.DocCode + "'")
            txtScheduleDate.Text = ScheduleDate
            txtTrainerCode.Text = obj.TrainerCode
            txtTraineeName.Text = TrainerName
            txtFeedback.Text = obj.FeedBack


            txtCode.MyReadOnly = True
            Btnsave.Text = "Update"
            BtnDelete.Enabled = True
            'btnSave.Text = "Update"
            btnPost.Enabled = True
            'btndelete.Enabled = True
            If obj.Posted = ERPTransactionStatus.Approved Then
                Btnsave.Enabled = False
                btnPost.Enabled = False
                BtnDelete.Enabled = False
            End If
            UsLock1.Status = obj.Posted
        End If
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE(Nothing)
        fndSchedule.Value = ""
        txtScheduleDate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtTrainerCode.Text = ""
        txtTraineeName.Text = ""
        txtFeedback.Text = ""
        Btnsave.Text = "Save"
        txtCode.MyReadOnly = False
        UsLock1.Status = ERPTransactionStatus.Pending
        Btnsave.Enabled = True
        btnPost.Enabled = False
        BtnDelete.Enabled = False
    End Sub
    Private Sub FrmTraineeFeedback_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(Btnsave, "Press Alt+S for Save/Update ")

        ButtonToolTip.SetToolTip(BtnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(BtnDelete, "Press Alt+D Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post")
        AddNew()

        txtDate.Text = clsCommon.GETSERVERDATE()
        txtScheduleDate.Text = clsCommon.GETSERVERDATE()
        btnPost.Enabled = False
        BtnDelete.Enabled = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub Btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnsave.Click
        savedata()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsTraineeFeedback.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete")
            Exit Sub
        End If

        funDelete()
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        DeleteData()
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Doc_Code As String = ""
            isFlag = True
            If clsCommon.myLen(txtCode.Value) > 0 Then
                Doc_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS Doc_Code from TSPL_HR_TRAINEE_FEEDBACK  where Doc_Code='" + txtCode.Value + "'"))
                If Doc_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        savedata()
                        If (clsTraineeFeedback.PostData(MyBase.Form_ID, txtCode.Value)) Then
                            'msg = "Successfully Posted"
                            'common.clsCommon.MyMessageBoxShow(msg)
                            LoadData(txtCode.Value, NavigatorType.Current)
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
            common.clsCommon.MyMessageBoxShow(ex.Message)
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
            clsCommon.MyMessageBoxShow("code not found to post")
        End If
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub


    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Try
            Dim qry As String = "select Doc_Code ,Doc_Date ,Trainer_Code ,Schedule_Code ,Feedback   from TSPL_HR_TRAINEE_FEEDBACK"
            txtCode.Value = clsCommon.ShowSelectForm("Trainee_Feedback", qry, "Doc_Code", "", txtCode.Value, "", isButtonClicked)

            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub



    Private Sub fndSchedule__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSchedule._MYValidating
        Dim qry As String

        qry = " select TSPL_Schedule_Training_HEAD.DOC_Code   from TSPL_Schedule_Training_HEAD "
        fndSchedule.Value = clsCommon.ShowSelectForm("TRAINEE_FEEDBACK", qry, "Doc_Code", "isPosted =1", fndSchedule.Value, "", isButtonClicked)

        If clsCommon.myLen(fndSchedule.Value) > 0 Then
            txtScheduleDate.Value = clsDBFuncationality.getSingleValue("select Doc_Date  from TSPL_Schedule_Training_HEAD where Doc_Code ='" + fndSchedule.Value + "' ")
        Else
            txtScheduleDate.Value = clsCommon.GETSERVERDATE()
        End If
        If clsCommon.myLen(fndSchedule.Value) > 0 Then
            txtTraineeName.Text = clsDBFuncationality.getSingleValue("select Tspl_Trainer_Master.DOC_Name   from TSPL_Schedule_Training_HEAD left outer join Tspl_Trainer_Master on TSPL_Schedule_Training_HEAD.Trainer_Code =Tspl_Trainer_Master.DOC_Code  where TSPL_Schedule_Training_HEAD.Doc_Code ='" + fndSchedule.Value + "' ")
        Else
            txtTraineeName.Text = ""
        End If
        If clsCommon.myLen(fndSchedule.Value) > 0 Then
            txtTrainerCode.Text = clsDBFuncationality.getSingleValue("select TSPL_Schedule_Training_HEAD.Trainer_Code  from TSPL_Schedule_Training_HEAD left outer join Tspl_Trainer_Master on TSPL_Schedule_Training_HEAD.Trainer_Code =Tspl_Trainer_Master.DOC_Code  where TSPL_Schedule_Training_HEAD.Doc_Code ='" + fndSchedule.Value + "' ")
        Else
            txtTrainerCode.Text = ""
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub FrmTraineeFeedback_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso Btnsave.Enabled Then
            savedata()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso BtnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
End Class
