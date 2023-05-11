
Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class clsHREXTerminationLetter
    '==================Created By Preeti gupta============
#Region "Variables"

    Public DOC_CODE As String
    Public DOC_DATE As DateTime
    Public EmpolyeeCode As String
    Public EmpolyeeName As String
    Public DepartmentCode As String
    Public DepartmentName As String
    Public ResonOfTermination As String
    Public ResonOfTermination_DATE As DateTime
    Public Remarks As String
    Public ResponsibilityCode As String
    Public ResponsibilityName As String
    Public CREATED_BY As String
    Public TerminationType As String
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
            qry = "delete from Tspl_HR_EM_Termination_Letter where Doc_code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""

        Dim qry As String = " select Tspl_HR_EM_Termination_Letter.Doc_code as Code,convert(varchar,Tspl_HR_EM_Termination_Letter.Doc_Date,103) as [Document Date],Tspl_HR_EM_Termination_Letter.warning_type as[Warning Type],Tspl_HR_EM_Termination_Letter.EMP_CODE  as [Employee Code],Tspl_HR_EM_Termination_Letter.DEPARTMENT_CODE as [Department Code],Tspl_HR_EM_Termination_Letter.Reson_Of_Termination  as [Reson Of Termination],convert(varchar,Tspl_HR_EM_Termination_Letter.Termination_Date,103)  as [Resignation Date],Tspl_HR_EM_Termination_Letter.Remarks from Tspl_HR_EM_Termination_Letter "
        str = clsCommon.ShowSelectForm("HREXTerminationLetter", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function savedata(ByVal TerminationLetter As clsHREXTerminationLetter, ByVal isnewentry As Boolean)
        trans = clsDBFuncationality.GetTransactin()
        Try

            Dim obj As clsHREXTerminationLetter = New clsHREXTerminationLetter


            Dim coll As New Hashtable()
            If isnewentry Then
                TerminationLetter.DOC_CODE = clsERPFuncationality.GetNextCode(trans, TerminationLetter.DOC_DATE, clsDocType.HREMTerminationLetter, "", "")
            End If
            'clsCommon.AddColumnsForChange(coll, "Requisition_Code", requisition.code)
            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(TerminationLetter.DOC_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", TerminationLetter.EmpolyeeCode, True)
            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", TerminationLetter.DepartmentCode, True)
            clsCommon.AddColumnsForChange(coll, "Reson_Of_Termination", TerminationLetter.ResonOfTermination)
            clsCommon.AddColumnsForChange(coll, "Termination_Date", clsCommon.GetPrintDate(TerminationLetter.ResonOfTermination_DATE, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Remarks", TerminationLetter.Remarks)
            clsCommon.AddColumnsForChange(coll, "warning_type", TerminationLetter.TerminationType)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            If isnewentry Then

                clsCommon.AddColumnsForChange(coll, "Doc_Code", TerminationLetter.DOC_CODE)
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Termination_Letter", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_HR_EM_Termination_Letter", OMInsertOrUpdate.Update, " DOC_CODE='" + TerminationLetter.DOC_CODE + "'", trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getdata(ByVal code As String, ByVal navigatortype As NavigatorType) As clsHREXTerminationLetter
        Try
            Dim obj As clsHREXTerminationLetter = Nothing
            Dim qst As String = (" select Tspl_HR_EM_Termination_Letter.Doc_code as Code,Tspl_HR_EM_Termination_Letter.warning_type ,convert(varchar,Tspl_HR_EM_Termination_Letter.Doc_Date,103) as [Document Date],Tspl_HR_EM_Termination_Letter.EMP_CODE  as [Employee Code],TSPL_EMPLOYEE_MASTER.Emp_Name as [Employee Name],Tspl_HR_EM_Termination_Letter.DEPARTMENT_CODE as [Department Code],TSPL_DEPARTMENT_MASTER.DEPARTMENT_NAME as [Department Name],Tspl_HR_EM_Termination_Letter.Reson_Of_Termination  as [Reson Of Termination],convert(varchar,Tspl_HR_EM_Termination_Letter.Termination_Date,103)  as [Termination Date],Tspl_HR_EM_Termination_Letter.Remarks    from Tspl_HR_EM_Termination_Letter left outer join TSPL_EMPLOYEE_MASTER  on TSPL_EMPLOYEE_MASTER .EMP_CODE =Tspl_HR_EM_Termination_Letter.EMP_CODE  left outer join TSPL_DEPARTMENT_MASTER on TSPL_DEPARTMENT_MASTER .DEPARTMENT_CODE =Tspl_HR_EM_Termination_Letter.DEPARTMENT_CODE where 2=2")
            Select Case navigatortype
                    Case navigatortype.Current
                        qst += "and Tspl_HR_EM_Termination_Letter.Doc_code in ('" + code + "')"
                    Case navigatortype.Next
                        qst += "and Tspl_HR_EM_Termination_Letter.Doc_code in (select  min(Doc_code)from Tspl_HR_EM_Termination_Letter  where Tspl_HR_EM_Termination_Letter.Doc_code  >'" + code + "')"
                    Case navigatortype.First
                        qst += "and Tspl_HR_EM_Termination_Letter.Doc_code in (select MIN(Doc_code)from Tspl_HR_EM_Termination_Letter )"

                    Case navigatortype.Last
                        qst += "and Tspl_HR_EM_Termination_Letter.Doc_code in (select Max(Doc_code)from Tspl_HR_EM_Termination_Letter )"
                    Case navigatortype.Previous
                        qst += "and Tspl_HR_EM_Termination_Letter.Doc_code in (select  max(Doc_code)from Tspl_HR_EM_Termination_Letter  where Tspl_HR_EM_Termination_Letter.Doc_code <'" + code + "')"
                End Select
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    obj = New clsHREXTerminationLetter
                    obj.DOC_CODE = clsCommon.myCstr(dt1.Rows(0)("Code"))
                    obj.DOC_DATE = clsCommon.myCDate(dt1.Rows(0)("Document Date"))
                    obj.EmpolyeeCode = clsCommon.myCstr(dt1.Rows(0)("Employee Code"))
                    obj.EmpolyeeName = clsCommon.myCstr(dt1.Rows(0)("Employee Name"))
                    obj.DepartmentCode = clsCommon.myCstr(dt1.Rows(0)("Department Code"))
                    obj.DepartmentName = clsCommon.myCstr(dt1.Rows(0)("Department Name"))
                    obj.ResonOfTermination = clsCommon.myCstr(dt1.Rows(0)("Reson Of Termination"))
                    obj.ResonOfTermination_DATE = clsCommon.myCDate(dt1.Rows(0)("Termination Date"))
                    obj.Remarks = clsCommon.myCstr(dt1.Rows(0)("Remarks"))
                    obj.TerminationType = clsCommon.myCstr(dt1.Rows(0)("warning_type"))

                End If


                Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

End Class

