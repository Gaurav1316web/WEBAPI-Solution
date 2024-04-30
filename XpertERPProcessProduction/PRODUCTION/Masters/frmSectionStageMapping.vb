' ----------------- Created By Anubhooti On 3-July-2014 Against BM00000003068-------------------- '
'--By Monika--------BM00000003317-------------change overall design and coding
'====================BM00000003773-------------BM00000004444--------BM00000004951--------
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmSectionStageMapping
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim SkipLogSheet As Boolean = False
    Dim PageMode As String
    Dim change As Boolean = True
    Dim DepartmentSelect As Boolean = False
    Const colsno As String = "Sno"
    Const colSequnceno As String = "Sequenceno"
    Const colStageCode As String = "Stage Code"
    Const colDescription As String = "Description"
    Const colDepartcode As String = "Depart_Code"
    Const colDepartname As String = "DepartName"
    Const colLogsheetno As String = "Log Sheet"
    Const colLogsheetDesc As String = "LogSheetDesc"
    Const colUserCode As String = "UserCode"
    Const colNoOfUser As String = "No.ofUser"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim Errorcontrol As New clsErrorControl()

    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim userCode, companyCode As String
    Dim isNewEntry As Boolean = True
    Dim dtcbo As DataTable
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmSectionStageMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If

        btnSave.Visible = Me.isModifyFlag
        btnDelete.Visible = Me.isDeleteFlag
    End Sub

    Private Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim reposno As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposno.Width = 70
        reposno.DecimalPlaces = 0
        reposno.HeaderText = "S.No."
        reposno.Name = colsno
        reposno.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reposno)

        Dim repostagecode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repostagecode.Width = 130
        repostagecode.HeaderText = "Stage Code"
        repostagecode.Name = colStageCode
        repostagecode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repostagecode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repostagecode)

        Dim repostage As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repostage.Width = 250
        repostage.HeaderText = "Description"
        repostage.Name = colDescription
        repostage.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repostage)

        Dim repologsheet As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repologsheet.Width = 130
        repologsheet.HeaderText = "Log Sheet No."
        repologsheet.Name = colLogsheetno
        repologsheet.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repologsheet.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repologsheet)

        Dim repologsheetdesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repologsheetdesc.Width = 130
        repologsheetdesc.HeaderText = "Log Description"
        repologsheetdesc.Name = colLogsheetDesc
        repologsheetdesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repologsheetdesc)

        Dim reposequnc As GridViewDecimalColumn = New GridViewDecimalColumn()
        reposequnc.Width = 70
        reposequnc.DecimalPlaces = 0
        reposequnc.HeaderText = "Sequence No."
        reposequnc.Name = colSequnceno
        reposequnc.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(reposequnc)

        Dim repodepartcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodepartcode.Width = 120
        repodepartcode.HeaderText = "Department Code"
        repodepartcode.Name = colDepartcode
        repodepartcode.IsVisible = False
        repodepartcode.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repodepartcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(repodepartcode)

        Dim repodeprtname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodeprtname.Width = 200
        repodeprtname.HeaderText = "No. of Department"
        repodeprtname.Name = colDepartname
        repodeprtname.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repodeprtname)

        Dim repousercode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repousercode.Width = 60
        repousercode.HeaderText = "User Code"
        repousercode.Name = colUserCode
        repousercode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repousercode)

        Dim reponoofuser As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponoofuser.Width = 70
        reponoofuser.HeaderText = "No. of Users"
        reponoofuser.Name = colNoOfUser
        reponoofuser.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(reponoofuser)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.Rows.AddNew()

        gv1.Rows(0).Cells(colNoOfUser).Value = "Double Click"
        gv1.Rows(0).Cells(colDepartname).Value = "Double Click"
    End Sub

    Private Sub LoadBlankUserGrid()
        Dim qry As String = "select User_Code as Code,User_Name as Name from TSPL_USER_MASTER order by user_code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv_user.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dt.Columns.Add("Status", GetType(Boolean))

            gv_user.DataSource = dt

            gv_user.Columns("Code").Width = 80
            gv_user.Columns("Code").ReadOnly = True
            gv_user.Columns("Name").Width = 180
            gv_user.Columns("Name").ReadOnly = True
            gv_user.Columns("Status").Width = 30
            gv_user.Columns("Status").ReadOnly = True

            gv_user.EnableFiltering = False

        End If
        RadGroupBox1.Text = "User Detail"
    End Sub

    Private Sub LoadBlankDepartmentGrid()
        Dim qry As String = "select department_code as Code,department_name as Name from tspl_department_master order by department_code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        gv_user.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            dt.Columns.Add("Status", GetType(Boolean))

            gv_user.DataSource = dt

            gv_user.Columns("Code").Width = 80
            gv_user.Columns("Code").ReadOnly = True
            gv_user.Columns("Name").Width = 180
            gv_user.Columns("Name").ReadOnly = True
            gv_user.Columns("Status").Width = 30
            gv_user.Columns("Status").ReadOnly = True
            gv_user.EnableFiltering = False
        End If
        RadGroupBox1.Text = "Department Detail"
    End Sub

    Public Sub savedata()
        Try
            Dim obj As New ClsSectionStageMapping()

            obj.doc_code = clsCommon.myCstr(fndCode.Value)
            obj.doc_date = clsCommon.myCDate(txtdate.Text)
            obj.Section_Code = clsCommon.myCstr(txtsec_code.Value)
            obj.Cate_Code = clsCommon.myCstr(txtcategorycode.Value)

            obj.Arr = New List(Of clsSectionStageMappingDetail)
            obj.Arr_User = New List(Of clsSectionStageMapping_User)

            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objtr As New clsSectionStageMappingDetail()

                objtr.sno = CInt(grow.Cells(colsno).Value)
                objtr.stagecode = clsCommon.myCstr(grow.Cells(colStageCode).Value)
                objtr.departcode = clsCommon.myCstr(grow.Cells(colDepartcode).Value)
                objtr.logsheetno = clsCommon.myCstr(grow.Cells(colLogsheetno).Value)
                objtr.sequnceno = CInt(grow.Cells(colSequnceno).Value)
                objtr.No_of_Department = CInt(clsCommon.myCdbl(grow.Cells(colDepartname).Value))

                If clsCommon.myLen(objtr.stagecode) > 0 Then
                    obj.Arr.Add(objtr)
                End If

                '------------user grid data--------------
                Dim xvalue As String = clsCommon.myCstr(grow.Cells(colUserCode).Value)

                Dim xspilt() As String = Nothing
                xspilt = xvalue.Split(",")

                If clsCommon.myLen(xvalue) > 0 Then
                    For jj As Integer = 0 To xspilt.Length - 1
                        Dim objtr1 As New clsSectionStageMapping_User()

                        objtr1.stagecode = objtr.stagecode
                        objtr1.usercode = xspilt(jj)

                        obj.Arr_User.Add(objtr1)
                    Next
                End If
            Next

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If ClsSectionStageMapping.SaveData(fndCode.Value, obj, isNewEntry, trans) Then
                If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If

                fndCode.Value = obj.doc_code
                UcAttachment1.SaveData(fndCode.Value)
                LoadData(fndCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
   
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As ClsSectionStageMapping = ClsSectionStageMapping.GetData(strCode, NavTyep)

            isInsideLoadData = True
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.doc_code) > 0 Then
                isNewEntry = False

                fndCode.Value = obj.doc_code
                txtdate.Text = obj.doc_date
                txtsec_code.Value = obj.Section_Code
                txtdesc.Text = obj.section_desc
                txtcategorycode.Value = obj.Cate_Code
                txtcategoryname.Text = obj.Cate_desc

                gv1.Rows.Clear()
                gv1.Rows.AddNew()

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsSectionStageMappingDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colsno).Value = objtr.sno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSequnceno).Value = objtr.sequnceno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colStageCode).Value = objtr.stagecode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = objtr.stagedesc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepartcode).Value = objtr.departcode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepartname).Value = objtr.No_of_Department
                        If objtr.No_of_Department <= 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDepartname).Value = "Double Click"
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLogsheetno).Value = objtr.logsheetno
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLogsheetDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_PP_LOG_SHEET_HEAD where stage_code='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colStageCode).Value) + "' and doc_no='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colLogsheetno).Value) + "'"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUserCode).Value = objtr.users
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colNoOfUser).Value = objtr.noofuser
                        gv1.Rows.AddNew()
                    Next
                End If

                UcAttachment1.LoadData(fndCode.Value)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                fndCode.MyReadOnly = True
            Else
                Reset()
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        isInsideLoadData = False
    End Sub

    Public Sub funDelete()
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                fndCode.Focus()
                fndCode.Select()
                Errorcontrol.SetError(fndCode, "Select document code to delete.")
                Throw New Exception("Select document code to delete.")
            Else
                Errorcontrol.ResetError(fndCode)
            End If

            If myMessages.deleteConfirm() Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If ClsSectionStageMapping.DeleteData(fndCode.Value, trans) Then
                    myMessages.delete()
                    Reset()
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub Reset()
        fndCode.Value = ""
        txtdate.Text = clsCommon.GETSERVERDATE(Nothing)
        txtsec_code.Value = ""
        txtdesc.Text = ""
        txtcategorycode.Value = ""
        txtcategoryname.Text = ""
        LoadBlankGrid()
        LoadBlankUserGrid()

        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnSave.Text = "Save"
        fndCode.MyReadOnly = False
        isNewEntry = True

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        RadGroupBox1.Visible = False
        RadGroupBox1.Text = "User Detail"
        fndCode.Focus()
        fndCode.Select()

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub FrmSectionStageMapping_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
                btnSave.Focus()
                btnSave.Select()
                If AllowToSave() Then savedata()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
                btnDelete.Focus()
                btnDelete.Select()
                funDelete()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                btnclose.Focus()
                btnclose.Select()
                Me.Close()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
                btnNew.Focus()
                btnNew.Select()
                Reset()
            ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                                                 "TSPL_SECTION_STAGE_MAPPING_HEAD " + Environment.NewLine + _
                                                                 "TSPL_SECTION_STAGE_MAPPING " + Environment.NewLine + _
                                                                 "TSPL_SECTION_STAGE_USER_DETAIL ")
            End If

            If e.KeyData = Keys.F4 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colLogsheetno) Then
                If clsCommon.myLen(gv1.CurrentRow.Cells(colLogsheetno).Value) > 0 Then
                    '    Dim frm As New FrmProcessProductionLogSheet()
                    '    frm.SetUserMgmt(clsUserMgtCode.frmProcessProductionLogSheet)
                    '    frm.StrLogSheetNo = clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value)
                    '    frm.WindowState = FormWindowState.Maximized
                    '    frm.ShowDialog()
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionLogSheet, clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value))
                End If
            End If

            If e.KeyData = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colStageCode) Then
                isCellValueChanged = True
                OpenStage(True)
                isCellValueChanged = False
            End If

            If e.KeyData = Keys.F2 AndAlso gv1.CurrentColumn IsNot Nothing AndAlso gv1.CurrentColumn Is gv1.Columns(colLogsheetno) Then
                isCellValueChanged = True
                OpenLogsheet(True)
                isCellValueChanged = False
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    
    Private Sub fndCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCode._MYValidating
        Dim qry As String = "select count(*) from TSPL_SECTION_STAGE_MAPPING_HEAD where doc_Code='" + fndCode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            fndCode.MyReadOnly = True
        Else
            fndCode.MyReadOnly = False
        End If

        If fndCode.MyReadOnly OrElse isButtonClicked Then
            fndCode.Value = ClsSectionStageMapping.GetFinder("", fndCode.Value, isButtonClicked)

            If clsCommon.myLen(fndCode.Value) > 0 Then
                LoadData(fndCode.Value, NavigatorType.Current)
            Else
                Reset()
            End If
        Else
            Reset()
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtsec_code.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtsec_code.Focus()
                txtsec_code.Select()
                Errorcontrol.SetError(txtdesc, "Select section detail.")
                Throw New Exception("Select section detail.")
            Else
                Errorcontrol.ResetError(txtdesc)
            End If

            If clsCommon.myLen(txtcategorycode.Value) <= 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                txtcategorycode.Focus()
                txtcategorycode.Select()
                Errorcontrol.SetError(txtcategoryname, "Select production category detail.")
                Throw New Exception("Select production category detail.")
            Else
                Errorcontrol.ResetError(txtcategoryname)
            End If
            ''===================================================
            Dim qry As String = "select count(*) from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + txtsec_code.Value + "' and structure_code='" + txtcategorycode.Value + "' and doc_code<>'" + fndCode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                RadPageView1.SelectedPage = RadPageViewPage1
                Throw New Exception("Mapping record already exist of selected Section and Category.")
            End If
            '==============================================

            FillAutoLogSheet()

            Dim stagecode As String = ""
            Dim logsheet As String = ""
            Dim department As String = ""
            Dim oldstagecode As String = ""
            Dim oldlogsheet As String = ""
            Dim olddepartment As String = ""
            Dim xusercode As String = ""
            Dim seqno As Decimal = 0

            For ii As Integer = 0 To gv1.Rows.Count - 1
                stagecode = clsCommon.myCstr(gv1.Rows(ii).Cells(colStageCode).Value)
                logsheet = clsCommon.myCstr(gv1.Rows(ii).Cells(colLogsheetno).Value)
                department = clsCommon.myCstr(gv1.Rows(ii).Cells(colDepartname).Value)
                xusercode = clsCommon.myCstr(gv1.Rows(ii).Cells(colNoOfUser).Value)
                seqno = clsCommon.myCdbl(gv1.Rows(ii).Cells(colSequnceno).Value)

                If ii = 0 AndAlso clsCommon.myLen(stagecode) <= 0 Then
                    RadPageView1.SelectedPage = RadPageViewPage1
                    gv1.Focus()
                    gv1.Select()
                    Throw New Exception("Fill atleast one row in grid")
                End If


                If clsCommon.myLen(stagecode) > 0 Then
                    If Not SkipLogSheet Then
                        If clsCommon.myLen(logsheet) <= 0 Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv1.Focus()
                            gv1.Select()
                            Throw New Exception("Select log sheet detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                        End If
                    End If
                    

                    'If clsCommon.myLen(department) <= 0 Then
                    '    RadPageView1.SelectedPage = RadPageViewPage1
                    '    gv1.Focus()
                    '    gv1.Select()
                    '    Throw New Exception("Select department detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    'If clsCommon.myLen(xusercode) <= 0 Or clsCommon.CompairString(xusercode, "Double Click") = CompairStringResult.Equal Or clsCommon.CompairString(xusercode, "0") = CompairStringResult.Equal Then
                    '    gv1.Focus()
                    '    gv1.Select()
                    '    Throw New Exception("Select user detail at row no. " + clsCommon.myCstr(ii + 1) + "")
                    'End If

                    For jj As Integer = ii + 1 To gv1.Rows.Count - 1
                        oldstagecode = clsCommon.myCstr(gv1.Rows(jj).Cells(colStageCode).Value)
                        oldlogsheet = clsCommon.myCstr(gv1.Rows(jj).Cells(colLogsheetno).Value)
                        olddepartment = clsCommon.myCstr(gv1.Rows(jj).Cells(colDepartcode).Value)

                        If clsCommon.CompairString(stagecode, oldstagecode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(logsheet, oldlogsheet) = CompairStringResult.Equal Then
                            RadPageView1.SelectedPage = RadPageViewPage1
                            gv1.Focus()
                            gv1.Select()
                            Throw New Exception("Duplicate values at row no. " + clsCommon.myCstr(jj + 1) + "")
                        End If
                    Next
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function

    Private Sub FillAutoLogSheet()
        Try
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(colStageCode).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colLogsheetno).Value) <= 0 Then
                    grow.Cells(colLogsheetno).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 doc_no from TSPL_PP_LOG_SHEET_HEAD where stage_code='" + clsCommon.myCstr(grow.Cells(colStageCode).Value) + "' order by doc_date desc"))
                    grow.Cells(colLogsheetDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PP_LOG_SHEET_HEAD where doc_no='" + clsCommon.myCstr(grow.Cells(colLogsheetno).Value) + "' and stage_code='" + clsCommon.myCstr(grow.Cells(colStageCode).Value) + "'"))
                    grow.Cells(colDepartcode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 department_code from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION' and Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(grow.Cells(colLogsheetno).Value) + "')"))
                    grow.Cells(colDepartname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from (select distinct department_code from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION' and Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(grow.Cells(colLogsheetno).Value) + "'))final"))
                    grow.Cells(colUserCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select distinct ','+Emp_Code from TSPL_QC_LOG_SHEET_USER_MASTER where Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(grow.Cells(colLogsheetno).Value) + "') for xml path('')) as emp_code"))
                    If clsCommon.myLen(grow.Cells(colUserCode).Value) > 0 AndAlso clsCommon.myCstr(grow.Cells(colUserCode).Value).Substring(0, 1) = "," Then
                        grow.Cells(colUserCode).Value = clsCommon.myCstr(grow.Cells(colUserCode).Value).Substring(1, clsCommon.myLen(grow.Cells(colUserCode).Value) - 1)
                    End If
                    grow.Cells(colNoOfUser).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from (select distinct emp_code from TSPL_QC_LOG_SHEET_USER_MASTER where Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(grow.Cells(colLogsheetno).Value) + "'))final"))
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then savedata()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub

    Private Sub FrmSectionStageMapping_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()

        SkipLogSheet = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToSkipStageQLLogSheetInProd, clsFixedParameterCode.AllowToSkipStageQLLogSheetInProd, Nothing)) = "1", True, False))

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    End Sub

    Private Sub txtcategorycode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcategorycode._MYValidating
        Try
            Dim qry As String = "select Structure_Code as Code,Structure_Descq as Description,Item_Structure as Structure,Total_Length as [Length],Default_Struct as [Default Structure] from TSPL_STRUCTURE_MASTER"
            txtcategorycode.Value = clsCommon.ShowSelectForm("CATFND1", qry, "Code", "", txtcategorycode.Value, "Code", isButtonClicked)
            txtcategoryname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code='" + txtcategorycode.Value + "'"))
            
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

#Region "Grid Events"

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Column Is gv1.Columns(colNoOfUser) OrElse e.Column Is gv1.Columns(colDepartname) Then
                RadGroupBox1.Visible = False
                DepartmentSelect = False
                If e.Column Is gv1.Columns(colDepartname) Then
                    DepartmentSelect = True
                End If
                OpenUserGrid()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenUserGrid()
        Try
            Dim xvalue As String = ""
            If DepartmentSelect Then
                LoadBlankDepartmentGrid()
                xvalue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select distinct ','+department_code from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION' and Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "') for xml path('')) as emp_code"))
            Else
                LoadBlankUserGrid()
                xvalue = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select distinct ','+Emp_Code from TSPL_QC_LOG_SHEET_USER_MASTER where Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "') for xml path('')) as emp_code"))
            End If

            Dim xspilt() As String = Nothing
            xspilt = xvalue.Split(",")

            If clsCommon.myLen(xvalue) > 0 Then
                For jj As Integer = 0 To xspilt.Length - 1
                    For ii As Integer = 0 To gv_user.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gv_user.Rows(ii).Cells("Code").Value), xspilt(jj)) = CompairStringResult.Equal Then
                            gv_user.Rows(ii).Cells("Status").Value = True
                        End If
                    Next
                Next
            ElseIf clsCommon.myLen(xvalue) <= 0 Then
                For ii As Integer = 0 To gv_user.Rows.Count - 1
                    gv_user.Rows(ii).Cells("Status").Value = True
                Next
            End If

            RadGroupBox1.Visible = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If Not isCellValueChanged Then
                    If e.Column Is gv1.Columns(colStageCode) Then
                        isCellValueChanged = True
                        OpenStage(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv1.Columns(colLogsheetno) Then
                        isCellValueChanged = True
                        OpenLogsheet(False)
                        isCellValueChanged = False
                    End If

                    If e.Column Is gv1.Columns(colDepartcode) Then
                        'isCellValueChanged = True
                        'OpenDepartment(False)
                        'isCellValueChanged = False
                    End If

                    If e.Column Is gv1.Columns(colSequnceno) Then
                        isCellValueChanged = True
                        For Each grow As GridViewRowInfo In gv1.Rows
                            If grow.Index <> gv1.CurrentRow.Index AndAlso clsCommon.myLen(grow.Cells(colStageCode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colSequnceno).Value), clsCommon.myCstr(gv1.CurrentRow.Cells(colSequnceno).Value)) = CompairStringResult.Equal Then
                                gv1.CurrentRow.Cells(colSequnceno).Value = Nothing
                                Throw New Exception("Same sequence no. not allowed.")
                            End If
                        Next
                        isCellValueChanged = False
                    End If
                End If
            End If
        Catch ex As Exception
            isCellValueChanged = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub OpenStage(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select Stage_Code as Code,Description from TSPL_STAGE_MASTER"
        Dim xvalue As String = clsCommon.ShowSelectForm("STGFND", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colStageCode).Value), "Code", isButtonClicked)

        If clsCommon.myLen(xvalue) > 0 Then
            gv1.CurrentRow.Cells(colStageCode).Value = xvalue
            gv1.CurrentRow.Cells(colDescription).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_STAGE_MASTER where Stage_Code='" + xvalue + "'"))
            gv1.CurrentRow.Cells(colLogsheetno).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 doc_no from TSPL_PP_LOG_SHEET_HEAD where stage_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colStageCode).Value) + "' order by doc_date desc")) 'structure_code='" + txtcategorycode.Value + "' and 
            gv1.CurrentRow.Cells(colLogsheetDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_PP_LOG_SHEET_HEAD where stage_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colStageCode).Value) + "' and doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "'"))
            gv1.CurrentRow.Cells(colSequnceno).Value = Nothing ' CInt(clsDBFuncationality.getSingleValue("select sequence_no from TSPL_PP_LOG_SHEET_HEAD where doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "' and structure_code='" + txtcategorycode.Value + "' and stage_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colStageCode).Value) + "'"))

            gv1.CurrentRow.Cells(colDepartcode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 department_code from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION' and Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "')"))
            gv1.CurrentRow.Cells(colDepartname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from (select distinct department_code from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION' and Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "'))final"))
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colDepartname).Value) <= 0 Then
                gv1.CurrentRow.Cells(colDepartname).Value = "Double Click"
            End If
            gv1.CurrentRow.Cells(colUserCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select distinct ','+Emp_Code from TSPL_QC_LOG_SHEET_USER_MASTER where Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "') for xml path('')) as emp_code"))
            If clsCommon.myLen(gv1.CurrentRow.Cells(colUserCode).Value) > 0 AndAlso clsCommon.myCstr(gv1.CurrentRow.Cells(colUserCode).Value).Substring(0, 1) = "," Then
                gv1.CurrentRow.Cells(colUserCode).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colUserCode).Value).Substring(1, clsCommon.myLen(gv1.CurrentRow.Cells(colUserCode).Value) - 1)
            End If
            gv1.CurrentRow.Cells(colNoOfUser).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from (select distinct emp_code from TSPL_QC_LOG_SHEET_USER_MASTER where Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value) + "'))final"))
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colNoOfUser).Value) <= 0 Then
                gv1.CurrentRow.Cells(colNoOfUser).Value = "Double Click"
            End If
        Else
            gv1.CurrentRow.Cells(colStageCode).Value = ""
            gv1.CurrentRow.Cells(colDescription).Value = ""
            gv1.CurrentRow.Cells(colLogsheetno).Value = ""
            gv1.CurrentRow.Cells(colLogsheetDesc).Value = ""
            gv1.CurrentRow.Cells(colSequnceno).Value = Nothing
            gv1.CurrentRow.Cells(colDepartcode).Value = ""
            gv1.CurrentRow.Cells(colDepartname).Value = "Double Click"
            gv1.CurrentRow.Cells(colUserCode).Value = ""
            gv1.CurrentRow.Cells(colNoOfUser).Value = "Double Click"
        End If
    End Sub

    Private Sub OpenLogsheet(ByVal isButtonClicked As Boolean)
        Dim xvalue As String = clsProcessProductionLogSheet.GetFinder(" TSPL_PP_LOG_SHEET_HEAD.stage_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colStageCode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colLogsheetno).Value), isButtonClicked) 'TSPL_PP_LOG_SHEET_HEAD.structure_code='" + txtcategorycode.Value + "' and 

        If clsCommon.myLen(xvalue) > 0 Then
            gv1.CurrentRow.Cells(colLogsheetno).Value = xvalue
            gv1.CurrentRow.Cells(colLogsheetDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_PP_LOG_SHEET_HEAD where doc_no='" + xvalue + "' and stage_code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colStageCode).Value) + "'"))
            gv1.CurrentRow.Cells(colDepartcode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 department_code from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION' and Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + xvalue + "')"))
            gv1.CurrentRow.Cells(colDepartname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from (select distinct department_code from TSPL_QC_LOG_SHEET_MASTER where trans_id='PRODUCTION' and Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + xvalue + "'))final"))
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colDepartname).Value) <= 0 Then
                gv1.CurrentRow.Cells(colDepartname).Value = "Double Click"
            End If
            gv1.CurrentRow.Cells(colUserCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select (select distinct ','+Emp_Code from TSPL_QC_LOG_SHEET_USER_MASTER where Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + xvalue + "') for xml path('')) as emp_code"))
            If clsCommon.myLen(gv1.CurrentRow.Cells(colUserCode).Value) > 0 AndAlso clsCommon.myCstr(gv1.CurrentRow.Cells(colUserCode).Value).Substring(0, 1) = "," Then
                gv1.CurrentRow.Cells(colUserCode).Value = clsCommon.myCstr(gv1.CurrentRow.Cells(colUserCode).Value).Substring(1, clsCommon.myLen(gv1.CurrentRow.Cells(colUserCode).Value) - 1)
            End If
            gv1.CurrentRow.Cells(colNoOfUser).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select count(*) from (select distinct emp_code from TSPL_QC_LOG_SHEET_USER_MASTER where Code in (select Parameter_Code from TSPL_PP_LOG_SHEET_DETAIL where doc_no='" + xvalue + "'))final"))
            If clsCommon.myCdbl(gv1.CurrentRow.Cells(colNoOfUser).Value) <= 0 Then
                gv1.CurrentRow.Cells(colNoOfUser).Value = "Double Click"
            End If
        Else
            gv1.CurrentRow.Cells(colLogsheetno).Value = ""
            gv1.CurrentRow.Cells(colDepartcode).Value = ""
            gv1.CurrentRow.Cells(colDepartname).Value = "Double Click"
            gv1.CurrentRow.Cells(colUserCode).Value = ""
            gv1.CurrentRow.Cells(colNoOfUser).Value = "Double Click"
            gv1.CurrentRow.Cells(colLogsheetDesc).Value = Nothing
        End If
    End Sub

    Private Sub OpenDepartment(ByVal isButtonClicked As Boolean)
        Dim xvalue As String = clsDepartmentMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colDepartcode).Value), isButtonClicked)

        If clsCommon.myLen(xvalue) > 0 Then
            gv1.CurrentRow.Cells(colDepartcode).Value = xvalue
            gv1.CurrentRow.Cells(colDepartname).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select department_name from TSPL_DEPARTMENT_MASTER where department_code='" + xvalue + "'"))
        Else
            gv1.CurrentRow.Cells(colDepartcode).Value = ""
            gv1.CurrentRow.Cells(colDepartname).Value = ""
        End If
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colsno).Value = CInt(intCurrRow + 1)

            If clsCommon.myLen(gv1.CurrentRow.Cells(colUserCode).Value) <= 0 Then
                gv1.CurrentRow.Cells(colNoOfUser).Value = "Double Click"
                gv1.CurrentRow.Cells(colUserCode).Value = ""
            End If
            If clsCommon.myLen(gv1.CurrentRow.Cells(colDepartcode).Value) <= 0 Then
                gv1.CurrentRow.Cells(colDepartcode).Value = ""
                gv1.CurrentRow.Cells(colDepartname).Value = "Double Click"
            End If
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
#End Region

    Private Sub btngridclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngridclose.Click
        Try
            Dim count As Integer = 0
            Dim xusercodes As String = ""

            If Not DepartmentSelect Then
                gv1.CurrentRow.Cells(colUserCode).Value = ""
                gv1.CurrentRow.Cells(colNoOfUser).Value = ""

                For ii As Integer = 0 To gv_user.Rows.Count - 1
                    If gv_user.Rows(ii).Cells("Status").Value Is DBNull.Value Then
                        gv_user.Rows(ii).Cells("Status").Value = False
                    End If
                    If clsCommon.CompairString(gv_user.Rows(ii).Cells("Status").Value, True) = CompairStringResult.Equal Then
                        xusercodes = xusercodes + "," + clsCommon.myCstr(gv_user.Rows(ii).Cells("Code").Value)
                        count += 1
                    End If
                Next

                If clsCommon.myLen(xusercodes) > 0 Then
                    If xusercodes.Substring(0, 1) = "," Then
                        xusercodes = xusercodes.Substring(1, xusercodes.Length - 1)
                    End If

                    gv1.CurrentRow.Cells(colUserCode).Value = xusercodes
                    gv1.CurrentRow.Cells(colNoOfUser).Value = clsCommon.myCstr(count)
                Else
                    gv1.CurrentRow.Cells(colNoOfUser).Value = "Double Click"
                End If
            ElseIf DepartmentSelect Then
                gv1.CurrentRow.Cells(colDepartcode).Value = ""
                gv1.CurrentRow.Cells(colDepartname).Value = ""

                For ii As Integer = 0 To gv_user.Rows.Count - 1
                    If gv_user.Rows(ii).Cells("Status").Value Is DBNull.Value Then
                        gv_user.Rows(ii).Cells("Status").Value = False
                    End If
                    If clsCommon.CompairString(gv_user.Rows(ii).Cells("Status").Value, True) = CompairStringResult.Equal Then
                        xusercodes = clsCommon.myCstr(gv_user.Rows(ii).Cells("Code").Value)
                        count += 1
                    End If
                Next

                If clsCommon.myLen(xusercodes) > 0 Then
                    gv1.CurrentRow.Cells(colDepartcode).Value = xusercodes
                    gv1.CurrentRow.Cells(colDepartname).Value = clsCommon.myCstr(count)
                Else
                    gv1.CurrentRow.Cells(colDepartname).Value = "Double Click"
                End If
            End If

            RadGroupBox1.Visible = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtsec_code__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtsec_code._MYValidating
        Dim qry As String = "select section_code as Code,Description,Created_by as [Created By],created_date as [Created Date],modified_by as [Modifeid By],modified_date as [Modified date] from tspl_section_master"
        txtsec_code.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("SCTFND", qry, "Code", "", txtsec_code.Value, "Code", isButtonClicked))

        If clsCommon.myLen(txtsec_code.Value) > 0 Then
            txtdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_section_master where section_code='" + txtsec_code.Value + "'"))
        Else
            txtsec_code.Value = ""
            txtdesc.Text = ""
        End If
    End Sub

    Private Sub btnexportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexportExcel.Click
        Dim qry As String = "select count(*) from TSPL_SECTION_STAGE_MAPPING_HEAD"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select TSPL_SECTION_STAGE_MAPPING_HEAD.Doc_Code,TSPL_SECTION_STAGE_MAPPING_HEAD.Doc_Date,TSPL_SECTION_STAGE_MAPPING_HEAD.Section_Code,TSPL_SECTION_STAGE_MAPPING_HEAD.Structure_Code,TSPL_SECTION_STAGE_MAPPING.Stage_Code,TSPL_SECTION_STAGE_MAPPING.Log_Sheet_No,TSPL_SECTION_STAGE_MAPPING.Sequence_No from TSPL_SECTION_STAGE_MAPPING_HEAD left outer join TSPL_SECTION_STAGE_MAPPING on TSPL_SECTION_STAGE_MAPPING.Doc_Code=TSPL_SECTION_STAGE_MAPPING_HEAD.Doc_Code"
        Else
            qry = "select '' as Doc_Code,'' as Doc_Date,'' as Section_Code,'' as Structure_Code,'' as Stage_Code,'' as Log_Sheet_No,1 as Sequence_No"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimportExcel.Click
        'qry = "select '' as Doc_Code,'' as Doc_Date,'' as Section_Code,'' as Structure_Code,1 as SNO,'' as Stage_Code,'' as Department_Code,'' as Log_Sheet_No,1 as Sequence_No"
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim currentdate As Date = Date.Today
        Dim oldNewentry As Boolean = isNewEntry

        If transportSql.importExcel(gv_Import, "Doc_Code", "Doc_Date", "Section_Code", "Structure_Code", "Stage_Code", "Log_Sheet_No", "Sequence_No") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                clsCommon.ProgressBarShow()
                Dim doc_code As String = ""
                Dim doc_date As Date = Nothing
                Dim section_code As String = ""
                Dim Structure_Code As String = ""
                Dim SNO As Integer = 0
                Dim stage_code As String = ""
                Dim Department_Code As String = ""
                Dim Log_Sheet_No As String = ""
                Dim Sequence_No As Integer = 0
                Dim qry As String = ""
                Dim check As Integer = 0

                For Each grow As GridViewRowInfo In gv_Import.Rows
                    doc_code = clsCommon.myCstr(grow.Cells("Doc_Code").Value)

                    If grow.Cells("Doc_date").Value Is Nothing OrElse grow.Cells("Doc_date").Value Is DBNull.Value Then
                        doc_date = clsCommon.GETSERVERDATE(trans)
                    Else
                        doc_date = clsCommon.myCDate(grow.Cells("Doc_date").Value)
                    End If

                    section_code = clsCommon.myCstr(grow.Cells("Section_Code").Value)
                    If clsCommon.myLen(section_code) <= 0 Then
                        Throw New Exception("Fill section_code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        qry = "select count(*) from TSPL_SECTION_MASTER where section_code='" + section_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled section_code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    Structure_Code = clsCommon.myCstr(grow.Cells("Structure_Code").Value)
                    If clsCommon.myLen(Structure_Code) <= 0 Then
                        Throw New Exception("Fill Structure_Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        qry = "select count(*) from TSPL_STRUCTURE_MASTER where structure_code='" + Structure_Code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Structure_Code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    ' clsCommon.myCdbl(grow.Cells("SNO").Value)

                    stage_code = clsCommon.myCstr(grow.Cells("Stage_Code").Value)
                    If clsCommon.myLen(stage_code) <= 0 Then
                        Throw New Exception("Fill stage_code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    Else
                        qry = "select count(*) from TSPL_STAGE_MASTER where stage_code='" + stage_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled stage_code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    Department_Code = "" 'clsCommon.myCstr(grow.Cells("Department_Code").Value)
                    'If clsCommon.myLen(Department_Code) <= 0 Then
                    '    Throw New Exception("Fill Department_Code at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    'Else
                    '    qry = "select count(*) from TSPL_DEPARTMENT_MASTER where department_code='" + Department_Code + "'"
                    '    check = clsDBFuncationality.getSingleValue(qry, trans)

                    '    If check <= 0 Then
                    '        Throw New Exception("Filled Department_Code does not exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                    '    End If
                    'End If

                    Log_Sheet_No = clsCommon.myCstr(grow.Cells("Log_Sheet_No").Value)
                    If clsCommon.myLen(Log_Sheet_No) <= 0 Then
                        Log_Sheet_No = clsDBFuncationality.getSingleValue("select doc_no from TSPL_PP_LOG_SHEET_HEAD where stage_code='" + stage_code + "'", trans) 'structure_code='" + Structure_Code + "' and 

                        If Not SkipLogSheet AndAlso clsCommon.myLen(Log_Sheet_No) <= 0 Then
                            Throw New Exception("Fill Log_Sheet_No at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    Else
                        qry = "select count(*) from TSPL_PP_LOG_SHEET_HEAD where doc_no='" + Log_Sheet_No + "' and stage_code='" + stage_code + "'" ' and structure_code='" + Structure_Code + "'
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            Throw New Exception("Filled Log_Sheet_No does not mapped with Stage exist see at line no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        End If
                    End If

                    Sequence_No = CInt(clsCommon.myCdbl(grow.Cells("Sequence_No").Value))

                    '============Head================================
                    If clsCommon.myLen(doc_code) > 0 Then
                        qry = "select count(*) from TSPL_SECTION_STAGE_MAPPING_HEAD where doc_code='" + doc_code + "'"
                        check = clsDBFuncationality.getSingleValue(qry, trans)

                        If check <= 0 Then
                            doc_code = ""
                            isNewEntry = True
                        End If
                    End If

                    If clsCommon.myLen(doc_code) <= 0 Then
                        qry = "select doc_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + section_code + "' and structure_code='" + Structure_Code + "'"
                        doc_code = clsDBFuncationality.getSingleValue(qry, trans)
                        isNewEntry = True
                        If clsCommon.myLen(doc_code) > 0 Then
                            isNewEntry = False
                        End If
                    ElseIf clsCommon.myLen(doc_code) > 0 Then
                        isNewEntry = False
                    End If

                    If isNewEntry Then
                        qry = "select max(doc_code) from TSPL_SECTION_STAGE_MAPPING_HEAD"
                        doc_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))

                        If clsCommon.myLen(doc_code) > 0 Then
                            doc_code = clsCommon.incval(doc_code)
                        Else
                            doc_code = "SCM000001"
                        End If
                    End If

                    '===========check from data===============================
                    qry = "select doc_code from TSPL_SECTION_STAGE_MAPPING_HEAD where section_code='" + section_code + "' and structure_code='" + Structure_Code + "'"
                    Dim newcode As String = clsDBFuncationality.getSingleValue(qry, trans)

                    If clsCommon.myLen(newcode) > 0 Then
                        isNewEntry = False
                        doc_code = newcode
                    End If
                    '==================================================

                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "doc_code", doc_code)
                    clsCommon.AddColumnsForChange(coll, "doc_date", clsCommon.GetPrintDate(doc_date, "dd/MMM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Section_Code", section_code)
                    clsCommon.AddColumnsForChange(coll, "Structure_Code", Structure_Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.myCstr(clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")))

                    If isNewEntry Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_STAGE_MAPPING_HEAD", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_STAGE_MAPPING_HEAD", OMInsertOrUpdate.Update, " TSPL_SECTION_STAGE_MAPPING_HEAD.doc_code='" + doc_code + "'", trans)
                    End If

                    '=Detail======
                    qry = "delete from TSPL_SECTION_STAGE_MAPPING where Doc_CODE ='" + doc_code + "' and section_code='" + section_code + "' and stage_code='" + stage_code + "' and log_sheet_no='" + Log_Sheet_No + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    SNO = CInt(1 + clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select max(sno) from TSPL_SECTION_STAGE_MAPPING where Doc_CODE ='" + doc_code + "' and section_code='" + section_code + "'")))

                    coll = New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "doc_code", doc_code)
                    clsCommon.AddColumnsForChange(coll, "Section_Code", section_code)
                    clsCommon.AddColumnsForChange(coll, "SNO", SNO)
                    clsCommon.AddColumnsForChange(coll, "Stage_Code", stage_code)
                    clsCommon.AddColumnsForChange(coll, "Department_Code", Department_Code)
                    clsCommon.AddColumnsForChange(coll, "Log_Sheet_No", Log_Sheet_No)
                    clsCommon.AddColumnsForChange(coll, "Sequence_No", Sequence_No)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SECTION_STAGE_MAPPING", OMInsertOrUpdate.Insert, "", trans)

                Next

                clsCommon.ProgressBarHide()
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data transfer successfully", Me.Text)

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If

        isNewEntry = oldNewentry
        Me.Controls.Remove(gv_Import)
    End Sub
End Class
