Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class ClsHRRaiseTravelRequisition
#Region "Variables"
    Public Travel_Req_Code As String = Nothing
    Public Document_Date As String = Nothing
    Public Is_Domesctic As Integer
    Public Is_International As Integer
    Public Travel_Purpose_Code As String = Nothing
    Public Travel_Request As String = Nothing
    Public Travel_Period_From As String = Nothing
    Public Travel_Period_To As String = Nothing
    Public Travel_Category_Code As String = Nothing
    Public Loc_From_Travel As String = Nothing
    Public Loc_To_Travel As String = Nothing
    Public Departure_Date As String = Nothing
    Public Arrival_Date As String = Nothing
    Public Travel_Mode_Code As String = Nothing

    Public Flight_No_Travel As String = Nothing
    Public Coupon_No_Travel As String = Nothing
    Public Booked_By_Travel As String = Nothing
    Public Booked_By_Name_Code As String = Nothing
    Public Remarks_Travel As String = Nothing
    Public Travel_Class_Code_Travel As String = Nothing

    Public Date_Of_Stay_From_Hotel As String = Nothing
    Public Date_Of_Stay_To_Hotel As String = Nothing
    Public Hotel_Rating_Code_Hotel As String = Nothing
    Public Travel_Room_Type As String = Nothing
    Public Days As String = Nothing
    Public Night As String = Nothing
    Public Booked_By_Name_Hotel As String = Nothing
    Public Booking_For_Code As String = Nothing

    Public Period_From_Car As String = Nothing
    Public Period_To_Car As String = Nothing
    Public Loc_From_Car As String = Nothing
    Public Loc_To_Car As String = Nothing
    Public Travel_Car_Code As String = Nothing
    Public Amount As String = Nothing
    Public Remarks_Car As String = Nothing
    
#End Region
    '----------------Code For Get Finder--------------------------------------------------------------------'
    Public Shared Function GetFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Dim qry As String = " Select Travel_Req_Code AS [Code],Is_Domesctic AS [Is Domesctic],Is_International AS [Is International],Travel_Purpose_Code AS [Travel Purpose],Travel_Request AS [Travel Request],CONVERT (VARCHAR,Travel_Period_From ,103) AS [Travel Period From],CONVERT (VARCHAR,Travel_Period_To ,103) AS [Travel Period To],Travel_Category_Code AS [Travel Category Code],Loc_From_Travel AS [Loc From Travel],Loc_To_Travel As [Loc To Travel],CONVERT(VARCHAR,Departure_Date,103) As [Departure Date],CONVERT (VARCHAR,Arrival_Date,103) AS [Arrival Date],Travel_Mode_Code As [Travel Mode Code],Travel_Class_Code_Travel AS [Travel Class Code Travel],Flight_No_Travel AS [Flight No Travel],Coupon_No_Travel As [Coupon No Travel],Booked_By_Travel As [Booked By Travel],Booked_By_Name_Code AS [Booked By Name Code],Remarks_Travel AS [Remarks Travel],Date_Of_Stay_From_Hotel AS [Date Of Stay From Hotel],Date_Of_Stay_To_Hotel AS [Date Of Stay To Hotel],Period_From_Car AS [Period From Car],Period_From_Car AS [Period To Car],Loc_From_Car AS [Loc From Car],Loc_To_Car AS [Loc To Car],Travel_Car_Code As [Travel Car Code],Amount As Amount,Remarks_Car As [Remarks Car],Created_By AS [Created By],CONVERT(varchar,Created_Date ,103) As [Created Date],Modified_By AS [Modified By],CONVERT(VARCHAR,modified_date,103) AS [Modified Date] From TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY  "
        str = clsCommon.ShowSelectForm("HRRTRFND", qry, "Code", whrcls, curcode, "Code", isButtonClicked)
        Return str
    End Function
    ''
    Public Shared Function SaveData(ByVal obj As ClsHRRaiseTravelRequisition, ByVal strCode As String) As Boolean
        Try
            Dim issaved As Boolean = True

            Dim coll As New Hashtable()

            Dim qry As String = "SELECT Count(*) FROM TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY where Travel_Req_Code= '" & obj.Travel_Req_Code & "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Is_Domesctic", obj.Is_Domesctic)
            clsCommon.AddColumnsForChange(coll, "Is_International", obj.Is_International)
            clsCommon.AddColumnsForChange(coll, "Travel_Purpose_Code", obj.Travel_Purpose_Code)
            clsCommon.AddColumnsForChange(coll, "Travel_Request", obj.Travel_Request)
            clsCommon.AddColumnsForChange(coll, "Travel_Period_From", clsCommon.GetPrintDate(obj.Travel_Period_From, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Travel_Period_To", clsCommon.GetPrintDate(obj.Travel_Period_To, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Travel_Category_Code", obj.Travel_Category_Code, True)

            clsCommon.AddColumnsForChange(coll, "Loc_From_Travel", obj.Loc_From_Travel, True)
            clsCommon.AddColumnsForChange(coll, "Loc_To_Travel", obj.Loc_To_Travel, True)
            clsCommon.AddColumnsForChange(coll, "Departure_Date", clsCommon.GetPrintDate(obj.Departure_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Arrival_Date", clsCommon.GetPrintDate(obj.Arrival_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Travel_Mode_Code", obj.Travel_Mode_Code, True)
            clsCommon.AddColumnsForChange(coll, "Days", obj.Days)
            clsCommon.AddColumnsForChange(coll, "Night", obj.Night)
            clsCommon.AddColumnsForChange(coll, "Flight_No_Travel", obj.Flight_No_Travel)
            clsCommon.AddColumnsForChange(coll, "Coupon_No_Travel", obj.Coupon_No_Travel)
            clsCommon.AddColumnsForChange(coll, "Booked_By_Travel", obj.Booked_By_Travel)
            clsCommon.AddColumnsForChange(coll, "Booked_By_Name_Code", obj.Booked_By_Name_Code, True)
            clsCommon.AddColumnsForChange(coll, "Remarks_Travel", obj.Remarks_Travel)
            clsCommon.AddColumnsForChange(coll, "Travel_Class_Code_Travel", obj.Travel_Class_Code_Travel, True)

            clsCommon.AddColumnsForChange(coll, "Date_Of_Stay_From_Hotel", clsCommon.GetPrintDate(obj.Date_Of_Stay_From_Hotel, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Date_Of_Stay_To_Hotel", clsCommon.GetPrintDate(obj.Date_Of_Stay_To_Hotel, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Hotel_Rating_Code_Hotel", obj.Hotel_Rating_Code_Hotel, True)
            clsCommon.AddColumnsForChange(coll, "Travel_Room_Type", obj.Travel_Room_Type, True)
 
            clsCommon.AddColumnsForChange(coll, "Period_From_Car", clsCommon.GetPrintDate(obj.Period_From_Car, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Period_To_Car", clsCommon.GetPrintDate(obj.Period_To_Car, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Travel_Car_Code", obj.Travel_Car_Code, True)
            clsCommon.AddColumnsForChange(coll, "Loc_From_Car", obj.Loc_From_Car, True)
            clsCommon.AddColumnsForChange(coll, "Loc_To_Car", obj.Loc_To_Car, True)
            clsCommon.AddColumnsForChange(coll, "Amount", obj.Amount)
            clsCommon.AddColumnsForChange(coll, "Booking_For_Code", obj.Booking_For_Code, True)
            clsCommon.AddColumnsForChange(coll, "Remarks_Car", obj.Remarks_Car)

            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)

            If check = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Travel_Req_Code", obj.Travel_Req_Code)
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY", OMInsertOrUpdate.Insert, "")
            Else
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY", OMInsertOrUpdate.Update, "Travel_Req_Code='" + obj.Travel_Req_Code + "'")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetData(ByVal strCode As String, ByVal NavType As NavigatorType) As ClsHRRaiseTravelRequisition
        Try
            Dim obj As ClsHRRaiseTravelRequisition = Nothing
            Dim qry As String = "select * from TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY where 2=2"
            Select Case NavType
                Case NavigatorType.First
                    qry += " and Travel_Req_Code = (select MIN(Travel_Req_Code) from TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY)"
                Case NavigatorType.Last
                    qry += " and Travel_Req_Code = (select Max(Travel_Req_Code) from TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY)"
                Case NavigatorType.Next
                    qry += " and Travel_Req_Code = (select Min(Travel_Req_Code) from TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY WHERE Travel_Req_Code>'" + strCode + "')"
                Case NavigatorType.Previous
                    qry += " and Travel_Req_Code = (select Max(Travel_Req_Code) from TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY WHERE Travel_Req_Code<'" + strCode + "')"
                Case NavigatorType.Current
                    qry += " and Travel_Req_Code = '" + strCode + "'"

            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj = New ClsHRRaiseTravelRequisition()
                obj.Travel_Req_Code = clsCommon.myCstr(dt.Rows(0)("Travel_Req_Code"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
                obj.Is_Domesctic = clsCommon.myCdbl(dt.Rows(0)("Is_Domesctic"))
                obj.Is_International = clsCommon.myCdbl(dt.Rows(0)("Is_International"))
                obj.Travel_Purpose_Code = clsCommon.myCstr(dt.Rows(0)("Travel_Purpose_Code"))
                obj.Travel_Period_From = clsCommon.myCDate(dt.Rows(0)("Travel_Period_From"))
                obj.Travel_Period_To = clsCommon.myCDate(dt.Rows(0)("Travel_Period_To"))
                obj.Travel_Category_Code = clsCommon.myCstr(dt.Rows(0)("Travel_Category_Code"))

                obj.Loc_From_Travel = clsCommon.myCstr(dt.Rows(0)("Loc_From_Travel"))
                obj.Loc_To_Travel = clsCommon.myCstr(dt.Rows(0)("Loc_To_Travel"))
                obj.Departure_Date = clsCommon.myCDate(dt.Rows(0)("Departure_Date"))
                obj.Arrival_Date = clsCommon.myCDate(dt.Rows(0)("Arrival_Date"))
                obj.Travel_Mode_Code = clsCommon.myCstr(dt.Rows(0)("Travel_Mode_Code"))
                obj.Days = clsCommon.myCdbl(dt.Rows(0)("Days"))
                obj.Night = clsCommon.myCdbl(dt.Rows(0)("Night"))
                obj.Flight_No_Travel = clsCommon.myCstr(dt.Rows(0)("Flight_No_Travel"))
                obj.Coupon_No_Travel = clsCommon.myCstr(dt.Rows(0)("Coupon_No_Travel"))
                obj.Booked_By_Travel = clsCommon.myCstr(dt.Rows(0)("Booked_By_Travel"))
                obj.Booked_By_Name_Code = clsCommon.myCstr(dt.Rows(0)("Booked_By_Name_Code"))
                obj.Remarks_Travel = clsCommon.myCstr(dt.Rows(0)("Remarks_Travel"))
                obj.Travel_Class_Code_Travel = clsCommon.myCstr(dt.Rows(0)("Travel_Class_Code_Travel"))

                obj.Date_Of_Stay_From_Hotel = clsCommon.myCDate(dt.Rows(0)("Date_Of_Stay_From_Hotel"))
                obj.Date_Of_Stay_To_Hotel = clsCommon.myCDate(dt.Rows(0)("Date_Of_Stay_To_Hotel"))
                obj.Hotel_Rating_Code_Hotel = clsCommon.myCstr(dt.Rows(0)("Hotel_Rating_Code_Hotel"))
                obj.Booking_For_Code = clsCommon.myCstr(dt.Rows(0)("Booking_For_Code"))

                obj.Period_From_Car = clsCommon.myCDate(dt.Rows(0)("Period_From_Car"))
                obj.Period_To_Car = clsCommon.myCDate(dt.Rows(0)("Period_To_Car"))
                obj.Travel_Car_Code = clsCommon.myCstr(dt.Rows(0)("Travel_Car_Code"))
                obj.Loc_From_Car = clsCommon.myCstr(dt.Rows(0)("Loc_From_Car"))
                obj.Loc_To_Car = clsCommon.myCstr(dt.Rows(0)("Loc_To_Car"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.Remarks_Car = clsCommon.myCstr(dt.Rows(0)("Remarks_Car"))
            End If

            Return obj
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim isSaved As Boolean
        Try
            isSaved = False
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Code not found to delete")
            End If

            Dim qry As String
            qry = "DELETE FROM TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY WHERE Travel_Req_Code ='" + strCode + "'"
            isSaved = clsDBFuncationality.ExecuteNonQuery(qry)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
        Return isSaved
    End Function
    ' ----------------- Get Travel Request ------------------------
    Public Shared Function GetTReq() As DataTable
        Dim DT_TReq As DataTable = New DataTable
        DT_TReq.Columns.Add("Code", GetType(String))
        DT_TReq.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_TReq.NewRow()
        DR("Name") = "Self"
        DR("Code") = "S"
        DT_TReq.Rows.Add(DR)

        DR = DT_TReq.NewRow()
        DR("Name") = "Non Employee"
        DR("Code") = "NE"
        DT_TReq.Rows.Add(DR)
        DT_TReq.AcceptChanges()

        Return DT_TReq
    End Function
    ' ----------------- Get Booked By ------------------------
    Public Shared Function GetBookedBy() As DataTable
        Dim DT_BookBy As DataTable = New DataTable
        DT_BookBy.Columns.Add("Code", GetType(String))
        DT_BookBy.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_BookBy.NewRow()
        DR("Name") = "Company"
        DR("Code") = "C"
        DT_BookBy.Rows.Add(DR)

        DR = DT_BookBy.NewRow()
        DR("Name") = "Self"
        DR("Code") = "S"
        DT_BookBy.Rows.Add(DR)
        DT_BookBy.AcceptChanges()

        Return DT_BookBy
    End Function
End Class
