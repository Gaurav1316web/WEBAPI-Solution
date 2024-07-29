Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsFFSettlement
#Region "Variables"

    Public EMP_CODE As String
    Public EMP_NAME As String
    Public DEPARTMENT_CODE As String
    Public DESIGNATION_ID As String
    Public NOTICE_PERIOD As Integer
    Public SHORT_FALL_DAYS As Integer
    Public SHORT_FALL_DAYS_STOCK As Decimal
    Public LEAVING_REASON As String
    Public TOTAL_SERVICE_PERIOD As String
    Public DESCRIPTION As String

    Public PAY_PERIOD_CODE As String
    Public Last_PAY_PERIOD_CODE As String
    Public WORKING_DAYS_AFTER_LAST_SAL As Integer
    Public DOJ As Date
    Public RESIGN_SUBMIT_DATE As Date

    Public LAST_WORKING_DAY As Date
    Public ACTUAL_LAST_WORKING_DAY As Date? = Nothing
    Public LAST_SALARY_UPTO_DATE As Date

    Public TOTAL_EARNING_AMOUNT As Decimal
    Public TOTAL_OTHR_EARNING_AMOUNT As Decimal
    Public TOTAL_DEDUCTION_AMOUNT As Decimal
    Public NET_PAYABLE_AMOUNT As Decimal

    Public CREATED_BY As String
    Public POSTED As Boolean
    Public Posting_Date As Date
    Public Created_Date As Date

    Public ObjListSalary As List(Of clsFFSalary)
    Public Document_Date As Date?
    Public CHEQUE_NO As String
    Public CHEQUE_DATED As Date?
    Public CHEQUE_AMOUNT As Decimal
    Public CHEQUE_CLEARANCE_DATE As Date?
    Public Sal_Gen_Code As String
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsFFSettlement
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = True

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String

            '' delete TSPL_FF_SALARY
            qry = "DELETE FROM TSPL_FF_SALARY WHERE EMP_CODE='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            qry = "delete from TSPL_FF_SETTLEMENT_HEAD where EMP_CODE ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsFFSettlement
        Dim obj As New clsFFSettlement()

        Dim qry As String = "SELECT T1.EMP_CODE,EMP.EMP_NAME,T1.DESCRIPTION,T1.TOTAL_SERVICE_PERIOD,T1.DOJ,T1.RESIGN_SUBMIT_DATE,T1.PAY_PERIOD_CODE,T1.WORKING_DAYS_AFTER_LAST_SAL,T1.LAST_WORKING_DAY,T1.ACTUAL_LAST_WORKING_DAY,T1.LAST_SALARY_UPTO_DATE,T1.DEPARTMENT_CODE,"
        qry += " T1.TOTAL_EARNING_AMOUNT,T1.TOTAL_OTHR_EARNING_AMOUNT,T1.TOTAL_DEDUCTION_AMOUNT,T1.NET_PAYABLE_AMOUNT,T1.DESIGNATION_ID,T1.LEAVING_REASON,T1.NOTICE_PERIOD,T1.SHORT_FALL_DAYS,"
        qry += " T1.MODIFIED_BY,T1.Created_By,T1.POSTED,T1.Created_Date,T1.POSTING_DATE,T1.Document_Date,T1.CHEQUE_NO,T1.CHEQUE_DATED,T1.SAL_GEN_CODE,T1.Last_PAY_PERIOD_CODE,T1.CHEQUE_CLEARANCE_DATE,T1.CHEQUE_AMOUNT FROM TSPL_FF_SETTLEMENT_HEAD T1 LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON T1.EMP_CODE=EMP.EMP_CODE    where 2=2 "

        Select Case NavType
            Case NavigatorType.First
                qry += " AND T1.EMP_CODE = (select MIN(EMP_CODE) from TSPL_FF_SETTLEMENT_HEAD)"
            Case NavigatorType.Last
                qry += " AND T1.EMP_CODE = (select Max(EMP_CODE) from TSPL_FF_SETTLEMENT_HEAD)"
            Case NavigatorType.Next
                qry += " AND T1.EMP_CODE = (select Min(EMP_CODE) from TSPL_FF_SETTLEMENT_HEAD where  EMP_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " AND T1.EMP_CODE = (select Max(EMP_CODE) from TSPL_FF_SETTLEMENT_HEAD where EMP_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " AND T1.EMP_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.EMP_CODE = dt.Rows(0)("EMP_CODE")
            obj.EMP_NAME = dt.Rows(0)("EMP_NAME")
            obj.DEPARTMENT_CODE = dt.Rows(0)("DEPARTMENT_CODE")
            obj.SHORT_FALL_DAYS = dt.Rows(0)("SHORT_FALL_DAYS")
            obj.NOTICE_PERIOD = dt.Rows(0)("NOTICE_PERIOD")

            obj.LEAVING_REASON = dt.Rows(0)("LEAVING_REASON")
            obj.DESIGNATION_ID = clsCommon.myCstr(dt.Rows(0)("DESIGNATION_ID"))
            obj.NOTICE_PERIOD = clsCommon.myCstr(dt.Rows(0)("NOTICE_PERIOD"))
            obj.TOTAL_SERVICE_PERIOD = clsCommon.myCstr(dt.Rows(0)("TOTAL_SERVICE_PERIOD"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))

            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.Last_PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("Last_PAY_PERIOD_CODE"))

            obj.WORKING_DAYS_AFTER_LAST_SAL = clsCommon.myCstr(dt.Rows(0)("WORKING_DAYS_AFTER_LAST_SAL"))

            obj.DOJ = clsCommon.GetPrintDate(dt.Rows(0)("DOJ"), "dd/MMM/yyyy")
            obj.RESIGN_SUBMIT_DATE = clsCommon.GetPrintDate(dt.Rows(0)("RESIGN_SUBMIT_DATE"), "dd/MMM/yyyy")

            '' PLAN START DATE
            If IsDBNull(dt.Rows(0)("LAST_WORKING_DAY")) = True Then
                obj.LAST_WORKING_DAY = Nothing
            Else
                obj.LAST_WORKING_DAY = clsCommon.GetPrintDate(dt.Rows(0)("LAST_WORKING_DAY"), "dd/MMM/yyyy")
            End If

            '' PLAN END DATE
            If IsDBNull(dt.Rows(0)("ACTUAL_LAST_WORKING_DAY")) = True Then
                obj.ACTUAL_LAST_WORKING_DAY = Nothing
            Else
                obj.ACTUAL_LAST_WORKING_DAY = clsCommon.GetPrintDate(dt.Rows(0)("ACTUAL_LAST_WORKING_DAY"), "dd/MMM/yyyy")
            End If

            '' ACTUAL START DATE
            If IsDBNull(dt.Rows(0)("LAST_SALARY_UPTO_DATE")) = True Then
                obj.LAST_SALARY_UPTO_DATE = Nothing
            Else
                obj.LAST_SALARY_UPTO_DATE = clsCommon.GetPrintDate(dt.Rows(0)("LAST_SALARY_UPTO_DATE"), "dd/MMM/yyyy")
            End If

            obj.TOTAL_EARNING_AMOUNT = clsCommon.myCstr(dt.Rows(0)("TOTAL_EARNING_AMOUNT"))
            obj.TOTAL_OTHR_EARNING_AMOUNT = clsCommon.myCstr(dt.Rows(0)("TOTAL_OTHR_EARNING_AMOUNT"))
            obj.TOTAL_DEDUCTION_AMOUNT = clsCommon.myCstr(dt.Rows(0)("TOTAL_DEDUCTION_AMOUNT"))
            obj.NET_PAYABLE_AMOUNT = clsCommon.myCstr(dt.Rows(0)("NET_PAYABLE_AMOUNT"))


            obj.CREATED_BY = clsCommon.myCstr(dt.Rows(0)("CREATED_BY"))
            obj.POSTED = clsCommon.myCstr(dt.Rows(0)("POSTED"))
            If IsDBNull(dt.Rows(0)("Document_Date")) Then
                obj.Document_Date = Nothing
            Else
                obj.Document_Date = clsCommon.myCstr(dt.Rows(0)("Document_Date"))
            End If

            strCode = dt.Rows(0)("EMP_CODE")

            '' POSTING DATE
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            obj.Sal_Gen_Code = clsCommon.myCstr(dt.Rows(0)("SAL_GEN_CODE"))
            '' CREATED DATE
            If clsCommon.myLen(dt.Rows(0)("Created_Date")) > 0 Then
                obj.Created_Date = clsCommon.myCDate(dt.Rows(0)("Created_Date"))
            Else
                obj.Created_Date = Nothing
            End If
            obj.CHEQUE_NO = clsCommon.myCstr(dt.Rows(0)("CHEQUE_NO"))
            If Not IsDBNull(dt.Rows(0)("CHEQUE_DATED")) Then
                obj.CHEQUE_DATED = clsCommon.GetPrintDate(dt.Rows(0)("CHEQUE_DATED"), "dd/MMM/yyyy")
            End If
            obj.CHEQUE_AMOUNT = clsCommon.myCstr(dt.Rows(0)("CHEQUE_AMOUNT"))
            If Not IsDBNull(dt.Rows(0)("CHEQUE_CLEARANCE_DATE")) Then
                obj.CHEQUE_CLEARANCE_DATE = clsCommon.GetPrintDate(dt.Rows(0)("CHEQUE_CLEARANCE_DATE"), "dd/MMM/yyyy")
            End If
            obj.ObjListSalary = clsFFSalary.GetFFSalary(strCode, trans)
        End If
        Return obj
    End Function
    Public Shared Function SaveData(ByVal obj As clsFFSettlement, ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            SaveData(obj, isNewEntry, trans, strCode)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function SaveData(ByVal obj As clsFFSettlement, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True

        If isNewEntry Then
            If strCode = "" Then
                obj.EMP_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"), clsDocType.MO, "", "")
            Else
                obj.EMP_CODE = strCode
            End If
        End If


        Try
            Dim qry As String
            '' delete TSPL_FF_SALARY
            qry = "DELETE FROM TSPL_FF_SALARY WHERE EMP_CODE='" + obj.EMP_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)


            Dim strDocNo As String = ""

            If (clsCommon.myLen(obj.EMP_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", obj.DEPARTMENT_CODE)
            clsCommon.AddColumnsForChange(coll, "DESIGNATION_ID", obj.DESIGNATION_ID)
            clsCommon.AddColumnsForChange(coll, "SHORT_FALL_DAYS", obj.SHORT_FALL_DAYS)

            clsCommon.AddColumnsForChange(coll, "NOTICE_PERIOD", obj.NOTICE_PERIOD)
            clsCommon.AddColumnsForChange(coll, "LEAVING_REASON", obj.LEAVING_REASON)
            clsCommon.AddColumnsForChange(coll, "TOTAL_SERVICE_PERIOD", obj.TOTAL_SERVICE_PERIOD)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)

            clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Last_PAY_PERIOD_CODE", obj.Last_PAY_PERIOD_CODE, True)
            clsCommon.AddColumnsForChange(coll, "WORKING_DAYS_AFTER_LAST_SAL", obj.WORKING_DAYS_AFTER_LAST_SAL)
            clsCommon.AddColumnsForChange(coll, "DOJ", clsCommon.GetPrintDate(obj.DOJ, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "RESIGN_SUBMIT_DATE", clsCommon.GetPrintDate(obj.RESIGN_SUBMIT_DATE, "dd/MMM/yyyy"))


            clsCommon.AddColumnsForChange(coll, "LAST_WORKING_DAY", clsCommon.GetPrintDate(obj.LAST_WORKING_DAY, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ACTUAL_LAST_WORKING_DAY", clsCommon.GetPrintDate(obj.ACTUAL_LAST_WORKING_DAY, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "LAST_SALARY_UPTO_DATE", clsCommon.GetPrintDate(obj.LAST_SALARY_UPTO_DATE, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "TOTAL_EARNING_AMOUNT", clsCommon.myCdbl(obj.TOTAL_EARNING_AMOUNT))
            clsCommon.AddColumnsForChange(coll, "TOTAL_OTHR_EARNING_AMOUNT", clsCommon.myCdbl(obj.TOTAL_OTHR_EARNING_AMOUNT))
            clsCommon.AddColumnsForChange(coll, "TOTAL_DEDUCTION_AMOUNT", clsCommon.myCdbl(obj.TOTAL_DEDUCTION_AMOUNT))
            clsCommon.AddColumnsForChange(coll, "NET_PAYABLE_AMOUNT", clsCommon.myCdbl(obj.NET_PAYABLE_AMOUNT))
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "CHEQUE_NO", obj.CHEQUE_NO)
            If Not obj.CHEQUE_DATED Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "CHEQUE_DATED", clsCommon.GetPrintDate(obj.CHEQUE_DATED, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "CHEQUE_AMOUNT", obj.CHEQUE_AMOUNT)
            If Not obj.CHEQUE_CLEARANCE_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "CHEQUE_CLEARANCE_DATE", clsCommon.GetPrintDate(obj.CHEQUE_CLEARANCE_DATE, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then

                'clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                Dim Strqry As String = "SELECT Count(*) FROM TSPL_FF_SETTLEMENT_HEAD where EMP_CODE = '" & obj.EMP_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FF_SETTLEMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FF_SETTLEMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_FF_SETTLEMENT_HEAD.EMP_CODE='" + obj.EMP_CODE + "'", trans)
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FF_SETTLEMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_FF_SETTLEMENT_HEAD.EMP_CODE='" + obj.EMP_CODE + "'", trans)
            End If

            '' saving ff salary
            If Not obj.ObjListSalary Is Nothing Then
                isSaved = isSaved AndAlso clsFFSalary.SaveData(obj.EMP_CODE, obj.ObjListSalary, trans)
            End If


        Catch err As Exception
            Throw New Exception(err.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsFFSettlement = clsFFSettlement.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.EMP_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim IsSaved As Boolean = True
            Dim objStatus As clsEmployeeStatus
            Dim Emp_Status_Code As String = ""
            '' get any salary generation that is not posted till date
            Dim qry As String = " select GS.SALARY_GENERATION_CODE,GS.PAY_PERIOD_CODE from TSPL_GENERATE_SALARY GS inner join TSPL_GENERATE_SALARY_ATTENDANCE GSA " & _
                                " on GS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE where GSA.EMP_CODE='" & obj.EMP_CODE & "' and GS.POSTED=0 "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim msg As String = ""
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    msg = msg & clsCommon.myCstr(dr.Item("PAY_PERIOD_CODE")) & Environment.NewLine
                Next
                Throw New Exception("Salary is not posted for the Month- " & msg & "")
            End If

            Emp_Status_Code = clsEmployeeStatus.GetEmployeeStatus(obj.EMP_CODE, obj.ACTUAL_LAST_WORKING_DAY, trans)
            objStatus = clsEmployeeStatus.GetData(Emp_Status_Code, NavigatorType.Current, trans)
            
            '' save and post salary
            Dim objSal As New clsSalaryGeneration
            Dim arrEMP As New ArrayList
            arrEMP.Add(obj.EMP_CODE)
            objSal.EmpList = arrEMP
            objSal.Code = obj.Sal_Gen_Code
            objSal.LOCATION_CODE = clsEmployeeMaster.GetLocation(obj.EMP_CODE, trans)
            objSal.DEVISION_CODE = objStatus.DEVISION_CODE
            objSal.PAY_PERIOD_CODE = obj.PAY_PERIOD_CODE
            objSal.PAYPERIOD_DAYS = obj.WORKING_DAYS_AFTER_LAST_SAL
            objSal.GENERATE_DATE = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            objSal.GENERATED_BY = obj.EMP_CODE
            objSal.GENERATE_REMARKS = obj.DESCRIPTION

            ''''''
            qry = " select EMP.SALARY_ACCOUNT_CODE,ACC.BANK_CODE,ACC.GL_Employer_ESI_PAYABLE,ACC.GL_EMPLOYER_OTHERS_PAYABLE,ACC.GL_Employer_PF_PAYABLE,ACC.GL_SALARY_PAYABLE " & _
                  " from TSPL_EMPLOYEE_MASTER EMP left join TSPL_PAYROLL_ACCOUNTSETS ACC on EMP.SALARY_ACCOUNT_CODE=ACC.ACCOUNT_SET_CODE where EMP.EMP_CODE='" & obj.EMP_CODE & "' "
            Dim dtEmp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtEmp.Rows.Count > 0 Then
                objSal.SALARY_PAYABLE_ACC = clsCommon.myCstr(dtEmp.Rows(0).Item("GL_SALARY_PAYABLE"))
                objSal.GL_Employer_ESI_PAYABLE = clsCommon.myCstr(dtEmp.Rows(0).Item("GL_Employer_ESI_PAYABLE"))
                objSal.GL_Employer_PF_PAYABLE = clsCommon.myCstr(dtEmp.Rows(0).Item("GL_Employer_PF_PAYABLE"))
                objSal.SAL_ACCOUNT_SET = clsCommon.myCstr(dtEmp.Rows(0).Item("SALARY_ACCOUNT_CODE"))
                objSal.GL_EMPLOYER_OTHERS_PAYABLE = clsCommon.myCstr(dtEmp.Rows(0).Item("GL_EMPLOYER_OTHERS_PAYABLE"))
            End If            
            objSal.CHEQUE_NO = obj.CHEQUE_NO
            objSal.CHEQUE_DATED = obj.CHEQUE_DATED
            objSal.CREATE_FE = True
            If objSal.SaveData(objSal, IIf(clsCommon.myLen(objSal.Code) > 0, False, True), trans) Then
                '' update actual salary from full and final table
                qry = " update TSPL_GENERATE_SALARY_PAYHEADS set ACTUAL_AMOUNT=FF.ACTUAL_AMOUNT from (SELECT TSPL_FF_SETTLEMENT_HEAD.SAL_GEN_CODE,TSPL_FF_SALARY.EMP_CODE,TSPL_FF_SALARY.PAY_HEAD_CODE,TSPL_FF_SALARY.ACTUAL_AMOUNT " & _
                      " FROM TSPL_FF_SALARY INNER JOIN TSPL_FF_SETTLEMENT_HEAD ON TSPL_FF_SALARY.EMP_CODE=TSPL_FF_SETTLEMENT_HEAD.EMP_CODE) FF " & _
                      " where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=FF.EMP_CODE AND TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=FF.PAY_HEAD_CODE " & _
                      " AND TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE='" & objSal.Code & "' and TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE='" & obj.EMP_CODE & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '' POST SALARY
                clsSalaryGeneration.PostData(objSal.Code, trans)
            Else
                System.Diagnostics.Process.Start("c:\ERPTempFolder\salgenlog.txt")
            End If

            '' SAVE FINAL STATUS OF EMPLOYEE
            objStatus.APPLICABLE_FROM = obj.ACTUAL_LAST_WORKING_DAY
            objStatus.WORKING_STATUS = "Resigned"
            '' done by Panch Raj against ticket no:BM00000008618
            Dim IsNewEntry As Boolean = False
            Dim CheckSetting As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveMultipleEmployeeStatus, clsFixedParameterCode.AllowToSaveMultipleEmployeeStatus, trans))
            If clsCommon.CompairString(CheckSetting, "1") = CompairStringResult.Equal Then
                IsNewEntry = True
                objStatus.Code = ""
                objStatus.REVISION_NO = objStatus.REVISION_NO + 1
            Else
                IsNewEntry = False
                objStatus.Code = clsEmployeeStatus.GetEmployeeLatestStatus(obj.EMP_CODE, trans)
            End If
            'objStatus.Code = ""
            IsSaved = IsSaved AndAlso objStatus.SaveData(objStatus, IsNewEntry, objStatus.Code, trans)

            qry = "Update TSPL_FF_SETTLEMENT_HEAD set SAL_GEN_CODE='" & objSal.Code & "', POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where EMP_CODE ='" + strDocNo + "'"
            IsSaved = IsSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

End Class

Public Class clsFFSalary
#Region "Variables"
    Public EMP_CODE As String
    Public Line_No As Integer
    Public PAY_HEAD_CODE As String
    Public PAY_HEAD_DESC As String
    Public PAYHEAD_FORMULA As String
    Public RATE_AMOUNT As Decimal
    Public PAYHEAD_AMOUNT As Decimal
    Public ACTUAL_AMOUNT As Decimal

    Public REMARKS As String
    Public IS_OTHR_EARNING As Integer
    Public IS_DEDUCTION As Integer

#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsFFSalary), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsFFSalary In Arr
                Dim coll As New Hashtable()

                clsCommon.AddColumnsForChange(coll, "EMP_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PAY_HEAD_CODE)
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_DESC", obj.PAY_HEAD_DESC)

                clsCommon.AddColumnsForChange(coll, "PAYHEAD_FORMULA", obj.PAYHEAD_FORMULA)
                clsCommon.AddColumnsForChange(coll, "RATE_AMOUNT", obj.RATE_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "PAYHEAD_AMOUNT", obj.PAYHEAD_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "ACTUAL_AMOUNT", obj.ACTUAL_AMOUNT)

                clsCommon.AddColumnsForChange(coll, "REMARKS", obj.REMARKS)
                clsCommon.AddColumnsForChange(coll, "IS_OTHR_EARNING", obj.IS_OTHR_EARNING)
                clsCommon.AddColumnsForChange(coll, "IS_DEDUCTION", obj.IS_DEDUCTION)

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FF_SALARY", OMInsertOrUpdate.Insert, "TSPL_FF_SALARY.EMP_CODE='" + strDocNo + "' and TSPL_FF_SALARY.PAY_HEAD_CODE='" + obj.PAY_HEAD_CODE + "' and  TSPL_FF_SALARY.PAY_HEAD_DESC='" + obj.PAY_HEAD_DESC + "' ", trans)
            Next

        End If

        Return True
    End Function
    Public Shared Function GetFFSalary(ByVal EMP_CODE As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsFFSalary)
        Dim dt As New DataTable
        Dim qry As String = ""
        qry = "SELECT * FROM TSPL_FF_SALARY where EMP_CODE='" & EMP_CODE & "'"
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objtr As clsFFSalary
        Dim ObjList As New List(Of clsFFSalary)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsFFSalary()

                objtr.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                objtr.Line_No = clsCommon.myCdbl(dr("Line_No"))
                objtr.PAY_HEAD_CODE = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.PAY_HEAD_DESC = clsCommon.myCstr(dr("PAY_HEAD_DESC"))
                objtr.PAYHEAD_FORMULA = clsCommon.myCstr(dr("PAYHEAD_FORMULA"))
                objtr.RATE_AMOUNT = clsCommon.myCdbl(dr("RATE_AMOUNT"))
                objtr.PAYHEAD_AMOUNT = clsCommon.myCdbl(dr("PAYHEAD_AMOUNT"))
                objtr.ACTUAL_AMOUNT = clsCommon.myCdbl(dr("ACTUAL_AMOUNT"))

                objtr.IS_OTHR_EARNING = clsCommon.myCdbl(dr("IS_OTHR_EARNING"))
                objtr.IS_DEDUCTION = clsCommon.myCdbl(dr("IS_DEDUCTION"))
                objtr.REMARKS = clsCommon.myCstr(dr("REMARKS"))

                ObjList.Add(objtr)
            Next
        End If
        Return ObjList
    End Function

End Class
