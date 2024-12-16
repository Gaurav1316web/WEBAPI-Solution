Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Public Class RptBmcTankerInOutReport
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Sub Reset()
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Getdata(True)
    End Sub

    'Private Sub LoadData()
    '    Try
    '        Dim dt As DataTable = New DataTable()
    '        Dim BaseQry As String = "Select Document_No as  Weighment_No,Document_Date as Weighment_Date,TSPL_PLANT_WEIGHMENT.Tanker_No as Vehicle_No_Manual,Tare_Weight,Gross_Weight,Net_Weight , TSPL_COMPANY_MASTER.comp_code[Company Code] , TSPL_COMPANY_MASTER.Comp_Name[Comp Desc] , CONCAT(TSPL_COMPANY_MASTER.Add1, ' ', TSPL_COMPANY_MASTER.Add2, ' ', 
    '                                 TSPL_COMPANY_MASTER.Add3 , ' , ', TSPL_COMPANY_MASTER.State ) as [Company Address], Remarks, coalesce(Tare_Weight_date, Document_Date) as Tare_Weight_date,Document_Date as Gross_Weight_date,TSPL_COMPANY_MASTER.GSTReg_No,TSPL_PLANT_WEIGHMENT.Gate_Entry_No,TSPL_PLANT_WEIGHMENT.Document_Date as Gate_entry_Date,TSPL_COMPANY_MASTER.Pincode as state_code,TSPL_COMPANY_MASTER.City_Code as Route_name,tspl_gate_entry_details.Challan_No as Challan_No,
    '                                 tspl_gate_entry_details.Item_Desc as Item_Description,tspl_gate_entry_details.UOM as Uom,TSPL_BULK_ROUTE_MASTER.ROUTE_NO as Route_No,TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as New from TSPL_PLANT_WEIGHMENT  LEFT JOIN  TSPL_COMPANY_MASTER on 2 = 2 
    '                                 left outer join tspl_gate_entry_details on tspl_gate_entry_details.Gate_Entry_No=TSPL_PLANT_WEIGHMENT.Gate_Entry_No  
    '                                 left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.Route_No=tspl_gate_entry_details.ROUTE_NO where tspl_gate_entry_details.Date_And_Time= '2024-11-07'  "
    '        dt = clsDBFuncationality.GetDataTable(finalQuery)
    '        Gv1.DataSource = Nothing
    '        Gv1.Rows.Clear()
    '        Gv1.Columns.Clear()
    '        Gv1.GroupDescriptors.Clear()
    '        Gv1.MasterView.Refresh()
    '        If dt.Rows.Count > 0 Then
    '            Gv1.DataSource = dt
    '            Gv1.GroupDescriptors.Clear()
    '            Gv1.EnableFiltering = True
    '            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    '            Gv1.MasterTemplate.AutoExpandGroups = True
    '            RadPageView1.SelectedPage = RadPageViewPage2
    '            Gv1.BestFitColumns()
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub



    Public Sub Getdata(ByVal print As Boolean)
        Try
            Dim strqry As String = ""
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            strqry = "  SELECT 
                        TSPL_BULK_ROUTE_MASTER.ROUTE_NO AS Route_No,
                        TSPL_BULK_ROUTE_MASTER.ROUTE_NAME as Route_Name,
                        Tspl_Gate_Entry_Details.Tanker_No AS Tanker_No,
                        	    FORMAT(TSPL_BULK_ROUTE_MASTER.Schedule_Time, 'hh:mm tt') AS Schedule_Time,

                         FORMAT(tspl_gate_entry_details.Date_And_Time, 'hh:mm tt') AS In_date,
                            FORMAT(TRY_CAST(tspl_gate_out.Created_Date AS DATETIME), 'hh:mm tt')AS Out_date,

	  CASE 
        WHEN FORMAT(tspl_gate_entry_details.Date_And_Time, 'hh:mm tt') <  FORMAT(TSPL_BULK_ROUTE_MASTER.Schedule_Time, 'hh:mm tt')
 THEN '00:00'
        ELSE RIGHT('00' + CAST(DATEPART(HOUR, DATEADD(MINUTE, DateDiff(MINUTE, TSPL_BULK_ROUTE_MASTER.Schedule_Time, tspl_gate_entry_details.Date_And_Time), '00:00:00')) AS VARCHAR(2)), 2) + ':' +
             RIGHT('00' + CAST(DATEPART(MINUTE, DATEADD(MINUTE, DateDiff(MINUTE, TSPL_BULK_ROUTE_MASTER.Schedule_Time, tspl_gate_entry_details.Date_And_Time), '00:00:00')) AS VARCHAR(2)), 2)
    END AS Time_Diff
,
	
                        '' AS Remark  from Tspl_Gate_Entry_Details  LEFT JOIN  TSPL_COMPANY_MASTER on 2 = 2  
                        left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.Route_No=tspl_gate_entry_details.ROUTE_NO
                        left outer join tspl_gate_out on tspl_gate_out.Gate_Entry_No=tspl_gate_entry_details.Gate_Entry_No
                        where convert(date,tspl_gate_entry_details.Date_And_Time,103)>=convert(date,('" + fromDate.Value + "'),103) and convert(date,tspl_gate_entry_details.Date_And_Time,103)<=convert(date,('" + ToDate.Value + "'),103)"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strqry)
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()

                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.BestFitColumns()
                Gv1.EnableFiltering = True
            End If

            'ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub RptBmcTankerInOutReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
    End Sub


    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))
            arrHeader.Add("Bmc Tanker In Out Report")


            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            'End If


            If Gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.VendorPaymentDetails & "'"))



            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember) + " "))
            'End If

            If Gv1.Rows.Count > 0 Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Bmc Tanker In Out Report", Gv1, arrHeader, "Bmc Tanker In Out Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data found", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class
