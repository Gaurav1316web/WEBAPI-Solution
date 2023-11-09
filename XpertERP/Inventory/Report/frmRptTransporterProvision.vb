'' Work done Internal Transfer Doc not show in report aganist ticket no. UDL/07/05/18-000151 
'' work done agaist ticket no. UDL/04/06/18-000181
'' work done agaist ticket no. SWA/24/09/18-000054
Imports common
Imports System.IO
Public Class frmRptTransporterProvision
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim arrBack As List(Of String)
    Const colSegCode As String = "SEGCODE"
    Const colSegName As String = "SEGNAME"
    Const colFrom As String = "FROMFILTER"
    Const colFromName As String = "FROMFILTERNAME"
    Const colTo As String = "TOFILTER"
    Const colToName As String = "TOFILTERNAME"
    Const colIsForAC As String = "ISFORAC"
    Dim StrQry As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"

    Public arrCompany As ArrayList
    Public arrSourceCode As ArrayList
    Public arrEmployee As ArrayList
    Public arrLocationSegment As ArrayList
    Public arrAccount As ArrayList
    Public arrDepartment As ArrayList
    Public arrVISI As ArrayList
    Public arrMachine As ArrayList
    Public arrVehicle As ArrayList

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False

    Dim dt As DataTable = Nothing
    Dim strERPStartDate As String
    Dim isRunDoubleClick As Boolean = False
#End Region


    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub frmRptTrialBalance_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            RefreshData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub FrmRptSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Transaction")
        funreset()
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RefreshData()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        EnableDisableControls(True)
        txtTransporter.arrValueMember = Nothing
        txtVehicle.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        arrBack = New List(Of String)
    End Sub


    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
        RefreshData()
    End Sub

    Public Sub RefreshData()
        '' work done regarding Transporter Name for Product Invoice type and Customer Name for Transfer type against ticket no. UDL/03/05/18-000146
        Try
            Dim BaseQry As String = "select 'CSA Transfer' as [Trans Type],TSPL_CSA_TRANSFER_HEAD.DOC_CODE as [Document Code],convert(varchar, MAX(TSPL_CSA_TRANSFER_HEAD.Transfer_Date),103) as [Document Date],MAX(TSPL_CSA_TRANSFER_HEAD.Document_Amount) as [Document Amount],MAX(TSPL_CSA_TRANSFER_HEAD.GR_No) as [GR No],MAX(TSPL_CSA_TRANSFER_HEAD.Vehicle_Id) as [Vehicle No],SUM(TSPL_CSA_TRANSFER_DETAIL.Qty) as [Qty],MAX(TSPL_CSA_TRANSFER_DETAIL.Unit_code) as [Unit Code]," & _
                                    " MAX(TSPL_CSA_TRANSFER_HEAD.Total_Item_Wt) as [Net Wt.],MAX(TSPL_CSA_TRANSFER_HEAD.Gross_Item_Wt) as [Gross Wt.],MAX(TSPL_CSA_TRANSFER_HEAD.Cust_Code) as [Customer Code],MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as [Customer Name],MAX(TSPL_CUSTOMER_MASTER.City_Code) as [City Code],MAX(TSPL_CITY_MASTER.City_Name) as [City Name],MAX(TSPL_TRANSPORT_MASTER.Transporter_Name) as [Transporter Name], MAX(TSPL_CSA_TRANSFER_HEAD.Transporter_Name_Manual) as [Transporter Name Manual], MAX(TSPL_CSA_TRANSFER_HEAD.Vehicle_Charge) as [Provision Amount] from TSPL_CSA_TRANSFER_HEAD " & _
                                    " left outer join TSPL_CSA_TRANSFER_DETAIL on TSPL_CSA_TRANSFER_DETAIL.DOC_CODE=TSPL_CSA_TRANSFER_HEAD.DOC_CODE" & _
                                    " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CSA_TRANSFER_HEAD.Cust_Code" & _
                                    " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code" & _
                                    " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_CSA_TRANSFER_HEAD.Transport_Id" & _
                                    " where convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103)>'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"

            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_CSA_TRANSFER_HEAD.Transport_Id in (" + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ") "
            End If

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_CSA_TRANSFER_HEAD.Vehicle_Id in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") "
            End If

            BaseQry += " GROUP BY TSPL_CSA_TRANSFER_HEAD.DOC_CODE "
            ''order by MAX(TSPL_CSA_TRANSFER_HEAD.Transfer_Date)
            BaseQry += "Union All"

            BaseQry += " select 'Transfer' as [Trans Type], TSPL_TRANSFER_ORDER_HEAD.Document_No as [Document Code],convert(varchar, MAX(TSPL_TRANSFER_ORDER_HEAD.Document_Date),103) as [Document Date],MAX(TSPL_TRANSFER_ORDER_HEAD.DOC_Total_Amt) as [Document Amount],max(TSPL_TRANSFER_ORDER_HEAD.GR_No) as [GR No],MAX(case when len(TSPL_TRANSFER_ORDER_HEAD.Vehicle_no)>0 then TSPL_TRANSFER_ORDER_HEAD.Vehicle_no else  TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No end) as [Vehicle No],SUM(TSPL_TRANSFER_ORDER_DETAIL.Out_Qty) as [Qty],MAX(TSPL_TRANSFER_ORDER_DETAIL.Unit_code) as [Unit Code], MAX(TSPL_TRANSFER_ORDER_HEAD.Total_Item_Wt) as [Net Wt.],MAX(TSPL_TRANSFER_ORDER_HEAD.Gross_Item_Wt) as [Gross Wt.],MAX(TSPL_TRANSFER_ORDER_HEAD.To_Location) as [Customer Code],MAX(TSPL_LOCATION_MASTER.Location_Desc) as [Customer Name],MAX('') as [City Code],MAX('') as [City Name],MAX(TSPL_TRANSPORT_MASTER.Transporter_Name) as [Transporter Name], MAX(TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual) as [Transporter Name Manual], MAX(TSPL_TRANSFER_ORDER_HEAD.Vehicle_Charge) as [Provision Amount] from TSPL_TRANSFER_ORDER_HEAD  left outer join TSPL_TRANSFER_ORDER_DETAIL on TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No   left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_TRANSFER_ORDER_HEAD.Transport_Id left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_TRANSFER_ORDER_HEAD.To_Location   where convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)>'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "'"

            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_TRANSFER_ORDER_HEAD.Transport_Id in (" + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ") "
            End If

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") "
            End If
            BaseQry += " and TSPL_TRANSFER_ORDER_HEAD.InternalTransfer=0 GROUP BY TSPL_TRANSFER_ORDER_HEAD.Document_No "

            BaseQry += "union all select 'Product-Invoice' as [Trans Type],TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE as [Document Code]" + Environment.NewLine + _
            " ,convert(varchar, MAX(TSPL_SD_SALE_INVOICE_HEAD.Document_Date),103) as [Document Date]" + Environment.NewLine + _
            " ,MAX(TSPL_SD_SALE_INVOICE_HEAD.Total_Amt) as [Document Amount],MAX(TSPL_SD_SHIPMENT_HEAD.GRNo) as [GR No],MAX(coalesce(coalesce(manualVehicle,Vehicle_Manual_NO),TSPL_SD_SHIPMENT_HEAD.VehicleNo)) as [Vehicle No]" + Environment.NewLine + _
            " ,SUM(TSPL_SD_SALE_INVOICE_DETAIL.Qty) as [Qty],MAX(TSPL_SD_SALE_INVOICE_DETAIL.Unit_code) as [Unit Code], MAX(TSPL_SD_SHIPMENT_HEAD.Total_Item_WeightMetric)  as [Net Wt.],max(TSPL_SD_SHIPMENT_HEAD.Gross_Item_Wt) as [Gross Wt.],MAX(TSPL_SD_SALE_INVOICE_HEAD.Customer_Code) as [Customer Code],MAX(TSPL_CUSTOMER_MASTER.Customer_Name) as [Customer Name],MAX(TSPL_CUSTOMER_MASTER.City_Code) as [City Code],MAX(TSPL_CITY_MASTER.City_Name) as [City Name],MAX(TSPL_TRANSPORT_MASTER.Transporter_Name) as [Transporter Name], MAX(TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual) as [Transporter Name Manual] " + Environment.NewLine + _
            ", (select TSPL_SD_SHIPMENT_HEAD.Freight_Charges from TSPL_SD_SHIPMENT_HEAD  where TSPL_SD_SHIPMENT_HEAD.Document_Code=max(TSPL_SD_SALE_INVOICE_DETAIL.Shipment_Code)) as [Provision Amount] " + Environment.NewLine + _
            "  from TSPL_SD_SALE_INVOICE_DETAIL" + Environment.NewLine + _
            " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE" + Environment.NewLine + _
            "  left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.SHIPMENT_CODE " + Environment.NewLine + _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code" + Environment.NewLine + _
            " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code " + Environment.NewLine + _
            " left outer join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id=TSPL_SD_SHIPMENT_HEAD.Transport_id " + Environment.NewLine + _
            " where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS'" + Environment.NewLine + _
            " and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' " + Environment.NewLine
            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_SD_SHIPMENT_HEAD.Transport_Id in (" + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ") "
            End If

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                BaseQry += " and TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") "
            End If
            BaseQry += " group by TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE"



            dt = clsDBFuncationality.GetDataTable(BaseQry)

            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.EnableFiltering = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to display")
            Else
                gv1.DataSource = dt
                SetGridFormation()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        RadPageView1.SelectedPage = RadPageViewPage2
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = True
        gv1.BestFitColumns()
        EnableDisableControls(False)
        ReStoreGridLayout()
    End Sub

    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        txtVehicle.Enabled = Val
        txtTransporter.Enabled = Val
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = MyBase.Form_ID
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(Me, err.Message)
        End Try
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptTransporterProvisionReport & "'"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            If txtTransporter.arrDispalyMember IsNot Nothing AndAlso txtTransporter.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transporter : " + clsCommon.GetMulcallStringWithComma(txtTransporter.arrDispalyMember))
            End If

            If txtVehicle.arrDispalyMember IsNot Nothing AndAlso txtVehicle.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Vehicle : " + clsCommon.GetMulcallStringWithComma(txtVehicle.arrDispalyMember))
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
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtTransporter__My_Click(sender As Object, e As EventArgs) Handles txtTransporter._My_Click
        Dim qry As String = " select Transport_Id as Code, Transporter_Name as Name from TSPL_TRANSPORT_MASTER"
        txtTransporter.arrValueMember = clsCommon.ShowMultipleSelectForm("MulUP", qry, "Code", "Name", txtTransporter.arrValueMember, txtTransporter.arrDispalyMember)
    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        Dim qry As String = " select distinct Vehicle_Id as Code, Vehicle_Id as Name from TSPL_CSA_TRANSFER_HEAD where isnull(Vehicle_Id,'')<>''"
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("MulUP", qry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub
End Class
