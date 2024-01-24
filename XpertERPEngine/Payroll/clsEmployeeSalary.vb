Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class clsEmployeeSalary

#Region "Variables"

    Public EMP_SAL_CODE As String
    Public EMP_CODE As String
    Public EMP_NAME As String
    Public REVISION_NO As Integer
    Public APPLICABLE_FROM As Date
    Public SALARY_STRUCT_CODE As String
    Public SALARY_STRUCT_NAME As String
    Public POSTED As Boolean
    Public Posting_Date As Date
    Public Location_Code As String = Nothing
    ' '' grid columns
    'Public Line_No As Integer
    'Public PayHeadCode As String
    'Public PayHeadName As String
    'Public Formula As String
    'Public Rate_Amount As Decimal
    'Public IsHiddenComponent As Boolean

    Public Shared ObjList As List(Of clsEmpSalaryPayHeadDetails)
    Public Arr As New List(Of clsEmpSalaryPayHeadDetails)

#End Region
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsEmployeeSalary
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim Qry As String = "select POSTED from TSPL_EMPLOYEE_SALARY where EMP_SAL_CODE='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) as cnt from TSPL_GENERATE_SALARY_ATTENDANCE where EMP_SAL_CODE='" & strCode & "'", trans)
            If check > 0 Then
                Throw New Exception("Salary Structure used in Transaction.")
            Else
                Qry = "Update TSPL_EMPLOYEE_SALARY set POSTED = 0 where EMP_SAL_CODE='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If



            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal ShowAll As Boolean, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = ""
        If ShowAll = False Then
            qry = " select Sal.EMP_SAL_CODE AS Code,Struct.SALARY_STRUCTURE_NAME as [Structure Name],Sal.EMP_CODE as [Employee Code],Emp.EMP_NAME as [Employee Name],Emp.Location_Code as [Location Code],Emp.DEVISION_CODE as [Division Code],Sal.APPLICABLE_FROM as [Applicable From], " &
                  " Sal.REVISION_NO AS [Revision No], Sal.POSTED,Emp.PF_NO as [PF No]  " &
                  " from (select Sal.* from TSPL_EMPLOYEE_SALARY Sal inner join " &
                  " (select EMP_CODE,max(REVISION_NO) as REVISION_NO from TSPL_EMPLOYEE_SALARY group by EMP_CODE) as Latest_Sal " &
                  " on Sal.EMP_CODE=Latest_Sal.EMP_CODE and Sal.REVISION_NO=Latest_Sal.REVISION_NO) Sal " &
                  " LEFT JOIN TSPL_EMPLOYEE_MASTER Emp ON Sal.EMP_CODE=Emp.EMP_CODE  " &
                  " LEFT JOIN TSPL_SALARY_STRUCTURE Struct ON Sal.SALARY_STRUCTURE_CODE=Struct.SALARY_STRUCTURE_CODE"
        Else
            qry = " select Sal.EMP_SAL_CODE AS Code,Struct.SALARY_STRUCTURE_NAME as [Structure Name],Sal.EMP_CODE as [Employee Code],Emp.EMP_NAME as [Employee Name],Emp.Location_Code as [Location Code],Emp.DEVISION_CODE as [Division Code], " &
                       " Sal.APPLICABLE_FROM as [Applicable From],Sal.REVISION_NO AS [Revision No], Sal.POSTED,Emp.PF_NO as [PF No]  " &
                       " from TSPL_EMPLOYEE_SALARY Sal " &
                       " LEFT JOIN TSPL_EMPLOYEE_MASTER Emp ON Sal.EMP_CODE=Emp.EMP_CODE " &
                       " LEFT JOIN TSPL_SALARY_STRUCTURE Struct ON Sal.SALARY_STRUCTURE_CODE=Struct.SALARY_STRUCTURE_CODE"
        End If


        str = clsCommon.ShowSelectForm("EmpSal123", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isDelete As Boolean = DeleteData(strCode, trans)
            trans.Commit()
            Return isDelete
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
            Return False
        End Try
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        isSaved = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Code not found to Delete")
        End If
        Dim qry As String
        qry = "delete from TSPL_EMPLOYEE_SALARY_PAYHEADS where EMP_SAL_CODE ='" + strCode + "'"
        isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        qry = "delete from TSPL_EMPLOYEE_SALARY where EMP_SAL_CODE ='" + strCode + "'"
        isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsEmployeeSalary
        Dim whrQry As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrQry = " And TAV.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim obj As New clsEmployeeSalary()
        Dim objtr As New clsEmpSalaryPayHeadDetails()
        ObjList = New List(Of clsEmpSalaryPayHeadDetails)

        Dim qry As String = "SELECT TAV.*, EMP.Emp_Name,SAL.SALARY_STRUCTURE_NAME  FROM TSPL_EMPLOYEE_SALARY TAV " _
                            & " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON TAV.EMP_CODE=EMP.EMP_CODE " _
                            & " left join TSPL_SALARY_STRUCTURE SAL ON TAV.SALARY_STRUCTURE_CODE=SAL.SALARY_STRUCTURE_CODE where 2=2 " + whrQry

        Select Case NavType
            Case NavigatorType.First
                qry += " and EMP_SAL_CODE = (select MIN(EMP_SAL_CODE) from TSPL_EMPLOYEE_SALARY)"
            Case NavigatorType.Last
                qry += " and EMP_SAL_CODE = (select Max(EMP_SAL_CODE) from TSPL_EMPLOYEE_SALARY)"
            Case NavigatorType.Next
                qry += " and EMP_SAL_CODE = (select Min(EMP_SAL_CODE) from TSPL_EMPLOYEE_SALARY where  EMP_SAL_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and EMP_SAL_CODE = (select Max(EMP_SAL_CODE) from TSPL_EMPLOYEE_SALARY where EMP_SAL_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and EMP_SAL_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            'obj.SALARY_STRUCTURE_NAME = dt.Rows(0)("SALARY_STRUCTURE_NAME")
            obj.EMP_SAL_CODE = dt.Rows(0)("EMP_SAL_CODE")
            obj.APPLICABLE_FROM = dt.Rows(0)("APPLICABLE_FROM")
            strCode = dt.Rows(0)("EMP_SAL_CODE")

            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.SALARY_STRUCT_CODE = clsCommon.myCstr(dt.Rows(0)("SALARY_STRUCTURE_CODE"))
            obj.SALARY_STRUCT_NAME = clsCommon.myCstr(dt.Rows(0)("SALARY_STRUCTURE_NAME"))
            obj.REVISION_NO = clsCommon.myCdbl(dt.Rows(0)("REVISION_NO"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0 Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
        End If
        qry = "select TAVD.*,TPH.PAY_HEAD_NAME" _
             & "  FROM TSPL_EMPLOYEE_SALARY_PAYHEADS TAVD " _
             & " INNER JOIN  TSPL_EMPLOYEE_SALARY TAV ON TAVD.EMP_SAL_CODE=TAV.EMP_SAL_CODE " _
             & " LEFT JOIN TSPL_PAYHEAD_MASTER TPH ON TAVD.PAY_HEAD_CODE=TPH.PAY_HEAD_CODE where 2=2" + whrQry
        qry += " and TAV.EMP_SAL_CODE = '" + strCode + "' ORDER BY TAVD.LINE_NO"
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsEmpSalaryPayHeadDetails()
                objtr.Line_No = clsCommon.myCdbl(dr("LINE_NO"))
                objtr.PayHeadCode = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.PayHeadName = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                objtr.Formula = clsCommon.myCstr(dr("PAYHEAD_FORMULA"))
                objtr.Rate_Amount = clsCommon.myCdbl(dr("RATE_AMOUNT"))
                objtr.IsHiddenComponent = clsCommon.myCBool(dr("IsHiddenComponent"))
                objtr.MAX_AMOUNT = clsCommon.myCdbl(dr("MAX_AMOUNT"))
                objtr.PAYPERIOD_AMOUNT = clsCommon.myCdbl(dr("PAYPERIOD_AMOUNT"))
                ObjList.Add(objtr)
            Next
        End If
        clsEmployeeSalary.ObjList = ObjList
        Return obj
    End Function
    Public Function SaveData(ByVal obj As clsEmployeeSalary, ByVal objList As List(Of clsEmpSalaryPayHeadDetails), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim isSaved As Boolean = SaveData(obj, objList, isNewEntry, strCode, trans)
        Try
            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Function SaveData(ByVal obj As clsEmployeeSalary, ByVal objList As List(Of clsEmpSalaryPayHeadDetails), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "", Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True

        If isNewEntry Then
            If strCode = "" Then
                obj.EMP_SAL_CODE = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(clsCommon.GETSERVERDATE(trans)), clsDocType.EmployeeSalary, "", "")
            Else
                obj.EMP_SAL_CODE = strCode
            End If
        End If
        Dim qry As String = "delete from TSPL_EMPLOYEE_SALARY_PAYHEADS where EMP_SAL_CODE='" + obj.EMP_SAL_CODE + "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Dim strDocNo As String = ""

        If (clsCommon.myLen(obj.EMP_SAL_CODE) <= 0) Then
            Throw New Exception("Error in Document Code Generation")
        End If

        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
        clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "SALARY_STRUCTURE_CODE", obj.SALARY_STRUCT_CODE)
        clsCommon.AddColumnsForChange(coll, "REVISION_NO", obj.REVISION_NO)
        clsCommon.AddColumnsForChange(coll, "POSTED", "0")
        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
        clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
        If isNewEntry Then
            clsCommon.AddColumnsForChange(coll, "EMP_SAL_CODE", obj.EMP_SAL_CODE)
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            Dim Strqry As String = "SELECT Count(*) FROM TSPL_EMPLOYEE_SALARY where EMP_SAL_CODE = '" & obj.EMP_SAL_CODE & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(Strqry, trans)
            If check = 0 Then
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SALARY", OMInsertOrUpdate.Insert, "", trans)
            Else
                Throw New Exception("This Code:" + obj.EMP_SAL_CODE + " Is Already Exist")
            End If
        Else
            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SALARY", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE='" + obj.EMP_SAL_CODE + "'", trans)
        End If
        isSaved = isSaved AndAlso clsEmpSalaryPayHeadDetails.SaveData(obj.EMP_SAL_CODE, objList, trans)

        qry = " UPDATE TSPL_EMPLOYEE_SALARY_PAYHEADS SET TSPL_EMPLOYEE_SALARY_PAYHEADS.LINE_NO=TSPL_SALSTRUCT_PAYHEADS.LINE_NO FROM  " &
                 " TSPL_SALSTRUCT_PAYHEADS INNER JOIN TSPL_EMPLOYEE_SALARY " &
                 " ON TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE=TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE  " &
                 " WHERE(TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE = TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE)  " &
                 " AND TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE AND TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE='" & obj.SALARY_STRUCT_CODE & "' and  TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE='" & obj.EMP_SAL_CODE & "'"
        isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Return isSaved
    End Function
    Public Shared Function GetPayHeadCodeString(ByVal SalStructCode As String) As String
        Dim qry As String
        Dim SalStructStr As String
        'Dim dt As DataTable

        'qry = " select left(fields,len(fields)-1)  from (select ( select '['+PAY_HEAD_CODE   +'],'   " & _
        '      " from (select distinct TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE   from TSPL_EMPLOYEE_SALARY_PAYHEADS" & _
        '      " inner join TSPL_EMPLOYEE_SALARY on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE " & _
        '      " where TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE='" & SalStructCode & "' " & _
        '      " ) xx  FOR XML PATH ('')) Fields ) yy"

        qry = " select left(fields,len(fields)-1)  from (select ( select '['+PAY_HEAD_CODE   +'],'   " &
             " from (select distinct TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE , TSPL_SALSTRUCT_PAYHEADS.LINE_NO  from TSPL_SALSTRUCT_PAYHEADS where TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE='" & SalStructCode & "' " &
             " ) xx order by xx.LINE_NO   FOR XML PATH ('')) Fields ) yy"
        SalStructStr = clsDBFuncationality.getSingleValue(qry)
        'SalStructStr = SalStructStr.Replace(".", "_")
        Return SalStructStr
    End Function
    Public Shared Function GetPayHeadCodeStringForSelect(ByVal SalStructCode As String) As String
        Dim qry As String
        Dim SalStructStr As String
        Dim SalStructStrForSelect As String = ""
        'Dim dt As DataTable

        qry = " select left(fields,len(fields)-1)  from (select ( select '['+PAY_HEAD_CODE   +'],'   " &
              " from (select distinct TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE   from TSPL_EMPLOYEE_SALARY_PAYHEADS" &
              " inner join TSPL_EMPLOYEE_SALARY on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE " &
              " where TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE='" & SalStructCode & "' " &
              " ) xx  FOR XML PATH ('')) Fields ) yy"
        SalStructStr = clsDBFuncationality.getSingleValue(qry)
        Dim arrPayHead() As String
        arrPayHead = SalStructStr.Split(",")
        For Each Str As String In arrPayHead
            If clsCommon.myLen(SalStructStrForSelect) <= 0 Then
                SalStructStrForSelect = Str & " as " & Str.Replace(".", "_")
            Else
                SalStructStrForSelect = SalStructStrForSelect & "," & Str & " as " & Str.Replace(".", "_")
            End If
        Next

        Return SalStructStrForSelect
    End Function
    Public Shared Function GetPayHeadCodeINSelect(ByVal SalStructCode As String) As String
        Dim qry As String
        Dim SalStructStrSel As String
        qry = "select left(fields,len(fields)-1)  from (select (select 'max' + '(' + '['+PAY_HEAD_CODE   +']' " &
              " +')' +'as ' + '['+PAY_HEAD_CODE   +']'  + ','  " &
              " from (select distinct  TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE   from TSPL_EMPLOYEE_SALARY_PAYHEADS " &
              " inner join TSPL_EMPLOYEE_SALARY on TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE " &
              " where TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE='" & SalStructCode & "' " &
              " ) xx  FOR XML PATH ('')) Fields ) yy"
        SalStructStrSel = clsDBFuncationality.getSingleValue(qry)
        Return SalStructStrSel

    End Function
    Public Shared Function GetRevisionNo(ByVal EmpCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Integer
        Return clsDBFuncationality.GetDataTable("select (coalesce(max(revision_no),0)+1) AS revision_no from TSPL_EMPLOYEE_SALARY where EMP_CODE='" & EmpCode & "'", trans).Rows(0).Item("revision_no")
    End Function

    Public Shared Function ExportEmployeeSalary(ByVal SalStructCode As String, ByVal frm As RadForm, ByVal SalStructDate As Date) As String
        Dim qry As String = ""
        Dim strSelect As String
        strSelect = GetPayHeadCodeString(SalStructCode)
        If clsCommon.myLen(strSelect) <= 0 Then
            clsCommon.MyMessageBoxShow("Pay Head Code not found")
            Return qry
        End If
        '', " & strSelect & "
        qry = " SELECT EMP_CODE AS [Emp ID],Emp_Name as [Employee Name],SALARY_STRUCTURE_CODE as [Salary Structure Code],REVISION_NO as [Revision No],APPLICABLE_FROM as [Applicable Date] , '' as [Copy Salary Code] FROM " &
              " (SELECT EMPSAL.EMP_SAL_CODE, EMPSAL.EMP_CODE,TSPL_EMPLOYEE_MASTER.Emp_Name,TSPL_EMPLOYEE_MASTER.RESIGNATION_SUBMIT_DATE,EMPSAL.SALARY_STRUCTURE_CODE ,EMPSAL.APPLICABLE_FROM,EMPSAL.REVISION_NO,SALPH.PAY_HEAD_CODE,SALPH.RATE_AMOUNT,EMPSAL.POSTED,EMPSAL.Posting_Date,EMPSAL.Created_By,EMPSAL.Created_Date,EMPSAL.Modified_By,EMPSAL.Modified_Date,Location_Code " &
              " FROM (SELECT TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE,TSPL_EMPLOYEE_SALARY.SALARY_STRUCTURE_CODE,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM,SAL.EMP_CODE,SAL.REVISION_NO,POSTED,Posting_Date,Created_By,Created_Date,Modified_By,Modified_Date " &
              " FROM TSPL_EMPLOYEE_SALARY INNER JOIN (SELECT EMP_CODE, MAX(REVISION_NO) AS REVISION_NO " &
              " FROM TSPL_EMPLOYEE_SALARY GROUP BY  EMP_CODE) AS SAL ON TSPL_EMPLOYEE_SALARY.EMP_CODE=SAL.EMP_CODE " &
              " AND TSPL_EMPLOYEE_SALARY.REVISION_NO=SAL.REVISION_NO) AS EMPSAL INNER JOIN TSPL_EMPLOYEE_SALARY_PAYHEADS SALPH " &
              " ON  EMPSAL.EMP_SAL_CODE=SALPH.EMP_SAL_CODE inner join TSPL_EMPLOYEE_MASTER on EMPSAL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE  ) p " &
              " Pivot " &
              " ( " &
              " MAX(Rate_Amount) " &
              " FOR [PAY_HEAD_CODE] IN " &
              " ( " & strSelect & " ) " &
              " ) AS pvt  "

        ''where 2=2 and (RESIGNATION_SUBMIT_DATE is null or ((cast('1' + '/' + datename(month,RESIGNATION_SUBMIT_DATE) + '/' + cast(Year(RESIGNATION_SUBMIT_DATE) as varchar) as date) >=convert(date,'" + SalStructDate + "',103))))
        'strSelect = GetPayHeadCodeStringForSelect(SalStructCode)
        transportSql.ExporttoExcelNew(qry, frm, strSelect)

        '" ORDER BY pvt.EMP_CODE;"
        Return qry
    End Function
    Public Shared Function getPayHeadAmount(ByVal EmpCode As String, ByVal SalStructCode As String, ByVal PayHeadCode As String, ByVal applicableFrom As Date, ByVal CopySalaryCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = Nothing
        If clsCommon.myLen(CopySalaryCode) > 0 Then
            qry = " select RATE_AMOUNT from TSPL_EMPLOYEE_SALARY_PAYHEADS where EMP_SAL_CODE = '" & CopySalaryCode & "' and PAY_HEAD_CODE='" & PayHeadCode & "' "
        Else
            qry = "select emp.EMP_CODE,emp.EMP_SAL_CODE,emp.REVISION_NO,TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE, " &
            " TSPL_EMPLOYEE_SALARY_PAYHEADS.RATE_AMOUNT from (" &
            " select EMP_CODE,MAX(EMP_SAL_CODE) AS EMP_SAL_CODE,MAX(REVISION_NO) AS REVISION_NO " &
            " from TSPL_EMPLOYEE_SALARY WHERE  APPLICABLE_FROM<='" & clsCommon.GetPrintDate(applicableFrom, "dd/MMM/yyyy") & "' and SALARY_STRUCTURE_CODE='" & SalStructCode & "' GROUP BY EMP_CODE " &
            " HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(applicableFrom, "dd/MMM/yyyy") & "') as emp  inner join TSPL_EMPLOYEE_SALARY_PAYHEADS " &
            " on emp.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE where EMP_CODE='" & EmpCode & "' and PAY_HEAD_CODE='" & PayHeadCode & "'"
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("RATE_AMOUNT")
        Else
            Return 0
        End If
    End Function
    Public Shared Function getPayHeadAmount_Arrear(ByVal EmpCode As String, ByVal SalStructCode As String, ByVal PayHeadCode As String, ByVal Revision_No As Integer, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select Sal.EMP_CODE,Sal.EMP_SAL_CODE,Sal.REVISION_NO,emp.PAY_HEAD_CODE,  emp.RATE_AMOUNT from TSPL_EMPLOYEE_SALARY_PAYHEADS as emp  inner join TSPL_EMPLOYEE_SALARY Sal on emp.EMP_SAL_CODE=Sal.EMP_SAL_CODE " &
        " where EMP_CODE='" & EmpCode & "' and PAY_HEAD_CODE='" & PayHeadCode & "' and Sal.REVISION_NO='" & Revision_No & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("RATE_AMOUNT")
        Else
            Return 0
        End If
    End Function
    Public Shared Function getBasicAmount(ByVal EmpCode As String, ByVal applicableFrom As Date, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select emp.EMP_CODE,emp.EMP_SAL_CODE,emp.REVISION_NO,TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE, " &
        " TSPL_EMPLOYEE_SALARY_PAYHEADS.RATE_AMOUNT from (" &
        " select EMP_CODE,MAX(EMP_SAL_CODE) AS EMP_SAL_CODE,MAX(REVISION_NO) AS REVISION_NO " &
        " from TSPL_EMPLOYEE_SALARY WHERE  APPLICABLE_FROM<='" & clsCommon.GetPrintDate(applicableFrom, "dd/MMM/yyyy") & "' GROUP BY EMP_CODE " &
        " HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(applicableFrom, "dd/MMM/yyyy") & "') as emp " &
        " INNER JOIN TSPL_EMPLOYEE_SALARY_PAYHEADS on emp.EMP_SAL_CODE=TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE " &
        " INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE=TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE " &
        " where emp.EMP_CODE='" & EmpCode & "' AND SUB_HEAD_TYPE='Basic'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("RATE_AMOUNT")
        Else
            Return 0
        End If
    End Function


    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsEmployeeSalary = clsEmployeeSalary.GetData(strDocNo, NavigatorType.Current, trans)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.EMP_SAL_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update TSPL_EMPLOYEE_SALARY set POSTED=1, Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where EMP_SAL_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


End Class


Public Class clsEmpSalaryPayHeadDetails
#Region "Variables"
    '' grid columns
    Public Line_No As Integer
    Public PayHeadCode As String
    Public PayHeadName As String
    Public Formula As String
    Public Rate_Amount As Decimal
    Public IsHiddenComponent As Boolean
    Public MAX_AMOUNT As Decimal = 0
    Public PAYPERIOD_AMOUNT As Decimal = 0
    Public Location_Code As String = Nothing
    'Public Shared ObjList As List(Of clsEmpSalaryPayHeadDetails)
    'Public Const AttendanceCode As String = "MT"
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsEmpSalaryPayHeadDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsEmpSalaryPayHeadDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "EMP_SAL_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PayHeadCode)
                clsCommon.AddColumnsForChange(coll, "PAYHEAD_FORMULA", obj.Formula)
                clsCommon.AddColumnsForChange(coll, "RATE_AMOUNT", obj.Rate_Amount)
                clsCommon.AddColumnsForChange(coll, "IsHiddenComponent", obj.IsHiddenComponent)
                clsCommon.AddColumnsForChange(coll, "MAX_AMOUNT", obj.MAX_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "PAYPERIOD_AMOUNT", obj.PAYPERIOD_AMOUNT, True)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_SALARY_PAYHEADS", OMInsertOrUpdate.Insert, "TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE='" + strDocNo + "'", trans)
            Next
        End If

        Return True
    End Function


End Class
