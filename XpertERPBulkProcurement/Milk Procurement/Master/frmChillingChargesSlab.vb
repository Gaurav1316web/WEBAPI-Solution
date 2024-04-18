Imports common
Public Class frmChillingChargesSlab
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

    Public Const colCapacity As String = "colCapacity"
    Public Const colRate As String = "colRate"
#End Region
    Private Sub frmJWPriceCodeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim coll As New Dictionary(Of String, String)()
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
                Dim obj As New clsChillingCharges()
                obj.Code = txtCode.Value
                obj.Start_Date = dtStartDate.Value
                If dtpEndDate.Checked Then
                    obj.End_Date = dtpEndDate.Value
                End If
                obj.Description = txtDescription.Text
                If rbtnApplicableOnSiloCapacity.IsChecked Then
                    obj.Applicable_On = 1
                ElseIf rbtnApplicableOnMilkReceived.IsChecked Then
                    obj.Applicable_On = 2
                ElseIf rbtnApplicableOnLower.IsChecked Then
                    obj.Applicable_On = 3
                End If
                If rbtnCalculationMehodDayWise.IsChecked Then
                    obj.Calculation_Mehod = 1
                ElseIf rbtnCalculationMehodAvg.IsChecked Then
                    obj.Calculation_Mehod = 2
                End If
                If rbtnPaymentMehodMonthly.IsChecked Then
                    obj.Payment_Mehod = 1
                ElseIf rbtnPaymentMehodPaymentCycle.IsChecked Then
                    obj.Payment_Mehod = 2
                End If
                obj.Arr = New List(Of clsChillingChargesSlab)
                Dim arrCapacityCheck As New List(Of Integer)
                For ii As Integer = 0 To gv1.RowCount - 1
                    Dim objtr As New clsChillingChargesSlab
                    objtr.Capacity = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCapacity).Value)
                    objtr.Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
                    If objtr.Capacity > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                    If arrCapacityCheck.Contains(objtr.Capacity) Then
                        Throw New Exception("Repeated capacity [" + clsCommon.myCstr(objtr.Capacity) + "]")
                    Else
                        arrCapacityCheck.Contains(objtr.Capacity)
                    End If
                Next
                If obj.Arr.Count <= 0 Then
                    Throw New Exception("Please define at least one row for slab.")
                End If
                arrCapacityCheck = Nothing
                If obj.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            funReset()
            Dim obj As clsChillingCharges = clsChillingCharges.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                btnsave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
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

                    btnEndDate.Enabled = Not dtpEndDate.Checked
                    If btnEndDate.Enabled Then
                        btnEndDate.Enabled = MyBase.isPostFlag
                    End If
                End If

                If obj.Applicable_On = 1 Then
                    rbtnApplicableOnSiloCapacity.IsChecked = True
                ElseIf obj.Applicable_On = 2 Then
                    rbtnApplicableOnMilkReceived.IsChecked = True
                ElseIf obj.Applicable_On = 3 Then
                    rbtnApplicableOnLower.IsChecked = True
                End If
                If obj.Calculation_Mehod = 1 Then
                    rbtnCalculationMehodDayWise.IsChecked = True
                ElseIf obj.Calculation_Mehod = 2 Then
                    rbtnCalculationMehodAvg.IsChecked = True
                End If
                If obj.Payment_Mehod = 1 Then
                    rbtnPaymentMehodMonthly.IsChecked = True
                ElseIf obj.Payment_Mehod = 2 Then
                    rbtnPaymentMehodPaymentCycle.IsChecked = True
                End If
                For Each objtr As clsChillingChargesSlab In obj.Arr
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = objtr.Capacity
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objtr.Rate
                    gv1.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtDescription.Text) <= 0 Then
            myMessages.blankValue(Me, "Description", Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsChillingCharges.DeleteData(txtCode.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_CHILLING_CHARGES where Code ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            txtCode.Value = clsChillingCharges.getFinder("", txtCode.Value, isButtonClicked)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Focus()
        btnsave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        chkInactive.Enabled = False
        chkInactive.Checked = False
        btnEndDate.Enabled = False
        txtCode.Value = Nothing
        txtDescription.Text = Nothing

        rbtnApplicableOnSiloCapacity.IsChecked = True
        rbtnPaymentMehodMonthly.IsChecked = True
        rbtnCalculationMehodDayWise.IsChecked = True

        loadBlankGridRange()
        gv1.Rows.AddNew()
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
                            If (clsChillingCharges.InactiveData(txtCode.Value)) Then
                                clsCommon.MyMessageBoxShow(Me, "Successfully Inactivated", Me.Text)
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
                If (clsChillingCharges.PostData(txtCode.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Private Sub btnEndDate_Click(sender As Object, e As EventArgs) Handles btnEndDate.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("Document No not found to update")
            End If
            Dim obj As New clsChillingCharges()
            obj.Code = txtCode.Value
            obj.End_Date = dtpEndDate.Value
            obj.SaveEndDateData(obj)
            clsCommon.MyMessageBoxShow(Me, "Successfully Updated", Me.Text)
            LoadData(txtCode.Value, NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            txtCode.Value = clsChillingCharges.getFinder("", txtCode.Value, True)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
                UsLock1.Status = ERPTransactionStatus.Pending
                isNewEntry = True
                txtCode.Value = ""
                btnDelete.Enabled = False
                btnPost.Enabled = False
                btnsave.Enabled = True
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Sub loadBlankGridRange()
        Try
            gv1.Rows.Clear()
            gv1.Columns.Clear()


            Dim repoDeciCol As GridViewDecimalColumn
            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colCapacity
            repoDeciCol.Width = 100
            repoDeciCol.FormatString = "{0:n0}"
            repoDeciCol.DecimalPlaces = 0
            repoDeciCol.Minimum = 0
            'repoDeciCol.Maximum = 200
            'repoDeciCol.ReadOnly = False
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.Step = 0
            repoDeciCol.HeaderText = "Silo Capacity"
            gv1.MasterTemplate.Columns.Add(repoDeciCol)

            repoDeciCol = New GridViewDecimalColumn()
            repoDeciCol.Name = colRate
            repoDeciCol.Width = 200
            repoDeciCol.DecimalPlaces = 2
            repoDeciCol.Minimum = 0
            repoDeciCol.Step = 0
            repoDeciCol.ShowUpDownButtons = False
            repoDeciCol.HeaderText = "Chilling Charges Rate"
            gv1.MasterTemplate.Columns.Add(repoDeciCol)

            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.AutoSizeRows = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub GvTS_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            If gv1.CurrentRow.Index > 0 Then
                gv1.Rows(gv1.CurrentRow.Index - 1).Cells(colCapacity).Value = clsCommon.myCdbl(clsCommon.myCdbl(gv1.Rows(gv1.CurrentRow.Index).Cells(colRate).Value) - 0.01)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub GvTS_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If Not myMessages.deleteConfirm() Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTS_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub


    Dim isCellValueChanged As Boolean = False
    Private Sub gvTS_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChanged Then
                    isCellValueChanged = True
                    'If e.Column Is gv1.Columns(colFrom) Then
                    '    If gv1.Rows.Count > (gv1.CurrentRow.Index + 1) Then
                    '        If clsCommon.myCdbl(gv1.Rows(gv1.CurrentRow.Index + 1).Cells(colFrom).Value) > 0 Then
                    '            gv1.Rows(gv1.CurrentRow.Index).Cells(colCapacity).Value = gv1.Rows(gv1.CurrentRow.Index + 1).Cells(colFrom).Value - 0.01
                    '        Else
                    '            gv1.Rows(gv1.CurrentRow.Index).Cells(colCapacity).Value = 15
                    '        End If
                    '    Else
                    '        gv1.Rows(gv1.CurrentRow.Index).Cells(colCapacity).Value = 15
                    '    End If

                    '    If gv1.CurrentRow.Index > 0 Then
                    '        gv1.Rows(gv1.CurrentRow.Index - 1).Cells(colCapacity).Value = gv1.Rows(gv1.CurrentRow.Index).Cells(colFrom).Value - 0.01
                    '    End If
                    'End If
                    isCellValueChanged = False
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class