Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class FrmExpenseType
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmExpenseType)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmExpenseType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
    Private Sub FrmExpenseType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        
        RadPageView1.Pages("pvpCustomFields").Item.Visibility = MyBase.customFieldTabProperty
        If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
            UcCustomFields1.Report_ID = MyBase.Form_ID
            UcCustomFields1.LoadCustomControls()
        End If
        AddNew()
        Maxlength()
    End Sub
    Sub AddNew()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtCode.MyReadOnly = True
        fndAcctCode.MyReadOnly = True
        txtDesc.Text = ""
        fndAcctCode.Value = ""
        txtComments.Text = ""
        chkIntegrate.Checked = False
        txtAcctDesc.Text = ""
        LoadExpenseType()
    End Sub
    Sub Maxlength()
        txtComments.MaxLength = 200
        txtDesc.MaxLength = 200
    End Sub
    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)

    End Sub

    Private Sub fndAcctCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndAcctCode._MYValidating
        Dim qry As String = "select account_code as [AccountCode],description as [Description] from tspl_gl_accounts "
        fndAcctCode.Value = clsCommon.ShowSelectForm("GlAccount", qry, "AccountCode", "", fndAcctCode.Value, "", isButtonClicked)
        txtAcctDesc.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndAcctCode.Value + "' ")
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select expense_code as Code,description,expense_type as [Expense type],integrate_AP as [Integrate AP] from tspl_expense_master"
            'txtCode.Value = clsCommon.ShowSelectForm("ExpenseType", qry, "Code", "", txtCode.Value, "", isButtonClicked)
            txtCode.Value = clsExpenseType.getFinder("", txtCode.Value, isButtonClicked)
            LoadData(txtCode.Value, NavigatorType.Current)
        End If
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As clsExpenseType = clsExpenseType.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCode.Value = obj.EXPENSE_CODE
            txtDesc.Text = obj.DESCRIPTION
            If clsCommon.CompairString(obj.INTEGRATE_AP, "Y") = CompairStringResult.Equal Then
                chkIntegrate.Checked = True
            Else
                chkIntegrate.Checked = False
            End If
            fndAcctCode.Value = obj.GLACCOUNT
            txtAcctDesc.Text = clsDBFuncationality.getSingleValue("select description from tspl_gl_accounts where account_code='" + fndAcctCode.Value + "' ")
            txtComments.Text = obj.Remarks
            ddlExpenseType.Text = obj.EXPENSE_TYPE

            txtCode.MyReadOnly = True

            ''For Custom Fields
            If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                UcCustomFields1.LoadData(obj.EXPENSE_CODE)
            End If
            ''End of For Custom Fields
        End If
    End Sub
    Sub LoadExpenseType()
        ddlExpenseType.DataSource = clsExpenseType.GetExpenseTypeTable
        ddlExpenseType.DisplayMember = "Name"
        ddlExpenseType.ValueMember = "Code"
        ddlExpenseType.SelectedIndex = -1
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(clsCommon.myCstr(txtCode.Value)) <= 0 Then
            '    txtCode.Focus()
            '    Throw New Exception("Please Fill  Code")
            'End If

            If clsCommon.myLen(clsCommon.myCstr(txtDesc.Text)) <= 0 Then
                txtDesc.Focus()
                Throw New Exception("Please Fill  Description")
            End If
            If clsCommon.myLen(clsCommon.myCstr(ddlExpenseType.Text)) <= 0 Then
                ddlExpenseType.Focus()
                Throw New Exception("Please Select  Expense Type")
            End If
            If clsCommon.myLen(clsCommon.myCstr(fndAcctCode.Value)) <= 0 Then
                fndAcctCode.Focus()
                Throw New Exception("Please Select  Account Code")
            End If

            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Function
    Sub SaveData()
        Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then
                trans = clsDBFuncationality.GetTransactin()
                Dim obj As New clsExpenseType()
                obj.EXPENSE_CODE = txtCode.Value
                obj.DESCRIPTION = txtDesc.Text
                obj.EXPENSE_TYPE = ddlExpenseType.Text
                obj.GLACCOUNT = fndAcctCode.Value
                obj.Remarks = txtComments.Text
                obj.INTEGRATE_AP = IIf(chkIntegrate.Checked, "Y", "N")

                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(expense_code) from tspl_expense_master where expense_code='" + obj.EXPENSE_CODE + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If

                ''For Custom Fields
                obj.Form_ID = MyBase.Form_ID
                obj.arrCustomFields = New List(Of clsCustomFieldValues)
                If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                    UcCustomFields1.GetData(obj.arrCustomFields)
                End If
                ''End of For Custom Fields
                clsExpenseType.SaveData(obj, isNewEntry, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                LoadData(obj.EXPENSE_CODE, NavigatorType.Current)

            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub DeleteData()
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                Throw New Exception("  Code not found to delete")

            End If
            If clsCommon.MyMessageBoxShow("Do you want to delete  Code '" + txtCode.Value + "'", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "delete from tspl_expense_master where EXPENSE_CODE='" + txtCode.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '' custom fields
                clsCustomFieldValues.DeleteData(Me.Form_ID, txtCode.Value, trans)
                trans.Commit()
                clsCommon.MyMessageBoxShow("Successfully Deleted", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Expense Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Expense Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
            trans.Rollback()
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click

        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Code", "Description", "ExpenseType", "IntegrateAP", "Glaccount", "Remarks") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsExpenseType()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    obj.EXPENSE_CODE = strCode
                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(expense_code) from tspl_expense_master where expense_code='" + obj.EXPENSE_CODE + "'", trans)
                    If (qry = 0) Then
                        isNewEntry = True
                    Else
                        isNewEntry = False
                    End If

                    obj.GLACCOUNT = clsCommon.myCstr(grow.Cells("Glaccount").Value)
                    Dim strGLAcct As String = clsDBFuncationality.getSingleValue("select account_code as [AccountCode],description as [Description] from tspl_gl_accounts where account_code='" & obj.GLACCOUNT & "'", trans)
                    If clsCommon.myLen(strGLAcct) <= 0 Then
                        Throw New Exception("GL Account doesn't exist")
                    End If
                    obj.EXPENSE_TYPE = clsCommon.myCstr(grow.Cells("ExpenseType").Value)

                    If obj.EXPENSE_TYPE.Length > 30 Or (String.IsNullOrEmpty(obj.EXPENSE_TYPE)) Then
                        Throw New Exception("Expense Type can not be blank or incorrect.")
                    End If

                    obj.DESCRIPTION = clsCommon.myCstr(grow.Cells("Description").Value)
                    If obj.DESCRIPTION.Length > 200 Or (String.IsNullOrEmpty(obj.DESCRIPTION)) Then
                        Throw New Exception("Description Type can not be blank or incorrect.")
                    End If
                    obj.INTEGRATE_AP = clsCommon.myCstr(grow.Cells("IntegrateAP").Value)
                    If Not (clsCommon.CompairString(obj.INTEGRATE_AP, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.INTEGRATE_AP, "N") = CompairStringResult.Equal) Then
                        Throw New Exception("INTEGRATE with AP format is incorrect.")
                    End If
                    obj.Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)

                    clsExpenseType.SaveData(obj, isNewEntry, trans)


                Next
                trans.Commit()

                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click

        Dim str As String
        str = " select expense_code as Code,description as Description,expense_type as ExpenseType,integrate_AP as IntegrateAP,GLACCOUNT as Glaccount,REMARKS as Remarks from tspl_expense_master"
        transportSql.ExporttoExcel(str, Me)


    End Sub
End Class
