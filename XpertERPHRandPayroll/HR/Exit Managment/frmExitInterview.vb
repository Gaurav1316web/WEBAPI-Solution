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
Imports XpertERPEngine
'===================Created by Preeti Gupta================
Public Class FrmExitInterview

    Const collineno As String = "Line No"
    Const colexitCode As String = "Exit Code"
    Const colQuesCode As String = "Question Code"
    Const colQuesName As String = "Question Name"
    Const colUserCode As String = "User Code"
    Const colStronglyAgree As String = "Strongly Agree"
    Const colSomeWhatAgree As String = "Some What Agree"
    Const colSomeWhatDisAgree As String = "Some What Dis Agree"
    Const colStronglyDisAgree As String = "Strongly Dis Agree"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim entry As String
    Private isInsideLoadData As Boolean = False
    Private isFromLoad As Boolean = False
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim isNewEntry As Boolean = True
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Private Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.frmHREMExitInterview)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag



        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankItemGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoILineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoILineNo.FormatString = ""
        repoILineNo.HeaderText = "Line No"
        repoILineNo.Name = collineno
        repoILineNo.Width = 50
        repoILineNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoILineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Exit Code"
        repoICode.Name = colexitCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        repoICode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoQuesCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQuesCode.FormatString = ""
        repoQuesCode.HeaderText = "Ques Code"
        repoQuesCode.Name = colQuesCode
        repoQuesCode.Width = 200
        repoQuesCode.ReadOnly = True
        repoQuesCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoQuesCode)

        Dim repoQuesName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQuesName.FormatString = ""
        repoQuesName.HeaderText = "Question"
        repoQuesName.Name = colQuesName
        repoQuesName.Width = 200
        'repoQuesName.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoQuesName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoQuesName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoQuesName)

        Dim repoStronglyCode As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoStronglyCode.FormatString = ""
        repoStronglyCode.HeaderText = "Strongly agree"
        repoStronglyCode.Name = colStronglyAgree
        repoStronglyCode.Width = 100
        repoStronglyCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoStronglyCode)

        Dim repoSomeWhatAgree As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSomeWhatAgree.FormatString = ""
        repoSomeWhatAgree.HeaderText = "Some what agree"
        repoSomeWhatAgree.Name = colSomeWhatAgree
        repoSomeWhatAgree.Width = 100
        repoSomeWhatAgree.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoSomeWhatAgree)

        Dim repoSomeWhatDisAgree As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSomeWhatDisAgree.FormatString = ""
        repoSomeWhatDisAgree.HeaderText = "Some what dis agree"
        repoSomeWhatDisAgree.Name = colSomeWhatDisAgree
        repoSomeWhatDisAgree.Width = 100
        repoSomeWhatDisAgree.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoSomeWhatDisAgree)

        Dim repoStronglyDisAgree As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoStronglyDisAgree.FormatString = ""
        repoStronglyDisAgree.HeaderText = "Strongly dis agree"
        repoStronglyDisAgree.Name = colStronglyDisAgree
        repoStronglyDisAgree.Width = 100
        repoStronglyDisAgree.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoStronglyDisAgree)


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
    Sub reset(ByVal isreset As Boolean)
        txtcode.Value = ""
        txtDate.Text = clsCommon.GETSERVERDATE()
        txtDetailReson.Text = ""
        txtSuggestion.Text = ""
        txtSupervisorCode.Value = ""
        txtSupervisorName.Text = ""
        LoadFriendRecommend()
        LoadReturnToWorkHere()
        LoadResonOfLeaving()
        loaddata()
        Load_Report(isreset)
        btnSave.Text = "Save"

    End Sub

    Sub LoadResonOfLeaving()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Personal Reson")
        dt.Rows.Add("Medical Benefits")
        dt.Rows.Add("Quality of supervision")
        dt.Rows.Add("Work environment")

        ddlResonOfLeavingType.DataSource = dt
        ddlResonOfLeavingType.ValueMember = "Code"
        ddlResonOfLeavingType.DisplayMember = "Code"
    End Sub

    Sub LoadReturnToWorkHere()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Yes")
        dt.Rows.Add("No")

        ddlReturnToWorkHere.DataSource = dt
        ddlReturnToWorkHere.ValueMember = "Code"
        ddlReturnToWorkHere.DisplayMember = "Code"
    End Sub
    Sub LoadFriendRecommend()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Yes")
        dt.Rows.Add("No")

        ddlRecommend.DataSource = dt
        ddlRecommend.ValueMember = "Code"
        ddlRecommend.DisplayMember = "Code"
    End Sub

    Function allowtosave()


        If clsCommon.myLen(clsCommon.myCstr(txtSupervisorCode.Value)) <= 0 Then
            myMessages.blankValue("SuperVisor ")
            txtSupervisorCode.Focus()
            txtSupervisorCode.Select()
            Errorcontrol.SetError(txtSupervisorCode, "SuperVisor ")
            Return False
        Else
            Errorcontrol.ResetError(txtSupervisorCode)
        End If

        Return True
    End Function
    Sub loaddata()
        Dim qry As String = "select tspl_user_master.User_Code ,tspl_user_master.User_Name ,TSPL_DESIGNATION_MASTER.Designation_id ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME  from TSPL_EMPLOYEE_MASTER"
        qry += " left outer join tspl_user_master on TSPL_EMPLOYEE_MASTER .USER_CODE  =tspl_user_master.User_Code  "
        qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =tspl_employee_master.Designation "
        qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =tspl_employee_master.DEPARTMENT_CODE where tspl_user_master.User_Code ='" + objCommonVar.CurrentUserCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        txtNameCode.Text = dt.Rows(0).Item("User_Code")
        txtName.Text = dt.Rows(0).Item("User_Name")
        txtDepartment.Text = dt.Rows(0).Item("DEPARTMENT_CODE")
        txtDepartmentName.Text = dt.Rows(0).Item("DEPARTMENT_NAME")
        txtPosition.Text = dt.Rows(0).Item("Designation_id")
        txtPositionName.Text = dt.Rows(0).Item("Designation_Desc")


    End Sub
    Private Sub FrmExitInterview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Transaction")
        reset(True)
        If clsCommon.myLen(Me.Tag) > 0 Then
            loaddata(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Sub savedata()
        Try
            If (allowtosave()) Then

                btnSave.Focus()
                Dim obj As clsHrEmexitInterviewDetail
                Dim arr As New List(Of clsHrEmexitInterviewDetail)
                Dim entry As String
                Dim count As Integer = 0
                'Dim i As Integer = 0
                Dim qry As String = "select count(*) from Tspl_HR_EM_Exit_Interview_Head  where exit_code ='" + txtcode.Value + "'"
                count = clsDBFuncationality.getSingleValue(qry)
                If count = 0 Then
                    isnewentry = True
                Else
                    isnewentry = False

                End If

                Dim exitInterview As New clsHREMExitInterview
                exitInterview.exit_code = clsCommon.myCstr(txtcode.Value)
                exitInterview.Exit_Date = txtDate.Text
                exitInterview.user_code = clsCommon.myCstr(txtNameCode.Text)
                exitInterview.DEPARTMENT_CODE = clsCommon.myCstr(txtDepartment.Text)
                exitInterview.DESIGNATION_ID = txtPosition.Text
                exitInterview.Supervisor_code = txtSupervisorCode.Value
                exitInterview.Reson_Of_Leaving = ddlResonOfLeavingType.Text
                exitInterview.Detail_Reson = txtDetailReson.Text
                exitInterview.Suggestion = clsCommon.myCstr(txtSuggestion.Text)
                exitInterview.Return_To_Work_Here = ddlReturnToWorkHere.Text
                exitInterview.Frnd_Recommend = ddlRecommend.Text


                For i As Integer = 0 To gv1.Rows.Count - 1
                    'If clsCommon.myLen(gv1.Rows(i).Cells(colSelect).Value) > 0 Then
                    ' Dim Obj As New clsSalesHierarchyMapping
                    obj = New clsHrEmexitInterviewDetail
                    obj.line_no = clsCommon.myCstr(gv1.Rows(i).Cells(collineno).Value)
                    obj.Exit_Code = clsCommon.myCstr(gv1.Rows(i).Cells(colexitCode).Value)
                    obj.Ques_code = clsCommon.myCstr(gv1.Rows(i).Cells(colQuesCode).Value)
                    obj.Ques_Name = clsCommon.myCstr(gv1.Rows(i).Cells(colQuesName).Value)
                    obj.Strongly_Agree = clsCommon.myCdbl(gv1.Rows(i).Cells(colStronglyAgree).Value)
                    obj.Strongly_Disagree = clsCommon.myCdbl(gv1.Rows(i).Cells(colStronglyDisAgree).Value)
                    obj.SomeWhat_agree = clsCommon.myCdbl(gv1.Rows(i).Cells(colSomeWhatAgree).Value)
                    obj.SomeWhat_disgree = clsCommon.myCdbl(gv1.Rows(i).Cells(colSomeWhatDisAgree).Value)
                    arr.Add(obj)
                    'End If
                Next




                If clsHREMExitInterview.savedata(exitInterview, isNewEntry, arr) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    entry = exitInterview.exit_code
                    loaddata(exitInterview.exit_code, NavigatorType.Current)
                    btnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    btnDelete.Enabled = False

                End If

            End If


        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Sub LoadData(ByVal strExit As String, ByVal NavType As NavigatorType)
        Try
            reset(False)
            Dim obj As New clsHREMExitInterview
            obj = clsHREMExitInterview.getdata(strExit, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.exit_code) > 0 Then
                isInsideLoadData = True
                txtcode.Value = obj.exit_code
                txtDate.Value = obj.Exit_Date
                txtNameCode.Text = obj.user_code
                txtName.Text = obj.User_Name
                txtDepartment.Text = obj.DEPARTMENT_CODE
                txtDepartmentName.Text = obj.Department_Name
                txtPosition.Text = obj.DESIGNATION_ID
                txtPositionName.Text = obj.Designation_Name
                txtSupervisorCode.Value = obj.Supervisor_code
                txtSupervisorName.Text = obj.SuperVisor_Name
                ddlResonOfLeavingType.SelectedValue = obj.Reson_Of_Leaving
                txtDetailReson.Text = obj.Detail_Reson
                txtSuggestion.Text = obj.Suggestion
                ddlReturnToWorkHere.SelectedValue = obj.Return_To_Work_Here
                ddlRecommend.SelectedValue = obj.Frnd_Recommend
              
                Dim LineNo As Integer = 0
                For Each objDTL As clsHrEmexitInterviewDetail In obj.arr
                    LineNo += 1
                    gv1.Rows.AddNew()
                    gv1.CurrentRow.Cells(collineno).Value = objDTL.line_no
                    'gv1.CurrentRow.Cells(colexitCode).Value = objDTL.Exit_Code
                    gv1.CurrentRow.Cells(colQuesCode).Value = objDTL.Ques_code
                    gv1.CurrentRow.Cells(colQuesName).Value = objDTL.Ques_Name
                    gv1.CurrentRow.Cells(colStronglyAgree).Value = objDTL.Strongly_Disagree
                    gv1.CurrentRow.Cells(colStronglyDisAgree).Value = objDTL.Strongly_Disagree
                    gv1.CurrentRow.Cells(colSomeWhatAgree).Value = objDTL.SomeWhat_agree
                    gv1.CurrentRow.Cells(colSomeWhatDisAgree).Value = objDTL.SomeWhat_disgree

                Next


                isNewEntry = False
                btnSave.Text = "Update"
                isInsideLoadData = False
                txtcode.MyReadOnly = True
            Else
                isNewEntry = True
                btnSave.Text = "Save"
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Load_Report(ByVal IsReset As Boolean)
        LoadBlankItemGrid()
        If IsReset Then
            Dim squery As String = " select  ROW_NUMBER() over (ORDER BY Ques_Code) AS Number,Tspl_HR_EM_Exit_Question.Ques_Code ,Tspl_HR_EM_Exit_Question.Description  from Tspl_HR_EM_Exit_Question"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(squery)
            For Each dr As DataRow In dtgv.Rows

                gv1.Rows.AddNew()
                isInsideLoadData = True
                gv1.Rows(gv1.Rows.Count - 1).Cells(collineno).Value = clsCommon.myCstr(dr("Number"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQuesCode).Value = clsCommon.myCstr(dr("Ques_Code"))
                gv1.Rows(gv1.Rows.Count - 1).Cells(colQuesName).Value = clsCommon.myCstr(dr("Description"))

                isInsideLoadData = False
            Next
        End If

    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsHREMExitInterview.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    reset(True)
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

    Private Sub FrmExitInterview_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            reset(True)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        savedata()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click

        reset(True)
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            loaddata(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from Tspl_HR_EM_Exit_Interview_Head where Exit_code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""

            qry = "select Tspl_HR_EM_Exit_Interview_Head.exit_code as [Code],Tspl_HR_EM_Exit_Interview_Head.Exit_Date as [Date] ,Tspl_HR_EM_Exit_Interview_Head.user_code as [User Code],TSPL_USER_MASTER.User_Name as [User Name]  ,Tspl_HR_EM_Exit_Interview_Head.Supervisor_code as [Supervisor Code],Supervisor.User_Name as [Supervisor Name] ,Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE as [Department Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name] ,Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID as [Position Code],TSPL_DESIGNATION_MASTER.Designation_Desc as [Position Name] ,Tspl_HR_EM_Exit_Interview_Head.Reson_Of_Leaving as [Reson of leaving],Tspl_HR_EM_Exit_Interview_Head.Detail_Reson as [Detail Reson],Tspl_HR_EM_Exit_Interview_Head.Suggestion ,Tspl_HR_EM_Exit_Interview_Head.Return_To_Work_Here as [Return to Work Here] ,Tspl_HR_EM_Exit_Interview_Head.Frnd_Recommend as [Friend Recommend]  from Tspl_HR_EM_Exit_Interview_Head left outer join TSPL_USER_MASTER on tspl_user_master.User_Code =Tspl_HR_EM_Exit_Interview_Head.user_code left outer join tspl_user_master as Supervisor on Supervisor.User_Code =Tspl_HR_EM_Exit_Interview_Head.Supervisor_code  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE       left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID"
            str = clsCommon.ShowSelectForm("ExitInterview", qry, "Code", "", txtcode.Value, "Code", isButtonClicked)
            txtcode.Value = str
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As clsHREMExitInterview
                objOT = clsHREMExitInterview.getdata(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    loaddata(txtcode.Value, NavigatorType.Current)
                End If
            Else
                reset(True)
            End If
        End If

    End Sub


    Private Sub txtSupervisorCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtSupervisorCode._MYValidating
        txtSupervisorCode.Value = clsUserMaster.getFinder("", txtSupervisorCode.Value, isButtonClicked)

        If clsCommon.myLen(txtSupervisorCode.Value) > 0 Then
            txtSupervisorName.Text = clsDBFuncationality.getSingleValue("select User_Name  from tspl_user_master where user_code ='" + txtSupervisorCode.Value + "' ")
        Else
            txtSupervisorName.Text = ""

        End If
    End Sub

    'Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
    '    If gv1.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gv1.CurrentRow.Index
    '        gv1.CurrentRow.Cells(collineno).Value = clsCommon.myCstr(clsCommon.myCdbl(intCurrRow + 1))
    '        If intCurrRow = gv1.Rows.Count - 1 Then
    '            gv1.Rows.AddNew()
    '            gv1.CurrentRow = gv1.Rows(intCurrRow)
    '        End If
    '    End If
    'End Sub
    Public Function funPrint(ByVal strDocNo As String) As DataTable

        Try
            Dim Qry As String = " select ROW_NUMBER() over (ORDER BY Tspl_HR_EM_Exit_Interview_Head.exit_code)+5 AS Number, TSPL_COMPANY_MASTER.Logo_Img ,TSPL_COMPANY_MASTER.Logo_Img2 ,TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name ,"
            Qry += " TSPL_COMPANY_MASTER.Add1 as Comp_Add1,TSPL_COMPANY_MASTER.Add2 as comp_add2,TSPL_COMPANY_MASTER.Add3 as comp_add3, Tspl_HR_EM_Exit_Question.Description as [Ques Name],"
            Qry += "   Tspl_HR_EM_Exit_Interview_Head.exit_code as [Exit Code],Tspl_HR_EM_Exit_Interview_Head.Exit_Date as [Exit Date] ,Tspl_HR_EM_Exit_Interview_Head.user_code as [User Code],"
            Qry += " TSPL_USER_MASTER.User_Name as [User Name]  ,Tspl_HR_EM_Exit_Interview_Head.Supervisor_code as [Supervisor Code],Supervisor.User_Name as [Supervisor Name] ,"
            Qry += " Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE as [Department Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name] ,"
            Qry += " Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID as [Position Code],TSPL_DESIGNATION_MASTER.Designation_Desc as [Position Name] ,"
            Qry += " Tspl_HR_EM_Exit_Interview_Head.Reson_Of_Leaving as [Reson of leaving],Tspl_HR_EM_Exit_Interview_Head.Detail_Reson as [Detail Reson],"
            Qry += " Tspl_HR_EM_Exit_Interview_Head.Suggestion ,Tspl_HR_EM_Exit_Interview_Head.Return_To_Work_Here as [Return to Work Here] ,"
            Qry += " Tspl_HR_EM_Exit_Interview_Head.Frnd_Recommend as [Friend Recommend],Tspl_HR_EM_Exit_Interview_detail.line_no,Tspl_HR_EM_Exit_Interview_detail.Ques_code,"
            Qry += " Tspl_HR_EM_Exit_Interview_detail.Strongly_Agree, Tspl_HR_EM_Exit_Interview_detail.SomeWhat_agree, Tspl_HR_EM_Exit_Interview_detail.SomeWhat_disgree, Strongly_Disagree"
            Qry += " from Tspl_HR_EM_Exit_Interview_Head left outer join TSPL_USER_MASTER on tspl_user_master.User_Code =Tspl_HR_EM_Exit_Interview_Head.user_code "
            Qry += " left outer join tspl_user_master as Supervisor on Supervisor.User_Code =Tspl_HR_EM_Exit_Interview_Head.Supervisor_code "
            Qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Exit_Interview_Head.DEPARTMENT_CODE "
            Qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER .Designation_id =Tspl_HR_EM_Exit_Interview_Head.DESIGNATION_ID "
            Qry += " left outer join Tspl_HR_EM_Exit_Interview_detail on Tspl_HR_EM_Exit_Interview_detail.exit_code=Tspl_HR_EM_Exit_Interview_Head.Exit_code "
            Qry += " left outer join Tspl_HR_EM_Exit_Question on Tspl_HR_EM_Exit_Question.Ques_Code =Tspl_HR_EM_Exit_Interview_detail.Ques_Code "
            Qry += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =Tspl_HR_EM_Exit_Interview_Head.Comp_Code "
            Qry += " where 2=2 and  Tspl_HR_EM_Exit_Interview_Head.exit_code = '" + strDocNo + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Return dt
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return Nothing
    End Function
    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No data found to print.", Me.Text)
        Else
            Dim dt As DataTable = funPrint(txtcode.Value)

            If dt.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HumanResource, dt, "rptHREMExitInterview", "Exit Interview")

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found.", Me.Text)
            End If
        End If
    End Sub
End Class
