Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===========Created by Sanjay==================
Public Class RptTankerDispatchvsAckn
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    'Dim arrLoc As String = Nothing

    'Private Sub LOCATIONRIGTHS()
    '    Try
    '        Dim obj As New clsMCCCodes()
    '        obj = clsMCCCodes.GetData()

    '        If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
    '            arrLoc = obj.arrLocCodes
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    'Sub LoadMCC()
    '    Dim qry As String = Nothing
    '    If clsCommon.myLen(arrLoc) > 0 Then

    '        qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

    '    Else
    '        btnGo.Enabled = False
    '    End If

    '    'cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    'cbgMCC.ValueMember = "Code"
    '    'cbgMCC.DisplayMember = "Name"

    'End Sub


    '===================================================================

    Sub FormatGrid()

        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).Width = 100
            gv.Columns(ii).IsVisible = True
        Next

        gv.Columns("Dispatch Location Code").HeaderText = "Location Code"
        gv.Columns("Dispatch Location Name").HeaderText = "Location Name"
        gv.Columns("Dispatch Date").HeaderText = "Date"
        gv.Columns("Dispatch Tanker No").HeaderText = "Tanker No"
        gv.Columns("Dispatch Qty").HeaderText = "Qty"
        gv.Columns("Dispatch FAT Per").HeaderText = "FAT Per"
        gv.Columns("Dispatch SNF Per").HeaderText = "SNF Per"
        gv.Columns("Dispatch FAT Kg").HeaderText = "FAT Kg"
        gv.Columns("Dispatch SNF Kg").HeaderText = "SNF Kg"
        gv.Columns("Dispatch FAT Amt").HeaderText = "FAT Amt"
        gv.Columns("Dispatch SNF Amt").HeaderText = "SNF Amt"
        gv.Columns("Dispatch Amount").HeaderText = "Amount"

        gv.Columns("Received Location Code").HeaderText = "Location Code"
        gv.Columns("Received Location Name").HeaderText = "Location Name"
        gv.Columns("Received Date").HeaderText = "Date"
        gv.Columns("Received Tanker No").HeaderText = "Tanker No"
        gv.Columns("Received Qty").HeaderText = "Qty"
        gv.Columns("Received FAT Per").HeaderText = "FAT Per"
        gv.Columns("Received SNF Per").HeaderText = "SNF Per"
        gv.Columns("Received FAT Kg").HeaderText = "FAT Kg"
        gv.Columns("Received SNF Kg").HeaderText = "SNF Kg"
        gv.Columns("Received FAT Amt").HeaderText = "FAT Amt"
        gv.Columns("Received SNF Amt").HeaderText = "SNF Amt"
        gv.Columns("Received Amount").HeaderText = "Amount"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim item21 As New GridViewSummaryItem("Dispatch Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("Dispatch FAT Kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)
        Dim item23 As New GridViewSummaryItem("Dispatch SNF Kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item23)
        Dim item24 As New GridViewSummaryItem("Dispatch FAT Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item24)
        Dim item25 As New GridViewSummaryItem("Dispatch SNF Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item25)
        Dim item26 As New GridViewSummaryItem("Dispatch Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item26)


        Dim item11 As New GridViewSummaryItem("Received Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("Received FAT Kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("Received SNF Kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("Received FAT Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("Received SNF Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("Received Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)


        Dim item1 As New GridViewSummaryItem("Diff Qty", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Diff FAT Kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Diff SNF Kg", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Diff FAT Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Diff SNF Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Diff Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        Try
            RadPageView1.SelectedPage = RadPageViewPage1
            'chkDetail.IsChecked = True
            'LoadMCC()
            EnableDisableControl(True)
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            fndLoc.Value = ""
            txtLocName.Text = ""
            txtMCC.arrValueMember = Nothing
            gv.DataSource = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    Private Sub LoadData()
        Try


            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            If clsCommon.myLen(fndLoc.Value) <= 0 Then
                fndLoc.Focus()
                Throw New Exception("Please select Plant")
            End If
            Dim sQuery As String = ""


            'sQuery = "select ROW_NUMBER() OVER( ORDER BY YY.Chalan_NO) AS SNo,
            '    YY.DispatchLocationCode AS [Dispatch Location Code] ,YY.DispatchLocationNAME AS [Dispatch Location Name] ,YY.Dispatch_Date AS [Dispatch Date]
            '     ,coalesce(DC_Final.dispatch_no,YY.Chalan_NO) as [Dispatch No]
            '     ,YY.Desp_Tanker_No  as [Dispatch Tanker No] ,convert(decimal(18,2),YY.Desp_Qty) as [Dispatch Qty] ,convert(decimal(18,2),YY.Desp_FAT) as [Dispatch FAT Per]
            '     ,convert(decimal(18,2),YY.desp_SNF) as [Dispatch SNF Per] ,convert(decimal(18,3),YY.DESP_FAT_Kg) as [Dispatch FAT Kg] 
            '     ,convert(decimal(18,3),YY.DESP_SNF_Kg)  as [Dispatch SNF Kg]
            '     ,YY.Disp_FAT_Amt AS [Dispatch FAT Amt]
            '     ,YY.Disp_SNF_Amt AS [Dispatch SNF Amt]
            '     ,YY.DESP_AMOUNT AS [Dispatch Amount]
            '     ,YY.RECDLocationCode AS [Received Location Code] ,YY.RECDLocationNAME AS [Received Location Name] ,YY.Receipt_challan_Date AS [Received Date]
            '     ,YY.Receipt_challan_no as [Milk Transfer In No]
            '     ,YY.Receipt_Tanker_No  as [Received Tanker No] 
            '     ,convert(decimal(18,2),YY.RECDQTY) as [Received Qty] ,convert(decimal(18,2),YY.RECDFATPer) as [Received FAT Per]
            '     ,convert(decimal(18,2),YY.RECDSNFPer) as [Received SNF Per] ,convert(decimal(18,3),YY.RECDFATKg) as [Received FAT Kg] 
            '     ,convert(decimal(18,3),YY.RECDSNFKg)  as [Received SNF Kg]
            '     ,YY.RECDFATAmt AS [Received FAT Amt]
            '     ,YY.RECDSNFAmt AS [Received SNF Amt]
            '     ,YY.RECDAMOUNT AS [Received Amount]
            '    ,convert(decimal(18,2),YY.Desp_Qty-ISNULL(YY.RECDQTY,0)) as [Diff Qty] 
            '     ,convert(decimal(18,3),YY.DESP_FAT_Kg-ISNULL(YY.RECDFATKg,0)) as [Diff FAT Kg] 
            '     ,convert(decimal(18,3),YY.DESP_SNF_Kg-ISNULL(YY.RECDSNFKg,0))  as [Diff SNF Kg]
            '     ,convert(decimal(18,2),YY.Disp_FAT_Amt-YY.RECDFATAmt) AS [Diff FAT Amt]
            '     ,convert(decimal(18,2),YY.Disp_SNF_Amt-YY.RECDSNFAmt) AS [Diff SNF Amt]
            '     ,convert(decimal(18,2),YY.DESP_AMOUNT-YY.RECDAMOUNT) AS [Diff Amount]
            '    from  (select xx.*
            '    ,xx.RECDFATPer   *xx.RECDQTY  /100 as RECDFATKg,xx.RECDSNFPer   *RECDQTY   /100 as RECDSNFKg--,(xx.Desp_Qty -xx.RECDQTY)  as DiffQty
            '    from  (select TSPL_MCC_DISPATCH_CHALLAN_DETAIL.sno as line_no ,coalesce(FromMcc.mcc_code,FromLocation.location_code) as DispatchLocationCode,coalesce(FromMcc.mcc_name,FromLocation.location_desc) as  DispatchLocationName 
            '     ,convert(varchar ,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) as  Dispatch_Date
            '     ,TSPL_MCC_Dispatch_Challan.Chalan_NO
            '     ,TSPL_MCC_Dispatch_Challan.Tanker_No AS Desp_Tanker_No
            '     , TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG  as Desp_Qty
            '     ,convert(float,DispFAT .Param_Field_Value) as Desp_FAT ,convert(float,DispSNF .Param_Field_Value) as desp_SNF
            '     , TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG  as Desp_FAT_KG
            '     , TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG  as Desp_SNF_KG
            '     ,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG*TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE AS Disp_FAT_Amt
            '     ,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG*TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE AS Disp_SNF_Amt
            '     , TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount  as Desp_Amount
            '    ,coalesce(ToMcc.mcc_code,ToLocation.location_code) as RECDLocationCode
            '    ,coalesce(ToMcc.mcc_name,ToLocation.location_desc) as  RECDLocationName 
            '    ,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date
            '     ,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No
            '     ,TSPL_Weighment_Detail.Tanker_No AS Receipt_Tanker_No
            '     ,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight  as RECDQTY
            '     ,QcFat.Param_Field_Value as RECDFATPer,Qcsnf.Param_Field_Value as RECDSNFPer
            '     ,0 AS RECDFATAmt
            '     ,0 AS RECDSNFAmt
            '     ,(case when TSPL_Weighment_Chember_Details.ch_amount>0 then TSPL_Weighment_Chember_Details.ch_amount else TSPL_Weighment_Detail.amount end) as RECDAMOUNT
            '      from TSPL_MCC_Dispatch_Challan left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  
            '     left outer join TSPL_MCC_MASTER  FromMcc on FromMcc .MCC_Code =TSPL_MCC_Dispatch_Challan.MCC_Code
            '      left outer join TSPL_LOCATION_MASTER FromLocation on FromLocation.LOCATION_Code =TSPL_MCC_Dispatch_Challan.MCC_Code
            '     left outer join TSPL_MILK_TRANSFER_IN on 
            '       TSPL_MILK_TRANSFER_IN.dispatch_challan_no=TSPL_MCC_Dispatch_Challan.Chalan_NO
            '       left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No=TSPL_MILK_TRANSFER_IN.Qc_No
            '      left outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Weighment_No =TSPL_MILK_TRANSFER_IN.Weighment_No   left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No=TSPL_Weighment_Detail.Weighment_No and TSPL_WEIGHMENT_CHEMBER_DETAILS.line_no= TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no 
            '      left outer join TSPL_MCC_MASTER  ToMcc on ToMcc.MCC_Code =TSPL_MILK_TRANSFER_IN.location_code
            '      left outer join TSPL_LOCATION_MASTER ToLocation on ToLocation.LOCATION_Code =TSPL_MILK_TRANSFER_IN.location_code
            '    left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail DispFat on DispFat.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO and DispFat.Param_Type='FAT' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=DispFat.sno
            '    left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail DispSNF on Dispsnf.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO and DispSNF .Param_Type='SNF' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=DispSNF.sno
            '    left outer join TSPL_QC_Parameter_Detail QcFat on QcFat.QC_No=TSPL_QUALITY_CHECK.QC_No  and QcFat .Param_Type='FAT' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=QcFat.line_no 
            '    left outer join TSPL_QC_Parameter_Detail Qcsnf on Qcsnf .QC_No=TSPL_QUALITY_CHECK.QC_No and Qcsnf  .Param_Type='SNF' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=Qcsnf.line_no
            '    where 2 = 2 "
            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    sQuery += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + clsCommon.GetMulcallString(txtMCC.arrValueMember) + " )"
            'Else
            '    Dim StrMCCQRY As String = "DECLARE @temp VARCHAR(MAX) SET @temp =(SELECT ',''' + cast(Code as varchar) + '''' from (select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master where Plant_Code='" & fndLoc.Value & "' union all select TSPL_LOCATION_MASTER.Location_Code as [Code],TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" & fndLoc.Value & "')xx order by Code FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS MCC "
            '    Dim StrMCC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrMCCQRY))
            '    sQuery += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + StrMCC + " )"
            'End If
            'sQuery += " and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
            'sQuery += " ) as xx) as YY "
            'sQuery += " left outer join ( SELECT TSPL_MCC_Dispatch_Challan_detail.chalan_no,coalesce(TSPL_MCC_Dispatch_Challan_detail.intermittent_dispatch_no,TSPL_MCC_Dispatch_Challan_detail.chalan_no) as dispatch_no ,SNO FROM TSPL_MCC_Dispatch_Challan_detail)DC_Final ON DC_Final.chalan_no=YY.Chalan_NO AND DC_Final.SNO=YY.line_no "
            'sQuery += " order by convert(date,Dispatch_Date ,103),YY.Chalan_NO"

            sQuery = "with WholeData as (Select  DISTINCT Line_No as Line_No_a, xx.[To MCC or Plant Code]
, xx.DocType,xx.[Vendor Code],xx.[Vendor Name],coalesce(DC_Final.dispatch_no,xx.[Challan No]) AS [Challan No],xx.[Challan Date], coalesce(xx.[SRN No],'') AS [SRN No],  xx.[SRN Status] as [SRN Status],  coalesce(xx.[SRN Date],'' ) as [SRN Date],xx.[Invoice No],xx.[Invoice Date],xx.[Tanker No],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Weighment No],xx.[Weighment Date],  xx.[Milk Receipt Challan No],xx.[Milk Receipt Challan Date],xx.[Challan Qty],xx.[Gross Weight],xx.[Tare Weight],xx.[Tare Date],xx.[Net Weight],  xx.MIKL_TYPE_CODE as [Milk Type],xx.Milk_Grade_code as [Grade Code],xx.GRADE_TYPE as [Grade Type], 
xx.[From MCC or Plant Code],xx.[From MCC or Plant Name], xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No], coalesce(xx.[Secondary QC No],'') as [Secondary QC No], XX.[Unloading Date Time],  XX.[QC Date Time],XX.STATUS,xx.[Unloading No],xx.[Cleaning No],xx.[MCC Name],xx.Plant,xx.[Silo Code],xx.[Silo Desc],xx.[Gate Out No],xx.[Gate Out Date Time],  xx.[FAT %],xx.[SNF %],  xx.[Standard Rate],xx.[Basic Rate], xx.incentive , xx.Deduction, xx.[Special Deduction] , xx.[Net Rate],  coalesce(xx.[FAT Rate],0) as [FAT Rate], coalesce(xx.[SNF Rate],0) as [SNF Rate],  case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total Amount],xx.[FAT Weightage & SNF Weightage],  xx.[FAT Ratio & SNF ratio],xx.[Vendor Class] , case when SRN_Return_NO is not Null then 'SRN Return' else '' end as [SRN Return],  Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG, Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG,Batch_No as [Batch No],TSPL_SHIFT_MASTER.SHIFT_NAME as [Shift Name],xx.allocatetomcc as [Allocate To MCC Code],tspl_mcc_master.MCC_NAME as [Allocate To MCC Name],TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO as [Allocated Tanker Gate Out No] , convert(varchar, TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE,103)+' '+left(convert(varchar, TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE,114),5)  as  [Allocated Tanker Gate Out Date],TSPL_MCC_TANKER_GATE_OUT.TANKER_NO as [Allocated Tanker No],TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No as [Security Gate Out No],convert(varchar, TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_Date,103)+' '+ left( convert(varchar, TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_Date,114),5)  as [Security Gate Out Date],TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO as [Tanker Dispatch No],convert(varchar, TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date,103)+' '+left( convert(varchar, TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date,114),5) [Tanker Dispatch Date] ,[FAT Amt],[SNF Amt]
,Receipt_Challan_Date,Receipt_Challan_No,Receipt_Tanker_No,RECDQTY,RECDFATPer,RECDSNFPer,RECDFATKg,RECDSNFKg,RECDFATAmt,RECDSNFAmt,RECDAMOUNT
From ( Select TSPL_Weighment_Detail.WEIGHMENT_NO,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date
,TSPL_MILK_TRANSFER_IN.Receipt_Challan_No,TSPL_Weighment_Detail.Tanker_No AS Receipt_Tanker_No,TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight  as RECDQTY
,TSPL_Quality_Chember_Details.fat_per as RECDFATPer,TSPL_Quality_Chember_Details.snf_per as RECDSNFPer,TSPL_WEIGHMENT_CHEMBER_DETAILS.CH_FAT_Kg AS RECDFATKg
,TSPL_WEIGHMENT_CHEMBER_DETAILS.CH_SNF_Kg AS RECDSNFKg,TSPL_WEIGHMENT_CHEMBER_DETAILS.ch_fat_value AS RECDFATAmt,TSPL_WEIGHMENT_CHEMBER_DETAILS.ch_snf_value AS RECDSNFAmt
,(case when TSPL_Weighment_Chember_Details.ch_amount>0 then TSPL_Weighment_Chember_Details.ch_amount else TSPL_Weighment_Detail.amount end) as RECDAMOUNT,
TSPL_Milk_unloading_Chember_Details.Batch_No,TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE
,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType,Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End
 As [Milk Receipt Challan No],   Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) + ' ' + Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,108) As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No],  Convert(varchar,TSPL_MCC_Dispatch_Challan.DISPATCH_DATE,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status],  Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) + ' ' + Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,108) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], 
 TSPL_MCC_Dispatch_Challan.Tanker_No
 As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Weighment_date,108) As [Weighment Date],   Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) + ' ' +  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,108) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_MCC_Dispatch_Challan_detail.qty_kg As [Challan Qty]
 ,   TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],
Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No],  Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) + ' ' + Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,108) As [Unloading Date Time],   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108)  As [QC Date Time],  Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No],TSPL_Cleaning .Doc_No as [Cleaning No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108) As [Gate Out Date Time],  Convert(decimal(18,3)
 ,t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,3),t_SNF.Param_Field_Value) As [SNF %]
  , TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp]
  ,  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]  ,TSPL_QUALITY_CHECK.Shift_Code,TSPL_Gate_Out.allocatetomcc From Tspl_Gate_Entry_Details  left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_WEIGHMENT_CHEMBER_DETAILS.line_no  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_QUALITY_CHEMBER_DETAILS.line_no left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Milk_unloading_Chember_Details On TSPL_MILK_UNLOADING.Unloading_No = TSPL_Milk_unloading_Chember_Details.Unloading_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No =TSPL_Milk_unloading_Chember_Details.Line_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.line_no  Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.chamber_desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.chamber_desc  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No
 Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no = TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no 
 left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail t_FAT on t_FAT.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO and t_FAT.Param_Type='FAT' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_FAT.sno
            left outer join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail t_SNF on t_SNF.Chalan_No=TSPL_MCC_Dispatch_Challan.Chalan_NO and t_SNF .Param_Type='SNF' and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no=t_SNF.sno
LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No  left outer join tspl_cleaning on tspl_cleaning.QC_No =TSPL_QUALITY_CHECK .QC_No   where 2 = 2
	AND ISNULL(Tspl_Gate_Entry_Details.Challan_No,'')<>'' "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                sQuery += " and tspl_mcc_dispatch_challan.MCC_Code In ( " + clsCommon.GetMulcallString(txtMCC.arrValueMember) + " )"
            Else
                Dim StrMCCQRY As String = "Declare @temp VARCHAR(MAX) Set @temp =(Select ',''' + cast(Code as varchar) + '''' from (select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master where Plant_Code='" & fndLoc.Value & "' union all select TSPL_LOCATION_MASTER.Location_Code as [Code],TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" & fndLoc.Value & "')xx order by Code FOR XML PATH('')) select SUBSTRING(@temp, 2, 200000) AS MCC "
                Dim StrMCC As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrMCCQRY))
                sQuery += " and tspl_mcc_dispatch_challan.MCC_Code in ( " + StrMCC + " )"
            End If
            sQuery += " and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
            sQuery += ") As xx 
              left join TSPL_SHIFT_MASTER on xx.Shift_Code=TSPL_SHIFT_MASTER.SHIFT_CODE left join tspl_mcc_master on xx.allocatetomcc=tspl_mcc_master.MCC_Code left outer join TSPL_MCC_DISPATCH_CHALLAN on TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO=xx.[Challan No] left  outer join TSPL_MCC_TANKER_GATE_OUT on TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO=TSPL_MCC_DISPATCH_CHALLAN.Against_Gate_Out and TSPL_MCC_TANKER_GATE_OUT.IsCancel=0 and TSPL_MCC_TANKER_GATE_OUT.IS_POSTED=1 left outer join TSPL_MCC_TANKER_GATE_OUT_SECURITY on TSPL_MCC_TANKER_GATE_OUT_SECURITY.Gate_Out_No=TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO and TSPL_MCC_TANKER_GATE_OUT_SECURITY.IS_POSTED=1 left outer join ( SELECT TSPL_MCC_Dispatch_Challan_detail.chalan_no,coalesce(TSPL_MCC_Dispatch_Challan_detail.intermittent_dispatch_no,TSPL_MCC_Dispatch_Challan_detail.chalan_no) as dispatch_no ,SNO FROM TSPL_MCC_Dispatch_Challan_detail
             left outer join tspl_mcc_dispatch_challan on tspl_mcc_dispatch_challan.chalan_no=TSPL_MCC_Dispatch_Challan_detail.chalan_no
             )DC_Final ON DC_Final.chalan_no=xx.[Challan No] AND DC_Final.SNO=XX.LINE_NO ) 
             select ROW_NUMBER() OVER( ORDER BY wholeData.[challan No]) AS SNo
            ,coalesce(FromMcc.mcc_code,FromLocation.location_code) as [Dispatch Location Code]
            ,coalesce(FromMcc.mcc_name,FromLocation.location_desc) as [Dispatch Location Name]
            ,wholeData.[challan date] AS [Dispatch Date], wholeData.[challan No] AS [Dispatch No]
            ,wholeData.[Tanker No]  as [Dispatch Tanker No] ,convert(decimal(18,3),wholeData.[Challan Qty]) as [Dispatch Qty] 
            ,convert(decimal(18,3),wholeData.[FAT %]) as [Dispatch FAT Per],convert(decimal(18,3),wholeData.[SNF %]) as [Dispatch SNF Per],convert(decimal(18,3),wholeData.FATKG) as [Dispatch FAT Kg] ,convert(decimal(18,3),wholeData.SNFKG)  as [Dispatch SNF Kg],convert(decimal(18,2),wholeData.[FAT Amt]) AS [Dispatch FAT Amt],convert(decimal(18,2),wholeData.[SNF Amt]) AS [Dispatch SNF Amt]
            ,convert(decimal(18,2),wholeData.[Total Amount]) AS [Dispatch Amount]
            ,coalesce(ToMcc.mcc_code,ToLocation.location_code) as [Received Location Code]
            ,coalesce(ToMcc.mcc_name,ToLocation.location_desc) as  [Received Location Name] 
            ,Convert(varchar,wholeData.Receipt_Challan_Date,103) as [Received Date]
            ,wholeData.Receipt_Challan_No as [Milk Transfer In No]
            ,wholeData.Receipt_Tanker_No as [Received Tanker No] 
            ,wholeData.RECDQTY as [Received Qty]
            ,wholeData.RECDFATPer as [Received FAT Per]
            ,wholeData.RECDSNFPer as [Received SNF Per]
            ,wholeData.RECDFATKg as [Received FAT Kg]
            ,wholeData.RECDSNFKg as [Received SNF Kg]
            ,wholeData.RECDFATAmt as [Received FAT Amt]
            ,wholeData.RECDSNFAmt as [Received SNF Amt]
            ,wholeData.RECDAMOUNT as [Received Amount]
            ,wholeData.RECDFATAmt,wholeData.RECDSNFAmt,wholeData.RECDAMOUNT
            ,convert(decimal(18,3),wholeData.[Challan Qty]-ISNULL(wholeData.RECDQTY,0)) as [Diff Qty] 
            ,convert(decimal(18,3),wholeData.FATKG-ISNULL(wholeData.RECDFATKg,0)) as [Diff FAT Kg] 
            ,convert(decimal(18,3),wholeData.SNFKG-ISNULL(wholeData.RECDSNFKg,0))  as [Diff SNF Kg]
            ,convert(decimal(18,2),wholeData.[FAT Amt]-wholeData.RECDFATAmt) AS [Diff FAT Amt]
            ,convert(decimal(18,2),wholeData.[SNF Amt]-wholeData.RECDSNFAmt) AS [Diff SNF Amt]
            ,convert(decimal(18,2),wholeData.[Total Amount]-wholeData.RECDAMOUNT) AS [Diff Amount]
            from wholeData left outer join
               ( SELECT TSPL_MCC_Dispatch_Challan_detail.chalan_no ,SNO
              ,tspl_mcc_dispatch_challan.mcc_code as DispatchLocationCode 
              ,tspl_mcc_dispatch_challan.mcc_or_plant_code as RECDLocationCode
               FROM TSPL_MCC_Dispatch_Challan_detail
               left outer join tspl_mcc_dispatch_challan on tspl_mcc_dispatch_challan.chalan_no=TSPL_MCC_Dispatch_Challan_detail.chalan_no
            )Floc ON Floc.chalan_no=wholeData.[Challan No] AND Floc.SNO=wholeData.Line_No_a
            left outer join TSPL_MCC_MASTER  FromMcc on FromMcc .MCC_Code =Floc.DispatchLocationCode
            left outer join TSPL_LOCATION_MASTER FromLocation on FromLocation.LOCATION_Code =Floc.DispatchLocationCode
            left outer join TSPL_MCC_MASTER  ToMcc on ToMcc.MCC_Code =Floc.RECDLocationCode
            left outer join TSPL_LOCATION_MASTER ToLocation on ToLocation.LOCATION_Code =Floc.RECDLocationCode
             where 2=2 order by convert(date,wholeData.[challan date],103),wholeData.[challan No],WholeData.line_no_a  "


            dt = clsDBFuncationality.GetDataTable(sQuery)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If


            RadPageView1.SelectedPage = RadPageViewPage2

            gv.BestFitColumns()
            ReStoreGridLayout()
            View()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub View()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()


            view.ColumnGroups.Add(New GridViewColumnGroup("Dispatch"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch Location Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch Location Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch Tanker No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch Qty").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch FAT Per").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch SNF Per").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch FAT Kg").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch SNF Kg").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch FAT Amt").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch SNF Amt").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Dispatch Amount").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Transfer In"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received Location Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received Location Name").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received Date").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Milk Transfer In No").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received Tanker No").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received FAT Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received SNF Per").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received FAT Kg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received SNF Kg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received FAT Amt").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received SNF Amt").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Received Amount").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Diff Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Diff FAT Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Diff SNF Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Diff FAT Amt").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Diff SNF Amt").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("Diff Amount").Name)

            gv.ViewDefinition = view
        End If

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try

            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptTankerDispatchvsAckn & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                '.Add("Report Type : " + IIf(chkSummary.IsChecked = True, "Summary", "Detail"))
                'arrHeader.Add(("MCC Name: " + strMCCName + " "))
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add("MCC/Plant : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then
                    transportSql.exportdata(gv, "", Me.Text, , arrHeader, False, False, True)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)

    End Sub

    Private Sub RptTankerDispatchvsAckn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'LOCATIONRIGTHS()
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
            RadPageView1.SelectedPage = RadPageViewPage1
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RptTankerDispatchvsAckn_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()

        End If
    End Sub



    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click, RadPageViewPage1.Click
        Try
            btnReferesh = True
            PageSetupReport_ID = MyBase.Form_ID
            LoadData()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub FndLoc__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLoc._MYValidating
        Dim whrCls As String = "TSPL_LOCATION_MASTER.Type = 'PLANT'"

        fndLoc.Value = clsLocation.getFinder(whrCls, fndLoc.Value, isButtonClicked)
        txtLocName.Text = clsLocation.GetName(fndLoc.Value, Nothing)
    End Sub

    Private Sub TxtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        If clsCommon.myLen(fndLoc.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Plant First.", Me.Text)
            txtMCC.arrValueMember = Nothing
            txtMCC.arrDispalyMember = Nothing
            fndLoc.Focus()
            Return
        End If
        'Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Code],tspl_location_master.location_desc as [Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where plant_Code = '" + clsCommon.myCstr(fndLoc.Value) + "' "
        Dim qry As String = " select Code,Name from (select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master where Plant_Code='" & fndLoc.Value & "'
         union all select TSPL_LOCATION_MASTER.Location_Code as [Code],TSPL_LOCATION_MASTER.Location_Desc as [Name] from TSPL_LOCATION_MASTER where TSPL_LOCATION_MASTER.Location_Code = '" & fndLoc.Value & "')xx"

        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCACK", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub
End Class
