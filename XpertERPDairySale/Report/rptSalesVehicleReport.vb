Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
' Ticket No : ERO/27/05/19-000624 By Prabhakar - Create New report 
Public Class rptSalesVehicleReport
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isShowEmployeeCurrentSalary As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()

    Public FilterON As Boolean = False
    Public FilterfromDate As Date
    Public FilterToDate As Date
    Public FilterArrRoute As ArrayList
    Public FilterArrZone As ArrayList
    Public FilterArrVehicleBrand As ArrayList
    Public FilterProvisionEntry As Boolean
#End Region


    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()
        If FilterON Then
            txtFromDate.Value = FilterfromDate
            txtToDate.Value = FilterToDate
            rdbProvisionEntryDate.Checked = FilterProvisionEntry
            rdbGatePassDate.Checked = Not FilterProvisionEntry
            txtVehicleBrand.arrValueMember = FilterArrVehicleBrand
            txtZone.arrValueMember = FilterArrZone
            txtRoute.arrValueMember = FilterArrRoute
            btnGo.PerformClick()
        End If
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try

            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                clsCommon.MyMessageBoxShow("To Date cant be less than from date", Me.Text)
                Exit Sub
            End If

            Dim qry As String = Nothing
            'Ticket No-ERO/02/08/19-000979 ,add 2 more column -Dairy gate pass no,Dairy gate pass date
            qry = " select "
            If chkLinkedwithShipment.Checked = True Then
                qry += " TSPL_SD_SHIPMENT_HEAD.Zone_Code, "
            End If
            qry += " ISNULL(TSPL_VEHICLE_MASTER.Vehicle_Brand,'') as Vehicle_Brand,TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No as [Route Code],TSPL_ROUTE_MASTER.Route_Desc as [Route Desc],TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode as [Gate Pass No] ,Convert ( varchar,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,113) as [Gate Pass Date],Convert ( varchar,TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Date,113) as [Vehicle In Time], TSPL_PROVISION_ENTRY .Doc_No as [Document code],Convert ( varchar, TSPL_PROVISION_ENTRY.Doc_Date,103) as Date, TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id  as [Vehicle Id], TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number as [Vehicle Number],(case when TSPL_VEHICLE_MASTER.Vehicle_Type='H' then 'Hire' when TSPL_VEHICLE_MASTER.Vehicle_Type='D' then 'Depot' else '' end) as [Vehicle Type H/D],TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter as [Transporter Id] , TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Name],TSPL_VEHICLE_MASTER.Model as [Vehicle Model],TSPL_DAIRYSALE_GATEPASS_MASTER.Opening_Km as [Opening KM] , TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km as [Closing KM],TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route as [Fixed KM] ,isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0) as [Running KM] , case when isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route,0) <  (isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0))  then  isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route,0) else (isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0))  end as  [Passing KM], TSPL_DAIRYSALE_GATEPASS_MASTER.Price_KM_In_Vehicle as [Rate/KM],(TSPL_PROVISION_ENTRY.Amount - isnull (TSPL_PROVISION_ENTRY.Toll_Amt,0)) as  [Transport Amount],isnull (TSPL_PROVISION_ENTRY.Toll_Amt,0) as [Toll/Weight Amount],TSPL_PROVISION_ENTRY.Amount as [Total Amount], TBL_LTR_CONV.Ltr_Qty as [Sales Milk/Ltr], TSPL_VEHICLE_MASTER.CrateCapacity as [Vehicle Capacity] ,TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate as [Vehicle Out Crate], Convert (decimal(18,2), ( (isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate,0)*100)/ nullif (TSPL_VEHICLE_MASTER.CrateCapacity,0))) as [In %] from "
            If rdbGatePassDate.Checked = True Then
                'Show all gatepass either provision exist or not
                qry += "   TSPL_DAIRYSALE_GATEPASS_MASTER left join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Ref_Doc_No = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode "
            Else
                qry += "   TSPL_PROVISION_ENTRY  inner join TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_PROVISION_ENTRY.Ref_Doc_No = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode "
            End If
            If chkLinkedwithShipment.Checked = True Then
                qry += "  inner join (SELECT SUM(isnull(TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount,0)) AS Amount_Less_Discount,TSPL_SD_SHIPMENT_HEAD.GPCode  ,max(tspl_customer_master.Zone_Code) as Zone_Code   FROM TSPL_SD_SHIPMENT_HEAD    Left Join tspl_customer_master on tspl_customer_master.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code   where isnull(TSPL_SD_SHIPMENT_HEAD.GPCode,'')<>''   Group BY TSPL_SD_SHIPMENT_HEAD.GPCode) TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.GPCode=TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode "
            End If

            qry += " left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter " &
                  " left Outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_id = TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id " &
                  " left Outer Join TSPL_ROUTE_MASTER on  TSPL_ROUTE_MASTER.Route_No = TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No " &
                  "  left outer  join  (select TSPL_DAIRYSALE_GATEPASS_Detail.GPCode ,sum (convert(decimal(18,2),(TSPL_DAIRYSALE_GATEPASS_Detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)) as Ltr_Qty from TSPL_DAIRYSALE_GATEPASS_MASTER " &
                                       " left join TSPL_DAIRYSALE_GATEPASS_Detail on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_Detail.GPCode " &
                                       " left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code " &
                                       " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code " &
                                       " left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code " &
                                       " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code and 	CurrentUnit.uom_code=	TSPL_DAIRYSALE_GATEPASS_Detail.unit_code " &
                                       " where  tspl_unit_master.Ltr_type ='Y' and StockUnit.stocking_unit='Y'  group by TSPL_DAIRYSALE_GATEPASS_Detail.GPCode ) TBL_LTR_CONV on  TBL_LTR_CONV.GPCode = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode "
            qry += " where 2=2 "
            If rdbGatePassDate.Checked = True Then
                qry += " and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            Else
                qry += " and convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            End If

            ' Ticket No : ERO/19/08/19-000995 By Prabhakar
            If txtVehicleNo.arrValueMember IsNot Nothing AndAlso txtVehicleNo.arrValueMember.Count > 0 Then
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id in (" + clsCommon.GetMulcallString(txtVehicleNo.arrValueMember) + ")  "
            End If

            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter in (" + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ")  "
            End If
            If txtVehicleBrand.arrValueMember IsNot Nothing AndAlso txtVehicleBrand.arrValueMember.Count > 0 Then
                qry += " and TSPL_VEHICLE_MASTER.Vehicle_Brand in (" + clsCommon.GetMulcallString(txtVehicleBrand.arrValueMember) + ")  "
            End If
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "
            End If
            If chkLinkedwithShipment.Checked = True Then
                If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SD_SHIPMENT_HEAD.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ")  "
                End If
            End If

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
            ' FormatGrid()
            gv1.BestFitColumns()

            ReStoreGridLayout()


        Catch ex As Exception

        End Try
    End Sub
    Sub FormatGrid()
        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Document code").IsVisible = True
        gv1.Columns("Document code").Width = 100
        gv1.Columns("Document code").HeaderText = "Document Code"



        gv1.Columns("Date").IsVisible = True
        gv1.Columns("Date").Width = 100
        gv1.Columns("Date").HeaderText = " Date"


        gv1.Columns("Vehicle Id").IsVisible = True
        gv1.Columns("Vehicle Id").Width = 100
        gv1.Columns("Vehicle Id").HeaderText = "Vehicle Id"

        gv1.Columns("Vehicle Number").IsVisible = True
        gv1.Columns("Vehicle Number").Width = 150
        gv1.Columns("Vehicle Number").HeaderText = "Vehicle Number"



        gv1.Columns("Transporter Id").IsVisible = True
        gv1.Columns("Transporter Id").Width = 100
        gv1.Columns("Transporter Id").HeaderText = "Transporter Id"

        gv1.Columns("Transporter Name").IsVisible = True
        gv1.Columns("Transporter Name").Width = 100
        gv1.Columns("Transporter Name").HeaderText = "Transporter Name"

        gv1.Columns("Fixed KM").IsVisible = True
        gv1.Columns("Fixed KM").Width = 100
        gv1.Columns("Fixed KM").HeaderText = "Fixed KM"

        gv1.Columns("Running KM").IsVisible = True
        gv1.Columns("Running KM").Width = 100
        gv1.Columns("Running KM").HeaderText = "Running KM"

        gv1.Columns("Running / Fixed Which ever is less").IsVisible = True
        gv1.Columns("Running / Fixed Which ever is less").Width = 100
        gv1.Columns("Running / Fixed Which ever is less").HeaderText = "Running / Fixed Which ever is less"

        gv1.Columns("Rate/KM").IsVisible = True
        gv1.Columns("Rate/KM").Width = 100
        gv1.Columns("Rate/KM").HeaderText = "Rate/KM"

        gv1.Columns("Amount").IsVisible = True
        gv1.Columns("Amount").Width = 100
        gv1.Columns("Amount").HeaderText = "Amount"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

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
            gv1.Columns(ii).BestFit()
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim TransportAmount As New GridViewSummaryItem("Transport Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TransportAmount)
        Dim TotalAmount As New GridViewSummaryItem("Total Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmount)
        Dim SalesMilkLtr As New GridViewSummaryItem("Sales Milk/Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SalesMilkLtr)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub Reset()
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtVehicleNo.arrValueMember = Nothing
        txtTransporter.arrValueMember = Nothing
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(chkLinkedwithShipment.Checked, "Shipment", "")
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Sales Vehicle Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                'End If


                'If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Sales Vehicle Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                'End If

                'If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Sales Vehicle Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVehicleNo__My_Click(sender As Object, e As EventArgs) Handles txtVehicleNo._My_Click
        Dim qry As String = " Select TSPL_VEHICLE_MASTER.Vehicle_Id as [Code] ,TSPL_VEHICLE_MASTER.Number as [Name] from TSPL_VEHICLE_MASTER "
        txtVehicleNo.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@VehicleNo", qry, "Code", "Name", txtVehicleNo.arrValueMember, txtVehicleNo.arrDispalyMember)
    End Sub
    Private Sub txtTransporter__My_Click(sender As Object, e As EventArgs) Handles txtTransporter._My_Click
        Dim qry As String = " Select TSPL_TRANSPORT_MASTER.Transport_Id as [Code] ,TSPL_TRANSPORT_MASTER.Transporter_Name as [Name] from TSPL_TRANSPORT_MASTER "
        txtTransporter.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@Transporter", qry, "Code", "Name", txtTransporter.arrValueMember, txtTransporter.arrDispalyMember)
    End Sub

    Private Sub txtVehicleBrand__My_Click(sender As Object, e As EventArgs) Handles txtVehicleBrand._My_Click
        Dim qry As String = " Select Vehicle_Brand  from (select Vehicle_Brand  from TSPL_VEHICLE_MASTER group by Vehicle_Brand)x "
        txtVehicleBrand.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@VehBrand", qry, "Vehicle_Brand", "", txtVehicleBrand.arrValueMember, Nothing)
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " select Route_No as Code,Route_Desc as Name  from TSPL_ROUTE_MASTER"
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@Route", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select Zone_Code as Code,Description as Name from TSPL_ZONE_MASTER"
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@Zone", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub chkLinkedwithShipment_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkLinkedwithShipment.ToggleStateChanged
        Try
            If chkLinkedwithShipment.IsChecked = True Then
                txtZone.Visible = True
                MyLabel4.Visible = True
            Else
                txtZone.Visible = False
                MyLabel4.Visible = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
