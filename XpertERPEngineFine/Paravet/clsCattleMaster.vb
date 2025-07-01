Imports System.Data.SqlClient
Imports common
Public Class clsCattleMaster
#Region "Variables"
    Public Registration_No As String = Nothing
    Public Registration_Desc As String = Nothing
    Public Registration_Date As Date? = Nothing
    Public Registration_Charge As Decimal = Nothing
    Public Tag_Id As String = Nothing
    Public Cattle_Code As String = Nothing
    Public Cattle_Status As String = Nothing
    Public DOB As Date = Nothing
    Public Cattle_In_Age As String = Nothing
    Public Gender As String = Nothing
    Public Type As String = Nothing
    Public NDDB_Code As String = Nothing
    Public Mother_Code As String = Nothing
    Public Father_Code As String = Nothing
    Public Farmer_Id As String = Nothing
    Public Cattle_Color_Code As String = Nothing
    Public Cattle_Type_Code As String = Nothing
    Public Bred_Type_Code As String = Nothing
    Public PMC_Code As String = Nothing
    Public MCC_Code As String = Nothing
    Public Head_Office As String = Nothing
    Public Zone_Code As String = Nothing
    Public Region_Code As String = Nothing
    Public Area_Code As String = Nothing
    Public Branch_Code As String = Nothing
    Public Insurance_No As String = Nothing
    Public Insurance_Date_To As Date? = Nothing
    Public Insurance_Date_From As Date? = Nothing
    Public Milk_Qty_Per_Day As String = Nothing
    Public Milk_Fat_Percentage As String = Nothing
    Public Created_Date As Date? = Nothing
    Public Created_By As String = Nothing
    Public Modify_By As String = Nothing
    Public Modify_Date As Date? = Nothing
    Public Comp_Code As String = Nothing
#End Region
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " select Registration_No as [Code], Registration_Desc AS [Description],Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_Cattle_Master "
        str = clsCommon.ShowSelectForm("NDDBCode", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function

    Public Shared Function SaveData(ByVal obj As clsCattleMaster, ByVal isNewEntry As Boolean) As Boolean
        Dim qry As String = ""
        Dim IsSaved As Boolean = False
        Try
            Dim coll As New Hashtable()
            Dim collNDDB As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Registration_Desc", obj.Registration_Desc)
            clsCommon.AddColumnsForChange(coll, "Registration_Date", clsCommon.GetPrintDate(obj.Registration_Date, "dd/MMM/yyyy")) ' Registration_Charge
            clsCommon.AddColumnsForChange(coll, "Registration_Charge", obj.Registration_Charge)
            clsCommon.AddColumnsForChange(coll, "Type", obj.Type)
            clsCommon.AddColumnsForChange(coll, "Tag_Id", obj.Tag_Id)
            clsCommon.AddColumnsForChange(coll, "Cattle_Code", obj.Cattle_Code)
            clsCommon.AddColumnsForChange(collNDDB, "Cattle_Id", obj.Cattle_Code)

            clsCommon.AddColumnsForChange(coll, "Cattle_Status", obj.Cattle_Status)
            clsCommon.AddColumnsForChange(coll, "DOB", clsCommon.GetPrintDate(obj.DOB, "dd/MMM/yyyy"))

            clsCommon.AddColumnsForChange(coll, "Cattle_In_Age", obj.Cattle_In_Age)
            clsCommon.AddColumnsForChange(coll, "Gender", obj.Gender)

            clsCommon.AddColumnsForChange(coll, "NDDB_Code", obj.NDDB_Code)
            clsCommon.AddColumnsForChange(collNDDB, "NDDB_No", obj.NDDB_Code)
            clsCommon.AddColumnsForChange(coll, "Mother_Code", obj.Mother_Code)
            clsCommon.AddColumnsForChange(coll, "Father_Code", obj.Father_Code)
            clsCommon.AddColumnsForChange(coll, "Farmer_Id", obj.Farmer_Id)
            clsCommon.AddColumnsForChange(collNDDB, "Farmer_Id", obj.Farmer_Id)

            clsCommon.AddColumnsForChange(coll, "Cattle_Color_Code", obj.Cattle_Color_Code)
            clsCommon.AddColumnsForChange(coll, "Cattle_Type_Code", obj.Cattle_Type_Code)
            '"select  Cattle_Type_Name from  TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code='" & txtCattleType.Value & "'"
            If clsCommon.myLen(obj.Cattle_Type_Code) > 0 Then
                Dim Cattle_Type_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Cattle_Type_Name from  TSPL_CATTLE_TYPE_MASTER where Cattle_Type_Code='" & obj.Cattle_Type_Code & "'"))
                clsCommon.AddColumnsForChange(collNDDB, "Cattle_Type", Cattle_Type_Name)
            End If

            clsCommon.AddColumnsForChange(coll, "Bred_Type_Code", obj.Bred_Type_Code)
            clsCommon.AddColumnsForChange(coll, "PMC_Code", obj.PMC_Code)
            clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
            clsCommon.AddColumnsForChange(coll, "Head_Office", obj.Head_Office)
            clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code)
            clsCommon.AddColumnsForChange(coll, "Region_Code", obj.Region_Code)
            clsCommon.AddColumnsForChange(coll, "Area_Code", obj.Area_Code)
            clsCommon.AddColumnsForChange(coll, "Branch_Code", obj.Branch_Code)
            clsCommon.AddColumnsForChange(coll, "Insurance_No", obj.Insurance_No)
            clsCommon.AddColumnsForChange(coll, "Insurance_Date_To", clsCommon.GetPrintDate(obj.Insurance_Date_To, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Insurance_Date_From", clsCommon.GetPrintDate(obj.Insurance_Date_From, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Milk_Qty_Per_Day", obj.Milk_Qty_Per_Day)
            clsCommon.AddColumnsForChange(coll, "Milk_Fat_Percentage", obj.Milk_Fat_Percentage)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Registration_No", obj.Registration_No)
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Cattle_Master", OMInsertOrUpdate.Insert, "")
            Else
                IsSaved = clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Cattle_Master", OMInsertOrUpdate.Update, "TSPL_Cattle_Master.Registration_No='" + obj.Registration_No + "'")
            End If
            If clsCommon.myLen(obj.NDDB_Code) > 0 And clsCommon.myLen(obj.Cattle_Code) Then
                IsSaved = clsCommonFunctionality.UpdateDataTable(collNDDB, "TSPL_Paravet_NDDB_Master", OMInsertOrUpdate.Update, "TSPL_Paravet_NDDB_Master.NDDB_No='" + obj.NDDB_Code + "'")
            End If

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return IsSaved
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As clsCattleMaster
        Dim obj As clsCattleMaster = Nothing
        Dim Arr As List(Of clsCattleMaster) = Nothing
        Dim qry As String = " select Registration_No,Registration_Desc,convert(varchar, Registration_Date,103) as Registration_Date,isnull(Registration_Charge,0) as Registration_Charge ,Tag_Id,Cattle_Code,Cattle_Status,convert(varchar,DOB,103) as DOB ,Cattle_In_Age,Gender,Type ,NDDB_Code, Mother_Code,Father_Code, Farmer_Id,Cattle_Color_Code,Cattle_Type_Code,Bred_Type_Code,PMC_Code,MCC_Code,Head_Office,Zone_Code,Region_Code,Area_Code,Branch_Code,Insurance_No,convert (varchar, Insurance_Date_To,103) as Insurance_Date_To,convert(varchar,Insurance_Date_From,103) as Insurance_Date_From,Milk_Qty_Per_Day,Milk_Fat_Percentage,Created_By as [Created By] ,Convert(varchar,Created_Date,103) as [Created Date] ,Modify_By as [Modified By],convert(varchar,Modify_Date,103) as [Modified Date]  from TSPL_Cattle_Master where 2=2 "
        Dim whrclas As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Cattle_Master.Registration_No = (select MIN(Registration_No) from TSPL_Cattle_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Last
                qry += " and TSPL_Cattle_Master.Registration_No  = (select Max(Registration_No) from TSPL_Cattle_Master WHERE 1=1 " + whrclas + " )"
            Case NavigatorType.Current
                qry += " and TSPL_Cattle_Master.Registration_No  = (select TOP 1 Registration_No from TSPL_Cattle_Master WHERE 1=1 " + whrclas + " and Registration_No='" + strCode + "' )"
            Case NavigatorType.Next
                qry += " and TSPL_Cattle_Master.Registration_No  = (select Min(Registration_No) from TSPL_Cattle_Master where Registration_No > '" + strCode + "' " + whrclas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_Cattle_Master.Registration_No  = (select Max(Registration_No) from TSPL_Cattle_Master where Registration_No < '" + strCode + "' " + whrclas + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsCattleMaster()
            obj.Registration_No = clsCommon.myCstr(dt.Rows(0)("Registration_No"))
            obj.Registration_Desc = clsCommon.myCstr(dt.Rows(0)("Registration_Desc"))
            obj.Registration_Date = clsCommon.myCDate(dt.Rows(0)("Registration_Date"))
            obj.Registration_Charge = clsCommon.myCdbl(dt.Rows(0)("Registration_Charge"))
            obj.Tag_Id = clsCommon.myCstr(dt.Rows(0)("Tag_Id"))
            obj.Cattle_Code = clsCommon.myCstr(dt.Rows(0)("Cattle_Code"))
            obj.Cattle_Status = clsCommon.myCstr(dt.Rows(0)("Cattle_Status"))
            obj.DOB = clsCommon.myCDate(dt.Rows(0)("DOB"))
            obj.Cattle_In_Age = clsCommon.myCstr(dt.Rows(0)("Cattle_In_Age"))
            obj.Gender = clsCommon.myCstr(dt.Rows(0)("Gender"))
            obj.Type = clsCommon.myCstr(dt.Rows(0)("Type"))
            obj.NDDB_Code = clsCommon.myCstr(dt.Rows(0)("NDDB_Code"))
            obj.Mother_Code = clsCommon.myCstr(dt.Rows(0)("Mother_Code"))
            obj.Father_Code = clsCommon.myCstr(dt.Rows(0)("Father_Code"))
            obj.Farmer_Id = clsCommon.myCstr(dt.Rows(0)("Farmer_Id"))
            obj.Cattle_Color_Code = clsCommon.myCstr(dt.Rows(0)("Cattle_Color_Code"))
            obj.Cattle_Type_Code = clsCommon.myCstr(dt.Rows(0)("Cattle_Type_Code"))
            obj.Bred_Type_Code = clsCommon.myCstr(dt.Rows(0)("Bred_Type_Code"))
            obj.PMC_Code = clsCommon.myCstr(dt.Rows(0)("PMC_Code"))
            obj.MCC_Code = clsCommon.myCstr(dt.Rows(0)("MCC_Code"))
            obj.Head_Office = clsCommon.myCstr(dt.Rows(0)("Head_Office"))
            obj.Zone_Code = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
            obj.Region_Code = clsCommon.myCstr(dt.Rows(0)("Region_Code"))
            obj.Area_Code = clsCommon.myCstr(dt.Rows(0)("Area_Code"))
            obj.Branch_Code = clsCommon.myCstr(dt.Rows(0)("Branch_Code"))
            obj.Insurance_No = clsCommon.myCstr(dt.Rows(0)("Insurance_No"))
            obj.Insurance_Date_To = clsCommon.myCDate(dt.Rows(0)("Insurance_Date_To"))
            obj.Insurance_Date_From = clsCommon.myCDate(dt.Rows(0)("Insurance_Date_From"))
            obj.Milk_Qty_Per_Day = clsCommon.myCstr(dt.Rows(0)("Milk_Qty_Per_Day"))
            obj.Milk_Fat_Percentage = clsCommon.myCstr(dt.Rows(0)("Milk_Fat_Percentage"))
            
        End If
        Return obj
    End Function
End Class
