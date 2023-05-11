Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLoanAdjustment

#Region "Variables"
    Public LOANADJUSTMENT_CODE As String
    Public PAY_PERIOD_CODE As String
    Public PAY_PERIOD_NAME As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public ADJUSTMENT_DATE As String
    Public LOAN_CODE As String
    Public ADJUSTMENT_PLUS As Decimal
    Public ADJUSTMENT_MINUS As Decimal
    Public ADJUSTMENT_BY_CODE As String
    Public ADJUSTMENT_BY_NAME As String
    Public ADJUSTMENT_REASON As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime 

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLoanAdjustment
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_LOAN_ADJUSTMENT where LOANADJUSTMENT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLoanAdjustment
        Dim obj As clsLoanAdjustment = Nothing
        Dim qry = "select ADJ.*,EMP.EMP_NAME,EMP1.EMP_NAME AS ADJUSTMENT_BY_NAME,TPM.PAY_PERIOD_NAME from TSPL_LOAN_ADJUSTMENT ADJ " _
        & " LEFT JOIN  TSPL_PAYPERIOD_MASTER TPM ON ADJ.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE " _
        & " LEFT JOIN  TSPL_EMPLOYEE_MASTER EMP ON ADJ.EMP_CODE=EMP.EMP_CODE " _
        & " LEFT JOIN  TSPL_EMPLOYEE_MASTER EMP1 ON ADJ.ADJUSTMENT_BY=EMP1.EMP_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and LOANADJUSTMENT_CODE = (select MIN(LOANADJUSTMENT_CODE) from TSPL_LOAN_ADJUSTMENT)"
            Case NavigatorType.Last
                qry += " and LOANADJUSTMENT_CODE = (select Max(LOANADJUSTMENT_CODE) from TSPL_LOAN_ADJUSTMENT)"
            Case NavigatorType.Next
                qry += " and LOANADJUSTMENT_CODE = (select Min(LOANADJUSTMENT_CODE) from TSPL_LOAN_ADJUSTMENT where  LOANADJUSTMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and LOANADJUSTMENT_CODE = (select Max(LOANADJUSTMENT_CODE) from TSPL_LOAN_ADJUSTMENT where LOANADJUSTMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and LOANADJUSTMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLoanAdjustment()
            obj.LOANADJUSTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("LOANADJUSTMENT_CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.ADJUSTMENT_DATE = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_DATE"))
            obj.LOAN_CODE = clsCommon.myCstr(dt.Rows(0)("LOAN_CODE"))
            obj.ADJUSTMENT_BY_CODE = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY"))
            obj.ADJUSTMENT_BY_NAME = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY_NAME"))
            obj.ADJUSTMENT_PLUS = clsCommon.myCdbl(dt.Rows(0)("ADJUSTMENT_PLUS"))
            obj.ADJUSTMENT_MINUS = clsCommon.myCdbl(dt.Rows(0)("ADJUSTMENT_MINUS"))
            obj.ADJUSTMENT_REASON = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_REASON"))
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        Return obj


    End Function
    Public Function SaveData(ByVal obj As clsLoanAdjustment, ByVal strCode As String, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_DATE", obj.ADJUSTMENT_DATE)
            clsCommon.AddColumnsForChange(coll, "LOAN_CODE", obj.LOAN_CODE)
            If clsCommon.myLen(obj.ADJUSTMENT_BY_CODE) > 0 Then
                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_BY", obj.ADJUSTMENT_BY_CODE)
            End If
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_PLUS", obj.ADJUSTMENT_PLUS)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_MINUS", obj.ADJUSTMENT_MINUS)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_REASON", obj.ADJUSTMENT_REASON)
            clsCommon.AddColumnsForChange(coll, "GENERATED", 0)
            clsCommon.AddColumnsForChange(coll, "POSTED", 0)


            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If strCode = "" Then
                    obj.LOANADJUSTMENT_CODE = clsERPFuncationality.GetNextCode(Nothing, obj.ADJUSTMENT_DATE, clsDocType.LoanAdjustment, "", "")
                Else
                    obj.LOANADJUSTMENT_CODE = strCode
                End If

                clsCommon.AddColumnsForChange(coll, "LOANADJUSTMENT_CODE", obj.LOANADJUSTMENT_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_LOAN_ADJUSTMENT where LOANADJUSTMENT_CODE= '" & obj.LOANADJUSTMENT_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_ADJUSTMENT", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_ADJUSTMENT", OMInsertOrUpdate.Update, "LOANADJUSTMENT_CODE='" + obj.LOANADJUSTMENT_CODE + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsLoanAdjustment = clsLoanAdjustment.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.LOANADJUSTMENT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_LOAN_ADJUSTMENT set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where LOANADJUSTMENT_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class
