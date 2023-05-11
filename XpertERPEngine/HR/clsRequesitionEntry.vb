Imports common
Imports System.Data.SqlClient

Public Class clsrequisitionentry
#Region "Variables"
    Public HR_REQUISITION_CODE As String = ""
    Public code As String = Nothing
    Public description As String = Nothing
    Public initiated_by As String = Nothing
    Public profile As String = Nothing
    Public recommended_by As String
    Public QualificationCode As String
    Public Department As String
    Public location As String
    Public sub_location As String
    Public designation As String
    Public jobtitle As String
    Public no_of_post As Integer
    Public Emp_type As String
    Public ctc_range As String
    Public Hiring_type As String
    Public Qualification As String
    Public minimumexperience As Integer
    Public maximumexperience As Integer
    Public gender As String
    Public age_yr As Integer
    Public age_month As Integer
    Public Req_Date As DateTime? = Nothing
    Public MinExpMonth As Integer
    Public MaxExpMonth As Integer
    Public Shared trans As SqlTransaction = Nothing
    Public arr As List(Of clsrequisitionentryDeatils) = Nothing
    Public arrqul As New ArrayList
    Public ClosedStatus As String
    Public Approved_By As String
    Public ApprovedStatus As String
    Public Industry_Code As String = Nothing
    Public Vertical_Code As String = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
      
        Dim qry As String = " SELECT Requisition_Code As Code,Requisition_Description AS [Requisition Description],convert(varchar,Date,103) As Date,Emp_Type AS [Emp Type],Profile_Code As [Profile Code] ,Initiated_By As [Initiated By],Recommended_By AS [Recommended By],DEPARTMENT_CODE AS [Department Code],Location_Code As [Location Code],Job_Title_Code As [Job Title Code],Industry_Code AS [Industry Code],Vertical_Code As [Vertical Code],NoOfPost AS [No of Post],CTCRange AS [CTC Range],Hiring_Type AS [Hiring Type],MinExpMonth AS [Min Exp Month],MaxExpMonth As [Max Exp Month],Gender,AgeYr AS [Age To],AgeMonth As [Age From],Approved_By AS [Approved By],Approved_Date As [Approved Date],Approved_Status AS [Approved Status],Closed_By As [Closed By],Closed_Date AS [Closed Date],Closed_Status AS [Closed Status] FROM TSPL_HR_REQUISITION  "
        str = clsCommon.ShowSelectForm("ReqHR", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str

    End Function
    Public Shared Function savedata(ByVal requisition As clsrequisitionentry, ByVal isnewentry As Boolean, ByVal Arra As List(Of clsrequisitionentryDeatils))
        trans = clsDBFuncationality.GetTransactin()
        Try
            ' Dim qry As String
            Dim obj As clsrequisitionentry = New clsrequisitionentry
            'If isnewentry Then
            '    qry = "select max(Requisition_Code) from tspl_hr_requisition"
            '    requisition.code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
            '    If clsCommon.myLen(requisition.code) <= 0 Then
            '        requisition.code = "REQ000001"

            '    Else
            '        requisition.code = clsCommon.incval(requisition.code)
            '    End If

            'End If
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_HR_REQUISITION_Details where requisition_Code='" + requisition.code + "'", trans)
            Dim coll As New Hashtable()
            If isnewentry Then
                requisition.code = clsERPFuncationality.GetNextCode(trans, requisition.Req_Date, clsDocType.HRRequisitionEntry, "", "")
            End If
            'clsCommon.AddColumnsForChange(coll, "Requisition_Code", requisition.code)
            clsCommon.AddColumnsForChange(coll, "Requisition_Description", requisition.description)
            clsCommon.AddColumnsForChange(coll, "Initiated_By", requisition.initiated_by)
            clsCommon.AddColumnsForChange(coll, "Date", clsCommon.GetPrintDate(requisition.Req_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Profile_Code", requisition.profile, True)
            clsCommon.AddColumnsForChange(coll, "Recommended_By", requisition.recommended_by, True)
            clsCommon.AddColumnsForChange(coll, "DEPARTMENT_CODE", requisition.Department, True)
            clsCommon.AddColumnsForChange(coll, "Location_Code", requisition.location, True)
            'clsCommon.AddColumnsForChange(coll, "Sub_location_Code", requisition.sub_location, True)
            clsCommon.AddColumnsForChange(coll, "DESIGNATION_ID", requisition.designation, True)
            clsCommon.AddColumnsForChange(coll, "Job_Title_Code", requisition.jobtitle, True)
            clsCommon.AddColumnsForChange(coll, "NoOfPost", requisition.no_of_post)
            clsCommon.AddColumnsForChange(coll, "Emp_Type", requisition.Emp_type, True)
            clsCommon.AddColumnsForChange(coll, "CTCRange", requisition.ctc_range)
            clsCommon.AddColumnsForChange(coll, "Hiring_Type", requisition.Hiring_type)
            clsCommon.AddColumnsForChange(coll, "MinExpYr", requisition.minimumexperience)
            clsCommon.AddColumnsForChange(coll, "MaxExpYr", requisition.maximumexperience)
            clsCommon.AddColumnsForChange(coll, "Gender ", requisition.gender)
            clsCommon.AddColumnsForChange(coll, "AgeYr", requisition.age_yr)
            clsCommon.AddColumnsForChange(coll, "AgeMonth", requisition.age_month)
            clsCommon.AddColumnsForChange(coll, "MinExpMonth", (requisition.MinExpMonth))
            clsCommon.AddColumnsForChange(coll, "MaxExpMonth", (requisition.MaxExpMonth))
            clsCommon.AddColumnsForChange(coll, "Approved_By", requisition.Approved_By, True)
            clsCommon.AddColumnsForChange(coll, "Industry_Code", requisition.Industry_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vertical_Code", requisition.Vertical_Code, True)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Approved_Status", clsCommon.myCdbl(requisition.ApprovedStatus))
            clsCommon.AddColumnsForChange(coll, "Closed_Status", clsCommon.myCdbl(requisition.ClosedStatus))
            If isnewentry Then
                'requisition.HR_REQUISITION_CODE = clsERPFuncationality.GetNextCode(trans, obj.Req_Date, "HR REQUISITION ENTRY", "", obj.code)
                clsCommon.AddColumnsForChange(coll, "Requisition_Code", requisition.code)
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_hr_requisition", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "tspl_hr_requisition", OMInsertOrUpdate.Update, " Requisition_Code='" + requisition.code + "'", trans)
            End If
            clsrequisitionentryDeatils.savedata(requisition.code, Arra, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getdata(ByVal code As String, ByVal navigatortype As NavigatorType) As clsrequisitionentry
        Try
            Dim obj As clsrequisitionentry = Nothing
            Dim qst As String = (" select tspl_hr_requisition.Requisition_Code,TSPL_HR_REQUISITION.Closed_Status,TSPL_HR_REQUISITION.Approved_Status  ,Requisition_Description ,tspl_hr_requisition.Date ,tspl_hr_requisition.Initiated_By ,tspl_hr_requisition.Recommended_By ,tspl_hr_requisition.DEPARTMENT_CODE ,tspl_hr_requisition.Location_Code,tspl_hr_requisition.Sub_location_Code  ,tspl_hr_requisition.DESIGNATION_ID ,tspl_hr_requisition.Job_Title_Code ,tspl_hr_requisition.Emp_Type ,tspl_hr_requisition.Profile_Code ,tspl_hr_requisition.NoOfPost ,tspl_hr_requisition.CTCRange ,tspl_hr_requisition.Hiring_Type ,tspl_hr_requisition.MinExpYr,tspl_hr_requisition.MaxExpMonth , tspl_hr_requisition.MaxExpYr,tspl_hr_requisition.AgeYr   ,tspl_hr_requisition.MinExpMonth ,tspl_hr_requisition.MaxExpYr ,tspl_hr_requisition.Gender  ,tspl_hr_requisition.AgeMonth ,TSPL_HR_REQUISITION_Details.Qualification_Code,tspl_hr_requisition.Industry_Code,tspl_hr_requisition.Vertical_Code from  tspl_hr_requisition left Outer join  TSPL_HR_REQUISITION_Details on TSPL_HR_REQUISITION_Details.requisition_Code=tspl_hr_requisition.Requisition_Code  where 2=2")
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and tspl_hr_requisition.Requisition_Code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and tspl_hr_requisition.Requisition_Code in (select  min(Requisition_Code)from tspl_hr_requisition  where tspl_hr_requisition.Requisition_Code  >'" + code + "')"
                Case navigatortype.First
                    qst += "and tspl_hr_requisition.Requisition_Code in (select MIN(Requisition_Code)from tspl_hr_requisition )"

                Case navigatortype.Last
                    qst += "and tspl_hr_requisition.Requisition_Code in (select Max(Requisition_Code)from tspl_hr_requisition )"
                Case navigatortype.Previous
                    qst += "and tspl_hr_requisition.Requisition_Code in (select  max(Requisition_Code)from tspl_hr_requisition  where tspl_hr_requisition.Requisition_Code <'" + code + "')"
            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsrequisitionentry
                obj.code = clsCommon.myCstr(dt1.Rows(0)("Requisition_Code"))
                obj.description = clsCommon.myCstr(dt1.Rows(0)("Requisition_Description"))
                obj.Req_Date = clsCommon.myCDate(dt1.Rows(0)("date"))
                obj.initiated_by = clsCommon.myCstr(dt1.Rows(0)("Initiated_By"))
                obj.profile = clsCommon.myCstr(dt1.Rows(0)("Profile_Code"))
                obj.recommended_by = clsCommon.myCstr(dt1.Rows(0)("Recommended_By"))
                obj.Department = clsCommon.myCstr(dt1.Rows(0)("DEPARTMENT_CODE"))
                obj.location = clsCommon.myCstr(dt1.Rows(0)("Location_Code"))
                obj.sub_location = clsCommon.myCstr(dt1.Rows(0)("Sub_location_Code"))
                obj.designation = clsCommon.myCstr(dt1.Rows(0)("DESIGNATION_ID"))
                obj.jobtitle = clsCommon.myCstr(dt1.Rows(0)("Job_Title_Code"))
                obj.no_of_post = clsCommon.myCstr(dt1.Rows(0)("NoOfPost"))
                obj.Emp_type = clsCommon.myCstr(dt1.Rows(0)("Emp_Type"))
                obj.ctc_range = clsCommon.myCstr(dt1.Rows(0)("CTCRange"))
                obj.Hiring_type = clsCommon.myCstr(dt1.Rows(0)("Hiring_Type"))
                obj.MinExpMonth = clsCommon.myCdbl(dt1.Rows(0)("MinExpMonth"))
                obj.MaxExpMonth = clsCommon.myCdbl(dt1.Rows(0)("MaxExpMonth"))
                obj.minimumexperience = clsCommon.myCdbl(dt1.Rows(0)("MinExpYr"))
                obj.maximumexperience = clsCommon.myCdbl(dt1.Rows(0)("MaxExpYr"))
                obj.gender = clsCommon.myCstr(dt1.Rows(0)("Gender"))
                obj.age_yr = clsCommon.myCdbl(dt1.Rows(0)("AgeYr"))
                obj.age_month = clsCommon.myCdbl(dt1.Rows(0)("AgeMonth"))
                'If obj.ApprovedStatus = clsCommon.myCstr(dt1.Rows(0)("Approved_Status")) Then
                obj.ApprovedStatus = IIf(clsCommon.myCdbl(dt1.Rows(0)("Approved_Status")) = 1, "True", "False")
                'End If
                'If obj.ClosedStatus = clsCommon.myCBool(dt1.Rows(0)("Closed_Status")) Then
                obj.ClosedStatus = IIf(clsCommon.myCdbl(dt1.Rows(0)("Closed_Status")) = 1, "True", "False")
                'End If
                obj.Industry_Code = clsCommon.myCstr(dt1.Rows(0)("Industry_Code"))
                obj.Vertical_Code = clsCommon.myCstr(dt1.Rows(0)("Vertical_Code"))
            End If
            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                obj.arr = New List(Of clsrequisitionentryDeatils)
                For Each dr As DataRow In dt1.Rows
                    Dim objTr As clsrequisitionentryDeatils = New clsrequisitionentryDeatils()
                    'objTr.ReqCode = clsCommon.myCdbl(dr("Requisition_Code"))
                    objTr.QualificationCode = clsCommon.myCstr(dr("Qualification_Code"))
                    obj.arr.Add(objTr)
                    obj.arrqul.Add(objTr.QualificationCode)
                Next
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
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
            qry = "DELETE FROM TSPL_HR_REQUISITION_Details WHERE Requisition_Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "DELETE FROM tspl_hr_requisition WHERE Requisition_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
                isSaved = False
            End If
        Catch ex As Exception
            trans.Rollback()
            isSaved = False
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function

End Class
Public Class clsrequisitionentryDeatils
#Region "Variables"
    Public QualificationCode As String
    Public DepartmentName As String
    Public ReqCode As String
#End Region
    Public Shared Function savedata(ByVal RerCode As String, ByVal arr As List(Of clsrequisitionentryDeatils), ByVal trans As SqlTransaction) As Boolean

        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            For Each ReqCode As clsrequisitionentryDeatils In arr
                Try

                    Dim coll As New Hashtable()

                    clsCommon.AddColumnsForChange(coll, "requisition_Code", RerCode)
                    clsCommon.AddColumnsForChange(coll, "Qualification_Code", ReqCode.QualificationCode)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_REQUISITION_Details", OMInsertOrUpdate.Insert, "", trans)


                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Next ReqCode

        End If
        Return True
    End Function


End Class

