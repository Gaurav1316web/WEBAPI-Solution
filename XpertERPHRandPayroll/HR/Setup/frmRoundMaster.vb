' ----------------- Created By Anubhooti On 06-Aug-2014 Against -------------------- '
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

Public Class FrmRoundMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Const RoundCodeCol As String = "Round Code"
    Const ParameterCodeCol As String = "Parameter Code"
    Const ParameterNameCol As String = "Parameter Name"
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmRoundMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankGridRound()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim RoundCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RoundCode.FormatString = ""
        RoundCode.HeaderText = "Round Code"
        RoundCode.Name = RoundCodeCol
        RoundCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        RoundCode.TextImageRelation = TextImageRelation.TextBeforeImage
        RoundCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        RoundCode.Width = 100
        RoundCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(RoundCode)

        Dim ParamaeterCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ParamaeterCode.FormatString = ""
        ParamaeterCode.HeaderText = "Parameter Code"
        ParamaeterCode.Name = ParameterCodeCol
        ParamaeterCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        ParamaeterCode.TextImageRelation = TextImageRelation.TextBeforeImage
        ParamaeterCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        ParamaeterCode.Width = 300
        gv1.MasterTemplate.Columns.Add(ParamaeterCode)

        Dim ParameterName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ParameterName.FormatString = ""
        ParameterName.HeaderText = "Parameter Name"
        ParameterName.Name = ParameterNameCol
        ParameterName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        ParameterName.Width = 300
        ParameterName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ParameterName)

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
            Dim arr As New List(Of ClsRoundMaster)
            Dim obj As New ClsRoundMaster()
            obj.Round_Code = txtcode.Value
            obj.Description = Me.txtdesp.Text
            obj.Clearing_Score = txtClearingScore.Value

            obj.ObjList = New List(Of ClsRoundDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(ParameterCodeCol).Value) <= 0 Then
                    Continue For
                End If
                Dim objTr As New ClsRoundDetail()
                objTr.Round_Code = clsCommon.myCstr(Me.txtcode.Value)
                objTr.Parameter_Code = clsCommon.myCstr(grow.Cells(ParameterCodeCol).Value)
                obj.ObjList.Add(objTr)
            Next
            arr.Add(obj)
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (ClsRoundMaster.SaveData(arr)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Round_Code, NavigatorType.Current)
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
            Dim Parameter_Name As String
            Dim obj As New ClsRoundMaster()
            obj = ClsRoundMaster.GetData(strCode, NavTyep)
            funReset()
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Round_Code) > 0) Then

                isNewEntry = False
                btnSave.Text = "Update"
                btnDelete.Enabled = True
                txtcode.Value = obj.Round_Code
                txtdesp.Text = obj.Description
                txtClearingScore.Value = obj.Clearing_Score

                txtcode.MyReadOnly = True
                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGridRound()
                    For Each objTr As ClsRoundDetail In obj.ObjList
                        gv1.Rows.AddNew()
                        ii = ii + 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(RoundCodeCol).Value = objTr.Round_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ParameterCodeCol).Value = objTr.Parameter_Code
                        Parameter_Name = clsDBFuncationality.getSingleValue("Select Parameter_Name From TSPL_HR_PARAMETER_MASTER Where Parameter_Code ='" + objTr.Parameter_Code + "'")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ParameterNameCol).Value = Parameter_Name
                    Next
                End If
                'isInsideLoadData = False
            Else
                isNewEntry = True
                Me.gv1.Rows.Clear()
                Me.gv1.Rows.AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtcode.Value) <= 0 Or clsCommon.myLen(txtcode.Value) > 30 Then
                myMessages.blankValue("Code")

                txtcode.Focus()
                txtcode.Select()
                Errorcontrol.SetError(txtcode, "Code")
                Return False
            Else
                Errorcontrol.ResetError(txtcode)
            End If

            If clsCommon.myLen(txtdesp.Text) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtdesp.Text)) > 100 Then
                myMessages.blankValue("Description")

                txtdesp.Focus()
                txtdesp.Select()
                Errorcontrol.SetError(txtdesp, "Description")
                Return False
            Else
                Errorcontrol.ResetError(txtdesp)
            End If
            If clsCommon.myCdbl(txtClearingScore.Value) <= 0 Then
                'myMessages.blankValue("Clearing score")
                clsCommon.MyMessageBoxShow("Clearing score can not be left")
                txtClearingScore.Focus()
                txtClearingScore.Select()
                Errorcontrol.SetError(txtClearingScore, "Clearing score")
                Return False
            Else
                Errorcontrol.ResetError(txtClearingScore)
            End If
            If clsCommon.myCdbl(txtClearingScore.Value) > 100 Then
                clsCommon.MyMessageBoxShow("Clearing score can not be more than 100")
                txtClearingScore.Focus()
                txtClearingScore.Select()
                Errorcontrol.SetError(txtClearingScore, "Clearing score")
                Return False
            Else
                Errorcontrol.ResetError(txtClearingScore)
            End If
            Dim GridRow As Integer = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(grow.Cells(ParameterCodeCol).Value) <= 0 Then
                    Continue For
                End If

                If clsCommon.myLen(grow.Cells(ParameterCodeCol).Value) <= 0 Then
                    Throw New Exception("Please fill parameter code")
                    'Return False
                End If

                GridRow = GridRow + 1
            Next
            '' Duplication Check
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells("Parameter Code").Value) > 0 Then
                    Dim Parameter As String = gv1.Rows(i).Cells("Parameter Code").Value
                    For j As Integer = i + 1 To gv1.Rows.Count - 1
                        Dim SecondParameter As String = gv1.Rows(j).Cells("Parameter Code").Value
                        If Parameter = SecondParameter Then
                            Throw New Exception("Please check ! duplicate parameter in grid")
                        End If
                    Next
                End If
            Next
            ''
            If GridRow <= 0 Then
                Throw New Exception("Enter at least one Parameter Code")

                'Return False
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete")
            Exit Sub
        End If

        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsRoundMaster.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
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
        txtcode.Value = Nothing
        txtcode.Focus()
        txtdesp.Text = ""
        txtClearingScore.Value = 0
        Me.gv1.Rows.Clear()
        Me.gv1.Rows.AddNew()
        'isInsideLoadData = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = False
    End Sub

    Private Sub FrmRoundMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FrmRoundMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGridRound()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        funReset()
    End Sub
    Sub OpenParameterCodeList(ByVal isButtonClick As Boolean)
        'gv1.CurrentRow.Cells(RoundCodeCol).Value = ""
        gv1.CurrentRow.Cells(ParameterNameCol).Value = ""
        Dim qry As String = "select Parameter_Code as Code,Parameter_Name as [Parameter Name] from TSPL_HR_PARAMETER_MASTER"
        gv1.CurrentRow.Cells(ParameterCodeCol).Value = clsCommon.ShowSelectForm("Para", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(ParameterCodeCol).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(ParameterCodeCol).Value) > 0 Then
            qry = "select Parameter_Code,Parameter_Name as [Parameter Name] from TSPL_HR_PARAMETER_MASTER WHERE Parameter_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ParameterCodeCol).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(ParameterCodeCol).Value = clsCommon.myCstr(dt.Rows(0)("Parameter_Code"))
                gv1.CurrentRow.Cells(ParameterNameCol).Value = clsCommon.myCstr(dt.Rows(0)("Parameter Name"))
            End If
        End If
    End Sub
#End Region
#Region "Functions"
    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(ParameterCodeCol) Then
                    OpenParameterCodeList(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_HR_ROUND_MASTER where Round_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If
        'isInsideLoadData = True
        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select Round_Code As [Code],Round_Name As [Round Name] from TSPL_HR_ROUND_MASTER"
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_HR_ROUND_MASTER", qry, "Code", "", txtcode.Value, "TSPL_HR_ROUND_MASTER.Round_Code", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsRoundMaster
                objOT = ClsRoundMaster.GetData(txtcode.Value, NavigatorType.Current)
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

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub
#End Region

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
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            'ElseIf clsCommon.myCBool(gv1.CurrentRow.Cells(colItemUsedINGRN).Value) Then
            '    common.clsCommon.MyMessageBoxShow("Can't Delete The Current Row.This Item is Used In GRN")
            '    e.Cancel = True
        End If
    End Sub
End Class
