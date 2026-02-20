Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsScheduleForTraining

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

    Public ObjList As List(Of clsScheduleForTrainingDetail)
    Public ObjList_Resource As List(Of clsScheduleForTrainingResourceDetail)
    Public objResource As ArrayList

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsScheduleForTraining
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
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsScheduleForTraining

        Dim obj As New clsScheduleForTraining()
        Dim objtr As New clsScheduleForTrainingDetail
        Dim objtr_Resource As New clsScheduleForTrainingResourceDetail

        obj.ObjList = New List(Of clsScheduleForTrainingDetail)
        obj.ObjList_Resource = New List(Of clsScheduleForTrainingResourceDetail)
        obj.objResource = New ArrayList

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
            Dim qry1 As String = clsDBFuncationality.getSingleValue(" select City_Name  from TSPL_CITY_MASTER where City_Code ='" + clsCommon.myCstr(dt.Rows(0)("Venue")) + "'")
            obj.Venue = qry1
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
        qry = "  SELECT Request_Code as Request_Code,sd.DOC_DATE ,pid.EMP_CODE as Emp_Code,Emp_Name,pid.Remarks   FROM TSPL_Schedule_Training_Employee_DETAIL " _
        & " pid Inner join TSPL_Schedule_Training_HEAD pih on pid.DOC_CODE=pih.DOC_CODE  Left join Tspl_Request_For_Training_Master sd on sd.Code=Request_Code " _
        & " Left join TSPL_EMPLOYEE_MASTER em on pid.EMP_CODE=em.EMP_CODE WHERE 2=2 " _
        & " AND pid.DOC_CODE = '" + obj.Doc_Code + "' ORDER BY sd.Code"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsScheduleForTrainingDetail

                objtr.DOC_CODE = strCode
                objtr.Remark = clsCommon.myCstr(dr("Remarks"))
                objtr.DOC_Request_Code = clsCommon.myCstr(dr("Request_Code"))
                objtr.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                objtr.Emp_Name = clsCommon.myCstr(dr("Emp_Name"))
                objtr.DOC_R_Date = clsCommon.myCstr(dr("DOC_DATE"))
                obj.ObjList.Add(objtr)
            Next
        End If

        'obj.ObjList = ObjList

        qry = " SELECT Resource_Code     FROM TSPL_Schedule_Training_Resource_DETAIL pid  WHERE 2=2  " _
        & " AND pid.DOC_CODE = '" + strCode + "' ORDER BY pid.Resource_Code"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr_Resource = New clsScheduleForTrainingResourceDetail
                'objtr_Resource.DOC_CODE = strCode
                objtr_Resource.DOC_Resource_Code = clsCommon.myCstr(dr("Resource_Code"))
                obj.objResource.Add(objtr_Resource.DOC_Resource_Code)
            Next
        End If

        'obj.objResource = objResource

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

    Public Shared Function SaveData(ByVal obj As clsScheduleForTraining, ByVal objList As List(Of clsScheduleForTrainingDetail), ByVal objList_Resource As List(Of clsScheduleForTrainingResourceDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim isNewEntry As Boolean
            If GetPost(obj.Doc_Date, obj.Doc_Code, trans) Then
                Throw New Exception("This Code:" + obj.Doc_Code + " Is Posted.")
            End If
            If clsCommon.myLen(obj.Doc_Code) <= 0 Then
                isNewEntry = True
                obj.Doc_Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.Sch_Training, "", "")
            Else
                isNewEntry = False
                Dim squery As String = "delete from TSPL_Schedule_Training_Employee_DETAIL where Doc_Code='" & obj.Doc_Code & "'"
                clsDBFuncationality.ExecuteNonQuery(squery, trans)
                'obj.DOC_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.MilkReceipt, "", "")
            End If


            If (clsCommon.myLen(obj.Doc_Code) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOC_CODE", obj.Doc_Code)
            clsCommon.AddColumnsForChange(coll, "Trainer_Code", obj.Trainer_Code)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(obj.Doc_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Schedule_Start_DATE", clsCommon.GetPrintDate(obj.Schedule_Start_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Schedule_ENd_DATE", clsCommon.GetPrintDate(obj.Schedule_End_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)

            clsCommon.AddColumnsForChange(coll, "Training_Course", obj.Training_Course)
            clsCommon.AddColumnsForChange(coll, "Venue", obj.Venue)
            clsCommon.AddColumnsForChange(coll, "Training_Mode", obj.Training_Mode)

            clsCommon.AddColumnsForChange(coll, "isPOSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_Schedule_Training_HEAD where DOC_CODE = '" & obj.Doc_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Schedule_Training_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code:" + obj.Doc_Code + " Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Schedule_Training_HEAD", OMInsertOrUpdate.Update, "TSPL_Schedule_Training_HEAD.DOC_CODE='" + obj.Doc_Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsScheduleForTrainingDetail.SaveData(obj.Doc_Code, objList, trans)
            isSaved = isSaved AndAlso clsScheduleForTrainingResourceDetail.SaveData(obj.Doc_Code, objList_Resource, trans)

            'If isSaved Then
            '    trans.Commit()
            'End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
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
            Dim obj As clsScheduleForTraining = clsScheduleForTraining.GetData(strDocNo, NavigatorType.Current)

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


Public Class clsScheduleForTrainingDetail
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_Request_Code As String
    Public DOC_R_Date As String
    Public Emp_Code As String
    Public Emp_Name As String
    Public Remark As String
#End Region

    Function Open_request_Code_finder(ByVal strcode As String) As clsScheduleForTrainingDetail

        Dim objtr As New clsScheduleForTrainingDetail


        Dim sQuery As String = "select Code,DOC_DATE as [Date],Remark,Training_Code,Employee_Code  from Tspl_Request_For_Training_Master"
        strcode = clsCommon.ShowSelectForm("Req_Training_Code", sQuery, "Code", "", strcode, "Code", True)
        sQuery = "select tm. *,emp_name  from Tspl_Request_For_Training_Master tm left join tspl_Employee_Master em on em.emp_code=employee_Code where Code='" & strcode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            objtr = New clsScheduleForTrainingDetail

            objtr.DOC_Request_Code = strcode
            objtr.DOC_R_Date = dt.Rows(0)("Doc_Date")
            objtr.Emp_Code = dt.Rows(0)("Employee_Code")
            objtr.Emp_Name = dt.Rows(0)("Emp_Name")
            objtr.Remark = ""
        End If
        Return objtr
    End Function

    Function Open_Employee_Code_finder(ByVal strcode As String) As clsScheduleForTrainingDetail

        Dim objtr As New clsScheduleForTrainingDetail

        Dim sQuery As String = "select Emp_Code as Code,Emp_Name as Name ,Designation,Phone from tspl_Employee_Master"
        strcode = clsCommon.ShowSelectForm("Emp_Code", sQuery, "Code", " lower(Emp_Status)='active'", strcode, "Code", True)
        sQuery = "select *  from tspl_Employee_Master where Emp_Code='" & strcode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            objtr = New clsScheduleForTrainingDetail

            objtr.Emp_Code = dt.Rows(0)("Emp_Code")
            objtr.Emp_Name = dt.Rows(0)("Emp_Name")
            objtr.Remark = ""
        End If
        Return objtr
    End Function


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsScheduleForTrainingDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsScheduleForTrainingDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Request_CODE", obj.DOC_Request_Code, True)
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.Emp_Code)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remark)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Schedule_Training_Employee_DETAIL", OMInsertOrUpdate.Insert, "TSPL_Schedule_Training_Employee_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
            Next
        End If
        Return True
    End Function
End Class

Public Class clsScheduleForTrainingResourceDetail
#Region "Variables"
    Public DOC_CODE As String
    Public DOC_Resource_Code As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsScheduleForTrainingResourceDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsScheduleForTrainingResourceDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Resource_CODE", obj.DOC_Resource_Code)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Schedule_Training_Resource_DETAIL", OMInsertOrUpdate.Insert, "TSPL_Schedule_Training_Resource_DETAIL.DOC_CODE='" + strDocNo + "'", trans)
            Next
        End If
        Return True
    End Function
End Class



