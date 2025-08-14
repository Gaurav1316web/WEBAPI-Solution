Imports common
Imports System.IO
Public Class FrmOneDayStop
    Inherits FrmMainTranScreen
    Private Sub FrmOneDayStop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
    End Sub


    Sub SetToDate()
        'If MultipleFinderFillAuto Then
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0

        'If clsCommon.myLen(fndLoc.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
        '    clsCommon.MyMessageBoxShow("Please select the Location first")
        '    Exit Sub
        'End If
        'If MultipleFinderFillAuto = True Then
        '    If mfndMcc.arrValueMember Is Nothing OrElse mfndMcc.arrValueMember.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("Please select the Location first")
        '        Exit Sub
        '    End If
        'End If
        Dim strMCCcode = ""


        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.MCC_Code  in (select Location_Code  from TSPL_LOCATION_MASTER where " + strMCCcode + "  Location_Category='MCC' and Rejected_Type='N') ")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If fromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                fromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                ToDate.Value = fromDate.Value
                Exit Sub
            End If
            ToDate.Value = fromDate.Value.AddDays(PaymentCycleValue - 1)

            If fromDate.Value.Month <> ToDate.Value.Month Then
                ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
            Dim dtNxtPay As DateTime = ToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
            If fromDate.Value.Month <> dtNxtPay.Month Then
                ToDate.Value = New Date(fromDate.Value.Year, fromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            ToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type")
                fromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                ToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            ToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, fromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
            Dim today As Date = fromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            fromDate.Value = today.AddDays(-dayDiff)
            ToDate.Value = fromDate.Value.AddDays(6)
        End If
        ' End If
        'If clsCommon.myLen(txtMCC.Text) > 0 Then
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(txtMCC.Text, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(txtMCC.Text, dtpToDate.Value)
        'Else
        '    txtPaymentCycleNo.Text = clsGenratePaymentCycles.GetPaymentCycleNo(fndLoc.Value, dtpToDate.Value)
        '    txtFiscalYear.Text = clsGenratePaymentCycles.GetPaymentFiscalCode(fndLoc.Value, dtpToDate.Value)
        'End If
        'End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False, Nothing, Nothing)
    End Sub
    Public Sub Print(ByVal isPrint As Boolean, ByVal BankAdviseDocNo As String, ByVal MCC As String)
        Try
            Dim qry As String = Nothing
            qry = "  select ROW_NUMBER() Over (Order By (Select 1)) As SNo, TSPL_COMPANY_MASTER.Comp_Name,
 TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
 ,Final1.DCS_Name,Final1.Bill_Amt,Final1.SavingAccountNo,Final1.SavingAmt,Final1.CurrentAccountNo,Final1.CurrentAmt
from (Select xxxFinal.[Bank Advise No],xxxFinal.[Bank Advise Date],xxxfinal.VLC_CODE_Uploader,	xxxfinal.BankCode,	xxxfinal.BankName,	xxxfinal.DCS_Name,	Round(xxxfinal.Bill_Amt,0)Bill_Amt,	xxxfinal.SavingAccountNo,	Round(xxxfinal.SavingAmt,0)SavingAmt,	xxxfinal.CurrentAccountNo,	Case When xxxfinal.CurrentAmt>0 Then Round(xxxfinal.CurrentAmt,0) Else 0 End As CurrentAmt,xxxDCSCount.DCSCount 
                            from 
                            (Select [Bank Advise No],[Bank Advise Date],xxxSaving.VLC_CODE_Uploader,IsNull(xxxSaving.Bank_Code,xxxCurrent.Bank_Code) As BankCode,IsNull(xxxsaving.Bank_Code_Desc,xxxCurrent.Bank_Code_Desc) As BankName,(xxxSaving.VLC_CODE_Uploader +' '+ xxxSaving.Payee_Joint_Name) As DCS_Name,
							Case When IsNull(xxxCurrent.Payable_Amount,0)>0 Then IsNull(xxxCurrent.Payable_Amount,0) Else IsNull(xxxSaving.Payable_Amount,0) End As Bill_Amt, 
                            Case When xxxSaving.Account_Type='Sav' Then xxxSaving.Payee_Joint_Account_No Else '' End As SavingAccountNo,
							IsNull(xxxSaving.Payable_Amount,0) As SavingAmt,
                            Case When xxxCurrent.Account_Type='Cur' Then xxxCurrent.Payee_Joint_Account_No Else '' End As CurrentAccountNo,
							IsNull(xxxCurrent.Payable_Amount,0)-IsNull(xxxSaving.Payable_Amount,0) As CurrentAmt
                            from (select Max(AccountType)As Account_Type, x.GRPColumn,max(x.From_Date)From_Date,max(x.Doc_No)Doc_No,max(x.Fiscal_Name)Fiscal_Name,max(x.Date_Range)Date_Range,x.VLC_CODE_Uploader,
                            max(x.Payee_Joint_Name)Payee_Joint_Name,max(x.Bank_Code)Bank_Code,max(x.Branch_Name)Branch_Name,max(x.Bank_Code_Desc)Bank_Code_Desc,
                            max(x.Payee_Joint_IFSC_Code)Payee_Joint_IFSC_Code,x.Payee_Joint_Account_No,sum(x.Payable_Amount)Payable_Amount 
                            from
                            (select  Case When TSPL_VENDOR_MASTER.Account_Type='sav' Then TSPL_VENDOR_MASTER.Account_Type Else Case When TSPL_VENDOR_MASTER.AccountType2='sav' Then TSPL_VENDOR_MASTER.AccountType2 else '' End  End As AccountType ,'' AS CycleRange, TSPL_Vendor_MASTER.Bank_Code+(case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')='Self' then (TSPL_VENDOR_MASTER.IFSC_Code)   else (TSPL_VENDOR_MASTER.Joint_IFSC_Code)   end)  as GRPColumn,
                            TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],TSPL_MCC_MASTER.MCC_NAME, TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader, (TSPL_VENDOR_MASTER.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
                              TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code_Desc,
                              case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')= 'Self' then (TSPL_VENDOR_MASTER.IFSCCode2) else (TSPL_VENDOR_MASTER.Joint_IFSC_Code) end as Payee_Joint_IFSC_Code, 
                              case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')= 'Self' then (TSPL_VENDOR_MASTER.AccNo2) else (TSPL_VENDOR_MASTER.Joint_Account_No) end as Payee_Joint_Account_No, 
                              TSPL_VENDOR_INVOICE_HEAD.Document_Total as Payable_Amount   
                              from TSPL_PAYMENT_PROCESS_SAVING 
                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_SAVING.Doc_No
                            left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No                                                       
                            left outer join (select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code --TSPL_PAYMENT_PROCESS_COMPULSORY.VSP_CODE
                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date    
                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank
                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
                            where TSPL_PAYMENT_PROCESS_HEAD.isPrePosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'     and	TSPL_PAYMENT_PROCESS_HEAD.To_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                            and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 --and TSPL_Vendor_MASTER.Bank_Code='504' 
                            ) x group by x.GRPColumn,x.VLC_CODE_Uploader,x.Payee_Joint_Account_No )xxxSaving
                            Left Outer Join
                            (select MAX([Bank Advise No])[Bank Advise No],MAX([Bank Advise Date])[Bank Advise Date],Max(Account_Type)As Account_Type,max(x.From_Date)From_Date,max(x.Doc_No)Doc_No,max(x.Fiscal_Name)Fiscal_Name,max(x.Date_Range)Date_Range,x.VLC_CODE_Uploader,max(x.Payee_Joint_Name)Payee_Joint_Name,max(x.Bank_Code)Bank_Code,max(x.Branch_Name)Branch_Name,max(x.Bank_Code_Desc)Bank_Code_Desc,max(x.Payee_Joint_IFSC_Code)Payee_Joint_IFSC_Code,x.Payee_Joint_Account_No,sum(x.Payable_Amount)Payable_Amount from
                            (select  TSPL_BANK_ADVISE.Document_No As [Bank Advise No],Convert(Varchar(10),TSPL_BANK_ADVISE.Document_Date,103) As [Bank Advise Date],Case When TSPL_VENDOR_MASTER.Account_Type='cur' Then TSPL_VENDOR_MASTER.Account_Type Else Case When TSPL_VENDOR_MASTER.AccountType2='cur' Then TSPL_VENDOR_MASTER.AccountType2 else '' End  End As Account_Type,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name,
                            convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, 
                            TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,
                            case when isnull(TSPL_Vendor_MASTER.Bank_Name,'')  = '' then  TSPL_Vendor_MASTER.Bank_Code else TSPL_Vendor_MASTER.Bank_Name end as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,
                            (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)+isnull(TSPL_PAYMENT_PROCESS_DETAIL.Saving_Amount,0))  as Payable_Amount 
                            from TSPL_PAYMENT_PROCESS_DETAIL 
                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE								
                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current 
                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
                            --left outer join TSPL_TRANSFER_TO_SAVING_DETAIL  on TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code = TSPL_TRANSFER_TO_SAVING_DETAIL.Vendor_Code 
                           left outer join( select Vendor_Code,Max(Amount)Head_Load_Rate from TSPL_TRANSFER_TO_SAVING_DETAIL left outer join TSPL_TRANSFER_TO_SAVING on TSPL_TRANSFER_TO_SAVING.Document_No = TSPL_TRANSFER_TO_SAVING_DETAIL.Document_No
                           where convert(date,TSPL_TRANSFER_TO_SAVING.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'   ,103) and convert(date,TSPL_TRANSFER_TO_SAVING.Document_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  ,103)
                           group by Vendor_Code )SavingDetail on TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code = SavingDetail.Vendor_Code
                            left outer join TSPL_BANK_ADVISE On TSPL_BANK_ADVISE.Payment_Process_Document_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No     
                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                            where TSPL_PAYMENT_PROCESS_HEAD.isPrePosted = 1 and  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'    and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'    ) x group by x.VLC_CODE_Uploader,x.Payee_Joint_Account_No ) xxxCurrent On xxxCurrent.VLC_CODE_Uploader=xxxSaving.VLC_CODE_Uploader) xxxFinal
                            Inner Join(SELECT YYY.[Vlc Uploader Code],YYY.[VLC Name],COUNT(YYY.[Vlc Uploader Code]) As DCSCount FROM (Select final.[Doc Date] ,(final.[Vlc Uploader Code])[Vlc Uploader Code] ,final.[VLC Name] From 
                            (Select Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], 
                            TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name]
                            From TSPL_MILK_SRN_DETAIL 
                            Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
                            Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                            Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                            left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                            left outer join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SRN_HEAD.ROUTE_CODE 
                            Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                             where 2 = 2  and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'   and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                             ) As final
                             where 2=2  GROUP BY FINAL.[Doc Date],FINAL.[VLC Name],FINAL.[Vlc Uploader Code] )YYY  GROUP BY [VLC Name],[Vlc Uploader Code])xxxDCSCount On xxxDCSCount.[Vlc Uploader Code]=xxxFinal.VLC_CODE_Uploader
						  )Final1 Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
							"
            '            qry = " Select ROW_NUMBER() Over (Order By (Select 1)) As SNo,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + " '+' to '+'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' ) As DateRange,Final.*,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address,case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone ,TSPL_COMPANY_MASTER.Regn_No
            '                            ,'GSTIN : '+ TSPL_COMPANY_MASTER.GSTReg_No as GSTReg_No,TSPL_COMPANY_MASTER.Pincode,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2   from ( Select xxx.BankCode,Max(xxx.BankName)BankName,Sum(xxx.Bill_Amt)TotalBillAmt,Sum(xxx.SavingAmt)TotalSavingAmt,Sum(xxx.CurrentAmt)CurrentAmt from (Select * from (Select xxxFinal.[Bank Advise No],xxxFinal.[Bank Advise Date],xxxfinal.VLC_CODE_Uploader,	xxxfinal.BankCode,	xxxfinal.BankName,	xxxfinal.DCS_Name,	Round(xxxfinal.Bill_Amt,0)Bill_Amt,	xxxfinal.SavingAccountNo,	Round(xxxfinal.SavingAmt,0)SavingAmt,	xxxfinal.CurrentAccountNo,	Case When xxxfinal.CurrentAmt>0 Then Round(xxxfinal.CurrentAmt,0) Else 0 End As CurrentAmt,xxxDCSCount.DCSCount 
            '                            from 
            '                            (Select [Bank Advise No],[Bank Advise Date],xxxSaving.VLC_CODE_Uploader,IsNull(xxxSaving.Bank_Code,xxxCurrent.Bank_Code) As BankCode,IsNull(xxxsaving.Bank_Code_Desc,xxxCurrent.Bank_Code_Desc) As BankName,(xxxSaving.VLC_CODE_Uploader +' '+ xxxSaving.Payee_Joint_Name) As DCS_Name,
            '							Case When IsNull(xxxCurrent.Payable_Amount,0)>0 Then IsNull(xxxCurrent.Payable_Amount,0) Else IsNull(xxxSaving.Payable_Amount,0) End As Bill_Amt, 
            '                            Case When xxxSaving.Account_Type='Sav' Then xxxSaving.Payee_Joint_Account_No Else '' End As SavingAccountNo,
            '							IsNull(xxxSaving.Payable_Amount,0) As SavingAmt,
            '                            Case When xxxCurrent.Account_Type='Cur' Then xxxCurrent.Payee_Joint_Account_No Else '' End As CurrentAccountNo,
            '							IsNull(xxxCurrent.Payable_Amount,0)-IsNull(xxxSaving.Payable_Amount,0) As CurrentAmt
            '                            from (select Max(AccountType)As Account_Type, x.GRPColumn,max(x.From_Date)From_Date,max(x.Doc_No)Doc_No,max(x.Fiscal_Name)Fiscal_Name,max(x.Date_Range)Date_Range,x.VLC_CODE_Uploader,
            '                            max(x.Payee_Joint_Name)Payee_Joint_Name,max(x.Bank_Code)Bank_Code,max(x.Branch_Name)Branch_Name,max(x.Bank_Code_Desc)Bank_Code_Desc,
            '                            max(x.Payee_Joint_IFSC_Code)Payee_Joint_IFSC_Code,x.Payee_Joint_Account_No,sum(x.Payable_Amount)Payable_Amount 
            '                            from
            '                            (select  Case When TSPL_VENDOR_MASTER.Account_Type='sav' Then TSPL_VENDOR_MASTER.Account_Type Else Case When TSPL_VENDOR_MASTER.AccountType2='sav' Then TSPL_VENDOR_MASTER.AccountType2 else '' End  End As AccountType ,'' AS CycleRange, TSPL_Vendor_MASTER.Bank_Code+(case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')='Self' then (TSPL_VENDOR_MASTER.IFSC_Code)   else (TSPL_VENDOR_MASTER.Joint_IFSC_Code)   end)  as GRPColumn,
            '                            TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],TSPL_MCC_MASTER.MCC_NAME, TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader, (TSPL_VENDOR_MASTER.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
            '                              TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code_Desc,
            '                              case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')= 'Self' then (TSPL_VENDOR_MASTER.IFSCCode2) else (TSPL_VENDOR_MASTER.Joint_IFSC_Code) end as Payee_Joint_IFSC_Code, 
            '                              case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')= 'Self' then (TSPL_VENDOR_MASTER.AccNo2) else (TSPL_VENDOR_MASTER.Joint_Account_No) end as Payee_Joint_Account_No, 
            '                              TSPL_VENDOR_INVOICE_HEAD.Document_Total as Payable_Amount   
            '                              from TSPL_PAYMENT_PROCESS_SAVING 
            '                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_SAVING.Doc_No
            '                            left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No                                                       
            '                            left outer join (select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
            '                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code --TSPL_PAYMENT_PROCESS_COMPULSORY.VSP_CODE
            '                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date    
            '                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
            '                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank
            '                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
            '                            where TSPL_PAYMENT_PROCESS_HEAD.isPrePosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and	TSPL_PAYMENT_PROCESS_HEAD.To_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
            '                            and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 --and TSPL_Vendor_MASTER.Bank_Code='504' 
            '                            ) x group by x.GRPColumn,x.VLC_CODE_Uploader,x.Payee_Joint_Account_No )xxxSaving
            '                            Left Outer Join
            '                            (select MAX([Bank Advise No])[Bank Advise No],MAX([Bank Advise Date])[Bank Advise Date],Max(Account_Type)As Account_Type,max(x.From_Date)From_Date,max(x.Doc_No)Doc_No,max(x.Fiscal_Name)Fiscal_Name,max(x.Date_Range)Date_Range,x.VLC_CODE_Uploader,max(x.Payee_Joint_Name)Payee_Joint_Name,max(x.Bank_Code)Bank_Code,max(x.Branch_Name)Branch_Name,max(x.Bank_Code_Desc)Bank_Code_Desc,max(x.Payee_Joint_IFSC_Code)Payee_Joint_IFSC_Code,x.Payee_Joint_Account_No,sum(x.Payable_Amount)Payable_Amount from
            '                            (select  TSPL_BANK_ADVISE.Document_No As [Bank Advise No],Convert(Varchar(10),TSPL_BANK_ADVISE.Document_Date,103) As [Bank Advise Date],Case When TSPL_VENDOR_MASTER.Account_Type='cur' Then TSPL_VENDOR_MASTER.Account_Type Else Case When TSPL_VENDOR_MASTER.AccountType2='cur' Then TSPL_VENDOR_MASTER.AccountType2 else '' End  End As Account_Type,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name,
            '                            convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, 
            '                            TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,
            '                            case when isnull(TSPL_Vendor_MASTER.Bank_Name,'')  = '' then  TSPL_Vendor_MASTER.Bank_Code else TSPL_Vendor_MASTER.Bank_Name end as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,
            '                            (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)+isnull(TSPL_PAYMENT_PROCESS_DETAIL.Saving_Amount,0))  as Payable_Amount 
            '                            from TSPL_PAYMENT_PROCESS_DETAIL 
            '                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
            '                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE								
            '                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
            '                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current 
            '                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
            '                            --left outer join TSPL_TRANSFER_TO_SAVING_DETAIL  on TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code = TSPL_TRANSFER_TO_SAVING_DETAIL.Vendor_Code 
            '                           left outer join( select Vendor_Code,Max(Amount)Head_Load_Rate from TSPL_TRANSFER_TO_SAVING_DETAIL left outer join TSPL_TRANSFER_TO_SAVING on TSPL_TRANSFER_TO_SAVING.Document_No = TSPL_TRANSFER_TO_SAVING_DETAIL.Document_No
            '                           where convert(date,TSPL_TRANSFER_TO_SAVING.Document_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'),103) and convert(date,TSPL_TRANSFER_TO_SAVING.Document_Date,103) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
            '                           group by Vendor_Code )SavingDetail on TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code = SavingDetail.Vendor_Code
            '                            left outer join TSPL_BANK_ADVISE On TSPL_BANK_ADVISE.Payment_Process_Document_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No     
            '                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
            '                            where TSPL_PAYMENT_PROCESS_HEAD.isPrePosted = 1 and  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='16/Sep/2024 12:00:00 AM' and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='30/Sep/2024 11:59:59 PM'  ) x group by x.VLC_CODE_Uploader,x.Payee_Joint_Account_No ) xxxCurrent On xxxCurrent.VLC_CODE_Uploader=xxxSaving.VLC_CODE_Uploader) xxxFinal
            '                            Inner Join(SELECT YYY.[Vlc Uploader Code],YYY.[VLC Name],COUNT(YYY.[Vlc Uploader Code]) As DCSCount FROM (Select final.[Doc Date] ,(final.[Vlc Uploader Code])[Vlc Uploader Code] ,final.[VLC Name] From 
            '                            (Select Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], 
            '                            TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name]
            '                            From TSPL_MILK_SRN_DETAIL 
            '                            Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
            '                            Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
            '                            Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
            '                            left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
            '                            left outer join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SRN_HEAD.ROUTE_CODE 
            '                            Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
            '                             where 2 = 2  
            'And Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 

            '                             ) As final
            '                             where 2=2  GROUP BY FINAL.[Doc Date],FINAL.[VLC Name],FINAL.[Vlc Uploader Code] )YYY  GROUP BY [VLC Name],[Vlc Uploader Code])xxxDCSCount On xxxDCSCount.[Vlc Uploader Code]=xxxFinal.VLC_CODE_Uploader  )xxxfinal )xxx
            '                            where xxx.DCSCount>1 Group By xxx.BankCode)Final Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='ALW'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterTemplate.FilterDescriptors.Clear()
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            gv1.DataSource = dt
            RadPageView1.SelectedPage = RadPageViewPage2
            SetGridFormat()
            'If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
            'Dim frmCRV As New frmCrystalReportViewer()

            'frmCRV.funreport(False, CrystalReportFolder.MilkProcurement, dt, "crptBankAdviseSubReportOneDayStopALW", "One Day Stop")

            'End If

        Catch ex As Exception

        End Try
    End Sub
    Public Function GetOneDayStop() As String
        Dim qry As String = Nothing
        qry = "  select ROW_NUMBER() Over (Order By (Select 1)) As SNo, TSPL_COMPANY_MASTER.Comp_Name,
 TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end as Comp_address
 ,Final1.DCS_Name,Final1.Bill_Amt,Final1.SavingAccountNo,Final1.SavingAmt,Final1.CurrentAccountNo,Final1.CurrentAmt
from (Select xxxFinal.[Bank Advise No],xxxFinal.[Bank Advise Date],xxxfinal.VLC_CODE_Uploader,	xxxfinal.BankCode,	xxxfinal.BankName,	xxxfinal.DCS_Name,	Round(xxxfinal.Bill_Amt,0)Bill_Amt,	xxxfinal.SavingAccountNo,	Round(xxxfinal.SavingAmt,0)SavingAmt,	xxxfinal.CurrentAccountNo,	Case When xxxfinal.CurrentAmt>0 Then Round(xxxfinal.CurrentAmt,0) Else 0 End As CurrentAmt,xxxDCSCount.DCSCount 
                            from 
                            (Select [Bank Advise No],[Bank Advise Date],xxxSaving.VLC_CODE_Uploader,IsNull(xxxSaving.Bank_Code,xxxCurrent.Bank_Code) As BankCode,IsNull(xxxsaving.Bank_Code_Desc,xxxCurrent.Bank_Code_Desc) As BankName,(xxxSaving.VLC_CODE_Uploader +' '+ xxxSaving.Payee_Joint_Name) As DCS_Name,
							Case When IsNull(xxxCurrent.Payable_Amount,0)>0 Then IsNull(xxxCurrent.Payable_Amount,0) Else IsNull(xxxSaving.Payable_Amount,0) End As Bill_Amt, 
                            Case When xxxSaving.Account_Type='Sav' Then xxxSaving.Payee_Joint_Account_No Else '' End As SavingAccountNo,
							IsNull(xxxSaving.Payable_Amount,0) As SavingAmt,
                            Case When xxxCurrent.Account_Type='Cur' Then xxxCurrent.Payee_Joint_Account_No Else '' End As CurrentAccountNo,
							IsNull(xxxCurrent.Payable_Amount,0)-IsNull(xxxSaving.Payable_Amount,0) As CurrentAmt
                            from (select Max(AccountType)As Account_Type, x.GRPColumn,max(x.From_Date)From_Date,max(x.Doc_No)Doc_No,max(x.Fiscal_Name)Fiscal_Name,max(x.Date_Range)Date_Range,x.VLC_CODE_Uploader,
                            max(x.Payee_Joint_Name)Payee_Joint_Name,max(x.Bank_Code)Bank_Code,max(x.Branch_Name)Branch_Name,max(x.Bank_Code_Desc)Bank_Code_Desc,
                            max(x.Payee_Joint_IFSC_Code)Payee_Joint_IFSC_Code,x.Payee_Joint_Account_No,sum(x.Payable_Amount)Payable_Amount 
                            from
                            (select  Case When TSPL_VENDOR_MASTER.Account_Type='sav' Then TSPL_VENDOR_MASTER.Account_Type Else Case When TSPL_VENDOR_MASTER.AccountType2='sav' Then TSPL_VENDOR_MASTER.AccountType2 else '' End  End As AccountType ,'' AS CycleRange, TSPL_Vendor_MASTER.Bank_Code+(case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')='Self' then (TSPL_VENDOR_MASTER.IFSC_Code)   else (TSPL_VENDOR_MASTER.Joint_IFSC_Code)   end)  as GRPColumn,
                            TSPL_BANK_MASTER.DESCRIPTION as [Company Bank], TSPL_BANK_MASTER.BANKACCNUMBER as [Company Bank Account No],TSPL_MCC_MASTER.MCC_NAME, TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name,TSPL_PAYMENT_CYCLE_GENERATED.Name as CycleNo ,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as VLC_CODE_Uploader, (TSPL_VENDOR_MASTER.VSP_Payee_Name)  as Payee_Joint_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code, 
                              TSPL_VENDOR_MASTER.BankBranch2 as Branch_Name,TSPL_Vendor_MASTER.BankCode2 as Bank_Code_Desc,
                              case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')= 'Self' then (TSPL_VENDOR_MASTER.IFSCCode2) else (TSPL_VENDOR_MASTER.Joint_IFSC_Code) end as Payee_Joint_IFSC_Code, 
                              case when isnull(TSPL_VENDOR_MASTER.vsp_payment,'')= 'Self' then (TSPL_VENDOR_MASTER.AccNo2) else (TSPL_VENDOR_MASTER.Joint_Account_No) end as Payee_Joint_Account_No, 
                              TSPL_VENDOR_INVOICE_HEAD.Document_Total as Payable_Amount   
                              from TSPL_PAYMENT_PROCESS_SAVING 
                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_SAVING.Doc_No
                            left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No                                                       
                            left outer join (select distinct VSP_Code, VLC_Code_VLC_Uploader  from TSPL_VLC_MASTER_HEAD) as TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code =  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code= TSPL_VENDOR_INVOICE_HEAD.Vendor_Code --TSPL_PAYMENT_PROCESS_COMPULSORY.VSP_CODE
                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date    
                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank
                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
                            where TSPL_PAYMENT_PROCESS_HEAD.isPrePosted = 1 and TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'     and	TSPL_PAYMENT_PROCESS_HEAD.To_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' 
                            and TSPL_VENDOR_INVOICE_HEAD.Document_Total>0 --and TSPL_Vendor_MASTER.Bank_Code='504' 
                            ) x group by x.GRPColumn,x.VLC_CODE_Uploader,x.Payee_Joint_Account_No )xxxSaving
                            Left Outer Join
                            (select MAX([Bank Advise No])[Bank Advise No],MAX([Bank Advise Date])[Bank Advise Date],Max(Account_Type)As Account_Type,max(x.From_Date)From_Date,max(x.Doc_No)Doc_No,max(x.Fiscal_Name)Fiscal_Name,max(x.Date_Range)Date_Range,x.VLC_CODE_Uploader,max(x.Payee_Joint_Name)Payee_Joint_Name,max(x.Bank_Code)Bank_Code,max(x.Branch_Name)Branch_Name,max(x.Bank_Code_Desc)Bank_Code_Desc,max(x.Payee_Joint_IFSC_Code)Payee_Joint_IFSC_Code,x.Payee_Joint_Account_No,sum(x.Payable_Amount)Payable_Amount from
                            (select  TSPL_BANK_ADVISE.Document_No As [Bank Advise No],Convert(Varchar(10),TSPL_BANK_ADVISE.Document_Date,103) As [Bank Advise Date],Case When TSPL_VENDOR_MASTER.Account_Type='cur' Then TSPL_VENDOR_MASTER.Account_Type Else Case When TSPL_VENDOR_MASTER.AccountType2='cur' Then TSPL_VENDOR_MASTER.AccountType2 else '' End  End As Account_Type,TSPL_PAYMENT_PROCESS_HEAD.From_Date,TSPL_PAYMENT_PROCESS_HEAD.Doc_No, TSPL_Fiscal_Year_Master.Fiscal_Name,
                            convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, 
                            TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_VENDOR_MASTER.Branch_Name,
                            case when isnull(TSPL_Vendor_MASTER.Bank_Name,'')  = '' then  TSPL_Vendor_MASTER.Bank_Code else TSPL_Vendor_MASTER.Bank_Name end as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,
                            (isnull(TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,0)+isnull(TSPL_PAYMENT_PROCESS_DETAIL.Saving_Amount,0))  as Payable_Amount 
                            from TSPL_PAYMENT_PROCESS_DETAIL 
                            left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                            left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE								
                            left outer join TSPL_Fiscal_Year_Master on TSPL_Fiscal_Year_Master.Start_Date<=TSPL_PAYMENT_PROCESS_HEAD.From_Date and TSPL_Fiscal_Year_Master.End_Date>=TSPL_PAYMENT_PROCESS_HEAD.From_Date
                            left outer join TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE = TSPL_Vendor_MASTER.Company_Bank_Current 
                            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
                            --left outer join TSPL_TRANSFER_TO_SAVING_DETAIL  on TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code = TSPL_TRANSFER_TO_SAVING_DETAIL.Vendor_Code 
                           left outer join( select Vendor_Code,Max(Amount)Head_Load_Rate from TSPL_TRANSFER_TO_SAVING_DETAIL left outer join TSPL_TRANSFER_TO_SAVING on TSPL_TRANSFER_TO_SAVING.Document_No = TSPL_TRANSFER_TO_SAVING_DETAIL.Document_No
                           where convert(date,TSPL_TRANSFER_TO_SAVING.Document_Date,103)>=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'   ,103) and convert(date,TSPL_TRANSFER_TO_SAVING.Document_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  ,103)
                           group by Vendor_Code )SavingDetail on TSPL_PAYMENT_PROCESS_DETAIL.VSP_Code = SavingDetail.Vendor_Code
                            left outer join TSPL_BANK_ADVISE On TSPL_BANK_ADVISE.Payment_Process_Document_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No     
                            left outer join TSPL_PAYMENT_CYCLE_GENERATED on convert(date, TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103)<=convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) and convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103)>=convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103)   and TSPL_PAYMENT_CYCLE_GENERATED.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   
                            where TSPL_PAYMENT_PROCESS_HEAD.isPrePosted = 1 and  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'    and	TSPL_PAYMENT_PROCESS_HEAD.To_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'    ) x group by x.VLC_CODE_Uploader,x.Payee_Joint_Account_No ) xxxCurrent On xxxCurrent.VLC_CODE_Uploader=xxxSaving.VLC_CODE_Uploader) xxxFinal
                            Inner Join(SELECT YYY.[Vlc Uploader Code],YYY.[VLC Name],COUNT(YYY.[Vlc Uploader Code]) As DCSCount FROM (Select final.[Doc Date] ,(final.[Vlc Uploader Code])[Vlc Uploader Code] ,final.[VLC Name] From 
                            (Select Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], 
                            TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name]
                            From TSPL_MILK_SRN_DETAIL 
                            Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
                            Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                            Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                            left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                            left outer join TSPL_BULK_ROUTE_MASTER On TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_SRN_HEAD.ROUTE_CODE 
                            Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                             where 2 = 2  and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'   and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  
                             ) As final
                             where 2=2  GROUP BY FINAL.[Doc Date],FINAL.[VLC Name],FINAL.[Vlc Uploader Code] )YYY  GROUP BY [VLC Name],[Vlc Uploader Code])xxxDCSCount On xxxDCSCount.[Vlc Uploader Code]=xxxFinal.VLC_CODE_Uploader
						  )Final1 Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1='" + objCommonVar.CurrComp_Code1 + "'
							"
        Return qry
    End Function
    Sub SetGridFormat()
        gv1.ShowGroupPanel = False
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("Comp_Name").HeaderText = "Comp_Name"
        gv1.Columns("Comp_Name").IsVisible = False
        gv1.Columns("Comp_address").HeaderText = "Comp address"
        gv1.Columns("Comp_address").IsVisible = False

        gv1.Columns("SNo").HeaderText = "SNo"
        gv1.Columns("SNo").IsVisible = True

        gv1.Columns("DCS_Name").HeaderText = "DCS Name"
        gv1.Columns("DCS_Name").IsVisible = True

        gv1.Columns("Bill_Amt").HeaderText = "Bill Amt"
        gv1.Columns("Bill_Amt").IsVisible = True

        gv1.Columns("SavingAccountNo").HeaderText = "SavingAccountNo"
        gv1.Columns("SavingAccountNo").IsVisible = True

        gv1.Columns("SavingAmt").HeaderText = "SavingAmt"
        gv1.Columns("SavingAmt").IsVisible = True

        gv1.Columns("CurrentAccountNo").HeaderText = "CurrentAccountNo"
        gv1.Columns("CurrentAccountNo").IsVisible = True

        gv1.Columns("CurrentAmt").HeaderText = "CurrentAmt"
        gv1.Columns("CurrentAmt").IsVisible = True


        Dim summaryRowItemB As New GridViewSummaryRowItem()
            'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)
            Dim Bill_Amt As New GridViewSummaryItem("Bill_Amt", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(Bill_Amt)
            Dim SavingAmt As New GridViewSummaryItem("SavingAmt", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(SavingAmt)
            Dim CurrentAmt As New GridViewSummaryItem("CurrentAmt", "{0:n2}", GridAggregateFunction.Sum)
            summaryRowItemB.Add(CurrentAmt)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
            gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub

    Private Sub fromDate_Leave_1(sender As Object, e As EventArgs) Handles fromDate.Leave
        SetToDate()
    End Sub

    Private Sub fromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles fromDate.Validating
        SetToDate()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select City_Code from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
            Dim strHeading As String = ""
            strHeading = clsCommon.myCstr("One Stop Day ")
            strHeading = clsCommon.myCstr("from " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + " for '" + strUnit + "' unit  ")

            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add(("Date: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " to " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "   "))
            'arrHeader.Add(("From Date: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "  "))

            'If MultipleFinderFillAuto Then
            '    'If rbtnSaving.IsChecked = True Then
            '    '    arrHeader.Add("Data: Saving ")
            '    'End If
            '    'If rbtnBankAdvice.IsChecked = True Then
            '    '    arrHeader.Add("Report Type: Bank Advice")
            '    'End If
            '    'If rbtnBankWiseSummary.IsChecked = True Then
            '    '    arrHeader.Add("Report Type: Bank Wise Summary")
            '    'End If
            'Else
            '    arrHeader.Add("Mcc : " + txtMCC.Value)
            'End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(strHeading, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF(strHeading, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(GetOneDayStop())
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(MyBase.Form_ID, False, CrystalReportFolder.MilkProcurement, dt, "crptBankAdviseSubReportOneDayStopALW", "One Day Stop")
            Else
                Throw New Exception("Data not Found!")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class