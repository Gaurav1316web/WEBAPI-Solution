Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports XpertERPEngineFine.ClsOwnBmcExpanse

Public Class FrmOwnBmcExpanse
    Inherits FrmMainTranScreen


#Region "Variable"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

    'Public Const colFrom As String = "colFrom"
    'Public Const colTo As String = "colTo"
    'Public Const colValue As String = "colValue"
    'Public Const colRange As String = "colRange"
    Public Const colRate As String = "colRate"
    Public Const colTo As String = "colTo"
    Public Const colFrom As String = "colFrom"
    'Public Const colValue As String = "colValue"
#End Region

    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P  for Post ")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New")
        funReset()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub
    Public Sub Save()

        Try
            If (AllowToSave()) Then
                Dim obj As New ClsOwnBmcExpanse()
                obj.Code = txtCode.Value
                obj.Description = txtDescription.Text
                obj.Start_Date = dtStartDate.Value
                If dtpEndDate.Checked Then
                    obj.End_Date = dtpEndDate.Value
                End If
                obj.Description = txtDescription.Text
                obj.FAT = txtFat.Value
                obj.SNF = txtSNF.Value
                obj.Rate = txtRate.Value
                obj.Arr = New List(Of ClsOwnBmcExpanseDetail)
                For ii As Integer = 0 To gvTs.RowCount - 1
                    Dim objtr As New ClsOwnBmcExpanseDetail
                    objtr.SNF_From = clsCommon.myCDecimal(gvTs.Rows(ii).Cells(colFrom).Value)
                    objtr.SNF_To = clsCommon.myCDecimal(gvTs.Rows(ii).Cells(colTo).Value)
                    objtr.Rate = clsCommon.myCDecimal(gvTs.Rows(ii).Cells(colRate).Value)
                    If objtr.SNF_To > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                Next
                If obj.Arr.Count <= 0 Then
                    Throw New Exception("Please define at least one row for slab.")
                End If
                If obj.SaveData(obj, isNewEntry) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            funReset()
            Dim obj As ClsOwnBmcExpanse = ClsOwnBmcExpanse.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                btnsave.Enabled = True
                btnPost.Enabled = True
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDescription.Text = obj.Description
                dtStartDate.Value = obj.Start_Date
                If obj.End_Date IsNot Nothing Then
                    dtpEndDate.Checked = True
                    dtpEndDate.Value = obj.End_Date
                Else
                    dtpEndDate.Checked = False
                End If
                txtFat.Value = obj.FAT
                txtSNF.Value = obj.SNF
                txtRate.Value = obj.Rate
                UsLock1.Status = obj.Posted
                chkInactive.Checked = obj.Inactive
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    chkInactive.Enabled = Not obj.Inactive
                    If chkInactive.Enabled Then
                        chkInactive.Enabled = MyBase.isPostFlag
                    End If
                End If
                For Each objtr As ClsOwnBmcExpanse.ClsOwnBmcExpanseDetail In obj.Arr
                    gvTs.Rows(gvTs.Rows.Count - 1).Cells(colFrom).Value = objtr.SNF_From
                    gvTs.Rows(gvTs.Rows.Count - 1).Cells(colTo).Value = objtr.SNF_To
                    gvTs.Rows(gvTs.Rows.Count - 1).Cells(colRate).Value = objtr.Rate
                    gvTs.Rows.AddNew()
                Next
                btnsave.Text = "Update"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If txtFat.Value <= 0 Then
            myMessages.blankValue("FAT")
            txtFat.Focus()
            Return False
        End If
        If txtSNF.Value <= 0 Then
            myMessages.blankValue("SNF")
            txtSNF.Focus()
            Return False
        End If
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue("Description")
            txtDescription.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsOwnBmcExpanse.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_OWN_BMC_EXPANSE where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = ClsOwnBmcExpanse.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Focus()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        txtFat.Text = ""
        txtSNF.Text = ""
        txtRate.Text = ""
        txtCode.Value = Nothing
        txtDescription.Text = Nothing
        LoadBlankGrid()
        gvTs.Rows.AddNew()
        dtpEndDate.Value = clsCommon.GETSERVERDATE()
        dtStartDate.Value = dtpEndDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub
    Private Sub frmHSNMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub
    Private Sub rdbtnreset_Click(sender As Object, e As EventArgs) Handles rdbtnreset.Click
        funReset()
    End Sub
    Private Sub chkInactive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInactive.ToggleStateChanged
        Try
            If Not isInsideLoadData Then
                If chkInactive.Checked Then
                    If clsCommon.myLen(txtCode.Value) > 0 Then
                        If clsCommon.MyMessageBoxShow(Me, "Current code [" + txtCode.Value + "] will be inactive" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, Telerik.WinControls.RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            If (ClsOwnBmcExpanse.InactiveData(txtCode.Value)) Then
                                clsCommon.MyMessageBoxShow(Me, "Successfully Inactivated")
                            End If
                        End If
                    End If
                End If
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Please select document no to post")
            End If
            If (myMessages.postConfirm()) Then
                If (ClsOwnBmcExpanse.PostData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            txtCode.Value = ClsOwnBmcExpanse.getFinder("", txtCode.Value, True)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
                UsLock1.Status = ERPTransactionStatus.Pending
                isNewEntry = True
                txtCode.Value = ""
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btnsave.Enabled = True
                btnsave.Text = "Save"
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGrid()
        Try
            'gvTs.Rows.Clear()
            'gvTs.Columns.Clear()

            gvTs.Rows.Clear()
            gvTs.Columns.Clear()
            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colFrom
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 5000
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            'repoDeciCol.HeaderText = "Slab Upto"
            repoDeciCol.HeaderText = "From SNF"
            gvTs.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colTo
            repoDeciCol.Width = 120
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Maximum = 5000
            'repoDeciCol.ReadOnly = True
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.Step = 0
            repoDeciCol.HeaderText = "To SNF"
            gvTs.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colRate
            repoDeciCol.Width = 200
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Rate"
            gvTs.MasterTemplate.Columns.Add(repoDeciCol)

            gvTs.AllowDeleteRow = True
            gvTs.AllowAddNewRow = False
            gvTs.ShowGroupPanel = False
            gvTs.AllowColumnReorder = False
            gvTs.AllowRowReorder = False
            gvTs.EnableSorting = False
            gvTs.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gvTs.MasterTemplate.ShowRowHeaderColumn = False
            gvTs.TableElement.TableHeaderHeight = 40
            gvTs.AutoSizeRows = False
            'gvTs.AllowRowReorder = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub gvTs_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvTs.UserDeletedRow
        Try
            If gvTs.CurrentRow.Index > 0 Then
                gvTs.Rows(gvTs.CurrentRow.Index - 1).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(gvTs.Rows(gvTs.CurrentRow.Index).Cells(colFrom).Value) - 1)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub gvTs_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvTs.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTs_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvTs.CurrentColumnChanged
        If gvTs.RowCount > 0 Then
            Dim intCurrRow As Integer = gvTs.CurrentRow.Index
            If intCurrRow = gvTs.Rows.Count - 1 Then
                gvTs.Rows.AddNew()
                gvTs.CurrentRow = gvTs.Rows(intCurrRow)
            End If
        End If
    End Sub


    Dim isCellValueChanged As Boolean = False
    Private Sub gvTS_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvTs.CellValueChanged
        Try
            'If (Not isInsideLoadData) Then
            '    If Not isCellValueChanged Then
            '        isCellValueChanged = True
            '        If e.Column Is gvTs.Columns(colFrom) Then
            '            If gvTs.Rows.Count > (gvTs.CurrentRow.Index + 1) Then
            '                If clsCommon.myCdbl(gvTs.Rows(gvTs.CurrentRow.Index + 1).Cells(colFrom).Value) > 0 Then
            '                    gvTs.Rows(gvTs.CurrentRow.Index).Cells(colTo).Value = gvTs.Rows(gvTs.CurrentRow.Index + 1).Cells(colFrom).Value - 0.01
            '                Else
            '                    gvTs.Rows(gvTs.CurrentRow.Index).Cells(colTo).Value = 0
            '                End If
            '            Else
            '                gvTs.Rows(gvTs.CurrentRow.Index).Cells(colTo).Value = 0
            '            End If

            '            If gvTs.CurrentRow.Index > 0 Then
            '                gvTs.Rows(gvTs.CurrentRow.Index - 1).Cells(colTo).Value = gvTs.Rows(gvTs.CurrentRow.Index).Cells(colFrom).Value - 0.01
            '            End If
            '        End If
            '        isCellValueChanged = False
            '    End If

            'End If

            Try
                If (Not isInsideLoadData) Then
                    If e.Column.Name = colTo And clsCommon.myCdbl(e.Value) > 0 And gvTs.CurrentRow.Index > 0 Then
                        'gvTs.Rows(gvTs.CurrentRow.Index - 1).Cells(colTo).Value = clsCommon.myCdbl(clsCommon.myCdbl(e.Value) - 1)
                        gvTs.Rows(gvTs.CurrentRow.Index).Cells(colFrom).Value = clsCommon.myCdbl(clsCommon.myCdbl(gvTs.Rows(gvTs.CurrentRow.Index - 1).Cells(colTo).Value) + 0.01)
                        'If (gvTs.CurrentRow.Index + 0.01 = gvTs.Rows.Count) OrElse (clsCommon.myCdbl(gvTs.Rows(gvTs.CurrentRow.Index 0.01).Cells(colTo).Value) = 0) Then
                        ' gvTs.Rows(gvTs.CurrentRow.Index).Cells(colTo).Value = 5000
                    End If
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

End Class