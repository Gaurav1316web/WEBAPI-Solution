Imports common
Imports System.Data.SqlClient
Public Class ClsRequestForTrainingMaster
#Region "variable"
    Public Code As String = Nothing
    Public Remark As String = Nothing
    Public Doc_Date As Date
    Public Training_Code As String = Nothing
    Public Employee_Code As String = Nothing
    Public tb_Name As String = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Shared ArrCourse_Arr As New ArrayList()
    Public ObjList As List(Of ClsRequestForTrainingMaster) = Nothing
    Public ObjDept As List(Of ClsRequestForTrainingMasterDept) = Nothing
    Dim ObjDetailDept As New ClsRequestForTrainingMasterDept()
    Public ObjEmp As List(Of ClsRequestForTrainingMasterEmp) = Nothing
    Dim ObjDetailEmp As New ClsRequestForTrainingMasterEmp()
#End Region
    Public Shared Function SaveData(ByVal obj As ClsRequestForTrainingMaster, ByVal isnewentry As Boolean)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select count(*) from Tspl_Request_For_Training_Master where Code='" + obj.Code + "'"
            Dim isexist As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If isexist = 0 Then
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.Doc_Date, clsDocType.RequestForTraining, "", "")
                isnewentry = True
            Else
                isnewentry = False
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark)
            clsCommon.AddColumnsForChange(coll, "Training_Code", obj.Training_Code)
            clsCommon.AddColumnsForChange(coll, "Employee_Code", obj.Employee_Code, True)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(obj.Doc_Date, "dd-MMM-yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posted", "0")
            If isnewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Request_For_Training_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Request_For_Training_Master ", OMInsertOrUpdate.Update, " Code='" + obj.Code + "'", trans)
            End If
            ClsRequestForTrainingMasterDept.SaveDataForDept(obj.Code, obj.ObjDept, trans)
            ClsRequestForTrainingMasterEmp.SaveDataForEmp(obj.Code, obj.ObjEmp, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function

    Public Function DeleteData(ByVal strcode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strcode) >= 0) Then
                Dim qry As String = "delete from Tspl_Request_For_Training_Master where code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            'Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsRequestForTrainingMaster = ClsRequestForTrainingMaster.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'If (isCheckForPosted AndAlso obj.POSTED = 1) Then
            '    Throw New Exception("Already Post on :" + obj.Posting_Date)
            'End If

            Dim qry As String = "Update Tspl_Request_For_Training_Master set POSTED=1 where Code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    '' ------------------------------------------------ Requested Data For Schedule Training ----------------------------------------------------
    'Public Shared Function GetRequestedData(ByVal TCode As String) As ClsRequestForTrainingMaster
    '    Try
    '        '=================== Requested Data Detail =================================================
    '        ArrCourse_Arr = New ArrayList

    '        Dim qst As String = "select * from Tspl_Request_For_Training_Master Where Training_Code  ='" & TCode & "'"

    '        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)

    '        If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
    '            For Each dr As DataRow In dt1.Rows
    '                ArrCourse_Arr.Add(clsCommon.myCstr(dr("Code")))
    '                ArrCourse_Arr.Add(clsCommon.myCDate(dr("DOC_DATE")))
    '                ArrCourse_Arr.Add(clsCommon.myCstr(dr("Employee_Code")))
    '                ArrCourse_Arr.Add(clsCommon.myCstr(dr("Remark")))
    '            Next
    '        End If
    '        '============================================================================================

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Function
    Public Shared Function GetRDData(ByVal code As String) As ClsRequestForTrainingMaster
        Try
            Dim obj As ClsRequestForTrainingMaster = Nothing
            Dim qst As String = ("select Code,Remark,Training_Code,Employee_Code,Doc_Date,Posted from Tspl_Request_For_Training_Master where 2=2 AND Training_Code ='" & code & "'")
            
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New ClsRequestForTrainingMaster
                obj.Code = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.Remark = clsCommon.myCstr(dt1.Rows(0)("Remark"))
                obj.Training_Code = clsCommon.myCstr(dt1.Rows(0)("Training_Code"))
                obj.Employee_Code = clsCommon.myCstr(dt1.Rows(0)("Employee_Code"))
                obj.Doc_Date = clsCommon.myCDate(dt1.Rows(0)("Doc_Date"))
                obj.ObjList = ClsRequestForTrainingMaster.GetRData(obj.Training_Code)
            End If
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetRData(ByVal strCode As String) As List(Of ClsRequestForTrainingMaster)
        Dim obj As ClsRequestForTrainingMaster = Nothing
        Dim ObjList As New List(Of ClsRequestForTrainingMaster)
        Dim qry As String = " select *  from Tspl_Request_For_Training_Master WHERE Training_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsRequestForTrainingMaster()
                obj.Code = clsCommon.myCstr(dr("Code"))
                obj.Remark = clsCommon.myCstr(dr("Remark"))
                obj.Employee_Code = clsCommon.myCstr(dr("Employee_Code"))
                obj.Doc_Date = clsCommon.myCDate(dr("DOC_DATE"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
    '' ------------------------------------------------------------------------------------------------------------------------------------------
    Public Shared Function GetData(ByVal code As String, ByVal navigatortype As NavigatorType) As ClsRequestForTrainingMaster
        Try

            Dim obj As ClsRequestForTrainingMaster = Nothing
            Dim qst As String = ("select Code,Remark,Training_Code,Employee_Code,Doc_Date,Posted from Tspl_Request_For_Training_Master where 2=2 ")
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and Code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and Code in (select  min(Code)from Tspl_Request_For_Training_Master where Code >'" + code + "')"
                Case navigatortype.First
                    qst += "and Code in (select MIN(Code)from Tspl_Request_For_Training_Master)"

                Case navigatortype.Last
                    qst += "and Code in (select Max(Code)from Tspl_Request_For_Training_Master)"
                Case navigatortype.Previous
                    qst += "and Code in (select  max(Code)from Tspl_Request_For_Training_Master where Code <'" + code + "')"

            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New ClsRequestForTrainingMaster
                obj.Code = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.Remark = clsCommon.myCstr(dt1.Rows(0)("Remark"))
                obj.Training_Code = clsCommon.myCstr(dt1.Rows(0)("Training_Code"))
                obj.Employee_Code = clsCommon.myCstr(dt1.Rows(0)("Employee_Code"))
                obj.Doc_Date = clsCommon.myCDate(dt1.Rows(0)("Doc_Date"))
                obj.Posted = IIf(clsCommon.myCdbl(dt1.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            End If
            obj.ObjDept = ClsRequestForTrainingMasterDept.GetDataForDept(obj.Code, Nothing)
            obj.ObjEmp = ClsRequestForTrainingMasterEmp.GetDataForEmp(obj.Code, Nothing)
            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
Public Class ClsRequestForTrainingMasterDept
#Region "Variables"
    Public Request_Code As String = Nothing
    Public Dept_Code As String = Nothing
    Public Dept_Name As String = Nothing
#End Region
    '' Department Grid Saving
    Public Shared Function SaveDataForDept(ByVal strCode As String, ByVal ObjDept As List(Of ClsRequestForTrainingMasterDept), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "Delete From Tspl_Request_For_Training_Master_Dept WHERE Request_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsRequestForTrainingMasterDept In ObjDept
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Request_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Department_Code", obj.Dept_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Request_For_Training_Master_Dept", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    '' Department Grid GetData
    Public Shared Function GetDataForDept(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsRequestForTrainingMasterDept)
        Dim obj As ClsRequestForTrainingMasterDept = Nothing
        Dim Objdept As New List(Of ClsRequestForTrainingMasterDept)
        Dim qry As String = " select *  from Tspl_Request_For_Training_Master_Dept WHERE Request_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsRequestForTrainingMasterDept()
                obj.Request_Code = clsCommon.myCstr(dr("Request_Code"))
                obj.Dept_Code = clsCommon.myCstr(dr("Department_Code"))
                Objdept.Add(obj)
            Next
        End If
        Return Objdept
    End Function
End Class
Public Class ClsRequestForTrainingMasterEmp
#Region "Variables"
    Public Dept_Code As String = Nothing
    Public Emp_Code As String = Nothing
    Public Emp_Name As String = Nothing
#End Region
    '' Emp Grid Saving
    Public Shared Function SaveDataForEmp(ByVal strCode As String, ByVal ObjEmp As List(Of ClsRequestForTrainingMasterEmp), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "Delete From Tspl_Request_For_Training_Master_Emp WHERE Request_Code ='" & strCode & "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsRequestForTrainingMasterEmp In ObjEmp
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Request_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Request_For_Training_Master_Emp", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    '' Emp Grid GetData
    Public Shared Function GetDataForEmp(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsRequestForTrainingMasterEmp)
        Dim obj As ClsRequestForTrainingMasterEmp = Nothing
        Dim ObjEmp As New List(Of ClsRequestForTrainingMasterEmp)
        Dim qry As String = " select *  from Tspl_Request_For_Training_Master_Emp WHERE Request_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsRequestForTrainingMasterEmp()
                strCode = clsCommon.myCstr(dr("Request_Code"))
                obj.Emp_Code = clsCommon.myCstr(dr("Emp_Code"))
                ObjEmp.Add(obj)
            Next
        End If
        Return ObjEmp
    End Function
End Class
