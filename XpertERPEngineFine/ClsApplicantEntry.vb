Imports System.Data.SqlClient
Imports common

Public Class ClsApplicantEntry

#Region "Variables"
    Public APPLICANT_CODE As String = Nothing
    Public Applicant_Description As String = Nothing
    Public Applicant_Date As String = Nothing
    Public Requisition_Code As String = Nothing
    Public Date_Of_Interview As String = Nothing
    Public Gender As String = Nothing
    Public Salutation As String = Nothing
    Public First_Name As String = Nothing
    Public Middle_Name As String = Nothing
    Public Last_Name As String = Nothing
    Public Applicant_Date_Of_Birth As String = Nothing
    Public Maritial_Status As String = Nothing
    Public Pan_No As String = Nothing

    Public Add1 As String = Nothing
    Public Add2 As String = Nothing
    Public Add3 As String = Nothing
    Public Add4 As String = Nothing
    Public COUNTRY_CODE As String = Nothing
    Public City_code As String = Nothing
    Public State_Code As String = Nothing
    Public Pin_Code As String = Nothing
    Public Email As String = Nothing
    Public TELEPHONE_NO As String = Nothing
    Public Relation_Code As String = Nothing
    Public Source_Type_Code As String = Nothing
    Public Source_Type_Detail_Code As String = Nothing
    Public Emp_Refrence As Integer
    Public Agency_Refrence As Integer
    Public Emp_Code As String = Nothing
    Public Handicaped_Detail As String = Nothing
    Public Blood_Group As String = Nothing
    Public Bank_Code As String = Nothing
    Public Branch_Code As String = Nothing
    Public Account_No As String = Nothing
    Public From_Location_Code As String = Nothing
    Public To_location_Code As String = Nothing
    Public Location_Code As String = Nothing
    Public Preferred_Location_Code As String = Nothing
    Public Current_Gross_Salary As Double = 0
    Public Total_CTC As Double = 0
    Public Performance_By As String = Nothing
    Public Is_Fresher As Integer
    Public Is_Handicaped As Integer
    Public Relocation As Integer
    Public DOCUMENT_FILE As Byte()
    Public DocName As String
    Public Agency_Code As String = Nothing

    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public ShortListed As Integer = 0
    Public Rejected As Integer = 0
    'Public isNewEntry As Boolean = False
    Public ObjList As List(Of ClsAppQualificationDetail) = Nothing
    Dim objDetail As New ClsAppQualificationDetail()
    Public ObjListChk As List(Of ClsAppCheckListDetail) = Nothing
    Dim objDetailChk As New ClsAppCheckListDetail()
    Public ObjListEmp As List(Of ClsAppEmpHisListDetail) = Nothing
    Dim objDetailEmp As New ClsAppEmpHisListDetail()
    Public ObjListFamily As List(Of ClsAppFamilyListDetail) = Nothing
    Dim objDetailFamily As New ClsAppFamilyListDetail()
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select APPLICANT_CODE AS [Code],Applicant_Description AS [Applicant Description],CONVERT (VARCHAR,Applicant_Date ,103) AS [Applicant Date],Requisition_Code AS [Requisition Code],CASE WHEN Gender='F' THEN 'Female' WHEN Gender ='M' THEN 'Male' END AS [Gender],First_Name + ' ' + Middle_Name + ' ' + Last_Name AS [Applicant Name],CONVERT(VARCHAR,Applicant_Date_Of_Birth,103) As [Applicant Date Of Birth],CONVERT (VARCHAR,Date_Of_Interview,103) AS [Date Of Interview],CASE WHEN Maritial_Status='M' THEN 'Married' WHEN Maritial_Status ='U' THEN 'UnMarried' END As [Maritial Status],Pan_No AS [Pan No],Add1 +' '+Add2 +' '+Add3 +' '+Add4 AS [Address],TELEPHONE_NO As [Phone No],Email,Source_Type_Code AS [Source Type Code],Source_Type_Detail_Code AS [Source Type Detail Code],Is_Fresher AS [Is Fresher],Short AS [Is Shortlisted],Rejected As [Is Rejected],COUNTRY_CODE AS [Country Code],State_Code As [State Code],City_code As [City Code],Pin_Code As [Pin Code],Blood_Group As [Blooad Group],Bank_Code As [Bank Code],Branch_Code As [Branch Code],Location_Code AS [Location Code],Preferred_Location_Code AS [Preferred Location Code],Current_Gross_Salary As [Current Gross Salary],Total_CTC AS [Total CTC],Performance_By As [Performance By] ,Is_Handicaped As [Is Handicaped],Created_By AS [Created By],CONVERT(varchar,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_HR_APPLICANT_ENTRY  "
        str = clsCommon.ShowSelectForm("HRAPPFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal arr As List(Of ClsApplicantEntry)) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()

            If ClsApplicantEntry.SaveData(arr, trans, Nothing) Then
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function SaveData(ByVal arr As List(Of ClsApplicantEntry), ByVal trans As SqlTransaction, ByVal isNewEntry As Boolean) As Boolean
        Dim isSaved As Boolean = True
        Try
            For Each obj As ClsApplicantEntry In arr
                Dim coll As New Hashtable()
                'clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", obj.APPLICANT_CODE)
                clsCommon.AddColumnsForChange(coll, "Applicant_Description", obj.Applicant_Description)
                clsCommon.AddColumnsForChange(coll, "Applicant_Date", clsCommon.GetPrintDate(obj.Applicant_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Requisition_Code", obj.Requisition_Code, True)
                clsCommon.AddColumnsForChange(coll, "Date_Of_Interview", clsCommon.GetPrintDate(obj.Date_Of_Interview, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Gender", obj.Gender)
                clsCommon.AddColumnsForChange(coll, "Salutation", obj.Salutation)
                clsCommon.AddColumnsForChange(coll, "First_Name", obj.First_Name)
                clsCommon.AddColumnsForChange(coll, "Middle_Name", obj.Middle_Name)
                clsCommon.AddColumnsForChange(coll, "Last_Name", obj.Last_Name)
                clsCommon.AddColumnsForChange(coll, "Applicant_Date_Of_Birth", clsCommon.GetPrintDate(obj.Applicant_Date_Of_Birth, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Maritial_Status", obj.Maritial_Status)
                clsCommon.AddColumnsForChange(coll, "Pan_No", obj.Pan_No)
                'clsCommon.AddColumnsForChange(coll, "Applicant_Photo", obj.Applicant_Photo)
                clsCommon.AddColumnsForChange(coll, "Add1", obj.Add1)
                clsCommon.AddColumnsForChange(coll, "Add2", obj.Add2)
                clsCommon.AddColumnsForChange(coll, "Add3", obj.Add3)
                clsCommon.AddColumnsForChange(coll, "Add4", obj.Add4)
                clsCommon.AddColumnsForChange(coll, "Email", obj.Email)
                clsCommon.AddColumnsForChange(coll, "TELEPHONE_NO", obj.TELEPHONE_NO)
                clsCommon.AddColumnsForChange(coll, "COUNTRY_CODE", obj.COUNTRY_CODE, True)
                clsCommon.AddColumnsForChange(coll, "City_code", obj.City_code, True)
                clsCommon.AddColumnsForChange(coll, "State_Code", obj.State_Code, True)
                clsCommon.AddColumnsForChange(coll, "Pin_Code", obj.Pin_Code)
                clsCommon.AddColumnsForChange(coll, "Relation_Code", obj.Relation_Code, True)
                clsCommon.AddColumnsForChange(coll, "Source_Type_Code", obj.Source_Type_Code, True)
                clsCommon.AddColumnsForChange(coll, "Source_Type_Detail_Code", obj.Source_Type_Detail_Code, True)
                clsCommon.AddColumnsForChange(coll, "Emp_Refrence", obj.Emp_Refrence)
                clsCommon.AddColumnsForChange(coll, "Agency_Refrence", obj.Agency_Refrence)
                clsCommon.AddColumnsForChange(coll, "Emp_Code", obj.Emp_Code, True)
                clsCommon.AddColumnsForChange(coll, "Handicaped_Detail", obj.Handicaped_Detail)
                clsCommon.AddColumnsForChange(coll, "Blood_Group", obj.Blood_Group)
                clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code, True)
                clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code, True)
                clsCommon.AddColumnsForChange(coll, "Account_No", obj.Account_No)
                clsCommon.AddColumnsForChange(coll, "From_Location_Code", obj.From_Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "To_location_Code", obj.To_location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Preferred_Location_Code", obj.Preferred_Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Current_Gross_Salary", obj.Current_Gross_Salary)
                clsCommon.AddColumnsForChange(coll, "Total_CTC", obj.Total_CTC)
                clsCommon.AddColumnsForChange(coll, "Performance_By", obj.Performance_By)
                clsCommon.AddColumnsForChange(coll, "Is_Fresher", obj.Is_Fresher)
                clsCommon.AddColumnsForChange(coll, "Is_Handicaped", obj.Is_Handicaped)
                clsCommon.AddColumnsForChange(coll, "Relocation", obj.Relocation)
                clsCommon.AddColumnsForChange(coll, "Agency_Code", obj.Agency_Code, True)

                clsCommon.AddColumnsForChange(coll, "Short", obj.ShortListed)
                clsCommon.AddColumnsForChange(coll, "Rejected", obj.Rejected)
                clsCommon.AddColumnsForChange(coll, "Posted", clsCommon.myCdbl(obj.Posted))

                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

                clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))


                'If isNewEntry Then
                '    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                '    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                '    isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_APPLICANT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
                'Else
                '    isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_APPLICANT_ENTRY", OMInsertOrUpdate.Update, "TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE='" + obj.APPLICANT_CODE + "'", trans)
                'End If
                'isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_APPLICANT_ENTRY", OMInsertOrUpdate.Update, "APPLICANT_CODE='" + obj.APPLICANT_CODE + "'", trans)
                If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_APPLICANT_ENTRY WHERE APPLICANT_CODE='" + obj.APPLICANT_CODE + "'", trans) <= 0 Then
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    Dim qry As String = "SELECT Count(*) FROM TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE= '" & obj.APPLICANT_CODE & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                    If check = 0 Then
                        obj.APPLICANT_CODE = clsERPFuncationality.GetNextCode(trans, obj.Applicant_Date, "HR Applicant Entry", "", "")
                        clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", obj.APPLICANT_CODE)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_APPLICANT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
                    Else

                        Throw New Exception("This Code Is Already Exist")

                    End If
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_APPLICANT_ENTRY", OMInsertOrUpdate.Update, "APPLICANT_CODE='" + obj.APPLICANT_CODE + "'", trans)
                End If


                ClsAppQualificationDetail.SaveData(obj.APPLICANT_CODE, obj.ObjList, trans)
                ClsAppCheckListDetail.SaveData(obj.APPLICANT_CODE, obj.ObjListChk, trans)
                ClsAppEmpHisListDetail.SaveData(obj.APPLICANT_CODE, obj.ObjListEmp, trans)
                ClsAppFamilyListDetail.SaveData(obj.APPLICANT_CODE, obj.ObjListFamily, trans)

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsApplicantEntry
        Return GetData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsApplicantEntry
        Dim obj As ClsApplicantEntry = Nothing

        Dim qry As String = "Select * From TSPL_HR_APPLICANT_ENTRY where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select MIN(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select Min(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY where  Applicant_Code >'" + strCode + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY where Applicant_Code <'" + strCode + "')"
            Case NavigatorType.Current
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = '" + strCode + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsApplicantEntry()
            obj.APPLICANT_CODE = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Applicant_Description = clsCommon.myCstr(dt.Rows(0)("Applicant_Description"))
            obj.Applicant_Date = clsCommon.myCDate(dt.Rows(0)("Applicant_Date"))
            obj.Requisition_Code = clsCommon.myCstr(dt.Rows(0)("Requisition_Code"))
            obj.Date_Of_Interview = clsCommon.myCDate(dt.Rows(0)("Date_Of_Interview"))
            obj.Gender = clsCommon.myCstr(dt.Rows(0)("Gender"))
            obj.Salutation = clsCommon.myCstr(dt.Rows(0)("Salutation"))
            obj.First_Name = clsCommon.myCstr(dt.Rows(0)("First_Name"))
            obj.Middle_Name = clsCommon.myCstr(dt.Rows(0)("Middle_Name"))
            obj.Last_Name = clsCommon.myCstr(dt.Rows(0)("Last_Name"))
            obj.Applicant_Date_Of_Birth = clsCommon.myCDate(dt.Rows(0)("Applicant_Date_Of_Birth"))
            obj.Maritial_Status = clsCommon.myCstr(dt.Rows(0)("Maritial_Status"))
            obj.Pan_No = clsCommon.myCstr(dt.Rows(0)("Pan_No"))
            obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
            obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
            obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
            obj.Add4 = clsCommon.myCstr(dt.Rows(0)("Add4"))
            obj.Relation_Code = clsCommon.myCstr(dt.Rows(0)("Relation_Code"))
            obj.Source_Type_Code = clsCommon.myCstr(dt.Rows(0)("Source_Type_Code"))
            obj.Source_Type_Detail_Code = clsCommon.myCstr(dt.Rows(0)("Source_Type_Detail_Code"))
            obj.COUNTRY_CODE = clsCommon.myCstr(dt.Rows(0)("COUNTRY_CODE"))
            obj.City_code = clsCommon.myCstr(dt.Rows(0)("City_code"))
            obj.State_Code = clsCommon.myCstr(dt.Rows(0)("State_Code"))
            obj.Pin_Code = clsCommon.myCstr(dt.Rows(0)("Pin_Code"))
            obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
            obj.TELEPHONE_NO = clsCommon.myCstr(dt.Rows(0)("TELEPHONE_NO"))
            obj.Emp_Refrence = clsCommon.myCdbl(dt.Rows(0)("Emp_Refrence"))
            obj.Agency_Refrence = clsCommon.myCdbl(dt.Rows(0)("Agency_Refrence"))
            obj.Emp_Code = clsCommon.myCstr(dt.Rows(0)("Emp_Code"))
            obj.Handicaped_Detail = clsCommon.myCstr(dt.Rows(0)("Handicaped_Detail"))
            obj.Blood_Group = clsCommon.myCstr(dt.Rows(0)("Blood_Group"))
            obj.Bank_Code = clsCommon.myCstr(dt.Rows(0)("Bank_Code"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Account_No = clsCommon.myCstr(dt.Rows(0)("Account_No"))
            obj.From_Location_Code = clsCommon.myCstr(dt.Rows(0)("From_Location_Code"))
            obj.To_location_Code = clsCommon.myCstr(dt.Rows(0)("To_location_Code"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Preferred_Location_Code = clsCommon.myCstr(dt.Rows(0)("Preferred_Location_Code"))
            obj.Current_Gross_Salary = clsCommon.myCdbl(dt.Rows(0)("Current_Gross_Salary"))
            obj.Total_CTC = clsCommon.myCdbl(dt.Rows(0)("Total_CTC"))
            obj.Performance_By = clsCommon.myCstr(dt.Rows(0)("Performance_By"))
            obj.Is_Fresher = clsCommon.myCdbl(dt.Rows(0)("Is_Fresher"))
            obj.Is_Handicaped = clsCommon.myCdbl(dt.Rows(0)("Is_Handicaped"))
            obj.Relocation = clsCommon.myCdbl(dt.Rows(0)("Relocation"))
            obj.Agency_Code = clsCommon.myCstr(dt.Rows(0)("Agency_Code"))
            obj.DocName = clsCommon.myCstr(dt.Rows(0)("DocName"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            obj.ShortListed = clsCommon.myCdbl(dt.Rows(0)("Short"))
            obj.Rejected = clsCommon.myCdbl(dt.Rows(0)("Rejected"))
            obj.ObjList = ClsAppQualificationDetail.GetData(obj.APPLICANT_CODE, trans)
            obj.ObjListChk = ClsAppCheckListDetail.GetData(obj.APPLICANT_CODE, trans)
            obj.ObjListEmp = ClsAppEmpHisListDetail.GetData(obj.APPLICANT_CODE, trans)
            obj.ObjListFamily = ClsAppFamilyListDetail.GetData(obj.APPLICANT_CODE, trans)
            'obj.APPLICANT_CODE = clsCommon.myCstr(dt.Rows(0)("Round_Code"))
            'obj.Description = clsCommon.myCstr(dt.Rows(0)("Round_Name"))
            'obj.ObjList = ClsRoundDetail.GetData(obj.APPLICANT_CODE, trans)
        End If
        Return obj
    End Function
    '' ------------------------------------------------------ GET FINDER FOR USER CONTROL ----------------------------------------------- ''
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsApplicantEntry
        Return GetPostedData(strCode, NavType, Nothing)
    End Function
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsApplicantEntry
        Dim obj As ClsApplicantEntry = Nothing

        Dim qry As String = "Select * From TSPL_HR_APPLICANT_ENTRY where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select MIN(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY WHERE POSTED=1 and short=1)"
            Case NavigatorType.Last
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY WHERE POSTED=1 and short=1)"
            Case NavigatorType.Next
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select Min(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY where  Applicant_Code >'" + strCode + "' AND POSTED=1 and short=1 )"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = (select Max(Applicant_Code) from TSPL_HR_APPLICANT_ENTRY where Applicant_Code <'" + strCode + "' AND POSTED=1 and short=1)"
            Case NavigatorType.Current
                qry += " and TSPL_HR_APPLICANT_ENTRY.Applicant_Code = '" + strCode + "' AND POSTED=1 and short=1"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsApplicantEntry()
            obj.APPLICANT_CODE = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.Applicant_Description = clsCommon.myCstr(dt.Rows(0)("Applicant_Description"))
            obj.Applicant_Date = clsCommon.myCDate(dt.Rows(0)("Applicant_Date"))
            obj.Requisition_Code = clsCommon.myCstr(dt.Rows(0)("Requisition_Code"))
            obj.Date_Of_Interview = clsCommon.myCDate(dt.Rows(0)("Date_Of_Interview"))
            obj.Gender = clsCommon.myCstr(dt.Rows(0)("Gender"))
            obj.Salutation = clsCommon.myCstr(dt.Rows(0)("Salutation"))
            obj.First_Name = clsCommon.myCstr(dt.Rows(0)("First_Name"))
            obj.Middle_Name = clsCommon.myCstr(dt.Rows(0)("Middle_Name"))
            obj.Last_Name = clsCommon.myCstr(dt.Rows(0)("Last_Name"))
            obj.Applicant_Date_Of_Birth = clsCommon.myCDate(dt.Rows(0)("Applicant_Date_Of_Birth"))
            obj.Maritial_Status = clsCommon.myCstr(dt.Rows(0)("Maritial_Status"))
            obj.Pan_No = clsCommon.myCstr(dt.Rows(0)("Pan_No"))
            obj.Add1 = clsCommon.myCstr(dt.Rows(0)("Add1"))
            obj.Add2 = clsCommon.myCstr(dt.Rows(0)("Add2"))
            obj.Add3 = clsCommon.myCstr(dt.Rows(0)("Add3"))
            obj.Add4 = clsCommon.myCstr(dt.Rows(0)("Add4"))

            obj.Pin_Code = clsCommon.myCstr(dt.Rows(0)("Pin_Code"))
            obj.Email = clsCommon.myCstr(dt.Rows(0)("Email"))
            obj.TELEPHONE_NO = clsCommon.myCstr(dt.Rows(0)("TELEPHONE_NO"))

        End If
        Return obj
    End Function
    '' ---------------------------------------------------------------------------------------------------------------------------------- ''
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String


            qry = "Delete From TSPL_HR_QUALIFICATION_APPLICANT_ENTRY Where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_HR_CHECK_APPLICANT_ENTRY Where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "Delete From TSPL_HR_EMP_HISTORY_APPLICANT_ENTRY Where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_HR_FAMILY_BACKGROUND_APPLICANT_ENTRY Where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            qry = "Delete From TSPL_HR_APPLICANT_ENTRY Where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If isSaved Then
                trans.Commit()
            Else
                trans.Rollback()
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function DeleteDocData(ByVal strEmpCode As String, ByVal strCode As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If
            Dim qry As String
            qry = " update TSPL_HR_APPLICANT_ENTRY set document_file = null ,docname=null where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function GetDocument(ByVal strEmpCode As String, ByVal strCode As String) As DataTable
        Dim qry As String = " select isnull(DOCUMENT_FILE,0x) As DOCUMENT_FILE,isnull(DocName,'') As DocName  from  TSPL_HR_APPLICANT_ENTRY where Applicant_CODE ='" + strEmpCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim data As Byte() = dt.Rows(0)("DOCUMENT_FILE")
        Return dt
    End Function
    ' ----------------- Get_Gender ------------------------
    Public Shared Function GetGender() As DataTable
        Dim DT_Gender As DataTable = New DataTable
        DT_Gender.Columns.Add("Code", GetType(String))
        DT_Gender.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_Gender.NewRow()
        DR("Name") = "Male"
        DR("Code") = "M"
        DT_Gender.Rows.Add(DR)

        DR = DT_Gender.NewRow()
        DR("Name") = "Female"
        DR("Code") = "F"
        DT_Gender.Rows.Add(DR)
        DT_Gender.AcceptChanges()

        Return DT_Gender
    End Function
    '' ----------------- Get_Salutation ------------------------
    'Public Shared Function GetSal() As DataTable
    '    Dim DT_Sal As DataTable = New DataTable
    '    DT_Sal.Columns.Add("Code", GetType(String))
    '    DT_Sal.Columns.Add("Name", GetType(String))

    '    Dim DR As DataRow = DT_Sal.NewRow()
    '    DR("Name") = "Mr."
    '    DR("Code") = "Mr"
    '    DT_Sal.Rows.Add(DR)

    '    DR = DT_Sal.NewRow()
    '    DR("Name") = "Ms."
    '    DR("Code") = "Ms"
    '    DT_Sal.Rows.Add(DR)

    '    DR = DT_Sal.NewRow()
    '    DR("Name") = "Mrs."
    '    DR("Code") = "Mrs"
    '    DT_Sal.Rows.Add(DR)

    '    DR = DT_Sal.NewRow()
    '    DR("Name") = "Dr."
    '    DR("Code") = "Dr"
    '    DT_Sal.Rows.Add(DR)
    '    DT_Sal.AcceptChanges()

    '    Return DT_Sal
    'End Function
    ' ----------------- Get_Maritial_Status ------------------------
    Public Shared Function GetMS() As DataTable
        Dim DT_MS As DataTable = New DataTable
        DT_MS.Columns.Add("Code", GetType(String))
        DT_MS.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_MS.NewRow()
        DR("Name") = "Married"
        DR("Code") = "M"
        DT_MS.Rows.Add(DR)

        DR = DT_MS.NewRow()
        DR("Name") = "Unmarried"
        DR("Code") = "U"
        DT_MS.Rows.Add(DR)

        DR = DT_MS.NewRow()
        DR("Name") = "Divorced"
        DR("Code") = "D"
        DT_MS.Rows.Add(DR)

        DT_MS.AcceptChanges()

        Return DT_MS
    End Function
End Class
' ============================================== Qualification Details ====================================================

Public Class ClsAppQualificationDetail

#Region "Variables"
    Public APPLICANT_CODE As String = Nothing
    Public COLLEGE_UNIVERSITY As String = Nothing
    Public COURSE_CODE As String = Nothing
    Public Year_Of_Passing As String = Nothing
    Public GRADE_PERCENTAGE As String = Nothing
    Public Higher_Qualification As Integer
    Public Course_Type As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_QUALIFICATION_APPLICANT_ENTRY where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjList As List(Of ClsAppQualificationDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_QUALIFICATION_APPLICANT_ENTRY where APPLICANT_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsAppQualificationDetail In ObjList
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "COLLEGE_UNIVERSITY", obj.COLLEGE_UNIVERSITY)
                clsCommon.AddColumnsForChange(coll, "COURSE_CODE", obj.COURSE_CODE, True)
                clsCommon.AddColumnsForChange(coll, "Year_Of_Passing", clsCommon.GetPrintDate(obj.Year_Of_Passing, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "GRADE_PERCENTAGE", obj.GRADE_PERCENTAGE)
                clsCommon.AddColumnsForChange(coll, "Higher_Qualification", obj.Higher_Qualification)
                clsCommon.AddColumnsForChange(coll, "Course_Type", obj.Course_Type)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_QUALIFICATION_APPLICANT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsAppQualificationDetail)
        Dim obj As ClsAppQualificationDetail = Nothing
        Dim ObjList As New List(Of ClsAppQualificationDetail)
        Dim qry As String = " select *  from TSPL_HR_QUALIFICATION_APPLICANT_ENTRY WHERE APPLICANT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsAppQualificationDetail()
                obj.APPLICANT_CODE = clsCommon.myCstr(dr("APPLICANT_CODE"))
                obj.COLLEGE_UNIVERSITY = clsCommon.myCstr(dr("COLLEGE_UNIVERSITY"))
                obj.COURSE_CODE = clsCommon.myCstr(dr("COURSE_CODE"))
                obj.Year_Of_Passing = clsCommon.myCDate(dr("Year_Of_Passing"))
                obj.GRADE_PERCENTAGE = clsCommon.myCstr(dr("GRADE_PERCENTAGE"))
                obj.Higher_Qualification = IIf(clsCommon.myCdbl(clsCommon.myCdbl(dr("Higher_Qualification"))) = 1, True, False)
                obj.Course_Type = clsCommon.myCstr(dr("Course_Type"))
                ObjList.Add(obj)
            Next
        End If
        Return ObjList
    End Function
End Class
' ============================================== Check List ====================================================
Public Class ClsAppCheckListDetail
#Region "Variables"
    Public APPLICANT_CODE As String = Nothing
    Public Chk_Code As String = Nothing
    Public Is_Received As Double = 0
    Public Is_Manadatory As Double = 0
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_CHECK_APPLICANT_ENTRY where Applicant_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjListChk As List(Of ClsAppCheckListDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_CHECK_APPLICANT_ENTRY where APPLICANT_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            If Not ObjListChk Is Nothing AndAlso ObjListChk.Count > 0 Then
                For Each obj As ClsAppCheckListDetail In ObjListChk
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", strCode)
                    clsCommon.AddColumnsForChange(coll, "Chk_Code", obj.Chk_Code, True)
                    'clsCommon.AddColumnsForChange(coll, "Is_Received", obj.Is_Received)
                    clsCommon.AddColumnsForChange(coll, "Is_Manadatory", obj.Is_Manadatory)
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_CHECK_APPLICANT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
           
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsAppCheckListDetail)
        Dim obj As ClsAppCheckListDetail = Nothing
        Dim ObjListChk As New List(Of ClsAppCheckListDetail)
        Dim qry As String = " select *  from TSPL_HR_CHECK_APPLICANT_ENTRY WHERE APPLICANT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsAppCheckListDetail()
                obj.APPLICANT_CODE = clsCommon.myCstr(dr("APPLICANT_CODE"))
                obj.Chk_Code = clsCommon.myCstr(dr("Chk_Code"))
                'obj.Is_Received = clsCommon.myCdbl(dr("Is_Received"))
                obj.Is_Manadatory = clsCommon.myCdbl(dr("Is_Manadatory"))
                ObjListChk.Add(obj)
            Next
        End If
        Return ObjListChk
    End Function
End Class
' ============================================== Emp History ====================================================
Public Class ClsAppEmpHisListDetail

#Region "Variables"
    Public APPLICANT_CODE As String = Nothing
    Public Organisation_Name As String = Nothing
    Public From_Period As String = Nothing
    Public To_Period As String = Nothing
    Public DESIGNATION_ID As String = Nothing
    Public Till_Date As Integer
    Public Roles_and_Responsibilities As String = Nothing
    Public Reason_for_Seperation As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_EMP_HISTORY_APPLICANT_ENTRY where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjListEmp As List(Of ClsAppEmpHisListDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_EMP_HISTORY_APPLICANT_ENTRY where APPLICANT_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsAppEmpHisListDetail In ObjListEmp
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "Organisation_Name", obj.Organisation_Name)
                clsCommon.AddColumnsForChange(coll, "From_Period", clsCommon.GetPrintDate(obj.From_Period, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Period", clsCommon.GetPrintDate(obj.To_Period, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "DESIGNATION_ID", obj.DESIGNATION_ID, True)
                clsCommon.AddColumnsForChange(coll, "Till_Date", obj.Till_Date)
                clsCommon.AddColumnsForChange(coll, "Roles_and_Responsibilities", obj.Roles_and_Responsibilities)
                clsCommon.AddColumnsForChange(coll, "Reason_for_Seperation", obj.Reason_for_Seperation)
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_EMP_HISTORY_APPLICANT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsAppEmpHisListDetail)
        Dim obj As ClsAppEmpHisListDetail = Nothing
        Dim ObjListEmp As New List(Of ClsAppEmpHisListDetail)
        Dim qry As String = " select *  from TSPL_HR_EMP_HISTORY_APPLICANT_ENTRY WHERE APPLICANT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsAppEmpHisListDetail()
                obj.APPLICANT_CODE = clsCommon.myCstr(dr("APPLICANT_CODE"))
                obj.Organisation_Name = clsCommon.myCstr(dr("Organisation_Name"))
                obj.From_Period = clsCommon.myCDate(dr("From_Period"))
                obj.To_Period = clsCommon.myCDate(dr("To_Period"))
                obj.DESIGNATION_ID = clsCommon.myCstr(dr("DESIGNATION_ID"))
                obj.Till_Date = clsCommon.myCdbl(dr("Till_Date"))
                obj.Roles_and_Responsibilities = clsCommon.myCstr(dr("Roles_and_Responsibilities"))
                obj.Reason_for_Seperation = clsCommon.myCstr(dr("Reason_for_Seperation"))
                ObjListEmp.Add(obj)
            Next
        End If
        Return ObjListEmp
    End Function

End Class
' ============================================== Family Background ====================================================
Public Class ClsAppFamilyListDetail

#Region "Variables"
    Public APPLICANT_CODE As String = Nothing
    Public Relation_Code As String = Nothing
    Public Qualification_Code As String = Nothing
    Public Occupation_Code As String = Nothing
    Public Family_Date_Of_Birth As String = Nothing
#End Region
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False

            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to Delete")
            End If

            Dim qry As String
            qry = " delete from TSPL_HR_FAMILY_BACKGROUND_APPLICANT_ENTRY where APPLICANT_CODE ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    Public Shared Function SaveData(ByVal strCode As String, ByVal ObjListFamily As List(Of ClsAppFamilyListDetail), ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            Dim qry As String = "DELETE FROM TSPL_HR_FAMILY_BACKGROUND_APPLICANT_ENTRY where APPLICANT_CODE = '" & strCode & "'  "
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            For Each obj As ClsAppFamilyListDetail In ObjListFamily
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", strCode)
                clsCommon.AddColumnsForChange(coll, "Relation_Code", obj.Relation_Code, True)
                clsCommon.AddColumnsForChange(coll, "Qualification_Code", obj.Qualification_Code, True)
                clsCommon.AddColumnsForChange(coll, "Occupation_Code", obj.Occupation_Code, True)
                clsCommon.AddColumnsForChange(coll, "Family_Date_Of_Birth", clsCommon.GetPrintDate(obj.Family_Date_Of_Birth, "dd/MMM/yyyy"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_FAMILY_BACKGROUND_APPLICANT_ENTRY", OMInsertOrUpdate.Insert, "", trans)
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of ClsAppFamilyListDetail)
        Dim obj As ClsAppFamilyListDetail = Nothing
        Dim ObjListFamily As New List(Of ClsAppFamilyListDetail)
        Dim qry As String = " select *  from TSPL_HR_FAMILY_BACKGROUND_APPLICANT_ENTRY WHERE APPLICANT_CODE = '" + strCode + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New ClsAppFamilyListDetail()
                obj.APPLICANT_CODE = clsCommon.myCstr(dr("APPLICANT_CODE"))
                obj.Qualification_Code = clsCommon.myCstr(dr("Qualification_Code"))
                obj.Occupation_Code = clsCommon.myCstr(dr("Occupation_Code"))
                obj.Relation_Code = clsCommon.myCstr(dr("Relation_Code"))
                obj.Family_Date_Of_Birth = clsCommon.myCDate(dr("Family_Date_Of_Birth"))
                ObjListFamily.Add(obj)
            Next
        End If
        Return ObjListFamily
    End Function

End Class