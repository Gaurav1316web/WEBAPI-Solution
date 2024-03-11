Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
' Ticket No : ERO/03/05/19-000583 By Prabhakar For - Create New Report 
Public Class rptJWDispatchReport
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try

            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                clsCommon.MyMessageBoxShow(Me, "To Date cant be less than from date", Me.Text)
                Exit Sub
            End If

            Dim qry As String = Nothing

            qry = " Select tspl_scrapsale_head.Shipment_No as [Document No],Convert (varchar,tspl_scrapsale_head.Shipment_Date ,103) as [Document Date],tspl_scrapsale_head.Loc_Code as [Location Code],tspl_Location_Master.Location_Desc as [Location Desc],tspl_scrapsale_head.Cust_Code as [Customer Code],TSPL_Customer_Master.Customer_Name as  [Customer Name],tspl_scrapsale_head.Vehicle_Code as [Vehicle Code],tspl_scrapsale_head.Description as [Description],tspl_scrapsale_head.Reff as [Reference],TSPL_SCRAPSALE_DETAIL.Item_code as [Item Code],tspl_Item_Master.Item_Desc as [Item Description],tspl_Item_Master.HSN_Code as [HSN Code],TSPL_SCRAPSALE_DETAIL.Unit_Code as [UOM],TSPL_SCRAPSALE_DETAIL.Shipped_Qty as [Qty] from TSPL_SCRAPSALE_DETAIL " & _
                  " left outer join tspl_scrapsale_head  on TSPL_SCRAPSALE_DETAIL.Shipment_No = tspl_scrapsale_head.Shipment_No " & _
                  " left outer join tspl_Item_Master on tspl_Item_Master.Item_Code = TSPL_SCRAPSALE_DETAIL.Item_Code " & _
                  " left outer join tspl_Location_Master on tspl_Location_Master.Location_Code = tspl_scrapsale_head.Loc_Code " & _
                  " left outer join tspl_Customer_Master on TSPL_Customer_Master.cust_code = tspl_scrapsale_head.Cust_Code " & _
                  " where 2 =2 "
           



            If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                qry += " and tspl_scrapsale_head.Cust_Code in (" + clsCommon.GetMulcallString(txtVendor.arrValueMember) + ")  "
            End If

            If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                qry += " and tspl_scrapsale_head.Loc_Code in (" + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember) + ")  "
            End If

            qry += " and tspl_scrapsale_head.Shipment_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and tspl_scrapsale_head.Shipment_Date <='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' "
            qry += " order by convert (date, tspl_scrapsale_head.Shipment_Date,103) asc "


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
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()
            gv1.BestFitColumns()

            ReStoreGridLayout()


        Catch ex As Exception

        End Try
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

    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtVendor.arrValueMember = Nothing
        TxtMultiToLocation.arrValueMember = Nothing
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv1
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


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()

    End Sub


    Private Sub TxtMultiToLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiToLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
        TxtMultiToLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiTo", qry, "Code", "Name", TxtMultiToLocation.arrValueMember, TxtMultiToLocation.arrDispalyMember)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Name : Job wrok dispatch Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                End If


                If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                End If
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
                arrHeader.Add("Name :Job wrok dispatch Report")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                End If

                If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Job wrok dispatch Report", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
        Dim qry As String = "select tspl_Customer_Master.Cust_Code as [Code] ,tspl_Customer_Master.Customer_Name as [Name] from tspl_Customer_Master "
        txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiCustomer", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    End Sub
End Class
