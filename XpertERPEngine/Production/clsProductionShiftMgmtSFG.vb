Imports System.Data.SqlClient
Public Class clsProductionShiftMgmtSFG
#Region "Variables"
    Public Document_No As String
    Public Document_Date As DateTime
    Public Location_Code As String
    Public Location_Name As String
    Public Shift_Code As String
    Public Shift_Start_Date As DateTime
    Public Shift_End_Date As DateTime
    Public Status As ERPTransactionStatus = ERPTransactionStatus.Pending
    Public Posted_Date As DateTime? = Nothing
    Public ArrProSFG As List(Of clsProductionShiftMgmtSFGProduction) = Nothing
    Public ArrProRMSummary As List(Of clsProductionShiftMgmtSFGProductionRMSummary) = Nothing

#End Region
    Public Function SaveData(ByVal obj As clsProductionShiftMgmtSFG, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION where Document_No='" + obj.Document_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If clsCommon.myLen(obj.Location_Code) <= 0 Then
                Throw New Exception("Please provide location")
            End If
            If clsCommon.myLen(obj.Shift_Code) <= 0 Then
                Throw New Exception("Please provide Shift")
            End If

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.DariyProductionUploader, obj.Location_Code, obj.Document_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
            clsCommon.AddColumnsForChange(coll, "Shift_Code", obj.Shift_Code)
            clsCommon.AddColumnsForChange(coll, "Shift_Start_Date", clsCommon.GetPrintDate(obj.Shift_Start_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Shift_End_Date", clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            If isNewEntry Then
                obj.Document_No = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.ShiftMgmtSFG, "", obj.Location_Code)
                If (clsCommon.myLen(obj.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG", OMInsertOrUpdate.Update, "TSPL_SHIFT_MGMT_SFG.Document_No='" + obj.Document_No + "'", trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Document_No, "TSPL_SHIFT_MGMT_SFG", "Document_No", trans)

            clsProductionShiftMgmtSFGProduction.SaveData(obj.Document_No, obj.ArrProSFG, trans)
            clsProductionShiftMgmtSFGProductionRMSummary.SaveData(obj.Document_No, obj.ArrProRMSummary, trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function
    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsProductionShiftMgmtSFG
        Dim obj As clsProductionShiftMgmtSFG = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_SFG.*,TSPL_LOCATION_MASTER.Location_Desc as Location_Name 
FROM TSPL_SHIFT_MGMT_SFG 
left outer join TSPL_LOCATION_MASTER   on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_SFG.Location_Code
where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_SHIFT_MGMT_SFG.Document_No = (select MIN(Document_No) from TSPL_SHIFT_MGMT_SFG where 1=1  )"
            Case NavigatorType.Last
                qry += " and TSPL_SHIFT_MGMT_SFG.Document_No = (select Max(Document_No) from TSPL_SHIFT_MGMT_SFG where 1=1  )"
            Case NavigatorType.Next
                qry += " and TSPL_SHIFT_MGMT_SFG.Document_No = (select Min(Document_No) from TSPL_SHIFT_MGMT_SFG where Document_No>'" + strDocNo + "'  )"
            Case NavigatorType.Previous
                qry += " and TSPL_SHIFT_MGMT_SFG.Document_No = (select Max(Document_No) from TSPL_SHIFT_MGMT_SFG where Document_No<'" + strDocNo + "'  )"
            Case NavigatorType.Current
                qry += " and TSPL_SHIFT_MGMT_SFG.Document_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsProductionShiftMgmtSFG()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.Location_Code = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
            obj.Location_Name = clsCommon.myCstr(dt.Rows(0)("Location_Name"))
            obj.Shift_Code = clsCommon.myCstr(dt.Rows(0)("Shift_Code"))
            obj.Shift_Start_Date = clsCommon.myCDate(dt.Rows(0)("Shift_Start_Date"))
            obj.Shift_End_Date = clsCommon.myCDate(dt.Rows(0)("Shift_End_Date"))
            obj.Status = IIf(clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            If dt.Rows(0)("Posted_Date") IsNot DBNull.Value Then
                obj.Posted_Date = clsCommon.myCDate(dt.Rows(0)("Posted_Date"))
            End If
            obj.ArrProSFG = clsProductionShiftMgmtSFGProduction.GetData(obj.Document_No, "", trans)
            obj.ArrProRMSummary = clsProductionShiftMgmtSFGProductionRMSummary.GetData(obj.Document_No, "", trans)
        End If
        Return obj
    End Function
    Public Shared Function HistoryUpdate(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Return clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SHIFT_MGMT_SFG", "Document_No", "TSPL_SHIFT_MGMT_SFG_PRODUCTION", "Document_No", "TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM", "Document_No", trans)
    End Function
    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim obj As clsProductionShiftMgmtSFG = clsProductionShiftMgmtSFG.GetData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                Throw New Exception("Document No: " + strCode + " not found to Delete")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, "PRO-SFT-SFG", obj.Location_Code, obj.Document_Date, trans)
            If (obj.Status = ERPTransactionStatus.Approved) Then
                Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posted_Date, "dd/MM/yyyy"))
            End If
            clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, strCode, "TSPL_SHIFT_MGMT_SFG", "Document_No", trans)

            HistoryUpdate(strCode, trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION where Document_No='" + obj.Document_No + "'", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TSPL_SHIFT_MGMT_SFG where Document_No='" + strCode + "'", trans)
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

            Dim obj As clsProductionShiftMgmtSFG = clsProductionShiftMgmtSFG.GetData(strCode, NavigatorType.Current, trans)
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
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG", OMInsertOrUpdate.Update, "Document_No='" + obj.Document_No + "'", trans)
            HistoryUpdate(obj.Document_No, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function HitInventoryAndJV(ByVal obj As clsProductionShiftMgmtSFG, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            For Each objtr As clsProductionShiftMgmtSFGProduction In obj.ArrProSFG
                If clsCommon.myLen(objtr.BOM_Code) <= 0 Then
                    Throw New Exception("BOM not found for SFG item [" + objtr.Item_Code + "] and LTR Qty [" + clsCommon.myCstr(objtr.Qty_LTR) + "]")
                End If
                If objtr.ArrRM Is Nothing OrElse objtr.ArrRM.Count <= 0 Then
                    Throw New Exception("Raw material not found for SFG item [" + objtr.Item_Code + "] and LTR Qty [" + clsCommon.myCstr(objtr.Qty_LTR) + "]")
                End If
            Next

            For Each objtr As clsProductionShiftMgmtSFGProductionRMSummary In obj.ArrProRMSummary
                If objtr.Arr Is Nothing OrElse objtr.Arr.Count <= 0 Then
                    Throw New Exception("Please issue raw item [" + objtr.Item_Code + "(" + objtr.Item_Name + ")] , Qty [" + clsCommon.myCstr(objtr.Qty) + "] and UOM [" + objtr.UOM + "] ")
                End If
            Next

            If True Then
                Dim ArrInventoryMovement As New List(Of clsInventoryMovement)
                Dim ArrInvetoryMovementNew As New List(Of clsInventoryMovementNew)
                Dim settAllowNegativeStockInDairyProduction As Boolean = (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.AllowNegativeStockInDairyProduction, clsFixedParameterCode.AllowNegativeStockInDairyProduction, trans)) > 0)

                ''out the Raw material and Packing Item
                For Each objRMSummary As clsProductionShiftMgmtSFGProductionRMSummary In obj.ArrProRMSummary
                    For Each objRMSIssue As clsProductionShiftMgmtSFGProductionRMIssue In objRMSummary.Arr
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
                    clsInventoryMovementNew.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                End If
                If ArrInventoryMovement.Count > 0 Then
                    clsInventoryMovement.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If

                For Each objPro As clsProductionShiftMgmtSFGProduction In obj.ArrProSFG
                    ArrInventoryMovement = New List(Of clsInventoryMovement)
                    ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                    If objPro.ArrAdd IsNot Nothing AndAlso objPro.ArrAdd.Count > 0 Then
                        For Each objAdd As clsProductionShiftMgmtSFGProductionItemAddRemove In objPro.ArrAdd
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
                        clsInventoryMovementNew.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                    End If
                    If ArrInventoryMovement.Count > 0 Then
                        clsInventoryMovement.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                    End If
                Next

                ''Consumption Raw Items Source_ID=1
                qry = "insert into TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION( Document_No,Against_PK_ID,Source_ID,Source_Code,Qty,UOM,FAT_KG,SNF_KG,FAT_AMT,SNF_AMT,AMT)  
( select Document_No,Against_PK_ID,Source_ID,Item_Code as Source_Code
,case when Summry_Qty=0 then 0 else Issue_Qty*Qty/Summry_Qty end as Qty,UOM
,case when Summry_FAT_KG=0 then 0 else Issue_FAT_KG*FAT_KG/Summry_FAT_KG end as FAT_KG 
,case when Summry_SNF_KG=0 then 0 else Issue_SNF_KG*SNF_KG/Summry_SNF_KG end as SNF_KG 
,case when Summry_FAT_KG=0 then 0 else Issue_FAT_Amt*FAT_KG/Summry_FAT_KG end as FAT_Amt
,case when Summry_SNF_KG=0 then 0 else Issue_SNF_Amt*SNF_KG/Summry_SNF_KG end as SNF_Amt 
,case when Summry_Qty=0 then 0 else Issue_Amt*Qty/Summry_Qty end as Amt
from (
select TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Document_No,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Against_PK_ID
,case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type else TSPL_INVENTORY_MOVEMENT.Ref_ID_Type end as Source_ID
,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Item_Code,TSPL_ITEM_MASTER.Product_Type,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Qty,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.UOM,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.FAT_KG,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.SNF_KG,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.Qty as Summry_Qty,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.FAT_KG as Summry_FAT_KG,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.SNF_KG as Summry_SNF_KG,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.PK_ID as Issue_PK_ID,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.Qty as Issue_Qty,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.FAT_KG as Issue_FAT_KG,TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.SNF_KG as Issue_SNF_KG
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt else 0 end ,0) as Issue_FAT_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt else 0 end ,0) as Issue_SNF_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then (TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt+TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt) else TSPL_INVENTORY_MOVEMENT.Avg_Cost end,0) as Issue_Amt
from TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM
left outer join TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY on TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.Document_No=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Document_No and TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Item_Code
left outer join TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE on TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.Against_RM_Summary=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.PK_ID
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.Item_Code
left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Document_No and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type='1' and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.PK_ID
left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Document_No and TSPL_INVENTORY_MOVEMENT.Ref_ID_Type='2' and TSPL_INVENTORY_MOVEMENT.Ref_ID=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.PK_ID 
where TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Document_No='" + obj.Document_No + "'
) xx
)"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                ''Consumption Overhead Cost Source_ID=3
                qry = "insert into TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION (Document_No,Against_PK_ID,Source_ID,Source_Code,Amt)  
select '" + obj.Document_No + "' as Document_No,PK_ID,3 as Source_ID,xx.COST_CODE,(xx.prod_qty * (xx.OverHead_Cost/xx.build_qty)) as Amt from (
select TSPL_SHIFT_MGMT_SFG_PRODUCTION.PK_ID,(TSPL_SHIFT_MGMT_SFG_PRODUCTION.Qty_LTR * TabConvFatMul.Conversion_Factor/ TabConvFatDiv.Conversion_Factor) as Prod_Qty,tspl_pp_bom_head.bom_code,tspl_pp_bom_head.prod_item_code,tspl_pp_bom_head.prod_quantity as build_qty
,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.COST_CODE,TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.OverHead_Cost
from TSPL_SHIFT_MGMT_SFG_PRODUCTION
left outer join TSPL_PP_BOM_HEAD on TSPL_PP_BOM_HEAD.BOM_CODE=TSPL_SHIFT_MGMT_SFG_PRODUCTION.BOM_Code
inner join TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS on TSPL_BOM_OVERHEAD_COST_MAPPING_DETAILS.Document_Code=TSPL_PP_BOM_HEAD.BOM_CODE
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatDiv on TabConvFatDiv.Item_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatDiv.UOM_Code=TSPL_PP_BOM_HEAD.PROD_ITEM_UNIT_CODE 
left outer join TSPL_ITEM_UOM_DETAIL as  TabConvFatMul on TabConvFatMul.item_code=TSPL_PP_BOM_HEAD.PROD_ITEM_CODE and TabConvFatMul.UOM_Code='LTR' 
where TSPL_SHIFT_MGMT_SFG_PRODUCTION.Document_No='" + obj.Document_No + "'
) xx"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)




                qry = "insert into TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION( Document_No,Against_PK_ID,Source_ID,Source_Code,Qty,UOM,FAT_KG,SNF_KG,FAT_AMT,SNF_AMT,AMT)  
select TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Against_PK_ID
,case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type else TSPL_INVENTORY_MOVEMENT.Ref_ID_Type end as Source_ID
,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Item_Code as Source_Code ,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Qty,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.UOM
,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.FAT_KG,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.SNF_KG
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt else 0 end ,0) as FAT_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt else 0 end ,0) as SNF_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then (TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt+TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt) else TSPL_INVENTORY_MOVEMENT.Avg_Cost end,0) as AMT
from TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Item_Code
left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type='4' and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.PK_ID
left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT.Ref_ID_Type='5' and TSPL_INVENTORY_MOVEMENT.Ref_ID=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.PK_ID 
where  TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Type=1 and TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No='" + obj.Document_No + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                ''In Remove Item
                ArrInventoryMovement = New List(Of clsInventoryMovement)
                ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                For Each objPro As clsProductionShiftMgmtSFGProduction In obj.ArrProSFG
                    If objPro.ArrRemove IsNot Nothing AndAlso objPro.ArrRemove.Count > 0 Then
                        ArrInventoryMovement = New List(Of clsInventoryMovement)
                        ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                        For Each objRemove As clsProductionShiftMgmtSFGProductionItemAddRemove In objPro.ArrRemove
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
                                qry = "select sum(FAT_KG) as FAT_KG,sum(SNF_KG) as SNF_KG,sum(FAT_AMT) as FAT_AMT,sum(SNF_AMT) as SNF_AMT from TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION where Document_No='" + obj.Document_No + "' and Against_PK_ID=" + clsCommon.myCstr(objPro.PK_ID) + " and Source_ID in (1,4)"
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
                                'qry = "select sum(FAT_KG) as FAT_KG,sum(SNF_KG) as SNF_KG,sum(FAT_AMT) as FAT_AMT,sum(SNF_AMT) as SNF_AMT from TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION where Document_No='" + obj.Document_No + "' and Against_PK_ID=" + clsCommon.myCstr(objPro.PK_ID) + " Source_ID in (1,4)"
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
                            clsInventoryMovementNew.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                        End If
                        If ArrInventoryMovement.Count > 0 Then
                            clsInventoryMovement.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                        End If
                    End If
                Next

                qry = "insert into TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION( Document_No,Against_PK_ID,Source_ID,Source_Code,Qty,UOM,FAT_KG,SNF_KG,FAT_AMT,SNF_AMT,AMT)  
select TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Against_PK_ID
,case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type else TSPL_INVENTORY_MOVEMENT.Ref_ID_Type end as Source_ID
,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Item_Code as Source_Code ,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Qty,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.UOM
,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.FAT_KG,TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.SNF_KG
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt else 0 end ,0) as FAT_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt else 0 end ,0) as SNF_Amt
,isnull(case when TSPL_ITEM_MASTER.Product_Type='MI' then (TSPL_INVENTORY_MOVEMENT_NEW.Fat_Amt+TSPL_INVENTORY_MOVEMENT_NEW.SNF_Amt) else TSPL_INVENTORY_MOVEMENT.Avg_Cost end,0) as AMT
from TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Item_Code
left outer join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID_Type='6' and TSPL_INVENTORY_MOVEMENT_NEW.Ref_ID=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.PK_ID
left outer join TSPL_INVENTORY_MOVEMENT on TSPL_INVENTORY_MOVEMENT.Source_Doc_No=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No 
and TSPL_INVENTORY_MOVEMENT.Ref_ID_Type='7' and TSPL_INVENTORY_MOVEMENT.Ref_ID=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.PK_ID 
where  TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Type=2 and TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No='" + obj.Document_No + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                ''In the Finish Goods Item
                ArrInventoryMovement = New List(Of clsInventoryMovement)
                ArrInvetoryMovementNew = New List(Of clsInventoryMovementNew)
                For Each objPro As clsProductionShiftMgmtSFGProduction In obj.ArrProSFG
                    qry = "select sum(Fat_KG) as Fat_KG,sum(SNF_KG)as SNF_KG,sum(Fat_Amt)as Fat_Amt,sum(SNF_Amt)as SNF_Amt,sum(AMT) as Avg_Cost  from TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION where Document_No='" + obj.Document_No + "' and Against_PK_ID=" + clsCommon.myCstr(objPro.PK_ID) + ""
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim strProductType As String = clsItemMaster.GetItemProductType(objPro.Item_Code, trans)
                    Dim strItemType As String
                    If clsCommon.CompairString(strProductType, "MI") = CompairStringResult.Equal Then
                        Dim objInventoryMovemnt = New clsInventoryMovementNew
                        objInventoryMovemnt.Trans_Type = "Production"
                        objInventoryMovemnt.InOut = "I"
                        objInventoryMovemnt.Location_Code = objPro.Location_code
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
                            clsBatchInventory.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, "I", objPro.Item_Code, obj.Location_Code, 1, 0, "LTR", arrBatchItem, trans)
                        End If
                    End If
                Next
                If ArrInvetoryMovementNew.Count > 0 Then
                    clsInventoryMovementNew.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInvetoryMovementNew, trans)
                End If
                If ArrInventoryMovement.Count > 0 Then
                    clsInventoryMovement.SaveData("PRO-SFT-SFG", obj.Document_No, obj.Shift_Start_Date, clsCommon.GetPrintDate(obj.Shift_End_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                End If
            End If



            Dim ArryLstGLAC As ArrayList = New ArrayList()
            qry = "select xxx.InOut,xxx.Item_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,xxx.Avg_Cost,TSPL_PURCHASE_ACCOUNTS.Loss_Ac from (
select InOut,Item_Code,sum(Avg_Cost) as Avg_Cost  from(
select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No in ('" + obj.Document_No + "') 
and Trans_Type='" + "PRO-SFT-SFG" + "'
union all
select InOut,Item_Code,Avg_Cost from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in ('" + obj.Document_No + "') 
and Trans_Type='" + "PRO-SFT-SFG" + "'
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
select TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION.Source_Code as Cost_Code,TSPL_OVERHEAD_COST.GL_Acc,TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION.AMT as Amount
from TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION
left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE=TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION.Source_Code
where TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION.Document_No='" + obj.Document_No + "' and TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION.Source_ID=3
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
                clsJournalMaster.FunGrnlEntryWithTrans(obj.Location_Code, False, trans, obj.Shift_End_Date, "Shift Mgmt SFG", "SF-MS", "Shift Mgmt SFG", obj.Document_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstGLAC, , "Journal Entry Against Shift Mgmt SFG - Doc No." & obj.Document_No & "", "")
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
            Dim Qry As String = "select Status from TSPL_SHIFT_MGMT_SFG where Document_No='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Document No [" + strCode + "] not found for reverse and unpost")
            End If

            If Not clsCommon.myCDecimal(dt.Rows(0)("Status")) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            Qry = "delete from TSPL_SHIFT_MGMT_SFG_PRODUCTION_CONSUMPTION where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='SF-MS' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='" + "PRO-SFT-SFG" + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + strCode + "' and Trans_Type='" + "PRO-SFT-SFG" + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_SHIFT_MGMT_SFG set Status = 0 where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_SHIFT_MGMT_SFG", "Document_No", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsProductionShiftMgmtSFGProduction
#Region "Variables"
    Public PK_ID As Integer
    Public Document_No As String
    Public Item_Code As String
    Public Item_Name As String
    Public Location_code As String
    Public Location_Name As String
    Public Batch_No As String
    Public Qty_KG As Decimal
    Public Qty_LTR As Decimal
    Public FAT As Decimal
    Public SNF As Decimal
    Public FAT_KG As Decimal
    Public SNF_KG As Decimal
    Public Remarks As String
    Public BOM_Code As String
    Public Entered_UOM As Integer ''1 LTR 2'KG
    Public ArrRM As List(Of clsProductionShiftMgmtSFGProductionRM)
    Public ArrAdd As List(Of clsProductionShiftMgmtSFGProductionItemAddRemove)
    Public ArrRemove As List(Of clsProductionShiftMgmtSFGProductionItemAddRemove)

#End Region
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtSFGProduction), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtSFGProduction In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Location_code", objTR.Location_code)
                clsCommon.AddColumnsForChange(coll, "Batch_No", objTR.Batch_No)
                clsCommon.AddColumnsForChange(coll, "Qty_KG", objTR.Qty_KG)
                clsCommon.AddColumnsForChange(coll, "Qty_LTR", objTR.Qty_LTR)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Remarks", objTR.Remarks)
                clsCommon.AddColumnsForChange(coll, "BOM_Code", objTR.BOM_Code)
                clsCommon.AddColumnsForChange(coll, "Entered_UOM", objTR.Entered_UOM)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG_PRODUCTION", OMInsertOrUpdate.Insert, "", trans)
                objTR.PK_ID = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))

                clsProductionShiftMgmtSFGProductionRM.SaveData(DocumentNo, objTR.PK_ID, objTR.ArrRM, trans)
                clsProductionShiftMgmtSFGProductionItemAddRemove.SaveData(DocumentNo, objTR.PK_ID, 1, objTR.ArrAdd, trans)
                clsProductionShiftMgmtSFGProductionItemAddRemove.SaveData(DocumentNo, objTR.PK_ID, 2, objTR.ArrRemove, trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtSFGProduction)
        Dim arr As List(Of clsProductionShiftMgmtSFGProduction) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_SFG_PRODUCTION.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc FROM TSPL_SHIFT_MGMT_SFG_PRODUCTION " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION.Item_Code " + Environment.NewLine +
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION.Location_code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_SFG_PRODUCTION.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_SFG_PRODUCTION.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtSFGProduction)
            Dim objTr As clsProductionShiftMgmtSFGProduction
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtSFGProduction
                objTr.PK_ID = clsCommon.myCstr(dr("PK_ID"))
                objTr.Document_No = clsCommon.myCstr(dr("Document_No"))
                objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objTr.Item_Name = clsCommon.myCstr(dr("Item_Desc"))
                objTr.Location_code = clsCommon.myCstr(dr("Location_code"))
                objTr.Location_Name = clsCommon.myCstr(dr("Location_Desc"))
                objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                objTr.Qty_LTR = clsCommon.myCDecimal(dr("Qty_LTR"))
                objTr.Qty_KG = clsCommon.myCDecimal(dr("Qty_KG"))
                objTr.FAT = clsCommon.myCDecimal(dr("FAT"))
                objTr.SNF = clsCommon.myCDecimal(dr("SNF"))
                objTr.FAT_KG = clsCommon.myCDecimal(dr("FAT_KG"))
                objTr.SNF_KG = clsCommon.myCDecimal(dr("SNF_KG"))
                objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                objTr.BOM_Code = clsCommon.myCstr(dr("BOM_Code"))
                objTr.Entered_UOM = clsCommon.myCDecimal(dr("Entered_UOM"))
                objTr.ArrRM = clsProductionShiftMgmtSFGProductionRM.GetData(objTr.Document_No, objTr.PK_ID, "", trans)
                objTr.ArrAdd = clsProductionShiftMgmtSFGProductionItemAddRemove.GetData(objTr.Document_No, objTr.PK_ID, 1, "", trans)
                objTr.ArrRemove = clsProductionShiftMgmtSFGProductionItemAddRemove.GetData(objTr.Document_No, objTr.PK_ID, 2, "", trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class

Public Class clsProductionShiftMgmtSFGProductionRM
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
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstPKID As Integer, ByVal Arr As List(Of clsProductionShiftMgmtSFGProductionRM), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtSFGProductionRM In Arr
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtSFGProductionRM)
        Dim arr As List(Of clsProductionShiftMgmtSFGProductionRM) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.Against_PK_ID=" + clsCommon.myCstr(AgainstPKID) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtSFGProductionRM)
            Dim objTr As clsProductionShiftMgmtSFGProductionRM
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtSFGProductionRM
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

Public Class clsProductionShiftMgmtSFGProductionItemAddRemove
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
    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstPKID As Integer, ByVal Type As Integer, ByVal Arr As List(Of clsProductionShiftMgmtSFGProductionItemAddRemove), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtSFGProductionItemAddRemove In Arr
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function
    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal Type As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtSFGProductionItemAddRemove)
        Dim arr As List(Of clsProductionShiftMgmtSFGProductionItemAddRemove) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Product_Type,TSPL_ITEM_MASTER.Item_Type,TSPL_LOCATION_MASTER.Location_Desc FROM TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Item_Code 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Location_Code 
where  TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Against_PK_ID=" + clsCommon.myCstr(AgainstPKID) + " and TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.Type=" + clsCommon.myCstr(Type) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_SFG_PRODUCTION_ITEM_ADD_REMOVE.PK_ID"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtSFGProductionItemAddRemove)
            Dim objTr As clsProductionShiftMgmtSFGProductionItemAddRemove
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtSFGProductionItemAddRemove
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
Public Class clsProductionShiftMgmtSFGProductionRMSummary
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
    Public Arr As List(Of clsProductionShiftMgmtSFGProductionRMIssue)
#End Region

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal Arr As List(Of clsProductionShiftMgmtSFGProductionRMSummary), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtSFGProductionRMSummary In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_No", DocumentNo)
                clsCommon.AddColumnsForChange(coll, "Item_Code", objTR.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Qty", objTR.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", objTR.UOM)
                clsCommon.AddColumnsForChange(coll, "FAT", objTR.FAT)
                clsCommon.AddColumnsForChange(coll, "SNF", objTR.SNF)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objTR.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objTR.SNF_KG)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY", OMInsertOrUpdate.Insert, "", trans)
                objTR.PK_ID = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue("select SCOPE_IDENTITY()", trans))
                clsProductionShiftMgmtSFGProductionRMIssue.SaveData(DocumentNo, objTR.PK_ID, objTR.Arr, trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtSFGProductionRMSummary)
        Dim arr As List(Of clsProductionShiftMgmtSFGProductionRMSummary) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.*,TSPL_ITEM_MASTER.Item_Desc FROM TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY " + Environment.NewLine +
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.Item_Code " + Environment.NewLine +
            " where  TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.Document_No='" + strPONo + "' "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_SUMMARY.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtSFGProductionRMSummary)
            Dim objTr As clsProductionShiftMgmtSFGProductionRMSummary
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtSFGProductionRMSummary
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
                objTr.Arr = clsProductionShiftMgmtSFGProductionRMIssue.GetData(strPONo, objTr.PK_ID, "", trans)
                arr.Add(objTr)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsProductionShiftMgmtSFGProductionRMIssue
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

    Public Shared Function SaveData(ByVal DocumentNo As String, ByVal AgainstRMSummary As Integer, ByVal Arr As List(Of clsProductionShiftMgmtSFGProductionRMIssue), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            For Each objTR As clsProductionShiftMgmtSFGProductionRMIssue In Arr
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
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strPONo As String, ByVal AgainstPKID As Integer, ByVal strExtraWhrclas As String, ByVal trans As SqlTransaction) As List(Of clsProductionShiftMgmtSFGProductionRMIssue)
        Dim arr As List(Of clsProductionShiftMgmtSFGProductionRMIssue) = Nothing
        Dim qry As String = "SELECT TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.*,TSPL_ITEM_MASTER.Item_Desc,TSPL_LOCATION_MASTER.Location_Desc,TSPL_ITEM_MASTER.Product_Type,TSPL_ITEM_MASTER.Item_Type
FROM TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.Item_Code
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.Location_Code
where TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.Document_No='" + strPONo + "' and TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.Against_RM_Summary=" + clsCommon.myCstr(AgainstPKID) + " "
        If clsCommon.myLen(strExtraWhrclas) > 0 Then
            qry += " and " + strExtraWhrclas
        End If
        qry += " ORDER BY TSPL_SHIFT_MGMT_SFG_PRODUCTION_RM_ISSUE.PK_ID"

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            arr = New List(Of clsProductionShiftMgmtSFGProductionRMIssue)
            Dim objTr As clsProductionShiftMgmtSFGProductionRMIssue
            For Each dr As DataRow In dt.Rows
                objTr = New clsProductionShiftMgmtSFGProductionRMIssue
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








