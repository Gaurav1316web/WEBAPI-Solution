Imports System.Data.SqlClient
Imports common

Public Class ClsSalaryFitment

#Region "Variables"
    Public Applicant_Code As String = Nothing
    Public CTC_Rs_Month As Double = 0
    Public Fixed_CTC_Rs_Month As Double = 0
    Public Variable_Amount As Double = 0
    Public Variable_Pay_Percentage As Double = 0
    Public Posted As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public arrPayHead As ArrayList = Nothing
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " SELECT TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE AS [Code],Applicant_Description AS [Applicant Description],CONVERT (VARCHAR,Applicant_Date ,103) AS [Applicant Date],Requisition_Code AS [Requisition Code],CASE WHEN Gender='F' THEN 'Female' WHEN Gender ='M' THEN 'Male' END AS [Gender],First_Name + ' ' + Middle_Name + ' ' + Last_Name AS [Applicant Name],CONVERT(VARCHAR,Applicant_Date_Of_Birth,103) As [Applicant Date Of Birth],CONVERT (VARCHAR,Date_Of_Interview,103) AS [Date Of Interview],CASE WHEN Maritial_Status='M' THEN 'Married' WHEN Maritial_Status ='U' THEN 'UnMarried' END As [Maritial Status],Pan_No AS [Pan No],Add1 +' '+Add2 +' '+Add3 +' '+Add4 AS [Address],TELEPHONE_NO As [Phone No],Email,Source_Type_Code AS [Source Type Code],Source_Type_Detail_Code AS [Source Type Detail Code],Is_Fresher AS [Is Fresher],Short AS [Is Shortlisted],Rejected As [Is Rejected],COUNTRY_CODE AS [Country Code],State_Code As [State Code],City_code As [City Code],Pin_Code As [Pin Code],Blood_Group As [Blooad Group],Bank_Code As [Bank Code],Branch_Code As [Branch Code],Location_Code AS [Location Code],Preferred_Location_Code AS [Preferred Location Code],Current_Gross_Salary As [Current Gross Salary],Total_CTC AS [Total CTC],Performance_By As [Performance By] ,Is_Handicaped As [Is Handicaped] FROM TSPL_HR_SALARY_FITMENT LEFT OUTER JOIN TSPL_HR_APPLICANT_ENTRY ON TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE =TSPL_HR_SALARY_FITMENT.APPLICANT_CODE  "
        str = clsCommon.ShowSelectForm("HRSALF", qry, "Code", "TSPL_HR_SALARY_FITMENT.Posted =1 ", curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal obj As ClsSalaryFitment, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Applicant_Code", obj.Applicant_Code, True)
            clsCommon.AddColumnsForChange(coll, "CTC_Rs_Month", obj.CTC_Rs_Month)
            clsCommon.AddColumnsForChange(coll, "Fixed_CTC_Rs_Month", obj.Fixed_CTC_Rs_Month)
            clsCommon.AddColumnsForChange(coll, "Variable_Amount", obj.Variable_Amount)
            clsCommon.AddColumnsForChange(coll, "Variable_Pay_Percentage", obj.Variable_Pay_Percentage)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posted", clsCommon.myCdbl(obj.Posted))

            If clsDBFuncationality.getSingleValue("Select COUNT(*) from TSPL_HR_SALARY_FITMENT WHERE APPLICANT_CODE='" + obj.Applicant_Code + "'") <= 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))

                Dim qry1 As String = "SELECT Count(*) FROM TSPL_HR_SALARY_FITMENT where APPLICANT_CODE= '" & obj.Applicant_Code & "'"
                Dim check As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check = 0 Then
                    IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_SALARY_FITMENT", OMInsertOrUpdate.Insert, "")
                Else
                    Throw New Exception("This Code Is Already Exist")

                End If
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_SALARY_FITMENT", OMInsertOrUpdate.Update, "APPLICANT_CODE='" + obj.Applicant_Code + "' AND Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'")
            End If

            qry = "delete from TSPL_FITMENT_PAYHEAD_MAPPING where APPLICANT_CODE='" + obj.Applicant_Code + "'"
            IsSaved = IsSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)

            IsSaved = IsSaved AndAlso clsPayHeadMapping.SaveData(obj.Applicant_Code, obj.arrPayHead)
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSalaryFitment
        Dim obj As ClsSalaryFitment = Nothing
        Dim Arr As List(Of ClsSalaryFitment) = Nothing
        Dim qry As String = "select * from TSPL_HR_SALARY_FITMENT where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'"
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select TOP 1 APPLICANT_CODE from TSPL_HR_SALARY_FITMENT WHERE 1=1 " + whrclas + " and APPLICANT_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select Min(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT where APPLICANT_CODE > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT where APPLICANT_CODE < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSalaryFitment()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.CTC_Rs_Month = clsCommon.myCdbl(dt.Rows(0)("CTC_Rs_Month"))
            obj.Fixed_CTC_Rs_Month = clsCommon.myCdbl(dt.Rows(0)("Fixed_CTC_Rs_Month"))
            obj.Variable_Amount = clsCommon.myCdbl(dt.Rows(0)("Variable_Amount"))
            obj.Variable_Pay_Percentage = clsCommon.myCdbl(dt.Rows(0)("Variable_Pay_Percentage"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            Dim templist As New ArrayList
            qry = "SELECT TSPL_FITMENT_PAYHEAD_MAPPING.* FROM TSPL_FITMENT_PAYHEAD_MAPPING  where TSPL_FITMENT_PAYHEAD_MAPPING.APPLICANT_CODE='" + strCode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    templist.Add(clsCommon.myCstr(dr("PAY_HEAD_CODE")))
                Next
            End If
            obj.arrPayHead = templist

        End If
        Return obj
    End Function
    '' ---------------------------------------------- Posted Data For Offer Letter ---------------------------------------------
    Public Shared Function GetPostedData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSalaryFitment
        Dim obj As ClsSalaryFitment = Nothing
        Dim Arr As List(Of ClsSalaryFitment) = Nothing
        Dim qry As String = "select * from TSPL_HR_SALARY_FITMENT where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "'"
        Dim whrclas As String = " AND Posted =1 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select MIN(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select TOP 1 APPLICANT_CODE from TSPL_HR_SALARY_FITMENT WHERE 1=1 " + whrclas + " and APPLICANT_CODE='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select Min(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT where APPLICANT_CODE > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_HR_SALARY_FITMENT.APPLICANT_CODE = (select Max(APPLICANT_CODE) from TSPL_HR_SALARY_FITMENT where APPLICANT_CODE < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSalaryFitment()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.CTC_Rs_Month = clsCommon.myCdbl(dt.Rows(0)("CTC_Rs_Month"))
            obj.Fixed_CTC_Rs_Month = clsCommon.myCdbl(dt.Rows(0)("Fixed_CTC_Rs_Month"))
            obj.Variable_Amount = clsCommon.myCdbl(dt.Rows(0)("Variable_Amount"))
            obj.Variable_Pay_Percentage = clsCommon.myCdbl(dt.Rows(0)("Variable_Pay_Percentage"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)


            Dim templist As New ArrayList
            qry = "SELECT TSPL_FITMENT_PAYHEAD_MAPPING.* FROM TSPL_FITMENT_PAYHEAD_MAPPING  where TSPL_FITMENT_PAYHEAD_MAPPING.APPLICANT_CODE='" + strCode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    templist.Add(clsCommon.myCstr(dr("PAY_HEAD_CODE")))
                Next
            End If
            obj.arrPayHead = templist
        End If
        Return obj
    End Function
    '' -------------------------------------------------------------------------------------------------------------------------
    '' ----------------------------------------------------- Nav. Query(=) -----------------------------------------------------
    Public Shared Function GetDataForNav(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsSalaryFitment
        Dim obj As ClsSalaryFitment = Nothing
        Dim Arr As List(Of ClsSalaryFitment) = Nothing
        Dim qry As String = "select * from TSPL_HR_SALARY_FITMENT where Comp_Code= '" + objCommonVar.CurrentCompanyCode + "' AND APPLICANT_CODE= '" + strCode + "'"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsSalaryFitment()
            obj.Applicant_Code = clsCommon.myCstr(dt.Rows(0)("APPLICANT_CODE"))
            obj.CTC_Rs_Month = clsCommon.myCdbl(dt.Rows(0)("CTC_Rs_Month"))
            obj.Fixed_CTC_Rs_Month = clsCommon.myCdbl(dt.Rows(0)("Fixed_CTC_Rs_Month"))
            obj.Variable_Amount = clsCommon.myCdbl(dt.Rows(0)("Variable_Amount"))
            obj.Variable_Pay_Percentage = clsCommon.myCdbl(dt.Rows(0)("Variable_Pay_Percentage"))
            obj.Posted = IIf(clsCommon.myCdbl(dt.Rows(0)("Posted")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)

            Dim templist As New ArrayList
            qry = "SELECT TSPL_FITMENT_PAYHEAD_MAPPING.* FROM TSPL_FITMENT_PAYHEAD_MAPPING  where TSPL_FITMENT_PAYHEAD_MAPPING.APPLICANT_CODE='" + strCode + "'"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    templist.Add(clsCommon.myCstr(dr("PAY_HEAD_CODE")))
                Next
            End If
            obj.arrPayHead = templist
        End If
        Return obj
    End Function
    '' ---------------------------------------------------------------------------------------------------------
    Public Shared Function PostData(ByVal FormId As String, ByVal strDocNo As String) As Boolean

        Try
            Dim isSaved As Boolean = True
            If (clsCommon.myLen(strDocNo) <= 0) Then
                Throw New Exception("Applicant code not found to Post")
            End If
            Dim obj As ClsSalaryFitment = ClsSalaryFitment.GetData(strDocNo, NavigatorType.Current)

            If (obj Is Nothing OrElse clsCommon.myLen(obj.Applicant_Code) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Dim qry = "Update TSPL_HR_SALARY_FITMENT set Posted=1, " & _
            "Posting_Date='" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "',Posted_By ='" + objCommonVar.CurrentUserCode + "'" & _
            " where Applicant_Code='" + strDocNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsPayHeadMapping
#Region "Variables"
    Public PayHead_Code As String = ""
#End Region

    Public Shared Function SaveData(ByVal DocNo As String, ByVal arr As ArrayList) As Boolean
        Try

            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For i As Integer = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "APPLICANT_CODE", DocNo)
                    clsCommon.AddColumnsForChange(coll, "PAY_HEAD_CODE", arr.Item(i))
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_FITMENT_PAYHEAD_MAPPING", OMInsertOrUpdate.Insert, "")
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
