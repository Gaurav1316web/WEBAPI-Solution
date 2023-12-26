Imports common
Imports System.Data.SqlClient
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.UI.Export

Public Class FrmScheduling
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim dt As DataTable
    Dim DocType As String
    Dim IsLoadData As Boolean = False
    Dim isaddnewcriteria As Boolean = True
    Const colSelect As String = "Select"
    Const colScreenCode As String = "ScreenCode"
    Const colScreenName As String = "ScreenName"
    Const colCriteria As String = "Criteria"
    Const colQuuarter As String = "Quaarter"
    Const colNotification As String = "NotificationMessage"
    Const colValidation As String = "Validation"
    Const colDays As String = "colDays"

    Private Sub FrmScheduling_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
    End Sub

    Sub LoadScreenGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        gv.MasterTemplate.Columns.Add(repoSelect)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Screen Code"
        repoICode.Name = colScreenCode
        repoICode.Width = 100
        repoICode.HeaderImage = Global.XpertERPAdminServices.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Screen"
        repoIName.Name = colScreenName
        repoIName.Width = 250
        repoIName.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoIName)

        Dim repoCriteria As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCriteria.HeaderText = "Criteria"
        repoCriteria.Name = colCriteria
        repoCriteria.Width = 100
        repoCriteria.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(repoCriteria)

        Dim repoQuarter As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoQuarter.HeaderText = "Quarter"
        repoQuarter.Name = colQuuarter
        repoQuarter.Width = 100
        repoQuarter.DataSource = GetQuarter()
        repoQuarter.ValueMember = "Code"
        repoQuarter.DisplayMember = "Desc"
        repoQuarter.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv.MasterTemplate.Columns.Add(repoQuarter)

        Dim repoNotification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNotification.FormatString = ""
        repoNotification.HeaderText = "Notification"
        repoNotification.Name = colNotification
        repoNotification.Width = 450
        gv.MasterTemplate.Columns.Add(repoNotification)

        Dim repoValidation As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoValidation.FormatString = ""
        repoValidation.HeaderText = "Validation"
        repoValidation.DataSource = GetValidationType()
        repoValidation.ValueMember = "Code"
        repoValidation.DisplayMember = "Code"
        repoValidation.Name = colValidation
        repoValidation.Width = 120
        gv.MasterTemplate.Columns.Add(repoValidation)

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

    Sub LoadLoginGrid()
        gvLogin.Rows.Clear()
        gvLogin.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        gvLogin.MasterTemplate.Columns.Add(repoSelect)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Screen Code"
        repoICode.Name = colScreenCode
        repoICode.Width = 100
        repoICode.HeaderImage = Global.XpertERPAdminServices.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        gvLogin.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Screen"
        repoIName.Name = colScreenName
        repoIName.Width = 250
        repoIName.ReadOnly = True
        gvLogin.MasterTemplate.Columns.Add(repoIName)

        Dim repoCriteria As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCriteria.HeaderText = "Criteria"
        repoCriteria.Name = colCriteria
        repoCriteria.Width = 100
        repoCriteria.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvLogin.MasterTemplate.Columns.Add(repoCriteria)

        Dim repoDays As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDays.HeaderText = "Days Before/After"
        repoDays.Name = colDays
        repoDays.Width = 110
        repoDays.FormatString = "{0:f0}"
        repoDays.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvLogin.MasterTemplate.Columns.Add(repoDays)

        Dim repoNotification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNotification.FormatString = ""
        repoNotification.HeaderText = "Notification"
        repoNotification.Name = colNotification
        repoNotification.Width = 420
        gvLogin.MasterTemplate.Columns.Add(repoNotification)

        gvLogin.AllowEditRow = True
        gvLogin.AllowDeleteRow = True
        gvLogin.AllowAddNewRow = False
        gvLogin.ShowGroupPanel = False
        gvLogin.AllowColumnReorder = False
        gvLogin.AllowRowReorder = False
        gvLogin.EnableSorting = False
        gvLogin.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLogin.MasterTemplate.ShowRowHeaderColumn = False
        gvLogin.Rows.AddNew()
    End Sub

    Private Function GetQuarter() As DataTable
        Dim dt1 As New DataTable
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Desc", GetType(String))
        dt1.Rows.Add("0", "Select")
        dt1.Rows.Add("1", "First")
        dt1.Rows.Add("2", "Seccond")
        dt1.Rows.Add("3", "Third")
        dt1.Rows.Add("4", "Fourth")
        Return dt1
    End Function

    Private Function GetValidationType() As DataTable
        Dim dt1 As New DataTable
        dt1.Columns.Add("Code", GetType(String))
        dt1.Rows.Add("Required Approval")
        dt1.Rows.Add("Full Stop")
        Return dt1
    End Function

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmScheduling)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '--------richa Ticket no. BM00000003014 on  11/07/2014
        btnSave.Visible = MyBase.isModifyFlag
        RadButton1.Visible = MyBase.isExport
    End Sub

    Private Sub LoadScreenData(ByVal strModuleCode As String)
        Try
            IsLoadData = True
            LoadScreenGrid()
            If clsCommon.myLen(txtModule.Value) > 0 Then
                dt = clsScreenNotificationSchedule.GetScreenData(strModuleCode, DocType)
                For Each dr As DataRow In dt.Rows
                    gv.CurrentRow.Cells(colSelect).Value = dr("Scheduling")
                    gv.CurrentRow.Cells(colScreenCode).Value = clsCommon.myCstr(dr("Screen_Code"))
                    gv.CurrentRow.Cells(colScreenName).Value = clsCommon.myCstr(dr("Program_Name"))
                    gv.CurrentRow.Cells(colCriteria).Value = clsCommon.myCstr(dr("Criteria"))
                    gv.CurrentRow.Cells(colQuuarter).Value = clsCommon.myCstr(dr("Quarter"))
                    gv.CurrentRow.Cells(colNotification).Value = clsCommon.myCstr(dr("Notification"))
                    gv.CurrentRow.Cells(colValidation).Value = clsCommon.myCstr(dr("Validation"))
                    If gv.CurrentRow.Cells(colSelect).Value = True Then
                        gv.CurrentRow.Cells(colCriteria).ReadOnly = False
                        gv.CurrentRow.Cells(colQuuarter).ReadOnly = False
                        gv.CurrentRow.Cells(colNotification).ReadOnly = False
                        gv.CurrentRow.Cells(colValidation).ReadOnly = False
                    Else
                        gv.CurrentRow.Cells(colCriteria).ReadOnly = True
                        gv.CurrentRow.Cells(colQuuarter).ReadOnly = False
                        gv.CurrentRow.Cells(colValidation).ReadOnly = True
                        gv.CurrentRow.Cells(colNotification).ReadOnly = True
                    End If
                    gv.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub LoadLoginData(ByVal strModuleCode As String)
        Try
            IsLoadData = True
            LoadLoginGrid()
            If clsCommon.myLen(txtModule.Value) > 0 Then
                dt = clsScreenNotificationSchedule.GetLoginData(strModuleCode, DocType)
                For Each dr As DataRow In dt.Rows
                    gvLogin.CurrentRow.Cells(colSelect).Value = dr("Scheduling")
                    gvLogin.CurrentRow.Cells(colScreenCode).Value = clsCommon.myCstr(dr("Screen_Code"))
                    gvLogin.CurrentRow.Cells(colScreenName).Value = clsCommon.myCstr(dr("Program_Name"))
                    gvLogin.CurrentRow.Cells(colCriteria).Value = clsCommon.myCstr(dr("Criteria"))
                    gvLogin.CurrentRow.Cells(colNotification).Value = clsCommon.myCstr(dr("Notification"))
                    If gvLogin.CurrentRow.Cells(colSelect).Value = True Then
                        gvLogin.CurrentRow.Cells(colCriteria).ReadOnly = False
                        gvLogin.CurrentRow.Cells(colNotification).ReadOnly = False
                    Else
                        gvLogin.CurrentRow.Cells(colCriteria).ReadOnly = True
                        gvLogin.CurrentRow.Cells(colNotification).ReadOnly = True
                    End If
                    gvLogin.CurrentRow.Cells(colDays).Value = clsCommon.myCdbl(dr("Days"))
                    gvLogin.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Reset()
        Try
            isaddnewcriteria = True
            txtModule.Value = ""
            lblModule.Text = ""
            chkTransaction.IsChecked = True
            LoadScreenGrid()
            LoadLoginGrid()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub txtModule__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtModule._MYValidating
        Try
            qry = "Select Program_Code as Code, [Program_Name] as Description from TSPL_PROGRAM_MASTER"
            Dim WhrCls As String = " Type='M'"
            If objCommonVar.IsDemoERP Then
                WhrCls += " and Program_Code not in ('MSales')"
            Else
                WhrCls += " and Program_Code not in ('MSalesNew')"
            End If
            txtModule.Value = clsCommon.ShowSelectForm("NotScheduleModule", qry, "Code", WhrCls, txtModule.Value, "SNo", isButtonClicked)
            lblModule.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Program_Name from TSPL_PROGRAM_MASTER WHERE Program_Code='" + txtModule.Value + "'"))
            '---------------for adding criteria------------------Monika---------------
            AddNewCriteria()
            '-------------------------------------------------------------------
            LoadScreenData(txtModule.Value)
            LoadLoginData(txtModule.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub



    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmScheduling, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim Arr As New List(Of clsScreenNotificationSchedule)
                Dim obj As clsScreenNotificationSchedule
                '------------Screen Level Settings----------------------
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells(colScreenCode).Value) > 0 Then
                        obj = New clsScreenNotificationSchedule()
                        obj.Scheduling = IIf(grow.Cells(colSelect).Value = True, "Y", "N")
                        obj.Module_Code = clsCommon.myCstr(txtModule.Value)
                        obj.Screen_Code = clsCommon.myCstr(grow.Cells(colScreenCode).Value)
                        obj.Criteria = clsCommon.myCstr(grow.Cells(colCriteria).Value)
                        obj.Quarter = clsCommon.myCdbl(grow.Cells(colQuuarter).Value)
                        obj.Notification = clsCommon.myCstr(grow.Cells(colNotification).Value)
                        obj.Validation = clsCommon.myCstr(grow.Cells(colValidation).Value)
                        obj.Level = "Screen"
                        Arr.Add(obj)
                    End If
                Next
                '------------Login Level Settings-----------------------
                For Each grow As GridViewRowInfo In gvLogin.Rows
                    If clsCommon.myLen(grow.Cells(colScreenCode).Value) > 0 Then
                        obj = New clsScreenNotificationSchedule()
                        obj.Scheduling = IIf(grow.Cells(colSelect).Value = True, "Y", "N")
                        obj.Module_Code = clsCommon.myCstr(txtModule.Value)
                        obj.Screen_Code = clsCommon.myCstr(grow.Cells(colScreenCode).Value)
                        obj.Criteria = clsCommon.myCstr(grow.Cells(colCriteria).Value)
                        obj.Days = clsCommon.myCdbl(grow.Cells(colDays).Value)
                        obj.Notification = clsCommon.myCstr(grow.Cells(colNotification).Value)
                        obj.Validation = ""
                        obj.Level = "Login"
                        Arr.Add(obj)
                    End If
                Next
                If clsScreenNotificationSchedule.SaveData(Arr) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
                    LoadScreenData(txtModule.Value)
                    LoadLoginData(txtModule.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtModule.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Module.", Me.Text)
            txtModule.Focus()
            Return False
        End If
        Dim Count As Integer = 0
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.myLen(grow.Cells(colScreenCode).Value) > 0 Then
                Count += 1
            End If
            If grow.Cells(colSelect).Value = True And clsCommon.myLen(grow.Cells(colScreenCode).Value) Then
                If clsCommon.myLen(grow.Cells(colCriteria).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Screen Settings:- Please Select 'Criteria' for Screen '" + clsCommon.myCstr(grow.Cells(colScreenName).Value) + "'")
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colNotification).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Screen Settings:- Please Provide 'Notification Message' for Screen '" + clsCommon.myCstr(grow.Cells(colScreenName)) + "'")
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colValidation).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Screen Settings:- Please Select 'Validation' for Screen '" + clsCommon.myCstr(grow.Cells(colScreenName).Value) + "'")
                    Return False
                End If
            End If
        Next
        For Each grow As GridViewRowInfo In gvLogin.Rows
            If clsCommon.myLen(grow.Cells(colScreenCode).Value) > 0 Then
                Count += 1
            End If
            If grow.Cells(colSelect).Value = True And clsCommon.myLen(grow.Cells(colScreenCode).Value) Then
                If clsCommon.myLen(grow.Cells(colCriteria).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Login Settings:- Please Select 'Criteria' for Screen '" + clsCommon.myCstr(grow.Cells(colScreenName).Value) + "'")
                    Return False
                End If
                If clsCommon.myLen(grow.Cells(colNotification).Value) <= 0 Then
                    clsCommon.MyMessageBoxShow("Login Settings:- Please Provide 'Notification Message' for Screen '" + clsCommon.myCstr(grow.Cells(colScreenName).Value) + "'")
                    Return False
                End If
            End If
        Next
        If Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No screen found for notification.", Me.Text)
            gv.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If IsLoadData = False Then
                If e.Column Is gv.Columns(colSelect) Then
                    If gv.CurrentRow.Cells(colSelect).Value = True Then
                        gv.CurrentRow.Cells(colCriteria).ReadOnly = False
                        gv.CurrentRow.Cells(colNotification).ReadOnly = False
                        gv.CurrentRow.Cells(colValidation).ReadOnly = False
                    Else
                        gv.CurrentRow.Cells(colCriteria).ReadOnly = True
                        gv.CurrentRow.Cells(colNotification).ReadOnly = True
                        gv.CurrentRow.Cells(colValidation).ReadOnly = False
                    End If
                End If
                If e.Column Is gv.Columns(colScreenCode) Then
                    OpenScreen("Screen", False)
                End If
                If e.Column Is gv.Columns(colCriteria) Then
                    isaddnewcriteria = True
                    OpenCriteria(clsCommon.myCstr(gv.CurrentRow.Cells(colScreenCode).Value), "Screen", False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvLogin_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvLogin.CellValueChanged
        Try
            If IsLoadData = False Then
                If e.Column Is gvLogin.Columns(colSelect) Then
                    If gvLogin.CurrentRow.Cells(colSelect).Value = True Then
                        gvLogin.CurrentRow.Cells(colCriteria).ReadOnly = False
                        gvLogin.CurrentRow.Cells(colNotification).ReadOnly = False
                    Else
                        gvLogin.CurrentRow.Cells(colCriteria).ReadOnly = True
                        gvLogin.CurrentRow.Cells(colNotification).ReadOnly = True
                    End If
                End If
                If e.Column Is gvLogin.Columns(colScreenCode) Then
                    OpenScreen("Login", False)
                End If
                If e.Column Is gvLogin.Columns(colCriteria) Then
                    isaddnewcriteria = True
                    OpenCriteria(clsCommon.myCstr(gvLogin.CurrentRow.Cells(colScreenCode).Value), "Login", False)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
                gv.CurrentRow.Cells(colSelect).Value = False
                gv.CurrentRow.Cells(colQuuarter).Value = "0"
            End If
        End If
    End Sub

    Private Sub gvLogin_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvLogin.CurrentColumnChanged
        If gvLogin.RowCount > 0 Then
            Dim intCurrRow As Integer = gvLogin.CurrentRow.Index
            If intCurrRow = gvLogin.Rows.Count - 1 Then
                gvLogin.Rows.AddNew()
                gvLogin.CurrentRow = gvLogin.Rows(intCurrRow)
                gvLogin.CurrentRow.Cells(colSelect).Value = False
            End If
        End If
    End Sub

    Sub OpenScreen(ByVal Level As String, ByVal isButtonClick As Boolean)
        Dim qry As String = "Select Program_Code as Code, Program_Name as Name, Case When Parent_Code like '%Trans' Then 'Transaction' When Parent_Code like '%Setup' Then 'Master' End as Type from TSPL_PROGRAM_MASTER"
        Dim whrCls As String = "Parent_Code in (Select Program_Code from TSPL_PROGRAM_MASTER Where Parent_Code='" + txtModule.Value + "' AND (Program_Code like '%Trans' OR Program_Code Like '%Setup'))"
        If clsCommon.CompairString(Level, "Screen") = CompairStringResult.Equal Then
            gv.CurrentRow.Cells(colScreenCode).Value = clsCommon.ShowSelectForm("Sscrfnd@Scheduling", qry, "Code", whrCls, clsCommon.myCstr(gv.CurrentRow.Cells(colScreenCode).Value), "Type, Code", isButtonClick)
            gv.CurrentRow.Cells(colScreenName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Program_Name from TSPL_PROGRAM_MASTER WHERE Program_Code='" + gv.CurrentRow.Cells(colScreenCode).Value + "'"))
            gv.CurrentRow.Cells(colSelect).Value = True
        Else
            gvLogin.CurrentRow.Cells(colScreenCode).Value = clsCommon.ShowSelectForm("Sscrfnd@Scheduling", qry, "Code", whrCls, clsCommon.myCstr(gvLogin.CurrentRow.Cells(colScreenCode).Value), "Type, Code", isButtonClick)
            gvLogin.CurrentRow.Cells(colScreenName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Program_Name from TSPL_PROGRAM_MASTER WHERE Program_Code='" + gvLogin.CurrentRow.Cells(colScreenCode).Value + "'"))
            gvLogin.CurrentRow.Cells(colSelect).Value = True
        End If
    End Sub

    Sub AddNewCriteria()
        Try
            clsScrenScheduling.FillDataValues(txtModule.Value, DocType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenCriteria(ByVal strScreenCode As String, ByVal Level As String, ByVal isButtonClick As Boolean)

        qry = "Select Criteria from TSPL_NOTIFICATION_SETTING_CRITERIA"
        If clsCommon.CompairString(Level, "Screen") = CompairStringResult.Equal Then
            gv.CurrentRow.Cells(colCriteria).Value = clsCommon.ShowSelectForm("Sscrfnd@Scheduling", qry, "Criteria", "Screen_Code= '" + strScreenCode + "' AND Level='Screen'", clsCommon.myCstr(gv.CurrentRow.Cells(colCriteria).Value), "Criteria", isButtonClick)
        Else
            gvLogin.CurrentRow.Cells(colCriteria).Value = clsCommon.ShowSelectForm("Sscrfnd@Scheduling", qry, "Criteria", "Screen_Code= '" + strScreenCode + "' AND Level='Login'", clsCommon.myCstr(gvLogin.CurrentRow.Cells(colCriteria).Value), "Criteria", isButtonClick)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkTransaction_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkTransaction.ToggleStateChanged
        If chkTransaction.IsChecked Then
            DocType = "Trans"
        Else
            DocType = "Setup"
        End If
        LoadScreenData(txtModule.Value)
        LoadLoginData(txtModule.Value)
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        'gv.DataSource = clsDBFuncationality.GetDataTable("Select * from TSPL_JOURNAL_DETAILS")
        'Dim exporter As ExportToExcelML = New ExportToExcelML(Me.gv)
        'exporter.ExportVisualSettings = True
        'exporter.SheetMaxRows = ExcelMaxRows._1048576
        'Dim fileName As String = "C:\\ExportedData.xls"
        'exporter.RunExport(fileName)
        '--------richa Ticket no. BM00000003014 on  11/07/2014
        Dim str As String
        str = "Select Journal_No as [Journal No],Voucher_No as [Voucher No],Voucher_Date as [Voucher Date],Detail_Line_No as [Detail Line No],Account_code as [Account code],Account_Desc as [Account Desc],Amount as Amount,Description as Description,Reference as [Reference],Posting_Date as [Posting Date],Account_Type as [Account Type],Account_Group_Code as [Account Group Code],Account_Seg_Code1 as [Account Seg Code1],Account_Seg_Desc1 as [Account Seg Desc1],Account_Seg_Code2 as [Account Seg Code2],Account_Seg_Desc2 as [Account Seg Desc2],Account_Seg_Code3 as [Account Seg Code3],Account_Seg_Desc3 as [Account Seg Desc3],Account_Seg_Code4 as [Account Seg Code4],Account_Seg_Desc4 as [Account Seg Desc4],Account_Seg_Code5 as [Account Seg Code5],Account_Seg_Desc5 as [Account Seg Desc5],Account_Seg_Code6 as [Account Seg Code6],Account_Seg_Desc6 as [Account Seg Desc6],Account_Seg_Code7 as [Account Seg Code7],Account_Seg_Desc7 as [Account Seg Desc7],Account_Seg_Code8 as [Account Seg Code8],Account_Seg_Desc8 as [Account Seg Desc8],Account_Seg_Code9 as [Account Seg Code9],Account_Seg_Desc9 as [Account Seg Desc9],Account_Seg_Code10 as [Account Seg Code10],Account_Seg_Desc10 as [Account Seg Desc10],CustVend_Code as [CustVend Code],CustVend_Name as [CustVend Name] from TSPL_JOURNAL_DETAILS"
        transportSql.ExporttoExcel(str, Me)
        '--------------------------------------------------
    End Sub
End Class
