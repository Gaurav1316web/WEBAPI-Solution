Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===================Created By Preeti Gupta  update by preeti ticket no[BM00000008439]===========================
Public Class RptBulkMilkRegister
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing
    Dim ShowFatSnfAfterApproval As Boolean = False
    Dim ApplyTransportChargeAddInActualAmount As Boolean = False
    Public FilterON As Boolean = False
    Public FilterfromDate As Date
    Public FilterToDate As Date
    Public FilterArrDocType As ArrayList
    Public FilterLocationCode As String
    Public FilterVendorName As String
#End Region

    Private Sub RptBulkMilkRegister_Load(
                                        sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        ShowFatSnfAfterApproval = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowFatSnfAfterApproval, clsFixedParameterCode.ShowFatSnfAfterApproval, Nothing)) = 1, True, False)
        ApplyTransportChargeAddInActualAmount = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTransportChargeAddInActualAmount, clsFixedParameterCode.ApplyTransportChargeAddInActualAmount, Nothing)) = "1", True, False)
        If FilterON Then
            txtFromDate.Value = FilterfromDate
            txtToDate.Value = FilterToDate
            chkDocTypeSelect.IsChecked = True
            cbgDocType.CheckedValue = FilterArrDocType
            btnGo.PerformClick()
            If clsCommon.myLen(FilterVendorName) > 0 Then
                Dim filter1 As New FilterDescriptor()
                filter1.PropertyName = "To MCC or Plant Code"
                filter1.[Operator] = FilterOperator.IsEqualTo
                filter1.Value = FilterLocationCode
                filter1.IsFilterEditor = True
                gv.FilterDescriptors.Add(filter1)
                Dim filter2 As New FilterDescriptor()
                filter2.PropertyName = "Vendor Name"
                filter2.[Operator] = FilterOperator.IsEqualTo
                filter2.Value = FilterVendorName.Substring(FilterVendorName.LastIndexOf(" - ") + 3)
                filter2.IsFilterEditor = True
                gv.FilterDescriptors.Add(filter2)
            ElseIf clsCommon.myLen(FilterLocationCode) > 0 Then
                Dim filter As New FilterDescriptor()
                filter.PropertyName = "From MCC or Plant Code"
                filter.[Operator] = FilterOperator.IsEqualTo
                filter.Value = FilterLocationCode
                filter.IsFilterEditor = True
                gv.FilterDescriptors.Add(filter)
            End If

        End If
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
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.RptBulkMilkRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        radbtnBulkExp.Visible = MyBase.isExport
    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If
        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"

    End Sub
    Sub LoadDocType()
        Dim qry As String = "Select xxx.Code,  xxx.Name From (Select 'Bulk In' As Code, 'Bulk In' As Name  Union  Select 'MCC In' As Code,    'MCC In' As Name Union  Select 'MCC Ret' As Code,    'MCC Ret' As Name union select 'Bulk Ret' as Code,'Bulk Ret' as Name) xxx "
        cbgDocType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocType.ValueMember = "Code"
        cbgDocType.DisplayMember = "Name"

    End Sub
    Sub LoadVendor()
        Dim qry As String = "Select TSPL_VENDOR_MASTER.Vendor_Code As Code,  TSPL_VENDOR_MASTER.Vendor_Name As Name From TSPL_VENDOR_MASTER WHERE Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"
    End Sub
    ' Ticket No : ERO/15/10/19-001058 By Prabhakar
    Public Sub Load_Report(Optional ByVal BulkExport As Integer = 0)
        Try
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If
            If chkDocTypeSelect.IsChecked AndAlso cbgDocType.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single Doc Type or select all.", Me.Text)
                Exit Sub
            End If
            If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
                Exit Sub
            End If
            If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor or select all.", Me.Text)
                Exit Sub
            End If
            'KUNAL > TICKET : BM00000009895 > DATE : 18-NOV-2016
            Dim dtExtraColumn As DataTable = clsDBFuncationality.GetDataTable("select description from tspl_parameter_master where type not in('FAT','SNF','CLR')")

            Dim TankerFromMaster As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.GateEntryTankerFromTankerMaster, clsFixedParameterCode.GateEntryTankerFromTankerMaster, Nothing))
            Dim SettCalculateLtrQtyFromKGQtyByCLR As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CalculateLtrQtyFromKGQtyByCLR, clsFixedParameterCode.CalculateLtrQtyFromKGQtyByCLR, Nothing)) > 0)
            Dim Pivot As String = clsDBFuncationality.getSingleValue("select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct (select '['+description+'],' from (select description,type from tspl_parameter_master union all select 'colRemarks','REM')tspl_parameter_master where type not in('FAT','SNF','CLR') for xml path('')) as xvalue)a")
            Dim SQuery As String = Nothing
            Dim strMilkTransportAmountColumnInner As String = ""
            Dim strMilkTransportAmountColumnOuter As String = ""
            '======update by preeti gupta Against ticket no[ERO/16/07/19-000949]
            If TankerFromMaster = 0 Then
                If ApplyTransportChargeAddInActualAmount = True Then
                    strMilkTransportAmountColumnOuter = " Milk_Amount as [Milk Amount] , Transport_Charges as [Transport Amount] ,"
                    strMilkTransportAmountColumnInner = " TSPL_Bulk_MILK_SRN.Milk_Amount, TSPL_Bulk_MILK_SRN.Transport_Charges , "
                End If
                SQuery += " with WholeData as (Select DISTINCT ROW_NUMBER() OVER (PARTITION BY xx.[Challan No] ORDER BY xx.[Challan No] ) row_num,xx.MCC_Code as [Tanker Dis From Loc],xx.MCC_NAME as [Tanker Dis From Loc Name],xx.[To MCC or Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [To MCC or Plant Name], xx.DocType,xx.[Vendor Code],xx.[Vendor Name],xx.[Ref Doc No] as [Ref Doc No],xx.[Challan No],xx.[Challan Date],xx.[SRN No],xx.[SRN Date],xx.[Invoice No],xx.[Invoice Date],xx.[Tanker No],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Weighment No],xx.[Weighment Date],xx.[Milk Receipt Challan No],xx.[Milk Receipt Challan Date],xx.[Challan Qty], xx.ChallanFatPer as [Challan Fat%],xx.ChallanSNFPer as [Challan SNF%],xx.ChallanFatKg as [Challan Fat KG],xx.ChallanSNFKg as [Challan SNF KG],(xx.ChallanFatKg+xx.ChallanSNFKg) as [Challan TS],xx.[Gross Weight],xx.[Tare Weight],xx.[Tare Date],xx.[Net Weight],xx.[From MCC or Plant Code],xx.[From MCC or Plant Name], xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No],XX.[Unloading Date Time], XX.[QC Date Time],XX.STATUS,xx.[Unloading No],xx.[MCC Name],xx.Plant,xx.[Silo Code],xx.[Silo Desc],xx.[Gate Out No],xx.[Gate Out Date Time],xx.[FAT %]"
                '' work done for ALPHA adjust FAT, SNF and CLR field add against ticket no. ALF/04/05/18-000058 
                If ShowFatSnfAfterApproval = True Then
                    SQuery += " ,xx.[After Approval Fat] "
                End If
                SQuery += " ,xx.[SNF %]"
                If ShowFatSnfAfterApproval = True Then
                    SQuery += " ,xx.[After Approval SNF] "
                End If

                SQuery += " , xx.CLR,(xx.[Challan Qty]-xx.[Net Weight]) as [Differrence Qty],Convert (decimal(18,2),(xx.[ChallanFatPer]-xx.[FAT %])) as [Differrence FAT %],Convert (decimal(18,2),(xx.[ChallanSNFPer]-xx.[SNF %])) as [Differrence SNF %],Convert (decimal(18,2),(xx.ChallanFatKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)))) as [Differrence FAT kG],Convert (decimal(18,2),(ChallanSNFKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)))) as [Differrence SNF KG] "
                If ShowFatSnfAfterApproval = True Then
                    SQuery += " ,xx.[After Approval CLR] "
                End If
                ''richa show qty from totalqty_in_kg column in gate entry table in case of MCC PRoc 27 June,2019 ERO/26/06/19-000657
                SQuery += " ,xx.[Basic Rate], xx.incentive ,xx.[Special Deduction],xx.[Deduction] , xx.[Net Rate],xx.[FAT Rate], xx.[SNF Rate], " + strMilkTransportAmountColumnOuter + " case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total SRN Amount],xx.[FAT Weightage & SNF Weightage],xx.[FAT Ratio & SNF ratio],xx.[Vendor Class] , case when SRN_Return_NO is not Null then 'SRN Return' else '' end as [SRN Return]"
                If SettCalculateLtrQtyFromKGQtyByCLR Then
                    SQuery += ",case when Doc_Type='BulkProc' then SRNFATKg else (Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] / 100))) end As FATKG " +
                    ",case when Doc_Type='BulkProc' then SRNSNFKg else (Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] / 100))) end As SNFKG"
                Else
                    SQuery += ", Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG" +
                    ", Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG"
                End If
                SQuery += ",[FAT Amt],[SNF Amt],[Standard Rate] From (" &
                            "  Select   Tspl_Gate_Entry_Details.MCC_Code,Tspl_Gate_Entry_Details.MCC_NAME,Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.fat_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.fat_per, 0) end" &
                             "  as ChallanFatPer,Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) end as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per*  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100" &
                            " as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100" &
                            " as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType," &
                            " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No], " &
                            " IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], " &
                            " TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name],TSPL_MCC_Dispatch_Challan.level1challanNo as [Ref Doc No],  Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date]," &
                            " TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No], " &
                            " Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No], " &
                            " Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date], " &
                            " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry], " &
                            " TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then Tspl_Gate_Entry_Details.Qty_In_Kg else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end END  As [Challan Qty], " &
                            " case when TSPL_Weighment_chember_details.Gross_Weight > 0 then TSPL_Weighment_chember_details.Gross_Weight  else  TSPL_Weighment_Detail.Gross_Weight end As [Gross Weight],  case when TSPL_Weighment_chember_details.Tare_Weight > 0 then TSPL_Weighment_chember_details.Tare_Weight  else  TSPL_Weighment_Detail.Tare_Weight end As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], case when TSPL_Weighment_chember_details.Net_Weight > 0 then TSPL_Weighment_chember_details.Net_Weight  else  TSPL_Weighment_Detail.Net_Weight end As [Net Weight] , " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code], " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else coalesce(TSPL_MCC_MASTER_From_Mcc.MCC_NAME,FromPlant.Location_Desc) End As [From MCC or Plant Name], " &
                            " Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code], " &
                            " Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code], " &
                            " TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM, " &
                            " TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time], " &
                            " Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS, " &
                            " TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], case when isnull(MCC_PLANT.Location_category,'')='MCC' then tspl_location_master.MAin_location_code  else '' end   As [MCC Name], case when isnull(MCC_PLANT.Type,'')='PLANT' then tspl_location_master.MAin_location_code  else '' end As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], " &
                            " TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time], " &
                            " Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %]"

                If ShowFatSnfAfterApproval = True Then
                    SQuery += " ,case when Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0))=Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) end  as [After Approval Fat]  "
                End If

                SQuery += " , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %]"

                If ShowFatSnfAfterApproval = True Then
                    SQuery += "  , case when Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0))= Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) end as [After Approval SNF], case when Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0))= Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) then 0 else Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) end as [After Approval CLR] "
                End If


                SQuery += " , Convert(decimal(18,2)," &
                            " isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], " &
                            " TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate]," &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate], " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate], " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt) Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt], " &
                            " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt) Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt], " &
                            " " + strMilkTransportAmountColumnInner + " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount Else TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio', " &
                            " TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class],TSPL_Bulk_MILK_SRN.fat_KG as SRNFATKg,TSPL_Bulk_MILK_SRN.SNF_KG as SRNSNFKg ,Tspl_Gate_Entry_Details.Doc_Type    " &
                            " From  " &
                            " (select FromMCCandDesc.MCC_Code,FromMCCandDesc.MCC_NAME,Tspl_Gate_Entry_Details.Gate_Entry_No , Tspl_Gate_Entry_Details.Doc_type ,Tspl_Gate_Entry_Details.Date_And_Time, Tspl_Gate_Entry_Details.Challan_No, Tspl_Gate_Entry_Details.Challan_Date,Tspl_Gate_Entry_Details.Tanker_No, Tspl_Gate_Entry_Details.isPosted, Posting_Date,Dispatched_From_Mcc,location_Code,Location_Desc ,Vendor_Code,Vendor_Desc,case when isnull(TSPL_Gate_Entry_Chember_Details.Item_Code,'')='' then  Tspl_Gate_Entry_Details.Item_Code else TSPL_Gate_Entry_Chember_Details.Item_Code end Item_Code , Tspl_Gate_Entry_Details.Item_Desc, case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end  Qty_In_Kg  , case when TSPL_Gate_Entry_Chember_Details.snf_Per > 0 then TSPL_Gate_Entry_Chember_Details.snf_Per else Tspl_Gate_Entry_Details.snf_Per end as snf_Per ,case when TSPL_Gate_Entry_Chember_Details.fat_per > 0 then TSPL_Gate_Entry_Chember_Details.fat_per else  Tspl_Gate_Entry_Details.fat_per end as fat_per , Created_By ,Created_Date, Modify_By,Modify_Date,comp_code,Tspl_Gate_Entry_Details.UOM,Intimation_No , Supplier_Code,Dispatch_Centre_Code,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE,Intimation_Status,Gate_Entry_Type,Seal_Status, case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end as TotalQty_In_Kg ,SealNo_Header,Tanker_Return,PO_No,Amendment_Code,Amendment_By,Amendment_Date,IsAgainstJobWork,Sublocation_Code,In_Return,Transpoter_Id,Bulk_ROUTE_NO,Distance,Rate,Amount,ProvisionNo,NO_OF_CHAMBER,No_Of_CAN,Weight,TSPL_Gate_Entry_Chember_Details.Line_No,TSPL_Gate_Entry_Chember_Details.Chamber_Desc from Tspl_Gate_Entry_Details Left Outer Join (Select MCC_Code,MCC_NAME From TSPL_MCC_MASTER)FromMCCandDesc On FromMCCandDesc.MCC_Code=Tspl_Gate_Entry_Details.MCC  left outer join TSPL_Gate_Entry_Chember_Details on Tspl_Gate_Entry_Details.Gate_Entry_No = TSPL_Gate_Entry_Chember_Details.GE_Code) as Tspl_Gate_Entry_Details " &
                            " Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " left outer join TSPL_Weighment_chember_details on TSPL_Weighment_chember_details.weighment_no=TSPL_Weighment_Detail.weighment_no and TSPL_Weighment_chember_details.item_code= Tspl_Gate_Entry_Details.Item_Code and Tspl_Gate_Entry_Details.Line_No=TSPL_Weighment_chember_details.line_no " &
                            " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code " &
                            " Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code " &
                            " Left Join TSPL_LOCATION_MASTER As FromPlant On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = FromPlant.Location_Code " &
                            " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code " &
                            " Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " Left Outer Join (select TSPL_Milk_unloading_Chember_Details.Line_No ,TSPL_MILK_UNLOADING.Unloading_Date_Time,TSPL_MILK_UNLOADING.Unloading_No,TSPL_MILK_UNLOADING.Gate_Entry_No ,case when isnull(TSPL_Milk_unloading_Chember_Details.Item_code,'')=''  then TSPL_MILK_UNLOADING.Item_code else  TSPL_Milk_unloading_Chember_Details.Item_code end as Item_code,case when isnull(TSPL_Milk_unloading_Chember_Details.Sublocation_Code,'')=''  then TSPL_MILK_UNLOADING.Sub_location_Code else  TSPL_Milk_unloading_Chember_Details.Sublocation_Code end as Sub_location_Code from TSPL_MILK_UNLOADING left outer join TSPL_Milk_unloading_Chember_Details  on TSPL_MILK_UNLOADING.unloading_no = TSPL_Milk_unloading_Chember_Details.unloading_no ) TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No and  TSPL_MILK_UNLOADING.item_code= Tspl_Gate_Entry_Details.item_code and Tspl_Gate_Entry_Details.Line_No=TSPL_MILK_UNLOADING.line_no " &
                            " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code " &
                            " Left Outer Join TSPL_LOCATION_MASTER as MCC_PLANT On MCC_PLANT.Location_Code = TSPL_LOCATION_MASTER.Main_Location_code " &
                            " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No" &
                            " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
                            " Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code " &
                            " Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
                            " Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO " &
                            " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " &
                            " Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " left outer join ( select TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNO,TSPL_MCC_Dispatch_Challan.Transfer_Price,TSPL_MCC_Dispatch_Challan.Chalan_NO,case when TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE > 0 then TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE else  TSPL_MCC_DISPATCH_CHALLAN.FAT_RATE end as FAT_RATE,case when TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE > 0 then TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE else  TSPL_MCC_DISPATCH_CHALLAN.SNF_RATE end as SNF_RATE,case when TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG > 0 then TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG else  TSPL_MCC_DISPATCH_CHALLAN.FAT_KG end as FAT_KG,case when TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG > 0 then TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG else  TSPL_MCC_DISPATCH_CHALLAN.SNF_KG end as SNF_KG,case when TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount > 0 then TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount else  TSPL_MCC_DISPATCH_CHALLAN.Amount end as Amount,TSPL_MCC_Dispatch_Challan.FAT_W,TSPL_MCC_Dispatch_Challan.SNF_W,TSPL_MCC_Dispatch_Challan.FAT_R,TSPL_MCC_Dispatch_Challan.SNF_R,case when isnull(TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code,'')=''  then TSPL_MCC_Dispatch_Challan.Item_code else  TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Item_code end as Item_code,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Qty_KG,TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chamber_Description ,tspl_mcc_dispatch_challan.level1challanNo 
 from TSPL_MCC_DISPATCH_CHALLAN left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL  on TSPL_MCC_Dispatch_Challan.Chalan_NO = TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_NO left outer join TSPL_MCC_TANKER_GATE_OUT on tspl_mcc_dispatch_challan.Against_Gate_Out=TSPL_MCC_TANKER_GATE_OUT.gate_out_no )
TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No and  TSPL_MCC_Dispatch_Challan.item_code= Tspl_Gate_Entry_Details.item_code and Tspl_Gate_Entry_Details.Qty_In_Kg =TSPL_MCC_Dispatch_Challan.Qty_KG and Tspl_Gate_Entry_Details.Chamber_Desc=TSPL_MCC_Dispatch_Challan.Chamber_Description --and Tspl_Gate_Entry_Details.Line_No=TSPL_MCC_Dispatch_Challan.SNO " & Environment.NewLine

                SQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=Tspl_Gate_Entry_Details.Line_No " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No and t_SNF.LINE_NO=Tspl_Gate_Entry_Details.Line_No " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=Tspl_Gate_Entry_Details.Line_No " &
                            " where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
                If cbgMCC.CheckedValue.Count > 0 Then
                    SQuery += "and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                End If

                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    SQuery += " and Tspl_Gate_Entry_Details.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
                End If
                SQuery += " union all"

                SQuery += "  Select  FromMCCandDesc.MCC_Code,FromMCCandDesc.MCC_NAME,Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100" &
" as ChallanSNFKG,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType," &
                            " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No, '') End As [Milk Receipt Challan No], " &
                            " IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], " &
                            " TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name],'' as [Ref Doc No], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date]," &
                            " TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No], " &
                            " Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No], " &
                            " Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date], " &
                            " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry], " &
                            " TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty], " &
                            " TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight], " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code], " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else coalesce(TSPL_MCC_MASTER_From_Mcc.MCC_NAME,FromPlant.Location_Desc) End As [From MCC or Plant Name], " &
                            " Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code], " &
                            " Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code], " &
                            " TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM, " &
                            " TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time], " &
                            " Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS, " &
                            " TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], " &
                            " TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time], " &
                            " Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %]"

                If ShowFatSnfAfterApproval = True Then
                    SQuery += " ,case when Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0))=Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) end  as [After Approval Fat]  "
                End If

                SQuery += " , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %]"
                If ShowFatSnfAfterApproval = True Then
                    SQuery += "  , case when Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0))= Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) end as [After Approval SNF], case when Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0))= Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) then 0 else Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) end as [After Approval CLR] "
                End If

                SQuery += " , Convert(decimal(18,2)," &
                            " isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], " &
                            " TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate]," &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate], " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate], " &
                            " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG)*-1 End As [FAT Amt], " &
                            " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG)*-1 End As [SNF Amt], " &
                            " " + strMilkTransportAmountColumnInner + " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount*-1 Else TSPL_MCC_Dispatch_Challan.Amount*-1 End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio', " &
                            " TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class],TSPL_Bulk_MILK_SRN.fat_KG as SRNFATKg,TSPL_Bulk_MILK_SRN.SNF_KG as SRNSNFKg,Tspl_Gate_Entry_Details.Doc_Type    " &
                            " From  TSPL_MILK_TRANSFER_IN_RETURN LEFT OUTER JOIN  TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Receipt_Challan_No = TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No " &
                            " LEFT OUTER JOIN Tspl_Gate_Entry_Details ON  Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no " &
                            " Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code " &
                            " Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code " &
                            " Left Outer Join (Select MCC_Code,MCC_NAME From TSPL_MCC_MASTER)FromMCCandDesc On FromMCCandDesc.MCC_Code=Tspl_Gate_Entry_Details.MCC " &
                            " Left Join TSPL_LOCATION_MASTER As FromPlant On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = FromPlant.Location_Code " &
                            " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code " &
                            " Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code " &
                            " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No" &
                            " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
                            " Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code " &
                            " Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
                            " Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO " &
                            " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " &
                            " Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No" &
                            " where 2 = 2 and convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
                If cbgMCC.CheckedValue.Count > 0 Then
                    SQuery += "and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                End If

                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    SQuery += " and Tspl_Gate_Entry_Details.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
                End If

                SQuery += " union all"

                SQuery += "  Select  FromMCCandDesc.MCC_Code,FromMCCandDesc.MCC_NAME,Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100" &
" as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, 'Purchase Return'  As DocType," &
                           " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No], " &
                           " IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], " &
                           " TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name],'' as [Ref Doc No], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date]," &
                           " TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No As [Invoice No], " &
                           " Convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No], " &
                           " Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date], " &
                           " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry], " &
                           " TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, (-1)* Tspl_Gate_Entry_Details.Qty_In_Kg As [Challan Qty], " &
                           " (-1)* TSPL_Weighment_Detail.Gross_Weight As [Gross Weight], (-1)* TSPL_Weighment_Detail.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date], (-1)* TSPL_Weighment_Detail.Net_Weight As [Net Weight], " &
                           " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code], " &
                           " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else coalesce(TSPL_MCC_MASTER_From_Mcc.MCC_NAME,FromPlant.Location_Desc) End As [From MCC or Plant Name], " &
                           " Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code], " &
                           " Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code], " &
                           " TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM, " &
                           " TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time], " &
                           " Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS, " &
                           " TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], " &
                           " TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time], " &
                           " Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %]"
                If ShowFatSnfAfterApproval = True Then
                    SQuery += " ,case when Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0))=Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) end  as [After Approval Fat]  "
                End If

                SQuery += " , Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %]"
                If ShowFatSnfAfterApproval = True Then
                    SQuery += "  , case when Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0))= Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) end as [After Approval SNF], case when Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0))= Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) then 0 else Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) end as [After Approval CLR] "
                End If

                SQuery += " , Convert(decimal(18,2)," &
                           " t_CLR.Param_Field_Value) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], " &
                           " (-1)* TSPL_Bulk_MILK_SRN.Incentive as Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate]," &
                           " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate], " &
                           " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate], " &
                           " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.FatAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt], " &
                           " Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.SnfAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt], " &
                           " " + strMilkTransportAmountColumnInner + " Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then (-1)* TSPL_Bulk_MILK_SRN.Actual_Amount Else (-1)*TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio', " &
                           " TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class] ,TSPL_Bulk_MILK_SRN.fat_KG as SRNFATKg,TSPL_Bulk_MILK_SRN.SNF_KG as SRNSNFKg,Tspl_Gate_Entry_Details.Doc_Type   " &
                           " From Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                           " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code " &
                           " Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code " &
                           " Left Outer Join (Select MCC_Code,MCC_NAME From TSPL_MCC_MASTER)FromMCCandDesc On FromMCCandDesc.MCC_Code=Tspl_Gate_Entry_Details.MCC " &
                           " Left Join TSPL_LOCATION_MASTER As FromPlant On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = FromPlant.Location_Code " &
                           " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code " &
                           " Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                           " Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                           " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code " &
                           " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                           " Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No" &
                           " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
                           " Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code " &
                           " Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
                           " Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO " &
                          " Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD On TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_No  = tspl_Bulk_milk_purchase_Invoice_head.DOC_NO " &
                            " Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL  On TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No = TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No  " &
                            " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y' " &
                            " Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No " &
                            " Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No " &
                            " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No" &
                            " where 2=2 and TSPL_Bulk_MILK_SRN.isposted=1 and isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No,'') <>''   and Convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD .Pur_Return_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and Convert(date,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD .Pur_Return_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
                If cbgMCC.CheckedValue.Count > 0 Then
                    SQuery += "and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                End If

                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    SQuery += " and Tspl_Gate_Entry_Details.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
                End If
                SQuery += " UNION ALL "
                SQuery += "Select  FromMCCandDesc.MCC_Code,FromMCCandDesc.MCC_NAME,Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100" &
" as ChallanSNFKG , TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType,  'Not Req'  As [Milk Receipt Challan No],  '' As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], '' as [Ref Doc No],Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty],  TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else coalesce(TSPL_MCC_MASTER_From_Mcc.MCC_NAME,FromPlant.Location_Desc) End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time]"
                SQuery += " ,  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %]"
                If ShowFatSnfAfterApproval = True Then
                    SQuery += " ,case when Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0))=Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_FAT,0)) end  as [After Approval Fat]  "
                End If
                SQuery += " , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %]"
                If ShowFatSnfAfterApproval = True Then
                    SQuery += "  , case when Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0))= Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) then 0 else Convert(decimal(18,2),isnull(TSPL_QUALITY_CHECK.Adjusted_SNF,0)) end as [After Approval SNF], case when Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0))= Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) then 0 else Convert(decimal(18,2), isnull(TSPL_QUALITY_CHECK.Adjusted_CLR,0)) end as [After Approval CLR] "
                End If

                SQuery += " , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], TSPL_Bulk_MILK_SRN.BasicRate As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) As [FAT Rate],   Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) As [SNF Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1  As [FAT Amt],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1  As [SNF Amt], " + strMilkTransportAmountColumnInner + " TSPL_Bulk_MILK_SRN.Actual_Amount*-1 As [Total Amount Temp], 'For ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage)  + ' & ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage)  As 'FAT Weightage & SNF Weightage', 'For ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage)  + ' & ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)  As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class] ,TSPL_Bulk_MILK_SRN.fat_KG as SRNFATKg,TSPL_Bulk_MILK_SRN.SNF_KG as SRNSNFKg,Tspl_Gate_Entry_Details.Doc_Type     From  " + Environment.NewLine +
                 " Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " + Environment.NewLine +
                 " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code " + Environment.NewLine +
                 " Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code " + Environment.NewLine +
                " Left Outer Join (Select MCC_Code,MCC_NAME From TSPL_MCC_MASTER)FromMCCandDesc On FromMCCandDesc.MCC_Code=Tspl_Gate_Entry_Details.MCC " + Environment.NewLine +
                " Left Join TSPL_LOCATION_MASTER As FromPlant On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = FromPlant.Location_Code " &
                 " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code " + Environment.NewLine +
                 " Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " + Environment.NewLine +
                 " Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " + Environment.NewLine +
                 " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  " + Environment.NewLine +
                 " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " + Environment.NewLine +
                 " Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No " + Environment.NewLine +
                 " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  " + Environment.NewLine +
                 " Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code " + Environment.NewLine +
                 " Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  " + Environment.NewLine +
                 " Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  " + Environment.NewLine +
                 " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No where 2 = 2 " + Environment.NewLine +
                 " and convert(date,TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103) <=convert(date,'" + txtToDate.Value + "',103) "

                If cbgMCC.CheckedValue.Count > 0 Then
                    SQuery += "and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                End If

                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    SQuery += " and Tspl_Gate_Entry_Details.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
                End If

                SQuery += " ) As xx left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.[To MCC or Plant Code] ) , Parameter as (select tspl_qc_parameter_detail.QC_No ,case when tspl_parameter_master.Description is null then  tspl_qc_parameter_detail.Param_Field_Code  else isnull(tspl_parameter_master.Description,'') end  as ParamDesc, tspl_qc_parameter_detail.Param_Field_Value  from tspl_qc_parameter_detail left outer join tspl_parameter_master on TSPL_PARAMETER_MASTER.Code=tspl_qc_parameter_detail.Param_Field_Code   where ISNULL(tspl_qc_parameter_detail.Param_Type ,'') not in ('AutoCLR','AutoFAT','AutoSNF','','FAT','SNF')) ,FinalData as (select QC_No," & Pivot & " from Parameter PIVOT (         max(Param_Field_Value) FOR ParamDesc IN (" & Pivot & ") ) AS PivotTable) "
                'SQuery += " select TSPL_MCC_Dispatch_Challan.Mcc_Code as [Tanker Dis From Loc],TSPL_MCC_Dispatch_Challan.Mcc_Name as [Tanker Dis From Loc Name],* from (select wholeData.*,isnull (TSPL_MCC_Dispatch_Challan.Collection_of_Milk,'') as [Collection of Milk] ,isnull (TSPL_MCC_Dispatch_Challan.Delivery_Challan_No,'') as [Delivery Challan No],case when wholeData.row_num=1 then wholeData.[Ref Doc No] else wholeData.[Challan No] end as [Tanker Dispatch Ref No],FinalData.* from wholeData left outer join FinalData on FinalData.qc_no=wholeData.[QC No]  left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.chalan_no =wholeData.[Challan No]   where 2=2"
                SQuery += " select * from (select wholeData.*,isnull (TSPL_MCC_Dispatch_Challan.Collection_of_Milk,'') as [Collection of Milk] ,isnull (TSPL_MCC_Dispatch_Challan.Delivery_Challan_No,'') as [Delivery Challan No],case when wholeData.row_num=1 then wholeData.[Ref Doc No] else wholeData.[Challan No] end as [Tanker Dispatch Ref No],FinalData.* from wholeData left outer join FinalData on FinalData.qc_no=wholeData.[QC No]  left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.chalan_no =wholeData.[Challan No]   where 2=2"

                If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                    SQuery += " and [To MCC or Plant Code]  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                End If
                If chkDocTypeSelect.IsChecked And cbgDocType.CheckedValue.Count > 0 Then
                    SQuery += " and [DocType] in (" + clsCommon.GetMulcallString(cbgDocType.CheckedValue) + ")  "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    SQuery += " and [Vendor Code] in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
                End If
                SQuery += " )finalWholeData  left outer join TSPL_MCC_Dispatch_Challan on TSPL_MCC_Dispatch_Challan.chalan_no =finalWholeData.[Tanker Dispatch Ref No] order by finalWholeData.[Challan No],convert(date,[Gate Entry Date],103)"


            Else
                Dim whrcls As String = Nothing
                Dim qry As String = Nothing
                whrcls = " where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
                If cbgMCC.CheckedValue.Count > 0 Then
                    whrcls += "and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                End If

                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    whrcls += " and Tspl_Gate_Entry_Details.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
                End If
                qry += RptBulkMilkRegister.GetBaseQuery(whrcls)
                SQuery += "with WholeData as (" + qry + ") , Parameter as (select tspl_qc_parameter_detail.LINE_NO,tspl_qc_parameter_detail.QC_No ,case when tspl_parameter_master.Description is null then  tspl_qc_parameter_detail.Param_Field_Code else isnull(tspl_parameter_master.Description,'') end  as ParamDesc, tspl_qc_parameter_detail.Param_Field_Value  from tspl_qc_parameter_detail left outer join tspl_parameter_master on TSPL_PARAMETER_MASTER.Code=tspl_qc_parameter_detail.Param_Field_Code   where ISNULL(tspl_qc_parameter_detail.Param_Type ,'') not in ('AutoCLR','AutoFAT','AutoSNF','','FAT','SNF')) ,FinalData as (select LINE_NO,QC_No," & Pivot & " from Parameter PIVOT (         max(Param_Field_Value) FOR ParamDesc IN (" & Pivot & ") ) AS PivotTable) select wholeData.*,FinalData.* from wholeData left outer join FinalData on FinalData.qc_no=wholeData.[QC No] and FinalData.LINE_NO=WholeData.Line_No_a    where 2=2"

                If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                    SQuery += " and [To MCC or Plant Code]  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                End If
                If chkDocTypeSelect.IsChecked And cbgDocType.CheckedValue.Count > 0 Then
                    SQuery += " and [DocType] in (" + clsCommon.GetMulcallString(cbgDocType.CheckedValue) + ")  "
                End If
                If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                    SQuery += " and [Vendor Code] in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
                End If
                SQuery += " order by convert(date,[Gate Entry Date],103),WholeData.line_no_a  "
            End If
            'Dim Pivot As String = clsDBFuncationality.getSingleValue("select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct (select '['+description+'],' from tspl_parameter_master for xml path('')) as xvalue)a")

            'KUNAL > TICKET : OLD > CLIENT > UDL > DATE : 26-12-2016 > TASK: DUPLICATE ENTRIES ARISING > STATUS : FIXED 


            '' bulk export
            If BulkExport = 1 Then
                If TankerFromMaster = 0 Then
                    transportSql.BulkExport("Bulk_Milk_Register", SQuery, "order by convert(date,[Gate Entry Date],103)", "csv", "select wholeData.*,FinalData.*")
                Else
                    transportSql.BulkExport("Bulk_Milk_Register", SQuery, "order by convert(date,[Gate Entry Date],103),WholeData.line_no_a", "csv", "select wholeData.*,FinalData.*")
                End If
                Exit Sub
            ElseIf BulkExport = 2 Then
                If TankerFromMaster = 0 Then
                    transportSql.BulkExport("Bulk_Milk_Register", SQuery, "order by convert(date,[Gate Entry Date],103)", "xls", "select wholeData.*,FinalData.*")
                Else
                    transportSql.BulkExport("Bulk_Milk_Register", SQuery, "order by convert(date,[Gate Entry Date],103),WholeData.line_no_a", "xls", "select wholeData.*,FinalData.*")
                End If
                Exit Sub
            End If


            'Dim dtgv As New DataTable
            Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(SQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.GroupDescriptors.Clear()
                gv.Templates.Clear()
                gv.MasterTemplate.Reset()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                gv.MasterView.Refresh()
                gv.ViewDefinition = New TableViewDefinition
                gv.AutoGenerateColumns = True
                gv.DataSource = dtgv

                'gv.Columns("Challan_Date").IsVisible = False[To MCC or Plant Code]
                gv.Columns("To MCC or Plant Code").IsVisible = False

                'gv.Columns("Total Amount Temp").IsVisible = False
                'gv.Columns("SRN_Return_NO").IsVisible = False
                For ii As Integer = 0 To gv.Columns.Count - 1
                    gv.Columns(ii).ReadOnly = True
                    gv.Columns(ii).Width = 200
                Next
                gv.BestFitColumns()

                FormatGrid(dtExtraColumn)

                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
            'View()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    ' This function wrote by stuti and it is used for UDL and secondary qc work was done by kunal and for udl only 
    Public Shared Function GetBaseQuery(ByVal whrcls As String)
        Dim SettBulkMilkFATSNFKGDecimalPlaces As Integer = clsFixedParameter.GetData(clsFixedParameterType.BulkMilkFATSNFKGDecimalPlaces, clsFixedParameterCode.BulkMilkFATSNFKGDecimalPlaces, Nothing)
        Dim PickFatSnfKGFromBulkMilkSRNInBulkMilkRegister As Boolean = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.PickFatSnfKGFromBulkMilkSRNInBulkMilkRegister, clsFixedParameterCode.PickFatSnfKGFromBulkMilkSRNInBulkMilkRegister, Nothing)) = "1", True, False)
        Dim qry As String = Nothing
        Dim Pivot As String = clsDBFuncationality.getSingleValue("select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct (select '['+description+'],' from tspl_parameter_master where type not in('FAT','SNF','CLR') for xml path('')) as xvalue)a")
        qry += "Select  DISTINCT Line_No as Line_No_a, xx.[To MCC or Plant Code], xx.DocType,xx.[Vendor Code],xx.[Vendor Name],coalesce(DC_Final.dispatch_no,xx.[Challan No]) AS [Challan No],xx.[Challan Date], coalesce(xx.[SRN No],'') AS [SRN No],  xx.[SRN Status] as [SRN Status],  coalesce(xx.[SRN Date],'' ) as [SRN Date]," &
                "xx.[Invoice No],xx.[Invoice Date],xx.[Tanker No],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Weighment No],xx.[Weighment Date],  " &
                "xx.[Milk Receipt Challan No],xx.[Milk Receipt Challan Date],xx.[Challan Qty],xx.[Gross Weight],xx.[Tare Weight],xx.[Tare Date],xx.[Net Weight],  " &
                "xx.MIKL_TYPE_CODE as [Milk Type],xx.Milk_Grade_code as [Grade Code],xx.GRADE_TYPE as [Grade Type], " &
                "xx.[From MCC or Plant Code],xx.[From MCC or Plant Name], xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No], coalesce(xx.[Secondary QC No],'') as [Secondary QC No], XX.[Unloading Date Time],  " &
                "XX.[QC Date Time],XX.STATUS,xx.[Unloading No],xx.[Cleaning No],xx.[MCC Name],xx.Plant,xx.[Silo Code],xx.[Silo Desc],xx.[Gate Out No],xx.[Gate Out Date Time],  " &
                "xx.[FAT %],xx.[SNF %], xx.CLR,  xx.[Standard Rate],xx.[Basic Rate], xx.incentive , xx.Deduction, xx.[Special Deduction] , xx.[Net Rate],  coalesce(xx.[FAT Rate],0) as [FAT Rate], coalesce(xx.[SNF Rate],0) as [SNF Rate],  " &
                "case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total Amount],xx.[FAT Weightage & SNF Weightage],  " &
                "xx.[FAT Ratio & SNF ratio],xx.[Vendor Class] , case when SRN_Return_NO is not Null then 'SRN Return' else '' end as [SRN Return],  "
        If PickFatSnfKGFromBulkMilkSRNInBulkMilkRegister = True Then
            qry += "xx.fat_KG As FATKG,xx.SNF_KG As SNFKG"
        Else
            qry += "Convert(decimal(18," + clsCommon.myCstr(SettBulkMilkFATSNFKGDecimalPlaces) + "),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG, Convert(decimal(18," + clsCommon.myCstr(SettBulkMilkFATSNFKGDecimalPlaces) + "),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG "
        End If

        qry += ",Batch_No as [Batch No],TSPL_SHIFT_MASTER.SHIFT_NAME as [Shift Name],xx.allocatetomcc as [Allocate To MCC Code],tspl_mcc_master.MCC_NAME as [Allocate To MCC Name],TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO as [Allocated Tanker Gate Out No] , convert(varchar, TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE,103)+' '+left(convert(varchar, TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_DATE,114),5)  as  [Allocated Tanker Gate Out Date]" &
                ",TSPL_MCC_TANKER_GATE_OUT.TANKER_NO as [Allocated Tanker No]" &
                ",TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_No as [Security Gate Out No],convert(varchar, TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_Date,103)+' '+ left( convert(varchar, TSPL_MCC_TANKER_GATE_OUT_SECURITY.Doc_Date,114),5)  as [Security Gate Out Date]" &
                ",TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO as [Tanker Dispatch No],convert(varchar, TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date,103)+' '+left( convert(varchar, TSPL_MCC_DISPATCH_CHALLAN.Dispatch_Date,114),5) [Tanker Dispatch Date] From (" &
                 " Select "
        If PickFatSnfKGFromBulkMilkSRNInBulkMilkRegister = True Then
            qry += " TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_KG, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_KG, "
        End If

        qry += " TSPL_Milk_unloading_Chember_Details.Batch_No,TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],  " &
                 " Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) + ' ' + Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,108) As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No],  " &
                 "Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status], " &
                 " Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) + ' ' + Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,108) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  " &
                "Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  " &
                "Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], " &
                "Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Weighment_date,108) As [Weighment Date],  " &
                " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) + ' ' +  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,108) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  " &
                "TSPL_Weighment_Detail.Weighment_No As [Weighment No], " &
                "TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty As [Challan Qty],  " &
                " TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  " &
                "TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date],  " &
                "TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  " &
                "Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  " &
                "Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  " &
                "TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   " &
                "Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  " &
                "TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No], " &
                " Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) + ' ' + Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,108) As [Unloading Date Time],  " &
                " Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108)  As [QC Date Time],  " &
                "Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  " &
                "TSPL_MILK_UNLOADING.Unloading_No As [Unloading No],TSPL_Cleaning .Doc_No as [Cleaning No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  " &
                "Convert(varchar,TSPL_Gate_Out.Doc_Date,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108) As [Gate Out Date Time],  " &
                "Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  " &
                "Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  " &
                "Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   " &
                "Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt],  " &
                "Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp],  " &
                "'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  " &
                "'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  " &
                "TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]  ,TSPL_QUALITY_CHECK.Shift_Code,TSPL_Gate_Out.allocatetomcc " &
                "From Tspl_Gate_Entry_Details  " &
                "left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  " &
                "Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_WEIGHMENT_CHEMBER_DETAILS.line_no  " &
                "Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   " &
                "Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   " &
                "Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   " &
                "Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
                "left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and " &
                "TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_QUALITY_CHEMBER_DETAILS.line_no " &
                "left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  " &
                "Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "Left Outer Join TSPL_Milk_unloading_Chember_Details On TSPL_MILK_UNLOADING.Unloading_No = TSPL_Milk_unloading_Chember_Details.Unloading_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No =TSPL_Milk_unloading_Chember_Details.Line_No " &
                "Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  " &
                "Left Outer Join TSPL_Gate_Out On   " &
                "TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.line_no  " &
                "Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  " &
                "Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  " &
                "Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.chamber_desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.chamber_desc  " &
                "Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  " &
                "Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  " &
                "Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no = TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no " &
                "Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " &
                "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " &
                "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " &
                "LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No " &
                " left outer join tspl_cleaning on tspl_cleaning.QC_No =TSPL_QUALITY_CHECK .QC_No  "
        qry += whrcls
        qry += Environment.NewLine + " Union ALL " + Environment.NewLine
        qry += " Select "
        If PickFatSnfKGFromBulkMilkSRNInBulkMilkRegister = True Then
            qry += " TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_KG, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_KG, "
        End If
        qry += " '' as Batch_No,TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No, '') End As [Milk Receipt Challan No],  " &
                 " Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) + ' ' + Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,108) As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No],  " &
                 "Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status], " &
                 " Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) + ' ' + Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,108) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  " &
                "Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  " &
                "Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], " &
                "Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Weighment_date,108) As [Weighment Date],  " &
                " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) + ' ' +  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,108) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  " &
                "TSPL_Weighment_Detail.Weighment_No As [Weighment No], " &
                "TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty*-1 As [Challan Qty],  " &
                " TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight*-1 As [Gross Weight],  " &
                "TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date],  " &
                "TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight*-1 As [Net Weight],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  " &
                "Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  " &
                "Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  " &
                "TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   " &
                "Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  " &
                "TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No], " &
                " Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) + ' ' + Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,108) As [Unloading Date Time],  " &
                " Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108)  As [QC Date Time],  " &
                "Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  " &
                "TSPL_MILK_UNLOADING.Unloading_No As [Unloading No],TSPL_Cleaning .Doc_No as [Cleaning No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  " &
                "Convert(varchar,TSPL_Gate_Out.Doc_Date,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108) As [Gate Out Date Time],  " &
                "Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  " &
                "Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  " &
                "Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   " &
                "Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt)*-1 Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG)*-1 End As [FAT Amt],  " &
                "Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt)*-1 Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG)*-1 End As [SNF Amt],  " &
                "Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount*-1 Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount*-1 End As [Total Amount Temp],  " &
                "'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  " &
                "'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  " &
                "TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class] ,TSPL_QUALITY_CHECK.Shift_Code,TSPL_Gate_Out.allocatetomcc " &
                "From TSPL_MILK_TRANSFER_IN_RETURN " &
                " Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Receipt_Challan_No=TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No " &
                " LEFT OUTER JOIN Tspl_Gate_Entry_Details ON  Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no    " &
                "left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  " &
                "Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_WEIGHMENT_CHEMBER_DETAILS.line_no  " &
                "Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   " &
                "Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   " &
                "Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   " &
                "Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
                "left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and " &
                "TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_QUALITY_CHEMBER_DETAILS.line_no " &
                "left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  " &
                "Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  " &
                "Left Outer Join TSPL_Gate_Out On   " &
                "TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
                "left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.line_no  " &
                "Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  " &
                "Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  " &
                "Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.chamber_desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.chamber_desc  " &
                "Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  " &
                "Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  " &
                " Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No and TSPL_MCC_DISPATCH_CHALLAN_DETAIL.chamber_no = TSPL_GATE_ENTRY_CHEMBER_DETAILS.line_no " &
                "Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " &
                "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " &
                "Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  " &
                "LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No " &
                " left outer join tspl_cleaning on tspl_cleaning.QC_No =TSPL_QUALITY_CHECK .QC_No  "
        qry += whrcls
        qry += " ) As xx left join TSPL_SHIFT_MASTER on xx.Shift_Code=TSPL_SHIFT_MASTER.SHIFT_CODE" &
        " left join tspl_mcc_master on xx.allocatetomcc=tspl_mcc_master.MCC_Code" &
        " left outer join TSPL_MCC_DISPATCH_CHALLAN on TSPL_MCC_DISPATCH_CHALLAN.Chalan_NO=xx.[Challan No]" &
        " left  outer join TSPL_MCC_TANKER_GATE_OUT on TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO=TSPL_MCC_DISPATCH_CHALLAN.Against_Gate_Out and TSPL_MCC_TANKER_GATE_OUT.IsCancel=0 and TSPL_MCC_TANKER_GATE_OUT.IS_POSTED=1" &
        " left outer join TSPL_MCC_TANKER_GATE_OUT_SECURITY on TSPL_MCC_TANKER_GATE_OUT_SECURITY.Gate_Out_No=TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO and TSPL_MCC_TANKER_GATE_OUT_SECURITY.IS_POSTED=1"
        qry += " left outer join ( SELECT TSPL_MCC_Dispatch_Challan_detail.chalan_no,coalesce(TSPL_MCC_Dispatch_Challan_detail.intermittent_dispatch_no,TSPL_MCC_Dispatch_Challan_detail.chalan_no) as dispatch_no ,SNO FROM TSPL_MCC_Dispatch_Challan_detail)DC_Final ON DC_Final.chalan_no=xx.[Challan No] AND DC_Final.SNO=XX.LINE_NO "
        Return qry
    End Function
    Sub FormatGrid(ByVal dtExtraColumn As DataTable)
        ' Dim strItemCode, head2 As String

        'gv.TableElement.TableHeaderHeight = 25
        'gv.MasterTemplate.ShowRowHeaderColumn = False
        'For ii As Integer = 0 To gv.Columns.Count - 1
        '    gv.Columns(ii).ReadOnly = True
        '    gv.Columns(ii).IsVisible = False

        'Next

        'gv.Columns("DocType").IsVisible = True
        'gv.Columns("DocType").Width = 100
        'gv.Columns("DocType").HeaderText = " Doc Type"

        'gv.Columns("Milk Receipt Challan No").IsVisible = True
        'gv.Columns("Milk Receipt Challan No").Width = 100
        'gv.Columns("Milk Receipt Challan No").HeaderText = " Milk Receipt Challan No"
        ''gv.Columns("shift_date").FormatString = "{0:d}"

        'gv.Columns("Milk Receipt Challan Date").IsVisible = True
        'gv.Columns("Milk Receipt Challan Date").Width = 100
        'gv.Columns("Milk Receipt Challan Date").HeaderText = " Milk Receipt Challan Date"

        'gv.Columns("Vendor Code").IsVisible = True
        'gv.Columns("Vendor Code").Width = 100
        'gv.Columns("Vendor Code").HeaderText = "Vendor Code"

        'gv.Columns("Vendor Name").IsVisible = True
        'gv.Columns("Vendor Name").Width = 100
        'gv.Columns("Vendor Name").HeaderText = "Vendor Name"
        ''gv.Columns("shift_date").FormatString = "{0:d}"

        'gv.Columns("Challan No").IsVisible = True
        'gv.Columns("Challan No").Width = 100

        'gv.Columns("Challan No").HeaderText = "Challan No"

        'gv.Columns("Challan Date").IsVisible = True
        'gv.Columns("Challan Date").Width = 100
        'gv.Columns("Challan Date").HeaderText = "Challan Date"



        'gv.Columns("SRN No").IsVisible = True
        'gv.Columns("SRN No").Width = 80
        'gv.Columns("SRN No").HeaderText = "SRN No"

        'gv.Columns("SRN Date").IsVisible = True
        'gv.Columns("SRN Date").Width = 80
        'gv.Columns("SRN Date").HeaderText = "SRN Date"

        'gv.Columns("Invoice No").IsVisible = True
        'gv.Columns("Invoice No").Width = 50
        'gv.Columns("Invoice No").HeaderText = "Invoice No"

        'gv.Columns("Invoice Date").IsVisible = True
        'gv.Columns("Invoice Date").Width = 100
        'gv.Columns("Invoice Date").HeaderText = "Invoice Date"

        'gv.Columns("Tanker No").IsVisible = True
        'gv.Columns("Tanker No").Width = 100
        'gv.Columns("Tanker No").HeaderText = "Tanker No"

        'gv.Columns("Gate Entry No").IsVisible = True
        'gv.Columns("Gate Entry No").Width = 100
        'gv.Columns("Gate Entry No").HeaderText = "Gate Entry No"

        'gv.Columns("Gate Entry Date").IsVisible = True
        'gv.Columns("Gate Entry Date").Width = 100
        'gv.Columns("Gate Entry Date").HeaderText = "Gate Entry Date"

        'gv.Columns("Gate Entry").IsVisible = True
        'gv.Columns("Gate Entry").Width = 100
        'gv.Columns("Gate Entry").HeaderText = "Gate Entry"

        'gv.Columns("Weighment No").IsVisible = True
        'gv.Columns("Weighment No").Width = 100
        'gv.Columns("Weighment No").HeaderText = "Weighment No"

        'gv.Columns("Weighment_date").IsVisible = True
        'gv.Columns("Weighment_date").Width = 100
        'gv.Columns("Weighment_date").HeaderText = "Weighment_date"

        'gv.Columns("Challan Qty").IsVisible = True
        'gv.Columns("Challan Qty").Width = 100
        'gv.Columns("Challan Qty").HeaderText = "Challan Qty"

        'gv.Columns("Gross Weight").IsVisible = True
        'gv.Columns("Gross Weight").Width = 100
        'gv.Columns("Gross Weight").HeaderText = "Gross Weight"


        'gv.Columns("Tare Weight").IsVisible = True
        'gv.Columns("Tare Weight").Width = 100
        'gv.Columns("Tare Weight").HeaderText = "Tare Weight"

        'gv.Columns("From MCC or Plant Code").IsVisible = True
        'gv.Columns("From MCC or Plant Code").Width = 100
        'gv.Columns("From MCC or Plant Code").HeaderText = "From MCC or Plant Code"

        'gv.Columns("From MCC or Plant Name").IsVisible = True
        'gv.Columns("From MCC or Plant Name").Width = 100
        'gv.Columns("From MCC or Plant Name").HeaderText = "From MCC or Plant Name"

        'gv.Columns("MCC or Plant Code").IsVisible = True
        'gv.Columns("MCC or Plant Code").Width = 100
        'gv.Columns("MCC or Plant Code").HeaderText = "MCC or Plant Code"

        'gv.Columns("To MCC or Plant Code").IsVisible = True
        'gv.Columns("To MCC or Plant Code").Width = 100
        'gv.Columns("To MCC or Plant Code").HeaderText = "To MCC or Plant Code"

        'gv.Columns("To MCC or Plant Name").IsVisible = True
        'gv.Columns("To MCC or Plant Name").Width = 100
        'gv.Columns("To MCC or Plant Name").HeaderText = "Net Weight"

        'gv.Columns("Item Code").IsVisible = True
        'gv.Columns("Item Code").Width = 100
        'gv.Columns("Item Code").HeaderText = "Item Code"

        'gv.Columns("Item Desc").IsVisible = True
        'gv.Columns("Item Desc").Width = 100
        'gv.Columns("Item Desc").HeaderText = "Item Desc"

        'gv.Columns("UOM").IsVisible = True
        'gv.Columns("UOM").Width = 100
        'gv.Columns("UOM").HeaderText = "UOM"

        'gv.Columns("QC No").IsVisible = True
        'gv.Columns("QC No").Width = 100
        'gv.Columns("QC No").HeaderText = "QC No"

        'gv.Columns("Unloading Date Time").IsVisible = True
        'gv.Columns("Unloading Date Time").Width = 100
        'gv.Columns("Unloading Date Time").HeaderText = "Unloading Date Time"

        'gv.Columns("QC Date Time").IsVisible = True
        'gv.Columns("QC Date Time").Width = 100
        'gv.Columns("QC Date Time").HeaderText = "QC Date Time"

        'gv.Columns("STATUS").IsVisible = True
        'gv.Columns("STATUS").Width = 100
        'gv.Columns("STATUS").HeaderText = "STATUS"

        'gv.Columns("Unloading No").IsVisible = True
        'gv.Columns("Unloading No").Width = 100
        'gv.Columns("Unloading No").HeaderText = "Unloading No"

        'gv.Columns("Gate Out No").IsVisible = True
        'gv.Columns("Gate Out No").Width = 100
        'gv.Columns("Gate Out No").HeaderText = "Gate Out No"


        'gv.Columns("Gate Out Date Time").IsVisible = True
        'gv.Columns("Gate Out Date Time").Width = 100
        'gv.Columns("Gate Out Date Time").HeaderText = "Gate Out Date Time"


        'gv.Columns("FAT %").IsVisible = True
        'gv.Columns("FAT %").Width = 100
        'gv.Columns("FAT %").HeaderText = "FAT %"


        'gv.Columns("SNF %").IsVisible = True
        'gv.Columns("SNF %").Width = 100
        'gv.Columns("SNF %").HeaderText = "SNF %"


        'gv.Columns("CLR").IsVisible = True
        'gv.Columns("CLR").Width = 100
        'gv.Columns("CLR").HeaderText = "CLR"


        'gv.Columns("Standard Rate").IsVisible = True
        'gv.Columns("Standard Rate").Width = 100
        'gv.Columns("Standard Rate").HeaderText = "Standard Rate"


        'gv.Columns("Basic Rate").IsVisible = True
        'gv.Columns("Basic Rate").Width = 100
        'gv.Columns("Basic Rate").HeaderText = "Basic Rate"


        'gv.Columns("Incentive").IsVisible = True
        'gv.Columns("Incentive").Width = 100
        'gv.Columns("Incentive").HeaderText = "Incentive"


        'gv.Columns("Deduction").IsVisible = True
        'gv.Columns("Deduction").Width = 100
        'gv.Columns("Deduction").HeaderText = "Deduction"


        'gv.Columns("Special Deduction").IsVisible = True
        'gv.Columns("Special Deduction").Width = 100
        'gv.Columns("Special Deduction").HeaderText = "Special Deduction"


        'gv.Columns("Gate Out No").IsVisible = True
        'gv.Columns("Gate Out No").Width = 100
        'gv.Columns("Gate Out No").HeaderText = "Gate Out No"


        'gv.Columns("Gate Out No").IsVisible = True
        'gv.Columns("Gate Out No").Width = 100
        'gv.Columns("Gate Out No").HeaderText = "Gate Out No"


        'gv.Columns("Gate Out No").IsVisible = True
        'gv.Columns("Gate Out No").Width = 100
        'gv.Columns("Gate Out No").HeaderText = "Gate Out No"


        'gv.Columns("Gate Out No").IsVisible = True
        'gv.Columns("Gate Out No").Width = 100
        'gv.Columns("Gate Out No").HeaderText = "Gate Out No"


        'gv.Columns("Gate Out No").IsVisible = True
        'gv.Columns("Gate Out No").Width = 100
        'gv.Columns("Gate Out No").HeaderText = "Gate Out No"


        'gv.Columns("Gate Out No").IsVisible = True
        'gv.Columns("Gate Out No").Width = 100
        'gv.Columns("Gate Out No").HeaderText = "Gate Out No"

        gv.Columns("colRemarks").IsVisible = True
        gv.Columns("colRemarks").Width = 100
        gv.Columns("colRemarks").HeaderText = "Remarks"

        If gv.Columns.Contains("row_num") Then
            gv.Columns("row_num").IsVisible = False
        End If

        If gv.Columns.Contains("Ref Doc No") Then
            gv.Columns("Ref Doc No").IsVisible = False
        End If

        'gv.Columns("From MCC or Plant Code").IsVisible = False
        'gv.Columns("From MCC or Plant Name").IsVisible = False

        If gv.Columns.Contains("Tanker Dis From Loc") Then
            gv.Columns("Tanker Dis From Loc").HeaderText = "From MCC or Plant Code Intermittent"
        End If
        If gv.Columns.Contains("Tanker Dis From Loc Name") Then
            gv.Columns("Tanker Dis From Loc Name").HeaderText = "From MCC or Plant Name Intermittent"
        End If


        If gv.Columns.Contains("Cleaning No") Then
            gv.Columns("Cleaning No").IsVisible = False
        End If

        If gv.Columns.Contains("Shift Name") Then
            gv.Columns("Shift Name").IsVisible = False
        End If

        If gv.Columns.Contains("Allocate To MCC Code") Then
            gv.Columns("Allocate To MCC Code").IsVisible = False
        End If
        If gv.Columns.Contains("Allocate To MCC Name") Then
            gv.Columns("Allocate To MCC Name").IsVisible = False
        End If


        Dim summaryRowItem As New GridViewSummaryRowItem()


        Dim intCount As Integer = 0
        'If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
        '    For Each dr As DataRow In dtExtraColumn.Rows
        '        Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(item1)
        '    Next
        'End If

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


        'gv1.ShowGroupPanel = False
        'gv1.MasterTemplate.AutoExpandGroups = True
        'gv1.Size = New System.Drawing.Size(456, 311)
        'gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        'gv1.MasterTemplate.ShowTotals = True

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                gv.MasterTemplate.FilterDescriptors.Clear()
                Dim obj As New clsGridLayout()
                obj.ReportID = MyBase.Form_ID
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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub chkDocTypeAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkDocTypeAll.ToggleStateChanged
        cbgDocType.Enabled = Not chkDocTypeAll.IsChecked
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    ' Ticket No : ERO/23/08/19-001001 By Prabhakar
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
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
        LoadDocType()
        LoadMCC()
        LoadVendor()
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        chkDocTypeAll.CheckState = CheckState.Checked
        chkVendorAll.CheckState = CheckState.Checked


        RadPageView1.SelectedPage = RadPageViewPage1
        gv.ViewDefinition = New TableViewDefinition
        gv.DataSource = Nothing

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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



    Private Sub RptBulkMilkRegister_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
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
            If chkVendorSelect.IsChecked Then
                Dim stVendorName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(stVendorName) > 0 Then
                        stVendorName += ", "
                    End If
                    stVendorName += StrName
                Next
                Dim strVendorCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strVendorCode) > 0 Then
                        strVendorCode += ", "
                    End If
                    strVendorCode += StrCode
                Next
                arrHeader.Add(("Vendor Name: " + stVendorName + " "))
            End If


            If chkMCCSelect.IsChecked Then
                Dim stMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(stMCCName) > 0 Then
                        stMCCName += ", "
                    End If
                    stMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add(("MCC Name: " + stMCCName + " "))
            End If

            If chkDocTypeSelect.IsChecked Then
                Dim stDocName As String = ""
                For Each StrName As String In cbgDocType.CheckedDisplayMember
                    If clsCommon.myLen(stDocName) > 0 Then
                        stDocName += ", "
                    End If
                    stDocName += StrName
                Next
                Dim strDocCode As String = ""
                For Each StrCode As String In cbgDocType.CheckedValue
                    If clsCommon.myLen(strDocCode) > 0 Then
                        strDocCode += ", "
                    End If
                    strDocCode += StrCode
                Next
                arrHeader.Add(("DOC Type: " + stDocName + " "))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Bulk Milk Register Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Bulk Milk Register Report", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        Load_Report(1)
    End Sub

    Private Sub BulkExport_Click(sender As Object, e As EventArgs) Handles BulkExport.Click
        Load_Report(2)
    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBulkMilkRegister & "'"))



            If chkVendorSelect.IsChecked Then
                Dim stVendorName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(stVendorName) > 0 Then
                        stVendorName += ", "
                    End If
                    stVendorName += StrName
                Next
                Dim strVendorCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strVendorCode) > 0 Then
                        strVendorCode += ", "
                    End If
                    strVendorCode += StrCode
                Next
                arrHeader.Add(("Vendor Name: " + stVendorName + " "))
            End If


            If chkMCCSelect.IsChecked Then
                Dim stMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(stMCCName) > 0 Then
                        stMCCName += ", "
                    End If
                    stMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add(("MCC Name: " + stMCCName + " "))
            End If

            If chkDocTypeSelect.IsChecked Then
                Dim stDocName As String = ""
                For Each StrName As String In cbgDocType.CheckedDisplayMember
                    If clsCommon.myLen(stDocName) > 0 Then
                        stDocName += ", "
                    End If
                    stDocName += StrName
                Next
                Dim strDocCode As String = ""
                For Each StrCode As String In cbgDocType.CheckedValue
                    If clsCommon.myLen(strDocCode) > 0 Then
                        strDocCode += ", "
                    End If
                    strDocCode += StrCode
                Next
                arrHeader.Add(("DOC Type: " + stDocName + " "))
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
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click

        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBulkMilkRegister & "'"))



            If chkVendorSelect.IsChecked Then
                Dim stVendorName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(stVendorName) > 0 Then
                        stVendorName += ", "
                    End If
                    stVendorName += StrName
                Next
                Dim strVendorCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strVendorCode) > 0 Then
                        strVendorCode += ", "
                    End If
                    strVendorCode += StrCode
                Next
                arrHeader.Add(("Vendor Name: " + stVendorName + " "))
            End If


            If chkMCCSelect.IsChecked Then
                Dim stMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(stMCCName) > 0 Then
                        stMCCName += ", "
                    End If
                    stMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next
                arrHeader.Add(("MCC Name: " + stMCCName + " "))
            End If

            If chkDocTypeSelect.IsChecked Then
                Dim stDocName As String = ""
                For Each StrName As String In cbgDocType.CheckedDisplayMember
                    If clsCommon.myLen(stDocName) > 0 Then
                        stDocName += ", "
                    End If
                    stDocName += StrName
                Next
                Dim strDocCode As String = ""
                For Each StrCode As String In cbgDocType.CheckedValue
                    If clsCommon.myLen(strDocCode) > 0 Then
                        strDocCode += ", "
                    End If
                    strDocCode += StrCode
                Next
                arrHeader.Add(("DOC Type: " + stDocName + " "))
            End If
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Bulk Milk Register Report", gv, arrHeader, "Bulk Milk Register Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnFatSnf_Click(sender As Object, e As EventArgs) Handles btnFatSnf.Click
        Try
            'If clsCommon.myLen(GateEntryNo) <= 0 Then
            '    Throw New Exception("Not found anything to print")
            'Else
            ' Ticket No : BHA/03/07/18-000124 Create New Print Format For Bharat and Add Chamber_Qty , snf_Per , snf_Per column 
            Dim qry As String = ""
            '       If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "BHAD") = CompairStringResult.Equal Then
            '       qry = " select g.Tanker_No ,g.Gate_Entry_No [Gate-In No] , isnull(o.Doc_No ,'') [Gate-Out No] , (CASE WHEN G.doc_type='MccProc' THEN G.Dispatched_From_Mcc ELSE g.Vendor_Code END) AS  [Vendor Code] ," &
            '"(CASE WHEN G.Doc_Type='MccProc' then TSPL_MCC_MASTER.MCC_NAME else  g.Vendor_Desc end) as [Vendor Name] , COALESCE(G.Supplier_Code ,'')  AS Sub_Vendor_Code, COALESCE(S.DESCRIPTION ,'')  AS Sub_Vendor_Code_Desc, CONVERT (varchar, g.Date_And_Time,103)  [Gate-In Date] , " &
            '"RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14) [Gate-In Time], CONVERT (varchar , O.Doc_Date ,103 ) [Gate-Out Date], RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14) [Gate-Out Time], CM.Item_Code [Item Code] , " &
            '"TSPL_ITEM_MASTER.Item_Desc [Item Desc]  , g.location_Code [Location] , g.Location_Desc [Loc Desc] , g.comp_code [Company Code] , c.Comp_Name [Comp Desc] , CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State ) as [Company Address] , " &
            '"g.Doc_Type [Doc Type],G.snf_Per,g.fat_per,g.MIKL_TYPE_CODE,G.Gate_Entry_Type,G.Seal_Status,G.TotalQty_In_Kg ,CM.Chamber_Desc,tspl_item_master.HSN_Code as HSNCode,CM.UOM, CM.Chamber_Qty,CM.snf_Per as snf_Per_CM, CM.fat_per as fat_per_CM , CM.Line_No,C.Logo_img, isnull(G.No_Of_CAN,'') as No_Of_CAN from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE   LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=G.Dispatched_From_Mcc LEFT JOIN TSPL_Gate_Entry_Chember_Details CM ON CM.GE_Code=G.Gate_Entry_No   left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code=CM.item_code  where 1=1 and convert(date,G.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,G.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "',103) group by g.Gate_Entry_No"
            '   Else
            qry = "Select xxx.*,xxxxx.* from( select LAG(Sum(CM.Chamber_Qty)) OVER (ORDER BY (Select 1)) AS Chamber_Qtyy,LAG(Sum(CM.snf_Per)) OVER (ORDER BY (Select 1)) AS snf_Per_CMm,LAG(Sum(CM.fat_per)) OVER (ORDER BY (Select 1)) AS fat_per_CMm, 
 max('In') as PrintType,max(g.Tanker_No)Tanker_No ,Max(g.Gate_Entry_No) [Gate-In No] , max(isnull(o.Doc_No ,'')) [Gate-Out No], 
 max((CASE WHEN G.doc_type='MccProc' THEN G.Dispatched_From_Mcc ELSE g.Vendor_Code END)) AS  [Vendor Code] ,max((CASE WHEN G.Doc_Type='MccProc' then TSPL_MCC_MASTER.MCC_NAME else  g.Vendor_Desc end)) as [Vendor Name] , max(COALESCE(G.Supplier_Code ,''))  AS Sub_Vendor_Code, max(COALESCE(S.DESCRIPTION ,''))  AS Sub_Vendor_Code_Desc, max(CONVERT (varchar, g.Date_And_Time,103))  [Gate-In Date] , max(RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14)) [Gate-In Time], max(CONVERT (varchar , O.Doc_Date ,103 )) [Gate-Out Date], max(RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14)) [Gate-Out Time], max(g.Item_Code) [Item Code] , max(g.Item_Desc) [Item Desc]  , max(g.location_Code) [Location] , max(g.Location_Desc) [Loc Desc] , max(g.comp_code) [Company Code] , max(c.Comp_Name) [Comp Desc] , max(CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State )) as [Company Address] , max(g.Doc_Type) [Doc Type],sum(G.snf_Per)snf_Per,sum(g.fat_per)fat_per,max(g.MIKL_TYPE_CODE)MIKL_TYPE_CODE,max(G.Gate_Entry_Type)Gate_Entry_Type,max(G.Seal_Status)Seal_Status,sum(G.TotalQty_In_Kg)TotalQty_In_Kg ,max(CM.Chamber_Desc)Chamber_Desc,max(tspl_item_master.HSN_Code) as HSNCode,max(CM.UOM)UOM, sum(CM.Chamber_Qty)Chamber_Qty, sum(CM.snf_Per) as snf_Per_CM, sum(CM.fat_Per) as fat_per_CM
 
 from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code=g.item_code LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=G.Dispatched_From_Mcc LEFT JOIN TSPL_Gate_Entry_Chember_Details CM ON CM.GE_Code=G.Gate_Entry_No
 --left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code
 where 1=1 and convert(date,G.Date_And_Time,103)>=convert(date,'01/01/2024',103) and convert(date,G.Date_And_Time,103) <=convert(date,'28/02/2024 16:47:25',103)
   group by CONVERT (varchar, g.Date_And_Time,103)) As xxx
   
   left Join(
   select TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_DETAIL.SNF_PER as snf_Per_rec,TSPL_MILK_SRN_DETAIL.FAT_PER as fat_per_rec,TSPL_MILK_SRN_DETAIL.Qty as Chamber_Qty_rec,TSPL_MILK_SRN_DETAIL.Item_Code As [Item Code]
   from TSPL_MILK_SRN_DETAIL
  inner join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE where 1=1 and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "',103)) As xxxxx 
  On convert(date,xxx.[Gate-In Date],103)=xxxxx.DOC_DATE  "

            '           = " select max('In') as PrintType,max(g.Tanker_No)Tanker_No ,Max(g.Gate_Entry_No) [Gate-In No] , max(isnull(o.Doc_No ,'')) [Gate-Out No] , 
            'max((CASE WHEN G.doc_type='MccProc' THEN G.Dispatched_From_Mcc ELSE g.Vendor_Code END)) AS  [Vendor Code] ,max((CASE WHEN G.Doc_Type='MccProc' then TSPL_MCC_MASTER.MCC_NAME else  g.Vendor_Desc end)) as [Vendor Name] , max(COALESCE(G.Supplier_Code ,''))  AS Sub_Vendor_Code, max(COALESCE(S.DESCRIPTION ,''))  AS Sub_Vendor_Code_Desc, max(CONVERT (varchar, g.Date_And_Time,103))  [Gate-In Date] , max(RIGHT(CONVERT(VARCHAR(26), g.Date_And_Time, 109),14)) [Gate-In Time], max(CONVERT (varchar , O.Doc_Date ,103 )) [Gate-Out Date], max(RIGHT(CONVERT(VARCHAR(26),  O.Doc_Date, 109),14)) [Gate-Out Time], max(g.Item_Code) [Item Code] , max(g.Item_Desc) [Item Desc]  , max(g.location_Code) [Location] , max(g.Location_Desc) [Loc Desc] , max(g.comp_code) [Company Code] , max(c.Comp_Name) [Comp Desc] , max(CONCAT(c.Add1 , ' ' , c.Add2 , ' ', c.Add3 , ' , ', c.State )) as [Company Address] , max(g.Doc_Type) [Doc Type],sum(G.snf_Per)snf_Per,sum(g.fat_per)fat_per,max(g.MIKL_TYPE_CODE)MIKL_TYPE_CODE,max(G.Gate_Entry_Type)Gate_Entry_Type,max(G.Seal_Status)Seal_Status,sum(G.TotalQty_In_Kg)TotalQty_In_Kg ,max(CM.Chamber_Desc)Chamber_Desc,max(tspl_item_master.HSN_Code) as HSNCode,max(CM.UOM)UOM, sum(CM.Chamber_Qty)Chamber_Qty, sum(CM.snf_Per) as snf_Per_CM, sum(CM.fat_Per) as fat_per_CM from Tspl_Gate_Entry_Details G LEFT JOIN TSPL_SUPPLIER_MASTER S ON G.Supplier_Code = S.SUPPLIER_CODE left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_code=g.item_code LEFT JOIN  TSPL_COMPANY_MASTER C on g.comp_code = c.Comp_Code LEFT JOIN TSPL_Gate_Out O ON G.Gate_Entry_No = O.Gate_Entry_No LEFT OUTER JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=G.Dispatched_From_Mcc LEFT JOIN TSPL_Gate_Entry_Chember_Details CM ON CM.GE_Code=G.Gate_Entry_No where 1=1 and convert(date,G.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,G.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "',103) group by CONVERT (varchar, g.Date_And_Time,103)"
            '           'End If

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptGateInfatsnfcomparison", "Milk Procurement Bulk Gate In", clsCommon.myCDate(dt.Rows(0)("Gate-In Date")))
            frmCRV = Nothing
            'End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
End Class