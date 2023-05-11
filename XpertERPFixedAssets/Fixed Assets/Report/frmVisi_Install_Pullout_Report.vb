'--Created by -[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001259, BM00000001522]
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmVisi_Install_Pullout_Report
    Inherits FrmMainTranScreen

    Private Sub FrmVisi_Install_Pullout_Report_vb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmVisi_Install_Pullout_Report)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isPrintFlag
    End Sub

    Sub reset()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadVisi()
        chkVisiAll.IsChecked = True
        LoadLocation()
        chkLocationAll.IsChecked = True
        LoadCustomer()
        chkCustomerAll.IsChecked = True
        chkDetail.IsChecked = True
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub LoadVisi()
        Dim strquery As String = "Select Visi_Id as Code, VisiMake as Description from TSPL_VISI_MASTER "
        cbgVisi.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgVisi.ValueMember = "Code"
        cbgVisi.DisplayMember = "Description"
    End Sub

    Sub LoadLocation()
        Dim strquery As String = "select Location_Code AS Code ,Location_Desc as Description FROM TSPL_LOCATION_MASTER WHERE 2=2 "
        cbgLoc.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgLoc.ValueMember = "Code"
        cbgLoc.DisplayMember = "Description"
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "Select Cust_Code as Code, Customer_Name As Name from TSPL_CUSTOMER_MASTER Where Status='N'"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            If chkVisiSelect.IsChecked = True AndAlso cbgVisi.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Visi or select ALL")
                Return
            ElseIf chkLocationSelect.IsChecked = True AndAlso cbgLoc.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Location or select ALL")
                Return
            ElseIf chkCustomerSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Vendor or select ALL")
                Return
            End If

            GV1.EnableFiltering = True
            Dim dt As DataTable
            Dim strFromDate As String = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
            Dim strQuery As String
            If chkDetail.IsChecked Then
                strQuery = "Select ROW_NUMBER() OVER (Order by TSPL_VISI_INSTALL_PULLOUT.Visi_Id) As RowNo, TSPL_VISI_INSTALL_PULLOUT.Visi_Id, TSPL_VISI_MASTER.VisiMake,"
                strQuery += " TSPL_VISI_MASTER.Asset_No, TSPL_VISI_MASTER.Visi_Size, TSPL_VISI_INSTALL_PULLOUT.Visi_Type, TSPL_VISI_INSTALL_PULLOUT.Trans_Type,"
                strQuery += " CONVERT(VARCHAR,TSPL_VISI_INSTALL_PULLOUT.Trans_Date,103) as Trans_Date, TSPL_VISI_INSTALL_PULLOUT.Customer_Id, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_VISI_INSTALL_PULLOUT.Created_By"
                strQuery += " from TSPL_VISI_INSTALL_PULLOUT"
                strQuery += " LEFT OUTER JOIN TSPL_VISI_MASTER ON TSPL_VISI_MASTER.Visi_Id=TSPL_VISI_INSTALL_PULLOUT.Visi_Id"
                strQuery += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_VISI_INSTALL_PULLOUT.Customer_Id"
                strQuery += " WHERE TSPL_VISI_INSTALL_PULLOUT.Trans_Date>='" + strFromDate + "' AND TSPL_VISI_INSTALL_PULLOUT.Trans_Date<='" + strToDate + "'"

                If chkVisiSelect.IsChecked = True AndAlso cbgVisi.CheckedValue.Count <= 0 Then
                    strQuery += " and  TSPL_VISI_INSTALL_PULLOUT.Visi_Id in (" + clsCommon.GetMulcallString(cbgVisi.CheckedValue) + ") "
                End If
                If chkLocationSelect.IsChecked = True AndAlso cbgLoc.CheckedValue.Count > 0 Then
                    strQuery += " and  TSPL_VISI_INSTALL_PULLOUT.location in (" + clsCommon.GetMulcallString(cbgLoc.CheckedValue) + ") "
                End If
                If chkCustomerSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                    strQuery += " and  TSPL_VISI_INSTALL_PULLOUT.cu in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If
                strQuery += " ORDER BY TSPL_VISI_INSTALL_PULLOUT.Visi_Id, TSPL_VISI_INSTALL_PULLOUT.Trans_Date"
            Else
                strQuery = "Select Location, MAX(TSPL_LOCATION_MASTER.Location_Desc) As LocDesc, SUM(New) As New, SUM(Refurnished) As Refurnished, SUM(Total) As Total, SUM(Installed) As Installed, SUM(PulledOut) As Pulledout from ("
                strQuery += " Select ROW_NUMBER() Over (Partition By Visi_Id Order by Trans_Date Desc) As Row, Visi_Id, Customer_Id, Customer_name, Location, Route, Trans_Type, Visi_Type,Trans_Date, "
                strQuery += " Case When Visi_Type='NEW' Then 1 Else 0 End As New, Case When Visi_Type='Refurnished' Then 1 Else 0 End As 'Refurnished', 1 as Total, Case When Trans_Type='Installed' Then 1 Else 0 End as Installed, Case When Trans_Type='Pulled Out' Then 1 Else 0 End as PulledOut from TSPL_VISI_INSTALL_PULLOUT"
                strQuery += " ) XXX LEFT OUTER JOIN TSPL_LOCATION_MASTER ON XXX.Location=TSPL_LOCATION_MASTER.Location_Code WHERE Row=1 AND Trans_Date>='" + strFromDate + "' AND Trans_Date<='" + strToDate + "' Group By XXX.Location"
            End If

            dt = clsDBFuncationality.GetDataTable(strQuery)
            GV1.DataSource = Nothing
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadMessageBox.Show("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt
                SetGridFormationOFGV1()

            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Date Range : " + strFromDate + " To " + strToDate
            arrHeader.Add(strtemp)


            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Visi Install/Pullout", GV1, arrHeader, "Visi_Install_Pullout")
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Visi Install/Pullout", GV1, arrHeader, "Visi_Install_Pullout", True)
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        'For ii As Integer = 0 To GV1.Columns.Count - 1
        '    GV1.Columns(ii).ReadOnly = True
        '    GV1.Columns(ii).IsVisible = False
        'Next
        If chkDetail.IsChecked Then
            GV1.Columns("RowNo").Width = 50
            GV1.Columns("RowNo").HeaderText = "S No."

            GV1.Columns("Visi_Id").Width = 80
            GV1.Columns("Visi_Id").HeaderText = "Visi Id"

            GV1.Columns("VisiMake").Width = 150
            GV1.Columns("VisiMake").HeaderText = "Visi Make"

            GV1.Columns("Asset_No").Width = 80
            GV1.Columns("Asset_No").HeaderText = "Asset Id"

            GV1.Columns("Visi_Type").Width = 80
            GV1.Columns("Visi_Type").HeaderText = "Visi Type"

            GV1.Columns("Visi_Size").Width = 80
            GV1.Columns("Visi_Size").HeaderText = "Visi Size"

            GV1.Columns("Trans_Type").Width = 100
            GV1.Columns("Trans_Type").HeaderText = "Install/Pullout"

            GV1.Columns("Trans_Date").Width = 80
            GV1.Columns("Trans_Date").HeaderText = "Date"

            GV1.Columns("Customer_Id").Width = 80
            GV1.Columns("Customer_Id").HeaderText = "Customer Id"

            GV1.Columns("Customer_Name").Width = 200
            GV1.Columns("Customer_Name").HeaderText = "Customer Name"

            GV1.Columns("Created_By").Width = 80
            GV1.Columns("Created_By").HeaderText = "Approved By"
        Else
            GV1.Columns("Location").Width = 80
            GV1.Columns("Location").HeaderText = "Depot"

            GV1.Columns("LocDesc").Width = 200
            GV1.Columns("LocDesc").HeaderText = "Deopt Name"

            GV1.Columns("New").Width = 80

            GV1.Columns("Refurnished").Width = 80

            GV1.Columns("Total").Width = 80

            GV1.Columns("Installed").Width = 80

            GV1.Columns("Pulledout").Width = 80
            GV1.Columns("Pulledout").HeaderText = "Pulled Out"
        End If
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        Refresh = 2
    End Enum

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLoc.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub chkVisiAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVisiAll.ToggleStateChanged
        cbgVisi.Enabled = Not chkVisiAll.IsChecked
    End Sub


    Private Sub chkDetail_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDetail.ToggleStateChanged
        gbVisi.Enabled = chkDetail.IsChecked
        gbCustomer.Enabled = chkDetail.IsChecked
    End Sub

    Private Sub chkSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSummary.ToggleStateChanged
        If chkSummary.IsChecked Then
            chkVisiAll.IsChecked = True
            chkCustomerAll.IsChecked = True
            chkLocationAll.IsChecked = True
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Print(EnumExportTo.Refresh)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Print(EnumExportTo.PDF)
    End Sub
End Class
