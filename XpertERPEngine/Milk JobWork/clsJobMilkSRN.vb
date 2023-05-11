'===========BM00000007790,Rohit========================
Imports common
Imports System.Data.SqlClient
Public Class clsJobMilkSRN

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
    Public HSN_Code As String = String.Empty
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
    Public TANKER_SKU_MANUAL As Integer = 0
    Public PO_NO As String = ""
    Public PO_Date As String = ""
    Public isApproved As Integer = 0  ' Stores SRN Approved Status, if 1 then Approved
    Public Approval_Ref_Doc_No As String = ""
    Public Approved_Rate As Double = 0
    Public Balance As Double = 0
    Public BalanceFatKg As Double = 0
    Public BalanceSNFKg As Double = 0
    Public Qty As Double = 0
    Public arrObj As List(Of clsJobSRNParam) = Nothing
    Public Shared Function postData(ByVal StrDocNo As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = Nothing
        Dim isSaved As Boolean = True
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(StrDocNo) <= 0) Then
                Throw New Exception(" Doc No not found to Post")
            End If

            Dim obj As clsJobMilkSRN = clsJobMilkSRN.getData(StrDocNo, NavigatorType.Current)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.SRN_NO) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Jobwork", "Milk JobWork SRN", obj.Loc_Code, obj.SRN_Date, trans)

            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            '--------------------
            Dim isResult As Boolean = clsApprovalScreen.CheckApprovalLevel(formId, "tspl_Job_milk_srn", "SRN_No", obj.SRN_NO, trans)
            If isResult = False Then
                trans.Commit()
                Return False
            End If

            Dim qry As String = ""
            Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)()
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim ArrInventoryMovementN As List(Of clsInventoryMovement) = New List(Of clsInventoryMovement)
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

            Dim sQuery As String = "select Product_Type from tspl_Item_Master where Item_Code ='" & obj.Item_Code & "'"
            Dim Pr_Type As String = clsDBFuncationality.getSingleValue(sQuery, trans)
            '-----------Getting Sub Location Where Milk Was unloaded
            Dim strSiloNo As String = ""

            'If clsCommon.myLen(obj.Gate_Entry_No) > 0 Then
            '    ' strSiloNo = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_JOB_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)

            '    strSiloNo = clsDBFuncationality.getSingleValue("select case when isnull(Main_Location_Code,'')='' then Location_Code else Main_Location_Code end as Location from TSPL_LOCATION_MASTER where Location_Code='" & obj.Loc_Code & "'", trans)
            'End If
            '-----------------------------------
            If Pr_Type = "MI" Then
                Dim objInventoryMovemnt As New clsInventoryMovementNew()
                objInventoryMovemnt.InOut = "I"

                objInventoryMovemnt.Location_Code = obj.Loc_Code

                objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)

                objInventoryMovemnt.Item_Code = obj.Item_Code
                objInventoryMovemnt.Item_Desc = obj.Item_Desc
                objInventoryMovemnt.Qty = obj.Qty
                objInventoryMovemnt.UOM = obj.UOM
                objInventoryMovemnt.MRP = 0
                objInventoryMovemnt.Add_Cost = 0
                objInventoryMovemnt.FAT_Per = obj.fat_per
                objInventoryMovemnt.SNF_Per = obj.snf_Per
                objInventoryMovemnt.FAT_KG = obj.fat_KG
                objInventoryMovemnt.SNF_KG = obj.SNF_KG
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
                objInventoryMovemnt.Basic_Cost = obj.Actual_Amount / obj.Qty
                ArrInventoryMovement.Add(objInventoryMovemnt)
                isSaved = isSaved AndAlso clsInventoryMovementNew.SaveData("MJ-SR", obj.SRN_NO, obj.SRN_Date, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            Else
                Dim objInventoryMovemnt As New clsInventoryMovement()
                objInventoryMovemnt.InOut = "I"
                objInventoryMovemnt.Location_Code = obj.Loc_Code

                objInventoryMovemnt.Vendor_Code = obj.Vendor_Code
                objInventoryMovemnt.Vendor_Name = clsVendorMaster.GetName(obj.Vendor_Code, trans)

                objInventoryMovemnt.Item_Code = obj.Item_Code
                objInventoryMovemnt.Item_Desc = obj.Item_Desc
                objInventoryMovemnt.Qty = obj.Qty '- IIf(obj.is_Srn_rejQty_goes_in_Rejstore = True And isSerialiedItem <> 1, obj.Rejected_Qty, 0)
                objInventoryMovemnt.UOM = obj.UOM
                objInventoryMovemnt.MRP = obj.Actual_Amount
                objInventoryMovemnt.Add_Cost = obj.Actual_Amount
                objInventoryMovemnt.Net_Cost = obj.Actual_Amount
                If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "RM"
                ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "OT"
                ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                    objInventoryMovemnt.ItemType = "FT"
                End If
                objInventoryMovemnt.ItemType = strItemTypeToSave

                objInventoryMovemnt.Basic_Cost = obj.Actual_Amount / obj.Qty
                objInventoryMovemnt.Batch_No = Nothing
                objInventoryMovemnt.MFG_Date = Nothing
                objInventoryMovemnt.Expiry_Date = Nothing
                ArrInventoryMovementN.Add(objInventoryMovemnt)
                isSaved = isSaved AndAlso clsInventoryMovement.SaveData("MJ-SR", obj.SRN_NO, obj.SRN_Date, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MM/yyyy"), ArrInventoryMovementN, trans)
            End If
            isSaved = isSaved AndAlso clsItemLocationDetails.SaveData(clsCommon.GetPrintDate(obj.SRN_Date, "dd/MM/yyyy"), ArrLocationDetails, trans)

            'Create GL Entry
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 0 Then
                qry = " select TSPL_PURCHASE_ACCOUNTS.Job_Work_Ac,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,tspl_Job_milk_srn.Actual_Amount,tspl_Job_milk_srn.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join tspl_Job_milk_srn on tspl_Job_milk_srn .Item_Code=TSPL_ITEM_MASTER.Item_Code where tspl_Job_milk_srn.SRN_NO='" & obj.SRN_NO & "' "
                Dim ArryLst As ArrayList = New ArrayList()
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim strInvCntrlAc As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                    Dim strPaybleClrAc As String = clsCommon.myCstr(dt.Rows(0)("Job_Work_Ac"))
                    If clsCommon.CompairString(strInvCntrlAc, "") = CompairStringResult.Equal Then
                        Throw New Exception("Please Set Inventory Control Account")
                    End If
                    If clsCommon.CompairString(strPaybleClrAc, "") = CompairStringResult.Equal Then
                        Throw New Exception("Please Set Job Work Account")
                    End If
                    strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                    strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                    ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount")})
                    ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount") * -1})
                    transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy"), "Against Job Milk SRN No  -" + obj.SRN_NO + "", "BM-SR", "Job Milk SRN", obj.SRN_NO, "", "C", obj.Item_Code, obj.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.Vendor_Code & ", " & clsVendorMaster.GetName(obj.Vendor_Code, trans))
                End If
            End If
            


            Dim strQry As String = " update tspl_Job_milk_srn set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "', sub_location='" & strSiloNo & "' where srn_no='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            Dim objApprov As New ClsTransactionApproval
            objApprov.Document_No = obj.SRN_NO
            objApprov.Doc_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt")
            objApprov.Approval_Type = "Rate"
            objApprov.Approve = 0
            objApprov.Program_Code = formId
            Dim qryp As String = "select Program_Name a   from TSPL_PROGRAM_MASTER where  Program_Code ='" & formId & "'"
            objApprov.Screen_Name = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryp, trans))
            isPosted = ClsTransactionApproval.SaveData(objApprov, True, trans)

            If isPosted Then
                trans.Commit()
            Else
                trans.Rollback()
            End If


            Return isPosted
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function deleteData(ByVal strDocNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim arr As List(Of String) = New List(Of String)
            arr.Add(strDocNo)
            clsERPFuncationality.AddToHistory(arr, clsUserMgtCode.FrmJobMilkSRN, trans)
            Dim qry As String = "delete from tspl_Job_milk_srn where srn_No='" & strDocNo & "'"
            Dim isDeleted As Boolean = True
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            qry = "delete from TSPL_Job_SRN_Parameter_Range_Detail where  srn_No='" & strDocNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            Dim qry As String = " select tspl_Job_milk_srn.SRN_NO as [SrnNo] ,tspl_Job_milk_srn.SRN_Date as [SRN Date] ,tspl_Job_milk_srn.Weighment_No as [Weighment No],tspl_Job_milk_srn.gate_entry_no as [Gate Entry No] ,tspl_Job_milk_srn.Weighment_Date as [Weighment Date] ,tspl_Job_milk_srn.QC_No as [Qc No] ,tspl_Job_milk_srn.Qc_Date as [Qc Date] ,tspl_Job_milk_srn.Vendor_Code as [Vendor Code] ,tspl_Job_milk_srn.Loc_Code as [Location Code] ,tspl_Job_milk_srn.Challan_No as [Challan No] ,tspl_Job_milk_srn.Challan_Date as [Challan Date] ,tspl_Job_milk_srn.Tanker_No as [Tanker No] ,tspl_Job_milk_srn.Price_Code as [Price Code] ,case when isnull(tspl_Job_milk_srn.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,tspl_Job_milk_srn.Posting_Date as [Posting Date] ,tspl_Job_milk_srn.Item_Code as [Item Code] ,tspl_Job_milk_srn.Item_Desc as [Item Desc] ,tspl_Job_milk_srn.UOM as [Uom] ,tspl_Job_milk_srn.Gross_Weight as [Gross Weight] ,tspl_Job_milk_srn.Tare_Weight as [Tare Weight] ,tspl_Job_milk_srn.Net_Weight as [Net Weight] ,tspl_Job_milk_srn.snf_Per as [SNF Per] ,tspl_Job_milk_srn.fat_per as [FAT Per] ,tspl_Job_milk_srn.fat_KG as [FAT Kg] ,tspl_Job_milk_srn.SNF_KG as [SNF Kg] ,tspl_Job_milk_srn.fat_Rate as [FAT Rate] ,tspl_Job_milk_srn.SNF_Rate as [SNF Rate] ,tspl_Job_milk_srn.Amount as [Amount] ,tspl_Job_milk_srn.Deduction as [Deduction] ,tspl_Job_milk_srn.SpecialDeduction as [Special Deduction],tspl_Job_milk_srn.Actual_Amount as [Actual Amount] ,tspl_Job_milk_srn.Created_By as [Created By] ,tspl_Job_milk_srn.Created_Date as [Created Date] ,tspl_Job_milk_srn.Modify_By as [Modify By] ,tspl_Job_milk_srn.Modify_Date as [Modify Date] ,tspl_Job_milk_srn.comp_code as [Company Code]  From tspl_Job_milk_srn "
            str = clsCommon.ShowSelectForm("BULKSRNFND", qry, "SrnNo", whrcls, curcode, "SrnNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function getWeighmentFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try

            Dim qry As String = "select TSPL_Job_Weighment_Detail.Weighment_No as [WeighmentNo] ,TSPL_Job_Weighment_Detail.Weighment_date as [Weighment Date] ,TSPL_Job_Weighment_Detail.Gate_Entry_No as [Gate Entry No] ,TSPL_Job_Weighment_Detail.Doc_Type as [Doc Type] ,TSPL_Job_Weighment_Detail.Date_And_Time as [Gate Entry Date And Time] ,TSPL_Job_Weighment_Detail.Challan_No as [Challan No] ,TSPL_Job_Weighment_Detail.Challan_Date as [Challan Date] ,TSPL_Job_Weighment_Detail.Tanker_No as [Tanker No] ,case when isnull(TSPL_Job_Weighment_Detail.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_Job_Weighment_Detail.Posting_Date as [Posting Date]  ,TSPL_Job_Weighment_Detail.location_Code as [Location Code] ,TSPL_Job_Weighment_Detail.Location_Desc as [Location Desc] ,TSPL_Job_Weighment_Detail.Vendor_Code as [Vendor Code] ,TSPL_Job_Weighment_Detail.Vendor_Desc as [Vendor Desc] ,TSPL_Job_Weighment_Detail.Item_Code as [Item Code] ,TSPL_Job_Weighment_Detail.Item_Desc as [Item Desc] ,TSPL_Job_Weighment_Detail.Qty_In_Kg as [Qty] ,TSPL_Job_Weighment_Detail.snf_Per as [SNF(%)] ,TSPL_Job_Weighment_Detail.fat_per as [FAT(%)] ,TSPL_Job_Weighment_Detail.Created_By as [Created By] ,TSPL_Job_Weighment_Detail.Created_Date as [Created Date] ,TSPL_Job_Weighment_Detail.Modify_By as [Modify By] ,TSPL_Job_Weighment_Detail.Modify_Date as [Modify Date] ,TSPL_Job_Weighment_Detail.comp_code as [Company Code] ,TSPL_Job_Weighment_Detail.Gross_Weight as [Gross Weight] ,TSPL_Job_Weighment_Detail.Dip_Value as [Dip Value] ,TSPL_Job_Weighment_Detail.Tare_Weight as [Tare Weight] ,TSPL_Job_Weighment_Detail.Net_Weight as [Net Weight]   From TSPL_Job_Weighment_Detail	 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Job_Weighment_Detail.vendor_code "
            str = clsCommon.ShowSelectForm("WGHMNTFND", qry, "WeighmentNo", whrcls, curcode, "WeighmentNo", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function saveData(ByVal obj As clsJobMilkSRN, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            Dim Item_Production_type As String = String.Empty
            Dim issaved As Boolean = True
            Dim chk As Integer = 0
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Jobwork", "Milk JobWork SRN", obj.Loc_Code, obj.SRN_Date, trans)

            clsERPFuncationality.IsDocumentAlreadyPosted("tspl_Job_milk_srn", "srn_no", obj.SRN_NO, "isPosted=1", trans)

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "SRN_NO", clsCommon.myCstr(obj.SRN_NO))
            If clsCommon.myLen(obj.SRN_Date) > 0 Then
                clsCommon.AddColumnsForChange(coll, "SRN_Date", clsCommon.GetPrintDate(obj.SRN_Date, "dd/MMM/yyyy hh:mm:ss tt"), True)
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
            clsCommon.AddColumnsForChange(coll, "Price_Code", clsCommon.myCstr(obj.Price_Code), True)
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
            clsCommon.AddColumnsForChange(coll, "FormType", "JobMilkSRN")
            clsCommon.AddColumnsForChange(coll, "fat_per", clsCommon.myCdbl(obj.fat_per))
            clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCdbl(obj.SNF_KG))
            clsCommon.AddColumnsForChange(coll, "fat_KG", clsCommon.myCdbl(obj.fat_KG))
            'clsCommon.AddColumnsForChange(coll, "fat_Rate", clsCommon.myCdbl(obj.fat_Rate))
            'clsCommon.AddColumnsForChange(coll, "SNF_Rate", clsCommon.myCdbl(obj.SNF_Rate))

            clsCommon.AddColumnsForChange(coll, "Balance_Qty", clsCommon.myCdbl(obj.Balance))
            clsCommon.AddColumnsForChange(coll, "Balance_FATKG", clsCommon.myCdbl(obj.BalanceFatKg))
            clsCommon.AddColumnsForChange(coll, "Balance_SNFKG", clsCommon.myCdbl(obj.BalanceSNFKg))
            clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCdbl(obj.Qty))
            clsCommon.AddColumnsForChange(coll, "Deduction", clsCommon.myCdbl(obj.Deduction))
            clsCommon.AddColumnsForChange(coll, "Incentive", clsCommon.myCdbl(obj.Incentive))
            clsCommon.AddColumnsForChange(coll, "Standardrate", clsCommon.myCdbl(obj.Standardrate))
            clsCommon.AddColumnsForChange(coll, "NetRate", clsCommon.myCdbl(obj.NetRate))
            clsCommon.AddColumnsForChange(coll, "BasicRate", clsCommon.myCdbl(obj.BasicRate))

            'clsCommon.AddColumnsForChange(coll, "FatAmt", clsCommon.myCdbl(obj.FatAmt))
            'clsCommon.AddColumnsForChange(coll, "SnfAmt", clsCommon.myCdbl(obj.SnfAmt))
            clsCommon.AddColumnsForChange(coll, "FinalMilkRate", clsCommon.myCdbl(obj.FinalMilkRate))
            clsCommon.AddColumnsForChange(coll, "TANKER_SKU_MANUAL", clsCommon.myCdbl(obj.TANKER_SKU_MANUAL))

            clsCommon.AddColumnsForChange(coll, "SpecialDeduction", clsCommon.myCdbl(obj.SpecialDeduction))
            ''===============================================

            clsCommon.AddColumnsForChange(coll, "Modify_By", clsCommon.myCstr(obj.Modify_By))
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.myCstr(obj.Modify_Date))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", clsCommon.myCstr(obj.comp_code))
            '' production costing columns
            Dim objCost As New MIlkComponentType
            'Item_Production_type = clsDBFuncationality.getSingleValue("select Product_Type from TSPL_ITEM_MASTER where item_Code='" & obj.Item_Code & "'", trans)
            ' objCost = clsInventoryMovementNew.GetAvgCost(Item_Production_type, obj.Item_Code, IIf(clsCommon.myLen(obj.Loc_Code) <= 0, obj.Loc_Code, obj.Loc_Code), obj.Net_Weight, obj.UOM, obj.fat_KG, obj.SNF_KG, obj.SRN_Date, obj.SRN_Date, True, trans)
            Dim DT As DataTable = GetBalanceQty(obj.Loc_Code, obj.Vendor_Code, trans)
            If DT.Rows.Count > 0 Then
                obj.fat_Rate = clsCommon.myCdbl(DT.Rows(0).Item("Fat_V")) 'objCost.FAT_Cost / IIf(obj.fat_KG <= 0, 1, obj.fat_KG)
                obj.SNF_Rate = clsCommon.myCdbl(DT.Rows(0).Item("SNF_V")) 'objCost.SNF_Cost / IIf(obj.SNF_KG <= 0, 1, obj.SNF_KG)
                obj.FatAmt = clsCommon.myCdbl(DT.Rows(0).Item("FAT_V")) * obj.fat_KG ' objCost.FAT_Cost
                obj.SnfAmt = clsCommon.myCdbl(DT.Rows(0).Item("SNF_V")) * obj.SNF_KG 'objCost.SNF_Cost
                clsCommon.AddColumnsForChange(coll, "Fat_Rate", obj.fat_Rate)
                clsCommon.AddColumnsForChange(coll, "SNF_Rate", obj.SNF_Rate)
                clsCommon.AddColumnsForChange(coll, "FatAmt", obj.FatAmt)
                clsCommon.AddColumnsForChange(coll, "SNFAmt", obj.SnfAmt)
                clsCommon.AddColumnsForChange(coll, "Amount", clsCommon.myCdbl(obj.FatAmt) + clsCommon.myCdbl(obj.SnfAmt))
                clsCommon.AddColumnsForChange(coll, "Actual_Amount", clsCommon.myCdbl(obj.FatAmt) + clsCommon.myCdbl(obj.SnfAmt))
            End If

            If obj.isNewEntry OrElse isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", clsCommon.myCstr(objCommonVar.CurrentUserCode))
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "tspl_Job_milk_srn_History", "tspl_Job_milk_srn"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_Job_milk_srn", OMInsertOrUpdate.Update, "tspl_Job_milk_srn.srn_no='" + obj.SRN_NO + "'", trans)
            End If
            If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                issaved = issaved AndAlso clsJobSRNParam.saveData(obj.arrObj, obj.SRN_NO, trans)
            End If
            Item_Production_type = Nothing
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function GetBalanceQty(ByVal Loc_Code As String, ByVal Vendor_Code As String, ByVal trans As SqlTransaction) As DataTable
        Try
            Dim sQuery As String = "select ((sum(FAT_Price)-coalesce(Fat_Amt,0))/(sum(Fat_KG)-coalesce(Fat_KGS,0))) as Fat_V, ((sum(SNF_Price)-coalesce(SNf_Amt,0))/(sum(SNF_KG)-coalesce(Snf_Kgs,0))) as SNF_V from tspl_milk_RGP_Head inner join TSPL_MILK_RGP_DETAIL on TSPL_MILK_RGP_DETAIL.RGP_No=tspl_milk_RGP_Head.RGP_No " _
                                           & " and Vendor_Code='" & Vendor_Code & "' and Location='" & clsCommon.myCstr(Loc_Code) & "'" _
                                           & " left join (select coalesce(sum(FATAmt),0) as Fat_Amt,coalesce(sum(Fat_KG),0) as Fat_KGS,coalesce(sum(SNFAmt),0) as SNF_Amt,coalesce(sum(SNF_KG),0) as SNF_KGS,Vendor_Code,Item_Code,Loc_Code from TSPL_JOB_MILK_Srn group by Vendor_Code,Item_Code,Loc_Code) tt on tt.vendor_COde=tspl_milk_RGP_Head.vendor_Code and tt.loc_COde=tspl_milk_RGP_Head.Location group by Unit_Code,Fat_Amt,Fat_KGS,SNF_AMt,SNF_KGS " 'and TSPL_MILK_RGP_DETAIL.UNIT_code='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value) & "'
            Dim DtBalance As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
            Return DtBalance
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsJobMilkSRN
        Dim obj As New clsJobMilkSRN
        Try

            Dim qst As String = " select *   From tspl_Job_milk_srn   where 1=1  and formType='JobMilkSrn'    "

            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and Loc_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_Job_milk_srn.srn_no in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_Job_milk_srn.srn_no in (select min(srn_no ) from tspl_Job_milk_srn where srn_no  >'" + strCode + "' " & whrCls & " and formType='JobMilkSrn'   and isnull(srn_return_no,'')='')"
                Case NavigatorType.First
                    qst += " and tspl_Job_milk_srn.srn_no in (select MIN(srn_no ) from tspl_Job_milk_srn  where 1=1 " & whrCls & " and formType='JobMilkSrn'    and isnull(srn_return_no,'')='')"
                Case NavigatorType.Last
                    qst += " and tspl_Job_milk_srn.srn_no in (select Max(srn_no ) from tspl_Job_milk_srn  where 1=1 " & whrCls & " and formType='JobMilkSrn'   and isnull(srn_return_no,'')='')"
                Case NavigatorType.Previous
                    qst += " and tspl_Job_milk_srn.srn_no in (select Max(srn_no ) from tspl_Job_milk_srn where srn_No  <'" + strCode + "'  " & whrCls & " and formType='JobMilkSrn'   and isnull(srn_return_no,'')='')"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.SRN_NO = clsCommon.myCstr(dt.Rows(0)("SRN_NO"))
                obj.SRN_Date = clsCommon.myCDate(dt.Rows(0)("srn_date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("gate_entry_no"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.QC_No = clsCommon.myCstr(dt.Rows(0)("QC_No"))
                obj.Qc_Date = clsCommon.myCDate(dt.Rows(0)("qc_date"), "dd/MMM/yyyy hh:mm:ss tt")
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
                obj.HSN_Code = clsItemMaster.GetItemHSNCode(obj.Item_Code, trans)
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
                obj.TANKER_SKU_MANUAL = clsCommon.myCdbl(dt.Rows(0)("TANKER_SKU_MANUAL"))
                obj.isApproved = clsCommon.myCdbl(dt.Rows(0)("isApproved"))
                If obj.isApproved = 1 Then
                    obj.Approval_Ref_Doc_No = clsCommon.myCstr(dt.Rows(0)("Approval_Ref_Doc_No"))
                    obj.Approved_Rate = clsCommon.myCdbl(dt.Rows(0)("Approved_Rate"))
                End If
                If clsCommon.myLen(dt.Rows(0)("PO_NO")) > 0 Then
                    obj.PO_NO = clsCommon.myCstr(dt.Rows(0)("PO_NO"))
                    obj.PO_Date = clsCommon.GetPrintDate(dt.Rows(0)("PO_Date"), "dd/MMM/yyyy")
                End If
                obj.Balance = clsCommon.myCdbl(dt.Rows(0)("Balance_Qty"))
                obj.BalanceFatKg = clsCommon.myCdbl(dt.Rows(0)("Balance_FATKG"))
                obj.BalanceSNFKg = clsCommon.myCdbl(dt.Rows(0)("Balance_SNFKG"))
                obj.Qty = clsCommon.myCdbl(dt.Rows(0)("Qty"))
                ' Return obj
                obj.arrObj = clsJobSRNParam.getData(obj.SRN_NO, trans)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function



End Class
Public Class clsJobSRNParam
    Public SRN_No As String = String.Empty
    Public Parameter As String = String.Empty
    Public Lower_Range As String = String.Empty
    Public Upper_Range As String = String.Empty
    Public value As String = String.Empty
    Public Incen_Deduc As Double = 0
    Public QCValue As String = String.Empty
    Public Shared Function saveData(ByVal arrObj As List(Of clsJobSRNParam), ByVal strSRNNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim coll As Hashtable

            If arrObj IsNot Nothing Then
                Dim qry As String = "delete from TSPL_Job_SRN_Parameter_Range_Detail where SRN_No='" & strSRNNo & "'"
                issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
                For Each obj As clsJobSRNParam In arrObj
                    coll = New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "SRN_No", obj.SRN_No)
                    clsCommon.AddColumnsForChange(coll, "Parameter", obj.Parameter)
                    clsCommon.AddColumnsForChange(coll, "Lower_Range", obj.Lower_Range)
                    clsCommon.AddColumnsForChange(coll, "Upper_Range", obj.Upper_Range)
                    clsCommon.AddColumnsForChange(coll, "value", obj.value)
                    clsCommon.AddColumnsForChange(coll, "Incen_Deduc", obj.Incen_Deduc)
                    clsCommon.AddColumnsForChange(coll, "QCValue", obj.QCValue)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Job_SRN_Parameter_Range_Detail", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getData(ByVal strSRNNo As String, Optional ByVal trans As SqlTransaction = Nothing) As List(Of clsJobSRNParam)
        Dim arrObj As List(Of clsJobSRNParam) = Nothing
        Try
            Dim obj As clsJobSRNParam = Nothing
            Dim qry As String = "select * from TSPL_Job_SRN_Parameter_Range_Detail where SRN_No='" & strSRNNo & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                arrObj = New List(Of clsJobSRNParam)
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj = New clsJobSRNParam()
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
            Dim qry As String = "delete from TSPL_Job_SRN_Parameter_Range_Detail where SRN_No='" & strSRNNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry)
            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class


Public Class clsJobMilkSRNReturn
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
            Dim obj As clsJobMilkSRN = clsJobMilkSRN.getData(StrDocNo, NavigatorType.Current, trans)
            Dim objReturn As clsJobMilkSRNReturn = clsJobMilkSRNReturn.getData(StrSRNReturnNo, NavigatorType.Current, trans)
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
            Dim strItemUnitCode As String = obj.UOM
            Dim ConvFac As Double = clsItemMaster.GetConvertionFactor(obj.Item_Code, strItemUnitCode, trans)
            If ConvFac = 0 Then
                Throw New Exception("Conversion Factor found zero for item :" + obj.Item_Code + " and Uom:'" + strItemUnitCode)
            End If
            Dim objInventoryMovemnt As New clsInventoryMovementNew()
            objInventoryMovemnt.InOut = "O"
            '-----------Getting Sub Location Where Milk Was unloaded
            Dim strSiloNo As String = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_JOB_MILK_UNLOADING where Gate_Entry_No='" & obj.Gate_Entry_No & "'", trans)
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
            'Create GL Entry

            qry = " select TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing ,tspl_Job_milk_srn.Actual_Amount,tspl_Job_milk_srn.Loc_Code   from TSPL_PURCHASE_ACCOUNTS left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Purchase_Class_Code =TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code left outer join tspl_Job_milk_srn on tspl_Job_milk_srn .Item_Code=TSPL_ITEM_MASTER.Item_Code where tspl_Job_milk_srn.SRN_NO='" & obj.SRN_NO & "' "
            Dim ArryLst As ArrayList = New ArrayList()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim strInvCntrlAc As String = dt.Rows(0)("Inv_Control_Account")
                Dim strPaybleClrAc As String = dt.Rows(0)("Inv_Payable_Clearing")
                strInvCntrlAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCntrlAc, dt.Rows(0)("Loc_Code"), trans)
                strPaybleClrAc = clsERPFuncationality.ChangeGLAccountLocationSegment(strPaybleClrAc, dt.Rows(0)("Loc_Code"), trans)
                ArryLst.Add(New String() {strInvCntrlAc, dt.Rows(0)("Actual_Amount") * -1})
                ArryLst.Add(New String() {strPaybleClrAc, dt.Rows(0)("Actual_Amount")})
                transportSql.FunGrnlEntryWithTrans(obj.Loc_Code, False, trans, clsCommon.GetPrintDate(objReturn.SRN_Return_Date, "dd/MMM/yyyy"), "Against Job Milk SRN Return No  -" + objReturn.SRN_Return_NO + "", "SR-RT", " Milk SRN Returm", objReturn.SRN_Return_NO, "", "C", obj.Item_Code, obj.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, "", " Vendor - " & obj.Vendor_Code & ", " & clsVendorMaster.GetName(obj.Vendor_Code, trans))
            End If
            Dim strQry As String = " update tspl_Job_milk_srn set SRN_Return_NO='" & objReturn.SRN_Return_NO & "' where srn_no='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            strQry = " delete from TSPL_TRANSACTION_APPROVAL where Document_No='" & StrDocNo & "' "
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            Return isPosted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function



    Public Shared Function saveData(ByVal objReturn As clsJobMilkSRNReturn, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim issaved As Boolean = True
            Dim obj As clsJobMilkSRN = clsJobMilkSRN.getData(objReturn.SRN_NO, NavigatorType.Current, trans)
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
            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_Job_milk_srn_Return", OMInsertOrUpdate.Insert, "", trans)
            issaved = issaved And postData(objReturn.SRN_NO, objReturn.SRN_Return_NO, trans)
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsJobMilkSRNReturn
        Dim obj As New clsJobMilkSRNReturn
        Try

            Dim qst As String = " select *   From tspl_Job_milk_srn_Return   where SRN_Return_NO='" & strCode & "'"


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