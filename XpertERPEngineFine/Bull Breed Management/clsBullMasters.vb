Imports System.Data.SqlClient

Public Class clsBullMasters
    Public Bull_Code As String = Nothing
    Public Species_Code As String = Nothing
    Public Category_Code As String = Nothing
    Public Sub_Category_Code As String = Nothing
    Public SS_Centre_Code As String = Nothing
    Public Breed_Code As String = Nothing
    Public Shed_Code As String = Nothing
    Public Pen_Code As String = Nothing
    Public Status_Code As String = Nothing
    Public Sub_Status_Code As String = Nothing
    Public is_Semen As Boolean = False
    Public RadioButton2 As String = Nothing
    Public RadioButton1 As String = Nothing
    Public Exotic_Blood_Per As String = Nothing
    Public Bull_Book_Value As String = Nothing
    Public Country_Code As String = Nothing
    Public Digit_Bull_Id As String = Nothing
    Public Prev_Bull_Id As String = Nothing
    Public Date_Of_Birth As Date
    Public Registration_Date As Date
    Public SS_Bull_Id As String = Nothing
    Public Status_Changed_Date As Date
    Public Bull_Alia_Name As String = Nothing
    Public Exit_Date As Date
    Public Bull_Rating As String = Nothing
    Public Dam_Location_Yeild As String = Nothing
    Public Bull_source_Printing_Straws As String = Nothing
    Public Bull_RFID As String = Nothing
    Public Remark As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As DateTime
    Public Modified_By As String = Nothing
    Public Modified_Date As DateTime
    Public Source_Code As String = Nothing
    Public Source_Address As String = Nothing
    Public Owner_Of_Bull As String = Nothing
    Public Semen_Price_per_Straw As String = Nothing
    Public Doses_Produce_Till_Date As String = Nothing
    Public Capacity_Of_Averege_Monthly_Doses As String = Nothing
    Public Breeding_Value As String = Nothing
    'Public Purchase_Request_No As String = Nothing
    Public Purchase_Date As Date? = Nothing
    Public Purchase_Request_Date As Date? = Nothing
    Public First_Collection_Date As Date
    Public Last_Updated_Date_For_Breeding_Value As Date
    Public Purchase_Request_No = String.Empty
    Public Genetic_Disease_Teasting As Boolean = False
    Public Son_Of_Proven_Sire As Boolean = False
    Public Genomic_Tested_Bulls As Boolean = False
    Public Karyotyping As Boolean = False
    Public SibilingTeasted As Boolean = False
    Public Should_be_shown_in_Sire_Directory As Boolean = False
    Public Proven As Boolean = False
    Public Under_Progeny_Test As Boolean = False
    Public Percentage_Verification As Boolean = False
    Public Is_IBR_Bull As Boolean = False
    Public NO_OF_Doughters As Integer = 0
    Public No_Milking_Done As Integer = 0
    Public Weight_At_Entry As String = Nothing
    Public Birth_Weight As String = Nothing
    Public Dam_Origin As String = Nothing
    Public Total_AI As String = Nothing
    Public Date_of_nominated_mating_initiated As String = Nothing
    Public No_of_males_produced As String = Nothing
    Public Training_Centre As String = Nothing
    Public No_of_male_calves As Integer = 0
    Public Total_Heifer_AI As String = Nothing
    Public No_of_insemination_carried_out As Integer = 0
    Public Pre_Quarantine As String = Nothing
    Public No_under_semen_collection As Integer = 0
    Public Total_Heifer_Conceptions As String = Nothing
    Public No_of_Female_Calves As Integer = 0
    Public No_of_elite_females_currently_pregnant As Integer = 0
    Public Quarntine As String = Nothing
    Public Total_Conceptions As String = Nothing
    Public REARING_Centre As String = Nothing
    Public No_abortions As Integer = 0

    Public No_males_born As Integer = 0

    Public Arr As List(Of clsBullMastersDetail) = Nothing
    Public Picture_Upload As Image






    Public Shared Function getFinder(ByVal strCode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim sql As String = "select * from tspl_bull_master"
        str = clsCommon.ShowSelectForm("BullMaster", sql, "Bull_Code", "", strCode, "Bull_Code", isButtonClicked)
        Return str
    End Function







    Public Function SaveData(ByVal obj As clsBullMasters, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function SaveData(ByVal obj As clsBullMasters, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim IsSaved As Boolean = True
        Try
            IsSaved = True
            Dim StrQry As String = "delete from TSPL_BULL_MASTER where Bull_Code='" + obj.Bull_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)


            Dim coll As New Hashtable()
            'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
            clsCommon.AddColumnsForChange(coll, "Bull_Code", obj.Bull_Code)
            clsCommon.AddColumnsForChange(coll, "Is_Semen", IIf(obj.is_Semen, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Species_Code", obj.Species_Code)
            clsCommon.AddColumnsForChange(coll, "Category_Code", obj.Category_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Category_Code", obj.Sub_Category_Code)
            clsCommon.AddColumnsForChange(coll, "SS_Centre_Code", obj.SS_Centre_Code)
            clsCommon.AddColumnsForChange(coll, "Breed_Code", obj.Breed_Code)
            clsCommon.AddColumnsForChange(coll, "Shed_Code", obj.Shed_Code)
            clsCommon.AddColumnsForChange(coll, "Pen_Code", obj.Pen_Code)
            clsCommon.AddColumnsForChange(coll, "Status_Code", obj.Status_Code)
            clsCommon.AddColumnsForChange(coll, "Sub_Status_Code", obj.Sub_Status_Code)
            clsCommon.AddColumnsForChange(coll, "Exotic_Blood_Per", obj.Exotic_Blood_Per)
            clsCommon.AddColumnsForChange(coll, "Bull_Book_Value", obj.Bull_Book_Value)
            clsCommon.AddColumnsForChange(coll, "Country_Code", obj.Country_Code)
            clsCommon.AddColumnsForChange(coll, "Prev_Bull_Id", obj.Prev_Bull_Id)
            clsCommon.AddColumnsForChange(coll, "SS_Bull_Id", obj.SS_Bull_Id)
            clsCommon.AddColumnsForChange(coll, "Bull_Alia_Name", obj.Bull_Alia_Name)
            clsCommon.AddColumnsForChange(coll, "Bull_Rating", obj.Bull_Rating)
            clsCommon.AddColumnsForChange(coll, "Dam_Location_Yeild", obj.Dam_Location_Yeild)
            clsCommon.AddColumnsForChange(coll, "Bull_source_Printing_Straws", obj.Bull_source_Printing_Straws)
            clsCommon.AddColumnsForChange(coll, "Bull_RFID", obj.Bull_RFID)
            clsCommon.AddColumnsForChange(coll, "Remark", obj.Remark)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Registration_Date", clsCommon.GetPrintDate(obj.Registration_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Date_Of_Birth", clsCommon.GetPrintDate(obj.Date_Of_Birth, "dd/MMM/yyyy"))
            'clsCommon.AddColumnsForChange(coll, "Picture_Upload", obj.Picture_Upload)
            clsCommon.AddColumnsForChange(coll, "Status_Changed_Date", clsCommon.GetPrintDate(obj.Status_Changed_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Exit_Date", clsCommon.GetPrintDate(obj.Exit_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Doses_Produce_Till_Date", obj.Doses_Produce_Till_Date)
            clsCommon.AddColumnsForChange(coll, "Source_Code", obj.Source_Code)
            clsCommon.AddColumnsForChange(coll, "Source_Address", obj.Source_Address)
            clsCommon.AddColumnsForChange(coll, "Owner_Of_Bull", obj.Owner_Of_Bull)
            clsCommon.AddColumnsForChange(coll, "Semen_Price_per_Straw", obj.Semen_Price_per_Straw)
            clsCommon.AddColumnsForChange(coll, "Capacity_Of_Averege_Monthly_Doses", obj.Capacity_Of_Averege_Monthly_Doses)
            clsCommon.AddColumnsForChange(coll, "First_Collection_Date", clsCommon.GetPrintDate(obj.First_Collection_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Last_Updated_Date_For_Breeding_Value", clsCommon.GetPrintDate(obj.Last_Updated_Date_For_Breeding_Value, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Breeding_Value", obj.Breeding_Value)
            clsCommon.AddColumnsForChange(coll, "Purchase_Request_No", clsCommon.myCstr(obj.Purchase_Request_No))
            clsCommon.AddColumnsForChange(coll, "Genetic_Disease_Teasting", IIf(obj.Genetic_Disease_Teasting, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Son_Of_Proven_Sire", IIf(obj.Son_Of_Proven_Sire, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Genomic_Tested_Bulls", IIf(obj.Genomic_Tested_Bulls, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Karyotyping", IIf(obj.Karyotyping, 1, 0))
            clsCommon.AddColumnsForChange(coll, "SibilingTeasted", IIf(obj.SibilingTeasted, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Should_be_shown_in_Sire_Directory", IIf(obj.Should_be_shown_in_Sire_Directory, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Proven", IIf(obj.Proven, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Under_Progeny_Test", IIf(obj.Under_Progeny_Test, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Percentage_Verification", IIf(obj.Percentage_Verification, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_IBR_Bull", IIf(obj.Is_IBR_Bull, 1, 0))
            clsCommon.AddColumnsForChange(coll, "NO_OF_Doughters", obj.NO_OF_Doughters)
            clsCommon.AddColumnsForChange(coll, "Dam_Origin", obj.Dam_Origin)

            clsCommon.AddColumnsForChange(coll, "Birth_Weight", obj.Birth_Weight)
            clsCommon.AddColumnsForChange(coll, "Weight_At_Entry", obj.Weight_At_Entry)
            clsCommon.AddColumnsForChange(coll, "No_Milking_Done", obj.No_Milking_Done)


            If obj.Purchase_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Purchase_Date", clsCommon.GetPrintDate(obj.Purchase_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Purchase_Date", Nothing, True)
            End If
            If obj.Purchase_Request_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Purchase_Request_Date", clsCommon.GetPrintDate(obj.Purchase_Request_Date, "dd/MMM/yyyy"))
            Else
                clsCommon.AddColumnsForChange(coll, "Purchase_Request_Date", Nothing, True)
            End If
            '----progeny detail---
            clsCommon.AddColumnsForChange(coll, "Total_AI", obj.Total_AI)
            clsCommon.AddColumnsForChange(coll, "Date_of_nominated_mating_initiated", obj.Date_of_nominated_mating_initiated)
            clsCommon.AddColumnsForChange(coll, "No_of_males_produced", obj.No_of_males_produced)
            clsCommon.AddColumnsForChange(coll, "Training_Centre", obj.Training_Centre)
            clsCommon.AddColumnsForChange(coll, "No_of_male_calves", obj.No_of_male_calves)
            clsCommon.AddColumnsForChange(coll, "Total_Heifer_AI", obj.Total_Heifer_AI)
            clsCommon.AddColumnsForChange(coll, "No_of_insemination_carried_out", obj.No_of_insemination_carried_out)
            clsCommon.AddColumnsForChange(coll, "Pre_Quarantine", obj.Pre_Quarantine)
            clsCommon.AddColumnsForChange(coll, "No_under_semen_collection", obj.No_under_semen_collection)
            clsCommon.AddColumnsForChange(coll, "Total_Heifer_Conceptions", obj.Total_Heifer_Conceptions)
            clsCommon.AddColumnsForChange(coll, "No_of_Female_Calves", obj.No_of_Female_Calves)
            clsCommon.AddColumnsForChange(coll, "No_of_elite_females_currently_pregnant", obj.No_of_elite_females_currently_pregnant)
            clsCommon.AddColumnsForChange(coll, "Quarntine", obj.Quarntine)
            clsCommon.AddColumnsForChange(coll, "Total_Conceptions", obj.Total_Conceptions)
            clsCommon.AddColumnsForChange(coll, "REARING_Centre", obj.REARING_Centre)
            clsCommon.AddColumnsForChange(coll, "No_abortions", obj.No_abortions)
            clsCommon.AddColumnsForChange(coll, "No_males_born", obj.No_males_born)
            If isNewEntry Then
                'obj.Code = clsERPFuncationality.GetNextCode(trans, DateTime.Now, clsDocType.BullMasters, "", "")
                'If clsCommon.myLen(obj.Code) <= 0 Then
                '    Throw New Exception("Error in Code Generation")
                'End If
                'clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MASTER", OMInsertOrUpdate.Insert, "", trans)
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MASTER", OMInsertOrUpdate.Update, "TSPL_BULL_MASTER.Bull_Code='" + obj.Bull_Code + "'", trans)
            End If
            IsSaved = IsSaved AndAlso clsBullMastersDetail.SaveData(obj.Bull_Code, obj.Arr, trans)
            'IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BULL_MASTER", OMInsertOrUpdate.Update, "TSPL_BULL_MASTER.picture_Upload='" + obj.Picture_Upload + "'", trans)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strCode, trans)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code No. not found to Delete")
            End If
            Dim qry As String = ""
            qry = "delete from TSPL_BULL_MASTER_Detail where Code='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_BULL_MASTER where BULL_Code='" + strCode + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return isSaved
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsBullMasters
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsBullMasters = GetData(strCode, NavType, trans)
            trans.Commit()

            Return obj
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsBullMasters
        Dim obj As clsBullMasters = Nothing

        Try
            Dim strQry As String = "select Bull_Code,Species_Code,Category_Code,Sub_Category_Code,SS_Centre_Code,Breed_Code,Shed_Code,Pen_Code,Status_Code,Sub_Status_Code,Exotic_Blood_Per,Bull_Book_Value,Country_Code,Registration_Date,Prev_Bull_Id
,Bull_RFID	,Remark,	Date_Of_Birth,	SS_Bull_Id	,Status_Changed_Date,	Bull_Alia_Name	,Exit_Date	,Bull_Rating	,Dam_Location_Yeild	,Bull_source_Printing_Straws	,Is_Semen	,Created_By	,Created_Date	,Modified_By,	Modified_Date	,Purchase_Request_No	,Purchase_Date,	Purchase_Request_Date,	Breeding_Value	,Last_Updated_Date_For_Breeding_Value,	Capacity_Of_Averege_Monthly_Doses	,Semen_Price_per_Straw,	First_Collection_Date,	Owner_Of_Bull,	Source_Address	,Source_Code,	Doses_Produce_Till_Date	,Genetic_Disease_Teasting	,Son_Of_Proven_Sire	,Genomic_Tested_Bulls	,Karyotyping,	SibilingTeasted	,Should_be_shown_in_Sire_Directory	,Proven,	Under_Progeny_Test,	Percentage_Verification	,Is_IBR_Bull,	NO_OF_Doughters,	Dam_Origin,	Birth_Weight	,Weight_At_Entry,	No_Milking_Done	,Total_AI,	Date_of_nominated_mating_initiated,	No_of_males_produced	,Training_Centre	,Total_Heifer_AI,	No_of_male_calves,	No_of_insemination_carried_out,	Pre_Quarantine,	No_under_semen_collection,	Total_Heifer_Conceptions	,No_of_Female_Calves	,No_of_elite_females_currently_pregnant,	Quarntine	,Total_Conceptions,	No_abortions
,No_males_born	,REARING_Centre
from TSPL_BULL_MASTER where 1=1  "
            Select Case NavType
                Case NavigatorType.First
                    strQry += " and TSPL_BULL_MASTER.Bull_Code = (select MIN(Bull_Code) from TSPL_BULL_MASTER where 1=1  )"
                Case NavigatorType.Last
                    strQry += " And TSPL_BULL_MASTER.Bull_Code = (Select Max(Bull_Code) from TSPL_BULL_MASTER where 1=1 )"
                Case NavigatorType.Next
                    strQry += " And TSPL_BULL_MASTER.Bull_Code = (Select Min(Bull_Code) from TSPL_BULL_MASTER where Bull_Code>'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Previous
                    strQry += " and TSPL_BULL_MASTER.Bull_Code = (select Max(Bull_Code) from TSPL_BULL_MASTER where Bull_Code<'" + clsCommon.myCstr(strCode) + "' )"
                Case NavigatorType.Current
                    strQry += " and TSPL_BULL_MASTER.Bull_Code = '" + clsCommon.myCstr(strCode) + "' "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New clsBullMasters()
                obj.Bull_Code = clsCommon.myCstr(dt.Rows(0)("Bull_Code"))
                obj.Species_Code = clsCommon.myCstr(dt.Rows(0)("Species_Code"))
                obj.Category_Code = clsCommon.myCstr(dt.Rows(0)("Category_Code"))
                obj.Sub_Category_Code = clsCommon.myCstr(dt.Rows(0)("Sub_Category_Code"))
                obj.SS_Centre_Code = clsCommon.myCstr(dt.Rows(0)("SS_Centre_Code"))
                obj.Breed_Code = clsCommon.myCstr(dt.Rows(0)("Breed_Code"))
                obj.Shed_Code = clsCommon.myCstr(dt.Rows(0)("Shed_Code"))
                obj.Pen_Code = clsCommon.myCstr(dt.Rows(0)("Pen_Code"))
                obj.Status_Code = clsCommon.myCstr(dt.Rows(0)("Status_Code"))
                obj.Sub_Status_Code = clsCommon.myCstr(dt.Rows(0)("Sub_Status_Code"))
                obj.is_Semen = clsCommon.myCdbl(dt.Rows(0)("is_Semen"))
                obj.Exotic_Blood_Per = clsCommon.myCstr(dt.Rows(0)("Exotic_Blood_Per"))
                obj.Bull_Rating = clsCommon.myCstr(dt.Rows(0)("Bull_Rating"))
                obj.Bull_source_Printing_Straws = clsCommon.myCstr(dt.Rows(0)("Bull_source_Printing_Straws"))
                obj.Total_Heifer_Conceptions = clsCommon.myCstr(dt.Rows(0)("Total_Heifer_Conceptions"))
                obj.Bull_Book_Value = clsCommon.myCstr(dt.Rows(0)("Bull_Book_Value"))
                obj.Remark = clsCommon.myCstr(dt.Rows(0)("Remark"))

                obj.Country_Code = clsCommon.myCstr(dt.Rows(0)("Country_Code"))
                'obj.Digit_Bull_Id = clsCommon.myCstr(dt.Rows(0)("Digit_Bull_Id"))
                obj.Prev_Bull_Id = clsCommon.myCstr(dt.Rows(0)("Prev_Bull_Id"))
                obj.Date_Of_Birth = clsCommon.myCDate(dt.Rows(0)("Date_Of_Birth"))
                obj.Registration_Date = clsCommon.myCDate(dt.Rows(0)("Registration_Date"))
                obj.Status_Changed_Date = clsCommon.myCDate(dt.Rows(0)("Status_Changed_Date"))
                obj.Exit_Date = clsCommon.myCDate(dt.Rows(0)("Exit_Date"))
                obj.Bull_RFID = clsCommon.myCstr(dt.Rows(0)("Bull_RFID"))
                obj.SS_Bull_Id = clsCommon.myCstr(dt.Rows(0)("SS_Bull_Id"))
                obj.Bull_Alia_Name = clsCommon.myCstr(dt.Rows(0)("Bull_Alia_Name"))
                obj.Bull_Rating = clsCommon.myCstr(dt.Rows(0)("Bull_Rating"))
                obj.Dam_Location_Yeild = clsCommon.myCstr(dt.Rows(0)("Dam_Location_Yeild"))
                obj.Bull_source_Printing_Straws = clsCommon.myCstr(dt.Rows(0)("Bull_source_Printing_Straws"))
                obj.Purchase_Request_No = clsCommon.myCstr(dt.Rows(0)("Purchase_Request_No"))
                obj.Purchase_Date = clsCommon.myCDate(dt.Rows(0)("Purchase_Date"))
                obj.Purchase_Request_Date = clsCommon.myCDate(dt.Rows(0)("Purchase_Request_Date"))
                obj.Breeding_Value = clsCommon.myCstr(dt.Rows(0)("Breeding_Value"))
                obj.Last_Updated_Date_For_Breeding_Value = clsCommon.myCDate(dt.Rows(0)("Last_Updated_Date_For_Breeding_Value"))
                obj.Capacity_Of_Averege_Monthly_Doses = clsCommon.myCstr(dt.Rows(0)("Capacity_Of_Averege_Monthly_Doses"))
                obj.Semen_Price_per_Straw = clsCommon.myCstr(dt.Rows(0)("Semen_Price_per_Straw"))
                obj.First_Collection_Date = clsCommon.myCDate(dt.Rows(0)("First_Collection_Date"))
                obj.Owner_Of_Bull = clsCommon.myCstr(dt.Rows(0)("Owner_Of_Bull"))
                obj.Source_Address = clsCommon.myCstr(dt.Rows(0)("Source_Address"))
                obj.Source_Code = clsCommon.myCstr(dt.Rows(0)("Source_Code"))
                obj.Doses_Produce_Till_Date = clsCommon.myCstr(dt.Rows(0)("Doses_Produce_Till_Date"))
                obj.Genetic_Disease_Teasting = clsCommon.myCdbl(dt.Rows(0)("Genetic_Disease_Teasting"))
                obj.Son_Of_Proven_Sire = clsCommon.myCdbl(dt.Rows(0)("Son_Of_Proven_Sire"))
                obj.Genomic_Tested_Bulls = clsCommon.myCdbl(dt.Rows(0)("Genomic_Tested_Bulls"))
                obj.Karyotyping = clsCommon.myCdbl(dt.Rows(0)("Karyotyping"))
                obj.SibilingTeasted = clsCommon.myCdbl(dt.Rows(0)("SibilingTeasted"))
                obj.Should_be_shown_in_Sire_Directory = clsCommon.myCdbl(dt.Rows(0)("Should_be_shown_in_Sire_Directory"))
                obj.Should_be_shown_in_Sire_Directory = clsCommon.myCdbl(dt.Rows(0)("Should_be_shown_in_Sire_Directory"))
                obj.Proven = clsCommon.myCdbl(dt.Rows(0)("Proven"))
                obj.Under_Progeny_Test = clsCommon.myCdbl(dt.Rows(0)("Under_Progeny_Test"))
                obj.Percentage_Verification = clsCommon.myCdbl(dt.Rows(0)("Percentage_Verification"))
                obj.Is_IBR_Bull = clsCommon.myCdbl(dt.Rows(0)("Is_IBR_Bull"))
                obj.NO_OF_Doughters = clsCommon.myCdbl(dt.Rows(0)("NO_OF_Doughters"))
                obj.Dam_Origin = clsCommon.myCdbl(dt.Rows(0)("Dam_Origin"))
                obj.Birth_Weight = clsCommon.myCstr(dt.Rows(0)("Birth_Weight"))
                obj.Weight_At_Entry = clsCommon.myCstr(dt.Rows(0)("Weight_At_Entry"))
                obj.No_Milking_Done = clsCommon.myCstr(dt.Rows(0)("No_Milking_Done"))
                obj.Total_AI = clsCommon.myCstr(dt.Rows(0)("Total_AI"))
                obj.Date_of_nominated_mating_initiated = clsCommon.myCstr(dt.Rows(0)("Date_of_nominated_mating_initiated"))
                obj.No_of_males_produced = clsCommon.myCstr(dt.Rows(0)("No_of_males_produced"))

                obj.Training_Centre = clsCommon.myCstr(dt.Rows(0)("Training_Centre"))
                obj.Total_Heifer_AI = clsCommon.myCstr(dt.Rows(0)("Total_Heifer_AI"))
                obj.No_of_male_calves = clsCommon.myCdbl(dt.Rows(0)("No_of_male_calves"))
                obj.No_of_insemination_carried_out = clsCommon.myCdbl(dt.Rows(0)("No_of_insemination_carried_out"))
                obj.Pre_Quarantine = clsCommon.myCdbl(dt.Rows(0)("Pre_Quarantine"))
                obj.No_under_semen_collection = clsCommon.myCdbl(dt.Rows(0)("No_under_semen_collection"))
                obj.No_of_Female_Calves = clsCommon.myCdbl(dt.Rows(0)("No_of_Female_Calves"))
                obj.No_of_elite_females_currently_pregnant = clsCommon.myCdbl(dt.Rows(0)("No_of_elite_females_currently_pregnant"))
                obj.Quarntine = clsCommon.myCstr(dt.Rows(0)("Quarntine"))
                obj.Total_Conceptions = clsCommon.myCstr(dt.Rows(0)("Total_Conceptions"))
                obj.No_abortions = clsCommon.myCstr(dt.Rows(0)("No_abortions"))
                obj.No_males_born = clsCommon.myCstr(dt.Rows(0)("No_males_born"))
                obj.REARING_Centre = clsCommon.myCstr(dt.Rows(0)("REARING_Centre"))
                strQry = "select * from tspl_bull_master left outer join tspl_bull_master_detail  on tspl_bull_master_detail.code=tspl_bull_master.Bull_Code where bull_code='" + obj.Bull_Code + "' "
                dt = clsDBFuncationality.GetDataTable(strQry, trans)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    obj.Arr = New List(Of clsBullMastersDetail)
                    For Each dr As DataRow In dt.Rows
                        Dim objtr As New clsBullMastersDetail
                        objtr.Code = clsCommon.myCstr(dr("code"))

                        objtr.Relation = clsCommon.myCstr(dr("Relation"))
                        objtr.Taq_No = clsCommon.myCdbl(dr("Taq_No"))
                        objtr.First_Standard_Location_Yield = clsCommon.myCstr(dr("First_Standard_Location_Yield"))
                        objtr.Fat_Percent = clsCommon.myCdbl(dr("Fat_Percent"))
                        objtr.Snf_Percent = clsCommon.myCdbl(dr("Snf_Percent"))
                        objtr.Protien = clsCommon.myCstr(dr("Protien"))
                        objtr.Lactose = clsCommon.myCstr(dr("Lactose"))
                        objtr.Date_Of_Pedigree_Information_Update = clsCommon.myCDate(dr("Date_Of_Pedigree_Information_Update"))
                        objtr.SCC = clsCommon.myCstr(dr("SCC"))
                        objtr.MUN = clsCommon.myCstr(dr("MUN"))
                        objtr.Best_Standard_Location_Yield = clsCommon.myCstr(dr("Best_Standard_Location_Yield"))
                        objtr.Fat_Percent = clsCommon.myCdbl(dr("Fat_Percent"))
                        objtr.Lact_No = clsCommon.myCdbl(dr("Lact_No"))
                        objtr.SNF_IN_KG = clsCommon.myCdbl(dr("SNF_IN_KG"))

                        obj.Arr.Add(objtr)
                    Next
                End If










            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return obj
    End Function

End Class

Public Class clsBullMastersDetail

    Public Code As String = Nothing
    Public Relation As String = Nothing
    Public Taq_No As String = Nothing
    Public First_Standard_Location_Yield As String = Nothing
    Public Protien As String = Nothing
    Public Lactose As String = Nothing
    Public Date_Of_Pedigree_Information_Update As Date
    Public MUN As String = Nothing
    Public SCC As String = Nothing
    Public Best_Standard_Location_Yield As String = Nothing
    Public Fat_Percent As Decimal = 0
    Public Snf_Percent As Decimal = 0
    Public Lact_No As Decimal = 0
    Public FAT_IN_KG As Decimal = 0
    Public SNF_IN_KG As Decimal = 0

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBullMastersDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsBullMastersDetail In Arr
                Dim colm As New Hashtable()
                clsCommon.AddColumnsForChange(colm, "Relation", obj.Relation)
                clsCommon.AddColumnsForChange(colm, "Code", strDocNo)
                clsCommon.AddColumnsForChange(colm, "Taq_No", obj.Taq_No)
                clsCommon.AddColumnsForChange(colm, "First_Standard_Location_Yield", obj.First_Standard_Location_Yield)
                clsCommon.AddColumnsForChange(colm, "Fat_Percent", obj.Fat_Percent)
                clsCommon.AddColumnsForChange(colm, "Snf_Percent", obj.Snf_Percent)
                clsCommon.AddColumnsForChange(colm, "Protien", obj.Protien)
                clsCommon.AddColumnsForChange(colm, "Lactose", obj.Lactose)
                clsCommon.AddColumnsForChange(colm, "Date_Of_Pedigree_Information_Update", clsCommon.GetPrintDate(obj.Date_Of_Pedigree_Information_Update, "dd/MMM/yyyy"))

                ' clsCommon.AddColumnsForChange(colm, "Date_Of_Pedigree_Information_Update", obj.Date_Of_Pedigree_Information_Update)
                clsCommon.AddColumnsForChange(colm, "SCC", obj.SCC)
                clsCommon.AddColumnsForChange(colm, "MUN", obj.MUN)
                clsCommon.AddColumnsForChange(colm, "Best_Standard_Location_Yield", obj.Best_Standard_Location_Yield)
                clsCommon.AddColumnsForChange(colm, "Lact_No", obj.Lact_No)
                clsCommon.AddColumnsForChange(colm, "FAT_IN_KG", obj.FAT_IN_KG)
                clsCommon.AddColumnsForChange(colm, "SNF_IN_KG", obj.SNF_IN_KG)

                clsCommonFunctionality.UpdateDataTable(colm, "TSPL_BULL_MASTER_Detail", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
End Class