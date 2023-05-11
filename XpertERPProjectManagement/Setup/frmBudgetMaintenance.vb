Imports common
Imports System.Data.SqlClient

Public Class FrmBudgetMaintenance
    Inherits FrmMainTranScreen
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()


#End Region

    Private Sub FrmBudgetMaintenance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmBudgetMaintenance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnnew, "Press Alt+N Adding New ")
        maxLength()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBudgetMaintenance)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub Save()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New clsBudgetMaintenace()
                obj.FROM_DATE = txtFromDate.Value
                obj.TO_DATE = txtToDate.Text
                obj.PROJECT_CODE = fndProject.Value
                obj.SPECIFICATION = txtProjectdesc.Text
                obj.Budget = txtBudget.Text
                obj.Remarks = txtComments.Text

                If (obj.SaveData(obj, isNewEntry, trans)) Then
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If

            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
            clsCommon.MyMessageBoxShow("From date should not be greater than To date")
            txtFromDate.Focus()
            Return False
        ElseIf clsCommon.myLen(fndProject.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Project Code")
            fndProject.Focus()
            Return False
        ElseIf clsCommon.myLen(txtBudget.Text) = 0 Then
            clsCommon.MyMessageBoxShow("Please enter Budget")
            txtBudget.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub fndProject__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProject._MYValidating
        Qry = "select PROJECT_CODE,SPECIFICATION,PROJECT_STATUS,Cust_Code from TSPL_PJC_PROJECT "
        fndProject.Value = clsCommon.ShowSelectForm("Project", Qry, "PROJECT_CODE", "", fndProject.Value.ToString, "PROJECT_CODE", isButtonClicked)
        txtProjectdesc.Text = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" & fndProject.Value & "'")
        If clsCommon.myLen(fndProject.Value) > 0 Then
            LoadData()
        End If
    End Sub

    Sub LoadData()
        Qry = "select * from TSPL_BUDGET_MAINTENANCE where PROJECT_CODE='" + fndProject.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtBudget.Text = clsCommon.myCstr(dt.Rows(0)("Budget"))
            txtProjectdesc.Text = clsCommon.myCstr(dt.Rows(0)("SPECIFICATION"))
            txtFromDate.Value = clsCommon.myCstr(dt.Rows(0)("FROM_DATE"))
            txtToDate.Value = clsCommon.myCstr(dt.Rows(0)("TO_DATE"))
            txtComments.Text = clsCommon.myCstr(dt.Rows(0)("Remarks"))
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndProject.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsBudgetMaintenace.DeleteData(fndProject.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Budget Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub funReset()
        isNewEntry = True

        fndProject.Value = Nothing
        fndProject.Focus()
        txtBudget.Text = ""
        txtProjectdesc.Text = ""
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtComments.Text = ""
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
    End Sub
    Sub maxLength()
        txtBudget.MaxLength = 10
        txtComments.MaxLength = 200
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Private Sub rdbtnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbtnnew.Click
        funReset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
    

        Dim str As String
        str = " Select PROJECT_CODE as Code ,SPECIFICATION as Description,FROM_DATE as FromDate,TO_DATE as ToDate,Budget,Remarks from TSPL_BUDGET_MAINTENANCE "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today

        If transportSql.importExcel(gv, "Code", "Description", "FromDate", "ToDate", "Budget", "Remarks") Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsBudgetMaintenace()

                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    Dim Str As String = "Select PROJECT_CODE from TSPL_PJC_PROJECT where PROJECT_CODE='" & strCode & "'"
                    Dim check As String = clsDBFuncationality.getSingleValue(Str, trans)

                    If (String.IsNullOrEmpty(check)) Then
                        Throw New Exception("Project Code doesn't exist.")
                    End If
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.PROJECT_CODE = strCode

                    Dim strName As String = clsDBFuncationality.getSingleValue("select SPECIFICATION from TSPL_PJC_PROJECT where PROJECT_CODE='" & strCode & "'", trans)

                    If strName.Length > 200 Or (String.IsNullOrEmpty(strName)) Then
                        Throw New Exception("Description can not be blank or incorrect.")
                    End If
                    obj.SPECIFICATION = strName
                    obj.Budget = clsCommon.myCstr(grow.Cells("Budget").Value)
                    obj.FROM_DATE = clsCommon.myCstr(grow.Cells("FromDate").Value)
                    obj.TO_DATE = clsCommon.myCstr(grow.Cells("ToDate").Value)
                    obj.Remarks = clsCommon.myCstr(grow.Cells("Remarks").Value)

                    obj.SaveData(obj, isNewEntry, trans)


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
End Class
