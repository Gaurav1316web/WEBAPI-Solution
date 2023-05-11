Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsCustomerDeductionHead
#Region "Variables"
    Public Deduction_Code As String = Nothing
    Public Deduction_Valid_Till As Date
    Public Deduction_Name As String = Nothing
    Public Deduction_Amount As Decimal
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Inactive_Status As Boolean
    Public arr As ArrayList
#End Region

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select  TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Code as [Code] ,TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Name AS [Description],TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Amount As [Deduction Amount] ,TSPL_CUSTOMER_DEDUCTION_HEAD.Created_By as [Created By] ,Convert(varchar,TSPL_CUSTOMER_DEDUCTION_HEAD.Created_Date,103) as [Created Date],TSPL_CUSTOMER_DEDUCTION_HEAD.Modified_By as [Modified By] ,Convert(varchar,TSPL_CUSTOMER_DEDUCTION_HEAD.Modified_Date,103) as [Modified Date],case when Posted=1 then 'Posted' else 'Pending' end as [Status] From TSPL_CUSTOMER_DEDUCTION_HEAD"
        Return clsCommon.myCstr(clsCommon.ShowSelectForm("custDeducode", qry, "Code", "", curcode, "Code", isButtonClicked))
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String = "delete from TSPL_CUSTOMER_DEDUCTION_CUSTOMER where Deduction_Code ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_CUSTOMER_DEDUCTION_HEAD where Deduction_Code ='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCustomerDeductionHead
        Return GetData(strCode, NavType, Nothing)
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsCustomerDeductionHead
        Dim obj As clsCustomerDeductionHead = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_DEDUCTION_HEAD.* from TSPL_CUSTOMER_DEDUCTION_HEAD where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and Deduction_Code = (select MIN(Deduction_Code) from TSPL_CUSTOMER_DEDUCTION_HEAD)"
            Case NavigatorType.Last
                qry += " and Deduction_Code = (select Max(Deduction_Code) from TSPL_CUSTOMER_DEDUCTION_HEAD)"
            Case NavigatorType.Next
                qry += " and Deduction_Code = (select Min(Deduction_Code) from TSPL_CUSTOMER_DEDUCTION_HEAD where  Deduction_Code>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and Deduction_Code = (select Max(Deduction_Code) from TSPL_CUSTOMER_DEDUCTION_HEAD where Deduction_Code<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and Deduction_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCustomerDeductionHead()
            obj.Deduction_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
            obj.Deduction_Valid_Till = clsCommon.myCDate(dt.Rows(0)("Deduction_Valid_Till"))
            obj.Deduction_Name = clsCommon.myCstr(dt.Rows(0)("Deduction_Name"))
            obj.Deduction_Amount = clsCommon.myCdbl(dt.Rows(0)("Deduction_Amount"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 0, ERPTransactionStatus.Pending, ERPTransactionStatus.Approved)
            obj.Inactive_Status = (clsCommon.myCdbl(dt.Rows(0)("Inactive_Status")) = 1)
            obj.arr = clsCustomerDeductionCustomer.GetData(obj.Deduction_Code, trans)
        End If
        Return obj
    End Function

    Public Shared Function SaveData(ByVal obj As clsCustomerDeductionHead, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
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

    Public Shared Function SaveData(ByVal obj As clsCustomerDeductionHead, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strqry As String = "delete from TSPL_CUSTOMER_DEDUCTION_CUSTOMER where Deduction_Code ='" + obj.Deduction_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(strqry, trans)

            obj.Deduction_Valid_Till = obj.Deduction_Valid_Till.AddMonths(1)
            obj.Deduction_Valid_Till = New Date(obj.Deduction_Valid_Till.Year, obj.Deduction_Valid_Till.Month, 1).AddDays(-1)

            strqry = "select TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Deduction_Code,TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Cust_Code from TSPL_CUSTOMER_DEDUCTION_CUSTOMER" + Environment.NewLine + _
            "left outer join TSPL_CUSTOMER_DEDUCTION_HEAD on TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Code=TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Deduction_Code" + Environment.NewLine + _
            "where TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Cust_Code in (" + clsCommon.GetMulcallString(obj.arr) + ") and TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Deduction_Code not in ('" + obj.Deduction_Code + "') and TSPL_CUSTOMER_DEDUCTION_HEAD.Inactive_Status = 0 " + Environment.NewLine + _
            "and TSPL_CUSTOMER_DEDUCTION_HEAD.Deduction_Valid_Till >= '" + clsCommon.GetPrintDate(obj.Deduction_Valid_Till, "dd/MMM/yyyy") + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                strqry = "cannnot save due to valid deduction code exists.Details are" + Environment.NewLine
                For Each dr As DataRow In dt.Rows
                    strqry += "Deduction Code [" + clsCommon.myCstr(dr("Deduction_Code")) + "] and customer [" + clsCommon.myCstr(dr("Cust_Code")) + "]"
                Next
                Throw New Exception(strqry)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Deduction_Valid_Till", clsCommon.GetPrintDate(obj.Deduction_Valid_Till, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Deduction_Name", obj.Deduction_Name)
            clsCommon.AddColumnsForChange(coll, "Deduction_Amount", obj.Deduction_Amount)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Deduction_Code = clsERPFuncationality.GetNextCode(trans, obj.Deduction_Valid_Till, clsDocType.CustomerDeduction, "", "")
                If clsCommon.myLen(obj.Deduction_Code) <= 0 Then
                    Throw New Exception("Error in code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Posted_By", Nothing, True)
                clsCommon.AddColumnsForChange(coll, "Deduction_Code", obj.Deduction_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_DEDUCTION_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_DEDUCTION_HEAD", OMInsertOrUpdate.Update, "Deduction_Code='" + obj.Deduction_Code + "'", trans)
            End If
            clsCustomerDeductionCustomer.SaveData(obj.arr, obj.Deduction_Code, obj.Deduction_Valid_Till, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsCustomerDeductionHead = clsCustomerDeductionHead.GetData(strCode, NavigatorType.Current, trans)
            If obj Is Nothing OrElse obj.arr.Count <= 0 Then
                Throw New Exception("Invalid deduction code")
            End If
            If obj.Posted = ERPTransactionStatus.Approved Then
                Throw New Exception("Already posted Document " + obj.Deduction_Code)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Posted", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_DEDUCTION_HEAD", OMInsertOrUpdate.Update, "Deduction_Code='" + obj.Deduction_Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function InactiveData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim obj As clsCustomerDeductionHead = clsCustomerDeductionHead.GetData(strCode, NavigatorType.Current, trans)
            If obj Is Nothing OrElse obj.arr.Count <= 0 Then
                Throw New Exception("Invalid deduction code")
            End If
            If obj.Posted <> ERPTransactionStatus.Approved Then
                Throw New Exception("Should be posted Document " + obj.Deduction_Code)
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Inactive_Status", 1)
            clsCommon.AddColumnsForChange(coll, "Inactive_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Inactive_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_DEDUCTION_HEAD", OMInsertOrUpdate.Update, "Deduction_Code='" + obj.Deduction_Code + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsCustomerDeductionCustomer
#Region "Variables"
    Public TR_Code As String = Nothing
    Public Deduction_Code As String = Nothing
    Public Cust_Code As String
#End Region

    Public Shared Function SaveData(ByVal Arr As ArrayList, ByVal strDedCode As String, ByVal dtDocDate As Date, ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each strCustCode As String In Arr
                Dim coll As New Hashtable()
                Dim strTRCode As String = clsERPFuncationality.GetNextCode(trans, dtDocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
                clsCommon.AddColumnsForChange(coll, "TR_Code", strTRCode)
                clsCommon.AddColumnsForChange(coll, "Cust_Code", strCustCode)
                clsCommon.AddColumnsForChange(coll, "Deduction_Code", strDedCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_DEDUCTION_CUSTOMER", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As ArrayList
        Dim arr As ArrayList = Nothing
        Dim qry As String = "select TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Cust_Code from TSPL_CUSTOMER_DEDUCTION_CUSTOMER where TSPL_CUSTOMER_DEDUCTION_CUSTOMER.Deduction_Code='" + strDocNo + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList()
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Cust_Code")))
            Next
        End If
        Return arr
    End Function
End Class
