Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLoanGeneration

#Region "Variables"

    Public LOAN_GENERATION_CODE As String
    Public GENERATION_DATE As Date
    Public PAY_PERIOD_CODE As String
    Public PAY_PERIOD_NAME As String
    Public GENERATED_BY As String
    Public GENERATED_BY_NAME As String
    Public GENERATE_REMARKS As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public ObjList As List(Of clsLoanGenerationDetail)
    Public ObjLoanGeneration As clsAdjustmentVoucher
    Public LOCATION_CODE As String
    Public DEVISION_CODE As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLoanGeneration
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
            qry = "delete from TSPL_LOANGENERATION_DETAIL where LOAN_GENERATION_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_LOAN_GENERATION where LOAN_GENERATION_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLoanGeneration
        Dim obj As New clsLoanGeneration()
        Dim qry As String = "SELECT TAV.*,TPM.PAY_PERIOD_NAME, " _
                            & " EMP.Emp_Name as GENERATED_BY_NAME  FROM TSPL_LOAN_GENERATION TAV " _
                            & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON TAV.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON TAV.GENERATED_BY=EMP.EMP_CODE  where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and LOAN_GENERATION_CODE = (select MIN(LOAN_GENERATION_CODE) from TSPL_LOAN_GENERATION)"
            Case NavigatorType.Last
                qry += " and LOAN_GENERATION_CODE = (select Max(LOAN_GENERATION_CODE) from TSPL_LOAN_GENERATION)"
            Case NavigatorType.Next
                qry += " and LOAN_GENERATION_CODE = (select Min(LOAN_GENERATION_CODE) from TSPL_LOAN_GENERATION where  LOAN_GENERATION_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and LOAN_GENERATION_CODE = (select Max(LOAN_GENERATION_CODE) from TSPL_LOAN_GENERATION where LOAN_GENERATION_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and LOAN_GENERATION_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.LOAN_GENERATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOAN_GENERATION_CODE"))
            obj.GENERATION_DATE = clsCommon.GetPrintDate(dt.Rows(0)("GENERATION_DATE"), "dd/MMM/yyyy")
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.GENERATE_REMARKS = clsCommon.myCstr(dt.Rows(0)("GENERATE_REMARKS"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.GENERATED_BY = clsCommon.myCstr(dt.Rows(0)("GENERATED_BY"))
            obj.GENERATED_BY_NAME = clsCommon.myCstr(dt.Rows(0)("GENERATED_BY_NAME"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.DEVISION_CODE = clsCommon.myCstr(dt.Rows(0)("DEVISION_CODE"))

            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            strCode = obj.LOAN_GENERATION_CODE
        End If

        qry = "select TAV.LOAN_GENERATION_CODE,TAV.GENERATED_BY,EMP.EMP_NAME AS GENERATE_BY_NAME,TAVD.EMP_CODE,EMP1.EMP_NAME ,TAVD.LOAN_CODE,TAVD.Bank_code,TAVD.EMI_NO,TAVD.EMI_AMOUNT," _
             & " TAVD.ADJUSTMENT_PLUS,TAVD.ADJUSTMENT_MINUS,TAVD.NET_EMI,EMP.EMP_NAME,TAVD.PAY_PERIOD_CODE FROM TSPL_LOANGENERATION_DETAIL TAVD " _
             & " INNER JOIN  TSPL_LOAN_GENERATION TAV ON TAVD.LOAN_GENERATION_CODE=TAV.LOAN_GENERATION_CODE " _
             & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON TAV.GENERATED_BY=EMP.EMP_CODE " _
             & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP1 ON TAVD.EMP_CODE=EMP1.EMP_CODE where 2=2"

        qry += " and TAV.LOAN_GENERATION_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As New clsLoanGenerationDetail()
        Dim ObjList As New List(Of clsLoanGenerationDetail)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsLoanGenerationDetail()
                objtr.LOAN_GENERATION_CODE = clsCommon.myCstr(dr("LOAN_GENERATION_CODE"))
                objtr.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.EMP_NAME = clsCommon.myCstr(dr("EMP_NAME"))
                objtr.LOAN_CODE = clsCommon.myCstr(dr("LOAN_CODE"))
                objtr.EMI_No = clsCommon.myCdbl(dr("EMI_NO"))
                objtr.EMI_AMOUNT = clsCommon.myCdbl(dr("EMI_AMOUNT"))
                objtr.ADJUSTMENT_PLUS = clsCommon.myCdbl(dr("ADJUSTMENT_PLUS"))
                objtr.ADJUSTMENT_MINUS = clsCommon.myCdbl(dr("ADJUSTMENT_MINUS"))
                objtr.NET_EMI = clsCommon.myCdbl(dr("NET_EMI"))
                objtr.PAY_PERIOD_CODE = clsCommon.myCstr(dr("PAY_PERIOD_CODE"))
                '    objtr.Bank_CODE = clsCommon.myCstr(dr("Bank_code"))
                ObjList.Add(objtr)
            Next
        End If
        obj.ObjList = ObjList
        Return obj
    End Function
    Public Shared Function GetLoanEntryForPayPeriod(ByVal Pay_Period_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String = ""
        Dim Code As String = ""
        qry = "select top 1 LOAN_GENERATION_CODE from TSPL_LOAN_GENERATION where PAY_PERIOD_CODE='" & Pay_Period_Code & "' order by GENERATION_DATE desc"
        Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return Code
    End Function

    Public Function SaveData(ByVal obj As clsLoanGeneration, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Payroll", "Loan Generation", obj.LOCATION_CODE, obj.GENERATION_DATE, trans)
            If isNewEntry Then
                If strCode = "" Then
                    obj.LOAN_GENERATION_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.GENERATION_DATE, "dd/MMM/yyyy"), clsDocType.LoanGeneration, "", "")
                Else
                    obj.LOAN_GENERATION_CODE = strCode
                End If
            End If
            Dim qry As String = "delete from TSPL_LOANGENERATION_DETAIL where LOAN_GENERATION_CODE='" + obj.LOAN_GENERATION_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If (clsCommon.myLen(obj.LOAN_GENERATION_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "GENERATE_REMARKS", obj.GENERATE_REMARKS)
            clsCommon.AddColumnsForChange(coll, "GENERATED_BY", obj.GENERATED_BY, True)
            clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "GENERATION_DATE", clsCommon.GetPrintDate(obj.GENERATION_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "LOAN_GENERATION_CODE", obj.LOAN_GENERATION_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_GENERATION", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOAN_GENERATION", OMInsertOrUpdate.Update, "TSPL_LOAN_GENERATION.LOAN_GENERATION_CODE='" + obj.LOAN_GENERATION_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsLoanGenerationDetail.SaveData(obj.LOAN_GENERATION_CODE, obj.ObjList, trans)
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
            Dim obj As clsLoanGeneration = clsLoanGeneration.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.LOAN_GENERATION_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_LOAN_GENERATION set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where LOAN_GENERATION_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsLoanGenerationDetail
#Region "Variables"
    Public LOAN_GENERATION_CODE As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public LOAN_CODE As String
    ' Public Bank_CODE As String
    Public EMI_No As Integer
    Public EMI_AMOUNT As Decimal
    Public PAY_PERIOD_CODE As String
    Public ADJUSTMENT_PLUS As Decimal
    Public ADJUSTMENT_MINUS As Decimal
    Public NET_EMI As Decimal
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal ObjList As List(Of clsLoanGenerationDetail), ByVal trans As SqlTransaction) As Boolean
        If (ObjList IsNot Nothing AndAlso ObjList.Count > 0) Then
            For Each obj As clsLoanGenerationDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LOAN_GENERATION_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "LOAN_CODE", obj.LOAN_CODE)
                ' clsCommon.AddColumnsForChange(coll, "Bank_CODE", obj.Bank_CODE)
                clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
                clsCommon.AddColumnsForChange(coll, "EMI_No", obj.EMI_No)
                clsCommon.AddColumnsForChange(coll, "EMI_AMOUNT", obj.EMI_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_PLUS", obj.ADJUSTMENT_PLUS)
                clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_MINUS", obj.ADJUSTMENT_MINUS)
                clsCommon.AddColumnsForChange(coll, "NET_EMI", obj.NET_EMI)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LOANGENERATION_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class
