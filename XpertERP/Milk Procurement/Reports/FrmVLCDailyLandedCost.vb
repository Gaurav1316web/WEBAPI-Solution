Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Imports System.IO
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmVLCDailyLandedCost
    Inherits FrmMainTranScreen
    Dim qry As String
    'Dim btnReset As Boolean = False
    Dim ds As New DataSet()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptDailyLandedCost)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Private Sub FrmVLCDailyLandedCost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LOCATIONRIGTHS()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        LoadData()
    End Sub

    Sub Reset()
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LOCATIONRIGTHS()
        LoadMCC()
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If

    End Sub
   
    Sub LoadData()
        Dim strCodeColumnMax As String = ""
        Try
            If clsCommon.GetDateWithStartTime(dtpFromdate1.Value) > clsCommon.GetDateWithEndTime(dtpToDate.Value) Then
                dtpFromdate1.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            Dim dtCategory As DataTable = Nothing
            dtCategory = clsDBFuncationality.GetDataTable("select Mcc_Name from TSPL_MCC_MASTER order by Mcc_Name")
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumnMax += ","
                End If
                strCodeColumnMax += "sum(convert(decimal(18,2),[" + clsCommon.myCstr(dtCategory.Rows(ii)("Mcc_Name")).Trim() + "])) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("Mcc_Name")).Trim() + "]"
              
            Next

            Dim pivat As String = " Select  STUFF((SELECT ','+'['+ +aa.MCC_NAME +']' from (select distinct  TSPL_MCC_MASTER.MCC_NAME from TSPL_MCC_MASTER )aa order by aa.MCC_NAME  FOR XML PATH ('')) , 1, 1, '') as MccNameList"
            Dim pivatresult = clsDBFuncationality.getSingleValue(pivat)
            'qry = " with TempTable as (Select 'Raw Milk(Ltr)' as Particulars,(final.[Milk Weight(LTR)]) as netamount,([MCC Name]) as [MCC Name], ([FAT(%)]) as [Fat %],(final.[SNF(%)]) as  [SNF %] From ( Select TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL    From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code where 2 = 2  and TSPL_MILK_RECEIPT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  ) and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null)  As final )  "

            'qry += "select Particulars," + strCodeColumnMax + "  from (select * from temptable union select 'FAT KG',netamount*[fat %]/100 as netamount,[mcc name],[fat %],[snf %] from TempTable union select 'SNF KG',netamount*[snf %]/100 as netamount,[mcc name],[fat %],[snf %] from TempTable )final12"
            'qry += " pivot(sum(final12.netamount) for final12.[Mcc Name] in (" + pivatresult + "))as t group by Particulars"

            qry = "Select case when coalesce([mcc name],'')='' then 'Total' else 'MCC Milk'+char(10)+'MCC Fat Kg'+char(10)+'MCC SNF Kg' end as doctype,([MCC Name]) as [MCC Name],sum(convert(decimal(18,4),final.[Milk Weight(LTR)])) as netamount, avg(convert(decimal(18,4),[FAT(%)])) as [Fat %],avg(convert(decimal(18,4),final.[SNF(%)])) as  [SNF %] ,sum(convert(decimal(18,4),final.[Milk Weight(LTR)])) * avg(convert(decimal(18,4),[FAT(%)])) /100 as [Fat Kg],sum(convert(decimal(18,4),final.[Milk Weight(LTR)])) * avg(convert(decimal(18,4),[SNF(%)])) /100 as [SNF Kg] ,sum(convert(decimal(18,4),final.[Milk Weight(LTR)]))+sum(convert(decimal(18,4),final.[Milk Weight(LTR)])) * avg(convert(decimal(18,4),[FAT(%)])) /100 +sum(convert(decimal(18,4),final.[Milk Weight(LTR)])) * avg(convert(decimal(18,4),[SNF(%)])) /100 as Total"

            qry += " From ( Select TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL    From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code where 2 = 2  and TSPL_MILK_RECEIPT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  ) and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null  "
            If cbgMCC.CheckedValue.Count > 0 Then
                qry += "and TSPL_MILK_RECEIPT_HEAD.MCC_CODE  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            qry += " )As final group by [MCC Name]"


            Dim qry2 As String = "select distinct 'Basic Rate Rs./Ltr' as doctype,(TSPL_MCC_MASTER.MCC_NAME) as MCC_Code ,tspl_fat_snf_Uploader_master.Date,TSPL_MILK_PRICE_MASTER.Declared_Rate from tspl_fat_snf_Uploader_master left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=tspl_fat_snf_Uploader_master.Price_Code left outer join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=tspl_fat_snf_Uploader_master.Code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code  where tspl_fat_snf_Uploader_master.Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and tspl_fat_snf_Uploader_master.Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            If cbgMCC.CheckedValue.Count > 0 Then
                qry2 += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            qry2 += "   union"

            qry2 += "  Select distinct 'Basic Rate Rs./k.g' as doctype,(TSPL_MCC_MASTER.MCC_NAME) as MCC_Code ,tspl_fat_snf_Uploader_master.Date,TSPL_MILK_PRICE_MASTER.Declared_Rate             from tspl_fat_snf_Uploader_master left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=tspl_fat_snf_Uploader_master.Price_Code left outer join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=tspl_fat_snf_Uploader_master.Code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code where tspl_fat_snf_Uploader_master.Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and tspl_fat_snf_Uploader_master.Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "'"
            If cbgMCC.CheckedValue.Count > 0 Then
                qry2 += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            qry2 += " union"

            qry2 += " Select distinct 'Commission Rs./Ltr' as doctype,(TSPL_MCC_MASTER.MCC_NAME) as MCC_Code ,tspl_fat_snf_Uploader_master.Date,'3' as Commission             from tspl_fat_snf_Uploader_master left outer join TSPL_MILK_PRICE_MASTER on TSPL_MILK_PRICE_MASTER.Price_Code=tspl_fat_snf_Uploader_master.Price_Code left outer join TSPL_FAT_SNF_UPLOADER_MCC on TSPL_FAT_SNF_UPLOADER_MCC.Code=tspl_fat_snf_Uploader_master.Code left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code where tspl_fat_snf_Uploader_master.Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' and tspl_fat_snf_Uploader_master.Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(dtpFromdate1.Value), "dd/MMM/yyyy hh:mm tt") + "' "
            If cbgMCC.CheckedValue.Count > 0 Then
                qry2 += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If

            
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry2)

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, dt, "rptVlcDailyLanded", "Vlc Landed Cost", "rptVLCDLandedCostSub2.rpt", "rptVLCDailyLandedSub.rpt")
                    frmCRV = Nothing
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
           
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = "Company : " & objCommonVar.CurrentCompanyName ' clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" & clsUserMgtCode.RptDailyStanderdMilkQtyMCCWise & "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
  
    Private Sub chkMCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_MCC_MASTER.MCC_Code as [Code] ,TSPL_MCC_MASTER .MCC_Name as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If
        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"

    End Sub
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

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
