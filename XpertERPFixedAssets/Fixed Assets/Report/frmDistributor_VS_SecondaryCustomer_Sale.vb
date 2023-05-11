'-Created By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000002121]
'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

Imports common
Imports System.Data.SqlClient
Imports System
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmDistributor_VS_SecondaryCustomer_Sale
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim dt As DataTable
    Dim strYear As String
    Dim strMonth As String
    Dim currentDate As DateTime = clsCommon.GETSERVERDATE()

    Private Sub FrmDistributor_VS_SecondaryCustomer_Sale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadYears()
        LoadMonths()
        LoadCustomer()
        LoadPakcs()
        reset()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmDistributor_VS_SecondaryCustomer_Sale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
        btnRefresh.Visible = MyBase.isPrintFlag
    End Sub

    Sub reset()
        ddlYear.SelectedValue = currentDate.Year
        ddlMonth.SelectedValue = currentDate.Month
        chkCustomerAll.IsChecked = True
        GV1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub LoadYears()
        Dim dtyear As New DataTable
        dtyear.Columns.Add("Year", GetType(Integer))
        For year As Integer = 2011 To 2030
            dtyear.Rows.Add(year)
        Next
        ddlYear.DataSource = dtyear
        ddlYear.DisplayMember = "Year"
        ddlYear.ValueMember = "Year"
    End Sub

    Private Sub LoadMonths()
        Dim dtmonth As New DataTable
        dtmonth.Columns.Add("Month", GetType(Integer))
        dtmonth.Columns.Add("MonthName", GetType(String))
        dtmonth.Rows.Add(1, "Janaury")
        dtmonth.Rows.Add(2, "February")
        dtmonth.Rows.Add(3, "March")
        dtmonth.Rows.Add(4, "April")
        dtmonth.Rows.Add(5, "May")
        dtmonth.Rows.Add(6, "June")
        dtmonth.Rows.Add(7, "July")
        dtmonth.Rows.Add(8, "August")
        dtmonth.Rows.Add(9, "September")
        dtmonth.Rows.Add(10, "October")
        dtmonth.Rows.Add(11, "November")
        dtmonth.Rows.Add(12, "December")
        ddlMonth.DataSource = dtmonth
        ddlMonth.DisplayMember = "MonthName"
        ddlMonth.ValueMember = "Month"
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "Select Cust_Code as Code, Customer_Name As Name from TSPL_CUSTOMER_MASTER Where Cust_Type_Code='D'"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub

    Sub LoadPakcs()
        Dim strquery As String = "Select Distinct Inv_Class_Code as Code, Inv_Class_Desc as Description from TSPL_INV_CLASS_DETAILS WHERE Inv_Class_Name='Size'"
        cbgPacks.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgPacks.ValueMember = "Code"
        cbgPacks.DisplayMember = "Description"
        cbgPacks.CheckedAll()
    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Dim strSum As String = ""
        Dim strPivot As String = ""
        Try
            strYear = clsCommon.myCstr(ddlYear.SelectedValue)
            strMonth = clsCommon.myCstr(ddlMonth.SelectedValue)
            If chkCustomerSelect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Customer or select ALL")
                Return
            End If
            If cbgPacks.CheckedValue.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Pack or select ALL")
                Return
            End If
            For Each Str As String In cbgPacks.CheckedDisplayMember
                If clsCommon.myLen(strPivot) > 0 Then
                    strPivot += "],["
                    strSum += "],0)+ISNULL(["
                Else
                    strPivot += "["
                    strSum += "ISNULL(["
                End If
                strPivot += Str
                strSum += Str
            Next
            strPivot += "]"
            strSum += "],0)"
            GV1.EnableFiltering = True

            qry = "Select *, (" + strSum + ") as Total from (" + Environment.NewLine & _
            " Select 'Primary' as Type, Cust_Code as DistributorCode, Customer_Name as DistributorName, Cust_Code, Customer_Name, 0 as OrderBy, " + strPivot + " From (" + Environment.NewLine & _
            " Select Cust_Code, MAX(Customer_Name) as Customer_Name, Class_Desc, SUM(Qty) as Qty from (" + Environment.NewLine & _
            " Select TSPL_SALE_INVOICE_HEAD.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No AS invoiceno, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, DATEPART(MM, TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date) as [Month], TSPL_ITEM_DETAILS.Class_Desc, TSPL_SALE_INVOICE_HEAD.location, (case when TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then convert(decimal(18,2),(isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))) else  convert(decimal(18,2),(isnull(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))) end ) as Qty, TSPL_ITEM_MASTER.Pack_Seq as  OrderBy, TSPL_SALE_INVOICE_DETAIL.Item_Code,TSPL_SALE_INVOICE_DETAIL.Item_Desc FROM  TSPL_SALE_INVOICE_DETAIL" & _
            " INNER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No" & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SALE_INVOICE_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" & _
            " INNER JOIN TSPL_ITEM_MASTER ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_MASTER.Item_Code" & _
            " LEFT OUTER JOIN TSPL_ITEM_DETAILS ON TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code" & _
            " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code WHERE TSPL_ITEM_DETAILS.Class_Name = 'size' AND DatePart(YY,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date)=" + strYear + " AND DatePart(MM,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date)=" + strMonth + " and Is_Post='Y' AND TSPL_CUSTOMER_MASTER.Cust_Type_Code='D'" & _
            Environment.NewLine + " Union All" + Environment.NewLine & _
            " SELECT TSPL_SALE_RETURN_HEAD.Cust_Code, TSPL_CUSTOMER_MASTER.Customer_Name, TSPL_SALE_RETURN_HEAD.Sale_Return_No, TSPL_SALE_RETURN_HEAD.Sale_Return_Date, DATEPART(MM,TSPL_SALE_RETURN_HEAD.Sale_Return_Date) As [Month], TSPL_ITEM_DETAILS.Class_Desc, TSPL_SALE_RETURN_HEAD .Location AS location, -(case when TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then convert(decimal(18,2),(isnull(TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) else  convert(decimal(18,2),(isnull(TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) end) as Qty, TSPL_ITEM_MASTER.Pack_Seq AS OrderBy, TSPL_SALE_RETURN_DETAIL.Item_Code, TSPL_SALE_RETURN_DETAIL.Item_Desc FROM TSPL_SALE_RETURN_HEAD" & _
            " LEFT OUTER JOIN TSPL_SALE_RETURN_DETAIL ON TSPL_SALE_RETURN_DETAIL.Sale_Return_No=TSPL_SALE_RETURN_HEAD.Sale_Return_No" & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_SALE_RETURN_HEAD.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code" & _
            " LEFT OUTER JOIN TSPL_ITEM_DETAILS ON TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS.Item_Code" & _
            " LEFT OUTER JOIN TSPL_ITEM_UOM_DETAIL ON TSPL_ITEM_UOM_DETAIL.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code AND  TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_SALE_RETURN_DETAIL.Unit_code" & _
            " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_SALE_RETURN_DETAIL.Item_Code  WHERE TSPL_ITEM_DETAILS.Class_Name = 'size' AND DatePart(YY,TSPL_SALE_RETURN_HEAD.Sale_Return_Date)=" + strYear + " AND DatePart(MM,TSPL_SALE_RETURN_HEAD.Sale_Return_Date)=" + strMonth + " and TSPL_SALE_RETURN_HEAD.Is_Post='Y' AND TSPL_CUSTOMER_MASTER.Cust_Type_Code='D'" & _
            " ) grp Group By Cust_Code, Class_Desc" + Environment.NewLine & _
            " ) XXX Pivot (SUM(Qty) for [Class_Desc] IN (" + strPivot + ")) as PVT" & _
            Environment.NewLine + " UNION ALL" + Environment.NewLine & _
            " Select 'Secondary' as Type, DistributorCode, DistributorName, Cust_Code, Customer_Name, 1 as OrderBy, " + strPivot + " from (" & _
            " Select DistributorCode, MAX(DistributorName) as DistributorName, Cust_Code, MAX(Customer_Name) as Customer_Name, Class_Desc, Case When SUM(Qty)=0 Then NULL Else SUM(Qty) END as Qty from (" & _
            " Select TSPL_SECONDARY_CUSTOMER_MASTER.Distributor as DistributorCode, TSPL_CUSTOMER_MASTER.Customer_Name as DistributorName, TSPL_SECONDARY_CUSTOMER_SALE.Cust_Code, TSPL_SECONDARY_CUSTOMER_MASTER.Customer_Name, Pack, TSPL_INV_CLASS_DETAILS.Inv_Class_Desc as Class_Desc, Sale as Qty from TSPL_SECONDARY_CUSTOMER_SALE" & _
            " LEFT OUTER JOIN TSPL_SECONDARY_CUSTOMER_MASTER ON TSPL_SECONDARY_CUSTOMER_MASTER.Cust_Code=TSPL_SECONDARY_CUSTOMER_SALE.Cust_Code" & _
            " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SECONDARY_CUSTOMER_MASTER.Distributor" & _
            " LEFT OUTER JOIN TSPL_INV_CLASS_DETAILS ON TSPL_INV_CLASS_DETAILS.Inv_Class_Code=TSPL_SECONDARY_CUSTOMER_SALE.Pack WHERE TSPL_INV_CLASS_DETAILS.Inv_Class_Name='Size' AND Year=" + strYear + " AND Month=" + strMonth + "" & _
            " ) grp Group By DistributorCode, Cust_Code, Class_Desc" + Environment.NewLine & _
            " ) XXX PIVOT (SUM(Qty) for [Class_Desc] IN (" + strPivot + ")) as PVT" + Environment.NewLine & _
            " ) Final Order By DistributorCode, OrderBy, Cust_Code"
            dt = clsDBFuncationality.GetDataTable(qry)
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

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = "Year : " + clsCommon.myCstr(ddlYear.Text) + ""
            arrHeader.Add(strTemp)
            strTemp = "Month : " + clsCommon.myCstr(ddlMonth.Text) + ""
            arrHeader.Add(strTemp)
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Visi Install/Pullout", GV1, arrHeader, "Visi_Install_Pullout")
            ElseIf exporter = EnumExportTo.PDF Then
                clsCommon.MyExportToPDF("Visi Install/Pullout", GV1, arrHeader, "Visi_Install_Pullout", True)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormationOFGV1()
        For Each coll As GridViewColumn In GV1.Columns
            coll.Width = 70
        Next
        GV1.Columns("DistributorCode").IsVisible = False
        GV1.Columns("DistributorName").IsVisible = False
        GV1.Columns("OrderBy").IsVisible = False

        GV1.Columns("Cust_Code").HeaderText = "Customer Code"
        GV1.Columns("Cust_Code").Width = 100

        GV1.Columns("Customer_Name").HeaderText = "Customer Name"
        GV1.Columns("Customer_Name").Width = 200

        GV1.Columns("Type").Width = 80

        For Each grow As GridViewRowInfo In GV1.Rows
            If grow.Cells("Type").Value = "Primary" Then
                For x As Integer = 0 To Me.GV1.ColumnCount - 1
                    grow.Cells(x).Style.CustomizeFill = True
                    grow.Cells(x).Style.DrawFill = True
                    grow.Cells(x).Style.BackColor = Color.AliceBlue
                Next x
            End If
        Next
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
        Refresh = 2
    End Enum

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        reset()
    End Sub

    Private Sub chkVendorAll_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomerAll.IsChecked
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Print(EnumExportTo.Refresh)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Print(EnumExportTo.PDF)
    End Sub
End Class
