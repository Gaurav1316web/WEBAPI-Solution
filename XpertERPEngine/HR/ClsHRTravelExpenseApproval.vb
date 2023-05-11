Imports common
Imports System.Data
Imports System.Data.SqlClient


Public Class ClsHRTravelExpenseApproval
#Region "Variables"
    Public Expense_Approval_Code As String
    Public Expense_Code As String
    Public Document_Date As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.EXPENSE_APPROVAL_CODE AS [Code],Expense_Code AS [Expense Code],CONVERT (VARCHAR,Document_Date ,103) AS [Document Date] FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL "
        str = clsCommon.ShowSelectForm("HRTRAPP", qry, "Code", "", curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal arr As List(Of ClsHRTravelExpenseApproval)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If ClsHRTravelExpenseApproval.SaveData(arr, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsHRTravelExpenseApproval), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim qry1 As String = Nothing
        Dim DBEmpty As String = ""
        Dim FCode As String = ""
        Try
            For Each obj As ClsHRTravelExpenseApproval In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Expense_Approval_Code", obj.Expense_Approval_Code)
                clsCommon.AddColumnsForChange(coll, "Expense_Code", obj.Expense_Code)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL WHERE Expense_Approval_Code='" + obj.Expense_Approval_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL where Expense_Approval_Code= '" & obj.Expense_Approval_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")
                    End If
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL", OMInsertOrUpdate.Update, "Expense_Approval_Code='" + obj.Expense_Approval_Code + "'", trans)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRTravelExpenseApproval
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsHRTravelExpenseApproval
        Dim obj As ClsHRTravelExpenseApproval = Nothing

        Dim qry As String = "Select * From TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.Expense_Approval_Code = (select MIN(Expense_Approval_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.Expense_Approval_Code = (select Max(Expense_Approval_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.Expense_Approval_Code = (select Min(Expense_Approval_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL where Expense_Approval_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.Expense_Approval_Code = (select Max(Expense_Approval_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL where Expense_Approval_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_APPROVAL.Expense_Approval_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRTravelExpenseApproval()
            obj.Expense_Approval_Code = clsCommon.myCstr(dt.Rows(0)("Expense_Approval_Code"))
            obj.Expense_Code = clsCommon.myCstr(dt.Rows(0)("Expense_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))

            'obj.ObjList = ClsHRTravelReimbursementExpenseDetail.GetData(obj.Expense_Code, trans)
        End If
        Return obj
    End Function
End Class

