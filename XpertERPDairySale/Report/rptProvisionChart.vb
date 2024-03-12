Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
' Ticket No : ERO/16/05/19-000606 By Prabhakar - Create New report
'=====Update by preeti gupta[ERO/17/10/19-001066]
Public Class rptProvisionChart
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isShowEmployeeCurrentSalary As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
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

            qry = " select Cast(1 as BIT) as 'Check' ,TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode as [Gate Pass No] ,Convert ( varchar,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) as [Gate Pass Date],TSPL_PROVISION_ENTRY .Doc_No as [Document code],Convert ( varchar, TSPL_PROVISION_ENTRY.Doc_Date,103) as Date,TSPL_PROVISION_ENTRY_KNOCKOFF.AP_Invoice_No as [AP Invoice No], TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id  as [Vehicle Id], TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number as [Vehicle Number],TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter as [Transporter Id] , TSPL_TRANSPORT_MASTER.Transporter_Name as [Transporter Name],TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route as [Fixed KM] ,isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0) as [Running KM] , case when isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route,0) <  (isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0))  then  isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route,0) else (isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0))  end as  [Running / Fixed Which ever is less], TSPL_DAIRYSALE_GATEPASS_MASTER.Price_KM_In_Vehicle as [Rate/KM],isnull (TSPL_PROVISION_ENTRY.Toll_Amt,0) as [Toll Amount],TSPL_PROVISION_ENTRY.Amount   from  TSPL_PROVISION_ENTRY " &
                  " inner join TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_PROVISION_ENTRY.Ref_Doc_No = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode " &
                  " left Outer Join TSPL_TRANSPORT_MASTER on TSPL_TRANSPORT_MASTER.Transport_Id = TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter " &
                  " left outer join TSPL_PROVISION_ENTRY_KNOCKOFF on TSPL_PROVISION_ENTRY_KNOCKOFF.Provision_No = TSPL_PROVISION_ENTRY.Doc_No "

            qry += " where 2=2 "
            '=============added by preeti gupta Against ticket no[ERO/18/09/19-001032]===
            If rdbGatePassDate.Checked = True Then
                qry += " and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            Else
                qry += " and convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            End If

            If txtVehicleNo.arrValueMember IsNot Nothing AndAlso txtVehicleNo.arrValueMember.Count > 0 Then
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id in (" + clsCommon.GetMulcallString(txtVehicleNo.arrValueMember) + ")  "
            End If

            If txtTransporter.arrValueMember IsNot Nothing AndAlso txtTransporter.arrValueMember.Count > 0 Then
                qry += " and TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter in (" + clsCommon.GetMulcallString(txtTransporter.arrValueMember) + ")  "
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
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If

            gv1.DataSource = dt
            SetGridFormationOFGV1()
            FormatGrid()
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

        gv1.Columns("Check").IsVisible = True
        gv1.Columns("Check").Width = 100
        gv1.Columns("Check").HeaderText = " "
        gv1.Columns("Check").ReadOnly = False

        gv1.Columns("Gate Pass No").IsVisible = True
        gv1.Columns("Gate Pass No").Width = 100
        gv1.Columns("Gate Pass No").HeaderText = "Gate Pass No"



        gv1.Columns("Gate Pass Date").IsVisible = True
        gv1.Columns("Gate Pass Date").Width = 100
        gv1.Columns("Gate Pass Date").HeaderText = "Gate Pass Date"

        gv1.Columns("Document code").IsVisible = True
        gv1.Columns("Document code").Width = 100
        gv1.Columns("Document code").HeaderText = "Document Code"



        gv1.Columns("Date").IsVisible = True
        gv1.Columns("Date").Width = 100
        gv1.Columns("Date").HeaderText = " Date"


        gv1.Columns("AP Invoice No").IsVisible = False
        gv1.Columns("AP Invoice No").Width = 100
        gv1.Columns("AP Invoice No").HeaderText = "AP Invoice No"



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

        gv1.Columns("Toll Amount").IsVisible = True
        gv1.Columns("Toll Amount").Width = 100
        gv1.Columns("Toll Amount").HeaderText = "Toll Amount"

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
        PageSetupReport_ID = MyBase.Form_ID
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


    Private Sub rptTankerStatusReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        Reset()

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
                arrHeader.Add("Name : Provision Chart")
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
                arrHeader.Add("Name : Provision Chart")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                'If txtVendor.arrValueMember IsNot Nothing AndAlso txtVendor.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Vendor : " + clsCommon.GetMulcallString(txtVendor.arrValueMember))
                'End If

                'If TxtMultiToLocation.arrValueMember IsNot Nothing AndAlso TxtMultiToLocation.arrValueMember.Count > 0 Then
                '    arrHeader.Add("To Location : " + clsCommon.GetMulcallString(TxtMultiToLocation.arrValueMember))
                'End If
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Provision Chart", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub txtVendor__My_Click(sender As Object, e As EventArgs) Handles txtVendor._My_Click
    '    Dim qry As String = "select TSPL_VENDOR_MASTER.Vendor_Code as [Code] ,TSPL_VENDOR_MASTER.Vendor_Name as [Name] from TSPL_VENDOR_MASTER "
    '    txtVendor.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiVendor", qry, "Code", "Name", txtVendor.arrValueMember, txtVendor.arrDispalyMember)
    'End Sub

    Private Sub txtVehicleNo__My_Click(sender As Object, e As EventArgs) Handles txtVehicleNo._My_Click
        Dim qry As String = " Select TSPL_VEHICLE_MASTER.Vehicle_Id as [Code] ,TSPL_VEHICLE_MASTER.Number as [Name] from TSPL_VEHICLE_MASTER "
        txtVehicleNo.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@VehicleNo", qry, "Code", "Name", txtVehicleNo.arrValueMember, txtVehicleNo.arrDispalyMember)
    End Sub

    Private Sub txtTransporter__My_Click(sender As Object, e As EventArgs) Handles txtTransporter._My_Click
        Dim qry As String = " Select TSPL_TRANSPORT_MASTER.Transport_Id as [Code] ,TSPL_TRANSPORT_MASTER.Transporter_Name as [Name] from TSPL_TRANSPORT_MASTER "
        txtTransporter.arrValueMember = clsCommon.ShowMultipleSelectForm("MulSel@Transporter", qry, "Code", "Name", txtTransporter.arrValueMember, txtTransporter.arrDispalyMember)
    End Sub
    ' Ticket No : ERO/27/05/19-000622 By prabhakar
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            ArrInvoice_Arr = New ArrayList
            Dim Provistion_Code As String = ""
            Dim frmCRV As New frmCrystalReportViewer()
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                    Provistion_Code = Provistion_Code + "','" + clsCommon.myCstr(grow.Cells("Document code").Value)
                End If
            Next

            If clsCommon.myLen(Provistion_Code) > 0 AndAlso clsCommon.myCstr(Provistion_Code).Substring(0, 3) = "','" Then
                Provistion_Code = Provistion_Code.Substring(3, Provistion_Code.Length - 3)
            End If

            Dim strFromDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy")
            Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
            Dim qry As String = "  Select *,tspl_company_master.logo_img from ( " & _
                                "  select TSPL_PROVISION_ENTRY.Comp_Code,'1' as CopyType, row_number() over(partition by TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter order by TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter ) as SNo,DateName( month , DateAdd( month , Convert (integer, TSPL_PROVISION_ENTRY.Prov_Month) , 0 ) - 1 ) as Prov_Month, Convert (varchar, TSPL_PROVISION_ENTRY.Prov_Year) as Prov_Year,'" + strFromDate + "' as From_Date, '" + strToDate + "' as To_Date,TSPL_PROVISION_ENTRY.Loc_Code,TSPL_Location_Master.Location_Desc,TSPL_Location_Master.Loc_Short_Name as Location_Short_Name , TSPL_Location_Master.Add1 as Location_Add1, TSPL_Location_Master.Add2 as Location_Add2 , TSPL_Location_Master.Add3 as Location_Add3, TSPL_Location_Master.City_Code as Location_City_Code, TSPL_Location_Master.State as Location_State, TSPL_Location_Master.Pin_Code as Location_Pin_Code,TSPL_Location_Master.Telphone as Loc_Telphone, TSPL_Location_Master.Email as Location_Email, " & _
                                "  TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode , Convert ( varchar,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) as [Gate Pass Date],TSPL_PROVISION_ENTRY .Doc_No ,Convert ( varchar, TSPL_PROVISION_ENTRY.Doc_Date,103) as Date, TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id  as [Vehicle Id], TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Number as [Vehicle Number],TSPL_VEHICLE_MASTER.Model as Vehicle_Model,TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter as Sale_Invoice_No , TSPL_VENDOR_MASTER.Vendor_Name as [Transporter Name], " & _
                                "  TSPL_VENDOR_MASTER.IFSC_CODE as Transporter_IFSC_CODE,TSPL_VENDOR_MASTER.branch_code,TSPL_VENDOR_MASTER.Branch_Name as Transporter_Branch_Name,TSPL_VENDOR_MASTER.Bank_Code, TSPL_VENDOR_MASTER.Bank_Name as Transporter_Bank_Name,TSPL_VENDOR_MASTER.Account_No as Transporter_Account_No,TSPL_VENDOR_MASTER.PAN as Transporter_PAN,TSPL_VENDOR_MASTER.Phone1 as Transporter_Phone1 " & _
                                "  ,TSPL_PROVISION_ENTRY.Route_Code as [Route Code],TSPL_ROUTE_MASTER.Route_Desc as [Route Desc],TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route as [Fixed KM] ,isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0) as [Running KM] , case when isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route,0) <  (isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0))  then  isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route,0) else (isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0))  end as  [Running / Fixed Which ever is less], TSPL_DAIRYSALE_GATEPASS_MASTER.Price_KM_In_Vehicle as [Rate/KM],(TSPL_PROVISION_ENTRY.Amount - isnull (TSPL_PROVISION_ENTRY.Toll_Amt,0)) as  Amount_Without_toll,isnull (TSPL_PROVISION_ENTRY.Toll_Amt,0) as Toll_Amt,TSPL_PROVISION_ENTRY.Amount   from  TSPL_PROVISION_ENTRY  inner join TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_PROVISION_ENTRY.Ref_Doc_No = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode  left Outer Join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = TSPL_DAIRYSALE_GATEPASS_MASTER.Transporter " & _
                                "  left Outer Join TSPL_ROUTE_MASTER on  TSPL_ROUTE_MASTER.Route_No = TSPL_PROVISION_ENTRY.Route_Code " & _
                                "  left Outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_id = TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id  " & _
                                "  left Outer Join TSPL_Location_Master on TSPL_Location_Master.Location_Code  =TSPL_PROVISION_ENTRY.Loc_Code " & _
                                "  where 2=2  and TSPL_PROVISION_ENTRY .Doc_No in ('" + Provistion_Code + "') "

            If rdbGatePassDate.Checked = True Then
                qry += " and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            Else
                qry += " and convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) >= convert(date,('" + txtFromDate.Value + "'),103) and convert(date,TSPL_PROVISION_ENTRY.Doc_Date,103) <= convert(date,('" + txtToDate.Value + "'),103) "
            End If

            qry += " ) XXX left join tspl_company_master on tspl_company_master.comp_code=XXX.comp_code LEFT OUTER JOIN (Select '1' as COL1, 1 as COL2,  'ORIGINA' as CopyType1  " & _
                                "  " & _
                                " ) YYY ON YYY.COL1=XXX.CopyType  ORDER BY YYY.COL2 "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "crptProvistionChartPrint", "Transport Bill ")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv1.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub
End Class
