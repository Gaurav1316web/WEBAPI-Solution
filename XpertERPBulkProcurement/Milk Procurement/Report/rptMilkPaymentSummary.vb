Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class rptMilkPaymentSummary
    Inherits FrmMainTranScreen
    Dim StrPermission As String
    Dim Slot1FD As DateTime = Nothing
    Dim Slot1TD As DateTime = Nothing
    Dim Slot2FD As DateTime = Nothing
    Dim Slot2TD As DateTime = Nothing
    Dim Slot3FD As DateTime = Nothing
    Dim Slot3TD As DateTime = Nothing
    Dim AreaWiseBilling As Boolean = False

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Sub Reset()
        gv.DataSource = Nothing
        txtMCC.arrValueMember = Nothing
        fndArea.Value = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged

        Dim selectedMonth As Integer = txtMonth.Value.Month
        Dim selectedYear As Integer = txtMonth.Value.Year

        Dim currentDate As New DateTime(selectedYear, selectedMonth, 1)
        Slot1FD = clsCommon.GetPrintDate(currentDate, "dd/MMM/yyyy")
        Slot1TD = clsCommon.GetPrintDate(currentDate.AddDays(9), "dd/MMM/yyyy")
        Slot2FD = clsCommon.GetPrintDate(currentDate.AddDays(10), "dd/MMM/yyyy")
        Slot2TD = clsCommon.GetPrintDate(currentDate.AddDays(19), "dd/MMM/yyyy")
        Slot3FD = clsCommon.GetPrintDate(currentDate.AddDays(20), "dd/MMM/yyyy")
        Slot3TD = clsCommon.GetPrintDate(currentDate.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        Dim qry As String = ""
        Dim dt As New DataTable()
        Dim arrMCC As ArrayList = Nothing

        Try
            Dim whrRej As String = ""
            Dim whrRec As String = ""
            Dim whre As String = ""
            Dim AreaName As String = ""
            If AreaWiseBilling Then
                If clsCommon.myLen(fndArea.Value) > 0 Then
                    whre += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                    AreaName = ",max(MCCName)AreaName"
                End If
            Else
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    whrRec += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                    whrRej += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                Else
                    whrRec += "And TSPL_MILK_SRN_HEAD.mcc_Code in (" & StrPermission & ")"
                    whrRej += "And TSPL_MILK_SRN_HEAD.mcc_Code in (" & StrPermission & ")"
                End If
            End If

            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    whrRej += "And  TSPL_MILK_REJECT_HEAD.mcc_Code in (" & StrPermission & ") "
            '    whrRec += "And  TSPL_MILK_RECEIPT_DETAIL.mcc_Code in (" & StrPermission & ") "
            'End If

            qry = "  select  (xx.MCC),max(MCCName)MCCName " + AreaName + ",sum(xx.SrnQty)SrnQty,sum(xx.SrnAmt)SrnAmt,sum(xx.FATKG)FATKG,sum(xx.SNFKG)SNFKG,
                            case when sum(SrnQty )=0 then 0 else (sum(FATKG )/sum(SrnQty ))*100 end as FATAVG,
                            case when sum(SrnQty )=0 then 0 else (sum(SNFKG )/sum(SrnQty ))*100 end as SNFAVG,
                            sum(xx.SrnQty2)SrnQty2,sum(xx.SrnAmt2)SrnAmt2,sum(xx.FATKG2)FATKG2,sum(xx.SNFKG2)SNFKG2,
                            case when sum(SrnQty2 )=0 then 0 else (sum(FATKG2 )/sum(SrnQty2 ))*100 end as FATAVG2,
                            case when sum(SrnQty2 )=0 then 0 else (sum(SNFKG2 )/sum(SrnQty2 ))*100 end as SNFAVG2,
                            sum(xx.SrnQty3)SrnQty3,sum(xx.SrnAmt3)SrnAmt3,sum(xx.FATKG3)FATKG3,sum(xx.SNFKG3)SNFKG3,
                            case when sum(SrnQty3 )=0 then 0 else (sum(FATKG3 )/sum(SrnQty3 ))*100 end as FATAVG3,
                            case when sum(SrnQty3 )=0 then 0 else (sum(SNFKG3 )/sum(SrnQty3 ))*100 end as SNFAVG3
                            from(
                            select  max(x.MCCName)MCCName,(x.MCC)   , SUM(x.[SRN Qty] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <='" + clsCommon.GetPrintDate(Slot1TD) + "' THEN 1 ELSE 0 END) AS SrnQty,
	                                SUM(x.[SRN Amount] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <='" + clsCommon.GetPrintDate(Slot1TD) + "' THEN 1 ELSE 0 END) AS SrnAmt,
		                            SUM(x.[FAT(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot1TD) + "' THEN 1 ELSE 0 END) AS FATKG,
		                            SUM(x.[SNF(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot1TD) + "'  THEN 1 ELSE 0 END) AS SNFKG,
		                            SUM(x.[SRN Qty] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS SrnQty2,
		                            SUM(x.[SRN Amount] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS SrnAmt2,
		                            SUM(x.[FAT(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS FATKG2,
		                            SUM(x.[SNF(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS SNFKG2 ,
       	                            SUM(x.[SRN Qty] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS SrnQty3,
                                    SUM(x.[SRN Amount] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS SrnAmt3,
    	                            SUM(x.[FAT(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >='" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS FATKG3,
    	                            SUM(x.[SNF(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS SNFKG3 from
                            ( Select  TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Buffalo Milk Qty (KG)]
                            ,  Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 0 Then 'M' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 5.5  Then 'C' Else 'M' End As [Milk Type], TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code],TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, "
            If AreaWiseBilling = True Then
                qry += " TSPL_LOCATION_MASTER.Location_Desc As MCCName,"
            Else
                qry += " TSPL_MCC_MASTER.MCC_NAME As MCCName,"
            End If

            qry += "  isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],  TSPL_MILK_SHIFT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.qty As [Milk Weight],TSPL_MILK_SRN_DETAIL.UOM_Code, TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)], TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,   TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = 1 Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample , '' as MACHINE_NO,(CASE WHEN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample= 1 THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount ,'' as RejectType,'' as RejectReason,'' as Defaulter   ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount
                             ,  TSPL_MILK_SRN_DETAIL.Price_code,[Transporter Code], [Transporter Name], isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount, 0) As Handling_Charges_Amount   , (isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply, 0) * TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  As VSP_Commission_Amount, (isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply, 0) * TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  As VSP_Deduction_Amount, TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard, 0) = 1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code] 
                             From TSPL_MILK_SRN_DETAIL 
                              Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
							 left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
							 left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No                 
                             Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE 
                             Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                             Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                             Left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                             Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                             Left Join(select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code], tspl_vendor_master.vendor_name As [Transporter Name], TSPL_Primary_Vehicle_Master.mcc_code, TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code And tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                             Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                             Left outer join TSPL_MILK_PRICE_SNF_DEDUCTION On TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TSPL_MILK_SRN_DETAIL.Price_code And cast(TSPL_MILK_SRN_DETAIL.SNF_PER As Decimal(18, 1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per"
            If AreaWiseBilling = True Then
                qry += " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Area_Location_Code "
            Else
                qry += " Left outer Join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code"
            End If

            qry += " where 2 = 2 And convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + clsCommon.GetPrintDate(Slot3TD) + "',103) " + whrRec + whre + " "


            qry += ")x group by x.MCC
                             )xx group by xx.MCC   "

            If clsCommon.myLen(qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.GroupDescriptors.Clear()
                gv.SummaryRowsBottom.Clear()
                gv.DataSource = dt
                'gv1.Columns("TransType").IsVisible = False
                'gv1.Columns("PROD_ENTRY_CODE").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2
                gv.BestFitColumns()
                'FormatGrid()
                'ReStoreGridLayout()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to display.", Me.Text)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub


    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        If AreaWiseBilling Then
            gv.Columns("AreaName").Width = 100
            gv.Columns("AreaName").IsVisible = True
            gv.Columns("AreaName").HeaderText = "AreaName"
        End If
        gv.Columns("MCC").Width = 100
        gv.Columns("MCC").IsVisible = False
        gv.Columns("MCC").HeaderText = "MCC"

        gv.Columns("MCCName").Width = 100
        gv.Columns("MCCName").IsVisible = True
        gv.Columns("MCCName").HeaderText = "MCCName"

        gv.Columns("SrnQty").Width = 100
        gv.Columns("SrnQty").IsVisible = True
        gv.Columns("SrnQty").HeaderText = "Quantity"

        gv.Columns("SrnAmt").Width = 100
        gv.Columns("SrnAmt").IsVisible = True
        gv.Columns("SrnAmt").HeaderText = "Amount"

        gv.Columns("FATKG").Width = 100
        gv.Columns("FATKG").IsVisible = True
        gv.Columns("FATKG").HeaderText = "FATKG"

        gv.Columns("SNFKG").Width = 100
        gv.Columns("SNFKG").IsVisible = True
        gv.Columns("SNFKG").HeaderText = "SNFKG"

        gv.Columns("FATAVG").Width = 100
        gv.Columns("FATAVG").IsVisible = True
        gv.Columns("FATAVG").HeaderText = "FATAVG"
        gv.Columns("FATAVG").FormatString = "{0n2}"

        gv.Columns("SNFAVG").Width = 100
        gv.Columns("SNFAVG").IsVisible = True
        gv.Columns("SNFAVG").HeaderText = "SNFAVG"
        gv.Columns("SNFAVG").FormatString = "{0n2}"

        gv.Columns("SrnQty2").Width = 100
        gv.Columns("SrnQty2").IsVisible = True
        gv.Columns("SrnQty2").HeaderText = "Quantity2"

        gv.Columns("SrnAmt2").Width = 100
        gv.Columns("SrnAmt2").IsVisible = True
        gv.Columns("SrnAmt2").HeaderText = "Amount2"

        gv.Columns("FATKG2").Width = 100
        gv.Columns("FATKG2").IsVisible = True
        gv.Columns("FATKG2").HeaderText = "FATKG2"

        gv.Columns("SNFKG2").Width = 100
        gv.Columns("SNFKG2").IsVisible = True
        gv.Columns("SNFKG2").HeaderText = "SNFKG2"

        gv.Columns("FATAVG2").Width = 100
        gv.Columns("FATAVG2").IsVisible = True
        gv.Columns("FATAVG2").HeaderText = "FATAVG2"
        gv.Columns("FATAVG2").FormatString = "{0n2}"

        gv.Columns("SNFAVG2").Width = 100
        gv.Columns("SNFAVG2").IsVisible = True
        gv.Columns("SNFAVG2").HeaderText = "SNFAVG2"
        gv.Columns("SNFAVG2").FormatString = "{0n2}"

        gv.Columns("SrnQty3").Width = 100
        gv.Columns("SrnQty3").IsVisible = True
        gv.Columns("SrnQty3").HeaderText = "Quantity3"

        gv.Columns("SrnAmt3").Width = 100
        gv.Columns("SrnAmt3").IsVisible = True
        gv.Columns("SrnAmt3").HeaderText = "Amount3"

        gv.Columns("FATKG3").Width = 100
        gv.Columns("FATKG3").IsVisible = True
        gv.Columns("FATKG3").HeaderText = "FATKG3"

        gv.Columns("SNFKG3").Width = 100
        gv.Columns("SNFKG3").IsVisible = True
        gv.Columns("SNFKG3").HeaderText = "SNFKG3"

        gv.Columns("FATAVG3").Width = 100
        gv.Columns("FATAVG3").IsVisible = True
        gv.Columns("FATAVG3").HeaderText = "FATAVG3"
        gv.Columns("FATAVG3").FormatString = "{0n2}"

        gv.Columns("SNFAVG3").Width = 100
        gv.Columns("SNFAVG3").IsVisible = True
        gv.Columns("SNFAVG3").HeaderText = "SNFAVG3"
        gv.Columns("SNFAVG3").FormatString = "{0n2}"
        'Dim summaryRowItem As New GridViewSummaryRowItem()

        'Dim item1 As New GridViewSummaryItem("Total Sale", "{0F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

    End Sub
    Private Sub rptMilkPaymentSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AreaWiseBilling = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AreaWiseBilling, clsFixedParameterCode.AreaWiseBilling, Nothing)) = 1)
        fndArea.Visible = AreaWiseBilling
        lblArea.Visible = AreaWiseBilling
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim qry As String = ""
        Dim dt As New DataTable()
        Dim arrMCC As ArrayList = Nothing

        Try
            Dim whrRej As String = ""
            Dim whrRec As String = ""
            Dim whre As String = ""
            Dim AreaName As String = ""
            If AreaWiseBilling Then
                If clsCommon.myLen(fndArea.Value) > 0 Then
                    whre += " And TSPL_MCC_MASTER.Area_Location_Code = '" + fndArea.Value + "' "
                End If
            Else
                If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
                    whrRec += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                    whrRej += "and TSPL_MILK_SRN_HEAD.MCC_Code  IN (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ") "
                Else
                    whrRec += "And TSPL_MILK_SRN_HEAD.mcc_Code in (" & StrPermission & ")"
                    whrRej += "And TSPL_MILK_SRN_HEAD.mcc_Code in (" & StrPermission & ")"
                End If
            End If

            qry = "  select  max(Comp_Name)Comp_Name,'" + objCommonVar.CurrentUser + "' as User_Name, max(Regn_No)Regn_No,max(Phone)Phone, (xx.MCC),max([MCC Name])[MCC Name],sum(xx.SrnQty)SrnQty,sum(xx.SrnAmt)SrnAmt,sum(xx.FATKG)FATKG,sum(xx.SNFKG)SNFKG,
                            case when sum(SrnQty )=0 then 0 else (sum(FATKG )/sum(SrnQty ))*100 end as [FAT(%)],
                            case when sum(SrnQty )=0 then 0 else (sum(SNFKG )/sum(SrnQty ))*100 end as [SNF(%)],
                            sum(xx.SrnQty2)SrnQty2,sum(xx.SrnAmt2)SrnAmt2,sum(xx.FATKG2)FATKG2,sum(xx.SNFKG2)SNFKG2,
                            case when sum(SrnQty2 )=0 then 0 else (sum(FATKG2 )/sum(SrnQty2 ))*100 end as [FAT(%)2],
                            case when sum(SrnQty2 )=0 then 0 else (sum(SNFKG2 )/sum(SrnQty2 ))*100 end as [SNF(%)2],
                            sum(xx.SrnQty3)SrnQty3,sum(xx.SrnAmt3)SrnAmt3,sum(xx.FATKG3)FATKG3,sum(xx.SNFKG3)SNFKG3,
                            case when sum(SrnQty3 )=0 then 0 else (sum(FATKG3 )/sum(SrnQty3 ))*100 end as [FAT(%)3],
                            case when sum(SrnQty3 )=0 then 0 else (sum(SNFKG3 )/sum(SrnQty3 ))*100 end as [SNF(%)3]
                            from(
                            select max(Comp_Name)Comp_Name,max(Regn_No)Regn_No,max(Phone1)Phone, max(x.[MCC Name])[MCC Name],(x.MCC)   , SUM(x.[SRN Qty] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <='" + clsCommon.GetPrintDate(Slot1TD) + "' THEN 1 ELSE 0 END) AS SrnQty,
	                                SUM(x.[SRN Amount] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <='" + clsCommon.GetPrintDate(Slot1TD) + "' THEN 1 ELSE 0 END) AS SrnAmt,
		                            SUM(x.[FAT(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot1TD) + "' THEN 1 ELSE 0 END) AS FATKG,
		                            SUM(x.[SNF(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot1FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot1TD) + "'  THEN 1 ELSE 0 END) AS SNFKG,
		                            SUM(x.[SRN Qty] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS SrnQty2,
		                            SUM(x.[SRN Amount] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS SrnAmt2,
		                            SUM(x.[FAT(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS FATKG2,
		                            SUM(x.[SNF(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot2FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot2TD) + "' THEN 1 ELSE 0 END) AS SNFKG2 ,
       	                            SUM(x.[SRN Qty] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS SrnQty3,
                                    SUM(x.[SRN Amount] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS SrnAmt3,
    	                            SUM(x.[FAT(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >='" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS FATKG3,
    	                            SUM(x.[SNF(KG)] * CASE WHEN CONVERT(date, x.[Doc Date], 103) >= '" + clsCommon.GetPrintDate(Slot3FD) + "' AND CONVERT(date, x.[Doc Date], 103) <= '" + clsCommon.GetPrintDate(Slot3TD) + "' THEN 1 ELSE 0 END) AS SNFKG3 from
                            ( Select Comp_Name,Regn_No,TSPL_COMPANY_MASTER.Phone1, TSPL_MCC_MASTER.MCC_Type as [MCC Type],case when TSPL_MCC_MASTER.is_Mcc=1 then 'MCC' else 'BMCC' end [Chilling Center] ,TSPL_MILK_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc, TSPL_MILK_SRN_DETAIL.EMP_Amount,TSPL_MILK_SRN_DETAIL.TIP_Amount,TSPL_MILK_SRN_DETAIL.Service_Charge_Amount,Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Cow FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Cow SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.FAT_PER Else 0 End [Buffalo FAT(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.SNF_PER Else 0 End [Buffalo SNF(%)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER <= 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Cow Milk Qty (KG)], Case When TSPL_MILK_SRN_DETAIL.FAT_PER > 5 Then TSPL_MILK_SRN_DETAIL.ACC_Qty Else 0 End [Buffalo Milk Qty (KG)]
                            ,  Case When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 0 Then 'M' When Coalesce(TSPL_MILK_SRN_DETAIL.FAT_PER, 0) <= 5.5  Then 'C' Else 'M' End As [Milk Type], TSPL_MILK_SRN_HEAD.DOC_CODE As [Milk Receipt Code],TSPL_MILK_SRN_HEAD.MCC_CODE As MCC,"
            If AreaWiseBilling= True Then
                qry += " tspl_location_master.Location_Desc As [MCC Name],"
            Else
                qry += " TSPL_MCC_MASTER.MCC_NAME As [MCC Name],"

            End If

            qry += "isnull(TSPL_MCC_MASTER.plant_code,'') As [Plant Code], isnull(tspl_location_master.location_desc,'') As [Plant Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date,  Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date], Case When TSPL_MILK_SRN_HEAD.SHIFT = 'M' Then 'Morning' Else 'Evening' End As Shift,  TSPL_MILK_SRN_HEAD.ROUTE_CODE As [Route Code],tspl_mcc_route_master.Supervisor_Name as [SuperVisor Code], TSPL_MCC_ROUTE_MASTER.Route_Name As [Route Name], TSPL_MILK_SRN_HEAD.VEHICLE_CODE As [Vehicle Code], TSPL_MILK_SRN_HEAD.VSP_CODE As [VSP Code], TSPL_VENDOR_MASTER.Vendor_Name As [VSP Name], TSPL_VENDOR_MASTER.Vendor_Group_Code As [Vendor Group Code],TSPL_VENDOR_GROUP.Group_Desc as [Vendor Group Desc] ,TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_HEAD.SAMPLE_NO As [Sample No],  TSPL_MILK_SHIFT_UPLOADER_DETAIL.NO_OF_CANS As [No Of Cans], TSPL_MILK_SRN_DETAIL.qty As [Milk Weight],TSPL_MILK_SRN_DETAIL.UOM_Code, TSPL_MILK_SRN_DETAIL.ACC_Qty As [Milk Weight(KG)], TSPL_MILK_SRN_DETAIL.ACC_Qty_LTR As [Milk Weight(LTR)], TSPL_MILK_SRN_DETAIL.FAT_PER As [FAT(%)], TSPL_MILK_SRN_DETAIL.SNF_PER As [SNF(%)], TSPL_MILK_SRN_DETAIL.CLR,   TSPL_MILK_SRN_DETAIL.FAT_kg As [FAT(KG)], TSPL_MILK_SRN_DETAIL.SNF_kg As [SNF(KG)], Case When TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample = 1 Then 'Auto' Else TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample End As [Sample Status], TSPL_MILK_SRN_HEAD.DOC_CODE As [SRN No], Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As [SRN Amount], TSPL_MILK_SRN_DETAIL.RATE As [SRN Rate], TSPL_MILK_SRN_DETAIL.Qty As [SRN Qty], Case When TSPL_MILK_SRN_HEAD.DOC_CODE Is Null Then 'Open' Else 'Close' End [Shift Status],TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE as Invoice_no, convert(varchar,TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_DATE,103) as Invoice_Date , TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample , '' as MACHINE_NO,(CASE WHEN TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Manual_Sample= 1 THEN 'N' ELSE 'Y' END) AS IS_MILK_SAMPLE_MANUAL,TSPL_MILK_SRN_HEAD.Purchase_Order_No,TSPL_MILK_SRN_DETAIL.Head_Load_Amount ,'' as RejectType,'' as RejectReason,'' as Defaulter   ,TSPL_MILK_PRICE_SNF_DEDUCTION.Amount as SNF_Ded_Value,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE) as decimal(18,2)) as SNF_Ded_Rate,cast((TSPL_MILK_PRICE_SNF_DEDUCTION.Amount+TSPL_MILK_SRN_DETAIL.RATE)*TSPL_MILK_SRN_DETAIL.ACC_Qty as decimal(18,2)) as SNF_Ded_Amount
                             ,  TSPL_MILK_SRN_DETAIL.Price_code,[Transporter Code], [Transporter Name], isnull(TSPL_MILK_PURCHASE_INVOICE_DETAIL.Handling_Charges_Amount, 0) As Handling_Charges_Amount   , (isnull(TSPL_MILK_SRN_DETAIL.VSP_Commission_Apply, 0) * TSPL_MILK_SRN_DETAIL.VSP_Commission_Amount)  As VSP_Commission_Amount, (isnull(TSPL_MILK_SRN_DETAIL.VSP_Deduction_Apply, 0) * TSPL_MILK_SRN_DETAIL.VSP_Deduction_Amount)  As VSP_Deduction_Amount, TSPL_MILK_SRN_DETAIL.VSP_Day_Wise_Incentive,case when isnull( TSPL_MILK_SRN_DETAIL.Sub_Standard, 0) = 1 then 'Sub Standard' else '' end as SubStandard,TSPL_Primary_Vehicle_Master.Vehicle,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code] 
                             From TSPL_MILK_SRN_DETAIL 
                             Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE 
                             left outer join TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code = TSPL_MILK_SRN_HEAD.Comp_Code
							 left outer join TSPL_MILK_SHIFT_UPLOADER_DETAIL on TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Shift_Uploader_TR_No
							 left outer join TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL on TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.TR_No = TSPL_MILK_SRN_HEAD.Against_Uploader_TR_No                 
                             Left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_MILK_SRN_DETAIL.item_code 
                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE 
                             Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE 
                             Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
                             Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_SRN_HEAD.VSP_CODE
                             Left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_MASTER.Vendor_Group_Code = TSPL_VENDOR_GROUP.Ven_Group_Code 
                             Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_SRN_HEAD.ROUTE_CODE
                             Left Join(select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code], tspl_vendor_master.vendor_name As [Transporter Name], TSPL_Primary_Vehicle_Master.mcc_code, TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code And tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                             Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code 
                             Left outer join TSPL_MILK_PRICE_SNF_DEDUCTION On TSPL_MILK_PRICE_SNF_DEDUCTION.Price_code=TSPL_MILK_SRN_DETAIL.Price_code And cast(TSPL_MILK_SRN_DETAIL.SNF_PER As Decimal(18, 1))=TSPL_MILK_PRICE_SNF_DEDUCTION.Per"
            If AreaWiseBilling = True Then
                qry += " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Area_Location_Code"
            Else
                qry += " left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.Plant_Code"
            End If

            qry += " where 2 = 2  and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >=convert(date,'" + clsCommon.GetPrintDate(Slot1FD) + "',103) and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <=convert(date,'" + clsCommon.GetPrintDate(Slot3TD) + "',103) " + whrRec + whre + "
 
                             )x group by x.MCC
                             )xx group by xx.MCC   "

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptMilkPaymentSummary", "")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            If gv.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If
                '

                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Milk Payment Summary Report", gv, arrHeader, "Milk Payment Summary Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Excel_Click(sender As Object, e As EventArgs) Handles Excel.Click
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "Admin") = CompairStringResult.Equal Then
                    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

                Else
                    'Dim strLocDesc As String = clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_master where Location_Code in (" + objCommonVar.strCurrUserLocations + ")")
                    'arrHeader.Add("Location : " + strLocDesc)
                End If


                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Milk Payment Summary Report", gv, arrHeader, Me.Text)
                ' transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
                'Process.Start(filePath)

            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndArea__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndArea._MYValidating
        Try
            Dim sQuery As String = " Select TSPL_LOCATION_MASTER.Location_Code as Code ,  TSPL_LOCATION_MASTER.Location_Desc, Type from TSPL_LOCATION_MASTER "
            fndArea.Value = clsCommon.ShowSelectForm("Location@Plant@Master", sQuery, "Code", "TSPL_LOCATION_MASTER.Type <> 'PLANT' OR TSPL_LOCATION_MASTER.Location_Category <> 'Mcc'", fndArea.Value, "Code", isButtonClicked)

            Dim arrMCCMapped As New ArrayList
            Dim dt As New DataTable
            Dim query As String = "select MCC_NAME from TSPL_MCC_MASTER  WHERE Area_Location_Code='" + fndArea.Value + "'"
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(query)

            For i As Integer = 0 To dt.Rows.Count - 1
                arrMCCMapped.Add(dt.Rows(i)("MCC_NAME"))
            Next
            txtMCC.arrValueMember = arrMCCMapped

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub
End Class