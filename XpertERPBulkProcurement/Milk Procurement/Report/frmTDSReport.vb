Imports System.Data.SqlClient
Imports System.IO
Imports common

Public Class frmTDSReport

    Private Sub frmTDSReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtFromDate.Value.AddMonths(1)
        Reset()
    End Sub
    Private Sub txtMultMCC__My_Click(sender As Object, e As EventArgs) Handles txtMultMCC._My_Click
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select tspl_mcc_master.MCC_Code as [Code] ,tspl_mcc_master.MCC_Type as [Mcc Type] ,tspl_mcc_master.MCC_NAME as [Mcc Name] ,tspl_mcc_master.Chilling_Vendor as [Chilling Vendor] ,tspl_mcc_master.Add1 as [Address1] ,tspl_mcc_master.Add2 as [Address2] ,tspl_mcc_master.Tehsil as [Tehsil] ,tspl_mcc_master.City_code as [City Code] ,tspl_mcc_master.State_Code as [State Code] ,tspl_mcc_master.Country_code as [Country Code] ,tspl_mcc_master.Pin_code as [Pin Code],tspl_mcc_master.Pan_No as [Pan No] ,tspl_mcc_master.Telphone as [Telphone] ,tspl_mcc_master.Email as [Email] ,tspl_mcc_master.Fax as [Fax] ,tspl_mcc_master.MCC_Area as [Mcc Area] ,tspl_mcc_master.Area_Of_Store as [Area Of Store] ,tspl_mcc_master.Area_Of_Office as [Area Of Office] ,tspl_mcc_master.Open_Area_For_tanker as [Open Area For Tanker] ,tspl_mcc_master.Area_Of_LAB as [Area Of Lab] ,tspl_mcc_master.No_Of_SILO as [No Of Silo] ,tspl_mcc_master.Total_Storage_capacity as [Total Storage Capacity] ,tspl_mcc_master.Area_Of_Receiving_DOCK as [Area Of Receiving Dock] ,tspl_mcc_master.No_Of_Chiller as [No Of Chiller] ,tspl_mcc_master.Chiller_Brand_Name as [Chiller Brand Name] ,tspl_mcc_master.Chiller_Capacity as [Chiller Capacity] ,tspl_mcc_master.No_Of_MilkPump as [No Of Milkpump] ,tspl_mcc_master.MilkPump_Capacity as [Milkpump Capacity] ,tspl_mcc_master.DripSaver as [Drip Saver] ,tspl_mcc_master.CanWasher as [Can Washer] ,tspl_mcc_master.CanScrubber as [Can Scrubber] ,tspl_mcc_master.FSSAI_NO as [FSSAI No] ,tspl_mcc_master.ETP as [ETP] ,tspl_mcc_master.Earthing as [Earthing] ,tspl_mcc_master.Coil_Length as [Coil Length] ,tspl_mcc_master.Electricity_Connection as [Electricity Connection] ,tspl_mcc_master.Boiler as [Boiler] ,tspl_mcc_master.NoOfDG as [No. of DG] ,tspl_mcc_master.NoOfCompressor as [No. of Compressor] ,tspl_mcc_master.PayeeName as [Payee Name] ,tspl_mcc_master.BankName as [Bank Name] ,tspl_mcc_master.BankBranch as [Bank Branch] ,tspl_mcc_master.BankCityCode as [Bank City Code] ,tspl_mcc_master.BankStateCode as [Bank State Code] ,tspl_mcc_master.IFCICode as [IFCI Code] ,tspl_mcc_master.AccountNO as [Account No] ,tspl_mcc_master.Created_By as [Created By] ,tspl_mcc_master.Created_Date as [Created Date] ,tspl_mcc_master.Modified_By as [Modified By] ,tspl_mcc_master.Modified_Date as [Modified Date] ,tspl_mcc_master.Comp_Code as [Company Code],tspl_mcc_master.mcc_code_vlc_uploader as [MCC Code For VLC Uploder],tspl_mcc_master.Plant_Code AS [Plant Code],TSPL_LOCATION_MASTER_PLANT.Location_Desc AS [Plant Name] from tspl_mcc_master LEFT JOIN TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_PLANT ON TSPL_LOCATION_MASTER_PLANT.Location_Code=tspl_mcc_master.Plant_Code  inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "
        txtMultMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCCMulSelect", qry, "Code", "Code", txtMultMCC.arrValueMember, txtMultMCC.arrDispalyMember)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub Reset()
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        txtMultMCC.Enabled = True
        txtMultDCS.Enabled = True
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.MasterView.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub Disable()
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
        txtMultMCC.Enabled = False
        txtMultDCS.Enabled = False
    End Sub

    Private Sub txtMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtMultDCS._My_Click
        Dim qry As String = " select M.Vendor_Code AS [Code], m.Vendor_Name as [Name],ISNULL(m.alies_name,'') As [Alies Name],TSPL_VLC_MASTER_HEAD.VLC_CODE_VLC_Uploader as [VLC Uploader Code], TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_MCC_MASTER.MCC_Name as [MCC Name],TSPL_MCC_MASTER.Plant_Code as [Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [Plant Name],(m.Add1+(case when m.Add2='' then '' else ',' end)+m.Add2) as [Address],m.Vendor_Group_Code as [Vendor Group Code],m.Vendor_Group_Code_Desc as [Vendor Group Desc],s.Acct_Set_Code as [Vendor Account Set],s.Acct_Set_Desc as [Vendor Account Set Desc] from TSPL_VENDOR_MASTER m join TSPL_VENDOR_ACCOUNT_SET s on m.Vendor_Account =s.Acct_Set_Code " &
                             " left outer Join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = M.Vendor_Code " &
                             " Left Outer Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_VLC_MASTER_HEAD.MCC " &
                             " Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_MCC_MASTER.Plant_Code where m.Status='N' "
        txtMultDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VSPMulSelect", qry, "VLC Uploader Code", "Name", txtMultDCS.arrValueMember, txtMultDCS.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
                Throw New Exception("To Date can't be greater than From date")
            End If
            Dim dt As DataTable = PrintData()
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt
                SetGridFormat()
                Disable()
                dt = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        RadPageView1.SelectedPage = RadPageViewPage2
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.Columns("From Date").IsVisible = False
        Gv1.Columns("To Date").IsVisible = False
        Gv1.Columns("Location").IsVisible = False
        Gv1.Columns("Comp_Name").IsVisible = False
        Gv1.Columns("Regn_No").IsVisible = False
        Gv1.Columns("Comp Contact No").IsVisible = False

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item As New GridViewSummaryItem
        If Gv1.Rows IsNot Nothing AndAlso Gv1.Columns.Count > 0 Then
            For ii As Integer = 0 To Gv1.Columns.Count - 1
                If ii > 5 Then
                    item = New GridViewSummaryItem(Gv1.Columns(ii).HeaderText, "{0:n2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                End If
            Next
        End If
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Function PrintData() As DataTable
        Dim Qry As String = "select ROW_NUMBER() Over (Order By  cast(VLC_CODE_Uploader as int)) As [SNo.],'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' As [From Date],'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' As [To Date], "
        If txtMultMCC.arrValueMember IsNot Nothing AndAlso txtMultMCC.arrValueMember.Count = 1 Then
            Qry += " TSPL_MCC_MASTER.MCC_NAME as [Location], "
        Else
            Qry += " '" + clsCommon.myCstr(objCommonVar.CurrComp_Code1) + "' as [Location], "
        End If
        Qry += "cast(VLC_CODE_Uploader as int) As [DCS Code], VSP_NAME As [Society Name],Milk_Qty As [Milk Quantity],
                    (Milk_Amount+Credit_Note_Amount) As [Milk Amount + OverHead],Isnull(TSPL_REMITTANCE.Actual_TDS_Base,0)+ Isnull(TSPL_PAYMENT_PROCESS_DETAIL.Credit_Note_Amount,0) as [Actual Amount], Isnull(TDS_Amount,0) As [TDS Amount],
                    TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Regn_No,Case When TSPL_COMPANY_MASTER.Phone1 Is Null Then TSPL_COMPANY_MASTER.Phone2 Else TSPL_COMPANY_MASTER.Phone1 End As [Comp Contact No]
                    from TSPL_PAYMENT_PROCESS_DETAIL 
                    left outer join TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
                    Left Outer join TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected
					left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_DETAIL.AP_Invoice_No
					left outer join TSPL_REMITTANCE on TSPL_REMITTANCE.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No
                    left outer join (select DOC_CODE,cast( sum(FATKg) as decimal(18,3)) as FATKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(FATKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as FATPer ,cast( sum(SNFKg) as decimal(18,3)) as SNFKg,cast(case when sum(ACC_Qty)=0 then 0 else sum(SNFKg)*100/sum(ACC_Qty) end as decimal(18,2) ) as SNFPer  from (select DOC_CODE, ACC_Qty,FAT_PER,SNF_PER,cast(ACC_Qty*FAT_PER/100 as decimal(18,2)) as FATKg,cast( ACC_Qty*SNF_PER/100 as decimal(18,2)) as SNFKg from TSPL_MILK_PURCHASE_INVOICE_DETAIL )xx group by DOC_CODE ) as TabFATSNFDetail on TabFATSNFDetail.DOC_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Milk_Purchase_Invoice_No
                    Left Outer Join TSPL_COMPANY_MASTER On TSPL_COMPANY_MASTER.Comp_Code1=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code
                    Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code=TSPL_COMPANY_MASTER.Comp_Code1
                    where Isnull(TDS_Amount,0)>0 And  TSPL_PAYMENT_PROCESS_HEAD.From_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and TSPL_PAYMENT_PROCESS_HEAD.From_Date<='" + clsCommon.GetPrintDate(txtToDate.Value) + "'"

        If txtMultMCC.arrValueMember IsNot Nothing AndAlso txtMultMCC.arrValueMember.Count > 0 Then
            Qry += "  and TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected in (" + clsCommon.GetMulcallString(txtMultMCC.arrValueMember) + ")"
        End If
        If txtMultDCS.arrValueMember IsNot Nothing AndAlso txtMultDCS.arrValueMember.Count > 0 Then
            Qry += "  and TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader in (" + clsCommon.GetMulcallString(txtMultDCS.arrValueMember) + ")"
        End If
        Qry += "order by [DCS Code]"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        Return dt
    End Function
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim Qry As String = Nothing
            If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
                Throw New Exception("To Date can't be greater than From date")
            End If

            Dim dt As DataTable = PrintData()
            If dt.Rows IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "crptTDSReport", "TDS Report")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found To Print", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validated(sender As Object, e As EventArgs) Handles txtFromDate.Validated
        Try
            If clsCommon.myLen(txtFromDate.Value) > 0 Then
                txtToDate.Value = txtFromDate.Value.AddMonths(1)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class