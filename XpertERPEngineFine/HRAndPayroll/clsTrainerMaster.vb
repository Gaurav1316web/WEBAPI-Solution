Imports common
Imports System.Data.SqlClient
Public Class clsTrainerMaster
#Region "variable"
    Public Code As String = Nothing
    Public Name As String = Nothing
    Public Type As String = Nothing
    Public Institute_Code As String = Nothing
    Public First_name As String = Nothing
    Public Last_Name As String = Nothing
    Public Emp_Code As String = Nothing
    Public Email_Id As String = Nothing
    Public Payment_type As String = Nothing
    Public Address1 As String = Nothing
    Public Address2 As String = Nothing
    Public Address3 As String = Nothing
    Public City As String = Nothing
    Public State As String = Nothing
    Public Country As String = Nothing

    Public City_Name As String = Nothing
    Public State_name As String = Nothing
    Public Country_Name As String = Nothing
    Public Institute_Name As String = Nothing
    Public Employee_Name As String = Nothing


    Public Pin_Code As String = Nothing
    Public PhoneNo1 As String = Nothing
    Public PhoneNo2 As String = Nothing
    Public Gendor As String = Nothing
    Public DOB As Date
    Public Remark As String = Nothing
    Public Is_Applicable As String = Nothing
    Public Fax As String = Nothing

    Public Shared ArrQualification As List(Of clsQualification)
    Public Shared ArrCourse As List(Of clsCourse)
    Public Shared ArrCities As List(Of clsTrainingGivenCities)

    Public Shared ArrQualification_Arr As New ArrayList()
    Public Shared ArrCourse_Arr As New ArrayList()
    Public Shared ArrCities_Arr As New ArrayList()


    Public CREATED_BY As String
    Public Posting_Date As Date
    Public POSTED As ERPTransactionStatus = ERPTransactionStatus.Pending

#End Region
    Public Shared Function SaveData(ByVal obj As clsTrainerMaster, ByVal objQualification As List(Of clsQualification), ByVal objCourse As List(Of clsCourse), ByVal objCities As List(Of clsTrainingGivenCities)) As Boolean

        Dim isSaved As Boolean = True
        Dim isNewEntry As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim qry As String = "select count(*) from tspl_trainer_master where Doc_Code='" + obj.Code + "'"
            Dim isexist As Integer = clsDBFuncationality.getSingleValue(qry, trans)
            If isexist = 0 Then
                isNewEntry = True
            Else
                isNewEntry = False

            End If
            Dim coll As New Hashtable()

            clsCommon.AddColumnsForChange(coll, "DOC_Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "DOC_Name", obj.Name)
            clsCommon.AddColumnsForChange(coll, "DOC_DATE", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Institute_Code", obj.Institute_Code)
            clsCommon.AddColumnsForChange(coll, "First_Name", obj.First_name)
            clsCommon.AddColumnsForChange(coll, "Last_Name", obj.Last_Name)
            clsCommon.AddColumnsForChange(coll, "Employee_Code", obj.Emp_Code, True)
            clsCommon.AddColumnsForChange(coll, "Payment_Type", obj.Payment_type)
            clsCommon.AddColumnsForChange(coll, "Add1", obj.Address1)
            clsCommon.AddColumnsForChange(coll, "Add2", obj.Address2)
            clsCommon.AddColumnsForChange(coll, "Add3", obj.Address3)
            clsCommon.AddColumnsForChange(coll, "City_code", obj.City)
            clsCommon.AddColumnsForChange(coll, "State_Code", obj.State)
            clsCommon.AddColumnsForChange(coll, "Country_code", obj.Country)
            clsCommon.AddColumnsForChange(coll, "Pin_code", obj.Pin_Code)
            clsCommon.AddColumnsForChange(coll, "Telphone1", obj.PhoneNo1)
            clsCommon.AddColumnsForChange(coll, "Telphone2", obj.PhoneNo2)
            clsCommon.AddColumnsForChange(coll, "Gender", obj.Gendor)
            clsCommon.AddColumnsForChange(coll, "Email", obj.Email_Id)
            clsCommon.AddColumnsForChange(coll, "Fax", obj.Fax)
            clsCommon.AddColumnsForChange(coll, "DOB", clsCommon.GetPrintDate(obj.DOB, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Is_Applicable", obj.Is_Applicable)


            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Posted", "0")
            If isnewentry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))

                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master", OMInsertOrUpdate.Update, " Doc_Code='" + obj.Code + "'", trans)
            End If
            isSaved = isSaved AndAlso clsQualification.SaveData(obj.Code, objQualification, trans, isNewEntry)
            isSaved = isSaved AndAlso clsCourse.SaveData(obj.Code, objCourse, trans, isNewEntry)
            isSaved = isSaved AndAlso clsTrainingGivenCities.SaveData(obj.Code, objCities, trans, isNewEntry)

            If isSaved Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function DeleteData(ByVal strcode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strcode) >= 0) Then
                Dim qry As String = "delete from TSPL_Trainer_Master_Qualification where Doc_code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Trainer_Master_Course where Doc_code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Trainer_Master_City where Doc_code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_Trainer_Master where Doc_code='" + strcode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function

    Public Shared Function PostData(ByVal strDocNo As String, ByVal isCheckForPosted As Boolean) As Boolean
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Code not found to Post")
            End If
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt")
            Dim obj As clsTrainerMaster = clsTrainerMaster.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            If (isCheckForPosted AndAlso obj.POSTED = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            Dim qry As String = "Update tspl_Trainer_master set POSTED=1,Modified_By='" + objCommonVar.CurrentUserCode + "' where DOC_CODE ='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function


    Public Shared Function GetData(ByVal code As String, ByVal navigatortype As NavigatorType) As clsTrainerMaster
        Try


            Dim obj As clsTrainerMaster = Nothing
            Dim qst As String = ("select DOC_Code,DOC_Name,Remark,case when Type='I' then 'Internal' else 'External' end as Type,Institute_Code," _
                                 & " First_Name,Last_Name,Employee_Code,Payment_Type,tm.Add1,tm.Add2,tm.Add3,tm.City_code,tm.State_Code,tm.Country_code," _
                                 & " tm.Pin_code,Telphone1, Telphone2,case when Gender='M' then 'Male' when Gender='F' then 'FeMale' else 'Unisex' end as Gender,Email," _
                                 & "Fax,DOB, Is_Applicable,tm.Posted ,city_name,state_name,Country_Name,Emp_name,name as Institute_name from " _
                                 & " tspl_trainer_master tm Left join TSPL_CITY_MASTER cm on tm.City_code=cm.City_Code Left join TSPL_State_MASTER sm on " _
                                 & " tm.state_code=sm.STATE_CODE Left join TSPL_Country_MASTER cmm on tm.Country_code=cmm.Country_Code Left join TSPL_institute_MASTER Im on tm.institute_code=Im.Code " _
                                 & " Left join TSPL_Employee_MASTER em on tm.Employee_code=em.EMP_CODE where 2=2 ")
            Select Case navigatortype
                Case navigatortype.Current
                    qst += "and DOC_Code in ('" + code + "')"
                Case navigatortype.Next
                    qst += "and DOC_Code in (select  min(DOC_Code)from tspl_trainer_master where DOC_Code >'" + code + "')"
                Case navigatortype.First
                    qst += "and DOC_Code in (select MIN(DOC_Code)from tspl_trainer_master)"

                Case navigatortype.Last
                    qst += "and DOC_Code in (select Max(DOC_Code)from tspl_trainer_master)"
                Case navigatortype.Previous
                    qst += "and DOC_Code in (select  max(DOC_Code)from tspl_trainer_master where DOC_Code <'" + code + "')"

            End Select
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qst)
            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                obj = New clsTrainerMaster
                obj.Code = clsCommon.myCstr(dt1.Rows(0)("Doc_Code"))
                obj.Name = clsCommon.myCstr(dt1.Rows(0)("Doc_Name"))


                obj.Remark = clsCommon.myCstr(dt1.Rows(0)("Remark"))
                obj.Type = clsCommon.myCstr(dt1.Rows(0)("Type"))
                obj.Institute_Code = clsCommon.myCstr(dt1.Rows(0)("Institute_Code"))
                obj.First_name = clsCommon.myCstr(dt1.Rows(0)("First_Name"))
                obj.Last_Name = clsCommon.myCstr(dt1.Rows(0)("Last_Name"))
                obj.Emp_Code = clsCommon.myCstr(dt1.Rows(0)("Employee_Code"))
                obj.Payment_type = clsCommon.myCstr(dt1.Rows(0)("Payment_Type"))
                obj.Address1 = clsCommon.myCstr(dt1.Rows(0)("Add1"))
                obj.Address2 = clsCommon.myCstr(dt1.Rows(0)("Add2"))
                obj.Address3 = clsCommon.myCstr(dt1.Rows(0)("Add3"))
                obj.City = clsCommon.myCstr(dt1.Rows(0)("City_code"))
                obj.State = clsCommon.myCstr(dt1.Rows(0)("State_Code"))
                obj.Country = clsCommon.myCstr(dt1.Rows(0)("Country_code"))
                obj.Pin_Code = clsCommon.myCstr(dt1.Rows(0)("Pin_code"))
                obj.PhoneNo1 = clsCommon.myCstr(dt1.Rows(0)("Telphone1"))
                obj.PhoneNo2 = clsCommon.myCstr(dt1.Rows(0)("Telphone2"))
                obj.Gendor = clsCommon.myCstr(dt1.Rows(0)("Gender"))
                obj.Email_Id = clsCommon.myCstr(dt1.Rows(0)("Email"))
                obj.Fax = clsCommon.myCstr(dt1.Rows(0)("Fax"))
                obj.DOB = clsCommon.myCstr(dt1.Rows(0)("DOB"))
                obj.Is_Applicable = clsCommon.myCstr(dt1.Rows(0)("Is_Applicable"))
                obj.Employee_Name = clsCommon.myCstr(dt1.Rows(0)("Emp_Name"))
                obj.Institute_Name = clsCommon.myCstr(dt1.Rows(0)("Institute_Name"))
                obj.City_Name = clsCommon.myCstr(dt1.Rows(0)("City_Name"))
                obj.State_name = clsCommon.myCstr(dt1.Rows(0)("State_Name"))
                obj.Country_Name = clsCommon.myCstr(dt1.Rows(0)("Country_Name"))
                obj.POSTED = clsCommon.myCstr(dt1.Rows(0)("POsted"))
                code = obj.Code
            End If
            '===================Qualification Detail=================================================
            ArrQualification_Arr = New ArrayList

            qst = "select * from Tspl_Trainer_Master_Qualification where doc_Code='" & code & "'"


            dt1 = New DataTable()
            dt1 = clsDBFuncationality.GetDataTable(qst)

            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                For Each dr As DataRow In dt1.Rows
                    ArrQualification_Arr.Add(clsCommon.myCstr(dr("Qualification_Code")))
                Next
            End If
            '============================================================================================


            '===================Course Detail=================================================
            ArrCourse_Arr = New ArrayList

            qst = "select * from Tspl_Trainer_Master_Course where doc_Code='" & code & "'"

            dt1 = New DataTable()
            dt1 = clsDBFuncationality.GetDataTable(qst)

            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                For Each dr As DataRow In dt1.Rows
                    ArrCourse_Arr.Add(clsCommon.myCstr(dr("Course_Code")))
                Next
            End If
            '============================================================================================


            '===================Cities Detail=================================================
            ArrCities_Arr = New ArrayList

            qst = "select * from Tspl_Trainer_Master_City where doc_Code='" & code & "'"

            dt1 = New DataTable()
            dt1 = clsDBFuncationality.GetDataTable(qst)

            If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                For Each dr As DataRow In dt1.Rows
                    ArrCities_Arr.Add(clsCommon.myCstr(dr("City_Code")))
                Next
            End If
            '============================================================================================

            clsTrainerMaster.ArrCities_Arr = ArrCities_Arr
            clsTrainerMaster.ArrCourse_Arr = ArrCourse_Arr
            clsTrainerMaster.ArrQualification_Arr = ArrQualification_Arr

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class

Public Class clsQualification
#Region "Variables"
    Public DOC_CODE As String
    Public Qualification_code As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsQualification), ByVal trans As SqlTransaction, ByVal isNewentry As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim Str As String = "delete from Tspl_Trainer_Master_Qualification where DOC_CODE='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(Str, trans)
            For Each obj As clsQualification In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Qualification_code", obj.Qualification_code)
                'If isNewentry Then
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master_Qualification", OMInsertOrUpdate.Insert, "Tspl_Trainer_Master_Qualification.DOC_CODE='" + strDocNo + "'", trans)
                'Else
                'clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master_Qualification", OMInsertOrUpdate.Update, "Tspl_Trainer_Master_Qualification.DOC_CODE='" + strDocNo + "' and Tspl_Trainer_Master_Qualification.Qualification_Code='" & obj.Qualification_code & "'", trans)
                'End If
            Next
        End If
        Return True
    End Function
End Class

Public Class clsCourse
#Region "Variables"
    Public DOC_CODE As String
    Public Course_code As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsCourse), ByVal trans As SqlTransaction, ByVal isNewentry As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim Str As String = "delete from Tspl_Trainer_Master_Course where DOC_CODE='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(Str, trans)
            For Each obj As clsCourse In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Course_code", obj.Course_code)
                'If isNewentry Then
                clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master_Course", OMInsertOrUpdate.Insert, "Tspl_Trainer_Master_Course.DOC_CODE='" + strDocNo + "'", trans)
                'Else
                '    clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master_Course", OMInsertOrUpdate.Update, "Tspl_Trainer_Master_Course.DOC_CODE='" + strDocNo + "' and Tspl_Trainer_Master_Course.Course_Code='" & obj.Course_code & "'", trans)
                'End If
            Next
        End If
        Return True
    End Function
End Class

Public Class clsTrainingGivenCities
#Region "Variables"
    Public DOC_CODE As String
    Public City_code As String
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTrainingGivenCities), ByVal trans As SqlTransaction, ByVal isNewentry As Boolean) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTrainingGivenCities In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DOC_CODE", strDocNo)
                clsCommon.AddColumnsForChange(coll, "City_code", obj.City_code)
                If isNewentry Then
                    clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master_City", OMInsertOrUpdate.Insert, "Tspl_Trainer_Master_City.DOC_CODE='" + strDocNo + "'", trans)
                Else
                    If clsDBFuncationality.getSingleValue("select count(*) from Tspl_Trainer_Master_City where Tspl_Trainer_Master_City.DOC_CODE='" + strDocNo + "' and Tspl_Trainer_Master_City.City_Code='" & obj.City_code & "'", trans) = 0 Then
                        clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master_City", OMInsertOrUpdate.Insert, "Tspl_Trainer_Master_City.DOC_CODE='" + strDocNo + "'", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "Tspl_Trainer_Master_City", OMInsertOrUpdate.Update, "Tspl_Trainer_Master_City.DOC_CODE='" + strDocNo + "' and Tspl_Trainer_Master_City.City_Code='" & obj.City_code & "'", trans)
                    End If
                End If
            Next
        End If
        Return True
    End Function
End Class


