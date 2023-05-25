Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
'Created By Sanjay - Create New report 
Public Class rptCollectionDataChangeReport
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try
            If txtFromDate.Value > txtToDate.Value Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If
            ',UpdateData.[Milk Weight(KG)] as [Milk Weight(KG) Update], UpdateData.[Milk Weight(LTR)]  as [Milk Weight(LTR) Update]
            ',final.[Milk Weight(KG)], final.[Milk Weight(LTR)]  as [Milk Weight(LTR)]
            'Dim qry As String = "select HistData.* " & _
            '    ",UpdateData.[Vlc Uploader Code] as [Vlc Uploader Code Update] " & _
            '    ",UpdateData.[Vlc Code] as [Vlc Code Update],UpdateData.[VLC Name] as [VLC Name Update], UpdateData.[Sample No] as [Sample No Update] " & _
            '    ",UpdateData.[No Of Cans] as [No Of Cans Update],UpdateData.[Item Code] as [Item Code Update],UpdateData.[Item Desc] as [Item Desc Update] " & _
            '    ",UpdateData.[Milk Weight] as [Milk Weight Update] " & _
            '    ", UpdateData.[FAT(%)] as [FAT(%) Update] ,UpdateData.CLR as [CLR Update],UpdateData.[SNF(%)] as [SNF(%) Update] " & _
            '    ",UpdateData.[FAT(KG)] as [FAT(KG) Update],UpdateData.[SNF(KG)] as [SNF(KG) Update] " & _
            '    ",UpdateData.[Milk Type] as [Milk Type Update],UpdateData.[SRN No] as [SRN No Update],UpdateData.[SRN Amount] as [SRN Amount Update] " & _
            '    ", UpdateData.[SRN Qty] as [SRN Qty Update],UpdateData.[SRN Rate] as [SRN Rate Update],UpdateData.[Handling Charges] as [Handling Charges Update] " & _
            '    ",UpdateData.[Price Code] as [Price Code Update] " & _
            '               " from " & _
            '    "(Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[Doc Date] ,final.Shift  " & _
            '    ",final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] , final.[Sample No] ,final.[No Of Cans] ,final.Item_Code as [Item Code],final.Item_Desc as [Item Desc],final.[Milk Weight], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)]  " & _
            '    ",final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate] " & _
            '    ",Handling_Charges_Amount as [Handling Charges],final.[Price Code]  " & _
            '               " From " & _
            '    "(Select TSPL_MILK_SRN_DETAIL_hist_data.Item_Code,TSPL_ITEM_MASTER.Item_Desc " & _
            '    ", Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC " & _
            '    " , TSPL_MCC_MASTER.MCC_NAME As [MCC Name] " & _
            '    " ,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift " & _
            '    ",TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR,  Convert(decimal(18,3), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,3),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)] " & _
            '    ", TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL_hist_data.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL_hist_data.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL_hist_data.Qty As [SRN Qty] " & _
            '    " ,isnull( TSPL_MILK_SRN_DETAIL_hist_data.EMP_Amount,0) as EMP_Amount " & _
            '    " ,isnull(TSPL_MILK_SRN_DETAIL_hist_data.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " & _
            '    " ,TSPL_MILK_SRN_DETAIL_hist_data.Price_Code as [Price Code] " & _
            '              "  From TSPL_MILK_RECEIPT_DETAIL " & _
            '    " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " & _
            '    " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
            '    " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  " & _
            '    " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO  " & _
            '    " Left Outer Join TSPL_MILK_SRN_DETAIL_hist_data On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL_hist_data.DOC_CODE " & _
            '    " inner join( select DOC_CODE, max(PK_Id) id from TSPL_MILK_SRN_DETAIL_hist_data group by DOC_CODE " & _
            '    " ) ss on TSPL_MILK_SRN_DETAIL_hist_data.PK_Id = ss.id and TSPL_MILK_SRN_DETAIL_hist_data.DOC_CODE = ss.DOC_CODE " & _
            '    " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL_hist_data.item_code  " & _
            '    " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " & _
            '    " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " & _
            '    " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
            '    " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & _
            '    " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
            '    " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " & _
            '    " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
            '    " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
            '    " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
            '    " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  " & _
            '    " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL_hist_data.Price_Code  " & _
            '    " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL_hist_data.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per  " & _
            '    " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  " & _
            '    " where 2 = 2  "
            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    qry += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            'End If
            'If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MILK_RECEIPT_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            'End If
            'If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
            'End If
            'qry += " and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'"
            'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            '    qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
            'End If
            'If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            '    qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
            'End If

            'qry += " ) As final where 2=2  " & _
            '    " ) HistData " & _
            '    " inner join " & _
            '    " (Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name] " & _
            '     " ,final.[Doc Date] ,final.Shift  " & _
            '    ",final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] , final.[Sample No] ,final.[No Of Cans] ,final.Item_Code as [Item Code],final.Item_Desc as [Item Desc],final.[Milk Weight], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)]  " & _
            '    ",final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate] " & _
            '    ",Handling_Charges_Amount as [Handling Charges]" & _
            '    ",final.[Price Code]    " & _
            '               " From " & _
            '    " (Select TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc " & _
            '    ", Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type] " & _
            '    ", TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name] " & _
            '     ",  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift " & _
            '    ",TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], TSPL_MILK_SAMPLE_DETAIL.CLR,  Convert(decimal(18,3), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,3),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)] " & _
            '    ", TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty] " & _
            '     ",isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount " & _
            '     ",isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " & _
            '     ",TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code] " & _
            '     " From TSPL_MILK_RECEIPT_DETAIL  " & _
            '    " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " & _
            '    " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
            '    " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  " & _
            '    " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO  " & _
            '    " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " & _
            '     " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  " & _
            '    " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " & _
            '    " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " & _
            '    " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
            '    " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & _
            '    " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
            '    " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " & _
            '    " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
            '    " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
            '    " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
            '    " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  " & _
            '    " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code  " & _
            '    " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per  " & _
            '    " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  where 2 = 2  "
            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    qry += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            'End If
            'If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MILK_RECEIPT_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            'End If
            'If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
            'End If
            'qry += " and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'"
            'If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            '    qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
            'End If
            'If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            '    qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
            'End If
            'qry += " ) As final where 2=2 " & _
            '    " )UpdateData " & _
            '    " on UpdateData.[Milk Receipt Code]=HistData.[Milk Receipt Code] " & _
            '    " and UpdateData.[MCC Code]=HistData.[MCC Code] " & _
            '    " and UpdateData.[Doc Date]=HistData.[Doc Date] " & _
            '    " and UpdateData.Shift=HistData.Shift " & _
            '    "  and UpdateData.[SRN No]=HistData.[SRN No]	 " & _
            '    "  order by HistData.[Doc Date],HistData.[Milk Receipt Code] ,HistData.[Sample No]  "

            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim qry As String = "select HistData.[Milk Receipt Code],HistData.[MCC Code] ,HistData.[MCC Name] ,HistData.[Doc Date] ,HistData.Shift" & _
                ",HistData.[Vlc Uploader Code] as [Vlc Uploader Code] " & _
               ",HistData.[Vlc Code] as [Vlc Code],HistData.[VLC Name] as [VLC Name], HistData.[Sample No] as [Sample No] " & _
               ",HistData.[No Of Cans] as [No Of Cans],HistData.[Item Code] as [Item Code],HistData.[Item Desc] as [Item Desc] " & _
               ",HistData.[Milk Weight] as [Milk Weight] " & _
               ", HistData.[FAT(%)] as [FAT(%)] ,HistData.CLR as [CLR],HistData.[SNF(%)] as [SNF(%)] " & _
               ",HistData.[FAT(KG)] as [FAT(KG)],HistData.[SNF(KG)] as [SNF(KG)] " & _
               ",HistData.[Milk Type] as [Milk Type],HistData.[SRN No] as [SRN No],HistData.[SRN Amount] as [SRN Amount] " & _
               ", HistData.[SRN Qty] as [SRN Qty],HistData.[SRN Rate] as [SRN Rate],HistData.[Handling Charges] as [Handling Charges] " & _
               ",HistData.[Price Code] as [Price Code] " & _
               "" & _
               ",UpdateData.[Vlc Uploader Code] as [Vlc Uploader Code Update] " & _
               ",UpdateData.[Vlc Code] as [Vlc Code Update],UpdateData.[VLC Name] as [VLC Name Update], UpdateData.[Sample No] as [Sample No Update] " & _
               ",UpdateData.[No Of Cans] as [No Of Cans Update],UpdateData.[Item Code] as [Item Code Update],UpdateData.[Item Desc] as [Item Desc Update] " & _
               ",UpdateData.[Milk Weight] as [Milk Weight Update] " & _
               ", UpdateData.[FAT(%)] as [FAT(%) Update] ,UpdateData.CLR as [CLR Update],UpdateData.[SNF(%)] as [SNF(%) Update] " & _
               ",UpdateData.[FAT(KG)] as [FAT(KG) Update],UpdateData.[SNF(KG)] as [SNF(KG) Update] " & _
               ",UpdateData.[Milk Type] as [Milk Type Update],UpdateData.[SRN No] as [SRN No Update],UpdateData.[SRN Amount] as [SRN Amount Update] " & _
               ", UpdateData.[SRN Qty] as [SRN Qty Update],UpdateData.[SRN Rate] as [SRN Rate Update],UpdateData.[Handling Charges] as [Handling Charges Update] " & _
               ",UpdateData.[Price Code] as [Price Code Update] " & _
               " ,HistData.[Modify By],HistData.[Modify Date]" & _
                          " from " & _
               "(Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name],final.[Doc Date] ,final.Shift  " & _
               ",final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] , final.[Sample No] ,final.[No Of Cans] ,final.Item_Code as [Item Code],final.Item_Desc as [Item Desc],final.[Milk Weight], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)]  " & _
               ",final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate] " & _
               ",Handling_Charges_Amount as [Handling Charges],final.[Price Code],final.[Modify By],final.[Modify Date]  " & _
                          " From " & _
               "(Select TSPL_MILK_SRN_DETAIL_hist_data.Item_Code,TSPL_ITEM_MASTER.Item_Desc " & _
               ", Case When Coalesce(TSPL_MILK_SRN_DETAIL_hist_data.FAT_PER, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SRN_DETAIL_hist_data.FAT_PER, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC " & _
               " , TSPL_MCC_MASTER.MCC_NAME As [MCC Name] " & _
               " ,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift " & _
               ",TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL_hist_data.ACC_Qty As [Milk Weight], TSPL_MILK_SRN_DETAIL_hist_data.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL_hist_data.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL_hist_data.CLR as CLR,  Convert(decimal(18,3), TSPL_MILK_SRN_DETAIL_hist_data.FAT_PER * TSPL_MILK_SRN_DETAIL_hist_data.ACC_Qty / 100) As [FAT(KG)], Convert(decimal(18,3),TSPL_MILK_SRN_DETAIL_hist_data.SNF_PER * TSPL_MILK_SRN_DETAIL_hist_data.ACC_Qty / 100) As [SNF(KG)] " & _
               ", TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL_hist_data.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL_hist_data.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL_hist_data.Qty As [SRN Qty] " & _
               " ,isnull( TSPL_MILK_SRN_DETAIL_hist_data.EMP_Amount,0) as EMP_Amount " & _
               " ,isnull(TSPL_MILK_SRN_DETAIL_hist_data.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " & _
               " ,TSPL_MILK_SRN_DETAIL_hist_data.Price_Code as [Price Code] " & _
               " ,TSPL_MILK_SRN_DETAIL_hist_data.Hist_By as [Modify By],TSPL_MILK_SRN_DETAIL_hist_data.Hist_On as [Modify Date] " & _
                         "  From TSPL_MILK_RECEIPT_DETAIL " & _
               " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " & _
               " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
               " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  " & _
               " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO  " & _
               " Left Outer Join TSPL_MILK_SRN_DETAIL_hist_data On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL_hist_data.DOC_CODE " & _
               " inner join( select DOC_CODE, min(Hist_Version) id from TSPL_MILK_SRN_DETAIL_hist_data group by DOC_CODE " & _
               " ) ss on TSPL_MILK_SRN_DETAIL_hist_data.Hist_Version = ss.id and TSPL_MILK_SRN_DETAIL_hist_data.DOC_CODE = ss.DOC_CODE " & _
               " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL_hist_data.item_code  " & _
               " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE " & _
               " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " & _
               " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
               " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & _
               " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
               " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " & _
               " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
               " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
               " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
               " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  " & _
               " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL_hist_data.Price_Code  " & _
               " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL_hist_data.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per  " & _
               " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  " & _
               " where 2 = 2  "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_RECEIPT_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
            End If
            qry += " and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'"
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
            End If

            qry += " ) As final where 2=2  " & _
                " ) HistData " & _
                " inner join " & _
                " (Select final.[Milk Receipt Code] ,final.MCC as [MCC Code] ,final.[MCC Name] " & _
                 " ,final.[Doc Date] ,final.Shift  " & _
                ",final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name] , final.[Sample No] ,final.[No Of Cans] ,final.Item_Code as [Item Code],final.Item_Desc as [Item Desc],final.[Milk Weight], final.[FAT(%)]  ,final.CLR,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)]  " & _
                ",final.[Milk Type],final.[SRN No],final.[SRN Amount], final.[SRN Qty],final.[SRN Rate] " & _
                ",Handling_Charges_Amount as [Handling Charges]" & _
                ",final.[Price Code]    " & _
                           " From " & _
                " (Select TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc " & _
                ", Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 5 Then 'C' Else 'B' End As [Milk Type] " & _
                ", TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name] " & _
                 ",  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift " & _
                ",TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,  Convert(decimal(18,3), TSPL_MILK_SRN_DETAIL.FAT_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [FAT(KG)], Convert(decimal(18,3),TSPL_MILK_SRN_DETAIL.SNF_PER * TSPL_MILK_SRN_DETAIL.ACC_Qty / 100) As [SNF(KG)] " & _
                ", TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty] " & _
                 ",isnull( TSPL_MILK_SRN_DETAIL.EMP_Amount,0) as EMP_Amount " & _
                 ",isnull(TSPL_MILK_SRN_DETAIL.Round_Off,0) as Round_Off,isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount,0) as Handling_Charges_Amount " & _
                 ",TSPL_MILK_SRN_DETAIL.Price_Code as [Price Code] " & _
                 " From TSPL_MILK_RECEIPT_DETAIL  " & _
                " Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " & _
                " Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE " & _
                " Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  " & _
                " Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO  " & _
                " Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE " & _
                 " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code  " & _
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  " & _
                " Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  " & _
                " Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
                " Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE " & _
                " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE " & _
                " Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE " & _
                " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
                " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " & _
                " Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  " & _
                " And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  " & _
                " And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  left outer join (select code,max(Price_code) as Price_code from  TSPL_FAT_SNF_UPLOADER_MASTER group by code) as TabTSPL_FAT_SNF_UPLOADER_MASTER on TabTSPL_FAT_SNF_UPLOADER_MASTER.code=TSPL_MILK_SRN_DETAIL.Price_Code  " & _
                " left outer join TSPL_MILK_PRICE_SNF_DEDUCTION on TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TabTSPL_FAT_SNF_UPLOADER_MASTER.Price_code and cast(TSPL_MILK_SRN_DETAIL.SNF_PER as decimal(18,1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per  " & _
                " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code  where 2 = 2  "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_RECEIPT_DETAIL.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            End If
            If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                qry += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  "
            End If
            qry += " and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "'"
            If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
            End If
            If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
                qry += " and 2=( case when Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
            End If
            qry += " ) As final where 2=2 " & _
                " )UpdateData " & _
                " on UpdateData.[Milk Receipt Code]=HistData.[Milk Receipt Code] " & _
                " and UpdateData.[MCC Code]=HistData.[MCC Code] " & _
                " and UpdateData.[Doc Date]=HistData.[Doc Date] " & _
                " and UpdateData.Shift=HistData.Shift " & _
                "  and UpdateData.[SRN No]=HistData.[SRN No]	 " & _
                "  order by HistData.[Doc Date],HistData.[Milk Receipt Code] ,HistData.[Sample No]  "



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
            ReStoreGridLayout()
            View()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    

    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).BestFit()
        Next

        'gv1.Columns("Milk Receipt Code").IsPinned = True
        'gv1.Columns("MCC Code").IsPinned = True
        'gv1.Columns("MCC Name").IsPinned = True
        'gv1.Columns("Doc Date").IsPinned = True
        'gv1.Columns("Shift").IsPinned = True

        Dim obj1 As New ExpressionFormattingObject("MyCondition1", "[Vlc Uploader Code]<>[Vlc Uploader Code Update]", False)
        obj1.CellForeColor = Color.Red
        gv1.Columns("Vlc Uploader Code Update").ConditionalFormattingObjectList.Add(obj1)

        Dim obj2 As New ExpressionFormattingObject("MyCondition2", "[Vlc Code]<>[Vlc Code Update]", False)
        obj2.CellForeColor = Color.Red
        gv1.Columns("Vlc Code Update").ConditionalFormattingObjectList.Add(obj2)

        Dim obj3 As New ExpressionFormattingObject("MyCondition3", "[VLC Name]<>[VLC Name Update]", False)
        obj3.CellForeColor = Color.Red
        gv1.Columns("VLC Name Update").ConditionalFormattingObjectList.Add(obj3)

        Dim obj4 As New ExpressionFormattingObject("MyCondition4", "[Sample No]<>[Sample No Update]", False)
        obj4.CellForeColor = Color.Red
        gv1.Columns("Sample No Update").ConditionalFormattingObjectList.Add(obj4)

        Dim obj5 As New ExpressionFormattingObject("MyCondition5", "[No Of Cans]<>[No Of Cans Update]", False)
        obj5.CellForeColor = Color.Red
        gv1.Columns("No Of Cans Update").ConditionalFormattingObjectList.Add(obj5)

        Dim obj6 As New ExpressionFormattingObject("MyCondition6", "[Item Code]<>[Item Code Update]", False)
        obj6.CellForeColor = Color.Red
        gv1.Columns("Item Code Update").ConditionalFormattingObjectList.Add(obj6)

        Dim obj7 As New ExpressionFormattingObject("MyCondition7", "[Item Desc]<>[Item Desc Update]", False)
        obj7.CellForeColor = Color.Red
        gv1.Columns("Item Desc Update").ConditionalFormattingObjectList.Add(obj7)

        Dim obj8 As New ExpressionFormattingObject("MyCondition8", "[Milk Weight]<>[Milk Weight Update]", False)
        obj8.CellForeColor = Color.Red
        gv1.Columns("Milk Weight Update").ConditionalFormattingObjectList.Add(obj8)

        Dim obj9 As New ExpressionFormattingObject("MyCondition9", "[FAT(%)]<>[FAT(%) Update]", False)
        obj9.CellForeColor = Color.Red
        gv1.Columns("FAT(%) Update").ConditionalFormattingObjectList.Add(obj9)

        Dim obj10 As New ExpressionFormattingObject("MyCondition10", "[CLR]<>[CLR Update]", False)
        obj10.CellForeColor = Color.Red
        gv1.Columns("CLR Update").ConditionalFormattingObjectList.Add(obj10)

        Dim obj11 As New ExpressionFormattingObject("MyCondition11", "[SNF(%)]<>[SNF(%) Update]", False)
        obj11.CellForeColor = Color.Red
        gv1.Columns("SNF(%) Update").ConditionalFormattingObjectList.Add(obj11)

        Dim obj12 As New ExpressionFormattingObject("MyCondition12", "[FAT(KG)]<>[FAT(KG) Update]", False)
        obj12.CellForeColor = Color.Red
        gv1.Columns("FAT(KG) Update").ConditionalFormattingObjectList.Add(obj12)

        Dim obj13 As New ExpressionFormattingObject("MyCondition13", "[SNF(KG)]<>[SNF(KG) Update]", False)
        obj13.CellForeColor = Color.Red
        gv1.Columns("SNF(KG) Update").ConditionalFormattingObjectList.Add(obj13)

        Dim obj14 As New ExpressionFormattingObject("MyCondition14", "[Milk Type]<>[Milk Type Update]", False)
        obj14.CellForeColor = Color.Red
        gv1.Columns("Milk Type Update").ConditionalFormattingObjectList.Add(obj14)

        Dim obj15 As New ExpressionFormattingObject("MyCondition15", "[SRN No]<>[SRN No Update]", False)
        obj15.CellForeColor = Color.Red
        gv1.Columns("SRN No Update").ConditionalFormattingObjectList.Add(obj15)

        Dim obj16 As New ExpressionFormattingObject("MyCondition16", "[SRN Amount]<>[SRN Amount Update]", False)
        obj16.CellForeColor = Color.Red
        gv1.Columns("SRN Amount Update").ConditionalFormattingObjectList.Add(obj16)

        Dim obj17 As New ExpressionFormattingObject("MyCondition17", "[SRN Qty]<>[SRN Qty Update]", False)
        obj17.CellForeColor = Color.Red
        gv1.Columns("SRN Qty Update").ConditionalFormattingObjectList.Add(obj17)

        Dim obj18 As New ExpressionFormattingObject("MyCondition18", "[SRN Rate]<>[SRN Rate Update]", False)
        obj18.CellForeColor = Color.Red
        gv1.Columns("SRN Rate Update").ConditionalFormattingObjectList.Add(obj18)

        Dim obj19 As New ExpressionFormattingObject("MyCondition19", "[Handling Charges]<>[Handling Charges Update]", False)
        obj19.CellForeColor = Color.Red
        gv1.Columns("Handling Charges Update").ConditionalFormattingObjectList.Add(obj19)

        Dim obj20 As New ExpressionFormattingObject("MyCondition20", "[Price Code]<>[Price Code Update]", False)
        obj20.CellForeColor = Color.Red
        gv1.Columns("Price Code Update").ConditionalFormattingObjectList.Add(obj20)

        gv1.Columns("Vlc Uploader Code Update").HeaderText = "Vlc Uploader Code"
        gv1.Columns("Vlc Code Update").HeaderText = "Vlc Code"
        gv1.Columns("VLC Name Update").HeaderText = "VLC Name"
        gv1.Columns("Sample No Update").HeaderText = "Sample No"
        gv1.Columns("No Of Cans Update").HeaderText = "No Of Cans"
        gv1.Columns("Item Code Update").HeaderText = "Item Code"
        gv1.Columns("Item Desc Update").HeaderText = "Item Desc"
        gv1.Columns("Milk Weight Update").HeaderText = "Milk Weight"
        gv1.Columns("FAT(%) Update").HeaderText = "FAT(%)"
        gv1.Columns("CLR Update").HeaderText = "CLR"
        gv1.Columns("SNF(%) Update").HeaderText = "SNF(%)"
        gv1.Columns("FAT(KG) Update").HeaderText = "FAT(KG)"
        gv1.Columns("SNF(KG) Update").HeaderText = "SNF(KG)"
        gv1.Columns("Milk Type Update").HeaderText = "Milk Type"
        gv1.Columns("SRN No Update").HeaderText = "SRN No"
        gv1.Columns("SRN Amount Update").HeaderText = "SRN Amount"
        gv1.Columns("SRN Qty Update").HeaderText = "SRN Qty"
        gv1.Columns("SRN Rate Update").HeaderText = "SRN Rate"
        gv1.Columns("Handling Charges Update").HeaderText = "Handling Charges"
        gv1.Columns("Price Code Update").HeaderText = "Price Code"

    End Sub
    Sub View()
        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Milk Receipt Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("MCC Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("MCC Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Doc Date").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Shift").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Original"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Vlc Uploader Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Vlc Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("VLC Name").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Sample No").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("No Of Cans").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Item Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Item Desc").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Milk Weight").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FAT(%)").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("CLR").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNF(%)").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FAT(KG)").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNF(KG)").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Milk Type").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SRN No").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SRN Amount").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SRN Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SRN Rate").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Handling Charges").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Price Code").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Updated"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Vlc Uploader Code Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Vlc Code Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("VLC Name Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Sample No Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("No Of Cans Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Item Code Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Item Desc Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Milk Weight Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("FAT(%) Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("CLR Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SNF(%) Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("FAT(KG) Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SNF(KG) Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Milk Type Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SRN No Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SRN Amount Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SRN Qty Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SRN Rate Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Handling Charges Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Price Code Update").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Modify By").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Modify Date").Name)
            gv1.ViewDefinition = view
        End If
    End Sub
    


    Sub Reset()
        txtMCC.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtVLC.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
        'cbgShift.DisplayMember = "Shift"
    End Sub

    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
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
        LoadShiftFrom()
        LoadShiftTo()
        Reset()
    End Sub



    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & Me.Text)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                transportSql.exportdata(gv1, "", Me.Text, , arrHeader, False, False, True)
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
                arrHeader.Add("Name : " & Me.Text)
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("MCC : " + clsCommon.GetMulcallStringWithComma(txtMCC.arrDispalyMember) + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If
                If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
                    arrHeader.Add(("VLC : " + clsCommon.GetMulcallStringWithComma(txtVLC.arrDispalyMember) + " "))
                End If


                'clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        RefreshRoute()
        RefreshVLC()
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select Route_Code,Route_Name from TSPL_MCC_ROUTE_MASTER where 2=2 "
            If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "Route_Code", "Route_Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVLC__My_Click(sender As Object, e As EventArgs) Handles txtVLC._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            End If

            txtVLC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC", qry, "VLC_Code", "VLC_Name", txtVLC.arrValueMember, txtVLC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub RefreshRoute()
        If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            Dim qry As String = "select Route_Code from TSPL_MCC_ROUTE_MASTER where Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtRoute.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("Route_Code")))
                Next
                txtRoute.arrValueMember = arr
            End If
        End If
    End Sub

    Sub RefreshVLC()
        If txtVLC.arrValueMember IsNot Nothing AndAlso txtVLC.arrValueMember.Count > 0 Then
            Dim qry As String = "select VLC_Code from TSPL_VLC_MASTER_HEAD where  VLC_Code in (" + clsCommon.GetMulcallString(txtVLC.arrValueMember) + ")  and Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtVLC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("VLC_Code")))
                Next
                txtVLC.arrValueMember = arr
            End If
        End If
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

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells("SRN No").Value)) > 0 Then
                clsERPFuncationalityold.ShowTransHistoryData(clsCommon.myCstr(gv1.CurrentRow.Cells("SRN No").Value), "DOC_CODE", "TSPL_MILK_SRN_HEAD", "TSPL_MILK_SRN_DETAIL")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

End Class
