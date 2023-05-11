Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class clsLTAClaim
#Region "Variables Declaration"
    Public LTA_Code As String
    Public Emp_Code As String
    Public Emp_Name As String
    Public Date_Of_Joining As String
    Public Dept_Code As String
    Public Dept_Name As String
    Public Designation_Code As String
    Public Designation_Desc As String
    Public from_Date As Date
    Public to_Date As Date
    Public Claim_Amount As Double
    Public Posted As Decimal
    Public Posting_Date As Date
    Public objLTADetails As List(Of clsLTAClaimDetail)
#End Region

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsLTAClaim
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsLTAClaim
        Dim objLTAClaim As New clsLTAClaim()

        Dim qry As String = "select TSPL_LTA_Claim_Head.LTA_CODE,TSPL_LTA_Claim_Head.EMP_CODE, TSPL_LTA_Claim_Head.Claim_From_Date, TSPL_LTA_Claim_Head.Claim_To_Date, TSPL_LTA_Claim_Head.Posted,TSPL_LTA_Claim_Head.Posting_Date, " & _
                            "TSPL_LTA_Claim_Head.Claim_Amount, TSPL_EMPLOYEE_MASTER.Emp_Name,Joining_date, TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE,TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME, TSPL_EMPLOYEE_MASTER.Designation,  " & _
                            "TSPL_DESIGNATION_MASTER.Designation_Desc " & _
                            "from TSPL_LTA_Claim_Head " & _
                            "LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_LTA_Claim_Head.EMP_CODE " & _
                            "LEFT OUTER JOIN TSPL_DEPARTMENT_MASTER ON TSPL_DEPARTMENT_MASTER.DEPARTMENT_CODE = TSPL_EMPLOYEE_MASTER.DEPARTMENT_CODE " & _
                            "LEFT OUTER JOIN TSPL_DESIGNATION_MASTER ON TSPL_DESIGNATION_MASTER.Designation_id = TSPL_EMPLOYEE_MASTER.Designation " & _
                            "where 1=1 "

        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_LTA_Claim_Head.LTA_CODE = (select MIN(LTA_CODE) from TSPL_LTA_Claim_Head)"
            Case NavigatorType.Last
                qry += " and TSPL_LTA_Claim_Head.LTA_CODE = (select Max(LTA_CODE) from TSPL_LTA_Claim_Head)"
            Case NavigatorType.Next
                qry += " and TSPL_LTA_Claim_Head.LTA_CODE = (select Min(LTA_CODE) from TSPL_LTA_Claim_Head where  LTA_CODE>'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_LTA_Claim_Head.LTA_CODE = (select Max(LTA_CODE) from TSPL_LTA_Claim_Head where LTA_CODE<'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_LTA_Claim_Head.LTA_CODE = '" + strCode + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            objLTAClaim.LTA_Code = clsCommon.myCstr(dt.Rows(0)("LTA_Code"))
            objLTAClaim.Emp_Code = clsCommon.myCstr(dt.Rows(0)("EMP_Code"))
            objLTAClaim.Emp_Name = clsCommon.myCstr(dt.Rows(0)("EMP_Name"))
            objLTAClaim.from_Date = clsCommon.myCDate(dt.Rows(0)("Claim_From_Date"))
            objLTAClaim.to_Date = clsCommon.myCDate(dt.Rows(0)("Claim_To_Date"))
            objLTAClaim.Dept_Code = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_CODE"))
            objLTAClaim.Dept_Name = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT_NAME"))
            objLTAClaim.Designation_Code = clsCommon.myCstr(dt.Rows(0)("Designation"))
            objLTAClaim.Designation_Desc = clsCommon.myCstr(dt.Rows(0)("Designation_Desc"))
            objLTAClaim.Date_Of_Joining = clsCommon.myCstr(dt.Rows(0)("Joining_date"))
            objLTAClaim.Claim_Amount = clsCommon.myCdbl(dt.Rows(0)("Claim_Amount"))
            objLTAClaim.Posted = clsCommon.myCdbl(dt.Rows(0)("Posted"))
            If (clsCommon.myLen(dt.Rows(0)("Posting_Date")) > 0) Then
                objLTAClaim.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            End If

        End If

        qry = "select Attended_Year, Attended_Month, Attended_Days, Basic_Salary from TSPL_LTA_Claim_Detail where LTA_Code='" + objLTAClaim.LTA_Code + "' "
        dt = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            objLTAClaim.objLTADetails = New List(Of clsLTAClaimDetail)
            For Each dr As DataRow In dt.Rows
                Dim objtr As New clsLTAClaimDetail
                objtr.Attended_Year = clsCommon.myCdbl(dr("Attended_Year"))
                objtr.Attended_Month = clsCommon.myCstr(dr("Attended_Month"))
                objtr.Attended_Days = clsCommon.myCdbl(dr("Attended_Days"))
                objtr.Basic_Salary = clsCommon.myCdbl(dr("Basic_Salary"))

                objLTAClaim.objLTADetails.Add(objtr)

            Next
        End If

        Return objLTAClaim

    End Function


    Public Function SaveData(ByVal objLTAClaim As clsLTAClaim, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If isNewEntry Then
                objLTAClaim.LTA_Code = clsERPFuncationality.GetNextCode(trans, objLTAClaim.from_Date, clsDocType.LTAClaim, "", "")
            End If

            If (clsCommon.myLen(objLTAClaim.LTA_Code)) <= 0 Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Emp_Code", objLTAClaim.Emp_Code)
            clsCommon.AddColumnsForChange(coll, "Claim_From_Date", Format(objLTAClaim.from_Date, "dd MMM yyyy"))
            clsCommon.AddColumnsForChange(coll, "Claim_To_Date", Format(objLTAClaim.to_Date, "dd MMM yyyy"))
            clsCommon.AddColumnsForChange(coll, "Claim_Amount", objLTAClaim.Claim_Amount)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            If isNewEntry Then

                clsCommon.AddColumnsForChange(coll, "LTA_Code", objLTAClaim.LTA_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LTA_Claim_Head", OMInsertOrUpdate.Insert, "", trans)
            Else

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LTA_Claim_Head", OMInsertOrUpdate.Update, "TSPL_LTA_Claim_Head.LTA_Code='" + objLTAClaim.LTA_Code + "'", trans)
            End If

            isSaved = isSaved And clsLTAClaimDetail.SaveData(objLTAClaim.LTA_Code, objLTAClaim.objLTADetails, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return isSaved
    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsLTAClaim = clsLTAClaim.GetData(strDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.LTA_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim qry As String = "Update TSPL_LTA_Claim_Head set POSTED=1, Posting_Date='" + strPostDate + "', Modified_Date='" + strPostDate + "', Modified_By='" + objCommonVar.CurrentUserCode + "' where LTA_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = "delete from TSPL_LTA_Claim_Detail where LTA_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "delete from TSPL_LTA_Claim_Head where LTA_Code ='" + strCode + "'"
            isSaved = isSaved And clsDBFuncationality.ExecuteNonQuery(qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function CheckPayHead(ByVal EMPCode As String, ByVal SalStruCode As String, Optional ByVal SalDate As Date = Nothing) As Boolean

        If SalDate = Nothing Then
            SalDate = clsCommon.GETSERVERDATE()
        Else
            SalDate = SalDate
        End If

        Dim qry As String = "Select TOP 1 A.EMP_SAL_CODE ,A.APPLICABLE_FROM AS DATE,TSPL_EMPLOYEE_SALARY_PAYHEADS.RATE_AMOUNT ,TSPL_EMPLOYEE_SALARY_PAYHEADS.SALARY_STRUCTURE_CODE ,TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE ,TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE,A.EMP_CODE From TSPL_EMPLOYEE_SALARY_PAYHEADS INNER JOIN (SELECT TSPL_EMPLOYEE_SALARY.EMP_SAL_CODE,TSPL_EMPLOYEE_SALARY.APPLICABLE_FROM,TSPL_EMPLOYEE_SALARY.EMP_CODE  FROM TSPL_EMPLOYEE_SALARY ) A ON A.EMP_SAL_CODE = TSPL_EMPLOYEE_SALARY_PAYHEADS.EMP_SAL_CODE INNER JOIN TSPL_PAYHEAD_MASTER ON TSPL_PAYHEAD_MASTER.PAY_HEAD_CODE =TSPL_EMPLOYEE_SALARY_PAYHEADS.PAY_HEAD_CODE WHERE TSPL_PAYHEAD_MASTER.SUB_HEAD_TYPE ='" + SalStruCode + "' AND A.APPLICABLE_FROM <='" + clsCommon.GetPrintDate(SalDate, "dd/MMM/yyyy") + "' AND A.EMP_CODE ='" + EMPCode + "' ORDER BY A.APPLICABLE_FROM DESC"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt Is Nothing Or dt.Rows.Count = 0) Then
            common.clsCommon.MyMessageBoxShow("Please map a Pay Head of  Sub Head Type-" + SalStruCode + " with salary structure of employee- " & EMPCode & "")
            Return False
        End If
        Return True
    End Function
End Class

Public Class clsLTAClaimDetail
#Region "Variables Declaration"
    Public LTA_Code As String
    Public Attended_Year As Integer
    Public Attended_Month As String
    Public Attended_Days As Integer
    Public Basic_Salary As Double
#End Region
    Public Shared Function SaveData(ByVal LTACode As String, ByVal Arr As List(Of clsLTAClaimDetail), ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = "delete from TSPL_LTA_Claim_Detail where LTA_Code='" + LTACode + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                For Each obj As clsLTAClaimDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "LTA_Code", LTACode)
                    clsCommon.AddColumnsForChange(coll, "Attended_Year", obj.Attended_Year)
                    clsCommon.AddColumnsForChange(coll, "Attended_Month", obj.Attended_Month)
                    clsCommon.AddColumnsForChange(coll, "Attended_Days", obj.Attended_Days)
                    clsCommon.AddColumnsForChange(coll, "Basic_Salary", obj.Basic_Salary)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_LTA_Claim_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next

            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try


    End Function
End Class
