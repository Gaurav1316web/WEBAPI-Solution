Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
'================Created By Parteek-==============
'============= Ticket No: BM00000010016 by Parteek'
Public Class rptTankerStatusReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ApplyNotShowJobWorkTypeTanker As Boolean = False
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptDairyProductionWreckageReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)


        If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
            clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
            Exit Sub
        End If

        Dim qry As String = Nothing


        'skg start

        'Dim strQryPre As String = Nothing

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            qry = "select * from (" &
    " Select Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' then TSPL_MCC_Dispatch_Challan.Chalan_NO else 'Not Req' end as [Challan No],TSPL_MCC_Dispatch_Challan.Dispatch_Date as [Challan Date],Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As [Proc Type], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Tspl_Gate_Entry_Details.Vendor_Code End As [Vendor Code], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then TSPL_VENDOR_MASTER.Vendor_Name End As [Vendor Name], IsNull(TSPL_MCC_Dispatch_Challan.MCC_Code, 'Not Req') As [From Location Code], IsNull(fromLocation.Location_Desc, 'Not Req') As [From Location Desc], IsNull(Tspl_Gate_Entry_Details.location_Code, '') As [To Location Code], IsNull(toLocation.Location_Desc, '') As [To Location Desc], IsNull(Tspl_Gate_Entry_Details.Tanker_No, '') As [Tanker No], IsNull(Tspl_Gate_Entry_Details.Gate_Entry_No, '') As [Gate Entry No], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Entry Date], IsNull(GrossWeight.Weighment_No, '') As [Gross Weightment],GrossWeight.Weighment_date As [Weighment Date], IsNull(TareWeight.Weighment_No, '') As [Tare Weighment No], Case When IsNull(TareWeight.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Weighment Posting Status], IsNull(TSPL_QUALITY_CHECK.QC_No, '') As [QC No], TSPL_QUALITY_CHECK.QC_In_Date_Time As [QC Date], Case When IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [QC Posting Status], Case When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.QC_No, '') = '' Then 'QC Pending' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 1 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Accepted' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Rejected' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 2 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'Accepted With Special Approval' Else 'QC Pending' End As [QC Status], IsNull(TSPL_MILK_UNLOADING.Unloading_No, '') As [Unloading No], TSPL_MILK_UNLOADING.Unloading_Date_Time As [Unloading Date], Case When IsNull(TSPL_MILK_UNLOADING.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Unloading Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Cleaning.Doc_No, '') End As [Cleaning No], TSPL_Cleaning.Start_Date_Time As [Cleaning Date], Case When IsNull(TSPL_Cleaning.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Cleaning Posting Status], IsNull(TSPL_Gate_Out.Doc_No, '') As [Gate Out No],TSPL_Gate_Out.Doc_Date As [Gate Out Date], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Bulk_MILK_SRN.SRN_NO, '') End As [SRN No], TSPL_Bulk_MILK_SRN.SRN_Date As [SRN Date], Case When IsNull(TSPL_Bulk_MILK_SRN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [SRN Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No], TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date As [Milk Receipt Challan Date], Case When IsNull(TSPL_MILK_TRANSFER_IN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Milk Receipt Posting Staus], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO, '') End As [PI NO], tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE As [PI Date],TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO as [SRN Return No],TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date as [SRN Return Date],TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No as [Milk Transfer In Return No],TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date  as [Milk Transfer In Return Date] From Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail GrossWeight On GrossWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Weighment_Detail TareWeight On TareWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Cleaning On TSPL_Cleaning.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_LOCATION_MASTER As toLocation On toLocation.Location_Code = Tspl_Gate_Entry_Details.location_Code Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO Left Outer Join TSPL_MCC_Dispatch_Challan On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO Left Outer Join TSPL_LOCATION_MASTER As fromLocation On fromLocation.Location_Code = TSPL_MCC_Dispatch_Challan.MCC_Code Left Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code " &
    "left join TSPL_MILK_TRANSFER_IN_RETURN on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No " &
    "left join TSPL_Bulk_Milk_SRN_Return  on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_Bulk_Milk_SRN_Return.SRN_NO " &
    ")x  where 2=2 "
        Else
            'qry += "Select Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' then TSPL_MCC_Dispatch_Challan.Chalan_NO else 'Not Req' end as [Challan No],TSPL_MCC_Dispatch_Challan.Dispatch_Date as [Challan Date],Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As [Proc Type], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Tspl_Gate_Entry_Details.Vendor_Code End As [Vendor Code], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then TSPL_VENDOR_MASTER.Vendor_Name End As [Vendor Name], IsNull(TSPL_MCC_Dispatch_Challan.MCC_Code, 'Not Req') As [From Location Code], IsNull(fromLocation.Location_Desc, 'Not Req') As [From Location Desc], IsNull(Tspl_Gate_Entry_Details.location_Code, '') As [To Location Code], IsNull(toLocation.Location_Desc, '') As [To Location Desc], IsNull(Tspl_Gate_Entry_Details.Tanker_No, '') As [Tanker No], IsNull(Tspl_Gate_Entry_Details.Gate_Entry_No, '') As [Gate Entry No], Convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Entry Date], IsNull(GrossWeight.Weighment_No, '') As [Gross Weightment], IsNull(Convert(varchar,GrossWeight.Weighment_date,103), '') As [Weighment Date], IsNull(TareWeight.Weighment_No, '') As [Tare Weighment No], Case When IsNull(TareWeight.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Weighment Posting Status], IsNull(TSPL_QUALITY_CHECK.QC_No, '') As [QC No], IsNull(Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103), '') As [QC Date], Case When IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [QC Posting Status], Case When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.QC_No, '') = '' Then 'QC Pending' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 1 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Accepted' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Rejected' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 2 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'Accepted With Special Approval' Else 'QC Pending' End As [QC Status], IsNull(TSPL_MILK_UNLOADING.Unloading_No, '') As [Unloading No], IsNull(Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103), '') As [Unloading Date], Case When IsNull(TSPL_MILK_UNLOADING.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Unloading Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Cleaning.Doc_No, '') End As [Cleaning No], IsNull(Convert(varchar,TSPL_Cleaning.Start_Date_Time,103), '') As [Cleaning Date], Case When IsNull(TSPL_Cleaning.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Cleaning Posting Status], IsNull(TSPL_Gate_Out.Doc_No, '') As [Gate Out No], IsNull(Convert(varchar,TSPL_Gate_Out.Doc_Date,103), '') As [Gate Out Date], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Bulk_MILK_SRN.SRN_NO, '') End As [SRN No], IsNull(Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103), '') As [SRN Date], Case When IsNull(TSPL_Bulk_MILK_SRN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [SRN Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No], IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Case When IsNull(TSPL_MILK_TRANSFER_IN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Milk Receipt Posting Staus], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO, '') End As [PI NO], IsNull(Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103), '') As [PI Date],TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO as [SRN Return No],TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date as [SRN Return Date],TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No as [Milk Transfer In Return No],TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date  as [Milk Transfer In Return Date] From Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail GrossWeight On GrossWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Weighment_Detail TareWeight On TareWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Cleaning On TSPL_Cleaning.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join TSPL_LOCATION_MASTER As toLocation On toLocation.Location_Code = Tspl_Gate_Entry_Details.location_Code Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO Left Outer Join TSPL_MCC_Dispatch_Challan On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO Left Outer Join TSPL_LOCATION_MASTER As fromLocation On fromLocation.Location_Code = TSPL_MCC_Dispatch_Challan.MCC_Code Left Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  " & _
            '    "left join TSPL_MILK_TRANSFER_IN_RETURN on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  " & _
            '    "left join TSPL_Bulk_Milk_SRN_Return  on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_Bulk_Milk_SRN_Return.SRN_NO "


            qry += "select case when x.row_num=1 then x.[Ref Doc No] else x.[Challan No] end as [Tanker Dispatch Ref No], * from (Select DISTINCT ROW_NUMBER() OVER (PARTITION BY TSPL_MCC_Dispatch_Challan.level1challanNo ORDER BY TSPL_MCC_Dispatch_Challan.level1challanNo ) row_num,
TSPL_MCC_Dispatch_Challan.level1challanNo as [Ref Doc No],Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' then TSPL_MCC_Dispatch_Challan.Chalan_NO else 'Not Req' end as [Challan No],TSPL_MCC_Dispatch_Challan.Dispatch_Date as [Challan Date],Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As [Proc Type], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Tspl_Gate_Entry_Details.Vendor_Code End As [Vendor Code], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then TSPL_VENDOR_MASTER.Vendor_Name End As [Vendor Name], IsNull(TSPL_MCC_Dispatch_Challan.MCC_Code, 'Not Req') As [From Location Code], IsNull(fromLocation.Location_Desc, 'Not Req') As [From Location Desc], IsNull(Tspl_Gate_Entry_Details.location_Code, '') As [To Location Code], IsNull(toLocation.Location_Desc, '') As [To Location Desc], IsNull(Tspl_Gate_Entry_Details.Tanker_No, '') As [Tanker No], IsNull(Tspl_Gate_Entry_Details.Gate_Entry_No, '') As [Gate Entry No], Convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Entry Date], IsNull(GrossWeight.Weighment_No, '') As [Gross Weightment], IsNull(Convert(varchar,GrossWeight.Weighment_date,103), '') As [Weighment Date], IsNull(TareWeight.Weighment_No, '') As [Tare Weighment No], Case When IsNull(TareWeight.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Weighment Posting Status], IsNull(TSPL_QUALITY_CHECK.QC_No, '') As [QC No], IsNull(Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103), '') As [QC Date], Case When IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [QC Posting Status], Case When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.QC_No, '') = '' Then 'QC Pending' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 1 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Accepted' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 0 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'QC Rejected' When IsNull(TSPL_QUALITY_CHECK.is_Param_Accepted, 0) = 2 And IsNull(TSPL_QUALITY_CHECK.isPosted, 0) = 1 Then 'Accepted With Special Approval' Else 'QC Pending' End As [QC Status], IsNull(TSPL_MILK_UNLOADING.Unloading_No, '') As [Unloading No], IsNull(Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103), '') As [Unloading Date], Case When IsNull(TSPL_MILK_UNLOADING.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Unloading Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Cleaning.Doc_No, '') End As [Cleaning No], IsNull(Convert(varchar,TSPL_Cleaning.Start_Date_Time,103), '') As [Cleaning Date], Case When IsNull(TSPL_Cleaning.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Cleaning Posting Status], IsNull(TSPL_Gate_Out.Doc_No, '') As [Gate Out No], IsNull(Convert(varchar,TSPL_Gate_Out.Doc_Date,103), '') As [Gate Out Date], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(TSPL_Bulk_MILK_SRN.SRN_NO, '') End As [SRN No], IsNull(Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103), '') As [SRN Date], Case When IsNull(TSPL_Bulk_MILK_SRN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [SRN Posting Status], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No], IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Case When IsNull(TSPL_MILK_TRANSFER_IN.isPosted, 0) = 0 Then 'Not Done' Else 'Done' End As [Milk Receipt Posting Staus], Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') <> 'BulkProc' Then 'Not Req' Else IsNull(tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO, '') End As [PI NO], IsNull(Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103), '') As [PI Date],TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO as [SRN Return No],TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date as [SRN Return Date],TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No as [Milk Transfer In Return No],TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date  as [Milk Transfer In Return Date] From Tspl_Gate_Entry_Details 
Left Outer Join TSPL_Weighment_Detail GrossWeight On GrossWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join TSPL_Weighment_Detail TareWeight On TareWeight.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join TSPL_Cleaning On TSPL_Cleaning.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join TSPL_LOCATION_MASTER As toLocation On toLocation.Location_Code = Tspl_Gate_Entry_Details.location_Code 
Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No 
Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO 
Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO 
Left Outer Join TSPL_MCC_Dispatch_Challan On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO 
Left Outer Join TSPL_MCC_DISPATCH_CHALLAN_DETAIL On TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG>0 
Left Outer Join TSPL_LOCATION_MASTER As fromLocation On fromLocation.Location_Code = TSPL_MCC_Dispatch_Challan.MCC_Code Left Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  " &
                "left join TSPL_MILK_TRANSFER_IN_RETURN on TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  " &
                "left join TSPL_Bulk_Milk_SRN_Return  on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_Bulk_Milk_SRN_Return.SRN_NO "
            If ApplyNotShowJobWorkTypeTanker = True Then
                qry += " where Tspl_Gate_Entry_Details.GATE_ENTRY_TYPE <> 'J' "
            End If
            qry += " )x  where 2=2 "
        End If



        If TxtMultiProcType.arrValueMember IsNot Nothing AndAlso TxtMultiProcType.arrValueMember.Count > 0 Then
            qry += " and [Proc Type] in (" + clsCommon.GetMulcallString(TxtMultiProcType.arrValueMember) + ")  "
        End If

        'IsNull(Tspl_Gate_Entry_Details.Tanker_No, '')
        If TxtMultiTanker.arrValueMember IsNot Nothing AndAlso TxtMultiTanker.arrValueMember.Count > 0 Then
            qry += " and [Tanker No] in (" + clsCommon.GetMulcallString(TxtMultiTanker.arrValueMember) + ")  "
        End If

        'IsNull(TSPL_MCC_Dispatch_Challan.MCC_Code, 'Not Req')
        If TxtMultiFromLocation.arrValueMember IsNot Nothing AndAlso TxtMultiFromLocation.arrValueMember.Count > 0 Then
            qry += " and [From Location Code] in (" + clsCommon.GetMulcallString(TxtMultiFromLocation.arrValueMember) + ")  "
        End If

        'IsNull(Tspl_Gate_Entry_Details.location_Code, '')
        If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
            qry += " and [To Location Code] in (" + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember) + ")  "
        End If

        qry += " and [Gate Entry Date]>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and [Gate Entry Date] <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' "


        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            qry += " order by  [Gate Entry Date]"
        Else
            qry = "select TSPL_MCC_Dispatch_Challan.Mcc_Code as [Tanker Dis From Loc],TSPL_MCC_Dispatch_Challan.Mcc_Name as [Tanker Dis From Loc Name],final.* from ( " & qry & "  )  final
 left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.Chalan_no=final.[Tanker Dispatch Ref No] 
 order by  [Gate Entry Date],row_num"
        End If

        'skg end

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
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv1.DataSource = dt
        SetGridFormationOFGV1()
        gv1.BestFitColumns()
        'FindAndRestoreGridLayout(Me)
        ReStoreGridLayout()
    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True

            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal Then
                gv1.Columns("From Location Code").IsVisible = False
                gv1.Columns("From Location Desc").IsVisible = False
                gv1.Columns("Ref Doc No").IsVisible = False
                gv1.Columns("row_num").IsVisible = False

                gv1.Columns("Tanker Dis From Loc").HeaderText = "From Location Code"
                gv1.Columns("Tanker Dis From Loc Name").HeaderText = "From Location Desc"
            End If


            gv1.Columns(ii).BestFit()
        Next

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Print(Exporter.Refresh)
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
        Reset()
        'Ticket No : ERO/29/03/19-000525 By Prabhakar 
        ApplyNotShowJobWorkTypeTanker = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyNotShowJobWorkTypeTanker, clsFixedParameterCode.ApplyNotShowJobWorkTypeTanker, Nothing)) = 1, True, False)
    End Sub

    Private Sub TxtMultiProcType__My_Click(sender As Object, e As EventArgs) Handles TxtMultiProcType._My_Click
        Dim qry As String = "Select 'Bulk In' as [Code], 'Bulk In' as [Name] UNION ALL Select 'MCC In' as [Code], 'MCC In' as [Name]"
        TxtMultiProcType.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiProc", qry, "Code", "Name", TxtMultiProcType.arrValueMember, TxtMultiProcType.arrDispalyMember)
    End Sub

    Private Sub TxtMultiTanker__My_Click(sender As Object, e As EventArgs) Handles TxtMultiTanker._My_Click
        Dim qry As String = "select Tanker_No as [Code] ,Tanker_Name as [Name] from TSPL_TANKER_MASTER   "
        TxtMultiTanker.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiTanker", qry, "Code", "Name", TxtMultiTanker.arrValueMember, TxtMultiTanker.arrDispalyMember)
    End Sub

    Private Sub TxtMultiFromLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiFromLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiFromLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiFrom", qry, "Code", "Name", TxtMultiFromLocation.arrValueMember, TxtMultiFromLocation.arrDispalyMember)
    End Sub

    Private Sub TxtMultiToLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiToLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiToLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiTo", qry, "Code", "Name", TxtMultiToLocation.arrValueMember, TxtMultiToLocation.arrDispalyMember)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Tanker Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTankerStatusReport & "'"))
                If TxtMultiProcType.arrValueMember IsNot Nothing AndAlso TxtMultiProcType.arrValueMember.Count > 0 Then
                    arrHeader.Add("Proc Type : " + clsCommon.GetMulcallString(TxtMultiProcType.arrValueMember))
                End If

                If TxtMultiTanker.arrValueMember IsNot Nothing AndAlso TxtMultiTanker.arrValueMember.Count > 0 Then
                    arrHeader.Add("Tanker No : " + clsCommon.GetMulcallString(TxtMultiTanker.arrValueMember))
                End If

                If TxtMultiFromLocation.arrValueMember IsNot Nothing AndAlso TxtMultiFromLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("From Location : " + clsCommon.GetMulcallString(TxtMultiFromLocation.arrValueMember))
                End If

                If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                End If

                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Tanker Status Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTankerStatusReport & "'"))
                If TxtMultiProcType.arrValueMember IsNot Nothing AndAlso TxtMultiProcType.arrValueMember.Count > 0 Then
                    arrHeader.Add("Proc Type : " + clsCommon.GetMulcallString(TxtMultiProcType.arrValueMember))
                End If

                If TxtMultiTanker.arrValueMember IsNot Nothing AndAlso TxtMultiTanker.arrValueMember.Count > 0 Then
                    arrHeader.Add("Tanker No : " + clsCommon.GetMulcallString(TxtMultiTanker.arrValueMember))
                End If

                If TxtMultiFromLocation.arrValueMember IsNot Nothing AndAlso TxtMultiFromLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("From Location : " + clsCommon.GetMulcallString(TxtMultiFromLocation.arrValueMember))
                End If

                If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Tanker Status Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
