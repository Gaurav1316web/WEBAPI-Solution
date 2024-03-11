Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptMccBulkMilkRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim arrLoc As String = Nothing



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
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptBulkMilkRegister)
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
        'cbgShift.DisplayMember = "Shift"
    End Sub
    Sub LoadDocType()
        Dim qry As String = "Select xxx.Code,  xxx.Name From (Select 'Bulk' As Code, 'Bulk Milk' As Name  Union  Select 'MCC' As Code,    'MCC Milk' As Name) xxx "
        cbgDocType.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgDocType.ValueMember = "Code"
        cbgDocType.DisplayMember = "Name"

    End Sub
    Sub LoadVendor()
        Dim qry As String = "Select TSPL_VENDOR_MASTER.Vendor_Code As Code,  TSPL_VENDOR_MASTER.Vendor_Name As Name From TSPL_VENDOR_MASTER  "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"

    End Sub
    Public Sub Load_Report(Optional ByVal BulkExport As Integer = 0, Optional dt As DataTable = Nothing)
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkDocTypeSelect.IsChecked AndAlso cbgDocType.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Doc Type or select all.", Me.Text)
            Exit Sub
        End If
        'If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
        '    Exit Sub
        'End If

        If chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor or select all.", Me.Text)
            Exit Sub
        End If

        '   Dim dtExtraColumn As DataTable = clsDBFuncationality.GetDataTable("select description from tspl_parameter_master where type not in('FAT','SNF','CLR')")


        Dim SQuery As String = Nothing

        'SQuery = "   (Select  DISTINCT   max(DocType) as DocType,(xx.[Vendor Code]) as [Code],max(xx.[Vendor Name]) as [Name],sum(xx.[Gross Weight]) as [Gross Weight],max(xx.Milk_Grade_code) as [Grade Code],max(xx.DESCRIPTION) as [Grade Name],max(xx.[MCC Name]) as [MCC Name],  avg(xx.[FAT %]) as [Fat %],avg(xx.[SNF %]) as  [SNF %], sum( Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) )As FATKG, sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100))) As SNFKG 
        SQuery = "  (Select  DISTINCT  max(DocType) as DocType,(xx.[Vendor Code]) as [Code],max(xx.[Vendor Name]) as [Name],max(xx.[Challan Date]) as [Doc Date],'' as [Shift], '' as [Count of Sample],sum(xx.[Net Weight]) as [Net Weight],isnull(sum(xx.[Total Amount Temp]),0) as [Amount],max(xx.Milk_Grade_code) as [Grade Code],max(xx.DESCRIPTION) as [Grade Name],  convert(decimal(18,2), avg(xx.[FAT %])) as [Fat %],convert(decimal(18,2),avg(xx.[SNF %])) as  [SNF %], sum( Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) )As FATKG, sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100))) As SNFKG,convert(decimal(18,3),avg(xx.Secondary_snf_per)) as [Secondary Snf %],convert(decimal(18,3),avg(xx.Secondary_fat_per)) as [Secondary Fat %], sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.Secondary_fat_per /    100))) As [Secondary FATKG], sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.Secondary_snf_per /    100))) As [Secondary SNFKG],0 as [Physical Closing Stock] From ( Select TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,TSPL_MILK_GRADE_MASTER.DESCRIPTION,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],   Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) + ' ' + Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,108) As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No],  Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], (case when TSPL_Bulk_MILK_SRN.isPosted IS NULL  then '' WHEN  TSPL_Bulk_MILK_SRN.isPosted IS NOT NULL  AND TSPL_Bulk_MILK_SRN.isPosted = 1 THEN 'Posted' else 'Pending' end ) as [SRN Status],  Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) + ' ' + Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,108) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) As [Weighment Date],   Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) + ' ' +  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,108) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No],  Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) as Weighment_date,   TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty As [Challan Qty],   TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], TSPL_SECONDARY_SETTING_QC_HEAD.Document_No AS [Secondary QC No],  Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) + ' ' + Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,108) As [Unloading Date Time],   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108)  As [QC Date Time],  Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]  ,convert(decimal(18,3),TSPL_SECONDARY_SETTING_QC_detail.FatPer) as [Secondary_fat_per],convert(decimal(18,3),TSPL_SECONDARY_SETTING_QC_detail.SNFPer) as [Secondary_snf_per] From Tspl_Gate_Entry_Details  left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc  Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  LEFT OUTER JOIN  TSPL_SECONDARY_SETTING_QC_HEAD  ON TSPL_SECONDARY_SETTING_QC_HEAD.QC_No = TSPL_QUALITY_CHECK.QC_No  left outer join TSPL_SECONDARY_SETTING_QC_detail on TSPL_SECONDARY_SETTING_QC_detail.document_no=TSPL_SECONDARY_SETTING_QC_HEAD.document_no where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103) "


        'SQuery = "  (Select  DISTINCT  max(DocType) as DocType,(xx.[Vendor Code]) as [Code],max(xx.[Vendor Name]) as [Name],max(xx.[Challan Date]) as [Doc Date],'' as [Shift], '' as [Count of Sample],sum(xx.[Net Weight]) as [Net Weight],sum(xx.[Total Amount Temp]) as [Amount],max(xx.Milk_Grade_code) as [Grade Code],max(xx.DESCRIPTION) as [Grade Name],  avg(xx.[FAT %]) as [Fat %],avg(xx.[SNF %]) as  [SNF %], sum( Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) )As FATKG, sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100))) As SNFKG,avg(xx.Secondary_snf_per) as [Secondary Snf %],avg(xx.Secondary_fat_per) as [Secondary Fat %], sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.Secondary_fat_per /    100))) As [Secondary FATKG], sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.Secondary_snf_per /    100))) As [Secondary SNFKG],0 as [Physical Closing Stock] From ( Select TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,TSPL_MILK_GRADE_MASTER.DESCRIPTION,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Milk' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],   Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) + ' ' + Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,108) As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No],  Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No],   Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) + ' ' + Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,108) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) As [Weighment Date],   Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) + ' ' +  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,108) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No],  Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) as Weighment_date,   TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty As [Challan Qty],   TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No],  Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) + ' ' + Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,108) As [Unloading Date Time],   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108)  As [QC Date Time],  Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]  ,convert(decimal(18,2),TSPL_SECONDARY_SETTING_QC_detail.FatPer) as [Secondary_fat_per],convert(decimal(18,2),TSPL_SECONDARY_SETTING_QC_detail.SNFPer) as [Secondary_snf_per]  From Tspl_Gate_Entry_Details  left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc  Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No   left outer join TSPL_SECONDARY_SETTING_QC_HEAD on TSPL_SECONDARY_SETTING_QC_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_SECONDARY_SETTING_QC_detail on TSPL_SECONDARY_SETTING_QC_detail.document_no=TSPL_SECONDARY_SETTING_QC_HEAD.document_no where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
        If cbgMCC.CheckedValue.Count > 0 Then
            SQuery += "and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If

        SQuery += " and Tspl_Gate_Entry_Details.Doc_Type in ('BulkProc')"

        If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
            SQuery += " and Tspl_Gate_Entry_Details.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
        End If

        SQuery += "   ) As xx group by [Vendor Code]) "

        SQuery += "Union all"

        'SQuery += "  select  'MCC Milk' as doctype, aa.[MCC Code] ,aa.[MCC Name] ,aa.[Milk Weight] ,'' as Type1	,'' as Type1,'' as Type1,aa.[FAT(%)] ,aa.[SNF(%)] ,aa.[FAT(KG)] ,aa.[SNF(KG)] 
        'SQuery += " Select 'MCC Milk' as doctype,final.MCC as [MCC Code] ,final.[MCC Name] ,final.[Doc Date] ,final.Shift , final.[Sample No],([SRN Amount]+EMP_Amount-Service_Charge_Amount) as NetAmount ,'' as grade1,'' as grade2, final.[FAT(%)] ,final.[SNF(%)] ,final.[FAT(KG)],final.[SNF(KG)]  From ( Select TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL    From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code 

        SQuery += "  Select 'MCC Milk' as doctype,final.MCC as [MCC Code] ,max(final.[MCC Name]) as [MCC Name] ,final.[Doc Date] ,(final.Shift) , max(final.[Sample No]) as [Sample No],sum(final.[Milk Weight(KG)]) as [Gross Weight],isnull(sum(([SRN Amount]+EMP_Amount-Service_Charge_Amount)),0) as NetAmount ,max('') as grade1,max('') as grade2, convert(decimal(18,2),avg(final.[FAT(%)])) as [Fat %],convert(decimal(18,2),avg(final.[SNF(%)])) as  [SNF %],sum(final.[FAT(KG)]) as [Fat(KG)],sum(final.[SNF(KG)]) as [SNF(KG)] ,convert(decimal(18,3),avg([Physical Fat_per])) as [Physical Fat_per],convert(decimal(18,3),avg([Physical SNF_Per])) as [Physical SNF_Per],max([Physical Fat_KG]) as [Physical Fat_KG],max([Physical Snf_KG]) as [Physical Snf_KG],max([Physical Closing Stock]) as [Physical Closing Stock] From ( Select TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL ,'' as RejectType,'' as RejectReason,'' as Defaulter  ,TSPL_MILK_Shift_End_Head.Manual_Stock as [Physical Closing Stock],TSPL_MILK_Shift_End_Head.Manual_FAT_Per as [Physical Fat_per],TSPL_MILK_Shift_End_Head.Manual_SNF_Per as [Physical SNF_Per],TSPL_MILK_Shift_End_Head.Manual_FAT as [Physical Fat_KG],TSPL_MILK_Shift_End_Head.Manual_SNF as [Physical Snf_KG]  From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code where 2 = 2  and TSPL_MILK_RECEIPT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            SQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )  "
            'strSRNQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            SQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  )"
        End If
        SQuery += " and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null"

        If cbgMCC.CheckedValue.Count > 0 Then
            SQuery += " and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If

        SQuery += "  Union all"

        SQuery += " Select TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Cow FAT(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT < 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Cow SNF(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.FAT Else 0 End [Buffalo FAT(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.SNF Else 0 End [Buffalo SNF(%)],  Case When TSPL_MILK_REJECT_DETAIL.FAT <= 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG Else 0 End [Cow Milk Qty (KG)],  Case When TSPL_MILK_REJECT_DETAIL.FAT > 5 Then TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_LTR Else 0 End [Buffalo Milk Qty (KG)],  Case When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_REJECT_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type],  TSPL_MILK_REJECT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_REJECT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name],  Convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_REJECT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_REJECT_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_REJECT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_REJECT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_REJECT_DETAIL.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_REJECT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_REJECT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_REJECT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(KG)], TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG As [Milk Weight(LTR)], TSPL_MILK_REJECT_DETAIL.FAT As [FAT(%)], TSPL_MILK_REJECT_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2), TSPL_MILK_REJECT_DETAIL.FAT * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [FAT(KG)],  Convert(decimal(18,2),TSPL_MILK_REJECT_DETAIL.SNF * TSPL_MILK_REJECT_DETAIL.ACC_WEIGHT_KG / 100) As [SNF(KG)], '' As [Sample Status],  TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate],  TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],  TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date ,  '' as IS_MANUAL ,'' as MACHINE_NO ,'' as IS_MILK_SAMPLE_MANUAL,case when TSPL_MILK_REJECT_DETAIL.Is_Return=0 then '' when TSPL_MILK_REJECT_DETAIL.Is_Return=1 then 'Return' when TSPL_MILK_REJECT_DETAIL.Is_Return=2 then 'Drain' end as RejectType,  TSPL_MILK_REJECT_DETAIL.Reject_Type as ReajectReason,TSPL_MILK_REJECT_DETAIL.Defaulter  ,TSPL_MILK_Shift_End_Head.Manual_Stock as [Physical Closing Stock],TSPL_MILK_Shift_End_Head.Manual_FAT_Per as [Physical Fat_per],TSPL_MILK_Shift_End_Head.Manual_SNF_Per as [Physical SNF_Per],TSPL_MILK_Shift_End_Head.Manual_FAT as [Physical Fat_KG],TSPL_MILK_Shift_End_Head.Manual_SNF as [Physical Snf_KG]  From  TSPL_MILK_REJECT_DETAIL Left Outer Join TSPL_MILK_REJECT_HEAD On TSPL_MILK_REJECT_HEAD.DOC_CODE = TSPL_MILK_REJECT_DETAIL.DOC_CODE  left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_REJECT_HEAD.DOC_CODe=TSPL_MILK_SRN_HEAD.Against_Reject_No and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_REJECT_DETAIL.SAMPLE_NO  Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_REJECT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On  TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_REJECT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On  TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_REJECT_DETAIL.VSP_CODE Left Outer Join  TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_REJECT_DETAIL.ROUTE_CODE Left Outer Join   TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_REJECT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_REJECT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_REJECT_HEAD.SHIFT   Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code where  Against_Reject_No <> '' and TSPL_MILK_REJECT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            SQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )  "
            'strSRNQuery += " and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            SQuery += " and 2=( case when TSPL_MILK_REJECT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_REJECT_HEAD.SHIFT='E' then 3 else 2 end  )"
        End If




        SQuery += "  ) As final where 2=2   group by [Doc Date],MCC,Shift "
        ') as  pp group by pp.[MCC Code]  )as xx ) as xxx ) as aa "


        '' bulk export
        If BulkExport = 1 Then
            transportSql.BulkExport("Day_Wise_Report", SQuery, "", "csv", "")
            Exit Sub
        ElseIf BulkExport = 2 Then
            transportSql.BulkExport("Day_Wise_Report", SQuery, "", "xls", "")
            Exit Sub
        End If


        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(SQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()



            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
            Next
            gv.BestFitColumns()


            FormatGrid(dtgv)

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If


        ReStoreGridLayout()
        'View()
    End Sub
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
        'For ii As Integer = 0 To gv.Rows.Count - 1

        For Each grow As GridViewRowInfo In gv.Rows
            Dim checkrows As Double = grow.Cells("Amount").Value
            If clsCommon.CompairString(checkrows, "0") = CompairStringResult.Equal Then
                grow.IsVisible = False
            End If

        Next
        '  Next

        Dim summaryRowItem As New GridViewSummaryRowItem()


        'Dim intCount As Integer = 0
        'If dtExtraColumn IsNot Nothing AndAlso dtExtraColumn.Rows.Count > 0 Then
        '    For Each dr As DataRow In dtExtraColumn.Rows
        '        Dim item1 As New GridViewSummaryItem(clsCommon.myCstr(dr(0)), "{0:F2}", GridAggregateFunction.Sum)
        '        summaryRowItem.Add(item1)
        '    Next
        'End If

        Dim item2 As New GridViewSummaryItem("SNF Rate", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("FATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item8 As New GridViewSummaryItem("Net Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        'Dim item6 As New GridViewSummaryItem("Secondary FATKG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        'Dim item7 As New GridViewSummaryItem("Secondary SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)

        'Dim summaryItem1 As New GridViewSummaryItem()
        'summaryItem1.FormatString = "{0:F2}"
        'summaryItem1.Name = "Fat %"
        'summaryItem1.AggregateExpression = "sum(FATKG)*100/sum([Net Weight])"
        'summaryRowItem.Add(summaryItem1)

        'Dim summaryItem2 As New GridViewSummaryItem()
        'summaryItem2.FormatString = "{0:F2}"
        'summaryItem2.Name = "SNF %"
        'summaryItem2.AggregateExpression = "sum(SNFKG)*100/sum([Net Weight])"
        'summaryRowItem.Add(summaryItem2)



        'Dim summaryItem3 As New GridViewSummaryItem()
        'summaryItem3.FormatString = "{0:F2}"
        'summaryItem3.Name = "Secondary Snf %"
        'summaryItem3.AggregateExpression = "sum(Secondary SNFKG)*100/sum([Net Wieght])"
        'summaryRowItem.Add(summaryItem3)

        'Dim summaryItem4 As New GridViewSummaryItem()
        'summaryItem4.FormatString = "{0:F2}"
        'summaryItem4.Name = "Secondary Fat %"
        'summaryItem4.AggregateExpression = "sum(Secondary FATKG)*100/sum([Net Weight])"
        'summaryRowItem.Add(summaryItem4)


        'Dim item6 As New GridViewSummaryItem("FAT Amt", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        'Dim item7 As New GridViewSummaryItem("SNF Amt", "{0:F2} ", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)
        'Dim item8 As New GridViewSummaryItem("Total Amount Temp", "{0:F2} ", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item8)
        'Dim item9 As New GridViewSummaryItem("SNF Amt", "{0:F2} ", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item9)
        'Dim item10 As New GridViewSummaryItem("Total Amount", "{0:F2} ", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item10)
        'Dim item11 As New GridViewSummaryItem("FATKG", "{0:F2} ", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item11)
        'Dim item12 As New GridViewSummaryItem("SNFKG", "{0:F2} ", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item12)


        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
        gv1.Size = New System.Drawing.Size(456, 311)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


        gv1.MasterTemplate.ShowTotals = True

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        funPrint()

    End Sub
    Private Sub funPrint()
        Try
            Dim SQuery As String = Nothing

            SQuery = "  (Select  DISTINCT  max(DocType) as DocType,(xx.[Vendor Code]) as [Code],max(xx.[Vendor Name]) as [Name],max(xx.[Challan Date]) as [Doc Date],'' as [Shift], '' as [Count of Sample],sum(xx.[Gross Weight]) as [Gross Weight KG] ,sum(xx.[Net Weight]) as [Net Weight],max(xx.Milk_Grade_code) as [Grade Code],max(xx.DESCRIPTION) as [Grade Name],  avg(xx.[FAT %]) as [Fat %],avg(xx.[SNF %]) as  [SNF %], sum( Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) )As FATKG, sum(Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100))) As SNFKG From  ( Select TSPL_QUALITY_CHEMBER_DETAILS.MIKL_TYPE_CODE,TSPL_QUALITY_CHEMBER_DETAILS.Milk_Grade_code,TSPL_MILK_GRADE_MASTER.DESCRIPTION,GRADE_TYPE,TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Milk' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],   Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103) + ' ' + Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,108) As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No],  Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No],   Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) + ' ' + Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,108) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) As [Weighment Date],   Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) + ' ' +  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,108) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No],  Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Date_And_Time,108) as Weighment_date,   TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Qty As [Challan Qty],   TSPL_WEIGHMENT_CHEMBER_DETAILS.Gross_Weight As [Gross Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Tare_Weight As [Tare Weight],  TSPL_WEIGHMENT_CHEMBER_DETAILS.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name],  TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code As [Item Code], TSPL_ITEM_MASTER.Item_Desc As [Item Desc],   Case When IsNull(TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else TSPL_GATE_ENTRY_CHEMBER_DETAILS.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No],  Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) + ' ' + Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,108) As [Unloading Date Time],   Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108)  As [QC Date Time],  Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code], TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No],  Convert(varchar,TSPL_Gate_Out.Doc_Date,103) + ' ' + Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,108) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %],  Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %],  Convert(decimal(18,2),t_CLR.Param_Field_Value) As CLR, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.StandardRate As [Standard Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate], TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Incentive, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Deduction, TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SpecialDeduction As [Special Deduction],   Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.NetRate) As [Net Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.fat_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE End As [FAT Rate],			Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SNF_Rate) Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.FatAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SnfAmt) Else (TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_RATE * TSPL_MCC_DISPATCH_CHALLAN_DETAIL.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Actual_Amount Else TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Amount End As [Total Amount Temp],  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage',  'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_BULK_PRICE_DETAIL.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]  From Tspl_Gate_Entry_Details  left outer join TSPL_GATE_ENTRY_CHEMBER_DETAILS on Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_GATE_ENTRY_CHEMBER_DETAILS.GE_Code and Chamber_Qty > 0  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_WEIGHMENT_CHEMBER_DETAILS on TSPL_Weighment_Detail.Weighment_No=TSPL_WEIGHMENT_CHEMBER_DETAILS.Weighment_No and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_WEIGHMENT_CHEMBER_DETAILS.Chamber_Desc  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code   Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code   Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code   Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   left outer join TSPL_QUALITY_CHEMBER_DETAILS on TSPL_QUALITY_CHECK.QC_No=TSPL_QUALITY_CHEMBER_DETAILS.QC_No  and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_QUALITY_CHEMBER_DETAILS.Chamber_Desc left outer join TSPL_MILK_GRADE_MASTER on TSPL_MILK_GRADE_MASTER.MILK_GRADE_CODE=TSPL_QUALITY_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On   TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  left outer join TSPL_BULK_MILK_SRN_CHEMBER_DETAILS on TSPL_Bulk_MILK_SRN.SRN_NO=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.SRN_NO and TSPL_GATE_ENTRY_CHEMBER_DETAILS.Chamber_Desc=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.Chamber_Desc  Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  left outer join TSPL_BULK_PRICE_DETAIL on TSPL_Bulk_Price_MASTER.Price_Code=TSPL_BULK_PRICE_DETAIL.Price_code  and TSPL_BULK_PRICE_DETAIL.Milk_Grade_code=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.MILK_GRADE_CODE  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO and tspl_Bulk_milk_purchase_Invoice_Detail.CHAMBER_DESC=TSPL_BULK_MILK_SRN_CHEMBER_DETAILS.CHAMBER_DESC  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GATE_ENTRY_CHEMBER_DETAILS.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No left outer join TSPL_MCC_DISPATCH_CHALLAN_DETAIL on TSPL_MCC_Dispatch_Challan.Chalan_NO=TSPL_MCC_DISPATCH_CHALLAN_DETAIL.Chalan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*  From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No and t_FAT.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  and t_SNF.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.* From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No and t_CLR.LINE_NO=TSPL_GATE_ENTRY_CHEMBER_DETAILS.Line_No  where 2 = 2 and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Tspl_Gate_Entry_Details.Date_And_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            If cbgMCC.CheckedValue.Count > 0 Then
                SQuery += "and Tspl_Gate_Entry_Details.location_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If

            SQuery += " and Tspl_Gate_Entry_Details.Doc_Type in ('BulkProc')"

            If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
                SQuery += " and Tspl_Gate_Entry_Details.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")  "
            End If

            SQuery += "   ) As xx group by [Vendor Code]) "

            SQuery += "Union all"

            SQuery += "    Select 'MCC Milk' as doctype,final.MCC as [MCC Code] ,max(final.[MCC Name]) as [MCC Name] ,final.[Doc Date] ,(final.Shift) , max(final.[Sample No]) as [Sample No],sum(final.[Milk Weight(KG)]) as [Gross Weight],sum(([SRN Amount]+EMP_Amount-Service_Charge_Amount)) as NetAmount ,max('') as grade1,max('') as grade2, avg(final.[FAT(%)]) as [Fat %],avg(final.[SNF(%)]) as  [SNF %],sum(final.[FAT(KG)]) as [Fat(KG)],sum(final.[SNF(KG)]) as [SNF(KG)]  From ( Select TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT < 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.FAT Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_SAMPLE_DETAIL.SNF Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT <= 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SAMPLE_DETAIL.FAT > 5 Then TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT Else 0 End [Buffalo Milk Qty (KG)], Case When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 0 Then '' When Coalesce(TSPL_MILK_SAMPLE_DETAIL.FAT, 0) <= 5 Then 'C' Else 'B' End As [Milk Type], TSPL_MILK_RECEIPT_HEAD.DOC_CODE As [Milk Receipt Code], TSPL_MILK_RECEIPT_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_RECEIPT_DETAIL.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE As [Route Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO As [Sample No],  TSPL_MILK_RECEIPT_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT As [Milk Weight], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As [Milk Weight(KG)], TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR As [Milk Weight(LTR)], TSPL_MILK_SAMPLE_DETAIL.FAT As [FAT(%)], TSPL_MILK_SAMPLE_DETAIL.SNF As [SNF(%)], Convert(decimal(18,2), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [FAT(KG)], Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As [SNF(KG)], Case When TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL = '' Then 'Auto' Else TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_Shift_End_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , tspl_milk_receipt_detail.IS_MANUAL , tspl_milk_receipt_detail.MACHINE_NO,(CASE WHEN TSPL_MILK_SAMPLE_DETAIL.IS_MANUAL='Auto' THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL    From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code  where 2 = 2  and TSPL_MILK_RECEIPT_HEAD.DOC_DATE >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and 2=( case when TSPL_MILK_RECEIPT_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and TSPL_MILK_RECEIPT_DETAIL.SHIFT='E' then 3 else 2 end  ) and TSPL_MILK_RECEIPT_DETAIL.Against_Uploader_TR_No is null "

            If cbgMCC.CheckedValue.Count > 0 Then
                SQuery += " and TSPL_MILK_RECEIPT_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If

            SQuery += "  ) As final where 2=2    group by [Doc Date],MCC,Shift  "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(SQuery)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dtgv, clsERPFuncationality.CompanyAddresShowinHeader(), "rptMccBulkMilkRegister", "Day Wise Milk Register", "Address.rpt")
                frmCRV = Nothing
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
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
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
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

    Private Sub RptBulkMilkRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadShiftFrom()
        LoadShiftTo()
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
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBulkMilkRegister & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

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
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Bulk Milk Register Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BulkExportCSV_Click(sender As Object, e As EventArgs) Handles BulkExportCSV.Click
        Load_Report(1)
    End Sub

    Private Sub BulkExport_Click(sender As Object, e As EventArgs) Handles BulkExport.Click
        Load_Report(2)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class