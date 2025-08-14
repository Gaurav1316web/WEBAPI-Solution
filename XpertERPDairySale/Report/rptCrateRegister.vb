Imports System.IO
Imports common
Public Class rptCrateRegister
    Inherits FrmMainTranScreen
    Private Sub rptCrateRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub MultLocation__My_Click(sender As Object, e As EventArgs) Handles MultLocation._My_Click
        Try
            'Dim qry As String = "select Cust_Code as Code ,Customer_Name as  Name from TSPL_CUSTOMER_MASTER "

            'If txtRoute.arrValueMember IsNot Nothing Then
            '    qry += "where Route_No in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            'End If
            Dim qry As String = " select CUST_CODE,Customer_Name from TSPL_CUSTOMER_MASTER"
            MultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "CUST_CODE", "CUST_CODE", MultLocation.arrValueMember, MultLocation.arrDispalyMember)
            ' MultLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LedgerCustomer", qry, "Code", "Name", MultLocation.arrValueMember, MultLocation.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MultArea__My_Click(sender As Object, e As EventArgs) Handles MultArea._My_Click
        Dim qry As String = " select Route_No,Route_Desc from tspl_route_master "
        MultArea.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Route_No", "Route_No", MultArea.arrValueMember, MultArea.arrDispalyMember)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False)
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try

            MultLocation.Enabled = False
            MultArea.Enabled = False
            txtFromDate.Enabled = False
            txtToDate.Enabled = False
            Dim FromDate As String = clsCommon.myCstr(txtFromDate.Text)
            Dim ToDate As String = clsCommon.myCstr(txtToDate.Text)
            Dim qry As String = Nothing
            Dim whrcls As String = ""
            Dim whrclsClosing As String = ""
            Dim whrclss As String = ""

            'whrclss = "    Document_Date >= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") & "' AND " & "Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") & "' "
            whrclss = " convert(date, Document_Date, 108) >= convert(date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") & "', 108) " &
          "AND convert(date, Document_Date, 108) <= convert(date, '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(ToDate), "dd/MMM/yyyy hh:mm:ss tt") & "', 108)"

            'whrcls = "where 2 = 2 and convert(date,Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            'whrclsClosing = "where 2 = 2 and convert(date,Document_Date,103)<convert(date,'" + txtFromDate.Value + "',103) "
            whrclsClosing = "  Document_Date <= '" & clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate), "dd/MMM/yyyy hh:mm:ss tt") & "'"


            Dim whrclsCust As String = Nothing
            Dim whrclsRoute As String = Nothing

            If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
                whrclsCust += "  and TSPL_CRATE_RECEIVED_detail_FRESHSALE.Customer_Code in (" + clsCommon.GetMulcallString(MultLocation.arrValueMember) + ")  "

            End If

            If MultArea.arrValueMember IsNot Nothing AndAlso MultArea.arrValueMember.Count > 0 Then
                whrclsRoute += " and tspl_route_master.Route_No in (" + clsCommon.GetMulcallString(MultArea.arrValueMember) + ")"
            End If
            qry = " WITH my_cte AS (
                      select ROW_NUMBER() over (Partition by 1 order by Sale_Invoice_Date) as SNO , * from (
                      select max(Customer_Name)Customer_Name,max(Comp_Name)Comp_Name,max(Location_Desc)Location_Desc,max(Location_Code)Location_Code, max(Vehicle_Id)Vehicle_Id,
                      max(Vehicle_Number)Vehicle_Number,(Route_No)Route_No,max(Route_Desc)Route_Desc,
                      (Customer_Code)Customer_Code,Sale_Invoice_Date ,
					  					  sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='M' then 1 else 0 end * case when Type='O' then 1 else 0 end ) as  Morning_Supply,
                      sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='M' then 1 else 0 end * case when Type='I' then 1 else 0 end ) as  Morning_Return,
                      sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='E' then 1 else 0 end * case when Type='O' then 1 else 0 end ) as  Evening_Supply,
                      sum(Qty * case when RI=1 THEN 1 else 0 end * case when ShiftType='E' then 1 else 0 end * case when Type='I' then 1 else 0 end ) as  Evening_Return
					                        from (
                      select TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.type, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No, TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code AS Vehicle_Id,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.VehicleNo AS Vehicle_Number, TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Route_code as Route_No,  TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.ShiftType,TSPL_route_master.Route_Desc,
                      TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name, 
                      CAST(TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Invoice_Date AS DATE) AS Sale_Invoice_Date,
                      TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd as Qty ,1 as RI,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code,
                      TSPL_LOCATION_MASTER.Location_Desc,TSPL_COMPANY_MASTER.Comp_Name 
                      From TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE
                      left outer join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No
                      left outer join tspl_route_master on tspl_route_master.route_No=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.route_Code 
					  left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code
					  LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Comp_Code 
                      left outer join tspl_customer_master on tspl_customer_master.Cust_Code = TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code
                  where 2=2 " + whrclsRoute + " " + whrclsCust + "
                  
                      )xx where 2=2
                      group by Sale_Invoice_Date,Customer_Code,Route_No
                      )xxx )
                         select '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as fromdate,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy") + "' as ToDate,
                     Comp_Name,Customer_Code,Customer_Name,Route_No,Route_Desc,
					        (OP+((Morning_Supply+Evening_Supply)-(Morning_Return+Evening_Return))) as CL ,
							Evening_Supply+Morning_Supply as Supply1,
						Evening_Return+Morning_Return as Return1,
Sale_Invoice_Date,OP from  (

               
                      select  (select isnull(sum((Morning_Supply+Evening_Supply)-(Morning_Return+Evening_Return)),0)  from my_cte as InnCTE 
                      where InnCTE.Sale_Invoice_Date<my_cte.Sale_Invoice_Date) as OP,* from my_cte 
                      where Sale_Invoice_Date>= '" + clsCommon.GetPrintDate(txtFromDate.Value) + "'  and Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtToDate.Value) + "') xx
                      order by xx.Sale_Invoice_Date asc"



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                If print = False Then
                    gv2.DataSource = Nothing
                    gv2.Rows.Clear()
                    gv2.Columns.Clear()
                    gv2.GroupDescriptors.Clear()
                    gv2.MasterTemplate.SummaryRowsBottom.Clear()
                    gv2.MasterView.Refresh()
                    gv2.DataSource = dt
                    For ii As Integer = 0 To gv2.Columns.Count - 1
                        gv2.Columns(ii).ReadOnly = True
                    Next
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv2.EnableFiltering = True
                    SetGridFormat1()
                    gv2.BestFitColumns()
                Else
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.KwalitySalesReport, dt, "rptCrateRegister", "")

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            '    gv2.DataSource = Nothing
            '    gv2.Rows.Clear()
            '    gv2.Columns.Clear()
            '    gv2.GroupDescriptors.Clear()
            '    gv2.MasterTemplate.SummaryRowsBottom.Clear()
            '    gv2.MasterView.Refresh()
            '    gv2.DataSource = dt
            '    For ii As Integer = 0 To gv2.Columns.Count - 1
            '        gv2.Columns(ii).ReadOnly = True
            '    Next
            '    RadPageView1.SelectedPage = RadPageViewPage2
            '    gv2.EnableFiltering = True
            '    SetGridFormat1()
            '    gv2.BestFitColumns()
            'Else
            '    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            '    Exit Sub
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat1()
        gv2.TableElement.TableHeaderHeight = 40
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."

            'gv2.Columns("Document_No").IsVisible = False
            'gv2.Columns("Document_No").HeaderText = "Document No"
            'gv2.Columns("Document_Date").IsVisible = False
            'gv2.Columns("Document_Date").HeaderText = "Document Date"
            'gv2.Columns("DistributorCode").IsVisible = True
            gv2.Columns("Customer_Code").HeaderText = "Distributor Code"
            gv2.Columns("Customer_Code").IsVisible = True
            gv2.Columns("Customer_Name").HeaderText = "Distributor Name"
            gv2.Columns("Route_No").IsVisible = True
            gv2.Columns("Route_No").HeaderText = "Area Code"
            gv2.Columns("Route_Desc").IsVisible = False
            gv2.Columns("Route_Desc").HeaderText = "Area Name"
            gv2.Columns("Supply1").IsVisible = True
            gv2.Columns("Supply1").HeaderText = "Supply"
            gv2.Columns("Return1").IsVisible = True
            gv2.Columns("Return1").HeaderText = "Return Qty"
            gv2.Columns("CL").IsVisible = True
            gv2.Columns("CL").HeaderText = "Closing Balance"
            gv2.Columns("OP").IsVisible = True
            gv2.Columns("OP").HeaderText = "Opening Balance"

            'gv2.Columns("Comp_Code").IsVisible = False
            'gv2.Columns("Comp_Code").HeaderText = "Comp_Code"
            gv2.Columns("Comp_Name").IsVisible = False
            gv2.Columns("Comp_Name").HeaderText = "Comp_Name"
            gv2.Columns("FromDate").IsVisible = False
            gv2.Columns("FromDate").HeaderText = "FromDate"
            gv2.Columns("ToDate").IsVisible = False
            gv2.Columns("ToDate").HeaderText = "ToDate"
            gv2.Columns("Sale_Invoice_Date").IsVisible = False
            gv2.Columns("Sale_Invoice_Date").HeaderText = "Sale Invoice Date"
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()

        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)

        Dim Supply As New GridViewSummaryItem("Supply", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Supply)
        Dim ReturnQty As New GridViewSummaryItem("ReturnQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(ReturnQty)
        Dim CloseingBal As New GridViewSummaryItem("CloseingBal", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(CloseingBal)
        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv2.AutoSizeRows = True
        gv2.BestFitColumns()
        gv2.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv2.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCrateRegister & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

                If MultLocation.arrValueMember IsNot Nothing AndAlso MultLocation.arrValueMember.Count > 0 Then
                    arrHeader.Add("Distrutor Code : " & MultLocation.arrDispalyMember(0))
                End If

                If MultArea.arrValueMember IsNot Nothing AndAlso MultArea.arrValueMember.Count > 0 Then
                    arrHeader.Add("Area : " & MultArea.arrDispalyMember(0))

                End If
                If exporter = EnumExportTo.Excel Then
                    'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.exportdata(gv2, "", Me.Text, , arrHeader, False, False, True)
                End If
                'transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub
    Private Sub Reset()
        'txtMultBmc.Enabled=True
        MultLocation.arrValueMember = Nothing
        MultArea.arrValueMember = Nothing
        txtFromDate.Enabled = True
        txtToDate.Enabled = True
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        MultLocation.Enabled = True
        MultArea.Enabled = True
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
End Class