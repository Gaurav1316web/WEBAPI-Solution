Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'Created By Sanjay - Create New report 
Public Class rptMCCWiseAbstractReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim MultipleFinderFillAuto As Boolean = False
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter, Optional ByVal isDotMatrixPrint As Boolean = False)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                fndLoc.Focus()
                clsCommon.MyMessageBoxShow(Me, "Please select the Shed first", Me.Text)
                Exit Sub
            End If
            If dtpFromDate.Value > dtpToDate.Value Then
                dtpFromDate.Focus()
                clsCommon.MyMessageBoxShow(Me, " 'From Date' can't be larger than 'To Date'", Me.Text)
                Exit Sub
            End If

            If rdbYearlyConsolidatedReportofMilkProcurement.Checked = True OrElse rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                dtpFromDate.Value = clsDBFuncationality.getSingleValue("Select convert(date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "yyyy")) + "',103)")
                dtpToDate.Value = clsDBFuncationality.getSingleValue("select EOMONTH('" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "')")
            End If

            Dim StrDeductionHead As String = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' + QUOTENAME(" &
               " ISNULL(TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,'')) as Alies_Name" &
               " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no left join TSPL_DEDUCTION_MASTER on TSPL_VENDOR_INVOICE_DETAIL.deductioncode=TSPL_DEDUCTION_MASTER.code " &
               " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
               "  where 2=2 "
            If rdbDetails.Checked = True OrElse rdbYearlyConsolidatedofMilkPayment.Checked = True Then
            Else
                StrDeductionHead += " and (TSPL_DEDUCTION_MASTER.ho_type=1 or TSPL_DEDUCTION_MASTER.vlc_type=1) "
            End If
            StrDeductionHead += " and document_type='D' and isDeduction=1 and Posting_Date is not null " &
               " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & dtpFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & dtpToDate.Value & "',103)" &
               " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')"
            StrDeductionHead = clsDBFuncationality.getSingleValue(StrDeductionHead)

            Dim StrDeductionHeadSum As String = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' " &
             "  +'Sum(isnull(' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) +',0))' +' as ' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) as Alies_Name" &
             " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no left join TSPL_DEDUCTION_MASTER on TSPL_VENDOR_INVOICE_DETAIL.deductioncode=TSPL_DEDUCTION_MASTER.code " &
             " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
             "  where 2=2 "
            If rdbDetails.Checked = True OrElse rdbYearlyConsolidatedofMilkPayment.Checked = True Then
            Else
                StrDeductionHeadSum += " and (TSPL_DEDUCTION_MASTER.ho_type=1 or TSPL_DEDUCTION_MASTER.vlc_type=1) "
            End If
            StrDeductionHeadSum += " and document_type='D' and isDeduction=1 and Posting_Date is not null " &
             " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & dtpFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & dtpToDate.Value & "',103)" &
             " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')"
            StrDeductionHeadSum = clsDBFuncationality.getSingleValue(StrDeductionHeadSum)

            Dim StrDeductionHeadSumTotal As String = "DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT " &
             "  isnull(STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) +',0))' as Alies_Name" &
             " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no left join TSPL_DEDUCTION_MASTER on TSPL_VENDOR_INVOICE_DETAIL.deductioncode=TSPL_DEDUCTION_MASTER.code " &
             " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
             "  where 2=2 "
            If rdbDetails.Checked = True OrElse rdbYearlyConsolidatedofMilkPayment.Checked = True Then
            Else
                StrDeductionHeadSumTotal += " and (TSPL_DEDUCTION_MASTER.ho_type=1 or TSPL_DEDUCTION_MASTER.vlc_type=1) "
            End If
            StrDeductionHeadSumTotal += " and document_type='D' and isDeduction=1 and Posting_Date is not null " &
             " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & dtpFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & dtpToDate.Value & "',103)" &
             " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')"
            StrDeductionHeadSumTotal = clsDBFuncationality.getSingleValue(StrDeductionHeadSumTotal)

            Dim qry As String = ""
            If clsCommon.myLen(StrDeductionHead) > 0 Then
                If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                    qry += "select [MONTH-YEAR] "
                Else
                    qry += "select [MCC Code],	max([MCC Name]) as [MCC Name] "
                End If
                qry += ",sum([BM Amount]) as [BM Amount], sum([BM Qty]) as [BM Qty],sum([CM Amount]) as [CM Amount],sum ([CM Qty])  as [CM Qty] " &
                        ",sum([COMSN Amount]) as [COMSN Amount],sum([OP-COST Amount]) as [OP-COST Amount],sum([CART Amount]) as [CART Amount],sum([INC Amount]) as [INC Amount] " &
                        ",sum([ADDN Amount]) as [ADDN Amount],sum([Gross Amount]) as [Gross Amount] " &
                        " ," & StrDeductionHeadSum & ",(" & StrDeductionHeadSumTotal & ") as [Total Deduction Amount],(sum([Gross Amount])-(" & StrDeductionHeadSumTotal & ")) as [Net Amount]" &
                        " ,sum([T.I.P Amount]) as [T.I.P Amount],sum([KG Fat(RS.30)]) as [KG Fat(RS.30)] from ( " &
                        "select s.[MCC Code], s.[MCC Name]  "
                If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                    qry += ",s.[MONTH-YEAR],s.yy,s.mm "
                End If

                qry += ",Deduction_Desc,isnull(deduction.DeductionAmt,0) as DeductionAmt,[BM Amount],[CM Amount],[BM Qty],[CM Qty] " &
                        ",[COMSN Amount],[OP-COST Amount],[CART Amount],[INC Amount] " &
                        ",[ADDN Amount],[Gross Amount] " &
                        " ,[T.I.P Amount],[KG Fat(RS.30)]" &
                        " from ( "
            End If

            qry += " select pp.MCC as [MCC Code],pp.[MCC Name] "
            If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                qry += " ,datename(MONTH,pp.date) +'-'+ datename(YEAR,pp.date) as [MONTH-YEAR],YEAR(pp.date) as yy,MONTH(pp.date) as mm "
            End If
            qry += ",sum(pp.[BM Amount]) as [BM Amount],sum (pp.[BM Qty]) as [BM Qty],sum(pp.[CM Amount]) as [CM Amount] , sum (pp.[CM Qty]) as [CM Qty]  " &
                       ",0 as [COMSN Amount],0 as [OP-COST Amount],0 as [CART Amount],0 as [INC Amount] " &
                       ",isnull (sum(Incentive_Amount),0) as [ADDN Amount] " &
                       ",(isnull(sum(pp.[BM Amount]),0)+isnull(sum(pp.[CM Amount]),0) + isnull(sum(Incentive_Amount),0) "

            If rdbDetails.Checked = True OrElse rdbYearlyConsolidatedofMilkPayment.Checked = True Then
            Else
                qry += " +isnull(sum(TIP_Amount),0) "
            End If
            qry += " ) as [Gross Amount],sum(TIP_Amount) as [T.I.P Amount], 0 as [KG Fat(RS.30)]" &
                       " from ( " &
            " Select  TSPL_MILK_SRN_HEAD.DOC_DATE as date,(case when TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type='B' then  Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) else 0 end) as [BM Amount], (case when TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type='B' then TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR   else 0 end) as [BM Qty] ," &  'convert (decimal(18,2) , (TSPL_MILK_SRN_DETAIL.qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )
            "(case when TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type='C' then Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) else 0 end) as [CM Amount],(case when TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type='C' then TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR  else 0 end) as [CM Qty],TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type  As [Milk Type] " &
            ", TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ) as Incentive_Amount " &  ' TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Amount
            " ,isnull(TSPL_MILK_SRN_DETAIL.TIP_Amount,0) as TIP_Amount From TSPL_MILK_SRN_DETAIL  " &
            " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_DOC_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  " &
            " and   TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE  " &
            " and TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_Item_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code  " &
            " Left Outer Join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Code " &
            " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  " &
            " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code   " &
            " left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_MILK_SRN_DETAIL.Item_Code
              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_MILK_SRN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_MILK_SRN_DETAIL.UOM_Code = Stocking_Conversion_Factor.UOM_Code " &
            " where 2=2 " &
             " and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) >=  convert(date,'" + dtpFromDate.Value + "',103)  and  convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= convert(date,'" + dtpToDate.Value + "',103) " &
             " and tspl_location_master.location_Code ='" + fndLoc.Value + "'" &
            " )pp group by pp.MCC,pp.[MCC Name]  "

            If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                qry += " ,datename(MONTH,pp.date) +'-'+ datename(YEAR,pp.date),YEAR(pp.date) ,MONTH(pp.date)"
            End If
            If clsCommon.myLen(StrDeductionHead) > 0 Then
                qry += " ) as s  " &
            " left join    " &
            "(select TSPL_VENDOR_INVOICE_HEAD.MCC_Code"
                If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                    qry += " ,datename(MONTH,convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)) +'-'+ datename(YEAR,convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)) as [MONTH-YEAR] "
                End If
                qry += ",TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as DeductionAmt " &
            " from TSPL_VENDOR_INVOICE_DETAIL  " &
            " left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
            " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
            " where document_type='D' and isDeduction=1 and Posting_Date is not null " &
            " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & dtpFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & dtpToDate.Value & "',103)" &
            " group by TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc "
                If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                    qry += " ,datename(MONTH,convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103)) +'-'+ datename(YEAR,convert(date,convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103),103))  "
                End If
                qry += ") deduction on deduction.MCC_Code=s.[MCC Code] "
                If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                    qry += " and deduction.[MONTH-YEAR]=s.[MONTH-YEAR] "
                End If
                qry += ")tt " &
            " pivot (  sum(DeductionAmt) for Deduction_Desc  in (" & StrDeductionHead &
            ") ) as zpivot group by   "
                If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                    qry += " zpivot.[MONTH-YEAR],zpivot.yy,zpivot.mm  "
                Else
                    qry += " zpivot.[MCC Code]  "
                End If
            End If

            If rdbSummary.Checked = True Then
                Dim strDeductionColumn As String = ""
                Dim strMinusDedAmtInNetAmt As String = ""
                If clsCommon.myLen(StrDeductionHead) > 0 Then
                    strDeductionColumn = " ," & StrDeductionHeadSum & ",(" & StrDeductionHeadSumTotal & ") as [Total Deduction Amount] "
                    strMinusDedAmtInNetAmt = " -(" & StrDeductionHeadSumTotal & ") "
                End If
                qry = " select TSPL_MCC_MASTER.Plant_Code as PlantCode, max(TSPL_LOCATION_MASTER.Location_Desc) as PlantName,sum (XXXFinal.[BM Amount]) as [BM Amount], cast (  sum ([BM Qty]) as Decimal(18,2)) as [BM Qty(Ltr)] , cast (  ( sum ([BM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + dtpFromDate.Value + "',103), convert (date, '" + dtpToDate.Value + "',103))+1 ,0) ) as Decimal(18,2)) as [BM Avg(Ltr)], cast( (sum (XXXFinal.[BM Amount]) /nullif (sum ([BM Qty]),0)) as decimal(18,2)) as  [BM Avg Rate], sum ([CM Amount]) as [CM Amount] ,cast ( sum ([CM Qty]) as  Decimal(18,2)) as [CM Qty(Ltr)], cast (  (sum ([CM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + dtpFromDate.Value + "',103), convert (date, '" + dtpToDate.Value + "',103))+1 ,0)) as decimal(18,2)) [CM Avg(Ltr)] ,cast( (sum (XXXFinal.[CM Amount]) /sum ([CM Qty])) as decimal(18,2)) as  [CM Avg Rate], cast (  (sum ([BM Qty])  +  sum ([CM Qty])) as decimal(18,2)) as [MM Qty(Ltr)], cast (  ( sum ([BM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + dtpFromDate.Value + "',103), convert (date, '" + dtpToDate.Value + "',103))+1 ,0) ) as Decimal(18,2)) + cast (  (sum ([CM Qty])/  nullif(DATEDIFF (DAY, convert (date,'" + dtpFromDate.Value + "',103), convert (date, '" + dtpToDate.Value + "',103))+1 ,0)) as decimal(18,2))  as [MM Avg(Ltr)], cast (( (sum (XXXFinal.[BM Amount]) + sum (XXXFinal.[CM Amount])) / nullif ((sum ([BM Qty]) + sum ([CM Qty]) ),0)  ) as decimal(18,2)) as [MM Avg Rate] ,  max(TBL_Buffalo.TSDDCS_Rate) as [BM TSDDCS Rate] , max(TBL_COW.TSDDCS_Rate) as [CM TSDDCS Rate] , sum ([COMSN Amount]) as [COMSN Amount],sum ([OP-COST Amount]) as [OP-COST Amount], sum ([CART Amount]) as [CART Amount], sum ([INC Amount]) as [INC Amount], sum ([ADDN Amount]) as [ADDN Amount], sum ([Gross Amount]) as [Gross Amount]  " + strDeductionColumn + " ,(sum([Gross Amount])  " + strMinusDedAmtInNetAmt + "  ) as [Net Amount] ,sum([T.I.P Amount]) as [T.I.P Amount],sum([KG Fat(RS.30)]) as [KG Fat(RS.30)]  from ( " + qry + " ) as XXXFinal 
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = XXXFinal.[MCC Code]
                        left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code
                        left outer join (select top 1 TSPL_MCC_MASTER.Plant_Code,  TSPL_PRICE_CHART_PLANNING.TSDDCS_Rate  from TSPL_PRICE_CHART_PLANNING_MCC  left outer join TSPL_PRICE_CHART_PLANNING on TSPL_PRICE_CHART_PLANNING.Planning_Code = TSPL_PRICE_CHART_PLANNING_MCC.Planning_Code
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PRICE_CHART_PLANNING_MCC.MCC_Code
                        where Dock_Collection_Milk_Type = 'B' and Status =1 and TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' 
                        order by  Planning_Date desc) as TBL_Buffalo on TBL_Buffalo.Plant_Code = TSPL_MCC_MASTER.Plant_Code
                        left outer join (select top 1 TSPL_MCC_MASTER.Plant_Code,  TSPL_PRICE_CHART_PLANNING.TSDDCS_Rate  from TSPL_PRICE_CHART_PLANNING_MCC  left outer join TSPL_PRICE_CHART_PLANNING on TSPL_PRICE_CHART_PLANNING.Planning_Code = TSPL_PRICE_CHART_PLANNING_MCC.Planning_Code
                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PRICE_CHART_PLANNING_MCC.MCC_Code
                        where Dock_Collection_Milk_Type = 'C' and Status =1 and TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' 
                        order by  Planning_Date desc) as TBL_COW on TBL_COW.Plant_Code = TSPL_MCC_MASTER.Plant_Code
                        group by TSPL_MCC_MASTER.Plant_Code
                        order by TSPL_MCC_MASTER.Plant_Code
                       "
            ElseIf rdbDetails.Checked = True Then
                qry += " order by [MCC Name]"
            ElseIf rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                qry += " order by yy,mm"
            ElseIf rdbProcurementAbstract.Checked = True AndAlso chkRejection.Checked = False Then
                Dim arrMCCList As ArrayList = New ArrayList()
                Dim strMCC As String = "select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "'"
                Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(strMCC)
                If (dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0) Then
                    For Each dr As DataRow In dtMCC.Rows
                        arrMCCList.Add(clsCommon.myCstr(dr("MCC_Code")))
                    Next
                End If
                qry = clsMilkRejectHead.GetMCCRegisterQuery(dtpFromDate.Value, dtpToDate.Value, "M", "E", "ZeroAndNonZero", Nothing, arrMCCList, Nothing, Nothing, "")
                ' aa.[Plant Code],aa.[Plant Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)],aa.CLR ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Total Solid] ,aa.[Cow FAT(%)],aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Buffalo FAT(%)],aa.[Buffalo CLR] ,aa.[Buffalo SNF(%)] ,,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle, aa.[FAT(%)],aa.CLR ,aa.[SNF(%)], aa.[MIXED Milk Weight]
                Dim FinalQuery As String = "select aa.[MCC Code],aa.[MCC Name] , aa.[Cow Milk Qty (Ltr)] ,aa.[Cow Milk Qty (KG)],aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)],aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)] as [Cow Total Solid], cast ( round (( aa.[Cow FAT (KG)] * 100 / nullif ( aa.[Cow Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Cow Avg FAT], cast ( round (( aa.[Cow SNF (KG)] * 100 / nullif(aa.[Cow Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Cow Avg SNF] ,aa.[Buffalo Milk Qty (Ltr)] ,aa.[Buffalo Milk Qty (KG)],aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)],aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)] as [Buffalo Total Solid],cast ( round (( aa.[Buffalo FAT (KG)] * 100 / nullif( aa.[Buffalo Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Buffalo Avg FAT], cast ( round (( aa.[Buffalo SNF (KG)] * 100 / nullif (aa.[Buffalo Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Buffalo Avg SNF] ,aa.[Milk Weight(LTR)] as [Mixed Milk Weight(LTR)]  ,aa.[Milk Weight(KG)] as [Mixed Milk Weight(KG)]	,aa.[FAT(KG)] as [Mixed FAT(KG)] ,aa.[SNF(KG)] as [Mixed SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Mixed Total Solid] ,cast ( round (( aa.[FAT(KG)] * 100 / nullif (aa.[Milk Weight(KG)],0)) ,1,0) as decimal(18,2)) as [Mixed Avg FAT], cast ( round (( aa.[SNF(KG)] * 100 / nullif (aa.[Milk Weight(KG)],0)) ,1,0) as decimal(18,2)) as [Mixed Avg SNF], isnull (TBL_Reject_Data.[PROD.LTS],0) as [PROD.LTS] , isnull (TBL_Reject_Data.[P.T.C LTS],0) as [P.T.C LTS]  from ( "
                FinalQuery += " select xxx.* ,"
                FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                FinalQuery += " from ("
                FinalQuery += " select xx.*"
                FinalQuery += " from ( "
                FinalQuery += "select pp.[Plant Code]  as [Plant Code],max(pp.[Plant Name]) as [Plant Name],pp.[MCC Code] as [MCC Code] ,max(pp.[MCC Name]) as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], sum(pp.[Cow Milk Qty (Ltr)]) as [Cow Milk Qty (Ltr)] ,"
                FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)], sum([Buffalo Milk Qty (Ltr)]) as [Buffalo Milk Qty (Ltr)], "
                FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount)as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from ("
                FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                FinalQuery += " ) as  pp group by pp.[Plant Code],pp.[MCC Code] "
                FinalQuery += " )as xx"
                FinalQuery += " ) as xxx ) as aa "
                FinalQuery += "   left outer join (select MCC_Code, sum ([PROD.LTS]) as [PROD.LTS] ,sum ([P.T.C LTS]) as [P.T.C LTS]  from (
                                  select TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code, case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [PROD.LTS], case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [P.T.C LTS]   from TSPL_MILK_SHIFT_UPLOADER_DETAIL left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No = TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                                  where 2 = 2  and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status =1 and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy") + "'  and TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "') and  case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end + case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end > 0 )  as TBL_Reject group by MCC_Code) as TBL_Reject_Data on  TBL_Reject_Data.MCC_Code = aa.[MCC Code] "

                FinalQuery += "   " ' order by [Plant Code],[MCC Code]
                FinalQuery += " Union All "
                FinalQuery += " select 'Total :' as [MCC Code],'' as [MCC Name]  , sum (aa.[Cow Milk Qty (Ltr)]) as [Cow Milk Qty (Ltr)] , sum (aa.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], sum (aa.[Cow FAT (KG)]) as [Cow FAT (KG)] , sum (aa.[Cow SNF (KG)]) as [Cow SNF (KG)], sum (aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)]) as [Cow Total Solid], cast ( round ((sum ( aa.[Cow FAT (KG)]) * 100 / nullif ( sum(aa.[Cow Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Cow Avg FAT], cast ( round (( sum (aa.[Cow SNF (KG)]) * 100 / nullif( sum (aa.[Cow Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Cow Avg SNF] , sum (aa.[Buffalo Milk Qty (Ltr)]) as [Buffalo Milk Qty (Ltr)] , sum (aa.[Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)] , sum (aa.[Buffalo FAT (KG)]) as [Buffalo FAT (KG)] , sum (aa.[Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum ( aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)]) as [Buffalo Total Solid],cast ( round (( sum (aa.[Buffalo FAT (KG)]) * 100 / nullif( sum (aa.[Buffalo Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Buffalo Avg FAT], cast ( round (( sum (aa.[Buffalo SNF (KG)]) * 100 / nullif ( sum (aa.[Buffalo Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Buffalo Avg SNF] , sum (aa.[Milk Weight(LTR)]) as [Mixed Milk Weight(LTR)]  , sum (aa.[Milk Weight(KG)]) as [Mixed Milk Weight(KG)]	, sum (aa.[FAT(KG)]) as [Mixed FAT(KG)] , sum (aa.[SNF(KG)]) as [Mixed SNF(KG)], sum (aa.[FAT(KG)]+aa.[SNF(KG)]) as [Mixed Total Solid] ,cast ( round (( sum (aa.[FAT(KG)]) * 100 / nullif ( sum (aa.[Milk Weight(KG)]),0)) ,1,0) as decimal(18,2)) as [Mixed Avg FAT], cast ( round (( sum (aa.[SNF(KG)]) * 100 / nullif ( sum (aa.[Milk Weight(KG)]),0)) ,1,0) as decimal(18,2)) as [Mixed Avg SNF],sum (isnull (TBL_Reject_Data.[PROD.LTS],0)) as [PROD.LTS] , sum (isnull (TBL_Reject_Data.[P.T.C LTS],0)) as [P.T.C LTS]  from (  "
                FinalQuery += " select xxx.* ,"
                FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                FinalQuery += " from ("
                FinalQuery += " select xx.*"
                FinalQuery += " from ( "
                FinalQuery += "select pp.[Plant Code]  as [Plant Code],max(pp.[Plant Name]) as [Plant Name],pp.[MCC Code] as [MCC Code] ,max(pp.[MCC Name]) as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], sum(pp.[Cow Milk Qty (Ltr)]) as [Cow Milk Qty (Ltr)] ,"
                FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)], sum([Buffalo Milk Qty (Ltr)]) as [Buffalo Milk Qty (Ltr)], "
                FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount)as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from ("
                FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                FinalQuery += " ) as  pp group by pp.[Plant Code],pp.[MCC Code] "
                FinalQuery += " )as xx"
                FinalQuery += " ) as xxx"

                FinalQuery += " ) as aa   " ' order by [Plant Code],[MCC Code]
                FinalQuery += "   left outer join (select MCC_Code, sum ([PROD.LTS]) as [PROD.LTS] ,sum ([P.T.C LTS]) as [P.T.C LTS]  from (
                                  select TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code, case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [PROD.LTS], case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [P.T.C LTS]   from TSPL_MILK_SHIFT_UPLOADER_DETAIL left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No = TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                                  where 2 = 2  and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status =1 and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy") + "'  and TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "') and  case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end + case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end > 0 )  as TBL_Reject group by MCC_Code) as TBL_Reject_Data on  TBL_Reject_Data.MCC_Code = aa.[MCC Code] "

                FinalQuery += " group by [Plant Code] "
                qry = FinalQuery
                'End If
            ElseIf rdbProcurementAbstract.Checked = True AndAlso chkRejection.Checked = True Then
                Dim strRejection As String = Nothing
                Dim strSRNQuery As String = Nothing
                Dim strRejectionQuery As String = Nothing
                If chkRejection.Checked = True Then
                    strRejection = ",'' as RejectType,'' as RejectReason,'' as Defaulter"
                Else
                    strRejection = ""
                End If

                strSRNQuery = "Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)]," &
                " Case When TSPL_MILK_SRN_DETAIL.FAT_Per > 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], 
                Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)],
                Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)]," &
                " Case When TSPL_MILK_SRN_DETAIL.FAT_Per > 5 Then TSPL_MILK_SRN_DETAIL.ACC_QTY Else 0 End [Buffalo Milk Qty (KG)]" + Environment.NewLine
                If objCommonVar.DisplayTypeInMilkReceipt Then
                    strSRNQuery += ",TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type  As [Milk Type] "
                Else
                    strSRNQuery += ", Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_Per, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_per, 0) <= 5 Then 'C' Else 'B' End As [Milk Type]"
                End If
                strSRNQuery += ", TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code]," &
                " TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date, " &
                " Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift, " &
                " TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code]," &
                " TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code]," &
                " TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No], " &
                " TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.QTY As [Milk Weight],TSPL_MILK_SRN_DETAIL.UOM_Code, TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)]," &
                " TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,  " &
                " TSPL_MILK_SRN_DETAIL.FAT_KG As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], 
                Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = '' Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status]," &
                " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no," &
                " convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Weight  AS Is_Manual, '' as MACHINE_NO,(CASE WHEN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount " & strRejection & "  " &
                " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                " ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " &
                "  ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle " + Environment.NewLine +
                " From TSPL_MILK_SRN_DETAIL " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                " left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.DOC_CODE" + Environment.NewLine +
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
                " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE" + Environment.NewLine +
                " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE" + Environment.NewLine +
                " left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE" + Environment.NewLine +
                " left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code" + Environment.NewLine +
                " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per" + Environment.NewLine +
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                " where 2 = 2 "
                strSRNQuery += " and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "'"
                'If clsCommon.CompairString("M", "E") = CompairStringResult.Equal Then
                '    strSRNQuery += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
                'End If
                'If clsCommon.CompairString("E", "M") = CompairStringResult.Equal Then
                '    strSRNQuery += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
                'End If
                strSRNQuery += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "' ) "
                strRejectionQuery = "  Select TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Cow FAT(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Cow SNF(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Buffalo FAT(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Buffalo SNF(%)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT <= 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG Else 0 End [Cow Milk Qty (KG)], " &
                " Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (KG)], "
                'strRejectionQuery += " Case When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], "
                strRejectionQuery += " case when TSPL_MILK_REJECT_TYPE.Type is not null  then TSPL_MILK_REJECT_TYPE.Type When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], "
                strRejectionQuery += " TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_REJECT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], " &
                " Convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_REJECT_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_REJECT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_REJECT_DETAIL.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_REJECT_DETAIL.UOM_Code, TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)],0 as CLR, Convert(decimal(18,3), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)], " &
                " Convert(decimal(18,3),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status], " &
                " TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], " &
                " TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status], " &
                " TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , " &
                " 	TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Weight as IS_MANUAL , '' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_REJECT_TYPE.description as RejectType, " &
                " case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' when TSPL_MILK_REJECT_DETAIL.Is_Return=3 then 'COB'  end as ReajectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  " + Environment.NewLine +
                " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount " + Environment.NewLine +
                " ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " + Environment.NewLine +
                " ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,t1.Vehicle " + Environment.NewLine +
                " From   TSPL_MILK_REJECT_DETAIL " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE " + Environment.NewLine +
                " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " + Environment.NewLine +
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code" + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE " + Environment.NewLine +
                " Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE " + Environment.NewLine +
                "   " + Environment.NewLine +
                " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE " + Environment.NewLine +
                "  left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code " + Environment.NewLine +
                " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE " + Environment.NewLine +
                " Left Outer Join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code,TSPL_Primary_Vehicle_Master.Vehicle from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code " + Environment.NewLine +
                " And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code " &
                " left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code " + Environment.NewLine +
                " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per " + Environment.NewLine +
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code " &
                " left join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.code=TSPL_MILK_REJECT_DETAIL.Reject_Type " &
                " where 2=2  "

                strRejectionQuery += " and TSPL_MILK_REJECT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                'If clsCommon.CompairString("M", " E") = CompairStringResult.Equal Then
                '    strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='M' then 3 else 2 end  )"
                'End If
                'If clsCommon.CompairString("E", "M") = CompairStringResult.Equal Then
                '    strRejectionQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='E' then 3 else 2 end  )"
                'End If
                strRejectionQuery += " and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "') "
                qry = "Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift ," &
               " final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ," &
               " final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)]," &
               " final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]," &
               " final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)]," &
               " final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount]," &
               " final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,'' as RejectType,'' as RejectReason,'' as Defaulter, " &
               " final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( " & strSRNQuery & " Union All " & strRejectionQuery & ") As final where 2=2 "

                Dim FinalQuery As String = "select aa.[Plant Code],aa.[Plant Name],aa.[MCC Code],aa.[MCC Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.CLR,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Total Solid],aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR] ,aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)],aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)] as [Cow Total Solid] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)]  ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)],aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)] as [Buffalo Total Solid] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_AMOUNT,aa.TIP_Amount,aa.Head_Load_Amount , aa.SNF_Ded_Amount,aa.price_code,aa.[Transporter Code],aa.[Transporter Name],aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle from ( " & Environment.NewLine &
                   " select xxx.* ," & Environment.NewLine &
                   "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)]," & Environment.NewLine &
                   " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)]," & Environment.NewLine &
                   "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)]," & Environment.NewLine &
                   " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]" & Environment.NewLine &
                   " from (" & Environment.NewLine &
                   " select xx.*" & Environment.NewLine &
                   " from ( " & Environment.NewLine &
                   "select pp.[Plant Code]  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[MCC Code] as [MCC Code] ,max(pp.[MCC Name]) as [MCC Name] ,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)]," & Environment.NewLine &
                   " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)]," & Environment.NewLine &
                   " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]" & Environment.NewLine &
                   " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)]," & Environment.NewLine &
                   " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)]," & Environment.NewLine &
                   " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)]," & Environment.NewLine &
                   " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_AMOUNT) as EMP_AMOUNT,sum(TIP_Amount) as TIP_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount, max(price_code) as price_code,max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from (" & Environment.NewLine &
                   "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + "" & Environment.NewLine &
                   " ) as  pp group by pp.[Plant Code],pp.[MCC Code] " & Environment.NewLine &
                   " )as xx" & Environment.NewLine &
                   " ) as xxx" & Environment.NewLine &
                   " ) as aa" & Environment.NewLine

                FinalQuery += " order by [Plant Code],[MCC Code] "
                qry = FinalQuery

            ElseIf rdbYearlyConsolidatedReportofMilkProcurement.Checked = True Then
                Dim arrMCCList As ArrayList = New ArrayList()
                Dim strMCC As String = "select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "'"
                Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(strMCC)
                If (dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0) Then
                    For Each dr As DataRow In dtMCC.Rows
                        arrMCCList.Add(clsCommon.myCstr(dr("MCC_Code")))
                    Next
                End If
                qry = clsMilkRejectHead.GetMCCRegisterQuery(dtpFromDate.Value, dtpToDate.Value, "M", "E", "ZeroAndNonZero", Nothing, arrMCCList, Nothing, Nothing, "")

                Dim FinalQuery As String = "select aa.[MONTH-YEAR], aa.[Cow Milk Qty (Ltr)] ,aa.[Cow Milk Qty (KG)],aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)],aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)] as [Cow Total Solid], cast ( round (( aa.[Cow FAT (KG)] * 100 / nullif ( aa.[Cow Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Cow Avg FAT], cast ( round (( aa.[Cow SNF (KG)] * 100 / nullif(aa.[Cow Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Cow Avg SNF] ,aa.[Buffalo Milk Qty (Ltr)] ,aa.[Buffalo Milk Qty (KG)],aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)],aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)] as [Buffalo Total Solid],cast ( round (( aa.[Buffalo FAT (KG)] * 100 / nullif( aa.[Buffalo Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Buffalo Avg FAT], cast ( round (( aa.[Buffalo SNF (KG)] * 100 / nullif (aa.[Buffalo Milk Qty (KG)],0)) ,1,0) as decimal(18,2)) as [Buffalo Avg SNF] ,aa.[Milk Weight(LTR)] as [Mixed Milk Weight(LTR)]  ,aa.[Milk Weight(KG)] as [Mixed Milk Weight(KG)]	,aa.[FAT(KG)] as [Mixed FAT(KG)] ,aa.[SNF(KG)] as [Mixed SNF(KG)],aa.[FAT(KG)]+aa.[SNF(KG)] as [Mixed Total Solid] ,cast ( round (( aa.[FAT(KG)] * 100 / nullif (aa.[Milk Weight(KG)],0)) ,1,0) as decimal(18,2)) as [Mixed Avg FAT], cast ( round (( aa.[SNF(KG)] * 100 / nullif (aa.[Milk Weight(KG)],0)) ,1,0) as decimal(18,2)) as [Mixed Avg SNF], isnull (TBL_Reject_Data.[PROD.LTS],0) as [PROD.LTS] , isnull (TBL_Reject_Data.[P.T.C LTS],0) as [P.T.C LTS],yy,mm  from ( "
                FinalQuery += " select xxx.* ,"
                FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                FinalQuery += " from ("
                FinalQuery += " select xx.*"
                FinalQuery += " from ( "
                FinalQuery += "select datename(MONTH,pp.date) +'-'+ datename(YEAR,pp.date) as [MONTH-YEAR],YEAR(pp.date) as yy,MONTH(pp.date) as mm,sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], sum(pp.[Cow Milk Qty (Ltr)]) as [Cow Milk Qty (Ltr)] ,"
                FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)], sum([Buffalo Milk Qty (Ltr)]) as [Buffalo Milk Qty (Ltr)], "
                FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount)as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from ("
                FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                FinalQuery += " ) as  pp group by datename(MONTH,pp.date) +'-'+ datename(YEAR,pp.date),YEAR(pp.date) ,MONTH(pp.date) "
                FinalQuery += " )as xx"
                FinalQuery += " ) as xxx ) as aa "
                FinalQuery += "   left outer join (select [MONTH-YEAR], sum ([PROD.LTS]) as [PROD.LTS] ,sum ([P.T.C LTS]) as [P.T.C LTS]  from (
                                  select datename(MONTH,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) +'-'+ datename(YEAR,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as [MONTH-YEAR], case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [PROD.LTS], case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [P.T.C LTS]   from TSPL_MILK_SHIFT_UPLOADER_DETAIL left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No = TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                                  where 2 = 2  and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status =1 and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy") + "'  and TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "') and  case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end + case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end > 0 )  as TBL_Reject group by [MONTH-YEAR]) as TBL_Reject_Data on  TBL_Reject_Data.[MONTH-YEAR] = aa.[MONTH-YEAR] "

                FinalQuery += "   "
                FinalQuery += " Union All "
                FinalQuery += " select 'Total :' as [MONTH-YEAR]  , sum (aa.[Cow Milk Qty (Ltr)]) as [Cow Milk Qty (Ltr)] , sum (aa.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], sum (aa.[Cow FAT (KG)]) as [Cow FAT (KG)] , sum (aa.[Cow SNF (KG)]) as [Cow SNF (KG)], sum (aa.[Cow FAT (KG)]+aa.[Cow SNF (KG)]) as [Cow Total Solid], cast ( round ((sum ( aa.[Cow FAT (KG)]) * 100 / nullif ( sum(aa.[Cow Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Cow Avg FAT], cast ( round (( sum (aa.[Cow SNF (KG)]) * 100 / nullif( sum (aa.[Cow Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Cow Avg SNF] , sum (aa.[Buffalo Milk Qty (Ltr)]) as [Buffalo Milk Qty (Ltr)] , sum (aa.[Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)] , sum (aa.[Buffalo FAT (KG)]) as [Buffalo FAT (KG)] , sum (aa.[Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum ( aa.[Buffalo FAT (KG)]+aa.[Buffalo SNF (KG)]) as [Buffalo Total Solid],cast ( round (( sum (aa.[Buffalo FAT (KG)]) * 100 / nullif( sum (aa.[Buffalo Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Buffalo Avg FAT], cast ( round (( sum (aa.[Buffalo SNF (KG)]) * 100 / nullif ( sum (aa.[Buffalo Milk Qty (KG)]),0)) ,1,0) as decimal(18,2)) as [Buffalo Avg SNF] , sum (aa.[Milk Weight(LTR)]) as [Mixed Milk Weight(LTR)]  , sum (aa.[Milk Weight(KG)]) as [Mixed Milk Weight(KG)]	, sum (aa.[FAT(KG)]) as [Mixed FAT(KG)] , sum (aa.[SNF(KG)]) as [Mixed SNF(KG)], sum (aa.[FAT(KG)]+aa.[SNF(KG)]) as [Mixed Total Solid] ,cast ( round (( sum (aa.[FAT(KG)]) * 100 / nullif ( sum (aa.[Milk Weight(KG)]),0)) ,1,0) as decimal(18,2)) as [Mixed Avg FAT], cast ( round (( sum (aa.[SNF(KG)]) * 100 / nullif ( sum (aa.[Milk Weight(KG)]),0)) ,1,0) as decimal(18,2)) as [Mixed Avg SNF],sum (isnull (TBL_Reject_Data.[PROD.LTS],0)) as [PROD.LTS] , sum (isnull (TBL_Reject_Data.[P.T.C LTS],0)) as [P.T.C LTS],'2100' as yy,'13' as mm  from (  "
                FinalQuery += " select xxx.* ,"
                FinalQuery += "  case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],"
                FinalQuery += " case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],"
                FinalQuery += "  case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],"
                FinalQuery += " case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]"
                FinalQuery += " from ("
                FinalQuery += " select xx.*"
                FinalQuery += " from ( "
                FinalQuery += "select datename(MONTH,pp.date) +'-'+ datename(YEAR,pp.date) as [MONTH-YEAR],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],"
                FinalQuery += " case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]"
                FinalQuery += " ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],"
                FinalQuery += " sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],"
                FinalQuery += " sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], sum(pp.[Cow Milk Qty (Ltr)]) as [Cow Milk Qty (Ltr)] ,"
                FinalQuery += " sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)], sum([Buffalo Milk Qty (Ltr)]) as [Buffalo Milk Qty (Ltr)], "
                FinalQuery += " sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount)as VSP_Commission_Amount,sum(VSP_Deduction_Amount) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle from ("
                FinalQuery += "" + Environment.NewLine + Environment.NewLine + qry + Environment.NewLine + Environment.NewLine + ""
                FinalQuery += " ) as  pp group by datename(MONTH,pp.date) +'-'+ datename(YEAR,pp.date) "
                FinalQuery += " )as xx"
                FinalQuery += " ) as xxx"
                FinalQuery += " ) as aa   "
                FinalQuery += "   left outer join (select [MONTH-YEAR], sum ([PROD.LTS]) as [PROD.LTS] ,sum ([P.T.C LTS]) as [P.T.C LTS]  from (
                                  select datename(MONTH,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) +'-'+ datename(YEAR,TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date) as [MONTH-YEAR], case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [PROD.LTS], case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end as [P.T.C LTS]   from TSPL_MILK_SHIFT_UPLOADER_DETAIL left outer join TSPL_MILK_SHIFT_UPLOADER_HEAD on TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No = TSPL_MILK_SHIFT_UPLOADER_DETAIL.Document_No
                                  where 2 = 2  and TSPL_MILK_SHIFT_UPLOADER_HEAD.Status =1 and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_SHIFT_UPLOADER_HEAD.Shift_Date as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy") + "'  and TSPL_MILK_SHIFT_UPLOADER_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "') and  case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type in ('company','VSP') then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end + case when TSPL_MILK_SHIFT_UPLOADER_DETAIL.Reject_Type = 'Transpoter' then  TSPL_MILK_SHIFT_UPLOADER_DETAIL.Milk_Weight else 0 end > 0 )  as TBL_Reject group by [MONTH-YEAR]) as TBL_Reject_Data on  TBL_Reject_Data.[MONTH-YEAR] = aa.[MONTH-YEAR] "
                FinalQuery += " order by yy,mm "
                qry = FinalQuery

            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True


                RadPageView1.SelectedPage = RadPageViewPage2

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            If rdbDetails.Checked = False And rdbSummary.Checked = False Then
                dt.Columns.Add("AddDash", GetType(String))
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    dt.Rows((dt.Rows.Count - 1))("AddDash") = "1"
                End If
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()

            gv1.BestFitColumns()
            If isDotMatrixPrint = True AndAlso rdbDetails.Checked = True Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = "" 'objCommonVar.CurrentCompanyName
                obj.ReportName1 = "MILK SHED NAME : " + txtLocName.Text + "                             FORTINIGHT CONSOLIDATED OF MILK PAYMENT, DEDUCTION, NET AMOUNT PAYABLE AND T.I.P DEDUCTION, SHARE-CAPITAL DEDUCTIONS PERIOD FROM: " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "MMM dd, yyyy")) + " TO " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "MMM dd, yyyy")) + "                        "
                'obj.ReportName2 = "                                 DETAILS OF GROSS AMOUNT                                                                                             DETAILS OF RECOVERIES"
                obj.ShowPageNo = False
                obj.LandscapPageSetupColumnsChar = 340
                'obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Zone", clsCommon.myCstr(dt.Rows(0)("Zone_Name"))))
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MCC Name", "NAME OF THE MCC/DAIRY", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("BM Amount", "BM Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CM Amount", "CM Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("COMSN Amount", "COMSN Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("OP-COST Amount", "OP-COST Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cart Amount", "Cart Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("INC Amount", "INC Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ADDN Amount", "ADDN Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Gross Amount", "Gross Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                If clsCommon.myLen(StrDeductionHead) > 0 Then
                    Dim result As String() = StrDeductionHead.Split(New String() {","}, StringSplitOptions.None)
                    Dim finalchar As String = ""
                    For Each s As String In result
                        finalchar = s.Replace("[", "")
                        finalchar = finalchar.Replace("]", "") ' SetSpace
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn(finalchar, (finalchar), False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    Next
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total Deduction Amount", "Total DEDN'sAmt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Net Amount", "Net Amount", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                End If
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("T.I.P Amount", "T.I.P Amount", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("KG Fat(RS.30)", "KG Fat(RS.30)", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.Print(obj, dt, PageSetup.Landscap)

            ElseIf isDotMatrixPrint = True AndAlso rdbSummary.Checked = True Then
                Dim qry2 As String = " select 1 as SNo, '" + clsCommon.myCstr("BM") + "' as  MILK , " + clsCommon.myCstr(dt.Rows(0)("BM Qty(Ltr)")) + " as [QTY-LTS], " + clsCommon.myCstr(dt.Rows(0)("BM Avg(Ltr)")) + " as [AVG-LTS] , " + clsCommon.myCstr(dt.Rows(0)("BM Avg Rate")) + " as [AVG-RATE]
                                       Union All
                                       select 2 as SNo, '" + clsCommon.myCstr("CM") + "' as  MILK , " + clsCommon.myCstr(dt.Rows(0)("CM Qty(Ltr)")) + " as [QTY-LTS], " + clsCommon.myCstr(dt.Rows(0)("CM Avg(Ltr)")) + " as [AVG-LTS] , " + clsCommon.myCstr(dt.Rows(0)("CM Avg Rate")) + " as [AVG-RATE]
                                        Union All
                                       select 3 as SNo, '" + clsCommon.myCstr("MM") + "' as  MILK , " + clsCommon.myCstr(dt.Rows(0)("MM Qty(Ltr)")) + " as [QTY-LTS], " + clsCommon.myCstr(dt.Rows(0)("MM Avg(Ltr)")) + " as [AVG-LTS] , " + clsCommon.myCstr(dt.Rows(0)("MM Avg Rate")) + " as [AVG-RATE]
                                     "
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)

                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = "MILK BILL STATEMENT ABSTRACT"
                obj.ReportName1 = "-----------------------------------------------------------------------------------------------------------------"

                obj.ReportName2 = "SHED NAME : " + txtLocName.Text + "                                                                                                                                                 "
                obj.ReportName3 = "PERIOD FROM : " + clsCommon.myCstr(dtpFromDate.Text) + " To " + clsCommon.myCstr(dtpToDate.Text) + "                                                                                "
                obj.ReportName4 = "-----------------------------------------------------------------------------------------------------------------"

                obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("SHED NAME", txtLocName.Text))
                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(dtpFromDate.Text) + " To " + clsCommon.myCstr(dtpToDate.Text)))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TSDDCF BM(KGFAT)", clsCommon.myCstr(dt.Rows(0)("BM TSDDCS Rate"))))
                obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TSDDCF CM(TOTAL SOLIDS)", clsCommon.myCstr(dt.Rows(0)("CM TSDDCS Rate"))))
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MILK", "MILK", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("QTY-LTS", "QTY-LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AVG-LTS", "AVG-LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AVG-RATE", "AVG-RATE", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrReportFooter = New List(Of clsDosPrintReportFooter)
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("BM TOTAL MILK AMOUNT", "     ", ":", clsCommon.myCstr(dt.Rows(0)("BM Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("CM TOTAL MILK AMOUNT", "     ", ":", clsCommon.myCstr(dt.Rows(0)("CM Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL OP-COST       ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("OP-COST Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL CARTAGE       ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("CART Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL ADDITIONS     ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("ADDN Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL TIP AMOUNT    ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("T.I.P Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL DIF AMOUNT    ", "     ", ":", "0", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("--------------------", "---------------------", "------------", "", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("GROSS AMOUNT        ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("Gross Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("--------------------", "---------------------", "------------", "", ""))
                If clsCommon.myLen(StrDeductionHead) > 0 Then
                    Dim result As String() = StrDeductionHead.Split(New String() {","}, StringSplitOptions.None)
                    Dim finalchar As String = ""
                    For Each s As String In result
                        finalchar = s.Replace("[", "")
                        finalchar = finalchar.Replace("]", "")
                        obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject(finalchar, "     ", ":", clsCommon.myCstr(dt.Rows(0)("" + finalchar + "")), ""))
                        'obj.arrColumn.Add(clsDosPrintColumn.SetColumn(finalchar, SetSpace(finalchar + " AMT"), False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    Next
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("--------------------", "---------------------", "------------", "", ""))
                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL DEDUCTION     ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("Total Deduction Amount")), ""))

                    obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("--------------------", "---------------------", "------------", "", ""))

                End If
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("NET AMOUNT PAYABLE      ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("Net Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("KGFAT VALUE             ", "     ", ":", clsCommon.myCstr("0"), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("                        ", "     ", "", "--------", ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("TOTAL PAYABLE           ", "     ", ":", clsCommon.myCstr(dt.Rows(0)("Net Amount")), ""))
                obj.arrReportFooter.Add(clsDosPrintReportFooter.GetObject("                        ", "     ", "", "--------", ""))
                obj.Print(obj, dt2, PageSetup.Potrate)

            ElseIf rdbProcurementAbstract.Checked = True AndAlso chkRejection.Checked = False Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = ""
                obj.ReportName1 = "MILK SHED NAME : " + txtLocName.Text + "                                                          FORTINIGHT CONSOLIDATED REPORT OF MILK PROCUREMENT, KG-FAT,KG-SNF,TOTAL SOLIDS PERIOD FROM: " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "MMM dd, yyyy")) + " TO " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "MMM dd, yyyy")) + "                                                       "
                obj.ShowPageNo = False
                obj.LandscapPageSetupColumnsChar = 270

                obj.arrMergeColumn = New List(Of clsDosPrintMergeColumn)()
                Dim objMergeColumn As clsDosPrintMergeColumn = New clsDosPrintMergeColumn()
                objMergeColumn.MergeText = "BM"
                objMergeColumn.arrColumn = New List(Of String)()
                objMergeColumn.arrColumn.Add("Buffalo Milk Qty (Ltr)")
                objMergeColumn.arrColumn.Add("Buffalo Milk Qty (KG)")
                objMergeColumn.arrColumn.Add("Buffalo FAT (KG)")
                objMergeColumn.arrColumn.Add("Buffalo SNF (KG)")
                objMergeColumn.arrColumn.Add("Buffalo Total Solid")
                objMergeColumn.arrColumn.Add("Buffalo Avg FAT")
                objMergeColumn.arrColumn.Add("Buffalo Avg SNF")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn()
                objMergeColumn.MergeText = "CM"
                objMergeColumn.arrColumn = New List(Of String)()
                objMergeColumn.arrColumn.Add("Cow Milk Qty (Ltr)")
                objMergeColumn.arrColumn.Add("Cow Milk Qty (KG)")
                objMergeColumn.arrColumn.Add("Cow FAT (KG)")
                objMergeColumn.arrColumn.Add("Cow SNF (KG)")
                objMergeColumn.arrColumn.Add("Cow Total Solid")
                objMergeColumn.arrColumn.Add("Cow Avg FAT")
                objMergeColumn.arrColumn.Add("Cow Avg SNF")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn()
                objMergeColumn.MergeText = "MM"
                objMergeColumn.arrColumn = New List(Of String)()
                objMergeColumn.arrColumn.Add("Mixed Milk Weight(LTR)")
                objMergeColumn.arrColumn.Add("Mixed Milk Weight(KG)")
                objMergeColumn.arrColumn.Add("Mixed FAT(KG)")
                objMergeColumn.arrColumn.Add("Mixed SNF(KG)")
                objMergeColumn.arrColumn.Add("Mixed Total Solid")
                objMergeColumn.arrColumn.Add("Mixed Avg FAT")
                objMergeColumn.arrColumn.Add("Mixed Avg SNF")
                obj.arrMergeColumn.Add(objMergeColumn)

                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MCC Code", "UNIT CODE", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MCC Name", "UNIT NAME", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                '--Buffalo Milk Qty (KG),Buffalo Milk Qty (Ltr),Buffalo FAT (KG),Buffalo SNF (KG),Buffalo Total Solid,Buffalo Avg FAT , Buffalo Avg SNF
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Milk Qty (Ltr)", "       LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Milk Qty (KG)", "       KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo FAT (KG)", "     KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo SNF (KG)", "     KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Total Solid", "Total Solid", False, DosPrintAlignment.Right, 11, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Avg FAT", "    AV.FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Avg SNF", "    AV.SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                ' Cow Milk Qty (KG), Cow Milk Qty (Ltr),Cow FAT (KG),Cow SNF (KG),Cow Total Solid, Cow Avg FAT , Cow Avg SNF
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Milk Qty (Ltr)", "      LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Milk Qty (KG)", "      KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow FAT (KG)", "    KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow SNF (KG)", "   KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Total Solid", "Total Solid", False, DosPrintAlignment.Right, 12, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Avg FAT", "   AV.FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Avg SNF", "  AV.SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                'Mixed Milk Weight(KG),Mixed Milk Weight(LTR),Mixed FAT(KG),Mixed SNF(KG),Mixed Total Solid,Mixed Avg FAT,Mixed Avg SNF
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Milk Weight(LTR)", "     LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Milk Weight(KG)", "     KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed FAT(KG)", "    KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed SNF(KG)", "    KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Total Solid", "Total Solid", False, DosPrintAlignment.Right, 12, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Avg FAT", "  AV.FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Avg SNF", "   AV.SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PROD.LTS", "PROD. LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("P.T.C LTS", "P.T.C LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.Print(obj, dt, PageSetup.Landscap, "AddDash", "1")
            ElseIf rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = "" 'objCommonVar.CurrentCompanyName
                obj.ReportName1 = "MILK SHED NAME : " + txtLocName.Text + "                                 YEARLY CONSOLIDATED OF MILK PAYMENT, DEDUCTION, NET AMOUNT PAYABLE AND T.I.P DEDUCTION, SHARE-CAPITAL DEDUCTIONS PERIOD FROM: " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "MMM dd, yyyy")) + " TO " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "MMM dd, yyyy")) + "                        "
                'obj.ReportName2 = "                                 DETAILS OF GROSS AMOUNT                                                                                             DETAILS OF RECOVERIES"
                obj.ShowPageNo = False
                obj.LandscapPageSetupColumnsChar = 340
                'obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("Zone", clsCommon.myCstr(dt.Rows(0)("Zone_Name"))))
                obj.arrColumn = New List(Of clsDosPrintColumn)()
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MONTH-YEAR", "MONTH-YEAR", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("BM Amount", "BM Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CM Amount", "CM Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("COMSN Amount", "COMSN Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("OP-COST Amount", "OP-COST Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cart Amount", "Cart Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("INC Amount", "INC Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ADDN Amount", "ADDN Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Gross Amount", "Gross Amt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                If clsCommon.myLen(StrDeductionHead) > 0 Then
                    Dim result As String() = StrDeductionHead.Split(New String() {","}, StringSplitOptions.None)
                    Dim finalchar As String = ""
                    For Each s As String In result
                        finalchar = s.Replace("[", "")
                        finalchar = finalchar.Replace("]", "") ' SetSpace
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn(finalchar, (finalchar), False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    Next
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total Deduction Amount", "Total DEDN'sAmt", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Net Amount", "Net Amount", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                End If
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("T.I.P Amount", "T.I.P Amount", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("KG Fat(RS.30)", "KG Fat(RS.30)", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                obj.Print(obj, dt, PageSetup.Landscap)
            ElseIf rdbYearlyConsolidatedReportofMilkProcurement.Checked = True Then
                Dim obj As clsDosPrint = New clsDosPrint()
                obj.ReportName = ""
                obj.ReportName1 = "MILK SHED NAME : " + txtLocName.Text + "                                                              YEARLY CONSOLIDATED REPORT OF MILK PROCUREMENT, KG-FAT,KG-SNF,TOTAL SOLIDS PERIOD FROM: " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpFromDate.Value, "MMM dd, yyyy")) + " TO " + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "MMM dd, yyyy")) + "                                                       "
                obj.ShowPageNo = False
                obj.LandscapPageSetupColumnsChar = 270

                obj.arrMergeColumn = New List(Of clsDosPrintMergeColumn)()
                Dim objMergeColumn As clsDosPrintMergeColumn = New clsDosPrintMergeColumn()
                objMergeColumn.MergeText = "BM"
                objMergeColumn.arrColumn = New List(Of String)()
                objMergeColumn.arrColumn.Add("Buffalo Milk Qty (Ltr)")
                objMergeColumn.arrColumn.Add("Buffalo Milk Qty (KG)")
                objMergeColumn.arrColumn.Add("Buffalo FAT (KG)")
                objMergeColumn.arrColumn.Add("Buffalo SNF (KG)")
                objMergeColumn.arrColumn.Add("Buffalo Total Solid")
                objMergeColumn.arrColumn.Add("Buffalo Avg FAT")
                objMergeColumn.arrColumn.Add("Buffalo Avg SNF")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn()
                objMergeColumn.MergeText = "CM"
                objMergeColumn.arrColumn = New List(Of String)()
                objMergeColumn.arrColumn.Add("Cow Milk Qty (Ltr)")
                objMergeColumn.arrColumn.Add("Cow Milk Qty (KG)")
                objMergeColumn.arrColumn.Add("Cow FAT (KG)")
                objMergeColumn.arrColumn.Add("Cow SNF (KG)")
                objMergeColumn.arrColumn.Add("Cow Total Solid")
                objMergeColumn.arrColumn.Add("Cow Avg FAT")
                objMergeColumn.arrColumn.Add("Cow Avg SNF")
                obj.arrMergeColumn.Add(objMergeColumn)

                objMergeColumn = New clsDosPrintMergeColumn()
                objMergeColumn.MergeText = "MM"
                objMergeColumn.arrColumn = New List(Of String)()
                objMergeColumn.arrColumn.Add("Mixed Milk Weight(LTR)")
                objMergeColumn.arrColumn.Add("Mixed Milk Weight(KG)")
                objMergeColumn.arrColumn.Add("Mixed FAT(KG)")
                objMergeColumn.arrColumn.Add("Mixed SNF(KG)")
                objMergeColumn.arrColumn.Add("Mixed Total Solid")
                objMergeColumn.arrColumn.Add("Mixed Avg FAT")
                objMergeColumn.arrColumn.Add("Mixed Avg SNF")
                obj.arrMergeColumn.Add(objMergeColumn)

                obj.arrColumn = New List(Of clsDosPrintColumn)()
                'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MCC Code", "UNIT CODE", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("MONTH-YEAR", "MONTH-YEAR", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                '--Buffalo Milk Qty (KG),Buffalo Milk Qty (Ltr),Buffalo FAT (KG),Buffalo SNF (KG),Buffalo Total Solid,Buffalo Avg FAT , Buffalo Avg SNF
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Milk Qty (Ltr)", "       LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Milk Qty (KG)", "       KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo FAT (KG)", "     KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo SNF (KG)", "     KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Total Solid", "Total Solid", False, DosPrintAlignment.Right, 11, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Avg FAT", "    AV.FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Buffalo Avg SNF", "    AV.SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                ' Cow Milk Qty (KG), Cow Milk Qty (Ltr),Cow FAT (KG),Cow SNF (KG),Cow Total Solid, Cow Avg FAT , Cow Avg SNF
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Milk Qty (Ltr)", "      LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Milk Qty (KG)", "      KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow FAT (KG)", "    KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow SNF (KG)", "   KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Total Solid", "Total Solid", False, DosPrintAlignment.Right, 12, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Avg FAT", "   AV.FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cow Avg SNF", "  AV.SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                'Mixed Milk Weight(KG),Mixed Milk Weight(LTR),Mixed FAT(KG),Mixed SNF(KG),Mixed Total Solid,Mixed Avg FAT,Mixed Avg SNF
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Milk Weight(LTR)", "     LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Milk Weight(KG)", "     KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed FAT(KG)", "    KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed SNF(KG)", "    KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Total Solid", "Total Solid", False, DosPrintAlignment.Right, 12, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Avg FAT", "  AV.FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Mixed Avg SNF", "   AV.SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PROD.LTS", "PROD. LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.arrColumn.Add(clsDosPrintColumn.SetColumn("P.T.C LTS", "P.T.C LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                obj.Print(obj, dt, PageSetup.Landscap, "AddDash", "1")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Shared Function SetSpace(ByVal strValue As String) As String
        Dim blankSpace As Integer = 19 - strValue.Length
        Dim i As Integer = 0
        For i = 0 To blankSpace
            strValue = " " + strValue
        Next





        Return strValue
    End Function

    Public Sub columnEnableDisable()
        If rdbDetails.Checked = True Then
            If gv1.Columns.Contains("BM Qty") = True Then
                gv1.Columns("BM Qty").IsVisible = False
            End If
            If gv1.Columns.Contains("CM Qty") = True Then
                gv1.Columns("CM Qty").IsVisible = False
            End If
        ElseIf rdbSummary.Checked = True Then

        End If
    End Sub
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next
        columnEnableDisable()
    End Sub
    Sub Reset()
        txtPlant.arrValueMember = Nothing
        txtMCC.arrValueMember = Nothing
        txtReciptMCC.arrValueMember = Nothing
        txtReciptMCC.arrDispalyMember = Nothing
        fndSingleMCCCode.Value = ""
        lblSingleMCCName.Text = ""
        dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        txtMilkReceiptFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtMilkReciptToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        dtpToDate.Enabled = True
        dtpToDate.ReadOnly = True
        dtpFromDate.Enabled = True
        gv1.DataSource = Nothing
        If rdbMilkReceipts.Checked = True OrElse rdbCheckList.Checked = True OrElse rdbUnitWiseTotal.Checked = True OrElse rdbUnitWiseAnalysis.Checked = True OrElse rdbRouteBillsAbstract.Checked = True OrElse rdbUnitWiseDeduction.Checked = True OrElse rdbTIPsummaryReportMCCandVLCwise.Checked = True OrElse rdbUnitWiseBillSummary.Checked = True Then
            pnlMilkReceipts.Visible = True
            'RadSplitButton1.Enabled = False
            'btnGo.Enabled = False

        Else
            pnlMilkReceipts.Visible = False
            RadSplitButton1.Enabled = True
            btnGo.Enabled = True
        End If
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        'PageSetupReport_ID = MyBase.Form_ID
        'TemplateGridview = gv1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        If rdbMilkReceipts.Checked = True Then
            MilkRepiptPrint(False)
        ElseIf rdbCheckList.Checked = True Then
            CheckListPrint(False)
        ElseIf rdbUnitWiseTotal.Checked = True Then
            UnitWiseTotal(False)
        ElseIf rdbUnitWiseAnalysis.Checked = True Then
            UnitwiseAnalysis(False)
        ElseIf rdbRouteBillsAbstract.Checked = True Then
            RouteBillsAbstract(False)
        ElseIf rdbUnitWiseDeduction.Checked = True Then
            UnitWiseDeduction(False)
        ElseIf rdbTIPsummaryReportMCCandVLCwise.Checked = True Then
            TIPsummaryReportMCCandVLCwise(False)
        ElseIf rdbUnitWiseBillSummary.Checked = True Then
            UnitwiseBillSummary(False)
        Else
            Print(Exporter.Refresh)
        End If
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rptTankerStatusReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        MultipleFinderFillAuto = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MultipleFinderFillAuto, clsFixedParameterCode.MultipleFinderFillAuto, Nothing)) = 1)
        Reset()
    End Sub



    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & Me.Text)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")

                arrHeader.Add("Shed : " + clsCommon.myCstr(txtLocName.Text))

                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & Me.Text)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")

                arrHeader.Add("Shed : " + clsCommon.myCstr(txtLocName.Text))

                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
    '    Try
    '        Dim qry As String = "select tspl_mcc_master.MCC_Code as [Code],tspl_mcc_master.MCC_NAME as [Name] from tspl_mcc_master"
    '        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSl@MCC", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtPlant__My_Click(sender As Object, e As EventArgs) Handles txtPlant._My_Click
    '    Try
    '        Dim qry As String = "Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc as Name from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Type = 'PLANT'"
    '        txtPlant.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@PL", qry, "Code", "Name", txtPlant.arrValueMember, txtPlant.arrDispalyMember)
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub


    Private Sub dtpFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtpFromDate.Validating
        If rdbYearlyConsolidatedofMilkPayment.Checked = True OrElse rdbYearlyConsolidatedReportofMilkProcurement.Checked = True Then
        Else
            SetToDate()
        End If
    End Sub

    Private Sub dtpFromDate_Leave(sender As Object, e As EventArgs) Handles dtpFromDate.Leave
        If rdbYearlyConsolidatedofMilkPayment.Checked = True OrElse rdbYearlyConsolidatedReportofMilkProcurement.Checked = True Then
        Else
            SetToDate()
        End If
    End Sub
    Sub SetToDate()
        'If Not isLoad Then
        Dim PaymentCycleType As String = ""
        Dim PaymentCycleValue As Integer = 0
        ' If Not isLoad Then
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
            Exit Sub
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.Plant_Code ='" & fndLoc.Value & "'")
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
            Exit Sub
        End If
        PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
        PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
        Dim dtCurr As DateTime = clsCommon.GETSERVERDATE()
        If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
            If dtpFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                dtpFromDate.Value = New Date(dtCurr.Year, dtCurr.Month, 1)
                dtpToDate.Value = dtpFromDate.Value
                Exit Sub
            End If
            dtpToDate.Value = dtpFromDate.Value.AddDays(PaymentCycleValue - 1)

            If dtpFromDate.Value.Month <> dtpToDate.Value.Month Then
                dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
            Dim dtNxtPay As DateTime = dtpToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
            If dtpFromDate.Value.Month <> dtNxtPay.Month Then
                dtpToDate.Value = New Date(dtpFromDate.Value.Year, dtpFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
            End If
        ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
            If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                dtpToDate.Value = "01/" & DatePart(DateInterval.Month, dtCurr) & "/" & DatePart(DateInterval.Year, dtCurr)
                Exit Sub
            End If
            dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
        ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
            Dim today As Date = dtpFromDate.Value
            Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
            dtpFromDate.Value = today.AddDays(-dayDiff)
            dtpToDate.Value = dtpFromDate.Value.AddDays(6)
        End If
        ' End If
        'End If
    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim whrCls As String = "TSPL_LOCATION_MASTER.Type = 'PLANT'"

        fndLoc.Value = clsLocation.getFinder(whrCls, fndLoc.Value, isButtonClicked)
        txtLocName.Text = clsLocation.GetName(fndLoc.Value, Nothing)
        If clsCommon.myLen(fndLoc.Value) > 0 AndAlso rdbMilkReceipts.Checked = False AndAlso rdbCheckList.Checked = False AndAlso rdbUnitWiseTotal.Checked = False AndAlso rdbUnitWiseAnalysis.Checked = False AndAlso rdbRouteBillsAbstract.Checked = False AndAlso rdbUnitWiseDeduction.Checked = False AndAlso rdbTIPsummaryReportMCCandVLCwise.Checked = False AndAlso rdbUnitWiseBillSummary.Checked = False Then

            Dim PaymentCycleType As String = ""
            Dim PaymentCycleValue As Integer = 0
            ' If Not isLoad Then
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select the Location first", Me.Text)
                Exit Sub
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select isnull(TSPL_MCC_MASTER.empOnAmountOnly,0) as empOnAmountOnly,TSPL_MCC_MASTER.Payment_Cycle,TSPL_PAYMENT_CYCLE_MASTER.PC_TYPE,TSPL_PAYMENT_CYCLE_MASTER.PC_VALUE  from TSPL_MCC_MASTER left outer join TSPL_PAYMENT_CYCLE_MASTER on TSPL_PAYMENT_CYCLE_MASTER.PC_CODE=TSPL_MCC_MASTER.Payment_Cycle   where TSPL_MCC_MASTER.Plant_Code ='" & fndLoc.Value & "'")
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found on current MCC/Location", Me.Text)
                Exit Sub
            End If
            PaymentCycleType = clsCommon.myCstr(dt.Rows(0)("PC_TYPE"))
            PaymentCycleValue = clsCommon.myCdbl(dt.Rows(0)("PC_VALUE"))
            'isEmpOnAmtOnly = IIf(clsCommon.myCdbl(dt.Rows(0)("empOnAmountOnly")) = 0, False, True)
            If clsCommon.CompairString(PaymentCycleType, "Day") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                    clsCommon.MyMessageBoxShow("Date can only be first day of month or at interval of " & PaymentCycleValue & " Day, Because MCC has payment Cycle of " & PaymentCycleValue & " Day ")
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Day, PaymentCycleValue - 1, dtpFromDate.Value)
                If DatePart(DateInterval.Month, dtpFromDate.Value) <> DatePart(DateInterval.Month, dtpToDate.Value) Then
                    dtpToDate.Value = DateAdd(DateInterval.Month, 1, clsCommon.myCDate("01/" & DatePart(DateInterval.Month, dtpFromDate.Value) & "/" & DatePart(DateInterval.Year, dtpFromDate.Value)))
                    dtpToDate.Value = DateAdd(DateInterval.Day, -1, dtpToDate.Value)
                End If
            ElseIf clsCommon.CompairString(PaymentCycleType, "Month") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Month Type", Me.Text)
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Month, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Year") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(clsCommon.GetPrintDate(dtpFromDate.Value, "dd")) <> 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Date can only be first day of month, Because MCC has payment Cycle of Year Type", Me.Text)
                    dtpFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    dtpToDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
                    Exit Sub
                End If
                dtpToDate.Value = DateAdd(DateInterval.Year, PaymentCycleValue, dtpFromDate.Value)
            ElseIf clsCommon.CompairString(PaymentCycleType, "Week") = CompairStringResult.Equal Then
                Dim today As Date = dtpFromDate.Value
                Dim dayDiff As Integer = today.DayOfWeek - IIf(PaymentCycleValue = 1, DayOfWeek.Sunday, IIf(PaymentCycleValue = 2, DayOfWeek.Monday, IIf(PaymentCycleValue = 3, DayOfWeek.Tuesday, IIf(PaymentCycleValue = 4, DayOfWeek.Wednesday, IIf(PaymentCycleValue = 5, DayOfWeek.Thursday, IIf(PaymentCycleValue = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                dtpFromDate.Value = today.AddDays(-dayDiff)
                dtpToDate.Value = dtpFromDate.Value.AddDays(6)
            End If
        End If

    End Sub

    Private Sub btnDotMatrixPrint_Click(sender As Object, e As EventArgs) Handles btnDotMatrixPrint.Click
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        If rdbMilkReceipts.Checked = True Then
            MilkRepiptPrint(True)
        ElseIf rdbCheckList.Checked = True Then
            CheckListPrint(True)
        ElseIf rdbUnitWiseTotal.Checked = True Then
            UnitWiseTotal(True)
        ElseIf rdbUnitWiseAnalysis.Checked = True Then
            UnitwiseAnalysis(True)
        ElseIf rdbRouteBillsAbstract.Checked = True Then
            RouteBillsAbstract(True)
        ElseIf rdbUnitWiseDeduction.Checked = True Then
            UnitWiseDeduction(True)
        ElseIf rdbTIPsummaryReportMCCandVLCwise.Checked Then
            TIPsummaryReportMCCandVLCwise(True)
        ElseIf rdbUnitWiseBillSummary.Checked = True Then
            UnitwiseBillSummary(True)
        Else
            Print(Exporter.Refresh, True)
        End If

    End Sub

    Public Sub MilkRepiptPrint(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            Dim strCorrectionFactor As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where code = 'MCCdefaultCorrectionFactorBS'  and Type = 'MCCdefaultCorrectionFactorBS'"))
            Dim strBMItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where type = 'MCC Default Milk Item Buffalo' and code = 'MilkSetting'"))
            Dim strCMItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where type = 'MCC Default Milk Item Cow' and code = 'MilkSetting'"))
            Dim strMMItem As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select  Description from TSPL_FIXED_PARAMETER where type = 'MCCDefaultMilkItem' and code = 'MilkSetting'"))

            Dim qry As String = " Select * from (
                              select 1 as SNo, tspl_mcc_dispatch_challan.MCC_Code,tspl_location_master_Name.Location_Desc as [MCC_Name] ,tspl_mcc_dispatch_challan.Chalan_NO as [Dispatch No],convert (varchar ,tspl_mcc_dispatch_challan.Dispatch_Date,103) as [Date],TSPL_MILK_TRANSFER_IN.Receipt_Challan_No as [Acknowledgement No]  , convert (varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) as [Acknowledgement Date] ,tspl_mcc_dispatch_challan.Tanker_No , tspl_mcc_dispatch_challan.Tanker_Dispatch_To , tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code, case when   TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strBMItem + "' then 'BM'  when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strCMItem + "' then 'CM' when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strMMItem + "' then 'MM' else '' end as [TYPE]  , TSPL_INVENTORY_MOVEMENT_NEW.Qty as [Qty(KG)],convert (decimal(18,2) , (TSPL_INVENTORY_MOVEMENT_NEW.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )  as [Qty(LTR)] , TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per , TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per , Cast (((TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per -" + strCorrectionFactor + "-(0.2 * TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per))*4 ) as Decimal (18,2)) as [CLR], cast ( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,2)) as Fat_KG, cast ( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal (18,2) ) as SNF_KG  from tspl_mcc_dispatch_challan 
                              left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No = tspl_mcc_dispatch_challan.Chalan_NO
                              inner join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No
                              left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = tspl_mcc_dispatch_challan.MCC_Code
                              left outer join tspl_location_master on tspl_location_master.Location_Code = tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code
                              left outer join tspl_location_master as tspl_location_master_Name on tspl_location_master_Name.Location_Code = tspl_mcc_dispatch_challan.MCC_Code
                              left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
                              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_INVENTORY_MOVEMENT_NEW.UOM = Stocking_Conversion_Factor.UOM_Code
                              where tspl_mcc_dispatch_challan.isPosted =1
                              and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103) and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)    and  ( tspl_mcc_dispatch_challan.MCC_Code = '" + fndLoc.Value + "' or  TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' ) "
            If txtReciptMCC.arrValueMember IsNot Nothing AndAlso txtReciptMCC.arrValueMember.Count > 0 Then
                qry += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + clsCommon.GetMulcallString(txtReciptMCC.arrValueMember) + " )"
            End If
            If rdbMainPlant.Checked Then
                qry += "  and  isnull (tspl_location_master.IsMainPlant,0) = 1 "
            ElseIf rdbOther.Checked Then
                qry += "  and  isnull (tspl_location_master.IsMainPlant,0) = 0 "
            End If
            qry += " Union all "
            qry += " select max ( XXXFinal.SNo ) as  SNo , '' as MCC_Code ,'' as [MCC_Name], '' as [Dispatch No] ,XXXFinal.TYPE + ' Total' as  [Date] ,'' as [Acknowledgement No]  , '' as [Acknowledgement Date], '' as Tanker_No , '' as Tanker_Dispatch_To , '' Mcc_Or_Plant_Code  , '' as [TYPE], sum ([Qty(KG)]) as [Qty(KG)], sum ([Qty(LTR)]) as [Qty(LTR)],  cast ( ( sum (Fat_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2))  as Fat_Per, cast ( ( sum (SNF_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2)) as SNF_Per , Null as  [CLR] , sum (Fat_KG) as Fat_KG ,  sum (SNF_KG) as SNF_KG   from  ( "
            qry += " select 2 as SNo, tspl_mcc_dispatch_challan.MCC_Code ,convert (varchar ,tspl_mcc_dispatch_challan.Dispatch_Date,103) as [Date], tspl_mcc_dispatch_challan.Tanker_No , tspl_mcc_dispatch_challan.Tanker_Dispatch_To , tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code, case when   TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strBMItem + "' then 'BM'  when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strCMItem + "' then 'CM' when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strMMItem + "' then 'MM' else '' end as [TYPE]  , TSPL_INVENTORY_MOVEMENT_NEW.Qty as [Qty(KG)],convert (decimal(18,2) , (TSPL_INVENTORY_MOVEMENT_NEW.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )  as [Qty(LTR)] , TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per , TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per , Cast (((TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per -" + strCorrectionFactor + "-(0.2 * TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per))*4 ) as Decimal (18,2)) as [CLR], cast ( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,2)) as Fat_KG, cast ( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal (18,2) ) as SNF_KG  from tspl_mcc_dispatch_challan 
                              left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No = tspl_mcc_dispatch_challan.Chalan_NO
                              inner join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No
                              left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = tspl_mcc_dispatch_challan.MCC_Code
                              left outer join tspl_location_master on tspl_location_master.Location_Code = tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code
                              left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
                              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_INVENTORY_MOVEMENT_NEW.UOM = Stocking_Conversion_Factor.UOM_Code
                              where tspl_mcc_dispatch_challan.isPosted =1
                              and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)     and   ( tspl_mcc_dispatch_challan.MCC_Code = '" + fndLoc.Value + "' or  TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' )  "
            If txtReciptMCC.arrValueMember IsNot Nothing AndAlso txtReciptMCC.arrValueMember.Count > 0 Then
                qry += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + clsCommon.GetMulcallString(txtReciptMCC.arrValueMember) + " )"
            End If
            If rdbMainPlant.Checked Then
                qry += "  and  isnull (tspl_location_master.IsMainPlant,0) = 1 "
            ElseIf rdbOther.Checked Then
                qry += "  and  isnull (tspl_location_master.IsMainPlant,0) = 0 "
            End If
            qry += " )  XXXFinal group by XXXFinal.TYPE "

            qry += " Union all "
            qry += " select  max ( XXXFinal.SNo ) as  SNo , '' as MCC_Code, '' as [MCC_Name] ,'' as [Dispatch No], 'Grand Total' as  [Date] , '' as [Acknowledgement No]  , '' as [Acknowledgement Date],'' as Tanker_No , '' as Tanker_Dispatch_To , '' Mcc_Or_Plant_Code  , '' as [TYPE], sum ([Qty(KG)]) as [Qty(KG)], sum ([Qty(LTR)]) as [Qty(LTR)],  cast ( ( sum (Fat_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2))  as Fat_Per, cast ( ( sum (SNF_KG) * 100 / nullif (sum ([Qty(KG)]),0)) as decimal(18,2)) as SNF_Per , Null as  [CLR] , sum (Fat_KG) as Fat_KG ,  sum (SNF_KG) as SNF_KG    from  ( "
            qry += " select 2 as SNo, tspl_mcc_dispatch_challan.MCC_Code ,convert (varchar ,tspl_mcc_dispatch_challan.Dispatch_Date,103) as [Date], tspl_mcc_dispatch_challan.Tanker_No , tspl_mcc_dispatch_challan.Tanker_Dispatch_To , tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code, case when   TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strBMItem + "' then 'BM'  when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strCMItem + "' then 'CM' when  TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = '" + strMMItem + "' then 'MM' else '' end as [TYPE]  , TSPL_INVENTORY_MOVEMENT_NEW.Qty as [Qty(KG)],convert (decimal(18,2) , (TSPL_INVENTORY_MOVEMENT_NEW.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) )  as [Qty(LTR)] , TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per , TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per , Cast (((TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per -" + strCorrectionFactor + "-(0.2 * TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per))*4 ) as Decimal (18,2)) as [CLR], cast ( TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as decimal(18,2)) as Fat_KG, cast ( TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as decimal (18,2) ) as SNF_KG  from  tspl_mcc_dispatch_challan 
                              left outer join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No = tspl_mcc_dispatch_challan.Chalan_NO
                              inner join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No = TSPL_MILK_TRANSFER_IN.Receipt_Challan_No
                              left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = tspl_mcc_dispatch_challan.MCC_Code
                              left outer join tspl_location_master on tspl_location_master.Location_Code = tspl_mcc_dispatch_challan.Mcc_Or_Plant_Code
                              left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_INVENTORY_MOVEMENT_NEW.Item_Code
                              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_INVENTORY_MOVEMENT_NEW.Item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_INVENTORY_MOVEMENT_NEW.UOM = Stocking_Conversion_Factor.UOM_Code
                              where tspl_mcc_dispatch_challan.isPosted =1
                              and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) >=  convert (date ,'" + txtMilkReceiptFromDate.Value + "' ,103)  and convert (date ,tspl_mcc_dispatch_challan.Dispatch_Date,103) <=  convert (date ,'" + txtMilkReciptToDate.Value + "' ,103)      and  ( tspl_mcc_dispatch_challan.MCC_Code = '" + fndLoc.Value + "' or  TSPL_MCC_MASTER.Plant_Code = '" + fndLoc.Value + "' )  "
            If txtReciptMCC.arrValueMember IsNot Nothing AndAlso txtReciptMCC.arrValueMember.Count > 0 Then
                qry += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + clsCommon.GetMulcallString(txtReciptMCC.arrValueMember) + " )"
            End If
            If rdbMainPlant.Checked Then
                qry += "  and  isnull (tspl_location_master.IsMainPlant,0) = 1 "
            ElseIf rdbOther.Checked Then
                qry += "  and  isnull (tspl_location_master.IsMainPlant,0) = 0 "
            End If
            qry += " )  XXXFinal group by XXXFinal.SNo  )Final order by Final.SNo asc "



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True


                dt.Columns.Add("AddDash", GetType(String))
                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Date")), "Grand Total") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Date")), "MM Total") = CompairStringResult.Equal Then
                        dt.Rows(i)("AddDash") = "1"
                    End If
                Next
                gv1.DataSource = dt
                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If
                If gv1.Columns.Contains("MCC_Code") = True Then
                    gv1.Columns("MCC_Code").HeaderText = "Location Code"
                End If
                If gv1.Columns.Contains("MCC_Name") = True Then
                    gv1.Columns("MCC_Name").HeaderText = "Location Name"
                End If

                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then


                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = ""
                    obj.ReportName1 = "M.P.F., HYDERABAD MILK RECEIPTS"
                    obj.ShowPageNo = True
                    obj.LandscapPageSetupColumnsChar = 120
                    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(txtLocName.Text)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("DATE", "DATE", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Tanker_No", "TANKER", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TYPE", "TYPE", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Qty(LTR)", "QTY-LTS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Fat_Per", "FAT%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Snf_Per", "SNF%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("CLR", "CLR", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Qty(KG)", "KGS", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Fat_KG", "KG-FAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SNF_KG", "KG-SNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.Print(obj, dt, PageSetup.Landscap, "AddDash", "1")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub CheckListPrint(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            If clsCommon.myLen(fndSingleMCCCode.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select MCC First.", Me.Text)
                Return
            End If
            Dim strShift As String = ""
            'If rdbMainPlant.Checked = True Then
            '    strShift = "M"
            'ElseIf rdbOther.Checked = True Then
            '    strShift = "E"
            'End If
            Dim BaseQuery As String = MCCMilkRegisterQueryWithRejection(fndLoc.Value, txtMilkReceiptFromDate.Value, txtMilkReciptToDate.Value, rdbMainPlant.Checked, rdbOther.Checked, fndSingleMCCCode.Value)
            'Dim BaseQuery As String = " select XXXFinal.[Plant Code] , XXXFinal.[Plant Name] , XXXFinal.[MCC Code],XXXFinal.[MCC Name],XXXFinal.[Doc Date] , [Milk Type]+'M' as [Milk Type] , XXXFinal.RejectType,XXXFinal.Defaulter,  [Milk Weight(KG)] , [Milk Weight(LTR)] , [FAT(%)], [SNF(%)] , 
            '                            case when len (XXXFinal.RejectType) = 0  then XXXFinal.[Milk Weight(KG)] else 0 end [Good Milk Qty(KGS)],
            '                            case when len (XXXFinal.RejectType) = 0  then XXXFinal.[Milk Weight(LTR)] else 0 end [Good Milk Qty(LTR)],
            '                            case when len (XXXFinal.RejectType) = 0  then XXXFinal.[FAT(%)] else 0 end [Good Milk FAT(%)],
            '                            case when len (XXXFinal.RejectType) = 0  then XXXFinal.[SNF(%)] else 0 end [Good Milk SNF(%)],
            '                            case when len (XXXFinal.RejectType) = 0  then XXXFinal.[FAT(KG)] else 0 end [Good Milk FAT KG],
            '                            case when len (XXXFinal.RejectType) = 0  then XXXFinal.[SNF(KG)] else 0 end [Good Milk SNF KG],
            '                            case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(KG)] else 0 end [Sour Qty(KG)],
            '                            case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(LTR)] else 0 end [Sour Qty(LTR)],
            '                            case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(%)] else 0 end [Sour FAT(%)],
            '                            case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(%)] else 0 end [Sour SNF(%)],
            '                            case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(KG)] else 0 end [Sour FAT KG],
            '                            case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(KG)] else 0 end [Sour SNF KG],
            '                            case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(KG)] else 0 end [Curd Qty(KG)],
            '                            case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(LTR)] else 0 end [Curd Qty(LTR)],
            '                            case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(%)] else 0 end [Curd FAT(%)],
            '                            case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(%)] else 0 end [Curd SNF(%)],
            '                            case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(KG)] else 0 end [Curd FAT KG],
            '                            case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(KG)] else 0 end [Curd SNF KG],
            '                            case when len (XXXFinal.RejectType) > 0 and len (XXXFinal.Defaulter) > 0 and XXXFinal.Defaulter = 'Transporter' then XXXFinal.[Milk Weight(KG)] else 0 end [PTC RECVRY] 
            '                            from (
            '                            Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift , final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] , final.[Sample No] ,final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)], final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)], Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter,  final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'C' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SAMPLE_DETAIL.TYPE = 'B' Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)]
            '                            ,TSPL_MILK_SAMPLE_DETAIL.TYPE   As [Milk Type] , TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_RECEIPT_DETAIL.UOM_Code, TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR,   TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount ,'' as RejectType,'' as RejectReason,'' as Defaulter   ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
            '                             ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount   ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle 
            '                             From TSPL_MILK_RECEIPT_DETAIL 
            '                             Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE 
            '                             Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE
            '                             Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO 
            '                             Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE
            '                             left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
            '                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
            '                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
            '                             Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE
            '                             Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE
            '                             left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
            '                             Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE
            '                             left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
            '                             Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
            '                             Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE 
            '                             And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) 
            '                             And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT 
            '                             Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE 
            '                             And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code 
            '                            left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code
            '                            left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per
            '                             left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  where 2 = 2  and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtMilkReceiptFromDate.Value), "dd/MMM/yyyy") + "'  and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtMilkReceiptFromDate.Value), "dd/MMM/yyyy") + "'  and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "')   and TSPL_MILK_RECEIPT_DETAIL.SHIFT = '" + strShift + "'  Union All   Select TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Cow FAT(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Cow SNF(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Buffalo FAT(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Buffalo SNF(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT <= 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG Else 0 End [Cow Milk Qty (KG)],  Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (KG)],  case when TSPL_MILK_REJECT_TYPE.Type is not null  then TSPL_MILK_REJECT_TYPE.Type When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type],  TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_REJECT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name],  Convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_REJECT_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_REJECT_DETAIL.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_REJECT_DETAIL.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name],TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight],TSPL_MILK_REJECT_DETAIL.UOM_Code, TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)],0 as CLR, Convert(decimal(18,3), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)],  Convert(decimal(18,3),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status],  TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate],  TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date ,  '' as IS_MANUAL ,'' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount,TSPL_MILK_REJECT_TYPE.Code as RejectType, case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' when TSPL_MILK_REJECT_DETAIL.Is_Return=3 then 'COB'  end as RejectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  
            '                             ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
            '                             ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount 
            '                             ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,t1.Vehicle 
            '                             From   TSPL_MILK_REJECT_DETAIL 
            '                             Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE 
            '                             left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO 
            '                             Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
            '                             left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code
            '                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
            '                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE 
            '                             Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE 
            '                             Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE 

            '                             Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE 
            '                              left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
            '                             Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE 
            '                             Left Outer Join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code,TSPL_Primary_Vehicle_Master.Vehicle from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
            '                             Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_REJECT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_REJECT_HEAD.SHIFT 
            '                             Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code 
            '                             left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per 
            '                             left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  left join TSPL_MILK_REJECT_TYPE on TSPL_MILK_REJECT_TYPE.code=TSPL_MILK_REJECT_DETAIL.Reject_Type  where 2=2   and TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtMilkReceiptFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtMilkReceiptFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_REJECT_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "') and TSPL_MILK_REJECT_HEAD.SHIFT = '" + strShift + "' ) As final where 2=2   

            '                             )  XXXFinal   "

            Dim qry As String = " Select * from (
                                 select 1 as SNo  , XXXXFinal.[Plant Code] , max( XXXXFinal.[Plant Name]) as [Plant Name] , XXXXFinal.[MCC Code] , max(XXXXFinal.[MCC Name]) as [MCC Name],XXXXFinal.[Vlc Uploader Code] ,XXXXFinal.[Vlc Code] , max(XXXXFinal.[VLC Name]) as [VLC Name],[Doc Date],[Milk Type], sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] , sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , isnull ( cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull (cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)], isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)],isnull (cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY] From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code]  , XXXXFinal.[MCC Code],[Vlc Uploader Code] ,[Vlc Code] , XXXXFinal.[Doc Date],[Milk Type]
                                 Union All
                                 select 2 as SNo  , '' as [Plant Code] , '' as [Plant Name] , '' as [MCC Code] , '' as   [MCC Name],'' as [Vlc Uploader Code] ,'' as [Vlc Code] , '' as [VLC Name] ,'' as [Doc Date] ,[Milk Type] + ' Total' as [Milk Type] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] , sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] ,isnull ( cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull (cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)], isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull (cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)],isnull (cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by    [Milk Type]
                                 Union All
                                 select 3 as SNo  , '' as [Plant Code] , ''   as [Plant Name] , '' as [MCC Code] , '' as   [MCC Name], '' as [Vlc Uploader Code] ,'' as [Vlc Code] , ''  as [VLC Name],'' as [Doc Date] ,'Grand Total' as [Milk Type], sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] , sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] ,isnull (  cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)], isnull ( cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code]  
                                 ) XXXXXFinal order by SNo, [Milk Type] asc  
                                 "






            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True

                dt.Columns.Add("AddDash", GetType(String))
                dt.Columns.Add("S.No.", GetType(String))
                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Milk Type")), "Grand Total") = CompairStringResult.Equal Then
                        dt.Rows(i)("AddDash") = "1"
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Milk Type")), "BM Total") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Milk Type")), "CM Total") = CompairStringResult.Equal Then
                    Else
                        dt.Rows(i)("S.No.") = clsCommon.myCstr(i + 1) + "."
                    End If
                Next
                gv1.DataSource = dt
                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If
                'If gv1.Columns.Contains("MCC_Code") = True Then
                '    gv1.Columns("MCC_Code").HeaderText = "Location Code"
                'End If
                'If gv1.Columns.Contains("MCC_Name") = True Then
                '    gv1.Columns("MCC_Name").HeaderText = "Location Name"
                'End If

                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then
                    'Dim strshift As String = ""
                    If rdbMainPlant.Checked = True Then
                        strshift = "MORNING"
                    ElseIf rdbOther.Checked = True Then
                        strshift = "EVENING"

                    End If

                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = ""
                    obj.ReportName1 = "CHECK LIST FOR MILK PROCUREMENT PARTICULARS DATED: " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text) + "   " + strShift + " UserLogin: " + objCommonVar.CurrentUserCode.ToUpper() + " "
                    obj.ShowPageNo = True
                    obj.LandscapPageSetupColumnsChar = 120

                    obj.arrMergeColumn = New List(Of clsDosPrintMergeColumn)()
                    Dim objMergeColumn As clsDosPrintMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Good Milk"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Good Milk Qty(KGS)")
                    objMergeColumn.arrColumn.Add("Good Milk FAT(%)")
                    objMergeColumn.arrColumn.Add("Good Milk SNF(%)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Sour"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Sour Qty(LTR)")
                    objMergeColumn.arrColumn.Add("Sour FAT(%)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "CURD"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Curd Qty(LTR)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "PTC RECVRY"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("PTC RECVRY")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT CODE", clsCommon.myCstr(fndSingleMCCCode.Value)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("DATE", "DATE", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("S.No.", "SNO", False, DosPrintAlignment.Left, 6, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Vlc Uploader Code", "MCC/CODE", False, DosPrintAlignment.Left, 8, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VLC Name", "CENTER NAME", False, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Milk Type", "TYPE MLK", False, DosPrintAlignment.Center, 12, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk Qty(KGS)", " Qty(KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk FAT(%)", " FAT%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF(%)", " SNF%", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour Qty(LTR)", " Qty(LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour FAT(%)", "   FAT(%)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Curd Qty(LTR)", " (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", " (KGS)", False, DosPrintAlignment.Right, 12, False, DecimalPlaces.Two))
                    obj.Print(obj, dt, PageSetup.Landscap, "AddDash", "1")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UnitWiseTotal(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            If clsCommon.myLen(fndSingleMCCCode.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select MCC First.", Me.Text)
                Return
            End If
            Dim strShift As String = ""

            Dim BaseQuery As String = MCCMilkRegisterQueryWithRejection(fndLoc.Value, txtMilkReceiptFromDate.Value, txtMilkReciptToDate.Value, True, True, fndSingleMCCCode.Value)


            Dim qry As String = " Select SNo, [Plant Code],[Plant Name] , [Shift], case when SNo =1 then  FORMAT ( convert (date, [Doc Date],103), 'dd/MM') else '' end as [Doc Date],  [Milk Type],  [Good Milk Qty(LTR)] ,  [Good Milk Qty(KGS)] , [Good Milk FAT(%)],  [Good Milk SNF(%)] , [Good Milk FAT KG], [Good Milk SNF KG] 
                                 ,[Sour Qty(KG)],  [Sour Qty(LTR)], [Sour FAT(%)],[Sour SNF(%)], [Sour FAT KG], [Sour SNF KG],  [Total(LTR)] ,  [Total(KGS)]
                                 , [Curd Qty(KG)],  [Curd Qty(LTR)], [Curd FAT(%)],[Curd SNF(%)], [Curd FAT KG], [Curd SNF KG] ,  [PTC RECVRY] from (

                                 select 1 as SNo  , XXXXFinal.[Plant Code] , max( XXXXFinal.[Plant Name]) as [Plant Name] ,case when  shift = 'Morning' then 'AM' else 'PM' end as [Shift],[Doc Date],[Doc Date] as [Doc Date2],[Milk Type], sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] , isnull ( cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull (cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)], isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)],isnull (cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY] From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code]  , XXXXFinal.[Doc Date],Shift,[Milk Type]
                                 Union All
                                 select 2 as SNo  , '' as [Plant Code] , '' as [Plant Name] , '' as [Shift], [Doc Date] as   [Doc Date],[Doc Date] as [Doc Date2] ,'Total' as [Milk Type] , sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] ,isnull ( cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull (cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)], isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull (cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)],isnull (cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by [Doc Date]
                                 Union All
                                 select 3 as SNo  , '' as [Plant Code] , ''   as [Plant Name] , '' as [Shift], max([Doc Date]) as [Doc Date],max([Doc Date]) as [Doc Date2] ,(case when [Shift]='Morning'	then 'AM' else 'PM'	end) + ' - ' +  [Milk Type]+' Tot' as [Milk Type], sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] ,isnull (  cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)], isnull ( cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code],[Milk Type],[Shift]  

                                 Union All
                                 select 4 as SNo  , '' as [Plant Code] , ''   as [Plant Name] , '' as [Shift], max([Doc Date]) as [Doc Date],max([Doc Date]) as [Doc Date2] ,'GrandTOT' as [Milk Type], sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] ,isnull (  cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)], isnull ( cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code] 

                                 ) XXXXXFinal order by [Doc Date2], SNo,Shift, [Milk Type] asc  
                                 "






            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True
                Dim Tablerowcount As Integer = dt.Rows.Count - 1
                Dim dr As DataRow() = dt.Select("[Milk Type]='Total'")
                If dr IsNot Nothing AndAlso dr.Length > 0 Then
                    Tablerowcount = Tablerowcount + dr.Length
                End If
                dt.Columns.Add("AddDash", GetType(String))
                For i As Integer = 0 To Tablerowcount
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Milk Type")), "Total") = CompairStringResult.Equal Then
                        dt.Rows(i)("AddDash") = "1"
                        Dim ROW As DataRow = dt.NewRow()
                        ROW("AddDash") = "1"
                        dt.Rows.InsertAt(ROW, (i + 1))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Milk Type")), "GrandTOT") = CompairStringResult.Equal Then
                        dt.Rows(i)("AddDash") = "1"
                    End If
                Next

                gv1.DataSource = dt
                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If
                'If gv1.Columns.Contains("MCC_Code") = True Then
                '    gv1.Columns("MCC_Code").HeaderText = "Location Code"
                'End If
                'If gv1.Columns.Contains("MCC_Name") = True Then
                '    gv1.Columns("MCC_Name").HeaderText = "Location Name"
                'End If

                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then
                    'Dim strshift As String = ""

                    strShift = "MORNING AND EVENING"

                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = ""
                    obj.ReportName1 = "STATEMENT OF UNIT WISE TOTAL LTS,KGS"   '"CHECK LIST FOR MILK PROCUREMENT PARTICULARS DATED: " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + "   " + strShift + " UserLogin: " + objCommonVar.CurrentUserCode.ToUpper() + " "
                    obj.ShowPageNo = True
                    obj.LandscapPageSetupColumnsChar = 120

                    obj.arrMergeColumn = New List(Of clsDosPrintMergeColumn)()
                    Dim objMergeColumn As clsDosPrintMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Good Milk"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Good Milk Qty(LTR)")
                    objMergeColumn.arrColumn.Add("Good Milk Qty(KGS)")
                    objMergeColumn.arrColumn.Add("Good Milk FAT(%)")
                    objMergeColumn.arrColumn.Add("Good Milk SNF(%)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Sour"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Sour Qty(LTR)")
                    objMergeColumn.arrColumn.Add("Sour Qty(KG)")
                    objMergeColumn.arrColumn.Add("Sour FAT(%)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Total"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Total(LTR)")
                    objMergeColumn.arrColumn.Add("Total(KGS)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "CURD"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Curd Qty(LTR)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "PTC RECVRY"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("PTC RECVRY")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT CODE", clsCommon.myCstr(fndSingleMCCCode.Value)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TRANSATION DATE", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("COLLECTIONS", strShift))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("DOC DATE", "DATE", False, DosPrintAlignment.Left, 6, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Shift", "DAY", False, DosPrintAlignment.Left, 5, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Milk Type", "Type", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk Qty(LTR)", "  (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk Qty(KGS)", "  (KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk FAT(%)", "  FAT%", False, DosPrintAlignment.Right, 8, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF(%)", "  SNF%", False, DosPrintAlignment.Right, 8, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour Qty(LTR)", "  (LTR)", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour Qty(KG)", "  (KGS)", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour FAT(%)", " FAT(%)", False, DosPrintAlignment.Right, 8, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(LTR)", "  (LTR)", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(KGS)", "  (KGS)", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Curd Qty(LTR)", " (LTR)", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", " (KGS)", False, DosPrintAlignment.Right, 12, False, DecimalPlaces.Two))
                    obj.Print(obj, dt, PageSetup.Landscap, "AddDash", "1")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Public Sub UnitwiseAnalysis(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            If clsCommon.myLen(fndSingleMCCCode.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select MCC First.", Me.Text)
                Return
            End If
            Dim strShift As String = ""

            Dim BaseQuery As String = MCCMilkRegisterQueryWithRejection(fndLoc.Value, txtMilkReceiptFromDate.Value, txtMilkReciptToDate.Value, True, True, fndSingleMCCCode.Value)


            Dim qry As String = " Select SNo, [Plant Code],[Plant Name] , [Shift], case when SNo =1 then  FORMAT ( convert (date, [Doc Date],103), 'dd/MM') else '' end as [Doc Date],  [Milk Type],  [Good Milk Qty(LTR)] ,  [Good Milk Qty(KGS)] , [Good Milk FAT(%)],  [Good Milk SNF(%)] , [Good Milk FAT KG], [Good Milk SNF KG] 
                                 ,[Sour Qty(KG)],  [Sour Qty(LTR)], [Sour FAT(%)],[Sour SNF(%)], [Sour FAT KG], [Sour SNF KG],  [Total(LTR)] ,  [Total(KGS)],  [Total FAT KG]
                                 , [Curd Qty(KG)],  [Curd Qty(LTR)], [Curd FAT(%)],[Curd SNF(%)], [Curd FAT KG], [Curd SNF KG] ,  [PTC RECVRY] from (

                                 select 1 as SNo  , XXXXFinal.[Plant Code] , max( XXXXFinal.[Plant Name]) as [Plant Name] ,case when  shift = 'Morning' then 'AM' else 'PM' end as [Shift],[Doc Date],[Doc Date] as [Doc Date2],[Milk Type], sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] , isnull ( cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull (cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)], sum ([Good Milk FAT KG]) + sum([Sour FAT KG]) as [Total FAT KG]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)], isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)],isnull (cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY] From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code]  , XXXXFinal.[Doc Date],Shift,[Milk Type]
                                 Union All
                                 select 2 as SNo  , '' as [Plant Code] , '' as [Plant Name] , '' as [Shift], [Doc Date] as   [Doc Date],[Doc Date] as [Doc Date2] ,'Total' as [Milk Type] , sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] ,isnull ( cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull (cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)], isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)],  sum ([Good Milk FAT KG]) + sum([Sour FAT KG]) as [Total FAT KG]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull (cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)],isnull (cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by [Doc Date]
                                 Union All
                                 select 3 as SNo  , '' as [Plant Code] , ''   as [Plant Name] , '' as [Shift], max([Doc Date]) as [Doc Date],max([Doc Date]) as [Doc Date2] ,(case when [Shift]='Morning'	then 'AM' else 'PM'	end) + ' - ' +  [Milk Type]+' Tot' as [Milk Type], sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] ,isnull (  cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)],  sum ([Good Milk FAT KG]) + sum([Sour FAT KG]) as [Total FAT KG]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)], isnull ( cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code],[Milk Type],[Shift]  

                                 Union All
                                 select 4 as SNo  , '' as [Plant Code] , ''   as [Plant Name] , '' as [Shift], max([Doc Date]) as [Doc Date],max([Doc Date]) as [Doc Date2] ,'Grand Total' as [Milk Type], sum ( [Good Milk Qty(LTR)]) as [Good Milk Qty(LTR)] , sum ( [Good Milk Qty(KGS)] ) as [Good Milk Qty(KGS)] ,isnull (  cast (  (sum ([Good Milk FAT KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0) as [Good Milk FAT(%)], isnull ( cast (  (  sum ([Good Milk SNF KG]) * 100 /  nullif (sum ( [Good Milk Qty(KGS)] ),0) ) as Decimal(18,2) ),0)  as [Good Milk SNF(%)] ,sum ([Good Milk FAT KG]) as [Good Milk FAT KG],sum ([Good Milk SNF KG]) as [Good Milk SNF KG] 
                                 ,sum ([Sour Qty(KG)]) as [Sour Qty(KG)], sum ([Sour Qty(LTR)]) as [Sour Qty(LTR)],isnull ( cast (  ( sum([Sour FAT KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour FAT(%)],isnull ( cast (  ( sum([Sour SNF KG]) * 100 / nullif (sum ([Sour Qty(KG)]),0)) as Decimal(18,2) ),0) as [Sour SNF(%)],sum([Sour FAT KG]) as [Sour FAT KG],sum([Sour SNF KG]) as [Sour SNF KG], sum ( [Good Milk Qty(LTR)])  +  sum ([Sour Qty(LTR)])  as [Total(LTR)] , sum ( [Good Milk Qty(KGS)])  +  sum ([Sour Qty(KG)])  as [Total(KGS)], sum ([Good Milk FAT KG]) + sum([Sour FAT KG]) as [Total FAT KG]
                                 ,sum ([Curd Qty(KG)]) as [Curd Qty(KG)], sum ([Curd Qty(LTR)]) as [Curd Qty(LTR)],isnull ( cast (  ( sum([Curd FAT KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0)  as [Curd FAT(%)], isnull ( cast (  ( sum([Curd SNF KG]) * 100 / nullif (sum ([Curd Qty(KG)]),0)) as Decimal(18,2) ),0) as [Curd SNF(%)],Sum([Curd FAT KG]) as [Curd FAT KG],Sum([Curd SNF KG]) as [Curd SNF KG] , sum ([PTC RECVRY]) as [PTC RECVRY]  From ( " + BaseQuery + " ) XXXXFinal group by XXXXFinal.[Plant Code] 

                                 ) XXXXXFinal order by [Doc Date2], SNo,Shift, [Milk Type] asc  
                                 "






            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then



                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True

                Dim Tablerowcount As Integer = dt.Rows.Count - 1
                Dim dr As DataRow() = dt.Select("[Milk Type]='Total'")
                If dr IsNot Nothing AndAlso dr.Length > 0 Then
                    Tablerowcount = Tablerowcount + dr.Length
                End If
                dt.Columns.Add("AddDash", GetType(String))
                For i As Integer = 0 To Tablerowcount
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Milk Type")), "Total") = CompairStringResult.Equal Then
                        dt.Rows(i)("AddDash") = "1"
                        Dim ROW As DataRow = dt.NewRow()
                        ROW("AddDash") = "1"
                        dt.Rows.InsertAt(ROW, (i + 1))
                    ElseIf clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Milk Type")), "Grand Total") = CompairStringResult.Equal Then
                        dt.Rows(i)("AddDash") = "1"
                    End If
                Next

                gv1.DataSource = dt


                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If
                'If gv1.Columns.Contains("MCC_Code") = True Then
                '    gv1.Columns("MCC_Code").HeaderText = "Location Code"
                'End If
                'If gv1.Columns.Contains("MCC_Name") = True Then
                '    gv1.Columns("MCC_Name").HeaderText = "Location Name"
                'End If

                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then
                    'Dim strshift As String = ""

                    strShift = "MORNING AND EVENING"

                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = ""
                    obj.ReportName1 = "STATEMENT OF UNIT WISE ANALYSIS FOR LTS,KGS,KGFAT,KGSNF AND AVRAGE FAT ,AVRAGE SNF"   '"CHECK LIST FOR MILK PROCUREMENT PARTICULARS DATED: " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + "   " + strShift + " UserLogin: " + objCommonVar.CurrentUserCode.ToUpper() + " "
                    obj.ShowPageNo = True
                    obj.LandscapPageSetupColumnsChar = 180

                    obj.arrMergeColumn = New List(Of clsDosPrintMergeColumn)()
                    Dim objMergeColumn As clsDosPrintMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Good Milk"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Good Milk Qty(LTR)")
                    objMergeColumn.arrColumn.Add("Good Milk Qty(KGS)")
                    objMergeColumn.arrColumn.Add("Good Milk FAT(%)")
                    objMergeColumn.arrColumn.Add("Good Milk SNF(%)")
                    objMergeColumn.arrColumn.Add("Good Milk FAT KG")
                    objMergeColumn.arrColumn.Add("Good Milk SNF KG")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Sour"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Sour Qty(LTR)")
                    objMergeColumn.arrColumn.Add("Sour Qty(KG)")
                    objMergeColumn.arrColumn.Add("Sour FAT(%)")
                    objMergeColumn.arrColumn.Add("Sour FAT KG")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "PTC RECVRY"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("PTC RECVRY")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "Total"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Total(LTR)")
                    objMergeColumn.arrColumn.Add("Total(KGS)")
                    objMergeColumn.arrColumn.Add("Total FAT KG")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    objMergeColumn = New clsDosPrintMergeColumn()
                    objMergeColumn.MergeText = "CURD"
                    objMergeColumn.arrColumn = New List(Of String)()
                    objMergeColumn.arrColumn.Add("Curd Qty(LTR)")
                    obj.arrMergeColumn.Add(objMergeColumn)

                    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT CODE", clsCommon.myCstr(fndSingleMCCCode.Value)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TRANSATION DATE", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("COLLECTIONS", strShift))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("DOC DATE", "DATE", False, DosPrintAlignment.Left, 9, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Shift", "DAY", False, DosPrintAlignment.Left, 8, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Milk Type", "Type", False, DosPrintAlignment.Right, 11, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk Qty(LTR)", "  (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk Qty(KGS)", "  (KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk FAT(%)", "  FAT%", False, DosPrintAlignment.Right, 8, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF(%)", "  SNF%", False, DosPrintAlignment.Right, 8, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk FAT KG", "  KGFAT", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF KG", "  KGSNF", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))


                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour Qty(LTR)", "  (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour Qty(KG)", "  (KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour FAT(%)", " FAT(%)", False, DosPrintAlignment.Right, 8, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Sour FAT KG", "  KGFAT", False, DosPrintAlignment.Right, 9, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", "  (KGS)", False, DosPrintAlignment.Right, 12, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(LTR)", "  (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(KGS)", "  (KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total FAT KG", "  KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", "      PTC RECVRY", False, DosPrintAlignment.Right, 11, False, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF KG", "    Total KGSNF", False, DosPrintAlignment.Right, 11, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Curd Qty(LTR)", " (LTR)", False, DosPrintAlignment.Right, 11, False, DecimalPlaces.Two))

                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", "        PTC RECVRY", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.Print(obj, dt, PageSetup.Landscap, "AddDash", "1")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub RouteBillsAbstract(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            If clsCommon.myLen(fndSingleMCCCode.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select MCC First.", Me.Text)
                Return
            End If


            Dim BaseQuery As String = " Select TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VENDOR_MASTER.Vendor_Name as [VSP Name],TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MILK_SRN_DETAIL.AMOUNT as AMOUNT, TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR as Qty ,TSPL_MILK_SRN_DETAIL .ACC_Qty as [Qty(KGS)],TSPL_MILK_SRN_DETAIL.FAT_KG , TSPL_MILK_SRN_DETAIL.SNF_KG  ,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type  As [Milk Type] , Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ) as VSP_Day_Wise_Incentive  ,isnull(TSPL_MILK_SRN_DETAIL.TIP_Amount,0) as TIP_Amount 
                                        From TSPL_MILK_SRN_DETAIL   
Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  
Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE   
Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  
Left Outer Join TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_DOC_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE   and   TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE   and TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_Item_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code   
Left Outer Join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Code 
Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  
left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code    
                                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_MILK_SRN_DETAIL.Item_Code
                                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_MILK_SRN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_MILK_SRN_DETAIL.UOM_Code = Stocking_Conversion_Factor.UOM_Code  
                                        left outer join  TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.MCC = TSPL_MILK_SRN_HEAD.MCC_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE and TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_MILK_SRN_HEAD.VSP_CODE and TSPL_VLC_MASTER_HEAD.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                                        left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                                        where 2=2  and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) >=  convert(date,'" + txtMilkReceiptFromDate.Value + "',103)  and  convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= convert(date,'" + txtMilkReciptToDate.Value + "',103)  and tspl_location_master.location_Code ='" + fndLoc.Value + "'  " + IIf(MultipleFinderFillAuto = True, " ", " and TSPL_MILK_SRN_HEAD.MCC_CODE = '" + fndSingleMCCCode.Value + "' ") + "  "
            Dim CTEQuery As String = " WITH Data_CTE 
                                   AS ( 
                                   Select distinct  MCC,  [MCC Name],VLC_CODE,VLC_Name,VLC_Code_VLC_Uploader,VSP_CODE,[VSP Name], TBL_MilkType.MilkType from (
                                   " + BaseQuery + "
                                   ) as Final 
                                   left outer join (Select '" + fndSingleMCCCode.Value + "' as  MCC_code, 'B' as  MilkType union  Select '" + fndSingleMCCCode.Value + "' as  MCC_code,  'C' as MilkType  union  Select '" + fndSingleMCCCode.Value + "' as  MCC_code,  'E' as MilkType ) as TBL_MilkType on TBL_MilkType.MCC_code = Final.MCC
                                   ) "
            Dim qryMain As String = "   select XXXXFinal.*, isnull (TBL_DEDUCTION_DETAILS_First.DeductionCode,0) as ColFirst_DeductionCode, isnull (TBL_DEDUCTION_DETAILS_First.DeductionMilkTypeRow,0) as ColFirst_DeductionMilkTypeRow , isnull(TBL_DEDUCTION_DETAILS_First.DeductionAmt,0) as ColFirst_DeductionAmt,
                                    isnull (TBL_DEDUCTION_DETAILS_Second.DeductionCode,0) as ColSecond_DeductionCode, isnull ( TBL_DEDUCTION_DETAILS_Second.DeductionMilkTypeRow,0) as ColSecond_DeductionMilkTypeRow , isnull ( TBL_DEDUCTION_DETAILS_Second.DeductionAmt,0) as ColSecond_DeductionAmt,
                                    isnull (TBL_DEDUCTION_DETAILS_Third.DeductionCode,0) as ColThird_DeductionCode, isnull (TBL_DEDUCTION_DETAILS_Third.DeductionMilkTypeRow,0) as ColThird_DeductionMilkTypeRow , isnull (TBL_DEDUCTION_DETAILS_Third.DeductionAmt,0) as ColThird_DeductionAmt,
                                    TBL_TOTAL_DEDUCTION.DeductionMilkTypeRow, isnull (TBL_TOTAL_DEDUCTION.DeductionAmt,0)   as TotalDeductionAmount
                                    , case when MilkType in ('C','D') then 0 else  sum(  case when milktype ='D' then  [Cartage/Others/GrossAmt] else 0 end - case when XXXXFinal.MilkType = 'B' then  (isnull (TBL_TOTAL_DEDUCTION.DeductionAmt,0)) else 0 end) over (PARTITION BY VLC_Code,VSP_Code) end  as NetAmount 
                                    from ( 
                                    select '1' as SNo, ROW_NUMBER() OVER (PARTITION BY Data_CTE.MCC, Data_CTE.VLC_Code, Data_CTE.VSP_Code,Data_CTE.VLC_Code_VLC_Uploader ORDER BY Data_CTE.VLC_CODE,Data_CTE.VSP_CODE , Data_CTE.VLC_Code_VLC_Uploader,Data_CTE.MilkType) as RowNo,Data_CTE.MCC,Data_CTE.[MCC Name],Data_CTE.VLC_CODE,Data_CTE.VLC_Name,Data_CTE.VLC_Code_VLC_Uploader,Data_CTE.VSP_CODE,Data_CTE.[VSP Name],Data_CTE.MilkType , case when Data_CTE.MilkType = 'B' then Data_CTE.VLC_Name  when Data_CTE.MilkType = 'C'  then Data_CTE.[VSP Name]  else '' end as Print_VLC_NAMEAndVSPName ,case when Data_CTE.MilkType = 'B' then Data_CTE.VLC_Code_VLC_Uploader else ''  end  Print_VLC_Code_VLC_Uploader ,isnull (TBL_Main_Data.AMOUNT,0) as AMOUNT , 0 as  [Cartage/Others/GrossAmt] ,isnull (TBL_Main_Data.Qty,0) as Qty, isnull (TBL_Main_Data.[Qty(KGS)],0) as [Qty(KGS)],isnull(TBL_Main_Data.FAT_KG,0) as FAT_KG,isnull (TBL_Main_Data.SNF_KG,0) as SNF_KG,isnull (TBL_Main_Data.VSP_Day_Wise_Incentive,0) as VSP_Day_Wise_Incentive, isnull (TBL_Main_Data.TIP_Amount,0) as TIP_Amount  from  Data_CTE  
                                    left outer join (
                                    select XXXFinal.MCC , max( XXXFinal.[MCC Name]) as [MCC Name] , XXXFinal.VLC_CODE, max( XXXFinal.VLC_Name) as VLC_Name , XXXFinal.VLC_Code_VLC_Uploader ,XXXFinal.VSP_CODE ,max (XXXFinal.[VSP Name]) as  [VSP Name] ,sum(XXXFinal.AMOUNT) as AMOUNT,sum (XXXFinal.Qty) as Qty,sum([Qty(KGS)]) as [Qty(KGS)],sum(FAT_KG) as FAT_KG, sum (SNF_KG) as SNF_KG,XXXFinal.[Milk Type], sum (XXXFinal.VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive , sum ( XXXFinal.TIP_Amount) as TIP_Amount from (
                                    " + BaseQuery + "
                                    )  XXXFinal 	group by 	XXXFinal.MCC, 	XXXFinal.VLC_CODE, XXXFinal.VLC_Code_VLC_Uploader , XXXFinal.VSP_CODE , [Milk Type]
                                    ) TBL_Main_Data on TBL_Main_Data.MCC = Data_CTE.MCC and TBL_Main_Data.VLC_CODE = Data_CTE.VLC_CODE and TBL_Main_Data.VSP_CODE = Data_CTE.VSP_CODE and TBL_Main_Data.[Milk Type] = Data_CTE.MilkType
                                    Union all
                                    select '2' as SNo, '3' as RowNo , '' as  MCC, '' as [MCC Name], VLC_CODE, '' as VLC_Name, '' as VLC_Code_VLC_Uploader, VSP_CODE , '' as  [VSP Name], 'D' as MilkType, '' as Print_VLC_NAMEAndVSPName , '' as Print_VLC_Code_VLC_Uploader , Convert (decimal(18,2), isnull (sum (VSP_Day_Wise_Incentive),0))  as AMOUNT , convert (decimal(18,2) ,sum(AMOUNT))  + Convert (decimal(18,2), isnull (sum (VSP_Day_Wise_Incentive),0)) as [Cartage/Others/GrossAmt], sum( Qty ) as  Qty, sum ([Qty(KGS)]) as [Qty(KGS)] ,sum( FAT_KG) as FAT_KG ,sum (SNF_KG) as  SNF_KG,sum (VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive, sum (TIP_Amount) as TIP_Amount from ( 
                                    select ROW_NUMBER() OVER (PARTITION BY Data_CTE.MCC, Data_CTE.VLC_Code, Data_CTE.VSP_Code,Data_CTE.VLC_Code_VLC_Uploader ORDER BY Data_CTE.VLC_CODE,Data_CTE.VSP_CODE , Data_CTE.VLC_Code_VLC_Uploader,Data_CTE.MilkType) as RowNo, Data_CTE.MCC,Data_CTE.[MCC Name],Data_CTE.VLC_CODE,Data_CTE.VLC_Name,Data_CTE.VLC_Code_VLC_Uploader,Data_CTE.VSP_CODE,Data_CTE.[VSP Name],Data_CTE.MilkType , case when Data_CTE.MilkType = 'B' then Data_CTE.VLC_Name  when Data_CTE.MilkType = 'C'  then Data_CTE.[VSP Name]  else '' end as Print_VLC_NAMEAndVSPName ,case when Data_CTE.MilkType = 'B' then Data_CTE.VLC_Code_VLC_Uploader else ''  end  Print_VLC_Code_VLC_Uploader ,isnull (TBL_Main_Data.AMOUNT,0) as AMOUNT , isnull (TBL_Main_Data.Qty,0) as Qty, isnull (TBL_Main_Data.[Qty(KGS)],0) as [Qty(KGS)],isnull(TBL_Main_Data.FAT_KG,0) as FAT_KG,isnull (TBL_Main_Data.SNF_KG,0) as SNF_KG,isnull (TBL_Main_Data.VSP_Day_Wise_Incentive,0) as VSP_Day_Wise_Incentive, isnull (TBL_Main_Data.TIP_Amount,0) as TIP_Amount  from  Data_CTE  left outer join 
                                    (  
                                    select XXXFinal.MCC , max( XXXFinal.[MCC Name]) as [MCC Name] , XXXFinal.VLC_CODE, max( XXXFinal.VLC_Name) as VLC_Name , XXXFinal.VLC_Code_VLC_Uploader ,XXXFinal.VSP_CODE ,max (XXXFinal.[VSP Name]) as  [VSP Name] ,sum(XXXFinal.AMOUNT) as AMOUNT,sum (XXXFinal.Qty) as Qty,sum([Qty(KGS)]) as [Qty(KGS)],sum(FAT_KG) as FAT_KG, sum (SNF_KG) as SNF_KG,XXXFinal.[Milk Type], sum (XXXFinal.VSP_Day_Wise_Incentive) as VSP_Day_Wise_Incentive , sum ( XXXFinal.TIP_Amount) as TIP_Amount from (
                                    " + BaseQuery + "
                                    )XXXFinal 	group by 	XXXFinal.MCC, 	XXXFinal.VLC_CODE, XXXFinal.VLC_Code_VLC_Uploader , XXXFinal.VSP_CODE , [Milk Type]  
                                    ) TBL_Main_Data on TBL_Main_Data.MCC = Data_CTE.MCC and TBL_Main_Data.VLC_CODE = Data_CTE.VLC_CODE and TBL_Main_Data.VSP_CODE = Data_CTE.VSP_CODE and TBL_Main_Data.[Milk Type] = Data_CTE.MilkType
                                    ) XXXXTotal group by VLC_CODE , VSP_CODE
                                    ) XXXXFinal
                                    
                                    left outer join (
                                    select TBL_Deduction_Header.DeductionSNO,TBL_Deduction_Header.DeductionMilkTypeRow,FinalDeduction.* from (
                                    select TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,max(TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) as Deduction_Desc,sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as DeductionAmt  from TSPL_VENDOR_INVOICE_DETAIL   left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no  INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code  
                                    where document_type='D' and isDeduction=1 and Posting_Date is not null  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" + txtMilkReceiptFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + txtMilkReciptToDate.Value + "',103) " + IIf(MultipleFinderFillAuto = True, " ", " and TSPL_VENDOR_INVOICE_HEAD.MCC_Code ='" + fndSingleMCCCode.Value + "'  ") + " 
                                    and TSPL_VENDOR_INVOICE_DETAIL.DeductionCode in ('CESS ON S DED', 'FEED DED','M.SPARES DED','M.TESTER DED')
                                    group by TSPL_VENDOR_INVOICE_HEAD.MCC_Code,Vendor_Code,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
                                    ) FinalDeduction  
                                    left outer join (Select  1 as DeductionSNO , 'B' as DeductionMilkTypeRow ,  'CESS ON S DED' as DeductionCode
                                    union all
                                    Select  2 as DeductionSNO , 'C' as DeductionMilkTypeRow ,  'FEED DED' as DeductionCode
                                    union all
                                    Select  3 as DeductionSNO , 'D' as DeductionMilkTypeRow ,  'M.SPARES DED' as DeductionCode
                                    union all
                                    Select  4 as DeductionSNO , 'E' as DeductionMilkTypeRow ,  'M.TESTER DED' as DeductionCode
                                    ) as TBL_Deduction_Header on TBL_Deduction_Header.DeductionCode = FinalDeduction.DeductionCode
                                    ) as TBL_DEDUCTION_DETAILS_First on TBL_DEDUCTION_DETAILS_First.MCC_CODE  = XXXXFinal.MCC  and TBL_DEDUCTION_DETAILS_First.Vendor_Code =XXXXFinal.VSP_CODE and TBL_DEDUCTION_DETAILS_First.DeductionMilkTypeRow = XXXXFinal.MilkType

                                    left outer join (
                                    select TBL_Deduction_Header.DeductionSNO,TBL_Deduction_Header.DeductionMilkTypeRow,FinalDeduction.* from (
                                    select TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,max(TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) as Deduction_Desc,sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as DeductionAmt  from TSPL_VENDOR_INVOICE_DETAIL   left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no  INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code  
                                    where document_type='D' and isDeduction=1 and Posting_Date is not null  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" + txtMilkReceiptFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + txtMilkReciptToDate.Value + "',103) " + IIf(MultipleFinderFillAuto = True, " ", " and TSPL_VENDOR_INVOICE_HEAD.MCC_Code ='" + fndSingleMCCCode.Value + "'  ") + " 
                                    and TSPL_VENDOR_INVOICE_DETAIL.DeductionCode in ('OTHER DED', 'SEED DED','STATIONAR DED','STORE(A) DED')
                                    group by TSPL_VENDOR_INVOICE_HEAD.MCC_Code,Vendor_Code,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
                                    ) FinalDeduction 
                                    left outer join (
                                    Select  1 as DeductionSNO , 'B' as DeductionMilkTypeRow ,  'OTHER DED' as DeductionCode
                                    union all
                                    Select  2 as DeductionSNO , 'C' as DeductionMilkTypeRow ,  'SEED DED' as DeductionCode
                                    union all
                                    Select  3 as DeductionSNO , 'D' as DeductionMilkTypeRow ,  'STATIONAR DED' as DeductionCode
                                    union all
                                    Select  4 as DeductionSNO , 'E' as DeductionMilkTypeRow ,  'STORE(A) DED' as DeductionCode
                                    ) as TBL_Deduction_Header on TBL_Deduction_Header.DeductionCode = FinalDeduction.DeductionCode
                                    ) as TBL_DEDUCTION_DETAILS_Second on TBL_DEDUCTION_DETAILS_Second.MCC_CODE  = XXXXFinal.MCC  and TBL_DEDUCTION_DETAILS_Second.Vendor_Code =XXXXFinal.VSP_CODE and TBL_DEDUCTION_DETAILS_Second.DeductionMilkTypeRow = XXXXFinal.MilkType

                                    left outer join (
                                    select TBL_Deduction_Header.DeductionSNO,TBL_Deduction_Header.DeductionMilkTypeRow,FinalDeduction.* from (
                                    select TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode,max(TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) as Deduction_Desc,sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as DeductionAmt  from TSPL_VENDOR_INVOICE_DETAIL   left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no  INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code  
                                    where document_type='D' and isDeduction=1 and Posting_Date is not null  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" + txtMilkReceiptFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + txtMilkReciptToDate.Value + "',103) " + IIf(MultipleFinderFillAuto = True, " ", " and TSPL_VENDOR_INVOICE_HEAD.MCC_Code ='" + fndSingleMCCCode.Value + "'  ") + " 
                                    and TSPL_VENDOR_INVOICE_DETAIL.DeductionCode in ('STORE(T) DED', 'VACCINE DED','VIJY LN DED','VIJY RD DED')
                                    group by TSPL_VENDOR_INVOICE_HEAD.MCC_Code,Vendor_Code,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode 
                                    ) FinalDeduction 
                                    left outer join (
                                    Select  1 as DeductionSNO , 'B' as DeductionMilkTypeRow ,  'STORE(T) DED' as DeductionCode
                                    union all
                                    Select  2 as DeductionSNO , 'C' as DeductionMilkTypeRow ,  'VACCINE DED' as DeductionCode
                                    union all
                                    Select  3 as DeductionSNO , 'D' as DeductionMilkTypeRow ,  'VIJY LN DED' as DeductionCode
                                    union all
                                    Select  4 as DeductionSNO , 'E' as DeductionMilkTypeRow ,  'VIJY RD DED' as DeductionCode) as TBL_Deduction_Header on TBL_Deduction_Header.DeductionCode = FinalDeduction.DeductionCode
                                    ) as TBL_DEDUCTION_DETAILS_Third on TBL_DEDUCTION_DETAILS_Third.MCC_CODE  = XXXXFinal.MCC  and TBL_DEDUCTION_DETAILS_Third.Vendor_Code =XXXXFinal.VSP_CODE and TBL_DEDUCTION_DETAILS_Third.DeductionMilkTypeRow = XXXXFinal.MilkType 

                                    left outer join  (
                                    select TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,'B' as DeductionMilkTypeRow,sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as DeductionAmt  from TSPL_VENDOR_INVOICE_DETAIL   left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no  INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code  
                                    where document_type='D' and isDeduction=1 and Posting_Date is not null  and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" + txtMilkReceiptFromDate.Value + "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" + txtMilkReciptToDate.Value + "',103) " + IIf(MultipleFinderFillAuto = True, "", " and TSPL_VENDOR_INVOICE_HEAD.MCC_Code ='" + fndSingleMCCCode.Value + "'  ") + " 
                                    and TSPL_VENDOR_INVOICE_DETAIL.DeductionCode in ('CESS ON S DED' ,'FEED DED','M.SPARES DED','M.TESTER DED','OTHER DED','SEED DED' ,'STATIONAR DED','STORE(A) DED', 'STORE(T) DED' ,'VACCINE DED','VIJY LN DED','VIJY RD DED')
                                    group by TSPL_VENDOR_INVOICE_HEAD.MCC_Code,Vendor_Code) as TBL_TOTAL_DEDUCTION on TBL_TOTAL_DEDUCTION.MCC_Code = XXXXFinal.MCC and TBL_TOTAL_DEDUCTION.Vendor_Code = XXXXFinal.VSP_CODE and TBL_TOTAL_DEDUCTION.DeductionMilkTypeRow = XXXXFinal.MilkType

                                     

                                "

            Dim qryFooterTotal As String = "  Union All select  3 as SNo  , 5 as  RowNo  ,  '' as MCC,  '' as [MCC Name], 'ZZZZ' as VLC_CODE , '' as VLC_Name, '' as VLC_Code_VLC_Uploader, 'ZZZZ' as VSP_CODE,'' as [VSP Name],  PPPP.MilkType , '' as Print_VLC_NAMEAndVSPName , '' as Print_VLC_Code_VLC_Uploader, sum( PPPP.AMOUNT) as AMOUNT , sum (PPPP.[Cartage/Others/GrossAmt]) as [Cartage/Others/GrossAmt], sum (PPPP.Qty ) as Qty, sum ( PPPP.[Qty(KGS)]) as [Qty(KGS)], sum (pppp. FAT_KG) as FAT_KG , sum ( PPPP.SNF_KG) as SNF_KG , sum (PPPP.VSP_Day_Wise_Incentive ) as  VSP_Day_Wise_Incentive, sum ( PPPP.TIP_Amount) as TIP_Amount , '' as ColFirst_DeductionCode ,'' as ColFirst_DeductionMilkTypeRow, sum (PPPP.ColFirst_DeductionAmt) as ColFirst_DeductionAmt , '' as ColSecond_DeductionCode, '' as ColSecond_DeductionMilkTypeRow, sum ( PPPP.ColSecond_DeductionAmt ) as  ColSecond_DeductionAmt, '' as ColThird_DeductionCode , '' as ColThird_DeductionMilkTypeRow , sum ( PPPP.ColThird_DeductionAmt) as ColThird_DeductionAmt , '' as DeductionMilkTypeRow , case when PPPP.MilkType = 'B' then sum (PPPP.TotalDeductionAmount) else 0 end TotalDeductionAmount , sum (PPPP.NetAmount ) as  NetAmount   from ( " + qryMain + " ) PPPP group by MilkType "
            Dim qry = CTEQuery + " select SNO, RowNo , MCC , [MCC Name] , VLC_CODE , VLC_Name , VLC_Code_VLC_Uploader , VSP_CODE ,[VSP Name] , MilkType , case when MilkType = 'B' and RowNo = 5 then 'Total'  else Print_VLC_NAMEAndVSPName end Print_VLC_NAMEAndVSPName,Print_VLC_Code_VLC_Uploader, case when MilkType = 'E' then Null else AMOUNT end AMOUNT , case when MilkType = 'E' then Null else [Cartage/Others/GrossAmt] end as [Cartage/Others/GrossAmt] , case when MilkType = 'E' then Null else  qty end qty ,case when MilkType = 'E' then Null else [Qty(KGS)] end as  [Qty(KGS)], case when MilkType = 'E' then Null else FAT_KG  end FAT_KG ,case when MilkType = 'E' then Null else SNF_KG end as SNF_KG , case when MilkType = 'E' then Null else VSP_Day_Wise_Incentive end  VSP_Day_Wise_Incentive, case when MilkType = 'E' then Null else  TIP_Amount end  TIP_Amount , ColFirst_DeductionAmt , ColSecond_DeductionAmt , ColThird_DeductionAmt, case when MilkType = 'B' then TotalDeductionAmount else Null end TotalDeductionAmount , case when MilkType in ('B','E') then NetAmount else null end NetAmount from ( " + qryMain + qryFooterTotal + "  ) ZZZZFinal order by VLC_CODE ,VSP_CODE  , MilkType,RowNo "




            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True

                Dim arrMCCList As ArrayList = New ArrayList()
                If MultipleFinderFillAuto = False Then
                    arrMCCList.Add(clsCommon.myCstr(fndSingleMCCCode.Value))
                End If


                qry = clsMilkRejectHead.GetMCCRegisterQuery(txtMilkReceiptFromDate.Value, txtMilkReciptToDate.Value, "M", "E", "ZeroAndNonZero", Nothing, arrMCCList, Nothing, Nothing, "")
                qry = " select pp.[MCC Code]  as [MCC Code],pp.[Route Code] as [Route Code],pp.[Vlc Code],MAX(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader, Convert(decimal(18,2),sum ([SubStandardQty])) as [SubStandardQty] from (
                    " + qry + "
                     ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[Vlc Code] "

                Dim SubQty As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim subQtyTotal As Decimal = 0
                For ii As Integer = 0 To SubQty.Rows.Count - 1
                    subQtyTotal += clsCommon.myCdbl(SubQty.Rows(ii)("SubStandardQty"))
                Next
                Dim Tablerowcount As Integer = dt.Rows.Count - 1
                Dim dr As DataRow() = dt.Select("MilkType='E'")
                If dr IsNot Nothing AndAlso dr.Length > 0 Then
                    Tablerowcount = Tablerowcount + ((dr.Length - 1) * 2)
                End If
                For i As Integer = 0 To Tablerowcount - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("MilkType")), "E") = CompairStringResult.Equal Then

                        Dim SubQtydr As DataRow() = SubQty.Select("[MCC Code]='" + dt.Rows(i).Item("MCC") + "' and [Vlc Code]='" + dt.Rows(i).Item("VLC_CODE") + "' and VLC_Code_VLC_Uploader='" + dt.Rows(i).Item("VLC_Code_VLC_Uploader") + "'")
                        Dim workRow As DataRow = dt.NewRow()
                        If SubQtydr IsNot Nothing AndAlso SubQtydr.Length > 0 Then
                            workRow.Item("Print_VLC_Code_VLC_Uploader") = "SUBSTANDARD"
                            workRow.Item("Print_VLC_NAMEAndVSPName") = "QUANTITY LTRS:" + clsCommon.myCstr(SubQtydr(0).Item("SubStandardQty"))
                        Else
                            workRow.Item("Print_VLC_Code_VLC_Uploader") = "SUBSTANDARD"
                            workRow.Item("Print_VLC_NAMEAndVSPName") = "QUANTITY LTRS:"
                        End If

                        dt.Rows.InsertAt(workRow, (i + 1))
                        dt.Rows.InsertAt((dt.NewRow()), (i + 2))
                    End If
                Next

                dt.Columns.Add("AddDash", GetType(String))
                For i As Integer = 0 To dt.Rows.Count - 1
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(i).Item("Print_VLC_NAMEAndVSPName")), "Total") = CompairStringResult.Equal Then
                        dt.Rows(i)("AddDash") = "1"
                    End If
                Next

                Dim drSubTemp As DataRow = dt.NewRow
                drSubTemp("Print_VLC_Code_VLC_Uploader") = "SUBSTANDARD"
                drSubTemp("Print_VLC_NAMEAndVSPName") = "TOT QUANTITY LTRS:" + clsCommon.myCstr(subQtyTotal)
                dt.Rows.Add(drSubTemp)
                gv1.DataSource = dt
                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If
                'If gv1.Columns.Contains("MCC_Code") = True Then
                '    gv1.Columns("MCC_Code").HeaderText = "Location Code"
                'End If
                'If gv1.Columns.Contains("MCC_Name") = True Then
                '    gv1.Columns("MCC_Name").HeaderText = "Location Name"
                'End If

                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then

                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = ""
                    obj.ReportName1 = "THE TELANGANA STATE DAIRY DEVELOPMENT CO-OPERATIVE FEDERATION LIMITED. "   '"CHECK LIST FOR MILK PROCUREMENT PARTICULARS DATED: " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + "   " + strShift + " UserLogin: " + objCommonVar.CurrentUserCode.ToUpper() + " "
                    obj.ReportName2 = "R O U T E    B I L L S    A B S T R A C T  " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " TO " + clsCommon.myCstr(txtMilkReciptToDate.Text) + ""
                    obj.ShowPageNo = True
                    obj.LandscapPageSetupColumnsChar = 220
                    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT CODE AND NAME", clsCommon.myCstr(fndSingleMCCCode.Value) + " " + clsCommon.myCstr(lblSingleMCCName.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TRANSATION DATE", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("COLLECTIONS", strShift))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Print_VLC_Code_VLC_Uploader", "CODE", False, DosPrintAlignment.Left, 8, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Print_VLC_NAMEAndVSPName", "NAME OF THE CENTER         NAME OF THE PRESIDENT      .....................      .....................", False, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Qty", "     QTY-LTS           BM           CM        TOTAL .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Qty(KGS)", "     QTY-KGS           BM           CM        TOTAL .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("FAT_KG", "      KG-FAT           BM           CM        TOTAL .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SNF_KG", "      KG-SNF           BM           CM        TOTAL .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("AMOUNT", " TOTAL GROSS       BM AMT       CM AMT     OPC/COMN .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Cartage/Others/GrossAmt", "      DETAILS      CARTAGE       OTHERS    GROSS-AMT    .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))


                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ColFirst_DeductionAmt", "        TOTAL    CESS ON S     FEED DED     M.SPARES   M.TESTER", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ColSecond_DeductionAmt", "   DEDUCTIONS    OTHER DED     SEED DED    STATIONAR    STORE(A)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("ColThird_DeductionAmt", "      DETAILS     STORE(T)  VACCINE DED  VIJY LN DED  VIJY RD DED", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF KG", " Good Milk KGSNF", False, DosPrintAlignment.Right, 11, False, DecimalPlaces.Two))


                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TotalDeductionAmount", "      TOT DED     ********     ********     ********     ********", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("NetAmount", "      NET AMT     ********     ********     ********     ********", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("TIP_Amount", "       T.I.P          BM          CM         TOT   .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VSP_Day_Wise_Incentive", "     INC-AMT          BM          CM         TOT     .....", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(LTR)", "    Total (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(KGS)", "    Total (KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total FAT KG", "    Total KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF KG", "    Total KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))


                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Curd Qty(LTR)", "     Curd Qty(LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", "        PTC RECVRY", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.Print(obj, dt, PageSetup.Landscap, "AddDash", "1")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UnitWiseDeduction(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            If clsCommon.myLen(fndSingleMCCCode.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select MCC First.", Me.Text)
                Return
            End If
            Dim strDeductionNameForPivot As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',['+Description+']'  from (select TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Sequence_No from TSPL_DEDUCTION_MASTER ) XXX order by Sequence_No For XML Path('')),1,1,'') "))
            If clsCommon.myLen(strDeductionNameForPivot) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Dim strDeductionNameWithIsNull As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',isnull( ['+Description+'],0) as ' + '['+Description+']' from (select TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Sequence_No from TSPL_DEDUCTION_MASTER ) XXX order by Sequence_No For XML Path('')),1,1,'') "))
            Dim strDeductionNameWithSum As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',sum(isnull( ['+Description+'],0)) as ' + '['+Description+']' from (select TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Sequence_No from TSPL_DEDUCTION_MASTER ) XXX order by Sequence_No For XML Path('')),1,1,'') "))
            Dim strDeductionNameForTotal As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select '+ isnull( ['+Description+'],0)' from (select TSPL_DEDUCTION_MASTER.Description,TSPL_DEDUCTION_MASTER.Sequence_No from TSPL_DEDUCTION_MASTER ) XXX order by Sequence_No For XML Path('')),1,1,'') "))

            Dim whrForMulDed As String = " and 2= 2 "
            Dim whrForSingleDed As String = " and 2= 2 "

            'If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            '    whrForMulDed += " and TSPL_VLC_MASTER_HEAD.VLC_Code  in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") "
            '    whrForSingleDed += " and TSPL_VLC_MASTER_HEAD.VLC_Code  in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ") "
            'End If
            'If fndSingleMCCCode.Value Then
            whrForMulDed += " and TSPL_MULTIPLE_DEDUCTION_HEAD.MCC_Code in ('" + fndSingleMCCCode.Value + "') "
                whrForSingleDed += " and TSPL_VLC_MASTER_HEAD.MCC in ('" + fndSingleMCCCode.Value + "') "
                'End If

                'If ChkPosted.IsChecked = True Then
                whrForMulDed += " and TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 1 "
            whrForSingleDed += " and  len (TSPL_VENDOR_INVOICE_HEAD.Posting_Date) > 0  "
            'ElseIf ChkUnPosted.IsChecked = True Then
            '    whrForMulDed += " and TSPL_MULTIPLE_DEDUCTION_HEAD.IsPosted = 0 "
            '    whrForSingleDed += " and  len (TSPL_VENDOR_INVOICE_HEAD.Posting_Date) <= 0  "
            'End If


            Dim qry As String = " select [Plant Code],MAX([Plant Name]) as [Plant Name] , [MCC Code], max([MCC Name]) as [MCC Name],[Vendor Code],[VLC Uploader Code],(VLC_Code) as [VLC Code],max(VLC_Name) as [VLC Name], " + strDeductionNameWithSum + " , sum ([Total]) as [Total] from (
                    select  [Plant Code] , [Plant Name] , [MCC Code],[MCC Name],Vendor_Code as [Vendor Code],[VLC Uploader Code],VLC_Code,VLC_Name, " + strDeductionNameWithIsNull + " , " + strDeductionNameForTotal + " as [Total]
                    from (
                    select Final.[Plant Code], max(Final. [Plant Name]) as [Plant Name] , Final. [MCC Code],max(Final.[MCC Name]) as  [MCC Name], Final.Vendor_Code , max(Final.[VLC Uploader Code]) as [VLC Uploader Code],(VLC_Code) as VLC_Code ,max(VLC_Name) as VLC_Name,Final.DeductionCode  , max(Final.DeductionName) as DeductionName, sum(Final. Amount)as Amount from (
                    
                    select TSPL_VENDOR_INVOICE_HEAD.Loc_Code  as [Plant Code], TSPL_GL_SEGMENT_CODE.Description as [Plant Name], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code], TSPL_LOCATION_MASTER.Location_Desc as [MCC Name],  TSPL_VENDOR_INVOICE_HEAD.Vendor_Code , (TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader) as [VLC Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name ,TSPL_VENDOR_INVOICE_DETAIL.DeductionCode  , (TSPL_DEDUCTION_MASTER.Description) as DeductionName, (TSPL_VENDOR_INVOICE_DETAIL.Total_Amount) as Amount   from TSPL_VENDOR_INVOICE_DETAIL  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No  left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code = TSPL_VENDOR_INVOICE_HEAD.Loc_Code  left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_VLC_MASTER_HEAD.MCC  left outer join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = TSPL_VENDOR_INVOICE_DETAIL.DeductionCode where TSPL_VENDOR_INVOICE_HEAD.isDeduction = 1
                     and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ,103)>=convert(date,'" + txtMilkReceiptFromDate.Value + "',103) AND convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)<=convert(date,'" + txtMilkReciptToDate.Value + "',103)  " + whrForSingleDed + "
                    ) Final group by Final.[Plant Code], Final.[MCC Code], Final.VLC_Code, Final.Vendor_Code,Final.DeductionCode
                    )  XFinal 
                    pivot
                    (
                    sum(XFinal.Amount)
                    for XFinal.DeductionName in (" + strDeductionNameForPivot + ")
                    ) piv  )Final group by [Plant Code] , [MCC Code],[Vendor Code],[VLC Uploader Code],VLC_Code "





            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True
                gv1.DataSource = dt
                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If


                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then

                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = ""
                    obj.ReportName1 = "UNIT NAME :  " + lblSingleMCCName.Text + "          DATE OF ENDING : " + clsCommon.myCstr(clsCommon.GetPrintDate(txtMilkReciptToDate.Value, "dd/MM/yyyy")) + "                                                                                                   "

                    obj.ShowPageNo = True
                    obj.LandscapPageSetupColumnsChar = 270

                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VLC Uploader Code", "Center Code", False, DosPrintAlignment.Left, 13, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VLC NAME", "Center Name", False, DosPrintAlignment.Left, 15, False, DecimalPlaces.NA))

                    If clsCommon.myLen(strDeductionNameForPivot) > 0 Then
                        Dim result As String() = strDeductionNameForPivot.Split(New String() {","}, StringSplitOptions.None)
                        Dim finalchar As String = ""
                        Dim finalcharHeading As String = ""
                        For Each s As String In result
                            finalchar = s.Replace("[", "")
                            finalchar = finalchar.Replace("]", "") ' SetSpace
                            finalcharHeading = finalchar
                            finalcharHeading = finalcharHeading.Replace(" DEDUCTION", "")
                            'finalcharHeading = finalchar.Replace("DEDUCTIONS", "")
                            finalcharHeading = finalcharHeading.Replace(" DED", "")
                            finalcharHeading = finalcharHeading.Replace(" AMOUNT", "")
                            finalcharHeading = finalcharHeading.Replace(" INPUT", "")
                            finalcharHeading = finalcharHeading.ToString.Trim()
                            obj.arrColumn.Add(clsDosPrintColumn.SetColumn(finalchar, finalcharHeading, False, DosPrintAlignment.Right, 13, True, DecimalPlaces.Two))
                        Next
                        obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total", "Total", False, DosPrintAlignment.Right, 15, True, DecimalPlaces.Two))

                    End If
                    obj.Print(obj, dt, PageSetup.Landscap)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub TIPsummaryReportMCCandVLCwise(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            If clsCommon.myLen(fndSingleMCCCode.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select MCC First.", Me.Text)
                Return
            End If
            Dim qry As String = ""
            Dim arrMCCList As ArrayList = New ArrayList()

            If MultipleFinderFillAuto = False Then
                arrMCCList.Add(clsCommon.myCstr(fndSingleMCCCode.Value))
            End If

            'Dim strMCC As String = "select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + fndLoc.Value + "'"
            'Dim dtMCC As DataTable = clsDBFuncationality.GetDataTable(strMCC)
            'If (dtMCC IsNot Nothing AndAlso dtMCC.Rows.Count > 0) Then
            '    For Each dr As DataRow In dtMCC.Rows
            '        arrMCCList.Add(clsCommon.myCstr(dr("MCC_Code")))
            '    Next
            'End If

            qry = clsMilkRejectHead.GetMCCRegisterQuery(txtMilkReceiptFromDate.Value, txtMilkReciptToDate.Value, "M", "E", "ZeroAndNonZero", Nothing, arrMCCList, Nothing, Nothing, "")
            qry = " Select aa.[MCC Code] ,aa.[MCC Name],aa.[MCC Type] ,aa.[Chilling Center],aa.[Plant Code],aa.[Plant Name] ,aa.[Route Code] ,aa.[Route Name],aa.[Vlc Code] ,aa.[VLC Name],aa.VLC_Code_VLC_Uploader as [VLC Uploader Code],aa.[Vendor Group Code],aa.[Vendor Group Desc],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.CLR,aa.[FAT(KG)] ,aa.[SNF(KG)] ,aa.[Cow Milk Qty (KG)] ,aa.[Cow FAT(%)] ,aa.[Cow CLR],aa.[Cow SNF(%)] ,aa.[Cow FAT (KG)] ,aa.[Cow SNF (KG)] ,aa.[Buffalo Milk Qty (KG)] ,aa.[Buffalo FAT(%)] ,aa.[Buffalo CLR],aa.[Buffalo SNF(%)] ,aa.[Buffalo FAT (KG)] ,aa.[Buffalo SNF (KG)] ,aa.[SRN Qty],aa.[SRN Amount],aa.EMP_Amount,aa.TIP_Amount,aa.NET_AMOUNT,aa.Round_Off,aa.Handling_Charges_Amount,aa.Head_Load_Amount,aa.SNF_Ded_Amount,aa.VSP_Commission_Amount ,aa.VSP_Deduction_Amount,aa.VSP_Day_Wise_Incentive,aa.Vehicle, SubStandardQty , isnull(aa.[Milk Weight(LTR)],0) - isnull( SubStandardQty,0) as IncentiveQty from ( 
                    Select  xxx.* ,
                    case when [Cow Milk Qty (KG)] =0 then 0 else [Cow FAT (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow FAT(%)],
                    case when [Cow Milk Qty (KG)] =0 then 0 else [Cow Snf (KG)]/[Cow Milk Qty (KG)] *100 end as [Cow SNF(%)],
                    case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo FAT (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo FAT(%)],
                    case when  [Buffalo Milk Qty (KG)] =0 then 0 else [Buffalo SNF (KG)]/[Buffalo Milk Qty (KG)] *100 end as [Buffalo SNF(%)]
                    from (
                    select xx.*
                    from ( 
                    select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],max(pp.[MCC Type]) as [MCC Type],max(pp.[Chilling Center]) as [Chilling Center],max(pp.[Plant Code])  as [Plant Code],max(pp.[Plant Name] )  as [Plant Name],pp.[Route Code] as [Route Code],max(pp.[Route Name] ) as [Route Name],pp.[Vlc Code],max([VLC Name]) as [VLC Name],MAX(pp.[Vlc Uploader Code]) AS VLC_Code_VLC_Uploader,MAX (pp.[Vendor Group Code]) as [Vendor Group Code] ,MAX ([Vendor Group Desc]) as [Vendor Group Desc],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)],
                    case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)],
                    case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)]
                    ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],
                    sum([FAT(LTR)] ) as [FAT(LTR)] ,sum([SNF(LTR)] ) as [SNF(LTR)],
                    sum(pp.[Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)],
                    sum([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)],
                    sum([SRN Qty]) as [SRN Qty] ,sum([Cow FAT (KG)]) as [Cow FAT (KG)], sum ([Cow SNF (KG)]) as [Cow SNF (KG)], sum([Buffalo FAT (KG)]) as [Buffalo FAT (KG)], sum( [Buffalo SNF (KG)]) as [Buffalo SNF (KG)],sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR,avg([Cow CLR]) as [Cow CLR] ,avg([Buffalo CLR]) as [Buffalo CLR],sum(EMP_Amount) as EMP_Amount,sum(TIP_Amount) as TIP_Amount,sum(NET_AMOUNT) as NET_AMOUNT,sum(Round_Off) as Round_Off,sum(Handling_Charges_Amount) as Handling_Charges_Amount,sum(Head_Load_Amount) as Head_Load_Amount,sum(SNF_Ded_Amount )as SNF_Ded_Amount,sum(VSP_Commission_Amount) as VSP_Commission_Amount,sum(VSP_Deduction_Amount ) as VSP_Deduction_Amount ,sum(VSP_Day_Wise_Incentive ) as VSP_Day_Wise_Incentive,max(Vehicle) as Vehicle, sum ([SubStandardQty]) as [SubStandardQty] from (
                    " + qry + "
                    ) as  pp group by pp.[MCC Code],pp.[Route Code],pp.[Vlc Code] 
                    )as xx
                    ) as xxx           
                    ) as aa order  by [MCC Code],[Route Code],[Vlc Code] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True
                gv1.DataSource = dt
                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If

                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then

                    Dim obj As clsDosPrint = New clsDosPrint()
                    obj.ReportName = ""
                    obj.ReportName1 = "THE TELANGANA STATE DAIRY DEVELOPMENT CO-OPERATIVE FEDERATION LIMITED. "   '"CHECK LIST FOR MILK PROCUREMENT PARTICULARS DATED: " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + "   " + strShift + " UserLogin: " + objCommonVar.CurrentUserCode.ToUpper() + " "
                    obj.ReportName2 = "CENTER WISE STATEMENT FROM " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " TO " + clsCommon.myCstr(txtMilkReciptToDate.Text) + ""
                    obj.ShowPageNo = True

                    obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("SHED NAME", clsCommon.myCstr(txtLocName.Text)))
                    obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TRANSATION DATE", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("COLLECTIONS", strShift))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



                    obj.arrColumn = New List(Of clsDosPrintColumn)()
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VLC Uploader Code", "CENTER ID", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VLC Name", "NAME", False, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Milk Weight(LTR)", "QTY", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("IncentiveQty", "INC QTY", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VSP_Day_Wise_Incentive", "INC AMT", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SubStandardQty", "SUB QTY", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))



                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(LTR)", "    Total (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(KGS)", "    Total (KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total FAT KG", "    Total KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF KG", "    Total KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))


                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Curd Qty(LTR)", "     Curd Qty(LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", "        PTC RECVRY", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    obj.Print(obj, dt, PageSetup.Potrate)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UnitwiseBillSummary(ByVal isDotMaterixPrint As Boolean)
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                Return
            End If
            If clsCommon.myLen(fndSingleMCCCode.Value) <= 0 AndAlso MultipleFinderFillAuto = False Then
                clsCommon.MyMessageBoxShow(Me, "Please select MCC First.", Me.Text)
                Return
            End If
            'Dim StrDeductionHead As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' + QUOTENAME(" &
            '   " ISNULL(TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,'')) as Alies_Name" &
            '   " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
            '   " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
            '   "  where document_type='D' and isDeduction=1 and Posting_Date is not null " &
            '   " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtMilkReceiptFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtMilkReciptToDate.Value & "',103)" &
            '   " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

            'Dim StrDeductionHeadSum As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' " &
            ' "  +'Sum(isnull(' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) +',0))' +' as ' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) as Alies_Name" &
            ' " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
            ' " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
            ' "  where document_type='D' and isDeduction=1 and Posting_Date is not null " &
            ' " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtMilkReceiptFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtMilkReciptToDate.Value & "',103)" &
            ' " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

            'Dim StrDeductionHeadSumTotal As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT " &
            ' "  isnull(STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc) +',0))' as Alies_Name" &
            ' " from TSPL_VENDOR_INVOICE_DETAIL left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
            ' " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
            ' "  where document_type='D' and isDeduction=1 and Posting_Date is not null " &
            ' " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtMilkReceiptFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtMilkReciptToDate.Value & "',103)" &
            ' " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

            Dim StrDeductionHead As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' + QUOTENAME(" &
               " ISNULL(TSPL_DEDUCTION_MASTER.Code,'')) as Alies_Name" &
               " from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'DEDUCTION' " &
               " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

            Dim StrDeductionHeadSum As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   isnull(STUFF((SELECT distinct ',' " &
             "  +'Sum(isnull(' + QUOTENAME( TSPL_DEDUCTION_MASTER.Code) +',0))' +' as ' + QUOTENAME( TSPL_DEDUCTION_MASTER.Code) as Alies_Name" &
             " from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'DEDUCTION' " &
             " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

            Dim StrDeductionHeadSumTotal As String = clsDBFuncationality.getSingleValue("DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT " &
             "  isnull(STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_DEDUCTION_MASTER.Code) +',0))' as Alies_Name" &
             " from TSPL_DEDUCTION_MASTER where Ded_Grp_Code = 'DEDUCTION' " &
             " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,''),'')")

            Dim qry As String = ""
            If clsCommon.myLen(StrDeductionHead) > 0 Then
                qry += " select [MCC Code],	max([MCC Name]) as [MCC Name] ,sum([BM Amount]) as [BM Amount], Convert (decimal(18,1), sum([BM Qty])) as [BM Qty],sum([CM Amount]) as [CM Amount],Convert (decimal(18,1),sum ([CM Qty]))  as [CM Qty], 
                         Convert (decimal(18,1),sum ([BM Qty(KGS)])) as [BM Qty(KGS)],Convert (decimal(18,1),sum ([CM Qty(KGS)])) as [CM Qty(KGS)],Convert (decimal(18,2),sum([BM FATKG])) as [BM FATKG],Convert (decimal(18,2),sum ([CM FATKG])) as [CM FATKG],Convert (decimal(18,2),sum ([BM SNFKG])) as [BM SNFKG],Convert (decimal(18,2),sum ([CM SNFKG])) as [CM SNFKG], sum([BM Amount]) + sum([CM Amount]) as [MM Amount], Convert (decimal(18,1),sum([BM Qty])) + Convert (decimal(18,1),sum ([CM Qty]))  as  [MM Qty], Convert (decimal(18,1),sum ([BM Qty(KGS)]))+ Convert (decimal(18,1),sum ([CM Qty(KGS)])) as [MM Qty(KGS)], Convert (decimal(18,2),sum([BM FATKG])) + Convert (decimal(18,2),sum ([CM FATKG])) as [MM FATKG] , Convert (decimal(18,2),sum ([BM SNFKG])) + Convert (decimal(18,2),sum ([CM SNFKG])) as [MM SNFKG], Convert (decimal(18,1),sum ([AM Qty(LTR)])) as [AM Qty(LTR)], Convert (decimal(18,1),sum ( [PM Qty(LTR)])) as [PM Qty(LTR)]
                         ,Convert (decimal(18,2),sum([COMSN Amount])) as [COMSN Amount],Convert (decimal(18,2),sum([OP-COST Amount])) as [OP-COST Amount],Convert (decimal(18,2),sum([CART Amount])) as [CART Amount],Convert (decimal(18,2),sum([INC Amount])) as [INC Amount] ,Convert (decimal(18,2),sum([ADDN Amount])) as [ADDN Amount],Convert (decimal(18,2),sum([Gross Amount])) as [Gross Amount] " &
                       " ," & StrDeductionHeadSum & ",(" & StrDeductionHeadSumTotal & ") as [Total Deduction Amount],(sum([Gross Amount])-(" & StrDeductionHeadSumTotal & ")) as [Net Amount]" &
                       " ,Convert (decimal(18,2),sum([T.I.P Amount])) as [T.I.P Amount],sum([KG Fat(RS.30)]) as [KG Fat(RS.30)], sum([BM Amount]) + sum([CM Amount])+Convert (decimal(18,2),sum([T.I.P Amount])) + Convert (decimal(18,2),sum([ADDN Amount])) as [SummaryTotal],Convert (decimal(18,2),sum([T.I.P Amount])) + " & StrDeductionHeadSumTotal & " as [TotalRecoveres] from ( " &
                       "select  s.[MCC Code], s.[MCC Name] ,Deduction_Desc ,isnull(deduction.DeductionAmt,0) as DeductionAmt,[BM Amount],[CM Amount],[BM Qty],[CM Qty],[BM Qty(KGS)],[CM Qty(KGS)],[BM FATKG],[CM FATKG],[BM SNFKG],[CM SNFKG] ,[COMSN Amount],[OP-COST Amount],[CART Amount],[INC Amount] ,[ADDN Amount],[Gross Amount]  ,[T.I.P Amount],[KG Fat(RS.30)],[AM Qty(LTR)],[PM Qty(LTR)] " &
                       " from ( "

            End If

            qry += " select pp.MCC as [MCC Code],pp.[MCC Name] ,sum(pp.[BM Amount]) as [BM Amount],sum (pp.[BM Qty]) as [BM Qty],sum([BM Qty(KGS)]) as [BM Qty(KGS)],sum( [BM FATKG]) as [BM FATKG],sum ([BM SNFKG]) as [BM SNFKG] ,sum(pp.[CM Amount]) as [CM Amount] , sum (pp.[CM Qty]) as [CM Qty] ,sum([CM Qty(KGS)]) as [CM Qty(KGS)],sum ([CM FATKG]) as [CM FATKG],sum ([CM SNFKG]) as [CM SNFKG] ,0 as [COMSN Amount],0 as [OP-COST Amount],0 as [CART Amount],0 as [INC Amount] ,isnull (sum(Incentive_Amount),0) as [ADDN Amount] ,(isnull(sum(pp.[BM Amount]),0)+isnull(sum(pp.[CM Amount]),0) + isnull(sum(Incentive_Amount),0)) as [Gross Amount]  ,sum(TIP_Amount) as [T.I.P Amount], 0 as [KG Fat(RS.30)], sum ([AM Qty(LTR)]) as [AM Qty(LTR)], sum ([PM Qty(LTR)]) as [PM Qty(LTR)] " &
                       " from ( " &
            " Select (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='B' then  Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) else 0 end) as [BM Amount], 
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='B' then TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR   else 0 end) as [BM Qty],
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='B' then TSPL_MILK_SRN_DETAIL.ACC_Qty   else 0 end) as [BM Qty(KGS)],
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='B' then TSPL_MILK_SRN_DETAIL.FAT_KG   else 0 end) as [BM FATKG] ,
(case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='B' then TSPL_MILK_SRN_DETAIL.SNF_KG   else 0 end) as [BM SNFKG],
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='C' then Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) else 0 end) as [CM Amount],
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='C' then TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR  else 0 end) as [CM Qty],
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='C' then TSPL_MILK_SRN_DETAIL.ACC_Qty  else 0 end) as [CM Qty(KGS)],
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='C' then TSPL_MILK_SRN_DETAIL.FAT_KG  else 0 end) as [CM FATKG],
            (case when TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type='C' then TSPL_MILK_SRN_DETAIL.SNF_KG  else 0 end) as [CM SNFKG],
            TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type  As [Milk Type] , TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, 
            TSPL_MCC_MASTER.MCC_NAME As [MCC Name],Convert(decimal(18,2),
            TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ) as Incentive_Amount  ,isnull(TSPL_MILK_SRN_DETAIL.TIP_Amount,0) as TIP_Amount ,
            TSPL_MILK_SRN_HEAD.SHIFT , case when TSPL_MILK_SRN_HEAD.SHIFT = 'M' then isnull (TSPL_MILK_SRN_DETAIL.ACC_Qty,0)  else 0 end as [AM Qty(LTR)],
            case when TSPL_MILK_SRN_HEAD.SHIFT = 'E' then isnull (TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR,0)  else 0 end as [PM Qty(LTR)] 
            From TSPL_MILK_SRN_HEAD  " &
            " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_DETAIL.DOC_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " &
            " left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.DOC_CODE " &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE " &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL on TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_DOC_Code = TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE  " &
            " and   TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_SRN_Code=TSPL_MILK_SRN_HEAD.DOC_CODE  " &
            " and TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.MILK_Item_Code=TSPL_MILK_PURCHASE_INVOICE_DETAIL.Item_Code  " &
            " Left Outer Join TSPL_INCENTIVE_MASTER_HEAD on TSPL_INCENTIVE_MASTER_HEAD.INCENTIVE_CODE=TSPL_MILK_PURCHASE_INVOICE_INCENTIVEDETAIL.Incentive_Code " &
            " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  " &
            " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code   " &
            " left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_MILK_SRN_DETAIL.Item_Code
              left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_MILK_SRN_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_MILK_SRN_DETAIL.UOM_Code = Stocking_Conversion_Factor.UOM_Code " &
            " where 2=2 " &
             " and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) >=  convert(date,'" + txtMilkReceiptFromDate.Value + "',103)  and  convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= convert(date,'" + txtMilkReciptToDate.Value + "',103) " &
             " and tspl_location_master.location_Code ='" + fndLoc.Value + "' " + IIf(MultipleFinderFillAuto = True, " ", " and TSPL_MILK_SRN_HEAD.MCC_CODE = '" + fndSingleMCCCode.Value + "' ") + "  " &
            " )pp group by pp.MCC,pp.[MCC Name]  "

            If clsCommon.myLen(StrDeductionHead) > 0 Then
                qry += " ) as s  " &
            " left join    " &
            "(select TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc,sum(TSPL_VENDOR_INVOICE_DETAIL.Amount) as DeductionAmt " &
            " from TSPL_VENDOR_INVOICE_DETAIL  " &
            " left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.document_no=TSPL_VENDOR_INVOICE_DETAIL.document_no " &
            " INNER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VENDOR_INVOICE_HEAD.MCC_Code " &
            " where document_type='D' and isDeduction=1 and Posting_Date is not null " &
            " and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >=convert(date,'" & txtMilkReceiptFromDate.Value & "',103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <=convert(date,'" & txtMilkReciptToDate.Value & "',103)" &
            " group by TSPL_VENDOR_INVOICE_HEAD.MCC_Code,TSPL_VENDOR_INVOICE_DETAIL.Deduction_Desc " &
            ") deduction on deduction.MCC_Code=s.[MCC Code] " &
            ")tt " &
            " pivot (  sum(DeductionAmt) for Deduction_Desc  in (" & StrDeductionHead &
            ") ) as zpivot group by zpivot.[MCC Code]  "
            End If




            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.ShowGroupPanel = True
                gv1.EnableFiltering = True
                gv1.DataSource = dt
                If gv1.Columns.Contains("SNO") = True Then
                    gv1.Columns("SNO").IsVisible = False
                End If

                gv1.BestFitColumns()

                RadPageView1.SelectedPage = RadPageViewPage2
                If isDotMaterixPrint = True Then

                    'Dim obj As clsDosPrint = New clsDosPrint()
                    'obj.ReportName = ""
                    'obj.ReportName1 = "THE TELANGANA STATE DAIRY DEVELOPMENT CO-OPERATIVE FEDERATION LIMITED. "   '"CHECK LIST FOR MILK PROCUREMENT PARTICULARS DATED: " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + "   " + strShift + " UserLogin: " + objCommonVar.CurrentUserCode.ToUpper() + " "
                    'obj.ReportName2 = "CENTER WISE STATEMENT FROM " + clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " TO " + clsCommon.myCstr(txtMilkReciptToDate.Text) + ""
                    'obj.ShowPageNo = True

                    'obj.arrFilter = New List(Of clsDosPrintHeaderFilter)()
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("SHED NAME", clsCommon.myCstr(txtLocName.Text)))
                    'obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    ''obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("UNIT NAME", clsCommon.myCstr(lblSingleMCCName.Text)))
                    ''obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("TRANSATION DATE", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))
                    ''obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("COLLECTIONS", strShift))
                    ''obj.arrFilter.Add(clsDosPrintHeaderFilter.GetObject("PERIOD FROM", clsCommon.myCstr(txtMilkReceiptFromDate.Text) + " To " + clsCommon.myCstr(txtMilkReciptToDate.Text)))



                    'obj.arrColumn = New List(Of clsDosPrintColumn)()
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VLC Uploader Code", "CENTER ID", False, DosPrintAlignment.Left, 10, False, DecimalPlaces.NA))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VLC Name", "NAME", False, DosPrintAlignment.Left, 20, False, DecimalPlaces.NA))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Milk Weight(LTR)", "QTY", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("IncentiveQty", "INC QTY", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("VSP_Day_Wise_Incentive", "INC AMT", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))
                    'obj.arrColumn.Add(clsDosPrintColumn.SetColumn("SubStandardQty", "SUB QTY", False, DosPrintAlignment.Right, 10, True, DecimalPlaces.Two))



                    ''obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(LTR)", "    Total (LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    ''obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total(KGS)", "    Total (KGS)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    ''obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Total FAT KG", "    Total KGFAT", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    ''obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Good Milk SNF KG", "    Total KGSNF", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))


                    ''obj.arrColumn.Add(clsDosPrintColumn.SetColumn("Curd Qty(LTR)", "     Curd Qty(LTR)", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))

                    ''obj.arrColumn.Add(clsDosPrintColumn.SetColumn("PTC RECVRY", "        PTC RECVRY", False, DosPrintAlignment.Right, 10, False, DecimalPlaces.Two))
                    'obj.Print(obj, dt, PageSetup.Potrate)
                    Dim frm As New frmCrystalReportViewer()
                    Dim strReportPath As String = frm.GetReportPath(CrystalReportFolder.MilkProcurement, "UnitMilkBillSummery")
                    Dim subPath As String = "C:\\ERPTempFolder"
                    strReportPath = strReportPath.Replace(".rpt", ".Txt")
                    Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
                    If (IsExists = False) Then
                        System.IO.Directory.CreateDirectory(subPath)
                    End If
                    subPath += "\\" & clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmsss") & ".Txt"
                    IsExists = System.IO.File.Exists(subPath)
                    If IsExists Then
                        System.IO.File.Delete(subPath)
                    End If
                    Dim fi As New IO.FileInfo(strReportPath)
                    fi.CopyTo(subPath, True)
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#UnitName#", lblSingleMCCName.Text))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#FromDate#", txtMilkReceiptFromDate.Text))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#ToDate#", txtMilkReciptToDate.Text))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalQuantity#", clsCommon.myCstr(dt.Rows(0)("MM Qty(KGS)"))))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalMilkValue#", clsCommon.myCstr(dt.Rows(0)("MM Amount"))))

                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TCrt10.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("CART Amount")), 10, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TOPC10.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("OP-COST Amount")), 10, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#AMLt10.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("AM Qty(LTR)")), 10, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#PMLt10.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("PM Qty(LTR)")), 10, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TOth10.2#", GetFormateColumnValue("0", 10, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TADD10.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("ADDN Amount")), 10, "L")))

                    '  #BQTYKg12.1# #BQTLtr12.1# #BKgFat12.2# #BKgSnf12.2# #BMkVal12.2#
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#BQTYKg12.1#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("BM Qty(KGS)")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#BQTLtr12.1#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("BM Qty")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#BKgFat12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("BM FATKG")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#BKgSnf12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("BM SNFKG")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#BMkVal12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("BM Amount")), 12, "L")))
                    '#CQTYKg12.1# #CQTLtr12.1# #CKgFat12.2# #CKgSnf12.2# #CMkVal12.2#  
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#CQTYKg12.1#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("CM Qty(KGS)")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#CQTLtr12.1#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("CM Qty")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#CKgFat12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("CM FATKG")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#CKgSnf12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("CM SNFKG")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#CMkVal12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("CM Amount")), 12, "L")))
                    '  #MQTYKg12.1# #MQTLtr12.1# #MKgFat12.2# #MKgSnf12.2# #MMkVal12.2#
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#MQTYKg12.1#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("MM Qty(KGS)")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#MQTLtr12.1#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("MM Qty")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#MKgFat12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("MM FATKG")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#MKgSnf12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("MM SNFKG")), 12, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#MMkVal12.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("MM Amount")), 12, "L")))

                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#FNEToDate#", GetFormateColumnValue(txtMilkReciptToDate.Text, 11, "L")))
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalFeedDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("FEED DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalFeedDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalSeedDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("SEED DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalSeedDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalStore(T)Ded16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("STORE(T) DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalStore(T)Ded16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalStore(A)Ded16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("STORE(A) DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalStore(A)Ded16.2#", GetFormateColumnValue("0.00", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalVijayRDDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("VIJY RD DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalVijayRDDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalVaccineDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("VACCINE DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalVaccineDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalVijayLnDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("VIJY LN DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalVijayLnDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalOtherDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("OTHER DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalOtherDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalMTesterDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("M.TESTER DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalMTesterDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalMSparesDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("M.SPARES DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalMSparesDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalCessOnSaleDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("CESS ON S DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalCessOnSaleDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalStationaryDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("STATIONAR DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalStationaryDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    'Try
                    '    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalKGFatDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("KG-FAT DEDUCTION")), 16, "L")))
                    'Catch ex As Exception
                    '    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalKGFatDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    'End Try
                    'Try
                    '    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalKGSnfDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("KG- SNF DEDUCTION")), 16, "L")))
                    'Catch ex As Exception
                    '    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalKGSnfDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    'End Try
                    Try
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalSchemeDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("SCHEME AMOUNT DED")), 16, "L")))
                    Catch ex As Exception
                        File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalSchemeDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    End Try
                    'Try
                    '    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalSourDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("SOUR DEDUCTION")), 16, "L")))
                    'Catch ex As Exception
                    '    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalSourDed16.2#", GetFormateColumnValue("0", 16, "L")))
                    'End Try



                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalDed16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("Total Deduction Amount")), 16, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#TotalTechFundTIP16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("T.I.P Amount")), 16, "L")))


                    '==========
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#SummaryTotalMilkValue16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("MM Amount")), 16, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#SummaryTotalTIP16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("T.I.P Amount")), 16, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#SummaryTotalAdditions16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("ADDN Amount")), 16, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#SummaryTotal16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("SummaryTotal")), 16, "L"))) ' 
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#SummaryRecoveries16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("Total Deduction Amount")), 16, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#SummaryRecoveresTIP16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("T.I.P Amount")), 16, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#SummaryTotalWithRecoveres16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("TotalRecoveres")), 16, "L"))) '
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#NetAmt16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("Net Amount")), 16, "L")))
                    File.WriteAllText(subPath, File.ReadAllText(subPath).Replace("#RoundedNetAmt16.2#", GetFormateColumnValue(clsCommon.myCstr(dt.Rows(0)("Net Amount")), 16, "L")))




                    'IO.File.ReadAllText(subPath).Replace("#FromDate#", clsCommon.myCstr(txtMilkReceiptFromDate.Text))
                    'IO.File.ReadAllText(subPath).Replace("#ToDate#", clsCommon.myCstr(txtMilkReciptToDate.Text))
                    'IO.File.ReadAllText(subPath).Replace("#TotalQuantity#", "1000.0")
                    Process.Start(subPath)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub rdbMilkReceipts_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMilkReceipts.CheckedChanged
        Try
            If rdbMilkReceipts.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Main Plant"
                rdbOther.Text = "Other"
                rdbMainPlant.Visible = True
                rdbOther.Visible = True
                MyLabel3.Visible = True
                MyLabel5.Visible = True
                txtMilkReciptToDate.Visible = True
                txtReciptMCC.Visible = True
                pnlSingleMCCCode.Visible = False
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtReciptMCC__My_Click(sender As Object, e As EventArgs) Handles txtReciptMCC._My_Click
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Plant First.", Me.Text)
                txtReciptMCC.arrValueMember = Nothing
                txtReciptMCC.arrDispalyMember = Nothing
                Return
            End If
            Dim qry As String = "select MCC_Code as Code ,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where plant_Code = '" + clsCommon.myCstr(fndLoc.Value) + "' "
            If rdbMilkReceipts.Checked = True Then
                qry = " select Code , Name from  (
                        select MCC_Code as Code ,MCC_NAME as  Name from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where plant_Code = '" + clsCommon.myCstr(fndLoc.Value) + "'
                        union
                        select TSPL_LOCATION_MASTER.Location_Code as [Code],TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" + clsCommon.myCstr(fndLoc.Value) + "'
                        ) XXXFinal "
            End If
            txtReciptMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCCAAA", qry, "Code", "NAME", txtReciptMCC.arrValueMember, txtReciptMCC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function MCCMilkRegisterQueryWithRejection(ByVal PlantCode As String, ByVal FromDate As Date, ByVal ToDate As Date, ByVal isMorningShift As Boolean, ByVal isEveningShift As Boolean, ByVal strMCCCode As String) As String
        Dim strShift As String = ""
        If isMorningShift = True AndAlso isEveningShift = True Then
            strShift = "'M','E'"
        ElseIf isMorningShift = False AndAlso isEveningShift = False Then
            strShift = "'M','E'"
        ElseIf isMorningShift = True AndAlso isEveningShift = False Then
            strShift = "'M'"
        ElseIf isMorningShift = False AndAlso isEveningShift = True Then
            strShift = "'E'"
        End If

        Dim BaseQuery As String = " select XXXFinal.[Plant Code] , XXXFinal.[Plant Name] , XXXFinal.[MCC Code],XXXFinal.[MCC Name],XXXFinal.[Vlc Uploader Code] , XXXFinal.[Vlc Code],XXXFinal.[VLC Name],XXXFinal.[Doc Date] , [Milk Type]+'M' as [Milk Type] , XXXFinal.RejectType,XXXFinal.Defaulter,  [Milk Weight(KG)] , [Milk Weight(LTR)] , [FAT(%)], [SNF(%)] , 
                                        case when len (XXXFinal.RejectType) = 0  then XXXFinal.[Milk Weight(KG)] else 0 end [Good Milk Qty(KGS)],
                                        case when len (XXXFinal.RejectType) = 0  then XXXFinal.[Milk Weight(LTR)] else 0 end [Good Milk Qty(LTR)],
                                        case when len (XXXFinal.RejectType) = 0  then XXXFinal.[FAT(%)] else 0 end [Good Milk FAT(%)],
                                        case when len (XXXFinal.RejectType) = 0  then XXXFinal.[SNF(%)] else 0 end [Good Milk SNF(%)],
                                        case when len (XXXFinal.RejectType) = 0  then XXXFinal.[FAT(KG)] else 0 end [Good Milk FAT KG],
                                        case when len (XXXFinal.RejectType) = 0  then XXXFinal.[SNF(KG)] else 0 end [Good Milk SNF KG],
                                        case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(KG)] else 0 end [Sour Qty(KG)],
                                        case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(LTR)] else 0 end [Sour Qty(LTR)],
                                        case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(%)] else 0 end [Sour FAT(%)],
                                        case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(%)] else 0 end [Sour SNF(%)],
                                        case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(KG)] else 0 end [Sour FAT KG],
                                        case when  (XXXFinal.RejectType) in ('SC' , 'SB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(KG)] else 0 end [Sour SNF KG],
                                        case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(KG)] else 0 end [Curd Qty(KG)],
                                        case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[Milk Weight(LTR)] else 0 end [Curd Qty(LTR)],
                                        case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(%)] else 0 end [Curd FAT(%)],
                                        case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(%)] else 0 end [Curd SNF(%)],
                                        case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[FAT(KG)] else 0 end [Curd FAT KG],
                                        case when  (XXXFinal.RejectType) in ('CC' , 'CB') and len (XXXFinal.Defaulter) >0  and XXXFinal.Defaulter <> 'Transporter' then XXXFinal.[SNF(KG)] else 0 end [Curd SNF KG],
                                        case when len (XXXFinal.RejectType) > 0 and len (XXXFinal.Defaulter) > 0 and XXXFinal.Defaulter = 'Transporter' then XXXFinal.[Milk Weight(KG)] else 0 end [PTC RECVRY], Shift 
                                        from (
                                        Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[MCC Type] ,final.[Chilling Center],final.[Plant Code],final.[Plant Name] ,final.Date ,final.[Doc Date] ,final.Shift , final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name], final.[Vendor Group Code],final.[Vendor Group Desc] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] , final.[Sample No] ,
final.[No Of Cans],final.Item_Code,final.Item_Desc,final.[Milk Weight],final.UOM_Code as [UOM],final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Cow Milk Qty (KG)],final.[Cow FAT(%)], Case When final.[FAT(%)] <= 5 Then CLR Else 0 End [Cow CLR],final.[Cow SNF(%)] , Case When final.[FAT(%)] <= 5 Then final.[FAT(KG)] Else 0 End [Cow FAT (KG)], Case When final.[FAT(%)] <= 5 Then final.[SNF(KG)] Else 0 End [Cow SNF (KG)],
final.[Buffalo Milk Qty (KG)], Case When final.[FAT(%)] > 5 Then CLR Else 0 End [Buffalo CLR],final.[Buffalo SNF(%)],final.[Buffalo FAT(%)], Case When final.[FAT(%)] > 5 Then final.[FAT(KG)] Else 0 End [Buffalo FAT (KG)],
Case When final.[FAT(%)] > 5 Then final.[SNF(KG)] Else 0 End [Buffalo SNF (KG)],final.[Milk Type],final.[SRN No],final.[SRN Amount], 
final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date , IS_MANUAL, MACHINE_NO,IS_MILK_SAMPLE_MANUAL,RejectType,RejectReason,Defaulter,  final.EMP_Amount,final.TIP_Amount,final.Service_Charge_Amount ,
([SRN Amount]+EMP_Amount+TIP_Amount-Service_Charge_Amount) as NetAmount,final.Purchase_Order_No,final.Head_Load_Amount ,final.SNF_Ded_Value,final.SNF_Ded_Rate,final.SNF_Ded_Amount, final.price_code,final.[Transporter Code],final.[Transporter Name],final.Handling_Charges_Amount,final.VSP_Commission_Amount,final.VSP_Deduction_Amount,final.VSP_Day_Wise_Incentive,final.SubStandard,final.vehicle  From ( Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,
TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,
TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,

Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type = 'C' Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], 
Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type = 'C' Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)], 
Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type = 'B' Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], 
Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type = 'B' Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], 
Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type = 'C' Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)], 
Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type = 'B' Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Buffalo Milk Qty (KG)]
                                        ,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Dock_Collection_Milk_Type   As [Milk Type] , TSPL_MILK_SRN_DETAIL.DOC_CODE As [Milk Receipt Code],
TSPL_MILK_SRN_DETAIL.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], 
Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code],
tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code],
TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name],
TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight],TSPL_MILK_SRN_DETAIL.UOM_Code, TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)], TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)],
TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)],
TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,   TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)],
Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = '' Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status],
TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no,
convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Weight as IS_MANUAL ,
'' as MACHINE_NO,(CASE WHEN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,
TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount ,

TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_type  as RejectType,'' as RejectReason,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Reject_Defaulter as Defaulter ,

TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount 
                                         ,TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code,[Transporter Code], [Transporter Name],isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount   ,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  as VSP_Commission_Amount,(isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply,0)*TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  as VSP_Deduction_Amount,TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive ,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard,0)=1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle 
                                         From TSPL_MILK_SRN_DETAIL 
                                         Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
                      										 left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No=TSPL_MILK_SRN_HEAD.DOC_CODE

                                         left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                                         Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE 
                                         Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                                         Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                                         left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                                         Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                                         left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master 
left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' 
left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                                         Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                                         
                                        left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code
                                        left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per
                                         left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  where 2 = 2  and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(FromDate), "dd/MMM/yyyy") + "'  and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate), "dd/MMM/yyyy") + "'  and TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No is null and TSPL_MILK_SRN_HEAD.MCC_Code  IN (select MCC_Code from TSPL_MCC_MASTER where Plant_Code = '" + PlantCode + "' " + IIf(MultipleFinderFillAuto = True, " ", " and MCC_Code = '" + strMCCCode + "' ") + " )   and TSPL_MILK_SRN_HEAD.SHIFT in ( " + strShift + ") 
                                         ) As Final where 2=2 
                                         )  XXXFinal   "

        Return BaseQuery
    End Function

    Private Sub rdbCheckList_CheckedChanged(sender As Object, e As EventArgs) Handles rdbCheckList.CheckedChanged
        Try
            If rdbCheckList.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Morning Shift"
                rdbOther.Text = "Evening Shift"
                'MyLabel3.Visible = False
                'MyLabel5.Visible = False
                'txtMilkReciptToDate.Visible = False
                rdbMainPlant.Visible = True
                rdbOther.Visible = True
                txtReciptMCC.Visible = False
                pnlSingleMCCCode.Visible = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub fndSingleMCCCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSingleMCCCode._MYValidating
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "First Select Shed", Me.Text)
            Return
        End If
        Dim qry As String = " Select MCC_Code , MCC_NAME  from TSPL_MCC_MASTER  "
        fndSingleMCCCode.Value = clsCommon.ShowSelectForm("MCCForCheckListReport", qry, "MCC_Code", "plant_code = '" + fndLoc.Value + "' ", fndSingleMCCCode.Value, "MCC_Code", isButtonClicked)
        lblSingleMCCName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select MCC_NAME from TSPL_MCC_MASTER where MCC_Code = '" + fndSingleMCCCode.Value + "' "))
    End Sub

    Private Sub rdbUnitWiseTotal_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUnitWiseTotal.CheckedChanged
        Try
            If rdbUnitWiseTotal.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Morning Shift"
                rdbOther.Text = "Evening Shift"
                rdbMainPlant.Visible = False
                rdbOther.Visible = False
                'MyLabel3.Visible = False
                'MyLabel5.Visible = False
                'txtMilkReciptToDate.Visible = False
                txtReciptMCC.Visible = False
                pnlSingleMCCCode.Visible = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbUnitWiseAnalysis_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUnitWiseAnalysis.CheckedChanged
        Try
            If rdbUnitWiseAnalysis.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Morning Shift"
                rdbOther.Text = "Evening Shift"
                rdbMainPlant.Visible = False
                rdbOther.Visible = False
                'MyLabel3.Visible = False
                'MyLabel5.Visible = False
                'txtMilkReciptToDate.Visible = False
                txtReciptMCC.Visible = False
                pnlSingleMCCCode.Visible = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbRouteBillsAbstract_CheckedChanged(sender As Object, e As EventArgs) Handles rdbRouteBillsAbstract.CheckedChanged
        Try
            If rdbRouteBillsAbstract.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Morning Shift"
                rdbOther.Text = "Evening Shift"
                rdbMainPlant.Visible = False
                rdbOther.Visible = False
                'MyLabel3.Visible = False
                'MyLabel5.Visible = False
                'txtMilkReciptToDate.Visible = False
                txtReciptMCC.Visible = False
                pnlSingleMCCCode.Visible = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbUnitWiseDeduction_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUnitWiseDeduction.CheckedChanged
        Try
            If rdbUnitWiseDeduction.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Morning Shift"
                rdbOther.Text = "Evening Shift"
                rdbMainPlant.Visible = False
                rdbOther.Visible = False
                'MyLabel3.Visible = False
                'MyLabel5.Visible = False
                'txtMilkReciptToDate.Visible = False
                txtReciptMCC.Visible = False
                pnlSingleMCCCode.Visible = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbTIPsummaryReportMCCandVLCwise_CheckedChanged(sender As Object, e As EventArgs) Handles rdbTIPsummaryReportMCCandVLCwise.CheckedChanged
        Try
            If rdbTIPsummaryReportMCCandVLCwise.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Morning Shift"
                rdbOther.Text = "Evening Shift"
                rdbMainPlant.Visible = False
                rdbOther.Visible = False
                'MyLabel3.Visible = False
                'MyLabel5.Visible = False
                'txtMilkReciptToDate.Visible = False
                txtReciptMCC.Visible = False
                pnlSingleMCCCode.Visible = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbUnitWiseBillSummary_CheckedChanged(sender As Object, e As EventArgs) Handles rdbUnitWiseBillSummary.CheckedChanged
        Try
            If rdbUnitWiseBillSummary.Checked = True Then
                pnlMilkReceipts.Visible = True
                'RadSplitButton1.Enabled = False
                'btnGo.Enabled = False
                rdbMainPlant.Text = "Morning Shift"
                rdbOther.Text = "Evening Shift"
                rdbMainPlant.Visible = False
                rdbOther.Visible = False
                'MyLabel3.Visible = False
                'MyLabel5.Visible = False
                'txtMilkReciptToDate.Visible = False
                txtReciptMCC.Visible = False
                pnlSingleMCCCode.Visible = True
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Else
                pnlMilkReceipts.Visible = False
                RadSplitButton1.Enabled = True
                btnGo.Enabled = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetFormateColumnValue(ByVal strValue As String, ByVal colFixLength As Integer, ByVal strSide As String) As String

        Dim collenth As Integer = clsCommon.myLen(strValue)
        Dim typeLength As Integer = colFixLength
        Dim collString = strValue
        If collenth < typeLength Then
            Dim balanceSpace As Integer = typeLength - collenth
            Dim ii As Integer
            For ii = 0 To balanceSpace - 1
                If clsCommon.CompairString(strSide, "L") = CompairStringResult.Equal Then
                    collString = " " + collString
                Else
                    collString = collString + " "
                End If
            Next
        End If
        Return collString
    End Function

    Private Sub RdbYearlyConsolidatedofMilkPayment_CheckedChanged(sender As Object, e As EventArgs) Handles rdbYearlyConsolidatedofMilkPayment.CheckedChanged
        Try
            If rdbYearlyConsolidatedofMilkPayment.Checked = True Then
                dtpToDate.ReadOnly = False
            Else
                dtpToDate.ReadOnly = True
                SetToDate()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RdbYearlyConsolidatedReportofMilkProcurement_CheckedChanged(sender As Object, e As EventArgs) Handles rdbYearlyConsolidatedReportofMilkProcurement.CheckedChanged
        Try
            If rdbYearlyConsolidatedReportofMilkProcurement.Checked = True Then
                dtpToDate.ReadOnly = False
            Else
                dtpToDate.ReadOnly = True
                SetToDate()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
