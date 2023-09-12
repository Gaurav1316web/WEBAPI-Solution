Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLeaveApplication

#Region "Variables"
    Public LVAPPLICATION_CODE As String
    Public EMP_CODE As String
    Public LEAVE_CODE As String
    Public PAY_PERIOD_CODE As String
    Public APPLICATION_DATE As DateTime
    Public FROM_DATE As DateTime
    Public TO_DATE As DateTime
    Public FIRST_HALF As Boolean
    Public SEC_HALF As Boolean
    Public TOTAL_DAYS As Double
    Public LEAVE_REASON As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public Location_Code As String

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLeaveApplication
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
            qry = "delete from TSPL_LEAVE_APPLICATION where LVAPPLICATION_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLeaveApplication
        Dim StrJoin As String = ""
        Dim StrWhere As String = ""
        Dim whrQry As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrQry = " and tspl_user_master.Default_Location=" + objCommonVar.strCurrUserLocations + ""
        End If

        If objCommonVar.IsLoginUserHRAdmin = True Then
            StrJoin = ""
            StrWhere = ""
        Else
            StrJoin = " LEFT JOIN tspl_user_master ON TSPL_LEAVE_APPLICATION.EMP_CODE=tspl_user_master.EMP_CODE "
            StrWhere = " and tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "' " + whrQry
        End If

        Dim obj As clsLeaveApplication = Nothing
        Dim qry As String = "select * from TSPL_LEAVE_APPLICATION where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and LVAPPLICATION_CODE = (select MIN(LVAPPLICATION_CODE) from TSPL_LEAVE_APPLICATION " + StrJoin + " where 1=1 " + StrWhere + ")"
            Case NavigatorType.Last
                qry += " and LVAPPLICATION_CODE = (select Max(LVAPPLICATION_CODE) from TSPL_LEAVE_APPLICATION " + StrJoin + " where 1=1 " + StrWhere + ")"
            Case NavigatorType.Next
                qry += " and LVAPPLICATION_CODE = (select Min(LVAPPLICATION_CODE) from TSPL_LEAVE_APPLICATION " + StrJoin + " where  LVAPPLICATION_CODE > '" + strCode + "' " + StrWhere + ")"
            Case NavigatorType.Previous
                qry += " and LVAPPLICATION_CODE = (select Max(LVAPPLICATION_CODE) from TSPL_LEAVE_APPLICATION " + StrJoin + " where LVAPPLICATION_CODE < '" + strCode + "' " + StrWhere + ")"
            Case NavigatorType.Current
                qry += " and LVAPPLICATION_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLeaveApplication()
            obj.LVAPPLICATION_CODE = clsCommon.myCstr(dt.Rows(0)("LVAPPLICATION_CODE"))
            obj.LEAVE_CODE = clsCommon.myCstr(dt.Rows(0)("LEAVE_CODE"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))

            If clsCommon.myLen(dt.Rows(0)("APPLICATION_DATE")) > 0 Then
                obj.APPLICATION_DATE = clsCommon.GetPrintDate(dt.Rows(0)("APPLICATION_DATE"), "dd/MMM/yyyy")
            Else
                obj.APPLICATION_DATE = Nothing
            End If

            If clsCommon.myLen(dt.Rows(0)("FROM_DATE")) > 0 Then
                obj.FROM_DATE = clsCommon.GetPrintDate(dt.Rows(0)("FROM_DATE"), "dd/MMM/yyyy")
            Else
                obj.FROM_DATE = Nothing
            End If

            If clsCommon.myLen(dt.Rows(0)("TO_DATE")) > 0 Then
                obj.TO_DATE = clsCommon.GetPrintDate(dt.Rows(0)("TO_DATE"), "dd/MMM/yyyy")
            Else
                obj.TO_DATE = Nothing
            End If
            obj.FIRST_HALF = clsCommon.myCBool(dt.Rows(0)("FIRST_HALF"))
            obj.SEC_HALF = clsCommon.myCBool(dt.Rows(0)("SEC_HALF"))
            obj.TOTAL_DAYS = clsCommon.myCdbl(dt.Rows(0)("TOTAL_DAYS"))
            obj.LEAVE_REASON = clsCommon.myCstr(dt.Rows(0)("LEAVE_REASON"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            If clsCommon.myLen(dt.Rows(0)("Location_Code")) > 0 Then
                obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            End If
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsLeaveApplication, ByVal strCode As String, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", obj.LEAVE_CODE)
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "APPLICATION_DATE", clsCommon.GetPrintDate(obj.APPLICATION_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "FROM_DATE", clsCommon.GetPrintDate(obj.FROM_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "TO_DATE", clsCommon.GetPrintDate(obj.TO_DATE, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "FIRST_HALF", obj.FIRST_HALF)
            clsCommon.AddColumnsForChange(coll, "SEC_HALF", obj.SEC_HALF)
            clsCommon.AddColumnsForChange(coll, "TOTAL_DAYS", obj.TOTAL_DAYS)
            clsCommon.AddColumnsForChange(coll, "LEAVE_REASON", obj.LEAVE_REASON)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            If isNewEntry Then
                If strCode = "" Then
                    obj.LVAPPLICATION_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.APPLICATION_DATE, "dd/MMM/yyyy"), clsDocType.LeaveApplication, "", "")
                Else
                    obj.LVAPPLICATION_CODE = strCode
                End If

                clsCommon.AddColumnsForChange(coll, "LVAPPLICATION_CODE", obj.LVAPPLICATION_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_LEAVE_APPLICATION where LVAPPLICATION_CODE= '" & obj.LVAPPLICATION_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_APPLICATION", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_APPLICATION", OMInsertOrUpdate.Update, "LVAPPLICATION_CODE='" + obj.LVAPPLICATION_CODE + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal Form_Id As String, ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsLeaveApplication = clsLeaveApplication.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.LVAPPLICATION_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            'Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(Form_Id, "TSPL_LEAVE_APPLICATION", "LVAPPLICATION_CODE", obj.LVAPPLICATION_CODE, "Modified_By", "Modified_Date", Nothing)
            'If isResult = False Then
            '    Return False
            'End If
            Dim qry As String = "Update TSPL_LEAVE_APPLICATION set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where LVAPPLICATION_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetGrid_Data(ByVal strEmpCode As String, ByVal strLeaveCode As String) As DataTable
        Dim qry As String = ""
        qry += "select * from TSPL_LEAVE_APPLICATION where 2=2 and LVAPPLICATION_CODE = '" + strEmpCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function


End Class
Public Class ClsMailReceipt
#Region "variables"
    Public Form_Id As String
    Public User_Code As String
#End Region
    Public Shared Function SaveData(ByVal Arr As List(Of ClsMailReceipt), ByVal Form_Id As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

        Try
            Dim qry As String = "delete  from TSPL_Mail_Receipt where Form_Id='" & Form_Id & "' "

            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsMailReceipt In Arr

                Dim IsSaved As Boolean = False
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "Form_Id", Form_Id)
                clsCommon.AddColumnsForChange(coll, "User_Code", obj.User_Code)

                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mail_Receipt", OMInsertOrUpdate.Insert, "", trans)

            Next
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
End Class