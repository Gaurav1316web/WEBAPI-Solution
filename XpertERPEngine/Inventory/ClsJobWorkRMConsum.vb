'--Updation By--pankaj Kumar----Punching Date, SourceDocdate, Entry Date Should be Equal as well as SOurceDocdate-----Fwd By-Priti Mam
'23/10/2012-6:22PM--Updation By--pankaj Kumar----if Cust Type of Type f or S Then Do not Create Breakage Account-----Fwd By-Priti Mam

'--Updation By--priti ----for bug no BM00000000500 .galti shipra ne ki thi 
'==============BM00000003063,Updated By Rohit===========
''updation by Richa Agarwal Against Ticket No. BM00000003766,BM00000008605
''richa agarwal 10/10/2014 Against Ticket No. BM00000004253
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI

Public Class ClsJobWorkRMConsum
#Region "Variables"
    Public Against_Physical_Stock_No As String = Nothing
    Public chklocation As String = Nothing
    Public Adjustment_No As String = Nothing
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

    Public Arr As List(Of ClsJobWorkRMConsumDetails) = Nothing
#End Region
    Public Function SaveData(ByVal obj As ClsJobWorkRMConsum, ByVal isNewEntry As Boolean, ByVal adjustmentType As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            SaveData(obj, isNewEntry, "", trans, adjustmentType)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Function SaveData(ByVal obj As ClsJobWorkRMConsum, ByVal isNewEntry As Boolean, ByVal strAdjustmentNoTemp As String, ByVal trans As SqlTransaction, ByVal adjustmentType As String) As Boolean
        Dim cntr As Integer = 0
        Dim isSaved As Boolean = True
        Try
            If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.mbtnEmptyTrans, obj.Loc_Code, obj.Adjustment_Date, trans)
            ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.frmAdjProductionEntry, obj.Loc_Code, obj.Adjustment_Date, trans)
            Else
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, "Job Work Inventory", obj.Loc_Code, obj.Adjustment_Date, trans)
            End If

            clsSerializeInvenotry.DeleteData("JW-IN", obj.Adjustment_No, trans)
            clsBatchInventory.DeleteData("JW-IN", obj.Adjustment_No, trans)
            Dim qry As String = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No='" + obj.Adjustment_No + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim strDocNo As String = ""
            Dim dtCurrent As DateTime = clsCommon.GETSERVERDATE(trans)
            If clsCommon.myLen(strAdjustmentNoTemp) > 0 Then
                obj.Adjustment_No = strAdjustmentNoTemp
                isNewEntry = True
            Else
                If isNewEntry Then
                    Dim strDoc As String = ""
                    Dim strDocTrans As String = ""

                    'If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                    '    strDoc = clsDocType.EmptyTransaction
                    '    If obj.isBySaleInvoice Then
                    '        strDocTrans = clsDocTransactionType.EmptyTransactionBySaleInvoice
                    '    Else

                    '        If clsCommon.CompairString(obj.Trans_Type, "Out") = CompairStringResult.Equal Then
                    '            strDocTrans = clsDocTransactionType.EmptyTransactionOut
                    '        Else
                    '            If clsCommon.myLen(obj.Reference_Document) > 0 Then
                    '                strDocTrans = clsDocTransactionType.EmptyTransactionRouteIn
                    '            Else
                    '                strDocTrans = clsDocTransactionType.EmptyTransactionDepotIn
                    '            End If
                    '        End If
                    '    End If
                    'ElseIf clsCommon.CompairString(obj.ItemType, "RM") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.ItemType, "OT") = CompairStringResult.Equal Then
                    '    strDoc = clsDocType.StoreAdjustment
                    '    strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustment
                    'ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                    '    strDoc = clsDocType.ProductionEntry
                    '    If clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Then
                    '        strDocTrans = clsDocTransactionType.ProductionEntryFGTrading
                    '    ElseIf clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                    '        strDocTrans = clsDocTransactionType.ProductionEntryFGManufacturing
                    '    End If
                    'Else
                    '    Throw New Exception("Item Type is Not Correct To Generate the Transaction Code")
                    'End If
                    strDoc = clsDocType.StoreAdjustment
                    strDocTrans = clsDocTransactionType.StoreAdjustmentAdjustment

                    If clsCommon.myLen(strDoc) <= 0 Then
                        Throw New Exception("Document type not found")
                    End If
                    If clsCommon.myLen(strDocTrans) <= 0 Then
                        Throw New Exception("Document Transaction type not found")
                    End If
                    If clsCommon.CompairString(adjustmentType, "JW") = CompairStringResult.Equal Then
                        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                            obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, clsDocType.JobWorkInventory, "", obj.MainLocationCode)
                        Else
                            obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, clsDocType.JobWorkInventory, "", obj.Loc_Code)
                        End If
                    ElseIf clsCommon.CompairString(adjustmentType, "RM") = CompairStringResult.Equal Then
                        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                            obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, clsDocType.RawMilkConsumtion, "", obj.MainLocationCode)
                        Else
                            obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, clsDocType.RawMilkConsumtion, "", obj.Loc_Code)
                        End If
                    Else
                        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                            obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, strDoc, strDocTrans, obj.MainLocationCode)
                        Else
                            obj.Adjustment_No = clsERPFuncationality.GetNextCode(trans, obj.Adjustment_Date, strDoc, strDocTrans, obj.Loc_Code)
                        End If
                    End If


                End If
            End If


            If (clsCommon.myLen(obj.Adjustment_No) <= 0) Then
                Throw New Exception("Error in Document Code Generation")
            End If

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "AdjustType", "Consume")
            clsCommon.AddColumnsForChange(coll, "Adjustment_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "EntryDateTime", clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Reference", obj.Reference)
            clsCommon.AddColumnsForChange(coll, "Description", obj.Description)
            clsCommon.AddColumnsForChange(coll, "Reference_Document", obj.Reference_Document)
            clsCommon.AddColumnsForChange(coll, "Document_No", obj.Document_No)
            clsCommon.AddColumnsForChange(coll, "Unit_Code", obj.Unit_Code)
            clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
            clsCommon.AddColumnsForChange(coll, "EMP_CODE", obj.EMP_CODE)
            clsCommon.AddColumnsForChange(coll, "EMP_NAME", obj.EMP_NAME)
            clsCommon.AddColumnsForChange(coll, "Customer_CODE", obj.Customer_CODE)
            clsCommon.AddColumnsForChange(coll, "Customer_NAME", obj.Customer_NAME)
            clsCommon.AddColumnsForChange(coll, "Against_Physical_Stock_No", obj.Against_Physical_Stock_No)
            clsCommon.AddColumnsForChange(coll, "Vehicle_Code", obj.Vehicle_Code)
            clsCommon.AddColumnsForChange(coll, "Vehicle_No", obj.Vehicle_No)

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

            If isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(dtCurrent, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "Adjustment_No", obj.Adjustment_No)
                clsCommon.AddColumnsForChange(coll, "Created_time", clsCommon.GetPrintDate(obj.Adjustment_Date, "hh:mm tt"))
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_HEADER", OMInsertOrUpdate.Insert, "", trans)
            Else
                isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_HEADER", OMInsertOrUpdate.Update, "Adjustment_No='" + obj.Adjustment_No + "'", trans)
            End If
            isSaved = isSaved AndAlso ClsJobWorkRMConsumDetails.SaveData(obj.Adjustment_No, obj.Loc_Code, Arr, trans, obj.Trans_Type, obj.Adjustment_Date)

        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function

    Shared Function PostData(ByVal adjno As String, ByVal Type As String, ByVal adjustmentType As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            PostData(adjno, Type, trans, adjustmentType)
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
        Return AdjustmentEnum.strJWInvetoryTrans
    End Function

    Shared Function PostData(ByVal StrAdjustmentNo As String, ByVal strType As String, ByVal trans As SqlTransaction, ByVal adjustmentType As String, Optional ByVal MakeGLEntry As Boolean = True, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
        Dim strAdjustmentType As String = GetTransactionType(StrAdjustmentNo, trans)

        Dim obj As New ClsJobWorkRMConsum()
        obj = obj.GetData(StrAdjustmentNo, strAdjustmentType, NavigatorType.Current, trans)
        If obj Is Nothing Then
            Throw New Exception("No Data Found to Post")
        End If

        If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.mbtnEmptyTrans, obj.Loc_Code, obj.Adjustment_Date, trans)
        ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.frmAdjProductionEntry, obj.Loc_Code, obj.Adjustment_Date, trans)
        Else
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Material Management", "Job Work Inventory", obj.Loc_Code, obj.Adjustment_Date, trans)
        End If

        If clsCommon.CompairString("Y", obj.Posted) = CompairStringResult.Equal Then
            Throw New Exception("Already Posted Transaction :" + StrAdjustmentNo)
        End If
        Dim qry As String = "select Is_Post from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No=(select Document_No from TSPL_ADJUSTMENT_HEADER where Reference_Document='Sale Invoice'  and Adjustment_No='" + StrAdjustmentNo + "')"
        Dim isSaleInvoicePosted As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        If clsCommon.myLen(isSaleInvoicePosted) > 0 AndAlso clsCommon.CompairString("N", isSaleInvoicePosted) = CompairStringResult.Equal Then
            Throw New Exception("Please first post it's Sale Invoice")
        End If
        Try
            'Dim conversion As Decimal
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
            Dim ArrInventoryMovementNew As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

            For Each objtr As ClsJobWorkRMConsumDetails In obj.Arr
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

                    objInventoryMovemnt.Batch_No = objtr.Batch_No
                    objInventoryMovemnt.MFG_Date = objtr.MFG_Date
                    objInventoryMovemnt.Expiry_Date = objtr.Expiry_Date
                    objInventoryMovemnt.itemstatus = objtr.Itemstatus

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
                    If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        objInventoryMovemntnew.Location_Code = obj.MainLocationCode
                    Else
                        objInventoryMovemntnew.Location_Code = obj.Loc_Code
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
                    objInventoryMovemntnew.main_location = obj.MainLocationCode
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

                    objInventoryMovemntnew.Batch_No = objtr.Batch_No
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
                            If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Empty_Value, Balance_Amt from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + obj.Document_No + "'", trans)
                                If total > clsCommon.myCdbl(dt.Rows(0)("Empty_Value")) Then
                                    total = clsCommon.myCdbl(dt.Rows(0)("Empty_Value"))
                                End If
                            End If

                            If clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                                clsDBFuncationality.ExecuteNonQuery("update TSPL_SALE_INVOICE_HEAD set Balance_Amt = Balance_Amt+'" + clsCommon.myCstr(total) + "' where Sale_Invoice_No = '" + obj.Document_No + "'", trans)
                            Else
                                If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SALE_INVOICE_HEAD set Balance_Amt = Case When Balance_Amt<" + clsCommon.myCstr(total) + " then 0 Else Balance_Amt-" + clsCommon.myCstr(total) + " End where Sale_Invoice_No = '" + obj.Document_No + "'", trans)
                                Else
                                    clsDBFuncationality.ExecuteNonQuery("update TSPL_SALE_INVOICE_HEAD set Balance_Amt = Balance_Amt-'" + clsCommon.myCstr(total) + "' where Sale_Invoice_No = '" + obj.Document_No + "'", trans)
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            '' Anubhooti 05-Dec-2014 (GL Entry should not make in case of difference entry (SRN-PI) from PI)
            'If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1 Then
            If MakeGLEntry = True Then
                If (clsCommon.CompairString(adjustmentType, "RM") = CompairStringResult.Equal OrElse clsCommon.CompairString(adjustmentType, "JW") = CompairStringResult.Equal) Then
                    CreateJE(obj, strType, trans, strVourcherNoForRecreateOnly, adjustmentType)
                Else
                    If obj.isAutoCreatedByMilkTransferIn = 1 Then
                        CreateJETransferWithBranchAc(obj, strType, trans, strVourcherNoForRecreateOnly)
                    Else
                        CreateJE(obj, strType, trans, strVourcherNoForRecreateOnly)
                    End If
                End If


            End If
            'End If

            clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)

            If ArrInventoryMovement IsNot Nothing AndAlso ArrInventoryMovement.Count > 0 Then
                clsInventoryMovement.SaveData("JW-IN", obj.Adjustment_No, obj.Adjustment_Date, clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            If ArrInventoryMovementNew IsNot Nothing AndAlso ArrInventoryMovementNew.Count > 0 Then
                clsInventoryMovementNew.SaveData("JW-IN", obj.Adjustment_No, obj.Adjustment_Date, clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MM/yyyy"), ArrInventoryMovementNew, trans)
            End If
            '--------------------------------------------------------------------------------------------------------------------------------------
            ''--- GL End
            qry = " update TSPL_ADJUSTMENT_HEADER  set Posted='Y' ,Posting_Date='" + clsCommon.GetPrintDate(obj.Adjustment_Date, "dd/MMM/yyyy hh:mm tt") + "',modified_time='" + clsCommon.GetPrintDate(obj.Adjustment_Date, "hh:mm tt") + "' where Adjustment_No='" + obj.Adjustment_No + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return True
    End Function
    Public Shared Function CreateMilkTransferAdjustmentDoc(objDisp As clsMccDispatch, obj As clsMilkTransferIn, trans As SqlTransaction) As Boolean
        ''changes by shivani against ticket no[BM00000008939]
        Dim rValue As Boolean = False
        Try
            Dim rcptQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Net_Weight from tspl_weighment_detail where Weighment_No='" & obj.Weighment_No & "'", trans))
            Dim isQtyDescreased As Boolean = False
            Dim isAmountDecreased As Boolean = False
            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
            Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
            Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))
            Dim FatValue As Double = (objW.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
            Dim SnfValue As Double = (objW.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
            Dim rcptAmount As Double = Math.Round((FatValue + SnfValue), 2)
            Dim diffamt As Double = rcptAmount - objDisp.Amount
            ''RICHA AGARWAL CHANGE  Dim diffQty As Double = objW.Net_Weight- objDisp.Net_Qty  BM00000008605
            Dim diffQty As Double = objDisp.Net_Qty - objW.Net_Weight

            Dim dblfatkgmilkin As Double = Math.Round(((objW.Net_Weight * FatQcPer) / 100), 3)
            Dim dblSNFkgmilkin As Double = Math.Round(((objW.Net_Weight * SNFQcPer) / 100), 3)

            Dim dblfatkg As Double = objDisp.FAT_KG - dblfatkgmilkin
            Dim dblSNFkg As Double = objDisp.SNF_KG - dblSNFkgmilkin
            ''----------------------------
            Dim tnkrNo As String = objDisp.Tanker_No
            Dim Loc As String = objDisp.MCC_Code
            Dim Silo As String = ""
            ''richa agarwal
            '  Dim PostingDate As Date = clsCommon.GETSERVERDATE(trans)
            Dim PostingDate As Date = obj.Receipt_Challan_Date
            ''------------------
            If diffQty < 0 Then
                isQtyDescreased = True
            Else
                isQtyDescreased = False
            End If

            If diffamt > 0 Then
                isAmountDecreased = False
            Else
                isAmountDecreased = True
                diffamt = diffamt * -1
            End If


            If diffQty > 0 And diffamt > 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "In"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""
                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "BI"
                objAdjOutTR.Item_Quantity = diffQty
                objAdjOutTR.Item_Cost = diffamt
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code
                objAdjOutTR.fat_pers = FatQcPer
                objAdjOutTR.snf_pers = SNFQcPer
                ''richa agarwal BM00000008605
                'objAdjOutTR.fat_kg = (FatQcPer * diffQty) / 100
                'objAdjOutTR.snf_kg = (SNFQcPer * diffQty) / 100

                objAdjOutTR.fat_kg = dblfatkg
                objAdjOutTR.snf_kg = dblSNFkg
                ''---------------------

                '' added by Panch raj
                objAdjOutTR.fat_Rate = objDisp.FAT_RATE
                objAdjOutTR.snf_Rate = objDisp.SNF_RATE
                objAdjOutTR.fat_Amt = objDisp.FAT_RATE * objAdjOutTR.fat_kg
                objAdjOutTR.snf_Amt = objDisp.SNF_RATE * objAdjOutTR.snf_kg

                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)
            ElseIf diffQty > 0 And diffamt < 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "In"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""

                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "QI"
                objAdjOutTR.Item_Quantity = diffQty
                objAdjOutTR.Item_Cost = 0
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code
                objAdjOutTR.fat_pers = FatQcPer
                objAdjOutTR.snf_pers = SNFQcPer
                ''richa agarwal BM00000008605
                'objAdjOutTR.fat_kg = (FatQcPer * diffQty) / 100
                'objAdjOutTR.snf_kg = (SNFQcPer * diffQty) / 100
                objAdjOutTR.fat_kg = dblfatkg
                objAdjOutTR.snf_kg = dblSNFkg
                ''---------------------
                '' added by Panch raj
                objAdjOutTR.fat_Rate = objDisp.FAT_RATE
                objAdjOutTR.snf_Rate = objDisp.SNF_RATE
                objAdjOutTR.fat_Amt = objDisp.FAT_RATE * objAdjOutTR.fat_kg
                objAdjOutTR.snf_Amt = objDisp.SNF_RATE * objAdjOutTR.snf_kg

                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

                ''richa agarwal 12jan BM00000008605
                'objAdjOut = New ClsJobWorkRMConsum
                'objAdjOut.Adjustment_Date = PostingDate
                'objAdjOut.Posting_Date = PostingDate
                'objAdjOut.EntryDateTime = PostingDate

                'objAdjOut.Loc_Code = objDisp.MCC_Code
                'objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                'objAdjOut.Trans_Type = "Out"
                'objAdjOut.IsMilkType = 1
                ''objAdjOut.Loc_Code = ""
                'objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                'objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                'objAdjOut.MainLocationCode = objDisp.MCC_Code
                'objAdjOut.FromLocation = objDisp.MCC_Code
                'objAdjOut.ToLocation = obj.location_code
                'objAdjOut.isAutoCreatedByMilkTransferIn = 1
                'objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                'objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                'objAdjOutTR = New ClsJobWorkRMConsumDetails()

                'objAdjOutTR.Item_Code = objDisp.Item_Code
                'objAdjOutTR.Item_Description = objDisp.Item_Desc
                'objAdjOutTR.Adjustment_Type = "CD"
                'objAdjOutTR.Item_Quantity = 0
                'objAdjOutTR.Item_Cost = Math.Abs(diffamt)
                'objAdjOutTR.mrp = 0
                'objAdjOutTR.Unit_Code = objDisp.UOM_Code
                'objAdjOut.Arr.Add(objAdjOutTR)
                'rValue = objAdjOut.SaveData(objAdjOut, True, "", trans)
                'rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

            ElseIf diffQty < 0 And diffamt < 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate

                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "Out"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "BD"
                objAdjOutTR.Item_Quantity = Math.Abs(diffQty)
                objAdjOutTR.Item_Cost = Math.Abs(diffamt)
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code
                objAdjOutTR.fat_pers = FatQcPer
                objAdjOutTR.snf_pers = SNFQcPer

                ''richa agarwal BM00000008605
                'objAdjOutTR.fat_kg = (FatQcPer * Math.Abs(diffQty)) / 100
                'objAdjOutTR.snf_kg = (SNFQcPer * Math.Abs(diffQty)) / 100

                objAdjOutTR.fat_kg = dblfatkg
                objAdjOutTR.snf_kg = dblSNFkg
                ''---------------------

                '' added by Panch raj
                objAdjOutTR.fat_Rate = objDisp.FAT_RATE
                objAdjOutTR.snf_Rate = objDisp.SNF_RATE
                objAdjOutTR.fat_Amt = objDisp.FAT_RATE * objAdjOutTR.fat_kg
                objAdjOutTR.snf_Amt = objDisp.SNF_RATE * objAdjOutTR.snf_kg
                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

            ElseIf diffQty < 0 And diffamt > 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate

                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "Out"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""
                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No

                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "QD"
                objAdjOutTR.Item_Quantity = Math.Abs(diffQty)
                objAdjOutTR.fat_pers = FatQcPer
                objAdjOutTR.snf_pers = SNFQcPer

                ''richa agarwal BM00000008605
                ' objAdjOutTR.fat_kg = (FatQcPer * Math.Abs(diffQty)) / 100
                'objAdjOutTR.snf_kg = (SNFQcPer * Math.Abs(diffQty)) / 100
                objAdjOutTR.fat_kg = dblfatkg
                objAdjOutTR.snf_kg = dblSNFkg

                ''---------------------


                ''richa agarwal change objAdjOutTR.Item_Cost = 0
                objAdjOutTR.Item_Cost = Math.Abs(diffamt)
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code

                '' added by Panch raj
                objAdjOutTR.fat_Rate = objDisp.FAT_RATE
                objAdjOutTR.snf_Rate = objDisp.SNF_RATE
                objAdjOutTR.fat_Amt = objDisp.FAT_RATE * objAdjOutTR.fat_kg
                objAdjOutTR.snf_Amt = objDisp.SNF_RATE * objAdjOutTR.snf_kg

                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

                ''richa agarwal 12jan
                'objAdjOut = New ClsJobWorkRMConsum
                'objAdjOut.Adjustment_Date = PostingDate
                'objAdjOut.Posting_Date = PostingDate
                'objAdjOut.EntryDateTime = PostingDate
                'objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                'objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                'objAdjOut.Loc_Code = objDisp.MCC_Code
                'objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                'objAdjOut.Trans_Type = "In"
                'objAdjOut.IsMilkType = 1

                ''objAdjOut.Loc_Code = ""
                'objAdjOut.MainLocationCode = objDisp.MCC_Code
                'objAdjOut.FromLocation = objDisp.MCC_Code
                'objAdjOut.ToLocation = obj.location_code
                'objAdjOut.isAutoCreatedByMilkTransferIn = 1
                'objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                'objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                'objAdjOutTR = New ClsJobWorkRMConsumDetails()

                'objAdjOutTR.Item_Code = objDisp.Item_Code
                'objAdjOutTR.Item_Description = objDisp.Item_Desc
                'objAdjOutTR.Adjustment_Type = "CI"
                'objAdjOutTR.Item_Quantity = 0
                'objAdjOutTR.Item_Cost = diffamt
                'objAdjOutTR.mrp = 0
                'objAdjOutTR.Unit_Code = objDisp.UOM_Code
                'objAdjOut.Arr.Add(objAdjOutTR)
                'rValue = objAdjOut.SaveData(objAdjOut, True, "", trans)
                'rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

            ElseIf diffQty < 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "Out"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""
                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "QD"
                objAdjOutTR.Item_Quantity = Math.Abs(diffQty)
                objAdjOutTR.Item_Cost = 0
                objAdjOutTR.fat_pers = FatQcPer
                objAdjOutTR.snf_pers = SNFQcPer
                objAdjOutTR.fat_kg = (FatQcPer * Math.Abs(diffQty)) / 100
                objAdjOutTR.snf_kg = (SNFQcPer * Math.Abs(diffQty)) / 100
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code

                '' added by Panch raj
                objAdjOutTR.fat_Rate = objDisp.FAT_RATE
                objAdjOutTR.snf_Rate = objDisp.SNF_RATE
                objAdjOutTR.fat_Amt = objDisp.FAT_RATE * objAdjOutTR.fat_kg
                objAdjOutTR.snf_Amt = objDisp.SNF_RATE * objAdjOutTR.snf_kg

                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

            ElseIf diffQty > 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "In"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""
                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "QI"
                objAdjOutTR.Item_Quantity = diffQty
                objAdjOutTR.Item_Cost = 0
                objAdjOutTR.fat_pers = FatQcPer
                objAdjOutTR.snf_pers = SNFQcPer
                objAdjOutTR.fat_kg = (FatQcPer * Math.Abs(diffQty)) / 100
                objAdjOutTR.snf_kg = (SNFQcPer * Math.Abs(diffQty)) / 100
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code

                '' added by Panch raj
                objAdjOutTR.fat_Rate = objDisp.FAT_RATE
                objAdjOutTR.snf_Rate = objDisp.SNF_RATE
                objAdjOutTR.fat_Amt = objDisp.FAT_RATE * objAdjOutTR.fat_kg
                objAdjOutTR.snf_Amt = objDisp.SNF_RATE * objAdjOutTR.snf_kg

                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

            ElseIf diffamt > 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "In"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""

                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "CI"
                objAdjOutTR.Item_Quantity = 0
                objAdjOutTR.Item_Cost = Math.Abs(diffamt)
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code

                'added by shivani 15 march 2016
                objAdjOutTR.fat_kg = dblfatkg
                objAdjOutTR.snf_kg = dblSNFkg
                objAdjOutTR.fat_pers = FatQcPer
                objAdjOutTR.snf_pers = SNFQcPer
                objAdjOutTR.fat_Rate = objDisp.FAT_RATE
                objAdjOutTR.snf_Rate = objDisp.SNF_RATE
                objAdjOutTR.fat_Amt = objDisp.FAT_RATE * objAdjOutTR.fat_kg
                objAdjOutTR.snf_Amt = objDisp.SNF_RATE * objAdjOutTR.snf_kg


                '-----------------------------

                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)

            ElseIf diffamt < 0 Then
                Dim objAdjOut As New ClsJobWorkRMConsum
                objAdjOut.Adjustment_Date = PostingDate
                objAdjOut.Posting_Date = PostingDate
                objAdjOut.EntryDateTime = PostingDate
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Loc_Code = objDisp.MCC_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objDisp.MCC_Code, trans)
                objAdjOut.Trans_Type = "Out"
                objAdjOut.IsMilkType = 1
                'objAdjOut.Loc_Code = ""
                objAdjOut.MainLocationCode = objDisp.MCC_Code
                objAdjOut.FromLocation = objDisp.MCC_Code
                objAdjOut.ToLocation = obj.location_code
                objAdjOut.isAutoCreatedByMilkTransferIn = 1
                objAdjOut.Description = " Auto Adjustment Against Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " And Milk Transfer In No: " & obj.Receipt_Challan_No & " Tanker No: " & objDisp.Tanker_No & " From Location: " & clsLocation.GetName(objDisp.MCC_Code, trans) & " To Location : " & clsLocation.GetName(obj.location_code, trans)
                objAdjOut.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objAdjOutTR As New ClsJobWorkRMConsumDetails()

                objAdjOutTR.Item_Code = objDisp.Item_Code
                objAdjOutTR.Item_Description = objDisp.Item_Desc
                objAdjOutTR.Adjustment_Type = "CD"
                objAdjOutTR.Item_Quantity = 0
                objAdjOutTR.Item_Cost = Math.Abs(diffamt)
                objAdjOutTR.mrp = 0
                objAdjOutTR.Unit_Code = objDisp.UOM_Code
                objAdjOut.Arr.Add(objAdjOutTR)
                rValue = objAdjOut.SaveData(objAdjOut, True, "", trans, "")
                rValue = ClsJobWorkRMConsum.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)
            ElseIf diffamt = 0 AndAlso diffQty = 0 Then
                rValue = True
            End If





        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function



    Public Shared Function CreateJE(ByVal obj As ClsJobWorkRMConsum, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing, Optional ByVal adjustmentType As String = Nothing) As Boolean
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


        Dim desc As String = String.Empty
        If clsCommon.CompairString(strType, "Adjustment Entry") = CompairStringResult.Equal Then
            desc = "Adjustment Against " + obj.Adjustment_No
        ElseIf clsCommon.CompairString(strType, "Empty Transactions") = CompairStringResult.Equal Then
            desc = "Empty transaction Against " + obj.Adjustment_No
        ElseIf clsCommon.CompairString(strType, "Production Entry") = CompairStringResult.Equal Then
            desc = "Production Entry Against " + obj.Adjustment_No
        ElseIf clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            desc = "Store Adjustment Against " + obj.Adjustment_No
        ElseIf clsCommon.CompairString(strType, "Job Work Inventory") = CompairStringResult.Equal Then
            desc = "Store Adjustment Against " + obj.Adjustment_No
        End If

        If clsCommon.myLen(obj.Customer_CODE) > 0 Then
            strAdjAcc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit FROM TSPL_CUSTOMER_ACCOUNT_SET INNER JOIN TSPL_CUSTOMER_MASTER ON  TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account = TSPL_CUSTOMER_MASTER.Cust_Account where Cust_Code='" + obj.Customer_CODE + "'", trans))
            If clsCommon.myLen(strAdjAcc) <= 0 Then
                Throw New Exception("Please set Container Deposit Account of customer " + obj.Customer_CODE)
            End If
            strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
        End If
        For Each objtr As ClsJobWorkRMConsumDetails In obj.Arr
            Dim ArryLst As ArrayList = New ArrayList()
            Dim dtPurchaseAccountSet As DataTable = clsDBFuncationality.GetDataTable("select   Inv_Control_Account  AS Inv_Control_Account , Adjustment_Account as Adjustment_Account,RM_Consumption from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')", trans)
            If dtPurchaseAccountSet Is Nothing AndAlso dtPurchaseAccountSet.Rows.Count <= 0 Then
                Throw New Exception("Please set Purchase Account set for item " + objtr.Item_Code)
            End If

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
                    Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                    ArryLst.Add(Acc1)
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
                    Else
                        If clsCommon.CompairString(adjustmentType, "RM") = CompairStringResult.Equal Then
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("RM_Consumption"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set RM Consumption Account of Purchase Account for item " + objtr.Item_Code)
                            End If
                        Else
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Adjustment_Account"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set RM Consumption Account of Adjustment Account for item " + objtr.Item_Code)
                            End If
                        End If

                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    End If


                Else

                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                    ArryLst.Add(Acc1)

                    Dim Acc2() As String = {strAdjAcc, -1 * objtr.Item_Cost}
                    ArryLst.Add(Acc2)
                End If
                'If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal Then
                '    Dim ArryLstNew As ArrayList = New ArrayList()
                '    For Each Str() As String In ArryLst
                '        Dim strNew() As String = {Str(0), -1 * Str(1)}
                '        ArryLstNew.Add(strNew)
                '    Next
                '    ArryLst = New ArrayList()
                '    ArryLst = ArryLstNew
                'End If
            ElseIf (clsCommon.CompairString(objtr.Adjustment_Type, "BI") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal) Then
                If (clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal) Then
                    If clsCommon.CompairString(obj.Reference_Document, "Sale Invoice") = CompairStringResult.Equal Then
                        'Dim strrecevable As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT TSPL_CUSTOMER_ACCOUNT_SET.Container_Deposit FROM   TSPL_SALE_INVOICE_HEAD INNER JOIN  TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code INNER JOIN TSPL_CUSTOMER_ACCOUNT_SET ON TSPL_CUSTOMER_MASTER.Cust_Account = TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No= '" + Convert.ToString(obj.Document_No) + "'", trans))
                        'strrecevable = clsERPFuncationality.ChangeGLAccountLocationSegment(strrecevable, clsCommon.myCstr(grow("Location_Code").ToString()), trans)

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
                            Dim inventoryaccount() As String = {strInvAcc, objtr.Item_Cost}
                            Dim receivableaccount() As String = {strAdjAcc, total * -1}
                            ArryLst.Add(inventoryaccount)
                            ArryLst.Add(receivableaccount)
                        Else
                            Dim inventoryaccount() As String = {strInvAcc, objtr.Item_Cost}
                            Dim receivableaccount() As String = {strAdjAcc, total * -1}
                            Dim breakageaccount() As String = {strbreakage, objtr.Breakage_Cost}
                            ArryLst.Add(breakageaccount)
                            ArryLst.Add(inventoryaccount)
                            ArryLst.Add(receivableaccount)
                        End If
                        'If clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                        '    clsDBFuncationality.ExecuteNonQuery("update TSPL_SALE_INVOICE_HEAD set Balance_Amt = Balance_Amt+'" + clsCommon.myCstr(total) + "' where Sale_Invoice_No = '" + obj.Document_No + "'", trans)
                        'Else
                        '    clsDBFuncationality.ExecuteNonQuery("update TSPL_SALE_INVOICE_HEAD set Balance_Amt = Balance_Amt-'" + clsCommon.myCstr(total) + "' where Sale_Invoice_No = '" + obj.Document_No + "'", trans)
                        'End If

                    End If
                Else
                    '23/10/2012--Applied Condition I.e-If Cust Type in ('F','S') Then does not create Breakage Account
                    Dim CustType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Cust_Type_Code  from TSPL_CUSTOMER_MASTER Where Cust_Code='" + obj.Customer_CODE + "'", trans))
                    If Not (clsCommon.CompairString(CustType, "F") = CompairStringResult.Equal OrElse clsCommon.CompairString(CustType, "S") = CompairStringResult.Equal) Then
                        Dim strbreakage As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select breakage_gl_account AS Inv_Control_Account ,Adjustment_Account as Adjustment_Account,RM_Consumption from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code= '" + objtr.Item_Code + "')", trans))
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
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                        ArryLst.Add(Acc1)

                        If clsCommon.CompairString(adjustmentType, "RM") = CompairStringResult.Equal Then
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("RM_Consumption"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set RM Consumption Account of Purchase Account for item " + objtr.Item_Code)
                            End If
                        Else
                            strAdjAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Adjustment_Account"))
                            If clsCommon.myLen(strAdjAcc) <= 0 Then
                                Throw New Exception("Please set RM Consumption Account of Adjustment Account for item " + objtr.Item_Code)
                            End If
                        End If


                        strAdjAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strAdjAcc, strsegment, True, trans)
                        Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                        ArryLst.Add(Acc2)

                    Else
                        strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                        Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                        ArryLst.Add(Acc1)

                        Dim Acc2() As String = {strAdjAcc, -1 * TotalAmt}
                        ArryLst.Add(Acc2)
                    End If

                    'If clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                    '    Dim ArryLstNew As ArrayList = New ArrayList()
                    '    For Each Str() As String In ArryLst
                    '        Dim strNew() As String = {Str(0), -1 * Str(1)}
                    '        ArryLstNew.Add(strNew)
                    '    Next
                    '    ArryLst = New ArrayList()
                    '    ArryLst = ArryLstNew
                    'End If
                End If
            End If
            If clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                For Each Str() As String In ArryLst
                    Dim strNew() As String = {Str(0), -1 * Str(1)}
                    ArryLstFinal.Add(strNew)
                Next
            Else
                For Each Str() As String In ArryLst
                    Dim strNew() As String = {Str(0), Str(1)}
                    ArryLstFinal.Add(strNew)
                Next
            End If
        Next

        '-----Updated By-Pankaj Kumar--For (There wl be no GL Entry Against Production Entry OR Stores Adjustment)-----Fwd By---Ranjana Mam---
        If clsCommon.CompairString(strType, "Empty Transactions") = CompairStringResult.Equal Then
            If Not clsCommon.CompairString(obj.Reference_Document, "Load Out/Transfer") = CompairStringResult.Equal Then
                If ArryLstFinal IsNot Nothing AndAlso ArryLstFinal.Count > 0 AndAlso isCreateGLTransaction Then

                    'Dim strRemarks As String = "Vehicle No:" + obj.Vehicle_No + ", Challan No:" + obj.Challan_No + ", Challan Date:" + IIf(obj.Challan_date.HasValue, clsCommon.GetPrintDate(obj.Challan_date, "dd/MM/yyyy"), "") + ", Gate EntryNo:" + obj.GateEntry_No + ", GateEntry Date:" + IIf(obj.GateEntry_Date.HasValue, clsCommon.GetPrintDate(obj.GateEntry_Date, "dd/MM/yyyy"), "")
                    Dim strRemarks As String = "Vehicle No:" + obj.Vehicle_No + ", Challan No:" + obj.Challan_No + ", Challan Date:"
                    If obj.Challan_date.HasValue Then
                        strRemarks += " " + clsCommon.GetPrintDate(obj.Challan_date, "dd/MM/yyyy")
                    End If
                    strRemarks += ", Gate EntryNo:" + obj.GateEntry_No + ", GateEntry Date:"
                    If obj.GateEntry_Date.HasValue Then
                        strRemarks += " " + clsCommon.GetPrintDate(obj.GateEntry_Date, "dd/MM/yyyy")
                    End If

                    If obj.Is_Imported = 0 Then
                        If clsCommon.myLen(obj.Customer_CODE) <= 0 Then
                            If strVourcherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then ''then update journal entry with existing voucher no 17/03/2015
                                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, strRemarks)
                            Else
                                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, strRemarks)
                            End If
                        Else
                            If strVourcherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "C", obj.Customer_CODE, obj.Customer_NAME, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, strRemarks)
                            Else
                                clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "C", obj.Customer_CODE, obj.Customer_NAME, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, strRemarks)
                            End If
                        End If
                    End If
                End If
            End If
        ElseIf clsCommon.CompairString(strType, "Production Entry") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                If strVourcherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, "IC-AD", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                Else
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "IC-AD", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                End If
            End If
        ElseIf clsCommon.CompairString(strType, "Store Adjustment") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                If strVourcherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                    If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.MainLocationCode, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, "IC-AD", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                    Else

                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, "IC-AD", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                    End If

                Else
                    If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.MainLocationCode, False, trans, obj.Adjustment_Date, desc, "IC-AD", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                    Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "IC-AD", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                    End If

                End If
            End If
            ElseIf clsCommon.CompairString(strType, "Job Work Inventory") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                    If strVourcherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVourcherNoForRecreateOnly) > 0 Then
                        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.MainLocationCode, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                        Else

                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVourcherNoForRecreateOnly, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                        End If

                    Else
                        If clsCommon.myLen(obj.Loc_Code) <= 0 Then
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.MainLocationCode, False, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                        Else
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLstFinal, obj.Reference, "")
                        End If

                    End If
                End If
        End If
        Return True
    End Function

    Public Shared Function CreateJETransferWithBranchAc(ByVal obj As ClsJobWorkRMConsum, ByVal strType As String, ByVal trans As SqlTransaction, Optional ByVal strVourcherNoForRecreateOnly As String = Nothing) As Boolean
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
            For Each objtr As ClsJobWorkRMConsumDetails In obj.Arr
                Dim dtPurchaseAccountSet As DataTable = clsDBFuncationality.GetDataTable("select   Inv_Control_Account  AS Inv_Control_Account  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code in  (select Purchase_Class_Code from TSPL_ITEM_MASTER where Item_Code='" + objtr.Item_Code + "')", trans)
                Dim Branch_Ac As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & FromLocSeg & "' and to_location='" & ToLocSeg & "'", trans))
                If clsCommon.myLen(Branch_Ac) <= 0 Then
                    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & ToLocSeg & " To " & FromLocSeg)
                End If
                If dtPurchaseAccountSet Is Nothing AndAlso dtPurchaseAccountSet.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + objtr.Item_Code)
                End If
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
                    If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                        ToLocSeg = clsDBFuncationality.getSingleValue("select Loc_Segment_Code from TSPL_LOCATION_MASTER where location_code in ( Select isnull(GIT_Location,'') from TSPL_LOCATION_MASTER where Location_Code='" + obj.FromLocation + "')", trans)
                        strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, ToLocSeg, True, trans)
                        ''---------------------------
                        Dim Acc1() As String = {strInvAcc, -1 * objtr.Item_Cost}
                        ArryLst.Add(Acc1)
                        Dim Acc2() As String = {Branch_Ac, objtr.Item_Cost}
                        ArryLst.Add(Acc2)
                    End If



                    ' clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Reference, "")
                ElseIf clsCommon.CompairString(objtr.Adjustment_Type, "CD") = CompairStringResult.Equal OrElse clsCommon.CompairString(objtr.Adjustment_Type, "BD") = CompairStringResult.Equal Then
                    strInvAcc = clsCommon.myCstr(dtPurchaseAccountSet.Rows(0)("Inv_Control_Account"))
                    strInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvAcc, strsegment, True, trans)
                    Dim Acc1() As String = {strInvAcc, objtr.Item_Cost}
                    ArryLst.Add(Acc1)
                    Dim Acc2() As String = {Branch_Ac, -1 * objtr.Item_Cost}
                    ArryLst.Add(Acc2)
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Adjustment_Date, desc, "JW-IN", "I/C Adjustments", obj.Adjustment_No, obj.Description, "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, obj.Reference, "")
                End If
            Next
        End If
        Return True
    End Function

    Public Shared Function GetEmptyAdjustmentCode(ByVal strInvoiceNo As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER where ItemType='E' and Reference_Document='Sale Invoice' and Document_No='" + strInvoiceNo + "'"
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
        Dim obj As ClsJobWorkRMConsum = New ClsJobWorkRMConsum()
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
            obj.Arr = New List(Of ClsJobWorkRMConsumDetails)
            For Each objSalesTr As clsSaleDetail In objSaleInv.Arr
                If clsCommon.myLen(objSalesTr.Item_Code) > 0 AndAlso objSalesTr.Shipped_Qty > 0 AndAlso clsCommon.myCdbl(objSalesTr.Empty_Value) > 0 Then
                    Dim objtr As New ClsJobWorkRMConsumDetails()
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
            obj.SaveData(obj, True, "", trans, "")
        End If
        Return obj.Adjustment_No
    End Function

    Public Function GetData(ByVal strDocNo As String, ByVal AdjustmentType As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As ClsJobWorkRMConsum

        Dim obj As ClsJobWorkRMConsum = Nothing
        Dim qry As String = "SELECT * from TSPL_ADJUSTMENT_HEADER where 2=2"
        Dim whrClas As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas = " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        'If clsCommon.CompairString(AdjustmentType, "Empty Transactions") = CompairStringResult.Equal Then
        '    whrClas += " AND ItemType='E'"
        'ElseIf clsCommon.CompairString(AdjustmentType, "Production Entry") = CompairStringResult.Equal Then
        '    whrClas += " AND ItemType IN ('FM', 'FT')"
        'ElseIf clsCommon.CompairString(AdjustmentType, "Store Adjustment") = CompairStringResult.Equal Then
        '    whrClas += " AND ItemType IN ('RM', 'OT')"
        'Else
        '    Throw New Exception("Adjustment Type is not Correct")
        'End If


        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_ADJUSTMENT_HEADER.Adjustment_No = (select MIN(Adjustment_No) from TSPL_ADJUSTMENT_HEADER where 1=1 " + whrClas + ")"
            Case NavigatorType.Last
                qry += " and TSPL_ADJUSTMENT_HEADER.Adjustment_No = (select Max(Adjustment_No) from TSPL_ADJUSTMENT_HEADER where 1=1 " + whrClas + ")"
            Case NavigatorType.Next
                qry += " and TSPL_ADJUSTMENT_HEADER.Adjustment_No = (select Min(Adjustment_No) from TSPL_ADJUSTMENT_HEADER where Adjustment_No>'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_ADJUSTMENT_HEADER.Adjustment_No = (select Max(Adjustment_No) from TSPL_ADJUSTMENT_HEADER where Adjustment_No<'" + strDocNo + "' " + whrClas + ")"
            Case NavigatorType.Current
                qry += " and TSPL_ADJUSTMENT_HEADER.Adjustment_No = '" + strDocNo + "'"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New ClsJobWorkRMConsum()
            obj.chklocation = clsCommon.myCstr(dt.Rows(0)("Third_Party_Location"))
            obj.Adjustment_No = clsCommon.myCstr(dt.Rows(0)("Adjustment_No"))
            obj.Adjustment_Date = clsCommon.myCDate(dt.Rows(0)("Adjustment_Date"))
            obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
            obj.Reference = clsCommon.myCstr(dt.Rows(0)("Reference"))
            obj.Description = clsCommon.myCstr(dt.Rows(0)("Description"))
            obj.Posted = clsCommon.myCstr(dt.Rows(0)("Posted"))
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

            obj.Against_Transfer_In_Doc_No = clsCommon.myCstr(dt.Rows(0)("Against_Transfer_In_Doc_No"))
            obj.Against_Tanker_Dispatch_Doc_No = clsCommon.myCstr(dt.Rows(0)("Against_Tanker_Dispatch_Doc_No"))

            obj.FromLocation = clsCommon.myCstr(dt.Rows(0)("FromLocation"))
            obj.ToLocation = clsCommon.myCstr(dt.Rows(0)("ToLocation"))
            obj.isAutoCreatedByMilkTransferIn = clsCommon.myCdbl(dt.Rows(0)("isAutoCreatedByMilkTransferIn"))

            obj.Against_AP_Invoice_No = clsCommon.myCstr(dt.Rows(0)("Against_AP_Invoice_No"))
            obj.Against_PI_No_Difference = clsCommon.myCstr(dt.Rows(0)("Against_PI_No_Difference"))
            obj.Against_PI_No_Difference_Rejected = clsCommon.myCstr(dt.Rows(0)("Against_PI_No_Difference_Rejected"))

            qry = "SELECT  * from TSPL_ADJUSTMENT_DETAIL where  Adjustment_No='" + obj.Adjustment_No + "' order by Adjustment_Line_No "
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry, trans)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                obj.Arr = New List(Of ClsJobWorkRMConsumDetails)
                Dim objTr As ClsJobWorkRMConsumDetails
                For Each dr As DataRow In dt.Rows
                    objTr = New ClsJobWorkRMConsumDetails()
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
                    objTr.arrSrItem = clsSerializeInvenotry.GetData("JW-IN", objTr.Adjustment_No, objTr.Item_Code, objTr.Adjustment_Line_No, trans)
                    objTr.arrBatchItem = clsBatchInventory.GetData("JW-IN", objTr.Adjustment_No, objTr.Item_Code, objTr.Adjustment_Line_No, trans)
                    obj.Arr.Add(objTr)
                Next
            End If
        End If
        Return obj
    End Function

    Public Shared Function DeleteData(ByVal strCode As String, ByVal AdjustmentType As String) As Boolean
        Dim isSaved As Boolean = False
        If (clsCommon.myLen(strCode) <= 0) Then
            Throw New Exception("Document No not found to Delete")
        End If
        Dim obj As New ClsJobWorkRMConsum()
        obj = obj.GetData(strCode, AdjustmentType, NavigatorType.Current, Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Adjustment_No) > 0) Then
            Try
                If clsCommon.CompairString(obj.ItemType, "E") = CompairStringResult.Equal Then
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.mbtnEmptyTrans, obj.Loc_Code, obj.Adjustment_Date, trans)
                ElseIf clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Or clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal Then
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.frmAdjProductionEntry, obj.Loc_Code, obj.Adjustment_Date, trans)
                Else
                    clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleMaterial, clsUserMgtCode.mbtnStoreAdjustment, obj.Loc_Code, obj.Adjustment_Date, trans)
                End If

                If (clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal) Then
                    Throw New Exception("Already Posted on :" + clsCommon.GetPrintDate(obj.Posting_Date, "dd/MM/yyyy hh:mm tt"))
                End If

                clsSerializeInvenotry.DeleteData("JW-IN", strCode, trans)
                clsBatchInventory.DeleteData("JW-IN", strCode, trans)

                Dim qry As String = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No='" + strCode + "'"
                isSaved = clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + strCode + "'"
                isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

                If (isSaved) Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        End If
        Return isSaved
    End Function


    Public Shared Sub ReverseAndUnpost(ByVal strCode As String)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ClsJobWorkRMConsum.ReverseAndUnpost(strCode, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = "select Posted  from TSPL_ADJUSTMENT_HEADER where Adjustment_No='" + strCode + "'"
        If Not clsCommon.CompairString(clsDBFuncationality.getSingleValue(Qry, trans), "Y") = CompairStringResult.Equal Then
            Throw New Exception("Transaction status should be posted for reverse and unpost")
        End If

        Try
            Dim obj As New ClsJobWorkRMConsum()
            obj = obj.GetData(strCode, "", NavigatorType.Current, trans)
            If clsCommon.CompairString(obj.Trans_Type, "In") = CompairStringResult.Equal Then
                For Each objtr As ClsJobWorkRMConsumDetails In obj.Arr
                    If objtr.arrBatchItem IsNot Nothing AndAlso objtr.arrBatchItem.Count > 0 Then
                        For Each objBatch As clsBatchInventory In objtr.arrBatchItem
                            Dim dblBalance As Double = clsBatchInventory.GetBatchBalance(objBatch.Item_Code, objBatch.Location_Code, objBatch.Batch_No, objBatch.MRP, objBatch.UOM, objBatch.Document_Code, objBatch.Document_Type, trans)
                            If dblBalance < objBatch.Qty Then
                                Throw New Exception("Balance will be going to -ve.Balance Qty : " + clsCommon.myCstr(dblBalance) + " and Entered Qty : " + clsCommon.myCstr(objBatch.Qty) + Environment.NewLine + "Item : " + objBatch.Item_Code + Environment.NewLine + "Batch : " + objBatch.Batch_No + Environment.NewLine + "MRP : " + clsCommon.myCstr(objBatch.MRP) + Environment.NewLine + "Unit : " + objBatch.UOM)
                            End If
                        Next
                    End If
                Next
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='JW-IN' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='JW-IN'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
            For Each objtr As DataRow In dt.Rows
                Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                Dim objLocationDetails As New clsItemLocationDetails()
                objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))
                ArrLocationDetails.Add(objLocationDetails)
            Next
            Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
            clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

            Qry = "update TSPL_BATCH_ITEM set Against_Inv_Movement_Trans_Id=null where Against_Inv_Movement_Trans_Id in (select Trans_Id from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='JW-IN')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + strCode + "' and Trans_Type='JW-IN'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_New where Source_Doc_No='" + strCode + "' and Trans_Type='JW-IN'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_ADJUSTMENT_HEADER set Posted = 'N' where adjustment_no='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Xtra.UpdateSaleInvoiceBalanceAmt(trans)

        Catch ex As Exception
            Throw New Exception(ex.Message)
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
End Class

Public Class ClsJobWorkRMConsumDetails
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
    '' add by Panch raj
    Public fat_Rate As Decimal = 0
    Public fat_Amt As Decimal = 0
    Public snf_Rate As Decimal = 0
    Public snf_Amt As Decimal = 0
    Public Price_Type As String = ""
    Public MCC_Price_Code As String = ""
    Public Bulk_Price_Code As String = ""

    Public Bin_No As String = Nothing

#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal strLocationCode As String, ByVal Arr As List(Of ClsJobWorkRMConsumDetails), ByVal trans As SqlTransaction, ByVal InoutType As String, ByVal DocDate As DateTime) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim counter As Integer = 1
            For Each objtr As ClsJobWorkRMConsumDetails In Arr
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
                clsCommon.AddColumnsForChange(coll, "fat_Amt", objtr.fat_Amt)
                clsCommon.AddColumnsForChange(coll, "snf_Amt", objtr.snf_Amt)

                clsCommon.AddColumnsForChange(coll, "Price_Type", objtr.Price_Type, True)
                clsCommon.AddColumnsForChange(coll, "MCC_Price_Code", objtr.MCC_Price_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bulk_Price_Code", objtr.Bulk_Price_Code, True)
                clsCommon.AddColumnsForChange(coll, "Bin_No", objtr.Bin_No)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ADJUSTMENT_DETAIL", OMInsertOrUpdate.Insert, "", trans)

                clsSerializeInvenotry.SaveData("JW-IN", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.arrSrItem, trans)
                clsBatchInventory.SaveData("JW-IN", strDocNo, DocDate, IIf(clsCommon.CompairString(InoutType, "In") = CompairStringResult.Equal, "I", "O"), objtr.Item_Code, strLocationCode, counter, objtr.mrp, objtr.Unit_Code, objtr.arrBatchItem, trans)

                ' SaveTagNo()
                Dim objAd As New ClsJobWorkRMConsumDetails
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

'Public Class clsReceiptHeader
'    Public Shared Sub ReciepEntryWithPostOfInvoice(ByVal strInvNo As String, ByVal strBankCode As String, ByVal strPaymentCode As String)
'        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
'        Try
'            ReciepEntryWithPostOfInvoice(strInvNo, strBankCode, strPaymentCode, trans)
'            trans.Commit()
'        Catch ex As Exception
'            trans.Rollback()
'            Throw New Exception(ex.Message)
'        End Try
'    End Sub


'    Public Shared Function ReciepEntryWithPostOfInvoice(ByVal strInvNo As String, ByVal strBankCode As String, ByVal strPaymentCode As String, ByVal trans As SqlTransaction) As String
'        Dim strRecNo As String = ""
'        Dim qry As String = "select TSPL_CUSTOMER_MASTER.Credit_Customer from TSPL_SALE_INVOICE_HEAD "
'        qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SALE_INVOICE_HEAD.Cust_Code where TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No='" + strInvNo + "'"

'        If Not clsCommon.CompairString("Y", clsDBFuncationality.getSingleValue(qry, trans)) = CompairStringResult.Equal Then
'            If objCommonVar.IsDemoERP Then
'                strRecNo = ReciepEntryOfInvoiceDemo(strInvNo, strBankCode, strPaymentCode, trans)
'            Else
'                strRecNo = ReciepEntryOfInvoice(strInvNo, strBankCode, strPaymentCode, trans)
'            End If
'            If Not strRecNo Is Nothing Then
'                ''To be Uncomment
'                'clsRcptEntryHeader.funRcptPost(strRecNo, trans)
'            End If
'        End If
'        Return strRecNo
'    End Function

'    Public Shared Function ReciepEntryOfInvoiceDemo(ByVal strInvNo As String, ByVal strBankCode As String, ByVal strPaymentCode As String, ByVal trans As SqlTransaction) As String
'        Dim objSaleInv As New clsCustomerInvoiceHead()
'        objSaleInv = clsCustomerInvoiceHead.GetData(strInvNo, trans)
'        Dim dblReceiptAmt As Double = objSaleInv.Document_Total
'        If dblReceiptAmt > 0 Then
'            Dim qry As String = "select SUM(Adjustment_Amount)  from TSPL_Receipt_Adjustment_Header where Doc_No='" + strInvNo + "' and Is_Post='Y'"
'            Dim dblAdjAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
'            dblReceiptAmt = dblReceiptAmt - dblAdjAmt

'            Dim originalamt As Decimal = objSaleInv.Document_Total

'            '---------------------------------------------------------------------------
'            Dim obj As New clsRcptEntryHeader()
'            obj.Entry_Desc = "AR Invoice No - " + strInvNo + " for Cash Memo No - " & objSaleInv.Against_Sale_No & "  "
'            obj.Receipt_Date = clsCommon.GetPrintDate(objSaleInv.Posting_Date, "dd/MMM/yyyy")
'            obj.Receipt_Post_Date = clsCommon.GetPrintDate(objSaleInv.Posting_Date, "dd/MMM/yyyy")
'            obj.Bank_Code = clsCommon.myCstr(strBankCode)
'            obj.Receipt_Type = "R"
'            obj.Payment_Code = clsCommon.myCstr(strPaymentCode)
'            obj.Cheque_No = ""
'            obj.Cheque_Date = Nothing
'            obj.Cust_Code = clsCommon.myCstr(objSaleInv.Customer_Code)
'            obj.Receipt_Amount = dblReceiptAmt
'            obj.Balance_Amt = dblReceiptAmt
'            obj.UnApply_Amt = dblReceiptAmt
'            obj.Apply_By = ""
'            obj.Apply_To = ""
'            obj.IsSalesmanType = "N"
'            obj.Salesman_Code = ""
'            obj.Salesman_Name = ""
'            obj.SecurityDeposit = "N"

'            obj.ArrTr = New List(Of clsReceiptDettail)

'            Dim objTr As New clsReceiptDettail()
'            objTr.Apply = "Y"
'            objTr.Receipt_Type = "I"
'            objTr.TagType = "C"
'            objTr.Document_No = strInvNo
'            If clsCommon.myLen(strInvNo) > 0 Then
'                objTr.Document_Date = clsDBFuncationality.getSingleValue("Select Sale_Invoice_Date from TSPL_SALE_INVOICE_HEAD Where Sale_Invoice_No='" + strInvNo + "'", trans)
'            End If
'            objTr.Original_Amt = originalamt
'            objTr.Pending_Balance = originalamt
'            objTr.Applied_Amount = dblReceiptAmt
'            objTr.Adjustment_No = ""
'            objTr.Comment = objSaleInv.Remarks
'            obj.ArrTr.Add(objTr)

'            obj.SaveData(obj, True, trans)
'            '---------------------------------------------------------------------------
'            Return obj.Receipt_No
'        Else
'            Throw New Exception("Receipt can't be created.Invoice amount is zero.")

'        End If

'        Return Nothing
'    End Function

'    Public Shared Function ReciepEntryOfInvoice(ByVal strInvNo As String, ByVal strBankCode As String, ByVal strPaymentCode As String, ByVal trans As SqlTransaction) As String
'        Dim objSaleInv As New clsSaleHead()
'        objSaleInv = objSaleInv.GetData(strInvNo, trans)
'        Dim dblReceiptAmt As Double = objSaleInv.Total_Invoice_Amt
'        Dim qry As String = "select SUM(Adjustment_Amount)  from TSPL_Receipt_Adjustment_Header where Doc_No='" + strInvNo + "' and Is_Post='Y'"
'        Dim dblAdjAmt As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
'        dblReceiptAmt = dblReceiptAmt - dblAdjAmt

'        Dim originalamt As Decimal = objSaleInv.Total_Invoice_Amt + objSaleInv.Empty_Value

'        Dim strQuery As String = "select Transfer_No from TSPL_SHIPMENT_MASTER where Shipment_No='" + objSaleInv.Shipment_No + "'"
'        Dim strLoadoutNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQuery, trans))

'        '---------------------------------------------------------------------------
'        Dim obj As New clsRcptEntryHeader()
'        obj.Entry_Desc = strLoadoutNo + " - Loadout No and Cash Memo No - " + objSaleInv.Sale_Invoice_No + " from Route " + objSaleInv.Route_No + "(" + objSaleInv.Route_Desc + ") " + objSaleInv.Description
'        obj.Receipt_Date = clsCommon.GetPrintDate(objSaleInv.Sale_Invoice_Date, "dd/MMM/yyyy")
'        obj.Receipt_Post_Date = obj.Receipt_Date
'        obj.Bank_Code = clsCommon.myCstr(strBankCode)
'        obj.Receipt_Type = "R"
'        obj.Payment_Code = clsCommon.myCstr(strPaymentCode)
'        obj.Cheque_No = ""
'        obj.Cheque_Date = Nothing
'        obj.Cust_Code = clsCommon.myCstr(objSaleInv.Cust_Code)
'        obj.Receipt_Amount = dblReceiptAmt
'        obj.Balance_Amt = dblReceiptAmt
'        obj.UnApply_Amt = dblReceiptAmt
'        obj.Apply_By = ""
'        obj.Apply_To = ""
'        obj.IsSalesmanType = "N"
'        obj.Salesman_Code = ""
'        obj.Salesman_Name = ""
'        obj.SecurityDeposit = "N"

'        obj.ArrTr = New List(Of clsReceiptDettail)

'        Dim objTr As New clsReceiptDettail()
'        objTr.Apply = "Y"
'        objTr.Receipt_Type = "I"
'        objTr.TagType = "N"
'        objTr.Document_No = strInvNo
'        If clsCommon.myLen(strInvNo) > 0 Then
'            objTr.Document_Date = clsDBFuncationality.getSingleValue("Select Sale_Invoice_Date from TSPL_SALE_INVOICE_HEAD Where Sale_Invoice_No='" + strInvNo + "'", trans)
'        End If
'        objTr.Original_Amt = originalamt
'        objTr.Pending_Balance = objSaleInv.Empty_Value
'        objTr.Applied_Amount = dblReceiptAmt
'        objTr.Adjustment_No = ""
'        objTr.Comment = objSaleInv.Remarks
'        obj.ArrTr.Add(objTr)

'        obj.SaveData(obj, True, trans)
'        '---------------------------------------------------------------------------
'        Return obj.Receipt_No

'    End Function
'End Class





