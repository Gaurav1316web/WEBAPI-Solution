'-------------Created By Monika 02/07/2014--------------
Imports common
Imports System.Data.SqlClient

Public Class FrmNotificationScreen
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colSno As String = "Sno"
    Const colScreenCode As String = "ScreenCode"
    Const colScreenName As String = "ScreenName"
    Const colSelect As String = "Select"
    Const colMsg As String = "Message"
    Const colStartDate As String = "StartDate"
    Const colNoOfUser As String = "NoofUsers"
    Const colCriteria As String = "Criteria"

    '---------------------------
    Const colUserSno As String = "UserSno"
    Const colUserCode As String = "UserCode"
    Const colUsername As String = "UserName"

    '-------------------------------
    Const colcheckd As String = ""
    Const colcriteriavalues As String = ""

    Dim isLoadedData As Boolean = False
    Dim isValueChanged As Boolean = True
    Dim Doctype As String = ""
    'Dim counter As Integer = 0
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
#End Region

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Try
            Dim qry As String = "drop table Temp_User_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try
        Try
            Dim qry As String = "drop table Temp_Criteria_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try
        Me.Close()
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.frmNotificationScreen)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnsave_user.Visible = MyBase.isModifyFlag
        btnsave_criteria.Visible = MyBase.isModifyFlag
    End Sub

    Sub Reset()
        txtModule.Value = ""
        lblModule.Text = ""
        chkMaster.IsChecked = False
        chkTransaction.IsChecked = False
        'gv.DataSource = Nothing
        'gv.Columns.Clear()
        gv.Rows.Clear()
        gv.Rows.AddNew()
        gv_user.Rows.Clear()
        gv_user.Rows.AddNew()
        LoadCriteria()
        'dg_criteria.Rows.Clear()
        'dg_criteria.Columns.Clear()
        RadGroupBox1.Visible = False
        Groupbox_criteria.Visible = False

        btnsave.Text = "&Save"
        btndelete.Enabled = False

        txtModule.Focus()
        txtModule.Select()

        Dim qry As String = "delete from Temp_User_Notification"
        clsDBFuncationality.ExecuteNonQuery(qry)
        qry = "delete from Temp_Criteria_Notification"
        clsDBFuncationality.ExecuteNonQuery(qry)
    End Sub

    Private Sub FrmNotificationScreen_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

    End Sub



    Private Sub FrmNotificationScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        LoadBlankUserGrid()
        LoadCriteria()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U to Save/Update")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Deleting Record")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for Window Closed")
        ButtonToolTip.SetToolTip(btnsave_user, "Press Alt+a to Save")

        Try
            Dim qry As String = "create table Temp_User_Notification(Module_Code varchar(12),Screen_Type varchar(12),Sno int,Screen_Code varchar(12),User_code varchar(12),Created_By varchar(12) NOT NULL,Created_Date varchar(10) NOT NULL,Modified_By varchar(12) NOT NULL,Modified_Date varchar(10) NOT NULL)"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Dim qry As String = "delete from Temp_User_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry)
        End Try

        Try
            Dim qry As String = "create table Temp_Criteria_Notification(Module_Code varchar(12),Screen_Type varchar(12),Screen_Code varchar(12),Criteria varchar(12),Created_By varchar(12) NOT NULL,Created_Date varchar(10) NOT NULL,Modified_By varchar(12) NOT NULL,Modified_Date varchar(10) NOT NULL)"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Dim qry As String = "delete from Temp_Criteria_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry)
        End Try

        Reset()
    End Sub

    Sub LoadCriteria()
        dg_criteria.DataSource = Nothing
        Dim qry As String = "select Criteria from (select 'SAVE' as Criteria union all select 'POST' as Criteria union all select 'BOTH' as Criteria)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt.Columns.Add("Status", GetType(Boolean))

        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            dg_criteria.DataSource = dt
            dg_criteria.AllowEditRow = True
            dg_criteria.AllowDeleteRow = False
            dg_criteria.AllowAddNewRow = False
            dg_criteria.ShowGroupPanel = False
            dg_criteria.AllowColumnReorder = False
            dg_criteria.AllowRowReorder = False
            dg_criteria.EnableSorting = False
            dg_criteria.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            dg_criteria.MasterTemplate.ShowRowHeaderColumn = False

        End If

    End Sub

    Sub LoadBlankUserGrid()
        gv_user.Rows.Clear()
        gv_user.Columns.Clear()

        Dim reposno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposno.Width = 40
        reposno.FormatString = ""
        reposno.ReadOnly = True
        reposno.Name = colUserSno
        reposno.HeaderText = "S.No."
        gv_user.MasterTemplate.Columns.Add(reposno)

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Width = 90
        repocode.FormatString = ""
        repocode.Name = colUserCode
        repocode.HeaderText = "User Code"
        repocode.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv_user.MasterTemplate.Columns.Add(repocode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.Width = 170
        reponame.FormatString = ""
        reponame.ReadOnly = True
        reponame.Name = colUsername
        reponame.HeaderText = "User Name"
        gv_user.MasterTemplate.Columns.Add(reponame)

        gv_user.AllowEditRow = True
        gv_user.AllowDeleteRow = True
        gv_user.AllowAddNewRow = False
        gv_user.ShowGroupPanel = False
        gv_user.AllowColumnReorder = False
        gv_user.AllowRowReorder = False
        gv_user.EnableSorting = False
        gv_user.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_user.MasterTemplate.ShowRowHeaderColumn = False
        gv_user.Rows.AddNew()
    End Sub

    Sub LoadBlankGrid()
        'gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()

        Dim reposno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposno.Width = 50
        reposno.FormatString = ""
        reposno.ReadOnly = True
        reposno.Name = colSno
        reposno.HeaderText = "S.No."
        gv.MasterTemplate.Columns.Add(reposno)

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Width = 90
        repocode.FormatString = ""
        repocode.ReadOnly = True
        repocode.Name = colScreenCode
        repocode.HeaderText = "Screen Code"
        gv.MasterTemplate.Columns.Add(repocode)

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.Width = 130
        reponame.FormatString = ""
        reponame.ReadOnly = True
        reponame.Name = colScreenName
        reponame.HeaderText = "Description"
        gv.MasterTemplate.Columns.Add(reponame)

        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.Width = 50
        repostatus.FormatString = ""
        repostatus.Name = colSelect
        repostatus.HeaderText = "Status"
        repostatus.DataSource = statuscombo()
        repostatus.DisplayMember = "Code"
        repostatus.ValueMember = "Code"
        gv.MasterTemplate.Columns.Add(repostatus)

        Dim repocriteria As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocriteria.Width = 80
        repocriteria.FormatString = ""
        repocriteria.Name = colCriteria
        repocriteria.HeaderText = "Criteria"
        repocriteria.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repocriteria)

        Dim repodate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repodate.Width = 80
        repodate.FormatString = ""
        repodate.Name = colStartDate
        repodate.HeaderText = "Start Date"
        gv.MasterTemplate.Columns.Add(repodate)

        Dim repomsg As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repomsg.Width = 350
        repomsg.FormatString = ""
        repomsg.Name = colMsg
        repomsg.HeaderText = "Notification Message"
        repomsg.MaxLength = 350
        gv.MasterTemplate.Columns.Add(repomsg)

        Dim repousers As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repousers.Width = 80
        repousers.FormatString = ""
        repousers.ReadOnly = True
        repousers.Name = colNoOfUser
        repousers.HeaderText = "No of Users"
        gv.MasterTemplate.Columns.Add(repousers)

        gv.AllowEditRow = True
        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.Rows.AddNew()
    End Sub

    Function statuscombo() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "NO"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "YES"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Function criteriacombo() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))

        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "SAVE"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "POST"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "UNPOST"
        dt.Rows.Add(dr)

        Return dt
    End Function

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtModule.Value) <= 0 Then
                txtModule.Focus()
                txtModule.Select()
                Errorcontrol.SetError(lblModule, "Please Select Module First")
                Throw New Exception("Please Select Module First")
            Else
                Errorcontrol.ResetError(lblModule)
            End If

            If Not chkMaster.IsChecked AndAlso Not chkTransaction.IsChecked Then
                Panel1.Focus()
                Panel1.Select()
                Errorcontrol.SetError(Panel1, "Please Select Module Type(Master/Transaction)")
                Throw New Exception("Please Select Module Type(Master/Transaction)")
            Else
                Errorcontrol.ResetError(Panel1)
            End If

            Dim status As String = ""
            Dim criteria As String = ""
            Dim xdate As Date = Nothing
            Dim msg As String = ""
            Dim users As Integer = 0
            Dim code As String = ""
            Dim xrow As Integer = 0
            Dim crntdate As Date = clsCommon.myCDate(clsCommon.GETSERVERDATE().ToString("dd/MM/yyyy"))

            If clsCommon.myLen(gv.Rows(0).Cells(colScreenCode).Value) > 0 Then
                For Each grow As GridViewRowInfo In gv.Rows()
                    status = ""
                    criteria = ""
                    xdate = Nothing
                    msg = ""
                    users = 0
                    code = ""

                    code = clsCommon.myCstr(grow.Cells(colScreenCode).Value)
                    status = clsCommon.myCstr(grow.Cells(colSelect).Value)
                    xdate = clsCommon.myCDate(grow.Cells(colStartDate).Value)
                    msg = clsCommon.myCstr(grow.Cells(colMsg).Value)
                    If clsCommon.myCstr(grow.Cells(colNoOfUser).Value) = "Double Click" Then
                        grow.Cells(colNoOfUser).Value = "0"
                    End If
                    users = CInt(grow.Cells(colNoOfUser).Value)
                    If clsCommon.myCstr(grow.Cells(colNoOfUser).Value) = "0" Then
                        grow.Cells(colNoOfUser).Value = "Double Click"
                    End If

                    If clsCommon.myCstr(grow.Cells(colCriteria).Value) = "Double Click" Then
                        grow.Cells(colCriteria).Value = "0"
                    End If
                    criteria = clsCommon.myCstr(grow.Cells(colCriteria).Value)
                    If clsCommon.myCstr(grow.Cells(colCriteria).Value) = "0" Then
                        grow.Cells(colCriteria).Value = "Double Click"
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.myLen(status) <= 0 Then
                        Throw New Exception("Please Select Screen Status (Yes/No) At Line No. " + clsCommon.myCstr(CInt(xrow) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(status, "YES") = CompairStringResult.Equal AndAlso CInt(criteria) <= 0 Then
                        Throw New Exception("Please Select Screen Criteria (Save/Post/Both) At Line No. " + clsCommon.myCstr(CInt(xrow) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(status, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(xdate) <= 0 Then
                        Throw New Exception("Please Fill Screen Start Date And Should Be" + Environment.NewLine + "Greater Than Or Equal To Current Server Date At Line No. " + clsCommon.myCstr(CInt(xrow) + 1) + "")
                        'ElseIf clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(status, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(xdate) > 0 AndAlso (xdate) < crntdate Then
                        '    Throw New Exception("Filled Date Must Be Greater Than Or Equal To Current Server Date At Line No. " + clsCommon.myCstr(CInt(xrow) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(status, "YES") = CompairStringResult.Equal AndAlso clsCommon.myLen(msg) <= 0 Then
                        Throw New Exception("Please Fill Notification Message At Line No. " + clsCommon.myCstr(CInt(xrow) + 1) + "")
                    End If

                    If clsCommon.myLen(code) > 0 AndAlso clsCommon.CompairString(status, "YES") = CompairStringResult.Equal AndAlso CInt(users) <= 0 Then
                        Throw New Exception("Please Select Users To Whom The Notification Is Send At Line No. " + clsCommon.myCstr(CInt(xrow) + 1) + "")
                    End If

                    xrow += 1
                Next
            Else
                Throw New Exception("No Grid Data Found For Saving")
            End If

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Sub SaveData()
        Try
            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmNotificationScreen, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New clsfrmNotificationScreen()
            obj.modulecode = clsCommon.myCstr(txtModule.Value)
            obj.doctype = clsCommon.myCstr(Doctype)

            Dim arr As New List(Of clsfrmNotificationScreen)

            For Each grow As GridViewRowInfo In gv.Rows()
                Dim obtr As New clsfrmNotificationScreen()

                obtr.sno = CInt(grow.Cells(colSno).Value)
                obtr.scrncode = clsCommon.myCstr(grow.Cells(colScreenCode).Value)
                obtr.status = clsCommon.myCstr(grow.Cells(colSelect).Value)
                If clsCommon.myCstr(grow.Cells(colCriteria).Value) = "Double Click" Then
                    obtr.criteria = "0"
                Else
                    obtr.criteria = clsCommon.myCstr(grow.Cells(colCriteria).Value)
                End If

                obtr.startdate = clsCommon.myCDate(grow.Cells(colStartDate).Value)
                obtr.msg = clsCommon.myCstr(grow.Cells(colMsg).Value)
                If clsCommon.myCstr(grow.Cells(colNoOfUser).Value) = "Double Click" Then
                    obtr.noofuser = "0"
                Else
                    obtr.noofuser = CInt(grow.Cells(colNoOfUser).Value)
                End If

                If clsCommon.myLen(obtr.scrncode) > 0 Then
                    arr.Add(obtr)
                End If
            Next

            If clsCommon.myLen(obj.modulecode) > 0 Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

                If clsfrmNotificationScreen.SaveData(obj, arr, trans) Then
                    If clsCommon.CompairString(btnsave.Text, "&Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data Updated Successfully", Me.Text)
                    End If

                    btnsave.Text = "&Update"
                    btndelete.Enabled = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub txtModule__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtModule._MYValidating
        Try
            Dim qry As String = "Select Program_Code as Code, [Program_Name] as Description from TSPL_PROGRAM_MASTER"
            Dim WhrCls As String = " Type='M'"
            If objCommonVar.IsDemoERP Then
                WhrCls += " and Program_Code not in ('MSales')"
            Else
                WhrCls += " and Program_Code not in ('MSalesNew')"
            End If
            gv.Rows.Clear()
            txtModule.Value = clsCommon.ShowSelectForm("NOTFSCRN", qry, "Code", WhrCls, txtModule.Value, "SNo", isButtonClicked)

            If clsCommon.myLen(txtModule.Value) > 0 Then
                lblModule.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Program_Name from TSPL_PROGRAM_MASTER WHERE Program_Code='" + txtModule.Value + "'"))
                chkMaster.IsChecked = False
                chkTransaction.IsChecked = False
            Else
                lblModule.Text = ""
                chkMaster.IsChecked = False
                chkTransaction.IsChecked = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub LoadGridData()
        If clsCommon.myLen(txtModule) > 0 AndAlso clsCommon.myLen(Doctype) > 0 Then
            Dim qry As String = "delete from Temp_User_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry)

            qry = "select TSPL_SCREEN_REMAINDER_SETTING.screen_type,TSPL_SCREEN_REMAINDER_SETTING.sno,TSPL_SCREEN_REMAINDER_SETTING.screen_code,a.Program_Name as screenname,TSPL_SCREEN_REMAINDER_SETTING.status,TSPL_SCREEN_REMAINDER_SETTING.criteria,TSPL_SCREEN_REMAINDER_SETTING.startdate,TSPL_SCREEN_REMAINDER_SETTING.notify_message,TSPL_SCREEN_REMAINDER_SETTING.no_of_users from TSPL_SCREEN_REMAINDER_SETTING left outer join TSPL_PROGRAM_MASTER a on a.Program_Code=TSPL_SCREEN_REMAINDER_SETTING.screen_code  where a.Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(txtModule.Value) + "' and Program_Code like '%" + Doctype + "%') order by TSPL_SCREEN_REMAINDER_SETTING.sno"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv.Rows.Clear()
            gv.Rows.AddNew()

            isLoadedData = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows()
                    gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCstr(gv.Rows.Count)
                    gv.Rows(gv.Rows.Count - 1).Cells(colScreenCode).Value = clsCommon.myCstr(dr("screen_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colScreenName).Value = clsCommon.myCstr(dr("screenname"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = clsCommon.myCstr(dr("status"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colCriteria).Value = clsCommon.myCstr(dr("criteria"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colStartDate).Value = clsCommon.myCstr(dr("startdate"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colMsg).Value = clsCommon.myCstr(dr("notify_message"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colNoOfUser).Value = clsCommon.myCstr(dr("no_of_users"))

                    If clsCommon.myLen(dr("no_of_users")) <= 0 Or clsCommon.myCstr(dr("no_of_users")) = 0 Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colNoOfUser).Value = "Double Click"
                    End If
                    If clsCommon.myLen(dr("criteria")) <= 0 Or clsCommon.myCstr(dr("criteria")) = "0" Then
                        gv.Rows(gv.Rows.Count - 1).Cells(colCriteria).Value = "Double Click"
                    End If

                    gv.Rows.AddNew()
                Next

                '------------insert into user table
                qry = "insert into Temp_User_Notification select * from TSPL_SCREEN_REMAINDER_USERS where module_code='" + txtModule.Value + "' and screen_type='" + Doctype + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)

                '--------insert into criteria table
                qry = "insert into Temp_Criteria_Notification select * from TSPL_SCREEN_REMAINDER_CRITERIA where module_code='" + txtModule.Value + "' and screen_type='" + Doctype + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)

                btnsave.Text = "&Update"
                btndelete.Enabled = True
            Else
                qry = "select ROW_NUMBER() over(order by program_code) as Sno,Program_Code,Program_Name from TSPL_PROGRAM_MASTER where Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + clsCommon.myCstr(txtModule.Value) + "' and Program_Code like '%" + Doctype + "%')"
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows()
                        gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCstr(dr("sno"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colScreenCode).Value = clsCommon.myCstr(dr("Program_Code"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colScreenName).Value = clsCommon.myCstr(dr("Program_Name"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colNoOfUser).Value = "Double Click"
                        gv.Rows(gv.Rows.Count - 1).Cells(colCriteria).Value = "Double Click"
                        gv.Rows.AddNew()
                    Next
                End If
                btnsave.Text = "&Save"
                btndelete.Enabled = False
            End If
        End If

        isLoadedData = False
    End Sub

    Private Sub chkMaster_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMaster.ToggleStateChanged, chkTransaction.ToggleStateChanged
        If chkMaster.IsChecked Then
            Doctype = "Setup"
        ElseIf chkTransaction.IsChecked Then
            Doctype = "Trans"
        End If
        If Not chkMaster.IsChecked AndAlso Not chkTransaction.IsChecked Then
            Doctype = ""
        End If
        LoadGridData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            Dim obj As clsfrmNotificationScreen = New clsfrmNotificationScreen()

            obj.modulecode = clsCommon.myCstr(txtModule.Value)
            obj.doctype = clsCommon.myCstr(Doctype)

            If clsCommon.myLen(obj.modulecode) <= 0 Then
                Throw New Exception("Please Select Module First")
            End If
            If clsCommon.myLen(obj.doctype) <= 0 Then
                Throw New Exception("Please Select Type First(Master/Transaction)")
            End If

            If Not clsCommon.MyMessageBoxShow(Me, "Are You Sure Want To Delete All Notifications of " + txtModule.Value + " Module of " + Doctype + " Type?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Return
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsfrmNotificationScreen.DeleteData(obj.modulecode, obj.doctype, trans) Then
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowUserGridToSave() As Boolean
        Try
            Dim usercode As String = ""
            Dim nextuser As String = ""

            For i As Integer = 0 To gv_user.Rows.Count - 1
                usercode = clsCommon.myCstr(gv_user.Rows(i).Cells(colUserCode).Value)

                If i = 0 AndAlso clsCommon.myLen(usercode) <= 0 Then
                    Throw New Exception("Please Select Atleast One User")
                End If

                For j As Integer = i + 1 To gv_user.Rows.Count - 1
                    nextuser = clsCommon.myCstr(gv_user.Rows(j).Cells(colUserCode).Value)

                    If clsCommon.myLen(usercode) > 0 AndAlso clsCommon.CompairString(usercode, nextuser) = CompairStringResult.Equal Then
                        Throw New Exception("Duplicate User Code Does Not Allowed,Check At Line No. " + clsCommon.myCstr(CInt(j) + 1) + "")
                    End If
                Next
            Next

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnsave_user_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave_user.Click
        Try
            If AllowUserGridToSave() Then
                Dim arr As New List(Of clsfrmNotificationScreen)
                Dim obj As clsfrmNotificationScreen
                Dim scncode As String = ""

                For Each grow As GridViewRowInfo In gv_user.Rows()
                    obj = New clsfrmNotificationScreen()

                    obj.modulecode = clsCommon.myCstr(txtModule.Value)
                    obj.doctype = Doctype
                    obj.user_sno = CInt(grow.Cells(colUserSno).Value)
                    obj.scrncode = clsCommon.myCstr(gv.CurrentRow.Cells(colScreenCode).Value)
                    scncode = obj.scrncode
                    obj.user_code = clsCommon.myCstr(grow.Cells(colUserCode).Value)

                    If clsCommon.myLen(obj.user_code) > 0 Then
                        arr.Add(obj)
                    End If
                Next

                If arr.Count > 0 Then
                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    If clsfrmNotificationScreen.SaveChildGrid(txtModule.Value, Doctype, scncode, arr, trans) Then
                        RadGroupBox1.Visible = False
                        gv.CurrentRow.Cells(colNoOfUser).Value = clsCommon.myCstr(arr.Count)
                    End If
                End If
                RadGroupBox1.Visible = False
            End If
        Catch ex As Exception
            RadGroupBox1.Visible = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.Column Is gv.Columns(colNoOfUser) Then
                RadGroupBox1.Text = "User List of " + clsCommon.myCstr(gv.CurrentRow.Cells(colScreenName).Value)
                RadGroupBox1.Visible = True
                LoadUserGridData()
            ElseIf e.Column Is gv.Columns(colCriteria) Then
                Groupbox_criteria.Text = "Criteria for " + clsCommon.myCstr(gv.CurrentRow.Cells(colScreenName).Value)
                Groupbox_criteria.Visible = True
                LoadCriteriaGridData()
            Else
                RadGroupBox1.Visible = False
                gv_user.Rows.Clear()
                Groupbox_criteria.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadCriteriaGridData()
        Dim qry As String = "select Temp_Criteria_Notification.criteria from Temp_Criteria_Notification where Temp_Criteria_Notification.screen_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colScreenCode).Value) + "' and Temp_Criteria_Notification.screen_type='" + Doctype + "' and Temp_Criteria_Notification.module_code='" + txtModule.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        dg_criteria.Rows(0).Cells(1).Value = False
        dg_criteria.Rows(1).Cells(1).Value = False
        dg_criteria.Rows(2).Cells(1).Value = False
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If clsCommon.CompairString(clsCommon.myCstr(dr("criteria")), "SAVE") = CompairStringResult.Equal Then
                    dg_criteria.Rows(0).Cells(1).Value = True
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("criteria")), "POST") = CompairStringResult.Equal Then
                    dg_criteria.Rows(1).Cells(1).Value = True
                ElseIf clsCommon.CompairString(clsCommon.myCstr(dr("criteria")), "BOTH") = CompairStringResult.Equal Then
                    dg_criteria.Rows(2).Cells(1).Value = True
                End If
            Next
        End If

    End Sub

    Sub LoadUserGridData()
        Dim qry As String = "select Temp_User_Notification.sno,Temp_User_Notification.user_code,tspl_user_master.User_Name from Temp_User_Notification left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=Temp_User_Notification.user_code where Temp_User_Notification.screen_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colScreenCode).Value) + "' and Temp_User_Notification.screen_type='" + Doctype + "' and Temp_User_Notification.module_code='" + txtModule.Value + "' order by Temp_User_Notification.sno"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv_user.Rows.Clear()
        gv_user.Rows.AddNew()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            isLoadedData = True
            For Each dr As DataRow In dt.Rows()
                gv_user.Rows(gv_user.Rows.Count - 1).Cells(colUserSno).Value = clsCommon.myCstr(dr("sno"))
                gv_user.Rows(gv_user.Rows.Count - 1).Cells(colUserCode).Value = clsCommon.myCstr(dr("user_code"))
                gv_user.Rows(gv_user.Rows.Count - 1).Cells(colUsername).Value = clsCommon.myCstr(dr("user_name"))
                gv_user.Rows.AddNew()
            Next
        End If
        isLoadedData = False
    End Sub

    Sub OpenUsers(ByVal strcurusercode As String)
        Dim code As String = clsUserMaster.getFinder("", strcurusercode, True)

        If clsCommon.myLen(code) > 0 Then
            gv_user.CurrentRow.Cells(colUserCode).Value = code
            gv_user.CurrentRow.Cells(colUsername).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select user_name from tspl_user_master where user_code='" + code + "'"))
            'gv_user.CurrentRow.Cells(colUserSno).Value = clsCommon.myCstr(CInt(gv_user.CurrentRow.Index) + 1)
        Else
            gv_user.CurrentRow.Cells(colUserCode).Value = ""
            gv_user.CurrentRow.Cells(colUsername).Value = ""
            gv_user.CurrentRow.Cells(colUserSno).Value = ""
        End If
    End Sub

    Private Sub gv_user_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv_user.CellValueChanged
        Try
            If Not isLoadedData Then
                If isValueChanged Then
                    If e.Column Is gv_user.Columns(colUserCode) Then
                        isValueChanged = False
                        OpenUsers(clsCommon.myCstr(gv_user.CurrentRow.Cells(colUserCode).Value))
                        isValueChanged = True
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmNotificationScreen_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Dim qry As String = "drop table Temp_User_Notification"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        RadGroupBox1.Visible = False
    End Sub

    Private Sub gv_user_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv_user.CurrentColumnChanged
        If gv_user.RowCount > 0 Then
            Dim intCurrRow As Integer = gv_user.CurrentRow.Index
            gv_user.CurrentRow.Cells(colUserSno).Value = clsCommon.myCstr(CInt(intCurrRow) + 1)
            If intCurrRow = gv_user.Rows.Count - 1 Then
                gv_user.Rows.AddNew()
                gv_user.CurrentRow = gv_user.Rows(intCurrRow)
            End If
        End If
    End Sub

    Function AllowToSaveCriteria() As Boolean
        Try
            If dg_criteria.Rows(0).Cells("Status").Value = False AndAlso dg_criteria.Rows(1).Cells("Status").Value = False AndAlso dg_criteria.Rows(2).Cells("Status").Value = False Then
                Throw New Exception("Please Select Either Save and Post Or Both")
            End If
            If dg_criteria.Rows(0).Cells("Status").Value = True AndAlso dg_criteria.Rows(1).Cells("Status").Value = True AndAlso dg_criteria.Rows(2).Cells("Status").Value = True Then
                Throw New Exception("Please Select Either Save and Post Or Both," + Environment.NewLine + "Not Save/Post and Both options.Invalid selection")
            End If
            
            If dg_criteria.Rows(0).Cells("Status").Value = True AndAlso dg_criteria.Rows(2).Cells("Status").Value = True Then
                Throw New Exception("Please Select Either Save and Post Or Both," + Environment.NewLine + "Not Save and Both options.Invalid selection")
            ElseIf dg_criteria.Rows(1).Cells("Status").Value = True AndAlso dg_criteria.Rows(2).Cells("Status").Value = True Then
                Throw New Exception("Please Select Either Save and Post Or Both," + Environment.NewLine + "Not Post and Both options.Invalid selection")
            End If


            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Sub btnsave_criteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave_criteria.Click
        Try
            If AllowToSaveCriteria() Then
                Dim arr As New List(Of clsfrmNotificationScreen)
                Dim obj As clsfrmNotificationScreen
                Dim scncode As String = ""
                Dim values As String = "1" 'clsCommon.GetMulcallString(dg_criteria.CheckedValue)


                If clsCommon.myLen(values) > 0 Then
                    For i As Integer = 0 To dg_criteria.Rows.Count - 1
                        obj = New clsfrmNotificationScreen()
                        obj.modulecode = clsCommon.myCstr(txtModule.Value)
                        obj.doctype = Doctype
                        If dg_criteria.Rows(i).Cells(1).Value = True Then
                            obj.criteriavalue = clsCommon.myCstr(dg_criteria.Rows(i).Cells(0).Value)
                        Else
                            obj.criteriavalue = ""
                        End If
                        obj.scrncode = clsCommon.myCstr(gv.CurrentRow.Cells(colScreenCode).Value)
                        scncode = obj.scrncode


                        If clsCommon.myLen(obj.criteriavalue) > 0 Then
                            arr.Add(obj)
                        End If
                    Next

                    If arr.Count > 0 Then
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        If clsfrmNotificationScreen.SaveCriteriaGrid(txtModule.Value, Doctype, scncode, arr, trans) Then
                            Groupbox_criteria.Visible = False
                            gv.CurrentRow.Cells(colCriteria).Value = clsCommon.myCstr(arr.Count)
                        End If
                    End If
                End If
                Groupbox_criteria.Visible = False
            End If
        Catch ex As Exception
            Groupbox_criteria.Visible = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub tnclose_criteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tnclose_criteria.Click
        Groupbox_criteria.Visible = False
    End Sub

End Class
