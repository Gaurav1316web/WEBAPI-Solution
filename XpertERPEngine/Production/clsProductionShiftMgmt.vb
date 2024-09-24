Imports System.Data.SqlClient
Public Class clsProductionShiftMgmt
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Location_Code As String
    Public Location_Name As String
    Public Shift_Code As String
    Public Shift_Start_Date As DateTime
    Public Shift_End_Date As DateTime
    Public Remarks As String
    Public Comment As String
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime? = Nothing
    Public ArrOP As List(Of clsProductionShiftMgmtOpen) = Nothing
    Public ArrRecPlant As List(Of clsProductionShiftMgmtReceiptPlantMilk) = Nothing
    Public ArrRecBulk As List(Of clsProductionShiftMgmtReceiptBulkMilk) = Nothing
    Public ArrPro As List(Of clsProductionShiftMgmtProduction) = Nothing
    Public ArrProRMSummary As List(Of clsProductionShiftMgmtProductionRMSummary) = Nothing
    Public ArrDisBulk As List(Of clsProductionShiftMgmtDisposalBulkMilk) = Nothing
    Public ArrCL As List(Of clsProductionShiftMgmtClose) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsProductionShiftMgmt, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_SHIFT_MGMT_CLOSE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_RM where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_OPEN where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If clsCommon.myLen(obj.Location_Code) <= 0 Then
                Throw New Exception("Please provide location")
            End If
            If clsCommon.myLen(obj.Shift_Code) <= 0 Then
                Throw New Exception("Please provide location to pick raw milk")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_Code, obj.Document_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Shift_Code", obj.Shift_Code)
            clsCommon.AddColumnsForChange(coll, "Shift_Start_Date", clsCommon.GetPrintDate(obj.Shift_Start_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Shift_End_Date", clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Remarks", obj.Remarks)
            clsCommon.AddColumnsForChange(coll, "Comment", obj.Comment)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ShiftMgmt, "", obj.Location_Code)
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT", OMInsertOrUpdate.Update, "TSPL_SHIFT_MGMT.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsProductionShiftMgmtOpen.SaveData(obj.Document_No, obj.ArrOP, trans)
            clsProductionShiftMgmtReceiptPlantMilk.SaveData(obj.Document_No, obj.ArrRecPlant, trans)
            clsProductionShiftMgmtReceiptBulkMilk.SaveData(obj.Document_No, obj.ArrRecBulk, trans)
            clsProductionShiftMgmtProduction.SaveData(obj.Document_No, obj.ArrPro, trans)
            clsProductionShiftMgmtProductionRMSummary.SaveData(obj.Document_No, obj.ArrProRMSummary, trans)
            clsProductionShiftMgmtDisposalBulkMilk.SaveData(obj.Document_No, obj.ArrDisBulk, trans)
            clsProductionShiftMgmtClose.SaveData(obj.Document_No, obj.ArrCL, trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionShiftMgmt
        Dim obj As clsProductionShiftMgmt = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT.*,TSPL_LOCATION_MASTER.Location_Desc as Location_Name 
FROM TSPL_SHIFT_MGMT 
left outer join TSPL_LOCATION_MASTER   on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT.Location_Code
where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select MIN(Document_No) from TSPL_SHIFT_MGMT where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select Max(Document_No) from TSPL_SHIFT_MGMT where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select Min(Document_No) from TSPL_SHIFT_MGMT where Document_No>'" + strDocNo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_SHIFT_MGMT.Document_No = (select Max(Document_No) from TSPL_SHIFT_MGMT where Document_No<'" + strDocNo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_SHIFT_MGMT.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsProductionShiftMgmt()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Shift_Code = clsCommon.myCstr(dt.Rows(0)("Shift_Code"))
            obj.Shift_Start_Date = clsCommon.myCDate(dt.Rows(0)("Shift_Start_Date"))
            obj.Shift_End_Date = clsCommon.myCDate(dt.Rows(0)("Shift_End_Date"))
            obj.Comment = clsCommon.myCstr(dt.Rows(0)("Comment"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            obj.ArrOP = clsProductionShiftMgmtOpen.GetData(obj.Document_No, "", trans)
            obj.ArrRecPlant = clsProductionShiftMgmtReceiptPlantMilk.GetData(obj.Document_No, "", trans)
            obj.ArrRecBulk = clsProductionShiftMgmtReceiptBulkMilk.GetData(obj.Document_No, "", trans)
            obj.ArrPro = clsProductionShiftMgmtProduction.GetData(obj.Document_No, "", trans)
            obj.ArrProRMSummary = clsProductionShiftMgmtProductionRMSummary.GetData(obj.Document_No, "", trans)
            obj.ArrDisBulk = clsProductionShiftMgmtDisposalBulkMilk.GetData(obj.Document_No, "", trans)
            obj.ArrCL = clsProductionShiftMgmtClose.GetData(obj.Document_No, "", trans)
        End If
        Return obj
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Return clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SHIFT_MGMT", "Document_No", "TSPL_SHIFT_MGMT_PRODUCTION", "Document_No", "TSPL_SHIFT_MGMT_OPEN", "Document_No", trans)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsProductionShiftMgmt = clsProductionShiftMgmt.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.ProductionShiftMgmt, obj.Location_Code, obj.Document_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_CLOSE where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION_RM where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_PRODUCTION where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_OPEN where Document_No='" + strCode + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT where Document_No='" + strCode + "'", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function PostData(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If (clsCommon.myLen(strCode) <= 0) Then
                Throw New Exception("Document No not found to Post")
            End If

            Dim obj As clsProductionShiftMgmt = clsProductionShiftMgmt.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_Code, obj.Document_Date, trans)

            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + obj.Posted_Date)
            End If

            HitInventoryAndJV(obj, trans)


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Status", 1)
            clsCommon.AddColumnsForChange(coll, "Posted_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Posted_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function HitInventoryAndJV(ByVal obj As clsProductionShiftMgmt, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            For Each objtr As clsProductionShiftMgmtProduction In obj.ArrPro
                If clsCommon.myLen(objtr.BOM_Code) <= 0 Then
                    Throw New Exception("BOM not found for produce item [" + objtr.Item_Code + "] and LTR Qty [" + clsCommon.myCstr(objtr.Qty_LTR) + "]")
                End If
                If objtr.ArrRM Is Nothing OrElse objtr.ArrRM.Count <= 0 Then
                    Throw New Exception("Raw material not found for produce item [" + objtr.Item_Code + "] and LTR Qty [" + clsCommon.myCstr(objtr.Qty_LTR) + "]")
                End If
            Next
            For Each objtr As clsProductionShiftMgmtProductionRMSummary In obj.ArrProRMSummary
                If objtr.Arr Is Nothing OrElse objtr.Arr.Count <= 0 Then
                    Throw New Exception("Please issue raw item [" + objtr.Item_Code + "(" + objtr.Item_Name + ")] , Qty [" + clsCommon.myCstr(objtr.Qty) + "] and UOM [" + objtr.UOM + "] ")
                End If
            Next

            If True Then
                Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                Dim ArrInvetoryMovementNew As New List(Of clsInventoryMovementNew)
                Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)

                ''out the Raw material and Packing Item
                For Each objRMSummary As clsProductionShiftMgmtProductionRMSummary In obj.ArrProRMSummary
                    For Each objRMSIssue As clsProductionShiftMgmtProductionRMIssue In objRMSummary.Arr
                        If Not settAllowNegativeStockInDairyProduction Then
                            Dim CheckStockServerDate As Boolean
                            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)), "1") = CompairStringResult.Equal Then
                                CheckStockServerDate = True
                            Else
                                CheckStockServerDate = False
                            End If

                            If clsCommon.CompairString(objRMSIssue.ItemProductType, "MI") = CompairStringResult.Equal Then
                                Dim strMainLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select main_location_code from tspl_location_master where Location_Code='" + objRMSIssue.Location_Code + "'", trans))
                                dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objRMSIssue.Item_Code, strMainLocation, objRMSIssue.Location_Code, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), obj.Shift_Start_Date), trans, objRMSIssue.UOM, 1)
                            Else
                                dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objRMSIssue.Item_Code, objRMSIssue.Location_Code, "", IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), obj.Shift_Start_Date), trans, objRMSIssue.UOM, 2)
                            End If
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                If objRMSIssue.Qty > clsCommon.myCDecimal(dt.Rows(0)("qty")) Then
                                    If Math.Abs(objRMSIssue.Qty - clsCommon.myCDecimal(dt.Rows(0)("qty"))) > 0.01 Then
                                        Throw New Exception("Item [" + objRMSIssue.Item_Code + "] Location [" + objRMSIssue.Location_Code + "] Issue Qty [" + clsCommon.myCstr(objRMSIssue.Qty) + "] is more than Balance Qty [" + clsCommon.myCstr(clsCommon.myCDecimal(dt.Rows(0)("qty"))) + "]")
                                    End If
                                End If
                            End If
                            'If isCheckFutureBalance Then
                            '    Dim Product_Type As String = clsItemMaster.GetItemProductType(objRMSIssue.Item_Code, trans)
                            '    Dim FutureBalanceQty As Decimal = 0
                            '    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                            '        FutureBalanceQty = clsInventoryMovementNew.getBalance(objRMSIssue.Item_Code, clsLocation.GetMainLocationMilk(objtr.frm_loc_code, trans), objtr.frm_loc_code, "", obj.Shift_Start_Date, trans, objRMSIssue.UOM)
                            '    Else
                            '        FutureBalanceQty = clsItemLocationDetails.getBalance(objRMSIssue.Item_Code, objtr.frm_loc_code, "", obj.Shift_Start_Date, trans, objRMSIssue.UOM, 0)
                            '    End If
                            '    FutureBalanceQty = Math.Round(Math.Round(FutureBalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                            '    If objRMSIssue.Qty > FutureBalanceQty Then
                            '        If Math.Abs(objRMSIssue.Qty - FutureBalanceQty) > 0.01 Then
                            '            Throw New Exception("Item [" + objRMSIssue.Item_Code + "] Location [" + objtr.frm_loc_code + "] Issue Qty [" + clsCommon.myCstr(objRMSIssue.Qty) + "] is more than Future Mininium Balance Qty [" + clsCommon.myCstr(FutureBalanceQty) + "]")
                            '        End If
                            '    End If
                            'End If
                        End If
                        If clsCommon.CompairString(objRMSIssue.ItemProductType, "MI") = CompairStringResult.Equal Then
                            Dim objInventoryMovemnt As New clsInventoryMovementNew()
                            objInventoryMovemnt.Source_Doc_Date = obj.Shift_Start_Date
                            objInventoryMovemnt.InOut = "O"
                            objInventoryMovemnt.main_location = ""
                            objInventoryMovemnt.Location_Code = objRMSIssue.Location_Code
                            objInventoryMovemnt.Other_Location_Code = ""
                            objInventoryMovemnt.Other_Location_Desc = ""
                            objInventoryMovemnt.Item_Code = objRMSIssue.Item_Code
                            objInventoryMovemnt.Item_Desc = objRMSIssue.Item_Name
                            objInventoryMovemnt.Qty = objRMSIssue.Qty
                            objInventoryMovemnt.UOM = objRMSIssue.UOM
                            objInventoryMovemnt.MRP = Nothing
                            objInventoryMovemnt.Add_Cost = Nothing
                            objInventoryMovemnt.Net_Cost = Nothing
                            If clsCommon.CompairString(objRMSIssue.ItemItemType, "R") = CompairStringResult.Equal Then
                                objInventoryMovemnt.ItemType = "RM"
                            ElseIf clsCommon.CompairString(objRMSIssue.ItemItemType, "F") = CompairStringResult.Equal Then
                                objInventoryMovemnt.ItemType = "FT"
                            Else
                                objInventoryMovemnt.ItemType = objRMSIssue.ItemItemType
                            End If
                            objInventoryMovemnt.Basic_Cost = Nothing
                            objInventoryMovemnt.Batch_No = ""
                            objInventoryMovemnt.MFG_Date = Nothing
                            objInventoryMovemnt.Expiry_Date = Nothing
                            objInventoryMovemnt.FAT_Per = objRMSIssue.FAT
                            objInventoryMovemnt.FAT_KG = objRMSIssue.FAT_KG
                            objInventoryMovemnt.SNF_Per = objRMSIssue.SNF
                            objInventoryMovemnt.SNF_KG = objRMSIssue.SNF_KG


                            Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", objRMSIssue.ItemProductType, objRMSIssue.Item_Code, objRMSIssue.Location_Code, objRMSIssue.Qty, objRMSIssue.UOM, objRMSIssue.FAT_KG, objRMSIssue.SNF_KG, obj.Shift_Start_Date, obj.Shift_Start_Date, False, trans)
                            objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
                            objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
                            objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
                            objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
                            Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                            objInventoryMovemnt.FIFO_Cost = cost
                            objInventoryMovemnt.Avg_Cost = cost
                            objInventoryMovemnt.LIFO_Cost = cost
                            objInventoryMovemnt.CalculateAvgCost = False
                            objInventoryMovemnt.Ref_ID_Type = "1" ''1-RM(Milk),2-RM(Other),3-OverheadCost,4-Add(Milk),5-Add(Other),6-Remove(Milk),7-Remove(Other)
                            objInventoryMovemnt.Ref_ID = objRMSIssue.PK_ID
                            ArrInvetoryMovementNew.Add(objInventoryMovemnt)
                        Else
                            Dim objInventoryMovemnt As New clsInventoryMovement()
                            objInventoryMovemnt.InOut = "O"
                            objInventoryMovemnt.Location_Code = objRMSIssue.Location_Code
                            objInventoryMovemnt.Other_Location_Code = ""
                            objInventoryMovemnt.Other_Location_Desc = ""
                            objInventoryMovemnt.Item_Code = objRMSIssue.Item_Code
                            objInventoryMovemnt.Item_Desc = objRMSIssue.Item_Name
                            objInventoryMovemnt.Qty = objRMSIssue.Qty
                            objInventoryMovemnt.UOM = objRMSIssue.UOM
                            objInventoryMovemnt.MRP = Nothing
                            objInventoryMovemnt.Add_Cost = Nothing
                            objInventoryMovemnt.Net_Cost = Nothing
                            If clsCommon.CompairString(objRMSIssue.ItemItemType, "R") = CompairStringResult.Equal Then
                                objInventoryMovemnt.ItemType = "RM"
                            ElseIf clsCommon.CompairString(objRMSIssue.ItemItemType, "F") = CompairStringResult.Equal Then
                                objInventoryMovemnt.ItemType = "FT"
                            Else
                                objInventoryMovemnt.ItemType = objRMSIssue.ItemItemType
                            End If
                            objInventoryMovemnt.Batch_No = ""
                            objInventoryMovemnt.MFG_Date = Nothing
                            objInventoryMovemnt.Expiry_Date = Nothing
                            objInventoryMovemnt.FAT_Per = objRMSIssue.FAT
                            objInventoryMovemnt.FAT_KG = objRMSIssue.FAT_KG
                            objInventoryMovemnt.SNF_Per = objRMSIssue.SNF
                            objInventoryMovemnt.SNF_KG = objRMSIssue.SNF_KG

                            Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", objRMSIssue.ItemProductType, objRMSIssue.Item_Code, objRMSIssue.Location_Code, objRMSIssue.Qty, objRMSIssue.UOM, objRMSIssue.FAT_KG, objRMSIssue.SNF_KG, obj.Shift_Start_Date, obj.Shift_Start_Date, False, trans)
                            objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
                            objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
                            objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
                            objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
                            Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                            objInventoryMovemnt.FIFO_Cost = cost
                            objInventoryMovemnt.Avg_Cost = cost
                            objInventoryMovemnt.LIFO_Cost = cost
                            'objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
                            objInventoryMovemnt.CalculateAvgCost = False
                            objInventoryMovemnt.Ref_ID_Type = "2" ''1-RM(Milk),2-RM(Other),3-OverheadCost,4-Add(Milk),5-Add(Other),6-Remove(Milk),7-Remove(Other)
                            objInventoryMovemnt.Ref_ID = objRMSIssue.PK_ID
                            ArrInventoryMovement.Add(objInventoryMovemnt)
                        End If
                    Next
                Next
                If ArrInvetoryMovementNew.Count > 0 Then
                    clsInventoryMovementNew.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                End If
                If ArrInventoryMovement.Count > 0 Then
                    clsInventoryMovement.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If

                For Each objPro As clsProductionShiftMgmtProduction In obj.ArrPro
                    ArrInventoryMovement = New List(Of clsInventoryMovement)
                    ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                    If objPro.ArrAdd IsNot Nothing AndAlso objPro.ArrAdd.Count > 0 Then
                        For Each objAdd As clsProductionShiftMgmtProductionItemAddRemove In objPro.ArrAdd
                            If Not settAllowNegativeStockInDairyProduction Then
                                Dim CheckStockServerDate As Boolean
                                If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, trans)), "1") = CompairStringResult.Equal Then
                                    CheckStockServerDate = True
                                Else
                                    CheckStockServerDate = False
                                End If
                                If clsCommon.CompairString(objAdd.ItemProductType, "MI") = CompairStringResult.Equal Then
                                    Dim strMainLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select main_location_code from tspl_location_master where Location_Code='" + objAdd.Location_Code + "'", trans))
                                    dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objAdd.Item_Code, strMainLocation, objAdd.Location_Code, IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), obj.Shift_Start_Date), trans, objAdd.UOM, 1)
                                Else
                                    dt = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG(objAdd.Item_Code, objAdd.Location_Code, "", IIf(CheckStockServerDate = True, clsCommon.GETSERVERDATE(trans), obj.Shift_Start_Date), trans, objAdd.UOM, 2)
                                End If
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    If objAdd.Qty > clsCommon.myCDecimal(dt.Rows(0)("qty")) Then
                                        If Math.Abs(objAdd.Qty - clsCommon.myCDecimal(dt.Rows(0)("qty"))) > 0.01 Then
                                            Throw New Exception("Item [" + objAdd.Item_Code + "] Location [" + objAdd.Location_Code + "] Issue Qty [" + clsCommon.myCstr(objAdd.Qty) + "] is more than Balance Qty [" + clsCommon.myCstr(clsCommon.myCDecimal(dt.Rows(0)("qty"))) + "]")
                                        End If
                                    End If
                                End If
                                'If isCheckFutureBalance Then
                                '    Dim Product_Type As String = clsItemMaster.GetItemProductType(objAdd.Item_Code, trans)
                                '    Dim FutureBalanceQty As Decimal = 0
                                '    If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                                '        FutureBalanceQty = clsInventoryMovementNew.getBalance(objAdd.Item_Code, clsLocation.GetMainLocationMilk(objtr.frm_loc_code, trans), objtr.frm_loc_code, "", obj.Shift_Start_Date, trans, objAdd.UOM)
                                '    Else
                                '        FutureBalanceQty = clsItemLocationDetails.getBalance(objAdd.Item_Code, objtr.frm_loc_code, "", obj.Shift_Start_Date, trans, objAdd.UOM, 0)
                                '    End If
                                '    FutureBalanceQty = Math.Round(Math.Round(FutureBalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                                '    If objAdd.Qty > FutureBalanceQty Then
                                '        If Math.Abs(objAdd.Qty - FutureBalanceQty) > 0.01 Then
                                '            Throw New Exception("Item [" + objAdd.Item_Code + "] Location [" + objtr.frm_loc_code + "] Issue Qty [" + clsCommon.myCstr(objAdd.Qty) + "] is more than Future Mininium Balance Qty [" + clsCommon.myCstr(FutureBalanceQty) + "]")
                                '        End If
                                '    End If
                                'End If
                            End If
                            If clsCommon.CompairString(objAdd.ItemProductType, "MI") = CompairStringResult.Equal Then
                                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                                objInventoryMovemnt.Source_Doc_Date = obj.Shift_Start_Date
                                objInventoryMovemnt.InOut = "O"
                                objInventoryMovemnt.main_location = ""
                                objInventoryMovemnt.Location_Code = objAdd.Location_Code
                                objInventoryMovemnt.Other_Location_Code = ""
                                objInventoryMovemnt.Other_Location_Desc = ""
                                objInventoryMovemnt.Item_Code = objAdd.Item_Code
                                objInventoryMovemnt.Item_Desc = objAdd.Item_Name
                                objInventoryMovemnt.Qty = objAdd.Qty
                                objInventoryMovemnt.UOM = objAdd.UOM
                                objInventoryMovemnt.MRP = Nothing
                                objInventoryMovemnt.Add_Cost = Nothing
                                objInventoryMovemnt.Net_Cost = Nothing
                                If clsCommon.CompairString(objAdd.ItemItemType, "R") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "RM"
                                ElseIf clsCommon.CompairString(objAdd.ItemItemType, "F") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "FT"
                                Else
                                    objInventoryMovemnt.ItemType = objAdd.ItemItemType
                                End If
                                objInventoryMovemnt.Basic_Cost = Nothing
                                objInventoryMovemnt.Batch_No = ""
                                objInventoryMovemnt.MFG_Date = Nothing
                                objInventoryMovemnt.Expiry_Date = Nothing
                                objInventoryMovemnt.FAT_Per = objAdd.FAT
                                objInventoryMovemnt.FAT_KG = objAdd.FAT_KG
                                objInventoryMovemnt.SNF_Per = objAdd.SNF
                                objInventoryMovemnt.SNF_KG = objAdd.SNF_KG


                                Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", objAdd.ItemProductType, objAdd.Item_Code, objAdd.Location_Code, objAdd.Qty, objAdd.UOM, objAdd.FAT_KG, objAdd.SNF_KG, obj.Shift_Start_Date, obj.Shift_Start_Date, False, trans)
                                objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
                                objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
                                objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
                                objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
                                Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                                objInventoryMovemnt.FIFO_Cost = cost
                                objInventoryMovemnt.Avg_Cost = cost
                                objInventoryMovemnt.LIFO_Cost = cost
                                objInventoryMovemnt.CalculateAvgCost = False
                                objInventoryMovemnt.Ref_ID_Type = "4" ''1-RM(Milk),2-RM(Other),3-OverheadCost,4-Add(Milk),5-Add(Other),6-Remove(Milk),7-Remove(Other)
                                objInventoryMovemnt.Ref_ID = objAdd.PK_ID
                                ArrInvetoryMovementNew.Add(objInventoryMovemnt)
                            Else
                                Dim objInventoryMovemnt As New clsInventoryMovement()
                                objInventoryMovemnt.InOut = "O"
                                objInventoryMovemnt.Location_Code = objAdd.Location_Code
                                objInventoryMovemnt.Other_Location_Code = ""
                                objInventoryMovemnt.Other_Location_Desc = ""
                                objInventoryMovemnt.Item_Code = objAdd.Item_Code
                                objInventoryMovemnt.Item_Desc = objAdd.Item_Name
                                objInventoryMovemnt.Qty = objAdd.Qty
                                objInventoryMovemnt.UOM = objAdd.UOM
                                objInventoryMovemnt.MRP = Nothing
                                objInventoryMovemnt.Add_Cost = Nothing
                                objInventoryMovemnt.Net_Cost = Nothing
                                If clsCommon.CompairString(objAdd.ItemItemType, "R") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "RM"
                                ElseIf clsCommon.CompairString(objAdd.ItemItemType, "F") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "FT"
                                Else
                                    objInventoryMovemnt.ItemType = objAdd.ItemItemType
                                End If
                                objInventoryMovemnt.Batch_No = ""
                                objInventoryMovemnt.MFG_Date = Nothing
                                objInventoryMovemnt.Expiry_Date = Nothing
                                objInventoryMovemnt.FAT_Per = objAdd.FAT
                                objInventoryMovemnt.FAT_KG = objAdd.FAT_KG
                                objInventoryMovemnt.SNF_Per = objAdd.SNF
                                objInventoryMovemnt.SNF_KG = objAdd.SNF_KG

                                Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", objAdd.ItemProductType, objAdd.Item_Code, objAdd.Location_Code, objAdd.Qty, objAdd.UOM, objAdd.FAT_KG, objAdd.SNF_KG, obj.Shift_Start_Date, obj.Shift_Start_Date, False, trans)
                                objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, objCost.FAT_Cost / objInventoryMovemnt.FAT_KG)
                                objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, objCost.SNF_Cost / objInventoryMovemnt.SNF_KG)
                                objInventoryMovemnt.Fat_Amt = objCost.FAT_Cost
                                objInventoryMovemnt.SNF_Amt = objCost.SNF_Cost
                                Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                                objInventoryMovemnt.FIFO_Cost = cost
                                objInventoryMovemnt.Avg_Cost = cost
                                objInventoryMovemnt.LIFO_Cost = cost
                                'objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
                                objInventoryMovemnt.CalculateAvgCost = False
                                objInventoryMovemnt.Ref_ID_Type = "5" ''1-RM(Milk),2-RM(Other),3-OverheadCost,4-Add(Milk),5-Add(Other),6-Remove(Milk),7-Remove(Other)
                                objInventoryMovemnt.Ref_ID = objAdd.PK_ID
                                ArrInventoryMovement.Add(objInventoryMovemnt)
                            End If
                        Next
                    End If

                    If ArrInvetoryMovementNew.Count > 0 Then
                        clsInventoryMovementNew.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                    End If
                    If ArrInventoryMovement.Count > 0 Then
                        clsInventoryMovement.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                    End If
                Next

                ''Consumption Raw Items Source_ID=1
                qry = "insert into TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION( Document_No,Against_PK_ID,Source_ID,Source_Code,Qty,UOM,FAT_KG,SNF_KG,FAT_AMT,SNF_AMT,AMT)  
( select Document_No,Against_PK_ID,Source_ID,Item_Code as Source_Code
,case when Summry_Qty=0 then 0 else Issue_Qty*Qty/Summry_Qty end as Qty,UOM
,case when Summry_FAT_KG=0 then 0 else Issue_FAT_KG*FAT_KG/Summry_FAT_KG end as FAT_KG 
,case when Summry_SNF_KG=0 then 0 else Issue_SNF_KG*SNF_KG/Summry_SNF_KG end as SNF_KG 
,case when Summry_FAT_KG=0 then 0 else Issue_FAT_Amt*FAT_KG/Summry_FAT_KG end as FAT_Amt
,case when Summry_SNF_KG=0 then 0 else Issue_SNF_Amt*SNF_KG/Summry_SNF_KG end as SNF_Amt 
,case when Summry_Qty=0 then 0 else Issue_Amt*Qty/Summry_Qty end as Amt
from (
select TSPL_SHIFT_MGMT_PRODUCTION_RM.Document_No,TSPL_SHIFT_MGMT_PRODUCTION_RM.Against_PK_ID
,case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type else TSPL_INVENTORY_MOVEMENT.Ref_ID_Type end as Source_ID
,TSPL_SHIFT_MGMT_PRODUCTION_RM.Item_Code,TSPL_ITEM_MASTER.Product_Type,TSPL_SHIFT_MGMT_PRODUCTION_RM.Qty,TSPL_SHIFT_MGMT_PRODUCTION_RM.UOM,TSPL_SHIFT_MGMT_PRODUCTION_RM.FAT_KG,TSPL_SHIFT_MGMT_PRODUCTION_RM.SNF_KG,TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.Qty as Summry_Qty,TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.FAT_KG as Summry_FAT_KG,TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.SNF_KG as Summry_SNF_KG,TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.PK_ID as Issue_PK_ID,TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Qty as Issue_Qty,TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.FAT_KG as Issue_FAT_KG,TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.SNF_KG as Issue_SNF_KG
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt else 0 end ,0) as Issue_FAT_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt else 0 end ,0) as Issue_SNF_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then (TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt+TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt) else TSPL_INVENTORY_MOVEMENT.Avg_Cost end,0) as Issue_Amt
from TSPL_SHIFT_MGMT_PRODUCTION_RM
left outer join TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY on TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.Document_No=TSPL_SHIFT_MGMT_PRODUCTION_RM.Document_No and TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM.Item_Code
left outer join TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE on TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Against_RM_Summary=TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.PK_ID
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Item_Code
left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_SHIFT_MGMT_PRODUCTION_RM.Document_No and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type='1' and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID=TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.PK_ID
left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SHIFT_MGMT_PRODUCTION_RM.Document_No and TSPL_INVENTORY_MOVEMENT.Ref_ID_Type='2' and TSPL_INVENTORY_MOVEMENT.Ref_ID=TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.PK_ID 
where TSPL_SHIFT_MGMT_PRODUCTION_RM.Document_No='" + obj.Document_No + "'
) xx
)"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                ''Consumption Overhead Cost Source_ID=3
                qry = "insert into TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION (Document_No,Against_PK_ID,Source_ID,Source_Code,Amt)  
select '" + obj.Document_No + "' as Document_No,PK_ID,3 as Source_ID,xx.COST_CODE,(xx.prod_qty * (xx.OverHead_Cost/xx.build_qty)) as Amt from (
select TSPL_SHIFT_MGMT_PRODUCTION.PK_ID,(TSPL_SHIFT_MGMT_PRODUCTION.Qty_LTR * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty
,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.COST_CODE,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.OverHead_Cost
from TSPL_SHIFT_MGMT_PRODUCTION
left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_SHIFT_MGMT_PRODUCTION.BOM_Code
inner join TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS on TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code=TSPL_PP_BOM_HEAD.BOM_CODE
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code='LTR' 
where TSPL_SHIFT_MGMT_PRODUCTION.Document_No='" + obj.Document_No + "'
) xx"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)




                qry = "insert into TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION( Document_No,Against_PK_ID,Source_ID,Source_Code,Qty,UOM,FAT_KG,SNF_KG,FAT_AMT,SNF_AMT,AMT)  
select TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Against_PK_ID
,case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type else TSPL_INVENTORY_MOVEMENT.Ref_ID_Type end as Source_ID
,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Item_Code as Source_Code ,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Qty,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.UOM
,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.FAT_KG,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.SNF_KG
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt else 0 end ,0) as FAT_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt else 0 end ,0) as SNF_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then (TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt+TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt) else TSPL_INVENTORY_MOVEMENT.Avg_Cost end,0) as AMT
from TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Item_Code
left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type='4' and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.PK_ID
left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT.Ref_ID_Type='5' and TSPL_INVENTORY_MOVEMENT.Ref_ID=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.PK_ID 
where  TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Type=1 and TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No='" + obj.Document_No + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                ''In Remove Item
                ArrInventoryMovement = New List(Of clsInventoryMovement)
                ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                For Each objPro As clsProductionShiftMgmtProduction In obj.ArrPro
                    If objPro.ArrRemove IsNot Nothing AndAlso objPro.ArrRemove.Count > 0 Then
                        ArrInventoryMovement = New List(Of clsInventoryMovement)
                        ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                        For Each objRemove As clsProductionShiftMgmtProductionItemAddRemove In objPro.ArrRemove
                            If clsCommon.CompairString(objRemove.ItemProductType, "MI") = CompairStringResult.Equal Then
                                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                                objInventoryMovemnt.Source_Doc_Date = obj.Shift_Start_Date
                                objInventoryMovemnt.InOut = "I"
                                objInventoryMovemnt.main_location = ""
                                objInventoryMovemnt.Location_Code = objRemove.Location_Code
                                objInventoryMovemnt.Other_Location_Code = ""
                                objInventoryMovemnt.Other_Location_Desc = ""
                                objInventoryMovemnt.Item_Code = objRemove.Item_Code
                                objInventoryMovemnt.Item_Desc = objRemove.Item_Name
                                objInventoryMovemnt.Qty = objRemove.Qty
                                objInventoryMovemnt.UOM = objRemove.UOM
                                objInventoryMovemnt.MRP = Nothing
                                objInventoryMovemnt.Add_Cost = Nothing
                                objInventoryMovemnt.Net_Cost = Nothing
                                If clsCommon.CompairString(objRemove.ItemItemType, "R") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "RM"
                                ElseIf clsCommon.CompairString(objRemove.ItemItemType, "F") = CompairStringResult.Equal Then
                                    objInventoryMovemnt.ItemType = "FT"
                                Else
                                    objInventoryMovemnt.ItemType = objRemove.ItemItemType
                                End If
                                objInventoryMovemnt.Basic_Cost = Nothing
                                objInventoryMovemnt.Batch_No = ""
                                objInventoryMovemnt.MFG_Date = Nothing
                                objInventoryMovemnt.Expiry_Date = Nothing
                                objInventoryMovemnt.FAT_Per = objRemove.FAT
                                objInventoryMovemnt.FAT_KG = objRemove.FAT_KG
                                objInventoryMovemnt.SNF_Per = objRemove.SNF
                                objInventoryMovemnt.SNF_KG = objRemove.SNF_KG
                                objInventoryMovemnt.Ref_ID_Type = "6"
                                objInventoryMovemnt.Ref_ID = objRemove.PK_ID
                                qry = "select sum(FAT_KG) as FAT_KG,sum(SNF_KG) as SNF_KG,sum(FAT_AMT) as FAT_AMT,sum(SNF_AMT) as SNF_AMT from TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION where Document_No='" + obj.Document_No + "' and Against_PK_ID=" + clsCommon.myCstr(objPro.PK_ID) + " and Source_ID in (1,4)"
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objInventoryMovemnt.Fat_Rate = If(objInventoryMovemnt.FAT_KG <= 0, 0, clsCommon.myCDecimal(dt.Rows(0)("FAT_AMT")) / clsCommon.myCDecimal(dt.Rows(0)("FAT_KG")))
                                    objInventoryMovemnt.SNF_Rate = If(objInventoryMovemnt.SNF_KG <= 0, 0, clsCommon.myCDecimal(dt.Rows(0)("SNF_AMT")) / clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
                                    objInventoryMovemnt.Fat_Amt = objInventoryMovemnt.Fat_Rate * objInventoryMovemnt.FAT_KG
                                    objInventoryMovemnt.SNF_Amt = objInventoryMovemnt.SNF_Rate * objInventoryMovemnt.SNF_KG
                                    Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                                    objInventoryMovemnt.FIFO_Cost = cost
                                    objInventoryMovemnt.Avg_Cost = cost
                                    objInventoryMovemnt.LIFO_Cost = cost
                                    objInventoryMovemnt.CalculateAvgCost = False
                                End If
                                ArrInvetoryMovementNew.Add(objInventoryMovemnt)
                            Else
                                Throw New Exception("Please remove item of Product Type- MI only")
                                'Dim objInventoryMovemnt As New clsInventoryMovement()
                                'objInventoryMovemnt.InOut = "I"
                                'objInventoryMovemnt.Location_Code = objRemove.Location_Code
                                'objInventoryMovemnt.Other_Location_Code = ""
                                'objInventoryMovemnt.Other_Location_Desc = ""
                                'objInventoryMovemnt.Item_Code = objRemove.Item_Code
                                'objInventoryMovemnt.Item_Desc = objRemove.Item_Name
                                'objInventoryMovemnt.Qty = objRemove.Qty
                                'objInventoryMovemnt.UOM = objRemove.UOM
                                'objInventoryMovemnt.MRP = Nothing
                                'objInventoryMovemnt.Add_Cost = Nothing
                                'objInventoryMovemnt.Net_Cost = Nothing
                                'If clsCommon.CompairString(objRemove.ItemItemType, "R") = CompairStringResult.Equal Then
                                '    objInventoryMovemnt.ItemType = "RM"
                                'ElseIf clsCommon.CompairString(objRemove.ItemItemType, "F") = CompairStringResult.Equal Then
                                '    objInventoryMovemnt.ItemType = "FT"
                                'Else
                                '    objInventoryMovemnt.ItemType = objRemove.ItemItemType
                                'End If
                                'objInventoryMovemnt.Batch_No = ""
                                'objInventoryMovemnt.MFG_Date = Nothing
                                'objInventoryMovemnt.Expiry_Date = Nothing
                                'objInventoryMovemnt.FAT_Per = objRemove.FAT
                                'objInventoryMovemnt.FAT_KG = objRemove.FAT_KG
                                'objInventoryMovemnt.SNF_Per = objRemove.SNF
                                'objInventoryMovemnt.SNF_KG = objRemove.SNF_KG
                                'objInventoryMovemnt.Item_Status = "7" ''1-RM(Milk),2-RM(Other),3-OverheadCost,4-Add(Milk),5-Add(Other),6-Remove(Milk),7-Remove(Other)
                                'objInventoryMovemnt.Ref_ID = objRemove.PK_ID
                                'qry = "select sum(FAT_KG) as FAT_KG,sum(SNF_KG) as SNF_KG,sum(FAT_AMT) as FAT_AMT,sum(SNF_AMT) as SNF_AMT from TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION where Document_No='" + obj.Document_No + "' and Against_PK_ID=" + clsCommon.myCstr(objPro.PK_ID) + " Source_ID in (1,4)"
                                'dt = clsDBFuncationality.GetDataTable(qry, trans)
                                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                '    Dim cost As Decimal = objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt
                                '    objInventoryMovemnt.FIFO_Cost = cost
                                '    objInventoryMovemnt.Avg_Cost = cost
                                '    objInventoryMovemnt.LIFO_Cost = cost
                                '    'objInventoryMovemnt.Basic_Cost = If(objtr.issue_qty <= 0, 0, cost / objtr.issue_qty)
                                '    objInventoryMovemnt.CalculateAvgCost = False
                                '    objInventoryMovemnt.Ref_ID_Type = "7" ''1-RM(Milk),2-RM(Other),3-OverheadCost,4-Add(Milk),5-Add(Other),6-Remove(Milk),7-Remove(Other)
                                '    ArrInventoryMovement.Add(objInventoryMovemnt)
                                'End If
                            End If
                        Next
                        If ArrInvetoryMovementNew.Count > 0 Then
                            clsInventoryMovementNew.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                        End If
                        If ArrInventoryMovement.Count > 0 Then
                            clsInventoryMovement.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                        End If
                    End If
                Next

                qry = "insert into TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION( Document_No,Against_PK_ID,Source_ID,Source_Code,Qty,UOM,FAT_KG,SNF_KG,FAT_AMT,SNF_AMT,AMT)  
select TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Against_PK_ID
,case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type else TSPL_INVENTORY_MOVEMENT.Ref_ID_Type end as Source_ID
,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Item_Code as Source_Code ,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Qty,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.UOM
,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.FAT_KG,TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.SNF_KG
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt else 0 end ,0) as FAT_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt else 0 end ,0) as SNF_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then (TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt+TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt) else TSPL_INVENTORY_MOVEMENT.Avg_Cost end,0) as AMT
from TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Item_Code
left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type='6' and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.PK_ID
left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT.Ref_ID_Type='7' and TSPL_INVENTORY_MOVEMENT.Ref_ID=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.PK_ID 
where  TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Type=2 and TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No='" + obj.Document_No + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                ''In the Finish Goods Item
                ArrInventoryMovement = New List(Of clsInventoryMovement)
                ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                For Each objPro As clsProductionShiftMgmtProduction In obj.ArrPro
                    qry = "select sum(Fat_KG) as Fat_KG,sum(SNF_KG)as SNF_KG,sum(Fat_Amt)as Fat_Amt,sum(SNF_Amt)as SNF_Amt,sum(AMT) as Avg_Cost  from TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION where Document_No='" + obj.Document_No + "' and Against_PK_ID=" + clsCommon.myCstr(objPro.PK_ID) + ""
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim strProductType As String = clsItemMaster.GetItemProductType(objPro.Item_Code, trans)
                    Dim strItemType As String
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        Dim objInventoryMovemnt = New clsInventoryMovementNew
                        objInventoryMovemnt.Trans_Type = "Production"
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.Location_Code = obj.Location_Code
                        objInventoryMovemnt.Item_Code = objPro.Item_Code
                        objInventoryMovemnt.Item_Desc = objPro.Item_Name
                        objInventoryMovemnt.Qty = objPro.Qty_LTR
                        objInventoryMovemnt.UOM = "LTR"
                        objInventoryMovemnt.Source_Doc_No = obj.Document_No
                        objInventoryMovemnt.Source_Doc_Date = obj.Shift_Start_Date
                        objInventoryMovemnt.CalculateAvgCost = False
                        objInventoryMovemnt.Batch_No = objPro.Batch_No

                        'objInventoryMovemnt.FAT_Per = objProd.FAT_Per
                        'objInventoryMovemnt.SNF_Per = objProd.SNF_Per
                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(dt.Rows(0)("Fat_KG"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
                        objInventoryMovemnt.Fat_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")), clsCommon.myCDecimal(dt.Rows(0)("Fat_KG")))
                        objInventoryMovemnt.SNF_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt")), clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
                        objInventoryMovemnt.Fat_Amt = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt"))
                        objInventoryMovemnt.SNF_Amt = clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Avg_Cost"))
                        objInventoryMovemnt.Avg_Cost = AvgCost
                        objInventoryMovemnt.FIFO_Cost = AvgCost
                        objInventoryMovemnt.LIFO_Cost = AvgCost
                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
                            objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(AvgCost, objPro.Qty_LTR)
                            objInventoryMovemnt.Net_Cost = AvgCost
                        End If

                        strItemType = clsItemMaster.GetItemType(objPro.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemType = "FT"

                        End If
                        objInventoryMovemnt.ItemType = strItemType
                        objInventoryMovemnt.MFG_Date = obj.Shift_End_Date
                        objInventoryMovemnt.Ref_ID_Type = "8"
                        objInventoryMovemnt.Ref_ID = objPro.PK_ID
                        ArrInvetoryMovementNew.Add(objInventoryMovemnt)
                    Else
                        Dim objInventoryMovemnt As New clsInventoryMovement
                        objInventoryMovemnt.Trans_Type = "Production"
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.Location_Code = obj.Location_Code
                        objInventoryMovemnt.Item_Code = objPro.Item_Code
                        objInventoryMovemnt.Item_Desc = objPro.Item_Name
                        objInventoryMovemnt.Qty = objPro.Qty_LTR
                        objInventoryMovemnt.UOM = "LTR"
                        objInventoryMovemnt.Source_Doc_Date = obj.Shift_End_Date
                        objInventoryMovemnt.CalculateAvgCost = False
                        strItemType = clsItemMaster.GetItemType(objPro.Item_Code, trans)
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            strItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            strItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            strItemType = "FT"
                        End If
                        objInventoryMovemnt.ItemType = strItemType
                        objInventoryMovemnt.Batch_No = objPro.Batch_No

                        objInventoryMovemnt.FAT_KG = clsCommon.myCDecimal(dt.Rows(0)("Fat_KG"))
                        objInventoryMovemnt.SNF_KG = clsCommon.myCDecimal(dt.Rows(0)("SNF_KG"))
                        objInventoryMovemnt.Fat_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt")), clsCommon.myCDecimal(dt.Rows(0)("Fat_KG")))
                        objInventoryMovemnt.SNF_Rate = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt")), clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
                        objInventoryMovemnt.Fat_Amt = clsCommon.myCDecimal(dt.Rows(0)("Fat_Amt"))
                        objInventoryMovemnt.SNF_Amt = clsCommon.myCDecimal(dt.Rows(0)("SNF_Amt"))
                        Dim AvgCost As Decimal = clsCommon.myCDecimal(dt.Rows(0)("Avg_Cost"))
                        objInventoryMovemnt.Avg_Cost = AvgCost
                        objInventoryMovemnt.FIFO_Cost = AvgCost
                        objInventoryMovemnt.LIFO_Cost = AvgCost
                        If clsCommon.CompairString(objInventoryMovemnt.InOut, "I") = CompairStringResult.Equal Then
                            objInventoryMovemnt.Basic_Cost = clsCommon.myCDivide(AvgCost, objPro.Qty_LTR)
                            objInventoryMovemnt.Net_Cost = AvgCost
                        End If
                        objInventoryMovemnt.MFG_Date = obj.Shift_End_Date
                        objInventoryMovemnt.Ref_ID_Type = "9"
                        objInventoryMovemnt.Ref_ID = objPro.PK_ID
                        ArrInventoryMovement.Add(objInventoryMovemnt)

                        If clsItemMaster.IsBatchItem(objPro.Item_Code, trans) Then
                            Dim arrBatchItem As New List(Of clsBatchInventory)
                            Dim objBatchItem As clsBatchInventory = New clsBatchInventory()
                            objBatchItem.Batch_No = objPro.Batch_No
                            objBatchItem.Manufacture_Date = obj.Shift_End_Date
                            objBatchItem.Expiry_Date = obj.Shift_End_Date.AddDays(clsItemMaster.GetSelfLife(objPro.Item_Code, trans))
                            objBatchItem.Qty = objPro.Qty_LTR
                            objBatchItem.Manual_BatchNo = objPro.Batch_No
                            If clsCommon.myLen(objBatchItem.Batch_No) > 0 AndAlso objBatchItem.Qty <> 0 Then
                                arrBatchItem.Add(objBatchItem)
                            End If
                            clsBatchInventory.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, "I", objPro.Item_Code, obj.Location_Code, 1, 0, "LTR", arrBatchItem, trans)
                        End If
                    End If
                Next
                If ArrInvetoryMovementNew.Count > 0 Then
                    clsInventoryMovementNew.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                End If
                If ArrInventoryMovement.Count > 0 Then
                    clsInventoryMovement.SaveData(clsUserMgtCode.ProductionShiftMgmt, obj.Document_No, obj.Shift_End_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
            End If



            Dim ArryLstGLAC As ArrayList = New ArrayList()
            qry = "select xxx.InOut,xxx.Item_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,xxx.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Loss_Ac from (
select InOut,Item_Code,sum(Avg_Cost) as Avg_Cost  from(
select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No in ('" + obj.Document_No + "') 
and Trans_Type='" + clsUserMgtCode.ProductionShiftMgmt + "'
union all
select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in ('" + obj.Document_No + "') 
and Trans_Type='" + clsUserMgtCode.ProductionShiftMgmt + "'
) xx group by Item_Code,InOut 
) xxx
left join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xxx.Item_Code
left join TSPL_PURCHASE_ACCOUNTS on TSPL_ITEM_MASTER.Purchase_Class_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code
order by InOut desc"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            Dim dclGainLoss As Decimal = 0
            Dim strGainLossAccount As String = ""
            Dim strGainLossItem As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(dr("Inv_Control_Account")) <= 0 Then
                        Throw New Exception("Inventory control Account not found for Item " & clsCommon.myCstr(dr("Item_Code")) & "")
                    End If
                    If clsCommon.myLen(strGainLossAccount) <= 0 Then
                        strGainLossItem = clsCommon.myCstr(dr("Item_Code"))
                        strGainLossAccount = clsCommon.myCstr(dr("Loss_Ac"))
                    End If
                    Dim InvCtrlAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("Inv_Control_Account")), obj.Location_Code, trans)
                    Dim RI As Integer = -1
                    If clsCommon.CompairString(clsCommon.myCstr(dr("InOut")), "I") = CompairStringResult.Equal Then
                        RI = 1
                    End If
                    If clsCommon.myLen(InvCtrlAcc) > 0 Then
                        Dim Acc1() As String = {InvCtrlAcc, RI * clsCommon.myCDecimal(dr("Avg_Cost"))}
                        ArryLstGLAC.Add(Acc1)
                    End If
                    dclGainLoss += RI * clsCommon.myCDecimal(dr("Avg_Cost"))
                Next
            End If
            qry = "select Cost_Code,max(GL_Acc) as GL_Acc,sum(Amount) as Amount from (
select TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION.Source_Code as Cost_Code,TSPL_OVERHEAD_COST.GL_Acc,TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION.AMT as Amount
from TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION
left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE=TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION.Source_Code
where TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION.Document_No='" + obj.Document_No + "' and TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION.Source_ID=3
)x group by Cost_Code"
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myLen(dr("GL_Acc")) <= 0 Then
                        Throw New Exception("GL Account not found for cost code " & clsCommon.myCstr(dr("Cost_Code")) & "")
                    End If
                    Dim GLAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(clsCommon.myCstr(dr("GL_Acc")), obj.Location_Code, trans)
                    If clsCommon.myLen(GLAcc) > 0 Then
                        Dim Acc2() As String = {GLAcc, -1 * clsCommon.myCDecimal(dr("Amount"))}
                        ArryLstGLAC.Add(Acc2)
                    End If
                    dclGainLoss += (-1 * clsCommon.myCDecimal(dr("Amount")))
                Next
            End If
            If dclGainLoss <> 0 Then
                If clsCommon.myLen(strGainLossAccount) <= 0 Then
                    Throw New Exception("Gain/Loss control Account not found for Item " & strGainLossItem & "")
                End If
                Dim GLAcc As String = clsERPFuncationality.ChangeGLAccountLocationSegment(strGainLossAccount, obj.Location_Code, trans)
                Dim Acc2() As String = {GLAcc, -1 * dclGainLoss}
                ArryLstGLAC.Add(Acc2)
            End If
            If ArryLstGLAC IsNot Nothing AndAlso ArryLstGLAC.Count > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Shift_End_Date, "Shift Mgmt", "SF-MG", "Shift Mgmt", obj.Document_No, obj.Remarks, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "Journal Entry Against Production Uploader Entry- Doc No." & obj.Document_No & "", "")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim Qry As String = "select Status from TSPL_SHIFT_MGMT where Document_No='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No [" + strCode + "] not found for reverse and unpost")
            End If

            If Not clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "delete from TSPL_SHIFT_MGMT_PRODUCTION_CONSUMPTION where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SF-MG' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='" + clsUserMgtCode.ProductionShiftMgmt + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + strCode + "' and Trans_Type='" + clsUserMgtCode.ProductionShiftMgmt + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_SHIFT_MGMT set Status = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
Public Class clsProductionShiftMgmtOpen
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtOpen), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtOpen In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_OPEN", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtOpen)
        Dim arr As List(Of clsProductionShiftMgmtOpen) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_OPEN.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc  FROM TSPL_SHIFT_MGMT_OPEN 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_OPEN.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_OPEN.Location_Code 
where  TSPL_SHIFT_MGMT_OPEN.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_OPEN.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtOpen)
            Dim objTr As clsProductionShiftMgmtOpen
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtOpen
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtReceiptPlantMilk
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Shift As String
    Public Reject_Type As String
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Remarks As String

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtReceiptPlantMilk), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtReceiptPlantMilk In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Shift", objTR.Shift)
                clsCommon.AddColumnsForChange(coll, "Reject_Type", objTR.Reject_Type)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtReceiptPlantMilk)
        Dim arr As List(Of clsProductionShiftMgmtReceiptPlantMilk) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc FROM TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.Location_Code 
where  TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_RECEIPT_PLANT_MILK.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtReceiptPlantMilk)
            Dim objTr As clsProductionShiftMgmtReceiptPlantMilk
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtReceiptPlantMilk
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Shift = clsCommon.myCstr(dr("Shift"))
                objTr.Reject_Type = clsCommon.myCstr(dr("Reject_Type"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtReceiptBulkMilk
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Trans_Type As String
    Public Trans_Name As String
    Public Against_MilkTransferIn As String
    Public Against_BulkMilkSRN As String
    Public Against_Adjustment As String

    Public TankerNo As String ''Not a Table Column
    Public ReciveFrom As String ''Not a Table Column
    Public ReciveFromName As String ''Not a Table Column
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtReceiptBulkMilk), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtReceiptBulkMilk In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Trans_Type", objTR.Trans_Type)
                clsCommon.AddColumnsForChange(coll, "Against_MilkTransferIn", objTR.Against_MilkTransferIn, True)
                clsCommon.AddColumnsForChange(coll, "Against_BulkMilkSRN", objTR.Against_BulkMilkSRN, True)
                clsCommon.AddColumnsForChange(coll, "Against_Adjustment", objTR.Against_Adjustment, True)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtReceiptBulkMilk)
        Dim arr As List(Of clsProductionShiftMgmtReceiptBulkMilk) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INVENTORY_SOURCE_CODE.Name as Trans_Name 
,(case when TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Trans_Type='BulkSRN' then TSPL_Bulk_MILK_SRN.Tanker_No else (case when TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Trans_Type='MilkTransferIn' then Tspl_Gate_Entry_Details.Tanker_No else '' end) end) as Tanker_No
,(case when TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Trans_Type='BulkSRN' then TSPL_Bulk_MILK_SRN.Vendor_Code else (case when TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Trans_Type='MilkTransferIn' then Tspl_Gate_Entry_Details.ROUTE_NO else '' end) end) as ReciveFrom
,(case when TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Trans_Type='BulkSRN' then TSPL_VENDOR_MASTER.Vendor_Name else (case when TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Trans_Type='MilkTransferIn' then TSPL_BULK_ROUTE_MASTER.ROUTE_NAME else '' end) end) as ReciveFromName
FROM TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Location_Code 
left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.Code=TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Trans_Type
left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No=TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Against_Adjustment  
left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Against_MilkTransferIn
left outer join Tspl_Gate_Entry_Details on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no
left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=Tspl_Gate_Entry_Details.ROUTE_NO
left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Against_BulkMilkSRN
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Bulk_MILK_SRN.Vendor_Code
where  TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_RECEIPT_BULK_MILK.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtReceiptBulkMilk)
            Dim objTr As clsProductionShiftMgmtReceiptBulkMilk
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtReceiptBulkMilk
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Against_MilkTransferIn = clsCommon.myCstr(dr("Against_MilkTransferIn"))
                objTr.Against_BulkMilkSRN = clsCommon.myCstr(dr("Against_BulkMilkSRN"))
                objTr.Against_Adjustment = clsCommon.myCstr(dr("Against_Adjustment"))
                objTr.Trans_Type = clsCommon.myCstr(dr("Trans_Type"))
                objTr.Trans_Name = clsCommon.myCstr(dr("Trans_Name"))
                objTr.TankerNo = clsCommon.myCstr(dr("Tanker_No"))
                objTr.ReciveFrom = clsCommon.myCstr(dr("ReciveFrom"))
                objTr.ReciveFromName = clsCommon.myCstr(dr("ReciveFromName"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtProduction
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Item_Code As String
    Public Item_Name As String
    Public Batch_No As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String
    Public BOM_Code As String
    Public Entered_UOM As Integer ''1 LTR 2'KG
    Public ArrRM As List(Of clsProductionShiftMgmtProductionRM)
    Public ArrAdd As List(Of clsProductionShiftMgmtProductionItemAddRemove)
    Public ArrRemove As List(Of clsProductionShiftMgmtProductionItemAddRemove)

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtProduction), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProduction In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Batch_No", objTR.Batch_No)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommon.AddColumnsForChange(coll, "BOM_Code", objTR.BOM_Code)
                clsCommon.AddColumnsForChange(coll, "Entered_UOM", objTR.Entered_UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION", OMInsertOrUpdate.Insert, "", trans)
                objTR.PK_ID = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsProductionShiftMgmtProductionRM.SaveData(DocumentNo, objTR.PK_ID, objTR.ArrRM, trans)
                clsProductionShiftMgmtProductionItemAddRemove.SaveData(DocumentNo, objTR.PK_ID, 1, objTR.ArrAdd, trans)
                clsProductionShiftMgmtProductionItemAddRemove.SaveData(DocumentNo, objTR.PK_ID, 2, objTR.ArrRemove, trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProduction)
        Dim arr As List(Of clsProductionShiftMgmtProduction) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_PRODUCTION " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_PRODUCTION.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProduction)
            Dim objTr As clsProductionShiftMgmtProduction
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProduction
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objTr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                objTr.Entered_UOM = clsCommon.myCDecimal(dr("Entered_UOM"))
                objTr.ArrRM = clsProductionShiftMgmtProductionRM.GetData(objTr.Document_No, objTr.PK_ID, "", trans)
                objTr.ArrAdd = clsProductionShiftMgmtProductionItemAddRemove.GetData(objTr.Document_No, objTr.PK_ID, 1, "", trans)
                objTr.ArrRemove = clsProductionShiftMgmtProductionItemAddRemove.GetData(objTr.Document_No, objTr.PK_ID, 2, "", trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtProductionRM
#Region "Variables"
    Public PK_ID As Integer
    Public Against_PK_ID As Integer
    Public Document_No As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty As Decimal
    Public UOM As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstPKID As Integer, ByVal Arr As List(Of clsProductionShiftMgmtProductionRM), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProductionRM In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Against_PK_ID", AgainstPKID)
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION_RM", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProductionRM)
        Dim arr As List(Of clsProductionShiftMgmtProductionRM) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION_RM.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_PRODUCTION_RM " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_PRODUCTION_RM.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_PRODUCTION_RM.Against_PK_ID=" + clsCommon.myCstr(AgainstPKID) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION_RM.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProductionRM)
            Dim objTr As clsProductionShiftMgmtProductionRM
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProductionRM
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Against_PK_ID = clsCommon.myCstr(dr("Against_PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtProductionRMSummary
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty As Decimal
    Public UOM As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Arr As List(Of clsProductionShiftMgmtProductionRMIssue)
#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtProductionRMSummary), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProductionRMSummary In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY", OMInsertOrUpdate.Insert, "", trans)
                objTR.PK_ID = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsProductionShiftMgmtProductionRMIssue.SaveData(DocumentNo, objTR.PK_ID, objTR.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProductionRMSummary)
        Dim arr As List(Of clsProductionShiftMgmtProductionRMSummary) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION_RM_SUMMARY.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProductionRMSummary)
            Dim objTr As clsProductionShiftMgmtProductionRMSummary
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProductionRMSummary
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Arr = clsProductionShiftMgmtProductionRMIssue.GetData(strPONo, objTr.PK_ID, "", trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtProductionRMIssue
#Region "Variables"
    Public PK_ID As Integer
    Public Against_RM_Summary As Integer
    Public Document_No As String
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public ItemProductType As String
    Public ItemItemType As String
    Public Qty As Decimal
    Public UOM As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstRMSummary As Integer, ByVal Arr As List(Of clsProductionShiftMgmtProductionRMIssue), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProductionRMIssue In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Against_RM_Summary", AgainstRMSummary)
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProductionRMIssue)
        Dim arr As List(Of clsProductionShiftMgmtProductionRMIssue) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc,TSPL_ITEM_MASTER.Product_Type,TSPL_ITEM_MASTER.Item_Type
FROM TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Item_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Location_Code
where TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.Against_RM_Summary=" + clsCommon.myCstr(AgainstPKID) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION_RM_ISSUE.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProductionRMIssue)
            Dim objTr As clsProductionShiftMgmtProductionRMIssue
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProductionRMIssue
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Against_RM_Summary = clsCommon.myCstr(dr("Against_RM_Summary"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.ItemProductType = clsCommon.myCstr(dr("Product_Type"))
                objTr.ItemItemType = clsCommon.myCstr(dr("Item_Type"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsProductionShiftMgmtProductionItemAddRemove
#Region "Variables"
    Public PK_ID As Integer
    Public Against_PK_ID As Integer
    Public Document_No As String
    'Public Type As Integer ''1-Add 2-Remove
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public ItemProductType As String
    Public ItemItemType As String
    Public Qty As Decimal
    Public UOM As String
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstPKID As Integer, ByVal Type As Integer, ByVal Arr As List(Of clsProductionShiftMgmtProductionItemAddRemove), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtProductionItemAddRemove In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Against_PK_ID", AgainstPKID)
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Type", Type)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal Type As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtProductionItemAddRemove)
        Dim arr As List(Of clsProductionShiftMgmtProductionItemAddRemove) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Product_Type,TSPL_ITEM_MASTER.Item_Type,TSPL_LOCATION_MASTER.Location_Desc FROM TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Location_Code 
where  TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Against_PK_ID=" + clsCommon.myCstr(AgainstPKID) + " and TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.Type=" + clsCommon.myCstr(Type) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_PRODUCTION_ITEM_ADD_REMOVE.PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtProductionItemAddRemove)
            Dim objTr As clsProductionShiftMgmtProductionItemAddRemove
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtProductionItemAddRemove
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Against_PK_ID = clsCommon.myCstr(dr("Against_PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.ItemProductType = clsCommon.myCstr(dr("Product_Type"))
                objTr.ItemItemType = clsCommon.myCstr(dr("Item_Type"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty = clsCommon.myCDecimal(dr("Qty"))
                objTr.UOM = clsCommon.myCstr(dr("UOM"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtDisposalBulkMilk
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Trans_Type As String
    Public Trans_Name As String
    Public Against_JWOTransferMilk As String
    Public Against_BulkDispatch As String
    Public Location_Code As String
    Public Location_Name As String
    Public TankerNo As String ''Not a Table Column
    Public SendTo As String ''Not a Table Column
    Public SendToName As String ''Not a Table Column
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtDisposalBulkMilk), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtDisposalBulkMilk In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Trans_Type", objTR.Trans_Type)
                clsCommon.AddColumnsForChange(coll, "Against_JWOTransferMilk", objTR.Against_JWOTransferMilk, True)
                clsCommon.AddColumnsForChange(coll, "Against_BulkDispatch", objTR.Against_BulkDispatch, True)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtDisposalBulkMilk)
        Dim arr As List(Of clsProductionShiftMgmtDisposalBulkMilk) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.*,TSPL_ITEM_MASTER.Item_Desc ,TSPL_LOCATION_MASTER.Location_Desc,TSPL_INVENTORY_SOURCE_CODE.Name as Trans_Name 
,(case when TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Trans_Type='DispatchBS' then TSPL_DISPATCH_BULKSALE.Tanker_Code else (case when TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Trans_Type='MilkTransferJobWork' then TSPL_MILK_JOBWORK_TRANSFER_HEAD.Tanker_No else '' end) end) as Tanker_No
,(case when TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Trans_Type='DispatchBS' then TSPL_DISPATCH_BULKSALE.Customer_Code else (case when TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Trans_Type='MilkTransferJobWork' then TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location else '' end) end) as SendTo
,(case when TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Trans_Type='DispatchBS' then TSPL_CUSTOMER_MASTER.Customer_Name else (case when TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Trans_Type='MilkTransferJobWork' then TabJobLocation.Location_Desc else '' end) end) as SendToName
FROM TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Location_Code
left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.Code=TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Trans_Type
left outer join TSPL_DISPATCH_BULKSALE on TSPL_DISPATCH_BULKSALE.Document_No=TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Against_BulkDispatch 
left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_DISPATCH_BULKSALE.Customer_Code
left outer join TSPL_MILK_JOBWORK_TRANSFER_HEAD on TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code=TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Against_JWOTransferMilk
left outer join TSPL_LOCATION_MASTER as TabJobLocation on TabJobLocation.Location_Code=TSPL_MILK_JOBWORK_TRANSFER_HEAD.JobWork_location
where  TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_DISPOSAL_BULK_MILK.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtDisposalBulkMilk)
            Dim objTr As clsProductionShiftMgmtDisposalBulkMilk
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtDisposalBulkMilk
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Trans_Type = clsCommon.myCstr(dr("Trans_Type"))
                objTr.Trans_Name = clsCommon.myCstr(dr("Trans_Name"))
                objTr.Against_JWOTransferMilk = clsCommon.myCstr(dr("Against_JWOTransferMilk"))
                objTr.Against_BulkDispatch = clsCommon.myCstr(dr("Against_BulkDispatch"))
                objTr.TankerNo = clsCommon.myCstr(dr("Tanker_No"))
                objTr.SendTo = clsCommon.myCstr(dr("SendTo"))
                objTr.SendToName = clsCommon.myCstr(dr("SendToName"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtClose
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Location_Code As String
    Public Location_Name As String
    Public Item_Code As String
    Public Item_Name As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Temp As Decimal
    Public Acidity As Decimal
    Public COB As Integer
    Public Alcohol_Test As String
    Public Remarks As String

#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtClose), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtClose In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", objTR.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Temp ", objTR.Temp)
                clsCommon.AddColumnsForChange(coll, "Acidity", objTR.Acidity)
                clsCommon.AddColumnsForChange(coll, "COB", objTR.COB)
                clsCommon.AddColumnsForChange(coll, "Alcohol_Test", objTR.Alcohol_Test)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_CLOSE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal DocumentNo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtClose)
        Dim arr As List(Of clsProductionShiftMgmtClose) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_CLOSE.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc   FROM TSPL_SHIFT_MGMT_CLOSE 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_CLOSE.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_CLOSE.Location_Code 
where  TSPL_SHIFT_MGMT_CLOSE.Document_No='" + DocumentNo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_CLOSE.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtClose)
            Dim objTr As clsProductionShiftMgmtClose
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtClose
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Temp = clsCommon.myCDecimal(dr("Temp"))
                objTr.Acidity = clsCommon.myCDecimal(dr("Acidity"))
                objTr.COB = clsCommon.myCDecimal(dt.Rows(0)("COB"))
                objTr.Alcohol_Test = clsCommon.myCstr(dr("Alcohol_Test"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class



