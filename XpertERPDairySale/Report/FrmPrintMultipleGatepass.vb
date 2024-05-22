Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class FrmPrintMultipleGatepass
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim strQry As String = ""
    'Dim Refresh As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Dim ShowShipToPartyInDairyDispatch As Integer = 0
    Dim AllowSeperateSchemeItemOnPrint As Boolean = False
    Dim strRptPath As String = ""
    Dim ApplyMilkPouchPrint As Boolean = False
    Public Shift As String = ""
    Dim atchqry As String = ""

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmPrintDistributerInvoiceStatement)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtMultLocation.arrValueMember = Nothing
        TxtMultiRoute.arrValueMember = Nothing
        TxtMultiVehicle.arrValueMember = Nothing
        cboShiftType.SelectedValue = ""
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadReportType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "-Select-"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "M"
        dr("Name") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "E"
        dr("Name") = "Evening"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        cboShiftType.DataSource = dt
        cboShiftType.ValueMember = "Code"
        cboShiftType.DisplayMember = "Name"
        cboShiftType.SelectedValue = ""
    End Sub

    Private Sub FrmPrintMultipleGatepass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        LoadReportType()
        Reset()
    End Sub

    Private Sub txtMultLocation__My_Click(sender As Object, e As EventArgs) Handles txtMultLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtMultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultLocation", strQry, "Code", "Name", txtMultLocation.arrValueMember, txtMultLocation.arrDispalyMember)

        'strQuery = "select distinct Bill_To_Location as Code,Location_Desc as Description from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location=TSPL_LOCATION_MASTER.Location_Code"
        'txtLocCode.Value = clsCommon.ShowSelectForm("LocationSegGP", strQuery, "Code", "screen_type='DS'", txtLocCode.Value, "Code", isButtonClicked)
        'txtLocDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocCode.Value & "'")

    End Sub

    Private Sub TxtMultiRoute__My_Click(sender As Object, e As EventArgs) Handles TxtMultiRoute._My_Click
        strQry = " select Route_No as RouteNo ,Route_Desc as RouteDesc from TSPL_ROUTE_MASTER "
        TxtMultiRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMultiRoute", strQry, "RouteNo", "RouteDesc", TxtMultiRoute.arrValueMember, TxtMultiRoute.arrDispalyMember)
    End Sub

    Private Sub TxtMultiVehicle__My_Click(sender As Object, e As EventArgs) Handles TxtMultiVehicle._My_Click
        strQry = " select Vehicle_Id as VehicleNo,Description as VehicleDesc from TSPL_VEHICLE_MASTER "
        TxtMultiVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("txtmultiVehicle", strQry, "VehicleNo", "VehicleDesc", TxtMultiVehicle.arrValueMember, TxtMultiVehicle.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        'TemplateGridview = gv
        loadReport()
    End Sub

    Public Sub loadReport()
        Try
            If txtFromDate.Value > txtToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                txtFromDate.Focus()
                Exit Sub
            End If
            'If clsCommon.CompairString(clsCommon.myCstr(cboShiftType.SelectedValue), "") = CompairStringResult.Equal Then
            '    clsCommon.MyMessageBoxShow(Me, "Please Select ShiftType.", Me.Text)
            '    cboShiftType.Focus()
            '    Exit Sub
            'End If

            Dim qry As String = Nothing
            Dim whrcls As String = Nothing

            qry = " select TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode,convert(varchar,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) as GPDate,
                   TSPL_DAIRYSALE_GATEPASS_MASTER.Location_Code,TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No,tspl_route_master.Route_Desc,
                   TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id,TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number,TSPL_DAIRYSALE_GATEPASS_MASTER.Loading_Slip,
                   TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate from TSPL_DAIRYSALE_GATEPASS_MASTER
                    left outer join tspl_route_master on tspl_route_master.route_no=TSPL_DAIRYSALE_GATEPASS_MASTER.route_no where 2=2 
                    and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103)>=convert(date,('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'),103)
                    and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <=convert(date,('" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),103)"

            If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
            End If
            If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No in (" + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember) + ") "
            End If
            If TxtMultiVehicle.arrValueMember IsNot Nothing AndAlso TxtMultiVehicle.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id in (" + clsCommon.GetMulcallString(TxtMultiVehicle.arrValueMember) + ") "
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboShiftType.SelectedValue), "M") = CompairStringResult.Equal Then
                whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType= 'Morning' "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboShiftType.SelectedValue), "E") = CompairStringResult.Equal Then
                whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType= 'Evening' "
                ' ElseIf clsCommon.CompairString(clsCommon.myCstr(cboShiftType.SelectedValue), "B") = CompairStringResult.Equal Then
            End If

            qry += whrcls

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(qry)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.GroupDescriptors.Clear()
                gv.MasterTemplate.SummaryRowsBottom.Clear()
                FormatGrid()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        gv.Columns("GPCode").IsVisible = True
        gv.Columns("GPCode").Width = 100
        gv.Columns("GPCode").HeaderText = "Document Code"
        gv.Columns("GPCode").ReadOnly = True

        gv.Columns("GPDate").IsVisible = True
        gv.Columns("GPDate").Width = 100
        gv.Columns("GPDate").HeaderText = "Document Date"
        gv.Columns("GPDate").ReadOnly = True

        gv.Columns("Location_Code").IsVisible = True
        gv.Columns("Location_Code").Width = 100
        gv.Columns("Location_Code").HeaderText = "Location"
        gv.Columns("Location_Code").ReadOnly = True

        gv.Columns("Route_No").IsVisible = True
        gv.Columns("Route_No").Width = 100
        gv.Columns("Route_No").HeaderText = "Route No."
        gv.Columns("Route_No").ReadOnly = True

        gv.Columns("Route_Desc").IsVisible = True
        gv.Columns("Route_Desc").Width = 100
        gv.Columns("Route_Desc").HeaderText = "Route Description"
        gv.Columns("Route_Desc").ReadOnly = True

        gv.Columns("Vehicle_Id").IsVisible = True
        gv.Columns("Vehicle_Id").Width = 100
        gv.Columns("Vehicle_Id").HeaderText = "Vehicle Id"
        gv.Columns("Vehicle_Id").ReadOnly = True

        gv.Columns("Vehicle_Number").IsVisible = True
        gv.Columns("Vehicle_Number").Width = 100
        gv.Columns("Vehicle_Number").HeaderText = "Vehicle No."
        gv.Columns("Vehicle_Number").ReadOnly = True

        gv.Columns("Loading_Slip").IsVisible = True
        gv.Columns("Loading_Slip").Width = 100
        gv.Columns("Loading_Slip").HeaderText = "Loading Slip"
        gv.Columns("Loading_Slip").ReadOnly = True

        gv.Columns("TotalCrate").IsVisible = False
        gv.Columns("TotalCrate").Width = 100
        gv.Columns("TotalCrate").HeaderText = "TotalCrate"
        gv.Columns("TotalCrate").ReadOnly = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("TotalCrate", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

    End Sub

    Private Function getattachqry()
        Dim qry As String = ""
        Dim Shifttype As String = ""
        Dim shift As String = ""
        Dim whrcls As String = ""

        If txtMultLocation.arrValueMember IsNot Nothing AndAlso txtMultLocation.arrValueMember.Count > 0 Then
            whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Location_Code in (" + clsCommon.GetMulcallString(txtMultLocation.arrValueMember) + ") "
        End If
        If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
            whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No in (" + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember) + ") "
        End If
        If TxtMultiVehicle.arrValueMember IsNot Nothing AndAlso TxtMultiVehicle.arrValueMember.Count > 0 Then
            whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id in (" + clsCommon.GetMulcallString(TxtMultiVehicle.arrValueMember) + ") "
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboShiftType.SelectedValue), "M") = CompairStringResult.Equal Then
            whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType= 'Morning' "
            'Shifttype += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType= 'Morning' "
            shift += " and TSPL_SD_SHIPMENT_HEAD.Shift_Type= 'AM' "
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboShiftType.SelectedValue), "E") = CompairStringResult.Equal Then
            whrcls += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType= 'Evening' "
            'Shifttype += " and TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType= 'Morning' "
            shift += " and TSPL_SD_SHIPMENT_HEAD.Shift_Type= 'PM' "
            ' ElseIf clsCommon.CompairString(clsCommon.myCstr(cboShiftType.SelectedValue), "B") = CompairStringResult.Equal Then
        End If

        qry = "  Select '" + objCommonVar.CurrentUserCode + "' as UserName, Case When CFinPouch > 0 Then ( ( Final.Crate_Qty * Final.Conversion_Factor )/ CFinPouch ) Else 0 End AS 'NoOfPouch', Case When CFinLTR > 0 Then ( ( ( Final.Crate_Qty * Final.Conversion_Factor + Final.Pouch_Qty ) )/ CFinLTR ) Else 0 End AS 'MilkQuantity', Case When CFinLTR > 0 Then ( ( ( Final.Crate_Qty * Final.Conversion_Factor )+( Final.Pouch_Qty * Final.CFinPouch ) )/ CFinLTR ) Else 0 End AS 'MilkQuantityltr', CAST( ( Final.Box_Crate_Qty * Final.Conversion_Factor ) / CFinKG AS DECIMAL(10, 2) ) AS 'MilkQuantityKG', Case When Final.Column_Crate > 0 Then Cast( ( Final.Crate_Qty / Final.Column_Crate ) AS int ) Else 0 End AS 'CrateLine', Case When Column_Crate > 0 Then cast( ( cast(qty as int)% Column_Crate ) as int ) Else 0 End AS 'LooseCrate', Pouch_Qty AS 'LoosePouch', CAST( CASE WHEN qty > 0 THEN CAST(qty AS decimal(18,2)) / Conversion_FactorCrt END AS decimal(18,2) ) AS CrateQtydd, CASE WHEN Unit_Code='POUCH' then qty*CFinPouch / Conversion_FactorCrt WHEN Unit_Code='LTR' then qty*CFinLTR / Conversion_FactorCrt WHEN Unit_Code='KG' then qty*CFinKG / Conversion_FactorCrt WHEN Unit_Code='CRATE' then qty*Conversion_FactorCrt / Conversion_FactorCrt WHEN Unit_Code='BOX' then qty*CFinBOX / Conversion_FactorCrt ELSE 0 END AS QtyCrate , Final.*, tbl_Brand.Brand, tbl_Brand.BRANDDESC, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Logo_Img2, case when isnull(Final.CFinLTR,0)=0 then Final.Amount/((Final.qty*Final.Conversion_Factor)/Final.CFinKG) else Final.Amount/((Final.qty*Final.Conversion_Factor)/Final.CFinLTR) end as item_Cost FROM 
                   ( Select  max(Supply_Date)as Supply_Date, "
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
            qry += " max(Conversion_FactorCrt) as Conversion_FactorCrt ,MAX(gpUnit) AS gpUnit, "
        Else
            qry += " max(Conversion_FactorCrt) as Conversion_FactorCrt, "
        End If
        qry += " Max(Distributor) Distributor" + IIf(clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal, ",Unit_Code", "") + " , Max(CFinPouch) CFinPouch, Max(CFinLTR) CFinLTR, Max(CFinBOX) CFinBOX, Max(CFinKG) CFinKG, Max(Conversion_Factor) Conversion_Factor,
                 Max(AgainstTransferNo) AgainstTransferNo, Max(Comp_Code) Comp_Code, Sum( Qty * case when Unit_Code = 'Crate' then 1 else 0 end ) Crate_Qty, Sum( Qty * case when Unit_Code = 'Pouch' then 1 else 0 end ) Pouch_Qty, Sum(Box_Crate_Qty) Box_Crate_Qty, Max(Insurance_No) Insurance_No, Max(Insurance_Comp_Name) Insurance_Comp_Name, Max(comp_name) comp_name"
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") <> CompairStringResult.Equal Then
            qry += " ,Max(unit_code) unit_code "
        End If

        qry += "   ,CASE WHEN max(unit_code) = 'crate' OR max(unit_code) = 'pouch' THEN 'Crate/Pouch' ELSE max(unit_code) END AS unit_code_result,
                   Sum(qty) qty, Max(Comp_Address) Comp_Address, Max(Loc_add) Loc_add, Max(Route_No) Route_No, Sum(Totalcrate) Totalcrate, Sum(TotalCan) TotalCan, Max(Route_Desc) Route_Desc, (GPCode) GPCode, Max(GPDate) GPDate, Max(GPTime) GPTime, Max(vehicle_id) vehicle_id, Max(VehicleDesc) VehicleDesc, Max(location_code) location_code, Max(Location_desc) Location_desc, Max(transporter) transporter, Max(remarks) remarks, Max(comments) comments, Max(post) post, Item_code, Max(item_desc) item_desc, Max(short_description) short_description, Max(sku_seq) sku_seq, Max(TranporterNameFromMaster) TranporterNameFromMaster, Max(HSN_Code) HSN_Code, Max(Salesman) Salesman, Sum(Column_Crate) Column_Crate, Max(Area_Code) Area_Code, Max(Zone_Code) Zone_Code, Max(ShiftType) ShiftType, Max(GSTReg_No) GSTReg_No, Max(Loading_Slip) Loading_Slip, Max(DispatchDate) DispatchDate, Max(GatePass_Date) GatePass_Date, Sum(Amount) Amount, sum(AmountWithoutTax) as AmountWithoutTax, Sum(Margin) Margin, sum(SecurityAmt) as SecurityAmt, max(Dist_Commission_Ratewithtax) Dist_Commission_Ratewithtax ,max(RoundOffAmt) as RoundOffAmt , max(ItemCost) as UnitRate, Max(Driver_Name) Driver_Name, Max(Driver_ContactNo) Driver_ContactNo FROM
                   ( select FORMAT( TSPL_DAIRYSALE_GATEPASS_MASTER.Supply_Date, 'dd/MM/yyyy' ) as Supply_Date,"
        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
            qry += " ItemConversionCrate.Conversion_Factor as Conversion_FactorCrt , gpUnit.Conversion_Factor AS gpUnit, "
        Else
            qry += " ItemConversionCrate.Conversion_Factor as Conversion_FactorCrt , "
        End If

        qry += "     TSPL_DAIRYSALE_GATEPASS_MASTER.DistributorName As 'Distributor', ItemConversionInPouch.Conversion_Factor As 'CFinPouch', ItemConversionInLTR.Conversion_Factor AS 'CFinLTR', ItemConversionInKG.Conversion_Factor AS 'CFinKG', ItemConversionInBox.Conversion_Factor AS 'CFinBOX', CurrentUnit.Conversion_Factor, isnull( TSPL_DAIRYSALE_GATEPASS_MASTER.AgainstTransferNo, '' ) as AgainstTransferNo, TSPL_COMPANY_MASTER.Comp_Code, CASE WHEN TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code <> 'Box' THEN convert( decimal(18, 2), ( TSPL_DAIRYSALE_GATEPASS_DETAIL.qty / CrateUnit.conversion_factor )* StockUnit.conversion_factor * CurrentUnit.conversion_factor ) ELSE 0 END as Crate_Qty, CASE WHEN TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code <> 'Crate' and TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code <> 'Pouch' THEN convert( decimal(18, 2), ( TSPL_DAIRYSALE_GATEPASS_DETAIL.qty / CurrentUnit.conversion_factor )* StockUnit.conversion_factor * CurrentUnit.conversion_factor ) else 0 end as Box_Crate_Qty, TSPL_COMPANY_MASTER.Insurance_No, TSPL_COMPANY_MASTER.Insurance_Comp_Name, TSPL_COMPANY_MASTER.comp_name, TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code, TSPL_DAIRYSALE_GATEPASS_DETAIL.qty, TSPL_COMPANY_MASTER.add1 + case when len(TSPL_COMPANY_MASTER.add2)> 0 then ', ' + TSPL_COMPANY_MASTER.add2 else '' end + case when LEN( isnull(TSPL_COMPANY_MASTER.Add3, '') )> 0 then ', ' + isnull(TSPL_COMPANY_MASTER.Add3, '') else ' ' end as Comp_Address, tspl_location_master.add1 + case when len(tspl_location_master.add2)> 0 then ', ' + tspl_location_master.add2 else '' end + case when LEN( isnull(tspl_location_master.Add3, '') )> 0 then ', ' + isnull(tspl_location_master.Add3, '') else ' ' end as Loc_add, TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No, TSPL_DAIRYSALE_GATEPASS_MASTER.Totalcrate, TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCan, tspl_route_master.Route_Desc, TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode, convert( varchar, TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate, 103 ) as GPDate, FORMAT( TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate, 'hh:mm tt' ) as GPTime, TSPL_DAIRYSALE_GATEPASS_MASTER.GatePass_Date, TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id AS vehicle_id, TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number as VehicleDesc, TSPL_DAIRYSALE_GATEPASS_MASTER.location_code, tspl_location_master.Location_desc, TSPL_DAIRYSALE_GATEPASS_MASTER.transporter, TSPL_DAIRYSALE_GATEPASS_MASTER.remarks, TSPL_DAIRYSALE_GATEPASS_MASTER.comments, TSPL_DAIRYSALE_GATEPASS_MASTER.post, TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_code, tspl_item_master.item_desc, tspl_item_master.short_description, tspl_item_master.sku_seq, TSPL_TRANSPORT_MASTER.Transporter_Name as TranporterNameFromMaster, TSPL_DAIRYSALE_GATEPASS_DETAIL.HSN_Code, TSPL_DAIRYSALE_GATEPASS_MASTER.Salesman, tspl_vehicle_master.Column_Crate, TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No AS Area_Code, TSPL_CUSTOMER_MASTER.Zone_Code, TSPL_DAIRYSALE_GATEPASS_MASTER.ShiftType, tspl_company_master.GSTReg_No, TSPL_DAIRYSALE_GATEPASS_MASTER.Loading_Slip,
				   ( Select Max(Document_Date) from TSPL_SD_SHIPMENT_HEAD where GPCode in (select GPCode from TSPL_DAIRYSALE_GATEPASS_MASTER where convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103)>=convert(date,('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <=convert(date,('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'),103) " + whrcls + " "
        qry += " )) AS 'DispatchDate'
		           ,xyz.Amount, xyz.AmountWithoutTax,xyz.Margin, xyz.SecurityAmt, xyz.Dist_Commission_Ratewithtax,xyz.RoundOffAmt, xyz.ItemCost,TSPL_DAIRYSALE_GATEPASS_MASTER.Driver_Name,TSPL_DAIRYSALE_GATEPASS_MASTER.Driver_ContactNo from TSPL_DAIRYSALE_GATEPASS_DETAIL 
				   left outer join TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode 
				   left outer join tspl_vehicle_master on tspl_vehicle_master.Vehicle_id=TSPL_DAIRYSALE_GATEPASS_MASTER.vehicle_id 
				   left outer join tspl_location_master on tspl_location_master.location_code=TSPL_DAIRYSALE_GATEPASS_MASTER.location_code 
				   left outer join tspl_company_master on tspl_company_master.comp_code=TSPL_DAIRYSALE_GATEPASS_MASTER.comp_code  
				   left outer join tspl_item_master on tspl_item_master.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_code  
				   left outer join  TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_VEHICLE_MASTER.Transport_Id 
				   left outer join  tspl_route_master on tspl_route_master.Route_No=TSPL_DAIRYSALE_GATEPASS_MASTER.Route_No 
				   left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code	and StockUnit.stocking_unit='Y'   
				   left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code and 	CurrentUnit.uom_code=	TSPL_DAIRYSALE_GATEPASS_DETAIL.unit_code 
				   left join tspl_item_uom_detail CrateUnit on CrateUnit.item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code  and 	CrateUnit.uom_code=	'Crate' 
				   left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Pouch') as ItemConversionInPouch on ItemConversionInPouch.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                    left join ( select Conversion_factor, TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code = 'Crate' ) as ItemConversionCrate on ItemConversionCrate.Item_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code 
                     left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='LTR') as ItemConversionInLTR on ItemConversionInLTR.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code 
                     left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='Box') as ItemConversionInBox on ItemConversionInBox.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
                     left join (select Conversion_factor,TSPL_ITEM_UOM_DETAIL.Item_code from TSPL_ITEM_UOM_DETAIL where UOM_code='KG') as ItemConversionInKG on ItemConversionInKG.Item_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code "

        If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_vehicle_master.Transport_Id  left join tspl_item_uom_detail gpUnit on gpUnit.item_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.item_code and CrateUnit.uom_code = TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code "
        Else
            qry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=tspl_vehicle_master.Transport_Id  "
        End If

        qry += " left outer join ((select Case When Sum(Isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0))>0 Then  Sum(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt/TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GP_Qty) Else 0 End As Amount,  Case When Sum(Isnull(TSPL_SD_SHIPMENT_DETAIL.Qty, 0))> 0 Then Sum(TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount) Else 0 End As AmountWithoutTax, Sum(IsNull(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Amt,0)/TSPL_SD_SHIPMENT_DETAIL.Qty*TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GP_Qty) AS Margin,sum( IsNull(TSPL_SD_SHIPMENT_DETAIL.Security_Amt,0))as SecurityAmt,TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code,max(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Rate) as Dist_Commission_Ratewithtax,max(TSPL_SD_SHIPMENT_HEAD.RoundOffAmount) as RoundOffAmt , max(TSPL_SD_SHIPMENT_DETAIL.Item_Cost) as ItemCost,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode from TSPL_SD_SHIPMENT_DETAIL   
                       Left Outer Join TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
					   Left Outer Join TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL On TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.PK_ID=TSPL_SD_SHIPMENT_DETAIL.PK_ID
                     WHERE  TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode  in(select GPCode from TSPL_DAIRYSALE_GATEPASS_MASTER where convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103)>=convert(date,('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <=convert(date,('" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),103) " + whrcls + " "
        qry += " )Group By  TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code,TSPL_DAIRYSALE_GATEPASS_SHIPMENT_DETAIL.GPCode)
                     Union All
                     (select Case When Sum(Isnull(TSPL_SD_SHIPMENT_DETAIL.Qty,0))>0 Then  Sum(TSPL_SD_SHIPMENT_DETAIL.Item_Net_Amt) Else 0 End As Amount,  Case When Sum(Isnull(TSPL_SD_SHIPMENT_DETAIL.Qty, 0))> 0 Then Sum(TSPL_SD_SHIPMENT_DETAIL.Amt_Less_Discount) Else 0 End As AmountWithoutTax, 
                     Sum(IsNull(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Amt,0)) AS Margin,sum( IsNull(TSPL_SD_SHIPMENT_DETAIL.Security_Amt,0))as SecurityAmt,TSPL_SD_SHIPMENT_DETAIL.Item_Code ,TSPL_SD_SHIPMENT_DETAIL.Unit_code ,max(TSPL_SD_SHIPMENT_DETAIL.Distributor_Commission_Rate) as Dist_Commission_Ratewithtax,max(TSPL_SD_SHIPMENT_HEAD.RoundOffAmount) as RoundOffAmt , max(TSPL_SD_SHIPMENT_DETAIL.Item_Cost) as ItemCost,TSPL_SD_SHIPMENT_HEAD.GPCode from TSPL_SD_SHIPMENT_DETAIL   
                     Left Outer Join TSPL_SD_SHIPMENT_HEAD ON TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE 
                     WHERE  TSPL_SD_SHIPMENT_HEAD.GPCode  in(select GPCode from TSPL_SD_SHIPMENT_HEAD where convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103)>=convert(date,('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_SD_SHIPMENT_HEAD.Document_Date,103) <=convert(date,('" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),103)" + whrcls + shift + " "
        qry += " )Group By  TSPL_SD_SHIPMENT_DETAIL.Item_Code,TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_HEAD.GPCode)
                     )xyz ON xyz.GPCode=TSPL_DAIRYSALE_GATEPASS_DETAIL.GPCode And xyz.Unit_code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Unit_Code and xyz.Item_Code=TSPL_DAIRYSALE_GATEPASS_DETAIL.Item_Code
					 where 2=2  and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103)>=convert(date,('" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "'),103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <=convert(date,('" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'),103)" + whrcls + " "
        qry += " )As Main Group by Item_code,GPCode" + IIf(clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal, ",Unit_Code", "") + ")AS Final 
					 left outer join  ( select Item_Code,max([CATEGORY RM]) as [CATEGORY RM],max([BRAND]) as [BRAND],max([SUB BRAND]) as [SUB BRAND], 
                    max([DESCRP]) as [DESCRP],max([PACK]) as [PACK], max([PACK SIZE]) as [PACK SIZE],max([CATEGORY OT]) as [CATEGORY OT],max([CATEGORY FA]) as [CATEGORY FA], 
                    max([P TYPE]) as [P TYPE],max([L TYPE]) as [L TYPE],max([JW]) as [JW], max([SCRAP]) as [SCRAP],max([CATEGORY RMDESC]) as [CATEGORY RMDESC],max([BRANDDESC])  as [BRANDDESC],
                   max([SUB BRANDDESC]) as [SUB BRANDDESC],max([DESCRPDESC]) as [DESCRPDESC],max([PACKDESC]) as [PACKDESC], max([PACK SIZEDESC]) as [PACK SIZEDESC],max([CATEGORY OTDESC]) as [CATEGORY OTDESC],
                   max([CATEGORY FADESC]) as [CATEGORY FADESC],max([P TYPEDESC]) as [P TYPEDESC],max([L TYPEDESC]) as [L TYPEDESC],max([JWDESC]) as [JWDESC], max([SCRAPDESC]) as [SCRAPDESC] 
                   from ( select * from (   select TSPL_ITEM_MASTER.Item_Code" + IIf(clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal, ",Unit_Code", "") + ",TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code   ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc   ,
                   TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc  from  TSPL_ITEM_MASTER   
                   left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code 
                   left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and  TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  
                   where 2=2 )xx  Pivot   ( max(Item_Cagetory_Values) for Item_Category_Code   in ( [CATEGORY RM],[BRAND],[SUB BRAND],[DESCRP],[PACK],[PACK SIZE],  [CATEGORY OT],[CATEGORY FA],[P TYPE],[L TYPE],[JW],[SCRAP])  ) Pivt 
                   Pivot  ( max(Category_Value_Desc) for Item_Category_CodeDesc in ([CATEGORY RMDESC], [BRANDDESC],[SUB BRANDDESC],[DESCRPDESC],[PACKDESC],[PACK SIZEDESC], [CATEGORY OTDESC],[CATEGORY FADESC],[P TYPEDESC],[L TYPEDESC],[JWDESC],[SCRAPDESC])  ) Pivt1 ) xxx 
                   group by Item_Code " + IIf(clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal, ",Unit_Code", "") + "  )  as tbl_Brand on tbl_Brand.Item_Code=Final.Item_Code 
                     left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =Final.Comp_Code  order by Final.sku_seq "

        Return qry
    End Function
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try

            atchqry = getattachqry()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(atchqry)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(objCommonVar.CurrComp_Code1, "ALW") = CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntryNewALW", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                    'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntry", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                ElseIf clsCommon.CompairString(objCommonVar.CurrComp_Code1, "TNK") = CompairStringResult.Equal Then

                    'frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, dt2, "crptDairySaleGatePassEntryNewTNK", "Dairy Sale Gate Pass", "subrptCrateInOut.rpt")

                Else
                    frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleMultipleGatePassEntryNew", "Multiple GatePass Entry", clsCommon.myCDate(txtFromDate.Value))
                    'frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "crptDairySaleGatePassEntries", "Dairy Sale GatePass Entry", clsCommon.myCDate(dt.Rows(0)("GPDate")))
                End If
                frmCRV = Nothing
            End If


        Catch ex As Exception

        End Try
    End Sub
End Class
