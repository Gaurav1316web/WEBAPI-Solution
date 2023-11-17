Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports common

Public Class FrmSamplingReportSummary1

    Private Sub FrmSamplingReportSummary1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        funreset()

    End Sub

    Sub LoadVendor()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub

    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"

    End Sub
    Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        CbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        CbgLocation.ValueMember = "Code"

    End Sub

    Sub funreset()
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()

        LoadVendor()
        LoadCustomer()
        LoadLocation()
        chkAll.IsChecked = True
        chkallcustomer.IsChecked = True
        ChklocationAll.IsChecked = True
        rdodetail.IsChecked = True
    End Sub


    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged

        cbgroute.Enabled = Not chkAll.IsChecked
    End Sub

    Private Sub chkallcustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkallcustomer.ToggleStateChanged
        cbgCustomer.Enabled = Not chkallcustomer.IsChecked
    End Sub
    Private Sub ChklocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChklocationAll.ToggleStateChanged
        CbgLocation.Enabled = Not ChklocationAll.IsChecked
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try

            'Dim qry As String = " SELECT  distinct  '" + dtpFromdate.Value + "' as fromdate,'" + dtptodate.Value + "' as todate, (TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AS invoiceno, TSPL_SALE_INVOICE_DETAIL.Item_Code , (TSPL_SALE_INVOICE_HEAD.Route_No) AS route, (ISNULL(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty, 0)) AS QTY,TSPL_SALE_INVOICE_DETAIL.Basic_Rate,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Cust_Name," & _
            '                      "( case when  TSPL_SALE_INVOICE_DETAIL.Scheme_Item='n'  then   TSPL_SALE_INVOICE_DETAIL.Balance_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor   else 0 end) as mainscheme ," & _
            '                      " ( case when  TSPL_SALE_INVOICE_DETAIL.Scheme_Item='y'  then   TSPL_SALE_INVOICE_DETAIL.Balance_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as qtyscheme " & _
            '                      ",TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Qty, (TSPL_COMPANY_MASTER.Comp_Name) AS company,(TSPL_COMPANY_MASTER.Add1) AS address FROM  TSPL_SALE_INVOICE_DETAIL " & _
            '                      " INNER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
            '                      "  INNER JOIN TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
            '                        " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code where 2=2  "



            Dim qry As String = " SELECT  distinct  '" + dtpFromdate.Value + "' as fromdate,'" + dtptodate.Value + "' as todate, (TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AS invoiceno, TSPL_SALE_INVOICE_DETAIL.Item_Code , (TSPL_SALE_INVOICE_HEAD.Route_No) AS route, (ISNULL(TSPL_SALE_INVOICE_DETAIL.Invoice_Qty, 0)) AS QTY,((TSPL_SALE_INVOICE_DETAIL.Basic_Rate+TSPL_SALE_INVOICE_DETAIL.Item_Tax)*TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Basic_Rate,TSPL_SALE_INVOICE_HEAD.Route_No,TSPL_SALE_INVOICE_HEAD.Cust_Name," & _
                                 "( case when  TSPL_SALE_INVOICE_DETAIL.Scheme_Item='n'  then   TSPL_SALE_INVOICE_DETAIL.Balance_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor   else 0 end) as mainscheme ," & _
                                 " ( case when  TSPL_SALE_INVOICE_DETAIL.Scheme_Item='y'  then   TSPL_SALE_INVOICE_DETAIL.Balance_Qty/TSPL_ITEM_UOM_DETAIL.Conversion_Factor else 0 end) as qtyscheme " & _
                                 ",TSPL_SALE_INVOICE_DETAIL.Scheme_Code_Qty, (TSPL_COMPANY_MASTER.Comp_Name) AS company,(TSPL_COMPANY_MASTER.Add1) AS address FROM  TSPL_SALE_INVOICE_DETAIL " & _
                                 " INNER JOIN TSPL_SALE_INVOICE_HEAD ON TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No " & _
                                 "  INNER JOIN TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
                                   " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SALE_INVOICE_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SALE_INVOICE_DETAIL.Unit_code where 2=2  "


            'If chkAll.IsChecked = True Then
            '    qry += " "

            'ElseIf chkSelect.IsChecked = True Then
            If chkSelect.IsChecked = True Then

                qry += " and  TSPL_SALE_INVOICE_HEAD.Route_No in(" + (clsCommon.GetMulcallString(cbgroute.CheckedValue)) + ") "
            End If
            If chkselectcustomer.IsChecked = True Then
                qry += " and  TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ")  "
            End If
            If ChkLocationSelect.IsChecked = True Then
                qry += " and Tspl_Sale_invoice_head.Location in (" + (clsCommon.GetMulcallString(CbgLocation.CheckedValue)) + ")"
            End If



            qry += " and    CONVERT(date,TSPL_SALE_INVOICE_HEAD.sale_invoice_date ,103) >=CONVERT(date,'" + dtpFromdate.Value + "',103)  and CONVERT(date,TSPL_SALE_INVOICE_HEAD.sale_invoice_date ,103)  <=CONVERT(date,'" + dtptodate.Value + "',103) "

            Dim frmCRV As New frmCrystalReportViewer()
            If rdodetail.IsChecked = True Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "SampleReportDetail", "Sample Report Detail")
            ElseIf rdosummary.IsChecked = True Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "SampleReportSummary", "Sample Report Summary")


            End If
            frmCRV = Nothing


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


   
End Class
