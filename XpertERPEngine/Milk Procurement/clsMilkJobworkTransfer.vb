Imports common
Imports System.Data.SqlClient
Public Class clsMilkJobworkTransfer
    Public Manual_Standard_Rate As Double = 0
    Public Doc_Type As String = Nothing
    Public Milk_Transfer_In As String = Nothing
    Public Document_Code As String = Nothing
    Public Document_Date As Date = Nothing
    Public Virtual_location As String = Nothing
    Public JobWork_location As String = Nothing
    Public SRN_NO As String = String.Empty
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
    Public arrObj As List(Of clsMilkTransferParam) = Nothing
    Public Arr As List(Of clsMilkJobworkTransferDetails) = Nothing
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isSaved As Boolean = True
        Dim isTransLocallyInitiatted As Boolean = False
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception(" Doc No not found to Post")
            End If
            If trans Is Nothing Then
                trans = clsDBFuncationality.GetTransactin()
                isTransLocallyInitiatted = True
            End If
            Dim obj As clsMilkJobworkTransfer = clsMilkJobworkTransfer.getData(StrDocNo, NavigatorType.Current, False, trans)
            If (obj Is Nothing) Then
                Throw New Exception("No Data found to Post")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Transfer", obj.Loc_Code, obj.Document_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", obj.Document_Code, trans)
            If isResult = False Then
                If isTransLocallyInitiatted Then
                    trans.Commit()
                End If
                Return False
            End If
            Dim allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.allowMilkJWOutowordWithAvgFatSNFPerAtInventory, clsFixedParameterCode.allowMilkJWOutowordWithAvgFatSNFPerAtInventory, trans)) = 1)
            HitInventoryMovement(obj, allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory, trans)
            CreateTransferOutJE(obj, trans, "", allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory)

            Dim strQry As String = " update TSPL_MILK_JOBWORK_TRANSFER_HEAD set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Document_Code='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(StrDocNo), "TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", trans)
            If isTransLocallyInitiatted Then
                If isPosted Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If
        Catch ex As Exception
            If isTransLocallyInitiatted Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function HitInventoryMovement(ByVal obj As clsMilkJobworkTransfer, ByVal allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory As Boolean, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = ""

        Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
        Dim ArrInventoryMovementIn As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
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
        Dim strSiloNo As String = Nothing
        Dim objIMOut As New clsInventoryMovementNew()
        Dim objIMIn As New clsInventoryMovementNew()
        Dim strJobLoc As String = Nothing
        Dim strJobVendor As String = Nothing
        If (clsCommon.myLen(obj.SRN_NO) > 0 OrElse clsCommon.myLen(obj.Milk_Transfer_In) > 0) Then
            strSiloNo = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
            strJobLoc = clsDBFuncationality.getSingleValue("select Sublocation_Code  from tspl_gate_entry_details where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
            strJobVendor = clsDBFuncationality.getSingleValue("select Jobwork_Vendor  from tspl_location_master where Location_Code='" & strJobLoc & "'", trans)
        Else
            strJobLoc = obj.JobWork_location
            strJobVendor = obj.Vendor_Code
        End If

        For Each objTr As clsMilkJobworkTransferDetails In obj.Arr
            ' FOr Out type
            objIMOut = New clsInventoryMovementNew()
            strItemType = clsItemMaster.GetItemType(objTr.Item_Code, trans)
            objIMOut.InOut = "O"
            If (clsCommon.myLen(obj.SRN_NO) > 0 OrElse clsCommon.myLen(obj.Milk_Transfer_In) > 0) Then
                objIMOut.Location_Code = obj.Virtual_location
            Else
                objIMOut.Location_Code = objTr.Sub_Location
            End If
            'objIMOut.Location_Code = obj.Loc_Code
            objIMOut.Vendor_Code = obj.Vendor_Code
            objIMOut.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)
            objIMOut.Item_Code = objTr.Item_Code
            objIMOut.Item_Desc = clsIntimation.getItemName(objTr.Item_Code, trans)
            objIMOut.Qty = objTr.Net_Weight
            objIMOut.UOM = objTr.UOM
            objIMOut.MRP = 0
            objIMOut.Add_Cost = 0
            If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                objIMOut.ItemType = "RM"
            ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                objIMOut.ItemType = "OT"
            ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                objIMOut.ItemType = "FT"
            End If
            objIMOut.ItemType = strItemTypeToSave
            objIMOut.main_location = obj.Loc_Code
            If allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory Then
                Dim dtFatSndClosing As DataTable = clsProcessProductionPlanning.GetMilkAndALLItemStockBalance_With_FATSNFKG("", objTr.Item_Code, obj.Loc_Code, objTr.Sub_Location, obj.Document_Date, trans, objTr.UOM, 1)
                objIMOut.FAT_Per = clsBOM.GetFatSNFPercentage_AfterConversion(objTr.Item_Code, objTr.UOM, Math.Round(clsCommon.myCdbl(dtFatSndClosing.Rows(0)("qty")), 3), clsCommon.myCdbl(dtFatSndClosing.Rows(0)("fat_kg")), trans, True)
                objIMOut.SNF_Per = clsBOM.GetFatSNFPercentage_AfterConversion(objTr.Item_Code, objTr.UOM, Math.Round(clsCommon.myCdbl(dtFatSndClosing.Rows(0)("qty")), 3), clsCommon.myCdbl(dtFatSndClosing.Rows(0)("snf_kg")), trans, True)
                objIMOut.FAT_KG = clsBOM.GetFatSNFKG_AfterConversion(objTr.Item_Code, objTr.UOM, objTr.Net_Weight, objIMOut.FAT_Per, trans)
                objIMOut.SNF_KG = clsBOM.GetFatSNFKG_AfterConversion(objTr.Item_Code, objTr.UOM, objTr.Net_Weight, objIMOut.SNF_Per, trans)
                Dim objCost As MIlkComponentType = clsInventoryMovementNew.GetAvgCost(True, False, False, False, "", "MI", objTr.Item_Code, objTr.Sub_Location, objTr.Net_Weight, objTr.UOM, objIMOut.FAT_KG, objIMOut.SNF_KG, obj.Document_Date, obj.Document_Date, False, trans)
                objIMOut.Fat_Rate = Math.Abs(Math.Round(If(objIMOut.FAT_KG <= 0, 0, objCost.FAT_Cost / objIMOut.FAT_KG), 2))
                objIMOut.SNF_Rate = Math.Abs(Math.Round(If(objIMOut.SNF_KG <= 0, 0, objCost.SNF_Cost / objIMOut.SNF_KG), 2))
                objIMOut.Fat_Amt = Math.Abs(objCost.FAT_Cost)
                objIMOut.SNF_Amt = Math.Abs(objCost.SNF_Cost)
                objIMOut.Basic_Cost = (objIMOut.Fat_Amt + objIMOut.SNF_Amt) / objTr.Net_Weight
                objIMOut.Net_Cost = (objIMOut.Fat_Amt + objIMOut.SNF_Amt) / objTr.Net_Weight
                objIMOut.DonNotCalculateAvgFATSNFCost = True
            Else
                objIMOut.FAT_Per = objTr.fat_per
                objIMOut.SNF_Per = objTr.snf_Per
                objIMOut.FAT_KG = objTr.fat_KG
                objIMOut.SNF_KG = objTr.SNF_KG
                objIMOut.Net_Cost = objTr.Actual_Amount
                objIMOut.Fat_Rate = objTr.fat_Rate
                objIMOut.SNF_Rate = objTr.SNF_Rate
                objIMOut.Fat_Amt = objTr.fat_Rate * objTr.fat_KG
                objIMOut.SNF_Amt = objTr.SNF_Rate * objTr.SNF_KG
                objIMOut.Basic_Cost = objTr.Actual_Amount / objTr.Net_Weight
            End If
            ArrInventoryMovement.Add(objIMOut)

            '' For In Type
            objIMIn = New clsInventoryMovementNew()
            objIMIn = clsInventoryMovementNew.DeepCopyObject(objIMOut)
            objIMIn.InOut = "I"
            objIMIn.Location_Code = strJobLoc
            ArrInventoryMovementIn.Add(objIMIn)
        Next


        clsInventoryMovementNew.SaveData("MilkTransferJobWork", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
        clsInventoryMovementNew.SaveData("MilkTransferJobWork", obj.Document_Code, obj.Document_Date, clsCommon.GetPrintDate(obj.Document_Date, "dd/MM/yyyy"), ArrInventoryMovementIn, trans)

        Return True
    End Function

    Public Shared Function CreateTransferOutJE(obj As clsMilkJobworkTransfer, trans As SqlTransaction, strVoucherNoForRecreateOnly As String, ByVal allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory As Boolean) As Boolean
        Dim rValue As Boolean = False
        Try
            If clsCommon.myLen(obj.SRN_NO) = 0 AndAlso clsCommon.myLen(obj.Milk_Transfer_In) = 0 Then
                Dim dt As Date = clsCommon.GETSERVERDATE(trans)
                Dim FromLocSeg As String = String.Empty
                Dim ToLocSeg As String = String.Empty
                If obj.isPosted = 1 Then
                    dt = obj.Document_Date
                End If
                FromLocSeg = obj.Loc_Code
                ToLocSeg = obj.JobWork_location
                Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim ArryLst As ArrayList = New ArrayList()
                    For i As Integer = 0 To obj.Arr.Count - 1
                        Dim Amt As Double = clsCommon.myCdbl(obj.Arr(i).Actual_Amount)
                        Dim dtAccount As DataTable = clsDBFuncationality.GetDataTable("select Purchase_JobWork,Stock_Transfer_JobWork,Difference_Account from tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Arr(i).Item_Code & "') ", trans)
                        If dtAccount Is Nothing OrElse dtAccount.Rows.Count <= 0 Then
                            Throw New Exception("Please Map Purchase Account Set vendor " + obj.Vendor_Code)
                        End If
                        Dim PurchaseJobWorkAcc As String = clsCommon.myCstr(dtAccount.Rows(0)("Purchase_JobWork"))
                        If clsCommon.myLen(PurchaseJobWorkAcc) <= 0 Then
                            Throw New Exception("Please Map  Purchase Job Work A/C From Purchase Account Set For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                        End If
                        PurchaseJobWorkAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(PurchaseJobWorkAcc, FromLocationSegment, True, False, trans)
                        ArryLst.Add(New String() {PurchaseJobWorkAcc, Amt})

                        Dim Stock_Transfer_Ac As String = clsCommon.myCstr(dtAccount.Rows(0)("Stock_Transfer_JobWork"))
                        If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                            Throw New Exception("Please Map Stock Transfer Job Work From Purchase Account Set For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                        End If
                        Stock_Transfer_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocationSegment, True, trans)
                        If allowMilkJobworkOutowordWithAvgFatSNFPercentageAtInventory Then
                            Dim dblInvAmt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select AVG_COST From TSPL_INVENTORY_MOVEMENT_NEW Where Trans_Type ='MilkTransferJobWork' and item_code='" + obj.Arr(i).Item_Code + "' and Source_Doc_No='" + obj.Document_Code + "'", trans))
                            ArryLst.Add(New String() {Stock_Transfer_Ac, dblInvAmt * -1})

                            If dblInvAmt - Amt <> 0 Then
                                Dim strDiffAccount As String = clsCommon.myCstr(dtAccount.Rows(0)("Difference_Account"))
                                If clsCommon.myLen(strDiffAccount) <= 0 Then
                                    Throw New Exception("Please Map  Difference A/C From Purchase Account Set For Item : " & obj.Arr(i).Item_Code & " (" & clsItemMaster.GetItemName(obj.Arr(i).Item_Code, trans) & ")")
                                End If
                                strDiffAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(strDiffAccount, FromLocationSegment, True, False, trans)
                                ArryLst.Add(New String() {strDiffAccount, dblInvAmt - Amt})
                            End If
                        Else
                            ArryLst.Add(New String() {Stock_Transfer_Ac, Amt * -1})
                        End If
                    Next

                    Dim GLDesc As String = "Journal Entry Against Milk Job Work Transfer - Doc No." & obj.Document_Code & " "
                    Dim Remarks As String = "Journal Entry against Milk Job Work Transfer - from location -" & obj.Loc_Code & " to location- " & obj.JobWork_location & " For Doc No. " & obj.Document_Code & ""
                    If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                        transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoForRecreateOnly, trans, obj.Document_Date, GLDesc, "JW-TF", "JWO Transfer", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, Remarks)
                    Else
                        transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Document_Date, GLDesc, "JW-MI", "JWO Transfer", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, Remarks)
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    'Public Shared Function CreateTransferOutJE(obj As clsMilkJobworkTransfer, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
    '    Dim rValue As Boolean = False
    '    Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
    '    Try
    '        Dim ArryLst As ArrayList = New ArrayList()
    '        Dim Branch_Ac As String = String.Empty
    '        Dim Inventory_Control_Ac As String = String.Empty
    '        Dim FromLocSeg As String = obj.Loc_Code
    '        Dim ToLocSeg As String = obj.JobWork_location
    '        Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))
    '        Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans))

    '        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TransferEntryOnInvCtrlAccount, clsFixedParameterCode.TransferEntryOnInvCtrlAccount, trans)) = 1 Then
    '            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
    '            If clsCommon.myLen(Branch_Ac) <= 0 Then
    '                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
    '            End If
    '            Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, FromLocationSegment, True, trans)
    '            ArryLst.Add(New String() {Branch_Ac, obj.Amount})


    '            Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_control_account from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
    '            If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
    '                Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
    '            End If
    '            Inventory_Control_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)
    '            ArryLst.Add(New String() {Inventory_Control_Ac, -1 * obj.Amount})
    '        Else
    '            Dim COGT_AC As String = String.Empty
    '            Dim Stock_Transfer_Ac As String = String.Empty


    '            Dim Inventory_Control_Ac_FromLoc As String = String.Empty
    '            Dim Inventory_Control_Ac_GITLoc As String = String.Empty
    '            Dim Stock_Transfer_Ac_FromLoc As String = String.Empty
    '            Dim Stock_Transfer_Ac_GITLoc As String = String.Empty
    '            Dim CostingMethod As Integer = 0
    '            Dim CostOfItem As Double = 0
    '            Dim dt As Date = clsCommon.GETSERVERDATE(trans)

    '            If obj.isPosted = 1 Then
    '                dt = obj.Posting_Date
    '            End If
    '            Dim strJobWorkLoc As String = obj.JobWork_location
    '            Dim strJobWorkLoc_SEG As String = String.Empty
    '            strJobWorkLoc_SEG = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & strJobWorkLoc & "'", trans))
    '            If clsCommon.myLen(strJobWorkLoc_SEG) <= 0 Then
    '                Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & strJobWorkLoc)
    '            End If
    '            strJobWorkLoc = strJobWorkLoc_SEG

    '            If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
    '                CostingMethod = clsInventoryMovementNew.getCostingMethod(obj.Item_Code, trans)
    '                CostOfItem = clsInventoryMovement.GetCost(CostingMethod, obj.Item_Code, obj.Loc_Code, obj.Net_Weight, obj.Document_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "TSPL_INVENTORY_MOVEMENT_NEW")
    '            Else
    '                CostOfItem = 0
    '            End If  '' Done By Pankaj Jha For Skipping Cogs GL
    '            Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Acc from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
    '            If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
    '                Throw New Exception("Please Map  Stock Transfer A/C From Purchase Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
    '            End If
    '            If CostOfItem > 0 Then
    '                COGT_AC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select COGT_AC from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
    '                If clsCommon.myLen(COGT_AC) <= 0 Then
    '                    Throw New Exception("Please Map Cost Of Goods Transfer A/C From Sales Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
    '                End If
    '                COGT_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(COGT_AC, FromLocationSegment, True, trans)
    '                Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, trans)
    '            End If
    '            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
    '            If clsCommon.myLen(Branch_Ac) <= 0 Then
    '                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)))
    '            End If

    '            Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & obj.Item_Code & "') ", trans))
    '            If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
    '                Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & obj.Item_Code & " (" & obj.Item_Desc & ")")
    '            End If
    '            Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, strJobWorkLoc, True, False, trans)
    '            Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, strJobWorkLoc, True, False, trans)
    '            Stock_Transfer_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, FromLocationSegment, True, trans)

    '            Dim Amt As Double = obj.Actual_Amount

    '            ArryLst.Add(New String() {Branch_Ac, Amt})
    '            ArryLst.Add(New String() {Stock_Transfer_Ac_FromLoc, Amt * -1})
    '            ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt})
    '            ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt * -1})

    '            If CostOfItem > 0 Then
    '                ArryLst.Add(New String() {COGT_AC, CostOfItem})
    '                ArryLst.Add(New String() {Inventory_Control_Ac_FromLoc, CostOfItem * -1})
    '            End If
    '        End If


    '        '===========BM00000008259
    '        Dim GLDesc As String = ""
    '        Dim Remarks As String = ""
    '        If clsCommon.myLen(obj.SRN_NO) > 0 Then
    '            GLDesc = "Journal Entry Against Milk Job Work Transfer - Doc No." & obj.Document_Code & " SRN No." & obj.SRN_NO & " Gate Entry No." & obj.Gate_Entry_No & " "
    '            Remarks = "Journal Entry against Milk Job Work Transfer from location -" & obj.Loc_Code & " to location- " & obj.JobWork_location & " For Doc No. " & obj.Document_Code & ""
    '        ElseIf clsCommon.myLen(obj.Milk_Transfer_In) > 0 Then
    '            GLDesc = "Journal Entry Against Milk Job Work Transfer - Doc No." & obj.Document_Code & " Milk Transfer In No." & obj.Milk_Transfer_In & " Gate Entry No." & obj.Gate_Entry_No & " "
    '            Remarks = "Journal Entry against Milk Job Work Transfer from location -" & obj.Loc_Code & " to location- " & obj.JobWork_location & " For Doc No. " & obj.Document_Code & ""
    '        End If

    '        If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then ''because if voucher no known then recreate it with same no.
    '            transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, strVoucherNoForRecreateOnly, trans, obj.Document_Date, GLDesc, "MI-JT", "Milk Job Work transfer", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, Remarks)
    '        Else
    '            transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, obj.Document_Date, GLDesc, "MI-JT", "Milk Job Work transfer", obj.Document_Code, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, Remarks)
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    '    Return True
    'End Function
    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim qry As String = ""
            Dim isDeleted As Boolean = True
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", "TSPL_MILK_JOBWORK_TRANSFER_DETAILS", "Document_Code", trans)
            qry = "delete from TSPL_MILK_JOBWORK_TRANSFER_DETAILS where  Document_Code='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MilkJobWork_Transfer_Parameter_Range_Detail where  Document_Code='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Document_Code='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code as Code, TSPL_MILK_JOBWORK_TRANSFER_HEAD.SRN_NO as [SrnNo],TSPL_MILK_JOBWORK_TRANSFER_HEAD.Milk_Transfer_In as [Milk Transfer In] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date as SRNDate ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Weighment_No as [Weighment No],TSPL_MILK_JOBWORK_TRANSFER_HEAD.gate_entry_no as [Gate Entry No] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Weighment_Date as [Weighment Date] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.QC_No as [Qc No] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Qc_Date as [Qc Date] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Vendor_Code as [Vendor Code] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Loc_Code as [Location Code] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Challan_No as [Challan No] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Challan_Date as [Challan Date] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Tanker_No as [Tanker No] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Price_Code as [Price Code] ,case when isnull(TSPL_MILK_JOBWORK_TRANSFER_HEAD.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Posting_Date as [Posting Date] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Item_Code as [Item Code] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Item_Desc as [Item Desc] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.UOM as [Uom] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Gross_Weight as [Gross Weight] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Tare_Weight as [Tare Weight] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Net_Weight as [Net Weight] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.snf_Per as [SNF Per] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_per as [FAT Per] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_KG as [FAT Kg] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.SNF_KG as [SNF Kg] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.fat_Rate as [FAT Rate] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.SNF_Rate as [SNF Rate] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Amount as [Amount] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Deduction as [Deduction] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.SpecialDeduction as [Special Deduction],TSPL_MILK_JOBWORK_TRANSFER_HEAD.Actual_Amount as [Actual Amount] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Created_By as [Created By] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Created_Date as [Created Date] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Modify_By as [Modify By] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.Modify_Date as [Modify Date] ,TSPL_MILK_JOBWORK_TRANSFER_HEAD.comp_code as [Company Code]  From TSPL_MILK_JOBWORK_TRANSFER_HEAD "
            str = clsCommon.ShowSelectForm("MTJobWork", qry, "Code", whrcls, curcode, "Code", isButtonClicked, "TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Date")
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
    Public Shared Function saveData(ByVal obj As clsMilkJobworkTransfer, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "JobWork Outward", "JobWork Milk Transfer", obj.Loc_Code, obj.Document_Date, trans)

            Dim issaved As Boolean = True
            If obj.isNewEntry Then
                obj.Document_Code = clsERPFuncationality.GetNextCode(trans, obj.Document_Date, clsDocType.frmMilkJobWorkTransfer, "", obj.Loc_Code)
            End If
            If clsCommon.myLen(obj.Document_Code) <= 0 Then
                Throw New Exception("Error In Document  No Genertion")
            End If

            clsERPFuncationality.IsDocumentAlreadyPosted("TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", obj.Document_Code, "isPosted=1", trans)
            If Not obj.isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Document_Code), "TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", "TSPL_MILK_JOBWORK_TRANSFER_DETAILS", "Document_Code", trans)
            End If
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Manual_Standard_Rate", obj.Manual_Standard_Rate)
            clsCommon.AddColumnsForChange(coll, "SRN_NO", clsCommon.myCstr(obj.SRN_NO))
            clsCommon.AddColumnsForChange(coll, "Milk_Transfer_In", clsCommon.myCstr(obj.Milk_Transfer_In))
            clsCommon.AddColumnsForChange(coll, "Doc_Type", clsCommon.myCstr(obj.Doc_Type))
            clsCommon.AddColumnsForChange(coll, "Virtual_location", clsCommon.myCstr(obj.Virtual_location))
            clsCommon.AddColumnsForChange(coll, "JobWork_location", clsCommon.myCstr(obj.JobWork_location))
            If clsCommon.myLen(obj.Document_Date) > 0 Then
                If DateTime = "1" Then
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy hh:mm:ss tt"), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy"), True)
                End If

            End If
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
            If obj.isNewEntry OrElse isHistory Then
                clsCommon.AddColumnsForChange(coll, "Document_Code", clsCommon.myCstr(obj.Document_Code))
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_JOBWORK_TRANSFER_HEAD", OMInsertOrUpdate.Insert, "", trans)

            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_JOBWORK_TRANSFER_HEAD", OMInsertOrUpdate.Update, "TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code='" + obj.Document_Code + "'", trans)
            End If
            If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                issaved = issaved AndAlso clsMilkTransferParam.saveData(obj.arrObj, obj.Document_Code, trans)
            End If
         
            issaved = issaved AndAlso clsMilkJobworkTransferDetails.SaveData(obj.Document_Code, obj.Arr, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function InsertParameterRange(ByVal obj As clsMilkJobworkTransfer, ByVal strLocation As String, ByVal strQCNo As String, ByVal trans As SqlTransaction) As Boolean

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
            Dim objParam As New clsMilkTransferParam
            obj.arrObj = New List(Of clsMilkTransferParam)
            For ii As Integer = 0 To intRow - 1
                Dim strMilkType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MIKL_TYPE_CODE from TSPL_QUALITY_CHEMBER_DETAILS where QC_No='" & strQCNo & "' and Line_No=" & ii + 1 & " ", trans))
                objParam = New clsMilkTransferParam
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    qry2 = Nothing
                    qry3 = Nothing
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & strQCNo & "' and line_No='" & ii + 1 & "'  and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3, trans))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "' and loc_code='" & obj.Loc_Code & "'  and isReject=0  and MIKL_TYPE_CODE='" & strMilkType & "'    order by Effective_Date desc  "
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
                            objParam.Document_Code = clsCommon.myCstr(obj.Document_Code)
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
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt22.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "' and loc_code='" & obj.Loc_Code & "' and isReject=0  and MIKL_TYPE_CODE='" & strMilkType & "'    order by Effective_Date desc  "
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
                            objParam.Document_Code = clsCommon.myCstr(obj.Document_Code)
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
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt33.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(obj.Document_Date, "dd/MMM/yyyy") & "' and loc_code='" & obj.Loc_Code & "' and isReject=0  and MIKL_TYPE_CODE='" & strMilkType & "'    order by Effective_Date desc  "
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
                            objParam.Document_Code = clsCommon.myCstr(obj.Document_Code)
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
                issaved = issaved AndAlso clsMilkTransferParam.saveData(obj.arrObj, obj.Document_Code, trans)
                Return issaved
            End If
            Return issaved
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ReverseAndUnpost(strCode, trans) Then
                trans.Commit()
            Else
                trans.Rollback()
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True


    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, ByVal Trans As SqlTransaction) As Boolean
        'If Trans Is Nothing Then
        '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        'End If

        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select isPosted from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Document_Code='" + strCode + "'"
            'If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
            '    Throw New Exception("Transaction status should be posted for reverse and unpost")
            'End If


            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code in ('MI-JT','JW-MI','JW-TF') and Source_Doc_No='" + strCode + "'", Trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", Trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_INVENTORY_MOVEMENT_NEW", "Source_Doc_No", Trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + strCode + "' and Trans_Type='MilkTransferJobWork'"
            clsDBFuncationality.ExecuteNonQuery(Qry, Trans)


            Dim intDirect As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_MILK_JOBWORK_TRANSFER_HEAD where (Milk_Transfer_In <> '' or SRN_NO <> '') and Document_Code='" & strCode & "'", Trans))
            If intDirect >= 1 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", "TSPL_MILK_JOBWORK_TRANSFER_DETAILS", "Document_Code", Trans)
                Qry = "delete from TSPL_MilkJobWork_Transfer_Parameter_Range_Detail where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)

                Qry = "delete from TSPL_MILK_JOBWORK_TRANSFER_DETAILS where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)

                Qry = "delete from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
            Else
                Qry = "update TSPL_MILK_JOBWORK_TRANSFER_HEAD set isposted=0 where Document_Code='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, Trans)
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strCode), "TSPL_MILK_JOBWORK_TRANSFER_HEAD", "Document_Code", Trans)
            End If
           
            'trans.Commit()
        Catch ex As Exception
            'trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional isrntrade As Boolean = False, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkJobworkTransfer
        Dim obj As New clsMilkJobworkTransfer
        Try
            ' Dim obj As New clsMilkJobworkTransfer
            Dim qst As String = " select *   From TSPL_MILK_JOBWORK_TRANSFER_HEAD   where 1=1  " & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ")

            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and Loc_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code in (select min(Document_Code ) from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Document_Code  >'" + strCode + "' " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
                Case NavigatorType.First
                    qst += " and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code in (select MIN(Document_Code ) from TSPL_MILK_JOBWORK_TRANSFER_HEAD  where 1=1 " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
                Case NavigatorType.Last
                    qst += " and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code in (select Max(Document_Code ) from TSPL_MILK_JOBWORK_TRANSFER_HEAD  where 1=1 " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
                Case NavigatorType.Previous
                    qst += " and TSPL_MILK_JOBWORK_TRANSFER_HEAD.Document_Code in (select Max(Document_Code ) from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Document_Code  <'" + strCode + "'  " & whrCls & IIf(isrntrade, "  and formType='Bulk Milk SRN Trade'   ", " and formType='BulkMilkSrn'   ") & " and isnull(srn_return_no,'')='')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Manual_Standard_Rate = clsCommon.myCdbl(dt.Rows(0)("Manual_Standard_Rate"))
                obj.Document_Code = clsCommon.myCstr(dt.Rows(0)("Document_Code"))
                obj.Virtual_location = clsCommon.myCstr(dt.Rows(0)("Virtual_location"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.JobWork_location = clsCommon.myCstr(dt.Rows(0)("JobWork_location"))
                obj.SRN_NO = clsCommon.myCstr(dt.Rows(0)("SRN_NO"))
                obj.Milk_Transfer_In = clsCommon.myCstr(dt.Rows(0)("Milk_Transfer_In"))
                obj.Document_Date = clsCommon.myCDate(dt.Rows(0)("Document_Date"), "dd/MMM/yyyy hh:mm:ss tt")
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
                If obj.isApproved = 1 Then
                    obj.Approval_Ref_Doc_No = clsCommon.myCstr(dt.Rows(0)("Approval_Ref_Doc_No"))
                    obj.Approved_Rate = clsCommon.myCdbl(dt.Rows(0)("Approved_Rate"))
                End If
                If clsCommon.myLen(dt.Rows(0)("PO_NO")) > 0 Then
                    obj.PO_NO = clsCommon.myCstr(dt.Rows(0)("PO_NO"))
                    obj.PO_Date = clsCommon.GetPrintDate(dt.Rows(0)("PO_Date"), "dd/MMM/yyyy")
                End If
                ' Return obj
                obj.arrObj = clsMilkTransferParam.getData(obj.Document_Code, trans)
                obj.Arr = clsMilkJobworkTransferDetails.GetData(obj.Document_Code, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function



End Class
Public Class clsMilkJobworkTransferDetails
#Region "Variables"
    Public fat_Qty As Double = 0
    Public SNF_Qty As Double = 0
    Public TotalStandardQty As Double = 0
    Public Incentive_Amt As Double = 0
    Public Deduction_Amt As Double = 0
    Public Document_Code As String = Nothing
    Public Line_No As Integer = 0
    Public Sub_Location As String = Nothing
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Integer = 0
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
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

    Public Receipt_Net_Weight As Decimal = 0
    Public Receipt_snf_Per As Decimal = 0
    Public Receipt_fat_per As Decimal = 0
    Public Receipt_fat_KG As Decimal = 0
    Public Receipt_SNF_KG As Decimal = 0
#End Region


    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsMilkJobworkTransferDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_MILK_JOBWORK_TRANSFER_DETAILS where Document_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            Dim dblTotAMt As Double = 0
            For Each obj As clsMilkJobworkTransferDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Document_Code", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                'clsCommon.AddColumnsForChange(coll, "fat_Qty", obj.fat_Qty)
                'clsCommon.AddColumnsForChange(coll, "SNF_Qty", obj.SNF_Qty)
                'clsCommon.AddColumnsForChange(coll, "TotalStandardQty", obj.TotalStandardQty)
                'clsCommon.AddColumnsForChange(coll, "Incentive_Amt", obj.Incentive_Amt)
                'clsCommon.AddColumnsForChange(coll, "Deduction_Amt", obj.Deduction_Amt)
                clsCommon.AddColumnsForChange(coll, "Item_Code", clsCommon.myCstr(obj.Item_Code))
                clsCommon.AddColumnsForChange(coll, "Sub_Location", obj.Sub_Location, True)
                clsCommon.AddColumnsForChange(coll, "UOM", clsCommon.myCstr(obj.UOM))
                clsCommon.AddColumnsForChange(coll, "Gross_Weight", clsCommon.myCdbl(obj.Gross_Weight))
                clsCommon.AddColumnsForChange(coll, "Tare_Weight", clsCommon.myCdbl(obj.Tare_Weight))
                clsCommon.AddColumnsForChange(coll, "Net_Weight", clsCommon.myCdbl(obj.Net_Weight))
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
                dblTotAMt += obj.Actual_Amount
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_JOBWORK_TRANSFER_DETAILS", OMInsertOrUpdate.Insert, "", trans)
            Next
            sQuery = "Update TSPL_MILK_JOBWORK_TRANSFER_DETAILS set Actual_Amount=" & dblTotAMt & " where Document_Code='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsMilkJobworkTransferDetails)
        Dim arr As List(Of clsMilkJobworkTransferDetails) = Nothing
        Dim qry As String
        qry = "select TSPL_MILK_JOBWORK_TRANSFER_DETAILS.* from " &
            "TSPL_MILK_JOBWORK_TRANSFER_DETAILS where TSPL_MILK_JOBWORK_TRANSFER_DETAILS.Document_Code='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsMilkJobworkTransferDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsMilkJobworkTransferDetails = New clsMilkJobworkTransferDetails()
                obj.Document_Code = clsCommon.myCstr(dr("Document_Code"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Sub_Location = clsCommon.myCstr(dr("Sub_Location"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dr("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dr("Net_Weight"))
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
                'obj.fat_Qty = clsCommon.myCdbl(dr("fat_Qty"))
                'obj.SNF_Qty = clsCommon.myCdbl(dr("SNF_Qty"))
                'obj.TotalStandardQty = clsCommon.myCdbl(dr("TotalStandardQty"))
                'obj.Incentive_Amt = clsCommon.myCdbl(dr("Incentive_Amt"))
                'obj.Deduction_Amt = clsCommon.myCdbl(dr("Deduction_Amt"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function

    Public Shared Function UpdateReceiptDetail(ByVal strCode As String, ByVal Arr As List(Of clsMilkJobworkTransferDetails)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select Document_NO from TSPL_JWO_ESTIMATION_TRANSFER where Transfer_Code='" + strCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Throw New Exception("Estimate No [" + dt.Rows(0)("Document_NO") + "] is Generated With this Document")
            End If
            For Each obj As clsMilkJobworkTransferDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Receipt_Net_Weight", clsCommon.myCdbl(obj.Receipt_Net_Weight))
                clsCommon.AddColumnsForChange(coll, "Receipt_snf_Per", clsCommon.myCstr(obj.Receipt_snf_Per))
                clsCommon.AddColumnsForChange(coll, "Receipt_fat_per", clsCommon.myCdbl(obj.Receipt_fat_per))
                clsCommon.AddColumnsForChange(coll, "Receipt_fat_KG", clsCommon.myCdbl(obj.Receipt_fat_KG))
                clsCommon.AddColumnsForChange(coll, "Receipt_SNF_KG", clsCommon.myCdbl(obj.Receipt_SNF_KG))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_JOBWORK_TRANSFER_DETAILS", OMInsertOrUpdate.Update, "Document_Code='" + strCode + "' and Line_No=" + clsCommon.myCstr(obj.Line_No) + "", trans)
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try

        Return True
    End Function
End Class
Public Class clsMilkTransferParam
    Public Line_No As Integer = 0
    Public Document_Code As String = String.Empty
    Public Parameter As String = String.Empty
    Public Lower_Range As String = String.Empty
    Public Upper_Range As String = String.Empty
    Public value As String = String.Empty
    Public Incen_Deduc As Double = 0
    Public QCValue As String = String.Empty


    Public Shared Function saveData(ByVal arrObj As List(Of clsMilkTransferParam), ByVal strSRNNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                Dim qry As String = "delete from TSPL_MilkJobWork_Transfer_Parameter_Range_Detail where Document_Code='" & strSRNNo & "'"
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As clsMilkTransferParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                    clsCommon.AddColumnsForChange(coll, "Document_Code", obj.Document_Code)
                    clsCommon.AddColumnsForChange(coll, "Parameter", obj.Parameter)
                    clsCommon.AddColumnsForChange(coll, "Lower_Range", obj.Lower_Range)
                    clsCommon.AddColumnsForChange(coll, "Upper_Range", obj.Upper_Range)
                    clsCommon.AddColumnsForChange(coll, "value", obj.value)
                    clsCommon.AddColumnsForChange(coll, "Incen_Deduc", obj.Incen_Deduc)
                    clsCommon.AddColumnsForChange(coll, "QCValue", obj.QCValue)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MilkJobWork_Transfer_Parameter_Range_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strSRNNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsMilkTransferParam)
        Dim arrObj As List(Of clsMilkTransferParam) = Nothing
        Try
            Dim obj As clsMilkTransferParam = Nothing
            Dim qry As String = "select * from TSPL_MilkJobWork_Transfer_Parameter_Range_Detail where Document_Code='" & strSRNNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsMilkTransferParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsMilkTransferParam()
                    obj.Document_Code = clsCommon.myCstr(dt.Rows(i)("Document_Code"))
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
            Dim qry As String = "delete from TSPL_MilkJobWork_Transfer_Parameter_Range_Detail where Document_Code='" & strSRNNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class






