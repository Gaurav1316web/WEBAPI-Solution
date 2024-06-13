Imports common
Imports System.Data.SqlClient
Public Class clsMilkTransferIn
    Public Document_Amount As Double = 0
    Public IsAgainstJobWork As Integer = 0
    Public Joblocation_Code As String = Nothing
    Public AcknowEntryDocument_No As String = String.Empty
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
        qry = "select Receipt_Challan_No from tspl_milk_transfer_in where Gate_Entry_no='" & strGateEntryNo & "' "
        Doc_No = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        Return Doc_No
    End Function
    Public Shared Function saveData(ByVal obj As clsMilkTransferIn, ByVal trans As SqlTransaction) As Boolean
        Dim issaved As Boolean = True
        Try

            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMilkTransferIn, obj.location_code, obj.Receipt_Challan_Date, trans)
            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "PriceCode", clsCommon.myCstr(obj.PriceCode))
            clsCommon.AddColumnsForChange(coll, "FAT_R", clsCommon.myCstr(obj.FAT_R))
            clsCommon.AddColumnsForChange(coll, "FAT_W", clsCommon.myCstr(obj.FAT_W))
            clsCommon.AddColumnsForChange(coll, "SNF_W", clsCommon.myCstr(obj.SNF_W))
            clsCommon.AddColumnsForChange(coll, "SNF_R", clsCommon.myCstr(obj.SNF_R))
            clsCommon.AddColumnsForChange(coll, "IsAgainstJobWork", obj.IsAgainstJobWork)
            clsCommon.AddColumnsForChange(coll, "Joblocation_Code", obj.Joblocation_Code)
            clsCommon.AddColumnsForChange(coll, "Transfer_Price", clsCommon.myCstr(obj.Transfer_Price))
            clsCommon.AddColumnsForChange(coll, "Receipt_Challan_No", clsCommon.myCstr(obj.Receipt_Challan_No))
            clsCommon.AddColumnsForChange(coll, "AcknowEntryDocument_No", clsCommon.myCstr(obj.AcknowEntryDocument_No), True)
            clsCommon.AddColumnsForChange(coll, "Receipt_Challan_Date", clsCommon.GetPrintDate(obj.Receipt_Challan_Date, "dd/MMM/yyyy hh:mm tt"))
            clsCommon.AddColumnsForChange(coll, "Dispatch_Challan_No", clsCommon.myCstr(obj.Dispatch_Challan_No))
            clsCommon.AddColumnsForChange(coll, "Weighment_No", clsCommon.myCstr(obj.Weighment_No))
            clsCommon.AddColumnsForChange(coll, "Qc_No", clsCommon.myCstr(obj.Qc_No))
            clsCommon.AddColumnsForChange(coll, "Gate_Entry_no", clsCommon.myCstr(obj.Gate_Entry_no))
            clsCommon.AddColumnsForChange(coll, "location_code", clsCommon.myCstr(obj.location_code))
            clsCommon.AddColumnsForChange(coll, "km_reading_receipt", clsCommon.myCstr(obj.km_reading_receipt))
            'clsCommon.AddColumnsForChange(coll, "isNewSealNo", clsCommon.myCdbl(obj.isNewSealNo))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_FAT", clsCommon.myCdbl(obj.Receipt_Control_FAT))
            clsCommon.AddColumnsForChange(coll, "Receipt_Control_SNF", clsCommon.myCdbl(obj.Receipt_Control_SNF))
            clsCommon.AddColumnsForChange(coll, "Document_Amount", clsCommon.myCdbl(obj.Document_Amount))
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
            'clsCommon.AddColumnsForChange(coll, "isPosted", obj.isPosted)
            'If obj.isPosted = 1 Then
            '    clsCommon.AddColumnsForChange(coll, "Posting_Date", clsCommon.GetPrintDate(obj.Posting_Date, "dd/MMM/yyyy"))
            'End If
            clsCommon.AddColumnsForChange(coll, "Modified_By", obj.Modified_By)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(obj.Modified_Date, "dd/MMM/yyyy hh:mm:ss tt"))
            clsCommon.AddColumnsForChange(coll, "Comp_Code", obj.Comp_Code)
            If obj.isNewEntry Then
                clsCommon.AddColumnsForChange(coll, "Created_By", obj.Created_By)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(obj.Created_Date, "dd/MMM/yyyy hh:mm:ss tt"))
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_milk_transfer_in", OMInsertOrUpdate.Insert, "", trans)
            Else
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, obj.Receipt_Challan_No, "tspl_milk_transfer_in", "Receipt_Challan_No", trans)
                issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "tspl_milk_transfer_in", OMInsertOrUpdate.Update, "tspl_milk_transfer_in.Receipt_Challan_No='" + obj.Receipt_Challan_No + "'", trans)
            End If
            'issaved = issaved And clsTransferInPaperSealDetail.SaveData(obj.arrPaperSeal, trans, False)
            'issaved = issaved And clsTransferInManualSealDetail.SaveData(obj.arrPaperSeal, trans, False)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return issaved

    End Function
    Public Shared Function getFinder(ByVal whrcls As String, ByVal curcode As String, ByVal isButtonClicked As Boolean) As String
        Dim str As String = ""
        Try
            'Ticket no- TEC/05/11/18-000366 order date wise desc
            Dim qry As String = "select tspl_milk_transfer_in.Receipt_Challan_No as [Receipt_Challan_No] ,convert(varchar,tspl_milk_transfer_in.Receipt_Challan_Date,103) as [Receipt Challan Date] ,tspl_milk_transfer_in.Dispatch_Challan_No as [Dispatch Challan No] ,tspl_milk_transfer_in.Weighment_No as [Weighment No] ,tspl_milk_transfer_in.Qc_No as [Qc No] ,tspl_milk_transfer_in.Gate_Entry_no as [Gate Entry No] ,case when tspl_gate_entry_details.In_Return=1 then 'Yes' else 'No' end as [Milk In Return] ,tspl_milk_transfer_in.location_code as [Dispatched From] ,tspl_milk_transfer_in.km_reading_receipt as [Km Reading Receipt]  ,case when isnull( tspl_milk_transfer_in.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,convert(varchar,tspl_milk_transfer_in.Posting_Date,103) as [Posting Date] ,tspl_milk_transfer_in.Comp_Code as [Company Code] ,tspl_milk_transfer_in.Created_By as [Created By] ,convert(varchar,cast(tspl_milk_transfer_in.Created_Date as date),103) as [Created Date] ,tspl_milk_transfer_in.Modified_By as [Modified By] ,convert(varchar,cast(tspl_milk_transfer_in.Modified_Date as date),103) as [Modified Date]  From tspl_milk_transfer_in  left outer join Tspl_Gate_Entry_Details on tspl_milk_transfer_in.Gate_Entry_No=Tspl_Gate_Entry_Details.Gate_Entry_No"
            str = clsCommon.ShowSelectForm("MLKTRNSFRIN", qry, "Receipt_Challan_No", whrcls, curcode, "tspl_milk_transfer_in.Receipt_Challan_Date desc", isButtonClicked, "tspl_milk_transfer_in.Receipt_Challan_Date")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return str
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
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("Transaction No not found for reverse and unpost")
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_code,Receipt_Challan_Date from tspl_milk_transfer_in where Receipt_Challan_No='" + strCode + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMilkTransferIn, dt.Rows(0)("location_code"), dt.Rows(0)("Receipt_Challan_Date"), trans)

            End If
            Dim Qry As String = "select isPosted from TSPL_MILK_TRANSFER_IN where Receipt_Challan_No='" + strCode + "'"
            If Not clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Transaction status should be posted for reverse and unpost")
            End If
            ''Check Return Doc for MTI
            Qry = "select count(1) as Cnt from TSPL_MILK_TRANSFER_IN_RETURN where Receipt_Challan_No='" + strCode + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Qry, trans)) = 1 Then
                Throw New Exception("Return Document has created against Milk Transfer In")
            End If

            ''Delete Consumption Entry by balwinder on 09/08/2017
            Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in (" + Environment.NewLine +
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

            Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable("SELECT Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Transfer_In_Doc_No='" + strCode + "'", trans)
            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                For Each dr As DataRow In dtTemp.Rows
                    Dim AdjustmentNo As String = clsCommon.myCstr(dr("Adjustment_No"))
                    If clsCommon.myLen(AdjustmentNo) > 0 Then
                        Dim AdjVoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='IC-AD' and Source_Doc_No='" + AdjustmentNo + "'", trans)
                        If clsCommon.myLen(AdjVoucherNo) > 0 Then
                            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, AdjVoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                            Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + AdjVoucherNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                            Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + AdjVoucherNo + "'"
                            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        End If
                        Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No IN (SELECT Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Transfer_In_Doc_No = '" + strCode + "') and Trans_Type='IC-AD'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Qry = "delete from TSPL_ADJUSTMENT_DETAIL where Adjustment_No IN (SELECT Adjustment_No FROM TSPL_ADJUSTMENT_HEADER WHERE Against_Transfer_In_Doc_No = '" + strCode + "')"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Qry = "delete from TSPL_ADJUSTMENT_HEADER where Against_Transfer_In_Doc_No ='" + strCode + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                Next
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
            Dim strMilkJobWorkTransfer As String = clsDBFuncationality.getSingleValue("select top 1 Document_Code from TSPL_MILK_JOBWORK_TRANSFER_HEAD where Milk_Transfer_In='" & strCode & "'", trans)
            If clsCommon.myLen(strMilkJobWorkTransfer) > 0 Then
                clsMilkJobworkTransfer.ReverseAndUnpost(strMilkJobWorkTransfer, trans)
            End If

            Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='MT-IN' and Source_Doc_No='" + strCode + "'", trans)
            If clsCommon.myLen(VoucherNo) > 0 Then
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, VoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            End If
            'Qry = "delete from TSPL_MILK_TRANSFER_IN where Receipt_Challan_No='" + strCode + "'"
            'clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            clsBatchInventoryNew.DeleteData("MilkTransferIn", strCode, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "TSPL_INVENTORY_MOVEMENT_NEW", "Source_Doc_No", trans)
            Qry = "delete from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No= '" + strCode + "'  and trans_type='MilkTransferIn'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)

            Qry = "Update TSPL_MILK_TRANSFER_IN set isPosted=0 where Receipt_Challan_No='" + strCode + "'"
            clsDBFuncationality.ExecuteNonQuery(Qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strCode, "tspl_milk_transfer_in", "Receipt_Challan_No", trans)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function CreateMilkJobWorkTransfer(ByVal trans As SqlTransaction, ByVal objMTIn As clsMilkTransferIn) As Boolean
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


    Public Shared Function postData(ByVal strReceiptChallanNo As String) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            postData(strReceiptChallanNo, trans)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Shared Function postData(ByVal strReceiptChallanNo As String, ByVal trans As SqlTransaction) As Boolean
        Return postData(strReceiptChallanNo, "", trans)
    End Function

    Public Shared Function postData(ByVal strReceiptChallanNo As String, ByVal strVoucherNoForRecreateOnly As String, ByVal trans As SqlTransaction) As Boolean
        Dim AllowBulkProcMCCwithoutTankerDispatch As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcMCCwithoutTankerDispatch, clsFixedParameterCode.AllowBulkProcMCCwithoutTankerDispatch, trans))
        Dim isTankerDispatchFinancialImpactInTransferIn As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchFinancialImpactInTransferIn, clsFixedParameterCode.TankerDispatchFinancialImpactInTransferIn, trans)) = 1, True, False)
        Try
            If (clsCommon.myLen(strReceiptChallanNo) <= 0) Then
                Throw New Exception("Receipt Challan  No not found to Post")
            End If

            Dim obj As clsMilkTransferIn = clsMilkTransferIn.getData(strReceiptChallanNo, NavigatorType.Current, trans)
            If (obj Is Nothing OrElse clsCommon.myLen(obj.Receipt_Challan_No) <= 0) Then
                Throw New Exception("No Data found to Post")
            End If
            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, "MccProc", NavigatorType.Current, trans)
            Dim objD As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
            'trans = clsDBFuncationality.GetTransactin()
            clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMilkTransferIn, obj.location_code, obj.Receipt_Challan_Date, trans)
            If (obj.isPosted = 1) Then
                Throw New Exception("Already Post on :" + obj.Posting_Date)
            End If

            If isTankerDispatchFinancialImpactInTransferIn Then
                Dim coll As New Hashtable()
                Dim objMCT As MIlkComponentType = clsInventoryMovementNew.GetAvgCost("MI", objD.Item_Code, objD.MCC_Code, 1, objD.UOM_Code, 1, 1, objD.Dispatch_Date, objD.Dispatch_Date, False, trans, objD.Chalan_NO)
                clsCommon.AddColumnsForChange(coll, "Avg_FAT_Rate", Math.Round(objMCT.FAT_Cost, 3, MidpointRounding.ToEven))
                clsCommon.AddColumnsForChange(coll, "Avg_SNF_Rate", Math.Round(objMCT.SNF_Cost, 3, MidpointRounding.ToEven))
                clsCommon.AddColumnsForChange(coll, "Avg_FAT_Amount", Math.Round(objD.Avg_FAT_Rate * objD.FAT_KG, 2, MidpointRounding.ToEven))
                clsCommon.AddColumnsForChange(coll, "Avg_SNF_Amount", Math.Round(objD.Avg_SNF_Rate * objD.SNF_KG, 2, MidpointRounding.ToEven))
                clsCommon.AddColumnsForChange(coll, "Avg_Amount", Math.Round(objD.Avg_FAT_Amount + objD.Avg_SNF_Amount, 2, MidpointRounding.ToEven))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_DISPATCH_CHALLAN", OMInsertOrUpdate.Update, "tspl_mcc_dispatch_challan.chalan_no='" + objD.Chalan_NO + "'", trans)

                objD = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
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
                    If objW IsNot Nothing Then
                        FatValue = objW.FAT_Value
                        SnfValue = objW.SNF_Value
                        rcptAmount = objW.Amount

                    End If

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
                End If

                subLocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code from TSPL_MILK_UNLOADING where weighment_no='" & objW.Weighment_No & "'", trans))

                objInventoryMovemnt.InOut = "I"
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
                    objInventoryMovemnt.Other_Location_Code = objW.location_Code
                    objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(objW.location_Code, trans)
                    '' added by Panch Raj for production costing
                    objInventoryMovemnt.Fat_Rate = objW.FAT_Rate
                    objInventoryMovemnt.SNF_Rate = objW.SNF_Rate
                    objInventoryMovemnt.Fat_Amt = objW.FAT_Value
                    objInventoryMovemnt.SNF_Amt = objW.SNF_Value
                End If
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
                Dim isCreateBulkProcPriceChartItemWise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, trans))
                ''richa agarwal 25 June,2019 add data for Batch Item New table when item is of Batch wise TEC/25/06/19-000566
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select TSPL_Milk_unloading_Chember_Details.Line_No,TSPL_Milk_unloading_Chember_Details.Batch_No,TSPL_Milk_unloading_Chember_Details.Chamber_Qty ,TSPL_Milk_unloading_Chember_Details.UOM ,TSPL_Milk_unloading_Chember_Details.Item_Code    from TSPL_MILK_UNLOADING left join TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No =TSPL_Milk_unloading_Chember_Details.Unloading_No where  Gate_Entry_No='" & obj.Gate_Entry_no & "' and isnull(TSPL_Milk_unloading_Chember_Details.Batch_No ,'')<>'' and TSPL_Milk_unloading_Chember_Details.IsBatchWise ='Y'", trans)
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
                            subLocCode = clsDBFuncationality.getSingleValue("select TSPL_Milk_unloading_Chember_Details.Sublocation_Code   from TSPL_MILK_UNLOADING left join " &
                                        "TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No =TSPL_Milk_unloading_Chember_Details.Unloading_No " &
                                        "where TSPL_Milk_unloading_Chember_Details.line_no='" & clsCommon.myCstr(dt.Rows(i)("Line_No")) & "' and Gate_Entry_No='" & obj.Gate_Entry_no & "'", trans)
                        Else
                            subLocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code from TSPL_MILK_UNLOADING where weighment_no='" & objW.Weighment_No & "'", trans))
                        End If
                        objBatchInvNew.Qty = clsCommon.myCdbl(dt.Rows(i)("Chamber_Qty"))
                        objBatchInvNew.UOM = clsCommon.myCstr(dt.Rows(i)("UOM"))
                        objBatchInvNew.Item_Code = clsCommon.myCstr(dt.Rows(i)("Item_Code"))
                        objBatchInvNew.In_Out_Type = "I"
                        If clsCommon.myLen(objBatchInvNew.Batch_No) > 0 AndAlso objBatchInvNew.Qty <> 0 Then
                            arr.Add(objBatchInvNew)
                        End If
                        clsBatchInventoryNew.SaveData("MilkTransferIn", obj.Receipt_Challan_No, obj.Receipt_Challan_Date, "I", clsCommon.myCstr(dt.Rows(i)("Item_Code")), subLocCode, clsCommon.myCstr(dt.Rows(i)("Line_No")), 0, clsCommon.myCstr(dt.Rows(i)("UOM")), arr, trans)
                    Next

                End If

                For Each objTr As clsWeighmentChemberNoDetails In objW.Arr

                    FatQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "' and Line_No='" & objTr.Line_No & "' and Param_Type='FAT' ", trans))
                    SNFQcPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.Qc_No & "'  and Line_No='" & objTr.Line_No & "' and Param_Type='SNF' ", trans))
                    If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(objTr.isCanType), "1") = CompairStringResult.Equal Then
                            Dim sno As Integer = clsDBFuncationality.getSingleValue("select SNO  from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where chalan_no='" & objD.Chalan_NO & "' and iscanType=1", trans)
                            FatValue = (objTr.Net_Weight * FatQcPer / 100) * objD.arr(sno - 1).FAT_Rate
                            SnfValue = (objTr.Net_Weight * SNFQcPer / 100) * objD.arr(sno - 1).SNF_Rate
                        Else
                            FatValue = (objTr.Net_Weight * FatQcPer / 100) * objD.arr(objTr.Line_No - 1).FAT_Rate
                            SnfValue = (objTr.Net_Weight * SNFQcPer / 100) * objD.arr(objTr.Line_No - 1).SNF_Rate
                        End If
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

                    ' done by priti BHA/16/07/18-000172 for saving sublocation chamber wise
                    'If isCreateBulkProcPriceChartItemWise = 1 Then
                    '    subLocCode = clsDBFuncationality.getSingleValue("select TSPL_Milk_unloading_Chember_Details.Sublocation_Code   from TSPL_MILK_UNLOADING left join " & _
                    '                "TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No =TSPL_Milk_unloading_Chember_Details.Unloading_No " & _
                    '                "where TSPL_Milk_unloading_Chember_Details.line_no='" & objTr.Line_No & "' and Gate_Entry_No='" & obj.Gate_Entry_no & "'", trans)
                    'Else
                    '    subLocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sub_location_Code from TSPL_MILK_UNLOADING where weighment_no='" & objW.Weighment_No & "'", trans))
                    'End If
                    subLocCode = clsDBFuncationality.getSingleValue("select CASE WHEN ISNULL(TSPL_Milk_unloading_Chember_Details.Sublocation_Code,'')='' THEN TSPL_MILK_UNLOADING.Sub_location_Code ELSE TSPL_Milk_unloading_Chember_Details.Sublocation_Code END   from TSPL_MILK_UNLOADING left join " &
                                    "TSPL_Milk_unloading_Chember_Details on TSPL_MILK_UNLOADING.Unloading_No =TSPL_Milk_unloading_Chember_Details.Unloading_No " &
                                    "where TSPL_Milk_unloading_Chember_Details.line_no='" & objTr.Line_No & "' and Gate_Entry_No='" & obj.Gate_Entry_no & "'", trans)

                    objInventoryMovemnt = New clsInventoryMovementNew
                    objInventoryMovemnt.InOut = "I"
                    objInventoryMovemnt.Location_Code = subLocCode
                    objInventoryMovemnt.main_location = obj.location_code

                    Dim objDispChallan As clsMccDispatch = clsMccDispatch.getData(obj.Dispatch_Challan_No, NavigatorType.Current, trans)
                    If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                        objInventoryMovemnt.Other_Location_Code = objDispChallan.MCC_Code
                        objInventoryMovemnt.Other_Location_Desc = objDispChallan.MCC_Name
                        objInventoryMovemnt.Fat_Rate = objD.arr(objTr.Line_No - 1).FAT_Rate
                        objInventoryMovemnt.SNF_Rate = objD.arr(objTr.Line_No - 1).SNF_Rate
                    Else
                        'objInventoryMovemnt.Other_Location_Code = objW.Dispatched_From_Mcc
                        'objInventoryMovemnt.Other_Location_Desc = clsLocation.GetName(objW.Dispatched_From_Mcc, trans)
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

            clsInventoryMovementNew.SaveData("MilkTransferIn", obj.Receipt_Challan_No, obj.Receipt_Challan_Date, clsCommon.GetPrintDate(obj.Receipt_Challan_Date, "dd/MM/yyyy"), ArrInventoryMovement, trans)

            Dim FromLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Code  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Dispatch_Challan_No & "'", trans))
            Dim ToLoc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Mcc_Or_Plant_code  from TSPL_MCC_Dispatch_Challan where Chalan_NO='" & obj.Dispatch_Challan_No & "'", trans))
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.CreateTransferInGL, trans)) = 1 Then
                If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                    clsMilkTransferIn.CreateTransferInJE(obj, strVoucherNoForRecreateOnly, trans)
                Else
                    clsMilkTransferIn.CreateTransferInJERCDF(obj, strVoucherNoForRecreateOnly, trans)
                End If
            End If

            Dim strQry As String = " update tspl_milk_transfer_in set isPosted='1', Posting_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy") & "' where Receipt_Challan_No='" & strReceiptChallanNo & "'"
            clsDBFuncationality.ExecuteNonQuery(strQry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strReceiptChallanNo, "tspl_milk_transfer_in", "Receipt_Challan_No", trans)
            If AllowBulkProcMCCwithoutTankerDispatch = 0 Then
                CreateConsumptionEntry(obj, subLocCode, trans)
            End If
            If objD.IsAgainstJobWork = 1 Then
                CreateMilkJobWorkTransfer(trans, obj)
            End If

            If isTankerDispatchFinancialImpactInTransferIn Then
                strQry = "select Voucher_No from TSPL_JOURNAL_MASTER where Source_Doc_No='" + objD.Chalan_NO + "' and Source_Code='DI-CH'"
                Dim strVoucherNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strQry, trans))
                If clsCommon.myLen(strVoucherNo) > 0 Then
                    clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strVoucherNo, "TSPL_JOURNAL_MASTER", "Voucher_No", "TSPL_JOURNAL_DETAILS", "Voucher_No", trans)
                    strQry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No='" + strVoucherNo + "'  "
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                    strQry = "delete from TSPL_JOURNAL_MASTER where Voucher_No='" + strVoucherNo + "'"
                    clsDBFuncationality.ExecuteNonQuery(strQry, trans)
                End If
                clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, objD.Chalan_NO, "tspl_inventory_movement_new", "Source_Doc_No", trans)
                strQry = "delete from tspl_inventory_movement_new where trans_Type='DispChallan' and Source_Doc_No='" + objD.Chalan_NO + "'"
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                clsMccDispatch.CreateJEAndInvenotryMovement(objD, trans, obj.Receipt_Challan_No, strVoucherNo)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Shared Function CreateConsumptionEntry(ByVal obj As clsMilkTransferIn, ByVal strSiloNo As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String
        Dim dtItem As DataTable
        Try
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateConsumeEntry, clsFixedParameterCode.CreateConsumeEntry, trans)) > 0 Then
                qry = "select Item_Code,Item_Desc,Qty,UOM,convert(decimal(18,2), case when Qty=0 then 0 else Avg_Cost/Qty end) as Rate,Avg_Cost as Amount,Fat_Per,Fat_KG,SNF_Per, SNF_KG,Fat_Amt,SNF_Amt,Fat_Rate,Fat_Amt,SNF_Rate,SNF_Amt  from TSPL_INVENTORY_MOVEMENT_NEW where Source_Doc_No='" + obj.Receipt_Challan_No + "' and Trans_Type='MilkTransferIn'"
                dtItem = clsDBFuncationality.GetDataTable(qry, trans)
                If dtItem IsNot Nothing AndAlso dtItem.Rows.Count > 0 Then
                    ''Use in RM Consumption 
                    Dim objConsumbption As New ClsJobWorkRMConsum
                    objConsumbption.Trans_Type = "Out"
                    objConsumbption.Adjustment_Date = obj.Receipt_Challan_Date
                    objConsumbption.Posting_Date = obj.Receipt_Challan_Date
                    objConsumbption.EntryDateTime = obj.Receipt_Challan_Date
                    objConsumbption.IsMilkType = 1
                    objConsumbption.Loc_Code = strSiloNo
                    objConsumbption.Loc_Desc = clsLocation.GetName(objConsumbption.Loc_Code, trans)
                    objConsumbption.MainLocationCode = obj.location_code
                    objConsumbption.MainLocationDesc = clsLocation.GetName(objConsumbption.MainLocationCode, trans)

                    objConsumbption.Description = "Adjustment for Consum.Bulk Transfer In :" & obj.Receipt_Challan_No & ""
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

    Public Shared Function CreateTransferInJERCDF(obj As clsMilkTransferIn, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
        Try
            Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
            Dim SettAllowPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1, True, False)
            Dim P_Or_I As String = "P"
            If Not SettAllowPurchaseAccounting Then
                P_Or_I = "I"
            End If
            Dim Inventory_Control_Ac As String = String.Empty
            Dim Non_Stock_Clearing_Ac As String
            Dim CostingMethod As Integer = 0
            Dim CostOfItem As Double = 0
            Dim dt As Date = clsCommon.GETSERVERDATE(trans)
            If obj.isPosted = 1 Then
                dt = obj.Posting_Date
            End If
            Dim ToLocationSegment As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & obj.location_code & "'", trans))
            Dim qry As String = ""
            Dim objW As clsWeighment = clsWeighment.getData(obj.Weighment_No, NavigatorType.Current, False, trans)
            Dim ArryLst As ArrayList = New ArrayList()
            If objW.Arr IsNot Nothing AndAlso objW.Arr.Count > 0 Then

                CostingMethod = clsInventoryMovementNew.getCostingMethod(objW.Item_Code, trans)
                    qry = " select " & IIf(CostingMethod = 1, "avg_cost", IIf(CostingMethod = 2, "FIFO_Cost", IIf(CostingMethod = 3, "LIFO_Cost", " 0 "))) & " from tspl_inventory_movement_new where source_doc_no='" & obj.Receipt_Challan_No & "' and Item_Code='" & objW.Item_Code & "' "
                    CostOfItem = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))

                For Each objTr As clsWeighmentChemberNoDetails In objW.Arr
                    Dim Item_Desc = clsIntimation.getItemName(objTr.Item_Code, trans)
                    qry = "select Inv_Control_Account,Non_Stock_Clearing from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') "
                    Dim dtAccount As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtAccount IsNot Nothing AndAlso dtAccount.Rows.Count > 0 Then
                        Inventory_Control_Ac = clsCommon.myCstr(dtAccount.Rows(0)("Inv_Control_Account"))
                        If clsCommon.myLen(Inventory_Control_Ac) <= 0 Then
                            Throw New Exception("Please Map Inventory Control A/C in Purchase Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        End If
                        Inventory_Control_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Inventory_Control_Ac, ToLocationSegment, True, trans)

                        Non_Stock_Clearing_Ac = clsCommon.myCstr(dtAccount.Rows(0)("Non_Stock_Clearing"))
                        If clsCommon.myLen(Non_Stock_Clearing_Ac) <= 0 Then
                            Throw New Exception("Please BMC Milk Purchase A/C in Purchase Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        End If
                        Non_Stock_Clearing_Ac = clsERPFuncationality.ChangeGLAccountLocationSegment(Non_Stock_Clearing_Ac, ToLocationSegment, True, trans)

                        If clsCommon.CompairString(Inventory_Control_Ac, Non_Stock_Clearing_Ac) = CompairStringResult.Equal Then
                            Throw New Exception("Please [Inventory Control A/C] and [BMC Milk Purchase A/C] should be different in Purchase Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                        End If
                    Else
                        Throw New Exception("Please set Purchase Account Set For Item : " & objTr.Item_Code & " (" & Item_Desc & ")")
                    End If
                    ArryLst.Add(New String() {Inventory_Control_Ac, CostOfItem})
                    ArryLst.Add(New String() {Non_Stock_Clearing_Ac, -1 * CostOfItem, "", "", "", "", "", "", P_Or_I})
                    If clsCommon.CompairString(P_Or_I, "I") = CompairStringResult.Equal Then
                        clsInventoryMovement.UpdateInvControlAccount(obj.Receipt_Challan_No, "MilkTransferIn", objTr.Item_Code, Inventory_Control_Ac, "", "", trans)
                    End If
                Next
            End If

            Dim GLDesc As String = "Journal Entry Against Milk Transfer In- Doc No." & obj.Receipt_Challan_No & " "
            Dim Remarks As String = "Journal Entry against Milk Transfer In  For Doc No. " & obj.Receipt_Challan_No & ", Transfer Out Doc No: " & obj.Dispatch_Challan_No

            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.location_code, False, strVoucherNoForRecreateOnly, trans, obj.Receipt_Challan_Date, GLDesc, "MT-IN", "Milk Transfer In", obj.Receipt_Challan_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, objW.Challan_No)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.location_code, False, trans, obj.Receipt_Challan_Date, GLDesc, "MT-IN", "Milk Transfer In", obj.Receipt_Challan_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, objW.Challan_No)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function CreateTransferInJE(obj As clsMilkTransferIn, strVoucherNoForRecreateOnly As String, trans As SqlTransaction) As Boolean
        '' Developed by Panch Raj on 01/07/2016
        Dim DoNotCreateAdjustmentonMilkTransferInGL As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotCreateAdjustmentonMilkTransferInGL, clsFixedParameterCode.DoNotCreateAdjustmentonMilkTransferInGL, trans))
        Dim isSkipCogsGL As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.SkipCogsEntry, clsFixedParameterCode.SkipCogsEntry, trans)) = 0, False, True)
        Dim SettAllowPurchaseAccounting As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowPurchaseAccounting, clsFixedParameterCode.AllowPurchaseAccounting, trans)) = 1, True, False)
        Dim PickTCAForStockTransferAndTankerDispatch As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickTCAForStockTransferAndTankerDispatch, clsFixedParameterCode.PickTCAForStockTransferAndTankerDispatch, trans)) = 0, False, True)
        Dim P_Or_I As String = "P"
        If Not SettAllowPurchaseAccounting Then ''[BHA/27/11/18-000722] by balwinder on 21/01/2019
            P_Or_I = "I"
        End If

        Dim MCCChamberwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsChamberWiseTanker, clsFixedParameterCode.IsChamberWiseTanker, trans))
        Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, trans))
        Dim isTankerDispatchFinancialImpactInTransferIn As Boolean = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TankerDispatchFinancialImpactInTransferIn, clsFixedParameterCode.TankerDispatchFinancialImpactInTransferIn, trans)) = 1, True, False)
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

            'Skip JV ,Setting- DoNotCreateJVOnSameLocationSegmentInTanDisAndMTIn
            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
            Else
                If clsCommon.CompairString(FromLocationSegment, ToLocationSegment) = CompairStringResult.Equal Then
                    If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotCreateJVOnSameLocationSegmentInTanDisAndMTIn, clsFixedParameterCode.DoNotCreateJVOnSameLocationSegmentInTanDisAndMTIn, trans)) = 1 Then
                        Return True
                    End If
                End If
            End If

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
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
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

                ArryLst.Add(New String() {Branch_Ac, rcptAmount * -1})
                ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, rcptAmount, "", "", "", "", "", "", P_Or_I})
                If clsCommon.CompairString(P_Or_I, "I") = CompairStringResult.Equal Then
                    clsInventoryMovement.UpdateInvControlAccount(obj.Receipt_Challan_No, "MilkTransferIn", objW.Item_Code, Inventory_Control_Ac_ToLoc, "", "", trans)
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
                    Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
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

                ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt, "", "", "", "", "", "", P_Or_I})
                If clsCommon.CompairString(P_Or_I, "I") = CompairStringResult.Equal Then
                    clsInventoryMovement.UpdateInvControlAccount(obj.Receipt_Challan_No, "MilkTransferIn", objW.Item_Code, Inventory_Control_Ac_ToLoc, "", "", trans)
                End If
                If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                    ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                    ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                End If
                Dim strQry = "Update TSPL_Weighment_Detail set Amount='" & clsCommon.myCdbl(Amt) & "' where Weighment_No='" & obj.Weighment_No & "'"
                clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                Dim DiffAmt As Double = 0

                If CostOfItem > OutAmt Then
                    DiffAmt = Math.Abs(CostOfItem - OutAmt)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt, "", "", "", "", "", "", P_Or_I})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                    DiffAmt = Math.Abs(OutAmt - CostOfItem)
                    ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1, "", "", "", "", "", "", P_Or_I})
                    ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                End If

                If rcptAmount <> OutAmt Then
                    If OutAmt < rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac as Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & objW.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)
                        ''richa agarwal 05 feb,2020
                        If PickTCAForStockTransferAndTankerDispatch = True Then
                            Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                            " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objW.Item_Code + "'", trans))
                            If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                Throw New Exception("Please Map Transfer Clearing Account For  for item " + objW.Item_Code + " (" & objW.Item_Desc & ")")
                            End If
                            Branch_AcFromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_AcFromLoc, FromLocationSegment, True, False, trans)
                        Else
                            Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                            If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                            End If
                        End If

                        DiffAmt = rcptAmount - OutAmt
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                        If DoNotCreateAdjustmentonMilkTransferInGL = 0 Then
                            ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                        End If
                    ElseIf OutAmt > rcptAmount Then
                        TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac as Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objW.Item_Code & "') ", trans))
                        If clsCommon.myLen(TransitLossAc) <= 0 Then
                            Throw New Exception("Please Map Transit Loss A/c For Item : " & objW.Item_Code)
                        End If
                        TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)

                        ''richa agarwal 05 feb,2020
                        If PickTCAForStockTransferAndTankerDispatch = True Then
                            Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                            " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objW.Item_Code + "'", trans))
                            If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                Throw New Exception("Please Map Transfer Clearing Account For  for item " + objW.Item_Code + " (" & objW.Item_Desc & ")")
                            End If
                            Branch_AcFromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_AcFromLoc, FromLocationSegment, True, False, trans)
                        Else
                            Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                            If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                            End If
                        End If

                        DiffAmt = OutAmt - rcptAmount
                        ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                        ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})

                        If DoNotCreateAdjustmentonMilkTransferInGL = 0 Then
                            ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
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
                        ''richa agarwal 16 Jan,2020
                        If PickTCAForStockTransferAndTankerDispatch = True Then
                            Branch_Ac = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                            " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objTr.Item_Code + "'", trans))
                            If clsCommon.myLen(Branch_Ac) <= 0 Then
                                Throw New Exception("Please Map Transfer Clearing Account For  for item " + objTr.Item_Code & " (" & Item_Desc & ")")
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

                        'Dim FatValue As Double = Math.Round((objTr.Net_Weight * FatQcPer / 100) * clsMccDispatch.getFATRate(objW.Challan_No, trans), 3, MidpointRounding.ToEven)
                        'Dim SnfValue As Double = Math.Round((objTr.Net_Weight * SNFQcPer / 100) * clsMccDispatch.getSNFRate(objW.Challan_No, trans), 3, MidpointRounding.ToEven)
                        'Dim rcptAmount As Double = FatValue + SnfValue
                        ' done by priti BHA/29/05/18-000038 to rectify problem of gain loss entry

                        Dim FATKG As Double = Math.Round(clsCommon.myCdbl(objTr.Net_Weight) * clsCommon.myCdbl(FatQcPer) / 100, 3, MidpointRounding.ToEven)
                        Dim SNFKG As Double = Math.Round(clsCommon.myCdbl(objTr.Net_Weight) * clsCommon.myCdbl(SNFQcPer) / 100, 3, MidpointRounding.ToEven)
                        Dim rcptAmount As Double = Math.Round(clsMccDispatch.getFATRate(objW.Challan_No, trans) * clsCommon.myCdbl(FATKG) + clsMccDispatch.getSNFRate(objW.Challan_No, trans) * clsCommon.myCdbl(SNFKG), 2)

                        Dim Amt As Double = Math.Round(rcptAmount, 2)
                        ''richa agarwal 18 Feb,2019 find exact line number of chamber ERO/18/02/20-001191
                        'Dim strChamberLineNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select SNO from TSPL_MCC_DISPATCH_CHALLAN_DETAIL where Chalan_No='" & objW.Challan_No & "' and Chamber_Description ='" & objTr.Chamber_Desc & "'", trans)) 
                        Dim OutAmt As Double = clsMccDispatch.getTransferAmountChamberwiseByChamberDescription(objW.Challan_No, trans, objTr.Chamber_Desc)

                        Dim strQry = "Update TSPL_WEIGHMENT_CHEMBER_DETAILS set CH_Amount='" & clsCommon.myCdbl(Amt) & "' where Weighment_No='" & obj.Weighment_No & "' and Line_No='" & objTr.Line_No & "'"
                        clsDBFuncationality.ExecuteNonQuery(strQry, trans)

                        ArryLst.Add(New String() {Branch_Ac, Amt * -1})
                        ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, Amt, "", "", "", "", "", "", P_Or_I})
                        If clsCommon.CompairString(P_Or_I, "I") = CompairStringResult.Equal Then
                            clsInventoryMovement.UpdateInvControlAccount(obj.Receipt_Challan_No, "MilkTransferIn", objTr.Item_Code, Inventory_Control_Ac_ToLoc, "", "", trans)
                        End If
                        If clsCommon.myCdbl(IGnoreGITAccount) = 0 Then
                            ArryLst.Add(New String() {Inventory_Control_Ac_GITLoc, Amt * -1})
                            ArryLst.Add(New String() {Stock_Transfer_Ac_GITLoc, Amt})
                        End If

                        TransferGainLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransferGainLossAc, ToLocationSegment, True, trans)
                        Dim DiffAmt As Double = 0

                        If CostOfItem > OutAmt Then
                            DiffAmt = Math.Abs(CostOfItem - OutAmt)
                            ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt, "", "", "", "", "", "", P_Or_I})
                            ArryLst.Add(New String() {TransferGainLossAc, DiffAmt * -1})
                        ElseIf CostOfItem < OutAmt AndAlso CostOfItem <> 0 Then
                            DiffAmt = Math.Abs(OutAmt - CostOfItem)
                            ArryLst.Add(New String() {Inventory_Control_Ac_ToLoc, DiffAmt * -1, "", "", "", "", "", "", P_Or_I})
                            ArryLst.Add(New String() {TransferGainLossAc, DiffAmt})
                        End If

                        If rcptAmount <> OutAmt Then
                            If OutAmt < rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac as Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & objTr.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)

                                ''richa agarwal 05 feb,2020
                                If PickTCAForStockTransferAndTankerDispatch = True Then
                                    Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objTr.Item_Code + "'", trans))
                                    If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                        Throw New Exception("Please Map Transfer Clearing Account For  for item " + objTr.Item_Code + " ")
                                    End If
                                    Branch_AcFromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_AcFromLoc, FromLocationSegment, True, False, trans)
                                Else
                                    Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                    If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                    End If
                                End If

                                DiffAmt = rcptAmount - OutAmt
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt * -1})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt})
                            ElseIf OutAmt > rcptAmount Then
                                TransitLossAc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Transfer_Gain_Loss_Ac as Loss_Ac from  tspl_purchase_accounts where purchase_class_code=(select purchase_class_code  from tspl_item_master where Item_Code='" & objTr.Item_Code & "') ", trans))
                                If clsCommon.myLen(TransitLossAc) <= 0 Then
                                    Throw New Exception("Please Map Transit Loss A/c For Item : " & objTr.Item_Code)
                                End If
                                TransitLossAc = clsERPFuncationality.ChangeGLAccountLocationSegment(TransitLossAc, FromLocationSegment, True, False, trans)

                                ''richa agarwal 05 feb,2020
                                If PickTCAForStockTransferAndTankerDispatch = True Then
                                    Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT PA.transfer_clearing FROM TSPL_ITEM_MASTER AS IM INNER JOIN " &
                                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " &
                                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + objTr.Item_Code + "'", trans))
                                    If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                        Throw New Exception("Please Map Transfer Clearing Account For  for item " + objTr.Item_Code + " ")
                                    End If
                                    Branch_AcFromLoc = clsERPFuncationality.ChangeGLAccountLocationSegment(Branch_AcFromLoc, FromLocationSegment, True, False, trans)
                                Else
                                    Branch_AcFromLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Branch_account from tspl_branch_account_mapping where From_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & FromLocSeg & "'", trans)) & "' and to_location='" & clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_segment_code from tspl_location_master where Location_Code='" & ToLocSeg & "'", trans)) & "'", trans))
                                    If clsCommon.myLen(Branch_AcFromLoc) <= 0 Then
                                        Throw New Exception("Please Map Account For Branch Account Mapping For Location From  " & FromLocationSegment & " To " & ToLocSeg)
                                    End If
                                End If

                                DiffAmt = OutAmt - rcptAmount
                                ArryLst.Add(New String() {TransitLossAc, DiffAmt})
                                ArryLst.Add(New String() {Branch_AcFromLoc, DiffAmt * -1})
                            End If
                        End If
                    Next

                    If DoNotCreateAdjustmentonMilkTransferInGL = 0 Then
                        ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
                    End If
                End If
            End If

            Dim GLDesc As String = "Journal Entry Against Milk Transfer In- Doc No." & obj.Receipt_Challan_No & " "
            Dim Remarks As String = "Journal Entry against Milk Transfer In from location -" & FromLocation & " For Doc No. " & obj.Receipt_Challan_No & ", Transfer Out Doc No: " & obj.Dispatch_Challan_No

            If strVoucherNoForRecreateOnly IsNot Nothing AndAlso clsCommon.myLen(strVoucherNoForRecreateOnly) > 0 Then
                clsJournalMaster.FunGrnlEntryWithTrans(obj.location_code, False, strVoucherNoForRecreateOnly, trans, obj.Receipt_Challan_Date, GLDesc, "MT-IN", "Milk Transfer In", obj.Receipt_Challan_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, objW.Challan_No)
            Else
                clsJournalMaster.FunGrnlEntryWithTrans(obj.location_code, False, trans, obj.Receipt_Challan_Date, GLDesc, "MT-IN", "Milk Transfer In", obj.Receipt_Challan_No, "", "I", "", "", objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode, ArryLst, , Remarks, objW.Challan_No)
            End If
            'End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    '' Created By Pankaj Jha on 10-04-2015 For Creating Journal Entry For Tanker Dispatch
    Public Shared Function CreateJournalEntryForTansferIn(ByVal obj As clsMilkTransferIn, ByVal strVoucherNo As String, Optional trans As SqlTransaction = Nothing) As Boolean
        Dim DoNotCreateAdjustmentonMilkTransferInGL As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotCreateAdjustmentonMilkTransferInGL, clsFixedParameterCode.DoNotCreateAdjustmentonMilkTransferInGL, trans))
        Dim isCreated As Boolean = False
        Dim isTransLocallyInit As Boolean = False
        ' Checking Transaction is been Passed to Argument or Not, if Not then being initialized locally, 
        'and Maintaining its State that it Initialized Locally Or Passed as Parameter
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

            FromLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & objDisp.MCC_Code & "'", trans))
            ToLocSeg = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Loc_Segment_Code  from tspl_location_master where location_code='" & objDisp.Mcc_Or_Plant_Code & "'", trans))
            If clsCommon.myLen(FromLocSeg) <= 0 Then
                Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & objDisp.MCC_Code)
            End If

            If clsCommon.myLen(ToLocSeg) <= 0 Then
                Throw New Exception(" Location Segment Not Found in Location Master, For Location : " & objDisp.Mcc_Or_Plant_Code)
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

                If clsCommon.myLen(strVoucherNo) <= 0 AndAlso DoNotCreateAdjustmentonMilkTransferInGL = 0 Then
                    isCreated = ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
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
                If clsCommon.myLen(strVoucherNo) <= 0 AndAlso DoNotCreateAdjustmentonMilkTransferInGL = 0 Then
                    isCreated = ClsAdjustments.CreateMilkTransferAdjustmentDoc(objDisp, obj, trans)
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
    Public Shared Function deleteData(ByVal strReceiptChallanNo As String, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select location_code,Receipt_Challan_Date from tspl_milk_transfer_in where Receipt_Challan_No='" + strReceiptChallanNo + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                clsERPFuncationality.ValidateLocationCode(objCommonVar.CurrentCompanyCode, clsUserMgtCode.ModuleProductionDairy, clsUserMgtCode.frmMilkTransferIn, dt.Rows(0)("location_code"), dt.Rows(0)("Receipt_Challan_Date"), trans)

            End If

            Dim isDeleted As Boolean = True
            '            Dim qry As String = "delete from TSPL_Milk_Transfer_In_Paper_Seal_Details where  chalan_No='" & strReceiptChallanNo & "'"
            '           isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, strReceiptChallanNo, "tspl_milk_transfer_in", "Receipt_Challan_No", trans)
            Dim qry As String = "delete from tspl_milk_transfer_in where  receipt_challan_no='" & strReceiptChallanNo & "'"
            isDeleted = isDeleted AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            Return isDeleted
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
    End Function

    Public Shared Function getData(ByVal strCode As String, ByVal navtype As NavigatorType, Optional ByVal trans As SqlTransaction = Nothing) As clsMilkTransferIn
        Dim whrCls As String = String.Empty
        whrCls = " and tspl_milk_transfer_in.comp_code='" & objCommonVar.CurrentCompanyCode & "'"
        If Not clsMccMaster.isCurrentUserHO(trans) Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls = whrCls & " and tspl_milk_transfer_in.location_code in (" & objCommonVar.strCurrUserLocations & ") "
            End If
        End If
        Dim obj As New clsMilkTransferIn
        Try
            ' Dim obj As New clsMilkTransferIn
            Dim qst As String = " select *   From tspl_milk_transfer_in   where 1=1  "
            qst = qst & whrCls
            Select Case navtype
                Case NavigatorType.Current
                    qst += " and tspl_milk_transfer_in.receipt_challan_no in ('" + strCode + "')  "
                Case NavigatorType.Next
                    qst += " and tspl_milk_transfer_in.receipt_challan_no in (select min(receipt_challan_no ) from tspl_milk_transfer_in where receipt_challan_no  >'" + strCode + "' " & whrCls & " )"
                Case NavigatorType.First
                    qst += " and tspl_milk_transfer_in.receipt_challan_no in (select MIN(receipt_challan_no ) from tspl_milk_transfer_in  where  1=1 " & whrCls & " ) "
                Case NavigatorType.Last
                    qst += " and tspl_milk_transfer_in.receipt_challan_no in (select Max(receipt_challan_no ) from tspl_milk_transfer_in where  1=1 " & whrCls & " )"
                Case NavigatorType.Previous
                    qst += " and tspl_milk_transfer_in.receipt_challan_no in (select Max(receipt_challan_no ) from tspl_milk_transfer_in where receipt_challan_no  <'" + strCode + "'" & whrCls & " ) "
            End Select
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.Document_Amount = clsCommon.myCdbl(dt.Rows(0)("Document_Amount"))
                obj.IsAgainstJobWork = clsCommon.myCdbl(dt.Rows(0)("IsAgainstJobWork"))
                obj.Joblocation_Code = clsCommon.myCstr(dt.Rows(0)("Joblocation_Code"))
                obj.AcknowEntryDocument_No = clsCommon.myCstr(dt.Rows(0)("AcknowEntryDocument_No"))
                obj.PriceCode = clsCommon.myCstr(dt.Rows(0)("PriceCode"))
                obj.FAT_R = clsCommon.myCdbl(dt.Rows(0)("FAT_R"))
                obj.FAT_W = clsCommon.myCdbl(dt.Rows(0)("FAT_W"))
                obj.SNF_R = clsCommon.myCdbl(dt.Rows(0)("SNF_R"))
                obj.SNF_W = clsCommon.myCdbl(dt.Rows(0)("SNF_W"))
                obj.Transfer_Price = clsCommon.myCdbl(dt.Rows(0)("Transfer_Price"))
                obj.Receipt_Challan_No = clsCommon.myCstr(dt.Rows(0)("Receipt_Challan_No"))
                obj.Receipt_Challan_Date = clsCommon.GetPrintDate(dt.Rows(0)("Receipt_Challan_Date"), "dd/MMM/yyyy hh:mm:ss tt")
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
Public Class clsTransferInPaperSealDetail
    Public Chalan_No As String = String.Empty
    Public Seal_No As String = String.Empty
    Public Status As String = String.Empty
    Public Shared Function SaveData(ByVal arr As List(Of clsTransferInPaperSealDetail), ByVal tran As SqlTransaction) As Boolean
        'Try
        '    Dim i As Integer = 0
        '    Dim issaved As Boolean = True
        '    If arr.Count > 0 Then
        '        Dim qry As String = "delete from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
        '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '        For i = 0 To arr.Count - 1
        '            Dim coll As New Hashtable()
        '            clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
        '            clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details", OMInsertOrUpdate.Insert, "", tran)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details_History", OMInsertOrUpdate.Insert, "", tran)
        '        Next
        '    End If
        '    Return issaved
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
        Return SaveData(arr, tran, False)
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsTransferInPaperSealDetail), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_Milk_Transfer_In_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Status", arr.Item(i).Status)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Transfer_In_Paper_Seal_Details", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal chalanNo As String) As List(Of clsTransferInPaperSealDetail)
        Dim arr As New List(Of clsTransferInPaperSealDetail)
        Try
            'Dim arr As New List(Of clsTransferInPaperSealDetail)
            Dim obj As New clsTransferInPaperSealDetail
            Dim q As String = "select * from TSPL_Milk_Transfer_In_Paper_Seal_Details where chalan_no='" & chalanNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsTransferInPaperSealDetail()
                    obj.Chalan_No = clsCommon.myCstr(dtbl.Rows(i)("Chalan_No"))
                    obj.Seal_No = clsCommon.myCstr(dtbl.Rows(i)("Seal_No"))
                    obj.Status = clsCommon.myCstr(dtbl.Rows(i)("Status"))
                    arr.Add(obj)
                Next
            End If
            'Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function

End Class

Public Class clsTransferInManualSealDetail
    Public Chalan_No As String = String.Empty
    Public Seal_No As String = String.Empty
    Public Status As String = String.Empty
    Public Shared Function SaveData(ByVal arr As List(Of clsTransferInManualSealDetail), ByVal tran As SqlTransaction) As Boolean
        'Try
        '    Dim i As Integer = 0
        '    Dim issaved As Boolean = True
        '    If arr.Count > 0 Then
        '        Dim qry As String = "delete from TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
        '        clsDBFuncationality.ExecuteNonQuery(qry, tran)
        '        For i = 0 To arr.Count - 1
        '            Dim coll As New Hashtable()
        '            clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
        '            clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details", OMInsertOrUpdate.Insert, "", tran)
        '            issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Mcc_Dispatch_Challan_Paper_Seal_Details_History", OMInsertOrUpdate.Insert, "", tran)
        '        Next
        '    End If
        '    Return issaved
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        'End Try
        Return SaveData(arr, tran, False)
    End Function
    Public Shared Function SaveData(ByVal arr As List(Of clsTransferInManualSealDetail), ByVal tran As SqlTransaction, ByVal isReversed As Boolean) As Boolean
        Try
            Dim i As Integer = 0
            Dim issaved As Boolean = True
            If arr.Count > 0 Then
                Dim qry As String = "delete from TSPL_Milk_Transfer_In_Manual_Seal_Details where  chalan_No='" & arr.Item(0).Chalan_No & "'"
                clsDBFuncationality.ExecuteNonQuery(qry, tran)
                For i = 0 To arr.Count - 1
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Chalan_No", arr.Item(i).Chalan_No)
                    clsCommon.AddColumnsForChange(coll, "Seal_No", arr.Item(i).Seal_No)
                    clsCommon.AddColumnsForChange(coll, "Status", arr.Item(i).Status)
                    issaved = issaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Milk_Transfer_In_manual_Seal_Details", OMInsertOrUpdate.Insert, "", tran)
                Next
            End If
            Return issaved
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Shared Function getData(ByVal chalanNo As String) As List(Of clsTransferInManualSealDetail)
        Dim arr As New List(Of clsTransferInManualSealDetail)
        Try
            ' Dim arr As New List(Of clsTransferInManualSealDetail)
            Dim obj As New clsTransferInManualSealDetail
            Dim q As String = "select * from TSPL_Milk_Transfer_In_manual_Seal_Details where chalan_no='" & chalanNo & "'"
            Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
            If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                For i As Integer = 0 To dtbl.Rows.Count - 1
                    obj = New clsTransferInManualSealDetail()
                    obj.Chalan_No = clsCommon.myCstr(dtbl.Rows(i)("Chalan_No"))
                    obj.Seal_No = clsCommon.myCstr(dtbl.Rows(i)("Seal_No"))
                    obj.Status = clsCommon.myCstr(dtbl.Rows(i)("Status"))
                    arr.Add(obj)
                Next
            End If
            'Return arr
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return arr
    End Function

End Class