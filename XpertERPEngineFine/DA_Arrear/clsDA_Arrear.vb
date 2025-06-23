Imports System.Data.SqlClient
Public Class clsDA_Arrear
#Region "Variables"
    Public Document_Code As String = Nothing
    Public Document_Date As Date = Nothing
    Public Arrear_Date As Date = Nothing
    Public PeriodFrom_Date As Date = Nothing
    Public PeriodTo_Date As Date = Nothing
    Public Location_Code As String = Nothing
    Public PAY_PERIOD_CODE As String = Nothing
    Public Emp_Code As ArrayList = Nothing
    Public DA_Per As Decimal = 0
    Public IsApplyLeaveIncashment As Integer = 0
#End Region
    Public Function SaveData(ByVal obj As clsDA_Arrear, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsDA_Arrear, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_DA_ARREAR_EMPLOYEE where Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Arrear_Date", clsCommon.GetPrintDate(obj.Arrear_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "PeriodFrom_Date", clsCommon.GetPrintDate(obj.PeriodFrom_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "PeriodTo_Date", clsCommon.GetPrintDate(obj.PeriodTo_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "DA_Per", obj.DA_Per)
            clsCommon.AddColumnsForChange(coll, "IsApplyLeaveIncashment", obj.IsApplyLeaveIncashment)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.DAArrearNew, "", "")
                If clsCommon.myLen(obj.Document_Code) <= 0 Then
                    Throw New Exception("Error in Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DA_ARREAR", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DA_ARREAR", OMInsertOrUpdate.Update, "TSPL_DA_ARREAR.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            clsDA_Arrear_Employee.SaveData(obj.Document_Code, obj.Emp_Code, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal Document_Code As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDA_Arrear
        Dim obj As clsDA_Arrear = Nothing
        Try
            Dim Whrcls As String = ""
            Dim strQry As String = "select Document_Code,Document_Date,Arrear_Date,PeriodFrom_Date,PeriodTo_Date,Location_Code,DA_Per,IsApplyLeaveIncashment,PAY_PERIOD_CODE from TSPL_DA_ARREAR  where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_DA_ARREAR.Document_Code = (select MIN(Document_Code) from TSPL_DA_ARREAR where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Last
                    strQry += " and TSPL_DA_ARREAR.Document_Code = (select Max(Document_Code) from TSPL_DA_ARREAR where 1=1 " + Whrcls + "  )"
                Case NavigatorType.Next
                    strQry += " and TSPL_DA_ARREAR.Document_Code = (select Min(Document_Code) from TSPL_DA_ARREAR where Document_Code>'" + clsCommon.myCstr(Document_Code) + "' " + Whrcls + "   )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_DA_ARREAR.Document_Code = (select Max(Document_Code) from TSPL_DA_ARREAR where Document_Code<'" + clsCommon.myCstr(Document_Code) + "' " + Whrcls + "  )"
                Case NavigatorType.Current
                    strQry += " and TSPL_DA_ARREAR.Document_Code = '" + clsCommon.myCstr(Document_Code) + "'  " + Whrcls + " "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsDA_Arrear()
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Document_Date = clsCommon.GetPrintDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy")
                obj.Arrear_Date = clsCommon.GetPrintDate(dt.Rows(0)("Arrear_Date"), "dd/MMM/yyyy")
                obj.PeriodFrom_Date = clsCommon.GetPrintDate(dt.Rows(0)("PeriodFrom_Date"), "dd/MMM/yyyy")
                obj.PeriodTo_Date = clsCommon.GetPrintDate(dt.Rows(0)("PeriodTo_Date"), "dd/MMM/yyyy")
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
                obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
                obj.DA_Per = clsCommon.myCdbl(dt.Rows(0)("DA_Per"))
                obj.IsApplyLeaveIncashment = clsCommon.myCdbl(dt.Rows(0)("IsApplyLeaveIncashment"))
                Dim countEmp As Integer = clsDBFuncationality.getSingleValue("select Count(EMP_CODE) as noofEmp from TSPL_EMPLOYEE_MASTER where Emp_Status='Active'", trans)
                Dim countDAemp As Integer = clsDBFuncationality.getSingleValue("select Count(EMP_CODE) as NoofEmp from TSPL_DA_ARREAR_EMPLOYEE where Document_Code='" & obj.Document_Code & "'", trans)
                If countEmp = countDAemp Then
                    obj.Emp_Code = Nothing
                Else
                    strQry = "select EMP_CODE from TSPL_DA_ARREAR_EMPLOYEE where Document_Code='" & obj.Document_Code & "'"
                    dt = New DataTable()
                    dt = clsDBFuncationality.GetDataTable(strQry, trans)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        obj.Emp_Code = New ArrayList()
                        For Each dr As DataRow In dt.Rows
                            obj.Emp_Code.Add(clsCommon.myCstr(dr("EMP_CODE")))
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
        Dim isSaved As Boolean
        Dim obj As New clsDA_Arrear()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_DA_ARREAR", "Document_Code", "TSPL_DA_ARREAR_EMPLOYEE", "Document_Code", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_DA_ARREAR", "Document_Code", "TSPL_DA_ARREAR_EMPLOYEE", "Document_Code", trans)
            Dim qry As String
            qry = "delete from TSPL_DA_ARREAR_EMPLOYEE where Document_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_DA_ARREAR where Document_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
Public Class clsDA_Arrear_Employee
#Region "Variables"
    Public Document_Code As String = Nothing
    Public EMP_CODE As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Try
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each strEmpCode As String In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "EMP_CODE", strEmpCode)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DA_ARREAR_EMPLOYEE", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class