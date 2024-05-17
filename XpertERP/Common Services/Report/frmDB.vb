Imports common
Imports System.Data.SqlClient
Imports Telerik.Charting
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.IO
Public Class frmDB
    Inherits XpertERPEngine.FrmMainTranScreen
    Private Sub FrmDasboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CheckBox1.Checked = True
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)
            LoadReportType()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadReportType()
        Dim dr As DataRow
        Dim dt1 As DataTable = New DataTable()
        dt1.Columns.Add("Code", GetType(String))
        dt1.Columns.Add("Name", GetType(String))

        dr = dt1.NewRow()
        dr("Code") = "All"
        dr("Name") = "All"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Bank Cash Book"
        dr("Name") = "Bank Cash Book"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sales Vehicle Utilization"
        dr("Name") = "Sales Vehicle Utilization"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Procurement Milk Purchase"
        dr("Name") = "Procurement Milk Purchase"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Milk Received At Factory"
        dr("Name") = "Milk Received At Factory"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Milk Sale"
        dr("Name") = "Milk Sale"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Product Sale"
        dr("Name") = "Product Sale"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "FG Mass Balance"
        dr("Name") = "FG Mass Balance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "SFG/Raw Milk Mass Balance"
        dr("Name") = "SFG/Raw Milk Mass Balance"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Sales Transport Costing"
        dr("Name") = "Sales Transport Costing"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Purchase"
        dr("Name") = "Purchase"
        dt1.Rows.Add(dr)

        dr = dt1.NewRow()
        dr("Code") = "Stock/Inventory"
        dr("Name") = "Stock/Inventory"
        dt1.Rows.Add(dr)

        cboReport.DataSource = dt1
        cboReport.ValueMember = "Code"
        cboReport.DisplayMember = "Name"
        cboReport.SelectedIndex = 0
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 11
        clsCommon.ProgressBarPercentShow()
        Try

            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Bank Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv3.DataSource = Nothing
            gv3.Rows.Clear()
            gv3.Columns.Clear()
            If chkBankCashBook.Checked Then
                Loadbankdata() ''Done
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Vehicle Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv4.DataSource = Nothing
            gv4.Rows.Clear()
            gv4.Columns.Clear()
            If chkVehicleUtilization.Checked Then
                LoadVehicle()
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Procuremnt Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv_Procurement.DataSource = Nothing
            gv_Procurement.Rows.Clear()
            gv_Procurement.Columns.Clear()
            If chkProcurementMilkPurchase.Checked Then
                LoadProcuremntData() ''Done
            End If


            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Received At Factory Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv_MilkReceived.DataSource = Nothing
            gv_MilkReceived.Rows.Clear()
            gv_MilkReceived.Columns.Clear()
            If chkMilkReceivedAtFactory.Checked Then
                MilkReceivedAtFactory() ''Done
            End If


            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Sale Data... " & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv_MilkSale.DataSource = Nothing
            gv_MilkSale.Rows.Clear()
            gv_MilkSale.Columns.Clear()
            If chkMilkSale.Checked Then
                LoadMilkSale() ''Done
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Product Sale Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv_ProductSale.DataSource = Nothing
            gv_ProductSale.Rows.Clear()
            gv_ProductSale.Columns.Clear()
            If chkProductSale.Checked Then
                LoadProductSale() ''Done
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Transport Charges Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gvTransportcost.DataSource = Nothing
            gvTransportcost.Rows.Clear()
            gvTransportcost.Columns.Clear()
            If chkTransportCosting.Checked Then
                LoadTransportCharges() ''not Done
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading PO Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv_po.DataSource = Nothing
            gv_po.Rows.Clear()
            gv_po.Columns.Clear()
            If chkPO.Checked Then
                LoadPO() ''not Done
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Store Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gv_store.DataSource = Nothing
            gv_store.Rows.Clear()
            gv_store.Columns.Clear()
            If chkStore.Checked Then
                LoadStore() ''Done
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading FG Mass Balance Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gvFGMassBalance.DataSource = Nothing
            gvFGMassBalance.Rows.Clear()
            gvFGMassBalance.Columns.Clear()
            If chkFGMassBalance.Checked Then
                LoadMassBalance(False, gvFGMassBalance) ''Done
            End If

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading SFG Mass Balance Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            gvSFGMassBalance.DataSource = Nothing
            gvSFGMassBalance.Rows.Clear()
            gvSFGMassBalance.Columns.Clear()
            If chkSFGMassBalance.Checked Then
                LoadMassBalance(True, gvSFGMassBalance) ''Done
            End If
            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Loadbankdata()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Bank_Cash_Book"
            Dim figure As String = 100000
            'If cboFigureInGraph_Bank_Cash_Book.Text = "Crores" Then
            '    figure = 1000000
            'ElseIf cboFigureInGraph_Bank_Cash_Book.Text = "Lacs" Then
            '    figure = 100000
            'ElseIf cboFigureInGraph_Bank_Cash_Book.Text = "Ten Thousands" Then
            '    figure = 10000
            'End If
            Dim strAddress As String = " (TSPL_COMPANY_MASTER.Add1 + case When isnull(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.Add2 End + Case When isnull(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add3 end + case When isnull(TSPL_COMPANY_MASTER.City_Code,'') ='' then '' else ', '+ TSPL_COMPANY_MASTER.City_Code end+ Case When isnull(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.State end +  Case When isnull(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Pincode  end) "
            Dim Qry As String = clsBankReco.GetQueryForTransactionOFBB(True, txtFromDate.Value, txtToDate.Value, "", "", strAddress, "Y", "", "Bank Book", "", False)
            Qry = "Select final.Bank_Code as [Bank Code],Final.Description as [Bank Name] , Final.BalAmt as [Opening Balance],Credit_Amount as [Payments],Debit_Amount as [Receipts],Convert(decimal(18,2),Closing_Balance) as [Closing], Convert (decimal(18,2),( Final.BalAmt / " + figure + " ))   as BalAmt_Chart , Convert (decimal(18,2),( Final.Credit_Amount / " + figure + " ))   as Credit_Amount_Chart, Convert (decimal(18,2), (Final.Debit_Amount / " + figure + ") )   as Debit_Amount_Chart, Convert (decimal(18,2), (Final.Closing_Balance / " + figure + ") )   as Closing_Balance_Chart  from ( SELECT BANK_CODE, MAX(DESCRIPTION) AS [DESCRIPTION], MAX(BankType) AS BankType,MAX(Startdate) AS Startdate,MAX(EndDate) AS EndDate,MAX(RunDate) AS RunDate,SUM(BalAmt) AS BalAmt,SUM(Debit_Amount) AS Debit_Amount ,SUM(Credit_Amount) AS Credit_Amount,(SUM(Debit_Amount)-SUM(Credit_Amount)+SUM(BalAmt)) AS Closing_Balance,max(POP.Add1) as Add1   FROM (" + Qry + ")POP where BankType<>'O' GROUP BY BANK_CODE )final Left Outer Join TSPL_COMPANY_MASTER ON '" & objCommonVar.CurrentCompanyCode & "'=TSPL_COMPANY_MASTER.Comp_Code ORDER BY  [Bank_Code] "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then

                gv3.DataSource = dt
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                gv3.Columns("BalAmt_Chart").IsVisible = False
                gv3.Columns("Credit_Amount_Chart").IsVisible = False
                gv3.Columns("Debit_Amount_Chart").IsVisible = False
                gv3.Columns("Closing_Balance_Chart").IsVisible = False
                gv3.Columns("Closing_Balance_Chart").Width = 200
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadVehicle()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Vehicle_Utilization"
            Dim Qry As String = clsDB.GetQueryVehicle(txtFromDate.Value, txtToDate.Value)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv4.DataSource = dt
                gv4.GroupDescriptors.Clear()
                gv4.MasterTemplate.SummaryRowsBottom.Clear()
                gv4.MasterTemplate.BestFitColumns()
                gv4.EnableFiltering = True
                For i As Integer = 0 To gv4.Columns.Count - 1
                    gv4.Columns(i).BestFit()
                Next

                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim NoofKMRunningPerMonth As New GridViewSummaryItem("No of KM Running Per Month", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(NoofKMRunningPerMonth)
                Dim FreightCost As New GridViewSummaryItem("Freight Cost", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(FreightCost)
                Dim SalesInLTR As New GridViewSummaryItem("Sales In LTR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR)

                gv4.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv4.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadProcuremntData()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Procurement"
            gv_Procurement.DataSource = Nothing
            Dim dt As DataTable = clsDB.GetTableProcurement(txtFromDate.Value, txtToDate.Value)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_Procurement.GroupDescriptors.Clear()
                gv_Procurement.EnableGrouping = False
                gv_Procurement.MasterTemplate.SummaryRowsBottom.Clear()
                gv_Procurement.DataSource = dt
                gv_Procurement.Columns("RI").IsVisible = False
                gv_Procurement.Columns("LOCCode").IsVisible = False
                gv_Procurement.MasterTemplate.BestFitColumns()
                gv_Procurement.EnableFiltering = True
                For i As Integer = 0 To gv_Procurement.Columns.Count - 1
                    gv_Procurement.Columns(i).BestFit()
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub MilkReceivedAtFactory()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "MilkReceived"
            Dim dt As DataTable = clsDB.GetTableMilkReceivedAtFactory(txtFromDate.Value, txtToDate.Value)
            gv_MilkReceived.DataSource = Nothing
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_MilkReceived.GroupDescriptors.Clear()
                gv_MilkReceived.MasterTemplate.SummaryRowsBottom.Clear()
                gv_MilkReceived.DataSource = dt
                gv_MilkReceived.MasterTemplate.BestFitColumns()
                gv_MilkReceived.EnableFiltering = True
                For i As Integer = 0 To gv_MilkReceived.Columns.Count - 1
                    gv_MilkReceived.Columns(i).BestFit()
                Next
                gv_MilkReceived.Columns("RI").IsVisible = False
                gv_MilkReceived.Columns("LOCCode").IsVisible = False
                gv_MilkReceived.Columns("Incentive Amount").IsVisible = False
                gv_MilkReceived.Columns("Rent Amount").IsVisible = False
                gv_MilkReceived.Columns("Prodcurement depart Salary").IsVisible = False
                gv_MilkReceived.Columns("Field Staff Fuel").IsVisible = False
                gv_MilkReceived.Columns("Emp CPL").IsVisible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadMilkSale()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "MilkSale"
            gv_MilkSale.DataSource = Nothing
            Dim dtAll As DataTable = clsDB.GetTableMilkSale(txtFromDate.Value, txtToDate.Value)
            If dtAll IsNot Nothing OrElse dtAll.Rows.Count > 0 Then
                gv_MilkSale.GroupDescriptors.Clear()
                gv_MilkSale.MasterTemplate.SummaryRowsBottom.Clear()
                gv_MilkSale.DataSource = dtAll
                gv_MilkSale.MasterTemplate.BestFitColumns()
                gv_MilkSale.EnableFiltering = True
                For i As Integer = 0 To gv_MilkSale.Columns.Count - 1
                    gv_MilkSale.Columns(i).ReadOnly = True
                    gv_MilkSale.Columns(i).BestFit()
                Next
                FormatGrid(gv_MilkSale)
                View(gv_MilkSale)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid(ByRef GridName As RadGridView)

        GridName.Columns("Quantity In Ltr").HeaderText = "Sales"
        GridName.Columns("Quantity In Ltr").FormatString = "{0:F2}"
        GridName.Columns("Scheme Quantity In Ltr").HeaderText = "Scheme"
        GridName.Columns("Sample Quantity In Ltr").HeaderText = "Sample"

        GridName.Columns("Quantity In Kg").HeaderText = "Sales"
        GridName.Columns("Quantity In Kg").FormatString = "{0:F2}"
        GridName.Columns("Scheme Quantity In Kg").HeaderText = "Scheme"
        GridName.Columns("Sample Quantity In Kg").HeaderText = "Sample"

        GridName.Columns("FAT KG").HeaderText = "Sales"
        GridName.Columns("FAT KG").FormatString = "{0:F2}"
        GridName.Columns("Scheme FAT KG").HeaderText = "Scheme"
        GridName.Columns("Sample FAT KG").HeaderText = "Sample"

        GridName.Columns("SNF KG").HeaderText = "Sales"
        GridName.Columns("SNF KG").FormatString = "{0:F2}"
        GridName.Columns("Scheme SNF KG").HeaderText = "Scheme"
        GridName.Columns("Sample SNF KG").HeaderText = "Sample"

        GridName.Columns("Sale Amount").HeaderText = "Sales"
        GridName.Columns("Sale Amount").FormatString = "{0:F2}"
        GridName.Columns("Scheme Sale Amount").HeaderText = "Scheme"
        GridName.Columns("Sample Sale Amount").HeaderText = "Sample"

        GridName.Columns("Ave Realisa Per Ltr").HeaderText = "Sales"
        GridName.Columns("Ave Realisa Per Ltr").FormatString = "{0:F2}"
        GridName.Columns("Scheme Ave Realisa Per Ltr").HeaderText = "Scheme"
        GridName.Columns("Sample Ave Realisa Per Ltr").HeaderText = "Sample"

        GridName.Columns("Ave Realisa Per Kg").HeaderText = "Sales"
        GridName.Columns("Ave Realisa Per Kg").FormatString = "{0:F2}"
        GridName.Columns("Scheme Ave Realisa Per Kg").HeaderText = "Scheme"
        GridName.Columns("Sample Ave Realisa Per Kg").HeaderText = "Sample"


        GridName.Columns("Total Quantity In Ltr").HeaderText = "Total Quantity In Ltr"
        GridName.Columns("Total Quantity In Ltr").FormatString = "{0:F2}"

        GridName.Columns("Total Quantity In Kg").HeaderText = "Total Quantity In Kg"
        GridName.Columns("Total Quantity In Kg").FormatString = "{0:F2}"

        GridName.Columns("Total FAT KG").HeaderText = "Total FAT KG"
        GridName.Columns("Total FAT KG").FormatString = "{0:F2}"

        GridName.Columns("Total SNF KG").HeaderText = "Total SNF KG"
        GridName.Columns("Total SNF KG").FormatString = "{0:F2}"

        GridName.Columns("Total Sale Amount").HeaderText = "Total Sale Amount"
        GridName.Columns("Total Sale Amount").FormatString = "{0:F2}"

        Dim summaryRowItem As New GridViewSummaryRowItem()

        Dim QtyInLTR As New GridViewSummaryItem("Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(QtyInLTR)
        Dim SchemeQtyInLTR As New GridViewSummaryItem("Scheme Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeQtyInLTR)
        Dim SampleQtyInLTR As New GridViewSummaryItem("Sample Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleQtyInLTR)

        Dim QtyInKG As New GridViewSummaryItem("Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(QtyInKG)
        Dim SchemeQtyInKG As New GridViewSummaryItem("Scheme Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeQtyInKG)
        Dim SampleQtyInKG As New GridViewSummaryItem("Sample Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleQtyInKG)

        Dim FATKG As New GridViewSummaryItem("FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(FATKG)
        Dim SchemeFATKG As New GridViewSummaryItem("Scheme FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeFATKG)
        Dim SampleFATKG As New GridViewSummaryItem("Sample FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleFATKG)

        Dim SNFKG As New GridViewSummaryItem("SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SNFKG)
        Dim SchemeSNFKG As New GridViewSummaryItem("Scheme SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeSNFKG)
        Dim SampleSNFKG As New GridViewSummaryItem("Sample SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSNFKG)

        Dim SaleAmount As New GridViewSummaryItem("Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SaleAmount)
        Dim SchemeSaleAmount As New GridViewSummaryItem("Scheme Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SchemeSaleAmount)
        Dim SampleSaleAmount As New GridViewSummaryItem("Sample Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)


        SampleSaleAmount = New GridViewSummaryItem("Total Quantity In Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total Quantity In Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total FAT KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total SNF KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)

        SampleSaleAmount = New GridViewSummaryItem("Total Sale Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SampleSaleAmount)


        GridName.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        GridName.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Sub View(ByRef GridName As RadGridView)

        If GridName.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(GridName.Columns("Item").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity In Ltr"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Quantity In Ltr").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Quantity In Ltr").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Sample Quantity In Ltr").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(GridName.Columns("Total Quantity In Ltr").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity In Kg"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Sample Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Total Quantity In Kg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("FAT KG"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Scheme FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Sample FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Total FAT KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("SNF KG"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Scheme SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Sample SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Total SNF KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Sale Amount"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Sample Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Total Sale Amount").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Ave Realisa Per Ltr"))
            view.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(GridName.Columns("Ave Realisa Per Ltr").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Ave Realisa Per Ltr").Name)
            view.ColumnGroups(6).Rows(0).ColumnNames.Add(GridName.Columns("Sample Ave Realisa Per Ltr").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Ave Realisa Per Kg"))
            view.ColumnGroups(7).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(GridName.Columns("Ave Realisa Per Kg").Name)
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Ave Realisa Per Kg").Name)
            view.ColumnGroups(7).Rows(0).ColumnNames.Add(GridName.Columns("Sample Ave Realisa Per Kg").Name)

            GridName.ViewDefinition = view
        End If

    End Sub

    Sub LoadProductSale()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "ProductSale"
            gv_ProductSale.DataSource = Nothing
            Dim dtAll As DataTable = clsDB.GetTableProductSale(txtFromDate.Value, txtToDate.Value)
            If dtAll IsNot Nothing OrElse dtAll.Rows.Count > 0 Then
                gv_ProductSale.GroupDescriptors.Clear()
                gv_ProductSale.MasterTemplate.SummaryRowsBottom.Clear()
                gv_ProductSale.DataSource = dtAll
                gv_ProductSale.MasterTemplate.BestFitColumns()
                gv_ProductSale.EnableFiltering = True
                For i As Integer = 0 To gv_ProductSale.Columns.Count - 1
                    gv_ProductSale.Columns(i).ReadOnly = True
                    gv_ProductSale.Columns(i).BestFit()
                Next
                FormatGrid(gv_ProductSale)
                View(gv_ProductSale)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadTransportCharges()
        Try
            Dim Qry As String = clsDB.GetQueryTransportCharges(txtFromDate.Value, txtToDate.Value)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gvTransportcost.DataSource = dt
                gvTransportcost.GroupDescriptors.Clear()
                gvTransportcost.MasterTemplate.SummaryRowsBottom.Clear()
                gvTransportcost.MasterTemplate.BestFitColumns()
                gvTransportcost.EnableFiltering = True
                For i As Integer = 0 To gvTransportcost.Columns.Count - 1
                    gvTransportcost.Columns(i).BestFit()
                Next
                gvTransportcost.Columns("Zone").IsVisible = False
                gvTransportcost.Columns("Route").IsVisible = False
                gvTransportcost.Columns("Vehicle").IsVisible = False

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim SalesInLTR As New GridViewSummaryItem("Sales In LTR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR)
                Dim SalesValue As New GridViewSummaryItem("Sales Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesValue)
                Dim FreightAmount As New GridViewSummaryItem("Freight Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(FreightAmount)

                gvTransportcost.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvTransportcost.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadPO()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "StoreReport1"
            Dim Qry As String = clsDB.GetQueryStorePO(txtFromDate.Value, txtToDate.Value)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_po.DataSource = dt
                gv_po.GroupDescriptors.Clear()
                gv_po.MasterTemplate.SummaryRowsBottom.Clear()
                gv_po.MasterTemplate.BestFitColumns()
                gv_po.EnableFiltering = True
                gv_po.Columns("Structure_Code").IsVisible = False
                For i As Integer = 0 To gv_po.Columns.Count - 1
                    gv_po.Columns(i).BestFit()
                Next
                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim NoofPO As New GridViewSummaryItem("No of PO", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(NoofPO)
                Dim NoofGRN As New GridViewSummaryItem("No of GRN", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(NoofGRN)
                Dim NoofSRN As New GridViewSummaryItem("No of SRN", "", GridAggregateFunction.Sum)
                summaryRowItem.Add(NoofSRN)
                Dim Values As New GridViewSummaryItem("Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Values)
                gv_po.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv_po.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadStore()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "StoreReport"
            Dim Qry As String = clsDB.GetQueryStoreStore(txtFromDate.Value, txtToDate.Value)
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_store.DataSource = dt
                gv_store.GroupDescriptors.Clear()
                gv_store.MasterTemplate.SummaryRowsBottom.Clear()
                gv_store.MasterTemplate.BestFitColumns()
                gv_store.EnableFiltering = True
                For i As Integer = 0 To gv_store.Columns.Count - 1
                    gv_store.Columns(i).BestFit()
                Next
                gv_store.Columns("Structure_Code").IsVisible = False
                gv_store.Columns("ITEM_TYPE_CODE").IsVisible = False
                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim OpeningStockValue As New GridViewSummaryItem("Opening Stock Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(OpeningStockValue)
                Dim Purchase As New GridViewSummaryItem("Purchase", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Purchase)
                Dim Issues As New GridViewSummaryItem("Issues", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Issues)
                Dim ClosingValue As New GridViewSummaryItem("Closing Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(ClosingValue)
                Dim Consumption As New GridViewSummaryItem("Consumption", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Consumption)

                gv_store.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv_store.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadMassBalance(ByVal isSFG As Boolean, ByVal gv1 As RadGridView)
        Try
            Dim strTotalInput As String = ""
            Dim strTotalOutput As String = ""
            Dim qry As String = clsDB.GetQueryMassBalance(Nothing, txtFromDate.Value, txtToDate.Value, IIf(isSFG, 1, 2), "", "", False, strTotalInput, strTotalOutput)
            Dim strColPraticularName As String = "ParticularName"
            qry = "select Alpha,case when max(Trans)='' then max(ParticularName) else max(Trans) end as Trans,sum(QtyKg) as QtyKg,sum(QtyLtr) as QtyLtr,case when sum(QtyKg)=0 then 0 else cast( sum(Fat_KG)*100/sum(QtyKg)as decimal(18,2)) end as Fat_Per, case when sum(QtyKg)=0 then 0 else CAST(sum(SNF_KG)*100/sum(QtyKg)as decimal(18,2)) end as SNF_Per ,sum(Fat_KG) as Fat_KG,sum(SNF_KG) as SNF_KG,sum(Avg_Cost) as Avg_Cost from (" + Environment.NewLine + qry + Environment.NewLine + ")xxx group by Alpha"
            strColPraticularName = "Trans"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                qry = strTotalInput + Environment.NewLine + " Union all " + Environment.NewLine + strTotalOutput
                Dim dtTotal As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dtTotal IsNot Nothing AndAlso dtTotal.Rows.Count > 0 Then
                    Dim drKg As DataRow = dt.NewRow()
                    drKg("Alpha") = "L"
                    drKg(strColPraticularName) = "Kg FAT & Kg SNF Loss/Gain"
                    drKg("Fat_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Fat_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)
                    drKg("SNF_KG") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("SNF_KG")) - clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)
                    drKg("Avg_Cost") = Math.Round(clsCommon.myCdbl(dtTotal.Rows(1)("Avg_Cost")) - clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)

                    Dim drPer As DataRow = dt.NewRow()
                    drPer("Alpha") = "M"
                    drPer(strColPraticularName) = "Kg FAT & Kg SNF Loss/Gain %"
                    If clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")) <> 0 Then
                        drPer("Fat_KG") = Math.Round((clsCommon.myCdbl(drKg("Fat_KG")) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Fat_KG")), 2, MidpointRounding.ToEven)
                    End If
                    If clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")) <> 0 Then
                        drPer("SNF_KG") = Math.Round((clsCommon.myCdbl(drKg("SNF_KG")) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("SNF_KG")), 2, MidpointRounding.ToEven)
                    End If
                    If clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")) <> 0 Then
                        drPer("Avg_Cost") = Math.Round((clsCommon.myCdbl(drKg("Avg_Cost")) * 100) / clsCommon.myCdbl(dtTotal.Rows(0)("Avg_Cost")), 2, MidpointRounding.ToEven)
                    End If

                    Dim drTS As DataRow = dt.NewRow()
                    drTS("Alpha") = "N"
                    drTS(strColPraticularName) = "Total TS Loss/gain %"
                    drTS("Fat_KG") = clsCommon.myCdbl(drPer("Fat_KG")) + clsCommon.myCdbl(drPer("SNF_KG"))

                    dt.Rows.Add(drKg)
                    dt.Rows.Add(drPer)
                    dt.Rows.Add(drTS)
                End If
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = False
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = True
                Next
                gv1.BestFitColumns()
                gv1.Columns("Alpha").HeaderText = "Alpha"
                gv1.Columns("QtyKg").HeaderText = "Qty KG"
                gv1.Columns("QtyLtr").HeaderText = "Qty Ltr"
                gv1.Columns("Fat_Per").HeaderText = "FAT %"
                gv1.Columns("SNF_Per").HeaderText = "SNF %"
                gv1.Columns("Fat_KG").HeaderText = "FAT Kg"
                gv1.Columns("SNF_KG").HeaderText = "SNF Kg"
                gv1.Columns("Avg_Cost").HeaderText = "Amount"
            End If
            gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvFGMassBalance_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvFGMassBalance.CellDoubleClick
        Try
            OpenMass(gvFGMassBalance, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gvSFGMassBalance_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvSFGMassBalance.CellDoubleClick
        Try
            OpenMass(gvSFGMassBalance, False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenMass(ByVal gv As RadGridView, ByVal isFG As Boolean)
        Try
            Dim frm As New rptMassBalanceReport
            frm.SetUserMgmt(clsUserMgtCode.MISMassBalanceReport)
            frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.MISMassBalanceReport)
            frm.FilterON = True
            frm.FilterfromDate = txtFromDate.Value
            frm.FilterToDate = txtToDate.Value
            frm.FilterisFG = isFG
            frm.FilterAlpha = clsCommon.myCstr(gv.CurrentRow.Cells("Alpha").Value)
            frm.MdiParent = MDI
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv3_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv3.CellDoubleClick
        Try
            Dim frm As New FrmBankBook(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
            frm.SetUserMgmt(clsUserMgtCode.frmBankBook)
            frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.frmBankBook)
            frm.FilterON = True
            frm.FilterfromDate = txtFromDate.Value
            frm.FilterToDate = txtToDate.Value
            frm.FilterBankCode = clsCommon.myCstr(gv3.CurrentRow.Cells("Bank Code").Value)
            frm.MdiParent = MDI
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_MilkSale_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv_MilkSale.CellDoubleClick
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv_MilkSale.CurrentRow.Cells("Item").Value)) > 0 Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select item_code from tspl_item_master where Alies_Name='" + clsCommon.myCstr(gv_MilkSale.CurrentRow.Cells("Item").Value) + "'")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frm As RptSaleRegisterReport
                    frm = New RptSaleRegisterReport(clsUserMgtCode.MISSaleRegister)
                    frm.isReadFlag = True
                    frm.isDataLoad = True
                    frm.dtFrom = txtFromDate.Value
                    frm.dtTo = txtToDate.Value
                    frm.Unit_Code = "Ltr"
                    'frm.arrTransaction = txtTransaction.arrValueMember
                    frm.arrItem = New ArrayList()
                    For Each dr As DataRow In dt.Rows
                        frm.arrItem.Add(clsCommon.myCstr(dr("item_code")))
                    Next

                    'frm.arrItemGroup = txtItemGroup.arrValueMember
                    'frm.arrLocation = txtLocation.arrValueMember
                    'frm.arrCustomer = txtCustomer.arrValueMember
                    'frm.arrCustGroup = txtCustGroup.arrValueMember
                    'frm.arrState = txtState.arrValueMember
                    'frm.arrCat = New Dictionary(Of String, Object)
                    'Dim arrSel As Dictionary(Of String, Object) = Nothing
                    'Dim TempCode As String = ""
                    'Dim dtCategory As DataTable

                    'dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
                    'If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                    '    For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    '        If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value), "") = CompairStringResult.Equal Then
                    '            Exit For
                    '        End If
                    '        arrSel = New Dictionary(Of String, Object)
                    '        TempCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "' AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value) + "'"))
                    '        arrSel.Add(TempCode, Nothing)
                    '        frm.arrCat.Add(clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim(), arrSel)
                    '    Next
                    'End If


                    frm.strType = "Item Wise"
                    frm.WindowState = FormWindowState.Maximized
                    frm.Focus()
                    frm.Visible = False
                    frm.MdiParent = MDI
                    frm.Show()
                End If
            Else
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select item_code+'  ['+Item_Desc+']' as Item from tspl_item_master where item_type='F' and Alies_Name='" + clsCommon.myCstr(gv_MilkSale.CurrentRow.Cells("Item").Value) + "'")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim exc As String = "Please Set the Alias Name of following Items"
                    For Each dr As DataRow In dt.Rows
                        exc += Environment.NewLine + clsCommon.myCstr(dr("Item"))
                    Next
                    Dim logFile As String = "MissingItemAliesName.txt"
                    If System.IO.File.Exists(logFile) Then
                        Dim stream As New IO.StreamWriter(logFile, False)
                        stream.WriteLine("")
                        stream.Close()
                    Else
                        Dim fs As IO.FileStream = System.IO.File.Create(logFile)
                        fs.Close()
                    End If
                    Dim objWriter As New System.IO.StreamWriter(logFile, True)
                    objWriter.WriteLine(exc)
                    objWriter.Close()

                    Dim objreader As New System.IO.StringReader(logFile)
                    If objreader IsNot Nothing AndAlso clsCommon.myLen(objreader) > 0 Then
                        Dim str As String = clsCommon.myCstr(System.IO.File.ReadAllText(logFile))
                        If clsCommon.myLen(str) > 0 Then
                            System.Diagnostics.Process.Start(logFile)
                        End If
                    End If

                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_ProductSale_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv_ProductSale.CellDoubleClick
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select item_code from tspl_item_master where Alies_Name='" + clsCommon.myCstr(gv_ProductSale.CurrentRow.Cells("Item").Value) + "'")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As RptSaleRegisterReport
                frm = New RptSaleRegisterReport(clsUserMgtCode.MISSaleRegister)
                frm.isReadFlag = True
                frm.isDataLoad = True
                frm.dtFrom = txtFromDate.Value
                frm.dtTo = txtToDate.Value
                frm.Unit_Code = "Ltr"
                'frm.arrTransaction = txtTransaction.arrValueMember
                frm.arrItem = New ArrayList()
                For Each dr As DataRow In dt.Rows
                    frm.arrItem.Add(clsCommon.myCstr(dr("item_code")))
                Next

                'frm.arrItemGroup = txtItemGroup.arrValueMember
                'frm.arrLocation = txtLocation.arrValueMember
                'frm.arrCustomer = txtCustomer.arrValueMember
                'frm.arrCustGroup = txtCustGroup.arrValueMember
                'frm.arrState = txtState.arrValueMember
                'frm.arrCat = New Dictionary(Of String, Object)
                'Dim arrSel As Dictionary(Of String, Object) = Nothing
                'Dim TempCode As String = ""
                'Dim dtCategory As DataTable

                'dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
                'If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                '    For ii As Integer = 0 To dtCategory.Rows.Count - 1
                '        If clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value), "") = CompairStringResult.Equal Then
                '            Exit For
                '        End If
                '        arrSel = New Dictionary(Of String, Object)
                '        TempCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "' AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim()).Value) + "'"))
                '        arrSel.Add(TempCode, Nothing)
                '        frm.arrCat.Add(clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim(), arrSel)
                '    Next
                'End If


                frm.strType = "Item Wise"
                frm.WindowState = FormWindowState.Maximized
                frm.Focus()
                frm.Visible = False
                frm.MdiParent = MDI
                frm.Show()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvTransportcost_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvTransportcost.CellDoubleClick
        Try
            Dim frm As New rptSalesVehicleReport()
            frm.SetUserMgmt(clsUserMgtCode.rptSalesVehicleReport)
            frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.rptSalesVehicleReport)
            frm.FilterON = True
            frm.FilterfromDate = txtFromDate.Value
            frm.FilterToDate = txtToDate.Value

            frm.FilterArrZone = New ArrayList
            frm.FilterArrZone.Add(clsCommon.myCstr(gvTransportcost.CurrentRow.Cells("Zone").Value))

            frm.FilterArrRoute = New ArrayList
            frm.FilterArrRoute.Add(clsCommon.myCstr(gvTransportcost.CurrentRow.Cells("Route").Value))

            frm.FilterArrVehicleBrand = New ArrayList
            frm.FilterArrVehicleBrand.Add(clsCommon.myCstr(gvTransportcost.CurrentRow.Cells("Vehicle").Value))
            frm.FilterProvisionEntry = True
            frm.MdiParent = MDI
            frm.Show()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_store_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv_store.CellDoubleClick
        Try
            Dim qry As String = "select item_code from TSPL_ITEM_MASTER  where Structure_Code='" + clsCommon.myCstr(gv_store.CurrentRow.Cells("Structure_Code").Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New FrmStockReco(clsUserMgtCode.stockRecoNew)
                frm.SetUserMgmt(clsUserMgtCode.stockRecoNew)
                frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.stockRecoNew)
                frm.isDataLoad = True
                frm.dtFrom = txtFromDate.Value
                frm.dtTo = txtToDate.Value
                frm.arrItem = New ArrayList
                For Each dr As DataRow In dt.Rows
                    frm.arrItem.Add(clsCommon.myCstr(dr("item_code")))
                Next
                frm.arrItemType = New ArrayList
                frm.arrItemType.Add(clsCommon.myCstr(gv_store.CurrentRow.Cells("ITEM_TYPE_CODE").Value))
                frm.strType = "Item And Location Wise Summary"
                frm.MdiParent = MDI

                frm.Show()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_Procurement_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv_Procurement.CellDoubleClick
        Try
            If clsCommon.myCdbl(gv_Procurement.CurrentRow.Cells("RI").Value) > 0 AndAlso clsCommon.myLen(gv_Procurement.CurrentRow.Cells("LOCCode").Value) > 0 Then
                If clsCommon.myCdbl(gv_Procurement.CurrentRow.Cells("RI").Value) = 1 Then
                    If gv_Procurement.CurrentColumn Is gv_Procurement.Columns("Freight Cost") Then
                        Dim frm As New frmRptMCCProvision()
                        frm.SetUserMgmt(clsUserMgtCode.MCCProvisonReport)
                        frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.MCCProvisonReport)
                        frm.FilterON = True
                        frm.FilterfromDate = txtFromDate.Value
                        frm.FilterToDate = txtToDate.Value
                        frm.FilterMCCCode = clsCommon.myCstr(gv_Procurement.CurrentRow.Cells("LOCCode").Value)
                        frm.MdiParent = MDI
                        frm.Show()
                    Else
                        Dim frm As New FrmMCCMilkRegister()
                        frm.SetUserMgmt(clsUserMgtCode.MCCMilkRegister)
                        frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.MCCMilkRegister)
                        frm.FilterON = True
                        frm.FilterfromDate = txtFromDate.Value
                        frm.FilterToDate = txtToDate.Value
                        frm.FilterMCCCode = clsCommon.myCstr(gv_Procurement.CurrentRow.Cells("LOCCode").Value)
                        frm.MdiParent = MDI
                        frm.Show()
                    End If
                ElseIf clsCommon.myCdbl(gv_Procurement.CurrentRow.Cells("RI").Value) = 2 Then
                    Dim frm As New RptBulkMilkRegister()
                    frm.SetUserMgmt(clsUserMgtCode.RptBulkMilkRegister)
                    frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.RptBulkMilkRegister)
                    frm.FilterON = True
                    frm.FilterfromDate = txtFromDate.Value
                    frm.FilterToDate = txtToDate.Value
                    frm.FilterArrDocType = New ArrayList
                    frm.FilterArrDocType.Add("Bulk In")
                    frm.FilterLocationCode = clsCommon.myCstr(gv_Procurement.CurrentRow.Cells("LOCCode").Value)
                    frm.FilterVendorName = clsCommon.myCstr(gv_Procurement.CurrentRow.Cells("MCC Name").Value)
                    frm.MdiParent = MDI
                    frm.Show()
                ElseIf clsCommon.myCdbl(gv_Procurement.CurrentRow.Cells("RI").Value) = 3 Then
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_MilkReceived_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv_MilkReceived.CellDoubleClick
        Try
            If clsCommon.myCdbl(gv_MilkReceived.CurrentRow.Cells("RI").Value) > 0 AndAlso clsCommon.myLen(gv_MilkReceived.CurrentRow.Cells("LOCCode").Value) > 0 Then
                If clsCommon.myCdbl(gv_MilkReceived.CurrentRow.Cells("RI").Value) = 1 Then
                    If gv_MilkReceived.CurrentColumn Is gv_MilkReceived.Columns("Freight Cost") Then
                        Dim frm As New RpttankerReportForErode()
                        frm.SetUserMgmt(clsUserMgtCode.RpttankerReport)
                        frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.RpttankerReport)
                        frm.FilterON = True
                        frm.FilterfromDate = txtFromDate.Value
                        frm.FilterToDate = txtToDate.Value
                        frm.FilterMCCCode = clsCommon.myCstr(gv_MilkReceived.CurrentRow.Cells("LOCCode").Value)
                        frm.MdiParent = MDI
                        frm.Show()
                    Else
                        Dim frm As New RptBulkMilkRegister()
                        frm.SetUserMgmt(clsUserMgtCode.RptBulkMilkRegister)
                        frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.RptBulkMilkRegister)
                        frm.FilterON = True
                        frm.FilterfromDate = txtFromDate.Value
                        frm.FilterToDate = txtToDate.Value
                        frm.FilterArrDocType = New ArrayList
                        frm.FilterArrDocType.Add("MCC In")
                        frm.FilterLocationCode = clsCommon.myCstr(gv_MilkReceived.CurrentRow.Cells("LOCCode").Value)
                        frm.MdiParent = MDI
                        frm.Show()
                    End If
                ElseIf clsCommon.myCdbl(gv_MilkReceived.CurrentRow.Cells("RI").Value) = 2 Then
                    Dim frm As New RptBulkMilkRegister()
                    frm.SetUserMgmt(clsUserMgtCode.RptBulkMilkRegister)
                    frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.RptBulkMilkRegister)
                    frm.FilterON = True
                    frm.FilterfromDate = txtFromDate.Value
                    frm.FilterToDate = txtToDate.Value
                    frm.FilterArrDocType = New ArrayList
                    frm.FilterArrDocType.Add("Bulk In")
                    frm.FilterLocationCode = clsCommon.myCstr(gv_MilkReceived.CurrentRow.Cells("LOCCode").Value)
                    frm.FilterVendorName = clsCommon.myCstr(gv_MilkReceived.CurrentRow.Cells("MCC Name").Value)
                    frm.MdiParent = MDI
                    frm.Show()
                ElseIf clsCommon.myCdbl(gv_MilkReceived.CurrentRow.Cells("RI").Value) = 3 Then
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv4_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv4.CellDoubleClick
        Try
            If gv4.CurrentColumn Is gv4.Columns("Sales In LTR") Then

            Else
                Dim frm As New rptSalesVehicleReport()
                frm.SetUserMgmt(clsUserMgtCode.rptSalesVehicleReport)
                frm.Text = clsUserMgtCode.GetName(clsUserMgtCode.rptSalesVehicleReport)
                frm.FilterON = True
                frm.FilterfromDate = txtFromDate.Value
                frm.FilterToDate = txtToDate.Value
                frm.FilterProvisionEntry = False
                frm.FilterArrVehicleBrand = New ArrayList
                frm.FilterArrVehicleBrand.Add(clsCommon.myCstr(gv4.CurrentRow.Cells("Vehicle Type").Value))
                frm.MdiParent = MDI
                frm.Show()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        chkBankCashBook.Checked = CheckBox1.Checked
        chkVehicleUtilization.Checked = CheckBox1.Checked
        chkProcurementMilkPurchase.Checked = CheckBox1.Checked
        chkMilkReceivedAtFactory.Checked = CheckBox1.Checked
        chkMilkSale.Checked = CheckBox1.Checked
        chkProductSale.Checked = CheckBox1.Checked
        chkFGMassBalance.Checked = CheckBox1.Checked
        chkSFGMassBalance.Checked = CheckBox1.Checked
        chkPO.Checked = CheckBox1.Checked
        chkStore.Checked = CheckBox1.Checked
        chkTransportCosting.Checked = CheckBox1.Checked
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub gv_po_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv_po.CellDoubleClick
        Try

            If clsCommon.myLen(clsCommon.myCstr(gv_po.CurrentRow.Cells("Structure_Code").Value)) > 0 Then
                Dim frm As FrmPurchaseOrderRegister
                frm = New FrmPurchaseOrderRegister()
                frm.isReadFlag = True
                frm.isDataLoad = True
                frm.dtFrom = txtFromDate.Value
                frm.dtTo = txtToDate.Value
                frm.arrStructure = New ArrayList()
                frm.arrStructure.Add(clsCommon.myCstr(gv_po.CurrentRow.Cells("Structure_Code").Value))
                frm.strType = "Detail"
                frm.WindowState = FormWindowState.Maximized
                frm.Focus()
                frm.Visible = False
                frm.MdiParent = MDI
                frm.Show()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Export(ByVal ExportType As Exporter)
        Dim arrHeader As List(Of String) = New List(Of String)()
        arrHeader.Add("Name : Dashboard Report")
        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        arrHeader.Add(("Date Range : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

        If ExportType = Exporter.Excel Then
            If clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "All") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv3, "", "Bank Cash Book", , arrHeader)
                transportSql.QuickExportToExcel(gv4, "", "Sales Vehicle Utilization", , arrHeader)
                transportSql.QuickExportToExcel(gv_Procurement, "", "Procurement Milk Purchase", , arrHeader)
                transportSql.QuickExportToExcel(gv_MilkReceived, "", "Milk Received At Factory", , arrHeader)
                transportSql.QuickExportToExcel(gv_MilkSale, "", "Milk Sale", , arrHeader)
                transportSql.QuickExportToExcel(gv_ProductSale, "", "Product Sale", , arrHeader)
                transportSql.QuickExportToExcel(gvFGMassBalance, "", "FG Mass Balance", , arrHeader)
                transportSql.QuickExportToExcel(gvSFGMassBalance, "", "SFG/Raw Milk Mass Balance", , arrHeader)
                transportSql.QuickExportToExcel(gvTransportcost, "", "Sales Transport Costing", , arrHeader)
                transportSql.QuickExportToExcel(gv_po, "", "Purchase", , arrHeader)
                transportSql.QuickExportToExcel(gv_store, "", "Stock/Inventory", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Bank Cash Book") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv3, "", "Bank Cash Book", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Vehicle Utilization") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv4, "", "Sales Vehicle Utilization", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Procurement Milk Purchase") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv_Procurement, "", "Procurement Milk Purchase", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Received At Factory") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv_MilkReceived, "", "Milk Received At Factory", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Sale") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv_MilkSale, "", "Milk Sale", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Product Sale") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv_ProductSale, "", "Product Sale", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "FG Mass Balance") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gvFGMassBalance, "", "FG Mass Balance", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "SFG/Raw Milk Mass Balance") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gvSFGMassBalance, "", "SFG/Raw Milk Mass Balance", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Transport Costing") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gvTransportcost, "", "Sales Transport Costing", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Purchase") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv_po, "", "Purchase", , arrHeader)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Stock/Inventory") = CompairStringResult.Equal Then
                transportSql.QuickExportToExcel(gv_store, "", "Stock/Inventory", , arrHeader)
            End If
        ElseIf ExportType = Exporter.PDF Then
            If clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "All") = CompairStringResult.Equal Then
                If gv3.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Bank Cash Book", gv3, arrHeader, "Bank Cash Book")
                End If
                If gv4.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Sales Vehicle Utilization", gv4, arrHeader, "Sales Vehicle Utilization")
                End If
                If gv_Procurement.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Procurement Milk Purchase", gv_Procurement, arrHeader, "Procurement Milk Purchase")
                End If
                If gv_MilkReceived.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Milk Received At Factory", gv_MilkReceived, arrHeader, "Milk Received At Factory")
                End If
                If gv_MilkSale.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Milk Sale", gv_MilkSale, arrHeader, "Milk Sale")
                End If
                If gv_ProductSale.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Product Sale", gv_ProductSale, arrHeader, "Product Sale")
                End If
                If gvFGMassBalance.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("FG Mass Balance", gvFGMassBalance, arrHeader, "FG Mass Balance")
                End If
                If gvSFGMassBalance.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("SFG-Raw Milk Mass Balance", gvSFGMassBalance, arrHeader, "SFG-Raw Milk Mass Balance")
                End If
                If gvTransportcost.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Sales Transport Costing", gvTransportcost, arrHeader, "Sales Transport Costing")
                End If
                If gv_po.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Purchase", gv_po, arrHeader, "Purchase")
                End If
                If gv_store.Rows.Count > 0 Then
                    clsCommon.MyExportToPDF("Stock-Inventory", gv_store, arrHeader, "Stock-Inventory")
                End If

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Bank Cash Book") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("Bank Cash Book", gv3, arrHeader, "Bank Cash Book")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Vehicle Utilization") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("Sales Vehicle Utilization", gv4, arrHeader, "Sales Vehicle Utilization")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Procurement Milk Purchase") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("Procurement Milk Purchase", gv_Procurement, arrHeader, "Procurement Milk Purchase")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Received At Factory") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("Milk Received At Factory", gv_MilkReceived, arrHeader, "Milk Received At Factory")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Milk Sale") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("Milk Sale", gv_MilkSale, arrHeader, "Milk Sale")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Product Sale") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("Product Sale", gv_ProductSale, arrHeader, "Product Sale")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "FG Mass Balance") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("FG Mass Balance", gvFGMassBalance, arrHeader, "FG Mass Balance")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "SFG/Raw Milk Mass Balance") = CompairStringResult.Equal Then
                clsCommon.MyExportToPDF("SFG-Raw Milk Mass Balance", gvSFGMassBalance, arrHeader, "SFG-Raw Milk Mass Balance")
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Sales Transport Costing") = CompairStringResult.Equal Then
                clsCommon.MyExportToPDF("Sales Transport Costing", gvTransportcost, arrHeader, "Sales Transport Costing")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Purchase") = CompairStringResult.Equal Then
                    clsCommon.MyExportToPDF("Purchase", gv_po, arrHeader, "Purchase")
                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboReport.SelectedValue), "Stock/Inventory") = CompairStringResult.Equal Then
                clsCommon.MyExportToPDF("Stock-Inventory", gv_store, arrHeader, "Stock-Inventory")
            End If
        End If
    End Sub

    Private Sub EXExcel_Click(sender As Object, e As EventArgs) Handles EXExcel.Click
        Export(Exporter.Excel)
    End Sub

    Private Sub EXPDF_Click(sender As Object, e As EventArgs) Handles EXPDF.Click
        Export(Exporter.PDF)
    End Sub
End Class


