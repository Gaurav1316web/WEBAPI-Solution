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
    Public Location_Code As String = ""
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
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
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
                    objTr.TotalCrates_ItemWise = clsCommon.myCdbl(dr("TotalCrates_ItemWise"))
                    objTr.TotalLtr_ItemWise = clsCommon.myCdbl(dr("TotalLtr_ItemWise"))
                    objTr.Item_Rate = clsCommon.myCdbl(dr("Item_Rate"))
                    objTr.ItemNetAmount = clsCommon.myCdbl(dr("ItemNetAmount"))
                    objTr.TAX_Group = clsCommon.myCstr(dr("TAX_Group"))
                    objTr.TAX1 = clsCommon.myCstr(dr("TAX1"))
                    objTr.TAX1_Rate = clsCommon.myCdbl(dr("TAX1_Rate"))
                    objTr.TAX1_Amt = clsCommon.myCdbl(dr("TAX1_Amt"))
                    objTr.TAX1_Base_Amt = clsCommon.myCdbl(dr("TAX1_Base_Amt"))
                    objTr.TAX2 = clsCommon.myCstr(dr("TAX2"))
                    objTr.TAX2_Rate = clsCommon.myCdbl(dr("TAX2_Rate"))
                    objTr.TAX2_Amt = clsCommon.myCdbl(dr("TAX2_Amt"))
                    objTr.TAX2_Base_Amt = clsCommon.myCdbl(dr("TAX2_Base_Amt"))
                    objTr.TAX3 = clsCommon.myCstr(dr("TAX3"))
                    objTr.TAX3_Rate = clsCommon.myCdbl(dr("TAX3_Rate"))
                    objTr.TAX3_Amt = clsCommon.myCdbl(dr("TAX3_Amt"))
                    objTr.TAX3_Base_Amt = clsCommon.myCdbl(dr("TAX3_Base_Amt"))
                    objTr.TAX4 = clsCommon.myCstr(dr("TAX4"))
                    objTr.TAX4_Rate = clsCommon.myCdbl(dr("TAX4_Rate"))
                    objTr.TAX4_Amt = clsCommon.myCdbl(dr("TAX4_Amt"))
                    objTr.TAX4_Base_Amt = clsCommon.myCdbl(dr("TAX4_Base_Amt"))
                    objTr.TAX5 = clsCommon.myCstr(dr("TAX5"))
                    objTr.TAX5_Rate = clsCommon.myCdbl(dr("TAX5_Rate"))
                    objTr.TAX5_Amt = clsCommon.myCdbl(dr("TAX5_Amt"))
                    objTr.TAX5_Base_Amt = clsCommon.myCdbl(dr("TAX5_Base_Amt"))
                    objTr.TAX6 = clsCommon.myCstr(dr("TAX6"))
                    objTr.TAX6_Rate = clsCommon.myCdbl(dr("TAX6_Rate"))
                    objTr.TAX6_Amt = clsCommon.myCdbl(dr("TAX6_Amt"))
                    objTr.TAX6_Base_Amt = clsCommon.myCdbl(dr("TAX6_Base_Amt"))
                    objTr.TAX7 = clsCommon.myCstr(dr("TAX7"))
                    objTr.TAX7_Rate = clsCommon.myCdbl(dr("TAX7_Rate"))
                    objTr.TAX7_Amt = clsCommon.myCdbl(dr("TAX7_Amt"))
                    objTr.TAX7_Base_Amt = clsCommon.myCdbl(dr("TAX7_Base_Amt"))
                    objTr.TAX8 = clsCommon.myCstr(dr("TAX8"))
                    objTr.TAX8_Rate = clsCommon.myCdbl(dr("TAX8_Rate"))
                    objTr.TAX8_Amt = clsCommon.myCdbl(dr("TAX8_Amt"))
                    objTr.TAX8_Base_Amt = clsCommon.myCdbl(dr("TAX8_Base_Amt"))
                    objTr.TAX9 = clsCommon.myCstr(dr("TAX9"))
                    objTr.TAX9_Rate = clsCommon.myCdbl(dr("TAX9_Rate"))
                    objTr.TAX9_Amt = clsCommon.myCdbl(dr("TAX9_Amt"))
                    objTr.TAX9_Base_Amt = clsCommon.myCdbl(dr("TAX9_Base_Amt"))
                    objTr.TAX10 = clsCommon.myCstr(dr("TAX10"))
                    objTr.TAX10_Rate = clsCommon.myCdbl(dr("TAX10_Rate"))
                    objTr.TAX10_Amt = clsCommon.myCdbl(dr("TAX10_Amt"))
                    objTr.TAX10_Base_Amt = clsCommon.myCdbl(dr("TAX10_Base_Amt"))

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
                        Qry = "update TSPL_DEMAND_BOOKING_DETAIL set Qty='" + clsCommon.myCstr(objTr.Final_Qty) + "',TotalCrates_ItemWise='" + clsCommon.myCstr(objTr.TotalCrates_ItemWise) + "',TotalLtr_ItemWise='" + clsCommon.myCstr(objTr.TotalLtr_ItemWise) + "'
,Item_Rate='" + clsCommon.myCstr(objTr.Item_Rate) + "',ItemNetAmount='" + clsCommon.myCstr(objTr.ItemNetAmount) + "',TAX_Group='" + clsCommon.myCstr(objTr.TAX_Group) + "',
TAX1='" + clsCommon.myCstr(objTr.TAX1) + "',TAX1_Rate='" + clsCommon.myCstr(objTr.TAX1_Rate) + "',TAX1_Amt='" + clsCommon.myCstr(objTr.TAX1_Amt) + "',TAX1_Base_Amt='" + clsCommon.myCstr(objTr.TAX1_Base_Amt) + "',
TAX2='" + clsCommon.myCstr(objTr.TAX2) + "',TAX2_Rate='" + clsCommon.myCstr(objTr.TAX2_Rate) + "',TAX2_Amt='" + clsCommon.myCstr(objTr.TAX2_Amt) + "',TAX2_Base_Amt='" + clsCommon.myCstr(objTr.TAX2_Base_Amt) + "',
TAX3='" + clsCommon.myCstr(objTr.TAX3) + "',TAX3_Rate='" + clsCommon.myCstr(objTr.TAX3_Rate) + "',TAX3_Amt='" + clsCommon.myCstr(objTr.TAX3_Amt) + "',TAX3_Base_Amt='" + clsCommon.myCstr(objTr.TAX3_Base_Amt) + "',
TAX4='" + clsCommon.myCstr(objTr.TAX4) + "',TAX4_Rate='" + clsCommon.myCstr(objTr.TAX4_Rate) + "',TAX4_Amt='" + clsCommon.myCstr(objTr.TAX4_Amt) + "',TAX4_Base_Amt='" + clsCommon.myCstr(objTr.TAX4_Base_Amt) + "',
TAX5='" + clsCommon.myCstr(objTr.TAX5) + "',TAX5_Rate='" + clsCommon.myCstr(objTr.TAX5_Rate) + "',TAX5_Amt='" + clsCommon.myCstr(objTr.TAX5_Amt) + "',TAX5_Base_Amt='" + clsCommon.myCstr(objTr.TAX5_Base_Amt) + "',
TAX6='" + clsCommon.myCstr(objTr.TAX6) + "',TAX6_Rate='" + clsCommon.myCstr(objTr.TAX6_Rate) + "',TAX6_Amt='" + clsCommon.myCstr(objTr.TAX6_Amt) + "',TAX6_Base_Amt='" + clsCommon.myCstr(objTr.TAX6_Base_Amt) + "',
TAX7='" + clsCommon.myCstr(objTr.TAX7) + "',TAX7_Rate='" + clsCommon.myCstr(objTr.TAX7_Rate) + "',TAX7_Amt='" + clsCommon.myCstr(objTr.TAX7_Amt) + "',TAX7_Base_Amt='" + clsCommon.myCstr(objTr.TAX7_Base_Amt) + "',
TAX8='" + clsCommon.myCstr(objTr.TAX8) + "',TAX8_Rate='" + clsCommon.myCstr(objTr.TAX8_Rate) + "',TAX8_Amt='" + clsCommon.myCstr(objTr.TAX8_Amt) + "',TAX8_Base_Amt='" + clsCommon.myCstr(objTr.TAX8_Base_Amt) + "',
TAX9='" + clsCommon.myCstr(objTr.TAX9) + "',TAX9_Rate='" + clsCommon.myCstr(objTr.TAX9_Rate) + "',TAX9_Amt='" + clsCommon.myCstr(objTr.TAX9_Amt) + "',TAX9_Base_Amt='" + clsCommon.myCstr(objTr.TAX9_Base_Amt) + "',
TAX10='" + clsCommon.myCstr(objTr.TAX10) + "',TAX10_Rate='" + clsCommon.myCstr(objTr.TAX10_Rate) + "',TAX10_Amt='" + clsCommon.myCstr(objTr.TAX10_Amt) + "',TAX10_Base_Amt='" + clsCommon.myCstr(objTr.TAX10_Base_Amt) + "'
where TR_Code='" + clsCommon.myCstr(objTr.TR_Code) + "'"
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
                            'Dim obj1 As New clsDemandBookingSale
                            ' obj1 = clsDemandBookingSale.GetData(item, NavigatorType.Current, trans)
                            'clsDemandBookingSale.SaveData(obj1, False, False, trans)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, item, "TSPL_DEMAND_BOOKING_MASTER", "Document_Code", "TSPL_DEMAND_BOOKING_DETAIL", "Document_Code", trans)


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
    Public TotalCrates_ItemWise As Double = 0
    Public TotalLtr_ItemWise As Double = 0
    Public Item_Rate As Double = 0
    Public ItemNetAmount As Double = 0
    Public TAX_Group As String = ""
    Public TAX1 As String = Nothing
    Public TAX1_Base_Amt As Double = 0
    Public TAX1_Rate As Double = 0
    Public TAX1_Amt As Double = 0
    Public TAX2 As String = Nothing
    Public TAX2_Base_Amt As Double = 0
    Public TAX2_Rate As Double = 0
    Public TAX2_Amt As Double = 0
    Public TAX3 As String = Nothing
    Public TAX3_Base_Amt As Double = 0
    Public TAX3_Rate As Double = 0
    Public TAX3_Amt As Double = 0
    Public TAX4 As String = Nothing
    Public TAX4_Base_Amt As Double = 0
    Public TAX4_Rate As Double = 0
    Public TAX4_Amt As Double = 0
    Public TAX5 As String = Nothing
    Public TAX5_Base_Amt As Double = 0
    Public TAX5_Rate As Double = 0
    Public TAX5_Amt As Double = 0
    Public TAX6 As String = Nothing
    Public TAX6_Base_Amt As Double = 0
    Public TAX6_Rate As Double = 0
    Public TAX6_Amt As Double = 0
    Public TAX7 As String = Nothing
    Public TAX7_Base_Amt As Double = 0
    Public TAX7_Rate As Double = 0
    Public TAX7_Amt As Double = 0
    Public TAX8 As String = Nothing
    Public TAX8_Base_Amt As Double = 0
    Public TAX8_Rate As Double = 0
    Public TAX8_Amt As Double = 0
    Public TAX9 As String = Nothing
    Public TAX9_Base_Amt As Double = 0
    Public TAX9_Rate As Double = 0
    Public TAX9_Amt As Double = 0
    Public TAX10 As String = Nothing
    Public TAX10_Base_Amt As Double = 0
    Public TAX10_Rate As Double = 0
    Public TAX10_Amt As Double = 0
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
                    clsCommon.AddColumnsForChange(coll, "TotalCrates_ItemWise", obj.TotalCrates_ItemWise)
                    clsCommon.AddColumnsForChange(coll, "TotalLtr_ItemWise", obj.TotalLtr_ItemWise)
                    clsCommon.AddColumnsForChange(coll, "Item_Rate", obj.Item_Rate)
                    clsCommon.AddColumnsForChange(coll, "ItemNetAmount", obj.ItemNetAmount)
                    clsCommon.AddColumnsForChange(coll, "TAX_Group", obj.TAX_Group)
                    clsCommon.AddColumnsForChange(coll, "TAX1", obj.TAX1)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Rate", obj.TAX1_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Amt", obj.TAX1_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX1_Base_Amt", obj.TAX1_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2", obj.TAX2)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Rate", obj.TAX2_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Amt", obj.TAX2_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX2_Base_Amt", obj.TAX2_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3", obj.TAX3)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Rate", obj.TAX3_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Amt", obj.TAX3_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX3_Base_Amt", obj.TAX3_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4", obj.TAX4)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Rate", obj.TAX4_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Amt", obj.TAX4_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX4_Base_Amt", obj.TAX4_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5", obj.TAX5)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Rate", obj.TAX5_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Amt", obj.TAX5_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX5_Base_Amt", obj.TAX5_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6", obj.TAX6)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Rate", obj.TAX6_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Amt", obj.TAX6_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX6_Base_Amt", obj.TAX6_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7", obj.TAX7)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Rate", obj.TAX7_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Amt", obj.TAX7_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX7_Base_Amt", obj.TAX7_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8", obj.TAX8)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Rate", obj.TAX8_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Amt", obj.TAX8_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX8_Base_Amt", obj.TAX8_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9", obj.TAX9)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Rate", obj.TAX9_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Amt", obj.TAX9_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX9_Base_Amt", obj.TAX9_Base_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10", obj.TAX10)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Rate", obj.TAX10_Rate)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Amt", obj.TAX10_Amt)
                    clsCommon.AddColumnsForChange(coll, "TAX10_Base_Amt", obj.TAX10_Base_Amt)

                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_DEMAND_ADJUSTMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                End If
            Next
        End If
        Return True
    End Function
End Class
