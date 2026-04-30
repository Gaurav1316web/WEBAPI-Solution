Imports common
Imports System.Data.SqlClient

Public Class ClsEmpOTCalculation

#Region "Variables"
    Public Document_Code As String = Nothing
    Public Remarks As String
    Public Document_Date As DateTime
    Public From_Date As DateTime
    Public To_Date As DateTime
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime?
    Public Arr As List(Of ClsEmpOTEntryDetailCalculationData) = Nothing
    Public ArrDT As DataTable = Nothing
    Public Arr_Calc As List(Of ClsEmpOTEntryDetailCalculation) = Nothing
#End Region


    Public Function SaveData(ByVal obj As ClsEmpOTCalculation, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = obj.SaveData(obj, isNewEntry, trans)
            If (isSaved) Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As ClsEmpOTCalculation, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True

        Dim qry As String = "delete from TSPL_EMPLOYEE_OT_CALCULATION_DETAILS where Document_Code='" + obj.Document_Code + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Dim qry1 As String = "delete from TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY where Document_Code='" + obj.Document_Code + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry1, trans)

        If (isNewEntry) Then
            obj.Document_Code = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(obj.Document_Date), clsDocType.EmployeeOTEntryCalculation, "", Nothing)
        End If
        If (clsCommon.myLen(obj.Document_Code) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If


        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
        clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy hh:mm tt"))
        clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
        clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", OMInsertOrUpdate.Insert, "", trans)
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", OMInsertOrUpdate.Update, "Document_Code='" + obj.Document_Code + "'", trans)
        End If

        isSaved = isSaved AndAlso ClsEmpOTEntryDetailCalculationData.SaveData(obj.Document_Code, Arr, trans)
        isSaved = isSaved AndAlso ClsEmpOTEntryDetailCalculation.SaveData(obj.Document_Code, Arr_Calc, trans)

        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", "Document_Code", "TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY", "Document_Code", "TSPL_EMPLOYEE_OT_CALCULATION_DETAILS", "Document_Code", trans)

        Return isSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If

        Dim obj As ClsEmpOTCalculation = ClsEmpOTCalculation.GetData(strDocNo, NavigatorType.Current, False, trans)
        clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", "Document_Code", "TSPL_EMPLOYEE_OT_CALCULATION_DETAILS", "Document_Code", "TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY", "Document_Code", trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", "Document_Code", "TSPL_EMPLOYEE_OT_CALCULATION_DETAILS", "Document_Code", "TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY", "Document_Code", trans)

        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
            Try
                If obj.Status = ERPTransactionStatus.Approved Then
                    Throw New Exception("Already Post on :" + obj.Posted_Date)
                End If
                Dim qry As String = "delete from TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY where Document_Code='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_EMPLOYEE_OT_CALCULATION_DETAILS where Document_Code='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "delete from TSPL_EMPLOYEE_OT_CALCULATION_HEAD where Document_Code='" + strDocNo + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = ClsEmpOTCalculation.PostData(strDocNo, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""
        If (clsCommon.myLen(strDocNo) <= 0) Then
            Throw New Exception("Document No not found to Post")
        End If

        Dim obj As ClsEmpOTCalculation = ClsEmpOTCalculation.GetData(strDocNo, NavigatorType.Current, False, trans)

        If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
            Throw New Exception("No Data found to Post")
        End If

        If (clsCommon.myLen(obj.Posted_Date) > 0) Then
            Throw New Exception("Already Post on :" + obj.Posted_Date)
        End If

        'CreateAPInvoiceHeader(obj, trans)
        qry = "Update TSPL_EMPLOYEE_OT_CALCULATION_HEAD set Posted_Date='" + clsCommon.GetPrintDate(clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), "dd/MMM/yyyy hh:mm:ss tt") + "',Status=1 ,Posted_By='" + objCommonVar.CurrentUserCode + "' where Document_Code='" + strDocNo + "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_EMPLOYEE_OT_CALCULATION_HEAD", "Document_Code", trans)

        Return True

    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "Select Posted_Date from TSPL_EMPLOYEE_OT_CALCULATION_HEAD WHERE Document_Code='" + strDocNo + "'"
            If clsCommon.myLen(clsDBFuncationality.getSingleValue(Qry, trans)) <= 0 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim obj As ClsEmpOTCalculation = ClsEmpOTCalculation.GetData(strDocNo, NavigatorType.Current, False, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Reverse And UnPost")
            End If

            Qry = "Update TSPL_EMPLOYEE_OT_CALCULATION_HEAD set Posted_By=null,Posted_Date=NULL, Modify_By='" + objCommonVar.CurrentUserCode + "',Status=0 where Document_Code='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType, ByVal isGetDT As Boolean, ByVal trans As SqlTransaction) As ClsEmpOTCalculation
        Dim obj As ClsEmpOTCalculation = Nothing

        Dim qry As String = "Select TSPL_EMPLOYEE_OT_CALCULATION_HEAD.* from TSPL_EMPLOYEE_OT_CALCULATION_HEAD where 2=2 "
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_EMPLOYEE_OT_CALCULATION_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Document_Code = (select Max(Document_Code) from TSPL_EMPLOYEE_OT_CALCULATION_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Document_Code = (select Min(Document_Code) from TSPL_EMPLOYEE_OT_CALCULATION_HEAD where Document_Code>'" + strDocumentNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Document_Code = (select Max(Document_Code) from TSPL_EMPLOYEE_OT_CALCULATION_HEAD where Document_Code<'" + strDocumentNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_EMPLOYEE_OT_CALCULATION_HEAD.Document_Code = '" + strDocumentNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsEmpOTCalculation()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.From_Date = clsCommon.myCstr(dt.Rows(0)("From_Date"))
            obj.To_Date = clsCommon.myCstr(dt.Rows(0)("To_Date"))
            obj.Status = IIf(clsCommon.myCdbl(dt.Rows(0)("Status")) = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)

            qry = "Select TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.Document_Code,TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.PK_ID,TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.Increment_DA,TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.Increment_Basic,TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.Amount,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.PK_ID as Ref_PKID,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.EMP_CODE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DATE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_HOURS,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_TYPE,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_BASIC,TSPL_EMPLOYEE_OT_ENTRY_DETAIL.OT_DA from TSPL_EMPLOYEE_OT_CALCULATION_DETAILS 
                    left outer join TSPL_EMPLOYEE_OT_ENTRY_DETAIL on TSPL_EMPLOYEE_OT_ENTRY_DETAIL.PK_ID = TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.REF_PK_ID
                   where TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.Document_Code='" + obj.Document_Code + "' ORDER BY TSPL_EMPLOYEE_OT_CALCULATION_DETAILS.PK_ID"
            obj.ArrDT = clsDBFuncationality.GetDataTable(qry, trans)

            If (obj.ArrDT IsNot Nothing AndAlso obj.ArrDT.Rows.Count > 0) Then
                obj.Arr_Calc = New List(Of ClsEmpOTEntryDetailCalculation)
                For Each dr As DataRow In obj.ArrDT.Rows
                    Dim objTr As New ClsEmpOTEntryDetailCalculation
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.PK_ID = clsCommon.myCstr(dr("Ref_PKID"))
                    objTr.Emp_Code = clsCommon.myCstr(dr("EMP_CODE"))
                    objTr.OT_Date = clsCommon.myCDate(dr("OT_DATE"))
                    objTr.OT_Type = clsCommon.myCstr(dr("OT_TYPE"))
                    objTr.OT_Hours = clsCommon.myCdbl(dr("OT_HOURS"))
                    objTr.OT_Basic = clsCommon.myCdbl(dr("OT_BASIC"))
                    objTr.OT_DA = clsCommon.myCdbl(dr("OT_DA"))
                    objTr.OT_Incremented_Basic = clsCommon.myCdbl(dr("Increment_Basic"))
                    objTr.OT_Incremented_DA = clsCommon.myCdbl(dr("Increment_DA"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    obj.Arr_Calc.Add(objTr)
                Next
            End If


            qry = " Select TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY.* from TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY 
                    where TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY.Document_Code='" + obj.Document_Code + "' "
            obj.ArrDT = clsDBFuncationality.GetDataTable(qry, trans)
            If (obj.ArrDT IsNot Nothing AndAlso obj.ArrDT.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsEmpOTEntryDetailCalculationData)
                For Each dr As DataRow In obj.ArrDT.Rows
                    Dim objTr As New ClsEmpOTEntryDetailCalculationData
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Emp_Code = clsCommon.myCstr(dr("EMP_CODE"))
                    objTr.OT_Hours = clsCommon.myCdbl(dr("OT_HOURS"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    obj.Arr.Add(objTr)
                Next
            End If


        End If
        Return obj
    End Function

End Class

Public Class ClsEmpOTEntryDetailCalculationData
#Region "Variables"
    Public PK_ID As Integer = 0
    Public Document_Code As String = Nothing
    Public Emp_Code As String = Nothing
    Public Emp_Name As String = Nothing
    Public OT_Date As DateTime
    Public OT_Type As String = Nothing
    Public OT_Hours As Decimal = 0
    Public OT_Basic As Decimal = 0
    Public OT_DA As Decimal = 0
    Public Amount As Decimal = 0
    'Public arr As List(Of clsfrmVLCMaster) = Nothing

    'Public BalanceAmount As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsEmpOTEntryDetailCalculationData), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsEmpOTEntryDetailCalculationData In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "OT_HOURS", obj.OT_Hours)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_OT_CALCULATION_SUMMARY", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
Public Class ClsEmpOTEntryDetailCalculation
#Region "Variables"
    Public PK_ID As Integer = 0
    Public Document_Code As String = Nothing
    Public Emp_Code As String = Nothing
    Public Emp_Name As String = Nothing
    Public OT_Date As DateTime
    Public OT_Type As String = Nothing
    Public OT_Hours As Decimal = 0
    Public OT_Incremented_Basic As Decimal = 0
    Public OT_Incremented_DA As Decimal = 0
    Public OT_Basic As Decimal = 0
    Public OT_DA As Decimal = 0
    Public Amount As Decimal = 0
    'Public arr As List(Of clsfrmVLCMaster) = Nothing

    'Public BalanceAmount As Decimal = 0
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of ClsEmpOTEntryDetailCalculation), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As ClsEmpOTEntryDetailCalculation In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "REF_PK_ID", obj.PK_ID)
                clsCommon.AddColumnsForChange(coll, "Increment_Basic", obj.OT_Incremented_Basic)
                clsCommon.AddColumnsForChange(coll, "Increment_DA", obj.OT_Incremented_DA)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_OT_CALCULATION_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Class EmpSummary
        Public Property EmpName As String
        Public Property TotalHours As Double
        Public Property TotalAmount As Double
    End Class
End Class
