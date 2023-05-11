Imports common
Imports System.IO
Public Class RptGPDetail
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Created by preeti Gupta Against ticket no[UDL/15/01/19-000255,UDL/18/01/19-000258]
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub EnableDisableControls(ByVal Val As Boolean)
        txtFromDate.Enabled = Val
        txtToDate.Enabled = Val
        txtVehicle.Enabled = Val
        txtTransporter.Enabled = Val
        txtmultCustomer.Enabled = Val
        txtmultLocation.Enabled = Val
        txtmultCity.Enabled = Val
    End Sub
    Sub funreset()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        EnableDisableControls(True)
        txtTransporter.arrValueMember = Nothing
        txtVehicle.arrValueMember = Nothing
        txtmultCustomer.arrValueMember = Nothing
        txtmultLocation.arrValueMember = Nothing
        txtmultCity.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
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
            common.clsCommon.MyMessageBoxShow(err.Message)
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

            If txtmultCustomer.arrDispalyMember IsNot Nothing AndAlso txtmultCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtmultCustomer.arrDispalyMember))
            End If

            If txtmultLocation.arrDispalyMember IsNot Nothing AndAlso txtmultLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtmultLocation.arrDispalyMember))
            End If

            If txtmultCity.arrDispalyMember IsNot Nothing AndAlso txtmultCity.arrDispalyMember.Count > 0 Then
                arrHeader.Add("City : " + clsCommon.GetMulcallStringWithComma(txtmultCity.arrDispalyMember))
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
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader) 'frm.Text)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Public Sub RefreshData()

        Try
            Dim wrcls As String = Nothing
            'wrcls = "  where 2=2 and convert(date,TSPL_GATEPASS_MASTER_PRODUCTSALE.GPDate,103)>'" + clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_GATEPASS_MASTER_PRODUCTSALE.GPDate,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            wrcls = " where 2=2 and Cast(TSPL_GATEPASS_MASTER_PRODUCTSALE.GPDate as Date) >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtfromDate.Value), "dd/MMM/yyyy") + "' and Cast(TSPL_GATEPASS_MASTER_PRODUCTSALE.GPDate as Date) <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy") + "' and  post='Y'"

            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                wrcls += " and TSPL_GATEPASS_MASTER_PRODUCTSALE.Transporter_code in (" + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ") "
            End If

            If txtVehicle.arrValueMember IsNot Nothing AndAlso txtVehicle.arrValueMember.Count > 0 Then
                wrcls += " and TSPL_GATEPASS_MASTER_PRODUCTSALE.Vehicle_Id in (" + clsCommon.GetMulcallString(txtVehicle.arrValueMember) + ") "
            End If

            If txtmultCustomer.arrValueMember IsNot Nothing AndAlso txtmultCustomer.arrValueMember.Count > 0 Then
                wrcls += " and TSPL_GATEPASS_DETAIL_ProductSale.cust_code in (" + clsCommon.GetMulcallString(txtmultCustomer.arrValueMember) + ") "
            End If

            If txtmultCity.arrValueMember IsNot Nothing AndAlso txtmultCity.arrValueMember.Count > 0 Then
                wrcls += " and TSPL_GATEPASS_MASTER_PRODUCTSALE.City_code in (" + clsCommon.GetMulcallString(txtmultCity.arrValueMember) + ") "
            End If

            If txtmultLocation.arrValueMember IsNot Nothing AndAlso txtmultLocation.arrValueMember.Count > 0 Then
                wrcls += " and TSPL_GATEPASS_MASTER_PRODUCTSALE.Location_Code in (" + clsCommon.GetMulcallString(txtmultLocation.arrValueMember) + ") "
            End If

            Dim BaseQry As String = "select GPCode as [Gate Pass No],GPDate as [Gate Pass Date],Location_Code as [Location Code],Location_Desc as [Location Name],City_code as [City Code],City_Name as [City Name] , Vehicle_Id as [Vehicle Id],Vehicle_Name as [Vehicle Name],Transporter_code as [Transporter code],Transporter_Name as [Transporter Name],cust_code as [Customer Code], Customer_Name as [Customer Name], Invoice_No as [Invoice No],Invoice_date as [Invoice Date],Gross_Weight as [Gross Weight],case when Sno =1 then Total_Gross_Weight else 0 end as [Total Gross Weight],Documnet_Amount as [Document Amount],Provision_Amount as [Provision Amount] from (select TSPL_GATEPASS_MASTER_PRODUCTSALE.Transporter_code ,TSPL_TRANSPORT_MASTER .Transporter_Name ,TSPL_GATEPASS_MASTER_PRODUCTSALE.Vehicle_Id ,TSPL_VEHICLE_MASTER .Description as Vehicle_Name,TSPL_GATEPASS_MASTER_PRODUCTSALE.GPCode ,convert(varchar,TSPL_GATEPASS_MASTER_PRODUCTSALE.GPDate,103) as GPDate," & _
                                    " TSPL_GATEPASS_MASTER_PRODUCTSALE.Location_Code,TSPL_LOCATION_MASTER.Location_Desc " & _
                                    " ,TSPL_GATEPASS_MASTER_PRODUCTSALE.City_code ,TSPL_CITY_MASTER.City_Name," & _
                                    " TSPL_GATEPASS_DETAIL_ProductSale.cust_code ,TSPL_CUSTOMER_MASTER.Customer_Name ," & _
                                    " TSPL_GATEPASS_DETAIL_ProductSale.Invoice_No ,convert(varchar,TSPL_GATEPASS_DETAIL_ProductSale.Invoice_date,103) as Invoice_date ,TSPL_GATEPASS_DETAIL_ProductSale.Gross_Weight," & _
                                    " TSPL_GATEPASS_MASTER_PRODUCTSALE.Total_Gross_Weight," & _
                                    " ROW_NUMBER () over (partition by TSPL_GATEPASS_MASTER_PRODUCTSALE.Total_Gross_Weight order by TSPL_GATEPASS_MASTER_PRODUCTSALE. GPCode ,Invoice_No) as Sno" & _
                                    ",TSPL_GATEPASS_DETAIL_ProductSale.Documnet_Amount ," & _
                                    " TSPL_GATEPASS_DETAIL_ProductSale.Provision_Amount" & _
                                    " from TSPL_GATEPASS_DETAIL_ProductSale" & _
                                    " left join TSPL_GATEPASS_MASTER_PRODUCTSALE on TSPL_GATEPASS_MASTER_PRODUCTSALE.GPCode =TSPL_GATEPASS_DETAIL_ProductSale.GPCode " & _
                                    " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_GATEPASS_MASTER_PRODUCTSALE.Location_Code " & _
                                    " left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_GATEPASS_MASTER_PRODUCTSALE.City_code " & _
                                    " left join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_GATEPASS_DETAIL_ProductSale.cust_code" & _
                                   " left join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_GATEPASS_MASTER_PRODUCTSALE.Vehicle_Id " & _
                                    " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id =TSPL_GATEPASS_MASTER_PRODUCTSALE.Transporter_code " & _
                                    "" & wrcls & "" & _
                                     ") as xx order by GPCode ,[Invoice No] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQry)

            Gv1.DataSource = Nothing
            Gv1.Columns.Clear()
            Gv1.Rows.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.EnableFiltering = True
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found to display")
            Else
                Gv1.DataSource = dt
                SetGridFormation()
                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.BestFitColumns()
                EnableDisableControls(False)
                ReStoreGridLayout()

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next


        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Gross Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Total Gross Weight", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Document Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Provision Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub txtTransporter__My_Click(sender As Object, e As EventArgs) Handles txtTransporter._My_Click
        Dim qry As String = " select Transport_Id as Code, Transporter_Name as Name from TSPL_TRANSPORT_MASTER"
        txtTransporter.arrValueMember = clsCommon.ShowMultipleSelectForm("GPMulTran", qry, "Code", "Name", txtTransporter.arrValueMember, txtTransporter.arrDispalyMember)
    End Sub

    Private Sub txtVehicle__My_Click(sender As Object, e As EventArgs) Handles txtVehicle._My_Click
        Dim qry As String = " select TSPL_VEHICLE_MASTER.Vehicle_Id as Code,TSPL_VEHICLE_MASTER.Description as Name  from TSPL_VEHICLE_MASTER "
        txtVehicle.arrValueMember = clsCommon.ShowMultipleSelectForm("GPMulVec", qry, "Code", "Name", txtVehicle.arrValueMember, txtVehicle.arrDispalyMember)
    End Sub

    Private Sub txtmultCustomer__My_Click(sender As Object, e As EventArgs) Handles txtmultCustomer._My_Click
        Dim qry As String = " select TSPL_CUSTOMER_MASTER.cust_code as Code,TSPL_CUSTOMER_MASTER.customer_Name as Name from TSPL_CUSTOMER_MASTER  "
        txtmultCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("GPMulCust", qry, "Code", "Name", txtmultCustomer.arrValueMember, txtmultCustomer.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        RefreshData()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub rm1SaveLayout_Click(sender As Object, e As EventArgs) Handles rm1SaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rm1DeleteLayout_Click(sender As Object, e As EventArgs) Handles rm1DeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub txtmultLocation__My_Click(sender As Object, e As EventArgs) Handles txtmultLocation._My_Click
        Dim qry As String = " select location_code as Code,Location_Desc  as Name from TSPL_LOCATION_MASTER    "
        txtmultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("GPMulLocation", qry, "Code", "Name", txtmultLocation.arrValueMember, txtmultLocation.arrDispalyMember)
    End Sub

    Private Sub RptGPDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Transaction")
        funreset()
    End Sub

    Private Sub RptGPDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            RefreshData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub txtmultCity__My_Click(sender As Object, e As EventArgs) Handles txtmultCity._My_Click
        Dim qry As String = " select City_Code as Code ,City_Name as Name  from TSPL_CITY_MASTER   "
        txtmultCity.arrValueMember = clsCommon.ShowMultipleSelectForm("GPMulCity", qry, "Code", "Name", txtmultCity.arrValueMember, txtmultCity.arrDispalyMember)
    End Sub
End Class
