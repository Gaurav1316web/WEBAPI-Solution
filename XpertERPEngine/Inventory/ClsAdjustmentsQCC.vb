Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class ClsAdjustmentsQCC
#Region "Variables"
    Public Against_Physical_Stock_No As String = Nothing
    Public chklocation As String = Nothing
    Public Adjustment_No As String = Nothing
    Public Production_Entry As String = Nothing
    Public Adjustment_Date As DateTime = Nothing
    Public Posting_Date As DateTime = Nothing
    Public Reference As String = Nothing
    Public Description As String = Nothing
    Public Posted As String = Nothing
    Public Reference_Document As String = Nothing
    Public Document_No As String = Nothing
    Public Unit_Code As String = Nothing
    Public ItemType As String = Nothing
    Public EMP_CODE As String = Nothing
    Public EMP_NAME As String = Nothing
    Public Customer_CODE As String = Nothing
    Public Customer_NAME As String = Nothing
    Public Created_time As String = Nothing
    Public Modified_Time As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public Challan_No As String = Nothing
    Public Challan_date As DateTime? = Nothing
    Public GateEntry_No As String = Nothing
    Public GateEntry_Date As DateTime? = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Desc As String = Nothing
    Public EntryDateTime As DateTime = Nothing
    Public Trans_Type As String = Nothing
    Public Is_Imported As Integer = 0
    Public Stock_Type As String = ""
    Dim Created_By As String = Nothing
    Dim Created_Date As DateTime = Nothing
    Dim Modify_By As String = Nothing
    Dim Modify_Date As DateTime = Nothing
    Dim Comp_Code As String = Nothing
    Dim GateEnt_No As String = ""
    Public IsMilkType As Integer = 0
    Public MainLocationCode As String = Nothing
    Public MainLocationDesc As String = Nothing
    Public isBySaleInvoice As Boolean = False ''Not a table columns
    Public Against_Item_Stock_Conversion As String = Nothing
    Public Against_Item_Stock_Conv_Doc As String = Nothing
    Public Against_Bulk_Srn_PI_adjustment As String = Nothing
    Public Against_AP_Invoice_No As String = Nothing

    Public Against_PI_No_Difference As String = Nothing
    Public Against_PI_No_Difference_Rejected As String = Nothing
    Public Against_Transfer_In_Doc_No As String = Nothing
    Public Against_Tanker_Dispatch_Doc_No As String = Nothing
    Public FromLocation As String = Nothing
    Public ToLocation As String = Nothing
    Public isAutoCreatedByMilkTransferIn As Integer = 0

    Public Adjustment_Type As String = Nothing
    Public Adjustment_Specification As String = Nothing

    Public Is_JobWork As Integer = 0
    Public Arr As List(Of ClsAdjustmentsQCCDetails) = Nothing
    Public Against_Transfer_In_Return_Doc_No As String = Nothing

#End Region
    Public Function SaveData(ByVal obj As ClsAdjustmentsQCC, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As ClsAdjustmentsQCC, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Empty Transactions", obj.Loc_Code, obj.Adjustment_Date, trans)
            ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Production Entry", obj.Loc_Code, obj.Adjustment_Date, trans)
            Else
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Store Adjustment", obj.Loc_Code, obj.Adjustment_Date, trans)
            End If

            clsSerializeInvenotry.DeleteData("IC-QC", obj.Adjustment_No, trans)
            clsBatchInventory.DeleteData("IC-QC", obj.Adjustment_No, trans)
            clsBatchInventoryNew.DeleteData("IC-QC", obj.Adjustment_No, trans)
            Dim qry As String = "delete from TSPL_ADJUSTMENT_DETAIL_QC where Adjustment_No='" + obj.Adjustment_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '======================= remove details data for Delete table during update record =============================
            qry = "delete from TSPL_ADJUSTMENT_DETAIL_QC_Delete_Data where Adjustment_No='" + obj.Adjustment_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '======================= remove details data for Delete table during update record =============================

            Dim strDocNo As String = ""
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            If clsCommon.myLen(strAdjustmentNoTemp) > 0 Then
                obj.Adjustment_No = strAdjustmentNoTemp
                isNewEntry = True
            Else
                If isNewEntry Then
                    Dim strDoc As String = ""
                    Dim strDocTrans As String = ""
                    If clsCommon.CompairString(obj.Adjustment_Type, "PRE") = CompairStringResult.Equal Then
                        strDoc = clsDocType.StoreAdjustmentProductionEntryQC
                        strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustmentProductionEntryQC
                    Else
                        strDoc = clsDocType.StoreAdjustment
                        strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustment
                    End If
                    'strDoc = clsDocType.StoreAdjustment
                    'strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustment

                    If clsCommon.myLen(strDoc) <= 0 Then
                        Throw New Exception("Document type not found")
                    End If
                    If clsCommon.myLen(strDocTrans) <= 0 Then
                        Throw New Exception("Document Transaction type not found")
                    End If
                    If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, strDoc, strDocTrans, obj.MainLocationCode)
                    Else
                        obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, strDoc, strDocTrans, obj.Loc_Code)
                    End If

                End If
            End If


            If (clsCommon.myLen(obj.Adjustment_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            ''UDL/30/07/19-000309 by balwinder on 30/07/2019
            If clsCommon.CompairString("Y", clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Posted from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No='" + obj.Adjustment_No + "'", trans))) = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Transaction :" + obj.Adjustment_No)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "AdjustType", "Adj")
            clsCommon.AddColumnsForChange(coll, "Adjustment_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "EntryDateTime", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Reference_Document", obj.Reference_Document)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "Production_Entry", obj.Production_Entry)
            clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "EMP_NAME", obj.EMP_NAME)
            clsCommon.AddColumnsForChange(coll, "Customer_CODE", obj.Customer_CODE)
            clsCommon.AddColumnsForChange(coll, "Customer_NAME", obj.Customer_NAME)
            clsCommon.AddColumnsForChange(coll, "Against_Physical_Stock_No", obj.Against_Physical_Stock_No)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)

            clsCommon.AddColumnsForChange(coll, "Adjustment_Specification", obj.Adjustment_Specification)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Type", obj.Adjustment_Type)

            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            If obj.Challan_date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Challan_date", clsCommon.GetPrintDate(obj.Challan_date, "dd/MMM/yyyy"))
            End If


            clsCommon.AddColumnsForChange(coll, "GateEntry_No", obj.GateEntry_No)
            If obj.GateEntry_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "GateEntry_Date", clsCommon.GetPrintDate(obj.GateEntry_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "GateEnt_No", obj.GateEnt_No)
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Desc", obj.Loc_Desc)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
            clsCommon.AddColumnsForChange(coll, "Is_Imported", obj.Is_Imported)
            clsCommon.AddColumnsForChange(coll, "Stock_Type", obj.Stock_Type)
            clsCommon.AddColumnsForChange(coll, "IsMilkType", obj.IsMilkType)
            clsCommon.AddColumnsForChange(coll, "MainLocationCode", obj.MainLocationCode, True)
            clsCommon.AddColumnsForChange(coll, "MainLocationDesc", obj.MainLocationDesc, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Third_Party_Location", obj.chklocation)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_Time", clsCommon.GetPrintDate(dtCurrent, "hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Against_Item_Stock_Conversion", obj.Against_Item_Stock_Conversion, True)
            clsCommon.AddColumnsForChange(coll, "Against_Item_Stock_Conv_Doc", obj.Against_Item_Stock_Conv_Doc, True)
            clsCommon.AddColumnsForChange(coll, "Against_Bulk_Srn_PI_adjustment", obj.Against_Bulk_Srn_PI_adjustment, True)
            clsCommon.AddColumnsForChange(coll, "Against_Tanker_Dispatch_Doc_No", obj.Against_Tanker_Dispatch_Doc_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_Transfer_In_Doc_No", obj.Against_Transfer_In_Doc_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_AP_Invoice_No", obj.Against_AP_Invoice_No, True)

            clsCommon.AddColumnsForChange(coll, "Against_PI_No_Difference", obj.Against_PI_No_Difference, True)
            clsCommon.AddColumnsForChange(coll, "Against_PI_No_Difference_Rejected", obj.Against_PI_No_Difference_Rejected, True)

            clsCommon.AddColumnsForChange(coll, "FromLocation", obj.FromLocation, True)
            clsCommon.AddColumnsForChange(coll, "ToLocation", obj.ToLocation, True)
            clsCommon.AddColumnsForChange(coll, "isAutoCreatedByMilkTransferIn", clsCommon.myCdbl(obj.isAutoCreatedByMilkTransferIn))
            clsCommon.AddColumnsForChange(coll, "Is_JobWork", obj.Is_JobWork)
            clsCommon.AddColumnsForChange(coll, "Against_Transfer_In_Return_Doc_No", obj.Against_Transfer_In_Return_Doc_No, True)

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", obj.Adjustment_No)
                clsCommon.AddColumnsForChange(coll, "Created_time", clsCommon.GetPrintDate(obj.Adjustment_Date, "hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_HEADER_QC", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_HEADER_QC", OMInsertOrUpdate.Update, "Adjustment_No='" + obj.Adjustment_No + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsAdjustmentsQCCDetails.SaveData(obj.Adjustment_No, obj.Loc_Code, Arr, trans, obj.Trans_Type, obj.Adjustment_Date)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Shared Function PostData(ByVal adjno As String, ByVal Type As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(adjno, Type, trans)
            ''Throw New Exception("deadlocked Occredddddd")
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetTransactionType(ByVal StrAdjustmentNo As String, ByVal trans As SqlTransaction) As String
        'Dim qry As String = "select ItemType  from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + StrAdjustmentNo + "'"
        'Dim strItemTypeTemp As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        'Dim strAdjustmentType As String = ""
        'If clsCommon.CompairString(strItemTypeTemp, "E") = CompairStringResult.Equal Then
        '    strAdjustmentType = frmAdjustmentEmpty.strCostTransaction
        'ElseIf clsCommon.CompairString(strItemTypeTemp, "FM") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemTypeTemp, "FT") = CompairStringResult.Equal Then
        '    strAdjustmentType = frmAdjustmentProduction.strCostTransaction
        'ElseIf clsCommon.CompairString(strItemTypeTemp, "RM") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemTypeTemp, "OT") = CompairStringResult.Equal Then
        '    strAdjustmentType = frmAdjustmentStore.strCostTransaction
        'Else
        '    Throw New Exception("Not a Valid Item Type")
        'End If
        'Return strAdjustmentType
        Return AdjustmentEnum.strCostTransaction
    End Function

    Shared Function PostData(ByVal StrAdjustmentNo As String, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal MakeGLEntry As Boolean = True, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
        Dim strAdjustmentType As String = GetTransactionType(StrAdjustmentNo, trans)
        Dim isCreateBulkProcPriceChartItemWise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, trans))
        Dim qry As String = ""
        Dim obj As New ClsAdjustmentsQCC()
        obj = obj.GetData(StrAdjustmentNo, strAdjustmentType, NavigatorType.Current, trans)
        If obj Is Nothing Then
            Throw New Exception("No Data Found to Post")
        End If

        If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Empty Transactions", obj.Loc_Code, obj.Adjustment_Date, trans)
        ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Production Entry", obj.Loc_Code, obj.Adjustment_Date, trans)
        Else
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Store Adjustment", obj.Loc_Code, obj.Adjustment_Date, trans)
        End If

        If clsCommon.CompairString("Y", obj.Posted) = CompairStringResult.Equal Then
            Throw New Exception("Already Posted Transaction :" + StrAdjustmentNo)
        End If

        Try
            'Dim conversion As Decimal
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            'Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            'Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            'If clsCommon.CompairString(obj.Adjustment_Type, "PRE") = CompairStringResult.Equal Then
            'Else


            For Each objtr As ClsAdjustmentsQCCDetails In obj.Arr
                Dim strItemType As String = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "A"
                ElseIf clsCommon.CompairString(strItemType, "S") = CompairStringResult.Equal Then
                    strItemTypeToSave = "S"
                ElseIf clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "O"
                Else
                    strItemTypeToSave = strItemType
                End If

                Dim objLocationDetails As New clsItemLocationDetails()
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_Code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objtr.Item_Code + " and Uom:'" + objtr.Unit_Code)
                End If
                Dim RI As Integer = 0
                If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                    RI = 1
                ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                    RI = -1
                Else
                    Throw New Exception("Transaction Type is not correct")
                End If
                objLocationDetails.Item_Code = objtr.Item_Code
                objLocationDetails.Item_Desc = objtr.Item_Description
                objLocationDetails.Location_Code = obj.Loc_Code
                objLocationDetails.Location_Desc = obj.Loc_Desc
                objLocationDetails.Item_Qty = RI * (objtr.Item_Quantity / ConvFac)
                objLocationDetails.Amount = 0
                Dim dblMRP As Double = objtr.mrp * ConvFac
                If clsCommon.CompairString(objtr.Unit_Code, "EB") = CompairStringResult.Equal Then
                    dblMRP += 100
                End If
                objLocationDetails.MRP = dblMRP
                If objtr.MFG_Date.HasValue Then
                    objLocationDetails.MFG_Date = objtr.MFG_Date
                End If
                objLocationDetails.Batch_No = objtr.Batch_No
                If objtr.Expiry_Date.HasValue Then
                    objLocationDetails.Expiry_Date = objtr.Expiry_Date
                End If
                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)

                '=====================get product type of item============================
                Dim productype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Product_Type from TSPL_ITEM_MASTER where item_code='" + objtr.Item_Code + "'", trans))

                If productype Is Nothing OrElse productype Is DBNull.Value Then
                    productype = ""
                End If
                Dim strIndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, trans))

                '===========if product type is not nilk only than go into inventory movement tableotherwise in new table======================
                'If clsCommon.CompairString(productype, "MI") <> CompairStringResult.Equal Then
                '    Dim objInventoryMovemnt As New clsInventoryMovement()
                '    If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                '        objInventoryMovemnt.InOut = "I"
                '    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                '        objInventoryMovemnt.InOut = "O"
                '    Else
                '        Throw New Exception("Transaction Type is not correct")
                '    End If
                '    objInventoryMovemnt.Location_Code = obj.Loc_Code
                '    objInventoryMovemnt.Item_Code = objtr.Item_Code
                '    objInventoryMovemnt.Item_Desc = objtr.Item_Description
                '    objInventoryMovemnt.Qty = objtr.Item_Quantity
                '    objInventoryMovemnt.UOM = objtr.Unit_Code
                '    objInventoryMovemnt.Basic_Cost = objtr.Item_Cost / IIf(objtr.Item_Quantity = 0, 1, objtr.Item_Quantity)
                '    objInventoryMovemnt.MRP = objtr.mrp
                '    objInventoryMovemnt.Add_Cost = objtr.Item_Cost
                '    objInventoryMovemnt.Net_Cost = objtr.Item_Cost
                '    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                '        objInventoryMovemnt.ItemType = "RM"
                '    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                '        objInventoryMovemnt.ItemType = "OT"
                '    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                '        objInventoryMovemnt.ItemType = "FT"
                '    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                '        objInventoryMovemnt.ItemType = "A"
                '    End If
                '    objInventoryMovemnt.ItemType = strItemTypeToSave

                '    If clsCommon.CompairString(strIndustryType, "R") = CompairStringResult.Equal Then
                '        objInventoryMovemnt.Batch_No = objtr.Bin_No
                '    Else
                '        objInventoryMovemnt.Batch_No = objtr.Batch_No
                '    End If

                '    objInventoryMovemnt.MFG_Date = objtr.MFG_Date
                '    objInventoryMovemnt.Expiry_Date = objtr.Expiry_Date
                '    objInventoryMovemnt.itemstatus = objtr.Itemstatus

                '    objInventoryMovemnt.FAT_KG = objtr.fat_kg
                '    objInventoryMovemnt.FAT_Per = objtr.fat_pers
                '    objInventoryMovemnt.SNF_KG = objtr.snf_kg
                '    objInventoryMovemnt.SNF_Per = objtr.snf_pers
                '    objInventoryMovemnt.Fat_Rate = objtr.fat_Rate
                '    objInventoryMovemnt.SNF_Rate = objtr.snf_Rate
                '    objInventoryMovemnt.Fat_Amt = objtr.fat_Amt
                '    objInventoryMovemnt.SNF_Amt = objtr.snf_Amt
                '    'objInventoryMovemnt.Cust_Code
                '    'objInventoryMovemnt.Cust_Name
                '    'objInventoryMovemnt.Vendor_Code
                '    'objInventoryMovemnt.Vendor_Name
                '    'objInventoryMovemnt.Other_Location_Code
                '    'objInventoryMovemnt.Other_Location_Desc

                '    ArrInventoryMovement.Add(objInventoryMovemnt)
                'ElseIf clsCommon.CompairString(productype, "MI") = CompairStringResult.Equal Then
                '    Dim objInventoryMovemntnew As New clsInventoryMovementNew()
                '    If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                '        objInventoryMovemntnew.InOut = "I"
                '    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                '        objInventoryMovemntnew.InOut = "O"
                '    Else
                '        Throw New Exception("Transaction Type is not correct")
                '    End If

                '    If clsCommon.myLen(obj.Against_Tanker_Dispatch_Doc_No) = 0 Then
                '        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                '            objInventoryMovemntnew.Location_Code = obj.MainLocationCode
                '        Else
                '            objInventoryMovemntnew.Location_Code = obj.Loc_Code
                '        End If
                '        objInventoryMovemntnew.main_location = obj.MainLocationCode
                '    Else
                '        ' done by priti BHA/24/07/18-000191 to update location for adjustment against milk transfer In.
                '        ''order by main_location added by balwinder on 06/12/2018 because location come in first Row.
                '        qry = "select Location_Code,main_location from tspl_inventory_movement_new where source_doc_no='" & obj.Against_Tanker_Dispatch_Doc_No & "' and Item_Code='" & objtr.Item_Code & "' order by main_location"
                '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '            objInventoryMovemntnew.Location_Code = clsCommon.myCstr(dt.Rows(0).Item("Location_Code"))
                '            objInventoryMovemntnew.main_location = clsCommon.myCstr(dt.Rows(0).Item("main_location"))
                '            clsDBFuncationality.ExecuteNonQuery("update TSPL_ADJUSTMENT_HEADER_QC set Loc_Code='" & objInventoryMovemntnew.Location_Code & "',MainLocationCode='" & objInventoryMovemntnew.main_location & "',loc_desc='" & clsLocation.GetName(objInventoryMovemntnew.Location_Code, trans) & "' where Adjustment_No='" & obj.Adjustment_No & "'", trans)
                '        End If
                '    End If

                '    objInventoryMovemntnew.Item_Code = objtr.Item_Code
                '    objInventoryMovemntnew.Item_Desc = objtr.Item_Description
                '    objInventoryMovemntnew.Qty = objtr.Item_Quantity
                '    objInventoryMovemntnew.UOM = objtr.Unit_Code
                '    objInventoryMovemntnew.Basic_Cost = objtr.Item_Cost / IIf(objtr.Item_Quantity = 0, 1, objtr.Item_Quantity)
                '    objInventoryMovemntnew.MRP = objtr.mrp
                '    objInventoryMovemntnew.Add_Cost = objtr.Item_Cost
                '    objInventoryMovemntnew.Net_Cost = objtr.Item_Cost
                '    ''richa agarwal 10/10/2014

                '    ''================
                '    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                '        objInventoryMovemntnew.ItemType = "RM"
                '    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                '        objInventoryMovemntnew.ItemType = "OT"
                '    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                '        objInventoryMovemntnew.ItemType = "FT"
                '    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                '        objInventoryMovemntnew.ItemType = "A"
                '    End If
                '    objInventoryMovemntnew.ItemType = strItemTypeToSave

                '    If clsCommon.CompairString(strIndustryType, "R") = CompairStringResult.Equal Then
                '        objInventoryMovemntnew.Batch_No = objtr.Bin_No
                '    Else
                '        objInventoryMovemntnew.Batch_No = objtr.Batch_No
                '    End If
                '    objInventoryMovemntnew.MFG_Date = objtr.MFG_Date
                '    objInventoryMovemntnew.Expiry_Date = objtr.Expiry_Date
                '    objInventoryMovemntnew.itemstatus = objtr.Itemstatus
                '    objInventoryMovemntnew.FAT_KG = objtr.fat_kg
                '    objInventoryMovemntnew.FAT_Per = objtr.fat_pers
                '    objInventoryMovemntnew.SNF_KG = objtr.snf_kg
                '    objInventoryMovemntnew.SNF_Per = objtr.snf_pers

                '    '' added by Panch Raj
                '    objInventoryMovemntnew.Fat_Rate = objtr.fat_Rate
                '    objInventoryMovemntnew.SNF_Rate = objtr.snf_Rate
                '    objInventoryMovemntnew.Fat_Amt = objtr.fat_Amt
                '    objInventoryMovemntnew.SNF_Amt = objtr.snf_Amt

                '    ArrInventoryMovementNew.Add(objInventoryMovemntnew)
                'End If

                If (clsCommon.CompairString(objtr.Adjustment_Type, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal) Then
                    If (clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal) Then
                        If clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Then
                            Dim total As Decimal
                            Dim CustType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Type_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Customer_CODE + "'", trans))
                            If clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal Then
                                total = objtr.Item_Cost
                            Else
                                total = objtr.Item_Cost + objtr.Breakage_Cost
                            End If
                        End If
                    End If
                End If
            Next

            clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)

            'If ArrInventoryMovement IsNot Nothing AndAlso ArrInventoryMovement.Count > 0 Then
            '    clsInventoryMovement.SaveData("IC-QC", obj.Adjustment_No, obj.Adjustment_Date, clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            'End If

            'If ArrInventoryMovementNew IsNot Nothing AndAlso ArrInventoryMovementNew.Count > 0 Then
            '    clsInventoryMovementNew.SaveData("IC-QC", obj.Adjustment_No, obj.Adjustment_Date, clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            'End If
            '--------------------------------------------------------------------------------------------------------------------------------------
            ''--- GL End
            '' Anubhooti 05-Dec-2014 (GL Entry should not make in case of difference entry (SRN-PI) from PI)
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
            'If MakeGLEntry = True Then
            '    If obj.isAutoCreatedByMilkTransferIn = 1 Then
            '        CreateJETransferWithBranchAc(obj, strType, trans, strVourcherNoForRecreateOnly)
            '    Else
            '        CreateJE(obj, strType, trans, strVourcherNoForRecreateOnly)
            '    End If
            'End If
            'End If
            'End If
            qry = " update TSPL_ADJUSTMENT_HEADER_QC  set Posted='Y', Posted_By = '" + objCommonVar.CurrentUserCode + "' ,Posting_Date='" + clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt") + "',modified_time='" + clsCommon.GetPrintDate(obj.Adjustment_Date, "hh:mm tt") + "' where Adjustment_No='" + obj.Adjustment_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function

    ''ERO/30/04/19-000577 by balwinder on 02/05/2019 
    Public Shared Function CreateMilkTransferAdjustmentDoc(objDisp As clsMccDispatch, obj As clsMilkTransferIn, trans As SqlTransaction) As Boolean
        Try
            Dim rcptQty As Double = 0
            Dim FatQcPer As Double = 0
            Dim SNFQcPer As Double = 0
            Dim FatValue As Double = 0
            Dim SnfValue As Double = 0
            Dim rcptAmount As Double = 0
            Dim DispAmount As Double = 0
            Dim DispNetQty As Double = 0
            Dim DispFatKg As Double = 0
            Dim DispSNFKg As Double = 0
            Dim diffamt As Double = 0
            Dim diffQty As Double = 0
            Dim dblfatkgmilkin As Double = 0
            Dim dblSNFkgmilkin As Double = 0
            Dim intDiffItem As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(distinct Item_Code) from TSPL_WEIGHMENT_CHEMBER_DETAILS where Weighment_No='" & obj.Weighment_No & "'", trans))

            Dim dblfatkg As Double = 0
            Dim dblSNFkg As Double = 0
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
            If TankerFromMaster = 0 AndAlso MCCChamberwise = 0 Then
                rcptQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Net_Weight from tspl_weighment_detail where Weighment_No='" & obj.Weighment_No & "'", trans))
                Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
                FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
                SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))
                FatValue = (objW.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
                SnfValue = (objW.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
                rcptAmount = Math.Round((FatValue + SnfValue), 2)
                diffamt = objDisp.Amount - rcptAmount
                diffQty = objDisp.Net_Qty - objW.Net_Weight
                dblfatkgmilkin = Math.Round(((objW.Net_Weight * FatQcPer) / 100), 3)
                dblSNFkgmilkin = Math.Round(((objW.Net_Weight * SNFQcPer) / 100), 3)
                dblfatkg = objDisp.FAT_KG - dblfatkgmilkin
                dblSNFkg = objDisp.SNF_KG - dblSNFkgmilkin
            Else
                Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
                If intDiffItem = 1 Then
                    For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
                        rcptQty = objTr.Net_Weight
                        FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Line_No='" & objTr.Line_No & "' and Param_Type='FAT' ", trans))
                        SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "'  and Line_No='" & objTr.Line_No & "' and Param_Type='SNF' ", trans))
                        Dim qry = "Select * from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No='" & objDisp.Chalan_NO & "' and Chamber_Description='" & objTr.Chamber_Desc & "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            DispAmount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                            DispNetQty = clsCommon.myCdbl(dt.Rows(0)("Qty_KG"))
                            DispFatKg = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
                            DispSNFKg = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                        End If
                        FatValue = (objTr.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
                        SnfValue = (objTr.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
                        rcptAmount = Math.Round((FatValue + SnfValue), 2)
                        diffamt += DispAmount - rcptAmount
                        diffQty += DispNetQty - objTr.Net_Weight
                        dblfatkgmilkin = Math.Round(((objTr.Net_Weight * FatQcPer) / 100), 3)
                        dblSNFkgmilkin = Math.Round(((objTr.Net_Weight * SNFQcPer) / 100), 3)
                        dblfatkg += DispFatKg - dblfatkgmilkin
                        dblSNFkg += DispSNFKg - dblSNFkgmilkin
                    Next
                End If
            End If

            Dim objAdjOut As New ClsAdjustmentsQCC
            objAdjOut.Adjustment_Date = obj.Receipt_Challan_Date
            objAdjOut.Posting_Date = obj.Receipt_Challan_Date
            objAdjOut.EntryDateTime = obj.Receipt_Challan_Date
            objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
            objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
            objAdjOut.Loc_Code = objDisp.MCC_Code
            objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
            objAdjOut.Trans_Type = "Out"
            objAdjOut.IsMilkType = 1
            objAdjOut.MainLocationCode = objDisp.MCC_Code
            objAdjOut.FromLocation = objDisp.MCC_Code
            objAdjOut.ToLocation = obj.location_code
            objAdjOut.isAutoCreatedByMilkTransferIn = 1
            objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
            objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
            objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
            objAdjOut.Arr = New List(Of ClsAdjustmentsQCCDetails)

            Dim objAdjIn As New ClsAdjustmentsQCC
            objAdjIn.Adjustment_Date = obj.Receipt_Challan_Date
            objAdjIn.Posting_Date = obj.Receipt_Challan_Date
            objAdjIn.EntryDateTime = obj.Receipt_Challan_Date
            objAdjIn.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
            objAdjIn.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
            objAdjIn.Loc_Code = objDisp.MCC_Code
            objAdjIn.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
            objAdjIn.Trans_Type = "In"
            objAdjIn.IsMilkType = 1
            objAdjIn.MainLocationCode = objDisp.MCC_Code
            objAdjIn.FromLocation = objDisp.MCC_Code
            objAdjIn.ToLocation = obj.location_code
            objAdjIn.isAutoCreatedByMilkTransferIn = 1
            objAdjIn.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
            objAdjIn.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
            objAdjIn.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
            objAdjIn.Arr = New List(Of ClsAdjustmentsQCCDetails)

            If intDiffItem > 1 Then
                Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
                For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
                    rcptQty = objTr.Net_Weight
                    FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Line_No='" & objTr.Line_No & "' and Param_Type='FAT' ", trans))
                    SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "'  and Line_No='" & objTr.Line_No & "' and Param_Type='SNF' ", trans))
                    Dim qry = "Select * from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No='" & objDisp.Chalan_NO & "' and Chamber_No='" & objTr.Line_No & "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        DispAmount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                        DispNetQty = clsCommon.myCdbl(dt.Rows(0)("Qty_KG"))
                        DispFatKg = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
                        DispSNFKg = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                    End If

                    FatValue = (objTr.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
                    SnfValue = (objTr.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
                    rcptAmount = Math.Round((FatValue + SnfValue), 2)
                    diffamt = DispAmount - rcptAmount
                    diffQty = DispNetQty - objTr.Net_Weight

                    dblfatkgmilkin = Math.Round(((objTr.Net_Weight * FatQcPer) / 100), 3)
                    dblSNFkgmilkin = Math.Round(((objTr.Net_Weight * SNFQcPer) / 100), 3)
                    dblfatkg = (DispFatKg - dblfatkgmilkin)
                    dblSNFkg = (DispSNFKg - dblSNFkgmilkin)

                    If diffQty > 0 OrElse diffamt > 0 OrElse dblfatkg > 0 OrElse dblSNFkg > 0 Then
                        Dim objAdjTR As New ClsAdjustmentsQCCDetails()
                        objAdjTR.Item_Code = objTr.Item_Code
                        objAdjTR.Item_Description = clsItemMaster.GetItemName(objTr.Item_Code, trans)
                        objAdjTR.Adjustment_Type = "BI"
                        If diffQty > 0 Then
                            objAdjTR.Item_Quantity = diffQty
                        End If
                        If diffamt > 0 Then
                            objAdjTR.Item_Cost = diffamt
                        End If
                        objAdjTR.mrp = 0
                        objAdjTR.Unit_Code = objTr.UOM
                        If dblfatkg > 0 Then
                            objAdjTR.fat_kg = dblfatkg
                            objAdjTR.fat_Rate = objDisp.FAT_RATE
                            objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
                            If objAdjTR.Item_Quantity > 0 Then
                                objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                            End If
                        End If
                        If dblSNFkg > 0 Then
                            objAdjTR.snf_kg = dblSNFkg
                            objAdjTR.snf_Rate = objDisp.SNF_RATE
                            objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
                            If objAdjTR.Item_Quantity > 0 Then
                                objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                            End If
                        End If
                        objAdjIn.Arr.Add(objAdjTR)
                    End If
                    If diffQty < 0 OrElse diffamt < 0 OrElse dblfatkg < 0 OrElse dblSNFkg < 0 Then
                        Dim objAdjTR As New ClsAdjustmentsQCCDetails()
                        objAdjTR.Item_Code = objTr.Item_Code
                        objAdjTR.Item_Description = clsItemMaster.GetItemName(objTr.Item_Code, trans)
                        objAdjTR.Adjustment_Type = "BI"
                        If diffQty < 0 Then
                            objAdjTR.Item_Quantity = -1 * diffQty
                        End If
                        If diffamt < 0 Then
                            objAdjTR.Item_Cost = -1 * diffamt
                        End If
                        objAdjTR.mrp = 0
                        objAdjTR.Unit_Code = objTr.UOM
                        If dblfatkg < 0 Then
                            objAdjTR.fat_kg = -1 * dblfatkg
                            objAdjTR.fat_Rate = objDisp.FAT_RATE
                            objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
                            If objAdjTR.Item_Quantity > 0 Then
                                objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                            End If
                        End If
                        If dblSNFkg < 0 Then
                            objAdjTR.snf_kg = -1 * dblSNFkg
                            objAdjTR.snf_Rate = objDisp.SNF_RATE
                            objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
                            If objAdjTR.Item_Quantity > 0 Then
                                objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                            End If
                        End If
                        objAdjOut.Arr.Add(objAdjTR)
                    End If
                Next
            Else
                Dim tnkrNo As String = objDisp.Tanker_No
                Dim Loc As String = objDisp.MCC_Code
                Dim Silo As String = ""
                Dim PostingDate As Date = obj.Receipt_Challan_Date
                If diffQty > 0 OrElse diffamt > 0 OrElse dblfatkg > 0 OrElse dblSNFkg > 0 Then
                    Dim objAdjTR As New ClsAdjustmentsQCCDetails()
                    objAdjTR.Item_Code = objDisp.Item_Code
                    objAdjTR.Item_Description = objDisp.Item_Desc
                    objAdjTR.Adjustment_Type = "BI"
                    If diffQty > 0 Then
                        objAdjTR.Item_Quantity = diffQty
                    End If
                    If diffamt > 0 Then
                        objAdjTR.Item_Cost = diffamt
                    End If
                    objAdjTR.mrp = 0
                    objAdjTR.Unit_Code = objDisp.UOM_Code
                    If dblfatkg > 0 Then
                        objAdjTR.fat_kg = dblfatkg
                        objAdjTR.fat_Rate = objDisp.FAT_RATE
                        objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
                        If objAdjTR.Item_Quantity > 0 Then
                            objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    If dblSNFkg > 0 Then
                        objAdjTR.snf_kg = dblSNFkg
                        objAdjTR.snf_Rate = objDisp.SNF_RATE
                        objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
                        If objAdjTR.Item_Quantity > 0 Then
                            objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    objAdjIn.Arr.Add(objAdjTR)
                End If
                If diffQty < 0 OrElse diffamt < 0 OrElse dblfatkg < 0 OrElse dblSNFkg < 0 Then
                    Dim objAdjTR As New ClsAdjustmentsQCCDetails()
                    objAdjTR.Item_Code = objDisp.Item_Code
                    objAdjTR.Item_Description = objDisp.Item_Desc
                    objAdjTR.Adjustment_Type = "BI"
                    If diffQty < 0 Then
                        objAdjTR.Item_Quantity = -1 * diffQty
                    End If
                    If diffamt < 0 Then
                        objAdjTR.Item_Cost = -1 * diffamt
                    End If
                    objAdjTR.mrp = 0
                    objAdjTR.Unit_Code = objDisp.UOM_Code
                    If dblfatkg < 0 Then
                        objAdjTR.fat_kg = -1 * dblfatkg
                        objAdjTR.fat_Rate = objDisp.FAT_RATE
                        objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
                        If objAdjTR.Item_Quantity > 0 Then
                            objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    If dblSNFkg < 0 Then
                        objAdjTR.snf_kg = -1 * dblSNFkg
                        objAdjTR.snf_Rate = objDisp.SNF_RATE
                        objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
                        If objAdjTR.Item_Quantity > 0 Then
                            objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
                        End If
                    End If
                    objAdjOut.Arr.Add(objAdjTR)
                End If
            End If

            If objAdjIn.Arr.Count > 0 Then
                objAdjIn.SaveData(objAdjIn, True, "", trans)
                ClsAdjustmentsQCC.PostData(objAdjIn.Adjustment_No, objAdjIn.Trans_Type, trans, True)
            End If
            If objAdjOut.Arr.Count > 0 Then
                objAdjOut.SaveData(objAdjOut, True, "", trans)
                ClsAdjustmentsQCC.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJE(ByVal obj As ClsAdjustmentsQCC, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
        Dim isCreateGLTransaction As Boolean = IIf(clsCommon.CompairString(clsCommon.myCstr(obj.GateEnt_No), "I") = CompairStringResult.Equal, False, True)
        Dim strAdjAcc As String = String.Empty
        Dim strEmpty As String = String.Empty
        Dim qry As String = String.Empty
        Dim strsegment As String = ""
        Dim strInvAcc As String
        Dim ArryLstFinal As ArrayList = New ArrayList()
        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
            strsegment = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.MainLocationCode + "'", trans)
        Else
            strsegment = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.Loc_Code + "'", trans)
        End If

        Dim strSourceCode As String = "IC-QC"
        Dim strSourceCodeName As String = "I/C Production QC"
        Dim desc As String = String.Empty
        If clsCommon.CompairString(strType, "Adjustment Entry") = CompairStringResult.Equal Then
            desc = "Adjustment Against " + obj.Adjustment_No
        ElseIf clsCommon.CompairString(strType, "Empty Transactions") = CompairStringResult.Equal Then
            desc = "Empty transaction Against " + obj.Adjustment_No
        ElseIf clsCommon.CompairString(strType, "Production Entry") = CompairStringResult.Equal Then
            desc = "Production Entry Against " + obj.Adjustment_No
        ElseIf clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            desc = "Store Adjustment Against " + obj.Adjustment_No
        End If

        Dim SettCreateOpeningEntryAutomatically As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)) > 0)
        If SettCreateOpeningEntryAutomatically Then ''TEC/02/11/18-000364 by balwinder on 21/11/2018
            SettCreateOpeningEntryAutomatically = False
            'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
            If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                If obj.Adjustment_Date <= dtERPStartDate Then
                    SettCreateOpeningEntryAutomatically = True
                    strSourceCode = "GL-JE"
                    strSourceCodeName = "GENERAL ENTRY"
                    desc += "[Item Opening]"
                End If
            Else
                Throw New Exception("Please set ERP Start Date")
            End If
        End If




        If clsCommon.myLen(obj.Customer_CODE) > 0 Then
            strAdjAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit FROM TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON  TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account where Cust_Code='" + obj.Customer_CODE + "'", trans))
            If clsCommon.myLen(strAdjAcc) <= 0 Then
                Throw New Exception("Please set Container Deposit Account of customer " + obj.Customer_CODE)
            End If
            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
        End If
        For Each objtr As ClsAdjustmentsQCCDetails In obj.Arr
            Dim ArryLst As ArrayList = New ArrayList()
            Dim dtPurchaseAccountSet As DataTable = clsDBFuncationality.GetDataTable("select   Inv_Control_Account  AS Inv_Control_Account , Adjustment_Account as Adjustment_Account,Item_Opening_Clearing,Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Physical_Inv_Adjustment from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')", trans)
            If dtPurchaseAccountSet Is Nothing AndAlso dtPurchaseAccountSet.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + objtr.Item_Code)
            End If
            Dim strItemProductType As String = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
            If clsCommon.myLen(obj.EMP_CODE) > 0 AndAlso clsCommon.myLen(obj.Customer_CODE) <= 0 AndAlso clsCommon.myLen(obj.Reference_Document) <= 0 Then
                qry = "select GL_Account from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + obj.EMP_CODE + "'"
                Dim strEmpGLAC As String = clsDBFuncationality.getSingleValue(qry, trans)
                If clsCommon.myLen(strEmpGLAC) <= 0 Then
                    Throw New Exception("Plese map Employee Gl Account for employee " + obj.EMP_CODE)
                End If
                For ii As Integer = 0 To dtPurchaseAccountSet.Rows.Count - 1
                    dtPurchaseAccountSet.Rows(ii)("Adjustment_Account") = strEmpGLAC
                Next
            End If
            ''--- GL Begins Now
            If clsCommon.CompairString(objtr.Adjustment_Type, "QI") = CompairStringResult.Equal Then
                'No need to make Journal Entry
            ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "QD") = CompairStringResult.Equal Then
                'No need to make Journal Entry
            ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "CI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal Then
                If clsCommon.myLen(obj.Customer_CODE) <= 0 Then
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                        ArryLst.Add(Acc1)
                    Else
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                        ArryLst.Add(Acc1)
                        ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                        If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                            clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, "", strInvAcc, "", trans)
                        Else
                            clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "", trans)
                        End If
                        '------------------
                    End If


                    If clsCommon.myLen(obj.Against_AP_Invoice_No) > 0 Then
                        Dim objVIH As clsVedorInvoiceHead = clsVedorInvoiceHead.GetData(obj.Against_AP_Invoice_No, "", trans)
                        For Each objVID As clsVedorInvoiceDetail In objVIH.Arr
                            Dim intCount As Integer = obj.Arr.Count
                            Dim dblLedgeerNonRecoverableAmt As Double = objVIH.GetTaxAmtNonShared(objVID, trans)
                            Dim dblAddionalCost As Double = Math.Round((objVIH.Total_Add_Charge / intCount), 6)
                            Dim tempAmt As Double = objVID.Amount_less_Discount + dblAddionalCost + dblLedgeerNonRecoverableAmt
                            ''richa agarwal 21 /12/2016
                            objVID.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVID.GL_Account_Code, strsegment, True, trans)
                            ''----------
                            Dim AccInvDR1() As String = {objVID.GL_Account_Code, -1 * tempAmt}
                            ArryLst.Add(AccInvDR1)
                        Next
                    ElseIf SettCreateOpeningEntryAutomatically Then
                        strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Item_Opening_Clearing"))
                        If clsCommon.myLen(strAdjAcc) <= 0 Then
                            Throw New Exception("Please set Item Opening Clearing Account of purchase account set [" + clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Purchase_Class_Code")) + "]")
                        End If
                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    Else
                        strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Adjustment_Account"))
                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    End If
                Else
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                        ArryLst.Add(Acc1)
                    Else
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                        ArryLst.Add(Acc1)
                        ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                        If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                            clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, "", strInvAcc, "", trans)
                        Else
                            clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "", trans)
                        End If
                        '------------------
                    End If


                    Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                    ArryLst.Add(Acc2)
                End If
            ElseIf (clsCommon.CompairString(objtr.Adjustment_Type, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal) Then
                If (clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Then
                        strInvAcc = clsDBFuncationality.getSingleValue("select Inv_Control_Account AS Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code = '" + objtr.Item_Code + "')", trans)
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, obj.Loc_Code, trans)
                        Dim strbreakage As String = clsDBFuncationality.getSingleValue("select breakage_gl_account AS Inv_Control_Account ,Adjustment_Account as Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code= '" + objtr.Item_Code + "')", trans)
                        If objtr.Breakage_Cost > 0 Then
                            strbreakage = clsERPFuncationality.ChangeGLAccountLocationSegment(strbreakage, obj.Loc_Code, trans)
                        End If

                        Dim total As Decimal
                        Dim CustType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Type_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Customer_CODE + "'", trans))
                        If clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal Then
                            total = objtr.Item_Cost
                        Else
                            total = objtr.Item_Cost + objtr.Breakage_Cost
                        End If
                        If objtr.Breakage_Cost = 0 Then
                            Dim inventoryaccount() As String
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                inventoryaccount = {strInvAcc, objtr.Item_Cost}
                            Else
                                inventoryaccount = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            End If
                            Dim receivableaccount() As String = {strAdjAcc, total * -1}
                            ArryLst.Add(inventoryaccount)
                            ArryLst.Add(receivableaccount)
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                                ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                                If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                    clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, "", strInvAcc, "", trans)
                                Else
                                    clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "", trans)
                                End If
                                '------------------
                            End If
                        Else
                            Dim inventoryaccount() As String
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                inventoryaccount = {strInvAcc, objtr.Item_Cost}
                            Else
                                inventoryaccount = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            End If
                            Dim receivableaccount() As String = {strAdjAcc, total * -1}
                            Dim breakageaccount() As String = {strbreakage, objtr.Breakage_Cost}
                            ArryLst.Add(breakageaccount)
                            ArryLst.Add(inventoryaccount)
                            ArryLst.Add(receivableaccount)
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                                ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                                If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                    clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, "", strInvAcc, "", trans)
                                Else
                                    clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "", trans)
                                End If
                            End If
                        End If
                    End If
                ElseIf clsCommon.myLen(obj.Against_Physical_Stock_No) > 0 Then
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select GL_Account,GL_Account_Inventroy_Ctrl from TSPL_PHYSICAL_STOCK where Physical_No='" + obj.Against_Physical_Stock_No + "' and Item_Code='" + objtr.Item_Code + "'", trans)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        strInvAcc = clsCommon.myCstr(dtTemp.Rows(0)("GL_Account_Inventroy_Ctrl"))
                        strAdjAcc = clsCommon.myCstr(dtTemp.Rows(0)("GL_Account"))
                        If clsCommon.myLen(strInvAcc) <= 0 Then
                            Throw New Exception("Please enter GL Account in Physical Stock No" + obj.Against_Physical_Stock_No)
                        End If
                        If clsCommon.myLen(strAdjAcc) <= 0 Then
                            Throw New Exception("Please enter Inventory GL Account in Physical Stock No" + obj.Against_Physical_Stock_No)
                        End If
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                            ArryLst.Add(Acc1)
                        Else
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            ArryLst.Add(Acc1)
                            'Ticket No- TEC/12/03/19-000442 sanjay
                            clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "I", trans)
                            'Ticket No- TEC/12/03/19-000442 sanjay
                        End If
                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    End If
                Else
                    '23/10/2012--Applied Condition I.e-If Cust Type in ('F','S') Then does not create Breakage Account
                    Dim CustType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Type_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Customer_CODE + "'", trans))
                    If Not (clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal) Then
                        Dim strbreakage As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select breakage_gl_account AS Inv_Control_Account ,Adjustment_Account as Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code= '" + objtr.Item_Code + "')", trans))
                        If clsCommon.myCdbl(objtr.Breakage_Cost) > 0 Then
                            strbreakage = clsERPFuncationality.ChangeGLAccountLocationSegment(strbreakage, strsegment, True, trans)
                            Dim BreakageAccount() As String = {strbreakage, objtr.Breakage_Cost}
                            ArryLst.Add(BreakageAccount)
                        End If
                    End If

                    '-------Added By Pankaj on 17/10/2012--------------------Fwd By---Ranjana Mam
                    Dim TotalAmt As Decimal
                    If clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal Then
                        TotalAmt = objtr.Item_Cost
                    Else
                        TotalAmt = objtr.Item_Cost + objtr.Breakage_Cost
                    End If
                    '-----------------------------------------------------------
                    If clsCommon.myLen(obj.Customer_CODE) <= 0 Then
                        strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                            ArryLst.Add(Acc1)
                        Else
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            ArryLst.Add(Acc1)

                            ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                            If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, "", strInvAcc, "", trans)
                            Else
                                clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "", trans)
                            End If
                        End If

                        If SettCreateOpeningEntryAutomatically Then
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Item_Opening_Clearing"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set Item Opening Clearing Account of purchase account set [" + clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Purchase_Class_Code")) + "]")
                            End If
                            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                            Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                            ArryLst.Add(Acc2)
                        ElseIf (clsCommon.CompairString(obj.Reference_Document, "MSE-MCC-OUT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Reference_Document, "MSE-PLT-IN") = CompairStringResult.Equal) Then
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Physical_Inv_Adjustment"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set Physical Inventory Adjustment Account of purchase account set [" + clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Purchase_Class_Code")) + "]")
                            End If

                            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                            Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                            ArryLst.Add(Acc2)
                        Else
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Adjustment_Account"))
                            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                            Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                            ArryLst.Add(Acc2)
                        End If
                    Else
                        strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                            ArryLst.Add(Acc1)
                        Else
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            ArryLst.Add(Acc1)

                            ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                            If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, "", strInvAcc, "", trans)
                            Else
                                clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "", trans)
                            End If
                        End If
                        Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                        ArryLst.Add(Acc2)
                    End If
                End If
            End If
            If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                For Each Str() As String In ArryLst
                    ''richa agarwal 06,Dec
                    If Str.Length = 9 Then
                        Dim strNew() As String = {Str(0), -1 * Str(1), "", "", "", "", "", "", "I"}
                        ArryLstFinal.Add(strNew)
                    Else
                        Dim strNew() As String = {Str(0), -1 * Str(1)}
                        ArryLstFinal.Add(strNew)
                    End If
                Next
            Else
                For Each Str() As String In ArryLst
                    ''richa agarwal 06,Dec
                    If Str.Length = 9 Then
                        Dim strNew() As String = {Str(0), Str(1), "", "", "", "", "", "", "I"}
                        ArryLstFinal.Add(strNew)
                    Else
                        Dim strNew() As String = {Str(0), Str(1)}
                        ArryLstFinal.Add(strNew)
                    End If

                Next
            End If
        Next

        If clsCommon.CompairString(strType, "Empty Transactions") = CompairStringResult.Equal Then
            If Not clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal Then
                If ArryLstFinal IsNot Nothing AndAlso ArryLstFinal.Count > 0 AndAlso isCreateGLTransaction Then
                    Dim strRemarks As String = "Vehicle No:" + obj.Vehicle_No + ", Challan No:" + obj.Challan_No + ", Challan Date:"
                    If obj.Challan_date.HasValue Then
                        strRemarks += " " + clsCommon.GetPrintDate(obj.Challan_date, "dd/MM/yyyy")
                    End If
                    strRemarks += ", Gate EntryNo:" + obj.GateEntry_No + ", GateEntry Date:"
                    If obj.GateEntry_Date.HasValue Then
                        strRemarks += " " + clsCommon.GetPrintDate(obj.GateEntry_Date, "dd/MM/yyyy")
                    End If
                    If obj.Is_Imported = 0 Then
                        transportSql.FunGrnlEntryWithTrans(SettCreateOpeningEntryAutomatically, 0, "", "N", obj.Loc_Code, False, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, strSourceCode, strSourceCodeName, obj.Adjustment_No, obj.Description, IIf(clsCommon.myLen(obj.Customer_CODE) > 0, "C", "O"), obj.Customer_CODE, obj.Customer_NAME, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, strRemarks)
                    End If
                End If
            End If
        ElseIf clsCommon.CompairString(strType, "Production Entry") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                transportSql.FunGrnlEntryWithTrans(SettCreateOpeningEntryAutomatically, 0, "", "N", obj.Loc_Code, False, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, strSourceCode, strSourceCodeName, obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
            End If
        ElseIf clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                transportSql.FunGrnlEntryWithTrans(SettCreateOpeningEntryAutomatically, 0, "", "N", IIf(clsCommon.myLen(obj.Loc_Code) <= 0, obj.MainLocationCode, obj.Loc_Code), False, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, strSourceCode, strSourceCodeName, obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
            End If
        End If
        Return True
    End Function

    Public Shared Function CreateJETransferWithBranchAc(ByVal obj As ClsAdjustmentsQCC, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            Dim strAdjAcc As String = String.Empty
            Dim strEmpty As String = String.Empty
            Dim qry As String = String.Empty
            Dim strInvAcc As String
            Dim ArryLstFinal As ArrayList = New ArrayList()
            Dim strsegment As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.Loc_Code + "'", trans)
            Dim FromLocSeg As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.FromLocation + "'", trans)
            Dim ToLocSeg As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.ToLocation + "'", trans)
            Dim desc As String = String.Empty
            'If clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            desc = "Auto Store Adjustment Against " + obj.Adjustment_No
            'End If
            Dim ArryLst As ArrayList = New ArrayList()
            For Each objtr As ClsAdjustmentsQCCDetails In obj.Arr
                Dim dtPurchaseAccountSet As DataTable = clsDBFuncationality.GetDataTable("select   Inv_Control_Account  AS Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')", trans)
                Dim Branch_Ac As String = String.Empty
                'Asked By Ashok sir
                Dim PickTCAForStockTransferAndTankerDispatch As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickTCAForStockTransferAndTankerDispatch, clsFixedParameterCode.PickTCAForStockTransferAndTankerDispatch, trans)) = 0, False, True)
                If PickTCAForStockTransferAndTankerDispatch = True Then
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objtr.Item_Code + "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Transfer Clearing Account For  for item " + objtr.Item_Code + "  (" & objtr.Item_Description & ")")
                    End If
                    Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, strsegment, True, trans)
                Else
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & FromLocSeg & "' and to_location='" & ToLocSeg & "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & ToLocSeg & " To " & FromLocSeg)
                    End If
                End If

                If dtPurchaseAccountSet Is Nothing AndAlso dtPurchaseAccountSet.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + objtr.Item_Code)
                End If

                Dim strItemProductType As String = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)

                ''--- GL Begins Now
                If clsCommon.CompairString(objtr.Adjustment_Type, "QI") = CompairStringResult.Equal Then
                    'No need to make Journal Entry
                ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "QD") = CompairStringResult.Equal Then
                    'No need to make Journal Entry
                ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "CI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BI") = CompairStringResult.Equal Then
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    ''richa agarwal 11/01/2016
                    'strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    Dim IGnoreGITAccount As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IGnoreGITAccount, clsFixedParameterCode.IGnoreGITAccount, trans))
                    '' on discussion with amit sir: GIT accounts will be added on setting based 
                    'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                    ToLocSeg = clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in ( Select isnull(GIT_Location,'') from TSPL_LOCATION_MASTER where Location_Code='" + obj.FromLocation + "')", trans)
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    ''---------------------------
                    '' richa inventory account debit and branch account credit in case of cost increase and BI as per ranjana mam
                    Dim Acc1() As String = {strInvAcc, 1 * objtr.Item_Cost, "", "", "", "", "", "", "I"}
                    ArryLst.Add(Acc1)

                    ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                    clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, "", strInvAcc, "", trans)
                    '------------------
                    Dim Acc2() As String = {Branch_Ac, objtr.Item_Cost * -1}
                    ArryLst.Add(Acc2)
                    'End If



                    transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "IC-QC", "I/C Adjustments", obj.Adjustment_No, obj.Description, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Reference, "")
                ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    '' richa inventory account credit and branch account debit  in case of cost decrease and BD as per ranjana mam
                    Dim Acc1() As String = {strInvAcc, objtr.Item_Cost * -1, "", "", "", "", "", "", "I"}
                    ArryLst.Add(Acc1)
                    ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                    clsInventoryMovement.UpdateInvControlAccount(obj.Adjustment_No, "IC-QC", objtr.Item_Code, strInvAcc, "", "", trans)

                    Dim Acc2() As String = {Branch_Ac, 1 * objtr.Item_Cost}
                    ArryLst.Add(Acc2)
                    transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "IC-QC", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Reference, "")
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetEmptyAdjustmentCode(ByVal strInvoiceNo As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER_QC where ItemType='E' and Reference_Document='Sale Invoice' and Document_No='" + strInvoiceNo + "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function
    Public Shared Function GetIsMilkType(ByVal strAdjNo As String, ByVal trans As SqlTransaction) As Integer
        Dim qry As String = "select IsMilkType from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No='" & strAdjNo & "'"
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

    Public Shared Function CreateAndPostEmptyReceiptOfSalesInvoiceOfTransfer(ByVal strSaleInvoiceNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim adjNo As String = CreateEmptyReceiptOfSalesInvoiceOfTransfer(strSaleInvoiceNo, trans)
            ''PostData(adjNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateEmptyReceiptOfSalesInvoiceOfTransfer(ByVal strSaleInvoiceNo As String, ByVal trans As SqlTransaction) As String
        Dim obj As ClsAdjustmentsQCC = New ClsAdjustmentsQCC()
        Dim objSaleInv As New clsSaleHead()
        objSaleInv = objSaleInv.GetData(strSaleInvoiceNo, trans)
        If objSaleInv IsNot Nothing AndAlso clsCommon.myLen(objSaleInv.Sale_Invoice_No) > 0 Then
            Dim LineNo As Integer = 1
            obj.Adjustment_Date = objSaleInv.Sale_Invoice_Date
            obj.Posting_Date = objSaleInv.Sale_Invoice_Date
            obj.Reference = objSaleInv.Ref_No
            obj.Description = objSaleInv.Description
            obj.Reference_Document = "Sale Invoice"
            obj.Document_No = strSaleInvoiceNo
            obj.Unit_Code = "ALL"
            obj.ItemType = "E"
            obj.EMP_CODE = objSaleInv.Salesman_Code
            obj.EMP_NAME = clsEmployeeMaster.GetName(objSaleInv.Salesman_Code, trans)
            obj.Customer_CODE = objSaleInv.Cust_Code
            obj.Customer_NAME = objSaleInv.Cust_Name
            obj.Created_time = objSaleInv.Date_Time_Removal
            obj.Modified_Time = objSaleInv.Date_Time_Removal
            obj.Vehicle_Code = objSaleInv.Vehicle_Code
            obj.Vehicle_No = objSaleInv.Vehicle_No
            obj.Trans_Type = "In"
            obj.Loc_Code = objSaleInv.Location
            obj.Loc_Desc = clsLocation.GetName(objSaleInv.Location, trans)
            obj.Arr = New List(Of ClsAdjustmentsQCCDetails)
            For Each objSalesTr As clsSaleDetail In objSaleInv.Arr
                If clsCommon.myLen(objSalesTr.Item_Code) > 0 AndAlso objSalesTr.Shipped_Qty > 0 AndAlso clsCommon.myCdbl(objSalesTr.Empty_Value) > 0 Then
                    Dim objtr As New ClsAdjustmentsQCCDetails()
                    objtr.Adjustment_Line_No = LineNo
                    objtr.Item_Code = clsItemMaster.GetFatherCode(objSalesTr.Item_Code, trans)
                    If clsCommon.myLen(objtr.Item_Code) <= 0 Then
                        Throw New Exception("Father code not found of item:" + objSalesTr.Item_Code)
                    End If
                    objtr.Item_Description = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                    objtr.Adjustment_Type = "BI"
                    objtr.Location_Code = obj.Loc_Code
                    objtr.Item_Quantity = objSalesTr.Shipped_Qty
                    objtr.Item_Cost = objSalesTr.Empty_Value
                    Dim strUOM As String = "EC"
                    If clsCommon.CompairString("FB", objSalesTr.Unit_code) = CompairStringResult.Equal Then
                        strUOM = "EB"
                    End If
                    objtr.Unit_Code = strUOM
                    Dim qry As String = "select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')"
                    objtr.Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(objtr.Account_Code) <= 0 Then
                        Throw New Exception("Please set Adjustment Account of purchase Account set of Item" + objtr.Item_Code)
                    End If
                    objtr.Account_Description = clsGLAccount.GetName(objtr.Account_Code, trans)
                    ''objtr.Remarks = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.Comments = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    objtr.mrp = objSalesTr.Empty_Value / objSalesTr.Shipped_Qty
                    ''objtr.MFG_Date = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(col))
                    ''objtr.Batch_No = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.Expiry_Date = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.Breakage_Cost = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    objtr.ItemType = obj.ItemType
                    ''objtr.BreakageType = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.LeakageQty = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    LineNo = LineNo + 1
                    obj.Arr.Add(objtr)

                End If
            Next
            obj.SaveData(obj, True, "", trans)
        End If
        Return obj.Adjustment_No
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal AdjustmentType As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, Optional ByVal isJobWorkConsuption As Boolean = False, Optional ByVal isProductionEntry As Boolean = False) As ClsAdjustmentsQCC

        Dim obj As ClsAdjustmentsQCC = Nothing
        Dim qry As String = "SELECT * from TSPL_ADJUSTMENT_HEADER_QC where 2=2"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        If isJobWorkConsuption Then
            whrClas += " and Reference_Document in ( 'JWO-SRN-JLO','JWO-SRN-JLI','JWO-SRN-RET-JLO','JWO-SRN-RET-JLI') "
        End If
        If isProductionEntry = True Then
            whrClas += " and Adjustment_Type in ( 'PRE') "
        Else
            whrClas += " and Adjustment_Type not in ( 'PRE') "
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = (select MIN(Adjustment_No) from TSPL_ADJUSTMENT_HEADER_QC where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = (select Max(Adjustment_No) from TSPL_ADJUSTMENT_HEADER_QC where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = (select Min(Adjustment_No) from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = (select Max(Adjustment_No) from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ADJUSTMENT_HEADER_QC.Adjustment_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAdjustmentsQCC()
            obj.chklocation = clsCommon.myCstr(dt.Rows(0)("Third_Party_Location"))
            obj.Adjustment_No = clsCommon.myCstr(dt.Rows(0)("Adjustment_No"))
            obj.Adjustment_Date = clsCommon.myCDate(dt.Rows(0)("Adjustment_Date"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            obj.Production_Entry = clsCommon.myCstr(dt.Rows(0)("Production_Entry"))
            obj.Reference_Document = clsCommon.myCstr(dt.Rows(0)("Reference_Document"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            obj.ItemType = clsCommon.myCstr(dt.Rows(0)("ItemType"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.Created_time = clsCommon.myCstr(dt.Rows(0)("Created_time"))
            obj.Modified_Time = clsCommon.myCstr(dt.Rows(0)("Modified_Time"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            obj.Customer_CODE = clsCommon.myCstr(dt.Rows(0)("Customer_CODE"))
            obj.Customer_NAME = clsCommon.myCstr(dt.Rows(0)("Customer_NAME"))
            If dt.Rows(0)("Challan_date") Is DBNull.Value Then
                obj.Challan_date = Nothing
            Else
                obj.Challan_date = clsCommon.myCDate(dt.Rows(0)("Challan_date"))
            End If
            obj.GateEntry_No = clsCommon.myCstr(dt.Rows(0)("GateEntry_No"))
            If dt.Rows(0)("GateEntry_Date") Is DBNull.Value Then
                obj.GateEntry_Date = Nothing
            Else
                obj.GateEntry_Date = clsCommon.myCDate(dt.Rows(0)("GateEntry_Date"))
            End If
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Desc = clsCommon.myCstr(dt.Rows(0)("Loc_Desc"))
            obj.EntryDateTime = clsCommon.myCDate(dt.Rows(0)("EntryDateTime"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.GateEnt_No = clsCommon.myCstr(dt.Rows(0)("GateEnt_No"))
            obj.Is_Imported = clsCommon.myCdbl(dt.Rows(0)("Is_Imported"))
            obj.Stock_Type = clsCommon.myCstr(dt.Rows(0)("Stock_Type"))
            obj.IsMilkType = clsCommon.myCdbl(dt.Rows(0)("IsMilkType"))
            obj.MainLocationCode = clsCommon.myCstr(dt.Rows(0)("MainLocationCode"))
            obj.MainLocationDesc = clsCommon.myCstr(dt.Rows(0)("MainLocationDesc"))
            obj.Against_Item_Stock_Conversion = clsCommon.myCstr(dt.Rows(0)("Against_Item_Stock_Conversion"))
            obj.Against_Item_Stock_Conv_Doc = clsCommon.myCstr(dt.Rows(0)("Against_Item_Stock_Conv_Doc"))
            obj.Against_Bulk_Srn_PI_adjustment = clsCommon.myCstr(dt.Rows(0)("Against_Bulk_Srn_PI_adjustment"))
            obj.Against_Physical_Stock_No = clsCommon.myCstr(dt.Rows(0)("Against_Physical_Stock_No"))
            obj.Against_Transfer_In_Doc_No = clsCommon.myCstr(dt.Rows(0)("Against_Transfer_In_Doc_No"))
            obj.Against_Tanker_Dispatch_Doc_No = clsCommon.myCstr(dt.Rows(0)("Against_Tanker_Dispatch_Doc_No"))

            obj.FromLocation = clsCommon.myCstr(dt.Rows(0)("FromLocation"))
            obj.ToLocation = clsCommon.myCstr(dt.Rows(0)("ToLocation"))
            obj.isAutoCreatedByMilkTransferIn = clsCommon.myCdbl(dt.Rows(0)("isAutoCreatedByMilkTransferIn"))

            obj.Against_AP_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Against_AP_Invoice_No"))
            obj.Against_PI_No_Difference = clsCommon.myCstr(dt.Rows(0)("Against_PI_No_Difference"))
            obj.Against_PI_No_Difference_Rejected = clsCommon.myCstr(dt.Rows(0)("Against_PI_No_Difference_Rejected"))

            obj.Adjustment_Type = clsCommon.myCstr(dt.Rows(0)("Adjustment_Type"))
            obj.Adjustment_Specification = clsCommon.myCstr(dt.Rows(0)("Adjustment_Specification"))
            obj.Is_JobWork = clsCommon.myCdbl(dt.Rows(0)("Is_JobWork"))

            qry = "SELECT  * from TSPL_ADJUSTMENT_DETAIL_QC where  Adjustment_No='" + obj.Adjustment_No + "' order by Adjustment_Line_No "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsAdjustmentsQCCDetails)
                Dim objTr As ClsAdjustmentsQCCDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsAdjustmentsQCCDetails()
                    objTr.Adjustment_No = clsCommon.myCstr(dr("Adjustment_No"))
                    objTr.Adjustment_Line_No = clsCommon.myCdbl(dr("Adjustment_Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Description = clsCommon.myCstr(dr("Item_Description"))
                    objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))
                    objTr.Adjustment_Type = clsCommon.myCstr(dr("Adjustment_Type"))
                    objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                    objTr.Item_Quantity = clsCommon.myCdbl(dr("Item_Quantity"))
                    objTr.Unit_Cost = clsCommon.myCdbl(dr("Unit_Cost"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Account_Code = clsCommon.myCstr(dr("Account_Code"))
                    objTr.Account_Description = clsCommon.myCstr(dr("Account_Description"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comments = clsCommon.myCstr(dr("Comments"))
                    objTr.mrp = clsCommon.myCdbl(dr("mrp"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCstr(dr("Expiry_Date"))
                    End If
                    objTr.Breakage = clsCommon.myCdbl(dr("Breakage"))
                    objTr.Breakage_Cost = clsCommon.myCdbl(dr("Breakage_Cost"))
                    objTr.ItemType = clsCommon.myCstr(dr("ItemType"))
                    'objTr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objTr.BreakageType = clsCommon.myCstr(dr("BreakageType"))
                    objTr.LeakageQty = clsCommon.myCstr(dr("LeakageQty"))

                    objTr.Itemstatus = clsCommon.myCstr(dr("item_status"))

                    If dr("FAT_Pers") Is DBNull.Value Then
                        objTr.fat_pers = 0
                    Else
                        objTr.fat_pers = clsCommon.myCdbl(dr("FAT_Pers"))
                    End If

                    If dr("FAT_KG") Is DBNull.Value Then
                        objTr.fat_kg = 0
                    Else
                        objTr.fat_kg = clsCommon.myCdbl(dr("FAT_KG"))
                    End If

                    If dr("SNF_Pers") Is DBNull.Value Then
                        objTr.snf_pers = 0
                    Else
                        objTr.snf_pers = clsCommon.myCdbl(dr("SNF_Pers"))
                    End If

                    If dr("SNF_KG") Is DBNull.Value Then
                        objTr.snf_kg = 0
                    Else
                        objTr.snf_kg = clsCommon.myCdbl(dr("SNF_KG"))
                    End If

                    '' adde by Panch raj
                    objTr.fat_Rate = clsCommon.myCdbl(dr("fat_Rate"))
                    objTr.snf_Rate = clsCommon.myCdbl(dr("snf_Rate"))
                    objTr.fat_Amt = clsCommon.myCdbl(dr("fat_Amt"))
                    objTr.snf_Amt = clsCommon.myCdbl(dr("snf_Amt"))
                    objTr.Price_Type = clsCommon.myCstr(dr("Price_Type"))
                    objTr.MCC_Price_Code = clsCommon.myCstr(dr("MCC_Price_Code"))
                    objTr.Bulk_Price_Code = clsCommon.myCstr(dr("Bulk_Price_Code"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    objTr.QC_Status = clsCommon.myCstr(dr("QC_Status"))
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("IC-QC", objTr.Adjustment_No, objTr.Item_Code, objTr.Adjustment_Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("IC-QC", objTr.Adjustment_No, objTr.Item_Code, objTr.Adjustment_Line_No, trans)
                    objTr.arrBatchItemNew = clsBatchInventoryNew.GetData("IC-QC", objTr.Adjustment_No, objTr.Item_Code, objTr.Adjustment_Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal AdjustmentType As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            DeleteData(strCode, AdjustmentType, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal AdjustmentType As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New ClsAdjustmentsQCC()
        obj = obj.GetData(strCode, AdjustmentType, NavigatorType.Current, trans, Nothing)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0) Then
            Try
                If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Empty Transactions", obj.Loc_Code, obj.Adjustment_Date, trans)
                ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Production Entry", obj.Loc_Code, obj.Adjustment_Date, trans)
                Else
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Store Adjustment", obj.Loc_Code, obj.Adjustment_Date, trans)
                End If

                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If

                clsSerializeInvenotry.DeleteData("IC-QC", strCode, trans)
                clsBatchInventory.DeleteData("IC-QC", strCode, trans)
                clsBatchInventoryNew.DeleteData("IC-QC", strCode, trans)
                Dim qry As String = "delete from TSPL_ADJUSTMENT_DETAIL_QC where Adjustment_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '====================== for Delete Table Ticket : TEC/01/08/19-000975 By Prabhakar ==============================
                qry = "update TSPL_ADJUSTMENT_DETAIL_QC_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where Adjustment_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_ADJUSTMENT_HEADER_QC_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where Adjustment_No='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '======================================================================
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function


    Public Shared Sub ReverseAndUnpost(ByVal strCode As String)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ClsAdjustmentsQCC.ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Return ReverseAndUnpost(strCode, trans, False)
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal isSkipBalanceCheck As Boolean) As Boolean
        Dim Qry As String = "select isnull(Posted,'N') AS Posted  from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No='" + strCode + "'"
        If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(Qry, trans), "Y") = CompairStringResult.Equal Then
            Throw New Exception("Transaction status should be posted for reverse and unpost")
        End If

        Qry = "select distinct ProductionStoreEntryNo from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where Against_Production_Entry_QC ='" + strCode + "'"
        Dim dt As DataTable
        dt = New DataTable()
        dt = clsDBFuncationality.GetDataTable(Qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Qry = "Production Entry QC is used in following Production Store Entry "
            For Each dr As DataRow In dt.Rows
                Qry += Environment.NewLine + clsCommon.myCstr(dr("ProductionStoreEntryNo"))
            Next
            Qry += Environment.NewLine + "Can't unpost it"
            Throw New Exception(Qry)
        End If

        'Dim dblBalance As Decimal = 0
        Try
            Dim obj As New ClsAdjustmentsQCC()
            obj = obj.GetData(strCode, "", NavigatorType.Current, trans)
            'Dim SettDoNotStopOnItemBalanceExceptionStoreAdjustment As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotStopOnItemBalanceExceptionStoreAdjustment, clsFixedParameterCode.DoNotStopOnItemBalanceExceptionStoreAdjustment, trans)) > 0)
            'If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal AndAlso Not isSkipBalanceCheck Then
            '    For Each objtr As ClsAdjustmentsQCCDetails In obj.Arr
            '        ''Main Item UDL/01/11/19-001008 by balwinder on 21/11/2019
            '        If obj.IsMilkType = 1 Then
            '            Dim Loc_code As String = ""
            '            Dim Main_Loc_code As String = ""
            '            If clsCommon.myLen(obj.Loc_Code) <= 0 Then
            '                Loc_code = obj.MainLocationCode
            '                Main_Loc_code = ""
            '            Else
            '                Loc_code = obj.Loc_Code
            '                Main_Loc_code = obj.MainLocationCode
            '            End If
            '            dblBalance = clsInventoryMovementNew.getBalance(objtr.Item_Code, Main_Loc_code, Loc_code, obj.Adjustment_No, obj.Adjustment_Date, trans, objtr.Unit_Code)
            '        Else
            '            dblBalance = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(objtr.Item_Code, obj.Loc_Code, obj.Adjustment_No, obj.Adjustment_Date, trans, objtr.Unit_Code)
            '        End If
            '        If dblBalance < objtr.Item_Quantity Then
            '            If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
            '                Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objtr.Item_Quantity) + Environment.NewLine + "Item : " + objtr.Item_Code + Environment.NewLine + "Unit : " + objtr.Unit_Code)
            '            End If
            '        End If
            '        ''End of Main Item

            '        ''Batch Item
            '        If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
            '            For Each objBatch As clsBatchInventory In objtr.arrBatchItem
            '                dblBalance = clsBatchInventory.GetBatchBalance(objBatch.Item_Code, objBatch.Location_Code, objBatch.Batch_No, objBatch.MRP, objBatch.UOM, objBatch.Document_Code, objBatch.Document_Type, trans)
            '                If dblBalance < objBatch.Qty Then
            '                    If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
            '                        Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objBatch.Qty) + Environment.NewLine + "Item : " + objBatch.Item_Code + Environment.NewLine + "Batch : " + objBatch.Batch_No + Environment.NewLine + "MRP : " + clsCommon.myCstr(objBatch.MRP) + Environment.NewLine + "Unit : " + objBatch.UOM)
            '                    End If
            '                End If
            '            Next
            '        End If
            '        If objtr.arrBatchItemNew IsNot Nothing AndAlso objtr.arrBatchItemNew.Count > 0 Then
            '            For Each objBatchNew As clsBatchInventoryNew In objtr.arrBatchItemNew
            '                dblBalance = clsBatchInventoryNew.GetBatchBalance(objBatchNew.Item_Code, objBatchNew.Location_Code, objBatchNew.Batch_No, objBatchNew.UOM, objBatchNew.Document_Code, objBatchNew.Document_Type, trans)
            '                If dblBalance < objBatchNew.Qty Then
            '                    If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
            '                        Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objBatchNew.Qty) + Environment.NewLine + "Item : " + objBatchNew.Item_Code + Environment.NewLine + "Batch : " + objBatchNew.Batch_No + Environment.NewLine + "Unit : " + objBatchNew.UOM)
            '                    End If
            '                End If
            '            Next
            '        End If
            '        ''End of Batch Item
            '    Next
            'End If

            'Qry = "select Against_Physical_Stock_No from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No ='" + strCode + "'  and isnull(Against_Physical_Stock_No,'')<>'' "
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    Qry = "Current document is created Against Physical stock-"
            '    For Each dr As DataRow In dt.Rows
            '        Qry += Environment.NewLine + clsCommon.myCstr(dr("Against_Physical_Stock_No"))
            '    Next
            '    Throw New Exception(Qry)
            'End If

            'Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in('IC-QC', 'GL-JE') and Source_Doc_No='" + strCode + "'", trans)
            'If clsCommon.myLen(VoucherNo) > 0 Then
            '    Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
            '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            '    Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
            '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'End If
            'VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER_OP where Source_Code in('IC-QC', 'GL-JE') and Source_Doc_No='" + strCode + "'", trans)
            'If clsCommon.myLen(VoucherNo) > 0 Then
            '    Qry = "delete from TSPL_JOURNAL_DETAILS_OP where Voucher_No ='" + VoucherNo + "'"
            '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            '    Qry = "delete from TSPL_JOURNAL_MASTER_OP where Voucher_No ='" + VoucherNo + "'"
            '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'End If

            'Qry = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=null where Against_Inv_Movement_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='IC-QC')"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'Qry = " update TSPL_BATCH_ITEM_New set Against_Inv_Movement_New_Trans_Id=null where Against_Inv_Movement_New_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + strCode + "' and Trans_Type='IC-QC') "
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            'Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='IC-QC'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            'Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + strCode + "' and Trans_Type='IC-QC'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)



            Qry = "Update TSPL_ADJUSTMENT_HEADER_QC set Posted = 'N' where adjustment_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception("Error in Production Qc no [" + strCode + "]" + Environment.NewLine + ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function GetMilkRate(ByVal StdFatW As Decimal, ByVal StdSNfW As Decimal, ByVal StdFATRatio As Decimal, ByVal StdSNFRatio As Decimal, ByVal StdRate As Decimal, ByVal fatKG As Decimal, ByVal snfKG As Decimal, ByVal Qty As Decimal)
        Dim FATRate As Double = 0
        Dim SNFRate As Double = 0
        Dim FATValue As Double = 0
        Dim SNfValue As Double = 0
        Dim whrcls As String = String.Empty

        FATRate = MyMath.RoundDown(clsCommon.myCdbl(StdRate) * StdFatW / StdFATRatio, 2)
        SNFRate = MyMath.RoundDown(clsCommon.myCdbl(StdRate) * StdSNfW / StdSNFRatio, 2)
        FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
        SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
        Return ((FATValue + SNfValue) / IIf(Qty <= 0, 1, Qty))
    End Function


    Public Shared Function GetBatchBalanceAdjustmentsQCC(ByVal strItemCode As String, ByVal strLocationCode As String, ByVal strBatchNo As String, ByVal dblMRP As Double, ByVal strUOM As String, ByVal strCurrDocNo As String, ByVal strCurrDocType As String, ByVal trans As SqlTransaction) As Double
        Dim qry As String = "select  Qty from ( select Batch_No as BatchNo,Min(Manufacture_Date) as ManufactureDate,MAX(Expiry_Date) as ExpiryDate,cast( round( sum(Qty * (case when Document_Type='IC-AD' then 1 else case when Document_Type='IC-QC' then -1 else 0 end end )),2,1) as decimal(18,2)) as Qty from ("
        qry += " select * from ("
        qry += " select TSPL_BATCH_ITEM.Document_Type,TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.In_Out_Type,TSPL_BATCH_ITEM.UOM as OrgUOM,TSPL_BATCH_ITEM.Qty as OrgQty,TSPL_BATCH_ITEM.MRP as OrgMRP,TSPL_BATCH_ITEM.Expiry_Date,TSPL_BATCH_ITEM.Manufacture_Date, convert(decimal(18,2),(TSPL_BATCH_ITEM.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ConvertedUOM.Conversion_Factor) as Qty, (TSPL_BATCH_ITEM.MRP /TSPL_ITEM_UOM_DETAIL.Conversion_Factor)*ConvertedUOM.Conversion_Factor as MRP"
        qry += " from TSPL_BATCH_ITEM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_BATCH_ITEM.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM.UOM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUOM on ConvertedUOM.Item_Code=TSPL_BATCH_ITEM.Item_Code and ConvertedUOM.UOM_Code='" + strUOM + "'"
        qry += " where TSPL_BATCH_ITEM.Item_Code='" + strItemCode + "' and TSPL_BATCH_ITEM.Location_Code='" + strLocationCode + "' and  TSPL_BATCH_ITEM.Batch_No='" + strBatchNo + "'"

        qry += " and TSPL_BATCH_ITEM.Document_Type in ('IC-AD','IC-QC')"
        qry += " ) xx where 2=2 "
        If dblMRP <> 0 Then
            qry += "and MRP='" + clsCommon.myCstr(dblMRP) + "'"
        End If
        qry += " )xxx"
        qry += " group by Batch_No )xxxx "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function


End Class

Public Class ClsAdjustmentsQCCDetails
#Region "Variables"
    Public Itemstatus As String = Nothing
    Public Adjustment_No As String = Nothing
    Public Adjustment_Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Description As String = Nothing
    Public Bar_Code As String = Nothing
    Public Adjustment_Type As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Quantity As Double = 0
    Public Unit_Cost As Double = 0
    Public Item_Cost As Double = 0
    Public Unit_Code As String = Nothing
    Public Account_Code As String = Nothing
    Public Account_Description As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public mrp As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Breakage As Double = 0
    Public Breakage_Cost As Double = 0
    Public ItemType As String = Nothing
    Dim Item_Type As String = Nothing
    Public BreakageType As String = Nothing
    Public LeakageQty As Double = 0
    Public Basic_Price As Double = 0
    Public fat_pers As Decimal = Nothing
    Public fat_kg As Decimal = Nothing
    Public snf_kg As Decimal = Nothing
    Public snf_pers As Decimal = Nothing
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public arrBatchItemNew As List(Of clsBatchInventoryNew) = Nothing
    '' add by Panch raj
    Public fat_Rate As Decimal = 0
    Public fat_Amt As Decimal = 0
    Public snf_Rate As Decimal = 0
    Public snf_Amt As Decimal = 0
    Public Price_Type As String = ""
    Public MCC_Price_Code As String = ""
    Public Bulk_Price_Code As String = ""

    Public Bin_No As String = Nothing
    Public QC_Status As String = Nothing
    'Public PS_GL_Account_Inventroy_Ctrl As String = "" ''Not a table column
    'Public PS_GL_Account As String = "" ''Not a table column
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strLocationCode As String, ByVal Arr As List(Of ClsAdjustmentsQCCDetails), ByVal trans As SqlTransaction, ByVal InoutType As String, ByVal DocDate As DateTime) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As ClsAdjustmentsQCCDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocationCode)
                clsCommon.AddColumnsForChange(coll, "Adjustment_Line_No", counter)

                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Description", objtr.Item_Description)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Bar_Code", objtr.Bar_Code, True)
                clsCommon.AddColumnsForChange(coll, "Adjustment_Type", objtr.Adjustment_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Quantity", objtr.Item_Quantity)
                clsCommon.AddColumnsForChange(coll, "Unit_Cost", objtr.Unit_Cost)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", objtr.Item_Cost)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PURCHASE_ACCOUNTS.Adjustment_Account ,TSPL_GL_ACCOUNTS.Description  from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_ITEM_MASTER.Item_Code='" + objtr.Item_Code + "'", trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Plese set the Purchase Account set or its Adjustment Writeoff Account for item " + objtr.Item_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Account_Code", clsCommon.myCstr(dt.Rows(0)("Adjustment_Account")))
                clsCommon.AddColumnsForChange(coll, "Account_Description", clsCommon.myCstr(dt.Rows(0)("Description")))
                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comments", objtr.Comments)
                clsCommon.AddColumnsForChange(coll, "mrp", objtr.mrp)
                If objtr.MFG_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(objtr.MFG_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Batch_No", objtr.Batch_No)
                If objtr.Expiry_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(objtr.Expiry_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Breakage", objtr.Breakage)
                clsCommon.AddColumnsForChange(coll, "Breakage_Cost", objtr.Breakage_Cost)
                clsCommon.AddColumnsForChange(coll, "ItemType", objtr.ItemType)
                clsCommon.AddColumnsForChange(coll, "Item_Type", objtr.Item_Type)
                clsCommon.AddColumnsForChange(coll, "BreakageType", objtr.BreakageType)
                clsCommon.AddColumnsForChange(coll, "LeakageQty", objtr.LeakageQty)
                clsCommon.AddColumnsForChange(coll, "Basic_Price", objtr.Basic_Price)
                clsCommon.AddColumnsForChange(coll, "item_status", objtr.Itemstatus)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.fat_kg)
                clsCommon.AddColumnsForChange(coll, "FAT_Pers", objtr.fat_pers)
                clsCommon.AddColumnsForChange(coll, "SNF_Pers", objtr.snf_pers)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.snf_kg)

                '' Added by Panch Raj
                clsCommon.AddColumnsForChange(coll, "fat_Rate", objtr.fat_Rate)
                clsCommon.AddColumnsForChange(coll, "snf_Rate", objtr.snf_Rate)
                'If ClsAdjustments.GetIsMilkType(strDocNo, trans) = 1 AndAlso (clsCommon.CompairString(objtr.Adjustment_Type, "CI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal) Then
                '    clsCommon.AddColumnsForChange(coll, "fat_Amt", Math.Round(objtr.Item_Cost * 2 / 3, 2))
                '    clsCommon.AddColumnsForChange(coll, "snf_Amt", (objtr.Item_Cost - Math.Round(objtr.Item_Cost * 2 / 3, 2)))
                'Else
                '    clsCommon.AddColumnsForChange(coll, "fat_Amt", objtr.fat_Amt)
                '    clsCommon.AddColumnsForChange(coll, "snf_Amt", objtr.snf_Amt)
                'End If
                clsCommon.AddColumnsForChange(coll, "fat_Amt", objtr.fat_Amt)
                clsCommon.AddColumnsForChange(coll, "snf_Amt", objtr.snf_Amt)

                clsCommon.AddColumnsForChange(coll, "Price_Type", objtr.Price_Type, True)
                clsCommon.AddColumnsForChange(coll, "MCC_Price_Code", objtr.MCC_Price_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bulk_Price_Code", objtr.Bulk_Price_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bin_No", objtr.Bin_No)
                'QC_Status
                clsCommon.AddColumnsForChange(coll, "QC_Status", objtr.QC_Status)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_DETAIL_QC", OMInsertOrUpdate.Insert, "", trans)

                clsSerializeInvenotry.SaveData("IC-QC", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.arrSrItem, trans)
                clsBatchInventory.SaveData("IC-QC", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.mrp, objtr.Unit_Code, objtr.arrBatchItem, trans)
                clsBatchInventoryNew.SaveData("IC-QC", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.mrp, objtr.Unit_Code, objtr.arrBatchItemNew, trans)

                ' SaveTagNo()
                Dim objAd As New ClsAdjustmentsQCCDetails
                objAd.SaveTagNo(objtr.Item_Code, objtr.arrSrItem, trans)
                counter += 1
            Next
        End If
        Return True

    End Function

    Public Function SaveTagNo(ByVal Item_code As String, ByRef arr As List(Of clsSerializeInvenotry), ByVal trans As SqlTransaction)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsSerializeInvenotry In arr
                Dim objTag As New clsassetservicemaster
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_ITEM_MASTER_CATEGORY where Item_code = '" + Item_code + "'", trans)
                If obj.Tag_No = "" Then
                    GoTo a
                End If
                objTag.tagno = obj.Tag_No
                objTag.serialno = obj.Auto_Sr_No
                objTag.asstcode = Item_code

                If dt.Rows.Count > 0 Then
                    objTag.comlevel1 = dt.Rows(0)("Item_Category_Code")
                    objTag.lev1code = dt.Rows(0)(3)
                End If
                If dt.Rows.Count > 1 Then
                    objTag.comlevel2 = dt.Rows(1)("Item_Category_Code")
                    objTag.lev2code = dt.Rows(1)(3)
                End If
                If dt.Rows.Count > 2 Then
                    objTag.comlevel3 = dt.Rows(2)("Item_Category_Code")
                    objTag.lev3code = dt.Rows(2)(3)
                End If
                If dt.Rows.Count > 3 Then
                    objTag.comlevel4 = dt.Rows(3)("Item_Category_Code")
                    objTag.lev4code = dt.Rows(3)(3)
                End If

                objTag.comlevel5 = ""
                Dim qry1 As String = "select count(*) from TSPL_VISI_MASTER where VISI_ID='" + objTag.serialno + "' and asset_no='" + objTag.asstcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                If check1 = 0 Then
                    clsassetservicemaster.SaveData(objTag, True, trans)
                End If
a:          Next
        End If
        Return True
    End Function
End Class

Public Class ClsAdjustmentsStoreEntry
#Region "Variables"
    Public Against_Physical_Stock_No As String = Nothing
    Public chklocation As String = Nothing
    Public ProductionStoreEntryNo As String = Nothing
    'Public Production_Entry As String = Nothing
    Public ProductionStoreEntry_Date As DateTime = Nothing
    Public Posting_Date As DateTime = Nothing
    Public Reference As String = Nothing
    Public Description As String = Nothing
    Public Posted As String = Nothing
    Public Reference_Document As String = Nothing
    Public Document_No As String = Nothing
    Public Unit_Code As String = Nothing
    Public ItemType As String = Nothing
    Public EMP_CODE As String = Nothing
    Public EMP_NAME As String = Nothing
    Public Customer_CODE As String = Nothing
    Public Customer_NAME As String = Nothing
    Public Created_time As String = Nothing
    Public Modified_Time As String = Nothing
    Public Vehicle_Code As String = Nothing
    Public Vehicle_No As String = Nothing
    Public Challan_No As String = Nothing
    Public Challan_date As DateTime? = Nothing
    Public GateEntry_No As String = Nothing
    Public GateEntry_Date As DateTime? = Nothing
    Public Loc_Code As String = Nothing
    Public Loc_Desc As String = Nothing
    Public EntryDateTime As DateTime = Nothing
    Public Trans_Type As String = Nothing
    Public Is_Imported As Integer = 0
    Public Stock_Type As String = ""
    Dim Created_By As String = Nothing
    Dim Created_Date As DateTime = Nothing
    Dim Modify_By As String = Nothing
    Dim Modify_Date As DateTime = Nothing
    Dim Comp_Code As String = Nothing
    Dim GateEnt_No As String = ""
    Public IsMilkType As Integer = 0
    Public MainLocationCode As String = Nothing
    Public MainLocationDesc As String = Nothing
    Public isBySaleInvoice As Boolean = False ''Not a table columns
    Public Against_Item_Stock_Conversion As String = Nothing
    Public Against_Item_Stock_Conv_Doc As String = Nothing
    Public Against_Bulk_Srn_PI_adjustment As String = Nothing
    Public Against_AP_Invoice_No As String = Nothing

    Public Against_PI_No_Difference As String = Nothing
    Public Against_PI_No_Difference_Rejected As String = Nothing
    Public Against_Transfer_In_Doc_No As String = Nothing
    Public Against_Tanker_Dispatch_Doc_No As String = Nothing
    Public FromLocation As String = Nothing
    Public ToLocation As String = Nothing
    Public isAutoCreatedByMilkTransferIn As Integer = 0

    Public Adjustment_Type As String = Nothing
    Public Adjustment_Specification As String = Nothing

    Public Is_JobWork As Integer = 0
    Public Arr As List(Of ClsAdjustmentsStoreEntryDetails) = Nothing
    Public Against_Transfer_In_Return_Doc_No As String = Nothing
    Public Against_Production_Entry As String = Nothing
    Public Against_Production_Entry_QC As String = Nothing

#End Region
    Public Function SaveData(ByVal obj As ClsAdjustmentsStoreEntry, ByVal isNewEntry As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, "", trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As ClsAdjustmentsStoreEntry, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Empty Transactions", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
            ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Production Entry", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
            Else
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Store Adjustment", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
            End If

            clsSerializeInvenotry.DeleteData("IC-SE", obj.ProductionStoreEntryNo, trans)
            clsBatchInventory.DeleteData("IC-SE", obj.ProductionStoreEntryNo, trans)
            clsBatchInventoryNew.DeleteData("IC-SE", obj.ProductionStoreEntryNo, trans)
            Dim qry As String = "delete from TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL where ProductionStoreEntryNo='" + obj.ProductionStoreEntryNo + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '======================= remove details data for Delete table during update record =============================
            qry = "delete from TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL_Delete_Data where ProductionStoreEntryNo='" + obj.ProductionStoreEntryNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            '======================= remove details data for Delete table during update record =============================

            Dim strDocNo As String = ""
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            If clsCommon.myLen(strAdjustmentNoTemp) > 0 Then
                obj.ProductionStoreEntryNo = strAdjustmentNoTemp
                isNewEntry = True
            Else
                If isNewEntry Then
                    Dim strDoc As String = ""
                    Dim strDocTrans As String = ""
                    If clsCommon.CompairString(obj.Adjustment_Type, "PRE") = CompairStringResult.Equal Then
                        strDoc = clsDocType.StoreAdjustmentProductionStoreEntry
                        strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustmentProductionStoreEntry
                    Else
                        strDoc = clsDocType.StoreAdjustment
                        strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustment
                    End If
                    'strDoc = clsDocType.StoreAdjustment
                    'strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustment

                    If clsCommon.myLen(strDoc) <= 0 Then
                        Throw New Exception("Document type not found")
                    End If
                    If clsCommon.myLen(strDocTrans) <= 0 Then
                        Throw New Exception("Document Transaction type not found")
                    End If
                    If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        obj.ProductionStoreEntryNo = clsERPFuncationality.GetNextCode(trans, obj.ProductionStoreEntry_Date, strDoc, strDocTrans, obj.MainLocationCode)
                    Else
                        obj.ProductionStoreEntryNo = clsERPFuncationality.GetNextCode(trans, obj.ProductionStoreEntry_Date, strDoc, strDocTrans, obj.Loc_Code)
                    End If

                End If
            End If


            If (clsCommon.myLen(obj.ProductionStoreEntryNo) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If
            ''UDL/30/07/19-000309 by balwinder on 30/07/2019
            If clsCommon.CompairString("Y", clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Posted from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where ProductionStoreEntryNo='" + obj.ProductionStoreEntryNo + "'", trans))) = CompairStringResult.Equal Then
                Throw New Exception("Already Posted Transaction :" + obj.ProductionStoreEntryNo)
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "AdjustType", "Adj")
            clsCommon.AddColumnsForChange(coll, "ProductionStoreEntry_Date", clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "EntryDateTime", clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Reference_Document", obj.Reference_Document)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            'clsCommon.AddColumnsForChange(coll, "Production_Entry", obj.Production_Entry)
            clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "EMP_NAME", obj.EMP_NAME)
            clsCommon.AddColumnsForChange(coll, "Customer_CODE", obj.Customer_CODE)
            clsCommon.AddColumnsForChange(coll, "Customer_NAME", obj.Customer_NAME)
            clsCommon.AddColumnsForChange(coll, "Against_Physical_Stock_No", obj.Against_Physical_Stock_No)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)

            clsCommon.AddColumnsForChange(coll, "Adjustment_Specification", obj.Adjustment_Specification)
            clsCommon.AddColumnsForChange(coll, "Adjustment_Type", obj.Adjustment_Type)

            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            If obj.Challan_date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "Challan_date", clsCommon.GetPrintDate(obj.Challan_date, "dd/MMM/yyyy"))
            End If


            clsCommon.AddColumnsForChange(coll, "GateEntry_No", obj.GateEntry_No)
            If obj.GateEntry_Date IsNot Nothing Then
                clsCommon.AddColumnsForChange(coll, "GateEntry_Date", clsCommon.GetPrintDate(obj.GateEntry_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "GateEnt_No", obj.GateEnt_No)
            clsCommon.AddColumnsForChange(coll, "Loc_Code", obj.Loc_Code)
            clsCommon.AddColumnsForChange(coll, "Loc_Desc", obj.Loc_Desc)
            clsCommon.AddColumnsForChange(coll, "Trans_Type", obj.Trans_Type)
            clsCommon.AddColumnsForChange(coll, "Is_Imported", obj.Is_Imported)
            clsCommon.AddColumnsForChange(coll, "Stock_Type", obj.Stock_Type)
            clsCommon.AddColumnsForChange(coll, "IsMilkType", obj.IsMilkType)
            clsCommon.AddColumnsForChange(coll, "MainLocationCode", obj.MainLocationCode, True)
            clsCommon.AddColumnsForChange(coll, "MainLocationDesc", obj.MainLocationDesc, True)
            clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
            clsCommon.AddColumnsForChange(coll, "Third_Party_Location", obj.chklocation)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Modified_Time", clsCommon.GetPrintDate(dtCurrent, "hh:mm tt"))

            clsCommon.AddColumnsForChange(coll, "Against_Item_Stock_Conversion", obj.Against_Item_Stock_Conversion, True)
            clsCommon.AddColumnsForChange(coll, "Against_Item_Stock_Conv_Doc", obj.Against_Item_Stock_Conv_Doc, True)
            clsCommon.AddColumnsForChange(coll, "Against_Bulk_Srn_PI_adjustment", obj.Against_Bulk_Srn_PI_adjustment, True)
            clsCommon.AddColumnsForChange(coll, "Against_Tanker_Dispatch_Doc_No", obj.Against_Tanker_Dispatch_Doc_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_Transfer_In_Doc_No", obj.Against_Transfer_In_Doc_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_AP_Invoice_No", obj.Against_AP_Invoice_No, True)

            clsCommon.AddColumnsForChange(coll, "Against_PI_No_Difference", obj.Against_PI_No_Difference, True)
            clsCommon.AddColumnsForChange(coll, "Against_PI_No_Difference_Rejected", obj.Against_PI_No_Difference_Rejected, True)

            clsCommon.AddColumnsForChange(coll, "FromLocation", obj.FromLocation, True)
            clsCommon.AddColumnsForChange(coll, "ToLocation", obj.ToLocation, True)
            clsCommon.AddColumnsForChange(coll, "isAutoCreatedByMilkTransferIn", clsCommon.myCdbl(obj.isAutoCreatedByMilkTransferIn))
            clsCommon.AddColumnsForChange(coll, "Is_JobWork", obj.Is_JobWork)
            clsCommon.AddColumnsForChange(coll, "Against_Transfer_In_Return_Doc_No", obj.Against_Transfer_In_Return_Doc_No, True)
            clsCommon.AddColumnsForChange(coll, "Against_Production_Entry", obj.Against_Production_Entry)
            clsCommon.AddColumnsForChange(coll, "Against_Production_Entry_QC", obj.Against_Production_Entry_QC)
            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "ProductionStoreEntryNo", obj.ProductionStoreEntryNo)
                clsCommon.AddColumnsForChange(coll, "Created_time", clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_STORE_ENTRY_HEAD", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_STORE_ENTRY_HEAD", OMInsertOrUpdate.Update, "ProductionStoreEntryNo='" + obj.ProductionStoreEntryNo + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsAdjustmentsStoreEntryDetails.SaveData(obj.ProductionStoreEntryNo, obj.Loc_Code, Arr, trans, obj.Trans_Type, obj.ProductionStoreEntry_Date)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Shared Function PostData(ByVal adjno As String, ByVal Type As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(adjno, Type, trans)
            ''Throw New Exception("deadlocked Occredddddd")
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function

    Public Shared Function GetTransactionType(ByVal StrAdjustmentNo As String, ByVal trans As SqlTransaction) As String
        'Dim qry As String = "select ItemType  from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + StrAdjustmentNo + "'"
        'Dim strItemTypeTemp As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        'Dim strAdjustmentType As String = ""
        'If clsCommon.CompairString(strItemTypeTemp, "E") = CompairStringResult.Equal Then
        '    strAdjustmentType = frmAdjustmentEmpty.strCostTransaction
        'ElseIf clsCommon.CompairString(strItemTypeTemp, "FM") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemTypeTemp, "FT") = CompairStringResult.Equal Then
        '    strAdjustmentType = frmAdjustmentProduction.strCostTransaction
        'ElseIf clsCommon.CompairString(strItemTypeTemp, "RM") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemTypeTemp, "OT") = CompairStringResult.Equal Then
        '    strAdjustmentType = frmAdjustmentStore.strCostTransaction
        'Else
        '    Throw New Exception("Not a Valid Item Type")
        'End If
        'Return strAdjustmentType
        Return AdjustmentEnum.strCostTransaction
    End Function

    Shared Function PostData(ByVal StrAdjustmentNo As String, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal MakeGLEntry As Boolean = True, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
        Dim strAdjustmentType As String = GetTransactionType(StrAdjustmentNo, trans)
        Dim isCreateBulkProcPriceChartItemWise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, trans))
        Dim qry As String = ""
        Dim obj As New ClsAdjustmentsStoreEntry()
        obj = obj.GetData(StrAdjustmentNo, strAdjustmentType, NavigatorType.Current, trans)
        If obj Is Nothing Then
            Throw New Exception("No Data Found to Post")
        End If

        If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Empty Transactions", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
        ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Production Entry", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
        Else
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Store Adjustment", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
        End If

        If clsCommon.CompairString("Y", obj.Posted) = CompairStringResult.Equal Then
            Throw New Exception("Already Posted Transaction :" + StrAdjustmentNo)
        End If

        Try
            'Dim conversion As Decimal
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            'If clsCommon.CompairString(obj.Adjustment_Type, "PRE") = CompairStringResult.Equal Then
            'Else


            For Each objtr As ClsAdjustmentsStoreEntryDetails In obj.Arr
                Dim strItemType As String = clsItemMaster.GetItemType(objtr.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "A"
                ElseIf clsCommon.CompairString(strItemType, "S") = CompairStringResult.Equal Then
                    strItemTypeToSave = "S"
                ElseIf clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "O"
                Else
                    strItemTypeToSave = strItemType
                End If

                Dim objLocationDetails As New clsItemLocationDetails()
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Unit_Code, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + objtr.Item_Code + " and Uom:'" + objtr.Unit_Code)
                End If
                Dim RI As Integer = 0
                If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                    RI = 1
                ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                    RI = -1
                Else
                    Throw New Exception("Transaction Type is not correct")
                End If
                objLocationDetails.Item_Code = objtr.Item_Code
                objLocationDetails.Item_Desc = objtr.Item_Description
                objLocationDetails.Location_Code = obj.Loc_Code
                objLocationDetails.Location_Desc = obj.Loc_Desc
                objLocationDetails.Item_Qty = RI * (objtr.Item_Quantity / ConvFac)
                objLocationDetails.Amount = 0
                Dim dblMRP As Double = objtr.mrp * ConvFac
                If clsCommon.CompairString(objtr.Unit_Code, "EB") = CompairStringResult.Equal Then
                    dblMRP += 100
                End If
                objLocationDetails.MRP = dblMRP
                If objtr.MFG_Date.HasValue Then
                    objLocationDetails.MFG_Date = objtr.MFG_Date
                End If
                objLocationDetails.Batch_No = objtr.Batch_No
                If objtr.Expiry_Date.HasValue Then
                    objLocationDetails.Expiry_Date = objtr.Expiry_Date
                End If
                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)

                '=====================get product type of item============================
                Dim productype As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Product_Type from TSPL_ITEM_MASTER where item_code='" + objtr.Item_Code + "'", trans))

                If productype Is Nothing OrElse productype Is DBNull.Value Then
                    productype = ""
                End If
                Dim strIndustryType = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.INDUSTRYTYPE, clsFixedParameterCode.INDUSTRYTYPE, trans))

                '===========if product type is not nilk only than go into inventory movement tableotherwise in new table======================
                If clsCommon.CompairString(productype, "MI") <> CompairStringResult.Equal Then
                    Dim objInventoryMovemnt As New clsInventoryMovement()
                    If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                        objInventoryMovemnt.InOut = "I"
                    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                        objInventoryMovemnt.InOut = "O"
                    Else
                        Throw New Exception("Transaction Type is not correct")
                    End If
                    objInventoryMovemnt.Location_Code = obj.Loc_Code
                    objInventoryMovemnt.Item_Code = objtr.Item_Code
                    objInventoryMovemnt.Item_Desc = objtr.Item_Description
                    objInventoryMovemnt.Qty = objtr.Item_Quantity
                    objInventoryMovemnt.UOM = objtr.Unit_Code
                    objInventoryMovemnt.Basic_Cost = objtr.Item_Cost / IIf(objtr.Item_Quantity = 0, 1, objtr.Item_Quantity)
                    objInventoryMovemnt.MRP = objtr.mrp
                    objInventoryMovemnt.Add_Cost = objtr.Item_Cost
                    objInventoryMovemnt.Net_Cost = objtr.Item_Cost
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "A"
                    End If
                    objInventoryMovemnt.ItemType = strItemTypeToSave

                    If clsCommon.CompairString(strIndustryType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.Batch_No = objtr.Bin_No
                    Else
                        objInventoryMovemnt.Batch_No = objtr.Batch_No
                    End If

                    objInventoryMovemnt.MFG_Date = objtr.MFG_Date
                    objInventoryMovemnt.Expiry_Date = objtr.Expiry_Date
                    objInventoryMovemnt.itemstatus = objtr.Itemstatus

                    objInventoryMovemnt.FAT_KG = objtr.fat_kg
                    objInventoryMovemnt.FAT_Per = objtr.fat_pers
                    objInventoryMovemnt.SNF_KG = objtr.snf_kg
                    objInventoryMovemnt.SNF_Per = objtr.snf_pers
                    objInventoryMovemnt.Fat_Rate = objtr.fat_Rate
                    objInventoryMovemnt.SNF_Rate = objtr.snf_Rate
                    objInventoryMovemnt.Fat_Amt = objtr.fat_Amt
                    objInventoryMovemnt.SNF_Amt = objtr.snf_Amt
                    'objInventoryMovemnt.Cust_Code
                    'objInventoryMovemnt.Cust_Name
                    'objInventoryMovemnt.Vendor_Code
                    'objInventoryMovemnt.Vendor_Name
                    'objInventoryMovemnt.Other_Location_Code
                    'objInventoryMovemnt.Other_Location_Desc

                    ArrInventoryMovement.Add(objInventoryMovemnt)
                ElseIf clsCommon.CompairString(productype, "MI") = CompairStringResult.Equal Then
                    Dim objInventoryMovemntnew As New clsInventoryMovementNew()
                    If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                        objInventoryMovemntnew.InOut = "I"
                    ElseIf clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                        objInventoryMovemntnew.InOut = "O"
                    Else
                        Throw New Exception("Transaction Type is not correct")
                    End If

                    If clsCommon.myLen(obj.Against_Tanker_Dispatch_Doc_No) = 0 Then
                        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                            objInventoryMovemntnew.Location_Code = obj.MainLocationCode
                        Else
                            objInventoryMovemntnew.Location_Code = obj.Loc_Code
                        End If
                        objInventoryMovemntnew.main_location = obj.MainLocationCode
                    Else
                        ' done by priti BHA/24/07/18-000191 to update location for adjustment against milk transfer In.
                        ''order by main_location added by balwinder on 06/12/2018 because location come in first Row.
                        qry = "select Location_Code,main_location from tspl_inventory_movement_new where source_doc_no='" & obj.Against_Tanker_Dispatch_Doc_No & "' and Item_Code='" & objtr.Item_Code & "' order by main_location"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            objInventoryMovemntnew.Location_Code = clsCommon.myCstr(dt.Rows(0).Item("Location_Code"))
                            objInventoryMovemntnew.main_location = clsCommon.myCstr(dt.Rows(0).Item("main_location"))
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_ADJUSTMENT_STORE_ENTRY_HEAD set Loc_Code='" & objInventoryMovemntnew.Location_Code & "',MainLocationCode='" & objInventoryMovemntnew.main_location & "',loc_desc='" & clsLocation.GetName(objInventoryMovemntnew.Location_Code, trans) & "' where ProductionStoreEntryNo='" & obj.ProductionStoreEntryNo & "'", trans)
                        End If
                    End If

                    objInventoryMovemntnew.Item_Code = objtr.Item_Code
                    objInventoryMovemntnew.Item_Desc = objtr.Item_Description
                    objInventoryMovemntnew.Qty = objtr.Item_Quantity
                    objInventoryMovemntnew.UOM = objtr.Unit_Code
                    objInventoryMovemntnew.Basic_Cost = objtr.Item_Cost / IIf(objtr.Item_Quantity = 0, 1, objtr.Item_Quantity)
                    objInventoryMovemntnew.MRP = objtr.mrp
                    objInventoryMovemntnew.Add_Cost = objtr.Item_Cost
                    objInventoryMovemntnew.Net_Cost = objtr.Item_Cost
                    ''richa agarwal 10/10/2014

                    ''================
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemntnew.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemntnew.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemntnew.ItemType = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        objInventoryMovemntnew.ItemType = "A"
                    End If
                    objInventoryMovemntnew.ItemType = strItemTypeToSave

                    If clsCommon.CompairString(strIndustryType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemntnew.Batch_No = objtr.Bin_No
                    Else
                        objInventoryMovemntnew.Batch_No = objtr.Batch_No
                    End If
                    objInventoryMovemntnew.MFG_Date = objtr.MFG_Date
                    objInventoryMovemntnew.Expiry_Date = objtr.Expiry_Date
                    objInventoryMovemntnew.itemstatus = objtr.Itemstatus
                    objInventoryMovemntnew.FAT_KG = objtr.fat_kg
                    objInventoryMovemntnew.FAT_Per = objtr.fat_pers
                    objInventoryMovemntnew.SNF_KG = objtr.snf_kg
                    objInventoryMovemntnew.SNF_Per = objtr.snf_pers

                    '' added by Panch Raj
                    objInventoryMovemntnew.Fat_Rate = objtr.fat_Rate
                    objInventoryMovemntnew.SNF_Rate = objtr.snf_Rate
                    objInventoryMovemntnew.Fat_Amt = objtr.fat_Amt
                    objInventoryMovemntnew.SNF_Amt = objtr.snf_Amt

                    ArrInventoryMovementNew.Add(objInventoryMovemntnew)
                End If

                If (clsCommon.CompairString(objtr.Adjustment_Type, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal) Then
                    If (clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal) Then
                        If clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Then
                            Dim total As Decimal
                            Dim CustType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Type_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Customer_CODE + "'", trans))
                            If clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal Then
                                total = objtr.Item_Cost
                            Else
                                total = objtr.Item_Cost + objtr.Breakage_Cost
                            End If
                        End If
                    End If
                End If
            Next

            clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)

            If ArrInventoryMovement IsNot Nothing AndAlso ArrInventoryMovement.Count > 0 Then
                clsInventoryMovement.SaveData("IC-SE", obj.ProductionStoreEntryNo, obj.ProductionStoreEntry_Date, clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            If ArrInventoryMovementNew IsNot Nothing AndAlso ArrInventoryMovementNew.Count > 0 Then
                clsInventoryMovementNew.SaveData("IC-SE", obj.ProductionStoreEntryNo, obj.ProductionStoreEntry_Date, clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If
            '--------------------------------------------------------------------------------------------------------------------------------------
            ''--- GL End
            '' Anubhooti 05-Dec-2014 (GL Entry should not make in case of difference entry (SRN-PI) from PI)
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                If MakeGLEntry = True Then
                    If obj.isAutoCreatedByMilkTransferIn = 1 Then
                        CreateJETransferWithBranchAc(obj, strType, trans, strVourcherNoForRecreateOnly)
                    Else
                        CreateJE(obj, strType, trans, strVourcherNoForRecreateOnly)
                    End If
                End If
            End If
            'End If
            qry = " update TSPL_ADJUSTMENT_STORE_ENTRY_HEAD  set Posted='Y', Posted_By = '" + objCommonVar.CurrentUserCode + "' ,Posting_Date='" + clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "dd/MMM/yyyy hh:mm tt") + "',modified_time='" + clsCommon.GetPrintDate(obj.ProductionStoreEntry_Date, "hh:mm tt") + "' where ProductionStoreEntryNo='" + obj.ProductionStoreEntryNo + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function

    ''ERO/30/04/19-000577 by balwinder on 02/05/2019 
    'Public Shared Function CreateMilkTransferAdjustmentDoc(objDisp As clsMccDispatch, obj As clsMilkTransferIn, trans As SqlTransaction) As Boolean
    '    Try
    '        Dim rcptQty As Double = 0
    '        Dim FatQcPer As Double = 0
    '        Dim SNFQcPer As Double = 0
    '        Dim FatValue As Double = 0
    '        Dim SnfValue As Double = 0
    '        Dim rcptAmount As Double = 0
    '        Dim DispAmount As Double = 0
    '        Dim DispNetQty As Double = 0
    '        Dim DispFatKg As Double = 0
    '        Dim DispSNFKg As Double = 0
    '        Dim diffamt As Double = 0
    '        Dim diffQty As Double = 0
    '        Dim dblfatkgmilkin As Double = 0
    '        Dim dblSNFkgmilkin As Double = 0
    '        Dim intDiffItem As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(distinct Item_Code) from TSPL_WEIGHMENT_CHEMBER_DETAILS where Weighment_No='" & obj.Weighment_No & "'", trans))

    '        Dim dblfatkg As Double = 0
    '        Dim dblSNFkg As Double = 0
    '        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
    '        Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
    '        If TankerFromMaster = 0 AndAlso MCCChamberwise = 0 Then
    '            rcptQty = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Net_Weight from tspl_weighment_detail where Weighment_No='" & obj.Weighment_No & "'", trans))
    '            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
    '            FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
    '            SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))
    '            FatValue = (objW.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
    '            SnfValue = (objW.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
    '            rcptAmount = Math.Round((FatValue + SnfValue), 2)
    '            diffamt = objDisp.Amount - rcptAmount
    '            diffQty = objDisp.Net_Qty - objW.Net_Weight
    '            dblfatkgmilkin = Math.Round(((objW.Net_Weight * FatQcPer) / 100), 3)
    '            dblSNFkgmilkin = Math.Round(((objW.Net_Weight * SNFQcPer) / 100), 3)
    '            dblfatkg = objDisp.FAT_KG - dblfatkgmilkin
    '            dblSNFkg = objDisp.SNF_KG - dblSNFkgmilkin
    '        Else
    '            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
    '            If intDiffItem = 1 Then
    '                For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
    '                    rcptQty = objTr.Net_Weight
    '                    FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Line_No='" & objTr.Line_No & "' and Param_Type='FAT' ", trans))
    '                    SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "'  and Line_No='" & objTr.Line_No & "' and Param_Type='SNF' ", trans))
    '                    Dim qry = "Select * from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No='" & objDisp.Chalan_NO & "' and Chamber_Description='" & objTr.Chamber_Desc & "'"
    '                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                        DispAmount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
    '                        DispNetQty = clsCommon.myCdbl(dt.Rows(0)("Qty_KG"))
    '                        DispFatKg = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
    '                        DispSNFKg = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
    '                    End If
    '                    FatValue = (objTr.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
    '                    SnfValue = (objTr.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
    '                    rcptAmount = Math.Round((FatValue + SnfValue), 2)
    '                    diffamt += DispAmount - rcptAmount
    '                    diffQty += DispNetQty - objTr.Net_Weight
    '                    dblfatkgmilkin = Math.Round(((objTr.Net_Weight * FatQcPer) / 100), 3)
    '                    dblSNFkgmilkin = Math.Round(((objTr.Net_Weight * SNFQcPer) / 100), 3)
    '                    dblfatkg += DispFatKg - dblfatkgmilkin
    '                    dblSNFkg += DispSNFKg - dblSNFkgmilkin
    '                Next
    '            End If
    '        End If

    '        Dim objAdjOut As New ClsAdjustmentsQCC
    '        objAdjOut.Adjustment_Date = obj.Receipt_Challan_Date
    '        objAdjOut.Posting_Date = obj.Receipt_Challan_Date
    '        objAdjOut.EntryDateTime = obj.Receipt_Challan_Date
    '        objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
    '        objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
    '        objAdjOut.Loc_Code = objDisp.MCC_Code
    '        objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
    '        objAdjOut.Trans_Type = "Out"
    '        objAdjOut.IsMilkType = 1
    '        objAdjOut.MainLocationCode = objDisp.MCC_Code
    '        objAdjOut.FromLocation = objDisp.MCC_Code
    '        objAdjOut.ToLocation = obj.location_code
    '        objAdjOut.isAutoCreatedByMilkTransferIn = 1
    '        objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
    '        objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
    '        objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
    '        objAdjOut.Arr = New List(Of ClsAdjustmentsQCCDetails)

    '        Dim objAdjIn As New ClsAdjustmentsQCC
    '        objAdjIn.Adjustment_Date = obj.Receipt_Challan_Date
    '        objAdjIn.Posting_Date = obj.Receipt_Challan_Date
    '        objAdjIn.EntryDateTime = obj.Receipt_Challan_Date
    '        objAdjIn.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
    '        objAdjIn.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
    '        objAdjIn.Loc_Code = objDisp.MCC_Code
    '        objAdjIn.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
    '        objAdjIn.Trans_Type = "In"
    '        objAdjIn.IsMilkType = 1
    '        objAdjIn.MainLocationCode = objDisp.MCC_Code
    '        objAdjIn.FromLocation = objDisp.MCC_Code
    '        objAdjIn.ToLocation = obj.location_code
    '        objAdjIn.isAutoCreatedByMilkTransferIn = 1
    '        objAdjIn.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
    '        objAdjIn.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
    '        objAdjIn.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
    '        objAdjIn.Arr = New List(Of ClsAdjustmentsQCCDetails)

    '        If intDiffItem > 1 Then
    '            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
    '            For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
    '                rcptQty = objTr.Net_Weight
    '                FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Line_No='" & objTr.Line_No & "' and Param_Type='FAT' ", trans))
    '                SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "'  and Line_No='" & objTr.Line_No & "' and Param_Type='SNF' ", trans))
    '                Dim qry = "Select * from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No='" & objDisp.Chalan_NO & "' and Chamber_No='" & objTr.Line_No & "'"
    '                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
    '                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '                    DispAmount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
    '                    DispNetQty = clsCommon.myCdbl(dt.Rows(0)("Qty_KG"))
    '                    DispFatKg = clsCommon.myCdbl(dt.Rows(0)("FAT_KG"))
    '                    DispSNFKg = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
    '                End If

    '                FatValue = (objTr.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
    '                SnfValue = (objTr.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
    '                rcptAmount = Math.Round((FatValue + SnfValue), 2)
    '                diffamt = DispAmount - rcptAmount
    '                diffQty = DispNetQty - objTr.Net_Weight

    '                dblfatkgmilkin = Math.Round(((objTr.Net_Weight * FatQcPer) / 100), 3)
    '                dblSNFkgmilkin = Math.Round(((objTr.Net_Weight * SNFQcPer) / 100), 3)
    '                dblfatkg = (DispFatKg - dblfatkgmilkin)
    '                dblSNFkg = (DispSNFKg - dblSNFkgmilkin)

    '                If diffQty > 0 OrElse diffamt > 0 OrElse dblfatkg > 0 OrElse dblSNFkg > 0 Then
    '                    Dim objAdjTR As New ClsAdjustmentsQCCDetails()
    '                    objAdjTR.Item_Code = objTr.Item_Code
    '                    objAdjTR.Item_Description = clsItemMaster.GetItemName(objTr.Item_Code, trans)
    '                    objAdjTR.Adjustment_Type = "BI"
    '                    If diffQty > 0 Then
    '                        objAdjTR.Item_Quantity = diffQty
    '                    End If
    '                    If diffamt > 0 Then
    '                        objAdjTR.Item_Cost = diffamt
    '                    End If
    '                    objAdjTR.mrp = 0
    '                    objAdjTR.Unit_Code = objTr.UOM
    '                    If dblfatkg > 0 Then
    '                        objAdjTR.fat_kg = dblfatkg
    '                        objAdjTR.fat_Rate = objDisp.FAT_RATE
    '                        objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
    '                        If objAdjTR.Item_Quantity > 0 Then
    '                            objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                        End If
    '                    End If
    '                    If dblSNFkg > 0 Then
    '                        objAdjTR.snf_kg = dblSNFkg
    '                        objAdjTR.snf_Rate = objDisp.SNF_RATE
    '                        objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
    '                        If objAdjTR.Item_Quantity > 0 Then
    '                            objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                        End If
    '                    End If
    '                    objAdjIn.Arr.Add(objAdjTR)
    '                End If
    '                If diffQty < 0 OrElse diffamt < 0 OrElse dblfatkg < 0 OrElse dblSNFkg < 0 Then
    '                    Dim objAdjTR As New ClsAdjustmentsQCCDetails()
    '                    objAdjTR.Item_Code = objTr.Item_Code
    '                    objAdjTR.Item_Description = clsItemMaster.GetItemName(objTr.Item_Code, trans)
    '                    objAdjTR.Adjustment_Type = "BI"
    '                    If diffQty < 0 Then
    '                        objAdjTR.Item_Quantity = -1 * diffQty
    '                    End If
    '                    If diffamt < 0 Then
    '                        objAdjTR.Item_Cost = -1 * diffamt
    '                    End If
    '                    objAdjTR.mrp = 0
    '                    objAdjTR.Unit_Code = objTr.UOM
    '                    If dblfatkg < 0 Then
    '                        objAdjTR.fat_kg = -1 * dblfatkg
    '                        objAdjTR.fat_Rate = objDisp.FAT_RATE
    '                        objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
    '                        If objAdjTR.Item_Quantity > 0 Then
    '                            objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                        End If
    '                    End If
    '                    If dblSNFkg < 0 Then
    '                        objAdjTR.snf_kg = -1 * dblSNFkg
    '                        objAdjTR.snf_Rate = objDisp.SNF_RATE
    '                        objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
    '                        If objAdjTR.Item_Quantity > 0 Then
    '                            objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                        End If
    '                    End If
    '                    objAdjOut.Arr.Add(objAdjTR)
    '                End If
    '            Next
    '        Else
    '            Dim tnkrNo As String = objDisp.Tanker_No
    '            Dim Loc As String = objDisp.MCC_Code
    '            Dim Silo As String = ""
    '            Dim PostingDate As Date = obj.Receipt_Challan_Date
    '            If diffQty > 0 OrElse diffamt > 0 OrElse dblfatkg > 0 OrElse dblSNFkg > 0 Then
    '                Dim objAdjTR As New ClsAdjustmentsQCCDetails()
    '                objAdjTR.Item_Code = objDisp.Item_Code
    '                objAdjTR.Item_Description = objDisp.Item_Desc
    '                objAdjTR.Adjustment_Type = "BI"
    '                If diffQty > 0 Then
    '                    objAdjTR.Item_Quantity = diffQty
    '                End If
    '                If diffamt > 0 Then
    '                    objAdjTR.Item_Cost = diffamt
    '                End If
    '                objAdjTR.mrp = 0
    '                objAdjTR.Unit_Code = objDisp.UOM_Code
    '                If dblfatkg > 0 Then
    '                    objAdjTR.fat_kg = dblfatkg
    '                    objAdjTR.fat_Rate = objDisp.FAT_RATE
    '                    objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
    '                    If objAdjTR.Item_Quantity > 0 Then
    '                        objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                    End If
    '                End If
    '                If dblSNFkg > 0 Then
    '                    objAdjTR.snf_kg = dblSNFkg
    '                    objAdjTR.snf_Rate = objDisp.SNF_RATE
    '                    objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
    '                    If objAdjTR.Item_Quantity > 0 Then
    '                        objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                    End If
    '                End If
    '                objAdjIn.Arr.Add(objAdjTR)
    '            End If
    '            If diffQty < 0 OrElse diffamt < 0 OrElse dblfatkg < 0 OrElse dblSNFkg < 0 Then
    '                Dim objAdjTR As New ClsAdjustmentsQCCDetails()
    '                objAdjTR.Item_Code = objDisp.Item_Code
    '                objAdjTR.Item_Description = objDisp.Item_Desc
    '                objAdjTR.Adjustment_Type = "BI"
    '                If diffQty < 0 Then
    '                    objAdjTR.Item_Quantity = -1 * diffQty
    '                End If
    '                If diffamt < 0 Then
    '                    objAdjTR.Item_Cost = -1 * diffamt
    '                End If
    '                objAdjTR.mrp = 0
    '                objAdjTR.Unit_Code = objDisp.UOM_Code
    '                If dblfatkg < 0 Then
    '                    objAdjTR.fat_kg = -1 * dblfatkg
    '                    objAdjTR.fat_Rate = objDisp.FAT_RATE
    '                    objAdjTR.fat_Amt = objDisp.FAT_RATE * objAdjTR.fat_kg
    '                    If objAdjTR.Item_Quantity > 0 Then
    '                        objAdjTR.fat_pers = Math.Round(objAdjTR.fat_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                    End If
    '                End If
    '                If dblSNFkg < 0 Then
    '                    objAdjTR.snf_kg = -1 * dblSNFkg
    '                    objAdjTR.snf_Rate = objDisp.SNF_RATE
    '                    objAdjTR.snf_Amt = objDisp.SNF_RATE * objAdjTR.snf_kg
    '                    If objAdjTR.Item_Quantity > 0 Then
    '                        objAdjTR.snf_pers = Math.Round(objAdjTR.snf_kg * 100 / objAdjTR.Item_Quantity, 2, MidpointRounding.ToEven)
    '                    End If
    '                End If
    '                objAdjOut.Arr.Add(objAdjTR)
    '            End If
    '        End If

    '        If objAdjIn.Arr.Count > 0 Then
    '            objAdjIn.SaveData(objAdjIn, True, "", trans)
    '            ClsAdjustmentsQCC.PostData(objAdjIn.Adjustment_No, objAdjIn.Trans_Type, trans, True)
    '        End If
    '        If objAdjOut.Arr.Count > 0 Then
    '            objAdjOut.SaveData(objAdjOut, True, "", trans)
    '            ClsAdjustmentsQCC.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function CreateJE(ByVal obj As ClsAdjustmentsStoreEntry, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
        Dim isCreateGLTransaction As Boolean = IIf(clsCommon.CompairString(clsCommon.myCstr(obj.GateEnt_No), "I") = CompairStringResult.Equal, False, True)
        Dim strAdjAcc As String = String.Empty
        Dim strEmpty As String = String.Empty
        Dim qry As String = String.Empty
        Dim strsegment As String = ""
        Dim strInvAcc As String
        Dim ArryLstFinal As ArrayList = New ArrayList()
        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
            strsegment = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.MainLocationCode + "'", trans)
        Else
            strsegment = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.Loc_Code + "'", trans)
        End If

        Dim strSourceCode As String = "IC-SE"
        Dim strSourceCodeName As String = "I/C Store Entry"
        Dim desc As String = String.Empty
        If clsCommon.CompairString(strType, "Adjustment Entry") = CompairStringResult.Equal Then
            desc = "Adjustment Against " + obj.ProductionStoreEntryNo
        ElseIf clsCommon.CompairString(strType, "Empty Transactions") = CompairStringResult.Equal Then
            desc = "Empty transaction Against " + obj.ProductionStoreEntryNo
        ElseIf clsCommon.CompairString(strType, "Production Entry") = CompairStringResult.Equal Then
            desc = "Production Entry Against " + obj.ProductionStoreEntryNo
        ElseIf clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            desc = "Store Adjustment Against " + obj.ProductionStoreEntryNo
        End If

        Dim SettCreateOpeningEntryAutomatically As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateOpeningEntryAutomatically, clsFixedParameterCode.CreateOpeningEntryAutomatically, trans)) > 0)
        If SettCreateOpeningEntryAutomatically Then ''TEC/02/11/18-000364 by balwinder on 21/11/2018
            SettCreateOpeningEntryAutomatically = False
            'Dim strERPStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.ERPStartDate, clsFixedParameterCode.ERPStartDate, trans)
            If clsCommon.myLen(objCommonVar.ERPStartDate) > 0 Then
                Dim dtERPStartDate As DateTime = clsCommon.GetDateWithEndTime(objCommonVar.ERPStartDate).AddDays(-1)
                If obj.ProductionStoreEntry_Date <= dtERPStartDate Then
                    SettCreateOpeningEntryAutomatically = True
                    strSourceCode = "GL-JE"
                    strSourceCodeName = "GENERAL ENTRY"
                    desc += "[Item Opening]"
                End If
            Else
                Throw New Exception("Please set ERP Start Date")
            End If
        End If




        If clsCommon.myLen(obj.Customer_CODE) > 0 Then
            strAdjAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit FROM TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON  TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account where Cust_Code='" + obj.Customer_CODE + "'", trans))
            If clsCommon.myLen(strAdjAcc) <= 0 Then
                Throw New Exception("Please set Container Deposit Account of customer " + obj.Customer_CODE)
            End If
            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
        End If
        For Each objtr As ClsAdjustmentsStoreEntryDetails In obj.Arr
            Dim ArryLst As ArrayList = New ArrayList()
            Dim dtPurchaseAccountSet As DataTable = clsDBFuncationality.GetDataTable("select   Inv_Control_Account  AS Inv_Control_Account , Adjustment_Account as Adjustment_Account,Item_Opening_Clearing,Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Physical_Inv_Adjustment from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')", trans)
            If dtPurchaseAccountSet Is Nothing AndAlso dtPurchaseAccountSet.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + objtr.Item_Code)
            End If
            Dim strItemProductType As String = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)
            If clsCommon.myLen(obj.EMP_CODE) > 0 AndAlso clsCommon.myLen(obj.Customer_CODE) <= 0 AndAlso clsCommon.myLen(obj.Reference_Document) <= 0 Then
                qry = "select GL_Account from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + obj.EMP_CODE + "'"
                Dim strEmpGLAC As String = clsDBFuncationality.getSingleValue(qry, trans)
                If clsCommon.myLen(strEmpGLAC) <= 0 Then
                    Throw New Exception("Plese map Employee Gl Account for employee " + obj.EMP_CODE)
                End If
                For ii As Integer = 0 To dtPurchaseAccountSet.Rows.Count - 1
                    dtPurchaseAccountSet.Rows(ii)("Adjustment_Account") = strEmpGLAC
                Next
            End If
            ''--- GL Begins Now
            If clsCommon.CompairString(objtr.Adjustment_Type, "QI") = CompairStringResult.Equal Then
                'No need to make Journal Entry
            ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "QD") = CompairStringResult.Equal Then
                'No need to make Journal Entry
            ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "CI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal Then
                If clsCommon.myLen(obj.Customer_CODE) <= 0 Then
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                        ArryLst.Add(Acc1)
                    Else
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                        ArryLst.Add(Acc1)
                        ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                        If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                            clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, "", strInvAcc, "", trans)
                        Else
                            clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "", trans)
                        End If
                        '------------------
                    End If


                    If clsCommon.myLen(obj.Against_AP_Invoice_No) > 0 Then
                        Dim objVIH As clsVedorInvoiceHead = clsVedorInvoiceHead.GetData(obj.Against_AP_Invoice_No, "", trans)
                        For Each objVID As clsVedorInvoiceDetail In objVIH.Arr
                            Dim intCount As Integer = obj.Arr.Count
                            Dim dblLedgeerNonRecoverableAmt As Double = objVIH.GetTaxAmtNonShared(objVID, trans)
                            Dim dblAddionalCost As Double = Math.Round((objVIH.Total_Add_Charge / intCount), 6)
                            Dim tempAmt As Double = objVID.Amount_less_Discount + dblAddionalCost + dblLedgeerNonRecoverableAmt
                            ''richa agarwal 21 /12/2016
                            objVID.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVID.GL_Account_Code, strsegment, True, trans)
                            ''----------
                            Dim AccInvDR1() As String = {objVID.GL_Account_Code, -1 * tempAmt}
                            ArryLst.Add(AccInvDR1)
                        Next
                    ElseIf SettCreateOpeningEntryAutomatically Then
                        strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Item_Opening_Clearing"))
                        If clsCommon.myLen(strAdjAcc) <= 0 Then
                            Throw New Exception("Please set Item Opening Clearing Account of purchase account set [" + clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Purchase_Class_Code")) + "]")
                        End If
                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    Else
                        strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Adjustment_Account"))
                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    End If
                Else
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                        ArryLst.Add(Acc1)
                    Else
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                        ArryLst.Add(Acc1)
                        ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                        If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                            clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, "", strInvAcc, "", trans)
                        Else
                            clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "", trans)
                        End If
                        '------------------
                    End If


                    Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                    ArryLst.Add(Acc2)
                End If
            ElseIf (clsCommon.CompairString(objtr.Adjustment_Type, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal) Then
                If (clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Then
                        strInvAcc = clsDBFuncationality.getSingleValue("select Inv_Control_Account AS Inv_Control_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code = '" + objtr.Item_Code + "')", trans)
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, obj.Loc_Code, trans)
                        Dim strbreakage As String = clsDBFuncationality.getSingleValue("select breakage_gl_account AS Inv_Control_Account ,Adjustment_Account as Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code= '" + objtr.Item_Code + "')", trans)
                        If objtr.Breakage_Cost > 0 Then
                            strbreakage = clsERPFuncationality.ChangeGLAccountLocationSegment(strbreakage, obj.Loc_Code, trans)
                        End If

                        Dim total As Decimal
                        Dim CustType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Type_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Customer_CODE + "'", trans))
                        If clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal Then
                            total = objtr.Item_Cost
                        Else
                            total = objtr.Item_Cost + objtr.Breakage_Cost
                        End If
                        If objtr.Breakage_Cost = 0 Then
                            Dim inventoryaccount() As String
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                inventoryaccount = {strInvAcc, objtr.Item_Cost}
                            Else
                                inventoryaccount = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            End If
                            Dim receivableaccount() As String = {strAdjAcc, total * -1}
                            ArryLst.Add(inventoryaccount)
                            ArryLst.Add(receivableaccount)
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                                ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                                If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                    clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, "", strInvAcc, "", trans)
                                Else
                                    clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "", trans)
                                End If
                                '------------------
                            End If
                        Else
                            Dim inventoryaccount() As String
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                                inventoryaccount = {strInvAcc, objtr.Item_Cost}
                            Else
                                inventoryaccount = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            End If
                            Dim receivableaccount() As String = {strAdjAcc, total * -1}
                            Dim breakageaccount() As String = {strbreakage, objtr.Breakage_Cost}
                            ArryLst.Add(breakageaccount)
                            ArryLst.Add(inventoryaccount)
                            ArryLst.Add(receivableaccount)
                            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                                ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                                If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                    clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, "", strInvAcc, "", trans)
                                Else
                                    clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "", trans)
                                End If
                            End If
                        End If
                    End If
                ElseIf clsCommon.myLen(obj.Against_Physical_Stock_No) > 0 Then
                    Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("select GL_Account,GL_Account_Inventroy_Ctrl from TSPL_PHYSICAL_STOCK where Physical_No='" + obj.Against_Physical_Stock_No + "' and Item_Code='" + objtr.Item_Code + "'", trans)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        strInvAcc = clsCommon.myCstr(dtTemp.Rows(0)("GL_Account_Inventroy_Ctrl"))
                        strAdjAcc = clsCommon.myCstr(dtTemp.Rows(0)("GL_Account"))
                        If clsCommon.myLen(strInvAcc) <= 0 Then
                            Throw New Exception("Please enter GL Account in Physical Stock No" + obj.Against_Physical_Stock_No)
                        End If
                        If clsCommon.myLen(strAdjAcc) <= 0 Then
                            Throw New Exception("Please enter Inventory GL Account in Physical Stock No" + obj.Against_Physical_Stock_No)
                        End If
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                            ArryLst.Add(Acc1)
                        Else
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            ArryLst.Add(Acc1)
                            'Ticket No- TEC/12/03/19-000442 sanjay
                            clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "I", trans)
                            'Ticket No- TEC/12/03/19-000442 sanjay
                        End If
                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    End If
                Else
                    '23/10/2012--Applied Condition I.e-If Cust Type in ('F','S') Then does not create Breakage Account
                    Dim CustType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Type_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Customer_CODE + "'", trans))
                    If Not (clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal) Then
                        Dim strbreakage As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select breakage_gl_account AS Inv_Control_Account ,Adjustment_Account as Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code= '" + objtr.Item_Code + "')", trans))
                        If clsCommon.myCdbl(objtr.Breakage_Cost) > 0 Then
                            strbreakage = clsERPFuncationality.ChangeGLAccountLocationSegment(strbreakage, strsegment, True, trans)
                            Dim BreakageAccount() As String = {strbreakage, objtr.Breakage_Cost}
                            ArryLst.Add(BreakageAccount)
                        End If
                    End If

                    '-------Added By Pankaj on 17/10/2012--------------------Fwd By---Ranjana Mam
                    Dim TotalAmt As Decimal
                    If clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal Then
                        TotalAmt = objtr.Item_Cost
                    Else
                        TotalAmt = objtr.Item_Cost + objtr.Breakage_Cost
                    End If
                    '-----------------------------------------------------------
                    If clsCommon.myLen(obj.Customer_CODE) <= 0 Then
                        strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)

                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                            ArryLst.Add(Acc1)
                        Else
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            ArryLst.Add(Acc1)

                            ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                            If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, "", strInvAcc, "", trans)
                            Else
                                clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "", trans)
                            End If
                        End If

                        If SettCreateOpeningEntryAutomatically Then
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Item_Opening_Clearing"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set Item Opening Clearing Account of purchase account set [" + clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Purchase_Class_Code")) + "]")
                            End If
                            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                            Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                            ArryLst.Add(Acc2)
                        ElseIf (clsCommon.CompairString(obj.Reference_Document, "MSE-MCC-OUT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Reference_Document, "MSE-PLT-IN") = CompairStringResult.Equal) Then
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Physical_Inv_Adjustment"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set Physical Inventory Adjustment Account of purchase account set [" + clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Purchase_Class_Code")) + "]")
                            End If

                            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                            Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                            ArryLst.Add(Acc2)
                        Else
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Adjustment_Account"))
                            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                            Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                            ArryLst.Add(Acc2)
                        End If
                    Else
                        strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                            ArryLst.Add(Acc1)
                        Else
                            Dim Acc1() As String = {strInvAcc, objtr.Item_Cost, "", "", "", "", "", "", "I"}
                            ArryLst.Add(Acc1)

                            ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                            If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, "", strInvAcc, "", trans)
                            Else
                                clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "", trans)
                            End If
                        End If
                        Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                        ArryLst.Add(Acc2)
                    End If
                End If
            End If
            If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                For Each Str() As String In ArryLst
                    ''richa agarwal 06,Dec
                    If Str.Length = 9 Then
                        Dim strNew() As String = {Str(0), -1 * Str(1), "", "", "", "", "", "", "I"}
                        ArryLstFinal.Add(strNew)
                    Else
                        Dim strNew() As String = {Str(0), -1 * Str(1)}
                        ArryLstFinal.Add(strNew)
                    End If
                Next
            Else
                For Each Str() As String In ArryLst
                    ''richa agarwal 06,Dec
                    If Str.Length = 9 Then
                        Dim strNew() As String = {Str(0), Str(1), "", "", "", "", "", "", "I"}
                        ArryLstFinal.Add(strNew)
                    Else
                        Dim strNew() As String = {Str(0), Str(1)}
                        ArryLstFinal.Add(strNew)
                    End If

                Next
            End If
        Next

        If clsCommon.CompairString(strType, "Empty Transactions") = CompairStringResult.Equal Then
            If Not clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal Then
                If ArryLstFinal IsNot Nothing AndAlso ArryLstFinal.Count > 0 AndAlso isCreateGLTransaction Then
                    Dim strRemarks As String = "Vehicle No:" + obj.Vehicle_No + ", Challan No:" + obj.Challan_No + ", Challan Date:"
                    If obj.Challan_date.HasValue Then
                        strRemarks += " " + clsCommon.GetPrintDate(obj.Challan_date, "dd/MM/yyyy")
                    End If
                    strRemarks += ", Gate EntryNo:" + obj.GateEntry_No + ", GateEntry Date:"
                    If obj.GateEntry_Date.HasValue Then
                        strRemarks += " " + clsCommon.GetPrintDate(obj.GateEntry_Date, "dd/MM/yyyy")
                    End If
                    If obj.Is_Imported = 0 Then
                        transportSql.FunGrnlEntryWithTrans(SettCreateOpeningEntryAutomatically, 0, "", "N", obj.Loc_Code, False, False, strVourcherNoForRecreateOnly, trans, obj.ProductionStoreEntry_Date, desc, strSourceCode, strSourceCodeName, obj.ProductionStoreEntryNo, obj.Description, IIf(clsCommon.myLen(obj.Customer_CODE) > 0, "C", "O"), obj.Customer_CODE, obj.Customer_NAME, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, strRemarks)
                    End If
                End If
            End If
        ElseIf clsCommon.CompairString(strType, "Production Entry") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                transportSql.FunGrnlEntryWithTrans(SettCreateOpeningEntryAutomatically, 0, "", "N", obj.Loc_Code, False, False, strVourcherNoForRecreateOnly, trans, obj.ProductionStoreEntry_Date, desc, strSourceCode, strSourceCodeName, obj.ProductionStoreEntryNo, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
            End If
        ElseIf clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                transportSql.FunGrnlEntryWithTrans(SettCreateOpeningEntryAutomatically, 0, "", "N", IIf(clsCommon.myLen(obj.Loc_Code) <= 0, obj.MainLocationCode, obj.Loc_Code), False, False, strVourcherNoForRecreateOnly, trans, obj.ProductionStoreEntry_Date, desc, strSourceCode, strSourceCodeName, obj.ProductionStoreEntryNo, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
            End If
        End If
        Return True
    End Function

    Public Shared Function CreateJETransferWithBranchAc(ByVal obj As ClsAdjustmentsStoreEntry, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
            Dim strAdjAcc As String = String.Empty
            Dim strEmpty As String = String.Empty
            Dim qry As String = String.Empty
            Dim strInvAcc As String
            Dim ArryLstFinal As ArrayList = New ArrayList()
            Dim strsegment As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.Loc_Code + "'", trans)
            Dim FromLocSeg As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.FromLocation + "'", trans)
            Dim ToLocSeg As String = clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from TSPL_LOCATION_MASTER  where Location_Code='" + obj.ToLocation + "'", trans)
            Dim desc As String = String.Empty
            'If clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            desc = "Auto Store Adjustment Against " + obj.ProductionStoreEntryNo
            'End If
            Dim ArryLst As ArrayList = New ArrayList()
            For Each objtr As ClsAdjustmentsStoreEntryDetails In obj.Arr
                Dim dtPurchaseAccountSet As DataTable = clsDBFuncationality.GetDataTable("select   Inv_Control_Account  AS Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')", trans)
                Dim Branch_Ac As String = String.Empty
                'Asked By Ashok sir
                Dim PickTCAForStockTransferAndTankerDispatch As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickTCAForStockTransferAndTankerDispatch, clsFixedParameterCode.PickTCAForStockTransferAndTankerDispatch, trans)) = 0, False, True)
                If PickTCAForStockTransferAndTankerDispatch = True Then
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                        " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                        " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objtr.Item_Code + "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Transfer Clearing Account For  for item " + objtr.Item_Code + "  (" & objtr.Item_Description & ")")
                    End If
                    Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, strsegment, True, trans)
                Else
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & FromLocSeg & "' and to_location='" & ToLocSeg & "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & ToLocSeg & " To " & FromLocSeg)
                    End If
                End If

                If dtPurchaseAccountSet Is Nothing AndAlso dtPurchaseAccountSet.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + objtr.Item_Code)
                End If

                Dim strItemProductType As String = clsItemMaster.GetItemProductType(objtr.Item_Code, trans)

                ''--- GL Begins Now
                If clsCommon.CompairString(objtr.Adjustment_Type, "QI") = CompairStringResult.Equal Then
                    'No need to make Journal Entry
                ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "QD") = CompairStringResult.Equal Then
                    'No need to make Journal Entry
                ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "CI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BI") = CompairStringResult.Equal Then
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    ''richa agarwal 11/01/2016
                    'strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    Dim IGnoreGITAccount As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IGnoreGITAccount, clsFixedParameterCode.IGnoreGITAccount, trans))
                    '' on discussion with amit sir: GIT accounts will be added on setting based 
                    'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                    ToLocSeg = clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in ( Select isnull(GIT_Location,'') from TSPL_LOCATION_MASTER where Location_Code='" + obj.FromLocation + "')", trans)
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    ''---------------------------
                    '' richa inventory account debit and branch account credit in case of cost increase and BI as per ranjana mam
                    Dim Acc1() As String = {strInvAcc, 1 * objtr.Item_Cost, "", "", "", "", "", "", "I"}
                    ArryLst.Add(Acc1)

                    ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                    clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, "", strInvAcc, "", trans)
                    '------------------
                    Dim Acc2() As String = {Branch_Ac, objtr.Item_Cost * -1}
                    ArryLst.Add(Acc2)
                    'End If



                    transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.ProductionStoreEntry_Date, desc, "IC-SE", "I/C Adjustments", obj.ProductionStoreEntryNo, obj.Description, "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Reference, "")
                ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    '' richa inventory account credit and branch account debit  in case of cost decrease and BD as per ranjana mam
                    Dim Acc1() As String = {strInvAcc, objtr.Item_Cost * -1, "", "", "", "", "", "", "I"}
                    ArryLst.Add(Acc1)
                    ''richa agarwal 5 Dec,2018 BHA/27/11/18-000731
                    clsInventoryMovement.UpdateInvControlAccount(obj.ProductionStoreEntryNo, "IC-SE", objtr.Item_Code, strInvAcc, "", "", trans)

                    Dim Acc2() As String = {Branch_Ac, 1 * objtr.Item_Cost}
                    ArryLst.Add(Acc2)
                    transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.ProductionStoreEntry_Date, desc, "IC-SE", "I/C Adjustments", obj.ProductionStoreEntryNo, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Reference, "")
                End If
            Next
        End If
        Return True
    End Function

    'Public Shared Function GetEmptyAdjustmentCode(ByVal strInvoiceNo As String, ByVal trans As SqlTransaction) As String
    '    Dim qry As String = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER_QC where ItemType='E' and Reference_Document='Sale Invoice' and Document_No='" + strInvoiceNo + "'"
    '    Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    'End Function
    'Public Shared Function GetIsMilkType(ByVal strAdjNo As String, ByVal trans As SqlTransaction) As Integer
    '    Dim qry As String = "select IsMilkType from TSPL_ADJUSTMENT_HEADER_QC where Adjustment_No='" & strAdjNo & "'"
    '    Return clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
    'End Function

    'Public Shared Function CreateAndPostEmptyReceiptOfSalesInvoiceOfTransfer(ByVal strSaleInvoiceNo As String) As Boolean
    '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    Try
    '        Dim adjNo As String = CreateEmptyReceiptOfSalesInvoiceOfTransfer(strSaleInvoiceNo, trans)
    '        ''PostData(adjNo, trans)
    '        trans.Commit()
    '    Catch ex As Exception
    '        trans.Rollback()
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function CreateEmptyReceiptOfSalesInvoiceOfTransfer(ByVal strSaleInvoiceNo As String, ByVal trans As SqlTransaction) As String
        Dim obj As ClsAdjustmentsQCC = New ClsAdjustmentsQCC()
        Dim objSaleInv As New clsSaleHead()
        objSaleInv = objSaleInv.GetData(strSaleInvoiceNo, trans)
        If objSaleInv IsNot Nothing AndAlso clsCommon.myLen(objSaleInv.Sale_Invoice_No) > 0 Then
            Dim LineNo As Integer = 1
            obj.Adjustment_Date = objSaleInv.Sale_Invoice_Date
            obj.Posting_Date = objSaleInv.Sale_Invoice_Date
            obj.Reference = objSaleInv.Ref_No
            obj.Description = objSaleInv.Description
            obj.Reference_Document = "Sale Invoice"
            obj.Document_No = strSaleInvoiceNo
            obj.Unit_Code = "ALL"
            obj.ItemType = "E"
            obj.EMP_CODE = objSaleInv.Salesman_Code
            obj.EMP_NAME = clsEmployeeMaster.GetName(objSaleInv.Salesman_Code, trans)
            obj.Customer_CODE = objSaleInv.Cust_Code
            obj.Customer_NAME = objSaleInv.Cust_Name
            obj.Created_time = objSaleInv.Date_Time_Removal
            obj.Modified_Time = objSaleInv.Date_Time_Removal
            obj.Vehicle_Code = objSaleInv.Vehicle_Code
            obj.Vehicle_No = objSaleInv.Vehicle_No
            obj.Trans_Type = "In"
            obj.Loc_Code = objSaleInv.Location
            obj.Loc_Desc = clsLocation.GetName(objSaleInv.Location, trans)
            obj.Arr = New List(Of ClsAdjustmentsQCCDetails)
            For Each objSalesTr As clsSaleDetail In objSaleInv.Arr
                If clsCommon.myLen(objSalesTr.Item_Code) > 0 AndAlso objSalesTr.Shipped_Qty > 0 AndAlso clsCommon.myCdbl(objSalesTr.Empty_Value) > 0 Then
                    Dim objtr As New ClsAdjustmentsQCCDetails()
                    objtr.Adjustment_Line_No = LineNo
                    objtr.Item_Code = clsItemMaster.GetFatherCode(objSalesTr.Item_Code, trans)
                    If clsCommon.myLen(objtr.Item_Code) <= 0 Then
                        Throw New Exception("Father code not found of item:" + objSalesTr.Item_Code)
                    End If
                    objtr.Item_Description = clsItemMaster.GetItemName(objtr.Item_Code, trans)
                    objtr.Adjustment_Type = "BI"
                    objtr.Location_Code = obj.Loc_Code
                    objtr.Item_Quantity = objSalesTr.Shipped_Qty
                    objtr.Item_Cost = objSalesTr.Empty_Value
                    Dim strUOM As String = "EC"
                    If clsCommon.CompairString("FB", objSalesTr.Unit_code) = CompairStringResult.Equal Then
                        strUOM = "EB"
                    End If
                    objtr.Unit_Code = strUOM
                    Dim qry As String = "select Adjustment_Account from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')"
                    objtr.Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                    If clsCommon.myLen(objtr.Account_Code) <= 0 Then
                        Throw New Exception("Please set Adjustment Account of purchase Account set of Item" + objtr.Item_Code)
                    End If
                    objtr.Account_Description = clsGLAccount.GetName(objtr.Account_Code, trans)
                    ''objtr.Remarks = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.Comments = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    objtr.mrp = objSalesTr.Empty_Value / objSalesTr.Shipped_Qty
                    ''objtr.MFG_Date = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(col))
                    ''objtr.Batch_No = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.Expiry_Date = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.Breakage_Cost = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    objtr.ItemType = obj.ItemType
                    ''objtr.BreakageType = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    ''objtr.LeakageQty = clsCommon.myCstr(gvLoadOut.Rows(ii).Cells(colUnitCode))
                    LineNo = LineNo + 1
                    obj.Arr.Add(objtr)

                End If
            Next
            obj.SaveData(obj, True, "", trans)
        End If
        Return obj.Adjustment_No
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal AdjustmentType As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction, Optional ByVal isJobWorkConsuption As Boolean = False, Optional ByVal isProductionEntry As Boolean = False) As ClsAdjustmentsStoreEntry

        Dim obj As ClsAdjustmentsStoreEntry = Nothing
        Dim qry As String = "SELECT * from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where 2=2"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        If isJobWorkConsuption Then
            whrClas += " and Reference_Document in ( 'JWO-SRN-JLO','JWO-SRN-JLI','JWO-SRN-RET-JLO','JWO-SRN-RET-JLI') "
        End If
        If isProductionEntry = True Then
            whrClas += " and Adjustment_Type in ( 'PRE') "
        Else
            whrClas += " and Adjustment_Type not in ( 'PRE') "
        End If
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo = (select MIN(ProductionStoreEntryNo) from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo = (select Max(ProductionStoreEntryNo) from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo = (select Min(ProductionStoreEntryNo) from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where ProductionStoreEntryNo>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo = (select Max(ProductionStoreEntryNo) from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where ProductionStoreEntryNo<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ADJUSTMENT_STORE_ENTRY_HEAD.ProductionStoreEntryNo = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsAdjustmentsStoreEntry()
            obj.chklocation = clsCommon.myCstr(dt.Rows(0)("Third_Party_Location"))
            obj.ProductionStoreEntryNo = clsCommon.myCstr(dt.Rows(0)("ProductionStoreEntryNo"))
            obj.ProductionStoreEntry_Date = clsCommon.myCDate(dt.Rows(0)("ProductionStoreEntry_Date"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
            'obj.Production_Entry = clsCommon.myCstr(dt.Rows(0)("Production_Entry"))
            obj.Reference_Document = clsCommon.myCstr(dt.Rows(0)("Reference_Document"))
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Unit_Code = clsCommon.myCstr(dt.Rows(0)("Unit_Code"))
            obj.ItemType = clsCommon.myCstr(dt.Rows(0)("ItemType"))
            obj.EMP_CODE = clsCommon.myCstr(dt.Rows(0)("EMP_CODE"))
            obj.EMP_NAME = clsCommon.myCstr(dt.Rows(0)("EMP_NAME"))
            obj.Created_time = clsCommon.myCstr(dt.Rows(0)("Created_time"))
            obj.Modified_Time = clsCommon.myCstr(dt.Rows(0)("Modified_Time"))
            obj.Vehicle_Code = clsCommon.myCstr(dt.Rows(0)("Vehicle_Code"))
            obj.Vehicle_No = clsCommon.myCstr(dt.Rows(0)("Vehicle_No"))
            obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
            obj.Customer_CODE = clsCommon.myCstr(dt.Rows(0)("Customer_CODE"))
            obj.Customer_NAME = clsCommon.myCstr(dt.Rows(0)("Customer_NAME"))
            If dt.Rows(0)("Challan_date") Is DBNull.Value Then
                obj.Challan_date = Nothing
            Else
                obj.Challan_date = clsCommon.myCDate(dt.Rows(0)("Challan_date"))
            End If
            obj.GateEntry_No = clsCommon.myCstr(dt.Rows(0)("GateEntry_No"))
            If dt.Rows(0)("GateEntry_Date") Is DBNull.Value Then
                obj.GateEntry_Date = Nothing
            Else
                obj.GateEntry_Date = clsCommon.myCDate(dt.Rows(0)("GateEntry_Date"))
            End If
            obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_Code"))
            obj.Loc_Desc = clsCommon.myCstr(dt.Rows(0)("Loc_Desc"))
            obj.EntryDateTime = clsCommon.myCDate(dt.Rows(0)("EntryDateTime"))
            obj.Trans_Type = clsCommon.myCstr(dt.Rows(0)("Trans_Type"))
            obj.Comp_Code = clsCommon.myCstr(dt.Rows(0)("Comp_Code"))
            obj.GateEnt_No = clsCommon.myCstr(dt.Rows(0)("GateEnt_No"))
            obj.Is_Imported = clsCommon.myCdbl(dt.Rows(0)("Is_Imported"))
            obj.Stock_Type = clsCommon.myCstr(dt.Rows(0)("Stock_Type"))
            obj.IsMilkType = clsCommon.myCdbl(dt.Rows(0)("IsMilkType"))
            obj.MainLocationCode = clsCommon.myCstr(dt.Rows(0)("MainLocationCode"))
            obj.MainLocationDesc = clsCommon.myCstr(dt.Rows(0)("MainLocationDesc"))
            obj.Against_Item_Stock_Conversion = clsCommon.myCstr(dt.Rows(0)("Against_Item_Stock_Conversion"))
            obj.Against_Item_Stock_Conv_Doc = clsCommon.myCstr(dt.Rows(0)("Against_Item_Stock_Conv_Doc"))
            obj.Against_Bulk_Srn_PI_adjustment = clsCommon.myCstr(dt.Rows(0)("Against_Bulk_Srn_PI_adjustment"))
            obj.Against_Physical_Stock_No = clsCommon.myCstr(dt.Rows(0)("Against_Physical_Stock_No"))
            obj.Against_Transfer_In_Doc_No = clsCommon.myCstr(dt.Rows(0)("Against_Transfer_In_Doc_No"))
            obj.Against_Tanker_Dispatch_Doc_No = clsCommon.myCstr(dt.Rows(0)("Against_Tanker_Dispatch_Doc_No"))

            obj.FromLocation = clsCommon.myCstr(dt.Rows(0)("FromLocation"))
            obj.ToLocation = clsCommon.myCstr(dt.Rows(0)("ToLocation"))
            obj.isAutoCreatedByMilkTransferIn = clsCommon.myCdbl(dt.Rows(0)("isAutoCreatedByMilkTransferIn"))

            obj.Against_AP_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Against_AP_Invoice_No"))
            obj.Against_PI_No_Difference = clsCommon.myCstr(dt.Rows(0)("Against_PI_No_Difference"))
            obj.Against_PI_No_Difference_Rejected = clsCommon.myCstr(dt.Rows(0)("Against_PI_No_Difference_Rejected"))

            obj.Adjustment_Type = clsCommon.myCstr(dt.Rows(0)("Adjustment_Type"))
            obj.Adjustment_Specification = clsCommon.myCstr(dt.Rows(0)("Adjustment_Specification"))
            obj.Is_JobWork = clsCommon.myCdbl(dt.Rows(0)("Is_JobWork"))

            obj.Against_Production_Entry = clsCommon.myCstr(dt.Rows(0)("Against_Production_Entry"))
            obj.Against_Production_Entry_QC = clsCommon.myCstr(dt.Rows(0)("Against_Production_Entry_QC"))

            qry = "SELECT  * from TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL where  ProductionStoreEntryNo='" + obj.ProductionStoreEntryNo + "' order by ProductionStoreEntry_Line_No "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsAdjustmentsStoreEntryDetails)
                Dim objTr As ClsAdjustmentsStoreEntryDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsAdjustmentsStoreEntryDetails()
                    objTr.ProductionStoreEntryNo = clsCommon.myCstr(dr("ProductionStoreEntryNo"))
                    objTr.ProductionStoreEntry_Line_No = clsCommon.myCdbl(dr("ProductionStoreEntry_Line_No"))
                    objTr.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                    objTr.Item_Description = clsCommon.myCstr(dr("Item_Description"))
                    objTr.Bar_Code = clsCommon.myCstr(dr("Bar_Code"))
                    objTr.Adjustment_Type = clsCommon.myCstr(dr("Adjustment_Type"))
                    objTr.Location_Code = clsCommon.myCstr(dr("Location_Code"))
                    objTr.Item_Quantity = clsCommon.myCdbl(dr("Item_Quantity"))
                    objTr.Unit_Cost = clsCommon.myCdbl(dr("Unit_Cost"))
                    objTr.Item_Cost = clsCommon.myCdbl(dr("Item_Cost"))
                    objTr.Unit_Code = clsCommon.myCstr(dr("Unit_Code"))
                    objTr.Account_Code = clsCommon.myCstr(dr("Account_Code"))
                    objTr.Account_Description = clsCommon.myCstr(dr("Account_Description"))
                    objTr.Remarks = clsCommon.myCstr(dr("Remarks"))
                    objTr.Comments = clsCommon.myCstr(dr("Comments"))
                    objTr.mrp = clsCommon.myCdbl(dr("mrp"))
                    If dr("MFG_Date") IsNot DBNull.Value Then
                        objTr.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                    End If
                    objTr.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                    If dr("Expiry_Date") IsNot DBNull.Value Then
                        objTr.Expiry_Date = clsCommon.myCstr(dr("Expiry_Date"))
                    End If
                    objTr.Breakage = clsCommon.myCdbl(dr("Breakage"))
                    objTr.Breakage_Cost = clsCommon.myCdbl(dr("Breakage_Cost"))
                    objTr.ItemType = clsCommon.myCstr(dr("ItemType"))
                    'objTr.Item_Type = clsCommon.myCstr(dr("Item_Type"))
                    objTr.BreakageType = clsCommon.myCstr(dr("BreakageType"))
                    objTr.LeakageQty = clsCommon.myCstr(dr("LeakageQty"))

                    objTr.Itemstatus = clsCommon.myCstr(dr("item_status"))

                    If dr("FAT_Pers") Is DBNull.Value Then
                        objTr.fat_pers = 0
                    Else
                        objTr.fat_pers = clsCommon.myCdbl(dr("FAT_Pers"))
                    End If

                    If dr("FAT_KG") Is DBNull.Value Then
                        objTr.fat_kg = 0
                    Else
                        objTr.fat_kg = clsCommon.myCdbl(dr("FAT_KG"))
                    End If

                    If dr("SNF_Pers") Is DBNull.Value Then
                        objTr.snf_pers = 0
                    Else
                        objTr.snf_pers = clsCommon.myCdbl(dr("SNF_Pers"))
                    End If

                    If dr("SNF_KG") Is DBNull.Value Then
                        objTr.snf_kg = 0
                    Else
                        objTr.snf_kg = clsCommon.myCdbl(dr("SNF_KG"))
                    End If

                    '' adde by Panch raj
                    objTr.fat_Rate = clsCommon.myCdbl(dr("fat_Rate"))
                    objTr.snf_Rate = clsCommon.myCdbl(dr("snf_Rate"))
                    objTr.fat_Amt = clsCommon.myCdbl(dr("fat_Amt"))
                    objTr.snf_Amt = clsCommon.myCdbl(dr("snf_Amt"))
                    objTr.Price_Type = clsCommon.myCstr(dr("Price_Type"))
                    objTr.MCC_Price_Code = clsCommon.myCstr(dr("MCC_Price_Code"))
                    objTr.Bulk_Price_Code = clsCommon.myCstr(dr("Bulk_Price_Code"))
                    objTr.Bin_No = clsCommon.myCstr(dr("Bin_No"))
                    'objTr.QC_Status = clsCommon.myCstr(dr("QC_Status"))
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("IC-SE", objTr.ProductionStoreEntryNo, objTr.Item_Code, objTr.ProductionStoreEntry_Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("IC-SE", objTr.ProductionStoreEntryNo, objTr.Item_Code, objTr.ProductionStoreEntry_Line_No, trans)
                    objTr.arrBatchItemNew = clsBatchInventoryNew.GetData("IC-SE", objTr.ProductionStoreEntryNo, objTr.Item_Code, objTr.ProductionStoreEntry_Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal AdjustmentType As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            DeleteData(strCode, AdjustmentType, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function DeleteData(ByVal strCode As String, ByVal AdjustmentType As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New ClsAdjustmentsStoreEntry()
        obj = obj.GetData(strCode, AdjustmentType, NavigatorType.Current, trans, Nothing)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ProductionStoreEntryNo) > 0) Then
            Try
                If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Empty Transactions", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
                ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Production Entry", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
                Else
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Store Adjustment", obj.Loc_Code, obj.ProductionStoreEntry_Date, trans)
                End If

                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If

                clsSerializeInvenotry.DeleteData("IC-SE", strCode, trans)
                clsBatchInventory.DeleteData("IC-SE", strCode, trans)
                clsBatchInventoryNew.DeleteData("IC-SE", strCode, trans)
                Dim qry As String = "delete from TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL where ProductionStoreEntryNo='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where ProductionStoreEntryNo='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '====================== for Delete Table Ticket : TEC/01/08/19-000975 By Prabhakar ==============================
                qry = "update TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where ProductionStoreEntryNo='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "update TSPL_ADJUSTMENT_STORE_ENTRY_HEAD_Delete_Data set Delete_By = '" + objCommonVar.CurrentUserCode + "' where ProductionStoreEntryNo='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                '======================================================================
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function


    Public Shared Sub ReverseAndUnpost(ByVal strCode As String)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ClsAdjustmentsQCC.ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Return ReverseAndUnpost(strCode, trans, False)
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction, ByVal isSkipBalanceCheck As Boolean) As Boolean
        Dim Qry As String = "select isnull(Posted,'N') AS Posted  from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where ProductionStoreEntryNo='" + strCode + "'"
        If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(Qry, trans), "Y") = CompairStringResult.Equal Then
            Throw New Exception("Transaction status should be posted for reverse and unpost")
        End If
        Dim dblBalance As Decimal = 0
        Try
            Dim obj As New ClsAdjustmentsStoreEntry()
            obj = obj.GetData(strCode, "", NavigatorType.Current, trans)
            Dim SettDoNotStopOnItemBalanceExceptionStoreAdjustment As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotStopOnItemBalanceExceptionStoreAdjustment, clsFixedParameterCode.DoNotStopOnItemBalanceExceptionStoreAdjustment, trans)) > 0)
            If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal AndAlso Not isSkipBalanceCheck Then
                For Each objtr As ClsAdjustmentsStoreEntryDetails In obj.Arr
                    ''Main Item UDL/01/11/19-001008 by balwinder on 21/11/2019
                    If obj.IsMilkType = 1 Then
                        Dim Loc_code As String = ""
                        Dim Main_Loc_code As String = ""
                        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                            Loc_code = obj.MainLocationCode
                            Main_Loc_code = ""
                        Else
                            Loc_code = obj.Loc_Code
                            Main_Loc_code = obj.MainLocationCode
                        End If
                        dblBalance = clsInventoryMovementNew.getBalance(objtr.Item_Code, Main_Loc_code, Loc_code, obj.ProductionStoreEntryNo, obj.ProductionStoreEntry_Date, trans, objtr.Unit_Code)
                    Else
                        dblBalance = clsItemLocationDetails.getBalanceWithUnapproveForRMOther(objtr.Item_Code, obj.Loc_Code, obj.ProductionStoreEntryNo, obj.ProductionStoreEntry_Date, trans, objtr.Unit_Code)
                    End If
                    If dblBalance < objtr.Item_Quantity Then
                        If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
                            Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objtr.Item_Quantity) + Environment.NewLine + "Item : " + objtr.Item_Code + Environment.NewLine + "Unit : " + objtr.Unit_Code)
                        End If
                    End If
                    ''End of Main Item

                    ''Batch Item
                    If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                        For Each objBatch As clsBatchInventory In objtr.arrBatchItem
                            dblBalance = clsBatchInventory.GetBatchBalance(objBatch.Item_Code, objBatch.Location_Code, objBatch.Batch_No, objBatch.MRP, objBatch.UOM, objBatch.Document_Code, objBatch.Document_Type, trans)
                            If dblBalance < objBatch.Qty Then
                                If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
                                    Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objBatch.Qty) + Environment.NewLine + "Item : " + objBatch.Item_Code + Environment.NewLine + "Batch : " + objBatch.Batch_No + Environment.NewLine + "MRP : " + clsCommon.myCstr(objBatch.MRP) + Environment.NewLine + "Unit : " + objBatch.UOM)
                                End If
                            End If
                        Next
                    End If
                    If objtr.arrBatchItemNew IsNot Nothing AndAlso objtr.arrBatchItemNew.Count > 0 Then
                        For Each objBatchNew As clsBatchInventoryNew In objtr.arrBatchItemNew
                            dblBalance = clsBatchInventoryNew.GetBatchBalance(objBatchNew.Item_Code, objBatchNew.Location_Code, objBatchNew.Batch_No, objBatchNew.UOM, objBatchNew.Document_Code, objBatchNew.Document_Type, trans)
                            If dblBalance < objBatchNew.Qty Then
                                If Not SettDoNotStopOnItemBalanceExceptionStoreAdjustment Then
                                    Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objBatchNew.Qty) + Environment.NewLine + "Item : " + objBatchNew.Item_Code + Environment.NewLine + "Batch : " + objBatchNew.Batch_No + Environment.NewLine + "Unit : " + objBatchNew.UOM)
                                End If
                            End If
                        Next
                    End If
                    ''End of Batch Item
                Next
            End If

            Qry = "select Against_Physical_Stock_No from TSPL_ADJUSTMENT_STORE_ENTRY_HEAD where ProductionStoreEntryNo ='" + strCode + "'  and isnull(Against_Physical_Stock_No,'')<>'' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current document is created Against Physical stock-"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("Against_Physical_Stock_No"))
                Next
                Throw New Exception(Qry)
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in('IC-SE', 'GL-JE') and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            VoucherNo = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER_OP where Source_Code in('IC-SE', 'GL-JE') and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS_OP where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER_OP where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=null where Against_Inv_Movement_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='IC-SE')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = " update TSPL_BATCH_ITEM_New set Against_Inv_Movement_New_Trans_Id=null where Against_Inv_Movement_New_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + strCode + "' and Trans_Type='IC-SE') "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='IC-SE'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + strCode + "' and Trans_Type='IC-SE'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)



            Qry = "Update TSPL_ADJUSTMENT_STORE_ENTRY_HEAD set Posted = 'N' where ProductionStoreEntryNo='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
        Catch ex As Exception
            Throw New Exception("Error in Production Store Entry No [" + strCode + "]" + Environment.NewLine + ex.Message)
        End Try
        Return True
    End Function

    'Public Shared Function GetMilkRate(ByVal StdFatW As Decimal, ByVal StdSNfW As Decimal, ByVal StdFATRatio As Decimal, ByVal StdSNFRatio As Decimal, ByVal StdRate As Decimal, ByVal fatKG As Decimal, ByVal snfKG As Decimal, ByVal Qty As Decimal)
    '    Dim FATRate As Double = 0
    '    Dim SNFRate As Double = 0
    '    Dim FATValue As Double = 0
    '    Dim SNfValue As Double = 0
    '    Dim whrcls As String = String.Empty

    '    FATRate = MyMath.RoundDown(clsCommon.myCdbl(StdRate) * StdFatW / StdFATRatio, 2)
    '    SNFRate = MyMath.RoundDown(clsCommon.myCdbl(StdRate) * StdSNfW / StdSNFRatio, 2)
    '    FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
    '    SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
    '    Return ((FATValue + SNfValue) / IIf(Qty <= 0, 1, Qty))
    'End Function


    Public Shared Function GetBatchBalanceAdjustmentsStoreEntry(ByVal strItemCode As String, ByVal strLocationCode As String, ByVal strBatchNo As String, ByVal dblMRP As Double, ByVal strUOM As String, ByVal strCurrDocNo As String, ByVal strCurrDocType As String, ByVal trans As SqlTransaction) As Double
        Dim qry As String = "select  Qty from ( select Batch_No as BatchNo,Min(Manufacture_Date) as ManufactureDate,MAX(Expiry_Date) as ExpiryDate,cast( round( sum(Qty * (case when Document_Type='IC-QC' then 1 else case when Document_Type='IC-SE' then -1 else 0 end end )),2,1) as decimal(18,2)) as Qty from ("
        qry += " select * from ("
        qry += " select TSPL_BATCH_ITEM.Document_Type,TSPL_BATCH_ITEM.Batch_No,TSPL_BATCH_ITEM.In_Out_Type,TSPL_BATCH_ITEM.UOM as OrgUOM,TSPL_BATCH_ITEM.Qty as OrgQty,TSPL_BATCH_ITEM.MRP as OrgMRP,TSPL_BATCH_ITEM.Expiry_Date,TSPL_BATCH_ITEM.Manufacture_Date, convert(decimal(18,2),(TSPL_BATCH_ITEM.Qty*TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/ConvertedUOM.Conversion_Factor) as Qty, (TSPL_BATCH_ITEM.MRP /TSPL_ITEM_UOM_DETAIL.Conversion_Factor)*ConvertedUOM.Conversion_Factor as MRP"
        qry += " from TSPL_BATCH_ITEM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_BATCH_ITEM.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BATCH_ITEM.UOM"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as ConvertedUOM on ConvertedUOM.Item_Code=TSPL_BATCH_ITEM.Item_Code and ConvertedUOM.UOM_Code='" + strUOM + "'"
        qry += " where TSPL_BATCH_ITEM.Item_Code='" + strItemCode + "' and TSPL_BATCH_ITEM.Location_Code='" + strLocationCode + "' and  TSPL_BATCH_ITEM.Batch_No='" + strBatchNo + "'"

        qry += " and TSPL_BATCH_ITEM.Document_Type in ('IC-QC','IC-SE')"
        qry += " ) xx where 2=2 "
        If dblMRP <> 0 Then
            qry += "and MRP='" + clsCommon.myCstr(dblMRP) + "'"
        End If
        qry += " )xxx"
        qry += " group by Batch_No )xxxx "
        Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
    End Function

End Class

Public Class ClsAdjustmentsStoreEntryDetails
#Region "Variables"
    Public Itemstatus As String = Nothing
    Public ProductionStoreEntryNo As String = Nothing
    Public ProductionStoreEntry_Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public Item_Description As String = Nothing
    Public Bar_Code As String = Nothing
    Public Adjustment_Type As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Quantity As Double = 0
    Public Unit_Cost As Double = 0
    Public Item_Cost As Double = 0
    Public Unit_Code As String = Nothing
    Public Account_Code As String = Nothing
    Public Account_Description As String = Nothing
    Public Remarks As String = Nothing
    Public Comments As String = Nothing
    Public mrp As Double = 0
    Public MFG_Date As Date? = Nothing
    Public Batch_No As String = Nothing
    Public Expiry_Date As Date? = Nothing
    Public Breakage As Double = 0
    Public Breakage_Cost As Double = 0
    Public ItemType As String = Nothing
    Dim Item_Type As String = Nothing
    Public BreakageType As String = Nothing
    Public LeakageQty As Double = 0
    Public Basic_Price As Double = 0
    Public fat_pers As Decimal = Nothing
    Public fat_kg As Decimal = Nothing
    Public snf_kg As Decimal = Nothing
    Public snf_pers As Decimal = Nothing
    Public arrSrItem As List(Of clsSerializeInvenotry) = Nothing
    Public arrBatchItem As List(Of clsBatchInventory) = Nothing
    Public arrBatchItemNew As List(Of clsBatchInventoryNew) = Nothing
    '' add by Panch raj
    Public fat_Rate As Decimal = 0
    Public fat_Amt As Decimal = 0
    Public snf_Rate As Decimal = 0
    Public snf_Amt As Decimal = 0
    Public Price_Type As String = ""
    Public MCC_Price_Code As String = ""
    Public Bulk_Price_Code As String = ""

    Public Bin_No As String = Nothing
    Public QC_Status As String = Nothing
    'Public PS_GL_Account_Inventroy_Ctrl As String = "" ''Not a table column
    'Public PS_GL_Account As String = "" ''Not a table column
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strLocationCode As String, ByVal Arr As List(Of ClsAdjustmentsStoreEntryDetails), ByVal trans As SqlTransaction, ByVal InoutType As String, ByVal DocDate As DateTime) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As ClsAdjustmentsStoreEntryDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "ProductionStoreEntryNo", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Location_Code", strLocationCode)
                clsCommon.AddColumnsForChange(coll, "ProductionStoreEntry_Line_No", counter)

                clsCommon.AddColumnsForChange(coll, "Item_Code", objtr.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Description", objtr.Item_Description)
                clsCommon.AddColumnsForChange(coll, "Unit_Code", objtr.Unit_Code)
                clsCommon.AddColumnsForChange(coll, "Bar_Code", objtr.Bar_Code, True)
                clsCommon.AddColumnsForChange(coll, "Adjustment_Type", objtr.Adjustment_Type)
                clsCommon.AddColumnsForChange(coll, "Item_Quantity", objtr.Item_Quantity)
                clsCommon.AddColumnsForChange(coll, "Unit_Cost", objtr.Unit_Cost)
                clsCommon.AddColumnsForChange(coll, "Item_Cost", objtr.Item_Cost)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_PURCHASE_ACCOUNTS.Adjustment_Account ,TSPL_GL_ACCOUNTS.Description  from  TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account where TSPL_ITEM_MASTER.Item_Code='" + objtr.Item_Code + "'", trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Plese set the Purchase Account set or its Adjustment Writeoff Account for item " + objtr.Item_Code)
                End If
                clsCommon.AddColumnsForChange(coll, "Account_Code", clsCommon.myCstr(dt.Rows(0)("Adjustment_Account")))
                clsCommon.AddColumnsForChange(coll, "Account_Description", clsCommon.myCstr(dt.Rows(0)("Description")))
                clsCommon.AddColumnsForChange(coll, "Remarks", objtr.Remarks)
                clsCommon.AddColumnsForChange(coll, "Comments", objtr.Comments)
                clsCommon.AddColumnsForChange(coll, "mrp", objtr.mrp)
                If objtr.MFG_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(objtr.MFG_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Batch_No", objtr.Batch_No)
                If objtr.Expiry_Date IsNot Nothing Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(objtr.Expiry_Date, "dd/MMM/yyyy"))
                End If
                clsCommon.AddColumnsForChange(coll, "Breakage", objtr.Breakage)
                clsCommon.AddColumnsForChange(coll, "Breakage_Cost", objtr.Breakage_Cost)
                clsCommon.AddColumnsForChange(coll, "ItemType", objtr.ItemType)
                clsCommon.AddColumnsForChange(coll, "Item_Type", objtr.Item_Type)
                clsCommon.AddColumnsForChange(coll, "BreakageType", objtr.BreakageType)
                clsCommon.AddColumnsForChange(coll, "LeakageQty", objtr.LeakageQty)
                clsCommon.AddColumnsForChange(coll, "Basic_Price", objtr.Basic_Price)
                clsCommon.AddColumnsForChange(coll, "item_status", objtr.Itemstatus)
                clsCommon.AddColumnsForChange(coll, "FAT_KG", objtr.fat_kg)
                clsCommon.AddColumnsForChange(coll, "FAT_Pers", objtr.fat_pers)
                clsCommon.AddColumnsForChange(coll, "SNF_Pers", objtr.snf_pers)
                clsCommon.AddColumnsForChange(coll, "SNF_KG", objtr.snf_kg)

                '' Added by Panch Raj
                clsCommon.AddColumnsForChange(coll, "fat_Rate", objtr.fat_Rate)
                clsCommon.AddColumnsForChange(coll, "snf_Rate", objtr.snf_Rate)
                'If ClsAdjustments.GetIsMilkType(strDocNo, trans) = 1 AndAlso (clsCommon.CompairString(objtr.Adjustment_Type, "CI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal) Then
                '    clsCommon.AddColumnsForChange(coll, "fat_Amt", Math.Round(objtr.Item_Cost * 2 / 3, 2))
                '    clsCommon.AddColumnsForChange(coll, "snf_Amt", (objtr.Item_Cost - Math.Round(objtr.Item_Cost * 2 / 3, 2)))
                'Else
                '    clsCommon.AddColumnsForChange(coll, "fat_Amt", objtr.fat_Amt)
                '    clsCommon.AddColumnsForChange(coll, "snf_Amt", objtr.snf_Amt)
                'End If
                clsCommon.AddColumnsForChange(coll, "fat_Amt", objtr.fat_Amt)
                clsCommon.AddColumnsForChange(coll, "snf_Amt", objtr.snf_Amt)

                clsCommon.AddColumnsForChange(coll, "Price_Type", objtr.Price_Type, True)
                clsCommon.AddColumnsForChange(coll, "MCC_Price_Code", objtr.MCC_Price_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bulk_Price_Code", objtr.Bulk_Price_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bin_No", objtr.Bin_No)
                'QC_Status
                'clsCommon.AddColumnsForChange(coll, "QC_Status", objtr.QC_Status)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_STORE_ENTRY_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                clsSerializeInvenotry.SaveData("IC-SE", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.arrSrItem, trans)
                clsBatchInventory.SaveData("IC-SE", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.mrp, objtr.Unit_Code, objtr.arrBatchItem, trans)
                clsBatchInventoryNew.SaveData("IC-SE", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.mrp, objtr.Unit_Code, objtr.arrBatchItemNew, trans)

                ' SaveTagNo()
                Dim objAd As New ClsAdjustmentsStoreEntryDetails
                objAd.SaveTagNo(objtr.Item_Code, objtr.arrSrItem, trans)
                counter += 1
            Next
        End If
        Return True

    End Function

    Public Function SaveTagNo(ByVal Item_code As String, ByRef arr As List(Of clsSerializeInvenotry), ByVal trans As SqlTransaction)
        If arr IsNot Nothing AndAlso arr.Count > 0 Then
            For Each obj As clsSerializeInvenotry In arr
                Dim objTag As New clsassetservicemaster
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_ITEM_MASTER_CATEGORY where Item_code = '" + Item_code + "'", trans)
                If obj.Tag_No = "" Then
                    GoTo a
                End If
                objTag.tagno = obj.Tag_No
                objTag.serialno = obj.Auto_Sr_No
                objTag.asstcode = Item_code

                If dt.Rows.Count > 0 Then
                    objTag.comlevel1 = dt.Rows(0)("Item_Category_Code")
                    objTag.lev1code = dt.Rows(0)(3)
                End If
                If dt.Rows.Count > 1 Then
                    objTag.comlevel2 = dt.Rows(1)("Item_Category_Code")
                    objTag.lev2code = dt.Rows(1)(3)
                End If
                If dt.Rows.Count > 2 Then
                    objTag.comlevel3 = dt.Rows(2)("Item_Category_Code")
                    objTag.lev3code = dt.Rows(2)(3)
                End If
                If dt.Rows.Count > 3 Then
                    objTag.comlevel4 = dt.Rows(3)("Item_Category_Code")
                    objTag.lev4code = dt.Rows(3)(3)
                End If

                objTag.comlevel5 = ""
                Dim qry1 As String = "select count(*) from TSPL_VISI_MASTER where VISI_ID='" + objTag.serialno + "' and asset_no='" + objTag.asstcode + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                If check1 = 0 Then
                    clsassetservicemaster.SaveData(objTag, True, trans)
                End If
a:          Next
        End If
        Return True
    End Function
End Class



