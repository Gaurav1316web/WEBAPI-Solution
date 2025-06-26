Imports System.Data.SqlClient
Public Class clsLeaveEncashmentHead
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date = Nothing
    Public Location_Code As String = Nothing
    Public Doc_Type As String = Nothing
    Public Remarks As String = Nothing
    Public Posted As Integer = Nothing
    Public Arr As List(Of clsLeaveEncashmentDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsLeaveEncashmentHead, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsLeaveEncashmentHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_Leave_Encashment_Detail where Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.LeaveEncashment, "", "")
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Leave_Encashment_Head", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Leave_Encashment_Head", OMInsertOrUpdate.Update, "TSPL_Leave_Encashment_Head.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsLeaveEncashmentDetail.SaveData(obj.Document_Code, obj.Arr, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_Leave_Encashment_Head", "Document_Code", "TSPL_Leave_Encashment_Detail", "Document_Code", trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLeaveEncashmentHead
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLeaveEncashmentHead
        Dim obj As clsLeaveEncashmentHead = Nothing
        Dim qry = "SELECT  TSPL_Leave_Encashment_Head.*  FROM TSPL_Leave_Encashment_Head where 2=2 "
        Dim whrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrCls = " AND Location in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Leave_Encashment_Head.Document_Code = (select MIN(Document_Code) from TSPL_Leave_Encashment_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_Leave_Encashment_Head.Document_Code = (select Max(Document_Code) from TSPL_Leave_Encashment_Head WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_Leave_Encashment_Head.Document_Code = '" + strCode + "'"
            Case NavigatorType.Next
                qry += " and TSPL_Leave_Encashment_Head.Document_Code = (select Min(Document_Code) from TSPL_Leave_Encashment_Head where Document_Code >'" + strCode + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Leave_Encashment_Head.Document_Code = (select Max(Document_Code) from TSPL_Leave_Encashment_Head where Document_Code <'" + strCode + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLeaveEncashmentHead()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))

            qry = "SELECT TSPL_Leave_Encashment_Detail.* FROM TSPL_Leave_Encashment_Detail where Document_Code='" & obj.Document_Code & "' order by TSPL_Leave_Encashment_Detail.Emp_Code asc"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsLeaveEncashmentDetail)
                Dim objTr As clsLeaveEncashmentDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsLeaveEncashmentDetail
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.IsApplied = clsCommon.myCdbl(dr("IsApplied"))
                    objTr.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                    objTr.Emp_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + clsCommon.myCstr(objTr.Emp_Code) + "' "))
                    objTr.LEAVE_CODE = clsCommon.myCstr(dr("LEAVE_CODE"))
                    objTr.LEAVE_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select LEAVE_NAME from TSPL_LEAVE_MASTER where LEAVE_CODE='" + clsCommon.myCstr(objTr.LEAVE_CODE) + "' "))
                    objTr.No_of_Days = clsCommon.myCdbl(dr("No_of_Days"))
                    objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
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
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            If clsCommon.myLen(strCode) > 0 Then
                Dim flag As Boolean = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Leave_Encashment_Head where Document_Code='" + strCode + "'", trans)) = 0, True, False)
                If flag Then
                    Throw New Exception(" Document Not Found!")
                End If
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_Leave_Encashment_Head", "Document_Code", "TSPL_Leave_Encashment_DETAIL", "Document_Code", trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_Leave_Encashment_Head", "Document_Code", "TSPL_Leave_Encashment_DETAIL", "Document_Code", trans)

            qry = "delete from TSPL_Leave_Encashment_DETAIL where Document_Code='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Leave_Encashment_Head where  Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
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
                Throw New Exception("Document Code not found to Post")
            End If
            Dim obj As clsLeaveEncashmentHead = clsLeaveEncashmentHead.GetData(strDocNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If obj.Posted = 1 Then
                Throw New Exception("Docuemnt is already posted")
            End If


            Dim dtNow As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt")
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posting_Date", dtNow)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Leave_Encashment_Head", OMInsertOrUpdate.Update, "TSPL_Leave_Encashment_Head.Document_Code='" + obj.Document_Code + "'", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_Leave_Encashment_Head", "Document_Code", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select Posted from TSPL_Leave_Encashment_Head where Document_Code='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            Qry = "Update TSPL_Leave_Encashment_Head set Posted = 0 where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class
Public Class clsLeaveEncashmentDetail
#Region "Variables"
    Public Document_Code As String = Nothing
    Public IsApplied As Integer = Nothing
    Public Emp_Code As String = Nothing
    Public Emp_Name As String = Nothing
    Public LEAVE_CODE As String = Nothing
    Public LEAVE_Name As String = Nothing
    Public No_of_Days As Double = Nothing
    Public Amount As Double = Nothing
#End Region
    Public Shared Function SaveData(ByVal StrCode As String, ByVal Arr As List(Of clsLeaveEncashmentDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsLeaveEncashmentDetail In Arr

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", StrCode)
                    clsCommon.AddColumnsForChange(coll, "IsApplied", obj.IsApplied, True)
                    clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                    clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", obj.LEAVE_CODE)
                    clsCommon.AddColumnsForChange(coll, "No_of_Days", obj.No_of_Days, True)
                    clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount, True)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Leave_Encashment_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
