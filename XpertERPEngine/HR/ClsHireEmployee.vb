Imports System.Data.SqlClient
Imports common

Public Class ClsHireEmployee

#Region "Variables"
    Public Applicant_Code As String = Nothing
    Public Bank_Code As String = Nothing
    Public IFSC_Code As String = Nothing
    Public Probation_Month As Double = 0
    Public Probation_Days As Double = 0
    Public Remarks As String = Nothing
    Public PAN_No As String = Nothing
    Public Bank_Account_No As String = Nothing
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
#End Region

    Public Shared Function SaveData(ByVal obj As ClsHireEmployee, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Applicant_Code", obj.Applicant_Code, True)
            clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code, True)
            clsCommon.AddColumnsForChange(coll, "Probation_Month", obj.Probation_Month)
            clsCommon.AddColumnsForChange(coll, "Probation_Days", obj.Probation_Days)
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "PAN_No", obj.PAN_No)
            clsCommon.AddColumnsForChange(coll, "IFSC_Code", obj.IFSC_Code)
            clsCommon.AddColumnsForChange(coll, "Bank_Account_No", obj.Bank_Account_No)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_HIRE_EMPLOYEE WHERE APPLICANT_CODE='" + obj.Applicant_Code + "'") <= 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

                Dim qry1 As String = "SELECT Count(*) FROM TSPL_HR_HIRE_EMPLOYEE where APPLICANT_CODE= '" & obj.Applicant_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check = 0 Then
                    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_HIRE_EMPLOYEE", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_HIRE_EMPLOYEE", OMInsertOrUpdate.Update, "APPLICANT_CODE='" + obj.Applicant_Code + "' AND Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHireEmployee
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsHireEmployee
        Dim obj As ClsHireEmployee = Nothing
        Dim Arr As List(Of ClsHireEmployee) = Nothing
        Dim qry As String = "select * from TSPL_HR_HIRE_EMPLOYEE where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'"
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_HIRE_EMPLOYEE.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_HIRE_EMPLOYEE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_HIRE_EMPLOYEE.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_HIRE_EMPLOYEE WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_HIRE_EMPLOYEE.APPLICANT_CODE = (select TOP 1 APPLICANT_CODE from TSPL_HR_HIRE_EMPLOYEE WHERE 1=1 " + whrclas + " and APPLICANT_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_HIRE_EMPLOYEE.APPLICANT_CODE = (select Min(APPLICANT_CODE) from TSPL_HR_HIRE_EMPLOYEE where APPLICANT_CODE > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_HIRE_EMPLOYEE.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_HIRE_EMPLOYEE where APPLICANT_CODE < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHireEmployee()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.IFSC_Code = clsCommon.myCstr(dt.Rows(0)("IFSC_Code"))
            obj.Probation_Month = clsCommon.myCdbl(dt.Rows(0)("Probation_Month"))
            obj.Probation_Days = clsCommon.myCdbl(dt.Rows(0)("Probation_Days"))
            obj.PAN_No = clsCommon.myCstr(dt.Rows(0)("PAN_No"))
            obj.Bank_Account_No = clsCommon.myCstr(dt.Rows(0)("Bank_Account_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function
    '' ------------------------------------ Nav. Query (=) --------------------------------------------------
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHireEmployee
        Dim obj As ClsHireEmployee = Nothing
        Dim Arr As List(Of ClsHireEmployee) = Nothing
        Dim qry As String = "select * from TSPL_HR_HIRE_EMPLOYEE where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "' AND APPLICANT_CODE ='" + strCode + "'"
        Dim whrclas As String = ""

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsHireEmployee()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.IFSC_Code = clsCommon.myCstr(dt.Rows(0)("IFSC_Code"))
            obj.Probation_Month = clsCommon.myCdbl(dt.Rows(0)("Probation_Month"))
            obj.Probation_Days = clsCommon.myCdbl(dt.Rows(0)("Probation_Days"))

            obj.PAN_No = clsCommon.myCstr(dt.Rows(0)("PAN_No"))
            obj.Bank_Account_No = clsCommon.myCstr(dt.Rows(0)("Bank_Account_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
        End If
        Return obj
    End Function
    '' ------------------------------------------------------------------------------------------------------
    'Public Shared Function EmpSaveAfterPost(ByVal AppName As String, ByVal strDocNo As String) As Boolean
    '    '  Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        EmpSaveAfterPost(AppName, strDocNo, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    Public Shared Function EmpSaveAfterPost(ByVal AppName As String, ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            Dim obj As New clsEmployeeMaster()

            'obj.EMP_CODE = clsCommon.myCstr(txtAppcode.Value)
            'obj.Emp_Name = clsCommon.myCstr(UcRequisitionDetail1.AppName)

            Dim objApp As New ClsApplicantEntry()
            objApp = ClsApplicantEntry.GetData(obj.EMP_CODE, Nothing, trans)
            If (objApp IsNot Nothing AndAlso clsCommon.myLen(objApp.APPLICANT_CODE) > 0) Then
                obj.Emp_Name = AppName
                obj.HRApplicant_Code = strDocNo
                Dim DeptCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ISNULL(TSPL_HR_REQUISITION.DEPARTMENT_CODE,'') AS [Department Code] From  TSPL_HR_REQUISITION LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_APPLICANT_ENTRY.Requisition_Code = TSPL_HR_REQUISITION.Requisition_Code WHERE TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE ='" + clsCommon.myCstr(objApp.APPLICANT_CODE) + "'", trans))
                obj.DEPARTMENT_CODE = clsCommon.myCstr(DeptCode)
                obj.Birth_date = clsCommon.GetPrintDate(objApp.Applicant_Date_Of_Birth, "dd/MM/yyyy")
                'obj.Joining_date = clsCommon.GetPrintDate(objApp, "dd/MM/yyyy")
                If clsCommon.CompairString(objApp.Gender, "M") = CompairStringResult.Equal Then
                    obj.SEX = "Male"
                ElseIf clsCommon.CompairString(objApp.Gender, "F") = CompairStringResult.Equal Then
                    obj.SEX = "Female"
                End If
                If clsCommon.CompairString(objApp.Maritial_Status, "M") = CompairStringResult.Equal Then
                    obj.MARITAL_STATUS = "Married"
                ElseIf clsCommon.CompairString(objApp.Maritial_Status, "U") = CompairStringResult.Equal Then
                    obj.MARITAL_STATUS = "Single"
                End If
                obj.LOCATION_CODE = clsCommon.myCstr(objApp.Location_Code)
                obj.BANK_ACC_NO = clsCommon.myCstr(objApp.Account_No)
                obj.BANK_CODE = clsCommon.myCstr(objApp.Bank_Code)
                obj.Add1 = clsCommon.myCstr(objApp.Add1)
                obj.Add2 = clsCommon.myCstr(objApp.Add2)
                obj.PRESENT_COUNTRY_CODE = clsCommon.myCstr(objApp.COUNTRY_CODE)
                obj.PRESENT_STATE_CODE = clsCommon.myCstr(objApp.State_Code)
                obj.PRESENT_CITY_CODE = clsCommon.myCstr(objApp.City_code)
                obj.Phone = clsCommon.myCstr(objApp.TELEPHONE_NO)
                obj.Pin_Code = clsCommon.myCstr(objApp.Pin_Code)
                obj.PAN_NO = clsCommon.myCstr(objApp.Pan_No)
                obj.DESCRIPTION = clsCommon.myCstr(objApp.Applicant_Description)
                obj.EMail_ID = clsCommon.myCstr(objApp.Email)

                'Dim OfferDate As String
                Dim dtCheck As DataTable
                dtCheck = clsDBFuncationality.GetDataTable("Select Offer_Date,CONVERT(VARCHAR,Date_Of_Joining,103) AS Date_Of_Joining From TSPL_HR_OFFER_LETTER Where APPLICANT_CODE ='" + clsCommon.myCstr(objApp.APPLICANT_CODE) + "'", trans)
                If dtCheck IsNot Nothing AndAlso dtCheck.Rows.Count > 0 Then
                    obj.CONFIRMATION_DATE = clsCommon.myCDate(dtCheck.Rows(0)("Offer_Date"))
                    obj.Joining_date = clsCommon.myCstr(dtCheck.Rows(0)("Date_Of_Joining"))
                Else
                    obj.CONFIRMATION_DATE = Nothing
                    obj.Joining_date = Nothing
                End If
               
                If (obj.SaveData(obj, True, trans)) Then
                    isSaved = True
                Else
                    isSaved = False
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strAppName As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(FormId, strDocNo, strAppName, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        'trans.Commit()
        Return True
    End Function
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String, ByVal strAppName As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Applicant code not found to Post")
            End If
            Dim obj As ClsHireEmployee = ClsHireEmployee.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Applicant_Code) <= 0) Then
                Throw New Exception("No data found to post")
            End If
            EmpSaveAfterPost(strAppName, strDocNo, trans)
            Dim qry = "Update TSPL_HR_HIRE_EMPLOYEE set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "',Posted_By ='" + objCommonVar.CurrentUserCode + "'" & _
            " where Applicant_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
