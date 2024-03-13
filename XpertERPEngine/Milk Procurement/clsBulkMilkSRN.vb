Imports common
Imports System.Data.SqlClient
Public Class clsBulkMilkSRN
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public SRN_NO As String = String.Empty
    Public SRN_Date As Date = Nothing
    Public Weighment_No As String = String.Empty
    Public Weighment_Date As Date = Nothing
    Public QC_No As String = String.Empty
    Public Qc_Date As Date = Nothing
    Public Vendor_Code As String = String.Empty
    Public Loc_Code As String = String.Empty
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date = Nothing
    Public Tanker_No As String = String.Empty
    Public Price_Code As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public UOM As String = String.Empty
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Net_Weight_Calculate As Decimal = 0
    Public CLR_Per As Decimal = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public fat_Rate As Double = 0
    Public SNF_Rate As Double = 0
    Public Amount As Double = 0
    Public Deduction As Double = 0
    Public Incentive As Double = 0
    Public Actual_Amount As Double = 0
    Public SpecialDeduction As Double = 0
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public isNewEntry As Boolean = False
    Public Gate_Entry_No As String = String.Empty
    Public Standardrate As Double = 0
    Public NetRate As Double = 0
    Public BasicRate As Double = 0
    Public FatAmt As Double = 0
    Public SnfAmt As Double = 0
    Public FinalMilkRate As Double = 0
    Public PO_NO As String = ""
    Public PO_Date As String = ""
    Public isApproved As Integer = 0  ' Stores SRN Approved Status, if 1 then Approved
    Public Approval_Ref_Doc_No As String = ""
    Public Approved_Rate As Double = 0
    Public RemoveForceAapprovalofBulkSRN As Double = False
    Public arrObj As List(Of clsSRNParam) = Nothing
    Public Arr As List(Of clsBulkMilkSRNChemberNoDetails) = Nothing
    Public Gate_Entry_Type As String = Nothing
    Public Transport_Charges As Decimal
    Public Net_Weight_LTR As Double = 0
    Public Milk_Amount As Double = 0

    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(StrDocNo, formId, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim isTransLocallyInitiatted As Boolean = False
        Try

            Dim ApplyTransportChargeAddInActualAmount As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyTransportChargeAddInActualAmount, clsFixedParameterCode.ApplyTransportChargeAddInActualAmount, trans)) > 0)
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception(" Doc No not found to Post")
            End If

            If trans Is Nothing Then
                trans = clsDBFuncationality.GetTransactin()
                isTransLocallyInitiatted = True
            End If

            Dim obj As clsBulkMilkSRN = clsBulkMilkSRN.getData(StrDocNo, NavigatorType.Current, False, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.SRN_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkSRN, obj.Loc_Code, obj.SRN_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "tspl_bulk_milk_srn", "SRN_No", obj.SRN_NO, trans)
            If isResult = False Then
                If isTransLocallyInitiatted Then
                    trans.Commit()
                End If
                Return False
            End If

            Dim qry As String = ""
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
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


            Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, strItemUnitCode, trans)
            If ConvFac = 0 Then
                Throw New Exception("Conversion Factor found zero for item :" + obj.Item_Code + " and Uom:'" + strItemUnitCode)
            End If
            Dim strSiloNo As String = Nothing
            If TankerFromMaster = 0 Then
                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "I"
                '-----------Getting Sub Location Where Milk Was unloaded
                strSiloNo = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
                '-----------------------------------
                objInventoryMovemnt.Location_Code = strSiloNo
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
                If obj.Net_Weight_Calculate > 0 Then
                    objInventoryMovemnt.Fat_Amt = obj.FatAmt
                    objInventoryMovemnt.SNF_Amt = obj.SnfAmt
                Else
                    objInventoryMovemnt.Fat_Amt = obj.fat_Rate * obj.fat_KG
                    objInventoryMovemnt.SNF_Amt = obj.SNF_Rate * obj.SNF_KG
                End If
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave
                objInventoryMovemnt.Basic_Cost = obj.Actual_Amount / obj.Net_Weight

                If ApplyTransportChargeAddInActualAmount AndAlso obj.Transport_Charges > 0 Then
                    objInventoryMovemnt.DonNotCalculateAvgFATSNFCost = True
                    Dim dblTranportFATPart As Decimal = 0
                    If (objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt) > 0 Then
                        dblTranportFATPart = Math.Round(obj.Transport_Charges * ((objInventoryMovemnt.Fat_Amt) / (objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt)), 2)
                    End If
                    Dim dblTranportSNFPart As Decimal = obj.Transport_Charges - dblTranportFATPart

                    objInventoryMovemnt.Fat_Amt += dblTranportFATPart
                    objInventoryMovemnt.SNF_Amt += dblTranportSNFPart
                End If
                ArrInventoryMovement.Add(objInventoryMovemnt)
            Else

                Dim isCreateBulkProcPriceChartItemWise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, trans))

                ''richa agarwal 25 June,2019 add data for Batch Item New table when item is of Batch wise TEC/25/06/19-000566
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_Milk_unloading_Chember_Details.Line_No,TSPL_Milk_unloading_Chember_Details.Batch_No,TSPL_Milk_unloading_Chember_Details.Chamber_Qty ,TSPL_Milk_unloading_Chember_Details.UOM ,TSPL_Milk_unloading_Chember_Details.Item_Code    from TSPL_MILK_UNLOADING left join TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No =TSPL_Milk_unloading_Chember_Details.Unloading_No where  Gate_Entry_No='" & obj.Gate_Entry_No & "' and isnull(TSPL_Milk_unloading_Chember_Details.Batch_No ,'')<>'' and TSPL_Milk_unloading_Chember_Details.IsBatchWise ='Y'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim objBatchInvNew As New clsBatchInventoryNew
                    Dim arr As List(Of clsBatchInventoryNew) = Nothing

                    arr = New List(Of clsBatchInventoryNew)
                    For i As Integer = 0 To dt.Rows.Count - 1
                        objBatchInvNew = New clsBatchInventoryNew()
                        arr = New List(Of clsBatchInventoryNew)
                        objBatchInvNew.Parent_Line_No = clsCommon.myCstr(dt.Rows(i)("Line_No"))
                        objBatchInvNew.Batch_No = clsCommon.myCstr(dt.Rows(i)("Batch_No"))
                        If isCreateBulkProcPriceChartItemWise = 1 Then
                            strSiloNo = clsDBFuncationality.getSingleValue("select TSPL_Milk_unloading_Chember_Details.Sublocation_Code   from TSPL_MILK_UNLOADING left join " &
                                   "TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No =TSPL_Milk_unloading_Chember_Details.Unloading_No " &
                                   "where TSPL_Milk_unloading_Chember_Details.line_no='" & clsCommon.myCstr(dt.Rows(i)("Line_No")) & "' and Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
                        Else
                            strSiloNo = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
                        End If
                        objBatchInvNew.Qty = clsCommon.myCdbl(dt.Rows(i)("Chamber_Qty"))
                        objBatchInvNew.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                        objBatchInvNew.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                        objBatchInvNew.In_Out_Type = "I"
                        If clsCommon.myLen(objBatchInvNew.Batch_No) > 0 AndAlso objBatchInvNew.Qty <> 0 Then
                            arr.Add(objBatchInvNew)
                        End If
                        clsBatchInventoryNew.SaveData("BulkSRN", obj.SRN_NO, obj.SRN_Date, "I", clsCommon.myCstr(dt.Rows(i)("Item_Code")), strSiloNo, clsCommon.myCstr(dt.Rows(i)("Line_No")), 0, clsCommon.myCstr(dt.Rows(i)("UOM")), arr, trans)
                    Next

                End If
                For Each objTr As clsBulkMilkSRNChemberNoDetails In obj.Arr
                    strItemType = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                    Dim objInventoryMovemnt As New clsInventoryMovementNew()
                    objInventoryMovemnt.InOut = "I"
                    '-----------Getting Sub Location Where Milk Was unloaded
                    ' done by priti BHA/03/07/18-000123 for option of silo chamber wise with diff item
                    If isCreateBulkProcPriceChartItemWise = 1 Then
                        strSiloNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_Milk_unloading_Chember_Details.Sublocation_Code   from TSPL_MILK_UNLOADING left join " &
                                    "TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No =TSPL_Milk_unloading_Chember_Details.Unloading_No " &
                                    "where TSPL_Milk_unloading_Chember_Details.line_no='" & objTr.Line_No & "' and Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans))
                    Else
                        strSiloNo = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)

                    End If
                    '-----------------------------------
                    objInventoryMovemnt.Location_Code = strSiloNo
                    objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                    objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
                    objInventoryMovemnt.Item_Code = objTr.Item_Code
                    objInventoryMovemnt.Item_Desc = clsIntimation.getItemName(objTr.Item_Code, trans)
                    objInventoryMovemnt.Qty = objTr.Net_Weight
                    objInventoryMovemnt.UOM = objTr.UOM
                    objInventoryMovemnt.MRP = 0
                    objInventoryMovemnt.Add_Cost = 0
                    objInventoryMovemnt.FAT_Per = objTr.fat_per
                    objInventoryMovemnt.SNF_Per = objTr.snf_Per
                    objInventoryMovemnt.FAT_KG = objTr.fat_KG
                    objInventoryMovemnt.SNF_KG = objTr.SNF_KG
                    objInventoryMovemnt.Net_Cost = objTr.Actual_Amount
                    objInventoryMovemnt.main_location = obj.Loc_Code
                    objInventoryMovemnt.Fat_Rate = objTr.fat_Rate
                    objInventoryMovemnt.SNF_Rate = objTr.SNF_Rate
                    objInventoryMovemnt.Fat_Amt = objTr.fat_Rate * objTr.fat_KG
                    objInventoryMovemnt.SNF_Amt = objTr.SNF_Rate * objTr.SNF_KG
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    objInventoryMovemnt.Basic_Cost = objTr.Actual_Amount / objTr.Net_Weight

                    If ApplyTransportChargeAddInActualAmount AndAlso objTr.Transport_Charges > 0 Then
                        objInventoryMovemnt.DonNotCalculateAvgFATSNFCost = True
                        Dim dblTranportFATPart As Decimal = 0
                        If (objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt) > 0 Then
                            dblTranportFATPart = Math.Round(objTr.Transport_Charges * ((objInventoryMovemnt.Fat_Amt) / (objInventoryMovemnt.Fat_Amt + objInventoryMovemnt.SNF_Amt)), 2)
                        End If
                        Dim dblTranportSNFPart As Decimal = objTr.Transport_Charges - dblTranportFATPart

                        objInventoryMovemnt.Fat_Amt += dblTranportFATPart
                        objInventoryMovemnt.SNF_Amt += dblTranportSNFPart
                    End If

                    ArrInventoryMovement.Add(objInventoryMovemnt)
                Next
            End If


            isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("BulkSRN", obj.SRN_NO, obj.SRN_Date, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            'Create GL Entry
            isSaved = clsBulkMilkSRN.CreateTransferInJE(obj, "", trans)

            Dim strQry As String = " update tspl_bulk_milk_srn set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "', sub_location='" & strSiloNo & "' where srn_no='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, StrDocNo, "tspl_bulk_milk_srn", "SRN_NO", trans)
            CreateConsumptionEntry(obj, strSiloNo, trans)

            If Not clsApply_Approval.AllowNlevelonScreen(clsUserMgtCode.frmBulkMilkSRN, trans) Then
                Dim objApprov As New ClsTransactionApproval
                objApprov.Document_No = obj.SRN_NO
                objApprov.Doc_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt")
                objApprov.Approval_Type = "Rate"
                objApprov.Approve = 0
                objApprov.Program_Code = formId
                Dim qryp As String = "select Program_Name a   from TSPL_PROGRAM_MASTER where  Program_Code ='" & formId & "'"
                objApprov.Screen_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryp, trans))

                obj.RemoveForceAapprovalofBulkSRN = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RemoveForceAapprovalofBulkSRN, clsFixedParameterCode.RemoveForceAapprovalofBulkSRN, trans))
                If obj.RemoveForceAapprovalofBulkSRN = 0 Then
                    isPosted = ClsTransactionApproval.SaveData(objApprov, True, trans)
                Else
                    If obj.Gate_Entry_Type <> "J" Then
                        isPosted = ClsTransactionApproval.SaveData(objApprov, True, trans)
                        qry = "Update TSPL_TRANSACTION_APPROVAL set Approve=1  where Program_Code='" & formId & "' and Document_No='" & obj.SRN_NO & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        ' added by priti on GKD/08/06/18-000148
                        qry = "Update tspl_bulk_milk_srn set isApproved=1,Approved_Rate=" & obj.BasicRate & "  where  srn_no='" & obj.SRN_NO & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If

                Dim AllowJobWorkonGateEntryBulkProc As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, trans))
                Dim intJobWork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsAgainstJobWork from tspl_gate_entry_details where gate_entry_no='" & obj.Gate_Entry_No & "' ", trans))
                If AllowJobWorkonGateEntryBulkProc = 1 AndAlso intJobWork = 1 Then
                    isPosted = CreateMilkJobWorkTransfer(trans, obj)
                End If
                If isTransLocallyInitiatted Then
                    If isPosted Then
                        trans.Commit()
                    Else
                        trans.Rollback()
                    End If
                End If
            End If
            Return isPosted
        Catch ex As Exception
            If isTransLocallyInitiatted Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function CreateTransferInJE(obj As clsBulkMilkSRN, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
        Dim qry As String = ""
        Try


            If TankerFromMaster = 0 Then
                qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_Bulk_MILK_SRN.Actual_Amount,TSPL_Bulk_MILK_SRN.Loc_Code, TSPL_Bulk_MILK_SRN.Item_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN .Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.SRN_NO & "' "
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                        Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                        strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                        strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                        ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount"), "", "", "", "", "", "", "I"})
                        ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount") * -1, "", "", "", "", "", "", "Y"})
                        clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"), "Against Bulk Milk SRN No  -" + obj.SRN_NO + "", "BM-SR", "Bulk Milk SRN", obj.SRN_NO, "", "V", obj.Vendor_Code, clsVendorMaster.GetName(obj.Vendor_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.Vendor_Code & ", " & clsVendorMaster.GetName(obj.Vendor_Code, trans))
                        'clsInventoryMovement.UpdateInvControlAccount(obj.SRN_NO, "BulkSRN", dt.Rows(0)("Item_Code"), strInvCntrlAc, strPaybleClrAc, "I", trans)
                        clsInventoryMovement.UpdateInvControlAccount(obj.SRN_NO, "BulkSRN", dt.Rows(0)("Item_Code"), strInvCntrlAc, "", "I", trans)
                    End If
                End If
            Else
                Dim intCounter As Integer = 0
                Dim ItemDesc As String = Nothing
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dt As DataTable = Nothing
                For Each objTr As clsBulkMilkSRNChemberNoDetails In obj.Arr
                    intCounter += 1
                    qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount,TSPL_BULK_MILK_SRN.Loc_Code , TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Item_Code  from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_MASTER.Item_Code left outer join tspl_bulk_milk_srn on TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO=tspl_bulk_milk_srn.SRN_NO  where TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO='" & objTr.SRN_NO & "' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Item_Code='" & objTr.Item_Code & "' and TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc='" & objTr.Chamber_Desc & "' "
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                            Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                            strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                            strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                            ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount"), "", "", "", "", "", "", "I"})
                            ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount") * -1, "", "", "", "", "", "", "Y"})
                            ItemDesc = clsIntimation.getItemName(objTr.Item_Code, trans)
                        End If
                    End If
                Next
                '' BHA/30/10/18-000646 RICHA AGARWAL SEND vENDOR CODE AND VENDOR NAME INTO JOURNAL ENTRY AND TYPE V instead of C 30 Oct,2018
                If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoForRecreateOnly, trans, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"), "Against Bulk Milk SRN No  -" + obj.SRN_NO + "", "BM-SR", "Bulk Milk SRN", obj.SRN_NO, "", "V", obj.Vendor_Code, clsVendorMaster.GetName(obj.Vendor_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.Vendor_Code & ", " & clsVendorMaster.GetName(obj.Vendor_Code, trans))
                Else
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"), "Against Bulk Milk SRN No  -" + obj.SRN_NO + "", "BM-SR", "Bulk Milk SRN", obj.SRN_NO, "", "V", obj.Vendor_Code, clsVendorMaster.GetName(obj.Vendor_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.Vendor_Code & ", " & clsVendorMaster.GetName(obj.Vendor_Code, trans))
                End If
                If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                    'clsInventoryMovement.UpdateInvControlAccount(obj.SRN_NO, "BulkSRN", dt.Rows(0)("Item_Code"), dt.Rows(0)("Inv_Control_Account"), dt.Rows(0)("Inv_Payable_Clearing"), "I", trans)
                    clsInventoryMovement.UpdateInvControlAccount(obj.SRN_NO, "BulkSRN", dt.Rows(0)("Item_Code"), dt.Rows(0)("Inv_Control_Account"), "", "I", trans)
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateMilkJobWorkTransfer(ByVal trans As SqlTransaction, ByVal objSRN As clsBulkMilkSRN) As Boolean
        Dim totalqty As Decimal = 0
        Dim obj = New clsMilkJobworkTransfer()
        obj.isNewEntry = True
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
        Dim strSiloNo As String = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & objSRN.Gate_Entry_No & "'", trans)
        Dim strJobLoc As String = clsDBFuncationality.getSingleValue("select Sublocation_Code  from tspl_gate_entry_details where Gate_Entry_No='" & objSRN.Gate_Entry_No & "'", trans)
        Dim strJobVendor As String = clsDBFuncationality.getSingleValue("select Jobwork_Vendor  from tspl_location_master where Location_Code='" & strJobLoc & "'", trans)

        obj.SRN_NO = objSRN.SRN_NO
        obj.Document_Date = objSRN.SRN_Date
        obj.Virtual_location = strSiloNo
        obj.JobWork_location = strJobLoc
        obj.Gate_Entry_No = objSRN.Gate_Entry_No
        obj.Weighment_No = objSRN.Weighment_No
        obj.Weighment_Date = objSRN.Weighment_Date
        obj.Vendor_Code = strJobVendor
        obj.Loc_Code = objSRN.Loc_Code
        obj.Challan_No = objSRN.Challan_No
        obj.Challan_Date = objSRN.Challan_Date
        obj.Tanker_No = objSRN.Tanker_No
        obj.Price_Code = objSRN.Price_Code
        obj.QC_No = objSRN.QC_No
        obj.Qc_Date = objSRN.Qc_Date
        obj.isPosted = 0
        obj.Item_Code = objSRN.Item_Code
        obj.Item_Desc = objSRN.Item_Desc
        obj.UOM = objSRN.UOM
        obj.Gross_Weight = objSRN.Gross_Weight
        obj.Tare_Weight = objSRN.Tare_Weight
        obj.Net_Weight = objSRN.Net_Weight
        obj.snf_Per = objSRN.snf_Per
        obj.fat_per = objSRN.fat_per
        obj.fat_KG = objSRN.fat_KG
        obj.SNF_KG = objSRN.SNF_KG
        obj.fat_Rate = objSRN.fat_Rate
        obj.SNF_Rate = objSRN.SNF_Rate
        obj.Amount = objSRN.Amount
        obj.SpecialDeduction = objSRN.SpecialDeduction
        obj.Deduction = objSRN.Deduction
        obj.Incentive = objSRN.Incentive
        obj.Actual_Amount = objSRN.Actual_Amount
        obj.BasicRate = objSRN.BasicRate
        obj.Standardrate = objSRN.Standardrate
        obj.NetRate = objSRN.NetRate

        obj.FatAmt = objSRN.FatAmt
        obj.SnfAmt = objSRN.SnfAmt
        obj.FinalMilkRate = objSRN.FinalMilkRate

        obj.Modify_By = objCommonVar.CurrentUserCode
        obj.Modify_Date = objSRN.Modify_Date
        obj.comp_code = objCommonVar.CurrentCompanyCode
        obj.Doc_Type = "BulkProc"
        If obj.isNewEntry Then
            obj.Created_By = objCommonVar.CurrentUserCode
            obj.Created_Date = objSRN.Modify_Date
        End If
        If (TankerFromMaster = 1) Then

            obj.Arr = New List(Of clsMilkJobworkTransferDetails)
            For Each objSRNDetail As clsBulkMilkSRNChemberNoDetails In objSRN.Arr
                Dim objTr As New clsMilkJobworkTransferDetails()
                objTr.Line_No = objSRNDetail.Line_No
                objTr.Chamber_Desc = objSRNDetail.Chamber_Desc
                objTr.Item_Code = objSRNDetail.Item_Code
                objTr.UOM = objSRNDetail.Item_Code
                objTr.fat_per = objSRNDetail.fat_per
                objTr.snf_Per = objSRNDetail.snf_Per
                objTr.Gross_Weight = objSRNDetail.Gross_Weight
                objTr.Tare_Weight = objSRNDetail.Tare_Weight
                objTr.Net_Weight = objSRNDetail.Net_Weight
                objTr.snf_Per = objSRNDetail.snf_Per
                objTr.fat_per = objSRNDetail.fat_per
                objTr.fat_KG = objSRNDetail.fat_KG
                objTr.SNF_KG = objSRNDetail.SNF_KG
                objTr.fat_Rate = objSRNDetail.fat_Rate
                objTr.SNF_Rate = objSRNDetail.SNF_Rate
                objTr.Amount = objSRNDetail.Amount
                objTr.SpecialDeduction = objSRNDetail.SpecialDeduction
                objTr.Deduction = objSRNDetail.Deduction
                objTr.Incentive = objSRNDetail.Incentive
                objTr.Actual_Amount = objSRNDetail.Actual_Amount
                objTr.BasicRate = objSRNDetail.BasicRate
                objTr.Standardrate = objSRNDetail.Standardrate
                objTr.NetRate = objSRNDetail.NetRate
                objTr.FatAmt = objSRNDetail.FatAmt
                objTr.SnfAmt = objSRNDetail.SnfAmt
                objTr.FinalMilkRate = objSRNDetail.FinalMilkRate
                objTr.Price_Code = objSRNDetail.Price_Code
                objTr.MILK_GRADE_CODE = objSRNDetail.MILK_GRADE_CODE
                objTr.MIKL_TYPE_CODE = objSRNDetail.MIKL_TYPE_CODE
                objTr.fat_Qty = objSRNDetail.fat_Qty
                objTr.SNF_Qty = objSRNDetail.SNF_Qty
                objTr.TotalStandardQty = objSRNDetail.TotalStandardQty
                objTr.Incentive_Amt = objSRNDetail.Incentive_Amt
                objTr.Deduction_Amt = objSRNDetail.Deduction_Amt
                If (clsCommon.myLen(objTr.Chamber_Desc) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
        End If

        obj.arrObj = New List(Of clsMilkTransferParam)
        If objSRN.arrObj IsNot Nothing Then
            For Each objSRNparam As clsSRNParam In objSRN.arrObj
                Dim objParam As New clsMilkTransferParam()
                objParam.Parameter = objSRNparam.Parameter
                objParam.Lower_Range = objSRNparam.Lower_Range
                objParam.Upper_Range = objSRNparam.Upper_Range
                objParam.value = objSRNparam.value
                objParam.QCValue = objSRNparam.QCValue
                objParam.Incen_Deduc = objSRNparam.Incen_Deduc
                obj.arrObj.Add(objParam)
            Next

        End If

        If clsMilkJobworkTransfer.saveData(obj, trans) Then
            If clsMilkJobworkTransfer.postData(obj.Document_Code, "", trans) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Shared Function CreateConsumptionEntry(ByVal obj As clsBulkMilkSRN, ByVal strSiloNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String
        Dim dtItem As DataTable
        Try
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateConsumeEntry, clsFixedParameterCode.CreateConsumeEntry, trans)) > 0 Then
                qry = "select Item_Code,Item_Desc,Qty,UOM,convert(decimal(18,2), case when Qty=0 then 0 else Avg_Cost/Qty end) as Rate,Avg_Cost as Amount,Fat_Per,Fat_KG,SNF_Per, SNF_KG,Fat_Amt,SNF_Amt,Fat_Rate,Fat_Amt,SNF_Rate,SNF_Amt  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + obj.SRN_NO + "' and Trans_Type='BulkSRN'"
                dtItem = clsDBFuncationality.GetDataTable(qry, trans)
                If dtItem IsNot Nothing AndAlso dtItem.Rows.Count > 0 Then
                    ''Use in RM Consumption 
                    Dim objConsumbption As New ClsJobWorkRMConsum
                    objConsumbption.Trans_Type = "Out"
                    objConsumbption.Adjustment_Date = obj.SRN_Date
                    objConsumbption.Posting_Date = obj.SRN_Date
                    objConsumbption.EntryDateTime = obj.SRN_Date
                    objConsumbption.IsMilkType = 1
                    objConsumbption.Loc_Code = strSiloNo
                    objConsumbption.Loc_Desc = clsLocation.GetName(objConsumbption.Loc_Code, trans)
                    objConsumbption.MainLocationCode = obj.Loc_Code
                    objConsumbption.MainLocationDesc = clsLocation.GetName(objConsumbption.MainLocationCode, trans)

                    objConsumbption.Description = "Adjustment for Consum.Bulk Milk SRN No :" & obj.SRN_NO & ""
                    objConsumbption.Reference_Document = "BML-SRN-CONSUME"
                    objConsumbption.Document_No = obj.SRN_NO
                    objConsumbption.Arr = New List(Of ClsJobWorkRMConsumDetails)

                    For Each dr As DataRow In dtItem.Rows
                        Dim objConsumbptionTR As New ClsJobWorkRMConsumDetails()
                        objConsumbptionTR.Adjustment_Line_No = 1
                        objConsumbptionTR.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                        objConsumbptionTR.Item_Description = clsCommon.myCstr(dr("Item_Desc"))
                        objConsumbptionTR.Adjustment_Type = "BD"
                        objConsumbptionTR.Item_Quantity = clsCommon.myCdbl(dr("Qty"))
                        objConsumbptionTR.Item_Cost = clsCommon.myCdbl(dr("Amount"))
                        objConsumbptionTR.mrp = 0
                        objConsumbptionTR.Unit_Code = clsCommon.myCstr(dr("UOM"))
                        objConsumbptionTR.fat_pers = clsCommon.myCdbl(dr("Fat_Per"))
                        objConsumbptionTR.fat_kg = clsCommon.myCdbl(dr("Fat_KG"))
                        objConsumbptionTR.snf_pers = clsCommon.myCdbl(dr("SNF_Per"))
                        objConsumbptionTR.snf_kg = clsCommon.myCdbl(dr("SNF_KG"))
                        objConsumbptionTR.Unit_Cost = clsCommon.myCdbl(dr("Rate"))
                        objConsumbptionTR.fat_Rate = clsCommon.myCdbl(dr("Fat_Rate"))
                        objConsumbptionTR.fat_Amt = clsCommon.myCdbl(dr("Fat_Amt"))
                        objConsumbptionTR.snf_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                        objConsumbptionTR.snf_Amt = clsCommon.myCdbl(dr("SNF_Amt"))
                        objConsumbption.Arr.Add(objConsumbptionTR)
                    Next
                    objConsumbption.SaveData(objConsumbption, True, "", trans, "RM")
                    ClsJobWorkRMConsum.PostData(objConsumbption.Adjustment_No, "Store Adjustment", trans, "RM")
                    ''End of Use in RM Consumption 
                End If
            End If
        Catch ex As Exception
        Finally
            qry = Nothing
            dtItem = Nothing
        End Try

        Return True
    End Function


    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select tspl_bulk_milk_srn.Loc_Code,tspl_bulk_milk_srn.SRN_Date from tspl_bulk_milk_srn where SRN_NO='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(dt.Rows(0)("Loc_Code")), clsCommon.myCDate(dt.Rows(0)("SRN_Date")), trans)
            End If
            Dim arr As List(Of String) = New List(Of String)
            arr.Add(strDocNo)
            clsERPFuncationalityOLD.AddToHistory(arr, clsUserMgtCode.frmBulkMilkSRN, trans)
            Dim qry As String = ""
            Dim isDeleted As Boolean = True
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "TSPL_Bulk_MILK_SRN", "SRN_NO", "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS", "SRN_NO", trans)
            qry = "delete from TSPL_Bulk_MILK_SRN_Chember_Details where  srn_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_SRN_Parameter_Range_Detail where  srn_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from tspl_bulk_milk_srn where srn_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select tspl_bulk_milk_srn.SRN_NO as [SrnNo] ,tspl_bulk_milk_srn.SRN_Date as SRNDate ,tspl_bulk_milk_srn.Weighment_No as [Weighment No],tspl_bulk_milk_srn.gate_entry_no as [Gate Entry No] ,tspl_bulk_milk_srn.Weighment_Date as [Weighment Date] ,tspl_bulk_milk_srn.QC_No as [Qc No] ,tspl_bulk_milk_srn.Qc_Date as [Qc Date] ,tspl_bulk_milk_srn.Vendor_Code as [Vendor Code] ,tspl_bulk_milk_srn.Loc_Code as [Location Code] ,tspl_bulk_milk_srn.Challan_No as [Challan No] ,tspl_bulk_milk_srn.Challan_Date as [Challan Date] ,tspl_bulk_milk_srn.Tanker_No as [Tanker No] ,tspl_bulk_milk_srn.Price_Code as [Price Code] ,case when isnull(tspl_bulk_milk_srn.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,tspl_bulk_milk_srn.Posting_Date as [Posting Date] ,tspl_bulk_milk_srn.Item_Code as [Item Code] ,tspl_bulk_milk_srn.Item_Desc as [Item Desc] ,tspl_bulk_milk_srn.UOM as [Uom] ,tspl_bulk_milk_srn.Gross_Weight as [Gross Weight] ,tspl_bulk_milk_srn.Tare_Weight as [Tare Weight] ,tspl_bulk_milk_srn.Net_Weight as [Net Weight] ,tspl_bulk_milk_srn.snf_Per as [SNF Per] ,tspl_bulk_milk_srn.fat_per as [FAT Per] ,tspl_bulk_milk_srn.fat_KG as [FAT Kg] ,tspl_bulk_milk_srn.SNF_KG as [SNF Kg] ,tspl_bulk_milk_srn.fat_Rate as [FAT Rate] ,tspl_bulk_milk_srn.SNF_Rate as [SNF Rate] ,tspl_bulk_milk_srn.Amount as [Amount] ,tspl_bulk_milk_srn.Deduction as [Deduction] ,tspl_bulk_milk_srn.SpecialDeduction as [Special Deduction],tspl_bulk_milk_srn.Actual_Amount as [Actual Amount] ,tspl_bulk_milk_srn.Created_By as [Created By] ,tspl_bulk_milk_srn.Created_Date as [Created Date] ,tspl_bulk_milk_srn.Modify_By as [Modify By] ,tspl_bulk_milk_srn.Modify_Date as [Modify Date] ,tspl_bulk_milk_srn.comp_code as [Company Code]  From tspl_bulk_milk_srn "
            str = clsCommon.ShowSelectForm("BULKSRNFND", qry, "SrnNo", whrcls, curcode, "tspl_bulk_milk_srn.SRN_Date desc", isButtonClicked, "tspl_bulk_milk_srn.SRN_Date")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getWeighmentFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_Weighment_Detail.Weighment_No as [WeighmentNo] ,TSPL_Weighment_Detail.Weighment_date as [Weighment Date] ,TSPL_Weighment_Detail.Gate_Entry_No as [Gate Entry No] ,TSPL_Weighment_Detail.Doc_Type as [Doc Type] ,TSPL_Weighment_Detail.Date_And_Time as [Gate Entry Date And Time] ,TSPL_Weighment_Detail.Challan_No as [Challan No] ,TSPL_Weighment_Detail.Challan_Date as [Challan Date] ,TSPL_Weighment_Detail.Tanker_No as [Tanker No] ,case when isnull(TSPL_Weighment_Detail.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_Weighment_Detail.Posting_Date as [Posting Date]  ,TSPL_Weighment_Detail.location_Code as [Location Code] ,TSPL_Weighment_Detail.Location_Desc as [Location Desc] ,TSPL_Weighment_Detail.Vendor_Code as [Vendor Code] ,TSPL_Weighment_Detail.Vendor_Desc as [Vendor Desc] ,TSPL_Weighment_Detail.Item_Code as [Item Code] ,TSPL_Weighment_Detail.Item_Desc as [Item Desc] ,TSPL_Weighment_Detail.Qty_In_Kg as [Qty] ,TSPL_Weighment_Detail.snf_Per as [SNF(%)] ,TSPL_Weighment_Detail.fat_per as [FAT(%)] ,TSPL_Weighment_Detail.Created_By as [Created By] ,TSPL_Weighment_Detail.Created_Date as [Created Date] ,TSPL_Weighment_Detail.Modify_By as [Modify By] ,TSPL_Weighment_Detail.Modify_Date as [Modify Date] ,TSPL_Weighment_Detail.comp_code as [Company Code] ,TSPL_Weighment_Detail.Gross_Weight as [Gross Weight] ,TSPL_Weighment_Detail.Dip_Value as [Dip Value] ,TSPL_Weighment_Detail.Tare_Weight as [Tare Weight] ,TSPL_Weighment_Detail.Net_Weight as [Net Weight]   From TSPL_Weighment_Detail	 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Weighment_Detail.vendor_code "
            str = clsCommon.ShowSelectForm("WGHMNTFND", qry, "WeighmentNo", whrcls, curcode, "WeighmentNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function saveData(ByVal obj As clsBulkMilkSRN, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkSRN, obj.Loc_Code, obj.SRN_Date, trans)
            Dim issaved As Boolean = True
            Dim chk As Integer = 0
            chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from TSPL_Bulk_MILK_SRN where gate_entry_no='" & obj.Gate_Entry_No & "' and srn_no <>'" & obj.SRN_NO & "' and isnull(srn_return_no,'')='' ", trans))
            If chk > 0 Then
                Throw New Exception("The Same Tanker you have selected is Already used in other SRN.")
            End If
            'chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from tspl_bulk_milk_srn where srn_no='" & obj.SRN_NO & "' AND isnull(isPosted,0)=1"))
            'If chk = 1 Then
            '    Throw New Exception("The Document is Already Posted, Kindly Reload the document")
            'End If

            clsERPFuncationality.IsDocumentAlreadyPosted("tspl_bulk_milk_srn", "srn_no", obj.SRN_NO, "isPosted=1", trans)
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SRN_NO", clsCommon.myCstr(obj.SRN_NO))
            If clsCommon.myLen(obj.SRN_Date) > 0 Then
                If DateTime = "1" Then
                    clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy hh:mm:ss tt"), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"), True)
                End If

            End If
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No))
            If clsCommon.myLen(obj.Weighment_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm:ss tt "), True)
            End If
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", clsCommon.myCstr(obj.Gate_Entry_No))
            clsCommon.AddColumnsForChange(coll, "QC_No", clsCommon.myCstr(obj.QC_No))

            If clsCommon.myLen(obj.Qc_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Qc_Date", clsCommon.GetPrintDate(obj.Qc_Date, "dd/MMM/yyyy hh:mm:ss tt"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "Tanker_No", clsCommon.myCstr(obj.Tanker_No))
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", clsCommon.myCstr(obj.Vendor_Code))
            clsCommon.AddColumnsForChange(coll, "Loc_Code", clsCommon.myCstr(obj.Loc_Code))
            clsCommon.AddColumnsForChange(coll, "Challan_No", clsCommon.myCstr(obj.Challan_No))
            If clsCommon.myLen(obj.Challan_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(obj.Price_Code))
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"), True)
            End If
            clsCommon.AddColumnsForChange(coll, "isApproved", obj.isApproved)
            If obj.isApproved = 1 Then
                clsCommon.AddColumnsForChange(coll, "Approval_Ref_Doc_No", obj.Approval_Ref_Doc_No)
            End If
            If clsCommon.myLen(obj.PO_NO) > 0 Then
                clsCommon.AddColumnsForChange(coll, "PO_NO", obj.PO_NO)
                clsCommon.AddColumnsForChange(coll, "PO_Date", clsCommon.GetPrintDate(obj.PO_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(obj.Item_Code))
            clsCommon.AddColumnsForChange(coll, "Item_Desc", clsCommon.myCstr(obj.Item_Desc))
            clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(obj.UOM))
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", clsCommon.myCdbl(obj.Gross_Weight))
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", clsCommon.myCdbl(obj.Tare_Weight))
            clsCommon.AddColumnsForChange(coll, "Net_Weight", clsCommon.myCdbl(obj.Net_Weight))
            clsCommon.AddColumnsForChange(coll, "Net_Weight_Calculate", obj.Net_Weight_Calculate)

            clsCommon.AddColumnsForChange(coll, "Net_Weight_LTR", obj.Net_Weight_LTR)
            clsCommon.AddColumnsForChange(coll, "Milk_Amount", obj.Milk_Amount)
            clsCommon.AddColumnsForChange(coll, "CLR_Per", clsCommon.myCstr(obj.CLR_Per))
            clsCommon.AddColumnsForChange(coll, "snf_Per", clsCommon.myCstr(obj.snf_Per))
            clsCommon.AddColumnsForChange(coll, "FormType", "BulkMilkSRN")
            clsCommon.AddColumnsForChange(coll, "fat_per", clsCommon.myCdbl(obj.fat_per))
            clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(obj.SNF_KG))
            clsCommon.AddColumnsForChange(coll, "fat_KG", clsCommon.myCdbl(obj.fat_KG))
            clsCommon.AddColumnsForChange(coll, "fat_Rate", clsCommon.myCdbl(obj.fat_Rate))
            clsCommon.AddColumnsForChange(coll, "SNF_Rate", clsCommon.myCdbl(obj.SNF_Rate))
            clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(obj.Amount))
            clsCommon.AddColumnsForChange(coll, "Deduction", clsCommon.myCdbl(obj.Deduction))
            clsCommon.AddColumnsForChange(coll, "Incentive", clsCommon.myCdbl(obj.Incentive))
            clsCommon.AddColumnsForChange(coll, "Standardrate", clsCommon.myCdbl(obj.Standardrate))
            clsCommon.AddColumnsForChange(coll, "NetRate", clsCommon.myCdbl(obj.NetRate))
            clsCommon.AddColumnsForChange(coll, "BasicRate", clsCommon.myCdbl(obj.BasicRate))
            clsCommon.AddColumnsForChange(coll, "Transport_Charges", obj.Transport_Charges)
            clsCommon.AddColumnsForChange(coll, "FatAmt", clsCommon.myCdbl(obj.FatAmt))
            clsCommon.AddColumnsForChange(coll, "SnfAmt", clsCommon.myCdbl(obj.SnfAmt))
            clsCommon.AddColumnsForChange(coll, "FinalMilkRate", clsCommon.myCdbl(obj.FinalMilkRate))
            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            clsCommon.AddColumnsForChange(coll, "SpecialDeduction", clsCommon.myCdbl(obj.SpecialDeduction))
            ''===============================================
            clsCommon.AddColumnsForChange(coll, "Actual_Amount", clsCommon.myCdbl(obj.Actual_Amount))
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(obj.Modify_By))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.myCstr(obj.Modify_Date))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            If Not obj.isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.SRN_NO, "TSPL_Bulk_MILK_SRN", "SRN_NO", "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS", "SRN_NO", trans)
            End If

            If obj.isNewEntry OrElse isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "tspl_bulk_milk_srn_History", "tspl_bulk_milk_srn"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_milk_srn", OMInsertOrUpdate.Update, "tspl_bulk_milk_srn.srn_no='" + obj.SRN_NO + "'", trans)
            End If
            If TankerFromMaster = 0 Then
                If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                    issaved = issaved AndAlso clsSRNParam.saveData(obj.arrObj, obj.SRN_NO, trans)
                End If
            Else
                issaved = issaved AndAlso InsertParameterRange(obj, obj.Loc_Code, obj.QC_No, trans)
            End If

            issaved = issaved AndAlso clsBulkMilkSRNChemberNoDetails.SaveData(obj.SRN_NO, obj.Arr, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function InsertParameterRange(ByVal obj As clsBulkMilkSRN, ByVal strLocation As String, ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean

        Try
            Dim issaved As Boolean = True
            Dim whrCls As String = String.Empty
            If clsERPFuncationality.isLocationMcc(obj.Loc_Code, trans) Then
                whrCls = " and Param_for='MCC' or Param_for='BOTH'"
            Else
                whrCls = " and (Param_for='PLANT' or Param_for='BOTH')"
            End If

            Dim paramName As String = String.Empty
            Dim qry1 As String = " select code,Description from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
            Dim qry2 As String = String.Empty
            Dim qry3 As String = String.Empty
            Dim paramValue As Double = 0
            Dim intRow As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select distinct count(line_no) from TSPL_QUALITY_CHEMBER_DETAILS where QC_No='" & strQCNo & "'", trans))
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
            Dim objParam As New clsSRNParam
            obj.arrObj = New List(Of clsSRNParam)
            For ii As Integer = 0 To intRow - 1
                Dim strMilkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MIKL_TYPE_CODE from TSPL_QUALITY_CHEMBER_DETAILS where QC_No='" & strQCNo & "' and Line_No=" & ii + 1 & " ", trans))
                objParam = New clsSRNParam
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    qry2 = Nothing
                    qry3 = Nothing
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "' and line_No='" & ii + 1 & "'  and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3, trans))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy") & "' and loc_code='" & obj.Loc_Code & "'  and isReject=0  and MIKL_TYPE_CODE='" & strMilkType & "'    order by Effective_Date desc  "
                        If i <> dt1.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2, trans)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' ", trans))
                            objParam.Line_No = ii + 1
                            objParam.SRN_No = clsCommon.myCstr(obj.SRN_NO)
                            objParam.Parameter = clsCommon.myCstr(dt2.Rows(0)("Code"))
                            objParam.Lower_Range = clsCommon.myCstr(dt2.Rows(0)("Lower_Range"))
                            objParam.Upper_Range = clsCommon.myCstr(dt2.Rows(0)("Upper_Range"))
                            objParam.value = Nothing
                            objParam.QCValue = clsCommon.myCstr(dt2.Rows(0)("QCValue"))
                            objParam.Incen_Deduc = clsCommon.myCdbl(dt2.Rows(0)("Value"))
                            obj.arrObj.Add(objParam)
                            'gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If

                qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
                qry2 = ""
                Dim paramValue1 As String = ""
                Dim dt22 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
                If dt22 IsNot Nothing AndAlso dt22.Rows.Count > 0 Then
                    For i As Integer = 0 To dt22.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "'  and line_No='" & ii + 1 & "' and Param_Field_Code='" & clsCommon.myCstr(dt22.Rows(i)("Code")) & "'"
                        paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3, trans))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt22.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy") & "' and loc_code='" & obj.Loc_Code & "' and isReject=0  and MIKL_TYPE_CODE='" & strMilkType & "'    order by Effective_Date desc  "
                        If i <> dt22.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2, trans)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' ", trans))
                            objParam.Line_No = ii + 1
                            objParam.SRN_No = clsCommon.myCstr(obj.SRN_NO)
                            objParam.Parameter = clsCommon.myCstr(dt2.Rows(0)("Code"))
                            objParam.Lower_Range = Nothing
                            objParam.Upper_Range = Nothing
                            objParam.value = clsCommon.myCstr(dt2.Rows(0)("Condition_value"))
                            objParam.QCValue = clsCommon.myCstr(dt2.Rows(0)("QCValue"))
                            objParam.Incen_Deduc = clsCommon.myCdbl(dt2.Rows(0)("Value"))
                            obj.arrObj.Add(objParam)
                            'gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If


                qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
                qry2 = ""
                paramValue1 = ""
                Dim dt33 As DataTable = clsDBFuncationality.GetDataTable(qry1, trans)
                If dt33 IsNot Nothing AndAlso dt33.Rows.Count > 0 Then
                    For i As Integer = 0 To dt33.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "'  and line_No='" & ii + 1 & "' and Param_Field_Code='" & clsCommon.myCstr(dt33.Rows(i)("Code")) & "'"
                        paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3, trans))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt33.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy") & "' and loc_code='" & obj.Loc_Code & "' and isReject=0  and MIKL_TYPE_CODE='" & strMilkType & "'    order by Effective_Date desc  "
                        If i <> dt33.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2, trans)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' ", trans))
                            objParam.Line_No = ii + 1
                            objParam.SRN_No = clsCommon.myCstr(obj.SRN_NO)
                            objParam.Parameter = clsCommon.myCstr(dt2.Rows(0)("Code"))
                            objParam.Lower_Range = Nothing
                            objParam.Upper_Range = Nothing
                            objParam.value = clsCommon.myCstr(dt2.Rows(0)("status"))
                            objParam.QCValue = clsCommon.myCstr(dt2.Rows(0)("QCValue"))
                            objParam.Incen_Deduc = clsCommon.myCdbl(dt2.Rows(0)("Value"))
                            obj.arrObj.Add(objParam)
                            'gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If

            Next
            If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                issaved = issaved AndAlso clsSRNParam.saveData(obj.arrObj, obj.SRN_NO, trans)
                Return issaved
            End If
            Return issaved
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function

    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal blnDelete As Boolean) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select tspl_bulk_milk_srn.Loc_Code,tspl_bulk_milk_srn.SRN_Date from tspl_bulk_milk_srn where SRN_NO='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(dt.Rows(0)("Loc_Code")), clsCommon.myCDate(dt.Rows(0)("SRN_Date")), trans)
            End If
            Dim Qry As String = "select isPosted from TSPL_Bulk_MILK_SRN where SRN_NO='" + strCode + "'"
            'If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
            '    Throw New Exception("Transaction status should be posted for reverse and unpost")
            'End If

            Qry = "SELECT distinct DOC_NO FROM tspl_Bulk_milk_purchase_Invoice_Detail  where  SRN_NO='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current SRN is used in following invoice -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("DOC_NO"))
                Next
                Throw New Exception(Qry)
            End If

            Qry = "select top 1 SRN_Return_NO from TSPL_Bulk_Milk_SRN_Return  where  SRN_NO='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Qry = "Current SRN is used in following Bulk Milk Sale Return -"
                For Each dr As DataRow In dt.Rows
                    Qry += Environment.NewLine + clsCommon.myCstr(dr("SRN_Return_NO"))
                Next
                Throw New Exception(Qry)
            End If

            '' to check stock balance of qty
            Qry = "select Main_Location,Item_Code,Location_Code,Qty,UOM,Fat_KG,SNF_KG,Punching_Date from tspl_inventory_movement_new where Trans_Type='BulkSRN' and Source_Doc_No='" + strCode + "'"
            dt = clsDBFuncationality.GetDataTable(Qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim BalanceQty As Decimal
                    BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Item_Code")), clsCommon.myCstr(dr.Item("Main_Location")), clsCommon.myCstr(dr.Item("Location_Code")), strCode, clsCommon.myCDate(dr.Item("Punching_Date")), trans, clsCommon.myCstr(dr.Item("UOM")))
                    BalanceQty = Math.Round(Math.Round(BalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                    If clsCommon.myCdbl(dr.Item("Qty")) > BalanceQty Then
                        If Math.Abs(Math.Round(clsCommon.myCdbl(dr.Item("Qty")) - BalanceQty, 2, MidpointRounding.AwayFromZero)) > 0.01 Then
                            Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("Location_Code")) & " Available Qty: " & BalanceQty & "  Transaction Qty: " & clsCommon.myCdbl(dr.Item("Qty")) & " ")
                        End If
                    End If
                Next
            End If
            ''-----------------


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='BM-SR' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Dim GateEntryNo As String = clsDBFuncationality.getSingleValue("select isnull(Gate_Entry_No,'') from TSPL_Bulk_MILK_SRN where SRN_NO='" + strCode + "'", trans)
            If clsCommon.myLen(GateEntryNo) > 0 Then
                Dim SecondaryQCNo As String = clsDBFuncationality.getSingleValue("select isnull(Document_No,'') from TSPL_SECONDARY_SETTING_QC_HEAD where Gate_Entry_No='" + GateEntryNo + "'", trans)
                Qry = "delete from TSPL_SECONDARY_SETTING_QC_DETAIL where Document_No='" + SecondaryQCNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                Qry = "delete from TSPL_SECONDARY_SETTING_QC_HEAD where Document_No='" + SecondaryQCNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Dim strMilkRGP As String = clsDBFuncationality.getSingleValue("select top 1 rgp_no from TSPL_MILK_RGP_detail where Bulk_Milk_SRN_Code='" & strCode & "'", trans)
            If clsCommon.myLen(strMilkRGP) > 0 Then
                clsMilkRGPHead.ReverseAndUnpost(strMilkRGP, trans)
                Qry = "delete from TSPL_MILK_RGP_detail where rgp_no= '" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_MILK_RGP_HEAD where rgp_no='" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_MR_ISSUE_QC_DETAIL where Issue_Code='" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            Dim strMilkJobWorkTransfer As String = clsDBFuncationality.getSingleValue("select top 1 Document_Code from TSPL_MILK_JOBWORK_TRANSFER_HEAD where SRN_NO='" & strCode & "'", trans)
            If clsCommon.myLen(strMilkJobWorkTransfer) > 0 Then
                clsMilkJobworkTransfer.ReverseAndUnpost(strMilkJobWorkTransfer, trans)
            End If

            clsBatchInventoryNew.DeleteData("BulkSRN", strCode, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT_NEW", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + strCode + "' and Trans_Type='BulkSRN'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_TRANSACTION_APPROVAL where Program_Code='M-SRN-B' and Document_No='" & strCode & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)


            If blnDelete Then
                Qry = "delete from TSPL_SRN_Parameter_Range_Detail where SRN_NO='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_Bulk_MILK_SRN", "SRN_NO", "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS", "SRN_NO", trans)

                Qry = "delete from TSPL_BULK_MILK_SRN_CHEMBER_DETAILS where SRN_NO='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_Bulk_MILK_SRN where SRN_NO='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_Bulk_MILK_SRN", "SRN_NO", "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS", "SRN_NO", trans)
                Qry = "Update TSPL_Bulk_MILK_SRN set isPosted=0 where SRN_NO='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If



            Qry = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set is_reverse=1 where document_code='" + strCode + "' and trans_code='" + clsCommon.myCstr(clsUserMgtCode.frmBulkMilkSRN) + "' and is_reverse=0"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional isrntrade As Boolean = False, Optional ByVal trans As SqlTransaction = Nothing) As clsBulkMilkSRN
        Dim obj As New clsBulkMilkSRN
        Try
            ' Dim obj As New clsBulkMilkSRN
            Dim qst As String = " select tspl_bulk_milk_srn.* ,ISNULL(Tspl_Gate_Entry_Details.Gate_Entry_Type,'') AS Gate_Entry_Type  From tspl_bulk_milk_srn  LEFT OUTER JOIN Tspl_Gate_Entry_Details ON tspl_bulk_milk_srn.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No where 1=1  " & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ")

            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and Loc_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_bulk_milk_srn.srn_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_bulk_milk_srn.srn_no in (select min(srn_no ) from tspl_bulk_milk_srn where srn_no  >'" + strCode + "' " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
                Case NavigatorType.First
                    qst += " and tspl_bulk_milk_srn.srn_no in (select MIN(srn_no ) from tspl_bulk_milk_srn  where 1=1 " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
                Case NavigatorType.Last
                    qst += " and tspl_bulk_milk_srn.srn_no in (select Max(srn_no ) from tspl_bulk_milk_srn  where 1=1 " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
                Case NavigatorType.Previous
                    qst += " and tspl_bulk_milk_srn.srn_no in (select Max(srn_no ) from tspl_bulk_milk_srn where srn_No  <'" + strCode + "'  " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.SRN_NO = clsCommon.myCstr(dt.Rows(0)("SRN_NO"))
                obj.SRN_Date = clsCommon.myCDate(dt.Rows(0)("srn_date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("gate_entry_no"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                If Not isrntrade Then
                    obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                Else
                    obj.Weighment_Date = Nothing
                End If
                'obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                If Not isrntrade Then
                    obj.Qc_Date = clsCommon.myCDate(dt.Rows(0)("qc_date"), "dd/MMM/yyyy hh:mm:ss tt")
                Else
                    obj.Qc_Date = Nothing
                End If

                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))


                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If

                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Loc_Code = clsCommon.myCstr(dt.Rows(0)("Loc_code"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("challan_no"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"), "dd/MMM/yyyy")
                obj.Price_Code = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("item_code"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("uom"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
                obj.Net_Weight_Calculate = clsCommon.myCdbl(dt.Rows(0)("Net_Weight_Calculate"))
                obj.CLR_Per = clsCommon.myCdbl(dt.Rows(0)("CLR_Per"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.fat_KG = clsCommon.myCdbl(dt.Rows(0)("fat_KG"))
                obj.SNF_KG = clsCommon.myCdbl(dt.Rows(0)("SNF_KG"))
                obj.fat_Rate = clsCommon.myCdbl(dt.Rows(0)("fat_Rate"))
                obj.SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("SNF_Rate"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.Deduction = clsCommon.myCdbl(dt.Rows(0)("Deduction"))
                obj.Incentive = clsCommon.myCdbl(dt.Rows(0)("Incentive"))
                obj.Actual_Amount = clsCommon.myCdbl(dt.Rows(0)("Actual_Amount"))
                obj.Net_Weight_LTR = clsCommon.myCdbl(dt.Rows(0)("Net_Weight_LTR"))
                obj.Milk_Amount = clsCommon.myCdbl(dt.Rows(0)("Milk_Amount"))
                ''richa Against Ticket No.BM00000003719 on 04/09/2014
                obj.SpecialDeduction = clsCommon.myCdbl(dt.Rows(0)("SpecialDeduction"))
                ''=======================================
                obj.Standardrate = clsCommon.myCdbl(dt.Rows(0)("Standardrate"))
                obj.NetRate = clsCommon.myCdbl(dt.Rows(0)("NetRate"))
                obj.BasicRate = clsCommon.myCdbl(dt.Rows(0)("BasicRate"))

                obj.FatAmt = clsCommon.myCdbl(dt.Rows(0)("FatAmt"))
                obj.SnfAmt = clsCommon.myCdbl(dt.Rows(0)("SnfAmt"))
                obj.FinalMilkRate = clsCommon.myCdbl(dt.Rows(0)("FinalMilkRate"))
                obj.isApproved = clsCommon.myCdbl(dt.Rows(0)("isApproved"))
                obj.Transport_Charges = clsCommon.myCdbl(dt.Rows(0)("Transport_Charges"))
                If obj.isApproved = 1 Then
                    obj.Approval_Ref_Doc_No = clsCommon.myCstr(dt.Rows(0)("Approval_Ref_Doc_No"))
                    obj.Approved_Rate = clsCommon.myCdbl(dt.Rows(0)("Approved_Rate"))
                End If
                If clsCommon.myLen(dt.Rows(0)("PO_NO")) > 0 Then
                    obj.PO_NO = clsCommon.myCstr(dt.Rows(0)("PO_NO"))
                    obj.PO_Date = clsCommon.GetPrintDate(dt.Rows(0)("PO_Date"), "dd/MMM/yyyy")
                End If
                obj.Gate_Entry_Type = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_Type"))
                ' Return obj
                obj.arrObj = clsSRNParam.getData(obj.SRN_NO, trans)
                obj.Arr = clsBulkMilkSRNChemberNoDetails.GetData(obj.SRN_NO, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    Public Shared Function GetSRNNoBySampleNo(strSampleDocNo As String, intSampleNo As Integer) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DOC_CODE from TSPL_MILK_SRN_HEAD where MILK_SAMPLE_CODE='" + strSampleDocNo + "' and SAMPLE_NO='" + clsCommon.myCstr(intSampleNo) + "' "))
    End Function

    Public Shared Function GetPriceCodeBySampleNo(strSampleDocNo As String, intSampleNo As Integer) As String
        Return clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Price_Code from TSPL_MILK_SAMPLE_DETAIL where DOC_CODE='" + strSampleDocNo + "' and SAMPLE_NO='" + clsCommon.myCstr(intSampleNo) + "' "))
    End Function
End Class
Public Class clsBulkMilkSRNChemberNoDetails
#Region "Variables"
    Public fat_Qty As Double = 0
    Public SNF_Qty As Double = 0
    Public TotalStandardQty As Double = 0
    Public Incentive_Amt As Double = 0
    Public Deduction_Amt As Double = 0
    Public SRN_NO As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public UOM_Calculate As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Integer = 0
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Net_Weight_Calculate As Decimal = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public fat_KG As Double = 0
    Public SNF_KG As Double = 0
    Public fat_Rate As Double = 0
    Public SNF_Rate As Double = 0
    Public Amount As Double = 0
    Public Deduction As Double = 0
    Public Incentive As Double = 0
    Public Actual_Amount As Double = 0
    Public SpecialDeduction As Double = 0
    Public Standardrate As Double = 0
    Public NetRate As Double = 0
    Public BasicRate As Double = 0
    Public FatAmt As Double = 0
    Public SnfAmt As Double = 0
    Public FinalMilkRate As Double = 0
    Public Price_Code As String = Nothing
    Public MIKL_TYPE_CODE As String = Nothing
    Public MILK_GRADE_CODE As String = Nothing
    Public Net_Weight_LTR As Double = 0
    Public Transport_Charges As Decimal = 0
    Public Milk_Amount As Decimal = 0
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsBulkMilkSRNChemberNoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_Bulk_MILK_SRN_Chember_Details where SRN_NO='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsBulkMilkSRNChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "SRN_NO", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "fat_Qty", obj.fat_Qty)
                clsCommon.AddColumnsForChange(coll, "SNF_Qty", obj.SNF_Qty)
                clsCommon.AddColumnsForChange(coll, "TotalStandardQty", obj.TotalStandardQty)
                clsCommon.AddColumnsForChange(coll, "Incentive_Amt", obj.Incentive_Amt)
                clsCommon.AddColumnsForChange(coll, "Deduction_Amt", obj.Deduction_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(obj.Item_Code))
                clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(obj.UOM))
                clsCommon.AddColumnsForChange(coll, "UOM_Calculate", obj.UOM_Calculate, True)
                clsCommon.AddColumnsForChange(coll, "Gross_Weight", clsCommon.myCdbl(obj.Gross_Weight))
                clsCommon.AddColumnsForChange(coll, "Tare_Weight", clsCommon.myCdbl(obj.Tare_Weight))
                clsCommon.AddColumnsForChange(coll, "Net_Weight", clsCommon.myCdbl(obj.Net_Weight))
                clsCommon.AddColumnsForChange(coll, "Net_Weight_Calculate", obj.Net_Weight_Calculate)
                clsCommon.AddColumnsForChange(coll, "snf_Per", clsCommon.myCstr(obj.snf_Per))
                clsCommon.AddColumnsForChange(coll, "fat_per", clsCommon.myCdbl(obj.fat_per))
                clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(obj.SNF_KG))
                clsCommon.AddColumnsForChange(coll, "fat_KG", clsCommon.myCdbl(obj.fat_KG))
                clsCommon.AddColumnsForChange(coll, "fat_Rate", clsCommon.myCdbl(obj.fat_Rate))
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", clsCommon.myCdbl(obj.SNF_Rate))
                clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(obj.Amount))
                clsCommon.AddColumnsForChange(coll, "SpecialDeduction", clsCommon.myCdbl(obj.SpecialDeduction))
                clsCommon.AddColumnsForChange(coll, "Deduction", clsCommon.myCdbl(obj.Deduction))
                clsCommon.AddColumnsForChange(coll, "Incentive", clsCommon.myCdbl(obj.Incentive))
                clsCommon.AddColumnsForChange(coll, "BasicRate", clsCommon.myCdbl(obj.BasicRate))
                clsCommon.AddColumnsForChange(coll, "Standardrate", clsCommon.myCdbl(obj.Standardrate))
                clsCommon.AddColumnsForChange(coll, "NetRate", clsCommon.myCdbl(obj.NetRate))
                clsCommon.AddColumnsForChange(coll, "FatAmt", clsCommon.myCdbl(obj.FatAmt))
                clsCommon.AddColumnsForChange(coll, "SnfAmt", clsCommon.myCdbl(obj.SnfAmt))
                clsCommon.AddColumnsForChange(coll, "FinalMilkRate", clsCommon.myCdbl(obj.FinalMilkRate))
                clsCommon.AddColumnsForChange(coll, "Actual_Amount", clsCommon.myCdbl(obj.Actual_Amount))
                clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(obj.Price_Code), True)
                clsCommon.AddColumnsForChange(coll, "MILK_GRADE_CODE", clsCommon.myCstr(obj.MILK_GRADE_CODE), True)
                clsCommon.AddColumnsForChange(coll, "MIKL_TYPE_CODE", clsCommon.myCstr(obj.MIKL_TYPE_CODE), True)
                clsCommon.AddColumnsForChange(coll, "Net_Weight_LTR", clsCommon.myCdbl(obj.Net_Weight_LTR))
                clsCommon.AddColumnsForChange(coll, "Transport_Charges", clsCommon.myCdbl(obj.Transport_Charges))
                clsCommon.AddColumnsForChange(coll, "Milk_Amount", clsCommon.myCdbl(obj.Milk_Amount))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_MILK_SRN_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsBulkMilkSRNChemberNoDetails)
        Dim arr As List(Of clsBulkMilkSRNChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_Bulk_MILK_SRN_Chember_Details where TSPL_Bulk_MILK_SRN_Chember_Details.SRN_NO='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsBulkMilkSRNChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsBulkMilkSRNChemberNoDetails = New clsBulkMilkSRNChemberNoDetails()
                obj.SRN_NO = clsCommon.myCstr(dr("SRN_NO"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.UOM_Calculate = clsCommon.myCstr(dr("UOM_Calculate"))
                obj.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dr("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dr("Net_Weight"))
                obj.Net_Weight_Calculate = clsCommon.myCdbl(dr("Net_Weight_Calculate"))
                obj.fat_per = clsCommon.myCdbl(dr("fat_per"))
                obj.snf_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.fat_KG = clsCommon.myCdbl(dr("fat_KG"))
                obj.SNF_KG = clsCommon.myCdbl(dr("SNF_KG"))
                obj.fat_Rate = clsCommon.myCdbl(dr("fat_Rate"))
                obj.SNF_Rate = clsCommon.myCdbl(dr("SNF_Rate"))
                obj.Amount = clsCommon.myCdbl(dr("Amount"))
                obj.SpecialDeduction = clsCommon.myCdbl(dr("SpecialDeduction"))
                obj.Incentive = clsCommon.myCdbl(dr("Incentive"))
                obj.Deduction = clsCommon.myCdbl(dr("Deduction"))
                obj.BasicRate = clsCommon.myCdbl(dr("BasicRate"))
                obj.NetRate = clsCommon.myCdbl(dr("NetRate"))
                obj.Standardrate = clsCommon.myCdbl(dr("Standardrate"))
                obj.FatAmt = clsCommon.myCdbl(dr("FatAmt"))
                obj.SnfAmt = clsCommon.myCdbl(dr("SnfAmt"))
                obj.FinalMilkRate = clsCommon.myCdbl(dr("FinalMilkRate"))
                obj.Actual_Amount = clsCommon.myCdbl(dr("Actual_Amount"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                obj.Price_Code = clsCommon.myCstr(dr("Price_Code"))
                obj.MIKL_TYPE_CODE = clsCommon.myCstr(dr("MIKL_TYPE_CODE"))
                obj.MILK_GRADE_CODE = clsCommon.myCstr(dr("MILK_GRADE_CODE"))
                obj.fat_Qty = clsCommon.myCdbl(dr("fat_Qty"))
                obj.SNF_Qty = clsCommon.myCdbl(dr("SNF_Qty"))
                obj.TotalStandardQty = clsCommon.myCdbl(dr("TotalStandardQty"))
                obj.Incentive_Amt = clsCommon.myCdbl(dr("Incentive_Amt"))
                obj.Deduction_Amt = clsCommon.myCdbl(dr("Deduction_Amt"))
                obj.Net_Weight_LTR = clsCommon.myCdbl(dr("Net_Weight_LTR"))
                obj.Transport_Charges = clsCommon.myCdbl(dr("Transport_Charges"))
                obj.Milk_Amount = clsCommon.myCdbl(dr("Milk_Amount"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class
Public Class clsSRNParam
    Public Line_No As Integer = 0
    Public SRN_No As String = String.Empty
    Public Parameter As String = String.Empty
    Public Lower_Range As String = String.Empty
    Public Upper_Range As String = String.Empty
    Public value As String = String.Empty
    Public Incen_Deduc As Double = 0
    Public QCValue As String = String.Empty


    Public Shared Function saveData(ByVal arrObj As List(Of clsSRNParam), ByVal strSRNNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                Dim qry As String = "delete from TSPL_SRN_Parameter_Range_Detail where SRN_No='" & strSRNNo & "'"
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As clsSRNParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
                    clsCommon.AddColumnsForChange(coll, "Parameter", obj.Parameter)
                    clsCommon.AddColumnsForChange(coll, "Lower_Range", obj.Lower_Range)
                    clsCommon.AddColumnsForChange(coll, "Upper_Range", obj.Upper_Range)
                    clsCommon.AddColumnsForChange(coll, "value", obj.value)
                    clsCommon.AddColumnsForChange(coll, "Incen_Deduc", obj.Incen_Deduc)
                    clsCommon.AddColumnsForChange(coll, "QCValue", obj.QCValue)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SRN_Parameter_Range_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strSRNNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsSRNParam)
        Dim arrObj As List(Of clsSRNParam) = Nothing
        Try
            Dim obj As clsSRNParam = Nothing
            Dim qry As String = "select * from TSPL_SRN_Parameter_Range_Detail where SRN_No='" & strSRNNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsSRNParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsSRNParam()
                    obj.SRN_No = clsCommon.myCstr(dt.Rows(i)("SRN_No"))
                    obj.Parameter = clsCommon.myCstr(dt.Rows(i)("Parameter"))
                    obj.Lower_Range = clsCommon.myCstr(dt.Rows(i)("Lower_Range"))
                    obj.Upper_Range = clsCommon.myCstr(dt.Rows(i)("Upper_Range"))
                    obj.value = clsCommon.myCstr(dt.Rows(i)("value"))
                    obj.QCValue = clsCommon.myCstr(dt.Rows(i)("QCValue"))
                    obj.Incen_Deduc = clsCommon.myCdbl(dt.Rows(i)("Incen_Deduc"))
                    arrObj.Add(obj)
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arrObj
    End Function
    Public Shared Function deleteData(ByVal strSRNNo As String) As Boolean
        Try
            Dim isDeleted As Boolean = True
            Dim qry As String = "delete from TSPL_SRN_Parameter_Range_Detail where SRN_No='" & strSRNNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsBulkMilkSRNReturn
    Public SRN_Return_NO As String = String.Empty
    Public SRN_Return_Date As Date = Nothing
    Public SRN_NO As String = String.Empty
    Public Created_By As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public isNewEntry As Boolean = False
    Public Shared Function postData(ByVal StrDocNo As String, ByVal StrSRNReturnNo As String, ByVal trans As SqlTransaction) As Boolean

        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            Dim obj As clsBulkMilkSRN = clsBulkMilkSRN.getData(StrDocNo, NavigatorType.Current, False, trans)
            Dim objReturn As clsBulkMilkSRNReturn = clsBulkMilkSRNReturn.getData(StrSRNReturnNo, NavigatorType.Current, trans)
            Dim qry As String = ""

            ''richa agarwal 25 June,2019 add data for Batch Item New table when item is of Batch wise TEC/25/06/19-000566
            Dim dtSRN As DataTable = clsDBFuncationality.GetDataTable("select Parent_Line_No ,Batch_No ,Item_Code ,Qty,UOM ,Location_Code  from TSPL_BATCH_ITEM_new where Document_code='" & StrDocNo & "' and Document_Type='BulkSRN' ", trans)
            If dtSRN IsNot Nothing AndAlso dtSRN.Rows.Count > 0 Then
                Dim objBatchInvNew As New clsBatchInventoryNew
                Dim arr As List(Of clsBatchInventoryNew) = Nothing

                arr = New List(Of clsBatchInventoryNew)
                For i As Integer = 0 To dtSRN.Rows.Count - 1
                    objBatchInvNew = New clsBatchInventoryNew()
                    arr = New List(Of clsBatchInventoryNew)
                    objBatchInvNew.Parent_Line_No = clsCommon.myCstr(dtSRN.Rows(i)("Parent_Line_No"))
                    objBatchInvNew.Batch_No = clsCommon.myCstr(dtSRN.Rows(i)("Batch_No"))
                    objBatchInvNew.Qty = clsCommon.myCdbl(dtSRN.Rows(i)("Qty"))
                    objBatchInvNew.UOM = clsCommon.myCstr(dtSRN.Rows(i)("UOM"))
                    objBatchInvNew.Item_Code = clsCommon.myCstr(dtSRN.Rows(i)("Item_Code"))
                    objBatchInvNew.In_Out_Type = "O"
                    If clsCommon.myLen(objBatchInvNew.Batch_No) > 0 AndAlso objBatchInvNew.Qty <> 0 Then
                        arr.Add(objBatchInvNew)
                    End If
                    clsBatchInventoryNew.SaveData("BulkSRNRet", objReturn.SRN_Return_NO, objReturn.SRN_Return_Date, "O", clsCommon.myCstr(dtSRN.Rows(i)("Item_Code")), clsCommon.myCstr(dtSRN.Rows(i)("Location_Code")), clsCommon.myCstr(dtSRN.Rows(i)("Parent_Line_No")), 0, clsCommon.myCstr(dtSRN.Rows(i)("UOM")), arr, trans)
                Next

            End If


            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            If obj.IsAgainstJobWork = 0 Then
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
                Dim strItemUnitCode As String = obj.UOM
                Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, strItemUnitCode, trans)
                If ConvFac = 0 Then
                    Throw New Exception("Conversion Factor found zero for item :" + obj.Item_Code + " and Uom:'" + strItemUnitCode)
                End If
                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "O"
                '-----------Getting Sub Location Where Milk Was unloaded
                Dim strSiloNo As String = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
                '-----------------------------------
                objInventoryMovemnt.Location_Code = strSiloNo
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
                objInventoryMovemnt.Fat_Rate = obj.fat_Rate
                objInventoryMovemnt.SNF_Rate = obj.SNF_Rate
                objInventoryMovemnt.Fat_Amt = obj.FatAmt
                objInventoryMovemnt.SNF_Amt = obj.SnfAmt
                objInventoryMovemnt.Net_Cost = obj.Actual_Amount
                objInventoryMovemnt.main_location = obj.Loc_Code
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
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("BulkSRNRet", objReturn.SRN_Return_NO, objReturn.SRN_Return_Date, clsCommon.GetPrintDate(objReturn.SRN_Return_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            End If
            'Create GL Entry

            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                qry = " select TSPL_Bulk_MILK_SRN .Item_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,TSPL_Bulk_MILK_SRN.Actual_Amount,TSPL_Bulk_MILK_SRN.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join TSPL_Bulk_MILK_SRN on TSPL_Bulk_MILK_SRN .Item_Code=TSPL_ITEM_MASTER.Item_Code where TSPL_Bulk_MILK_SRN.SRN_NO='" & obj.SRN_NO & "' "
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                    Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                    strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                    strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)

                    ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount") * -1, "", "", "", "", "", "", "I"})
                    ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount"), "", "", "", "", "", "", "Y"})
                    '' BHA/30/10/18-000646 RICHA AGARWAL SEND vENDOR CODE AND VENDOR NAME INTO JOURNAL ENTRY AND TYPE V instead of C 30 Oct,2018
                    clsJournalMaster.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(objReturn.SRN_Return_Date, "dd/MMM/yyyy"), "Against Bulk Milk SRN Return No  -" + objReturn.SRN_Return_NO + "", "SR-RT", "Bulk Milk SRN Returm", objReturn.SRN_Return_NO, "", "V", obj.Vendor_Code, clsVendorMaster.GetName(obj.Vendor_Code, trans), objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.Vendor_Code & ", " & clsVendorMaster.GetName(obj.Vendor_Code, trans))
                    'Ticket No- TEC/12/03/19-000442 sanjay
                    clsInventoryMovement.UpdateInvControlAccount(StrSRNReturnNo, "BulkSRNRet", dt.Rows(0)("Item_Code"), "", strInvCntrlAc, "O", trans)
                End If
            End If
            Dim strQry As String = " update tspl_bulk_milk_srn set SRN_Return_NO='" & objReturn.SRN_Return_NO & "' where srn_no='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            strQry = " delete from TSPL_TRANSACTION_APPROVAL where Document_No='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            strQry = " update TSPL_Bulk_Milk_SRN_Return set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where SRN_Return_NO='" & objReturn.SRN_Return_NO & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            Dim AllowJobWorkonGateEntryBulkProc As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowJobWorkonGateEntryBulkProc, clsFixedParameterCode.AllowJobWorkonGateEntryBulkProc, trans))
            Dim intJobWork As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsAgainstJobWork from tspl_gate_entry_details where gate_entry_no='" & obj.Gate_Entry_No & "' ", trans))
            If AllowJobWorkonGateEntryBulkProc = 1 AndAlso intJobWork = 1 Then
                ' obj.SRN_Date = objReturn.SRN_Return_Date
                isPosted = CreateJobWorkTransferMilkReturn(objReturn, obj.Joblocation_Code, trans)
            End If
            Return isPosted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function CreateJobWorkTransferMilkReturn(ByVal objSrnReturn As clsBulkMilkSRNReturn, ByVal JobLocation_Code As String, ByVal trans As SqlTransaction) As Boolean
        Dim obj = New clsMilkJWOTransferReturn()
        Dim isNewEntry As Boolean = True
        obj.Document_Date = objSrnReturn.SRN_Return_Date
        obj.Remarks = "Auto Return From Bulk Milk SRN Return(job work Type)"
        obj.JWO_Transfer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_MILK_JOBWORK_TRANSFER_HEAD WHERE SRN_NO='" + objSrnReturn.SRN_NO + "'", trans))
        obj.JWO_SRN_From_Location_Code = JobLocation_Code
        obj.SRN_Return_NO = objSrnReturn.SRN_Return_NO
        ''For Custom Fields
        ' obj.arrCustomFields = New List(Of clsCustomFieldValues)
        If obj.SaveData(obj, isNewEntry, trans) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function saveData(ByVal objReturn As clsBulkMilkSRNReturn, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim obj As clsBulkMilkSRN = clsBulkMilkSRN.getData(objReturn.SRN_NO, NavigatorType.Current, False, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SRN_NO", objReturn.SRN_NO)
            clsCommon.AddColumnsForChange(coll, "SRN_Return_NO", objReturn.SRN_Return_NO)
            If clsCommon.myLen(objReturn.SRN_Return_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_Return_Date", clsCommon.GetPrintDate(objReturn.SRN_Return_Date, "dd/MMM/yyyy"), True)
            End If
            objReturn.isNewEntry = True
            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans)))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(objCommonVar.CurrentCompanyCode))
            clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.myCstr(clsCommon.GETSERVERDATE(trans)))
            ''richa BHA/12/11/18-000668 13 Nov,2018
            clsCommon.AddColumnsForChange(coll, "isPosted", 0)
            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Milk_SRN_Return", OMInsertOrUpdate.Insert, "", trans)
            issaved = issaved And postData(objReturn.SRN_NO, objReturn.SRN_Return_NO, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsBulkMilkSRNReturn
        Dim obj As New clsBulkMilkSRNReturn
        Try

            Dim qst As String = " select *   From TSPL_Bulk_Milk_SRN_Return   where SRN_Return_NO='" & strCode & "'"


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.SRN_Return_NO = clsCommon.myCstr(dt.Rows(0)("SRN_Return_NO"))
                obj.SRN_Return_Date = clsCommon.myCDate(dt.Rows(0)("SRN_Return_Date"), "dd/MMM/yyyy")
                obj.SRN_NO = clsCommon.myCstr(dt.Rows(0)("SRN_NO"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function



End Class




