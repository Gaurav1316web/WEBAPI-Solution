Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class clsDemandSheet
#Region "Variable"
    Public DEMAND_Date As Date = Nothing
    Public ShiftType As String = Nothing
    Public Cust_Code As String = Nothing
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
            Dim StrQry As String = "select Count(*) from TSPL_DEMAND_SHEET where DEMAND_Date='" + clsCommon.GetPrintDate(obj.DEMAND_Date) + "' and ShiftType='" + obj.ShiftType + "' and Item_Code='" + obj.Item_Code + "' and Cust_Code='" + obj.Cust_Code + "'"
            colCount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(StrQry, trans))
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "DEMAND_Date", clsCommon.GetPrintDate(obj.DEMAND_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ShiftType", obj.ShiftType)
            clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code)
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_SHEET", OMInsertOrUpdate.Update, "TSPL_DEMAND_SHEET.DEMAND_Date='" + clsCommon.GetPrintDate(obj.DEMAND_Date) + "' and TSPL_DEMAND_SHEET.ShiftType='" + obj.ShiftType + "' and TSPL_DEMAND_SHEET.Item_Code='" + obj.Item_Code + "'  and TSPL_DEMAND_SHEET.Cust_Code='" + obj.Cust_Code + "'", trans)
            End If


        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal CurrDate As Date, ByVal Shift As String, ByVal CurrUser As String, ByVal trans As SqlTransaction) As List(Of clsDemandSheet)
        Dim lstobj As List(Of clsDemandSheet) = Nothing
        Try
            'Dim arr As New List(Of String)
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select distinct Item_code from TSPL_Demand_Sheet where DEMAND_Date='" + clsCommon.GetPrintDate(CurrDate) + "' and ShiftType='" + Shift + "' and Created_By='" + CurrUser + "'", trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    For Each dr As DataRow In dt.Rows
            '        arr.Add(clsCommon.myCstr(dr("Item_code")))
            '    Next

            'End If
            'Dim str As String = clsCommon.GetMulcallStringWithComma(Arr)
            lstobj = New List(Of clsDemandSheet)
            Dim obj As clsDemandSheet = Nothing
            Dim StrQry As String = " select distinct Cust_Code from TSPL_Demand_Sheet where DEMAND_Date='" + clsCommon.GetPrintDate(CurrDate) + "' and ShiftType='" + Shift + "' and Created_By='" + CurrUser + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each objdr As DataRow In dt.Rows
                    obj = New clsDemandSheet()

                    obj.Cust_Code = clsCommon.myCstr(objdr("Cust_Code"))

                    StrQry = " select Cust_Code,Set_Zero,Item_Code,Qty from TSPL_Demand_Sheet where DEMAND_Date='" + clsCommon.GetPrintDate(CurrDate) + "' and ShiftType='" + Shift + "' and Created_By='" + CurrUser + "' and Cust_Code='" + obj.Cust_Code + "'"
                    Dim dt1 As DataTable = New DataTable()
                    dt1 = clsDBFuncationality.GetDataTable(StrQry, trans)
                    If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                        obj.Arr = New List(Of clsDemandSheetDetails)
                        Dim objTr As clsDemandSheetDetails
                        For Each dr As DataRow In dt1.Rows
                            objTr = New clsDemandSheetDetails

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
    'Public Shared Function SaveDemandData(ByVal CurrDate As Date, ByVal Shift As String, ByVal CustCode As String, ByVal CurrUser As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

    '    Try
    '        SaveDemandData(CurrDate, Shift, CustCode, CurrUser, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    '    Public Shared Function SaveDemandData(ByVal CurrDate As Date, ByVal Shift As String, ByVal CustCode As String, ByVal CurrUser As String, ByVal trans As SqlTransaction) As Boolean
    '        Dim Status As Boolean
    '        Try
    '            Dim qry As String = "select cust_code,item_code,qty from TSPL_Demand_Sheet 
    'where demand_date>='" + clsCommon.GetPrintDate(CurrDate) + "' and demand_date<='" + clsCommon.GetPrintDate(CurrDate) + "'
    'and shifttype='" + clsCommon.myCstr(Shift) + "' and cust_code='" + CustCode + "' and created_by='" + CurrUser + "'"
    '            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                For Each dr As DataRow In dt.Rows
    '                    Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_Customer_Master where cust_code='" + CustCode + "'"))
    '                    Dim strQry As String = "select TSPL_DEMAND_BOOKING_MASTER.Document_No from TSPL_DEMAND_BOOKING_MASTER
    'left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_MASTER.Document_No=TSPL_DEMAND_BOOKING_DETAIL.Document_No
    'where TSPL_DEMAND_BOOKING_MASTER.Document_Date>='" + clsCommon.GetPrintDate(CurrDate) + "' and TSPL_DEMAND_BOOKING_MASTER.Document_Date<='" + clsCommon.GetPrintDate(CurrDate) + "'
    'and TSPL_DEMAND_BOOKING_MASTER.Route_No='" + RouteNo + "' and TSPL_DEMAND_BOOKING_DETAIL.Cust_Code='" + CustCode + "' and TSPL_DEMAND_BOOKING_DETAIL.ShiftType='" + Shift + "'"
    '                    Dim DocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry))
    '                    If clsCommon.myLen(DocumentNo) > 0 Then
    '                        Dim obj As New clsDemandBookingSale()
    '                        obj = clsDemandBookingSale.GetData(DocumentNo, NavigatorType.Current, Nothing)
    '                        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
    '                            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
    '                                For Each objTr As clsDemandBookingSaleDetail In obj.Arr
    '                                    If clsCommon.CompairString(CustCode, objTr.Cust_Code) = CompairStringResult.Equal Then
    '                                        Dim k As Integer = 1
    '                                        For columns = 3 To gv1.Columns.Count - 1
    '                                            Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
    '                                            k = k + 1
    '                                            If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal Then
    '                                                objTr.Qty = grow.Cells(columns).Value
    '                                                obj.SaveData(obj, False)
    '                                            End If
    '                                        Next
    '                                    End If
    '                                    'obj.SaveData(obj, False)
    '                                Next
    '                            End If
    '                        End If
    '                    Else
    '                        Dim obj1 As New clsDemandBookingSale()
    '                        obj1.ShiftType = txtShift.Text
    '                        obj1.Location_Code = objCommonVar.CurrLocationCode
    '                        obj1.Document_Date = txtDate.Value
    '                    End If
    '                Next
    '            End If
    '        Catch ex As Exception

    '        End Try
    '        Return True

    '    End Function


End Class
Public Class clsDemandSheetDetails
    Public Cust_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Set_Zero As Decimal = 0
    Public Qty As Decimal = 0
End Class
