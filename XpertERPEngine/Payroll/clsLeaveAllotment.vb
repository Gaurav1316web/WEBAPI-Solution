Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLeaveAllotment


#Region "Variables"
    Public LVALLOTMENT_CODE As String
    Public Location_Code As String
    Public Location_Desc As String
    Public PAY_PERIOD_CODE As String
    Public PayPer_Name As String
    Public EMP_CODE As String
    Public Emp_Name As String
    Public ALLOTMENT_DATE As Date?
    Public ALLOTMENT_REMARKS As String
    Public ObjList As New List(Of clsLeaveAllotmentDetails)
    Dim objLeaveAllotmentDetails As New clsLeaveAllotmentDetails()
    Public Division_Code As String
    ''added by shivani
    Public Document_Type As String
    Public Allotment_Type As String

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLeaveAllotment
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            isSaved = clsLeaveAllotmentDetails.DeleteData(strCode)
            Dim qry As String
            qry = "delete from TSPL_LEAVE_ALLOTMENT where LVALLOTMENT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLeaveAllotment
        Dim obj As clsLeaveAllotment = Nothing
        Dim qry As String = " select TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE,TSPL_LEAVE_ALLOTMENT.ALLOTMENT_DATE, TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE, " & _
        " TSPL_LEAVE_ALLOTMENT.EMP_CODE, TSPL_EMPLOYEE_MASTER.Emp_Name, TSPL_PAYPERIOD_MASTER.PAY_PERIOD_NAME,TSPL_LEAVE_ALLOTMENT.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LEAVE_ALLOTMENT.Division, TSPL_LEAVE_ALLOTMENT.Document_type,TSPL_LEAVE_ALLOTMENT.Allotment_Type,TSPL_LEAVE_ALLOTMENT.ALLOTMENT_REMARKS from TSPL_LEAVE_ALLOTMENT " & _
        " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_LEAVE_ALLOTMENT.EMP_CODE " & _
        " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE = TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE " & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_LEAVE_ALLOTMENT.Location_Code where 2=2"

        Select Case NavType
            Case NavigatorType.First
                qry += " and LVALLOTMENT_CODE = (select MIN(LVALLOTMENT_CODE) from TSPL_LEAVE_ALLOTMENT)"
            Case NavigatorType.Last
                qry += " and LVALLOTMENT_CODE = (select Max(LVALLOTMENT_CODE) from TSPL_LEAVE_ALLOTMENT)"
            Case NavigatorType.Next
                qry += " and LVALLOTMENT_CODE = (select Min(LVALLOTMENT_CODE) from TSPL_LEAVE_ALLOTMENT where  LVALLOTMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and LVALLOTMENT_CODE = (select Max(LVALLOTMENT_CODE) from TSPL_LEAVE_ALLOTMENT where LVALLOTMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and LVALLOTMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsLeaveAllotment()
            obj.LVALLOTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("LVALLOTMENT_CODE"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))

            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.PayPer_Name = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_NAME"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))
            obj.Division_Code = clsCommon.myCstr(dt.Rows(0)("Division"))
            obj.Allotment_Type = clsCommon.myCstr(dt.Rows(0)("Allotment_Type"))
            obj.Document_Type = clsCommon.myCstr(dt.Rows(0)("Document_Type"))
            obj.ALLOTMENT_REMARKS = clsCommon.myCstr(dt.Rows(0)("ALLOTMENT_REMARKS"))
            If clsCommon.myLen(dt.Rows(0)("ALLOTMENT_DATE")) > 0 Then
                obj.ALLOTMENT_DATE = clsCommon.myCDate(dt.Rows(0)("ALLOTMENT_DATE"))
            End If
            obj.ObjList = clsLeaveAllotmentDetails.GetData(obj.LVALLOTMENT_CODE, trans)
        End If
        Return obj
    End Function

    Public Function SaveData(ByVal obj As clsLeaveAllotment, ByVal isNewEntry As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleHR, clsUserMgtCode.FrmAllotmentOfLeaves, obj.Location_Code, obj.ALLOTMENT_DATE, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE, True)
            clsCommon.AddColumnsForChange(coll, "ALLOTMENT_REMARKS", obj.ALLOTMENT_REMARKS)
            clsCommon.AddColumnsForChange(coll, "Division", obj.Division_Code, True)
            '' added by Shivani
            clsCommon.AddColumnsForChange(coll, "Document_Type", obj.Document_Type, True)
            clsCommon.AddColumnsForChange(coll, "Allotment_Type", obj.Allotment_Type, True)

            If Not obj.ALLOTMENT_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "ALLOTMENT_DATE", clsCommon.GetPrintDate(obj.ALLOTMENT_DATE, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If isNewEntry Then
                If clsCommon.myLen(obj.LVALLOTMENT_CODE) <= 0 Then
                    obj.LVALLOTMENT_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.ALLOTMENT_DATE, "dd/MMM/yyyy"), clsDocType.LeaveAllotment, "", obj.Location_Code)
                    If clsCommon.myLen(obj.LVALLOTMENT_CODE) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "LVALLOTMENT_CODE", obj.LVALLOTMENT_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim qry As String = "SELECT Count(*) FROM TSPL_LEAVE_ALLOTMENT where LVALLOTMENT_CODE= '" & obj.LVALLOTMENT_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_ALLOTMENT", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_ALLOTMENT", OMInsertOrUpdate.Update, "LVALLOTMENT_CODE='" + obj.LVALLOTMENT_CODE + "'", trans)
            End If
            isSaved = objLeaveAllotmentDetails.SaveData(obj.LVALLOTMENT_CODE, obj.ObjList, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function UpdateLeaveAllotmentAllEmployee(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, ByVal PAY_PERIOD_CODE As String, ByVal Location_Code As String, Optional ByVal Division_Code As String = "") As Integer
        Dim QryEmp As String = " SELECT T1.EMP_STATUS_CODE AS [Status Code],T1.EMP_CODE,emp.Emp_Name " & _
                               " FROM TSPL_EMPLOYEE_STATUS T1 JOIN (  " & _
                               " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  " & _
                               " from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<=CURRENT_TIMESTAMP  " & _
                               " GROUP BY EMP_CODE HAVING MAX(applicable_from)<=CURRENT_TIMESTAMP ) AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE " & _
                               " inner join TSPL_EMPLOYEE_MASTER emp on T2.EMP_CODE=emp.EMP_CODE  " & _
                               " WHERE T1.LOCATION_CODE='" & Location_Code & "'  AND T1.WORKING_STATUS='Working'"
        If clsCommon.myLen(EMP_CODE) > 0 Then
            QryEmp = QryEmp & " and T1.EMP_CODE='" & EMP_CODE & "'"
        End If
        If clsCommon.myLen(Division_Code) > 0 Then
            QryEmp = QryEmp & " and emp.DEVISION_CODE ='" & Division_Code & "'"
        End If
        Dim QryLeave As String = "SELECT LEAVE_CODE FROM TSPL_LEAVE_MASTER"
        Dim dtEmp As DataTable
        Dim dtLeave As DataTable
        Dim totalRecUpdated As Integer = 0

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try


            Dim PP_Start_Date As Date
            PP_Start_Date = clsPayPeriodMaster.GetFromDate(PAY_PERIOD_CODE, trans)
            If clsCommon.myLen(EMP_CODE) <= 0 Then
                dtEmp = clsDBFuncationality.GetDataTable(QryEmp, trans)
                For Each drEmp As DataRow In dtEmp.Rows
                    If clsCommon.myLen(LEAVE_CODE) <= 0 Then
                        dtLeave = clsDBFuncationality.GetDataTable(QryLeave, trans)
                        For Each drLeave As DataRow In dtLeave.Rows
                            totalRecUpdated = totalRecUpdated + SendAllotedLeave(drEmp.Item("EMP_CODE"), drLeave.Item("LEAVE_CODE"), PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans)
                        Next
                    Else
                        totalRecUpdated = totalRecUpdated + SendAllotedLeave(drEmp.Item("EMP_CODE"), LEAVE_CODE, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans)
                    End If
                Next

            Else
                If clsCommon.myLen(LEAVE_CODE) <= 0 Then
                    dtLeave = clsDBFuncationality.GetDataTable(QryLeave, trans)
                    For Each drLeave As DataRow In dtLeave.Rows
                        totalRecUpdated = totalRecUpdated + SendAllotedLeave(EMP_CODE, drLeave.Item("LEAVE_CODE"), PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans)
                    Next
                Else
                    totalRecUpdated = totalRecUpdated + SendAllotedLeave(EMP_CODE, LEAVE_CODE, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans)
                End If
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
            Return 0
        End Try
        Return totalRecUpdated
    End Function
    Public Shared Function SendAllotedLeave(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, ByVal PAY_PERIOD_CODE As String, ByVal PP_Start_Date As Date, ByVal Location_Code As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        'Dim CalculatedAllotedLeave As Decimal
        Dim MonthlyAlloted As Decimal
        Dim LastAllotedPayPeriod As String
        Dim LeaveOB As Decimal = 0
        Dim LastPP_Start_Date As Date
        Dim isNewEmployee As Boolean
        Dim ForwardedLeave As Decimal
        Dim LapsedLeave As Decimal
        Dim AllotedRemarks As String

        '' get monthly allted leave(Step-1)
        MonthlyAlloted = GetMonthlyAllotLeave(EMP_CODE, LEAVE_CODE, PAY_PERIOD_CODE, PP_Start_Date, trans)
        '' get last pay period in which this leave code is alloted to employee(step-2)
        LastAllotedPayPeriod = clsLeaveAllotment.GetLastPayPeriodCode(EMP_CODE, LEAVE_CODE, trans)
        If clsCommon.myLen(LastAllotedPayPeriod) > 0 Then '' step-2.1
            '' apply step-2.1.1
            LastPP_Start_Date = clsPayPeriodMaster.GetFromDate(LastAllotedPayPeriod, trans)
            If LastPP_Start_Date >= PP_Start_Date Then '' check for already alloted in this pay period
                '' only step-3 and step-4 will be processed
                '' save forwarded leave (step-3)
                If PP_Start_Date.Month = 1 Then
                    ForwardedLeave = GetCarryForwardedLeave(PAY_PERIOD_CODE, EMP_CODE, LEAVE_CODE, trans)
                    If ForwardedLeave > 0 Then
                        AllotedRemarks = "Forwarded Leave"
                        If AssignAndSave(EMP_CODE, LEAVE_CODE, ForwardedLeave, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                            Return 1
                        End If
                    End If

                End If

                '' save lapsed leave (step-4)
                LapsedLeave = GetLapsedLeave(PAY_PERIOD_CODE, PP_Start_Date, EMP_CODE, LEAVE_CODE, trans)
                If LapsedLeave <> 0 Then
                    AllotedRemarks = "Lapsed Leave"
                    If AssignAndSave(EMP_CODE, LEAVE_CODE, LapsedLeave, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                        Return 1
                    End If
                End If


            Else
                '' step-1, step-3 and step-4 will be processed

                '' save alloted leave for current pay period  (step-1)
                AllotedRemarks = "New Alloted Leave"
                If MonthlyAlloted > 0 Then
                    If AssignAndSave(EMP_CODE, LEAVE_CODE, MonthlyAlloted, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                        Return 1
                    End If
                End If


                '' save forwarded leave (step-3)
                If PP_Start_Date.Month = 1 Then
                    ForwardedLeave = GetCarryForwardedLeave(PAY_PERIOD_CODE, EMP_CODE, LEAVE_CODE, trans)
                    If ForwardedLeave > 0 Then
                        AllotedRemarks = "Forwarded Leave"
                        If AssignAndSave(EMP_CODE, LEAVE_CODE, ForwardedLeave, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                            Return 1
                        End If
                    End If

                End If

                '' save lapsed leave (step-4)
                LapsedLeave = GetLapsedLeave(PAY_PERIOD_CODE, PP_Start_Date, EMP_CODE, LEAVE_CODE, trans)
                If LapsedLeave <> 0 Then
                    AllotedRemarks = "Lapsed Leave"
                    If AssignAndSave(EMP_CODE, LEAVE_CODE, LapsedLeave, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                        Return 1
                    End If
                End If

            End If

        Else
            '' step 2.2
            LeaveOB = clsLeaveOpeningBalance.GetLeaveOpeningBalance(EMP_CODE, LEAVE_CODE, trans)
            If LeaveOB <> 0 Then '' step 2.3
                '' step-1, step-3 and step-4 will be processed(step-2.3.1)

                '' save alloted leave for current pay period  (step-1)
                AllotedRemarks = "New Alloted Leave"
                If MonthlyAlloted > 0 Then
                    If AssignAndSave(EMP_CODE, LEAVE_CODE, MonthlyAlloted, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                        Return 1
                    End If
                End If

                '' save forwarded leave (step-3)
                If PP_Start_Date.Month = 1 Then

                    ForwardedLeave = GetCarryForwardedLeave(PAY_PERIOD_CODE, EMP_CODE, LEAVE_CODE, trans)
                    If ForwardedLeave > 0 Then
                        AllotedRemarks = "Forwarded Leave"
                        If AssignAndSave(EMP_CODE, LEAVE_CODE, ForwardedLeave, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                            Return 1
                        End If
                    End If

                End If

                '' save lapsed leave (step-4)
                LapsedLeave = GetLapsedLeave(PAY_PERIOD_CODE, PP_Start_Date, EMP_CODE, LEAVE_CODE, trans)
                If LapsedLeave <> 0 Then
                    AllotedRemarks = "Lapsed Leave"
                    If AssignAndSave(EMP_CODE, LEAVE_CODE, LapsedLeave, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                        Return 1
                    End If
                End If


            Else  '' step-2.4
                '' check for new employee
                isNewEmployee = CheckForNewEmployee(EMP_CODE, LEAVE_CODE, PP_Start_Date, trans)
                If isNewEmployee Then
                    '' only step-1 will be processed

                    '' save alloted leave for current pay period  (step-1)
                    AllotedRemarks = "New Alloted Leave"
                    If MonthlyAlloted > 0 Then
                        If AssignAndSave(EMP_CODE, LEAVE_CODE, MonthlyAlloted, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                            Return 1
                        End If
                    End If

                Else
                    '' nothing to do
                    '' save alloted leave for current pay period  (step-1)
                    AllotedRemarks = "New Alloted Leave"
                    If MonthlyAlloted > 0 Then
                        If AssignAndSave(EMP_CODE, LEAVE_CODE, MonthlyAlloted, AllotedRemarks, PAY_PERIOD_CODE, PP_Start_Date, Location_Code, trans) Then
                            Return 1
                        End If
                    End If

                End If
            End If
        End If
        Return 0
    End Function
    Public Shared Function AssignAndSave(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, ByVal AllotedLeave As Decimal, ByVal AllotRemarks As String, ByVal PAY_PERIOD_CODE As String, ByVal PP_Start_Date As Date, Optional ByVal Location_Code As String = "", Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim obj As New clsLeaveAllotment
        'Dim objList As New List(Of clsLeaveAllotmentDetails)
        Dim objTr As New clsLeaveAllotmentDetails

        obj = New clsLeaveAllotment
        obj.EMP_CODE = EMP_CODE
        obj.Division_Code = clsDBFuncationality.getSingleValue("select DEVISION_CODE from TSPL_EMPLOYEE_MASTER where Emp_Code='" & obj.EMP_CODE & "'", trans)
        obj.PAY_PERIOD_CODE = PAY_PERIOD_CODE
        If clsCommon.myLen(Location_Code) > 0 Then
            obj.Location_Code = Location_Code
        Else
            obj.Location_Code = clsEmployeeMaster.GetLocation(EMP_CODE, trans)
        End If

        obj.ALLOTMENT_REMARKS = AllotRemarks
        obj.ALLOTMENT_DATE = PP_Start_Date

        'ObjList = New List(Of clsLeaveAllotmentDetails)
        objTr = New clsLeaveAllotmentDetails
        objTr.LEAVE_CODE = LEAVE_CODE
        objTr.ALLOTED_LEAVE = AllotedLeave
        obj.ObjList.Add(objTr)
        Return obj.SaveData(obj, True, trans)

    End Function
    Public Shared Function GetMonthlyAllotLeave(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, ByVal PAY_PERIOD_CODE As String, ByVal PP_Start_Date As Date, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        'Dim qry As String
        Dim LeaveAllotmentType As String
        Dim Location_Code As String
        Dim Basic As Decimal
        Dim YearlyAllotedLeave As Decimal
        Dim MonthlyAllotedLeave As Decimal
        Location_Code = clsEmployeeMaster.GetLocation(EMP_CODE, trans)

        '' get payroll setiing
        Dim objPayrollSett As clsPayrollSetting = clsPayrollSetting.GetPayrollSetting(Location_Code, trans)
        If Not objPayrollSett Is Nothing Then
            LeaveAllotmentType = objPayrollSett.LEAVE_ALLOTMENT
        Else
            LeaveAllotmentType = "Monthly"
        End If

        '' get basic
        Basic = clsEmployeeSalary.getBasicAmount(EMP_CODE, PP_Start_Date, trans)
        YearlyAllotedLeave = clsLeaveSetting.GetYearlyAllotedLeave(LEAVE_CODE, Basic, trans)
        MonthlyAllotedLeave = YearlyAllotedLeave / 12
        Return MonthlyAllotedLeave
    End Function
    Public Shared Function GetLastPayPeriodCode(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim qry As String
        Dim dt As DataTable
        Dim LastPay_Period_Code As String = ""
        qry = " select max(TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE) as PAY_PERIOD_CODE,MAX(DATE_FROM) as DATE_FROM " & _
              " from TSPL_LEAVE_ALLOTMENT inner join TSPL_LEAVE_ALLOTMENTDETAIL " & _
              " on TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE=TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE " & _
              " inner join TSPL_PAYPERIOD_MASTER on TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE " & _
              " where TSPL_LEAVE_ALLOTMENT.EMP_CODE='" & EMP_CODE & "' and TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE='" & LEAVE_CODE & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            If clsCommon.myLen(dt.Rows(0).Item("PAY_PERIOD_CODE")) > 0 Then
                LastPay_Period_Code = clsCommon.myCstr(dt.Rows(0).Item("PAY_PERIOD_CODE"))
            End If
        End If
        Return LastPay_Period_Code
    End Function
    Public Shared Function CheckForNewEmployee(ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, ByVal PP_Start_Date As Date, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim objLVSet As clsLeaveSetting
        Dim objEmp As clsEmployeeMaster
        'Dim LeaveAllotDate As Date
        Dim JoiningDate As Date
        Dim ConfirmDate As Date?
        Dim ProbPeriodCompDate As Date?
        Dim AllotAfterMonth As Decimal
        Dim AllotAfterDays As Decimal
        Dim LeaveAllotType As String

        Dim ActualDate As Date

        objLVSet = clsLeaveSetting.GetData(LEAVE_CODE, NavigatorType.Current, trans)
        objEmp = clsEmployeeMaster.GetData(EMP_CODE, NavigatorType.Current, trans)

        '' leave setting info
        LeaveAllotType = objLVSet.LEAVE_ALLOT_TYPE
        AllotAfterMonth = objLVSet.ALLOT_AFTER_MONTHS
        AllotAfterDays = objLVSet.AVAIL_AFTER_DAYS
        '' fetch emp details
        JoiningDate = objEmp.Joining_date
        ConfirmDate = objEmp.CONFIRMATION_DATE
        ProbPeriodCompDate = objEmp.PROBATION_END_DATE

        objLVSet = Nothing
        objEmp = Nothing

        If LeaveAllotType = 1 Then
            ActualDate = JoiningDate
        ElseIf LeaveAllotType = 2 Then
            ActualDate = ConfirmDate
        ElseIf LeaveAllotType = 3 Then
            ActualDate = ProbPeriodCompDate
        ElseIf LeaveAllotType = 4 Then
            ActualDate = ActualDate.AddMonths(AllotAfterMonth).AddDays(AllotAfterDays)
        End If
        If ActualDate >= PP_Start_Date Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function GetCarryForwardedLeave(ByVal PAY_PERIOD_CODE As String, ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim objLVSet As clsLeaveSetting
        Dim LeaveBalance As Decimal
        objLVSet = clsLeaveSetting.GetData(LEAVE_CODE, NavigatorType.Current, trans)
        LeaveBalance = GetLeaveBalance(PAY_PERIOD_CODE, EMP_CODE, LEAVE_CODE, trans)
        If objLVSet.CARRY_OVER = 1 Then
            If objLVSet.CARRY_UPPER_LIM <= LeaveBalance And LeaveBalance <= objLVSet.CARRY_UPPER_LIM Then
                Return LeaveBalance
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetLapsedLeave(ByVal PAY_PERIOD_CODE As String, ByVal PP_START_DATE As Date, ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim objLVSet As clsLeaveSetting
        Dim LeaveBalance As Decimal
        Dim lEAVE_TYPE As String

        objLVSet = clsLeaveSetting.GetData(LEAVE_CODE, NavigatorType.Current, trans)
        LeaveBalance = GetLeaveBalance(PAY_PERIOD_CODE, EMP_CODE, LEAVE_CODE, trans)
        lEAVE_TYPE = clsLeaveMaster.GetLeaveType(LEAVE_CODE, trans)

        If objLVSet.LAPSE_UNAVAILED = 1 Then
            If objLVSet.LAPSE_MONTH = MonthName(PP_START_DATE.Month) And LeaveBalance > 0 And clsCommon.CompairString(lEAVE_TYPE, "COFF") <> CompairStringResult.Equal Then
                Return LeaveBalance
            ElseIf objLVSet.LAPSE_NEGATIVE = 1 And LeaveBalance < 0 Then
                If objLVSet.LAPSE_EXCEEDING <= (-LeaveBalance) Then
                    Return -LeaveBalance
                Else
                    Return 0
                End If
            ElseIf objLVSet.LAPSE_AFTER_DAYS > 0 And clsCommon.CompairString(lEAVE_TYPE, "COFF") = CompairStringResult.Equal Then
                Return 0 '' user have to enter adjustment entry to lapse the unavailed coff
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function
    Public Shared Function GetLeaveBalance(ByVal PAY_PERIOD_CODE As String, ByVal EMP_CODE As String, ByVal LEAVE_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = ""
        qry = "select BALANCE from TSPL_FUN_LEAVE_STATUS('" & PAY_PERIOD_CODE & "') WHERE EMP_CODE='" & EMP_CODE & "' AND LEAVE_CODE='" & LEAVE_CODE & "'"
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    '====================shivani
    Public Shared Function GetOpeningBalance(ByVal strcode As String, ByVal EMP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        'Dim dt As DataTable
        Dim qry As String = ""
        qry = "select  count (TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE)  from TSPL_LEAVE_ALLOTMENTDETAIL left join TSPL_LEAVE_ALLOTMENT on TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE = TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE where TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE='" + EMP_CODE + "' and Document_Type ='O' and TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE not in  ('" + strcode + "') "
        Dim xdd As Decimal = 0
        xdd = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

        Return xdd
        'Return dt
    End Function
    
End Class

Public Class clsLeaveAllotmentDetails

#Region "Variables"
    Public LVALLOTMENT_CODE As String
    Public LEAVE_CODE As String
    Public LEAVE_NAME As String
    Public ALLOTED_LEAVE As Double
    Public Emp_Code As String
    Public Document_Type As String = Nothing 'use only for effect of pivot grid effect from 2nd screen
#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_LEAVE_ALLOTMENTDETAIL where LVALLOTMENT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsLeaveAllotmentDetails)
        Dim obj As clsLeaveAllotmentDetails = Nothing
        Dim ObjList As New List(Of clsLeaveAllotmentDetails)
        Dim qry As String = " select TSPL_LEAVE_MASTER.LEAVE_CODE, TSPL_LEAVE_MASTER.LEAVE_NAME, TSPL_LEAVE_ALLOTMENTDETAIL.ALLOTED_LEAVE,TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE  " & _
        " from TSPL_LEAVE_ALLOTMENTDETAIL  inner join TSPL_LEAVE_MASTER on TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE = TSPL_LEAVE_MASTER.LEAVE_CODE  left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE "
        qry += " where LVALLOTMENT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsLeaveAllotmentDetails()
                obj.Emp_Code = clsCommon.myCstr(dr("EMP_CODE"))

                obj.LEAVE_CODE = clsCommon.myCstr(dr("LEAVE_CODE"))
                obj.LEAVE_NAME = clsCommon.myCstr(dr("LEAVE_NAME"))
                obj.ALLOTED_LEAVE = clsCommon.myCdbl(dr("ALLOTED_LEAVE"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList


    End Function

    Public Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of clsLeaveAllotmentDetails), Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Try
            If Not ObjList Is Nothing AndAlso ObjList.Count > 0 Then
                For Each obj As clsLeaveAllotmentDetails In ObjList
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LEAVE_CODE", obj.LEAVE_CODE)
                    clsCommon.AddColumnsForChange(coll, "ALLOTED_LEAVE", obj.ALLOTED_LEAVE)
                    clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                    Dim whrcls As String = ""
                    If clsCommon.CompairString(obj.Document_Type, "PIVOT") = CompairStringResult.Equal Then
                        whrcls = " and emp_code='" + obj.Emp_Code + "' "
                    End If
                    Dim qry As String = "SELECT Count(*) FROM TSPL_LEAVE_ALLOTMENTDETAIL where LVALLOTMENT_CODE = '" & strCode & "' and LEAVE_CODE = '" & obj.LEAVE_CODE & "' " + whrcls + " "
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)

                    If check = 0 Then
                        clsCommon.AddColumnsForChange(coll, "LVALLOTMENT_CODE", strCode)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_ALLOTMENTDETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LEAVE_ALLOTMENTDETAIL", OMInsertOrUpdate.Update, " LVALLOTMENT_CODE = '" & strCode & "' and LEAVE_CODE = '" & obj.LEAVE_CODE & "' " + whrcls + " ", trans)
                    End If
                Next
            End If            
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function BlankDataTableForGrid() As DataTable
        Dim qry As String = " select TSPL_LEAVE_MASTER.LEAVE_CODE, TSPL_LEAVE_MASTER.LEAVE_NAME, '' as ALLOTED_LEAVE  from TSPL_LEAVE_MASTER "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

End Class
