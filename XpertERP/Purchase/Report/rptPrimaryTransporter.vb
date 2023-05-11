Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===========Created by Preeti gupta Ticket No[5605,BM00000008005,BM00000008598]==================
Public Class RptPrimaryTransporter
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim ApplyCalculationOnRouteLenth As Boolean = False
    Dim ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster As Boolean = False

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
        'MyBase.SetUserMgmt(clsUserMgtCode.rptPrimaryTransporter)
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

    Sub FormatGrid()
        ' Dim strItemCode, head2 As Stringg

        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True

        Next
        gv.Columns("Vehicle").IsVisible = ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster
        gv.Columns("TDS 20%").IsVisible = False
        'gv.Columns("Document_Type").IsVisible = False

        If chkDetail.IsChecked Then
            gv.Columns("DOC DATE").IsVisible = True
        ElseIf chkSummary.IsChecked Then
            gv.Columns("DOC DATE").IsVisible = False
        End If

        'gv.Columns("DOC_DATE").IsVisible = False
        gv.Columns("Other Deduction").IsVisible = False

        '----Sanjeet(24/05/2017)--
        gv.Columns("Transport Cost/Kg").FormatString = "{0:n3}"

        If ApplyCalculationOnRouteLenth = True Then
            'gv.Columns("Standard Total Km").IsVisible = False
            'gv.Columns("KM Per Shift").HeaderText = "Route Length"
            gv.Columns("KM Per Shift").IsVisible = False
            gv.Columns("Standard Total Km").HeaderText = "Route Length"

            '[Diesel rate] , [Diesel Amount], [Rate/Ltr],[Ltr Amount],[Rate/Km],[Km Amount],[Provision Code],[Provision Date]
            gv.Columns("Diesel rate").IsVisible = False
            gv.Columns("Diesel Amount").IsVisible = False
            gv.Columns("Rate/Ltr").IsVisible = False
            gv.Columns("Ltr Amount").IsVisible = False
            gv.Columns("Rate/Km").IsVisible = False
            gv.Columns("Km Amount").IsVisible = False
            'gv.Columns("Provision Code").IsVisible = False
            'gv.Columns("Provision Date").IsVisible = False
        End If
        '--------------------
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Total Milk Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        item1 = New GridViewSummaryItem("Total Milk Qty Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        ''Dim item2 As New GridViewSummaryItem("Total VLC", "{0:F2}", GridAggregateFunction.Sum)
        ''summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Standard Total No of days", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("Total Shifts", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Standard Total Km", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Diesel Consume (Ltr)", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Total Shift Charges", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        item7 = New GridViewSummaryItem("Total SlabRange Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("Diesel Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item9 As New GridViewSummaryItem("No of Days Vehicle Run", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Rent Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Gross Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("Material Sale", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("Material Sale Return", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("Item Issue", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        'Dim item15 As New GridViewSummaryItem("Other Deduction", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item15)

        Dim item16 As New GridViewSummaryItem("Net Payment", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("DrNote", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)
        Dim item18 As New GridViewSummaryItem("CrNote", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)

        Dim item19 As New GridViewSummaryItem("Km Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item19)
        Dim item20 As New GridViewSummaryItem("Ltr Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item20)


        'FOR UDL ONLY 
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            Try
                If chkActualDistance.IsChecked Then
                    Dim item22 As New GridViewSummaryItem("Actual Distance (Single Shift)", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item22)
                    Dim item23 As New GridViewSummaryItem("Actual Distance (All Shifts)", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item23)
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(ex.Message)
            End Try
        End If




        'Dim item5 As New GridViewSummaryItem("SNF(KG)", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item5)
        'Dim summaryItem1 As New GridViewSummaryItem()
        'summaryItem1.FormatString = "{0:F2}"
        'summaryItem1.Name = "FAT(%)"
        'summaryItem1.AggregateExpression = "sum([FAT(KG)])*100/sum([Milk Weight(KG)])"
        'summaryRowItem.Add(summaryItem1)

        'Dim summaryItem2 As New GridViewSummaryItem()
        'summaryItem2.FormatString = "{0:F2}"
        'summaryItem2.Name = "SNF(%)"
        'summaryItem2.AggregateExpression = "sum([SNF(KG)])*100/sum([Milk Weight(KG)])"
        'summaryRowItem.Add(summaryItem2)

        'Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item6)
        'Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item7)

        'Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)

        'summaryRowItem.Add(item8)
        'Dim summaryItem3 As New GridViewSummaryItem()
        'summaryItem3.FormatString = "{0:F2}"
        'summaryItem3.Name = "Cow SNF(%)"
        'summaryItem3.AggregateExpression = "sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)])"
        'summaryRowItem.Add(summaryItem3)

        'Dim summaryItem4 As New GridViewSummaryItem()
        'summaryItem4.FormatString = "{0:F2}"
        'summaryItem4.Name = "Cow FAT(%)"
        'summaryItem4.AggregateExpression = "sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)])"
        'summaryRowItem.Add(summaryItem4)

        'Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item9)
        'Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item10)
        'Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item11)
        'Dim summaryItem5 As New GridViewSummaryItem()
        'summaryItem5.FormatString = "{0:F2}"
        'summaryItem5.Name = "Buffalo FAT(%)"
        'summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
        'summaryRowItem.Add(summaryItem5)

        'Dim summaryItem6 As New GridViewSummaryItem()
        'summaryItem6.FormatString = "{0:F2}"
        'summaryItem6.Name = "Buffalo SNF(%)"
        'summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
        'summaryRowItem.Add(summaryItem6)


        ''Dim item12 As New GridViewSummaryItem("SRN Amount", "{0:F2}", GridAggregateFunction.Sum)
        ''summaryRowItem.Add(item12)
        ''Dim item13 As New GridViewSummaryItem("SRN Qty", "{0:F2}", GridAggregateFunction.Sum)
        ''summaryRowItem.Add(item13)
        'Dim item14 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item14)
        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_UPLOADER as Item format ""{0}: {1}"" Group By VLC_UPLOADER"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        Try

            RadPageView1.SelectedPage = RadPageViewPage1
            If chkMCCAll.IsChecked Then
                cbgMCC.CheckedAll()
            Else
                cbgMCC.UnCheckedAll()
            End If
            chkDetail.IsChecked = True
            LoadMCC()
            LoadRoute()
            LoadTransporter()
            chkMCCAll.CheckState = CheckState.Checked
            chkAllRoute.CheckState = CheckState.Checked
            chkAllTransporter.CheckState = CheckState.Checked
            'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
            EnableDisableControl(True)

            'udl only
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                chkActualDistance.Visible = True
                chkActualDistance.Checked = False
                chkDetail.CheckState = CheckState.Checked
            Else
                chkActualDistance.Checked = False
                chkActualDistance.Visible = False
            End If

            gv.DataSource = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub
    Private Sub LoadData(ByVal companyCode As String)
        Try
            If clsCommon.CompairString(companyCode, "UDL") = CompairStringResult.Equal Then
                If txtFromDate.Value > txtToDate.Value Then
                    txtFromDate.Focus()
                    Throw New Exception("From date can not be greater then to Date")
                End If

                If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
                    clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
                    Exit Sub
                End If
                If ChkSelectRoute.IsChecked AndAlso cbgRoute.CheckedValue.Count = 0 Then
                    clsCommon.MyMessageBoxShow("Please select atleast single Route or select all.")
                    Exit Sub
                End If
                If ChkSelectTransporter.IsChecked AndAlso cbgTransporter.CheckedValue.Count = 0 Then
                    clsCommon.MyMessageBoxShow("Please select atleast single Transporter or select all.")
                    Exit Sub
                End If
                Dim companyADD, CompName, CompCode As String
                Dim qry As String
                Dim Qry1 As String = Nothing
                qry = ""
                qry += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                companyADD = dt1.Rows(0).Item("comp_address")

                qry = ""
                qry += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
                Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                CompName = dt2.Rows(0).Item("Comp_Name")


                qry = ""
                qry += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
                Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)
                CompCode = dt5.Rows(0).Item("Comp_Code")

                Dim fromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")

                Dim Todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")

                ''=====================================Slab Range Query=========Done by Monikaon ravi system16/03/2017================================================
                Dim SlabRangeqry As String = " case when isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) <=(select min(tspl_slab_range_detail.slab_upto) as slab_upto from tspl_slab_range_detail where tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code) then " &
                    " isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) * (select top 1 Slab_Rate from tspl_slab_range_detail where tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code and Slab_Upto= (select min(slab_upto) as slab_upto from tspl_slab_range_detail where tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code)) else " &
                    " (select sum(mmf.act * isnull(mmf.Slab_Rate,0)) from (select case when (isnull(mm.diff,0) - isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) + isnull(mm.Slab_Upto,0))<0 then 0 when (isnull(mm.diff,0) - isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) + isnull(mm.Slab_Upto,0))=0 and isnull(mm.diff,0)>=0 then (case when isnull(mm.Slab_Upto,0) > isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) then isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) else isnull(mm.Slab_Upto,0) end) else (isnull(mm.diff,0) - isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) + isnull(mm.Slab_Upto,0)) end as act,mm.Slab_Rate from (select tspl_slab_range_detail.trans_id,tspl_slab_range_detail.Slab_Upto,tspl_slab_range_detail.Slab_Rate,sum(isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) - isnull(tspl_slab_range_detail.Slab_Upto,0)) over (partition by trans_id order by slab_upto) as diff from tspl_slab_range_detail where tspl_slab_range_detail.form_id='PTV-MST' and tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code group by tspl_slab_range_detail.trans_id,tspl_slab_range_detail.Slab_Upto,tspl_slab_range_detail.Slab_Rate)mm)mmf) end as slab_range_amt, "
                ''================================================================================================


                qry = ""

                qry += " select final.*,convert(decimal(18,2),(isnull(final.[Gross Amount],0) -isnull(final.[TDS 20%],0) -isnull(final.[Material Sale],0)+isnull(final.[Material Sale Return],0) -isnull(final.[Item Issue],0)+isnull(final.[CrNote],0) -isnull(final.[DrNote],0)))  as [Net Payment],round(isnull(final.[Gross Amount],0)/nullif(final.[Total Milk Qty Kg],0),3) as [Transport Cost/Kg] from(" &
                 "select convert(varchar,xx.DOC_DATE,103) as [DOC DATE] , xx.transporter_code as [Transporter Code],xx.Transp_Name as [Transporter Name],xx.Account_No as [Account No],xx.IFSC_Code as [IFSC Code],xx.bank_name as [Bank Name],xx.branch_name as [Branch Name],xx.MCC_Code as [MCC Code]," &
                 " xx.MCC_NAME as [MCC Name], xx.Route_Code as [Route Code],xx.Route_Name as [Route Name],xx.Pan_No  as [PAN No] ,xx.Transp_vehicleNo as [Transporter Vehicle No]," &
                 " xx.qty as [Total Milk Qty],xx.qty_kg as [Total Milk Qty Kg],CONVERT(decimal(18,2),xx.std_qty) as Standard_Qty, CONVERT(decimal(18,2),(XX.SNF_KG*100)/XX.ACC_Qty) AS [SNF %],CONVERT(decimal(18,2),(XX.FAT_KG*100)/XX.ACC_Qty) AS [FAT %],Total_VLC as [Total VLC],CONVERT(decimal(18,2),xx.qty/Total_VLC) AS Avg_Qty_Per_VLC,xx.Stand_Per_Day_km as [KM Per Shift],xx.Stand_Total_Shift as [Total Shifts]," &
                 " xx.Standard_Total_Km as [Standard Total Km], xx.ActualKMsMax as [Actual Distance (Single Shift)],  xx.VLC_Actual_KMs_allShift as [Actual Distance (All Shifts)], xx.mileage_Km_Ltr as [Mileage (Km/Ltr)],xx.Diesel_consume_ltr as [Diesel Consume (Ltr)],xx.Shift_Charges,xx.[Total Shift Charges],xx.slab_range_amt,xx.[Total SlabRange Amt]," &
                 " xx.Diesel_Rate as [Diesel rate],(xx.Diesel_consume_ltr*xx.Diesel_Rate) as [Diesel Amount], xx.Rental_Month as [Total Monthly Rent]," &
                 " xx.No_Days_In_month as [No. of days in Month],xx.countVehicle as [No of Days Vehicle Run] ,convert(decimal(18,2),(xx.Rental_Month/xx.No_Days_In_month))*xx.countVehicle as [Rent Amount],xx.[Rate/Ltr],xx.[Ltr Amount],xx.[Rate/Km], xx.[Km Amount], " &
                " isnull(convert(decimal(18,2),isnull(xx.[Total Shift Charges],0)+isnull(xx.[total slabrange amt],0)+isnull(xx.[Ltr Amount],0)+isnull(xx.[Km Amount],0)+(isnull(xx.Diesel_consume_ltr,0)*isnull(xx.Diesel_Rate,0))+((isnull(xx.Rental_Month,0)/isnull(xx.No_Days_In_month,0))*isnull(xx.countVehicle,0))),0) as [Gross Amount],  " &
                  " case when Len_Pan_no  =10 then 0 else (isnull(xx.Diesel_consume_ltr,0)*isnull(xx.Diesel_Rate,0))+((isnull(xx.Rental_Month,0)/isnull(xx.No_Days_In_month,0))*isnull(xx.countVehicle,0))*0.2 end as [TDS 20%],  " &
               "  isnull(Matsalequery.MaterialSale_Amt,0) as [Material Sale],isnull(MaterialSaleRetQry.MaterialSale_Return,0) as [Material Sale Return],isnull(Item_IssueQuery.Item_Issue_Amt ,0) as [Item Issue],Other_Deduction as [Other Deduction],isnull(xx .DrNote,0) AS DrNote,isnull(xx .CrNote,0) AS CrNote"
                If chkSummary.IsChecked Then
                    qry += ",XX.SNF_KG,XX.FAT_KG,xx.qty,XX.ACC_Qty"
                End If
                qry += " from (" &
               " select Doc_Date,transporter_code,max(transporter_Name ) as Transp_Name,max(Account_No) as Account_No ,max(IFSC_Code) as IFSC_Code ,max(bank_name) as bank_name,max(branch_name) as branch_name,MCC_Code,max(MCC_NAME) as MCC_NAME  ,ROUTE_CODE,max(Route_Name)  as Route_Name," &
                  " max(Pan)as Pan_No,max(len(PAN ))as Len_Pan_no,Transp_vehicleNo,sum(qty) as qty,sum(isnull(qty_kg,0)) as qty_kg,sum(isnull(std_qty,0)) as std_qty,sum(isnull(SNF_KG,0)) AS SNF_KG,sum(isnull(FAT_KG,0)) AS FAT_KG,SUM(isnull(ACC_Qty,0)) AS ACC_Qty,count(distinct Total_VLC )as Total_VLC ,  MAX(VLC_Actual_KMs) as ActualKMsMax  , (MAX(VLC_Actual_KMs) * COUNT(DISTINCT Stand_Total_Shift)) AS VLC_Actual_KMs_allShift  " &
                  " ,max(Stand_Per_Day_km ) as Stand_Per_Day_km,count(distinct Stand_Total_Shift ) as Stand_Total_Shift,max(Stand_Per_Day_km )*Count(distinct Stand_Total_Shift ) as Standard_Total_Km, " &
                  " max(Avg_Km_Ltr ) as mileage_Km_Ltr,case when max(Avg_Km_Ltr )=0 then max(Avg_Km_Ltr ) else  max(Stand_Per_Day_km )*count(distinct Stand_Total_Shift )/max(Avg_Km_Ltr ) end as Diesel_consume_ltr ," &
                  " max(Diesel_Rate) as Diesel_Rate,max(Rental_Month ) as Rental_Month,Shift_Charges,slab_range_amt,count(distinct stand_total_shift) * isnull(slab_range_amt,0) as [Total SlabRange Amt],max(No_Days_In_month ) as No_Days_In_month ,sum(countVehicle ) as Vehicle,max(countVehicle ) as countVehicle" &
                  " ,0 as Material_sale,isnull(max(Other_Deduction),0) as Other_Deduction,isnull(max(DrNote),0) as DrNote,isnull(max(CrNote),0) as CrNote,max([Ltr Amount]) as [Rate/Ltr],sum(Qty)*max([Ltr Amount]) as [Ltr Amount],max([Rate/Km]) as [Rate/Km],max(Stand_Per_Day_km)*count(distinct Stand_Total_Shift)*max([Rate/Km]) as [Km Amount],count(distinct Stand_Total_Shift)*Shift_Charges as [Total Shift Charges]" &
                  " from (" &
                  " select tspl_milk_srn_detail.std_qty,TSPL_MILK_SAMPLE_DETAIL.SNF,TSPL_MILK_SAMPLE_DETAIL.SNF_KG,TSPL_MILK_SAMPLE_DETAIL.FAT,TSPL_MILK_SAMPLE_DETAIL.FAT_KG,TSPL_MILK_SAMPLE_DETAIL.ACC_Qty,TSPL_Primary_Vehicle_Master.Vendor_Code as transporter_code,TSPL_VENDOR_MASTER.Vendor_Name as transporter_Name,TSPL_VENDOR_MASTER .Account_No ,TSPL_VENDOR_MASTER.IFSC_Code,TSPL_Vendor_Bank_MASTER.bank_name,TSPL_Vendor_Bank_Branch_Details.branch_name," &
                  " " + SlabRangeqry + " " &
                  " TSPL_Primary_Vehicle_Master.MCC_Code,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MCC_ROUTE_MASTER.ROUTE_CODE ,TSPL_MCC_ROUTE_MASTER.Route_Name, " &
                  " TSPL_VENDOR_MASTER.PAN  ,isnull(TSPL_Primary_Vehicle_Master.Shift_Charges,0) as Shift_Charges,TSPL_Primary_Vehicle_Master.Vehicle_Code as Transp_vehicleNo," &
                  " isnull(TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT,0) as Qty,isnull(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT,0) as qty_kg, TSPL_MILK_RECEIPT_DETAIL.VLC_CODE   as Total_VLC,isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) as Stand_Per_Day_km ," &
                  " isnull(TSPL_MCC_ROUTE_VLC_MAPPING.Distance,0) as VLC_Actual_KMs , " &
                  " TSPL_MILK_RECEIPT_HEAD.shift as Stand_Total_Shift,isnull(TSPL_MILK_Shift_End_Route_DETAIL.Total_KM,0) as Total_KM ,isnull(TSPL_Primary_Vehicle_Master.Avg_Km_Ltr,0) as Avg_Km_Ltr," &
                  " isnull(TSPL_Primary_Vehicle_Master.Diesel_Rate,0) as Diesel_Rate , " &
                  " isnull((case when TSPL_Primary_Vehicle_Master.Status='Rental/Diesel' then  TSPL_Primary_Vehicle_Master.Rental_Amount " &
                  " when TSPL_Primary_Vehicle_Master.Status='Rental' then " &
                  " (case when TSPL_Primary_Vehicle_Master.Rental_Type='Day' then TSPL_Primary_Vehicle_Master.Rental_Amount*DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,TSPL_MILK_RECEIPT_HEAD.DOC_DATE),0))) " &
                  " when TSPL_Primary_Vehicle_Master.Rental_Type='Month' then TSPL_Primary_Vehicle_Master.Rental_Amount " &
                  " when TSPL_Primary_Vehicle_Master.Rental_Type='Year' then TSPL_Primary_Vehicle_Master.Rental_Amount/12.00 else 0 end ) " &
                  " end),0) as Rental_Month ," &
                  " DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,TSPL_MILK_RECEIPT_HEAD.DOC_DATE),0))) as No_Days_In_month,TSPL_Primary_Vehicle_Master.Rate_Type as [Rate/Ltr],TSPL_Primary_Vehicle_Master.Price_Ltr_KG as [Ltr Amount],TSPL_Primary_Vehicle_Master.Price_KM as [Rate/Km] " &
                  " , convert(date,TSPL_MILK_RECEIPT_head.DOC_DATE,103) as Doc_Date,  " &
                  " (select distinct case when coalesce(VEHICLE_CODE,'')='' then 0 else 1 end as Vehicle from TSPL_MILK_RECEIPT_DETAIL inner join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE" &
                  " where (TSPL_MILK_RECEIPT_HEAD.doc_date between '" + fromDate + "' and '" + Todate + "') and VEHICLE_CODE=TSPL_Primary_Vehicle_Master.Vehicle_Code) " &
                  " as countVehicle," &
                  " TSPL_VENDOR_INVOICE_HEAD.Total_Amt as Other_Deduction,TSPL_VENDOR_INVOICE_HEAD.DrNote,TSPL_VENDOR_INVOICE_HEAD.CrNote  from TSPL_Primary_Vehicle_Master " &
                  " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code and Form_Type='PTM' " &
                  " left outer join TSPL_Vendor_Bank_MASTER on TSPL_Vendor_Bank_MASTER.bank_code=tspl_vendor_master.bank_code left outer join TSPL_Vendor_Bank_Branch_Details on TSPL_Vendor_Bank_Branch_Details.bank_code=tspl_vendor_master.bank_code and TSPL_Vendor_Bank_Branch_Details.bank_ifsc_code=tspl_vendor_master.branch_code " &
                  " left join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE =TSPL_Primary_Vehicle_Master.Vehicle_Code  and TSPL_Primary_Vehicle_Master.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code" &
                  " left join TSPL_MILK_RECEIPT_head on TSPL_MILK_RECEIPT_head.DOC_CODE  =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " &
                  " Left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE=TSPL_MILK_RECEIPT_head.MCC_CODE and  TSPL_MILK_Shift_End_HEAD.shift=TSPL_MILK_RECEIPT_head.shift and convert(date, TSPL_MILK_Shift_End_HEAD.doc_date,103)=convert(date, TSPL_MILK_RECEIPT_head.doc_date  ,103) " &
                  " left join TSPL_MILK_Shift_End_Route_DETAIL on TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE =TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE  and TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE=TSPL_MILK_Shift_End_HEAD.DOC_CODE " &
                  " left join TSPL_MCC_MASTER on tspl_mcc_master .MCC_Code =TSPL_Primary_Vehicle_Master.MCC_Code " &
                  " left join TSPL_MCC_ROUTE_MASTER  on  TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE" &
                  "  left join  TSPL_MCC_ROUTE_VLC_MAPPING on TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE = TSPL_MCC_ROUTE_VLC_MAPPING.Route_CODE and  TSPL_MILK_RECEIPT_DETAIL.VLC_CODE = TSPL_MCC_ROUTE_VLC_MAPPING.VLC_CODE " &
                  " left outer join (select TSPL_MILK_SAMPLE_DETAIL.*,MILK_RECEIPT_CODE from TSPL_MILK_SAMPLE_DETAIL inner join TSPL_MILK_SAMPLE_Head on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE " &
                  " =TSPL_MILK_SAMPLE_Head.DOC_CODE) TSPL_MILK_SAMPLE_DETAIL  on TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE =TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE left join tspl_milk_srn_Head on  tspl_milk_srn_Head.vlc_doc_code=TSPL_MILK_RECEIPT_DETAIL.vlc_doc_code and tspl_milk_srn_Head.sample_No=" &
                   " TSPL_MILK_RECEIPT_DETAIL.sample_No and tspl_milk_srn_Head.milk_sample_code=TSPL_MILK_sample_DETAIL.doc_code left join tspl_milk_srn_detail " &
                   " on tspl_milk_srn_detail.doc_code=tspl_milk_srn_Head.doc_code " &
                  " left join (select sum(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 0 when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 0 else TSPL_VENDOR_INVOICE_HEAD.Document_Total end) as Total_Amt ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Document_Date,sum(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end)as DrNote,sum(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end)as CrNote  " &
                  " from TSPL_VENDOR_INVOICE_HEAD  /*left join TSPL_Primary_Vehicle_Master on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) between convert(date,('" + txtFromDate.Value + "'),103) and convert(date,('" + txtToDate.Value + "'),103) */ " &
                  " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code where  " &
                  " convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) <=convert(date,('" + txtToDate.Value + "'),103)" &
                  " AND ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='PTM' group by TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ,convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103))  as TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code" &
                  " and  convert(date,TSPL_MILK_RECEIPT_head.DOC_DATE,103) =convert(date,TSPL_VENDOR_INVOICE_HEAD.Document_Date,103)" &
                  ")xx  where 2=2  and DOC_DATE >='" + fromDate + "' and DOC_DATE <='" + Todate + "'  "


                If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count > 0 Then
                    qry += " and MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
                Else
                    qry += " and mcc_code in (select location_code from tspl_location_master where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' "
                    If clsCommon.myLen(arrLoc) > 0 Then
                        qry += " and Location_Code in (" + arrLoc + ") "
                    End If
                    qry += " ) "
                End If
                If ChkSelectRoute.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
                    qry += " and route_code  IN (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
                End If
                If ChkSelectTransporter.IsChecked AndAlso cbgTransporter.CheckedValue.Count > 0 Then
                    qry += " and transporter_code  IN (" + clsCommon.GetMulcallString(cbgTransporter.CheckedValue) + ") "
                End If
                qry += "  group by Doc_Date ,transporter_code ,MCC_Code ,ROUTE_CODE ,Transp_vehicleNo,Shift_Charges,slab_range_amt )xx " &
                    " lEFT jOIN (select isnull(sum(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt),0) as MaterialSale_Amt,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) AS Document_Date " &
                    " from TSPL_SD_SALE_INVOICE_HEAD left join TSPL_Primary_Vehicle_Master on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =TSPL_Primary_Vehicle_Master.Vendor_Code where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_Primary_Vehicle_Master.Vendor_Code and" &
                    " TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='MCC' group by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) ) AS Matsalequery on xx.transporter_code=Matsalequery.Customer_Code and convert(date,xx.Doc_Date ,103)=Matsalequery.Document_Date " &
                    " left join (select sum(TSPL_SD_SALE_RETURN_HEAD.Total_Amt) as MaterialSale_Return," &
                    " CONVERT(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code " &
                    " from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_HEAD.Document_Code Left Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No =TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code Where 2=2  and  TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' and TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_RETURN_HEAD.Status=1 " &
                    " group by CONVERT(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),TSPL_SD_SALE_RETURN_HEAD.Customer_Code) as MaterialSaleRetQry on xx.transporter_code=MaterialSaleRetQry.Customer_Code and convert(date,xx.Doc_Date ,103)=MaterialSaleRetQry.Document_Date" &
                    " left join (select isnull(sum(TSPL_VSPItem_HEAD.Doc_Amt ),0) as Item_Issue_Amt,TSPL_VSPItem_HEAD.Issue_To as Vendor_Code,CONVERT(DATE,TSPL_VSPItem_HEAD.Doc_Date  ,103) AS Document_Date from TSPL_VSPItem_HEAD left join TSPL_Primary_Vehicle_Master on TSPL_VSPItem_HEAD.Issue_To  =TSPL_Primary_Vehicle_Master.Vendor_Code where TSPL_VSPItem_HEAD.Issue_To =TSPL_Primary_Vehicle_Master.Vendor_Code and TSPL_VSPItem_HEAD.Doc_Type  ='Issue' group by TSPL_VSPItem_HEAD.Issue_To ,CONVERT(DATE,TSPL_VSPItem_HEAD.Doc_Date ,103) ) As Item_IssueQuery on xx.transporter_code=Item_IssueQuery.Vendor_Code  and convert(date,xx.Doc_Date ,103)=Item_IssueQuery.Document_Date" &
                    " ) final left outer join TSPL_VENDOR_MASTER   on final.[Transporter Code]  =TSPL_Vendor_MASTER.Vendor_Code   "

                If chkDetail.IsChecked Then
                    Qry1 = "" & qry & " order by [DOC DATE]"
                ElseIf chkSummary.IsChecked Then
                    Qry1 = "select '" & clsCommon.GetPrintDate(fromDate, "dd/MM/yyyy") & "' as [From Date],'" & clsCommon.GetPrintDate(Todate, "dd/MM/yyyy") & "' as [To Date],summary.[Transporter Code] ,max(summary .[Transporter Name]) as [Transporter Name],max(summary .[Account No]) as [Account No],max(summary.[IFSC Code]) as [IFSC Code],max(summary.[Bank Name]) as [Bank Name],max(summary.[Branch Name]) as [Branch Name]  ,'' as [Doc Date],"
                    Qry1 += "  summary .[MCC Code] ,max(summary .[MCC Name]) as [MCC Name] ,summary.[Route Code] ,max(summary .[Route Name]) as [Route Name] ,max(summary .[PAN No]) as [PAN No] ,"
                    Qry1 += "  max(summary .[Transporter Vehicle No]) as [Transporter Vehicle No] ,sum(summary .[Total Milk Qty]) as [Total Milk Qty],sum(summary.[Total Milk Qty Kg]) as [Total Milk Qty Kg],sum(summary.Standard_Qty) as Standard_Qty, CONVERT(decimal(18,2),(sum(summary.SNF_KG)*100)/sum(summary.ACC_Qty)) AS [SNF %],CONVERT(decimal(18,2),(sum(summary.FAT_KG)*100)/sum(summary.ACC_Qty)) AS [FAT %] ,max(summary .[Total VLC]) as [Total VLC],CONVERT(decimal(18,2),sum(summary.qty)/max(summary .[Total VLC])) AS Avg_Qty_Per_VLC,"
                    Qry1 += "  max(summary .[KM Per Shift]) as [KM Per Shift] ,sum(summary.[Total Shifts]) as [Total Shifts] ,"
                    Qry1 += "  max(summary .[KM Per Shift])*sum(summary.[Total Shifts]) as [Standard Total Km] , max([Actual Distance (Single Shift)]) as [Actual Distance (Single Shift)], max([Actual Distance (All Shifts)]) as [Actual Distance (All Shifts)], max(summary .[Mileage (Km/Ltr)]) as [Mileage (Km/Ltr)],"
                    Qry1 += "  sum(summary.[Diesel Consume (Ltr)]) as [Diesel Consume (Ltr)],max(Shift_Charges) as Shift_Charges,sum([Total Shift Charges]) as [Total Shift Charges],sum(summary.[Total SlabRange Amt]) as [Total SlabRange Amt],max(summary.[Diesel rate]) as [Diesel rate],sum(summary.[Diesel Amount]) as [Diesel Amount] ,"
                    Qry1 += "  max(summary.[Total Monthly Rent]) as [Total Monthly Rent],max([No. of days in Month]) as [No. of days in Month],sum(summary.[No of Days Vehicle Run]) as [No of Days Vehicle Run],"
                    Qry1 += "  max(summary .[Rent Amount]) as [Rent Amount],max(summary.[Rate/Ltr]) as [Rate/Ltr],sum(summary.[Ltr Amount]) as [Ltr Amount],max(summary.[Rate/Km]) as [Rate/Km], sum(summary.[Km Amount]) as [Km Amount],sum(summary.[Gross Amount]) as [Gross Amount] ,sum(summary.[TDS 20%]) as [TDS 20%] ,"
                    Qry1 += "  sum(summary.[Material Sale]) as [Material Sale],sum(summary.[Material Sale Return]) as [Material Sale Return],sum(summary .[Item Issue]) as [Item Issue],sum(summary .[Other Deduction]) as [Other Deduction],sum(summary .DrNote) as DrNote ,sum(summary .CrNote) as CrNote,"
                    Qry1 += "  sum(summary.[Net Payment]) as [Net Payment],round(isnull(sum(summary.[Gross Amount]),0)/isnull(sum(summary.[Total Milk Qty Kg]),0),3) as [Transport Cost/Kg]  "

                    Qry1 += "  from( " & qry & ""
                    Qry1 += " )summary group by [Transporter Code] ,[MCC Code] ,[Route Code],[Transporter Vehicle No] "
                End If


                dt = clsDBFuncationality.GetDataTable(Qry1)
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dt
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                If btnReferesh = False Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "crptMemberPaymentSlip", "Member Payment Slip", "")
                    frmCRV = Nothing
                End If
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                RadPageView1.SelectedPage = RadPageViewPage2
                gv.BestFitColumns()
                ReStoreGridLayout()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub LoadData()
        If txtFromDate.Value > txtToDate.Value Then
            txtFromDate.Focus()
            Throw New Exception("From date can not be greater then to Date")
        End If

        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.")
            Exit Sub
        End If
        If ChkSelectRoute.IsChecked AndAlso cbgRoute.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Route or select all.")
            Exit Sub
        End If
        If ChkSelectTransporter.IsChecked AndAlso cbgTransporter.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Transporter or select all.")
            Exit Sub
        End If
        Dim companyADD, CompName, CompCode As String
        Dim qry As String
        Dim Qry1 As String = Nothing
        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        companyADD = dt1.Rows(0).Item("comp_address")

        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompName = dt2.Rows(0).Item("Comp_Name")


        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")

        Dim Todate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        qry = ""
        Dim SlabRangeqry As String = " case when isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) <=(select min(tspl_slab_range_detail.slab_upto) as slab_upto from tspl_slab_range_detail where tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code) then " &
            " isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) * (select top 1 Slab_Rate from tspl_slab_range_detail where tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code and Slab_Upto= (select min(slab_upto) as slab_upto from tspl_slab_range_detail where tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code)) else " &
            " (select sum(mmf.act * isnull(mmf.Slab_Rate,0)) from (select case when (isnull(mm.diff,0) - isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) + isnull(mm.Slab_Upto,0))<0 then 0 when (isnull(mm.diff,0) - isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) + isnull(mm.Slab_Upto,0))=0 and isnull(mm.diff,0)>=0 then (case when isnull(mm.Slab_Upto,0) > isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) then isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) else isnull(mm.Slab_Upto,0) end) else (isnull(mm.diff,0) - isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) + isnull(mm.Slab_Upto,0)) end as act,mm.Slab_Rate from (select tspl_slab_range_detail.trans_id,tspl_slab_range_detail.Slab_Upto,tspl_slab_range_detail.Slab_Rate,sum(isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) - isnull(tspl_slab_range_detail.Slab_Upto,0)) over (partition by trans_id order by slab_upto) as diff from tspl_slab_range_detail where tspl_slab_range_detail.form_id='PTV-MST' and tspl_slab_range_detail.Trans_ID=TSPL_Primary_Vehicle_Master.Vehicle_Code group by tspl_slab_range_detail.trans_id,tspl_slab_range_detail.Slab_Upto,tspl_slab_range_detail.Slab_Rate)mm)mmf) end as slab_range_amt, "

        qry += " select final.*,TSPL_VENDOR_MASTER.VSP_Payee_Name as [Account Holder Name],convert(decimal(18,2),(isnull(final.[Gross Amount],0) -isnull(final.[TDS 20%],0) -isnull(final.[Material Sale],0)+isnull(final.[Material Sale Return],0) -isnull(final.[Item Issue],0)+isnull(final.[CrNote],0) -isnull(final.[DrNote],0)))  as [Net Payment],round(isnull(final.[Gross Amount],0)/nullif(final.[Total Milk Qty Kg],0),3) as [Transport Cost/Kg],final.VehicleNo as Vehicle from(" &
         "select convert(varchar,xx.DOC_DATE,103) as [DOC DATE] , xx.transporter_code as [Transporter Code],xx.Transp_Name as [Transporter Name],xx.Account_No as [Account No],xx.IFSC_Code as [IFSC Code],xx.bank_name as [Bank Name],xx.branch_name as [Branch Name],xx.MCC_Code as [MCC Code]," &
         " xx.MCC_NAME as [MCC Name], xx.Route_Code as [Route Code],xx.Route_Name as [Route Name],xx.Pan_No  as [PAN No] ,xx.Transp_vehicleNo as [Transporter Vehicle No] ,  " & ' " + IIf(ApplyCalculationOnRouteLenth = True, " ,[Provision Code],[Provision Date] ", "") + "    
         " xx.qty as [Total Milk Qty],xx.qty_kg as [Total Milk Qty Kg],   Total_VLC as [Total VLC],xx.Stand_Per_Day_km as [KM Per Shift],xx.Stand_Total_Shift as [Total Shifts]," &
         " xx.Standard_Total_Km as [Standard Total Km],  xx.mileage_Km_Ltr as [Mileage (Km/Ltr)],xx.Diesel_consume_ltr as [Diesel Consume (Ltr)],xx.Shift_Charges,xx.[Total Shift Charges],xx.slab_range_amt,xx.[Total SlabRange Amt]," &
         " xx.Diesel_Rate as [Diesel rate],(xx.Diesel_consume_ltr*xx.Diesel_Rate) as [Diesel Amount], xx.Rental_Month as [Total Monthly Rent]," &
         " xx.No_Days_In_month as [No. of days in Month],xx.countVehicle as [No of Days Vehicle Run] ,convert(decimal(18,2),(xx.Rental_Month/xx.No_Days_In_month))*xx.countVehicle as [Rent Amount],xx.[Rate/Ltr],xx.[Ltr Amount],xx.[Rate/Km], xx.[Km Amount], "
        If ApplyCalculationOnRouteLenth Then
            qry += " ProvisionAmt as [Gross Amount],"
        Else
            qry += " isnull(convert(decimal(18,2),isnull(xx.[Total Shift Charges],0)   " + IIf(ApplyCalculationOnRouteLenth = True, " +  isnull (xx.Temp_Rental_Amount,0) ", " ") + "  +isnull(xx.[total slabrange amt],0)+isnull(xx.[Ltr Amount],0)+isnull(xx.[Km Amount],0)+(isnull(xx.Diesel_consume_ltr,0)*isnull(xx.Diesel_Rate,0))+((isnull(xx.Rental_Month,0)/isnull(xx.No_Days_In_month,0))*isnull(xx.countVehicle,0))),0) as [Gross Amount],  "
        End If
        qry += " case when Len_Pan_no  =10 then 0 else (isnull(xx.Diesel_consume_ltr,0)*isnull(xx.Diesel_Rate,0))+((isnull(xx.Rental_Month,0)/isnull(xx.No_Days_In_month,0))*isnull(xx.countVehicle,0))*0.2 end as [TDS 20%],  " &
       "  isnull(Matsalequery.MaterialSale_Amt,0) as [Material Sale],isnull(MaterialSaleRetQry.MaterialSale_Return,0) as [Material Sale Return],isnull(Item_IssueQuery.Item_Issue_Amt ,0) as [Item Issue],Other_Deduction as [Other Deduction],isnull(xx .DrNote,0) AS DrNote,isnull(xx .CrNote,0) AS CrNote,xx.VehicleNo  " &
       " from (
        select xxx.*,TSPL_PROVISION_ENTRY.Amount as ProvisionAmt from ( 
        select Doc_Date,transporter_code,max(transporter_Name ) as Transp_Name,max(Account_No) as Account_No ,max(IFSC_Code) as IFSC_Code,max(bank_name) as bank_name,max(branch_name) as branch_name ,MCC_Code,max(MCC_NAME) as MCC_NAME  ,ROUTE_CODE,max(Route_Name)  as Route_Name," &
          " max(Pan)as Pan_No,max(len(PAN ))as Len_Pan_no,Transp_vehicleNo,sum(qty) as qty,sum(isnull(qty_kg,0)) as qty_kg,count(distinct Total_VLC )as Total_VLC" &
          " ,max(Stand_Per_Day_km ) as Stand_Per_Day_km,count(distinct Stand_Total_Shift ) as Stand_Total_Shift,max(Stand_Per_Day_km )*Count(distinct Stand_Total_Shift ) as Standard_Total_Km, max (Temp_Rental_Amount) as Temp_Rental_Amount  " + IIf(ApplyCalculationOnRouteLenth = True, " , max ([Provision Code]) as [Provision Code],max ([Provision Date]) as [Provision Date] ", "") + " , " &
          " max(Avg_Km_Ltr ) as mileage_Km_Ltr,case when max(Avg_Km_Ltr )=0 then max(Avg_Km_Ltr ) else  max(Stand_Per_Day_km )*count(distinct Stand_Total_Shift )/max(Avg_Km_Ltr ) end as Diesel_consume_ltr ," &
          " max(Diesel_Rate) as Diesel_Rate,max(Rental_Month ) as Rental_Month,Shift_Charges,slab_range_amt,count(distinct stand_total_shift) * slab_range_amt as [Total SlabRange Amt],max(No_Days_In_month ) as No_Days_In_month ,sum(countVehicle ) as Vehicle,max(countVehicle ) as countVehicle" &
          " ,0 as Material_sale,isnull(max(Other_Deduction),0) as Other_Deduction,isnull(max(DrNote),0) as DrNote,isnull(max(CrNote),0) as CrNote,max([Ltr Amount]) as [Rate/Ltr],sum(Qty)*max([Ltr Amount]) as [Ltr Amount],max([Rate/Km]) as [Rate/Km],max(Stand_Per_Day_km)*count(distinct Stand_Total_Shift)*max([Rate/Km]) as [Km Amount],count(distinct Stand_Total_Shift)*Shift_Charges as [Total Shift Charges],VehicleNo" &
          " from (" &
          " select TSPL_Primary_Vehicle_Master.Vendor_Code as transporter_code,TSPL_VENDOR_MASTER.Vendor_Name as transporter_Name,TSPL_VENDOR_MASTER .Account_No ,TSPL_VENDOR_MASTER.IFSC_Code,TSPL_Vendor_Bank_MASTER.bank_name,TSPL_Vendor_Bank_Branch_Details.branch_name," &
          " " + SlabRangeqry + " " &
          " TSPL_Primary_Vehicle_Master.MCC_Code,TSPL_MCC_MASTER .MCC_NAME ,TSPL_MCC_ROUTE_MASTER.ROUTE_CODE ,TSPL_MCC_ROUTE_MASTER.Route_Name, " &
          " TSPL_VENDOR_MASTER.PAN  ,isnull(TSPL_Primary_Vehicle_Master.Shift_Charges,0) as Shift_Charges,TSPL_Primary_Vehicle_Master.Vehicle_Code as Transp_vehicleNo," &
          " isnull(TSPL_MILK_RECEIPT_DETAIL.MILK_WEIGHT,0) as Qty,isnull(TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT,0) as qty_kg, TSPL_MILK_RECEIPT_DETAIL.VLC_CODE   as Total_VLC, " + IIf(ApplyCalculationOnRouteLenth = True, " isnull (TSPL_MILK_Shift_End_Route_DETAIL.Actual_KM,0) ", " isnull(TSPL_MCC_ROUTE_MASTER.KiloMeter,0) ") + "  as Stand_Per_Day_km ,isnull (TSPL_Primary_Vehicle_Master.Rental_Amount,0) as  Temp_Rental_Amount  " + IIf(ApplyCalculationOnRouteLenth = True, " , TSPL_PROVISION_ENTRY.Doc_No as [Provision Code], convert (varchar, TSPL_PROVISION_ENTRY.Doc_Date,103) as [Provision Date] ", "") + " ," &
          " TSPL_MILK_RECEIPT_HEAD.shift as Stand_Total_Shift,isnull(TSPL_MILK_Shift_End_Route_DETAIL.Total_KM,0) as Total_KM ,isnull(TSPL_Primary_Vehicle_Master.Avg_Km_Ltr,0) as Avg_Km_Ltr," &
          " isnull(TSPL_Primary_Vehicle_Master.Diesel_Rate,0) as Diesel_Rate , " &
          " isnull((case when TSPL_Primary_Vehicle_Master.Status='Rental/Diesel' then  TSPL_Primary_Vehicle_Master.Rental_Amount " &
          " when TSPL_Primary_Vehicle_Master.Status='Rental' then " &
          " (case when TSPL_Primary_Vehicle_Master.Rental_Type='Day' then TSPL_Primary_Vehicle_Master.Rental_Amount*DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,TSPL_MILK_RECEIPT_HEAD.DOC_DATE),0))) " &
          " when TSPL_Primary_Vehicle_Master.Rental_Type='Month' then TSPL_Primary_Vehicle_Master.Rental_Amount " &
          " when TSPL_Primary_Vehicle_Master.Rental_Type='Year' then TSPL_Primary_Vehicle_Master.Rental_Amount/12.00 else 0 end ) " &
          " end),0) as Rental_Month ," &
          " DAY(DATEADD(DD,-1,DATEADD(MM,DATEDIFF(MM,-1,TSPL_MILK_RECEIPT_HEAD.DOC_DATE),0))) as No_Days_In_month,TSPL_Primary_Vehicle_Master.Rate_Type as [Rate/Ltr],TSPL_Primary_Vehicle_Master.Price_Ltr_KG as [Ltr Amount],TSPL_Primary_Vehicle_Master.Price_KM as [Rate/Km] " &
          " , convert(date,TSPL_MILK_RECEIPT_head.DOC_DATE,103) as Doc_Date,  " &
          " (select distinct case when coalesce(VEHICLE_CODE,'')='' then 0 else 1 end as Vehicle from TSPL_MILK_RECEIPT_DETAIL inner join TSPL_MILK_RECEIPT_HEAD on TSPL_MILK_RECEIPT_HEAD.DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE" &
          " where (TSPL_MILK_RECEIPT_HEAD.doc_date between '" + fromDate + "' and '" + Todate + "') and VEHICLE_CODE=TSPL_Primary_Vehicle_Master.Vehicle_Code) " &
          " as countVehicle," &
          " TSPL_VENDOR_INVOICE_HEAD.Total_Amt as Other_Deduction,TSPL_VENDOR_INVOICE_HEAD.DrNote,TSPL_VENDOR_INVOICE_HEAD.CrNote,TSPL_Primary_Vehicle_Master.Vehicle as VehicleNo  from TSPL_Primary_Vehicle_Master " &
          " left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code and Form_Type='PTM' " &
          " left outer join TSPL_Vendor_Bank_MASTER on TSPL_Vendor_Bank_MASTER.bank_code=tspl_vendor_master.bank_code left outer join (Select distinct * from TSPL_Vendor_Bank_Branch_Details ) TSPL_Vendor_Bank_Branch_Details on TSPL_Vendor_Bank_Branch_Details.bank_code=tspl_vendor_master.bank_code and TSPL_Vendor_Bank_Branch_Details.bank_ifsc_code=tspl_vendor_master.branch_code and TSPL_Vendor_Bank_Branch_Details.Branch_Name= tspl_vendor_master.Branch_Name  " &
          " left join TSPL_MILK_RECEIPT_DETAIL on TSPL_MILK_RECEIPT_DETAIL.VEHICLE_CODE =TSPL_Primary_Vehicle_Master.Vehicle_Code  and TSPL_Primary_Vehicle_Master.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code" &
          " left join TSPL_MILK_RECEIPT_head on TSPL_MILK_RECEIPT_head.DOC_CODE  =TSPL_MILK_RECEIPT_DETAIL.DOC_CODE  " &
          " Left join TSPL_MILK_Shift_End_HEAD on TSPL_MILK_Shift_End_HEAD.MCC_CODE=TSPL_MILK_RECEIPT_head.MCC_CODE and  TSPL_MILK_Shift_End_HEAD.shift=TSPL_MILK_RECEIPT_head.shift and convert(date, TSPL_MILK_Shift_End_HEAD.doc_date,103)=convert(date, TSPL_MILK_RECEIPT_head.doc_date  ,103) " &
          " left join TSPL_MILK_Shift_End_Route_DETAIL on TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE =TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE  and TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE=TSPL_MILK_Shift_End_HEAD.DOC_CODE " &
          " " + IIf(ApplyCalculationOnRouteLenth = True, " left outer join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Ref_Doc_No = TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE  and TSPL_PROVISION_ENTRY.Vendor_Code = TSPL_Primary_Vehicle_Master.Vendor_Code and TSPL_PROVISION_ENTRY.Route_Code = TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE ", "") + " " &
          " left join TSPL_MCC_MASTER on tspl_mcc_master .MCC_Code =TSPL_Primary_Vehicle_Master.MCC_Code " &
          " left join TSPL_MCC_ROUTE_MASTER  on  TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE" &
          " left join (select sum(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then 0 when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then 0 else TSPL_VENDOR_INVOICE_HEAD.Document_Total end) as Total_Amt ,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) as Document_Date,sum(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='D' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end)as DrNote,sum(case when TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' then TSPL_VENDOR_INVOICE_HEAD.Document_Total else 0 end)as CrNote  " &
          " from TSPL_VENDOR_INVOICE_HEAD  /*left join TSPL_Primary_Vehicle_Master on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code =TSPL_Primary_Vehicle_Master.Vendor_Code and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) between convert(date,('" + txtFromDate.Value + "'),103) and convert(date,('" + txtToDate.Value + "'),103) */ " &
          " LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code where  " &
          " convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date ,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103) <=convert(date,('" + txtToDate.Value + "'),103)" &
          " AND ISNULL(TSPL_VENDOR_MASTER.Form_Type,'') ='PTM' group by TSPL_VENDOR_INVOICE_HEAD.Vendor_Code ,convert(date,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103))  as TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_Primary_Vehicle_Master.Vendor_Code" &
          " and  convert(date,TSPL_MILK_RECEIPT_head.DOC_DATE,103) =convert(date,TSPL_VENDOR_INVOICE_HEAD.Document_Date,103)" &
          ")xx  where 2=2  and DOC_DATE >='" + fromDate + "' and DOC_DATE <='" + Todate + "'  "

        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count > 0 Then
            qry += " and MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        Else
            qry += " and mcc_code in (select location_code from tspl_location_master where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' "
            If clsCommon.myLen(arrLoc) > 0 Then
                qry += " and Location_Code in (" + arrLoc + ") "
            End If
            qry += " ) "
        End If
        If ChkSelectRoute.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
            qry += " and route_code  IN (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If
        If ChkSelectTransporter.IsChecked AndAlso cbgTransporter.CheckedValue.Count > 0 Then
            qry += " and transporter_code  IN (" + clsCommon.GetMulcallString(cbgTransporter.CheckedValue) + ") "
        End If
        qry += "  group by Doc_Date ,transporter_code ,MCC_Code ,ROUTE_CODE ,Transp_vehicleNo,Shift_Charges,slab_range_amt,vehicleNo
  )XXX left outer join ( select   Route_Code,Doc_Date,sum(Amount) as Amount  from (
select TSPL_PROVISION_ENTRY.Doc_No,TSPL_PROVISION_ENTRY.Route_Code,convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) as Doc_Date,TSPL_PROVISION_ENTRY.Amount
from TSPL_PROVISION_ENTRY 
where TSPL_PROVISION_ENTRY.Prog_Code='M-Shift_End' and convert(date, TSPL_PROVISION_ENTRY.Doc_Date,103)>= '" + fromDate + "' and convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) <='" + Todate + "' "
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count > 0 Then
            qry += " and TSPL_PROVISION_ENTRY.Loc_Code in (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        Else
            qry += " and TSPL_PROVISION_ENTRY.Loc_Code in (select location_code from tspl_location_master where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' "
            If clsCommon.myLen(arrLoc) > 0 Then
                qry += " and Location_Code in (" + arrLoc + ") "
            End If
            qry += " ) "
        End If
        If ChkSelectRoute.IsChecked AndAlso cbgRoute.CheckedValue.Count > 0 Then
            qry += " and TSPL_PROVISION_ENTRY.Route_Code IN (" + clsCommon.GetMulcallString(cbgRoute.CheckedValue) + ") "
        End If
        If ChkSelectTransporter.IsChecked AndAlso cbgTransporter.CheckedValue.Count > 0 Then
            qry += " and TSPL_PROVISION_ENTRY.Vendor_Code  IN (" + clsCommon.GetMulcallString(cbgTransporter.CheckedValue) + ") "
        End If
        qry += ")x group by Doc_Date,Route_Code
) as TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Doc_Date=convert(date,XXX.Doc_Date,103) and TSPL_PROVISION_ENTRY.Route_code=XXX.ROUTE_CODE 
)xx " &
            " lEFT jOIN (select isnull(sum(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt),0) as MaterialSale_Amt,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) AS Document_Date " &
            " from TSPL_SD_SALE_INVOICE_HEAD left join TSPL_Primary_Vehicle_Master on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code =TSPL_Primary_Vehicle_Master.Vendor_Code where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_Primary_Vehicle_Master.Vendor_Code and" &
            " TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='MCC' group by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,CONVERT(DATE,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) ) AS Matsalequery on xx.transporter_code=Matsalequery.Customer_Code and convert(date,xx.Doc_Date ,103)=Matsalequery.Document_Date " &
            " left join (select sum(TSPL_SD_SALE_RETURN_HEAD.Total_Amt) as MaterialSale_Return," &
            " CONVERT(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) as Document_Date,TSPL_SD_SALE_RETURN_HEAD.Customer_Code " &
            " from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_RETURN_HEAD.Customer_Code left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE =TSPL_SD_SALE_RETURN_HEAD.Document_Code Left Outer Join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No =TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code Where 2=2  and  TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' and TSPL_SD_SHIPMENT_HEAD.Trans_Type='MCC' and TSPL_SD_SALE_RETURN_HEAD.Status=1 " &
            " group by CONVERT(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103),TSPL_SD_SALE_RETURN_HEAD.Customer_Code) as MaterialSaleRetQry on xx.transporter_code=MaterialSaleRetQry.Customer_Code and convert(date,xx.Doc_Date ,103)=MaterialSaleRetQry.Document_Date" &
            " left join (select isnull(sum(TSPL_VSPItem_HEAD.Doc_Amt ),0) as Item_Issue_Amt,TSPL_VSPItem_HEAD.Issue_To as Vendor_Code,CONVERT(DATE,TSPL_VSPItem_HEAD.Doc_Date  ,103) AS Document_Date from TSPL_VSPItem_HEAD left join TSPL_Primary_Vehicle_Master on TSPL_VSPItem_HEAD.Issue_To  =TSPL_Primary_Vehicle_Master.Vendor_Code where TSPL_VSPItem_HEAD.Issue_To =TSPL_Primary_Vehicle_Master.Vendor_Code and TSPL_VSPItem_HEAD.Doc_Type  ='Issue' group by TSPL_VSPItem_HEAD.Issue_To ,CONVERT(DATE,TSPL_VSPItem_HEAD.Doc_Date ,103) ) As Item_IssueQuery on xx.transporter_code=Item_IssueQuery.Vendor_Code  and convert(date,xx.Doc_Date ,103)=Item_IssueQuery.Document_Date" &
            " ) final left outer join TSPL_VENDOR_MASTER   on final.[Transporter Code]  =TSPL_Vendor_MASTER.Vendor_Code   "

        If chkDetail.IsChecked Then
            Qry1 = "" & qry & " order by [DOC DATE]"
        ElseIf chkSummary.IsChecked Then
            Qry1 = "select '" & clsCommon.GetPrintDate(fromDate, "dd/MM/yyyy") & "' as [From Date],'" & clsCommon.GetPrintDate(Todate, "dd/MM/yyyy") & "' as [To Date],summary.[Transporter Code] ,max(summary .[Transporter Name]) as [Transporter Name],max([Account Holder Name]) as [Account Holder Name],max(summary .[Account No]) as [Account No],max(summary.[IFSC Code]) as [IFSC Code],max(summary.[bank name]) as [Bank Name],max(summary.[branch name]) as [Branch Name]  ,'' as [Doc Date],"
            Qry1 += "  summary .[MCC Code] ,max(summary .[MCC Name]) as [MCC Name] ,summary.[Route Code] ,max(summary .[Route Name]) as [Route Name] ,max(summary .[PAN No]) as [PAN No] ,"
            Qry1 += "  max(summary .[Transporter Vehicle No]) as [Transporter Vehicle No] ,sum(summary .[Total Milk Qty]) as [Total Milk Qty],sum(summary.[Total Milk Qty Kg]) as [Total Milk Qty Kg] ,max(summary .[Total VLC]) as [Total VLC],"
            Qry1 += "  max(summary .[KM Per Shift]) as [KM Per Shift] ,sum(summary.[Total Shifts]) as [Total Shifts] ,"
            Qry1 += "  max(summary .[KM Per Shift])*sum(summary.[Total Shifts]) as [Standard Total Km] ,max(summary .[Mileage (Km/Ltr)]) as [Mileage (Km/Ltr)],"
            Qry1 += "  sum(summary.[Diesel Consume (Ltr)]) as [Diesel Consume (Ltr)],max(Shift_Charges) as Shift_Charges,sum([Total Shift Charges]) as [Total Shift Charges],sum(summary.[Total SlabRange Amt]) as [Total SlabRange Amt],max(summary.[Diesel rate]) as [Diesel rate],sum(summary.[Diesel Amount]) as [Diesel Amount] ,"
            Qry1 += "  max(summary.[Total Monthly Rent]) as [Total Monthly Rent],max([No. of days in Month]) as [No. of days in Month],sum(summary.[No of Days Vehicle Run]) as [No of Days Vehicle Run],"
            Qry1 += "  max(summary .[Rent Amount]) as [Rent Amount],max(summary.[Rate/Ltr]) as [Rate/Ltr],sum(summary.[Ltr Amount]) as [Ltr Amount],max(summary.[Rate/Km]) as [Rate/Km], sum(summary.[Km Amount]) as [Km Amount],sum(summary.[Gross Amount]) as [Gross Amount] ,sum(summary.[TDS 20%]) as [TDS 20%] ,"
            Qry1 += "  sum(summary.[Material Sale]) as [Material Sale],sum(summary.[Material Sale Return]) as [Material Sale Return],sum(summary .[Item Issue]) as [Item Issue],sum(summary .[Other Deduction]) as [Other Deduction],sum(summary .DrNote) as DrNote ,sum(summary .CrNote) as CrNote,"
            Qry1 += "  sum(summary.[Net Payment]) as [Net Payment],round(isnull(sum(summary.[Gross Amount]),0)/nullif (isnull(sum(summary.[Total Milk Qty Kg]),0),0),3) as [Transport Cost/Kg],max(summary.Vehicle) as Vehicle  "

            Qry1 += "  from( " & qry & ""
            Qry1 += " )summary group by [Transporter Code] ,[MCC Code] ,[Route Code],[Transporter Vehicle No] "
        End If


        dt = clsDBFuncationality.GetDataTable(Qry1)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = dt
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        FormatGrid()
        If btnReferesh = False Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "crptMemberPaymentSlip", "Member Payment Slip", "")
            frmCRV = Nothing
        End If
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
        gv.BestFitColumns()
        ReStoreGridLayout()
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
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPrimaryTransporter & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Report Type : " + IIf(chkSummary.IsChecked = True, "Summary", "Detail"))
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
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.", Me.Text)
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Primary Transporter Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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

    Private Sub RptPrimaryTransporter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            LOCATIONRIGTHS()
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
            ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
            RadPageView1.SelectedPage = RadPageViewPage1
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            ApplyCalculationOnRouteLenth = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCalculationOnRouteLenth, clsFixedParameterCode.ApplyCalculationOnRouteLenth, Nothing)) = 1, True, False)
            ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, clsFixedParameterCode.ShowVehicleNoSeparatelyInPrimaryTransVehicleMaster, Nothing)) > 0, True, False)
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RptPrimaryTransporter_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
            PageSetupReport_ID = MyBase.Form_ID + IIf(chkSummary.IsChecked = True, "S", "D")
            TemplateGridview = gv
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
                If chkActualDistance.IsChecked Then
                    LoadData("UDL")
                Else
                    LoadData()
                End If
            Else
                LoadData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
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
