Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsTrainingAttendance

#Region "Variables"

    Public Doc_Code As String
    Public Doc_Date As DateTime
    Public Schedule_Start_Date As DateTime
    Public Schedule_End_Date As DateTime
    Public Schedule_Start_Time As DateTime
    Public Schedule_End_TIme As DateTime
    Public Trainer_Code As String
    Public Remarks As String
    Public Training_Course As String
    Public Venue As String
    Public Training_Mode As String

    Public Trainer_Name As String
    Public Training_Name As String


    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    '' grid columns

    Public Shared ObjList As List(Of clsTrainingAttendanceDetail)
    Public Shared dt_saved_Attendance As DataTable
    Public Shared ObjList_Resource As List(Of clsTrainingAttendanceResourceDetail)
    Public Shared objResource As ArrayList

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsTrainingAttendance
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
            qry = "delete from TSPL_Schedule_Training_Resource_Detail where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_Schedule_Training_Employee_Detail where DOC_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_Schedule_Training_Head where DOC_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsTrainingAttendance

        Dim obj As New clsTrainingAttendance()
        Dim objtr As New clsTrainingAttendanceDetail
        Dim objtr_Resource As New clsTrainingAttendanceResourceDetail

        ObjList = New List(Of clsTrainingAttendanceDetail)
        ObjList_Resource = New List(Of clsTrainingAttendanceResourceDetail)
        objResource = New ArrayList
        clsTrainingAttendance.dt_saved_Attendance = New DataTable

        Dim qry As String = "SELECT TSPL_Schedule_Training_HEAD.*,tm.DOC_Name " _
        & " as Traininer_name,pid.Name as Training_Course   FROM TSPL_Schedule_Training_HEAD  Left JOIN Tspl_Trainer_Master tm   ON TSPL_Schedule_Training_HEAD.Trainer_Code=" _
        & " tm.DOC_Code Left join  Tspl_Training_Master pid on pid.Code= TSPL_Schedule_Training_HEAD.Training_Course  " _
        & "  where 2=2  "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND TSPL_Schedule_Training_HEAD.DOC_CODE = (select MIN(DOC_CODE) from TSPL_Schedule_Training_HEAD)"
            Case NavigatorType.Last
                qry += " AND TSPL_Schedule_Training_HEAD.DOC_CODE = (select Max(DOC_CODE) from TSPL_Schedule_Training_HEAD)"
            Case NavigatorType.Next
                qry += " AND TSPL_Schedule_Training_HEAD.DOC_CODE = (select Min(DOC_CODE) from TSPL_Schedule_Training_HEAD where  DOC_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND TSPL_Schedule_Training_HEAD.DOC_CODE = (select Max(DOC_CODE) from TSPL_Schedule_Training_HEAD where DOC_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND TSPL_Schedule_Training_HEAD.DOC_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then

            obj.Doc_Code = dt.Rows(0)("DOC_CODE")
            obj.Doc_Date = clsCommon.GetPrintDate(dt.Rows(0)("DOC_DATE"), "dd/MMM/yyyy")
            obj.Schedule_Start_Date = clsCommon.GetPrintDate(dt.Rows(0)("Schedule_Start_Date"), "dd/MMM/yyyy hh:mm:ss tt")
            obj.Schedule_End_Date = clsCommon.GetPrintDate(dt.Rows(0)("Schedule_End_Date"), "dd/MMM/yyyy hh:mm:ss tt")
            obj.Schedule_Start_Time = clsCommon.GetPrintDate(dt.Rows(0)("Schedule_Start_Date"), "dd/MMM/yyyy hh:mm:ss tt")
            obj.Schedule_End_TIme = clsCommon.GetPrintDate(dt.Rows(0)("Schedule_End_Date"), "dd/MMM/yyyy hh:mm:ss tt")

            obj.Trainer_Code = clsCommon.myCstr(dt.Rows(0)("Trainer_Code"))

            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Training_Course = clsCommon.myCstr(dt.Rows(0)("Training_Course"))
            obj.Venue = clsCommon.myCstr(dt.Rows(0)("Venue"))
            obj.Training_Mode = clsCommon.myCstr(dt.Rows(0)("Training_Mode"))

            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("isPosted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            '        strCode = dt.Rows(0)("DOC_CODE")

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
        End If
        qry = " SELECT Distinct DEPARTMENT_Name,Request_Code as Request_Code,sd.DOC_DATE ,pid.EMP_CODE as Emp_Code,Emp_Name,pid.Remarks " _
        & " FROM TSPL_Schedule_Training_Employee_DETAIL  pid Inner join TSPL_Schedule_Training_HEAD pih on pid.DOC_CODE=pih.DOC_CODE  Left join " _
        & " Tspl_Request_For_Training_Master sd on sd.Code=Request_Code  Left join TSPL_EMPLOYEE_MASTER em on pid.EMP_CODE=em.EMP_CODE Left join " _
        & " TSPL_DEPARTMENT_MASTER dm on dm.DEPARTMENT_CODE=em.DEPARTMENT_CODE WHERE 2=2 " _
        & " AND pid.DOC_CODE = '" + strCode + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsTrainingAttendanceDetail

                objtr.DOC_CODE = strCode
                objtr.Remark = clsCommon.myCstr(dr("Remarks"))
                objtr.DOC_Request_Code = clsCommon.myCstr(dr("Request_Code"))
                objtr.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                objtr.Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                objtr.Dept = clsCommon.myCstr(dr("DEPARTMENT_Name"))
                objtr.DOC_R_Date = clsCommon.myCstr(dr("DOC_DATE"))
                ObjList.Add(objtr)
            Next
        End If

        clsTrainingAttendance.ObjList = ObjList

        qry = " SELECT Distinct DEPARTMENT_Name,Request_Code as Request_Code,sd.DOC_DATE ,pid.EMp_Code as Emp_Code,Emp_Name,pid.Remarks,attendance,user_status " _
       & " FROM TSPL_Schedule_Training_Employee_DETAIL  pid inner join TSPL_Schedule_Training_HEAD pih on pid.DOC_CODE=pih.DOC_CODE  Left join " _
       & " Tspl_Request_For_Training_Master sd on sd.Code=Request_Code  Left join TSPL_EMPLOYEE_MASTER em on pid.EMp_Code=em.EMP_CODE Left join " _
       & " TSPL_DEPARTMENT_MASTER dm on dm.DEPARTMENT_CODE=em.DEPARTMENT_CODE left join tspl_training_attendance ta on ta.employee_Code=pid.EMP_CODE " _
       & " and convert(date,attendance,103) between convert(date,Schedule_Start_Date,103) and convert(date,Schedule_end_Date,103) WHERE 2=2 " _
       & " AND pid.DOC_CODE = '" + strCode + "'"


        clsTrainingAttendance.dt_saved_Attendance = clsDBFuncationality.GetDataTable(qry, trans)

       
        qry = " SELECT Resource_Code     FROM TSPL_Schedule_Training_Resource_DETAIL pid  WHERE 2=2  " _
        & " AND pid.DOC_CODE = '" + strCode + "' ORDER BY pid.Resource_Code"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr_Resource = New clsTrainingAttendanceResourceDetail
                'objtr_Resource.DOC_CODE = strCode
                objtr_Resource.DOC_Resource_Code = clsCommon.myCstr(dr("Resource_Code"))
                objResource.Add(objtr_Resource.DOC_Resource_Code)
            Next
        End If

        clsTrainingAttendance.objResource = objResource

        Return obj
    End Function

    Public Function Gettable(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As DataTable
        Dim qry As String = " SELECT DOC_CODE as [DOC CODE],VLC_DOC_CODE as [VLC DOC CODE],SAMPLE_NO as [SAMPLE NO],VLC_CODE as [VLC CODE],TSPL_ITEM_MASTER.Item_Code + '(' + Item_Desc + ')' as [Item],ROUTE_CODE as [ROUTE CODE],VSP_CODE as [VSP CODE],VEHICLE_CODE as [VEHICLE CODE], "
        qry += " NO_OF_CANS as [NO OF CANS],MILK_WEIGHT as [MILK WEIGHT],TYPE,MILK_TYPE as [MILK TYPE],FAT,SNF,SAMPLE_NO_VALUES as [SAMPLE NO VALUES],MCC_CODE as [MCC CODE],DOC_DATE as [DOC DATE],SHIFT,COMM_PORT as [COM PORT],MACHINE_NO as [MACHINE NO],Case when IS_MANUAL='N' then 'Auto' else 'Manual' end as [Entry Type] FROM TSPL_Schedule_Training_Employee_DETAIL inner join tspl_item_master on tspl_item_master.item_Code=TSPL_Schedule_Training_Employee_DETAIL.item_Code  WHERE 2=2"
        qry += " AND TSPL_Schedule_Training_Employee_DETAIL.DOC_CODE = '" + strCode + "' ORDER BY TSPL_Schedule_Training_Employee_DETAIL.VLC_DOC_CODE"

        Dim dt As DataTable = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Return dt
    End Function

    Public Shared Function SaveData(ByVal obj As clsTrainingAttendance, ByVal objList As List(Of clsTrainingAttendanceDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
           
            Dim Count As Integer = 0
            Dim StrTem As String = ""
            For Each obj1 As clsTrainingAttendanceDetail In objList
                StrTem = " select Count(*) From TSPL_Training_Attendance where Employee_Code='" + obj1.Emp_Code + "' and Attendance='" + clsCommon.GetPrintDate(obj1.Attendance, "dd/MMM/yyyy") + "'"
                Count = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) From TSPL_Training_Attendance where Employee_Code='" + obj1.Emp_Code + "' and Attendance='" + clsCommon.GetPrintDate(obj1.Attendance, "dd/MMM/yyyy") + "'", trans))
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Employee_Code", obj1.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Attendance", clsCommon.GetPrintDate(obj1.Attendance, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "USER_Status", obj1.USER_Status)
                clsCommon.AddColumnsForChange(coll, "STATUS", IIf(obj1.STATUS, 1, 0))
                clsCommon.AddColumnsForChange(coll, "CREATED_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "MODIFY_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "MODIFY_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode, True)
                If Count <= 0 Then
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Training_Attendance", OMInsertOrUpdate.Insert, "", trans)
                Else
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Training_Attendance", OMInsertOrUpdate.Update, "TSPL_Training_Attendance.Employee_Code='" + obj1.Emp_Code + "' and TSPL_Training_Attendance.Attendance='" + clsCommon.GetPrintDate(obj1.Attendance, "dd/MMM/yyyy") + "'", trans)
                End If
            Next
            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsTrainingAttendance = clsTrainingAttendance.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_Schedule_Training_HEAD set isPOSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetPost(ByVal DocDate As Date, ByVal DOC_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim qry As String
        qry = "SELECT isPosted FROM TSPL_Schedule_Training_HEAD WHERE DOC_CODE='" & DOC_Code & "' AND DOC_DATE='" & clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") & "'  and isPosted=1"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class


Public Class clsTrainingAttendanceDetail
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_Request_Code As String
    Public DOC_R_Date As String
    Public Emp_Code As String
    Public Dept As String
    Public Emp_Name As String
    Public Remark As String


    Public Attendance As String = Nothing
    Public USER_Status As String = Nothing
    Public STATUS As Boolean = False
    Public CREATED_DATE As Date
    Public Created_By As String
    Public MODIFY_DATE As Date
    Public MODIFY_By As String

#End Region

End Class



Public Class clsTrainingAttendanceResourceDetail
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_Resource_Code As String
#End Region
End Class




