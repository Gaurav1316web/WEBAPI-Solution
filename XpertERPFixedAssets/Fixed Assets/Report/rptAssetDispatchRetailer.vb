Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine

'' created by richa Agarwal on 15 Jan,2020
'sanjay add Export grid
Public Class rptAssetDispatchRetailer
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub rptAssetDispatchRetailer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtLocation.arrValueMember = Nothing
        txtMultiDistributor.arrValueMember = Nothing
        TxtRoute.arrValueMember = Nothing
        TxtMultiZone.arrValueMember = Nothing
        LoadTypes()
        LoadDocumentTypes()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            qry = "select TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No as [Document No] ,convert(varchar,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,103) as [Document Date],TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type as [Doc Type] ,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_Code as [Distributor Code],TSPL_CUSTOMER_MASTER.Customer_Name AS [Distributor Name],TSPL_ASSET_DISPATCH_RETAILER_HEAD.issue_To AS [Retailer Code],IssueEmp.Customer_Name as [Retailer Name],TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By as [Requested by Code],RequestEmp.Emp_Name as [Requested by Name],case when TSPL_ASSET_DISPATCH_RETAILER_HEAD.isItemWise=1 then 'Item' else 'Asset' end as Type ,
    TSPL_ASSET_DISPATCH_RETAILER_detail.AssetCode as [Asset Code],TSPL_ACQUISITION_DETAIL.Asset_Name as [Asset Description],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Code as [Item Code],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Item_Desc as [Item Description],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Unit_code as [UOM],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.AssetID AS [Asset ID],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.SerialNo AS [Serial No],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.AMOUNT AS [Amount],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.DepositType as [Deposit Type],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.DepositReceiptNo as [Deposit Receipt No],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.DepositValue as [Deposit Value]
    ,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.BrandName as [Brand Name],convert(varchar,TSPL_ASSET_DISPATCH_RETAILER_DETAIL.PurchaseInvoiceDate,103) as [Purchase Invoice Date],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.PurchaseInvoiceNo as [Purchase Invoice No],TSPL_ASSET_DISPATCH_RETAILER_DETAIL.Capacity as  [Capacity],TSPL_ASSET_DISPATCH_RETAILER_HEAD.Route_No as [Route Code],tspl_route_master.Route_Desc as [Route Name],TSPL_CUSTOMER_MASTER.Zone_Code as [Zone Code],tspl_zone_master.Description as [Zone Name],TSPL_VEHICLE_MASTER.Description as [Vehicle No],AltVehicle.Description as [Alternate Vehicle No]
    from TSPL_ASSET_DISPATCH_RETAILER_HEAD 
    left outer Join TSPL_ASSET_DISPATCH_RETAILER_detail on TSPL_ASSET_DISPATCH_RETAILER_detail.Doc_No=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_No 
    left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location 
    left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_Code 
    left outer join TSPL_SECONDARY_CUSTOMER_MASTER as IssueEmp on IssueEmp.Cust_Code= TSPL_ASSET_DISPATCH_RETAILER_HEAD.issue_To 
    left outer join TSPL_EMPLOYEE_MASTER as RequestEmp on RequestEmp.EMP_CODE= TSPL_ASSET_DISPATCH_RETAILER_HEAD.Request_By
    left outer join TSPL_ACQUISITION_DETAIL on TSPL_ACQUISITION_DETAIL.Asset_Code=TSPL_ASSET_DISPATCH_RETAILER_detail.AssetCode 
    left outer join tspl_route_master on tspl_route_master.Route_No=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Route_No
    left outer join tspl_zone_master on tspl_zone_master.Zone_Code=TSPL_CUSTOMER_MASTER.Zone_Code
    left outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Vehicle_Id
    left outer join TSPL_VEHICLE_MASTER AltVehicle on AltVehicle.Vehicle_Id=TSPL_ASSET_DISPATCH_RETAILER_HEAD.Alternate_Vehicle_Id
    where TSPL_ASSET_DISPATCH_RETAILER_HEAD.Status=1 and convert(date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,103) >= convert(date,('" + fromDate.Value + "'),103) and convert(date,TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Date,103) <= convert(date,('" & ToDate.Value & "'),103) "


            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.From_Location in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtMultiDistributor.arrValueMember IsNot Nothing AndAlso txtMultiDistributor.arrValueMember.Count > 0 Then
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Distributor_Code in(" + clsCommon.GetMulcallString(txtMultiDistributor.arrValueMember) + ")"
            End If

            If TxtMultiZone.arrValueMember IsNot Nothing AndAlso TxtMultiZone.arrValueMember.Count > 0 Then
                qry += " and tspl_zone_master.Zone_Code in (" + clsCommon.GetMulcallString(TxtMultiZone.arrValueMember) + ")"
            End If
            If TxtRoute.arrValueMember IsNot Nothing AndAlso TxtRoute.arrValueMember.Count > 0 Then
                qry += " and tspl_route_master.Route_No in(" + clsCommon.GetMulcallString(TxtRoute.arrValueMember) + ")"
            End If

            If txtAsset.arrValueMember IsNot Nothing AndAlso txtAsset.arrValueMember.Count > 0 Then
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_detail.AssetCode in(" + clsCommon.GetMulcallString(txtAsset.arrValueMember) + ")"
            End If


            If clsCommon.CompairString(ddlDocumentType.SelectedValue, "Issue") = CompairStringResult.Equal Then
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type = 'Issue' "
            ElseIf clsCommon.CompairString(ddlDocumentType.SelectedValue, "Return") = CompairStringResult.Equal Then
                qry += " and TSPL_ASSET_DISPATCH_RETAILER_HEAD.Doc_Type = 'Return' "
            End If

            If clsCommon.CompairString(ddlReportType.SelectedValue, "Distributor Wise") = CompairStringResult.Equal Then


            End If
            ''qry += " group by final.Cust_Code,final.route_no  ,final.Item_Code  ,final.Payment_Mode ,final.Document_Date ,final.Loc_Code  order by convert(date,final.Document_Date,103)  "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                'Gv1.Columns("Amount").FormatString = "{0:n2}"
                'Gv1.Columns("ToParty").IsVisible = False
                'Gv1.Columns("Cheque/DD No").IsVisible = False
                'Gv1.Columns("Cheque Date").IsVisible = False
                'Gv1.Columns("Bank/Branch").IsVisible = False
                'Gv1.Columns("Receipt No").IsVisible = False
                'Gv1.Columns("Receipt Type").IsVisible = False


                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(Amount)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.BestFitColumns()

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(clsUserMgtCode.RptAssetDispatchRetailer) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(clsUserMgtCode.RptAssetDispatchRetailer, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where  Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDetailedCardReport", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = clsUserMgtCode.RptAssetDispatchRetailer
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(clsUserMgtCode.RptAssetDispatchRetailer, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptAssetDispatchRetailer & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtMultiDistributor.arrDispalyMember IsNot Nothing AndAlso txtMultiDistributor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Distributor : " + clsCommon.GetMulcallStringWithComma(txtMultiDistributor.arrDispalyMember))
            End If


            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, clsUserMgtCode.RptAssetDispatchRetailer)
                clsCommon.MyExportToExcelGrid("Asset Dispatch Retailer Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, clsUserMgtCode.RptAssetDispatchRetailer)
                clsCommon.MyExportToPDF("Asset Dispatch Retailer Report", Gv1, arrHeader, Me.Text, clsUserMgtCode.RptAssetDispatchRetailer, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtMultiDistributor__My_Click(sender As Object, e As EventArgs) Handles txtMultiDistributor._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiDistributor.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSelect", qry, "Code", "Name", txtMultiDistributor.arrValueMember, txtMultiDistributor.arrDispalyMember)
    End Sub

    Private Sub TxtMultiZone__My_Click(sender As Object, e As EventArgs) Handles TxtMultiZone._My_Click
        Dim strQry As String = "select Zone_Code as Code ,Description as Name from TSPL_ZONE_MASTER where 1=1"
        If txtMultiDistributor.arrValueMember IsNot Nothing AndAlso txtMultiDistributor.arrValueMember.Count > 0 Then
            strQry += " and TSPL_ZONE_MASTER. Zone_Code in (Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultiDistributor.arrValueMember) + ") )"
        End If
        TxtMultiZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMulSelp", strQry, "Code", "Name", TxtMultiZone.arrValueMember, TxtMultiZone.arrDispalyMember)
    End Sub


    Private Sub TxtRoute__My_Click(sender As Object, e As EventArgs) Handles TxtRoute._My_Click
        Dim qry As String = "Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER  where 1=1 "
        If txtMultiDistributor.arrValueMember IsNot Nothing AndAlso txtMultiDistributor.arrValueMember.Count > 0 Then
            qry += " and TSPL_ROUTE_MASTER.Route_No in (Select TSPL_CUSTOMER_MASTER.Route_No from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(txtMultiDistributor.arrValueMember) + ") )"
        End If
        TxtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", TxtRoute.arrValueMember, TxtRoute.arrDispalyMember)
    End Sub
    Private Sub txtAsset__My_Click(sender As Object, e As EventArgs) Handles txtAsset._My_Click
        Dim qry As String = " select TSPL_ACQUISITION_DETAIL.Asset_Code as Code,TSPL_ACQUISITION_DETAIL.Asset_Name,TSPL_ACQUISITION_DETAIL.Item_code as [Item Code],TSPL_ITEM_MASTER.Item_desc as [Item Name] FROM TSPL_ACQUISITION_DETAIL LEFT OUTER JOIN TSPL_ACQUISITION_HEAD ON TSPL_ACQUISITION_HEAD.Acquisition_Code=TSPL_ACQUISITION_DETAIL.Acquisition_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_ACQUISITION_DETAIL.item_code"
        txtAsset.arrValueMember = clsCommon.ShowMultipleSelectForm("assetacq", qry, "Code", "Name", txtAsset.arrValueMember, txtAsset.arrDispalyMember)
    End Sub
    Sub LoadTypes()
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Document Detail Wise")
        dt.Rows.Add("Document Wise")
        dt.Rows.Add("Distributor Wise")
        ddlReportType.DataSource = dt
        ddlReportType.ValueMember = "Code"
        ddlReportType.DisplayMember = "Code"
    End Sub
    Sub LoadDocumentTypes()
        Dim dt As DataTable
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("Both")
        dt.Rows.Add("Issue")
        dt.Rows.Add("Return")
        ddlDocumentType.DataSource = dt
        ddlDocumentType.ValueMember = "Code"
        ddlDocumentType.DisplayMember = "Code"
    End Sub

    Private Sub RmiExcelGrid_Click(sender As Object, e As EventArgs) Handles rmiExcelGrid.Click
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptAssetDispatchRetailer & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtMultiDistributor.arrDispalyMember IsNot Nothing AndAlso txtMultiDistributor.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Distributor : " + clsCommon.GetMulcallStringWithComma(txtMultiDistributor.arrDispalyMember))
            End If

            clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text, True)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
        End Try
    End Sub
End Class
