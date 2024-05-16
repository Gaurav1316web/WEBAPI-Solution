'--03/07/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports XpertERPEngine
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class frmGenerateBonus
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colLineNo As String = "LineNo"
    Const colempcode As String = "empcode"
    Const colempname As String = "empname"
    Const colbonusCode As String = "bonusCode"
    Const colbonusName As String = "bonusName"
    Const colbonusAmount As String = "bonusAmount"

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SavingData(False)
    End Sub

    Public Function Save() As Boolean
        Try
            If AllowToSave() Then
                GenerateBonus()
                Dim obj As New clsBonus()
                obj.EMP_BONUS_CODE = txtCode.Value
                obj.Location_Code = fndLocation.Value
                obj.Division_Code = fndDivision.Value

                obj.FROM_PAY_PERIOD_CODE = txtFromPayPeriodCode.Value
                obj.TO_PAY_PERIOD_CODE = txtToPayPeriodCode.Value
                obj.PAYABLE_PAY_PERIOD_CODE = txtPayablePayPeriodCode.Value
                obj.DESCRIPTION = txtDescription.Text
                obj.ObjList = New List(Of clsBonusDetails)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colempcode).Value) > 0 Then

                        Dim objTr As New clsBonusDetails()
                        objTr.EMP_BONUS_CODE = txtCode.Value
                        objTr.EMP_CODE = clsCommon.myCstr(grow.Cells(colempcode).Value)
                        objTr.BONUS_CODE = clsCommon.myCstr(grow.Cells(colbonusCode).Value)
                        objTr.BONUS_AMOUNT = clsCommon.myCdbl(grow.Cells(colbonusAmount).Value)
                        obj.ObjList.Add(objTr)
                    End If

                Next

                If (obj.SaveData(obj, isNewEntry)) Then
                    'common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.EMP_BONUS_CODE, NavigatorType.Current)
                    Return True
                    'Else
                    '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try

        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        'btnsave.Enabled = True
        'btndelete.Enabled = True
        Dim obj As New clsBonus()
        obj = clsBonus.GetData(strCode, NavTyep)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_BONUS_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            If obj.POSTED Then
                btnsave.Enabled = False
                btnPost.Enabled = False
                btndelete.Enabled = False
                btnGenerate.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
            Else
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnPost.Enabled = True
                btnGenerate.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            txtCode.Value = obj.EMP_BONUS_CODE
            fndLocation.Value = obj.Location_Code
            lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
            fndDivision.Value = obj.Division_Code
            lblDivision.Text = clsDevisionMaster.GetName(fndDivision.Value, Nothing)

            txtFromPayPeriodCode.Value = obj.FROM_PAY_PERIOD_CODE
            lblFromPayPeriodName.Text = clsPayPeriodMaster.GetName(txtFromPayPeriodCode.Value, Nothing)
            txtToPayPeriodCode.Value = obj.TO_PAY_PERIOD_CODE
            lblToPayPeriodName.Text = clsPayPeriodMaster.GetName(txtToPayPeriodCode.Value, Nothing)
            txtPayablePayPeriodCode.Value = obj.PAYABLE_PAY_PERIOD_CODE
            lblPayablePayPeriodName.Text = clsPayPeriodMaster.GetName(txtPayablePayPeriodCode.Value, Nothing)
            txtDescription.Text = obj.DESCRIPTION
            'txtToPayPeriodCode__MYValidating(Nothing, Nothing, False)
            'txtFromPayPeriodCode__MYValidating(Nothing, Nothing, False)
            'txtPayablePayPeriodCode__MYValidating(Nothing, Nothing, False)
            Dim ii As Int16 = 0
            If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                LoadGridColumns()
                For Each objTr As clsBonusDetails In obj.ObjList
                    gv1.Rows.AddNew()
                    ii = ii + 1
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = ii
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colempcode).Value = objTr.EMP_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colempname).Value = objTr.EMP_NAME
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colbonusCode).Value = objTr.BONUS_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colbonusName).Value = objTr.BONUS_NAME
5:                  gv1.Rows(gv1.Rows.Count - 1).Cells(colbonusAmount).Value = objTr.BONUS_AMOUNT
                Next
            End If
            '' fill report tab
            LoadBonusSummaryData(False)
            LoadBonusDetailData(False)
        End If


    End Sub

    Function AllowToSave() As Boolean
        If btnsave.Text = "Update" Then
            Dim QryStr As String = "select POSTED from TSPL_EMPLOYEE_BONUS  where EMP_BONUS_CODE = '" + txtCode.Value + "' "
            Dim chkpost As String = clsDBFuncationality.getSingleValue(QryStr)
            If chkpost = "1" Then
                clsCommon.MyMessageBoxShow(Me, "Transection already posted", Me.Text)
                Return False
            End If
        End If

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'Else
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            myMessages.blankValue(Me, "Location Code", Me.Text)
            fndLocation.Focus()
            Return False
        ElseIf clsCommon.myLen(txtToPayPeriodCode.Value) <= 0 Then
            myMessages.blankValue(Me, "To Pay Period Code", Me.Text)
            txtToPayPeriodCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtFromPayPeriodCode.Value) <= 0 Then
            myMessages.blankValue(Me, "From Pay Period Code", Me.Text)
            txtFromPayPeriodCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtPayablePayPeriodCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Payable Pay Period Code", Me.Text)
            txtPayablePayPeriodCode.Focus()
            Return False
        End If

        Dim II As Int16 = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            II = II + 1

            'If clsCommon.myCdbl(grow.Cells(colbonusCode).Value) > 366 Then
            '    clsCommon.MyMessageBoxShow("Value of ")
            'End If

        Next
        '' Anubhooti 10-July-2014 (BM00000002913)
        If CheckSalStructure() = False Then
            Return False
        End If
        Return True
    End Function
    '' Anubhooti 10-July-2014 (BM00000002913)
    Function CheckSalStructure() As Boolean
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsLTAClaim.CheckPayHead(clsCommon.myCstr(grow.Cells(colempcode).Value), "Bonus".ToUpper(), clsCommon.GETSERVERDATE()) = False Then
                Return False
            End If
        Next
        Return True
    End Function
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsBonus.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmGenerateBonus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmGenerateBonus)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        fndLocation.Value = Nothing
        lblLocation.Text = ""
        fndDivision.Value = Nothing
        lblDivision.Text = ""
        txtToPayPeriodCode.Value = Nothing
        lblToPayPeriodName.Text = ""
        txtFromPayPeriodCode.Value = Nothing
        lblFromPayPeriodName.Text = ""
        txtPayablePayPeriodCode.Value = Nothing
        lblPayablePayPeriodName.Text = ""
        txtDescription.Text = ""
        LoadGridColumns()
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        btnPost.Enabled = True
        btnGenerate.Enabled = True
        btnGenerate.Visible = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_EMPLOYEE_BONUS where EMP_BONUS_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select EMP_BONUS_CODE as Code,  FROM_PAY_PERIOD_CODE as 'From Pay Period', TO_PAY_PERIOD_CODE as 'To Pay Period',PAYABLE_PAY_PERIOD_CODE as 'Payable Pay Period', DESCRIPTION as Description from TSPL_EMPLOYEE_BONUS "
            txtCode.Value = clsCommon.ShowSelectForm("EMP_BONUS", qry, "Code", "", txtCode.Value, "EMP_BONUS_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmGenerateBonus_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
        End If
    End Sub

    Private Sub txtToPayPeriodCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtToPayPeriodCode._MYValidating
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        txtToPayPeriodCode.Value = clsPayPeriodMaster.getFinder("POSTED=1 and convert(date, date_from,103) <= Convert (date,SYSDATETIME(),103)", txtToPayPeriodCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtToPayPeriodCode.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblToPayPeriodName.Text = clsPayPeriodMaster.GetName(txtToPayPeriodCode.Value, Nothing)
    End Sub

    Private Sub txtFromPayPeriodCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPayPeriodCode._MYValidating

        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        txtFromPayPeriodCode.Value = clsPayPeriodMaster.getFinder("POSTED=1 and convert(date, date_from,103) <= Convert (date,SYSDATETIME(),103)", txtFromPayPeriodCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtFromPayPeriodCode.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblFromPayPeriodName.Text = clsPayPeriodMaster.GetName(txtFromPayPeriodCode.Value, Nothing)
    End Sub

    Private Sub txtPayablePayPeriodCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPayablePayPeriodCode._MYValidating
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        txtPayablePayPeriodCode.Value = clsPayPeriodMaster.getFinder("POSTED=1 and convert(date, date_from,103) <= Convert (date,SYSDATETIME(),103)", txtPayablePayPeriodCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtPayablePayPeriodCode.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblPayablePayPeriodName.Text = clsPayPeriodMaster.GetName(txtPayablePayPeriodCode.Value, Nothing)
    End Sub
    Sub LoadGridColumns()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.ReadOnly = False

        Dim lineNo As GridViewTextBoxColumn
        lineNo = New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colLineNo
        lineNo.Width = 50
        lineNo.ReadOnly = True
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(lineNo)

        Dim empcode As New GridViewTextBoxColumn
        empcode.FormatString = ""
        empcode.HeaderText = "Employee Code"
        empcode.Name = colempcode
        empcode.Width = 100
        empcode.ReadOnly = True
        empcode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(empcode)

        Dim empname As New GridViewTextBoxColumn
        empname.FormatString = ""
        empname.HeaderText = "Employee Name"
        empname.Name = colempname
        empname.Width = 200
        empname.ReadOnly = True
        empname.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(empname)

        Dim bonusCode As New GridViewTextBoxColumn
        bonusCode.FormatString = ""
        bonusCode.HeaderText = "Bonus Code"
        bonusCode.Name = colbonusCode
        bonusCode.Width = 100
        bonusCode.ReadOnly = True
        bonusCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(bonusCode)

        Dim bonusName As New GridViewTextBoxColumn
        bonusName.FormatString = ""
        bonusName.HeaderText = "Bonus Name"
        bonusName.Name = colbonusName
        bonusName.Width = 150
        bonusName.ReadOnly = True
        bonusName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(bonusName)

        Dim bonusAmount As New GridViewDecimalColumn
        bonusAmount.FormatString = ""
        bonusAmount.HeaderText = "Bonus Amount"
        bonusAmount.Name = colbonusAmount
        bonusAmount.Width = 150
        bonusName.ReadOnly = False
        bonusAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(bonusAmount)

        gv1.ReadOnly = False

    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs)
        '    If gv1.Rows.Count > 0 Then
        '        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '        gv1.CurrentRow.Cells(0).Value = clsCommon.myCdbl(intCurrRow + 1)
        '        If intCurrRow = gv1.Rows.Count - 1 Then
        '            gv1.Rows.AddNew()
        '            gv1.CurrentRow = gv1.Rows(intCurrRow)
        '        End If
        '    End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
        '    If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
        '        e.Cancel = True
        '    End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)

        'If e.Column Is gv1.Columns("LeaveCode") Then
        '    Dim qry As String = " select LEAVE_CODE AS Code, LEAVE_NAME as Name, PRINT_NAME as 'Print Name', AFFECTS_SALARY as 'Is Affects Salary'  from TSPL_LEAVE_MASTER"
        '    gv1.CurrentRow.Cells(1).Value = clsCommon.ShowSelectForm("LEAVE_MASTER", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value), "LEAVE_CODE", False)
        '    gv1.CurrentRow.Cells(2).Value = clsLeaveMaster.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(1).Value), Nothing)
        'End If

    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsBonus.PostData(txtCode.Value, True)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        GenerateBonus()
    End Sub
    Sub GenerateBonus()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()

        LoadBonusSummaryData(True)
        LoadBonusDetailData(True)

        Dim ii As Int16 = 0
        Dim dt As DataTable = gvBonusSummary.DataSource
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            LoadGridColumns()
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                ii = ii + 1
                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = ii.ToString()
                gv1.Rows(gv1.Rows.Count - 1).Cells(colempcode).Value = dr("empcode")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colempname).Value = dr("empname")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colbonusCode).Value = dr("bonuscode")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colbonusName).Value = dr("bonusname")
                gv1.Rows(gv1.Rows.Count - 1).Cells(colbonusAmount).Value = dr("TOTAL BONUS")
            Next
        End If
    End Sub
    Sub LoadBonusSummaryData(ByVal Recalculate As Boolean)
        Dim doc_code As String
        If isNewEntry Or Recalculate Then
            doc_code = ""
        Else
            doc_code = txtCode.Value
        End If
        Dim dt As DataTable = clsBonus.GetGenerateBonusDataTable(fndLocation.Value, fndDivision.Value, txtFromPayPeriodCode.Value, txtToPayPeriodCode.Value, txtPayablePayPeriodCode.Value, True, doc_code)
        gvBonusSummary.DataSource = Nothing
        gvBonusSummary.DataSource = dt
        gvBonusSummary.ReadOnly = True
        gvBonusSummary.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For Each col As GridViewDataColumn In gvBonusSummary.Columns
            If col.Name.Contains("Basic") OrElse col.Name.Contains("Wages") OrElse col.Name.Contains("PD") OrElse col.Name.Contains("Total") Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            End If
        Next
        gvBonusSummary.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvBonusSummary.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        For col As Integer = 0 To gvBonusSummary.Columns.Count - 1
            gvBonusSummary.Columns(col).Width = 100
        Next
    End Sub
    Sub LoadBonusDetailData(ByVal Recalculate As Boolean)
        Dim doc_code As String
        If isNewEntry Or Recalculate Then
            doc_code = ""
        Else
            doc_code = txtCode.Value
        End If
        Dim dt As DataTable = clsBonus.GetGenerateBonusDataTable(fndLocation.Value, fndDivision.Value, txtFromPayPeriodCode.Value, txtToPayPeriodCode.Value, txtPayablePayPeriodCode.Value, False, doc_code)
        gvBonusDetail.DataSource = Nothing
        gvBonusDetail.DataSource = dt
        gvBonusDetail.ReadOnly = True
        gvBonusDetail.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For Each col As GridViewDataColumn In gvBonusDetail.Columns
            If col.Name.Contains("Basic") OrElse col.Name.Contains("Wages") OrElse col.Name.Contains("PD") OrElse col.Name.Contains("Total") Then
                Dim item1 As New GridViewSummaryItem(col.Name, "{0:F3}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            End If
        Next
        gvBonusDetail.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gvBonusDetail.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        For col As Integer = 0 To gvBonusDetail.Columns.Count - 1
            gvBonusDetail.Columns(col).Width = 100
        Next
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsSalaryGeneration.getFinderForSalaryLocation("", fndLocation.Value, isButtonClicked) 'clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtFromPayPeriodCode.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblLocation.Text = clsLocation.GetName(fndLocation.Value, Nothing)
    End Sub

    Private Sub fndDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivision._MYValidating
        fndDivision.Value = clsDevisionMaster.getFinder("", fndDivision.Value, isButtonClicked) 'clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", txtFromPayPeriodCode.Value, "PAY_PERIOD_CODE", isButtonClicked)
        lblDivision.Text = clsDevisionMaster.GetName(fndDivision.Value, Nothing)
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Bonus Summary Sheet ")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            arr.Add(" Location : " + fndLocation.Value)
        End If
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            arr.Add(" Division : " + fndDivision.Value)
        End If

        If clsCommon.myLen(txtFromPayPeriodCode.Value) > 0 Then
            arr.Add(" From Month : " + txtFromPayPeriodCode.Value)
        End If
        If clsCommon.myLen(txtToPayPeriodCode.Value) > 0 Then
            arr.Add(" To Month : " + txtToPayPeriodCode.Value)
        End If
        If gv1.Rows.Count <= 0 Then
            gv1.Focus()
            clsCommon.MyMessageBoxShow(Me, "Data not found.", Me.Text)
        Else
            clsCommon.MyExportToExcelGrid("Bonus Summary", gvBonusSummary, arr, "Bonus Summary", False)
        End If

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Bonus Detail ")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            arr.Add(" Location : " + fndLocation.Value)
        End If
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            arr.Add(" Division : " + fndDivision.Value)
        End If
        If clsCommon.myLen(txtFromPayPeriodCode.Value) > 0 Then
            arr.Add(" From Month : " + txtFromPayPeriodCode.Value)
        End If
        If clsCommon.myLen(txtToPayPeriodCode.Value) > 0 Then
            arr.Add(" To Month : " + txtToPayPeriodCode.Value)
        End If
        If gv1.Rows.Count <= 0 Then
            gv1.Focus()
            clsCommon.MyMessageBoxShow(Me, "Data not found.", Me.Text)
        Else
            clsCommon.MyExportToExcelGrid("Bonus Detail", gvBonusDetail, arr, "Bonus Detail", False)
        End If

    End Sub
End Class
