Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class clsDemandSheet
#Region "Variable"
    Public DEMAND_Date As Date = Nothing
    Public ShiftType As String = Nothing
    Public Cust_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Set_Zero As Decimal = 0
    Public Item_Code As String = Nothing
    Public Qty As Decimal = 0

    Public Arr As List(Of clsDemandSheetDetails) = Nothing


#End Region
    Public Function SaveData(ByVal obj As clsDemandSheet) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function SaveData(ByVal obj As clsDemandSheet, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim colCount As Decimal = 0
            Dim StrQry As String = "select Count(*) from TSPL_DEMAND_SHEET where convert(date, DEMAND_Date,103)='" + clsCommon.GetPrintDate(obj.DEMAND_Date, "dd/MMM/yyyy") + "' and ShiftType='" + obj.ShiftType + "' and Item_Code='" + obj.Item_Code + "' and Cust_Code='" + obj.Cust_Code + "' and Created_By='" + objCommonVar.CurrentUserCode + "'"
            colCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(StrQry, trans))
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DEMAND_Date", clsCommon.GetPrintDate(obj.DEMAND_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Route_No", obj.Route_No)
            clsCommon.AddColumnsForChange(coll, "Set_Zero", obj.Set_Zero)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If colCount = 0 Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_SHEET", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_SHEET", OMInsertOrUpdate.Update, "TSPL_DEMAND_SHEET.DEMAND_Date='" + clsCommon.GetPrintDate(obj.DEMAND_Date) + "' and TSPL_DEMAND_SHEET.ShiftType='" + obj.ShiftType + "' and TSPL_DEMAND_SHEET.Item_Code='" + obj.Item_Code + "'  and TSPL_DEMAND_SHEET.Cust_Code='" + obj.Cust_Code + "'  and Created_By='" + objCommonVar.CurrentUserCode + "'", trans)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal CurrDate As Date, ByVal Shift As String, ByVal CurrUser As String, ByVal trans As SqlTransaction) As List(Of clsDemandSheet)
        Dim lstobj As List(Of clsDemandSheet) = Nothing
        Try

            lstobj = New List(Of clsDemandSheet)
            Dim obj As clsDemandSheet = Nothing
            Dim StrQry As String = " select distinct Cust_Code from TSPL_Demand_Sheet where DEMAND_Date='" + clsCommon.GetPrintDate(CurrDate) + "' and ShiftType='" + Shift + "' and Created_By='" + CurrUser + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each objdr As DataRow In dt.Rows
                    obj = New clsDemandSheet()

                    obj.Cust_Code = clsCommon.myCstr(objdr("Cust_Code"))

                    StrQry = " select Cust_Code,Route_No,Set_Zero,Item_Code,Qty from TSPL_Demand_Sheet where DEMAND_Date='" + clsCommon.GetPrintDate(CurrDate) + "' and ShiftType='" + Shift + "' and Created_By='" + CurrUser + "' and Cust_Code='" + obj.Cust_Code + "'"
                    Dim dt1 As DataTable = New DataTable()
                    dt1 = clsDBFuncationality.GetDataTable(StrQry, trans)
                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        obj.Arr = New List(Of clsDemandSheetDetails)
                        Dim objTr As clsDemandSheetDetails
                        For Each dr As DataRow In dt1.Rows
                            objTr = New clsDemandSheetDetails

                            objTr.Route_No = clsCommon.myCdbl(dr("Route_No"))
                            objTr.Set_Zero = clsCommon.myCdbl(dr("Set_Zero"))
                            objTr.Cust_Code = clsCommon.myCstr(dr("Cust_Code"))
                            objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                            objTr.Qty = clsCommon.myCdbl(dr("Qty"))
                            obj.Arr.Add(objTr)

                        Next
                    End If
                    lstobj.Add(obj)
                Next

            End If
        Catch ex As Exception

        End Try
        Return lstobj
    End Function

    'Public Shared Function SaveDemandData(ByVal CurrDate As Date, ByVal Shift As String, ByVal CurrUser As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

    '    Try
    '        SaveDemandData(CurrDate, Shift, CurrUser, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    'Public Shared Function SaveDemandData(ByVal CurrDate As Date, ByVal Shift As String, ByVal CurrUser As String, ByVal trans As SqlTransaction) As Boolean
    '    Dim Status As Boolean
    '    '    Try
    '    '        Dim qry As String = "select cust_code,item_code,qty from TSPL_Demand_Sheet 
    '    'where demand_date>='" + clsCommon.GetPrintDate(CurrDate) + "' and demand_date<='" + clsCommon.GetPrintDate(CurrDate) + "'
    '    'and shifttype='" + clsCommon.myCstr(Shift) + "' and cust_code='" + CustCode + "' and created_by='" + CurrUser + "'"
    '    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '    '            For Each dr As DataRow In dt.Rows
    '    '                Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_Customer_Master where cust_code='" + CustCode + "'"))
    '    '                Dim strQry As String = "select TSPL_DEMAND_BOOKING_MASTER.Document_No from TSPL_DEMAND_BOOKING_MASTER
    '    'left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
    '    'where TSPL_DEMAND_BOOKING_MASTER.Document_Date>='" + clsCommon.GetPrintDate(CurrDate) + "' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(CurrDate) + "'
    '    'and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + RouteNo + "' and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code='" + CustCode + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" + Shift + "'"
    '    '                Dim DocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry))
    '    '                If clsCommon.myLen(DocumentNo) > 0 Then
    '    '                    Dim obj As New clsDemandBookingSale()
    '    '                    obj = clsDemandBookingSale.GetData(DocumentNo, NavigatorType.Current, Nothing)
    '    '                    If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
    '    '                        If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
    '    '                            For Each objTr As clsDemandBookingSaleDetail In obj.Arr
    '    '                                If clsCommon.CompairString(CustCode, objTr.Cust_Code) = CompairStringResult.Equal Then
    '    '                                    Dim k As Integer = 1
    '    '                                    For columns = 3 To gv1.Columns.Count - 1
    '    '                                        Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
    '    '                                        k = k + 1
    '    '                                        If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal Then
    '    '                                            objTr.Qty = grow.Cells(columns).Value
    '    '                                            obj.SaveData(obj, False)
    '    '                                        End If
    '    '                                    Next
    '    '                                End If
    '    '                                'obj.SaveData(obj, False)
    '    '                            Next
    '    '                        End If
    '    '                    End If
    '    '                Else
    '    '                    Dim obj1 As New clsDemandBookingSale()
    '    '                    obj1.ShiftType = txtShift.Text
    '    '                    obj1.Location_Code = objCommonVar.CurrLocationCode
    '    '                    obj1.Document_Date = txtDate.Value
    '    '                End If
    '    '            Next
    '    '        End If
    '    '    Catch ex As Exception

    '    '    End Try
    '    Return True

    'End Function
    Public Class clsDemandBookingSaleDetail
#Region "Variable"
        Public Document_No As String = Nothing
        Public Line_No As Integer
        Public Item_Code As String = Nothing
        Public Cust_Code As String = Nothing
        Public Item_Desc As String = Nothing
        Public Unit_code As String = Nothing
        Public TotalCrates_ItemWise As Decimal = 0
        Public TotalLtr_ItemWise As Decimal = 0
        Public ItemNetAmount As Decimal = 0
        Public IsGatePassGenerated As String = "N"
        Public IsTruckSheetGenerated As String = "N"
        Public Is_Posted As String = Nothing
        Public Qty As Double = 0
        Public Rate As Double = 0
        Public Price_Code As String = Nothing
        Public Vehicle_Code As String = ""
        Public ShiftType As String = ""
        Public TR_CODE As String = Nothing
        Public IsItemUpdate As Integer = 0

#End Region


    End Class

End Class
Public Class clsDemandSheetDetails
    Public Cust_Code As String = Nothing
    Public Route_No As String = Nothing
    Public Item_Code As String = Nothing
    Public Set_Zero As Decimal = 0
    Public Qty As Decimal = 0
End Class
Public Class clsUpdateDemandDetails
#Region "Variable"
    Public Document_No As String = Nothing
    Public Line_No As Integer
    Public Item_Code As String = Nothing
    Public Cust_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Unit_code As String = Nothing
    Public TotalCrates_ItemWise As Decimal = 0
    Public TotalLtr_ItemWise As Decimal = 0
    Public ItemNetAmount As Decimal = 0
    Public IsGatePassGenerated As String = "N"
    Public IsTruckSheetGenerated As String = "N"
    Public Is_Posted As String = Nothing
    Public Qty As Double = 0
    Public Rate As Double = 0
    Public Price_Code As String = Nothing
    Public Vehicle_Code As String = ""
    Public ShiftType As String = ""
    Public TR_CODE As String = Nothing
    Public IsItemUpdate As Integer = 0

#End Region
    Public Shared Function InsertNewItem(ByVal obj As clsUpdateDemandDetails, ByVal DocDate As Date) As Boolean

        If clsCommon.myLen(obj.Document_No) > 0 Then

            Dim coll As New Hashtable()
            obj.TR_CODE = clsERPFuncationality.GetNextCode(Nothing, DocDate, clsDocType.Detail, clsDocTransactionType.Detail, "")
            clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_CODE)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Unit_code", obj.Unit_code)
            clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
            clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Rate)
            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
            clsCommon.AddColumnsForChange(coll, "IsItemUpdate", obj.IsItemUpdate)
            clsCommon.AddColumnsForChange(coll, "TotalCrates_ItemWise", obj.TotalCrates_ItemWise)
            clsCommon.AddColumnsForChange(coll, "TotalLtr_ItemWise", obj.TotalLtr_ItemWise)
            clsCommon.AddColumnsForChange(coll, "ItemNetAmount", obj.ItemNetAmount)
            clsCommon.AddColumnsForChange(coll, "IsGatePassGenerated", obj.IsGatePassGenerated)
            clsCommon.AddColumnsForChange(coll, "IsTruckSheetGenerated", obj.IsTruckSheetGenerated)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_BOOKING_DETAIL", OMInsertOrUpdate.Insert, "", Nothing)
        End If


        Return True
    End Function
End Class

Public Class clsDemandHistoryMaster
#Region "Variable"
    Public History_Version As Integer = 0
    Public Demand_No As String = Nothing
    Public Document_No As String = Nothing
    Public Route_No As String = Nothing
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public ShiftType As String = Nothing
    Public History_By As String = Nothing
    Public History_ON As DateTime = Nothing
    Public Arr As List(Of clsDemandHistoryDetail) = Nothing
#End Region
    Public Shared Function GetData(ByVal DocDate As Date, ByVal Shift As String, ByVal Booth As String, ByVal trans As SqlTransaction) As List(Of clsDemandHistoryMaster)
        Dim lstobj As List(Of clsDemandHistoryMaster) = Nothing
        Try
            Dim lstStr As List(Of String) = New List(Of String)
            lstobj = New List(Of clsDemandHistoryMaster)
            Dim obj As clsDemandHistoryMaster = Nothing
            Dim StrQry As String = " select TSPL_BOOKING_MATSER.Against_DemandBooking_No as Demand_No,TSPL_BOOKING_MATSER.Document_No as Document_No,
TSPL_BOOKING_DETAIL.route_no as Route_No,
TSPL_ITEM_MASTER.Item_Code as Item_Name,
 TSPL_BOOKING_DETAIL.Amount_with_Tax as Amount,
TSPL_BOOKING_DETAIL.Booking_Qty as Qty,
TSPL_BOOKING_DETAIL.Unit_code as Unit_Code,
TSPL_BOOKING_MATSER.Modified_By as  History_By,
TSPL_BOOKING_MATSER.Modified_Date as History_ON,
 TSPL_BOOKING_DETAIL.Cust_Code as Cust_Code,
 TSPL_Customer_Master.Customer_Name as Customer_Name,
 TSPL_BOOKING_MATSER.GatePass_Type as ShiftType,
 TSPL_BOOKING_DETAIL.Item_Code,
 TSPL_ITEM_MASTER.Short_Description as Item_Desc
from TSPL_BOOKING_MATSER
left join TSPL_BOOKING_DETAIL on TSPL_BOOKING_MATSER.Document_No=TSPL_BOOKING_DETAIL.Document_No
left join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
left join TSPL_CUSTOMER_MASTER on TSPL_BOOKING_DETAIL.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
where Convert(date,TSPL_BOOKING_MATSER.Document_Date,103)='" + clsCommon.GetPrintDate(DocDate) + "'  And TSPL_BOOKING_MATSER.GatePass_Type='" + Shift + "' and TSPL_BOOKING_DETAIL.Cust_Code='" + Booth + "'  union all 
            Select TSPL_BOOKING_MATSER_Hist_Data.Against_DemandBooking_No As Demand_No,
TSPL_BOOKING_MATSER_Hist_Data.Document_No as Document_No,
TSPL_BOOKING_DETAIL_Hist_Data.route_no as Route_No,
TSPL_ITEM_MASTER.Item_Code as Item_Name,
 TSPL_BOOKING_DETAIL_Hist_Data.Amount_with_Tax as Amount,
TSPL_BOOKING_DETAIL_Hist_Data.Booking_Qty as Qty,
TSPL_BOOKING_DETAIL_Hist_Data.Unit_code as Unit_Code,
TSPL_BOOKING_MATSER_Hist_Data.Modified_By as  History_By,
TSPL_BOOKING_MATSER_Hist_Data.Modified_Date as History_ON,
 TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code as Cust_Code,
 TSPL_Customer_Master.Customer_Name as Customer_Name,
 TSPL_BOOKING_MATSER_Hist_Data.GatePass_Type as ShiftType,
 TSPL_BOOKING_DETAIL_Hist_Data.Item_Code,
 TSPL_ITEM_MASTER.Short_Description as Item_Desc
from TSPL_BOOKING_MATSER_Hist_Data
left join TSPL_BOOKING_DETAIL_Hist_Data on TSPL_BOOKING_MATSER_Hist_Data.Document_No=TSPL_BOOKING_DETAIL_Hist_Data.Document_No
left join TSPL_ITEM_MASTER on TSPL_BOOKING_DETAIL_Hist_Data.Item_Code=TSPL_ITEM_MASTER.Item_Code
left join TSPL_CUSTOMER_MASTER on TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code
where Convert(date,TSPL_BOOKING_MATSER_Hist_Data.Document_Date,103)='" + clsCommon.GetPrintDate(DocDate) + "' and TSPL_BOOKING_MATSER_Hist_Data.GatePass_Type='" + Shift + "' And TSPL_BOOKING_DETAIL_Hist_Data.Cust_Code ='" + Booth + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim HisVersion As Integer = 0
                For Each objdr As DataRow In dt.Rows
                    obj = New clsDemandHistoryMaster()
                    obj.Document_No = clsCommon.myCstr(objdr("Document_No"))
                    If lstStr.Contains(obj.Document_No) Then
                        Continue For
                    End If
                    lstStr.Add(obj.Document_No)
                    obj.History_Version = HisVersion
                    obj.Demand_No = clsCommon.myCstr(objdr("Demand_No"))
                    obj.Route_No = clsCommon.myCstr(objdr("Route_No"))
                    obj.Cust_Code = clsCommon.myCstr(objdr("Cust_Code"))
                    obj.Cust_Name = clsCommon.myCstr(objdr("Customer_Name"))
                    obj.ShiftType = clsCommon.myCstr(objdr("ShiftType"))
                    obj.History_By = clsCommon.myCstr(objdr("History_By"))
                    obj.History_ON = clsCommon.GetPrintDate(objdr("History_ON"), "dd/MMM/yyyy hh:mm tt")
                    Dim hisDoc As String = clsDBFuncationality.getSingleValue("select Document_No from TSPL_BOOKING_MATSER_Hist_Data where Document_No='" + obj.Document_No + "'", trans)
                    If clsCommon.myLen(hisDoc) > 0 Then
                        StrQry = "select Document_No,Cust_Code,Item_Code,Unit_code,Booking_Qty,Amount_with_Tax from TSPL_BOOKING_DETAIL_Hist_Data where Document_No='" + obj.Document_No + "'"
                    Else
                        StrQry = "select Document_No,Cust_Code,Item_Code,Unit_code,Booking_Qty,Amount_with_Tax from TSPL_BOOKING_DETAIL where Document_No='" + obj.Document_No + "'"
                    End If
                    Dim dt1 As DataTable = New DataTable()
                    dt1 = clsDBFuncationality.GetDataTable(StrQry, trans)
                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        obj.Arr = New List(Of clsDemandHistoryDetail)
                        Dim objTr As clsDemandHistoryDetail
                        For Each dr As DataRow In dt1.Rows
                            objTr = New clsDemandHistoryDetail
                            objTr.Document_No = clsCommon.myCdbl(dr("Document_No"))
                            objTr.Cust_Code = clsCommon.myCdbl(dr("Cust_Code"))
                            objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                            objTr.Unit_Code = clsCommon.myCstr(dr("Unit_code"))
                            objTr.Qty = clsCommon.myCdbl(dr("Booking_Qty"))
                            objTr.Amount = clsCommon.myCdbl(dr("Amount_with_Tax"))
                            obj.Arr.Add(objTr)
                        Next

                    End If
                    HisVersion += 1
                    lstobj.Add(obj)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return lstobj
    End Function
End Class
Public Class clsDemandHistoryDetail
#Region "Variable"
    Public Document_No As String = Nothing
    Public Cust_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Unit_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Decimal = 0
    Public Amount As Decimal = 0
#End Region


End Class
