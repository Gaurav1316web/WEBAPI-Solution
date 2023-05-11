Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsResignationLetter
    '==================Created By Preeti gupta============
#Region "Variables"

    Public DOC_CODE As String
    Public DOC_DATE As DateTime
    Public EmpolyeeCode As String
    Public EmpolyeeName As String
    Public DepartmentCode As String
    Public DepartmentName As String
    Public ResonOfResignation As String
    Public ResonOFResignation_DATE As DateTime
    Public Remarks As String
    Public ResponsibilityCode As String
    Public ResponsibilityName As String
    Public NoticePeriod As Integer = 0
    Public CREATED_BY As String
    Public Status As String
    Public Approved_by As String
    Public Approved_Date As DateTime
    Public Posting_Date As Date
    Public Shared trans As SqlTransaction = Nothing
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending

#End Region

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = "delete from Tspl_HR_EM_Resignation_Letter where Doc_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_EMPLOYEE_MASTER SET NOTICE_PERIOD_IN_DAYS = 0,RESIGNATION_SUBMIT_DATE =NULL,LEAVING_REASON ='' WHERE EMP_CODE='" & strCode & "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""

        Dim qry As String = " select Tspl_HR_EM_Resignation_Letter.Doc_code as Code,convert(varchar,Tspl_HR_EM_Resignation_Letter.Doc_Date,103) as [Document Date],Tspl_HR_EM_Resignation_Letter.EMP_CODE  as [Employee Code],Tspl_HR_EM_Resignation_Letter.DEPARTMENT_CODE as [Department Code],Tspl_HR_EM_Resignation_Letter.Reson_Of_Resignation  as [Reson Of Resignation],convert(varchar,Tspl_HR_EM_Resignation_Letter.Resignation_Date,103)  as [Resignation Date],Tspl_HR_EM_Resignation_Letter.Remarks ,Tspl_HR_EM_Resignation_Letter.responsibility_emp_code as [Handover Responsibility To],NoticePeriod As [Notice Period(In Days)] from Tspl_HR_EM_Resignation_Letter "
        str = clsCommon.ShowSelectForm("HREXResignationLetter", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function savedata(ByVal ResignationLetter As clsResignationLetter, ByVal isnewentry As Boolean)
        trans = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsResignationLetter = New clsResignationLetter


            Dim coll As New Hashtable()
            If isnewentry Then
                ResignationLetter.DOC_CODE = clsERPFuncationality.GetNextCode(trans, ResignationLetter.DOC_DATE, clsDocType.HREMResignationLetter, "", "")
            End If
            'clsCommon.AddColumnsForChange(coll, "Requisition_Code", requisition.code)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(ResignationLetter.DOC_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", ResignationLetter.EmpolyeeCode, True)

            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", ResignationLetter.DepartmentCode, True)
            clsCommon.AddColumnsForChange(coll, "Reson_Of_Resignation", ResignationLetter.ResonOfResignation)
            clsCommon.AddColumnsForChange(coll, "Resignation_Date", clsCommon.GetPrintDate(ResignationLetter.ResonOFResignation_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Remarks", ResignationLetter.Remarks, True)
            clsCommon.AddColumnsForChange(coll, "responsibility_emp_code", ResignationLetter.ResponsibilityCode, True)
            'clsCommon.AddColumnsForChange(coll, "Status", ResignationLetter.ResponsibilityCode, True)
            'clsCommon.AddColumnsForChange(coll, "approved_by", ResignationLetter.Approved_by)
            'clsCommon.AddColumnsForChange(coll, "approved_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "NoticePeriod", ResignationLetter.NoticePeriod, True)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isnewentry Then

                clsCommon.AddColumnsForChange(coll, "Doc_Code", ResignationLetter.DOC_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Resignation_Letter", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Resignation_Letter", OMInsertOrUpdate.Update, " DOC_CODE='" + ResignationLetter.DOC_CODE + "'", trans)
            End If
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_EMPLOYEE_MASTER SET NOTICE_PERIOD_IN_DAYS =" & ResignationLetter.NoticePeriod & ",RESIGNATION_SUBMIT_DATE ='" & clsCommon.GetPrintDate(ResignationLetter.ResonOFResignation_DATE, "dd/MMM/yyyy") & "',LEAVING_REASON ='" & ResignationLetter.ResonOfResignation & "' WHERE EMP_CODE='" & ResignationLetter.EmpolyeeCode & "'", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getdata(ByVal code As String, ByVal navigatortype As NavigatorType) As clsResignationLetter
        Try
            Dim obj As clsResignationLetter = Nothing
            Dim qst As String = (" select Tspl_HR_EM_Resignation_Letter.Doc_code as Code,Tspl_HR_EM_Resignation_Letter.status,convert(varchar,Tspl_HR_EM_Resignation_Letter.Doc_Date,103) as [Document Date],Tspl_HR_EM_Resignation_Letter.EMP_CODE  as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],Tspl_HR_EM_Resignation_Letter.DEPARTMENT_CODE as [Department Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name],Tspl_HR_EM_Resignation_Letter.Reson_Of_Resignation  as [Reson Of Resignation],convert(varchar,Tspl_HR_EM_Resignation_Letter.Resignation_Date,103)  as [Resignation Date],Tspl_HR_EM_Resignation_Letter.Remarks,Tspl_HR_EM_Resignation_Letter.Responsibility_EMP_CODE as [Responsibility Code],TSPL_EMPLOYEE_MASTER_for_Responsibility.Emp_Name as [Responsibility Name] ,Tspl_HR_EM_Resignation_Letter.NoticePeriod  from Tspl_HR_EM_Resignation_Letter   left outer join TSPL_EMPLOYEE_MASTER  on TSPL_EMPLOYEE_MASTER .EMP_CODE =Tspl_HR_EM_Resignation_Letter.EMP_CODE  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Resignation_Letter.DEPARTMENT_CODE left outer join TSPL_EMPLOYEE_MASTER as TSPL_EMPLOYEE_MASTER_for_Responsibility on TSPL_EMPLOYEE_MASTER_for_Responsibility.EMP_CODE =Tspl_HR_EM_Resignation_Letter.Responsibility_EMP_CODE  where 2=2")
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and Tspl_HR_EM_Resignation_Letter.Doc_code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and Tspl_HR_EM_Resignation_Letter.Doc_code in (select  min(Doc_code)from Tspl_HR_EM_Resignation_Letter  where Tspl_HR_EM_Resignation_Letter.Doc_code  >'" + code + "')"
                Case navigatortype.First
                    qst += "and Tspl_HR_EM_Resignation_Letter.Doc_code in (select MIN(Doc_code)from Tspl_HR_EM_Resignation_Letter )"

                Case navigatortype.Last
                    qst += "and Tspl_HR_EM_Resignation_Letter.Doc_code in (select Max(Doc_code)from Tspl_HR_EM_Resignation_Letter )"
                Case navigatortype.Previous
                    qst += "and Tspl_HR_EM_Resignation_Letter.Doc_code in (select  max(Doc_code)from Tspl_HR_EM_Resignation_Letter  where Tspl_HR_EM_Resignation_Letter.Doc_code <'" + code + "')"
            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsResignationLetter
                obj.DOC_CODE = clsCommon.myCstr(dt1.Rows(0)("Code"))
                obj.DOC_DATE = clsCommon.myCDate(dt1.Rows(0)("Document Date"))
                obj.EmpolyeeCode = clsCommon.myCstr(dt1.Rows(0)("Employee Code"))
                obj.EmpolyeeName = clsCommon.myCstr(dt1.Rows(0)("Employee Name"))
                obj.DepartmentCode = clsCommon.myCstr(dt1.Rows(0)("Department Code"))
                obj.DepartmentName = clsCommon.myCstr(dt1.Rows(0)("Department Name"))
                obj.ResonOfResignation = clsCommon.myCstr(dt1.Rows(0)("Reson Of Resignation"))
                obj.ResonOFResignation_DATE = clsCommon.myCDate(dt1.Rows(0)("Resignation Date"))
                obj.Remarks = clsCommon.myCstr(dt1.Rows(0)("Remarks"))
                obj.ResponsibilityCode = clsCommon.myCstr(dt1.Rows(0)("Responsibility Code"))
                obj.ResponsibilityName = clsCommon.myCstr(dt1.Rows(0)("Responsibility Name"))
                obj.Status = clsCommon.myCstr(dt1.Rows(0)("Status"))
                obj.NoticePeriod = clsCommon.myCdbl(dt1.Rows(0)("NoticePeriod"))
            End If
            

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class
