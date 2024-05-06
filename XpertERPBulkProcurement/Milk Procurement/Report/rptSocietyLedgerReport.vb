Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptSocietyLedgerReport
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    'Dim StrPermission As String
    'Dim dtREJECT As DataTable
    'Dim arrMCC As New ArrayList
    'Dim arrRoute As New ArrayList
    'Dim arrVLC As New ArrayList
    Private Sub rptSocietyLedgerReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        'StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        Reset()
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        EnableDisableCtrl(True)
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        TemplateGridview = Gv1
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try

            Dim dt1 As New DataTable
            Dim sQuery As String = Nothing
            Dim whrcls As String = " where 2=2 "
            Dim whrcls1 As String = " where 2=2 "

            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Location First.", Me.Text)
                fndLoc.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtVSP.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select VSP First.", Me.Text)
                txtVSP.Focus()
                Exit Sub
            End If

            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleFrom.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Payment Cycle From First.", Me.Text)
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            If clsCommon.myLen(txtPaymentCycleTo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Payment Cycle To First.", Me.Text)
                txtPaymentCycleTo.Focus()
                Exit Sub
            End If

            If clsCommon.myCdbl(txtPaymentCycleFrom.Value) > clsCommon.myCdbl(txtPaymentCycleTo.Value) Then
                common.clsCommon.MyMessageBoxShow(Me, "From Payment Cycle can not be greater then to Payment Cycle", Me.Text)
                txtPaymentCycleFrom.Focus()
                Exit Sub
            End If
            Patment_Cycle_changed()
            'arrMCC.Clear()
            'arrRoute.Clear()
            'arrVLC.Clear()
            'arrMCC.Add(fndMCC.Value)
            'arrRoute.Add(txtRouteNo.Value)
            'arrVLC.Add(txtVLC.Value)
            'qry = clsMilkRejectHead.GetMCCRegisterQuery(fromDate.Value, ToDate.Value, "M", "E", "", StrPermission, arrMCC, arrRoute, arrVLC, "")
            ',SUM(max(xx.Payable_Amount)) OVER (Partition BY xx.VSP_Code order by CycleNo) as OS
            whrcls += "  and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) <=convert(date,('" + ToDate.Value + "'),103) "
            whrcls += "  and TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE  in ('" & txtVSP.Value & "')"
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrcls += " and TSPL_LOCATION_MASTER.Loc_Segment_Code     IN ('" + fndLoc.Value + "') "
            End If
            whrcls += " and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS where TSPL_MILK_PURCHASE_INVOICE_PRO_LOSS.InvoiceNo=TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE) "

            whrcls1 += "  and convert(date,TSPL_PAYMENT_PROCESS_HEAD.From_Date,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) <=convert(date,('" + ToDate.Value + "'),103) "
            whrcls1 += "  and TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  in ('" & txtVSP.Value & "')"
            If clsCommon.myLen(fndLoc.Value) > 0 Then
                whrcls1 += " and TSPL_PAYMENT_PROCESS_Head.loc_seg_code    IN ('" + fndLoc.Value + "') "
            End If

            Dim BaseQry As String = ""
            BaseQry = "SELECT coalesce(PaymentProcess.Payable_Amount,0) as Payable_Amount,coalesce(PaymentProcess.Credit_Note_Amount,0)as Credit_Note_Amount,coalesce(PaymentProcess.Deduction_Amount,0) as Deduction_Amount,coalesce(PaymentProcess.Item_Issue_Amount,0)*(-1) as Item_Issue_Amount,coalesce(PaymentProcess.Item_Issue_Return_Amount,0) as Item_Issue_Return_Amount,coalesce(PaymentProcess.MCC_Sale_Amount,0)*(-1) as MCC_Sale_Amount ,coalesce(PaymentProcess.MCC_Sale_Return_Amount,0) as MCC_Sale_Return_Amount, TSPL_MCC_MASTER.add1 + TSPL_MCC_MASTER.add2 as addd,TSPL_MILK_SRN_DETAIL.UOM_Code,TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty "
            BaseQry += " ,TSPL_MILK_PURCHASE_INVOICE_DETAIL.AMOUNT as Net_AMOUNT,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_RO_Amount , TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_CODE , convert(varchar,TSPL_MILK_SRN_head.DOC_DATE,103) as DOC_DATE,TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE ,case when isnull(TSPL_MILK_SRN_HEAD.Against_reject_no,'')='' then TSPL_MILK_RECEIPT_HEAD.shift else TSPL_MILK_REJECT_head.shift end as SHIFT,"
            BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_MCC_ROUTE_MASTER .Route_Name  ,TSPL_MCC_MASTER .MCC_NAME ,case when isnull(TSPL_MILK_SAMPLE_DETAIL.TYPE,'')='' then 'Mix' else TSPL_MILK_SAMPLE_DETAIL.TYPE end as Type ,TSPL_MILK_SAMPLE_DETAIL.CLR,TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO ,TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,"
            BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Name ,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.TOTAL_PaymentCOMMISSION,0) as [EMP],coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.incentive_head,0) as Incentive,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_head_load_amount,0) as HEDAmt,coalesce(TSPL_MILK_PURCHASE_INVOICE_HEAD.total_Own_Asset_Amount,0) as AstAMT,coalesce(Total_dEDUCTION_AMOUNT,0) as DedAmt"
            BaseQry += " ,TSPL_VLC_MASTER_HEAD.Village_Code, case when TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER >= 5 then 'Buffalo' else 'Cow' end as CowBuffalo_Type " + Environment.NewLine +
            ",(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)) as SRN_Net_Amount
,TSPL_MILK_PURCHASE_INVOICE_HEAD.Total_Basic_AMOUNT "
            BaseQry += ",cast( case when  TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty=0 then 0 else (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))/TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty end as decimal(18,2)) as RATE
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER 
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as FATQTY
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else ( cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.FAT_PER*TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) ) end as decimal(18,2)) as FAT_Rate
,cast( round((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as FAT_Amount
,TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER
,round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 ) as SNFQTY 
,cast(case when round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1 )=0 then 0 else (cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer)/round(TSPL_MILK_PURCHASE_INVOICE_DETAIL.SNF_PER *TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty/100,2,1)) end as decimal(18,2)) as SNF_Rate
,cast(((TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0)))-round( (TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_Net_Amount+ isnull(TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,0))*isnull(TSPL_MILK_SRN_DETAIL.FAT_Ratio,0),0) as integer) as SNF_Amount
,TSPL_MILK_REJECT_DETAIL.Reject_Type  as QBD" + Environment.NewLine +
        " from TSPL_MILK_PURCHASE_INVOICE_DETAIL  " + Environment.NewLine + " Inner Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE =TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " + Environment.NewLine + " left outer join TSPL_MILK_SRN_HEAD  on TSPL_MILK_SRN_HEAD .DOC_CODE  =TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE " + Environment.NewLine
            BaseQry += " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.DOC_CODE =      TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE  " + Environment.NewLine + "  Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.DOC_CODE      = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE      = TSPL_MILK_SRN_HEAD.VLC_DOC_CODE  " + Environment.NewLine
            BaseQry += " left outer join TSPL_MILK_SRN_DETAIL   on TSPL_MILK_SRN_DETAIL .DOC_CODE  =TSPL_MILK_SRN_HEAD.DOC_CODE " + Environment.NewLine
            BaseQry += " left outer join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE =TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE " + Environment.NewLine + "  left outer join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.DOC_CODE =TSPL_MILK_RECEIPT_HEAD.DOC_CODE and   TSPL_MILK_SRN_HEAD.vlc_doc_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE " + Environment.NewLine + " Left Outer Join TSPL_VENDOR_MASTER On"
            BaseQry += " TSPL_MILK_PURCHASE_INVOICE_HEAD.VSP_CODE =TSPL_VENDOR_MASTER.Vendor_Code And TSPL_VENDOR_MASTER.Form_Type = 'VSP'  " + Environment.NewLine + " Left Outer Join TSPL_MCC_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD .MCC_CODE = TSPL_MCC_MASTER.MCC_Code  " + Environment.NewLine + " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_MILK_PURCHASE_INVOICE_HEAD.MCC_Code Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MILK_PURCHASE_INVOICE_HEAD.ROUTE_CODE =TSPL_MCC_ROUTE_MASTER.Route_Code " + Environment.NewLine + " left outer join TSPL_VLC_MASTER_HEAD on"
            BaseQry += " TSPL_VLC_MASTER_HEAD.VLC_Code =TSPL_MILK_PURCHASE_INVOICE_DETAIL.VLC_NO  " + Environment.NewLine
            BaseQry += " left join TSPL_CITY_MASTER  as MCC_City on MCC_City.city_code=TSPL_MCC_MASTER.City_code " + Environment.NewLine
            BaseQry += " left join TSPL_STATE_MASTER as MCC_State on MCC_State.STATE_CODE =TSPL_MCC_MASTER.State_Code " + Environment.NewLine
            BaseQry += " left join (select TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Incentive_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Vsp_Own_System_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Total_EMP_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Head_Load_Amount , TSPL_VLC_MASTER_HEAD.VLC_Code  ,TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE ,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount,TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Deduction_Amount+TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount as Deduction_Amount  ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Return_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.Item_Issue_Amount,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Amount ,TSPL_PAYMENT_PROCESS_DETAIL.MCC_Sale_Return_Amount  from TSPL_PAYMENT_PROCESS_DETAIL"
            BaseQry += " left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No =TSPL_PAYMENT_PROCESS_DETAIL.Doc_No"
            BaseQry += " left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader =TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader " & whrcls1 & ""
            BaseQry += " ) as PaymentProcess on "
            BaseQry += "  PaymentProcess.vsp_code = TSPL_MILK_PURCHASE_INVOICE_Head.vsp_code And PaymentProcess.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_Code and TSPL_MILK_PURCHASE_INVOICE_HEAD.Doc_Code=PaymentProcess.Milk_Purchase_Invoice_No " + Environment.NewLine +
        "  left join TSPL_MILK_REJECT_head on TSPL_MILK_REJECT_head.doc_code=TSPL_MILK_SRN_HEAD.Against_reject_no
          left outer join  TSPL_MILK_REJECT_DETAIL on TSPL_MILK_REJECT_DETAIL.DOC_CODE = TSPL_MILK_REJECT_head.DOC_CODE and TSPL_VLC_MASTER_HEAD.VLC_Code  = TSPL_MILK_REJECT_DETAIL.VLC_CODE  and TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT = TSPL_MILK_PURCHASE_INVOICE_DETAIL.Qty     
          "
            BaseQry += "  " & whrcls & " "

            sQuery = "select CycleNo as [CY],xx.mcc_code as [MCC Code],Max(xx.mcc_name) as [MCC Name],xx.Route_code as [Route Code],max(xx.Route_Name) as [Route Name]
                    ,xx.VSP_Code as [VSP Code],xx.vlc_code_vlc_uploader As [Society Code],max(xx.Vendor_Name) as [Society Name]
                    ,sum(case When isnull(xx.Shift,'')='M' then xx.Qty else 0 end) as [Milk Qty Morning]
                    ,sum(case When isnull(xx.Shift,'')='E' then xx.Qty else 0 end) as [Milk Qty Evening]
                    ,sum(xx.Qty) as [Milk Qty Total]
                    ,sum(case When isnull(xx.CowBuffalo_Type,'')='Buffalo' then xx.Qty else 0 end) as [Milk B]
                    ,sum(case When isnull(xx.CowBuffalo_Type,'')='Cow' then xx.Qty else 0 end) as [Milk C]
                    ,cast(sum(xx.FATQTY) as decimal(18,4)) as [KG FAT] 
                    ,cast(sum(xx.SNFQTY)as decimal(18,4)) as [KG SNF]
                    ,cast(case when sum(xx.Qty)=0 then 0 else (sum(xx.FATQTY)/sum(xx.Qty))*100 end as decimal(18,2)) as [FAT(%)]
                    ,cast(case when sum(xx.Qty)=0 then 0 else (sum(xx.SNFQTY)/sum(xx.Qty))*100 end as decimal(18,2)) as [SNF(%)]
                    ,sum(xx.Net_AMOUNT) as [Milk Val],0.00 as [Hold V],max(xx.Credit_Note_Amount) as [Commission],0.00 as [Share A]
                    ,sum(xx.Net_AMOUNT)+max(xx.Credit_Note_Amount) as [Net Val],max(xx.Deduction_Amount) as [Total Deduction],sum(xx.Net_AMOUNT)+max(xx.Credit_Note_Amount)-max(xx.Deduction_Amount) as [Net Paid]
                    ,0.00 as OS
                    from
                    (select case when convert(date,final.[DOC_DATE],103)>=convert(date,TSPL_PAYMENT_CYCLE_GENERATED.From_Date,103) 
                    and convert(date,final.[DOC_DATE],103) <= convert(date,TSPL_PAYMENT_CYCLE_GENERATED.To_Date,103) then 
                    TSPL_PAYMENT_CYCLE_GENERATED.Name else '0' end as CycleNo,
                    final.* from (" + BaseQry + " )final	
            left join TSPL_PAYMENT_CYCLE_GENERATED on TSPL_PAYMENT_CYCLE_GENERATED.mcc_code=final.MCC_code	
            )xx where xx.CycleNo>0	GROUP BY xx.mcc_code ,xx.Route_code,xx.VSP_Code ,xx.vlc_code_vlc_uploader,CycleNo order by CycleNo"

            dt1 = Nothing
            dt1 = clsDBFuncationality.GetDataTable(sQuery)
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt1 Is Nothing OrElse dt1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                Gv1.DataSource = dt1
                RadPageView1.SelectedPage = RadPageViewPage2
                EnableDisableCtrl(False)
                SetGridFormat()
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFiscalYear.Enabled = val
        txtPaymentCycleFrom.Enabled = val
        txtPaymentCycleTo.Enabled = val
        fndLoc.Enabled = val
        txtRouteNo.Enabled = val
        txtVSP.Enabled = val
    End Sub
    Sub SetGridFormat()

        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next

        Gv1.Columns("MCC Code").IsVisible = False
        Gv1.Columns("Route Code").IsVisible = False
        Gv1.Columns("VSP Code").IsVisible = False

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim Cycle As New GridViewSummaryItem("CY", "Total", GridAggregateFunction.Max)
            summaryRowItem.Add(Cycle)

            Dim summaryItemFAT As New GridViewSummaryItem()
            summaryItemFAT.FormatString = "{0:n2}"
            summaryItemFAT.Name = "FAT(%)"
            summaryItemFAT.AggregateExpression = "sum([KG FAT])*100/sum([Milk Qty Total])"
            summaryRowItem.Add(summaryItemFAT)

            Dim summaryItemSNF As New GridViewSummaryItem()
            summaryItemSNF.FormatString = "{0:n2}"
            summaryItemSNF.Name = "SNF(%)"
            summaryItemSNF.AggregateExpression = "sum([KG SNF])*100/sum([Milk Qty Total])"
            summaryRowItem.Add(summaryItemSNF)

        For i As Integer = 8 To Gv1.Columns.Count - 2
            Dim aa = Gv1.Columns(i).HeaderText()
            If clsCommon.CompairString(aa, "FAT(%)") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(aa, "SNF(%)") <> CompairStringResult.Equal Then
                Dim summaryItem1 As New GridViewSummaryItem()
                summaryItem1.FormatString = "{0:n2}"
                summaryItem1.Name = aa
                summaryItem1.AggregateExpression = "sum([" + aa + "])"
                summaryRowItem.Add(summaryItem1)
            End If
        Next

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv1.AutoSizeRows = True
            Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr("Society Ledger For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Name : " & "Milk Bill Procurement Summary For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value)
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            'If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            'End If
            'arrHeader.Add("MCC : " + txtmccode.Value)
            'arrHeader.Add("Fiscal Year : " + txtFiscalYear.Value)
            'arrHeader.Add("Payment Cycle : " + txtPaymentCycleFrom.Value)
            arrHeader.Add("Mcc : " + lblMCC.Text)
            arrHeader.Add("Route : " + lblRouteDesc.Text)
            arrHeader.Add("Society : " + lblVSPDesc.Text)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code as [MCC Code],MCC_NAME as [MCC Name],TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MBPSMCC", qry, "MCC Code", "MCC Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    'Private Sub Txtmccode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim whrcls As String = ""

    '    txtmccode.Value = clsMccMaster.getFinder(whrcls, txtmccode.Value, isButtonClicked)
    '    If clsCommon.myLen(txtmccode.Value) > 0 Then
    '        lblmccname.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + txtmccode.Value + "'"))
    '    Else
    '        txtmccode.Value = ""
    '        lblmccname.Text = ""
    '    End If
    'End Sub

    Private Sub TxtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("LRFY", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleFrom__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleFrom._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleFrom.Value = clsCommon.ShowSelectForm("LRPCF", qry, "Code", whrcls, txtPaymentCycleFrom.Value, "Code", isButtonClicked)

            If clsCommon.myLen(txtPaymentCycleFrom.Value) > 0 Then
                txtPaymentCycleTo.Value = txtPaymentCycleFrom.Value
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try

    End Sub

    Private Sub Patment_Cycle_changed()
        Try
            Dim dt As DataTable
            Dim qry As String = "SELECT Name ,From_Date,To_Date FROM TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Payment Cycle found for Selected Fiscal Year", Me.Text)
                Exit Sub
            End If

            fromDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select From_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleFrom.Value + "' "))
            ToDate.Value = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select To_Date from TSPL_PAYMENT_CYCLE_GENERATED where Fiscal_Code='" + txtFiscalYear.Value + "' and Name='" + txtPaymentCycleTo.Value + "' "))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub TxtPaymentCycleTo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPaymentCycleTo._MYValidating
        Try
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Fiscal Year First.", Me.Text)
                txtFiscalYear.Focus()
                Exit Sub
            End If
            Dim whrcls As String = " Fiscal_Code='" + txtFiscalYear.Value + "' "
            Dim qry As String = "SELECT distinct convert(int,name) as Code,convert(varchar,From_Date,103) as [From Date],convert(varchar,To_Date,103) as [To Date] FROM TSPL_PAYMENT_CYCLE_GENERATED"
            txtPaymentCycleTo.Value = clsCommon.ShowSelectForm("LRPCT", qry, "Code", whrcls, txtPaymentCycleTo.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr("Society Ledger For Cycle : " + txtPaymentCycleFrom.Value + " To " + txtPaymentCycleTo.Value + ", " + txtFiscalYear.Value + "")
            Dim arrHeader As List(Of String) = New List(Of String)()

            'If txtMCC.arrDispalyMember IsNot Nothing AndAlso txtMCC.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Mcc : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
            'End If
            arrHeader.Add("Mcc : " + lblMCC.Text)
            arrHeader.Add("Route : " + lblRouteDesc.Text)
            arrHeader.Add("Society : " + lblVSPDesc.Text)
            clsCommon.MyExportToExcelGrid(strHeading, Gv1, arrHeader, Me.Text, True)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub fndMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndMCC._MYValidating
        Try
            Dim qry As String = "select MCC_Code as Code,MCC_NAME as Name,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
            fndMCC.Value = clsCommon.ShowSelectForm("slMccm", qry, "Code", "", fndMCC.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndMCC.Value) > 0 Then
                txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select mcc_name from tspl_mcc_master where mcc_code='" + fndMCC.Value + "'"))
                txtRouteNo.Value = ""
                lblRouteDesc.Text = ""
                txtVSP.Value = ""
                lblVSPDesc.Text = ""
                lblUploaderCode.Text = ""
            Else
                fndMCC.Value = ""
                txtLocName.Text = ""
                txtRouteNo.Value = ""
                lblRouteDesc.Text = ""
                txtVSP.Value = ""
                lblVSPDesc.Text = ""
                lblUploaderCode.Text = ""
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Location First.", Me.Text)
                fndLoc.Focus()
                Exit Sub
            End If
            Dim qry As String = "Select Route_code as Code,Route_Name as Description from TSPL_MCC_ROUTE_MASTER"
            txtRouteNo.Value = clsCommon.ShowSelectForm("SLRouteFinder", qry, "Code", "mcc_code='" + fndMCC.Value + "'", txtRouteNo.Value, "Route_code", isButtonClicked)

            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                lblRouteDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ROUTE_NAME from TSPL_MCC_ROUTE_MASTER where route_code='" + txtRouteNo.Value + "'"))
                txtVSP.Value = ""
                lblVSPDesc.Text = ""
                lblUploaderCode.Text = ""
            Else
                txtRouteNo.Value = ""
                lblRouteDesc.Text = ""
                txtVSP.Value = ""
                lblVSPDesc.Text = ""
                lblUploaderCode.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVSP__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVSP._MYValidating
        Try
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Route First.", Me.Text)
                txtRouteNo.Focus()
                Exit Sub
            End If
            Dim qry As String
            qry = "  select Cust_Code as 'Code' , Customer_Name as Name , TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code] from TSPL_CUSTOMER_MASTER left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_CUSTOMER_MASTER.Cust_Code "
            txtVSP.Value = clsCommon.ShowSelectForm("SLVSPFinder", qry, "Code", "Cust_Group_Code = 'VSP'", txtVSP.Value, "Cust_Code", isButtonClicked)

            If clsCommon.myLen(txtVSP.Value) > 0 Then
                lblVSPDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where cust_code='" + txtVSP.Value + "'"))
                lblUploaderCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD where Vsp_Code='" + txtVSP.Value + "'"))
            Else
                txtVSP.Value = ""
                lblVSPDesc.Text = ""
                lblUploaderCode.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtVLC._MYValidating
        Try
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Plz Select Route First.", Me.Text)
                txtRouteNo.Focus()
                Exit Sub
            End If

            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code as Code,TSPL_VLC_MASTER_HEAD.VLC_Name as Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code"
            Dim StrWhere As String = " TSPL_VLC_MASTER_HEAD.Active='1' and " + " TSPL_VLC_MASTER_HEAD.Route_Code='" + txtRouteNo.Value + "'"
            txtVLC.Value = clsCommon.ShowSelectForm("SLVLCFinder", qry, "Code", StrWhere, txtVLC.Value, "VLC_Code", isButtonClicked)

            If clsCommon.myLen(txtVLC.Value) > 0 Then
                lblVSPDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code='" + txtVLC.Value + "'"))
                lblUploaderCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Code_VLC_Uploader from TSPL_VLC_MASTER_HEAD where VLC_Code='" + txtVLC.Value + "'"))
            Else
                txtVLC.Value = ""
                lblVSPDesc.Text = ""
                lblUploaderCode.Text = ""
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Try
            Dim whrCls As String = " 1=1 "
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
                End If
            End If

            whrCls = whrCls & " and   Rejected_Type='N' and Location_Category='MCC'"
            Dim dr As DataRow = clsLocation.getLocSegFinderFullRow(whrCls)
            If dr Is Nothing OrElse dr.ItemArray.Count <= 0 Then
                fndLoc.Value = ""
                fndMCC.Value = ""
                lblMCC.Text = ""
                txtLocName.Text = ""
                txtRouteNo.Value = ""
                lblRouteDesc.Text = ""
                txtVSP.Value = ""
                lblVSPDesc.Text = ""
                lblUploaderCode.Text = ""
                Exit Sub
            End If

            fndLoc.Value = clsCommon.myCstr(dr("LocationSegmentCode"))
            txtLocName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Description  from TSPL_GL_SEGMENT_CODE WHERE  Segment_code='" & fndLoc.Value & "' "))
            fndMCC.Value = clsCommon.myCstr(dr("Code"))
            lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + fndMCC.Value + "'"))
            txtRouteNo.Value = ""
            lblRouteDesc.Text = ""
            txtVSP.Value = ""
            lblVSPDesc.Text = ""
            lblUploaderCode.Text = ""
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
End Class
