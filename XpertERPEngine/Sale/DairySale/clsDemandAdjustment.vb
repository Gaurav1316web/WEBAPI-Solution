Imports System.Data.SqlClient
Public Class clsDemandAdjustment
#Region "Variable"
    Public Document_Code As String = ""
    Public Document_Date As DateTime = Nothing
    Public Demand_Date As DateTime = Nothing
    Public Zone_Code As String = ""
    Public Route_Code As String = ""
    Public Item_Code As String = ""
    Public Unit_Code As String = ""
    Public Shift_Type As String = ""
    Public Is_Change_Product As Boolean = False
    Public Minimum_Qty As Double = 0
    Public Is_Automatic As Boolean = False
    Public Is_FixQty As Boolean = False
    Public Increase_Decrease_Qty As Double = 0
    Public Percentage As Double = 0
    Public FixedQty As Double = 0
    Public Deduct_Qty As Double = 0
    Public Change_Item_Code As String = ""
    Public Add_Qty As Double = 0
    Public Status As Integer = 0
    Public Modified_By As String = ""
    Public Modified_Date As DateTime = Nothing
    Public Created_By As String = ""
    Public Created_Date As DateTime = Nothing
    Public Arr As List(Of clsDemandAdjustmentDetail) = Nothing
#End Region
    Public Function SaveData(ByVal obj As clsDemandAdjustment, ByVal isNewEntry As Boolean) As Boolean
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
    Public Function SaveData(ByVal obj As clsDemandAdjustment, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim StrQry As String = "delete from TSPL_DEMAND_ADJUSTMENT_DETAIL where Document_Code='" + obj.Document_Code + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Demand_Date", clsCommon.GetPrintDate(obj.Demand_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code, True)
            clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code, True)
            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "Shift_Type", obj.Shift_Type)
            clsCommon.AddColumnsForChange(coll, "Is_Change_Product", IIf(obj.Is_Change_Product, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Minimum_Qty", obj.Minimum_Qty)
            clsCommon.AddColumnsForChange(coll, "Is_Automatic", IIf(obj.Is_Automatic, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Is_FixQty", IIf(obj.Is_FixQty, 1, 0))
            clsCommon.AddColumnsForChange(coll, "Increase_Decrease_Qty", obj.Increase_Decrease_Qty)
            clsCommon.AddColumnsForChange(coll, "Percentage", obj.Percentage)
            clsCommon.AddColumnsForChange(coll, "FixedQty", obj.FixedQty)
            clsCommon.AddColumnsForChange(coll, "Deduct_Qty", obj.Deduct_Qty)
            clsCommon.AddColumnsForChange(coll, "Change_Item_Code", obj.Change_Item_Code, True)
            clsCommon.AddColumnsForChange(coll, "Add_Qty", obj.Add_Qty)
            clsCommon.AddColumnsForChange(coll, "Status", obj.Status)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
            If isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmDemandAdjustment, "", "")
                If (clsCommon.myLen(obj.Document_Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_ADJUSTMENT_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_ADJUSTMENT_HEAD", OMInsertOrUpdate.Update, "TSPL_DEMAND_ADJUSTMENT_HEAD.Document_Code='" + clsCommon.myCstr(obj.Document_Code) + "' ", trans)
            End If
            clsDemandAdjustmentDetail.SaveData(obj.Document_Code, obj.Document_Date, obj.Arr, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocumentNo As String, ByVal NavType As NavigatorType) As clsDemandAdjustment
        Return GetData(strDocumentNo, NavType, Nothing)
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsDemandAdjustment
        Dim obj As clsDemandAdjustment = Nothing
        Dim qry = "SELECT  TSPL_DEMAND_ADJUSTMENT_HEAD.*  FROM TSPL_DEMAND_ADJUSTMENT_HEAD where 2=2 "
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_DEMAND_ADJUSTMENT_HEAD.Document_Code = (select MIN(Document_Code) from TSPL_DEMAND_ADJUSTMENT_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_DEMAND_ADJUSTMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_DEMAND_ADJUSTMENT_HEAD WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_DEMAND_ADJUSTMENT_HEAD.Document_Code = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_DEMAND_ADJUSTMENT_HEAD.Document_Code = (select Min(Document_Code) from TSPL_DEMAND_ADJUSTMENT_HEAD where Document_Code>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_DEMAND_ADJUSTMENT_HEAD.Document_Code = (select Max(Document_Code) from TSPL_DEMAND_ADJUSTMENT_HEAD where Document_Code<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsDemandAdjustment()
            obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Demand_Date = clsCommon.myCDate(dt.Rows(0)("Demand_Date"))
            obj.Shift_Type = clsCommon.myCstr(dt.Rows(0)("Shift_Type"))
            obj.Zone_Code = clsCommon.myCstr(dt.Rows(0)("Zone_Code"))
            obj.Route_Code = clsCommon.myCstr(dt.Rows(0)("Route_Code"))
            obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            obj.Minimum_Qty = clsCommon.myCdbl(dt.Rows(0)("Minimum_Qty"))
            obj.Is_Change_Product = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Change_Product")) = 1, True, False)
            obj.Is_Automatic = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Automatic")) = 1, True, False)
            obj.Is_FixQty = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_FixQty")) = 1, True, False)
            obj.Increase_Decrease_Qty = clsCommon.myCdbl(dt.Rows(0)("Increase_Decrease_Qty"))
            obj.Percentage = clsCommon.myCdbl(dt.Rows(0)("Percentage"))
            obj.FixedQty = clsCommon.myCdbl(dt.Rows(0)("FixedQty"))
            obj.Deduct_Qty = clsCommon.myCdbl(dt.Rows(0)("Deduct_Qty"))
            obj.Change_Item_Code = clsCommon.myCstr(dt.Rows(0)("Change_Item_Code"))
            obj.Add_Qty = clsCommon.myCdbl(dt.Rows(0)("Add_Qty"))
            obj.Status = clsCommon.myCdbl(dt.Rows(0)("Status"))
            qry = "SELECT TSPL_DEMAND_ADJUSTMENT_DETAIL.* from TSPL_DEMAND_ADJUSTMENT_DETAIL where Document_Code='" + obj.Document_Code + "' "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of clsDemandAdjustmentDetail)
                Dim objTr As clsDemandAdjustmentDetail
                For Each dr As DataRow In dt.Rows
                    objTr = New clsDemandAdjustmentDetail
                    objTr.TR_Code = clsCommon.myCstr(dr("TR_Code"))
                    objTr.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                    objTr.Zone_Code = clsCommon.myCstr(dr("Zone_Code"))
                    objTr.Route_Code = clsCommon.myCstr(dr("Route_Code"))
                    objTr.Booth_Code = clsCommon.myCstr(dr("Booth_Code"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Demand_Qty = clsCommon.myCstr(dr("Demand_Qty"))
                    objTr.Adjust_Qty = clsCommon.myCstr(dr("Adjust_Qty"))
                    objTr.Final_Qty = clsCommon.myCstr(dr("Final_Qty"))
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function
    Public Shared Function ProceedDemand(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ProceedDemand(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ProceedDemand(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim obj As New clsDemandAdjustment
            'Dim DocNo As String = ""
            Dim Qry As String = ""
            Dim lstDocNO As New List(Of String)

            obj = GetData(strDocNo, NavigatorType.Current, trans)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsDemandAdjustmentDetail In obj.Arr
                        'DocNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select document_No from TSPL_BOOKING_DETAIL where Against_DemandBooking_TR_Code='" + objTr.TR_Code + "'", trans))
                        'clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, DocNo, "TSPL_BOOKING_MATSER", "Document_No", "TSPL_BOOKING_DETAIL", "Document_No", "TSPL_BOOKING_PAYMENT_MODE_DETAIL", "Document_No", trans)
                        'obj1 = clsDemandBookingSale.GetData(demandQry, NavigatorType.Current, trans)
                        'For Each objTr1 As clsDemandBookingSaleDetail In obj1.Arr
                        '    If clsCommon.CompairString(objTr1.Cust_Code, objTr.Booth_Code) = CompairStringResult.Equal Then
                        '        If clsCommon.CompairString(objTr1.Item_Code, objTr.Item_Code) = CompairStringResult.Equal Then
                        '            If clsCommon.CompairString(objTr1.Unit_code, objTr.Unit_Code) = CompairStringResult.Equal Then
                        '                objTr1.Qty = objTr.Final_Qty
                        '            End If
                        '        End If
                        '    End If
                        'Next
                        'clsDemandBookingSale.SaveData(obj1, False, trans)
                        Qry = "update TSPL_DEMAND_BOOKING_DETAIL set Qty='" + clsCommon.myCstr(objTr.Final_Qty) + "' where TR_Code='" + objTr.TR_Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Dim DcoNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select document_no from TSPL_DEMAND_BOOKING_DETAIL where TR_Code='" + objTr.TR_Code + "'", trans))
                        If Not lstDocNO.Contains(DcoNo) Then
                            lstDocNO.Add(DcoNo)
                        End If
                        'Qry = "update TSPL_BOOKING_DETAIL set Booking_Qty='" + clsCommon.myCstr(objTr.Final_Qty) + "' where Against_DemandBooking_TR_Code='" + objTr.TR_Code + "'"
                        'clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Next
                    If lstDocNO IsNot Nothing AndAlso lstDocNO.Count > 0 Then
                        For Each item As String In lstDocNO
                            Dim obj1 As New clsDemandBookingSale
                            obj1 = clsDemandBookingSale.GetData(item, NavigatorType.Current, trans)
                            clsDemandBookingSale.SaveData(obj1, False, False, trans)

                        Next
                    End If

                    clsDBFuncationality.ExecuteNonQuery("Update TSPL_Demand_Adjustment_Head set Status=1 where Document_Code='" + obj.Document_Code + "'", trans)
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_Code, "TSPL_Demand_Adjustment_Head", "Document_Code", "TSPL_DEMAND_ADJUSTMENT_DETAIL", "Document_Code", trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            DeleteData(strDocNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Delete")
            End If
            Dim StrQry As String = "delete from TSPL_DEMAND_ADJUSTMENT_DETAIL where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)
            Dim StrQry1 As String = "delete from TSPL_DEMAND_ADJUSTMENT_Head where Document_Code='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(StrQry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsDemandAdjustmentDetail
#Region "Variable"
    Public Document_Code As String = ""
    Public TR_Code As String = ""
    Public Zone_Code As String = ""
    Public Route_Code As String = ""
    Public Booth_Code As String = ""
    Public Item_Code As String = ""
    Public Unit_Code As String = ""
    Public Demand_Qty As Double = 0
    Public Adjust_Qty As Double = 0
    Public Final_Qty As Double = 0
#End Region
    Public Shared Function SaveData(ByVal strDocNo As String, ByVal DocDate As Date, ByVal Arr As List(Of clsDemandAdjustmentDetail), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each obj As clsDemandAdjustmentDetail In Arr
                If obj.Final_Qty > 0 Then
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "TR_CODE", obj.TR_Code)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                    clsCommon.AddColumnsForChange(coll, "Zone_Code", obj.Zone_Code)
                    clsCommon.AddColumnsForChange(coll, "Route_Code", obj.Route_Code)
                    clsCommon.AddColumnsForChange(coll, "Booth_Code", obj.Booth_Code)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
                    clsCommon.AddColumnsForChange(coll, "Demand_Qty", obj.Demand_Qty)
                    clsCommon.AddColumnsForChange(coll, "Adjust_Qty", obj.Adjust_Qty)
                    clsCommon.AddColumnsForChange(coll, "Final_Qty", obj.Final_Qty)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_ADJUSTMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        End If
        Return True
    End Function
End Class
