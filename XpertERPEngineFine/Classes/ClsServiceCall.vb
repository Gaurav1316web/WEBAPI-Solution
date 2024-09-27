Imports System.Data.SqlClient
Imports common

Public Class ClsServiceCall
#Region "Variables"
    Public Service_Call_Code As String = Nothing
    Public Service_Call_Date As String = Nothing
    Public Cust_Group_Code As String = Nothing
    Public Dealer_Code As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_Sr_No As String = Nothing
    Public Call_Status As String = Nothing
    Public Priority As String = Nothing
    Public Item_Part_No As String = Nothing
    Public Issued_Notice As String = Nothing
    Public Subject As String = Nothing

    Public Origin As String = String.Empty
    Public Problem_Type As String = String.Empty
    Public Call_Type As String = String.Empty
    Public Start_Date As String = String.Empty
    Public Closed_Date As String = String.Empty

    Public Activity_Type As String = String.Empty
    Public Assigned_To As String = String.Empty
    Public Assigned_By As String = String.Empty
    Public Recurrence As String = String.Empty
    Public Activity_Remarks As String = String.Empty
    Public SMS As String = String.Empty
    Public Email As String = String.Empty
    Public Document_Type As String = String.Empty
    Public Document_No As String = String.Empty

    Public Resolution_Remarks As String = String.Empty

    Public ObjList As List(Of ClsServiceCallSolution) = Nothing
    Dim objDetail As New ClsServiceCallSolution()

#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT Service_Call_Code AS [Code],CONVERT (VARCHAR,Service_Call_Date ,103) AS [Document Date],Cust_Group_Code AS [Customer Group Code],Dealer_Code AS [Dealer Code],Vehicle_Code AS [Vehicle Code],Vehicle_Sr_No As [Vehicle Sr No],CASE WHEN Call_Status='O' THEN 'Open' WHEN Call_Status='P' THEN 'Pending' WHEN Call_Status='C' THEN 'Closed' END AS [Call Status],CASE WHEN Priority='L' THEN 'Low' WHEN Priority='M' THEN 'Medium' WHEN Priority='H' THEN 'High' END As Priority,CASE WHEN Origin ='EM' THEN 'E-Mail' WHEN Origin='TN' THEN 'Telephone No.' WHEN Origin='W' THEN 'Web' END As [Origin],CASE WHEN Recurrence='D' THEN 'Daily' WHEN Recurrence='W' THEN 'Weekly' WHEN Recurrence='M' THEN 'Monthly' WHEN Recurrence='A' THEN 'Annually' END AS Recurrence,Subject,Assigned_To As Techician,Assigned_By AS [Assigned By],Created_By AS [Created By],CONVERT(VARCHAR,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_SW_SERVICE_CALL  "
        str = clsCommon.ShowSelectForm("SWSerCall", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceCall)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            If ClsServiceCall.SaveData(arr, trans, Nothing) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsServiceCall), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsServiceCall In arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Call_Date", clsCommon.GetPrintDate(obj.Service_Call_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Cust_Group_Code", obj.Cust_Group_Code, True)
                clsCommon.AddColumnsForChange(coll, "Dealer_Code", obj.Dealer_Code, True)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code, True)
                clsCommon.AddColumnsForChange(coll, "Vehicle_Sr_No", obj.Vehicle_Sr_No, True)
                clsCommon.AddColumnsForChange(coll, "Call_Status", obj.Call_Status)
                clsCommon.AddColumnsForChange(coll, "Priority", obj.Priority)
                clsCommon.AddColumnsForChange(coll, "Item_Part_No", obj.Item_Part_No, True)
                clsCommon.AddColumnsForChange(coll, "Issued_Notice", obj.Issued_Notice, True)
                clsCommon.AddColumnsForChange(coll, "Subject", obj.Subject, True)
                clsCommon.AddColumnsForChange(coll, "Origin", obj.Origin)
                clsCommon.AddColumnsForChange(coll, "Problem_Type", obj.Problem_Type, True)
                clsCommon.AddColumnsForChange(coll, "Call_Type", obj.Call_Type, True)
                clsCommon.AddColumnsForChange(coll, "Start_Date", obj.Start_Date)
                clsCommon.AddColumnsForChange(coll, "Closed_Date", obj.Closed_Date)
                clsCommon.AddColumnsForChange(coll, "Activity_Type", obj.Activity_Type, True)
                clsCommon.AddColumnsForChange(coll, "Assigned_To", obj.Assigned_To, True)
                clsCommon.AddColumnsForChange(coll, "Assigned_By", obj.Assigned_By, True)
                clsCommon.AddColumnsForChange(coll, "Recurrence", obj.Recurrence)
                clsCommon.AddColumnsForChange(coll, "Activity_Remarks", obj.Activity_Remarks)
                clsCommon.AddColumnsForChange(coll, "SMS", obj.SMS)
                clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
                clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Resolution_Remarks", obj.Resolution_Remarks)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_SW_SERVICE_CALL WHERE Service_Call_Code='" + obj.Service_Call_Code + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_SW_SERVICE_CALL WHERE Service_Call_Code= '" & obj.Service_Call_Code & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        obj.Service_Call_Code = clsERPFuncationality.GetNextCode(trans, obj.Service_Call_Date, clsDocType.SWServiceCall, "", "")
                        clsCommon.AddColumnsForChange(coll, "Service_Call_Code", obj.Service_Call_Code)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_CALL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Throw New Exception("This Code Is Already Exist")
                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_CALL", OMInsertOrUpdate.Update, "Service_Call_Code='" + obj.Service_Call_Code + "'", trans)
                End If

                ClsServiceCallSolution.SaveData(obj.Service_Call_Code, obj.ObjList, trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsServiceCall
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsServiceCall
        Dim obj As ClsServiceCall = Nothing

        Dim qry As String = "Select * From TSPL_SW_SERVICE_CALL where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SW_SERVICE_CALL.Service_Call_Code = (select MIN(Service_Call_Code) from TSPL_SW_SERVICE_CALL)"
            Case NavigatorType.Last
                qry += " and TSPL_SW_SERVICE_CALL.Service_Call_Code = (select Max(Service_Call_Code) from TSPL_SW_SERVICE_CALL)"
            Case NavigatorType.Next
                qry += " and TSPL_SW_SERVICE_CALL.Service_Call_Code = (select Min(Service_Call_Code) from TSPL_SW_SERVICE_CALL where  Service_Call_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_SW_SERVICE_CALL.Service_Call_Code = (select Max(Service_Call_Code) from TSPL_SW_SERVICE_CALL where Service_Call_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_SW_SERVICE_CALL.Service_Call_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsServiceCall()
            obj.Service_Call_Code = clsCommon.myCstr(dt.Rows(0)("Service_Call_Code"))
            obj.Service_Call_Date = clsCommon.myCDate(dt.Rows(0)("Service_Call_Date"))
            obj.Cust_Group_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Group_Code"))
            obj.Dealer_Code = clsCommon.myCstr(dt.Rows(0)("Dealer_Code"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_Sr_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_Sr_No"))
            obj.Item_Part_No = clsCommon.myCstr(dt.Rows(0)("Item_Part_No"))
            obj.Issued_Notice = clsCommon.myCstr(dt.Rows(0)("Issued_Notice"))
            obj.Call_Type = clsCommon.myCstr(dt.Rows(0)("Call_Type"))
            obj.Priority = clsCommon.myCstr(dt.Rows(0)("Priority"))
            obj.Call_Status = clsCommon.myCstr(dt.Rows(0)("Call_Status"))
            obj.Assigned_By = clsCommon.myCstr(dt.Rows(0)("Assigned_By"))
            obj.Subject = clsCommon.myCstr(dt.Rows(0)("Subject"))
            obj.Origin = clsCommon.myCstr(dt.Rows(0)("Origin"))
            obj.Call_Type = clsCommon.myCstr(dt.Rows(0)("Call_Type"))
            obj.Problem_Type = clsCommon.myCstr(dt.Rows(0)("Problem_Type"))
            obj.Start_Date = clsCommon.myCstr(dt.Rows(0)("Start_Date"))
            obj.Closed_Date = clsCommon.myCstr(dt.Rows(0)("Closed_Date"))
            obj.Assigned_To = clsCommon.myCstr(dt.Rows(0)("Assigned_To"))
            obj.Recurrence = clsCommon.myCstr(dt.Rows(0)("Recurrence"))
            obj.Activity_Type = clsCommon.myCstr(dt.Rows(0)("Activity_Type"))
            obj.Activity_Remarks = clsCommon.myCstr(dt.Rows(0)("Activity_Remarks"))
            obj.SMS = clsCommon.myCstr(dt.Rows(0)("SMS"))
            obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Resolution_Remarks = clsCommon.myCstr(dt.Rows(0)("Resolution_Remarks"))

            obj.ObjList = ClsServiceCallSolution.GetData(obj.Service_Call_Code, trans)

        End If
        Return obj
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

            qry = "Delete From TSPL_SW_SERVICE_CALL_SOLUTION Where Service_Call_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_SW_SERVICE_CALL Where Service_Call_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    ' ----------------- Get_Call_Status ------------------------
    Public Shared Function GetCallStatus() As DataTable
        Dim DT_CallStatus As DataTable = New DataTable
        DT_CallStatus.Columns.Add("Code", GetType(String))
        DT_CallStatus.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_CallStatus.NewRow()
        DR("Name") = "Open"
        DR("Code") = "O"
        DT_CallStatus.Rows.Add(DR)

        DR = DT_CallStatus.NewRow()
        DR("Name") = "Pending"
        DR("Code") = "P"
        DT_CallStatus.Rows.Add(DR)

        DR = DT_CallStatus.NewRow()
        DR("Name") = "Closed"
        DR("Code") = "C"
        DT_CallStatus.Rows.Add(DR)

        DT_CallStatus.AcceptChanges()

        Return DT_CallStatus
    End Function
    ' ----------------- Get_Priority ------------------------
    Public Shared Function GetPriority() As DataTable
        Dim DT_Priority As DataTable = New DataTable
        DT_Priority.Columns.Add("Code", GetType(String))
        DT_Priority.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Priority.NewRow()
        DR("Name") = "Low"
        DR("Code") = "L"
        DT_Priority.Rows.Add(DR)

        DR = DT_Priority.NewRow()
        DR("Name") = "Medium"
        DR("Code") = "M"
        DT_Priority.Rows.Add(DR)

        DR = DT_Priority.NewRow()
        DR("Name") = "High"
        DR("Code") = "H"
        DT_Priority.Rows.Add(DR)

        DT_Priority.AcceptChanges()

        Return DT_Priority
    End Function
    ' ----------------- Get_Origin ------------------------
    Public Shared Function GetOrigin() As DataTable
        Dim DT_Origin As DataTable = New DataTable
        DT_Origin.Columns.Add("Code", GetType(String))
        DT_Origin.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Origin.NewRow()
        DR("Name") = "E-Mail"
        DR("Code") = "EM"
        DT_Origin.Rows.Add(DR)

        DR = DT_Origin.NewRow()
        DR("Name") = "Telephone No."
        DR("Code") = "TN"
        DT_Origin.Rows.Add(DR)

        DR = DT_Origin.NewRow()
        DR("Name") = "Web"
        DR("Code") = "W"
        DT_Origin.Rows.Add(DR)

        DT_Origin.AcceptChanges()

        Return DT_Origin
    End Function
    ' ----------------- Get_Recurrence ------------------------
    Public Shared Function GetRecurrence() As DataTable
        Dim DT_Recurrence As DataTable = New DataTable
        DT_Recurrence.Columns.Add("Code", GetType(String))
        DT_Recurrence.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Recurrence.NewRow()
        DR("Name") = "Daily"
        DR("Code") = "D"
        DT_Recurrence.Rows.Add(DR)

        DR = DT_Recurrence.NewRow()
        DR("Name") = "Weekly"
        DR("Code") = "W"
        DT_Recurrence.Rows.Add(DR)

        DR = DT_Recurrence.NewRow()
        DR("Name") = "Monthly"
        DR("Code") = "M"
        DT_Recurrence.Rows.Add(DR)

        DR = DT_Recurrence.NewRow()
        DR("Name") = "Annually"
        DR("Code") = "A"
        DT_Recurrence.Rows.Add(DR)

        DT_Recurrence.AcceptChanges()

        Return DT_Recurrence
    End Function
End Class
' ============================================== Service Call Solution ====================================================
Public Class ClsServiceCallSolution
#Region "Variables"
    Public Service_Call_Code As String = String.Empty
    Public Sol_Knowledge_Code As String = String.Empty
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " DELETE FROM TSPL_SW_SERVICE_CALL_SOLUTION WHERE Service_Call_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjListChk As List(Of ClsServiceCallSolution), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_SW_SERVICE_CALL_SOLUTION WHERE Service_Call_Code = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsServiceCallSolution In ObjListChk
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Service_Call_Code", strCode)
                clsCommon.AddColumnsForChange(coll, "Sol_Knowledge_Code", obj.Sol_Knowledge_Code, True)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SW_SERVICE_CALL_SOLUTION", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsServiceCallSolution)
        Dim obj As ClsServiceCallSolution = Nothing
        Dim ObjListChk As New List(Of ClsServiceCallSolution)
        Dim qry As String = " select *  from TSPL_SW_SERVICE_CALL_SOLUTION WHERE Service_Call_Code = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsServiceCallSolution()
                obj.Service_Call_Code = clsCommon.myCstr(dr("Service_Call_Code"))
                obj.Sol_Knowledge_Code = clsCommon.myCstr(dr("Sol_Knowledge_Code"))
                ObjListChk.Add(obj)
            Next
        End If
        Return ObjListChk
    End Function
End Class