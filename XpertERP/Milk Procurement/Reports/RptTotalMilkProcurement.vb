Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===================Created By Richa Agarwal 05 March,2020
Public Class rpttotalMilkProcurement
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Dim ShowFatSnfAfterApproval As Boolean = False

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.RptTotalMilkProcurement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isExport
    End Sub
  

    Public Sub Load_Report_Detail()
        Try

            If Year(txtFromDate.Value) <> Year(txtToDate.Value) OrElse Month(txtFromDate.Value) <> Month(txtToDate.Value) Then
                common.clsCommon.MyMessageBoxShow("Year/Month must be same in From and To Date for deatil report!", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If

            'Dim StartDate As String = ""
            'Dim EndDate As String = ""
            'StartDate = "01/" + clsCommon.myCstr(Month(txtFromDate.Value)) + "/" + clsCommon.myCstr(Year(txtFromDate.Value))
            'EndDate = clsCommon.myCstr(Month(txtFromDate.Value)) + "/01/" + clsCommon.myCstr(Year(txtFromDate.Value))
            'Dim DaysCount As Integer = clsDBFuncationality.getSingleValue("select DAY(EOMONTH('" + EndDate + "'))")
            'EndDate = clsDBFuncationality.getSingleValue("select EOMONTH('" + EndDate + "')")

            Dim StartDate As Date?
            Dim EndDate As Date?
            StartDate = clsDBFuncationality.getSingleValue("SELECT DATEADD(month, DATEDIFF(month, 0, '" & clsCommon.GetPrintDate(txtFromDate.Value, "yyyy/MM/dd") & "'), 0) AS StartOfMonth")
            'EndDate = clsCommon.myCstr(Month(txtFromDate.Value)) + "/01/" + clsCommon.myCstr(Year(txtFromDate.Value))
            Dim DaysCount As Integer = clsDBFuncationality.getSingleValue("select DAY(EOMONTH('" + clsCommon.GetPrintDate(txtFromDate.Value, "yyyy/MM/dd") + "'))")
            EndDate = clsDBFuncationality.getSingleValue("select EOMONTH('" + clsCommon.GetPrintDate(txtFromDate.Value, "yyyy/MM/dd") + "')")

            'Dim strItem As String = clsDBFuncationality.getSingleValue("SELECT STUFF((select distinct ',' + QUOTENAME(convert(date, DATEADD(day, n.Number, '" & StartDate & "'),103)) as DateFrom FROM master.dbo.spt_values n WHERE  n.Number <= DATEDIFF(day, '" & StartDate & "', '" & EndDate & "') AND n.Type = 'P' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")

            Dim strDate As String = clsDBFuncationality.getSingleValue("SELECT STUFF((select distinct ',' + QUOTENAME(convert(varchar, DATEADD(day, n.Number, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "'),103)) as DateFrom FROM master.dbo.spt_values n WHERE  n.Number <= DATEDIFF(day, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "', '" & clsCommon.GetPrintDate(EndDate, "yyyy/MM/dd") & "') AND n.Type = 'P' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")

            Dim strSumDate As String = clsDBFuncationality.getSingleValue("SELECT STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME(convert(varchar, DATEADD(day, n.Number, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "'),103)) +',0))' +' as ' + QUOTENAME(convert(varchar, DATEADD(day, n.Number, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "'),103)) as DateFrom FROM master.dbo.spt_values n WHERE  n.Number <= DATEDIFF(day, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "', '" & clsCommon.GetPrintDate(EndDate, "yyyy/MM/dd") & "') AND n.Type = 'P' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")

            Dim strGrandTotalDate As String = clsDBFuncationality.getSingleValue("SELECT STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME(convert(varchar, DATEADD(day, n.Number, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "'),103)) +',0))' as DateFrom FROM master.dbo.spt_values n WHERE  n.Number <= DATEDIFF(day, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "', '" & clsCommon.GetPrintDate(EndDate, "yyyy/MM/dd") & "') AND n.Type = 'P' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")

            'Dim strGrandAvgDate As String = clsDBFuncationality.getSingleValue("SELECT STUFF((SELECT distinct '+' +'AVG(isnull(' + QUOTENAME(convert(date, DATEADD(day, n.Number, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "'),103)) +',0))' as DateFrom FROM master.dbo.spt_values n WHERE  n.Number <= DATEDIFF(day, '" & clsCommon.GetPrintDate(StartDate, "yyyy/MM/dd") & "', '" & clsCommon.GetPrintDate(EndDate, "yyyy/MM/dd") & "') AND n.Type = 'P' FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')")

            '' write query to take data from bulk milk procurement
            'MainQuery = " select  [VEHICLE NO],[WdName],[Group],[Cust Group Desc], [Customer Category Code],[Zone],[Route No],UOM  , " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total]  from ("
            ',(" + strGrandAvgDate + ") as [AVG]
            Dim SQuery As String = "select ROW_NUMBER () over (order by [Vendor Code] ) As SNo,[Vendor Code],[Vendor Name]," + strSumDate + ",(" + strGrandTotalDate + ") as [Grand Total] " + ",Convert(decimal(18,3),(" + strGrandTotalDate + ")/" & DaysCount & ") as [AVG]   from (select * from ( "

            SQuery += " select  convert(varchar,BulkMilkData.[Gate Entry Date],103) as [TR_Date],MAX(BulkMilkData.[DocType]) AS [DocType],BulkMilkData.[Vendor Code] ,max(BulkMilkData.[Vendor Name]) as [Vendor Name],sum(BulkMilkData.[Challan Qty]) as [Qty] " & Environment.NewLine & _
    " ,sum(BulkMilkData.[Challan Qty]) as [Qty_In_Kg],Convert(decimal(18,3),sum(BulkMilkData.[Challan Qty Ltr] )) as [Qty_In_Ltr] " & Environment.NewLine & _
    " ,Convert(decimal(18,4),((sum(BulkMilkData.FATKG)*100) /sum(BulkMilkData.[Challan Qty] ))) as [FAT_Per],Convert(decimal(18,4),((sum(BulkMilkData.SNFKG)*100) /sum(BulkMilkData.[Challan Qty] ))) as [SNF_Per], " & Environment.NewLine & _
    " sum(BulkMilkData.FATKG ) as [FAT_KG],sum(BulkMilkData.SNFKG ) as [SNF_KG],sum(BulkMilkData.[Total Amount]  ) as [Total_Amount] " & _
    " from (select wholeData.[Gate Entry Date],wholeData.[DocType],wholeData.[Vendor Code] ," & Environment.NewLine & _
    " wholeData.[Vendor Name] ,wholeData.[Challan Qty],wholeData.[Challan Qty Ltr],wholeData.[FAT %] ,wholeData.CLR,wholeData.[SNF %],wholeData.FATKG,wholeData .SNFKG,wholeData.[Total Amount]  " & Environment.NewLine & _
    "  from (Select  DISTINCT Line_No as Line_No_a, xx.[To MCC or Plant Code], xx.DocType,xx.[Vendor Code],xx.[Vendor Name],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Challan Qty]," & Environment.NewLine & _
    "  xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No], XX.STATUS,xx.[MCC Name], xx.[Net Rate], xx.[FAT %] ,xx.[SNF %] ,xx.CLR , Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG," & Environment.NewLine & _
    "  Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG, case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total Amount] " & _
    " ,[Challan Qty LTR]" & _
    " From ( Select (TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty*TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor AS [Challan Qty LTR],TSPL_Milk_unloading_Chember_Details.Batch_No," & Environment.NewLine & _
    " TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType," & Environment.NewLine & _
    "  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],   Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) As [Milk Receipt Challan Date]," & Environment.NewLine & _
    "  Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], " & Environment.NewLine & _
    "  Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], " & Environment.NewLine & _
    " (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status], " & Environment.NewLine & _
    "   Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No], " & Environment.NewLine & _
    " Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No]," & Environment.NewLine & _
    " Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103)  As [Weighment Date],  " & Environment.NewLine & _
    " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], " & Environment.NewLine & _
    " Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty As [Challan Qty]," & Environment.NewLine & _
    "    TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight], " & Environment.NewLine & _
    " Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date], " & Environment.NewLine & _
    " TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code]," & Environment.NewLine & _
    "  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name], " & Environment.NewLine & _
    " Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  " & Environment.NewLine & _
    " Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc]," & Environment.NewLine & _
    "  Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM, " & Environment.NewLine & _
    " TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No],  " & Environment.NewLine & _
    " Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time], " & Environment.NewLine & _
    "   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], " & Environment.NewLine & _
    " Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And " & Environment.NewLine & _
    " TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else " & Environment.NewLine & _
    " Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS, " & Environment.NewLine & _
    "  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No],TSPL_Cleaning .Doc_No as [Cleaning No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], " & Environment.NewLine & _
    " TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time]," & Environment.NewLine & _
    " Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR," & Environment.NewLine & _
    "  TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], " & Environment.NewLine & _
    " TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction], " & Environment.NewLine & _
    "   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],	" & Environment.NewLine & _
    " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate]," & Environment.NewLine & _
    "  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt], " & Environment.NewLine & _
    " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]  ,TSPL_QUALITY_CHECK.Shift_Code From Tspl_Gate_Entry_Details  left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Milk_unloading_Chember_Details On TSPL_MILK_UNLOADING.Unloading_No = TSPL_Milk_unloading_Chember_Details.Unloading_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No =TSPL_Milk_unloading_Chember_Details.Line_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc " & Environment.NewLine & _
    " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No" & Environment.NewLine & _
    " left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " & Environment.NewLine & _
    " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " & Environment.NewLine & _
    "LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No  left outer join tspl_cleaning on tspl_cleaning.QC_No =TSPL_QUALITY_CHECK .QC_No  " & _
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_KG on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_KG.Item_Code and TSPL_ITEM_UOM_DETAIL_KG.UOM_Code=Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End" & _
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' " & _
    " where 2 = 2 " & _
    " and MONTH(cast(Tspl_Gate_Entry_Details.Date_And_Time as date))='" + clsCommon.myCstr(Month(txtFromDate.Value)) + "' and Year(cast(Tspl_Gate_Entry_Details.Date_And_Time as date)) ='" + clsCommon.myCstr(Year(txtToDate.Value)) + "'" & Environment.NewLine & _
    " Union ALL" & Environment.NewLine & _
    " Select (TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty*TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor AS [Challan Qty LTR],'' as Batch_No,TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No," & Environment.NewLine & _
    " TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType," & Environment.NewLine & _
    " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No, '') End As [Milk Receipt Challan No]," & Environment.NewLine & _
    " Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) As [Milk Receipt Challan Date]," & Environment.NewLine & _
    " Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], " & Environment.NewLine & _
    " Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], " & Environment.NewLine & _
    " (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status]," & Environment.NewLine & _
    " Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  " & Environment.NewLine & _
    " Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],   Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty*-1 As [Challan Qty],   TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight*-1 As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No],  Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time],  Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No],TSPL_Cleaning .Doc_No as [Cleaning No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt)*-1 Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG)*-1 End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt)*-1 Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG)*-1 End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount*-1 Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount*-1 End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class] ,TSPL_QUALITY_CHECK.Shift_Code From TSPL_MILK_TRANSFER_IN_RETURN  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  LEFT OUTER JOIN Tspl_Gate_Entry_Details ON  Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no    left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc  Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'   Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No " & Environment.NewLine & _
    " LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No" & Environment.NewLine & _
    " left outer join tspl_cleaning on tspl_cleaning.QC_No =TSPL_QUALITY_CHECK .QC_No " & Environment.NewLine & _
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_KG on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_KG.Item_Code and TSPL_ITEM_UOM_DETAIL_KG.UOM_Code=Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End" & _
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' " & _
    " where 2 = 2 and MONTH(cast(Tspl_Gate_Entry_Details.Date_And_Time as date)) ='" & clsCommon.myCstr(Month(txtFromDate.Value)) & "' and YEAR(cast(Tspl_Gate_Entry_Details.Date_And_Time as Date)) ='" & clsCommon.myCstr(Year(txtFromDate.Value)) & "' ) As xx " & Environment.NewLine & _
    " left join TSPL_SHIFT_MASTER on xx.Shift_Code=TSPL_SHIFT_MASTER.SHIFT_CODE " & _
    " ) wholeData " & Environment.NewLine & _
    " where 2=2 and [DocType] in ('Bulk In','Bulk Ret') ) BulkMilkData group By BulkMilkData.[Vendor Code],BulkMilkData.[Gate Entry Date]"

            SQuery += " UNION ALL "

            ''write query to take data from mcc milk procurement


            SQuery += " select convert(varchar,aa.[Doc Date],103) ,'MCC Procurement' as [DocType],aa.[MCC Code] ,aa.[MCC Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,Convert(decimal(18,4),aa.[FAT(%)]) as [FAT Per],Convert(decimal(18,4),aa.[SNF(%)]) as [SNF Per],aa.[FAT(KG)] as [FAT KG],aa.[SNF(KG)] AS [SNF KG],aa.[SRN Amount] " &
            " from (  select xxx.*  from ( select xx.* from ( select PP.[Doc Date] ,pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)] ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],  sum([SRN Qty]) as [SRN Qty] ,sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR  from ( " & Environment.NewLine &
            " Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.Date ,final.[Doc Date] ,final.Shift ,final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name],final.[Vendor Group Code] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ,final.Item_Code,final.Item_Desc,final.[Milk Weight],final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date ,MACHINE_NO,(CASE WHEN [Sample Status]='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL   From " & Environment.NewLine &
            " (Select TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Buffalo Milk Qty (KG)] " & Environment.NewLine &
            " , Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight], TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)], TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,  Convert(decimal(18,2), TSPL_MILK_SRN_DETAIL.FAT_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.SNF_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [SNF(KG)], Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = '' Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample , '' as MACHINE_NO,[Transporter Code],[Transporter Name],isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code],TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount  " & Environment.NewLine &
            " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount  " & Environment.NewLine &
            " From TSPL_MILK_SRN_DETAIL  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
             left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No  " & Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  " & Environment.NewLine &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " & Environment.NewLine &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " & Environment.NewLine &
            " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  " & Environment.NewLine &
            " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE " & Environment.NewLine &
            " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE " & Environment.NewLine &
            " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE " & Environment.NewLine &
            " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & Environment.NewLine &
            " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & Environment.NewLine & "
            Left outer join (Select code, max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code  " & Environment.NewLine &
            " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per  " & Environment.NewLine &
            " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  where 2 = 2 " &
            " and month(Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date)) ='" & clsCommon.myCstr(Month(txtFromDate.Value)) & "' and year(Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date)) ='" & clsCommon.myCstr(Year(txtFromDate.Value)) & "' ) As final where 2=2  " & Environment.NewLine &
            " ) as  pp group by pp.[MCC Code],pp.[Doc Date]  )as xx ) as xxx ) as aa "

            SQuery += ") as zz where 1=1 "


            If TxtDocType.arrValueMember IsNot Nothing AndAlso TxtDocType.arrValueMember.Count > 0 Then
                SQuery += " and zz.[DocType] in (" + clsCommon.GetMulcallString(TxtDocType.arrValueMember) + ")  "
            End If

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                SQuery += " and zz.[Vendor Code] in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")  "
            End If

            If TxtVendorCode.arrValueMember IsNot Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
                SQuery += " and zz.[Vendor Code] in (" + clsCommon.GetMulcallString(TxtVendorCode.arrValueMember) + ")  "
            End If

            SQuery += ") as s pivot (  sum("
            If clsCommon.CompairString(ddlUOM.SelectedValue, "LTR") = CompairStringResult.Equal Then
                SQuery += "[Qty_In_Ltr]"
            Else
                SQuery += "[Qty_In_Kg]"
            End If

            SQuery += ") for TR_Date in (" + strDate + " )) as zpivot group by zpivot.[Vendor Code],zpivot.[Vendor Name]"

            Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(SQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()


                For ii As Integer = 0 To gv.Columns.Count - 1
                    gv.Columns(ii).ReadOnly = True
                    gv.Columns(ii).Width = 200
                    gv.Columns(ii).IsVisible = True
                Next
                gv.BestFitColumns()

                If gv.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 3 To gv.Columns.Count - 2
                        Dim aa = gv.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F3}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    Next
                    gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                End If


                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub



    Public Sub Load_Report()
        Try
            If txtFromDate.Value > txtToDate.Value Then
                Throw New Exception("From date can not be greater then to Date")
            End If
            '' write query to take data from bulk milk procurement
            Dim SQuery As String = "select ROW_NUMBER () over (order by [Vendor Code] ) As SNo,xx.* from ( "

            SQuery += " select MAX(BulkMilkData.[DocType]) AS [DocType],BulkMilkData.[Vendor Code] ,max(BulkMilkData.[Vendor Name]) as [Vendor Name],sum(BulkMilkData.[Challan Qty]) as [Qty] " & Environment.NewLine &
    " ,sum(BulkMilkData.[Challan Qty]) as [Qty_In_Kg],Convert(decimal(18,3),sum(BulkMilkData.[Challan Qty Ltr] )) as [Qty_In_Ltr] " & Environment.NewLine &
    " ,Convert(decimal(18,4),((sum(BulkMilkData.FATKG)*100) /sum(BulkMilkData.[Challan Qty] ))) as [FAT_Per],Convert(decimal(18,4),((sum(BulkMilkData.SNFKG)*100) /sum(BulkMilkData.[Challan Qty] ))) as [SNF_Per], " & Environment.NewLine &
    " sum(BulkMilkData.FATKG ) as [FAT_KG],sum(BulkMilkData.SNFKG ) as [SNF_KG],sum(BulkMilkData.[Total Amount]  ) as [Total_Amount] " &
    " ,(case when (sum(BulkMilkData.FATKG)=0 and sum(BulkMilkData.SNFKG)=0) then 0 else Convert(decimal(18,2),(sum(BulkMilkData.[Total Amount]  )/(sum(BulkMilkData.FATKG)+(sum(BulkMilkData.SNFKG)*2/3)))*2) end) as [EFU Price]" &
    " ,Convert(decimal(18,2),sum(BulkMilkData.[Total Amount])/ sum(BulkMilkData.[Challan Qty])) as [Price Per KG]" &
    " ,(case when sum(BulkMilkData.FATKG)=0 then 0 else Convert(decimal(18,2),sum(BulkMilkData.[Total Amount])/sum(BulkMilkData.FATKG)) end) as [FAT Rate Per KG]" &
    " from (select wholeData.[DocType],wholeData.[Vendor Code] ," & Environment.NewLine &
    " wholeData.[Vendor Name] ,wholeData.[Challan Qty],wholeData.[Challan Qty Ltr],wholeData.[FAT %] ,wholeData.CLR,wholeData.[SNF %],wholeData.FATKG,wholeData .SNFKG,wholeData.[Total Amount]  " & Environment.NewLine &
    "  from (Select  DISTINCT Line_No as Line_No_a, xx.[To MCC or Plant Code], xx.DocType,xx.[Vendor Code],xx.[Vendor Name],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Challan Qty]," & Environment.NewLine &
    "  xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No], XX.STATUS,xx.[MCC Name], xx.[Net Rate], xx.[FAT %] ,xx.[SNF %] ,xx.CLR , Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG," & Environment.NewLine &
    "  Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG, case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total Amount] " &
    " ,[Challan Qty LTR]" &
    " From ( Select  (TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty*TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor AS [Challan Qty LTR],TSPL_Milk_unloading_Chember_Details.Batch_No," & Environment.NewLine &
    " TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType," & Environment.NewLine &
    "  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],   Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) As [Milk Receipt Challan Date]," & Environment.NewLine &
    "  Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], " & Environment.NewLine &
    "  Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], " & Environment.NewLine &
    " (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status], " & Environment.NewLine &
    "   Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No], " & Environment.NewLine &
    " Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No]," & Environment.NewLine &
    " Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  " & Environment.NewLine &
    " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], " & Environment.NewLine &
    " Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty As [Challan Qty]," & Environment.NewLine &
    "    TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight], " & Environment.NewLine &
    " Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date], " & Environment.NewLine &
    " TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code]," & Environment.NewLine &
    "  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name], " & Environment.NewLine &
    " Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  " & Environment.NewLine &
    " Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc]," & Environment.NewLine &
    "  Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM, " & Environment.NewLine &
    " TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No],  " & Environment.NewLine &
    " Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103)  As [Unloading Date Time], " & Environment.NewLine &
    "   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103)  As [QC Date Time], " & Environment.NewLine &
    " Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And " & Environment.NewLine &
    " TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else " & Environment.NewLine &
    " Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS, " & Environment.NewLine &
    "  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No],TSPL_Cleaning .Doc_No as [Cleaning No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], " & Environment.NewLine &
    " TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time]," & Environment.NewLine &
    " Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR," & Environment.NewLine &
    "  TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], " & Environment.NewLine &
    " TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction], " & Environment.NewLine &
    "   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],	" & Environment.NewLine &
    " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate]," & Environment.NewLine &
    "  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt], " & Environment.NewLine &
    " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]  ,TSPL_QUALITY_CHECK.Shift_Code From Tspl_Gate_Entry_Details  left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Milk_unloading_Chember_Details On TSPL_MILK_UNLOADING.Unloading_No = TSPL_Milk_unloading_Chember_Details.Unloading_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No =TSPL_Milk_unloading_Chember_Details.Line_No Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc " & Environment.NewLine &
    " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No" & Environment.NewLine &
    " left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " & Environment.NewLine &
    " Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " & Environment.NewLine &
    " LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No  left outer join tspl_cleaning on tspl_cleaning.QC_No =TSPL_QUALITY_CHECK .QC_No " &
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_KG on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_KG.Item_Code and TSPL_ITEM_UOM_DETAIL_KG.UOM_Code=Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End" &
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' " &
    " where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103)" & Environment.NewLine &
    " Union ALL" & Environment.NewLine &
    " Select  (TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty*TSPL_ITEM_UOM_DETAIL_KG.Conversion_Factor)/TSPL_ITEM_UOM_DETAIL_LTR.Conversion_Factor AS [Challan Qty LTR],'' as Batch_No,TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No," & Environment.NewLine &
    " TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType," & Environment.NewLine &
    " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No, '') End As [Milk Receipt Challan No]," & Environment.NewLine &
    " Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) As [Milk Receipt Challan Date]," & Environment.NewLine &
    " Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], " & Environment.NewLine &
    " Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], " & Environment.NewLine &
    " (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status]," & Environment.NewLine &
    " Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  " & Environment.NewLine &
    " Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],   Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty*-1 As [Challan Qty],   TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight*-1 As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No],  Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time],  Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No],TSPL_Cleaning .Doc_No as [Cleaning No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt)*-1 Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG)*-1 End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt)*-1 Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG)*-1 End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount*-1 Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount*-1 End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class] ,TSPL_QUALITY_CHECK.Shift_Code From TSPL_MILK_TRANSFER_IN_RETURN  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  LEFT OUTER JOIN Tspl_Gate_Entry_Details ON  Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no    left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc  Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'   Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No " & Environment.NewLine &
    " LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No" & Environment.NewLine &
    " left outer join tspl_cleaning on tspl_cleaning.QC_No =TSPL_QUALITY_CHECK .QC_No " & Environment.NewLine &
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_KG on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_KG.Item_Code and TSPL_ITEM_UOM_DETAIL_KG.UOM_Code=Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End" &
    " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAIL_LTR on TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code=TSPL_ITEM_UOM_DETAIL_LTR.Item_Code and TSPL_ITEM_UOM_DETAIL_LTR.UOM_Code='LTR' " &
    " where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103) ) As xx " & Environment.NewLine &
    " left join TSPL_SHIFT_MASTER on xx.Shift_Code=TSPL_SHIFT_MASTER.SHIFT_CODE " &
    " ) wholeData " & Environment.NewLine &
    " where 2=2 and [DocType] in ('Bulk In','Bulk Ret') ) BulkMilkData group By BulkMilkData.[Vendor Code]"

            SQuery += " UNION ALL "

            ''write query to take data from mcc milk procurement


            SQuery += " select 'MCC Procurement' as [DocType],aa.[MCC Code] ,aa.[MCC Name],aa.[Milk Weight] ,aa.[Milk Weight(KG)]	,aa.[Milk Weight(LTR)] ,Convert(decimal(18,4),aa.[FAT(%)]) as [FAT Per],Convert(decimal(18,4),aa.[SNF(%)]) as [SNF Per],aa.[FAT(KG)] as [FAT KG],aa.[SNF(KG)] AS [SNF KG],aa.[SRN Amount] " &
            "  ,(case when (aa.[FAT(KG)]=0 and aa.[SNF(KG)]=0) then 0 else Convert(decimal(18,2),(aa.[SRN Amount]/(aa.[FAT(KG)]+(aa.[SNF(KG)]*2/3)))*2) end) as [EFU Price]" &
            " ,Convert(decimal(18,2),aa.[SRN Amount]/ aa.[Milk Weight(KG)]) as [Price Per KG]" &
            " ,(case when aa.[FAT(KG)]=0 then 0 else Convert(decimal(18,2),aa.[SRN Amount]/aa.[FAT(KG)]) end) as [FAT Rate Per KG]" &
            " from (  select xxx.*  from ( select xx.* from ( select pp.[MCC Code]  as [MCC Code],max(pp.[MCC Name] )  as [MCC Name],sum([Milk Weight] ) as [Milk Weight],sum([Milk Weight(KG)] ) as [Milk Weight(KG)],sum([Milk Weight(LTR)] ) as [Milk Weight(LTR)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as [FAT(%)], case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as [SNF(%)] ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)],  sum([SRN Qty]) as [SRN Qty] ,sum([SRN Amount]) as [SRN Amount],avg(CLR) as CLR  from ( " & Environment.NewLine &
            " Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.Date ,final.[Doc Date] ,final.Shift ,final.[Route Code],final.[Route Name] ,final.[Vehicle Code] ,final.[VSP Code],final.[VSP Name],final.[Vendor Group Code] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] ,final.Item_Code,final.Item_Desc,final.[Milk Weight],final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)] ,final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate],final.[Shift Status] ,Invoice_no ,Invoice_Date ,MACHINE_NO,(CASE WHEN [Sample Status]='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL   From " & Environment.NewLine &
            " (Select TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Buffalo Milk Qty (KG)] " & Environment.NewLine &
            " , Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],  TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight], TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)], TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,  Convert(decimal(18,2), TSPL_MILK_SRN_DETAIL.FAT_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.SNF_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [SNF(KG)], Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = '' Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample , '' as MACHINE_NO,[Transporter Code],[Transporter Name],isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,isnull(TSPL_MILK_SRN_DETAIL.NET_AMOUNT,0) as NET_AMOUNT,isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount,TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code],TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount  " & Environment.NewLine &
            " ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount  " & Environment.NewLine &
            " From   TSPL_MILK_SRN_DETAIL   Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
            left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No  " & Environment.NewLine &
            " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  " & Environment.NewLine &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " & Environment.NewLine &
            " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " & Environment.NewLine &
            " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  " & Environment.NewLine &
            " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE " & Environment.NewLine &
            " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE " & Environment.NewLine &
            " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE " & Environment.NewLine &
            " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & Environment.NewLine &
            " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & Environment.NewLine & "
            left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code  " & Environment.NewLine &
            " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per  " & Environment.NewLine &
            " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  where 2 = 2  and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) >=convert(date,'" + txtFromDate.Value + "',103) and Cast(TSPL_MILK_SRN_HEAD.DOC_DATE as Date) <=convert(date,'" + txtToDate.Value + "',103) ) As final where 2=2  " & Environment.NewLine &
            " ) as  pp group by pp.[MCC Code]  )as xx ) as xxx ) as aa "


            SQuery += ")xx where 1=1 "

            If TxtDocType.arrValueMember IsNot Nothing AndAlso TxtDocType.arrValueMember.Count > 0 Then
                SQuery += " and xx.[DocType] in (" + clsCommon.GetMulcallString(TxtDocType.arrValueMember) + ")  "
            End If

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                SQuery += " and xx.[Vendor Code] in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")  "
            End If

            If TxtVendorCode.arrValueMember IsNot Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
                SQuery += " and xx.[Vendor Code] in (" + clsCommon.GetMulcallString(TxtVendorCode.arrValueMember) + ")  "
            End If

            Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(SQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()

                FormatGridSummary()


                '  FormatGrid(dtExtraColumn)

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FormatGridSummary()
        Try
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).Width = 200
                gv.Columns(ii).IsVisible = True
            Next
            gv.BestFitColumns()

            gv.Columns("Qty").IsVisible = False

            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim item2 As New GridViewSummaryItem("Qty_In_Kg", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("Qty_In_Ltr", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)
            Dim item4 As New GridViewSummaryItem("FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Total_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)

            Dim summaryItem1 As New GridViewSummaryItem()
            summaryItem1.FormatString = "{0:F4}"
            summaryItem1.Name = "FAT_Per"
            summaryItem1.AggregateExpression = "sum(FAT_KG)*100/sum(Qty_In_Kg)"
            summaryRowItem.Add(summaryItem1)

            Dim summaryItem2 As New GridViewSummaryItem()
            summaryItem2.FormatString = "{0:F4}"
            summaryItem2.Name = "SNF_Per"
            summaryItem2.AggregateExpression = "sum(SNF_KG)*100/sum(Qty_In_Kg)"
            summaryRowItem.Add(summaryItem2)

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            gv.Columns("Qty_In_Kg").HeaderText = "Qty In Kg"
            gv.Columns("Qty_In_Ltr").HeaderText = "Qty In Ltr"
            gv.Columns("FAT_Per").HeaderText = "FAT Per"
            gv.Columns("SNF_Per").HeaderText = "SNF Per"
            gv.Columns("FAT_KG").HeaderText = "FAT KG"
            gv.Columns("SNF_KG").HeaderText = "SNF KG"
            gv.Columns("Total_Amount").HeaderText = "Total Amount"


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub FormatGrid(ByVal dtExtraColumn As DataTable)

        If gv.Columns.Contains("Cleaning No") Then
            gv.Columns("Cleaning No").IsVisible = False
        End If

        If gv.Columns.Contains("Shift Name") Then
            gv.Columns("Shift Name").IsVisible = False
        End If


        Dim summaryRowItem As New GridViewSummaryRowItem()


        Dim intCount As Integer = 0


        Dim item2 As New GridViewSummaryItem("Challan Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Gross Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Tare Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Net Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("FAT Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("SNF Amt", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        Dim item8 As New GridViewSummaryItem("Total Amount Temp", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        'Dim item9 As New GridViewSummaryItem("SNF Amt", "{0:F2} ", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Total Amount", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("FATKG", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("SNFKG", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)



        Dim item13 As New GridViewSummaryItem("Challan Fat%", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("Challan SNF%", "{0:F2}", GridAggregateFunction.Avg)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("Challan Fat KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("Challan SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("Challan TS", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)
        Dim item18 As New GridViewSummaryItem("Differrence Qty", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)
        Dim item19 As New GridViewSummaryItem("Differrence FAT %", "{0:F2} ", GridAggregateFunction.Avg)
        summaryRowItem.Add(item19)
        Dim item20 As New GridViewSummaryItem("Differrence SNF %", "{0:F2} ", GridAggregateFunction.Avg)
        summaryRowItem.Add(item20)
        Dim item21 As New GridViewSummaryItem("Differrence FAT kG", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("Differrence SNF KG", "{0:F2} ", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)


        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Try
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
                    common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                End If
                ''stuti regarding memory leakage
                obj.GridLayout.Close()
                obj.GridLayout.Dispose()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If chkSummarywise.Checked = True Then
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv
        Else
            PageSetupReport_ID = ""
            TemplateGridview = Nothing
        End If

        If ChkDetailWise.Checked = True Then
            Load_Report_Detail()
        Else
            Load_Report()
        End If
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtMCC.arrValueMember = Nothing
        TxtVendorCode.arrValueMember = Nothing
        TxtDocType.arrValueMember = Nothing
        ddlUOM.SelectedValue = "LTR"
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing

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

    Private Sub RptBulkMilkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        LoadUOM()
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadUOM()
        Dim dt1 As DataTable
        dt1 = New DataTable
        dt1.Columns.Add("Code", GetType(String))
        dt1.Rows.Add("LTR")
        dt1.Rows.Add("KG")

        ddlUOM.DataSource = dt1
        ddlUOM.ValueMember = "Code"
        ddlUOM.DisplayMember = "Code"
    End Sub

    Private Sub RptBulkMilkRegister_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            'Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Total Milk Procurement Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Total Milk Procurement Report", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptTotalMilkProcurement & "'"))

            If TxtDocType.arrValueMember IsNot Nothing AndAlso TxtDocType.arrValueMember.Count > 0 Then
                arrHeader.Add("Doc Type : " + clsCommon.GetMulcallString(TxtDocType.arrValueMember))
            End If

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add("MCC : " + clsCommon.GetMulcallString(txtMCC.arrValueMember))
            End If

            If TxtVendorCode.arrValueMember IsNot Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(TxtVendorCode.arrValueMember))
            End If

            If ChkDetailWise.Checked = True Then
                arrHeader.Add("UOM : " + clsCommon.myCstr(ddlUOM.SelectedValue))
            End If

            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click

        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptTotalMilkProcurement & "'"))

            If TxtDocType.arrValueMember IsNot Nothing AndAlso TxtDocType.arrValueMember.Count > 0 Then
                arrHeader.Add("Doc Type : " + clsCommon.GetMulcallString(TxtDocType.arrValueMember))
            End If

            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                arrHeader.Add("MCC : " + clsCommon.GetMulcallString(txtMCC.arrValueMember))
            End If

            If TxtVendorCode.arrValueMember IsNot Nothing AndAlso TxtVendorCode.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(TxtVendorCode.arrValueMember))
            End If

            If ChkDetailWise.Checked = True Then
                arrHeader.Add("UOM : " + clsCommon.myCstr(ddlUOM.SelectedValue))
            End If

            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Total Milk Procurement Report", gv, arrHeader, "Total Milk Procurement Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            Dim qry As String = "select tspl_mcc_master.MCC_Code as [Code],tspl_mcc_master.MCC_NAME as [Name] from tspl_mcc_master"
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSl@MC1", qry, "Code", "Name", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub TxtVendorCode__My_Click(sender As Object, e As EventArgs) Handles TxtVendorCode._My_Click
        Try
            Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Code as [Code],Vendor_Name as [Name]," & _
                        " ISNULL(TSPL_VENDOR_MASTER.Alies_Name,'') As [Alies Name],TSPL_VENDOR_MASTER.Add1,TSPL_VENDOR_MASTER.Add2,Add3,TSPL_VENDOR_MASTER.Closing_Date as [Closing Date], " & _
                        " TSPL_VENDOR_MASTER.Vendor_Group_Code as [Vendor Group Code],TSPL_VENDOR_MASTER.Vendor_Group_Code_Desc as [Vendor Group Description], " & _
                        " TSPL_VENDOR_MASTER.City_Code as [City Code],TSPL_VENDOR_MASTER.City_Code_Desc as [City Description],TSPL_VENDOR_MASTER.State,TSPL_VENDOR_MASTER.Phone1,TSPL_VENDOR_MASTER.Phone2,TSPL_VENDOR_MASTER.Fax,TSPL_VENDOR_MASTER.Email,TSPL_VENDOR_MASTER.WebSite, " & _
                        " TSPL_VENDOR_MASTER.Contact_Person_Name as [Contact Person Name],TSPL_VENDOR_MASTER.Contact_Person_Phone as [Contact Person Phone] from TSPL_VENDOR_MASTER "

            TxtVendorCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivVenMulSel1", qry, "Code", "Name", TxtVendorCode.arrValueMember, TxtVendorCode.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub TxtDocType__My_Click(sender As Object, e As EventArgs) Handles TxtDocType._My_Click
        Try
            Dim qry As String = "Select xxx.Code,  xxx.Name From (Select 'Bulk In' As Code, 'Bulk In' As Name  Union  Select 'MCC Procurement' As Code,    'MCC Procurement' As Name union select 'Bulk Ret' as Code,'Bulk Ret' as Name) xxx "
            TxtDocType.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSl@DOC1", qry, "Code", "Name", TxtDocType.arrValueMember, TxtDocType.arrDispalyMember)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    
End Class