Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSalaryGeneration


#Region "Variables"
    Public Code As String
    Public PAY_PERIOD_CODE As String
    Public PAYPERIOD_DAYS As Int16
    Public GENERATE_DATE As DateTime
    Public GENERATED_BY As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public GENERATE_REMARKS As String
    Public SALARY_PAYABLE_AMT As Decimal = 0
    Public SALARY_PAYABLE_ACC As String
    Public SALARY_PAYABLE_ACC_Desc As String
    Public BANKGL_ACC As String
    Public BANKGL_ACC_Desc As String
    Public BANK_CODE As String
    Public SAL_ACCOUNT_SET As String
    Public SOURCE_CODE As String
    Public CHEQUE_NO As String
    Public CHEQUE_DATED As Date?
    Public Payment_Date As Date?
    Public Payment_Bank_Code As String = String.Empty
    Public Todate As Date?
    Public LOCATION_CODE As String
    Public LOCATION_DESC As String
    Public DEVISION_CODE As String
    Public DEVISION_NAME As String
    Public GL_Employer_PF_PAYABLE As String
    Public GL_Employer_PF_PAYABLE_Desc As String
    Public GL_Employer_ESI_PAYABLE As String
    Public GL_Employer_ESI_PAYABLE_Desc As String
    Public GL_EMPLOYER_OTHERS_PAYABLE As String
    Public GL_EMPLOYER_OTHERS_PAYABLE_Desc As String
    Public CREATE_FE As Boolean = False
    Public LEAVEALLOTMENT_CODE As String = ""
    Public GL_SALARY_PAYABLE As String
    Public GL_SALARY_PAYABLE_AMOUNT As String
    Public EmpList As New ArrayList
    Public arrAccGL As New List(Of clsSalaryFEAccounts)
    Public arrEMP As ArrayList
#End Region

    Public Shared Function getFinderForSalaryLocation(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        If clsCommon.myLen(whrcls) <= 0 Then
            whrcls = " LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY )"
        End If
        Dim str As String = clsCommon.ShowSelectForm("Locmst", qry, "Code", whrcls, curcode, "Location_Code", isButtonClicked)
        Return str
    End Function
    Public Shared Function GetFinderQueryForSalaryLocationMulti(ByVal PayPeriodCode As String) As String
        Dim qry As String = " select distinct GS.Location_Code as Code,Loc.Location_Desc as [Name] from TSPL_GENERATE_SALARY GS left join TSPL_LOCATION_MASTER Loc on Loc.Location_Code=GS.Location_Code  where 2=2 "
        If clsCommon.myLen(PayPeriodCode) > 0 Then
            qry = qry & " and GS.LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & PayPeriodCode & "')"
        End If
        Return qry
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsSalaryGeneration
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal Code As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(Code, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean
        isSaved = False
        If (clsCommon.myLen(Code) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim obj As clsSalaryGeneration = clsSalaryGeneration.GetData(Code, NavigatorType.Current, trans)
        Dim qry As String
        qry = "delete from TSPL_LEAVE_ALLOTMENTDETAIL where LVALLOTMENT_CODE='" & obj.LEAVEALLOTMENT_CODE & "'"
        clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_GENERATE_SALARY_ATTENDANCE where SALARY_GENERATION_CODE ='" & obj.Code & "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE ='" & obj.Code & "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE ='" & obj.Code & "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_LEAVE_ALLOTMENT where LVALLOTMENT_CODE ='" & obj.LEAVEALLOTMENT_CODE & "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsSalaryGeneration
        Dim obj As clsSalaryGeneration = Nothing
        Dim qry As String = "select TSPL_GENERATE_SALARY.*,TSPL_GL_ACCOUNTS.Description AS GL_SALARY_PAYABLE_DESC, " &
        " TSPL_LOCATION_MASTER.LOCATION_DESC,TSPL_DEVISION_MASTER.DEVISION_NAME,GL_PF.Description as GL_Employer_PF_PAYABLE_Desc, " &
        " GL_ESI.Description as GL_Employer_ESI_PAYABLE_Desc,GL_Othr.Description as GL_EMPLOYER_OTHERS_PAYABLE_Desc  from TSPL_GENERATE_SALARY " &
        " LEFT JOIN TSPL_GL_ACCOUNTS ON TSPL_GENERATE_SALARY.GL_SALARY_PAYABLE=TSPL_GL_ACCOUNTS.Account_Code " &
        " LEFT JOIN TSPL_GL_ACCOUNTS as GL_PF ON TSPL_GENERATE_SALARY.GL_Employer_PF_PAYABLE=GL_PF.Account_Code " &
        " LEFT JOIN TSPL_GL_ACCOUNTS as GL_ESI ON TSPL_GENERATE_SALARY.GL_Employer_ESI_PAYABLE=GL_ESI.Account_Code " &
        " LEFT JOIN TSPL_GL_ACCOUNTS as GL_Othr ON TSPL_GENERATE_SALARY.GL_EMPLOYER_OTHERS_PAYABLE=GL_Othr.Account_Code " &
        " LEFT JOIN TSPL_LOCATION_MASTER ON TSPL_GENERATE_SALARY.LOCATION_CODE=TSPL_LOCATION_MASTER.LOCATION_CODE " &
        " LEFT JOIN TSPL_DEVISION_MASTER ON TSPL_GENERATE_SALARY.DEVISION_CODE=TSPL_DEVISION_MASTER.DEVISION_CODE where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and SALARY_GENERATION_CODE = (select MIN(SALARY_GENERATION_CODE) from TSPL_GENERATE_SALARY)"
            Case NavigatorType.Last
                qry += " and SALARY_GENERATION_CODE = (select Max(SALARY_GENERATION_CODE) from TSPL_GENERATE_SALARY)"
            Case NavigatorType.Next
                qry += " and SALARY_GENERATION_CODE = (select Min(SALARY_GENERATION_CODE) from TSPL_GENERATE_SALARY where  SALARY_GENERATION_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and SALARY_GENERATION_CODE = (select Max(SALARY_GENERATION_CODE) from TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and SALARY_GENERATION_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsSalaryGeneration()
            obj.Code = clsCommon.myCstr(dt.Rows(0)("SALARY_GENERATION_CODE"))
            obj.LOCATION_CODE = clsCommon.myCstr(dt.Rows(0)("LOCATION_CODE"))
            obj.LOCATION_DESC = clsCommon.myCstr(dt.Rows(0)("LOCATION_DESC"))
            obj.DEVISION_CODE = clsCommon.myCstr(dt.Rows(0)("DEVISION_CODE"))
            obj.DEVISION_NAME = clsCommon.myCstr(dt.Rows(0)("DEVISION_NAME"))
            obj.PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAY_PERIOD_CODE"))
            obj.PAYPERIOD_DAYS = Convert.ToInt16(clsCommon.myCdbl(dt.Rows(0)("PAYPERIOD_DAYS")))
            obj.GENERATE_DATE = clsCommon.myCDate(dt.Rows(0)("GENERATE_DATE"))
            obj.GENERATED_BY = clsCommon.myCstr(dt.Rows(0)("GENERATED_BY"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            obj.CREATE_FE = clsCommon.myCBool(dt.Rows(0)("CREATE_FE"))
            obj.SALARY_PAYABLE_ACC = clsCommon.myCstr(dt.Rows(0)("GL_SALARY_PAYABLE"))
            obj.SALARY_PAYABLE_ACC_Desc = clsCommon.myCstr(dt.Rows(0)("GL_SALARY_PAYABLE_DESC"))
            obj.BANKGL_ACC = clsCommon.myCstr(dt.Rows(0)("BANKACC"))
            obj.BANK_CODE = clsCommon.myCstr(dt.Rows(0)("BANK_CODE"))
            obj.SAL_ACCOUNT_SET = clsCommon.myCstr(dt.Rows(0)("SAL_ACCOUNT_SET"))
            obj.SOURCE_CODE = clsCommon.myCstr(dt.Rows(0)("SOURCE_CODE"))
            obj.CHEQUE_NO = clsCommon.myCstr(dt.Rows(0)("CHEQUE_NO"))

            obj.GL_Employer_PF_PAYABLE = clsCommon.myCstr(dt.Rows(0)("GL_Employer_PF_PAYABLE"))
            obj.GL_Employer_ESI_PAYABLE = clsCommon.myCstr(dt.Rows(0)("GL_Employer_ESI_PAYABLE"))
            obj.GL_EMPLOYER_OTHERS_PAYABLE = clsCommon.myCstr(dt.Rows(0)("GL_EMPLOYER_OTHERS_PAYABLE"))

            obj.GL_Employer_PF_PAYABLE_Desc = clsCommon.myCstr(dt.Rows(0)("GL_Employer_PF_PAYABLE_Desc"))
            obj.GL_Employer_ESI_PAYABLE_Desc = clsCommon.myCstr(dt.Rows(0)("GL_Employer_ESI_PAYABLE_Desc"))
            obj.GL_EMPLOYER_OTHERS_PAYABLE_Desc = clsCommon.myCstr(dt.Rows(0)("GL_EMPLOYER_OTHERS_PAYABLE_Desc"))
            '' new column added by Panch Raj
            obj.LEAVEALLOTMENT_CODE = clsCommon.myCstr(dt.Rows(0)("LEAVEALLOTMENT_CODE"))

            If IsDBNull(dt.Rows(0)("CHEQUE_DATED")) Then
            Else
                obj.CHEQUE_DATED = clsCommon.myCDate(dt.Rows(0)("CHEQUE_DATED"))
            End If
            If IsDBNull(dt.Rows(0)("Payment_Date")) Then
            Else
                obj.Payment_Date = clsCommon.myCDate(dt.Rows(0)("Payment_Date"))
            End If
            obj.Payment_Bank_Code = clsCommon.myCstr(dt.Rows(0)("Payment_Bank_Code"))

            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            obj.GENERATE_REMARKS = clsCommon.myCstr(dt.Rows(0)("GENERATE_REMARKS"))
            obj.GL_SALARY_PAYABLE = clsCommon.myCstr(dt.Rows(0)("GL_SALARY_PAYABLE"))
            obj.GL_SALARY_PAYABLE_AMOUNT = clsCommon.myCdbl(dt.Rows(0)("GL_SALARY_PAYABLE_AMOUNT"))

            obj.arrAccGL = clsSalaryFEAccounts.GetDFEACList(obj.Code, trans)

            obj.arrEMP = Nothing
            qry = "select distinct EMP_CODE from TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" + obj.Code + "'"
            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                obj.arrEMP = New ArrayList
                For Each dr As DataRow In dtTemp.Rows
                    obj.arrEMP.Add(clsCommon.myCstr(dr("EMP_CODE")))
                Next
            End If

        End If
        Return obj
    End Function
    Public Function SaveData(ByVal isNewEntry As Boolean, ByVal obj As clsSalaryGeneration) As Boolean
        clsCommon.ProgressBarShow()
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            If Generate_Salary(obj.PAY_PERIOD_CODE, obj.EmpList, trans) = False Then
                Return False
            End If
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)
            trans.Commit()
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return isSaved
    End Function
    Public Function SaveData(ByVal obj As clsSalaryGeneration, ByVal isNewEntry As Boolean) As Boolean
        clsCommon.ProgressBarShow()
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = Nothing
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            If Generate_Salary(obj.PAY_PERIOD_CODE, obj.EmpList, trans) = False Then
                Return False
            End If
            isSaved = isSaved AndAlso SaveData(obj, isNewEntry, trans)
            'trans.Commit()
            clsCommon.ProgressBarHide()
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Function SaveData(ByVal obj As clsSalaryGeneration, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleHR, clsUserMgtCode.frmSalaryGeneration, obj.LOCATION_CODE, obj.GENERATE_DATE, trans)
        If clsCommon.myLen(obj.Code) <= 0 Then
            obj.Code = clsERPFuncationality.GetNextCode(trans, Todate, clsDocType.SalaryGeneration, "", "")
        Else
            obj.Code = clsCommon.myCstr(obj.Code)
        End If
        If clsCommon.myLen(obj.Code) <= 0 Then
            Return False
        End If
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "LOCATION_CODE", obj.LOCATION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE, True)
        clsCommon.AddColumnsForChange(coll, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
        clsCommon.AddColumnsForChange(coll, "GENERATE_REMARKS", obj.GENERATE_REMARKS)
        clsCommon.AddColumnsForChange(coll, "PAYPERIOD_DAYS", obj.PAYPERIOD_DAYS)
        clsCommon.AddColumnsForChange(coll, "GENERATE_DATE", clsCommon.GetPrintDate(obj.GENERATE_DATE, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "GENERATED_BY", obj.GENERATED_BY)
        clsCommon.AddColumnsForChange(coll, "POSTED", obj.POSTED)
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

        clsCommon.AddColumnsForChange(coll, "GL_SALARY_PAYABLE", obj.SALARY_PAYABLE_ACC, True)
        clsCommon.AddColumnsForChange(coll, "BANKACC", obj.BANKGL_ACC, True)
        clsCommon.AddColumnsForChange(coll, "BANK_CODE", obj.BANK_CODE, True)
        clsCommon.AddColumnsForChange(coll, "SAL_ACCOUNT_SET", obj.SAL_ACCOUNT_SET)
        clsCommon.AddColumnsForChange(coll, "SOURCE_CODE", obj.SOURCE_CODE, True)
        clsCommon.AddColumnsForChange(coll, "CHEQUE_NO", obj.CHEQUE_NO)
        clsCommon.AddColumnsForChange(coll, "CREATE_FE", obj.CREATE_FE)
        clsCommon.AddColumnsForChange(coll, "GL_Employer_PF_PAYABLE", obj.GL_Employer_PF_PAYABLE, True)
        clsCommon.AddColumnsForChange(coll, "GL_Employer_ESI_PAYABLE", obj.GL_Employer_ESI_PAYABLE, True)
        clsCommon.AddColumnsForChange(coll, "GL_EMPLOYER_OTHERS_PAYABLE", obj.GL_EMPLOYER_OTHERS_PAYABLE, True)
        clsCommon.AddColumnsForChange(coll, "Payment_Bank_Code", obj.Payment_Bank_Code, True)
        If Not obj.Payment_Date Is Nothing Then
            clsCommon.AddColumnsForChange(coll, "Payment_Date", clsCommon.GetPrintDate(obj.Payment_Date, "dd/MMM/yyyy"))
        End If


        If Not obj.CHEQUE_DATED Is Nothing Then
            clsCommon.AddColumnsForChange(coll, "CHEQUE_DATED", clsCommon.GetPrintDate(obj.CHEQUE_DATED, "dd/MMM/yyyy"))
        End If
        '' get LeaveAllotmentCode
        Dim qry As String = "select LEAVEALLOTMENT_CODE from TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE =  '" & obj.Code & "' "
        obj.LEAVEALLOTMENT_CODE = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))


        clsCommon.AddColumnsForChange(coll, "GL_SALARY_PAYABLE_AMOUNT", obj.GL_SALARY_PAYABLE_AMOUNT, True)
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "SALARY_GENERATION_CODE", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            qry = "SELECT Count(*) FROM TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE= '" & obj.Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERATE_SALARY", OMInsertOrUpdate.Insert, "", trans)
            Else
                Throw New Exception("This Code Is Already Exist")

            End If
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERATE_SALARY", OMInsertOrUpdate.Update, "SALARY_GENERATION_CODE='" & obj.Code & "'", trans)
        End If

        If isSaved Then
            Dim satQry As String = " "
            satQry = " Delete from TSPL_GENERATE_SALARY_ATTENDANCE where SALARY_GENERATION_CODE =  '" & obj.Code & "' "
            clsDBFuncationality.ExecuteNonQuery(satQry, trans)
            satQry = " Delete from TSPL_GENERATE_SALARY_FE_ACCOUNTS where SALARY_GENERATION_CODE =  '" & obj.Code & "' "
            clsDBFuncationality.ExecuteNonQuery(satQry, trans)
            satQry = "delete from TSPL_LEAVE_ALLOTMENTDETAIL where LVALLOTMENT_CODE='" & obj.LEAVEALLOTMENT_CODE & "'"
            clsDBFuncationality.ExecuteNonQuery(satQry, trans)

            satQry = ""
            satQry += " insert into  TSPL_GENERATE_SALARY_ATTENDANCE  (SALARY_GENERATION_CODE ,EMP_CODE,PAYPERIOD_DAYS ,PRESENT_DAYS ,ABSENT_DAYS ,LEAVE_DAYS ,HOLIDAY_DAYS,PAYABLE_DAYS_TYPE  ,PAYABLE_DAYS,LOP_DAYS , ACTUAL_BASIC ,PAYABLE_BASIC ,TOTAL_ALLOWANCE ,TOTAL_DEDUCTION ,NET_SALARY,SALARY_STRUCTURE_CODE,EMP_SAL_CODE,EMP_STATUS_CODE ) "
            satQry += "(select distinct '" & obj.Code & "' ,t1.EMP_CODE,t1.PAYPERIOD_DAYS ,t1.PRESENT_DAYS ,t1.ABSENT_DAYS ,t1.LEAVE_DAYS ,t1.HOLIDAY_DAYS,'' ,t1.PAYABLE_DAYS,t1.LOP_DAYS , coalesce(t2.ACTUAL_BASIC,0) as [Basic amount],t2.ACTUAL_BASIC,t2.TOTAL_EARNING,t2.TOTAL_DEDUCTION,t2.NET_SALARY,T1.SALARY_STRUCTURE_CODE,t1.EMP_SAL_CODE,t1.EMP_STATUS_CODE from TSPL_SALARY_CALCULATION t1"
            satQry += " left outer join (SELECT T1.EMP_CODE,SUM((CASE WHEN T1.SUB_HEAD_TYPE='BASIC' THEN T1.ACTUAL_AMOUNT ELSE 0 END )) AS ACTUAL_BASIC,SUM((CASE WHEN T2.ISEARNING=1 THEN T1.ACTUAL_AMOUNT ELSE 0 END)) AS TOTAL_EARNING,SUM((CASE WHEN T2.ISEARNING=0 THEN T1.ACTUAL_AMOUNT ELSE 0 END)) AS TOTAL_DEDUCTION,SUM(T1.ACTUAL_AMOUNT) AS NET_SALARY FROM TSPL_SALARY_CALCULATION T1 INNER JOIN TSPL_PAYHEAD_MASTER T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE GROUP BY T1.EMP_CODE  ) as t2 on t1.EMP_CODE = t2.EMP_CODE  ) "
            clsDBFuncationality.ExecuteNonQuery(satQry, trans)


            satQry = ""
            satQry += " Delete from TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE =  '" & obj.Code & "' "
            clsDBFuncationality.ExecuteNonQuery(satQry, trans)

            satQry = ""
            satQry += " insert into TSPL_GENERATE_SALARY_PAYHEADS(SALARY_GENERATION_CODE ,EMP_CODE,LINE_NO  ,PAY_HEAD_CODE ,HEAD_TYPE  ,SUB_HEAD_TYPE ,CALC_BASIS ,PAYHEAD_FORMULA ,RATE_AMOUNT ," &
                " ACTUAL_AMOUNT,PAYABLE_AMOUNT,FORMULA_AMOUNT,HEAD_VALUE,ISHIDDENCOMPONENT,PF_MAX_LM,ACCOUNT_CODE,EPF_RATE,ESI_RATE,CoEPF_RATE,CoEPF_RATE_AC01,CoEPF_AMT_AC01,CoEPS_RATE_AC10,CoEPS_AMT_AC10,ADMIN_RATE_AC02,ADMIN_AMT_AC02,EDLI_RATE_AC21," &
                " EDLI_AMT_AC21,ADMIN_EDLI_RATE_AC22,ADMIN_EDLI_AMT_AC22,OTHER_CHARGE,Co_ESI_RATE,Co_ESI_AMT,Employer_Account,Arrear_Amt,PRINCIPAL_ROUND_OFF,ARREAR_ROUND_OFF,CoEPF_AMT_AC01_ROUND_OFF,CoEPS_AMT_AC10_ROUND_OFF,PF_Applicable,PF_Calculation_Type,PF_Rule_Max_Lim,Custom_PF_Max_Lim,PF_No,ESI_Applicable,ESI_Calculation_Type,ESI_Rule_Max_Lim,Custom_ESI_Max_Lim,ESI_No,EPS_To_EPF,OT_Applicable,OT_CODE,OT_HOURS,OT_RATE,HOUR_MULTIPLIER,Bonus_Applicable,BONUS_CODE,BONUS_FROM_PAY_PERIOD_CODE,BONUS_TO_PAY_PERIOD_CODE,OD_Applicable,MAX_AMOUNT,PREV_ESI)"
            satQry += "(select '" & obj.Code & "', EMP_CODE,LINE_NO  ,PAY_HEAD_CODE ,HEAD_TYPE  ,SUB_HEAD_TYPE ,CALC_BASIS ,PAYHEAD_FORMULA ,RATE_AMOUNT , " &
                " ACTUAL_AMOUNT, (case when PAY_HEAD_CODE in ('EPF','GPF') then ACTUAL_AMOUNT When PAY_HEAD_CODE in ('ESI') then PAYABLE_AMOUNT else STD_AMOUNT end) as PAYABLE_AMOUNT,FORMULA_AMOUNT,HEAD_VALUE,ISHIDDENCOMPONENT,PF_MAX_LIM,ACCOUNT_CODE,EPF_RATE,ESI_RATE,CoEPF_RATE,CoEPF_RATE_AC01, " &
                " CoEPF_AMT_AC01,CoEPS_RATE_AC10,CoEPS_AMT_AC10,ADMIN_RATE_AC02,ADMIN_AMT_AC02,EDLI_RATE_AC21," &
                " EDLI_AMT_AC21,ADMIN_EDLI_RATE_AC22,ADMIN_EDLI_AMT_AC22,OTHER_CHARGE,Co_ESI_RATE,Co_ESI_AMT,Employer_Account,Arrear_Amt,PRINCIPAL_ROUND_OFF,ARREAR_ROUND_OFF,CoEPF_AMT_AC01_ROUND_OFF,CoEPS_AMT_AC10_ROUND_OFF,IS_PF_APPL,PF_Calculation_Type,PF_Rule_Max_Lim,Custom_PF_Max_Lim,PF_No,IS_ESI_APPL,ESI_Calculation_Type,ESI_Rule_Max_Lim,Custom_ESI_Max_Lim,ESI_No,EPS_To_EPF,IS_OT_APPL,OT_CODE,OT_HOURS,OT_RATE,HOUR_MULTIPLIER,IS_BONUS_APPL,BONUS_CODE,BONUS_FROM_PAY_PERIOD_CODE,BONUS_TO_PAY_PERIOD_CODE,OD_Applicable,MAX_AMOUNT,PREV_ESI  from TSPL_SALARY_CALCULATION) "
            clsDBFuncationality.ExecuteNonQuery(satQry, trans)

            satQry = " update TSPL_GENERATE_SALARY_ATTENDANCE set ARREAR_AMT=coalesce(Arrear.Arrear_Amt,0) from " &
                     " (select SALARY_GENERATION_CODE,EMP_CODE,sum(case when PHM.ISEARNING=1 then Arrear_Amt else -ARREAR_AMT end) as Arrear_Amt from TSPL_GENERATE_SALARY_PAYHEADS " &
                     " left join TSPL_PAYHEAD_MASTER PHM on TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=PHM.PAY_HEAD_CODE " &
                     " where SALARY_GENERATION_CODE='" & obj.Code & "' group by SALARY_GENERATION_CODE,EMP_CODE) as Arrear   " &
                     " where TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE = Arrear.SALARY_GENERATION_CODE  " &
                     " And TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE = Arrear.EMP_CODE " &
                     " and TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE='" & obj.Code & "'"
            clsDBFuncationality.ExecuteNonQuery(satQry, trans)
        End If
        '' Save Monthly Leave Allotment
        Dim objAllot As clsLeaveAllotment = SaveMonthlyAllotmentOfLeave(obj, trans)
        If clsCommon.myLen(objAllot.LVALLOTMENT_CODE) > 0 Then
            qry = "update TSPL_GENERATE_SALARY set LEAVEALLOTMENT_CODE='" & objAllot.LVALLOTMENT_CODE & "' where SALARY_GENERATION_CODE='" & obj.Code & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return isSaved
    End Function
    Public Shared Function SaveMonthlyAllotmentOfLeave(ByVal objSal As clsSalaryGeneration, ByVal trans As SqlTransaction) As clsLeaveAllotment
        '' emp qry
        Dim obj As New clsLeaveAllotment
        Dim AllotQry As String = ""
        Dim Qry As String = ""
        AllotQry = "select EMP.EMP_CODE,EMP.PAY_PERIOD_CODE,EMP.ACTUAL_BASIC,EMP.DATE_FROM,EMP.DATE_TO,EMP.PRESENT_DAYS,EMP.HOLIDAY_DAYS," &
                  Environment.NewLine & " EMP.DOJ,EMP.CONFIRMATION_DATE,EMP.PROBATION_END_DATE,LS.LEAVE_CODE,LS.LEAVE_ALLOT_TYPE," &
                  Environment.NewLine & " LS.ALLOT_AFTER_MONTHS,LS.ALLOT_AFTER_DAYS,LS.Allot_Periodicity,LS.Allot_Type,LS.Allot_Per_Month,LS.PerPresentDays," &
                  Environment.NewLine & " coalesce(round((CASE WHEN LS.Allot_Type='F' then LS.Allot_Per_Month WHEN LS.Allot_Type='Attn'  " &
                  Environment.NewLine & " then (EMP.PRESENT_DAYS/case when LS.PerPresentDays= 0 then 1 else LS.PerPresentDays end )*LS.Allot_Per_Month WHEN	LS.Allot_Type='Salary' then (select top 1 COALESCE((case when LS.Allot_Periodicity='Y' " &
                  Environment.NewLine & " then ALLOTED_LEAVE/12 else ALLOTED_LEAVE end ),0) AS  ALLOTED_LEAVE  from TSPL_LEAVE_SETTING_SALARY_SLAB_ALLOTED_LEAVE " &
                  Environment.NewLine & " where LEAVE_CODE=LS.LEAVE_CODE AND EMP.ACTUAL_BASIC BETWEEN SALARY_FROM AND SALARY_TO) END),2),0) AS Alloted_Leave from " &
                  Environment.NewLine & " ( select LEAVE_CODE,LEAVE_ALLOT_TYPE,ALLOT_AFTER_MONTHS,ALLOT_AFTER_DAYS,Allot_Periodicity, " &
                  Environment.NewLine & " Allot_Type,(case when Allot_Periodicity='Y' then  Alloted_Days/12 else Alloted_Days end) as Allot_Per_Month," &
                  Environment.NewLine & " (case when Allot_Periodicity='Y' then  PerPresentDays/12 else PerPresentDays end) as PerPresentDays " &
                  Environment.NewLine & " from TSPL_LEAVE_SETTING WHERE AutoAllotDuringSalaryGen='1') LS, " &
                  Environment.NewLine & " (select GSA.EMP_CODE,GS.PAY_PERIOD_CODE,GSA.ACTUAL_BASIC,PPM.DATE_FROM,PPM.DATE_TO,GSA.PRESENT_DAYS, " &
                  Environment.NewLine & " (coalesce(GSA.PAYABLE_DAYS,0)-coalesce(GSA.PRESENT_DAYS,0)-coalesce(GSA.LEAVE_DAYS,0)) as HOLIDAY_DAYS," &
                  Environment.NewLine & " CONVERT(DATE,EMP.Joining_date,103) AS DOJ,EMP.CONFIRMATION_DATE,EMP.PROBATION_END_DATE from TSPL_GENERATE_SALARY_ATTENDANCE GSA" &
                  Environment.NewLine & " INNER JOIN TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " &
                  Environment.NewLine & " LEFT JOIN TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " &
                  Environment.NewLine & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON GSA.EMP_CODE=EMP.EMP_CODE " &
                  Environment.NewLine & " where GS.SALARY_GENERATION_CODE='" & objSal.Code & "') EMP" &
                  Environment.NewLine & " WHERE (CASE WHEN LS.LEAVE_ALLOT_TYPE=1 THEN EMP.DOJ " &
                  Environment.NewLine & " WHEN LS.LEAVE_ALLOT_TYPE=2 THEN EMP.CONFIRMATION_DATE " &
                  Environment.NewLine & " WHEN LS.LEAVE_ALLOT_TYPE=3 THEN EMP.PROBATION_END_DATE " &
                  Environment.NewLine & " WHEN LS.LEAVE_ALLOT_TYPE=4 THEN DATEADD(DAY,LS.ALLOT_AFTER_DAYS,DateAdd(MONTH,LS.ALLOT_AFTER_MONTHS,EMP.DOJ))" &
                  Environment.NewLine & " END)<=EMP.DATE_TO"
        Qry = "select Count(*) as tot from (" & AllotQry & ") as Final"
        Dim tot As Integer = clsDBFuncationality.getSingleValue(Qry, trans)
        If tot > 0 Then
            ''If clsCommon.myLen(obj.LVALLOTMENT_CODE)
            obj.LVALLOTMENT_CODE = objSal.LEAVEALLOTMENT_CODE
            obj.ALLOTMENT_DATE = clsPayPeriodMaster.GetToDate(objSal.PAY_PERIOD_CODE, trans)
            obj.ALLOTMENT_REMARKS = "Automatic Allotment of Leave against Salary Generation-" & objSal.Code & " on date " & objSal.GENERATE_DATE & ""
            obj.Allotment_Type = "I"
            obj.Division_Code = objSal.DEVISION_CODE
            obj.Document_Type = "L"
            obj.EMP_CODE = ""
            obj.Location_Code = objSal.LOCATION_CODE
            obj.PAY_PERIOD_CODE = objSal.PAY_PERIOD_CODE
            obj.ObjList = Nothing
            obj.SaveData(obj, IIf(clsCommon.myLen(objSal.LEAVEALLOTMENT_CODE) > 0, False, True), trans)
            Qry = " insert into TSPL_LEAVE_ALLOTMENTDETAIL(LVALLOTMENT_CODE,EMP_CODE,LEAVE_CODE,ALLOTED_LEAVE,Created_By,Created_Date,Modified_By,Modified_Date)" &
                  " select '" & obj.LVALLOTMENT_CODE & "',EMP_CODE,LEAVE_CODE,ALLOTED_LEAVE,'" & objSal.GENERATED_BY & "','" & clsCommon.GetPrintDate(objSal.GENERATE_DATE, "dd/MMM/yyyy") & "','" & objSal.GENERATED_BY & "','" & clsCommon.GetPrintDate(objSal.GENERATE_DATE, "dd/MMM/yyyy") & "' from (" & AllotQry & ") as Final "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'obj = clsLeaveAllotment.GetData(obj.LVALLOTMENT_CODE, NavigatorType.Current, trans)
        End If
        Return obj
    End Function

    'To be Uncomment
    'Task No-TEC/19/07/19-000948
    Public Shared Sub sendSalarySlipToMail(ByVal docNo As String, ByVal strStartupPath As String)
        Try
            Dim arrTo As List(Of String) = Nothing
            Dim strQuery As String = "select TSPL_GENERATE_SALARY. SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE,TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE,TSPL_EMPLOYEE_MASTER.EMail_ID    from TSPL_GENERATE_SALARY  left outer join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY. SALARY_GENERATION_CODE left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE  where LEN(TSPL_EMPLOYEE_MASTER.EMail_ID)>5 and  TSPL_GENERATE_SALARY. SALARY_GENERATION_CODE='" & docNo & "'"
            Dim dtt As DataTable = clsDBFuncationality.GetDataTable(strQuery)

            If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
                For i As Integer = 0 To dtt.Rows.Count - 1
                    Dim header1 As String = "Salary Certificate for the month of " & dtt.Rows(0)("PAY_PERIOD_CODE") & ""
                    Dim header2 As String = ""


                    Dim Qry As String = ""
                    Qry = ""
                    Qry += "  SELECT '0' as onePagePrint,T1.EMP_CODE As [Code] ,T1.Emp_Name as [Name],T1.PF_NO as [PFNo], T1.ESI_NO  as [ESINo], "
                    Qry += " t1.FATHERS_NAME as [FathersName],'" & objCommonVar.CurrentCompanyName & "' as [CompanyName],  t2.Add1+Case When ISNULL(t2.Add2,'')='' Then ''  else ', '+t2.Add2+ Case When ISNULL(t2.Add3,'')='' Then '' Else ', '+t2.Add3+ Case When ISNULL(t2.Pincode,'')='' Then '' else '-'+CONVERT(varchar, t2.Pincode) End End End  as [CompanyAddress],Logo_Img,Location_Desc as LocationDesc,"
                    Qry += " T1.Location_Code as LocationCode, "
                    Qry += " TSPL_DESIGNATION_MASTER.Designation_id ,TSPL_DESIGNATION_MASTER.Designation_Desc ,TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE ,"
                    Qry += " TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME ,TSPL_BANK_MASTER.BANK_CODE ,t1.Bank_Name,T1.BANK_ACC_NO ,T1.PAN_NO as Emp_Pan_no,"
                    Qry += "  TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then '' else ', '+TSPL_LOCATION_MASTER.Add2+"
                    Qry += "  Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_STATE_MASTER.State_Name ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_STATE_MASTER.State_Name) End End End as Location_Address"
                    Qry += " ,t1.Joining_date ,t7.PAY_PERIOD_CODE,T6.PAYPERIOD_DAYS,HOLIDAY_DAYS,PAYABLE_DAYS,(T6.PAYPERIOD_DAYS-HOLIDAY_DAYS) as Working_days,T6.Present_Days as [Present Days],(T6.PAYPERIOD_DAYS-Present_Days-HOLIDAY_DAYS-LEAVE_DAYS-LOP_DAYS) as Weekly_off,TSPL_LOCATION_MASTER.PF_NO as Firm_PF_No,T1.PF_NO as [PFNo] from TSPL_EMPLOYEE_MASTER T1"
                    Qry += " left Outer join tspl_company_Master T2 on T2.Comp_Code =T1.Comp_Code  "
                    Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = T1.LOCATION_CODE "
                    Qry += " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code = TSPL_LOCATION_MASTER.State "
                    Qry += " left outer join TSPL_DESIGNATION_MASTER on TSPL_DESIGNATION_MASTER.Designation_id =t1.Designation "
                    Qry += " left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =t1.DEPARTMENT_CODE "
                    Qry += " left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =t1.BANK_CODE"
                    Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE T6 ON T6.EMP_CODE =T1.EMP_CODE "
                    Qry += " inner JOIN TSPL_GENERATE_SALARY T7 on T7.SALARY_GENERATION_CODE =T6.SALARY_GENERATION_CODE where T7.Pay_Period_Code='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' and T1.EMP_CODE ='" & dtt.Rows(i)("EMP_CODE") & "'"
                    Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)


                    If Hader_Info.Rows.Count <= 0 Then
                        common.clsCommon.MyMessageBoxShow("No Data Found")
                    Else


                        Dim dtFinal As DataTable = New DataTable
                        dtFinal.Columns.Add("onePagePrint", GetType(String))
                        dtFinal.Columns.Add("Code", GetType(String))
                        dtFinal.Columns.Add("Name", GetType(String))
                        dtFinal.Columns.Add("FathersName", GetType(String))
                        dtFinal.Columns.Add("CompanyName", GetType(String))
                        dtFinal.Columns.Add("CompanyAddress", GetType(String))
                        dtFinal.Columns.Add("PFNo", GetType(String))
                        dtFinal.Columns.Add("ESINo", GetType(String))
                        dtFinal.Columns.Add("Logo_Img", GetType(Byte()))
                        dtFinal.Columns.Add("LocationCode", GetType(String))
                        dtFinal.Columns.Add("LocationDesc", GetType(String))
                        dtFinal.Columns.Add("Firm_PF_No", GetType(String))
                        dtFinal.Columns.Add("Designation_id", GetType(String))
                        dtFinal.Columns.Add("Designation_Desc", GetType(String))
                        dtFinal.Columns.Add("DEPARTMENT_CODE", GetType(String))
                        dtFinal.Columns.Add("DEPARTMENT_NAME", GetType(String))
                        dtFinal.Columns.Add("BANK_CODE", GetType(String))
                        dtFinal.Columns.Add("Bank_Name", GetType(String))
                        dtFinal.Columns.Add("BANK_ACC_NO", GetType(String))

                        dtFinal.Columns.Add("Emp_Pan_no", GetType(String))
                        dtFinal.Columns.Add("Location_Address", GetType(String))
                        dtFinal.Columns.Add("Joining_date", GetType(String))
                        dtFinal.Columns.Add("PAY_PERIOD_CODE", GetType(String))
                        dtFinal.Columns.Add("ErPayHead_name", GetType(String))
                        dtFinal.Columns.Add("ErPayHead_Rate", GetType(Double))
                        dtFinal.Columns.Add("ErPayHead_amt", GetType(Double))
                        dtFinal.Columns.Add("ErArrHead_amt", GetType(Double))
                        dtFinal.Columns.Add("DuPayHead_name", GetType(String))
                        dtFinal.Columns.Add("DuPayHead_amt", GetType(Double))
                        dtFinal.Columns.Add("Leave_Code", GetType(String))
                        dtFinal.Columns.Add("AVAILED", GetType(String))
                        dtFinal.Columns.Add("PAYPERIOD_DAYS", GetType(String))
                        dtFinal.Columns.Add("HOLIDAY_DAYS", GetType(String))
                        dtFinal.Columns.Add("PAYABLE_DAYS", GetType(String))
                        dtFinal.Columns.Add("Working_days", GetType(String))
                        dtFinal.Columns.Add("Present Days", GetType(String))
                        dtFinal.Columns.Add("Weekly_off", GetType(String))
                        dtFinal.Columns.Add("EL_Bal", GetType(String))
                        dtFinal.Columns.Add("CL_Bal", GetType(String))
                        dtFinal.Columns.Add("SL_Bal", GetType(String))
                        dtFinal.Columns.Add("HEADER1", GetType(String))
                        dtFinal.Columns.Add("HEADER2", GetType(String))
                        dtFinal.AcceptChanges()

                        Dim DrFinal As DataRow = dtFinal.NewRow()
                        Dim DrDT As DataRow
                        Dim DrDT1 As DataRow
                        Dim DrDT3 As DataRow
                        For Each DrHead As DataRow In Hader_Info.Rows
                            Dim WD As Decimal = 0
                            Dim PD As Decimal = 0
                            Dim HD As Decimal = 0
                            Dim WF As Decimal = 0
                            WD = DrHead.Item("Present Days")
                            PD = DrHead.Item("PAYABLE_DAYS")
                            HD = DrHead.Item("HOLIDAY_DAYS")
                            WF = DrHead.Item("Weekly_off")

                            Qry = ""
                            Qry += "select LINE_NO,head.PAY_HEAD_CODE,head.PAY_HEAD_NAME,EMP_CODE,RATE_AMOUNT,ACTUAL_AMOUNT,Arrear_Amount from"
                            Qry += " (SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' "
                            Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' and t1.HEAD_TYPE <> 'Arrear'   )as head"
                            Qry += " Left Join (SELECT T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.ACTUAL_AMOUNT as  Arrear_Amount,ARREAR_TYPE  FROM TSPL_PAYHEAD_MASTER T1  INNER JOIN ( SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1  JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE WHERE(1 = 1) and T1.ISEARNING=1  AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "' "
                            Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' and  t1.HEAD_TYPE = 'Arrear'  )as detail on  detail.ARREAR_TYPE=head.PAY_HEAD_CODE"
                            Qry += "  ORDER BY EMP_CODE,LINE_NO"

                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)



                            Qry = ""
                            Qry += " SELECT T2.LINE_NO,T1.PAY_HEAD_CODE,T1.PAY_HEAD_NAME,T2.EMP_CODE,T2.RATE_AMOUNT,T2.ACTUAL_AMOUNT FROM TSPL_PAYHEAD_MASTER T1 "
                            Qry += " INNER JOIN ("
                            Qry += " SELECT T2.PAY_PERIOD_CODE,T1.* FROM TSPL_GENERATE_SALARY_PAYHEADS T1 "
                            Qry += " JOIN TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE) AS T2 ON T1.PAY_HEAD_CODE=T2.PAY_HEAD_CODE"
                            Qry += " WHERE(1 = 1)"
                            Qry += " and T1.ISEARNING=0 "
                            Qry += " AND T2.PAY_PERIOD_CODE='" + dtt.Rows(0)("PAY_PERIOD_CODE") + "'  "
                            Qry += " AND T2.EMP_CODE ='" + DrHead("Code") + "' "
                            Qry += " ORDER BY T2.EMP_CODE,T2.LINE_NO "
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)
                            Qry = ""
                            Qry += "select 'W.D' as Leave_Code," & WD & " as AVAILED,0 as Balance Union All select 'H.D' as Leave_Code," & HD & " as AVAILED,0 as Balance  Union All select 'W.F' as Leave_Code," & WF & " as AVAILED,0 as Balance Union All select Leave_Code,AVAILED,Balance from TSPL_FUN_LEAVE_STATUS ('" + dtt.Rows(0)("PAY_PERIOD_CODE") + "') where Emp_Code='" + DrHead("Code") + "'  Union All select 'P.D' as Leave_Code," & PD & " as AVAILED,0 as Balance"
                            Dim dt4 As DataTable = clsDBFuncationality.GetDataTable(Qry)

                            Dim Counter As Int16 = dt.Rows.Count
                            If dt1.Rows.Count > dt.Rows.Count Then
                                Counter = dt1.Rows.Count
                            End If

                            For ii As Int16 = 0 To Counter

                                If dt.Rows.Count > ii Then
                                    DrDT = dt.Rows(ii)
                                Else
                                    DrDT = dt.NewRow()
                                End If

                                If dt1.Rows.Count > ii Then
                                    DrDT1 = dt1.Rows(ii)
                                Else
                                    DrDT1 = dt1.NewRow()
                                End If
                                If dt4.Rows.Count > ii Then
                                    DrDT3 = dt4.Rows(ii)
                                Else
                                    DrDT3 = dt4.NewRow()
                                End If
                                DrFinal = dtFinal.NewRow()
                                header2 = "This is to certify that " & clsCommon.myCstr(DrHead("Name")) & " is in employment with us  " _
                                & " since " & clsCommon.GetPrintDate(DrHead("JOINING_DATE"), "dd/MMM/yyyy") & " and is in receipt of following monthly emoluments."

                                DrFinal.Item("onePagePrint") = clsCommon.myCstr(DrHead("onePagePrint"))
                                DrFinal.Item("Code") = clsCommon.myCstr(DrHead("Code"))
                                DrFinal.Item("Name") = clsCommon.myCstr(DrHead("Name"))
                                DrFinal.Item("FathersName") = clsCommon.myCstr(DrHead("FathersName"))
                                DrFinal.Item("CompanyName") = clsCommon.myCstr(DrHead("CompanyName"))
                                DrFinal.Item("CompanyAddress") = clsCommon.myCstr(DrHead("CompanyAddress"))
                                DrFinal.Item("LocationCode") = clsCommon.myCstr(DrHead("LocationCode"))
                                DrFinal.Item("LocationDesc") = clsCommon.myCstr(DrHead("LocationDesc"))
                                DrFinal.Item("Firm_PF_No") = clsCommon.myCstr(DrHead("Firm_PF_No"))
                                DrFinal.Item("PAYPERIOD_DAYS") = clsCommon.myCstr(DrHead("PAYPERIOD_DAYS"))
                                DrFinal.Item("HOLIDAY_DAYS") = clsCommon.myCstr(DrHead("HOLIDAY_DAYS"))
                                DrFinal.Item("PAYABLE_DAYS") = clsCommon.myCstr(DrHead("PAYABLE_DAYS"))
                                DrFinal.Item("Working_days") = clsCommon.myCstr(DrHead("Working_days"))
                                DrFinal.Item("Present Days") = clsCommon.myCstr(DrHead("Present Days"))
                                DrFinal.Item("Weekly_off") = clsCommon.myCstr(DrHead("Weekly_off"))

                                If clsCommon.myLen(DrHead("Logo_Img")) > 0 Then
                                    DrFinal.Item("Logo_Img") = DrHead("Logo_Img")


                                End If

                                DrFinal.Item("Designation_id") = clsCommon.myCstr(DrHead("Designation_id"))
                                DrFinal.Item("Designation_Desc") = clsCommon.myCstr(DrHead("Designation_Desc"))
                                DrFinal.Item("DEPARTMENT_CODE") = clsCommon.myCstr(DrHead("DEPARTMENT_CODE"))

                                DrFinal.Item("DEPARTMENT_NAME") = clsCommon.myCstr(DrHead("DEPARTMENT_NAME"))
                                DrFinal.Item("BANK_CODE") = clsCommon.myCstr(DrHead("BANK_CODE"))
                                DrFinal.Item("Bank_Name") = clsCommon.myCstr(DrHead("Bank_Name"))

                                DrFinal.Item("BANK_ACC_NO") = clsCommon.myCstr(DrHead("BANK_ACC_NO"))
                                DrFinal.Item("Emp_Pan_no") = clsCommon.myCstr(DrHead("Emp_Pan_no"))
                                DrFinal.Item("Location_Address") = clsCommon.myCstr(DrHead("Location_Address"))
                                DrFinal.Item("Joining_date") = clsCommon.myCstr(DrHead("Joining_date"))
                                DrFinal.Item("PFNo") = clsCommon.myCstr(DrHead("PFNo"))
                                DrFinal.Item("ESINo") = clsCommon.myCstr(DrHead("ESINo"))
                                DrFinal.Item("PAY_PERIOD_CODE") = clsCommon.myCstr(dtt.Rows(0)("PAY_PERIOD_CODE"))

                                ' For Each rowleave As DataRow In dt4.Rows
                                DrFinal.Item("Leave_Code") = clsCommon.myCstr(DrDT3("Leave_Code"))
                                DrFinal.Item("AVAILED") = clsCommon.myCstr(DrDT3("AVAILED"))

                                '' balance of leaves
                                DrFinal.Item("EL_Bal") = clsCommon.myCstr(DrDT3("Balance"))
                                DrFinal.Item("CL_Bal") = clsCommon.myCstr(DrDT3("Balance"))
                                DrFinal.Item("SL_Bal") = clsCommon.myCstr(DrDT3("Balance"))

                                DrFinal.Item("HEADER1") = header1
                                DrFinal.Item("HEADER2") = header2



                                If clsCommon.myLen(DrDT("PAY_HEAD_NAME")) > 0 Then
                                    DrFinal.Item("ErPayHead_name") = clsCommon.myCstr(DrDT("PAY_HEAD_NAME"))
                                    DrFinal.Item("ErPayHead_Rate") = clsCommon.myCdbl(DrDT("RATE_AMOUNT"))
                                    DrFinal.Item("ErPayHead_amt") = clsCommon.myCdbl(DrDT("ACTUAL_AMOUNT"))
                                    DrFinal.Item("ErArrHead_amt") = clsCommon.myCdbl(DrDT("Arrear_Amount"))
                                End If

                                If clsCommon.myLen(DrDT1("PAY_HEAD_NAME")) > 0 Then
                                    DrFinal.Item("DuPayHead_name") = clsCommon.myCstr(DrDT1("PAY_HEAD_NAME"))
                                    DrFinal.Item("DuPayHead_amt") = clsCommon.myCdbl(DrDT1("ACTUAL_AMOUNT"))
                                End If
                                dtFinal.Rows.Add(DrFinal)
                            Next
                        Next
                        dtFinal.AcceptChanges()
                        'Dim strTrgtFile As String = ("SalaryCertificate_" & dtt.Rows(0)("PAY_PERIOD_CODE") & "_" & dtt.Rows(0)("EMP_CODE")).ToString.Replace("/", "").Replace("\", "")
                        'If clsERPFuncationality.exportCrystalToPDF(dtFinal, strStartupPath & "\Crystal Reports\HR_Payroll", "crptKDILSalarySlip ForSingleEmployee", strTrgtFile, strStartupPath) Then
                        '    arrTo = New List(Of String)
                        '    arrTo.Add(dtt.Rows(0)("EMail_ID"))
                        'clsMailViaOutlook.SendEmail("Salary Certificate", "Salary Certificate For The Period- " & dtt.Rows(0)("PAY_PERIOD_CODE"), arrTo, Nothing, strStartupPath & "\pdfTemp\" & strTrgtFile & ".pdf")
                        'End If

                        Dim objEmailH As New clsEMailHead()
                        objEmailH.arrEMail = New List(Of String)()

                        If clsCommon.myLen(dtt.Rows(i)("EMail_ID")) > 0 Then
                            objEmailH.arrEMail.Add(dtt.Rows(i)("EMail_ID"))
                        End If
                        objEmailH.Email_Subject = "Salary Certificate"
                        objEmailH.Email_Text = "Salary Certificate For The Period- " & dtt.Rows(i)("PAY_PERIOD_CODE")
                        Dim strRptPath As String = ""
                        Dim frmCRV As New frmCrystalReportViewer()
                        strRptPath = frmCRV.funreport(True, CrystalReportFolder.HRPayroll, dtFinal, "crptKDILSalarySlip ForSingleEmployee", "Salary Certificate")
                        frmCRV = Nothing
                        objEmailH.Attachment_1_Path = strRptPath

                        objEmailH.SaveData(clsUserMgtCode.frmSalaryGeneration, objEmailH, Nothing)
                        objEmailH = Nothing
                    End If
                Next
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Shared Function PostData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            PostData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

        Return True
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
            Dim obj As clsSalaryGeneration = clsSalaryGeneration.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'If (obj.POSTED = 1) Then
            '    Throw New Exception("Already Post on :" + obj.Posting_Date)
            'End If
            If obj.CREATE_FE Then
                ''====================================================
                'Dim strEmpCode As String = ""
                'If obj.arrEMP IsNot Nothing Then
                '    If obj.arrEMP IsNot Nothing AndAlso obj.arrEMP.Count > 0 Then
                '        For i As Integer = 0 To obj.arrEMP.Count - 1
                '            If i = 0 Then
                '                strEmpCode = "'" + obj.arrEMP(i) + "'"
                '            Else
                '                strEmpCode = strEmpCode + "," + "'" + obj.arrEMP(i) + "'"
                '            End If
                '        Next
                '        Dim qry As String = "  Select distinct Emp_code from ( select MAX(tspl_Vendor_master.isemployee) AS isemployee,TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ,sum(DEDUCTION_AMOUNT ) as DEDUCTION_AMOUNT,max(tspl_Vendor_master.Vendor_code) Vendor_code,max(tspl_Vendor_master.Vendor_name) as Vendor_name,max(TSPL_PAYHEAD_MASTER.Account_Code) as DeductionAccount from TSPL_DEDUCTION  left outer join TSPL_DEDUCTION_DETAIL on TSPL_DEDUCTION.DEDUCTION_CODE =TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE left outer  join tspl_Vendor_master on TSPL_DEDUCTION_DETAIL.EMP_CODE=tspl_Vendor_master.EMP_CODE left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_DEDUCTION_DETAIL.PAY_HEAD_CODE  where TSPL_DEDUCTION.PAY_PERIOD_CODE ='" + obj.PAY_PERIOD_CODE + "' and TSPL_DEDUCTION_DETAIL.EMP_CODE in (" + strEmpCode + ")  and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE <>'TDS' group by TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ) XFinal where  (isemployee is null or len (isnull(isemployee,'')) <=0 ) "
                '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '            System.Diagnostics.Process.Start( "c:\ERPTempFolder\salgenlog.txt")
                '        End If

                '    End If
                'End If
                ''====================================================

                Dim CreateAPinvoiceofsalaryemployeewiseduringsalarygen As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CreateAPinvoiceofsalaryemployeewiseduringsalarygen, clsFixedParameterCode.CreateAPinvoiceofsalaryemployeewiseduringsalarygen, trans)) = 1, True, False))

                If CreateAPinvoiceofsalaryemployeewiseduringsalarygen = False Then
                    clsSalaryGeneration.FunCreateJournalEntryForSalary(obj, trans)
                End If


                If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.DoNotCreatePaymentWhileSalaryGeneration, clsFixedParameterCode.DoNotCreatePaymentWhileSalaryGeneration, trans)) = "0" Then
                    clsSalaryGeneration.PaymentEntry(obj, trans)
                End If
                If CheckIsEmployeeTypeAndVendorMapping(obj, trans) Then
                    clsSalaryGeneration.APInvoice_CreditNote(obj, trans)
                End If
                ''richa BHO/15/07/21-000044
                If CreateAPinvoiceofsalaryemployeewiseduringsalarygen = True Then
                    clsSalaryGeneration.Create_APInvoiceEntry(obj, trans)
                End If

            End If
            If AllowToPost(obj, trans) Then
                Dim qry As String = "Update TSPL_GENERATE_SALARY set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where SALARY_GENERATION_CODE = '" + strDocNo + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select POSTED from TSPL_GENERATE_SALARY where SALARY_GENERATION_CODE='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='PL-JE' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            Dim PaymentNo As String = clsDBFuncationality.getSingleValue("select Payment_No from TSPL_PAYMENT_HEADER where Payment_Type='MI' and Against_Salary_Generation_Code='" + strCode + "'", trans)
            If clsCommon.myLen(PaymentNo) > 0 Then
                ''richa agarwal 20 Jan,2020 against ticket no ERO/13/01/20-001174
                Dim VoucherNo1 As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No = (select Payment_No from TSPL_PAYMENT_HEADER where Payment_Type='MI' and Against_Salary_Generation_Code='" + strCode + "')", trans)
                If clsCommon.myLen(VoucherNo1) > 0 Then
                    Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo1 + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo1 + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                End If

                Qry = "delete from TSPL_PAYMENT_DETAIL where Payment_No ='" + PaymentNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_PAYMENT_HEADER where Payment_No ='" + PaymentNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            Dim SalaryFE As String = clsDBFuncationality.getSingleValue("select SALARY_GENERATION_CODE from TSPL_GENERATE_SALARY_FE_ACCOUNTS where SALARY_GENERATION_CODE='" + strCode + "'", trans)
            If clsCommon.myLen(SalaryFE) > 0 Then
                Qry = "delete from TSPL_GENERATE_SALARY_FE_ACCOUNTS where SALARY_GENERATION_CODE ='" + SalaryFE + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE ='" + SalaryFE + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_GENERATE_SALARY_ATTENDANCE where SALARY_GENERATION_CODE ='" + SalaryFE + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            ''richa agarwal 25 Nov,2019
            Dim ArInvoiceNo As String = clsDBFuncationality.getSingleValue("select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "'", trans)
            If clsCommon.myLen(ArInvoiceNo) > 0 Then

                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-CN' and Source_Doc_No in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "'))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AP-CN' and Source_Doc_No in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "'))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AP-DN' and Source_Doc_No in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='AP-IN' and Source_Doc_No in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "'))"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='AP-IN' and Source_Doc_No in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)


                Qry = "delete from  tspl_remittance  where document_no in  (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_VENDOR_INVOICE_detail  where document_no in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_VENDOR_INVOICE_HEAD where document_no in (select Document_No  from TSPL_VENDOR_INVOICE_HEAD where isnull(Against_Salary_Generation_Code ,'')='" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "Update TSPL_GENERATE_SALARY set POSTED = 0 where SALARY_GENERATION_CODE='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function AllowToPost(ByVal obj As clsSalaryGeneration, ByVal trans As SqlTransaction) As Boolean
        If obj.CREATE_FE Then
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'If (obj.POSTED = 0) Then
            '    Throw New Exception("Document " & obj.Code & " not posted")
            'End If
        End If


        If clsCommon.myLen(clsCommon.myCstr(obj.SAL_ACCOUNT_SET)) <= 0 Then
            Throw New Exception("Please Fill AccountSet Code")
        End If
        If clsCommon.myLen(clsCommon.myCstr(obj.GL_SALARY_PAYABLE)) <= 0 Then
            Throw New Exception("Salary Payable Account is empty !")
        End If
        If clsCommon.myLen(obj.GL_Employer_PF_PAYABLE) <= 0 Then
            Throw New Exception("Employer PF Payable Account is empty !")
        End If
        If clsCommon.myLen(obj.GL_Employer_ESI_PAYABLE) <= 0 Then
            Throw New Exception("Employer ESI Payable Account is empty !")
        End If

        If clsCommon.myLen(obj.GL_EMPLOYER_OTHERS_PAYABLE) <= 0 Then
            Throw New Exception("Employer Other Payable Account is empty !")
        End If

        Dim strLog As String = ""
        Dim logFile As String = "c:\ERPTempFolder\salgenlog.txt"
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, False)
            stream.WriteLine("")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        'Dim objWriter As New System.IO.StreamWriter(logFile, True)
        Dim strq As String

        strq = " SELECT * FROM ( " &
               " select TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE,(case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' " &
               " then TSPL_EMPLOYEE_MASTER.ADVANCE_TO_STAFF else TSPL_GENERATE_SALARY_PAYHEADS.Account_Code end) AS SalAccount_Code," &
               " (case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' " &
               " then TSPL_EMPLOYEE_MASTER.ADVANCE_TO_STAFF else TSPL_PAYHEAD_MASTER.Account_Code end) AS PHAccount_Code, " &
               " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)=1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Debit, " &
               " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)<>1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Credit, " &
               " (case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE " &
               " from TSPL_GENERATE_SALARY_PAYHEADS " &
               " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
               " INNER JOIN TSPL_EMPLOYEE_MASTER ON  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " &
               " left join TSPL_PAYHEAD_MASTER on TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " &
               " left join TSPL_GL_ACCOUNTS on TSPL_PAYHEAD_MASTER.Account_Code=TSPL_GL_ACCOUNTS.Account_Code " &
               " where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & obj.Code & "'  ) as final " &
               " where ((SalAccount_Code is null or PHAccount_Code is null) and EMP_CODE is null) or ((SalAccount_Code is null or PHAccount_Code is null) and EMP_CODE is not null and Credit>0)"
        Dim dtValid As DataTable
        dtValid = clsDBFuncationality.GetDataTable(strq, trans)
        If dtValid.Rows.Count > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Pay heads/ employee loan advance gl are not mapped to GL Accounts:")
            For Each drAC As DataRow In dtValid.Rows
                objWriter.WriteLine((dtValid.Rows.IndexOf(drAC) + 1) & ". " & drAC.Item("PAY_HEAD_CODE") & "-Emp Code:" & clsCommon.myCstr(drAC.Item("EMP_CODE")))
            Next
            objWriter.Close()
            Throw New Exception("Some Pay heads are not mapped to GL Accounts !")
        End If

        '' check for Employer GL Account
        strq = " SELECT * FROM ( " &
              " select TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE,TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account AS SalAccount_Code," &
              " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)=1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Debit, " &
              " ((case when isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0)<>1 then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else 0 end)) as Credit, " &
              " (case when TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE " &
              " from TSPL_GENERATE_SALARY_PAYHEADS " &
              " inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
              " INNER JOIN TSPL_EMPLOYEE_MASTER ON  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE " &
              " left join TSPL_PAYHEAD_MASTER on TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " &
              " left join TSPL_GL_ACCOUNTS on TSPL_PAYHEAD_MASTER.Account_Code=TSPL_GL_ACCOUNTS.Account_Code " &
              " where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" & obj.Code & "'  and TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE in ('EPF','EMPESI','LWF')) as final " &
              " where ((SalAccount_Code is null) and EMP_CODE is null) or ((SalAccount_Code is null) and EMP_CODE is not null and Credit>0)"
        Dim dtEmplValid As DataTable
        dtEmplValid = clsDBFuncationality.GetDataTable(strq, trans)
        If dtValid.Rows.Count > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Pay heads(Employer) are not mapped to GL Accounts:")
            For Each drAC As DataRow In dtValid.Rows
                objWriter.WriteLine((dtValid.Rows.IndexOf(drAC) + 1) & ". " & drAC.Item("PAY_HEAD_CODE") & "-Emp Code:" & clsCommon.myCstr(drAC.Item("EMP_CODE")))
            Next
            objWriter.Close()
            Throw New Exception("Some Pay head employer account are not mapped !")
        End If

        '' location wise account code check
        Dim LocSeg As String
        Dim LocSep As String
        Dim FinalQry As String
        'LocSep = obj.GL_Employer_ESI_PAYABLE.Substring(obj.GL_Employer_ESI_PAYABLE.Length - 4, 1)
        'If clsCommon.CompairString(LocSep, "-") = CompairStringResult.Equal Then
        '    LocSeg = obj.GL_Employer_ESI_PAYABLE.Substring(obj.GL_Employer_ESI_PAYABLE.Length - 3, 3)
        'Else
        '    LocSeg = ""
        'End If
        Dim QryLoc As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" & obj.LOCATION_CODE & "'"
        LocSep = "-"
        LocSeg = clsDBFuncationality.getSingleValue(QryLoc, trans)
        If clsCommon.myLen(LocSeg) <= 0 Then
            strq = " SELECT distinct (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'') " &
                   " ELSE Account_Code+ '' END) AS Account_Code,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else null end ) as ACTUAL_AMOUNT  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & obj.Code & "'"
        Else
            strq = " SELECT distinct (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'-" & LocSeg & "') " &
                   " ELSE Account_Code+ '-" & LocSeg & "' END) AS Account_Code,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE else null end ) as EMP_CODE,(case when TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='Loan' then TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT else null end ) as ACTUAL_AMOUNT  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & obj.Code & "'"
        End If
        FinalQry = "select NewACC.Account_Code,TSPL_GL_ACCOUNTS.Account_Code as OldAccCode from (" & strq & ") as NewACC left join TSPL_GL_ACCOUNTS on NewACC.Account_Code=TSPL_GL_ACCOUNTS.Account_Code  where (TSPL_GL_ACCOUNTS.Account_Code is null and EMP_CODE is null) or (EMP_CODE is not null and ACTUAL_AMOUNT>0)"
        Dim dtACCode As DataTable
        dtACCode = clsDBFuncationality.GetDataTable(FinalQry, trans)

        If dtACCode.Rows.Count > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Account Code that does not exist in GL account:")
            For Each drAC As DataRow In dtACCode.Rows
                objWriter.WriteLine((dtACCode.Rows.IndexOf(drAC) + 1) & ". " & drAC.Item("Account_Code"))
            Next
            objWriter.Close()
            Throw New Exception("Some Account Code that does not exist in GL account !")
        End If

        Return True
    End Function

    Public Shared Function CheckIsEmployeeTypeAndVendorMapping(ByVal obj As clsSalaryGeneration, ByVal trans As SqlTransaction) As Boolean
        If obj.CREATE_FE Then
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
        End If
        Dim strLog As String = ""
        Dim logFile As String = "c:\ERPTempFolder\salgenlog.txt"
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, False)
            stream.WriteLine("")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        '===========================================
        Dim qryDedection As String = "  Select distinct Emp_code from ( " &
                            " select MAX(tspl_Vendor_master.isemployee) AS isemployee,TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ,sum(DEDUCTION_AMOUNT ) as DEDUCTION_AMOUNT,max(tspl_Vendor_master.Vendor_code) Vendor_code,max(tspl_Vendor_master.Vendor_name) as Vendor_name,max(TSPL_PAYHEAD_MASTER.Account_Code) as DeductionAccount from TSPL_DEDUCTION  left outer join TSPL_DEDUCTION_DETAIL on TSPL_DEDUCTION.DEDUCTION_CODE =TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE left outer  join tspl_Vendor_master on TSPL_DEDUCTION_DETAIL.EMP_CODE=tspl_Vendor_master.EMP_CODE left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_DEDUCTION_DETAIL.PAY_HEAD_CODE  where TSPL_DEDUCTION.PAY_PERIOD_CODE in (Select distinct TSPL_GENERATE_SALARY.PAY_PERIOD_CODE from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" + obj.Code + "') and TSPL_DEDUCTION_DETAIL.EMP_CODE in (select distinct EMP_CODE from TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" + obj.Code + "')  and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE <>'TDS' group by TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE " &
                            " ) XFinal where  (isemployee is null or len (isnull(isemployee,'')) <=0 )  "
        Dim dtDedection As DataTable
        dtDedection = clsDBFuncationality.GetDataTable(qryDedection, trans)
        If dtDedection IsNot Nothing AndAlso dtDedection.Rows.Count > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Vendor should be of Employee type :")
            For Each drDedc As DataRow In dtDedection.Rows
                objWriter.WriteLine(drDedc.Item("Emp_code"))
            Next
            objWriter.Close()
            Throw New Exception("Some Vendor should be of Employee type!")
        End If

        '*******************************************
        Dim qryDV As String = "  Select distinct Emp_code from ( " &
                            " select MAX(tspl_Vendor_master.isemployee) AS isemployee,TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ,sum(DEDUCTION_AMOUNT ) as DEDUCTION_AMOUNT,max(tspl_Vendor_master.Vendor_code) Vendor_code,max(tspl_Vendor_master.Vendor_name) as Vendor_name,max(TSPL_PAYHEAD_MASTER.Account_Code) as DeductionAccount from TSPL_DEDUCTION  left outer join TSPL_DEDUCTION_DETAIL on TSPL_DEDUCTION.DEDUCTION_CODE =TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE left outer  join tspl_Vendor_master on TSPL_DEDUCTION_DETAIL.EMP_CODE=tspl_Vendor_master.EMP_CODE left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_DEDUCTION_DETAIL.PAY_HEAD_CODE  where TSPL_DEDUCTION.PAY_PERIOD_CODE in (Select distinct TSPL_GENERATE_SALARY.PAY_PERIOD_CODE from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE='" + obj.Code + "') and TSPL_DEDUCTION_DETAIL.EMP_CODE in (select distinct EMP_CODE from TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" + obj.Code + "')  and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE <>'TDS' group by TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE " &
                            " ) XFinal where  isemployee =1 and (vendor_code is null or  len (isnull(vendor_Code,''))  <=0)  "
        Dim dtDV As DataTable
        dtDV = clsDBFuncationality.GetDataTable(qryDV, trans)
        If dtDV IsNot Nothing AndAlso dtDV.Rows.Count > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Please map Vendor for Employee:")
            For Each drDV As DataRow In dtDV.Rows
                objWriter.WriteLine(drDV.Item("Emp_code"))
            Next
            objWriter.Close()
            Throw New Exception("Map some Vendor for Employee!")
        End If
        '===========================================
        Return True
    End Function

    Public Shared Function updateGLAccInGenSalary(ByVal obj As clsSalaryGeneration, ByVal trans As SqlTransaction) As Boolean
        '' location wise account code check

        If obj.CREATE_FE = False Then
            Return True
        End If


        Dim logFile As String = "c:\ERPTempFolder\salgenlog.txt"
        If System.IO.File.Exists(logFile) Then
            'Dim stream As New IO.StreamWriter(logFile, False)
            'stream.WriteLine("")
            'stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If

        Dim LocSeg As String
        Dim LocSep As String
        Dim strq As String
        Dim FinalQry As String
        'LocSep = txtBankGLAccount.Text.Substring(txtBankGLAccount.Text.Length - 4, 1)
        'If clsCommon.CompairString(LocSep, "-") = CompairStringResult.Equal Then
        '    LocSeg = txtBankGLAccount.Text.Substring(txtBankGLAccount.Text.Length - 3, 3)
        'Else
        '    LocSeg = ""
        'End If
        Dim QryLoc As String = "select Loc_Segment_Code from TSPL_LOCATION_MASTER where Location_Code='" & obj.LOCATION_CODE & "'"
        LocSep = "-"
        LocSeg = clsDBFuncationality.getSingleValue(QryLoc, trans)
        If clsCommon.myLen(LocSeg) <= 0 Then
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'') " &
                   " ELSE Account_Code+ '' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & obj.Code & "'"
        Else
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Account_Code,4),1,1)='-' THEN REPLACE(Account_Code,RIGHT(Account_Code,4),'-" & LocSeg & "') " &
                   " ELSE Account_Code+ '-" & LocSeg & "' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & obj.Code & "'"
        End If
        Dim qryCheck As String = "select TSPL_GL_ACCOUNTS.Account_Code,gla_new.New_Account_Code from TSPL_GL_ACCOUNTS right outer join (select distinct New_Account_Code from (" & strq & ") GLA) gla_new on TSPL_GL_ACCOUNTS.Account_Code=gla_new.New_Account_Code " &
            " where gla_new.New_Account_Code is not null and TSPL_GL_ACCOUNTS.Account_Code is null"

        Dim dtCGlNA As DataTable = clsDBFuncationality.GetDataTable(qryCheck, trans)
        If dtCGlNA.Rows.Count > 0 Then

            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Accounts that does not exists for current location:")
            For Each drGL As DataRow In dtCGlNA.Rows
                objWriter.WriteLine((dtCGlNA.Rows.IndexOf(drGL) + 1) & ". " & clsCommon.myCstr(drGL.Item("New_Account_Code")))
            Next
            objWriter.Close()
            Throw New Exception("Some General Accounts does not exists for current location !")
        End If

        FinalQry = "update TSPL_GENERATE_SALARY_PAYHEADS set Account_Code=newAcc.New_Account_Code from (" & strq & ") as NewACC where newAcc.Account_Code=TSPL_GENERATE_SALARY_PAYHEADS.Account_Code and SALARY_GENERATION_CODE='" & obj.Code & "'"
        Dim isSaved As Boolean
        isSaved = clsDBFuncationality.ExecuteNonQuery(FinalQry, trans)

        '' update employer account
        If clsCommon.myLen(LocSeg) <= 0 Then
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Employer_Account,4),1,1)='-' THEN REPLACE(Employer_Account,RIGHT(Employer_Account,4),'') " &
                   " ELSE Employer_Account+ '' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account as Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & obj.Code & "' and TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type in ('EPF','EMPESI','LWF')"
        Else
            strq = " SELECT  (CASE WHEN SUBSTRING(RIGHT(Employer_Account,4),1,1)='-' THEN REPLACE(Employer_Account,RIGHT(Employer_Account,4),'-" & LocSeg & "') " &
                   " ELSE Employer_Account+ '-" & LocSeg & "' END) AS New_Account_Code,TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account as Account_Code  FROM TSPL_GENERATE_SALARY_PAYHEADS where SALARY_GENERATION_CODE='" & obj.Code & "' and TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type in ('EPF','EMPESI','LWF')"
        End If
        Dim qryCheckEmpl As String = "select TSPL_GL_ACCOUNTS.Account_Code,gla_new.New_Account_Code from TSPL_GL_ACCOUNTS right outer join (select distinct New_Account_Code from (" & strq & ") GLA) gla_new on TSPL_GL_ACCOUNTS.Account_Code=gla_new.New_Account_Code " &
            " where gla_new.New_Account_Code is not null and TSPL_GL_ACCOUNTS.Account_Code is null"

        Dim dtCGlEmplNA As DataTable = clsDBFuncationality.GetDataTable(qryCheck, trans)
        If dtCGlNA.Rows.Count > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Accounts that does not exists for current location:")
            For Each drGL As DataRow In dtCGlNA.Rows
                objWriter.WriteLine((dtCGlNA.Rows.IndexOf(drGL) + 1) & ". " & clsCommon.myCstr(drGL.Item("New_Account_Code")))
            Next
            objWriter.Close()
            Throw New Exception("Some General Accounts does not exists for current location !")
        End If

        FinalQry = "update TSPL_GENERATE_SALARY_PAYHEADS set Employer_Account=newAcc.New_Account_Code from (" & strq & ") as NewACC where newAcc.Account_Code=TSPL_GENERATE_SALARY_PAYHEADS.Employer_Account and SALARY_GENERATION_CODE='" & obj.Code & "' and TSPL_GENERATE_SALARY_PAYHEADS.Sub_Head_Type in ('EPF','EMPESI','LWF')"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(FinalQry, trans)

        FinalQry = "update TSPL_GENERATE_SALARY set CREATE_FE=1 where SALARY_GENERATION_CODE='" & obj.Code & "' "
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(FinalQry, trans)
        Return isSaved
    End Function
    Public Shared Function FunCreateJournalEntryForSalary(ByVal obj As clsSalaryGeneration, ByVal trans As SqlTransaction) As Boolean
        Dim arrmis As New ArrayList()

        Dim sourceType As String = "PL-JE"
        Dim sourceDesc As String = "Payroll Journal Entry"
        Dim strVoucherNoifExists As String = ""
        For Each objTr As clsSalaryFEAccounts In obj.arrAccGL
            If clsCommon.myLen(clsCommon.myCstr(objTr.AccountCode)) <= 0 Then
                Continue For
            End If
            Dim strAccountLocation As String = obj.LOCATION_CODE
            Dim dblAmount As Double = objTr.DEBIT - objTr.CREDIT
            If clsCommon.myCdbl(objTr.DEBIT) > 0 Then
                Dim acc3() As String = {objTr.AccountCode, objTr.DEBIT, objTr.Description}
                arrmis.Add(acc3)
            Else
                Dim Acc4() As String = {objTr.AccountCode, -1 * objTr.CREDIT, objTr.Description}
                arrmis.Add(Acc4)
            End If
        Next

        'Dim SalaryPayment_Date As Date = clsPayPeriodMaster.GetToDate(obj.PAY_PERIOD_CODE, trans)
        Dim paymentDesc As String = "Payment of salary for the month of " & obj.PAY_PERIOD_CODE & ""
        Return clsJournalMaster.FunGrnlEntryWithTrans(obj.LOCATION_CODE, False, strVoucherNoifExists, trans, obj.GENERATE_DATE, paymentDesc, sourceType, sourceDesc, obj.Code, obj.GENERATE_REMARKS, "", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, arrmis, , obj.GENERATE_REMARKS, obj.GENERATE_REMARKS)

    End Function
    Shared Function Create_APInvoiceEntry(ByVal objSG As clsSalaryGeneration, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim strQry As String = String.Empty
            Dim dblSalaryAmount As Double = 0
            If objSG.arrEMP IsNot Nothing Then
                If objSG.arrEMP IsNot Nothing AndAlso objSG.arrEMP.Count > 0 Then
                    For i As Integer = 0 To objSG.arrEMP.Count - 1
                        '''''''''''''''''''''''''''''''''' For Making AR Invoice ''''''''''''''''''''''''''''''''''
                        '' Dim strOuterQry As String = "[" + " Payment_Mode in (" & clsCommon.GetMulcallString(txtPaymentModeMulti.arrValueMember) & ")" + "]"

                        Dim arrPayPeriodCode As New ArrayList
                        arrPayPeriodCode.Add(objSG.PAY_PERIOD_CODE)
                        Dim arrLocationCode As New ArrayList
                        arrLocationCode.Add(objSG.LOCATION_CODE)
                        ''  Dim dt As DataTable = clsSalaryGeneration.GetSalaryReportData(arrPayPeriodCode, arrLocationCode, Nothing, "", False)
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select tspl_Vendor_master.isemployee,tspl_Vendor_master.Vendor_code,tspl_Vendor_master.Vendor_name from tspl_Vendor_master where tspl_Vendor_master.EMP_CODE='" & objSG.arrEMP(i) & "'", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isemployee")), "1") = CompairStringResult.Equal Then

                                strQry = GetSalaryReportBaseQry(arrPayPeriodCode, arrLocationCode, Nothing, "''", False, trans)
                                strQry = "select z.[Net Salary] from ( " & strQry & ") z where  z.emp_code='" & objSG.arrEMP(i) & "' and z.Salary_generation_code='" & objSG.Code & "' "
                                dblSalaryAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry, trans))

                                Dim ARNote As String = String.Empty

                                Dim objVendInv As New clsVedorInvoiceHead()
                                ''objCustInv.Document_No ''Will be Generateed
                                objVendInv.Invoice_Entry_Date = objSG.GENERATE_DATE 'clsPayPeriodMaster.GetToDate(objSG.PAY_PERIOD_CODE, trans)

                                objVendInv.Document_Type = "I"
                                ARNote = "AP Invoice Note"

                                objVendInv.Against_Salary_Generation_Code = objSG.Code
                                ''richa BHO/15/07/21-000045
                                objVendInv.loc_code = clsLocation.GetSegmentCode(objSG.LOCATION_CODE, trans)
                                objVendInv.Document_Total = clsCommon.myCdbl(dblSalaryAmount)
                                objVendInv.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_code"))
                                If (clsCommon.myLen(objVendInv.Vendor_Code) < 0) Then
                                    Throw New Exception("Please map Vendor for Employee: " + objSG.arrEMP(i))
                                End If
                                objVendInv.Invoice_Type = "AP"
                                objVendInv.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_name"))
                                objVendInv.Posting_Date = objVendInv.Invoice_Entry_Date
                                objVendInv.Vendor_Invoice_Date = objVendInv.Invoice_Entry_Date
                                objVendInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Vendor_Account,'') AS [Vendor_Account] FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + objVendInv.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendInv.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor account set for vendor : " + objVendInv.Vendor_Code)
                                End If

                                objVendInv.Vendor_Control_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payable_account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans))
                                objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, objVendInv.loc_code, True, trans)

                                If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If

                                objVendInv.On_Hold = 0
                                objVendInv.Remarks = "AP Invoice Entry for Salary Generation " & objSG.Code & " for Pay Period of  " & objSG.PAY_PERIOD_CODE & " against Employee " & objSG.arrEMP(i) & ""
                                objVendInv.Description = "AP Invoice Entry for Salary Generation " & objSG.Code & " for Pay Period of  " & objSG.PAY_PERIOD_CODE & " against Employee " & objSG.arrEMP(i) & ""
                                objVendInv.Balance_Amt = clsCommon.myCdbl(dblSalaryAmount)
                                objVendInv.Amount_Less_Discount = clsCommon.myCdbl(dblSalaryAmount)
                                objVendInv.Discount_Base = clsCommon.myCdbl(dblSalaryAmount)
                                '=========================================================

                                objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

                                '' Detail Level Saving

                                Dim VendAccSet As String = String.Empty


                                Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()

                                objVendInvTR.Detail_Line_No = 1
                                objVendInvTR.Amount = clsCommon.myCdbl(dblSalaryAmount)
                                objVendInvTR.Amount_less_Discount = clsCommon.myCdbl(dblSalaryAmount)
                                objVendInvTR.Total_Amount = clsCommon.myCdbl(dblSalaryAmount)
                                '' objVendInvTR.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_SALARY_PAYABLE from TSPL_PAYROLL_ACCOUNTSETS where ACCOUNT_SET_CODE ='" + objSG.SAL_ACCOUNT_SET + "'", trans))
                                objVendInvTR.GL_Account_Code = objSG.SALARY_PAYABLE_ACC
                                objVendInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInvTR.GL_Account_Code, objVendInv.loc_code, True, trans)
                                objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & objVendInvTR.GL_Account_Code & "'", trans))
                                objVendInv.Arr.Add(objVendInvTR)

                                objVendInv.SaveData(objVendInv, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
                            Else
                                Throw New Exception("Vendor should be of Employee type.")
                            End If
                        End If

                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function


    ''richa agarwal 25 Nov,2019 ERO/19/11/19-001120
    Shared Function APInvoice_CreditNote(ByVal objSG As clsSalaryGeneration, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            If objSG.arrEMP IsNot Nothing Then
                If objSG.arrEMP IsNot Nothing AndAlso objSG.arrEMP.Count > 0 Then
                    For i As Integer = 0 To objSG.arrEMP.Count - 1
                        '''''''''''''''''''''''''''''''''' For Making AR Invoice ''''''''''''''''''''''''''''''''''
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select MAX(tspl_Vendor_master.isemployee) AS isemployee,TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ,sum(DEDUCTION_AMOUNT ) as DEDUCTION_AMOUNT,max(tspl_Vendor_master.Vendor_code) Vendor_code,max(tspl_Vendor_master.Vendor_name) as Vendor_name,max(TSPL_PAYHEAD_MASTER.Account_Code) as DeductionAccount from TSPL_DEDUCTION  left outer join TSPL_DEDUCTION_DETAIL on TSPL_DEDUCTION.DEDUCTION_CODE =TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE left outer  join tspl_Vendor_master on TSPL_DEDUCTION_DETAIL.EMP_CODE=tspl_Vendor_master.EMP_CODE left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_DEDUCTION_DETAIL.PAY_HEAD_CODE  where TSPL_DEDUCTION.PAY_PERIOD_CODE ='" & objSG.PAY_PERIOD_CODE & "' and TSPL_DEDUCTION_DETAIL.EMP_CODE='" & objSG.arrEMP(i) & "'  and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE <>'TDS' and  TSPL_PAYHEAD_MASTER.IsCreateAPInvoice=1 group by TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isemployee")), "1") = CompairStringResult.Equal Then

                                Dim ARNote As String = String.Empty

                                Dim objVendInv As New clsVedorInvoiceHead()
                                ''objCustInv.Document_No ''Will be Generateed
                                objVendInv.Invoice_Entry_Date = objSG.GENERATE_DATE 'clsPayPeriodMaster.GetToDate(objSG.PAY_PERIOD_CODE, trans)

                                objVendInv.Document_Type = "C"
                                ARNote = "Credit Note"

                                objVendInv.Against_Salary_Generation_Code = objSG.Code
                                objVendInv.loc_code = clsLocation.GetSegmentCode(objSG.LOCATION_CODE, trans)
                                objVendInv.Document_Total = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                                objVendInv.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_code"))
                                If (clsCommon.myLen(objVendInv.Vendor_Code) < 0) Then
                                    Throw New Exception("Please map Vendor for Employee: " + objSG.arrEMP(i))
                                End If
                                objVendInv.Invoice_Type = "AP"
                                objVendInv.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_name"))
                                objVendInv.Posting_Date = objVendInv.Invoice_Entry_Date
                                objVendInv.Vendor_Invoice_Date = objVendInv.Invoice_Entry_Date
                                objVendInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Vendor_Account,'') AS [Vendor_Account] FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + objVendInv.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendInv.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor account set for vendor : " + objVendInv.Vendor_Code)
                                End If

                                objVendInv.Vendor_Control_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Advance_Against_Salary from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans))
                                objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, objVendInv.loc_code, True, trans)

                                If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If

                                objVendInv.On_Hold = 0
                                objVendInv.Remarks = "Deduction for Salary Generation " & objSG.Code & " for Pay Period of  " & objSG.PAY_PERIOD_CODE & " against Employee " & objSG.arrEMP(i) & ""
                                objVendInv.Description = "Deduction for Salary Generation " & objSG.Code & " for Pay Period of  " & objSG.PAY_PERIOD_CODE & " against Employee " & objSG.arrEMP(i) & ""
                                objVendInv.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                                objVendInv.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                                objVendInv.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                                '=========================================================

                                objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

                                '' Detail Level Saving

                                Dim VendAccSet As String = String.Empty


                                Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()

                                objVendInvTR.Detail_Line_No = 1
                                objVendInvTR.Amount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                                objVendInvTR.Amount_less_Discount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                                objVendInvTR.Total_Amount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                                objVendInvTR.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GL_SALARY_PAYABLE from TSPL_PAYROLL_ACCOUNTSETS where ACCOUNT_SET_CODE ='" + objSG.SAL_ACCOUNT_SET + "'", trans))
                                objVendInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInvTR.GL_Account_Code, objVendInv.loc_code, True, trans)
                                objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & objVendInvTR.GL_Account_Code & "'", trans))
                                objVendInv.Arr.Add(objVendInvTR)

                                objVendInv.SaveData(objVendInv, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
                            Else
                                Throw New Exception("Vendor should be of Employee type.")
                            End If
                        End If

                        ''Ap Invoice Credit note for TDS Type COMMENT TDS SECTION AS DISCUSSED IT WITH RANJANA MAM
                        'dt = clsDBFuncationality.GetDataTable("select MAX(TSPL_TDS_DEDUCTION_HEAD.Description) as deductionDes,MAX(TSPL_TDS_DEDUCTION_HEAD.TDS_Section) as TDS_Section,MAX(TSPL_TDS_DEDUCTION_HEAD.Gl_Account) AS Gl_Account, MAX(tspl_Vendor_master.Deduction_Code) AS Deduction_Code,MAX(tspl_Vendor_master.Is_TDS_Applicable) AS Is_TDS_Applicable,MAX(tspl_Vendor_master.isemployee) AS isemployee,TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ,sum(DEDUCTION_AMOUNT ) as DEDUCTION_AMOUNT,max(tspl_Vendor_master.Vendor_code) Vendor_code,max(tspl_Vendor_master.Vendor_name) as Vendor_name from TSPL_DEDUCTION  left outer join TSPL_DEDUCTION_DETAIL on TSPL_DEDUCTION.DEDUCTION_CODE =TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE left outer  join tspl_Vendor_master on TSPL_DEDUCTION_DETAIL.EMP_CODE=tspl_Vendor_master.EMP_CODE left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_DEDUCTION_DETAIL.PAY_HEAD_CODE LEFT OUTER JOIN TSPL_TDS_DEDUCTION_HEAD ON TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=tspl_vendor_master.Deduction_Code where TSPL_DEDUCTION.PAY_PERIOD_CODE ='" & objSG.PAY_PERIOD_CODE & "' and TSPL_DEDUCTION_DETAIL.EMP_CODE='" & objSG.arrEMP(i) & "'  and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE ='TDS' group by TSPL_DEDUCTION_DETAIL.EMP_CODE ,TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE ", trans)
                        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        '    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("isemployee")), "0") = CompairStringResult.Equal Then
                        '        Throw New Exception("Vendor should be of Employee type.")
                        '    End If
                        '    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Is_TDS_Applicable")), "0") = CompairStringResult.Equal Then
                        '        Throw New Exception("Vendor should be of TDS type.")
                        '    End If

                        '    Dim ARNote As String = String.Empty

                        '    Dim objVendInv As New clsVedorInvoiceHead()
                        '    ''objCustInv.Document_No ''Will be Generateed
                        '    objVendInv.Invoice_Entry_Date = clsPayPeriodMaster.GetToDate(objSG.PAY_PERIOD_CODE, trans)
                        '    objVendInv.Due_Date = clsPayPeriodMaster.GetToDate(objSG.PAY_PERIOD_CODE, trans)
                        '    objVendInv.Document_Type = "D"
                        '    ARNote = "Debit Note"
                        '    objVendInv.is_For_TDS = 1
                        '    objVendInv.Against_Salary_Generation_Code = objSG.Code
                        '    objVendInv.loc_code = objSG.LOCATION_CODE
                        '    objVendInv.Document_Total = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objVendInv.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_code"))
                        '    If (clsCommon.myLen(objVendInv.Vendor_Code) < 0) Then
                        '        Throw New Exception("Please map Vendor for Employee: " + objSG.arrEMP(i))
                        '    End If
                        '    objVendInv.Invoice_Type = "AP"
                        '    objVendInv.Vendor_Name = clsCommon.myCstr(dt.Rows(0)("Vendor_name"))
                        '    objVendInv.Posting_Date = objVendInv.Invoice_Entry_Date
                        '    objVendInv.Vendor_Invoice_Date = objVendInv.Invoice_Entry_Date
                        '    objVendInv.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT  ISNULL(Vendor_Account,'') AS [Vendor_Account] FROM TSPL_VENDOR_MASTER WHERE Vendor_Code ='" + objVendInv.Vendor_Code + "'", trans))
                        '    If (clsCommon.myLen(objVendInv.Account_Set) < 0) Then
                        '        Throw New Exception("Please set the vendor account set for vendor : " + objVendInv.Vendor_Code)
                        '    End If

                        '    objVendInv.Vendor_Control_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Payable_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendInv.Account_Set + "'", trans))
                        '    objVendInv.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInv.Vendor_Control_AC, objVendInv.loc_code, True, trans)

                        '    If clsCommon.myLen(objVendInv.Vendor_Control_AC) <= 0 Then
                        '        Throw New Exception("Please set the vendor payable Account")
                        '    End If

                        '    objVendInv.On_Hold = 0
                        '    objVendInv.Remarks = "Deduction for Salary Generation " & objSG.Code & " for Pay Period of  " & objSG.PAY_PERIOD_CODE & " against Employee " & objSG.arrEMP(i) & ""
                        '    objVendInv.Description = "Deduction for Salary Generation " & objSG.Code & " for Pay Period of  " & objSG.PAY_PERIOD_CODE & " against Employee " & objSG.arrEMP(i) & ""
                        '    objVendInv.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objVendInv.Amount_Less_Discount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objVendInv.Discount_Base = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    '=========================================================

                        '    objVendInv.Arr = New List(Of clsVedorInvoiceDetail)

                        '    '' Detail Level Saving

                        '    Dim VendAccSet As String = String.Empty


                        '    Dim objVendInvTR As clsVedorInvoiceDetail = New clsVedorInvoiceDetail()

                        '    objVendInvTR.Detail_Line_No = 1
                        '    objVendInvTR.Amount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objVendInvTR.Amount_less_Discount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objVendInvTR.Total_Amount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objVendInvTR.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Gl_Account"))

                        '    objVendInvTR.Deduction_Code = clsCommon.myCstr(dt.Rows(0)("Deduction_Code"))
                        '    objVendInvTR.Deduction_Name = clsCommon.myCstr(dt.Rows(0)("deductionDes"))
                        '    objVendInvTR.Deduction_Section = clsCommon.myCstr(dt.Rows(0)("TDS_Section"))
                        '    objVendInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendInvTR.GL_Account_Code, objVendInv.loc_code, True, trans)
                        '    objVendInvTR.GL_Account_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Desp FROM TSPL_GL_ACCOUNTS WHERE Account_Code='" & objVendInvTR.GL_Account_Code & "'", trans))
                        '    objVendInv.Arr.Add(objVendInvTR)

                        '    '' for remittance entry 

                        '    'If objRemittance IsNot Nothing Then
                        '    Dim objRemittance As clsRemittance = New clsRemittance()

                        '    Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(objVendInv.Vendor_Code, trans)
                        '    objRemittance.Branch_Code = objVendor.Branch_Code
                        '    objRemittance.Select_By = objVendor.VendorTypeCode
                        '    objRemittance.Deduction_Code = objVendInvTR.Deduction_Code
                        '    objRemittance.Section_Code = objVendInvTR.Deduction_Section
                        '    objRemittance.Section_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description from TSPL_TDS_SECTION_MASTER where TDS_Group='" + objRemittance.Section_Code + "'", trans))
                        '    Dim qry As String = "select Year_Name from TSPL_TDS_FINANCIAL_YEAR where convert(date,'" + objVendInv.Invoice_Entry_Date + "',103)>=  convert(date,From_Date,103)  and convert(date,'" + objVendInv.Invoice_Entry_Date + "',103)<=convert(date,To_Date,103) "
                        '    objRemittance.Fiscal_Year = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        '    objRemittance.Quarter = "First"

                        '    objRemittance.Vendor_Code = objVendInv.Vendor_Code
                        '    objRemittance.Vendor_Name = objVendInv.Vendor_Name
                        '    objRemittance.Document_Date = objVendInv.Invoice_Entry_Date
                        '    objRemittance.Document_Type = "D"
                        '    objRemittance.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objRemittance.Calculated_TDS_Base = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objRemittance.IsTDSOverride = False
                        '    objRemittance.Actual_TDS_Base = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objRemittance.Calculated_TDS = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objRemittance.Actual_TDS = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT"))
                        '    objRemittance.Calculated_Surcharge = 0
                        '    objRemittance.Actual_Surcharge = 0
                        '    objRemittance.Calculated_Edu_Cess = 0
                        '    objRemittance.Actual_Edu_Cess = 0
                        '    objRemittance.Calculated_Sec_Educess = 0
                        '    objRemittance.Actual_Sec_Educess = 0
                        '    objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
                        '    objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess

                        '    objVendInv.RemittanceObject = objRemittance
                        '    objVendInv.RemittanceObject.Vendor_Invoice_No = ""
                        '    objVendInv.TDS_Base_Actual_Amount = objRemittance.Actual_TDS_Base
                        '    objVendInv.TDS_Base_Calculated_Amount = objRemittance.Calculated_TDS_Base
                        '    objVendInv.TDS_Percentage = objRemittance.TDS_Per
                        '    objVendInv.TDS_Actual_Amount = objRemittance.Actual_Total_TDS
                        '    objVendInv.TDS_Calculated_Amount = objRemittance.Calculated_Total_TDS
                        '    objVendInv.Nature_of_deduction = objRemittance.Deduction_Code
                        '    objVendInv.Branch_Code = objRemittance.Branch_Code
                        '    objVendInv.Balance_Amt = clsCommon.myCdbl(dt.Rows(0)("DEDUCTION_AMOUNT")) - objRemittance.Actual_Total_TDS
                        '    objVendInv.Section_Code = objRemittance.Section_Code


                        '    objVendInv.SaveData(objVendInv, True, trans)
                        '    clsVedorInvoiceHead.PostData("", objVendInv.Document_No, "", trans)
                        'End If

                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return isSaved
    End Function
    Shared Function PaymentEntry(ByVal objSG As clsSalaryGeneration, ByVal trans As SqlTransaction) As Boolean
        'BHA/16/01/19-000781 by balwinder on 17/01/2019

        Dim isSaved As Boolean = True
        Dim obj As New clsPaymentHeader()
        obj.Payment_No = ""
        obj.Entry_Desc = "Being Salaries payment for the month of  " & objSG.PAY_PERIOD_CODE & " Against Salary Generation Doc No. " & objSG.Code & ""
        obj.Against_Salary_Generation_Code = objSG.Code
        obj.Payment_Type = "MI"
        obj.Vendor_Code = ""
        obj.Vendor_Name = ""
        obj.Payment_Code = "Cheque"
        '' richa ERO/15/06/21-001411
        If clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowtoSelDateandBankforPayEntryOnSalaryGeneration, clsFixedParameterCode.AllowtoSelDateandBankforPayEntryOnSalaryGeneration, trans)) = "1" Then
            obj.Payment_Date = objSG.Payment_Date
            obj.Bank_Code = objSG.Payment_Bank_Code
        Else
            obj.Payment_Date = objSG.GENERATE_DATE 'clsPayPeriodMaster.GetToDate(objSG.PAY_PERIOD_CODE, trans)
            obj.Bank_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select BANK_CODE from TSPL_PAYROLL_ACCOUNTSETS where ACCOUNT_SET_CODE='" + objSG.SAL_ACCOUNT_SET + "'", trans))
        End If
        obj.Payment_Post_Date = obj.Payment_Date
        If clsCommon.CompairString(obj.Payment_Code, "Cheque") = CompairStringResult.Equal Then
            obj.Cheque_No = objSG.CHEQUE_NO
            obj.Cheque_Date = clsCommon.myCDate(objSG.CHEQUE_DATED)
        Else
            obj.Cheque_No = ""
            obj.Cheque_Date = Nothing
        End If


        obj.Payment_Amount = objSG.GL_SALARY_PAYABLE_AMOUNT
        obj.Total_Applied_Amount = objSG.GL_SALARY_PAYABLE_AMOUNT
        obj.Remit_To = ""
        obj.Loadout_No = ""

        obj.IsChkReverse = "N"
        obj.Bank_Charges = 0 'clsCommon.myCdbl(txtBankCharges.Text)
        obj.objRemittance = Nothing

        'If chkCForm.Checked = True Then
        '    obj.CFormRecd = "Y"
        'Else
        '    obj.CFormRecd = "N"
        'End If
        obj.CFormRecd = "N"
        obj.CForm_InvoiceNo = ""

        obj.ArrTr = New List(Of clsPaymentDetail)

        ''For Custom Fields
        'obj.Form_ID = MyBase.Form_ID
        'obj.arrCustomFields = New List(Of clsCustomFieldValues)
        'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
        '    UcCustomFields1.GetData(obj.arrCustomFields)
        'End If
        ''End of For Custom Fields

        '============================Detail Section==============================
        If clsCommon.CompairString(obj.Payment_Type, "PY") = CompairStringResult.Equal Then

        ElseIf clsCommon.CompairString(obj.Payment_Type, "MI") = CompairStringResult.Equal Then
            Dim ESiAmt As Decimal = 0.0
            Dim MiscAmt As Decimal = 0.0
            Dim ESI_Percent As Decimal = 0.0
            MiscAmt = MiscAmt + objSG.GL_SALARY_PAYABLE_AMOUNT
            ESiAmt = ESiAmt + objSG.GL_SALARY_PAYABLE_AMOUNT * -1
            If MiscAmt = 0 Then
                ESI_Percent = 0
            Else
                ESI_Percent = (ESiAmt / MiscAmt) * 100
            End If


            If clsCommon.myLen(objSG.GL_Employer_ESI_PAYABLE) > 0 Then
                Dim objTr As New clsPaymentDetail()
                objTr.Payment_Type = obj.Payment_Type
                objTr.Account_Code = objSG.SALARY_PAYABLE_ACC
                objTr.Description = objSG.SALARY_PAYABLE_ACC_Desc
                'objTr.Applied_Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                objTr.Net_Balance = objSG.GL_SALARY_PAYABLE_AMOUNT
                objTr.Remarks = "Being Salaries payment for the month of " & objSG.PAY_PERIOD_CODE
                objTr.ESI_WCT_Percentage = ESI_Percent
                obj.ArrTr.Add(objTr)
            End If
        End If
        '==================Detail Section Ends Here=======================

        '' CurrencyConversion

        obj.CURRENCY_CODE = Nothing
        obj.ConvRate = 1
        obj.ConvRateOld = 1
        obj.ApplicableFrom = Nothing
        obj.BASE_CURRENCY_CODE = Nothing
        obj.PAYMENT_AMOUNT_BASE_CURRENCY = objSG.GL_SALARY_PAYABLE_AMOUNT
        obj.EXCHANGE_LOSS_AMT = 0
        obj.EXCHANGE_GAIN_AMT = 0
        obj.EXCHANGE_GAIN_ACCOUNT = Nothing
        obj.EXCHANGE_LOSS_ACCOUNT = Nothing
        '' end CurrencyConversion
        isSaved = isSaved And clsPaymentHeader.PostData(obj.SaveDataWithPaymentNo(obj, True, trans).Payment_No, "MPayable", trans)
        Return isSaved
    End Function

    Public Shared Function GetSalaryReportData(ByVal arrPayPeriodCode As ArrayList, ByVal arrLocation As ArrayList, ByVal arrDivision As ArrayList, ByVal strOuterCond As String, ByVal WithArrear As Boolean) As DataTable
        Dim dt As DataTable
        Dim FinalQry As String = ""
        Try
            FinalQry = GetSalaryReportBaseQry(arrPayPeriodCode, arrLocation, arrDivision, strOuterCond, WithArrear) & " ORDER BY Final.EMP_CODE,PPM.DATE_FROM"
            ' '' done by panch Raj Against Ticket No : BM00000007870 on 17/09/2015
            'Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(arrPayPeriodCode.Item(0))
            'Dim qry As String = ""
            'qry = "declare @TYPE TYPE_TSPL_HEADWISE_SALARY , @STRQ VARCHAR(MAX); "
            'qry = qry & "insert into @TYPE "
            'If Not arrPayPeriodCode Is Nothing AndAlso arrPayPeriodCode.Count > 0 Then
            '    For Each Str As String In arrPayPeriodCode
            '        If arrPayPeriodCode.IndexOf(Str) = 0 Then
            '            qry = qry & " select '" & Str & "',null,null "
            '        Else
            '            qry = qry & " union all  select '" & Str & "',null,null "
            '        End If
            '    Next
            'End If
            'If Not arrLocation Is Nothing AndAlso arrLocation.Count > 0 Then
            '    For Each Str As String In arrLocation
            '        qry = qry & " union all  select null,'" & Str & "',null "
            '    Next
            'End If
            'If Not arrDivision Is Nothing AndAlso arrDivision.Count > 0 Then
            '    For Each Str As String In arrDivision
            '        qry = qry & " union all  select null,null,'" & Str & "' "
            '    Next
            'End If

            'qry += " EXEC TSPL_HEADWISE_SALARY @Type," & strOuterCond & ",@STRQ OUTPUT; "
            'qry += "SELECT @STRQ; "
            'dt = clsDBFuncationality.GetDataTable(qry)
            If clsCommon.myLen(FinalQry) > 0 Then
                '    Dim strQry As String = clsCommon.myCstr(dt.Rows(0)(0))
                '    Dim FinalQry As String = ""
                '    FinalQry = " select GS.Pay_Period_Code as [Pay Period],Final.*,cast(EMPStatus.IS_PF_APPL as integer) AS EPF_AC_01,((case when EMPStatus.IS_PF_APPL=1 " & _
                '        " then 1 else 0 end)-(case when EMPStatus.IS_PF_APPL=1 and CoEPF_AMT_AC01>0 and  coalesce(CoEPS_AMT_AC10,0)<=0 then 1 else 0 end)) as EPF_AC_10,cast(EMPStatus.IS_PF_APPL as integer) AS EDLI_AC_21," & _
                '        " Salary_EPF_AC_01,Salary_EPF_AC_10,Salary_EDLI_AC_21,EPF_Amount_AC_01,Pension_Amount_AC_10,Diff_Amount_AC_01,Admin_Amt_AC_02," & _
                '        " CoEPF_RATE_AC01,CoEPF_AMT_AC01,CoEPS_RATE_AC10,CoEPS_AMT_AC10,EDLI_RATE_AC21,EDLI_Amt_AC_21,ESI_HEAD_VALUE " & _
                '        " ,ESI_Amount,Co_ESI_RATE,Co_ESI_AMT from (" & strQry & ") as Final " & _
                '        " left join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON Final.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE " & _
                '        " AND Final.EMP_CODE=GSA.EMP_CODE" & _
                '        " left join TSPL_GENERATE_SALARY GS ON Final.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " & _
                '        " left join TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " & _
                '        " LEFT JOIN TSPL_EMPLOYEE_STATUS EMPStatus on GSA.EMP_STATUS_CODE=EMPStatus.EMP_STATUS_CODE " & _
                '        " LEFT JOIN (select TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_01, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_10, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EDLI_AC_21, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' then ACTUAL_AMOUNT ELSE 0 END) AS EPF_Amount_AC_01, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then CoEPS_AMT_AC10 ELSE 0 END) AS Pension_Amount_AC_10, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and Actual_Amount>0 then (Actual_Amount-(case when HEAD_VALUE>PF_MAX_LM then CoEPS_AMT_AC10*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else CoEPS_AMT_AC10 end)) ELSE 0 END) AS Diff_Amount_AC_01, " & _
                '        " (max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*" & objPF.ACCOEPF_PER & "/100 AS Admin_Amt_AC_02, " & _
                '        " (max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*" & objPF.COEDLI_PER & "/100 AS EDLI_Amt_AC_21, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS ELSE 0 END) AS PF_MAX_LM, " & _
                '        " max(CoEPF_RATE_AC01) as CoEPF_RATE_AC01,max(CoEPF_AMT_AC01) as CoEPF_AMT_AC01,max(CoEPS_RATE_AC10) as CoEPS_RATE_AC10, " & _
                '        " max(CoEPS_AMT_AC10) as CoEPS_AMT_AC10,max(EDLI_RATE_AC21) as EDLI_RATE_AC21, " & _
                '        " max(EDLI_AMT_AC21) as EDLI_AMT_AC21, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 then HEAD_VALUE ELSE 0 END) as ESI_HEAD_VALUE, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EMPESI' then ACTUAL_AMOUNT ELSE 0 END) AS ESI_Amount, " & _
                '        " max(Co_ESI_RATE) as Co_ESI_RATE,max(Co_ESI_AMT) as Co_ESI_AMT " & _
                '        " from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " & _
                '        " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where SUB_HEAD_TYPE in ('EPF','EMPESI') " & _
                '        " group by TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE) AS GSP ON Final.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE " & _
                '        " AND Final.EMP_CODE=GSP.EMP_CODE " & _
                '        " ORDER BY Final.EMP_CODE,PPM.DATE_FROM "

                dt = clsDBFuncationality.GetDataTable(FinalQry)
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetSalaryReportData1(ByVal arrPayPeriodCode As ArrayList, ByVal arrLocation As ArrayList, ByVal arrDivision As ArrayList, ByVal strOuterCond As String, ByVal employee As String, ByVal WithArrear As Boolean) As DataTable
        Dim dt As DataTable
        Dim FinalQry As String = ""
        Try
            FinalQry = GetSalaryReportBaseQry1(arrPayPeriodCode, arrLocation, arrDivision, strOuterCond, employee, WithArrear)
            ' '' done by panch Raj Against Ticket No : BM00000007870 on 17/09/2015
            'Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(arrPayPeriodCode.Item(0))
            'Dim qry As String = ""
            'qry = "declare @TYPE TYPE_TSPL_HEADWISE_SALARY , @STRQ VARCHAR(MAX); "
            'qry = qry & "insert into @TYPE "
            'If Not arrPayPeriodCode Is Nothing AndAlso arrPayPeriodCode.Count > 0 Then
            '    For Each Str As String In arrPayPeriodCode
            '        If arrPayPeriodCode.IndexOf(Str) = 0 Then
            '            qry = qry & " select '" & Str & "',null,null "
            '        Else
            '            qry = qry & " union all  select '" & Str & "',null,null "
            '        End If
            '    Next
            'End If
            'If Not arrLocation Is Nothing AndAlso arrLocation.Count > 0 Then
            '    For Each Str As String In arrLocation
            '        qry = qry & " union all  select null,'" & Str & "',null "
            '    Next
            'End If
            'If Not arrDivision Is Nothing AndAlso arrDivision.Count > 0 Then
            '    For Each Str As String In arrDivision
            '        qry = qry & " union all  select null,null,'" & Str & "' "
            '    Next
            'End If

            'qry += " EXEC TSPL_HEADWISE_SALARY @Type," & strOuterCond & ",@STRQ OUTPUT; "
            'qry += "SELECT @STRQ; "
            'dt = clsDBFuncationality.GetDataTable(qry)
            If clsCommon.myLen(FinalQry) > 0 Then
                '    Dim strQry As String = clsCommon.myCstr(dt.Rows(0)(0))
                '    Dim FinalQry As String = ""
                '    FinalQry = " select GS.Pay_Period_Code as [Pay Period],Final.*,cast(EMPStatus.IS_PF_APPL as integer) AS EPF_AC_01,((case when EMPStatus.IS_PF_APPL=1 " & _
                '        " then 1 else 0 end)-(case when EMPStatus.IS_PF_APPL=1 and CoEPF_AMT_AC01>0 and  coalesce(CoEPS_AMT_AC10,0)<=0 then 1 else 0 end)) as EPF_AC_10,cast(EMPStatus.IS_PF_APPL as integer) AS EDLI_AC_21," & _
                '        " Salary_EPF_AC_01,Salary_EPF_AC_10,Salary_EDLI_AC_21,EPF_Amount_AC_01,Pension_Amount_AC_10,Diff_Amount_AC_01,Admin_Amt_AC_02," & _
                '        " CoEPF_RATE_AC01,CoEPF_AMT_AC01,CoEPS_RATE_AC10,CoEPS_AMT_AC10,EDLI_RATE_AC21,EDLI_Amt_AC_21,ESI_HEAD_VALUE " & _
                '        " ,ESI_Amount,Co_ESI_RATE,Co_ESI_AMT from (" & strQry & ") as Final " & _
                '        " left join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON Final.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE " & _
                '        " AND Final.EMP_CODE=GSA.EMP_CODE" & _
                '        " left join TSPL_GENERATE_SALARY GS ON Final.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " & _
                '        " left join TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " & _
                '        " LEFT JOIN TSPL_EMPLOYEE_STATUS EMPStatus on GSA.EMP_STATUS_CODE=EMPStatus.EMP_STATUS_CODE " & _
                '        " LEFT JOIN (select TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_01, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_10, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EDLI_AC_21, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' then ACTUAL_AMOUNT ELSE 0 END) AS EPF_Amount_AC_01, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then CoEPS_AMT_AC10 ELSE 0 END) AS Pension_Amount_AC_10, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' and Actual_Amount>0 then (Actual_Amount-(case when HEAD_VALUE>PF_MAX_LM then CoEPS_AMT_AC10*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else CoEPS_AMT_AC10 end)) ELSE 0 END) AS Diff_Amount_AC_01, " & _
                '        " (max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*" & objPF.ACCOEPF_PER & "/100 AS Admin_Amt_AC_02, " & _
                '        " (max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*" & objPF.COEDLI_PER & "/100 AS EDLI_Amt_AC_21, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EPF' then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS ELSE 0 END) AS PF_MAX_LM, " & _
                '        " max(CoEPF_RATE_AC01) as CoEPF_RATE_AC01,max(CoEPF_AMT_AC01) as CoEPF_AMT_AC01,max(CoEPS_RATE_AC10) as CoEPS_RATE_AC10, " & _
                '        " max(CoEPS_AMT_AC10) as CoEPS_AMT_AC10,max(EDLI_RATE_AC21) as EDLI_RATE_AC21, " & _
                '        " max(EDLI_AMT_AC21) as EDLI_AMT_AC21, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 then HEAD_VALUE ELSE 0 END) as ESI_HEAD_VALUE, " & _
                '        " max(CASE WHEN SUB_HEAD_TYPE='EMPESI' then ACTUAL_AMOUNT ELSE 0 END) AS ESI_Amount, " & _
                '        " max(Co_ESI_RATE) as Co_ESI_RATE,max(Co_ESI_AMT) as Co_ESI_AMT " & _
                '        " from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " & _
                '        " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where SUB_HEAD_TYPE in ('EPF','EMPESI') " & _
                '        " group by TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE) AS GSP ON Final.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE " & _
                '        " AND Final.EMP_CODE=GSP.EMP_CODE " & _
                '        " ORDER BY Final.EMP_CODE,PPM.DATE_FROM "

                dt = clsDBFuncationality.GetDataTable(FinalQry)
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetEmployeeMISReportData(ByVal arrPayPeriodCode As ArrayList, ByVal arrLocation As ArrayList, ByVal arrDivision As ArrayList, ByVal strOuterCond As String, ByVal arrEmpCode As ArrayList, ByVal strFromDate As Date, ByVal StrToDate As Date, ByVal WithArrear As Boolean) As DataTable
        Dim dt As DataTable
        Dim SalaryQry As String = ""
        Dim FinalQry As String
        Try
            SalaryQry = GetSalaryReportBaseQry(arrPayPeriodCode, arrLocation, arrDivision, strOuterCond, WithArrear)
            Dim qry1 As String = " select distinct(select distinct ',['+PAY_HEAD_CODE+']'from 	(select  (SELECT PAY_PERIOD_CODE FROM TSPL_PAYPERIOD_mASTER WHERE CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.INVOICE_ENTRY_DATE,103) BETWEEN DATE_FROM AND DATE_TO ) AS PAY_PERIOD_CODE,TSPL_PAYHEAD_MASTER.PAY_HEAD_NAME,isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0) as ISEARNING,TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE,TSPL_VENDOR_INVOICE_DETAIL.Amount as amount,TSPL_EMPLOYEE_MASTER.Emp_code,TSPL_EMPLOYEE_MASTER.Designation,TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,GL_Account_Code,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_VENDOR_INVOICE_DETAIL.Payment_Amount from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No inner join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code inner join TSPL_PAYHEAD_MASTER on concat(reverse(stuff(reverse(TSPL_PAYHEAD_MASTER.Account_Code),1,4,'')),'-',TSPL_VENDOR_INVOICE_HEAD.Loc_Code)=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code    union all select   (SELECT PAY_PERIOD_CODE FROM TSPL_PAYPERIOD_mASTER WHERE TSPL_PAYMENT_HEADER.PAYMENT_DATE BETWEEN DATE_FROM AND DATE_TO ) AS PAY_PERIOD_CODE ,isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0) as ISEARNING,TSPL_PAYHEAD_MASTER.PAY_HEAD_NAME,TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE,TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT as amount,TSPL_EMPLOYEE_MASTER.Emp_code,TSPL_EMPLOYEE_MASTER.Designation,TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,TSPL_PAYMENT_DETAIL.Account_Code,TSPL_PAYMENT_HEADER.Document_No,TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_EMPLOYEE_MASTER.LOCATION_CODE,TSPL_LOCATION_MASTER.Location_Desc, TSPL_PAYMENT_HEADER.Payment_Amount from TSPL_PAYMENT_HEADER inner join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No=TSPL_PAYMENT_HEADER.Document_No left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_PAYMENT_HEADER.Vendor_Code inner join TSPL_PAYHEAD_MASTER on concat(reverse(stuff(reverse(TSPL_PAYHEAD_MASTER.Account_Code),1,4,'')),'-',TSPL_EMPLOYEE_MASTER.LOCATION_CODE)=TSPL_PAYMENT_DETAIL.Account_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EMPLOYEE_MASTER.LOCATION_CODE where TSPL_PAYMENT_HEADER.Payment_Type='MI'	)as Test " &
" inner join tspl_payperiod_master on tspl_payperiod_master.Pay_Period_Code=test.PAY_PERIOD_CODE " &
" where TSPL_PAYPERIOD_MASTER.DATE_FROM>=convert(date,'" & strFromDate & "',103) and TSPL_PAYPERIOD_MASTER.DATE_TO<=convert(date,'" & StrToDate & "',103) " &
  " for xml path('')) as x "

            '            Dim qry1 As String = " select distinct (Select distinct ',['+PAY_HEAD_CODE+']' from TSPL_VENDOR_INVOICE_HEAD" & _
            '" left join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No " & _
            '" inner join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code " & _
            '" inner join TSPL_PAYHEAD_MASTER on concat(reverse(stuff(reverse(TSPL_PAYHEAD_MASTER.Account_Code),1,4,'')),'-',TSPL_VENDOR_INVOICE_HEAD.Loc_Code)=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code " & _
            '" inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code	" & _
            '" left join TSPL_PAYMENT_DETAIL on  TSPL_PAYMENT_DETAIL.account_code=tspl_payhead_master.account_code " & _
            '" left join TSPL_PAYMENT_HEADER on TSPL_PAYMENT_HEADER.document_no=TSPL_PAYMENT_DETAIL.document_no " & _
            ' " for xml path('')) as x "
            Dim pivotheader As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
            If clsCommon.myLen(pivotheader) > 0 AndAlso pivotheader.Substring(0, 1) = "," Then
                pivotheader = pivotheader.Substring(1, pivotheader.Length - 1)
            End If

            'Ticket No  BHA/06/02/19-000807 
            If clsCommon.myLen(pivotheader) = 0 Then
                dt = New DataTable
                Return dt
            End If

            FinalQry = "select *,CTC as [CTC Amount] from (select Salary.*,xxx.PAY_HEAD_CODE,xxx.amount,CAse when xxx.ISEARNING=1 then Amount+salary.[NET SALARY] else Salary.[NET SALARY]-amount end as CTC from (" & SalaryQry & ") as Salary " &
                " left outer join (" &
                     " select max(pp.PAY_PERIOD_CODE) as PAY_PERIOD_CODE,max(pp.PAY_HEAD_CODE ) as PAY_HEAD_CODE ,max(pp.PAY_HEAD_NAME) as PAY_HEAD_NAME ,sum(amount) as amount,max(pp.EMP_CODE) as Emp_Code,max(pp.Vendor_Name) as Vendor_Name,sum(pp.Payment_Amount)as Payment_Amount,MAX(CONVERT(int,pp.ISEARNING)) as ISEARNING" &
            " from ( select  (SELECT PAY_PERIOD_CODE FROM TSPL_PAYPERIOD_mASTER WHERE CONVERT(DATE,TSPL_VENDOR_INVOICE_HEAD.INVOICE_ENTRY_DATE,103) BETWEEN DATE_FROM AND DATE_TO ) AS PAY_PERIOD_CODE,TSPL_PAYHEAD_MASTER.PAY_HEAD_NAME,isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0) as ISEARNING,TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE,(TSPL_VENDOR_INVOICE_DETAIL.Amount) as amount,TSPL_EMPLOYEE_MASTER.Emp_code,TSPL_EMPLOYEE_MASTER.Designation,TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,GL_Account_Code,TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Vendor_Name,TSPL_VENDOR_INVOICE_HEAD.Loc_Code,TSPL_LOCATION_MASTER.Location_Desc, TSPL_VENDOR_INVOICE_DETAIL.Payment_Amount from TSPL_VENDOR_INVOICE_HEAD inner join TSPL_VENDOR_INVOICE_DETAIL on TSPL_VENDOR_INVOICE_DETAIL.Document_No=TSPL_VENDOR_INVOICE_HEAD.Document_No inner join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code inner join TSPL_PAYHEAD_MASTER on concat(reverse(stuff(reverse(TSPL_PAYHEAD_MASTER.Account_Code),1,4,'')),'-',TSPL_VENDOR_INVOICE_HEAD.Loc_Code)=TSPL_VENDOR_INVOICE_DETAIL.GL_Account_Code  inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code   " &
            " union all " &
            "  select   (SELECT PAY_PERIOD_CODE FROM TSPL_PAYPERIOD_mASTER WHERE TSPL_PAYMENT_HEADER.PAYMENT_DATE BETWEEN DATE_FROM AND DATE_TO ) AS PAY_PERIOD_CODE ,tSPL_PAYHEAD_MASTER.PAY_HEAD_NAME,isnull(TSPL_PAYHEAD_MASTER.ISEARNING,0) as ISEARNING,TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE,(TSPL_PAYMENT_HEADER.PAYMENT_AMOUNT) as amount,TSPL_EMPLOYEE_MASTER.Emp_code,TSPL_EMPLOYEE_MASTER.Designation,TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE,TSPL_PAYMENT_DETAIL.Account_Code,TSPL_PAYMENT_HEADER.Document_No,TSPL_PAYMENT_HEADER.Vendor_Name,TSPL_EMPLOYEE_MASTER.LOCATION_CODE,TSPL_LOCATION_MASTER.Location_Desc, TSPL_PAYMENT_HEADER.Payment_Amount from TSPL_PAYMENT_HEADER inner join TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No=TSPL_PAYMENT_HEADER.Document_No left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_PAYMENT_HEADER.Vendor_Code inner join TSPL_PAYHEAD_MASTER on concat(reverse(stuff(reverse(TSPL_PAYHEAD_MASTER.Account_Code),1,4,'')),'-',TSPL_EMPLOYEE_MASTER.LOCATION_CODE)=TSPL_PAYMENT_DETAIL.Account_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EMPLOYEE_MASTER.LOCATION_CODE where TSPL_PAYMENT_HEADER.Payment_Type='MI'  ) " &
            " as pp  group by EMP_CODE ) " &
             " as xxx on Salary.EMP_Code=xxx.Emp_code  AND Salary.[PAY PERIOD]=xxx.PAY_PERIOD_CODE inner join tspl_payperiod_master on tspl_payperiod_master.Pay_Period_Code=salary.[pay Period] where 2=2  "
            If Not arrEmpCode Is Nothing AndAlso arrEmpCode.Count > 0 Then
                For Each Str As String In arrEmpCode
                    FinalQry += " and Salary.EMP_CODE in ('" & Str & "') "
                Next
            End If
            FinalQry += " and tspl_payperiod_master.DATE_FROM>=convert(date,'" & strFromDate & "',103) and tspl_payperiod_master.DATE_TO<=convert(date,'" & StrToDate & "',103)" &
             " ) as pp  pivot(max(Amount) for PAY_HEAD_Code in (" & pivotheader & "))t  "
            If clsCommon.myLen(SalaryQry) > 0 Then

                dt = clsDBFuncationality.GetDataTable(FinalQry)
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return dt
    End Function
    Public Shared Function GetSalaryReportBaseQry1(ByVal arrPayPeriodCode As ArrayList, ByVal arrLocation As ArrayList, ByVal arrDivision As ArrayList, ByVal strOuterCond As String, ByVal employee As String, ByVal withArrear As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim dt As DataTable
        Dim FinalQry As String = ""
        Dim whr As String = ""
        Try
            '' done by panch Raj Against Ticket No : BM00000007870 on 17/09/2015
            Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(arrPayPeriodCode.Item(0), trans)
            Dim qry As String = ""
            qry = "declare @TYPE TYPE_TSPL_HEADWISE_SALARY , @STRQ VARCHAR(MAX); "
            qry = qry & "insert into @TYPE "
            If Not arrPayPeriodCode Is Nothing AndAlso arrPayPeriodCode.Count > 0 Then
                For Each Str As String In arrPayPeriodCode
                    If arrPayPeriodCode.IndexOf(Str) = 0 Then
                        qry = qry & " select '" & Str & "',null,null "
                    Else
                        qry = qry & " union all  select '" & Str & "',null,null "
                    End If
                Next
            End If
            If Not arrLocation Is Nothing AndAlso arrLocation.Count > 0 Then
                For Each Str As String In arrLocation
                    qry = qry & " union all  select null,'" & Str & "',null "
                Next
            End If
            If Not arrDivision Is Nothing AndAlso arrDivision.Count > 0 Then
                For Each Str As String In arrDivision
                    qry = qry & " union all  select null,null,'" & Str & "' "
                Next
            End If
            If withArrear Then
                qry += " EXEC TSPL_HEADWISE_SALARY_With_Arrear @Type," & strOuterCond & ",@STRQ OUTPUT; "
            Else
                qry += " EXEC TSPL_HEADWISE_SALARY @Type," & strOuterCond & ",@STRQ OUTPUT; "
            End If

            qry += "SELECT @STRQ; "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strQry As String = clsCommon.myCstr(dt.Rows(0)(0))
                If clsCommon.myLen(employee) > 0 Then
                    whr = " where EMP_CODE= '" + employee + "'"
                End If
                FinalQry = "select PAY_PERIOD_CODE,max(SALARY_GENERATION_CODE)SALARY_GENERATION_CODE,max(EMP_CODE) AS EMP_CODE,max(EMPLOYEE_NAME)EMPLOYEE_NAME, max([Father Name])[Father Name],max([Working City])[Working City],max([PF No])[PF No],max([ESI No])[ESI No], max([Bank Acc No])[Bank Acc No],max([Date of Birth])[Date of Birth],max([Joining Date])[Joining Date], max([Relieving Date])[Relieving Date], max(Designation)Designation,max(Department)Department,max(Location)Location, max(Division)Division, max([Bank Name])[Bank Name],max([Bank Branch])[Bank Branch], max([Bank Branch Name])[Bank Branch Name],max([Payment Mode])[Payment Mode],max([Month Days])[Month Days], max([Present Days])[Present Days],  max([Payable Days])[Payable Days],max([Holidays])[Holidays], max([Week Off Days])[Week Off Days],max([Leave Days])[Leave Days] ,sum(basic)basic,sum(cca)cca,sum(daily_allowance)da,sum(house_rent_allowance)hra,sum(vwashing_allawonce)[wash.all.],sum(Gross)Gross,sum(vbasic)vbasic,sum(vcca)vcca,sum(vdaily_allowance)vda,sum(vhouse_rent_allowance)vhra,sum(vwashing_allawonce)[vwash.all.],sum([GROSS SALARY])[GROSS SALARY],sum(vempolyee_provident_fund)vepf,sum(vemployee_state_insurance)vesi,sum(vgenral_provident_fund)vgsli,sum(vkarmchari_kalyan_kosh)vkkk,sum(vswsf)vswsf,sum([TOTAL DEDUCTION])[TOTAL DEDUCTION],sum([NET SALARY])[NET SALARY],'" + objCommonVar.CurrentUser + "' As 'PrintBy' from 
                           (" & strQry & ")xy " + whr + "  group by PAY_PERIOD_CODE "
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return FinalQry
    End Function
    Public Shared Function GetSalaryReportBaseQry(ByVal arrPayPeriodCode As ArrayList, ByVal arrLocation As ArrayList, ByVal arrDivision As ArrayList, ByVal strOuterCond As String, ByVal withArrear As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As String
        Dim dt As DataTable
        Dim FinalQry As String = ""
        Try
            '' done by panch Raj Against Ticket No : BM00000007870 on 17/09/2015
            Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(arrPayPeriodCode.Item(0), trans)
            Dim qry As String = ""
            qry = "declare @TYPE TYPE_TSPL_HEADWISE_SALARY , @STRQ VARCHAR(MAX); "
            qry = qry & "insert into @TYPE "
            If Not arrPayPeriodCode Is Nothing AndAlso arrPayPeriodCode.Count > 0 Then
                For Each Str As String In arrPayPeriodCode
                    If arrPayPeriodCode.IndexOf(Str) = 0 Then
                        qry = qry & " select '" & Str & "',null,null "
                    Else
                        qry = qry & " union all  select '" & Str & "',null,null "
                    End If
                Next
            End If
            If Not arrLocation Is Nothing AndAlso arrLocation.Count > 0 Then
                For Each Str As String In arrLocation
                    qry = qry & " union all  select null,'" & Str & "',null "
                Next
            End If
            If Not arrDivision Is Nothing AndAlso arrDivision.Count > 0 Then
                For Each Str As String In arrDivision
                    qry = qry & " union all  select null,null,'" & Str & "' "
                Next
            End If
            If withArrear Then
                qry += " EXEC TSPL_HEADWISE_SALARY_With_Arrear @Type," & strOuterCond & ",@STRQ OUTPUT; "
            Else
                qry += " EXEC TSPL_HEADWISE_SALARY @Type," & strOuterCond & ",@STRQ OUTPUT; "
            End If

            qry += "SELECT @STRQ; "
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strQry As String = clsCommon.myCstr(dt.Rows(0)(0))

                FinalQry = " select GS.Pay_Period_Code as [Pay Period],Final.*,cast(EMPStatus.IS_PF_APPL as integer) AS EPF_AC_01,((case when EMPStatus.IS_PF_APPL=1 " &
                    " then 1 else 0 end)-(case when EMPStatus.IS_PF_APPL=1 and CoEPF_AMT_AC01>0 and  coalesce(CoEPS_AMT_AC10,0)<=0 then 1 else 0 end)) as EPF_AC_10,cast(EMPStatus.IS_PF_APPL as integer) AS EDLI_AC_21," &
                    " Salary_EPF_AC_01,Salary_EPF_AC_10,Salary_EDLI_AC_21,EPF_Amount_AC_01,Pension_Amount_AC_10,Diff_Amount_AC_01,Admin_Amt_AC_02," &
                    " CoEPF_RATE_AC01,CoEPF_AMT_AC01,CoEPS_RATE_AC10,CoEPS_AMT_AC10,EDLI_RATE_AC21,EDLI_Amt_AC_21,ESI_HEAD_VALUE " &
                    " ,ESI_Amount,Co_ESI_RATE,Co_ESI_AMT,convert (int, isnull(Final.OT_HOURS,0)/8 ) + case when  (isnull(Final.OT_HOURS,0)/8 - convert (int, isnull(Final.OT_HOURS,0)/8 ) ) > .49 then .50 else 0 end as OT_HOURS_In_Days,TSPL_EMPLOYEE_MASTER.UANNo as [UAN No] from (" & strQry & ") as Final " &
                    " left join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON Final.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE " &
                    " AND Final.EMP_CODE=GSA.EMP_CODE" &
                    " left join TSPL_GENERATE_SALARY GS ON Final.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " &
                    " left join TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " &
                    " LEFT JOIN TSPL_EMPLOYEE_STATUS EMPStatus on GSA.EMP_STATUS_CODE=EMPStatus.EMP_STATUS_CODE " &
                    " LEFT JOIN (select TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='COPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_01, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='COPF' and CoEPS_AMT_AC10>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_10, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='COPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EDLI_AC_21, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='COPF' then ACTUAL_AMOUNT ELSE 0 END) AS EPF_Amount_AC_01, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='COPF' and CoEPS_AMT_AC10>0 then CoEPS_AMT_AC10 ELSE 0 END) AS Pension_Amount_AC_10, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='COPF' and Actual_Amount>0 then (Actual_Amount-(case when HEAD_VALUE>PF_MAX_LM then CoEPS_AMT_AC10*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else CoEPS_AMT_AC10 end)) ELSE 0 END) AS Diff_Amount_AC_01, " &
                    " (max(CASE WHEN SUB_HEAD_TYPE='COPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*" & objPF.ACCOEPF_PER & "/100 AS Admin_Amt_AC_02, " &
                    " (max(CASE WHEN SUB_HEAD_TYPE='COPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & "*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*" & objPF.COEDLI_PER & "/100 AS EDLI_Amt_AC_21, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='COPF' then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS ELSE 0 END) AS PF_MAX_LM, " &
                    " max(CoEPF_RATE_AC01) as CoEPF_RATE_AC01,max(CoEPF_AMT_AC01) as CoEPF_AMT_AC01,max(CoEPS_RATE_AC10) as CoEPS_RATE_AC10, " &
                    " max(CoEPS_AMT_AC10) as CoEPS_AMT_AC10,max(EDLI_RATE_AC21) as EDLI_RATE_AC21, " &
                    " max(EDLI_AMT_AC21) as EDLI_AMT_AC21, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 then HEAD_VALUE ELSE 0 END) as ESI_HEAD_VALUE, " &
                    " max(CASE WHEN SUB_HEAD_TYPE='EMPESI' then ACTUAL_AMOUNT ELSE 0 END) AS ESI_Amount, " &
                    " max(Co_ESI_RATE) as Co_ESI_RATE,max(Co_ESI_AMT) as Co_ESI_AMT " &
                    " from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
                    " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where SUB_HEAD_TYPE in ('EPF','EMPESI') " &
                    " group by TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE) AS GSP ON Final.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE " &
                    " AND Final.EMP_CODE=GSP.EMP_CODE LEFT JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=Final.EMP_CODE "


            Else
                FinalQry = ""
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return FinalQry
    End Function
    Public Shared Function GetVoucherPaymentReportData(ByVal strPayPeriodCode As String) As DataTable
        Dim dt As DataTable
        Try
            Dim qry As String = ""
            qry = " DECLARE @STRQ VARCHAR(MAX); "
            qry += " EXEC TSPL_VOUCHER_PAYMENT_REGISTER '" + strPayPeriodCode + "',NULL,@STRQ OUTPUT; "
            qry += "SELECT @STRQ; "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dt = clsDBFuncationality.GetDataTable(clsCommon.myCstr(dt.Rows(0)(0)))
            Else
                dt = New DataTable
            End If
        Catch ex As Exception
            Throw New Exception("No Data Found")
        End Try
        Return dt
    End Function

    Public Shared Function GetPFESIQuery(ByVal PayPeriod As String, ByVal Location_Code As String, ByVal DivCond As String, ByVal LocName As String, ByVal LocAdress As String, ByVal FirmPf As String, Optional ByVal LocDesc As String = "") As String
        Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(PayPeriod)
        Dim objESI As clsESIRulesMaster = clsESIRulesMaster.GetRecentESIRule(PayPeriod)
        Dim Qry As String = ""
        Dim arrLoc() As String = Location_Code.Replace("(", "").Replace(")", "").Split(",")
        Dim adminEDLI As Decimal = 0
        Dim qryAdminEDLI As String = ""
        Dim strLocation As String = Nothing
        If Location_Code IsNot Nothing AndAlso clsCommon.myLen(Location_Code) > 0 Then
            strLocation = " TSPL_GENERATE_SALARY.LOCATION_CODE in " + Location_Code + " And "
        End If
        For Each strLoc As String In arrLoc
            qryAdminEDLI = " select (CASE WHEN  (((select SUM(case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
                           " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  "
            If strLoc IsNot Nothing AndAlso clsCommon.myLen(strLoc) > 0 Then
                qryAdminEDLI += " TSPL_GENERATE_SALARY.LOCATION_CODE = " & strLoc & " And "
            End If
            qryAdminEDLI += " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")* " & objPF.ACCOEDLI_PER & ")/100)>" & objPF.ACCOEDLI_MIN & " THEN ((select SUM(case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
                            " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")* " & objPF.ACCOEDLI_PER & ")/100 ELSE " & objPF.ACCOEDLI_MIN & " END) as Admin_EDLI_Amt"
            adminEDLI = adminEDLI + clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryAdminEDLI))
        Next
        ''Ticket No - BM00000007674,BM00000007377 By Panch Raj
        Dim Total_58 As Integer = 0
        Qry = " select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  where  " + strLocation + " SUB_HEAD_TYPE='EPF'  " &
              " and  PF_Applicable=1 and CoEPF_AMT_AC01>0 and  coalesce(CoEPS_AMT_AC10,0)<=0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ""
        Total_58 = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))


        Qry = "select '" + objCommonVar.CurrentUser + "' As PrintBy,'" & LocDesc & "' as Location_Code ,'" & LocName & "' as Location_Desc,'" & LocAdress & "' as Location_Address ,Comp_Name as Name,Add1 Address1,Add2 as Address2,Add3 as Address3,"
        Qry += " DATEPART(month,(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + PayPeriod + "'))Month,"
        Qry += " DATEPART(Year,(select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + PayPeriod + "'))Year,"
        Qry += " (select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" & PayPeriod & "')DateFr,"
        Qry += " (select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" & PayPeriod & "')DateTo,"
        Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  where  " + strLocation + " SUB_HEAD_TYPE='EPF'  and  PF_Applicable=1 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "'  " & DivCond & ")TotalEmpEPFAc01,"
        Qry += " (select COUNT(*)-" & Total_58 & "  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  where  " + strLocation + " SUB_HEAD_TYPE='EPF'  and  PF_Applicable=1 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")TotalEmpPensionAC10,"
        Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  where  " + strLocation + " SUB_HEAD_TYPE='EPF'  and  PF_Applicable=1 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")TotalEmpEDLIAc21,"

        'Qry += " (select COUNT(GSA.EMP_CODE)  from TSPL_GENERATE_SALARY_ATTENDANCE GSA left join TSPL_EMPLOYEE_MASTER EMP ON EMP.EMP_CODE=GSA.EMP_CODE " & _
        '       " INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE in " + Location_Code + " and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ") AS TotalEMP ,"

        Qry += " (select SUM(case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")TotalSalaryEPFAc01,"
        Qry += " (select SUM(case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")TotalSalaryPensionAc10,"
        Qry += " (select SUM(case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.CoEPF_AMT_AC01>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")TotalSalaryEDLIAc21,"

        Qry += " (select SUM(Actual_Amount)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")EPFAmtAc01,"
        Qry += " (select Case When (SUM(Actual_Amount)*" & objPF.COEPS_PER & ")/100 > " & objPF.EPS_MAX & " Then " & objPF.EPS_MAX & " Else (SUM(Actual_Amount)*" & objPF.COEPS_PER & ")/100 End   from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")PensionAmtAc10,"
        Qry += " (select SUM(Actual_Amount-(case when TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10<0 then 0 else TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10 end))+ ((SUM(Actual_Amount)*" & objPF.COEPS_PER & ")/100 - " & objPF.EPS_MAX & ")  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")DifferenceAmtAc01,"

        Qry += " (select (SUM(Actual_Amount)* " & objPF.ACCOEPF_PER & ")/100 from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")AdminAmtAc02,"
        Qry += " (select (SUM(Actual_Amount)*  " & objPF.COEDLI_PER & ")/100  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " SUB_HEAD_TYPE='EPF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")EDLIAmtAc21,"
        Qry += " " & adminEDLI & " as  AdminEDLIAmtAc22,"

        Qry += " (select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  " + strLocation + " SUB_HEAD_TYPE in('EMPESI','EMPESI') and  TSPL_GENERATE_SALARY_PAYHEADS.ESI_Applicable  = 1 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")TotEmpESI,"
        Qry += " (select SUM(HEAD_VALUE)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  " + strLocation + " SUB_HEAD_TYPE in('EMPESI','COESI') and TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")TotSalESI,"
        Qry += " (select SUM(ACTUAL_AMOUNT)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  " + strLocation + " SUB_HEAD_TYPE='EMPESI' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")EmpESIAmt,"
        'Qry += " (select SUM(Co_ESI_AMT)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  TSPL_GENERATE_SALARY.LOCATION_CODE in " + Location_Code + " and SUB_HEAD_TYPE='EMPESI' and  TSPL_GENERATE_SALARY_PAYHEADS.Co_ESI_AMT>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")EmployerESIAMT, "
        Qry += " (select CEILING((SUM(HEAD_VALUE)* " & objESI.COESI_PER & "/100))"
        '  (CASE WHEN '" & objESI.COESI_ROUNDOFF_YPE & "'='R' THEN ROUND((SUM(HEAD_VALUE)* " & objESI.COESI_PER & "/100),0) " _
        '& " WHEN '" & objESI.COESI_ROUNDOFF_YPE & "'='L' THEN  FLOOR((SUM(HEAD_VALUE)* " & objESI.COESI_PER & "/100)) WHEN '" & objESI.COESI_ROUNDOFF_YPE & "'='U' " _
        '& "THEN CEILING((SUM(HEAD_VALUE)* " & objESI.COESI_PER & "/100)) END) "
        Qry += "  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  " + strLocation + " SUB_HEAD_TYPE='EMPESI' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")EmployerESIAMT,"

        Qry += " (select SUM(Actual_Amount)*2  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE where  " + strLocation + " SUB_HEAD_TYPE='LWF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")LWFER,"
        Qry += " (select COUNT(distinct GSA.EMP_CODE)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  where  " + strLocation + " TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "'  " & DivCond & ")Total_Employee_Count,'" & FirmPf & "' as FirmPFNo "
        Qry += " from TSPL_COMPANY_MASTER where Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"
        Return Qry
    End Function

    Public Shared Function GetPFESIQuery1(ByVal PayPeriod As String, ByVal Location_Code As String, ByVal DivCond As String, ByVal LocName As String, ByVal LocAdress As String, ByVal FirmPf As String, Optional ByVal LocDesc As String = "") As String
        Dim objPF As clsPFRulesMaster = clsPFRulesMaster.GetRecentPFRule(PayPeriod)
        Dim objESI As clsESIRulesMaster = clsESIRulesMaster.GetRecentESIRule(PayPeriod)
        Dim Qry As String = ""
        Dim arrLoc() As String = Location_Code.Replace("(", "").Replace(")", "").Split(",")
        Dim adminEDLI As Decimal = 0
        Dim qryAdminEDLI As String = ""
        Dim strLocation As String = Nothing
        If Location_Code IsNot Nothing AndAlso clsCommon.myLen(Location_Code) > 0 Then
            strLocation = " TSPL_GENERATE_SALARY.LOCATION_CODE in " + Location_Code + " And "
        End If
        For Each strLoc As String In arrLoc
            qryAdminEDLI = " select (CASE WHEN  (((select SUM(case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
                           " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  "
            If strLoc IsNot Nothing AndAlso clsCommon.myLen(strLoc) > 0 Then
                qryAdminEDLI += " TSPL_GENERATE_SALARY.LOCATION_CODE = " & strLoc & " And "
            End If
            qryAdminEDLI += " PAY_HEAD_CODE='EPF' And  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")* " & objPF.ACCOEDLI_PER & ")/100)>" & objPF.ACCOEDLI_MIN & " THEN ((select SUM(case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and " &
                            " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where  " + strLocation + " PAY_HEAD_CODE='EPF' And TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ")* " & objPF.ACCOEDLI_PER & ")/100 ELSE " & objPF.ACCOEDLI_MIN & " END) as Admin_EDLI_Amt"
            adminEDLI = adminEDLI + clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qryAdminEDLI))
        Next
        ''Ticket No - BM00000007674,BM00000007377 By Panch Raj
        Dim Total_58 As Integer = 0
        Qry = " select COUNT(*)  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE  where  " + strLocation + " PAY_HEAD_CODE='EPF' " &
              " and  PF_Applicable=1 and CoEPF_AMT_AC01>0 and  coalesce(CoEPS_AMT_AC10,0)<=0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & ""
        Total_58 = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry))

        Qry = "Select ('" + objCommonVar.CurrentUser + "') As PrintBy,('" & Location_Code & "') as Location_Code ,('" & LocName & "') as Location_Desc,(TSPL_COMPANY_MASTER.Comp_Name) as Location_Address ,
(TSPL_COMPANY_MASTER.Comp_Name) as Name,(TSPL_COMPANY_MASTER.Add1) As Address1,(TSPL_COMPANY_MASTER.Add2) as Address2,(TSPL_COMPANY_MASTER.Add3) as Address3,xxx.* from (select DATEPART(month,(select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + PayPeriod + "'))Month, 
DATEPART(Year,(select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + PayPeriod + "'))Year, (select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + PayPeriod + "')DateFr,
(select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE ='" + PayPeriod + "')DateTo, Max(TSPL_GENERATE_SALARY.Pay_Period_Code)Pay_Period_Code,EMP.EMP_CODE,Max(EMP.Emp_Name)Emp_Name,
Max(TotalEmp.TotalEmpEPFAc01)TotalEmpEPFAc01,Max(TotalEmp.TotalEmpPensionAC10)TotalEmpPensionAC10,Max(TotalEmp.TotalEmpEDLIAc21)TotalEmpEDLIAc21,Max(Isnull(TotEmpESI.TotEmpESI,0))TotEmpESI,
Sum(Case When TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 Then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)Else 0 End) AS TotalSalaryEPFAc01,
Sum(Case When TSPL_GENERATE_SALARY_PAYHEADS.CoEPS_AMT_AC10>0 Then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) Else 0 End) As TotalSalaryPensionAc10,
Sum(Case When TSPL_GENERATE_SALARY_PAYHEADS.CoEPF_AMT_AC01>0 Then (case when HEAD_VALUE>" & objPF.EMPEPF_MAX & " then " & objPF.EMPEPF_MAX & " *PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end)  Else 0 End) AS TotalSalaryEDLIAc21,
Sum(Case When TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0  Then Actual_Amount Else 0 End) AS EPFAmtAc01,
Sum(Case When TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 Then Case When ((Actual_Amount)*" & objPF.COEPS_PER & ")/100 > " & objPF.EPS_MAX & " Then " & objPF.EPS_MAX & " Else ((Actual_Amount)*" & objPF.COEPS_PER & ")/100 End Else 0 End) As PensionAmtAc10,
Sum(Case When TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 Then (Actual_Amount)-(case when ((Actual_Amount)*" & objPF.COEPS_PER & ")/100 > " & objPF.EPS_MAX & " then ((Actual_Amount*" & objPF.COEPS_PER & ")/100 - " & objPF.EPS_MAX & ")*(-1) else ((Actual_Amount*" & objPF.COEPS_PER & ")/100) End) Else 0 End) As DifferenceAmtAc01,
Sum(Case When (TotWages.TotalWages)>0 Then ((TotWages.TotalWages)* " & objPF.ACCOEPF_PER & ")/100   Else 0 End) AdminAmtAc02,
Sum(Case When (TotWages.TotalWages)>0 Then ((TotWages.TotalWages)*  " & objPF.COEDLI_PER & ")/100   Else 0 End) EDLIAmtAc21,
Sum(Case When (TotWages.TotalWages)>0 Then ((TotWages.TotalWages*" & objPF.ACCOEDLI_PER & ")/100) Else 0 End) as  AdminEDLIAmtAc22,
Sum(Case When SUB_HEAD_TYPE in('EMPESI','COESI') and TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 Then IsNull(HEAD_VALUE,0) Else 0 End) As TotSalESI,
Sum(Case When SUB_HEAD_TYPE ='EMPESI' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 Then IsNull(ACTUAL_AMOUNT,0) Else 0 End) As EmpESIAmt,
Sum(Case When SUB_HEAD_TYPE ='EMPESI' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 Then CEILING(((HEAD_VALUE)* " & objESI.COESI_PER & "/100)) Else 0 End) As EmployerESIAMT,
Sum(Case When SUB_HEAD_TYPE ='LWF' and  TSPL_GENERATE_SALARY_PAYHEADS.Actual_Amount>0 Then IsNull(Actual_Amount,0)*2   Else 0 End) As LWFER,
'" & FirmPf & "' as FirmPFNo,
COUNT(distinct EMP.EMP_CODE)Total_Employee_Count 
from TSPL_GENERATE_SALARY_PAYHEADS 
left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE  
inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE 
Left Outer Join(select Max(TSPL_GENERATE_SALARY.Pay_Period_Code)Pay_Period_Code,(EMP.EMP_CODE)EMP_CODE,COUNT(*)TotalEmpEPFAc01,(COUNT(*)-" & Total_58 & ")TotalEmpPensionAC10,
COUNT(*)TotalEmpEDLIAc21
from TSPL_GENERATE_SALARY_PAYHEADS 
left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE 
INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
where " + strLocation + "  PAY_HEAD_CODE='EPF' And  PF_Applicable=1 And TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0 And TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & " Group By EMP.EMP_CODE )TotalEmp
On TotalEmp.EMP_CODE=EMP.EMP_CODE And TotalEmp.Pay_Period_Code=TSPL_GENERATE_SALARY.Pay_Period_Code
Left Outer Join(select Max(TSPL_GENERATE_SALARY.Pay_Period_Code)Pay_Period_Code,(EMP.EMP_CODE)EMP_CODE,COUNT(*)TotEmpESI  
from TSPL_GENERATE_SALARY_PAYHEADS 
left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE 
where " + strLocation + "  SUB_HEAD_TYPE in('EMPESI','EMPESI') and  TSPL_GENERATE_SALARY_PAYHEADS.ESI_Applicable  = 1 And TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & "
Group By EMP.EMP_CODE)TotEmpESI
On TotEmpESI.EMP_CODE=EMP.EMP_CODE And TotEmpESI.Pay_Period_Code=TSPL_GENERATE_SALARY.Pay_Period_Code

Left Outer Join(select Max(TSPL_GENERATE_SALARY.Pay_Period_Code)Pay_Period_Code,(EMP.EMP_CODE)EMP_CODE,Sum(Case When PAY_HEAD_CODE='BASIC' Then PAYABLE_AMOUNT Else 0 End) As BasicAmt,Sum(Case When PAY_HEAD_CODE='DA' Then PAYABLE_AMOUNT Else 0 End) As DAAmt,(Sum(Case When PAY_HEAD_CODE='BASIC' Then PAYABLE_AMOUNT Else 0 End)+Sum(Case When PAY_HEAD_CODE='DA' Then PAYABLE_AMOUNT Else 0 End)) As TotalWages
from TSPL_GENERATE_SALARY_PAYHEADS 
left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE 
INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE 
where " + strLocation + "  PF_Applicable=1 And TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT>0  And TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE in('BASIC','DA') and TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & " 
Group By EMP.EMP_CODE)TotWages
On TotWages.EMP_CODE=EMP.EMP_CODE And TotWages.Pay_Period_Code=TSPL_GENERATE_SALARY.Pay_Period_Code

where " + strLocation + "  PAY_HEAD_CODE='EPF' And  SUB_HEAD_TYPE In (Select SUB_HEAD_TYPE from TSPL_GENERATE_SALARY_PAYHEADS WHERE PAY_HEAD_CODE='EPF') And TSPL_GENERATE_SALARY.Pay_Period_Code='" & PayPeriod & "' " & DivCond & " 
Group By EMP.EMP_CODE)xxx Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'"
        Return Qry
    End Function
    Public Shared Function Generate_SalaryOLD(ByVal Pay_Period_Code As String, ByVal EmpList As ArrayList, ByVal trans As SqlTransaction, Optional ByVal Is_Arrear As Boolean = False) As Boolean
        'ProgressBar1.Minimum = 0
        'ProgressBar1.Maximum = 100
        If Is_Arrear = False Then
            'clsCommon.ProgressBarShow()
        End If

        Dim strq As String = ""
        Dim strLog As String = ""
        Dim Calc_Table As String = ""
        If Is_Arrear = False Then
            Calc_Table = "TSPL_SALARY_CALCULATION"
        Else
            Calc_Table = "TSPL_ARREAR_CALCULATION"
        End If
        Dim logFile As String = "c:\ERPTempFolder\salgenlog.txt"
        clsCommon.ProgressBarUpdate("Checking for log file...")
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, False)
            stream.WriteLine("")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        Dim SettPFCalculationOnHeadValue As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PFCalculationOnFormulaHead, clsFixedParameterCode.PFCalculationOnFormulaHead, trans))

        'If isNewEntry Then
        'Else
        '    cbgEmp.CheckedAll()
        'End If
        clsCommon.ProgressBarUpdate("Getting Pay Period Information...")
        Dim objPP As clsPayPeriodMaster = clsPayPeriodMaster.GetData(Pay_Period_Code, NavigatorType.Current, trans)
        Dim PP_START_DATE As Date
        Dim PP_END_DATE As Date
        Dim PayPeriodDays As Integer
        PP_START_DATE = objPP.DATE_FROM 'Me.dtpFrom.Value
        PP_END_DATE = objPP.DATE_TO
        PayPeriodDays = DateDiff(DateInterval.Day, objPP.DATE_FROM, objPP.DATE_TO) + 1
        '' checking for the working employee
        '(" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")
        Dim strEmp As String
        strEmp = "(" & clsCommon.GetMulcallString(EmpList) & ")"

        ''  set the text status in progress bar here "Checking the list of working employees."
        'ProgressBar1.Text = "Collecting the list of working employees..."
        clsCommon.ProgressBarUpdate("Collecting the list of working employees...")
        strq = "SELECT T1.EMP_STATUS_CODE,T1.EMP_CODE,T1.REVISION_NO,T1.BRANCH_CODE,T1.DESIGNATION_ID,T1.IS_PF_APPL," _
        & " T1.PF_NO, T1.IS_ESI_APPL, T1.ESI_NO, T1.IS_BONUS_APPL, T1.BONUS_CODE, T1.IS_OT_APPL, T1.OT_CODE, T1.WORKING_STATUS " _
        & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
        & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
        & " GROUP BY EMP_CODE  HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "') AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE WHERE T1.EMP_CODE IN " & strEmp & ""
        Dim dt_empStatus As DataTable
        dt_empStatus = clsDBFuncationality.GetDataTable(strq, trans)
        'Me.ProgressBar1.Value = 5
        If dt_empStatus.Rows.Count = 0 Then
            'Me.ProgressBar1.Value = 100            
            'clsCommon.ProgressBarHide()
            Throw New Exception("No Working Employee to generate salary or Unapproved!")
            'Return False
            Exit Function
        End If

        '' checking Salay status of working employees.
        'ProgressBar1.Text = "Collecting Salary Status of working ..."
        clsCommon.ProgressBarUpdate("Collecting Salary Status of working employees...")
        strq = "SELECT TT1.*,TT2.EMP_SAL_CODE,TT2.REVISION_NO AS SAL_REVISION_NO,COALESCE(EMP.EMP_NAME,'') AS EMP_NAME FROM  (" _
               & " SELECT T1.EMP_STATUS_CODE,T1.EMP_CODE,T1.REVISION_NO,T1.BRANCH_CODE,T1.DESIGNATION_ID,T1.IS_PF_APPL," _
               & " T1.PF_NO, T1.IS_ESI_APPL, T1.ESI_NO, T1.IS_BONUS_APPL, T1.BONUS_CODE, T1.IS_OT_APPL, T1.OT_CODE, T1.WORKING_STATUS " _
               & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
               & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "'   " _
               & " GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "') AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
               & " LEFT JOIN ( " _
               & " select EMP_CODE,MAX(EMP_SAL_CODE) AS EMP_SAL_CODE,MAX(REVISION_NO) AS REVISION_NO  " _
               & " from TSPL_EMPLOYEE_SALARY WHERE  APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
               & " GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "') AS TT2 ON TT1.EMP_CODE=TT2.EMP_CODE " _
               & " left join TSPL_EMPLOYEE_MASTER EMP ON TT1.EMP_CODE=EMP.EMP_CODE " _
               & " WHERE TT1.EMP_CODE IN " & strEmp & ""

        Dim dt_salStatus As DataTable
        dt_salStatus = clsDBFuncationality.GetDataTable(strq, trans)
        'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'If (Me.ProgressBar1.Value + 5) > 100 Then
        '    Me.ProgressBar1.Value = 100
        'Else
        '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'End If
        Dim drNASal() As DataRow
        drNASal = dt_salStatus.Select("EMP_SAL_CODE IS NULL")
        If drNASal.Length > 0 Then

            'ProgressBar1.Text = "Generating log of employees without salary definition..."
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Employees not having the salary definitions:")
            For intloop As Integer = 0 To drNASal.Length - 1
                objWriter.WriteLine((intloop + 1) & ". " & drNASal(intloop).Item("EMP_CODE") & "-" & drNASal(intloop).Item("EMP_NAME"))
            Next
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
            'If (Me.ProgressBar1.Value + 5) > 100 Then
            '    Me.ProgressBar1.Value = 100
            'Else
            '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
            'End If
            'Me.ProgressBar1.Text = "Finished...."
            'Me.ProgressBar1.Value = 100
            objWriter.Close()
            'clsCommon.ProgressBarHide()
            Throw New Exception("Some Working Employee's Salary is not defined or Unapproved !")
            'Return False
            Exit Function
        End If
        '' calculation of salary calculation days

        '' this calculation must be on the basis of setting in attendance master 
        '' 1. Actual days/Month(ADM)
        '' 2. Only Working Days(WD)
        '' 3. Only Working days+Weekly Holidays(WDWH)
        '' 4. Only Working Days + Holidays(WDH)
        '' 5. Fixed(30) Days/Month(FD)
        '' 6. User Defined(UD)
        Dim salADM As Integer
        Dim salWD As Integer
        Dim salWDWH As Integer
        Dim salWDH As Integer
        Dim salFD As Integer
        Dim salUD As Integer
        Dim condSalDays As String = "" '' days on which salary is to be calculated
        Dim condPayableDays As String = ""
        Dim condLOPDays As String = ""
        Dim leaveCode As String = "('CL','PL','EL'"

        clsCommon.ProgressBarUpdate("Calculating Days in each leave status ...")
        '' but currently this salary generation is processed according to the first setting ie. Actual Days per month that is default
        salADM = PayPeriodDays
        salWD = PayPeriodDays - TotalGLHL(objPP.DATE_FROM, objPP.DATE_TO, trans) - TotalWKHL(objPP.DATE_FROM, objPP.DATE_TO, trans)
        salWDWH = PayPeriodDays - TotalGLHL(objPP.DATE_FROM, objPP.DATE_TO, trans)
        salWDH = PayPeriodDays - TotalWKHL(objPP.DATE_FROM, objPP.DATE_TO, trans)
        salFD = 30
        salUD = 30
        condSalDays += " (CASE WHEN T3.CALC_SAL_ON='ADM' THEN " & salADM & " WHEN T3.CALC_SAL_ON='WD' THEN " & salWD & " WHEN T3.CALC_SAL_ON='WDWH' THEN " & salWDWH & " "
        condSalDays += " WHEN T3.CALC_SAL_ON='WDH' THEN " & salWDH & " WHEN T3.CALC_SAL_ON='FM' THEN " & salFD & " ELSE " & salUD & " END)"

        condPayableDays += " (CASE WHEN T3.CALC_SAL_ON='ADM' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS+T2.WEEKLY_OFF_DAYS) WHEN T3.CALC_SAL_ON='WD' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS) WHEN T3.CALC_SAL_ON='WDWH' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.WEEKLY_OFF_DAYS) "
        condPayableDays += " WHEN T3.CALC_SAL_ON='WDH' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS) WHEN T3.CALC_SAL_ON='FM' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS+T2.WEEKLY_OFF_DAYS) ELSE (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS+T2.WEEKLY_OFF_DAYS) END)"

        Dim strqLeave As String
        strqLeave = "select LEAVE_CODE from TSPL_LEAVE_MASTER where LEAVE_CODE not in ('EL','CL','PL')"
        Dim dtLeave As DataTable
        dtLeave = clsDBFuncationality.GetDataTable(strqLeave, trans)
        For intloop As Integer = 0 To dtLeave.Rows.Count - 1
            leaveCode = leaveCode & "," & "'" & dtLeave.Rows(intloop).Item("LEAVE_CODE") & "'"
        Next
        leaveCode = leaveCode & ")"

        ''--------------------- attendance checking-------------------------
        '& " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO','H','COFF') THEN 0.5 ELSE 0 END)) AS WKHL_DAYS," _
        '& " SUM((CASE WHEN T2.FIRST_HALF IN ('H') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO','H','COFF') THEN 0.5 ELSE 0 END)) AS GNGL_DAYS," _

        'ProgressBar1.Text = "Collecting list of attendance..."
        clsCommon.ProgressBarUpdate("Collecting list of attendance...")
        strq = "SELECT '" & Pay_Period_Code & "' AS PAY_PERIOD_CODE,T1.EMP_CODE,COALESCE(EMP.EMP_NAME,'') AS EMP_NAME,T2.REGISTER_TYPE,T1.ATTENDANCE_CODE,T3.CALC_SAL_ON," & condSalDays & " AS PAYPERIOD_DAYS, " _
               & " T2.PRESENT_DAYS,T2.ABSENT_DAYS,T2.LEAVE_DAYS,T2.HOLIDAY_DAYS,T2.WEEKLY_OFF_DAYS," & condPayableDays & " AS PAYABLE_DAYS, " _
               & " (" & condSalDays & "-" & condPayableDays & ") AS LOP_DAYS,T2.POSTED FROM ( " _
               & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS  FROM TSPL_EMPLOYEE_STATUS T1 " _
               & " INNER JOIN (  select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO " _
               & " FROM TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "'  GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "' ) AS T2" _
               & " ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS T1" & Environment.NewLine _
               & " LEFT JOIN ( " _
               & " SELECT T1.*,'HR' AS REGISTER_TYPE FROM (" _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS,T1.POSTED " _
               & " FROM TSPL_HOURLY_ATTENDANCE T1 INNER JOIN TSPL_HOURLY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE  T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE,T1.POSTED) AS T1 " & Environment.NewLine _
               & " UNION ALL " _
               & " SELECT T1.*,'DL' AS REGISTER_TYPE FROM ( " _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS,T1.POSTED" _
               & " FROM TSPL_DAILY_ATTENDANCE T1 INNER JOIN TSPL_DAILY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE  T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE,T1.POSTED) AS T1  " & Environment.NewLine _
               & " UNION ALL " _
               & " SELECT T2.EMP_CODE,T2.TOTAL_DAYS,T2.PRESENT_DAYS,T2.HOLIDAYS_DAYS,T2.WEEKLY_OFF_DAYS,T2.LEAVE_DAYS,T2.ABSENT_DAYS,T1.POSTED,'MT' AS REGISTER_TYPE " _
               & " FROM TSPL_MONTHLY_ATTENDANCE T1 INNER JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL T2 ON T1.MTA_CODE=T2.MTA_CODE" _
               & " WHERE  T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T2 ON T1.EMP_CODE=T2.EMP_CODE  " _
               & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON T1.EMP_CODE=EMP.EMP_CODE " _
               & " LEFT JOIN TSPL_ATTENDANCE_MASTER T3 ON T1.ATTENDANCE_CODE=T3.ATTENDANCE_CODE WHERE T1.EMP_CODE IN " & strEmp & " "

        Dim dt_attendance As DataTable
        dt_attendance = clsDBFuncationality.GetDataTable(strq, trans)
        '' check for attendance unavailability
        Dim drNAAttd() As DataRow
        'drNAAttd = dt_attendance.Select("(PRESENT_DAYS+ABSENT_DAYS+LEAVE_DAYS+HOLIDAY_DAYS)<PAYPERIOD_DAYS OR REGISTER_TYPE IS NULL")
        drNAAttd = dt_attendance.Select("REGISTER_TYPE IS NULL ")
        'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'If (Me.ProgressBar1.Value + 5) > 100 Then
        '    Me.ProgressBar1.Value = 100
        'Else
        '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'End If
        If drNAAttd.Length > 0 Then
            'ProgressBar1.Text = "Generating log of employees without Attendance..."
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Employees not having the Attendance:")
            For intloop As Integer = 0 To drNAAttd.Length - 1
                objWriter.WriteLine((intloop + 1) & ". " & drNAAttd(intloop).Item("EMP_CODE") & "-" & drNAAttd(intloop).Item("EMP_NAME"))
            Next
            'Me.ProgressBar1.Text = "Finished...."
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
            'Me.ProgressBar1.Value = 100
            objWriter.Close()
            'clsCommon.ProgressBarHide()
            Throw New Exception("Some of the employee's attendance is not entered!")
            'Return False
            Exit Function
        End If

        '' CHECK FOR UNAPPROVED ALLOWANCES 
        'ProgressBar1.Text = "Checking unapproved allowances..."
        'strq = "SELECT COUNT(TSPL_ALLOWANCE.ALLOWANCE_CODE) AS TOTAL FROM TSPL_ALLOWANCE INNER JOIN TSPL_ALLOWANCE_DETAIL ON TSPL_ALLOWANCE.ALLOWANCE_CODE=TSPL_ALLOWANCE_DETAIL.ALLOWANCE_CODE WHERE TSPL_ALLOWANCE.POSTED=0 " _
        '& " AND TSPL_ALLOWANCE.PAY_PERIOD_CODE='" & Pay_Period_Code & "' AND TSPL_ALLOWANCE_DETAIL.EMP_CODE IN " & strEmp & ""
        'Dim dtAllowance As DataTable
        'dtAllowance = clsDBFuncationality.GetDataTable(strq)
        ''Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'If (Me.ProgressBar1.Value + 5) > 100 Then
        '    Me.ProgressBar1.Value = 100
        'Else
        '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'End If
        'If dtAllowance.Rows(0).Item("TOTAL") > 0 Then
        '    clsCommon.MyMessageBoxShow("Some of the allowance entries are Unapproved !")
        '    Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '    objWriter.WriteLine("Some Allowance entries are unapproved:")

        '    Me.ProgressBar1.Text = "Finished...."
        '    'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        '    Me.ProgressBar1.Value = 100
        '    objWriter.Close()
        '    Return False
        '    Exit Function
        'End If

        ' '' CHECK FOR UNAPPROVED DEDUCTIONS 
        'ProgressBar1.Text = "Checking unapproved deductions..."
        'strq = "SELECT COUNT(TSPL_DEDUCTION.DEDUCTION_CODE) AS TOTAL FROM TSPL_DEDUCTION INNER JOIN TSPL_DEDUCTION_DETAIL ON TSPL_DEDUCTION.DEDUCTION_CODE=TSPL_DEDUCTION_DETAIL.DEDUCTION_CODE  WHERE TSPL_DEDUCTION.POSTED=0 " _
        '& " AND TSPL_DEDUCTION.PAY_PERIOD_CODE='" & Pay_Period_Code & "' AND TSPL_DEDUCTION_DETAIL.EMP_CODE IN " & strEmp & ""
        'Dim dtDeduction As DataTable
        'dtDeduction = clsDBFuncationality.GetDataTable(strq)
        ''Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'If (Me.ProgressBar1.Value + 5) > 100 Then
        '    Me.ProgressBar1.Value = 100
        'Else
        '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'End If
        'If dtDeduction.Rows(0).Item("TOTAL") > 0 > 0 Then
        '    clsCommon.MyMessageBoxShow("Some of the deduction entries are Unapproved !")
        '    Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '    objWriter.WriteLine("Some Deduction entries are unapproved:")

        '    Me.ProgressBar1.Text = "Finished...."
        '    'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        '    Me.ProgressBar1.Value = 100
        '    objWriter.Close()
        '    Return False
        '    Exit Function
        'End If

        ' '' CHECK FOR UNAPPROVED BONUS 
        ''ProgressBar1.Text = "Checking unapproved bonus..."
        'clsCommon.ProgressBarUpdate("Checking unapproved bonus...")
        'strq = "SELECT COUNT(TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE) AS TOTAL FROM TSPL_EMPLOYEE_BONUS INNER JOIN TSPL_EMPBONUS_DETAIL ON TSPL_EMPLOYEE_BONUS.EMP_BONUS_CODE= TSPL_EMPBONUS_DETAIL.EMP_BONUS_CODE WHERE TSPL_EMPLOYEE_BONUS.POSTED=0 " _
        '& " AND TSPL_EMPLOYEE_BONUS.PAYABLE_PAY_PERIOD_CODE='" & Pay_Period_Code & "' AND TSPL_EMPBONUS_DETAIL.EMP_CODE IN " & strEmp & " "
        'Dim dtBonus As DataTable
        'dtBonus = clsDBFuncationality.GetDataTable(strq)

        'If dtBonus.Rows(0).Item("TOTAL") > 0 Then
        '    clsCommon.MyMessageBoxShow("Some of the bonus entries are Unapproved !")
        '    Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '    objWriter.WriteLine("Some bonus entries are unapproved:")
        '    objWriter.Close()
        '    'clsCommon.ProgressBarHide()
        '    Return False
        '    Exit Function
        'End If

        ' '' CHECK FOR UNAPPROVED ADJUSTMENTS 
        'clsCommon.ProgressBarUpdate("Checking unapproved Adjustment entries...")
        'strq = "SELECT COUNT(ADJUSTMENT_CODE) AS TOTAL FROM TSPL_ADJUSTMENT_VOUCHER WHERE POSTED=0 " _
        '& " AND PAY_PERIOD_CODE='" & Pay_Period_Code & "' AND EMP_CODE IN " & strEmp & ""
        'Dim dtAdjust As DataTable
        'dtAdjust = clsDBFuncationality.GetDataTable(strq)
        'If dtAdjust.Rows(0).Item("TOTAL") > 0 Then
        '    clsCommon.MyMessageBoxShow("Some of the adjustment entries are Unapproved !")
        '    Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '    objWriter.WriteLine("Some adjustment entries are unapproved:")
        '    objWriter.Close()
        '    'clsCommon.ProgressBarHide()
        '    Return False
        '    Exit Function
        'End If

        '' CHECK FOR UNAPPROVED OT 
        'ProgressBar1.Text = "Checking unapproved OT entries..."
        'strq = "SELECT COUNT(OT_SHEET_CODE) AS TOTAL FROM TSPL_OT_SHEET WHERE POSTED=0 " _
        '& " AND PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        'Dim dtOT As DataTable
        'dtOT = clsDBFuncationality.GetDataTable(strq)
        'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        'If dtOT.Rows.Count > 0 Then
        '    clsCommon.MyMessageBoxShow("Some of the OT entries are Unapproved !")
        '    Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '    objWriter.WriteLine("Some OT entries are unapproved:")

        '    Me.ProgressBar1.Text = "Finished...."
        '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
        '    Me.ProgressBar1.Value = 100
        '    objWriter.Close()
        '    Return False
        '    Exit Sub
        'End If

        ' '' CHECK FOR UNAPPROVED REIMBURSEMENT        
        'clsCommon.ProgressBarUpdate("Checking unapproved Adjustment entries...")
        'strq = "SELECT COUNT(REIMBURSEMENT_CODE) AS TOTAL FROM TSPL_EMP_REIMBURSEMENT WHERE POSTED=0 " _
        '& " AND PAY_PERIOD_CODE='" & Pay_Period_Code & "' AND EMP_CODE IN " & strEmp & ""
        'Dim dtREIM As DataTable
        'dtREIM = clsDBFuncationality.GetDataTable(strq)

        'If dtREIM.Rows(0).Item("TOTAL") > 0 Then
        '    clsCommon.MyMessageBoxShow("Some of the Reimbursement entries are Unapproved !")
        '    Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '    objWriter.WriteLine("Some Reimbursement entries are unapproved:")

        '    objWriter.Close()
        '    'clsCommon.ProgressBarHide()
        '    Return False
        '    Exit Function
        'End If

        ' '' CHECK FOR UNAPPROVED LOAN GENERATION 
        'clsCommon.ProgressBarUpdate("Checking unapproved LOAN Generation entries...")
        'strq = "SELECT COUNT(LOAN_CODE) AS TOTAL FROM TSPL_LOAN_APPLICATION WHERE POSTED=1 AND PAID=0 AND EMP_CODE IN " & strEmp & ""
        'Dim dtLOAN As DataTable
        'dtLOAN = clsDBFuncationality.GetDataTable(strq)
        'If dtLOAN.Rows(0).Item("TOTAL") > 0 Then
        '    strq = "SELECT COUNT(TSPL_LOAN_GENERATION.LOAN_GENERATION_CODE) AS TOTAL FROM TSPL_LOAN_GENERATION INNER JOIN TSPL_LOANGENERATION_DETAIL ON TSPL_LOAN_GENERATION.LOAN_GENERATION_CODE=TSPL_LOANGENERATION_DETAIL.LOAN_GENERATION_CODE WHERE TSPL_LOAN_GENERATION.POSTED=0 " _
        '& " AND TSPL_LOAN_GENERATION.PAY_PERIOD_CODE='" & Pay_Period_Code & "' AND TSPL_LOANGENERATION_DETAIL.EMP_CODE IN " & strEmp & ""
        '    Dim dtLoanGen As DataTable
        '    dtLoanGen = clsDBFuncationality.GetDataTable(strq)


        '    If dtLoanGen.Rows(0).Item("TOTAL") > 0 Then
        '        clsCommon.MyMessageBoxShow("Some of the Loan Generation entries are Unapproved !")
        '        Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '        objWriter.WriteLine("Some Loan Generation entries are unapproved:")


        '        objWriter.Close()
        '        'clsCommon.ProgressBarHide()
        '        Return False
        '        Exit Function
        '    End If

        '    '' loan adjustment
        '    strq = "SELECT COUNT(LOANADJUSTMENT_CODE) AS TOTAL FROM TSPL_LOAN_ADJUSTMENT WHERE POSTED=0  AND EMP_CODE IN " & strEmp & "" _
        '& " AND PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        '    Dim dtLoanGenAdj As DataTable
        '    dtLoanGenAdj = clsDBFuncationality.GetDataTable(strq)


        '    If dtLoanGenAdj.Rows(0).Item("TOTAL") > 0 Then
        '        clsCommon.MyMessageBoxShow("Some of the Loan Adjustment entries are Unapproved !")
        '        Dim objWriter As New System.IO.StreamWriter(logFile, True)
        '        objWriter.WriteLine("Some Loan Adjustment entries are unapproved:")
        '        objWriter.Close()
        '        'clsCommon.ProgressBarHide()
        '        Return False
        '        Exit Function
        '    End If
        'End If

        '' Saving Final attendance

        clsCommon.ProgressBarUpdate("Saving final attendance...")
        'strq = "DELETE FROM TSPL_ATTENDANCE_SUMMARY WHERE (SALARY_GENERATION_CODE='" & Me.txtCode.Value & "' OR SALARY_GENERATION_CODE IS NULL)"
        strq = "DELETE FROM TSPL_ATTENDANCE_SUMMARY WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "' "
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Error in saving attendance... :")

            objWriter.Close()
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in saving attendance... !")
            'Return False
            Exit Function
        End If


        strq = "INSERT INTO TSPL_ATTENDANCE_SUMMARY(PAY_PERIOD_CODE,EMP_CODE,REGISTER_TYPE,ATTENDANCE_CODE,CALC_SAL_ON,PAYPERIOD_DAYS," _
               & " PRESENT_DAYS,ABSENT_DAYS,LEAVE_DAYS,HOLIDAY_DAYS,WEEKLY_OFF_DAYS,PAYABLE_DAYS,LOP_DAYS,POSTED,Created_By,Created_Date,Modified_By,Modified_Date) " _
               & " (SELECT '" & Pay_Period_Code & "' AS PAY_PERIOD_CODE,T1.EMP_CODE,T2.REGISTER_TYPE,T1.ATTENDANCE_CODE,T3.CALC_SAL_ON," & condSalDays & " AS PAYPERIOD_DAYS, " _
               & " T2.PRESENT_DAYS,T2.ABSENT_DAYS,T2.LEAVE_DAYS,T2.HOLIDAY_DAYS,T2.WEEKLY_OFF_DAYS," & condPayableDays & " AS PAYABLE_DAYS, " _
               & " (" & condSalDays & "-" & condPayableDays & ") AS LOP_DAYS,1,'" & objCommonVar.CurrentUserCode & "',CURRENT_TIMESTAMP,'" & objCommonVar.CurrentUserCode & "',CURRENT_TIMESTAMP FROM ( " _
               & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS  FROM TSPL_EMPLOYEE_STATUS T1 " _
               & " INNER JOIN (  select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO " _
               & " FROM TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "' ) AS T2" _
               & " ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS T1" _
               & " LEFT JOIN ( " _
               & " SELECT T1.*,'HR' AS REGISTER_TYPE FROM (" _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS " _
               & " FROM TSPL_HOURLY_ATTENDANCE T1 INNER JOIN TSPL_HOURLY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE) AS T1" _
               & " UNION ALL " _
               & " SELECT T1.*,'DL' AS REGISTER_TYPE FROM ( " _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS" _
               & " FROM TSPL_DAILY_ATTENDANCE T1 INNER JOIN TSPL_DAILY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE) AS T1 " _
               & " UNION ALL " _
               & " SELECT T2.EMP_CODE,T2.TOTAL_DAYS,T2.PRESENT_DAYS,T2.HOLIDAYS_DAYS,T2.WEEKLY_OFF_DAYS,T2.LEAVE_DAYS,T2.ABSENT_DAYS,'MT' AS REGISTER_TYPE " _
               & " FROM TSPL_MONTHLY_ATTENDANCE T1 INNER JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL T2 ON T1.MTA_CODE=T2.MTA_CODE" _
               & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T2 ON T1.EMP_CODE=T2.EMP_CODE " _
               & " LEFT JOIN TSPL_ATTENDANCE_MASTER T3 ON T1.ATTENDANCE_CODE=T3.ATTENDANCE_CODE WHERE T1.EMP_CODE IN " & strEmp & "  )"

        Dim attStatus As Boolean
        attStatus = clsDBFuncationality.ExecuteNonQuery(strq, trans)
        If attStatus = True Then
            'ProgressBar1.Text = "Attendance saved..."
            'If (Me.ProgressBar1.Value + 5) > 100 Then
            '    Me.ProgressBar1.Value = 100
            'Else
            '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 5
            'End If
        Else
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Error in saving attendance... :")

            'Me.ProgressBar1.Text = "Logging Finished...."
            'Me.ProgressBar1.Value = 100
            objWriter.Close()
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in saving attendance... !")
            'Return False
            Exit Function
        End If
        '' Generating Salary....
        'ProgressBar1.Text = "Generating Salary..."
        ''"""""""""""""""""""""""""""""""""START OF SALARY GENERATION""""""""""""""""""""""""""""""""""

        '' STEPS 
        ' 1. FIND PAY PERIOD START DATE AND END DATE
        ' 2. FIND LATEST PF RULES IN DATAROW
        ' 3. FIND LATEST ESI RULES IN DATAROW
        ' 4. CREATE A TABLE FOR SALARY CALCULATION
        ' 5. TRUNCATE ABOVE SALARY CALCULATION  TABLE
        ' 6. INSERT SALARY HEADS, FORMULAS ETC INTO SALARY CALCULATION TABLE FROM EMPLOYEE SALARY TABLE
        ' 7. UPDATE ATTENDANCE BASED AND FIXED HEADS OF SALARY CALCULATION TABLE FROM EMPLOYEE SALARY TABLE
        ' 8. UPDATE ALLOWANCE AMOUNT FROM ALLOWANCE TABLE
        ' 9. UPDATE DEDUCTION AMOUNT FROM DEDUCTION TABLE
        ' 10.UPDATE BONUS AMOUNT FROM BONUS TABLE
        ' 11.UPDATE ADJUSTMENT IN PAY HEADS FROM ADJUSTMENT VOUCHER
        ' 12.UPDATE LOANS AMOUNT FROM LOAN GENERATION TABLE
        ' 13.UPDATE OT FROM OT SHHET TABLE
        ' 14.UPDATE REIMBURSEMENT AMOUNT FROM REIMBURSEMENT TABLE
        ' 15.START FOR LOOP FOR ALL PAY HEADS IN SALARY GENERATION TABLE ORDER BY LINE_NO FOR PAYHEADTYPE=F
        ' 16.REPLACE FORMULA WITH CORRESPONDING PAY HEADS AMOUNT AND CALCULATE AMOUNT
        ' 17.CALCULATE PF AMOUNTS BY APPLYING PF CALCULATION LOGIC
        ' 18.CALCULATE ESI AMOUNT BY APPLYING ESI CALCULATION LOGIC

        ''"""""""""""""""""""""""""""""""""END OF OF SALARY GENERATION""""""""""""""""""""""""""""""""""

        'Dim PP_END_DATE As Date
        Dim drPF As DataRow
        Dim drESI As DataRow
        Dim dtPT As DataTable


        ' 1. PP START DATE
        'ProgressBar1.Text = "Generating Salary...1. Pay Period Start Date"
        PP_START_DATE = objPP.DATE_FROM
        ' 2. PP END DATE
        'ProgressBar1.Text = "Generating Salary...1. Pay Period End Date"
        PP_END_DATE = objPP.DATE_TO

        'If (Me.ProgressBar1.Value + 1) > 100 Then
        '    Me.ProgressBar1.Value = 100
        'Else
        '    Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        'End If
        '' 3. LATEST PF RULES 
        'ProgressBar1.Text = "Generating Salary...3. PF Rules"
        clsCommon.ProgressBarUpdate("Gathering information regarding PF Rules...")
        strq = "SELECT T1.PFRULE_CODE,T2.COEPF_PER,T2.COEPF_ROUNDOFF_YPE,T2.COEPS_PER,T2.EPS_MAX,T2.EMPEPF_PER," _
              & " T2.EMPEPF_MAX,T2.EMPEPF_ROUNDOFF_YPE,T2.ACCOEPF_PER,T2.ACCOEPF_MAX,T2.COEDLI_PER,T2.ACCOEDLI_PER,T2.COEDLI_MAX," _
              & " T2.OC,T2.OC_MAX,T2.OTH_ROUNDOFF_YPE,ACCOEDLI_MAX FROM ( " _
              & " SELECT MAX(PFRULE_CODE) AS PFRULE_CODE FROM TSPL_PF_RULE_MASTER " _
              & " WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
              & " GROUP BY APPLICABLE_FROM) AS T1 LEFT JOIN TSPL_PF_RULE_MASTER T2 ON T1.PFRULE_CODE=T2.PFRULE_CODE"
        Dim dtPFRules As DataTable
        dtPFRules = clsDBFuncationality.GetDataTable(strq, trans)
        If dtPFRules.Rows.Count > 0 Then
            drPF = dtPFRules.Rows(dtPFRules.Rows.Count - 1)
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("PF Rules not found !")
            'Return False
            Exit Function
        End If


        '' 4. ESI RULES
        clsCommon.ProgressBarUpdate("Gathering information regarding ESI Rules...")
        strq = "SELECT T1.ESIRULE_CODE,T2.COESI_PER,T2.EMPESI_PER,T2.COESI_ROUNDOFF_YPE,T2.TOTALEARNING_MAX FROM (" _
               & " SELECT MAX(ESIRULE_CODE) AS ESIRULE_CODE FROM TSPL_ESI_RULE_MASTER " _
               & " WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
               & " GROUP BY APPLICABLE_FROM) AS T1 LEFT JOIN TSPL_ESI_RULE_MASTER T2 ON T1.ESIRULE_CODE=T2.ESIRULE_CODE"
        Dim dtEsiRules As DataTable
        dtEsiRules = clsDBFuncationality.GetDataTable(strq, trans)
        If dtEsiRules.Rows.Count > 0 Then
            drESI = dtEsiRules.Rows(dtEsiRules.Rows.Count - 1)
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("ESI Rules not found !")
            'Return False
            Exit Function
        End If

        '' 4. PROFF TAX RULES        
        clsCommon.ProgressBarUpdate("Gathering information regarding PT Rules...")
        strq = "SELECT T1.PT_CODE,T2.SLAB_FROM,T2.SLAB_TO,T2.PT_AMOUNT FROM ( " _
              & " SELECT MAX(PT_CODE) AS PT_CODE FROM TSPL_PT_RULE_MASTER " _
              & " WHERE APPLICABLE_FROM<='" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "' AND STATE_CODE=(SELECT STATE FROM TSPL_COMPANY_MASTER WHERE COMP_CODE='" & objCommonVar.CurrentCompanyCode & "') " _
              & " GROUP BY APPLICABLE_FROM) AS T1 LEFT JOIN TSPL_PT_DETAIL T2 ON T1.PT_CODE=T2.PT_CODE"


        dtPT = clsDBFuncationality.GetDataTable(strq, trans)
        If dtPT.Rows.Count > 0 Then

        Else
            'clsCommon.MyMessageBoxShow("PT Rules not found !")
            'Me.ProgressBar1.Value = 100
            'Return False
            'Exit Function
        End If

        '' TRUNCATE SALARY GENERATION TABLE
        'ProgressBar1.Text = "Generating Salary...5. Evacuate Salary Generation Table"
        clsCommon.ProgressBarUpdate("Saving raw salary calculation in temprary table for further calculation...")
        If Is_Arrear = False Then
            strq = "TRUNCATE TABLE " & Calc_Table & ""
            If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            Else
                'clsCommon.ProgressBarHide()
                Throw New Exception("Error in truncating Salary Calculation Table !")
                'Return False
                Exit Function
            End If
        End If

        '' INSERT EMPLOYEE WAISE SALARY STRUCTURE FROM EMPLOYEE SALARY TABLE

        'ProgressBar1.Text = "Generating Salary...6. Inserting salary structure of all employees"
        Dim QryArrear As String = "TSPL_EMPLOYEE_SALARY"
        If Is_Arrear Then
            QryArrear = " (select TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE,TSPL_EMPLOYEE_SALARY.EMP_CODE,TSPL_EMPLOYEE_SALARY.REVISION_NO,TSPL_EMPLOYEE_INCREMENT_HEAD.ARREAR_FROM as Applicable_From " &
                        " from TSPL_EMPLOYEE_INCREMENT_HEAD inner join TSPL_EMPLOYEE_SALARY on TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_SAL_CODE_NEW=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE)"
        End If

        strq = "INSERT INTO " & Calc_Table & " ( EMP_CODE, EMP_SAL_CODE,SALARY_STRUCTURE_CODE,ISHIDDENCOMPONENT,PAY_HEAD_CODE, LINE_NO, " _
            & " HEAD_TYPE,SUB_HEAD_TYPE,CALC_BASIS,PAYHEAD_FORMULA,RATE_AMOUNT,STD_AMOUNT,PAYPERIOD_DAYS, " _
            & " PRESENT_DAYS,ABSENT_DAYS,LEAVE_DAYS,HOLIDAY_DAYS,PAYABLE_DAYS,LOP_DAYS,IS_PF_APPL,IS_PF_ATTN_ENABLE, " _
            & " PF_MAX_LIM,EPS_TO_EPF,EPS_MAX,COEPF_ROUNDOFF_YPE,EMPEPF_ROUNDOFF_YPE,IS_ESI_APPL,FORMULA_HEAD," _
            & " IS_OT_APPL,OT_CODE,OT_HOURS,OT_RATE,HOUR_MULTIPLIER,IS_ASPER_ACTUAL_CALC,IS_BONUS_APPL,BONUS_CODE,ACTUAL_AMOUNT,PAYABLE_AMOUNT,ROUND_OFF_TYPE,MAX_AMOUNT,EMP_STATUS_CODE,ESI_MAX_LIM,EPF_RATE,ESI_RATE,PAY_PERIOD_CODE," _
            & " PF_Calculation_Type,PF_Rule_Max_Lim,Custom_PF_Max_Lim,PF_No,ESI_Calculation_Type,ESI_Rule_Max_Lim,Custom_ESI_Max_Lim,ESI_No,OD_Applicable " _
            & " )( " _
            & " SELECT DISTINCT(T1.EMP_CODE),T1.EMP_SAL_CODE,T1.SALARY_STRUCTURE_CODE,T2.ISHIDDENCOMPONENT,T3.PAY_HEAD_CODE,T3.LINE_NO,T4.HEAD_TYPE,T3.SUB_HEAD_TYPE," _
            & " T3.CALC_BASIS,T3.PAYHEAD_FORMULA, " _
            & " (CASE WHEN T4.HEAD_TYPE IN ('ATTN', 'FIXED') THEN T2.RATE_AMOUNT WHEN T4.HEAD_TYPE ='F' then T3.RATE_AMOUNT WHEN T4.HEAD_TYPE = 'UD' THEN COALESCE(T8.ALLOWANCE_AMOUNT,0) ELSE 0 End) AS RATE, " _
            & " (CASE WHEN T4.HEAD_TYPE IN ('ATTN', 'FIXED') THEN T2.RATE_AMOUNT WHEN T4.HEAD_TYPE = 'UD'  THEN COALESCE(T8.ALLOWANCE_AMOUNT,0) ELSE 0 End) AS STD_AMOUNT, " _
            & " T6.PAYPERIOD_DAYS,T6.PRESENT_DAYS,T6.ABSENT_DAYS,T6.LEAVE_DAYS,T6.HOLIDAY_DAYS,T6.PAYABLE_DAYS,T6.LOP_DAYS,T7.IS_PF_APPL, " _
            & " " + SettPFCalculationOnHeadValue + " AS IS_PF_ATTN_ENABLE,(CASE WHEN T7.Max_Amount_EPF>0  THEN T7.Max_Amount_EPF ELSE " & drPF.Item("EMPEPF_MAX") & " END) AS EPF_MAX_LIM,T7.EPS_TO_EPF," & drPF.Item("EPS_MAX") & " AS EPS_MAX,'" & drPF.Item("COEPF_ROUNDOFF_YPE") & "' AS COEPF_ROUNDOFF_YPE,'" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "' AS EMPEPF_ROUNDOFF_YPE, " _
            & " T7.IS_ESI_APPL,T3.PAYHEAD_FORMULA AS FORMULA_HEAD,T7.IS_OT_APPL,T7.OT_CODE,NULL AS OT_HOURS,NULL AS OT_RATE, " _
            & " COALESCE(T9.HOUR_MULTIPLIER,1) AS HOUR_MULTIPLIER ,COALESCE(T9.IS_ASPER_ACTUAL_CALC,0) AS IS_ASPER_ACTUAL_CALC,T7.IS_BONUS_APPL,T7.BONUS_CODE, " _
            & " (ROUND(CASE WHEN T4.HEAD_TYPE ='ATTN' THEN T2.RATE_AMOUNT  * T6.PAYABLE_DAYS / T6.PAYPERIOD_DAYS WHEN T4.HEAD_TYPE ='FIXED' THEN T2.RATE_AMOUNT WHEN T4.HEAD_TYPE = 'UD' THEN COALESCE(T8.ALLOWANCE_AMOUNT,0) ELSE 0 End ,3)) AS ACTUAL_AMOUNT, " _
            & " 0 AS PAYABLE_AMOUNT,T4.ROUND_OFF_TYPE,T2.MAX_AMOUNT,T7.EMP_STATUS_CODE,(CASE WHEN T7.Max_Amount_ESI>0 THEN T7.Max_Amount_ESI ELSE " & drESI.Item("TOTALEARNING_MAX") & " END) AS ESI_MAX_LIM,T7.EPF_RATE,T7.ESI_RATE,'" & Pay_Period_Code & "',T7.PF_Calculation_Type,T7.PF_Rule_Max_Lim,T7.Custom_PF_Max_Lim,T7.PF_No,T7.ESI_Calculation_Type,T7.ESI_Rule_Max_Lim,T7.Custom_ESI_Max_Lim,T7.ESI_No,T7.OD_Applicable FROM " _
            & " ( " _
            & " SELECT T1.EMP_SAL_CODE,T1.EMP_CODE,T1.SALARY_STRUCTURE_CODE,T1.APPLICABLE_FROM AS APP_DATE " _
            & " FROM TSPL_EMPLOYEE_SALARY T1 " _
            & " INNER JOIN ( " _
            & " SELECT EMP_SAL_CODE,EMPSAL.EMP_CODE,EMPSAL.REVISION_NO FROM TSPL_EMPLOYEE_SALARY INNER JOIN ( " _
            & " SELECT  MAX(REVISION_NO) AS REVISION_NO,EMP_CODE  FROM " & QryArrear & " AS  TSPL_EMPLOYEE_SALARY  WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "'  Group BY EMP_CODE " _
            & " HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "') AS EMPSAL ON TSPL_EMPLOYEE_SALARY.EMP_CODE=EMPSAL.EMP_CODE " _
            & " And EMPSAL.REVISION_NO = TSPL_EMPLOYEE_SALARY.REVISION_NO " _
            & " ) AS T2 ON T1.EMP_SAL_CODE = T2.EMP_SAL_CODE AND T1.EMP_CODE = T2.EMP_CODE) AS T1 " _
            & " LEFT JOIN TSPL_EMPLOYEE_SALARY_PAYHEADS T2 ON T1.EMP_SAL_CODE = T2.EMP_SAL_CODE " _
            & " LEFT JOIN TSPL_SALSTRUCT_PAYHEADS T3 ON T1.SALARY_STRUCTURE_CODE = T3.SALARY_STRUCTURE_CODE " _
            & " And T3.PAY_HEAD_CODE = T2.PAY_HEAD_CODE " _
            & " LEFT JOIN TSPL_PAYHEAD_MASTER T4 ON T3.PAY_HEAD_CODE = T4.PAY_HEAD_CODE " _
            & " LEFT JOIN (" _
            & " SELECT TAS.EMP_CODE,TAS.PAY_PERIOD_CODE,COALESCE(TAS.PRESENT_DAYS,0) as PRESENT_DAYS, " _
            & " TAS.PAYPERIOD_DAYS,TAS.ABSENT_DAYS,TAS.LEAVE_DAYS,TAS.HOLIDAY_DAYS, TAS.PAYABLE_DAYS, TAS.LOP_DAYS " _
            & " FROM TSPL_ATTENDANCE_SUMMARY AS TAS " _
            & " WHERE TAS.PAY_PERIOD_CODE = '" & Pay_Period_Code & "' " _
            & " ) AS T6 ON T1.EMP_CODE = T6.EMP_CODE " _
            & " LEFT JOIN ( " _
            & " SELECT T1.*, T2.IS_PF_APPL,COALESCE(T2.EPS_TO_EPF, 0) AS EPS_TO_EPF,IS_ESI_APPL,T2.IS_OT_APPL,T2.OT_CODE,T2.IS_BONUS_APPL,T2.BONUS_CODE,T2.WORKING_STATUS,T2.Max_Amount_EPF,T2.Max_Amount_ESI,T2.EPF_RATE,T2.ESI_RATE,T2.PF_Calculation_Type," & drPF.Item("EMPEPF_MAX") & " AS PF_Rule_Max_Lim,T2.Max_Amount_EPF AS Custom_PF_Max_Lim,T2.PF_No,'' as ESI_Calculation_Type," & drESI.Item("TOTALEARNING_MAX") & " AS ESI_Rule_Max_Lim,T2.Max_Amount_ESI AS Custom_ESI_Max_Lim,T2.ESI_No,T2.IS_OD_APPL OD_Applicable " _
            & " FROM " _
            & " ( " _
            & " SELECT EMP_STATUS_CODE,EMPSTATUS.EMP_CODE,EMPSTATUS.REVISION_NO FROM TSPL_EMPLOYEE_STATUS INNER JOIN ( " _
            & " SELECT  MAX(REVISION_NO) AS REVISION_NO,EMP_CODE  FROM TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "'   Group BY EMP_CODE " _
            & " HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "') AS EMPSTATUS ON TSPL_EMPLOYEE_STATUS.EMP_CODE=EMPSTATUS.EMP_CODE " _
            & " AND EMPSTATUS.REVISION_NO=TSPL_EMPLOYEE_STATUS.REVISION_NO " _
            & " ) AS T1 " _
            & " LEFT JOIN TSPL_EMPLOYEE_STATUS T2  ON T1.EMP_STATUS_CODE = T2.EMP_STATUS_CODE  " _
            & " INNER JOIN TSPL_EMPLOYEE_MASTER T_2 " _
            & " on T2.EMP_CODE=T_2.EMP_CODE ) AS T7 ON T1.EMP_CODE = T7.EMP_CODE " _
            & " LEFT JOIN (SELECT ALWD.* from TSPL_ALLOWANCE ALW INNER JOIN TSPL_ALLOWANCE_DETAIL ALWD " _
            & " ON ALW.ALLOWANCE_CODE=ALWD.ALLOWANCE_CODE WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "'  " _
            & " UNION ALL " _
            & " SELECT DND.* FROM TSPL_DEDUCTION DN INNER JOIN TSPL_DEDUCTION_DETAIL DND " _
            & " ON DN.DEDUCTION_CODE=DND.DEDUCTION_CODE WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "' ) T8 " _
            & " ON T1.EMP_CODE=T8.EMP_CODE AND T3.PAY_HEAD_CODE=T8.PAY_HEAD_CODE " _
            & " LEFT JOIN (select OT_CODE,HOUR_MULTIPLIER,OT_RATE as OT_RATE,IS_ASPER_ACTUAL_CALC from TSPL_OT_MASTER) AS  T9 ON T7.OT_CODE=T9.OT_CODE " _
            & " WHERE T3.PAY_HEAD_CODE IS NOT NULL AND T1.EMP_CODE IN " & strEmp & ") "

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in inserting Salary Calculation Table !")
            'Return False
            Exit Function
        End If

        ''adjustment IN PRINCIPAL AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in principal amount of pay heads...")
        strq = "UPDATE " & Calc_Table & " SET RATE_AMOUNT=RATE_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS,ACTUAL_AMOUNT=ACTUAL_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS, " _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and ADJUSTMENT_TYPE='PA' and TPH.HEAD_TYPE NOT IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & Calc_Table & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & Calc_Table & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Adjustment !")
            'Return False
            Exit Function
        End If

        ''adjustment IN ARREAR AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in ADJSTMENT IN ARREAR pay heads...")
        strq = "UPDATE " & Calc_Table & " SET RATE_AMOUNT=RATE_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS," _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS,ARREAR_AMT=T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and ADJUSTMENT_TYPE='AR' and TPH.HEAD_TYPE NOT IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & Calc_Table & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & Calc_Table & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Adjustment !")
            'Return False
            Exit Function
        End If

        '' UPDATE ACCOUNT_CODE OF "& Calc_Table &" for non loan advance payheads
        clsCommon.ProgressBarUpdate("updating GL accounts of non loan/advance pay heads...")
        strq = "update " & Calc_Table & " set " & Calc_Table & ".account_code=TSPL_PAYHEAD_MASTER.account_code,Employer_Account=TSPL_PAYHEAD_MASTER.GL_Employer_Account from TSPL_PAYHEAD_MASTER " &
               " where " & Calc_Table & ".PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in updating payhead gl account in Salary Calculation Table !")
            'Return False
            Exit Function
        End If

        '' UPDATE ACCOUNT_CODE OF "& Calc_Table &" for loan advance payheads
        clsCommon.ProgressBarUpdate("updating GL accounts of loa/advancen pay head...")
        strq = "update " & Calc_Table & " set " & Calc_Table & ".account_code=TSPL_EMPLOYEE_MASTER.ADVANCE_TO_STAFF from TSPL_EMPLOYEE_MASTER " &
               " where " & Calc_Table & ".EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE AND " & Calc_Table & ".SUB_HEAD_TYPE='Loan' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in updating advance gl account in Salary Calculation Table !")
            'Return False
            Exit Function
        End If

        '' update formula coluns of user defined pay heads
        strq = "UPDATE " & Calc_Table & " SET FORMULA_HEAD = '0',FORMULA_AMOUNT='0' WHERE HEAD_TYPE IN ('UD') and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Formula Pay Heads !")
            'Return False
            Exit Function
        End If


        '' UPDATE ATTENDANCE BASED AND FIXED PAY HEADS

        clsCommon.ProgressBarUpdate("updating attendance based and fixed pay heads...")
        strq = "UPDATE " & Calc_Table & "  SET RATE_AMOUNT=" _
        & " (CASE WHEN " & Calc_Table & ".SUB_HEAD_TYPE ='COPF'  THEN " & drPF.Item("COEPF_PER") & "" _
        & " WHEN " & Calc_Table & ".SUB_HEAD_TYPE ='COEPS'  THEN " & drPF.Item("COEPS_PER") & "" _
        & " WHEN " & Calc_Table & ".SUB_HEAD_TYPE ='EPF'  THEN (CASE WHEN EPF_RATE>0 THEN EPF_RATE ELSE " & drPF.Item("EMPEPF_PER") & " END) " _
        & " WHEN " & Calc_Table & ".SUB_HEAD_TYPE ='COESI'  THEN " & drESI.Item("COESI_PER") & "" _
        & " WHEN " & Calc_Table & ".SUB_HEAD_TYPE ='EMPESI'  THEN (CASE WHEN ESI_RATE>0 THEN ESI_RATE ELSE " & drESI.Item("EMPESI_PER") & " END) " _
        & " END), CoEPF_RATE_AC01=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("COEPF_PER") & " ELSE 0 END),CoEPS_RATE_AC10=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("COEPS_PER") & " ELSE 0 END),ADMIN_RATE_AC02=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("ACCOEPF_PER") & " ELSE 0 END)," _
        & " EDLI_RATE_AC21=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("COEDLI_PER") & " ELSE 0 END),ADMIN_EDLI_RATE_AC22=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("ACCOEDLI_PER") & " ELSE 0 END), " _
        & " OTHER_CHARGE=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("OC") & " ELSE 0 END),Co_ESI_RATE=(CASE WHEN SUB_HEAD_TYPE ='EMPESI' THEN " & drESI.Item("COESI_PER") & " ELSE 0 END) WHERE " & Calc_Table & ".SUB_HEAD_TYPE in ('COPF','COEPS','EPF','COESI','EMPESI') and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating attendance based and fixedpay heads !")
            'Return False
            Exit Function
        End If
        '' SECOND PART OF UPDATE ATTENDANCE BASED AND FIXED PAY HEADS
        strq = "UPDATE " & Calc_Table & "" _
              & " SET FORMULA_AMOUNT = CAST(RATE_AMOUNT AS VARCHAR) + '' + (" _
              & " CASE" _
              & " WHEN HEAD_TYPE = 'ATTN' THEN " _
              & " '*(' + CAST(COALESCE (PAYABLE_DAYS, 0) / COALESCE (PAYPERIOD_DAYS, 1) AS VARCHAR) + ')' " _
              & " ELSE '*1' End )" _
              & " WHERE HEAD_TYPE IN ('ATTN','FIXED') and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating attendance based and fixedpay heads !")
            'Return False
            Exit Function
        End If

        '' update FIXED PAY HEADS for nill attendance
        strq = "UPDATE " & Calc_Table & "" _
              & " SET Actual_Amount =0 " _
              & " WHERE HEAD_TYPE IN ('FIXED') and PAYABLE_DAYS=0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating fixedpay heads !")
            'Return False
            Exit Function
        End If


        '' loan calculation 
        '' generate loan

        clsCommon.ProgressBarUpdate("updating loan/advance pay heads...")
        strq = "UPDATE  " & Calc_Table & " SET ACTUAL_AMOUNT=0 WHERE SUB_HEAD_TYPE='LOAN' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Loan !")
            'Return False
            Exit Function
        End If

        strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=(T1.EMI+COALESCE(T2.ADJUSTMENT_PLUS,0)-COALESCE(T2.ADJUSTMENT_MINUS,0)) from ( " _
             & " select T2.EMP_CODE,T2.EMI_AMOUNT AS EMI FROM TSPL_LOAN_GENERATION T1  " _
             & " INNER JOIN TSPL_LOANGENERATION_DETAIL T2 ON T1.LOAN_GENERATION_CODE=T2.LOAN_GENERATION_CODE " _
             & " WHERE  T2.PAY_PERIOD_CODE='" & Pay_Period_Code & "' ) AS T1 " _
             & " LEFT JOIN (SELECT EMP_CODE,SUM(ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS," _
             & " SUM(ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS  FROM TSPL_LOAN_ADJUSTMENT " _
             & " WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "'  GROUP BY EMP_CODE) AS T2" _
             & " ON T1.EMP_CODE=T2.EMP_CODE" _
             & " WHERE " & Calc_Table & ".EMP_CODE=T1.EMP_CODE AND  SUB_HEAD_TYPE='LOAN' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Loan !")
            'Return False
            Exit Function
        End If
        '' bonus calculation/generation

        'ProgressBar1.Text = "Generating Salary...10.  calculating bonus"
        clsCommon.ProgressBarUpdate("updating Bonus pay heads...")
        strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT =0 WHERE SUB_HEAD_TYPE='BONUS' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Bonus !")
            'Return False
            Exit Function
        End If

        strq = "UPDATE  " & Calc_Table & " SET ACTUAL_AMOUNT=T1.BONUS_AMOUNT,BONUS_CODE=T1.BONUS_CODE," _
             & " BONUS_FROM_PAY_PERIOD_CODE=T1.FROM_PAY_PERIOD_CODE,BONUS_TO_PAY_PERIOD_CODE=T1.TO_PAY_PERIOD_CODE  " _
             & " FROM (SELECT T1.FROM_PAY_PERIOD_CODE,T1.TO_PAY_PERIOD_CODE,T2.EMP_CODE,T2.BONUS_AMOUNT," _
             & " T2.BONUS_CODE FROM TSPL_EMPLOYEE_BONUS T1  " _
             & " INNER JOIN TSPL_EMPBONUS_DETAIL T2 ON T1.EMP_BONUS_CODE=T2.EMP_BONUS_CODE" _
             & " WHERE  T1.PAYABLE_PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T1 " _
             & " WHERE " & Calc_Table & ".EMP_CODE=T1.EMP_CODE AND  SUB_HEAD_TYPE='BONUS' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Bonus !")
            'Return False
            Exit Function
        End If

        '' REIMBURSEMENT AMOUNT CALCULATION 
        'ProgressBar1.Text = "Generating Salary...10.  calculating bonus"
        clsCommon.ProgressBarUpdate("updating reimbursement pay heads...")
        strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=T1.REIMBURSEMENT_AMOUNT FROM " _
        & " (select T1.EMP_CODE,T2.PAY_HEAD_CODE,SUM(COALESCE(T2.REIMBURSEMENT_AMOUNT,0)) AS REIMBURSEMENT_AMOUNT  " _
        & " from TSPL_EMP_REIMBURSEMENT T1 " _
        & " INNER JOIN  TSPL_EMPREIMBURSEMENT_DETAIL T2 " _
        & " ON T1.REIMBURSEMENT_CODE=T2.REIMBURSEMENT_CODE " _
        & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' " _
        & " GROUP BY T1.EMP_CODE,T2.PAY_HEAD_CODE) AS T1" _
        & " WHERE(" & Calc_Table & ".EMP_CODE = T1.EMP_CODE) " _
        & " AND " & Calc_Table & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Reimbursement !")
            'Return False
            Exit Function
        End If


        '' update formula coluns of user defined pay heads
        clsCommon.ProgressBarUpdate("updating user defined pay heads...")
        strq = "UPDATE " & Calc_Table & " SET FORMULA_HEAD = RATE_AMOUNT,FORMULA_AMOUNT=RATE_AMOUNT WHERE HEAD_TYPE IN ('UD') and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Formula Pay Heads !")
            'Return False
            Exit Function
        End If


        '' LOOP QUERY FOR EACH PAY HEADS
        'ProgressBar1.Text = "Generating Salary...8. Update formula based pay heads"

        If Is_Arrear = False Then
            clsCommon.ProgressBarUpdate("updating formula based pay heads...")
        Else
            clsCommon.ProgressBarUpdate("updating Arrears(" & Pay_Period_Code & ")...")
        End If
        strq = "SELECT DISTINCT LINE_NO FROM " & Calc_Table & " WHERE LINE_NO IS NOT NULL and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "' ORDER BY LINE_NO"
        Dim dtSeq As DataTable
        dtSeq = clsDBFuncationality.GetDataTable(strq, trans)
        For Each drSeq As DataRow In dtSeq.Rows
            strq = "UPDATE " & Calc_Table & " " _
                    & " SET PAYHEAD_FORMULA = REPLACE(PAYHEAD_FORMULA,'[' + T5.PAY_HEAD_CODE + ']',COALESCE(T5.FORMULA_AMOUNT, '0')), " _
                    & " FORMULA_AMOUNT = '(' + REPLACE(PAYHEAD_FORMULA,'[' + T5.PAY_HEAD_CODE + ']',COALESCE(T5.FORMULA_AMOUNT, '0')) + ')*(' + CAST(RATE_AMOUNT as VARCHAR(10)) + '/100.00)' + ( " _
                    & " CASE " _
                    & " WHEN HEAD_TYPE = 'ATTN' THEN " _
                    & " '*(' + CAST(COALESCE(PAYABLE_DAYS, 0) / CAST(COALESCE (PAYPERIOD_DAYS, 1) AS FLOAT) AS VARCHAR(200)) + ')' " _
                    & " ELSE " _
                    & " '*1' End), " _
                    & " FORMULA_HEAD = REPLACE ( " _
                    & " FORMULA_HEAD, " _
                    & " '[' + T5.PAY_HEAD_CODE + ']', " _
                    & " CAST(COALESCE (T5.STD_AMOUNT, '0') AS VARCHAR(200))) " _
                    & " FROM " _
                    & " ( " _
                    & " SELECT EMP_CODE,FORMULA_AMOUNT,PAY_HEAD_CODE,LINE_NO,ACTUAL_AMOUNT,STD_AMOUNT " _
                    & " FROM " & Calc_Table & " " _
                    & " WHERE LINE_NO = " & drSeq.Item("LINE_NO") & " and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "' " _
                    & " ) AS T5 " _
                    & " WHERE " & Calc_Table & ".EMP_CODE = T5.EMP_CODE AND " & Calc_Table & ".LINE_NO > " & drSeq.Item("LINE_NO") & "" _
                    & " AND " & Calc_Table & ".HEAD_TYPE NOT IN ('ATTN', 'FIXED','UD') and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
            If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

            Else
                'clsCommon.ProgressBarHide()
                Throw New Exception("Error in Updating Formula Pay Heads !")
                'Return False
                Exit Function
            End If
        Next
        strq = "UPDATE " & Calc_Table & " SET PAYHEAD_FORMULA = '0' WHERE (LTRIM(PAYHEAD_FORMULA) = '' OR PAYHEAD_FORMULA IS NULL) and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Formula Pay Heads !")
            'Return False
            Exit Function
        End If
        strq = "UPDATE " & Calc_Table & " SET FORMULA_HEAD = '0' WHERE (LTRIM (FORMULA_HEAD) = '' OR FORMULA_HEAD IS NULL) and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            Throw New Exception("Error in Updating Formula Pay Heads !")
            'Return False
            Exit Function
        End If


        Dim dtSal As DataTable
        dtSal = clsDBFuncationality.GetDataTable("select * from " & Calc_Table & " WHERE LINE_NO IS NOT NULL and HEAD_TYPE='F' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "' ORDER BY SALARY_CALCULATION_CODE", trans)
        For Each drSal As DataRow In dtSal.Rows
            strq = "UPDATE " & Calc_Table & " SET FORMULA_VALUE = (select " & drSal.Item("FORMULA_AMOUNT") & ")," _
            & " HEAD_VALUE=round((select " & drSal.Item("PAYHEAD_FORMULA") & "),0) where SALARY_CALCULATION_CODE= " & drSal.Item("SALARY_CALCULATION_CODE") & ""

            If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            Else
                'clsCommon.ProgressBarHide()
                Throw New Exception("Error in Updating Formula Pay Heads !")
                'Return False
                Exit Function
            End If

        Next

        strq = "UPDATE " & Calc_Table & " SET FORMULA_AMOUNT=0 where (COALESCE(FORMULA_AMOUNT,'')='' or FORMULA_AMOUNT='') and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Formula Pay Heads !")
            'Return False
            Exit Function
        End If

        '' UPDATE PF_MAX_LIM ON THE BASIS OF PF_CALCULATION_TYPE FROM EMPLOYEE STATUS.
        '' done by panch raj against ticket No:BM00000008481
        strq = "UPDATE " & Calc_Table & " SET PF_MAX_LIM=(CASE WHEN PF_Calculation_Type='PR' THEN PF_Rule_Max_Lim WHEN PF_Calculation_Type='FA' THEN HEAD_VALUE WHEN PF_Calculation_Type='C' THEN  Custom_PF_Max_Lim ELSE  PF_Rule_Max_Lim END) where SUB_HEAD_TYPE='EPF' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating PF Max Limit !")
            'Return False
            Exit Function
        End If


        'ProgressBar1.Text = "Generating Salary...9. Update calculating pf and eps"
        clsCommon.ProgressBarUpdate("updating PF...")
        ''new query
        strq = "UPDATE " & Calc_Table & "" _
        & " SET ACTUAL_AMOUNT = ( " _
        & " CASE" _
        & " WHEN SUB_HEAD_TYPE IN ('COPF', 'COEPS', 'EPF') THEN " _
        & " ( " _
        & " ( " _
        & " CASE " _
        & " WHEN IS_PF_APPL = 1 THEN " _
        & " ( " _
        & " CASE " _
        & " WHEN SUB_HEAD_TYPE = 'EPF' THEN " +
        "( " +
"(CASE WHEN PF_MAX_LIM >0 THEN " +
 "(CASE WHEN IS_PF_ATTN_ENABLE = 0 THEN (CASE WHEN (case when PAYABLE_DAYS=0 then 0 else   HEAD_VALUE*PAYPERIOD_DAYS/PAYABLE_DAYS end  ) > PF_MAX_LIM THEN PF_MAX_LIM  ELSE (case when PAYABLE_DAYS=0 then 0 else HEAD_VALUE*PAYPERIOD_DAYS/PAYABLE_DAYS end) End * (PAYABLE_DAYS / PAYPERIOD_DAYS))" +
 "ELSE " +
 "(CASE WHEN HEAD_VALUE > PF_MAX_LIM THEN ((PF_MAX_LIM * PAYABLE_DAYS) / PAYPERIOD_DAYS ) ELSE ( (HEAD_VALUE))End)End)" +
"ELSE" +
"(CASE WHEN IS_PF_ATTN_ENABLE = 0 THEN FORMULA_VALUE ELSE ((CASE WHEN HEAD_VALUE > PF_MAX_LIM THEN ((HEAD_VALUE))ELSE((HEAD_VALUE))End))End)End)" +
") * RATE_AMOUNT / 100 " _
        & " WHEN SUB_HEAD_TYPE IN ('COPF', 'COEPS') THEN " _
        & " ( " _
        & " CASE " _
        & " WHEN EPS_TO_EPF = 1 THEN " _
        & " 	(  " _
        & " 	CASE " _
        & " 	WHEN SUB_HEAD_TYPE = 'COPF' THEN " _
        & " 	( " _
        & " 	( " _
        & " 		CASE " _
        & " 		WHEN PF_MAX_LIM>0 THEN " _
        & " 		( " _
        & " 		CASE " _
        & " 	    WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
        & "      PF_MAX_LIM " _
        & " 		ELSE " _
        & "      FORMULA_VALUE " _
        & "      End " _
        & " 		) " _
        & " 	ELSE " _
        & " (FORMULA_VALUE) " _
        & "  End " _
        & " 			) " _
        & " 			) * RATE_AMOUNT / 100 " _
        & " 	ELSE " _
        & "      0 " _
        & "      End " _
        & " 							) " _
        & " 		ELSE " _
        & " 		( " _
        & " 	    CASE " _
        & " 		WHEN SUB_HEAD_TYPE = 'COPF' THEN " _
        & " 	    ( " _
        & " 		CASE " _
        & " 	    WHEN ( " _
        & " 		( " _
        & " 		( " _
        & " 		( " _
        & " 		CASE " _
        & " 	    WHEN PF_MAX_LIM>0  THEN " _
        & " 		( " _
        & " 		CASE " _
        & " 		WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
        & "      PF_MAX_LIM " _
        & " 		ELSE " _
        & "      FORMULA_VALUE " _
        & "      End " _
        & " 		) " _
        & " 		ELSE " _
        & " 		(FORMULA_VALUE) " _
        & "      End " _
        & " 		) " _
        & " 		) " _
        & " 		) * " & drPF.Item("EMPEPF_PER") & " / 100 " _
        & " 		) > (PF_MAX_LIM * " & drPF.Item("EMPEPF_PER") & " / 100) THEN " _
        & " 	    ((PF_MAX_LIM * " & drPF.Item("EMPEPF_PER") & ") / 100) " _
        & " 		ELSE " _
        & " 		( " _
        & " 	    ( " _
        & " 	    ( " _
        & " 		( " _
        & " 		CASE " _
        & " 	    WHEN PF_MAX_LIM>0 THEN " _
        & " 		( " _
        & " 		CASE " _
        & " 		WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
        & "      PF_MAX_LIM " _
        & " 		ELSE " _
        & "      FORMULA_VALUE " _
        & "      End " _
        & " 	 	) " _
        & " 		ELSE " _
        & " 	    (FORMULA_VALUE) " _
        & "      End " _
        & " 	    ) " _
        & " 		) " _
        & " 	    ) * " & drPF.Item("EMPEPF_PER") & " / 100 " _
        & " 	    ) " _
        & "      End " _
        & " 		) " _
        & " 		ELSE " _
        & "      0 " _
        & "      End " _
        & " 		) " _
        & "      End " _
        & " 	    ) " _
        & " 			ELSE " _
        & " 			( " _
        & " 			( " _
        & " 	 		CASE " _
        & " 			WHEN PF_MAX_LIM>0 THEN " _
        & " 			( " _
        & " 			CASE " _
        & " 		    WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
        & "          PF_MAX_LIM " _
        & " 			ELSE " _
        & "         FORMULA_VALUE " _
        & "         End " _
        & " 		   ) " _
        & " 		  ELSE " _
        & " 		(FORMULA_VALUE) " _
        & "      End " _
        & " 	    ) " _
        & "   	) * RATE_AMOUNT / 100 " _
        & "      End " _
        & " 		) " _
        & " 	   ELSE " _
        & "     0 " _
        & "     End " _
        & " 	   ) " _
        & " 	   ) " _
        & "     ELSE " _
        & " 	   ( " _
        & " 	   COALESCE ( " _
        & " 	   FORMULA_VALUE, " _
        & "     '0' " _
        & " 	   ) " _
        & "     ) " _
        & "     End " _
        & "     ) " _
        & "     WHERE " _
        & "     COALESCE (FORMULA_AMOUNT, '') <> '' " _
        & "     AND LINE_NO IS NOT NULL  and HEAD_TYPE='F' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Formula Pay Heads !")
            'Return False
            Exit Function
        End If

        clsCommon.ProgressBarUpdate("updating EPS...")
        strq = "UPDATE " & Calc_Table & " SET" _
  & " ACTUAL_AMOUNT = ROUND(T1.EPS, 2) " _
  & " FROM " _
  & " ( " _
  & " SELECT " _
  & " T1.EMP_CODE, " _
  & " ( " _
  & " COALESCE (T2.EPF, 0) - COALESCE (T3.COPF, 0) " _
  & " ) AS EPS " _
  & " FROM " _
  & " " & Calc_Table & " T1 " _
  & " LEFT JOIN ( " _
  & " SELECT " _
  & " EMP_CODE, " _
  & " COALESCE (SUM(ACTUAL_AMOUNT), 0) AS EPF " _
  & " FROM " _
  & " " & Calc_Table & " " _
  & " WHERE " _
  & " SUB_HEAD_TYPE = 'EPF' " _
  & " Group BY " _
  & " EMP_CODE " _
  & " ) AS T2 ON T1.EMP_CODE = T2.EMP_CODE " _
  & " LEFT JOIN ( " _
  & " SELECT " _
  & " EMP_CODE, " _
  & " COALESCE (SUM(ACTUAL_AMOUNT), 0) AS COPF " _
  & " FROM " _
  & " " & Calc_Table & " " _
  & " WHERE " _
  & " SUB_HEAD_TYPE = 'COPF' " _
  & " Group BY " _
  & " EMP_CODE " _
  & " ) AS T3 ON T1.EMP_CODE = T3.EMP_CODE " _
  & " ) AS T1 " _
  & " WHERE " _
  & " " & Calc_Table & ".EMP_CODE = T1.EMP_CODE" _
  & " AND " & Calc_Table & ".SUB_HEAD_TYPE = 'COEPS' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating EPS !")
            'Return False
            Exit Function
        End If

        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN ((" & drPF.Item("EMPEPF_MAX") & ")*PAYABLE_DAYS/PAYPERIOD_DAYS) ELSE HEAD_VALUE END) *CoEPS_RATE_AC10/100) " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Employer EPS Account !")
            'Return False
            Exit Function
        End If

        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=0 " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0  and CoEPS_AMT_AC10<0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Employer EPS Account !")
            'Return False
            Exit Function
        End If
        '' apply max amount on employer accounts(COEPS)

        strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=(case when " & drPF.Item("EPS_MAX") & ">0 AND CoEPS_AMT_AC10> " & drPF.Item("EPS_MAX") & " THEN " & drPF.Item("EPS_MAX") & " ELSE   CoEPS_AMT_AC10 END) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Applying Maximum of Employer EPS Account !")
            'Return False
            Exit Function
        End If

        ' '' ROUND OFF COEPS
        ' If Is_Arrear = False Then
        '     clsCommon.ProgressBarUpdate("Rounding off Employer Accounts...")
        '     strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(CoEPS_AMT_AC10,0) " _
        '    & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(CoEPS_AMT_AC10) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(CoEPS_AMT_AC10) END), " _
        '    & " CoEPS_AMT_AC10_ROUND_OFF=(CoEPS_AMT_AC10-(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(CoEPS_AMT_AC10,0) " _
        '    & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(CoEPS_AMT_AC10) WHEN ROUND_OFF_TYPE='U' THEN CEILING(CoEPS_AMT_AC10) END))" _
        '    & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        '     If clsDBFuncationality.ExecuteNonQuery(strq) = True Then

        '     Else
        '         clsCommon.MyMessageBoxShow("Error in Updating Rounding off Employer EPS account!")
        '         'clsCommon.ProgressBarHide()
        '         Return False
        '         Exit Function
        '     End If
        ' End If


        ' '' UPDATE EMPLOYER SHARE ACCOUNTS -COEPF
        ' Dim EPF As String = "((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN ((" & drPF.Item("EMPEPF_MAX") & ")*PAYABLE_DAYS/PAYPERIOD_DAYS) ELSE HEAD_VALUE END)*(CoEPF_RATE_AC01+CoEPS_RATE_AC10)/100)"
        ' strq = "UPDATE " & Calc_Table & " SET CoEPF_AMT_AC01=((CASE WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(" & EPF & ",0) " _
        '       & " WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(" & EPF & ") WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(" & EPF & ") END)-CoEPS_AMT_AC10), " _
        '       & " CoEPF_AMT_AC01_ROUND_OFF=-CoEPS_AMT_AC10_ROUND_OFF" _
        '       & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        ' If clsDBFuncationality.ExecuteNonQuery(strq) = True Then
        ' Else
        '     clsCommon.MyMessageBoxShow("Error in Updating Employer PF Accounts !")
        '     'clsCommon.ProgressBarHide()
        '     Return False
        '     Exit Function
        ' End If

        ' '' update other admin accounts
        ' strq = "UPDATE " & Calc_Table & " SET " _
        '   & " ADMIN_AMT_AC02=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END) * ADMIN_RATE_AC02/100),EDLI_AMT_AC21=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END)*EDLI_RATE_AC21/100), " _
        '   & " ADMIN_EDLI_AMT_AC22=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END) * ADMIN_EDLI_RATE_AC22/100) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        ' If clsDBFuncationality.ExecuteNonQuery(strq) = True Then
        ' Else
        '     clsCommon.MyMessageBoxShow("Error in Updating Employer admin Accounts !")
        '     'clsCommon.ProgressBarHide()
        '     Return False
        '     Exit Function
        ' End If


        ' ''''''''''''''''''''''''''''''''
        ' '' apply max amount on other employer accounts

        ' strq = "UPDATE " & Calc_Table & " SET " _
        '     & " ADMIN_AMT_AC02=(case when " & drPF.Item("ACCOEPF_MAX") & ">0 AND ADMIN_AMT_AC02> " & drPF.Item("ACCOEPF_MAX") & " THEN " & drPF.Item("ACCOEPF_MAX") & " ELSE   ADMIN_AMT_AC02 END),EDLI_AMT_AC21=(case when " & drPF.Item("COEDLI_MAX") & ">0 AND EDLI_AMT_AC21> " & drPF.Item("COEDLI_MAX") & " THEN " & drPF.Item("COEDLI_MAX") & " ELSE   EDLI_AMT_AC21 END), " _
        '     & " ADMIN_EDLI_AMT_AC22=(case when " & drPF.Item("ACCOEDLI_MAX") & ">0 AND ADMIN_EDLI_AMT_AC22> " & drPF.Item("ACCOEDLI_MAX") & " THEN " & drPF.Item("ACCOEDLI_MAX") & " ELSE   ADMIN_EDLI_AMT_AC22 END) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        ' If clsDBFuncationality.ExecuteNonQuery(strq) = True Then
        '     'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        ' Else
        '     clsCommon.MyMessageBoxShow("Error in Applying Maximum of Employer PF Accounts !")
        '     'clsCommon.ProgressBarHide()
        '     Return False
        '     Exit Function
        ' End If

        ' strq = "UPDATE " & Calc_Table & " SET " _
        '& " ADMIN_AMT_AC02=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(ADMIN_AMT_AC02,0) " _
        '& " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(ADMIN_AMT_AC02) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(ADMIN_AMT_AC02) END)," _
        '& " EDLI_AMT_AC21=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(EDLI_AMT_AC21,0) " _
        '& " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(EDLI_AMT_AC21) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(EDLI_AMT_AC21) END)," _
        '& " ADMIN_EDLI_AMT_AC22=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(ADMIN_EDLI_AMT_AC22,0) " _
        '& " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(ADMIN_EDLI_AMT_AC22) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(ADMIN_EDLI_AMT_AC22) END) " _
        '& " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        ' If clsDBFuncationality.ExecuteNonQuery(strq) = True Then
        '     'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        ' Else
        '     clsCommon.MyMessageBoxShow("Error in Updating Rounding off Employer PF accounts!")
        '     'clsCommon.ProgressBarHide()
        '     Return False
        '     Exit Function
        ' End If
        If Is_Arrear = False Then
            clsCommon.ProgressBarUpdate("updating ESI...")
        Else
            clsCommon.ProgressBarUpdate("updating Arrears(" & Pay_Period_Code & ")...")
        End If

        '' UPDATE PREV_ESI IN "& Calc_Table &"
        strq = "UPDATE " & Calc_Table & " SET PREV_ESI=FINAL.ACTUAL_AMOUNT FROM " &
               " ( " &
               " SELECT TAB.SALARY_GENERATION_CODE,TAB.PAY_PERIOD_CODE, " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE,TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE, " &
               " TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT FROM ( " &
               " SELECT TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE FROM TSPL_GENERATE_SALARY INNER JOIN TSPL_PAYPERIOD_MASTER " &
               " ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE " &
               " WHERE TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE IN  " &
               " (SELECT PAY_PERIOD_CODE FROM TSPL_PAYPERIOD_MASTER WHERE DATE_FROM='" & clsCommon.GetPrintDate(PP_END_DATE.AddMonths(-1), "dd/MMM/yyyy") & "')) TAB " &
               " LEFT JOIN TSPL_GENERATE_SALARY_PAYHEADS " &
               " ON TAB.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
               " WHERE TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE IN ('EMPESI','COESI')) FINAL " &
               " WHERE " & Calc_Table & ".EMP_CODE=FINAL.EMP_CODE " &
               " AND " & Calc_Table & ".PAY_HEAD_CODE=FINAL.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Previous ESI !")
            'Return False
            Exit Function
        End If
        Dim Arrear As Integer = 0
        If Is_Arrear Then
            Arrear = 1
        Else
            Arrear = 0
        End If
        'strq = "UPDATE " & Calc_Table & "" _
        '       & " SET ACTUAL_AMOUNT = ( " _
        '       & " CASE " _
        '       & " WHEN IS_ESI_APPL = 1 " _
        '       & " AND HEAD_VALUE > ESI_MAX_LIM and " & Arrear & "=0 AND " & PP_START_DATE.Month & " IN (4,10) THEN " _
        '       & " 0 " _
        '       & " WHEN IS_ESI_APPL = 1 " _
        '       & " AND HEAD_VALUE > ESI_MAX_LIM AND COALESCE(PREV_ESI,0)<=0 and " & Arrear & "=0 THEN " _
        '       & " 0 " _
        '       & " WHEN IS_ESI_APPL = 0 THEN " _
        '       & " 0 " _
        '       & " WHEN HEAD_VALUE > ESI_MAX_LIM THEN " _
        '       & " ESI_MAX_LIM*RATE_AMOUNT/100  " _
        '       & " ELSE " _
        '       & " ACTUAL_AMOUNT " _
        '       & " End " _
        '       & " ) " _
        '       & " WHERE " _
        '       & " SUB_HEAD_TYPE IN ('COESI', 'EMPESI') "

        strq = "UPDATE " & Calc_Table & "" _
              & " SET ACTUAL_AMOUNT = ( " _
              & " CASE " _
              & " WHEN IS_ESI_APPL = 1 " _
              & " AND HEAD_VALUE > ESI_MAX_LIM and " & Arrear & "=0 AND " & PP_END_DATE.Month & " IN (4,10) THEN " _
              & " 0 " _
              & " WHEN IS_ESI_APPL = 1 " _
              & " AND HEAD_VALUE > ESI_MAX_LIM AND COALESCE(PREV_ESI,0)<=0 and " & Arrear & "=0 THEN " _
              & " 0 " _
              & " WHEN IS_ESI_APPL = 0 THEN " _
              & " 0 " _
              & " ELSE " _
              & " ACTUAL_AMOUNT " _
              & " End " _
              & " ) " _
              & " WHERE " _
              & " SUB_HEAD_TYPE IN ('COESI', 'EMPESI') and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating EPS !")
            'Return False
            Exit Function
        End If
        '' UPDATE EMPLOYER ESI 
        strq = " UPDATE " & Calc_Table & " SET Co_ESI_AMT=(HEAD_VALUE*Co_ESI_RATE/100) WHERE SUB_HEAD_TYPE ='EMPESI' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating EMPLOYER ESI ACCOUNT !")
            'Return False
            Exit Function
        End If

        '' ROUND OFF EMPLOYER ESI 
        strq = "UPDATE " & Calc_Table & " SET Co_ESI_AMT=(CASE WHEN '" & drESI.Item("COESI_ROUNDOFF_YPE") & "'='R' THEN ROUND(Co_ESI_AMT,2) " _
       & " WHEN '" & drESI.Item("COESI_ROUNDOFF_YPE") & "'='L' THEN  ROUND(Co_ESI_AMT,2) WHEN '" & drESI.Item("COESI_ROUNDOFF_YPE") & "'='U' THEN ROUND(Co_ESI_AMT,2) END) " _
       & " WHERE SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Rounding off Employer ESI account!")
            'Return False
            Exit Function
        End If




        clsCommon.ProgressBarUpdate("updating PT...")
        '' PROFF TAX CALCULATION
        If dtPT.Rows.Count > 0 Then
            strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=(" & GetQueryPT(dtPT) & ") WHERE SUB_HEAD_TYPE='PT' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "';"

            If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
                'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
            Else
                'clsCommon.ProgressBarHide()
                Throw New Exception("Error in Updating Proff Tax !")
                'Return False
                Exit Function
            End If
        End If

        '' Mediclaim CALCULATION
        clsCommon.ProgressBarUpdate("updating Mediclaim...")
        strq = " UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=mediclim.Total_Amount " &
               " from (select EMP_CODE,SUM(Total_Amount) AS Total_Amount from TSPL_MEDICLAIM_HEAD " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY EMP_CODE) as mediclim " &
               " where " & Calc_Table & ".EMP_CODE=mediclim.EMP_CODE AND " & Calc_Table & ".SUB_HEAD_TYPE='Mediclaim' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        Dim STRQ1 As String
        STRQ1 = "update TSPL_MEDICLAIM_HEAD set PAY_PERIOD_CODE='" & Pay_Period_Code & "' where PAY_PERIOD_CODE is null"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Mediclaim !")
            'Return False
            Exit Function
        End If

        '' Gratuity CALCULATION
        clsCommon.ProgressBarUpdate("updating Gratuity...")
        strq = " UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=GRATUITY.Total_Amount " &
               " from (select EMP_CODE,SUM(GRATUITYAMT) AS Total_Amount from TSPL_GRATUITY  " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY EMP_CODE) as GRATUITY " &
               " where " & Calc_Table & ".EMP_CODE=GRATUITY.EMP_CODE AND " & Calc_Table & ".SUB_HEAD_TYPE='Gratuity' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        STRQ1 = "update TSPL_GRATUITY set PAY_PERIOD_CODE='" & Pay_Period_Code & "' where PAY_PERIOD_CODE is null"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating GRATUITY !")
            'Return False
            Exit Function
        End If

        '' LTA CALCULATION
        clsCommon.ProgressBarUpdate("updating LTA...")
        strq = " UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=LTA.Total_Amount " &
               " from (select EMP_CODE,SUM(Claim_Amount) AS Total_Amount from TSPL_LTA_Claim_Head   " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY EMP_CODE) as LTA " &
               " where " & Calc_Table & ".EMP_CODE=LTA.EMP_CODE AND " & Calc_Table & ".SUB_HEAD_TYPE='LTA' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        STRQ1 = "update TSPL_LTA_Claim_Head set PAY_PERIOD_CODE='" & Pay_Period_Code & "' where PAY_PERIOD_CODE is null"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating LTA !")
            'Return False
            Exit Function
        End If

        '' Conveyance
        clsCommon.ProgressBarUpdate("updating Conveyance...")
        strq = " UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=(CASE WHEN MAX_AMOUNT>0 AND  Conv.Total_Amount>MAX_AMOUNT THEN  MAX_AMOUNT ELSE Conv.Total_Amount END)  " &
               " from (select EMP_CODE,SUM(Claim_Amount) AS Total_Amount from TSPL_CONVEYANCE_CLAIM   " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY EMP_CODE) as Conv " &
               " where " & Calc_Table & ".EMP_CODE=Conv.EMP_CODE AND " & Calc_Table & ".SUB_HEAD_TYPE='Conveyance' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        STRQ1 = "update TSPL_CONVEYANCE_CLAIM set PAY_PERIOD_CODE='" & Pay_Period_Code & "' where PAY_PERIOD_CODE is null"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Conveyance !")
            'Return False
            Exit Function
        End If

        ''update pension of employees having age equal to or greater than 58 years
        clsCommon.ProgressBarUpdate("Applying 58 years condition in Pension...")
        '',CoEPS_AMT_AC10=0
        'ACTUAL_AMOUNT*CoEPF_RATE_AC01/(CoEPF_RATE_AC01+CoEPS_RATE_AC10)
        strq = " UPDATE  " & Calc_Table & " SET CoEPS_AMT_AC10=0  where SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'" &
                " AND EMP_CODE IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_MASTER where DATEADD(year,Age_For_Pension,CONVERT(DATE,Birth_date,103))<='" & clsCommon.GetPrintDate(objPP.DATE_TO, "dd/MMM/yyyy") & "' and Age_For_Pension>0 );"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating pension of employees having age equal to or greater than 58 years !")
            'Return False
            Exit Function
        End If

        '' ROUNDING OFF ALL PAY HEAD AMOUNT
        'If Is_Arrear = False Then
        '    clsCommon.ProgressBarUpdate("Rounding Off all pay heads...")
        '    strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        '    & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END),PRINCIPAL_ROUND_OFF=((CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        '    & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END)-ACTUAL_AMOUNT) where " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        '    If clsDBFuncationality.ExecuteNonQuery(strq) = True Then
        '        'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        '    Else
        '        clsCommon.MyMessageBoxShow("Error in Updating Rounding off !")
        '        'clsCommon.ProgressBarHide()
        '        Return False
        '        Exit Function
        '    End If

        'End If

        STRQ1 = "update " & Calc_Table & " set STD_AMOUNT=case when isnull(PAYABLE_DAYS,0)=0 then 0 else (CAST( FORMULA_VALUE as decimal(18,2)) * PAYPERIOD_DAYS/PAYABLE_DAYS) end where HEAD_TYPE='F' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) = True Then

        Else
            Throw New Exception("Error in Updating Standard Amount !")
            Exit Function
        End If
        If Is_Arrear = False Then
            CalculateArrear(Pay_Period_Code, trans)
        End If

        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN ((" & drPF.Item("EMPEPF_MAX") & ")*PAYABLE_DAYS/PAYPERIOD_DAYS) ELSE HEAD_VALUE END) *CoEPS_RATE_AC10/100) " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Employer EPS Account !")
            'Return False
            Exit Function
        End If


        '' apply max amount on employer accounts(COEPS)

        strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=(case when " & drPF.Item("EPS_MAX") & ">0 AND CoEPS_AMT_AC10> " & drPF.Item("EPS_MAX") & " THEN " & drPF.Item("EPS_MAX") & " ELSE   CoEPS_AMT_AC10 END) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Applying Maximum of Employer EPS Account !")
            'Return False
            Exit Function
        End If

        '' ROUND OFF COEPS
        If Is_Arrear = False Then
            clsCommon.ProgressBarUpdate("Rounding off Employer Accounts...")
            strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(CoEPS_AMT_AC10,0) " _
           & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(CoEPS_AMT_AC10) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(CoEPS_AMT_AC10) END), " _
           & " CoEPS_AMT_AC10_ROUND_OFF=(CoEPS_AMT_AC10-(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(CoEPS_AMT_AC10,0) " _
           & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(CoEPS_AMT_AC10) WHEN ROUND_OFF_TYPE='U' THEN CEILING(CoEPS_AMT_AC10) END))" _
           & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
            If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

            Else
                'clsCommon.ProgressBarHide()
                Throw New Exception("Error in Updating Rounding off Employer EPS account!")
                'Return False
                Exit Function
            End If
        End If
        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & Calc_Table & " SET CoEPS_AMT_AC10=0 " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0  and CoEPS_AMT_AC10<0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Employer EPS Account !")
            'Return False
            Exit Function
        End If

        ''update pension of employees having age equal to or greater than 58 years after arrear calculation
        clsCommon.ProgressBarUpdate("Applying 58 years condition in Pension...")
        '',CoEPS_AMT_AC10=0
        'ACTUAL_AMOUNT*CoEPF_RATE_AC01/(CoEPF_RATE_AC01+CoEPS_RATE_AC10)
        strq = " UPDATE  " & Calc_Table & " SET CoEPS_AMT_AC10=0  where SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'" &
                " AND EMP_CODE IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_MASTER where DATEADD(year,Age_For_Pension,CONVERT(DATE,Birth_date,103))<='" & clsCommon.GetPrintDate(objPP.DATE_TO, "dd/MMM/yyyy") & "' and Age_For_Pension>0 );"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating pension of employees having age equal to or graeter than 58 years !")
            'Return False
            Exit Function
        End If


        '' UPDATE EMPLOYER SHARE ACCOUNTS -COEPF
        Dim EPF As String = "((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN ((" & drPF.Item("EMPEPF_MAX") & ")*PAYABLE_DAYS/PAYPERIOD_DAYS) ELSE HEAD_VALUE END)*(CoEPF_RATE_AC01+CoEPS_RATE_AC10)/100)"
        strq = "UPDATE " & Calc_Table & " SET CoEPF_AMT_AC01=((CASE WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(" & EPF & ",0) " _
              & " WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(" & EPF & ") WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(" & EPF & ") END)-CoEPS_AMT_AC10), " _
              & " CoEPF_AMT_AC01_ROUND_OFF=-CoEPS_AMT_AC10_ROUND_OFF" _
              & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Employer PF Accounts !")
            'Return False
            Exit Function
        End If

        '' update other admin accounts
        strq = "UPDATE " & Calc_Table & " SET " _
          & " ADMIN_AMT_AC02=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END) * ADMIN_RATE_AC02/100),EDLI_AMT_AC21=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END)*EDLI_RATE_AC21/100), " _
          & " ADMIN_EDLI_AMT_AC22=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END) * ADMIN_EDLI_RATE_AC22/100) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Employer admin Accounts !")
            Return False
            Exit Function
        End If


        ''''''''''''''''''''''''''''''''
        '' apply max amount on other employer accounts

        strq = "UPDATE " & Calc_Table & " SET " _
            & " ADMIN_AMT_AC02=(case when " & drPF.Item("ACCOEPF_MAX") & ">0 AND ADMIN_AMT_AC02> " & drPF.Item("ACCOEPF_MAX") & " THEN " & drPF.Item("ACCOEPF_MAX") & " ELSE   ADMIN_AMT_AC02 END),EDLI_AMT_AC21=(case when " & drPF.Item("COEDLI_MAX") & ">0 AND EDLI_AMT_AC21> " & drPF.Item("COEDLI_MAX") & " THEN " & drPF.Item("COEDLI_MAX") & " ELSE   EDLI_AMT_AC21 END), " _
            & " ADMIN_EDLI_AMT_AC22=(case when " & drPF.Item("ACCOEDLI_MAX") & ">0 AND ADMIN_EDLI_AMT_AC22> " & drPF.Item("ACCOEDLI_MAX") & " THEN " & drPF.Item("ACCOEDLI_MAX") & " ELSE   ADMIN_EDLI_AMT_AC22 END) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Applying Maximum of Employer PF Accounts !")
            'Return False
            Exit Function
        End If

        strq = "UPDATE " & Calc_Table & " SET " _
       & " ADMIN_AMT_AC02=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(ADMIN_AMT_AC02,0) " _
       & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(ADMIN_AMT_AC02) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(ADMIN_AMT_AC02) END)," _
       & " EDLI_AMT_AC21=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(EDLI_AMT_AC21,0) " _
       & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(EDLI_AMT_AC21) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(EDLI_AMT_AC21) END)," _
       & " ADMIN_EDLI_AMT_AC22=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(ADMIN_EDLI_AMT_AC22,0) " _
       & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(ADMIN_EDLI_AMT_AC22) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(ADMIN_EDLI_AMT_AC22) END) " _
       & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Rounding off Employer PF accounts!")
            'Return False
            Exit Function
        End If


        ''adjustment IN PRINCIPAL AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in principal amount of pay heads...")
        strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=ACTUAL_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS, " _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and ADJUSTMENT_TYPE='PA' and TPH.HEAD_TYPE IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & Calc_Table & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & Calc_Table & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Adjustment !")
            'Return False
            Exit Function
        End If

        ''adjustment IN PRINCIPAL AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in principal amount of pay heads...")
        strq = "UPDATE " & Calc_Table & " SET RATE_AMOUNT=RATE_AMOUNT-(T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS) FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and TPH.HEAD_TYPE NOT IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & Calc_Table & ".EMP_CODE = T1.EMP_CODE)" _
             & " AND " & Calc_Table & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Adjustment !")
            'Return False
            Exit Function
        End If

        ''adjustment IN ARREAR AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in ADJSTMENT IN ARREAR pay heads...")
        strq = "UPDATE " & Calc_Table & " SET " _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS,ARREAR_AMT=T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and ADJUSTMENT_TYPE='AR'  GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & Calc_Table & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & Calc_Table & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Adjustment !")
            'Return False
            Exit Function
        End If

        clsCommon.ProgressBarUpdate("update actual amount in two decimals ...")
        strq = "UPDATE TSPL_SALARY_CALCULATION SET ACTUAL_AMOUNT=round(ACTUAL_AMOUNT,2,2) "

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating actual amount in two decimals !")
            'Return False
            Exit Function
        End If

        'ACTUAL_AMOUNT=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        '& " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END),
        'PRINCIPAL_ROUND_OFF=(COALESCE(PRINCIPAL_ROUND_OFF,0)+(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        '& " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END)-ACTUAL_AMOUNT),
        clsCommon.ProgressBarUpdate("Rounding Off all pay heads...")
        strq = "UPDATE " & Calc_Table & " SET ARREAR_AMT=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ARREAR_AMT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ARREAR_AMT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ARREAR_AMT) END),ARREAR_ROUND_OFF=((CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ARREAR_AMT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ARREAR_AMT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ARREAR_AMT) END)-ARREAR_AMT),ACTUAL_AMOUNT=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END)," _
        & " PRINCIPAL_ROUND_OFF=(COALESCE(PRINCIPAL_ROUND_OFF,0)+(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END)-ACTUAL_AMOUNT) where  " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            ''clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Rounding off !")
            'Return False
            Exit Function
        End If

        ''TDS Amount From Income tax Slab.
        clsCommon.ProgressBarUpdate("updating TDS in principal amount of pay heads...")
        strq = "select PAY_HEAD_CODE from TSPL_PAYHEAD_MASTER where SUB_HEAD_TYPE='TDS'"
        Dim strTDS As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strq, trans))
        If clsCommon.myLen(strTDS) > 0 Then
            strq = "select Fiscal_Code from TSPL_Fiscal_Year_Master where exists(select 1 from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & Pay_Period_Code & "'" + Environment.NewLine +
            "and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)>= convert(date, TSPL_Fiscal_Year_Master.Start_Date,103)  " + Environment.NewLine +
            "and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)<= convert(date, TSPL_Fiscal_Year_Master.End_Date,103))"
            Dim strFiscalCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strq, trans))
            If clsCommon.myLen(strFiscalCode) > 0 Then
                strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=xx.Total_TDS_Amt from (" + Environment.NewLine +
                "select TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code,cast( TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Total_TDS_Amt/12 as decimal(18,2)) as Total_TDS_Amt from TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP" + Environment.NewLine +
                "left outer join TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD on TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Code" + Environment.NewLine +
                "where TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Status=1" + Environment.NewLine +
                "and Fiscal_Code ='" + strFiscalCode + "'" + Environment.NewLine +
                ")xx inner join " & Calc_Table & " on " & Calc_Table & ".EMP_CODE=xx.Emp_Code" + Environment.NewLine +
                "inner join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=" & Calc_Table & ".PAY_HEAD_CODE" + Environment.NewLine +
                "where " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "' and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='" + strTDS + "'"
                If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
                Else
                    Throw New Exception("Error in Updating TDS Amount !")
                End If
            End If
        End If
        ''End of TDS Amount From Income tax Slab.

        STRQ1 = "update " & Calc_Table & " set ACTUAL_AMOUNT=0 where Payable_Days=0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) = True Then

        Else
            Throw New Exception("Error in update payble days !")
            Exit Function
        End If


        ''OT should be at last because it will use gross salary of employee.
        'ProgressBar1.Text = "Generating Salary...10. .calculating OT"
        clsCommon.ProgressBarUpdate("updating OT...")
        strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT=0 where SUB_HEAD_TYPE='OT' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            Throw New Exception("Error in Updating OT !")
            Exit Function
        End If
        '' calculation of OT
        '' get settings
        Dim WorkingHours As Decimal = 0
        Dim TreatExcessLeaveAbsentSett As String
        strq = "select Description from TSPL_FIXED_PARAMETER where Type='WorkingHours'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            WorkingHours = dt.Rows(0).Item("Description")
        Else
            WorkingHours = 8
        End If
        strq = "select Description from TSPL_FIXED_PARAMETER where Type='TreatExcessLeaveAbsent'"
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            TreatExcessLeaveAbsentSett = dt.Rows(0).Item("Description")
        Else
            TreatExcessLeaveAbsentSett = "0"
        End If

        strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT = 0,OT_HOURS=T11.OT_HOURS,OT_RATE=(CASE WHEN IS_ASPER_ACTUAL_CALC=0 THEN T11.OT_RATE ELSE COALESCE(HEAD_VALUE,0)/PAYPERIOD_DAYS END) " _
        & " FROM ( " _
        & " SELECT T1.EMP_CODE,T1.OT_HOURS,T1.OT_RATE,T1.OT_TOTAL_AMOUNT FROM TSPL_OT_SHEET T1 " _
        & " WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T11 WHERE " & Calc_Table & ".EMP_CODE=T11.EMP_CODE " _
        & " AND SUB_HEAD_TYPE='OT' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating OT !")
            'Return False
            Exit Function
        End If
        ''  UPDATE OT HOURS IN CASE OF EXCESS LEAVE ON THE BASIS OF SETTING
        If clsCommon.CompairString(TreatExcessLeaveAbsentSett, "0") = CompairStringResult.Equal Then
        Else
            strq = "UPDATE " & Calc_Table & " SET ACTUAL_AMOUNT = 0,OT_HOURS=(" & Calc_Table & ".OT_HOURS-EMP_LEAVE.EXCESS_LEAVE * " & WorkingHours & ")  " _
            & " FROM (SELECT TSPL_EMPLOYEE_MASTER.EMP_CODE,COALESCE(EMP_LEAVE.EXCESS_LEAVE,0) as EXCESS_LEAVE FROM TSPL_EMPLOYEE_MASTER left join " _
            & " (SELECT EMP_CODE,(SUM(AVAILED)-SUM(ALLOTED)) AS EXCESS_LEAVE FROM TSPL_VIEW_LEAVE_DETAIL inner join TSPL_PAYPERIOD_MASTER " _
            & " on TSPL_VIEW_LEAVE_DETAIL.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE where TSPL_VIEW_LEAVE_DETAIL.LEAVE_DATE<='" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "' " _
            & " GROUP BY EMP_CODE HAVING (SUM(AVAILED)-SUM(ALLOTED))>0  ) EMP_LEAVE on TSPL_EMPLOYEE_MASTER.EMP_CODE=EMP_LEAVE.EMP_CODE) AS EMP_LEAVE " _
            & " WHERE " & Calc_Table & ".EMP_CODE=EMP_LEAVE.EMP_CODE AND SUB_HEAD_TYPE='OT' and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
            ''WHERE PAY_PERIOD_CODE NOT IN ('" & Pay_Period_Code & "' 

            If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

            Else
                'clsCommon.ProgressBarHide()
                Throw New Exception("Error in Updating OT Hours!")
                'Return False
                Exit Function
            End If
        End If

        '' calculate ot 
        strq = " SELECT SALARY_CALCULATION_CODE,EMP_CODE,PAYPERIOD_DAYS,PAY_HEAD_CODE,SUB_HEAD_TYPE,RATE_AMOUNT,(select actual_amount from " & Calc_Table & "  AS SALARY where sub_head_type='BASIC' AND SALARY.EMP_CODE=" & Calc_Table & ".EMP_CODE) AS HEAD_VALUE,ACTUAL_AMOUNT,OT_CODE,OT_HOURS," &
               " OT_RATE,HOUR_MULTIPLIER,IS_ASPER_ACTUAL_CALC " + Environment.NewLine +
               " ,(select sum(STD_AMOUNT) from " & Calc_Table & "  AS SALARY left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=SALARY.PAY_HEAD_CODE where TSPL_PAYHEAD_MASTER.ISEARNING=1 AND SALARY.EMP_CODE=" & Calc_Table & ".EMP_CODE and TSPL_PAYHEAD_MASTER.Do_Not_Include_In_Gross_Salary_For_Over_Time=0 ) AS GrossSalary" + Environment.NewLine +
               " FROM " & Calc_Table & " " &
               " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=TSPL_SALARY_CALCULATION.PAY_PERIOD_CODE " + Environment.NewLine +
               " WHERE SUB_HEAD_TYPE='OT' AND OT_CODE IS NOT NULL AND COALESCE(OT_HOURS,0)>0 and " & Calc_Table & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "' ORDER BY EMP_CODE,LINE_NO "
        'balwinder()
        Dim dtOT As DataTable
        dtOT = clsDBFuncationality.GetDataTable(strq, trans)
        For Each dr As DataRow In dtOT.Rows
            strq = " update  " & Calc_Table & " SET ACTUAL_AMOUNT=" & CalculateOT(dr.Item("OT_CODE"), dr.Item("OT_HOURS"), clsCommon.myCdbl(dr.Item("HEAD_VALUE")) / (dr.Item("PAYPERIOD_DAYS") * WorkingHours), clsCommon.myCdbl(dr.Item("HEAD_VALUE")), trans, clsCommon.myCdbl(dr.Item("GrossSalary")) / (dr.Item("PAYPERIOD_DAYS") * WorkingHours)) & " WHERE SALARY_CALCULATION_CODE=" & dr.Item("SALARY_CALCULATION_CODE") & ""
            If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then

            Else
                'clsCommon.ProgressBarHide()
                Throw New Exception("Error in Updating OT Amount !")
                'Return False
                Exit Function
            End If
        Next


        If Is_Arrear = False Then
            ''clsCommon.ProgressBarHide()
        End If

        'Me.ProgressBar1.Value = 100
        Return True
    End Function
    Public Shared Function GetTotalWKHL(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal Day As Integer, ByVal dayName As String, ByVal trans As SqlTransaction) As Decimal
        Dim totGL As Decimal = 0
        Dim strq As String
        strq = ""
        strq += " DECLARE @totDay int,@dateFrom date,@dateTo date,@day integer,@totWKHL decimal(4,2),@dayName varchar(20)"
        strq += " set @dateFrom='" & clsCommon.GetPrintDate(dateFrom, "dd/MMM/yyyy") & "';"
        strq += " set @dateTo='" & clsCommon.GetPrintDate(dateTo, "dd/MMM/yyyy") & "';"
        strq += " set @day=" & Day & ";"
        strq += " set @dayName='" & dayName & "';"
        strq += " set @totDay=(SELECT sunday = count(* ) FROM (SELECT TOP ( datediff(DAY,@dateFrom,@dateTo) + 1 )[Date] = dateadd(DAY,ROW_NUMBER() "
        strq += " OVER(ORDER BY c1.name),DATEADD(DD,-1,@dateFrom)) FROM   [master].[dbo].[spt_values] c1 ) x WHERE  datepart(dw,[Date]) = @day);"
        strq += " if @totDay=5 "
        strq += " set @totWKHL=(select (convert(int,FSTWK_FSTHALF)+convert(int,FSTWK_SECHALF)+convert(int,SECWK_FSTHALF)+convert(int,SECWK_SECHALF)+convert(int,THDWK_FSTHALF)"
        strq += " +convert(int,THDWK_SECHALF)+convert(int,FTHWK_FSTHALF)+convert(int,FTHWK_SECHALF)+convert(int,FIVWK_FSTHALF)+convert(int,FIVWK_SECHALF))/2 from "
        strq += " TSPL_WEEKLY_HOLIDAYS where applicable_from ="
        strq += " (select max(APPLICABLE_FROM) from TSPL_WEEKLY_HOLIDAYS where  APPLICABLE_FROM<=@dateTo "
        strq += " and WEEKDAY_NAME= @dayName) and WEEKDAY_NAME= @dayName);"
        strq += " else"
        strq += " set @totWKHL=(select (convert(int,FSTWK_FSTHALF)+convert(int,FSTWK_SECHALF)+convert(int,SECWK_FSTHALF)+convert(int,SECWK_SECHALF)+convert(int,THDWK_FSTHALF)"
        strq += " +convert(int,THDWK_SECHALF)+convert(int,FTHWK_FSTHALF)+convert(int,FTHWK_SECHALF))/2 from "
        strq += " TSPL_WEEKLY_HOLIDAYS where applicable_from ="
        strq += " (select max(APPLICABLE_FROM) from TSPL_WEEKLY_HOLIDAYS where  APPLICABLE_FROM<=@dateTo "
        strq += " and WEEKDAY_NAME= @dayName) and WEEKDAY_NAME= @dayName);"
        strq += " select coalesce(@totWKHL,0) as TotalHLDays;"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            totGL = dt.Rows(0).Item("TotalHLDays")
        Else
            totGL = 0
        End If

        Return totGL
    End Function
    Public Shared Function TotalWKHL(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal trans As SqlTransaction) As Decimal
        Dim totWKHL As Decimal
        totWKHL = GetTotalWKHL(dateFrom, dateTo, 1, "Sunday", trans) + GetTotalWKHL(dateFrom, dateTo, 2, "Monday", trans) + GetTotalWKHL(dateFrom, dateTo, 3, "Tuesday", trans) + GetTotalWKHL(dateFrom, dateTo, 4, "Wednesday", trans) + GetTotalWKHL(dateFrom, dateTo, 5, "Thursday", trans) + GetTotalWKHL(dateFrom, dateTo, 6, "Friday", trans) + GetTotalWKHL(dateFrom, dateTo, 7, "Saturday", trans)
        Return totWKHL
    End Function

    Public Shared Function TotalGLHL(ByVal dateFrom As Date, ByVal dateTo As Date, ByVal trans As SqlTransaction) As Decimal
        Dim totGLHL As Decimal
        Dim strq As String = ""
        strq += " SELECT COUNT(*) AS totGL,ATTENDANCE_CODE from TSPL_GENERAL_HOLIDAYS"
        strq += " where HOLIDAY_DATE between '" & clsCommon.GetPrintDate(dateFrom, "dd/MMM/yyyy") & "' and '" & clsCommon.GetPrintDate(dateTo.AddDays(1), "dd/MMM/yyyy") & "' group by ATTENDANCE_CODE"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            totGLHL = dt.Rows(0).Item("totGL")
        Else
            totGLHL = 0
        End If

        Return totGLHL
    End Function
    Public Shared Function CalculateOT(ByVal OTCode As String, ByVal otHours As Decimal, ByVal BasicPerHour As Decimal, ByVal Basic As Decimal, ByVal trans As SqlTransaction, ByVal GrossPerHour As Decimal) As Decimal
        Dim qry As String
        Dim OTAmount As Decimal = 0.0
        qry = " select TSPL_OT_MASTER.OT_CODE,TSPL_OT_MASTER.IS_ASPER_ACTUAL_CALC,TSPL_OT_MASTER.HOUR_MULTIPLIER,TSPL_OT_SLAB_DETAIL.CRITERIA_TYPE," &
              " TSPL_OT_SLAB_DETAIL._FROM,TSPL_OT_SLAB_DETAIL._TO,coalesce(TSPL_OT_SLAB_DETAIL.OT_RATE,TSPL_OT_MASTER.ot_rate) as OT_RATE,TSPL_OT_SLAB_DETAIL.RATE_TYPE from TSPL_OT_MASTER left join " &
              " TSPL_OT_SLAB on TSPL_OT_MASTER.OT_CODE=TSPL_OT_SLAB.OT_CODE left join TSPL_OT_SLAB_DETAIL " &
              " on TSPL_OT_SLAB.OT_CODE=TSPL_OT_SLAB_DETAIL.OT_CODE where TSPL_OT_MASTER.OT_CODE='" & OTCode & "'  order by TSPL_OT_MASTER.OT_CODE,TSPL_OT_SLAB_DETAIL._FROM"
        Dim dtOT As DataTable
        dtOT = clsDBFuncationality.GetDataTable(qry, trans)
        For Each dr As DataRow In dtOT.Rows
            If IsDBNull(dr.Item("CRITERIA_TYPE")) Then
                If clsCommon.myCBool(dr.Item("IS_ASPER_ACTUAL_CALC")) Then
                    OTAmount = otHours * BasicPerHour
                Else
                    OTAmount = otHours * dr.Item("OT_RATE") * BasicPerHour
                End If
                Exit For
            Else
                If clsCommon.CompairString(dr.Item("CRITERIA_TYPE"), "Basic", False) = CompairStringResult.Equal Then
                    If Basic <= dr.Item("_TO") And Basic >= dr.Item("_FROM") Then
                        If clsCommon.myCBool(dr.Item("IS_ASPER_ACTUAL_CALC")) Then
                            If clsCommon.CompairString(dr.Item("Rate_Type"), "PBS", False) = CompairStringResult.Equal Then
                                OTAmount = otHours * BasicPerHour * dr.Item("OT_RATE") / 100
                            Else
                                OTAmount = otHours * dr.Item("OT_RATE") * BasicPerHour
                            End If
                        Else
                            If clsCommon.CompairString(dr.Item("Rate_Type"), "PBS", False) = CompairStringResult.Equal Then
                                OTAmount = otHours * BasicPerHour * dr.Item("OT_RATE") / 100
                            Else
                                OTAmount = otHours * dr.Item("OT_RATE") * BasicPerHour
                            End If
                        End If
                        Exit For
                    End If
                ElseIf clsCommon.CompairString(dr.Item("CRITERIA_TYPE"), "OTH", False) = CompairStringResult.Equal Then
                    If otHours <= dr.Item("_TO") And otHours >= dr.Item("_FROM") Then
                        If clsCommon.myCBool(dr.Item("IS_ASPER_ACTUAL_CALC")) Then
                            If clsCommon.CompairString(dr.Item("Rate_Type"), "PBS", False) = CompairStringResult.Equal Then
                                OTAmount = otHours * BasicPerHour * dr.Item("OT_RATE") / 100
                            ElseIf clsCommon.CompairString(dr.Item("Rate_Type"), "PGS", False) = CompairStringResult.Equal Then
                                OTAmount = otHours * GrossPerHour * dr.Item("OT_RATE") / 100
                            Else
                                OTAmount = otHours * BasicPerHour
                            End If
                        Else
                            If clsCommon.CompairString(dr.Item("Rate_Type"), "PBS", False) = CompairStringResult.Equal Then
                                OTAmount = otHours * BasicPerHour * dr.Item("OT_RATE") / 100
                            ElseIf clsCommon.CompairString(dr.Item("Rate_Type"), "PGS", False) = CompairStringResult.Equal Then
                                OTAmount = otHours * GrossPerHour * dr.Item("OT_RATE") / 100
                            Else
                                OTAmount = otHours * dr.Item("OT_RATE")
                            End If
                        End If
                        Exit For
                    End If
                End If
            End If
        Next
        Return Math.Round(OTAmount, 0, MidpointRounding.ToEven)
    End Function
    Public Shared Function GetQueryPT(ByVal dtpt As DataTable) As String
        Dim strq As String
        strq = "(CASE "
        For intloop As Integer = 0 To dtpt.Rows.Count - 1
            strq = strq & " WHEN HEAD_VALUE BETWEEN " & " " & dtpt.Rows(intloop).Item("SLAB_FROM") & " AND " & dtpt.Rows(intloop).Item("SLAB_TO") & " THEN " & dtpt.Rows(intloop).Item("PT_AMOUNT")
        Next
        strq = strq & " ELSE 0 END)"
        Return strq
    End Function
    Public Shared Function CalculateArrear(ByVal Pay_Period_Code As String, ByVal trans As SqlTransaction) As Boolean
        ''Ticket No:BM00000007750  by Panch raj
        clsCommon.ProgressBarUpdate("Calculating Arrears....")
        Dim objPP As clsPayPeriodMaster = clsPayPeriodMaster.GetData(Pay_Period_Code, NavigatorType.Current, trans)
        Dim qry As String = ""
        qry = "TRUNCATE TABLE TSPL_ARREAR_CALCULATION"
        If clsDBFuncationality.ExecuteNonQuery(qry, trans) = True Then
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in truncating Arrear Calculation Table !")
            'Return False
            Exit Function
        End If

        qry = " select distinct TSPL_SALARY_CALCULATION.EMP_CODE,TSPL_SALARY_CALCULATION.EMP_SAL_CODE as NEW_EMP_SAL_CODE,Incr.EMP_SAL_CODE as OLD_EMP_SAL_CODE," &
              " EMP_Sal.APPLICABLE_FROM,Incr.Arrear_From from TSPL_SALARY_CALCULATION " &
              " inner join TSPL_EMPLOYEE_INCREMENT_HEAD Incr on Incr.EMP_SAL_CODE_NEW=TSPL_SALARY_CALCULATION.EMP_SAL_CODE " &
              " inner join TSPL_EMPLOYEE_SALARY EMP_Sal on EMP_Sal.EMP_SAL_CODE=TSPL_SALARY_CALCULATION.EMP_SAL_CODE where Incr.Arrear_From is not null " &
              " and  EMP_Sal.APPLICABLE_FROM between (select DATE_FROM from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & Pay_Period_Code & "') and (select DATE_TO from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & Pay_Period_Code & "') " &
              " order by TSPL_SALARY_CALCULATION.EMP_CODE "
        Dim dtEMP As DataTable
        '' collect list of employees having salary increment in their current applicable salary.
        dtEMP = clsDBFuncationality.GetDataTable(qry, trans)
        Dim objPPList As New List(Of PayPeriod_Employee)
        'Dim objPPCode As New PayPeriod_Employee

        For Each drEMP As DataRow In dtEMP.Rows
            qry = String.Empty
            qry = " select COUNT(*) AS Total from TSPL_GENERATE_SALARY_ATTENDANCE GSA INNER JOIN TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " &
                  " INNER JOIN TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " &
                  " where EMP_SAL_CODE='" & drEMP.Item("NEW_EMP_SAL_CODE") & "' AND PPM.DATE_FROM>='" & clsCommon.GetPrintDate(drEMP.Item("Arrear_From"), "dd/MMM/yyyy") & "' AND PPM.DATE_TO<'" & clsCommon.GetPrintDate(objPP.DATE_TO, "dd/MMM/yyyy") & "' and coalesce(GSA.Arrear_Amt,0)>0"
            Dim ArrCalc As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            If ArrCalc <= 0 Then
                qry = String.Empty
                'and coalesce(GSA.Arrear_Amt,0)<=0
                qry = " select  GS.Pay_Period_Code,GSA.EMP_CODE,GSA.SALARY_STRUCTURE_CODE,GSA.EMP_SAL_CODE,GSA.EMP_STATUS_CODE,GSA.ARREAR_AMT from TSPL_GENERATE_SALARY_ATTENDANCE GSA INNER JOIN TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " &
                  " INNER JOIN TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " &
                  " where EMP_CODE='" & drEMP.Item("EMP_CODE") & "' AND PPM.DATE_FROM>='" & clsCommon.GetPrintDate(drEMP.Item("Arrear_From"), "dd/MMM/yyyy") & "' AND PPM.DATE_TO<'" & clsCommon.GetPrintDate(objPP.DATE_TO, "dd/MMM/yyyy") & "'  order by PPM.DATE_FROM"
                Dim dtEmpArr As DataTable
                Dim lstEmp As New ArrayList
                Dim PP_Arrear As String = ""

                dtEmpArr = clsDBFuncationality.GetDataTable(qry, trans)
                'If dtEmpArr.Rows.Count > 0 Then
                '    PP_Arrear = dtEmpArr.Rows(0).Item("Pay_Period_Code")
                'End If
                'lstEmp = New ArrayList
                For Each drEMPArr As DataRow In dtEmpArr.Rows
                    'If clsCommon.CompairString(PP_Arrear, drEMPArr.Item("Pay_Period_Code")) = CompairStringResult.Equal Then
                    '    lstEmp.Add(drEMPArr.Item("EMP_CODE"))
                    'Else
                    '    Generate_Salary(PP_Arrear, lstEmp, True)
                    '    '' update arrear amount in main temp table
                    '    clsCommon.ProgressBarUpdate("updating Arrears in salary temp table....")
                    '    If AddPayPeriodArrear(PP_Arrear, lstEmp) = True Then
                    '    Else
                    '        clsCommon.MyMessageBoxShow("Error in updating Arrear amount !")
                    '        'clsCommon.ProgressBarHide()
                    '        Return False
                    '    End If
                    '    PP_Arrear = drEMPArr.Item("Pay_Period_Code")
                    '    lstEmp = New ArrayList
                    '    lstEmp.Add(drEMPArr.Item("EMP_CODE"))
                    'End If

                    Dim Found As Boolean = False
                    For Each objPPCode As PayPeriod_Employee In objPPList
                        If clsCommon.CompairString(objPPCode.Pay_Period_Code, drEMPArr.Item("Pay_Period_Code")) = CompairStringResult.Equal Then
                            Found = True
                            'Dim EMP_Code As New PayPeriod_EmployeeList
                            'EMP_Code.EMP_CODE = drEMPArr.Item("EMP_CODE")
                            objPPCode.objEMPList.Add(drEMPArr.Item("EMP_CODE"))
                            Exit For
                        End If
                    Next
                    If Found = False Then
                        Dim objPPCode As New PayPeriod_Employee
                        objPPCode.Pay_Period_Code = drEMPArr.Item("Pay_Period_Code")
                        'Dim EMP_Code As New PayPeriod_EmployeeList
                        'EMP_Code.EMP_CODE = drEMPArr.Item("EMP_CODE")
                        objPPCode.objEMPList.Add(drEMPArr.Item("EMP_CODE"))
                        objPPList.Add(objPPCode)
                    End If
                Next
            End If
        Next
        '' calculate arrear
        For Each PP_Arrear As PayPeriod_Employee In objPPList
            'Dim lstEmp As New ArrayList(PP_Arrear.objEMPList)
            Generate_Salary(PP_Arrear.Pay_Period_Code, PP_Arrear.objEMPList, trans, True)
            '    '' update arrear amount in main temp table
            clsCommon.ProgressBarUpdate("updating Arrears in salary temp table....")
            If AddPayPeriodArrear(PP_Arrear.Pay_Period_Code, PP_Arrear.objEMPList, trans) = True Then
            Else
                'clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow("Error in updating Arrear amount !")
                'Return False
            End If
        Next

        clsCommon.ProgressBarUpdate("Add Arrear in Principal Amount ...")
        qry = "UPDATE TSPL_SALARY_CALCULATION SET ACTUAL_AMOUNT=ACTUAL_AMOUNT+COALESCE(ARREAR_AMT,0) "

        If clsDBFuncationality.ExecuteNonQuery(qry, trans) = True Then
            'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        Else
            'clsCommon.ProgressBarHide()
            Throw New Exception("Error in Updating Rounding off !")
            'Return False
            Exit Function
        End If

        'clsCommon.ProgressBarUpdate("Rounding Off all pay heads...")
        'qry = "UPDATE TSPL_SALARY_CALCULATION SET ARREAR_AMT=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ARREAR_AMT,0) " _
        '& " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ARREAR_AMT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ARREAR_AMT) END)"

        'If clsDBFuncationality.ExecuteNonQuery(qry) = True Then
        '    'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        'Else
        '    clsCommon.MyMessageBoxShow("Error in Updating Rounding off !")
        '    'clsCommon.ProgressBarHide()
        '    Return False
        '    Exit Function
        'End If

        'clsCommon.ProgressBarUpdate("Updating final employer accounts...")
        'qry = "UPDATE TSPL_SALARY_CALCULATION SET CoEPF_AMT_AC01=(Actual_Amount+coalesce(ARREAR_AMT,0))*CoEPF_RATE_AC01/RATE_AMOUNT where TSPL_SALARY_CALCULATION.SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0"

        'If clsDBFuncationality.ExecuteNonQuery(qry) = True Then
        '    'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        'Else
        '    clsCommon.MyMessageBoxShow("Error in employer accounts !")
        '    'clsCommon.ProgressBarHide()
        '    Return False
        '    Exit Function
        'End If
        'clsCommon.ProgressBarUpdate("Rounding Off all employer accounts...")
        'qry = "UPDATE TSPL_SALARY_CALCULATION SET CoEPF_AMT_AC01=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(CoEPF_AMT_AC01,0) " _
        '& " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(CoEPF_AMT_AC01) WHEN ROUND_OFF_TYPE='U' THEN CEILING(CoEPF_AMT_AC01) END)"

        'If clsDBFuncationality.ExecuteNonQuery(qry) = True Then
        '    'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        'Else
        '    clsCommon.MyMessageBoxShow("Error in Updating Rounding off !")
        '    'clsCommon.ProgressBarHide()
        '    Return False
        '    Exit Function
        'End If

        'clsCommon.ProgressBarUpdate("Updating final employer accounts...")
        'qry = "UPDATE TSPL_SALARY_CALCULATION SET CoEPS_AMT_AC10=(Actual_Amount+coalesce(ARREAR_AMT,0)-coalesce(CoEPF_AMT_AC01,0)) where TSPL_SALARY_CALCULATION.SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0"

        'If clsDBFuncationality.ExecuteNonQuery(qry) = True Then
        '    'Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1
        'Else
        '    clsCommon.MyMessageBoxShow("Error in employer accounts !")
        '    'clsCommon.ProgressBarHide()
        '    Return False
        '    Exit Function
        'End If
        Return True
    End Function
    Public Shared Function AddPayPeriodArrear(ByVal PP_Arrear As String, ByVal lstEmp As ArrayList, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = ""

        'Qry = " update TSPL_SALARY_CALCULATION set ARREAR_AMT=coalesce(TSPL_SALARY_CALCULATION.ARREAR_AMT,0)+coalesce(Arrear.ACTUAL_AMOUNT,0)-coalesce(Old_Sal.ACTUAL_AMOUNT,0),HEAD_VALUE=coalesce(TSPL_SALARY_CALCULATION.HEAD_VALUE,0)+coalesce(Arrear.HEAD_VALUE,0)-coalesce(Old_Sal.HEAD_VALUE,0) from " & _
        '                     " (select EMP_CODE,PAY_HEAD_CODE,sum(ACTUAL_AMOUNT) as ACTUAL_AMOUNT,sum(HEAD_VALUE) as HEAD_VALUE from TSPL_ARREAR_CALCULATION where EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ") and PAY_PERIOD_CODE='" & PP_Arrear & "' group by EMP_CODE,PAY_HEAD_CODE) Arrear " & _
        '                     " LEFT JOIN (SELECT GSP.EMP_CODE,PAY_HEAD_CODE ,((case when GSP.ACTUAL_AMOUNT<>0 then (CASE WHEN GSP.SUB_HEAD_TYPE='EMPESI' THEN GSP.PAYABLE_AMOUNT ELSE ACTUAL_AMOUNT END) else  0 end) -COALESCE(GSP.ARREAR_AMT,0)) AS ACTUAL_AMOUNT,round(coalesce(HEAD_VALUE,0)-(case when Head_Type='F' and GSP.ARREAR_AMT>0 then coalesce(GSP.ARREAR_AMT,0)*100/(case when COALESCE(GSP.RATE_AMOUNT,0)=0 then 1 else COALESCE(GSP.RATE_AMOUNT,0) end) else 0 end),0) as HEAD_VALUE  FROM TSPL_GENERATE_SALARY_PAYHEADS GSP " & _
        '                     " INNER JOIN TSPL_GENERATE_SALARY GS ON GSP.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE inner join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON GSP.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=GSP.EMP_CODE WHERE GS.PAY_PERIOD_CODE='" & PP_Arrear & "' " & _
        '                     " AND GSP.EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ")) AS Old_Sal ON " & _
        '                     " Arrear.EMP_CODE = Old_Sal.EMP_CODE And Arrear.PAY_HEAD_CODE = Old_Sal.PAY_HEAD_CODE " & _
        '                     " where TSPL_SALARY_CALCULATION.EMP_CODE=Arrear.EMP_CODE and TSPL_SALARY_CALCULATION.PAY_HEAD_CODE=Arrear.PAY_HEAD_CODE "
        'clsDBFuncationality.ExecuteNonQuery(Qry)


        Qry = " update TSPL_SALARY_CALCULATION set ARREAR_AMT=coalesce(TSPL_SALARY_CALCULATION.ARREAR_AMT,0)+coalesce(Arrear.ACTUAL_AMOUNT,0)-coalesce(Old_Sal.ACTUAL_AMOUNT,0),HEAD_VALUE=coalesce(TSPL_SALARY_CALCULATION.HEAD_VALUE,0)+coalesce(Arrear.HEAD_VALUE,0)-coalesce(Old_Sal.HEAD_VALUE,0) from " &
                             " (select EMP_CODE,PAY_HEAD_CODE,sum(ACTUAL_AMOUNT-COALESCE(PRINCIPAL_ROUND_OFF,0)) as ACTUAL_AMOUNT,sum(HEAD_VALUE) as HEAD_VALUE from TSPL_ARREAR_CALCULATION where EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ") and PAY_PERIOD_CODE='" & PP_Arrear & "' group by EMP_CODE,PAY_HEAD_CODE) Arrear " &
                             " LEFT JOIN (SELECT GSP.EMP_CODE,PAY_HEAD_CODE ,((GSP.ACTUAL_AMOUNT-COALESCE(PRINCIPAL_ROUND_OFF,0)) -(COALESCE(GSP.ARREAR_AMT,0)+COALESCE(ARREAR_ROUND_OFF,0))) AS ACTUAL_AMOUNT,round(coalesce(HEAD_VALUE,0)-(case when Head_Type='F' and GSP.ARREAR_AMT>0 then coalesce(GSP.ARREAR_AMT,0)*100/(case when COALESCE(GSP.RATE_AMOUNT,0)=0 then 1 else COALESCE(GSP.RATE_AMOUNT,0) end) else 0 end),0) as HEAD_VALUE  FROM TSPL_GENERATE_SALARY_PAYHEADS GSP " &
                             " INNER JOIN TSPL_GENERATE_SALARY GS ON GSP.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE inner join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON GSP.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE AND GSA.EMP_CODE=GSP.EMP_CODE WHERE GS.PAY_PERIOD_CODE='" & PP_Arrear & "' " &
                             " AND GSP.EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ")) AS Old_Sal ON " &
                             " Arrear.EMP_CODE = Old_Sal.EMP_CODE And Arrear.PAY_HEAD_CODE = Old_Sal.PAY_HEAD_CODE " &
                             " where TSPL_SALARY_CALCULATION.EMP_CODE=Arrear.EMP_CODE and TSPL_SALARY_CALCULATION.PAY_HEAD_CODE=Arrear.PAY_HEAD_CODE "
        clsDBFuncationality.ExecuteNonQuery(Qry, trans)


        '' UPDATE EMPLOYER ACCOUNTS FOR PF
        Qry = " update TSPL_SALARY_CALCULATION set CoEPF_AMT_AC01=coalesce(TSPL_SALARY_CALCULATION.CoEPF_AMT_AC01,0)+coalesce(Arrear.CoEPF_AMT_AC01,0)-coalesce(Old_Sal.CoEPF_AMT_AC01,0), " &
                             " CoEPS_AMT_AC10=coalesce(TSPL_SALARY_CALCULATION.CoEPS_AMT_AC10,0)+coalesce(Arrear.CoEPS_AMT_AC10,0)-coalesce(Old_Sal.CoEPS_AMT_AC10,0),  " &
                             " ADMIN_AMT_AC02=coalesce(TSPL_SALARY_CALCULATION.ADMIN_AMT_AC02,0)+coalesce(Arrear.ADMIN_AMT_AC02,0)-coalesce(Old_Sal.ADMIN_AMT_AC02,0),  " &
                             " EDLI_AMT_AC21=coalesce(TSPL_SALARY_CALCULATION.EDLI_AMT_AC21,0)+coalesce(Arrear.EDLI_AMT_AC21,0)-coalesce(Old_Sal.EDLI_AMT_AC21,0),  " &
                             " ADMIN_EDLI_AMT_AC22=coalesce(TSPL_SALARY_CALCULATION.ADMIN_EDLI_AMT_AC22,0)+coalesce(Arrear.ADMIN_EDLI_AMT_AC22,0)-coalesce(Old_Sal.ADMIN_EDLI_AMT_AC22,0),  " &
                             " OTHER_CHARGE=coalesce(TSPL_SALARY_CALCULATION.OTHER_CHARGE,0)+coalesce(Arrear.OTHER_CHARGE,0)-coalesce(Old_Sal.OTHER_CHARGE,0)  " &
                             " from (select EMP_CODE,PAY_HEAD_CODE,sum(CoEPF_AMT_AC01) as CoEPF_AMT_AC01,sum(CoEPS_AMT_AC10) as CoEPS_AMT_AC10,sum(ADMIN_AMT_AC02) as ADMIN_AMT_AC02, " &
                             " sum(EDLI_AMT_AC21) as EDLI_AMT_AC21,sum(ADMIN_EDLI_AMT_AC22) as ADMIN_EDLI_AMT_AC22,sum(OTHER_CHARGE) as OTHER_CHARGE from TSPL_ARREAR_CALCULATION where SUB_HEAD_TYPE='EPF' and EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ") and PAY_PERIOD_CODE='" & PP_Arrear & "'   group by EMP_CODE,PAY_HEAD_CODE) Arrear " &
                             " LEFT JOIN (SELECT EMP_CODE,PAY_HEAD_CODE ,(GSP.CoEPF_AMT_AC01+coalesce(CoEPF_AMT_AC01_ROUND_OFF,0)-(case when GSP.ARREAR_AMT>0 then coalesce(ARREAR_AMT,0)*GSP.CoEPF_RATE_AC01/(case when COALESCE(GSP.RATE_AMOUNT,0)=0 then 1 else COALESCE(GSP.RATE_AMOUNT,0) end) ELSE 0 END)) as CoEPF_AMT_AC01,(GSP.CoEPS_AMT_AC10+coalesce(CoEPS_AMT_AC10_ROUND_OFF,0)-(case when GSP.ARREAR_AMT>0 " &
                             " then coalesce(ARREAR_AMT,0)*GSP.CoEPS_RATE_AC10/(case when COALESCE(GSP.RATE_AMOUNT,0)=0 then 1 else COALESCE(GSP.RATE_AMOUNT,0) end) ELSE 0 END)) as CoEPS_AMT_AC10,GSP.ADMIN_AMT_AC02,GSP.EDLI_AMT_AC21,GSP.ADMIN_EDLI_AMT_AC22,GSP.OTHER_CHARGE FROM TSPL_GENERATE_SALARY_PAYHEADS GSP " &
                             " INNER JOIN TSPL_GENERATE_SALARY GS ON GSP.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE WHERE GS.PAY_PERIOD_CODE='" & PP_Arrear & "' " &
                             " AND GSP.EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ") and  GSP.SUB_HEAD_TYPE='EPF') AS Old_Sal ON " &
                             " Arrear.EMP_CODE = Old_Sal.EMP_CODE And Arrear.PAY_HEAD_CODE = Old_Sal.PAY_HEAD_CODE " &
                             " where TSPL_SALARY_CALCULATION.EMP_CODE=Arrear.EMP_CODE and TSPL_SALARY_CALCULATION.PAY_HEAD_CODE=Arrear.PAY_HEAD_CODE and  TSPL_SALARY_CALCULATION.SUB_HEAD_TYPE='EPF' "
        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        '' UPDATE EMPLOYER ACCOUNT FOR ESI
        Qry = " update TSPL_SALARY_CALCULATION set Co_ESI_AMT=coalesce(TSPL_SALARY_CALCULATION.Co_ESI_AMT,0)+coalesce(Arrear.Co_ESI_AMT,0)-coalesce(Old_Sal.Co_ESI_AMT,0) " &
                             " from (select EMP_CODE,PAY_HEAD_CODE,sum(Co_ESI_AMT) as Co_ESI_AMT from TSPL_ARREAR_CALCULATION where SUB_HEAD_TYPE='EMPESI' and EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ") and PAY_PERIOD_CODE='" & PP_Arrear & "' group by EMP_CODE,PAY_HEAD_CODE) Arrear " &
                             " LEFT JOIN (SELECT EMP_CODE,PAY_HEAD_CODE ,round(GSP.Co_ESI_AMT-(case when GSP.ARREAR_AMT>0 then coalesce(ARREAR_AMT,0)*GSP.Co_ESI_RATE/(case when COALESCE(GSP.RATE_AMOUNT,0)=0 then 1 else COALESCE(GSP.RATE_AMOUNT,0) end) ELSE 0 END),0) AS Co_ESI_AMT FROM TSPL_GENERATE_SALARY_PAYHEADS GSP " &
                             " INNER JOIN TSPL_GENERATE_SALARY GS ON GSP.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE WHERE GS.PAY_PERIOD_CODE='" & PP_Arrear & "' " &
                             " AND GSP.EMP_CODE IN (" & clsCommon.GetMulcallString(lstEmp) & ") and  GSP.SUB_HEAD_TYPE='EMPESI') AS Old_Sal ON " &
                             " Arrear.EMP_CODE = Old_Sal.EMP_CODE And Arrear.PAY_HEAD_CODE = Old_Sal.PAY_HEAD_CODE " &
                             " where TSPL_SALARY_CALCULATION.EMP_CODE=Arrear.EMP_CODE and TSPL_SALARY_CALCULATION.PAY_HEAD_CODE=Arrear.PAY_HEAD_CODE and  TSPL_SALARY_CALCULATION.SUB_HEAD_TYPE='EMPESI' "
        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Return True
    End Function
    '' get total count of pf and EDLI applicable employees(New)
    Public Shared Function GetPFNewEmpListCount(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As Integer
        Dim totalEmp As Integer = 0
        Dim qry As String = GetPFNewEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        qry = "select count(*) from (" & qry & ") Final"
        totalEmp = clsDBFuncationality.getSingleValue(qry, trans)
        Return totalEmp
    End Function
    '' get List employees of pf and EDLI applicable employees(New)
    Public Shared Function GetPFNewEMPList(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As ArrayList
        Dim qry As String = GetPFNewEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim lstEmp As New ArrayList
        For Each drEmp As DataRow In dt.Rows
            lstEmp.Add(clsCommon.myCstr(drEmp.Item("New_EMP")))
        Next
        Return lstEmp
    End Function

    '' get total count of Pension applicable employees(New)
    Public Shared Function GetPFPensionNewEmpListCount(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As Integer
        Dim totalEmp As Integer = 0
        Dim qry As String = GetPFNewPensionEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        qry = "select count(*) from (" & qry & ") Final"
        totalEmp = clsDBFuncationality.getSingleValue(qry, trans)
        Return totalEmp
    End Function
    '' get List Pension applicable employees(New)
    Public Shared Function GetPFNewPensionEMPList(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As ArrayList
        Dim qry As String = GetPFNewPensionEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim lstEmp As New ArrayList
        For Each drEmp As DataRow In dt.Rows
            lstEmp.Add(clsCommon.myCstr(drEmp.Item("New_EMP")))
        Next
        Return lstEmp
    End Function

    Public Shared Function GetPFLeftEmpListCount(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As Integer
        Dim totalEmp As Integer = 0
        Dim qry As String = GetPFLeftEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        qry = "select count(*) from (" & qry & ") Final"
        totalEmp = clsDBFuncationality.getSingleValue(qry, trans)
        Return totalEmp
    End Function
    Public Shared Function GetPFLeftEmpList(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As ArrayList
        Dim qry As String = GetPFLeftEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim lstEmp As New ArrayList
        For Each drEmp As DataRow In dt.Rows
            lstEmp.Add(clsCommon.myCstr(drEmp.Item("Old_EMP")))
        Next
        Return lstEmp
    End Function

    Public Shared Function GetPFLeftPensionEmpListCount(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As Integer
        Dim totalEmp As Integer = 0
        Dim qry As String = GetPFLeftPensionEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        qry = "select count(*) from (" & qry & ") Final"
        totalEmp = clsDBFuncationality.getSingleValue(qry, trans)
        Return totalEmp
    End Function
    Public Shared Function GetPFLeftPensionEMPList(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal trans As SqlTransaction) As ArrayList
        Dim qry As String = GetPFLeftPensionEmpListQuery(strPPOld, strPPNew, strLocationList, strDivisionList)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        Dim lstEmp As New ArrayList
        For Each drEmp As DataRow In dt.Rows
            lstEmp.Add(clsCommon.myCstr(drEmp.Item("Old_EMP")))
        Next
        Return lstEmp
    End Function

    Public Shared Function GetPFLeftEmpListQuery(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList) As String
        Dim qry As String = ""
        Dim oldPPQry As String = GetPFEMPListQry(strPPOld, strLocationList, strDivisionList)
        Dim NewPPQry As String = GetPFEMPListQry(strPPNew, strLocationList, strDivisionList)
        qry = "select NewPP.EMP_CODE AS New_EMP,OldPP.EMP_CODE as Old_EMP from (" & NewPPQry & ") as NewPP right join (" & oldPPQry & ") as OldPP on NewPP.EMP_CODE=OldPP.EMP_CODE where NewPP.EMP_CODE is null"
        Return qry
    End Function
    Public Shared Function GetPFLeftPensionEmpListQuery(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList) As String
        Dim qry As String = ""
        Dim oldPPQry As String = GetPFPensionEMPListQry(strPPOld, strLocationList, strDivisionList)
        Dim NewPPQry As String = GetPFPensionEMPListQry(strPPNew, strLocationList, strDivisionList)
        qry = "select NewPP.EMP_CODE AS New_EMP,OldPP.EMP_CODE as Old_EMP from (" & NewPPQry & ") as NewPP right join (" & oldPPQry & ") as OldPP on NewPP.EMP_CODE=OldPP.EMP_CODE where NewPP.EMP_CODE is null"
        Return qry
    End Function
    Public Shared Function GetPFNewEmpListQuery(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList) As String
        Dim qry As String = ""
        Dim oldPPQry As String = GetPFEMPListQry(strPPOld, strLocationList, strDivisionList)
        Dim NewPPQry As String = GetPFEMPListQry(strPPNew, strLocationList, strDivisionList)
        qry = "select NewPP.EMP_CODE AS New_EMP,OldPP.EMP_CODE as Old_EMP from (" & NewPPQry & ") as NewPP left join (" & oldPPQry & ") as OldPP on NewPP.EMP_CODE=OldPP.EMP_CODE where OldPP.EMP_CODE is null"
        Return qry
    End Function
    Public Shared Function GetPFNewPensionEmpListQuery(ByVal strPPOld As String, ByVal strPPNew As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList) As String
        Dim qry As String = ""
        Dim oldPPQry As String = GetPFPensionEMPListQry(strPPOld, strLocationList, strDivisionList)
        Dim NewPPQry As String = GetPFPensionEMPListQry(strPPNew, strLocationList, strDivisionList)
        qry = "select NewPP.EMP_CODE AS New_EMP,OldPP.EMP_CODE as Old_EMP from (" & NewPPQry & ") as NewPP left join (" & oldPPQry & ") as OldPP on NewPP.EMP_CODE=OldPP.EMP_CODE where OldPP.EMP_CODE is null"
        Return qry
    End Function

    Public Shared Function GetPFEMPListQry(ByVal strPPCode As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList) As String
        Dim qry As String = ""
        qry = "select GSA.EMP_CODE  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE " &
            " INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
            " INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE " &
            " AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE " &
            " where PF_Applicable=1 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & strPPCode & "' and  TSPL_GENERATE_SALARY.LOCATION_CODE in (" & clsCommon.GetMulcallString(strLocationList) & ") and SUB_HEAD_TYPE='EPF' "
        If Not strDivisionList Is Nothing AndAlso strDivisionList.Count > 0 Then
            qry = qry & " and TSPL_GENERATE_SALARY.DEVISION_CODE in (" & clsCommon.GetMulcallString(strDivisionList) & ")"
        End If
        Return qry
    End Function
    Public Shared Function GetPFPensionEMPListQry(ByVal strPPCode As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList) As String
        Dim qry As String = ""
        qry = "select GSA.EMP_CODE  from TSPL_GENERATE_SALARY_PAYHEADS left join tspl_employee_master EMP ON EMP.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE " &
            " INNER JOIN TSPL_GENERATE_SALARY ON TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
            " INNER JOIN TSPL_GENERATE_SALARY_ATTENDANCE GSA ON TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE " &
            " AND GSA.EMP_CODE=TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE " &
            " where PF_Applicable=1 and CoEPF_AMT_AC01>0 and  coalesce(CoEPS_AMT_AC10,0)>0 and TSPL_GENERATE_SALARY.Pay_Period_Code='" & strPPCode & "' and  TSPL_GENERATE_SALARY.LOCATION_CODE in (" & clsCommon.GetMulcallString(strLocationList) & ") and SUB_HEAD_TYPE='EPF' "
        If Not strDivisionList Is Nothing AndAlso strDivisionList.Count > 0 Then
            qry = qry & " and TSPL_GENERATE_SALARY.DEVISION_CODE in (" & clsCommon.GetMulcallString(strDivisionList) & ")"
        End If
        Return qry
    End Function
    Public Shared Function GetLeaveActuarialQuery(ByVal Leave_Code As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal strEmpList As ArrayList, ByVal FromDate As Date, ByVal ToDate As Date) As String
        Dim Finqry As String = ""
        Dim From_Year As String = FromDate.Year
        Dim To_Year As String = ToDate.Year
        Dim WD As String = "WD"
        Dim HD As String = "HD"
        Dim Allot As String = "Allot"
        Dim Avail As String = "Avail"

        '' cols and other dynamic variables
        Dim WDCol As String = "[" & WD & "April-" & From_Year & "], [" & WD & "May-" & From_Year & "], [" & WD & "June-" & From_Year & "], [" & WD & "July-" & From_Year & "], [" & WD & "August-" & From_Year & "],[" & WD & "September-" & From_Year & "],[" & WD & "October-" & From_Year & "],[" & WD & "November-" & From_Year & "],[" & WD & "December-" & From_Year & "],[" & WD & "January-" & To_Year & "],[" & WD & "February-" & To_Year & "],[" & WD & "March-" & To_Year & "]"
        Dim WDMaxCol As String = "max(COALESCE([" & WD & "April-" & From_Year & "],0)) as [" & WD & "April-" & From_Year & "], max(COALESCE([" & WD & "May-" & From_Year & "],0)) as [" & WD & "May-" & From_Year & "], max(COALESCE([" & WD & "June-" & From_Year & "],0)) as [" & WD & "June-" & From_Year & "], max(COALESCE([" & WD & "July-" & From_Year & "],0)) as [" & WD & "July-" & From_Year & "], max(COALESCE([" & WD & "August-" & From_Year & "],0)) as [" & WD & "August-" & From_Year & "],max(COALESCE([" & WD & "September-" & From_Year & "],0)) as [" & WD & "September-" & From_Year & "],max(COALESCE([" & WD & "October-" & From_Year & "],0)) as [" & WD & "October-" & From_Year & "],max(COALESCE([" & WD & "November-" & From_Year & "],0)) as [" & WD & "November-" & From_Year & "],max(COALESCE([" & WD & "December-" & From_Year & "],0)) as [" & WD & "December-" & From_Year & "],max(COALESCE([" & WD & "January-" & To_Year & "],0)) as [" & WD & "January-" & To_Year & "],max(COALESCE([" & WD & "February-" & To_Year & "],0)) as [" & WD & "February-" & To_Year & "],max(COALESCE([" & WD & "March-" & To_Year & "],0)) as [" & WD & "March-" & To_Year & "]"
        Dim WDTotalCol As String = "[" & WD & "April-" & From_Year & "]+ [" & WD & "May-" & From_Year & "]+ [" & WD & "June-" & From_Year & "]+ [" & WD & "July-" & From_Year & "]+ [" & WD & "August-" & From_Year & "]+[" & WD & "September-" & From_Year & "]+[" & WD & "October-" & From_Year & "]+[" & WD & "November-" & From_Year & "]+[" & WD & "December-" & From_Year & "]+[" & WD & "January-" & To_Year & "]+[" & WD & "February-" & To_Year & "]+[" & WD & "March-" & To_Year & "]"
        Dim WDPiv As String = "[" & WD & "April-" & From_Year & "], [" & WD & "May-" & From_Year & "], [" & WD & "June-" & From_Year & "], [" & WD & "July-" & From_Year & "], [" & WD & "August-" & From_Year & "],[" & WD & "September-" & From_Year & "],[" & WD & "October-" & From_Year & "],[" & WD & "November-" & From_Year & "],[" & WD & "December-" & From_Year & "],[" & WD & "January-" & To_Year & "],[" & WD & "February-" & To_Year & "],[" & WD & "March-" & To_Year & "]"

        Dim HDCol As String = "[" & HD & "April-" & From_Year & "], [" & HD & "May-" & From_Year & "], [" & HD & "June-" & From_Year & "], [" & HD & "July-" & From_Year & "], [" & HD & "August-" & From_Year & "],[" & HD & "September-" & From_Year & "],[" & HD & "October-" & From_Year & "],[" & HD & "November-" & From_Year & "],[" & HD & "December-" & From_Year & "],[" & HD & "January-" & To_Year & "],[" & HD & "February-" & To_Year & "],[" & HD & "March-" & To_Year & "]"
        Dim HDMaxCol As String = "max(COALESCE([" & HD & "April-" & From_Year & "],0)) as [" & HD & "April-" & From_Year & "], max(COALESCE([" & HD & "May-" & From_Year & "],0)) as [" & HD & "May-" & From_Year & "], max(COALESCE([" & HD & "June-" & From_Year & "],0)) as [" & HD & "June-" & From_Year & "], max(COALESCE([" & HD & "July-" & From_Year & "],0)) as [" & HD & "July-" & From_Year & "], max(COALESCE([" & HD & "August-" & From_Year & "],0)) as [" & HD & "August-" & From_Year & "],max(COALESCE([" & HD & "September-" & From_Year & "],0)) as [" & HD & "September-" & From_Year & "],max(COALESCE([" & HD & "October-" & From_Year & "],0)) as [" & HD & "October-" & From_Year & "],max(COALESCE([" & HD & "November-" & From_Year & "],0)) as [" & HD & "November-" & From_Year & "],max(COALESCE([" & HD & "December-" & From_Year & "],0)) as [" & HD & "December-" & From_Year & "],max(COALESCE([" & HD & "January-" & To_Year & "],0)) as [" & HD & "January-" & To_Year & "],max(COALESCE([" & HD & "February-" & To_Year & "],0)) as [" & HD & "February-" & To_Year & "],max(COALESCE([" & HD & "March-" & To_Year & "],0)) as [" & HD & "March-" & To_Year & "]"
        Dim HDTotalCol As String = "[" & HD & "April-" & From_Year & "]+ [" & HD & "May-" & From_Year & "]+ [" & HD & "June-" & From_Year & "]+ [" & HD & "July-" & From_Year & "]+ [" & HD & "August-" & From_Year & "]+[" & HD & "September-" & From_Year & "]+[" & HD & "October-" & From_Year & "]+[" & HD & "November-" & From_Year & "]+[" & HD & "December-" & From_Year & "]+[" & HD & "January-" & To_Year & "]+[" & HD & "February-" & To_Year & "]+[" & HD & "March-" & To_Year & "]"
        Dim HDPiv As String = "[" & HD & "April-" & From_Year & "], [" & HD & "May-" & From_Year & "], [" & HD & "June-" & From_Year & "], [" & HD & "July-" & From_Year & "], [" & HD & "August-" & From_Year & "],[" & HD & "September-" & From_Year & "],[" & HD & "October-" & From_Year & "],[" & HD & "November-" & From_Year & "],[" & HD & "December-" & From_Year & "],[" & HD & "January-" & To_Year & "],[" & HD & "February-" & To_Year & "],[" & HD & "March-" & To_Year & "]"

        Dim AllotCol As String = "[" & Allot & "April-" & From_Year & "], [" & Allot & "May-" & From_Year & "], [" & Allot & "June-" & From_Year & "], [" & Allot & "July-" & From_Year & "], [" & Allot & "August-" & From_Year & "],[" & Allot & "September-" & From_Year & "],[" & Allot & "October-" & From_Year & "],[" & Allot & "November-" & From_Year & "],[" & Allot & "December-" & From_Year & "],[" & Allot & "January-" & To_Year & "],[" & Allot & "February-" & To_Year & "],[" & Allot & "March-" & To_Year & "]"
        Dim AllotMaxCol As String = "max(COALESCE([" & Allot & "April-" & From_Year & "],0)) as [" & Allot & "April-" & From_Year & "], max(COALESCE([" & Allot & "May-" & From_Year & "],0)) as [" & Allot & "May-" & From_Year & "], max(COALESCE([" & Allot & "June-" & From_Year & "],0)) as [" & Allot & "June-" & From_Year & "], max(COALESCE([" & Allot & "July-" & From_Year & "],0)) as [" & Allot & "July-" & From_Year & "], max(COALESCE([" & Allot & "August-" & From_Year & "],0)) as [" & Allot & "August-" & From_Year & "],max(COALESCE([" & Allot & "September-" & From_Year & "],0)) as [" & Allot & "September-" & From_Year & "],max(COALESCE([" & Allot & "October-" & From_Year & "],0)) as [" & Allot & "October-" & From_Year & "],max(COALESCE([" & Allot & "November-" & From_Year & "],0)) as [" & Allot & "November-" & From_Year & "],max(COALESCE([" & Allot & "December-" & From_Year & "],0)) as [" & Allot & "December-" & From_Year & "],max(COALESCE([" & Allot & "January-" & To_Year & "],0)) as [" & Allot & "January-" & To_Year & "],max(COALESCE([" & Allot & "February-" & To_Year & "],0)) as [" & Allot & "February-" & To_Year & "],max(COALESCE([" & Allot & "March-" & To_Year & "],0)) as [" & Allot & "March-" & To_Year & "]"
        Dim AllotTotalCol As String = "[" & Allot & "April-" & From_Year & "]+ [" & Allot & "May-" & From_Year & "]+ [" & Allot & "June-" & From_Year & "]+ [" & Allot & "July-" & From_Year & "]+ [" & Allot & "August-" & From_Year & "]+[" & Allot & "September-" & From_Year & "]+[" & Allot & "October-" & From_Year & "]+[" & Allot & "November-" & From_Year & "]+[" & Allot & "December-" & From_Year & "]+[" & Allot & "January-" & To_Year & "]+[" & Allot & "February-" & To_Year & "]+[" & Allot & "March-" & To_Year & "]"
        Dim AllotPiv As String = "[" & Allot & "April-" & From_Year & "], [" & Allot & "May-" & From_Year & "], [" & Allot & "June-" & From_Year & "], [" & Allot & "July-" & From_Year & "], [" & Allot & "August-" & From_Year & "],[" & Allot & "September-" & From_Year & "],[" & Allot & "October-" & From_Year & "],[" & Allot & "November-" & From_Year & "],[" & Allot & "December-" & From_Year & "],[" & Allot & "January-" & To_Year & "],[" & Allot & "February-" & To_Year & "],[" & Allot & "March-" & To_Year & "]"

        Dim AvailCol As String = "[" & Avail & "April-" & From_Year & "], [" & Avail & "May-" & From_Year & "], [" & Avail & "June-" & From_Year & "], [" & Avail & "July-" & From_Year & "], [" & Avail & "August-" & From_Year & "],[" & Avail & "September-" & From_Year & "],[" & Avail & "October-" & From_Year & "],[" & Avail & "November-" & From_Year & "],[" & Avail & "December-" & From_Year & "],[" & Avail & "January-" & To_Year & "],[" & Avail & "February-" & To_Year & "],[" & Avail & "March-" & To_Year & "]"
        Dim AvailMaxCol As String = "max(COALESCE([" & Avail & "April-" & From_Year & "],0)) as [" & Avail & "April-" & From_Year & "], max(COALESCE([" & Avail & "May-" & From_Year & "],0)) as [" & Avail & "May-" & From_Year & "], max(COALESCE([" & Avail & "June-" & From_Year & "],0)) as [" & Avail & "June-" & From_Year & "], max(COALESCE([" & Avail & "July-" & From_Year & "],0)) as [" & Avail & "July-" & From_Year & "], max(COALESCE([" & Avail & "August-" & From_Year & "],0)) as [" & Avail & "August-" & From_Year & "],max(COALESCE([" & Avail & "September-" & From_Year & "],0)) as [" & Avail & "September-" & From_Year & "],max(COALESCE([" & Avail & "October-" & From_Year & "],0)) as [" & Avail & "October-" & From_Year & "],max(COALESCE([" & Avail & "November-" & From_Year & "],0)) as [" & Avail & "November-" & From_Year & "],max(COALESCE([" & Avail & "December-" & From_Year & "],0)) as [" & Avail & "December-" & From_Year & "],max(COALESCE([" & Avail & "January-" & To_Year & "],0)) as [" & Avail & "January-" & To_Year & "],max(COALESCE([" & Avail & "February-" & To_Year & "],0)) as [" & Avail & "February-" & To_Year & "],max(COALESCE([" & Avail & "March-" & To_Year & "],0)) as [" & Avail & "March-" & To_Year & "]"
        Dim AvailTotalCol As String = "[" & Avail & "April-" & From_Year & "]+ [" & Avail & "May-" & From_Year & "]+ [" & Avail & "June-" & From_Year & "]+ [" & Avail & "July-" & From_Year & "]+ [" & Avail & "August-" & From_Year & "]+[" & Avail & "September-" & From_Year & "]+[" & Avail & "October-" & From_Year & "]+[" & Avail & "November-" & From_Year & "]+[" & Avail & "December-" & From_Year & "]+[" & Avail & "January-" & To_Year & "]+[" & Avail & "February-" & To_Year & "]+[" & Avail & "March-" & To_Year & "]"
        Dim AvailPiv As String = "[" & Avail & "April-" & From_Year & "], [" & Avail & "May-" & From_Year & "], [" & Avail & "June-" & From_Year & "], [" & Avail & "July-" & From_Year & "], [" & Avail & "August-" & From_Year & "],[" & Avail & "September-" & From_Year & "],[" & Avail & "October-" & From_Year & "],[" & Avail & "November-" & From_Year & "],[" & Avail & "December-" & From_Year & "],[" & Avail & "January-" & To_Year & "],[" & Avail & "February-" & To_Year & "],[" & Avail & "March-" & To_Year & "]"

        Dim LocList As String = ""
        Dim DivList As String = ""
        Dim EmpList As String = ""
        If Not strLocationList Is Nothing AndAlso strLocationList.Count > 0 Then
            LocList = clsCommon.GetMulcallString(strLocationList)
        End If
        If Not strDivisionList Is Nothing AndAlso strDivisionList.Count > 0 Then
            DivList = clsCommon.GetMulcallString(strDivisionList)
        End If
        If Not strEmpList Is Nothing AndAlso strEmpList.Count > 0 Then
            EmpList = clsCommon.GetMulcallString(strEmpList)
        End If

        '' month qry
        Dim MonthQry As String = ""
        MonthQry = " select 1 as Month_No, 'April-" & From_Year & "' as _Month" &
                   " union all " &
                   " select 2, 'May-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 3, 'June-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 4, 'July-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 5, 'August-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 6, 'September-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 7, 'October-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 8, 'November-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 9, 'December-" & From_Year & "' as _Month " &
                   " union all " &
                   " select 10,'January-" & To_Year & "' as _Month " &
                   " union all " &
                   " select 11,'February-" & To_Year & "' as _Month " &
                   " union all " &
                   " select 12,'March-" & To_Year & "' as _Month"

        '' emp qry
        Dim EMPQry As String = ""
        EMPQry = " select GSA.EMP_CODE,GS.PAY_PERIOD_CODE,PPM.DATE_FROM,PPM.DATE_TO,GSA.PRESENT_DAYS," &
                 " (coalesce(GSA.PAYABLE_DAYS,0)-coalesce(GSA.PRESENT_DAYS,0)-coalesce(GSA.LEAVE_DAYS,0)) as HOLIDAY_DAYS from TSPL_GENERATE_SALARY_ATTENDANCE GSA" &
                 " INNER JOIN TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " &
                 " LEFT JOIN TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " &
                 " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON GSA.EMP_CODE=EMP.EMP_CODE " &
                 " WHERE CAST(PPM.DATE_FROM AS DATE) BETWEEN '" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "' AND '" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "' "
        If clsCommon.myLen(LocList) > 0 Then
            EMPQry = EMPQry & " and EMP.LOCATION_CODE IN (" & LocList & ")"
        End If
        If clsCommon.myLen(DivList) > 0 Then
            EMPQry = EMPQry & " and EMP.DEVISION_CODE IN (" & DivList & ")"
        End If
        If clsCommon.myLen(EmpList) > 0 Then
            EMPQry = EMPQry & " and GSA.EMP_CODE IN (" & EmpList & ")"
        End If

        '' leave qry
        Dim LeaveQry As String = ""
        LeaveQry = "select  LEDGER.EMP_CODE,PAY_PERIOD_CODE,sum(CASE WHEN TR_TYPE ='ALLOT' THEN ALLOTED  ELSE 0 END) AS Allot," &
                   " sum(CASE WHEN TR_TYPE ='AVAIL' THEN AVAILED ELSE 0 END) AS Avail  from TSPL_VIEW_LEAVE_LEDGER LEDGER " &
                   " LEFT JOIN TSPL_LEAVE_MASTER LEAVE ON LEDGER.LEAVE_CODE=LEAVE.LEAVE_CODE " &
                   " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON LEDGER.EMP_CODE=EMP.EMP_CODE " &
                   " where LEDGER.LEAVE_CODE='" & Leave_Code & "' and LEDGER.DATE_FROM BETWEEN '" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "' AND '" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "' "
        If clsCommon.myLen(LocList) > 0 Then
            LeaveQry = LeaveQry & " and EMP.LOCATION_CODE IN (" & LocList & ")"
        End If
        If clsCommon.myLen(DivList) > 0 Then
            LeaveQry = LeaveQry & " and EMP.DEVISION_CODE IN (" & DivList & ")"
        End If
        If clsCommon.myLen(EmpList) > 0 Then
            LeaveQry = LeaveQry & " and LEDGER.EMP_CODE IN (" & EmpList & ")"
        End If
        LeaveQry = LeaveQry & " group by LEDGER.EMP_CODE,PAY_PERIOD_CODE"

        '' opening qry
        Dim OpeningQry As String = ""
        OpeningQry = " select Leave.EMP_CODE,sum(Alloted) as Alloted,sum(Availed) as Availed,(sum(Alloted)-sum(Availed)) as Opening from TSPL_VIEW_LEAVE_LEDGER Leave " &
                     " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON Leave.EMP_CODE=EMP.EMP_CODE " &
                     " where Leave.LEAVE_CODE='" & Leave_Code & "' AND (DATE_FROM<'" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "' or TR_DATE<='" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "')  "

        If clsCommon.myLen(LocList) > 0 Then
            OpeningQry = OpeningQry & " and EMP.LOCATION_CODE IN (" & LocList & ")"
        End If
        If clsCommon.myLen(DivList) > 0 Then
            OpeningQry = OpeningQry & " and EMP.DEVISION_CODE IN (" & DivList & ")"
        End If
        If clsCommon.myLen(EmpList) > 0 Then
            OpeningQry = OpeningQry & " and Leave.EMP_CODE IN (" & EmpList & ")"
        End If
        OpeningQry = OpeningQry & " group by Leave.EMP_CODE"


        '' last drawn salary query
        Dim lastBasicQry As String = ""
        lastBasicQry = " select EMP_CODE,max(RATE_AMOUNT) as LastDrawnBasic,max(seqNo) as Last_Seq from (" &
                       " select GSP.EMP_CODE,GS.PAY_PERIOD_CODE,GSP.RATE_AMOUNT,GSP.ACTUAL_AMOUNT," &
                       " ROW_NUMBER() over (partition by GSP.EMP_CODE order by PPM.DATE_FROM ) AS SeqNo from TSPL_GENERATE_SALARY_PAYHEADS GSP " &
                       " left join TSPL_GENERATE_SALARY GS ON GSP.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " &
                       " left join TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE " &
                       " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON GSP.EMP_CODE=EMP.EMP_CODE " &
                       " WHERE GSP.SUB_HEAD_TYPE='Basic'  AND PPM.DATE_FROM BETWEEN '" & clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") & "' AND '" & clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") & "' "

        If clsCommon.myLen(LocList) > 0 Then
            lastBasicQry = lastBasicQry & " and EMP.LOCATION_CODE IN (" & LocList & ")"
        End If
        If clsCommon.myLen(DivList) > 0 Then
            lastBasicQry = lastBasicQry & " and EMP.DEVISION_CODE IN (" & DivList & ")"
        End If
        If clsCommon.myLen(EmpList) > 0 Then
            lastBasicQry = lastBasicQry & " and GSP.EMP_CODE IN (" & EmpList & ")"
        End If
        lastBasicQry = lastBasicQry & " ) as Sal group by EMP_CODE"

        Finqry = " select OuterMost.EMP_CODE AS empcode,EMP.Emp_Name as empname,emp.Birth_date as dob,Loc.Location_Desc as Location,emp.Joining_date as doj," & WDCol & ",(" & WDTotalCol & ") as WDTotal," & HDCol & ",(" & HDTotalCol & ") as HDTotal,coalesce(Opening.Opening,0) as Opening," & AllotCol & ",(coalesce(Opening.Opening,0)+" & AllotTotalCol & ") as AllotTotal," & AvailCol & ",(" & AvailTotalCol & ") as AvailTotal,((coalesce(Opening.Opening,0)+" & AllotTotalCol & ")-(" & AvailTotalCol & ")) as [Net Balance],LastBasic.LastDrawnBasic,('" & objCommonVar.CurrentCompanyName & " '+ Div.DEVISION_NAME) as DivAddress " &
                 Environment.NewLine & " from (select OutData.EMP_CODE," & WDMaxCol & "," & HDMaxCol & "," & AllotMaxCol & "," & AvailMaxCol & " from ( select * from (select Months.Month_No,('WD'+Months._Month) as WDMonth,('HD'+Months._Month) as HDMonth,('Allot'+Months._Month) as AllotMonth," &
                 Environment.NewLine & " 'Avail'+(Months._Month) as AvailMonth,Innerqry.EMP_CODE,Innerqry.PAY_PERIOD_CODE,Innerqry.DATE_FROM,Innerqry.DATE_TO, " &
                 Environment.NewLine & " Innerqry.WD,Innerqry.HD,coalesce(Innerqry.Allot,0) as Allot,coalesce(Innerqry.Avail,0) as Avail  " &
                 Environment.NewLine & " from (" & MonthQry & ") as Months  " &
                 Environment.NewLine & " Left Join ( " &
                 Environment.NewLine & " select GSA.EMP_CODE,GSA.PAY_PERIOD_CODE, " &
                 Environment.NewLine & " (CAST(datename(month,GSA.DATE_FROM) AS VARCHAR)+'-'+ CAST(YEAR(GSA.DATE_FROM) AS VARCHAR)) AS _GSMonth,GSA.DATE_FROM,GSA.DATE_TO,GSA.PRESENT_DAYS as WD,GSA.HOLIDAY_DAYS as HD,LEAVES.Allot,LEAVES.Avail  " &
                 Environment.NewLine & " from ( " & EMPQry & ") as GSA left join (" & LeaveQry & ") as Leaves on GSA.EMP_CODE=LEAVES.EMP_CODE and GSA.PAY_PERIOD_CODE=LEAVES.PAY_PERIOD_CODE" &
                 Environment.NewLine & " ) as Innerqry on Months._Month=Innerqry._GSMonth " &
                 Environment.NewLine & " ) as Final " &
                 Environment.NewLine & " PIVOT " &
                 Environment.NewLine & " ( " &
                 Environment.NewLine & " max(WD) " &
                 Environment.NewLine & " FOR WDMonth IN (" & WDPiv & ") " &
                 Environment.NewLine & " ) AS PivotWD " &
                 Environment.NewLine & " PIVOT " &
                 Environment.NewLine & " ( " &
                 Environment.NewLine & " max(HD) " &
                 Environment.NewLine & " FOR HDMonth IN (" & HDPiv & ") " &
                 Environment.NewLine & " ) AS PivotHD " &
                 Environment.NewLine & " PIVOT " &
                 Environment.NewLine & " ( " &
                 Environment.NewLine & " max(Allot) " &
                 Environment.NewLine & " FOR AllotMonth IN (" & AllotPiv & ") " &
                 Environment.NewLine & " ) AS PivotAllot " &
                 Environment.NewLine & " PIVOT " &
                 Environment.NewLine & " ( " &
                 Environment.NewLine & " max(Avail) " &
                 Environment.NewLine & " FOR AvailMonth IN (" & AvailPiv & ") " &
                 Environment.NewLine & " ) AS PivotAvail ) as OutData Group by EMP_CODE) as OuterMost " &
                 Environment.NewLine & " left join (" & OpeningQry & ") as Opening on OuterMost.EMP_CODE=Opening.EMP_CODE " &
                 Environment.NewLine & " left join (" & lastBasicQry & ") as LastBasic on OuterMost.EMP_CODE=LastBasic.EMP_CODE " &
                 Environment.NewLine & " left join TSPL_EMPLOYEE_MASTER EMP ON OuterMost.EMP_CODE=EMP.EMP_CODE  " &
                 Environment.NewLine & " left join TSPL_LOCATION_MASTER Loc on EMP.LOCATION_CODE=Loc.Location_Code " &
                 Environment.NewLine & " left join TSPL_DEVISION_MASTER Div on EMP.DEVISION_CODE=Div.DEVISION_CODE where OuterMost.EMP_CODE IS NOT NULL "

        Return Finqry
    End Function
    Public Shared Function GetLeaveActuarialDT(ByVal Leave_Code As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal strEmpList As ArrayList, ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim qry As String = GetLeaveActuarialQuery(Leave_Code, strLocationList, strDivisionList, strEmpList, FromDate, ToDate)
        qry = qry & Environment.NewLine & " order by OuterMost.EMP_CODE"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function GetLeaveActuarialSummaryDT(ByVal Leave_Code As String, ByVal strLocationList As ArrayList, ByVal strDivisionList As ArrayList, ByVal strEmpList As ArrayList, ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim qry As String = GetLeaveActuarialQuery(Leave_Code, strLocationList, strDivisionList, strEmpList, FromDate, ToDate)
        qry = "select empcode,empname,dob,doj,lastDrawnBasic as MonthlySalaryForGratuity,lastDrawnBasic as MonthlySalaryForLeave,[Net Balance],DivAddress as [Location/Division],null as [CTC/Month] from (" & qry & ") as Summary" & Environment.NewLine & " order by Summary.empcode"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function GetLastDrawnSalaryPayPeriod(ByVal EMP_CODE As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = " select top 1 GS.PAY_PERIOD_CODE from TSPL_GENERATE_SALARY_ATTENDANCE GSA " &
                            " INNER JOIN TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " &
                            " inner join TSPL_PAYPERIOD_MASTER PPM on gs.PAY_PERIOD_CODE=ppm.PAY_PERIOD_CODE " &
                            " where GSA.EMP_CODE='" & EMP_CODE & "' order by ppm.DATE_FROM desc"
        Dim PP As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return PP
    End Function
    Public Shared Function GetNextPayPeriod(ByVal Pay_Period_Code As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select top 1 PAY_PERIOD_CODE from TSPL_PAYPERIOD_MASTER where DATE_FROM>=(select max(DATE_TO) " &
            " from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & Pay_Period_Code & "') order by DATE_FROM"
        Dim PP As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return PP
    End Function

    Public Shared Function Generate_Salary(ByVal Pay_Period_Code As String, ByVal EmpList As ArrayList, ByVal trans As SqlTransaction, Optional ByVal Is_Arrear As Boolean = False) As Boolean
        Dim strLog As String = ""
        Dim strTableName As String = ""
        If Is_Arrear = False Then
            strTableName = "TSPL_SALARY_CALCULATION"
        Else
            strTableName = "TSPL_ARREAR_CALCULATION"
        End If
        Dim logFile As String = "c:\ERPTempFolder\salgenlog.txt"
        clsCommon.ProgressBarUpdate("Checking for log file...")
        If System.IO.File.Exists(logFile) Then
            Dim stream As New IO.StreamWriter(logFile, False)
            stream.WriteLine("")
            stream.Close()
        Else
            Dim fs As IO.FileStream = System.IO.File.Create(logFile)
            fs.Close()
        End If
        Dim SettPFCalculationOnHeadValue As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PFCalculationOnFormulaHead, clsFixedParameterCode.PFCalculationOnFormulaHead, trans))
        clsCommon.ProgressBarUpdate("Getting Pay Period Information...")
        Dim objPP As clsPayPeriodMaster = clsPayPeriodMaster.GetData(Pay_Period_Code, NavigatorType.Current, trans)
        Dim PP_START_DATE As Date
        Dim PP_END_DATE As Date
        Dim PayPeriodDays As Integer
        PP_START_DATE = objPP.DATE_FROM
        PP_END_DATE = objPP.DATE_TO
        PayPeriodDays = DateDiff(DateInterval.Day, objPP.DATE_FROM, objPP.DATE_TO) + 1
        Dim strEmp As String = "(" & clsCommon.GetMulcallString(EmpList) & ")"

        clsCommon.ProgressBarUpdate("Collecting the list of working employees...")
        Dim strq As String = "SELECT T1.EMP_STATUS_CODE,T1.EMP_CODE,T1.REVISION_NO,T1.BRANCH_CODE,T1.DESIGNATION_ID,T1.IS_PF_APPL," _
        & " T1.PF_NO, T1.IS_ESI_APPL, T1.ESI_NO, T1.IS_BONUS_APPL, T1.BONUS_CODE, T1.IS_OT_APPL, T1.OT_CODE, T1.WORKING_STATUS " _
        & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
        & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
        & " GROUP BY EMP_CODE  HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "') AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE WHERE T1.EMP_CODE IN " & strEmp & ""
        Dim dt_empStatus As DataTable
        dt_empStatus = clsDBFuncationality.GetDataTable(strq, trans)
        If dt_empStatus.Rows.Count = 0 Then
            Throw New Exception("No Working Employee to generate salary or Unapproved!")
        End If

        clsCommon.ProgressBarUpdate("Collecting Salary Status of working employees...")
        strq = "SELECT TT1.*,TT2.EMP_SAL_CODE,TT2.REVISION_NO AS SAL_REVISION_NO,COALESCE(EMP.EMP_NAME,'') AS EMP_NAME FROM  (" _
               & " SELECT T1.EMP_STATUS_CODE,T1.EMP_CODE,T1.REVISION_NO,T1.BRANCH_CODE,T1.DESIGNATION_ID,T1.IS_PF_APPL," _
               & " T1.PF_NO, T1.IS_ESI_APPL, T1.ESI_NO, T1.IS_BONUS_APPL, T1.BONUS_CODE, T1.IS_OT_APPL, T1.OT_CODE, T1.WORKING_STATUS " _
               & " FROM TSPL_EMPLOYEE_STATUS T1 JOIN ( " _
               & " select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO  from TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "'   " _
               & " GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "') AS T2 ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS TT1 " _
               & " LEFT JOIN ( " _
               & " select EMP_CODE,MAX(EMP_SAL_CODE) AS EMP_SAL_CODE,MAX(REVISION_NO) AS REVISION_NO  " _
               & " from TSPL_EMPLOYEE_SALARY WHERE  APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
               & " GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "') AS TT2 ON TT1.EMP_CODE=TT2.EMP_CODE " _
               & " left join TSPL_EMPLOYEE_MASTER EMP ON TT1.EMP_CODE=EMP.EMP_CODE " _
               & " WHERE TT1.EMP_CODE IN " & strEmp & ""

        Dim dt_salStatus As DataTable
        dt_salStatus = clsDBFuncationality.GetDataTable(strq, trans)
        Dim drNASal() As DataRow = dt_salStatus.Select("EMP_SAL_CODE IS NULL")
        If drNASal.Length > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Employees not having the salary definitions:")
            For intloop As Integer = 0 To drNASal.Length - 1
                objWriter.WriteLine((intloop + 1) & ". " & drNASal(intloop).Item("EMP_CODE") & "-" & drNASal(intloop).Item("EMP_NAME"))
            Next
            objWriter.Close()
            Throw New Exception("Some Working Employee's Salary is not defined or Unapproved !")
            Exit Function
        End If

        clsCommon.ProgressBarUpdate("Calculating Days in each leave status ...")
        Dim salADM As Integer = PayPeriodDays
        Dim salWD As Integer = PayPeriodDays - TotalGLHL(objPP.DATE_FROM, objPP.DATE_TO, trans) - TotalWKHL(objPP.DATE_FROM, objPP.DATE_TO, trans)
        Dim salWDWH As Integer = PayPeriodDays - TotalGLHL(objPP.DATE_FROM, objPP.DATE_TO, trans)
        Dim salWDH As Integer = PayPeriodDays - TotalWKHL(objPP.DATE_FROM, objPP.DATE_TO, trans)
        Dim salFD As Integer = 30
        Dim salUD As Integer = 30
        Dim condSalDays As String = " (CASE WHEN T3.CALC_SAL_ON='ADM' THEN " & salADM & " WHEN T3.CALC_SAL_ON='WD' THEN " & salWD & " WHEN T3.CALC_SAL_ON='WDWH' THEN " & salWDWH & " "
        condSalDays += " WHEN T3.CALC_SAL_ON='WDH' THEN " & salWDH & " WHEN T3.CALC_SAL_ON='FM' THEN " & salFD & " ELSE " & salUD & " END)"

        Dim condPayableDays As String = " (CASE WHEN T3.CALC_SAL_ON='ADM' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS+T2.WEEKLY_OFF_DAYS) WHEN T3.CALC_SAL_ON='WD' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS) WHEN T3.CALC_SAL_ON='WDWH' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.WEEKLY_OFF_DAYS) "
        condPayableDays += " WHEN T3.CALC_SAL_ON='WDH' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS) WHEN T3.CALC_SAL_ON='FM' THEN (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS+T2.WEEKLY_OFF_DAYS) ELSE (T2.PRESENT_DAYS+T2.LEAVE_DAYS+T2.HOLIDAY_DAYS+T2.WEEKLY_OFF_DAYS) END)"

        Dim strqLeave As String = "select LEAVE_CODE from TSPL_LEAVE_MASTER where LEAVE_CODE not in ('EL','CL','PL')"
        Dim dtLeave As DataTable = clsDBFuncationality.GetDataTable(strqLeave, trans)
        Dim leaveCode As String = "('CL','PL','EL'"
        For intloop As Integer = 0 To dtLeave.Rows.Count - 1
            leaveCode = leaveCode & "," & "'" & dtLeave.Rows(intloop).Item("LEAVE_CODE") & "'"
        Next
        leaveCode = leaveCode & ")"

        clsCommon.ProgressBarUpdate("Collecting list of attendance...")
        strq = "SELECT '" & Pay_Period_Code & "' AS PAY_PERIOD_CODE,T1.EMP_CODE,COALESCE(EMP.EMP_NAME,'') AS EMP_NAME,T2.REGISTER_TYPE,T1.ATTENDANCE_CODE,T3.CALC_SAL_ON," & condSalDays & " AS PAYPERIOD_DAYS, " _
               & " T2.PRESENT_DAYS,T2.ABSENT_DAYS,T2.LEAVE_DAYS,T2.HOLIDAY_DAYS,T2.WEEKLY_OFF_DAYS," & condPayableDays & " AS PAYABLE_DAYS, " _
               & " (" & condSalDays & "-" & condPayableDays & ") AS LOP_DAYS,T2.POSTED FROM ( " _
               & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS  FROM TSPL_EMPLOYEE_STATUS T1 " _
               & " INNER JOIN (  select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO " _
               & " FROM TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "'  GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "' ) AS T2" _
               & " ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS T1" & Environment.NewLine _
               & " LEFT JOIN ( " _
               & " SELECT T1.*,'HR' AS REGISTER_TYPE FROM (" _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS,T1.POSTED " _
               & " FROM TSPL_HOURLY_ATTENDANCE T1 INNER JOIN TSPL_HOURLY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE  T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE,T1.POSTED) AS T1 " & Environment.NewLine _
               & " UNION ALL " _
               & " SELECT T1.*,'DL' AS REGISTER_TYPE FROM ( " _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS,T1.POSTED" _
               & " FROM TSPL_DAILY_ATTENDANCE T1 INNER JOIN TSPL_DAILY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE  T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE,T1.POSTED) AS T1  " & Environment.NewLine _
               & " UNION ALL " _
               & " SELECT T2.EMP_CODE,T2.TOTAL_DAYS,T2.PRESENT_DAYS,T2.HOLIDAYS_DAYS,T2.WEEKLY_OFF_DAYS,T2.LEAVE_DAYS,T2.ABSENT_DAYS,T1.POSTED,'MT' AS REGISTER_TYPE " _
               & " FROM TSPL_MONTHLY_ATTENDANCE T1 INNER JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL T2 ON T1.MTA_CODE=T2.MTA_CODE" _
               & " WHERE  T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T2 ON T1.EMP_CODE=T2.EMP_CODE  " _
               & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON T1.EMP_CODE=EMP.EMP_CODE " _
               & " LEFT JOIN TSPL_ATTENDANCE_MASTER T3 ON T1.ATTENDANCE_CODE=T3.ATTENDANCE_CODE WHERE T1.EMP_CODE IN " & strEmp & " "
        Dim dt_attendance As DataTable
        dt_attendance = clsDBFuncationality.GetDataTable(strq, trans)
        Dim drNAAttd() As DataRow
        drNAAttd = dt_attendance.Select("REGISTER_TYPE IS NULL ")
        If drNAAttd.Length > 0 Then
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("List of Employees not having the Attendance:")
            For intloop As Integer = 0 To drNAAttd.Length - 1
                objWriter.WriteLine((intloop + 1) & ". " & drNAAttd(intloop).Item("EMP_CODE") & "-" & drNAAttd(intloop).Item("EMP_NAME"))
            Next
            objWriter.Close()
            Throw New Exception("Some of the employee's attendance is not entered!")
        End If
        clsCommon.ProgressBarUpdate("Saving final attendance...")
        strq = "DELETE FROM TSPL_ATTENDANCE_SUMMARY WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "' "
        If clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
        Else
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Error in saving attendance... :")
            objWriter.Close()
            Throw New Exception("Error in saving attendance... !")
        End If

        strq = "INSERT INTO TSPL_ATTENDANCE_SUMMARY(PAY_PERIOD_CODE,EMP_CODE,REGISTER_TYPE,ATTENDANCE_CODE,CALC_SAL_ON,PAYPERIOD_DAYS," _
               & " PRESENT_DAYS,ABSENT_DAYS,LEAVE_DAYS,HOLIDAY_DAYS,WEEKLY_OFF_DAYS,PAYABLE_DAYS,LOP_DAYS,POSTED,Created_By,Created_Date,Modified_By,Modified_Date) " _
               & " (SELECT '" & Pay_Period_Code & "' AS PAY_PERIOD_CODE,T1.EMP_CODE,T2.REGISTER_TYPE,T1.ATTENDANCE_CODE,T3.CALC_SAL_ON," & condSalDays & " AS PAYPERIOD_DAYS, " _
               & " T2.PRESENT_DAYS,T2.ABSENT_DAYS,T2.LEAVE_DAYS,T2.HOLIDAY_DAYS,T2.WEEKLY_OFF_DAYS," & condPayableDays & " AS PAYABLE_DAYS, " _
               & " (" & condSalDays & "-" & condPayableDays & ") AS LOP_DAYS,1,'" & objCommonVar.CurrentUserCode & "',CURRENT_TIMESTAMP,'" & objCommonVar.CurrentUserCode & "',CURRENT_TIMESTAMP FROM ( " _
               & " SELECT T1.EMP_STATUS_CODE,t1.ATTENDANCE_CODE,T1.EMP_CODE,T1.WORKING_STATUS  FROM TSPL_EMPLOYEE_STATUS T1 " _
               & " INNER JOIN (  select EMP_CODE,MAX(EMP_STATUS_CODE) AS EMP_STATUS_CODE,MAX(REVISION_NO) AS REVISION_NO " _
               & " FROM TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' GROUP BY EMP_CODE HAVING MAX(APPLICABLE_FROM) <= '" & Format(PP_END_DATE, "dd MMM yyyy") & "' ) AS T2" _
               & " ON T1.EMP_STATUS_CODE=T2.EMP_STATUS_CODE) AS T1" _
               & " LEFT JOIN ( " _
               & " SELECT T1.*,'HR' AS REGISTER_TYPE FROM (" _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS " _
               & " FROM TSPL_HOURLY_ATTENDANCE T1 INNER JOIN TSPL_HOURLY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE) AS T1" _
               & " UNION ALL " _
               & " SELECT T1.*,'DL' AS REGISTER_TYPE FROM ( " _
               & " SELECT T2.EMP_CODE,COUNT(T2.ATTENDANCE_DATE) AS ATTENDANCE_DAYS, " _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('P','OD','T') THEN 0.5 ELSE 0 END)) AS PRESENT_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('H','COFF') THEN 0.5 ELSE 0 END)) AS HOLIDAY_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN ('WO') THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN ('WO') THEN 0.5 ELSE 0 END)) AS WEEKLY_OFF_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF IN " & leaveCode & " THEN 0.5 ELSE 0 END)) AS LEAVE_DAYS," _
               & " SUM((CASE WHEN T2.FIRST_HALF='A' THEN 0.5 ELSE 0 END)+(CASE WHEN T2.SECOND_HALF='A' THEN 0.5 ELSE 0 END)) AS ABSENT_DAYS" _
               & " FROM TSPL_DAILY_ATTENDANCE T1 INNER JOIN TSPL_DAILY_ATTENDANCE_DETAIL T2 ON T1.DLA_CODE=T2.DLA_CODE " _
               & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' GROUP BY T2.EMP_CODE) AS T1 " _
               & " UNION ALL " _
               & " SELECT T2.EMP_CODE,T2.TOTAL_DAYS,T2.PRESENT_DAYS,T2.HOLIDAYS_DAYS,T2.WEEKLY_OFF_DAYS,T2.LEAVE_DAYS,T2.ABSENT_DAYS,'MT' AS REGISTER_TYPE " _
               & " FROM TSPL_MONTHLY_ATTENDANCE T1 INNER JOIN TSPL_MONTHLY_ATTENDANCE_DETAIL T2 ON T1.MTA_CODE=T2.MTA_CODE" _
               & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T2 ON T1.EMP_CODE=T2.EMP_CODE " _
               & " LEFT JOIN TSPL_ATTENDANCE_MASTER T3 ON T1.ATTENDANCE_CODE=T3.ATTENDANCE_CODE WHERE T1.EMP_CODE IN " & strEmp & "  )"

        Dim attStatus As Boolean
        attStatus = clsDBFuncationality.ExecuteNonQuery(strq, trans)
        If attStatus = True Then
        Else
            Dim objWriter As New System.IO.StreamWriter(logFile, True)
            objWriter.WriteLine("Error in saving attendance... :")
            objWriter.Close()
            Throw New Exception("Error in saving attendance... !")
        End If
        Dim drPF As DataRow
        Dim drESI As DataRow
        Dim dtPT As DataTable
        PP_START_DATE = objPP.DATE_FROM
        PP_END_DATE = objPP.DATE_TO
        'clsCommon.ProgressBarUpdate("Gathering information regarding PF Rules...")
        'strq = "SELECT T1.PFRULE_CODE,T2.COEPF_PER,T2.COEPF_ROUNDOFF_YPE,T2.COEPS_PER,T2.EPS_MAX,T2.EMPEPF_PER," _
        '      & " T2.EMPEPF_MAX,T2.EMPEPF_ROUNDOFF_YPE,T2.ACCOEPF_PER,T2.ACCOEPF_MAX,T2.COEDLI_PER,T2.ACCOEDLI_PER,T2.COEDLI_MAX," _
        '      & " T2.OC,T2.OC_MAX,T2.OTH_ROUNDOFF_YPE,ACCOEDLI_MAX FROM ( " _
        '      & " SELECT MAX(PFRULE_CODE) AS PFRULE_CODE FROM TSPL_PF_RULE_MASTER " _
        '      & " WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
        '      & " GROUP BY APPLICABLE_FROM) AS T1 LEFT JOIN TSPL_PF_RULE_MASTER T2 ON T1.PFRULE_CODE=T2.PFRULE_CODE"
        'Dim dtPFRules As DataTable
        'dtPFRules = clsDBFuncationality.GetDataTable(strq, trans)
        'If dtPFRules.Rows.Count > 0 Then
        '    drPF = dtPFRules.Rows(dtPFRules.Rows.Count - 1)
        'Else
        '    Throw New Exception("PF Rules not found !")
        'End If


        '' 4. ESI RULES
        clsCommon.ProgressBarUpdate("Gathering information regarding ESI Rules...")
        strq = "SELECT T1.ESIRULE_CODE,T2.COESI_PER,T2.EMPESI_PER,T2.COESI_ROUNDOFF_YPE,T2.TOTALEARNING_MAX FROM (" _
               & " SELECT MAX(ESIRULE_CODE) AS ESIRULE_CODE FROM TSPL_ESI_RULE_MASTER " _
               & " WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
               & " GROUP BY APPLICABLE_FROM) AS T1 LEFT JOIN TSPL_ESI_RULE_MASTER T2 ON T1.ESIRULE_CODE=T2.ESIRULE_CODE"
        Dim dtEsiRules As DataTable
        dtEsiRules = clsDBFuncationality.GetDataTable(strq, trans)
        If dtEsiRules.Rows.Count > 0 Then
            drESI = dtEsiRules.Rows(dtEsiRules.Rows.Count - 1)
        Else
            Throw New Exception("ESI Rules not found !")
        End If

        '' 4. PROFF TAX RULES        
        clsCommon.ProgressBarUpdate("Gathering information regarding PT Rules...")
        strq = "SELECT T1.PT_CODE,T2.SLAB_FROM,T2.SLAB_TO,T2.PT_AMOUNT FROM ( " _
              & " SELECT MAX(PT_CODE) AS PT_CODE FROM TSPL_PT_RULE_MASTER " _
              & " WHERE APPLICABLE_FROM<='" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "' AND STATE_CODE=(SELECT STATE FROM TSPL_COMPANY_MASTER WHERE COMP_CODE='" & objCommonVar.CurrentCompanyCode & "') " _
              & " GROUP BY APPLICABLE_FROM) AS T1 LEFT JOIN TSPL_PT_DETAIL T2 ON T1.PT_CODE=T2.PT_CODE"
        dtPT = clsDBFuncationality.GetDataTable(strq, trans)

        '' TRUNCATE SALARY GENERATION TABLE
        clsCommon.ProgressBarUpdate("Saving raw salary calculation in temprary table for further calculation...")
        If Is_Arrear = False Then
            strq = "TRUNCATE TABLE " & strTableName & ""
            If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                Throw New Exception("Error in truncating Salary Calculation Table !")
            End If
        End If

        '' INSERT EMPLOYEE WAISE SALARY STRUCTURE FROM EMPLOYEE SALARY TABLE
        Dim QryArrear As String = "TSPL_EMPLOYEE_SALARY"
        If Is_Arrear Then
            QryArrear = " (select TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE,TSPL_EMPLOYEE_SALARY.EMP_CODE,TSPL_EMPLOYEE_SALARY.REVISION_NO,TSPL_EMPLOYEE_INCREMENT_HEAD.ARREAR_FROM as Applicable_From " &
                        " from TSPL_EMPLOYEE_INCREMENT_HEAD inner join TSPL_EMPLOYEE_SALARY on TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_SAL_CODE_NEW=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE)"
        End If
        'Dim drPF As DataRow
        'Dim drESI As DataRow
        'Dim dtPT As DataTable
        'PP_START_DATE = objPP.DATE_FROM
        'PP_END_DATE = objPP.DATE_TO
        clsCommon.ProgressBarUpdate("Gathering information regarding PF Rules...")
        strq = "SELECT T1.PFRULE_CODE,T2.COEPF_PER,T2.COEPF_ROUNDOFF_YPE,T2.COEPS_PER,T2.EPS_MAX,T2.EMPEPF_PER," _
              & " T2.EMPEPF_MAX,T2.EMPEPF_ROUNDOFF_YPE,T2.ACCOEPF_PER,T2.ACCOEPF_MAX,T2.COEDLI_PER,T2.ACCOEDLI_PER,T2.COEDLI_MAX," _
              & " T2.OC,T2.OC_MAX,T2.OTH_ROUNDOFF_YPE,ACCOEDLI_MAX FROM ( " _
              & " SELECT MAX(PFRULE_CODE) AS PFRULE_CODE FROM TSPL_PF_RULE_MASTER " _
              & " WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "' " _
              & " GROUP BY APPLICABLE_FROM) AS T1 LEFT JOIN TSPL_PF_RULE_MASTER T2 ON T1.PFRULE_CODE=T2.PFRULE_CODE"
        ' strq = "SELECT T2.EMP_CODE,T2.COEPF_PER,T2.COEPF_ROUNDOFF_YPE,T2.COEPS_PER,T2.EPS_MAX,T2.EMPEPF_PER, T2.EMPEPF_MAX,T2.EMPEPF_ROUNDOFF_YPE,T2.ACCOEPF_PER,T2.ACCOEPF_MAX,T2.COEDLI_PER,T2.ACCOEDLI_PER,T2.COEDLI_MAX, T2.OC,T2.OC_MAX,T2.OTH_ROUNDOFF_YPE,ACCOEDLI_MAX FROM TSPL_EMPLOYEE_MASTER T2 "
        Dim dtPFRules As DataTable
        dtPFRules = clsDBFuncationality.GetDataTable(strq, trans)
        If dtPFRules.Rows.Count > 0 Then
            drPF = dtPFRules.Rows(dtPFRules.Rows.Count - 1)
        Else
            Throw New Exception("PF Rules not found !")
        End If


        strq = "INSERT INTO " & strTableName & " ( EMP_CODE, EMP_SAL_CODE,SALARY_STRUCTURE_CODE,ISHIDDENCOMPONENT,PAY_HEAD_CODE, LINE_NO, " _
           & " HEAD_TYPE,SUB_HEAD_TYPE,CALC_BASIS,PAYHEAD_FORMULA,RATE_AMOUNT,STD_AMOUNT,PAYPERIOD_DAYS, " _
           & " PRESENT_DAYS,ABSENT_DAYS,LEAVE_DAYS,HOLIDAY_DAYS,PAYABLE_DAYS,LOP_DAYS,IS_PF_APPL,IS_PF_ATTN_ENABLE, " _
           & " PF_MAX_LIM,EPS_TO_EPF,EPS_MAX,COEPF_ROUNDOFF_YPE,EMPEPF_ROUNDOFF_YPE,IS_ESI_APPL,FORMULA_HEAD," _
           & " IS_OT_APPL,OT_CODE,OT_HOURS,OT_RATE,HOUR_MULTIPLIER,IS_ASPER_ACTUAL_CALC,IS_BONUS_APPL,BONUS_CODE,ACTUAL_AMOUNT,PAYABLE_AMOUNT,ROUND_OFF_TYPE,MAX_AMOUNT,EMP_STATUS_CODE,ESI_MAX_LIM,EPF_RATE,ESI_RATE,PAY_PERIOD_CODE," _
           & " PF_Calculation_Type,PF_Rule_Max_Lim,Custom_PF_Max_Lim,PF_No,ESI_Calculation_Type,ESI_Rule_Max_Lim,Custom_ESI_Max_Lim,ESI_No,OD_Applicable,Is_Earning_Payhead,Is_Professional_Tax_Applicable,ISESI " _
           & " )( " _
           & " SELECT DISTINCT(T1.EMP_CODE),T1.EMP_SAL_CODE,T1.SALARY_STRUCTURE_CODE,T2.ISHIDDENCOMPONENT,T2.PAY_HEAD_CODE,T2.LINE_NO,T4.HEAD_TYPE,T4.SUB_HEAD_TYPE," _
           & " T4.CALC_BASIS,T2.PAYHEAD_FORMULA, " _
           & " (CASE WHEN T4.HEAD_TYPE IN ('ATTN', 'FIXED','F') and (t4.SUB_HEAD_TYPE not in('COPF')) THEN T2.RATE_AMOUNT WHEN (t4.SUB_HEAD_TYPE in('COPF') and t4.ISEARNING=0) THEN T7.EMPEPF_PER WHEN T4.HEAD_TYPE = 'UD' THEN COALESCE(T8.ALLOWANCE_AMOUNT,0) ELSE 0 End) AS RATE , " _
           & " (CASE WHEN T4.HEAD_TYPE IN ('ATTN', 'FIXED','F') and (t4.SUB_HEAD_TYPE not in('COPF')) THEN T2.RATE_AMOUNT WHEN (t4.SUB_HEAD_TYPE in('COPF') and t4.ISEARNING=0) THEN T7.EMPEPF_PER WHEN T4.HEAD_TYPE = 'UD' THEN COALESCE(T8.ALLOWANCE_AMOUNT,0) ELSE 0 End) AS STD_AMOUNT , " _
           & " T6.PAYPERIOD_DAYS,T6.PRESENT_DAYS,T6.ABSENT_DAYS,T6.LEAVE_DAYS,T6.HOLIDAY_DAYS,T6.PAYABLE_DAYS,T6.LOP_DAYS,T7.IS_PF_APPL, " _
           & " " + SettPFCalculationOnHeadValue + " AS IS_PF_ATTN_ENABLE,(CASE WHEN T7.Max_Amount_EPF>0  THEN T7.Max_Amount_EPF ELSE PF_Rule_Max_Lim END) AS EPF_MAX_LIM,T7.EPS_TO_EPF,EPS_MAX AS EPS_MAX,COEPF_ROUNDOFF_YPE AS COEPF_ROUNDOFF_YPE,EMPEPF_ROUNDOFF_YPE AS EMPEPF_ROUNDOFF_YPE, " _
           & " T7.IS_ESI_APPL,T2.PAYHEAD_FORMULA AS FORMULA_HEAD,T7.IS_OT_APPL,T7.OT_CODE,NULL AS OT_HOURS,NULL AS OT_RATE, " _
           & " COALESCE(T9.HOUR_MULTIPLIER,1) AS HOUR_MULTIPLIER ,COALESCE(T9.IS_ASPER_ACTUAL_CALC,0) AS IS_ASPER_ACTUAL_CALC,T7.IS_BONUS_APPL,T7.BONUS_CODE, " _
           & " (ROUND(CASE WHEN T4.HEAD_TYPE ='ATTN' THEN T2.RATE_AMOUNT  * T6.PAYABLE_DAYS / T6.PAYPERIOD_DAYS WHEN T4.HEAD_TYPE ='FIXED' THEN T2.RATE_AMOUNT WHEN T4.HEAD_TYPE = 'UD' THEN COALESCE(T8.ALLOWANCE_AMOUNT,0) ELSE 0 End ,3)) AS ACTUAL_AMOUNT, " _
           & " 0 AS PAYABLE_AMOUNT,T4.ROUND_OFF_TYPE,T2.MAX_AMOUNT,T7.EMP_STATUS_CODE,(CASE WHEN T7.Max_Amount_ESI>0 THEN T7.Max_Amount_ESI ELSE " & drESI.Item("TOTALEARNING_MAX") & " END) AS ESI_MAX_LIM,T7.EPF_RATE,T7.ESI_RATE,'" & Pay_Period_Code & "',T7.PF_Calculation_Type,T7.PF_Rule_Max_Lim,T7.Custom_PF_Max_Lim,T7.PF_No,T7.ESI_Calculation_Type,T7.ESI_Rule_Max_Lim,T7.Custom_ESI_Max_Lim,T7.ESI_No,T7.OD_Applicable,T4.ISEARNING,T7.Is_Professional_Tax_Applicable,T7.ISESI FROM " _
           & " ( " _
           & " SELECT T1.EMP_SAL_CODE,T1.EMP_CODE,T1.SALARY_STRUCTURE_CODE,T1.APPLICABLE_FROM AS APP_DATE " _
           & " FROM TSPL_EMPLOYEE_SALARY T1 " _
           & " INNER JOIN ( " _
           & "  " _
           & "  " _
           & " select TSPL_EMPLOYEE_SALARY.EMP_CODE,TSPL_EMPLOYEE_SALARY.REVISION_NO,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM,TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE from  " _
           & " (SELECT  tt.EMP_CODE,max(REVISION_NO) as REVISION_NO,max(convert(date,tt.APPLICABLE_FROM,103)) as APPLICABLE_FROM " _
           & " FROM 	 (select EMP_CODE,max(convert(date,APPLICABLE_FROM,103)) as APPLICABLE_FROM From  " & QryArrear & " as TSPL_EMPLOYEE_SALARY " _
           & " where  convert(date,APPLICABLE_FROM,103) <= convert(date,'" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "',103) " _
           & " group by EMP_CODE " _
           & " )dt " _
           & "        Left Join " _
           & " TSPL_EMPLOYEE_SALARY as tt	" _
           & " on dt.EMP_CODE=tt.EMP_CODE " _
           & " and convert(date,dt.APPLICABLE_FROM,103)=convert(date,tt.APPLICABLE_FROM,103) " _
           & " group by tt.EMP_CODE  " _
           & " )tt  " _
           & " left join 	TSPL_EMPLOYEE_SALARY " _
           & " on tt.REVISION_NO=TSPL_EMPLOYEE_SALARY.REVISION_NO and tt.APPLICABLE_FROM=TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM  " _
           & " and tt.EMP_CODE=TSPL_EMPLOYEE_SALARY.EMP_CODE " _
           & "        where 1 = 1  " _
           & " group by TSPL_EMPLOYEE_SALARY.emp_code,TSPL_EMPLOYEE_SALARY.REVISION_NO,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM,TSPL_EMPLOYEE_SALARY.emp_sal_code " _
           & "  " _
           & "  " _
           & "  " _
           & "  " _
           & "  " _
           & " ) AS T2 ON T1.EMP_SAL_CODE = T2.EMP_SAL_CODE AND T1.EMP_CODE = T2.EMP_CODE) AS T1 " _
           & " LEFT JOIN TSPL_EMPLOYEE_SALARY_PAYHEADS T2 ON T1.EMP_SAL_CODE = T2.EMP_SAL_CODE " _
           & " LEFT JOIN TSPL_SALSTRUCT_PAYHEADS T3 ON T1.SALARY_STRUCTURE_CODE = T3.SALARY_STRUCTURE_CODE  And T3.PAY_HEAD_CODE = T2.PAY_HEAD_CODE  " _
           & " LEFT JOIN TSPL_PAYHEAD_MASTER T4 ON T2.PAY_HEAD_CODE = T4.PAY_HEAD_CODE " _
           & " LEFT JOIN (" _
           & " SELECT TAS.EMP_CODE,TAS.PAY_PERIOD_CODE,COALESCE(TAS.PRESENT_DAYS,0) as PRESENT_DAYS, " _
           & " TAS.PAYPERIOD_DAYS,TAS.ABSENT_DAYS,TAS.LEAVE_DAYS,TAS.HOLIDAY_DAYS, TAS.PAYABLE_DAYS, TAS.LOP_DAYS " _
           & " FROM TSPL_ATTENDANCE_SUMMARY AS TAS " _
           & " WHERE TAS.PAY_PERIOD_CODE = '" & Pay_Period_Code & "' " _
           & " ) AS T6 ON T1.EMP_CODE = T6.EMP_CODE " _
           & " LEFT JOIN ( " _
           & " SELECT isnull( T_2.ISESI,0) as ISESI,T1.*, T2.IS_PF_APPL,COALESCE(T2.EPS_TO_EPF, 0) AS EPS_TO_EPF,IS_ESI_APPL,T2.IS_OT_APPL,T2.OT_CODE,T2.IS_BONUS_APPL,T2.BONUS_CODE,T2.WORKING_STATUS,T2.Max_Amount_EPF,T2.Max_Amount_ESI,T2.EPF_RATE,T2.ESI_RATE,T2.PF_Calculation_Type,EMPEPF_MAX AS PF_Rule_Max_Lim,T_2.EPS_MAX,T_2.COEPF_ROUNDOFF_YPE,T_2.EMPEPF_ROUNDOFF_YPE,T2.Max_Amount_EPF AS Custom_PF_Max_Lim,T2.PF_No,'' as ESI_Calculation_Type," & drESI.Item("TOTALEARNING_MAX") & " AS ESI_Rule_Max_Lim,T2.Max_Amount_ESI AS Custom_ESI_Max_Lim,T2.ESI_No,T2.IS_OD_APPL OD_Applicable,T2.Professional_Tax_Applicable as Is_Professional_Tax_Applicable ,T_2.EMPEPF_PER " _
           & " FROM " _
           & " ( " _
           & " SELECT EMP_STATUS_CODE,EMPSTATUS.EMP_CODE,EMPSTATUS.REVISION_NO FROM TSPL_EMPLOYEE_STATUS INNER JOIN ( " _
           & " SELECT  MAX(REVISION_NO) AS REVISION_NO,EMP_CODE  FROM TSPL_EMPLOYEE_STATUS WHERE APPLICABLE_FROM<='" & Format(PP_END_DATE, "dd MMM yyyy") & "'   Group BY EMP_CODE " _
           & " HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "') AS EMPSTATUS ON TSPL_EMPLOYEE_STATUS.EMP_CODE=EMPSTATUS.EMP_CODE " _
           & " AND EMPSTATUS.REVISION_NO=TSPL_EMPLOYEE_STATUS.REVISION_NO " _
           & " ) AS T1 " _
           & " LEFT JOIN TSPL_EMPLOYEE_STATUS T2  ON T1.EMP_STATUS_CODE = T2.EMP_STATUS_CODE  " _
           & " INNER JOIN TSPL_EMPLOYEE_MASTER T_2 " _
           & " on T2.EMP_CODE=T_2.EMP_CODE ) AS T7 ON T1.EMP_CODE = T7.EMP_CODE " _
           & " LEFT JOIN (SELECT ALWD.* from TSPL_ALLOWANCE ALW INNER JOIN TSPL_ALLOWANCE_DETAIL ALWD " _
           & " ON ALW.ALLOWANCE_CODE=ALWD.ALLOWANCE_CODE WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "'  " _
           & " UNION ALL " _
           & " SELECT DND.* FROM TSPL_DEDUCTION DN INNER JOIN TSPL_DEDUCTION_DETAIL DND " _
           & " ON DN.DEDUCTION_CODE=DND.DEDUCTION_CODE WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "' ) T8 " _
           & " ON T1.EMP_CODE=T8.EMP_CODE AND T2.PAY_HEAD_CODE=T8.PAY_HEAD_CODE " _
           & " LEFT JOIN (select OT_CODE,HOUR_MULTIPLIER,OT_RATE as OT_RATE,IS_ASPER_ACTUAL_CALC from TSPL_OT_MASTER) AS  T9 ON T7.OT_CODE=T9.OT_CODE " _
           & " WHERE T2.PAY_HEAD_CODE IS NOT NULL AND T1.EMP_CODE IN " & strEmp & ") "

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in inserting Salary Calculation Table !")
        End If
        clsCommon.ProgressBarUpdate("updating attendance based and fixed pay heads...")
        strq = "UPDATE " & strTableName & "  SET RATE_AMOUNT=" _
        & " (CASE WHEN " & strTableName & ".SUB_HEAD_TYPE = 'COPF' THEN TSPL_EMPLOYEE_MASTER.EMPEPF_PER " _
        & " WHEN " & strTableName & ".SUB_HEAD_TYPE ='COEPS' THEN TSPL_EMPLOYEE_MASTER.COEPS_PER " _
        & " WHEN " & strTableName & ".SUB_HEAD_TYPE ='COPF'  THEN (CASE WHEN " & strTableName & ".EPF_RATE>0 THEN " & strTableName & ".EPF_RATE ELSE (case when " & strTableName & ".PF_Calculation_Type='PR' then TSPL_EMPLOYEE_MASTER.EMPEPF_PER  else " & strTableName & ".EPF_RATE end) END) " _
        & " WHEN " & strTableName & ".SUB_HEAD_TYPE ='COESI'  THEN " & drESI.Item("COESI_PER") & "" _
        & " WHEN " & strTableName & ".SUB_HEAD_TYPE ='EMPESI'  THEN (case when " & strTableName & ".ISESI=0 then 0 else (CASE WHEN ESI_RATE>0 THEN ESI_RATE ELSE " & drESI.Item("EMPESI_PER") & " END) end) " _
        & " END), CoEPF_RATE_AC01=(CASE WHEN SUB_HEAD_TYPE ='COPF' THEN TSPL_EMPLOYEE_MASTER.COEPF_PER ELSE 0 END),CoEPS_RATE_AC10=(CASE WHEN SUB_HEAD_TYPE ='COPF' THEN TSPL_EMPLOYEE_MASTER.COEPS_PER ELSE 0 END),ADMIN_RATE_AC02=(CASE WHEN SUB_HEAD_TYPE ='COPF' THEN TSPL_EMPLOYEE_MASTER.ACCOEPF_PER ELSE 0 END)," _
        & " EDLI_RATE_AC21=(CASE WHEN SUB_HEAD_TYPE ='COPF' THEN TSPL_EMPLOYEE_MASTER.COEDLI_PER ELSE 0 END),ADMIN_EDLI_RATE_AC22=(CASE WHEN SUB_HEAD_TYPE ='COPF' THEN TSPL_EMPLOYEE_MASTER.ACCOEDLI_PER ELSE 0 END), " _
        & " OTHER_CHARGE=(CASE WHEN SUB_HEAD_TYPE ='COPF' THEN TSPL_EMPLOYEE_MASTER.OC ELSE 0 END),Co_ESI_RATE=(CASE WHEN SUB_HEAD_TYPE ='EMPESI' THEN " & drESI.Item("COESI_PER") & " ELSE 0 END) FROM TSPL_SALARY_CALCULATION 
        left JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_SALARY_CALCULATION.EMP_CODE WHERE " & strTableName & ".SUB_HEAD_TYPE in ('COPF','COEPS','COESI','EMPESI') and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "' and " & strTableName & ".EMP_CODE IN " & strEmp & " "
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating attendance based and fixedpay heads !")
        End If

        ''adjustment IN PRINCIPAL AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in principal amount of pay heads...")
        strq = "UPDATE " & strTableName & " SET RATE_AMOUNT=RATE_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS,ACTUAL_AMOUNT=ACTUAL_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS, " _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and ADJUSTMENT_TYPE='PA' and TPH.HEAD_TYPE NOT IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & strTableName & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & strTableName & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Adjustment !")
        End If

        ''adjustment IN ARREAR AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in ADJSTMENT IN ARREAR pay heads...")
        strq = "UPDATE " & strTableName & " SET RATE_AMOUNT=RATE_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS," _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS,ARREAR_AMT=T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and ADJUSTMENT_TYPE='AR' and TPH.HEAD_TYPE NOT IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & strTableName & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & strTableName & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Adjustment !")
        End If

        '' UPDATE ACCOUNT_CODE OF "& strTableName &" for non loan advance payheads
        clsCommon.ProgressBarUpdate("updating GL accounts of non loan/advance pay heads...")
        strq = "update " & strTableName & " set " & strTableName & ".account_code=TSPL_PAYHEAD_MASTER.account_code,Employer_Account=TSPL_PAYHEAD_MASTER.GL_Employer_Account from TSPL_PAYHEAD_MASTER " &
               " where " & strTableName & ".PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in updating payhead gl account in Salary Calculation Table !")
        End If

        '' UPDATE ACCOUNT_CODE OF "& strTableName &" for loan advance payheads
        clsCommon.ProgressBarUpdate("updating GL accounts of loa/advancen pay head...")
        strq = "update " & strTableName & " set " & strTableName & ".account_code=TSPL_EMPLOYEE_MASTER.ADVANCE_TO_STAFF from TSPL_EMPLOYEE_MASTER " &
               " where " & strTableName & ".EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE AND " & strTableName & ".SUB_HEAD_TYPE='Loan' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in updating advance gl account in Salary Calculation Table !")
        End If

        '' update formula coluns of user defined pay heads
        strq = "UPDATE " & strTableName & " SET FORMULA_HEAD = '0',FORMULA_AMOUNT='0' WHERE HEAD_TYPE IN ('UD') and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Formula Pay Heads !")
        End If


        '' UPDATE ATTENDANCE BASED AND FIXED PAY HEADS
        'clsCommon.ProgressBarUpdate("updating attendance based and fixed pay heads...")
        'strq = "UPDATE " & strTableName & "  SET RATE_AMOUNT=" _
        '& " (CASE WHEN " & strTableName & ".SUB_HEAD_TYPE ='COPF'  THEN " & drPF.Item("COEPF_PER") & "" _
        '& " WHEN " & strTableName & ".SUB_HEAD_TYPE ='COEPS'  THEN " & drPF.Item("COEPS_PER") & "" _
        '& " WHEN " & strTableName & ".SUB_HEAD_TYPE ='EPF'  THEN (CASE WHEN EPF_RATE>0 THEN EPF_RATE ELSE (case when PF_Calculation_Type='PR' then " & drPF.Item("EMPEPF_PER") & "  else EPF_RATE end) END) " _
        '& " WHEN " & strTableName & ".SUB_HEAD_TYPE ='COESI'  THEN " & drESI.Item("COESI_PER") & "" _
        '& " WHEN " & strTableName & ".SUB_HEAD_TYPE ='EMPESI'  THEN (case when ISESI=0 then 0 else (CASE WHEN ESI_RATE>0 THEN ESI_RATE ELSE " & drESI.Item("EMPESI_PER") & " END) end) " _
        '& " END), CoEPF_RATE_AC01=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("COEPF_PER") & " ELSE 0 END),CoEPS_RATE_AC10=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("COEPS_PER") & " ELSE 0 END),ADMIN_RATE_AC02=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("ACCOEPF_PER") & " ELSE 0 END)," _
        '& " EDLI_RATE_AC21=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("COEDLI_PER") & " ELSE 0 END),ADMIN_EDLI_RATE_AC22=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("ACCOEDLI_PER") & " ELSE 0 END), " _
        '& " OTHER_CHARGE=(CASE WHEN SUB_HEAD_TYPE ='EPF' THEN " & drPF.Item("OC") & " ELSE 0 END),Co_ESI_RATE=(CASE WHEN SUB_HEAD_TYPE ='EMPESI' THEN " & drESI.Item("COESI_PER") & " ELSE 0 END) WHERE " & strTableName & ".SUB_HEAD_TYPE in ('COPF','COEPS','EPF','COESI','EMPESI') and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        'If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
        '    Throw New Exception("Error in Updating attendance based and fixedpay heads !")
        'End If




        '' SECOND PART OF UPDATE ATTENDANCE BASED AND FIXED PAY HEADS
        strq = "UPDATE " & strTableName & "" _
              & " SET FORMULA_AMOUNT = CAST(RATE_AMOUNT AS VARCHAR) + '' + (" _
              & " CASE" _
              & " WHEN HEAD_TYPE = 'ATTN' THEN " _
              & " '*(' + CAST(COALESCE (PAYABLE_DAYS, 0) / COALESCE (PAYPERIOD_DAYS, 1) AS VARCHAR) + ')' " _
              & " ELSE '*1' End ),Pay_Days_Ratio=( CASE WHEN HEAD_TYPE = 'ATTN' THEN    CAST(COALESCE (PAYABLE_DAYS, 0) / COALESCE (PAYPERIOD_DAYS, 1) AS VARCHAR)    ELSE '' End )" _
              & " WHERE HEAD_TYPE IN ('ATTN','FIXED') and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating attendance based and fixedpay heads !")
        End If


        strq = " update " & strTableName & " set Pay_Days_Ratio=(select top 1 innTable.Pay_Days_Ratio from " & strTableName & " as innTable WHERE innTable.HEAD_TYPE ='ATTN' and innTable.PAY_PERIOD_CODE='" & Pay_Period_Code & "' and innTable.EMP_CODE=" & strTableName & ".EMP_CODE )  WHERE HEAD_TYPE not IN ('ATTN') and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Pay Days Ratio !")
        End If

        '' update FIXED PAY HEADS for nill attendance
        strq = "UPDATE " & strTableName & "" _
              & " SET Actual_Amount =0 " _
              & " WHERE HEAD_TYPE IN ('FIXED') and PAYABLE_DAYS=0 and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating fixedpay heads !")
        End If


        clsCommon.ProgressBarUpdate("updating loan/advance pay heads...")
        strq = "UPDATE  " & strTableName & " SET ACTUAL_AMOUNT=0 WHERE SUB_HEAD_TYPE='LOAN' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Loan !")
        End If

        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT=(T1.EMI+COALESCE(T2.ADJUSTMENT_PLUS,0)-COALESCE(T2.ADJUSTMENT_MINUS,0)) from ( " _
             & " select T2.EMP_CODE,T2.EMI_AMOUNT AS EMI FROM TSPL_LOAN_GENERATION T1  " _
             & " INNER JOIN TSPL_LOANGENERATION_DETAIL T2 ON T1.LOAN_GENERATION_CODE=T2.LOAN_GENERATION_CODE " _
             & " WHERE  T2.PAY_PERIOD_CODE='" & Pay_Period_Code & "' ) AS T1 " _
             & " LEFT JOIN (SELECT EMP_CODE,SUM(ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS," _
             & " SUM(ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS  FROM TSPL_LOAN_ADJUSTMENT " _
             & " WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "'  GROUP BY EMP_CODE) AS T2" _
             & " ON T1.EMP_CODE=T2.EMP_CODE" _
             & " WHERE " & strTableName & ".EMP_CODE=T1.EMP_CODE AND  SUB_HEAD_TYPE='LOAN' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Loan !")
        End If

        '' bonus calculation/generation
        clsCommon.ProgressBarUpdate("updating Bonus pay heads...")
        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT =0 WHERE SUB_HEAD_TYPE='BONUS' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Bonus !")
        End If

        strq = "UPDATE  " & strTableName & " SET ACTUAL_AMOUNT=T1.BONUS_AMOUNT,BONUS_CODE=T1.BONUS_CODE," _
             & " BONUS_FROM_PAY_PERIOD_CODE=T1.FROM_PAY_PERIOD_CODE,BONUS_TO_PAY_PERIOD_CODE=T1.TO_PAY_PERIOD_CODE  " _
             & " FROM (SELECT T1.FROM_PAY_PERIOD_CODE,T1.TO_PAY_PERIOD_CODE,T2.EMP_CODE,T2.BONUS_AMOUNT," _
             & " T2.BONUS_CODE FROM TSPL_EMPLOYEE_BONUS T1  " _
             & " INNER JOIN TSPL_EMPBONUS_DETAIL T2 ON T1.EMP_BONUS_CODE=T2.EMP_BONUS_CODE" _
             & " WHERE  T1.PAYABLE_PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T1 " _
             & " WHERE " & strTableName & ".EMP_CODE=T1.EMP_CODE AND  SUB_HEAD_TYPE='BONUS' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Bonus !")
        End If

        '' REIMBURSEMENT AMOUNT CALCULATION 
        clsCommon.ProgressBarUpdate("updating reimbursement pay heads...")
        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT=T1.REIMBURSEMENT_AMOUNT FROM " _
        & " (select T1.EMP_CODE,T2.PAY_HEAD_CODE,SUM(COALESCE(T2.REIMBURSEMENT_AMOUNT,0)) AS REIMBURSEMENT_AMOUNT  " _
        & " from TSPL_EMP_REIMBURSEMENT T1 " _
        & " INNER JOIN  TSPL_EMPREIMBURSEMENT_DETAIL T2 " _
        & " ON T1.REIMBURSEMENT_CODE=T2.REIMBURSEMENT_CODE " _
        & " WHERE T1.PAY_PERIOD_CODE='" & Pay_Period_Code & "' " _
        & " GROUP BY T1.EMP_CODE,T2.PAY_HEAD_CODE) AS T1" _
        & " WHERE(" & strTableName & ".EMP_CODE = T1.EMP_CODE) " _
        & " AND " & strTableName & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Reimbursement !")
        End If


        '' update formula coluns of user defined pay heads
        clsCommon.ProgressBarUpdate("updating user defined pay heads...")
        strq = "UPDATE " & strTableName & " SET FORMULA_HEAD = RATE_AMOUNT,FORMULA_AMOUNT=RATE_AMOUNT WHERE HEAD_TYPE IN ('UD') and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Formula Pay Heads !")
        End If


        '' LOOP QUERY FOR EACH PAY HEADS
        If Is_Arrear = False Then
            clsCommon.ProgressBarUpdate("updating formula based pay heads...")
        Else
            clsCommon.ProgressBarUpdate("updating Arrears(" & Pay_Period_Code & ")...")
        End If

        updateSalary("1", strTableName, Pay_Period_Code, drPF, Is_Arrear, PP_END_DATE, drESI, dtPT, objPP, EmpList, trans)

        clsCommon.ProgressBarUpdate("updating HRA Max...")
        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT =(CASE WHEN ( " & strTableName & ".ACTUAL_AMOUNT>TSPL_PAYHEAD_MASTER.MaximumHRA and TSPL_PAYHEAD_MASTER.MaximumHRA>0) THEN TSPL_PAYHEAD_MASTER.MaximumHRA " &
              " ELSE  " & strTableName & ".ACTUAL_AMOUNT END) FROM  " & strTableName & " INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE= " & strTableName & ".SUB_HEAD_TYPE WHERE  " & strTableName & ".SUB_HEAD_TYPE='HRA' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in updating HRA Maximum Amount !")
        End If


        ''OT should be at last because it will use gross salary of employee.
        clsCommon.ProgressBarUpdate("updating OT...")
        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT=0 where SUB_HEAD_TYPE='OT' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating OT !")
        End If
        '' calculation of OT
        '' get settings
        Dim WorkingHours As Decimal = 0
        Dim TreatExcessLeaveAbsentSett As String
        strq = "select Description from TSPL_FIXED_PARAMETER where Type='WorkingHours'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            WorkingHours = dt.Rows(0).Item("Description")
        Else
            WorkingHours = 8
        End If
        strq = "select Description from TSPL_FIXED_PARAMETER where Type='TreatExcessLeaveAbsent'"
        dt = clsDBFuncationality.GetDataTable(strq, trans)
        If dt.Rows.Count > 0 Then
            TreatExcessLeaveAbsentSett = dt.Rows(0).Item("Description")
        Else
            TreatExcessLeaveAbsentSett = "0"
        End If

        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT = 0,OT_CODE=T11.OT_CODE,OT_HOURS=T11.OT_HOURS,OT_RATE=(CASE WHEN IS_ASPER_ACTUAL_CALC=0 THEN T11.OT_RATE ELSE COALESCE(HEAD_VALUE,0)/PAYPERIOD_DAYS END) " _
        & " FROM ( " _
        & " SELECT T1.OT_CODE,T1.EMP_CODE,T1.OT_HOURS,T1.OT_RATE,T1.OT_TOTAL_AMOUNT FROM TSPL_OT_SHEET T1 " _
        & " WHERE PAY_PERIOD_CODE='" & Pay_Period_Code & "') AS T11 WHERE " & strTableName & ".EMP_CODE=T11.EMP_CODE " _
        & " AND SUB_HEAD_TYPE='OT' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating OT !")
        End If

        ''  UPDATE OT HOURS IN CASE OF EXCESS LEAVE ON THE BASIS OF SETTING
        If clsCommon.CompairString(TreatExcessLeaveAbsentSett, "0") = CompairStringResult.Equal Then
        Else
            strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT = 0,OT_HOURS=(" & strTableName & ".OT_HOURS-EMP_LEAVE.EXCESS_LEAVE * " & WorkingHours & ")  " _
            & " FROM (SELECT TSPL_EMPLOYEE_MASTER.EMP_CODE,COALESCE(EMP_LEAVE.EXCESS_LEAVE,0) as EXCESS_LEAVE FROM TSPL_EMPLOYEE_MASTER left join " _
            & " (SELECT EMP_CODE,(SUM(AVAILED)-SUM(ALLOTED)) AS EXCESS_LEAVE FROM TSPL_VIEW_LEAVE_DETAIL inner join TSPL_PAYPERIOD_MASTER " _
            & " on TSPL_VIEW_LEAVE_DETAIL.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE where TSPL_VIEW_LEAVE_DETAIL.LEAVE_DATE<='" & clsCommon.GetPrintDate(PP_END_DATE, "dd/MMM/yyyy") & "' " _
            & " GROUP BY EMP_CODE HAVING (SUM(AVAILED)-SUM(ALLOTED))>0  ) EMP_LEAVE on TSPL_EMPLOYEE_MASTER.EMP_CODE=EMP_LEAVE.EMP_CODE) AS EMP_LEAVE " _
            & " WHERE " & strTableName & ".EMP_CODE=EMP_LEAVE.EMP_CODE AND SUB_HEAD_TYPE='OT' and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "'"
            If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                Throw New Exception("Error in Updating OT Hours!")
            End If
        End If

        '' calculate ot 
        '' BHA/06/08/19-000921 by balwinder on 07/08/2019
        strq = " SELECT SALARY_CALCULATION_CODE,EMP_CODE,PAYPERIOD_DAYS,PAY_HEAD_CODE,SUB_HEAD_TYPE,RATE_AMOUNT,(select actual_amount from " & strTableName & "  AS SALARY where sub_head_type='BASIC' AND SALARY.EMP_CODE=" & strTableName & ".EMP_CODE) AS HEAD_VALUE,ACTUAL_AMOUNT,OT_CODE,OT_HOURS," &
               " OT_RATE,HOUR_MULTIPLIER,IS_ASPER_ACTUAL_CALC " + Environment.NewLine +
               " ,(select sum(STD_AMOUNT) from " & strTableName & "  AS SALARY left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=SALARY.PAY_HEAD_CODE where TSPL_PAYHEAD_MASTER.ISEARNING=1 AND SALARY.EMP_CODE=" & strTableName & ".EMP_CODE and TSPL_PAYHEAD_MASTER.Do_Not_Include_In_Gross_Salary_For_Over_Time=0 ) AS GrossSalary" + Environment.NewLine +
               " FROM " & strTableName & " " &
               " left outer join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE=" & strTableName & " .PAY_PERIOD_CODE " + Environment.NewLine +
               " WHERE SUB_HEAD_TYPE='OT' AND OT_CODE IS NOT NULL AND COALESCE(OT_HOURS,0)>0 and " & strTableName & ".PAY_PERIOD_CODE='" & Pay_Period_Code & "' ORDER BY EMP_CODE,LINE_NO "
        Dim dtOT As DataTable
        dtOT = clsDBFuncationality.GetDataTable(strq, trans)
        For Each dr As DataRow In dtOT.Rows
            Dim dclOTAmt As Decimal = CalculateOT(dr.Item("OT_CODE"), dr.Item("OT_HOURS"), clsCommon.myCdbl(dr.Item("HEAD_VALUE")) / (dr.Item("PAYPERIOD_DAYS") * WorkingHours), clsCommon.myCdbl(dr.Item("HEAD_VALUE")), trans, clsCommon.myCdbl(dr.Item("GrossSalary")) / (dr.Item("PAYPERIOD_DAYS") * WorkingHours))
            strq = " update  " & strTableName & " SET ACTUAL_AMOUNT=" & clsCommon.myCstr(dclOTAmt) & ",FORMULA_AMOUNT=" & clsCommon.myCstr(dclOTAmt) & ",std_amount=" & clsCommon.myCstr(dclOTAmt) & " WHERE SALARY_CALCULATION_CODE=" & dr.Item("SALARY_CALCULATION_CODE") & ""
            If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                Throw New Exception("Error in Updating OT Amount !")
            End If
        Next

        updateSalary("0", strTableName, Pay_Period_Code, drPF, Is_Arrear, PP_END_DATE, drESI, dtPT, objPP, EmpList, trans)
        Return True
    End Function

    Shared Sub updateSalary(ByVal isEarning As String, ByVal strTableName As String, ByVal PAY_PERIOD_CODE As String, ByVal drPF As DataRow, ByVal Is_Arrear As Boolean, ByVal PP_END_DATE As Date, ByVal drESI As DataRow, ByVal dtPT As DataTable, ByVal objPP As clsPayPeriodMaster, ByVal EmpList As ArrayList, ByVal trans As SqlTransaction)
        Dim strq As String = "SELECT DISTINCT LINE_NO FROM " & strTableName & " WHERE LINE_NO IS NOT NULL and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' ORDER BY LINE_NO"
        Dim dtSeq As DataTable = clsDBFuncationality.GetDataTable(strq, trans)
        Dim strEmp As String = "(" & clsCommon.GetMulcallString(EmpList) & ")"
        For Each drSeq As DataRow In dtSeq.Rows
            strq = "UPDATE " & strTableName & " " _
                    & " SET PAYHEAD_FORMULA = REPLACE(PAYHEAD_FORMULA,'[' + T5.PAY_HEAD_CODE + ']',COALESCE(T5.FORMULA_AMOUNT, '0')), " _
                    & " FORMULA_AMOUNT = '(' + REPLACE(PAYHEAD_FORMULA,'[' + T5.PAY_HEAD_CODE + ']',COALESCE(T5.FORMULA_AMOUNT, '0')) + ')*(' + CAST(RATE_AMOUNT as VARCHAR(10)) + '/100.00)' + ( " _
                    & " CASE " _
                    & " WHEN HEAD_TYPE = 'ATTN' THEN " _
                    & " '*(' + CAST(COALESCE(PAYABLE_DAYS, 0) / CAST(COALESCE (PAYPERIOD_DAYS, 1) AS FLOAT) AS VARCHAR(200)) + ')' " _
                    & " ELSE " _
                    & " '*1' End), " _
                    & " FORMULA_HEAD = REPLACE ( " _
                    & " FORMULA_HEAD, " _
                    & " '[' + T5.PAY_HEAD_CODE + ']', " _
                    & " CAST(COALESCE (T5.STD_AMOUNT, '0') AS VARCHAR(200))) " _
                    & " FROM " _
                    & " ( " _
                    & " SELECT EMP_CODE,FORMULA_AMOUNT,PAY_HEAD_CODE,LINE_NO,ACTUAL_AMOUNT,STD_AMOUNT " _
                    & " FROM " & strTableName & " " _
                    & " WHERE LINE_NO = " & drSeq.Item("LINE_NO") & " and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' " _
                    & " ) AS T5 " _
                    & " WHERE " & strTableName & ".EMP_CODE = T5.EMP_CODE AND " & strTableName & ".LINE_NO > " & drSeq.Item("LINE_NO") & "" _
                    & " AND " & strTableName & ".HEAD_TYPE NOT IN ('ATTN', 'FIXED','UD') and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
            If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                Throw New Exception("Error in Updating Formula Pay Heads !")
            End If
        Next

        strq = "UPDATE " & strTableName & " SET PAYHEAD_FORMULA = '0' WHERE (LTRIM(PAYHEAD_FORMULA) = '' OR PAYHEAD_FORMULA IS NULL) and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Formula Pay Heads !")
        End If
        strq = "UPDATE " & strTableName & " SET FORMULA_HEAD = '0' WHERE (LTRIM (FORMULA_HEAD) = '' OR FORMULA_HEAD IS NULL) and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Formula Pay Heads !")
        End If


        Dim dtSal As DataTable = clsDBFuncationality.GetDataTable("select * from " & strTableName & " WHERE LINE_NO IS NOT NULL and HEAD_TYPE='F' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "'  and Is_Earning_Payhead=" + isEarning + "  ORDER BY SALARY_CALCULATION_CODE", trans)
        For Each drSal As DataRow In dtSal.Rows
            strq = "UPDATE " & strTableName & " SET FORMULA_VALUE = (select " & drSal.Item("FORMULA_AMOUNT") & ")," _
            & " HEAD_VALUE=round((select " & drSal.Item("PAYHEAD_FORMULA") & "),0) where SALARY_CALCULATION_CODE= " & drSal.Item("SALARY_CALCULATION_CODE") & " and Is_Earning_Payhead=" + isEarning + ""
            If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                Throw New Exception("Error in Updating Formula Pay Heads !")
            End If
        Next

        strq = "UPDATE " & strTableName & " SET FORMULA_AMOUNT=0 where (COALESCE(FORMULA_AMOUNT,'')='' or FORMULA_AMOUNT='') and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Formula Pay Heads !")
        End If

        '' UPDATE PF_MAX_LIM ON THE BASIS OF PF_CALCULATION_TYPE FROM EMPLOYEE STATUS.
        strq = "UPDATE " & strTableName & " SET PF_MAX_LIM=(CASE WHEN PF_Calculation_Type='PR' THEN PF_Rule_Max_Lim WHEN PF_Calculation_Type='FA' THEN HEAD_VALUE WHEN PF_Calculation_Type='C' THEN  Custom_PF_Max_Lim ELSE  PF_Rule_Max_Lim END) where SUB_HEAD_TYPE='EPF' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating PF Max Limit !")
        End If


        'ProgressBar1.Text = "Generating Salary...9. Update calculating pf and eps"
        clsCommon.ProgressBarUpdate("updating PF...")
        strq = "UPDATE " & strTableName & "" _
       & " SET ACTUAL_AMOUNT = ( " _
       & " CASE" _
       & " WHEN SUB_HEAD_TYPE IN ('COPF', 'COEPS') THEN " _
       & " ( " _
       & " ( " _
       & " CASE " _
       & " WHEN IS_PF_APPL = 1 THEN " _
       & " ( " _
       & " CASE " _
       & " WHEN SUB_HEAD_TYPE = 'COPF' THEN " +
       "( " +
"(CASE WHEN PF_MAX_LIM >0 THEN " +
"(CASE WHEN IS_PF_ATTN_ENABLE = 0 THEN (CASE WHEN (case when PAYABLE_DAYS=0 then 0 else   HEAD_VALUE end  ) > PF_MAX_LIM THEN " + Environment.NewLine +
" ((Isnull(HEAD_VALUE,0)+IsNull(ACTUAL_AMOUNT,0))  )" + Environment.NewLine +
" ELSE (case when PAYABLE_DAYS=0 then 0 else HEAD_VALUE end) End )" +
"ELSE " +
"(CASE WHEN HEAD_VALUE > PF_MAX_LIM THEN ((PF_MAX_LIM  )) ELSE ( (HEAD_VALUE))End)End)" +
"ELSE" +
"(CASE WHEN IS_PF_ATTN_ENABLE = 0 THEN FORMULA_VALUE ELSE ((CASE WHEN HEAD_VALUE > PF_MAX_LIM THEN ((HEAD_VALUE))ELSE((HEAD_VALUE))End))End)End)" +
") * RATE_AMOUNT / 100 " _
       & " WHEN SUB_HEAD_TYPE IN ('COPF', 'COEPS') THEN " _
       & " ( " _
       & " CASE " _
       & " WHEN EPS_TO_EPF = 1 THEN " _
       & " 	(  " _
       & " 	CASE " _
       & " 	WHEN SUB_HEAD_TYPE = 'COPF' THEN " _
       & " 	( " _
       & " 	( " _
       & " 		CASE " _
       & " 		WHEN PF_MAX_LIM>0 THEN " _
       & " 		( " _
       & " 		CASE " _
       & " 	    WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
       & "      PF_MAX_LIM " _
       & " 		ELSE " _
       & "      FORMULA_VALUE " _
       & "      End " _
       & " 		) " _
       & " 	ELSE " _
       & " (FORMULA_VALUE) " _
       & "  End " _
       & " 			) " _
       & " 			) * RATE_AMOUNT / 100 " _
       & " 	ELSE " _
       & "      0 " _
       & "      End " _
       & " 							) " _
       & " 		ELSE " _
       & " 		( " _
       & " 	    CASE " _
       & " 		WHEN SUB_HEAD_TYPE = 'COPF' THEN " _
       & " 	    ( " _
       & " 		CASE " _
       & " 	    WHEN ( " _
       & " 		( " _
       & " 		( " _
       & " 		( " _
       & " 		CASE " _
       & " 	    WHEN PF_MAX_LIM>0  THEN " _
       & " 		( " _
       & " 		CASE " _
       & " 		WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
       & "      PF_MAX_LIM " _
       & " 		ELSE " _
       & "      FORMULA_VALUE " _
       & "      End " _
       & " 		) " _
       & " 		ELSE " _
       & " 		(FORMULA_VALUE) " _
       & "      End " _
       & " 		) " _
       & " 		) " _
       & " 		) * TSPL_EMPLOYEE_MASTER.EMPEPF_PER / 100 " _
       & " 		) > (PF_MAX_LIM * TSPL_EMPLOYEE_MASTER.EMPEPF_PER / 100) THEN " _
       & " 	    ((PF_MAX_LIM * TSPL_EMPLOYEE_MASTER.EMPEPF_PER) / 100) " _
       & " 		ELSE " _
       & " 		( " _
       & " 	    ( " _
       & " 	    ( " _
       & " 		( " _
       & " 		CASE " _
       & " 	    WHEN PF_MAX_LIM>0 THEN " _
       & " 		( " _
       & " 		CASE " _
       & " 		WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
       & "      PF_MAX_LIM " _
       & " 		ELSE " _
       & "      FORMULA_VALUE " _
       & "      End " _
       & " 	 	) " _
       & " 		ELSE " _
       & " 	    (FORMULA_VALUE) " _
       & "      End " _
       & " 	    ) " _
       & " 		) " _
       & " 	    ) * TSPL_EMPLOYEE_MASTER.EMPEPF_PER / 100 " _
       & " 	    ) " _
       & "      End " _
       & " 		) " _
       & " 		ELSE " _
       & "      0 " _
       & "      End " _
       & " 		) " _
       & "      End " _
       & " 	    ) " _
       & " 			ELSE " _
       & " 			( " _
       & " 			( " _
       & " 	 		CASE " _
       & " 			WHEN PF_MAX_LIM>0 THEN " _
       & " 			( " _
       & " 			CASE " _
       & " 		    WHEN FORMULA_VALUE > PF_MAX_LIM THEN " _
       & "          PF_MAX_LIM " _
       & " 			ELSE " _
       & "         FORMULA_VALUE " _
       & "         End " _
       & " 		   ) " _
       & " 		  ELSE " _
       & " 		(FORMULA_VALUE) " _
       & "      End " _
       & " 	    ) " _
       & "   	) * RATE_AMOUNT / 100 " _
       & "      End " _
       & " 		) " _
       & " 	   ELSE " _
       & "     0 " _
       & "     End " _
       & " 	   ) " _
       & " 	   ) " _
       & "     ELSE " _
       & " 	   ( " _
       & " 	   COALESCE ( " _
       & " 	   FORMULA_VALUE, " _
       & "     '0' " _
       & " 	   ) " _
       & "     ) " _
       & "     End " _
       & "     ) " _
       & "     FROM TSPL_SALARY_CALCULATION 
             left JOIN TSPL_EMPLOYEE_MASTER  ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_SALARY_CALCULATION.EMP_CODE WHERE " _
       & "     COALESCE (FORMULA_AMOUNT, '') <> '' " _
       & "     AND LINE_NO IS NOT NULL  and HEAD_TYPE='F' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Formula Pay Heads !")
        End If

        clsCommon.ProgressBarUpdate("updating EPS...")
        strq = "UPDATE " & strTableName & " SET" _
  & " ACTUAL_AMOUNT = ROUND(T1.EPS, 2) " _
  & " FROM " _
  & " ( " _
  & " SELECT " _
  & " T1.EMP_CODE, " _
  & " ( " _
  & " COALESCE (T2.EPF, 0) - COALESCE (T3.COPF, 0) " _
  & " ) AS EPS " _
  & " FROM " _
  & " " & strTableName & " T1 " _
  & " LEFT JOIN ( " _
  & " SELECT " _
  & " EMP_CODE, " _
  & " COALESCE (SUM(ACTUAL_AMOUNT), 0) AS EPF " _
  & " FROM " _
  & " " & strTableName & " " _
  & " WHERE " _
  & " SUB_HEAD_TYPE = 'EPF' " _
  & " Group BY " _
  & " EMP_CODE " _
  & " ) AS T2 ON T1.EMP_CODE = T2.EMP_CODE " _
  & " LEFT JOIN ( " _
  & " SELECT " _
  & " EMP_CODE, " _
  & " COALESCE (SUM(ACTUAL_AMOUNT), 0) AS COPF " _
  & " FROM " _
  & " " & strTableName & " " _
  & " WHERE " _
  & " SUB_HEAD_TYPE = 'COPF' " _
  & " Group BY " _
  & " EMP_CODE " _
  & " ) AS T3 ON T1.EMP_CODE = T3.EMP_CODE " _
  & " ) AS T1 " _
  & " WHERE " _
  & " " & strTableName & ".EMP_CODE = T1.EMP_CODE" _
  & " AND " & strTableName & ".SUB_HEAD_TYPE = 'COEPS' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating EPS !")
        End If

        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & strTableName & " SET CoEPS_AMT_AC10=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN ((" & drPF.Item("EMPEPF_MAX") & ")*PAYABLE_DAYS/PAYPERIOD_DAYS) ELSE HEAD_VALUE END) *CoEPS_RATE_AC10/100) " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Employer EPS Account !")
        End If

        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & strTableName & " SET CoEPS_AMT_AC10=0 " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0  and CoEPS_AMT_AC10<0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Employer EPS Account !")
        End If

        '' apply max amount on employer accounts(COEPS)
        strq = "UPDATE " & strTableName & " SET CoEPS_AMT_AC10=(case when " & drPF.Item("EPS_MAX") & ">0 AND CoEPS_AMT_AC10> " & drPF.Item("EPS_MAX") & " THEN " & drPF.Item("EPS_MAX") & " ELSE   CoEPS_AMT_AC10 END) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Applying Maximum of Employer EPS Account !")
        End If

        If Is_Arrear = False Then
            clsCommon.ProgressBarUpdate("updating ESI...")
        Else
            clsCommon.ProgressBarUpdate("updating Arrears(" & PAY_PERIOD_CODE & ")...")
        End If

        '' UPDATE PREV_ESI IN "& strTableName &"
        strq = "UPDATE " & strTableName & " SET PREV_ESI=FINAL.ACTUAL_AMOUNT FROM " &
               " ( " &
               " SELECT TAB.SALARY_GENERATION_CODE,TAB.PAY_PERIOD_CODE, " &
               " TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE,TSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE, " &
               " TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT FROM ( " &
               " SELECT TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY.PAY_PERIOD_CODE FROM TSPL_GENERATE_SALARY INNER JOIN TSPL_PAYPERIOD_MASTER " &
               " ON TSPL_GENERATE_SALARY.PAY_PERIOD_CODE=TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE " &
               " WHERE TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE IN  " &
               " (SELECT PAY_PERIOD_CODE FROM TSPL_PAYPERIOD_MASTER WHERE DATE_FROM='" & clsCommon.GetPrintDate(PP_END_DATE.AddMonths(-1), "dd/MMM/yyyy") & "')) TAB " &
               " LEFT JOIN TSPL_GENERATE_SALARY_PAYHEADS " &
               " ON TAB.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE " &
               " WHERE TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE IN ('EMPESI','COESI')) FINAL " &
               " WHERE " & strTableName & ".EMP_CODE=FINAL.EMP_CODE " &
               " AND " & strTableName & ".PAY_HEAD_CODE=FINAL.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Previous ESI !")
        End If
        Dim Arrear As Integer = 0
        If Is_Arrear Then
            Arrear = 1
        Else
            Arrear = 0
        End If

        Dim ESIFrom As Double = 0
        Dim ESITo As Double = 0
        Dim Qry As String = "Select IsNull(ESI_FROM_MONTH,0)ESI_FROM_MONTH,IsNull(ESI_TO_MONTH,0)ESI_TO_MONTH from TSPL_PAYPERIOD_MASTER Where PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "'"
        Dim dtt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        If dtt IsNot Nothing AndAlso dtt.Rows.Count > 0 Then
            If clsCommon.myCdbl(dtt.Rows(0)("ESI_FROM_MONTH")) > 0 AndAlso clsCommon.myCdbl(dtt.Rows(0)("ESI_TO_MONTH")) > 0 Then
                ESIFrom = clsCommon.myCdbl(dtt.Rows(0)("ESI_FROM_MONTH"))
                ESITo = clsCommon.myCdbl(dtt.Rows(0)("ESI_TO_MONTH")) + 1
            Else
                ESIFrom = 4
                ESITo = 10
            End If
        Else
            ESIFrom = 4
            ESITo = 10
        End If

        strq = "UPDATE " & strTableName & "" _
              & " SET ACTUAL_AMOUNT = ( " _
              & " CASE " _
              & " WHEN IS_ESI_APPL = 1 " _
              & " AND HEAD_VALUE > ESI_MAX_LIM and " & Arrear & "=0 AND " & PP_END_DATE.Month & " IN (" & ESIFrom & "," & ESITo & ") THEN " _
              & " 0 " _
              & " WHEN IS_ESI_APPL = 1 " _
              & " AND HEAD_VALUE > ESI_MAX_LIM AND COALESCE(PREV_ESI,0)<=0 and " & Arrear & "=0 THEN " _
              & " 0 " _
              & " WHEN IS_ESI_APPL = 0 THEN " _
              & " 0 " _
              & " ELSE " _
              & " ACTUAL_AMOUNT " _
              & " End " _
              & " ) " _
              & " , Payable_Amount = ( " _
              & " CASE " _
              & " WHEN IS_ESI_APPL = 1 " _
              & " AND HEAD_VALUE > ESI_MAX_LIM and " & Arrear & "=0 AND " & PP_END_DATE.Month & " IN (" & ESIFrom & "," & ESITo & ") THEN " _
              & " 0 " _
              & " WHEN IS_ESI_APPL = 1 " _
              & " AND HEAD_VALUE > ESI_MAX_LIM AND COALESCE(PREV_ESI,0)<=0 and " & Arrear & "=0 THEN " _
              & " 0 " _
              & " WHEN IS_ESI_APPL = 0 THEN " _
              & " 0 " _
              & " ELSE " _
              & " ACTUAL_AMOUNT " _
              & " End " _
              & " ) " _
              & " WHERE " _
              & " SUB_HEAD_TYPE IN ('COESI', 'EMPESI') and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating EPS !")
        End If

        '' UPDATE EMPLOYER ESI 
        strq = " UPDATE " & strTableName & " SET Co_ESI_AMT=(HEAD_VALUE*Co_ESI_RATE/100) WHERE SUB_HEAD_TYPE ='EMPESI' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating EMPLOYER ESI ACCOUNT !")
        End If

        '' ROUND OFF EMPLOYER ESI 
        strq = "UPDATE " & strTableName & " SET Co_ESI_AMT=(CASE WHEN '" & drESI.Item("COESI_ROUNDOFF_YPE") & "'='R' THEN ROUND(Co_ESI_AMT,2) " _
       & " WHEN '" & drESI.Item("COESI_ROUNDOFF_YPE") & "'='L' THEN  ROUND(Co_ESI_AMT,2) WHEN '" & drESI.Item("COESI_ROUNDOFF_YPE") & "'='U' THEN ROUND(Co_ESI_AMT,2) END) " _
       & " WHERE SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Rounding off Employer ESI account!")
        End If

        clsCommon.ProgressBarUpdate("updating PT...")
        '' PROFF TAX CALCULATION
        If dtPT.Rows.Count > 0 Then ''ERO/30/08/19-001010 by banlwinder on 09/09/2019
            strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT=(case when Is_Professional_Tax_Applicable=1 then (" & GetQueryPT(dtPT) & ") else 0 end) WHERE SUB_HEAD_TYPE='PT' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + " ;"
            If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                Throw New Exception("Error in Updating Proff Tax !")
            End If
        End If

        '' Mediclaim CALCULATION
        clsCommon.ProgressBarUpdate("updating Mediclaim...")
        strq = " UPDATE " & strTableName & " SET ACTUAL_AMOUNT=mediclim.Total_Amount " &
               " from (select EMP_CODE,SUM(Total_Amount) AS Total_Amount from TSPL_MEDICLAIM_HEAD " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' GROUP BY EMP_CODE) as mediclim " &
               " where " & strTableName & ".EMP_CODE=mediclim.EMP_CODE AND " & strTableName & ".SUB_HEAD_TYPE='Mediclaim' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        Dim STRQ1 As String
        STRQ1 = "update TSPL_MEDICLAIM_HEAD set PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' where PAY_PERIOD_CODE is null "
        If Not (clsDBFuncationality.ExecuteNonQuery(strq, trans) And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans)) Then
            Throw New Exception("Error in Updating Mediclaim !")
        End If

        '' Gratuity CALCULATION
        clsCommon.ProgressBarUpdate("updating Gratuity...")
        strq = " UPDATE " & strTableName & " SET ACTUAL_AMOUNT=GRATUITY.Total_Amount " &
               " from (select EMP_CODE,SUM(GRATUITYAMT) AS Total_Amount from TSPL_GRATUITY  " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' GROUP BY EMP_CODE) as GRATUITY " &
               " where " & strTableName & ".EMP_CODE=GRATUITY.EMP_CODE AND " & strTableName & ".SUB_HEAD_TYPE='Gratuity' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        STRQ1 = "update TSPL_GRATUITY set PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' where PAY_PERIOD_CODE is null"
        If Not (clsDBFuncationality.ExecuteNonQuery(strq, trans) And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans)) Then
            Throw New Exception("Error in Updating GRATUITY !")
        End If

        '' LTA CALCULATION
        clsCommon.ProgressBarUpdate("updating LTA...")
        strq = " UPDATE " & strTableName & " SET ACTUAL_AMOUNT=LTA.Total_Amount " &
               " from (select EMP_CODE,SUM(Claim_Amount) AS Total_Amount from TSPL_LTA_Claim_Head   " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' GROUP BY EMP_CODE) as LTA " &
               " where " & strTableName & ".EMP_CODE=LTA.EMP_CODE AND " & strTableName & ".SUB_HEAD_TYPE='LTA' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        STRQ1 = "update TSPL_LTA_Claim_Head set PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' where PAY_PERIOD_CODE is null"
        If Not (clsDBFuncationality.ExecuteNonQuery(strq, trans) And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans)) Then
            Throw New Exception("Error in Updating LTA !")
        End If

        '' Conveyance
        clsCommon.ProgressBarUpdate("updating Conveyance...")
        strq = " UPDATE " & strTableName & " SET ACTUAL_AMOUNT=(CASE WHEN MAX_AMOUNT>0 AND  Conv.Total_Amount>MAX_AMOUNT THEN  MAX_AMOUNT ELSE Conv.Total_Amount END)  " &
               " from (select EMP_CODE,SUM(Claim_Amount) AS Total_Amount from TSPL_CONVEYANCE_CLAIM   " &
               " where PAY_PERIOD_CODE is null or PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' GROUP BY EMP_CODE) as Conv " &
               " where " & strTableName & ".EMP_CODE=Conv.EMP_CODE AND " & strTableName & ".SUB_HEAD_TYPE='Conveyance' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        STRQ1 = "update TSPL_CONVEYANCE_CLAIM set PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' where PAY_PERIOD_CODE is null"
        If Not (clsDBFuncationality.ExecuteNonQuery(strq, trans) And clsDBFuncationality.ExecuteNonQuery(STRQ1, trans)) Then
            Throw New Exception("Error in Updating Conveyance !")
        End If


        ''-------------------Deduction According Leave Master Apply Leave Type Ded Is 1
        Dim check As String = "Select * from TSPL_LEAVE_MASTER where APPLY_LEAVE_TYPE_DED=1"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(check, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            check = Nothing
            For Each row As DataRow In dt.Rows
                If clsCommon.CompairString(row("Leave_Type"), "EL") = CompairStringResult.Equal Then
                    check += " + TSPL_MONTHLY_ATTENDANCE_DETAIL.Earned_Leave "
                End If
                If clsCommon.CompairString(row("Leave_Type"), "CL") = CompairStringResult.Equal Then
                    check += " + TSPL_MONTHLY_ATTENDANCE_DETAIL.Casual_Leave "
                End If
                If clsCommon.CompairString(row("Leave_Type"), "MED") = CompairStringResult.Equal Then
                    check += " + TSPL_MONTHLY_ATTENDANCE_DETAIL.Medical_Leave "
                End If
            Next
            strq = " UPDATE " & strTableName & " Set ACTUAL_AMOUNT=(RATE_AMOUNT/30)*(30-(" & strTableName & ".ABSENT_DAYS " & check & ")) from
                TSPL_MONTHLY_ATTENDANCE_DETAIL Inner Join TSPL_MONTHLY_ATTENDANCE On TSPL_MONTHLY_ATTENDANCE.MTA_CODE=TSPL_MONTHLY_ATTENDANCE_DETAIL.MTA_CODE
                where TSPL_MONTHLY_ATTENDANCE_DETAIL.EMP_CODE=TSPL_SALARY_CALCULATION.EMP_CODE and
                TSPL_MONTHLY_ATTENDANCE.PAY_PERIOD_CODE ='" & PAY_PERIOD_CODE & "'  AND  " & strTableName & ".HEAD_TYPE='FIXED' And
                " & strTableName & ".SUB_HEAD_TYPE In (Select SUB_HEAD_TYPE from TSPL_PAYHEAD_MASTER where HEAD_TYPE='FIXED' And CALC_BASIS='FIXED_30_DAYS') 
                And TSPL_SALARY_CALCULATION.PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=1 and CALC_BASIS='FIXED_30_DAYS'"
            If Not (clsDBFuncationality.ExecuteNonQuery(strq, trans)) Then
                Throw New Exception("Error in Updating Apply Leave Type Deduction !")
            End If
        End If


        ''update pension of employees having age equal to or greater than 58 years
        clsCommon.ProgressBarUpdate("Applying 58 years condition in Pension...")
        strq = " UPDATE  " & strTableName & " SET CoEPS_AMT_AC10=0  where SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "'" &
                " AND EMP_CODE IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_MASTER where DATEADD(year,Age_For_Pension,CONVERT(DATE,Birth_date,103))<='" & clsCommon.GetPrintDate(objPP.DATE_TO, "dd/MMM/yyyy") & "' and Age_For_Pension>0 ) and Is_Earning_Payhead=" + isEarning + ";"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating pension of employees having age equal to or graeter than 58 years !")
        End If

        'STRQ1 = "update " & strTableName & " set STD_AMOUNT=case when isnull(PAYABLE_DAYS,0)=0 then 0 else (CAST( FORMULA_VALUE as decimal(18,2)) * PAYPERIOD_DAYS/PAYABLE_DAYS) end where HEAD_TYPE='F' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        STRQ1 = "update " & strTableName & " set STD_AMOUNT=case when isnull(PAYABLE_DAYS,0)=0 then 0 else round((CAST( FORMULA_VALUE as decimal(18,2)) * PAYPERIOD_DAYS/PAYABLE_DAYS),0) end where HEAD_TYPE='F' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) Then
            Throw New Exception("Error in Updating Standard Amount !")
        End If

        ' ''Calcualte Gross Salary
        dtSal = clsDBFuncationality.GetDataTable("select REPLACE(FORMULA_AMOUNT,Pay_Days_Ratio,'1' ) as ToBeCalcualte,* from " & strTableName & " WHERE HEAD_TYPE='F' and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "'  and Is_Earning_Payhead=" + isEarning + "  ORDER BY SALARY_CALCULATION_CODE", trans)
        For Each drSal As DataRow In dtSal.Rows
            If clsCommon.myCdbl(drSal.Item("PAYABLE_DAYS")) > 0 Then
                strq = "UPDATE " & strTableName & " SET STD_AMOUNT =  round((select " & drSal.Item("ToBeCalcualte") & "),0)" _
                & "  where SALARY_CALCULATION_CODE= " & drSal.Item("SALARY_CALCULATION_CODE") & "  and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and " & strTableName & ".EMP_CODE='" & clsCommon.myCstr(drSal.Item("EMP_CODE")) & "' and Is_Earning_Payhead=" + isEarning + ""
                If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                    Throw New Exception("Error in Updating Formula Pay Heads !")
                End If
            End If
        Next



        If Is_Arrear = False Then
            CalculateArrear(PAY_PERIOD_CODE, trans)
        End If

        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & strTableName & " SET CoEPS_AMT_AC10=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN ((" & drPF.Item("EMPEPF_MAX") & ")*PAYABLE_DAYS/PAYPERIOD_DAYS) ELSE HEAD_VALUE END) *CoEPS_RATE_AC10/100) " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Employer EPS Account !")
        End If


        '' apply max amount on employer accounts(COEPS)
        strq = "UPDATE " & strTableName & " SET CoEPS_AMT_AC10=(case when " & drPF.Item("EPS_MAX") & ">0 AND CoEPS_AMT_AC10> " & drPF.Item("EPS_MAX") & " THEN " & drPF.Item("EPS_MAX") & " ELSE   CoEPS_AMT_AC10 END) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) = True Then
            Throw New Exception("Error in Applying Maximum of Employer EPS Account !")
        End If

        '' ROUND OFF COEPS
        If Is_Arrear = False Then
            clsCommon.ProgressBarUpdate("Rounding off Employer Accounts...")
            strq = "UPDATE " & strTableName & " SET CoEPS_AMT_AC10=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(CoEPS_AMT_AC10,0) " _
           & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(CoEPS_AMT_AC10) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(CoEPS_AMT_AC10) END), " _
           & " CoEPS_AMT_AC10_ROUND_OFF=(CoEPS_AMT_AC10-(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(CoEPS_AMT_AC10,0) " _
           & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(CoEPS_AMT_AC10) WHEN ROUND_OFF_TYPE='U' THEN CEILING(CoEPS_AMT_AC10) END))" _
           & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
            If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                Throw New Exception("Error in Updating Rounding off Employer EPS account!")
            End If
        End If

        '' UPDATE EPS account
        clsCommon.ProgressBarUpdate("updating Employer Accounts...")
        strq = "UPDATE " & strTableName & " SET CoEPS_AMT_AC10=0 " _
               & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0  and CoEPS_AMT_AC10<0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Employer EPS Account !")
        End If

        ''update pension of employees having age equal to or greater than 58 years after arrear calculation
        clsCommon.ProgressBarUpdate("Applying 58 years condition in Pension...")
        strq = " UPDATE  " & strTableName & " SET CoEPS_AMT_AC10=0  where SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "'" &
                " AND EMP_CODE IN (SELECT EMP_CODE FROM TSPL_EMPLOYEE_MASTER where DATEADD(year,Age_For_Pension,CONVERT(DATE,Birth_date,103))<='" & clsCommon.GetPrintDate(objPP.DATE_TO, "dd/MMM/yyyy") & "' and Age_For_Pension>0 ) and Is_Earning_Payhead=" + isEarning + ";"
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating pension of employees having age equal to or graeter than 58 years !")
        End If


        '' UPDATE EMPLOYER SHARE ACCOUNTS -COEPF
        Dim EPF As String = "((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN ((" & drPF.Item("EMPEPF_MAX") & ")*PAYABLE_DAYS/PAYPERIOD_DAYS) ELSE HEAD_VALUE END)*(CoEPF_RATE_AC01+CoEPS_RATE_AC10)/100)"
        strq = "UPDATE " & strTableName & " SET CoEPF_AMT_AC01=((CASE WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(" & EPF & ",0) " _
              & " WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(" & EPF & ") WHEN '" & drPF.Item("EMPEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(" & EPF & ") END)-CoEPS_AMT_AC10), " _
              & " CoEPF_AMT_AC01_ROUND_OFF=-CoEPS_AMT_AC10_ROUND_OFF" _
              & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Employer PF Accounts !")
        End If

        '' update other admin accounts
        strq = "UPDATE " & strTableName & " SET " _
          & " ADMIN_AMT_AC02=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END) * ADMIN_RATE_AC02/100),EDLI_AMT_AC21=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END)*EDLI_RATE_AC21/100), " _
          & " ADMIN_EDLI_AMT_AC22=((CASE WHEN HEAD_VALUE>" & drPF.Item("EMPEPF_MAX") & " THEN " & drPF.Item("EMPEPF_MAX") & " ELSE HEAD_VALUE END) * ADMIN_EDLI_RATE_AC22/100) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "'  and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Employer admin Accounts !")
        End If


        '' apply max amount on other employer accounts
        strq = "UPDATE " & strTableName & " SET " _
            & " ADMIN_AMT_AC02=(case when " & drPF.Item("ACCOEPF_MAX") & ">0 AND ADMIN_AMT_AC02> " & drPF.Item("ACCOEPF_MAX") & " THEN " & drPF.Item("ACCOEPF_MAX") & " ELSE   ADMIN_AMT_AC02 END),EDLI_AMT_AC21=(case when " & drPF.Item("COEDLI_MAX") & ">0 AND EDLI_AMT_AC21> " & drPF.Item("COEDLI_MAX") & " THEN " & drPF.Item("COEDLI_MAX") & " ELSE   EDLI_AMT_AC21 END), " _
            & " ADMIN_EDLI_AMT_AC22=(case when " & drPF.Item("ACCOEDLI_MAX") & ">0 AND ADMIN_EDLI_AMT_AC22> " & drPF.Item("ACCOEDLI_MAX") & " THEN " & drPF.Item("ACCOEDLI_MAX") & " ELSE   ADMIN_EDLI_AMT_AC22 END) WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Applying Maximum of Employer PF Accounts !")
        End If

        strq = "UPDATE " & strTableName & " SET " _
       & " ADMIN_AMT_AC02=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(ADMIN_AMT_AC02,0) " _
       & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(ADMIN_AMT_AC02) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(ADMIN_AMT_AC02) END)," _
       & " EDLI_AMT_AC21=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(EDLI_AMT_AC21,0) " _
       & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(EDLI_AMT_AC21) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(EDLI_AMT_AC21) END)," _
       & " ADMIN_EDLI_AMT_AC22=(CASE WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='R' THEN ROUND(ADMIN_EDLI_AMT_AC22,0) " _
       & " WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='L' THEN  FLOOR(ADMIN_EDLI_AMT_AC22) WHEN '" & drPF.Item("COEPF_ROUNDOFF_YPE") & "'='U' THEN CEILING(ADMIN_EDLI_AMT_AC22) END) " _
       & " WHERE SUB_HEAD_TYPE='EPF' AND ACTUAL_AMOUNT>0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Rounding off Employer PF accounts!")
        End If


        ''adjustment IN PRINCIPAL AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in principal amount of pay heads...")
        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT=ACTUAL_AMOUNT+T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS, " _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and ADJUSTMENT_TYPE='PA' and TPH.HEAD_TYPE IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & strTableName & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & strTableName & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Adjustment !")
        End If

        ''adjustment IN PRINCIPAL AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in principal amount of pay heads...")
        strq = "UPDATE " & strTableName & " SET RATE_AMOUNT=RATE_AMOUNT-(T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS) FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and TPH.HEAD_TYPE NOT IN ('F') GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & strTableName & ".EMP_CODE = T1.EMP_CODE)" _
             & " AND " & strTableName & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Adjustment !")
        End If

        ''adjustment IN ARREAR AMOUNT details
        clsCommon.ProgressBarUpdate("updating adjustment in ADJSTMENT IN ARREAR pay heads...")
        strq = "UPDATE " & strTableName & " SET " _
             & " ADJUSTMENT_PLUS=T1.ADJUSTMENT_PLUS,ADJUSTMENT_MINUS=T1.ADJUSTMENT_MINUS,ARREAR_AMT=T1.ADJUSTMENT_PLUS-T1.ADJUSTMENT_MINUS FROM ( " _
             & " SELECT T2.EMP_CODE,T2.PAY_HEAD_CODE,SUM(T2.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS,SUM(T2.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS " _
             & " FROM TSPL_ADJUSTMENT_VOUCHER T1  INNER JOIN TSPL_EMPADJUSTMENT_DETAIL T2 ON T1.ADJUSTMENT_CODE=T2.ADJUSTMENT_CODE inner join TSPL_PAYHEAD_MASTER TPH ON TPH.PAY_HEAD_CODE=T2.PAY_HEAD_CODE " _
             & " WHERE T1.PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and ADJUSTMENT_TYPE='AR'  GROUP BY T2.EMP_CODE,T2.PAY_HEAD_CODE) AS T1  " _
             & " WHERE(" & strTableName & ".EMP_CODE = T1.EMP_CODE)" _
            & " AND " & strTableName & ".PAY_HEAD_CODE=T1.PAY_HEAD_CODE and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""

        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Adjustment !")
        End If

        clsCommon.ProgressBarUpdate("update actual amount in two decimals ...")
        strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT=round(ACTUAL_AMOUNT,2,2) "
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating actual amount in two decimals !")
        End If

        clsCommon.ProgressBarUpdate("Rounding Off all pay heads...")
        strq = "UPDATE " & strTableName & " SET ARREAR_AMT=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ARREAR_AMT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ARREAR_AMT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ARREAR_AMT) END),ARREAR_ROUND_OFF=((CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ARREAR_AMT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ARREAR_AMT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ARREAR_AMT) END)-ARREAR_AMT),ACTUAL_AMOUNT=(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END)," _
        & " PRINCIPAL_ROUND_OFF=(COALESCE(PRINCIPAL_ROUND_OFF,0)+(CASE WHEN ROUND_OFF_TYPE='R' THEN ROUND(ACTUAL_AMOUNT,0) " _
        & " WHEN ROUND_OFF_TYPE='L' THEN  FLOOR(ACTUAL_AMOUNT) WHEN ROUND_OFF_TYPE='U' THEN CEILING(ACTUAL_AMOUNT) END)-ACTUAL_AMOUNT) where  " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
            Throw New Exception("Error in Updating Rounding off !")
        End If

        ''TDS Amount From Income tax Slab.
        clsCommon.ProgressBarUpdate("updating TDS in principal amount of pay heads...")
        strq = "select PAY_HEAD_CODE from TSPL_PAYHEAD_MASTER where SUB_HEAD_TYPE='TDS'"
        Dim strTDS As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strq, trans))
        If clsCommon.myLen(strTDS) > 0 Then
            strq = "select Fiscal_Code from TSPL_Fiscal_Year_Master where exists(select 1 from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "'" + Environment.NewLine +
            "and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)>= convert(date, TSPL_Fiscal_Year_Master.Start_Date,103)  " + Environment.NewLine +
            "and convert(date,TSPL_PAYPERIOD_MASTER.DATE_FROM,103)<= convert(date, TSPL_Fiscal_Year_Master.End_Date,103))"
            Dim strFiscalCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strq, trans))
            If clsCommon.myLen(strFiscalCode) > 0 Then
                strq = "UPDATE " & strTableName & " SET ACTUAL_AMOUNT=xx.Total_TDS_Amt from (" + Environment.NewLine +
                "select TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Emp_Code,cast( TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Total_TDS_Amt/12 as decimal(18,2)) as Total_TDS_Amt from TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP" + Environment.NewLine +
                "left outer join TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD on TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Code=TSPL_HR_TDS_INCOME_TAX_CALCULATION_EMP.Code" + Environment.NewLine +
                "where TSPL_HR_TDS_INCOME_TAX_CALCULATION_HEAD.Status=1" + Environment.NewLine +
                "and Fiscal_Code ='" + strFiscalCode + "'" + Environment.NewLine +
                ")xx inner join " & strTableName & " on " & strTableName & ".EMP_CODE=xx.Emp_Code" + Environment.NewLine +
                "inner join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE=" & strTableName & ".PAY_HEAD_CODE" + Environment.NewLine +
                "where " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE='" + strTDS + "' and Is_Earning_Payhead=" + isEarning + ""
                If Not clsDBFuncationality.ExecuteNonQuery(strq, trans) Then
                    Throw New Exception("Error in Updating TDS Amount !")
                End If
            End If
        End If
        ''End of TDS Amount From Income tax Slab.

        STRQ1 = "update " & strTableName & " set ACTUAL_AMOUNT=0 where Payable_Days=0 and " & strTableName & ".PAY_PERIOD_CODE='" & PAY_PERIOD_CODE & "' and Is_Earning_Payhead=" + isEarning + ""
        If Not clsDBFuncationality.ExecuteNonQuery(STRQ1, trans) Then
            Throw New Exception("Error in update payble days !")
        End If
    End Sub

End Class



Public Class PayPeriod_Employee
    Public Pay_Period_Code As String = ""
    Public objEMPList As New ArrayList
End Class
Public Class clsSalaryFEAccounts
    Public SALARY_GENERATION_CODE As String = ""
    Public Description As String
    Public AccountCode As String
    Public DEBIT As Decimal = 0
    Public CREDIT As Decimal = 0
    Public IS_EMPLOYER As Integer
    Public objList As List(Of clsSalaryFEAccounts)
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsSalaryFEAccounts)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin

        Try
            Dim qry As String = "delete from TSPL_GENERATE_SALARY_FE_ACCOUNTS where SALARY_GENERATION_CODE='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsSalaryFEAccounts In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SALARY_GENERATION_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                    clsCommon.AddColumnsForChange(coll, "AccountCode", obj.AccountCode)

                    clsCommon.AddColumnsForChange(coll, "DEBIT ", obj.DEBIT)
                    clsCommon.AddColumnsForChange(coll, "CREDIT", obj.CREDIT)
                    clsCommon.AddColumnsForChange(coll, "IS_EMPLOYER ", obj.IS_EMPLOYER)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERATE_SALARY_FE_ACCOUNTS", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal Arr As List(Of clsSalaryFEAccounts), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsSalaryFEAccounts In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SALARY_GENERATION_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
                clsCommon.AddColumnsForChange(coll, "AccountCode", obj.AccountCode)

                clsCommon.AddColumnsForChange(coll, "DEBIT ", obj.DEBIT)
                clsCommon.AddColumnsForChange(coll, "CREDIT", obj.CREDIT)
                clsCommon.AddColumnsForChange(coll, "IS_EMPLOYER ", obj.IS_EMPLOYER)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GENERATE_SALARY_FE_ACCOUNTS", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetDFEACList(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsSalaryFEAccounts)
        Dim objList As New List(Of clsSalaryFEAccounts)
        Dim obj As New clsSalaryFEAccounts
        Dim qry As String = "select * from TSPL_GENERATE_SALARY_FE_ACCOUNTS where SALARY_GENERATION_CODE='" & strCode & "' "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsSalaryFEAccounts()
                obj.SALARY_GENERATION_CODE = clsCommon.myCstr(dr("SALARY_GENERATION_CODE"))
                obj.Description = clsCommon.myCstr(dr("Description"))
                obj.AccountCode = clsCommon.myCstr(dr("AccountCode"))
                obj.DEBIT = clsCommon.myCdbl(dr("DEBIT"))
                obj.CREDIT = clsCommon.myCdbl(dr("CREDIT"))
                obj.IS_EMPLOYER = clsCommon.myCdbl(dr("IS_EMPLOYER"))
                objList.Add(obj)
            Next

        End If
        Return objList
    End Function
End Class