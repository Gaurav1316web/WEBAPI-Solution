'' Update BY abhishek as on 4 Nov 7:03 Pm
'update by vipin for correction of report for discount type on 07/12/2012
'''' for bug no BM00000000560,BM00000000561
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports common

Public Class FrmTargetReport1
    Inherits FrmMainTranScreen
    Private Sub FrmTargetReport1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        rdbSummary.IsChecked = True
        reset()
        SetUserMgmtNew()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmTargetReport1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Sub reset()
        LoadCustomer()
        LoadDiscountType()
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtpTodate.Value = clsCommon.GETSERVERDATE()
        chkallcustomer.IsChecked = True
        discountall.IsChecked = True
        rdbSummary.IsChecked = True
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where Status='N'"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Sub LoadDiscountType()
        Dim strquery As String = "select Code, Description from TSPL_Discount_Master"
        dgvdiscount.DataSource = clsDBFuncationality.GetDataTable(strquery)
        dgvdiscount.ValueMember = "Code"
        dgvdiscount.DisplayMember = "Name"
    End Sub
    Private Sub chkallcustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkallcustomer.ToggleStateChanged
        cbgCustomer.Enabled = Not chkallcustomer.IsChecked
    End Sub

    Private Sub discountall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles discountall.ToggleStateChanged
        dgvdiscount.Enabled = Not discountall.IsChecked
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()
    End Sub
    Sub print()
        Try
            Dim Qry As String

            If chkselectcustomer.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
                Exit Sub

            ElseIf discountselect.IsChecked = True AndAlso dgvdiscount.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Discount Type")
                Exit Sub

            End If

            If rdbSummary.IsChecked OrElse rdbPendingSummary.IsChecked Then
                Qry = "select DiscType as DiscType, '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as RunDate, '" + clsCommon.myCstr(dtpFromdate.Value.Date.Year) + "' as Year, (Cust_Code),(Customer_Name) as CustName, SUM(Month1) as Month1, SUM(Month2) as Month2, " & _
            " SUM(Month3) as Month3, SUM(Month4) as Month4, SUM(Month5) as Month5, SUM(Month6) as Month6, SUM(Month7) as Month7, SUM(Month8) as Month8, " & _
            " SUM(Month9) as Month9, SUM(Month10) as Month10, SUM(Month11) as Month11 , SUM(Month12) as Month12, SUM(Amount) as [TotalAmt], SUM(Bal_Amount) as [TotalBalAmt], (Sum(Amount)-Sum(Bal_Amount)) as [PaiDBal] " & _
            " from ( " & _
            " select TSPL_Discount_Master.Description as DiscType, TSPL_TARGET_MASTER.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name " & _
            " ,case when  DatePart(Month, Month_Year) =1 then Amount else 0 end Month1" & _
            " ,case when  DatePart(Month, Month_Year) =2 then Amount else 0 end Month2" & _
            " ,case when  DatePart(Month, Month_Year) =3 then Amount else 0 end Month3" & _
            " ,case when  DatePart(Month, Month_Year) =4 then Amount else 0 end Month4" & _
            " ,case when  DatePart(Month, Month_Year) =5 then Amount else 0 end Month5 " & _
            " ,case when  DatePart(Month, Month_Year) =6 then Amount else 0 end Month6" & _
            " ,case when  DatePart(Month, Month_Year) =7 then Amount else 0 end Month7" & _
            " ,case when  DatePart(Month, Month_Year) =8 then Amount else 0 end Month8" & _
            " ,case when  DatePart(Month, Month_Year) =9 then Amount else 0 end Month9" & _
            " ,case when  DatePart(Month, Month_Year) =10 then Amount else 0 end Month10" & _
            " ,case when  DatePart(Month, Month_Year) =11 then Amount else 0 end Month11" & _
            " ,case when  DatePart(Month, Month_Year) =12 then Amount else 0 end Month12" & _
            " , Amount, Bal_Amount" & _
                " from TSPL_TARGET_MASTER " & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= TSPL_TARGET_MASTER.Cust_Code Left Outer Join TSPL_Discount_Master on TSPL_Discount_Master.Code=TSPL_TARGET_MASTER.Discount_Type " & _
            " where TSPL_TARGET_MASTER.Month_Year >= '" + clsCommon.GetPrintDate(dtpFromdate.Value.Date) + "' and TSPL_TARGET_MASTER.Month_Year <= '" + clsCommon.GetPrintDate(dtpTodate.Value.Date) + "'   "

                If rdbPendingSummary.IsChecked Then
                    Qry += " and Bal_Amount <> 0 "
                End If
                If discountselect.IsChecked = True Then
                    Qry += " and Discount_Type in  (" + clsCommon.GetMulcallString(dgvdiscount.CheckedValue) + ")"
                End If

                If chkselectcustomer.IsChecked = True Then
                    Qry += " and TSPL_TARGET_MASTER.Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                End If

                Qry += " )xxx  group by DiscType,Cust_Code,Customer_Name "
            Else
                Qry = "select Code ,TSPL_Discount_Master.Description AS DiscType,TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_no,Sale_Invoice_Date, TSPL_SALE_INVOICE_HEAD.Cust_Code, " & _
                "Cust_Type_Code,Cust_Name as CustName,case when Scheme_Item='Y' and Discount_Code <> ''  THEN  " & _
                "convert(decimal(18,2),((Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)))) ELSE 0 END AS Amount " & _
                "from TSPL_SALE_INVOICE_HEAD left outer join TSPL_SALE_INVOICE_DETAIL on  " & _
                "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No=TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No left outer join  " & _
                "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_INVOICE_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
                "TSPL_SALE_INVOICE_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
                "TSPL_Discount_Master on TSPL_SALE_INVOICE_DETAIL.Discount_Code=TSPL_Discount_Master.Code left outer join " & _
                "TSPL_CUSTOMER_MASTER on TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  where " & _
                "Scheme_Item='Y' and Discount_Code <> '' and Is_Post='Y' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date >= CONVERT(date,'" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + "', 103)   " & _
                "  AND TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date <= CONVERT(date, '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy") + " ', 103) "

                If discountselect.IsChecked = True Then
                    Qry += " and Code in  (" + clsCommon.GetMulcallString(dgvdiscount.CheckedValue) + ")"
                End If

                If chkselectcustomer.IsChecked = True Then
                    Qry += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                End If

                Qry += " Union all "
                Qry += "select Code ,TSPL_Discount_Master.Description AS DiscType,TSPL_SALE_RETURN_DETAIL.Sale_Return_No as Sale_Invoice_no, " & _
                "Sale_Return_Date as Sale_Invoice_Date, TSPL_SALE_RETURN_HEAD.Cust_Code, Cust_Type_Code,Cust_Name as CustName, " & _
                " - case when Scheme_Item='Y' and Discount_Code <> ''  THEN  convert(decimal(18,2),((Return_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) * ((MRP_Amt * TSPL_ITEM_UOM_DETAIL.Conversion_Factor) - (Price_Amount1 + Price_Amount2 + Price_Amount3 + Price_Amount4 + Price_Amount5 + Price_Amount6 + Price_Amount7 + Price_Amount8 + Price_Amount9)))) ELSE 0 END AS Amount " & _
                "from TSPL_SALE_RETURN_HEAD left outer join TSPL_SALE_RETURN_DETAIL on " & _
                "TSPL_SALE_RETURN_HEAD.Sale_Return_No=TSPL_SALE_RETURN_DETAIL.Sale_Return_No left outer join " & _
                "TSPL_ITEM_UOM_DETAIL on TSPL_SALE_RETURN_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and " & _
                "TSPL_SALE_RETURN_DETAIL.Unit_code=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
                "TSPL_Discount_Master on TSPL_SALE_RETURN_DETAIL.Discount_Code=TSPL_Discount_Master.Code left outer join " & _
                "TSPL_CUSTOMER_MASTER on TSPL_SALE_RETURN_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code  " & _
                "where Scheme_Item='Y' and Discount_Code <> '' and Is_Post='Y' and  " & _
                " CONVERT(Date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) >= CONVERT(date,'" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + "', 103)   " & _
                "  AND CONVERT(Date,TSPL_SALE_RETURN_HEAD.Sale_Return_Date,103) <= CONVERT(date, '" + clsCommon.GetPrintDate(dtpTodate.Value, "dd/MM/yyyy") + " ', 103) "

                If discountselect.IsChecked = True Then
                    Qry += " and Code in  (" + clsCommon.GetMulcallString(dgvdiscount.CheckedValue) + ")"
                End If

                If chkselectcustomer.IsChecked = True Then
                    Qry += " and TSPL_SALE_RETURN_HEAD.Cust_Code in  (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
                End If


                Qry = "select Sale_Invoice_No,Sale_Invoice_Date,DiscType, " & _
                "(Cust_Code),CustName, SUM(Month1) as Month1, SUM(Month2) as Month2, " & _
                "SUM(Month3) as Month3, SUM(Month4) as Month4, SUM(Month5) as Month5, SUM(Month6) as Month6, SUM(Month7) as Month7, " & _
                "SUM(Month8) as Month8,  SUM(Month9) as Month9, SUM(Month10) as Month10, SUM(Month11) as Month11 , SUM(Month12) as Month12, " & _
                "SUM(Amount) as [TotalAmt] from ( " & _
                "select DiscType,Sale_Invoice_no,Sale_Invoice_Date,(Cust_Code), CustName,Cust_Type_Code, " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =1 then Amount else 0 end Month1 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =2 then Amount else 0 end Month2 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =3 then Amount else 0 end Month3 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =4 then Amount else 0 end Month4 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =5 then Amount else 0 end Month5  , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =6 then Amount else 0 end Month6 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =7 then Amount else 0 end Month7 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =8 then Amount else 0 end Month8 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =9 then Amount else 0 end Month9 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =10 then Amount else 0 end Month10 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =11 then Amount else 0 end Month11 , " & _
                "case when  DatePart(Month, Sale_Invoice_Date) =12 then Amount else 0 end Month12 , Amount from ( " + Qry + " ) aa  ) xxx group by Cust_Code,CustName,Sale_Invoice_No,Sale_Invoice_Date,DiscType,DiscType ORDER BY Sale_Invoice_Date"
            End If



            '---------------------------------------------------------------------------------

            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(Qry)
            GV1.DataSource = Nothing
            GV1.GroupDescriptors.Clear()
            GV1.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                RadMessageBox.Show("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                GV1.DataSource = dt
                SetGridFormationOFGV1()
            End If

            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            'If dt.Rows.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("No Data Found")
            'Else
            '    SalesReportViewer.funreport(dt, "crptTargetReport", "Target Report")
            'End If




        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        ' Dim strItemCode, head2 As String

        GV1.TableElement.TableHeaderHeight = 40
        GV1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To GV1.Columns.Count - 1
            GV1.Columns(ii).ReadOnly = True
            GV1.Columns(ii).IsVisible = False
        Next

        Dim TotMonth1 As Decimal = 0
        Dim TotMonth2 As Decimal = 0
        Dim TotMonth3 As Decimal = 0
        Dim TotMonth4 As Decimal = 0
        Dim TotMonth5 As Decimal = 0
        Dim TotMonth6 As Decimal = 0
        Dim TotMonth7 As Decimal = 0
        Dim TotMonth8 As Decimal = 0
        Dim TotMonth9 As Decimal = 0
        Dim TotMonth10 As Decimal = 0
        Dim TotMonth11 As Decimal = 0
        Dim TotMonth12 As Decimal = 0

        For Each grow As GridViewRowInfo In GV1.Rows
            TotMonth1 = TotMonth1 + clsCommon.myCdbl(grow.Cells("Month1").Value)
            TotMonth2 = TotMonth2 + clsCommon.myCdbl(grow.Cells("Month2").Value)
            TotMonth3 = TotMonth3 + clsCommon.myCdbl(grow.Cells("Month3").Value)
            TotMonth4 = TotMonth4 + clsCommon.myCdbl(grow.Cells("Month4").Value)
            TotMonth5 = TotMonth5 + clsCommon.myCdbl(grow.Cells("Month5").Value)
            TotMonth6 = TotMonth6 + clsCommon.myCdbl(grow.Cells("Month6").Value)
            TotMonth7 = TotMonth7 + clsCommon.myCdbl(grow.Cells("Month7").Value)
            TotMonth8 = TotMonth8 + clsCommon.myCdbl(grow.Cells("Month8").Value)
            TotMonth9 = TotMonth9 + clsCommon.myCdbl(grow.Cells("Month9").Value)
            TotMonth10 = TotMonth10 + clsCommon.myCdbl(grow.Cells("Month10").Value)
            TotMonth11 = TotMonth11 + clsCommon.myCdbl(grow.Cells("Month11").Value)
            TotMonth12 = TotMonth12 + clsCommon.myCdbl(grow.Cells("Month12").Value)

        Next

        If rdbSummary.IsChecked = True OrElse rdbPendingSummary.IsChecked = True Then
            GV1.Columns("DiscType").IsVisible = True
            GV1.Columns("DiscType").Width = 50
            GV1.Columns("DiscType").HeaderText = "DiscType"

            GV1.Columns("Cust_Code").IsVisible = True
            GV1.Columns("Cust_Code").Width = 70
            GV1.Columns("Cust_Code").HeaderText = "Cust Code"

            GV1.Columns("CustName").IsVisible = True
            GV1.Columns("CustName").Width = 100
            GV1.Columns("CustName").HeaderText = "Cust Name"
            ''GV1.Columns("Customer Group").BestFit()

           
            If TotMonth1 > 0 Then              
                GV1.Columns("Month1").IsVisible = True
                GV1.Columns("Month1").Width = 80
                GV1.Columns("Month1").HeaderText = "Jan"
            End If
           
            If TotMonth2 > 0 Then
                GV1.Columns("Month2").IsVisible = True
                GV1.Columns("Month2").Width = 80
                GV1.Columns("Month2").HeaderText = "Feb"
            End If

            If TotMonth3 > 0 Then
                GV1.Columns("Month3").IsVisible = True
                GV1.Columns("Month3").Width = 80
                GV1.Columns("Month3").HeaderText = "March"
            End If

            If TotMonth4 > 0 Then
                GV1.Columns("Month4").IsVisible = True
                GV1.Columns("Month4").Width = 80
                GV1.Columns("Month4").HeaderText = "April"
            End If

            If TotMonth5 > 0 Then
                GV1.Columns("Month5").IsVisible = True
                GV1.Columns("Month5").Width = 80
                GV1.Columns("Month5").HeaderText = "May"
            End If

            If TotMonth6 > 0 Then
                GV1.Columns("Month6").IsVisible = True
                GV1.Columns("Month6").Width = 80
                GV1.Columns("Month6").HeaderText = "June"
            End If

            If TotMonth7 > 0 Then
                GV1.Columns("Month7").IsVisible = True
                GV1.Columns("Month7").Width = 80
                GV1.Columns("Month7").HeaderText = "July"
            End If

            If TotMonth8 > 0 Then
                GV1.Columns("Month8").IsVisible = True
                GV1.Columns("Month8").Width = 80
                GV1.Columns("Month8").HeaderText = "Aug"
            End If

            If TotMonth9 > 0 Then
                GV1.Columns("Month9").IsVisible = True
                GV1.Columns("Month9").Width = 80
                GV1.Columns("Month9").HeaderText = "Sep"
            End If

            If TotMonth10 > 0 Then
                GV1.Columns("Month10").IsVisible = True
                GV1.Columns("Month10").Width = 80
                GV1.Columns("Month10").HeaderText = "Oct"
            End If

            If TotMonth11 > 0 Then
                GV1.Columns("Month11").IsVisible = True
                GV1.Columns("Month11").Width = 80
                GV1.Columns("Month11").HeaderText = "Nov"
            End If

            If TotMonth12 > 0 Then
                GV1.Columns("Month12").IsVisible = True
                GV1.Columns("Month12").Width = 80
                GV1.Columns("Month12").HeaderText = "Dec"
            End If

            GV1.Columns("TotalAmt").IsVisible = True
            GV1.Columns("TotalAmt").Width = 80
            GV1.Columns("TotalAmt").HeaderText = "TotalAmt"

            GV1.Columns("TotalBalAmt").IsVisible = True
            GV1.Columns("TotalBalAmt").Width = 80
            GV1.Columns("TotalBalAmt").HeaderText = "TotalBalAmt"

            GV1.Columns("PaiDBal").IsVisible = True
            GV1.Columns("PaiDBal").Width = 80
            GV1.Columns("PaiDBal").HeaderText = "PaiDBal"
        Else
            GV1.Columns("Sale_Invoice_No").IsVisible = True
            GV1.Columns("Sale_Invoice_No").Width = 50
            GV1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            GV1.Columns("Sale_Invoice_Date").IsVisible = True
            GV1.Columns("Sale_Invoice_Date").Width = 50
            GV1.Columns("Sale_Invoice_Date").FormatString = "{0:d}"
            GV1.Columns("Sale_Invoice_Date").HeaderText = "SaleInvoice Date"

            GV1.Columns("DiscType").IsVisible = True
            GV1.Columns("DiscType").Width = 50
            GV1.Columns("DiscType").HeaderText = "DiscType"

            GV1.Columns("Cust_Code").IsVisible = True
            GV1.Columns("Cust_Code").Width = 70
            GV1.Columns("Cust_Code").HeaderText = "Cust Code"
            'GV1.Columns("Location").BestFit()

            GV1.Columns("CustName").IsVisible = True
            GV1.Columns("CustName").Width = 100
            GV1.Columns("CustName").HeaderText = "Cust Name"
            ''GV1.Columns("Customer Group").BestFit()

            If TotMonth1 > 0 Then
                GV1.Columns("Month1").IsVisible = True
                GV1.Columns("Month1").Width = 80
                GV1.Columns("Month1").HeaderText = "Jan"
            End If

            If TotMonth2 > 0 Then
                GV1.Columns("Month2").IsVisible = True
                GV1.Columns("Month2").Width = 80
                GV1.Columns("Month2").HeaderText = "Feb"
            End If

            If TotMonth3 > 0 Then
                GV1.Columns("Month3").IsVisible = True
                GV1.Columns("Month3").Width = 80
                GV1.Columns("Month3").HeaderText = "March"
            End If

            If TotMonth4 > 0 Then
                GV1.Columns("Month4").IsVisible = True
                GV1.Columns("Month4").Width = 80
                GV1.Columns("Month4").HeaderText = "April"
            End If

            If TotMonth5 > 0 Then
                GV1.Columns("Month5").IsVisible = True
                GV1.Columns("Month5").Width = 80
                GV1.Columns("Month5").HeaderText = "May"
            End If

            If TotMonth6 > 0 Then
                GV1.Columns("Month6").IsVisible = True
                GV1.Columns("Month6").Width = 80
                GV1.Columns("Month6").HeaderText = "June"
            End If

            If TotMonth7 > 0 Then
                GV1.Columns("Month7").IsVisible = True
                GV1.Columns("Month7").Width = 80
                GV1.Columns("Month7").HeaderText = "July"
            End If

            If TotMonth8 > 0 Then
                GV1.Columns("Month8").IsVisible = True
                GV1.Columns("Month8").Width = 80
                GV1.Columns("Month8").HeaderText = "Aug"
            End If

            If TotMonth9 > 0 Then
                GV1.Columns("Month9").IsVisible = True
                GV1.Columns("Month9").Width = 80
                GV1.Columns("Month9").HeaderText = "Sep"
            End If

            If TotMonth10 > 0 Then
                GV1.Columns("Month10").IsVisible = True
                GV1.Columns("Month10").Width = 80
                GV1.Columns("Month10").HeaderText = "Oct"
            End If

            If TotMonth11 > 0 Then
                GV1.Columns("Month11").IsVisible = True
                GV1.Columns("Month11").Width = 80
                GV1.Columns("Month11").HeaderText = "Nov"
            End If

            If TotMonth12 > 0 Then
                GV1.Columns("Month12").IsVisible = True
                GV1.Columns("Month12").Width = 80
                GV1.Columns("Month12").HeaderText = "Dec"
            End If

            GV1.Columns("TotalAmt").IsVisible = True
            GV1.Columns("TotalAmt").Width = 80
            GV1.Columns("TotalAmt").HeaderText = "TotalAmt"

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0


        Dim Month1 As New GridViewSummaryItem("Month1", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month1)
        Dim Month2 As New GridViewSummaryItem("Month2", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month2)
        Dim Month3 As New GridViewSummaryItem("Month3", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month3)
        Dim Month4 As New GridViewSummaryItem("Month4", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month4)
        Dim Month5 As New GridViewSummaryItem("Month5", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month5)
        Dim Month6 As New GridViewSummaryItem("Month6", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month6)
        Dim Month7 As New GridViewSummaryItem("Month7", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month7)
        Dim Month8 As New GridViewSummaryItem("Month8", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month8)
        Dim Month9 As New GridViewSummaryItem("Month9", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month9)
        Dim Month10 As New GridViewSummaryItem("Month10", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month10)
        Dim Month11 As New GridViewSummaryItem("Month11", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month11)
        Dim Month12 As New GridViewSummaryItem("Month12", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Month12)
        Dim TotalAmt As New GridViewSummaryItem("TotalAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(TotalAmt)


        If rdbSummary.IsChecked = True OrElse rdbPendingSummary.IsChecked = True Then
            Dim TotalBalAmt As New GridViewSummaryItem("TotalBalAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(TotalBalAmt)
            Dim PaiDBal As New GridViewSummaryItem("PaiDBal", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(PaiDBal)
        End If


        GV1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        print()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If GV1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        If GV1.Rows.Count > 0 Then
            ExporttoExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub
    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = "Year : " + clsCommon.myCstr(dtpFromdate.Value.Date.Year) + ""
            arrHeader.Add(strtemp)

            If discountselect.IsChecked Then
                strtemp = ""
                For Each Str As String In dgvdiscount.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Customer : " + strtemp)
            End If
            If chkselectcustomer.IsChecked Then
                strtemp = ""
                For Each Str As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strtemp) > 0 Then
                        strtemp += ", "
                    End If
                    strtemp += Str
                Next
                arrHeader.Add("Discount Type : " + strtemp)
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Target " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Target " + IIf(rdbDetail.IsChecked, " ( Detail ) ", " ( Summary ) "), GV1, arrHeader, "Target report ", True)
            End If


        Catch ex As Exception
            RadMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rdbSummary_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbSummary.ToggleStateChanged

    End Sub
    Dim total As Integer
    Dim grandTotal As Integer
    'Private Sub Gv1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Data.gr) Handles GV1.da
    '    If (TypeOf e.Item Is GridDataItem) Then
    '        Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '        Dim fieldValue As Integer = Integer.Parse(dataItem("Quantity").Text)
    '        total = (total + fieldValue)
    '    End If
    '    If (TypeOf e.Item Is GridFooterItem) Then
    '        Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
    '        footerItem("Quantity").Text = "Total: " + total.ToString()
    '    End If
    'End Sub

    
   
End Class
