Imports common
Imports System.Data.SqlClient
Imports Telerik.Charting
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports System.IO
Public Class frmDasboardCombined
    Inherits FrmMainTranScreen

    Private Sub FrmDasboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            txtFromDate.Value = txtToDate.Value.AddMonths(-1)

            Dim TempMonth As Integer = IIf(DateTime.Now.Month = 1, 12, DateTime.Now.Month)
            Dim TempYear As Date = IIf(DateTime.Now.Month = 1, clsCommon.GETSERVERDATE().AddYears(-1), clsCommon.GETSERVERDATE())
            dtpFromDate_Back_Cash_Book.Value = clsCommon.GETSERVERDATE()
            dtpToDate_Bank_Cash_Book.Value = clsCommon.GETSERVERDATE()
            cboFigureInGraph_Bank_Cash_Book.SelectedIndex = 1

            ddlProvMonth.DataSource = clsProvisionEntry.LoadMonthName()
            ddlProvMonth.ValueMember = "monthNumber"
            ddlProvMonth.DisplayMember = "Monthname"
            ddlProvMonth.SelectedValue = TempMonth
            dtpProvYear.Value = TempYear

            dtpFromDate_Procurement.Value = clsCommon.GETSERVERDATE()
            dtpToDate_Procurement.Value = clsCommon.GETSERVERDATE()

            dtpFromDate_MilkRec.Value = clsCommon.GETSERVERDATE()
            dtpToDate_MilkRec.Value = clsCommon.GETSERVERDATE()

            dtpFromDate_MilkSale.Value = clsCommon.GETSERVERDATE()
            dtpToDate_MilkSale.Value = clsCommon.GETSERVERDATE()

            dtpFromDate_ProductSale.Value = clsCommon.GETSERVERDATE()
            dtpToDate_ProductSale.Value = clsCommon.GETSERVERDATE()


            dtpFromDate_Transport.Value = clsCommon.GETSERVERDATE()
            dtpToDate_Transport.Value = clsCommon.GETSERVERDATE()


            dtpFromDate_Store.Value = clsCommon.GETSERVERDATE()
            dtpToDate_Store.Value = clsCommon.GETSERVERDATE()

            SetTabNmae()
            TabHideVisible()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetTabNmae()
        RadPageViewPage1.Name = clsDashBoard.BankCashBook
        RadPageViewPage2.Name = clsDashBoard.VehicleUtili
        RadPageViewPage3.Name = clsDashBoard.ProcMilkPur
        RadPageViewPage4.Name = clsDashBoard.MilkRecAtFac
        RadPageViewPage5.Name = clsDashBoard.MilkSale
        RadPageViewPage6.Name = clsDashBoard.ProductSale
        RadPageViewPage7.Name = clsDashBoard.Transportcos
        RadPageViewPage8.Name = clsDashBoard.StoreReport
    End Sub
    Private Sub TabHideVisible()
        'Tab Visible/Hide
        If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "Admin") = CompairStringResult.Equal Then
            Dim str As String = "select TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING.Dashboard_Code from TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING " &
               " left join TSPL_DASHBOARD_REPORT on TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING.Dashboard_Code=TSPL_DASHBOARD_REPORT.code " &
               " where TSPL_DASHBOARD_GROUP_PROGRAM_MAPPING.Group_Code in " &
               " (select Group_Code  from TSPL_USER_GROUP_MAPPING where User_Code='" + objCommonVar.CurrentUserCode + "') "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To RadPageView1.Pages.Count - 1
                    Dim rows As DataRow() = dt.Select("Dashboard_Code='" + RadPageView1.Pages(i).Name + "'")
                    If rows Is Nothing OrElse rows.Length <= 0 Then
                        RadPageView1.Pages(i).Item.Visibility = ElementVisibility.Collapsed
                    End If
                Next
            Else
                For i As Integer = 0 To RadPageView1.Pages.Count - 1
                    RadPageView1.Pages(i).Item.Visibility = ElementVisibility.Collapsed
                Next
            End If
        End If
    End Sub

    'Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
    '    RadChartView5.Series.Clear()
    '    RadChartView6.Series.Clear()
    '    RadChartView7.Series.Clear()
    '    RadChartView8.Series.Clear()
    '    Dim figure As String = Nothing
    '    If cboFigure.Text = "Crores" Then
    '        figure = 1000000
    '    ElseIf cboFigure.Text = "Lacs" Then
    '        figure = 100000
    '    ElseIf cboFigure.Text = "Ten Thousands" Then
    '        figure = 10000
    '    End If
    '    Dim strAddress As String = " (TSPL_COMPANY_MASTER.Add1 + case When isnull(TSPL_COMPANY_MASTER.Add2,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.Add2 End + Case When isnull(TSPL_COMPANY_MASTER.Add3,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Add3 end + case When isnull(TSPL_COMPANY_MASTER.City_Code,'') ='' then '' else ', '+ TSPL_COMPANY_MASTER.City_Code end+ Case When isnull(TSPL_COMPANY_MASTER.State,'')='' Then '' else ', '+ TSPL_COMPANY_MASTER.State end +  Case When isnull(TSPL_COMPANY_MASTER.Pincode,'')='' Then '' Else ', '+ TSPL_COMPANY_MASTER.Pincode  end) "
    '    Dim Qry As String = clsBankReco.GetQueryForTransactionOFBB(True, dtpFromDate.Value, dtpToDate.Value, "", "", strAddress, "Y", "B", "Bank Book", "", False)
    '    Qry = "Select final.Bank_Code as [Bank Code],Final.Description as [Bank Name] , Final.BalAmt as [Opening Balance],Credit_Amount as [Payments],Debit_Amount as [Receipts],Closing_Balance as [Closing], Convert (decimal(18,2),( Final.BalAmt / " + figure + " ))   as BalAmt_Chart , Convert (decimal(18,2),( Final.Credit_Amount / " + figure + " ))   as Credit_Amount_Chart, Convert (decimal(18,2), (Final.Debit_Amount / " + figure + ") )   as Debit_Amount_Chart, Convert (decimal(18,2), (Final.Closing_Balance / " + figure + ") )   as Closing_Balance_Chart  from ( SELECT BANK_CODE, MAX(DESCRIPTION) AS [DESCRIPTION], MAX(BankType) AS BankType,MAX(Startdate) AS Startdate,MAX(EndDate) AS EndDate,MAX(RunDate) AS RunDate,SUM(BalAmt) AS BalAmt,SUM(Debit_Amount) AS Debit_Amount ,SUM(Credit_Amount) AS Credit_Amount,(SUM(Debit_Amount)-SUM(Credit_Amount)+SUM(BalAmt)) AS Closing_Balance,max(POP.Add1) as Add1   FROM (" + Qry + ")POP GROUP BY BANK_CODE )final Left Outer Join TSPL_COMPANY_MASTER ON '" & objCommonVar.CurrentCompanyCode & "'=TSPL_COMPANY_MASTER.Comp_Code ORDER BY  [Bank_Code] "
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
    '    If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
    '        gv1.DataSource = Nothing
    '        gv1.Rows.Clear()
    '        gv1.Columns.Clear()
    '        gv1.DataSource = dt
    '        gv1.GroupDescriptors.Clear()
    '        gv1.MasterTemplate.BestFitColumns()
    '        gv1.EnableFiltering = True
    '        gv1.Columns("BalAmt_Chart").IsVisible = False
    '        gv1.Columns("Credit_Amount_Chart").IsVisible = False
    '        gv1.Columns("Debit_Amount_Chart").IsVisible = False
    '        gv1.Columns("Closing_Balance_Chart").IsVisible = False
    '        ==============================================================================
    '        Dim barSeries As New Telerik.WinControls.UI.BarSeries("BalAmt_Chart", "Bank Code")
    '        barSeries.Name = "Bank Cash Book Opening Balance"
    '        Me.RadChartView5.Series.Add(barSeries)
    '        barSeries.DataSource = dt

    '        Dim barSeries2 As New Telerik.WinControls.UI.BarSeries("Credit_Amount_Chart", "Bank Code")
    '        barSeries2.Name = "Q2"
    '        Me.RadChartView6.Series.Add(barSeries2)
    '        barSeries2.DataSource = dt

    '        Dim barSeries3 As New Telerik.WinControls.UI.BarSeries("Debit_Amount_Chart", "Bank Code")
    '        barSeries3.Name = "Q3"
    '        Me.RadChartView7.Series.Add(barSeries3)
    '        barSeries3.DataSource = dt

    '        Dim barSeries4 As New Telerik.WinControls.UI.BarSeries("Closing_Balance_Chart", "Bank Code")
    '        barSeries4.Name = "Q4"
    '        Me.RadChartView8.Series.Add(barSeries4)
    '        barSeries4.DataSource = dt

    '        RadPageView1.SelectedPage = RadPageViewPage2


    '    Else
    '        clsCommon.MyMessageBoxShow("Record Not Found.")
    '    End If
    'End Sub

    Private Sub btnGo_Bank_Cash_Book_Click(sender As Object, e As EventArgs) Handles btnGo_Bank_Cash_Book.Click
        LoadBankData()
    End Sub



    Private Sub btnGo_Vehicle_Utilization_Click(sender As Object, e As EventArgs) Handles btnGo_Vehicle_Utilization.Click
        LoadVehicle()
    End Sub


    'Private Sub RadPageView1_SelectedPageChanged(sender As Object, e As EventArgs) Handles RadPageView1.SelectedPageChanged
    '    Try

    '        If clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.BankCashBook) = CompairStringResult.Equal Then
    '            If gv3.DataSource Is Nothing Then
    '                btnGo_Bank_Cash_Book.PerformClick()
    '            End If
    '        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.VehicleUtili) = CompairStringResult.Equal Then
    '            If gv4.DataSource Is Nothing Then
    '                btnGo_Vehicle_Utilization.PerformClick()
    '            End If
    '        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.ProcMilkPur) = CompairStringResult.Equal Then
    '            If gv_Procurement.DataSource Is Nothing Then
    '                btnGo_Procurement.PerformClick()
    '            End If
    '        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.MilkRecAtFac) = CompairStringResult.Equal Then
    '            If gv_MilkReceived.DataSource Is Nothing Then
    '                btnGo_MilkReceived.PerformClick()
    '            End If
    '        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.MilkSale) = CompairStringResult.Equal Then
    '            If gv_MilkSale.DataSource Is Nothing Then
    '                btnGo_MilkSale.PerformClick()
    '            End If
    '        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.ProductSale) = CompairStringResult.Equal Then
    '            If gv_ProductSale.DataSource Is Nothing Then
    '                btnGo_ProductSale.PerformClick()
    '            End If
    '        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.Transportcos) = CompairStringResult.Equal Then
    '            If gvTransportcost.DataSource Is Nothing Then
    '                btn_Go_Transport_cost.PerformClick()
    '            End If
    '        ElseIf clsCommon.CompairString(RadPageView1.SelectedPage.Name, clsDashBoard.StoreReport) = CompairStringResult.Equal Then
    '            If gv_po.DataSource Is Nothing Then
    '                btn_StoreReport.PerformClick()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnGo_Procurement_Click(sender As Object, e As EventArgs) Handles btnGo_Procurement.Click
        LoadProcuremntData()
    End Sub


    Private Sub BCBExcel_Click(sender As Object, e As EventArgs) Handles BCBExcel.Click
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Bank Cash Book")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Back_Cash_Book.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Bank_Cash_Book.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(gv3, "", "Bank Cash Book", , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BCBCSV_Click(sender As Object, e As EventArgs) Handles BCBCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Bank Cash Book"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv3, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub BCBPDF_Click(sender As Object, e As EventArgs) Handles BCBPDF.Click
        Try
            If gv3.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Bank Cash Book")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Back_Cash_Book.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Bank_Cash_Book.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Bank_Cash_Book"
                clsCommon.MyExportToPDF("Bank Cash Book", gv3, arrHeader, "Bank Cash Book", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    'Private Sub VUExcel_Click(sender As Object, e As EventArgs)
    '    Try
    '        If gv4.Rows.Count > 0 Then
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
    '            arrHeader.Add("Name : " & "Vehicle Utilization")
    '            arrHeader.Add("Period : " + clsCommon.myCstr(ddlProvMonth.Text) + " - " + clsCommon.myCstr(Year(dtpProvYear.Value)) + "")
    '            transportSql.QuickExportToExcel(gv4, "", "Vehicle Utilization", , arrHeader)
    '        Else
    '            common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub VUCSV_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim sfd As SaveFileDialog = New SaveFileDialog()
    '        Dim filePath As String
    '        sfd.FileName = "Vehicle Utilization"
    '        sfd.Filter = "CSV Files (*.csv) |*.csv"
    '        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    '            filePath = sfd.FileName
    '        Else
    '            Exit Sub
    '        End If
    '        Dim filecount As Integer = ExportCSVMultipleFile(gv4, filePath, True)
    '        If filecount <= 1 Then
    '            clsCommon.MyMessageBoxShow("Data Exported successfully")
    '            Process.Start(filePath)
    '        Else
    '            clsCommon.MyMessageBoxShow("Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files")
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    'Private Sub VYPDF_Click(sender As Object, e As EventArgs)
    '    Try
    '        If gv4.Rows.Count > 0 Then
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
    '            arrHeader.Add("Name : " & "Vehicle Utilization")
    '            arrHeader.Add("Period : " + clsCommon.myCstr(ddlProvMonth.Text) + " - " + clsCommon.myCstr(Year(dtpProvYear.Value)) + "")
    '            clsCommon.MyExportToPDF("Vehicle Utilization", gv4, arrHeader, "Vehicle Utilization", PageSetupReport_ID, objCommonVar.CurrentUserCode)
    '        Else
    '            common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)
    '    End Try
    'End Sub

    Private Sub PMPExcel_Click(sender As Object, e As EventArgs) Handles PMPExcel.Click
        Try
            If gv_Procurement.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Procurement Milk Purchase")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthProcurement.Text) + " - " + clsCommon.myCstr(Year(dtpYearProcurement.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Procurement.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Procurement.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(gv_Procurement, "", "Procurement Milk Purchase", , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PMPCSV_Click(sender As Object, e As EventArgs) Handles PMPCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Procurement Milk Purchase"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv_Procurement, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub PMPPDF_Click(sender As Object, e As EventArgs) Handles PMPPDF.Click
        Try
            If gv_Procurement.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Procurement Milk Purchase")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthProcurement.Text) + " - " + clsCommon.myCstr(Year(dtpYearProcurement.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Procurement.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Procurement.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Procurement"
                clsCommon.MyExportToPDF("Procurement Milk Purchase", gv_Procurement, arrHeader, "Procurement Milk Purchase", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub MRExcel_Click(sender As Object, e As EventArgs) Handles MRExcel.Click
        Try
            If gv_MilkReceived.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Milk Received at Factory")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthMilkRec.Text) + " - " + clsCommon.myCstr(Year(dtpYearMilkRec.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_MilkRec.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_MilkRec.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(gv_MilkReceived, "", "Milk Received at Factory", , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MRCSV_Click(sender As Object, e As EventArgs) Handles MRCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Milk Received at Factory"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv_MilkReceived, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub MRPDF_Click(sender As Object, e As EventArgs) Handles MRPDF.Click
        Try
            If gv_MilkReceived.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Milk Received at Factory")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthMilkRec.Text) + " - " + clsCommon.myCstr(Year(dtpYearMilkRec.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_MilkRec.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_MilkRec.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "MilkReceived"
                clsCommon.MyExportToPDF("Milk Received at Factory", gv_MilkReceived, arrHeader, "Milk Received at Factory", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnGo_MilkReceived_Click(sender As Object, e As EventArgs) Handles btnGo_MilkReceived.Click
        MilkReceivedAtFactory()
    End Sub


    Private Sub btnGo_MilkSale_Click(sender As Object, e As EventArgs) Handles btnGo_MilkSale.Click
        LoadMilkSale()
    End Sub

    Sub FormatGrid(ByRef GridName As RadGridView)

        GridName.Columns("Quantity In Ltr").HeaderText = "Sales"
        GridName.Columns("Scheme Quantity In Ltr").HeaderText = "Scheme"
        GridName.Columns("Sample Quantity In Ltr").HeaderText = "Sample"

        GridName.Columns("Quantity In Kg").HeaderText = "Sales"
        GridName.Columns("Scheme Quantity In Kg").HeaderText = "Scheme"
        GridName.Columns("Sample Quantity In Kg").HeaderText = "Sample"

        GridName.Columns("FAT KG").HeaderText = "Sales"
        GridName.Columns("Scheme FAT KG").HeaderText = "Scheme"
        GridName.Columns("Sample FAT KG").HeaderText = "Sample"

        GridName.Columns("SNF KG").HeaderText = "Sales"
        GridName.Columns("Scheme SNF KG").HeaderText = "Scheme"
        GridName.Columns("Sample SNF KG").HeaderText = "Sample"

        GridName.Columns("Sale Amount").HeaderText = "Sales"
        GridName.Columns("Scheme Sale Amount").HeaderText = "Scheme"
        GridName.Columns("Sample Sale Amount").HeaderText = "Sample"

        GridName.Columns("Ave Realisa Per Ltr").HeaderText = "Sales"
        GridName.Columns("Scheme Ave Realisa Per Ltr").HeaderText = "Scheme"
        GridName.Columns("Sample Ave Realisa Per Ltr").HeaderText = "Sample"

        GridName.Columns("Ave Realisa Per Kg").HeaderText = "Sales"
        GridName.Columns("Scheme Ave Realisa Per Kg").HeaderText = "Scheme"
        GridName.Columns("Sample Ave Realisa Per Kg").HeaderText = "Sample"

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

            view.ColumnGroups.Add(New GridViewColumnGroup("Quantity In Kg"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Quantity In Kg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(GridName.Columns("Sample Quantity In Kg").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("FAT KG"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Scheme FAT KG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(GridName.Columns("Sample FAT KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("SNF KG"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Scheme SNF KG").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(GridName.Columns("Sample SNF KG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Sale Amount"))
            view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Scheme Sale Amount").Name)
            view.ColumnGroups(5).Rows(0).ColumnNames.Add(GridName.Columns("Sample Sale Amount").Name)

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

    Private Sub MSExcel_Click(sender As Object, e As EventArgs) Handles MSExcel.Click
        Try
            If gv_MilkSale.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Milk Sale")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthMilkSale.Text) + " - " + clsCommon.myCstr(Year(dtpYearMilkSale.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_MilkSale.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_MilkSale.Value, "dd/MM/yyyy"))
                'transportSql.QuickExportToExcel(gv_MilkSale, "", "Milk Sale", , arrHeader)
                transportSql.exportdata(gv_MilkSale, "", "Milk Sale", , arrHeader, False, False, True)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub MSCSV_Click(sender As Object, e As EventArgs) Handles MSCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Milk Sale"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv_MilkSale, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub MSPDF_Click(sender As Object, e As EventArgs) Handles MSPDF.Click
        Try
            If gv_MilkSale.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Milk Sale")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthMilkSale.Text) + " - " + clsCommon.myCstr(Year(dtpYearMilkSale.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_MilkSale.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_MilkSale.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "MilkSale"
                clsCommon.MyExportToPDF("Milk Sale", gv_MilkSale, arrHeader, "Milk Sale", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnGo_ProductSale_Click(sender As Object, e As EventArgs) Handles btnGo_ProductSale.Click
        LoadProductSale()
    End Sub

    Private Sub PSExcel_Click(sender As Object, e As EventArgs) Handles PSExcel.Click
        Try
            If gv_ProductSale.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Product Sale")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthProductSale.Text) + " - " + clsCommon.myCstr(Year(dtpYearProductSale.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_ProductSale.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_ProductSale.Value, "dd/MM/yyyy"))
                'transportSql.QuickExportToExcel(gv_ProductSale, "", "Product Sale", , arrHeader)
                transportSql.exportdata(gv_ProductSale, "", "Product Sale", , arrHeader, False, False, True)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub PSCSV_Click(sender As Object, e As EventArgs) Handles PSCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Product Sale"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv_ProductSale, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub PSPDF_Click(sender As Object, e As EventArgs) Handles PSPDF.Click
        Try
            If gv_ProductSale.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Product Sale")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlMonthProductSale.Text) + " - " + clsCommon.myCstr(Year(dtpYearProductSale.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_ProductSale.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_ProductSale.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "ProductSale"
                clsCommon.MyExportToPDF("Product Sale", gv_ProductSale, arrHeader, "Product Sale", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub btn_Go_Transport_cost_Click(sender As Object, e As EventArgs) Handles btn_Go_Transport_cost.Click
        LoadTransportCharges()
    End Sub

    Private Sub TCExcel_Click(sender As Object, e As EventArgs) Handles TCExcel.Click
        Try
            If gvTransportcost.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Transport Costing")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlTransportMonth.Text) + " - " + clsCommon.myCstr(Year(dtpTransportYear.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Transport.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Transport.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(gvTransportcost, "", "Transport Costing", , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TCCSV_Click(sender As Object, e As EventArgs) Handles TCCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Transport Costing"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gvTransportcost, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub TCPDF_Click(sender As Object, e As EventArgs) Handles TCPDF.Click
        Try
            If gvTransportcost.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Transport Costing")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlTransportMonth.Text) + " - " + clsCommon.myCstr(Year(dtpTransportYear.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Transport.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Transport.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Transport_cost"
                clsCommon.MyExportToPDF("Transport Costing", gvTransportcost, arrHeader, "Transport Costing", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub SRExcel_Click(sender As Object, e As EventArgs) Handles SRExcel.Click
        Try
            If gv_store.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Store Report")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlStoreMonth.Text) + " - " + clsCommon.myCstr(Year(dtpStoreYear.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Store.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Store.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(gv_store, "", "Store Report", , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SRCSV_Click(sender As Object, e As EventArgs) Handles SRCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Store Report"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv_store, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub SRPDF_Click(sender As Object, e As EventArgs) Handles SRPDF.Click
        Try
            If gv_store.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Store Report")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlStoreMonth.Text) + " - " + clsCommon.myCstr(Year(dtpStoreYear.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Store.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Store.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "StoreReport"
                clsCommon.MyExportToPDF("Store Report", gv_store, arrHeader, "Store Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub btn_StoreReport_Click(sender As Object, e As EventArgs) Handles btn_StoreReport.Click
        LoadStore()
    End Sub

    Private Sub POExcel_Click(sender As Object, e As EventArgs) Handles POExcel.Click
        Try
            If gv_po.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "PO Report")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlStoreMonth.Text) + " - " + clsCommon.myCstr(Year(dtpStoreYear.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Store.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Store.Value, "dd/MM/yyyy"))
                transportSql.QuickExportToExcel(gv_po, "", "PO Report", , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub POCSV_Click(sender As Object, e As EventArgs) Handles POCSV.Click
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "PO Report"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv_po, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub POPDF_Click(sender As Object, e As EventArgs) Handles POPDF.Click
        Try
            If gv_po.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "PO Report")
                'arrHeader.Add("Period : " + clsCommon.myCstr(ddlStoreMonth.Text) + " - " + clsCommon.myCstr(Year(dtpStoreYear.Value)) + "")
                arrHeader.Add("Date Range : " + clsCommon.GetPrintDate(dtpFromDate_Store.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate_Store.Value, "dd/MM/yyyy"))
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "StoreReport"
                clsCommon.MyExportToPDF("PO Report", gv_po, arrHeader, "PO Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Private Sub VUExcel_Click(sender As Object, e As EventArgs)
        Try
            If gv4.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Vehicle Utilization")
                arrHeader.Add("Period : " + clsCommon.myCstr(ddlProvMonth.Text) + " - " + clsCommon.myCstr(Year(dtpProvYear.Value)) + "")
                transportSql.QuickExportToExcel(gv4, "", "Vehicle Utilization", , arrHeader)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub VUCSV_Click(sender As Object, e As EventArgs)
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = "Vehicle Utilization"
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim filecount As Integer = ExportCSVMultipleFile(gv4, filePath, True)
            If filecount <= 1 Then
                clsCommon.MyMessageBoxShow(Me, "Data Exported successfully", Me.Text)
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Exported in directory -" & System.IO.Path.GetDirectoryName(filePath) & "\" & System.IO.Path.GetFileName(filePath) & " in " & filecount & " files", Me.Text)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub VUPDF_Click(sender As Object, e As EventArgs)
        Try
            If gv4.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & "Vehicle Utilization")
                arrHeader.Add("Period : " + clsCommon.myCstr(ddlProvMonth.Text) + " - " + clsCommon.myCstr(Year(dtpProvYear.Value)) + "")
                PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Vehicle_Utilization"
                clsCommon.MyExportToPDF("Vehicle Utilization", gv4, arrHeader, "Vehicle Utilization", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub BtnClose1_Click(sender As Object, e As EventArgs) Handles btnClose1.Click
        Me.Close()
    End Sub

    Private Sub Btn_Close2_Click(sender As Object, e As EventArgs) Handles Btn_Close2.Click
        Me.Close()
    End Sub

    Private Sub Btn_Close3_Click(sender As Object, e As EventArgs) Handles Btn_Close3.Click
        Me.Close()
    End Sub

    Private Sub Btn_Close4_Click(sender As Object, e As EventArgs) Handles Btn_Close4.Click
        Me.Close()
    End Sub

    Private Sub Btn_Close5_Click(sender As Object, e As EventArgs) Handles Btn_Close5.Click
        Me.Close()
    End Sub

    Private Sub Btn_Close6_Click(sender As Object, e As EventArgs) Handles Btn_Close6.Click
        Me.Close()
    End Sub

    Private Sub Btn_Close7_Click(sender As Object, e As EventArgs) Handles Btn_Close7.Click
        Me.Close()
    End Sub

    Private Sub Btn_Close8_Click(sender As Object, e As EventArgs) Handles Btn_Close8.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ii As Integer = 0
        Dim Total As Integer = 10
        clsCommon.ProgressBarPercentShow()
        Try

            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Bank Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            Loadbankdata()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Vehicle Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadVehicle()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Procuremnt Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadProcuremntData()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Received At Factory Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            MilkReceivedAtFactory()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Milk Sale Data... " & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadMilkSale()

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Product Sale Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadProductSale()
            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Transport Charges Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadTransportCharges()
            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading Store Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadStore()
            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading FG Mass Balance Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadMassBalance(False, gvFGMassBalance)

            ii += 1
            clsCommon.ProgressBarPercentUpdate(((ii) * 100 / (Total)), "Loading SNF MassBalance Data..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(Total) & "")
            LoadMassBalance(True, gvSFGMassBalance)

            clsCommon.ProgressBarPercentHide()
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Loadbankdata()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Bank_Cash_Book"
            RadChartView1.Series.Clear()
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
                gv3.DataSource = Nothing
                gv3.Rows.Clear()
                gv3.Columns.Clear()
                gv3.DataSource = dt
                gv3.GroupDescriptors.Clear()
                gv3.MasterTemplate.BestFitColumns()
                gv3.EnableFiltering = True
                gv3.Columns("BalAmt_Chart").IsVisible = False
                gv3.Columns("Credit_Amount_Chart").IsVisible = False
                gv3.Columns("Debit_Amount_Chart").IsVisible = False
                gv3.Columns("Closing_Balance_Chart").IsVisible = False
                If False Then
                    Dim barSeries As New Telerik.WinControls.UI.BarSeries("BalAmt_Chart", "Bank Code")
                    barSeries.Name = "Opening Balance"

                    Me.RadChartView1.Series.Add(barSeries)
                    barSeries.DataSource = dt
                    barSeries.HorizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine
                    'barSeries.VerticalAxis.LabelInterval = 2

                    Dim barSeries2 As New Telerik.WinControls.UI.BarSeries("Credit_Amount_Chart", "Bank Code")
                    barSeries2.Name = "Payments Balance"
                    Me.RadChartView1.Series.Add(barSeries2)
                    barSeries2.DataSource = dt
                    barSeries2.HorizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine
                    'barSeries2.VerticalAxis.LabelInterval = 2

                    Dim barSeries3 As New Telerik.WinControls.UI.BarSeries("Debit_Amount_Chart", "Bank Code")
                    barSeries3.Name = "Receipts Balance"
                    Me.RadChartView1.Series.Add(barSeries3)
                    barSeries3.DataSource = dt
                    barSeries3.HorizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine
                    'barSeries3.VerticalAxis.LabelInterval = 2

                    Dim barSeries4 As New Telerik.WinControls.UI.BarSeries("Closing_Balance_Chart", "Bank Code")
                    barSeries4.Name = "Closing Balance"
                    Me.RadChartView1.Series.Add(barSeries4)
                    barSeries4.DataSource = dt
                    barSeries4.HorizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine

                    Me.RadChartView1.LegendTitle = "Value In " + clsCommon.myCstr(cboFigureInGraph_Bank_Cash_Book.Text)
                    Me.RadChartView1.ChartElement.LegendElement.Items(0).Title = "Opening"
                    Me.RadChartView1.ChartElement.LegendElement.Items(1).Title = "Payments"
                    Me.RadChartView1.ChartElement.LegendElement.Items(2).Title = "Receipts"
                    Me.RadChartView1.ChartElement.LegendElement.Items(3).Title = "Closing"
                End If
            Else
                'clsCommon.MyMessageBoxShow("Record Not Found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadVehicle()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Vehicle_Utilization"
            RadChartView2.Series.Clear()
            Dim StartDate As String = clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy")
            Dim EndDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")
            'StartDate = "01/" + clsCommon.myCstr(ddlProvMonth.SelectedValue) + "/" + clsCommon.myCstr(Year(dtpProvYear.Value))
            'EndDate = clsCommon.myCstr(ddlProvMonth.SelectedValue) + "/01/" + clsCommon.myCstr(Year(dtpProvYear.Value))
            'EndDate = clsDBFuncationality.getSingleValue("select EOMONTH('" + EndDate + "')")
            Dim Qry As String = ""
            Qry = "select ROW_NUMBER () over (order by Vehicle_Brand ) As SNo,XX.Vehicle_Brand as [Vehicle Type] " &
                 ",COUNT(DISTINCT XX.Vehicle_Id) as [No of Vehicles] " &
                 ",COUNT(DISTINCT XX.Route_No) as [No of Route Operated] " &
                 ",sum(XX.[Running KM]) as [No of KM Running Per Month] " &
                 ",(case when sum(XX.[Running KM])>0 then round(sum(XX.[Freight Amount])/sum(XX.[Running KM]),2) else 0 end) as [Rate Per KM] " &
                 ",MAX(XX.[Vehicle Capacity])  as [Capacity of Vehicle]  " &
                 ",round(sum(XX.[Vehicle Out Crate])/count(DISTINCT XX.[Gate Pass No]),2) as [Capacity Utilised]  " &
                 ",round(sum(XX.[Freight Amount]),2) as [Freight Cost] " &
                 ",round(sum(XX.[Sales Milk/Ltr]),2) as [Sales In LTR] " &
                 ",round(sum(XX.[Freight Amount])/sum(XX.[Sales Milk/Ltr]),2) as [CPL] from " &
                 "(select ISNULL(TSPL_VEHICLE_MASTER.Vehicle_Brand,'') as Vehicle_Brand " &
                 ",ISNULL(TSPL_VEHICLE_MASTER.Vehicle_Id,'') as Vehicle_Id " &
                 ",ISNULL(TSPL_ROUTE_MASTER.Route_No,'') AS Route_No " &
                 ",TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode as [Gate Pass No]  " &
                 ",(case when isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0)>0 then isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0) " &
                 " else 0 end) as [Running KM] " &
                 ",TSPL_PROVISION_ENTRY.Amount as [Freight Amount] " &
                 ", TBL_LTR_CONV.Ltr_Qty as [Sales Milk/Ltr], TSPL_VEHICLE_MASTER.CrateCapacity as [Vehicle Capacity]  " &
                 ",TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate as [Vehicle Out Crate] " &
                 " from  " &
                 " TSPL_DAIRYSALE_GATEPASS_MASTER left join TSPL_PROVISION_ENTRY on TSPL_PROVISION_ENTRY.Ref_Doc_No = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode " &
                 "left Outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_id = TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id   " &
                 "left Outer Join TSPL_ROUTE_MASTER on  TSPL_ROUTE_MASTER.Route_No = TSPL_PROVISION_ENTRY.Route_Code  " &
                 "left outer  join  ( " &
                 "select TSPL_DAIRYSALE_GATEPASS_Detail.GPCode ,sum (convert(decimal(18,2),(TSPL_DAIRYSALE_GATEPASS_Detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)) as Ltr_Qty from TSPL_DAIRYSALE_GATEPASS_MASTER  left join TSPL_DAIRYSALE_GATEPASS_Detail on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_Detail.GPCode  left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code  left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code  " &
                "left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code  left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code and 	CurrentUnit.uom_code=	TSPL_DAIRYSALE_GATEPASS_Detail.unit_code  where  tspl_unit_master.Ltr_type ='Y' and StockUnit.stocking_unit='Y'  " &
                "group by TSPL_DAIRYSALE_GATEPASS_Detail.GPCode ) TBL_LTR_CONV on  TBL_LTR_CONV.GPCode = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode  where 2=2   "
            Qry += " and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) >= convert(date,('" + StartDate + "'),103) and convert(date,TSPL_DAIRYSALE_GATEPASS_MASTER.GPDate,103) <= convert(date,('" + EndDate + "'),103) "
            Qry += ")XX group by Vehicle_Brand "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv4.DataSource = Nothing
                gv4.Rows.Clear()
                gv4.Columns.Clear()
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
                If False Then
                    Dim barSeries As New Telerik.WinControls.UI.BarSeries("CPL", "Vehicle Type")
                    barSeries.BackColor = Color.Olive
                    Me.RadChartView2.Series.Add(barSeries)
                    barSeries.DataSource = dt
                    'barSeries.HorizontalAxis.LabelFitMode = AxisLabelFitMode.Rotate
                    'barSeries.VerticalAxis.LabelInterval = 2
                    Me.RadChartView2.LegendTitle = "CPL"
                End If
            Else
                'clsCommon.MyMessageBoxShow("Record Not Found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadProcuremntData()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Procurement"
            RadChartViewProcurement.Series.Clear()
            Dim Qry As String = ""
            Qry = "SELECT MAIN.[MCC Name],MAIN.[No of VLC],MAIN.[No of Route],MP.MP_Count AS [No of Farmer],Cast(MAIN.[Qty In LTR] as decimal(18,2)) as [Qty In LTR],Cast(MAIN.[Qty In KG] as decimal(18,2)) as [Qty In KG],MAIN.[Milk Payment] " &
            ",Cast(MAIN.[FAT %] as decimal(18,2)) as [FAT %],Cast(MAIN.[SNF %] as decimal(18,2)) as [SNF %],MAIN.[FAT KG],MAIN.[SNF KG],Cast(MAIN.Rate as decimal(18,2)) as Rate,INC.Incentive_Amount as [Incentive Amount],INC.Rent_Amount as [Rent Amount]" &
            ",Cast(PROV.[Freight Cost] as decimal(18,2)) as [Freight Cost],convert(decimal(18,2),PROV.[Freight Cost]/MAIN.[Qty In LTR]) AS CPL,'' AS [Prodcurement depart Salary] " &
            ",'' as [Field Staff Fuel],'' as [Emp CPL] FROM "
            Qry += " (select XX.MCC_Code,XX.MCC_Name as [MCC Name],COUNT(DISTINCT XX.VLC_Code) as [No of VLC] ,COUNT(DISTINCT XX.Route_Code) as [No of Route] " &
            ",ROUND(SUM(XX.Qty_LTR),2) as [Qty In LTR],ROUND(SUM(XX.Qty_KG),2) as [Qty In KG] ,SUM(XX.AMOUNT) as [Milk Payment] " &
            ", ROUND((sum(XX.FAT_KG )/sum(Qty_KG))*100,2) as [FAT %],ROUND((sum(XX.SNF_KG)/sum(Qty_KG ))*100,2)  as [SNF %] " &
            ",sum(FAT_KG) as [FAT KG],sum(SNF_KG) as [SNF KG],round(SUM(XX.AMOUNT)/SUM(XX.Qty),2) as [Rate]  from ( " &
            "select TSPL_MILK_RECEIPT_HEAD.MCC_CODE , TSPL_MCC_MASTER.MCC_NAME , TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_MCC_ROUTE_MASTER.Route_Code " &
             ", TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT_LTR as Qty_LTR,TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT As Qty_KG " &
             ",Convert(decimal(18,2), TSPL_MILK_SAMPLE_DETAIL.FAT * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As FAT_KG, Convert(decimal(18,2),TSPL_MILK_SAMPLE_DETAIL.SNF * TSPL_MILK_RECEIPT_DETAIL.ACC_WEIGHT / 100) As SNF_KG, TSPL_MILK_SRN_DETAIL.Qty As Qty,Convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.AMOUNT) As AMOUNT " &
             " From TSPL_MILK_RECEIPT_DETAIL Left Outer Join TSPL_MILK_RECEIPT_HEAD On TSPL_MILK_RECEIPT_HEAD.DOC_CODE = TSPL_MILK_RECEIPT_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_HEAD On TSPL_MILK_SAMPLE_HEAD.MILK_RECEIPT_CODE = TSPL_MILK_RECEIPT_HEAD.DOC_CODE Left Outer Join TSPL_MILK_SAMPLE_DETAIL On TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO = TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO And TSPL_MILK_SAMPLE_DETAIL.DOC_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE  Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.MILK_SAMPLE_CODE = TSPL_MILK_SAMPLE_HEAD.DOC_CODE And TSPL_MILK_SRN_HEAD.SAMPLE_NO = TSPL_MILK_SAMPLE_DETAIL.SAMPLE_NO Left Outer Join TSPL_MILK_SRN_DETAIL On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_DETAIL On TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE = TSPL_MILK_SRN_HEAD.DOC_CODE Left Outer Join TSPL_MILK_PURCHASE_INVOICE_HEAD On TSPL_MILK_PURCHASE_INVOICE_HEAD.DOC_CODE = TSPL_MILK_PURCHASE_INVOICE_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_RECEIPT_HEAD.MCC_CODE Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_RECEIPT_DETAIL.VLC_CODE Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MILK_RECEIPT_DETAIL.VSP_CODE Left Outer Join TSPL_MCC_ROUTE_MASTER On TSPL_MCC_ROUTE_MASTER.Route_Code = TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1 on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  Left Outer Join TSPL_MILK_Shift_End_HEAD On TSPL_MILK_Shift_End_HEAD.MCC_CODE = TSPL_MILK_RECEIPT_HEAD.MCC_CODE  And convert(date,TSPL_MILK_Shift_End_HEAD.DOC_DATE,103) = convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)  And TSPL_MILK_Shift_End_HEAD.SHIFT = TSPL_MILK_RECEIPT_HEAD.SHIFT  Left Outer Join TSPL_MILK_Shift_End_Route_DETAIL On TSPL_MILK_Shift_End_Route_DETAIL.DOC_CODE = TSPL_MILK_Shift_End_HEAD.DOC_CODE  And TSPL_MILK_Shift_End_Route_DETAIL.Route_CODE = TSPL_MCC_ROUTE_MASTER.Route_Code where 2 = 2  "
            Qry += " and convert(date, TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " )XX group by XX.MCC_Code,MCC_Name " &
            ")MAIN " &
            " left join  (SELECT COUNT(DISTINCT MP.MP_Code) as MP_Count,MP.MCC_CODE FROM (select TSPL_Mp_MASTER.MP_Code,TSPL_MP_MASTER.MCC as MCC_CODE from (select TSPL_Mp_MASTER.MP_Code ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,VLC_Code_VLC_Uploader,MP_Code_VLC_Uploader,Mp_Name,TSPL_MP_MASTER.AccountNO,TSPL_MP_MASTER.BankBranch " &
            " ,TSPL_MP_MASTER.BankName,TSPL_MP_MASTER.IFCIcode ,TSPL_VLC_MASTER_HEAD.MCC, MCC_NAME,UOM_Code,TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MP_MASTER.PayeeName,TSPL_MP_MASTER.Education   " &
            "  from TSPL_VLC_MASTER_HEAD   left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_VLC_MASTER_HEAD.MCC  " &
            "  Left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE =TSPL_MCC_MASTER.MCC_Code and Stocking_Unit ='Y'  " &
            "  Left join TSPL_MP_MASTER on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code  left join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_BRANCH_MASTER.BRANCH_CODE =TSPL_MP_MASTER.BankBranch  left join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE =TSPL_MP_MASTER.BankName) TSPL_MP_MASTER  " &
            "  left join TSPL_VLC_DATA_UPLOADER on TSPL_MP_MASTER.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE  " &
            "  and TSPL_MP_MASTER.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_MP_MASTER.MCC =TSPL_VLC_DATA_UPLOADER.MCC_Code  " &
            " left join TSPL_MCC_ROUTE_MASTER RT on TSPL_MP_MASTER.Route_Code=RT.Route_Code  where 2=2  " &
            "  and convert(date,File_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,File_Date,103) <=convert(date,'" + txtToDate.Value + "',103) and qty >0  " &
            "  union all  " &
            "  select TSPL_MP_MASTER.mp_code,tspl_mcc_master.mcc_code " &
            "  from TSPL_VLC_DATA_UPLOADER_DETAIL VDUD  inner join TSPL_VLC_DATA_UPLOADER_MASTER VDUM on VDUD.Document_Code=VDUM.Document_Code  " &
            "  left join TSPL_VLC_MASTER_HEAD VLCM on VDUM.VLC_Code=VLCM.VLC_Code  left join tspl_mcc_master on tspl_mcc_master.mcc_code=VLCM.MCC  " &
            "  left join tspl_mp_master on tspl_mp_master.mp_code=VDUD.farmer_code left join tspl_mcc_route_master on tspl_mcc_route_master.Route_Code=VLCM.Route_Code  " &
            "  where 2 = 2  and  convert(date, VDUM.Document_Date,103) >= convert(date,'" + txtFromDate.Value + "',103) and  convert(date, VDUM.Document_Date,103) <= convert(date,'" + txtToDate.Value + "',103)  " &
            " ) MP group by  MCC_CODE )MP ON MP.MCC_Code=MAIN.MCC_Code " &
            "left join  (select TSPL_MCC_MASTER.MCC_Code,sum(TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Amount) as Incentive_Amount " &
            ",(sum(TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Rent_Amount)) as Rent_Amount " &
            " from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL " &
            " LEFT JOIN TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD ON TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code " &
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code " &
            " where 2=2 "
            Qry += " and convert(date, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month,103) <= convert(date,'" + txtToDate.Value + "',103) " &
            " group by TSPL_MCC_MASTER.MCC_Code " &
             ")INC ON INC.MCC_Code=MAIN.MCC_Code " &
            " left join (select TSPL_MCC_ROUTE_MASTER.MCC_Code,SUM(TSPL_PROVISION_ENTRY.Amount) AS [Freight Cost] from " &
            " TSPL_PROVISION_ENTRY left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_PROVISION_ENTRY.Route_Code " &
            " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1  " &
            " on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code   " &
            " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  " &
            " where TSPL_PROVISION_ENTRY.isposted=1 and TSPL_PROVISION_ENTRY.Prog_code='M-Shift_End' " &
            " and convert(date, TSPL_PROVISION_ENTRY.DOC_DATE,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_PROVISION_ENTRY.DOC_DATE,103) <= convert(date,'" + txtToDate.Value + "',103) " &
            " GROUP BY TSPL_MCC_ROUTE_MASTER.MCC_Code " &
          ")PROV ON PROV.MCC_Code=MAIN.MCC_Code "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            'Total Row
            Dim NoofVLC As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of VLC])", " [No of VLC] is not null"))
            Dim NoofRoute As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of Route])", " [No of Route] is not null"))
            Dim NoofFarmer As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of Farmer])", " [No of Farmer] is not null"))

            Dim FatPerAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "")), 2)
            Dim SNFPerAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "")), 2)
            Dim RateAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(Rate)", " Rate is not null")), 2)
            Dim CPLAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(CPL)", " CPL is not null")), 2)

            Dim QtyInLTRSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In LTR])", " [Qty In LTR] is not null")), 2)
            Dim QtyInKGSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In KG])", " [Qty In KG] is not null")), 2)
            Dim MilkPaymentSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Milk Payment])", " [Milk Payment] is not null")), 2)
            Dim FATKGSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([FAT KG])", " [FAT KG] is not null")), 2)
            Dim SNFKGSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([SNF KG])", " [SNF KG] is not null")), 2)
            Dim IncentiveAmountSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Incentive Amount])", " [Incentive Amount] is not null")), 2)
            Dim RentAmountSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Rent Amount])", " [Rent Amount] is not null")), 2)
            Dim FreightCostSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Freight Cost])", " [Freight Cost] is not null")), 2)


            dt.Rows.Add("Total", NoofVLC, NoofRoute, NoofFarmer, QtyInLTRSum, QtyInKGSum, MilkPaymentSum, FatPerAvg, SNFPerAvg, FATKGSum, SNFKGSum, RateAvg, IncentiveAmountSum, RentAmountSum, FreightCostSum, CPLAvg, DBNull.Value, DBNull.Value, DBNull.Value)


            Dim newBlankRow1 As DataRow = dt.NewRow
            dt.Rows.Add(newBlankRow1)


            Qry = " select yy.[To MCC or Plant Name]+ ' - ' +yy.[Vendor Name] as [MCC Name],NULL as [No of VLC],NULL as [No of Route],NULL as [No of Farmer] " &
                    ",ROUND(sum([Qty In LTR]),2) as [Qty In LTR],sum([Net Weight]) as [Qty In KG],round(sum(yy.[Total SRN Amount]),2) As [Milk Payment] " &
                    ",ROUND((sum(yy.FATKG )/sum([Net Weight]))*100,2) as [FAT %],ROUND((sum(yy.SNFKG)/sum([Net Weight]))*100,2)  as [SNF %]  " &
                    ",Cast(ROUND((sum(yy.FATKG )),2) as decimal(18,2)) as [FAT KG],Cast(ROUND((sum(yy.SNFKG )),2) as decimal(18,2)) as [SNF KG],round(sum(yy.[Total SRN Amount])/sum([Net Weight]),2) As [Rate] " &
                    ",NULL as [Incentive Amount],NULL as [Rent Amount],NULL as [Freight Cost],NULL AS CPL,NULL AS [Prodcurement depart Salary] ,NULL as [Field Staff Fuel],NULL as [Emp CPL] " &
                    " from " &
                     " (Select DISTINCT xx.[To MCC or Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [To MCC or Plant Name], xx.DocType,xx.[Vendor Code],xx.[Vendor Name],xx.[Challan No],xx.[Challan Date],xx.[SRN No],xx.[SRN Date],xx.[Invoice No],xx.[Invoice Date],xx.[Tanker No],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Weighment No],xx.[Weighment Date],xx.[Milk Receipt Challan No],xx.[Milk Receipt Challan Date],xx.[Challan Qty], xx.ChallanFatPer as [Challan Fat%],xx.ChallanSNFPer as [Challan SNF%],xx.ChallanFatKg as [Challan Fat KG],xx.ChallanSNFKg as [Challan SNF KG],(xx.ChallanFatKg+xx.ChallanSNFKg) as [Challan TS],xx.[Gross Weight],xx.[Tare Weight],xx.[Tare Date],xx.[Net Weight],xx.[From MCC or Plant Code],xx.[From MCC or Plant Name], xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No],XX.[Unloading Date Time], XX.[QC Date Time],XX.STATUS,xx.[Unloading No],xx.[MCC Name],xx.Plant,xx.[Silo Code],xx.[Silo Desc],xx.[Gate Out No],xx.[Gate Out Date Time],xx.[FAT %] ,xx.[SNF %] , xx.CLR,(xx.[Challan Qty]-xx.[Net Weight]) as [Differrence Qty],Convert (decimal(18,2),(xx.[ChallanFatPer]-xx.[FAT %])) as [Differrence FAT %],Convert (decimal(18,2),(xx.[ChallanSNFPer]-xx.[SNF %])) as [Differrence SNF %],Convert (decimal(18,2),(xx.ChallanFatKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)))) as [Differrence FAT kG],Convert (decimal(18,2),(ChallanSNFKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)))) as [Differrence SNF KG]  ,xx.[Basic Rate], xx.incentive ,xx.[Special Deduction],xx.[Deduction] , xx.[Net Rate],xx.[FAT Rate], xx.[SNF Rate],case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total SRN Amount],xx.[FAT Weightage & SNF Weightage],xx.[FAT Ratio & SNF ratio],xx.[Vendor Class] , case when SRN_Return_NO is not Null then 'SRN Return' else '' end as [SRN Return], Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG, Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG,[FAT Amt],[SNF Amt],[Standard Rate] ,[Net Weight]/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS [Qty In LTR] " &
                " From (  " &
                " Select Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.fat_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.fat_per, 0) end  as ChallanFatPer,Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) end as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per*  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100 as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, " &
            "Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then Tspl_Gate_Entry_Details.Qty_In_Kg else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end END  As [Challan Qty]" &
             ",  TSPL_Weighment_Detail.Gross_Weight As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] " &
         ", Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR" &
          ", TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt) Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt) Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount Else TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From   (select Tspl_Gate_Entry_Details.Gate_Entry_No , Tspl_Gate_Entry_Details.Doc_type ,Tspl_Gate_Entry_Details.Date_And_Time, Tspl_Gate_Entry_Details.Challan_No, Tspl_Gate_Entry_Details.Challan_Date,Tspl_Gate_Entry_Details.Tanker_No, Tspl_Gate_Entry_Details.isPosted, Posting_Date,Dispatched_From_Mcc,location_Code,Location_Desc ,Vendor_Code,Vendor_Desc,Tspl_Gate_Entry_Details.Item_Code , Tspl_Gate_Entry_Details.Item_Desc, case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end  Qty_In_Kg  , case when TSPL_Gate_Entry_Chember_Details.snf_Per > 0 then TSPL_Gate_Entry_Chember_Details.snf_Per else Tspl_Gate_Entry_Details.snf_Per end as snf_Per ,case when TSPL_Gate_Entry_Chember_Details.fat_per > 0 then TSPL_Gate_Entry_Chember_Details.fat_per else  Tspl_Gate_Entry_Details.fat_per end as fat_per , Created_By ,Created_Date, Modify_By,Modify_Date,comp_code,Tspl_Gate_Entry_Details.UOM,Intimation_No , Supplier_Code,Dispatch_Centre_Code,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE,Intimation_Status,Gate_Entry_Type,Seal_Status, case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end as TotalQty_In_Kg ,SealNo_Header,Tanker_Return,PO_No,Amendment_Code,Amendment_By,Amendment_Date,IsAgainstJobWork,Sublocation_Code,In_Return,Transpoter_Id,Bulk_ROUTE_NO,Distance,Rate,Amount,ProvisionNo,NO_OF_CHAMBER,No_Of_CAN,Weight from " &
           " Tspl_Gate_Entry_Details  left outer join TSPL_Gate_Entry_Chember_Details on Tspl_Gate_Entry_Details.Gate_Entry_No = TSPL_Gate_Entry_Chember_Details.GE_Code) as Tspl_Gate_Entry_Details  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  " &
           " Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No   " &
           " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT  On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
          " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
          " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
           " where 2 = 2"
            Qry += " and convert(date, Tspl_Gate_Entry_Details.Date_And_Time,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, Tspl_Gate_Entry_Details.Date_And_Time,103) <= convert(date,'" + txtToDate.Value + "',103) "

            Qry += " union all  Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty],  TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG)*-1 End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG)*-1 End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount*-1 Else TSPL_MCC_Dispatch_Challan.Amount*-1 End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From  TSPL_MILK_TRANSFER_IN_RETURN LEFT OUTER JOIN  TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Receipt_Challan_No = TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  LEFT OUTER JOIN Tspl_Gate_Entry_Details ON  Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                    " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                    " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
            " where 2 = 2 "
            Qry += " and convert(date, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " union all  Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, 'Purchase Return'  As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No As [Invoice No],  Convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, (-1)* Tspl_Gate_Entry_Details.Qty_In_Kg As [Challan Qty],  (-1)* TSPL_Weighment_Detail.Gross_Weight As [Gross Weight], (-1)* TSPL_Weighment_Detail.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date], (-1)* TSPL_Weighment_Detail.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %] , Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %] , Convert(decimal(18,2), t_CLR.Param_Field_Value) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  (-1)* TSPL_Bulk_MILK_SRN.Incentive as Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.FatAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.SnfAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then (-1)* TSPL_Bulk_MILK_SRN.Actual_Amount Else (-1)*TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD On TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_No  = tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL  On TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No = TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No   Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT " &
           " On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No " &
           " where 2=2 and TSPL_Bulk_MILK_SRN.isposted=1 and isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No,'') <>''  "
            Qry += " and convert(date, TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += "  UNION ALL Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG , TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType,  'Not Req'  As [Milk Receipt Challan No],  '' As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty],  TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time] ,  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], TSPL_Bulk_MILK_SRN.BasicRate As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) As [FAT Rate],   Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) As [SNF Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1  As [FAT Amt],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1  As [SNF Amt],  TSPL_Bulk_MILK_SRN.Actual_Amount*-1 As [Total Amount Temp], 'For ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage)  + ' & ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage)  As 'FAT Weightage & SNF Weightage', 'For ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage)  + ' & ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)  As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From  " &
              " Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
              " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  " &
              " Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  " &
              " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  " &
              " Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code   " &
              " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
              " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO   " &
              " Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  " &
              " Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
              " Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO   " &
              " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
             " where 2=2   "
            Qry += " and convert(date, TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += "  ) As xx left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.[To MCC or Plant Code] " &
              " Left Outer Join TSPL_ITEM_UOM_DETAIL  On TSPL_ITEM_UOM_DETAIL.Item_Code = xx.[Item Code] And TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' " &
              " where  isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' and  isnull(TSPL_LOCATION_MASTER.Is_Jobwork,0) =0 and xx.DocType in('Bulk In','Bulk Ret')" &
             " ) as yy  GROUP BY yy.[To MCC or Plant Name]+ ' - ' +yy.[Vendor Name] "

            Dim dtBulk As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dtBulk IsNot Nothing And dtBulk.Rows.Count > 0 Then
                dt.Merge(dtBulk, True, MissingSchemaAction.Ignore)

                Dim FatPerAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim SNFPerAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim RateAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("AVG(Rate)", " Rate is not null")), 2)
                Dim CPLAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("AVG(CPL)", " CPL is not null")), 2)

                Dim QtyInLTRSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Qty In LTR])", " [Qty In LTR] is not null")), 2)
                Dim QtyInKGSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Qty In KG])", " [Qty In KG] is not null")), 2)
                Dim MilkPaymentSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Milk Payment])", " [Milk Payment] is not null")), 2)
                Dim FATKGSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([FAT KG])", " [FAT KG] is not null")), 2)
                Dim SNFKGSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([SNF KG])", " [SNF KG] is not null")), 2)
                Dim IncentiveAmountSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Incentive Amount])", " [Incentive Amount] is not null")), 2)
                Dim RentAmountSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Rent Amount])", " [Rent Amount] is not null")), 2)
                Dim FreightCostSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Freight Cost])", " [Freight Cost] is not null")), 2)


                dt.Rows.Add("Total", DBNull.Value, DBNull.Value, DBNull.Value, QtyInLTRSum1, QtyInKGSum1, MilkPaymentSum1, FatPerAvg1, SNFPerAvg1, FATKGSum1, SNFKGSum1, RateAvg1, IncentiveAmountSum1, RentAmountSum1, FreightCostSum1, CPLAvg1, DBNull.Value, DBNull.Value, DBNull.Value)

                'Sub Total
                Dim NoofVLCSub As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of VLC])", "[MCC Name]='Total'"))
                Dim NoofRouteSub As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of Route])", "[MCC Name]='Total'"))
                Dim NoofFarmerSub As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of Farmer])", "[MCC Name]='Total'"))

                Dim FatPerAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
                Dim SNFPerAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
                Dim RateAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(Rate)", "[MCC Name]='Total'")), 2)
                Dim CPLAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(CPL)", "[MCC Name]='Total'")), 2)

                Dim QtyInLTRSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In LTR])", "[MCC Name]='Total'")), 2)
                Dim QtyInKGSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
                Dim MilkPaymentSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Milk Payment])", "[MCC Name]='Total'")), 2)
                Dim FATKGSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([FAT KG])", "[MCC Name]='Total'")), 2)
                Dim SNFKGSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([SNF KG])", "[MCC Name]='Total'")), 2)
                Dim IncentiveAmountSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Incentive Amount])", "[MCC Name]='Total'")), 2)
                Dim RentAmountSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Rent Amount])", "[MCC Name]='Total'")), 2)
                Dim FreightCostSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Freight Cost])", "[MCC Name]='Total'")), 2)

                dt.Rows.Add("Sub Total", NoofVLCSub, NoofRouteSub, NoofFarmerSub, QtyInLTRSumSub, QtyInKGSumSub, MilkPaymentSumSub, FatPerAvgSub, SNFPerAvgSub, FATKGSumSub, SNFKGSumSub, RateAvgSub, IncentiveAmountSumSub, RentAmountSumSub, FreightCostSumSub, CPLAvgSub, DBNull.Value, DBNull.Value, DBNull.Value)

            End If
            'Job Work
            Qry = "select TSPL_LOCATION_MASTER.Location_Desc as [MCC Name],NULL as [No of VLC],NULL as [No of Route],NULL as [No of Farmer] " &
             " ,Cast(ROUND(sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight/TSPL_ITEM_UOM_DETAIL.Conversion_Factor),2) as decimal(18,2)) as [Qty In LTR] " &
             " ,ROUND(sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight),2) as [Qty In KG] " &
             " ,NULL As [Milk Payment],Cast(ROUND((sum(xx.FAT_KG)/sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight))*100,2) as decimal(18,2)) as [FAT %] " &
             " ,Cast(ROUND((sum(xx.SNF_KG)/sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight))*100,2) as decimal(18,2))  as [SNF %] ,sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG] " &
              " ,NULL As [Rate],NULL as [Incentive Amount],NULL as [Rent Amount],NULL as [Freight Cost],NULL AS CPL " &
              " ,NULL AS [Prodcurement depart Salary] ,NULL as [Field Staff Fuel],NULL as [Emp CPL] " &
             " from TSPL_GENERAL_WEIGHMENT_DETAIL   left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code   left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code   left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor   left join  " &
             " (select TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG ,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG " &
             " ,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no where TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No   " &
             " Left Outer Join TSPL_ITEM_UOM_DETAIL  On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code " &
             " And TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' " &
            "  where " &
            " TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork = 1 And TSPL_GENERAL_WEIGHMENT_DETAIL.Posted = 1   "
            Qry += " and convert(date, TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) " &
            " group by TSPL_LOCATION_MASTER.Location_Desc "

            Dim dtJobWork As DataTable = clsDBFuncationality.GetDataTable(Qry)



            If dtJobWork IsNot Nothing And dtJobWork.Rows.Count > 0 Then
                Dim newBlankRow2 As DataRow = dt.NewRow
                dt.Rows.Add(newBlankRow2)
                dt.Merge(dtJobWork, True, MissingSchemaAction.Ignore)
                Dim FatPerAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim SNFPerAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim RateAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("AVG(Rate)", " Rate is not null")), 2)
                Dim CPLAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("AVG(CPL)", " CPL is not null")), 2)

                Dim QtyInLTRSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Qty In LTR])", " [Qty In LTR] is not null")), 2)
                Dim QtyInKGSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Qty In KG])", " [Qty In KG] is not null")), 2)
                Dim MilkPaymentSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Milk Payment])", " [Milk Payment] is not null")), 2)
                Dim FATKGSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([FAT KG])", " [FAT KG] is not null")), 2)
                Dim SNFKGSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([SNF KG])", " [SNF KG] is not null")), 2)
                Dim IncentiveAmountSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Incentive Amount])", " [Incentive Amount] is not null")), 2)
                Dim RentAmountSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Rent Amount])", " [Rent Amount] is not null")), 2)
                Dim FreightCostSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Freight Cost])", " [Freight Cost] is not null")), 2)


                dt.Rows.Add("Total", DBNull.Value, DBNull.Value, DBNull.Value, QtyInLTRSum3, QtyInKGSum3, MilkPaymentSum3, FatPerAvg3, SNFPerAvg3, FATKGSum3, SNFKGSum3, RateAvg3, IncentiveAmountSum3, RentAmountSum3, FreightCostSum3, CPLAvg3, DBNull.Value, DBNull.Value, DBNull.Value)
            End If

            'Grand Total Row
            Dim NoofVLC2 As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of VLC])", "[MCC Name]='Total'"))
            Dim NoofRoute2 As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of Route])", "[MCC Name]='Total'"))
            Dim NoofFarmer2 As Decimal = clsCommon.myCdbl(dt.Compute("SUM([No of Farmer])", "[MCC Name]='Total'"))

            Dim FatPerAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
            Dim SNFPerAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
            Dim RateAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(Rate)", "[MCC Name]='Total'")), 2)
            Dim CPLAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(CPL)", "[MCC Name]='Total'")), 2)

            Dim QtyInLTRSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In LTR])", "[MCC Name]='Total'")), 2)
            Dim QtyInKGSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
            Dim MilkPaymentSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Milk Payment])", "[MCC Name]='Total'")), 2)
            Dim FATKGSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([FAT KG])", "[MCC Name]='Total'")), 2)
            Dim SNFKGSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([SNF KG])", "[MCC Name]='Total'")), 2)
            Dim IncentiveAmountSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Incentive Amount])", "[MCC Name]='Total'")), 2)
            Dim RentAmountSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Rent Amount])", "[MCC Name]='Total'")), 2)
            Dim FreightCostSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Freight Cost])", "[MCC Name]='Total'")), 2)

            dt.Rows.Add("Grand Total", NoofVLC2, NoofRoute2, NoofFarmer2, QtyInLTRSum2, QtyInKGSum2, MilkPaymentSum2, FatPerAvg2, SNFPerAvg2, FATKGSum2, SNFKGSum2, RateAvg2, IncentiveAmountSum2, RentAmountSum2, FreightCostSum2, CPLAvg2, DBNull.Value, DBNull.Value, DBNull.Value)


            gv_Procurement.DataSource = Nothing

            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_Procurement.Rows.Clear()
                gv_Procurement.Columns.Clear()
                gv_Procurement.GroupDescriptors.Clear()
                gv_Procurement.EnableGrouping = False
                gv_Procurement.MasterTemplate.SummaryRowsBottom.Clear()
                gv_Procurement.DataSource = dt
                gv_Procurement.MasterTemplate.BestFitColumns()
                gv_Procurement.EnableFiltering = True
                For i As Integer = 0 To gv_Procurement.Columns.Count - 1
                    gv_Procurement.Columns(i).BestFit()
                Next
                If False Then
                    Dim dr As DataRow() = dt.Select(" CPL is not null and CPL> 0 and [MCC Name]<>'Total' and [MCC Name]<>'Sub Total' and [MCC Name]<>'Grand Total'")
                    If dr IsNot Nothing AndAlso dr.Length > 0 Then
                        Dim dtchart As DataTable = dr.CopyToDataTable()
                        Dim barSeries As New Telerik.WinControls.UI.BarSeries("CPL", "MCC Name")
                        barSeries.BackColor = Color.Olive
                        Me.RadChartViewProcurement.Series.Add(barSeries)
                        barSeries.DataSource = dtchart
                        barSeries.HorizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine
                        Me.RadChartViewProcurement.LegendTitle = "CPL"
                    End If
                End If
            Else
                'clsCommon.MyMessageBoxShow("Record Not Found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub MilkReceivedAtFactory()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "MilkReceived"
            RadChartViewMilkReceived.Series.Clear()
            Dim Qry As String = ""
            Qry = "select zz.[MCC Name],zz.[Qty In LTR],zz.[Qty In KG],zz.[Milk Payment] ,zz.[FAT %],zz.[SNF %],zz.[FAT KG],zz.[SNF KG],zz.Rate " &
                ",Cast(INC.Incentive_Amount as decimal(18,2)) as [Incentive Amount],Cast(INC.Rent_Amount as decimal(18,2)) as [Rent Amount],Cast(PROV.[Freight Cost] as decimal(18,2)) as [Freight Cost],convert(decimal(18,2),PROV.[Freight Cost]/zz.[Qty In LTR]) AS CPL " &
                 ",NULL AS [Prodcurement depart Salary] ,NULL as [Field Staff Fuel],NULL as [Emp CPL] from ( " &
                " select  yy.[From MCC or Plant Name] as [MCC Name],yy.[From MCC or Plant Code] as [MCC Code] " &
                    ",ROUND(sum([Qty In LTR]),2) as [Qty In LTR],sum([Challan Qty]) as [Qty In KG],round(sum(yy.[Total SRN Amount]),2) As [Milk Payment] " &
                    ",ROUND((sum(yy.FATKG )/sum([Challan Qty]))*100,2) as [FAT %],ROUND((sum(yy.SNFKG)/sum([Challan Qty]))*100,2)  as [SNF %]  " &
                    ",ROUND((sum(yy.FATKG )),2) as [FAT KG],ROUND((sum(yy.SNFKG )),2) as [SNF KG],round(sum(yy.[Total SRN Amount])/sum([Challan Qty]),2) As [Rate] " &
                    ",NULL as [Incentive Amount],NULL as [Rent Amount],NULL as [Freight Cost],NULL AS CPL,NULL AS [Prodcurement depart Salary] ,NULL as [Field Staff Fuel],NULL as [Emp CPL] " &
                    " from " &
                     " (Select xx.DocType,xx.[Vendor Code],xx.[Vendor Name],xx.[Challan No],xx.[Challan Date],xx.[SRN No],xx.[SRN Date],xx.[Invoice No],xx.[Invoice Date],xx.[Tanker No],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Weighment No],xx.[Weighment Date],xx.[Milk Receipt Challan No],xx.[Milk Receipt Challan Date],xx.[Challan Qty], xx.ChallanFatPer as [Challan Fat%],xx.ChallanSNFPer as [Challan SNF%],xx.ChallanFatKg as [Challan Fat KG],xx.ChallanSNFKg as [Challan SNF KG],(xx.ChallanFatKg+xx.ChallanSNFKg) as [Challan TS],xx.[Gross Weight],xx.[Tare Weight],xx.[Tare Date],xx.[Net Weight],xx.[From MCC or Plant Code],xx.[From MCC or Plant Name], xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No],XX.[Unloading Date Time], XX.[QC Date Time],XX.STATUS,xx.[Unloading No],xx.[MCC Name],xx.Plant,xx.[Silo Code],xx.[Silo Desc],xx.[Gate Out No],xx.[Gate Out Date Time],xx.[FAT %] ,xx.[SNF %] , xx.CLR,(xx.[Challan Qty]-xx.[Net Weight]) as [Differrence Qty],Convert (decimal(18,2),(xx.[ChallanFatPer]-xx.[FAT %])) as [Differrence FAT %],Convert (decimal(18,2),(xx.[ChallanSNFPer]-xx.[SNF %])) as [Differrence SNF %],Convert (decimal(18,2),(xx.ChallanFatKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)))) as [Differrence FAT kG],Convert (decimal(18,2),(ChallanSNFKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)))) as [Differrence SNF KG]  ,xx.[Basic Rate], xx.incentive ,xx.[Special Deduction],xx.[Deduction] , xx.[Net Rate],xx.[FAT Rate], xx.[SNF Rate],case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total SRN Amount],xx.[FAT Weightage & SNF Weightage],xx.[FAT Ratio & SNF ratio],xx.[Vendor Class] , case when SRN_Return_NO is not Null then 'SRN Return' else '' end as [SRN Return], Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG, Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG,[FAT Amt],[SNF Amt],[Standard Rate] ,[Challan Qty]/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS [Qty In LTR] " &
                " From (  " &
                " Select Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.fat_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.fat_per, 0) end  as ChallanFatPer,Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) end as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per*  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100 as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, " &
            "Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then Tspl_Gate_Entry_Details.Qty_In_Kg else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end END  As [Challan Qty]" &
             ",  TSPL_Weighment_Detail.Gross_Weight As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] " &
         ", Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR" &
          ", TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt) Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt) Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount Else TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From   (select Tspl_Gate_Entry_Details.Gate_Entry_No , Tspl_Gate_Entry_Details.Doc_type ,Tspl_Gate_Entry_Details.Date_And_Time, Tspl_Gate_Entry_Details.Challan_No, Tspl_Gate_Entry_Details.Challan_Date,Tspl_Gate_Entry_Details.Tanker_No, Tspl_Gate_Entry_Details.isPosted, Posting_Date,Dispatched_From_Mcc,location_Code,Location_Desc ,Vendor_Code,Vendor_Desc,Tspl_Gate_Entry_Details.Item_Code , Tspl_Gate_Entry_Details.Item_Desc, case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end  Qty_In_Kg  , case when TSPL_Gate_Entry_Chember_Details.snf_Per > 0 then TSPL_Gate_Entry_Chember_Details.snf_Per else Tspl_Gate_Entry_Details.snf_Per end as snf_Per ,case when TSPL_Gate_Entry_Chember_Details.fat_per > 0 then TSPL_Gate_Entry_Chember_Details.fat_per else  Tspl_Gate_Entry_Details.fat_per end as fat_per , Created_By ,Created_Date, Modify_By,Modify_Date,comp_code,Tspl_Gate_Entry_Details.UOM,Intimation_No , Supplier_Code,Dispatch_Centre_Code,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE,Intimation_Status,Gate_Entry_Type,Seal_Status, case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end as TotalQty_In_Kg ,SealNo_Header,Tanker_Return,PO_No,Amendment_Code,Amendment_By,Amendment_Date,IsAgainstJobWork,Sublocation_Code,In_Return,Transpoter_Id,Bulk_ROUTE_NO,Distance,Rate,Amount,ProvisionNo,NO_OF_CHAMBER,No_Of_CAN,Weight from " &
           " Tspl_Gate_Entry_Details  left outer join TSPL_Gate_Entry_Chember_Details on Tspl_Gate_Entry_Details.Gate_Entry_No = TSPL_Gate_Entry_Chember_Details.GE_Code) as Tspl_Gate_Entry_Details  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  " &
           " Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No   " &
           " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT  On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
          " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
          " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
           " where 2 = 2 "
            Qry += " and convert(date, Tspl_Gate_Entry_Details.Date_And_Time,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, Tspl_Gate_Entry_Details.Date_And_Time,103) <= convert(date,'" + txtToDate.Value + "',103) "

            Qry += " union all  Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty],  TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG)*-1 End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG)*-1 End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount*-1 Else TSPL_MCC_Dispatch_Challan.Amount*-1 End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From  TSPL_MILK_TRANSFER_IN_RETURN LEFT OUTER JOIN  TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Receipt_Challan_No = TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  LEFT OUTER JOIN Tspl_Gate_Entry_Details ON  Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                    " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                    " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
            " where 2 = 2 "
            Qry += " and convert(date, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " union all  Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, 'Purchase Return'  As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No As [Invoice No],  Convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, (-1)* Tspl_Gate_Entry_Details.Qty_In_Kg As [Challan Qty],  (-1)* TSPL_Weighment_Detail.Gross_Weight As [Gross Weight], (-1)* TSPL_Weighment_Detail.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date], (-1)* TSPL_Weighment_Detail.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %] , Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %] , Convert(decimal(18,2), t_CLR.Param_Field_Value) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  (-1)* TSPL_Bulk_MILK_SRN.Incentive as Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.FatAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.SnfAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then (-1)* TSPL_Bulk_MILK_SRN.Actual_Amount Else (-1)*TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD On TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_No  = tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL  On TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No = TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No   Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT " &
           " On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No " &
           " where 2=2 and TSPL_Bulk_MILK_SRN.isposted=1 and isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No,'') <>''  "
            Qry += " and convert(date, TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "

            Qry += "  UNION ALL Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG , TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType,  'Not Req'  As [Milk Receipt Challan No],  '' As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty],  TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time] ,  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], TSPL_Bulk_MILK_SRN.BasicRate As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) As [FAT Rate],   Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) As [SNF Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1  As [FAT Amt],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1  As [SNF Amt],  TSPL_Bulk_MILK_SRN.Actual_Amount*-1 As [Total Amount Temp], 'For ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage)  + ' & ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage)  As 'FAT Weightage & SNF Weightage', 'For ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage)  + ' & ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)  As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From  " &
              " Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
              " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  " &
              " Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  " &
              " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  " &
              " Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code   " &
              " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
              " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO   " &
              " Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  " &
              " Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
              " Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO   " &
              " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
             " where 2=2   "
            Qry += " and convert(date, TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += "  ) As xx inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.[From MCC or Plant Code] " &
              " Left Outer Join TSPL_ITEM_UOM_DETAIL  On TSPL_ITEM_UOM_DETAIL.Item_Code = xx.[Item Code] And TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' " &
              " where  isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' and  isnull(TSPL_LOCATION_MASTER.Is_Jobwork,0) =0 " &
             " ) as yy  GROUP BY yy.[From MCC or Plant Code],yy.[From MCC or Plant Name] "
            Qry += " ) as zz " &
            "left join  (select TSPL_MCC_MASTER.MCC_Code,sum(TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Amount) as Incentive_Amount " &
            ",(sum(TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Rent_Amount)) as Rent_Amount " &
            " from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL " &
            " LEFT JOIN TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD ON TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code " &
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code "
            Qry += " where convert(date, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " group by TSPL_MCC_MASTER.MCC_Code " &
             ")INC ON INC.MCC_Code=zz.[MCC Code] " &
            " left join (select TSPL_MCC_ROUTE_MASTER.MCC_Code,SUM(TSPL_PROVISION_ENTRY.Amount) AS [Freight Cost] from " &
            " TSPL_PROVISION_ENTRY left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_PROVISION_ENTRY.Route_Code " &
            " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1  " &
            " on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code   " &
            " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  "
            Qry += " where TSPL_PROVISION_ENTRY.isposted=1 and TSPL_PROVISION_ENTRY.Prog_code='M-Shift_End' and convert(date, TSPL_PROVISION_ENTRY.Doc_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_PROVISION_ENTRY.Doc_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " GROUP BY TSPL_MCC_ROUTE_MASTER.MCC_Code " &
             ")PROV ON PROV.MCC_Code=zz.[MCC Code] "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim FatPerAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "")), 2)
            Dim SNFPerAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "")), 2)
            Dim RateAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(Rate)", " Rate is not null")), 2)
            Dim CPLAvg As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(CPL)", " CPL is not null")), 2)

            Dim QtyInLTRSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In LTR])", " [Qty In LTR] is not null")), 2)
            Dim QtyInKGSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In KG])", " [Qty In KG] is not null")), 2)
            Dim MilkPaymentSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Milk Payment])", " [Milk Payment] is not null")), 2)
            Dim FATKGSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([FAT KG])", " [FAT KG] is not null")), 2)
            Dim SNFKGSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([SNF KG])", " [SNF KG] is not null")), 2)
            Dim IncentiveAmountSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Incentive Amount])", " [Incentive Amount] is not null")), 2)
            Dim RentAmountSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Rent Amount])", " [Rent Amount] is not null")), 2)
            Dim FreightCostSum As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Freight Cost])", " [Freight Cost] is not null")), 2)


            dt.Rows.Add("Total", QtyInLTRSum, QtyInKGSum, MilkPaymentSum, FatPerAvg, SNFPerAvg, FATKGSum, SNFKGSum, RateAvg, IncentiveAmountSum, RentAmountSum, FreightCostSum, CPLAvg, DBNull.Value, DBNull.Value, DBNull.Value)

            Dim newBlankRow1 As DataRow = dt.NewRow
            dt.Rows.Add(newBlankRow1)


            Qry = "select zz.[MCC Name],zz.[Qty In LTR],zz.[Qty In KG],zz.[Milk Payment] ,zz.[FAT %],zz.[SNF %],zz.[FAT KG],zz.[SNF KG],zz.Rate " &
                ",Cast(INC.Incentive_Amount as decimal(18,2)) as [Incentive Amount],Cast(INC.Rent_Amount as decimal(18,2)) as [Rent Amount],Cast(PROV.[Freight Cost] as decimal(18,2)) as [Freight Cost],convert(decimal(18,2),PROV.[Freight Cost]/zz.[Qty In LTR]) AS CPL " &
                 ",NULL AS [Prodcurement depart Salary] ,NULL as [Field Staff Fuel],NULL as [Emp CPL] from ( " &
                " select  yy.[To MCC or Plant Name]+ ' - ' +yy.[Vendor Name] as [MCC Name],yy.[To MCC or Plant Code] as [MCC Code] " &
                    ",ROUND(sum([Qty In LTR]),2) as [Qty In LTR],sum([Net Weight]) as [Qty In KG],round(sum(yy.[Total SRN Amount]),2) As [Milk Payment] " &
                    ",ROUND((sum(yy.FATKG )/sum([Net Weight]))*100,2) as [FAT %],ROUND((sum(yy.SNFKG)/sum([Net Weight]))*100,2)  as [SNF %]  " &
                    ",ROUND((sum(yy.FATKG )),2) as [FAT KG],ROUND((sum(yy.SNFKG )),2) as [SNF KG],round(sum(yy.[Total SRN Amount])/sum([Net Weight]),2) As [Rate] " &
                    ",NULL as [Incentive Amount],NULL as [Rent Amount],NULL as [Freight Cost],NULL AS CPL,NULL AS [Prodcurement depart Salary] ,NULL as [Field Staff Fuel],NULL as [Emp CPL] " &
                    " from " &
                     " (Select DISTINCT xx.[To MCC or Plant Code],TSPL_LOCATION_MASTER.Location_Desc as [To MCC or Plant Name],xx.DocType,xx.[Vendor Code],xx.[Vendor Name],xx.[Challan No],xx.[Challan Date],xx.[SRN No],xx.[SRN Date],xx.[Invoice No],xx.[Invoice Date],xx.[Tanker No],xx.[Gate Entry No],xx.[Gate Entry Date],xx.[Weighment No],xx.[Weighment Date],xx.[Milk Receipt Challan No],xx.[Milk Receipt Challan Date],xx.[Challan Qty], xx.ChallanFatPer as [Challan Fat%],xx.ChallanSNFPer as [Challan SNF%],xx.ChallanFatKg as [Challan Fat KG],xx.ChallanSNFKg as [Challan SNF KG],(xx.ChallanFatKg+xx.ChallanSNFKg) as [Challan TS],xx.[Gross Weight],xx.[Tare Weight],xx.[Tare Date],xx.[Net Weight],xx.[From MCC or Plant Code],xx.[From MCC or Plant Name], xx.[Item Code],xx.[Item Desc],xx.UOM,xx.[QC No],XX.[Unloading Date Time], XX.[QC Date Time],XX.STATUS,xx.[Unloading No],xx.[MCC Name],xx.Plant,xx.[Silo Code],xx.[Silo Desc],xx.[Gate Out No],xx.[Gate Out Date Time],xx.[FAT %] ,xx.[SNF %] , xx.CLR,(xx.[Challan Qty]-xx.[Net Weight]) as [Differrence Qty],Convert (decimal(18,2),(xx.[ChallanFatPer]-xx.[FAT %])) as [Differrence FAT %],Convert (decimal(18,2),(xx.[ChallanSNFPer]-xx.[SNF %])) as [Differrence SNF %],Convert (decimal(18,2),(xx.ChallanFatKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)))) as [Differrence FAT kG],Convert (decimal(18,2),(ChallanSNFKg-Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)))) as [Differrence SNF KG]  ,xx.[Basic Rate], xx.incentive ,xx.[Special Deduction],xx.[Deduction] , xx.[Net Rate],xx.[FAT Rate], xx.[SNF Rate],case when SRN_Return_NO is not null then [Total Amount temp]*-1 else [Total Amount temp] end [Total SRN Amount],xx.[FAT Weightage & SNF Weightage],xx.[FAT Ratio & SNF ratio],xx.[Vendor Class] , case when SRN_Return_NO is not Null then 'SRN Return' else '' end as [SRN Return], Convert(decimal(18,3),(xx.[Net Weight] * xx.[FAT %] /    100)) As FATKG, Convert(decimal(18,3),(xx.[Net Weight] * xx.[SNF %] /    100)) As SNFKG,[FAT Amt],[SNF Amt],[Standard Rate] ,[Net Weight]/TSPL_ITEM_UOM_DETAIL.Conversion_Factor AS [Qty In LTR] " &
                " From (  " &
                " Select Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.fat_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.fat_per, 0) end  as ChallanFatPer,Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) ELSE IsNull(Tspl_Gate_Entry_Details.SNF_per, 0) end as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per*  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) END)/100 as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk In' Else 'MCC In' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, " &
            "Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' THEN Tspl_Gate_Entry_Details.Qty_In_Kg ELSE case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then Tspl_Gate_Entry_Details.Qty_In_Kg else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end END  As [Challan Qty]" &
             ",  TSPL_Weighment_Detail.Gross_Weight As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] " &
         ", Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR" &
          ", TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt) Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt) Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount Else TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From   (select Tspl_Gate_Entry_Details.Gate_Entry_No , Tspl_Gate_Entry_Details.Doc_type ,Tspl_Gate_Entry_Details.Date_And_Time, Tspl_Gate_Entry_Details.Challan_No, Tspl_Gate_Entry_Details.Challan_Date,Tspl_Gate_Entry_Details.Tanker_No, Tspl_Gate_Entry_Details.isPosted, Posting_Date,Dispatched_From_Mcc,location_Code,Location_Desc ,Vendor_Code,Vendor_Desc,Tspl_Gate_Entry_Details.Item_Code , Tspl_Gate_Entry_Details.Item_Desc, case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end  Qty_In_Kg  , case when TSPL_Gate_Entry_Chember_Details.snf_Per > 0 then TSPL_Gate_Entry_Chember_Details.snf_Per else Tspl_Gate_Entry_Details.snf_Per end as snf_Per ,case when TSPL_Gate_Entry_Chember_Details.fat_per > 0 then TSPL_Gate_Entry_Chember_Details.fat_per else  Tspl_Gate_Entry_Details.fat_per end as fat_per , Created_By ,Created_Date, Modify_By,Modify_Date,comp_code,Tspl_Gate_Entry_Details.UOM,Intimation_No , Supplier_Code,Dispatch_Centre_Code,Tspl_Gate_Entry_Details.MIKL_TYPE_CODE,Intimation_Status,Gate_Entry_Type,Seal_Status, case when IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) = 0 then case when TSPL_Gate_Entry_Chember_Details.Chamber_Qty > 0 then TSPL_Gate_Entry_Chember_Details.Chamber_Qty else Tspl_Gate_Entry_Details.Qty_In_Kg end else IsNull(Tspl_Gate_Entry_Details.TotalQty_In_Kg, 0) end as TotalQty_In_Kg ,SealNo_Header,Tanker_Return,PO_No,Amendment_Code,Amendment_By,Amendment_Date,IsAgainstJobWork,Sublocation_Code,In_Return,Transpoter_Id,Bulk_ROUTE_NO,Distance,Rate,Amount,ProvisionNo,NO_OF_CHAMBER,No_Of_CAN,Weight from " &
           " Tspl_Gate_Entry_Details  left outer join TSPL_Gate_Entry_Chember_Details on Tspl_Gate_Entry_Details.Gate_Entry_No = TSPL_Gate_Entry_Chember_Details.GE_Code) as Tspl_Gate_Entry_Details  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  " &
           " Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No   " &
           " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT  On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
          " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
          " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
           " where 2 = 2 "
            Qry += " and convert(date, Tspl_Gate_Entry_Details.Date_And_Time,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, Tspl_Gate_Entry_Details.Date_And_Time,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " union all  Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty],  TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG)*-1 End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1 Else (TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG)*-1 End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.Actual_Amount*-1 Else TSPL_MCC_Dispatch_Challan.Amount*-1 End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From  TSPL_MILK_TRANSFER_IN_RETURN LEFT OUTER JOIN  TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Receipt_Challan_No = TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_No  LEFT OUTER JOIN Tspl_Gate_Entry_Details ON  Tspl_Gate_Entry_Details.Gate_Entry_No=TSPL_MILK_TRANSFER_IN.Gate_Entry_no  Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                    " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
                    " Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
            " where 2 = 2 "
            Qry += " and convert(date, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_MILK_TRANSFER_IN_RETURN.Receipt_Challan_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " union all  Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG ,TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, 'Purchase Return'  As DocType, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Not Req' Else IsNull(TSPL_MILK_TRANSFER_IN.Receipt_Challan_No, '') End As [Milk Receipt Challan No],  IsNull(Convert(varchar,TSPL_MILK_TRANSFER_IN.Receipt_Challan_Date,103), '') As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No As [Invoice No],  Convert(varchar,TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, (-1)* Tspl_Gate_Entry_Details.Qty_In_Kg As [Challan Qty],  (-1)* TSPL_Weighment_Detail.Gross_Weight As [Gross Weight], (-1)* TSPL_Weighment_Detail.Tare_Weight As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) As [Tare Date], (-1)* TSPL_Weighment_Detail.Net_Weight As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time],  Convert(decimal(18,2),t_FAT.Param_Field_Value) As [FAT %] , Convert(decimal(18,2),t_SNF.Param_Field_Value) As [SNF %] , Convert(decimal(18,2), t_CLR.Param_Field_Value) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then TSPL_Bulk_MILK_SRN.BasicRate Else TSPL_MCC_Dispatch_Challan.Transfer_Price End As [Basic Rate],  (-1)* TSPL_Bulk_MILK_SRN.Incentive as Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate], Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) Else TSPL_MCC_Dispatch_Challan.FAT_RATE End As [FAT Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) Else TSPL_MCC_Dispatch_Challan.SNF_RATE End As [SNF Rate],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.FatAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.FAT_RATE * TSPL_MCC_Dispatch_Challan.FAT_KG) End As [FAT Amt],  Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then Convert(decimal(18,2),(-1)*TSPL_Bulk_MILK_SRN.SnfAmt) Else (-1)*(TSPL_MCC_Dispatch_Challan.SNF_RATE * TSPL_MCC_Dispatch_Challan.SNF_KG) End As [SNF Amt],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then (-1)* TSPL_Bulk_MILK_SRN.Actual_Amount Else (-1)*TSPL_MCC_Dispatch_Challan.Amount End As [Total Amount Temp], 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_W) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_W) End As 'FAT Weightage & SNF Weightage', 'For ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.FAT_R) End + ' & ' + Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage) Else Convert(varchar,TSPL_MCC_Dispatch_Challan.SNF_R) End As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code  Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO  Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO  Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_HEAD On TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Invoice_No  = tspl_Bulk_milk_purchase_Invoice_head.DOC_NO  Left Outer Join TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL  On TSPL_BULK_MILK_PURCHASE_RETURN_DETAIL.Pur_Return_No = TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No   Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join TSPL_MILK_TRANSFER_IN On TSPL_MILK_TRANSFER_IN.Gate_Entry_no = Tspl_Gate_Entry_Details.Gate_Entry_No  Left Outer Join TSPL_MCC_Dispatch_Challan On TSPL_MCC_Dispatch_Challan.Chalan_NO = Tspl_Gate_Entry_Details.Challan_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT " &
           " On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No " &
           " where 2=2 and TSPL_Bulk_MILK_SRN.isposted=1 and isnull(TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_No,'') <>''  "
            Qry += " and convert(date, TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_BULK_MILK_PURCHASE_RETURN_HEAD.Pur_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "

            Qry += "  UNION ALL Select  Tspl_Gate_Entry_Details.fat_per as ChallanFatPer,Tspl_Gate_Entry_Details.snf_per as ChallanSNFPer, (Tspl_Gate_Entry_Details.fat_per* (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanFATKG , (Tspl_Gate_Entry_Details.snf_Per *  (Tspl_Gate_Entry_Details.Qty_In_Kg*-1))/100 as ChallanSNFKG , TSPL_Bulk_Milk_SRN_Return.SRN_Return_NO, Case When IsNull(Tspl_Gate_Entry_Details.Doc_Type, '') = 'BulkProc' Then 'Bulk Ret' Else 'MCC Ret' End As DocType,  'Not Req'  As [Milk Receipt Challan No],  '' As [Milk Receipt Challan Date], Tspl_Gate_Entry_Details.Vendor_Code As [Vendor Code],  TSPL_VENDOR_MASTER.Vendor_Name As [Vendor Name], Tspl_Gate_Entry_Details.Challan_No As [Challan No], Convert(varchar,Tspl_Gate_Entry_Details.Challan_Date,103) As [Challan Date], TSPL_Bulk_MILK_SRN.SRN_NO As [SRN No], Convert(varchar,TSPL_Bulk_MILK_SRN.SRN_Date,103) As [SRN Date], tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO As [Invoice No],  Convert(varchar,tspl_Bulk_milk_purchase_Invoice_head.DOC_DATE,103) As [Invoice Date], Tspl_Gate_Entry_Details.Tanker_No As [Tanker No],  Tspl_Gate_Entry_Details.Gate_Entry_No As [Gate Entry No], Convert(varchar,TSPL_Weighment_Detail.Weighment_date,103) As [Weighment Date],  Convert(varchar,Tspl_Gate_Entry_Details.Date_And_Time,103) As [Gate Entry Date], Tspl_Gate_Entry_Details.Date_And_Time As [Gate Entry],  TSPL_Weighment_Detail.Weighment_No As [Weighment No], TSPL_Weighment_Detail.Weighment_date, Tspl_Gate_Entry_Details.Qty_In_Kg*-1 As [Challan Qty],  TSPL_Weighment_Detail.Gross_Weight*-1 As [Gross Weight], TSPL_Weighment_Detail.Tare_Weight*-1 As [Tare Weight], Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,103) + ' ' + Convert(varchar,TSPL_Weighment_Detail.Tare_Weight_date,108) As [Tare Date], TSPL_Weighment_Detail.Net_Weight*-1 As [Net Weight],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else Tspl_Gate_Entry_Details.Dispatched_From_Mcc End As [From MCC or Plant Code],  Case When Tspl_Gate_Entry_Details.Doc_Type = 'BulkProc' Then '' Else TSPL_MCC_MASTER_From_Mcc.MCC_NAME End As [From MCC or Plant Name],  Tspl_Gate_Entry_Details.location_Code As [MCC or Plant Code], Tspl_Gate_Entry_Details.location_Code [To MCC or Plant Code],  Tspl_Gate_Entry_Details.Location_Desc As [To MCC or Plant Name], Tspl_Gate_Entry_Details.Item_Code As [Item Code],  TSPL_ITEM_MASTER.Item_Desc As [Item Desc], Case When IsNull(Tspl_Gate_Entry_Details.UOM, '') = '' Then TSPL_ITEM_UOM_DETAIL.UOM_Code Else Tspl_Gate_Entry_Details.UOM End As UOM,  TSPL_QUALITY_CHECK.QC_No As [QC No], Convert(varchar,TSPL_MILK_UNLOADING.Unloading_Date_Time,103) As [Unloading Date Time],  Convert(varchar,TSPL_QUALITY_CHECK.QC_In_Date_Time,103) As [QC Date Time], Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '0' Then 'Rejected' Else Case When TSPL_QUALITY_CHECK.isPosted = '0' And TSPL_QUALITY_CHECK.is_Param_Accepted = TSPL_QUALITY_CHECK.is_Param_Accepted Then 'Pending' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '1' Then 'Accepted' Else Case When TSPL_QUALITY_CHECK.isPosted = '1' And TSPL_QUALITY_CHECK.is_Param_Accepted = '2' Then 'Accepted with Special Approval' End End End End End As STATUS,  TSPL_MILK_UNLOADING.Unloading_No As [Unloading No], TSPL_MILK_UNLOADING.Sub_location_Code As [MCC Name], TSPL_MILK_UNLOADING.Sub_location_Code As Plant, TSPL_MILK_UNLOADING.Sub_location_Code As [Silo Code],  TSPL_LOCATION_MASTER.Location_Desc As [Silo Desc], TSPL_Gate_Out.Doc_No As [Gate Out No], Convert(varchar,TSPL_Gate_Out.Doc_Date,103) As [Gate Out Date Time] ,  Convert(decimal(18,2),isnull(t_FAT.Param_Field_Value,0)) As [FAT %] , Convert(decimal(18,2),isnull(t_SNF.Param_Field_Value,0)) As [SNF %] , Convert(decimal(18,2), isnull(t_CLR.Param_Field_Value,0)) As CLR, TSPL_Bulk_MILK_SRN.StandardRate As [Standard Rate], TSPL_Bulk_MILK_SRN.BasicRate As [Basic Rate],  TSPL_Bulk_MILK_SRN.Incentive, TSPL_Bulk_MILK_SRN.Deduction, TSPL_Bulk_MILK_SRN.SpecialDeduction As [Special Deduction], Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.NetRate) As [Net Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.fat_Rate) As [FAT Rate],   Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SNF_Rate) As [SNF Rate],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.FatAmt)*-1  As [FAT Amt],  Convert(decimal(18,2),TSPL_Bulk_MILK_SRN.SnfAmt)*-1  As [SNF Amt],  TSPL_Bulk_MILK_SRN.Actual_Amount*-1 As [Total Amount Temp], 'For ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Weightage)  + ' & ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Weightage)  As 'FAT Weightage & SNF Weightage', 'For ' + Convert(varchar,TSPL_Bulk_Price_MASTER.Fat_Percentage)  + ' & ' +  Convert(varchar,TSPL_Bulk_Price_MASTER.Snf_Percentage)  As 'FAT Ratio & SNF ratio',  TSPL_VENDOR_MASTER.Vendor_Type As [Vendor Class]     From  " &
              " Tspl_Gate_Entry_Details Left Outer Join TSPL_Weighment_Detail On TSPL_Weighment_Detail.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
              " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code = Tspl_Gate_Entry_Details.Vendor_Code  " &
              " Left Join TSPL_MCC_MASTER As TSPL_MCC_MASTER_From_Mcc On Tspl_Gate_Entry_Details.Dispatched_From_Mcc = TSPL_MCC_MASTER_From_Mcc.MCC_Code  " &
              " Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = Tspl_Gate_Entry_Details.Item_Code  " &
              " Left Outer Join TSPL_QUALITY_CHECK On TSPL_QUALITY_CHECK.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_MILK_UNLOADING On TSPL_MILK_UNLOADING.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_MILK_UNLOADING.Sub_location_Code   " &
              " Left Outer Join TSPL_Gate_Out On TSPL_Gate_Out.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No   " &
              " Left Outer Join TSPL_Bulk_MILK_SRN On TSPL_Bulk_MILK_SRN.Gate_Entry_No = Tspl_Gate_Entry_Details.Gate_Entry_No  " &
              " Left Join TSPL_Bulk_Milk_SRN_Return On TSPL_Bulk_Milk_SRN_Return.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO   " &
              " Left Outer Join TSPL_Bulk_Price_MASTER On TSPL_Bulk_Price_MASTER.Price_Code = TSPL_Bulk_MILK_SRN.Price_Code  " &
              " Left Outer Join tspl_Bulk_milk_purchase_Invoice_Detail On tspl_Bulk_milk_purchase_Invoice_Detail.SRN_NO = TSPL_Bulk_MILK_SRN.SRN_NO " &
              " Left Outer Join tspl_Bulk_milk_purchase_Invoice_head On tspl_Bulk_milk_purchase_Invoice_head.DOC_NO = tspl_Bulk_milk_purchase_Invoice_Detail.DOC_NO   " &
              " Left Outer Join TSPL_ITEM_UOM_DETAIL On TSPL_ITEM_UOM_DETAIL.Item_Code = Tspl_Gate_Entry_Details.Item_Code And TSPL_ITEM_UOM_DETAIL.Stocking_Unit = 'Y'  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'FAT') t_FAT On t_FAT.QC_No = TSPL_QUALITY_CHECK.QC_No   Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'SNF') t_SNF On t_SNF.QC_No = TSPL_QUALITY_CHECK.QC_No  Left Outer Join (Select TSPL_QC_Parameter_Detail.*      From TSPL_QUALITY_CHECK Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No = TSPL_QUALITY_CHECK.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'CLR') t_CLR On t_CLR.QC_No = TSPL_QUALITY_CHECK.QC_No  " &
             " where 2=2   "
            Qry += " and convert(date, TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_Bulk_Milk_SRN_Return.SRN_Return_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += "  ) As xx inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.[To MCC or Plant Code] " &
              " Left Outer Join TSPL_ITEM_UOM_DETAIL  On TSPL_ITEM_UOM_DETAIL.Item_Code = xx.[Item Code] And TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' " &
              " where  isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N' and  isnull(TSPL_LOCATION_MASTER.Is_Jobwork,0) =0 and xx.DocType in('Bulk In','Bulk Ret')" &
             " ) as yy  GROUP BY yy.[To MCC or Plant Name]+ ' - ' +yy.[Vendor Name],yy.[To MCC or Plant Code] "
            Qry += " ) as zz " &
            "left join  (select TSPL_MCC_MASTER.MCC_Code,sum(TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Incentive_Amount) as Incentive_Amount " &
            ",(sum(TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Rent_Amount)) as Rent_Amount " &
            " from TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL " &
            " LEFT JOIN TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD ON TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Doc_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_DETAIL.Doc_Code " &
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.MCC_Code "
            Qry += " where convert(date, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_INCENTIVE_ENTRY_BY_SRN_HEAD.Filter_Month,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " group by TSPL_MCC_MASTER.MCC_Code " &
             ")INC ON INC.MCC_Code=zz.[MCC Code] " &
            " left join (select TSPL_MCC_ROUTE_MASTER.MCC_Code,SUM(TSPL_PROVISION_ENTRY.Amount) AS [Freight Cost] from " &
            " TSPL_PROVISION_ENTRY left join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_PROVISION_ENTRY.Route_Code " &
            " Left join (select TSPL_Primary_Vehicle_Master.vendor_code as [Transporter Code],tspl_vendor_master.vendor_name as [Transporter Name],TSPL_Primary_Vehicle_Master.mcc_code,TSPL_Primary_Vehicle_Master.vehicle_code from TSPL_Primary_Vehicle_Master left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=TSPL_Primary_Vehicle_Master.vendor_code and tspl_vendor_master.form_type='PTM' left outer join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_Primary_Vehicle_Master.mcc_code) as t1  " &
            " on t1.vehicle_code=TSPL_MCC_ROUTE_MASTER.Vehicle_Code   " &
            " Left Outer Join TSPL_Primary_Vehicle_Master On TSPL_Primary_Vehicle_Master.Vehicle_Code = TSPL_MCC_ROUTE_MASTER.Vehicle_Code  "
            Qry += " where TSPL_PROVISION_ENTRY.isposted=1 and TSPL_PROVISION_ENTRY.Prog_code='M-Shift_End' and convert(date, TSPL_PROVISION_ENTRY.Doc_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_PROVISION_ENTRY.Doc_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " GROUP BY TSPL_MCC_ROUTE_MASTER.MCC_Code " &
             ")PROV ON PROV.MCC_Code=zz.[MCC Code] "

            Dim dtBulk As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If dtBulk IsNot Nothing And dtBulk.Rows.Count > 0 Then
                dt.Merge(dtBulk, True, MissingSchemaAction.Ignore)
                Dim FatPerAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim SNFPerAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim RateAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("AVG(Rate)", " Rate is not null")), 2)
                Dim CPLAvg1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("AVG(CPL)", " CPL is not null")), 2)

                Dim QtyInLTRSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Qty In LTR])", " [Qty In LTR] is not null")), 2)
                Dim QtyInKGSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Qty In KG])", " [Qty In KG] is not null")), 2)
                Dim MilkPaymentSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Milk Payment])", " [Milk Payment] is not null")), 2)
                Dim FATKGSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([FAT KG])", " [FAT KG] is not null")), 2)
                Dim SNFKGSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([SNF KG])", " [SNF KG] is not null")), 2)
                Dim IncentiveAmountSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Incentive Amount])", " [Incentive Amount] is not null")), 2)
                Dim RentAmountSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Rent Amount])", " [Rent Amount] is not null")), 2)
                Dim FreightCostSum1 As Decimal = Math.Round(clsCommon.myCdbl(dtBulk.Compute("SUM([Freight Cost])", " [Freight Cost] is not null")), 2)


                dt.Rows.Add("Total", QtyInLTRSum1, QtyInKGSum1, MilkPaymentSum1, FatPerAvg1, SNFPerAvg1, FATKGSum1, SNFKGSum1, RateAvg1, IncentiveAmountSum1, RentAmountSum1, FreightCostSum1, CPLAvg1, DBNull.Value, DBNull.Value, DBNull.Value)
                Dim FatPerAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
                Dim SNFPerAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
                Dim RateAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(Rate)", "[MCC Name]='Total'")), 2)
                Dim CPLAvgSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(CPL)", "[MCC Name]='Total'")), 2)

                Dim QtyInLTRSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In LTR])", "[MCC Name]='Total'")), 2)
                Dim QtyInKGSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
                Dim MilkPaymentSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Milk Payment])", "[MCC Name]='Total'")), 2)
                Dim FATKGSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([FAT KG])", "[MCC Name]='Total'")), 2)
                Dim SNFKGSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([SNF KG])", "[MCC Name]='Total'")), 2)
                Dim IncentiveAmountSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Incentive Amount])", "[MCC Name]='Total'")), 2)
                Dim RentAmountSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Rent Amount])", "[MCC Name]='Total'")), 2)
                Dim FreightCostSumSub As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Freight Cost])", "[MCC Name]='Total'")), 2)

                dt.Rows.Add("Sub Total", QtyInLTRSumSub, QtyInKGSumSub, MilkPaymentSumSub, FatPerAvgSub, SNFPerAvgSub, FATKGSumSub, SNFKGSumSub, RateAvgSub, IncentiveAmountSumSub, RentAmountSumSub, FreightCostSumSub, CPLAvgSub, DBNull.Value, DBNull.Value, DBNull.Value)
            End If

            '''''''''''''''''''

            Qry = "select TSPL_LOCATION_MASTER.Location_Desc as [MCC Name] " &
             " ,Cast(ROUND(sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight/TSPL_ITEM_UOM_DETAIL.Conversion_Factor),2) as decimal(18,2)) as [Qty In LTR] " &
             " ,ROUND(sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight),2) as [Qty In KG] " &
             " ,NULL As [Milk Payment],ROUND((sum(xx.FAT_KG)/sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight))*100,2) as [FAT %] " &
             " ,ROUND((sum(xx.SNF_KG)/sum(TSPL_GENERAL_WEIGHMENT_DETAIL.Net_Weight))*100,2)  as [SNF %] ,sum(xx.FAT_KG) as [FAT KG],sum(xx.SNF_KG) as [SNF KG] " &
              " ,NULL As [Rate],NULL as [Incentive Amount],NULL as [Rent Amount],NULL as [Freight Cost],NULL AS CPL " &
              " ,NULL AS [Prodcurement depart Salary] ,NULL as [Field Staff Fuel],NULL as [Emp CPL] " &
             " from TSPL_GENERAL_WEIGHMENT_DETAIL   left join tspl_item_master on tspl_item_master.Item_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code   left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Location_Code   left join TSPL_VENDOR_master on TSPL_VENDOR_master.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor   left join  " &
             " (select TSPL_JWI_ESTIMATION_WEIGHMENT.Weighment_Code,TSPL_JWI_ESTIMATION_WEIGHMENT.FAT_KG,TSPL_JWI_ESTIMATION_WEIGHMENT.SNF_KG ,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_FAT_KG " &
             " ,TSPL_JWI_ESTIMATION_WEIGHMENT.Estimated_SNF_KG from TSPL_JWI_ESTIMATION_WEIGHMENT left join TSPL_JWI_ESTIMATION_HEAD on TSPL_JWI_ESTIMATION_HEAD.Document_No=TSPL_JWI_ESTIMATION_WEIGHMENT.document_no where TSPL_JWI_ESTIMATION_HEAD.Status=1)xx on xx.Weighment_Code=TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_No   " &
             " Left Outer Join TSPL_ITEM_UOM_DETAIL  On TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_GENERAL_WEIGHMENT_DETAIL.Item_code " &
             " And TSPL_ITEM_UOM_DETAIL.UOM_Code = 'Ltr' " &
            "  where " &
            " TSPL_GENERAL_WEIGHMENT_DETAIL.Item_Code is not null and TSPL_GENERAL_WEIGHMENT_DETAIL.IsJobWork = 1 And TSPL_GENERAL_WEIGHMENT_DETAIL.Posted = 1   "
            Qry += " and convert(date, TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_GENERAL_WEIGHMENT_DETAIL.Weighment_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " group by TSPL_LOCATION_MASTER.Location_Desc "

            Dim dtJobWork As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dtJobWork IsNot Nothing And dtJobWork.Rows.Count > 0 Then
                Dim newBlankRow2 As DataRow = dt.NewRow
                dt.Rows.Add(newBlankRow2)
                dt.Merge(dtJobWork, True, MissingSchemaAction.Ignore)
                Dim FatPerAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim SNFPerAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "")), 2)
                Dim RateAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("AVG(Rate)", " Rate is not null")), 2)
                Dim CPLAvg3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("AVG(CPL)", " CPL is not null")), 2)

                Dim QtyInLTRSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Qty In LTR])", " [Qty In LTR] is not null")), 2)
                Dim QtyInKGSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Qty In KG])", " [Qty In KG] is not null")), 2)
                Dim MilkPaymentSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Milk Payment])", " [Milk Payment] is not null")), 2)
                Dim FATKGSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([FAT KG])", " [FAT KG] is not null")), 2)
                Dim SNFKGSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([SNF KG])", " [SNF KG] is not null")), 2)
                Dim IncentiveAmountSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Incentive Amount])", " [Incentive Amount] is not null")), 2)
                Dim RentAmountSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Rent Amount])", " [Rent Amount] is not null")), 2)
                Dim FreightCostSum3 As Decimal = Math.Round(clsCommon.myCdbl(dtJobWork.Compute("SUM([Freight Cost])", " [Freight Cost] is not null")), 2)


                dt.Rows.Add("Total", QtyInLTRSum3, QtyInKGSum3, MilkPaymentSum3, FatPerAvg3, SNFPerAvg3, FATKGSum3, SNFKGSum3, RateAvg3, IncentiveAmountSum3, RentAmountSum3, FreightCostSum3, CPLAvg3, DBNull.Value, DBNull.Value, DBNull.Value)
            End If

            'Grand Total Row
            Dim FatPerAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([FAT KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
            Dim SNFPerAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("(SUM([SNF KG])*100)/SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
            Dim RateAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(Rate)", "[MCC Name]='Total'")), 2)
            Dim CPLAvg2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("AVG(CPL)", "[MCC Name]='Total'")), 2)

            Dim QtyInLTRSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In LTR])", "[MCC Name]='Total'")), 2)
            Dim QtyInKGSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Qty In KG])", "[MCC Name]='Total'")), 2)
            Dim MilkPaymentSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Milk Payment])", "[MCC Name]='Total'")), 2)
            Dim FATKGSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([FAT KG])", "[MCC Name]='Total'")), 2)
            Dim SNFKGSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([SNF KG])", "[MCC Name]='Total'")), 2)
            Dim IncentiveAmountSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Incentive Amount])", "[MCC Name]='Total'")), 2)
            Dim RentAmountSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Rent Amount])", "[MCC Name]='Total'")), 2)
            Dim FreightCostSum2 As Decimal = Math.Round(clsCommon.myCdbl(dt.Compute("SUM([Freight Cost])", "[MCC Name]='Total'")), 2)

            dt.Rows.Add("Grand Total", QtyInLTRSum2, QtyInKGSum2, MilkPaymentSum2, FatPerAvg2, SNFPerAvg2, FATKGSum2, SNFKGSum2, RateAvg2, IncentiveAmountSum2, RentAmountSum2, FreightCostSum2, CPLAvg2, DBNull.Value, DBNull.Value, DBNull.Value)

            gv_MilkReceived.DataSource = Nothing

            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_MilkReceived.Rows.Clear()
                gv_MilkReceived.Columns.Clear()
                gv_MilkReceived.GroupDescriptors.Clear()
                gv_MilkReceived.MasterTemplate.SummaryRowsBottom.Clear()
                gv_MilkReceived.DataSource = dt
                gv_MilkReceived.MasterTemplate.BestFitColumns()
                gv_MilkReceived.EnableFiltering = True
                For i As Integer = 0 To gv_MilkReceived.Columns.Count - 1
                    gv_MilkReceived.Columns(i).BestFit()
                Next

                If False Then
                    Dim dr As DataRow() = dt.Select(" CPL is not null and CPL> 0 and [MCC Name]<>'Total' and [MCC Name]<>'Sub Total' and [MCC Name]<>'Grand Total'")
                    If dr IsNot Nothing AndAlso dr.Length > 0 Then
                        Dim dtchart As DataTable = dr.CopyToDataTable()
                        Dim barSeries As New Telerik.WinControls.UI.BarSeries("CPL", "MCC Name")
                        barSeries.BackColor = Color.Olive
                        Me.RadChartViewMilkReceived.Series.Add(barSeries)
                        barSeries.DataSource = dtchart
                        barSeries.HorizontalAxis.LabelFitMode = AxisLabelFitMode.MultiLine 'AxisLabelFitMode.Rotate
                        Me.RadChartViewMilkReceived.LegendTitle = "CPL"
                    End If
                End If


            Else
                'clsCommon.MyMessageBoxShow("Record Not Found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadMilkSale()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "MilkSale"
            RadChartViewMilkSale.Series.Clear()
            Dim Qry As String = ""
            Dim obj As New clsSaleRegisterParameterType
            obj.Unit_Code = "Ltr"
            obj.From_Date = clsCommon.myCDate(txtFromDate.Value)
            obj.To_Date = clsCommon.myCDate(txtToDate.Value)
            obj.Other_Cond = " and xx.Status=1  "

            Dim qry1 As String
            qry1 = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
            Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry1)
            Dim arrTrans As New ArrayList
            For Each dr As DataRow In dtTrans.Rows
                arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
            Next
            obj.Trans_Type_List = arrTrans

            Qry = "select isnull(TSPL_ITEM_MASTER.Alies_Name,'') as [Item] " &
 ",Sum(TempData.Qty) as [Quantity In Ltr],Sum(TempData.SchemeQty) as [Scheme Quantity In Ltr],Sum(TempData.SampleQty) as [Sample Quantity In Ltr] " &
 ",sum(TempData.FATKG) AS [FAT KG],sum(TempData.SchemeFATKG) AS [Scheme FAT KG],sum(TempData.SampleFATKG) AS [Sample FAT KG] " &
 ",sum(TempData.SNFKG) AS [SNF KG],sum(TempData.SchemeSNFKG) AS [Scheme SNF KG],sum(TempData.SampleSNFKG) AS [Sample SNF KG] " &
 ",sum(TempData.Amt) AS [Sale Amount],sum(TempData.SchemeAmt) AS [Scheme Sale Amount],sum(TempData.SampleAmt) AS [Sample Sale Amount] " &
 ",case when Sum(TempData.Qty)=0 then 0 else convert(decimal(18,2),sum(TempData.Amt)/Sum(TempData.Qty)) end AS [Ave Realisa Per Ltr] " &
 ",case when Sum(TempData.SchemeQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SchemeAmt)/Sum(TempData.SchemeQty)) end AS [Scheme Ave Realisa Per Ltr] " &
 ",case when Sum(TempData.SampleQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SampleAmt)/Sum(TempData.SampleQty)) end AS [Sample Ave Realisa Per Ltr] " &
 " from( " &
 " Select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name] " &
  ", SUM([Quantity]) [Quantity], MAX([UOM]) [UOM] ,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum(COALESCE([FAT KG],0))+sum(COALESCE([SNF KG],0)) as TSKG,Convert(decimal(18,2),sum([Sale Amount])) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount] " &
  ",sum([Total Tax Amount]) as [Total Tax Amount],Convert(decimal(18,2),sum([Total Amount] )) as [Total Amount] " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Quantity] else 0 end) as Qty " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Sale Amount] else 0 end) Amt " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then COALESCE([FAT KG],0) else 0 end) as FATKG " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then COALESCE([SNF KG],0) else 0 end) as SNFKG " &
 ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Quantity] else 0 end) as SchemeQty " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Sale Amount] else 0 end) as SchemeAmt " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then COALESCE([FAT KG],0) else 0 end) as SchemeFATKG " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then COALESCE([SNF KG],0) else 0 end) as SchemeSNFKG " &
 ",sum(case when ISNULL(Sampling,'')='Y' then [Quantity] else 0 end) as SampleQty " &
  ",sum(case when ISNULL(Sampling,'')='Y' then [Sale Amount] else 0 end) SampleAmt " &
  ",sum(case when ISNULL(Sampling,'')='Y' then COALESCE([FAT KG],0) else 0 end) as SampleFATKG " &
  ",sum(case when ISNULL(Sampling,'')='Y' then COALESCE([SNF KG],0) else 0 end) as SampleSNFKG " &
 " from (  "
            Qry += clsPSInvoiceHead.GetReportDataQuery(obj)
            Qry += " ) as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code] " &
  ",[Item Name] ,  [UOM]  )TempData  left join tspl_item_master on tspl_item_master.Item_Code=TempData.[Item Code]  where isnull(TSPL_ITEM_MASTER.Is_FreshItem,0)=1   " &
  " group by isnull(TSPL_ITEM_MASTER.Alies_Name,'') order by isnull(TSPL_ITEM_MASTER.Alies_Name,'')"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            obj.Unit_Code = "Kg"

            Qry = "select isnull(TSPL_ITEM_MASTER.Alies_Name,'') as [Item] " &
 ",Sum(TempData.Qty) as [Quantity In Kg],Sum(TempData.SchemeQty) as [Scheme Quantity In Kg],Sum(TempData.SampleQty) as [Sample Quantity In Kg] " &
 ",sum(TempData.Amt) AS [Sale Amount],sum(TempData.SchemeAmt) AS [Scheme Sale Amount],sum(TempData.SampleAmt) AS [Sample Sale Amount] " &
 ",case when Sum(TempData.Qty)=0 then 0 else convert(decimal(18,2),sum(TempData.Amt)/Sum(TempData.Qty)) end AS [Ave Realisa Per Kg] " &
 ",case when Sum(TempData.SchemeQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SchemeAmt)/Sum(TempData.SchemeQty)) end AS [Scheme Ave Realisa Per Kg] " &
 ",case when Sum(TempData.SampleQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SampleAmt)/Sum(TempData.SampleQty)) end AS [Sample Ave Realisa Per Kg] " &
 " from( " &
 " Select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name] " &
  ", SUM([Quantity]) [Quantity], MAX([UOM]) [UOM] ,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum(COALESCE([FAT KG],0))+sum(COALESCE([SNF KG],0)) as TSKG,Convert(decimal(18,2),sum([Sale Amount])) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount] " &
  ",sum([Total Tax Amount]) as [Total Tax Amount],Convert(decimal(18,2),sum([Total Amount] )) as [Total Amount] " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Quantity] else 0 end) as Qty " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Sale Amount] else 0 end) Amt " &
   ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Quantity] else 0 end) as SchemeQty " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Sale Amount] else 0 end) as SchemeAmt " &
   ",sum(case when ISNULL(Sampling,'')='Y' then [Quantity] else 0 end) as SampleQty " &
  ",sum(case when ISNULL(Sampling,'')='Y' then [Sale Amount] else 0 end) SampleAmt " &
 " from (  "
            Qry += clsPSInvoiceHead.GetReportDataQuery(obj)
            Qry += " ) as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code] " &
  ",[Item Name] ,  [UOM]  )TempData  left join tspl_item_master on tspl_item_master.Item_Code=TempData.[Item Code]  where isnull(TSPL_ITEM_MASTER.Is_FreshItem,0)=1   " &
  " group by isnull(TSPL_ITEM_MASTER.Alies_Name,'') order by isnull(TSPL_ITEM_MASTER.Alies_Name,'')"

            Dim dtKg As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim selectedColumns As String() = {"Item", "Quantity In Kg", "Scheme Quantity In Kg", "Sample Quantity In Kg", "Ave Realisa Per Kg", "Scheme Ave Realisa Per Kg", "Sample Ave Realisa Per Kg"}
            Dim dtSelectKg As DataTable = New DataView(dtKg).ToTable(False, selectedColumns)

            Dim dtAll As DataTable = New DataTable()
            dtAll.Merge(dt)
            dtAll.Columns.Add(New DataColumn("Quantity In Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Scheme Quantity In Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Sample Quantity In Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Ave Realisa Per Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Scheme Ave Realisa Per Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Sample Ave Realisa Per Kg", System.Type.GetType("System.Decimal")))

            For jrow As Integer = 0 To dtSelectKg.Rows.Count - 1
                If clsCommon.myCstr(dtAll.Rows(jrow)("Item")) = clsCommon.myCstr(dtSelectKg.Rows(jrow)("Item")) Then
                    dtAll.Rows(jrow)("Quantity In Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Quantity In Kg"))
                    dtAll.Rows(jrow)("Scheme Quantity In Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Scheme Quantity In Kg"))
                    dtAll.Rows(jrow)("Sample Quantity In Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Sample Quantity In Kg"))
                    dtAll.Rows(jrow)("Ave Realisa Per Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Ave Realisa Per Kg"))
                    dtAll.Rows(jrow)("Scheme Ave Realisa Per Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Scheme Ave Realisa Per Kg"))
                    dtAll.Rows(jrow)("Sample Ave Realisa Per Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Sample Ave Realisa Per Kg"))
                End If
            Next

            dtAll.Columns("Item").SetOrdinal(0)
            dtAll.Columns("Quantity In Ltr").SetOrdinal(1)
            dtAll.Columns("Scheme Quantity In Ltr").SetOrdinal(2)
            dtAll.Columns("Sample Quantity In Ltr").SetOrdinal(3)

            dtAll.Columns("Quantity In Kg").SetOrdinal(4)
            dtAll.Columns("Scheme Quantity In Kg").SetOrdinal(5)
            dtAll.Columns("Sample Quantity In Kg").SetOrdinal(6)

            dtAll.Columns("FAT KG").SetOrdinal(7)
            dtAll.Columns("Scheme FAT KG").SetOrdinal(8)
            dtAll.Columns("Sample FAT KG").SetOrdinal(9)

            dtAll.Columns("SNF KG").SetOrdinal(10)
            dtAll.Columns("Scheme SNF KG").SetOrdinal(11)
            dtAll.Columns("Sample SNF KG").SetOrdinal(12)

            dtAll.Columns("Sale Amount").SetOrdinal(13)
            dtAll.Columns("Scheme Sale Amount").SetOrdinal(14)
            dtAll.Columns("Sample Sale Amount").SetOrdinal(15)

            dtAll.Columns("Ave Realisa Per Ltr").SetOrdinal(16)
            dtAll.Columns("Scheme Ave Realisa Per Ltr").SetOrdinal(17)
            dtAll.Columns("Sample Ave Realisa Per Ltr").SetOrdinal(18)

            dtAll.Columns("Ave Realisa Per Kg").SetOrdinal(19)
            dtAll.Columns("Scheme Ave Realisa Per Kg").SetOrdinal(20)
            dtAll.Columns("Sample Ave Realisa Per Kg").SetOrdinal(21)

            gv_MilkSale.DataSource = Nothing

            If dtAll IsNot Nothing OrElse dtAll.Rows.Count > 0 Then
                gv_MilkSale.Rows.Clear()
                gv_MilkSale.Columns.Clear()
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

                If False Then
                    Dim barSeries As New Telerik.WinControls.UI.BarSeries("Ave Realisa Per Ltr", "Item")
                    barSeries.BackColor = Color.Olive
                    Me.RadChartViewMilkSale.Series.Add(barSeries)
                    barSeries.DataSource = dtAll
                    barSeries.HorizontalAxis.LabelFitMode = AxisLabelFitMode.Rotate
                    barSeries.VerticalAxis.LabelInterval = 4
                    Me.RadChartViewMilkSale.LegendTitle = "Ave Realisa Per Ltr"
                End If
            Else
                'clsCommon.MyMessageBoxShow("Record Not Found.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadProductSale()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "ProductSale"
            RadChartViewProductSale.Series.Clear()
            Dim Qry As String = ""
            Dim obj As New clsSaleRegisterParameterType
            obj.Unit_Code = "Ltr"
            obj.From_Date = clsCommon.myCDate(txtFromDate.Value)
            obj.To_Date = clsCommon.myCDate(txtToDate.Value)
            obj.Other_Cond = " and xx.Status=1  "

            Dim qry1 As String
            qry1 = clsPSInvoiceHead.GetAllSaleTransactionTypeQuery()
            Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable(qry1)
            Dim arrTrans As New ArrayList
            For Each dr As DataRow In dtTrans.Rows
                arrTrans.Add(clsCommon.myCstr(dr.Item("Name")))
            Next
            obj.Trans_Type_List = arrTrans


            Qry = "select isnull(TSPL_ITEM_MASTER.Alies_Name,'') as [Item] " &
 ",Sum(TempData.Qty) as [Quantity In Ltr],Sum(TempData.SchemeQty) as [Scheme Quantity In Ltr],Sum(TempData.SampleQty) as [Sample Quantity In Ltr] " &
 ",sum(TempData.FATKG) AS [FAT KG],sum(TempData.SchemeFATKG) AS [Scheme FAT KG],sum(TempData.SampleFATKG) AS [Sample FAT KG] " &
 ",sum(TempData.SNFKG) AS [SNF KG],sum(TempData.SchemeSNFKG) AS [Scheme SNF KG],sum(TempData.SampleSNFKG) AS [Sample SNF KG] " &
 ",sum(TempData.Amt) AS [Sale Amount],sum(TempData.SchemeAmt) AS [Scheme Sale Amount],sum(TempData.SampleAmt) AS [Sample Sale Amount] " &
 ",case when Sum(TempData.Qty)=0 then 0 else convert(decimal(18,2),sum(TempData.Amt)/Sum(TempData.Qty)) end AS [Ave Realisa Per Ltr] " &
 ",case when Sum(TempData.SchemeQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SchemeAmt)/Sum(TempData.SchemeQty)) end AS [Scheme Ave Realisa Per Ltr] " &
 ",case when Sum(TempData.SampleQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SampleAmt)/Sum(TempData.SampleQty)) end AS [Sample Ave Realisa Per Ltr] " &
 " from( " &
 " Select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name] " &
  ", SUM([Quantity]) [Quantity], MAX([UOM]) [UOM] ,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum(COALESCE([FAT KG],0))+sum(COALESCE([SNF KG],0)) as TSKG,Convert(decimal(18,2),sum([Sale Amount])) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount] " &
  ",sum([Total Tax Amount]) as [Total Tax Amount],Convert(decimal(18,2),sum([Total Amount] )) as [Total Amount] " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Quantity] else 0 end) as Qty " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Sale Amount] else 0 end) Amt " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then COALESCE([FAT KG],0) else 0 end) as FATKG " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then COALESCE([SNF KG],0) else 0 end) as SNFKG " &
 ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Quantity] else 0 end) as SchemeQty " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Sale Amount] else 0 end) as SchemeAmt " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then COALESCE([FAT KG],0) else 0 end) as SchemeFATKG " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then COALESCE([SNF KG],0) else 0 end) as SchemeSNFKG " &
 ",sum(case when ISNULL(Sampling,'')='Y' then [Quantity] else 0 end) as SampleQty " &
  ",sum(case when ISNULL(Sampling,'')='Y' then [Sale Amount] else 0 end) SampleAmt " &
  ",sum(case when ISNULL(Sampling,'')='Y' then COALESCE([FAT KG],0) else 0 end) as SampleFATKG " &
  ",sum(case when ISNULL(Sampling,'')='Y' then COALESCE([SNF KG],0) else 0 end) as SampleSNFKG " &
 " from (  "
            Qry += clsPSInvoiceHead.GetReportDataQuery(obj)
            Qry += " ) as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code] " &
  ",[Item Name] ,  [UOM]  )TempData  left join tspl_item_master on tspl_item_master.Item_Code=TempData.[Item Code]  where isnull(TSPL_ITEM_MASTER.Is_Ambient,0)=1   " &
  " group by isnull(TSPL_ITEM_MASTER.Alies_Name,'') order by isnull(TSPL_ITEM_MASTER.Alies_Name,'')"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

            obj.Unit_Code = "Kg"

            Qry = "select isnull(TSPL_ITEM_MASTER.Alies_Name,'') as [Item] " &
 ",Sum(TempData.Qty) as [Quantity In Kg],Sum(TempData.SchemeQty) as [Scheme Quantity In Kg],Sum(TempData.SampleQty) as [Sample Quantity In Kg] " &
 ",sum(TempData.Amt) AS [Sale Amount],sum(TempData.SchemeAmt) AS [Scheme Sale Amount],sum(TempData.SampleAmt) AS [Sample Sale Amount] " &
 ",case when Sum(TempData.Qty)=0 then 0 else convert(decimal(18,2),sum(TempData.Amt)/Sum(TempData.Qty)) end AS [Ave Realisa Per Kg] " &
 ",case when Sum(TempData.SchemeQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SchemeAmt)/Sum(TempData.SchemeQty)) end AS [Scheme Ave Realisa Per Kg] " &
 ",case when Sum(TempData.SampleQty)=0 then 0 else convert(decimal(18,2),sum(TempData.SampleAmt)/Sum(TempData.SampleQty)) end AS [Sample Ave Realisa Per Kg] " &
 " from( " &
 " Select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name] " &
  ", SUM([Quantity]) [Quantity], MAX([UOM]) [UOM] ,sum(COALESCE([FAT KG],0)) as [Total FAT KG],sum(COALESCE([SNF KG],0)) as [Total SNF KG],sum(COALESCE([FAT KG],0))+sum(COALESCE([SNF KG],0)) as TSKG,Convert(decimal(18,2),sum([Sale Amount])) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount] " &
  ",sum([Total Tax Amount]) as [Total Tax Amount],Convert(decimal(18,2),sum([Total Amount] )) as [Total Amount] " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Quantity] else 0 end) as Qty " &
  ",sum(case when (ISNULL(Sampling,'')='N' and ISNULL([Scheme Type],'')='N') then [Sale Amount] else 0 end) Amt " &
   ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Quantity] else 0 end) as SchemeQty " &
  ",sum(case when (ISNULL([Scheme Type],'')='Y' and ISNULL(Sampling,'')='N') then [Sale Amount] else 0 end) as SchemeAmt " &
   ",sum(case when ISNULL(Sampling,'')='Y' then [Quantity] else 0 end) as SampleQty " &
  ",sum(case when ISNULL(Sampling,'')='Y' then [Sale Amount] else 0 end) SampleAmt " &
 " from (  "
            Qry += clsPSInvoiceHead.GetReportDataQuery(obj)
            Qry += " ) as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code] " &
  ",[Item Name] ,  [UOM]  )TempData  left join tspl_item_master on tspl_item_master.Item_Code=TempData.[Item Code]  where isnull(TSPL_ITEM_MASTER.Is_Ambient,0)=1   " &
  " group by isnull(TSPL_ITEM_MASTER.Alies_Name,'') order by isnull(TSPL_ITEM_MASTER.Alies_Name,'')"


            Dim dtKg As DataTable = clsDBFuncationality.GetDataTable(Qry)

            Dim selectedColumns As String() = {"Item", "Quantity In Kg", "Scheme Quantity In Kg", "Sample Quantity In Kg", "Ave Realisa Per Kg", "Scheme Ave Realisa Per Kg", "Sample Ave Realisa Per Kg"}
            Dim dtSelectKg As DataTable = New DataView(dtKg).ToTable(False, selectedColumns)

            Dim dtAll As DataTable = New DataTable()
            dtAll.Merge(dt)
            dtAll.Columns.Add(New DataColumn("Quantity In Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Scheme Quantity In Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Sample Quantity In Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Ave Realisa Per Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Scheme Ave Realisa Per Kg", System.Type.GetType("System.Decimal")))
            dtAll.Columns.Add(New DataColumn("Sample Ave Realisa Per Kg", System.Type.GetType("System.Decimal")))

            For jrow As Integer = 0 To dtSelectKg.Rows.Count - 1
                If clsCommon.myCstr(dtAll.Rows(jrow)("Item")) = clsCommon.myCstr(dtSelectKg.Rows(jrow)("Item")) Then
                    dtAll.Rows(jrow)("Quantity In Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Quantity In Kg"))
                    dtAll.Rows(jrow)("Scheme Quantity In Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Scheme Quantity In Kg"))
                    dtAll.Rows(jrow)("Sample Quantity In Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Sample Quantity In Kg"))
                    dtAll.Rows(jrow)("Ave Realisa Per Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Ave Realisa Per Kg"))
                    dtAll.Rows(jrow)("Scheme Ave Realisa Per Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Scheme Ave Realisa Per Kg"))
                    dtAll.Rows(jrow)("Sample Ave Realisa Per Kg") = clsCommon.myCdbl(dtSelectKg.Rows(jrow)("Sample Ave Realisa Per Kg"))
                End If
            Next


            dtAll.Columns("Item").SetOrdinal(0)
            dtAll.Columns("Quantity In Ltr").SetOrdinal(1)
            dtAll.Columns("Scheme Quantity In Ltr").SetOrdinal(2)
            dtAll.Columns("Sample Quantity In Ltr").SetOrdinal(3)

            dtAll.Columns("Quantity In Kg").SetOrdinal(4)
            dtAll.Columns("Scheme Quantity In Kg").SetOrdinal(5)
            dtAll.Columns("Sample Quantity In Kg").SetOrdinal(6)

            dtAll.Columns("FAT KG").SetOrdinal(7)
            dtAll.Columns("Scheme FAT KG").SetOrdinal(8)
            dtAll.Columns("Sample FAT KG").SetOrdinal(9)

            dtAll.Columns("SNF KG").SetOrdinal(10)
            dtAll.Columns("Scheme SNF KG").SetOrdinal(11)
            dtAll.Columns("Sample SNF KG").SetOrdinal(12)

            dtAll.Columns("Sale Amount").SetOrdinal(13)
            dtAll.Columns("Scheme Sale Amount").SetOrdinal(14)
            dtAll.Columns("Sample Sale Amount").SetOrdinal(15)

            dtAll.Columns("Ave Realisa Per Ltr").SetOrdinal(16)
            dtAll.Columns("Scheme Ave Realisa Per Ltr").SetOrdinal(17)
            dtAll.Columns("Sample Ave Realisa Per Ltr").SetOrdinal(18)

            dtAll.Columns("Ave Realisa Per Kg").SetOrdinal(19)
            dtAll.Columns("Scheme Ave Realisa Per Kg").SetOrdinal(20)
            dtAll.Columns("Sample Ave Realisa Per Kg").SetOrdinal(21)

            gv_ProductSale.DataSource = Nothing

            If dtAll IsNot Nothing OrElse dtAll.Rows.Count > 0 Then
                gv_ProductSale.Rows.Clear()
                gv_ProductSale.Columns.Clear()
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

                If False Then
                    Dim barSeries As New Telerik.WinControls.UI.BarSeries("Ave Realisa Per Kg", "Item")
                    barSeries.BackColor = Color.Olive
                    Me.RadChartViewProductSale.Series.Add(barSeries)
                    barSeries.DataSource = dtAll
                    barSeries.HorizontalAxis.LabelFitMode = AxisLabelFitMode.Rotate
                    barSeries.VerticalAxis.LabelInterval = 4
                    Me.RadChartViewProductSale.LegendTitle = "Ave Realisa Per Kg"
                End If
            Else
                'clsCommon.MyMessageBoxShow("Record Not Found.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadTransportCharges()
        Try

            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "Transport_cost"
            Dim Qry As String = ""

            Qry = "select isnull(XX.Zone_Code,'') + '-' + ISNULL(XX.Route_No,'')+ '-' +ISNULL(XX.Vehicle_Brand,'') as [Zone-Route-Vehicle] ,MAX(XX.[Vehicle Capacity])  as [Vehicle Capacity]  ,round(sum(XX.[Vehicle Out Crate])/count(DISTINCT XX.[Gate Pass No]),2) as [Vehicle Capacity Utilized] ,round(sum(XX.[Sales Milk/Ltr]),2) as [Sales In LTR] ,round(sum(XX.[Sales Value]),2) as [Sales Value]   " &
               ",max([Fixed KM]) as [Fixed KM],convert(decimal(18,2),sum(XX.[Freight Amount])) as [Freight Amount],sum(XX.[Running KM]) as [Running KM] " &
               " ,(case when sum(XX.[Running KM])>0 then round(sum(XX.[Freight Amount])/sum(XX.[Running KM]),2) else 0 end) as [Rate Per KM]" &
               " from ( " &
               " select ISNULL(TSPL_SD_SHIPMENT_HEAD.Zone_Code,'') as Zone_Code,ISNULL(TSPL_VEHICLE_MASTER.Vehicle_Brand,'') as Vehicle_Brand ,ISNULL(TSPL_VEHICLE_MASTER.Vehicle_Id,'') as Vehicle_Id ,ISNULL(TSPL_ROUTE_MASTER.Route_No,'') AS Route_No ,TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode as [Gate Pass No]  ,(case when isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0)>0 then isnull (TSPL_DAIRYSALE_GATEPASS_MASTER.Closing_Km,0) -isnull( Opening_Km,0)  else 0 end) as [Running KM] ,TSPL_PROVISION_ENTRY.Amount as [Freight Amount],isnull(TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount,0) as [Sales Value]  " &
               " , TBL_LTR_CONV.Ltr_Qty as [Sales Milk/Ltr], TSPL_VEHICLE_MASTER.CrateCapacity as [Vehicle Capacity]  ,TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate as [Vehicle Out Crate],TSPL_DAIRYSALE_GATEPASS_MASTER.Distance_In_Route as [Fixed KM]   " &
               " from TSPL_PROVISION_ENTRY    " &
               " inner join TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_PROVISION_ENTRY.Ref_Doc_No = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode   " &
               "  left join (SELECT SUM(isnull(TSPL_SD_SHIPMENT_HEAD.Amount_Less_Discount,0)) AS Amount_Less_Discount,TSPL_SD_SHIPMENT_HEAD.GPCode  " &
                ",max(tspl_customer_master.Zone_Code) as Zone_Code  " &
                " FROM TSPL_SD_SHIPMENT_HEAD   " &
                " Left Join tspl_customer_master on tspl_customer_master.Cust_Code=TSPL_SD_SHIPMENT_HEAD.Customer_Code  " &
                " where isnull(TSPL_SD_SHIPMENT_HEAD.GPCode,'')<>''  " &
                " Group BY TSPL_SD_SHIPMENT_HEAD.GPCode) TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.GPCode=TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode" &
                 "  left Outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.Vehicle_id = TSPL_DAIRYSALE_GATEPASS_MASTER.Vehicle_Id   left Outer Join TSPL_ROUTE_MASTER on  TSPL_ROUTE_MASTER.Route_No = TSPL_PROVISION_ENTRY.Route_Code " &
               " left outer  join  ( select TSPL_DAIRYSALE_GATEPASS_Detail.GPCode ,sum (convert(decimal(18,2),(TSPL_DAIRYSALE_GATEPASS_Detail.qty/LtrUnit.conversion_factor)*StockUnit.conversion_factor*CurrentUnit.conversion_factor)) as Ltr_Qty from TSPL_DAIRYSALE_GATEPASS_MASTER  left join TSPL_DAIRYSALE_GATEPASS_Detail on TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode=TSPL_DAIRYSALE_GATEPASS_Detail.GPCode  left join tspl_item_uom_detail LtrUnit on LtrUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code   " &
               " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code=LtrUnit.uom_code  left join tspl_item_uom_detail StockUnit on StockUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code   " &
               " left join tspl_item_uom_detail CurrentUnit on CurrentUnit.item_code=TSPL_DAIRYSALE_GATEPASS_Detail.item_code and 	CurrentUnit.uom_code=	TSPL_DAIRYSALE_GATEPASS_Detail.unit_code  where  tspl_unit_master.Ltr_type ='Y' and StockUnit.stocking_unit='Y'  group by TSPL_DAIRYSALE_GATEPASS_Detail.GPCode ) TBL_LTR_CONV on  TBL_LTR_CONV.GPCode = TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode  where TSPL_PROVISION_ENTRY.isPosted=1 and 2=2    "
            Qry += " and convert(date, TSPL_PROVISION_ENTRY.Doc_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, TSPL_PROVISION_ENTRY.Doc_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += ")XX group by  " &
               " isnull(XX.Zone_Code,'') + '-' + ISNULL(XX.Route_No,'')+ '-' +ISNULL(XX.Vehicle_Brand,'')"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)





            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gvTransportcost.DataSource = Nothing
                gvTransportcost.Rows.Clear()
                gvTransportcost.Columns.Clear()
                gvTransportcost.DataSource = dt
                gvTransportcost.GroupDescriptors.Clear()
                gvTransportcost.MasterTemplate.SummaryRowsBottom.Clear()
                gvTransportcost.MasterTemplate.BestFitColumns()
                gvTransportcost.EnableFiltering = True
                For i As Integer = 0 To gvTransportcost.Columns.Count - 1
                    gvTransportcost.Columns(i).BestFit()
                Next

                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim SalesInLTR As New GridViewSummaryItem("Sales In LTR", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesInLTR)
                Dim SalesValue As New GridViewSummaryItem("Sales Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(SalesValue)
                Dim FreightAmount As New GridViewSummaryItem("Freight Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(FreightAmount)

                gvTransportcost.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvTransportcost.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            Else
                clsCommon.MyMessageBoxShow(Me, "Record Not Found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadStore()
        Try
            PageSetupReport_ID = clsUserMgtCode.frmDasboard + "StoreReport"
            Dim Qry As String = ""
            Qry = "select ROW_NUMBER () over (order by ass1.Structure_Desc ) As SNo,ass1.Structure_Desc As [Item Group] " &
                ",COUNT(DISTINCT ass1.purchase_no) as [No of PO] " &
                ",COUNT(DISTINCT ass1.GRN_NO) as [No of GRN] " &
                ",COUNT(DISTINCT ass1.SRN_NO) as [No of SRN] " &
                ",Cast(sum(value) as decimal(18,2)) as Value " &
                ",COUNT(DISTINCT CASE WHEN POStatus='Open' then ass1.purchase_no else null end) as [Pending PO] " &
            " from  ( SELECT  PO.PurchaseOrder_No AS purchase_no,PO.PurchaseOrder_Date AS po_date ," &
             " CASE WHEN (PO.close_yn) = 'Y' THEN 'Close' WHEN (PO.status) = 1 THEN 'Close' when QC.Document_Code is not null and TSPL_SRN_DETAIL.SRN_No is null  and ISNULL( QC.Ok_Qty,0)=0 and isnull(QC.Reject_Qty,0)>0 then 'Open'  WHEN ((ISNULL(PO.PurchaseOrder_Qty, 0) + ISNULL(Tolerence_Qty, 0)) - ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Short_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Leak_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Burst_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Rejected_Qty, 0)) > 0 THEN 'Open'  ELSE 'Complete' END AS POStatus " &
              " ,  TSPL_SRN_DETAIL.SRN_NO AS SRN_NO " &
              " ,ISNULL(TSPL_GRN_DETAIL.GRN_NO, '') AS GRN_NO,CONVERT(varchar, TSPL_GRN_HEAD.GRN_Date, 103) AS GRN_Date,PO.Against_Requisition, TSPL_REQUISITION_HEAD.Requisition_Date,ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0) AS GRNQty,ISNULL(TSPL_GRN_DETAIL.Tolerence_Qty, 0) AS Tolerence_Qty,ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0) AS MRNQty, ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0) AS SRN_Qty,ISNULL(TSPL_SRN_DETAIL.Short_Qty, 0) AS Short_Qty,ISNULL(TSPL_SRN_DETAIL.Leak_Qty, 0) AS Leak_Qty,ISNULL(TSPL_SRN_DETAIL.Burst_Qty, 0) AS Burst_Qty, ISNULL(TSPL_SRN_DETAIL.Rejected_Qty, 0) AS Rejected_Qty " &
            " , CASE WHEN COALESCE((ISNULL(ISNULL(TSPL_SRN_DETAIL.SRN_Qty, 0), 0.0)), 0) <= 0 THEN COALESCE((SELECT (ISNULL(TSPL_MRN_DETAIL.MRN_Qty, 0))), (ISNULL((ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0)), 0.0))) END AS QCQty,TSPL_SRN_DETAIL.Balance_Qty,TSPL_ITEM_MASTER.Structure_Desc,PO.value" &
            " ,CASE WHEN (PO.close_yn) = 'Y' THEN 'Close' WHEN (PO.status) = 1 THEN 'Close' when QC.Document_Code is not null and TSPL_SRN_DETAIL.SRN_No is null  and ISNULL( QC.Ok_Qty,0)=0 and isnull(QC.Reject_Qty,0)>0 then 'Open' " &
            " WHEN ((ISNULL(PO.PurchaseOrder_Qty, 0) + ISNULL(Tolerence_Qty, 0)) - ISNULL(TSPL_GRN_DETAIL.GRN_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Short_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Leak_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Burst_Qty, 0) + ISNULL(TSPL_SRN_DETAIL.Rejected_Qty, 0)) > 0 THEN 'Open'  ELSE 'Complete' END AS PPStatus FROM( " &
            " select ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, '') AS PurchaseOrder_No ,tspl_purchase_order_detail.Item_Code,MAX(TSPL_PURCHASE_ORDER_HEAD.comp_code) AS comp_code,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date) AS PurchaseOrder_Date ,MAX(TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Type) AS PurchaseOrder_Type,MAX(TSPL_PURCHASE_ORDER_HEAD.Closed_By) AS Closed_By ,MAX(TSPL_PURCHASE_ORDER_HEAD.Closed_Date) AS Closed_Date,MAX(TSPL_PURCHASE_ORDER_HEAD.Vendor_Code) AS Vendor_Code ,MAX(TSPL_PURCHASE_ORDER_HEAD.Vendor_Name) AS Vendor_Name,SUM(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty) AS PurchaseOrder_Qty ,MAX(TSPL_PURCHASE_ORDER_HEAD.close_yn) AS close_yn,MAX(TSPL_PURCHASE_ORDER_DETAIL.status) AS status " &
             " ,MAX(tspl_purchase_order_head.Against_Requisition) AS Against_Requisition ,MAX(TSPL_PURCHASE_ORDER_DETAIL.Item_Cost)*SUM(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_Qty) AS Value" &
              " from tspl_purchase_order_detail LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No GROUP BY ISNULL(TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No, ''),tspl_purchase_order_detail.Item_Code)PO  LEFT JOIN TSPL_GRN_DETAIL ON ISNULL(TSPL_GRN_DETAIL.PO_Id, '') = ISNULL(PO.PurchaseOrder_No, '') AND TSPL_GRN_DETAIL.Item_Code = PO.Item_Code LEFT JOIN TSPL_GRN_HEAD ON ISNULL(TSPL_GRN_HEAD.grn_no, '') = ISNULL(TSPL_GRN_DETAIL.grn_no, '') LEFT JOIN TSPL_MRN_DETAIL ON TSPL_MRN_DETAIL.Item_Code = PO.Item_Code AND ISNULL(TSPL_MRN_DETAIL.GRN_Id, '') = ISNULL(TSPL_GRN_DETAIL.grn_no, '') AND ISNULL(TSPL_GRN_DETAIL.PO_Id, '') = ISNULL(PO.PurchaseOrder_No, '') AND TSPL_GRN_DETAIL.Item_Code = PO.Item_Code left outer join (select TSPL_QC_CHECK_DETAIL.*  from TSPL_QC_CHECK_DETAIL left outer join  TSPL_QC_CHECK_HEAD on TSPL_QC_CHECK_HEAD.Document_Code = TSPL_QC_CHECK_DETAIL.Document_Code where TSPL_QC_CHECK_HEAD.Posted=1) as  QC on isnull( QC.MRN_No,'')=ISNULL( TSPL_MRN_DETAIL.MRN_No,'') and QC.Item_Code = TSPL_MRN_DETAIL.Item_Code  and ISNULL(QC.PO_No, '')=ISNULL(TSPL_MRN_DETAIL.PO_ID, '')  LEFT JOIN TSPL_SRN_DETAIL ON TSPL_SRN_DETAIL.Item_Code = PO.Item_Code AND ISNULL(PO.PurchaseOrder_No, '') = ISNULL(TSPL_SRN_DETAIL.PO_ID, '') AND ISNULL(TSPL_MRN_DETAIL.mrn_no, '') = ISNULL(TSPL_SRN_DETAIL.mrn_id, '') AND ISNULL(TSPL_GRN_DETAIL.grn_no, '') = ISNULL(TSPL_MRN_DETAIL.grn_id, '') LEFT JOIN TSPL_SRN_HEAD ON TSPL_SRN_HEAD.SRN_No = TSPL_SRN_DETAIL.SRN_no" &
               " LEFT JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = PO.Item_Code LEFT OUTER JOIN TSPL_ITEM_MASTER_CATEGORY ON TSPL_ITEM_MASTER_CATEGORY.item_code = TSPL_ITEM_MASTER.item_code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL ON TSPL_ITEM_CATEGORY_LEVEL.item_category_code = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code LEFT OUTER JOIN TSPL_ITEM_CATEGORY_LEVEL_VALUES ON TSPL_ITEM_CATEGORY_LEVEL_VALUES.item_category_code = TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code AND TSPL_ITEM_CATEGORY_LEVEL_VALUES.code = TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values LEFT OUTER JOIN TSPL_REQUISITION_HEAD ON ISNULL(PO.Against_Requisition, '') = ISNULL(TSPL_REQUISITION_HEAD.Requisition_Id, '')  WHERE 2 = 2 "
            Qry += " and convert(date, PO.PurchaseOrder_Date,103) >=  convert(date,'" + txtFromDate.Value + "',103)  and  convert(date, PO.PurchaseOrder_Date,103) <= convert(date,'" + txtToDate.Value + "',103) "
            Qry += " and PO.PurchaseOrder_Type <>  'J' " &
           " )ass1 where 2=2 GROUP BY ass1.Structure_Desc "



            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_po.DataSource = Nothing
                gv_po.Rows.Clear()
                gv_po.Columns.Clear()
                gv_po.DataSource = dt
                gv_po.GroupDescriptors.Clear()
                gv_po.MasterTemplate.SummaryRowsBottom.Clear()
                gv_po.MasterTemplate.BestFitColumns()
                gv_po.EnableFiltering = True
                For i As Integer = 0 To gv_po.Columns.Count - 1
                    gv_po.Columns(i).BestFit()
                Next

                Dim summaryRowItem As New GridViewSummaryRowItem()

                Dim Values As New GridViewSummaryItem("Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(Values)

                gv_po.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv_po.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If


            Dim StartDate As Date = CDate(txtFromDate.Value)
            Dim EndDate As Date = CDate(txtToDate.Value)

            Qry = "select * from ( select TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_MASTER.Product_Type,InventroyMovement.Trans_Type,InventroyMovement.Punching_Date, InventroyMovement.InOut,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq, (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code" &
                ",TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation ,InventroyMovement.IS_CONSUMPTION" &
                " from (  select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,IS_CONSUMPTION from TSPL_INVENTORY_MOVEMENT" &
                " union all " &
                " select Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM,MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,IS_CONSUMPTION from TSPL_INVENTORY_MOVEMENT_NEW ) InventroyMovement " &
               " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code " &
                 " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code " &
                 " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code " &
                 " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code  " &
                 " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end)  " &
                 " left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG' left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type  left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No  " &
                 " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account   left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code  Where 2=2  and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'  ) xxxxx  where 2=2 "
            Dim InnerOpClo As String = String.Empty
            InnerOpClo = " SUM(Cost  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [OPBalCost]  , " &
                           " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else 0 end))  AS RecdCost , " &
                           " SUM(Cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * (case when InOut='I' then 0 else 1 end))  AS IssueCost," &
                           " SUM(cost * (CASE WHEN PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm:ss tt") + "'  THEN 1 ELSE 0 end) * (case when InOut='I' then 1 else -1 end))  AS [Cost], " &
                           " SUM(cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm:ss tt") + "' AND InOut='O' AND IS_CONSUMPTION=1 AND Trans_Type in ('PROD_ENTRY','PP_STD-FQC') and Item_Type in ('O','R') and isnull(Product_Type,'')='' THEN 1 ELSE 0 end) )  AS [Consumption], " &
                           " SUM(cost * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(StartDate), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(EndDate), "dd/MMM/yyyy hh:mm:ss tt") + "' AND InOut='I' and Item_Type in ('F')  THEN 1 ELSE 0 end) )  AS [Total FG Products] "


            Dim strFinalQry As String = ""
            strFinalQry = "select  Structure_Descq as Structure,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME as [Item Type],OPBalCost as [Opening Stock Value], RecdCost as [Purchase],IssueCost as [Issues],Cost as [Closing Value],Consumption "
            'strFinalQry += OuterOpClo
            strFinalQry += " from (" + Environment.NewLine
            strFinalQry += " select Structure_Code,Item_Type,max(Structure_Descq) as Structure_Descq,"
            strFinalQry += InnerOpClo
            strFinalQry += "  from (" + Qry + ")xxx Group by Structure_Code,Item_Type )xxxx left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE=xxxx.Item_Type where (TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE <> '' or Structure_Code <> '') order by TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME "


            dt = clsDBFuncationality.GetDataTable(strFinalQry)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then
                gv_store.DataSource = Nothing
                gv_store.Rows.Clear()
                gv_store.Columns.Clear()
                gv_store.DataSource = dt
                gv_store.GroupDescriptors.Clear()
                gv_store.MasterTemplate.SummaryRowsBottom.Clear()
                gv_store.MasterTemplate.BestFitColumns()
                gv_store.EnableFiltering = True
                For i As Integer = 0 To gv_store.Columns.Count - 1
                    gv_store.Columns(i).BestFit()
                Next

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

            If gv_po.Rows.Count() = 0 AndAlso gv_store.Rows.Count() = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Record Not Found.", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function GetQueryMassBalance(ByVal isSFG As Boolean, ByVal strAlpha As String, ByVal strParticular As String, ByVal isCheckParticular As Boolean, ByRef strTotalInput As String, ByRef strTotalOutput As String) As String
        Dim BaseQry As String = ""
        Dim qry As String = ""
        Dim strInput As String = ""
        strTotalInput = ""
        Dim strOutput As String = ""
        strTotalOutput = ""
        Dim strOpening As String = ""
        Dim strCommonColumn As String = "cast( sum(QtyKg*RI) as decimal(18,2)) as QtyKg,cast(sum(QtyLtr*RI)as decimal(18,2)) as QtyLtr, case when sum(QtyKg*RI)=0 then 0 else cast( sum(Fat_KG*RI)*100/sum(QtyKg*RI)as decimal(18,2)) end as Fat_Per, case when sum(QtyKg*RI)=0 then 0 else CAST(sum(SNF_KG*RI)*100/sum(QtyKg*RI)as decimal(18,2)) end as SNF_Per  ,cast(sum(Fat_KG*RI)as decimal(18,2)) as Fat_KG  ,cast(sum(SNF_KG*RI)as decimal(18,2)) as SNF_KG,cast(sum(Avg_Cost*RI)as decimal(18,2)) as Avg_Cost"
        Dim strCommonColumnForAlpha As String = "Source_Doc_No,Punching_Date,Vendor_Code,Cust_Code,Trans_Type,Item_Code,Location_Code,Other_Location_Code, cast( (QtyKg*RI) as decimal(18,2)) as QtyKg,cast((QtyLtr*RI)as decimal(18,2)) as QtyLtr, case when (QtyKg*RI)=0 then 0 else cast( (Fat_KG*RI)*100/(QtyKg*RI)as decimal(18,2)) end as Fat_Per, case when (QtyKg*RI)=0 then 0 else CAST((SNF_KG*RI)*100/(QtyKg*RI)as decimal(18,2)) end as SNF_Per  ,cast((Fat_KG*RI)as decimal(18,2)) as Fat_KG  ,cast((SNF_KG*RI)as decimal(18,2)) as SNF_KG,cast((Avg_Cost*RI)as decimal(18,2)) as Avg_Cost "
        Dim strFromDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt")
        Dim strToDate As String = clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt")
        If isSFG Then
            BaseQry = " select TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date,TSPL_INVENTORY_MOVEMENT_NEW.Vendor_Code,TSPL_INVENTORY_MOVEMENT_NEW.Cust_Code,TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type,TSPL_ITEM_MASTER.Cheapter_Heads as Sub_item_category,TSPL_CHAPTER_HEAD.Description as Sub_item_category_Name, TSPL_INVENTORY_MOVEMENT_NEW.Item_Code,TSPL_INVENTORY_MOVEMENT_NEW.main_location, TSPL_INVENTORY_MOVEMENT_NEW.Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.Other_Location_Code,TSPL_INVENTORY_MOVEMENT_NEW.InOut,TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty,TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM,case when TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM='KG' then TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty else TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end as QtyKg,case when TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM='LTR' then TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty else TSPL_INVENTORY_MOVEMENT_NEW.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end as QtyLtr  ,TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per,TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG,TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per,TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG,TSPL_INVENTORY_MOVEMENT_NEW.Avg_Cost ,TSPL_INVENTORY_MOVEMENT_NEW.IS_CONSUMPTION" + Environment.NewLine +
        "from TSPL_INVENTORY_MOVEMENT_NEW" + Environment.NewLine +
        "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code" + Environment.NewLine +
        "left outer join TSPL_CHAPTER_HEAD on TSPL_CHAPTER_HEAD.Chapter_Head_Code=TSPL_ITEM_MASTER.Cheapter_Heads" + Environment.NewLine +
        "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT_NEW.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=(case when TSPL_INVENTORY_MOVEMENT_NEW.Stock_UOM='LTR' then 'KG' else 'LTR' end)" + Environment.NewLine +
        "where TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in ('MilkTransferIn','BulkSRN','JWSRNESTM','PP_STD-FQC','Assembly','DispatchBS', 'DisCanSale','IC-AD','PP_ISSUE')"

            strOpening = "select '#$Alpha$#' as Alpha,'#$Trans$#' as Trans,xx.Item_Code as ParticularCode,max(TSPL_ITEM_MASTER.Item_Desc) as ParticularName," + strCommonColumn + " from (" + Environment.NewLine +
        "select *,case when Trans_Type in ('MilkTransferIn','BulkSRN') then 1 " + Environment.NewLine +
        "else case when InOut='I' and ((Trans_Type='PP_STD-FQC' and IS_CONSUMPTION=0) or Trans_Type in ('Assembly','IC-AD','JWSRNESTM')) then 1 " + Environment.NewLine +
        "else case when InOut='O' and ((Trans_Type='PP_STD-FQC' and IS_CONSUMPTION=0) or Trans_Type in ('PP_ISSUE','DispatchBS', 'DisCanSale','JWSRNESTM')) then -1 else 0" + Environment.NewLine +
        "end end end  as RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date #$Punching_Date$# )x " + Environment.NewLine +
        ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code group by xx.Item_Code"



            strInput = strOpening.Replace("#$Alpha$#", "A").Replace("#$Trans$#", "Opening Balance").Replace("#$Punching_Date$#", " < '" + strFromDate + "'") + Environment.NewLine + "union all" + Environment.NewLine +
       "select 'B' as Alpha,'Own Tanker Receipt' as Trans,Other_Location_Code as ParticularCode,max(TSPL_LOCATION_MASTER.Location_Desc) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "B") = CompairStringResult.Equal Then
                qry = "select *,  1   RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + strToDate + "' and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='MilkTransferIn'  )x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Other_Location_Code='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strInput += qry + Environment.NewLine +
       ")xx left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Other_Location_Code" + Environment.NewLine +
       "group by Other_Location_Code" + Environment.NewLine +
       "union all" + Environment.NewLine +
       "select 'C' as Alpha,'Purchase Milk' as Trans,xx.Vendor_Code as ParticularCode,max(TSPL_VENDOR_MASTER.Vendor_Name) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "C") = CompairStringResult.Equal Then
                qry = "select *,  1   RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + strToDate + "' and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='BulkSRN' )x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Vendor_Code='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strInput += qry + Environment.NewLine +
        ")xx left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code" + Environment.NewLine +
       "group by xx.Vendor_Code" + Environment.NewLine +
         "union all" + Environment.NewLine +
       "select 'D' as Alpha,'Conversion Tankers' as Trans,xx.Location_Code as ParticularCode,max(TSPL_LOCATION_MASTER.Location_Desc) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "D") = CompairStringResult.Equal Then
                qry = "select *, case when InOut='I' then 1 else 0 end  RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + strToDate + "' and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='JWSRNESTM' )x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " where Location_Code='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strInput += qry + Environment.NewLine +
       ")xx left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location_Code" + Environment.NewLine +
       "group by xx.Location_Code" + Environment.NewLine +
       "union all" + Environment.NewLine +
       "select 'E' as Alpha,'Other In' as Trans,xx.Sub_item_category as ParticularCode,max(xx.Sub_item_category_Name) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "E") = CompairStringResult.Equal Then
                qry = "select *, case when InOut='I' then 1 else 0 end  RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + strToDate + "' and (TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in ('Assembly','IC-AD')) )x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Sub_item_category='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strInput += qry + Environment.NewLine +
  ")xx  " + Environment.NewLine +
  "group by xx.Sub_item_category" + Environment.NewLine

            strTotalInput = "select 'F' as Alpha,'' as Trans,'' as ParticularCode,'Total Input' as ParticularName," + strCommonColumn + " from (" + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "F") = CompairStringResult.Equal Then
                qry = "select *,1 as RI from ( " + Environment.NewLine + strInput + Environment.NewLine + ")xxx" + Environment.NewLine
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strTotalInput += qry + Environment.NewLine +
            ")xx"

            strOutput = "select 'G' as Alpha,'Production' as Trans,xx.Sub_item_category as ParticularCode,max(xx.Sub_item_category_Name) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "G") = CompairStringResult.Equal Then
                qry = "select *, case when InOut='I' then 1 else 0 end  RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + strToDate + "' and ((TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='PP_STD-FQC' and IS_CONSUMPTION=0)) )x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Sub_item_category='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strOutput += qry + Environment.NewLine +
        ")xx  " + Environment.NewLine +
        "group by xx.Sub_item_category" + Environment.NewLine +
        "union all" + Environment.NewLine +
        "select 'H' as Alpha,'Production Issue' as Trans,xx.Sub_item_category as ParticularCode,max(xx.Sub_item_category_Name) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "H") = CompairStringResult.Equal Then
                qry = " select *, case when InOut='O' then 1 else 0 end  RI    from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + strToDate + "' and ((TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='PP_STD-FQC' and IS_CONSUMPTION=0) or TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='PP_ISSUE') )x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Sub_item_category='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strOutput += qry + Environment.NewLine +
   ")xx  " + Environment.NewLine +
   "group by xx.Sub_item_category" + Environment.NewLine +
   "union all" + Environment.NewLine +
   "select 'I' as Alpha,'Sale' as Trans,xx.Sub_item_category as ParticularCode,max(xx.Sub_item_category_Name) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "I") = CompairStringResult.Equal Then
                qry = "select *, case when InOut='O' then 1 else 0 end  RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date<='" + strToDate + "' and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type in ('DispatchBS', 'DisCanSale') )x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Sub_item_category='" + strParticular + "'"
                End If

                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strOutput += qry + Environment.NewLine +
   ")xx  " + Environment.NewLine +
   "group by xx.Sub_item_category"
            strOutput += Environment.NewLine + " Union all " + Environment.NewLine + strOpening.Replace("#$Alpha$#", "J").Replace("#$Trans$#", "Closing Balance").Replace("#$Punching_Date$#", " <= '" + strToDate + "'")

            strTotalOutput = "select 'K' as Alpha,'' as Trans,'' as ParticularCode,'Total Output' as ParticularName," + strCommonColumn + " from (" + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "K") = CompairStringResult.Equal Then
                qry = " Select *,1 As RI from (" + Environment.NewLine + strOutput + Environment.NewLine + ")xxx" + Environment.NewLine
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strTotalOutput += qry + Environment.NewLine +
            ")xx"

            qry = strInput + Environment.NewLine + " Union all " + Environment.NewLine + strTotalInput + Environment.NewLine + " Union all  " + Environment.NewLine + strOutput + Environment.NewLine + " Union all  " + Environment.NewLine + strTotalOutput


        Else
            BaseQry = "select TSPL_INVENTORY_MOVEMENT.*,cast( ((QtyKg*Fat_Per) / 100) as decimal(18,3)) as Fat_KG ,cast( ((QtyKg*SNF_Per) / 100 )as decimal(18,3)) as Snf_KG from (select TSPL_INVENTORY_MOVEMENT.Source_Doc_No,TSPL_INVENTORY_MOVEMENT.Punching_Date,TSPL_INVENTORY_MOVEMENT.Vendor_Code,TSPL_INVENTORY_MOVEMENT.Cust_Code,TSPL_INVENTORY_MOVEMENT.Trans_Type,TSPL_INVENTORY_MOVEMENT.Item_Code,TSPL_INVENTORY_MOVEMENT.Location_Code,TSPL_INVENTORY_MOVEMENT.Other_Location_Code,TSPL_INVENTORY_MOVEMENT.InOut,TSPL_INVENTORY_MOVEMENT.Stock_Qty,TSPL_INVENTORY_MOVEMENT.Stock_UOM,case when TSPL_INVENTORY_MOVEMENT.Stock_UOM='KG' then TSPL_INVENTORY_MOVEMENT.Stock_Qty else TSPL_INVENTORY_MOVEMENT.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end as QtyKg,case when TSPL_INVENTORY_MOVEMENT.Stock_UOM='LTR' then TSPL_INVENTORY_MOVEMENT.Stock_Qty else TSPL_INVENTORY_MOVEMENT.Stock_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor end as QtyLtr  ,isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=TSPL_INVENTORY_MOVEMENT.Item_Code and Type='FAT'),0) as Fat_Per,isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=TSPL_INVENTORY_MOVEMENT.Item_Code and Type='SNF'),0) as SNF_Per, TSPL_INVENTORY_MOVEMENT.Avg_Cost ," + Environment.NewLine +
            "case when TSPL_SD_SHIPMENT_HEAD.IsSampling=1 then 'Y' else TSPL_INVENTORY_MOVEMENT.Is_Scheme_Item end as Is_Scheme_Item" + Environment.NewLine +
            "from TSPL_INVENTORY_MOVEMENT " + Environment.NewLine +
            "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code" + Environment.NewLine +
            "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INVENTORY_MOVEMENT.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=(case when TSPL_INVENTORY_MOVEMENT.Stock_UOM='LTR' then 'KG' else 'LTR' end)" + Environment.NewLine +
            "left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.Document_Code=TSPL_INVENTORY_MOVEMENT.Source_Doc_No and TSPL_INVENTORY_MOVEMENT.Trans_Type ='FS-SH'" + Environment.NewLine +
            "where TSPL_ITEM_MASTER.Product_Type='MP')TSPL_INVENTORY_MOVEMENT where 2=2 "

            strOpening = "select '#$Alpha$#' as Alpha,'#$Trans$#' as Trans,xx.Item_Code as ParticularCode,max(TSPL_ITEM_MASTER.Item_Desc) as ParticularName," + strCommonColumn + " from (" + Environment.NewLine +
       "select *,case when InOut='I' then 1 else -1  end  as RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT.Punching_Date #$Punching_Date$# )x " + Environment.NewLine +
       ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code group by xx.Item_Code"

            strInput = strOpening.Replace("#$Alpha$#", "A").Replace("#$Trans$#", "Opening Balance").Replace("#$Punching_Date$#", " < '" + strFromDate + "'") + Environment.NewLine + "union all" + Environment.NewLine +
       "select 'B' as Alpha,'Production' as Trans,xx.Item_Code as ParticularCode,max(TSPL_ITEM_MASTER.Item_Desc) as ParticularName, " + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "B") = CompairStringResult.Equal Then
                qry = "select *,  1   RI  from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + strToDate + "' and InOut='I')x " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Item_Code='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strInput += qry + Environment.NewLine +
       ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code" + Environment.NewLine +
       "group by xx.Item_Code" + Environment.NewLine

            strTotalInput = "select 'C' as Alpha,'' as Trans,'' as ParticularCode,'Total Input' as ParticularName," + strCommonColumn + " from (" + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "C") = CompairStringResult.Equal Then
                qry = "select *,1 as RI from ( " + Environment.NewLine + strInput + Environment.NewLine + ")xxx" + Environment.NewLine
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strTotalInput += qry + Environment.NewLine +
                ")xx"

            strOutput = "select 'D' as Alpha,'Market Sale/Dispatch' as Trans,xx.Item_Code as ParticularCode,max(TSPL_ITEM_MASTER.Item_Desc) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "D") = CompairStringResult.Equal Then
                qry = "select *, 1 as RI from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + strToDate + "' and InOut='O' )x where  Is_Scheme_Item='N' " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Item_Code='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strOutput += qry + Environment.NewLine +
      ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code " + Environment.NewLine +
   "group by xx.Item_Code" + Environment.NewLine +
   "union all" + Environment.NewLine +
    "select 'E' as Alpha,'Free Sample' as Trans,xx.Item_Code as ParticularCode,max(TSPL_ITEM_MASTER.Item_Desc) as ParticularName," + strCommonColumn + " from ( " + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "B") = CompairStringResult.Equal Then
                qry = "select *, 1 as RI from (" + Environment.NewLine + BaseQry + Environment.NewLine + " and TSPL_INVENTORY_MOVEMENT.Punching_Date>='" + strFromDate + "' and  TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + strToDate + "' and InOut='O' )x where  Is_Scheme_Item='Y' " + Environment.NewLine
                If isCheckParticular Then
                    qry += " Where Item_Code='" + strParticular + "'"
                End If
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strOutput += qry + Environment.NewLine +
   ")xx left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code " + Environment.NewLine +
   "group by xx.Item_Code" + Environment.NewLine +
   " Union all " + Environment.NewLine +
   strOpening.Replace("#$Alpha$#", "F").Replace("#$Trans$#", "Closing Balance").Replace("#$Punching_Date$#", " <= '" + strToDate + "'")

            strTotalOutput = "select 'G' as Alpha,'' as Trans,'' as ParticularCode,'Total Output' as ParticularName," + strCommonColumn + " from (" + Environment.NewLine
            qry = ""
            If clsCommon.myLen(strAlpha) <= 0 OrElse clsCommon.CompairString(strAlpha, "G") = CompairStringResult.Equal Then
                qry = "select *,1 as RI from (" + Environment.NewLine + strOutput + Environment.NewLine + ")xxx" + Environment.NewLine
                If clsCommon.myLen(strAlpha) > 0 Then
                    Return "select " + strCommonColumnForAlpha + " from (" + qry + " )xxx"
                End If
            End If
            strTotalOutput += qry + Environment.NewLine +
           ")xx"

            qry = strInput + Environment.NewLine + " Union all " + Environment.NewLine + strTotalInput + Environment.NewLine + " Union all " + Environment.NewLine +
        strOutput + Environment.NewLine +
       " Union all  " + Environment.NewLine + strTotalOutput
        End If
        Return qry
    End Function

    Sub LoadMassBalance(ByVal isSFG As Boolean, ByVal gv1 As RadGridView)
        Try
            Dim strTotalInput As String = ""
            Dim strTotalOutput As String = ""
            Dim qry As String = GetQueryMassBalance(isSFG, "", "", False, strTotalInput, strTotalOutput)
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


                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.DataSource = dt
                gv1.GroupDescriptors.Clear()
                gv1.ShowGroupPanel = False
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.EnableFiltering = False
                RadPageView1.SelectedPage = RadPageViewPage2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                    gv1.Columns(ii).IsVisible = True
                Next
                gv1.BestFitColumns()

                gv1.Columns("Alpha").HeaderText = "Alpha"
                'If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
                '    gv1.Columns("ParticularCode").HeaderText = "Particular Code"
                '    gv1.Columns("ParticularName").HeaderText = "Particular"
                'End If
                gv1.Columns("QtyKg").HeaderText = "Qty KG"
                gv1.Columns("QtyLtr").HeaderText = "Qty Ltr"
                gv1.Columns("Fat_Per").HeaderText = "FAT %"
                gv1.Columns("SNF_Per").HeaderText = "SNF %"
                gv1.Columns("Fat_KG").HeaderText = "FAT Kg"
                gv1.Columns("SNF_KG").HeaderText = "SNF Kg"
                gv1.Columns("Avg_Cost").HeaderText = "Amount"

                'Else
                '    clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
            End If
            gv1.BestFitColumns()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvFGMassBalance_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvFGMassBalance.CellDoubleClick
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSFGMassBalance_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvSFGMassBalance.CellDoubleClick
        Try

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenMass()
        Try
            'If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Detail") = CompairStringResult.Equal Then
            '    If Not (clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans").Value), "Opening Balance") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(Gv1.CurrentRow.Cells("Trans").Value), "Closing Balance") = CompairStringResult.Equal) AndAlso clsCommon.myLen(Gv1.CurrentRow.Cells("Trans").Value) > 0 Then
            '        Dim qry As String = GetQuery(clsCommon.myCstr(Gv1.CurrentRow.Cells("Alpha").Value), clsCommon.myCstr(Gv1.CurrentRow.Cells("ParticularCode").Value), True, "", "")
            '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            '        If dt IsNot Nothing And dt.Rows.Count > 0 Then
            '            gvDetail.DataSource = Nothing
            '            gvDetail.Rows.Clear()
            '            gvDetail.Columns.Clear()
            '            gvDetail.DataSource = dt
            '            gvDetail.GroupDescriptors.Clear()
            '            gvDetail.ShowGroupPanel = False
            '            gvDetail.MasterTemplate.SummaryRowsBottom.Clear()
            '            gvDetail.EnableFiltering = False
            '            RadPageView1.SelectedPage = RadPageViewPage3
            '            For ii As Integer = 0 To gvDetail.Columns.Count - 1
            '                gvDetail.Columns(ii).ReadOnly = True
            '                gvDetail.Columns(ii).IsVisible = True
            '            Next
            '            gvDetail.BestFitColumns()

            '            gvDetail.Columns("Source_Doc_No").HeaderText = "Document No"
            '            gvDetail.Columns("Punching_Date").HeaderText = "Document Date"
            '            gvDetail.Columns("Vendor_Code").HeaderText = "Vendor"
            '            gvDetail.Columns("Cust_Code").HeaderText = "Customer"
            '            gvDetail.Columns("Trans_Type").HeaderText = "Transaction"
            '            gvDetail.Columns("Item_Code").HeaderText = "Item"
            '            gvDetail.Columns("Location_Code").HeaderText = "Locatioin"
            '            gvDetail.Columns("Other_Location_Code").HeaderText = "Other Location"
            '            gvDetail.Columns("QtyKg").HeaderText = "Qty KG"
            '            gvDetail.Columns("QtyLtr").HeaderText = "Qty Ltr"
            '            gvDetail.Columns("Fat_Per").HeaderText = "FAT %"
            '            gvDetail.Columns("SNF_Per").HeaderText = "SNF %"
            '            gvDetail.Columns("Fat_KG").HeaderText = "FAT Kg"
            '            gvDetail.Columns("SNF_KG").HeaderText = "SNF Kg"
            '            gvDetail.Columns("Avg_Cost").HeaderText = "Amount"

            '            Dim summaryRowItem As New GridViewSummaryRowItem()
            '            Dim Smitem As New GridViewSummaryItem("QtyLtr", "{0:F2}", GridAggregateFunction.Sum)
            '            summaryRowItem.Add(Smitem)

            '            Smitem = New GridViewSummaryItem("QtyKg", "{0:F2}", GridAggregateFunction.Sum)
            '            summaryRowItem.Add(Smitem)

            '            Smitem = New GridViewSummaryItem("Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
            '            summaryRowItem.Add(Smitem)
            '            Smitem = New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
            '            summaryRowItem.Add(Smitem)

            '            Smitem = New GridViewSummaryItem("Avg_Cost", "{0:F2}", GridAggregateFunction.Sum)
            '            summaryRowItem.Add(Smitem)

            '            gvDetail.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            '        Else
            '            clsCommon.MyMessageBoxShow("No data found to display", Me.Text)
            '        End If
            '    End If
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

