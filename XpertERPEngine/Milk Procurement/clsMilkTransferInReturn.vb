Imports common
Imports System.Data.SqlClient
Public Class clsMilkTransferInReturn
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public FAT_Rate As Double = 0
    Public SNF_Rate As Double = 0
    Public FAT_Value As Double = 0
    Public SNF_Value As Double = 0
    Public Amount As Double = 0
    Public PriceCode As String = Nothing
    Public FAT_W As Double = 0
    Public SNF_W As Double = 0
    Public FAT_R As Double = 0
    Public SNF_R As Double = 0
    Public Transfer_Price As Double = 0
    Public isNewEntry As Boolean = False
    Public Receipt_Challan_Return_No As String = Nothing
    Public Receipt_Challan_No As String = Nothing
    Public Receipt_Challan_Date As Date = Nothing
    Public Dispatch_Challan_No As String = Nothing
    Public Weighment_No As String = Nothing
    Public Qc_No As String = Nothing
    Public Gate_Entry_no As String = Nothing
    Public location_code As String = Nothing
    Public km_reading_receipt As String = Nothing
    Public New_Seal_No1 As String = Nothing
    Public New_Seal_No2 As String = Nothing
    Public New_Seal_No3 As String = Nothing
    Public New_Seal_No4 As String = Nothing
    Public New_Seal_No5 As String = Nothing
    Public New_Seal_No6 As String = Nothing
    Public New_Seal_No7 As String = Nothing
    Public New_Seal_No8 As String = Nothing
    Public New_Seal_No9 As String = Nothing
    Public New_Seal_No10 As String = Nothing
    Public isNewSealNo As Integer
    Public isPosted As Integer
    Public Posting_Date As Date = Nothing
    Public Comp_Code As String = Nothing
    Public Created_By As String = Nothing
    Public Created_Date As String = Nothing
    Public Modified_By As String = Nothing
    Public Modified_Date As String = Nothing
    Public Receipt_Control_FAT As Double = 0
    Public Receipt_Control_SNF As Double = 0
    'Public arrPaperSeal As List(Of clsTransferInPaperSealDetail) = Nothing
    'Public arrManualSeal As List(Of clsTransferInManualSealDetail) = Nothing

    Public Shared Function GetTransferInDocNoFromGateEntry(ByVal strGateEntryNo As String, ByVal trans As SqlTransaction) As String
        Dim qry As String = String.Empty
        Dim Doc_No As String = ""
        qry = "select Receipt_Challan_No from TSPL_MILK_TRANSFER_IN_RETURN where Gate_Entry_no='" & strGateEntryNo & "' "
        Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return Doc_No
    End Function
    Public Shared Function saveData(ByVal obj As clsMilkTransferInReturn, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement Bulk", "Milk Transfer In Return", obj.location_code, obj.Receipt_Challan_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PriceCode", clsCommon.myCstr(obj.PriceCode))
            clsCommon.AddColumnsForChange(coll, "FAT_R", clsCommon.myCstr(obj.FAT_R))
            clsCommon.AddColumnsForChange(coll, "FAT_W", clsCommon.myCstr(obj.FAT_W))
            clsCommon.AddColumnsForChange(coll, "SNF_W", clsCommon.myCstr(obj.SNF_W))
            clsCommon.AddColumnsForChange(coll, "SNF_R", clsCommon.myCstr(obj.SNF_R))
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "Transfer_Price", clsCommon.myCstr(obj.Transfer_Price))
            clsCommon.AddColumnsForChange(coll, "Receipt_Challan_Return_No", clsCommon.myCstr(obj.Receipt_Challan_Return_No))
            clsCommon.AddColumnsForChange(coll, "Receipt_Challan_Return_Date", clsCommon.GetPrintDate(obj.Receipt_Challan_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Receipt_Challan_No", clsCommon.myCstr(obj.Receipt_Challan_No))
            clsCommon.AddColumnsForChange(coll, "Dispatch_Challan_No", clsCommon.myCstr(obj.Dispatch_Challan_No))
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No))
            clsCommon.AddColumnsForChange(coll, "Qc_No", clsCommon.myCstr(obj.Qc_No))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_no", clsCommon.myCstr(obj.Gate_Entry_no))
            clsCommon.AddColumnsForChange(coll, "location_code", clsCommon.myCstr(obj.location_code))
            clsCommon.AddColumnsForChange(coll, "km_reading_receipt", clsCommon.myCstr(obj.km_reading_receipt))
            'clsCommon.AddColumnsForChange(coll, "isNewSealNo", clsCommon.myCdbl(obj.isNewSealNo))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_FAT", clsCommon.myCdbl(obj.Receipt_Control_FAT))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_SNF", clsCommon.myCdbl(obj.Receipt_Control_SNF))
            'If obj.isNewSealNo = 1 Then
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No1", clsCommon.myCstr(obj.New_Seal_No1))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No2", clsCommon.myCstr(obj.New_Seal_No2))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No3", clsCommon.myCstr(obj.New_Seal_No3))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No4", clsCommon.myCstr(obj.New_Seal_No4))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No5", clsCommon.myCstr(obj.New_Seal_No5))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No6", clsCommon.myCstr(obj.New_Seal_No6))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No7", clsCommon.myCstr(obj.New_Seal_No7))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No8", clsCommon.myCstr(obj.New_Seal_No8))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No9", clsCommon.myCstr(obj.New_Seal_No9))
            '    clsCommon.AddColumnsForChange(coll, "New_Seal_No10", clsCommon.myCstr(obj.New_Seal_No10))
            'End If
            clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            If obj.isPosted = 1 Then
                clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modified_By)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_TRANSFER_IN_RETURN", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Receipt_Challan_No, "TSPL_MILK_TRANSFER_IN_RETURN", "Receipt_Challan_No", trans)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_TRANSFER_IN_RETURN", OMInsertOrUpdate.Update, "TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No='" + obj.Receipt_Challan_No + "'", trans)
            End If
            'issaved = issaved And clsTransferInPaperSealDetail.SaveData(obj.arrPaperSeal, trans, False)
            'issaved = issaved And clsTransferInManualSealDetail.SaveData(obj.arrPaperSeal, trans, False)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved

    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        'Sanjay Ticket no-TEC/23/11/18-000375 Order by date desc
        Dim str As String = ""
        Try
            Dim qry As String = "select TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No  ,convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) as [Receipt Challan Return Date] ,TSPL_MILK_TRANSFER_IN_RETURN.Dispatch_Challan_No as [Dispatch Challan No] ,TSPL_MILK_TRANSFER_IN_RETURN.Weighment_No as [Weighment No] ,TSPL_MILK_TRANSFER_IN_RETURN.Qc_No as [Qc No] ,TSPL_MILK_TRANSFER_IN_RETURN.Gate_Entry_no as [Gate Entry No] ,TSPL_MILK_TRANSFER_IN_RETURN.location_code as [Dispatched From] ,TSPL_MILK_TRANSFER_IN_RETURN.km_reading_receipt as [Km Reading Receipt]  ,case when isnull( TSPL_MILK_TRANSFER_IN_RETURN.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Posting_Date,103) as [Posting Date] ,TSPL_MILK_TRANSFER_IN_RETURN.Comp_Code as [Company Code] ,TSPL_MILK_TRANSFER_IN_RETURN.Created_By as [Created By] ,convert(varchar,cast(TSPL_MILK_TRANSFER_IN_RETURN.Created_Date as date),103) as [Created Date] ,TSPL_MILK_TRANSFER_IN_RETURN.Modified_By as [Modified By] ,convert(varchar,cast(TSPL_MILK_TRANSFER_IN_RETURN.Modified_Date as date),103) as [Modified Date]  From TSPL_MILK_TRANSFER_IN_RETURN "
            str = clsCommon.ShowSelectForm("MLKTRNSFRIN", qry, "Receipt_Challan_Return_No", whrcls, curcode, "TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date desc", isButtonClicked, "TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
    End Function
    Public Shared Function ReverseAndUnpost(ByVal strCode As String, Optional trans As SqlTransaction = Nothing) As Boolean
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
        End If
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If

            Dim Qry As String = "select isPosted from TSPL_MILK_TRANSFER_IN_RETURN where Receipt_Challan_Return_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If

            ''Delete Consumption Entry by balwinder on 09/08/2017
            Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (" + Environment.NewLine + _
            " select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'BML-TRI-CONSUME'  and Document_No='" + strCode + "') and Source_Code='IC-AD')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_JOURNAL_MASTER where Source_Doc_No in (select adjustment_no from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'BML-TRI-CONSUME'  and Document_No='" + strCode + "') and Source_Code='IC-AD'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT  where Source_Doc_No in  (select adjustment_no from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'BML-TRI-CONSUME'  and Document_No='" + strCode + "') "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No in  (select adjustment_no from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'BML-TRI-CONSUME'  and Document_No='" + strCode + "') and Trans_Type ='JW-IN'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_ADJUSTMENT_DETAIL where adjustment_no in  (select adjustment_no from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'BML-TRI-CONSUME'  and Document_No='" + strCode + "')"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "delete from TSPL_ADJUSTMENT_HEADER where Reference_Document = 'BML-TRI-CONSUME'  and Document_No='" + strCode + "' "
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            ''End of Delete Consumption Entry

            Dim AdjustmentNo As String = clsDBFuncationality.getSingleValue("SELECT top 1 Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Transfer_In_Return_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(AdjustmentNo) > 0 Then
                Dim AdjVoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No='" + AdjustmentNo + "'", trans)
                If clsCommon.myLen(AdjVoucherNo) > 0 Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, AdjVoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + AdjVoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + AdjVoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                End If

                Qry = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No IN (SELECT Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Transfer_In_Return_Doc_No = '" + strCode + "')"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_ADJUSTMENT_HEADER where Against_Transfer_In_Doc_No ='" + strCode + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No IN (SELECT Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Transfer_In_Return_Doc_No = '" + strCode + "') and Trans_Type='IC-AD'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            Dim strMilkRGP As String = clsDBFuncationality.getSingleValue("select top 1 rgp_no from TSPL_MILK_RGP_HEAD where Milk_Transfer_In='" & strCode & "'", trans)
            If clsCommon.myLen(strMilkRGP) > 0 Then
                clsMilkRGPHead.ReverseAndUnpost(strMilkRGP, trans)
                Qry = "delete from TSPL_MILK_RGP_detail where rgp_no= '" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_MILK_RGP_HEAD where rgp_no='" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_MR_ISSUE_QC_DETAIL where Issue_Code='" + strMilkRGP + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If


            Dim strMilkJobWorkTransfer As String = clsDBFuncationality.getSingleValue("select top 1 Document_No from TSPL_MILK_JOBWORK_TRANSFER_RETURN where MilkTransRetNo='" & strCode & "'", trans)
            If clsCommon.myLen(strMilkJobWorkTransfer) > 0 Then
                clsMilkJWOTransferReturn.ReverseAndUnpost(strMilkJobWorkTransfer, trans)
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='MT-IR' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If

            clsBatchInventoryNew.DeleteData("MilkTransferInReturn", strCode, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "tspl_inventory_movement_new", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No= '" + strCode + "' and trans_type='MilkTransferInReturn'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            ''richa BHA/11/09/18-000534
            Dim obj As clsMilkTransferInReturn = clsMilkTransferInReturn.getData(strCode, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Receipt_Challan_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If

            Qry = " update TSPL_MILK_TRANSFER_IN set In_Return='0' where Receipt_Challan_No='" & obj.Receipt_Challan_No & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = " update TSPL_Weighment_Detail set In_Return='0' where Gate_Entry_No='" & obj.Gate_Entry_no & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = " update Tspl_Gate_Entry_Details set In_Return='0' where Gate_Entry_No='" & obj.Gate_Entry_no & "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            ''--------------------


            Qry = "Update TSPL_MILK_TRANSFER_IN_RETURN set isPosted=0 where Receipt_Challan_Return_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_MILK_TRANSFER_IN_RETURN", "receipt_challan_Return_no", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Receipt_Challan_No, "TSPL_MILK_TRANSFER_IN", "Receipt_Challan_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Gate_Entry_no, "TSPL_Weighment_Detail", "Gate_Entry_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Gate_Entry_no, "Tspl_Gate_Entry_Details", "Gate_Entry_No", trans)

            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateJobWorkTransferMilkReturn(ByVal objMTIn As clsMilkTransferInReturn, ByVal trans As SqlTransaction) As Boolean
        Dim obj = New clsMilkJWOTransferReturn()
        Dim isNewEntry As Boolean = True
        obj.Document_Date = objMTIn.Receipt_Challan_Date
        obj.Remarks = "Auto Return From  Milk Transfer In Return(job work Type)"
        obj.JWO_Transfer_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_Code from TSPL_MILK_JOBWORK_TRANSFER_HEAD WHERE Milk_Transfer_In='" + objMTIn.Receipt_Challan_No + "'", trans))
        obj.JWO_SRN_From_Location_Code = objMTIn.Joblocation_Code
        obj.MilkTransRetNo = objMTIn.Receipt_Challan_Return_No
        If obj.SaveData(obj, isNewEntry, trans) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function CreateMilkJobWorkTransfer(ByVal trans As SqlTransaction, ByVal objMTIn As clsMilkTransferInReturn) As Boolean
        Dim totalqty As Decimal = 0
        Dim obj = New clsMilkJobworkTransfer()
        obj.isNewEntry = True
        Dim objW As clsWeighment = clsWeighment.getData(objMTIn.Weighment_No, "MccProc", NavigatorType.Current, trans)
        Dim objQ As clsQualityCheck = clsQualityCheck.getData(objMTIn.Qc_No, "MccProc", NavigatorType.Current, trans)
        Dim objD As clsMccDispatch = clsMccDispatch.getData(objMTIn.Dispatch_Challan_No, NavigatorType.Current, trans)

        Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
        Dim strSiloNo As String = clsDBFuncationality.getSingleValue("select Sub_location_Code  from TSPL_MILK_UNLOADING where Gate_Entry_No='" & objMTIn.Gate_Entry_no & "'", trans)
        Dim strJobLoc As String = clsDBFuncationality.getSingleValue("select Sublocation_Code  from tspl_gate_entry_details where Gate_Entry_No='" & objMTIn.Gate_Entry_no & "'", trans)
        Dim strJobVendor As String = clsDBFuncationality.getSingleValue("select Jobwork_Vendor  from tspl_location_master where Location_Code='" & strJobLoc & "'", trans)

        obj.SRN_NO = ""
        obj.Milk_Transfer_In = objMTIn.Receipt_Challan_No
        obj.Document_Date = objMTIn.Receipt_Challan_Date
        obj.Virtual_location = strSiloNo
        obj.JobWork_location = strJobLoc
        obj.Gate_Entry_No = objMTIn.Gate_Entry_no
        obj.Weighment_No = objMTIn.Weighment_No
        obj.Weighment_Date = clsDBFuncationality.getSingleValue("select Weighment_date from TSPL_Weighment_Detail where Weighment_No='" & objMTIn.Weighment_No & "' ", trans)
        obj.Vendor_Code = strJobVendor
        obj.Loc_Code = objMTIn.location_code
        obj.Challan_No = objMTIn.Dispatch_Challan_No
        obj.Challan_Date = objD.Dispatch_Date
        obj.Tanker_No = objW.Tanker_No
        obj.Price_Code = objD.PriceCode
        obj.QC_No = objMTIn.Qc_No
        obj.Qc_Date = objQ.QC_Out_Date_Time
        obj.isPosted = 0
        obj.Item_Code = objW.Item_Code
        obj.Item_Desc = objW.Item_Desc
        obj.UOM = objW.UOM
        obj.Gross_Weight = objW.Gross_Weight
        obj.Tare_Weight = objW.Tare_Weight
        obj.Net_Weight = objW.Net_Weight
        obj.snf_Per = objW.snf_Per
        obj.fat_per = objW.fat_per
        obj.fat_KG = objW.FAT_Kg
        obj.SNF_KG = objW.SNF_Kg
        obj.fat_Rate = objW.FAT_Rate
        obj.SNF_Rate = objW.SNF_Rate
        obj.Amount = objW.Amount
        obj.SpecialDeduction = 0
        obj.Deduction = 0
        obj.Incentive = 0
        obj.Actual_Amount = objW.Amount
        obj.BasicRate = 0
        obj.Standardrate = 0
        obj.NetRate = objD.Transfer_Price

        obj.FatAmt = objW.FAT_Value
        obj.SnfAmt = objW.SNF_Value
        obj.FinalMilkRate = 0

        obj.Modify_By = objCommonVar.CurrentUserCode
        obj.Modify_Date = objMTIn.Modified_Date
        obj.comp_code = objCommonVar.CurrentCompanyCode
        obj.Doc_Type = "MccProc"
        If obj.isNewEntry Then
            obj.Created_By = objCommonVar.CurrentUserCode
            obj.Created_Date = objMTIn.Modified_Date
        End If
        If (MCCChamberwise = 1) Then

            obj.Arr = New List(Of clsMilkJobworkTransferDetails)
            For Each objMTInDetail As clsWeighmentChemberNoDetails In objW.Arr
                Dim objTr As New clsMilkJobworkTransferDetails()
                objTr.Line_No = objMTInDetail.Line_No
                objTr.Chamber_Desc = objMTInDetail.Chamber_Desc
                objTr.Item_Code = objMTInDetail.Item_Code
                objTr.UOM = objMTInDetail.Item_Code
                objTr.fat_per = objMTInDetail.fat_per
                objTr.snf_Per = objMTInDetail.snf_Per
                objTr.Gross_Weight = objMTInDetail.Gross_Weight
                objTr.Tare_Weight = objMTInDetail.Tare_Weight
                objTr.Net_Weight = objMTInDetail.Net_Weight
                objTr.snf_Per = objMTInDetail.snf_Per
                objTr.fat_per = objMTInDetail.fat_per
                objTr.fat_KG = objMTInDetail.CH_FAT_Kg
                objTr.SNF_KG = objMTInDetail.CH_SNF_Kg
                objTr.fat_Rate = objMTInDetail.CH_FAT_Rate
                objTr.SNF_Rate = objMTInDetail.CH_SNF_Rate
                objTr.Amount = objMTInDetail.CH_Amount
                objTr.SpecialDeduction = 0
                objTr.Deduction = 0
                objTr.Incentive = 0
                objTr.Actual_Amount = objMTInDetail.CH_Amount
                objTr.BasicRate = 0
                objTr.Standardrate = 0
                objTr.NetRate = 0
                objTr.FatAmt = objMTInDetail.CH_FAT_Value
                objTr.SnfAmt = objMTInDetail.CH_SNF_Value
                objTr.FinalMilkRate = objD.Transfer_Price
                objTr.Price_Code = objD.PriceCode
                objTr.MILK_GRADE_CODE = ""
                objTr.MIKL_TYPE_CODE = ""
                objTr.fat_Qty = 0
                objTr.SNF_Qty = 0
                objTr.TotalStandardQty = objW.Net_Weight
                objTr.Incentive_Amt = 0
                objTr.Deduction_Amt = 0
                If (clsCommon.myLen(objTr.Chamber_Desc) > 0) Then
                    obj.Arr.Add(objTr)
                End If
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
    Public Shared Function postData(ByVal strReceiptChallanNo As String, ByVal formId As String, ByVal trans As SqlTransaction) As Boolean
        Dim isTrnasInitPostData As Boolean = False
        Dim AllowBulkProcMCCwithoutTankerDispatch As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcMCCwithoutTankerDispatch, clsFixedParameterCode.AllowBulkProcMCCwithoutTankerDispatch, trans))
        Dim isTankerDispatchFinancialImpactInTransferIn As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchFinancialImpactInTransferIn, clsFixedParameterCode.TankerDispatchFinancialImpactInTransferIn, trans)) = 1, True, False)
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            isTrnasInitPostData = True
        Else
            isTrnasInitPostData = False
        End If
        Try
            Dim isPosted As Boolean = True
            If (clsCommon.myLen(strReceiptChallanNo) <= 0) Then
                Throw New Exception("Receipt Challan  No not found to Post")
            End If

            Dim obj As clsMilkTransferInReturn = clsMilkTransferInReturn.getData(strReceiptChallanNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Receipt_Challan_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
            Dim objD As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
            'trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, "Milk Procurement Bulk", "Milk Transfer In Return", obj.location_code, obj.Receipt_Challan_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If


            Dim FatQcPer As Double = 0
            Dim SNFQcPer As Double = 0
            Dim FatValue As Double = 0
            Dim SnfValue As Double = 0
            Dim rcptAmount As Double = 0
            Dim strItemType As String = ""
            Dim strItemTypeToSave As String = ""
            Dim subLocCode As String = ""
            Dim ArrInventoryMovement As List(Of clsInventoryMovementNew) = New List(Of clsInventoryMovementNew)
            Dim objInventoryMovemnt As New clsInventoryMovementNew()

            ''richa agarwal 25 June,2019 add data for Batch Item New table when item is of Batch wise TEC/25/06/19-000566
            Dim dtSRN As DataTable = clsDBFuncationality.GetDataTable("select Parent_Line_No ,Batch_No ,Item_Code ,Qty,UOM ,Location_Code  from TSPL_BATCH_ITEM_new where Document_code='" & obj.Receipt_Challan_No & "' and Document_Type='MilkTransferIn' ", trans)
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
                    clsBatchInventoryNew.SaveData("MilkTransferInReturn", obj.Receipt_Challan_Return_No, obj.Receipt_Challan_Date, "O", clsCommon.myCstr(dtSRN.Rows(i)("Item_Code")), clsCommon.myCstr(dtSRN.Rows(i)("Location_Code")), clsCommon.myCstr(dtSRN.Rows(i)("Parent_Line_No")), 0, clsCommon.myCstr(dtSRN.Rows(i)("UOM")), arr, trans)
                Next

            End If

            If obj.IsAgainstJobWork = 0 Then
                Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
                Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
                If MCCChamberwise = 0 Then
                    FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
                    SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))
                    If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                        FatValue = (objW.Net_Weight * FatQcPer / 100) * objD.FAT_RATE
                        SnfValue = (objW.Net_Weight * SNFQcPer / 100) * objD.SNF_RATE
                        rcptAmount = FatValue + SnfValue
                    Else

                        FatValue = objW.FAT_Value
                        SnfValue = objW.SNF_Value
                        rcptAmount = objW.Amount
                    End If
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

                    objInventoryMovemnt.InOut = "O"
                    objInventoryMovemnt.Location_Code = subLocCode
                    objInventoryMovemnt.main_location = obj.location_code
                    objInventoryMovemnt.Item_Code = objW.Item_Code
                    objInventoryMovemnt.Item_Desc = objW.Item_Desc
                    objInventoryMovemnt.Qty = objW.Net_Weight
                    objInventoryMovemnt.UOM = clsItemMaster.GetStockUnit(objW.Item_Code, trans)
                    objInventoryMovemnt.MRP = rcptAmount
                    objInventoryMovemnt.Add_Cost = 0
                    Dim objDispChallan As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
                    If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                        objInventoryMovemnt.Other_Location_Code = objDispChallan.MCC_Code
                        objInventoryMovemnt.Other_Location_Desc = objDispChallan.MCC_Name
                        '' added by Panch Raj for production costing
                        objInventoryMovemnt.Fat_Rate = objD.FAT_RATE
                        objInventoryMovemnt.SNF_Rate = objD.SNF_RATE
                        objInventoryMovemnt.Fat_Amt = FatValue
                        objInventoryMovemnt.SNF_Amt = SnfValue
                    Else
                        objInventoryMovemnt.Other_Location_Code = objW.Dispatched_From_Mcc
                        objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(objW.Dispatched_From_Mcc, trans)
                        '' added by Panch Raj for production costing
                        objInventoryMovemnt.Fat_Rate = objW.FAT_Rate
                        objInventoryMovemnt.SNF_Rate = objW.SNF_Rate
                        objInventoryMovemnt.Fat_Amt = objW.FAT_Value
                        objInventoryMovemnt.SNF_Amt = objW.SNF_Value
                    End If

                    'select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='QCM00001' and Param_Type='SNF'
                    objInventoryMovemnt.FAT_Per = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
                    objInventoryMovemnt.SNF_Per = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))
                    objInventoryMovemnt.FAT_KG = objInventoryMovemnt.FAT_Per * objW.Net_Weight / 100
                    objInventoryMovemnt.SNF_KG = objInventoryMovemnt.SNF_Per * objW.Net_Weight / 100
                    objInventoryMovemnt.Net_Cost = rcptAmount
                    If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "RM"
                    ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "OT"
                    ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                        objInventoryMovemnt.ItemType = "FT"
                    End If
                    objInventoryMovemnt.ItemType = strItemTypeToSave
                    objInventoryMovemnt.Basic_Cost = rcptAmount / objW.Net_Weight
                    ArrInventoryMovement.Add(objInventoryMovemnt)
                Else
                    For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
                        FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Line_No='" & objTr.Line_No & "' and Param_Type='FAT' ", trans))
                        SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "'  and Line_No='" & objTr.Line_No & "' and Param_Type='SNF' ", trans))
                        If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                            FatValue = (objTr.Net_Weight * FatQcPer / 100) * objD.arr(objTr.Line_No - 1).FAT_Rate
                            SnfValue = (objTr.Net_Weight * SNFQcPer / 100) * objD.arr(objTr.Line_No - 1).SNF_Rate
                            rcptAmount = FatValue + SnfValue
                        Else
                            FatValue = objTr.CH_FAT_Value
                            SnfValue = objTr.CH_SNF_Value
                            rcptAmount = objTr.CH_Amount
                        End If

                        strItemType = clsItemMaster.GetItemType(objTr.Item_Code, trans)
                        strItemTypeToSave = ""
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
                        objInventoryMovemnt = New clsInventoryMovementNew
                        objInventoryMovemnt.InOut = "O"
                        objInventoryMovemnt.Location_Code = subLocCode
                        objInventoryMovemnt.main_location = obj.location_code

                        Dim objDispChallan As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
                        If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                            objInventoryMovemnt.Other_Location_Code = objDispChallan.MCC_Code
                            objInventoryMovemnt.Other_Location_Desc = objDispChallan.MCC_Name
                            objInventoryMovemnt.Fat_Rate = objD.arr(objTr.Line_No - 1).FAT_Rate
                            objInventoryMovemnt.SNF_Rate = objD.arr(objTr.Line_No - 1).SNF_Rate
                        Else
                            objInventoryMovemnt.Other_Location_Code = objW.Dispatched_From_Mcc
                            objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(objW.Dispatched_From_Mcc, trans)
                            objInventoryMovemnt.Fat_Rate = objTr.CH_FAT_Rate
                            objInventoryMovemnt.SNF_Rate = objTr.CH_SNF_Rate
                        End If

                        objInventoryMovemnt.Item_Code = objTr.Item_Code
                        objInventoryMovemnt.Item_Desc = clsIntimation.getItemName(objTr.Item_Code, trans)
                        objInventoryMovemnt.Qty = objTr.Net_Weight
                        objInventoryMovemnt.UOM = objTr.UOM
                        objInventoryMovemnt.MRP = rcptAmount
                        objInventoryMovemnt.Add_Cost = 0
                        objInventoryMovemnt.Fat_Amt = FatValue
                        objInventoryMovemnt.SNF_Amt = SnfValue
                        objInventoryMovemnt.FAT_Per = FatQcPer
                        objInventoryMovemnt.SNF_Per = SNFQcPer
                        objInventoryMovemnt.FAT_KG = objInventoryMovemnt.FAT_Per * objTr.Net_Weight / 100
                        objInventoryMovemnt.SNF_KG = objInventoryMovemnt.SNF_Per * objTr.Net_Weight / 100
                        objInventoryMovemnt.Net_Cost = rcptAmount
                        If clsCommon.CompairString(strItemType, "R") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "RM"
                        ElseIf clsCommon.CompairString(strItemType, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(strItemType, "O") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "OT"
                        ElseIf clsCommon.CompairString(strItemType, "F") = CompairStringResult.Equal Then
                            objInventoryMovemnt.ItemType = "FT"
                        End If
                        objInventoryMovemnt.ItemType = strItemTypeToSave
                        objInventoryMovemnt.Basic_Cost = rcptAmount / objTr.Net_Weight
                        ArrInventoryMovement.Add(objInventoryMovemnt)
                    Next
                End If
                isPosted = isPosted AndAlso clsInventoryMovementNew.SaveData("MilkTransferInReturn", obj.Receipt_Challan_Return_No, obj.Receipt_Challan_Date, clsCommon.GetPrintDate(obj.Receipt_Challan_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)
            End If

            Dim FromLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Dispatch_Challan_No & "'", trans))
            Dim ToLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Or_Plant_code  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Dispatch_Challan_No & "'", trans))
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.CreateTransferInGL, trans)) = 1 Then
                'isPosted = clsMilkTransferInReturn.CreateJournalEntryForTansferIn(obj, "", trans)
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    isPosted = clsMilkTransferInReturn.CreateTransferInJE(obj, "", trans)
                    ' isPosted = CreateJournalEntryForTansferInReturn(obj, "", trans)
                End If
            End If

            Dim strQry As String = " update TSPL_MILK_TRANSFER_IN_RETURN set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Receipt_Challan_Return_No='" & strReceiptChallanNo & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            strQry = " update TSPL_MILK_TRANSFER_IN set In_Return='1' where Receipt_Challan_No='" & obj.Receipt_Challan_No & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            strQry = " update TSPL_Weighment_Detail set In_Return='1' where Gate_Entry_No='" & obj.Gate_Entry_no & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            strQry = " update Tspl_Gate_Entry_Details set In_Return='1' where Gate_Entry_No='" & obj.Gate_Entry_no & "'"
            isPosted = isPosted AndAlso clsDBFuncationality.ExecuteNonQuery(strQry, trans)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strReceiptChallanNo, "TSPL_MILK_TRANSFER_IN_RETURN", "receipt_challan_Return_no", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Receipt_Challan_No, "TSPL_MILK_TRANSFER_IN", "Receipt_Challan_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Gate_Entry_no, "TSPL_Weighment_Detail", "Gate_Entry_No", trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Gate_Entry_no, "Tspl_Gate_Entry_Details", "Gate_Entry_No", trans)

            If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                CreateConsumptionEntry(obj, subLocCode, trans)
            End If
            If objD.IsAgainstJobWork = 1 Then
                CreateJobWorkTransferMilkReturn(obj, trans)
                'CreateMilkJobWorkTransfer(trans, obj)
            End If

            If isTankerDispatchFinancialImpactInTransferIn Then
                strQry = "select 1 from TSPL_JOURNAL_MASTER where Source_Doc_No='" + objD.Chalan_NO + "' and Source_Code='DI-CH'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsMccDispatch.CreateJEAndInvenotryMovement(objD, trans, obj.Receipt_Challan_Return_No, "")
                End If
            End If


            If isTrnasInitPostData Then
                If isPosted Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If
            Return isPosted
        Catch ex As Exception
            If isTrnasInitPostData Then
                Try
                    trans.Rollback()
                Catch ex1 As Exception

                End Try
            End If
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Shared Function CreateConsumptionEntry(ByVal obj As clsMilkTransferInReturn, ByVal strSiloNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String
        Dim dtItem As DataTable
        Try
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateConsumeEntry, clsFixedParameterCode.CreateConsumeEntry, trans)) > 0 Then
                qry = "select Item_Code,Item_Desc,Qty,UOM,convert(decimal(18,2), case when Qty=0 then 0 else Avg_Cost/Qty end) as Rate,Avg_Cost as Amount,Fat_Per,Fat_KG,SNF_Per, SNF_KG,Fat_Amt,SNF_Amt,Fat_Rate,Fat_Amt,SNF_Rate,SNF_Amt  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + obj.Receipt_Challan_Return_No + "' and Trans_Type='MilkTransInRet'"
                dtItem = clsDBFuncationality.GetDataTable(qry, trans)
                If dtItem IsNot Nothing AndAlso dtItem.Rows.Count > 0 Then
                    ''Use in RM Consumption 
                    Dim objConsumbption As New ClsJobWorkRMConsum
                    objConsumbption.Trans_Type = "In"
                    objConsumbption.Adjustment_Date = obj.Receipt_Challan_Date
                    objConsumbption.Posting_Date = obj.Receipt_Challan_Date
                    objConsumbption.EntryDateTime = obj.Receipt_Challan_Date
                    objConsumbption.IsMilkType = 1
                    objConsumbption.Loc_Code = strSiloNo
                    objConsumbption.Loc_Desc = clsLocation.GetName(objConsumbption.Loc_Code, trans)
                    objConsumbption.MainLocationCode = obj.location_code
                    objConsumbption.MainLocationDesc = clsLocation.GetName(objConsumbption.MainLocationCode, trans)

                    objConsumbption.Description = "Adjustment for Consum.Bulk Transfer In :" & obj.Receipt_Challan_Return_No & ""
                    objConsumbption.Reference_Document = "BML-TRI-CONSUME"
                    objConsumbption.Document_No = obj.Receipt_Challan_No
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

    Public Shared Function CreateTransferInJE(obj As clsMilkTransferInReturn, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
        Dim isTankerDispatchFinancialImpactInTransferIn As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchFinancialImpactInTransferIn, clsFixedParameterCode.TankerDispatchFinancialImpactInTransferIn, trans)) = 1, True, False)
        Dim SettAllowPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1, True, False)
        Dim PickTCAForStockTransferAndTankerDispatch As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickTCAForStockTransferAndTankerDispatch, clsFixedParameterCode.PickTCAForStockTransferAndTankerDispatch, trans)) = 0, False, True)
        Dim P_Or_I As String = "P"
        If Not SettAllowPurchaseAccounting Then
            P_Or_I = "I"
        End If
        Dim rValue As Boolean = False
        Try
            Dim COGT_AC As String = String.Empty
            Dim Stock_Transfer_Ac As String = String.Empty
            Dim Inventory_Control_Ac As String = String.Empty
            Dim Branch_Ac As String = String.Empty
            Dim Inventory_Control_Ac_FromLoc As String = String.Empty
            Dim Inventory_Control_Ac_ToLoc As String = String.Empty
            Dim Inventory_Control_Ac_GITLoc As String = String.Empty
            Dim Stock_Transfer_Ac_FromLoc As String = String.Empty
            Dim Stock_Transfer_Ac_GITLoc As String = String.Empty
            Dim GIT_LOC As String = String.Empty
            Dim CostingMethod As Integer = 0
            Dim CostOfItem As Double = 0
            Dim dt As Date = clsCommon.GETSERVERDATE(trans)
            Dim FromLocSeg As String = String.Empty
            Dim ToLocSeg As String = String.Empty
            If obj.isPosted = 1 Then
                dt = obj.Posting_Date
            End If
            Dim FromLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Dispatch_Challan_No & "'", trans))
            FromLocSeg = FromLocation
            ToLocSeg = obj.location_code

            Dim FromLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans))
            Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans))

            Dim TransitLossAc As String = ""
            Dim Branch_AcFromLoc As String = ""
            Dim IGnoreGITAccount As String = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.IGnoreGITAccount, clsFixedParameterCode.IGnoreGITAccount, trans))
            If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                GIT_LOC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location  from tspl_location_master where location_code='" & FromLocation & "'", trans))
                If clsCommon.myLen(GIT_LOC) <= 0 Then
                    Throw New Exception("Please Map GIT Location For Location : " & FromLocation)
                End If
                Dim GIT_LOC_SEG As String = String.Empty
                GIT_LOC_SEG = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & GIT_LOC & "'", trans))
                If clsCommon.myLen(GIT_LOC_SEG) <= 0 Then
                    Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & GIT_LOC)
                End If
                GIT_LOC = GIT_LOC_SEG
            End If


            Dim qry As String = ""
            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, NavigatorType.Current, False, trans)
            Dim objDisp As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
            Dim ArryLst As ArrayList = New ArrayList()

            If isTankerDispatchFinancialImpactInTransferIn Then
                ''richa agarwal 16 Jan,2020
                If PickTCAForStockTransferAndTankerDispatch = True Then
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objW.Item_Code + "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Transfer Clearing Account For  for item " + objW.Item_Code + " (" & objW.Item_Desc & ")")
                    End If
                Else
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)))
                    End If
                End If

                Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, ToLocationSegment, True, trans)

                Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                    Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & objW.Item_Code & " (" & objW.Item_Desc & ")")
                End If
                Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)


                Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
                Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))
                Dim FatValue As Double = (objW.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans)
                Dim SnfValue As Double = (objW.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans)
                Dim rcptAmount As Double = Math.Round(FatValue + SnfValue, 2, MidpointRounding.ToEven)

                ArryLst.Add(New String() {Branch_Ac, rcptAmount})
                ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, rcptAmount * -1, "", "", "", "", "", "", P_Or_I})
                If clsCommon.CompairString(P_Or_I, "I") = CompairStringResult.Equal Then
                    clsInventoryMovement.UpdateInvControlAccount(obj.Receipt_Challan_Return_No, "MilkTransferInReturn", objW.Item_Code, "", Inventory_Control_Ac_ToLoc, "", trans)
                End If
            ElseIf MCCChamberwise = 0 Then
                If Not isSkipCogsGL Then
                    CostingMethod = clsInventoryMovementNew.getCostingMethod(objW.Item_Code, trans)
                    qry = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement where source_doc_no='" & obj.Dispatch_Challan_No & "' and Item_Code='" & objW.Item_Code & "' and InOut='O' and Trans_Type='Transfer' "
                    CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                Else
                    CostOfItem = 0
                End If
                Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                    Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & objW.Item_Code & " (" & objW.Item_Desc & ")")
                End If

                Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)
                Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, False, trans)
                ''richa agarwal 16 Jan,2020
                If PickTCAForStockTransferAndTankerDispatch = True Then
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objW.Item_Code + "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Transfer Clearing Account For  for item " + objW.Item_Code + " (" & objW.Item_Desc & ")")
                    End If
                    Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, ToLocationSegment, True, trans)
                Else
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "'", trans))
                    If clsCommon.myLen(Branch_Ac) <= 0 Then
                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)))
                    End If
                End If

                Dim TransferGainLossAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                If clsCommon.myLen(TransferGainLossAc) <= 0 Then
                    Throw New Exception("Please Map Transfer Profit/Loss A/c For Item : " & objW.Item_Code)
                End If

                Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                    Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & objW.Item_Code & " (" & objW.Item_Desc & ")")
                End If
                If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                    Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                    Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                End If


                Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
                Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))

                Dim FatValue As Double = (objW.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans)
                Dim SnfValue As Double = (objW.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans)
                Dim rcptAmount As Double = FatValue + SnfValue

                Dim Amt As Double = rcptAmount
                Dim OutAmt As Double = clsMccDispatch.getTransferAmount(objW.Challan_No, trans)

                'ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                'ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt})
                'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                '    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                '    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                'End If

                ArryLst.Add(New String() {Branch_Ac, Amt})
                ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt * -1, "", "", "", "", "", "", P_Or_I})
                If clsCommon.CompairString(P_Or_I, "I") = CompairStringResult.Equal Then
                    clsInventoryMovement.UpdateInvControlAccount(obj.Receipt_Challan_Return_No, "MilkTransferInReturn", objW.Item_Code, "", Inventory_Control_Ac_ToLoc, "", trans)
                End If
                If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt})
                    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt * -1})
                End If


                TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                Dim DiffAmt As Double = 0

                If CostOfItem > OutAmt Then
                    DiffAmt = Math.Abs(CostOfItem - OutAmt)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                    DiffAmt = Math.Abs(OutAmt - CostOfItem)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                End If

                If rcptAmount <> OutAmt Then
                    If OutAmt < rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & objW.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                        Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                        End If
                        DiffAmt = rcptAmount - OutAmt
                        'ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                        'ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})
                        If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                            CreateMilkTransferReturnAdjustmentDoc(obj, trans)
                        End If
                    ElseIf OutAmt > rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & objW.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                        Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                        If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                            Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                        End If
                        DiffAmt = OutAmt - rcptAmount
                        'ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                        'ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})

                        If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                            CreateMilkTransferReturnAdjustmentDoc(obj, trans)
                        End If
                    End If
                End If
            Else
                If objW.Arr IsNot Nothing AndAlso objW.Arr.Count > 0 Then
                    '' entry for chamber wise
                    If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                        CostingMethod = clsInventoryMovementNew.getCostingMethod(objW.Item_Code, trans)
                        qry = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement where source_doc_no='" & obj.Dispatch_Challan_No & "' and Item_Code='" & objW.Item_Code & "' and InOut='O' and Trans_Type='Transfer' "
                        CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) 'clsInventoryMovement.GetCost(CostingMethod, obj.Arr(i).Item_Code, obj.From_Location, obj.Arr(i).Out_Qty, obj.Document_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement")
                    Else
                        CostOfItem = 0
                    End If  '' Done By Pankaj Jha For Skipping Cogs GL
                    For Each objTr As clsWeighmentChemberNoDetails In objW.Arr

                        Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        Dim Item_Desc = clsIntimation.getItemName(objTr.Item_Code, trans)
                        If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                            Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        End If

                        Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)
                        Inventory_Control_Ac_FromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, FromLocationSegment, True, False, trans)

                        ''richa agarwal 17 Jan,2020
                        If PickTCAForStockTransferAndTankerDispatch = True Then
                            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                            " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objTr.Item_Code + "'", trans))
                            If clsCommon.myLen(Branch_Ac) <= 0 Then
                                Throw New Exception("Please Map Transfer Clearing Account For  for item " + objTr.Item_Code + " (" & Item_Desc & ")")
                            End If
                            Branch_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_Ac, ToLocationSegment, True, trans)
                        Else
                            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "'", trans))
                            If clsCommon.myLen(Branch_Ac) <= 0 Then
                                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & " To " & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)))
                            End If
                        End If

                        Dim TransferGainLossAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransferGainLossAc) <= 0 Then
                            Throw New Exception("Please Map Transfer Profit/Loss A/c For Item : " & objTr.Item_Code)
                        End If

                        Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                        If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                            Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        End If
                        If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                            Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, False, trans)
                            Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, False, trans)
                        End If


                        Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' and line_no='" & objTr.Line_No & "' ", trans))
                        Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' and line_no='" & objTr.Line_No & "' ", trans))

                        Dim FatValue As Double = (objTr.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans)
                        Dim SnfValue As Double = (objTr.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans)
                        Dim rcptAmount As Double = FatValue + SnfValue

                        Dim Amt As Double = rcptAmount
                        Dim OutAmt As Double = clsMccDispatch.getTransferAmountChamberwise(objW.Challan_No, trans, objTr.Line_No)

                        'ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                        'ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt})
                        'If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                        '    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                        '    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                        'End If

                        ArryLst.Add(New String() {Branch_Ac, Amt})
                        ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt * -1, "", "", "", "", "", "", P_Or_I})
                        If clsCommon.CompairString(P_Or_I, "I") = CompairStringResult.Equal Then
                            clsInventoryMovement.UpdateInvControlAccount(obj.Receipt_Challan_Return_No, "MilkTransferInReturn", objW.Item_Code, "", Inventory_Control_Ac_ToLoc, "", trans)
                        End If
                        If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                            ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt})
                            ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt * -1})
                        End If

                        TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                        Dim DiffAmt As Double = 0

                        If CostOfItem > OutAmt Then
                            DiffAmt = Math.Abs(CostOfItem - OutAmt)
                            ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1, "", "", "", "", "", "", "P"})
                            ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                        ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                            DiffAmt = Math.Abs(OutAmt - CostOfItem)
                            ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt, "", "", "", "", "", "", "P"})
                            ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                        End If

                        If rcptAmount <> OutAmt Then
                            If OutAmt < rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & objTr.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                                Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                End If
                                DiffAmt = rcptAmount - OutAmt
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})
                            ElseIf OutAmt > rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & objW.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                                Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                    Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                End If
                                DiffAmt = OutAmt - rcptAmount
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                            End If
                        End If
                    Next
                    If clsCommon.myLen(strVoucherNoForRecreateOnly) <= 0 Then
                        CreateMilkTransferReturnAdjustmentDoc(obj, trans)
                    End If
                End If
            End If

            Dim GLDesc As String = "Journal Entry Against Milk Transfer In Return- Doc No." & obj.Receipt_Challan_Return_No & " "
            Dim Remarks As String = "Journal Entry against Milk Transfer In Return from location -" & FromLocation & " For Doc No. " & obj.Receipt_Challan_Return_No & ", Transfer Out Doc No: " & obj.Dispatch_Challan_No

            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.location_code, False, strVoucherNoForRecreateOnly, trans, obj.Receipt_Challan_Date, GLDesc, "MT-IR", "Milk Transfer In Return", obj.Receipt_Challan_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, objW.Challan_No)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.location_code, False, trans, obj.Receipt_Challan_Date, GLDesc, "MT-IR", "Milk Transfer In Return", obj.Receipt_Challan_Return_No, "", "O", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, objW.Challan_No)
            End If
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    '' Created By Pankaj Jha on 10-04-2015 For Creating Journal Entry For Tanker Dispatch
    Public Shared Function CreateJournalEntryForTansferInReturn(ByVal obj As clsMilkTransferInReturn, ByVal strVoucherNo As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim isCreated As Boolean = False
        Dim isTransLocallyInit As Boolean = False
        If trans Is Nothing Then
            trans = clsDBFuncationality.GetTransactin()
            isTransLocallyInit = True
        Else
            isTransLocallyInit = False
        End If
        Dim objDisp As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
        If objDisp Is Nothing Then
            If isTransLocallyInit Then
                trans.Rollback()
            End If
            Return False
        End If
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)


        '----GL Entry
        Dim rcptQty As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Net_Weight from tspl_weighment_detail where Weighment_No='" & obj.Weighment_No & "'", trans))

        Dim dsptchQty As Double = clsCommon.myCdbl(objDisp.Net_Qty)
        Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
        If objW Is Nothing Then
            If isTransLocallyInit Then
                trans.Rollback()
            End If
            Return False
        End If

        Try
            Dim FatQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='FAT' ", trans))
            Dim SNFQcPer As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Param_Type='SNF' ", trans))

            Dim FatValue As Double = (objW.Net_Weight * FatQcPer / 100) * objDisp.FAT_RATE
            Dim SnfValue As Double = (objW.Net_Weight * SNFQcPer / 100) * objDisp.SNF_RATE
            Dim rcptAmount As Double = FatValue + SnfValue


            Dim Stock_Transfer_Ac As String = String.Empty
            Dim Inventory_Control_Ac As String = String.Empty
            Dim Branch_Ac As String = String.Empty
            Dim Inventory_Control_Ac_ToLoc As String = String.Empty
            Dim Inventory_Control_Ac_GITLoc As String = String.Empty
            Dim Stock_Transfer_Ac_ToLoc As String = String.Empty
            Dim Stock_Transfer_Ac_GITLoc As String = String.Empty
            Dim GIT_LOC As String = String.Empty
            Dim CostingMethod As Integer = clsInventoryMovementNew.getCostingMethod(objDisp.Item_Code, trans)
            Dim dt As Date = clsCommon.GETSERVERDATE(trans)
            Dim FromLocSeg As String = String.Empty
            Dim ToLocSeg As String = String.Empty
            If obj.isPosted = 1 Then
                dt = obj.Posting_Date
            End If
            Dim CostOfItem As Double = 0
            'Dim CostOfItem As Double = clsInventoryMovement.GetCost(CostingMethod, objDisp.Item_Code, objDisp.MCC_Code, objDisp.Net_Qty, objDisp.Dispatch_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement_new")
            Dim qry As String = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement_new where source_doc_no='" & objDisp.Chalan_NO & "' and Item_Code='" & objDisp.Item_Code & "' and InOut='O' and Trans_Type='DispChallan' "
            If Not isSkipCogsGL Then    '' Done By Pankaj Jha For Skipping Cogs GL
                CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans)) 'clsInventoryMovement.GetCost(CostingMethod, obj.Arr(i).Item_Code, obj.From_Location, obj.Arr(i).Out_Qty, obj.Document_Date, clsCommon.GetPrintDate(dt, "dd/MMM/yyyy"), True, trans, "tspl_inventory_movement")
            Else
                CostOfItem = 0
            End If  '' Done By Pankaj Jha For Skipping Cogs GL

            'FromLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & objDisp.MCC_Code & "'", trans))
            'ToLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & objDisp.Mcc_Or_Plant_Code & "'", trans))
            FromLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & objDisp.Mcc_Or_Plant_Code & "'", trans))
            ToLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & objDisp.MCC_Code & "'", trans))
            If clsCommon.myLen(FromLocSeg) <= 0 Then
                Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & objDisp.Mcc_Or_Plant_Code)
            End If

            If clsCommon.myLen(ToLocSeg) <= 0 Then
                Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & objDisp.MCC_Code)
            End If

            Inventory_Control_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_Transfer_In from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objDisp.Item_Code & "') ", trans))
            If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                Throw New Exception("Please Map Stock Transfer In A/C From Purchase Account Set For Item : " & objDisp.Item_Code)
            End If

            Inventory_Control_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocSeg, True, trans)

            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & ToLocSeg & "' and to_location='" & FromLocSeg & "'", trans))
            If clsCommon.myLen(Branch_Ac) <= 0 Then
                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & ToLocSeg & " To " & FromLocSeg)
            End If

            Dim TransferGainLossAc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objDisp.Item_Code & "') ", trans))
            If clsCommon.myLen(TransferGainLossAc) <= 0 Then
                Throw New Exception("Please Map Transfer Profit/Loss A/c For Item : " & objDisp.Item_Code)
            End If

            GIT_LOC = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location  from tspl_location_master where location_code='" & objDisp.MCC_Code & "'", trans))
            If clsCommon.myLen(GIT_LOC) <= 0 Then
                Throw New Exception("Please Map GIT Location For Location : " & objDisp.MCC_Code)
            End If
            Dim GIT_LOC_SEG As String = String.Empty
            GIT_LOC_SEG = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & GIT_LOC & "'", trans))
            If clsCommon.myLen(GIT_LOC_SEG) <= 0 Then
                Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & GIT_LOC)
            End If
            GIT_LOC = GIT_LOC_SEG
            Inventory_Control_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, GIT_LOC, True, trans)
            Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, trans)



            Stock_Transfer_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stock_transfer_Ac from  tspl_sales_accounts where Sales_class_code=(select Sale_class_code  from tspl_item_master where Item_Code='" & objDisp.Item_Code & "') ", trans))
            If clsCommon.myLen(Stock_Transfer_Ac) <= 0 Then
                Throw New Exception("Please Map Stock Transfer A/C From Sales Account Set For Item : " & objDisp.Item_Code)
            End If
            Stock_Transfer_Ac_GITLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, GIT_LOC, True, trans)
            Stock_Transfer_Ac_ToLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Stock_Transfer_Ac, ToLocSeg, True, trans)
            TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocSeg, True, trans)
            Dim ArryLst As ArrayList = New ArrayList()

            If rcptAmount < objDisp.Amount Then
                ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, rcptAmount * -1, "", "", "", "", "", "", "P"})
                ArryLst.Add(New String() {Branch_Ac, rcptAmount})
                ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, objDisp.Amount})
                ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, objDisp.Amount * -1})

                Dim DiffAmt As Double = 0

                If CostOfItem > objDisp.Amount Then
                    DiffAmt = Math.Abs(CostOfItem - objDisp.Amount)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                ElseIf CostOfItem < objDisp.Amount And CostOfItem <> 0 Then
                    DiffAmt = Math.Abs(objDisp.Amount - CostOfItem)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                End If

                isCreated = clsJournalMaster.FunGrnlEntryWithTrans(ToLocSeg, True, strVoucherNo, trans, clsCommon.GetPrintDate(obj.Receipt_Challan_Date, "dd/MMM/yyyy"), " Against Milk Transfer In Retun, receipt Challan Return No:  -" + obj.Receipt_Challan_Return_No + "  from " + objDisp.MCC_Code + " to  " + objDisp.Mcc_Or_Plant_Code, "MT-IN", "Milk Transfer In Return", obj.Receipt_Challan_Return_No, "", "C", objW.Item_Code, objW.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, " ", " Milk Tranfer In Return,Receipt Challan Return No:  -" + obj.Receipt_Challan_Return_No + "  From " & clsLocation.GetName(objDisp.MCC_Code, trans) & "  to " & clsLocation.GetName(objDisp.Mcc_Or_Plant_Code, trans))

                If clsCommon.myLen(strVoucherNo) <= 0 Then
                    ' isCreated = ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                    isCreated = CreateMilkTransferReturnAdjustmentDoc(obj, trans)
                End If

            ElseIf rcptAmount > objDisp.Amount Then
                ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, rcptAmount, "", "", "", "", "", "", "P"})
                ArryLst.Add(New String() {Branch_Ac, rcptAmount * -1})
                ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, objDisp.Amount * -1})
                ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, objDisp.Amount})
           
                Dim DiffAmt As Double = 0

                If CostOfItem > objDisp.Amount Then
                    DiffAmt = Math.Abs(CostOfItem - objDisp.Amount)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                ElseIf CostOfItem < objDisp.Amount And CostOfItem <> 0 Then
                    DiffAmt = Math.Abs(objDisp.Amount - CostOfItem)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                End If

                isCreated = clsJournalMaster.FunGrnlEntryWithTrans(ToLocSeg, True, strVoucherNo, trans, clsCommon.GetPrintDate(obj.Receipt_Challan_Date, "dd/MMM/yyyy"), " Against Milk Transfer In , receipt Challan No:  -" + obj.Receipt_Challan_No + "  from " + objDisp.MCC_Code + " to  " + objDisp.Mcc_Or_Plant_Code, "MT-IN", "Milk Transfer In", obj.Receipt_Challan_No, "", "C", objW.Item_Code, objW.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, " ", " Milk Tranfer In From " & clsLocation.GetName(objDisp.MCC_Code, trans) & "  to " & clsLocation.GetName(objDisp.Mcc_Or_Plant_Code, trans))
                If clsCommon.myLen(strVoucherNo) <= 0 Then
                    isCreated = CreateMilkTransferReturnAdjustmentDoc(obj, trans)
                End If
              
            Else

                ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, rcptAmount, "", "", "", "", "", "", "P"})
                ArryLst.Add(New String() {Branch_Ac, rcptAmount * -1})
                ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, objDisp.Amount * -1})
                ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, objDisp.Amount})
     
                Dim DiffAmt As Double = 0

                If CostOfItem > objDisp.Amount Then
                    DiffAmt = Math.Abs(CostOfItem - objDisp.Amount)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                ElseIf CostOfItem < objDisp.Amount And CostOfItem <> 0 Then
                    DiffAmt = Math.Abs(objDisp.Amount - CostOfItem)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1, "", "", "", "", "", "", "P"})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                End If

                isCreated = clsJournalMaster.FunGrnlEntryWithTrans(ToLocSeg, True, strVoucherNo, trans, clsCommon.GetPrintDate(obj.Receipt_Challan_Date, "dd/MMM/yyyy"), " Against Milk Transfer In , receipt Challan No:  -" + obj.Receipt_Challan_No + "  from " + objDisp.MCC_Code + " to  " + objDisp.Mcc_Or_Plant_Code, "MT-IN", "Milk Transfer In", obj.Receipt_Challan_No, "", "C", objW.Item_Code, objW.Item_Desc, objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, " ", " Milk Tranfer In From " & clsLocation.GetName(objDisp.MCC_Code, trans) & "  to " & clsLocation.GetName(objDisp.Mcc_Or_Plant_Code, trans))
            End If

            'Commiting/Rollbacking Transaction if Transaction Is Locally Initialized
            If isTransLocallyInit Then
                If isCreated Then
                    trans.Commit()
                Else
                    trans.Rollback()
                End If
            End If
        Catch ex As Exception
            If isTransLocallyInit Then
                trans.Rollback()
            End If
            Throw New Exception(ex.Message)
        End Try


        Return isCreated
    End Function

    Public Shared Function CreateMilkTransferReturnAdjustmentDoc(obj As clsMilkTransferInReturn, trans As SqlTransaction) As Boolean
        Dim rValue As Boolean = False
        Try
            Dim AdjustmentNo As String = clsDBFuncationality.getSingleValue("select Adjustment_No  from TSPL_ADJUSTMENT_HEADER WHERE  Against_Transfer_In_Doc_No='" & obj.Receipt_Challan_No & "'", trans)
            If clsCommon.myLen(AdjustmentNo) > 0 Then
                Dim objAdjOut As New ClsAdjustments
                objAdjOut = objAdjOut.GetData(AdjustmentNo, "ADJ", NavigatorType.Current, trans)
                objAdjOut.Adjustment_No = ""
                objAdjOut.Adjustment_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                objAdjOut.Posting_Date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                objAdjOut.EntryDateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy")
                objAdjOut.Against_Transfer_In_Return_Doc_No = obj.Receipt_Challan_Return_No
                objAdjOut.Against_Tanker_Dispatch_Doc_No = obj.Dispatch_Challan_No
                objAdjOut.Against_Transfer_In_Doc_No = obj.Receipt_Challan_No
                objAdjOut.Loc_Code = objAdjOut.Loc_Code
                objAdjOut.Loc_Desc = clsLocation.GetName(objAdjOut.Loc_Code, trans)
                If clsCommon.CompairString(objAdjOut.Trans_Type, "In") = CompairStringResult.Equal Then
                    objAdjOut.Trans_Type = "Out"
                Else
                    objAdjOut.Trans_Type = "In"
                End If
                objAdjOut.Description = " Auto Adjustment Against Milk Transfer In Return No: " & obj.Receipt_Challan_Return_No & " and  Milk Transfer In No: " & obj.Receipt_Challan_No & " and Tanker Dispatch Challan No: " & obj.Dispatch_Challan_No & " From Location: " & clsLocation.GetName(objAdjOut.FromLocation, trans) & " To Location : " & clsLocation.GetName(objAdjOut.ToLocation, trans)
                If Not objAdjOut.Arr Is Nothing Then
                    rValue = objAdjOut.SaveData(objAdjOut, True, "", trans)
                    rValue = ClsAdjustments.PostData(objAdjOut.Adjustment_No, objAdjOut.Trans_Type, trans, True)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Public Shared Function deleteData(ByVal strReceiptChallanNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim isDeleted As Boolean = True
            '            Dim qry As String = "delete from TSPL_Milk_Transfer_In_Paper_Seal_Details where  chalan_No='" & strReceiptChallanNo & "'"
            '           isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strReceiptChallanNo, "TSPL_MILK_TRANSFER_IN_RETURN", "receipt_challan_Return_no", trans)
            Dim qry As String = "delete from TSPL_MILK_TRANSFER_IN_RETURN where  receipt_challan_Return_no='" & strReceiptChallanNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkTransferInReturn
        Dim whrCls As String = String.Empty
        whrCls = " and TSPL_MILK_TRANSFER_IN_RETURN.comp_code='" & objCommonVar.CurrentCompanyCode & "'"
        If Not clsMccMaster.isCurrentUserHO(trans) Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = whrCls & " and TSPL_MILK_TRANSFER_IN_RETURN.location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        Dim obj As New clsMilkTransferInReturn
        Try
            ' Dim obj As New clsMilkTransferInReturn
            Dim qst As String = " select *   From TSPL_MILK_TRANSFER_IN_RETURN   where 1=1  "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No in ('" + strCode + "')  "
                Case NavigatorType.Next
                    qst += " and TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No in (select min(Receipt_Challan_Return_No ) from TSPL_MILK_TRANSFER_IN_RETURN where Receipt_Challan_Return_No  >'" + strCode + "' " & whrCls & " )"
                Case NavigatorType.First
                    qst += " and TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No in (select MIN(Receipt_Challan_Return_No ) from TSPL_MILK_TRANSFER_IN_RETURN  where  1=1 " & whrCls & " ) "
                Case NavigatorType.Last
                    qst += " and TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No in (select Max(Receipt_Challan_Return_No ) from TSPL_MILK_TRANSFER_IN_RETURN where  1=1 " & whrCls & " )"
                Case NavigatorType.Previous
                    qst += " and TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No in (select Max(Receipt_Challan_Return_No ) from TSPL_MILK_TRANSFER_IN_RETURN where Receipt_Challan_Return_No  <'" + strCode + "'" & whrCls & " ) "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Receipt_Challan_Return_No = clsCommon.myCstr(dt.Rows(0)("Receipt_Challan_Return_No"))
                obj.Receipt_Challan_Date = clsCommon.GetPrintDate(dt.Rows(0)("Receipt_Challan_Return_Date"), "dd/MMM/yyyy hh:mm:ss tt")
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.PriceCode = clsCommon.myCstr(dt.Rows(0)("PriceCode"))
                obj.FAT_R = clsCommon.myCdbl(dt.Rows(0)("FAT_R"))
                obj.FAT_W = clsCommon.myCdbl(dt.Rows(0)("FAT_W"))
                obj.SNF_R = clsCommon.myCdbl(dt.Rows(0)("SNF_R"))
                obj.SNF_W = clsCommon.myCdbl(dt.Rows(0)("SNF_W"))
                obj.Transfer_Price = clsCommon.myCdbl(dt.Rows(0)("Transfer_Price"))
                obj.Receipt_Challan_No = clsCommon.myCstr(dt.Rows(0)("Receipt_Challan_No"))
                obj.Dispatch_Challan_No = clsCommon.myCstr(dt.Rows(0)("Dispatch_Challan_No"))
                obj.Weighment_No = clsCommon.myCstr(dt.Rows(0)("Weighment_No"))
                obj.Qc_No = clsCommon.myCstr(dt.Rows(0)("Qc_No"))
                obj.Gate_Entry_no = clsCommon.myCstr(dt.Rows(0)("Gate_Entry_no"))
                obj.location_code = clsCommon.myCstr(dt.Rows(0)("location_code"))
                obj.km_reading_receipt = clsCommon.myCstr(dt.Rows(0)("km_reading_receipt"))
                obj.isNewSealNo = clsCommon.myCdbl(dt.Rows(0)("isNewSealNo"))
                obj.Receipt_Control_FAT = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_FAT"))
                obj.Receipt_Control_SNF = clsCommon.myCdbl(dt.Rows(0)("Receipt_Control_SNF"))
                'If obj.isNewSealNo = 1 Then
                '    obj.New_Seal_No1 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No1"))
                '    obj.New_Seal_No2 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No2"))
                '    obj.New_Seal_No3 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No3"))
                '    obj.New_Seal_No4 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No4"))
                '    obj.New_Seal_No5 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No5"))
                '    obj.New_Seal_No6 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No6"))
                '    obj.New_Seal_No7 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No7"))
                '    obj.New_Seal_No8 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No8"))
                '    obj.New_Seal_No9 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No9"))
                '    obj.New_Seal_No10 = clsCommon.myCstr(dt.Rows(0)("New_Seal_No10"))
                'End If

                obj.isPosted = clsCommon.myCstr(dt.Rows(0)("isPosted"))
                If obj.isPosted = 1 Then
                    obj.Posting_Date = clsCommon.myCDate(dt.Rows(0)("Posting_Date"))
                End If
                'obj.arrPaperSeal = clsTransferInPaperSealDetail.getData(obj.Receipt_Challan_No)
                'Return obj
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return obj
    End Function

    'Public Shared Function ReverseAndUnpost(ByVal strDocNo As String) As Boolean
    '    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
    '    'Try
    '    '    If clsCommon.myLen(strDocNo) <= 0 Then
    '    '        Throw New Exception("Please select a Gate Entry No")
    '    '    End If
    '    '    Dim Qry As String = "select isPosted from Tspl_Gate_Entry_Details where Gate_Entry_No='" + strDocNo + "'"
    '    '    If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
    '    '        Throw New Exception("Transaction status should be posted for reverse and unpost")
    '    '    End If
    '    '    Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Weighment_Detail where gate_entry_no='" & strDocNo & "' union all select COUNT(*) as row_Count from tspl_quality_check where gate_entry_no='" & strDocNo & "') xx ", trans)
    '    '    If isUsed > 0 Then
    '    '        Throw New Exception("Gate Entry No is in use")
    '    '    End If
    '    '    Qry = "Update Tspl_Gate_Entry_Details set isPosted = 0,Posting_Date=null where gate_entry_no='" + strDocNo + "'"
    '    '    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
    '    '    trans.Commit()
    '    '    Return True
    '    'Catch ex As Exception
    '    '    trans.Rollback()
    '    '    Throw New Exception(ex.Message)
    '    'End Try
    'End Function

End Class




