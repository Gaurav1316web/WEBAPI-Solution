Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.Math
Public Class clsEmployeeGratuity

#Region "Variables"
    Public EMP_CODE As String
    Public Emp_Name As String
    Public DOJ As DateTime
    Public DOL As DateTime
    Public LASTDRAWNSALARY As Double
    Public NOOFYEARS As Integer
    Public GRATUITYAMT As Double
    Public CREATEDBY As Double
    Public CREATEDATE As Date
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeGratuity
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_GRATUITY where EMP_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEmployeeGratuity
        Dim obj As clsEmployeeGratuity = Nothing
        Dim qry As String = "select EMP_CODE,Emp_Name,Joining_date DOJ,RELIEVING_DATE DOL from TSPL_EMPLOYEE_MASTER where 2=2 and RELIEVING_DATE>convert(datetime,Joining_date,103)  "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select MIN(EMP_CODE) from TSPL_EMPLOYEE_MASTER where RELIEVING_DATE>convert(datetime,Joining_date,103))"
            Case NavigatorType.Last
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select Max(EMP_CODE) from TSPL_EMPLOYEE_MASTER where RELIEVING_DATE>convert(datetime,Joining_date,103))"
            Case NavigatorType.Next
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select Min(EMP_CODE) from TSPL_EMPLOYEE_MASTER where  EMP_CODE>'" + strCode + "' and RELIEVING_DATE>convert(datetime,Joining_date,103))"
            Case NavigatorType.Previous
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = (select Max(EMP_CODE) from TSPL_EMPLOYEE_MASTER where EMP_CODE<'" + strCode + "' and RELIEVING_DATE>convert(datetime,Joining_date,103))"
            Case NavigatorType.Current
                qry += " and TSPL_EMPLOYEE_MASTER.EMP_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsEmployeeGratuity()
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.Emp_Name = clsCommon.myCstr(dt.Rows(0)("Emp_Name"))

            If Date.TryParse(dt.Rows(0)("DOJ").ToString, obj.DOJ) = False Then
                obj.DOJ = Nothing
            End If
            If Date.TryParse(dt.Rows(0)("DOL").ToString, obj.DOL) = False Then
                obj.DOL = Nothing
            End If
            obj.NOOFYEARS = Round(obj.DOL.Subtract(obj.DOJ).TotalDays / 365, 0)
        Else
            Return obj
        End If
        'qry = " select t1.EMP_CODE,max(t3.DATE_FROM)DateFrom,"
        'qry += " sum(case when SUB_HEAD_TYPE='BASIC' then t1.ACTUAL_AMOUNT else 0 end)BasicAmt,"
        'qry += " sum(case when SUB_HEAD_TYPE='DA' then t1.ACTUAL_AMOUNT else 0 end)DAAmt"
        'qry += " from TSPL_GENERATE_SALARY_PAYHEADS t1"
        'qry += " inner join TSPL_GENERATE_SALARY t2 on t1.SALARY_GENERATION_CODE=t2.SALARY_GENERATION_CODE"
        'qry += " inner join TSPL_PAYPERIOD_MASTER t3 on t3.PAY_PERIOD_CODE=t2.PAY_PERIOD_CODE"
        'qry += " where (SUB_HEAD_TYPE='BASIC' or SUB_HEAD_TYPE='DA') and t1.EMP_CODE='" & obj.EMP_CODE & "'"
        'qry += " group by t1.EMP_CODE order by t1.EMP_CODE"



        qry = "select sum( TSPL_GENERATE_SALARY_PAYHEADS.ACTUAL_AMOUNT) as ACTUAL_AMOUNT  " + Environment.NewLine + _
        "from TSPL_GENERATE_SALARY_PAYHEADS" + Environment.NewLine + _
        "where (TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='BASIC' or TSPL_GENERATE_SALARY_PAYHEADS.SUB_HEAD_TYPE='DA') and TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE='" + obj.EMP_CODE + "' and TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE in (select top 1 TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE from TSPL_GENERATE_SALARY_PAYHEADS " + Environment.NewLine + _
        "left outer join TSPL_GENERATE_SALARY on TSPL_GENERATE_SALARY.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE" + Environment.NewLine + _
        "where TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE='" + obj.EMP_CODE + "'  order by TSPL_GENERATE_SALARY.GENERATE_DATE  desc)"

        Dim dtP As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dtP IsNot Nothing AndAlso dtP.Rows.Count > 0) Then
            'obj.LASTDRAWNSALARY = Val(dtP.Rows(0)("BasicAmt").ToString) + Val(dtP.Rows(0)("DAAmt").ToString)
            obj.LASTDRAWNSALARY = clsCommon.myCdbl(dtP.Rows(0)("ACTUAL_AMOUNT"))
        Else
            obj.LASTDRAWNSALARY = 0
        End If
        '======shivani
        Dim Qry1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Location_Code from TSPL_EMPLOYEE_MASTER where Emp_Code='" & obj.EMP_CODE & "'"))
        Dim Qry2 As Integer = clsDBFuncationality.getSingleValue("select Gratuity_Period from TSPL_PAYROLL_SETTING where LOCATION_CODE='" & Qry1 & "'")

        If clsCommon.myCdbl(Qry2 = obj.NOOFYEARS) Or clsCommon.myCdbl(Qry2 < obj.NOOFYEARS) Then
            obj.GRATUITYAMT = Round((obj.LASTDRAWNSALARY * obj.NOOFYEARS * 15) / 26, 0)
        Else
            obj.GRATUITYAMT = 0
        End If





        Return obj


    End Function

    Public Function SaveData(ByVal obj As clsEmployeeGratuity, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DOJ", clsCommon.GetPrintDate(obj.DOJ, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "DOL", clsCommon.GetPrintDate(obj.DOL, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "LASTDRAWNSALARY", obj.LASTDRAWNSALARY)
            clsCommon.AddColumnsForChange(coll, "NOOFYEARS", obj.NOOFYEARS)
            clsCommon.AddColumnsForChange(coll, "GRATUITYAMT", obj.GRATUITYAMT)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))

            Dim qry As String = "SELECT Count(*) FROM TSPL_GRATUITY where EMP_CODE= '" & obj.EMP_CODE & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRATUITY", OMInsertOrUpdate.Insert, "")
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_GRATUITY", OMInsertOrUpdate.Update, "EMP_CODE='" + obj.EMP_CODE + "'")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
End Class
