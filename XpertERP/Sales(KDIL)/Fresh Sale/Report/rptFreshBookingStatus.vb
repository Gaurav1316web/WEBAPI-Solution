Imports common
Imports System.IO
Public Class RptFreshBookingStatus
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

 

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCrateAccountingReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Sub FormatGrid()
        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        Gv1.Columns("Booking No").IsVisible = True
        Gv1.Columns("Booking No").Width = 100
        Gv1.Columns("Booking No").HeaderText = "Booking No"
        Gv1.ReadOnly = True

        Gv1.Columns("Booking date").IsVisible = True
        Gv1.Columns("Booking date").Width = 100
        Gv1.Columns("Booking date").HeaderText = "Booking date"
        Gv1.ReadOnly = True

        Gv1.Columns("Book date").IsVisible = True
        Gv1.Columns("Book date").Width = 100
        Gv1.Columns("Book date").HeaderText = "Book date"
        Gv1.ReadOnly = True

        Gv1.Columns("Delivery No").IsVisible = True
        Gv1.Columns("Delivery No").Width = 100
        Gv1.Columns("Delivery No").HeaderText = "Delivery No"
        Gv1.ReadOnly = True

        Gv1.Columns("Dispatch No").IsVisible = True
        Gv1.Columns("Dispatch No").Width = 100
        Gv1.Columns("Dispatch No").HeaderText = "Dispatch No"
        Gv1.ReadOnly = True

        Gv1.Columns("Dispatch date").IsVisible = True
        Gv1.Columns("Dispatch date").Width = 100
        Gv1.Columns("Dispatch date").HeaderText = "Dispatch date"
        Gv1.ReadOnly = True

        Gv1.Columns("Invoice No").IsVisible = True
        Gv1.Columns("Invoice No").Width = 100
        Gv1.Columns("Invoice No").HeaderText = "Invoice No"
        Gv1.ReadOnly = True

        Gv1.Columns("Vehicle Code").IsVisible = True
        Gv1.Columns("Vehicle Code").Width = 100
        Gv1.Columns("Vehicle Code").HeaderText = "Vehicle Code"
        Gv1.ReadOnly = True

        Gv1.Columns("Customer Name").IsVisible = True
        Gv1.Columns("Customer Name").Width = 100
        Gv1.Columns("Customer Name").HeaderText = "Customer Name"
        Gv1.ReadOnly = True

        Gv1.Columns("Group Code").IsVisible = True
        Gv1.Columns("Group Code").Width = 100
        Gv1.Columns("Group Code").HeaderText = "Group Code"
        Gv1.ReadOnly = True

        Gv1.Columns("Group Desc").IsVisible = True
        Gv1.Columns("Group Desc").Width = 100
        Gv1.Columns("Group Desc").HeaderText = "Group Desc"
        Gv1.ReadOnly = True

        Gv1.Columns("Scheme Code").IsVisible = True
        Gv1.Columns("Scheme Code").Width = 100
        Gv1.Columns("Scheme Code").HeaderText = "Scheme Code"
        Gv1.ReadOnly = True

        Gv1.Columns("Loc Code").IsVisible = True
        Gv1.Columns("Loc Code").Width = 100
        Gv1.Columns("Loc Code").HeaderText = "Loc Code"


        Gv1.Columns("Location Desc").IsVisible = True
        Gv1.Columns("Location Desc").Width = 100
        Gv1.Columns("Location Desc").HeaderText = "Location Desc"

        Gv1.Columns("Sampling").IsVisible = True
        Gv1.Columns("Sampling").Width = 100
        Gv1.Columns("Sampling").HeaderText = "Sampling"


        Gv1.Columns("EntryPriceCode").IsVisible = True
        Gv1.Columns("EntryPriceCode").Width = 100
        Gv1.Columns("EntryPriceCode").HeaderText = "EntryPriceCode"


        Gv1.Columns("CurrentPriceCode").IsVisible = True
        Gv1.Columns("CurrentPriceCode").Width = 100
        Gv1.Columns("CurrentPriceCode").HeaderText = "CurrentPriceCode"

        Gv1.Columns("Item Code").IsVisible = True
        Gv1.Columns("Item Code").Width = 100
        Gv1.Columns("Item Code").HeaderText = "Item Code"

        Gv1.Columns("Item Name").IsVisible = True
        Gv1.Columns("Item Name").Width = 100
        Gv1.Columns("Item Name").HeaderText = "Item Name"

        Gv1.Columns("Unit").IsVisible = True
        Gv1.Columns("Unit").Width = 100
        Gv1.Columns("Unit").HeaderText = "Unit"

        Gv1.Columns("Rate").IsVisible = True
        Gv1.Columns("Rate").Width = 100
        Gv1.Columns("Rate").HeaderText = "Rate"


        Gv1.Columns("Booking Qty").IsVisible = True
        Gv1.Columns("Booking Qty").Width = 100
        Gv1.Columns("Booking Qty").HeaderText = "Booking Qty"

        Gv1.Columns("Dispatch Qty").IsVisible = True
        Gv1.Columns("Dispatch Qty").Width = 100
        Gv1.Columns("Dispatch Qty").HeaderText = "Dispatch Qty"

        Gv1.Columns("Dispatch Amt").IsVisible = True
        Gv1.Columns("Dispatch Amt").Width = 100
        Gv1.Columns("Dispatch Amt").HeaderText = "Dispatch Amt"

        Gv1.Columns("Balance Qty").IsVisible = True
        Gv1.Columns("Balance Qty").Width = 100
        Gv1.Columns("Balance Qty").HeaderText = "Balance Qty"


    End Sub

    'Sub LoadVicle()
    '    Dim strquery As String = "Select TSPL_VEHICLE_MASTER.Vehicle_Id As Code,  TSPL_VEHICLE_MASTER.Description As Name From TSPL_VEHICLE_MASTER where 2=2 "
    '    cbgVehicle.DataSource = clsDBFuncationality.GetDataTable(strquery)
    '    cbgVehicle.ValueMember = "Code"
    '    cbgVehicle.DisplayMember = "Description"
    'End Sub

    'Sub LoadCustomer()
    '    Dim strquery As String = "Select TSPL_CUSTOMER_MASTER.Cust_Code As Code,  TSPL_CUSTOMER_MASTER.Customer_Name As Description From TSPL_CUSTOMER_MASTER where 2=2 "
    '    cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
    '    cbgCustomer.ValueMember = "Code"
    '    cbgCustomer.DisplayMember = "Description"
    'End Sub
    'Sub LoadLocation()
    '    Dim strquery As String = "Select TSPL_LOCATION_MASTER.Location_Code As Code,  TSPL_LOCATION_MASTER.Location_Desc As Description From TSPL_LOCATION_MASTER where 2=2 "
    '    cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strquery)
    '    cbgLocation.ValueMember = "Code"
    '    cbgLocation.DisplayMember = "Description"
    'End Sub
    'Sub LoadItem()
    '    Dim strquery As String = "Select TSPL_ITEM_MASTER.Item_Code As Code,  TSPL_ITEM_MASTER.Item_Desc As Description From TSPL_ITEM_MASTER where 2=2 "
    '    cbgItem.DataSource = clsDBFuncationality.GetDataTable(strquery)
    '    cbgItem.ValueMember = "Code"
    '    cbgItem.DisplayMember = "Description"
    'End Sub
    'Sub LoadGroup()
    '    Dim strquery As String = "Select TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code As Code,  TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc As Description From TSPL_CUSTOMER_GROUP_MASTER where 2=2 "
    '    cbgGroup.DataSource = clsDBFuncationality.GetDataTable(strquery)
    '    cbgGroup.ValueMember = "Code"
    '    cbgGroup.DisplayMember = "Description"
    'End Sub

    Private Sub RptFreshBookingStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        'ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        Gv1.DataSource = Nothing
        txtVehicleCode.arrValueMember = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtItemCode.arrValueMember = Nothing
        txtCustomer.arrValueMember = Nothing

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub


    'Private Sub chkVehicleAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgVehicle.Enabled = chkVehicleSelect.IsChecked
    'End Sub

    'Private Sub chkCustomerAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgCustomer.Enabled = chkCustomerSelect.IsChecked
    'End Sub

    'Private Sub chkLocationAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgLocation.Enabled = chkLocationSelect.IsChecked
    'End Sub

    'Private Sub chkItemAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgItem.Enabled = chkCustomerSelect.IsChecked
    'End Sub

    'Private Sub chkGroupAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    cbgGroup.Enabled = chkGroupSelect.IsChecked
    'End Sub

    Sub Print()
        'Dim qry As String = "  select * from (Select aa.Document_No As [Booking No], Max(aa.Document_Date) As [Booking date], Convert(varchar,Max(aa.Document_Date),103) As [Book date], Max(aa.Delivery_No) As [Delivery No], Max(aa.DispatchNo) As [Dispatch No], Convert(varchar,Max(aa.DispatchDate),103) As [Dispatch date], Max(aa.Sale_Invoice_No) As [Invoice No], Max(aa.Vehicle_Code) As [Vehicle Code], Max(aa.Number) As [Vehicle No], Max(aa.AlternateVehicle) As [Alternate Vehicle], Max(aa.ManualVehicle) As [Manual Vehicle], aa.Cust_Code As [Cust Code], Max(aa.Customer_Name) As [Customer Name],Max(aa.Cust_Group_Code) as [Group Code],Max(aa.Cust_Group_Desc) as [Group Desc] ,Max(aa.Scheme_Code) as [Scheme Code], aa.Loc_Code As [Loc Code], Max(aa.Location_Desc) As [Location Desc], aa.Sampling, Max(aa.TransPriceCode) As EntryPriceCode, Max(aa.CurrentPriceCode) As CurrentPriceCode, aa.Item_Code As [Item Code], Max(aa.Item_Desc) As [Item Name], (aa.Unit_code) As Unit, Max(aa.Item_Rate) As Rate, Sum(aa.Booking_Qty) As [Booking Qty], Sum(aa.DispatchQty) As [Dispatch Qty], Max(aa.Item_Rate) * Sum(aa.DispatchQty) As [Dispatch Amt], Sum(aa.Booking_Qty - aa.DispatchQty) As [Balance Qty] From  (Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Price_code As TransPriceCode, TSPL_CUSTOMER_MASTER.price_CodeNon As CurrentPriceCode, TSPL_BOOKING_DETAIL.Document_No, TSPL_BOOKING_MATSER.Document_Date, '' As DispatchNo, Null As DispatchDate, '' As Sale_Invoice_No, TSPL_BOOKING_DETAIL.Vehicle_Code, TSPL_VEHICLE_MASTER.Number, TSPL_BOOKING_DETAIL.Sampling, TSPL_BOOKING_DETAIL.Delivery_No, TSPL_BOOKING_DETAIL.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc ,''As [Scheme_Code], TSPL_BOOKING_DETAIL.Loc_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_BOOKING_DETAIL.Unit_code, TSPL_BOOKING_DETAIL.Item_Rate, TSPL_BOOKING_DETAIL.Booking_Qty As Booking_Qty, 0 As DispatchQty, 1 As RI, '' As ManualVehicle, '' As AlternateVehicle    From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_BOOKING_DETAIL.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_BOOKING_DETAIL.Loc_Code = TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_BOOKING_DETAIL.Delivery_No = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code   Where TSPL_BOOKING_DETAIL.Delivery_No <> ''   Union All    Select TSPL_SD_SHIPMENT_HEAD.Price_code As TransPriceCode, TSPL_CUSTOMER_MASTER.price_CodeNon As CurrentPriceCode, TSPL_BOOKING_MATSER.Document_No, TSPL_BOOKING_MATSER.Document_Date, TSPL_SD_SHIPMENT_HEAD.Document_Code As DispatchNo, TSPL_SD_SHIPMENT_HEAD.Document_Date As DispatchDate, TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No, TSPL_SD_SHIPMENT_HEAD.Vehicle_Code As Vehicle_Code, TSPL_VEHICLE_MASTER.Number, 0 As Sampling, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No As Delivery_No, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code As Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc ,TSPL_SD_SHIPMENT_DETAIL .Scheme_Code, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code As Loc_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SHIPMENT_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost, 0 As BookQty, TSPL_SD_SHIPMENT_DETAIL.Qty As DispatchQty, -1 As RI, TSPL_SD_SHIPMENT_HEAD.ManualVehicle, TSPL_SD_SHIPMENT_HEAD.AlternateVehicle    From TSPL_DELIVERY_NOTE_MASTER_FRESHSALE Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code = TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_SD_SHIPMENT_DETAIL On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SHIPMENT_DETAIL.Delivery_Code Left Outer Join TSPL_SD_SHIPMENT_HEAD On TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE Left Outer Join TSPL_ITEM_MASTER On TSPL_SD_SHIPMENT_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_SD_SHIPMENT_HEAD.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code    Where TSPL_SD_SHIPMENT_HEAD.Trans_Type = 'FS' And TSPL_SD_SHIPMENT_DETAIL.Scheme_Item = 'N') aa Group By aa.Document_No, aa.Cust_Code, aa.Loc_Code, aa.Sampling, aa.Item_Code, aa.Vehicle_Code, aa.Unit_code)xxx "
        'qry += "   where 2=2 "
        'If chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast one Vehicle")
        '    Return
        'ElseIf chkVehicleSelect.IsChecked AndAlso cbgVehicle.CheckedValue.Count > 0 Then
        '    qry += " and  [Vehicle Code]  in (" + clsCommon.GetMulcallString(cbgVehicle.CheckedValue) + ")"
        'End If

        'If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast one Customer")
        '    Return
        'ElseIf chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
        '    qry += " and  [Cust Code] in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        'End If

        'If chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast one Location")
        '    Return
        'ElseIf chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
        '    qry += " and  [Loc Code] in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
        'End If

        'If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast one Item")
        '    Return
        'ElseIf chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count > 0 Then
        '    qry += " and  [Item Code] in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
        'End If

        'If chkGroupSelect.IsChecked AndAlso cbgGroup.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast one Customer Group")
        '    Return
        'ElseIf chkGroupSelect.IsChecked AndAlso cbgGroup.CheckedValue.Count > 0 Then
        '    qry += " and  [Group Code] in (" + clsCommon.GetMulcallString(cbgGroup.CheckedValue) + ")"
        'End If

        'qry += " and convert(date,[Booking date],103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        'qry += " convert(date,[Booking date],103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

        'qry += " order by  [Booking date] "
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'Gv1.DataSource = Nothing
        'Gv1.Columns.Clear()
        'Gv1.Rows.Clear()
        'Gv1.GroupDescriptors.Clear()

        'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
        '    Exit Sub
        'Else
        '    Gv1.DataSource = dt
        '    SetGridFormationOFGV1()
        'End If

        'Gv1.MasterTemplate.AllowAddNewRow = False
        'RadPageView1.SelectedPage = RadPageViewPage2

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs)
        Print()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        Reset()
    End Sub

    Private Sub btnGo_Click_1(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnReset_Click_1(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptFreshBookingStatus_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

   
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If (Gv1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
                Exit Sub
            End If


            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If txtVehicleCode.arrValueMember IsNot Nothing AndAlso txtVehicleCode.arrValueMember.Count > 0 Then
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicleCode.arrDispalyMember))
            End If
            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
            End If
            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
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
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        Print(Exporter.PDF)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)
        btnReferesh = False
        Print(Exporter.Refresh)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub

    Private Sub txtVehicleCode__My_Click(sender As Object, e As EventArgs) Handles txtVehicleCode._My_Click
        strQry = "Select TSPL_VEHICLE_MASTER.Vehicle_Id As Code,  TSPL_VEHICLE_MASTER.Description As Name From TSPL_VEHICLE_MASTER"
        txtVehicleCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtVehicleCode.arrValueMember, txtVehicleCode.arrDispalyMember)
    End Sub

    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        strQry = "Select TSPL_ITEM_MASTER.Item_Code As Code,  TSPL_ITEM_MASTER.Item_Desc As Name From TSPL_ITEM_MASTER "
        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub

    
    Sub Print(ByVal IsPrint As Exporter)
        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            fromDate.Focus()
            Exit Sub
        End If
        Dim squeryClosing As String = String.Empty
        Dim MainQuery As String = String.Empty
        Dim strWhrClause As String = ""

        If txtVehicleCode.arrValueMember IsNot Nothing AndAlso txtVehicleCode.arrValueMember.Count > 0 Then
            strWhrClause += "and [Vehicle Code] in (" + clsCommon.GetMulcallString(txtVehicleCode.arrValueMember) + ")  "
        End If
        If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
            strWhrClause += "and [Item Code] in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
        End If


        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            strWhrClause += " and [Loc Code] in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")  "
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            strWhrClause += " and [Cust Code] in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")  "
        End If

        If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
            strWhrClause += " and [Group Code] in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")  "
        End If

        MainQuery = "  select * from (Select aa.Document_No As [Booking No], convert(varchar,Max(aa.Document_Date),103) As [Booking date], Max(aa.Delivery_No) As [Delivery No], Max(aa.DispatchNo) As [Dispatch No], Convert(varchar,Max(aa.DispatchDate),103) As [Dispatch date], Max(aa.Sale_Invoice_No) As [Invoice No], Max(aa.Vehicle_Code) As [Vehicle Code], Max(aa.Number) As [Vehicle No], Max(aa.AlternateVehicle) As [Alternate Vehicle], Max(aa.ManualVehicle) As [Manual Vehicle], aa.Cust_Code As [Cust Code], Max(aa.Customer_Name) As [Customer Name],Max(aa.Cust_Group_Code) as [Group Code],Max(aa.Cust_Group_Desc) as [Group Desc] ,Max(aa.Scheme_Code) as [Scheme Code], aa.Loc_Code As [Loc Code], Max(aa.Location_Desc) As [Location Desc], aa.Sampling, Max(aa.TransPriceCode) As EntryPriceCode, Max(aa.CurrentPriceCode) As CurrentPriceCode, aa.Item_Code As [Item Code], Max(aa.Item_Desc) As [Item Name], (aa.Unit_code) As Unit, Max(aa.Item_Rate) As Rate, Sum(aa.Booking_Qty) As [Booking Qty], Sum(aa.DispatchQty) As [Dispatch Qty], Max(aa.Item_Rate) * Sum(aa.DispatchQty) As [Dispatch Amt], Sum(aa.Booking_Qty - aa.DispatchQty) As [Balance Qty] From  (Select TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Price_code As TransPriceCode, TSPL_CUSTOMER_MASTER.price_CodeNon As CurrentPriceCode, TSPL_BOOKING_DETAIL.Document_No, TSPL_BOOKING_MATSER.Document_Date, '' As DispatchNo, Null As DispatchDate, '' As Sale_Invoice_No, TSPL_BOOKING_DETAIL.Vehicle_Code, TSPL_VEHICLE_MASTER.Number, TSPL_BOOKING_DETAIL.Sampling, TSPL_BOOKING_DETAIL.Delivery_No, TSPL_BOOKING_DETAIL.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc ,''As [Scheme_Code], TSPL_BOOKING_DETAIL.Loc_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_BOOKING_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_BOOKING_DETAIL.Unit_code, TSPL_BOOKING_DETAIL.Item_Rate, TSPL_BOOKING_DETAIL.Booking_Qty As Booking_Qty, 0 As DispatchQty, 1 As RI, '' As ManualVehicle, '' As AlternateVehicle    From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_BOOKING_DETAIL.Cust_Code = TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_BOOKING_DETAIL.Loc_Code = TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_BOOKING_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_BOOKING_DETAIL.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id Left Outer Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE On TSPL_BOOKING_DETAIL.Delivery_No = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code   Where TSPL_BOOKING_DETAIL.Delivery_No <> ''   Union All    Select TSPL_SD_SHIPMENT_HEAD.Price_code As TransPriceCode, TSPL_CUSTOMER_MASTER.price_CodeNon As CurrentPriceCode, TSPL_BOOKING_MATSER.Document_No, TSPL_BOOKING_MATSER.Document_Date, TSPL_SD_SHIPMENT_HEAD.Document_Code As DispatchNo, TSPL_SD_SHIPMENT_HEAD.Document_Date As DispatchDate, TSPL_SD_SHIPMENT_HEAD.Sale_Invoice_No, TSPL_SD_SHIPMENT_HEAD.Vehicle_Code As Vehicle_Code, TSPL_VEHICLE_MASTER.Number, 0 As Sampling, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No As Delivery_No, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code As Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc ,TSPL_SD_SHIPMENT_DETAIL .Scheme_Code, TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code As Loc_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SHIPMENT_DETAIL.Item_Code, TSPL_ITEM_MASTER.Item_Desc, TSPL_SD_SHIPMENT_DETAIL.Unit_code, TSPL_SD_SHIPMENT_DETAIL.Item_Cost, 0 As BookQty, TSPL_SD_SHIPMENT_DETAIL.Qty As DispatchQty, -1 As RI, TSPL_SD_SHIPMENT_HEAD.ManualVehicle, TSPL_SD_SHIPMENT_HEAD.AlternateVehicle    From TSPL_DELIVERY_NOTE_MASTER_FRESHSALE Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Customer_Code = TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code = TSPL_LOCATION_MASTER.Location_Code Left Outer Join TSPL_SD_SHIPMENT_DETAIL On TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SHIPMENT_DETAIL.Delivery_Code Left Outer Join TSPL_SD_SHIPMENT_HEAD On TSPL_SD_SHIPMENT_HEAD.Document_Code = TSPL_SD_SHIPMENT_DETAIL.DOCUMENT_CODE Left Outer Join TSPL_ITEM_MASTER On TSPL_SD_SHIPMENT_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_SD_SHIPMENT_HEAD.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_MASTER.Cust_Group_Code =TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code    Where TSPL_SD_SHIPMENT_HEAD.Trans_Type = 'FS' And TSPL_SD_SHIPMENT_DETAIL.Scheme_Item = 'N') aa Group By aa.Document_No, aa.Cust_Code, aa.Loc_Code, aa.Sampling, aa.Item_Code, aa.Vehicle_Code, aa.Unit_code)xxx "
        MainQuery += "   where 2=2 "
        MainQuery += strWhrClause
        MainQuery += " and convert(date,[Booking date],103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        MainQuery += " convert(date,[Booking date],103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "

        MainQuery += " order by  convert(date,[Booking date],103) "

        Dim dtgv As New DataTable


        dtgv = clsDBFuncationality.GetDataTable(MainQuery)
        Gv1.DataSource = Nothing

        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.DataSource = dtgv
        Gv1.BestFitColumns()
        ReStoreGridLayout()
        If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2

        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

        If txtVehicleCode.arrValueMember IsNot Nothing AndAlso txtVehicleCode.arrValueMember.Count > 0 Then
            arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicleCode.arrDispalyMember))
        End If
        If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then
            arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
        End If
        If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
            arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
        End If
        If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
            arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
        End If
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
        End If

        If IsPrint = Exporter.Excel Then
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Fresh Booking Status Report", Gv1, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Fresh Booking Status Report", Gv1, arrHeader, "Fresh Sale", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
