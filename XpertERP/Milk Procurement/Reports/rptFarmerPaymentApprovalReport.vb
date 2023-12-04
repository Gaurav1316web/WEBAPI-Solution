Imports common
Imports System.IO

' Date- 02-Sep-2020  by Sanjay - Create new report 
Public Class rptFarmerPaymentApprovalReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        GroupBox1.Enabled = False
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub
    Sub Reset()
        fndMCC.Value = ""
        lblMCC.Text = ""
        fndDocument.Value = ""
        dtpFromDate.Value = Nothing
        dtpToDate.Value = Nothing
        EnableDisiablecontrol(True)
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            If clsCommon.myLen(fndDocument.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Select Document No First", Me.Text)
                Return
            End If
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable
            'Change by sanjay,Match with Abstract report,SPMMD, Asked By Ranjana Mam
            '",((sum(LastOuter.Amount)+sum(LastOuter.[Incentive Amount])+0)-sum(Deduction_Amount)-sum([OutStanding Amount])-sum(LastOuter.Farmer_Sale_Amt)) as [Net Amount] " &
            qry = "select ROW_NUMBER () over (order by [VLC Code] ) As [S.No] " &
              ",max(LastOuter.[Incharge Code]) as [VSP Code] " &
              ",max(LastOuter.[Incharge Name]) as [VSP Name] " &
              ",LastOuter.[VLC Code] as [Code] " &
              ",max(LastOuter.[VLC Name]) [VLC Name] " &
              ",max(LastOuter.[Vendor Group Code Desc]) as [Type] " &
              ",max(LastOuter.VLC_CODE_VLC_Uploader) [VLC Code] " &
              ",convert ( decimal(18,3),sum(LastOuter.Qty)) as [Qty - Ltr] " &
              ",convert ( decimal(18,2), ((sum(LastOuter.Fat_KG) * 100) / sum(nullif (LastOuter.Qty_In_KG,0)) ) )  as [Fat %] " &
              ", convert (decimal(18,2), ((sum(LastOuter.SNF_KG) * 100 )/ sum(nullif (LastOuter.Qty_In_KG,0)))) [Snf %] " &
              ",convert ( decimal(18,2), ((sum(LastOuter.Fat_KG) * 100) / sum(nullif (LastOuter.Qty_In_KG,0)) ) + ((sum(LastOuter.SNF_KG) * 100 )/ sum(nullif (LastOuter.Qty_In_KG,0)))) [TS %] " &
              ",sum(LastOuter.Amount) as [Amount] " &
              ",sum(LastOuter.[Incentive Amount]) as [Incentive Amount] " &
                ",0 as [Arrear Amount] " &
              ",sum(LastOuter.Deduction_Amount) as [TS Deduction],sum(LastOuter.Total_Advance_Amount_Recovery) as [Advance Amount Recovery] ,sum(LastOuter.Total_Loan_Payment_Recovery) as [Loan Payment Recovery] " &
                ",convert ( decimal(18,2) ,(sum(LastOuter.Amount)+sum(LastOuter.[Incentive Amount])+0+SUM(DedAddAmount)-SUM(Deduction_Amount)) /nullif ( sum(LastOuter.Qty),0) ) as [Rate/Ltr] " &
                ",convert ( decimal(18,2) ,((sum(LastOuter.Amount)+sum(LastOuter.[Incentive Amount])+0+SUM(DedAddAmount)-SUM(Deduction_Amount)) /nullif ( sum(LastOuter.Qty),0)) " &
                " *100/ (((sum(LastOuter.Fat_KG) * 100) / sum(nullif (LastOuter.Qty_In_KG,0)) ) + ((sum(LastOuter.SNF_KG) * 100 )/ sum(nullif (LastOuter.Qty_In_KG,0))))) [TS Rate] " &
               ",sum([OutStanding Amount]) as [Adjustment Amount] " &
               ",sum(LastOuter.Farmer_Sale_Amt) as [Cattle Feed] " &
                ",sum(LastOuter.Payable_Amount) as [Net Amount] " &
                ",sum(nullif (LastOuter.Qty_In_KG,0)) as Qty_In_KG,sum(LastOuter.Fat_KG) as Fat_KG,sum(LastOuter.SNF_KG) as SNF_KG " &
            " from ( Select XXXX.VLC_CODE_VLC_Uploader,isnull(TBL_Farmer_Sale.Amount,0) as Farmer_Sale_Amt, isnull(DedAddAmount,0) as DedAddAmount, isnull(Deduction_Amount,0) as Deduction_Amount, isnull(Total_Advance_Amount_Recovery,0) as Total_Advance_Amount_Recovery, isnull(Total_Loan_Payment_Recovery,0) as Total_Loan_Payment_Recovery , isnull(Payable_Amount,0) as Payable_Amount,"
            qry += "'" + clsCommon.myCstr(fndMCC.Value) + "' as  [MCC Code],'" + clsCommon.myCstr(lblMCC.Text) + "' as  [MCC Name], TBL_Farmer_Invoice.Vendor_Group_Code as [Vendor Group Code],TBL_Farmer_Invoice.Vendor_Group_Code_Desc as [Vendor Group Code Desc],'" + clsCommon.myCDate(dtpFromDate.Value) + "' as FromDate ,'" + clsCommon.myCDate(dtpToDate.Value) + "' as ToDate , XXXX.CompName, XXXX.VLC_Code as [VLC Code],XXXX.VLC_Name as [VLC Name],XXXX.[Incharge Code],XXXX.[Incharge Name] ,TSPL_MCC_ROUTE_MASTER.Supervisor_Name as [Supervisor Code],tspl_employee_master.EMP_NAME as [Supervisor Name],isnull(TSPL_MCC_ROUTE_MASTER.Contact_No,'') as [Supervisor Contact No] ,XXXX.MP_CODE as [MP CODE],XXXX.MP_Name as [MP Name],XXXX.No_Of_Shift as [No Of Shift],XXXX.Qty_In_Ltr as Qty,convert ( decimal(18,2) ,XXXX.Amount /nullif ( XXXX.Qty_In_Ltr,0) ) as Rate  ,convert ( decimal(18,2), ((XXXX.Fat_KG * 100) / nullif (XXXX.Qty_In_KG,0) ))  as [FAT PER]  ,convert (decimal(18,2), ((XXXX.SNF_KG * 100 )/ nullif (XXXX.Qty_In_KG,0)))   as [SNF PER],XXXX.Amount,Incentive_Amount as [Incentive Amount], DedAddAmount-Deduction_Amount as [Total Deduction] ,TBL_Farmer_Invoice.MP_Adjust_Amount as [OutStanding Amount],XXXX.Amount +Incentive_Amount+DedAddAmount - Deduction_Amount + (isnull(TBL_Farmer_Invoice.MP_Adjust_Amount,0)) as [Net Amount] ,XXXX.Bank_Code,XXXX.Bank_Name,XXXX.AccountNo,XXXX.PayeeName ,XXXX.IFCICode,XXXX.BankBranch,XXXX.Education,XXXX.MP_VLC_Uploader_Code_Last3Degit, XXXX.Comp_Code  as Comp_Code,TSPL_COMPANY_MASTER.Logo_Img,XXXX.Fat_KG,XXXX.SNF_KG,XXXX.Qty_In_KG " &
                " from ( " &
                " Select max(Final.VLC_CODE_VLC_Uploader) as VLC_CODE_VLC_Uploader,max(Final.CompName) as CompName, Final.VLC_Code , max(Final .VLC_Name) as VLC_Name,max(Final.[Incharge Code]) as [Incharge Code],max(Final.[Incharge Name]) as [Incharge Name] ,max(Final.Route_No) as Route_No, Final.MP_CODE, max (Final.MP_Name) as MP_Name ,max(Final.Education) as Education, count (Final.shift) as No_Of_Shift, sum (qty) as Qty_In_Ltr, sum (fat_KG) as Fat_KG, Sum (snf_KG) as SNF_KG, Sum (Amount) as Amount , sum (Qty_In_KG) as Qty_In_KG , max (Incentive_Amount) as Incentive_Amount ,max(DedAddAmount) as DedAddAmount ,max (Deduction_Amount) as Deduction_Amount,max (Total_Advance_Amount_Recovery) as Total_Advance_Amount_Recovery ,max (Total_Loan_Payment_Recovery) as Total_Loan_Payment_Recovery,max (Payable_Amount) as Payable_Amount ,max(Final.Bank_Code) as Bank_Code,max(Final.Bank_Name) as Bank_Name,max(Final.AccountNo) as AccountNo ,max(Final.IFCICode) as IFCICode,max(Final.BankBranch) as BankBranch,max(Final.MP_VLC_Uploader_Code_Last3Degit) as MP_VLC_Uploader_Code_Last3Degit,max(Comp_Code) as Comp_Code ,max(PayeeName) as PayeeName from ( " &
                "  " &
                "  " &
                " Select SSSS.CompName, SSSS.MCC_CODE, SSSS.MCC_NAME,SSSS.SHIFT, SSSS.VLC_CODE_VLC_Uploader, SSSS.VSP_CODE as MP_CODE, SSSS.VSP_NAME as MP_NAME,SSSS.Education, SSSS.VLC_CODE ,SSSS.VLC_Name,SSSS.[Incharge Code],SSSS.[Incharge Name],SSSS.Route_No, SSSS.MP_VLC_Uploader_Code,SSSS.UOM_CODE,SSSS.Qty,SSSS.FAT_PER,SSSS.SNF_PER, " &
                "  " &
                " (SSSS.FAT_PER *  (zzz.CF*SSSS.Qty) ) /100  as FAT_KG,   (SSSS.SNF_PER * (zzz.CF*SSSS.Qty) ) /100  as  SNF_KG , zzz.CF*SSSS.Qty as Qty_In_KG  " &
                "  " &
                "  " &
                " ,Net_Amount as Amount,SSSS.DedAddAmount,TBL_IncentiveDeduction.Incentive_Amount,TBL_IncentiveDeduction.Total_Advance_Amount_Recovery,TBL_IncentiveDeduction.Total_Loan_Payment_Recovery,TBL_IncentiveDeduction.Payable_Amount , TBL_IncentiveDeduction.Deduction_Amount ,SSSS.Bank_Code,SSSS.Bank_Name,SSSS.AccountNo,SSSS.IFCICode,SSSS.BankBranch,SSSS.MP_VLC_Uploader_Code_Last3Degit,SSSS.Comp_Code, SSSS.PayeeName from ( select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name as CompName,'13/09/2019' as FromDate ,'19/09/2019' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER.Doc_Date,103) as Doc_Date ,TSPL_VLC_DATA_UPLOADER.Milk_Type, TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , TSPL_VLC_DATA_UPLOADER.shift , TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_Code_VLC_Uploader ,  TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name,TSPL_MP_MASTER.Education  , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name ,TSPL_VLC_MASTER_HEAD.VSP_Code as [Incharge Code],tspl_vendor_master.Vendor_Name as [Incharge Name] , TSPL_VLC_DATA_UPLOADER.Route_No , TSPL_VLC_DATA_UPLOADER.MP_CODE as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER.Uom_Code, TSPL_VLC_DATA_UPLOADER.qty , TSPL_VLC_DATA_UPLOADER.fat as FAT_PER, TSPL_VLC_DATA_UPLOADER.snf as SNF_PER,TSPL_VLC_DATA_UPLOADER.Rate , TSPL_VLC_DATA_UPLOADER.Amount as Net_Amount, TSPL_MP_MASTER.PayeeName  from TSPL_VLC_DATA_UPLOADER  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_DATA_UPLOADER.MCC_Code  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_UPLOADER = TSPL_VLC_DATA_UPLOADER.VLC_CODE left outer Join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code  left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER.Comp_Code  left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader = TSPL_VLC_DATA_UPLOADER.MP_CODE  where convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocument.Value + "')   and convert (date, TSPL_VLC_DATA_UPLOADER.Doc_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocument.Value + "')  and TSPL_VLC_DATA_UPLOADER.MP_CODE in (select distinct TSPL_MP_MASTER.MP_Code_VLC_Uploader from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocument.Value + "')  ) Final   left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from (  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocument.Value + "'  Union All  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocument.Value + "'  ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code  Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code  left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName    Union All select Final.*, isnull (TBL_FOR_DEDUCTION.Amount,0) as DedAddAmount,TSPL_MP_MASTER.BankName as Bank_Code, tspl_vendor_bank_master.Bank_Name,TSPL_MP_MASTER.AccountNo,TSPL_MP_MASTER.IFCICode,TSPL_MP_MASTER.BankBranch, RIGHT( MP_VLC_Uploader_Code,3) as MP_VLC_Uploader_Code_Last3Degit from ( select TSPL_COMPANY_MASTER.Comp_Code, TSPL_COMPANY_MASTER.Comp_Name as CompName,'19/09/2019' as FromDate ,'19/09/2019' as ToDate ,Convert (varchar,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Doc_Date ,'C' as Milk_Type,TSPL_VLC_MASTER_HEAD_ForMCC.MCC  as MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Add1 as MCC_Add1 , TSPL_MCC_MASTER.Add2 as MCC_Add2 , TSPL_MCC_MASTER.City_code as MCC_City_Code, TSPL_CITY_MASTER_MCC.City_Name as MCC_City_Name,TSPL_MCC_MASTER.State_Code as MCC_State_Code,TSPL_STATE_MASTER_MCC.STATE_NAME as MCC_STATE_NAME,TSPL_MCC_MASTER.Pin_code as MCC_Pin_Code , case when  TSPL_VLC_DATA_UPLOADER_MASTER.Shift = 'MORNING' then 'M' else 'E' end as Shift , TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader  as VLC_Code_VLC_Uploader ,  TSPL_MP_MASTER.MP_Code as VSP_Code, TSPL_MP_MASTER.MP_Name as VSP_Name,TSPL_MP_MASTER.Education  , TSPL_MP_MASTER.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name , TSPL_VLC_MASTER_HEAD.VSP_Code as [Incharge Code],tspl_vendor_master.Vendor_Name as [Incharge Name] , TSPL_VLC_DATA_UPLOADER_MASTER.Route_Code as Route_No , TSPL_MP_MASTER.MP_Code_VLC_Uploader as MP_VLC_Uploader_Code ,TSPL_VLC_DATA_UPLOADER_DETAIL.Unit_Code as Uom_Code, TSPL_VLC_DATA_UPLOADER_DETAIL.Qty , TSPL_VLC_DATA_UPLOADER_DETAIL.fatPer as FAT_PER, TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer as SNF_PER,TSPL_VLC_DATA_UPLOADER_DETAIL.Rate , TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as Net_Amount  ,TSPL_MP_MASTER.PayeeName from  TSPL_VLC_DATA_UPLOADER_DETAIL left outer join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code  Left Outer Join TSPL_VLC_MASTER_HEAD as TSPL_VLC_MASTER_HEAD_ForMCC on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD_ForMCC.VLC_Code  left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code  left outer Join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_Code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD_ForMCC.MCC  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_MCC on TSPL_CITY_MASTER_MCC.City_Code =  TSPL_MCC_MASTER.City_code  left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_MCC on TSPL_STATE_MASTER_MCC.State_Code = TSPL_MCC_MASTER.State_Code    left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = TSPL_VLC_DATA_UPLOADER_MASTER.Comp_Code  left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code  where convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) > = (select TSPL_PAYMENT_PROCESS_HEAD.From_Date  from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocument.Value + "')   and convert (date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) < = (select  TSPL_PAYMENT_PROCESS_HEAD.To_Date from TSPL_PAYMENT_PROCESS_HEAD where Doc_No = '" + fndDocument.Value + "')  and TSPL_VLC_DATA_UPLOADER_DETAIL.Farmer_Code in (select distinct TSPL_MP_MASTER.MP_Code from TSPL_MP_PAY_PROCESS_DETAIL inner join TSPL_MP_MASTER on TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code = TSPL_MP_MASTER.MP_Code  where Doc_No = '" + fndDocument.Value + "')  ) Final   left outer join  (select Final.VSP_Code  ,sum (Final.Item_Net_Amt) as Amount from (  select TSPL_MP_PAY_PROCESS_MCC_SALE.FARMER_CODE as VSP_Code,TSPL_MCC_SALE_FARMER_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MCC_SALE_FARMER_DETAIL.Item_Net_Amt * -1 as Item_Net_Amt from TSPL_MP_PAY_PROCESS_MCC_SALE inner join TSPL_MCC_SALE_FARMER_DETAIL on TSPL_MP_PAY_PROCESS_MCC_SALE.Shipment_Doc_No = TSPL_MCC_SALE_FARMER_DETAIL.DOCUMENT_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_FARMER_DETAIL.Item_Code where TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocument.Value + "'  Union All  select TSPL_MCC_SALE_RETURN_HEAD_FARMER.FARMER_CODE as  VSP_Code,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Amount  as Item_Net_Amt from TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN   inner join TSPL_MCC_SALE_RETURN_DETAIL_FARMER on TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Item_Issue_Return_No = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.DOCUMENT_CODE   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Item_Code   left outer join TSPL_MCC_SALE_RETURN_HEAD_FARMER on TSPL_MCC_SALE_RETURN_HEAD_FARMER.Document_Code = TSPL_MCC_SALE_RETURN_DETAIL_FARMER.Document_Code   where TSPL_PAYMENT_PROCESS_ITEM_ISSUE_RETURN.Doc_No = '" + fndDocument.Value + "'  ) Final group by Final.VSP_Code ) As TBL_FOR_DEDUCTION on TBL_FOR_DEDUCTION.VSP_Code = Final.VSP_Code  Left Outer Join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code = Final.VSP_Code  left Outer Join tspl_vendor_bank_master on tspl_vendor_bank_master.Bank_Code = TSPL_MP_MASTER.BankName  ) SSSS  left outer join  ( select VLC_CODE_Uploader,Farmer_Code, Sum ( isNull (Incentive_Amount,0)) as Incentive_Amount , Sum ( isnull(Deduction_Amount,0)) as Deduction_Amount,Sum( isnull(Total_Advance_Amount_Recovery,0)) as Total_Advance_Amount_Recovery, Sum ( isnull(Total_Loan_Payment_Recovery,0)) as Total_Loan_Payment_Recovery,Sum(isnull(Payable_Amount,0)) as Payable_Amount from TSPL_MP_PAY_PROCESS_DETAIL where Doc_No = '" + fndDocument.Value + "' group by VLC_CODE_Uploader,Farmer_Code ) TBL_IncentiveDeduction on TBL_IncentiveDeduction.VLC_CODE_Uploader = SSSS.VLC_Code_VLC_Uploader and TBL_IncentiveDeduction.Farmer_Code = SSSS.VSP_Code   " &
                "  " &
                " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/  nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =SSSS.UOM_CODE and lower(zzz.TOUOM)='KG' " &
                "  " &
                " ) Final Group by Final.MP_CODE , Final.VLC_Code  " &
                " ) XXXX left outer join TSPL_MCC_ROUTE_VLC_MAPPING on TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE = XXXX.VLC_Code left outer join TSPL_MCC_ROUTE_MASTER on  TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE " &
                " Left Outer Join tspl_employee_master on tspl_employee_master.EMP_CODE = TSPL_MCC_ROUTE_MASTER.Supervisor_Name left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = XXXX.Comp_Code  " &
                " left outer join (  select   TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code ,left ( TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No,(len(TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Invoice_No)- ( len(TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code)+1 ))) as Farmer_Invoice_No ,TSPL_MP_PAY_PROCESS_DETAIL.MP_Adjust_Amount,tspl_vendor_master.Vendor_Group_Code, tspl_vendor_master.Vendor_Group_Code_Desc  from TSPL_MP_PAY_PROCESS_DETAIL left outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_MP_PAY_PROCESS_DETAIL.VSP_CODE   where  TSPL_MP_PAY_PROCESS_DETAIL.Doc_No = '" + fndDocument.Value + "') as  TBL_Farmer_Invoice on XXXX.MP_CODE = TBL_Farmer_Invoice.Farmer_Code  " &
                " left outer join (select  x.Farmer_Code ,sum(x.Amount) as Amount from (  select   TSPL_MP_PAY_PROCESS_MCC_SALE.Farmer_Code ,TSPL_MP_PAY_PROCESS_MCC_SALE.Amount " &
                " from TSPL_MP_PAY_PROCESS_MCC_SALE left outer join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_MP_PAY_PROCESS_MCC_SALE.VSP_CODE   where  TSPL_MP_PAY_PROCESS_MCC_SALE.Doc_No = '" + fndDocument.Value + "')x Group by x.Farmer_Code) as  TBL_Farmer_Sale on XXXX.MP_CODE = TBL_Farmer_Sale.Farmer_Code   " &
                  " )LastOuter group by LastOuter.[VLC Code]  "

            qry += " order by LastOuter.[VLC Code] "
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).BestFit()
                Next
                Gv1.Columns("Qty_In_KG").IsVisible = False
                Gv1.Columns("Fat_KG").IsVisible = False
                Gv1.Columns("SNF_KG").IsVisible = False

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.AutoSizeRows = True
                Gv1.EnableFiltering = True
                EnableDisiablecontrol(False)
                btnGo.Enabled = False

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim Qty As New GridViewSummaryItem("Qty - Ltr", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Qty)
                Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Amount)
                Dim IncentiveAmount As New GridViewSummaryItem("Incentive Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(IncentiveAmount)
                Dim ArrearAmount As New GridViewSummaryItem("Arrear Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ArrearAmount)
                Dim TSDeduction As New GridViewSummaryItem("TS Deduction", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(TSDeduction)
                Dim AdjustmentAmount As New GridViewSummaryItem("Adjustment Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(AdjustmentAmount)
                Dim CattleFeed As New GridViewSummaryItem("Cattle Feed", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(CattleFeed)
                Dim NetAmount As New GridViewSummaryItem("Net Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(NetAmount)

                Dim summaryItem1 As New GridViewSummaryItem()
                summaryItem1.FormatString = "{0:F2}"
                summaryItem1.Name = "Fat %"
                summaryItem1.AggregateExpression = "sum(Fat_KG)*100/sum(Qty_In_KG)"
                summaryRowItem.Add(summaryItem1)

                Dim summaryItem2 As New GridViewSummaryItem()
                summaryItem2.FormatString = "{0:F2}"
                summaryItem2.Name = "Snf %"
                summaryItem2.AggregateExpression = "sum(SNF_KG)*100/sum(Qty_In_KG)"
                summaryRowItem.Add(summaryItem2)

                Dim summaryItem3 As New GridViewSummaryItem()
                summaryItem3.FormatString = "{0:F2}"
                summaryItem3.Name = "Rate/Ltr"
                summaryItem3.AggregateExpression = "(sum(Amount)+sum([Incentive Amount])+sum([Arrear Amount])-sum([TS Deduction])) / sum([Qty - Ltr])" '"sum(Amount)/sum(Qty)"
                summaryRowItem.Add(summaryItem3)

                Gv1.ShowGroupPanel = True
                Gv1.MasterTemplate.AutoExpandGroups = True
                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub



    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Farmer Payment Approval Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Farmer Payment Approval Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub

    Private Sub fndMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCC._MYValidating
        Dim whrCls As String = " 1=1 "
        If Not clsMccMaster.isCurrentUserHO() Then
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
            End If
        End If

        whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
        fndMCC.Value = clsLocation.getLocSegFinder(whrCls, fndMCC.Value, isButtonClicked)
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndMCC.Value & "' "))
    End Sub
    Private Sub fndDocument__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocument._MYValidating
        Dim whre As String = ""
        If clsCommon.myLen(fndMCC.Value) > 0 Then
            whre = " and Loc_Seg_Code = '" + fndMCC.Value + "'"
        End If
        fndDocument.Value = clsPaymentProcessFarmerHead.getFinder("FarmType='PPF' " + whre + "", fndDocument.Value, isButtonClicked)
        fndMCC.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Loc_Seg_Code  from TSPL_PAYMENT_PROCESS_HEAD WHERE  Doc_No='" & fndDocument.Value & "' "))
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndMCC.Value & "' "))
        dtpFromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select From_Date  from TSPL_PAYMENT_PROCESS_HEAD WHERE  Doc_No='" & fndDocument.Value & "' "))
        dtpToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue(" select To_Date  from TSPL_PAYMENT_PROCESS_HEAD WHERE  Doc_No='" & fndDocument.Value & "' "))
    End Sub
    Sub EnableDisiablecontrol(ByVal isEnable As Boolean)
        fndMCC.Enabled = isEnable
        fndDocument.Enabled = isEnable
        dtpFromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
    End Sub

End Class
