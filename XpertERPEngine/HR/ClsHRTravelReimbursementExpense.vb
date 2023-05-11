Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsHRTravelReimbursementExpense
#Region "Variables"

    Public Expense_Code As String
    Public Document_Date As String = Nothing
    Public ObjList As List(Of ClsHRTravelReimbursementExpenseDetail) = Nothing
    Dim objDetail As New ClsHRTravelReimbursementExpenseDetail()
#End Region

    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.EXPENSE_CODE AS [Code],CONVERT (VARCHAR,Document_Date ,103) AS [Document Date] FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE "
        str = clsCommon.ShowSelectForm("HRTRE", qry, "Code", "", curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal arr As List(Of ClsHRTravelReimbursementExpense)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            If ClsHRTravelReimbursementExpense.SaveData(arr, trans) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsHRTravelReimbursementExpense), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim qry1 As String = Nothing
        Dim DBEmpty As String = ""
        Dim FCode As String = ""
        Try
            For Each obj As ClsHRTravelReimbursementExpense In arr
                Dim coll As New Hashtable()
                'If clsCommon.myLen(obj.Expense_Code) <= 0 Then
                '    qry1 = "select max(Expense_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE where Expense_Code='" + obj.Expense_Code + "'"
                '    Dim value As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1, trans))

                '    If clsCommon.myLen(value) <= 0 Then
                '        DBEmpty = "select max(Expense_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE"
                '        FCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue(DBEmpty, trans))
                '        If clsCommon.myLen(FCode) > 0 Then
                '            value = clsCommon.myCstr(clsCommon.incval(FCode))
                '        Else
                '            value = "E-001"
                '        End If

                '    Else
                '        value = clsCommon.myCstr(clsCommon.incval(FCode))
                '    End If
                '    obj.Expense_Code = value
                'End If
                clsCommon.AddColumnsForChange(coll, "Expense_Code", obj.Expense_Code)
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE WHERE Expense_Code='" + obj.Expense_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE where Expense_Code= '" & obj.Expense_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")

                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE", OMInsertOrUpdate.Update, "Expense_Code='" + obj.Expense_Code + "'", trans)
                End If
                ClsHRTravelReimbursementExpenseDetail.SaveData(obj.Expense_Code, obj.ObjList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim Applicant_Code As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT isnull(Expense_Code,'') As Expense_Code FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE WHERE Expense_Code='" + strCode + "'", trans))
            If clsCommon.myLen(Applicant_Code) > 0 Then
                Dim qry As String
                qry = "Delete From TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL Where Expense_Code ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "Delete From TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE Where Expense_Code ='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                If isSaved Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Else
                Throw New Exception("Code not found to delete")
            End If

        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRTravelReimbursementExpense
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsHRTravelReimbursementExpense
        Dim obj As ClsHRTravelReimbursementExpense = Nothing

        Dim qry As String = "Select * From TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code = (select MIN(Expense_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code = (select Max(Expense_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code = (select Min(Expense_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE where Expense_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code = (select Max(Expense_Code) from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE where Expense_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE.Expense_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHRTravelReimbursementExpense()
            obj.Expense_Code = clsCommon.myCstr(dt.Rows(0)("Expense_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))

            obj.ObjList = ClsHRTravelReimbursementExpenseDetail.GetData(obj.Expense_Code, trans)
        End If
        Return obj
    End Function
End Class
Public Class ClsHRTravelReimbursementExpenseDetail

#Region "Variables"
    Public Expense_Code As String = Nothing
    Public S_No As String = Nothing
    Public Emp_Code As String = Nothing
    Public Claim_Code As String = Nothing
    Public Claim_Type As String = Nothing
    Public Applied_Amount As Integer = 0
    Public Approved_Amount As Integer = 0
    Public Applied_Date As String = Nothing
    Public Last_Action_Date As String = Nothing
    Public Remarks As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL where Applicant_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of ClsHRTravelReimbursementExpenseDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL where Expense_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsHRTravelReimbursementExpenseDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Expense_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "S_No", obj.S_No)
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Claim_Code", obj.Claim_Code, True)
                clsCommon.AddColumnsForChange(coll, "Travel_Type", obj.Claim_Type, True)
                clsCommon.AddColumnsForChange(coll, "Applied_Amount", obj.Applied_Amount, True)
                clsCommon.AddColumnsForChange(coll, "Approved_Amount", obj.Approved_Amount, True)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
                clsCommon.AddColumnsForChange(coll, "Applied_Date", clsCommon.GetPrintDate(obj.Applied_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Last_Action_Date", clsCommon.GetPrintDate(obj.Last_Action_Date, "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsHRTravelReimbursementExpenseDetail)
        Dim obj As ClsHRTravelReimbursementExpenseDetail = Nothing
        Dim ObjList As New List(Of ClsHRTravelReimbursementExpenseDetail)
        Dim qry As String = " select *  from TSPL_HR_TRAVEL_REIMBURSEMENT_EXPENSE_DETAIL WHERE Expense_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsHRTravelReimbursementExpenseDetail()
                obj.S_No = clsCommon.myCstr(dr("S_No"))
                obj.Expense_Code = clsCommon.myCstr(dr("Expense_Code"))
                obj.Claim_Code = clsCommon.myCstr(dr("Claim_Code"))
                obj.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                obj.Claim_Type = clsCommon.myCstr(dr("Travel_Type"))
                obj.Applied_Amount = clsCommon.myCdbl(dr("Applied_Amount"))
                obj.Approved_Amount = clsCommon.myCdbl(dr("Approved_Amount"))
                obj.Applied_Date = clsCommon.myCstr(dr("Applied_Date"))
                obj.Last_Action_Date = clsCommon.myCstr(dr("Last_Action_Date"))
                obj.Remarks = clsCommon.myCstr(dr("Remarks"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
End Class