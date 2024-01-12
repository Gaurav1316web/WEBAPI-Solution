' ----------------- Created By Anubhooti On 05-Aug-2014 Against -------------------- '
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

Public Class FrmProfileMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Const ProfileCodeCol As String = "Profile Code"
    Const RoundCodeCol As String = "Round Code"
    Const RoundNameCol As String = "Round Name"

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmProfileMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankGridRound()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim ProfileCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ProfileCode.FormatString = ""
        ProfileCode.HeaderText = "Profile Code"
        ProfileCode.Name = ProfileCodeCol
        ProfileCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        ProfileCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ProfileCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        ProfileCode.Width = 100
        ProfileCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(ProfileCode)

        Dim RoundCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RoundCode.FormatString = ""
        RoundCode.HeaderText = "Round Code"
        RoundCode.Name = RoundCodeCol
        RoundCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        RoundCode.TextImageRelation = TextImageRelation.TextBeforeImage
        RoundCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        RoundCode.Width = 300
        gv1.MasterTemplate.Columns.Add(RoundCode)

        Dim RoundName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RoundName.FormatString = ""
        RoundName.HeaderText = "Round Name"
        RoundName.Name = RoundNameCol
        RoundName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        RoundName.Width = 300
        RoundName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RoundName)

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
    Public Sub Save()

        If AllowToSave() Then
            Dim arr As New List(Of ClsProfileMaster)
            Dim obj As New ClsProfileMaster()
            obj.Profile_Code = txtcode.Value
            obj.Description = Me.txtdesp.Text

            obj.ObjList = New List(Of ClsProfileMasterDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(RoundCodeCol).Value) <= 0 Then
                    Continue For
                End If
                Dim objTr As New ClsProfileMasterDetail()
                objTr.Profile_Code = clsCommon.myCstr(Me.txtcode.Value)
                objTr.Round_Code = clsCommon.myCstr(grow.Cells(RoundCodeCol).Value)
                obj.ObjList.Add(objTr)
            Next
            arr.Add(obj)
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (ClsProfileMaster.SaveData(arr)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Profile_Code, NavigatorType.Current)
                btnSave.Text = "Update"
                btnDelete.Enabled = True
            Else
                btnSave.Text = "Save"
                btnDelete.Enabled = False
            End If

        End If
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            btnSave.Enabled = True
            btnDelete.Enabled = True
            isNewEntry = False
            Dim Round_Name As String
            Dim obj As New ClsProfileMaster()
            obj = ClsProfileMaster.GetData(strCode, NavTyep)

            funReset()

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Profile_Code) > 0) Then

                isNewEntry = False
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtcode.Value = obj.Profile_Code
                txtdesp.Text = obj.Description
                txtcode.MyReadOnly = True
                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGridRound()
                    For Each objTr As ClsProfileMasterDetail In obj.ObjList
                        gv1.Rows.AddNew()
                        ii = ii + 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ProfileCodeCol).Value = objTr.Profile_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(RoundCodeCol).Value = objTr.Round_Code
                        Round_Name = clsDBFuncationality.getSingleValue("Select Round_Name From TSPL_HR_ROUND_MASTER Where Round_code ='" + objTr.Round_Code + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(RoundNameCol).Value = Round_Name
                    Next
                End If
                'isInsideLoadData = False
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
    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtcode.Value) <= 0 Or clsCommon.myLen(txtcode.Value) > 30 Then
            clsCommon.MyMessageBoxShow(Me, "Code can not be left blank or incorrect", Me.Text)
            txtcode.Focus()
            Return False

        ElseIf clsCommon.myLen(txtdesp.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtdesp.Focus()
            Return False

        End If
        Dim GridRow As Integer = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(RoundCodeCol).Value) <= 0 Then
                Continue For
            End If
            If clsCommon.myLen(grow.Cells(RoundCodeCol).Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please fill round code", Me.Text)
                Return False
            End If
          
            GridRow = GridRow + 1
        Next
        '' Duplication Check
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells("Round Code").Value) > 0 Then
                Dim Round As String = gv1.Rows(i).Cells("Round Code").Value
                For j As Integer = i + 1 To gv1.Rows.Count - 1
                    Dim SecondRound As String = gv1.Rows(j).Cells("Round Code").Value
                    If Round = SecondRound Then
                        clsCommon.MyMessageBoxShow(Me, "Please check ! duplicate round in grid", Me.Text)
                        Return False
                    End If
                Next
            End If
        Next
        ''
        If GridRow <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Enter at least one Round Code", Me.Text)

            Return False
        End If
        Return True
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "code not found to delete", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsProfileMaster.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub funReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtdesp.Text = ""
        Me.gv1.Rows.Clear()
        Me.gv1.Rows.AddNew()
        'isInsideLoadData = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnDelete.Enabled = False
    End Sub

    Private Sub FrmProfileMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmProfileMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGridRound()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        funReset()
    End Sub
    Sub OpenRoundCodeList(ByVal isButtonClick As Boolean)
        'gv1.CurrentRow.Cells(RoundCodeCol).Value = ""
        gv1.CurrentRow.Cells(RoundNameCol).Value = ""
        Dim qry As String = "select Round_Code as Code,Round_Name as Round_Name from TSPL_HR_ROUND_MASTER"
        gv1.CurrentRow.Cells(RoundCodeCol).Value = clsCommon.ShowSelectForm("IMRMUOM", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(RoundCodeCol).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(RoundCodeCol).Value) > 0 Then
            qry = "select Round_Code,Round_Name from TSPL_HR_ROUND_MASTER  WHERE Round_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(RoundCodeCol).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(RoundCodeCol).Value = clsCommon.myCstr(dt.Rows(0)("Round_Code"))
                gv1.CurrentRow.Cells(RoundNameCol).Value = clsCommon.myCstr(dt.Rows(0)("Round_Name"))
            End If
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(RoundCodeCol) Then
                    OpenRoundCodeList(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_HR_PROFILE_MASTER where Profile_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If
        'isInsideLoadData = True
        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Profile_Code As [Code],Profile_Name As [Profile Name] from TSPL_HR_PROFILE_MASTER"
            txtcode.Value = clsCommon.ShowSelectForm("PROF", qry, "Code", "", txtcode.Value, "TSPL_HR_PROFILE_MASTER.Profile_Code", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsProfileMaster
                objOT = ClsProfileMaster.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            'gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                'gv1.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = FullType
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                'gv1.CurrentRow.Cells(ColSNo).Value = intCurrRow
            End If
        End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            'ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
            '    common.clsCommon.MyMessageBoxShow("Can't Delete The Current Row.This Item is Used In GRN")
            '    e.Cancel = True
        End If
    End Sub
End Class
