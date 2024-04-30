Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions

Public Class rptAvgSaleDetailReport
    Inherits FrmMainTranScreen

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim StrPermission As String
    Dim FromDate As String
    Dim ToDate As String

    Private Sub rptAvgSaleDetailReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
        LoadMonth()

    End Sub
    Sub LoadMonth()

        Dim dr As DataRow
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = ""
        dr("Name") = ""
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "January"
        dr("Name") = "January"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "February"
        dr("Name") = "February"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "March"
        dr("Name") = "March"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "April"
        dr("Name") = "April"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "May"
        dr("Name") = "May"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "June"
        dr("Name") = "June"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "July"
        dr("Name") = "July"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "August"
        dr("Name") = "August"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "September"
        dr("Name") = "September"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "October"
        dr("Name") = "October"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "November"
        dr("Name") = "November"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "December"
        dr("Name") = "December"
        dt1.Rows.Add(dr)

        ddMonth.DataSource = dt1
        ddMonth.ValueMember = "Code"
        ddMonth.DisplayMember = "Name"
        ddMonth.SelectedIndex = 0
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        txtYear.Value = ""
        txtRoute.Value = ""
        ddMonth.SelectedIndex = 0
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    Private Sub LoadData()
        Try
            Dim strCurrentYear As String = ""
            Dim pattern As String = "\d+-\d+"
            Dim regex As New Regex(pattern)
            Dim match As Match
            If clsCommon.myLen(txtYear.Value) > 0 Then

                match = regex.Match(txtYear.Value)
                strCurrentYear = match.Value.Substring(0, 2)
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select financial year ")
                Exit Sub
            End If


            FromDate = "01/Apr/20" & strCurrentYear
            Dim strEndYear As Integer = Nothing
            If ddMonth.SelectedIndex > 3 Then
                strEndYear = strCurrentYear
            Else
                strEndYear = strCurrentYear + 1
            End If

            Dim monthName As String = ""
            If ddMonth.SelectedIndex > 0 Then
                monthName = ddMonth.SelectedItem.Text.Substring(0, 3)
            Else
                clsCommon.MyMessageBoxShow(Me, "Please select month ", Me.Text)
                Exit Sub
            End If
            ToDate = clsDBFuncationality.getSingleValue("SELECT RIGHT(CONVERT(VARCHAR(2), EOMONTH('2023-" & monthName & "-01'), 103), 2) + '/' + '" & monthName & "' + '/' + '20' + '" & strEndYear & "' AS LastDate ")

            Dim whrCls1 As String = "  WHERE TSPL_ITEM_MASTER.Is_FreshItem = 1 and TSPL_ITEM_MASTER.IsTaxable = 0 and convert(date , TSPL_DEMAND_BOOKING_MASTER.Document_Date , 103) >= '" & FromDate & "' and convert(date , TSPL_DEMAND_BOOKING_MASTER.Document_Date , 103) <= '" & ToDate & "' AND TSPL_DEMAND_BOOKING_MASTER.Posted = 1"
            Dim whrCls2 As String = "Where  Posted = 'Y' AND TSPL_RECEIPT_HEADER.SecurityDeposit='Y' and TSPL_RECEIPT_HEADER.SecurityDepositType = 'S' AND Receipt_Type in('F' , 'P')  and convert(date , TSPL_RECEIPT_HEADER.Receipt_Date , 103) <= '" & ToDate & "'"
            If clsCommon.myLen(txtRoute.Value) > 0 Then
                whrCls1 += "And TSPL_DEMAND_BOOKING_MASTER.Route_No = '" & txtRoute.Value & "'"
                whrCls2 += "and TSPL_CUSTOMeR_MASTer.Route_No  = '" & txtRoute.Value & " '"
            End If

            Dim qry As String = ""
            qry = "select SNO ,Route_No,Cust_Code,Customer_Name,Contact_Person_Name,Total_Ltr_Qty,AVG as Avg_Sale, (case when Avg > 36 then convert(decimal(18,2),avg) else 36.0 end ) as Cons_Avg_Sale,Amt,Two_Days_Amt,One_Day_Emp_Crate,Total_Rs from (
         select  SNO,  Route_No,Cust_Code , Customer_Name , Contact_Person_Name , Total_Ltr_Qty,convert(decimal(18,2),Total_Ltr_Qty/Days)Avg ,  convert(decimal(18,2),Amt)Amt, convert(decimal(18,2),Two_Days_Amt)Two_Days_Amt , convert(decimal(18,2),One_Day_Emp_Crate)One_Day_Emp_Crate,convert(decimal(18,2),Total_Rs)Total_Rs  from (select max(Days)Days, 1 as SNO,  Route_No,Cust_Code,MAX(Customer_Name)Customer_Name,MAX(Contact_Person_Name)Contact_Person_Name,SUM(Qty)_Qty,sum(Total_Ltr_Qty)Total_Ltr_Qty, sum(credit)Amt , sum(Two_Days_Amt)Two_Days_Amt , sum(One_Day_Emp_Crate) as One_Day_Emp_Crate,sum(Two_Days_Amt + One_Day_Emp_Crate) as Total_Rs from ( " & Environment.NewLine & "
            SELECT Days,Route_No,Cust_Code,Customer_Name,Contact_Person_Name,Qty,Total_Ltr_Qty, credit , (credit*2*42) as Two_Days_Amt , (Credit/12)*256 as One_Day_Emp_Crate FROM   ( " & Environment.NewLine & " select Days,Route_No,Cust_Code , Customer_Name , Contact_Person_Name,Qty,Total_Ltr_Qty ,Credit   from ( " & Environment.NewLine & " select Days, Route_No,Cust_Code,Customer_Name,Contact_Person_Name,Qty,Total_Ltr_Qty,  Credit  from (select  TSPL_DEMAND_BOOKING_MASTER.Route_No , TSPL_DEMAND_BOOKING_DETAIL.Cust_Code ,
            max(TSPL_CUSTOMER_MASTER.Customer_Name)Customer_Name,max(TSPL_CUSTOMER_MASTER.Contact_Person_Name)Contact_Person_Name, sum(TSPL_DEMAND_BOOKING_DETAIL.Qty)Qty , sum(TotalLtr_ItemWise)Total_Ltr_Qty, DATEDIFF(day,'" & FromDate & "','" & ToDate & "' ) AS Days,0 as Credit  from TSPL_DEMAND_BOOKING_DETAIL 
            left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No
            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
            " & whrCls1 & "  group by TSPL_DEMAND_BOOKING_MASTER.Route_No,  TSPL_DEMAND_BOOKING_DETAIL.Document_No,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code ) xx )xxx
            " & Environment.NewLine & " union all " & Environment.NewLine & " select  DATEDIFF(day,'01/Apr/2023','31/Jan/2024' ) AS Days, TSPL_CUSTOMER_MASTER.Route_No, TSPL_RECEIPT_HEADER.Cust_Code,MAX(TSPL_CUSTOMeR_MASTer.Customer_Name) Customer_Name, max(TSPL_CUSTOMER_MASTER.Contact_Person_Name)Contact_Person_Name, 0 as Qty, 0 as Total_Ltr_Qty , sum(case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then (-1*Receipt_Amount) when  TSPL_RECEIPT_HEADER.Receipt_Type='P' then (1* Receipt_Amount) Else 0 end) as Credit from  TSPL_RECEIPT_HEADER  left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_RECEIPT_HEADER.Cust_Code 
           " & whrCls2 & "  group by TSPL_CUSTOMER_MASTER.Route_No, TSPL_RECEIPT_HEADER.Cust_Code )XXXX )xxxxx GROUP BY xxxxx.Route_No ,xxxxx.Cust_Code 
           " & Environment.NewLine & " ---------subtotal " & Environment.NewLine & " union all " & Environment.NewLine & "  select max(Days)Days, 2 as SNO, Route_No,'' as Cust_Code,MAX(Customer_Name)Customer_Name,MAX(Contact_Person_Name)Contact_Person_Name,SUM(Qty)_Qty,sum(Total_Ltr_Qty)Total_Ltr_Qty, sum(credit)Amt , sum(Two_Days_Amt)Two_Days_Amt , sum(One_Day_Emp_Crate) as One_Day_Emp_Crate,sum(Two_Days_Amt + One_Day_Emp_Crate) as Total_Rs from  ( " & Environment.NewLine & " 
           SELECT Days, Route_No,Cust_Code,Customer_Name,Contact_Person_Name,Qty,Total_Ltr_Qty,  credit , (credit*2*42) as Two_Days_Amt , (Credit/12)*256 as One_Day_Emp_Crate FROM  ( " & Environment.NewLine & " select Days, Route_No,Cust_Code ,'Sub Total:'  as  Customer_Name , '' as Contact_Person_Name,Qty,Total_Ltr_Qty ,Credit   from ( " & Environment.NewLine & " 
           Select Days, Route_No,Cust_Code,Customer_Name,Contact_Person_Name,Qty,Total_Ltr_Qty, Credit  from (select   TSPL_DEMAND_BOOKING_MASTER.Route_No , TSPL_DEMAND_BOOKING_DETAIL.Cust_Code ,max(TSPL_CUSTOMER_MASTER.Customer_Name)Customer_Name,max(TSPL_CUSTOMER_MASTER.Contact_Person_Name)Contact_Person_Name, sum(TSPL_DEMAND_BOOKING_DETAIL.Qty)Qty , sum(TotalLtr_ItemWise)Total_Ltr_Qty ,DATEDIFF(day,'01/Apr/2023','30/Apr/2023' ) AS Days,0 as Credit
           from TSPL_DEMAND_BOOKING_DETAIL  left outer join TSPL_DEMAND_BOOKING_MASTER on TSPL_DEMAND_BOOKING_MASTER.Document_No = TSPL_DEMAND_BOOKING_DETAIL.Document_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = TSPL_DEMAND_BOOKING_DETAIL.Item_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_DEMAND_BOOKING_DETAIL.Cust_Code 
           " & whrCls1 & " group by TSPL_DEMAND_BOOKING_MASTER.Route_No,  TSPL_DEMAND_BOOKING_DETAIL.Document_No,TSPL_DEMAND_BOOKING_DETAIL.Cust_Code )xx )xxx " & Environment.NewLine & "  union all " & Environment.NewLine & "   
          select DATEDIFF(day,'01/Apr/2023','31/Jan/2024' ) AS Days, TSPL_CUSTOMER_MASTER.Route_No, TSPL_RECEIPT_HEADER.Cust_Code,'Sub Total:'  as  Customer_Name, '' AS Contact_Person_Name, 0 as Qty, 0 as Total_Ltr_Qty , sum(case when TSPL_RECEIPT_HEADER.Receipt_Type='F' then (-1*Receipt_Amount) when  TSPL_RECEIPT_HEADER.Receipt_Type='P' then (1* Receipt_Amount) Else 0 end) as Credit from  TSPL_RECEIPT_HEADER   left join TSPL_CUSTOMeR_MASTer on TSPL_CUSTOMeR_MASTer.Cust_Code =TSPL_RECEIPT_HEADER.Cust_Code 
           " & whrCls2 & " group by TSPL_CUSTOMER_MASTER.Route_No, TSPL_RECEIPT_HEADER.Cust_Code  )XXXX ) xxxxx GROUP BY xxxxx.Route_No )Final )xxfinal"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.EnableFiltering = True
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                EnableDisableControl(False)
                SetGridFormation()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        gv1.EnableFiltering = True
        gv1.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        gv1.Columns("SNO").IsVisible = False
        gv1.Columns("Route_No").HeaderText = "Route No"
        gv1.Columns("Cust_Code").HeaderText = "Customer"
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"
        gv1.Columns("Contact_Person_Name").HeaderText = "Customer Contact Name"
        gv1.Columns("Total_Ltr_Qty").HeaderText = "Total  Milk  Qty (In L)"
        gv1.Columns("Avg_Sale").HeaderText = "Avg Sale (From 1 april to til month days total)"
        gv1.Columns("Cons_Avg_Sale").HeaderText = "Considered Avg Sale for  calculation of required security"
        gv1.Columns("Amt").HeaderText = "Amt (Rs)"
        gv1.Columns("Two_Days_Amt").HeaderText = "Two Days Sale Amt @ Rs 42.00/-"
        gv1.Columns("One_Day_Emp_Crate").HeaderText = "One Day Empty Crate Cost Rs 256.00/crate"
        gv1.Columns("Total_Rs").HeaderText = "Total (Rs)"

        'gv1.Columns("Doc_Date").IsVisible = False

        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim Qty As New GridViewSummaryItem("Qty", "", GridAggregateFunction.Sum)
        'summaryRowItem.Add(Qty)
        'Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(Amount)

        ' gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptAvgSaleDetailReport & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(FromDate, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(ToDate, "dd/MM/yyyy"))
                If clsCommon.myLen(txtRoute.Value) > 0 Then
                    arrHeader.Add("Route : " & txtRoute.Value)
                End If

                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtYear__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtYear._MYValidating
        Dim qry As String = " select Fiscal_Code as [Code],convert (varchar, Start_Date,103) as [Start_Date] ,convert (varchar, End_Date,103) as [End Date]  from TSPL_Fiscal_Year_Master "
        txtYear.Value = clsCommon.ShowSelectForm("FinancialYear@Finder", qry, "Code", "", txtYear.Value, "", isButtonClicked)
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim style As New GridPrintStyle()
                style.PrintGrouping = True
                style.HeaderCellBackColor = Color.White
                style.GroupRowBackColor = Color.White
                style.SummaryCellBackColor = Color.White
                style.PrintSummaries = True
                gv1.PrintStyle = style

                Dim doc As New clsMyPrintDocument()

                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 90
                doc.Landscape = True
                doc.AssociatedObject = gv1

                doc.DocumentName = objCommonVar.CurrentCompanyName
                doc.MiddleHeader = clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptAvgSaleDetailReport & "'")
                doc.LeftHeader = "Date Range: " + clsCommon.GetPrintDate(FromDate, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate, "dd/MM/yyyy") + Environment.NewLine & "Company : " & objCommonVar.CurrentCompanyName + Environment.NewLine + "Route : " + txtRoute.Value
                doc.RightHeader = "Print Date(" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm : ss tt") + ")"
                doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)

                doc.AssociatedObject = gv1

                doc.RightFooter = "Page [Page #] Of [Total Pages]"

                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.WindowState = FormWindowState.Maximized
                dialog.SetZoom(1)
                dialog.ShowDialog(Me)

                doc.Print()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtRoute__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRoute._MYValidating
        Dim qry As String = "Select Route_No as Code,Route_Desc as Description from TSPL_ROUTE_MASTER"
        txtRoute.Value = clsCommon.ShowSelectForm("EXRUTFND", qry, "Code", "", txtRoute.Value, "", isButtonClicked)
    End Sub


End Class