Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===========Created by Sanjay==================
Public Class rptVLCTransportExpense
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim arrRange As New Dictionary(Of String, clsDateRange)
    Dim arr As New Dictionary(Of Integer, DataRow)
    Dim obj As clsDateRange
    Dim Key As String

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
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


    Sub LoadRoute()
        Dim qry As String = Nothing
        qry = "select TSPL_MCC_ROUTE_MASTER.Route_Code as [Code] ,TSPL_MCC_ROUTE_MASTER.Route_Name as [Name]  from TSPL_MCC_ROUTE_MASTER "
        cbgRoute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgRoute.ValueMember = "Code"
        cbgRoute.DisplayMember = "Name"

    End Sub
    Sub LoadTransporter()
        Dim qry As String = Nothing
        qry = "select TSPL_Primary_Vehicle_Master.Vendor_Code as [Code],max(TSPL_VENDOR_MASTER.Vendor_Name) as [Name] from   TSPL_Primary_Vehicle_Master "
        qry += " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code and Form_Type='PTM' group by TSPL_Primary_Vehicle_Master.Vendor_Code" ''do grouping because multiple data is coming
        cbgTransporter.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTransporter.ValueMember = "Code"
        cbgTransporter.DisplayMember = "Name"
    End Sub
    '===================================================================


    Sub Reset()
        Try

            RadPageView1.SelectedPage = RadPageViewPage1
            If chkMCCAll.IsChecked Then
                cbgMCC.CheckedAll()
            Else
                cbgMCC.UnCheckedAll()
            End If
            LoadMCC()
            LoadRoute()
            LoadTransporter()
            chkMCCAll.CheckState = CheckState.Checked
            chkAllRoute.CheckState = CheckState.Checked
            chkAllTransporter.CheckState = CheckState.Checked
            'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
            EnableDisableControl(True)


            gv.DataSource = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        RadGroupBox2.Enabled = val
        RadGroupBox3.Enabled = val
        RadGroupBox4.Enabled = val
    End Sub

    Private Sub LoadData()
        Try
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()

            If clsCommon.GetDateWithStartTime(txtFromDate.Value) > clsCommon.GetDateWithStartTime(txtToDate.Value) Then
                txtFromDate.Focus()
                Throw New Exception("From date can not be greater then to Date")
            End If

            If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single MCC or select all.")
            End If
            If ChkSelectRoute.IsChecked AndAlso cbgRoute.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single Route or select all.")
            End If
            If ChkSelectTransporter.IsChecked AndAlso cbgTransporter.CheckedValue.Count = 0 Then
                Throw New Exception("Please select atleast single Transporter or select all.")
            End If

            Dim fromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim Todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")

            Dim finlaQry As String = ""
            Dim BaseQry As String = "select max(ProvisionNo) as ProvisionNo,Doc_Date,max(SHIFT) as SHIFT,  case when max(Avg_Km_Ltr )>0 then   cast(ROUND(((max(Stand_Per_Day_km)/max(Avg_Km_Ltr ))*max(DieselRate)) , 2,1) as decimal(18,2))  else 0 end  as [HSD Amount],case when max(isnull(ProvisionFixedAmount,0))>0 then max(isnull(ProvisionFixedAmount,0)) else cast(ROUND((max(Rental_Month)/ (max(No_Days_In_month)*2)), 2,1) as decimal(18,2)) end as [Fixed Amount],max(DieselRate) as DieselRate,MILK_Shift_End_doc_code,sum([FAT(LTR)]) as [FAT(LTR)],sum([SNF(LTR)]) as [SNF(LTR)],((case when max(Avg_Km_Ltr )>0 then   cast(ROUND(((max(Stand_Per_Day_km)/max(Avg_Km_Ltr ))*max(DieselRate)) , 2,1) as decimal(18,2))  else 0 end)
+(case when max(isnull(ProvisionFixedAmount,0))>0 then max(isnull(ProvisionFixedAmount,0)) else cast(ROUND((max(Rental_Month)/ (max(No_Days_In_month)*2)), 2,1) as decimal(18,2)) end)) as [Total Amount], transporter_code,max(transporter_Name ) as Transp_Name,MCC_Code,max(MCC_NAME) as MCC_NAME  ,ROUTE_CODE,max(Route_Name)  as Route_Name,max(Supervisor_Name) as Supervisor_Name,max(KiloMeter) as KiloMeter, max(Pan)as Pan_No,max(len(PAN ))as Len_Pan_no,Transp_vehicleNo,sum(qty) as qty,sum(Std_Qty) as Std_Qty,max(Self_Route) as Self_Route, --BSP
sum(isnull(qty_kg,0)) as qty_kg,count(distinct Total_VLC )as Total_VLC ,max(Stand_Per_Day_km ) as Stand_Per_Day_km,count(distinct Stand_Total_Shift ) as Stand_Total_Shift,max(Stand_Per_Day_km )*Count(distinct Stand_Total_Shift ) as Standard_Total_Km, max (Temp_Rental_Amount) as Temp_Rental_Amount   ,  max(Avg_Km_Ltr ) as mileage_Km_Ltr
, max(Diesel_Rate) as Diesel_Rate,max(Rental_Month ) as Rental_Month,Shift_Charges,max(No_Days_In_month ) as No_Days_In_month ,sum(countVehicle ) as Vehicle
,max(countVehicle ) as countVehicle,VehicleNo,max(ProvisionFixedAmount) as ProvisionFixedAmount from ( 

select TSPL_PROVISION_ENTRY.Doc_No as ProvisionNo,convert(date,TSPL_MILK_RECEIPT_head.DOC_DATE,103) as Doc_Date,TSPL_MILK_RECEIPT_HEAD.SHIFT,TSPL_Primary_Vehicle_Master.Vendor_Code as transporter_code,TSPL_VENDOR_MASTER.Vendor_Name as transporter_Name,TSPL_MILK_Shift_End_HEAD.MCC_Code,TSPL_MCC_MASTER.MCC_NAME ,TSPL_MCC_ROUTE_MASTER.ROUTE_CODE ,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_EMPLOYEE_MASTER.Emp_Name as Supervisor_Name,TSPL_MCC_ROUTE_MASTER.KiloMeter,  TSPL_VENDOR_MASTER.PAN  ,isnull(TSPL_Primary_Vehicle_Master.Shift_Charges,0) as Shift_Charges,TSPL_Primary_Vehicle_Master.Vehicle_Code as Transp_vehicleNo, isnull(TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT,0) as Qty
,TSPL_MILK_SRN_DETAIL.Std_Qty,isnull(TSPL_MCC_ROUTE_MASTER.Self_Route,0) as Self_Route,isnull(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT,0) as qty_kg, TSPL_MILK_RECEIPT_DETAIL.VLC_CODE   as Total_VLC,  case when TSPL_MILK_Shift_End_Route_DETAIL.Actual_KM>0 then TSPL_MILK_Shift_End_Route_DETAIL.Actual_KM else isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) end   as Stand_Per_Day_km ,isnull (TSPL_Primary_Vehicle_Master.Rental_Amount,0) as Temp_Rental_Amount,TSPL_MILK_RECEIPT_HEAD.shift as Stand_Total_Shift,isnull(TSPL_MILK_Shift_End_Route_DETAIL.Total_KM,0) as Total_KM ,(case when isnull(TSPL_MILK_Shift_End_Route_DETAIL.AvgKmLtr,0)>0 then isnull(TSPL_MILK_Shift_End_Route_DETAIL.AvgKmLtr,0) else isnull(TSPL_Primary_Vehicle_Master.Avg_Km_Ltr,0) end) as Avg_Km_Ltr, isnull(TSPL_Primary_Vehicle_Master.Diesel_Rate,0) as Diesel_Rate,isnull(TSPL_PROVISION_ENTRY.FixedAmount,0) as ProvisionFixedAmount ,case when isnull(TSPL_PROVISION_ENTRY.FixedCharge,0)>0 then TSPL_PROVISION_ENTRY.FixedCharge else isnull((case when TSPL_Primary_Vehicle_Master.Status='Rental/Diesel' then  TSPL_Primary_Vehicle_Master.Rental_Amount  when TSPL_Primary_Vehicle_Master.Status='Rental' then  (case when TSPL_Primary_Vehicle_Master.Rental_Type='Day' then TSPL_Primary_Vehicle_Master.Rental_Amount*DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,TSPL_MILK_RECEIPT_HEAD.DOC_DATE),0)))  when TSPL_Primary_Vehicle_Master.Rental_Type='Month' then TSPL_Primary_Vehicle_Master.Rental_Amount  when TSPL_Primary_Vehicle_Master.Rental_Type='Year' then TSPL_Primary_Vehicle_Master.Rental_Amount/12.00 else 0 end )  end),0) end as Rental_Month,DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,TSPL_MILK_RECEIPT_HEAD.DOC_DATE),0))) as No_Days_In_month,TSPL_Primary_Vehicle_Master.Rate_Type as [Rate/Ltr],TSPL_Primary_Vehicle_Master.Price_Ltr_KG as [Ltr Amount],TSPL_Primary_Vehicle_Master.Price_KM as [Rate/Km]
,(select distinct case when coalesce(VEHICLE_CODE,'')='' then 0 else 1 end as Vehicle from TSPL_MILK_RECEIPT_DETAIL inner join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE where ( Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + fromDate + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='" + Todate + "') and VEHICLE_CODE=TSPL_Primary_Vehicle_Master.Vehicle_Code)  as countVehicle,TSPL_Primary_Vehicle_Master.Vehicle as VehicleNo 
, Convert(decimal(18,2), ROUND(TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR / 100,2,1)) As [FAT(LTR)], Convert(decimal(18,2),ROUND(TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR / 100,2,1)) As [SNF(LTR)]
,isnull(TSPL_MILK_Shift_End_Route_DETAIL.amount,0) as [Total Amount],TSPL_MILK_Shift_End_Route_DETAIL.doc_code as MILK_Shift_End_doc_code,TSPL_MILK_Shift_End_Route_DETAIL.DieselRate from
( 

select TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data.Vendor_Code,  TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data.Hist_Version as Hist_Version, Rental_Amount,Vehicle_Code, MCC_Code, Shift_Charges, Avg_Km_Ltr, Diesel_Rate, Status, Rental_Type , Rate_Type, Price_Ltr_KG, Price_KM , Vehicle from 
(select Vendor_Code,  TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data.Hist_Version as Hist_Version, Rental_Amount,Vehicle_Code, MCC_Code, Shift_Charges, Avg_Km_Ltr, Diesel_Rate, Status, Rental_Type , Rate_Type, Price_Ltr_KG, Price_KM , Vehicle from TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data where cast ( Hist_On as datetime) <= convert (datetime,'" + Todate + "') 
) as TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data 
inner join (select TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data.Vehicle_Code as vc , max(TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data.Hist_Version) as Hist_Version from TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data where cast ( Hist_On as datetime) <= convert (datetime,'" + Todate + "') group by Vehicle_Code) as VersionMax  on VersionMax.vc = TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data.Vehicle_Code  and VersionMax.Hist_Version = TSPL_PRIMARY_VEHICLE_MASTER_Hist_Data.Hist_Version

) as TSPL_Primary_Vehicle_Master  
left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code and Form_Type='PTM'  left outer join TSPL_Vendor_Bank_MASTER on TSPL_Vendor_Bank_MASTER.bank_code=tspl_vendor_master.bank_code 
left join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE =TSPL_Primary_Vehicle_Master.Vehicle_Code  and TSPL_Primary_Vehicle_Master.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code 
left join TSPL_MILK_RECEIPT_head on TSPL_MILK_RECEIPT_head.DOC_CODE  =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE
Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE
Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE=TSPL_MILK_SAMPLE_DETAIL.DOC_CODE	and TSPL_MILK_SRN_HEAD.SAMPLE_NO=TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO
left outer join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_DETAIL.DOC_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE--BSP
Left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE=TSPL_MILK_RECEIPT_head.MCC_CODE and  TSPL_MILK_Shift_End_HEAD.shift=TSPL_MILK_RECEIPT_head.shift and convert(date,TSPL_MILK_Shift_End_HEAD.doc_date,103)=convert(date,TSPL_MILK_RECEIPT_head.doc_date,103)  
left join TSPL_MILK_Shift_End_Route_DETAIL on TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE =TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE  and TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE=TSPL_MILK_Shift_End_HEAD.DOC_CODE   
left outer join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Prog_Code='M-Shift_End' and TSPL_PROVISION_ENTRY.Ref_Doc_No=TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE and TSPL_PROVISION_ENTRY.Route_Code=TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE
left join TSPL_MCC_MASTER on tspl_mcc_master.MCC_Code =TSPL_MILK_Shift_End_HEAD.MCC_Code  
left join TSPL_MCC_ROUTE_MASTER  on  TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE 
left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.emp_code=TSPL_MCC_ROUTE_MASTER.Supervisor_Name
where Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) >='" + fromDate + "' and Cast(TSPL_MILK_RECEIPT_HEAD.DOC_DATE as Date) <='" + Todate + "' "
            If Not clsCommon.CompairString(txtFromShift.SelectedValue, "B") = CompairStringResult.Equal Then
                BaseQry += " and TSPL_MILK_RECEIPT_HEAD.SHIFT='" + clsCommon.myCstr(txtFromShift.SelectedValue) + "'"
            End If
            BaseQry += ")xx  where 2=2    "
            If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count > 0 Then
                BaseQry += " and MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            Else
                BaseQry += " and mcc_code in (select location_code from tspl_location_master where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' "
                If clsCommon.myLen(arrLoc) > 0 Then
                    BaseQry += " and Location_Code in (" + arrLoc + ") "
                End If
                BaseQry += " ) "
            End If
            If ChkSelectRoute.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
                BaseQry += " and route_code  IN (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
            End If
            If ChkSelectTransporter.IsChecked AndAlso cbgTransporter.CheckedValue.Count > 0 Then
                BaseQry += " and transporter_code  IN (" + clsCommon.GetMulcallString(cbgTransporter.CheckedValue) + ") "
            End If
            BaseQry += "Group by transporter_code ,MCC_Code ,ROUTE_CODE ,Transp_vehicleNo,Shift_Charges,vehicleNo,MILK_Shift_End_doc_code,Doc_Date"

            If clsCommon.CompairString(cboType.SelectedValue, "Detail") = CompairStringResult.Equal Then
                finlaQry = " select ROW_NUMBER() OVER( ORDER BY xx.MCC_Code,xx.Transp_vehicleNo) AS SNo,xx.MCC_Code as [Center Code],xx.MCC_NAME as [Center Name]
,xx.Transp_vehicleNo as [Vehicle No], xx.Route_Code as [Route Code],xx.Route_Name as [Route Name],xx.transporter_code as [Transporter Code]
,xx.Transp_Name as [Transporter Name],xx.Pan_No  as [PAN No],convert(decimal(18,1), (sum(convert (decimal(18,2),xx.countVehicle))/2)) as [Days], max(xx.Rental_Month) as [Fixed Charge], cast(sum(xx.qty) as decimal(18,2)) as [Milk Qty],cast(sum(xx.Std_Qty) as decimal(18,2)) as [Std_Qty],max(Self_Route) as Self_Route
,sum(xx.[FAT(LTR)]) as [FAT Qty],sum(xx.[SNF(LTR)]) as [SNF Qty], sum(xx.Stand_Per_Day_km) as [KM],cast(ROUND(sum(case when (xx.mileage_Km_Ltr)>0  then isnull(xx.Stand_Per_Day_km/xx.mileage_Km_Ltr,0) else 0 end), 2,1) as decimal(18,2)) as [HSD In Ltr],max(xx.mileage_Km_Ltr) as [Avg],case when max(DieselRate)>0 then max(DieselRate) else (case when sum(case when (xx.mileage_Km_Ltr)>0 and (xx.Stand_Per_Day_km)>0 then isnull(xx.Stand_Per_Day_km/xx.mileage_Km_Ltr,0) else 0 end)>0 then cast(ROUND(sum([HSD Amount])/sum(case when (xx.mileage_Km_Ltr)>0 and (xx.Stand_Per_Day_km)>0 then isnull(xx.Stand_Per_Day_km/xx.mileage_Km_Ltr,0) else 0 end), 2,1) as decimal(18,2)) else 0 end) end as [Rate Avg],sum([HSD Amount]) as [HSD Amount],sum([Fixed Amount]) as [Fixed Amount],cast(sum(xx.[Total Amount]) as decimal(18,2)) as [Total Amount],max(xx.VehicleNo) as [Vehicle] from ( " + Environment.NewLine +
    BaseQry + Environment.NewLine +
    ")xx  group by Transp_vehicleNo,xx.MCC_Code,xx.MCC_NAME,xx.Transp_vehicleNo, xx.Route_Code,xx.Route_Name,xx.transporter_code,xx.Transp_Name,xx.Pan_No"
                dt = clsDBFuncationality.GetDataTable(finlaQry)
            ElseIf clsCommon.CompairString(cboType.SelectedValue, "MCC Wise Summary") = CompairStringResult.Equal Then
                finlaQry = " select xx.MCC_Code as [Center Code],max(xx.MCC_NAME) as [Center Name]
 ,cast(sum(xx.qty) as decimal(18,2)) as [Milk Qty],cast(sum(xx.Std_Qty) as decimal(18,2)) as [Std Qty]
 ,cast(sum(xx.qty * Self_Route ) as decimal(18,2)) as [Milk Qty (Self)],cast(sum(xx.Std_Qty * Self_Route) as decimal(18,2)) as [Std. Milk (Self)]
 ,sum([HSD Amount]) as [HSD Amount]
 ,sum([Fixed Amount]) as [Fixed Amount],cast(sum(xx.[Total Amount]) as decimal(18,2)) as [Total Amount]
 ,case when cast(sum(xx.Std_Qty) as decimal(18,2)) >0 then cast(sum([HSD Amount])/cast(sum(xx.Std_Qty) as decimal(18,2)) as decimal(18,2))  else 0 end as [TC HSD]
 ,case when cast(sum(xx.Std_Qty) as decimal(18,2)) >0 then cast(sum([Fixed Amount])/cast(sum(xx.Std_Qty) as decimal(18,2)) as decimal(18,2))  else 0 end as [TC FIXED]
 ,case when cast(sum(xx.Std_Qty) as decimal(18,2)) >0 then cast(sum([Total Amount])/cast(sum(xx.Std_Qty) as decimal(18,2)) as decimal(18,2))  else 0 end as [TC Total]
 ,count(distinct xx.Transp_vehicleNo) as [No Of Vehicle] from (" + Environment.NewLine +
    BaseQry + Environment.NewLine +
    ")xx  group by  xx.MCC_Code "
                dt = clsDBFuncationality.GetDataTable(finlaQry)
            ElseIf clsCommon.CompairString(cboType.SelectedValue, "MCC,Supervisor,Route and Date wise") = CompairStringResult.Equal Then
                Dim FD As Date = clsCommon.GetDateWithStartTime(txtFromDate.Value)
                Dim TD As Date = clsCommon.GetDateWithStartTime(txtToDate.Value)
                arrRange = New Dictionary(Of String, clsDateRange)
                While FD <= TD
                    obj = New clsDateRange
                    obj.FromDate = FD
                    obj.ToDate = FD

                    arrRange.Add(clsCommon.GetPrintDate(FD, "dd.MM.yyyy"), obj)
                    FD = FD.AddDays(1)
                End While

                finlaQry = "select xx.MCC_Code as [Center Code],max(xx.MCC_NAME) as [Center Name]
 ,Route_Code as [Route Code],max(Route_Name) as [Route],max(Supervisor_Name) as Supervisor_Name,max(KiloMeter) as [Route Distance]"
                For ii As Integer = 0 To arrRange.Count - 1
                    Key = arrRange.Keys(ii)
                    obj = arrRange(Key)

                    finlaQry += ",cast(sum(xx.qty * case when Doc_Date='" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "' then 1 else 0 end ) as decimal(18,2)) as [" + Key + " Milk Qty]
,cast(sum(xx.Std_Qty * case when Doc_Date='" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "' then 1 else 0 end) as decimal(18,2)) as [" + Key + " Std Qty]
,cast(sum(xx.[Total Amount] * case when Doc_Date='" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "' then 1 else 0 end) as decimal(18,2)) as [" + Key + " Hide Total Amount]
 ,case when cast(sum(xx.Std_Qty* case when Doc_Date='" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "' then 1 else 0 end) as decimal(18,2)) >0 then cast(sum([Total Amount] * case when Doc_Date='" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "' then 1 else 0 end)/cast(sum(xx.Std_Qty * case when Doc_Date='" + clsCommon.GetPrintDate(obj.FromDate, "dd/MMM/yyyy") + "' then 1 else 0 end ) as decimal(18,2)) as decimal(18,2))  else 0 end as [" + Key + " TC] "
                Next

                finlaQry += " from (" + Environment.NewLine +
  BaseQry + Environment.NewLine +
    ")xx  group by  xx.MCC_Code,xx.Route_Code "
                dt = clsDBFuncationality.GetDataTable(finlaQry)
                AddTotalRowsDayWiseTrend()
            End If




            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            End If
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()


            RadPageView1.SelectedPage = RadPageViewPage2
            gv.BestFitColumns()
            If Not clsCommon.CompairString(cboType.SelectedValue, "MCC,Supervisor,Route and Date wise") = CompairStringResult.Equal Then
                ReStoreGridLayout()
            End If
            EnableDisableControl(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub AddTotalRowsDayWiseTrend()
        arr = New Dictionary(Of Integer, DataRow)
        Dim Total As Decimal = 0
        Dim TotalQty As Decimal = 0
        Dim strPrevious As String = clsCommon.myCstr(dt.Rows(0)("Center Code"))
        Dim drTotal As DataRow = dt.NewRow()
        For jj As Integer = 0 To arrRange.Count - 1
            drTotal(arrRange.Keys(jj) + " Milk Qty") = 0
            drTotal(arrRange.Keys(jj) + " Std Qty") = 0
            drTotal(arrRange.Keys(jj) + " Hide Total Amount") = 0
            drTotal(arrRange.Keys(jj) + " TC") = 0
        Next


        For ii As Integer = 0 To dt.Rows.Count - 1
            Dim flag As Boolean = False
            If clsCommon.CompairString(strPrevious, clsCommon.myCstr(dt.Rows(ii)("Center Code"))) = CompairStringResult.Equal Then
                For jj As Integer = 0 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " Milk Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Milk Qty")
                    drTotal(arrRange.Keys(jj) + " Std Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Std Qty")
                    drTotal(arrRange.Keys(jj) + " Hide Total Amount") += dt.Rows(ii)(arrRange.Keys(jj) + " Hide Total Amount")
                    If clsCommon.myCDecimal(drTotal(arrRange.Keys(jj) + " Std Qty")) > 0 Then
                        drTotal(arrRange.Keys(jj) + " TC") = Math.Round(clsCommon.myCDecimal(drTotal(arrRange.Keys(jj) + " Hide Total Amount")) / clsCommon.myCDecimal(drTotal(arrRange.Keys(jj) + " Std Qty")), 2, MidpointRounding.ToEven)
                    End If
                Next
            Else
                'drTotal("Center Code") = strPrevious
                drTotal("Center Name") = "Total"
                arr.Add(ii, drTotal)

                drTotal = dt.NewRow()
                For jj As Integer = 0 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " Milk Qty") = 0
                    drTotal(arrRange.Keys(jj) + " Std Qty") = 0
                    drTotal(arrRange.Keys(jj) + " Hide Total Amount") = 0
                    drTotal(arrRange.Keys(jj) + " TC") = 0
                Next

                For jj As Integer = 0 To arrRange.Count - 1
                    drTotal(arrRange.Keys(jj) + " Milk Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Milk Qty")
                    drTotal(arrRange.Keys(jj) + " Std Qty") += dt.Rows(ii)(arrRange.Keys(jj) + " Std Qty")
                    drTotal(arrRange.Keys(jj) + " Hide Total Amount") += dt.Rows(ii)(arrRange.Keys(jj) + " Hide Total Amount")
                    If clsCommon.myCDecimal(drTotal(arrRange.Keys(jj) + " Std Qty")) > 0 Then
                        drTotal(arrRange.Keys(jj) + " TC") = Math.Round(clsCommon.myCDecimal(drTotal(arrRange.Keys(jj) + " Hide Total Amount")) / clsCommon.myCDecimal(drTotal(arrRange.Keys(jj) + " Std Qty")), 2, MidpointRounding.ToEven)
                    End If
                Next
            End If
            strPrevious = clsCommon.myCstr(dt.Rows(ii)("Center Code"))
            If dt.Rows.Count - 1 = ii Then
                'drTotal("Center Code") = strPrevious
                drTotal("Center Name") = "Total"
                arr.Add(ii + 1, drTotal)
            End If
        Next

        For ii As Integer = arr.Count - 1 To 0 Step -1
            Dim Key As Integer = arr.Keys(ii)
            dt.Rows.InsertAt(arr(Key), Key)
        Next
    End Sub

    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            If gv.Columns(ii).Name.Contains("Hide Total Amount") Then
                gv.Columns(ii).IsVisible = False
            End If
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        If clsCommon.CompairString(cboType.SelectedValue, "Detail") = CompairStringResult.Equal Then
            Dim item1 As New GridViewSummaryItem("Milk Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("FAT Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            Dim item2 As New GridViewSummaryItem("SNF Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item2)
            Dim item3 As New GridViewSummaryItem("HSD In Ltr", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item3)

            Dim item4 As New GridViewSummaryItem("HSD Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item4)
            Dim item5 As New GridViewSummaryItem("Fixed Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item5)
            Dim item6 As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item6)
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "MCC Wise Summary") = CompairStringResult.Equal Then
            Dim item1 As New GridViewSummaryItem("Milk Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Std Qty", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Milk Qty (Self)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Std. Milk (Self)", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("HSD Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Fixed Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem("No Of Vehicle", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem()
            item1.FormatString = "{0:F2}"
            item1.Name = "TC HSD"
            item1.AggregateExpression = "IIf (sum([Std Qty])=0 , '0.00' , sum([HSD Amount]) / sum([Std Qty]))"
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem()
            item1.FormatString = "{0:F2}"
            item1.Name = "TC FIXED"
            item1.AggregateExpression = "IIf (sum([Std Qty])=0 , '0.00' , sum([Fixed Amount]) / sum([Std Qty]))"
            summaryRowItem.Add(item1)

            item1 = New GridViewSummaryItem()
            item1.FormatString = "{0:F2}"
            item1.Name = "TC Total"
            item1.AggregateExpression = "IIf (sum([Std Qty])=0 , '0.00' , sum([Total Amount]) / sum([Std Qty]))"
            summaryRowItem.Add(item1)
        ElseIf clsCommon.CompairString(cboType.SelectedValue, "MCC,Supervisor,Route and Date wise") = CompairStringResult.Equal Then
        End If
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True
        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Report Type : " + clsCommon.myCstr(cboType.SelectedValue))
                If chkMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("MCC Name: " + strMCCName + " "))
                End If

                If ChkSelectRoute.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgRoute.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgRoute.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("Route Name: " + strMCCName + " "))
                End If

                If ChkSelectTransporter.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgTransporter.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgTransporter.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("Transporter Name: " + strMCCName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("VLC Transport Expense Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")

    End Sub

    Private Sub rptVLCTransportExpense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LOCATIONRIGTHS()
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
            RadPageView1.SelectedPage = RadPageViewPage1
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            LoadType()
            LoadShiftFrom()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "B"
        dr("Shift") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt.Copy
        txtFromShift.ValueMember = "Code"

        txtFromShift.SelectedValue = "B"
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr("Code") = "Detail"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC Wise Summary"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MCC,Supervisor,Route and Date wise"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"

        cboType.SelectedValue = "Detail"
    End Sub

    Private Sub rptVLCTransportExpense_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()

        End If
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub chkAllRoute_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgRoute.Enabled = Not chkAllRoute.IsChecked
    End Sub

    Private Sub chkAllTransporter_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllTransporter.ToggleStateChanged
        cbgTransporter.Enabled = Not chkAllTransporter.IsChecked
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click, RadPageViewPage1.Click
        Try
            btnReferesh = True
            PageSetupReport_ID = MyBase.Form_ID
            If clsCommon.CompairString(cboType.SelectedValue, "Detail") = CompairStringResult.Equal Then
                PageSetupReport_ID += "D"
            ElseIf clsCommon.CompairString(cboType.SelectedValue, "MCC Wise Summary") = CompairStringResult.Equal Then
                PageSetupReport_ID += "M"
            ElseIf clsCommon.CompairString(cboType.SelectedValue, "MCC,Supervisor,Route and Date wise") = CompairStringResult.Equal Then
                PageSetupReport_ID += "S"
            End If
            TemplateGridview = gv
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        gv.DataSource = Nothing
        EnableDisableControl(True)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub





    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
