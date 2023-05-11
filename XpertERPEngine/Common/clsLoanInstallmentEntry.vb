Imports common
Imports System.Data.SqlClient
Public Class clsLoanInstallmentEntry
#Region "Variables"
    Public Installment_Code As String = Nothing
    Public Installment_Date As DateTime
    Public Against_Loan_Code As String = Nothing
    Public Installment_Amount As Decimal
    Public Remarks As String = Nothing
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime?
#End Region

    Public Function SaveData(ByVal obj As clsLoanInstallmentEntry, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal obj As clsLoanInstallmentEntry, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Installment_Date", clsCommon.GetPrintDate(obj.Installment_Date, "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Installment_Amount", obj.Installment_Amount)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Against_Loan_Code", obj.Against_Loan_Code)
                Dim LoadType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT Loan_Type FROM TSPL_LOAN_ENTRY WHERE Loan_Code='" + obj.Against_Loan_Code + "'", trans))
                obj.Installment_Code = clsERPFuncationality.GetNextCode(trans, obj.Installment_Date, clsDocType.LoanInstallmentEntry, IIf(clsCommon.CompairString(LoadType, "R") = CompairStringResult.Equal, clsDocTransactionType.LoanReceipt, clsDocTransactionType.LoanPayment), "")
                If (clsCommon.myLen(obj.Installment_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Installment_Code", obj.Installment_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_INSTALLMENT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_INSTALLMENT_ENTRY", OMInsertOrUpdate.Update, "TSPL_LOAN_INSTALLMENT_ENTRY.Installment_Code='" + obj.Installment_Code + "'", trans)
            End If
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType) As clsLoanInstallmentEntry
        Return GetData(strDocNo, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLoanInstallmentEntry
        Dim obj As clsLoanInstallmentEntry = Nothing
        Dim qry As String = "select TSPL_LOAN_INSTALLMENT_ENTRY.* from TSPL_LOAN_INSTALLMENT_ENTRY where 2=2 "
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LOAN_INSTALLMENT_ENTRY.Installment_Code = (select MIN(Installment_Code) from TSPL_LOAN_INSTALLMENT_ENTRY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_LOAN_INSTALLMENT_ENTRY.Installment_Code = (select Max(Installment_Code) from TSPL_LOAN_INSTALLMENT_ENTRY WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Next
                qry += " and TSPL_LOAN_INSTALLMENT_ENTRY.Installment_Code = (select Min(Installment_Code) from TSPL_LOAN_INSTALLMENT_ENTRY where Installment_Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_LOAN_INSTALLMENT_ENTRY.Installment_Code = (select Max(Installment_Code) from TSPL_LOAN_INSTALLMENT_ENTRY where Installment_Code<'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_LOAN_INSTALLMENT_ENTRY.Installment_Code = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLoanInstallmentEntry()
            obj.Installment_Code = clsCommon.myCstr(dt.Rows(0)("Installment_Code"))
            obj.Installment_Date = clsCommon.myCDate(dt.Rows(0)("Installment_Date"))
            obj.Against_Loan_Code = clsCommon.myCstr(dt.Rows(0)("Against_Loan_Code"))
            obj.Installment_Amount = clsCommon.myCdbl(dt.Rows(0)("Installment_Amount"))
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
            Dim obj As clsLoanInstallmentEntry = clsLoanInstallmentEntry.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Installment_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Post on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "Update TSPL_LOAN_INSTALLMENT_ENTRY set Status=1, Posted_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By='" + objCommonVar.CurrentUserCode + "' where Installment_Code='" + strDocNo + "'"
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
            Dim obj As clsLoanInstallmentEntry = clsLoanInstallmentEntry.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Installment_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If

            Dim qry As String = "delete from TSPL_LOAN_INSTALLMENT_ENTRY where Installment_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class


