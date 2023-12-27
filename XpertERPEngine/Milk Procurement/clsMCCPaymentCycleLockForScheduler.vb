Imports common
Imports System.Data.SqlClient
Public Class clsMCCPaymentCycleLockForScheduler
#Region "Variables"
    Public Code As String = Nothing
    Public MCC_Code As String = Nothing
    Public From_Date As Date
    Public To_Date As Date
    Public Status As Integer
    Public Bank_Code As String = Nothing
    Public Payment_Mode As String = Nothing
#End Region
    Public Shared Function SaveData(ByVal Arr As List(Of clsMCCPaymentCycleLockForScheduler)) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            For Each obj As clsMCCPaymentCycleLockForScheduler In Arr
                CheckForSchedulerLock(obj.MCC_Code, obj.To_Date, trans)
                obj.Code = clsERPFuncationality.GetNextCode(trans, obj.From_Date, clsDocType.Detail, clsDocTransactionType.Detail, "")
                If (clsCommon.myLen(obj.Code) <= 0) Then
                    Throw New Exception("Error in Document Code Generation")
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Code", obj.Code)
                clsCommon.AddColumnsForChange(coll, "MCC_Code", obj.MCC_Code)
                clsCommon.AddColumnsForChange(coll, "From_Date", clsCommon.GetPrintDate(obj.From_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "To_Date", clsCommon.GetPrintDate(obj.To_Date, "dd/MMM/yyyy"))
                clsCommon.AddColumnsForChange(coll, "Bank_Code", obj.Bank_Code)
                clsCommon.AddColumnsForChange(coll, "Payment_Mode", obj.Payment_Mode)
                clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt"))
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER", OMInsertOrUpdate.Insert, "", trans)
            Next
            trans.Commit()
        Catch err As Exception
            trans.Rollback()
            Throw New Exception(err.Message)
        End Try
        Return True
    End Function

    Public Shared Function CheckForSchedulerLock(ByVal strMCC As String, ByVal DateToCheck As DateTime, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select max(To_Date) as To_Date from TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER where MCC_Code='" + strMCC + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt.Rows(0)(0) Is DBNull.Value Then
            Return True
        End If
        If clsCommon.GetDateWithStartTime(DateToCheck) <= clsCommon.GetDateWithStartTime(clsCommon.myCDate(dt.Rows(0)(0))) Then
            Throw New Exception("MCC [" + strMCC + "] Scheduler Locked up to [" + clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)(0)), "dd/MM/yyyy") + "]")
        End If
        Return True
    End Function

    Public Shared Function DeleteData(ByVal strCode As String) As Boolean
        Dim qry As String = "Delete From TSPL_MCC_PAYMENT_CYCLE_LOCK_FOR_SCHEDULER where Code='" + strCode + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        Return True
    End Function
End Class

Public Class clsVSPBillAndIncentiveCalculation
    Public Function BillGenerationMCCWise(ByVal frmBillGen As RadForm, ByVal strMCCCode As String, ByVal txtFromDate As DateTime, ByVal txtToDate As DateTime, ByVal Formcode As String, ByVal arrVSP As ArrayList, ByVal PaymentType As String, ByVal PaymentValue As Integer) As Boolean
        UpdateSTDQtyOFSRN(txtFromDate, strMCCCode)


        Dim isStopVSPBillIfSomethingWrong As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.StopVSPBillIfSomethingWrong, clsFixedParameterCode.StopVSPBillIfSomethingWrong, Nothing)) = 1)

        Dim strMCCName As String = clsMccMaster.GetName(strMCCCode, Nothing)
        If clsCommon.myLen(strMCCCode) <= 0 Then
            Throw New Exception("Please Select MCC To Generate Bill")
        End If
        If arrVSP Is Nothing OrElse arrVSP.Count <= 0 Then
            Throw New Exception("Please Select VSP To Generate Bill")
        End If
        CreateMCCChillingProvision(strMCCCode, strMCCName, txtFromDate, txtToDate)
        Dim qry As String = "update TSPL_DCS_ADDITION_DEDUCTION set TSPL_DCS_ADDITION_DEDUCTION.Mapping_Matching=xx.MappingCode from (
select Code,MappingCode from TSPL_DCS_ADDITION_DEDUCTION where len(isnull( MappingCode,''))>0 and TSPL_DCS_ADDITION_DEDUCTION.Posted=1
union all
select MappingCode as Code,MappingCode from TSPL_DCS_ADDITION_DEDUCTION where len(isnull( MappingCode,''))>0 and TSPL_DCS_ADDITION_DEDUCTION.Posted=1
)xx inner join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=xx.Code"
        clsDBFuncationality.ExecuteNonQuery(qry)

        If clsCommon.CompairString(Formcode, clsUserMgtCode.MPBillGeneration) = CompairStringResult.Equal Then
            qry = "SELECT LOCK_CODE FROM TSPL_LOCK_MP_PC WHERE MCC_Code='" & strMCCCode & "' AND Posted='Y' AND FROM_DATE='" & clsCommon.GetPrintDate(txtFromDate, "dd-MMM-yyyy") & "' AND TO_DATE='" & clsCommon.GetPrintDate(txtToDate, "dd-MMM-yyyy") & "'"
            Dim Lock_Code As String = clsDBFuncationality.getSingleValue(QRY)
            If clsCommon.myLen(Lock_Code) <= 0 Then
                Throw New Exception("Selected MCC is not locked for selected From and To date. Please lock it first.")
            End If
        End If
        If Formcode = clsUserMgtCode.MilkVSPIssuePayment Then
            Generate_Vsp_Issue_Debit_Note(strMCCCode, arrVSP, "D", txtToDate)
            Generate_Vsp_Issue_Debit_Note(strMCCCode, arrVSP, "C", txtToDate)
        Else
            If isStopVSPBillIfSomethingWrong Then
                qry = "select DOC_CODE,FAT_PER,SNF_PER,VLC_Code_VLC_Uploader as VLCUploaderCode  from (" + Environment.NewLine +
                " select TSPL_MILK_SRN_DETAIL.DOC_CODE,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER ,TSPL_MILK_SRN_HEAD.VLC_CODE," + Environment.NewLine +
                "(select top 1 Rate" + Environment.NewLine +
                 " from TSPL_FAT_SNF_UPLOADER_MASTER" + Environment.NewLine +
                "inner join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_MCC.Code " + Environment.NewLine +
                " inner join TSPL_FAT_SNF_UPLOADER_VLC on VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE  and  TSPL_FAT_SNF_UPLOADER_MASTER.Code=TSPL_FAT_SNF_UPLOADER_VLC.Code " + Environment.NewLine +
                " where  posted='1' and  fat=TSPL_MILK_SRN_DETAIL.FAT_PER and SNF=TSPL_MILK_SRN_DETAIL.SNF_PER and (date< convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) or (date= convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) and Price_code_shift>=TSPL_MILK_SRN_HEAD.SHIFT)) " + Environment.NewLine +
                " order by date desc ,TSPL_FAT_SNF_UPLOADER_MASTER.code desc) as RATE,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader " + Environment.NewLine +
                " from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " left outer join TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " left outer join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE " + Environment.NewLine +
                " where TSPL_MILK_SRN_DETAIL.AMOUNT <= 0 And Against_Reject_No Is null" + Environment.NewLine +
                " and TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount is null" + Environment.NewLine +
                " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  is null" + Environment.NewLine +
                " and TSPL_MILK_SRN_HEAD.MCC_CODE = '" + strMCCCode + "' " + Environment.NewLine +
                " and TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
                " and TSPL_MILK_SRN_HEAD.VSP_coDE in (" + clsCommon.GetMulcallString(arrVSP) + ")" + Environment.NewLine +
                " )xxx where isnull(RATE,0)>0 "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If frmBillGen Is Nothing Then
                        Throw New Exception("There are some SRN With Wrong Price")
                    Else
                        If clsCommon.MyMessageBoxShow("There are some SRN With Wrong Price.Do You want to export these documents", frmBillGen.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            transportSql.ExporttoExcelWithoutFilter(qry, "", "", frmBillGen)
                        End If
                        Return False
                    End If
                End If
                qry = "select TSPL_MILK_REJECT_HEAD.DOC_CODE,TSPL_MILK_REJECT_DETAIL.SAMPLE_NO,TSPL_MILK_REJECT_DETAIL.Defaulter from TSPL_MILK_REJECT_DETAIL " + Environment.NewLine +
                " left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine +
                " where " + Environment.NewLine +
                " TSPL_MILK_REJECT_HEAD.MCC_CODE='" + strMCCCode + "' " + Environment.NewLine +
                " and TSPL_MILK_REJECT_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm tt") + "'  " + Environment.NewLine +
                " and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
                " and TSPL_MILK_REJECT_DETAIL.VSP_CODE in (" + clsCommon.GetMulcallString(arrVSP) + ")" + Environment.NewLine +
                " and TSPL_MILK_REJECT_DETAIL.Amount<=0 "
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    If frmBillGen Is Nothing Then
                        Throw New Exception("There are some Wrong Milk rejection having amount zero")
                    Else
                        If clsCommon.MyMessageBoxShow("There are some Wrong Milk rejection having amount zero.Do You want to export these documents", frmBillGen.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            transportSql.ExporttoExcelWithoutFilter(qry, "", "", frmBillGen)
                        End If
                        Return False
                    End If
                End If
            End If
            Generate_Bill_Payment_cycle_wise(frmBillGen, strMCCCode, txtFromDate, txtToDate, arrVSP, PaymentType, PaymentValue, True, Formcode)
        End If
        Return True
    End Function
    Sub Generate_Bill_Payment_cycle_wise(ByVal frmBillGen As RadForm, ByVal strMCCCode As String, ByVal txtFromDate As DateTime, ByVal txtToDate As DateTime, ByVal txtVSP As ArrayList, ByVal PaymentType As String, ByVal PaymentValue As Integer, ByVal is_with_bill As Boolean, ByVal Formcode As String)
        Dim trans As SqlTransaction = Nothing
        Try
            Dim MultipleFinderFillAuto As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)
            Dim IsRoundOffPaiseAmount As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.RoundOffPaiseAmount, clsFixedParameterCode.RoundOffPaiseAmount, Nothing)) = 1)
            Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PickPendingMilkSRNinNextPaymentCycle, clsFixedParameterCode.PickPendingMilkSRNinNextPaymentCycle, Nothing)) = 1)
            Dim settDoNotIncludeIncentiveInMilkPurchaseInvoice As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DoNotIncludeIncentiveInMilkPurchaseInvoice, clsFixedParameterCode.DoNotIncludeIncentiveInMilkPurchaseInvoice, Nothing)) = 1)
            Dim qry As String = "select Non_Company_VSP_Deduction,Company_VSP_Deduction,MCC_Name from tspl_mcc_master where mcc_Code='" + strMCCCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim CompanyVSPDeduction As Decimal = 0
            Dim NonCompanyVSPDeduction As Decimal = 0
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                CompanyVSPDeduction = clsCommon.myCdbl(dt.Rows(0)("Company_VSP_Deduction"))
                NonCompanyVSPDeduction = clsCommon.myCdbl(dt.Rows(0)("Non_Company_VSP_Deduction"))
            Else
                CompanyVSPDeduction = 0
                NonCompanyVSPDeduction = 0
            End If
            If frmBillGen IsNot Nothing Then
                clsCommon.ProgressBarPercentShow()
            End If
            If clsCommon.myLen(strMCCCode) <= 0 Then
                Throw New Exception("Please Select MCC To Generate Bill")
            End If
            If txtVSP Is Nothing OrElse txtVSP.Count <= 0 Then
                Throw New Exception("Please Select VSP To Generate Bill")
            End If
            '' get MPPayment setting
            Dim IsMP As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isFarmerPaymentCycle, clsFixedParameterCode.isFarmerPaymentCycle, trans))
            If IsMP = 1 Then
                Dim arrVSP As ArrayList = clsMilkPurchaseInvoiceMCC.GetVSPWithoutMPData(txtFromDate, txtToDate, strMCCCode, txtVSP, trans)
                If Not arrVSP Is Nothing AndAlso arrVSP.Count > 0 Then
                    Dim strVSP As String = clsCommon.GetMulcallStringWithComma(arrVSP)
                    Throw New Exception("Some of the VSP dont have MP Collection Data. VSP without MP Data: " & strVSP)
                    'If clsCommon.MyMessageBoxShow("Some of the VSP dont have MP Collection Data. Still do you want to proceed Bill Generation ?", "Validate MP Data", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                    '    Throw New Exception("VSP without MP Data: " & strVSP)
                    'End If
                End If
            End If
            Dim Payment_Cycle_value As Integer = 0
            Dim Srn_No_List As New List(Of String)

            If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
                objCommonVar.SelectedUser = "All"
                objCommonVar.SelectedUser = clsCommon.GetMulcallString(txtVSP)
            Else
                objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
            End If

            Dim counter As Integer = 0
            If clsCommon.CompairString(PaymentType, "Week") = CompairStringResult.Equal Then
                If Not txtFromDate.DayOfWeek = IIf(PaymentValue = 1, DayOfWeek.Sunday, IIf(PaymentValue = 2, DayOfWeek.Monday, IIf(PaymentValue = 3, DayOfWeek.Tuesday, IIf(PaymentValue = 4, DayOfWeek.Wednesday, IIf(PaymentValue = 5, DayOfWeek.Thursday, IIf(PaymentValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday)))))) Then
                    Throw New Exception("From Date Day of week should be " + IIf(PaymentValue = 1, "Sunday", IIf(PaymentValue = 2, "Monday", IIf(PaymentValue = 3, "Tuesday", IIf(PaymentValue = 4, "Wednesday", IIf(PaymentValue = 5, "Thursday", IIf(PaymentValue = 6, "Friday", "Saturday")))))))
                End If
            Else
                qry = "select case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtToDate.Year, txtToDate.Month) & " end " _
            & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & strMCCCode & "'"
                Payment_Cycle_value = clsDBFuncationality.getSingleValue(qry, trans)
                If Payment_Cycle_value <= 0 Then
                    Throw New Exception("Please Set Payment Cycle on Mcc [" & strMCCCode & "]")
                End If
            End If

            GeneratePONumber(strMCCCode, txtFromDate, txtToDate, trans)
            If txtToDate.Date > clsCommon.GETSERVERDATE(trans).Date Then
                Throw New Exception("To Date is Greate than Server Date")
            End If

            Dim arrMCC As New ArrayList
            arrMCC.Add(strMCCCode)
            If (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, trans)) = 1) Then
                txtVSP = Nothing
            End If
            Dim dtVSP As DataTable = clsDBFuncationality.GetDataTable(VSPQry(arrMCC, txtVSP, txtFromDate, txtToDate, Formcode, isPickPendingMilkSRNinNextPaymentCycle))
            If dtVSP IsNot Nothing AndAlso dtVSP.Rows.Count > 0 Then
                'For Each VSP As String In txtVSP
                For Each drVSP As DataRow In dtVSP.Rows
                    Dim VSP As String = clsCommon.myCstr(drVSP("Code"))
                    counter += 1
                    trans = clsDBFuncationality.GetTransactin()
                    Try
                        Srn_No_List.Clear()
                        'aaaaaaaaaaaaaaaaaa()
                        qry = "select Distinct TSPL_MILK_SRN_Head. DOC_CODE from TSPL_MILK_SRN_Head inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_Head.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
                               & " left join TSPL_MILK_PURCHASE_INVOICE_DETAIL on TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " _
                               & " and TSPL_MILK_PURCHASE_INVOICE_DETAIL.item_code=TSPL_MILK_srn_DETAIL.Item_Code where  Coalesce(Is_Incentive_Created,'N')='N' and " _
                               & " VSP_coDE='" & VSP & "' "
                        If Not MultipleFinderFillAuto Then
                            qry += " And TSPL_MILK_SRN_Head.mcc_coDE='" & strMCCCode & "' "
                        End If

                        If isPickPendingMilkSRNinNextPaymentCycle Then
                            ''Comment by balwinder on 24/Jan/2017 at  gajraula becuase pick Zero amount SRN in Milk Purchase Invoice
                            'qry += " and TSPL_MILK_SRN_DETAIL.AMOUNT>0 " 
                        Else
                            qry += " AND convert(date,DOC_DATE,103) >=convert(date,'" & txtFromDate.Date & "',103) "
                        End If
                        qry += " and convert(date,DOC_DATE,103) <=convert(date,'" & txtToDate.Date & "',103)"
                        If is_with_bill Then
                            qry += "and SRN_CODE is null "
                        Else
                            qry += "and SRN_CODE is Not null "
                        End If

                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            Dim strNo As String = ""
                            For Each row As DataRow In dt.Rows()
                                Srn_No_List.Add(row("Doc_Code"))
                                strNo += clsCommon.myCstr(row("Doc_Code")) + ","
                            Next
                            If frmBillGen IsNot Nothing Then
                                clsCommon.ProgressBarPercentUpdate(((counter - 1) * 100 / dtVSP.Rows.Count), "MCC [" + strMCCCode + "] VSP [" & counter & "/" & dtVSP.Rows.Count & "] VSP [" + VSP + "]")
                            End If
                            Dim strr As String = clsDBFuncationality.getSingleValue("select coalesce(vsp_farmer_billing,0) FROM TSPL_Vendor_master where vendor_Code='" & VSP & "'", trans)
                            If(Formcode = clsUserMgtCode.MilkVSPPayment Or Formcode = clsUserMgtCode.MPBillGeneration Or strr <> "1") Then
                                SelectMilkSRNItemsForVspPayment(strMCCCode, Srn_No_List, VSP, txtFromDate.Date, txtToDate.Date, is_with_bill, trans, Formcode, IsRoundOffPaiseAmount, CompanyVSPDeduction, NonCompanyVSPDeduction, settDoNotIncludeIncentiveInMilkPurchaseInvoice)
                            Else
                                SelectMilkSRNItemsForMPPayment(Srn_No_List, VSP, txtFromDate.Date, txtToDate.Date, is_with_bill, trans, Formcode)
                            End If
                            qry = "update tspl_Milk_srn_Head set is_Incentive_Created='Y' where Doc_Code  in (" + clsCommon.GetMulcallString(Srn_No_List) + ")"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            CreateAssetEMIOFVSP(strMCCCode, VSP, trans, txtToDate)
                            CreateVSPDebitNoteOfSecurityDeduction(VSP, txtToDate, strMCCCode, trans)
                            CreateDebitNoteForAdvanceInterestAmt(strMCCCode, VSP, trans, txtToDate) ''by balwinder on 27/04/2017
                            CreateVSPDebitNoteOfTIP(VSP, txtToDate, strMCCCode, trans) ''By Balwinder on 16/12/2019
                        End If

                        qry = " select DOC_CODE from ( select TSPL_MILK_REJECT_DETAIL.DOC_CODE  from TSPL_MILK_REJECT_DETAIL" + Environment.NewLine +
                        " left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine +
                        " where " + Environment.NewLine +
                        " TSPL_MILK_REJECT_HEAD.POSTED=1 and TSPL_MILK_REJECT_DETAIL.Defaulter in ('Transporter','VSP') and TSPL_MILK_REJECT_HEAD.MCC_CODE ='" + strMCCCode + "' and TSPL_MILK_REJECT_DETAIL.VSP_CODE in ('" + VSP + "') and  " + Environment.NewLine +
                        " TSPL_MILK_REJECT_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm tt") + "' and not exists (select 1 from TSPL_VENDOR_INVOICE_HEAD where TSPL_VENDOR_INVOICE_HEAD.RefDocType='MILK-REJ' and TSPL_VENDOR_INVOICE_HEAD.RefDocNo=TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine   ''and TSPL_VENDOR_INVOICE_HEAD.Ref_SNo=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO
                        qry += "))xx group by DOC_CODE"
                        dt = clsDBFuncationality.GetDataTable(qry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                clsMilkRejectHead.CreateDebitNoteForRejection(txtFromDate, txtToDate, clsCommon.myCstr(dr("DOC_CODE")), trans)
                            Next
                        End If
                        'Throw New Exception("Balwinder singh premi")
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        Throw New Exception(ex.Message)
                    End Try
                Next
            End If
            counter = 0
            If frmBillGen IsNot Nothing Then
                clsCommon.ProgressBarPercentHide()
            End If
        Catch ex As Exception
            If frmBillGen IsNot Nothing Then
                clsCommon.ProgressBarPercentHide()
            End If
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Shared Sub CreateVSPDebitNoteOfTIP(ByVal strVSPCode As String, ByVal dtDocDate As DateTime, ByVal strMCC As String, ByVal trans As SqlTransaction) ''VIJ/11/12/19-000118 by balwinder on 27/01/2019
        Dim qry As String = "select max(TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) as DOC_CODE, sum(TSPL_MILK_SRN_DETAIL.TIP_Amount) as TIP_Amount from TSPL_MILK_PURCHASE_INVOICE_DETAIL" + Environment.NewLine +
        "left outer join TSPL_MILK_PURCHASE_INVOICE_HEAD  on TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE" + Environment.NewLine +
        "left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE" + Environment.NewLine +
        "where TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE='" + strVSPCode + "' and TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE='" + strMCC + "' and convert(date, TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)='" + clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy") + "'"
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
            Dim dblAmount As Decimal = clsCommon.myCdbl(dt1.Rows(0)("TIP_Amount"))
            If dblAmount > 0 Then
                Dim strVendor As String = strVSPCode
                Dim dt As DataTable
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                objVendorInvHead.isDeduction = 1
                objVendorInvHead.Security = 1


                'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
                objVendorInvHead.Vendor_Code = strVendor
                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVendor, trans)
                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = dtDocDate
                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCC, trans) 'obj.MCC_CODE
                'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
                objVendorInvHead.Description = "AP Debit Note Against TIP Deduction"
                'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                End If

                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                ''objVendorInvHead.PO_Number = obj.p

                '' ''added by priti
                objVendorInvHead.RefDocType = "TIP-DED"
                objVendorInvHead.RefDocNo = clsCommon.myCstr(dt1.Rows(0)("DOC_CODE"))
                'objVendorInvHead.Ref_SNo = ""
                '' '' priti ends here
                'objVendorInvHead.Order_No = txtOrderNo.Text
                ' objVendorInvHead.Total_Tax = 0

                objVendorInvHead.On_Hold = False
                'Dim srndate As String = ""
                'Dim srncode As String = ""
                'Dim Vlc_Code As String = ""
                'Dim Vlc_Name As String = ""
                'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
                '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
                '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
                '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
                '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
                '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
                '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
                '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
                '    End If
                'Next



                'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
                'objVendorInvHead.Tax_Calculation_Type = Nothing
                'objVendorInvHead.Tax_Group = Nothing
                'If (clsCommon.myLen(obj.TAX1) > 0) Then
                '    objVendorInvHead.TAX1 = obj.TAX1
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
                '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
                '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
                '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
                '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX2) > 0) Then
                '    objVendorInvHead.TAX2 = obj.TAX2
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
                '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
                '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
                '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
                '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX3) > 0) Then
                '    objVendorInvHead.TAX3 = obj.TAX3
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
                '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
                '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
                '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
                '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX4) > 0) Then
                '    objVendorInvHead.TAX4 = obj.TAX4
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
                '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
                '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
                '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
                '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX5) > 0) Then
                '    objVendorInvHead.TAX5 = obj.TAX5
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
                '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
                '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

                '    End If
                '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
                '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
                '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX6) > 0) Then
                '    objVendorInvHead.TAX6 = obj.TAX6
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
                '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
                '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
                '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
                '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX7) > 0) Then
                '    objVendorInvHead.TAX7 = obj.TAX7
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
                '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
                '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

                '    End If
                '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
                '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
                '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX8) > 0) Then
                '    objVendorInvHead.TAX8 = obj.TAX8
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
                '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
                '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
                '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
                '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX9) > 0) Then
                '    objVendorInvHead.TAX9 = obj.TAX9
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
                '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
                '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
                '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
                '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
                'End If
                'If (clsCommon.myLen(obj.TAX10) > 0) Then
                '    objVendorInvHead.TAX10 = obj.TAX10
                '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
                '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
                '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
                '    End If
                '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
                '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
                '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
                'End If

                'objVendorInvHead.Terms_Code = obj.Terms_Code
                'objVendorInvHead.Terms_Description = obj.TermsName
                objVendorInvHead.Due_Date = dtDocDate

                'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
                'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCC, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCC, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If

                'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

                'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
                'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
                'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

                'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
                'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
                'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

                'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
                'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
                'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

                'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
                'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
                'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

                'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
                'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
                'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

                'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
                'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
                'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

                'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
                'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
                'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

                'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
                'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
                'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

                'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
                'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
                'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

                'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
                'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
                'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True
                ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
                'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
                objVendorInvHead.Total_Landed_Amt = 0

                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()





                ''Set AP Invvoice Detail Table
                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select Code,Description,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_TIP=1", trans)
                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    Throw New Exception("Please set default TIP deduction code")
                End If
                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                objVendorInvDetail.DeductionDesc = clsCommon.myCstr(dtDed.Rows(0)("Description"))

                Dim strInvCtrlAC As String = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCC, trans)
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set GL Account Code for deduction code " + objVendorInvDetail.Deduction_Code)
                End If
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
                objVendorInvDetail.Amount = dblAmount

                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = dblAmount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = dblAmount
                objVendorInvDetail.Landed_Amount = dblAmount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += dblAmount
                objVendorInvHead.Discount_Base += dblAmount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += dblAmount
                objVendorInvHead.Document_Total += dblAmount
                objVendorInvHead.Balance_Amt += dblAmount
                ''End of Set AP Invvoice Header Table

                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If
                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                ''multicurrency
                'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
                'objVendorInvHead.ConvRate = 1
                objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
                ''end multicurrency

                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, dtDocDate)
            End If
        End If


    End Sub
    Private Sub CreateDebitNoteForAdvanceInterestAmt(ByVal strMCCCode As String, ByVal strVSPCode As String, ByVal trans As SqlTransaction, ByVal txtToDate As DateTime)
        Dim qry As String = clsAPInvoiceAdvanceInterest.GetAdvancePaymentQry("'" + strVSPCode + "'", Nothing, txtToDate, False, trans)
        Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dtMain IsNot Nothing AndAlso dtMain.Rows.Count > 0 Then
            qry = "select PC_VALUE from TSPL_MCC_MASTER  left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle where MCC_Code='" + strMCCCode + "'"
            Dim dblPCDays As Integer = clsDBFuncationality.getSingleValue(qry, trans)


            Dim objVendorInvHead As New clsVedorInvoiceHead()
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(txtToDate, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVSPCode
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVSPCode, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = txtToDate
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCCCode, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against Installment Amount of VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p
            objVendorInvHead.isDeduction = 1

            '' ''added by priti
            ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
            ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = txtToDate

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
            objVendorInvHead.RefDocType = "VSP-INT"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCCCode, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCCCode, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAdvanceInterest = New List(Of clsAPInvoiceAdvanceInterest)()

            For Each dr As DataRow In dtMain.Rows

                ''Set Installment amount
                Dim objAEMI As New clsAPInvoiceAdvanceInterest
                objAEMI.Payment_No = clsCommon.myCstr(dr("Payment_No"))
                objAEMI.Interest_Amount = Math.Round((clsCommon.myCdbl(dr("Payment_Amount")) * clsCommon.myCdbl(dr("Interest_Rate")) * dblPCDays) / (365 * 100), 2, MidpointRounding.ToEven)  ''BHA/20/06/18-000059 By balwinder change 360 days to 365 
                objVendorInvHead.ArrAdvanceInterest.Add(objAEMI)
                ''end of Set Installment amount

                ''Set AP Invvoice Detail Table




                Dim objVendorInvDetail As New clsVedorInvoiceDetail()

                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select Code,Description,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_Advance_Interest=1", trans)
                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    Throw New Exception("Please set default Advance Interest in deduction Master")
                End If
                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                objVendorInvDetail.DeductionDesc = clsCommon.myCstr(dtDed.Rows(0)("Description"))

                Dim strInvCtrlAC As String = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCCCode, trans)
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set GL Account Code for deduction code " + objVendorInvDetail.Deduction_Code)
                End If


                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
                objVendorInvDetail.Amount = objAEMI.Interest_Amount
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = objVendorInvDetail.Amount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = objVendorInvDetail.Amount
                objVendorInvDetail.Landed_Amount = objVendorInvDetail.Amount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount
                objVendorInvHead.Document_Total += objVendorInvDetail.Amount
                objVendorInvHead.Balance_Amt += objVendorInvDetail.Amount
                ''End of Set AP Invvoice Header Table
            Next


            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If


            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(txtToDate, "dd/MMM/yyyy")
            ''end multicurrency
            '' skip entry of installment if amount i zero
            If objVendorInvHead.Document_Total > 0 Then
                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, txtToDate)
            End If
            objVendorInvHead = Nothing
        End If
    End Sub
    Public Shared Sub CreateVSPDebitNoteOfSecurityDeduction(ByVal strVSPCode As String, ByVal dtDocDate As DateTime, ByVal strMCC As String, ByVal trans As SqlTransaction)
        Dim dblAmount As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Security_Deduction_Amount from TSPL_VENDOR_MASTER where Vendor_Code='" + strVSPCode + "'", trans))
        If dblAmount > 0 Then
            Dim strVendor As String = strVSPCode
            Dim dt As DataTable
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvHead.isDeduction = 1
            objVendorInvHead.Security = 1
            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_Security_Deduction=1", trans)
            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                Throw New Exception("Please set default Security deduction code")
            End If
            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))

            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVendor
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVendor, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = dtDocDate
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCC, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against VSP security Deduction"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p

            '' ''added by priti
            objVendorInvHead.RefDocType = "SEC-DED"
            'objVendorInvHead.RefDocNo = ""
            'objVendorInvHead.Ref_SNo = ""
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = dtDocDate

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCC, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCC, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()



            ''Set AP Invvoice Detail Table

            Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set Deduction Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")))
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCC, trans)




            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmount

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmount
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmount
            objVendorInvDetail.Landed_Amount = dblAmount
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmount
            objVendorInvHead.Discount_Base += dblAmount
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmount
            objVendorInvHead.Document_Total += dblAmount
            objVendorInvHead.Balance_Amt += dblAmount
            ''End of Set AP Invvoice Header Table

            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If
            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, dtDocDate)
        End If
    End Sub
    Private Sub CreateAssetEMIOFVSP(ByVal strMCCCode As String, ByVal strVSPCode As String, ByVal trans As SqlTransaction, ByVal txtToDate As DateTime)
        Dim dtMain As DataTable = clsDBFuncationality.GetDataTable(clsAPInvoiceAssetEMIDetails.GetVSPAssetEMIQuery(strVSPCode), trans)
        If dtMain IsNot Nothing AndAlso dtMain.Rows.Count > 0 Then
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(txtToDate, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVSPCode
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVSPCode, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = txtToDate
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCCCode, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against Installment Amount of VSP : " & objVendorInvHead.Vendor_Name & "(" + objVendorInvHead.Vendor_Code + ")"
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p
            objVendorInvHead.isDeduction = 1

            '' ''added by priti
            ''objVendorInvHead.RefDocType = clsCommon.myCstr(cmbRefType.SelectedValue)
            ''objVendorInvHead.RefDocNo = txtRefDocNo.Text
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = txtToDate

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE
            objVendorInvHead.RefDocType = "VSP-AIE"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Payable_Account,Discount_Account from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCCCode, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCCCode, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

            For Each dr As DataRow In dtMain.Rows
                ''Set Installment amount
                Dim objAEMI As New clsAPInvoiceAssetEMIDetails
                objAEMI.Asset_Issue_No = clsCommon.myCstr(dr("Doc_No"))
                objAEMI.Item_Code = clsCommon.myCstr(dr("Item_Code"))
                objAEMI.Installment_Amount = clsCommon.myCstr(dr("Installment_Amt"))
                objVendorInvHead.ArrAssetEMI.Add(objAEMI)
                ''end of Set Installment amount

                ''Set AP Invvoice Detail Table
                Dim qry As String = "select TSPL_ITEM_MASTER.Purchase_Class_Code,TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing,TSPL_GL_ACCOUNTS.Description as ClearingACName, TSPL_ITEM_MASTER.Two_Count_Status as isEmpty,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as EmptyAccount from TSPL_ITEM_MASTER left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing where TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(dr("Item_Code")) + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set Purchase Account set for item " + clsCommon.myCstr(dr("Item_Code")))
                End If
                Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Inv_Control_Account"))
                strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCCCode, trans)
                If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                    Throw New Exception("Please set Inventory Control Account for Purchase Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Purchase_Class_Code")) + " and Item: " + clsCommon.myCstr(dr("Item_Code")))
                End If


                Dim objVendorInvDetail As New clsVedorInvoiceDetail()

                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_Asset_Installment=1", trans)
                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                    Throw New Exception("Please set default Asset Installment deduction code")
                End If
                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))


                ii = ii + 1
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.GL_Account_Code = strInvCtrlAC
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
                objVendorInvDetail.Amount = clsCommon.myCdbl(dr("Installment_Amt"))
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = objVendorInvDetail.Amount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = objVendorInvDetail.Amount
                objVendorInvDetail.Landed_Amount = objVendorInvDetail.Amount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += objVendorInvDetail.Landed_Amount
                objVendorInvHead.Discount_Base += objVendorInvDetail.Amount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += objVendorInvDetail.Amount
                objVendorInvHead.Document_Total += objVendorInvDetail.Amount
                objVendorInvHead.Balance_Amt += objVendorInvDetail.Amount
                ''End of Set AP Invvoice Header Table
            Next


            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If


            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(txtToDate, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, txtToDate)


        End If
    End Sub
    Public Sub SelectMilkSRNItemsForVspPayment(ByVal strMCCCode As String, ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Is_With_Bill As Boolean, ByVal trans As SqlTransaction, ByVal Formcode As String, ByVal IsRoundOffPaiseAmount As Boolean, ByVal CompanyVSPDeduction As Decimal, ByVal NonCompanyVSPDeduction As Decimal, ByVal settDoNotIncludeIncentiveInMilkPurchaseInvoice As Boolean)
        Dim obj_SRN As New clsMilkSRNMCC
        Dim frm As New UcMilkPendingSRN()
        frm.VendorCode = Vsp_Name
        frm.Frm_date = frm_date
        frm.To_date = End_date
        frm.isForMP = False
        Dim StrDoc As New List(Of String)
        If Is_With_Bill Then
            If Not frm.LoaDHeadDataQuery(trans) Then
                Exit Sub
            End If
        Else
            If Not frm.LoaDHeadDataQueryVsp(trans) Then
                Exit Sub
            End If
        End If
        For Each row As GridViewRowInfo In frm.gvHead.Rows()
            If strSRN_No.Contains(clsCommon.myCstr(row.Cells(UcMilkPendingSRN.colHCode).Value)) Then
                frm.gvHead.CurrentRow = row
                row.Cells(UcMilkPendingSRN.colHSelect).Value = True
            End If
        Next
        frm.btnOKPressed()
        If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
            If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
                obj_SRN = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
                If obj_SRN IsNot Nothing AndAlso clsCommon.myLen(obj_SRN.DOC_CODE) > 0 Then
                    Dim TotOwnAsset As Double = 0
                    Dim TotDeduction_Amount As Double = 0
                    Dim objHead As New clsMilkPurchaseInvoiceMCC
                    objHead.Program_Code = Formcode
                    objHead.FROM_DATE = frm_date
                    objHead.TO_DATE = End_date
                    objHead.DOC_CODE = ""
                    objHead.DOC_DATE = clsCommon.myCDate(End_date)
                    objHead.Description = ""
                    objHead.ROUTE_CODE = clsCommon.myCstr(obj_SRN.ROUTE_CODE)
                    objHead.VSP_CODE = clsCommon.myCstr(obj_SRN.VSP_CODE)
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select vsp_Payment,Handling_Charges_Per from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj_SRN.VSP_CODE & "'", trans)
                    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                        Throw New Exception("VSP- " + obj_SRN.VSP_CODE + "Not exists in vsp master")
                    End If
                    objHead.Payment = clsCommon.myCstr(dt.Rows(0)("vsp_Payment"))
                    objHead.Handling_Charges_Per = clsCommon.myCdbl(dt.Rows(0)("Handling_Charges_Per"))
                    objHead.Handling_Charges_Amount = 0
                    objHead.SRN_Net_Amount = 0
                    objHead.SRN_RO_Amount = 0
                    objHead.Irregular_MCC_CODE = clsCommon.myCstr(obj_SRN.MCC_CODE)

                    Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)
                    Dim objDetail As clsMilkPurchaseInvoiceMCCDetail = Nothing
                    Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj_SRN.MCC_CODE) & "' " _
                    & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj_SRN.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj_SRN.SHIFT) = "M", "Morning", "Evening") & "'"

                    Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                    Dim totAmount As Double = 0
                    Dim totCommssion As Double = 0
                    objHead.Total_PaymentCommission = 0
                    Dim totAmountwithPaymentCommssion As Double = 0
                    Dim totAmountIncentive As Double = 0
                    Dim totAmountIncentiveEMP As Double = 0
                    Dim totBasicAmount As Double = 0
                    objHead.Total_Head_Load_Amount = 0
                    For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
                        objDetail = New clsMilkPurchaseInvoiceMCCDetail
                        objDetail.DOC_CODE = ""
                        objDetail.AMOUNT = clsCommon.myCdbl(obj1.AMOUNT)
                        objDetail.Cans = clsCommon.myCdbl(obj1.Cans)
                        objDetail.CLR = clsCommon.myCdbl(obj1.CLR)
                        objDetail.COMMISSION = clsCommon.myCdbl(obj1.Commission)
                        objDetail.Payment_COMMISSION = clsCommon.myCdbl(obj1.Payment_Commission)
                        If DtShiftEnd.Rows.Count > 0 Then
                            Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(obj1.VlC_Code) & "' and srn_code='" & clsCommon.myCstr(obj1.DOC_CODE) & "'")
                            If dr.Length > 0 Then
                                objDetail.Deduction = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
                            End If
                        End If
                        objDetail.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
                        objDetail.Correction_Factor = clsCommon.myCdbl(obj1.Correction_Factor)
                        objDetail.FAT_PER = clsCommon.myCdbl(obj1.FAT)
                        objDetail.Item_Code = clsCommon.myCstr(obj1.Item_CODE)
                        objDetail.MCC_CODE = strMCCCode
                        objDetail.Qty = clsCommon.myCdbl(obj1.MILK_Qty)
                        objDetail.Acc_Qty = clsCommon.myCdbl(obj1.ACC_Qty)
                        objDetail.Service_Charge = clsCommon.myCstr(obj1.Service_Charge_Type)
                        objDetail.RATE = clsCommon.myCdbl(obj1.RATE)
                        objDetail.SNF_PER = clsCommon.myCdbl(obj1.SNF)
                        objDetail.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
                        Dim Commission_AMount As Double = 0
                        objDetail.Service_Charge_Amount = Math.Round(obj1.Service_Charge_Amount, 2)
                        ''Extra column of SRN
                        dt = clsDBFuncationality.GetDataTable("select NET_AMOUNT,Round_Off from TSPL_MILK_SRN_DETAIL where DOC_CODE='" + obj1.DOC_CODE + "'", trans)
                        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                            Throw New Exception("Milk SRN No " + obj1.DOC_CODE + " not found")
                        End If

                        obj1.NET_AMOUNT = Math.Round(clsCommon.myCdbl(dt.Rows(0)("NET_AMOUNT")), 2)
                        obj1.Round_Off = Math.Round(clsCommon.myCdbl(dt.Rows(0)("Round_Off")), 2)

                        objDetail.SRN_Net_Amount = obj1.NET_AMOUNT
                        objDetail.SRN_RO_Amount = obj1.Round_Off

                        ''End of Extra column of SRN
                        objDetail.Net_AMOUNT = Math.Round(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select NET_AMOUNT from TSPL_MILK_SRN_DETAIL where DOC_CODE='" + obj1.DOC_CODE + "'", trans)), 2) ''Not coming in object and only one row is exists 
                        objDetail.Handling_Charges_Amount = Math.Round((objDetail.Net_AMOUNT) * objHead.Handling_Charges_Per / 100, 2)
                        objDetail.Net_AMOUNT += objDetail.Handling_Charges_Amount

                        objDetail.TOTAL_AMOUNT = Math.Round(objDetail.Net_AMOUNT + objDetail.Service_Charge_Amount, 2)
                        objDetail.SRN_CODE = clsCommon.myCstr(obj1.DOC_CODE)
                        objDetail.VEHICLE_NO = clsCommon.myCstr(obj_SRN.VEHICLE_CODE)
                        objDetail.VLC_NO = clsCommon.myCstr(obj1.VlC_Code)
                        objHead.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(objDetail.SRN_CODE, trans)
                        objDetail.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(objDetail.SRN_CODE, trans)
                        If clsCommon.CompairString(objHead.MCC_CODE, objHead.Irregular_MCC_CODE) = CompairStringResult.Equal Then
                            objHead.Irregular_MCC_CODE = ""
                        End If

                        objList.Add(objDetail)
                        objHead.Total_Head_Load_Amount += objDetail.Head_Load_Amount
                        TotOwnAsset += objDetail.Own_Asset_Amount
                        TotDeduction_Amount += (objDetail.Deduction)

                        totAmount += objDetail.AMOUNT
                        totBasicAmount += objDetail.AMOUNT
                        totCommssion += 0
                        Dim TotPaycomm As Decimal = Math.Round((objDetail.TOTAL_AMOUNT - objDetail.AMOUNT - objDetail.Handling_Charges_Amount + obj1.Round_Off), 2, MidpointRounding.ToEven)
                        If Math.Abs(TotPaycomm) > 0.1 Then
                            objHead.Total_PaymentCommission += TotPaycomm
                        End If
                        totAmountwithPaymentCommssion += objDetail.Net_AMOUNT
                        objHead.Handling_Charges_Amount += objDetail.Handling_Charges_Amount
                        objHead.SRN_Net_Amount += objDetail.SRN_Net_Amount
                        objHead.SRN_RO_Amount += objDetail.SRN_RO_Amount
                    Next
                    objHead.Handling_Charges_Amount = Math.Round(objHead.Handling_Charges_Amount, 2, MidpointRounding.ToEven)
                    objHead.Total_Head_Load_Amount = Math.Round(objHead.Total_Head_Load_Amount, 2, MidpointRounding.ToEven)
                    If IsRoundOffPaiseAmount Then
                        objHead.Handling_Charges_RO_Amount = (objHead.Handling_Charges_Amount Mod 1)
                        objHead.Handling_Charges_Amount = objHead.Handling_Charges_Amount - objHead.Handling_Charges_RO_Amount

                        objHead.Total_Head_Load_RO_Amount = (objHead.Total_Head_Load_Amount Mod 1)
                        objHead.Total_Head_Load_Amount = objHead.Total_Head_Load_Amount - objHead.Total_Head_Load_RO_Amount
                    ElseIf objCommonVar.DCSAddDedROHeaderLevel Then
                        objHead.Handling_Charges_RO_Amount = objHead.Handling_Charges_Amount - clsCommon.myRoundOFF(objHead.Handling_Charges_Amount, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                        objHead.Handling_Charges_Amount = objHead.Handling_Charges_Amount - objHead.Handling_Charges_RO_Amount

                        objHead.Total_Head_Load_RO_Amount = objHead.Total_Head_Load_Amount - clsCommon.myRoundOFF(objHead.Total_Head_Load_Amount, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                        objHead.Total_Head_Load_Amount = objHead.Total_Head_Load_Amount - objHead.Total_Head_Load_RO_Amount

                        objHead.SRN_RO_Amount = objHead.SRN_Net_Amount - clsCommon.myRoundOFF(objHead.SRN_Net_Amount, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                        objHead.SRN_Net_Amount = objHead.SRN_Net_Amount - objHead.SRN_RO_Amount

                        totAmount = clsCommon.myRoundOFF(totAmount, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                        totCommssion = clsCommon.myRoundOFF(totCommssion, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                        totAmountwithPaymentCommssion = clsCommon.myRoundOFF(totAmountwithPaymentCommssion, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                    Else


                        objHead.Total_Head_Load_RO_Amount = objHead.Total_Head_Load_Amount - clsCommon.myRoundOFF(objHead.Total_Head_Load_Amount, clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.HeadLoadRODecimalPlace, clsFixedParameterCode.HeadLoadRODecimalPlace, trans)), clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.HeadLoadROIncreaseAfter, clsFixedParameterCode.HeadLoadROIncreaseAfter, trans)))
                        objHead.Total_Head_Load_Amount = objHead.Total_Head_Load_Amount - objHead.Total_Head_Load_RO_Amount

                    End If

                    objHead.Total_Own_Asset_Amount = TotOwnAsset
                    objHead.Total_Deduction_Amount = TotDeduction_Amount
                    objHead.VENDOR_INVOICE_NO = ""
                    objHead.VENDOR_INVOICE_DATE = obj_SRN.DOC_DATE
                    objHead.Amount = clsCommon.myCdbl(totAmount)
                    objHead.Basic_Amount = Math.Round(clsCommon.myCdbl(totAmount) - clsCommon.myCdbl(totCommssion), 2)
                    objHead.Commission = clsCommon.myCdbl(totCommssion)
                    objHead.Total_Amount_Acc = clsCommon.myCdbl(totAmountwithPaymentCommssion) - objHead.Handling_Charges_RO_Amount

                    objHead.Program_Code = Formcode
                    objHead.No_Of_Asset = 0
                    If CompanyVSPDeduction > 0 Or NonCompanyVSPDeduction > 0 Then
                        sQuery = "select Issue_To,sum(Issued_Qty*RI) as NoOFAsset from (" + Environment.NewLine +
                                "select TSPL_VSPAsset_HEAD.Doc_No,TSPL_VSPAsset_HEAD.Issue_To,TSPL_VSPAsset_detail.Item_Code,case when Doc_Type='Issue' then Issued_Qty else Issued_Qty_againstret end as Issued_Qty,case when Doc_Type='Issue' then 1 else -1 end as RI from TSPL_VSPAsset_detail" + Environment.NewLine +
                                "left outer join TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No=TSPL_VSPAsset_detail.Doc_No" + Environment.NewLine +
                                "where convert(date ,Doc_Date,103)<='" + clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy") + "' and Issue_To='" + objHead.VSP_CODE + "' and Status=1" + Environment.NewLine +
                                ")x group by Issue_To"
                        Dim dtAsset As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                        If dtAsset IsNot Nothing AndAlso dtAsset.Rows.Count > 0 Then
                            If clsCommon.myCdbl(dtAsset.Rows(0)("NoOFAsset")) > 0 Then
                                objHead.No_Of_Asset = clsCommon.myCdbl(dtAsset.Rows(0)("NoOFAsset"))
                            End If
                        End If
                    End If

                    totAmount = 0
                    If clsMilkPurchaseInvoiceMCC.SaveData(objHead, objList, trans) Then
                        clsMilkPurchaseInvoiceMCC.SaveMPData(objHead.DOC_CODE, objHead.FROM_DATE, objHead.TO_DATE, objHead.MCC_CODE, objHead.VSP_CODE, trans)
                        If Not settDoNotIncludeIncentiveInMilkPurchaseInvoice Then
                            Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
                            clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive_MP(objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
                            Dim totincentiveEMP As Double = 0
                            Dim totincentive As Double = 0
                            totAmount = 0
                            totBasicAmount = 0
                            totAmountwithPaymentCommssion = 0
                            Dim is_processed As Integer = 0
                            Dim is_Emp_On_Amount_Only As String = clsDBFuncationality.getSingleValue("select EmpOnAMountOnly from tspl_Mcc_Master where Mcc_Code='" & objDetail.MCC_CODE & "'", trans)
                            If incentive.Count > 0 Then
                                If incentive(1) > 0 Then
                                    For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
                                        If is_processed = 0 Then
                                            totincentiveEMP = Math.Round(clsCommon.myCdbl(incentive(1)) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
                                            totAmount += objDetail.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                                            totBasicAmount += objDetail.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                                            objDetail.Net_AMOUNT += +IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                                            totAmountwithPaymentCommssion += objDetail.Net_AMOUNT '+ totincentiveEMP '+ incentive(1)
                                            sQuery = "Update tspl_Milk_Purchase_Invoice_Detail set Total_Amount='" & clsCommon.myCdbl(objDetail.AMOUNT) & "',Total_Amount_Acc='" & clsCommon.myCdbl(objDetail.Net_AMOUNT) & "',Net_Amount='" & clsCommon.myCdbl(objDetail.Net_AMOUNT) & "',incentive='" & incentive(1) & "' , incentiveEMP='" & totincentiveEMP & "' where srn_code='" & objDetail.SRN_CODE & "'"
                                            clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                                            is_processed = 1
                                        End If
                                        'Exit For
                                    Next
                                    is_processed = 0
                                    totAmount = objHead.Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                                    totAmountwithPaymentCommssion = objHead.Total_Amount_Acc + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)

                                    sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount='" & clsCommon.myCdbl(totAmount) & "',Total_Amount_Acc='" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & "' ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
                                    clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                                End If
                            End If
                        End If

                        sQuery = "select Total_Amount_Acc from tspl_Milk_Purchase_Invoice_Head where doc_code='" & objHead.DOC_CODE & "'"
                        totAmount = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sQuery, trans))


                        Dim dblPreviousTDSAmt As Decimal = 0
                        Dim objRemittance As clsRemittance = SetVendorTDSDetails(True, objHead.DOC_CODE, objHead.DOC_DATE, dblPreviousTDSAmt, objHead.VSP_CODE, totAmount, totAmount, trans)
                        'UpdateTDSAmount(objRemittance, objHead.DOC_CODE, objHead.DOC_DATE, dblPreviousTDSAmt, objHead.VSP_CODE, totAmount, totAmount, trans)
                        Dim objPIRemittance As clsPIRemittance = clsPIRemittance.Convert(objRemittance, dblPreviousTDSAmt)
                        clsPIRemittance.SaveData(objPIRemittance, objHead.DOC_CODE, objHead.DOC_DATE, trans)

                        clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", objHead.DOC_CODE, trans)
                        'CreateHandlingCharges(objHead, trans) ''GKD/02/05/18-000126.By Balwinder On 03/05/2018 .No Need To make adjustment Entry of Handling Charges 
                        CreateDebitNoteForDeductionMapping(objHead, objList, trans)
                        VSPCommissionAndDeduction(frm_date, End_date, objHead, strSRN_No, trans) ''Bhole Baba
                        VSPCommissionAndPashuVikashKosh(frm_date, End_date, objHead, strSRN_No, trans) ''UCDF Baba
                        CreateVSPDebitNoteOfAsset(objHead, End_date, strMCCCode, trans, NonCompanyVSPDeduction, CompanyVSPDeduction)
                        CreateApplyDocmentOfAssetLost(objHead, End_date, strMCCCode, trans, NonCompanyVSPDeduction, CompanyVSPDeduction)
                    End If
                End If
            End If
        End If
    End Sub
    Public Shared Function SetVendorTDSDetails(ByVal IsNewEntry As Boolean, ByVal DocNo As String, ByVal docDate As DateTime, ByRef dblPreviousTDSAmt As Decimal, ByVal VendorCode As String, ByVal TaxableAmt As Decimal, ByVal TotalAmt As Decimal, ByVal trans As SqlTransaction) As clsRemittance
        Return SetVendorTDSDetails(False, IsNewEntry, DocNo, docDate, dblPreviousTDSAmt, VendorCode, TaxableAmt, TotalAmt, trans)
    End Function
    Public Shared Function SetVendorTDSDetails(ByVal SkipGoveRule As Boolean, ByVal IsNewEntry As Boolean, ByVal DocNo As String, ByVal docDate As DateTime, ByRef dblPreviousTDSAmt As Decimal, ByVal VendorCode As String, ByVal TaxableAmt As Decimal, ByVal TotalAmt As Decimal, ByVal trans As SqlTransaction) As clsRemittance
        Dim objRemittance As clsRemittance = Nothing
        Dim objVendor As clsTDSVendorDetails = clsTDSVendorDetails.GetData(VendorCode, trans)
        If objVendor IsNot Nothing AndAlso (objCommonVar.ApplyGovtRulesInTDS OrElse SkipGoveRule) Then
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + VendorCode + "'", trans)
            Dim appAmt As Double = 0
            If (clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal) Then
                appAmt = clsCommon.myCdbl(TaxableAmt)
            Else
                appAmt = clsCommon.myCdbl(TotalAmt)
            End If
            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objVendor.Nature_Of_Deduction, appAmt, trans, False, VendorCode)
            If (objDedDetails IsNot Nothing) Then
                Dim isApplyTDS As Boolean = False
                Dim qry As String = "select Fiscal_Code,Start_Date,End_Date from TSPL_Fiscal_Year_Master where convert(date,'" + docDate + "',103)>=  convert(date,Start_Date,103)  and convert(date,'" + docDate + "',103)<=convert(date,End_Date,103) "
                Dim dtFY As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtFY Is Nothing OrElse dtFY.Rows.Count <= 0 Then
                    Throw New Exception("Please make fiscal year where document date exists")
                End If

                ''Check if any TDS entry found in Document Fiscal Year
                qry = "select top 1 Remittance_Code from TSPL_REMITTANCE  where Vendor_Code='" + VendorCode + "' and convert(date, Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and Document_No not in ('" + DocNo + "')"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    isApplyTDS = True
                Else
                    qry = "select Cumm_Cutoff,Cumm_Cutoff_Document from TSPL_TDS_DEDUCTION_HEAD where Deduction_Code='" + objVendor.Nature_Of_Deduction + "'"
                    dtTemp = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) <= 0 AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) <= 0 Then
                            isApplyTDS = True
                        Else
                            qry = "select sum( " + IIf(clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal, "TSPL_VENDOR_INVOICE_HEAD.Amount_Less_Discount", "TSPL_VENDOR_INVOICE_HEAD.Document_Total") + ") as Document_Total from TSPL_VENDOR_INVOICE_HEAD where Vendor_Code='" + VendorCode + "' and Document_Type in ('I','C') and Document_No not in ('" + DocNo + "') and  convert(date, Invoice_Entry_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("Start_Date")), "dd/MMM/yyyy") + "' and  convert(date, Invoice_Entry_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.myCDate(dtFY.Rows(0)("End_Date")), "dd/MMM/yyyy") + "' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null "
                            dblPreviousTDSAmt = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                            If appAmt >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff_Document")) > 0 Then
                                isApplyTDS = True
                            ElseIf (dblPreviousTDSAmt + appAmt) >= clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) AndAlso clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")) > 0 Then
                                isApplyTDS = True
                                appAmt = ((dblPreviousTDSAmt + appAmt) - clsCommon.myCdbl(dtTemp.Rows(0)("Cumm_Cutoff")))
                                dblPreviousTDSAmt = 0
                                TaxableAmt = appAmt
                                TotalAmt = appAmt
                            End If
                        End If
                    End If
                End If

                If isApplyTDS Then
                    objRemittance = New clsRemittance()
                    objRemittance.Branch_Code = objVendor.Branch_Code
                    objRemittance.Deduction_Code = objVendor.Nature_Of_Deduction
                    objRemittance.TDS_Per = objDedDetails.TDS
                    objRemittance.Surcharge_Per = objDedDetails.Surcharge
                    objRemittance.Edu_Cess_Per = objDedDetails.Educess
                    objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
                    objRemittance.IsTDSOverride = False

                    If IsNewEntry Then
                        objRemittance.IsApplyTDS = True
                    Else
                        objRemittance.IsApplyTDS = clsPIRemittance.IsTDSApplied(DocNo, trans)
                    End If
                    objRemittance.Section_Code = objVendor.TDSSection
                    objRemittance.Section_Description = objVendor.TDSSectionDescription
                    objRemittance.Select_By = objVendor.VendorTypeCode
                    'objRemittance.Include_Tax = objVendor.Include_Tax

                    objRemittance.Fiscal_Year = clsCommon.myCstr(dtFY.Rows(0)("Fiscal_Code"))
                    objRemittance.Quarter = "First"

                    UpdateTDSAmount(objRemittance, DocNo, docDate, dblPreviousTDSAmt, VendorCode, TaxableAmt, TotalAmt, trans)
                End If
            End If
        End If
        Return objRemittance
    End Function
    Shared Sub UpdateTDSAmount(ByRef objRemittance As clsRemittance, ByVal DocNo As String, ByVal docDate As DateTime, ByRef dblPreviousTDSAmt As Decimal, ByVal VendorCode As String, ByVal TaxableAmt As Decimal, ByVal TotalAmt As Decimal, ByVal trans As SqlTransaction)

        If (objRemittance IsNot Nothing) Then
            Dim IncludeTax As String = clsDBFuncationality.getSingleValue("select ISNULL(TSPL_TDS_SECTION_MASTER.Include_Tax,'') AS Include_Tax from TSPL_TDS_VENDOR_DETAILS left outer join TSPL_TDS_DEDUCTION_HEAD on TSPL_TDS_DEDUCTION_HEAD.Deduction_Code=TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction left outer join TSPL_TDS_SECTION_MASTER on TSPL_TDS_SECTION_MASTER.TDS_Group=TSPL_TDS_DEDUCTION_HEAD.TDS_Section where Vendor_Code='" + VendorCode + "'", trans)
            Dim applicableAmt As Double = 0
            If clsCommon.CompairString(IncludeTax, "N") = CompairStringResult.Equal Then
                applicableAmt = TaxableAmt
            Else
                applicableAmt = TotalAmt
            End If
            applicableAmt += dblPreviousTDSAmt


            Dim objDedDetails As clsTDSDeductionDetails = clsTDSDeductionDetails.GetApplicableTDRate(objRemittance.Deduction_Code, applicableAmt, trans, False, VendorCode)
            If (objDedDetails IsNot Nothing AndAlso objRemittance.IsApplyTDS) Then
                objRemittance.TDS_Per = objDedDetails.TDS
                objRemittance.Surcharge_Per = objDedDetails.Surcharge
                objRemittance.Edu_Cess_Per = objDedDetails.Educess
                objRemittance.Sec_Educess_Per = objDedDetails.Seceducess
            End If

            objRemittance.Vendor_Code = VendorCode
            objRemittance.Vendor_Name = clsVendorMaster.GetName(VendorCode, trans)
            objRemittance.Document_Date = docDate
            objRemittance.Document_Type = "I"
            objRemittance.Document_Amount = TotalAmt
            objRemittance.Calculated_TDS_Base = applicableAmt
            If Not objRemittance.IsTDSOverride Then
                objRemittance.Actual_TDS_Base = applicableAmt
            End If

            objRemittance.Calculated_TDS = (objRemittance.Calculated_TDS_Base * objRemittance.TDS_Per) / 100
            objRemittance.Actual_TDS = (objRemittance.Actual_TDS_Base * objRemittance.TDS_Per) / 100

            objRemittance.Calculated_Surcharge = (objRemittance.Calculated_TDS_Base * objRemittance.Surcharge_Per) / 100
            objRemittance.Actual_Surcharge = (objRemittance.Actual_TDS_Base * objRemittance.Surcharge_Per) / 100

            objRemittance.Calculated_Edu_Cess = (objRemittance.Calculated_TDS_Base * objRemittance.Edu_Cess_Per) / 100
            objRemittance.Actual_Edu_Cess = (objRemittance.Actual_TDS_Base * objRemittance.Edu_Cess_Per) / 100

            objRemittance.Calculated_Sec_Educess = (objRemittance.Calculated_TDS_Base * objRemittance.Sec_Educess_Per) / 100
            objRemittance.Actual_Sec_Educess = (objRemittance.Actual_TDS_Base * objRemittance.Sec_Educess_Per) / 100

            objRemittance.Calculated_Total_TDS = objRemittance.Calculated_TDS + objRemittance.Calculated_Surcharge + objRemittance.Calculated_Edu_Cess + objRemittance.Calculated_Sec_Educess
            objRemittance.Actual_Total_TDS = objRemittance.Actual_TDS + objRemittance.Actual_Surcharge + objRemittance.Actual_Edu_Cess + objRemittance.Actual_Sec_Educess
        End If
    End Sub
    Public Sub CreateVSPDebitNoteOfAsset(ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal dtDocDate As DateTime, ByVal strMCC As String, ByVal trans As SqlTransaction, ByVal NonCompanyVSPDeduction As Decimal, ByVal CompanyVSPDeduction As Decimal)
        Dim dblAmount As Decimal = NonCompanyVSPDeduction
        If objHead.No_Of_Asset > 0 Then
            dblAmount = CompanyVSPDeduction
        End If

        If dblAmount > 0 Then
            Dim strVendor As String = objHead.VSP_CODE
            Dim dt As DataTable
            Dim objVendorInvHead As New clsVedorInvoiceHead()
            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
            objVendorInvHead.isDeduction = 1
            objVendorInvHead.Security = 1
            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code from TSPL_DEDUCTION_MASTER  where Is_Default_Asset_Installment=1", trans)
            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                Throw New Exception("Please set default Asset Installment")
            End If
            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))

            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = strVendor
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(strVendor, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = dtDocDate
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(strMCC, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE

            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If

            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            objVendorInvHead.Description = "AP Debit Note For Non Company Assets Deduction"
            objVendorInvHead.RefDocType = "NCM-DED"
            If objHead.No_Of_Asset > 0 Then
                objVendorInvHead.RefDocType = "CM-DED"
                objVendorInvHead.Description = "AP Debit Note For Company Assets Deduction"
            End If
            objVendorInvHead.RefDocNo = objHead.DOC_CODE
            'objVendorInvHead.Ref_SNo = ""
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0

            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = dtDocDate

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, strMCC, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, strMCC, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()



            ''Set AP Invvoice Detail Table

            Dim strInvCtrlAC As String = clsCommon.myCstr(dt.Rows(0)("Deduction_ACCOUNT"))
            If clsCommon.myLen(strInvCtrlAC) <= 0 Then
                Throw New Exception("Please set Deduction Account for Vendor Account set Code :" + clsCommon.myCstr(dt.Rows(0)("Acct_Set_Code")))
            End If
            strInvCtrlAC = clsERPFuncationality.ChangeGLAccountLocationSegment(strInvCtrlAC, strMCC, trans)




            ii = ii + 1
            objVendorInvDetail.Detail_Line_No = ii
            objVendorInvDetail.GL_Account_Code = strInvCtrlAC
            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(strInvCtrlAC, trans)
            objVendorInvDetail.Amount = dblAmount

            objVendorInvDetail.Discount_Per = 0
            objVendorInvDetail.Discount = 0
            objVendorInvDetail.Amount_less_Discount = dblAmount
            objVendorInvDetail.Total_Tax = 0
            objVendorInvDetail.Total_Amount = dblAmount
            objVendorInvDetail.Landed_Amount = dblAmount
            ''End of Set AP Invvoice Detail Table

            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                objVendorInvHead.Arr.Add(objVendorInvDetail)
            End If

            ''Set AP Invvoice Header Table
            objVendorInvHead.Total_Landed_Amt += dblAmount
            objVendorInvHead.Discount_Base += dblAmount
            objVendorInvHead.Discount_Amount += 0
            objVendorInvHead.Amount_Less_Discount += dblAmount
            objVendorInvHead.Document_Total += dblAmount
            objVendorInvHead.Balance_Amt += dblAmount
            ''End of Set AP Invvoice Header Table

            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
            If objVendorInvHead.Empty_Amount > 0 Then
                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                    Throw New Exception("Please set Inventory Control Empties")
                End If
                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
            End If
            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = clsCommon.GetPrintDate(dtDocDate, "dd/MMM/yyyy")
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans, dtDocDate)
        End If
    End Sub
    Public Sub CreateApplyDocmentOfAssetLost(ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal dtDocDate As DateTime, ByVal strMCC As String, ByVal trans As SqlTransaction, ByVal NonCompanyVSPDeduction As Decimal, ByVal CompanyVSPDeduction As Decimal)
        Dim dtAPDebitNote As DataTable = clsDBFuncationality.GetDataTable("select Document_No,Document_Total from tspl_Vendor_invoice_head where Against_MillkPurchaseInvoice_No='" + objHead.DOC_CODE + "'", trans)
        If dtAPDebitNote Is Nothing OrElse dtAPDebitNote.Rows.Count > 0 Then
            Dim qry As String = "select xx.*,Item_Net_Amt/EMI_No_Of_Payment_Cycle as EMIAmt from (
select Doc_No,APDebitNoteAssetIssue,sum(Item_Net_Amt*(case when RI=1 then 1 else 0 end)) as Item_Net_Amt, sum(EMI_No_Of_Payment_Cycle*(case when RI=1 then 1 else 0 end)) as EMI_No_Of_Payment_Cycle,sum(case when RI=-1 then 1 else 0 end)as EMIPaid , sum(Item_Net_Amt*RI) as RemaingAmt from (
select  TSPL_VSPAsset_DETAIL.Doc_No,TSPL_VENDOR_INVOICE_HEAD.Document_No as APDebitNoteAssetIssue,case when (isnull(EMI_Asset_Value,0))>0 then EMI_Asset_Value else Item_Net_Amt end as Item_Net_Amt,EMI_No_Of_Payment_Cycle,1 as RI,1 as chk from TSPL_VSPAsset_DETAIL
left outer join  TSPL_VSPAsset_HEAD on TSPL_VSPAsset_HEAD.Doc_No=TSPL_VSPAsset_DETAIL.Doc_No
left outer join TSPL_VSPAsset_HEAD as Issue on Issue.Doc_No=TSPL_VSPAsset_HEAD.Issue_No
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Against_VSP_Asset_Issue=Issue.Doc_No
where TSPL_VSPAsset_HEAD.IS_LOST=1 and TSPL_VSPAsset_DETAIL.EMI_No_Of_Payment_Cycle>0 and TSPL_VSPAsset_HEAD.Status=1 and TSPL_VSPAsset_HEAD.Doc_Type='Return' and TSPL_VSPAsset_HEAD.Issue_To='" + objHead.VSP_CODE + "' and TSPL_VENDOR_INVOICE_HEAD.Document_No is not null
union all
select TSPL_PAYMENT_HEADER.Against_VSP_Asset_Lost,TSPL_PAYMENT_HEADER.Applied_Payment as APDebitNoteAssetIssue,TSPL_PAYMENT_HEADER.Payment_Amount,0 as EMI_No_Of_Payment_Cycle,-1 as RI,0 as chk  
from TSPL_PAYMENT_HEADER where TSPL_PAYMENT_HEADER.Entry_Desc = 'Apply document for Asset Lost'  and TSPL_PAYMENT_HEADER.Vendor_Code='" + objHead.VSP_CODE + "' and len(isnull(TSPL_PAYMENT_HEADER.Applied_Payment,''))>0
)x group by Doc_No,APDebitNoteAssetIssue having sum(chk)>0
)xx where RemaingAmt>1"
            Dim dtEMI As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtEMI IsNot Nothing AndAlso dtEMI.Rows.Count > 0 Then
                For Each drEMI As DataRow In dtEMI.Rows
                    Dim dblAmount As Decimal = clsCommon.myCdbl(drEMI("EMIAmt"))
                    If dblAmount > 0 Then
                        Dim objPay As clsPaymentHeader = New clsPaymentHeader()
                        objPay.Payment_No = ""
                        objPay.Payment_Date = dtDocDate
                        objPay.Payment_Post_Date = dtDocDate
                        objPay.Bank_Code = clsFixedParameter.GetData(clsFixedParameterType.BankCodeForApplyDocumentPaymentOFAssetLost, clsFixedParameterCode.BankCodeForApplyDocumentPaymentOFAssetLost, trans)
                        If clsCommon.myLen(objPay.Bank_Code) <= 0 Then
                            Throw New Exception("Please set Bank Code Of Fixed Parameter [" + clsFixedParameterType.BankCodeForApplyDocumentPaymentOFAssetLost + "]")
                        End If
                        objPay.Bank_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select BANK_CODE from TSPL_BANK_MASTER where BANK_CODE='" + objPay.Bank_Code + "'", trans))
                        If clsCommon.myLen(objPay.Bank_Code) <= 0 Then
                            Throw New Exception("Invalide Bank Code Of Fixed Parameter [" + clsFixedParameterType.BankCodeForApplyDocumentPaymentOFAssetLost + "]")
                        End If
                        objPay.Payment_Type = "AD"
                        objPay.Vendor_Code = objHead.VSP_CODE
                        objPay.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                        objPay.Payment_Amount = dblAmount
                        objPay.Balance_Amt = dblAmount
                        objPay.Location_GL_Code = clsLocation.GetSegmentCode(strMCC, trans)
                        objPay.Entry_Desc = "Apply document for Asset Lost"
                        objPay.Reference = objHead.DOC_CODE
                        objPay.Applied_Payment = clsCommon.myCstr(drEMI("APDebitNoteAssetIssue")) ''Debit Note Of Asset Issue Docucment
                        objPay.Against_VSP_Asset_Lost = clsCommon.myCstr(drEMI("Doc_No"))
                        objPay.ArrTr = New List(Of clsPaymentDetail)

                        Dim objPayTr As clsPaymentDetail = New clsPaymentDetail()
                        objPayTr.Apply = "1"
                        objPayTr.Payment_Type = "AD"
                        objPayTr.Document_No = clsCommon.myCstr(dtAPDebitNote.Rows(0)("Document_No"))
                        objPayTr.Net_Balance = clsCommon.myCdbl(dtAPDebitNote.Rows(0)("Document_Total"))
                        objPayTr.Original_Invoice_Amt = clsCommon.myCdbl(dtAPDebitNote.Rows(0)("Document_Total"))
                        objPayTr.Applied_Amount = dblAmount
                        'objPayTr.Pending_Balance = KFAmt
                        objPayTr.Vendor_Invoice_No = objHead.DOC_CODE
                        objPayTr.Security_Amount = 0
                        objPayTr.ConvRateOld = 1
                        objPay.ArrTr.Add(objPayTr)
                        objPay.SaveData(objPay, True, trans, True)
                        clsPaymentHeader.PostData(objPay.Payment_No, "Payable", trans)
                    End If
                Next
            End If
        End If
    End Sub
    Sub VSPCommissionAndDeduction(ByVal FromDate As Date, ByVal ToDate As Date, ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal strSRN_No As List(Of String), ByVal trans As SqlTransaction)
        If strSRN_No IsNot Nothing AndAlso strSRN_No.Count > 0 Then
            Dim dblAmount As Decimal
            Dim settVSPDayWiseIncentiveAtSRN As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VSPDayWiseIncentiveAtSRN, clsFixedParameterCode.VSPDayWiseIncentiveAtSRN, trans)) > 0)

            Dim qry As String = "update TSPL_MILK_SRN_DETAIL set VSP_Commission_Apply=0, VSP_Deduction_Apply=0 "
            If Not settVSPDayWiseIncentiveAtSRN Then
                qry += " ,VSP_Day_Wise_Incentive=0,VSP_Day_Wise_Incentive_Rate=0 "
            End If
            qry += " ,Farmer_Pro_Code=null,VSP_Mapping_Code_Day_Wise_Incentive=null,VSP_Mapping_Code=null where DOC_CODE In (" + clsCommon.GetMulcallString(strSRN_No) + ")  "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            Dim objVSPMapping As clsVSPMapping = clsVSPMapping.GetMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, objHead.DOC_DATE, trans)
            Dim ArrFarmerPro As New List(Of String)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(clsFarmerPro.GetLatestCodeByDate(objHead.MCC_CODE, objHead.VSP_CODE, FromDate, ToDate), trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    qry = clsCommon.GetPrintDate(clsCommon.myCDate(dr("thedate")), "dd/MMM/yyyy")
                    If Not ArrFarmerPro.Contains(qry) Then
                        ArrFarmerPro.Add(qry)
                    End If
                    If clsCommon.myLen(clsCommon.myCstr(dr("PriceCode"))) > 0 Then
                        qry = "update TSPL_MILK_SRN_DETAIL Set Farmer_Pro_Code='" + clsCommon.myCstr(dr("PriceCode")) + "' where DOC_CODE in (" +
                          "select DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and  convert(date, DOC_DATE,103)='" + clsCommon.GetPrintDate(dr("thedate"), "dd/MMM/yyyy") + "' and SHIFT='" + clsCommon.myCstr(dr("SHIFT")) + "')"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                Next
            End If
            Dim BaseQry As String = "select  REPLACE( convert(varchar, DOC_DATE,106),' ','/') as DOC_DATE,SHIFT,Qty  from (select  TSPL_MILK_SRN_HEAD.DOC_DATE,SHIFT,sum(Qty) as Qty " +
                        "from TSPL_MILK_SRN_DETAIL " +
                        "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " +
                        " where TSPL_MILK_SRN_HEAD.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") " +
                        "group by DOC_DATE,SHIFT )x  where 2=2 "
            If objVSPMapping IsNot Nothing Then
                Dim objVSPComm As clsVSPCommission = Nothing
                If clsCommon.myLen(objVSPMapping.Commission_Code) > 0 Then
                    objVSPComm = clsVSPCommission.GetData(objVSPMapping.Commission_Code, NavigatorType.Current, trans)
                End If
                Dim objVSPDeduction As clsVSPDeduction = Nothing
                If clsCommon.myLen(objVSPMapping.Deduction_Code) > 0 Then
                    objVSPDeduction = clsVSPDeduction.GetData(objVSPMapping.Deduction_Code, NavigatorType.Current, trans)
                End If

                For Each strSRN As String In strSRN_No
                    Dim dtSRN As DataTable = Nothing
                    Dim dclVSP_Commission_Amount As Decimal = 0
                    Dim dclVSP_Deduction_Amount As Decimal = 0
                    If clsCommon.myLen(objVSPMapping.Commission_Code) > 0 Then
                        dtSRN = getSRN(strSRN, trans)
                        If dtSRN IsNot Nothing AndAlso dtSRN.Rows.Count > 0 Then
                            If objVSPComm.Commission_Rate > 0 Then
                                Dim dtFATSNFUploader As DataTable = clsDBFuncationality.GetDataTable("select top 1 Price_Code,Planning_Code  from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + clsCommon.myCstr(dtSRN.Rows(0)("Price_Code")) + "'", trans)
                                dclVSP_Commission_Amount = clsEkoPro.GetRateCalculated(clsCommon.myCstr(dtFATSNFUploader.Rows(0)("Planning_Code")), clsCommon.myCstr(dtFATSNFUploader.Rows(0)("Price_Code")), clsCommon.myCdbl(dtSRN.Rows(0)("Qty")), clsCommon.myCdbl(dtSRN.Rows(0)("FAT_PER")), clsCommon.myCdbl(dtSRN.Rows(0)("SNF_PER")), trans, objVSPComm.Commission_Rate)
                            End If
                        End If
                    End If
                    If clsCommon.myLen(objVSPMapping.Deduction_Code) > 0 Then
                        If dtSRN Is Nothing OrElse dtSRN.Rows.Count <= 0 Then
                            dtSRN = getSRN(strSRN, trans)
                        End If
                        If dtSRN IsNot Nothing AndAlso dtSRN.Rows.Count > 0 Then
                            If clsCommon.myCdbl(dtSRN.Rows(0)("FAT_PER")) < objVSPDeduction.Deduction_Minimum_FAT_Per OrElse clsCommon.myCdbl(dtSRN.Rows(0)("SNF_PER")) < objVSPDeduction.Deduction_Minimum_SNF_Per Then
                                If objVSPDeduction.Deduction_Rate > 0 Then
                                    If objVSPDeduction.Deduction_On = 0 Then ''Apply On Rate
                                        dclVSP_Deduction_Amount = Math.Round(objVSPDeduction.Deduction_Rate * clsCommon.myCdbl(dtSRN.Rows(0)("Qty")), 0, MidpointRounding.AwayFromZero)
                                    Else ''Apply On Percentage
                                        dclVSP_Deduction_Amount = Math.Round((objVSPDeduction.Deduction_Rate * clsCommon.myCdbl(dtSRN.Rows(0)("NET_AMOUNT"))) / 100, 0, MidpointRounding.AwayFromZero)
                                    End If
                                End If
                            End If
                        End If
                    End If
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "VSP_Commission_Amount", dclVSP_Commission_Amount)
                    clsCommon.AddColumnsForChange(coll, "VSP_Deduction_Amount", dclVSP_Deduction_Amount)
                    clsCommon.AddColumnsForChange(coll, "VSP_Mapping_Code", objVSPMapping.Code)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_DETAIL", OMInsertOrUpdate.Update, "TSPL_MILK_SRN_DETAIL.DOC_CODE='" + strSRN + "'", trans)
                Next

                qry = "select count(*) from TSPL_MILK_PURCHASE_INVOICE_HEAD where vsp_code='" + objHead.VSP_CODE + "' and DOC_CODE not in ('" + objHead.DOC_CODE + "')"
                Dim countInv As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                If objVSPComm IsNot Nothing Then
                    If countInv >= clsCommon.myCdbl(objVSPComm.Commission_No_Of_Payment_Cycle_For_New_VSP) Then
                        dt = clsDBFuncationality.GetDataTable(BaseQry, trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If dt.Rows.Count >= clsCommon.myCdbl(objVSPComm.Commission_Minimum_Shift_In_Payment_Cycle) Then
                                qry = BaseQry + " and Qty >=" + clsCommon.myCstr(objVSPComm.Commission_Minimum_Qty_In_Shift) + ""
                                dt = clsDBFuncationality.GetDataTable(qry, trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    qry = "update TSPL_MILK_SRN_DETAIL set VSP_Commission_Apply=1 where DOC_CODE in (" +
                                "select  DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and ("
                                    For ii As Integer = 0 To dt.Rows.Count - 1
                                        qry += " (convert(date, DOC_DATE,103)='" + clsCommon.myCstr(dt.Rows(ii)("DOC_DATE")) + "' and SHIFT='" + clsCommon.myCstr(dt.Rows(ii)("SHIFT")) + "') "
                                        If ii = dt.Rows.Count - 1 Then
                                            qry += " )) "
                                        Else
                                            qry += " or "
                                        End If
                                    Next
                                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                End If
                            End If
                        End If
                    Else
                        qry = "update TSPL_MILK_SRN_DETAIL set VSP_Commission_Apply=1 where isnull(VSP_Commission_Amount,0)>0  and DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ")"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
                If objVSPDeduction IsNot Nothing Then
                    If countInv >= objVSPDeduction.Deduction_No_Of_Payment_Cycle_For_New_VSP Then
                        qry = "update TSPL_MILK_SRN_DETAIL set VSP_Deduction_Apply=1 where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and isnull( VSP_Deduction_Amount,0)>0"
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If
                End If
            End If


#Region "VSPDayWiseIncentiveCalculatioin"
            If Not settVSPDayWiseIncentiveAtSRN Then
                qry = "select DOC_DATE,sum(Qty) as Qty  from (" + BaseQry + ")x group by DOC_DATE"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If ArrFarmerPro.Contains(clsCommon.GetPrintDate(clsCommon.myCDate(dr("DOC_DATE")), "dd/MMM/yyyy")) Then
                            Continue For
                        End If
                        Dim dtVSPdayWiseIncentive As DataTable = Nothing
                        objVSPMapping = clsVSPMapping.GetMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, clsCommon.myCDate(dr("DOC_DATE")), trans)
                        If objVSPMapping IsNot Nothing Then
                            If clsCommon.myLen(objVSPMapping.Day_Wise_Incentive_Code) > 0 Then
                                qry = "select * from TSPL_VSP_DAY_WISE_INCENTIVE_MASTER where Day_Wise_Incentive_Code='" + objVSPMapping.Day_Wise_Incentive_Code + "'"
                                dtVSPdayWiseIncentive = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtVSPdayWiseIncentive IsNot Nothing AndAlso dtVSPdayWiseIncentive.Rows.Count > 0 Then
                                    For ii As Integer = 5 To 1 Step -1
                                        If clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_Rate_" + clsCommon.myCstr(ii) + "")) > 0 Then
                                            If clsCommon.myCdbl(dr("Qty")) >= clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_From_" + clsCommon.myCstr(ii) + "")) AndAlso clsCommon.myCdbl(dr("Qty")) <= clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_To_" + clsCommon.myCstr(ii) + "")) Then
                                                qry = "select DOC_CODE,Qty,FAT_PER,SNF_PER,Price_Code,NET_AMOUNT from TSPL_MILK_SRN_DETAIL where DOC_CODE in (" +
                                                    "select  DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") and  convert(date, DOC_DATE,103)='" + clsCommon.myCstr(dr("DOC_DATE")) + "')"
                                                Dim dtSRND As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                                If dtSRND IsNot Nothing AndAlso dtSRND.Rows.Count > 0 Then
                                                    For Each drSRND As DataRow In dtSRND.Rows
                                                        Dim strPMCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 Price_Code  from TSPL_FAT_SNF_UPLOADER_MASTER where Code='" + clsCommon.myCstr(drSRND("Price_Code")) + "'", trans))
                                                        Dim IncAmt As Decimal = clsEkoPro.GetRateCalculatedExact(strPMCode, clsCommon.myCdbl(drSRND("Qty")), clsCommon.myCdbl(drSRND("FAT_PER")), clsCommon.myCdbl(drSRND("SNF_PER")), trans, clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_Rate_" + clsCommon.myCstr(ii) + "")))
                                                        IncAmt -= clsCommon.myCdbl(drSRND("NET_AMOUNT"))
                                                        qry = "update TSPL_MILK_SRN_DETAIL set VSP_Day_Wise_Incentive_Rate='" + clsCommon.myCstr(clsCommon.myCdbl(dtVSPdayWiseIncentive.Rows(0)("Day_Wise_Incentive_Rate_" + clsCommon.myCstr(ii) + ""))) + "', VSP_Day_Wise_Incentive=" + clsCommon.myCstr(IncAmt) + ",VSP_Mapping_Code_Day_Wise_Incentive='" + objVSPMapping.Code + "' where DOC_CODE='" + clsCommon.myCstr(drSRND("DOC_CODE")) + "'"
                                                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                                                    Next
                                                End If
                                                Exit For
                                            End If
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If
            End If
#End Region

            qry = " select Item_Code,max(Item_Desc) as Item_Desc,max(UOM) as UOM,sum(Fat_Amt) as Fat_Amt,sum(SNF_Amt) as SNF_Amt from TSPL_INVENTORY_MOVEMENT_NEW where source_doc_no in (" + clsCommon.GetMulcallString(strSRN_No) + ") and Trans_Type='MCC-MSRN' group by Item_Code   "
            Dim dtSRNFATSNF As DataTable = clsDBFuncationality.GetDataTable(qry, trans)

            qry = "select sum( VSP_Commission_Amount*VSP_Commission_Apply) as VSP_Commission_Amount,sum(VSP_Deduction_Amount *  VSP_Deduction_Apply) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive from TSPL_MILK_SRN_DETAIL where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") "
            Dim dtAmt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Commission_Amount")) > 0 Then
#Region "CreateCreditNotForCommision"
                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                    objVendorInvHead.isDeduction = 0
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                    objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                    objVendorInvHead.Vendor_Invoice_No = ""
                    objVendorInvHead.Invoice_Type = "AP"
                    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                    objVendorInvHead.Description = "AP Credit Note For VSP Commission"
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                    End If
                    objVendorInvHead.Document_Type = "C"
                    objVendorInvHead.RefDocType = "VSP-COM"
                    objVendorInvHead.RefDocNo = objHead.DOC_CODE
                    objVendorInvHead.On_Hold = False
                    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                    dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT,Commission_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If
                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim ii As Integer = 0
                    Dim isFirstTime As Boolean = True
                    objVendorInvHead.Total_Landed_Amt = 0
                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()


                    If True Then
                        ''Set AP Invvoice Detail Table
                        ii = ii + 1
                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                        objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Commission_ACCOUNT"))
                        If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                            Throw New Exception("Please set Commission Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
                        End If
                        objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                        objVendorInvDetail.Detail_Line_No = ii
                        objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                        dblAmount = clsERPFuncationality.myFloor(clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Commission_Amount")), 0)
                        objVendorInvDetail.Amount = dblAmount
                        objVendorInvDetail.Discount_Per = 0
                        objVendorInvDetail.Discount = 0
                        objVendorInvDetail.Amount_less_Discount = dblAmount
                        objVendorInvDetail.Total_Tax = 0
                        objVendorInvDetail.Total_Amount = dblAmount
                        objVendorInvDetail.Landed_Amount = dblAmount
                        ''End of Set AP Invvoice Detail Table

                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                            objVendorInvHead.Arr.Add(objVendorInvDetail)
                        End If

                        ''Set AP Invvoice Header Table
                        objVendorInvHead.Total_Landed_Amt += dblAmount
                        objVendorInvHead.Discount_Base += dblAmount
                        objVendorInvHead.Discount_Amount += 0
                        objVendorInvHead.Amount_Less_Discount += dblAmount
                        objVendorInvHead.Document_Total += dblAmount
                        objVendorInvHead.Balance_Amt += dblAmount
                        ''End of Set AP Invvoice Header Table
                    End If
                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                        Throw New Exception("No GL Account Found For AP Invoice")
                    End If
                    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)

                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
#End Region

                    CreateCostAdjustmentAgainstAPInvoice(dtSRNFATSNF, objVendorInvHead, objHead.MCC_CODE, objHead.MCC_NAME, "Cost Adjustment Against AP Invoice (VSP Commissioin) : ", trans)
                End If

                If clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Deduction_Amount")) > 0 Then
#Region "CreateDebitNotForDeduction"
                    'Dim objDedMapping As New clsDeductionMappingHead
                    'objDedMapping = objDedMapping.GetLatestMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, objHead.DOC_DATE, trans)
                    If True Then
                        Dim objVendorInvHead As New clsVedorInvoiceHead()
                        objVendorInvHead.isDeduction = 1
                        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                        objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                        objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                        objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                        objVendorInvHead.Invoice_Type = "AP"
                        objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                        objVendorInvHead.Description = "AP Debit Note Against VSP Quality Deduction"
                        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                            Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                        End If
                        objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                        objVendorInvHead.RefDocType = "VSP-QLT"
                        objVendorInvHead.RefDocNo = objHead.DOC_CODE
                        objVendorInvHead.On_Hold = False
                        objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                        dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                            End If
                        End If
                        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                            Throw New Exception("Please set the vendor payable Account")
                        End If
                        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                        Dim ii As Integer = 0
                        Dim isFirstTime As Boolean = True
                        objVendorInvHead.Total_Landed_Amt = 0
                        objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                        If True Then
                            ''Set AP Invvoice Detail Table
                            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_VSP_Quality_Deduction=1", trans)
                            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                Throw New Exception("Please set default VSP Quality deduction in Deduction Master")
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                            End If

                            ii = ii + 1
                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                            objVendorInvDetail.Detail_Line_No = ii
                            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                            objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                            dblAmount = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Deduction_Amount")), 0)
                            objVendorInvDetail.Amount = dblAmount
                            objVendorInvDetail.Discount_Per = 0
                            objVendorInvDetail.Discount = 0
                            objVendorInvDetail.Amount_less_Discount = dblAmount
                            objVendorInvDetail.Total_Tax = 0
                            objVendorInvDetail.Total_Amount = dblAmount
                            objVendorInvDetail.Landed_Amount = dblAmount
                            ''End of Set AP Invvoice Detail Table
                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                objVendorInvHead.Arr.Add(objVendorInvDetail)
                            End If

                            ''Set AP Invvoice Header Table
                            objVendorInvHead.Total_Landed_Amt += dblAmount
                            objVendorInvHead.Discount_Base += dblAmount
                            objVendorInvHead.Discount_Amount += 0
                            objVendorInvHead.Amount_Less_Discount += dblAmount
                            objVendorInvHead.Document_Total += dblAmount
                            objVendorInvHead.Balance_Amt += dblAmount
                            ''End of Set AP Invvoice Header Table

                            objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                            If objVendorInvHead.Empty_Amount > 0 Then
                                If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                    Throw New Exception("Please set Inventory Control Empties")
                                End If
                                objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                            End If
                        End If
                        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                            Throw New Exception("No GL Account Found For AP Invoice")
                        End If
                        objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                        objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                        clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

                        CreateCostAdjustmentAgainstAPInvoice(dtSRNFATSNF, objVendorInvHead, objHead.MCC_CODE, objHead.MCC_NAME, "Cost Adjustment Against AP Invoice (VSP Deduction) : ", trans)
                    End If
#End Region
                End If

                If clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Day_Wise_Incentive")) > 0 Then
#Region "CreateCreditNotForVSPDayWiseIncentive"
                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                    objVendorInvHead.isDeduction = 0
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                    objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                    objVendorInvHead.Vendor_Invoice_No = ""
                    objVendorInvHead.Invoice_Type = "AP"
                    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                    objVendorInvHead.Description = "AP Credit Note For VSP Commission"
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                    End If
                    objVendorInvHead.Document_Type = "C"
                    objVendorInvHead.RefDocType = "VSP-DIT"
                    objVendorInvHead.RefDocNo = objHead.DOC_CODE
                    objVendorInvHead.On_Hold = False
                    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                    dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT,Incentive_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If
                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim ii As Integer = 0
                    Dim isFirstTime As Boolean = True
                    objVendorInvHead.Total_Landed_Amt = 0
                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                    If True Then
                        ''Set AP Invvoice Detail Table
                        ii = ii + 1
                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                        objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCOUNT"))
                        If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                            Throw New Exception("Please set Incentive Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
                        End If
                        objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                        objVendorInvDetail.Detail_Line_No = ii
                        objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                        dblAmount = clsERPFuncationality.myFloor(clsCommon.myCdbl(dtAmt.Rows(0)("VSP_Day_Wise_Incentive")), 0)
                        objVendorInvDetail.Amount = dblAmount
                        objVendorInvDetail.Discount_Per = 0
                        objVendorInvDetail.Discount = 0
                        objVendorInvDetail.Amount_less_Discount = dblAmount
                        objVendorInvDetail.Total_Tax = 0
                        objVendorInvDetail.Total_Amount = dblAmount
                        objVendorInvDetail.Landed_Amount = dblAmount
                        ''End of Set AP Invvoice Detail Table

                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                            objVendorInvHead.Arr.Add(objVendorInvDetail)
                        End If

                        ''Set AP Invvoice Header Table
                        objVendorInvHead.Total_Landed_Amt += dblAmount
                        objVendorInvHead.Discount_Base += dblAmount
                        objVendorInvHead.Discount_Amount += 0
                        objVendorInvHead.Amount_Less_Discount += dblAmount
                        objVendorInvHead.Document_Total += dblAmount
                        objVendorInvHead.Balance_Amt += dblAmount
                        ''End of Set AP Invvoice Header Table
                    End If
                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                        Throw New Exception("No GL Account Found For AP Invoice")
                    End If
                    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
#End Region
                End If

            End If
            Dim strVLCCode As String = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VSP_Code='" + objHead.VSP_CODE + "'", trans)
            If ArrFarmerPro IsNot Nothing Then
                Dim settProStartDate As String = clsFixedParameter.GetData(clsFixedParameterType.BholeBabaPaymentProcessProPrintStartDate, clsFixedParameterCode.BholeBabaPaymentProcessProPrintStartDate, trans)
                Dim IsNewFormatApplicable As Boolean = False
                If clsCommon.myLen(settProStartDate) > 0 Then
                    If clsCommon.GetDateWithStartTime(clsCommon.myCDate(settProStartDate)) <= clsCommon.GetDateWithStartTime(FromDate) Then
                        IsNewFormatApplicable = True
                    End If
                End If

                dblAmount = 0

                Dim SettLocalSaleAllowedPer As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LocalSaleAllowedPer, clsFixedParameterCode.LocalSaleAllowedPer, trans))
                Dim SettLocalSaleRate As Decimal = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.LocalSaleAllowedRate, clsFixedParameterCode.LocalSaleAllowedRate, trans))

                qry = clsMilkPurchaseInvoiceProvisionHead.GetQryProData(objHead.DOC_CODE, strVLCCode, strSRN_No, FromDate, ToDate, SettLocalSaleAllowedPer, SettLocalSaleRate, IsNewFormatApplicable)
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                qry = "select   sum(NoteAmt) as NoteAmt,sum(Local_Sale_Amt) as Local_Sale_Amt,case when (sum(Farmer_Std_Qty) - sum(VSP_Std_Qty))>0 then cast((sum(FarmerAmt)*(sum(Farmer_Std_Qty) - sum(VSP_Std_Qty)))/sum(Farmer_Std_Qty) as decimal(18,2)) else 0 end as StdDedAmt from (
select  NoteAmt,Local_Sale_Amt
,case when Farmer_Qty=0 then VSP_Std_Qty  else Farmer_Std_Qty end as Farmer_Std_Qty
,VSP_Std_Qty
,case when Farmer_Qty=0 then VSPAmt else FarmerAmt end as FarmerAmt
,Farmer_Std_Qty as Org_Farmer_Std_Qty,FarmerAmt as Org_FarmerAmt  from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where InvoiceNo='" + objHead.DOC_CODE + "'
)xx"
                dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")) < 0 Then
#Region "CreateCreditNotForPROData"
                        Dim objVendorInvHead As New clsVedorInvoiceHead()
                        objVendorInvHead.isDeduction = 0
                        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                        objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                        objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                        objVendorInvHead.Vendor_Invoice_No = ""
                        objVendorInvHead.Invoice_Type = "AP"
                        objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                        objVendorInvHead.Description = "AP Credit Note For Farmer PRO"
                        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                            Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                        End If
                        objVendorInvHead.Document_Type = "C"
                        objVendorInvHead.RefDocType = "PRO-VFC"
                        objVendorInvHead.RefDocNo = objHead.DOC_CODE
                        objVendorInvHead.On_Hold = False
                        objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                        dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                            End If
                        End If
                        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                            Throw New Exception("Please set the vendor payable Account")
                        End If
                        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                        Dim ii As Integer = 0
                        Dim isFirstTime As Boolean = True
                        objVendorInvHead.Total_Landed_Amt = 0
                        objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                        If True Then
                            ''Set AP Invvoice Detail Table
                            ii = ii + 1
                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("PRO_DATA_ACCOUNT"))
                            If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                                Throw New Exception("Please set PRO Data Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
                            End If
                            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                            objVendorInvDetail.Detail_Line_No = ii
                            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                            dblAmount = Math.Abs(clsERPFuncationality.myFloor(clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")), 0))
                            objVendorInvDetail.Amount = dblAmount
                            objVendorInvDetail.Discount_Per = 0
                            objVendorInvDetail.Discount = 0
                            objVendorInvDetail.Amount_less_Discount = dblAmount
                            objVendorInvDetail.Total_Tax = 0
                            objVendorInvDetail.Total_Amount = dblAmount
                            objVendorInvDetail.Landed_Amount = dblAmount
                            ''End of Set AP Invvoice Detail Table

                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                objVendorInvHead.Arr.Add(objVendorInvDetail)
                            End If

                            ''Set AP Invvoice Header Table
                            objVendorInvHead.Total_Landed_Amt += dblAmount
                            objVendorInvHead.Discount_Base += dblAmount
                            objVendorInvHead.Discount_Amount += 0
                            objVendorInvHead.Amount_Less_Discount += dblAmount
                            objVendorInvHead.Document_Total += dblAmount
                            objVendorInvHead.Balance_Amt += dblAmount
                            ''End of Set AP Invvoice Header Table
                        End If
                        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                            Throw New Exception("No GL Account Found For AP Invoice")
                        End If
                        objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                        objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                        clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

                        CreateCostAdjustmentAgainstAPInvoice(dtSRNFATSNF, objVendorInvHead, objHead.MCC_CODE, objHead.MCC_NAME, "Cost Adjustment Against AP Invoice (Farmer PRO Credit Note) : ", trans)
#End Region
                    ElseIf clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")) > 0 Then
#Region "CreateDebitNotForProDATA"
                        If True Then
                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                            objVendorInvHead.Description = "AP Debit Note Against VLC Farmer PRO Data"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "PRO-VFD"
                            objVendorInvHead.RefDocNo = objHead.DOC_CODE
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                            dblAmount = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")), 0)
                            If True Then
                                ''Set AP Invvoice Detail Table
                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_PRO_Data=1", trans)
                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                    Throw New Exception("Please set default PRO Data deduction in Deduction Master")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                End If

                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

                            CreateCostAdjustmentAgainstAPInvoice(dtSRNFATSNF, objVendorInvHead, objHead.MCC_CODE, objHead.MCC_NAME, "Cost Adjustment Against AP Invoice (Farmer PRO Debit Note) : ", trans)
                        End If
#End Region
                    End If
                    If IsNewFormatApplicable Then
#Region "DebitNoteOfStdDeduction"
                        dblAmount = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("StdDedAmt")), 0)
                        If dblAmount > 0 Then
                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                            objVendorInvHead.Description = "AP Debit Note Std Deduction"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "PRO-STD"
                            objVendorInvHead.RefDocNo = objHead.DOC_CODE
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                            If True Then
                                ''Set AP Invvoice Detail Table
                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_Std_Deduction=1", trans)
                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                    Throw New Exception("Please set default Std deduction in Deduction Master")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                End If

                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

                            CreateCostAdjustmentAgainstAPInvoice(dtSRNFATSNF, objVendorInvHead, objHead.MCC_CODE, objHead.MCC_NAME, "Cost Adjustment Against AP Invoice (Farmer PRO Debit Note) : ", trans)
                        End If
#End Region

#Region "DebitNoteOfLocalSale"
                        dblAmount = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("Local_Sale_Amt")), 0)
                        If dblAmount > 0 Then
                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 1
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                            objVendorInvHead.Description = "AP Debit Note Local Sale"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                            objVendorInvHead.RefDocType = "PRO-LCS"
                            objVendorInvHead.RefDocNo = objHead.DOC_CODE
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                            If True Then
                                ''Set AP Invvoice Detail Table
                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_Local_Sale=1", trans)
                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                    Throw New Exception("Please set default Local Sale deduction in Deduction Master")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                End If

                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount
                                ''End of Set AP Invvoice Detail Table
                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table

                                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                If objVendorInvHead.Empty_Amount > 0 Then
                                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                        Throw New Exception("Please set Inventory Control Empties")
                                    End If
                                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                End If
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

                            CreateCostAdjustmentAgainstAPInvoice(dtSRNFATSNF, objVendorInvHead, objHead.MCC_CODE, objHead.MCC_NAME, "Cost Adjustment Against AP Invoice (Farmer PRO Debit Note) : ", trans)
                        End If
#End Region
                    End If
                End If
            End If


            If clsfrmVLCMaster.IsOwnBMC(strVLCCode, objHead.MCC_CODE, trans) Then
#Region "CreateCreditNotForOWNBMCExpanse"
                qry = "select 1 from TSPL_OWN_BMC_EXPANSE"
                Dim dtTemp As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                    qry = "select top 1 case when TSPL_OWN_BMC_EXPANSE.Inactive=0 then TSPL_OWN_BMC_EXPANSE.Code else '' end as  FindCode,TSPL_OWN_BMC_EXPANSE.FAT,TSPL_OWN_BMC_EXPANSE.SNF,TSPL_OWN_BMC_EXPANSE.Rate 
from TSPL_OWN_BMC_EXPANSE 
where '" + clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy") + "'>=TSPL_OWN_BMC_EXPANSE.Start_Date  and (2= case when TSPL_OWN_BMC_EXPANSE.End_Date is null then 2 else case when '" + clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy") + "'<= TSPL_OWN_BMC_EXPANSE.End_Date then 2 else 3 end end)  and TSPL_OWN_BMC_EXPANSE.Posted=1 order by TSPL_OWN_BMC_EXPANSE.Start_Date desc,TSPL_OWN_BMC_EXPANSE.Code desc"
                    dtTemp = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                        If clsCommon.myLen(clsCommon.myCstr(dtTemp.Rows(0)("FindCode"))) > 0 Then
                            qry = "select sum(Qty) as Qty,sum(FAT_KG) as FAT_KG,sum(SNF_KG) as SNF_KG from TSPL_MILK_SRN_DETAIL where DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ")"
                            dt = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                If clsCommon.myCDecimal(dt.Rows(0)("Qty")) > 0 Then
                                    Dim dclFATPer As Decimal = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("FAT_KG")) * 100, clsCommon.myCDecimal(dt.Rows(0)("Qty")))
                                    dclFATPer = clsCommon.myRoundOFF(dclFATPer, 2, 4)

                                    Dim dclSNFPer As Decimal = clsCommon.myCDivide(clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")) * 100, clsCommon.myCDecimal(dt.Rows(0)("Qty")))
                                    dclSNFPer = clsCommon.myRoundOFF(dclSNFPer, 2, 4)

                                    Dim dclRate As Decimal = 0
                                    Dim coll As New Hashtable()
                                    clsCommon.AddColumnsForChange(coll, "InvoiceNo", objHead.DOC_CODE)
                                    clsCommon.AddColumnsForChange(coll, "Against_Own_BMC_Expanse", clsCommon.myCstr(dtTemp.Rows(0)("FindCode")))
                                    If dclFATPer >= clsCommon.myCDecimal(dtTemp.Rows(0)("FAT")) AndAlso dclSNFPer >= clsCommon.myCDecimal(dtTemp.Rows(0)("SNF")) Then
                                        dclRate = clsCommon.myCDecimal(dtTemp.Rows(0)("Rate"))
                                    Else
                                        qry = "select PK_Id,Rate from TSPL_OWN_BMC_EXPANSE_SLAB where Code='" + clsCommon.myCstr(dtTemp.Rows(0)("FindCode")) + "' and SNF_From <='" + clsCommon.myCstr(dclSNFPer) + "' and SNF_To >= '" + clsCommon.myCstr(dclSNFPer) + "' "
                                        dtTemp = clsDBFuncationality.GetDataTable(qry, trans)
                                        If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                            dclRate = clsCommon.myCDecimal(dtTemp.Rows(0)("Rate"))
                                            clsCommon.AddColumnsForChange(coll, "Against_Own_BMC_Expanse_Slab", clsCommon.myCDecimal(dtTemp.Rows(0)("PK_Id")), True)
                                        End If
                                    End If
                                    clsCommon.AddColumnsForChange(coll, "Qty", clsCommon.myCDecimal(dt.Rows(0)("Qty")))
                                    clsCommon.AddColumnsForChange(coll, "Rate", dclRate)
                                    clsCommon.AddColumnsForChange(coll, "FAT_KG", clsCommon.myCDecimal(dt.Rows(0)("FAT_KG")))
                                    clsCommon.AddColumnsForChange(coll, "SNF_KG", clsCommon.myCDecimal(dt.Rows(0)("SNF_KG")))
                                    clsCommon.AddColumnsForChange(coll, "FAT_Per", dclFATPer)
                                    clsCommon.AddColumnsForChange(coll, "SNF_Per", dclSNFPer)
                                    clsCommon.AddColumnsForChange(coll, "Amt", clsCommon.myCDecimal(dt.Rows(0)("Qty")) * dclRate)
                                    If dclRate > 0 Then
                                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_OWN_BMC_EXPANSE", OMInsertOrUpdate.Insert, "", trans)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                ''Now Create Dr/Cr Note
                qry = "select sum(Amt) as NoteAmt  from TSPL_MILK_PURCHASE_INVOICE_OWN_BMC_EXPANSE where InvoiceNo='" + objHead.DOC_CODE + "'"
                dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
                dblAmount = 0
                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                    If clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")) > 0 Then

                        Dim objVendorInvHead As New clsVedorInvoiceHead()
                        objVendorInvHead.isDeduction = 0
                        objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                        objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                        objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                        objVendorInvHead.Vendor_Invoice_No = ""
                        objVendorInvHead.Invoice_Type = "AP"
                        objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                        objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                        objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                        If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                            Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                        End If
                        objVendorInvHead.Document_Type = "C"

                        objVendorInvHead.Description = "AP Credit Note For Own BMC Expanse"
                        objVendorInvHead.RefDocType = "OWD-CRE"

                        objVendorInvHead.RefDocNo = objHead.DOC_CODE
                        objVendorInvHead.On_Hold = False
                        objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                        dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                            objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                            If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                            End If
                        End If
                        If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                            Throw New Exception("Please set the vendor payable Account")
                        End If
                        objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                        Dim ii As Integer = 0
                        Dim isFirstTime As Boolean = True
                        objVendorInvHead.Total_Landed_Amt = 0
                        objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                        If True Then
                            ''Set AP Invvoice Detail Table

                            Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Own_BMC_Excess=1", trans)
                            If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                Throw New Exception("Please set default  Own BMC Excess in Deduction Master")
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                            End If

                            ii = ii + 1
                            Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                            objVendorInvDetail.Detail_Line_No = ii
                            objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                            objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                            objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                            objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                            objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                            dblAmount = Math.Abs(clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")))
                            objVendorInvDetail.Amount = dblAmount
                            objVendorInvDetail.Discount_Per = 0
                            objVendorInvDetail.Discount = 0
                            objVendorInvDetail.Amount_less_Discount = dblAmount
                            objVendorInvDetail.Total_Tax = 0
                            objVendorInvDetail.Total_Amount = dblAmount
                            objVendorInvDetail.Landed_Amount = dblAmount
                            ''End of Set AP Invvoice Detail Table

                            If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                objVendorInvHead.Arr.Add(objVendorInvDetail)
                            End If

                            ''Set AP Invvoice Header Table
                            objVendorInvHead.Total_Landed_Amt += dblAmount
                            objVendorInvHead.Discount_Base += dblAmount
                            objVendorInvHead.Discount_Amount += 0
                            objVendorInvHead.Amount_Less_Discount += dblAmount
                            objVendorInvHead.Document_Total += dblAmount
                            objVendorInvHead.Balance_Amt += dblAmount
                            ''End of Set AP Invvoice Header Table
                        End If
                        If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                            Throw New Exception("No GL Account Found For AP Invoice")
                        End If
                        objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                        objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                        objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                        clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

                    End If
                End If
#End Region

                If clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.OwnBMCCreateDRCRNote, clsFixedParameterCode.OwnBMCCreateDRCRNote, trans)) = 1 Then
                    Dim dclFATApplicatblePer As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.OwnBMCApplicationFATRatio, clsFixedParameterCode.OwnBMCApplicationFATRatio, trans))
                    Dim dclSNFApplicatblePer As Decimal = clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.OwnBMCApplicationSNFRatio, clsFixedParameterCode.OwnBMCApplicationSNFRatio, trans))

                    qry = "select  PK_Id,max(Document_Date) as Document_Date,sum(MCCQty) as MCCQty,sum(MCCFAT) as MCCFAT,sum(MCCSNF) as MCCSNF,sum(MCCFATKG) as MCCFATKG,sum(MCCSNFKG) as MCCSNFKG,sum(DCSQty) as DCSQty,sum(DCSFATKG) as DCSFATKG,sum(DCSSNFKG) as DCSSNFKG,(max(MCCFATKG) -sum(DCSFATKG)) as DiffFATKG,(max(MCCSNFKG)-sum(DCSSNFKG)) as DiffSNFKG from (
select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) as Document_Date,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty as MCCQty,TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as MCCFATKG,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as MCCFAT,TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as MCCSNF,TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as MCCSNFKG
,0 as DCSQty ,0 as DCSFATKG ,0 as DCSSNFKG
from   TSPL_MILK_COLLECTION_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code='" + objHead.MCC_CODE + "'
union all
select Tab.PK_Id,null as Document_Date,0 as MCCQty,0 as MCCFATKG,0 as MCCFAT,0 as MCCSNF,0 as MCCSNFKG
,TSPL_MILK_COLLECTION_DCS_DETAIL.Qty as DCSQty ,TSPL_MILK_COLLECTION_DCS_DETAIL.FATKG as DCSFATKG ,TSPL_MILK_COLLECTION_DCS_DETAIL.SNFKG as DCSSNFKG
from   TSPL_MILK_COLLECTION_DCS_DETAIL 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No 
inner join (
select Document_No,min(PK_Id) as PK_Id from (
select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No 
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
left outer join  TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
where convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code='" + objHead.MCC_CODE + "'
)xx group by xx.Document_No
)Tab on Tab.Document_No= TSPL_MILK_COLLECTION_DCS.Document_No
)X group by PK_Id having (abs(max(MCCFATKG) -sum(DCSFATKG))>0 or abs(max(MCCSNFKG)-sum(DCSSNFKG))>0)
and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_OWN_BMC where TSPL_MILK_PURCHASE_INVOICE_OWN_BMC.Against_Milk_Collection_MCC_Detail=x.PK_Id)"
                    dt = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "InvoiceNo", objHead.DOC_CODE)
                            clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(clsCommon.myCDate(dr("Document_Date")), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Against_Milk_Collection_MCC_Detail", clsCommon.myCDecimal(dr("PK_Id")))
                            clsCommon.AddColumnsForChange(coll, "DCS_Qty", clsCommon.myCDecimal(dr("DCSQty")))
                            clsCommon.AddColumnsForChange(coll, "DCS_FATKG", clsCommon.myCDecimal(dr("DCSFATKG")))
                            clsCommon.AddColumnsForChange(coll, "DCS_SNFKG", clsCommon.myCDecimal(dr("DCSSNFKG")))
                            clsCommon.AddColumnsForChange(coll, "DIFF_FATKG", clsCommon.myCDecimal(dr("DiffFATKG")))
                            clsCommon.AddColumnsForChange(coll, "DIFF_SNFKG", clsCommon.myCDecimal(dr("DiffSNFKG")))
                            Dim dclFATAmt As Decimal = 0
                            Dim dclSNFAmt As Decimal = 0
                            Dim FAT_Rate As Decimal = 0
                            Dim SNF_Rate As Decimal = 0
                            Dim AgainstDocCode As String = Nothing
                            qry = "select 1 from TSPL_OWN_BMC_GAIN_LOSS_RATE"
                            dtTemp = clsDBFuncationality.GetDataTable(qry, trans)

                            If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                qry = "select top 1 case when TSPL_OWN_BMC_GAIN_LOSS_RATE.Inactive=0 then TSPL_OWN_BMC_GAIN_LOSS_RATE.Code else '' end as  FindCode,TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_FAT_Rate,TSPL_OWN_BMC_GAIN_LOSS_RATE.Gain_SNF_Rate,TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_FAT_Rate,TSPL_OWN_BMC_GAIN_LOSS_RATE.Loss_SNF_Rate  
from TSPL_OWN_BMC_GAIN_LOSS_RATE 
where '" + clsCommon.GetPrintDate(clsCommon.myCDate(dr("Document_Date")), "dd/MMM/yyyy") + "'>=TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date  and (2= case when TSPL_OWN_BMC_GAIN_LOSS_RATE.End_Date is null then 2 else case when '" + clsCommon.GetPrintDate(clsCommon.myCDate(dr("Document_Date")), "dd/MMM/yyyy") + "'<= TSPL_OWN_BMC_GAIN_LOSS_RATE.End_Date then 2 else 3 end end)  and TSPL_OWN_BMC_GAIN_LOSS_RATE.Posted=1 order by TSPL_OWN_BMC_GAIN_LOSS_RATE.Start_Date desc,TSPL_OWN_BMC_GAIN_LOSS_RATE.Code desc"
                                dtTemp = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtTemp IsNot Nothing AndAlso dtTemp.Rows.Count > 0 Then
                                    AgainstDocCode = clsCommon.myCstr(dtTemp.Rows(0)("FindCode"))
                                    If clsCommon.myLen(AgainstDocCode) > 0 Then
                                        If clsCommon.myCDecimal(dr("DiffFATKG")) < 0 Then
                                            FAT_Rate = clsCommon.myCdbl(dtTemp.Rows(0)("Loss_FAT_Rate"))
                                        Else
                                            FAT_Rate = clsCommon.myCdbl(dtTemp.Rows(0)("Gain_FAT_Rate"))
                                        End If
                                        If clsCommon.myCDecimal(dr("DiffSNFKG")) < 0 Then
                                            SNF_Rate = clsCommon.myCdbl(dtTemp.Rows(0)("Loss_SNF_Rate"))
                                        Else
                                            SNF_Rate = clsCommon.myCdbl(dtTemp.Rows(0)("Gain_SNF_Rate"))
                                        End If
                                    End If
                                End If
                                clsCommon.AddColumnsForChange(coll, "Against_Own_BMC_Gain_Loss_Rate", AgainstDocCode, True)
                            Else
                                Dim dblRate As Decimal = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCDecimal(dr("MCCQty")), AgainstDocCode, clsCommon.myCDecimal(dr("MCCFAT")), clsCommon.myCDecimal(dr("MCCSNF")), objHead.MCC_CODE, "", "M", clsCommon.myCDate(dr("Document_Date")), trans, "M")
                                Dim dblAmt As Decimal = dblRate * clsCommon.myCDecimal(dr("MCCQty"))
                                qry = "select * from TSPL_MILK_PRICE_MASTER where Price_Code in (select Price_Chart_Code from TSPL_PRICE_CHART_PLANNING where Planning_Code='" & AgainstDocCode & "')"
                                Dim dtMilkPrice As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                                If dtMilkPrice Is Nothing OrElse dtMilkPrice.Rows.Count <= 0 Then
                                    Throw New Exception("Price Not found While Making Dr/Cr Note of Own DCS")
                                End If
                                Dim arr As clsFatSnfRateCalculator = clsFatSnfRateCalculator.CalculateFATSNFRatefromTransactionPer(clsCommon.myCDecimal(dr("MCCQty")), dblAmt, clsCommon.myCDecimal(dr("MCCFAT")), clsCommon.myCDecimal(dr("MCCSNF")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Ratio")), clsCommon.myCdbl(dtMilkPrice.Rows(0).Item("Snf_Ratio")))
                                FAT_Rate = arr.fatR
                                SNF_Rate = arr.snfR
                                clsCommon.AddColumnsForChange(coll, "Planning_Code", AgainstDocCode, True)
                            End If

                            clsCommon.AddColumnsForChange(coll, "FAT_Rate", FAT_Rate)
                            clsCommon.AddColumnsForChange(coll, "SNF_Rate", SNF_Rate)
                            clsCommon.AddColumnsForChange(coll, "DIFF_FAT_Amt", FAT_Rate * clsCommon.myCDecimal(dr("DiffFATKG")))
                            clsCommon.AddColumnsForChange(coll, "DIFF_SNF_Amt", SNF_Rate * clsCommon.myCDecimal(dr("DiffSNFKG")))

                            dclFATAmt = Math.Round(((dclFATApplicatblePer / 100) * (FAT_Rate * clsCommon.myCDecimal(dr("DiffFATKG")))), 2, MidpointRounding.AwayFromZero)
                            dclSNFAmt = Math.Round(((dclSNFApplicatblePer / 100) * (SNF_Rate * clsCommon.myCDecimal(dr("DiffSNFKG")))), 2, MidpointRounding.AwayFromZero)

                            clsCommon.AddColumnsForChange(coll, "FAT_Amt", dclFATAmt)
                            clsCommon.AddColumnsForChange(coll, "SNF_Amt", dclSNFAmt)
                            clsCommon.AddColumnsForChange(coll, "Amt", dclFATAmt + dclSNFAmt)

                            If clsCommon.myLen(AgainstDocCode) > 0 Then
                                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_OWN_BMC", OMInsertOrUpdate.Insert, "", trans)
                            End If
                        Next
                    End If


                    ''Now Create Dr/Cr Note
                    qry = "select sum(Amt) as NoteAmt  from TSPL_MILK_PURCHASE_INVOICE_OWN_BMC where InvoiceNo='" + objHead.DOC_CODE + "'"
                    dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
                    dblAmount = 0
                    If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                        If clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")) > 0 Then
#Region "CreateCreditNotForOWNBMC"
                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 0
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                            objVendorInvHead.Vendor_Invoice_No = ""
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "C"

                            objVendorInvHead.Description = "AP Credit Note For Own BMC"
                            objVendorInvHead.RefDocType = "OWD-CRD"

                            objVendorInvHead.RefDocNo = objHead.DOC_CODE
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                            If True Then
                                ''Set AP Invvoice Detail Table

                                Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Own_BMC_Excess=1", trans)
                                If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                    Throw New Exception("Please set default  Own BMC Excess in Deduction Master")
                                End If
                                If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                    Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                End If

                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                                dblAmount = Math.Abs(clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")))
                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount
                                ''End of Set AP Invvoice Detail Table

                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)

#End Region
                        ElseIf clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")) < 0 Then
#Region "CreateDebitNotForOWNBMC"
                            If True Then
                                Dim objVendorInvHead As New clsVedorInvoiceHead()
                                objVendorInvHead.isDeduction = 1
                                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                                objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                                objVendorInvHead.Invoice_Type = "AP"
                                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                                objVendorInvHead.Description = "AP Debit Note Against Own BMC"
                                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                                End If
                                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                                objVendorInvHead.RefDocType = "OWD-DBT"
                                objVendorInvHead.RefDocNo = objHead.DOC_CODE
                                objVendorInvHead.On_Hold = False
                                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                    End If
                                End If
                                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If
                                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                                Dim ii As Integer = 0
                                Dim isFirstTime As Boolean = True
                                objVendorInvHead.Total_Landed_Amt = 0
                                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                                dblAmount = Math.Abs(clsCommon.myCdbl(dtAmt.Rows(0)("NoteAmt")))
                                If True Then
                                    ''Set AP Invvoice Detail Table
                                    Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Own_BMC_Shortage=1", trans)
                                    If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                        Throw New Exception("Please set default  Own BMC Shortage in Deduction Master")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                        Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                    End If

                                    ii = ii + 1
                                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                    objVendorInvDetail.Detail_Line_No = ii
                                    objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                    objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                    objVendorInvDetail.Amount = dblAmount
                                    objVendorInvDetail.Discount_Per = 0
                                    objVendorInvDetail.Discount = 0
                                    objVendorInvDetail.Amount_less_Discount = dblAmount
                                    objVendorInvDetail.Total_Tax = 0
                                    objVendorInvDetail.Total_Amount = dblAmount
                                    objVendorInvDetail.Landed_Amount = dblAmount
                                    ''End of Set AP Invvoice Detail Table
                                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                                    End If

                                    ''Set AP Invvoice Header Table
                                    objVendorInvHead.Total_Landed_Amt += dblAmount
                                    objVendorInvHead.Discount_Base += dblAmount
                                    objVendorInvHead.Discount_Amount += 0
                                    objVendorInvHead.Amount_Less_Discount += dblAmount
                                    objVendorInvHead.Document_Total += dblAmount
                                    objVendorInvHead.Balance_Amt += dblAmount
                                    ''End of Set AP Invvoice Header Table

                                    objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                    If objVendorInvHead.Empty_Amount > 0 Then
                                        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                            Throw New Exception("Please set Inventory Control Empties")
                                        End If
                                        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                    End If
                                End If
                                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                    Throw New Exception("No GL Account Found For AP Invoice")
                                End If
                                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                            End If
#End Region
                        End If
                    End If
                End If


#Region "CreateDebitNoteforRejectInBMCTruckSheet"
                qry = "select Qty,(case when Qty<=0 then 0.0 else cast(FAT_KG*100/Qty as decimal(18,1)) end) as FATPer,(case when Qty<=0 then 0.0 else cast(SNF_KG*100/Qty as decimal(18,1)) end) as SNFPer  from (select isnull(sum(TSPL_MILK_SRN_DETAIL.Qty),0) as Qty
,isnull(sum(TSPL_MILK_SRN_DETAIL.FAT_KG),0) as FAT_KG,isnull(sum(TSPL_MILK_SRN_DETAIL.SNF_KG),0) as SNF_KG 
from TSPL_MILK_SRN_DETAIL
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
where TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and  TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.MCC_CODE='" + objHead.MCC_CODE + "')x"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                Dim AvgRate As Decimal = 0
                Dim AvgFAT As Decimal = clsCommon.myCDecimal(dt.Rows(0)("FATPer"))
                Dim AvgSNF As Decimal = clsCommon.myCDecimal(dt.Rows(0)("SNFPer"))
                Dim strPriceCode As String = ""
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim isPickCLRInsteadOfSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.MilkProcuremntPickCLRInsteadOfSNF, trans)) > 0)
                    Dim PickPriceFromFATAndSNF As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProcuremntPickCLRInsteadOfSNF, clsFixedParameterCode.PickPriceFromFATAndSNF, trans)) > 0)
                    If isPickCLRInsteadOfSNF Then
                        If PickPriceFromFATAndSNF Then
                            AvgRate = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCDecimal(dt.Rows(0)("Qty")), strPriceCode, AvgFAT, AvgSNF, objHead.MCC_CODE, strVLCCode, "M", objHead.DOC_DATE, trans, "M", 0, 0)
                        Else
                            AvgRate = clsEkoPro.getRateFromUploaderShiftWiseCLR(AvgFAT, AvgSNF, objHead.MCC_CODE, strVLCCode, "M", objHead.DOC_DATE, trans, "M", strPriceCode)
                            clsMilkSRNMCC.ObjList(0).Price_Code = strPriceCode
                        End If
                    Else
                        AvgRate = clsEkoPro.getRateAndPriceCodeFromUploaderShiftWise(clsCommon.myCDecimal(dt.Rows(0)("Qty")), strPriceCode, AvgFAT, AvgSNF, objHead.MCC_CODE, strVLCCode, "M", objHead.DOC_DATE, trans, "M", 0, 0)
                    End If
                End If

                qry = "select TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_REJECT_TYPE.Applicable_On,TSPL_MILK_REJECT_TYPE.Applicable_Per
from   TSPL_MILK_COLLECTION_MCC_DETAIL 
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
inner join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.Code=TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type
where isnull(TSPL_MILK_REJECT_TYPE.Applicable_On,0) in (0,1)
and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") + "' 
and convert(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") + "' 
and TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code='" + objHead.MCC_CODE + "'"
                dt = clsDBFuncationality.GetDataTable(qry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "InvoiceNo", objHead.DOC_CODE)
                        clsCommon.AddColumnsForChange(coll, "Doc_Date", clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Against_Milk_Collection_MCC_Detail", clsCommon.myCDecimal(dr("PK_Id")))
                        clsCommon.AddColumnsForChange(coll, "Milk_Type", clsCommon.myCstr(dr("Milk_Type")))
                        clsCommon.AddColumnsForChange(coll, "Applicable_On", clsCommon.myCDecimal(dr("Applicable_On")))
                        Dim dclBaseRate As Decimal = 0
                        Dim dclBaseValue As Decimal = 0
                        If clsCommon.myCDecimal(dr("Applicable_On")) = 0 Then ''%age
                            dclBaseRate = (100 - clsCommon.myCDecimal(dr("Applicable_Per"))) / 100
                            qry = "select sum(TSPL_MILK_SRN_DETAIL.AMOUNT) as AMOUNT
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_DCS_MCC_DETAIL on TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Against_Milk_Collection_MCC_Detail=TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id 
left outer join TSPL_MILK_COLLECTION_DCS on TSPL_MILK_COLLECTION_DCS.Document_No=TSPL_MILK_COLLECTION_DCS_MCC_DETAIL.Document_No
left outer join TSPL_MILK_COLLECTION_DCS_DETAIL on TSPL_MILK_COLLECTION_DCS_DETAIL.Document_No=TSPL_MILK_COLLECTION_DCS.Document_No
left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Against_Milk_Collection_DCS_Detail=TSPL_MILK_COLLECTION_DCS_DETAIL.PK_Id
left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.Against_Shift_Uploader_TR_No=TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No or TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No 
left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE
left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO
left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE 
where  TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id=" + clsCommon.myCstr(dr("PK_Id")) + ""
                            dclBaseValue = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
                        ElseIf clsCommon.myCDecimal(dr("Applicable_On")) = 1 Then ''Rate
                            clsCommon.AddColumnsForChange(coll, "Avg_FAT", AvgFAT)
                            clsCommon.AddColumnsForChange(coll, "Avg_SNF", AvgSNF)
                            clsCommon.AddColumnsForChange(coll, "Price_Code", strPriceCode)

                            dclBaseRate = (AvgRate - clsCommon.myCDecimal(dr("Applicable_Per")))
                            dclBaseValue = clsCommon.myCDecimal(dr("Qty"))
                        End If
                        clsCommon.AddColumnsForChange(coll, "Base_Rate", dclBaseRate)
                        If (dclBaseRate > 0) Then
                            clsCommon.AddColumnsForChange(coll, "Base_Value", dclBaseValue)
                            clsCommon.AddColumnsForChange(coll, "Amount", (dclBaseValue * dclBaseRate))
                        End If
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_PURCHASE_INVOICE_OWN_BMC_REJECT", OMInsertOrUpdate.Insert, "", trans)
                    Next
                End If


                ''Now Create Dr/Cr Note
                qry = "select Milk_Type, sum(Amount) as Amount from ( select Amount,Milk_Type from TSPL_MILK_PURCHASE_INVOICE_OWN_BMC_REJECT  where InvoiceNo='" + objHead.DOC_CODE + "')xxx group by Milk_Type"
                dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
                dblAmount = 0
                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                    For Each dr As DataRow In dtAmt.Rows
                        If clsCommon.myCdbl(dr("Amount")) > 0 Then
#Region "CreateDebitNotForOWNBMC"
                            If True Then
                                Dim objVendorInvHead As New clsVedorInvoiceHead()
                                objVendorInvHead.isDeduction = 1
                                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                                objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                                objVendorInvHead.Invoice_Type = "AP"
                                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                                objVendorInvHead.Description = "AP Debit Note Against Own BMC Milk Reject " + (dr("Milk_Type"))
                                objVendorInvHead.Remarks = clsCommon.myCstr(dr("Milk_Type"))
                                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                                End If
                                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                                objVendorInvHead.RefDocType = "OWD-RJM"
                                objVendorInvHead.RefDocNo = objHead.DOC_CODE
                                objVendorInvHead.On_Hold = False
                                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                    End If
                                End If
                                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If
                                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                                Dim ii As Integer = 0
                                Dim isFirstTime As Boolean = True
                                objVendorInvHead.Total_Landed_Amt = 0
                                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                                dblAmount = Math.Abs(clsCommon.myCdbl(dr("Amount")))
                                If True Then
                                    ''Set AP Invvoice Detail Table
                                    Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Own_BMC_Milk_Reject_Type='" + clsCommon.myCstr(dr("Milk_Type")) + "'", trans)
                                    If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                        Throw New Exception("Please make Deduction of Own BMC Milk Reject Type [ " + clsCommon.myCstr(dr("Milk_Type")) + " ]")
                                    End If
                                    If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                        Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                    End If

                                    ii = ii + 1
                                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                    objVendorInvDetail.Detail_Line_No = ii
                                    objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                    objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                    objVendorInvDetail.Amount = dblAmount
                                    objVendorInvDetail.Discount_Per = 0
                                    objVendorInvDetail.Discount = 0
                                    objVendorInvDetail.Amount_less_Discount = dblAmount
                                    objVendorInvDetail.Total_Tax = 0
                                    objVendorInvDetail.Total_Amount = dblAmount
                                    objVendorInvDetail.Landed_Amount = dblAmount
                                    ''End of Set AP Invvoice Detail Table
                                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                                    End If

                                    ''Set AP Invvoice Header Table
                                    objVendorInvHead.Total_Landed_Amt += dblAmount
                                    objVendorInvHead.Discount_Base += dblAmount
                                    objVendorInvHead.Discount_Amount += 0
                                    objVendorInvHead.Amount_Less_Discount += dblAmount
                                    objVendorInvHead.Document_Total += dblAmount
                                    objVendorInvHead.Balance_Amt += dblAmount
                                    ''End of Set AP Invvoice Header Table

                                    objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                    If objVendorInvHead.Empty_Amount > 0 Then
                                        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                            Throw New Exception("Please set Inventory Control Empties")
                                        End If
                                        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                    End If
                                End If
                                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                    Throw New Exception("No GL Account Found For AP Invoice")
                                End If
                                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                            End If
#End Region
                        End If
                    Next

                End If
#End Region


            End If

            qry = "select top 1 1 from TSPL_DCS_ADDITION_DEDUCTION where ISNULL(Inactive,0)=0"
            dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
            If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
#Region "Create DCS Addition/Deduction"
                qry = "insert into TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED (InvoiceNo,Against_DCS_ADDITION_DEDUCTION,SRN_CODE,Against_Milk_Collection_MCC_Detail,Amt)
select '" + objHead.DOC_CODE + "' as InvoiceNo,Code, DOC_CODE,null as Against_Milk_Collection_MCC_Detail,((((case when Applicable_On=0 then (case when Qty_UOM=2 then ACC_Qty else (case when Qty_UOM=1 then ACC_WEIGHT_LTR else Qty end) end) else AMOUNT end) * Applicable_Value) / (case when Applicable_Type=0 then 1 else 100 end ))*Conversion) as Amt from ( 
select  TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_DETAIL.Qty,TSPL_MILK_SRN_DETAIL.ACC_Qty,(case when len(isnull(TSPL_MILK_SRN_HEAD.Against_Reject_No,''))<=2 then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR else TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR end) as ACC_WEIGHT_LTR,TSPL_MILK_SRN_DETAIL.AMOUNT, TSPL_DCS_ADDITION_DEDUCTION.Code,TSPL_DCS_ADDITION_DEDUCTION.Applicable_On,TSPL_DCS_ADDITION_DEDUCTION.Qty_UOM,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value,TSPL_DCS_ADDITION_DEDUCTION.Conversion 
from TSPL_MILK_SRN_DETAIL 
inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE
left outer join  TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=TSPL_MILK_SRN_HEAD.SAMPLE_NO
left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_REJECT_DETAIL.SAMPLE_NO=TSPL_MILK_SRN_HEAD.SAMPLE_NO
inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE
inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
inner join TSPL_DCS_ADDITION_DEDUCTION on TSPL_MILK_SRN_HEAD.DOC_DATE>=TSPL_DCS_ADDITION_DEDUCTION.Start_Date
where TSPL_DCS_ADDITION_DEDUCTION.Posted=1 and 
isnull(TSPL_DCS_ADDITION_DEDUCTION.Inactive,0)=0
and (2= case when TSPL_DCS_ADDITION_DEDUCTION.End_Date is null then 2 else case when TSPL_MILK_SRN_HEAD.DOC_DATE<= TSPL_DCS_ADDITION_DEDUCTION.End_Date then 2 else 3 end end) 
and TSPL_MILK_SRN_DETAIL.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") 
and (2= case when ISNULL(TSPL_DCS_ADDITION_DEDUCTION.Check_Saving_AC,0)=0 then 2 else (case when TSPL_DCS_ADDITION_DEDUCTION.Check_Saving_AC=1 and len(isnull(TSPL_VENDOR_MASTER.AccNo2,''))>0 then 2 else (case when TSPL_DCS_ADDITION_DEDUCTION.Check_Saving_AC=2 and len(isnull(TSPL_VENDOR_MASTER.AccNo2,''))<=0 then 2 else 3 end ) end ) end )
and (2= case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=0 then 2 else 
(case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=1 and TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER='Registered' then 2 else 
(case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=2 and TSPL_VENDOR_MASTER.Registered_PDCS_CLUSTER='PDCS' then 2  else 
(case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=3 and isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=TSPL_MILK_SRN_HEAD.MCC_CODE  then 2  else 
(case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=4 and isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=0  then 2  else 1 
end) end) end) end) end )
and TSPL_DCS_ADDITION_DEDUCTION.Milk_Type like '%'''+isnull(TSPL_MILK_REJECT_DETAIL.Reject_Type,'Good')+'''%' 
)x"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)



                qry = "insert into TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED (InvoiceNo,Against_DCS_ADDITION_DEDUCTION,SRN_CODE,Against_Milk_Collection_MCC_Detail,Amt)
select '" + objHead.DOC_CODE + "' as InvoiceNo,Code,null as SRN_CODE,PK_Id,((((case when Applicable_On=0 then Qty else AMOUNT end) * Applicable_Value) / (case when Applicable_Type=0 then 1 else 100 end ))*Conversion) as Amt from (
select  TSPL_MILK_COLLECTION_MCC_DETAIL.PK_Id,TSPL_MILK_COLLECTION_MCC.Document_Date,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,0 as AMOUNT, TSPL_DCS_ADDITION_DEDUCTION.Code,TSPL_DCS_ADDITION_DEDUCTION.Applicable_On,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value ,TSPL_DCS_ADDITION_DEDUCTION.Conversion 
from TSPL_MILK_COLLECTION_MCC_DETAIL
left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.MCC=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code and isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1
inner join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
inner join TSPL_DCS_ADDITION_DEDUCTION on CONVERT(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)>=TSPL_DCS_ADDITION_DEDUCTION.Start_Date
where TSPL_DCS_ADDITION_DEDUCTION.Posted=1 and 
isnull(TSPL_DCS_ADDITION_DEDUCTION.Inactive,0)=0
and (2= case when ISNULL(TSPL_DCS_ADDITION_DEDUCTION.Check_Saving_AC,0)=0 then 2 else (case when TSPL_DCS_ADDITION_DEDUCTION.Check_Saving_AC=1 and len(isnull(TSPL_VENDOR_MASTER.AccNo2,''))>0 then 2 else (case when TSPL_DCS_ADDITION_DEDUCTION.Check_Saving_AC=2 and len(isnull(TSPL_VENDOR_MASTER.AccNo2,''))<=0 then 2 else 3 end ) end ) end )
and (2= case when TSPL_DCS_ADDITION_DEDUCTION.End_Date is null then 2 else case when CONVERT(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)<= TSPL_DCS_ADDITION_DEDUCTION.End_Date then 2 else 3 end end) 
and CONVERT(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)>='" + clsCommon.GetPrintDate(FromDate, "dd/MMM/yyyy") + "' and CONVERT(date, TSPL_MILK_COLLECTION_MCC.Document_Date,103)<='" + clsCommon.GetPrintDate(ToDate, "dd/MMM/yyyy") + "' 
and TSPL_VLC_MASTER_HEAD.VLC_Code='" + strVLCCode + "'
and (2=(case when TSPL_DCS_ADDITION_DEDUCTION.Applicable_DCS_Type=5 and isnull(TSPL_VLC_MASTER_HEAD.isOwnBMC,0)=1 and TSPL_VLC_MASTER_HEAD.MCC=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code  then 2  else 1 end))
and TSPL_DCS_ADDITION_DEDUCTION.Milk_Type like '%'''+trim(isnull(TSPL_MILK_COLLECTION_MCC_DETAIL.Milk_Type,'Good'))+'''%'
)x"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                ''Now Create Dr/Cr Note
                qry = "select xxx.Against_DCS_ADDITION_DEDUCTION,xxx.Amt,TSPL_DCS_ADDITION_DEDUCTION.Apply_TDS,TSPL_DCS_ADDITION_DEDUCTION.Nature_Type,TSPL_DCS_ADDITION_DEDUCTION.GL_Account,TSPL_DCS_ADDITION_DEDUCTION.Saving,TSPL_DCS_ADDITION_DEDUCTION.Mapping_Matching,TSPL_DCS_ADDITION_DEDUCTION.RO_Decimal_Places,TSPL_DCS_ADDITION_DEDUCTION.RO_Increase_After
,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Value,TSPL_DCS_ADDITION_DEDUCTION.Applicable_Type,TSPL_DCS_ADDITION_DEDUCTION.Applicable_On
,(select top 1 Add_Of_Add_Ded_Code from TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT where TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT.code=TSPL_DCS_ADDITION_DEDUCTION.Code) as Add_Of_Add_Ded_Code,TSPL_DCS_ADDITION_DEDUCTION.Include_Shortage_Own_BMC,TSPL_DCS_ADDITION_DEDUCTION.Subtract from 
(select Against_DCS_ADDITION_DEDUCTION, sum(Amt) as Amt  from TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED where InvoiceNo='" + objHead.DOC_CODE + "' group by Against_DCS_ADDITION_DEDUCTION) 
xxx Left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=xxx.Against_DCS_ADDITION_DEDUCTION order by Mapping_Matching,Amt desc"
                dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
                If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                    dblAmount = 0
                    qry = "select TSPL_DCS_ADDITION_DEDUCTION.Mapping_Matching, sum(Amt) as Amt,max(TSPL_DCS_ADDITION_DEDUCTION.RO_Decimal_Places) as RO_Decimal_Places,max(TSPL_DCS_ADDITION_DEDUCTION.RO_Increase_After) as RO_Increase_After  from TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED Left outer join TSPL_DCS_ADDITION_DEDUCTION on TSPL_DCS_ADDITION_DEDUCTION.Code=TSPL_MILK_PURCHASE_INVOICE_DCS_ADD_DED.Against_DCS_ADDITION_DEDUCTION  where InvoiceNo='" + objHead.DOC_CODE + "' group by TSPL_DCS_ADDITION_DEDUCTION.Mapping_Matching having sum(Amt)>0 and len(isnull( TSPL_DCS_ADDITION_DEDUCTION.Mapping_Matching,''))>0 "
                    Dim dtMappingMatching As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtMappingMatching IsNot Nothing AndAlso dtMappingMatching.Rows.Count > 0 Then
                        For Each drAmt As DataRow In dtMappingMatching.Rows
                            dblAmount = clsCommon.myRoundOFF(Math.Abs(clsCommon.myCDecimal(drAmt("Amt"))), IIf(clsCommon.myCDecimal(drAmt("RO_Decimal_Places")) >= 0, clsCommon.myCDecimal(drAmt("RO_Decimal_Places")), objCommonVar.DCSAddDedRODecimalPlace), IIf(clsCommon.myCDecimal(drAmt("RO_Increase_After")) >= 0, clsCommon.myCDecimal(drAmt("RO_Increase_After")), objCommonVar.DCSAddDedROIncreaseAfter))
                            Dim dblDetAmt As Decimal = 0
                            Dim indexFirst As Integer = -1
                            For index As Integer = 0 To dtAmt.Rows.Count - 1
                                If clsCommon.CompairString(clsCommon.myCstr(drAmt("Mapping_Matching")), clsCommon.myCstr(dtAmt.Rows(index)("Mapping_Matching"))) = CompairStringResult.Equal Then
                                    If indexFirst < 0 Then
                                        indexFirst = index
                                    End If
                                    dblDetAmt += clsCommon.myRoundOFF(Math.Abs(clsCommon.myCDecimal(dtAmt.Rows(index)("Amt"))), objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                                End If
                            Next
                            If indexFirst >= 0 Then
                                dtAmt.Rows(indexFirst)("Amt") += (dblAmount - dblDetAmt)
                            End If
                        Next
                    End If

                    dtAmt.DefaultView.Sort = "Add_Of_Add_Ded_Code"
                    For Each drAmt As DataRow In dtAmt.DefaultView.ToTable().Rows
                        dblAmount = clsCommon.myRoundOFF(Math.Abs(clsCommon.myCDecimal(drAmt("Amt"))), IIf(clsCommon.myCDecimal(drAmt("RO_Decimal_Places")) >= 0, clsCommon.myCDecimal(drAmt("RO_Decimal_Places")), objCommonVar.DCSAddDedRODecimalPlace), IIf(clsCommon.myCDecimal(drAmt("RO_Increase_After")) >= 0, clsCommon.myCDecimal(drAmt("RO_Increase_After")), objCommonVar.DCSAddDedROIncreaseAfter))
                        If clsCommon.myCdbl(drAmt("Include_Shortage_Own_BMC")) = 1 Then
                            If clsfrmVLCMaster.IsOwnBMC(strVLCCode, objHead.MCC_CODE, trans) Then
                                Dim arrMCC As New ArrayList
                                arrMCC.Add(objHead.MCC_CODE)
                                BaseQry = clsMilkCollectionDCS.GetBaseQueryFATSNFGainLoss(FromDate, ToDate, arrMCC)
                                qry = "select sum(Amt) as Amt from ( " + BaseQry + ")XX group by MCC_Code"
                                Dim dclGainLossAmt As Decimal = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
                                dclGainLossAmt = (clsCommon.myCDivide(dclGainLossAmt * clsCommon.myCDecimal(drAmt("Applicable_Value")), IIf(clsCommon.myCDecimal(drAmt("Applicable_Type")) = 0, 1, 100)))
                                dclGainLossAmt = clsCommon.myRoundOFF(dclGainLossAmt, IIf(clsCommon.myCDecimal(drAmt("RO_Decimal_Places")) >= 0, clsCommon.myCDecimal(drAmt("RO_Decimal_Places")), objCommonVar.DCSAddDedRODecimalPlace), IIf(clsCommon.myCDecimal(drAmt("RO_Increase_After")) >= 0, clsCommon.myCDecimal(drAmt("RO_Increase_After")), objCommonVar.DCSAddDedROIncreaseAfter))
                                dblAmount += dclGainLossAmt
                            End If
                        End If

                        If clsCommon.myLen(drAmt("Add_Of_Add_Ded_Code")) > 0 Then
                            Dim dclAddBaseAmt As Decimal = 0
                            qry = "select Add_Of_Add_Ded_Code from TSPL_DCS_ADDITION_DEDUCTION_ADD_AMT where Code='" + clsCommon.myCstr(drAmt("Against_DCS_ADDITION_DEDUCTION")) + "'"
                            Dim dtAdd As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtAdd IsNot Nothing AndAlso dtAdd.Rows.Count > 0 Then
                                For Each drAdd As DataRow In dtAdd.Rows
                                    qry = "select Amount from TSPL_VENDOR_INVOICE_DETAIL
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No
where TSPL_VENDOR_INVOICE_HEAD.RefDocType  in ('DCS-ADD','DCS-DED') and TSPL_VENDOR_INVOICE_HEAD.RefDocNo='" + objHead.DOC_CODE + "' and TSPL_VENDOR_INVOICE_DETAIL.DCS_Addition_Deduction='" + clsCommon.myCstr(drAdd("Add_Of_Add_Ded_Code")) + "'"
                                    dclAddBaseAmt += clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
                                Next
                                If clsCommon.myLen(drAmt("Include_Shortage_Own_BMC")) = 0 Then
                                    dclAddBaseAmt = (clsCommon.myCDivide(dclAddBaseAmt * clsCommon.myCDecimal(drAmt("Applicable_Value")), IIf(clsCommon.myCDecimal(drAmt("Applicable_Type")) = 0, 1, 100)))
                                    dclAddBaseAmt = clsCommon.myRoundOFF(dclAddBaseAmt, IIf(clsCommon.myCDecimal(drAmt("RO_Decimal_Places")) >= 0, clsCommon.myCDecimal(drAmt("RO_Decimal_Places")), objCommonVar.DCSAddDedRODecimalPlace), IIf(clsCommon.myCDecimal(drAmt("RO_Increase_After")) >= 0, clsCommon.myCDecimal(drAmt("RO_Increase_After")), objCommonVar.DCSAddDedROIncreaseAfter))
                                End If
                                If clsCommon.myCDecimal(drAmt("Subtract")) = 1 Then
                                    dblAmount -= dclAddBaseAmt
                                Else
                                    dblAmount += dclAddBaseAmt
                                End If
                            End If
                        End If

                        If clsCommon.myCdbl(drAmt("Nature_Type")) = 0 Then
#Region "CreateCreditNotForDCSAddition"
                            Dim objVendorInvHead As New clsVedorInvoiceHead()
                            objVendorInvHead.isDeduction = 0
                            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                            objVendorInvHead.Vendor_Invoice_No = ""
                            objVendorInvHead.Invoice_Type = "AP"
                            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                            objVendorInvHead.Description = "AP Credit Note For DCS Addition"
                            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                            End If
                            objVendorInvHead.Document_Type = "C"
                            objVendorInvHead.RefDocType = "DCS-ADD"
                            objVendorInvHead.RefDocNo = objHead.DOC_CODE
                            objVendorInvHead.On_Hold = False
                            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                            dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                End If
                            End If
                            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                Throw New Exception("Please set the vendor payable Account")
                            End If
                            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                            Dim ii As Integer = 0
                            Dim isFirstTime As Boolean = True
                            objVendorInvHead.Total_Landed_Amt = 0
                            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                            objVendorInvHead.Saving = clsCommon.myCDecimal(drAmt("Saving"))
                            If True Then
                                ''Set AP Invvoice Detail Table
                                ii = ii + 1
                                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                objVendorInvDetail.Detail_Line_No = ii
                                objVendorInvDetail.DCS_Addition_Deduction = clsCommon.myCstr(drAmt("Against_DCS_ADDITION_DEDUCTION"))
                                objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(drAmt("GL_Account"))
                                objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)



                                objVendorInvDetail.Amount = dblAmount
                                objVendorInvDetail.Discount_Per = 0
                                objVendorInvDetail.Discount = 0
                                objVendorInvDetail.Amount_less_Discount = dblAmount
                                objVendorInvDetail.Total_Tax = 0
                                objVendorInvDetail.Total_Amount = dblAmount
                                objVendorInvDetail.Landed_Amount = dblAmount

                                ''End of Set AP Invvoice Detail Table

                                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                                End If

                                ''Set AP Invvoice Header Table
                                objVendorInvHead.Total_Landed_Amt += dblAmount
                                objVendorInvHead.Discount_Base += dblAmount
                                objVendorInvHead.Discount_Amount += 0
                                objVendorInvHead.Amount_Less_Discount += dblAmount
                                objVendorInvHead.Document_Total += dblAmount
                                objVendorInvHead.Balance_Amt += dblAmount
                                ''End of Set AP Invvoice Header Table
                            End If
                            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                Throw New Exception("No GL Account Found For AP Invoice")
                            End If
                            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                            objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                            If clsCommon.myCdbl(drAmt("Apply_TDS")) = 1 Then
                                Dim dblPreviousTDSAmt As Decimal = 0
                                Dim objRemittance As clsRemittance = SetVendorTDSDetails(True, objVendorInvHead.Document_No, objHead.DOC_DATE, dblPreviousTDSAmt, objVendorInvHead.Vendor_Code, objVendorInvHead.Document_Total, objVendorInvHead.Document_Total, trans)
                                objVendorInvHead.RemittanceObject = objRemittance
                            End If

                            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
#End Region
                        ElseIf clsCommon.myCdbl(drAmt("Nature_Type")) = 1 Then
#Region "CreateDebitNotForDCSDeduction"
                            If True Then
                                Dim objVendorInvHead As New clsVedorInvoiceHead()
                                objVendorInvHead.isDeduction = 1
                                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                                objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                                objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                                objVendorInvHead.Invoice_Type = "AP"
                                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                                objVendorInvHead.Description = "AP Debit Note Against DCS Deduction"
                                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                                End If
                                objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                                objVendorInvHead.RefDocType = "DCS-DED"
                                objVendorInvHead.RefDocNo = objHead.DOC_CODE
                                objVendorInvHead.On_Hold = False
                                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                    End If
                                End If
                                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If
                                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                                objVendorInvHead.Saving = 0
                                Dim ii As Integer = 0
                                Dim isFirstTime As Boolean = True
                                objVendorInvHead.Total_Landed_Amt = 0
                                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()


                                If True Then
                                    ii = ii + 1
                                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                    objVendorInvDetail.Detail_Line_No = ii
                                    objVendorInvDetail.DCS_Addition_Deduction = clsCommon.myCstr(drAmt("Against_DCS_ADDITION_DEDUCTION"))
                                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(drAmt("GL_Account"))
                                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)
                                    objVendorInvDetail.Amount = dblAmount
                                    objVendorInvDetail.Discount_Per = 0
                                    objVendorInvDetail.Discount = 0
                                    objVendorInvDetail.Amount_less_Discount = dblAmount
                                    objVendorInvDetail.Total_Tax = 0
                                    objVendorInvDetail.Total_Amount = dblAmount
                                    objVendorInvDetail.Landed_Amount = dblAmount
                                    ''End of Set AP Invvoice Detail Table
                                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                                    End If

                                    ''Set AP Invvoice Header Table
                                    objVendorInvHead.Total_Landed_Amt += dblAmount
                                    objVendorInvHead.Discount_Base += dblAmount
                                    objVendorInvHead.Discount_Amount += 0
                                    objVendorInvHead.Amount_Less_Discount += dblAmount
                                    objVendorInvHead.Document_Total += dblAmount
                                    objVendorInvHead.Balance_Amt += dblAmount
                                    ''End of Set AP Invvoice Header Table

                                    objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                    If objVendorInvHead.Empty_Amount > 0 Then
                                        If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                            Throw New Exception("Please set Inventory Control Empties")
                                        End If
                                        objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                    End If
                                End If
                                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                    Throw New Exception("No GL Account Found For AP Invoice")
                                End If
                                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                            End If
#End Region
                        End If
                    Next

                End If

#End Region
            End If


#Region "CreateCreditNotForQAT"
            qry = "select  sum(QAT_Amt) as Amt  from TSPL_MILK_SRN_DETAIL where TSPL_MILK_SRN_DETAIL.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") "
            dblAmount = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
            If dblAmount > 0 Then
                dblAmount = clsCommon.myRoundOFF(dblAmount, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                objVendorInvHead.isDeduction = 0
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                objVendorInvHead.Vendor_Invoice_No = ""
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                objVendorInvHead.Description = "AP Credit Note For DCS QAT"
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                End If
                objVendorInvHead.Document_Type = "C"
                objVendorInvHead.RefDocType = "DCS-QAT"
                objVendorInvHead.RefDocNo = objHead.DOC_CODE
                objVendorInvHead.On_Hold = False
                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT,Incentive_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If
                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True
                objVendorInvHead.Total_Landed_Amt = 0
                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                If True Then
                    ''Set AP Invvoice Detail Table
                    ii = ii + 1
                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                    objVendorInvDetail.Detail_Line_No = ii
                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCOUNT"))
                    If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                        Throw New Exception("Please set Incentive Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
                    End If
                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                    objVendorInvDetail.Amount = dblAmount
                    objVendorInvDetail.Discount_Per = 0
                    objVendorInvDetail.Discount = 0
                    objVendorInvDetail.Amount_less_Discount = dblAmount
                    objVendorInvDetail.Total_Tax = 0
                    objVendorInvDetail.Total_Amount = dblAmount
                    objVendorInvDetail.Landed_Amount = dblAmount

                    ''End of Set AP Invvoice Detail Table

                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If

                    ''Set AP Invvoice Header Table
                    objVendorInvHead.Total_Landed_Amt += dblAmount
                    objVendorInvHead.Discount_Base += dblAmount
                    objVendorInvHead.Discount_Amount += 0
                    objVendorInvHead.Amount_Less_Discount += dblAmount
                    objVendorInvHead.Document_Total += dblAmount
                    objVendorInvHead.Balance_Amt += dblAmount
                    ''End of Set AP Invvoice Header Table
                End If
                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
            End If
#End Region

#Region "CreateCreditNotForLoyalty"
            qry = "select  (max(isnull(TSPL_VLC_MASTER_HEAD.Loyalty_Rate,0))*sum(TSPL_MILK_SRN_DETAIL.Qty)) as Amt  
from TSPL_MILK_SRN_DETAIL  
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE= TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE
where TSPL_MILK_SRN_DETAIL.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ")  and  isnull(TSPL_VLC_MASTER_HEAD.Loyalty_Rate,0)>0"
            dblAmount = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry, trans))
            If dblAmount > 0 Then
                dblAmount = clsCommon.myRoundOFF(dblAmount, objCommonVar.DCSAddDedRODecimalPlace, objCommonVar.DCSAddDedROIncreaseAfter)
                Dim objVendorInvHead As New clsVedorInvoiceHead()
                objVendorInvHead.isDeduction = 0
                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                objVendorInvHead.Vendor_Invoice_No = ""
                objVendorInvHead.Invoice_Type = "AP"
                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                objVendorInvHead.Description = "AP Credit Note For DCS Loyalty"
                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                End If
                objVendorInvHead.Document_Type = "C"
                objVendorInvHead.RefDocType = "DCS-LYT"
                objVendorInvHead.RefDocNo = objHead.DOC_CODE
                objVendorInvHead.On_Hold = False
                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,PRO_DATA_ACCOUNT,Incentive_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                    End If
                End If
                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                    Throw New Exception("Please set the vendor payable Account")
                End If
                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                Dim ii As Integer = 0
                Dim isFirstTime As Boolean = True
                objVendorInvHead.Total_Landed_Amt = 0
                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()
                If True Then
                    ''Set AP Invvoice Detail Table
                    ii = ii + 1
                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                    objVendorInvDetail.Detail_Line_No = ii
                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Incentive_ACCOUNT"))
                    If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                        Throw New Exception("Please set Incentive Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
                    End If
                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                    objVendorInvDetail.Amount = dblAmount
                    objVendorInvDetail.Discount_Per = 0
                    objVendorInvDetail.Discount = 0
                    objVendorInvDetail.Amount_less_Discount = dblAmount
                    objVendorInvDetail.Total_Tax = 0
                    objVendorInvDetail.Total_Amount = dblAmount
                    objVendorInvDetail.Landed_Amount = dblAmount

                    ''End of Set AP Invvoice Detail Table

                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                    End If

                    ''Set AP Invvoice Header Table
                    objVendorInvHead.Total_Landed_Amt += dblAmount
                    objVendorInvHead.Discount_Base += dblAmount
                    objVendorInvHead.Discount_Amount += 0
                    objVendorInvHead.Amount_Less_Discount += dblAmount
                    objVendorInvHead.Document_Total += dblAmount
                    objVendorInvHead.Balance_Amt += dblAmount
                    ''End of Set AP Invvoice Header Table
                End If
                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                    Throw New Exception("No GL Account Found For AP Invoice")
                End If
                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
            End If
#End Region

#Region "Debit Note for Negative Amount"
            qry = "select  sum(isnull(TSPL_MILK_SRN_DETAIL.Negative_Amount,0)) as Negative_Amount 
from TSPL_MILK_SRN_DETAIL 
where TSPL_MILK_SRN_DETAIL.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") "
            dtAmt = clsDBFuncationality.GetDataTable(qry, trans)
            If clsCommon.myCdbl(dtAmt.Rows(0)("Negative_Amount")) > 0 Then
                If True Then
                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                    objVendorInvHead.isDeduction = 1
                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                    objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                    objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                    objVendorInvHead.Invoice_Type = "AP"
                    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                    objVendorInvHead.Description = "AP Debit Note Against VSP Negative Amount"
                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                    End If
                    objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                    objVendorInvHead.RefDocType = "VSP-NGT"
                    objVendorInvHead.RefDocNo = objHead.DOC_CODE
                    objVendorInvHead.On_Hold = False
                    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                    dt = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                        End If
                    End If
                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                        Throw New Exception("Please set the vendor payable Account")
                    End If
                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                    Dim ii As Integer = 0
                    Dim isFirstTime As Boolean = True
                    objVendorInvHead.Total_Landed_Amt = 0
                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                    If True Then
                        ''Set AP Invvoice Detail Table
                        Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Negative_SRN=1", trans)
                        If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                            Throw New Exception("Please set default Negative SRN in Deduction Master")
                        End If
                        If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                            Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                        End If

                        ii = ii + 1
                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                        objVendorInvDetail.Detail_Line_No = ii
                        objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                        objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                        objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                        objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                        objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                        dblAmount = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("Negative_Amount")), 0)
                        objVendorInvDetail.Amount = dblAmount
                        objVendorInvDetail.Discount_Per = 0
                        objVendorInvDetail.Discount = 0
                        objVendorInvDetail.Amount_less_Discount = dblAmount
                        objVendorInvDetail.Total_Tax = 0
                        objVendorInvDetail.Total_Amount = dblAmount
                        objVendorInvDetail.Landed_Amount = dblAmount
                        ''End of Set AP Invvoice Detail Table
                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                            objVendorInvHead.Arr.Add(objVendorInvDetail)
                        End If

                        ''Set AP Invvoice Header Table
                        objVendorInvHead.Total_Landed_Amt += dblAmount
                        objVendorInvHead.Discount_Base += dblAmount
                        objVendorInvHead.Discount_Amount += 0
                        objVendorInvHead.Amount_Less_Discount += dblAmount
                        objVendorInvHead.Document_Total += dblAmount
                        objVendorInvHead.Balance_Amt += dblAmount
                        ''End of Set AP Invvoice Header Table

                        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                        If objVendorInvHead.Empty_Amount > 0 Then
                            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                Throw New Exception("Please set Inventory Control Empties")
                            End If
                            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                        End If
                    End If
                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                        Throw New Exception("No GL Account Found For AP Invoice")
                    End If
                    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                    objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                End If
            End If
#End Region
        End If
    End Sub
    Sub VSPCommissionAndPashuVikashKosh(ByVal FromDate As Date, ByVal ToDate As Date, ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal strSRN_No As List(Of String), ByVal trans As SqlTransaction)
        If strSRN_No IsNot Nothing AndAlso strSRN_No.Count > 0 Then
            Dim qry As String = "select Commission_Rate,Deduction_Rate from TSPL_MCC_MASTER where MCC_Code='" + objHead.MCC_CODE + "'"
            Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0 Then
                If clsCommon.myCdbl(dtMCC.Rows(0)("Commission_Rate")) > 0 OrElse clsCommon.myCdbl(dtMCC.Rows(0)("Deduction_Rate")) > 0 Then
                    qry = "select sum(TSPL_MILK_SRN_DETAIL.AMOUNT) as AMOUNT,sum(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR) as ACC_WEIGHT_LTR from  TSPL_MILK_SRN_DETAIL 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE 
left outer join TSPL_MILK_SAMPLE_DETAIL on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE and TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO=TSPL_MILK_SRN_HEAD.SAMPLE_NO
left outer join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_HEAD.DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE
left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE and TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO
where TSPL_MILK_SRN_HEAD.DOC_CODE in (" + clsCommon.GetMulcallString(strSRN_No) + ") "
                    Dim dtAmt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtAmt IsNot Nothing AndAlso dtAmt.Rows.Count > 0 Then
                        Dim settDCSAddDedRODecimalPlance As Integer = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VSPBillDocumentToBeAddedInMilkCost, clsFixedParameterCode.VSPBillDocumentToBeAddedInMilkCost, trans)) > 0)

                        If clsCommon.myCdbl(dtMCC.Rows(0)("Commission_Rate")) > 0 Then
                            If clsCommon.myCdbl(dtAmt.Rows(0)("AMOUNT")) > 0 Then
#Region "CreateCreditNotForCommision"
                                Dim objVendorInvHead As New clsVedorInvoiceHead()
                                objVendorInvHead.isDeduction = 0
                                objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                                objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                                objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                                objVendorInvHead.Vendor_Invoice_No = ""
                                objVendorInvHead.Invoice_Type = "AP"
                                objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans)
                                objVendorInvHead.Description = "AP Credit Note For VSP Commission"
                                objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                                If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                    Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                                End If
                                objVendorInvHead.Document_Type = "C"
                                objVendorInvHead.RefDocType = "VSP-CMP"
                                objVendorInvHead.RefDocNo = objHead.DOC_CODE
                                objVendorInvHead.On_Hold = False
                                objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT,Commission_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                    objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                    objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                    If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                        objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                        objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                    End If
                                End If
                                If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                    Throw New Exception("Please set the vendor payable Account")
                                End If
                                objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                                Dim ii As Integer = 0
                                Dim isFirstTime As Boolean = True
                                objVendorInvHead.Total_Landed_Amt = 0
                                objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()


                                If True Then
                                    ''Set AP Invvoice Detail Table
                                    ii = ii + 1
                                    Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                    objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dt.Rows(0)("Commission_ACCOUNT"))
                                    If clsCommon.myLen(objVendorInvDetail.GL_Account_Code) <= 0 Then
                                        Throw New Exception("Please set Commission Account for Vendor Account set  [" + objVendorInvHead.Account_Set + "]")
                                    End If
                                    objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                    objVendorInvDetail.Detail_Line_No = ii
                                    objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)


                                    Dim dblAmount As Decimal = clsERPFuncationality.myFloor((clsCommon.myCdbl(dtAmt.Rows(0)("AMOUNT")) * clsCommon.myCdbl(dtMCC.Rows(0)("Commission_Rate"))) / 100, 0)
                                    objVendorInvDetail.Amount = dblAmount
                                    objVendorInvDetail.Discount_Per = 0
                                    objVendorInvDetail.Discount = 0
                                    objVendorInvDetail.Amount_less_Discount = dblAmount
                                    objVendorInvDetail.Total_Tax = 0
                                    objVendorInvDetail.Total_Amount = dblAmount
                                    objVendorInvDetail.Landed_Amount = dblAmount
                                    ''End of Set AP Invvoice Detail Table

                                    If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                        objVendorInvHead.Arr.Add(objVendorInvDetail)
                                    End If

                                    ''Set AP Invvoice Header Table
                                    objVendorInvHead.Total_Landed_Amt += dblAmount
                                    objVendorInvHead.Discount_Base += dblAmount
                                    objVendorInvHead.Discount_Amount += 0
                                    objVendorInvHead.Amount_Less_Discount += dblAmount
                                    objVendorInvHead.Document_Total += dblAmount
                                    objVendorInvHead.Balance_Amt += dblAmount
                                    ''End of Set AP Invvoice Header Table
                                End If
                                If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                    Throw New Exception("No GL Account Found For AP Invoice")
                                End If
                                objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                                objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)

                                objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                                clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
#End Region
                            End If
                        End If
                        If clsCommon.myCdbl(dtMCC.Rows(0)("Deduction_Rate")) > 0 Then
                            If clsCommon.myCdbl(dtAmt.Rows(0)("ACC_WEIGHT_LTR")) > 0 Then
#Region "CreateDebitNotForPashuVikashKos"
                                If True Then
                                    Dim objVendorInvHead As New clsVedorInvoiceHead()
                                    objVendorInvHead.isDeduction = 1
                                    objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
                                    objVendorInvHead.Vendor_Code = objHead.VSP_CODE
                                    objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
                                    objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
                                    objVendorInvHead.Invoice_Type = "AP"
                                    objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
                                    objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
                                    objVendorInvHead.Description = "AP Debit Note Against Pashu Vikash Kos"
                                    objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
                                    If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                                        Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
                                    End If
                                    objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
                                    objVendorInvHead.RefDocType = "VSP-PVK"
                                    objVendorInvHead.RefDocNo = objHead.DOC_CODE
                                    objVendorInvHead.On_Hold = False
                                    objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date
                                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
                                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                                        objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                                        objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                                        If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                                            objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                                            objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                                        End If
                                    End If
                                    If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                                        Throw New Exception("Please set the vendor payable Account")
                                    End If
                                    objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
                                    Dim ii As Integer = 0
                                    Dim isFirstTime As Boolean = True
                                    objVendorInvHead.Total_Landed_Amt = 0
                                    objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

                                    If True Then
                                        ''Set AP Invvoice Detail Table
                                        Dim dtDed As DataTable = clsDBFuncationality.GetDataTable("select code,GL_Account_Code from TSPL_DEDUCTION_MASTER  where Is_Default_Pashu_Vikash_Kos=1", trans)
                                        If dtDed Is Nothing OrElse dtDed.Rows.Count <= 0 Then
                                            Throw New Exception("Please set default Pashu Vikash Kos in Deduction Master")
                                        End If
                                        If clsCommon.myLen(clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))) <= 0 Then
                                            Throw New Exception("Please set GL Account for deduction [" + clsCommon.myCstr(dtDed.Rows(0)("code")) + "]")
                                        End If

                                        ii = ii + 1
                                        Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                                        objVendorInvDetail.Detail_Line_No = ii
                                        objVendorInvDetail.DeductionCode = clsCommon.myCstr(dtDed.Rows(0)("code"))
                                        objVendorInvDetail.DeductionDesc = ClsDeductionMaster.GetName(objVendorInvDetail.DeductionCode, trans)
                                        objVendorInvDetail.GL_Account_Code = clsCommon.myCstr(dtDed.Rows(0)("GL_Account_Code"))
                                        objVendorInvDetail.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvDetail.GL_Account_Code, objHead.MCC_CODE, trans)
                                        objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objVendorInvDetail.GL_Account_Code, trans)

                                        Dim dblAmount As Decimal = Math.Round(clsCommon.myCdbl(dtAmt.Rows(0)("ACC_WEIGHT_LTR")) * clsCommon.myCdbl(dtMCC.Rows(0)("Deduction_Rate")), 0)
                                        objVendorInvDetail.Amount = dblAmount
                                        objVendorInvDetail.Discount_Per = 0
                                        objVendorInvDetail.Discount = 0
                                        objVendorInvDetail.Amount_less_Discount = dblAmount
                                        objVendorInvDetail.Total_Tax = 0
                                        objVendorInvDetail.Total_Amount = dblAmount
                                        objVendorInvDetail.Landed_Amount = dblAmount
                                        ''End of Set AP Invvoice Detail Table
                                        If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                                            objVendorInvHead.Arr.Add(objVendorInvDetail)
                                        End If

                                        ''Set AP Invvoice Header Table
                                        objVendorInvHead.Total_Landed_Amt += dblAmount
                                        objVendorInvHead.Discount_Base += dblAmount
                                        objVendorInvHead.Discount_Amount += 0
                                        objVendorInvHead.Amount_Less_Discount += dblAmount
                                        objVendorInvHead.Document_Total += dblAmount
                                        objVendorInvHead.Balance_Amt += dblAmount
                                        ''End of Set AP Invvoice Header Table

                                        objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                                        If objVendorInvHead.Empty_Amount > 0 Then
                                            If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                                                Throw New Exception("Please set Inventory Control Empties")
                                            End If
                                            objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                                        End If
                                    End If
                                    If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                                        Throw New Exception("No GL Account Found For AP Invoice")
                                    End If
                                    objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
                                    objVendorInvHead.Main_VSP_Milk_AP_Invoice_No = clsVedorInvoiceHead.GetMainVSPMilkAPInvoiceNo(ToDate, objVendorInvHead.Vendor_Code, trans)
                                    objVendorInvHead.SaveData(objVendorInvHead, True, trans)
                                    clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
                                End If
#End Region
                            End If
                        End If
                    End If

                End If
            End If

        End If
    End Sub
    Public Shared Function CreateCostAdjustmentAgainstAPInvoice(ByVal dtSRNFATSNF As DataTable, ByVal objVendorInvHead As clsVedorInvoiceHead, ByVal strMCCCode As String, ByVal strMCCName As String, ByVal strPreDesc As String, ByVal trans As SqlTransaction) As Boolean
        Dim SettVSPBillDocumentToBeAddedInMilkCost As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.VSPBillDocumentToBeAddedInMilkCost, clsFixedParameterCode.VSPBillDocumentToBeAddedInMilkCost, trans)) > 0)
        If SettVSPBillDocumentToBeAddedInMilkCost Then
            If dtSRNFATSNF IsNot Nothing AndAlso dtSRNFATSNF.Rows.Count > 0 Then
                Dim objCostAdj As ClsAdjustments = Nothing
                objCostAdj = New ClsAdjustments()
                objCostAdj.Arr = New List(Of ClsAdjustmentsDetails)
                objCostAdj.Reference_Document = objVendorInvHead.Document_No
                objCostAdj.Adjustment_Date = clsCommon.myCDate(objVendorInvHead.Invoice_Entry_Date)
                objCostAdj.Against_AP_Invoice_No = objVendorInvHead.Document_No
                objCostAdj.Reference = strPreDesc + " " + objVendorInvHead.Document_No + "."
                objCostAdj.Description = strPreDesc + " " + objVendorInvHead.Document_No + "."
                objCostAdj.Unit_Code = "ALL"
                objCostAdj.IsMilkType = 1
                objCostAdj.ItemType = ""
                objCostAdj.Loc_Code = strMCCCode
                objCostAdj.Loc_Desc = strMCCName
                If clsCommon.CompairString(objVendorInvHead.Document_Type, "D") = CompairStringResult.Equal Then
                    objCostAdj.Trans_Type = "Out"
                ElseIf clsCommon.CompairString(objVendorInvHead.Document_Type, "C") = CompairStringResult.Equal Then
                    objCostAdj.Trans_Type = "In"
                End If

                Dim objCostAdjTr As New ClsAdjustmentsDetails()
                objCostAdjTr.Adjustment_Line_No = 1
                objCostAdjTr.Item_Code = clsCommon.myCstr(dtSRNFATSNF.Rows(0)("Item_Code"))
                objCostAdjTr.Item_Description = clsCommon.myCstr(dtSRNFATSNF.Rows(0)("Item_Desc"))
                If clsCommon.CompairString(objVendorInvHead.Document_Type, "D") = CompairStringResult.Equal Then
                    objCostAdjTr.Adjustment_Type = "CD"
                ElseIf clsCommon.CompairString(objVendorInvHead.Document_Type, "C") = CompairStringResult.Equal Then
                    objCostAdjTr.Adjustment_Type = "CI"
                End If
                objCostAdjTr.Item_Quantity = 0
                objCostAdjTr.Item_Cost = objVendorInvHead.Document_Total
                objCostAdjTr.Unit_Code = clsCommon.myCstr(dtSRNFATSNF.Rows(0)("UOM"))
                objCostAdjTr.Remarks = ""
                objCostAdjTr.Comments = ""
                objCostAdjTr.BreakageType = ""
                objCostAdjTr.Breakage = 0.0
                objCostAdjTr.Breakage_Cost = 0.0
                objCostAdjTr.LeakageQty = 0.0
                objCostAdjTr.fat_pers = 0
                objCostAdjTr.fat_kg = 0
                objCostAdjTr.snf_pers = 0
                objCostAdjTr.snf_kg = 0
                objCostAdjTr.fat_Amt = clsCommon.myCDivide((objVendorInvHead.Document_Total * clsCommon.myCdbl(dtSRNFATSNF.Rows(0)("Fat_Amt"))), (clsCommon.myCdbl(dtSRNFATSNF.Rows(0)("Fat_Amt")) + clsCommon.myCdbl(dtSRNFATSNF.Rows(0)("SNF_Amt"))))
                objCostAdjTr.snf_Amt = objVendorInvHead.Document_Total - objCostAdjTr.fat_Amt
                objCostAdj.Arr.Add(objCostAdjTr)

                objCostAdj.SaveData(objCostAdj, True, "", trans)
                ClsAdjustments.PostData(objCostAdj.Adjustment_No, "Store Adjustment", trans, False)
            End If
        End If
        Return True
    End Function
    Function getSRN(ByVal strSRNNo As String, ByVal tran As SqlTransaction) As DataTable
        Dim qry As String = "select Price_Code,Qty,FAT_PER,SNF_PER,NET_AMOUNT  from TSPL_MILK_SRN_DETAIL where doc_code='" + strSRNNo + "' "
        Return clsDBFuncationality.GetDataTable(qry, tran)
    End Function
    Sub CreateDebitNoteForDeductionMapping(ByVal objHead As clsMilkPurchaseInvoiceMCC, ByVal objList As List(Of clsMilkPurchaseInvoiceMCCDetail), ByVal trans As SqlTransaction)
        Dim objDedMapping As New clsDeductionMappingHead
        objDedMapping = objDedMapping.GetLatestMappingCode(objHead.MCC_CODE, objHead.VSP_CODE, objHead.DOC_DATE, trans)
        If objDedMapping IsNot Nothing AndAlso clsCommon.myLen(objDedMapping.Doc_Code) > 0 Then

            Dim objVendorInvHead As New clsVedorInvoiceHead()
            objVendorInvHead.isDeduction = 1
            'objVendorInvHead.Document_No = txtDocNo.Value'ToBeGenerated
            objVendorInvHead.Invoice_Entry_Date = clsCommon.GetPrintDate(objHead.DOC_DATE, "dd/MMM/yyyy")
            objVendorInvHead.Vendor_Code = objHead.VSP_CODE
            objVendorInvHead.Vendor_Name = clsVendorMaster.GetName(objHead.VSP_CODE, trans)
            objVendorInvHead.Vendor_Invoice_No = "" ''No Need to send vendor invoice no because it is of debit note type
            objVendorInvHead.Invoice_Type = "AP"
            objVendorInvHead.Vendor_Invoice_Date = objVendorInvHead.Invoice_Entry_Date
            objVendorInvHead.loc_code = clsLocation.GetSegmentCode(objHead.MCC_CODE, trans) 'obj.MCC_CODE
            'objVendorInvHead.Irregular_loc_code = obj.Irregular_MCC_CODE
            objVendorInvHead.Description = "AP Debit Note Against Deduction Mapping No:" + objDedMapping.Doc_Code
            'objVendorInvHead.PROJECT_ID = 1 'obj.PROJECT_ID
            objVendorInvHead.Account_Set = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  Vendor_Account from TSPL_VENDOR_MASTER where Vendor_Code ='" + objVendorInvHead.Vendor_Code + "'", trans))
            If (clsCommon.myLen(objVendorInvHead.Account_Set) < 0) Then
                Throw New Exception("Please set the vendor Account Set For Vendor : " + objVendorInvHead.Vendor_Name)
            End If
            objVendorInvHead.Document_Type = "D" ''For Purchase Invoice Type
            ''objVendorInvHead.PO_Number = obj.p
            '' ''added by priti
            objVendorInvHead.RefDocType = "DED-MAP"
            objVendorInvHead.RefDocNo = objHead.DOC_CODE
            'objVendorInvHead.Ref_SNo = ""
            '' '' priti ends here
            'objVendorInvHead.Order_No = txtOrderNo.Text
            ' objVendorInvHead.Total_Tax = 0
            objVendorInvHead.On_Hold = False
            'Dim srndate As String = ""
            'Dim srncode As String = ""
            'Dim Vlc_Code As String = ""
            'Dim Vlc_Name As String = ""
            'For Each objTr As clsMilkPurchaseInvoiceMCCDetail In obj.ObjList
            '    If clsCommon.myLen(objTr.SRN_CODE) > 0 Then
            '        Dim query As String = "select doc_date,vd.VLC_Code,VLC_Name from TSPL_Milk_SRN_HEAD sh inner join TSPL_VLC_MASTER_HEAD vd on sh.VLC_CODE=vd.VLC_Code where DOc_Code ='" + objTr.SRN_CODE + "' "
            '        Dim Dt_SRN As DataTable = clsDBFuncationality.GetDataTable(query, trans)
            '        srndate = IIf(srndate = "", clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"), srndate & "," & clsCommon.myCDate(CStr(Dt_SRN.Rows(0).Item("Doc_Date")), "dd/MMM/yyyy"))
            '        srncode = IIf(srncode = "", objTr.SRN_CODE, srncode & "," & objTr.SRN_CODE)
            '        Vlc_Code = IIf(Vlc_Code = "", Dt_SRN.Rows(0).Item("VLC_Code").ToString, Vlc_Code & "," & Dt_SRN.Rows(0).Item("VLC_Code").ToString)
            '        Vlc_Name = IIf(Vlc_Name = "", Dt_SRN.Rows(0).Item("VLC_Name").ToString, Vlc_Name & "," & Dt_SRN.Rows(0).Item("VLC_name").ToString)
            '    End If
            'Next



            'objVendorInvHead.Description = "VSP : " + obj.VSP_CODE + "/" + vendor_name + "VLC : " + Vlc_Code + "/" + Vlc_Name + " .Against PI Invoice No " + obj.DOC_CODE + "-" + srncode + "-" + srndate
            'objVendorInvHead.Tax_Calculation_Type = Nothing
            'objVendorInvHead.Tax_Group = Nothing
            'If (clsCommon.myLen(obj.TAX1) > 0) Then
            '    objVendorInvHead.TAX1 = obj.TAX1
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX1, trans) Then
            '        objVendorInvHead.TAX1_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX1, trans)
            '        objVendorInvHead.TAX1_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX1_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX1_Rate = obj.TAX1_Rate
            '    objVendorInvHead.Tax1_BAmount = obj.TAX1_Base_Amt
            '    objVendorInvHead.TAX1_Amt = obj.TAX1_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX2) > 0) Then
            '    objVendorInvHead.TAX2 = obj.TAX2
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX2, trans) Then
            '        objVendorInvHead.TAX2_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX2, trans)
            '        objVendorInvHead.TAX2_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX2_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX2_Rate = obj.TAX2_Rate
            '    objVendorInvHead.Tax2_BAmount = obj.TAX2_Base_Amt
            '    objVendorInvHead.TAX2_Amt = obj.TAX2_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX3) > 0) Then
            '    objVendorInvHead.TAX3 = obj.TAX3
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX3, trans) Then
            '        objVendorInvHead.TAX3_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX3, trans)
            '        objVendorInvHead.TAX3_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX3_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX3_Rate = obj.TAX3_Rate
            '    objVendorInvHead.Tax3_BAmount = obj.TAX3_Base_Amt
            '    objVendorInvHead.TAX3_Amt = obj.TAX3_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX4) > 0) Then
            '    objVendorInvHead.TAX4 = obj.TAX4
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX4, trans) Then
            '        objVendorInvHead.TAX4_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX4, trans)
            '        objVendorInvHead.TAX4_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX4_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX4_Rate = obj.TAX4_Rate
            '    objVendorInvHead.Tax4_BAmount = obj.TAX4_Base_Amt
            '    objVendorInvHead.TAX4_Amt = obj.TAX4_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX5) > 0) Then
            '    objVendorInvHead.TAX5 = obj.TAX5
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX5, trans) Then
            '        objVendorInvHead.TAX5_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX5, trans)
            '        objVendorInvHead.TAX5_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX5_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX5_Rate = obj.TAX5_Rate
            '    objVendorInvHead.Tax5_BAmount = obj.TAX5_Base_Amt
            '    objVendorInvHead.TAX5_Amt = obj.TAX5_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX6) > 0) Then
            '    objVendorInvHead.TAX6 = obj.TAX6
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX6, trans) Then
            '        objVendorInvHead.TAX6_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX6, trans)
            '        objVendorInvHead.TAX6_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX6_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX6_Rate = obj.TAX6_Rate
            '    objVendorInvHead.Tax6_BAmount = obj.TAX6_Base_Amt
            '    objVendorInvHead.TAX6_Amt = obj.TAX6_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX7) > 0) Then
            '    objVendorInvHead.TAX7 = obj.TAX7
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX7, trans) Then
            '        objVendorInvHead.TAX7_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX7, trans)
            '        objVendorInvHead.TAX7_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX7_GLAC, obj.MCC_CODE, trans)

            '    End If
            '    objVendorInvHead.TAX7_Rate = obj.TAX7_Rate
            '    objVendorInvHead.Tax7_BAmount = obj.TAX7_Base_Amt
            '    objVendorInvHead.TAX7_Amt = obj.TAX7_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX8) > 0) Then
            '    objVendorInvHead.TAX8 = obj.TAX8
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX8, trans) Then
            '        objVendorInvHead.TAX8_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX8, trans)
            '        objVendorInvHead.TAX8_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX8_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX8_Rate = obj.TAX8_Rate
            '    objVendorInvHead.Tax8_BAmount = obj.TAX8_Base_Amt
            '    objVendorInvHead.TAX8_Amt = obj.TAX8_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX9) > 0) Then
            '    objVendorInvHead.TAX9 = obj.TAX9
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX9, trans) Then
            '        objVendorInvHead.TAX9_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX9, trans)
            '        objVendorInvHead.TAX9_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX9_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX9_Rate = obj.TAX9_Rate
            '    objVendorInvHead.Tax9_BAmount = obj.TAX9_Base_Amt
            '    objVendorInvHead.TAX9_Amt = obj.TAX9_Amt
            'End If
            'If (clsCommon.myLen(obj.TAX10) > 0) Then
            '    objVendorInvHead.TAX10 = obj.TAX10
            '    If clsTaxMaster.IsTaxRecoverableAC(obj.TAX10, trans) Then
            '        objVendorInvHead.TAX10_GLAC = clsTaxMaster.GetTaxRecoverableAC(obj.TAX10, trans)
            '        objVendorInvHead.TAX10_GLAC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.TAX10_GLAC, obj.MCC_CODE, trans)
            '    End If
            '    objVendorInvHead.TAX10_Rate = obj.TAX10_Rate
            '    objVendorInvHead.Tax10_BAmount = obj.TAX10_Base_Amt
            '    objVendorInvHead.TAX10_Amt = obj.TAX10_Amt
            'End If

            'objVendorInvHead.Terms_Code = obj.Terms_Code
            'objVendorInvHead.Terms_Description = obj.TermsName
            objVendorInvHead.Due_Date = objVendorInvHead.Invoice_Entry_Date

            'objVendorInvHead.Against_POInvoice_No = obj.DOC_CODE
            'objVendorInvHead.Against_MillkPurchaseInvoice_No = obj.DOC_CODE

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Acct_Set_Code,Payable_Account,Discount_Account,Deduction_ACCOUNT from TSPL_VENDOR_ACCOUNT_SET  where Acct_Set_Code='" + objVendorInvHead.Account_Set + "'", trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objVendorInvHead.Vendor_Control_AC = clsCommon.myCstr(dt.Rows(0)("Payable_Account"))
                objVendorInvHead.Vendor_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Vendor_Control_AC, objHead.MCC_CODE, trans)
                If clsCommon.myCdbl(objVendorInvHead.Discount_Amount) > 0 Then
                    objVendorInvHead.Discount_GL_AC = clsCommon.myCstr(dt.Rows(0)("Discount_Account"))
                    objVendorInvHead.Discount_GL_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objVendorInvHead.Discount_GL_AC, objHead.MCC_CODE, trans)
                End If
            End If
            If clsCommon.myLen(objVendorInvHead.Vendor_Control_AC) <= 0 Then
                Throw New Exception("Please set the vendor payable Account")
            End If

            'objVendorInvHead.Total_Add_Charge = obj.Total_Add_Charge

            'objVendorInvHead.Add_Charge_Code1 = obj.Add_Charge_Code1
            'objVendorInvHead.Add_Charge_Name1 = obj.Add_Charge_Name1
            'objVendorInvHead.Add_Charge_Amt1 = obj.Add_Charge_Amt1

            'objVendorInvHead.Add_Charge_Code2 = obj.Add_Charge_Code2
            'objVendorInvHead.Add_Charge_Name2 = obj.Add_Charge_Name2
            'objVendorInvHead.Add_Charge_Amt2 = obj.Add_Charge_Amt2

            'objVendorInvHead.Add_Charge_Code3 = obj.Add_Charge_Code3
            'objVendorInvHead.Add_Charge_Name3 = obj.Add_Charge_Name3
            'objVendorInvHead.Add_Charge_Amt3 = obj.Add_Charge_Amt3

            'objVendorInvHead.Add_Charge_Code4 = obj.Add_Charge_Code4
            'objVendorInvHead.Add_Charge_Name4 = obj.Add_Charge_Name4
            'objVendorInvHead.Add_Charge_Amt4 = obj.Add_Charge_Amt4

            'objVendorInvHead.Add_Charge_Code5 = obj.Add_Charge_Code5
            'objVendorInvHead.Add_Charge_Name5 = obj.Add_Charge_Name5
            'objVendorInvHead.Add_Charge_Amt5 = obj.Add_Charge_Amt5

            'objVendorInvHead.Add_Charge_Code6 = obj.Add_Charge_Code6
            'objVendorInvHead.Add_Charge_Name6 = obj.Add_Charge_Name6
            'objVendorInvHead.Add_Charge_Amt6 = obj.Add_Charge_Amt6

            'objVendorInvHead.Add_Charge_Code7 = obj.Add_Charge_Code7
            'objVendorInvHead.Add_Charge_Name7 = obj.Add_Charge_Name7
            'objVendorInvHead.Add_Charge_Amt7 = obj.Add_Charge_Amt7

            'objVendorInvHead.Add_Charge_Code8 = obj.Add_Charge_Code8
            'objVendorInvHead.Add_Charge_Name8 = obj.Add_Charge_Name8
            'objVendorInvHead.Add_Charge_Amt8 = obj.Add_Charge_Amt8

            'objVendorInvHead.Add_Charge_Code9 = obj.Add_Charge_Code9
            'objVendorInvHead.Add_Charge_Name9 = obj.Add_Charge_Name9
            'objVendorInvHead.Add_Charge_Amt9 = obj.Add_Charge_Amt9

            'objVendorInvHead.Add_Charge_Code10 = obj.Add_Charge_Code10
            'objVendorInvHead.Add_Charge_Name10 = obj.Add_Charge_Name10
            'objVendorInvHead.Add_Charge_Amt10 = obj.Add_Charge_Amt10


            objVendorInvHead.Arr = New List(Of clsVedorInvoiceDetail)
            Dim ii As Integer = 0
            Dim isFirstTime As Boolean = True
            ' Dim strFirstItemCode As String = GetFirstItemCode(obj.ObjList)
            'objVendorInvHead.Empty_Amount = obj.Tot_Empty_Amount
            objVendorInvHead.Total_Landed_Amt = 0

            objVendorInvHead.ArrAssetEMI = New List(Of clsAPInvoiceAssetEMIDetails)()

            For Each objDedMappingDetail As clsDeductionMappingDetail In objDedMapping.Arr
                ''Set AP Invvoice Detail Table
                If clsCommon.myLen(objDedMappingDetail.DeductionGLAccount) <= 0 Then
                    Throw New Exception("Please set GL Account for deduction  :" + objDedMappingDetail.Deduction_Code)
                End If
                objDedMappingDetail.DeductionGLAccount = clsERPFuncationality.ChangeGLAccountLocationSegment(objDedMappingDetail.DeductionGLAccount, objHead.MCC_CODE, trans)

                ii = ii + 1
                Dim objVendorInvDetail As New clsVedorInvoiceDetail()
                objVendorInvDetail.Detail_Line_No = ii
                objVendorInvDetail.DeductionCode = objDedMappingDetail.Deduction_Code
                objVendorInvDetail.DeductionDesc = objDedMappingDetail.DeductionName
                objVendorInvDetail.GL_Account_Code = objDedMappingDetail.DeductionGLAccount
                objVendorInvDetail.GL_Account_Desc = clsGLAccount.GetName(objDedMappingDetail.DeductionGLAccount, trans)
                Dim dblAmount As Decimal = 0
                If clsCommon.CompairString(objDedMappingDetail.Type, "Qty") = CompairStringResult.Equal Then
                    Dim dblqty As Decimal = 0
                    For Each objdet As clsMilkPurchaseInvoiceMCCDetail In objList
                        dblqty += objdet.Qty
                    Next
                    dblAmount = dblqty * objDedMappingDetail.Per / 100
                End If
                If clsCommon.CompairString(objDedMappingDetail.Type, "Amt") = CompairStringResult.Equal Then
                    dblAmount = objHead.SRN_Net_Amount * objDedMappingDetail.Per / 100
                End If
                If objDedMapping.Is_Round_Down Then
                    dblAmount = dblAmount - (dblAmount Mod 1)
                End If

                objVendorInvDetail.Amount = dblAmount
                objVendorInvDetail.Discount_Per = 0
                objVendorInvDetail.Discount = 0
                objVendorInvDetail.Amount_less_Discount = dblAmount
                objVendorInvDetail.Total_Tax = 0
                objVendorInvDetail.Total_Amount = dblAmount
                objVendorInvDetail.Landed_Amount = dblAmount
                ''End of Set AP Invvoice Detail Table

                If (clsCommon.myLen(objVendorInvDetail.GL_Account_Code) > 0) Then
                    objVendorInvHead.Arr.Add(objVendorInvDetail)
                End If

                ''Set AP Invvoice Header Table
                objVendorInvHead.Total_Landed_Amt += dblAmount
                objVendorInvHead.Discount_Base += dblAmount
                objVendorInvHead.Discount_Amount += 0
                objVendorInvHead.Amount_Less_Discount += dblAmount
                objVendorInvHead.Document_Total += dblAmount
                objVendorInvHead.Balance_Amt += dblAmount
                ''End of Set AP Invvoice Header Table

                objVendorInvHead.Empty_Amount = 0 'obj.Tot_Empty_Amount
                If objVendorInvHead.Empty_Amount > 0 Then
                    If clsCommon.myLen(objVendorInvHead.Empty_Account) <= 0 Then
                        Throw New Exception("Please set Inventory Control Empties")
                    End If
                    objVendorInvHead.Document_Total += objVendorInvHead.Empty_Amount
                End If
            Next

            If (objVendorInvHead.Arr Is Nothing OrElse objVendorInvHead.Arr.Count <= 0) Then
                Throw New Exception("No GL Account Found For AP Invoice")
            End If
            ''multicurrency
            'objVendorInvHead.CURRENCY_CODE = obj.CURRENCY_CODE
            'objVendorInvHead.ConvRate = 1
            objVendorInvHead.ApplicableFrom = objVendorInvHead.Invoice_Entry_Date
            ''end multicurrency

            objVendorInvHead.SaveData(objVendorInvHead, True, trans)
            clsVedorInvoiceHead.PostData("", objVendorInvHead.Document_No, "", trans)
        End If
    End Sub
    Public Sub SelectMilkSRNItemsForMPPayment(ByVal strSRN_No As List(Of String), ByVal Vsp_Name As String, ByVal frm_date As Date, ByVal End_date As Date, ByVal Is_With_Bill As Boolean, ByVal trans As SqlTransaction, ByVal Formcode As String)
        'Dim sQuery As String = "select Distinct tspl_mp_master.MP_Code from TSPL_VLC_DATA_UPLOADER inner join tspl_mp_master on tspl_mp_master.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE inner join tspl_vendor_master on tspl_mp_master.MP_CODE=Vendor_Code where Parent_Vendor_Code='" & Vsp_Name & "' and convert(date,tspl_Vlc_Data_Uploader.DOC_DATE,103) Between  convert(date,'" & clsCommon.GetPrintDate(frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(End_date, "dd-MMM-yyyy") & "',103) "
        Dim sQuery As String = "select distinct TSPL_MP_MASTER.mp_Code,vsp_COde from TSPL_VLC_DATA_UPLOADER  inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.VLC_CODE and VSP_Code='" & Vsp_Name & "' and convert(date,tspl_Vlc_Data_Uploader.DOC_DATE,103) Between  convert(date,'" & clsCommon.GetPrintDate(frm_date, "dd-MMM-yyyy") & "',103) and convert(date,'" & clsCommon.GetPrintDate(End_date, "dd-MMM-yyyy") & "',103)   inner join TSPL_MP_MASTER on MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code"
        Dim DT_Mp As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
        For Each row_MP As DataRow In DT_Mp.Rows
            Dim obj_SRN As New clsMilkSRNMCC

            Dim frm As New UcMilkPendingSRN()
            frm.VendorCode = Vsp_Name
            frm.MpCode = clsCommon.myCstr(row_MP.Item("MP_Code"))
            frm.isForMP = True
            'frm.fORMCode = clsUserMgtCode.MilkMPPayment
            frm.stran = trans
            'frm.strCurrCode = FndSRNNO.Value
            frm.Frm_date = frm_date
            frm.To_date = End_date
            Dim StrDoc As New List(Of String)
            If Is_With_Bill Then
                If Not frm.LoaDHeadDataQuery(trans) Then
                    GoTo a
                End If
            Else
                If Not frm.LoaDHeadDataQueryVsp(trans) Then
                    GoTo a
                End If
            End If
            'frm.ShowDialog()
            'For Each Get_srn_no As String In strSRN_No
            For Each row As GridViewRowInfo In frm.gvHead.Rows()
                If strSRN_No.Contains(clsCommon.myCstr(row.Cells(UcMilkPendingSRN.colHCode).Value)) Then
                    frm.gvHead.CurrentRow = row
                    row.Cells(UcMilkPendingSRN.colHSelect).Value = True
                    'frm.LoadDetailData(True, clsCommon.myCstr(row.Cells(frm.colHCode).Value))
                End If
            Next
            'Next
            frm.btnOKPressed()
            If frm.ArrReturn IsNot Nothing AndAlso frm.ArrReturn.Count > 0 Then
                If clsCommon.myLen(frm.ArrReturn(0).DOC_CODE) > 0 Then
                    obj_SRN = clsMilkSRNMCC.GetData(frm.ArrReturn(0).DOC_CODE, NavigatorType.Current, trans)
                    If obj_SRN IsNot Nothing AndAlso clsCommon.myLen(obj_SRN.DOC_CODE) > 0 Then
                        '            txtCode.Value = obj.DOC_CODE
                        '  If dtpDocDate.MinDate < obj.DOC_DATE Then
                        '      dtpDocDate.MinDate = obj.DOC_DATE
                        '  End If
                        '  FndMccCode.Value = obj.MCC_CODE
                        '  'If clsCommon.myLen(obj.MCC_CODE) > 0 Then
                        '  '    Payment_Cycle_value = clsDBFuncationality.getSingleValue("SELECT payment_cycle from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")
                        '  'End If
                        '  DtMCC = clsDBFuncationality.GetDataTable("select * from tspl_Mcc_Master where Mcc_Code='" & clsCommon.myCstr(FndMccCode.Value) & "'", trans)
                        '  lblMccName.Text = DtMCC.Rows(0).Item("mcc_name") 'clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_Code='" & obj.MCC_CODE & "'")

                        '  'dtpDocDate.Value = obj.DOC_DATE

                        '  FndSRNNO.Value = ""
                        '  fndVSPCode.Value = obj.VSP_CODE

                        '  txtPayment.Text = clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
                        '  fndRouteCOde.Text = obj.ROUTE_CODE


                        '  lblRouteDesc.Text = clsDBFuncationality.getSingleValue("select Route_Name from TSPL_MCC_ROUTE_MASTER where Route_Code='" & fndRouteCOde.Text & "'", trans)
                        '  ' If LCase(txtPayment.Text) = "different" Then
                        '  '   lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select joint_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.text & "'")
                        '  'Else
                        '  lblVSPDesc.Text = clsDBFuncationality.getSingleValue("select vendor_name from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & fndVSPCode.Value & "'", trans)
                        '  'End If

                        '  'If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                        '  LoadBlankGridVSpPay()
                        '  ' Dim sQuery As String = "select * from TSPL_MILK_Shift_End_DETAIL where  MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
                        '  '& "and convert(date,DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"
                        '  Dim sQuery As String = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj.MCC_CODE) & "' " _
                        '& "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj.SHIFT) = "M", "Morning", "Evening") & "'"

                        '  Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                        '  For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
                        '      gv1.Rows.AddNew()

                        '      FndSRNNO.Value = IIf(FndSRNNO.Value = "", obj1.DOC_CODE, FndSRNNO.Value & "," & obj1.DOC_CODE)
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCans).Value = obj1.Cans
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCLR).Value = obj1.CLR
                        '      ' gv1.Rows(gv1.Rows.Count - 1).Cells(colCode).Value = obj1.DOC_CODE
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = obj1.MILK_Qty
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colAcc_Qty).Value = obj1.ACC_Qty
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colService_Charge).Value = obj1.Service_Charge_Type


                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCorrection_Factor).Value = obj1.Correction_Factor
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colFAT_PER).Value = obj1.FAT
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentive).Value = 0 '0
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colIncentiveEMP).Value = 0
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = obj1.Item_CODE
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = obj1.Item_Desc

                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colRATE).Value = obj1.RATE
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSNF_PER).Value = obj1.SNF
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value = obj1.DOC_CODE
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colSrn_Date).Value = obj.DOC_DATE
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colTOTAL_AMOUNT).Value = obj1.AMOUNT
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = obj1.UOM
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colVEHICLE_NO).Value = obj.VEHICLE_CODE
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value = obj1.VlC_Code
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value = obj1.AMOUNT
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colHead_Load_Amount).Value = obj1.Head_Load_Amount
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colOwn_Asset_Amount).Value = obj1.Own_Asset_Amount

                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colPaymentCOMMISSION).Value = obj1.Payment_Commission
                        '      gv1.Rows(gv1.Rows.Count - 1).Cells(colCOMMISSION).Value = obj1.Commission
                        '      If DtShiftEnd.Rows.Count > 0 Then
                        '          Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "' and srn_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colSRN_CODE).Value) & "'")
                        '          'Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
                        '          If dr.Length > 0 Then
                        '              gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colAMOUNT).Value) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
                        '          End If
                        '      End If
                        '      If Not Is_With_Bill Then
                        '          If Not StrDoc.Contains(obj1.Invoice_Code) Then
                        '              StrDoc.Add(obj1.Invoice_Code)
                        '          End If
                        '      End If
                        '      ' gv1.Rows(gv1.Rows.Count - 1).Cells(colDeduction).Value = 0
                        '  Next
                        'Else
                        '    gv1.Rows.AddNew()
                        'End If



                        'If rbtnTaxCalManual.IsChecked Then ''For Calcuation custom tax according to ratio of amount
                        '    For ii As Integer = 0 To gv1.RowCount - 1
                        '        UpdateCurrentRow(ii)
                        '    Next
                        'End If

                        Dim objHead As clsMilkPurchaseInvoiceMCC
                        Dim TotHeadLoad As Double = 0
                        Dim TotOwnAsset As Double = 0
                        Dim TotDeduction_Amount As Double = 0
                        '' asign screen vaules in objHead
                        objHead = New clsMilkPurchaseInvoiceMCC
                        objHead.DOC_CODE = ""
                        objHead.DOC_DATE = clsCommon.myCDate(End_date) 'obj_SRN.DOC_DATE)
                        objHead.Description = ""
                        objHead.ROUTE_CODE = clsCommon.myCstr(obj_SRN.ROUTE_CODE)
                        objHead.VSP_CODE = clsCommon.myCstr(frm.VendorCode) 'obj_SRN.VSP_CODE)
                        objHead.Payment = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vsp_Payment from TSPL_VENDOR_MASTER where form_type='VSP' and Vendor_Code='" & obj_SRN.VSP_CODE & "'", trans))
                        objHead.Irregular_MCC_CODE = clsCommon.myCstr(obj_SRN.MCC_CODE)
                        objHead.Program_Code = Formcode


                        Dim objList As New List(Of clsMilkPurchaseInvoiceMCCDetail)

                        Dim obj As clsMilkPurchaseInvoiceMCCDetail = Nothing
                        sQuery = "select TSPL_MILK_Shift_End_DETAIL.*,TSPL_MILK_SRN_HEAD.doc_code as srn_code from TSPL_MILK_Shift_End_DETAIL inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_head.VLC_DOC_CODE=TSPL_MILK_Shift_End_DETAIL.VLC_DOC_CODE where  TSPL_MILK_Shift_End_DETAIL.MCC_CODE='" & clsCommon.myCstr(obj_SRN.MCC_CODE) & "' " _
                     & "and convert(date,TSPL_MILK_Shift_End_DETAIL.DOC_DATE,103)='" & clsCommon.GetPrintDate(obj_SRN.DOC_DATE, "dd-MMM-yyyy") & "' and TSPL_MILK_Shift_End_DETAIL.SHIFT='" & IIf(clsCommon.myCstr(obj_SRN.SHIFT) = "M", "Morning", "Evening") & "'"

                        Dim DtShiftEnd As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)

                        '========================Total==================
                        Dim totAmount As Double = 0
                        Dim totCommssion As Double = 0
                        Dim totPaymentCommssion As Double = 0
                        Dim totAmountwithPaymentCommssion As Double = 0
                        Dim totAmountIncentive As Double = 0
                        Dim totAmountIncentiveEMP As Double = 0
                        Dim totBasicAmount As Double = 0

                        '==============================================
                        For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
                            obj = New clsMilkPurchaseInvoiceMCCDetail
                            obj.DOC_CODE = ""
                            obj.AMOUNT = clsCommon.myCdbl(obj1.AMOUNT)
                            obj.Cans = clsCommon.myCdbl(obj1.Cans)
                            obj.CLR = clsCommon.myCdbl(obj1.CLR)
                            obj.COMMISSION = clsCommon.myCdbl(obj1.Commission)
                            obj.Payment_COMMISSION = clsCommon.myCdbl(obj1.Payment_Commission)
                            If DtShiftEnd.Rows.Count > 0 Then
                                Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(obj1.VlC_Code) & "' and srn_code='" & clsCommon.myCstr(obj1.DOC_CODE) & "'")
                                'Dim dr() As DataRow = DtShiftEnd.Select("vlc_code='" & clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(colVLC_NO).Value) & "'")
                                If dr.Length > 0 Then
                                    obj.Deduction = IIf(clsCommon.myCstr(dr(0)("A_Or_R")) = "R", clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(dr(0)("Deduction_of_VSP")) / 100, clsCommon.myCdbl(dr(0)("Deduction_of_VSP")))
                                End If
                            End If
                            'obj.Deduction = clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                            obj.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
                            obj.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
                            obj.Correction_Factor = clsCommon.myCdbl(obj1.Correction_Factor)
                            obj.FAT_PER = clsCommon.myCdbl(obj1.FAT)
                            'obj.Incentive = clsCommon.myCdbl(grow.Cells(colIncentive).Value)
                            'obj.IncentiveEMP = clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)
                            obj.Item_Code = clsCommon.myCstr(obj1.Item_CODE)
                            obj.MCC_CODE = obj1.MCC_CODE
                            obj.Qty = clsCommon.myCdbl(obj1.MILK_Qty)
                            obj.Acc_Qty = clsCommon.myCdbl(obj1.ACC_Qty)
                            obj.Service_Charge = clsCommon.myCstr(obj1.Service_Charge_Type)
                            obj.RATE = clsCommon.myCdbl(obj1.RATE)
                            obj.SNF_PER = clsCommon.myCdbl(obj1.SNF)
                            obj.Head_Load_Amount = clsCommon.myCdbl(obj1.Head_Load_Amount)
                            'obj.Own_Asset_Amount = clsCommon.myCdbl(obj1.Own_Asset_Amount)
                            '=====================================
                            Dim Commission_AMount As Double = 0
                            Dim Payment_Commission_AMount As Double = 0
                            If clsCommon.myCstr(obj1.Service_Charge_Type) = "%(Percentage)" Then
                                'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colCOMMISSION).Value / 100
                                'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAMOUNT).Value * grow.Cells(colPaymentCOMMISSION).Value / 100
                                'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

                                Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Commission) / 100, 2)
                                Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
                                ' obj1.i = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
                            ElseIf clsCommon.myCstr(obj1.Service_Charge_Type) = "Rate/Kg" Then
                                'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colCOMMISSION).Value
                                'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colAcc_Qty).Value * grow.Cells(colPaymentCOMMISSION).Value
                                'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

                                Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.ACC_Qty) * clsCommon.myCdbl(obj1.Commission), 2)
                                Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.ACC_Qty) * clsCommon.myCdbl(obj1.Payment_Commission), 2)
                            ElseIf clsCommon.myCstr(obj1.Service_Charge_Type) = "Rate/Ltr" And clsCommon.myCstr(obj1.UOM) = "LTR" Then
                                'grow.Cells(colCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colCOMMISSION).Value
                                'grow.Cells(colPaymentCOMMISSIONAmount).Value = grow.Cells(colQty).Value * grow.Cells(colPaymentCOMMISSION).Value
                                'grow.Cells(colIncentiveEMP).Value = clsCommon.myCdbl(grow.Cells(colIncentive).Value) * grow.Cells(colPaymentCOMMISSION).Value / 100

                                Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.MILK_Qty) * clsCommon.myCdbl(obj1.Commission), 2)
                                Payment_Commission_AMount = Math.Round(clsCommon.myCdbl(obj1.MILK_Qty) * clsCommon.myCdbl(obj1.Payment_Commission), 2)
                            End If
                            'grow.Cells(colDocument_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                            'grow.Cells(colTOTAL_AMOUNT).Value = clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                            'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
                            'grow.Cells(colNetsaveAMOUNT).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAMOUNT).Value) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))

                            'obj.do = clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                            obj.TOTAL_AMOUNT = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount), 2) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value) '- clsCommon.myCdbl(grow.Cells(colDeduction).Value)
                            'grow.Cells(colNetAMOUNT).Value = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(grow.Cells(colPaymentCOMMISSIONAmount).Value) + clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_CALC"))))
                            obj.Net_AMOUNT = Math.Round(clsCommon.myCdbl(obj1.AMOUNT) + clsCommon.myCdbl(Payment_Commission_AMount), 2) '+ clsCommon.myCdbl(grow.Cells(colIncentive).Value) + clsCommon.myCdbl(grow.Cells(colIncentiveEMP).Value)) ' - clsCommon.myCdbl(grow.Cells(colDeduction).Value), CInt(clsCommon.myCdbl(DtMCC.Rows(0).Item("FAT_SNF_SAVE"))))

                            '===============================================================
                            obj.SRN_CODE = clsCommon.myCstr(obj1.DOC_CODE)
                            obj.TOTAL_AMOUNT = clsCommon.myCdbl(obj.TOTAL_AMOUNT)
                            obj.VEHICLE_NO = clsCommon.myCstr(obj_SRN.VEHICLE_CODE)
                            obj.VLC_NO = clsCommon.myCstr(obj1.VlC_Code)
                            obj.Net_AMOUNT = clsCommon.myCdbl(obj.Net_AMOUNT)
                            obj1.NET_AMOUNT = clsCommon.myCdbl(obj.Net_AMOUNT)
                            objHead.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(obj.SRN_CODE, trans)
                            obj.MCC_CODE = clsMilkPurchaseInvoiceMCC.GetIrregular_Location(obj.SRN_CODE, trans)
                            If clsCommon.CompairString(objHead.MCC_CODE, objHead.Irregular_MCC_CODE) = CompairStringResult.Equal Then
                                objHead.Irregular_MCC_CODE = ""
                            End If

                            objList.Add(obj)
                            TotHeadLoad += obj.Head_Load_Amount
                            TotOwnAsset += obj.Own_Asset_Amount
                            TotDeduction_Amount += obj.Deduction

                            totAmount += obj.AMOUNT
                            totBasicAmount += obj.AMOUNT
                            totCommssion += Commission_AMount
                            totPaymentCommssion += Payment_Commission_AMount
                            totAmountwithPaymentCommssion += obj.Net_AMOUNT
                        Next
                        objHead.Total_Head_Load_Amount = TotHeadLoad
                        objHead.Total_Own_Asset_Amount = TotOwnAsset
                        objHead.Total_Deduction_Amount = TotDeduction_Amount


                        objHead.VENDOR_INVOICE_NO = "" 'clsCommon.myCstr(TxtVendorInvoiceNo.Text)
                        objHead.VENDOR_INVOICE_DATE = obj_SRN.DOC_DATE 'clsCommon.myCDate(VENDOR_INVOICE_DATE.Value)
                        objHead.Amount = clsCommon.myCdbl(totAmount)
                        objHead.Basic_Amount = Math.Round(clsCommon.myCdbl(totAmount) - clsCommon.myCdbl(totCommssion), 2)
                        objHead.Commission = clsCommon.myCdbl(totCommssion)
                        objHead.Total_Amount_Acc = clsCommon.myCdbl(totAmountwithPaymentCommssion)
                        objHead.Total_PaymentCommission = clsCommon.myCdbl(totPaymentCommssion)
                        objHead.Program_Code = Formcode
                        'End If
                        'Next
                        ' ''For Custom Fields
                        ''Dim obj As New clsMilkPurchaseInvoiceMCC()
                        'obj = New clsMilkPurchaseInvoiceMCC
                        'obj.Form_ID = MyBase.Form_ID
                        'obj.arrCustomFields = New List(Of clsCustomFieldValues)
                        'If MyBase.customFieldTabProperty = ElementVisibility.Visible Then
                        '    UcCustomFields1.GetData(obj.arrCustomFields)
                        'End If
                        'If MyBase.ArrDetailFields IsNot Nothing AndAlso MyBase.ArrDetailFields.Count > 0 Then
                        '    clsCustomFieldGrid.GetData(obj.arrCustomFields, gv1, MyBase.ArrDetailFields, colCode)
                        'End If
                        ' ''End of For Custom Fields

                        If clsMilkPurchaseInvoiceMCC.SaveData(objHead, objList, trans) Then
                            ' trans.Commit()
                            'Dim transs As SqlTransaction
                            'UcAttachment1.SaveData(objHead.DOC_CODE)
                            'Dim incentive As ArrayList = clsMilkPurchaseInvoiceMCC.LoadDataQuery_For_Incentive(objHead.DOC_CODE, objHead.VSP_CODE, objHead.MCC_CODE, frm_date, Today.Date, False, trans, (End_date.Day - frm_date.Day) + 1)
                            'Dim totincentiveEMP As Double = 0
                            'Dim totincentive As Double = 0
                            'totAmount = 0
                            'totBasicAmount = 0
                            'totAmountwithPaymentCommssion = 0
                            'Dim is_processed As Integer = 0
                            'Dim is_Emp_On_Amount_Only As String = clsDBFuncationality.getSingleValue("select EmpOnAMountOnly from tspl_Mcc_Master where Mcc_Code='" & obj.MCC_CODE & "'", trans)
                            'If incentive.Count > 0 Then
                            '    If incentive(1) > 0 Then
                            '        For Each obj1 As clsMilkSRNMCCDetail In frm.ArrReturn
                            '            If is_processed = 0 Then
                            '                totincentiveEMP = Math.Round(clsCommon.myCdbl(incentive(1)) * clsCommon.myCdbl(obj1.Payment_Commission) / 100, 2)
                            '                totAmount += obj.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                            '                totBasicAmount += obj.AMOUNT + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                            '                obj.Net_AMOUNT += +IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                            '                totAmountwithPaymentCommssion += obj.Net_AMOUNT '+ totincentiveEMP '+ incentive(1)
                            '                sQuery = "Update tspl_Milk_Purchase_Invoice_Detail set Total_Amount='" & clsCommon.myCdbl(obj.AMOUNT) & "',Total_Amount_Acc='" & clsCommon.myCdbl(obj.Net_AMOUNT) & "',Net_Amount='" & clsCommon.myCdbl(obj.Net_AMOUNT) & "',incentive='" & incentive(1) & "' , incentiveEMP='" & totincentiveEMP & "' where srn_code='" & obj.SRN_CODE & "'"
                            '                clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            '                is_processed = 1
                            '            End If
                            '            'Exit For
                            '        Next
                            '        is_processed = 0
                            '        totAmount = objHead.Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)
                            '        totAmountwithPaymentCommssion = objHead.ACC_Amount + IIf(is_Emp_On_Amount_Only = "1", 0, totincentiveEMP) + incentive(1)

                            '        'totincentiveEMP += clsCommon.myCdbl(gv1.Rows(0).Cells(colIncentiveEMP).Value)
                            '        sQuery = "select * from tspl_Milk_Purchase_Invoice_Head where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
                            '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
                            '        'sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount=convert(Total_Amount as float)+" & clsCommon.myCdbl(totAmount) & ",Total_Amount_Acc=convert(Total_Amount_Acc  as float)+" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & " ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
                            '        sQuery = "Update tspl_Milk_Purchase_Invoice_Head set Total_Amount='" & clsCommon.myCdbl(totAmount) & "',Total_Amount_Acc='" & clsCommon.myCdbl(totAmountwithPaymentCommssion) & "' ,incentive_Head='" & incentive(1) & "' , incentiveEMP_Head='" & totincentiveEMP & "' where doc_code='" & clsCommon.myCstr(objHead.DOC_CODE) & "'"
                            '        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                            '    End If
                            'End If
                            clsMilkPurchaseInvoiceMCC.PostData("M-PURINVOICE", objHead.DOC_CODE, trans)
                        End If
                        ' Return True
                    End If


                End If
            End If
a:      Next


    End Sub
    Function VSPQry(ByVal arrMCC As ArrayList, ByVal arrVSP As ArrayList, ByVal txtFromDate As DateTime, ByVal txtToDate As DateTime, ByVal Formcode As String, ByVal isPickPendingMilkSRNinNextPaymentCycle As Boolean) As String

        Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,xx.ROUTE_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name from (" + Environment.NewLine +
            " select VSP_CODE,max(VLC_CODE)as VLC_CODE,ROUTE_CODE from (" + Environment.NewLine +
            " select VSP_CODE,VLC_CODE as VLC_CODE,ROUTE_CODE from TSPL_MILK_SRN_Head where 2=2  " + Environment.NewLine

        qry += " and MCC_CODE in (" + clsCommon.GetMulcallString(arrMCC) + ")"

        If Not isPickPendingMilkSRNinNextPaymentCycle Then
            qry += " and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
        " union all " + Environment.NewLine +
        " select VSP_CODE,VLC_CODE as VLC_CODE,ROUTE_CODE from TSPL_MILK_REJECT_DETAIL left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE where TSPL_MILK_REJECT_HEAD.Posted=1 "

        qry += " and TSPL_MILK_REJECT_HEAD.MCC_CODE in (" + clsCommon.GetMulcallString(arrMCC) + ")"


        If Not isPickPendingMilkSRNinNextPaymentCycle Then
            qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
        " )x group by VSP_CODE,ROUTE_CODE " + Environment.NewLine
        If arrVSP IsNot Nothing AndAlso arrVSP.Count > 0 Then
            qry += " having VSP_CODE in (" + clsCommon.GetMulcallString(arrVSP) + ") "
        End If

        qry += " )xx " + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine &
        " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE " + Environment.NewLine &
        " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=xx.ROUTE_CODE" + Environment.NewLine & ""

        '' done by Panch Raj to distinguish vsp bill and farmer bill 
        If clsCommon.CompairString(Formcode, clsUserMgtCode.MPBillGeneration) = CompairStringResult.Equal Then
            qry = qry & " where coalesce(TSPL_VENDOR_MASTER.VSP_Farmer_Billing,0)=1"
        Else
            qry = qry & " where coalesce(TSPL_VENDOR_MASTER.VSP_Farmer_Billing,0)=0 and isnull(TSPL_VENDOR_MASTER.is_Drip_Saver,'')<>'Y' "
        End If
        qry = qry & " order by xx.VSP_CODE "
        Return qry
    End Function
    Sub GeneratePONumber(ByVal strMCCCode As String, ByVal txtFromDate As DateTime, ByVal txtToDate As DateTime, ByVal tran As SqlTransaction)
        Dim qry As String = "select TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.MCC_CODE " + Environment.NewLine +
        "from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
        "left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE =TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
        "left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_SRN_HEAD.mcc_code" + Environment.NewLine +
        "where TSPL_MILK_SRN_Head.MCC_CODE='" + strMCCCode + "' and isnull(Purchase_Order_No,'')='' and isnull( TSPL_MILK_SRN_head.Against_Reject_No,'')='' and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) Between convert(date,'" + clsCommon.GetPrintDate(txtFromDate, "dd/MMM/yyyy") + "',103) and convert(date,'" + clsCommon.GetPrintDate(txtToDate, "dd/MMM/yyyy") + "',103) " + Environment.NewLine +
        "and isnull(TSPL_MCC_MASTER.Failed_Sample_Apply,0)=1 and TSPL_MILK_SRN_DETAIL.FAT_PER>=TSPL_MCC_MASTER.Failed_Sample_FAT and TSPL_MILK_SRN_DETAIL.SNF_PER>=TSPL_MCC_MASTER.Failed_Sample_SNF order by TSPL_MILK_SRN_HEAD.DOC_DATE "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, tran)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Dim strCode As String = clsERPFuncationality.GetNextCode(tran, clsCommon.myCDate(dr("DOC_DATE")), clsDocType.MilkPO, "", clsCommon.myCstr(dr("MCC_CODE")), False, True, False, False, objCommonVar.ShowMCCFinderInPaymentProcess)
                If clsCommon.myLen(strCode) <= 0 Then
                    Throw New Exception("Error in code generation of Milk purchae order")
                End If
                Dim coll As New Hashtable()
                clsCommon.AddColumnsForChange(coll, "Purchase_Order_No", strCode)
                clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MILK_SRN_HEAD", OMInsertOrUpdate.Update, "DOC_CODE='" + clsCommon.myCstr(dr("DOC_CODE")) + "'", tran)
                coll = Nothing
            Next
            dt = Nothing
        End If

    End Sub
    Sub Generate_Vsp_Issue_Debit_Note(ByVal strMCCCode As String, ByVal txtVSP As ArrayList, ByVal DebitCreditNoteType As String, ByVal txtMonth As DateTime)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strMCCCode) <= 0 Then
                Throw New Exception("Please Select MCC To Generate Bill")
            End If
            If txtVSP Is Nothing OrElse txtVSP.Count <= 0 Then
                Throw New Exception("Please Select VSP To Generate Bill")
            End If

            Dim Dt As DataTable
            Dim sQuery As String = String.Empty
            Dim Frm_Date As Date = CDate("01-" & MonthName(txtMonth.Month, True) & "-" & txtMonth.Year)
            Dim To_Date As Date
            If txtMonth.Month = 1 Then
                To_Date = CDate("" & Date.DaysInMonth(txtMonth.Year - 1, txtMonth.Month) & "-" & MonthName(12, True) & "-" & txtMonth.Year - 1)
            Else
                To_Date = CDate("" & Date.DaysInMonth(txtMonth.Year, txtMonth.Month - 1) & "-" & MonthName(txtMonth.Month - 1, True) & "-" & txtMonth.Year)
            End If
            Dim Srn_No_List As New List(Of String)
            If (clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal) Then
                objCommonVar.SelectedUser = "All"
                objCommonVar.SelectedUser = clsCommon.GetMulcallString(txtVSP)
            Else
                objCommonVar.SelectedUser = objCommonVar.CurrentUserCode
            End If

            Dim doc_type As String = ""
            If clsCommon.CompairString(DebitCreditNoteType, "D") = CompairStringResult.Equal Then
                doc_type = "Issue"
            ElseIf clsCommon.CompairString(DebitCreditNoteType, "C") = CompairStringResult.Equal Then
                doc_type = "Return"
            Else
                Throw New Exception("Document Type must be D or C")
            End If
            For Each VSP As String In txtVSP
                Srn_No_List.Clear()
                Frm_Date = To_Date.AddDays(1)
                To_Date = To_Date.AddDays(Date.DaysInMonth(txtMonth.Year, txtMonth.Month))
                sQuery = "select Distinct TSPL_VSPItem_HEAD. Doc_No as doc_code from TSPL_VSPItem_HEAD  where  Coalesce(Is_debit_Note_Created,0)=0 and " _
                & " Issue_To='" & VSP & "' AND From_Location='" & strMCCCode & "' AND convert(date,DOC_DATE,103) >=convert(date,'" & Frm_Date & "',103)  and " _
                & " convert(date,DOC_DATE,103) <=convert(date,'" & To_Date & "',103) and doc_type='" & doc_type & "' and TSPL_VSPItem_HEAD.status=1"
                Dt = clsDBFuncationality.GetDataTable(sQuery, trans)
                If Dt.Rows.Count > 0 Then
                    For Each row As DataRow In Dt.Rows()
                        Dim objvsp As New clsVSPItemIssue
                        objvsp.SaveDebitNoteEntry(clsCommon.myCstr(row("Doc_Code")), DebitCreditNoteType, trans)
                        sQuery = "update TSPL_VSPItem_HEAD set is_debit_note_Created=1 where Doc_No='" & clsCommon.myCstr(row("Doc_Code")) & "'"
                        clsDBFuncationality.ExecuteNonQuery(sQuery, trans)
                    Next
                End If

                Frm_Date = CDate("01-" & MonthName(txtMonth.Month, True) & "-" & txtMonth.Year)
                If txtMonth.Month = 1 Then
                    To_Date = CDate("" & Date.DaysInMonth(txtMonth.Year - 1, txtMonth.Month) & "-" & MonthName(12, True) & "-" & txtMonth.Year - 1)
                Else
                    To_Date = CDate("" & Date.DaysInMonth(txtMonth.Year, txtMonth.Month - 1) & "-" & MonthName(txtMonth.Month - 1, True) & "-" & txtMonth.Year)
                End If
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function CreateMCCChillingProvision(ByVal strMCCCode As String, ByVal strMCCName As String, ByVal txtFromDate As DateTime, ByVal txtToDate As DateTime) As Boolean
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "select is_Chilling_Provision_Monthly from TSPL_MCC_MASTER where MCC_Type='Chilling Basis' and MCC_Code='" + strMCCCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If clsCommon.myCdbl(dt.Rows(0)("is_Chilling_Provision_Monthly")) = 0 Then
                    qry = "select * FROM ExplodeDates( " + "'" + clsCommon.GetPrintDate(txtFromDate, "dd/MMM/yyyy") + "'" + ",'" + clsCommon.GetPrintDate(txtToDate, "dd/MMM/yyyy") + "')"
                    Dim dtDateRange As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtDateRange IsNot Nothing AndAlso dtDateRange.Rows.Count > 0 Then
                        For Each drDateRange As DataRow In dtDateRange.Rows
                            CreateMCCChillingProvisionDaily(clsCommon.myCDate(drDateRange(0)), strMCCCode, strMCCName, trans)
                        Next
                    End If
                ElseIf clsCommon.myCdbl(dt.Rows(0)("is_Chilling_Provision_Monthly")) = 1 Then
                    CreateMCCChillingProvisionMonthly(strMCCCode, strMCCName, trans, txtToDate)
                End If
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Public Function CreateMCCChillingProvisionDaily(ByVal DOC_DATE As Date, ByVal strMCCCode As String, ByVal strMCCName As String, ByVal trans As SqlTransaction) As Boolean
        Dim qry As String = "select 1 from TSPL_PROVISION_ENTRY where Prov_type='Chilling Charge' and Loc_Code='" + strMCCCode + "' and convert(date, Doc_Date,103)='" + clsCommon.GetPrintDate(DOC_DATE, "dd/MMM/yyyy") + "' and Prog_Code='" + clsUserMgtCode.MilkVSPPayment + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Dim sQuery As String = "select (case when Qty<Chilling_Assure_Qty then Chilling_Assure_Qty else Qty end)*Chilling_Rate as Amount  from (" + Environment.NewLine +
                " select TSPL_MCC_MASTER.Chilling_Assure_Qty,TSPL_MCC_MASTER.Chilling_Rate,TSPL_MCC_MASTER.Unit_ChillingOn,xxx.ACC_Qty" + Environment.NewLine +
                " ,( case when Unit_ChillingOn='K' then ACC_Qty else Qty end ) as Qty from " + Environment.NewLine +
                " ( " + Environment.NewLine +
                " select max(MCC_CODE) as MCC_CODE, sum(ACC_Qty) as ACC_Qty,sum(Qty) as Qty from ( " + Environment.NewLine +
                " select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_DETAIL.ACC_Qty,Qty from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                " where TSPL_MILK_SRN_HEAD.MCC_CODE='" + strMCCCode + "' and TSPL_MILK_SRN_HEAD.Posted=1 and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(DOC_DATE), "dd/MMM/yyyy hh:mm tt") + "' and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DOC_DATE), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
                " )xx" + Environment.NewLine +
                " )xxx left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCC_CODE " + Environment.NewLine +
                " where  TSPL_MCC_MASTER.MCC_Type='Chilling Basis'" + Environment.NewLine +
                " )xxxx"
            Dim dtChilling As DataTable = clsDBFuncationality.GetDataTable(sQuery, trans)
            Dim Provision_Amt As Decimal = 0
            If dtChilling IsNot Nothing AndAlso dtChilling.Rows.Count > 0 Then
                Provision_Amt = Math.Round(clsCommon.myCdbl(dtChilling.Rows(0)("Amount")), 2)
            End If
            If Provision_Amt <= 0 Then
                Provision_Amt = 0
            End If
            ChillingProvision(Provision_Amt, strMCCCode, strMCCName, trans, DOC_DATE)

        End If
        Return True
    End Function
    Sub ChillingProvision(ByVal Provision_Amt As Decimal, ByVal strMCCCode As String, ByVal strMCCName As String, ByVal trans As SqlTransaction, ByVal lastDate As Date)
        If Provision_Amt > 0 Then
            Dim dtmcc As DataTable = clsDBFuncationality.GetDataTable("select tspl_mcc_master.*,vendor_name from tspl_mcc_master inner join " _
                    & " tspl_vendor_master on vendor_code=chilling_vendor where  tspl_mcc_master.mcc_code='" + strMCCCode + "'", trans)
            If dtmcc.Rows.Count > 0 Then
                Dim obj As New clsProvisionEntry()
                obj = New clsProvisionEntry()
                obj.Doc_Date = lastDate
                obj.Vendor_Code = dtmcc.Rows(0)("Chilling_Vendor")
                obj.Vendor_Desc = dtmcc.Rows(0)("Vendor_Name")
                obj.Vendor_Type = "MCC Lease Vendor"
                obj.Status = "No"
                obj.Loc_Code = strMCCCode
                obj.Loc_Desc = strMCCName
                obj.Ref_Doc_No = ""
                obj.Prov_type = "Chilling Charge"
                obj.Amount = Provision_Amt
                obj.Prog_Code = clsUserMgtCode.MilkVSPPayment
                obj.Prov_Month = Month(lastDate)
                obj.Prov_Year = Year(lastDate)
                obj.Modified_Date = clsCommon.GETSERVERDATE(trans)
                obj.isNewEntry = True
                clsProvisionEntry.SaveData(obj, trans)
                clsProvisionEntry.PostData(obj.Doc_No, trans, False)
            End If
        End If
    End Sub
    Public Function CreateMCCChillingProvisionMonthly(ByVal strMCCCode As String, ByVal strMCCName As String, ByVal trans As SqlTransaction, ByVal txtToDate As DateTime) As Boolean
        Dim FirstDate As DateTime = New DateTime(txtToDate.Year, txtToDate.Month, 1)
        Dim lastDate As DateTime = FirstDate.AddMonths(1).AddDays(-1)
        If txtToDate.Day = lastDate.Day Then
            Dim qry As String = "select 1 from TSPL_PROVISION_ENTRY where Prov_type='Chilling Charge' and Loc_Code='" + strMCCCode + "' and Prov_Year='" + clsCommon.myCstr(lastDate.Year) + "' and Prov_Month='" + clsCommon.myCstr(lastDate.Month) + "' and Prog_Code='" + clsUserMgtCode.MilkVSPPayment + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Try
                    qry = "select (case when Qty<Chilling_Assure_Qty then Chilling_Assure_Qty else Qty end)*Chilling_Rate as Amount  from (" + Environment.NewLine +
                        " select TSPL_MCC_MASTER.Chilling_Assure_Qty,TSPL_MCC_MASTER.Chilling_Rate,TSPL_MCC_MASTER.Unit_ChillingOn,xxx.ACC_Qty" + Environment.NewLine +
                        " ,( case when Unit_ChillingOn='K' then ACC_Qty else Qty end ) as Qty from " + Environment.NewLine +
                        " ( " + Environment.NewLine +
                        " select max(MCC_CODE) as MCC_CODE, sum(ACC_Qty) as ACC_Qty,sum(Qty) as Qty from ( " + Environment.NewLine +
                        " select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_DETAIL.ACC_Qty,TSPL_MILK_SRN_DETAIL.Qty from TSPL_MILK_SRN_DETAIL" + Environment.NewLine +
                        " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine +
                        " where TSPL_MILK_SRN_HEAD.MCC_CODE='" + strMCCCode + "' and TSPL_MILK_SRN_HEAD.Posted=1 and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FirstDate), "dd/MMM/yyyy hh:mm:ss tt") + "' and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(lastDate), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine +
                        " )xx" + Environment.NewLine +
                        " )xxx left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=xxx.MCC_CODE " + Environment.NewLine +
                        " where  TSPL_MCC_MASTER.MCC_Type='Chilling Basis' and TSPL_MCC_MASTER.is_Chilling_Provision_Monthly='1'" + Environment.NewLine +
                        " )xxxx"

                    Dim dtChilling As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim Provision_Amt = 0
                    If dtChilling IsNot Nothing AndAlso dtChilling.Rows.Count > 0 Then
                        Provision_Amt = Math.Round(clsCommon.myCdbl(dtChilling.Rows(0)("Amount")), 2)
                    End If
                    If Provision_Amt <= 0 Then
                        Provision_Amt = 0
                    End If
                    ChillingProvision(Provision_Amt, strMCCCode, strMCCName, trans, lastDate)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            End If
        End If
        Return True
    End Function
    Shared Sub UpdateSTDQtyOFSRN(ByVal txtFromDate As DateTime, ByVal strMCCCode As String)
        Try
            Dim qry As String = "update TSPL_MILK_SRN_DETAIL set Std_Qty=xxxx.STDQTY" +
            " from(" +
            " select xx.DOC_CODE,convert(decimal(18,2), (select  top 1 (  FATKG *CAST(Ratio as decimal)/FAT_Pers)+(SNFKg*SNF_Ratio/SNF_Pers) as Qty  " +
            " from TSPL_MILK_PRICE_MASTER " +
            " where Effective_Date<=xx.doc_date order by Effective_Date desc))  as STDQTY  from (" +
            " select TSPL_MILK_SRN_DETAIL.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_DETAIL.ACC_Qty,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER  " +
            " ,TSPL_MILK_SRN_DETAIL.ACC_Qty*TSPL_MILK_SRN_DETAIL.FAT_PER/100 as FATKG,TSPL_MILK_SRN_DETAIL.ACC_Qty*TSPL_MILK_SRN_DETAIL.SNF_PER/100 as SNFKg " +
            " from TSPL_MILK_SRN_DETAIL " +
            " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE " +
            " where TSPL_MILK_SRN_DETAIL.Std_Qty = 0 " +
            " )xx " +
            " )xxxx inner join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=xxxx.DOC_CODE"
            clsDBFuncationality.ExecuteNonQuery(qry)


            '            If clsCommon.myLen(clsFixedParameter.GetData(clsFixedParameterType.BholeBabaPaymentProcessProPrintStartDate, clsFixedParameterCode.BholeBabaPaymentProcessProPrintStartDate, Nothing)) > 0 Then
            '                qry = "delete from TSPL_VLC_DATA_UPLOADER where Doc_No in (select distinct MinDoc  from (
            'select MIN(Doc_No) AS  MinDoc,MAX(Doc_No) as MaxDoc,MCC_Code,File_Date,shift,VLC_CODE,MP_CODE,qty,fat,snf,SUM(1) as a from TSPL_VLC_DATA_UPLOADER 
            'WHERE File_Date>='" + clsCommon.GetPrintDate(txtFromDate, "dd/MMM/yyyy") + "' and MCC_Code='" + strMCCCode + "'  group by MCC_Code,File_Date,shift,VLC_CODE,MP_CODE,qty,fat,snf HAVING SUM(1)>1
            ')x where MinDoc<>MaxDoc)"
            '                clsDBFuncationality.ExecuteNonQuery(qry)
            '            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function VSPQry(ByVal arrMCC As ArrayList, ByVal arrVSP As ArrayList, ByVal isPickPendingMilkSRNinNextPaymentCycle As Boolean, ByVal txtFromDate As DateTime, ByVal txtToDate As DateTime, ByVal Formcode As String) As String
        Dim qry As String = "select xx.VSP_CODE as Code,TSPL_VENDOR_MASTER.Vendor_Name as Name,xx.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,xx.ROUTE_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name from (" + Environment.NewLine +
            " select VSP_CODE,max(VLC_CODE)as VLC_CODE,ROUTE_CODE from (" + Environment.NewLine +
            " select VSP_CODE,VLC_CODE as VLC_CODE,ROUTE_CODE from TSPL_MILK_SRN_Head where 2=2  " + Environment.NewLine

        qry += " and MCC_CODE in (" + clsCommon.GetMulcallString(arrMCC) + ")"

        If Not isPickPendingMilkSRNinNextPaymentCycle Then
            qry += " and DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " and DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
        " union all " + Environment.NewLine +
        " select VSP_CODE,VLC_CODE as VLC_CODE,ROUTE_CODE from TSPL_MILK_REJECT_DETAIL left outer join TSPL_MILK_REJECT_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODE=TSPL_MILK_REJECT_DETAIL.DOC_CODE where TSPL_MILK_REJECT_HEAD.Posted=1 "

        qry += " and TSPL_MILK_REJECT_HEAD.MCC_CODE in (" + clsCommon.GetMulcallString(arrMCC) + ")"


        If Not isPickPendingMilkSRNinNextPaymentCycle Then
            qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate), "dd/MMM/yyyy hh:mm tt") + "'"
        End If
        qry += " and TSPL_MILK_REJECT_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate), "dd/MMM/yyyy hh:mm tt") + "' " + Environment.NewLine +
        " )x group by VSP_CODE,ROUTE_CODE " + Environment.NewLine
        If arrVSP IsNot Nothing AndAlso arrVSP.Count > 0 Then
            qry += " having VSP_CODE in (" + clsCommon.GetMulcallString(arrVSP) + ") "
        End If

        qry += " )xx " + Environment.NewLine +
        " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.VSP_CODE " + Environment.NewLine &
        " left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xx.VLC_CODE " + Environment.NewLine &
        " left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=xx.ROUTE_CODE" + Environment.NewLine & ""

        '' done by Panch Raj to distinguish vsp bill and farmer bill 
        If clsCommon.CompairString(Formcode, clsUserMgtCode.MPBillGeneration) = CompairStringResult.Equal Then
            qry = qry & " where coalesce(TSPL_VENDOR_MASTER.VSP_Farmer_Billing,0)=1"
        Else
            qry = qry & " where coalesce(TSPL_VENDOR_MASTER.VSP_Farmer_Billing,0)=0 and isnull(TSPL_VENDOR_MASTER.is_Drip_Saver,'')<>'Y' "
        End If
        qry = qry & " order by xx.VSP_CODE "
        Return qry
    End Function
End Class