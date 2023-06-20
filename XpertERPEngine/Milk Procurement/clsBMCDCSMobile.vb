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




    Public Arr_BMCDCS_DCS As List(Of clsBMCDCS_DCS) = Nothing
    Public Arr_BMCDCS_Trip As List(Of clsBMCDCS_Trip) = Nothing


    Public Shared Function GetData(ByVal IDate As Date)
        Try


            Dim obj As clsBMCDCSMobile = Nothing
            Dim obj_DCS As New clsBMCDCS_DCS()
            Dim obj_Trip As New clsBMCDCS_Trip()
            Dim strQry As String = String.Empty
            '       
            strQry = "select XXX.* from
(select max(XX.REF_PK_ID) as REF_PK_ID,max(XX.PK_ID) as PK_ID,max(XX.IDate)as Document_Date,max(XX.Route_Code) as Route_Code,max(XX.MCC_Code)as MCC_Code, XX.Vehicle_No,sum(XX.Qty) as Qty,sum(XX.FATKG)as FATKG, sum(XX.SNFKG) as SNFKG,XX.Trip_No
                from ( select TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID as PK_ID,TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID as REF_PK_ID, TSPL_MILK_COLLECTION_BMCDCS.Route_Code,TSPL_MILK_COLLECTION_BMCDCS.IDate,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code, TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No, TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty,TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG from TSPL_MILK_COLLECTION_BMCDCS
            left join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID ) XX where convert ( date, XX.IDate, 103 ) = convert (date, '" + clsCommon.GetPrintDate(IDate, "dd/MMM/yyyy") + "', 103) group by XX.Vehicle_No, XX.Route_Code,XX.Trip_No )XXX
  where not exists(select * from TSPL_MILK_COLLECTION_MCC_DETAIL where TSPL_MILK_COLLECTION_MCC_DETAIL.REF_PK_ID_BMCDCS_TRIP=XXX.PK_ID )"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
            Dim lstObj As New List(Of clsBMCDCSMobile)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                'obj = New clsBMCDCSMobile()

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

                    'obj.Arrive_Time = clsCommon.GetPrintDate(dt.Rows(0)("Arrive_Time"), "dd/MMM/yyyy")
                    'obj.Dispatch_Time = clsCommon.GetPrintDate(dt.Rows(0)("Dispatch_Time"), "dd/MMM/yyyy")
                    'obj.Truck_Sheet_SNo = clsCommon.myCstr(dt.Rows(0)("Truck_Sheet_SNo"))
                    'obj.Last_BMC = clsCommon.myCDecimal(dt.Rows(0)("Last_BMC"))
                    'obj.Last_BMC_Seal_No = clsCommon.myCstr(dt.Rows(0)("Last_BMC_Seal_No"))
                    'obj.Partial_Dispatch = Convert.ToInt32(dt.Rows(0)("Partial_Dispatch"))
                    'obj.Created_By = clsCommon.myCstr(dt.Rows(0)("Created_By"))
                    'obj.Created_Date = clsCommon.GetPrintDate(dt.Rows(0)("Created_Date"), "dd/MMM/yyyy")
                    obj.Arr_BMCDCS_Trip = clsBMCDCS_Trip.GetBMCDCS_Trip(obj.Route_Code, obj.Vehicle_No, obj.Trip_No)
                    obj.Arr_BMCDCS_DCS = clsBMCDCS_DCS.GetBMCDCS_DCS(obj.REF_PK_ID)
                    lstObj.Add(obj)
                Next

            End If



            Return lstObj.ToArray()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return 0
        End Try

    End Function

    Public Shared Function GetTrankerNO(ByVal Route_Code As String)

        Dim strQry As String = "select  TSPL_BULK_ROUTE_MASTER.ROUTE_NAME,TSPL_BULK_ROUTE_MASTER.Tanker_No,TSPL_TANKER_MASTER.TANKER_NAME from TSPL_BULK_ROUTE_MASTER left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_BULK_ROUTE_MASTER.Tanker_No where TSPL_BULK_ROUTE_MASTER.ROUTE_NO='" + clsCommon.myCstr(Route_Code) + "'"
        Dim TankerNo As String = ""
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            TankerNo = dt.Rows(0)("Tanker_No")

        End If
        Return TankerNo
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

    Public Shared Function GetBMCDCS_DCS(ByVal PK_ID As Integer)
        Try


            Dim dt As DataTable
            Dim obj As clsBMCDCSMobile = New clsBMCDCSMobile()

            '            Dim strQry As String = "select max(REF_PK_ID) as REF_PK_ID,max(PK_ID) as PK_ID,VLC_Code,IShift,sum(Qty) as Qty,Sum(FAT) as FAT,sum(SNF) as SNF,sum(FATKG) as FATKG,sum(SNFKG)as SNFKG
            'from TSPL_MILK_COLLECTION_BMCDCS_DCS where TSPL_MILK_COLLECTION_BMCDCS_DCS.Ref_pk_id=" + clsCommon.myCstr(PK_ID) + "
            ' group by TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code, TSPL_MILK_COLLECTION_BMCDCS_DCS.IShift
            '"
            Dim strQry = "select TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID,TSPL_MILK_COLLECTION_BMCDCS_DCS.PK_ID,
TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code,
TSPL_MILK_COLLECTION_BMCDCS_DCS.IShift,
TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty,
TSPL_MILK_COLLECTION_BMCDCS_DCS.FAT,
TSPL_MILK_COLLECTION_BMCDCS_DCS.SNF,
TSPL_MILK_COLLECTION_BMCDCS_DCS.FATKG,
TSPL_MILK_COLLECTION_BMCDCS_DCS.SNFKG from TSPL_MILK_COLLECTION_BMCDCS_DCS where TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=" + clsCommon.myCstr(PK_ID)

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(strQry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr_BMCDCS_DCS = New List(Of clsBMCDCS_DCS)
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


                    obj.Arr_BMCDCS_DCS.Add(Obj_DCS)
                Next
            End If

            Return obj.Arr_BMCDCS_DCS
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return 0
        End Try
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
    Public MCC_Code As String = ""

    Public Shared Function GetBMCDCS_Trip(ByVal Route_Code As String, ByVal Vehical_No As String, ByVal Trip_No As Integer)
        Try
            If clsCommon.CompairString(Vehical_No, "N/A") = CompairStringResult.Equal Then
                Vehical_No = ""

            End If


            Dim dt As DataTable
            Dim obj As clsBMCDCSMobile = New clsBMCDCSMobile()

            Dim strQry As String = "select TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID,TSPL_MILK_COLLECTION_BMCDCS_TRIP.PK_ID,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.FAT,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNF,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG,
TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,
TSPL_MILK_COLLECTION_BMCDCS_TRIP.Temp from TSPL_MILK_COLLECTION_BMCDCS_TRIP
left join TSPL_MILK_COLLECTION_BMCDCS on TSPL_MILK_COLLECTION_BMCDCS.PK_ID= TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID
where TSPL_MILK_COLLECTION_BMCDCS.Route_Code=" + clsCommon.myCstr(Route_Code) + " and TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No='" + Vehical_No + "' and TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No=" + clsCommon.myCstr(Trip_No)

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

                    obj.Arr_BMCDCS_Trip.Add(Obj_Trip)
                Next
            End If

            Return obj.Arr_BMCDCS_Trip
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return 0
        End Try

    End Function
End Class
