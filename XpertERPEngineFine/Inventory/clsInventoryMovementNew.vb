Imports common
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class clsInventoryMovementNew
#Region "Variables"
    Public itemtypeinventry As String = Nothing
    Public itemstatus As String = Nothing
    Public main_location As String = Nothing
    Public Trans_Type As String = Nothing
    Public InOut As String = Nothing
    Public Location_Code As String = Nothing
    Public Item_Code As String = Nothing
    Public Item_Desc As String = Nothing
    Public Qty As Double = 0
    Public UOM As String = Nothing
    Public Source_Doc_No As String = Nothing
    Public Source_Doc_Date As String = Nothing
    Public Entry_Date As String = Nothing
    Public Basic_Cost As Double = 0
    Public Rec_Cost As Double = 0
    Public Add_Cost As Double = 0
    Public Net_Cost As Double = 0
    Public MRP As Double = 0
    Public ItemType As String = Nothing
    Public Punching_Date As String
    Public Batch_No As String = Nothing
    Public FIFO_Cost As Double = 0
    Public LIFO_Cost As Double = 0
    Public Avg_Cost As Double = 0
    Public Posting_Date As DateTime? = Nothing
    Public MFG_Date As DateTime? = Nothing
    Public Expiry_Date As DateTime? = Nothing
    Public Stock_Qty As Double = 0
    Public Stock_UOM As String = Nothing
    Public FAT_Per As Double = 0
    Public SNF_Per As Double = 0
    Public FAT_KG As Double = 0
    Public SNF_KG As Double = 0
    Public IS_CONSUMPTION As Integer = 0
    ''RICHA AGARWAL 06/07/2015
    Public Fat_Rate As Double = 0
    Public SNF_Rate As Double = 0
    Public Fat_Amt As Double = 0
    Public SNF_Amt As Double = 0
    Public Std_Qty As Double = 0
    Public Is_Scheme_Item As String = String.Empty
    Public Inventory_DrAcc As String = String.Empty
    Public Inventory_CrAcc As String = String.Empty
    ''--------------
    Public Ref_Line_No As Integer
    Public Cust_Code As String = Nothing
    Public Cust_Name As String = Nothing
    Public Vendor_Code As String = Nothing
    Public Vendor_Name As String = Nothing
    Public Other_Location_Code As String = Nothing
    Public Other_Location_Desc As String = Nothing
    Public CalculateAvgCost As Boolean = True
    Public DonNotCalculateAvgFATSNFCost As Boolean = False
    Public CustomCoversionCLR As Decimal ''Not a Table Column
    Public Custom_UOM As String = Nothing
    Public Custom_Coversion_Factor As Decimal = 0

    Public PI_Cost As Decimal
    Public Item_Status As String
    Public Assmbly_Status As String
#End Region

    Public Shared Function DeepCopyObject(ByVal obj As clsInventoryMovementNew) As clsInventoryMovementNew
        Dim objNew As clsInventoryMovementNew = New clsInventoryMovementNew()
        objNew.Trans_Type = obj.Trans_Type
        objNew.InOut = obj.InOut
        objNew.Location_Code = obj.Location_Code
        objNew.Item_Code = obj.Item_Code
        objNew.Item_Desc = obj.Item_Desc
        objNew.Qty = obj.Qty
        objNew.UOM = obj.UOM
        objNew.Source_Doc_No = obj.Source_Doc_No
        objNew.Source_Doc_Date = obj.Source_Doc_Date
        objNew.Entry_Date = obj.Entry_Date
        objNew.Basic_Cost = obj.Basic_Cost
        objNew.Rec_Cost = obj.Rec_Cost
        objNew.Add_Cost = obj.Add_Cost
        objNew.Net_Cost = obj.Net_Cost
        objNew.ItemType = obj.ItemType
        objNew.Punching_Date = obj.Punching_Date
        objNew.MRP = obj.MRP
        objNew.Batch_No = obj.Batch_No
        objNew.MFG_Date = obj.MFG_Date
        objNew.Expiry_Date = obj.Expiry_Date
        objNew.FIFO_Cost = obj.FIFO_Cost
        objNew.LIFO_Cost = obj.LIFO_Cost
        objNew.Avg_Cost = obj.Avg_Cost
        objNew.Posting_Date = obj.Posting_Date
        objNew.PI_Cost = obj.PI_Cost
        objNew.Stock_UOM = obj.Stock_UOM
        objNew.Stock_Qty = obj.Stock_Qty
        objNew.Item_Status = obj.Item_Status
        objNew.Assmbly_Status = obj.Assmbly_Status
        objNew.FAT_Per = obj.FAT_Per
        objNew.SNF_Per = obj.SNF_Per
        objNew.FAT_KG = obj.FAT_KG
        objNew.SNF_KG = obj.SNF_KG
        objNew.main_location = obj.main_location
        objNew.IS_CONSUMPTION = obj.IS_CONSUMPTION
        objNew.Cust_Code = obj.Cust_Code
        objNew.Cust_Name = obj.Cust_Name
        objNew.Vendor_Code = obj.Vendor_Code
        objNew.Vendor_Name = obj.Vendor_Name
        objNew.Other_Location_Code = obj.Other_Location_Code
        objNew.Other_Location_Desc = obj.Other_Location_Desc
        objNew.Fat_Rate = obj.Fat_Rate
        objNew.SNF_Rate = obj.SNF_Rate
        objNew.Fat_Amt = obj.Fat_Amt
        objNew.SNF_Amt = obj.SNF_Amt
        objNew.Std_Qty = obj.Std_Qty
        objNew.Inventory_DrAcc = obj.Inventory_DrAcc
        objNew.Inventory_CrAcc = obj.Inventory_CrAcc
        objNew.Is_Scheme_Item = obj.Is_Scheme_Item
        objNew.Custom_UOM = obj.Custom_UOM
        objNew.Custom_Coversion_Factor = obj.Custom_Coversion_Factor
        objNew.DonNotCalculateAvgFATSNFCost = obj.DonNotCalculateAvgFATSNFCost
        Return objNew
    End Function

    Private Shared Function GetDataWithBatch(ByVal TransType As String, ByVal DocNo As String, ByVal DocDate As DateTime, ByVal arr As List(Of clsInventoryMovementNew), ByVal trans As SqlTransaction)
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isBatchApplyOnInventoryMovement, clsFixedParameterCode.isBatchApplyOnInventoryMovement, trans)) = 0 OrElse clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal OrElse clsCommon.CompairString(TransType, "SRN") = CompairStringResult.Equal Then
            Return arr
        End If
        Dim arrReturn As New List(Of clsInventoryMovementNew)
        Dim arrItemDone As New List(Of String)
        For Each obj As clsInventoryMovementNew In arr
            If obj.Qty = 0 Then
                Continue For
            End If

            If clsCommon.CompairString(obj.ItemType, "FM") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.ItemType, "FT") = CompairStringResult.Equal Then
                Dim strRI As String = "1*"
                If clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                    strRI = "-1*"
                End If
                Dim strLOType As String = ""
                If clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                    strLOType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Type from TSPL_TRANSFER_HEAD where Transfer_No='" + DocNo + "'", trans))
                    If clsCommon.CompairString(strLOType, "LI") = CompairStringResult.Equal Then
                        strRI = "-1*"
                    End If
                End If


                Dim convFact As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans)
                Dim dblMRP As Double = obj.MRP * convFact
                Dim qry As String = "select Stock_Qty*Conversion_Factor as Stock_Qty,Batch_No,MFG_Date,Expiry_Date from("
                qry += " select sum(" + strRI + "Stock_Qty * case when inout='I' then 1 else case when inout='O' then -1 else 0 end end) as Stock_Qty,Batch_No,MAX(MFG_Date) as MFG_Date,MAX(Expiry_Date) as Expiry_Date  from("
                qry += " select  Stock_Qty,inout,Batch_No,MFG_Date,Expiry_Date  "
                qry += " from TSPL_INVENTORY_MOVEMENT_new "
                qry += " where TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + obj.Item_Code + "' and (Qty*MRP/Stock_Qty)=" + clsCommon.myCstr(dblMRP) + " and Stock_Qty<>0 "
                If clsCommon.CompairString(TransType, "Transfer") = CompairStringResult.Equal Then
                    If clsCommon.CompairString(strLOType, "LI") = CompairStringResult.Equal Then
                        qry += " and TSPL_INVENTORY_MOVEMENT_new.InOut='O' and Source_Doc_No in (select top 1 Load_Out_No from TSPL_TRANSFER_HEAD where Transfer_No='" + DocNo + "')"
                    ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                        qry += " and Source_Doc_No='" + DocNo + "'"
                    Else
                        qry += " and TSPL_INVENTORY_MOVEMENT_new.Location_Code='" + obj.Location_Code + "' "
                    End If
                ElseIf clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal Then
                    Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Shipment_No from TSPL_SHIPMENT_MASTER where Invoice_No in ( select Invoice_No from TSPL_SALE_RETURN_HEAD where Sale_Return_No='" + DocNo + "')", trans))
                    If clsCommon.myLen(strShipmentNo) > 0 Then
                        qry += " and Source_Doc_No in ('" + strShipmentNo + "') "
                    Else
                        qry += " and Expiry_Date >='" + clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy") + "'"
                    End If
                ElseIf clsCommon.CompairString(TransType, "Purchase Return") = CompairStringResult.Equal Then
                    Dim strSRNNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Against_SRN  from TSPL_PR_HEAD where PR_No='" + DocNo + "'", trans))
                    If clsCommon.myLen(strSRNNo) > 0 Then
                        qry += " and Source_Doc_No in ('" + strSRNNo + "') "
                    End If
                Else ''For WareHouse and rest all
                    qry += " and TSPL_INVENTORY_MOVEMENT_new.Location_Code='" + obj.Location_Code + "' "
                End If

                If arrItemDone.Contains(obj.Item_Code.Trim()) Then
                    For Each objForBatch As clsInventoryMovementNew In arrReturn
                        If clsCommon.CompairString(objForBatch.Item_Code, obj.Item_Code) = CompairStringResult.Equal Then
                            Dim innerconvFact As Double = clsItemMaster.GetConvertionFactor(objForBatch.Item_Code, objForBatch.UOM, trans)
                            Dim dblinnerMRP As Double = objForBatch.MRP * convFact
                            If dblinnerMRP = dblMRP Then
                                qry += " union all "
                                qry += " select " + clsCommon.myCstr(objForBatch.Qty / innerconvFact) + " as Stock_Qty,'O' as inout,'" + objForBatch.Batch_No + "' as Batch_No,null as MFG_Date,null as Expiry_Date "
                            End If
                        End If
                    Next
                Else
                    arrItemDone.Add(obj.Item_Code.Trim())
                End If
                qry += " )xxx group by Batch_No having sum(" + strRI + " Stock_Qty * case when inout='I' then 1 else case when inout='O' then -1 else 0 end end)<>0"
                qry += " )xxxx"
                qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code='" + obj.Item_Code + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + obj.UOM + "'"
                qry += " order by Expiry_Date "

                Dim qtyToApply As Double = obj.Qty
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim objToInsert As clsInventoryMovementNew = DeepCopyObject(obj)
                        objToInsert.Batch_No = clsCommon.myCstr(dr("Batch_No"))
                        objToInsert.MFG_Date = clsCommon.myCDate(dr("MFG_Date"))
                        objToInsert.Expiry_Date = clsCommon.myCDate(dr("Expiry_Date"))
                        If clsCommon.myCdbl(dr("Stock_Qty")) > qtyToApply Then
                            objToInsert.Qty = qtyToApply
                            qtyToApply -= qtyToApply
                        Else
                            objToInsert.Qty = clsCommon.myCdbl(dr("Stock_Qty"))
                            qtyToApply -= clsCommon.myCdbl(dr("Stock_Qty"))
                        End If
                        arrReturn.Add(objToInsert)
                        If qtyToApply = 0 Then
                            Exit For
                        End If
                    Next
                End If
                If qtyToApply > 0 Then
                    Throw New Exception("Item Qty not available for item" + obj.Item_Code)
                End If
            Else
                arrReturn.Add(obj)
            End If
        Next
        Return arrReturn
    End Function

    Public Shared Function SaveData(ByVal TransType As String, ByVal DocNo As String, ByVal DocDate As DateTime, ByVal EntryDate As String, ByVal ArrInvMov As List(Of clsInventoryMovementNew), ByVal trans As SqlTransaction) As Boolean
        If objCommonVar.StopInventoryNew Then
            Return True
        End If

        Dim LineNo As Integer = 1
        Dim arr As List(Of clsInventoryMovementNew) = GetDataWithBatch(TransType, DocNo, DocDate, ArrInvMov, trans)
        If (arr IsNot Nothing AndAlso arr.Count > 0) Then
            If clsInventorySourceCode.CheckNewEntry(TransType, trans) Then
                If Not clsCommon.CompairString(TransType, "TempDispChallan") = CompairStringResult.Equal Then
                    Throw New Exception("Please make Inventory Source code " + TransType)
                End If
            End If
            For Each obj As clsInventoryMovementNew In arr
                If clsCommon.myLen(obj.Item_Code) <= 0 Then
                    Continue For
                End If
                Dim coll As New Hashtable()
                Dim dtpostingDate As DateTime = clsCommon.GETSERVERDATE(trans)

                clsCommon.AddColumnsForChange(coll, "Trans_Type", TransType)
                clsCommon.AddColumnsForChange(coll, "InOut", obj.InOut)
                clsCommon.AddColumnsForChange(coll, "Location_Code", obj.Location_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
                obj.Qty = Math.Round(obj.Qty, 3, MidpointRounding.AwayFromZero)
                clsCommon.AddColumnsForChange(coll, "Qty", obj.Qty)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Source_Doc_No", DocNo)
                obj.Source_Doc_Date = clsCommon.GetPrintDate(DocDate, "dd/MM/yyyy")
                obj.Posting_Date = clsCommon.GetPrintDate(DocDate, "dd/MM/yyyy")
                clsCommon.AddColumnsForChange(coll, "Source_Doc_Date", clsCommon.GetPrintDate(DocDate, "dd/MM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Entry_Date", EntryDate)

                clsCommon.AddColumnsForChange(coll, "Rec_Cost", obj.Rec_Cost)
                clsCommon.AddColumnsForChange(coll, "Add_Cost", obj.Add_Cost)
                clsCommon.AddColumnsForChange(coll, "Net_Cost", obj.Net_Cost)
                clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "ItemType", obj.ItemType)
                clsCommon.AddColumnsForChange(coll, "MRP", obj.MRP)
                clsCommon.AddColumnsForChange(coll, "Batch_No", obj.Batch_No)
                If clsCommon.CompairString(TransType, "OUT-PUT") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "Punching_Date", clsCommon.GetPrintDate(obj.Punching_Date, "dd/MMM/yyyy hh:mm tt"))
                Else
                    clsCommon.AddColumnsForChange(coll, "Punching_Date", clsCommon.GetPrintDate(DocDate, "dd/MMM/yyyy hh:mm tt"))
                End If

                clsCommon.AddColumnsForChange(coll, "fat_per", obj.FAT_Per)
                clsCommon.AddColumnsForChange(coll, "snf_per", obj.SNF_Per)
                clsCommon.AddColumnsForChange(coll, "fat_kg", obj.FAT_KG)
                clsCommon.AddColumnsForChange(coll, "snf_kg", obj.SNF_KG)
                clsCommon.AddColumnsForChange(coll, "Std_Qty", clsInventoryMovementNew.GetStdQty(trans, obj.FAT_KG, obj.SNF_KG, DocDate))
                If obj.MFG_Date IsNot Nothing AndAlso obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "MFG_Date", clsCommon.GetPrintDate(obj.MFG_Date, "dd/MMM/yyyy"))
                End If

                If obj.Expiry_Date IsNot Nothing AndAlso obj.MFG_Date.HasValue Then
                    clsCommon.AddColumnsForChange(coll, "Expiry_Date", clsCommon.GetPrintDate(obj.Expiry_Date, "dd/MMM/yyyy"))
                End If
                obj.Stock_UOM = clsItemMaster.GetStockUnit(obj.Item_Code, trans)

                If obj.CustomCoversionCLR > 0 Then
                    obj.Custom_UOM = clsItemMaster.GetCustomConversionUOM(obj.Item_Code, trans)
                    If clsCommon.myLen(obj.Custom_UOM) > 0 Then
                        obj.Custom_Coversion_Factor = 1 + (obj.CustomCoversionCLR / 1000)
                        If clsCommon.CompairString(obj.Stock_UOM, obj.UOM) = CompairStringResult.Equal Then
                            obj.Stock_Qty = Math.Round(obj.Qty, 3, MidpointRounding.AwayFromZero)
                        Else
                            obj.Stock_Qty = Math.Round(obj.Qty * obj.Custom_Coversion_Factor, 3, MidpointRounding.AwayFromZero)
                        End If
                        clsCommon.AddColumnsForChange(coll, "Custom_UOM", obj.Custom_UOM)
                        clsCommon.AddColumnsForChange(coll, "Custom_Coversion_Factor", obj.Custom_Coversion_Factor)
                    Else
                        obj.Stock_Qty = Math.Round(obj.Qty * clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans), 3, MidpointRounding.AwayFromZero)
                    End If
                Else
                    obj.Stock_Qty = Math.Round(obj.Qty * clsItemMaster.GetConvertionFactor(obj.Item_Code, obj.UOM, trans), 3, MidpointRounding.AwayFromZero)
                End If

                '' changes  done by richa against job work outward case because in this case main location and sub location will be same
                Dim strCheckJobWorkLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select location_code from TSPL_LOCATION_MASTER where location_code='" & obj.Location_Code & "' and is_Jobwork=1 and isnull(is_sub_location,'')='Y' and isnull(Location_Type,'')='Physical' ", trans))
                Dim Main_Location As String = clsLocation.GetMainLocationMilk(obj.Location_Code, trans)
                If clsCommon.myLen(strCheckJobWorkLocation) > 0 Then
                    Main_Location = obj.main_location
                End If
                ''-------------------
                clsCommon.AddColumnsForChange(coll, "Stock_Qty", obj.Stock_Qty)
                clsCommon.AddColumnsForChange(coll, "Stock_UOM", obj.Stock_UOM)
                clsCommon.AddColumnsForChange(coll, "main_location", Main_Location)
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(dtpostingDate, "dd/MMM/yyyy hh:mm tt"))

                Dim strRefDoc As String = Nothing
                If clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal Then
                    strRefDoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Invoice_Code from TSPL_SD_SALE_RETURN_DETAIL where DOCUMENT_CODE='" & DocNo & "'", trans))
                End If
                If Not obj.CalculateAvgCost Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", obj.FIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", obj.LIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                ElseIf clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Basic_Cost * IIf(obj.Qty = 0, 1, obj.Qty))
                ElseIf clsCommon.CompairString(TransType, "Sale Return") = CompairStringResult.Equal AndAlso clsCommon.myLen(strRefDoc) <= 0 Then
                    Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT_NEW"))
                ElseIf clsCommon.CompairString(TransType, "DispChallan-RET") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", obj.FIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", obj.LIFO_Cost)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                ElseIf clsCommon.CompairString(TransType, "DisCanSale") = CompairStringResult.Equal Then
                    Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal AndAlso clsCommon.CompairString(TransType, "SI-MT") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                ElseIf clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Then
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", obj.Basic_Cost * obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", obj.Basic_Cost * obj.Qty)
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Basic_Cost * obj.Qty)
                Else
                    Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                    clsCommon.AddColumnsForChange(coll, "FIFO_Cost", 0)
                    clsCommon.AddColumnsForChange(coll, "LIFO_Cost", 0)
                    If clsCommon.CompairString(TransType, "DispChallan") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.PostTankerDispatchWithZeroAvgCost, trans)) = 1 Then
                        clsCommon.AddColumnsForChange(coll, "Avg_Cost", 0)
                    Else
                        If clsCommon.CompairString(TransType, "DispatchBS") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(TransType, "SI-MT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(TransType, "OUT-PUT") <> CompairStringResult.Equal Then
                            If (obj.Fat_Amt + obj.SNF_Amt) > 0 Then
                                clsCommon.AddColumnsForChange(coll, "Avg_Cost", (obj.Fat_Amt + obj.SNF_Amt))
                            Else
                                clsCommon.AddColumnsForChange(coll, "Avg_Cost", clsInventoryMovement.GetCost(EnumCostingMethod.Averege, obj.Item_Code, obj.Location_Code, obj.Stock_Qty, DocDate, dtpostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT_NEW"))
                            End If
                        End If
                    End If
                End If
                '' update fat,snf cost colums ''RICHA CHANGES DONE BY ME TO CALCUALTE COST ON AVG_BASES FOR OVERALL TRANSACTION INTO SYSTEM
                If Not (clsCommon.CompairString(obj.InOut, "I") = CompairStringResult.Equal Or obj.DonNotCalculateAvgFATSNFCost Or clsCommon.CompairString(TransType, "IC-AD") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "DispChallan") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "DispatchBSTrade") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PP_ISSUE") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PP_STDN") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PRD_STG_PROC") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "PROD_ENTRY") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "BulkSRN") = CompairStringResult.Equal Or clsCommon.CompairString(TransType, "BulkSRNRet") = CompairStringResult.Equal) Then
                    'Sanjay, Bulk Milk Purchase Return Fat Amt and SNF Amt calculation
                    If clsCommon.CompairString(TransType, "M-PURRETURN") = CompairStringResult.Equal Then
                        obj.Fat_Amt = Math.Round(obj.FAT_KG * obj.Fat_Rate, 2)
                        obj.SNF_Amt = Math.Round(obj.SNF_KG * obj.SNF_Rate, 2)
                    Else
                        Dim isApplyCostOnPostDate As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCostingOnPostedDate, clsFixedParameterCode.ApplyCostingOnPostedDate, trans)) = 1, True, False)
                        Dim objCost As New MIlkComponentType
                        objCost = clsInventoryMovementNew.GetAvgCost("MI", obj.Item_Code, obj.Location_Code, obj.Qty, obj.UOM, obj.FAT_KG, obj.SNF_KG, obj.Source_Doc_Date, obj.Source_Doc_Date, isApplyCostOnPostDate, trans)
                        obj.Fat_Rate = objCost.FAT_Cost / IIf(obj.FAT_KG <= 0, 1, obj.FAT_KG)
                        obj.SNF_Rate = objCost.SNF_Cost / IIf(obj.SNF_KG <= 0, 1, obj.SNF_KG)
                        obj.Fat_Amt = objCost.FAT_Cost
                        obj.SNF_Amt = objCost.SNF_Cost
                    End If
                End If
                ''richa 26 Sep,2018

                If obj.CalculateAvgCost AndAlso (clsCommon.CompairString(TransType, "DisCanSale") = CompairStringResult.Equal OrElse clsCommon.CompairString(TransType, "DispatchBS") = CompairStringResult.Equal OrElse clsCommon.CompairString(TransType, "OUT-PUT") = CompairStringResult.Equal) Then
                    obj.Avg_Cost = obj.Fat_Amt + obj.SNF_Amt
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                    clsCommon.AddColumnsForChange(coll, "Basic_Cost", obj.Avg_Cost / obj.Qty)
                Else
                    clsCommon.AddColumnsForChange(coll, "Basic_Cost", obj.Basic_Cost)
                End If

                ''------------
                If clsCommon.CompairString(TransType, "SI-MT") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.InOut, "O") = CompairStringResult.Equal Then
                    obj.Avg_Cost = obj.Fat_Amt + obj.SNF_Amt
                    clsCommon.AddColumnsForChange(coll, "Avg_Cost", obj.Avg_Cost)
                End If

                clsCommon.AddColumnsForChange(coll, "Fat_Rate", obj.Fat_Rate)
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)
                clsCommon.AddColumnsForChange(coll, "Fat_Amt", obj.Fat_Amt)
                clsCommon.AddColumnsForChange(coll, "SNF_Amt", obj.SNF_Amt)

                clsCommon.AddColumnsForChange(coll, "item_status", obj.itemstatus)
                clsCommon.AddColumnsForChange(coll, "Assmbly_Status", obj.itemtypeinventry)
                clsCommon.AddColumnsForChange(coll, "IS_CONSUMPTION", obj.IS_CONSUMPTION)

                clsCommon.AddColumnsForChange(coll, "Cust_Code", obj.Cust_Code, True)
                clsCommon.AddColumnsForChange(coll, "Cust_Name", obj.Cust_Name)
                clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
                clsCommon.AddColumnsForChange(coll, "Vendor_Name", obj.Vendor_Name)
                clsCommon.AddColumnsForChange(coll, "Other_Location_Code", obj.Other_Location_Code, True)
                clsCommon.AddColumnsForChange(coll, "Other_Location_Desc", obj.Other_Location_Desc)
                '' update Sync Satatus
                clsCommon.AddColumnsForChange(coll, "SYNC_STATUS", 0)
                clsCommon.AddColumnsForChange(coll, "Is_Scheme_Item", obj.Is_Scheme_Item, True)
                clsCommon.AddColumnsForChange(coll, "Inventory_CrAcc", obj.Inventory_CrAcc, True)
                clsCommon.AddColumnsForChange(coll, "Inventory_DrAcc", obj.Inventory_DrAcc, True)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_INVENTORY_MOVEMENT_new", OMInsertOrUpdate.Insert, "", trans)
                If obj.Ref_Line_No > 0 Then ''for special condition,where on screen line no is not refreshed ,so pass line no in obj,otherwise incremented default line no applied.
                    LineNo = obj.Ref_Line_No
                End If
                ''richa agarwal 25 June,2019 batch item work for milk type item of batch wise
                If obj.Qty <> 0 Then ''if qty=0 from store adjustment and item is of batch type ,then also no need of below method,as it sends error message.
                    clsBatchInventoryNew.PostData(TransType, DocNo, obj.Item_Code, obj.InOut, LineNo, trans, False, (obj.IS_CONSUMPTION = 1))
                End If
                LineNo += 1
            Next
        End If
        Return True
    End Function

    ''richa 7 march,2019 GKD/28/02/19-000177 function to get fat and snf kg with committed and back date entry data 
    Public Shared Function GetbalanceQuery_FatAndSnfKG(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String)
        Dim strCondition As String = ""
        Dim strCondition1 As String = ""
        Dim strCondition2 As String = ""
        Dim strCondition3 As String = String.Empty
        Dim strCondition4 As String = String.Empty
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition1 = "  and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
        Else
            strCondition1 = " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strLocation + "' "
        End If

        ''richa agarwal changes done on  29 march 2016
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition2 = "  and TSPL_ADJUSTMENT_HEADER.MainLocationCode='" + strLocation + "' and TSPL_ADJUSTMENT_HEADER.Loc_Code='" + strSubLocation + "'"
        Else
            strCondition2 = " and TSPL_ADJUSTMENT_HEADER.MainLocationCode='" + strLocation + "' "
        End If
        ''---------

        ''richa agarwal changes done on 17 Apr,2018 for TSPL_MCC_Dispatch_Challan_Stock_Detail
        If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            strCondition3 = " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strLocation + "' and TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strSubLocation + "'"
        ElseIf clsCommon.myLen(strSubLocation) > 0 Then
            strCondition3 = " and (TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strSubLocation + "' or TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strSubLocation + "') "
        ElseIf clsCommon.myLen(strLocation) > 0 Then
            strCondition3 = " and (TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code='" + strLocation + "' or (TSPL_MCC_Dispatch_Challan_Stock_Detail.Main_Location='" + strLocation + "' and TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code ='')) "
        End If

        ''richa agarwal changes done on 17 Apr,2018 for TSPL_Dispatch_BulkSale
        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition4 = "  and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strSubLocation + "'"
        Else
            strCondition4 = " and TSPL_Dispatch_BulkSale.Location_Code='" + strLocation + "' "
        End If

        ''---------

        If clsCommon.myLen(strSubLocation) > 0 AndAlso clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "'"
        ElseIf clsCommon.myLen(strSubLocation) > 0 Then
            strCondition = " and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strSubLocation + "' or TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strSubLocation + "') "
        ElseIf clsCommon.myLen(strLocation) > 0 Then
            strCondition = " and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "' or (TSPL_INVENTORY_MOVEMENT_NEW.Main_Location='" + strLocation + "' and TSPL_INVENTORY_MOVEMENT_NEW.Location_Code ='')) "
        End If

        Dim qry As String = "select (((case when Minimum_Bal.Minimum_Balance is null then  xx.Qty else (case when Minimum_Bal.Minimum_Balance>xx.Qty then xx.Qty else Minimum_Bal.Minimum_Balance end)  end))/FinalUOM.Conversion_Factor) as Qty," + Environment.NewLine &
" (((case when Minimum_Bal.Minimum_Balance_FATkg  is null then  xx.fat_kg  else (case when Minimum_Bal.Minimum_Balance_FATkg >xx.fat_kg  then xx.fat_kg else Minimum_Bal.Minimum_Balance_FATkg  end)  end))/FinalUOM.Conversion_Factor) as FAT_KG," + Environment.NewLine &
" (((case when Minimum_Bal.Minimum_Balance_SNFKG is null then  xx.Snf_kg  else (case when Minimum_Bal.Minimum_Balance_SNFKG>xx.Snf_kg then xx.Snf_kg else Minimum_Bal.Minimum_Balance_SNFKG end)  end))/FinalUOM.Conversion_Factor) as SNF_KG from (" + Environment.NewLine
        qry += " select xxx.ICode,xxx.Location,SUM(qty * TSPL_ITEM_UOM_DETAIL.Conversion_Factor*RI) as Qty,SUM(fat_kg * TSPL_ITEM_UOM_DETAIL.Conversion_Factor*RI) as fat_kg,SUM(Snf_kg * TSPL_ITEM_UOM_DETAIL.Conversion_Factor*RI) as Snf_kg from( " + Environment.NewLine
        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM ,sum(fat_kg*RI) as fat_kg,sum(Snf_kg*RI) as Snf_kg from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
        qry += " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,TSPL_INVENTORY_MOVEMENT_NEW.Location_Code AS Location_Code, "
        qry += " TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty as Qty   ,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM as UOMNew ,isnull(TSPL_INVENTORY_MOVEMENT_NEW.fat_kg,0) as fat_kg,isnull(TSPL_INVENTORY_MOVEMENT_NEW.Snf_kg,0) as Snf_kg "
        qry += " from TSPL_INVENTORY_MOVEMENT_NEW "
        qry += " where  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + strICode + "' " + strCondition + " "
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance"
        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance,min(Closing_Balance_FATkg) as Minimum_Balance_FATkg,min(Closing_Balance_SNFKG ) as Minimum_Balance_SNFKG from (" &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance, " &
                        " sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Fat_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_FATkg,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*SNF_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_SNFKG " &
                        " from TSPL_INVENTORY_MOVEMENT where Item_Code='" & strICode & "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code " &
                        " union all " &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " ,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Fat_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_FATkg,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*SNF_KG )) over(order by cast(Punching_Date as date)) as Closing_Balance_SNFKG " &
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & strICode & "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' " &
                        " group by Item_Code,Location_Code "
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If

        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,case when ISNULL(TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No,'')<>'' then TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No else TSPL_Dispatch_BulkSale.Location_Code  end  as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom " &
            " ,isnull(TSPL_Dispatch_Detail_BulkSale.fat_kg,0) as fat_kg,isnull(TSPL_Dispatch_Detail_BulkSale.SNF_KG,0) as SNF_KG  " &
        " from TSPL_Dispatch_Detail_BulkSale " &
        " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No" &
        " LEFT OUTER JOIN TSPL_Quality_Check_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_No" &
        " LEFT OUTER JOIN TSPL_LOADING_TANKER_DETAIL_BULKSALE ON TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No" &
        " where TSPL_Dispatch_BulkSale.Posted=0 and TSPL_Dispatch_Detail_BulkSale.Item_Code='" + strICode + "'  " + strCondition4 + " and TSPL_Dispatch_Detail_BulkSale.Qty<>0 " &
        " and TSPL_Dispatch_Detail_BulkSale.Document_No not in ('" + strDocumentNo + "')"

        ''can sale
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_CANSALE_DISPATCH_DETAIL.ItemCode as ICode,TSPL_CANSALE_DISPATCH_HEAD.Location_Code as Locaion,TSPL_CANSALE_DISPATCH_DETAIL.Qty,-1 as RI,TSPL_CANSALE_DISPATCH_DETAIL.UOM AS Uom " &
            " ,isnull(TSPL_CANSALE_DISPATCH_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_CANSALE_DISPATCH_DETAIL.SNF_KG,0) as SNF_KG " &
        " from TSPL_CANSALE_DISPATCH_DETAIL " &
        " left outer join TSPL_CANSALE_DISPATCH_HEAD on TSPL_CANSALE_DISPATCH_HEAD.Document_No=TSPL_CANSALE_DISPATCH_DETAIL.Document_No" &
        " where TSPL_CANSALE_DISPATCH_HEAD.Posted=0 and TSPL_CANSALE_DISPATCH_DETAIL.ItemCode='" + strICode + "' and TSPL_CANSALE_DISPATCH_HEAD.Location_Code='" + strLocation + "' and TSPL_CANSALE_DISPATCH_DETAIL.Qty<>0  " &
        " and TSPL_CANSALE_DISPATCH_DETAIL.Document_No not in ('" + strDocumentNo + "')"

        ''SILO MILK TRANSFER
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code  as ICode,TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code as Locaion,TSPL_SILO_MILK_TRANSFER_DETAIL.Qty,-1 as RI,TSPL_SILO_MILK_TRANSFER_DETAIL.UOM AS Uom " &
            " ,isnull(TSPL_SILO_MILK_TRANSFER_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_SILO_MILK_TRANSFER_DETAIL.SNF_KG,0) as SNF_KG  " &
        " from TSPL_SILO_MILK_TRANSFER_DETAIL " &
        " left outer join TSPL_SILO_MILK_TRANSFER_HEAD on TSPL_SILO_MILK_TRANSFER_HEAD.Document_Code =TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code " &
        " where TSPL_SILO_MILK_TRANSFER_HEAD.Posted=0 and TSPL_SILO_MILK_TRANSFER_DETAIL.Item_Code='" + strICode + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Qty<>0  " &
        " and TSPL_SILO_MILK_TRANSFER_DETAIL.Document_Code not in ('" + strDocumentNo + "')"

        If clsCommon.myLen(strSubLocation) > 0 Then
            strCondition4 = "  and TSPL_SILO_MILK_TRANSFER_HEAD.MainLocation_Code='" + strLocation + "' and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + strSubLocation + "'"
        Else
            strCondition4 = " and TSPL_SILO_MILK_TRANSFER_DETAIL.Silo_Code='" + strLocation + "' "
        End If

        qry += " union all " + Environment.NewLine &
            " select TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code as ICode,TSPL_Dispatch_BulkSale_Trade.Location_Code as Locaion,  " + Environment.NewLine &
            " TSPL_Dispatch_Detail_BulkSale_Trade.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale_Trade.Unit_code AS Uom,isnull(TSPL_Dispatch_Detail_BulkSale_Trade.fat_kg,0) as fat_kg,isnull(TSPL_Dispatch_Detail_BulkSale_Trade.SNF_KG,0) as SNF_KG   from TSPL_Dispatch_Detail_BulkSale_Trade  " + Environment.NewLine &
            " left outer join TSPL_Dispatch_BulkSale_Trade  on TSPL_Dispatch_BulkSale_Trade.Document_No=TSPL_Dispatch_Detail_BulkSale_Trade.Document_No  " + Environment.NewLine &
            " where TSPL_Dispatch_BulkSale_Trade.Posted=0 and TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code='" + strICode + "' and TSPL_Dispatch_BulkSale_Trade.Location_Code='" + strLocation + "'" + Environment.NewLine &
            " and TSPL_Dispatch_Detail_BulkSale_Trade.Qty<>0   and TSPL_Dispatch_Detail_BulkSale_Trade.Document_No not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Locaion,TSPL_PP_ISSUE_ITEM_DETAIL.Qty,-1 as RI,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_code AS Uom " &
        " ,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_PP_ISSUE_ITEM_DETAIL.SNF_KG,0) as SNF_KG " + Environment.NewLine &
        " from TSPL_PP_ISSUE_ITEM_DETAIL " + Environment.NewLine &
         " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code" + Environment.NewLine &
         " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code " + Environment.NewLine &
        " where TSPL_PP_ISSUE_HEAD.Is_post=0 and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + strICode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" + strLocation + "' and TSPL_PP_ISSUE_ITEM_DETAIL.Qty<>0  " + Environment.NewLine &
         " and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine
        qry += " select Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI," + Environment.NewLine &
        " BUILD_ITEM_UNIT_CODE as UnitCode ,isnull(TSPL_PROD_ASSEMBLIES.fat_kg,0) as fat_kg,isnull(TSPL_PROD_ASSEMBLIES.SNF_KG,0) as SNF_KG  from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.TRANSACTION_TYPE='Disassembly' and TSPL_PROD_ASSEMBLIES.POSTED=0 and  TSPL_PROD_ASSEMBLIES.Main_Item_Code='" + strICode + "'  and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strSubLocation & "'"
        Else
            qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If

        qry += " union all  "

        qry += " select  TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location," + Environment.NewLine &
         " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY as Qty," + Environment.NewLine &
         " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI," + Environment.NewLine &
        " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode,isnull(TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.fat_kg,0) as fat_kg,isnull(TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.SNF_KG,0) as SNF_KG  from TSPL_PJC_ASSEMBLIES " + Environment.NewLine &
         " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE " + Environment.NewLine &
         " where TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly'  and  TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE='" + strICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strSubLocation & "'"
        Else
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If

        qry += " union all  "

        qry += " select  TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
        qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
        qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode,isnull(TSPL_WRECKAGE_BOOKING.Avail_FAT_KG ,0) as fat_kg,isnull(TSPL_WRECKAGE_BOOKING.Avail_SNF_KG ,0) as SNF_KG  from TSPL_WRECKAGE_ENTRY "
        qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
        qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0 and TSPL_WRECKAGE_BOOKING.ITEM_CODE='" + strICode + "'  and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strSubLocation & "'"
        Else
            qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strLocation & "'"
        End If


        '' added by richa agarwal to include transactions of store adjustment whose trans type is Out and milk type is 1 
        qry += "union all " + Environment.NewLine &
            "  select TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,"
        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += "  TSPL_ADJUSTMENT_HEADER.Loc_Code as Locaion, "
        Else
            qry += " TSPL_ADJUSTMENT_HEADER.MainLocationCode as Locaion, "
        End If
        qry += " TSPL_ADJUSTMENT_DETAIL.Item_Quantity ,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_code AS Uom ,isnull(TSPL_ADJUSTMENT_DETAIL.FAT_KG ,0) as fat_kg,isnull(TSPL_ADJUSTMENT_DETAIL.SNF_KG ,0) as SNF_KG  " &
            " from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_DETAIL.Adjustment_No  " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' " &
            " where TSPL_ADJUSTMENT_HEADER.Posted ='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + strICode + "' " + strCondition2 + " and TSPL_ADJUSTMENT_DETAIL.Item_Quantity <>0 " &
            " and TSPL_ADJUSTMENT_HEADER.Trans_Type  ='Out' and TSPL_ADJUSTMENT_HEADER .IsMilkType =1 and TSPL_ADJUSTMENT_DETAIL.Adjustment_No  not in ('" + strDocumentNo + "') " + Environment.NewLine


        qry += " union all " + Environment.NewLine


        '' save data into detail table to check qty with main and silo location
        qry += "  select  TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code ,  TSPL_MCC_Dispatch_Challan_Stock_Detail.Location_Code AS Location_Code , TSPL_MCC_Dispatch_Challan_Stock_Detail.Qty  as Qty,  -1 as RI, TSPL_MCC_Dispatch_Challan_Stock_Detail.UOM  as UOMNew ,isnull(TSPL_MCC_Dispatch_Challan_Stock_Detail.FAT_KG ,0) as fat_kg,isnull(TSPL_MCC_Dispatch_Challan_Stock_Detail.SNF_KG ,0) as SNF_KG  from TSPL_MCC_Dispatch_Challan_Stock_Detail  where TSPL_MCC_Dispatch_Challan_Stock_Detail.IsPosted=0 and TSPL_MCC_Dispatch_Challan_Stock_Detail.Qty<>0 " &
            " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Item_Code='" + strICode + "'  " + strCondition3 + " and TSPL_MCC_Dispatch_Challan_Stock_Detail.Chalan_No not in ('" + strDocumentNo + "')"

        '' query for add/remove items durng Process production Standardization
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
        qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_FAT_KG ,0) as fat_kg,isnull(TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.AR_SNF_KG ,0) as SNF_KG from TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
        qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + strICode + "' "
        qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Standardization_Code not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_STD_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

        '' query for  Process production Standardization
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code,TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code,"
        qry += " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_Qty,"
        qry += " 1 as RI,"
        qry += " TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.UNIT_CODE,isnull(TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_FAT_KG ,0) as fat_kg,isnull(TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Produced_SNF_KG ,0) as SNF_KG from TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL "
        qry += " inner join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code = TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code "
        qry += " where TSPL_PP_STANDARDIZATION_HEAD.Posted=0 and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Item_Code='" + strICode + "' "
        qry += " and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.Standardization_Code not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_BATCH_ITEM_PRODUCTION_DETAIL.STD_Loaction_Code='" & strLocation & "' "

        '' PRODUCTION CONSUMPTION 
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE," &
              " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE,TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_QTY,-1 as RI," &
              " TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.UNIT_CODE ,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.fat_kg ,0) as fat_kg,isnull(TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.SNF_KG ,0) as SNF_KG from TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL " &
              " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " &
              " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.CONSM_ITEM_CODE='" & strICode & "' " &
              " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "') " &
              " and TSPL_PP_PRODUCTION_CONSUMPTION_DETAIL.LOCATION_CODE='" & strLocation & "'"

        '' query for add/remove items durng Process production STAGE PROCESS
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code,TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_QTY,"
        qry += " (case when TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.ADD_REMOVE_TYPE='Add' then 1 else  -1  end)as RI,"
        qry += " TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.UNIT_CODE,isnull(TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.fat_kg ,0) as fat_kg,isnull(TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.SNF_KG ,0) as SNF_KG from TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL "
        qry += " inner join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE = TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE "
        qry += " where TSPL_PP_STAGE_PROCESS_HEAD.Posted=0 and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Item_Code='" + strICode + "' "
        qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.STAGE_PROCESS_CODE not in ('" + strDocumentNo + "')"
        qry += " and TSPL_PP_SP_ADD_REMOVE_ITEM_DETAIL.Loaction_Code='" & strLocation & "' "

        '' PRODUCTION ENTRY 
        qry += " union all " + Environment.NewLine
        qry += " select TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE," &
               " TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE,TSPL_PP_PRODUCTION_ENTRY_DETAIL.RECEIPT_QTY,1 as RI," &
               " TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE,isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.fat_kg ,0) as fat_kg,isnull(TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG ,0) as SNF_KG from TSPL_PP_PRODUCTION_ENTRY_DETAIL " &
               " inner join TSPL_PP_PRODUCTION_ENTRY on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=TSPL_PP_PRODUCTION_ENTRY.PROD_ENTRY_CODE " &
               " where TSPL_PP_PRODUCTION_ENTRY.POSTED=0 and TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE='" & strICode & "' " &
               " and TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE not in ('" & strDocumentNo & "')" &
               " and TSPL_PP_PRODUCTION_ENTRY.LOCATION_CODE='" & strLocation & "'"

        If clsCommon.myLen(strSubLocation) > 0 Then
            qry += " )xxx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.UOM where xxx.Location ='" & strSubLocation & "' group by ICode,Location"
        Else
            qry += " )xxx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xxx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xxx.UOM group by ICode,Location"
        End If
        qry += " )xx" + Environment.NewLine +
        " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code " + Environment.NewLine +
        " left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + strUOM + "'"
        Return qry
    End Function
    Public Shared Function getBalance_FatAndSnfKG(ByVal strICode As String, ByVal strLocation As String, ByVal strSubLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As DataTable
        Dim qry = GetbalanceQuery_FatAndSnfKG(strICode, strLocation, strSubLocation, strDocumentNo, dtDocumentDate, trans, strUOM)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        'Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)
        '' MIL/07/06/18-000025 richa 
        '' Return BalQty / IIf(dblConvFac <= 0, 1, dblConvFac)
        Return dt
    End Function

    ''------ end of fatAndSnfKG function

    Public Shared Function getCostingMethod(itemCode As String, Optional trans As SqlTransaction = Nothing) As Integer
        Try
            Return clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select costing_method from  tspl_purchase_accounts where purchase_class_code=(select Purchase_Class_Code  from tspl_item_master where Item_Code='" & itemCode & "') ", trans))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function GetCost(ByVal CostMethod As EnumCostingMethod, ByVal strICode As String, ByVal strLocation As String, ByVal dblqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction) As Double
        Dim dblRetCost As Double = 0
        If Not CostMethod = EnumCostingMethod.NA AndAlso dblqty > 0 Then
            Dim strSymbolCost As String = " >= "
            If CostMethod = EnumCostingMethod.LIFO Then
                strSymbolCost = " <= "
            End If

            Dim strDateColumn As String = " Punching_Date "
            Dim strDateForCheck As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt")
            If isApplyCostOnPostDate Then
                strDateColumn = " Posting_Date "
                strDateForCheck = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtPostingDate), "dd/MMM/yyyy hh:mm tt")
            End If

            Dim qry As String
            If CostMethod = EnumCostingMethod.Averege Then
                'qry = "select case when Qty=0 then 0 else abs(Amt/Qty)*" + clsCommon.myCstr(dblqty) + "  end as AvgCost from( select  sum(Amt * RI) as Amt,sum(Qty * RI) as Qty from(" + Environment.NewLine
                'qry += " select Stock_Qty as Qty,( Avg_Cost) as Amt,case when InOut='O' then -1 else 1 end as RI  from TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' " + Environment.NewLine
                'qry += " )xxx )xxxx" + Environment.NewLine
                qry = "select case when (Qty<0 or Amt<0)  then (AvgPostitiveRate*" + clsCommon.myCstr(dblqty) + ") else case when (Qty>0 and Qty>=" + clsCommon.myCstr(dblqty) + ")  then (Amt/Qty)*" + clsCommon.myCstr(dblqty) + " else case when Qty>0 and Qty<" + clsCommon.myCstr(dblqty) + " then (Amt+((" + clsCommon.myCstr(dblqty) + "-Qty)*AvgPostitiveRate)) else 0 end end end as AvgCost from( "
                qry += " select  sum(Amt * RI) as Amt,sum(Qty * RI) as Qty,(select top 1 Avg_Cost/(case when Stock_Qty=0 then 1 else Stock_Qty end)  from TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' and InOut='I' and Avg_Cost>0) as AvgPostitiveRate from(" + Environment.NewLine
                qry += " select Stock_Qty as Qty,( Avg_Cost) as Amt,case when InOut='O' then -1 else 1 end as RI  from TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and  " + strDateColumn + " <= '" + strDateForCheck + "' " + Environment.NewLine
                qry += " )xxx )xxxx" + Environment.NewLine
                dblRetCost = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
            Else
                qry = ";WITH cteStockSum AS ( " + Environment.NewLine
                qry += " SELECT   Item_Code ,SUM(Stock_Qty * CASE WHEN  InOut = 'O' THEN -1 ELSE 1 END) AS TotalStock FROM  TSPL_INVENTORY_MOVEMENT_new where Item_Code='" + strICode + "' and Location_Code='" + strLocation + "' and " + strDateColumn + " <= '" + strDateForCheck + "'  GROUP BY Item_Code)," + Environment.NewLine

                qry += " cteReverseInSum AS (" + Environment.NewLine
                qry += " SELECT  s.Item_Code ,s." + strDateColumn + " as TranDate ,(SELECT SUM(i.Stock_Qty) FROM TSPL_INVENTORY_MOVEMENT_new AS i  WHERE i.Item_Code = s.Item_Code AND i.InOut IN ( 'I' ) and i." + strDateColumn + " <= '" + strDateForCheck + "' and i.Location_Code='" + strLocation + "' AND i." + strDateColumn + " " + strSymbolCost + " s." + strDateColumn + " --for FIFO  >= " + Environment.NewLine
                qry += " ) AS RollingStock ,s.Stock_Qty AS ThisStock FROM TSPL_INVENTORY_MOVEMENT_new AS s WHERE  s.Item_Code='" + strICode + "' and s.Location_Code='" + strLocation + "' and s." + strDateColumn + " <= '" + strDateForCheck + "'  and s.InOut IN ( 'I' ))," + Environment.NewLine

                qry += " cteWithLastTranDate  AS ( " + Environment.NewLine
                qry += " SELECT   w.Item_Code ,w.TotalStock ,LastPartialStock. TranDate ,LastPartialStock.StockToUse ,LastPartialStock.RunningTotal ,w.TotalStock -LastPartialStock.RunningTotal+ LastPartialStock.StockToUse AS UseThisStock FROM cteStockSum AS w" + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP ( 1 )z. TranDate ,z.ThisStock AS StockToUse ,z.RollingStock AS RunningTotal FROM  cteReverseInSum AS z WHERE z.Item_Code = w.Item_Code AND z.RollingStock >= w.TotalStock ORDER BY  z.TranDate " + IIf(CostMethod = EnumCostingMethod.FIFO, "DESC", "") + " --for FIFO DESC" + Environment.NewLine
                qry += " ) AS LastPartialStock" + Environment.NewLine
                qry += " )" + Environment.NewLine

                qry += " select *  from (" + Environment.NewLine
                qry += " SELECT  y.Item_Code ,y.TotalStock AS CurrentItems ,e.Basic_Cost,e." + strDateColumn + " as TranDate,(CASE WHEN e." + strDateColumn + " = y.TranDate THEN y.UseThisStock" + Environment.NewLine
                qry += " ELSE e.Stock_Qty END * Price.Basic_Cost) AS CurrentValue,(CASE WHEN e. " + strDateColumn + "  = y.TranDate THEN y.UseThisStock  ELSE e.Stock_Qty END  ) as BalanceQty FROM cteWithLastTranDate AS y INNER JOIN TSPL_INVENTORY_MOVEMENT_new AS e ON e.Item_Code = y.Item_Code and e." + strDateColumn + " <= '" + strDateForCheck + "' AND e." + strDateColumn + " " + strSymbolCost + " y.TranDate -- for Fifo >=" + Environment.NewLine
                qry += " AND e.InOut IN ('I') and e.Location_Code='" + strLocation + "' " + Environment.NewLine
                qry += " CROSS APPLY ( SELECT TOP ( 1 ) case when Stock_Qty =0 then 0 else  (p.Basic_Cost*p.Qty)/p.Stock_Qty end as Basic_Cost FROM TSPL_INVENTORY_MOVEMENT_new AS p  WHERE p.Item_Code = e.Item_Code " + Environment.NewLine
                qry += " AND p." + strDateColumn + " <= e." + strDateColumn + "  " + Environment.NewLine
                qry += " AND p.InOut = 'I' and p.Location_Code='" + strLocation + "'  ORDER BY p." + strDateColumn + " DESC ) AS Price" + Environment.NewLine
                qry += ")xxx   " + IIf(CostMethod = EnumCostingMethod.FIFO, " order by TranDate ", IIf(CostMethod = EnumCostingMethod.LIFO, "order by TranDate DESC", "")) + " --For Fifo not Desc order" + Environment.NewLine

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim dblbalanceQty As Double = dblqty
                    For Each dr As DataRow In dt.Rows
                        Dim dblCurrQty As Double = clsCommon.myCdbl(dr("BalanceQty"))
                        If dblbalanceQty >= dblCurrQty Then
                            dblRetCost += clsCommon.myCdbl(dr("CurrentValue"))
                        Else
                            dblRetCost += (clsCommon.myCdbl(dr("CurrentValue")) * dblbalanceQty) / dblCurrQty
                        End If
                        dblbalanceQty -= dblCurrQty
                        If dblbalanceQty <= 0 Then
                            dblRetCost = dblRetCost
                            Exit For
                        End If
                    Next
                    If dblbalanceQty > 0 Then
                        'Throw New Exception("Quantity Not available for " + strICode)
                    End If
                End If
            End If
        End If
        Return dblRetCost
    End Function

    Public Shared Function GetAvgCost(ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost("", Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function

    Public Shared Function GetAvgCost(ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(False, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function

    Public Shared Function GetAvgCost(ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(False, IsDateWithTime, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function
    Public Shared Function GetAvgCost(ByVal IsRejectOnly As Boolean, ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(True, IsRejectOnly, IsDateWithTime, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function
    Public Shared Function GetAvgCost(ByVal IsCheckInMainLocation As Boolean, ByVal IsRejectOnly As Boolean, ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Return GetAvgCost(False, IsCheckInMainLocation, IsRejectOnly, IsDateWithTime, ExtrWhrl, Product_Type, Item_Code, strLocation, dblQty, Unit_Code, dblFATqty, dblSNFqty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, Current_Doc_Code, Trans_Id)
    End Function

    Public Shared Function GetAvgCost(ByVal For10decimalPlaces As Boolean, ByVal IsCheckInMainLocation As Boolean, ByVal IsRejectOnly As Boolean, ByVal IsDateWithTime As Boolean, ByVal ExtrWhrl As String, ByVal Product_Type As String, ByVal Item_Code As String, ByVal strLocation As String, ByVal dblQty As Decimal, ByVal Unit_Code As String, ByVal dblFATqty As Double, ByVal dblSNFqty As Double, ByVal dtDocumentDate As DateTime, ByVal dtPostingDate As DateTime, ByVal isApplyCostOnPostDate As Boolean, ByVal trans As SqlTransaction, Optional ByVal Current_Doc_Code As String = "", Optional ByVal Trans_Id As Integer = 0) As MIlkComponentType
        Dim obj As New MIlkComponentType
        Try
            Dim settPickProductCostFromItemUOMDetail As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickProductCostFromItemUOMDetail, clsFixedParameterCode.PickProductCostFromItemUOMDetail, trans)) > 0)
            Dim dblRetCost As Double = 0
            Dim strSymbolCost As String = " >= "

            Dim strDateColumn As String = " Punching_Date "
            Dim strDateForCheck As String = clsCommon.GetPrintDate(IIf(IsDateWithTime, dtDocumentDate, clsCommon.GetDateWithEndTime(dtDocumentDate)), "dd/MMM/yyyy hh:mm tt")
            If isApplyCostOnPostDate Then
                strDateColumn = " Posting_Date "
                strDateForCheck = clsCommon.GetPrintDate(IIf(IsDateWithTime, dtPostingDate, clsCommon.GetDateWithEndTime(dtPostingDate)), "dd/MMM/yyyy hh:mm tt")
            End If
            Dim qry As String = ""
            Dim cond_TransId As String = ""
            If Trans_Id > 0 Then
                cond_TransId = " and  TSPL_INVENTORY_MOVEMENT_new.Trans_id<'" & Trans_Id & "'"
            End If
            If clsCommon.myLen(Product_Type) <= 0 Then
                Product_Type = clsItemMaster.GetItemProductType(Item_Code, trans)
            End If

            If clsCommon.CompairString(Product_Type, "MI") = CompairStringResult.Equal Then
                qry = " select (case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or Fat_Amt<=0 or Fat_KG<=0) then Last_Fat_Rate else Fat_Amt/Fat_KG end)*" + clsCommon.myCstr(dblFATqty) + " as Avg_Fat_Cost" + Environment.NewLine +
                    ",(case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or SNF_Amt<=0 or SNF_KG<=0) then Last_SNF_Rate else SNF_Amt/SNF_KG end)*" + clsCommon.myCstr(dblSNFqty) + " as Avg_SNF_Cost " +
                    ",(case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or SNF_Amt<=0 or SNF_KG<=0 or Stock_Qty<=0) then Last_FAT_Per else Fat_KG*100/Stock_Qty end) as FAT_Per " +
                    ",(case when (Fat_Amt*SNF_Amt<0 or Fat_KG*SNF_KG<0 or SNF_Amt<=0 or SNF_KG<=0 or Stock_Qty<=0) then Last_SNF_Per else SNF_KG*100/Stock_Qty end) as SNF_Per ,Stock_Qty,Stock_UOM " +
                      " from ( select  sum(Stock_Qty*RI) as Stock_Qty,max(Stock_UOM) as Stock_UOM, sum(Fat_Amt * RI) as Fat_Amt,cast(sum(Fat_KG * RI)as numeric(18,3)) as Fat_KG,sum(SNF_Amt * RI) as SNF_Amt,cast(sum(SNF_KG * RI)as numeric(18,3)) as SNF_KG," + Environment.NewLine
                If settPickProductCostFromItemUOMDetail Then
                    Dim Tqty As String = "select description,Specification	from tspl_Fixed_Parameter where Type='" + clsFixedParameterType.FATSNFRate + "' and Code='" + clsFixedParameterCode.FATSNFRate + "' and description<>'0'"
                    Dim Tdt As DataTable = clsDBFuncationality.GetDataTable(Tqty, trans)
                    If Tdt Is Nothing OrElse Tdt.Rows.Count > 0 Then
                        qry += "'" + clsCommon.myCstr(clsCommon.myCdbl(Tdt.Rows(0)("description"))) + "' as Last_Fat_Rate " + Environment.NewLine +
                            ",'" + clsCommon.myCstr(clsCommon.myCdbl(Tdt.Rows(0)("Specification"))) + "' as Last_SNF_Rate  " + Environment.NewLine
                    Else
                        qry += "(Select TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='FAT') as Last_Fat_Rate " + Environment.NewLine +
                    ",(Select TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='SNF') as Last_SNF_Rate " + Environment.NewLine
                    End If
                    qry += ",(Select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='FAT') as Last_Fat_Per " + Environment.NewLine +
                    ",(Select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code where TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code='" + Item_Code + "' and TSPL_PARAMETER_MASTER.Type='SNF') as Last_SNF_Per "
                Else
                    Dim strMainLocCheck As String = ""
                    ''Comment by balwinder on 24/03/2021 Pick rate Only for the location not from the main location
                    If IsCheckInMainLocation Then
                        strMainLocCheck = " or main_location='" + strLocation + "' "
                    End If
                    qry += "isnull((select top 1 Fat_Rate from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + " ) and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and Fat_Rate>0 order by trans_id desc ),0) as Last_Fat_Rate" + Environment.NewLine +
                        ",isnull((select top 1 SNF_Rate from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + ") and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and SNF_Rate>0 order by trans_id desc ),0) as Last_SNF_Rate " + Environment.NewLine +
                        ",isnull((select top 1 Fat_Per from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + ") and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and Fat_Rate>0 order by trans_id desc ),0) as Last_Fat_Per" + Environment.NewLine +
                        ",isnull((select top 1 SNF_Per from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" + Item_Code + "' and (Location_Code='" + strLocation + "' " + strMainLocCheck + ") and  " + strDateColumn + " < '" + strDateForCheck + "' and Source_Doc_No<>'" + Current_Doc_Code + "' and InOut='I' and SNF_Rate>0 order by trans_id desc ),0) as Last_SNF_Per "
                End If
                qry += " from( select " + Environment.NewLine
                ''"  cast(Fat_KG as numeric(18,2)) as Fat_KG ,cast(SNF_KG as numeric(18,2)) as SNF_KG ,"
                qry += " Fat_KG ,SNF_KG, cast(Fat_Amt as numeric(18,2)) as Fat_Amt, cast(SNF_Amt as numeric(18,2)) as SNF_Amt,case when InOut='O' then -1 else 1 end as RI,Stock_Qty ,Stock_UOM  from TSPL_INVENTORY_MOVEMENT_NEW " + Environment.NewLine +
                       " left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code=TSPL_INVENTORY_MOVEMENT_NEW.Location_Code where 2=2 "
                If IsRejectOnly Then
                    qry += " and TSPL_Location_MASTER.Rejected_Type='Y'"
                Else
                    qry += " and TSPL_Location_MASTER.Rejected_Type='N'"
                End If
                qry += "  and Item_Code='" & Item_Code & "' and (TSPL_INVENTORY_MOVEMENT_NEW.Location_Code='" + strLocation + "'"
                ''Comment by balwinder on 24/03/2021 Pick rate Only for the location not from the main location
                If IsCheckInMainLocation Then
                    qry += "  or TSPL_INVENTORY_MOVEMENT_NEW.main_location='" + strLocation + "'"
                End If
                qry += " ) and  " + strDateColumn + " <= '" + strDateForCheck + "' and Source_Doc_No not in ('" & Current_Doc_Code & "') " & cond_TransId & "  " + ExtrWhrl + Environment.NewLine +
             " ) xxx ) xxxx"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt.Rows.Count > 0 Then
                    obj.FAT_Cost = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Avg_Fat_Cost")), IIf(For10decimalPlaces, 10, 2))
                    obj.SNF_Cost = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Avg_SNF_Cost")), IIf(For10decimalPlaces, 10, 2))

                    obj.FAT_Per = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("FAT_Per")), IIf(For10decimalPlaces, 10, 3))
                    obj.SNF_Per = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("SNF_Per")), IIf(For10decimalPlaces, 10, 3))

                    obj.Stock_Qty = Math.Round(clsCommon.myCdbl(dt.Rows(0).Item("Stock_Qty")), 2)
                    obj.Stock_UOM = clsCommon.myCstr(dt.Rows(0).Item("Stock_UOM"))
                End If
            ElseIf clsCommon.CompairString(Product_Type, "MP") = CompairStringResult.Equal Then
                Dim Stock_Qty As Decimal = 0
                Stock_Qty = dblQty * clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans)
                Dim AvgCost As Decimal = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, Item_Code, strLocation, Stock_Qty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT", "", ExtrWhrl)
                Dim avgRate As Decimal = If((dblFATqty + dblSNFqty) <= 0, 0, AvgCost / IIf((dblFATqty + dblSNFqty) <= 0, 1, (dblFATqty + dblSNFqty)))
                obj.FAT_Cost = avgRate * dblFATqty
                obj.SNF_Cost = avgRate * dblSNFqty
            Else
                Dim Stock_Qty As Decimal = 0
                Stock_Qty = dblQty * clsItemMaster.GetConvertionFactor(Item_Code, Unit_Code, trans)
                Dim AvgCost As Decimal = clsInventoryMovement.GetCost(EnumCostingMethod.Averege, Item_Code, strLocation, Stock_Qty, dtDocumentDate, dtPostingDate, isApplyCostOnPostDate, trans, "TSPL_INVENTORY_MOVEMENT", "", ExtrWhrl)
                obj.FAT_Cost = AvgCost * 2 / 3
                obj.SNF_Cost = AvgCost / 3
            End If

            If obj.FAT_Cost + obj.SNF_Cost <= 0 Then
                If settPickProductCostFromItemUOMDetail Then
                    Dim Tqty As String = "select description,Specification	from tspl_Fixed_Parameter where Type='" + clsFixedParameterType.FATSNFRate + "' and Code='" + clsFixedParameterCode.FATSNFRate + "' and description<>'0'"
                    Dim Tdt As DataTable = clsDBFuncationality.GetDataTable(Tqty, trans)
                    If Tdt Is Nothing OrElse Tdt.Rows.Count > 0 Then
                        obj.FAT_Cost = clsCommon.myCdbl(Tdt.Rows(0)("description")) * dblFATqty
                        obj.SNF_Cost = clsCommon.myCdbl(Tdt.Rows(0)("Specification")) * dblSNFqty
                    Else
                        ''BHA/10/09/18-000527,BHA/07/09/18-000521,KDI/16/03/18-000138,BHA/17/08/18-000450 by balwinder on 11/09/2018
                        qry = "select TSPL_PARAMETER_MASTER.Type, TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate from TSPL_ITEM_QC_PARAMETER_MASTER " + Environment.NewLine +
                        "left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code" + Environment.NewLine +
                        "where Item_Code='" + Item_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        dblFATqty = Math.Abs(dblFATqty)
                        dblSNFqty = Math.Abs(dblSNFqty)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "FAT") = CompairStringResult.Equal Then
                                    obj.FAT_Cost = clsCommon.myCdbl(dr("StandardRate")) * dblFATqty
                                    If clsCommon.myCdbl(obj.FAT_Cost) <= 0 And dblFATqty <> 0 Then
                                        Throw New Exception("Please Provide FAT Standard Rate of Item " + Item_Code + "")
                                    End If
                                End If
                                If clsCommon.CompairString(clsCommon.myCstr(dr("Type")), "SNF") = CompairStringResult.Equal Then
                                    obj.SNF_Cost = clsCommon.myCdbl(dr("StandardRate")) * dblSNFqty
                                    If clsCommon.myCdbl(obj.SNF_Cost) <= 0 And dblSNFqty <> 0 Then
                                        Throw New Exception("Please Provide SNF Standard Rate of Item " + Item_Code + "")
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Return obj
    End Function

    ''richa Ticket No.BM00000003617 on 25/08/2014
    Public Shared Function getBalance(ByVal strICode As String, ByVal strMainLocation As String, ByVal strLocation As String, ByVal strDocumentNo As String, ByVal dtDocumentDate As DateTime, ByVal trans As SqlTransaction, ByVal strUOM As String) As Double
        Dim qry As String = "select SUM(qty*RI) as Qty from(" + Environment.NewLine
        qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,((case when Minimum_Bal.Minimum_Balance is null then  xx.Qty else (case when Minimum_Bal.Minimum_Balance>xx.Qty then xx.Qty else Minimum_Bal.Minimum_Balance end)  end)* TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Qty" + Environment.NewLine
        qry += " from (" + Environment.NewLine

        qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM  from("
        qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew from("
        qry += " select TSPL_INVENTORY_MOVEMENT_NEW.Trans_Id, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code ,ISNULL(TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,'') as Location_Code , TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty as Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM  as UOMNew "
        qry += " from TSPL_INVENTORY_MOVEMENT_NEW "
        qry += " where TSPL_INVENTORY_MOVEMENT_NEW.Qty<>0 and TSPL_INVENTORY_MOVEMENT_NEW.Item_Code='" + strICode + "' and coalesce(Main_Location,'')='" + strMainLocation + "' and  Location_Code='" + strLocation + "' "
        'If dblMRP > 0 Then
        '    qry += " and TSPL_INVENTORY_MOVEMENT_NEW.MRP='" + clsCommon.myCstr(dblMRP) + "'"
        'End If
        Dim qryMinBal As String = "select null as Item_Code,null as Location_Code,null as Minimum_Balance"
        Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, trans))
        If intSettingType = 1 Then
            'qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_NEW.InOut='I' and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            qryMinBal = " select Item_Code,Location_Code,min(Closing_Balance) as Minimum_Balance from (" &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT where Item_Code='" & strICode & "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code " &
                        " union all " &
                        " select Item_Code,Location_Code,cast(Punching_Date as date) as Punching_Date,sum(SUM((CASE WHEN InOut='I' THEN 1 ELSE -1 END)*Stock_Qty)) over(order by cast(Punching_Date as date)) as Closing_Balance " &
                        " from TSPL_INVENTORY_MOVEMENT_NEW where Item_Code='" & strICode & "' and coalesce(Main_Location,'')='" + strMainLocation + "' AND Location_Code='" & strLocation & "' " &
                        " group by cast(Punching_Date as date),Item_Code,Location_Code) as MinimumQry where Punching_Date>'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "' " &
                        " group by Item_Code,Location_Code "
        ElseIf intSettingType = 0 Then
            qry += " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtDocumentDate), "dd/MMM/yyyy hh:mm tt") + "'"

        End If
        qry += " )xxx  "
        qry += " )xxxx group by Item_Code,Location_Code,UOMNew "


        qry += " union all " + Environment.NewLine

        qry += " select TSPL_Dispatch_Detail_BulkSale.Item_Code as ICode,TSPL_Dispatch_BulkSale.Location_Code as Locaion,TSPL_Dispatch_Detail_BulkSale.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale.Unit_code AS Uom "
        qry += " from TSPL_Dispatch_Detail_BulkSale "
        qry += " left outer join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_Detail_BulkSale.Document_No"
        qry += " where TSPL_Dispatch_BulkSale.Posted=0 and TSPL_Dispatch_Detail_BulkSale.Item_Code='" + strICode + "' and TSPL_Dispatch_BulkSale.Location_Code='" + strMainLocation + "' and TSPL_Dispatch_Detail_BulkSale.Qty<>0  "
        qry += " and TSPL_Dispatch_Detail_BulkSale.Document_No not in ('" + strDocumentNo + "')"
        'If dblMRP > 0 Then
        '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
        'End If

        ''richa agawral

        qry += " union all " + Environment.NewLine &
            " select TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code as ICode,TSPL_Dispatch_BulkSale_Trade.Location_Code as Locaion,  " + Environment.NewLine &
            " TSPL_Dispatch_Detail_BulkSale_Trade.Qty,-1 as RI,TSPL_Dispatch_Detail_BulkSale_Trade.Unit_code AS Uom  from TSPL_Dispatch_Detail_BulkSale_Trade  " + Environment.NewLine &
            " left outer join TSPL_Dispatch_BulkSale_Trade  on TSPL_Dispatch_BulkSale_Trade.Document_No=TSPL_Dispatch_Detail_BulkSale_Trade.Document_No  " + Environment.NewLine &
            " where TSPL_Dispatch_BulkSale_Trade.Posted=0 and TSPL_Dispatch_Detail_BulkSale_Trade.Item_Code='" + strICode + "' and TSPL_Dispatch_BulkSale_Trade.Location_Code='" + strMainLocation + "'" + Environment.NewLine &
            " and TSPL_Dispatch_Detail_BulkSale_Trade.Qty<>0   and TSPL_Dispatch_Detail_BulkSale_Trade.Document_No not in ('" + strDocumentNo + "')"

        qry += " union all " + Environment.NewLine

        qry += " select TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code as ICode,TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code as Locaion,TSPL_PP_ISSUE_ITEM_DETAIL.Qty,-1 as RI,TSPL_PP_ISSUE_ITEM_DETAIL.Unit_code AS Uom "
        qry += " from TSPL_PP_ISSUE_ITEM_DETAIL "
        qry += " left outer join TSPL_PP_ISSUE_HEAD on TSPL_PP_ISSUE_HEAD.Issue_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code"
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' "
        qry += " where TSPL_PP_ISSUE_HEAD.Is_post=0 and TSPL_PP_ISSUE_ITEM_DETAIL.Item_Code='" + strICode + "' and TSPL_PP_ISSUE_ITEM_DETAIL.From_Loaction_Code='" + strLocation + "' and TSPL_PP_ISSUE_ITEM_DETAIL.Qty<>0  "
        qry += " and TSPL_PP_ISSUE_ITEM_DETAIL.Issue_Code not in ('" + strDocumentNo + "')"
        'If dblMRP > 0 Then
        '    qry += " and TSPL_Dispatch_Detail_BulkSale.MRP='" + clsCommon.myCstr(dblMRP) + "' "
        'End If

        qry += " union all " + Environment.NewLine
        qry += " select Main_Item_Code as ICode,LOCATION_CODE as Location,QUANTITY,(case when TRANSACTION_TYPE='Assembly' then 1  else -1 end) as RI,"
        qry += " BUILD_ITEM_UNIT_CODE as UnitCode from TSPL_PROD_ASSEMBLIES where TSPL_PROD_ASSEMBLIES.TRANSACTION_TYPE='Disassembly' and TSPL_PROD_ASSEMBLIES.POSTED=0 and  TSPL_PROD_ASSEMBLIES.Main_Item_Code='" + strICode + "'  and TSPL_PROD_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strLocation) > 0 Then
            qry += " and TSPL_PROD_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If


        qry += " union all  "

        qry += " select  TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE AS ICode,TSPL_PJC_ASSEMBLIES.LOCATION_CODE as Location,"
        qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_QUANTITY as Qty,"
        qry += " (case when TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly' then  -1 else  1 end) AS RI,"
        qry += " TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_UNIT_CODE as UnitCode from TSPL_PJC_ASSEMBLIES "
        qry += " inner JOIN TSPL_PROD_ASSEMBLIES_ITEM_DETAIL ON TSPL_PJC_ASSEMBLIES.CODE=TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.ASSEMBLY_CODE "
        qry += " where TSPL_PJC_ASSEMBLIES.TRANSACTION_TYPE='Assembly'  and  TSPL_PJC_ASSEMBLIES.POSTED=0 and TSPL_PROD_ASSEMBLIES_ITEM_DETAIL.CONSM_ITEM_CODE='" + strICode + "'  and TSPL_PJC_ASSEMBLIES.CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strLocation) > 0 Then
            qry += " and TSPL_PJC_ASSEMBLIES.LOCATION_CODE='" & strLocation & "'"
        End If



        qry += " union all  "

        qry += " select  TSPL_WRECKAGE_BOOKING.Item_Code AS ICode,TSPL_WRECKAGE_ENTRY.LOCATION_CODE as Location,"
        qry += " TSPL_WRECKAGE_BOOKING.WRECKAGE_QTY as Qty, -1 AS RI,"
        qry += " TSPL_WRECKAGE_BOOKING.Unit_Code as UnitCode from TSPL_WRECKAGE_ENTRY "
        qry += " inner JOIN TSPL_WRECKAGE_BOOKING ON TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE=TSPL_WRECKAGE_BOOKING.WRECKAGE_CODE "
        qry += " where TSPL_WRECKAGE_ENTRY.POSTED=0 and TSPL_WRECKAGE_BOOKING.ITEM_CODE='" + strICode + "'  and TSPL_WRECKAGE_ENTRY.WRECKAGE_ENTRY_CODE  not in ('" + strDocumentNo + "')"
        If clsCommon.myLen(strLocation) > 0 Then
            qry += " and TSPL_WRECKAGE_ENTRY.LOCATION_CODE='" & strLocation & "'"
        End If


        qry += " union all " + Environment.NewLine

        qry += " select TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code as ICode,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code as Locaion,TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity as Qty,-1 as RI,TSPL_ITEM_MASTER.Unit_code AS Uom "
        qry += " from TSPL_LOADING_TANKER_DETAIL_BULKSALE "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' "
        qry += " where TSPL_LOADING_TANKER_DETAIL_BULKSALE.Item_Code='" + strICode + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Location_Code='" + strMainLocation + "' AND TSPL_LOADING_TANKER_DETAIL_BULKSALE.Silo_No='" + strLocation + "' and TSPL_LOADING_TANKER_DETAIL_BULKSALE.Quantity<>0  "
        qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in ('" + strDocumentNo + "')"
        qry += " and TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No not in (select LoadingTanker_No FROM TSPL_Quality_Check_BulkSale LEFT OUTER JOIN TSPL_Dispatch_BulkSale ON TSPL_Dispatch_BulkSale.QC_Code=TSPL_Quality_Check_BulkSale.QC_No WHERE ISNULL(TSPL_Dispatch_BulkSale.QC_Code,'')<>'')"
        'qry += " and not exists (select 1 from TSPL_LOADING_TANKER_DETAIL_BULKSALE Left outer Join TSPL_Quality_Check_BulkSale on TSPL_LOADING_TANKER_DETAIL_BULKSALE.LoadingTanker_No=TSPL_Quality_Check_BulkSale.LoadingTanker_No)"

        '' added by richa agarwal to include transactions of store adjustment whose trans type is Out and milk type is 1
        qry += "union all " + Environment.NewLine &
            "  select TSPL_ADJUSTMENT_DETAIL.Item_Code as ICode,TSPL_ADJUSTMENT_HEADER.MainLocationCode  as Locaion,TSPL_ADJUSTMENT_DETAIL.Item_Quantity ,-1 as RI,TSPL_ADJUSTMENT_DETAIL.Unit_code AS Uom " &
            " from TSPL_ADJUSTMENT_DETAIL left outer join TSPL_ADJUSTMENT_HEADER on TSPL_ADJUSTMENT_HEADER.Adjustment_No =TSPL_ADJUSTMENT_DETAIL.Adjustment_No  " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ADJUSTMENT_DETAIL.Item_Code and TSPL_ITEM_MASTER.Product_Type='MI' " &
            " where TSPL_ADJUSTMENT_HEADER.Posted ='N' and TSPL_ADJUSTMENT_DETAIL.Item_Code='" + strICode + "' and TSPL_ADJUSTMENT_HEADER.Loc_Code ='" + strLocation + "' and TSPL_ADJUSTMENT_HEADER.MainLocationCode ='" & strMainLocation & "' and TSPL_ADJUSTMENT_DETAIL.Item_Quantity <>0 " &
            " and TSPL_ADJUSTMENT_HEADER.Trans_Type  ='Out' and TSPL_ADJUSTMENT_HEADER .IsMilkType =1 and TSPL_ADJUSTMENT_DETAIL.Adjustment_No  not in ('" + strDocumentNo + "') " + Environment.NewLine

        qry += " )xx" + Environment.NewLine &
               " left join (" & qryMinBal & ") as Minimum_Bal on xx.ICode=Minimum_Bal.Item_Code and xx.Location=Minimum_Bal.Location_Code "
        qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM"
        qry += " )xxx group by ICode,Location"
        Dim BalQty As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
        Dim dblConvFac As Decimal = clsItemMaster.GetConvertionFactor(strICode, strUOM, trans)

        Return BalQty / IIf(dblConvFac <= 0, 1, dblConvFac)
    End Function

    Public Shared Function GetStdQty(ByVal Trans As SqlTransaction, ByVal FATKG As Double, ByVal SNFKG As Double, ByVal TransDate As DateTime) As Decimal
        Dim retVal As Double = 0
        Try
            Dim qry As String = "select  top 1 (" + clsCommon.myCstr(FATKG) + "*CAST(Ratio as decimal)/FAT_Pers)+(" + clsCommon.myCstr(SNFKG) + "*SNF_Ratio/SNF_Pers) as Qty  from TSPL_MILK_PRICE_MASTER where Effective_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(TransDate), "dd/MMM/yyyy hh:mm tt") + "' order by Effective_Date desc"
            retVal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Trans))
        Catch ex As Exception
        End Try
        Return retVal
    End Function

End Class