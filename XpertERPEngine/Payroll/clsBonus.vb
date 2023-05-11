Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsBonus


#Region "Variables"
    Public EMP_BONUS_CODE As String
    Public Location_Code As String
    Public Division_Code As String
    Public FROM_PAY_PERIOD_CODE As String
    Public TO_PAY_PERIOD_CODE As String
    Public PAYABLE_PAY_PERIOD_CODE As String
    Public DESCRIPTION As String
    Public POSTED As Boolean
    Public Posting_Date As DateTime
    Public ObjList As List(Of clsBonusDetails) = Nothing
    Dim objBonusDetails As New clsBonusDetails()

#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBonus
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            clsBonus.DeleteData(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = True

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String = ""
            qry = " delete from TSPL_EMPBONUS_DETAIL where EMP_BONUS_CODE ='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BONUS_GENERATION_DETAIL where EMP_BONUS_CODE ='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EMPLOYEE_BONUS where EMP_BONUS_CODE ='" & strCode & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBonus
        Dim obj As clsBonus = Nothing
        Dim qry As String = " select * from TSPL_EMPLOYEE_BONUS  where 2=2"
        Select Case NavType
            Case NavigatorType.First
                qry += " and EMP_BONUS_CODE = (select MIN(EMP_BONUS_CODE) from TSPL_EMPLOYEE_BONUS)"
            Case NavigatorType.Last
                qry += " and EMP_BONUS_CODE = (select Max(EMP_BONUS_CODE) from TSPL_EMPLOYEE_BONUS)"
            Case NavigatorType.Next
                qry += " and EMP_BONUS_CODE = (select Min(EMP_BONUS_CODE) from TSPL_EMPLOYEE_BONUS where  EMP_BONUS_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and EMP_BONUS_CODE = (select Max(EMP_BONUS_CODE) from TSPL_EMPLOYEE_BONUS where EMP_BONUS_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and EMP_BONUS_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsBonus()
            obj.EMP_BONUS_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_BONUS_CODE"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Division_Code = clsCommon.myCstr(dt.Rows(0)("Division_Code"))

            obj.FROM_PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("FROM_PAY_PERIOD_CODE"))
            obj.TO_PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("TO_PAY_PERIOD_CODE"))
            obj.PAYABLE_PAY_PERIOD_CODE = clsCommon.myCstr(dt.Rows(0)("PAYABLE_PAY_PERIOD_CODE"))
            obj.DESCRIPTION = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            obj.ObjList = clsBonusDetails.GetData(obj.EMP_BONUS_CODE, trans)
        End If
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsBonus, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsBonus, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = ""
            qry = "delete from TSPL_EMPBONUS_DETAIL where EMP_BONUS_CODE='" & EMP_BONUS_CODE & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_BONUS_GENERATION_DETAIL where EMP_BONUS_CODE='" & EMP_BONUS_CODE & "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Division_Code", obj.Division_Code, True)

            clsCommon.AddColumnsForChange(coll, "FROM_PAY_PERIOD_CODE", obj.FROM_PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "TO_PAY_PERIOD_CODE", obj.TO_PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "PAYABLE_PAY_PERIOD_CODE", obj.PAYABLE_PAY_PERIOD_CODE)
            clsCommon.AddColumnsForChange(coll, "DESCRIPTION", obj.DESCRIPTION)
            clsCommon.AddColumnsForChange(coll, "POSTED", False)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

            If isNewEntry Then
                If clsCommon.myLen(obj.EMP_BONUS_CODE) <= 0 Then
                    obj.EMP_BONUS_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.GenerateBonus, "", "")
                    If clsCommon.myLen(obj.EMP_BONUS_CODE) <= 0 Then
                        Throw New Exception("Error in Code Genration")
                    End If
                End If
                clsCommon.AddColumnsForChange(coll, "EMP_BONUS_CODE", obj.EMP_BONUS_CODE)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                qry = "SELECT Count(*) FROM TSPL_EMPLOYEE_BONUS where EMP_BONUS_CODE= '" & obj.EMP_BONUS_CODE & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                If check = 0 Then
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_BONUS", OMInsertOrUpdate.Insert, "", trans)
                Else
                    Throw New Exception("This Code Is Already Exist")
                End If
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_BONUS", OMInsertOrUpdate.Update, "EMP_BONUS_CODE='" & obj.EMP_BONUS_CODE & "'", trans)
            End If
            isSaved = isSaved AndAlso objBonusDetails.SaveData(obj, trans)
            '' SAVE BONUS GENERATION DETAIL
            qry = clsBonus.GetGenerateBonusDetailBaseQuery(obj.Location_Code, obj.Division_Code, obj.FROM_PAY_PERIOD_CODE, obj.TO_PAY_PERIOD_CODE, obj.PAYABLE_PAY_PERIOD_CODE)
            Dim qryInsert As String = "INSERT INTO TSPL_BONUS_GENERATION_DETAIL (EMP_BONUS_CODE,EMP_CODE,DEPARTMENT_CODE,Designation_Code,Location_Code,Division_Code, " & _
                " PAY_PERIOD_CODE,STD_AMOUNT,ACTUAL_AMOUNT,PAYPERIOD_DAYS,PAYABLE_DAYS,BONUS_ON,BONUS_CODE,BONUS_RATE,BONUS_AMOUNT) " & _
                " SELECT '" & obj.EMP_BONUS_CODE & "',EMP_CODE,DEPARTMENT_CODE,Designation,Location_Code,'" & obj.Division_Code & "',PAY_PERIOD_CODE_MAIN,Std_Basic,Actual_Basic,PAYPERIOD_DAYS,PAYABLE_DAYS,BONUS_ON,BONUS_CODE,BONUS_RATE,BONUS_AMOUNT FROM (" & qry & ") AS BONUS"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qryInsert, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsBonus = clsBonus.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.EMP_BONUS_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_EMPLOYEE_BONUS set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where EMP_BONUS_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetGenerateBonusDataTable(ByVal strFromPayPeriod As String, ByVal strToPayPeriod As String, ByVal strPayablePayPeriod As String) As DataTable
        Dim strQry As String = ""
        strQry += " DECLARE @FROM_PERIOD VARCHAR(30); "
        strQry += " DECLARE @TO_PERIOD VARCHAR(30); "
        strQry += " DECLARE @CALC_PERIOD VARCHAR(30); "
        strQry += " DECLARE @FROM_DATE DATE; "
        strQry += " DECLARE @TO_DATE DATE; "
        strQry += " DECLARE @CALC_DATE DATE; "

        strQry += " SET @FROM_PERIOD='" + strFromPayPeriod + "'; "
        strQry += " SET @TO_PERIOD='" + strToPayPeriod + "'; "
        strQry += " SET @CALC_PERIOD='" + strPayablePayPeriod + "'; "

        strQry += " SELECT @FROM_DATE=DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE=@FROM_PERIOD; "
        strQry += " SELECT @TO_DATE=DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE=@TO_PERIOD; "
        strQry += " SELECT @CALC_DATE=DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE=@CALC_PERIOD; "
        strQry += " SELECT TF.EMP_CODE,T2.EMP_NAME,TF.BONUS_CODE,T3.BONUS_NAME,TF.BONUS_AMOUNT FROM ( "
        strQry += "        SELECT T1.*,"
        strQry += "      (CASE WHEN T1.BASIC_PER_MONTH>T1.CLAUSE_1 THEN (CASE WHEN (T1.CLAUSE_1*12*T1.BONUS_RATE/100)>T1.CLAUSE_3 THEN (T1.CLAUSE_3/12) * T1.TOTAL_PERIOD  ELSE  (T1.CLAUSE_1 * T1.TOTAL_PERIOD * T1.BONUS_RATE/100)  END )  ELSE "
        strQry += "         (CASE WHEN T1.TOTAL_EARNING_PER_MONTH>T1.CLAUSE_2 THEN 0 "
        strQry += "          ELSE"
        strQry += "              (CASE WHEN (T1.TOTAL_EARNING_PER_MONTH*12*T1.BONUS_RATE/100)>T1.CLAUSE_3 "
        strQry += "               THEN (T1.CLAUSE_3/12) * T1.TOTAL_PERIOD "
        strQry += "               ELSE  (T1.TOTAL_EARNING_PER_MONTH * T1.TOTAL_PERIOD * T1.BONUS_RATE/100) "
        strQry += "               END ) "
        strQry += "          END) "
        strQry += "                   End"
        strQry += "      ) AS BONUS_AMOUNT FROM ( "
        strQry += "	SELECT "
        strQry += "		T1.EMP_CODE,T2.BONUS_CODE,COALESCE(T3.COND_BASIC_PER_MONTH,0) AS CLAUSE_1,COALESCE(T3.COND_MAX_EARNING_PER_MONTH,0) AS CLAUSE_2, "
        strQry += " COALESCE(T3.COND_MAX_BONUS_PER_YEAR,0) AS CLAUSE_3,COALESCE(T3.BONUS_RATE,0) AS BONUS_RATE, "
        strQry += " 0 AS IS_LIMIT,T4.BASIC_PER_MONTH, "
        strQry += " T4.TOTAL_EARNING_PER_MONTH, T4.TOTAL_PERIOD "
        strQry += " FROM "
        strQry += "		( "
        strQry += "	SELECT "
        strQry += "				MAX (EMP_STATUS_CODE) AS EMP_STATUS_CODE, "
        strQry += " EMP_CODE "
        strQry += " FROM "
        strQry += " TSPL_EMPLOYEE_STATUS "
        strQry += " Group BY "
        strQry += " EMP_CODE HAVING "
        strQry += " MAX (APPLICABLE_FROM) <= @CALC_DATE "
        strQry += " ) AS T1 "
        strQry += " JOIN TSPL_EMPLOYEE_STATUS T2  ON T1.EMP_STATUS_CODE = T2.EMP_STATUS_CODE JOIN TSPL_EMPLOYEE_MASTER T_2 "
        strQry += " ON T2.EMP_CODE=T_2.EMP_CODE "
        strQry += " LEFT JOIN TSPL_BONUS_MASTER T3 ON T2.BONUS_CODE=T3.BONUS_CODE "
        strQry += " JOIN ( "
        strQry += " SELECT T12.EMP_CODE,COUNT(T11.PAY_PERIOD_CODE) AS TOTAL_PERIOD,(SUM(T12.ACTUAL_BASIC)/COUNT(T11.PAY_PERIOD_CODE)) AS BASIC_PER_MONTH, "
        strQry += " (SUM(T12.TOTAL_ALLOWANCE)/COUNT(T11.PAY_PERIOD_CODE)) AS TOTAL_EARNING_PER_MONTH FROM  TSPL_GENERATE_SALARY T11 "
        ''strQry += "   ON T11.SALARY_GENERATION_CODE=T12.SALARY_GENERATION_CODE "
        strQry += " JOIN TSPL_GENERATE_SALARY_ATTENDANCE T12 ON T11.SALARY_GENERATION_CODE=T12.SALARY_GENERATION_CODE "
        strQry += " JOIN TSPL_PAYPERIOD_MASTER T13 ON T11.PAY_PERIOD_CODE=T13.PAY_PERIOD_CODE WHERE "
        strQry += " T13.DATE_FROM BETWEEN @FROM_DATE AND @TO_DATE GROUP BY T12.EMP_CODE) AS T4 ON T1.EMP_CODE=T4.EMP_CODE "
        strQry += " WHERE T2.WORKING_STATUS='Working'"
        strQry += " ) AS T1 ) AS TF "
        strQry += " INNER JOIN TSPL_EMPLOYEE_MASTER T2 ON TF.EMP_CODE=T2.EMP_CODE "
        strQry += " INNER JOIN TSPL_BONUS_MASTER T3 ON TF.BONUS_CODE=T3.BONUS_CODE "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)

        Return dt
    End Function
    Public Shared Function GetGenerateBonusDataTable(ByVal Location As String, ByVal Division As String, ByVal strFromPayPeriod As String, ByVal strToPayPeriod As String, ByVal strPayablePayPeriod As String, ByVal isSummary As Boolean, ByVal DOC_CODE As String) As DataTable
        Dim qry As String = GetGenerateBonusQuery(Location, Division, strFromPayPeriod, strToPayPeriod, strPayablePayPeriod, isSummary, DOC_CODE)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Public Shared Function GetGenerateBonusQuery(ByVal Location As String, ByVal Division As String, ByVal strFromPayPeriod As String, ByVal strToPayPeriod As String, ByVal strPayablePayPeriod As String, ByVal isSummary As Boolean, ByVal DOC_CODE As String) As String
        Dim qryBase As String = ""
        If clsCommon.myLen(DOC_CODE) <= 0 Then
            qryBase = GetGenerateBonusDetailBaseQuery(Location, Division, strFromPayPeriod, strToPayPeriod, strPayablePayPeriod)
        Else
            qryBase = GetGenerateBonusDetailBaseQuery(DOC_CODE)
        End If

        Dim Qry As String = ""
        Dim colPIV As String = ""

        Dim PivBasic As String = ""
        Dim PivPD As String = ""
        Dim PivBO As String = ""

        Dim colBasic As String = ""
        Dim colPD As String = ""
        Dim colBO As String = ""

        Dim colBasicTotal As String = ""
        Dim colPDTotal As String = ""
        Dim colBOTotal As String = ""
        Dim dynamicCol As String = ""

        '' pivot 
        Qry = " SELECT ('[Amount_' + PAY_PERIOD_CODE + '],')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colPIV = clsDBFuncationality.getSingleValue(Qry)
        PivBasic = colPIV.Substring(0, colPIV.Length - 1)

        Qry = " SELECT ('[PD_' + PAY_PERIOD_CODE + '],')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colPIV = clsDBFuncationality.getSingleValue(Qry)
        PivPD = colPIV.Substring(0, colPIV.Length - 1)

        Qry = " SELECT ('[BonusWages_' + PAY_PERIOD_CODE + '],')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colPIV = clsDBFuncationality.getSingleValue(Qry)
        PivBO = colPIV.Substring(0, colPIV.Length - 1)

        '' pivot cols
        Qry = " SELECT ('coalesce(MAX([Amount_' + PAY_PERIOD_CODE + ']),0) AS [Amount_' + PAY_PERIOD_CODE + '],')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colBasic = clsDBFuncationality.getSingleValue(Qry)
        'colBasic = colPIV.Substring(0, colPIV.Length - 2)

        Qry = " SELECT ('coalesce(MAX([PD_' + PAY_PERIOD_CODE + ']),0) AS [PD_' + PAY_PERIOD_CODE + '],')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colPD = clsDBFuncationality.getSingleValue(Qry)
        'colPD = colPIV.Substring(0, colPIV.Length - 2)

        Qry = " SELECT ('coalesce(MAX([BonusWages_' + PAY_PERIOD_CODE + ']),0) AS [BonusWages_' + PAY_PERIOD_CODE + '],')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colBO = clsDBFuncationality.getSingleValue(Qry)
        'colBO = colPIV.Substring(0, colPIV.Length - 2)

        '' pivot col total        
        Qry = " SELECT ('coalesce(MAX([Amount_' + PAY_PERIOD_CODE + ']),0) +')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colBasicTotal = clsDBFuncationality.getSingleValue(Qry)
        colBasicTotal = colBasicTotal.Substring(0, colBasicTotal.Length - 1)

        Qry = " SELECT ('coalesce(MAX([PD_' + PAY_PERIOD_CODE + ']),0) +')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colPDTotal = clsDBFuncationality.getSingleValue(Qry)
        colPDTotal = colPDTotal.Substring(0, colPDTotal.Length - 1)

        Qry = " SELECT ('coalesce(MAX([BonusWages_' + PAY_PERIOD_CODE + ']),0) +')  FROM TSPL_PAYPERIOD_MASTER " & _
              " WHERE DATE_FROM BETWEEN (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') order by DATE_FROM for xml path('')"
        colBOTotal = clsDBFuncationality.getSingleValue(Qry)
        colBOTotal = colBOTotal.Substring(0, colBOTotal.Length - 1)


        Qry = Nothing
        If isSummary Then
            dynamicCol = "" & colBO & " (" & colBOTotal & ") as [Total Bonus Wages], round((" & colBOTotal & ")*BONUS_RATE/100,0) as [Total Bonus]"
        Else
            dynamicCol = "" & colBasic & " (" & colBasicTotal & ") AS [Total Basic], " & colPD & " (" & colPDTotal & ") as [Total Payable Days], " & colBO & " (" & colBOTotal & ") as [Total Bonus Wages], round((" & colBOTotal & ")*BONUS_RATE/100,0) as [Total Bonus]"
        End If
        Qry = " select * from (select EMP_CODE AS empcode,EMP_NAME as empname,FATHERS_NAME as fname,DEPARTMENT_CODE as dcode,DEPARTMENT_NAME as dname,LOCATION_CODE as location,Location_Desc as locdesc,PF_NO as pfno,Designation as desgcode,Designation_Desc as desgDesc, " & _
              " Joining_date as doj,RELIEVING_DATE as dol,BONUS_CODE AS bonuscode,BONUS_NAME as bonusname,BONUS_RATE as bonusrate," & dynamicCol & " from " & _
              " (" & qryBase & ") as Final " & _
              " PIVOT " & _
              " ( " & _
              " sum(Std_Basic) " & _
              " FOR Pay_Period_Code IN (" & PivBasic & ") " & _
              " ) AS Wages " & _
              " PIVOT " & _
              " ( " & _
              " sum(PAYABLE_DAYS) " & _
              " FOR PD_PAY_PERIOD_CODE IN (" & PivPD & ") " & _
              " ) AS PDays " & _
              " PIVOT " & _
              " ( " & _
              " sum(Bonus_On) " & _
              " FOR BonusWages_PAY_PERIOD_CODE IN (" & PivBO & ") " & _
              " ) AS Bonus_On " & _
              " group by EMP_CODE,BONUS_CODE,BONUS_NAME,BONUS_RATE,EMP_NAME,FATHERS_NAME,DEPARTMENT_CODE,DEPARTMENT_NAME,LOCATION_CODE,Location_Desc,PF_NO,Designation,Designation_Desc,Joining_date,RELIEVING_DATE) Final " & _
              " where [Total Bonus]>0"

        Return Qry
    End Function
    Public Shared Function GetGenerateBonusDetailBaseQuery(ByVal Location As String, ByVal Division As String, ByVal strFromPayPeriod As String, ByVal strToPayPeriod As String, ByVal strPayablePayPeriod As String) As String
        Dim qry As String = ""
        Dim strCond As String = " WHERE 2=2 "
        If clsCommon.myLen(Location) > 0 Then
            strCond = strCond & " AND GS.Location_Code='" & Location & "'"
        End If
        If clsCommon.myLen(Division) > 0 Then
            strCond = strCond & " AND GS.DEVISION_CODE='" & Division & "'"
        End If
        qry = " select GSA.EMP_CODE,EMP.EMP_NAME,EMP.FATHERS_NAME,EMP.DEPARTMENT_CODE,DEPT.DEPARTMENT_NAME,EMP.LOCATION_CODE,LOC.Location_Desc,EMP.PF_NO,EMP.Designation,DES.Designation_Desc,EMP.Joining_date,EMP.RELIEVING_DATE,GS.PAY_PERIOD_CODE as PAY_PERIOD_CODE_MAIN,'Amount_' + GS.PAY_PERIOD_CODE as PAY_PERIOD_CODE,'PD_' + GS.PAY_PERIOD_CODE as PD_PAY_PERIOD_CODE,'BonusWages_' + GS.PAY_PERIOD_CODE as BonusWages_PAY_PERIOD_CODE,(case when GSP.Rate_Amount<=BONUS.COND_MAX_EARNING_PER_MONTH then GSP.RATE_AMOUNT else 0 end) AS Std_Basic,(case when GSP.Rate_Amount<=BONUS.COND_MAX_EARNING_PER_MONTH then GSP.ACTUAL_AMOUNT else 0 end) AS Actual_Basic,(case when GSP.Rate_Amount<=BONUS.COND_MAX_EARNING_PER_MONTH then GSA.PAYPERIOD_DAYS else 0 end) as PAYPERIOD_DAYS,(case when GSP.Rate_Amount<=BONUS.COND_MAX_EARNING_PER_MONTH then GSA.PAYABLE_DAYS else 0 end) as PAYABLE_DAYS," & _
              " (case when GSP.Rate_Amount<=BONUS.COND_MAX_EARNING_PER_MONTH " & _
              " then ROUND((case when GSP.Rate_Amount>=BONUS.COND_BASIC_PER_MONTH then BONUS.COND_BASIC_PER_MONTH else GSP.Rate_Amount end)* (case when BONUS.Is_Consider_Pay_Days=1 then GSA.PAYABLE_DAYS/GSA.PAYPERIOD_DAYS else 1 end),0) ELSE 0 end) as Bonus_On, " & _
              " (case when GSP.Rate_Amount<=BONUS.COND_MAX_EARNING_PER_MONTH " & _
              " then ROUND((case when GSP.Rate_Amount>=BONUS.COND_BASIC_PER_MONTH then BONUS.COND_BASIC_PER_MONTH else GSP.Rate_Amount end)*(case when BONUS.Is_Consider_Pay_Days=1 then GSA.PAYABLE_DAYS/GSA.PAYPERIOD_DAYS else 1 end),0) ELSE 0 end)*BONUS.BONUS_RATE/100 AS BONUS_AMOUNT, " & _
              " ESTS.BONUS_CODE,BONUS.BONUS_NAME,BONUS.BONUS_RATE " + Environment.NewLine + _
              " from TSPL_GENERATE_SALARY_ATTENDANCE GSA " + Environment.NewLine + _
              " inner join TSPL_GENERATE_SALARY GS ON GSA.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE " + Environment.NewLine + _
              " left join TSPL_EMPLOYEE_MASTER EMP ON EMP.EMP_CODE=GSA.EMP_CODE " & _
              " LEFT JOIN TSPL_EMPLOYEE_STATUS ESTS ON GSA.EMP_CODE=ESTS.EMP_CODE AND GSA.EMP_STATUS_CODE=ESTS.EMP_STATUS_CODE " & _
              " LEFT JOIN TSPL_BONUS_MASTER BONUS ON ESTS.BONUS_CODE=BONUS.BONUS_CODE " & _
              " left join (" + Environment.NewLine + _
              " SELECT SALARY_GENERATION_CODE,EMP_CODE,Rate_Amount,ACTUAL_AMOUNT,'Basic' as CalculationMethod FROM TSPL_GENERATE_SALARY_PAYHEADS  WHERE SUB_HEAD_TYPE='BASIC' " + Environment.NewLine + _
              " union all" + Environment.NewLine + _
              " select SALARY_GENERATION_CODE,EMP_CODE,sum(ACTUAL_AMOUNT * case when x.ISEARNING=1 then 1 else -1 end) as Rate_Amount,sum(ACTUAL_AMOUNT* case when x.ISEARNING=1 then 1 else -1 end) as ACTUAL_AMOUNT,'Net' as CalculationMethod  from (" + Environment.NewLine + _
              " SELECT SALARY_GENERATION_CODE,EMP_CODE,Rate_Amount,ACTUAL_AMOUNT,TSPL_PAYHEAD_MASTER.ISEARNING FROM TSPL_GENERATE_SALARY_PAYHEADS as innTSPL_GENERATE_SALARY_PAYHEADS" + Environment.NewLine + _
              " left outer join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE= innTSPL_GENERATE_SALARY_PAYHEADS.PAY_HEAD_CODE " + Environment.NewLine + _
              " )x group by SALARY_GENERATION_CODE, EMP_CODE  " + Environment.NewLine + _
              " ) GSP ON GSA.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE  AND GSA.EMP_CODE=GSP.EMP_CODE and GSP.CalculationMethod=BONUS.Calculation_Method " & _
              " left join TSPL_PAYPERIOD_MASTER PM on PM.PAY_PERIOD_CODE=GS.PAY_PERIOD_CODE " & _
              " left join TSPL_DEPARTMENT_MASTER DEPT ON EMP.DEPARTMENT_CODE=DEPT.DEPARTMENT_CODE " & _
              " left join TSPL_LOCATION_MASTER LOC ON EMP.Location_Code=LOC.Location_Code " & _
              " left join TSPL_DESIGNATION_MASTER DES ON EMP.Designation=DES.Designation_id " & _
              " " & strCond & " AND ESTS.BONUS_CODE is not null " & _
              " and PM.DATE_FROM BETWEEN   (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strFromPayPeriod & "') " & _
              " AND (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" & strToPayPeriod & "') "
        Return qry
    End Function
    Public Shared Function GetGenerateBonusDetailBaseQuery(ByVal Doc_Code As String) As String
        Dim qry As String = ""
        Dim strCond As String = " WHERE 2=2 "
        If clsCommon.myLen(Doc_Code) > 0 Then
            strCond = strCond & " AND EMP_BONUS_CODE='" & Doc_Code & "'"
        End If
        qry = " select BGD.EMP_CODE,EMP.EMP_NAME,EMP.FATHERS_NAME,EMP.DEPARTMENT_CODE,DEPT.DEPARTMENT_NAME,EMP.LOCATION_CODE,LOC.Location_Desc," & _
              " EMP.PF_NO,EMP.Designation,DES.Designation_Desc,EMP.Joining_date,EMP.RELIEVING_DATE,'Amount_' + BGD.PAY_PERIOD_CODE as PAY_PERIOD_CODE, " & _
              " 'PD_' + BGD.PAY_PERIOD_CODE as PD_PAY_PERIOD_CODE,'BonusWages_' + BGD.PAY_PERIOD_CODE as BonusWages_PAY_PERIOD_CODE, " & _
              " BGD.STD_AMOUNT AS Std_Basic,BGD.ACTUAL_AMOUNT AS Actual_Basic,BGD.PAYPERIOD_DAYS,BGD.PAYABLE_DAYS," & _
              " BGD.Bonus_On,BGD.BONUS_AMOUNT,BGD.BONUS_CODE,BONUS.BONUS_NAME,BGD.BONUS_RATE from TSPL_BONUS_GENERATION_DETAIL BGD " & _
              " left join TSPL_EMPLOYEE_MASTER EMP ON EMP.EMP_CODE=BGD.EMP_CODE " & _
              " left join TSPL_BONUS_MASTER BONUS ON BGD.BONUS_CODE=BONUS.BONUS_CODE " & _
              " left join TSPL_PAYPERIOD_MASTER PM on PM.PAY_PERIOD_CODE=BGD.PAY_PERIOD_CODE " & _
              " left join TSPL_DEPARTMENT_MASTER DEPT ON EMP.DEPARTMENT_CODE=DEPT.DEPARTMENT_CODE " & _
              " left join TSPL_LOCATION_MASTER LOC ON EMP.Location_Code=LOC.Location_Code " & _
              " left join TSPL_DESIGNATION_MASTER DES ON EMP.Designation=DES.Designation_id " & _
              " " & strCond & " "
        Return qry
    End Function
End Class
Public Class clsBonusDetails

#Region "Variables"
    Public EMP_BONUS_CODE As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public BONUS_CODE As String
    Public BONUS_NAME As String
    Public BONUS_AMOUNT As Double
#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_EMPBONUS_DETAIL where EMP_BONUS_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsBonusDetails)
        Dim obj As clsBonusDetails = Nothing
        Dim ObjList As New List(Of clsBonusDetails)
        Dim qry As String = " select TSPL_EMPBONUS_DETAIL.EMP_CODE, TSPL_EMPLOYEE_MASTER.EMP_name, TSPL_EMPBONUS_DETAIL.BONUS_CODE, TSPL_BONUS_MASTER.BONUS_NAME, TSPL_EMPBONUS_DETAIL.BONUS_AMOUNT  from TSPL_EMPBONUS_DETAIL left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_EMPBONUS_DETAIL.EMP_CODE left outer join TSPL_BONUS_MASTER on TSPL_BONUS_MASTER.BONUS_CODE = TSPL_EMPBONUS_DETAIL.BONUS_CODE "
        qry += " where EMP_BONUS_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsBonusDetails()
                obj.EMP_BONUS_CODE = strCode
                obj.EMP_CODE = clsCommon.myCstr(dr("EMP_CODE"))
                obj.EMP_NAME = clsCommon.myCstr(dr("EMP_NAME"))
                obj.BONUS_CODE = clsCommon.myCstr(dr("BONUS_CODE"))
                obj.BONUS_NAME = clsCommon.myCstr(dr("BONUS_NAME"))
                obj.BONUS_AMOUNT = clsCommon.myCdbl(dr("BONUS_AMOUNT"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList

    End Function

    Public Function SaveData(ByVal obj As clsBonus, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each objTr As clsBonusDetails In obj.ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "EMP_BONUS_CODE", objTr.EMP_BONUS_CODE)
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", objTr.EMP_CODE)
                clsCommon.AddColumnsForChange(coll, "BONUS_AMOUNT", objTr.BONUS_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "BONUS_CODE", objTr.BONUS_CODE)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPBONUS_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                'Dim qry As String = "SELECT Count(*) FROM TSPL_EMPBONUS_DETAIL where EMP_BONUS_CODE = '" & strCode & "' and EMP_CODE = '" & obj.EMP_CODE & "' and BONUS_CODE = '" & obj.BONUS_CODE & "' "
                'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

                'If check = 0 Then
                '    clsCommon.AddColumnsForChange(coll, "EMP_BONUS_CODE", strCode)
                '    ''  clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                '    ''clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPBONUS_DETAIL", OMInsertOrUpdate.Insert, "")
                'Else
                '    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPBONUS_DETAIL", OMInsertOrUpdate.Update, " EMP_BONUS_CODE = '" & strCode & "' and EMP_CODE = '" & obj.EMP_CODE & "' and BONUS_CODE = '" & obj.BONUS_CODE & "' ")
                'End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function


End Class
