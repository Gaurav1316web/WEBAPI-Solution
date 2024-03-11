'' created By Richa Agarwal 27 MAy,2019 ERO/27/05/19-000623
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RpttankerReportForErode
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim CreateProvisionOfTankerDispatchWithClosingKM As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()

    Public FilterON As Boolean = False
    Public FilterfromDate As Date
    Public FilterToDate As Date
    Public FilterMCCCode As String
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
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
    Private Sub RptSecondaryTransporterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LOCATIONRIGTHS()
            SetUserMgmtNew()
            CreateProvisionOfTankerDispatchWithClosingKM = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateProvisionOfTankerDispatchWithClosingKM, clsFixedParameterCode.CreateProvisionOfTankerDispatchWithClosingKM, Nothing)) = 0, False, True)
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
            RadPageView1.SelectedPage = RadPageViewPage1
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            Reset()
            If FilterON Then
                txtFromDate.Value = FilterfromDate
                txtToDate.Value = FilterToDate
                btnGo.PerformClick()
                If clsCommon.myLen(FilterMCCCode) > 0 Then
                    Dim filter As New FilterDescriptor()
                    filter.PropertyName = "CC Code"
                    filter.[Operator] = FilterOperator.IsEqualTo
                    filter.Value = FilterMCCCode
                    filter.IsFilterEditor = True
                    gv.FilterDescriptors.Add(filter)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    Sub Reset()
        Try
            RadPageView1.SelectedPage = RadPageViewPage1
            If chkMCCAll.IsChecked Then
                cbgMCC.CheckedAll()
            Else
                cbgMCC.UnCheckedAll()
            End If
            LoadMCC()
            LoadPlant()
            LoadTanker()
            chkMCCAll.CheckState = CheckState.Checked
            chkAllRoute.CheckState = CheckState.Checked
            chkAllTransporter.CheckState = CheckState.Checked
            gv.DataSource = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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

    Sub LoadPlant()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "
        Else
            btnGo.Enabled = False
        End If
        cbgPlantCode.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgPlantCode.ValueMember = "Code"
        cbgPlantCode.DisplayMember = "Name"
    End Sub
    Sub LoadTanker()
        Dim qry As String = Nothing
        qry = "Select TSPL_TANKER_MASTER.Tanker_No As Code,  TSPL_TANKER_MASTER.Tanker_Name As Name From TSPL_TANKER_MASTER "
        cbgTanker.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgTanker.ValueMember = "Code"
        cbgTanker.DisplayMember = "Name"
    End Sub
    Private Sub chkAllRoute_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgPlantCode.Enabled = Not chkAllRoute.IsChecked
    End Sub
    Private Sub chkAllTransporter_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllTransporter.ToggleStateChanged
        cbgTanker.Enabled = Not chkAllTransporter.IsChecked
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            btnReferesh = True
            PageSetupReport_ID = MyBase.Form_ID
            If chkIntermittent.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + "I"
            ElseIf chkBulk.Checked = True Then
                PageSetupReport_ID = MyBase.Form_ID + "B"
            End If
            TemplateGridview = gv
            LoadData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadData(ByVal isPrint As Boolean)
        Dim isIntermittent As Boolean = chkIntermittent.Checked
        If txtFromDate.Value > txtToDate.Value Then
            txtFromDate.Focus()
            Throw New Exception("From date can not be greater than to Date")
        End If

        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        If ChkSelectPlant.IsChecked AndAlso cbgPlantCode.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Plant or select all.", Me.Text)
            Exit Sub
        End If
        If ChkSelectTanker.IsChecked AndAlso cbgTanker.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Tanker or select all.", Me.Text)
            Exit Sub
        End If
        Dim companyADD, CompName, CompCode As String
        Dim qry As String
        Dim Qry1 As String = Nothing

        qry = " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        companyADD = dt1.Rows(0).Item("comp_address")

        qry = " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompName = dt2.Rows(0).Item("Comp_Name")


        qry = " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
        Dim Todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        '===============================
        ArrInvoice_Arr = New ArrayList
        Dim Document_Code As String = ""
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                Document_Code = Document_Code + "','" + clsCommon.myCstr(grow.Cells("Dispatch No").Value)
            End If
        Next

        If clsCommon.myLen(Document_Code) > 0 AndAlso clsCommon.myCstr(Document_Code).Substring(0, 3) = "','" Then
            Document_Code = Document_Code.Substring(3, Document_Code.Length - 3)
        End If

        ' for Intermittent
        If chkBulk.Checked = True Then
            qry = "  Select Cast(1 as BIT) as 'Check' , [Route Code],[Route Name] ,final.[MCC Code] as [CC Code],final.[Mcc Name] as [CC Name],final.[Plant Code],final.[Plant Name],final.Chalan_NO as [Dispatch No],
                     final.[Dispatch Date],final.[Tanker No], final.[Transporter Code],final.[Transporter Name2] as [Transporter Name] ,final.provision_no as [Provision No],final.provision_date as [Provision Date], final.[OP. KM],final.[CL. KM],final.[CL. KM] -final.[OP. KM]  as [Running km],final.[Route Fixed K.M] ,
                     convert(decimal(18,3),final.[Rate/Km]) as [Rate/Km],  
                     case when (final.[CL. KM] -final.[OP. KM])<final.[Route Fixed K.M] then (final.[CL. KM] -final.[OP. KM]) else final.[Route Fixed K.M] end as [Passing K.M],
                     final.[Transport Amount ] ,final.[Toll/Weight Amount], final.[Transport Amount ] +final.[Toll/Weight Amount] as [Total Amount],
                     final.[Dispatch Qty] as [Milk Kg], convert(decimal(18,2),(final.[Transport Amount ] +final.[Toll/Weight Amount])/final.[Dispatch Qty]) as Cpl, final.Storage_Capacity as [Vehicle Capacity] ,final.StorageCapacityDesc  as [Vehicle Capacity Desc],
                     0 as [Vehicle out Crate],final.[Dispatch Qty]/iif(final.Storage_Capacity=0,1,final.Storage_Capacity) as [In %]  "
            If isPrint = True Then
                qry += " ,final.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address , TSPL_COMPANY_MASTER.add1 as Comp_add1, TSPL_COMPANY_MASTER.add2 as Comp_add2,TSPL_COMPANY_MASTER.Add3 as Comp_add3, TSPL_COMPANY_MASTER.Email as Comp_Email, TSPL_COMPANY_MASTER.Phone1  as Comp_Phone1, TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2,TSPL_COMPANY_MASTER.Pan_No as Comp_PanNo, TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTREG_No, TSPL_COMPANY_MASTER.CINNo as Comp_CINNO , TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, final.Transporter_IFSC_CODE,final.branch_code,final.Transporter_Branch_Name,final.Bank_Code, final.Transporter_Bank_Name, final.Transporter_Account_No,final.Transporter_PAN,final. Transporter_Phone1  "
            End If
            qry += " From  (

                     select TSPL_MCC_TANKER_GATE_OUT.Bulk_Route_Code as [Route Code] ,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as [Route Name], tspl_provision_entry.Doc_No as provision_no,Convert(varchar,tspl_provision_entry.Doc_Date,103) as provision_date,tspl_gate_entry_details.Gate_Entry_No as Chalan_NO,tspl_gate_entry_details.Date_And_Time as Dispatch_Date, Convert(varchar,tspl_gate_entry_details.Date_And_Time,103) As [Dispatch Date], tspl_gate_entry_details.location_Code As [MCC Code], 
                     TSPL_LOCATION_MASTER_MCC.Location_Desc As [Mcc Name],TSPL_MCC_TANKER_GATE_OUT.TO_LOCATION_CODE as [Plant Code] , TSPL_LOCATION_MASTER_Plant.Location_Desc As [Plant Name],
                     Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Tanker Receiving date], tspl_gate_entry_details.Tanker_No As [Tanker No], case when len(isnull (tspl_gate_entry_details.TotalQty_In_Kg,0) ) > 0 then isnull (tspl_gate_entry_details.TotalQty_In_Kg,0)  else isnull (tspl_gate_entry_details.Qty_In_Kg,0) end As [Dispatch Qty],
                     IsNull(TSPL_Weighment_Detail.Net_Weight, 0) As [Receiving Qty], tspl_gate_entry_details.Challan_No As [Chalan No], TSPL_VENDOR_MASTER.Vendor_Name As [Transporter Name],
                     IsNull(TSPL_MCC_TANKER_GATE_OUT.Distance_of_Route, 0) As [Route Fixed K.M],
                     case when TSPL_TANKER_MASTER.Status ='Day/Diesel' then IsNull(TSPL_TANKER_MASTER.Diesel_Rate ,0)/iif(IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)=0 ,1,IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)) when TSPL_TANKER_MASTER.Status ='Rate/K.M'  then IsNull(TSPL_TANKER_MASTER.Price_KM  ,0)  end  As [Rate/Km]  ,case when (IsNull(tspl_gate_entry_details.ClosingKM, 0)-IsNull(tspl_gate_entry_details.OpeningKM, 0))<IsNull(TSPL_MCC_TANKER_GATE_OUT.Distance_of_Route, 0) then  (IsNull(tspl_gate_entry_details.ClosingKM, 0)-IsNull(tspl_gate_entry_details.OpeningKM, 0)) else IsNull(TSPL_MCC_TANKER_GATE_OUT.Distance_of_Route, 0) end *  (case when TSPL_TANKER_MASTER.Status ='Day/Diesel' then IsNull(TSPL_TANKER_MASTER.Diesel_Rate ,0)/iif(IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)=0 ,1,IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)) when TSPL_TANKER_MASTER.Status ='Rate/K.M'  then IsNull(TSPL_TANKER_MASTER.Price_KM  ,0)  end) As [Transport Amount ] , TSPL_MCC_TANKER_GATE_OUT.TollAmount As [Toll/Weight Amount]  , 0 As [Other Expenses],0 As [Insurance Amount], 0 As [Fat/ Snf Loss Amount], 0 As [Other Deduction], 0 As [Net Payable Amount ] , IsNull(tspl_gate_entry_details.OpeningKM ,0) as  [OP. KM],
                     IsNull(tspl_gate_entry_details.ClosingKM,0) as [CL. KM],TSPL_TANKER_MASTER.Storage_Capacity ,TSPL_TANKER_MASTER.StorageCapacityDesc, tspl_gate_entry_details.Comp_Code ,TSPL_TANKER_MASTER.Tanker_Transporter_Code as [Transporter Code], TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Name2] ,TSPL_VENDOR_MASTER.IFSC_CODE as Transporter_IFSC_CODE,TSPL_VENDOR_MASTER.branch_code,TSPL_VENDOR_MASTER.Branch_Name as Transporter_Branch_Name,TSPL_VENDOR_MASTER.Bank_Code, TSPL_VENDOR_MASTER.Bank_Name as Transporter_Bank_Name,TSPL_VENDOR_MASTER.Account_No as Transporter_Account_No,TSPL_VENDOR_MASTER.PAN as Transporter_PAN,TSPL_VENDOR_MASTER.Phone1 as Transporter_Phone1 "

            qry += " from  tspl_gate_entry_details  left outer join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Ref_Doc_No = tspl_gate_entry_details.Gate_Entry_No 
                     left outer join TSPL_MCC_TANKER_GATE_OUT on TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO = tspl_gate_entry_details.Against_Gate_Out

                     Left Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_MCC On TSPL_LOCATION_MASTER_MCC.Location_Code = tspl_gate_entry_details.location_Code 
                     Left Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_Plant On TSPL_LOCATION_MASTER_Plant.Location_Code = TSPL_MCC_TANKER_GATE_OUT.TO_LOCATION_CODE  
                     Left Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Challan_No = tspl_gate_entry_details.Challan_No 
                     Left Join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_No = tspl_gate_entry_details.Tanker_No  
                     left outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_TANKER_MASTER.Tanker_Transporter_Code 
                     left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MCC_TANKER_GATE_OUT.Bulk_Route_Code "

            qry += " where tspl_gate_entry_details.Doc_Type = 'BulkProc' and len (Against_Gate_Out) >0 and tspl_gate_entry_details.isPosted = 1 and len( isnull(tspl_provision_entry.Doc_No ,'')) >0
                     ) As final   "
            If isPrint = True Then
                qry += " left outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code = final.comp_code "
            End If
            qry += " where (convert(date,[Dispatch_Date],103) between '" + fromDate + "' and '" + Todate + "')  
                   "
            If cbgMCC.CheckedValue.Count > 0 AndAlso chkMCCSelect.IsChecked Then
                qry += " And [MCC Code]  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            If cbgPlantCode.CheckedValue.Count > 0 AndAlso ChkSelectPlant.IsChecked Then
                qry += " And [Plant Code]  IN (" + clsCommon.GetMulcallString(cbgPlantCode.CheckedValue) + ") "
            End If
            If cbgTanker.CheckedValue.Count > 0 AndAlso ChkSelectTanker.IsChecked Then
                qry += " And [Tanker No]  IN (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ") "
            End If
            If isPrint = True Then
                qry += " And final.Chalan_NO in ('" + Document_Code + "')"
            End If

            qry += " order by [Transporter Code] asc, convert(date,[Dispatch_Date],103) asc  "
        Else
            Dim WhrIntermittent As String = ""
            Dim RouteForIntermittent As String = ""
            If isIntermittent Then
                WhrIntermittent = " where TSPL_MCC_Dispatch_Challan.isIntermittent =1 and len(isnull(tspl_provision_entry.Doc_No,'')) > 0 "
                RouteForIntermittent = " ,[Route Code],[Route Name]"
            Else
                WhrIntermittent = " where TSPL_MCC_Dispatch_Challan.isIntermittent =0 "
            End If

            '===============================

            qry = "  Select Cast(1 as BIT) as 'Check' " + RouteForIntermittent + " ,final.[MCC Code] as [CC Code],final.[Mcc Name] as [CC Name],final.[Plant Code],final.[Plant Name],final.Chalan_NO as [Dispatch No]," & Environment.NewLine &
            " final.[Dispatch Date],final.[Tanker No], final.[Transporter Code],final.[Transporter Name2] as [Transporter Name] ,final.provision_no as [Provision No],final.provision_date as [Provision Date], final.[OP. KM],final.[CL. KM],final.[CL. KM] -final.[OP. KM]  as [Running km],final.[Route Fixed K.M] ," & Environment.NewLine &
            " convert(decimal(18,3),final.[Rate/Km]) as [Rate/Km],  " & Environment.NewLine &
            " case when (final.[CL. KM] -final.[OP. KM])<final.[Route Fixed K.M] then (final.[CL. KM] -final.[OP. KM]) else final.[Route Fixed K.M] end as [Passing K.M]," & Environment.NewLine &
            " final.[Transport Amount ] ,final.[Toll/Weight Amount], final.[Transport Amount ] +final.[Toll/Weight Amount] as [Total Amount]," & Environment.NewLine &
            " final.[Dispatch Qty] as [Milk Kg], convert(decimal(18,2),(final.[Transport Amount ] +final.[Toll/Weight Amount])/final.[Dispatch Qty]) as Cpl, final.Storage_Capacity as [Vehicle Capacity] ,final.StorageCapacityDesc  as [Vehicle Capacity Desc]," & Environment.NewLine &
            " 0 as [Vehicle out Crate],final.[Dispatch Qty]/iif(final.Storage_Capacity=0,1,final.Storage_Capacity) as [In %]  "
            If isPrint = True Then
                qry += " ,final.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address , TSPL_COMPANY_MASTER.add1 as Comp_add1, TSPL_COMPANY_MASTER.add2 as Comp_add2,TSPL_COMPANY_MASTER.Add3 as Comp_add3, TSPL_COMPANY_MASTER.Email as Comp_Email, TSPL_COMPANY_MASTER.Phone1  as Comp_Phone1, TSPL_COMPANY_MASTER.Phone2 as Comp_Phone2,TSPL_COMPANY_MASTER.Pan_No as Comp_PanNo, TSPL_COMPANY_MASTER.GSTReg_No as Comp_GSTREG_No, TSPL_COMPANY_MASTER.CINNo as Comp_CINNO , TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2, final.Transporter_IFSC_CODE,final.branch_code,final.Transporter_Branch_Name,final.Bank_Code, final.Transporter_Bank_Name, final.Transporter_Account_No,final.Transporter_PAN,final. Transporter_Phone1  "
            End If

            qry += " From  (Select " & Environment.NewLine &
            " TSPL_MCC_TANKER_GATE_OUT.Bulk_Route_Code as [Route Code] ,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as [Route Name], tspl_provision_entry.Doc_No as provision_no,Convert(varchar,tspl_provision_entry.Doc_Date,103) as provision_date,TSPL_MCC_Dispatch_Challan.Chalan_NO,TSPL_MCC_Dispatch_Challan.Dispatch_Date, Convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) As [Dispatch Date], TSPL_MCC_Dispatch_Challan.MCC_Code As [MCC Code], " & Environment.NewLine &
            " TSPL_LOCATION_MASTER_MCC.Location_Desc As [Mcc Name],TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code as [Plant Code] , TSPL_LOCATION_MASTER_Plant.Location_Desc As [Plant Name]," & Environment.NewLine &
            " Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Tanker Receiving date], TSPL_MCC_Dispatch_Challan.Tanker_No As [Tanker No], IsNull(TSPL_MCC_Dispatch_Challan.Net_Qty, 0) As [Dispatch Qty]," & Environment.NewLine &
            " IsNull(TSPL_Weighment_Detail.Net_Weight, 0) As [Receiving Qty], TSPL_MCC_Dispatch_Challan.Chalan_NO As [Chalan No], TSPL_MCC_Dispatch_Challan.Tanker_Transporter_Name As [Transporter Name]," & Environment.NewLine &
            " IsNull(tspl_location_distance_master.Distance, 0) As [Route Fixed K.M]," & Environment.NewLine &
            " case when TSPL_TANKER_MASTER.Status ='Day/Diesel' then IsNull(TSPL_TANKER_MASTER.Diesel_Rate ,0)/iif(IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)=0 ,1,IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)) when TSPL_TANKER_MASTER.Status ='Rate/K.M'  then IsNull(TSPL_TANKER_MASTER.Price_KM  ,0)  end  As [Rate/Km] "
            If CreateProvisionOfTankerDispatchWithClosingKM = True Then
                qry += " ,case when (IsNull(TSPL_MCC_Dispatch_Challan.ClosingKM, 0)-IsNull(TSPL_MCC_Dispatch_Challan.OpeningKM, 0))<IsNull(tspl_location_distance_master.Distance, 0) then " &
                        " (IsNull(TSPL_MCC_Dispatch_Challan.ClosingKM, 0)-IsNull(TSPL_MCC_Dispatch_Challan.OpeningKM, 0)) else IsNull(tspl_location_distance_master.Distance, 0) end * " &
                        " (case when TSPL_TANKER_MASTER.Status ='Day/Diesel' then IsNull(TSPL_TANKER_MASTER.Diesel_Rate ,0)/iif(IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)=0 ,1,IsNull(TSPL_TANKER_MASTER.Avg_KM_Ltr , 0)) when TSPL_TANKER_MASTER.Status ='Rate/K.M'  then IsNull(TSPL_TANKER_MASTER.Price_KM  ,0)  end) As [Transport Amount ]"
            Else
                qry += " ,IsNull(TSPL_MCC_Dispatch_Challan.Payment_Amount, 0) As [Transport Amount ]"
            End If
            If IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.TollTaxMaster, clsFixedParameterCode.TollTaxMaster, Nothing)) = 0, False, True) = False Then
                qry += " , isnull (tspl_mcc_dispatch_challan.Toll_Amount,0)  As [Toll/Weight Amount] "
            Else
                qry += " , tspl_location_distance_master.Toll_Amt As [Toll/Weight Amount] "
            End If

            qry += " , 0 As [Other Expenses],0 As [Insurance Amount], 0 As [Fat/ Snf Loss Amount], 0 As [Other Deduction], 0 As [Net Payable Amount ] , IsNull(TSPL_MCC_Dispatch_Challan.OpeningKM ,0) as  [OP. KM]," & Environment.NewLine &
            " IsNull(TSPL_MCC_Dispatch_Challan.ClosingKM,0) as [CL. KM],TSPL_TANKER_MASTER.Storage_Capacity ,TSPL_TANKER_MASTER.StorageCapacityDesc,TSPL_MCC_Dispatch_Challan.Comp_Code, TSPL_TANKER_MASTER.Tanker_Transporter_Code as [Transporter Code], TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Name2] ,TSPL_VENDOR_MASTER.IFSC_CODE as Transporter_IFSC_CODE,TSPL_VENDOR_MASTER.branch_code,TSPL_VENDOR_MASTER.Branch_Name as Transporter_Branch_Name,TSPL_VENDOR_MASTER.Bank_Code, TSPL_VENDOR_MASTER.Bank_Name as Transporter_Bank_Name,TSPL_VENDOR_MASTER.Account_No as Transporter_Account_No,TSPL_VENDOR_MASTER.PAN as Transporter_PAN,TSPL_VENDOR_MASTER.Phone1 as Transporter_Phone1   From TSPL_MCC_Dispatch_Challan " & Environment.NewLine &
            " Left Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_MCC On TSPL_LOCATION_MASTER_MCC.Location_Code = TSPL_MCC_Dispatch_Challan.MCC_Code " & Environment.NewLine &
            " Left Join TSPL_LOCATION_MASTER As TSPL_LOCATION_MASTER_Plant On TSPL_LOCATION_MASTER_Plant.Location_Code = TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code " & Environment.NewLine &
            " Left Join Tspl_Gate_Entry_Details On Tspl_Gate_Entry_Details.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " & Environment.NewLine &
            " Left Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Challan_No = TSPL_MCC_Dispatch_Challan.Chalan_NO " & Environment.NewLine &
            " Left Join TSPL_TANKER_MASTER On TSPL_TANKER_MASTER.Tanker_No = TSPL_MCC_Dispatch_Challan.Tanker_No  " & Environment.NewLine &
            " left outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_TANKER_MASTER.Tanker_Transporter_Code " & Environment.NewLine &
            " Left Join tspl_location_distance_master On tspl_location_distance_master.From_Location_code = TSPL_MCC_Dispatch_Challan.MCC_Code And tspl_location_distance_master.to_Location_Code = TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code left join tspl_provision_entry on tspl_provision_entry.Ref_Doc_No=TSPL_MCC_Dispatch_Challan.Chalan_NO
          left outer join TSPL_MCC_TANKER_GATE_OUT on TSPL_MCC_TANKER_GATE_OUT.GATE_OUT_NO = TSPL_MCC_Dispatch_Challan.Against_Gate_Out
          left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MCC_TANKER_GATE_OUT.Bulk_Route_Code
        " + WhrIntermittent + "
        " & Environment.NewLine &
            " ) As final  "
            If isPrint = True Then
                qry += " left outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code = final.comp_code "
            End If
            qry += " where (convert(date,[Dispatch_Date],103) between '" + fromDate + "' and '" + Todate + "')"

            If cbgMCC.CheckedValue.Count > 0 AndAlso chkMCCSelect.IsChecked Then
                qry += " and [MCC Code]  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            If cbgPlantCode.CheckedValue.Count > 0 AndAlso ChkSelectPlant.IsChecked Then
                qry += " and [Plant Code]  IN (" + clsCommon.GetMulcallString(cbgPlantCode.CheckedValue) + ") "
            End If
            If cbgTanker.CheckedValue.Count > 0 AndAlso ChkSelectTanker.IsChecked Then
                qry += " and [Tanker No]  IN (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ") "
            End If
            If isPrint = True Then
                qry += " and final.Chalan_NO in ('" + Document_Code + "')"
            End If

            qry += " order by [Transporter Code] asc, convert(date,[Dispatch_Date],103) asc  "
        End If

        dt = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If
        If isPrint = True Then

            Dim frmCRV As New frmCrystalReportViewer()
            If isIntermittent = True OrElse chkBulk.Checked = True Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, Nothing, "rptTankerReportIntermittent", "Tanker Bill")
            Else
                frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, Nothing, "rptTankerReport", "Tanker Bill")
            End If

            frmCRV = Nothing

        Else
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dt
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            Dim summaryRowItem As New GridViewSummaryRowItem()
            gv.SummaryRowsBottom.Clear()

            For iii As Integer = 1 To gv.Columns.Count - 2
                If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                    summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                End If
            Next

            Dim item13 As New GridViewSummaryItem("In %", "{0:F3}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item13)
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            'gv.ReadOnly = True
            SetGridFormationOFGV1()
            gv.Columns("Check").IsVisible = True
            gv.Columns("Check").Width = 100
            gv.Columns("Check").HeaderText = " "
            gv.Columns("Check").ReadOnly = False

            RadPageView1.SelectedPage = RadPageViewPage2
            gv.BestFitColumns()
            ReStoreGridLayout()
        End If

    End Sub

    Sub SetGridFormationOFGV1()
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).BestFit()
        Next

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
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptSecondaryTransporterReport & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If chkIntermittent.Checked = True Then
                    arrHeader.Add("Document Type : Intermittent")
                End If
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

                If ChkSelectPlant.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgPlantCode.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgPlantCode.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("Plant Name: " + strMCCName + " "))
                End If

                If ChkSelectTanker.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgTanker.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgTanker.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("Tanker Name: " + strMCCName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Tanker Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No data to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
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
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
End Class
