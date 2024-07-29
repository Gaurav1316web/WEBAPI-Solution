Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class ClsEmpIncrement

    Public INCREMENT_CODE As String
    Public INCREMENT_DATE As Date? = Nothing
    Public EMP_SAL_CODE As String
    Public REVISION_NO As Integer
    Public EMP_CODE As String
    Public APPLICABLE_FROM As Date
    Public SALARY_STRUCTURE_CODE As String
    Public location_Code As String
    Public Location_Desc As String
    Public DEVISION_CODE As String
    Public Devision_Name As String
    Public POSTED As Boolean
    Public EMP_SAL_CODE_NEW As String = ""
    Public Posting_Date As DateTime
    Public Shared ObjList As List(Of clsEmpIncrementDetail)
    Public Arr As New List(Of clsEmpIncrementDetail)
    Public ARREAR_FROM As Date? = Nothing


    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim obj As ClsEmpIncrement = ClsEmpIncrement.GetData(strCode, NavigatorType.Current, trans)
            Dim qry As String
            qry = "delete from TSPL_EMPLOYEE_INCREMENT_DETAIL where INCREMENT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_EMPLOYEE_INCREMENT_HEAD where INCREMENT_CODE ='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            isSaved = isSaved AndAlso clsEmployeeSalary.DeleteData(obj.EMP_SAL_CODE_NEW, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

    Public Function SaveData(ByVal obj As ClsEmpIncrement, ByVal objList As List(Of clsEmpIncrementDetail), ByVal isNewEntry As Boolean, Optional ByVal strCode As String = "") As Boolean
        Dim isSaved As Boolean = True
        clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleHR, clsUserMgtCode.frmEmployeeIncrement, obj.location_Code, obj.INCREMENT_DATE, Nothing)
        If isNewEntry Then
            If strCode = "" Then
                obj.INCREMENT_CODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GetPrintDate(obj.INCREMENT_DATE, "dd/MMM/yyyy"), clsDocType.EmpIncrement, "", "")
            Else
                obj.INCREMENT_CODE = strCode
            End If
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_EMPLOYEE_INCREMENT_DETAIL where INCREMENT_CODE='" + obj.INCREMENT_CODE + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            If (clsCommon.myLen(obj.INCREMENT_CODE) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "INCREMENT_CODE", obj.INCREMENT_CODE)
            clsCommon.AddColumnsForChange(coll, "EMP_SAL_CODE", obj.EMP_SAL_CODE, True)
            clsCommon.AddColumnsForChange(coll, "REVISION_NO", obj.REVISION_NO)
            clsCommon.AddColumnsForChange(coll, "SALARY_STRUCTURE_CODE", obj.SALARY_STRUCTURE_CODE)
            If Not obj.INCREMENT_DATE Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "INCREMENT_DATE", clsCommon.GetPrintDate(obj.INCREMENT_DATE, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "INCREMENT_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "APPLICABLE_FROM", clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "location_Code", obj.location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc, True)
            clsCommon.AddColumnsForChange(coll, "DEVISION_CODE", obj.DEVISION_CODE, True)
            clsCommon.AddColumnsForChange(coll, "Devision_Name", obj.Devision_Name, True)
            'clsCommon.AddColumnsForChange(coll, "EMP_SAL_CODE_NEW", obj.EMP_SAL_CODE_NEW, True)
            clsCommon.AddColumnsForChange(coll, "POSTED", "0")
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            If Not obj.ARREAR_FROM Is Nothing Then
                clsCommon.AddColumnsForChange(coll, "ARREAR_FROM", clsCommon.GetPrintDate(obj.ARREAR_FROM, "dd/MMM/yyyy"))
            End If

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_INCREMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_INCREMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE='" + obj.INCREMENT_CODE + "'", trans)
            End If
            isSaved = isSaved AndAlso clsEmpIncrementDetail.SaveData(obj.INCREMENT_CODE, objList, trans)
            obj = GetData(obj.INCREMENT_CODE, NavigatorType.Current, trans)
            Dim objSal As clsEmployeeSalary
            objSal = SaveEmployeeSalary(obj, trans, False)

            If isSaved Then
                trans.Commit()
            End If
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As ClsEmpIncrement = ClsEmpIncrement.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.INCREMENT_CODE) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim IsSaved As Boolean = True
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            Dim objSal As clsEmployeeSalary
            objSal = SaveEmployeeSalary(obj, trans, True)
            
            Dim qry As String = "Update TSPL_EMPLOYEE_INCREMENT_HEAD set POSTED = '1',EMP_SAL_CODE_NEW='" & objSal.EMP_SAL_CODE & "', Posting_Date='" + strPostDate + "',Modified_By='" + objCommonVar.CurrentUserCode + "' where INCREMENT_CODE ='" + strDocNo + "'"
            IsSaved = IsSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveEmployeeSalary(ByVal obj As ClsEmpIncrement, ByVal trans As SqlTransaction, Optional ByVal Posted As Boolean = False) As clsEmployeeSalary
        Dim isnewentry As Boolean = True
        Dim IsSaved As Boolean = True
        Dim objSal As New clsEmployeeSalary
        Dim ObjList As New List(Of clsEmpSalaryPayHeadDetails)

        objSal.EMP_SAL_CODE = obj.EMP_SAL_CODE_NEW
        objSal.EMP_CODE = obj.EMP_CODE
        If clsCommon.myLen(obj.EMP_SAL_CODE_NEW) <= 0 Then
            isnewentry = True
            objSal.REVISION_NO = clsEmployeeSalary.GetRevisionNo(obj.EMP_CODE, trans)
        Else
            objSal.REVISION_NO = obj.REVISION_NO + 1
            isnewentry = False
        End If

        objSal.APPLICABLE_FROM = clsCommon.GetPrintDate(obj.APPLICABLE_FROM, "dd/MMM/yyyy")
        objSal.SALARY_STRUCT_CODE = obj.SALARY_STRUCTURE_CODE 'lblSalaryStructCode.Text
        objSal.Location_Code = obj.location_Code
        Dim objt As clsEmpSalaryPayHeadDetails
        ObjList = New List(Of clsEmpSalaryPayHeadDetails)
        For Each objTr As clsEmpIncrementDetail In obj.Arr
            If clsCommon.myLen(clsCommon.myCstr(objTr.PayHeadCode)) > 0 Then
                objt = New clsEmpSalaryPayHeadDetails()

                objt.PayHeadCode = clsCommon.myCstr(objTr.PayHeadCode)
                objt.Line_No = clsCommon.myCdbl(objTr.Line_No)
                objt.PayHeadName = clsCommon.myCstr(objTr.PayHeadName)
                objt.Formula = clsCommon.myCstr(objTr.Formula)
                If clsPayHeadDefinitions.CheckIncrementPayhead(objTr.PayHeadCode, trans) = True Then
                    objt.Rate_Amount = objTr.IncrementedRate_Amt                    
                Else
                    objt.Rate_Amount = clsEmployeeSalary.getPayHeadAmount_Arrear(obj.EMP_CODE, obj.SALARY_STRUCTURE_CODE, objTr.PayHeadCode, obj.REVISION_NO, trans)
                End If

                'objt.Rate_Amount = objTr.IncrementedRate_Amt 'IIf(clsCommon.myCdbl(objTr.IncrementedRate_Amt) > 0, clsCommon.myCdbl(objTr.IncrementedRate_Amt), clsCommon.myCdbl(objTr.Rate_Amount))
                objt.IsHiddenComponent = clsCommon.myCdbl(objTr.IsHiddenComponent)
                objt.MAX_AMOUNT = clsCommon.myCdbl(objTr.MAX_AMOUNT)
                ObjList.Add(objt)
            End If
        Next
        IsSaved = IsSaved AndAlso objSal.SaveData(objSal, ObjList, isnewentry, objSal.EMP_SAL_CODE, trans)
        Dim coll As New Hashtable()
        clsCommon.AddColumnsForChange(coll, "INCREMENT_CODE", obj.INCREMENT_CODE)
        clsCommon.AddColumnsForChange(coll, "EMP_SAL_CODE_NEW", objSal.EMP_SAL_CODE, True)
        IsSaved = IsSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_INCREMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE='" + obj.INCREMENT_CODE + "'", trans)
        If Posted = True Then
            IsSaved = IsSaved AndAlso clsEmployeeSalary.PostData(objSal.EMP_SAL_CODE, True, trans)
        End If
        Return objSal
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsEmpIncrement
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsEmpIncrement
        Dim obj As New ClsEmpIncrement()
        Dim objtr As New clsEmpIncrementDetail()

        ObjList = New List(Of clsEmpIncrementDetail)

        Dim qry As String = "select INCREMENT_CODE ,INCREMENT_DATE ,TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE,emp_name,APPLICABLE_FROM,REVISION_NO ,EMP_SAL_CODE ,TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE ,SALARY_STRUCTURE_NAME,posted,Posting_Date,TSPL_EMPLOYEE_INCREMENT_HEAD.Location_Code,Location_Desc,TSPL_EMPLOYEE_INCREMENT_HEAD.Devision_Code,Devision_Name,EMP_SAL_CODE_NEW,ARREAR_FROM  from TSPL_EMPLOYEE_INCREMENT_HEAD left join TSPL_employee_master on TSPL_employee_master.EMP_CODE =TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE left join TSPL_SALARY_STRUCTURE on TSPL_SALARY_STRUCTURE.SALARY_STRUCTURE_CODE =TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and INCREMENT_CODE = (select MIN(INCREMENT_CODE) from TSPL_EMPLOYEE_INCREMENT_HEAD)"
            Case NavigatorType.Last
                qry += " and INCREMENT_CODE = (select Max(INCREMENT_CODE) from TSPL_EMPLOYEE_INCREMENT_HEAD)"
            Case NavigatorType.Next
                qry += " and INCREMENT_CODE = (select Min(INCREMENT_CODE) from TSPL_EMPLOYEE_INCREMENT_HEAD where  INCREMENT_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and INCREMENT_CODE = (select Max(INCREMENT_CODE) from TSPL_EMPLOYEE_INCREMENT_HEAD where INCREMENT_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and INCREMENT_CODE = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.INCREMENT_CODE = dt.Rows(0)("INCREMENT_CODE")
            obj.INCREMENT_DATE = dt.Rows(0)("INCREMENT_DATE")
            obj.APPLICABLE_FROM = dt.Rows(0)("APPLICABLE_FROM")
            strCode = dt.Rows(0)("INCREMENT_CODE")
            obj.EMP_CODE = dt.Rows(0)("EMP_CODE")
            obj.REVISION_NO = clsCommon.myCstr(dt.Rows(0)("REVISION_NO"))
            obj.EMP_SAL_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_SAL_CODE"))            
            obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
            obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
            obj.DEVISION_CODE = clsCommon.myCstr(dt.Rows(0)("DEVISION_CODE"))
            obj.Devision_Name = clsCommon.myCstr(dt.Rows(0)("Devision_Name"))

            obj.SALARY_STRUCTURE_CODE = clsCommon.myCstr(dt.Rows(0)("SALARY_STRUCTURE_CODE"))
            obj.EMP_SAL_CODE_NEW = clsCommon.myCstr(dt.Rows(0)("EMP_SAL_CODE_NEW"))
            obj.POSTED = clsCommon.myCBool(dt.Rows(0)("POSTED"))
            If clsCommon.myLen(dt.Rows(0)("Posting_Date")) Then
                obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            Else
                obj.Posting_Date = Nothing
            End If
            If clsCommon.myLen(dt.Rows(0)("ARREAR_FROM")) > 0 Then
                obj.ARREAR_FROM = dt.Rows(0)("ARREAR_FROM")
            Else
                obj.ARREAR_FROM = Nothing
            End If

        End If

        qry = "  select INCREMENT_CODE ,TSPL_EMPLOYEE_INCREMENT_DETAIL.PAY_HEAD_CODE,PAY_HEAD_NAME,Increment_Type ,MAX_AMOUNT ,LINE_NO ,PAYHEAD_FORMULA ,RATE_AMOUNT ,TSPL_EMPLOYEE_INCREMENT_DETAIL.IsHiddenComponent ,PAYPERIOD_AMOUNT ,FORMULA_AMT ,IncrementRate_Amt ,IncrementedRate_Amt ,TotalExperience,convert(decimal,IncrementAmt,2) as  IncrementAmt  from TSPL_EMPLOYEE_INCREMENT_DETAIL left join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_EMPLOYEE_INCREMENT_DETAIL.PAY_HEAD_CODE where 2=2"

        qry += " and TSPL_EMPLOYEE_INCREMENT_DETAIL.INCREMENT_CODE = '" + strCode + "' order by LINE_NO"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                objtr = New clsEmpIncrementDetail()

                objtr.Line_No = clsCommon.myCdbl(dr("LINE_NO"))
                objtr.PayHeadCode = clsCommon.myCstr(dr("PAY_HEAD_CODE"))
                objtr.PayHeadName = clsCommon.myCstr(dr("PAY_HEAD_NAME"))
                objtr.Formula = clsCommon.myCstr(dr("PAYHEAD_FORMULA"))
                objtr.Rate_Amount = clsCommon.myCdbl(dr("RATE_AMOUNT"))
                objtr.IsHiddenComponent = clsCommon.myCBool(dr("IsHiddenComponent"))
                objtr.MAX_AMOUNT = clsCommon.myCdbl(dr("MAX_AMOUNT"))
                objtr.PAYPERIOD_AMOUNT = clsCommon.myCdbl(dr("PAYPERIOD_AMOUNT"))
                objtr.Increment_Type = clsCommon.myCstr(dr("Increment_Type"))
                objtr.IncrementAmt = clsCommon.myCdbl(dr("IncrementAmt"))
                objtr.IncrementedRate_Amt = clsCommon.myCdbl(dr("IncrementedRate_Amt"))
                objtr.IncrementRate_Amt = clsCommon.myCdbl(dr("IncrementRate_Amt"))
                objtr.TotalExperience = clsCommon.myCdbl(dr("TotalExperience"))
                ObjList.Add(objtr)
            Next
        End If

        obj.Arr = ObjList
        Return obj
    End Function
    Public Shared Function GetPayHeadCodeString(ByVal SalStructCode As String) As String
        Dim qry As String
        Dim SalStructStr As String

        qry = " select left(fields,len(fields)-1)  from (select ( select '['+PAY_HEAD_CODE   +'],'   " & _
             " from (select distinct TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE   from TSPL_SALSTRUCT_PAYHEADS left join TSPL_PAYHEAD_MASTER on TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_SALSTRUCT_PAYHEADS.PAY_HEAD_CODE where TSPL_SALSTRUCT_PAYHEADS.SALARY_STRUCTURE_CODE='" & SalStructCode & "' " & _
             " and ISEARNING ='1' and TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE <> 'Arrear') xx  FOR XML PATH ('')) Fields ) yy"
        SalStructStr = clsDBFuncationality.getSingleValue(qry)

        Return SalStructStr
    End Function
    Public Shared Function ExportEmployeeSalary(ByVal SalStructCode As String, ByVal frm As RadForm) As String
        Dim qry As String = ""
        Dim strSelect As String
        strSelect = GetPayHeadCodeString(SalStructCode)
        If clsCommon.myLen(strSelect) <= 0 Then
            clsCommon.MyMessageBoxShow("Pay Head Code not found")
            Return qry
        End If

        'qry = "  SELECT EMP_CODE AS [Emp ID],Emp_Name as [Employee Name],SALARY_STRUCTURE_CODE as [Salary Structure Code],REVISION_NO as [REVISION NO],APPLICABLE_FROM as [Applicable Date],EMP_SAL_CODE as [EmpSal],Location_Code as [Location Code],Location_Desc as [Location Name],Devision_Code as [Division Code],Devision_Name as [Division Name]   FROM  (SELECT EMPSAL.INCREMENT_CODE ,INCREMENT_DATE ,EMPSAL.EMP_SAL_CODE ,EMPSAL.REVISION_NO,EMPSAL.SALARY_STRUCTURE_CODE,EMPSAL.APPLICABLE_FROM,EMPSAL.EMP_CODE,EMPSAL.Location_Code,Location_Desc,EMPSAL.Devision_Code,Devision_Name ,POSTED,Posting_Date,EMPSAL.Created_By,EMPSAL.Created_Date,EMPSAL.Modified_By,EMPSAL.Modified_Date,SALPH.IncrementedRate_Amt,SALPH.PAY_HEAD_CODE,TSPL_EMPLOYEE_MASTER. Emp_Name    FROM"
        qry = "  SELECT pvt.*  FROM  (SELECT EMPSAL.EMP_CODE as [EMP CODE] , convert(decimal,(SALPH.IncrementAmt),2) as IncrementAmt, SALPH.PAY_HEAD_CODE,convert(varchar ,APPLICABLE_FROM,103)as [APPLICABLE FROM],convert(varchar ,ARREAR_FROM,103)as [Arrear From]  FROM"
        'qry += " (SELECT TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE ,INCREMENT_DATE ,EMP_SAL_CODE ,TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO,TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE,TSPL_EMPLOYEE_INCREMENT_HEAD.APPLICABLE_FROM,TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE,TSPL_EMPLOYEE_INCREMENT_HEAD.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_EMPLOYEE_INCREMENT_HEAD.Devision_Code,TSPL_DEVISION_MASTER.Devision_Name ,POSTED,Posting_Date,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_Date,TSPL_EMPLOYEE_INCREMENT_HEAD.Modified_By,TSPL_EMPLOYEE_INCREMENT_HEAD.Modified_Date  FROM TSPL_EMPLOYEE_INCREMENT_HEAD left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_INCREMENT_HEAD.location_Code left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_EMPLOYEE_INCREMENT_HEAD.DEVISION_CODE  INNER JOIN (SELECT EMP_CODE, MAX(REVISION_NO) AS REVISION_NO  FROM TSPL_EMPLOYEE_INCREMENT_HEAD GROUP BY  EMP_CODE) AS SAL ON TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE=SAL.EMP_CODE  AND TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO=SAL.REVISION_NO)AS EMPSAL INNER JOIN TSPL_EMPLOYEE_INCREMENT_DETAIL SALPH  ON  EMPSAL.INCREMENT_CODE=SALPH.INCREMENT_CODE inner join TSPL_EMPLOYEE_MASTER on EMPSAL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE)p Pivot "
        qry += " (SELECT TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE ,TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE ,APPLICABLE_FROM,ARREAR_FROM  FROM TSPL_EMPLOYEE_INCREMENT_HEAD left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_INCREMENT_HEAD.location_Code left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_EMPLOYEE_INCREMENT_HEAD.DEVISION_CODE  INNER JOIN (SELECT EMP_CODE, MAX(REVISION_NO) AS REVISION_NO  FROM TSPL_EMPLOYEE_INCREMENT_HEAD GROUP BY  EMP_CODE) AS SAL ON TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE=SAL.EMP_CODE  AND TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO=SAL.REVISION_NO)AS EMPSAL INNER JOIN TSPL_EMPLOYEE_INCREMENT_DETAIL SALPH  ON  EMPSAL.INCREMENT_CODE=SALPH.INCREMENT_CODE inner join TSPL_EMPLOYEE_MASTER on EMPSAL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE)p Pivot "
        qry += " ( " & _
               " MAX(IncrementAmt) " & _
               " FOR [PAY_HEAD_CODE] IN " & _
               " ( " & strSelect & " ) " & _
               " ) AS pvt "
        transportSql.ExporttoExcelForPivot (qry, frm, strSelect)
        Return qry
    End Function
    Public Shared Function ExportEmployeeIncrementedSalary(ByVal SalStructCode As String, ByVal frm As RadForm) As String
        Dim qry As String = ""
        Dim strSelect As String
        strSelect = GetPayHeadCodeString(SalStructCode)
        If clsCommon.myLen(strSelect) <= 0 Then
            clsCommon.MyMessageBoxShow("Pay Head Code not found")
            Return qry
        End If

        'qry = "  SELECT EMP_CODE AS [Emp ID],Emp_Name as [Employee Name],SALARY_STRUCTURE_CODE as [Salary Structure Code],REVISION_NO as [REVISION NO],APPLICABLE_FROM as [Applicable Date],EMP_SAL_CODE as [EmpSal],Location_Code as [Location Code],Location_Desc as [Location Name],Devision_Code as [Division Code],Devision_Name as [Division Name]   FROM  (SELECT EMPSAL.INCREMENT_CODE ,INCREMENT_DATE ,EMPSAL.EMP_SAL_CODE ,EMPSAL.REVISION_NO,EMPSAL.SALARY_STRUCTURE_CODE,EMPSAL.APPLICABLE_FROM,EMPSAL.EMP_CODE,EMPSAL.Location_Code,Location_Desc,EMPSAL.Devision_Code,Devision_Name ,POSTED,Posting_Date,EMPSAL.Created_By,EMPSAL.Created_Date,EMPSAL.Modified_By,EMPSAL.Modified_Date,SALPH.IncrementedRate_Amt,SALPH.PAY_HEAD_CODE,TSPL_EMPLOYEE_MASTER. Emp_Name    FROM"
        qry = "  SELECT pvt.*  FROM  (SELECT EMPSAL.EMP_CODE as [EMP CODE] , convert(decimal,(SALPH.IncrementedRate_Amt),2) as IncrementedRate_Amt, SALPH.PAY_HEAD_CODE,convert(varchar ,APPLICABLE_FROM,103)as [APPLICABLE FROM],convert(varchar ,ARREAR_FROM,103)as [Arrear From]  FROM"
        'qry += " (SELECT TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE ,INCREMENT_DATE ,EMP_SAL_CODE ,TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO,TSPL_EMPLOYEE_INCREMENT_HEAD.SALARY_STRUCTURE_CODE,TSPL_EMPLOYEE_INCREMENT_HEAD.APPLICABLE_FROM,TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE,TSPL_EMPLOYEE_INCREMENT_HEAD.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_EMPLOYEE_INCREMENT_HEAD.Devision_Code,TSPL_DEVISION_MASTER.Devision_Name ,POSTED,Posting_Date,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_By,TSPL_EMPLOYEE_INCREMENT_HEAD.Created_Date,TSPL_EMPLOYEE_INCREMENT_HEAD.Modified_By,TSPL_EMPLOYEE_INCREMENT_HEAD.Modified_Date  FROM TSPL_EMPLOYEE_INCREMENT_HEAD left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_INCREMENT_HEAD.location_Code left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_EMPLOYEE_INCREMENT_HEAD.DEVISION_CODE  INNER JOIN (SELECT EMP_CODE, MAX(REVISION_NO) AS REVISION_NO  FROM TSPL_EMPLOYEE_INCREMENT_HEAD GROUP BY  EMP_CODE) AS SAL ON TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE=SAL.EMP_CODE  AND TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO=SAL.REVISION_NO)AS EMPSAL INNER JOIN TSPL_EMPLOYEE_INCREMENT_DETAIL SALPH  ON  EMPSAL.INCREMENT_CODE=SALPH.INCREMENT_CODE inner join TSPL_EMPLOYEE_MASTER on EMPSAL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE)p Pivot "
        qry += " (SELECT TSPL_EMPLOYEE_INCREMENT_HEAD.INCREMENT_CODE ,TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE ,APPLICABLE_FROM,ARREAR_FROM  FROM TSPL_EMPLOYEE_INCREMENT_HEAD left join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER.Location_Code =TSPL_EMPLOYEE_INCREMENT_HEAD.location_Code left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_EMPLOYEE_INCREMENT_HEAD.DEVISION_CODE  INNER JOIN (SELECT EMP_CODE, MAX(REVISION_NO) AS REVISION_NO  FROM TSPL_EMPLOYEE_INCREMENT_HEAD GROUP BY  EMP_CODE) AS SAL ON TSPL_EMPLOYEE_INCREMENT_HEAD.EMP_CODE=SAL.EMP_CODE  AND TSPL_EMPLOYEE_INCREMENT_HEAD.REVISION_NO=SAL.REVISION_NO)AS EMPSAL INNER JOIN TSPL_EMPLOYEE_INCREMENT_DETAIL SALPH  ON  EMPSAL.INCREMENT_CODE=SALPH.INCREMENT_CODE inner join TSPL_EMPLOYEE_MASTER on EMPSAL.EMP_CODE=TSPL_EMPLOYEE_MASTER.EMP_CODE)p Pivot "
        qry += " ( " & _
               " MAX(IncrementedRate_Amt) " & _
               " FOR [PAY_HEAD_CODE] IN " & _
               " ( " & strSelect & " ) " & _
               " ) AS pvt "
        transportSql.ExporttoExcelForPivot(qry, frm, strSelect)
        Return qry
    End Function

    Public Shared Function getPayHeadAmount(ByVal EmpCode As String, ByVal SalStructCode As String, ByVal PayHeadCode As String, ByVal applicableFrom As Date, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        Dim qry As String = "select emp.EMP_CODE,convert(decimal,IncrementedRate_Amt,2) as  IncrementedRate_Amt  " & _
        "  from (" & _
        " select EMP_CODE,MAX(INCREMENT_CODE) AS INCREMENT_CODE,MAX(REVISION_NO) AS REVISION_NO,max(EMP_SAL_CODE) as EMP_SAL_CODE ,Location_Code,max(Location_Desc) as Location_Desc,Devision_Code,max(Devision_Name) as Devision_Name " & _
        " from TSPL_EMPLOYEE_INCREMENT_HEAD WHERE  APPLICABLE_FROM<='" & clsCommon.GetPrintDate(applicableFrom, "dd/MMM/yyyy") & "' and SALARY_STRUCTURE_CODE='" & SalStructCode & "' GROUP BY EMP_CODE,Location_Code,Devision_Code " & _
        " HAVING MAX(APPLICABLE_FROM) <= '" & clsCommon.GetPrintDate(applicableFrom, "dd/MMM/yyyy") & "') as emp  inner join TSPL_EMPLOYEE_INCREMENT_DETAIL  " & _
        " on emp.INCREMENT_CODE=TSPL_EMPLOYEE_INCREMENT_DETAIL.INCREMENT_CODE where EMP_CODE='" & EmpCode & "' and PAY_HEAD_CODE='" & PayHeadCode & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0).Item("IncrementedRate_Amt")
        Else
            Return 0
        End If
    End Function
End Class
Public Class clsEmpIncrementDetail
#Region "Variables"
    '' grid columns
    Public Line_No As Integer
    Public PayHeadCode As String
    Public EMP_SAL_CODE As String
    Public PayHeadName As String
    Public Formula As String
    Public Rate_Amount As Decimal
    Public IsHiddenComponent As Boolean
    Public MAX_AMOUNT As Decimal = 0
    Public PAYPERIOD_AMOUNT As Decimal = 0
    Public Increment_Type As String
    Public IncrementRate_Amt As Decimal = 0
    Public IncrementedRate_Amt As Decimal = 0
    Public TotalExperience As String
    Public IncrementAmt As Decimal = 0
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsEmpIncrementDetail), ByVal trans As SqlTransaction) As Boolean


        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsEmpIncrementDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "LINE_NO", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "INCREMENT_CODE", strDocNo)
                'clsCommon.AddColumnsForChange(coll, "EMP_SAL_CODE", obj.EMP_SAL_CODE)
                clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", obj.PayHeadCode)
                clsCommon.AddColumnsForChange(coll, "PAYHEAD_FORMULA", obj.Formula)
                clsCommon.AddColumnsForChange(coll, "RATE_AMOUNT", obj.Rate_Amount)
                clsCommon.AddColumnsForChange(coll, "IsHiddenComponent", obj.IsHiddenComponent)
                clsCommon.AddColumnsForChange(coll, "MAX_AMOUNT", obj.MAX_AMOUNT)
                clsCommon.AddColumnsForChange(coll, "PAYPERIOD_AMOUNT", obj.PAYPERIOD_AMOUNT, True)
                clsCommon.AddColumnsForChange(coll, "IncrementRate_Amt", obj.IncrementRate_Amt, True)
                clsCommon.AddColumnsForChange(coll, "IncrementAmt", obj.IncrementAmt, True)
                clsCommon.AddColumnsForChange(coll, "IncrementedRate_Amt", obj.IncrementedRate_Amt, True)
                clsCommon.AddColumnsForChange(coll, "TotalExperience", obj.TotalExperience)
                clsCommon.AddColumnsForChange(coll, "Increment_Type", obj.Increment_Type)
                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))

                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_EMPLOYEE_INCREMENT_DETAIL", OMInsertOrUpdate.Insert, "TSPL_EMPLOYEE_INCREMENT_DETAIL.INCREMENT_CODE='" + strDocNo + "'", trans)
            Next

        End If

        Return True
    End Function
End Class