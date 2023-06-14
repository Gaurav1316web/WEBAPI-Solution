Public Class clsBMCDCSMobile

    Public PK_ID As Integer = 0
    Public MCC_Code As String = ""
    Public IDate As Date?
    Public Route_Code As String = ""
    Public Arrive_Time As Date?
    Public Dispatch_Time As Date?
    Public Truck_Sheet_SNo As String = ""
    Public Last_BMC As Integer = 0
    Public Last_BMC_Seal_No As String = ""
    Public Partial_Dispatch As Integer = 0
    Public Created_By As String = ""
    Public Created_Date As Date?
    Public Arr_BMCDCS_DCS As List(Of BMCDCS_DCS) = Nothing
    Public Arr_BMCDCS_Trip As List(Of BMCDCS_Trip) = Nothing

End Class
Public Class BMCDCS_DCS

    Public REF_PK_ID As Integer = 0
    Public PK_ID As Integer = 0
    Public VLC_Code As String = ""
    Public IShift As String = ""
    Public Qty As Decimal = 0
    Public FAT As Decimal = 0
    Public SNF As Decimal = 0
    Public FATKG As Decimal = 0
    Public SNFKG As Decimal = 0

End Class

Public Class BMCDCS_Trip
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

End Class
