Imports common
Imports System.Data.SqlClient

Public Class clsProjectMaster
#Region "Variables"
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing

    Public PROJECT_CODE As String = Nothing
    Public SPECIFICATION As String = Nothing
    Public PROJECT_STATUS As String = Nothing

    Public Created_By As String = Nothing
    Public Create_Name As String = Nothing ''Not a Table field
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Comp_Code As String = Nothing

    Public Project_Manager As String = Nothing
    Public Project_Manager_Name As String = Nothing

    Public Cust_Code As String = Nothing
    Public Customer_Name As String = Nothing
    Public Sale_Order_No As String = Nothing
    Public Project_Type As String = Nothing
    Public Project_Type_Value As Double
    Public Account_Method As String = Nothing

    Public Approve_By As String = Nothing
    Public Approve_Name As String = Nothing ''Not a Table field
    Public Approve_Date? As DateTime = Nothing
    Public Approve_Date_Actual? As DateTime = Nothing

    Public Open_By As String ''Released is same
    Public Open_Name As String = Nothing ''Not a Table field
    Public Open_Date? As DateTime = Nothing
    Public Open_Date_Actual? As DateTime = Nothing


    Public On_Hold_By As String = Nothing
    Public On_Hold_Name As String = Nothing ''Not a Table field
    Public On_Hold_Date? As DateTime = Nothing

    Public Close_By As String = Nothing
    Public Close_Name As String = Nothing ''Not a Table field
    Public Close_Date? As DateTime = Nothing

    Public Completed_By As String
    Public Completed_Name As String = Nothing ''Not a Table field
    Public Completed_Date? As DateTime = Nothing
    Public Completed_Date_Actual? As DateTime = Nothing

    Public Inactive_By As String = Nothing
    Public Inactive_Name As String = Nothing ''Not a Table field
    Public Inactive_Date? As DateTime = Nothing

    Public Comment As String

    Public Total_Cost As Double = 0
    Public Total_Billing As Double = 0
    Public Total_Profit As Double = 0

    Public Actual_Cost As Double = 0
    Public Actual_Billing As Double = 0
    Public Actual_Profit As Double = 0
    Public arrJob As List(Of clsProjectJobMaster)
#End Region

    Public Function SaveData(ByVal obj As clsProjectMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_PJC_PROJECT_TASK where PROJECT_ID='" + obj.PROJECT_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_PJC_PROJECT_JOB where PROJECT_CODE='" + obj.PROJECT_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isNewEntry Then
                Dim strCode As String = clsDBFuncationality.getSingleValue("select isnull(max(PROJECT_CODE),'') from TSPL_PJC_PROJECT", trans)
                If clsCommon.myLen(strCode) <= 0 Then
                    obj.PROJECT_CODE = "PJEX000000001"
                Else
                    obj.PROJECT_CODE = clsCommon.incval(strCode)
                End If

            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SPECIFICATION", obj.SPECIFICATION)
            clsCommon.AddColumnsForChange(coll, "PROJECT_STATUS", obj.PROJECT_STATUS)
            clsCommon.AddColumnsForChange(coll, "Project_Manager", obj.Project_Manager, True)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
            clsCommon.AddColumnsForChange(coll, "Sale_Order_No", obj.Sale_Order_No, True)
            clsCommon.AddColumnsForChange(coll, "Project_Type", obj.Project_Type)
            clsCommon.AddColumnsForChange(coll, "Project_Type_Value", obj.Project_Type_Value)
            clsCommon.AddColumnsForChange(coll, "Account_Method", obj.Account_Method)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Total_Cost", obj.Total_Cost)
            clsCommon.AddColumnsForChange(coll, "Total_Billing", obj.Total_Billing)
            clsCommon.AddColumnsForChange(coll, "Total_Profit", obj.Total_Profit)
            If obj.Approve_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Approve_Date", clsCommon.GetPrintDate(obj.Approve_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
            If obj.Completed_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Completed_Date", clsCommon.GetPrintDate(obj.Completed_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
            If obj.Open_Date.HasValue Then
                clsCommon.AddColumnsForChange(coll, "Open_Date", clsCommon.GetPrintDate(obj.Open_Date, "dd/MMM/yyyy hh:mm tt"))
            End If
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "PROJECT_CODE", obj.PROJECT_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_PROJECT", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_PROJECT", OMInsertOrUpdate.Update, "TSPL_PJC_PROJECT.PROJECT_CODE='" + obj.PROJECT_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsProjectJobMaster.SaveData(obj.PROJECT_CODE, obj.Project_Type, obj.Account_Method, obj.arrJob, trans)
            isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(obj.Form_ID, obj.PROJECT_CODE, obj.arrCustomFields, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsProjectMaster
        Dim obj As clsProjectMaster = Nothing
        Dim qry As String = "select TSPL_PJC_PROJECT.*,createBy.User_Name as Create_Name,ApproveBy.User_Name as Approve_Name,CompleteBy.User_Name as Completed_Name,inActive.User_Name as Inactive_Name,OpenBy.User_Name as Open_Name,OnHold.User_Name as On_Hold_Name,Closet.User_Name as Close_Name,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_CUSTOMER_MASTER.Customer_Name,((select isnull(SUM(TSPL_VENDOR_INVOICE_HEAD.Document_Total),0) from TSPL_VENDOR_INVOICE_HEAD where TSPL_VENDOR_INVOICE_HEAD.PROJECT_ID=TSPL_PJC_PROJECT.PROJECT_CODE)+(select isnull(SUM( TSPL_PJC_TIMESHEET.TOTAL_COST),0) from TSPL_PJC_TIMESHEET where  TSPL_PJC_TIMESHEET.PROJECT_CODE=TSPL_PJC_PROJECT.PROJECT_CODE)+(select isnull(SUM(TSPL_PJC_EXPENSE_HEADER.TotalCost),0)  from TSPL_PJC_EXPENSE_HEADER where  TSPL_PJC_EXPENSE_HEADER.PROJECT_CODE=TSPL_PJC_PROJECT.PROJECT_CODE)) as Actual_Cost,(select SUM(TSPL_Customer_Invoice_Head.Document_Total) from TSPL_Customer_Invoice_Head where TSPL_Customer_Invoice_Head.PROJECT_ID=TSPL_PJC_PROJECT.PROJECT_CODE) as Actual_Billing  from TSPL_PJC_PROJECT" & _
        " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_PJC_PROJECT.Project_Manager" & _
        " left outer join TSPL_USER_MASTER as createBy on createBy.User_Code=TSPL_PJC_PROJECT.Created_By" & _
        " left outer join TSPL_USER_MASTER as ApproveBy on ApproveBy.User_Code=TSPL_PJC_PROJECT.Approve_By" & _
        " left outer join TSPL_USER_MASTER as OpenBy on OpenBy.User_Code=TSPL_PJC_PROJECT.Open_By" & _
        " left outer join TSPL_USER_MASTER as CompleteBy on CompleteBy.User_Code=TSPL_PJC_PROJECT.Completed_By" & _
        " left outer join TSPL_USER_MASTER as OnHold on OnHold.User_Code=TSPL_PJC_PROJECT.On_Hold_By" & _
         " left outer join TSPL_USER_MASTER as Closet on Closet.User_Code=TSPL_PJC_PROJECT.Close_By" & _
         " left outer join TSPL_CUSTOMER_MASTER  on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_PJC_PROJECT.Cust_Code" & _
        " left outer join TSPL_USER_MASTER as inActive on inActive.User_Code=TSPL_PJC_PROJECT.Inactive_By where 2=2"
        Dim whrClas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_PJC_PROJECT.PROJECT_CODE = (select MIN(PROJECT_CODE) from TSPL_PJC_PROJECT where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_PJC_PROJECT.PROJECT_CODE = (select Max(PROJECT_CODE) from TSPL_PJC_PROJECT where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_PJC_PROJECT.PROJECT_CODE = (select Min(PROJECT_CODE) from TSPL_PJC_PROJECT where PROJECT_CODE>'" + strCode + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_PJC_PROJECT.PROJECT_CODE = (select Max(PROJECT_CODE) from TSPL_PJC_PROJECT where PROJECT_CODE<'" + strCode + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_PJC_PROJECT.PROJECT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsProjectMaster()
            obj.PROJECT_CODE = clsCommon.myCstr(dt.Rows(0)("PROJECT_CODE"))
            obj.SPECIFICATION = clsCommon.myCstr(dt.Rows(0)("SPECIFICATION"))
            obj.PROJECT_STATUS = clsCommon.myCstr(dt.Rows(0)("PROJECT_STATUS"))
            obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
            obj.Create_Name = clsCommon.myCstr(dt.Rows(0)("Create_Name"))
            obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            obj.Modified_By = clsCommon.myCstr(dt.Rows(0)("Modified_By"))
            obj.Modified_Date = clsCommon.myCDate(dt.Rows(0)("Modified_Date"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.Project_Manager = clsCommon.myCstr(dt.Rows(0)("Project_Manager"))
            obj.Project_Manager_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.Cust_Code = clsCommon.myCstr(dt.Rows(0)("Cust_Code"))
            obj.Customer_Name = clsCommon.myCstr(dt.Rows(0)("Customer_Name"))
            obj.Sale_Order_No = clsCommon.myCstr(dt.Rows(0)("Sale_Order_No"))
            obj.Project_Type = clsCommon.myCstr(dt.Rows(0)("Project_Type"))
            obj.Project_Type_Value = clsCommon.myCstr(dt.Rows(0)("Project_Type_Value"))
            obj.Account_Method = clsCommon.myCstr(dt.Rows(0)("Account_Method"))

            obj.Approve_By = clsCommon.myCstr(dt.Rows(0)("Approve_By"))
            obj.Approve_Name = clsCommon.myCstr(dt.Rows(0)("Approve_Name"))
            If dt.Rows(0)("Approve_Date") IsNot DBNull.Value Then
                obj.Approve_Date = clsCommon.myCDate(dt.Rows(0)("Approve_Date"))
            End If
            If dt.Rows(0)("Approve_Date_Actual") IsNot DBNull.Value Then
                obj.Approve_Date_Actual = clsCommon.myCDate(dt.Rows(0)("Approve_Date_Actual"))
            End If

            obj.Open_By = clsCommon.myCstr(dt.Rows(0)("Open_By"))
            obj.Open_Name = clsCommon.myCstr(dt.Rows(0)("Open_Name"))
            If dt.Rows(0)("Open_Date") IsNot DBNull.Value Then
                obj.Open_Date = clsCommon.myCDate(dt.Rows(0)("Open_Date"))
            End If
            If dt.Rows(0)("Open_Date_Actual") IsNot DBNull.Value Then
                obj.Open_Date_Actual = clsCommon.myCDate(dt.Rows(0)("Open_Date_Actual"))
            End If

            obj.On_Hold_By = clsCommon.myCstr(dt.Rows(0)("On_Hold_By"))
            obj.On_Hold_Name = clsCommon.myCstr(dt.Rows(0)("On_Hold_Name"))
            If dt.Rows(0)("On_Hold_Date") IsNot DBNull.Value Then
                obj.On_Hold_Date = clsCommon.myCDate(dt.Rows(0)("On_Hold_Date"))
            End If

            obj.Close_By = clsCommon.myCstr(dt.Rows(0)("Close_Date"))
            obj.Close_Name = clsCommon.myCstr(dt.Rows(0)("Close_Name"))
            If dt.Rows(0)("Close_Date") IsNot DBNull.Value Then
                obj.Close_Date = clsCommon.myCDate(dt.Rows(0)("Close_Date"))
            End If


            obj.Completed_By = clsCommon.myCstr(dt.Rows(0)("Completed_By"))
            obj.Completed_Name = clsCommon.myCstr(dt.Rows(0)("Completed_Name"))
            If dt.Rows(0)("Completed_Date") IsNot DBNull.Value Then
                obj.Completed_Date = clsCommon.myCDate(dt.Rows(0)("Completed_Date"))
            End If
            If dt.Rows(0)("Completed_Date_Actual") IsNot DBNull.Value Then
                obj.Completed_Date_Actual = clsCommon.myCDate(dt.Rows(0)("Completed_Date_Actual"))
            End If

            obj.Inactive_By = clsCommon.myCstr(dt.Rows(0)("Inactive_By"))
            obj.Inactive_Name = clsCommon.myCstr(dt.Rows(0)("Inactive_Name"))
            If dt.Rows(0)("Inactive_Date") IsNot DBNull.Value Then
                obj.Inactive_Date = clsCommon.myCDate(dt.Rows(0)("Inactive_Date"))
            End If

            obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))

            obj.Total_Cost = clsCommon.myCdbl(dt.Rows(0)("Total_Cost"))
            obj.Total_Billing = clsCommon.myCdbl(dt.Rows(0)("Total_Billing"))
            obj.Total_Profit = clsCommon.myCdbl(dt.Rows(0)("Total_Profit"))

            obj.Actual_Cost = clsCommon.myCdbl(dt.Rows(0)("Actual_Cost"))
            obj.Actual_Billing = clsCommon.myCdbl(dt.Rows(0)("Actual_Billing"))
            obj.Actual_Profit = obj.Actual_Billing - obj.Actual_Cost

            obj.arrJob = clsProjectJobMaster.GetData(obj.PROJECT_CODE)

        End If

        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean = True
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New clsProjectMaster()
        obj = clsProjectMaster.GetData(strCode, NavigatorType.Current)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.PROJECT_CODE) > 0) Then
            Try
                
                Dim qry As String = "delete from TSPL_PJC_PROJECT_TASK where PROJECT_ID='" + obj.PROJECT_CODE + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PJC_PROJECT_JOB where PROJECT_CODE='" + obj.PROJECT_CODE + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_PJC_PROJECT where PROJECT_CODE='" + obj.PROJECT_CODE + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function

End Class

Public Class clsProjectJobMaster
#Region "Variables"
    Public Job_ID As String
    Public SNo As Double = 0
    Public PROJECT_ID As String = Nothing
    Public Job_Code As String = Nothing
    Public Job_Desc As String = Nothing
    Public Job_Type As String = Nothing
    Public Accounting_Method As String = Nothing
    Public Billing_Type As String = Nothing
    Public Close_To_Bill As Boolean = False
    Public Close_To_Cost As Boolean = False

    Public Start_Date As DateTime? = Nothing
    Public End_Date As DateTime? = Nothing
    Public Status As String = Nothing
    Public Status_Date As DateTime? = Nothing

    Public Status_Date_Open As DateTime? = Nothing
    Public Status_Date_WIP As DateTime? = Nothing
    Public Status_Date_Complete As DateTime? = Nothing
    Public Status_Date_Hold As DateTime? = Nothing


    Public arrTask As List(Of clsProjectTaskMaster)
#End Region

    Public Shared Function SaveData(ByVal strProjectID As String, ByVal strJobType As String, ByVal strAccountMethod As String, ByVal Arr As List(Of clsProjectJobMaster), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsProjectJobMaster In Arr
                Dim coll As New Hashtable()
                Dim strCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MAX(Job_ID) from TSPL_PJC_PROJECT_JOB", trans))
                If clsCommon.myLen(strCode) <= 0 Then
                    strCode = "JOB0000001"
                Else
                    strCode = clsCommon.incval(strCode)
                End If
                clsCommon.AddColumnsForChange(coll, "Job_ID", strCode)
                clsCommon.AddColumnsForChange(coll, "S_No", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "PROJECT_CODE", strProjectID)
                clsCommon.AddColumnsForChange(coll, "Job_Code", obj.Job_Code)
                clsCommon.AddColumnsForChange(coll, "Job_Type", strJobType)
                clsCommon.AddColumnsForChange(coll, "Accounting_Method", strAccountMethod)
                clsCommon.AddColumnsForChange(coll, "Billing_Type", obj.Billing_Type)
                clsCommon.AddColumnsForChange(coll, "Close_To_Bill", IIf(obj.Close_To_Bill, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Close_To_Cost", IIf(obj.Close_To_Cost, 1, 0))


                If obj.Start_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                End If
                If obj.End_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                If obj.Status_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Status_Date", Nothing, True)
                End If
                If clsCommon.CompairString(obj.Status, "Open") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_Open", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                ElseIf clsCommon.CompairString(obj.Status, "WIP") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_WIP", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                ElseIf clsCommon.CompairString(obj.Status, "Complete") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_Complete", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                ElseIf clsCommon.CompairString(obj.Status, "Hold") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_Hold", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                End If
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_PROJECT_JOB", OMInsertOrUpdate.Insert, "", trans)
                clsProjectTaskMaster.SaveData(strCode, obj.Billing_Type, strProjectID, obj.arrTask, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strProject As String) As List(Of clsProjectJobMaster)
        Dim arr = New List(Of clsProjectJobMaster)
        Dim qry As String = "select TSPL_PJC_PROJECT_JOB.* ,TSPL_PJC_JOB.DESCRIPTION as Job_Name" & _
            " from TSPL_PJC_PROJECT_JOB " & _
            " left outer join TSPL_PJC_JOB on TSPL_PJC_JOB.JOB_CODE=TSPL_PJC_PROJECT_JOB.JOB_CODE" & _
            " where PROJECT_CODE='" + strProject + "' order by S_No"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsProjectJobMaster
            For Each dr As DataRow In dt.Rows
                objTr = New clsProjectJobMaster
                objTr.Job_ID = clsCommon.myCstr(dr("Job_ID"))
                objTr.Job_Desc = clsCommon.myCstr(dr("Job_Name"))
                objTr.SNo = clsCommon.myCdbl(dr("S_No"))
                objTr.PROJECT_ID = clsCommon.myCstr(clsCommon.myCdbl(dr("PROJECT_CODE")))
                objTr.Job_Code = clsCommon.myCstr(dr("Job_Code"))
                objTr.Job_Type = clsCommon.myCstr(dr("Job_Type"))
                objTr.Billing_Type = clsCommon.myCstr(dr("Billing_Type"))
                objTr.Accounting_Method = clsCommon.myCstr(dr("Accounting_Method"))

                objTr.Close_To_Bill = IIf(clsCommon.myCstr(dr("Close_To_Bill")) = 1, True, False)
                objTr.Close_To_Cost = IIf(clsCommon.myCdbl(dr("Close_To_Cost")) = 1, True, False)

                If dr("Start_Date") IsNot DBNull.Value Then
                    objTr.Start_Date = clsCommon.myCDate(dr("Start_Date"))
                End If
                If dr("End_Date") IsNot DBNull.Value Then
                    objTr.End_Date = clsCommon.myCDate(dr("End_Date"))
                End If
                objTr.Status = clsCommon.myCstr(dr("Status"))
                If dr("Status_Date") IsNot DBNull.Value Then
                    objTr.Status_Date = clsCommon.myCDate(dr("Status_Date"))
                End If
                If dr("Status_Date_Open") IsNot DBNull.Value Then
                    objTr.Status_Date_Open = clsCommon.myCDate(dr("Status_Date_Open"))
                End If
                If dr("Status_Date_WIP") IsNot DBNull.Value Then
                    objTr.Status_Date_WIP = clsCommon.myCDate(dr("Status_Date_WIP"))
                End If
                If dr("Status_Date_Complete") IsNot DBNull.Value Then
                    objTr.Status_Date_Complete = clsCommon.myCDate(dr("Status_Date_Complete"))
                End If
                If dr("Status_Date_Hold") IsNot DBNull.Value Then
                    objTr.Status_Date_Hold = clsCommon.myCDate(dr("Status_Date_Hold"))
                End If

                objTr.arrTask = clsProjectTaskMaster.GetData(objTr.Job_ID)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsProjectTaskMaster
#Region "Variables"
    Public Task_ID As String = Nothing
    Public SNo As Double = 0
    Public Job_ID As String = Nothing
    Public PROJECT_ID As String = Nothing
    Public Task_Code As String = Nothing
    Public Task_Description As String = Nothing ''Not A Table Column
    Public Billing_Type As String = Nothing
    Public Qty As Double = 0
    Public Cost As Double = 0
    Public Amount As Double = 0
    Public Cost_Plus As Double = 0
    Public Billing_Amt As Double = 0
    Public Is_Task_Type_External As Boolean = False
    Public Vendor_Code As String = Nothing
    Public Vendor_Desc As String = Nothing

    Public Start_Date As DateTime? = Nothing
    Public End_Date As DateTime? = Nothing
    Public Status As String = Nothing
    Public Status_Date As DateTime? = Nothing

    Public Status_Date_Open As DateTime? = Nothing
    Public Status_Date_WIP As DateTime? = Nothing
    Public Status_Date_Complete As DateTime? = Nothing
    Public Status_Date_Hold As DateTime? = Nothing
#End Region

    Public Shared Function SaveData(ByVal strJOBID As String, ByVal strBillingType As String, ByVal strProjectID As String, ByVal Arr As List(Of clsProjectTaskMaster), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsProjectTaskMaster In Arr
                Dim coll As New Hashtable()
                Dim strCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MAX(Task_ID) from TSPL_PJC_PROJECT_TASK", trans))
                If clsCommon.myLen(strCode) <= 0 Then
                    strCode = "TASK0000001"
                Else
                    strCode = clsCommon.incval(strCode)
                End If
                clsCommon.AddColumnsForChange(coll, "Task_ID", strCode)
                clsCommon.AddColumnsForChange(coll, "SNo", obj.SNo)
                clsCommon.AddColumnsForChange(coll, "Job_ID", strJOBID)
                clsCommon.AddColumnsForChange(coll, "PROJECT_ID", strProjectID)
                clsCommon.AddColumnsForChange(coll, "Task_Code", obj.Task_Code)
                clsCommon.AddColumnsForChange(coll, "Billing_Type", strBillingType)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Cost", obj.Cost)
                clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
                clsCommon.AddColumnsForChange(coll, "Cost_Plus", obj.Cost_Plus)
                clsCommon.AddColumnsForChange(coll, "Billing_Amt", obj.Billing_Amt)
                clsCommon.AddColumnsForChange(coll, "Is_Task_Type_External", IIf(obj.Is_Task_Type_External, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)

                If obj.Start_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Start_Date", clsCommon.GetPrintDate(obj.Start_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Start_Date", Nothing, True)
                End If
                If obj.End_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "End_Date", clsCommon.GetPrintDate(obj.End_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "End_Date", Nothing, True)
                End If
                clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
                If obj.Status_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Status_Date", Nothing, True)
                End If
                If clsCommon.CompairString(obj.Status, "Open") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_Open", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                ElseIf clsCommon.CompairString(obj.Status, "WIP") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_WIP", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                ElseIf clsCommon.CompairString(obj.Status, "Complete") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_Complete", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                ElseIf clsCommon.CompairString(obj.Status, "Hold") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Status_Date_Hold", clsCommon.GetPrintDate(obj.Status_Date, "dd/MMM/yyyy hh:mm tt"))
                End If

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PJC_PROJECT_TASK", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strJobID As String) As List(Of clsProjectTaskMaster)
        Dim arr = New List(Of clsProjectTaskMaster)
        Dim qry As String = "select TSPL_PJC_PROJECT_TASK.* ,TSPL_PJC_TASK.DESCRIPTION as Task_Name,TSPL_VENDOR_MASTER.Vendor_Name " & _
            " from TSPL_PJC_PROJECT_TASK " & _
            " left outer join TSPL_PJC_TASK on TSPL_PJC_TASK.TASK_CODE=TSPL_PJC_PROJECT_TASK.TASK_CODE" & _
            " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PJC_PROJECT_TASK.Vendor_Code" & _
            " where Job_ID='" + strJobID + "' order by SNo"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            Dim objTr As clsProjectTaskMaster
            For Each dr As DataRow In dt.Rows
                objTr = New clsProjectTaskMaster
                objTr.Task_ID = clsCommon.myCstr(dr("Task_ID"))
                objTr.SNo = clsCommon.myCdbl(dr("SNo"))
                objTr.Job_ID = clsCommon.myCstr(dr("Job_ID"))
                objTr.PROJECT_ID = clsCommon.myCstr(clsCommon.myCdbl(dr("PROJECT_ID")))
                objTr.Task_Code = clsCommon.myCstr(dr("Task_Code"))
                objTr.Task_Description = clsCommon.myCstr(dr("Task_Name"))
                objTr.Billing_Type = clsCommon.myCstr(dr("Billing_Type"))
                objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                objTr.Cost = clsCommon.myCdbl(dr("Cost"))
                objTr.Amount = clsCommon.myCdbl(dr("Amount"))
                objTr.Cost_Plus = clsCommon.myCdbl(dr("Cost_Plus"))
                objTr.Billing_Amt = clsCommon.myCdbl(dr("Billing_Amt"))
                objTr.Is_Task_Type_External = IIf(clsCommon.myCdbl(dr("Is_Task_Type_External")) = 1, True, False)
                objTr.Vendor_Code = clsCommon.myCstr(dr("Vendor_Code"))
                objTr.Vendor_Desc = clsCommon.myCstr(dr("Vendor_Name"))

                If dr("Start_Date") IsNot DBNull.Value Then
                    objTr.Start_Date = clsCommon.myCDate(dr("Start_Date"))
                End If
                If dr("End_Date") IsNot DBNull.Value Then
                    objTr.End_Date = clsCommon.myCDate(dr("End_Date"))
                End If
                objTr.Status = clsCommon.myCstr(dr("Status"))
                If dr("Status_Date") IsNot DBNull.Value Then
                    objTr.Status_Date = clsCommon.myCDate(dr("Status_Date"))
                End If
                If dr("Status_Date_Open") IsNot DBNull.Value Then
                    objTr.Status_Date_Open = clsCommon.myCDate(dr("Status_Date_Open"))
                End If
                If dr("Status_Date_WIP") IsNot DBNull.Value Then
                    objTr.Status_Date_WIP = clsCommon.myCDate(dr("Status_Date_WIP"))
                End If
                If dr("Status_Date_Complete") IsNot DBNull.Value Then
                    objTr.Status_Date_Complete = clsCommon.myCDate(dr("Status_Date_Complete"))
                End If
                If dr("Status_Date_Hold") IsNot DBNull.Value Then
                    objTr.Status_Date_Hold = clsCommon.myCDate(dr("Status_Date_Hold"))
                End If
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
