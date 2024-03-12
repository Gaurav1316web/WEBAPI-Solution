Public Class clsBMCDCSMobile
    Public Document_No As String
    Public Document_Date As DateTime
    Public MCC_Code As String = ""
    Public Route_Code As String
    Public Route_Name As String ''Not a table Column
    Public Tanker_No As String
    Public Vehicle_No As String
    Public Trip_No As Integer
    Public Entered_Qty As Decimal
    Public Entered_FATKg As Decimal
    Public Entered_SNFKg As Decimal
    Public REF_PK_ID As Integer
    'Public Arr_BMCDCS_DCS As List(Of clsBMCDCS_DCS) = Nothing
    Public Arr_BMCDCS_Trip As List(Of clsBMCDCS_Trip) = Nothing
    Public Shared Function GetData(ByVal IDate As Date) As List(Of clsBMCDCSMobile)
        Dim Arr As List(Of clsBMCDCSMobile) = Nothing
        Try
            Dim SettAPKAddPostfunctionality As Boolean = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AndroidMilkCollectionBMCDCS, clsFixedParameterCode.AddPostFunctionality, Nothing)) = 1)
            Dim obj As clsBMCDCSMobile = Nothing
            Dim obj_Trip As New clsBMCDCS_Trip()
            Arr = New List(Of clsBMCDCSMobile)
            Dim strQry As String = "select XXX.* from (
select max(XX.REF_PK_ID) as REF_PK_ID,max(XX.PK_ID) as PK_ID,max(XX.IDate)as Document_Date,max(XX.Route_Code) as Route_Code,max(XX.MCC_Code)as MCC_Code, max(XX.Vehicle_No) as Vehicle_No,sum(XX.Qty) as Qty,sum(XX.FATKG)as FATKG, sum(XX.SNFKG) as SNFKG,XX.Trip_No from ( 
select TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID as PK_ID,TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID as REF_PK_ID, TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code,TSPL_MILK_COLLECTION_BMCDCS.IDate,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code, TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No, TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty,TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG 
from TSPL_MILK_COLLECTION_BMCDCS
left join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID 
where convert ( date, TSPL_MILK_COLLECTION_BMCDCS.IDate, 103 ) = convert (date, '" + clsCommon.GetPrintDate(IDate, "dd/MMM/yyyy") + "', 103) "
            If SettAPKAddPostfunctionality Then
                strQry += " and 2 = ( case when TSPL_MILK_COLLECTION_BMCDCS.Status=1 then 2 else 3 end )  "
            End If
            strQry += " ) XX group by XX.Vehicle_No,XX.Route_Code,XX.Trip_No 
)XXX where not exists(select * from TSPL_MILK_COLLECTION_MCC_DETAIL where TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=XXX.PK_ID )"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                For Each dr As DataRow In dt.Rows
                    obj = New clsBMCDCSMobile()
                    obj.REF_PK_ID = clsCommon.myCDecimal(dr("REF_PK_ID"))
                    obj.MCC_Code = clsCommon.myCstr(dr("MCC_Code"))
                    obj.Document_Date = clsCommon.GetPrintDate(dr("Document_Date"), "dd/MMM/yyyy")
                    obj.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                    obj.Tanker_No = GetTrankerNO(clsCommon.myCstr(dr("Route_Code")))
                    obj.Vehicle_No = clsCommon.myCstr(dr("Vehicle_No"))
                    obj.Entered_Qty = clsCommon.myCDecimal(dr("Qty"))
                    obj.Entered_FATKg = clsCommon.myCDecimal(dr("FATKG"))
                    obj.Entered_SNFKg = clsCommon.myCDecimal(dr("SNFKG"))
                    obj.Trip_No = clsCommon.myCDecimal(dr("Trip_No"))
                    obj.Arr_BMCDCS_Trip = clsBMCDCS_Trip.GetBMCDCS_Trip(obj.Route_Code, obj.Document_Date, obj.Trip_No)
                    ' obj.Arr_BMCDCS_DCS = clsBMCDCS_DCS.GetBMCDCS_DCS(obj.REF_PK_ID)
                    Arr.Add(obj)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return Arr
    End Function
    Public Shared Function GetTrankerNO(ByVal Route_Code As String)
        Dim TankerNo As String = ""
        Try
            'Dim strQry As String = "select TSPL_BULK_ROUTE_MASTER.Tanker_No from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + clsCommon.myCstr(Route_Code) + "'"
            Dim strQry As String = "select case when TSPL_BULK_ROUTE_MASTER.Tanker_No is null
then (select TSPL_BULK_ROUTE_MASTER.Tanker_No from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No 
where TSPL_BULK_ROUTE_MASTER.IsDefault=1)
else TSPL_BULK_ROUTE_MASTER.Tanker_No
end as Tanker_No
from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + clsCommon.myCstr(Route_Code) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                TankerNo = dt.Rows(0)("Tanker_No")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return TankerNo
    End Function
End Class
Public Class clsBMCDCS_Trip
    Public REF_PK_ID As Integer = 0
    Public PK_ID As Integer = 0
    Public Vehicle_No As String = ""
    Public Trip_No As Integer = 0
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public FATKG As Decimal = 0
    Public SNFKG As Decimal = 0
    Public Temp As Decimal = 0
    Public Gaze_Reading_Code As String = ""
    Public Gaze_Reading As Decimal = 0
    Public Silo_Capacity As Integer = 0
    Public Sample_No As Integer = 0

    Public MCC_Code As String = ""
    Public Shared Function GetBMCDCS_Trip(ByVal Route_Code As String, ByVal Document_Date As DateTime, ByVal Trip_No As Integer) As List(Of clsBMCDCS_Trip)
        Dim obj As clsBMCDCSMobile = New clsBMCDCSMobile()
        Try
            Dim dt As DataTable
            'Dim obj As clsBMCDCSMobile = New clsBMCDCSMobile()
            Dim strQry As String = "select TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID,TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.FAT,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNF,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Gaze_Reading_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Gaze_Reading,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Silo_Capacity,
TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Temp,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Sample_No from TSPL_MILK_COLLECTION_BMCDCS_TRIP
left join TSPL_MILK_COLLECTION_BMCDCS on TSPL_MILK_COLLECTION_BMCDCS.PK_ID= TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID
where TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code=" + clsCommon.myCstr(Route_Code) + " and TSPL_MILK_COLLECTION_BMCDCS.IDate='" + clsCommon.GetPrintDate(Document_Date) + "' and TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No=" + clsCommon.myCstr(Trip_No)
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr_BMCDCS_Trip = New List(Of clsBMCDCS_Trip)
                Dim Obj_Trip As clsBMCDCS_Trip
                For Each dr As DataRow In dt.Rows
                    Obj_Trip = New clsBMCDCS_Trip
                    Obj_Trip.REF_PK_ID = clsCommon.myCDecimal(dr("REF_PK_ID"))
                    Obj_Trip.PK_ID = clsCommon.myCDecimal(dr("PK_ID"))
                    Obj_Trip.MCC_Code = clsCommon.myCstr(dr("MCC_Code"))
                    Obj_Trip.Vehicle_No = clsCommon.myCstr(dr("Vehicle_No"))
                    Obj_Trip.Trip_No = clsCommon.myCDecimal(dr("Trip_No"))
                    Obj_Trip.Qty = clsCommon.myCDecimal(dr("Qty"))
                    Obj_Trip.FAT = clsCommon.myCDecimal(dr("FAT"))
                    Obj_Trip.SNF = clsCommon.myCDecimal(dr("SNF"))
                    Obj_Trip.FATKG = clsCommon.myCDecimal(dr("FATKG"))
                    Obj_Trip.SNFKG = clsCommon.myCDecimal(dr("SNFKG"))
                    Obj_Trip.Temp = clsCommon.myCDecimal(dr("Temp"))
                    Obj_Trip.Gaze_Reading_Code = clsCommon.myCstr(dr("Gaze_Reading_Code"))
                    Obj_Trip.Gaze_Reading = clsCommon.myCDecimal(dr("Gaze_Reading"))
                    Obj_Trip.Silo_Capacity = clsCommon.myCDecimal(dr("Silo_Capacity"))
                    Obj_Trip.Sample_No = clsCommon.myCDecimal(dr("Sample_No"))
                    obj.Arr_BMCDCS_Trip.Add(Obj_Trip)
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            ' Return 0
        End Try
        Return obj.Arr_BMCDCS_Trip
    End Function
End Class

Public Class clsBMCDCS_DCS_Head
    Public Document_No As String
    Public Document_Date As DateTime
    Public REF_PK_ID As String
    Public MCC_Code As String
    Public Arr_DCSMCCDetails As List(Of clsDCS_MCC_DETAIL) = Nothing
    Public Arr_DCSDetails As List(Of clsBMCDCS_DCS) = Nothing
    Public Shared Function GetDCSData(ByVal IDate As Date)
        Dim obj As clsBMCDCS_DCS_Head = Nothing
        Dim obj_DCS As New clsBMCDCS_DCS()
        'Dim obj_Trip As New clsBMCDCS_Trip()
        Dim strQry As String = String.Empty
        Dim SettAPKAddPostfunctionality As Boolean = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AndroidMilkCollectionBMCDCS, clsFixedParameterCode.AddPostFunctionality, Nothing)) = 1)
        strQry = "select max(XX.REF_PK_ID) as REF_PK_ID,XX.MCC_Code as MCC_Code,max(Document_Date) as Document_Date 
from TSPL_MILK_COLLECTION_BMCDCS_DCS 
left join ( select REF_PK_ID,max(MCC_Code)as MCC_Code,max(IDate) as Document_Date from ( select REF_PK_ID,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TSPL_MILK_COLLECTION_BMCDCS.IDate
from TSPL_MILK_COLLECTION_BMCDCS_TRIP 
left outer join TSPL_MILK_COLLECTION_BMCDCS on TSPL_MILK_COLLECTION_BMCDCS.PK_ID=TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID 
inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID 
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No 
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id
where TSPL_MILK_COLLECTION_BMCDCS.IDate='" + clsCommon.GetPrintDate(IDate) + "' and TSPL_MILK_COLLECTION_MCC.Status=1 and TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id is null 
)x group by REF_PK_ID )XX on XX.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID 
where XX.REF_PK_ID in ( select REF_PK_ID from ( select REF_PK_ID 
from TSPL_MILK_COLLECTION_BMCDCS_TRIP 
left outer join TSPL_MILK_COLLECTION_BMCDCS on TSPL_MILK_COLLECTION_BMCDCS.PK_ID=TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID 
inner join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No 
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
where TSPL_MILK_COLLECTION_BMCDCS.IDate='" + clsCommon.GetPrintDate(IDate) + "' and TSPL_MILK_COLLECTION_MCC.Status=1 and TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.PK_Id is null "
        If SettAPKAddPostfunctionality Then
            strQry += " and 2 = ( case when TSPL_MILK_COLLECTION_BMCDCS.Status=1 then 2 else 3 end )  "
        End If
        strQry += ")x group by REF_PK_ID 
) group by XX.MCC_Code"


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
        Dim lstObj As New List(Of clsBMCDCS_DCS_Head)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                obj = New clsBMCDCS_DCS_Head()
                obj.REF_PK_ID = clsCommon.myCDecimal(dr("REF_PK_ID"))
                obj.Document_Date = clsCommon.GetPrintDate(dr("Document_Date"), "dd/MMM/yyyy")
                obj.MCC_Code = clsCommon.myCstr(dr("MCC_Code"))

                obj.Arr_DCSMCCDetails = clsDCS_MCC_DETAIL.GetDCSMCCDetails(obj.MCC_Code, obj.Document_Date)
                obj.Arr_DCSDetails = clsBMCDCS_DCS.GetBMCDCS_DCS(obj.MCC_Code, obj.Document_Date)
                lstObj.Add(obj)
            Next
        End If
        Return lstObj.ToArray()
    End Function
End Class
Public Class clsDCS_MCC_DETAIL
    Public BMCDCS_Trip_PK_ID As Integer
    Public Shared Function GetDCSMCCDetails(ByVal MCC_Code As String, ByVal Document_Date As DateTime)
        Dim obj As clsBMCDCS_DCS_Head = New clsBMCDCS_DCS_Head()
        'Dim obj_Trip As New clsBMCDCS_Trip()
        Dim strQry As String = String.Empty

        strQry = "Select TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP from
        TSPL_MILK_COLLECTION_MCC_DETAIL
        left join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
        left join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID=TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP
        where MCC_Code='" + clsCommon.myCstr(MCC_Code) + "' and TSPL_MILK_COLLECTION_MCC.Document_Date='" + clsCommon.GetPrintDate(Document_Date) + "'
        and exists(select * from TSPL_MILK_COLLECTION_MCC_DETAIL where TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID ) 
        "


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
        ' Dim lstObj As New List(Of clsBMCDCS_DCS_Head)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Arr_DCSMCCDetails = New List(Of clsDCS_MCC_DETAIL)
            Dim Obj_DCSMCC As clsDCS_MCC_DETAIL
            For Each dr As DataRow In dt.Rows
                Obj_DCSMCC = New clsDCS_MCC_DETAIL
                Obj_DCSMCC.BMCDCS_Trip_PK_ID = clsCommon.myCDecimal(dr("REF_PK_ID_BMCDCS_TRIP"))
                obj.Arr_DCSMCCDetails.Add(Obj_DCSMCC)
            Next
        End If
        Return obj.Arr_DCSMCCDetails
    End Function
End Class
Public Class clsBMCDCS_DCS
    Public REF_PK_ID As Integer = 0
    Public PK_ID As Integer = 0
    Public VLC_Code As String = ""
    Public IShift As String = ""
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public FATKG As Decimal = 0
    Public SNFKG As Decimal = 0
    Public Shared Function GetBMCDCS_DCS(ByVal MCC_Code As String, ByVal Document_Date As DateTime)

        Dim dt As DataTable
        Dim obj As clsBMCDCS_DCS_Head = New clsBMCDCS_DCS_Head()

        Dim strQry = "select TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID,TSPL_MILK_COLLECTION_BMCDCS_DCS.PK_ID,TSPL_MILK_COLLECTION_BMCDCS_DCS.SNFKG,
TSPL_MILK_COLLECTION_BMCDCS_DCS.FATKG,TSPL_MILK_COLLECTION_BMCDCS_DCS.SNF,TSPL_MILK_COLLECTION_BMCDCS_DCS.FAT,TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty,
TSPL_MILK_COLLECTION_BMCDCS_DCS.IShift,TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code
from TSPL_MILK_COLLECTION_BMCDCS_DCS
left outer join TSPL_MILK_COLLECTION_BMCDCS on TSPL_MILK_COLLECTION_BMCDCS.PK_ID=TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID
where TSPL_MILK_COLLECTION_BMCDCS.MCC_Code='" + clsCommon.myCstr(MCC_Code) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate='" + clsCommon.GetPrintDate(Document_Date) + "'"

        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(strQry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj.Arr_DCSDetails = New List(Of clsBMCDCS_DCS)
            Dim Obj_DCS As clsBMCDCS_DCS
            For Each dr As DataRow In dt.Rows
                Obj_DCS = New clsBMCDCS_DCS
                Obj_DCS.REF_PK_ID = clsCommon.myCDecimal(dr("REF_PK_ID"))
                Obj_DCS.PK_ID = clsCommon.myCDecimal(dr("PK_ID"))
                Obj_DCS.VLC_Code = clsCommon.myCstr(dr("VLC_Code"))
                Obj_DCS.IShift = clsCommon.myCstr(dr("IShift"))
                Obj_DCS.Qty = clsCommon.myCDecimal(dr("Qty"))
                Obj_DCS.FAT = clsCommon.myCDecimal(dr("FAT"))
                Obj_DCS.SNF = clsCommon.myCDecimal(dr("SNF"))
                Obj_DCS.FATKG = clsCommon.myCDecimal(dr("FATKG"))
                Obj_DCS.SNFKG = clsCommon.myCDecimal(dr("SNFKG"))
                obj.Arr_DCSDetails.Add(Obj_DCS)
            Next
        End If


        Return obj.Arr_DCSDetails
    End Function
End Class
