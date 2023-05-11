'----Created By Monika 12/06/2014--------------
'----------------BM00000003414
Imports common
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class FrmMilkRecurringScheduler
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrLoc As String = Nothing
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True
    Dim IsInsieLoadData As Boolean
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmMilkRecurringScheduler)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadUser()
        Dim qry As String = "select User_Code as [Code],User_Name as [User Name] from TSPL_USER_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        dt.Columns.Add("Select", GetType(Boolean))
        dt.Columns("Select").SetOrdinal(0)

        cbguser.DataSource = Nothing
        cbguser.DataSource = dt

        cbguser.Columns("Code").Width = 180
        cbguser.Columns("Code").ReadOnly = True

        cbguser.Columns("User Name").Width = 250
        cbguser.Columns("User Name").ReadOnly = True

        cbguser.AllowDeleteRow = False
        cbguser.AllowAddNewRow = False
        cbguser.ShowGroupPanel = False
        cbguser.AllowColumnReorder = False
        cbguser.AllowRowReorder = False
        cbguser.EnableSorting = False
        cbguser.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        cbguser.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub Reset()
        TxtEveryDay.Text = ""
        fndcode.Value = ""
        txtdesc.Text = ""
        TxtMonthlyEvery.Text = ""
        TxtMonthlyOnthe.Text = ""
        CboSemiMonthCmb1.SelectedIndex = -1
        TxtRemindinAdvance.Text = 0
        DtpStartDate.Value = clsCommon.GETSERVERDATE()
        TxtWeeklyEvery.Text = ""
        TxtYearOnthe.Text = ""
        RdbDaily.Checked = True
        rdbDailyEvery.Checked = False
        RdbDailyTheseWorkDays.Checked = False
        RdbMonthly.Checked = False
        RdbMonthlyOnThe2.Checked = False
        RdbMonthlyOnTheDay1.Checked = False
        RdbSemiFirst.Checked = False
        RdbSemimonthly.Checked = False
        RdbSemiThe.Checked = False
        RdbTuesday.Checked = False
        RdbWeekly.Checked = False
        rdbWeeklyFriday.Checked = False
        RdbWeeklyMonday.Checked = False
        RdbweeklySaturday.Checked = False
        RdbWeeklySunday.Checked = False
        RdbWeeklyThursday.Checked = False
        RdbWeeklyWednesday.Checked = False
        RdbYearly.Checked = False
        RdbYearOnThe1.Checked = False
        RdbYearOnthe2.Checked = False
        ChkDailyFriday.Checked = False
        ChkDailyMonday.Checked = False
        ChkDailySunday.Checked = False
        ChkDailyThursday.Checked = False
        ChkDailySaturday.Checked = False
        ChkDailyTuesday.Checked = False
        ChkDailyWednesday.Checked = False

        CboMonthlyDays.SelectedIndex = -1
        CboMonthlyFirst.SelectedIndex = -1
        CboSemiMonthTheCombo.SelectedIndex = -1
        cboUser.SelectedIndex = -1
        CboYearDays.SelectedIndex = -1
        CboYearEvery.SelectedIndex = -1
        CboYearFirst.SelectedIndex = -1

        RadPageView1.Pages("PgDaily").Item.Visibility = ElementVisibility.Collapsed 'ElementVisibility.Visible
        RadPageView1.Pages("PgWeekly").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.Pages("PgSemi").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.Pages("PgMonthly").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.Pages("pgYearly").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = PgDaily

        btnsave.Text = "Save"
        btndelete.Enabled = False
        fndcode.MyReadOnly = False
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        isNewEntry = True

        GetUser()
        GetDays(CboSemiMonthTheCombo)
        GetDays(CboSemiMonthCmb1)
        GetDays(CboMonthlyDays)
        GetDays(CboYearDays)
        GetDays(CboYearEvery)
        GetDays(CboYearFirst)
        GetDays(CboMonthlyFirst)

        'GetFirstandLast(CboYearFirst)
        'GetFirstandLast(CboMonthlyFirst)
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub FrmMilkRecurringScheduler_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        NotifyIcon1.ShowBalloonTip(50000, "Click Here", "Its Me", ToolTipIcon.Info)
        NotifyIcon1.Text = "See this"
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtdesc.Text) <= 0 Then
                txtdesc.Focus()
                txtdesc.Select()
                Errorcontrol.SetError(txtdesc, "Please Fill Route Name")
                Throw New Exception("Please Fill Route Name")
            Else
                Errorcontrol.ResetError(txtdesc)
            End If
            If clsCommon.myLen(cboUser.Text) <= 0 Then
                cboUser.Focus()
                cboUser.Select()
                Errorcontrol.SetError(cboUser, "Please Fill User Name")
                Throw New Exception("Please Fill User Name")
            Else
                Errorcontrol.ResetError(cboUser)
            End If
            If Not RdbDaily.Checked And Not RdbWeekly.Checked And Not RdbSemimonthly.Checked And Not RdbMonthly.Checked And Not RdbYearly.Checked Then
                RdbDaily.Focus()
                RdbDaily.Select()
                Errorcontrol.SetError(RdbDaily, "Please Check One Daily/Weekly/Semi Monthly/Monthly/Yearly")
                Throw New Exception("Please Check One Daily/Weekly/Semi Monthly/Monthly/Yearly")
            Else
                Errorcontrol.ResetError(RdbDaily)
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboUser.SelectedValue), "N") = CompairStringResult.Equal Then
                Dim is_checked As Integer = 0
                For Each row As GridViewRowInfo In cbguser.Rows
                    If clsCommon.CompairString(clsCommon.myCstr(row.Cells("Select").Value), "True") = CompairStringResult.Equal Then
                        is_checked += 1
                    End If
                Next
                If is_checked <= 0 Then
                    cbguser.Focus()
                    Throw New Exception("Please Select atleast one user.")
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try
            Dim obj As New clsFrmMilkRecurringScheduler()

            obj.code = clsCommon.myCstr(fndcode.Value)
            obj.desc = clsCommon.myCstr(txtdesc.Text)
            obj.User_Mode = clsCommon.myCstr(cboUser.SelectedValue)
            obj.Remind_In_Advance = clsCommon.myCdbl(TxtRemindinAdvance.Text)
            obj.Sch_Start_date = clsCommon.myCstr(DtpStartDate.Value)

            If RdbDaily.Checked Then
                obj.Recurring_period = "Daily"
                ' obj.Duration = clsCommon.myCstr(TxtEveryDay.Text)
            ElseIf RdbWeekly.Checked Then
                obj.Recurring_period = "Weekly"
                ' obj.Duration = clsCommon.myCstr(TxtWeeklyEvery.Text)
            ElseIf RdbSemimonthly.Checked Then
                obj.Recurring_period = "Semi_M"
            ElseIf RdbMonthly.Checked Then
                obj.Recurring_period = "Monthly"
                'obj.Duration = clsCommon.myCstr(TxtMonthlyEvery.Text)
            ElseIf RdbYearly.Checked Then
                obj.Recurring_period = "Yearly"
                ' obj.Duration = clsCommon.myCstr(CboYearEvery.Text)
            End If

            Dim objList As New List(Of clsfrmMilkRecurringScheduler_Detail)


            Dim obj1 As clsfrmMilkRecurringScheduler_Detail
            '================================Daily==================================
            If RdbDaily.Checked Then
                If ChkDailySunday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Sunday")
                    objList.Add(obj1)
                End If
                If ChkDailyMonday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Monday")
                    objList.Add(obj1)
                End If
                If ChkDailyTuesday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Tuesday")
                    objList.Add(obj1)
                End If
                If ChkDailyWednesday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Wednesday")
                    objList.Add(obj1)
                End If
                If ChkDailyThursday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Thursday")
                    objList.Add(obj1)
                End If
                If ChkDailyFriday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Friday")
                    objList.Add(obj1)
                End If
                If ChkDailySaturday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Saturday")
                    objList.Add(obj1)
                End If


                '====================End Daily===================================
                '===========================Weekly=================================================

            ElseIf RdbWeekly.Checked Then
                If RdbWeeklySunday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Sunday")
                    objList.Add(obj1)
                ElseIf RdbWeeklyMonday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Monday")
                    objList.Add(obj1)
                ElseIf RdbTuesday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Tuesday")
                    objList.Add(obj1)
                ElseIf RdbWeeklyWednesday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Wednesday")
                    objList.Add(obj1)
                ElseIf RdbWeeklyThursday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Thursday")
                    objList.Add(obj1)
                ElseIf rdbWeeklyFriday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Friday")
                    objList.Add(obj1)
                ElseIf RdbweeklySaturday.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr("Saturday")
                    objList.Add(obj1)
                End If
                '==================================End Weekly=========================================
                '====================================Semi Monthly==================================
            ElseIf RdbSemimonthly.Checked Then
                If clsCommon.myLen(CboSemiMonthTheCombo.Text) > 0 Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr(CboSemiMonthTheCombo.SelectedValue)
                    objList.Add(obj1)
                End If
                ''========================End Semi Monthly===========================================
                '================================Monthly============================================
            ElseIf RdbMonthly.Checked Then
                If RdbMonthlyOnTheDay1.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr(TxtMonthlyOnthe.Text)
                    objList.Add(obj1)
                ElseIf RdbMonthlyOnThe2.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr(CboMonthlyDays.SelectedValue)
                    obj1.Day_Index = clsCommon.myCstr(CboMonthlyFirst.SelectedValue)
                    objList.Add(obj1)
                End If
                '=============================End Monthly==============================
                '================================Yearly============================================
            ElseIf RdbYearly.Checked Then
                If RdbYearOnThe1.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr(TxtYearOnthe.Text)
                    objList.Add(obj1)
                ElseIf RdbYearOnthe2.Checked Then
                    obj1 = New clsfrmMilkRecurringScheduler_Detail()
                    obj1.Doc_COde = fndcode.Value
                    obj1.Day_of_Week = clsCommon.myCstr(CboYearDays.SelectedValue)
                    obj1.Day_Index = clsCommon.myCstr(CboYearFirst.SelectedValue)
                    objList.Add(obj1)
                End If
                '=============================End Yearly==============================
            End If
            clsFrmMilkRecurringScheduler.arr_Recurring_Detail = objList
            Dim objUserList As New List(Of clsfrmMilkRecurringScheduler_User_Detail)
            For Each row As GridViewRowInfo In cbguser.Rows
                If clsCommon.CompairString(clsCommon.myCstr(row.Cells("Select").Value), "True") = CompairStringResult.Equal Then
                    Dim obj_user As New clsfrmMilkRecurringScheduler_User_Detail
                    obj_user.Doc_Code = fndcode.Value
                    obj_user.User_Code = row.Cells("Code").Value
                    objUserList.Add(obj_user)
                End If
            Next
            clsFrmMilkRecurringScheduler.arr_Recurring_User_Detail = objUserList
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsFrmMilkRecurringScheduler.SaveData(obj.code, trans, obj, isNewEntry) Then
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If

                btnsave.Text = "Update"
                btndelete.Enabled = True
                fndcode.Value = obj.code
                fndcode.MyReadOnly = True
                UcAttachment1.SaveData(fndcode.Value)
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
                fndcode.MyReadOnly = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(fndcode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Route Code For Deletion", Me.Text)
            fndcode.Focus()
            fndcode.Select()
            Errorcontrol.SetError(fndcode, "Please Select Route Code For Deletion")
            Return
        Else
            Errorcontrol.ResetError(fndcode)
        End If

        Dim qry As String = "select count(*) from tspl_mcc_route_master where route_code='" + fndcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found For Deletion", Me.Text)
            Return
        End If

        If Not clsCommon.MyMessageBoxShow("Are You Sure,Want To Delete The Route Master Of Route Code " + fndcode.Value + "?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
            Return
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            qry = "delete from tspl_mcc_route_master where route_code='" + fndcode.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
            trans.Commit()
            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub GetUser()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "N"
        dr("Name") = "Specific Users"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A"
        dr("Name") = "All Users"
        dt.Rows.Add(dr)

        cboUser.DataSource = dt
        cboUser.ValueMember = "Code"
        cboUser.DisplayMember = "Name"
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            IsInsieLoadData = True
            Dim obj As clsFrmMilkRecurringScheduler = clsFrmMilkRecurringScheduler.GetData(strCode, NavType)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
                isNewEntry = False
                fndcode.Value = obj.code
                txtdesc.Text = obj.desc
                cboUser.SelectedValue = obj.User_Mode
                TxtRemindinAdvance.Text = obj.Remind_In_Advance
                If clsCommon.myLen(obj.Sch_Start_date) > 0 Then
                    DtpStartDate.Value = obj.Sch_Start_date
                End If
                If clsCommon.myCstr(obj.Recurring_period) = "Daily" Then
                    RdbDaily.Checked = True
                    TxtEveryDay.Text = clsCommon.myCstr(obj.Duration)
                    If clsCommon.myCdbl(TxtEveryDay.Text) > 0 Then
                        rdbDailyEvery.Checked = True
                    Else
                        RdbDailyTheseWorkDays.Checked = True
                    End If
                ElseIf clsCommon.myCstr(obj.Recurring_period) = "Weekly" Then
                    RdbWeekly.Checked = True
                    TxtWeeklyEvery.Text = clsCommon.myCstr(obj.Duration)
                    'If clsCommon.myCdbl(TxtWeeklyEvery.Text) > 0 Then
                    '    RdbWeekly.Checked = True
                    'End If
                ElseIf clsCommon.myCstr(obj.Recurring_period) = "Semi_M" Then
                    RdbSemimonthly.Checked = True
                ElseIf clsCommon.myCstr(obj.Recurring_period) = "Monthly" Then
                    RdbMonthly.Checked = True
                    TxtMonthlyEvery.Text = clsCommon.myCstr(obj.Duration)
                ElseIf clsCommon.myCstr(obj.Recurring_period) = "Yearly" Then
                    RdbYearly.Checked = True
                    CboYearEvery.Text = clsCommon.myCstr(obj.Duration)
                End If

                If (clsFrmMilkRecurringScheduler.arr_Recurring_User_Detail IsNot Nothing AndAlso clsFrmMilkRecurringScheduler.arr_Recurring_User_Detail.Count > 0) Then
                    LoadUser()
                    For Each obj1 As clsfrmMilkRecurringScheduler_User_Detail In clsFrmMilkRecurringScheduler.arr_Recurring_User_Detail
                        For Each row As GridViewRowInfo In cbguser.Rows
                            If clsCommon.myCstr(row.Cells("Code").Value) = clsCommon.myCstr(obj1.User_Code) Then
                                row.Cells("Select").Value = True
                            End If
                        Next
                    Next
                End If

                        If (clsFrmMilkRecurringScheduler.arr_Recurring_Detail IsNot Nothing AndAlso clsFrmMilkRecurringScheduler.arr_Recurring_Detail.Count > 0) Then
                            For Each obj1 As clsfrmMilkRecurringScheduler_Detail In clsFrmMilkRecurringScheduler.arr_Recurring_Detail
                                '================================Daily==================================
                                If RdbDaily.Checked Then
                                    If obj1.Day_of_Week = "Sunday" Then
                                        ChkDailySunday.CheckState = CheckState.Checked
                                    End If
                                    If obj1.Day_of_Week = "Monday" Then
                                        ChkDailyMonday.CheckState = CheckState.Checked
                                    End If
                                    If obj1.Day_of_Week = "Tuesday" Then
                                        ChkDailyTuesday.CheckState = CheckState.Checked
                                    End If
                                    If obj1.Day_of_Week = "Wednesday" Then
                                        ChkDailyWednesday.CheckState = CheckState.Checked
                                    End If
                                    If obj1.Day_of_Week = "Thursday" Then
                                        ChkDailyThursday.CheckState = CheckState.Checked
                                    End If
                                    If obj1.Day_of_Week = "Friday" Then
                                        ChkDailyFriday.CheckState = CheckState.Checked
                                    End If
                                    If obj1.Day_of_Week = "Saturday" Then
                                        ChkDailySaturday.CheckState = CheckState.Checked
                                    End If


                                    '====================End Daily===================================
                                    '===========================Weekly=================================================

                                ElseIf RdbWeekly.Checked Then
                                    If obj1.Day_of_Week = "Saturday" Then
                                        RdbweeklySaturday.Checked = True
                                    ElseIf obj1.Day_of_Week = "Sunday" Then
                                        RdbWeeklySunday.Checked = True
                                    ElseIf obj1.Day_of_Week = "Monday" Then
                                        RdbWeeklyMonday.Checked = True
                                    ElseIf obj1.Day_of_Week = "Tuesday" Then
                                        RdbTuesday.Checked = True
                                    ElseIf obj1.Day_of_Week = "Wednesday" Then
                                        RdbWeeklyWednesday.Checked = True
                                    ElseIf obj1.Day_of_Week = "Thursday" Then
                                        RdbWeeklyThursday.Checked = True
                                    ElseIf obj1.Day_of_Week = "Friday" Then
                                        rdbWeeklyFriday.Checked = True
                                    End If
                                    '==================================End Weekly=========================================
                                    '====================================Semi Monthly==================================
                                ElseIf RdbSemimonthly.Checked Then
                                    CboSemiMonthTheCombo.SelectedValue = clsCommon.myCstr(obj1.Day_of_Week)
                                    If clsCommon.myLen(CboSemiMonthTheCombo.SelectedValue) > 0 Then
                                        RdbSemiThe.Checked = True
                                        RdbSemiFirst.Checked = False
                                    Else
                                        RdbSemiThe.Checked = False
                                        RdbSemiFirst.Checked = True
                                    End If
                                    ''========================End Semi Monthly===========================================
                                    '================================Monthly============================================
                                ElseIf RdbMonthly.Checked Then
                                    If obj1.Checked_Value = "1" Then
                                        RdbMonthlyOnTheDay1.Checked = True
                                    Else
                                        RdbMonthlyOnThe2.Checked = True
                                    End If
                            If RdbMonthlyOnTheDay1.Checked Then
                                TxtMonthlyOnthe.Text = clsCommon.myCstr(obj1.Day_of_Week)
                            ElseIf RdbMonthlyOnThe2.Checked Then
                                CboMonthlyDays.SelectedValue = clsCommon.myCstr(obj1.Day_of_Week)
                                CboMonthlyFirst.SelectedValue = clsCommon.myCstr(obj1.Day_Index)
                            End If
                                    '=============================End Monthly==============================
                                    '================================Yearly============================================
                                ElseIf RdbYearly.Checked Then
                                    If obj1.Checked_Value = "1" Then
                                        RdbYearOnThe1.Checked = True
                                    Else
                                        RdbYearOnthe2.Checked = True
                                    End If
                            If RdbYearOnThe1.Checked Then
                                CboYearEvery.SelectedValue = clsCommon.myCstr(obj1.Day_of_Week)
                            ElseIf RdbYearOnthe2.Checked Then
                                CboYearDays.SelectedValue = clsCommon.myCstr(obj1.Day_of_Week)
                                CboYearFirst.SelectedValue = clsCommon.myCstr(obj1.Day_Index)
                            End If
                                    '=============================End Yearly==============================
                                End If
                            Next
                        End If

                        btnsave.Text = "Update"
                        btndelete.Enabled = True
                        fndcode.MyReadOnly = True


                        UcAttachment1.LoadData(fndcode.Value)
            Else
                        Reset()
            End If
                IsInsieLoadData = False
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        LoadData(clsCommon.myCstr(fndcode.Value), NavType)
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim whrclas As String = ""
        If clsCommon.myLen(arrLoc) > 0 Then
            whrclas = " Tspl_Recurring_Scheduler_Head.Doc_code in (" + arrLoc + ")"
        End If

        Dim qry As String = "select count(*) from Tspl_Recurring_Scheduler_Head where Doc_code='" + fndcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            fndcode.MyReadOnly = True
        Else
            fndcode.MyReadOnly = False
        End If

        If fndcode.MyReadOnly Or isButtonClicked Then
            fndcode.Value = clsFrmMilkRecurringScheduler.getFinder(whrclas, fndcode.Value, isButtonClicked)

            If clsCommon.myLen(fndcode.Value) > 0 Then
                LoadData(fndcode.Value, NavigatorType.Current)
            End If
        End If
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Dim qry As String = "select count(*) from tspl_mcc_route_master"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select TSPL_MCC_ROUTE_MASTER.route_code as Code,TSPL_MCC_ROUTE_MASTER.route_name as [Route Name],TSPL_MCC_ROUTE_MASTER.mcc_code as [MCC Code],tspl_mcc_master.mcc_name as [MCC Name],TSPL_MCC_ROUTE_MASTER.KiloMeter,TSPL_MCC_ROUTE_MASTER.supervisor_name as [Supervisor Name],TSPL_MCC_ROUTE_MASTER.contact_no as [Contact No],TSPL_MCC_ROUTE_MASTER.add1 as Address1,TSPL_MCC_ROUTE_MASTER.add2 as Address2,TSPL_MCC_ROUTE_MASTER.add3 as Address3,TSPL_MCC_ROUTE_MASTER.country_code as [Country Code],tspl_country_master.country_name as [Country Name],TSPL_MCC_ROUTE_MASTER.state_code as [State Code],tspl_state_master.state_name as [State Name],TSPL_MCC_ROUTE_MASTER.city_code as [City Code],tspl_city_master.city_name as [City Name],TSPL_MCC_ROUTE_MASTER.email_id as [Email ID],TSPL_MCC_ROUTE_MASTER.effective_date as [Effective Date] from TSPL_MCC_ROUTE_MASTER left outer join tspl_mcc_master on Tspl_mcc_master.mcc_code=TSPL_MCC_ROUTE_MASTER.mcc_code left outer join tspl_country_master on TSPL_MCC_ROUTE_MASTER.country_code=tspl_country_master.country_code left outer join tspl_state_master on tspl_state_master.state_code=TSPL_MCC_ROUTE_MASTER.state_code left outer join tspl_city_master on tspl_city_master.city_code=TSPL_MCC_ROUTE_MASTER.city_code"
        Else
            qry = "select '' as Code,'' as [Route Name],'' as [MCC Code],'' as [MCC Name],'' as KiloMeter,'' as [Supervisor Name],'' as [Contact No],'' as Address1,'' as Address2,'' as Address3,'' as [Country Code],'' as [Country Name],'' as [State Code],'' as [State Name],'' as [City Code],'' as [City Name],'' as [Email ID],'' as [Effective Date]"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Code", "Route Name", "MCC Code", "MCC Name", "KiloMeter", "Supervisor Name", "Contact No", "Address1", "Address2", "Address3", "Country Code", "Country Name", "State Code", "State Name", "City Code", "City Name", "Email ID", "Effective Date") Then
            Try
                clsCommon.ProgressBarShow()
                Dim obj As clsFrmMilkRecurringScheduler
                Dim counter As Integer = 1

                For Each grow As GridViewRowInfo In gv.Rows
                    obj = New clsFrmMilkRecurringScheduler()

                    obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.code) <= 0 Or clsCommon.myLen(obj.code) > 30 Then
                        Throw New Exception("Please Fill Route Code(Max. 30 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '-----------------------------

                    obj.desc = clsCommon.myCstr(grow.Cells("Route Name").Value)
                    If clsCommon.myLen(obj.desc) <= 0 Or clsCommon.myLen(obj.desc) > 150 Then
                        Throw New Exception("Please Fill Route Name(Max. 150 Characters) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    '--------------------------------

                    Dim qry As String = ""
                    Dim check As Integer = 0

                    'obj.vehiclecode = clsCommon.myCstr(grow.Cells("Vehicle Code").Value)
                    'obj.vehiclename = clsCommon.myCstr(grow.Cells("Vehicle Name").Value)
                    'If clsCommon.myLen(obj.vehiclecode) <= 0 AndAlso clsCommon.myLen(obj.vehiclename) <= 0 Then
                    '    Throw New Exception("Please Fill Vehicle Details At Line No. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'If clsCommon.myLen(obj.vehiclecode) > 0 Then
                    '    qry = "select count(*) from tspl_primary_vehicle_master where vehicle_code='" + obj.vehiclecode + "'"
                    '    check = clsDBFuncationality.getSingleValue(qry)

                    '    If check <= 0 Then
                    '        qry = "select count(*) from tspl_primary_vehicle_master where description='" + obj.vehiclename + "'"
                    '        check = clsDBFuncationality.getSingleValue(qry)

                    '        If check <= 0 Then
                    '            obj.vehiclecode = ""
                    '            Throw New Exception("Filled Vehicle Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                    '        End If
                    '    End If
                    'End If
                    'If clsCommon.myLen(obj.vehiclecode) <= 0 Then
                    '    qry = "select count(*) from tspl_primary_vehicle_master where description='" + obj.vehiclename + "'"
                    '    check = clsDBFuncationality.getSingleValue(qry)

                    '    If check <= 0 Then
                    '        Throw New Exception("Filled Vehicle Details Is Invlaid Or Does Not Exist,See At Line No. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    'End If
                    ''-----------------------------------

                    qry = "select count(*) from tspl_mcc_route_master where route_code='" + obj.code + "'"
                    check = clsDBFuncationality.getSingleValue(qry)

                    If check <= 0 Then
                        isNewEntry = True
                    Else
                        isNewEntry = False
                    End If

                    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                    If clsFrmMilkRecurringScheduler.SaveData(obj.code, trans, obj, isNewEntry) Then
                    Else
                        Throw New Exception("No Data Transfer")
                    End If

                    counter += 1
                Next

                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
                Reset()
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub FrmMilkRecurringScheduler_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub RdbDaily_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbDaily.CheckedChanged
        If RdbDaily.Checked Then
            RadPageView1.Pages("PgDaily").Item.Visibility = ElementVisibility.Collapsed 'ElementVisibility.Visible
            RadPageView1.Pages("PgWeekly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgSemi").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgMonthly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("pgYearly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.SelectedPage = PgDaily
        End If
    End Sub

    Private Sub RdbWeekly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbWeekly.CheckedChanged
        If RdbWeekly.Checked Then
            RadPageView1.Pages("PgDaily").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgWeekly").Item.Visibility = ElementVisibility.Visible
            RadPageView1.Pages("PgSemi").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgMonthly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("pgYearly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.SelectedPage = PgWeekly
        End If
    End Sub

    Private Sub RdbSemimonthly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbSemimonthly.CheckedChanged
        If RdbSemimonthly.Checked Then
            RadPageView1.Pages("PgDaily").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgWeekly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgSemi").Item.Visibility = ElementVisibility.Visible
            RadPageView1.Pages("PgMonthly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("pgYearly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.SelectedPage = PgSemi
        End If
    End Sub

    Private Sub RdbMonthly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbMonthly.CheckedChanged
        If RdbMonthly.Checked Then
            RadPageView1.Pages("PgDaily").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgWeekly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgSemi").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgMonthly").Item.Visibility = ElementVisibility.Visible
            RadPageView1.Pages("pgYearly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.SelectedPage = PgMonthly
        End If
    End Sub

    Private Sub RdbYearly_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbYearly.CheckedChanged
        If RdbYearly.Checked Then
            RadPageView1.Pages("PgDaily").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgWeekly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgSemi").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("PgMonthly").Item.Visibility = ElementVisibility.Collapsed
            RadPageView1.Pages("pgYearly").Item.Visibility = ElementVisibility.Visible
            RadPageView1.SelectedPage = pgYearly
        End If
    End Sub

    Public Sub GetDays(ByVal Cmb As common.Controls.MyComboBox)
        Dim Dt As New DataTable
        Dt.Columns.Add("Code", GetType(String))
        Dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Dt.NewRow()
        dr("Code") = "1"
        dr("Name") = "1st"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "2"
        dr("Name") = "2nd"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "3"
        dr("Name") = "3rd"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "4"
        dr("Name") = "4th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "5"
        dr("Name") = "5th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "6"
        dr("Name") = "6th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "7"
        dr("Name") = "7th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "8"
        dr("Name") = "8th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "9"
        dr("Name") = "9th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "10"
        dr("Name") = "10th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "11"
        dr("Name") = "11th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "12"
        dr("Name") = "12th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "13"
        dr("Name") = "13th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "14"
        dr("Name") = "14th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "15"
        dr("Name") = "15th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "16"
        dr("Name") = "16th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "17"
        dr("Name") = "17th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "18"
        dr("Name") = "18th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "19"
        dr("Name") = "19th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "20"
        dr("Name") = "20th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "21"
        dr("Name") = "21st"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "22"
        dr("Name") = "22nd"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "23"
        dr("Name") = "23rd"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "24"
        dr("Name") = "24th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "25"
        dr("Name") = "25th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "26"
        dr("Name") = "26th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "27"
        dr("Name") = "27th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "28"
        dr("Name") = "28th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "29"
        dr("Name") = "29th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "30"
        dr("Name") = "30th"
        Dt.Rows.Add(dr)

        dr = Dt.NewRow()
        dr("Code") = "31"
        dr("Name") = "31st"
        Dt.Rows.Add(dr)


        Cmb.DataSource = Dt
        Cmb.ValueMember = "Code"
        Cmb.DisplayMember = "Name"
    End Sub

    Private Sub GetFirstandLast(ByVal cmb As common.Controls.MyComboBox)
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "First Day"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Last Day"
        dt.Rows.Add(dr)

        cmb.DataSource = dt
        cmb.ValueMember = "Code"
        cmb.DisplayMember = "Name"
    End Sub

    Private Sub RdbSemiThe_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdbSemiThe.CheckedChanged
        If Not RdbSemiThe.Checked Then
            CboSemiMonthTheCombo.SelectedValue = Nothing
        End If
    End Sub

    Private Sub cboUser_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUser.SelectedValueChanged
        Try
            If cboUser.SelectedValue = "N" Then
                LoadUser()
                RadPageView1.Pages("Pg_Users").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("Pg_Users").Item.Visibility = ElementVisibility.Collapsed
            End If
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub TxtRemindinAdvance_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtRemindinAdvance.KeyPress
        Try
            If Not IsNumeric(e.KeyChar) Then
                e.Handled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub NotifyIcon1_MouseMove(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseMove
        NotifyIcon1.Text = "Rohit"
    End Sub
End Class
