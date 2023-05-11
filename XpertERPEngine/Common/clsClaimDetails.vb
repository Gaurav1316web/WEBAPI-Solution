Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsClaimDetails
#Region "Variables"
    Public Claim_Code As String
    Public Claim_Date As Date
    Public Receiving_Date As Date
    Public Cust_Code As String
    Public Customer_Name As String
    Public Target_Code As String
    Public Claim_Amount As Double
    Public Approved_Amount As Double
    Public Approved_Date As Date
    Public Status As Boolean
    Public Isopening As Boolean

#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = " delete from TSPL_CLAIM_DETAILS where Cust_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsClaimDetails)
        Dim obj As clsClaimDetails = Nothing
        Dim ObjList As New List(Of clsClaimDetails)
        Dim qry As String = " select T1.*,T2.Customer_Name,T3.IsOpening from TSPL_CLAIM_DETAILS T1 "
        qry += " left outer join TSPL_CUSTOMER_MASTER T2 on T2.Cust_Code = T1.Cust_Code "
        qry += " left outer join TSPL_DISCOUNT_MASTER T3 on T3.Code = T1.Target_Code "
        qry += " where T1.Cust_Code = '" + strCode + "'"
        qry += " ORDER BY CONVERT(int, T3.IsOpening) DESC"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsClaimDetails()
                obj.Claim_Code = clsCommon.myCstr(dr("Claim_Code"))

                If clsCommon.myLen(dr("Claim_Date")) > 0 Then
                    obj.Claim_Date = clsCommon.myCDate(dr("Claim_Date"))
                Else
                    obj.Claim_Date = Nothing
                End If
                If clsCommon.myLen(dr("Receiving_Date")) > 0 Then
                    obj.Receiving_Date = clsCommon.myCDate(dr("Receiving_Date"))
                Else
                    obj.Receiving_Date = Nothing
                End If
                obj.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                obj.Customer_Name = clsCommon.myCstr(dr("Customer_Name"))
                obj.Target_Code = clsCommon.myCstr(dr("Target_Code"))
                obj.Isopening = clsCommon.myCBool(dr("IsOpening"))
                obj.Claim_Amount = clsCommon.myCdbl(dr("Claim_Amount"))
                If clsCommon.myCBool(dr("Status")) Then
                    obj.Approved_Amount = clsCommon.myCdbl(dr("Approved_Amount"))
                    If clsCommon.myLen(dr("Approved_Date")) > 0 Then
                        obj.Approved_Date = clsCommon.myCDate(dr("Approved_Date"))
                    Else
                        obj.Approved_Date = Nothing
                    End If
                    obj.Status = clsCommon.myCBool(dr("Status"))
                End If
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsClaimDetails)) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As clsClaimDetails In ObjList
                Dim coll As New Hashtable()
                If clsCommon.myLen(obj.Claim_Code) <= 0 Then
                    obj.Claim_Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.ClaimMaster, "", "")
                    If clsCommon.myLen(obj.Claim_Code) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "Claim_Date", clsCommon.GetPrintDate(obj.Claim_Date, "dd/MMM/yyyy"))
                If clsCommon.myLen(obj.Receiving_Date) > 0 Then
                    clsCommon.AddColumnsForChange(coll, "Receiving_Date", clsCommon.GetPrintDate(obj.Receiving_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Cust_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Target_Code", obj.Target_Code)
                clsCommon.AddColumnsForChange(coll, "Claim_Amount", obj.Claim_Amount)
                clsCommon.AddColumnsForChange(coll, "Approved_Amount", obj.Approved_Amount)
                clsCommon.AddColumnsForChange(coll, "Approved_Date", clsCommon.GetPrintDate(obj.Approved_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Status", clsCommon.myCBool(obj.Status))
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_CLAIM_DETAILS where Claim_Code = '" & obj.Claim_Code & "' and Target_Code = '" & obj.Target_Code & "' "
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    clsCommon.AddColumnsForChange(coll, "Claim_Code", obj.Claim_Code)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CLAIM_DETAILS", OMInsertOrUpdate.Insert, "")
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CLAIM_DETAILS", OMInsertOrUpdate.Update, " Claim_Code = '" & obj.Claim_Code & "' and Target_Code = '" & obj.Target_Code & "' ")
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetBalanceOfClaim(ByVal strCustCode As String, ByVal strDate As Date, ByVal strInvNo As String, ByVal trans As SqlTransaction) As Double
        Try
            Dim qry As String = ""

            Dim dtTo As Date = New Date(strDate.Year, strDate.Month + 1, 1)
            dtTo = dtTo.AddDays(-1)


            qry += " select SUM(final.inv_amt*RI)+SUM(final.Claim_amt*RI) AS INV_AMT   from("
            qry += " select Cust_Code,Claim_Date as 'Date', 0 as 'inv_amt',  Approved_Amount AS 'Claim_amt', 1 AS RI  from TSPL_CLAIM_DETAILS  "
            qry += " WHERE Cust_Code  ='" + strCustCode + "'  AND Claim_Date <= '" + clsCommon.GetPrintDate(dtTo, "dd/MMM/yyyy") + "'"
            qry += "  union all "
            qry += " SELECT Cust_Code,Sale_Invoice_Date as 'Date', Inv_Discount_Amt  as 'inv_amt', 0 AS 'Claim_amt', -1 AS RI FROM TSPL_SALE_INVOICE_HEAD"
            qry += " WHERE Discount_On =1 AND Cust_Code  ='" + strCustCode + "' AND Sale_Invoice_No NOT IN ( '" + strInvNo + "')  "
            qry += " ) as final "
            qry += " GROUP BY final.Cust_Code  "

            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

End Class
