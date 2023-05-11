Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsOTSheet

#Region "Variables"
    Public Code As String
    Public EMP_CODE As String
    Public OT_CODE As String
    Public Emp_Name As String
    Public OT_NAME As String
    Public OT_RATE As Double
    Public OT_HOURS As Double
    Public OT_TOTAL_AMOUNT As Double
    Public PAY_PERIOD_CODE As String
    Public PAY_PERIOD_NAME As String
    Public POSTED As Boolean

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsOTSheet
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
            qry = "delete from TSPL_OT_SHEET where OT_SHEET_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)


        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsOTSheet = clsOTSheet.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.OT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
       
            Dim qry As String = "Update tspl_ot_sheet set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where OT_Sheet_Code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsOTSheet
        Dim obj As clsOTSheet = Nothing
        Dim qry As String = ""
        qry += " select TSPL_OT_SHEET.*, TSPL_OT_MASTER.OT_NAME, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME from TSPL_OT_SHEET "
        qry += " left outer join TSPL_OT_MASTER on TSPL_OT_MASTER.OT_CODE= TSPL_OT_SHEET.OT_CODE  "
        qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE= TSPL_OT_SHEET.EMP_CODE "
        qry += " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE= TSPL_OT_SHEET.PAY_PERIOD_CODE where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and OT_SHEET_CODE = (select MIN(OT_SHEET_CODE) from TSPL_OT_SHEET)"
            Case NavigatorType.Last
                qry += " and OT_SHEET_CODE = (select Max(OT_SHEET_CODE) from TSPL_OT_SHEET)"
            Case NavigatorType.Next
                qry += " and OT_SHEET_CODE = (select Min(OT_SHEET_CODE) from TSPL_OT_SHEET where  OT_SHEET_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and OT_SHEET_CODE = (select Max(OT_SHEET_CODE) from TSPL_OT_SHEET where OT_SHEET_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and OT_SHEET_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsOTSheet()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("OT_SHEET_CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.OT_CODE = clsCommon.myCstr(dt.Rows(0)("OT_CODE"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.OT_NAME = clsCommon.myCstr(dt.Rows(0)("OT_NAME"))
            obj.OT_RATE = clsCommon.myCdbl(dt.Rows(0)("OT_RATE"))
            obj.OT_HOURS = clsCommon.myCdbl(dt.Rows(0)("OT_HOURS"))
            obj.OT_TOTAL_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("OT_TOTAL_AMOUNT"))
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.PAY_PERIOD_NAME = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
        End If
        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsOTSheet, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "OT_CODE", obj.OT_CODE)
            clsCommon.AddColumnsForChange(coll, "OT_RATE", obj.OT_RATE)
            clsCommon.AddColumnsForChange(coll, "OT_HOURS", obj.OT_HOURS)
            clsCommon.AddColumnsForChange(coll, "OT_TOTAL_AMOUNT", obj.OT_TOTAL_AMOUNT)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            If isNewEntry Then
                If clsCommon.myLen(obj.Code) <= 0 Then
                    obj.Code = clsERPFuncationality.GetNextCode(Nothing, clsCommon.myCDate(clsCommon.GETSERVERDATE()), clsDocType.OTSheet, "", "")
                    If clsCommon.myLen(obj.Code) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "OT_SHEET_CODE", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_OT_SHEET where OT_SHEET_CODE= '" & obj.Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SHEET", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_OT_SHEET", OMInsertOrUpdate.Update, "OT_SHEET_CODE='" + obj.Code + "'")
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Function CheckOTCodeExist(ByVal OT_Sheet_Code As String) As Boolean
        Dim strq As String
        strq = "SELECT OT_SHEET_CODE FROM TSPL_OT_SHEET WHERE OT_SHEET_CODE='" & OT_Sheet_Code & "'"
        ' Dim isNew As Boolean
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            Return False
        End If
        Return True
    End Function
End Class
