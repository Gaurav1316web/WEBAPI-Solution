' Done By Pankaj Jha on dated 14/07/2014 against ticket no: BM00000002725
Imports common
Imports System.Data.SqlClient

Public Class clsWeighment
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public AcknowEntryDocument_No As String = Nothing
    Public FAT_Rate As Double = 0
    Public SNF_Rate As Double = 0
    Public FAT_Value As Double = 0
    Public SNF_Value As Double = 0
    Public FAT_Kg As Double = 0
    Public SNF_Kg As Double = 0
    Public Amount As Double = 0
    Public Tanker_Return As Integer = 0
    Public Arr As List(Of clsWeighmentChemberNoDetails) = Nothing
    Public Gross_Weight_Header As Double = 0
    Public isNewEntry As Boolean = True
    Public Weighment_No As String = String.Empty
    Public Weighment_Date As Date = Nothing
    Public Gate_Entry_No As String = String.Empty
    Public Doc_Type As String = String.Empty
    Public Date_And_Time As Date
    Public Challan_No As String = String.Empty
    Public Challan_Date As Date
    Public Tanker_No As String = String.Empty
    Public isPosted As Integer = 0
    Public Posting_Date As Date = Nothing
    Public Dispatched_From_Mcc As String = String.Empty
    Public location_Code As String = String.Empty
    Public Location_Desc As String = String.Empty
    Public Vendor_Code As String = String.Empty
    Public Vendor_Desc As String = String.Empty
    Public Item_Code As String = String.Empty
    Public Item_Desc As String = String.Empty
    Public Qty_In_Kg As Double = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public Created_By As String = String.Empty
    Public UOM As String = String.Empty
    Public Created_Date As String = String.Empty
    Public Modify_By As String = String.Empty
    Public Modify_Date As String = String.Empty
    Public comp_code As String = String.Empty
    Public Gross_Weight As Double = 0
    Public Dip_Value As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public status As Integer = 0
    Public Sent_to_QC_By As String = String.Empty
    Public Sent_To_QC_Date As Date = Nothing
    Public QC_Done_By As String = String.Empty
    Public QC_Done_Date As Date = Nothing
    Public Weighment_Slip_No As String = String.Empty
    Public Tare_Weight_date As DateTime? = Nothing
    Public Vendor_Weight As Double = 0
    Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strDocNo) <= 0 Then
                Throw New Exception("Please select a Weighment No")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_Code,Date_And_Time from tspl_weighment_detail where Weighment_No='" + strDocNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmWeighment, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("Date_And_Time")), trans)

            End If
            Dim Qry As String = "select isPosted from tspl_weighment_detail where Weighment_no='" + strDocNo + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
          
            ''done by richa agarwal Against Ticket No. BHA/06/07/18-000133 on 21 Jan,2019 when created MILK Transfer In  and Gate Out auto
            Dim strQry As String = String.Empty
            Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
            Dim AllowBulkProcMCCwithoutTankerDispatch As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcMCCwithoutTankerDispatch, clsFixedParameterCode.AllowBulkProcMCCwithoutTankerDispatch, trans))
            Dim strMCCProc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Doc_Type from tspl_weighment_detail where weighment_no='" + strDocNo + "' ", trans))

            'If clsDBFuncationality.getSingleValue("select IsNetWeight from tspl_gate_entry_details where Gate_Entry_No in (select Gate_Entry_No from tspl_weighment_detail WHERE Weighment_No='" & strDocNo & "')", trans) = 1 Then
            '    strQry = "delete from TSPL_MILK_UNLOADING_CHEMBER_DETAILS where Unloading_No in (select Unloading_No from TSPL_MILK_UNLOADING where weighment_no='" + strDocNo + "')"
            '    clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            '    strQry = "delete from TSPL_MILK_UNLOADING where weighment_no='" + strDocNo + "'"
            '    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            'End If

            If MCCChamberwise = 1 And clsCommon.CompairString(strMCCProc, "MccProc") = CompairStringResult.Equal AndAlso AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                Dim strChalan_NO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Challan_No from tspl_weighment_detail where weighment_no='" + strDocNo + "' ", trans))
                Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select isintermittent from TSPL_MCC_Dispatch_Challan where Chalan_NO ='" & strChalan_NO & "' ", trans))
                If rValue = 0 Then
                    Dim MilkTrasferInNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Receipt_Challan_No from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No='" & strChalan_NO & "'", trans))
                    If clsCommon.myLen(MilkTrasferInNo) > 0 Then
                        '' to check stock balance of qty
                        Qry = "select Main_Location,Item_Code,Location_Code,Qty,UOM,Fat_KG,SNF_KG,Punching_Date from tspl_inventory_movement_new where Trans_Type='MilkTransferIn' and InOut='I' and Source_Doc_No='" + MilkTrasferInNo + "'"
                        dt = clsDBFuncationality.GetDataTable(Qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                Dim BalanceQty As Decimal
                                BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Item_Code")), clsCommon.myCstr(dr.Item("Main_Location")), clsCommon.myCstr(dr.Item("Location_Code")), MilkTrasferInNo, clsCommon.myCDate(dr.Item("Punching_Date")), trans, clsCommon.myCstr(dr.Item("UOM")))
                                BalanceQty = Math.Round(Math.Round(BalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                                If clsCommon.myCdbl(dr.Item("Qty")) > BalanceQty Then
                                    If Math.Abs(Math.Round(clsCommon.myCdbl(dr.Item("Qty")) - BalanceQty, 2, MidpointRounding.AwayFromZero)) > 0.01 Then
                                        Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("Location_Code")) & " Available Qty: " & BalanceQty & "  Transaction Qty: " & clsCommon.myCdbl(dr.Item("Qty")) & " ")
                                    End If
                                End If
                            Next
                        End If
                        ''-----------------


                        ''richa 7 Feb,2019 ERO/07/02/19-000488
                        strQry = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER where against_transfer_in_doc_no='" + MilkTrasferInNo + "'"
                        Dim strAdjustMentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry, trans))
                        If clsCommon.myLen(strAdjustMentNo) > 0 Then
                            strQry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + strAdjustMentNo + "'"
                            Dim VoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry, trans))
                            If clsCommon.myLen(VoucherNo) > 0 Then
                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                                strQry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + VoucherNo + "'  "
                                clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                                strQry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + VoucherNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                            End If

                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strAdjustMentNo, "tspl_inventory_movement", "Source_Doc_No", trans)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strAdjustMentNo, "tspl_inventory_movement_new", "Source_Doc_No", trans)
                            strQry = "delete from tspl_inventory_movement where Trans_Type ='IC-AD' and Source_Doc_No='" + strAdjustMentNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                            strQry = "delete from tspl_inventory_movement_new where Trans_Type ='IC-AD' and Source_Doc_No='" + strAdjustMentNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strAdjustMentNo, "TSPL_ADJUSTMENT_HEADER", "Adjustment_No", "TSPL_ADJUSTMENT_DETAIL", "Adjustment_No", trans)
                            strQry = "delete from TSPL_ADJUSTMENT_DETAIL  where Adjustment_No='" & strAdjustMentNo & "'"
                            clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                            strQry = "delete from TSPL_ADJUSTMENT_HEADER  where Adjustment_No='" & strAdjustMentNo & "'"
                            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                        End If
                        ''-------------------


                        strQry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + MilkTrasferInNo + "' and Source_Code='MT-IN'"
                        Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry, trans))
                        If clsCommon.myLen(strVoucherNo) > 0 Then
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strVoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                            strQry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'  "
                            clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                            strQry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                        End If

                        clsBatchInventoryNew.DeleteData("MilkTransferIn", MilkTrasferInNo, trans)
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, MilkTrasferInNo, "tspl_inventory_movement_new", "Source_Doc_No", trans)
                        strQry = "delete from tspl_inventory_movement_new where Trans_Type ='MilkTransferIn' and Source_Doc_No='" + MilkTrasferInNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, MilkTrasferInNo, "tspl_milk_transfer_in", "Receipt_Challan_no", trans)
                        strQry = "delete from tspl_milk_transfer_in where Receipt_Challan_no='" + MilkTrasferInNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_gate_out", "Weighment_No", trans)
                        strQry = "delete from tspl_gate_out where Weighment_No='" + strDocNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                    End If
                End If
            End If
            ''done by richa agarwal Against Ticket No. BHA/06/07/18-000133 on 21 Jan,2019 when created bulk milk srn auto
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            Dim AllowRandomOnlyOneSecondaryQC As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowRandomOnlyOneSecondaryQC, clsFixedParameterCode.AllowRandomOnlyOneSecondaryQC, trans))
            If TankerFromMaster = 1 And clsCommon.CompairString(strMCCProc, "BulkProc") = CompairStringResult.Equal AndAlso AllowRandomOnlyOneSecondaryQC = 0 Then
                Dim strGateEntryNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_No from tspl_weighment_detail where weighment_no='" + strDocNo + "' ", trans))
                Dim intQC As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_SECONDARY_SETTING_QC_HEAD where gate_entry_no='" & strGateEntryNo & "' and posted=1", trans))
                Dim intSRN As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 SRN_NO from TSPL_Bulk_MILK_SRN where Weighment_No='" & strDocNo & "'", trans))
                If intQC = 1 And clsCommon.myLen(intSRN) > 0 Then
                    '' to check stock balance of qty 23 Oct,2020
                    Qry = "select Main_Location,Item_Code,Location_Code,Qty,UOM,Fat_KG,SNF_KG,Punching_Date from tspl_inventory_movement_new where Trans_Type='BulkSRN' and Source_Doc_No='" + intSRN + "'"
                    dt = clsDBFuncationality.GetDataTable(Qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim BalanceQty As Decimal
                            BalanceQty = clsInventoryMovementNew.getBalance(clsCommon.myCstr(dr.Item("Item_Code")), clsCommon.myCstr(dr.Item("Main_Location")), clsCommon.myCstr(dr.Item("Location_Code")), intSRN, clsCommon.myCDate(dr.Item("Punching_Date")), trans, clsCommon.myCstr(dr.Item("UOM")))
                            BalanceQty = Math.Round(Math.Round(BalanceQty, 3, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero)
                            If clsCommon.myCdbl(dr.Item("Qty")) > BalanceQty Then
                                If Math.Abs(Math.Round(clsCommon.myCdbl(dr.Item("Qty")) - BalanceQty, 2, MidpointRounding.AwayFromZero)) > 0.01 Then
                                    Throw New Exception("Item: " & clsCommon.myCstr(dr.Item("Item_Code")) & ", Location:" & clsCommon.myCstr(dr.Item("Location_Code")) & " Available Qty: " & BalanceQty & "  Transaction Qty: " & clsCommon.myCdbl(dr.Item("Qty")) & " ")
                                End If
                            End If
                        Next
                    End If
                    ''-----------------

                    strQry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + intSRN + "' and Source_Code='BM-SR'"
                    Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry, trans))
                    If clsCommon.myLen(strVoucherNo) > 0 Then
                        clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strVoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                        strQry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'  "
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                        strQry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                    End If

                    clsBatchInventoryNew.DeleteData("BulkSRN", intSRN, trans)
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, intSRN, "tspl_inventory_movement_new", "Source_Doc_No", trans)
                    strQry = "delete from tspl_inventory_movement_new where Trans_Type ='BulkSRN' and Source_Doc_No='" + intSRN + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, intSRN, "TSPL_Bulk_MILK_SRN", "SRN_NO", "TSPL_BULK_MILK_SRN_CHEMBER_DETAILS", "SRN_NO", trans)
                    strQry = "delete from TSPL_BULK_MILK_SRN_CHEMBER_DETAILS  where SRN_NO='" + intSRN + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                    strQry = "delete from TSPL_Bulk_MILK_SRN where SRN_NO='" + intSRN + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                End If
            End If

            ''done by richa agarwal Against Ticket No. BHA/06/07/18-000133 on 21 Jan,2019 when created milk transfer in,Unloading and gate Out auto in case of intermittent
            Dim GateEntryNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Gate_Entry_No from tspl_weighment_detail where weighment_no='" + strDocNo + "' ", trans))
            If clsQualityCheck.isMccInDoc(GateEntryNo, trans) Then
                If clsQualityCheck.isIntermittentDoc(clsQualityCheck.getChallanNo(GateEntryNo, trans), trans) Then
                    If clsQualityCheck.isWeighmentDone(GateEntryNo, trans) And clsQualityCheck.isQCDone(GateEntryNo, trans) Then 'And clsMccDispatch.GetReachedAtFinalLoc(GateEntryNo, trans) = 0
                        Dim strChalan_NO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Challan_No from tspl_weighment_detail where weighment_no='" + strDocNo + "' ", trans))
                        Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("  select isintermittent from TSPL_MCC_Dispatch_Challan where Chalan_NO ='" & strChalan_NO & "' ", trans))
                        If rValue = 1 Then
                            Dim MilkTrasferInNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Receipt_Challan_No from TSPL_MILK_TRANSFER_IN where Dispatch_Challan_No='" & strChalan_NO & "'", trans))
                            If clsCommon.myLen(MilkTrasferInNo) > 0 Then
                                strQry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + MilkTrasferInNo + "' and Source_Code='MT-IN'"
                                Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry, trans))
                                If clsCommon.myLen(strVoucherNo) > 0 Then
                                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strVoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                                    strQry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'  "
                                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                                    strQry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                                End If
                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, MilkTrasferInNo, "tspl_inventory_movement_new", "Source_Doc_No", trans)
                                strQry = "delete from tspl_inventory_movement_new where Trans_Type ='MilkTransferIn' and Source_Doc_No='" + MilkTrasferInNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, MilkTrasferInNo, "tspl_milk_transfer_in", "Receipt_Challan_no", trans)
                                strQry = "delete from tspl_milk_transfer_in where Receipt_Challan_no='" + MilkTrasferInNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                                Dim strtankerNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tanker_No from TSPL_Gate_Out where Weighment_No='" + strDocNo + "'", trans))

                                Dim coll As New Hashtable()
                                coll = New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "Modified_By", clsCommon.myCstr(objCommonVar.CurrentUser))
                                clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans, "dd/MM/yyyy hh:mm:ss tt"), "dd/MM/yyyy hh:mm:ss tt"))
                                clsCommon.AddColumnsForChange(coll, "isGateOut", 0)
                                clsCommon.AddColumnsForChange(coll, "Ref_Doc_No", "", True)
                                clsCommonFunctionality.UpdateDataTable(coll, "tspl_tanker_master", OMInsertOrUpdate.Update, "tspl_tanker_master.tanker_no='" + strtankerNo + "'", trans)

                                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strDocNo, "tspl_gate_out", "Weighment_No", trans)
                                strQry = "delete from tspl_gate_out where Weighment_No='" + strDocNo + "'"
                                clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                            End If
                        End If
                        strQry = "delete from TSPL_MILK_UNLOADING_CHEMBER_DETAILS where Unloading_No in (select Unloading_No from TSPL_MILK_UNLOADING where weighment_no='" + strDocNo + "')"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                        strQry = "delete from TSPL_MILK_UNLOADING where weighment_no='" + strDocNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                    End If
                End If
            End If
            ''-------------------------end of work done by richa agarwal Against Ticket No. BHA/06/07/18-000133 on 21 Jan,2019

            Dim isUsed As Integer = clsDBFuncationality.getSingleValue(" select count(*) from TSPL_Bulk_MILK_SRN where Weighment_no='" & strDocNo & "'", trans)
            If isUsed > 0 Then
                Throw New Exception("Weighment No is in use")
            End If

            Qry = "Update tspl_weighment_detail set isPosted = 0,Posting_Date=null where weighment_no='" + strDocNo + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strDocNo), "TSPL_Weighment_Detail", "Weighment_No", trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function saveData(ByVal obj As clsWeighment, ByVal trans As SqlTransaction, Optional ByVal isHistory As Boolean = False) As Boolean
        Try
            Dim IsPosted As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isposted from tspl_weighment_detail where Weighment_No='" & obj.Weighment_No & "'", trans))
            If IsPosted = 1 AndAlso Not isHistory Then
                Throw New Exception("Record is Already posted")
            End If
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmWeighment, obj.location_Code, obj.Weighment_Date, trans)
            If Not obj.isNewEntry Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(obj.Weighment_No), "TSPL_Weighment_Detail", "Weighment_No", "TSPL_Weighment_Chember_Details", "Weighment_No", trans)
            End If

            Dim issaved As Boolean = True
            Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Weighment_No", obj.Weighment_No)
            '==============================Added by preeti gupta======================
            If DateTime = "1" Then
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            Else
                clsCommon.AddColumnsForChange(coll, "Weighment_Date", clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy"))
            End If
            '===================================End=======================================
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "Gross_Weight_Header", obj.Gross_Weight_Header)
            clsCommon.AddColumnsForChange(coll, "AcknowEntryDocument_No", obj.AcknowEntryDocument_No, True)
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_No", obj.Gate_Entry_No)
            clsCommon.AddColumnsForChange(coll, "Doc_Type", obj.Doc_Type)
            clsCommon.AddColumnsForChange(coll, "Date_And_Time", clsCommon.GetPrintDate(obj.Date_And_Time, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Challan_No", obj.Challan_No)
            clsCommon.AddColumnsForChange(coll, "Challan_Date", clsCommon.GetPrintDate(obj.Challan_Date, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Tanker_No", obj.Tanker_No)
            clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            clsCommon.AddColumnsForChange(coll, "Tanker_Return", obj.Tanker_Return)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If

            clsCommon.AddColumnsForChange(coll, "Dispatched_From_Mcc", obj.Dispatched_From_Mcc, True)

            clsCommon.AddColumnsForChange(coll, "location_Code", obj.location_Code, True)
            clsCommon.AddColumnsForChange(coll, "Weighment_Slip_No", obj.Weighment_Slip_No, True)
            clsCommon.AddColumnsForChange(coll, "Location_Desc", obj.Location_Desc, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Code", obj.Vendor_Code, True)
            clsCommon.AddColumnsForChange(coll, "Vendor_Desc", obj.Vendor_Desc, True)


            clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
            clsCommon.AddColumnsForChange(coll, "Item_Desc", obj.Item_Desc)
            clsCommon.AddColumnsForChange(coll, "Qty_In_Kg", obj.Qty_In_Kg)
            clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
            clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
            If obj.status <> 0 Then
                clsCommon.AddColumnsForChange(coll, "status", obj.status)
            End If
            clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
            clsCommon.AddColumnsForChange(coll, "Dip_Value", obj.Dip_Value)
            clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
            clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
            clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.comp_code)
            If obj.Tare_Weight_date IsNot Nothing Then

                '==============================Added by preeti gupta======================
                If DateTime = "1" Then
                    clsCommon.AddColumnsForChange(coll, "Tare_Weight_date", clsCommon.GetPrintDate(obj.Tare_Weight_date, "dd/MMM/yyyy hh:mm tt"), True)
                Else
                    clsCommon.AddColumnsForChange(coll, "Tare_Weight_date", clsCommon.GetPrintDate(obj.Tare_Weight_date, "dd/MMM/yyyy"), True)
                End If
                '===================================End=======================================

            Else

                clsCommon.AddColumnsForChange(coll, "Tare_Weight_date", Nothing, True)
            End If
            clsCommon.AddColumnsForChange(coll, "Vendor_Weight", obj.Vendor_Weight)
            If obj.isNewEntry Or isHistory Then
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, IIf(isHistory, "tspl_weighment_detail_history", "tspl_weighment_detail"), OMInsertOrUpdate.Insert, "", trans)
            Else
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_weighment_detail", OMInsertOrUpdate.Update, "tspl_weighment_detail.Weighment_No='" + obj.Weighment_No + "'", trans)
            End If
            If Not isHistory Then
                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_quality_check where isposted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'  and weighment_no=''", trans)) > 0 Then
                    issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery("update tspl_quality_check set weighment_no='" & obj.Weighment_No & "',weighment_date='" & clsCommon.GetPrintDate(obj.Weighment_Date, "dd/MMM/yyyy") & "' where gate_entry_no='" & obj.Gate_Entry_No & "'", trans)
                End If
            End If
            issaved = issaved AndAlso clsWeighmentChemberNoDetails.SaveData(obj.Weighment_No, obj.Arr, trans)
            issaved = issaved AndAlso clsDBFuncationality.ExecuteNonQuery("update tspl_gate_entry_details set Tanker_Return='" & obj.Tanker_Return & "' where gate_entry_no='" & obj.Gate_Entry_No & "'", trans)

            ''Notification on save
            Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmWeighment + "2" + "'", trans))
            If clsCommon.myLen(strNotifiContent) > 0 Then
                Dim FATPER As String = ""
                Dim SNFPER As String = ""
                Dim qty As String = ""
                Dim ItemDetail As String = ""
                'Dim strNotifiContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmWeighment + "2" + "'", trans))
                'Dim strNotifi_DetalContent As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Detail_Text from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.mbtnPurchaseOrder + "'", trans))
                Dim strNotifiCaption As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_Caption from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmWeighment + "2" + "'", trans))
                Dim strNotificationOn As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Notification_On from TSPL_ES_Content where Form_ID='" + clsUserMgtCode.frmWeighment + "2" + "'", trans))

                If (obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0) Then
                    For Each objdetail As clsWeighmentChemberNoDetails In obj.Arr
                        FATPER = clsCommon.myCstr(objdetail.fat_per)
                        SNFPER = clsCommon.myCstr(objdetail.snf_Per)
                        qty = clsCommon.myCstr(objdetail.Chamber_Qty)
                        ItemDetail += "Qty- " + qty + " FAT Per- " + FATPER + " SNF Per- " + SNFPER + Environment.NewLine
                    Next
                End If


                If clsCommon.myLen(strNotifiContent) > 0 Then
                    Dim objNotification As New clsNotificationHead()
                    objNotification.Notification_Text = strNotifiContent
                    objNotification.Notification_Caption = strNotifiCaption
                    objNotification.Notification_On = strNotificationOn
                    'objNotification.Notification_ConditionDate = clsCommon.myCDate(dt.Rows(0)("Delivery_date"))
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(obj.Vendor_Code))
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(obj.Vendor_Desc))
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.TankerNo, clsCommon.myCstr(obj.Tanker_No))
                    objNotification.Notification_Text = objNotification.Notification_Text.Replace(frmEMailAndSMSSetting.ItemDetail, ItemDetail)
                    objNotification.Notification_Tanker_Doc_Type = obj.Doc_Type
                    objNotification.SaveData(clsUserMgtCode.frmWeighment + "2", objNotification, trans)
                    objNotification = Nothing
                End If
            End If
            ''Notification on save

            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsWeighment
        Return getData(strCode, docType, navtype, False, trans)
    End Function
    Public Shared Function getData(ByVal strCode As String, ByVal docType As String, ByVal navtype As NavigatorType, ByVal isPendingOnly As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As clsWeighment
        Try
            Dim obj As New clsWeighment
            Dim whrCls As String = String.Empty
            Dim whrcls2 As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrcls2 = " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ") & whrcls2
            Else
                whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ")
            End If

            If isPendingOnly Then
                whrCls = whrCls & "    and isPosted=0 "

            End If
            Dim qst As String = " select *   From tspl_weighment_detail   where 1=1 "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_weighment_detail.Weighment_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_weighment_detail.Weighment_No in (select min(Weighment_No ) from tspl_weighment_detail where Weighment_No  >'" + strCode + "'" & whrCls & ")"
                Case NavigatorType.First
                    qst += " and tspl_weighment_detail.Weighment_No in (select MIN(Weighment_No ) from tspl_weighment_detail where 1=1 " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and tspl_weighment_detail.Weighment_No in (select Max(Weighment_No ) from tspl_weighment_detail where 1=1" & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and tspl_weighment_detail.Weighment_No in (select Max(Weighment_No ) from tspl_weighment_detail where Weighment_No  <'" + strCode + "'" & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.FAT_Kg = clsCommon.myCdbl(dt.Rows(0)("FAT_Kg"))
                obj.SNF_Kg = clsCommon.myCdbl(dt.Rows(0)("SNF_Kg"))
                obj.FAT_Rate = clsCommon.myCdbl(dt.Rows(0)("FAT_Rate"))
                obj.SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("SNF_Rate"))
                obj.FAT_Value = clsCommon.myCdbl(dt.Rows(0)("FAT_Value"))
                obj.SNF_Value = clsCommon.myCdbl(dt.Rows(0)("SNF_Value"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Gross_Weight_Header = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight_Header"))
                obj.Tanker_Return = clsCommon.myCdbl(dt.Rows(0)("Tanker_Return"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                '                If clsCommon.CompairString(docType, "MccProc") = CompairStringResult.Equal Then
                obj.Dispatched_From_Mcc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc"))
                'ElseIf clsCommon.CompairString(docType, "BulkProc") = CompairStringResult.Equal Then
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Weighment_Slip_No = clsCommon.myCstr(dt.Rows(0)("Weighment_Slip_No"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.Dip_Value = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
                obj.Vendor_Weight = clsCommon.myCdbl(dt.Rows(0)("Vendor_Weight"))
                If clsDBFuncationality.getSingleValue("select count(*) from tspl_quality_check where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'", trans) = 0 Then
                    obj.status = 0
                Else
                    obj.status = 1
                End If
                ''obj.status = clsCommon.myCdbl(dt.Rows(0)("status"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If

                If dt.Rows(0)("Tare_Weight_date") IsNot DBNull.Value Then
                    obj.Tare_Weight_date = clsCommon.myCDate(dt.Rows(0)("Tare_Weight_date"))
                End If
                obj.Arr = clsWeighmentChemberNoDetails.GetData(obj.Weighment_No, trans)
            Else
                obj = Nothing
            End If

            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, ByVal isPendingOnly As Boolean, Optional ByVal trans As SqlTransaction = Nothing) As clsWeighment
        Try
            Dim obj As New clsWeighment
            Dim whrCls As String = String.Empty
            Dim whrcls2 As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO(trans) Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = " and location_code in (" & objCommonVar.strCurrUserLocations & ")"
                End If
            End If
            'If Not clsMccMaster.isCurrentUserHO Then
            '    whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ") & whrcls2
            'Else
            '    whrCls = IIf(clsCommon.myLen(docType) > 0, " and doc_type='" & docType & "'  ", "  ")
            'End If

            If isPendingOnly Then
                whrCls = whrCls & "    and isPosted=0 "

            End If
            Dim qst As String = " select *   From tspl_weighment_detail   where 1=1 "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_weighment_detail.Weighment_No in ('" + strCode + "') "
                Case NavigatorType.Next
                    qst += " and tspl_weighment_detail.Weighment_No in (select min(Weighment_No ) from tspl_weighment_detail where Weighment_No  >'" + strCode + "'" & whrCls & ")"
                Case NavigatorType.First
                    qst += " and tspl_weighment_detail.Weighment_No in (select MIN(Weighment_No ) from tspl_weighment_detail where 1=1 " & whrCls & ")"
                Case NavigatorType.Last
                    qst += " and tspl_weighment_detail.Weighment_No in (select Max(Weighment_No ) from tspl_weighment_detail where 1=1" & whrCls & ")"
                Case NavigatorType.Previous
                    qst += " and tspl_weighment_detail.Weighment_No in (select Max(Weighment_No ) from tspl_weighment_detail where Weighment_No  <'" + strCode + "'" & whrCls & ")"
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.FAT_Kg = clsCommon.myCdbl(dt.Rows(0)("FAT_Kg"))
                obj.SNF_Kg = clsCommon.myCdbl(dt.Rows(0)("SNF_Kg"))
                obj.FAT_Rate = clsCommon.myCdbl(dt.Rows(0)("FAT_Rate"))
                obj.SNF_Rate = clsCommon.myCdbl(dt.Rows(0)("SNF_Rate"))
                obj.FAT_Value = clsCommon.myCdbl(dt.Rows(0)("FAT_Value"))
                obj.SNF_Value = clsCommon.myCdbl(dt.Rows(0)("SNF_Value"))
                obj.Amount = clsCommon.myCdbl(dt.Rows(0)("Amount"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.Weighment_Date = clsCommon.myCDate(dt.Rows(0)("Weighment_Date"))
                obj.Gate_Entry_No = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_No"))
                obj.Gross_Weight_Header = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight_Header"))
                obj.Tanker_Return = clsCommon.myCdbl(dt.Rows(0)("Tanker_Return"))
                obj.Date_And_Time = clsCommon.myCDate(dt.Rows(0)("Date_And_Time"))
                obj.Challan_No = clsCommon.myCstr(dt.Rows(0)("Challan_No"))
                obj.Challan_Date = clsCommon.myCDate(dt.Rows(0)("Challan_Date"))
                obj.Tanker_No = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                '                If clsCommon.CompairString(docType, "MccProc") = CompairStringResult.Equal Then
                obj.Dispatched_From_Mcc = clsCommon.myCstr(dt.Rows(0)("Dispatched_From_Mcc"))
                'ElseIf clsCommon.CompairString(docType, "BulkProc") = CompairStringResult.Equal Then
                obj.Vendor_Code = clsCommon.myCstr(dt.Rows(0)("Vendor_Code"))
                obj.Vendor_Desc = clsCommon.myCstr(dt.Rows(0)("Vendor_Desc"))
                obj.location_Code = clsCommon.myCstr(dt.Rows(0)("location_Code"))
                obj.Location_Desc = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
                obj.Item_Code = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                obj.Weighment_Slip_No = clsCommon.myCstr(dt.Rows(0)("Weighment_Slip_No"))
                obj.Item_Desc = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                obj.UOM = clsCommon.myCstr(dt.Rows(0)("UOM"))
                obj.Qty_In_Kg = clsCommon.myCdbl(dt.Rows(0)("Qty_In_Kg"))
                obj.fat_per = clsCommon.myCdbl(dt.Rows(0)("fat_per"))
                obj.snf_Per = clsCommon.myCdbl(dt.Rows(0)("snf_Per"))
                obj.Dip_Value = clsCommon.myCdbl(dt.Rows(0)("Dip_Value"))
                obj.Gross_Weight = clsCommon.myCdbl(dt.Rows(0)("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dt.Rows(0)("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dt.Rows(0)("Net_Weight"))
                If clsDBFuncationality.getSingleValue("select count(*) from tspl_quality_check where isPosted=1 and gate_entry_no='" & obj.Gate_Entry_No & "'", trans) = 0 Then
                    obj.status = 0
                Else
                    obj.status = 1
                End If
                ''obj.status = clsCommon.myCdbl(dt.Rows(0)("status"))
                obj.Doc_Type = clsCommon.myCstr(dt.Rows(0)("Doc_Type"))
                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                If dt.Rows(0)("Tare_Weight_date") IsNot DBNull.Value Then
                    obj.Tare_Weight_date = clsCommon.myCDate(dt.Rows(0)("Tare_Weight_date"))
                End If
                obj.Arr = clsWeighmentChemberNoDetails.GetData(obj.Weighment_No, trans)
            Else
                obj = Nothing
            End If

            Return obj

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function isWeighmentDone(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = String.Empty
        qry = "select count(*) from tspl_weighment_detail where gate_entry_no='" & strGateEntryNo & "' and isposted=1"
        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) <= 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Shared Function deleteData(ByVal strWeighmentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Return deleteData(True, strWeighmentNo, trans)
    End Function

    Public Shared Function deleteData(ByVal isCheckForPost As Boolean, ByVal strWeighmentNo As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Try
            Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_milk_unloading where weighment_no='" & strWeighmentNo & "'", trans)
            If isUsed > 0 Then
                Throw New Exception("Record is in use.")
            End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_Code,Date_And_Time from tspl_weighment_detail where Weighment_No='" + strWeighmentNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmWeighment, clsCommon.myCstr(dt.Rows(0)("location_Code")), clsCommon.myCDate(dt.Rows(0)("Date_And_Time")), trans)

            End If

            If isCheckForPost Then
                Dim IsPosted As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isposted from tspl_weighment_detail where Weighment_No='" & strWeighmentNo & "'", trans))
                If IsPosted = 1 Then
                    Throw New Exception("Record is posted so you can not delete")
                End If
            End If

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strWeighmentNo), "TSPL_Weighment_Detail", "Weighment_No", "TSPL_Weighment_Chember_Details", "Weighment_No", trans)
            Dim strQry As String = "delete from TSPL_Weighment_Chember_Details where Weighment_No='" & strWeighmentNo & "'"
            Dim isDeleted As Boolean = clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            strQry = "delete from tspl_weighment_detail where Weighment_No='" & strWeighmentNo & "'"
            isDeleted = clsDBFuncationality.ExecuteNonQuery(strQry, trans)



            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        '============update by preeti gupta Against ticket no[ERO/15/07/19-000947]
        Try
            'Dim qry As String = " select tspl_weighment_detail.Weighment_No as [Code],tspl_weighment_detail.Weighment_Slip_no as [Weighment Slip No] ,convert(varchar,tspl_weighment_detail.Weighment_date,103) as [Weighment Date], tspl_weighment_detail.Gate_Entry_No as [Gate Entry No] ,tspl_weighment_detail.Doc_Type as [Doc Type] ,convert(varchar,tspl_weighment_detail.Date_And_Time,103) as [Gate Entry Date] ,tspl_weighment_detail.Challan_No as [Challan No] ,convert(varchar,tspl_weighment_detail.Challan_Date,103) as [Challan Date] ,tspl_weighment_detail.Tanker_No as [Tanker No] ,case when isnull(tspl_weighment_detail.isPosted,0)=0 then 'No' else 'Yes' end  as [Is Posted] ,convert(varchar,tspl_weighment_detail.Posting_Date,103) as [Posting Date] ,tspl_weighment_detail.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_MCC_MASTER.MCC_NAME as [Mcc Name] ,tspl_weighment_detail.location_Code as [Location Code] ,tspl_weighment_detail.Location_Desc as [Location Desc] ,tspl_weighment_detail.Vendor_Code as [Vendor Code] ,tspl_weighment_detail.Vendor_Desc as [Vendor Desc] ,tspl_weighment_detail.Item_Code as [Item Code] ,tspl_weighment_detail.Item_Desc as [Item Desc] ,tspl_weighment_detail.Qty_In_Kg as [Qty In Kg] ,tspl_weighment_detail.snf_Per as [SNF Per] ,tspl_weighment_detail.fat_per as [FAT Per] ,tspl_weighment_detail.Created_By as [Created By] ,cast(convert(date,tspl_weighment_detail.Created_Date,103) as varchar) as [Created Date] ,tspl_weighment_detail.Modify_By as [Modify By] ,cast(convert(date,tspl_weighment_detail.Modify_Date,103) as varchar) as [Modify Date] ,tspl_weighment_detail.comp_code as [Company Code] ,tspl_weighment_detail.Gross_Weight as [Gross Weight] ,tspl_weighment_detail.Dip_Value as [Dip Value] ,tspl_weighment_detail.Tare_Weight as [Tare Weight] ,tspl_weighment_detail.Net_Weight as [Net Weight] ,case when isnull (tspl_weighment_detail.status,0)=0 then 'Not Sent For QC' when tspl_weighment_detail.status=1 then 'Sent For QC' else 'QC Done' end as [Status] ,tspl_weighment_detail.Sent_to_QC_By as [Sent To Qc By] ,convert(varchar,tspl_weighment_detail.Sent_To_QC_Date,103) as [Sent To Qc Date] ,tspl_weighment_detail.QC_Done_By as [Qc Done By] ,convert(varchar,tspl_weighment_detail.QC_Done_Date,103) as [Qc Done Date]   From tspl_weighment_detail  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=tspl_weighment_detail.Dispatched_From_Mcc"
            Dim qry As String = " select tspl_weighment_detail.Weighment_No as [Code],tspl_weighment_detail.Weighment_Slip_no as [Weighment Slip No] ,convert(varchar,tspl_weighment_detail.Weighment_date,103) as [Weighment Date], tspl_weighment_detail.Gate_Entry_No as [Gate Entry No] ,tspl_weighment_detail.Doc_Type as [Doc Type] ,convert(varchar,tspl_weighment_detail.Date_And_Time,103) as [Gate Entry Date] ,tspl_weighment_detail.Challan_No as [Challan No] ,convert(varchar,tspl_weighment_detail.Challan_Date,103) as [Challan Date] ,tspl_weighment_detail.Tanker_No as [Tanker No] ,case when isnull(tspl_weighment_detail.isPosted,0)=0 then 'No' else 'Yes' end  as [Is Posted] ,convert(varchar,tspl_weighment_detail.Posting_Date,103) as [Posting Date] ,tspl_weighment_detail.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_MCC_MASTER.MCC_NAME as [Mcc Name] ,tspl_weighment_detail.location_Code as [Location Code] ,tspl_weighment_detail.Location_Desc as [Location Desc] ,tspl_weighment_detail.Vendor_Code as [Vendor Code] ,tspl_weighment_detail.Vendor_Desc as [Vendor Desc],case when tspl_weighment_detail.In_Return=1 then 'Yes' else 'No' end as [Milk In Return]  ,tspl_weighment_detail.Item_Code as [Item Code] ,tspl_weighment_detail.Item_Desc as [Item Desc] ,tspl_weighment_detail.Qty_In_Kg as [Qty In Kg] ,tspl_weighment_detail.snf_Per as [SNF Per] ,tspl_weighment_detail.fat_per as [FAT Per] ,tspl_weighment_detail.Created_By as [Created By] ,cast(convert(date,tspl_weighment_detail.Created_Date,103) as varchar) as [Created Date] ,tspl_weighment_detail.Modify_By as [Modify By] ,cast(convert(date,tspl_weighment_detail.Modify_Date,103) as varchar) as [Modify Date] ,tspl_weighment_detail.comp_code as [Company Code] ,tspl_weighment_detail.Gross_Weight as [Gross Weight] ,tspl_weighment_detail.Dip_Value as [Dip Value] ,tspl_weighment_detail.Tare_Weight as [Tare Weight] ,tspl_weighment_detail.Net_Weight as [Net Weight] ,case when isnull (tspl_weighment_detail.status,0)=0 then 'Not Sent For QC' when tspl_weighment_detail.status=1 then 'Sent For QC' else 'QC Done' end as [Status] ,tspl_weighment_detail.Sent_to_QC_By as [Sent To Qc By] ,convert(varchar,tspl_weighment_detail.Sent_To_QC_Date,103) as [Sent To Qc Date] ,tspl_weighment_detail.QC_Done_By as [Qc Done By] ,convert(varchar,tspl_weighment_detail.QC_Done_Date,103) as [Qc Done Date], case when In_Return=1 then 'Yes' else 'No' end as [Tanker Return]  From tspl_weighment_detail  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=tspl_weighment_detail.Dispatched_From_Mcc"
            str = clsCommon.ShowSelectForm("WGHMNT", qry, "Code", whrcls, curcode, "tspl_weighment_detail.Weighment_date desc", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function postData(ByVal strWeighmentNo As String, ByVal docType As String, ByVal formId As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(strWeighmentNo, docType, formId, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal strWeighmentNo As String, ByVal docType As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
       
        Dim isPosted As Boolean = True
        Try

            If (clsCommon.myLen(strWeighmentNo) <= 0) Then
                Throw New Exception("Weighment No not found to Post")
            End If

            Dim obj As clsWeighment = clsWeighment.getData(strWeighmentNo, docType, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Weighment_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            'trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleBulkMilkProcurement, clsUserMgtCode.frmWeighment, obj.location_Code, obj.Weighment_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If
            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
            If TankerFromMaster = 1 And clsCommon.CompairString(obj.Doc_Type, "BulkProc") = CompairStringResult.Equal Then
                Dim dblNetWt As Double = 0
                Dim AddWtPer As Double = 0
                Dim AddWt As Double = 0
                Dim TotalWt As Double = 0
                Dim strQCNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_SECONDARY_SETTING_QC_HEAD where gate_entry_no='" & obj.Gate_Entry_No & "' ", trans))
                If clsCommon.myLen(strQCNo) > 0 Then
                    For Each objTr As clsWeighmentChemberNoDetails In obj.Arr
                        dblNetWt = objTr.Net_Weight
                        If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAdditionalWeightinPercentage, clsFixedParameterCode.AllowAdditionalWeightinPercentage, trans)), "1") = CompairStringResult.Equal Then
                            AddWtPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnterAdditionalWeight, clsFixedParameterCode.EnterAdditionalWeight, trans))
                            AddWt = (clsCommon.myCdbl(dblNetWt)) * clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnterAdditionalWeight, clsFixedParameterCode.EnterAdditionalWeight, trans)) / 100
                        Else
                            AddWtPer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnterAdditionalWeight, clsFixedParameterCode.EnterAdditionalWeight, trans))
                        End If
                        TotalWt = AddWt + dblNetWt
                        Dim qry = "Update TSPL_SECONDARY_SETTING_QC_DETAIL set NetWeight='" & dblNetWt & "',AdditinalWeightper='" & AddWtPer & "',CalculatedAdditionalWeight='" & AddWt & "',TotalWeight='" & TotalWt & "' where Document_No='" & strQCNo & "' and Line_No='" & objTr.Line_No & "'  and Chamber_Desc='" & objTr.Chamber_Desc & "'"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    Next
                End If

            End If
            ' done by priti BHA/08/07/18-000135 for auto gate out setting based
            Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
            Dim FirstGateOutProcessForMCCBulkProcument As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FirstGateOutProcessForMCCBulkProcument, clsFixedParameterCode.FirstGateOutProcessForMCCBulkProcument, trans))
            Dim settTankerDispatchIntermittentSingleGateIn As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchIntermittentSingleGateIn, clsFixedParameterCode.TankerDispatchIntermittentSingleGateIn, trans)) = 1)
            Dim rValue As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select isintermittent from TSPL_MCC_Dispatch_Challan  left outer join tspl_weighment_detail  on tspl_weighment_detail.challan_no=TSPL_MCC_Dispatch_Challan.chalan_no  where tspl_weighment_detail.Weighment_No ='" & strWeighmentNo & "' ", trans))
            If rValue = 1 AndAlso settTankerDispatchIntermittentSingleGateIn = True AndAlso MCCChamberwise = 1 AndAlso clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                'No Gate Out entry
            ElseIf MCCChamberwise = 1 AndAlso FirstGateOutProcessForMCCBulkProcument = 1 And clsCommon.CompairString(obj.Doc_Type, "MccProc") = CompairStringResult.Equal Then
                isPosted = clsQualityCheck.SaveGateOutData(obj.Gate_Entry_No, trans)
            End If

            Dim strQry As String = " update tspl_weighment_detail set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Weighment_No='" & strWeighmentNo & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, clsCommon.myCstr(strWeighmentNo), "TSPL_Weighment_Detail", "Weighment_No", trans)
            clsQualityCheck.SaveAndPostUnloadingGateOutMilkTransferIn(obj.Gate_Entry_No, trans)
          
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class

Public Class clsWeighmentChemberNoDetails
#Region "Variables"
    Public CH_FAT_Rate As Double = 0
    Public CH_SNF_Rate As Double = 0
    Public CH_FAT_Value As Double = 0
    Public CH_SNF_Value As Double = 0
    Public CH_Amount As Double = 0
    Public CH_FAT_Kg As Double = 0
    Public CH_SNF_Kg As Double = 0
    Public Weighment_No As String = Nothing
    Public Line_No As Integer = 0
    Public Item_Code As String = Nothing
    Public UOM As String = Nothing
    Public Chamber_Desc As String = Nothing
    Public Chamber_Qty As Integer = 0
    Public snf_Per As Double = 0
    Public fat_per As Double = 0
    Public DIP_Status As String = Nothing
    Public Sample_Lifted As String = Nothing
    Public Gross_Weight As Double = 0
    Public Tare_Weight As Double = 0
    Public Net_Weight As Double = 0
    Public Weighment_Sequence As Integer = 0
    Public isCanType As Integer = 0
    Public Vendor_Weight As Double = 0
    Public Sublocation_Code As String = Nothing
#End Region

    Public Shared Function SaveData(ByVal strDocNo As String, ByVal Arr As List(Of clsWeighmentChemberNoDetails), ByVal trans As SqlTransaction) As Boolean
        If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
            Dim sQuery As String = "Delete from TSPL_Weighment_Chember_Details where Weighment_No='" & strDocNo & "'"
            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
            For Each obj As clsWeighmentChemberNoDetails In Arr
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Weighment_No", strDocNo)
                clsCommon.AddColumnsForChange(coll, "Line_No", obj.Line_No)
                clsCommon.AddColumnsForChange(coll, "Item_Code", obj.Item_Code)
                clsCommon.AddColumnsForChange(coll, "UOM", obj.UOM)
                clsCommon.AddColumnsForChange(coll, "Chamber_Desc", obj.Chamber_Desc)
                clsCommon.AddColumnsForChange(coll, "Chamber_Qty", obj.Chamber_Qty)
                clsCommon.AddColumnsForChange(coll, "CH_FAT_Rate", obj.CH_FAT_Rate)
                clsCommon.AddColumnsForChange(coll, "CH_SNF_Rate", obj.CH_SNF_Rate)
                clsCommon.AddColumnsForChange(coll, "CH_FAT_Value", obj.CH_FAT_Value)
                clsCommon.AddColumnsForChange(coll, "CH_SNF_Value", obj.CH_SNF_Value)
                clsCommon.AddColumnsForChange(coll, "CH_FAT_Kg", obj.CH_FAT_Kg)
                clsCommon.AddColumnsForChange(coll, "CH_SNF_Kg", obj.CH_SNF_Kg)
                clsCommon.AddColumnsForChange(coll, "CH_Amount", obj.CH_Amount)
                clsCommon.AddColumnsForChange(coll, "snf_Per", obj.snf_Per)
                clsCommon.AddColumnsForChange(coll, "fat_per", obj.fat_per)
                clsCommon.AddColumnsForChange(coll, "DIP_Status", obj.DIP_Status)
                clsCommon.AddColumnsForChange(coll, "Sample_Lifted", obj.Sample_Lifted)
                clsCommon.AddColumnsForChange(coll, "Gross_Weight", obj.Gross_Weight)
                clsCommon.AddColumnsForChange(coll, "Tare_Weight", obj.Tare_Weight)
                clsCommon.AddColumnsForChange(coll, "Net_Weight", obj.Net_Weight)
                clsCommon.AddColumnsForChange(coll, "Weighment_Sequence", obj.Weighment_Sequence)
                clsCommon.AddColumnsForChange(coll, "Vendor_Weight", obj.Vendor_Weight)
                clsCommon.AddColumnsForChange(coll, "Sublocation_Code", obj.Sublocation_Code, True)
                clsCommon.AddColumnsForChange(coll, "isCanType", obj.isCanType)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Weighment_Chember_Details", OMInsertOrUpdate.Insert, "", trans)
            Next
        End If
        Return True
    End Function

    Public Shared Function GetData(ByVal strCode As String, ByVal trans As SqlTransaction) As List(Of clsWeighmentChemberNoDetails)
        Dim arr As List(Of clsWeighmentChemberNoDetails) = Nothing
        Dim qry As String
        qry = "select * from " & _
            "TSPL_Weighment_Chember_Details where TSPL_Weighment_Chember_Details.Weighment_No='" + strCode + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New List(Of clsWeighmentChemberNoDetails)()
            For Each dr As DataRow In dt.Rows
                Dim obj As clsWeighmentChemberNoDetails = New clsWeighmentChemberNoDetails()
                obj.CH_FAT_Rate = clsCommon.myCdbl(dr("CH_FAT_Rate"))
                obj.CH_SNF_Rate = clsCommon.myCdbl(dr("CH_SNF_Rate"))
                obj.CH_FAT_Value = clsCommon.myCdbl(dr("CH_FAT_Value"))
                obj.CH_SNF_Value = clsCommon.myCdbl(dr("CH_SNF_Value"))
                obj.CH_FAT_Kg = clsCommon.myCdbl(dr("CH_FAT_Kg"))
                obj.CH_SNF_Kg = clsCommon.myCdbl(dr("CH_SNF_Kg"))
                obj.CH_Amount = clsCommon.myCdbl(dr("CH_Amount"))
                obj.Weighment_No = clsCommon.myCstr(dr("Weighment_No"))
                obj.Line_No = clsCommon.myCstr(dr("Line_No"))
                obj.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                obj.UOM = clsCommon.myCstr(dr("UOM"))
                obj.snf_Per = clsCommon.myCdbl(dr("snf_Per"))
                obj.fat_per = clsCommon.myCdbl(dr("fat_per"))
                obj.Chamber_Qty = clsCommon.myCdbl(dr("Chamber_Qty"))
                obj.Chamber_Desc = clsCommon.myCstr(dr("Chamber_Desc"))
                obj.DIP_Status = clsCommon.myCstr(dr("DIP_Status"))
                obj.Sample_Lifted = clsCommon.myCstr(dr("Sample_Lifted"))
                obj.Gross_Weight = clsCommon.myCdbl(dr("Gross_Weight"))
                obj.Tare_Weight = clsCommon.myCdbl(dr("Tare_Weight"))
                obj.Net_Weight = clsCommon.myCdbl(dr("Net_Weight"))
                obj.Weighment_Sequence = clsCommon.myCdbl(dr("Weighment_Sequence"))
                obj.Vendor_Weight = clsCommon.myCdbl(dr("Vendor_Weight"))
                obj.Sublocation_Code = clsCommon.myCstr(dr("Sublocation_Code"))
                obj.isCanType = clsCommon.myCdbl(dr("isCanType"))
                arr.Add(obj)
            Next
        End If
        Return arr
    End Function
End Class