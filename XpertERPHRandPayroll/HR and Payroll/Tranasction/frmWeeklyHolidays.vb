'--01/07/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI

Public Class frmWeeklyHolidays
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colLineNo As String = "LineNo"
    Const colFSTWK_FSTHALF As String = "FSTWK_FSTHALF"
    Const colFSTWK_SECHALF As String = "FSTWK_SECHALF"

    Const colSECWK_FSTHALF As String = "SECWK_FSTHALF"
    Const colSECWK_SECHALF As String = "SECWK_SECHALF"

    Const colTHDWK_FSTHALF As String = "THDWK_FSTHALF"
    Const colTHDWK_SECHALF As String = "THDWK_SECHALF"

    Const colFTHWK_FSTHALF As String = "FTHWK_FSTHALF"
    Const colFTHWK_SECHALF As String = "FTHWK_SECHALF"

    Const colFIVWK_FSTHALF As String = "FIVWK_FSTHALF"
    Const colFIVWK_SECHALF As String = "FIVWK_SECHALF"

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        Try

            If AllowToSave() Then
                Dim obj As New clsWeeklyHolidays()

                obj.WKHOLIDAY_CODE = txtCode.Value
                obj.WKHOLIDAY_NAME = txtName.Text
                If clsCommon.myLen(dtpApplicableFrom.Text) > 0 Then
                    obj.APPLICABLE_FROM = clsCommon.GetPrintDate(dtpApplicableFrom.Value, "dd/MMM/yyyy")
                Else
                    obj.APPLICABLE_FROM = Nothing
                End If
                obj.APPLY_IN = CboapplyIn.SelectedText
                obj.APPLIED_ON = CboapplyIn.SelectedValue
                obj.WEEKDAY_NAME = CboSelectedDay.SelectedValue

                'obj.FSTWK_FSTHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colFSTWK_FSTHALF).Value)
                'obj.FSTWK_SECHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colFSTWK_SECHALF).Value)
                'obj.SECWK_FSTHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colSECWK_FSTHALF).Value)
                'obj.SECWK_SECHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colSECWK_SECHALF).Value)
                'obj.THDWK_FSTHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colTHDWK_FSTHALF).Value)
                'obj.THDWK_SECHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colTHDWK_SECHALF).Value)
                'obj.FTHWK_FSTHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colFTHWK_FSTHALF).Value)
                'obj.FTHWK_SECHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colFTHWK_SECHALF).Value)
                'obj.FIVWK_FSTHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colFIVWK_FSTHALF).Value)
                'obj.FIVWK_SECHALF = clsCommon.myCBool(gv1.Rows(0).Cells(colFIVWK_SECHALF).Value)

                obj.FSTWK_FSTHALF = True
                obj.FSTWK_SECHALF = True
                obj.SECWK_FSTHALF = True
                obj.SECWK_SECHALF = True
                obj.THDWK_FSTHALF = True
                obj.THDWK_SECHALF = True
                obj.FTHWK_FSTHALF = True
                obj.FTHWK_SECHALF = True
                obj.FIVWK_FSTHALF = True
                obj.FIVWK_SECHALF = True

                '==============
                obj.Location_Code = fndLocation.Value
                obj.Division = fndDivision.Value
                obj.arr = txtEmp.arrValueMember
                If (obj.SaveData(obj, obj.WKHOLIDAY_CODE, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.WKHOLIDAY_CODE, NavigatorType.Current)
                    'Else
                    '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
                End If

            End If

        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsWeeklyHolidays()
        obj = clsWeeklyHolidays.GetData(strCode, NavTyep)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.WKHOLIDAY_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.WKHOLIDAY_CODE
            txtName.Text = obj.WKHOLIDAY_NAME
            dtpApplicableFrom.Value = clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy")
            lblApplyOn.Text = obj.APPLIED_ON
            CboapplyIn.SelectedValue = obj.APPLIED_ON
            CboSelectedDay.SelectedValue = obj.WEEKDAY_NAME
            '=====================
            fndLocation.Value = obj.Location_Code
            fndDivision.Value = obj.Division
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code ='" & fndLocation.Value & "'")
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER where DEVISION_CODE ='" & fndDivision.Value & "'")
            txtEmp.arrValueMember = obj.arr
            LoadGridColumns()
            ' gv1.Rows.AddNew()

            gv1.Rows(0).Cells(colFSTWK_FSTHALF).Value = clsCommon.myCBool(obj.FSTWK_FSTHALF)
            gv1.Rows(0).Cells(colFSTWK_SECHALF).Value = clsCommon.myCBool(obj.FSTWK_SECHALF)
            gv1.Rows(0).Cells(colSECWK_FSTHALF).Value = clsCommon.myCBool(obj.SECWK_FSTHALF)
            gv1.Rows(0).Cells(colSECWK_SECHALF).Value = clsCommon.myCBool(obj.SECWK_SECHALF)
            gv1.Rows(0).Cells(colTHDWK_FSTHALF).Value = clsCommon.myCBool(obj.THDWK_FSTHALF)
            gv1.Rows(0).Cells(colTHDWK_SECHALF).Value = clsCommon.myCBool(obj.THDWK_SECHALF)
            gv1.Rows(0).Cells(colFTHWK_FSTHALF).Value = clsCommon.myCBool(obj.FTHWK_FSTHALF)
            gv1.Rows(0).Cells(colFTHWK_SECHALF).Value = clsCommon.myCBool(obj.FTHWK_SECHALF)
            gv1.Rows(0).Cells(colFIVWK_FSTHALF).Value = clsCommon.myCBool(obj.FIVWK_FSTHALF)
            gv1.Rows(0).Cells(colFIVWK_SECHALF).Value = clsCommon.myCBool(obj.FIVWK_SECHALF)

        End If
    End Sub

    Function AllowToSave() As Boolean

        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'End If
        Dim II As Int16 = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            II = II + 1

            'If clsCommon.myCdbl(grow.Cells(colAllotedLeave).Value) > 366 Then
            '    clsCommon.MyMessageBoxShow("Value of ")
            'End If

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
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If

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
                If (clsWeeklyHolidays.DeleteData(txtCode.Value)) Then
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

    Private Sub frmWeeklyHolidays_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadGridColumns()
        Me.dtpApplicableFrom.Value = clsCommon.GETSERVERDATE
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        funReset()
        CboapplyIn.Text = "Company"
        Me.CboapplyIn.Enabled = False
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
        gv1.Visible = False
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmWeeklyHolidays)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtName.Text = ""
        lblApplyOn.Text = ""
        txtEmp.arrValueMember = Nothing
        dtpApplicableFrom.Value = clsCommon.GETSERVERDATE()
        CboapplyIn.DataSource = GetCboApplyInDataTable()
        CboapplyIn.ValueMember = "Code"
        CboapplyIn.DisplayMember = "Name"
        CboapplyIn.SelectedIndex = -1

        CboSelectedDay.DataSource = GetCboDayDataTable()
        CboSelectedDay.ValueMember = "Code"
        CboSelectedDay.DisplayMember = "Name"
        CboSelectedDay.SelectedIndex = -1
        fndLocation.Value = ""
        fndDivision.Value = ""
        lblLocationName.Text = ""
        lblDivisionName.Text = ""
        LoadGridColumns()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_WEEKLY_HOLIDAYS where WKHOLIDAY_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = " select WKHOLIDAY_CODE as Code,  WKHOLIDAY_NAME as 'Nmae', APPLICABLE_FROM as 'Applicable From',APPLY_IN as 'Apply In', APPLIED_ON as 'Applied On',WEEKDAY_NAME as 'Weekday Name'  from TSPL_WEEKLY_HOLIDAYS "
            txtCode.Value = clsCommon.ShowSelectForm("WEEKLY_HOLIDAYS", qry, "Code", "", txtCode.Value, "WKHOLIDAY_CODE", isButtonClicked)
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

    Private Sub frmWeeklyHolidays_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub LoadGridColumns()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.ReadOnly = False

        'Dim lineNo As GridViewTextBoxColumn
        'lineNo = New GridViewTextBoxColumn()
        'lineNo.FormatString = ""
        'lineNo.HeaderText = "Line No"
        'lineNo.Name = colLineNo
        'lineNo.Width = 50
        'lineNo.ReadOnly = False
        'lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        'gv1.Columns.Add(lineNo)

        Dim FSTWK_FSTHALF As GridViewCheckBoxColumn
        FSTWK_FSTHALF = New GridViewCheckBoxColumn()
        FSTWK_FSTHALF.HeaderText = "Ist Half"
        FSTWK_FSTHALF.Name = colFSTWK_FSTHALF
        FSTWK_FSTHALF.Width = 100
        FSTWK_FSTHALF.ReadOnly = False
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        FSTWK_FSTHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FSTWK_FSTHALF)

        Dim FSTWK_SECHALF As GridViewCheckBoxColumn
        FSTWK_SECHALF = New GridViewCheckBoxColumn()
        FSTWK_SECHALF.HeaderText = "IInd Half"
        FSTWK_SECHALF.Name = colFSTWK_SECHALF
        FSTWK_SECHALF.Width = 100
        FSTWK_SECHALF.ReadOnly = False
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        FSTWK_SECHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FSTWK_SECHALF)

        Dim SECWK_FSTHALF As GridViewCheckBoxColumn
        SECWK_FSTHALF = New GridViewCheckBoxColumn()
        SECWK_FSTHALF.HeaderText = "Ist Half"
        SECWK_FSTHALF.Name = colSECWK_FSTHALF
        SECWK_FSTHALF.Width = 100
        SECWK_FSTHALF.ReadOnly = False
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        SECWK_FSTHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SECWK_FSTHALF)

        Dim SECWK_SECHALF As GridViewCheckBoxColumn
        SECWK_SECHALF = New GridViewCheckBoxColumn()
        SECWK_SECHALF.FormatString = ""
        SECWK_SECHALF.HeaderText = "IInd Half"
        SECWK_SECHALF.Name = colSECWK_SECHALF
        SECWK_SECHALF.Width = 100
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        SECWK_SECHALF.ReadOnly = False
        SECWK_SECHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(SECWK_SECHALF)

        Dim THDWK_FSTHALF As GridViewCheckBoxColumn
        THDWK_FSTHALF = New GridViewCheckBoxColumn()
        THDWK_FSTHALF.FormatString = ""
        THDWK_FSTHALF.HeaderText = "Ist Half"
        THDWK_FSTHALF.Name = colTHDWK_FSTHALF
        THDWK_FSTHALF.Width = 100
        THDWK_FSTHALF.ReadOnly = False
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        THDWK_FSTHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(THDWK_FSTHALF)

        Dim THDWK_SECHALF As GridViewCheckBoxColumn
        THDWK_SECHALF = New GridViewCheckBoxColumn()
        THDWK_SECHALF.FormatString = ""
        THDWK_SECHALF.HeaderText = "IInd Half"
        THDWK_SECHALF.Name = colTHDWK_SECHALF
        THDWK_SECHALF.Width = 100
        THDWK_SECHALF.ReadOnly = False
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        THDWK_SECHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(THDWK_SECHALF)

        Dim FTHWK_FSTHALF As GridViewCheckBoxColumn
        FTHWK_FSTHALF = New GridViewCheckBoxColumn()
        FTHWK_FSTHALF.FormatString = ""
        FTHWK_FSTHALF.HeaderText = "Ist Half"
        FTHWK_FSTHALF.Name = colFTHWK_FSTHALF
        FTHWK_FSTHALF.Width = 100
        FTHWK_FSTHALF.ReadOnly = False
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        FTHWK_FSTHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FTHWK_FSTHALF)

        Dim FTHWK_SECHALF As GridViewCheckBoxColumn
        FTHWK_SECHALF = New GridViewCheckBoxColumn()
        FTHWK_SECHALF.FormatString = ""
        FTHWK_SECHALF.HeaderText = "IInd Half"
        FTHWK_SECHALF.Name = colFTHWK_SECHALF
        FTHWK_SECHALF.Width = 100
        FTHWK_SECHALF.ReadOnly = False
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        FTHWK_SECHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FTHWK_SECHALF)

        Dim FIVWK_FSTHALF As GridViewCheckBoxColumn
        FIVWK_FSTHALF = New GridViewCheckBoxColumn()
        FIVWK_FSTHALF.FormatString = ""
        FIVWK_FSTHALF.HeaderText = "Ist Half"
        FIVWK_FSTHALF.Name = colFIVWK_FSTHALF
        FIVWK_FSTHALF.Width = 100
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        FIVWK_FSTHALF.ReadOnly = False
        FIVWK_FSTHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FIVWK_FSTHALF)

        Dim FIVWK_SECHALF As GridViewCheckBoxColumn
        FIVWK_SECHALF = New GridViewCheckBoxColumn()
        FIVWK_SECHALF.FormatString = ""
        FIVWK_SECHALF.HeaderText = "IInd Half"
        FIVWK_SECHALF.Name = colFIVWK_SECHALF
        FIVWK_SECHALF.Width = 100
        FSTWK_FSTHALF.DataType = GetType(Boolean)
        FIVWK_SECHALF.ReadOnly = False
        FIVWK_SECHALF.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FIVWK_SECHALF)
        gv1.Rows.AddNew()

        gv1.ReadOnly = False

        Dim view As New ColumnGroupsViewDefinition()
        view.ColumnGroups.Add(New GridViewColumnGroup("Ist Week"))
        view.ColumnGroups.Add(New GridViewColumnGroup("IInd Week"))
        view.ColumnGroups.Add(New GridViewColumnGroup("IIIrd Week"))
        view.ColumnGroups.Add(New GridViewColumnGroup("IVth Week"))
        view.ColumnGroups.Add(New GridViewColumnGroup("Vth Week"))

        view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colFSTWK_FSTHALF).Name)
        view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colFSTWK_SECHALF).Name)

        view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
        view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colSECWK_FSTHALF).Name)
        view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colSECWK_SECHALF).Name)

        view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
        view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colTHDWK_FSTHALF).Name)
        view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colTHDWK_SECHALF).Name)

        view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
        view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colFTHWK_FSTHALF).Name)
        view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colFTHWK_SECHALF).Name)

        view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
        view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colFIVWK_FSTHALF).Name)
        view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv1.Columns(colFIVWK_SECHALF).Name)

        gv1.ViewDefinition = view

    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Public Shared Function GetCboApplyInDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "Company"
        DR("Code") = "COMPANY"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Branch"
        DR("Code") = "BRANCH"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Grade"
        DR("Code") = "GRADE"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Designation"
        DR("Code") = "DESIGNATION"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Employee"
        DR("Code") = "EMPLOYEE"
        DT.Rows.Add(DR)

        DT.AcceptChanges()
        Return DT
    End Function
    Public Shared Function GetCboDayDataTable() As DataTable
        Dim DT As DataTable = New DataTable
        DT.Columns.Add("Code", GetType(String))
        DT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT.NewRow()
        DR("Name") = "Sunday"
        DR("Code") = "SUNDAY"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Monday"
        DR("Code") = "MONDAY"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Tuesday"
        DR("Code") = "TUESDAY"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Wednesday"
        DR("Code") = "WEDNESDAY"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Thursday"
        DR("Code") = "THURSDAY"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Friday"
        DR("Code") = "FRIDAY"
        DT.Rows.Add(DR)

        DR = DT.NewRow()
        DR("Name") = "Saturday"
        DR("Code") = "SATURDAY"
        DT.Rows.Add(DR)
        DT.AcceptChanges()
        Return DT
    End Function
    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocation.Value, isButtonClicked)
        lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
    End Sub
    Private Sub fndDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivision._MYValidating
        fndDivision.Value = clsDevisionMaster.getFinder("", Me.fndDivision.Value, isButtonClicked)
        lblDivisionName.Text = clsDevisionMaster.GetName(fndDivision.Value, Nothing)
    End Sub

    Private Sub txtEmp__My_Click(sender As Object, e As EventArgs) Handles txtEmp._My_Click
        Try
            Dim qry As String

            qry = "Select TSPL_EMPLOYEE_MASTER.EMP_CODE As [Employee Code] ,TSPL_EMPLOYEE_MASTER.Emp_Name As [Employee Name] from TSPL_EMPLOYEE_MASTER where TSPL_EMPLOYEE_MASTER.Emp_Status='Active'"

            txtEmp.arrValueMember = clsCommon.ShowMultipleSelectForm("EMP@WH1", qry, "Employee Code", "Employee Name", txtEmp.arrValueMember, txtEmp.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
