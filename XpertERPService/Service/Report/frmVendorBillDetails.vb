'=========,Updated By Rohit on June 24,2014,11:00 PM (Remark : Update Vendor Load Code and Design .Add code to Show City,Phone No,Address,State,Region Vendor Wise.) =========================
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine

Public Class frmVendorBillDetails
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmVendorBillDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        'Dim strVendor As String = ""
        'Dim strBill As String = ""
        'Dim strDateRange As String = ""
        'Dim arrHeader As List(Of String) = New List(Of String)()
        'arrHeader.Add("Vendor Bill Details : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
        'arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + " ")
        ''---------------------------------
        'strDateRange = " where convert(date,Vendor_Invoice_Date ,103) between convert(date,'" & clsCommon.myCDate(fromDate.Value) & "',103) and convert(date,'" & clsCommon.myCstr(ToDate.Value) & "',103) "
        'If IsPrint = Exporter.Refresh Then
        '    If cbgVendor.CheckedValue.Count > 0 Then
        '        strVendor += " and Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ") "
        '    Else
        '        strVendor = ""

        '    End If
        '    If cbgBill.CheckedValue.Count > 0 Then
        '        strBill += " and  [Bill No] in (" + clsCommon.GetMulcallString(cbgBill.CheckedValue) + ") "
        '    Else
        '        strBill = ""
        '    End If
        '    Dim str As String = "Select ROW_NUMBER() over(order by [Bill No]) as [SL NO], Vendor_Code, Vendor_Name, Area, charge_cat_code, Description, Month, [BIll No], Vendor_Invoice_Date, Total_Amount,Case When RowNo=1 Then ChequeNo Else '' End as ChequeNo, Case When RowNo=1 Then PaymentAmt Else 0 End as PaymentAmt,  Case When RowNo=1 then ChequeDate Else '' End As ChequeDate from ( Select Vendor_Code, Vendor_Name, Area, charge_cat_code, Description, Month, [BIll No], Vendor_Invoice_Date, Total_Amount, PaymentAmt, ChequeNo, ChequeDate, ROW_NUMBER() Over ( Partition By [BIll No] Order By [BIll No]) as RowNo from ( Select XXX.Vendor_Code, XXX.Vendor_Name, XXX.Area, XXX.charge_cat_code, XXX.Description, XXX.Month, XXX.[BIll No], XXX.Vendor_Invoice_Date, XXX.Total_Amount, YYY.Document_No, YYY.PaymentAmt, YYY.ChequeNo, YYY.ChequeDate from ( select TSPL_VENDOR_INVOICE_HEAD.Vendor_Code, TSPL_VENDOR_MASTER.Vendor_Name, TSPL_LOCATION_MASTER.City_Code as [Area], TSPL_VENDOR_INVOICE_DETAIL.charge_cat_code, TSPL_Charge_Category.Description, left(DATENAME(MONTH,convert(datetime,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)),3)+ '-' + CONVERT(varchar(4), right(datepart(yy,convert(datetime,TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date,103)),2)) as [Month], TSPL_VENDOR_INVOICE_HEAD.Document_No as [BIll No], TSPL_VENDOR_INVOICE_HEAD.Vendor_Invoice_Date, TSPL_VENDOR_INVOICE_DETAIL.Total_Amount  from TSPL_VENDOR_INVOICE_DETAIL  left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_VENDOR_INVOICE_DETAIL.Document_No  left outer join TSPL_Charge_Category on TSPL_Charge_Category.Charge_Cat_Code =TSPL_VENDOR_INVOICE_DETAIL.charge_cat_code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_VENDOR_INVOICE_HEAD.Loc_Code   WHERE ISNULL(TSPL_VENDOR_INVOICE_HEAD.Posting_Date,'')<>''  ) XXX   LEFT OUTER JOIN (Select TSPL_PAYMENT_DETAIL.Document_No, SUM(TSPL_PAYMENT_DETAIL.Applied_Amount) as PaymentAmt, STUFF((Select ', '+ISNULL(Cheque_No, '') From TSPL_PAYMENT_HEADER PYHead LEFT OUTER JOIN TSPL_PAYMENT_DETAIL PYDetail On PYDetail.Payment_No=PYHead.Payment_No WHERE PYDetail.Document_No=TSPL_PAYMENT_DETAIL.Document_No For XML Path ('')), 1,2,'') as ChequeNo, STUFF((Select ', '+Case When ISNULL(Cheque_No, '')<>'' Then CONVERT(VARCHAR,Cheque_Date,103) Else '' End From TSPL_PAYMENT_HEADER PYHead LEFT OUTER JOIN TSPL_PAYMENT_DETAIL PYDetail On PYDetail.Payment_No=PYHead.Payment_No WHERE PYDetail.Document_No=TSPL_PAYMENT_DETAIL.Document_No For XML Path ('')), 1,2,'') as ChequeDate from TSPL_PAYMENT_DETAIL LEFT OUTER JOIN TSPL_PAYMENT_HEADER ON TSPL_PAYMENT_HEADER.Payment_No=TSPL_PAYMENT_DETAIL.Payment_No WHERE TSPL_PAYMENT_HEADER.Posted=1 GROUP BY TSPL_PAYMENT_DETAIL.Document_No) YYY ON XXX.[BIll No]=YYY.Document_No    ) ZZZ  ) FINAL   "
        '    str = str + strDateRange + strVendor + strBill
        '    dt = clsDBFuncationality.GetDataTable(str)

        '    If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
        '        common.clsCommon.MyMessageBoxShow("No Record Found")
        '    Else
        '        gv.DataSource = dt
        '        SetGridFormation()
        '        RadPageView1.SelectedPage = RadPageViewPage2
        '    End If
        'ElseIf IsPrint = Exporter.Excel Then
        '    clsCommon.MyExportToExcel("Vendor Bill Details", gv, arrHeader, Me.Text)
        'Else
        '    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, True)
        'End If
        Dim str As String = "select row_number() over(order by convert(date,Entry_Date,103)) as SLNO, InOut,Item_Code,Qty,convert(date,Entry_Date,103) as Entry_Date, Qty as BalQty  from TSPL_INVENTORY_MOVEMENT where Item_Code='d0061' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
        If dt.Rows.Count <= 0 Then
            Exit Sub
        End If
        dt.DefaultView.RowFilter = "InOut='O'"
        dt.DefaultView.Sort = "Entry_Date"
        Dim dt1 As DataTable = dt.DefaultView.ToTable
        dt.DefaultView.RowFilter = Nothing
        Dim dtfnl As DataTable = dt.Clone()
        dtfnl.Rows.Clear()

        Dim outqty As Integer = 0
        Dim inqty As Integer = 0
        Dim newinqty As Integer = 0
        Dim icode As String = ""
        Dim idate As Date = Nothing
        Dim flag As Boolean = False
        Dim j As Integer = 0
        Dim x As Integer = 0
        For i As Integer = 0 To dt1.Rows.Count - 1
            If Not flag Then
                dt.DefaultView.RowFilter = Nothing
                dt.DefaultView.RowFilter = "InOut='I' and Item_Code='" & dt1.Rows(i)("Item_Code").ToString & "' and Entry_Date<='" & dt1.Rows(i)("Entry_Date") & "' and BalQty>0"
                dt.DefaultView.Sort = "Entry_Date"
                j = 0
            Else
                i = x
                j = j + 1
            End If
            If (dt.DefaultView.ToTable).Rows.Count > 0 Then
                If (dt.DefaultView.ToTable).Rows(j)("BalQty") = dt1.Rows(i)("Qty") Then
                    dt.Rows((dt.DefaultView.ToTable).Rows(j)("SLNO") - 1)("BalQty") = 0

                    dtfnl.ImportRow((dt.DefaultView.ToTable).Rows(j))
                    dtfnl.ImportRow(dt1.Rows(i))
                    flag = False
                ElseIf (dt.DefaultView.ToTable).Rows(j)("BalQty") > dt1.Rows(i)("Qty") Then
                    dtfnl.ImportRow(dt1.Rows(i))
                    dt.Rows((dt.DefaultView.ToTable).Rows(j)("SLNO") - 1)("BalQty") = ((dt.DefaultView.ToTable).Rows(j)("BalQty") - dt1.Rows(i)("Qty"))
                    flag = False
                Else
                    Dim curqty As Integer = (dt.DefaultView.ToTable).Rows(j)("BalQty")
                    Dim diff As Integer = dt1.Rows(i)("Qty") - (dt.DefaultView.ToTable).Rows(j)("BalQty")
                    dt.Rows((dt.DefaultView.ToTable).Rows(j)("SLNO") - 1)("BalQty") = 0
                    dtfnl.ImportRow((dt.DefaultView.ToTable).Rows(j))
                    dt1.Rows(i)("Qty") = curqty
                    dtfnl.ImportRow(dt1.Rows(i))
                    dt1.Rows(i)("Qty") = diff
                    flag = True
                    x = i
                End If
            End If
            gv.DataSource = dtfnl
            gv.BestFitColumns()


        Next
        dt.DefaultView.RowFilter = Nothing
        dt.DefaultView.RowFilter = "InOut='I' and BalQty>0"
        If (dt.DefaultView.ToTable).Rows.Count > 0 Then
            For j = 0 To (dt.DefaultView.ToTable).Rows.Count - 1
                dtfnl.ImportRow((dt.DefaultView.ToTable).Rows(j))
            Next
        End If
        gv.DataSource = dtfnl
        gv.BestFitColumns()
        'gv.DataSource = dt
        'gv.BestFitColumns()
        RadPageView1.SelectedPage = RadPageViewPage2

    End Sub

    Sub SetGridFormation()
        Try
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.DataSource = dt
            gv.AllowAddNewRow = False
            gv.AllowDragToGroup = False
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = False
            Next


            gv.Columns("SL NO").IsVisible = True
            gv.Columns("SL NO").Width = 100
            gv.Columns("SL NO").HeaderText = "SL NO"

            gv.Columns("Vendor_Name").IsVisible = True
            gv.Columns("Vendor_Name").Width = 200
            gv.Columns("Vendor_Name").HeaderText = "Vendor Name"

            gv.Columns("Area").IsVisible = True
            gv.Columns("Area").Width = 150
            gv.Columns("Area").HeaderText = "Area"

            gv.Columns("Description").IsVisible = True
            gv.Columns("Description").Width = 70
            gv.Columns("Description").HeaderText = "Nature Of Head"

            gv.Columns("Month").IsVisible = True
            gv.Columns("Month").Width = 70
            gv.Columns("Month").HeaderText = "Month"

            gv.Columns("Bill No").IsVisible = True
            gv.Columns("Bill No").Width = 100
            gv.Columns("Bill No").HeaderText = "Bill No"

            gv.Columns("Vendor_Invoice_Date").IsVisible = True
            gv.Columns("Vendor_Invoice_Date").Width = 130
            gv.Columns("Vendor_Invoice_Date").HeaderText = "Bill Date"

            gv.Columns("Total_Amount").IsVisible = True
            gv.Columns("Total_Amount").Width = 150
            gv.Columns("Total_Amount").HeaderText = "Claimed Amount"

            gv.Columns("ChequeNo").IsVisible = True
            gv.Columns("ChequeNo").Width = 100
            gv.Columns("ChequeNo").HeaderText = "Cheque No"


            gv.Columns("PaymentAmt").IsVisible = True
            gv.Columns("PaymentAmt").Width = 150
            gv.Columns("PaymentAmt").HeaderText = "Amount"

            gv.Columns("ChequeDate").IsVisible = True
            gv.Columns("ChequeDate").Width = 150
            gv.Columns("ChequeDate").HeaderText = "Cheque Date"
            'gv.Columns("Cheque_Date").FormatString = "dd/MM/yyyy"

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub


    Private Sub FrmSaleOrderSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        LoadVendor()
        LoadBill()
        cbgBill.CheckedAll()
        cbgVendor.CheckedAll()
    End Sub

    Sub LoadVendor()
        Dim strquery As String = "select vendor_code as [Vendor Code], Vendor_Name as [Vendor Name],Phone1 as [Phone No]," _
        & " Coalesce(Add1,'') + coalesce(Add2,'') + coalesce( add3,'')  as [Address], tspl_vendor_master.city_code as [City],region_code as " _
        & " [Region],State,Tin_No as [Tin No] from tspl_vendor_master  left join TSPL_CITY_MASTER on tspl_vendor_master.City_Code=TSPL_CITY_MASTER.City_Code where 2=2 "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgVendor.ValueMember = "Vendor Code"
        cbgVendor.DisplayMember = "Vendor Name"
    End Sub
    Sub LoadBill()
        Dim qry As String = "select Document_No as 'Bill No' from TSPL_VENDOR_INVOICE_HEAD "
        cbgBill.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBill.ValueMember = "Bill No"
        cbgBill.DisplayMember = "Bill No"
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        fromDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE())
        ToDate.Value = clsCommon.myCDate(clsCommon.GETSERVERDATE())
        cbgBill.UnCheckedAll()
        cbgVendor.UnCheckedAll()
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.DataSource = Nothing
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(Exporter.Refresh)
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.Excel)
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If (gv.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow("No Data To Export")
            Exit Sub
        End If
        LoadData(Exporter.PDF)
    End Sub

End Class
