Imports common
Public Class RptBoothNilDemandl
    Inherits FrmMainTranScreen
    Dim Slot1 As DateTime = Nothing
    Dim Slot2 As DateTime = Nothing
    Dim EnableProductSaleForJPR As Boolean = False

    Private Sub RptBoothNilDemandl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Addnew()
        EnableProductSaleForJPR = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableProductSaleForJPR, clsFixedParameterCode.EnableProductSaleForJPR, Nothing)) = 1, True, False)
        If EnableProductSaleForJPR Then
            rbtnIceCream.Visible = True
            rdbDemandBoth.Visible = False
        Else
            rbtnIceCream.Visible = False
            rdbDemandBoth.Visible = True
        End If
    End Sub
    Private Sub rdbDay_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDay.CheckedChanged
        If rdbDay.Checked Then
            RadGroupBox1.Visible = True
            RadLabel2.Visible = False
            ToDate.Visible = False
            monthlyDate.Visible = False
        Else
            RadGroupBox1.Visible = False
            RadLabel2.Visible = True
            ToDate.Visible = True
            monthlyDate.Visible = False
            rbnmorning.Checked = False
            rbnEvening.Checked = False
        End If
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim Whr As String = ""
            Dim Routewhr As String = ""
            Dim status As String = ""
            If txtrouteNo.arrValueMember IsNot Nothing AndAlso txtrouteNo.arrValueMember.Count > 0 Then
                Routewhr = " and  Route_No in (" + clsCommon.GetMulcallString(txtrouteNo.arrValueMember) + ")  "
            End If
            If rdbActive.Checked Then
                status += " and Status='N' "
            ElseIf rdbInActive.Checked Then
                status += " and Status='Y' "
            End If
            'If rdbMilk.Checked Then
            '    Whr += "and TSPL_DEMAND_BOOKING_MASTER.ItemType='Fresh' "
            'ElseIf rdbProduct.Checked Then
            '    Whr += "and TSPL_DEMAND_BOOKING_MASTER.ItemType='Product' "
            'ElseIf rdbDemandBoth.Checked Then
            '    Whr += " and TSPL_DEMAND_BOOKING_MASTER.ItemType='Both' "
            'End If
            If Not EnableProductSaleForJPR Then
                If rbnEvening.Checked Then
                    Whr += "and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Evening' "
                ElseIf rbnmorning.Checked Then
                    Whr += " and TSPL_DEMAND_BOOKING_MASTER.ShiftType='Morning' "
                End If
            End If
            If rdbDay.Checked Then
                Whr += "  and convert(date,document_date,103)='" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value)) + "' "
            ElseIf rdbRangeWise.Checked Then
                Whr += "and convert(date,document_date,103)>='" + fromDate.Value + "' and convert(date,document_date,103)<='" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value)) + "' "
            End If
            If rdbMonth.Checked Then
                Whr += "and convert(date,document_date,103)>='" + Slot1 + "' and convert(date,document_date,103)<='" + clsCommon.myCstr(clsCommon.GetPrintDate(Slot2)) + "' "
            End If

            If EnableProductSaleForJPR Then
                If rdbProduct.Checked Then
                    Whr += " and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.ItemType='Product' "
                ElseIf rbtnIceCream.Checked Then
                    Whr += " and TSPL_PRODUCT_DEMAND_BOOKING_MASTER.ItemType='IceCream' "
                End If
            End If
            Dim Qry As String = " select row_number() over(order by(select 1)) as SNo,Cust_Code as [Booth Code],customer_name as [Booth Name],Route_No as [Route No],Status,tspl_customer_master.Phone1 as [Mobile No] from tspl_customer_master where " + Routewhr + "   not exists  ( "
            If rdbMilk.Checked Then
                Qry += " Select  TSPL_DEMAND_BOOKING_DETAIL.Cust_Code from TSPL_DEMAND_BOOKING_MASTER
left join TSPL_DEMAND_BOOKING_DETAIL on TSPL_DEMAND_BOOKING_DETAIL.document_no=TSPL_DEMAND_BOOKING_MASTER.Document_No  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
where  tspl_customer_master.Cust_Code=TSPL_DEMAND_BOOKING_DETAIL.Cust_Code " + Whr + "   " + Routewhr + ")"
            End If
            If EnableProductSaleForJPR Then
                If rdbProduct.Checked OrElse rbtnIceCream.Checked Then
                    Qry += " Select  TSPL_PRODUCT_DEMAND_BOOKING_detail.Cust_Code from TSPL_PRODUCT_DEMAND_BOOKING_MASTER
left join TSPL_PRODUCT_DEMAND_BOOKING_detail on TSPL_PRODUCT_DEMAND_BOOKING_detail.document_no=TSPL_PRODUCT_DEMAND_BOOKING_MASTER.Document_No  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_PRODUCT_DEMAND_BOOKING_detail.Item_Code
where  tspl_customer_master.Cust_Code=TSPL_PRODUCT_DEMAND_BOOKING_detail.Cust_Code " + Whr + "   " + Routewhr + ")"
                End If
            End If
            Qry += " And tspl_customer_master.IsDistributor='N'   " + status + " "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Gv1.DataSource = Nothing
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
                Gv1.EnableFiltering = True
                SetGridFormat()
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        rdbMilk.Checked = True

    End Sub
    Sub Addnew()
        rdbDay.Checked = True
        rdbRangeWise.Checked = False
        rdbMonth.Checked = False
        RadGroupBox1.Visible = True
        RadLabel2.Visible = True
        ToDate.Visible = True
        RadLabel2.Visible = False
        ToDate.Visible = False
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        monthlyDate.Value = clsCommon.GETSERVERDATE()
        rbnmorning.Checked = True
        rbnEvening.Checked = False
        rdbMilk.Checked = True
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub rdbMonth_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMonth.CheckedChanged
        If rdbMonth.Checked Then
            ToDate.Visible = False
            fromDate.Visible = False
            RadLabel2.Visible = False
            monthlyDate.Visible = True
            rbnmorning.Checked = False
            rbnEvening.Checked = False
            monthlyDate.Location = New System.Drawing.Point(72, 48)
        Else
            ToDate.Visible = True
            fromDate.Visible = True
            RadLabel2.Visible = True
            monthlyDate.Visible = False
        End If
    End Sub
    Sub SetGridFormat()
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv1.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = True
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.Columns("SNo").Name = "SNo"
        Gv1.Columns("SNo").IsVisible = True '

        Gv1.Columns("Booth Code").HeaderText = "Booth Code"
        Gv1.Columns("Booth Code").Width = 500
        Gv1.Columns("Booth Code").IsVisible = True

        Gv1.Columns("Booth Name").HeaderText = "Booth Name"
        Gv1.Columns("Booth Name").Width = 500
        Gv1.Columns("Booth Name").IsVisible = True

        Gv1.Columns("Route No").HeaderText = "Route No"
        Gv1.Columns("Route No").Width = 500
        Gv1.Columns("Route No").IsVisible = False



        Gv1.Columns("Status").HeaderText = "Status"
        Gv1.Columns("Status").Width = 200

        Gv1.Columns("Mobile No").HeaderText = "Mobile No"
        Gv1.Columns("Mobile No").Width = 500


        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub monthlyDate_ValueChanged(sender As Object, e As EventArgs) Handles monthlyDate.ValueChanged
        Try
            Dim SM As Integer = monthlyDate.Value.Month
            Dim SY As Integer = monthlyDate.Value.Year

            Dim CD As New DateTime(SY, SM, 1)
            Slot1 = clsCommon.GetPrintDate(CD, "dd/MMM/yyyy")
            MonthSupplyDate()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub MonthSupplyDate()
        If clsCommon.myLen(monthlyDate.Value) > 0 Then
            Dim SM As Integer = monthlyDate.Value.Month
            Dim SY As Integer = monthlyDate.Value.Year
            Dim CD As New DateTime(SY, SM, 1)
            Slot2 = clsCommon.GetPrintDate(CD.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
        End If
    End Sub
    Private Sub txtrouteNo__My_Click(sender As Object, e As EventArgs) Handles txtrouteNo._My_Click
        Dim strQry As String = " Select TSPL_ROUTE_MASTER.Route_No AS Code,TSPL_ROUTE_MASTER.Route_Desc as Name from TSPL_ROUTE_MASTER "
        txtrouteNo.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtrouteNo.arrValueMember, txtrouteNo.arrDispalyMember)
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDemandNill & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            If rdbDay.Checked = False Then
                arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            Else
                arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy"))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class