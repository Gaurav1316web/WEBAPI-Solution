Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAllowanceDetails


#Region "Variables"

    Public ALLOWANCE_CODE As String
    Public PAY_PERIOD_CODE As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public ALLOWANCE_REMARKS As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public PAY_PERIOD_NAME As String
    Public ALLOWANCE_DATE As Date
    Public ALLOWANCE_BY_CODE As String
    Public ALLOWANCE_BY_NAME As String
    Public LOCATION_CODE As String = ""
    'Public PP_TOTAL_DAYS As Integer

    ' '' grid columns
    'Public empCode As String
    'Public PayHeadCode As String
    'Public PayHeadName As String

    'Public ALLOWANCE_AMOUNT As Decimal


    'Public Shared ObjList As List(Of clsAllowanceDetails)
    Public Arr As New List(Of clsAllowancePayHeadDetails)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsAllowanceDetails
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_ALLOWANCE_DETAIL where ALLOWANCE_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_ALLOWANCE where ALLOWANCE_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsAllowanceDetails
        Dim obj As New clsAllowanceDetails()
        Dim objtr As New clsAllowancePayHeadDetails()

        obj.Arr = New List(Of clsAllowancePayHeadDetails)

        Dim qry As String = "SELECT TAV.*,TPM.PAY_PERIOD_NAME, " _
                            & " TAV.ALLOWANCE_REMARKS,EMP.Emp_Name,EMP1.EMP_NAME AS ALLOWANCE_BY_NAME  FROM TSPL_ALLOWANCE TAV " _
                            & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON TAV.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON TAV.EMP_CODE=EMP.EMP_CODE " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP1 ON TAV.EMP_CODE=EMP1.EMP_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and ALLOWANCE_CODE = (select MIN(ALLOWANCE_CODE) from TSPL_ALLOWANCE)"
            Case NavigatorType.Last
                qry += " and ALLOWANCE_CODE = (select Max(ALLOWANCE_CODE) from TSPL_ALLOWANCE)"
            Case NavigatorType.Next
                qry += " and ALLOWANCE_CODE = (select Min(ALLOWANCE_CODE) from TSPL_ALLOWANCE where  ALLOWANCE_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and ALLOWANCE_CODE = (select Max(ALLOWANCE_CODE) from TSPL_ALLOWANCE where ALLOWANCE_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and ALLOWANCE_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.ALLOWANCE_CODE = dt.Rows(0)("ALLOWANCE_CODE")
            obj.ALLOWANCE_DATE = dt.Rows(0)("ALLOWANCE_DATE")
            strCode = dt.Rows(0)("ALLOWANCE_CODE")
            obj.PAY_PERIOD_CODE = dt.Rows(0)("PAY_PERIOD_CODE")

            obj.ALLOWANCE_REMARKS = clsCommon.myCstr(dt.Rows(0)("ALLOWANCE_REMARKS"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.ALLOWANCE_BY_CODE = clsCommon.myCstr(dt.Rows(0)("ALLOWANCE_BY"))
            obj.ALLOWANCE_BY_NAME = clsCommon.myCstr(dt.Rows(0)("ALLOWANCE_BY_NAME"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If

        End If

        qry = "select TAV.ALLOWANCE_CODE,TAVD.EMP_CODE,EMP.EMP_NAME,TAVD.PAY_HEAD_CODE,TPH.PAY_HEAD_NAME," _
             & " TAVD.ALLOWANCE_AMOUNT FROM TSPL_ALLOWANCE_DETAIL TAVD " _
             & " INNER JOIN  TSPL_ALLOWANCE TAV ON TAVD.ALLOWANCE_CODE=TAV.ALLOWANCE_CODE " _
             & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON TAVD.EMP_CODE=EMP.EMP_CODE " _
             & " LEFT JOIN TSPL_PAYHEAD_MASTER TPH ON TAVD.PAY_HEAD_CODE=TPH.PAY_HEAD_CODE where 2=2"

        qry += " and TAV.ALLOWANCE_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsAllowancePayHeadDetails()
                objtr.ALLOWANCE_CODE = clsCommon.myCstr(dr("ALLOWANCE_CODE"))
                objtr.empCode = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.empName = clsCommon.myCstr(dr("EMP_NAME"))
                objtr.PayHeadCode = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.PayHeadName = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                objtr.ALLOWANCE_AMOUNT = clsCommon.myCdbl(dr("ALLOWANCE_AMOUNT"))
                obj.Arr.Add(objtr)
            Next
        End If

        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsAllowanceDetails, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Payroll", "Allowance Details", obj.LOCATION_CODE, obj.ALLOWANCE_DATE, trans)
            If isNewEntry Then
                If strCode = "" Then
                    obj.ALLOWANCE_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.ALLOWANCE_DATE, "dd/MMM/yyyy"), clsDocType.AllowanceDetails, "", "")
                Else
                    obj.ALLOWANCE_CODE = strCode
                End If
            End If
            Dim qry As String = "delete from TSPL_ALLOWANCE_DETAIL where ALLOWANCE_CODE='" + obj.ALLOWANCE_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.ALLOWANCE_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "ALLOWANCE_CODE", obj.ALLOWANCE_CODE)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ALLOWANCE_REMARKS", obj.ALLOWANCE_REMARKS)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE, True)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ALLOWANCE_BY", obj.ALLOWANCE_BY_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ALLOWANCE_DATE", clsCommon.GetPrintDate(obj.ALLOWANCE_DATE, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ALLOWANCE", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ALLOWANCE", OMInsertOrUpdate.Update, "TSPL_ALLOWANCE.ALLOWANCE_CODE='" + obj.ALLOWANCE_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsAllowancePayHeadDetails.SaveData(obj.ALLOWANCE_CODE, obj, trans)
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsAllowanceDetails = clsAllowanceDetails.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.ALLOWANCE_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_ALLOWANCE set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where ALLOWANCE_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetRegisterDT(ByVal strFromPP As String, ByVal strToPP As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT TT1.ALLOWANCE_CODE ,TT1.ALLOWANCE_DATE ,TT2.ALLOWANCE_AMOUNT ,TT1.ALLOWANCE_REMARKS ,"
            qry += " TT1.PAY_PERIOD_CODE ,TT1.PAY_PERIOD_NAME ,"
            qry += " TT1.EMP_CODE ,TT1.Emp_Name ,TT1.ALLOWANCE_BY ,"
            qry += " TT1.ALLOWANCE_BY_NAME ,TT1.POSTED  FROM ("
            qry += " SELECT T1.ALLOWANCE_CODE,T1.ALLOWANCE_DATE,T1.PAY_PERIOD_CODE,T2.PAY_PERIOD_NAME,T1.EMP_CODE,T3.Emp_Name,"
            qry += " T1.ALLOWANCE_BY,T4.Emp_Name AS ALLOWANCE_BY_NAME,T1.ALLOWANCE_REMARKS,T1.POSTED "
            qry += " FROM TSPL_ALLOWANCE T1 "
            qry += " LEFT JOIN TSPL_PAYPERIOD_MASTER T2 ON T1.PAY_PERIOD_CODE=T2.PAY_PERIOD_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T3 ON T1.EMP_CODE=T3.EMP_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T4 ON T1.EMP_CODE=T4.EMP_CODE "
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " WHERE T2.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strToPP + "')"
            End If
            qry += " ) AS TT1"
            qry += " LEFT JOIN (SELECT ALLOWANCE_CODE,SUM(ALLOWANCE_AMOUNT) AS ALLOWANCE_AMOUNT FROM TSPL_ALLOWANCE_DETAIL GROUP BY ALLOWANCE_CODE) AS TT2"
            qry += " ON TT1.ALLOWANCE_CODE=TT2.ALLOWANCE_CODE ;"
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetRegisterDTDetailed(ByVal strFromPP As String, ByVal strToPP As String, ByVal Location_Code As String, ByVal Division_Code As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT Ded.ALLOWANCE_CODE as [Document Code] ,Dedd.PAY_HEAD_CODE as [Pay Head Code] ,PHM.PAY_HEAD_NAME as [Pay Head Name] ," & _
                   " Dedd.EMP_CODE  as [Employee Code],EMP.Emp_Name as [Employee Name],EMP.Location_Code as [Location Code],Loc.Location_Desc as [Location Name] ,EMP.DEVISION_CODE as                     [Division Code],Div.Devision_Name as [Division Name],Dedd.ALLOWANCE_AMOUNT as [Allowance Amount]  FROM TSPL_ALLOWANCE Ded " & _
                   " INNER JOIN TSPL_ALLOWANCE_DETAIL Dedd ON Ded.ALLOWANCE_CODE=Dedd.ALLOWANCE_CODE " & _
                   " LEFT JOIN TSPL_PAYPERIOD_MASTER PP ON Ded.PAY_PERIOD_CODE=PP.PAY_PERIOD_CODE " & _
                   " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON Dedd.EMP_CODE=EMP.EMP_CODE " & _
                   " LEFT JOIN TSPL_PAYHEAD_MASTER PHM ON Dedd.PAY_HEAD_CODE=PHM.PAY_HEAD_CODE " & _
                   " left join TSPL_LOCATION_MASTER Loc on EMP.LOCATION_CODE=Loc.Location_Code " & _
                   " left join TSPL_DEVISION_MASTER Div on EMP.DEVISION_CODE=Div.DEVISION_CODE where 2=2 "
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " and PP.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strToPP + "') "
            End If
            If clsCommon.myLen(Location_Code) > 0 Then
                qry += " and EMP.Location_Code='" & Location_Code & "' "
            End If
            If clsCommon.myLen(Division_Code) > 0 Then
                qry += " and EMP.DEVISION_CODE='" & Division_Code & "' "
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
End Class
Public Class clsAllowancePayHeadDetails
#Region "Variables"
    '' grid columns
    Public ALLOWANCE_CODE As String
    Public empCode As String
    Public empName As String
    Public PayHeadCode As String
    Public PayHeadName As String
    Public ALLOWANCE_AMOUNT As Decimal
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal obj As clsAllowanceDetails, ByVal trans As SqlTransaction) As Boolean
        If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
            For Each objTr As clsAllowancePayHeadDetails In obj.Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ALLOWANCE_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "emp_Code", objTr.empCode)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", objTr.PayHeadCode)
                clsCommon.AddColumnsForChange(coll, "ALLOWANCE_AMOUNT", objTr.ALLOWANCE_AMOUNT)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ALLOWANCE_DETAIL", OMInsertOrUpdate.Insert, "TSPL_ALLOWANCE_DETAIL.ALLOWANCE_CODE='" + strDocNo + "'", trans)
            Next
        End If
        Return True
    End Function
End Class
