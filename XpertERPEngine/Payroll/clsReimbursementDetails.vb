Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsReimbursementDetails

#Region "Variables"

    Public REIMBURSEMENT_CODE As String
    Public PAY_PERIOD_CODE As String
    'Public ADJUSTMENT_BY_Code As String
    'Public ADJUSTMENT_BY_Name As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public REIMBURSEMENT_REMARK As String
    Public POSTED As Boolean
    Public PAY_PERIOD_NAME As String
    Public REIMBURSEMENT_DATE As Date
    Public Posting_Date As DateTime
    'Public PP_TOTAL_DAYS As Integer

    '' grid columns
    Public empCode As String
    Public PayHeadCode As String
    Public PayHeadName As String

    Public REIMBURSEMENT_AMOUNT As Decimal


    Public Shared ObjList As List(Of clsReimbursementDetails)
    Public Arr As New List(Of clsReimbursementPayHeadDetails)

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsReimbursementDetails
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
            qry = "delete from TSPL_EMPREIMBURSEMENT_DETAIL where REIMBURSEMENT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EMP_REIMBURSEMENT where REIMBURSEMENT_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsReimbursementDetails
        Dim obj As New clsReimbursementDetails()
        Dim objtr As New clsReimbursementDetails()

        ObjList = New List(Of clsReimbursementDetails)

        Dim qry As String = "SELECT TAV.REIMBURSEMENT_CODE,TAV.REIMBURSEMENT_DATE,TAV.PAY_PERIOD_CODE,TPM.PAY_PERIOD_NAME,TAV.POSTED,TPM.Posting_Date, " _
                            & " TAV.REIMBURSEMENT_REMARK,TAV.EMP_CODE,EMP.Emp_Name  FROM TSPL_EMP_REIMBURSEMENT TAV " _
                            & " INNER JOIN TSPL_PAYPERIOD_MASTER TPM ON TAV.PAY_PERIOD_CODE=TPM.PAY_PERIOD_CODE " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON TAV.EMP_CODE=EMP.EMP_CODE  where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and REIMBURSEMENT_CODE = (select MIN(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT)"
            Case NavigatorType.Last
                qry += " and REIMBURSEMENT_CODE = (select Max(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT)"
            Case NavigatorType.Next
                qry += " and REIMBURSEMENT_CODE = (select Min(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT where  REIMBURSEMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and REIMBURSEMENT_CODE = (select Max(REIMBURSEMENT_CODE) from TSPL_EMP_REIMBURSEMENT where REIMBURSEMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and REIMBURSEMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.REIMBURSEMENT_CODE = dt.Rows(0)("REIMBURSEMENT_CODE")
            obj.REIMBURSEMENT_DATE = dt.Rows(0)("REIMBURSEMENT_DATE")
            strCode = dt.Rows(0)("REIMBURSEMENT_CODE")
            obj.PAY_PERIOD_CODE = dt.Rows(0)("PAY_PERIOD_CODE")
            'obj.ADJUSTMENT_BY_Code = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY"))
            'obj.ADJUSTMENT_BY_Name = clsCommon.myCstr(dt.Rows(0)("ADJUSTMENT_BY_NAME"))

            obj.REIMBURSEMENT_REMARK = clsCommon.myCstr(dt.Rows(0)("REIMBURSEMENT_REMARK"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))

        End If

        qry = "select TAV.REIMBURSEMENT_CODE,TAVD.EMP_CODE,EMP.EMP_NAME,TAVD.PAY_HEAD_CODE,TPH.PAY_HEAD_NAME," _
             & " TAVD.REIMBURSEMENT_AMOUNT FROM TSPL_EMPREIMBURSEMENT_DETAIL TAVD " _
             & " INNER JOIN  TSPL_EMP_REIMBURSEMENT TAV ON TAVD.REIMBURSEMENT_CODE=TAV.REIMBURSEMENT_CODE " _
             & " INNER JOIN TSPL_EMPLOYEE_MASTER EMP ON TAVD.EMP_CODE=EMP.EMP_CODE " _
             & " LEFT JOIN TSPL_PAYHEAD_MASTER TPH ON TAVD.PAY_HEAD_CODE=TPH.PAY_HEAD_CODE where 2=2"

        qry += " and TAV.REIMBURSEMENT_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsReimbursementDetails()
                objtr.REIMBURSEMENT_CODE = clsCommon.myCstr(dr("REIMBURSEMENT_CODE"))
                objtr.empCode = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.PayHeadCode = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.PayHeadName = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                objtr.REIMBURSEMENT_AMOUNT = clsCommon.myCstr(dr("REIMBURSEMENT_AMOUNT"))
                ObjList.Add(objtr)
            Next
        End If

        clsReimbursementDetails.ObjList = ObjList
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsReimbursementDetails, ByVal objList As List(Of clsReimbursementDetails), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            If isNewEntry Then
                If strCode = "" Then
                    obj.REIMBURSEMENT_CODE = clsERPFuncationality.GetNextCode(trans, obj.REIMBURSEMENT_DATE, clsDocType.ReimbursementDetails, "", "")
                Else
                    obj.REIMBURSEMENT_CODE = strCode
                End If
            End If

            Dim qry As String = "delete from TSPL_EMPREIMBURSEMENT_DETAIL where REIMBURSEMENT_CODE='" + obj.REIMBURSEMENT_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.REIMBURSEMENT_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_CODE", obj.REIMBURSEMENT_CODE)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            'clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_BY", IIf(obj.ADJUSTMENT_BY_Code Is Nothing, DBNull.Value, obj.ADJUSTMENT_BY_Code))
            clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_REMARK", obj.REIMBURSEMENT_REMARK)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_DATE", clsCommon.GetPrintDate(obj.REIMBURSEMENT_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_AMOUNT", obj.REIMBURSEMENT_AMOUNT)

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            'clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            'clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then

                'clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_CODE", obj.REIMBURSEMENT_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMP_REIMBURSEMENT", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMP_REIMBURSEMENT", OMInsertOrUpdate.Update, "TSPL_EMP_REIMBURSEMENT.REIMBURSEMENT_CODE='" + obj.REIMBURSEMENT_CODE + "'", trans)
            End If


            isSaved = isSaved AndAlso clsReimbursementPayHeadDetails.SaveData(obj.REIMBURSEMENT_CODE, objList, trans)
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
            Dim obj As clsReimbursementDetails = clsReimbursementDetails.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.REIMBURSEMENT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_EMP_REIMBURSEMENT set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where REIMBURSEMENT_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select POSTED from TSPL_EMP_REIMBURSEMENT where REIMBURSEMENT_CODE='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If


            Qry = "Update TSPL_EMP_REIMBURSEMENT set POSTED = 0 where REIMBURSEMENT_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetRegisterDT(ByVal strFromPP As String, ByVal strToPP As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT TT1.REIMBURSEMENT_CODE ,TT1.REIMBURSEMENT_DATE ,TT2.REIMBURSEMENT_AMOUNT ,TT1.REIMBURSEMENT_REMARK ,"
            qry += " TT1.PAY_PERIOD_CODE ,TT1.PAY_PERIOD_NAME ,"
            qry += " TT1.EMP_CODE ,TT1.Emp_Name ,TT1.POSTED  FROM ("
            qry += " SELECT T1.REIMBURSEMENT_CODE,T1.REIMBURSEMENT_DATE,T1.PAY_PERIOD_CODE,T2.PAY_PERIOD_NAME,T1.EMP_CODE,T3.Emp_Name,"
            qry += " T1.REIMBURSEMENT_REMARK, T1.POSTED"
            qry += " FROM TSPL_EMP_REIMBURSEMENT T1 LEFT JOIN TSPL_PAYPERIOD_MASTER T2 ON T1.PAY_PERIOD_CODE=T2.PAY_PERIOD_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T3 ON T1.EMP_CODE=T3.EMP_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T4 ON T1.EMP_CODE=T4.EMP_CODE "
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " WHERE T2.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strToPP + "')"
            End If
            qry += " ) AS TT1 LEFT JOIN (SELECT REIMBURSEMENT_CODE,SUM(REIMBURSEMENT_AMOUNT) AS REIMBURSEMENT_AMOUNT FROM TSPL_EMPREIMBURSEMENT_DETAIL GROUP BY REIMBURSEMENT_CODE) AS TT2"
            qry += " ON TT1.REIMBURSEMENT_CODE=TT2.REIMBURSEMENT_CODE ;"

            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

    Public Shared Function GetRegisterDTDetailed(ByVal strFromPP As String, ByVal strToPP As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry += " SELECT T1.REIMBURSEMENT_CODE ,T2.PAY_HEAD_CODE ,T5.PAY_HEAD_NAME ,"
            qry += " T2.EMP_CODE ,T4.Emp_Name ,T2.REIMBURSEMENT_AMOUNT  FROM TSPL_EMP_REIMBURSEMENT T1 "
            qry += " INNER JOIN TSPL_EMPREIMBURSEMENT_DETAIL T2 ON T1.REIMBURSEMENT_CODE=T2.REIMBURSEMENT_CODE"
            qry += " LEFT JOIN TSPL_PAYPERIOD_MASTER T3 ON T1.PAY_PERIOD_CODE=T3.PAY_PERIOD_CODE "
            qry += " LEFT JOIN TSPL_EMPLOYEE_MASTER T4 ON T2.EMP_CODE=T4.EMP_CODE "
            qry += " LEFT JOIN TSPL_PAYHEAD_MASTER T5 ON T2.PAY_HEAD_CODE=T5.PAY_HEAD_CODE"
            If clsCommon.myLen(strFromPP) > 0 AndAlso clsCommon.myLen(strToPP) > 0 Then
                qry += " WHERE T3.DATE_FROM BETWEEN "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strFromPP + "') AND "
                qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + strToPP + "') "
            End If
            dt = clsDBFuncationality.GetDataTable(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function

End Class

Public Class clsReimbursementPayHeadDetails
#Region "Variables"
    Public empCode As String
    Public PayHeadCode As String
    Public Reimburse_amt As Decimal


    Public Shared ObjList As List(Of clsAdjustmentVoucher)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsReimbursementDetails), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsReimbursementDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "emp_Code", obj.empCode)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PayHeadCode)
                clsCommon.AddColumnsForChange(coll, "REIMBURSEMENT_AMOUNT", obj.REIMBURSEMENT_AMOUNT)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPREIMBURSEMENT_DETAIL", OMInsertOrUpdate.Insert, "TSPL_EMPREIMBURSEMENT_DETAIL.REIMBURSEMENT_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
End Class
