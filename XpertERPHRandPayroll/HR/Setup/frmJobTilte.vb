' ----------------- Created By Anubhooti On 05-Aug-2014 Against -------------------- ''
'--preeti gupta ticket no[BM00000003469,BM00000003736]
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

Public Class FrmJobTilte
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Const colInterviewChk As String = "colInterviewChk"
    Const colInterviewCode As String = "colInterviewCode"
    Const colInterviewChkListCode As String = "colInterviewChkListCode"
    Const colInterviewChkListDescription As String = "colInterviewChkListDescription"
    Const colOfferviewChk As String = "colOfferviewChk"
    Const colOfferCode As String = "colOfferCode"
    Const colofferChkListCode As String = "colofferChkListCode"
    Const colofferChkListDescription As String = "colofferChkListDescription"
    Const colInterviewMandatory As String = "colInterviewMandatory"
    Const colofferMandatory As String = "colofferMandatory"
    Const coljoiningCode As String = "coljoiningCode"
    Const coljoiningChkListCode As String = "coljoiningChkListCode"
    Const coljoiningChkListDescription As String = "coljoiningChkListDescription"
    Const coljoiningMandatory As String = "coljoiningMandatory"
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.JobTitle)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtcode.Value = Nothing

        'LoadBlankInterviewChkList()
        
        Dim obj As ClsJobTitle = ClsJobTitle.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            LoadBlankOfferChkList()
            LoadBlankJoiningChkList()
            isNewEntry = False
            txtcode.Value = obj.Job_Title_Code
            txtdesp.Text = obj.Job_Title
            txtcode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True

            'If Not IsNothing(obj.arrInterview) Then
            '    For Each objtr As ClsChkInterviewJoblist In obj.arrInterview
            '        gvChkInterview.Rows.AddNew()
            '        gvChkInterview.Rows(gvChkInterview.RowCount - 1).Cells(colInterviewChkListCode).Value = objtr.ChkListCode
            '        gvChkInterview.Rows(gvChkInterview.RowCount - 1).Cells(colInterviewCode).Value = objtr.Code
            '        gvChkInterview.Rows(gvChkInterview.RowCount - 1).Cells(colInterviewChk).Value = objtr.Mandatory
            '        gvChkInterview.Rows(gvChkInterview.RowCount - 1).Cells(colInterviewChkListDescription).Value = objtr.Description
            '    Next
            'End If
            'gvChkInterview.Rows.AddNew()


            If Not IsNothing(obj.arrOffer) Then

                For Each objt As ClsChkOfferJoblist In obj.arrOffer
                    gvOffer.Rows.AddNew()
                    gvOffer.Rows(gvOffer.RowCount - 1).Cells(colofferChkListCode).Value = objt.ChkListCode
                    gvOffer.Rows(gvOffer.RowCount - 1).Cells(colOfferCode).Value = objt.Code
                    gvOffer.Rows(gvOffer.RowCount - 1).Cells(colOfferviewChk).Value = objt.Mandatory
                    gvOffer.Rows(gvOffer.RowCount - 1).Cells(colofferChkListDescription).Value = objt.Description

                Next
            End If
            gvOffer.Rows.AddNew()
            If Not IsNothing(obj.arrJoin) Then


                For Each objte As ClsChkJoiningJoblist In obj.arrJoin
                    gvJoining.Rows.AddNew()
                    gvJoining.Rows(gvJoining.RowCount - 1).Cells(coljoiningChkListCode).Value = objte.ChkListCode
                    gvJoining.Rows(gvJoining.RowCount - 1).Cells(coljoiningCode).Value = objte.Code
                    gvJoining.Rows(gvJoining.RowCount - 1).Cells(coljoiningMandatory).Value = objte.Mandatory
                    gvJoining.Rows(gvJoining.RowCount - 1).Cells(coljoiningChkListDescription).Value = objte.Description

                Next
            End If
            gvJoining.Rows.AddNew()
        End If
    End Sub

    Sub SaveData()

        Try
            If AllowToSave() Then

                Dim obj As New ClsJobTitle()
                obj.Job_Title_Code = txtcode.Value
                obj.Job_Title = txtdesp.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Job_Title_Code) from TSPL_HR_JOB_TITLE where Job_Title_Code='" + obj.Job_Title_Code + "'")
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                'Dim arr As New List(Of ClsChkInterviewJoblist)
                'For i As Integer = 0 To gvChkInterview.Rows.Count - 1

                '    If clsCommon.myLen(gvChkInterview.Rows(i).Cells(colInterviewChkListCode).Value) > 0 Then

                '        Dim objtr As New ClsChkInterviewJoblist
                '        objtr.Code = clsCommon.myCstr(gvChkInterview.Rows(i).Cells(colInterviewCode).Value)
                '        objtr.ChkListCode = clsCommon.myCstr(gvChkInterview.Rows(i).Cells(colInterviewChkListCode).Value)
                '        objtr.Mandatory = clsCommon.myCdbl(gvChkInterview.Rows(i).Cells(colInterviewChk).Value)
                '        arr.Add(objtr)
                '    End If


                'Next
                Dim ara As New List(Of ClsChkOfferJoblist)
                For i As Integer = 0 To gvOffer.Rows.Count - 1
                    If clsCommon.myLen(gvOffer.Rows(i).Cells(colofferChkListCode).Value) > 0 Then
                        Dim ObjOffer As New ClsChkOfferJoblist
                        ObjOffer.Code = clsCommon.myCstr(gvOffer.Rows(i).Cells(colOfferCode).Value)
                        ObjOffer.ChkListCode = clsCommon.myCstr(gvOffer.Rows(i).Cells(colofferChkListCode).Value)
                        ObjOffer.Mandatory = clsCommon.myCdbl(gvOffer.Rows(i).Cells(colOfferviewChk).Value)
                        ara.Add(ObjOffer)
                    End If
                Next
                Dim arraa As New List(Of ClsChkJoiningJoblist)
                For i As Integer = 0 To gvJoining.Rows.Count - 1
                    If clsCommon.myLen(gvJoining.Rows(i).Cells(coljoiningChkListCode).Value) > 0 Then
                        Dim objJoin As New ClsChkJoiningJoblist
                        objJoin.Code = clsCommon.myCstr(gvJoining.Rows(i).Cells(coljoiningCode).Value)
                        objJoin.ChkListCode = clsCommon.myCstr(gvJoining.Rows(i).Cells(coljoiningChkListCode).Value)
                        objJoin.Mandatory = clsCommon.myCdbl(gvJoining.Rows(i).Cells(coljoiningMandatory).Value)
                        arraa.Add(objJoin)
                    End If


                Next
                If (ClsJobTitle.SaveData(obj, isNewEntry, ara, arraa)) Then
                    'trans.Commit()
                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    LoadData(obj.Job_Title_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            'trans.Rollback()
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
           
            'Dim isOneChecked As Boolean = False
            'For Each grow As GridViewRowInfo In gvChkInterview.Rows
            '    If grow.Cells("").Value = True Then
            '        isOneChecked = True
            '    End If
            'Next
            'If isOneChecked = False Then
            '    clsCommon.MyMessageBoxShow("Please Select AtLeast one Interview")
            '    Exit Function
            'End If
            If clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) > 30 Then
                myMessages.blankValue("Code")

                txtcode.Focus()
                txtcode.Select()
                Errorcontrol.SetError(txtcode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtcode)
            End If
            If clsCommon.myLen(clsCommon.myCstr(txtdesp.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtdesp.Text)) > 150 Then
                myMessages.blankValue("Description")

                txtdesp.Focus()
                txtdesp.Select()
                Errorcontrol.SetError(txtdesp, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtdesp)
            End If

            For i As Integer = 0 To gvOffer.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gvOffer.Rows(i).Cells(colofferChkListCode).Value)) > 0 Then
                    Dim Offer As String = clsCommon.myCstr(gvOffer.Rows(i).Cells(colofferChkListCode).Value)
                    For j As Integer = i + 1 To gvOffer.Rows.Count - 1
                        Dim SecondRound As String = clsCommon.myCstr(gvOffer.Rows(j).Cells(colofferChkListCode).Value)
                        If clsCommon.CompairString(Offer, SecondRound) = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow("Please check ! duplicate value for '" + gvOffer.Rows(i).Cells(colofferChkListCode).Value + "' in Offer Check list")
                            Return False
                        End If
                    Next
                End If
            Next

            For i As Integer = 0 To gvJoining.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gvJoining.Rows(i).Cells(coljoiningChkListCode).Value)) > 0 Then
                    Dim Joining As String = clsCommon.myCstr(gvJoining.Rows(i).Cells(coljoiningChkListCode).Value)
                    For j As Integer = i + 1 To gvJoining.Rows.Count - 1
                        Dim SecondRound As String = clsCommon.myCstr(gvJoining.Rows(j).Cells(coljoiningChkListCode).Value)
                        If clsCommon.CompairString(Joining, SecondRound) = CompairStringResult.Equal Then
                            clsCommon.MyMessageBoxShow("Please check ! duplicate value for '" + gvJoining.Rows(i).Cells(coljoiningChkListCode).Value + "' in Joining Check List")
                            Return False
                        End If
                    Next
                End If
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub DeleteData()
        
        Try
            Dim obj As New ClsChkOfferJoblist
            ClsChkOfferJoblist.DeleteData(txtcode.Value)
            If clsCommon.MyMessageBoxShow("are you sure? do you want to delete this Code ('" + txtcode.Value + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
            End If
            'RadMessageBox.Show("delete successfully")
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
        End Try
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtdesp.Text = ""
        txtcode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        'LoadBlankInterviewChkList()
        LoadBlankJoiningChkList()
        LoadBlankOfferChkList()
        'gvChkInterview.Rows.AddNew()
        gvOffer.Rows.AddNew()
        gvJoining.Rows.AddNew()
    End Sub
#End Region
#Region "Events"
   
    Private Sub FrmJobTilte_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub
    'Public Sub LoadBlankInterviewChkList()

    '    gvChkInterview.Rows.Clear()
    '    gvChkInterview.Columns.Clear()

    '    Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoCode.FormatString = ""
    '    repoCode.HeaderText = " Code"

    '    repoCode.Name = colInterviewCode
    '    repoCode.Width = 100
    '    repoCode.IsVisible = False
    '    repoCode.ReadOnly = True
    '    gvChkInterview.MasterTemplate.Columns.Add(repoCode)

    '    Dim repoChkListCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repoChkListCode.FormatString = ""
    '    repoChkListCode.HeaderText = "Interview Code"
    '    repoChkListCode.Name = colInterviewChkListCode
    '    repoChkListCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
    '    repoChkListCode.TextImageRelation = TextImageRelation.TextBeforeImage
    '    repoChkListCode.Width = 100
    '    repoChkListCode.IsVisible = True
    '    repoChkListCode.ReadOnly = False
    '    gvChkInterview.MasterTemplate.Columns.Add(repoChkListCode)



    '    Dim repochkDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '    repochkDescription.FormatString = ""
    '    repochkDescription.HeaderText = " Interview Description"
    '    repochkDescription.Name = colInterviewChkListDescription

    '    repochkDescription.Width = 200
    '    repochkDescription.IsVisible = True
    '    repochkDescription.ReadOnly = True
    '    gvChkInterview.MasterTemplate.Columns.Add(repochkDescription)

    '    Dim repoChk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
    '    repoChk.FormatString = ""
    '    repoChk.HeaderText = "Mandatory"
    '    repoChk.Name = colInterviewChk
    '    repoChk.Width = 100
    '    repoChk.IsVisible = True
    '    repoChk.ReadOnly = False
    '    gvChkInterview.MasterTemplate.Columns.Add(repoChk)


    '    gvChkInterview.AllowDeleteRow = True

    '    gvChkInterview.ShowGroupPanel = False
    '    gvChkInterview.AllowColumnReorder = True
    '    gvChkInterview.AllowRowReorder = False
    '    gvChkInterview.EnableSorting = True
    '    gvChkInterview.EnableFiltering = True
    '    gvChkInterview.EnableAlternatingRowColor = True
    '    gvChkInterview.AutoSizeRows = False
    '    gvChkInterview.AllowRowResize = True
    '    gvChkInterview.VerticalScrollState = ScrollState.AlwaysShow
    '    gvChkInterview.HorizontalScrollState = ScrollState.AlwaysShow
    '    gvChkInterview.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '    gvChkInterview.MasterTemplate.ShowRowHeaderColumn = False
    '    gvChkInterview.TableElement.TableHeaderHeight = 40
    '    gvChkInterview.ShowFilteringRow = True

    'End Sub

    Public Sub LoadBlankOfferChkList()
        gvOffer.Rows.Clear()
        gvOffer.Columns.Clear()
        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = " Code"
        repoCode.Name = colOfferCode
        repoCode.Width = 100
        repoCode.IsVisible = False
        repoCode.ReadOnly = True
        gvOffer.MasterTemplate.Columns.Add(repoCode)
        'Ticket No :  TEC/19/03/19-000455  For Change Column name 
        Dim repoChkListCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChkListCode.FormatString = ""
        repoChkListCode.HeaderText = "Offer Check List"
        repoChkListCode.Name = colofferChkListCode
        repoChkListCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoChkListCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoChkListCode.Width = 130
        repoChkListCode.IsVisible = True
        repoChkListCode.ReadOnly = False
        gvOffer.MasterTemplate.Columns.Add(repoChkListCode)



        Dim repochkDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repochkDescription.FormatString = ""
        repochkDescription.HeaderText = "Offer Description"
        repochkDescription.Name = colofferChkListDescription
        repochkDescription.Width = 227
        repochkDescription.IsVisible = True
        repochkDescription.ReadOnly = True
        gvOffer.MasterTemplate.Columns.Add(repochkDescription)

        Dim repoChk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoChk.FormatString = ""
        repoChk.HeaderText = "Mandatory"
        repoChk.Name = colOfferviewChk
        repoChk.Width = 100
        repoChk.IsVisible = True
        repoChk.ReadOnly = False
        gvOffer.MasterTemplate.Columns.Add(repoChk)



        gvOffer.AllowDeleteRow = True

        gvOffer.ShowGroupPanel = False
        gvOffer.AllowColumnReorder = True
        gvOffer.AllowRowReorder = False
        gvOffer.EnableSorting = True
        gvOffer.EnableFiltering = True
        gvOffer.EnableAlternatingRowColor = True
        gvOffer.AutoSizeRows = False
        gvOffer.AllowRowResize = True
        gvOffer.VerticalScrollState = ScrollState.AlwaysShow
        gvOffer.HorizontalScrollState = ScrollState.AlwaysShow
        gvOffer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvOffer.MasterTemplate.ShowRowHeaderColumn = False
        gvOffer.TableElement.TableHeaderHeight = 40
        gvOffer.ShowFilteringRow = True

    End Sub
    Public Sub LoadBlankJoiningChkList()
        gvJoining.Rows.Clear()
        gvJoining.Columns.Clear()
        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = " Code"
        repoCode.Name = coljoiningCode
        repoCode.Width = 100
        repoCode.IsVisible = False
        repoCode.ReadOnly = True
        gvJoining.MasterTemplate.Columns.Add(repoCode)
        'Ticket No :  TEC/19/03/19-000455  For Change Column name 
        Dim repoChkListCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoChkListCode.FormatString = ""
        repoChkListCode.HeaderText = " Joining Check List"
        repoChkListCode.Name = coljoiningChkListCode
        repoChkListCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoChkListCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoChkListCode.Width = 130
        repoChkListCode.IsVisible = True
        repoChkListCode.ReadOnly = False
        gvJoining.MasterTemplate.Columns.Add(repoChkListCode)



        Dim repochkDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repochkDescription.FormatString = ""
        repochkDescription.HeaderText = "Joining Description"
        repochkDescription.Name = coljoiningChkListDescription
        repochkDescription.Width = 227
        repochkDescription.IsVisible = True
        repochkDescription.ReadOnly = True
        gvJoining.MasterTemplate.Columns.Add(repochkDescription)

        Dim repoChk As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoChk.FormatString = ""
        repoChk.HeaderText = "Mandatory"
        repoChk.Name = coljoiningMandatory
        repoChk.Width = 100
        repoChk.IsVisible = True
        repoChk.ReadOnly = False
        gvJoining.MasterTemplate.Columns.Add(repoChk)



        gvJoining.AllowDeleteRow = True

        gvJoining.ShowGroupPanel = False
        gvJoining.AllowColumnReorder = True
        gvJoining.AllowRowReorder = False
        gvJoining.EnableSorting = True
        gvJoining.EnableFiltering = True
        gvJoining.EnableAlternatingRowColor = True
        gvJoining.AutoSizeRows = False
        gvJoining.AllowRowResize = True
        gvJoining.VerticalScrollState = ScrollState.AlwaysShow
        gvJoining.HorizontalScrollState = ScrollState.AlwaysShow
        gvJoining.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvJoining.MasterTemplate.ShowRowHeaderColumn = False
        gvJoining.TableElement.TableHeaderHeight = 40
        gvJoining.ShowFilteringRow = True

    End Sub



    Private Sub FrmJobTilte_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ''LoadBlankInterviewChkList()
        LoadBlankOfferChkList()
        LoadBlankJoiningChkList()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        'gvChkInterview.AllowAddNewRow = False
        gvOffer.AllowAddNewRow = False
        gvJoining.AllowAddNewRow = False
        AddNew()
        ''gvChkInterview.Rows.AddNew()
        ''gvOffer.Rows.AddNew()
        ''gvJoining.Rows.AddNew()
        
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        'If txtcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "select Job_Title_Code As Code,Description from  TSPL_HR_JOB_TITLE"
        '    txtcode.Value = clsCommon.ShowSelectForm("TSPL_HR_JOB_TITLE", qry, "Code", "", txtcode.Value, "", isButtonClicked)
        '    LoadData(txtcode.Value, NavigatorType.Current)
        'End If

        Dim str As String = "select count(*) from TSPL_HR_JOB_TITLE where JOB_TITLE_CODE ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select JOB_TITLE_CODE As [Code],Job_Title As [Job Title] from TSPL_HR_JOB_TITLE"
            txtcode.Value = clsCommon.ShowSelectForm("JOBT", qry, "Code", "", txtcode.Value, "TSPL_HR_JOB_TITLE.JOB_TITLE_CODE", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsJobTitle
                objOT = ClsJobTitle.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                AddNew()
            End If
        End If
    End Sub
#End Region

   

    Private Sub gvOffer_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvOffer.CellValueChanged

        If gvOffer.CurrentColumn Is gvOffer.Columns(colofferChkListCode) Then

            If clsCommon.myLen(clsCommon.myCstr(gvOffer.CurrentRow.Cells(colofferChkListCode).Value)) <= 0 Then
                gvOffer.CurrentRow.Cells(colofferChkListCode).Value = Nothing
                gvOffer.CurrentRow.Cells(colofferChkListDescription).Value = Nothing
                Return
            End If
            Dim count As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_HR_Check_List where Chk_Code='" + clsCommon.myCstr(gvOffer.CurrentRow.Cells(colofferChkListCode).Value) + "'"))
            If (count = 0) Then
                Dim qry As String = "select Chk_Code As Code,Chk_Description AS Description from TSPL_HR_Check_List "
                gvOffer.CurrentRow.Cells(colofferChkListCode).Value = clsCommon.ShowSelectForm("JTChk", qry, "Code", "", gvOffer.CurrentRow.Cells(colofferChkListCode).Value, "", False)
            End If
            If clsCommon.myLen(gvOffer.CurrentRow.Cells(colofferChkListCode).Value) > 0 Then
                gvOffer.CurrentRow.Cells(colofferChkListDescription).Value = clsDBFuncationality.getSingleValue("select Chk_Description   from TSPL_HR_Check_List where Chk_Code='" + gvOffer.CurrentRow.Cells(colofferChkListCode).Value + "' ")
            Else
                gvOffer.CurrentRow.Cells(colofferChkListCode).Value = Nothing
                gvOffer.CurrentRow.Cells(colofferChkListDescription).Value = Nothing
            End If
        End If
        'gvOffer.Rows.AddNew()
    End Sub

    'Private Sub gvChkInterview_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
    '    If gvChkInterview.CurrentRow.Index >= 0 AndAlso e.Column Is gvChkInterview.Columns(colInterviewChkListCode) Then ' And CInt(gvChkInterview.CurrentRow.Index) > 0
    '        Dim count As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_HR_Check_List where Chk_Code='" + gvChkInterview.CurrentRow.Cells(colInterviewChkListCode).Value + "'")
    '        If (count = 0) Then
    '            Dim qry As String = "select Chk_Code ,Chk_Description  from TSPL_HR_Check_List "
    '            gvChkInterview.CurrentRow.Cells(colInterviewChkListCode).Value = clsCommon.ShowSelectForm("HR_Check_List", qry, "Chk_Code", "", gvChkInterview.CurrentRow.Cells(colInterviewChkListCode).Value, "", False)
    '            gvChkInterview.Rows.AddNew()
    '        End If
    '        gvChkInterview.CurrentRow.Cells(colInterviewChkListDescription).Value = clsDBFuncationality.getSingleValue("select Chk_Description   from TSPL_HR_Check_List where Chk_Code='" + gvChkInterview.CurrentRow.Cells(colInterviewChkListCode).Value + "' ")

    '    End If
    '    'gvChkInterview.Rows.AddNew()
    'End Sub


    Private Sub gvJoining_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvJoining.CellValueChanged
        If gvJoining.CurrentColumn Is gvJoining.Columns(coljoiningChkListCode) Then

            If clsCommon.myLen(clsCommon.myCstr(gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value)) <= 0 Then
                gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value = Nothing
                gvJoining.CurrentRow.Cells(coljoiningChkListDescription).Value = Nothing
                Return
            End If
            Dim count As Integer = CInt(clsDBFuncationality.getSingleValue("select count(*) from TSPL_HR_Check_List where Chk_Code='" + clsCommon.myCstr(gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value) + "'"))
            If (count = 0) Then
                Dim qry As String = "select Chk_Code AS Code,Chk_Description As Description from TSPL_HR_Check_List "
                gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value = clsCommon.ShowSelectForm("JTChkJoin", qry, "Code", "", gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value, "", False)
            End If
            If clsCommon.myLen(gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value) > 0 Then
                gvJoining.CurrentRow.Cells(coljoiningChkListDescription).Value = clsDBFuncationality.getSingleValue("select Chk_Description   from TSPL_HR_Check_List where Chk_Code='" + gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value + "' ")
            Else
                gvJoining.CurrentRow.Cells(coljoiningChkListCode).Value = Nothing
                gvJoining.CurrentRow.Cells(coljoiningChkListDescription).Value = Nothing
            End If
        End If
        'gvJoining.Rows.AddNew()
    End Sub
    Private Sub gvJoining_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvJoining.CurrentColumnChanged
        If gvJoining.RowCount > 0 Then
            Dim intCurrRow As Integer = gvJoining.CurrentRow.Index
            '            gvOffer.CurrentRow.Cells(colOfferCode).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvJoining.Rows.Count - 1 And clsCommon.myLen(gvJoining.CurrentRow.Cells(coljoiningChkListDescription).Value) > 0 Then
                gvJoining.Rows.AddNew()
                gvJoining.CurrentRow = gvJoining.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvOffer_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvOffer.CurrentColumnChanged
        If gvOffer.RowCount > 0 Then
            Dim intCurrRow As Integer = gvOffer.CurrentRow.Index
            '            gvOffer.CurrentRow.Cells(colOfferCode).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvOffer.Rows.Count - 1 And clsCommon.myLen(gvOffer.CurrentRow.Cells(colofferChkListDescription).Value) > 0 Then
                gvOffer.Rows.AddNew()
                gvOffer.CurrentRow = gvOffer.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvOffer_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvOffer.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True

        End If
    End Sub

    
    Private Sub gvJoining_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvJoining.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True

        End If
    End Sub
End Class
