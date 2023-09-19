Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLeaveAdjustment

#Region "Variables"

    Public LVADJUSTMENT_CODE As String
    Public EMP_CODE As String
    Public ADJUSTMENT_DATE As DateTime
    Public PAY_PERIOD_CODE As String
    Public LEAVE_CODE As String
    Public LEAVE_REASON As String
    Public ADJUST_ALLOTED As Double
    Public ADJUST_AVAILED As Double
    Public POSTED As Boolean
    Public Posting_Date As DateTime? = Nothing
    Public Location_Code As String

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLeaveAdjustment
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean

        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("LVADJUSTMENT_CODE not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_LEAVE_ADJUSTMENT where LVADJUSTMENT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLeaveAdjustment
        Dim obj As clsLeaveAdjustment = Nothing
        Dim whrcls As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrcls = " And Location_Code=" + objCommonVar.strCurrUserLocations + ""
        End If
        Dim qry As String = "select * from TSPL_LEAVE_ADJUSTMENT where 2=2" + whrcls
        Select Case NavType
            Case NavigatorType.First
                qry += " and LVADJUSTMENT_CODE = (select MIN(LVADJUSTMENT_CODE) from TSPL_LEAVE_ADJUSTMENT)"
            Case NavigatorType.Last
                qry += " and LVADJUSTMENT_CODE = (select Max(LVADJUSTMENT_CODE) from TSPL_LEAVE_ADJUSTMENT)"
            Case NavigatorType.Next
                qry += " and LVADJUSTMENT_CODE = (select Min(LVADJUSTMENT_CODE) from TSPL_LEAVE_ADJUSTMENT where  LVADJUSTMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and LVADJUSTMENT_CODE = (select Max(LVADJUSTMENT_CODE) from TSPL_LEAVE_ADJUSTMENT where LVADJUSTMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and LVADJUSTMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLeaveAdjustment()
            obj.LVADJUSTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("LVADJUSTMENT_CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.LEAVE_CODE = clsCommon.myCstr(dt.Rows(0)("LEAVE_CODE"))
            obj.LEAVE_REASON = clsCommon.myCstr(dt.Rows(0)("LEAVE_REASON"))
            obj.ADJUSTMENT_DATE = clsCommon.GetPrintDate(dt.Rows(0)("ADJUSTMENT_DATE"), "dd/MMM/yyyy")
            obj.ADJUST_ALLOTED = clsCommon.myCdbl(dt.Rows(0)("ADJUST_ALLOTED"))
            obj.ADJUST_AVAILED = clsCommon.myCdbl(dt.Rows(0)("ADJUST_AVAILED"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If dt.Rows(0)("Posting_Date") IsNot DBNull.Value Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            If clsCommon.myLen(dt.Rows(0)("Location_Code")) > 0 Then
                obj.Location_Code = clsCommon.myCDate(dt.Rows(0)("Location_Code"))
            Else
                obj.Location_Code = Nothing
            End If
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsLeaveAdjustment, ByVal strCode As String, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", obj.LEAVE_CODE)
            clsCommon.AddColumnsForChange(coll, "LEAVE_REASON", obj.LEAVE_REASON)
            clsCommon.AddColumnsForChange(coll, "ADJUSTMENT_DATE", clsCommon.GetPrintDate(obj.ADJUSTMENT_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "POSTED", obj.POSTED)
            clsCommon.AddColumnsForChange(coll, "ADJUST_AVAILED", obj.ADJUST_AVAILED)
            clsCommon.AddColumnsForChange(coll, "ADJUST_ALLOTED", obj.ADJUST_ALLOTED)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            If isNewEntry Then
                If strCode = "" Then
                    obj.LVADJUSTMENT_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.ADJUSTMENT_DATE, "dd/MMM/yyyy"), clsDocType.LeaveAdjustment, "", "")
                Else
                    obj.LVADJUSTMENT_CODE = strCode
                End If

                clsCommon.AddColumnsForChange(coll, "LVADJUSTMENT_CODE", obj.LVADJUSTMENT_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_LEAVE_ADJUSTMENT where LVADJUSTMENT_CODE= '" & obj.LVADJUSTMENT_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_ADJUSTMENT", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This LVADJUSTMENT_CODE Is Already Exist")

                End If

            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_ADJUSTMENT", OMInsertOrUpdate.Update, "LVADJUSTMENT_CODE='" + obj.LVADJUSTMENT_CODE + "'")
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
            Dim obj As clsLeaveAdjustment = clsLeaveAdjustment.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.LVADJUSTMENT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_LEAVE_ADJUSTMENT set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where LVADJUSTMENT_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
