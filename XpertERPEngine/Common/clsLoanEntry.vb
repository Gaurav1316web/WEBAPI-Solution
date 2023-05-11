Imports common
Imports System.Data.SqlClient
Public Class clsLoanEntry
#Region "Variables"
    Public Loan_Code As String = Nothing
    Public Loan_Date As DateTime
    Public Loan_Desc As String = Nothing
    Public Loan_On_Name As String = Nothing
    Public Loan_Type As String = Nothing
    Public Transaction_Type As String = Nothing
    Public Account_Code As String = Nothing
    Public Account_Name As String = Nothing
    Public Loan_Amount As Decimal
    Public Interest_Rate As Decimal
    Public Tenaure As Double = 0
    Public Installment_Amount As Decimal = 0
    Public Loan_Start_Date As Date
    Public Installment_Date_Of_Month As Integer = 0
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime?
#End Region

    Public Function SaveData(ByVal obj As clsLoanEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As clsLoanEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Loan_Date", clsCommon.GetPrintDate(obj.Loan_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Loan_Desc", obj.Loan_Desc)
            clsCommon.AddColumnsForChange(coll, "Loan_On_Name", obj.Loan_On_Name)

            clsCommon.AddColumnsForChange(coll, "Transaction_Type", obj.Transaction_Type)
            clsCommon.AddColumnsForChange(coll, "Account_Code", obj.Account_Code, True)
            clsCommon.AddColumnsForChange(coll, "Loan_Amount", obj.Loan_Amount)
            clsCommon.AddColumnsForChange(coll, "Interest_Rate", obj.Interest_Rate)
            clsCommon.AddColumnsForChange(coll, "Tenaure", obj.Tenaure)
            clsCommon.AddColumnsForChange(coll, "Installment_Amount", obj.Installment_Amount)
            clsCommon.AddColumnsForChange(coll, "Loan_Start_Date", clsCommon.GetPrintDate(obj.Loan_Start_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Installment_Date_Of_Month", obj.Installment_Date_Of_Month)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Loan_Type", obj.Loan_Type)
                obj.Loan_Code = clsERPFuncationality.GetNextCode(trans, obj.Loan_Date, clsDocType.LoanEntry, IIf(clsCommon.CompairString(obj.Loan_Type, "R") = CompairStringResult.Equal, clsDocTransactionType.LoanReceipt, clsDocTransactionType.LoanPayment), "")
                If (clsCommon.myLen(obj.Loan_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Loan_Code", obj.Loan_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_ENTRY", OMInsertOrUpdate.Update, "TSPL_LOAN_ENTRY.Loan_Code='" + obj.Loan_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsLoanEntry
        Return GetData(strDocNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLoanEntry
        Dim obj As clsLoanEntry = Nothing
        Dim qry As String = "select TSPL_LOAN_ENTRY.*,TSPL_GL_ACCOUNTS.Description as Account_Name from TSPL_LOAN_ENTRY " + Environment.NewLine + _
            " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_LOAN_ENTRY.Account_Code where  2=2"
        Dim whrCls As String = "  "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LOAN_ENTRY.Loan_Code = (select MIN(Loan_Code) from TSPL_LOAN_ENTRY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_LOAN_ENTRY.Loan_Code = (select Max(Loan_Code) from TSPL_LOAN_ENTRY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_LOAN_ENTRY.Loan_Code = (select Min(Loan_Code) from TSPL_LOAN_ENTRY where Loan_Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_LOAN_ENTRY.Loan_Code = (select Max(Loan_Code) from TSPL_LOAN_ENTRY where Loan_Code<'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_LOAN_ENTRY.Loan_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLoanEntry()
            obj.Loan_Code = clsCommon.myCstr(dt.Rows(0)("Loan_Code"))
            obj.Loan_Date = clsCommon.myCDate(dt.Rows(0)("Loan_Date"))
            obj.Loan_Desc = clsCommon.myCstr(dt.Rows(0)("Loan_Desc"))
            obj.Loan_On_Name = clsCommon.myCstr(dt.Rows(0)("Loan_On_Name"))
            obj.Loan_Type = clsCommon.myCstr(dt.Rows(0)("Loan_Type"))
            obj.Transaction_Type = clsCommon.myCstr(dt.Rows(0)("Transaction_Type"))
            obj.Account_Code = clsCommon.myCstr(dt.Rows(0)("Account_Code"))
            obj.Account_Name = clsCommon.myCstr(dt.Rows(0)("Account_Name"))
            obj.Loan_Amount = clsCommon.myCdbl(dt.Rows(0)("Loan_Amount"))
            obj.Interest_Rate = clsCommon.myCdbl(dt.Rows(0)("Interest_Rate"))
            obj.Tenaure = clsCommon.myCdbl(dt.Rows(0)("Tenaure"))
            obj.Installment_Amount = clsCommon.myCdbl(dt.Rows(0)("Installment_Amount"))
            obj.Loan_Start_Date = clsCommon.myCDate(dt.Rows(0)("Loan_Start_Date"))
            obj.Installment_Date_Of_Month = clsCommon.myCdbl(dt.Rows(0)("Installment_Date_Of_Month"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
        End If
        Return obj
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Entry No not found to Post")
            End If
            Dim obj As clsLoanEntry = clsLoanEntry.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Loan_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "Update TSPL_LOAN_ENTRY set Status=1, Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Loan_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Entry No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsLoanEntry = clsLoanEntry.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Loan_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "delete from TSPL_LOAN_ENTRY where Loan_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetLoanType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Receipt"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "P"
        dr("Name") = "Payment"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Public Shared Function GetTansactionType() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "L"
        dr("Name") = "Loan"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "S"
        dr("Name") = "Secured Loan"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "U"
        dr("Name") = "Unsecured Loan"
        dt.Rows.Add(dr)
        Return dt
    End Function
End Class


