Imports common
Imports System.Data
Imports System.Data.SqlClient
'' changes by shivani[BM00000008358]
Public Class ClsEmployeeTransfer
    Public Document_Code As String
    Public Emp_Code As String
    Public Transfer_Location As String
    Public Transfer_Designation As String
    Public Transfer_Department As String
    Public Transfer_Division As String
    Public Document_Date As Date
    Public Description As String
    Public Document_Type As String
    Public Posting_Date As Date
    Public Salary_Affected As String
    Public Salary_Code As String
    Public Effective_Date As Date
    Public Current_Location As String
    Public Current_Designation As String
    Public Current_Department As String
    Public Current_Division As String
    Public Previous_Salary_Code As String
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Shared Function SaveData(ByVal obj As ClsEmployeeTransfer, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = String.Empty
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Payroll", "Employee Transfer", obj.Transfer_Location, obj.Document_Date, trans)

            Dim coll As New Hashtable()
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.EmployeeTransfer, "", "")
            End If

            clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Effective_Date", clsCommon.GetPrintDate(obj.Effective_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)

            clsCommon.AddColumnsForChange(coll, "Salary_Affected", obj.Salary_Affected, True)
            clsCommon.AddColumnsForChange(coll, "Salary_Code", obj.Salary_Code, False)
            clsCommon.AddColumnsForChange(coll, "Previous_Salary_Code", obj.Previous_Salary_Code)
            clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
            clsCommon.AddColumnsForChange(coll, "Transfer_Location", obj.Transfer_Location, True)
            clsCommon.AddColumnsForChange(coll, "Transfer_Designation", obj.Transfer_Designation, True)
            clsCommon.AddColumnsForChange(coll, "Transfer_Department", obj.Transfer_Department, True)
            clsCommon.AddColumnsForChange(coll, "Transfer_Division", obj.Transfer_Division, True)
            clsCommon.AddColumnsForChange(coll, "Current_Location", obj.Current_Location)
            clsCommon.AddColumnsForChange(coll, "Current_Designation", obj.Current_Designation)
            clsCommon.AddColumnsForChange(coll, "Current_Department", obj.Current_Department)
            clsCommon.AddColumnsForChange(coll, "Current_Division", obj.Current_Division)
            'clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Posted", "0")
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_TRANSFER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_TRANSFER", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_TRANSFER.Document_Code='" + obj.Document_Code + "'", trans)
            End If


            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function


    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal NavType As NavigatorType) As ClsEmployeeTransfer
        Dim obj As ClsEmployeeTransfer = Nothing
        Dim Arr As List(Of ClsEmployeeTransfer) = Nothing
        Dim qry As String = "select * from TSPL_EMPLOYEE_TRANSFER where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EMPLOYEE_TRANSFER.Document_Code = (select MIN(Document_Code) from TSPL_EMPLOYEE_TRANSFER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_EMPLOYEE_TRANSFER.Document_Code = (select Max(Document_Code) from TSPL_EMPLOYEE_TRANSFER WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_EMPLOYEE_TRANSFER.Document_Code = (select TOP 1 Document_Code from TSPL_EMPLOYEE_TRANSFER WHERE 1=1 " + whrclas + " and Document_Code='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_EMPLOYEE_TRANSFER.Document_Code = (select Min(Document_Code) from TSPL_EMPLOYEE_TRANSFER where Document_Code > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_EMPLOYEE_TRANSFER.Document_Code = (select Max(Document_Code) from TSPL_EMPLOYEE_TRANSFER where Document_Code < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsEmployeeTransfer()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Effective_Date = clsCommon.myCDate(dt.Rows(0)("Effective_Date"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.Salary_Affected = clsCommon.myCstr(dt.Rows(0)("Salary_Affected"))
            obj.Transfer_Department = clsCommon.myCstr(dt.Rows(0)("Transfer_Department"))
            obj.Transfer_Designation = clsCommon.myCstr(dt.Rows(0)("Transfer_Designation"))
            obj.Transfer_Location = clsCommon.myCstr(dt.Rows(0)("Transfer_Location"))
            obj.Transfer_Division = clsCommon.myCstr(dt.Rows(0)("Transfer_Division"))
            obj.Current_Department = clsCommon.myCstr(dt.Rows(0)("Current_Department"))
            obj.Current_Designation = clsCommon.myCstr(dt.Rows(0)("Current_Designation"))
            obj.Current_Location = clsCommon.myCstr(dt.Rows(0)("Current_Location"))
            obj.Current_Division = clsCommon.myCstr(dt.Rows(0)("Current_Division"))
            obj.Emp_Code = clsCommon.myCstr(dt.Rows(0)("Emp_Code"))
            obj.Salary_Code = clsCommon.myCstr(dt.Rows(0)("Salary_Code"))
            obj.Previous_Salary_Code = clsCommon.myCstr(dt.Rows(0)("Previous_Salary_Code"))
            obj.POSTED = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If

        Return obj
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsEmployeeTransfer = ClsEmployeeTransfer.GetData(strDocNo, Nothing, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            trans = clsDBFuncationality.GetTransactin
            Dim qry As String = "Update TSPL_EMPLOYEE_TRANSFER set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where Document_Code ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            SaveEmployeeStatus(obj, trans)
            trans.Commit()
        Catch ex As Exception
            If trans IsNot Nothing Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveEmployeeStatus(ByVal obj1 As ClsEmployeeTransfer, ByVal trans As SqlTransaction) As Boolean
        If obj1 IsNot Nothing Then

            Dim obj As clsEmployeeStatus = Nothing
            'Dim objStatus As clsEmployeeStatus = Nothing
            Dim Code As String = ""
            Code = clsEmployeeStatus.GetEmployeeLatestStatus(obj1.Emp_Code, trans)
            If clsCommon.myLen(Code) <= 0 Then
                Throw New Exception("Employee Status not created for the employee " & obj1.Emp_Code & "")
            Else
                obj = clsEmployeeStatus.GetData(Code, NavigatorType.Current, trans)
            End If
            
            If (obj IsNot Nothing) Then               
                If clsCommon.CompairString(obj1.Document_Type, "Transfer Letter(For Location)") = CompairStringResult.Equal Then
                    obj.LOCATION_CODE = obj1.Transfer_Location
                    obj.DEVISION_CODE = obj1.Transfer_Division
                    obj.DEPARTMENT_CODE = obj1.Transfer_Department
                    obj.DESIGNATION_ID = obj1.Transfer_Designation
                ElseIf clsCommon.CompairString(obj1.Document_Type, "Promotion Letter") = CompairStringResult.Equal Then
                    obj.DEPARTMENT_CODE = obj1.Transfer_Department
                    obj.DESIGNATION_ID = obj1.Transfer_Designation
                ElseIf clsCommon.CompairString(obj1.Document_Type, "Transfer Letter(For Department)") = CompairStringResult.Equal Then
                    obj.DEPARTMENT_CODE = obj1.Transfer_Department
                End If

                '' done by Panch Raj against ticket no:BM00000008618
                Dim IsNewEntry As Boolean = False
                Dim CheckSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveMultipleEmployeeStatus, clsFixedParameterCode.AllowToSaveMultipleEmployeeStatus, trans))
                If clsCommon.CompairString(CheckSetting, "1") = CompairStringResult.Equal Then
                    IsNewEntry = True
                    obj.Code = ""
                    obj.REVISION_NO = obj.REVISION_NO + 1
                Else
                    IsNewEntry = False
                    obj.Code = clsEmployeeStatus.GetEmployeeLatestStatus(obj1.Emp_Code, trans)
                End If
                If obj IsNot Nothing Then
                    If obj.SaveData(obj, IsNewEntry, obj.Code, trans) Then
                        If clsCommon.CompairString(obj1.Document_Type, "Transfer Letter(For Location)") = CompairStringResult.Equal Then
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.LOCATION_CODE, True)
                            clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE, True)
                            clsCommon.AddColumnsForChange(coll, "Designation", obj.DESIGNATION_ID, True)
                            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_MASTER", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_MASTER.Emp_Code='" + obj.EMP_CODE + "'", trans)
                        End If
                        If clsCommon.CompairString(obj1.Document_Type, "Promotion Letter") = CompairStringResult.Equal Then
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "Designation", obj.DESIGNATION_ID, True)
                            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_MASTER", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_MASTER.Emp_Code='" + obj.EMP_CODE + "'", trans)
                        End If
                        If clsCommon.CompairString(obj1.Document_Type, "Transfer Letter(For Department)") = CompairStringResult.Equal Then
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE, True)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_MASTER", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_MASTER.Emp_Code='" + obj.EMP_CODE + "'", trans)
                        End If
                        Return True
                    End If
                End If
            End If


        End If
        Return True
    End Function
End Class
