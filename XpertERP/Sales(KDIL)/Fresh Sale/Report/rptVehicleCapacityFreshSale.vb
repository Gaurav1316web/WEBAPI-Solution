Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common

Imports System.IO
Public Class RptVehicleCapacityFreshSale
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.RptVehicleCapacityFreshSaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub LoadLocation()
        Dim qry As String = " select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"

    End Sub

    Sub Loadvehicle()
        Dim qry As String = " select  vehicle_id as [Code],Number as [Name]   from tspl_vehicle_master as [Name]"
        cbgVeh.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVeh.ValueMember = "Code"
        cbgVeh.DisplayMember = "Name"

    End Sub
    Sub LoadCustomerGroup()
        Dim qry As String = " select Cust_Group_Code as [Code],Cust_Group_Desc as [Name] from TSPL_CUSTOMER_GROUP_MASTER as [Name]"
        CbgCustomerGroup.DataSource = clsDBFuncationality.GetDataTable(qry)
        CbgCustomerGroup.ValueMember = "Code"
        CbgCustomerGroup.DisplayMember = "Name"

    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy")))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If ChkVehSelect.IsChecked Then
                Dim strVehName As String = ""
                For Each StrName As String In cbgVeh.CheckedDisplayMember
                    If clsCommon.myLen(strVehName) > 0 Then
                        strVehName += ", "
                    End If
                    strVehName += StrName
                Next
                Dim strVehCode As String = ""
                For Each StrCode As String In cbgVeh.CheckedValue
                    If clsCommon.myLen(strVehCode) > 0 Then
                        strVehCode += ", "
                    End If
                    strVehCode += StrCode
                Next

                arrHeader.Add(("Vehicle Name: " + strVehName + " "))

            End If
            If chkSelectLocation.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next

                arrHeader.Add(("Location: " + strLocationName + " "))

            End If

            If ChkCustSelect.IsChecked Then
                Dim strCustomerGroupName As String = ""
                For Each StrName As String In CbgCustomerGroup.CheckedDisplayMember
                    If clsCommon.myLen(strCustomerGroupName) > 0 Then
                        strCustomerGroupName += ", "
                    End If
                    strCustomerGroupName += StrName
                Next
                arrHeader.Add(("Customer Group: " + strCustomerGroupName + " "))

            End If
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Vehicle Capacity Fresh Sale Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Vehicle Capacity Fresh Sale Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub Reset()
        txtDate.Value = clsCommon.GETSERVERDATE()
        SearchToDate.Value = clsCommon.GETSERVERDATE()
        LoadLocation()
        Loadvehicle()
        LoadCustomerGroup()
        ChkVehAll.CheckState = CheckState.Checked
        chkAllLocation.CheckState = CheckState.Checked
        ChkCustAll.CheckState = CheckState.Checked
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
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
    Public Sub Load_Report()
        Dim sQuery As String = String.Empty
        'KUNAL > TICKET : BM00000009476 > DATE : 26 - 10 - 2016 > KDIL
        If chkSelectLocation.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Location or select all.", Me.Text)
            Exit Sub
        End If

        If ChkVehSelect.IsChecked AndAlso cbgVeh.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vehicle or select all.", Me.Text)
            Exit Sub
        End If
        Dim ToDate As String = clsCommon.myCDate(clsCommon.GetPrintDate(SearchToDate.Value, "dd/MMM/yyyy"))
        Dim Fromdate As String = clsCommon.myCDate(clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy"))
        sQuery += "select '' as SNo, '" + Fromdate + "' as FromDate ,'" + ToDate + "' as ToDate,convert(varchar,final.Doc_date,103) as Doc_date,final.[Location code],final.[Location Name], final.[Vehicle Code] ,final.[Vehicle No],final. crateQty as [Total Crate Qty],"
        sQuery += " tspl_vehicle_master.Capacity as [Vehicle Capicity] ,tspl_vehicle_master.Capacity-final. crateQty as [Difference],convert(decimal(18,2),"
        sQuery += "  case when tspl_vehicle_master.Capacity=0 then '' else (final. crateQty/tspl_vehicle_master.Capacity)*100  end )as [Vehicle Utlization],final.Transporter_Name,"
        sQuery += " Case When   ISNULL  (tspl_customer_master.Phone1,'')<>'(+__)__________' Then '  '+ tspl_customer_master.Phone1 end as [Mobile No],Cust_Group_Code as [Customer Group] ,  Cust_Group_Desc  AS  [Cust Group Desc]  "
        sQuery += " from (select  max(TSPL_SD_SALE_INVOICE_head.Document_Date) as Doc_Date,TSPL_SD_SALE_INVOICE_head.vehicle_Code as [Vehicle Code],"
        sQuery += "  max(tspl_vehicle_master.Number) as [Vehicle No],sum(TSPL_SD_SALE_INVOICE_head.crateQty) as crateQty,max(tspl_customer_master.cust_code) as cust_code,"
        sQuery += " max(tspl_vehicle_master.transport_id) as transport_id,max(tspl_transport_master.Transporter_Name) as Transporter_Name, "
        sQuery += " max(TSPL_SD_SALE_INVOICE_head.bill_to_location) as [Location Code],max(tspl_location_master.location_desc) as [Location Name],max(TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc) as Cust_Group , MAX(tspl_customer_group_master.Cust_Group_Desc) As Cust_Group_Desc "
        sQuery += "  from TSPL_SD_SALE_INVOICE_head"
        sQuery += " left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_SD_SALE_INVOICE_head.customer_code"
        sQuery += " left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code "
        sQuery += " left outer join tspl_location_master on tspl_location_master.location_code=TSPL_SD_SALE_INVOICE_head.bill_to_location"
        sQuery += "  left outer join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=TSPL_SD_SALE_INVOICE_head.vehicle_Code"
        sQuery += " left outer join tspl_transport_master on tspl_transport_master.Transport_Id=tspl_vehicle_master.transport_id"
        sQuery += " where 2=2 and TSPL_SD_SALE_INVOICE_head.trans_type='FS'"
        sQuery += "  and convert(date,TSPL_SD_SALE_INVOICE_head.Document_Date,103)>=convert(date,'" + txtDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_head.Document_Date,103)<=convert(date,'" + SearchToDate.Value + "',103) "
        If chkSelectLocation.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
            sQuery += "and TSPL_SD_SALE_INVOICE_head.bill_to_location  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If
        If ChkVehSelect.IsChecked And cbgVeh.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_SD_SALE_INVOICE_head.vehicle_Code  in (" + clsCommon.GetMulcallString(cbgVeh.CheckedValue) + ")"
        End If
        If ChkCustSelect.IsChecked And CbgCustomerGroup.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code  in (" + clsCommon.GetMulcallString(CbgCustomerGroup.CheckedValue) + ")"
        End If
        sQuery += "  group by TSPL_SD_SALE_INVOICE_head.vehicle_Code) as Final"
        sQuery += "  left outer join tspl_vehicle_master on tspl_vehicle_master.vehicle_id=final.[vehicle Code]"
        sQuery += "  left outer join tspl_customer_master on tspl_customer_master.cust_code=final.cust_code"
        sQuery += " order by convert(date,final.Doc_date,103)"


        'sQuery += "group by TSPL_MCC_MASTER.MCC_Code,    TSPL_VSPItem_DETAIL.Item_Code,Doc_Type ,Issue_To ) as final group by Issue_To "

        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            For i As Integer = 0 To Gv1.Rows.Count - 1
                Gv1.Rows(i).Cells(0).Value = i + 1
            Next
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()


        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
            
        Next
        Gv1.Columns("SNo").IsVisible = True
        Gv1.Columns("SNo").Width = 100
        Gv1.Columns("SNo").HeaderText = " Sno"

        gv1.Columns("FromDate").IsVisible = True
        gv1.Columns("FromDate").Width = 100
        gv1.Columns("FromDate").HeaderText = " From Date"
        gv1.Columns("FromDate").FormatString = "{0:d}"

        gv1.Columns("ToDate").IsVisible = True
        gv1.Columns("ToDate").Width = 100
        gv1.Columns("ToDate").HeaderText = " To Date"
        gv1.Columns("ToDate").FormatString = "{0:d}"

        Gv1.Columns("Location code").IsVisible = False
        Gv1.Columns("Location code").Width = 100
        Gv1.Columns("Location code").HeaderText = " Location Code"

        Gv1.Columns("Location Name").IsVisible = False
        Gv1.Columns("Location Name").Width = 100
        Gv1.Columns("Location Name").HeaderText = " Location Name"

        Gv1.Columns("Vehicle Code").IsVisible = False
        Gv1.Columns("Vehicle Code").Width = 100
        Gv1.Columns("Vehicle Code").HeaderText = " Vehicle Code"

        Gv1.Columns("Vehicle No").IsVisible = True
        Gv1.Columns("Vehicle No").Width = 100
        Gv1.Columns("Vehicle No").HeaderText = "Vehicle No"

        Gv1.Columns("Total Crate Qty").IsVisible = True
        Gv1.Columns("Total Crate Qty").Width = 80
        Gv1.Columns("Total Crate Qty").HeaderText = "Total crate Qty"

        Gv1.Columns("Vehicle Capicity").IsVisible = True
        Gv1.Columns("Vehicle Capicity").Width = 100
        Gv1.Columns("Vehicle Capicity").HeaderText = "Vehicle Capacity"

        Gv1.Columns("Difference").IsVisible = True
        Gv1.Columns("Difference").Width = 100
        Gv1.Columns("Difference").HeaderText = "Difference"

        Gv1.Columns("Vehicle Utlization").IsVisible = True
        Gv1.Columns("Vehicle Utlization").Width = 100
        Gv1.Columns("Vehicle Utlization").HeaderText = "Vehicle Utlization"

        Gv1.Columns("Transporter_Name").IsVisible = True
        Gv1.Columns("Transporter_Name").Width = 100
        Gv1.Columns("Transporter_Name").HeaderText = "Transporter Name"

        Gv1.Columns("Mobile No").IsVisible = True
        Gv1.Columns("Mobile No").Width = 100
        Gv1.Columns("Mobile No").HeaderText = "Mobile No."

        gv1.Columns("Customer Group").IsVisible = True
        gv1.Columns("Customer Group").Width = 100
        gv1.Columns("Customer Group").HeaderText = "Customer Group"

        'KUNAL > TICKET : BM00000009476 > DATE : 26 - 10 - 2016 > KDIL
        gv1.Columns("Cust Group Desc").IsVisible = True
        gv1.Columns("Cust Group Desc").Width = 100
        gv1.Columns("Cust Group Desc").HeaderText = "Cust Group Desc"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
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
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")

    End Sub

    Private Sub RptVehicleCapacityFreshSale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
       
        Reset()
    End Sub

    Private Sub ChkVehAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkVehAll.ToggleStateChanged
        cbgVeh.Enabled = Not ChkVehAll.IsChecked
    End Sub

    Private Sub chkAllLocation_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkAllLocation.ToggleStateChanged
        cbgLocation.Enabled = Not chkAllLocation.IsChecked
    End Sub
    Private Sub ChkCustAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkCustAll.ToggleStateChanged
        CbgCustomerGroup.Enabled = Not ChkCustAll.IsChecked
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        Load_Report()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If ChkVehSelect.IsChecked Then
                Dim strVehName As String = ""
                For Each StrName As String In cbgVeh.CheckedDisplayMember
                    If clsCommon.myLen(strVehName) > 0 Then
                        strVehName += ", "
                    End If
                    strVehName += StrName
                Next
                Dim strVehCode As String = ""
                For Each StrCode As String In cbgVeh.CheckedValue
                    If clsCommon.myLen(strVehCode) > 0 Then
                        strVehCode += ", "
                    End If
                    strVehCode += StrCode
                Next

                arrHeader.Add(("Vehicle Name: " + strVehName + " "))

            End If
            If chkSelectLocation.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next

                arrHeader.Add(("Location: " + strLocationName + " "))

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
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RptVehicleCapacityFreshSale_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        
        If e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Load_Report()
        End If
    End Sub

End Class
