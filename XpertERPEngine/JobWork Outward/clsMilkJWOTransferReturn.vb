Imports common
Imports System.Data.SqlClient
Public Class clsMilkJWOTransferReturn
#Region "Variables"
    Public Document_No As String = Nothing
    Public SRN_Return_NO As String = Nothing
    Public MilkTransRetNo As String = Nothing
    Public Document_Date As DateTime
    Public JWO_Transfer_No As String = Nothing
    Public Remarks As String = Nothing
    Public JWO_SRN_From_Location_Code As String = Nothing ''Not a Table field
    Public Form_ID As String = Nothing
    Public arrCustomFields As List(Of clsCustomFieldValues) = Nothing
#End Region

    Public Function SaveData(ByVal obj As clsMilkJWOTransferReturn, ByVal isNewEntry As Boolean) As Boolean
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

    Public Function SaveData(ByVal objt As clsMilkJWOTransferReturn, ByVal isNewEntry As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Try
            'Dim objSRN As clsJWOTransferOtherHead = clsJWOTransferOtherHead.GetData(objt.JWO_Transfer_No, NavigatorType.Current, trans)
            AllowToSave(objt, trans)
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Transfer Return", objt.JWO_SRN_From_Location_Code, objt.Document_Date, trans)
            Try
                If isNewEntry Then
                    objt.Document_No = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(objt.Document_Date), clsDocType.JobWorkTransferMilkReturn, "", objt.JWO_SRN_From_Location_Code)
                Else
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(objt.Document_No), "TSPL_MILK_JOBWORK_TRANSFER_RETURN", "Document_No", trans)
                End If
                If (clsCommon.myLen(objt.Document_No) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If

                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(objt.Document_Date, "dd/MMM/yyyy hh:mm tt"))
                clsCommon.AddColumnsForChange(coll, "JWO_Transfer_No", objt.JWO_Transfer_No)
                clsCommon.AddColumnsForChange(coll, "MilkTransRetNo", objt.MilkTransRetNo, True)
                clsCommon.AddColumnsForChange(coll, "SRN_Return_NO", objt.SRN_Return_NO, True)
                clsCommon.AddColumnsForChange(coll, "Remarks", objt.Remarks)
                If isNewEntry Then
                    clsCommon.AddColumnsForChange(coll, "Document_No", objt.Document_No)
                    clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_JOBWORK_TRANSFER_RETURN", OMInsertOrUpdate.Insert, "", trans)
                Else
                    isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_JOBWORK_TRANSFER_RETURN", OMInsertOrUpdate.Update, "TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_No='" + objt.Document_No + "'", trans)
                End If
                isSaved = isSaved AndAlso clsCustomFieldValues.SaveData(objt.Form_ID, objt.Document_No, objt.arrCustomFields, trans)


                ''Revese Inventory movement

                Dim obj As clsMilkJobworkTransfer = clsMilkJobworkTransfer.getData(objt.JWO_Transfer_No, NavigatorType.Current, False, trans)

                Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
                obj.Document_Code = objt.Document_No
                obj.Document_Date = objt.Document_Date
                Dim qry As String = ""
                Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
                Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                Dim ArrInventoryMovementIn As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
                Dim ArrInventoryMovementReturn As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

                Dim strItemType As String = clsItemMaster.GetItemType(obj.Item_Code, trans)
                Dim strItemTypeToSave As String = ""
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    strItemTypeToSave = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    strItemTypeToSave = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    strItemTypeToSave = "FT"
                ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                    strItemTypeToSave = "A"
                Else
                    strItemTypeToSave = strItemType
                End If
                Dim strItemUnitCode As String = obj.UOM ' clsItemMaster.GetStockUnit(obj.Item_Code, trans)

                Dim objLocationDetails As New clsItemLocationDetails()

                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, strItemUnitCode, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + obj.Item_Code + " and Uom:'" + strItemUnitCode)
                End If

                objLocationDetails.Item_Code = obj.Item_Code
                objLocationDetails.Item_Desc = obj.Item_Desc
                objLocationDetails.Location_Code = obj.Loc_Code
                objLocationDetails.Location_Desc = clsLocation.GetName(obj.Loc_Code, trans)
                objLocationDetails.Item_Qty = obj.Net_Weight
                objLocationDetails.Amount = obj.Actual_Amount
                objLocationDetails.MRP = 0
                objLocationDetails.ItemType = strItemTypeToSave
                ArrLocationDetails.Add(objLocationDetails)
                Dim strSiloNo As String = Nothing
                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                Dim strJobLoc As String = Nothing
                Dim strJobVendor As String = Nothing
                Dim strMainLocat As String
                If (clsCommon.myLen(obj.SRN_NO) > 0 OrElse clsCommon.myLen(obj.Milk_Transfer_In) > 0) Then
                    strSiloNo = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
                    strJobLoc = clsDBFuncationality.getSingleValue("select Sublocation_Code  from tspl_gate_entry_details where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
                    strJobVendor = clsDBFuncationality.getSingleValue("select Jobwork_Vendor  from tspl_location_master where Location_Code='" & strJobLoc & "'", trans)
                    strMainLocat = obj.Virtual_location
                Else
                    strJobLoc = obj.JobWork_location
                    strJobVendor = obj.Vendor_Code
                    strMainLocat = obj.Loc_Code
                End If

                '' For Out Type
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = strMainLocat
                objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                objInventoryMovemnt.Item_Code = obj.Item_Code
                objInventoryMovemnt.Item_Desc = obj.Item_Desc
                objInventoryMovemnt.Qty = obj.Net_Weight
                objInventoryMovemnt.UOM = obj.UOM
                objInventoryMovemnt.MRP = 0
                objInventoryMovemnt.Add_Cost = 0
                objInventoryMovemnt.FAT_Per = obj.fat_per
                objInventoryMovemnt.SNF_Per = obj.snf_Per
                objInventoryMovemnt.FAT_KG = obj.fat_KG
                objInventoryMovemnt.SNF_KG = obj.SNF_KG
                objInventoryMovemnt.Net_Cost = obj.Actual_Amount
                objInventoryMovemnt.main_location = obj.Loc_Code
                objInventoryMovemnt.Fat_Rate = obj.fat_Rate
                objInventoryMovemnt.SNF_Rate = obj.SNF_Rate
                objInventoryMovemnt.Fat_Amt = obj.fat_Rate * obj.fat_KG
                objInventoryMovemnt.SNF_Amt = obj.SNF_Rate * obj.SNF_KG
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                objInventoryMovemnt.Basic_Cost = obj.Actual_Amount / obj.Net_Weight
                ArrInventoryMovement.Add(objInventoryMovemnt)

                '' For In Type
                Dim objInventoryMovemnt1 As New clsInventoryMovementNew()
                objInventoryMovemnt1.InOut = "O"
                objInventoryMovemnt1.Location_Code = strJobLoc
                objInventoryMovemnt1.Vendor_Code = obj.Vendor_Code
                objInventoryMovemnt1.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                objInventoryMovemnt1.Item_Code = obj.Item_Code
                objInventoryMovemnt1.Item_Desc = obj.Item_Desc
                objInventoryMovemnt1.Qty = obj.Net_Weight
                objInventoryMovemnt1.UOM = obj.UOM
                objInventoryMovemnt1.MRP = 0
                objInventoryMovemnt1.Add_Cost = 0
                objInventoryMovemnt1.FAT_Per = obj.fat_per
                objInventoryMovemnt1.SNF_Per = obj.snf_Per
                objInventoryMovemnt1.FAT_KG = obj.fat_KG
                objInventoryMovemnt1.SNF_KG = obj.SNF_KG
                objInventoryMovemnt1.Net_Cost = obj.Actual_Amount
                objInventoryMovemnt1.main_location = obj.Loc_Code
                objInventoryMovemnt1.Fat_Rate = obj.fat_Rate
                objInventoryMovemnt1.SNF_Rate = obj.SNF_Rate
                objInventoryMovemnt1.Fat_Amt = obj.fat_Rate * obj.fat_KG
                objInventoryMovemnt1.SNF_Amt = obj.SNF_Rate * obj.SNF_KG
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt1.ItemType = "FT"
                End If
                objInventoryMovemnt1.ItemType = strItemTypeToSave
                objInventoryMovemnt1.Basic_Cost = obj.Actual_Amount / obj.Net_Weight
                ArrInventoryMovementIn.Add(objInventoryMovemnt1)


                isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("MilkTransJWOReturn", obj.Document_Code, objt.Document_Date, clsCommon.GetPrintDate(objt.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("MilkTransJWOReturn", obj.Document_Code, objt.Document_Date, clsCommon.GetPrintDate(objt.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)

                ' added by priti for milk transfer in return
                If clsCommon.myLen(objt.MilkTransRetNo) > 0 Then


                    Dim FatQcPer As Double = 0
                    Dim SNFQcPer As Double = 0
                    Dim FatValue As Double = 0
                    Dim SnfValue As Double = 0
                    Dim rcptAmount As Double = 0
                    Dim subLocCode As String = ""
                    Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
                    Dim objD As clsMccDispatch = clsMccDispatch.getData(obj.Challan_No, NavigatorType.Current, trans)

                    FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Type='FAT' ", trans))
                    SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Type='SNF' ", trans))

                    FatValue = (objW.Net_Weight * FatQcPer / 100) * objD.FAT_RATE
                    SnfValue = (objW.Net_Weight * SNFQcPer / 100) * objD.SNF_RATE
                    rcptAmount = FatValue + SnfValue


                    strItemType = clsItemMaster.GetItemType(objW.Item_Code, trans)
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        strItemTypeToSave = "A"
                    Else
                        strItemTypeToSave = strItemType
                        'Throw New Exception("Item Type not found: " + strItemType)
                    End If

                    subLocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code from TSPL_MILK_UNLOADING where weighment_no='" & objW.Weighment_No & "'", trans))

                    Dim objInventoryMovemntReturn As New clsInventoryMovementNew()
                    objInventoryMovemntReturn.InOut = "O"
                    objInventoryMovemntReturn.Location_Code = subLocCode
                    objInventoryMovemntReturn.main_location = obj.Loc_Code
                    objInventoryMovemntReturn.Item_Code = objW.Item_Code
                    objInventoryMovemntReturn.Item_Desc = objW.Item_Desc
                    objInventoryMovemntReturn.Qty = objW.Net_Weight
                    objInventoryMovemntReturn.UOM = clsItemMaster.GetStockUnit(objW.Item_Code, trans)
                    objInventoryMovemntReturn.MRP = rcptAmount
                    objInventoryMovemntReturn.Add_Cost = 0

                    objInventoryMovemntReturn.Other_Location_Code = objD.MCC_Code
                    objInventoryMovemntReturn.Other_Location_Desc = objD.MCC_Name
                    '' added by Panch Raj for production costing
                    objInventoryMovemntReturn.Fat_Rate = objD.FAT_RATE
                    objInventoryMovemntReturn.SNF_Rate = objD.SNF_RATE
                    objInventoryMovemntReturn.Fat_Amt = FatValue
                    objInventoryMovemntReturn.SNF_Amt = SnfValue

                    'select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='QCM00001' and Param_Type='SNF'
                    objInventoryMovemntReturn.FAT_Per = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Type='FAT' ", trans))
                    objInventoryMovemntReturn.SNF_Per = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Type='SNF' ", trans))
                    objInventoryMovemntReturn.FAT_KG = objInventoryMovemnt.FAT_Per * objW.Net_Weight / 100
                    objInventoryMovemntReturn.SNF_KG = objInventoryMovemnt.SNF_Per * objW.Net_Weight / 100
                    objInventoryMovemntReturn.Net_Cost = rcptAmount
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemntReturn.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemntReturn.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemntReturn.ItemType = "FT"
                    End If
                    objInventoryMovemntReturn.ItemType = strItemTypeToSave
                    objInventoryMovemntReturn.Basic_Cost = rcptAmount / objW.Net_Weight
                    ArrInventoryMovementReturn.Add(objInventoryMovemntReturn)
                    clsInventoryMovementNew.SaveData("MilkTransferInReturn", objt.MilkTransRetNo, objt.Document_Date, clsCommon.GetPrintDate(objt.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementReturn, trans)
                End If

                ' added by priti for bulk milk srn return
                If clsCommon.myLen(objt.SRN_Return_NO) > 0 Then
                    Dim objSRN As clsBulkMilkSRN = clsBulkMilkSRN.getData(obj.SRN_NO, NavigatorType.Current, False, trans)
                    Dim objReturn As clsBulkMilkSRNReturn = clsBulkMilkSRNReturn.getData(objt.SRN_Return_NO, NavigatorType.Current, trans)
                    Dim ArrInventoryMovementSRNRet As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)

                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        strItemTypeToSave = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        strItemTypeToSave = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        strItemTypeToSave = "FT"
                    ElseIf clsCommon.CompairString(strItemType, "A") = CompairStringResult.Equal Then
                        strItemTypeToSave = "A"
                    Else
                        strItemTypeToSave = strItemType
                    End If
                    strItemUnitCode = obj.UOM
                    ConvFac = clsItemMaster.GetConvertionFactor(obj.Item_Code, strItemUnitCode, trans)
                    If ConvFac = 0 Then
                        Throw New Exception("Conversion Factor found zero for item :" + obj.Item_Code + " and Uom:'" + strItemUnitCode)
                    End If
                    Dim objInventoryMovemntSRNRet As New clsInventoryMovementNew()
                    objInventoryMovemntSRNRet.InOut = "O"
                    '-----------Getting Sub Location Where Milk Was unloaded
                    strSiloNo = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
                    '-----------------------------------
                    objInventoryMovemntSRNRet.Location_Code = strSiloNo
                    objInventoryMovemntSRNRet.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemntSRNRet.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)

                    objInventoryMovemntSRNRet.Item_Code = obj.Item_Code
                    objInventoryMovemntSRNRet.Item_Desc = obj.Item_Desc
                    objInventoryMovemntSRNRet.Qty = obj.Net_Weight
                    objInventoryMovemntSRNRet.UOM = obj.UOM
                    objInventoryMovemntSRNRet.MRP = 0
                    objInventoryMovemntSRNRet.Add_Cost = 0
                    objInventoryMovemntSRNRet.FAT_Per = obj.fat_per
                    objInventoryMovemntSRNRet.SNF_Per = obj.snf_Per
                    objInventoryMovemntSRNRet.FAT_KG = obj.fat_KG
                    objInventoryMovemntSRNRet.SNF_KG = obj.SNF_KG
                    objInventoryMovemntSRNRet.Fat_Rate = obj.fat_Rate
                    objInventoryMovemntSRNRet.SNF_Rate = obj.SNF_Rate
                    objInventoryMovemntSRNRet.Fat_Amt = obj.FatAmt
                    objInventoryMovemntSRNRet.SNF_Amt = obj.SnfAmt
                    objInventoryMovemntSRNRet.Net_Cost = obj.Actual_Amount
                    objInventoryMovemntSRNRet.main_location = obj.Loc_Code
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemntSRNRet.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemntSRNRet.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemntSRNRet.ItemType = "FT"
                    End If
                    objInventoryMovemntSRNRet.ItemType = strItemTypeToSave
                    objInventoryMovemntSRNRet.Basic_Cost = obj.Actual_Amount / obj.Net_Weight
                    ArrInventoryMovementSRNRet.Add(objInventoryMovemntSRNRet)
                    isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("BulkSRNRet", objReturn.SRN_Return_NO, objReturn.SRN_Return_Date, clsCommon.GetPrintDate(objReturn.SRN_Return_Date, "dd/MM/yyyy"), ArrInventoryMovementSRNRet, trans)
                End If
                CreateTransferOutJE(obj, trans, "")

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Catch err As Exception
            Throw New Exception(err.Message)
        End Try
        Return isSaved
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal Trans As SqlTransaction) As Boolean
        'If Trans Is Nothing Then
        '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        'End If

        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = ""

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='JW-MR' and Source_Doc_No='" + strCode + "'", Trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", Trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
            End If
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT_NEW", "Source_Doc_No", Trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + strCode + "' and Trans_Type='MilkTransJWOReturn'"
            clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
            Qry = "delete from TSPL_MILK_JOBWORK_TRANSFER_RETURN where Document_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_MILK_JOBWORK_TRANSFER_RETURN", "Document_No", Trans)
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Shared Function AllowToSave(ByVal obj As clsMilkJWOTransferReturn, ByVal trans As SqlTransaction) As Boolean
        Dim Qry As String = "select Document_No from TSPL_MILK_JOBWORK_TRANSFER_RETURN where JWO_Transfer_No='" + obj.JWO_Transfer_No + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Throw New Exception("Transfer Return No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("Document_No")) + " already created"))
        End If

        'Qry = "select distinct PI_No from TSPL_PI_DETAIL where SRN_Id ='" + obj.JWO_Transfer_No + "'"
        'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '    Throw New Exception("SRN is used in Purchase Invoice No " + clsCommon.myCstr(clsDBFuncationality.getSingleValue(dt.Rows(0)("PI_No")) + " can not return it."))
        'End If

        Return True
    End Function

    Public Shared Function CreateTransferOutJE(obj As clsMilkJobworkTransfer, trans As SqlTransaction, strVoucherNoForRecreateOnly As String) As Boolean
        Dim rValue As Boolean = False
        Try
            If clsCommon.myLen(obj.SRN_NO) = 0 AndAlso clsCommon.myLen(obj.Milk_Transfer_In) = 0 Then
                Dim Stock_Transfer_Ac As String = String.Empty
                Dim PurchaseJobWorkAcc As String = String.Empty
                Dim Stock_Transfer_Ac_FromLoc As String = String.Empty
                Dim dt As Date = clsCommon.GETSERVERDATE(trans)
                Dim FromLocSeg As String = String.Empty
                Dim ToLocSeg As String = String.Empty
                If obj.isPosted = 1 Then
                    dt = obj.Document_Date
                End If
                FromLocSeg = obj.Loc_Code
                ToLocSeg = obj.JobWork_location
                Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))
                Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans))

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim ArryLst As ArrayList = New ArrayList()
                    For i As Integer = 0 To obj.Arr.Count - 1
                        Dim Amt As Double = clsCommon.myCdbl(obj.Arr(i).Actual_Amount)
                        Dim dtAccount As DataTable = clsDBFuncationality.GetDataTable("select Purchase_JobWork,Stock_Transfer_JobWork from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Arr(i).Item_Code & "') ", trans)
                        If dtAccount Is Nothing OrElse dtAccount.Rows.Count <= 0 Then
                            Throw New Exception("Please Map Purchase Account Set vendor " + obj.Vendor_Code)
                        End If

                        PurchaseJobWorkAcc = clsCommon.myCstr(dtAccount.Rows(0)("Purchase_JobWork"))
                        If clsCommon.myLen(PurchaseJobWorkAcc) <= 0 Then
                            Throw New Exception("Please Map  Purchase Job Work A/C From Purchase Account Set For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                        End If
                        PurchaseJobWorkAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(PurchaseJobWorkAcc, FromLocationSegment, True, False, trans)
                        '  ArryLst.Add(New String() {PurchaseJobWorkAcc, Amt})
                        ArryLst.Add(New String() {PurchaseJobWorkAcc, Amt * -1})
                        Stock_Transfer_Ac = clsCommon.myCstr(dtAccount.Rows(0)("Stock_Transfer_JobWork"))
                        If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                            Throw New Exception("Please Map Stock Transfer Job Work From Purchase Account Set For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                        End If
                        Stock_Transfer_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocationSegment, True, trans)
                        ' ArryLst.Add(New String() {Stock_Transfer_Ac, Amt * -1})
                        ArryLst.Add(New String() {Stock_Transfer_Ac, Amt})
                    Next

                    Dim GLDesc As String = "Journal Entry Against Milk Job Work Transfer Return - Doc No." & obj.Document_Code & " "
                    Dim Remarks As String = "Journal Entry against Milk Job Work Transfer Return - from location -" & obj.JobWork_location & " to location- " & obj.Loc_Code & " For Doc No. " & obj.Document_Code & ""
                    If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then ''because if voucher no known then recreate it with same no.
                        transportSql.FunGrnlEntryWithTrans(obj.JobWork_location, False, strVoucherNoForRecreateOnly, trans, obj.Document_Date, GLDesc, "JW-TR", "JWO Milk Transfer Return", obj.Document_Code, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, Remarks)
                    Else
                        transportSql.FunGrnlEntryWithTrans(obj.JobWork_location, False, trans, obj.Document_Date, GLDesc, "JW-MR", "JWO Milk Transfer Return", obj.Document_Code, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, Remarks)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Public Shared Function GetData(ByVal strDocNo As String, ByVal NavType As NavigatorType, ByVal trans As SqlTransaction) As clsMilkJWOTransferReturn
        Dim obj As clsMilkJWOTransferReturn = Nothing
        Dim qry As String = "SELECT TSPL_MILK_JOBWORK_TRANSFER_RETURN.* from TSPL_MILK_JOBWORK_TRANSFER_RETURN  where 2=2"
        Dim whrCls As String = ""
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_No = (select MIN(Document_No) from TSPL_MILK_JOBWORK_TRANSFER_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Last
                qry += " and TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_No = (select Max(Document_No) from TSPL_MILK_JOBWORK_TRANSFER_RETURN WHERE 1=1 " + whrCls + ")"
            Case NavigatorType.Current
                qry += " and TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_No = '" + strDocNo + "'"
            Case NavigatorType.Next
                qry += " and TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_No = (select Min(Document_No) from TSPL_MILK_JOBWORK_TRANSFER_RETURN where Document_No>'" + strDocNo + "' " + whrCls + ")"
            Case NavigatorType.Previous
                qry += " and TSPL_MILK_JOBWORK_TRANSFER_RETURN.Document_No = (select Max(Document_No) from TSPL_MILK_JOBWORK_TRANSFER_RETURN where Document_No<'" + strDocNo + "' " + whrCls + ")"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            obj = New clsMilkJWOTransferReturn()
            obj.Document_No = clsCommon.myCstr(dt.Rows(0)("Document_No"))
            obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"))
            obj.JWO_Transfer_No = clsCommon.myCstr(dt.Rows(0)("JWO_Transfer_No"))
            obj.Remarks = clsCommon.myCstr(dt.Rows(0)("Remarks"))
        End If
        Return obj
    End Function
End Class
