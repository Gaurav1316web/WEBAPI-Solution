Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptQualityStatus
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Const ReportID As String = "Qua_Sta"
    'Private Sub SetUserMgmtNew()
    '    'MyBase.SetUserMgmt(clsUserMgtCode.CustomersListReport)
    '    If Not (MyBase.isReadFlag) Then
    '        Throw New Exception("Permission Denied")
    '    End If

    'End Function
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadItem()
        LoadVendor()
        LoadLocation()
        chkItemAll.CheckState = CheckState.Checked
        chkLocationAll.CheckState = CheckState.Checked
        chkVendorAll.CheckState = CheckState.Checked
        cboSelectedBy.Text = "Both"
        gvDetails.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub RptQualityStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

   
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvDetails.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvDetails.Columns.Count - 1 Step ii + 1
                        gvDetails.Columns(ii).IsVisible = False
                        gvDetails.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gvDetails.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not chkVendorAll.IsChecked
    End Sub

    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub

    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub

    
  
    Private Sub RmSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmSL.Click
        gvDetails.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = ReportID
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gvDetails.SaveLayout(obj.GridLayout)
        obj.GridColumns = gvDetails.ColumnCount
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
        End If
        ''stuti regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
    End Sub

    Private Sub RmDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmDL.Click
        If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow("Layout Deleted successfully", "Information")
        End If
    End Sub


    Public Sub Load_Report()
        If chkItemSelect.IsChecked AndAlso cbgItem.CheckedValue.Count = 0 Then
            Throw New Exception("Please select atleast single Item or select all.")
        ElseIf chkLocationSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            Throw New Exception("Please select atleast single Location or select all.")
        ElseIf chkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
            Throw New Exception("Please select atleast single Vendor or select all.")

        End If

        Dim sQuery As String = "select sh.Vendor_Code as [Vendor Code],vm.Vendor_Name as [Vendor Name],PurchaseOrder_No as [PO No]," _
        & " convert(varchar,PurchaseOrder_Date,103) as [PO Date],sh.SRN_No as [SRN No],convert(varchar,SRN_Date,103) as [SRN Date], sd.Item_Code as [Item Code]," _
        & " im.Item_Desc as [Item Desc],Auto_Sr_No as [Serial No],case when QC_Complete='1' then 'Accepted' when coalesce(QC_Complete,0)= 0 then 'Rejected' end as " _
        & " [QC Status],lm.Location_Code as [Location Code],lm.Location_Desc as [Location Desc] from TSPL_SRN_DETAIL sd left join TSPL_SRN_HEAD sh on sh.SRN_No=sd.SRN_No " _
        & " left join TSPL_VENDOR_MASTER vm on vm.Vendor_Code=sh.Vendor_Code left join TSPL_ITEM_MASTER im on im.Item_Code=sd.Item_Code left join " _
        & " TSPL_PURCHASE_ORDER_HEAD poh on poh.PurchaseOrder_No=PO_ID left join TSPL_SERIAL_ITEM si on si.Item_Code=sd.Item_Code and si.Document_Code=sh.SRN_No " _
        & " left join TSPL_LOCATION_MASTER lm on lm.Location_Code=sd.Location where 2=2 "

        sQuery += " and Convert(date,sh.SRN_Date,103)>=Convert(date,'" + txtFromDate.Value + "',103)and Convert(date,sh.SRN_Date,103)<=Convert(date,'" + txtToDate.Value + "',103) "

        'If chkLocationSelect.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
        '    sQuery += "and lm.Location_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        'End If
        'If chkItemSelect.IsChecked And cbgItem.CheckedValue.Count > 0 Then
        '    sQuery += " and sh.SRN_No in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")  "
        'End If
        'If chkVendorSelect.IsChecked And cbgVendor.CheckedValue.Count > 0 Then
        '    sQuery += " and sh.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
        'End If
        '====added by shivani
        If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            sQuery += " and lm.Location_Code  IN (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            sQuery += " and im.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
            sQuery += " and sh.Vendor_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ") " + Environment.NewLine
        End If
        '========================
        If cboSelectedBy.Text = "Accepted" Then
            sQuery += " and coalesce(QC_Complete,0) = 1 "
        ElseIf cboSelectedBy.Text = "Rejected Item" Then
            sQuery += " and coalesce(QC_Complete,0)= 0 "
        End If
        sQuery += " and im.Serial_Counter <> '' order by SRN_Date"
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gvDetails.DataSource = Nothing
            gvDetails.Rows.Clear()
            gvDetails.Columns.Clear()
            gvDetails.DataSource = dtgv
            FormatGrid()
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found..")
        End If
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = ReportID
            TemplateGridview = gvDetails
            Load_Report()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub BtnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub FormatGrid()
        gvDetails.AllowAddNewRow = False
        gvDetails.TableElement.TableHeaderHeight = 40
        gvDetails.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvDetails.Columns.Count - 1
            gvDetails.Columns(ii).ReadOnly = True
            '     gvDetails.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim item1 As GridViewSummaryItem

        gvDetails.Columns("Vendor Code").Width = 100
        gvDetails.Columns("Vendor Name").Width = 150
        gvDetails.Columns("PO No").Width = 100
        'gvDetails.Columns("PO Date").Width = 150
        gvDetails.Columns("SRN No").Width = 100
        gvDetails.Columns("SRN Date").Width = 200
        gvDetails.Columns("Item Code").Width = 100
        gvDetails.Columns("Item Desc").Width = 300
        gvDetails.Columns("Serial No").Width = 100
        gvDetails.Columns("QC Status").Width = 100
        gvDetails.Columns("Location Code").Width = 100
        gvDetails.Columns("Location Desc").Width = 200
    End Sub

    Private Sub gvDetails_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gvDetails.CellFormatting
        Try
            If e.Column Is gvDetails.Columns("QC Status") Then
                If TypeOf e.CellElement.RowInfo Is GridViewDataRowInfo Then
                    Dim strFlatWithInvesterDealer As String = clsCommon.myCstr(gvDetails.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
                    Dim strFlatCode As String = ""

                    If clsCommon.myLen(strFlatWithInvesterDealer) > 0 AndAlso strFlatWithInvesterDealer.Contains("/") Then
                        Dim intIndex As Integer = strFlatWithInvesterDealer.IndexOf("/")
                        strFlatCode = strFlatWithInvesterDealer.Substring(0, intIndex)
                    Else
                        strFlatCode = strFlatWithInvesterDealer
                    End If

                    ' If arrReservedFlats IsNot Nothing AndAlso arrReservedFlats.Contains(strFlatCode) Then
                    e.CellElement.DrawFill = True
                    e.CellElement.GradientStyle = GradientStyles.Solid
                    e.CellElement.ForeColor = Color.Black
                    e.CellElement.BackColor = Color.LightGreen

                    If clsCommon.myCstr(e.Row.Cells("QC Status").Value) = clsCommon.myCstr("Rejected") Then
                        e.CellElement.BackColor = Color.Red
                        e.CellElement.ForeColor = Color.White
                    End If
                    If clsCommon.myCstr(e.Row.Cells("QC Status").Value) = clsCommon.myCstr("Accepted") Then
                        e.CellElement.BackColor = Color.LightGreen
                    End If
                End If
            Else
                e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local)
                e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local)
                e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local)
                e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RmSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RmSaveLayout.Click
        ReStoreGridLayout()
    End Sub

    Private Sub btnDeleteLayour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmDeleteLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    'Sub Export()
    '    If gvDetails.Rows.Count > 0 Then
    '        ExportToExcel()
    '    Else
    '        common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '    End If
    'End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            If gvDetails.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptQualityStatus & "'"))
                arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If txtVendor.arrDispalyMember IsNot Nothing AndAlso txtVendor.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtVendor.arrDispalyMember))
                End If
                If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
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
                    transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvDetails, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gvDetails, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gvDetails, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Quality Status Report", gvDetails, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    'Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    Export()
    'End Sub
    Sub LoadVendor()
        Dim qry As String = "select Vendor_Code as Code ,Vendor_Name as Name from TSPL_VENDOR_MASTER   WHERE  Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"

    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_Code as Code,Location_Desc  as Description from TSPL_LOCATION_MASTER    "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Description"

    End Sub

    Sub LoadItem()
        Dim qry As String = "select Item_Code as Code,Item_Desc as Description  from TSPL_ITEM_Master    "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
        cbgItem.DisplayMember = "Description"

    End Sub
    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = "select Location_Code as Code,Location_Desc  as Name from TSPL_LOCATION_MASTER    "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name from TSPL_VENDOR_MASTER  WHERE  Status='N' "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = "select Item_Code as Code,Item_Desc as Name  from TSPL_ITEM_Master"
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", qry, "Code", "Name", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class
