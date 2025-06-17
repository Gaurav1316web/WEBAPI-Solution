Imports System.Data.SqlClient
Public Class clsTenderDetail
#Region "Variables"
    Public DocumentCode As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Name As String = Nothing
    Public Item_Type As String = Nothing
    Public Item_Type_Name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Unit_code As String = Nothing
    Public Location As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public Qty As Double = 0
    Public Discount As Double = 0

    Public Rate As Double = 0
    Public Tax_Exclusive As Boolean = False
    Public Item_Cost As Double = 0
    Public Remarks As String = Nothing
    Public Comments As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderDetail In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type, True)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Discount", obj.Discount)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
                clsCommon.AddColumnsForChange(coll, "Location", obj.Location)
                clsCommon.AddColumnsForChange(coll, "Rate", obj.Rate)
                clsCommon.AddColumnsForChange(coll, "Tax_Exclusive", IIf(obj.Tax_Exclusive, 1, 0))
                clsCommon.AddColumnsForChange(coll, "Item_Cost", obj.Item_Cost)
                clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks, True)
                clsCommon.AddColumnsForChange(coll, "Comments", obj.Comments, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_DETAIL", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetFinder(ByVal strTenderNo As String, ByVal strVendorCode As String, ByVal strLocation As String) As clsTenderDetail
        Dim obj As clsTenderDetail = Nothing
        Dim qry As String = " select TSPL_TENDER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_TENDER_DETAIL.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME,TSPL_TENDER_DETAIL.Unit_code,TSPL_TENDER_DETAIL.Rate,TSPL_TENDER_DETAIL.Discount,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Tax_Exclusive from TSPL_TENDER_DETAIL
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.ITEM_CODE=TSPL_TENDER_DETAIL.Item_Code
left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE=TSPL_TENDER_DETAIL.Item_Type
 where DocumentCode='" + strTenderNo + "' and Vendor_Code='" + strVendorCode + "'and Location='" + strLocation + "'"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TenVedItm", qry)
        If dr IsNot Nothing Then
            obj = New clsTenderDetail()
            obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
            obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
            obj.Item_Type_Name = clsCommon.myCstr(dr("ITEM_TYPE_NAME"))
            obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
            obj.Rate = clsCommon.myCdbl(dr("Rate"))
            obj.Tax_Exclusive = (clsCommon.myCdbl(dr("Tax_Exclusive")) = 1)
            obj.Discount = clsCommon.myCdbl(dr("Discount"))
            obj.Location = clsCommon.myCdbl(dr("Location"))
        End If
        Return obj
    End Function

    Public Shared Function GetItemTypeFinder(ByVal strTenderNo As String, ByVal strVendorCode As String, ByVal strLocation As String) As clsTenderDetail
        Dim obj As clsTenderDetail = Nothing
        Dim qry As String = " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_TENDER_DETAIL.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME,TSPL_TENDER_DETAIL.Unit_code,TSPL_TENDER_DETAIL.Rate,TSPL_TENDER_DETAIL.Discount,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Tax_Exclusive 
from TSPL_TENDER_DETAIL
left outer join TSPL_ITEM_MASTER on  TSPL_ITEM_MASTER.Item_Type=TSPL_TENDER_DETAIL.Item_Type
left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE=TSPL_TENDER_DETAIL.Item_Type and TSPL_ITEM_MASTER.Item_Code is not null
 where DocumentCode='" + strTenderNo + "' and Vendor_Code='" + strVendorCode + "'and Location='" + strLocation + "'  and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_TENDER_DETAIL.Unit_code"
        Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TenVedItm", qry)
        If dr IsNot Nothing Then
            obj = New clsTenderDetail()
            obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
            obj.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
            obj.Item_Type = clsCommon.myCstr(dr("Item_Type"))
            obj.Item_Type_Name = clsCommon.myCstr(dr("ITEM_TYPE_NAME"))
            obj.Unit_code = clsCommon.myCstr(dr("Unit_code"))
            obj.Rate = clsCommon.myCdbl(dr("Rate"))
            obj.Tax_Exclusive = (clsCommon.myCdbl(dr("Tax_Exclusive")) = 1)
            obj.Discount = clsCommon.myCdbl(dr("Discount"))
            obj.Location = clsCommon.myCdbl(dr("Location"))
        End If
        Return obj
    End Function
End Class

Public Class clsTenderSchedule
#Region "Variables"
    Public DocumentCode As String
    Public SNo As Integer
    Public PSNo As Integer
    Public Schedule_No As Integer
    Public From_Date As Date
    Public To_Date As Date
    Public Vendor_Code As String
    Public Location_Code As String
    Public Item_Code As String
    Public Item_Type As String
    Public Schedule_Qty_Per As Decimal
    Public Schedule_Qty As Decimal
    Public Schedule_Tolerance_Qty As Decimal
    Public Schedule_Short_Per As Decimal
    Public Schedule_Short As Decimal
    Public Late_Days As Integer
    Public Extension_Days As Integer
    Public Item_Name As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Location_Name As String = Nothing
    Public Arr As List(Of clsTenderSchedulePenelty) = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsTenderSchedule), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsTenderSchedule In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
                clsCommon.AddColumnsForChange(coll, "PSNo", obj.PSNo)
                clsCommon.AddColumnsForChange(coll, "Schedule_No", obj.Schedule_No)
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code, True)
                clsCommon.AddColumnsForChange(coll, "Item_Type", obj.Item_Type, True)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty_Per", obj.Schedule_Qty_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Qty", obj.Schedule_Qty)
                clsCommon.AddColumnsForChange(coll, "Tolerance_Qty", obj.Schedule_Tolerance_Qty)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short_Per", obj.Schedule_Short_Per)
                clsCommon.AddColumnsForChange(coll, "Schedule_Short", obj.Schedule_Short)
                clsCommon.AddColumnsForChange(coll, "Late_Days", obj.Late_Days)
                clsCommon.AddColumnsForChange(coll, "Extension_Days", obj.Extension_Days)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE", OMInsertOrUpdate.Insert, "", trans)

                Dim PK As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsTenderSchedulePenelty.SaveData(strDocNo, PK, obj.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As List(Of clsTenderSchedule)
        Dim arr As List(Of clsTenderSchedule) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE.* from TSPL_TENDER_SCHEDULE  where TSPL_TENDER_SCHEDULE.DocumentCode='" + clsCommon.myCstr(strDocNo) + "' order by TSPL_TENDER_SCHEDULE.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedule)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedule
                obj.SNo = ii + 1
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.PSNo = clsCommon.myCDecimal(dt.Rows(ii)("PSNo"))
                obj.Schedule_No = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_No"))
                obj.From_Date = clsCommon.myCDate(dt.Rows(ii)("From_Date"))
                obj.To_Date = clsCommon.myCDate(dt.Rows(ii)("To_Date"))
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(ii)("Vendor_Code"))
                obj.Location_Code = clsCommon.myCstr(dt.Rows(ii)("Location_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(ii)("Item_Code"))
                obj.Item_Type = clsCommon.myCstr(dt.Rows(ii)("Item_Type"))
                obj.Schedule_Qty_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty_Per"))
                obj.Schedule_Qty = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Qty"))
                obj.Schedule_Short_Per = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short_Per"))
                obj.Schedule_Tolerance_Qty = clsCommon.myCDecimal(dt.Rows(ii)("Tolerance_Qty"))
                obj.Schedule_Short = clsCommon.myCDecimal(dt.Rows(ii)("Schedule_Short"))
                obj.Late_Days = clsCommon.myCDecimal(dt.Rows(ii)("Late_Days"))
                obj.Extension_Days = clsCommon.myCDecimal(dt.Rows(ii)("Extension_Days"))
                obj.Arr = clsTenderSchedulePenelty.GetData(clsCommon.myCDecimal(dt.Rows(ii)("PK_Id")), False, trans)
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsTenderSchedulePenelty
#Region "Variables"
    Public PK_Id As Integer
    Public DocumentCode As String
    Public Against_Tender_Schedule_PK_Id As Integer
    Public Penalty_Date As Date
    Public Penalty As Decimal
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal AgainstSchedulePKId As Integer, ByVal Arr As List(Of clsTenderSchedulePenelty), ByVal trans As SqlTransaction) As Boolean
        For Each obj As clsTenderSchedulePenelty In Arr
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DocumentCode", strDocNo)
            clsCommon.AddColumnsForChange(coll, "Against_Tender_Schedule_PK_Id", AgainstSchedulePKId)
            clsCommon.AddColumnsForChange(coll, "Penalty_Date", clsCommon.GetPrintDate(obj.Penalty_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Penalty", obj.Penalty)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TENDER_SCHEDULE_PENALTY", OMInsertOrUpdate.Insert, "", trans)
        Next
        Return True
    End Function

    Public Shared Function GetData(ByVal AgainstSchedulePKId As Integer, ByVal AddExtensionDays As Boolean, ByVal trans As SqlTransaction) As List(Of clsTenderSchedulePenelty)
        Dim arr As List(Of clsTenderSchedulePenelty) = Nothing
        Dim qry As String = "select TSPL_TENDER_SCHEDULE_PENALTY.DocumentCode,TSPL_TENDER_SCHEDULE_PENALTY.PK_Id,TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id "
        If AddExtensionDays = True Then
            qry += " ,DATEADD(day,isnull(TSPL_TENDER_SCHEDULE.Extension_Days,0),TSPL_TENDER_SCHEDULE_PENALTY.Penalty_Date) "
        Else
            qry += " ,TSPL_TENDER_SCHEDULE_PENALTY.Penalty_Date "
        End If
        qry += " AS Penalty_Date ,TSPL_TENDER_SCHEDULE_PENALTY.Penalty
         from TSPL_TENDER_SCHEDULE_PENALTY
         left outer join TSPL_TENDER_SCHEDULE on TSPL_TENDER_SCHEDULE.DocumentCode=TSPL_TENDER_SCHEDULE_PENALTY.DocumentCode
         and TSPL_TENDER_SCHEDULE.PK_ID=TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id
         where TSPL_TENDER_SCHEDULE_PENALTY.Against_Tender_Schedule_PK_Id='" + clsCommon.myCstr(AgainstSchedulePKId) + "' order by TSPL_TENDER_SCHEDULE_PENALTY.PK_Id"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsTenderSchedulePenelty)()
            For ii As Integer = 0 To dt.Rows.Count - 1
                Dim obj As New clsTenderSchedulePenelty
                obj.PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("PK_Id"))
                obj.DocumentCode = clsCommon.myCstr(dt.Rows(ii)("DocumentCode"))
                obj.Against_Tender_Schedule_PK_Id = clsCommon.myCDecimal(dt.Rows(ii)("Against_Tender_Schedule_PK_Id"))
                obj.Penalty_Date = clsCommon.myCDate(dt.Rows(ii)("Penalty_Date"))
                obj.Penalty = clsCommon.myCDecimal(dt.Rows(ii)("Penalty"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class