' ----------------- Created By Anubhooti On 04-May-2015 BM00000006298 -------------------- '
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
Imports System.IO
Imports XpertERPEngine

Public Class FrmHRTravelExpenseApproval
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False

#Region "Variables"
    Const ColSNo As String = "S No."
    Const colApproved As String = "Approved"
    Const colRejected As String = "Rejected"
    Const colStatus As String = "Status"
    Const colEmpCode As String = "Emp Code"
    Const colEmpName As String = "Emp Name"
    Const colClaimCode As String = "Claim Code"
    Const colClaimName As String = "Claim Name"
    Const colClaimType As String = "Claim Type"
    Const colAppliedDate As String = "Applied Date"
    Const colLastActionDate As String = "Last Action Date"
    Const colTravelCategory As String = "Travel Category"
    Const colTotalAppliedAmt As String = "Total Applied Amount"
    Const colAppliedAmount As String = "Applied Amount"
    Const colApprovedAmount As String = "Approved Date"
    Const colHideApproved As String = "Hide Approved"
    Const colHideRejected As String = "Hide Rejected"
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRTravelReqApproval)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            isNewEntry = False

            Dim obj As New ClsHRTravelExpenseApproval()
            obj = ClsHRTravelExpenseApproval.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Expense_Code) > 0) Then

                isNewEntry = False
                btnSave.Text = "Update"
                'btnDelete.Enabled = True

                txtcode.Value = obj.Expense_Approval_Code
                TxtExpenseCode.Value = obj.Expense_Code
                dtpDate.Value = obj.Document_Date
                txtcode.MyReadOnly = True
                Dim ii As Int16 = 0
                LoadGrid(txtcode.Value, "")
            Else
                isNewEntry = True
                'Me.gv1.Rows.Clear()
                'Me.gv1.Rows.AddNew()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub LoadGrid(ByVal ApprovalCode As String, ByVal ExpenseCode As String)
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()

            Dim strquery As String = ""

            strquery += " SELECT TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.S_No AS [S No.], CAST(ISNULL(TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Approved,0) as Bit) AS [Approved],CAST(ISNULL(TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Rejected,0) as Bit) AS [Rejected],TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Claim_Code AS [Claim Code],TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Description AS [Claim Name],CASE WHEN TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Travel_Type ='D' THEN 'Domestic' WHEN TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Travel_Type ='I' THEN 'International' END AS [Claim Type],TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Emp_Code AS [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name AS [Employee Name],TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Applied_Date As [Applied Date],TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Last_Action_Date AS [Last Action Date],TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Applied_Amount AS [Applied Amount],TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Approved_Amount AS [Approved Amount], CAST(ISNULL(TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Approved,0) as Bit) AS [Hide Approved],CAST(ISNULL(TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Rejected,0) as Bit) AS [Hide Rejected],TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code As [Hide Expense Code] FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE " & _
                        " LEFT OUTER JOIN TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL ON TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code = TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Expense_Code " & _
                        " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Emp_Code = TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                        " LEFT OUTER JOIN TSPL_HR_REIMBURSEMENT_TYPE_MASTER ON TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Claim_Code =TSPL_HR_REIMBURSEMENT_TYPE_MASTER.Reimbursement_Code " & _
                        " LEFT OUTER JOIN TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL ON TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.Expense_Code=TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL.Expense_Code "
            If clsCommon.myLen(ApprovalCode) > 0 Then
                strquery += " WHERE TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.Expense_Approval_Code ='" & txtcode.Value & "'"
            End If
            If clsCommon.myLen(ExpenseCode) > 0 Then
                strquery += " WHERE TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code ='" & TxtExpenseCode.Value & "'"
            End If

            gv1.DataSource = clsDBFuncationality.GetDataTable(strquery)
            FormatGrid()

            btnSave.Enabled = True
            btnSave.Text = "Save"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub FormatGrid()
        If gv1.Rows.Count > 0 Then
            gv1.Columns("S No.").Width = 50
            gv1.Columns("S No.").ReadOnly = True
            gv1.Columns("Approved").Width = 75
            gv1.Columns("Approved").ReadOnly = False
            gv1.Columns("Rejected").Width = 75
            gv1.Columns("Rejected").ReadOnly = False
            gv1.Columns("Claim Code").Width = 75
            gv1.Columns("Claim Code").ReadOnly = True
            gv1.Columns("Claim Name").Width = 100
            gv1.Columns("Claim Name").ReadOnly = True
            gv1.Columns("Employee Code").Width = 100
            gv1.Columns("Employee Code").ReadOnly = True
            gv1.Columns("Employee Name").Width = 105
            gv1.Columns("Employee Name").ReadOnly = True
            gv1.Columns("Claim Type").Width = 75
            gv1.Columns("Claim Type").ReadOnly = True
            gv1.Columns("Applied Amount").Width = 100
            gv1.Columns("Applied Amount").ReadOnly = True
            gv1.Columns("Approved Amount").Width = 110
            gv1.Columns("Approved Amount").ReadOnly = True
            gv1.Columns("Applied Date").Width = 80
            gv1.Columns("Applied Date").FormatString = "{0:dd/MMM/yyyy}"
            gv1.Columns("Applied Date").ReadOnly = True
            gv1.Columns("Last Action Date").Width = 120
            gv1.Columns("Last Action Date").FormatString = "{0:dd/MMM/yyyy}"
            gv1.Columns("Last Action Date").ReadOnly = True
            gv1.Columns("Hide Approved").IsVisible = False
            gv1.Columns("Hide Rejected").IsVisible = False
            gv1.Columns("Hide Expense Code").IsVisible = False
            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableFiltering = True
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
        Else
            gv1.BestFitColumns()
        End If
    End Sub
    Function AllowToSave() As Boolean
        Try

            Dim GridRow As Integer = 0
            Dim LineNo As Integer = 0

            If clsCommon.myLen(txtcode.Value) <= 0 Then
                Throw New Exception("please fill code")
            End If
            For Each grow As GridViewRowInfo In gv1.Rows

                LineNo += 1
                If CBool(grow.Cells(colApproved).Value) = True OrElse CBool(grow.Cells(colRejected).Value) = True Then
                    GridRow = GridRow + 1
                End If
            Next

            If GridRow <= 0 Then
                Throw New Exception("Enter at least one reimbursement summary")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Public Sub SaveData()
        Try
            Dim currentdate As Date = Date.Today
            Dim IsApproved As Integer = 0
            Dim IsRejected As Integer = 0
            Dim qry1 As String = String.Empty
            Dim AR_Date As String = String.Empty
            Dim AR_By As String = String.Empty
            Dim Code As String = String.Empty

            If AllowToSave() Then

                For i As Integer = 0 To gv1.Rows.Count - 1
                    If CBool(gv1.Rows(i).Cells(colApproved).Value) = True Then
                        IsApproved = 1
                        IsRejected = 0
                        AR_Date = "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                        AR_By = "'" + objCommonVar.CurrentUserCode + "'"
                        Code = clsCommon.myCstr(gv1.Rows(i).Cells("Hide Expense Code").Value)

                        qry1 = "UPDATE TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL SET Approved =" + clsCommon.myCstr(IsApproved) + ", Approved_Date = " + AR_Date + " ,Approved_By = " + AR_By + " ,Rejected =" + clsCommon.myCstr(IsRejected) + ", Rejected_Date = " + AR_Date + " ,Rejected_By = " + AR_By + " WHERE Expense_Code ='" + Code + "' AND Claim_Code ='" & clsCommon.myCstr(gv1.Rows(i).Cells("Claim Code").Value) & "' AND Emp_Code='" & clsCommon.myCstr(gv1.Rows(i).Cells("Employee Code").Value) & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)

                    ElseIf CBool(gv1.Rows(i).Cells(colRejected).Value) = True Then
                        IsApproved = 0
                        IsRejected = 1
                        AR_Date = "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                        AR_By = "'" + objCommonVar.CurrentUserCode + "'"
                        Code = clsCommon.myCstr(gv1.Rows(i).Cells("Hide Expense Code").Value)

                        qry1 = "UPDATE TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL SET Approved =" + clsCommon.myCstr(IsApproved) + ", Approved_Date = " + AR_Date + " ,Approved_By = " + AR_By + " ,Rejected =" + clsCommon.myCstr(IsRejected) + ", Rejected_Date = " + AR_Date + " ,Rejected_By = " + AR_By + " WHERE Expense_Code ='" + Code + "' AND Claim_Code ='" & clsCommon.myCstr(gv1.Rows(i).Cells("Claim Code").Value) & "' AND Emp_Code='" & clsCommon.myCstr(gv1.Rows(i).Cells("Employee Code").Value) & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)

                    End If
                Next
                Dim arr As New List(Of ClsHRTravelExpenseApproval)
                Dim obj As New ClsHRTravelExpenseApproval()
                obj.Expense_Code = TxtExpenseCode.Value
                obj.Document_Date = dtpDate.Value
                obj.Expense_Approval_Code = txtcode.Value
                arr.Add(obj)
                If (ClsHRTravelExpenseApproval.SaveData(arr)) Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    LoadData(obj.Expense_Approval_Code, NavigatorType.Current)
                    btnSave.Text = "Update"
                    ' btnDelete.Enabled = True
                Else
                    btnSave.Text = "Save"
                    ' btnDelete.Enabled = False
                End If

                ' myMessages.insert()
                '  Reset()
            End If
            btnsave.Text = "Save"

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
#End Region
#Region "Events"
    Private Sub FrmHRTravelExpenseApproval_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        End If
    End Sub
    Private Sub FrmHRTravelExpenseApproval_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            SaveData()
        End If
    End Sub
    Private Sub TxtExpenseCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtExpenseCode._MYValidating
        TxtExpenseCode.Value = ClsHRTravelReimbursementExpense.GetFinder("", TxtExpenseCode.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtExpenseCode.Value) > 0 Then
            ' LblExpenseName.Text = clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE WHERE EMP_CODE='" + TxtExpenseCode.Value + "'")
            LoadGrid("", TxtExpenseCode.Value)
        Else
            'LblExpenseName.Text = ""
        End If
    End Sub
    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Reset()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtcode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL where Expense_Approval_Code='" + txtcode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtcode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtcode.MyReadOnly = False
            End If
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Try
            Dim str As String = "select count(*) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL where Expense_Code ='" + txtcode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 Then
                txtcode.MyReadOnly = False
            Else
                txtcode.MyReadOnly = True
            End If
            If txtcode.MyReadOnly OrElse isButtonClicked Then

                txtcode.Value = ClsHRTravelExpenseApproval.GetFinder("", txtcode.Value, isButtonClicked)
                If clsCommon.myLen(txtcode.Value) > 0 Then
                    ' btnDelete.Enabled = True
                    btnSave.Text = "Update"
                    txtcode.MyReadOnly = True
                Else
                    btnSave.Text = "Save"
                    '  btnDelete.Enabled = False
                    txtcode.MyReadOnly = False
                End If
            End If
            LoadData(txtcode.Value, NavigatorType.Current)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        If e.Column Is gv1.Columns(colApproved) OrElse e.Column Is gv1.Columns(colRejected) Then
            gv1.CurrentRow.Cells(colApproved).ReadOnly = False
            gv1.CurrentRow.Cells(colRejected).ReadOnly = False

            If CBool(gv1.CurrentRow.Cells(colApproved).Value) = True Then
                If CBool(gv1.CurrentRow.Cells(colHideApproved).Value) = True Then
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                    gv1.CurrentRow.Cells(colRejected).Value = 0
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                    gv1.CurrentRow.Cells(colRejected).Value = 0
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = False
                End If
            ElseIf CBool(gv1.CurrentRow.Cells(colRejected).Value) = True Then
                If CBool(gv1.CurrentRow.Cells(colHideRejected).Value) = True Then
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = True
                    gv1.CurrentRow.Cells(colApproved).Value = 0
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = True
                    gv1.CurrentRow.Cells(colApproved).Value = 0
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = False
                End If

            End If
        End If
    End Sub
    Private Sub gv1_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gv1.RowFormatting
        If e.RowElement.RowInfo.Cells(colApproved).Value = True Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LightGreen
        ElseIf e.RowElement.RowInfo.Cells(colRejected).Value = True Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LightSalmon
        Else
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.AliceBlue
        End If
    End Sub
#End Region
End Class
