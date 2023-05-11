'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
''update by preeti gupta Against ticket no[BM00000008062]
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

Imports System.Globalization

Imports System.IO
Public Class frmCompanyMonthWiseSaleComparison
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
    Dim frm As New frmSalesAnalysisReport(strUserCode, strCompany)
    Dim Year, CustomerName, May As String
    Dim frmdate As String = ""
    Dim todate As String = ""
    Dim saleinvoice As String


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCompanyMonthWiseSaleComparison)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        '' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''
        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub FrmSaleOrderReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadCustomer()
        LoadLocation()
        LoadFinYear()
        rbtnCustAll.IsChecked = True
        rbtnLocAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Sub rbtnCustAllSelectClick() Handles rbtnCustAll.ToggleStateChanged, rbtnCustSelect.ToggleStateChanged
        Try

            If rbtnCustAll.IsChecked Then
                cbgCustomer.CheckedAll()
                cbgCustomer.Enabled = False
            Else
                cbgCustomer.UnCheckedAll()
                cbgCustomer.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub rbtnLocAllSelectClick() Handles rbtnLocAll.ToggleStateChanged, rbtnLocSelect.ToggleStateChanged
        Try
            If rbtnLocAll.IsChecked Then
                cbgLocation.CheckedAll()
                cbgLocation.Enabled = False
            Else
                cbgLocation.UnCheckedAll()
                cbgLocation.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadLocation()
        Try
            Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER "
            cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgLocation.ValueMember = "Location"
            cbgLocation.DisplayMember = "Location Description"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadCustomer()
        Try
            Dim qry As String = "select Cust_Code as Code,Customer_Name as [Customer Name] from TSPL_Customer_MASTER  "
            cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgCustomer.ValueMember = "Code"
            cbgCustomer.DisplayMember = "Customer Name"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadFinYear()
        Try
            Dim qry As String = "select CONVERT (varchar,year) + '-' + CONVERT(varchar,year+1) as Year from  (select  distinct  case when MONTH(TSPL_SD_SALE_INVOICE_HEAD.document_date)<=3 then year(TSPL_SD_SALE_INVOICE_HEAD.document_date)-1 else year(TSPL_SD_SALE_INVOICE_HEAD.document_date) end as YEAR from TSPL_SD_SALE_INVOICE_HEAD  ) as yyy "
            FinYear.DataSource = clsDBFuncationality.GetDataTable(qry)
            FinYear.ValueMember = "Year"
            FinYear.DisplayMember = "Year"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Sub LoadData(ByVal IsPrint As Exporter)
        Dim strCustomer As String = ""
        Dim strLocation As String = ""
        Dim strFinYear As String = ""
        Dim qry1 As String = ""
        Dim qry2 As String = ""
        Dim qry3 As String = ""
        Dim qry As String = ""
        Dim arr As New List(Of String)
        arr.Add("COMPANY-WISE MONTH-WISE SALES VALUE ANALYSIS")
        arr.Add("Sales Summary, " + clsCommon.myCstr(FinYear.Text))
        If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then
            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""
            End If

            If clsCommon.CompairString(FinYear.Text, "") <> CompairStringResult.Equal Then
                strFinYear += " where convert(varchar,year) = '" + clsCommon.myCstr(FinYear.Text) + "' "

            Else
                strFinYear = ""

            End If

            If cbgLocation.CheckedValue.Count > 0 Then
                strLocation += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLocation = ""
            End If
            qry1 = " SELECT convert(varchar,ROW_NUMBER () over(order by [customer name])) as [SL NO]  ,[Customer Name],[Customer Code] , ISNULL(April ,0) as April, ISNULL(May ,0) as May, isnull(June ,0) as June , isnull(July ,0) as July,isnull(August ,0) as August, ISNULL(September ,0) as September, ISNULL(October ,0) as October, isnull(November ,0)as November, isnull(December ,0) as December, isnull(January ,0) as January, isnull(February ,0) as February, isnull(March ,0) as March, (ISNULL(April ,0)+ISNULL(May ,0)+isnull(June ,0)+isnull(July ,0)+isnull(August ,0)+ISNULL(September ,0)+ISNULL(October ,0)+isnull(November ,0)+isnull(December ,0)+isnull(January ,0)+isnull(February ,0)+isnull(March ,0)) as Total FROM (  select convert(varchar,YEAR) + '-' + CONVERT(varchar,YEAR+1) as YEAR,[Customer Name],[Customer Code]  ,MONTH,Amount  from(      "
            qry2 = " SELECT    case when MONTH(TSPL_SD_SALE_INVOICE_HEAD.document_date)<=3 then year(TSPL_SD_SALE_INVOICE_HEAD.document_date)-1 else year(TSPL_SD_SALE_INVOICE_HEAD.document_date) end YEAR,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],  upper(datename(month,TSPL_SD_SALE_INVOICE_HEAD.document_date)) as MONTH,  TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as AMOUNT  FROM TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     "
            qry3 = " )T1 ) as s PIVOT  (  SUM(Amount) FOR [month] IN (  April, May, June, July,August, September, October, November, December, January, February, March  )  )AS p   "
            qry2 += strCustomer
            qry3 += strFinYear
            qry2 += strLocation
            qry = qry1 + qry2 + qry3
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim dt1 As New DataTable
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else
                Dim tot As Double = 0
                Dim valJan As Double = 0
                Dim valFeb As Double = 0
                Dim valMar As Double = 0
                Dim valApr As Double = 0
                Dim valMay As Double = 0
                Dim valJun As Double = 0
                Dim valJul As Double = 0
                Dim valAug As Double = 0
                Dim valSep As Double = 0
                Dim valOct As Double = 0
                Dim valNov As Double = 0
                Dim valDec As Double = 0
                Dim tot1 As Double = 0
                Dim valJan1 As Double = 0
                Dim valFeb1 As Double = 0
                Dim valMar1 As Double = 0
                Dim valApr1 As Double = 0
                Dim valMay1 As Double = 0
                Dim valJun1 As Double = 0
                Dim valJul1 As Double = 0
                Dim valAug1 As Double = 0
                Dim valSep1 As Double = 0
                Dim valOct1 As Double = 0
                Dim valNov1 As Double = 0
                Dim valDec1 As Double = 0
                For i As Integer = 0 To dt.Rows.Count - 1
                    valApr += clsCommon.myCdbl(dt.Rows(i)("April"))
                    valMay += clsCommon.myCdbl(dt.Rows(i)("May"))
                    valJun += clsCommon.myCdbl(dt.Rows(i)("June"))
                    valJul += clsCommon.myCdbl(dt.Rows(i)("July"))
                    valAug += clsCommon.myCdbl(dt.Rows(i)("August"))
                    valSep += clsCommon.myCdbl(dt.Rows(i)("September"))
                    valOct += clsCommon.myCdbl(dt.Rows(i)("October"))
                    valNov += clsCommon.myCdbl(dt.Rows(i)("November"))
                    valDec += clsCommon.myCdbl(dt.Rows(i)("December"))
                    valJan += clsCommon.myCdbl(dt.Rows(i)("January"))
                    valFeb += clsCommon.myCdbl(dt.Rows(i)("February"))
                    valMar += clsCommon.myCdbl(dt.Rows(i)("March"))
                    tot += clsCommon.myCdbl(dt.Rows(i)("Total"))
                Next
                gv.Rows.Clear()
                gv.Columns.Clear()
                loadBlankgrid()
                gv.AutoGenerateColumns = False
                For j As Integer = 0 To dt.Rows.Count - 1
                    valApr1 = clsCommon.myCdbl(dt.Rows(j)("April"))
                    valMay1 = clsCommon.myCdbl(dt.Rows(j)("May"))
                    valJun1 = clsCommon.myCdbl(dt.Rows(j)("June"))
                    valJul1 = clsCommon.myCdbl(dt.Rows(j)("July"))
                    valAug1 = clsCommon.myCdbl(dt.Rows(j)("August"))
                    valSep1 = clsCommon.myCdbl(dt.Rows(j)("September"))
                    valOct1 = clsCommon.myCdbl(dt.Rows(j)("October"))
                    valNov1 = clsCommon.myCdbl(dt.Rows(j)("November"))
                    valDec1 = clsCommon.myCdbl(dt.Rows(j)("December"))
                    valJan1 = clsCommon.myCdbl(dt.Rows(j)("January"))
                    valFeb1 = clsCommon.myCdbl(dt.Rows(j)("February"))
                    valMar1 = clsCommon.myCdbl(dt.Rows(j)("March"))
                    tot1 = clsCommon.myCdbl(dt.Rows(j)("Total"))
                    gv.Rows.Add(clsCommon.myCstr(dt.Rows(j)("SL NO")), clsCommon.myCstr(dt.Rows(j)("Customer Code")), clsCommon.myCstr(dt.Rows(j)("Customer Name")), valApr1, valMay1, valJun1, valJul1, valAug1, valSep1, valOct1, valNov1, valDec1, valJan1, valFeb1, valMar1, tot1)
                    gv.Rows.Add("", "", Math.Round(clsCommon.myCdbl((valApr1 / valApr) * 100), 2), Math.Round(clsCommon.myCdbl((valMay1 / valMay) * 100), 2), Math.Round(clsCommon.myCdbl((valJun1 / valJun) * 100), 2), Math.Round(clsCommon.myCdbl((valJul1 / valJul) * 100), 2), Math.Round(clsCommon.myCdbl((valAug1 / valAug) * 100), 2), Math.Round(clsCommon.myCdbl((valSep1 / valSep) * 100), 2), Math.Round(clsCommon.myCdbl((valOct1 / valOct) * 100), 2), Math.Round(clsCommon.myCdbl((valNov1 / valNov) * 100), 2), Math.Round(clsCommon.myCdbl((valDec1 / valDec) * 100), 2), Math.Round(clsCommon.myCdbl((valJan1 / valJan) * 100), 2), Math.Round(clsCommon.myCdbl((valFeb1 / valFeb) * 100), 2), Math.Round(clsCommon.myCdbl((valMar1 / valMar) * 100), 2), Math.Round(clsCommon.myCdbl((tot1 / tot) * 100), 2))
                Next
                gv.Rows.Add("", "Total", valApr, valMay, valJun, valJul, valAug, valSep, valOct, valNov, valDec, valJan, valFeb, valMar, tot)
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        ElseIf IsPrint = Exporter.Excel Then
            If RadPageView1.Pages("RadPageViewPage2").Item.IsSelected = True Then
                clsCommon.MyExportToExcelGrid(objCommonVar.CurrentCompanyName, gv, arr, Me.Text)
            Else
                clsCommon.MyExportToExcelGrid(objCommonVar.CurrentCompanyName, gv1, arr, Me.Text)
            End If

        Else
            If RadPageView1.Pages("RadPageViewPage2").Item.IsSelected = True Then
                clsCommon.MyExportToPDF(objCommonVar.CurrentCompanyName, gv, arr, Me.Text, True)
            Else
                clsCommon.MyExportToPDF(objCommonVar.CurrentCompanyName, gv1, arr, Me.Text, True)
            End If

        End If

    End Sub

    Sub loadBlankgrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        gv.Columns.Add("SLNO", "SL.NO")
        gv.Columns("SLNO").TextAlignment = ContentAlignment.MiddleCenter
        gv.Columns.Add("CustomerCode", "")
        gv.Columns("CustomerCode").TextAlignment = ContentAlignment.MiddleCenter
        gv.Columns("CustomerCode").IsVisible = False

        gv.Columns.Add("CustomerName", "")
        gv.Columns("CustomerName").TextAlignment = ContentAlignment.MiddleCenter
        gv.Columns.Add("APRIL", "APRIL")
        gv.Columns("APRIL").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns.Add("MAY", "MAY")
        gv.Columns("MAY").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns.Add("JUNE", "JUNE")
        gv.Columns.Add("JULY", "JULY")
        gv.Columns.Add("AUGUST", "AUGUST")
        gv.Columns.Add("SEP", "SEPT.")
        gv.Columns.Add("OCT", "OCT.")
        gv.Columns.Add("NOV", "NOV.")
        gv.Columns.Add("DEC", "DEC.")
        gv.Columns.Add("JAN", "JAN.")
        gv.Columns.Add("FEB", "FEB")
        gv.Columns.Add("MARCH", "MARCH")
        gv.Columns.Add("TOTAL", "TOTAL")
        gv.Columns("JUNE").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("JULY").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("AUGUST").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("SEP").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("OCT").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("NOV").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("DEC").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("JAN").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("FEB").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("MARCH").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("TOTAL").TextAlignment = ContentAlignment.MiddleRight

    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Try
            rbtnCustAll.IsChecked = True
            ''rbtnFinAll.IsChecked = True
            rbtnLocAll.IsChecked = True
            gv.DataSource = Nothing
            gv1.DataSource = Nothing

            cbgLocation.CheckedAll()
            
            cbgCustomer.CheckedAll()


            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Function checkSelection() As Boolean
        If cbgCustomer.CheckedValue.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Customer")
            Return False
        ElseIf clsCommon.CompairString(FinYear.Text, "") = CompairStringResult.Equal Then
            clsCommon.MyMessageBoxShow("Please Select financial Year")
            Return False
        ElseIf cbgLocation.CheckedValue.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Atleast One Location")
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        If checkSelection() Then
            LoadData(Exporter.Refresh)
        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        If gv IsNot Nothing Then
            LoadData(Exporter.Excel)
        Else
            clsCommon.MyMessageBoxShow("No Data Found To Export")
        End If
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If checkSelection() AndAlso gv IsNot Nothing Then
            LoadData(Exporter.PDF)
        Else

            clsCommon.MyMessageBoxShow("No Data Found To Export")
        End If
    End Sub



    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick


        AutoSaleAnalysisFromCmpMnth()
    End Sub
    Function AutoSaleAnalysisFromCmpMnth() As Boolean
        Dim arr As New ArrayList()

        CustomerName = gv.CurrentRow.Cells("CustomerCode").Value
        May = gv.CurrentRow.Cells("May").Value
        Year = FinYear.Text

        saleinvoice = "Sale Invoice"
        frmdate = "01/05/" + FinYear.Text.Substring(0, FinYear.Text.LastIndexOf("-"))
        todate = "31/05/" + FinYear.Text.Substring(0, FinYear.Text.LastIndexOf("-"))

        frm.LoadCustomer()
        'frm.LoadCategory()
        'frm.LoadLocation()
        'frm.ddlSaleType.Text = saleinvoice
        frm.dtpFdate.Value = frmdate
        frm.dtpToDate.Value = todate
        frm.chkClassAll.IsChecked = False
        frm.chkClassSelect.IsChecked = True
        'frm.chkLocAll.IsChecked = True
        'LoadCustomer()
        'frm.chkClassSelect.IsChecked = True
        'frm.rdbDocSummary.IsChecked = True
        'frm.rdbWithFOC.IsChecked = True
        'frm.RadGroupBox1.Visible = True
        'frm.ddlSaleType.SelectedIndex = 0
        'frm.chkMismatch.Checked = True
        'frm.rbtnSaleAccount.IsChecked = True
        If clsCommon.myLen(CustomerName) > 0 Then
            arr.Add(CustomerName)
        End If

        If arr IsNot Nothing AndAlso arr.Count > 0 Then ''write this cond. because array doesn't contains any value and can't understand the why,here use array.

            frm.chkCustomer.CheckedValue = arr
            rbtnCustSelect.IsChecked = True
            cbgCustomer.CheckedValue = arr

        End If


        Dim dt As DataTable = frm.PrintDrillDown()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Function
        Else
            gv1.DataSource = dt

            SetGridFormationOFGV1()
            ReStoreGridLayout()
          
            'If arr IsNot Nothing AndAlso arr.Count > 0 Then ''write this cond. because array doesn't contains any value and can't understand the why,here use array.

            '    ' frm.chkCustomer.CheckedValue = arr
            '    rbtnCustSelect.IsChecked = True
            '    cbgCustomer.CheckedValue = arr
            '    'cbgCustomer.CheckedAll()
            'End If
            RadPageView1.SelectedPage = RadPageViewPage7
        End If

    End Function

    Sub SetGridFormationOFGV1()
        'Dim strItemCode, head2 As String
        CustomerName = gv.CurrentRow.Cells("CustomerName").Value
        May = gv.CurrentRow.Cells("May").Value
        Year = FinYear.Text

        saleinvoice = "Sale Invoice"
        frmdate = "01/05/" + FinYear.Text.Substring(0, FinYear.Text.LastIndexOf("-"))
        todate = "31/05/" + FinYear.Text.Substring(0, FinYear.Text.LastIndexOf("-"))

        frm.ddlSaleType.Text = saleinvoice
        frm.dtpFdate.Value = frmdate
        frm.dtpToDate.Value = todate
        frm.chkLocAll.IsChecked = True
        LoadCustomer()
        frm.chkClassAll.IsChecked = True
        frm.rdbDocSummary.IsChecked = True
        frm.rdbWithFOC.IsChecked = True
        frm.RadGroupBox1.Visible = True
        frm.ddlSaleType.SelectedIndex = 0
        frm.chkMismatch.Checked = True
        frm.rbtnSaleAccount.IsChecked = True
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        If frm.rdbDetail.IsChecked Then
            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 80
            gv1.Columns("Location_Code").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 80
            gv1.Columns("Location_Desc").HeaderText = "Location Name"

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 80
            gv1.Columns("Route_No").HeaderText = "Route No"

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 80
            gv1.Columns("Route_Desc").HeaderText = "Route Desc"

            gv1.Columns("Transfer_Date").IsVisible = False
            gv1.Columns("Transfer_Date").Width = 80
            gv1.Columns("Transfer_Date").HeaderText = "Transfer Date"


            gv1.Columns("Adjustment_Amount").IsVisible = False
            gv1.Columns("Adjustment_Amount").Width = 80
            gv1.Columns("Adjustment_Amount").HeaderText = "Settlement Amount"


            gv1.Columns("Type").IsVisible = True
            gv1.Columns("Type").Width = 80
            gv1.Columns("Type").HeaderText = "Type"

            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").Width = 80
            gv1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Document Date"

            gv1.Columns("DocAmt").IsVisible = True
            gv1.Columns("DocAmt").Width = 100
            gv1.Columns("DocAmt").HeaderText = "Document Amount"

            gv1.Columns("Transfer_No").IsVisible = False
            gv1.Columns("Transfer_No").Width = 80
            gv1.Columns("Transfer_No").HeaderText = "Transfer No"


            gv1.Columns("Customer_Code").IsVisible = True
            gv1.Columns("Customer_Code").Width = 80
            gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 80
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 80
            gv1.Columns("Item_Code").HeaderText = "Item Code"

            gv1.Columns("Unit_Code").IsVisible = True
            gv1.Columns("Unit_Code").Width = 50
            gv1.Columns("Unit_Code").HeaderText = "Unit Code"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 70
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("MRPBottle").IsVisible = False
            gv1.Columns("MRPBottle").Width = 70
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"

            gv1.Columns("TP").IsVisible = False
            gv1.Columns("TP").Width = 70
            gv1.Columns("TP").HeaderText = "TP"

            gv1.Columns("Basic_Rate").IsVisible = True
            gv1.Columns("Basic_Rate").Width = 100
            gv1.Columns("Basic_Rate").HeaderText = "Basic Rate"

            gv1.Columns("Excise").IsVisible = False
            gv1.Columns("Excise").Width = 100
            gv1.Columns("Excise").HeaderText = "Excise"

            gv1.Columns("Cess").IsVisible = False
            gv1.Columns("Cess").Width = 80
            gv1.Columns("Cess").HeaderText = "Cess"

            gv1.Columns("Hcess").IsVisible = False
            gv1.Columns("Hcess").Width = 80
            gv1.Columns("Hcess").HeaderText = "Hcess"

            gv1.Columns("DVAT").IsVisible = False
            gv1.Columns("DVAT").Width = 80
            gv1.Columns("DVAT").HeaderText = "Tax"

            gv1.Columns("TPT Rate").IsVisible = False
            gv1.Columns("TPT Rate").Width = 80
            gv1.Columns("TPT Rate").HeaderText = "TPT Rate"

            gv1.Columns("T.Rate").IsVisible = False
            gv1.Columns("T.Rate").Width = 80
            gv1.Columns("T.Rate").HeaderText = "T.Rate"

            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 80
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("MRPBottle").IsVisible = False
            gv1.Columns("MRPBottle").Width = 70
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"


            gv1.Columns("Margin").IsVisible = False
            gv1.Columns("Margin").Width = 80
            gv1.Columns("Margin").HeaderText = "Margin"

            gv1.Columns("T.Price").IsVisible = False
            gv1.Columns("T.Price").Width = 80
            gv1.Columns("T.Price").HeaderText = "T.Price"

            gv1.Columns("Gross Qty").IsVisible = True
            gv1.Columns("Gross Qty").Width = 80
            gv1.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv1.Columns("Net Qty").IsVisible = True
            gv1.Columns("Net Qty").Width = 80
            gv1.Columns("Net Qty").HeaderText = "Net Qty"

            gv1.Columns("Qty Disc").IsVisible = True
            gv1.Columns("Qty Disc").Width = 80
            gv1.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv1.Columns("FOCAMt").IsVisible = True
            gv1.Columns("FOCAMt").Width = 80
            gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv1.Columns("InvoiceAmt").IsVisible = False
            gv1.Columns("InvoiceAmt").Width = 80
            gv1.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"



            gv1.Columns("Total Basic Amt").IsVisible = True
            gv1.Columns("Total Basic Amt").Width = 80
            gv1.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv1.Columns("Excise Amt").IsVisible = True
            gv1.Columns("Excise Amt").Width = 80
            gv1.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv1.Columns("Cess Amt").IsVisible = True
            gv1.Columns("Cess Amt").Width = 80
            gv1.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv1.Columns("Hcess Amt").IsVisible = True
            gv1.Columns("Hcess Amt").Width = 80
            gv1.Columns("Hcess Amt").HeaderText = "Hcess Amt"


            gv1.Columns("CST").IsVisible = True
            gv1.Columns("CST").Width = 80
            gv1.Columns("CST").HeaderText = "CST"

            gv1.Columns("VAT").IsVisible = True
            gv1.Columns("VAT").Width = 80
            gv1.Columns("VAT").HeaderText = "VAT"

            gv1.Columns("Other Tax").IsVisible = True
            gv1.Columns("Other Tax").Width = 80
            gv1.Columns("Other Tax").HeaderText = "Other Tax"


            gv1.Columns("TPT Amt").IsVisible = False
            gv1.Columns("TPT Amt").Width = 80
            gv1.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv1.Columns("T.Rate Amt").IsVisible = False
            gv1.Columns("T.Rate Amt").Width = 80
            gv1.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv1.Columns("Total MRP").IsVisible = False
            gv1.Columns("Total MRP").Width = 80
            gv1.Columns("Total MRP").HeaderText = "Total MRP"

            gv1.Columns("T.Margin").IsVisible = False
            gv1.Columns("T.Margin").Width = 80
            gv1.Columns("T.Margin").HeaderText = "T.Margin"

            gv1.Columns("T.Price Amt").IsVisible = False
            gv1.Columns("T.Price Amt").Width = 80
            gv1.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv1.Columns("COMMAmt").IsVisible = False
            gv1.Columns("COMMAmt").Width = 80
            gv1.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv1.Columns("DISC").IsVisible = True
            gv1.Columns("DISC").Width = 80
            gv1.Columns("DISC").HeaderText = "Discount"

            gv1.Columns("Sale Account Amt").IsVisible = True
            gv1.Columns("Sale Account Amt").Width = 80
            gv1.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv1.Columns("Exciseamt").IsVisible = False
            gv1.Columns("Exciseamt").Width = 80
            gv1.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"

            gv1.Columns("Total_Cust_Discount").IsVisible = False
            gv1.Columns("Total_Cust_Discount").Width = 80
            gv1.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            gv1.Columns("MICode").IsVisible = True
            gv1.Columns("MICode").Width = 80
            gv1.Columns("MICode").HeaderText = "Main Item"


            gv1.Columns("MIUnit").IsVisible = True
            gv1.Columns("MIUnit").Width = 80
            gv1.Columns("MIUnit").HeaderText = "Main Unit"


            gv1.Columns("mainqty").IsVisible = True
            gv1.Columns("mainqty").Width = 80
            gv1.Columns("mainqty").HeaderText = "Main Qty"


            Try
                gv1.Columns("Category").IsVisible = True
                gv1.Columns("Category").Width = 220
                gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        ElseIf frm.rdbDocSummary.IsChecked Then

            gv1.Columns("Type").IsVisible = True
            gv1.Columns("Type").Width = 100
            gv1.Columns("Type").HeaderText = "Type"

            gv1.Columns("Location_Code").IsVisible = True
            gv1.Columns("Location_Code").Width = 100
            gv1.Columns("Location_Code").HeaderText = "Location"

            gv1.Columns("Location_Desc").IsVisible = True
            gv1.Columns("Location_Desc").Width = 200
            gv1.Columns("Location_Desc").HeaderText = "Location Name"

            gv1.Columns("Route_No").IsVisible = True
            gv1.Columns("Route_No").Width = 100
            gv1.Columns("Route_No").HeaderText = "Route No"

            gv1.Columns("Route_Desc").IsVisible = True
            gv1.Columns("Route_Desc").Width = 200
            gv1.Columns("Route_Desc").HeaderText = "Route Desc"

            gv1.Columns("Transfer_Date").IsVisible = False
            gv1.Columns("Transfer_Date").Width = 100
            gv1.Columns("Transfer_Date").HeaderText = "Transfer Date"


            gv1.Columns("Transfer_No").IsVisible = False
            gv1.Columns("Transfer_No").Width = 100
            gv1.Columns("Transfer_No").HeaderText = "Transfer No"

            gv1.Columns("Adjustment_Amount").IsVisible = False
            gv1.Columns("Adjustment_Amount").Width = 100
            gv1.Columns("Adjustment_Amount").HeaderText = "Settlement Amount"

            gv1.Columns("Sale_Invoice_No").IsVisible = True
            gv1.Columns("Sale_Invoice_No").Width = 100
            gv1.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv1.Columns("DocDate").IsVisible = True
            gv1.Columns("DocDate").Width = 100
            gv1.Columns("DocDate").HeaderText = "Document Date"

            gv1.Columns("DocAmt").IsVisible = True
            gv1.Columns("DocAmt").Width = 100
            gv1.Columns("DocAmt").HeaderText = "Document Amount"

            gv1.Columns("Customer_Code").IsVisible = True
            gv1.Columns("Customer_Code").Width = 100
            gv1.Columns("Customer_Code").HeaderText = "Customer Code"

            gv1.Columns("Customer_Name").IsVisible = True
            gv1.Columns("Customer_Name").Width = 100
            gv1.Columns("Customer_Name").HeaderText = "Customer Name"


            gv1.Columns("Gross Qty").IsVisible = True
            gv1.Columns("Gross Qty").Width = 100
            gv1.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv1.Columns("Net Qty").IsVisible = True
            gv1.Columns("Net Qty").Width = 100
            gv1.Columns("Net Qty").HeaderText = "Net Qty"

            gv1.Columns("Qty Disc").IsVisible = True
            gv1.Columns("Qty Disc").Width = 100
            gv1.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv1.Columns("FOCAMt").IsVisible = True
            gv1.Columns("FOCAMt").Width = 100
            gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv1.Columns("InvoiceAmt").IsVisible = False
            gv1.Columns("InvoiceAmt").Width = 100


            gv1.Columns("Total Basic Amt").IsVisible = True
            gv1.Columns("Total Basic Amt").Width = 100
            gv1.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv1.Columns("Excise Amt").IsVisible = True
            gv1.Columns("Excise Amt").Width = 100
            gv1.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv1.Columns("Cess Amt").IsVisible = True
            gv1.Columns("Cess Amt").Width = 100
            gv1.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv1.Columns("Hcess Amt").IsVisible = True
            gv1.Columns("Hcess Amt").Width = 100
            gv1.Columns("Hcess Amt").HeaderText = "Hcess Amt"


            gv1.Columns("CST").IsVisible = True
            gv1.Columns("CST").Width = 100
            gv1.Columns("CST").HeaderText = "CST"

            gv1.Columns("VAT").IsVisible = True
            gv1.Columns("VAT").Width = 100
            gv1.Columns("VAT").HeaderText = "VAT"

            gv1.Columns("Other Tax").IsVisible = True
            gv1.Columns("Other Tax").Width = 100
            gv1.Columns("Other Tax").HeaderText = "Other Tax"

            gv1.Columns("TPT Amt").IsVisible = False
            gv1.Columns("TPT Amt").Width = 100
            gv1.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv1.Columns("T.Rate Amt").IsVisible = False
            gv1.Columns("T.Rate Amt").Width = 100
            gv1.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv1.Columns("Total MRP").IsVisible = False
            gv1.Columns("Total MRP").Width = 100
            gv1.Columns("Total MRP").HeaderText = "Total MRP"

            gv1.Columns("T.Margin").IsVisible = False
            gv1.Columns("T.Margin").Width = 100
            gv1.Columns("T.Margin").HeaderText = "T.Margin"

            gv1.Columns("T.Price Amt").IsVisible = False
            gv1.Columns("T.Price Amt").Width = 80
            gv1.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv1.Columns("COMMAmt").IsVisible = False
            gv1.Columns("COMMAmt").Width = 100
            gv1.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv1.Columns("DISC").IsVisible = True
            gv1.Columns("DISC").Width = 100
            gv1.Columns("DISC").HeaderText = "DISC"

            gv1.Columns("Sale Account Amt").IsVisible = True
            gv1.Columns("Sale Account Amt").Width = 100
            gv1.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv1.Columns("Exciseamt").IsVisible = False
            gv1.Columns("Exciseamt").Width = 100
            gv1.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"


            gv1.Columns("Total_Cust_Discount").IsVisible = False
            gv1.Columns("Total_Cust_Discount").Width = 100
            gv1.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            Try
                gv1.Columns("Category").IsVisible = True
                gv1.Columns("Category").Width = 220
                gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        ElseIf frm.rdbItemSummary.IsChecked Then

            gv1.Columns("Item_Code").IsVisible = True
            gv1.Columns("Item_Code").Width = 80
            gv1.Columns("Item_Code").HeaderText = "Item Code"


            gv1.Columns("MRP").IsVisible = True
            gv1.Columns("MRP").Width = 70
            gv1.Columns("MRP").HeaderText = "MRP"

            gv1.Columns("MRPBottle").IsVisible = False
            gv1.Columns("MRPBottle").Width = 70
            gv1.Columns("MRPBottle").HeaderText = "MRPBottle"



            gv1.Columns("DocAmt").IsVisible = True
            gv1.Columns("DocAmt").Width = 100
            gv1.Columns("DocAmt").HeaderText = "Document Amount"


            gv1.Columns("Gross Qty").IsVisible = True
            gv1.Columns("Gross Qty").Width = 80
            gv1.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv1.Columns("Net Qty").IsVisible = True
            gv1.Columns("Net Qty").Width = 80
            gv1.Columns("Net Qty").HeaderText = "Net Qty"

            gv1.Columns("Qty Disc").IsVisible = True
            gv1.Columns("Qty Disc").Width = 80
            gv1.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv1.Columns("FOCAMt").IsVisible = True
            gv1.Columns("FOCAMt").Width = 80
            gv1.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv1.Columns("InvoiceAmt").IsVisible = False
            gv1.Columns("InvoiceAmt").Width = 80


            gv1.Columns("Total Basic Amt").IsVisible = True
            gv1.Columns("Total Basic Amt").Width = 80
            gv1.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv1.Columns("Excise Amt").IsVisible = True
            gv1.Columns("Excise Amt").Width = 80
            gv1.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv1.Columns("Cess Amt").IsVisible = True
            gv1.Columns("Cess Amt").Width = 80
            gv1.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv1.Columns("Hcess Amt").IsVisible = True
            gv1.Columns("Hcess Amt").Width = 80
            gv1.Columns("Hcess Amt").HeaderText = "Hcess Amt"


            gv1.Columns("CST").IsVisible = True
            gv1.Columns("CST").Width = 80
            gv1.Columns("CST").HeaderText = "CST"

            gv1.Columns("VAT").IsVisible = True
            gv1.Columns("VAT").Width = 80
            gv1.Columns("VAT").HeaderText = "VAT"

            gv1.Columns("Other Tax").IsVisible = True
            gv1.Columns("Other Tax").Width = 80
            gv1.Columns("Other Tax").HeaderText = "Other Tax"

            gv1.Columns("TPT Amt").IsVisible = False
            gv1.Columns("TPT Amt").Width = 80
            gv1.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv1.Columns("T.Rate Amt").IsVisible = False
            gv1.Columns("T.Rate Amt").Width = 80
            gv1.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv1.Columns("Total MRP").IsVisible = False
            gv1.Columns("Total MRP").Width = 80
            gv1.Columns("Total MRP").HeaderText = "Total MRP"

            gv1.Columns("T.Margin").IsVisible = False
            gv1.Columns("T.Margin").Width = 80
            gv1.Columns("T.Margin").HeaderText = "T.Margin"

            gv1.Columns("T.Price Amt").IsVisible = False
            gv1.Columns("T.Price Amt").Width = 80
            gv1.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv1.Columns("COMMAmt").IsVisible = False
            gv1.Columns("COMMAmt").Width = 80
            gv1.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv1.Columns("DISC").IsVisible = True
            gv1.Columns("DISC").Width = 80
            gv1.Columns("DISC").HeaderText = "Discount"

            gv1.Columns("Sale Account Amt").IsVisible = True
            gv1.Columns("Sale Account Amt").Width = 80
            gv1.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv1.Columns("Total_Cust_Discount").IsVisible = False
            gv1.Columns("Total_Cust_Discount").Width = 80
            gv1.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            gv1.Columns("Exciseamt").IsVisible = False
            gv1.Columns("Exciseamt").Width = 80
            gv1.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"

            Try
                gv1.Columns("Category").IsVisible = True
                gv1.Columns("Category").Width = 220
                gv1.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Gross Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Net Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Qty Disc", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Total Basic Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Excise Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("Cess Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Hcess Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("CST", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8)
        Dim item8a As New GridViewSummaryItem("VAT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8a)
        Dim item8b As New GridViewSummaryItem("Other Tax", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item8b)

        Dim item9 As New GridViewSummaryItem("TPT Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("T.Rate Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Total MRP", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim item12 As New GridViewSummaryItem("T.Margin", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("T.Price Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        Dim item14 As New GridViewSummaryItem("COMMAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item14)
        Dim item15 As New GridViewSummaryItem("DISC", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item15)
        Dim item16 As New GridViewSummaryItem("FOCAMt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item16)
        Dim item17 As New GridViewSummaryItem("Sale Account Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item17)
        Dim item18 As New GridViewSummaryItem("Total_Cust_Discount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item18)
        Dim item20 As New GridViewSummaryItem("Exciseamt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item20)
        Dim item21 As New GridViewSummaryItem("DocAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item21)
        Dim item22 As New GridViewSummaryItem("mainqty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item22)
        Dim item23 As New GridViewSummaryItem("InvoiceAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item23)


        If frm.rdbItemSummary.IsChecked = False Then
            Dim item19 As New GridViewSummaryItem("Adjustment_Amount", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(item19)
        End If

        If frm.rdbDocSummary.IsChecked AndAlso frm.pnlAdminSetting.Visible AndAlso frm.chkReconcile.Checked Then
            gv1.Columns("SubledgerAmt").IsVisible = True
            gv1.Columns("SubledgerAmt").Width = 100
            gv1.Columns("SubledgerAmt").HeaderText = "Subledger Amt"

            Dim emptydr As New GridViewSummaryItem("SubledgerAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(emptydr)


        End If

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Public Function GetReportID() As String

        If frm.rdbItemSummary.IsChecked Then
            Return "SAleRecoDiscounSummary"
        ElseIf frm.rdbDocSummary.IsChecked Then
            Return "SAleRecoDocSummary"
        Else
            Return "SAleRecoDetails"
        End If


    End Function

    'Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)
    '    If frm.rdbDocSummary.IsChecked Then
    '        If gv1.Rows.Count > 0 Then
    '            Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Type").Value)
    '            Dim strDoc = gv1.CurrentRow.Cells("Sale_Invoice_No").Value
    '            If strTransType = "Sale Invoice" Then
    '                strTransType = "SD-IN"
    '            Else
    '                strTransType = "Sale Return"
    '            End If
    '            Select Case strTransType
    '                Case "SD-IN"
    '                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
    '                Case "Sale Return"
    '                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strDoc)
    '            End Select
    '        End If
    '    End If
    'End Sub
    Public Function printDrillDownOnYearMnthWise() As DataTable
        rbtnCustAll.IsChecked = True
        rbtnLocAll.IsChecked = True
        Dim strCustomer As String = ""
        Dim strLocation As String = ""
        Dim strFinYear As String = ""
        Dim qry1 As String = ""
        Dim qry2 As String = ""
        Dim qry3 As String = ""
        Dim qry As String = ""
        Dim arr As New List(Of String)
        arr.Add("COMPANY-WISE MONTH-WISE SALES VALUE ANALYSIS")
        arr.Add("Sales Summary, " + clsCommon.myCstr(FinYear.Text))

        'If cbgCustomer.CheckedValue.Count > 0 Then
        '    strCustomer += " where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        'Else
        '    strCustomer = ""
        'End If

        'If clsCommon.CompairString(FinYear.Text, "") <> CompairStringResult.Equal Then
        '    strFinYear += " where convert(varchar,year) = '" + clsCommon.myCstr(FinYear.Text) + "' "

        'Else
        '    strFinYear = ""

        'End If

        'If cbgLocation.CheckedValue.Count > 0 Then
        '    strLocation += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        'Else
        '    strLocation = ""
        'End If
        qry1 = " SELECT convert(varchar,ROW_NUMBER () over(order by [customer name])) as [SL NO]  ,[Customer Name],[Customer Code] , ISNULL(April ,0) as April, ISNULL(May ,0) as May, isnull(June ,0) as June , isnull(July ,0) as July,isnull(August ,0) as August, ISNULL(September ,0) as September, ISNULL(October ,0) as October, isnull(November ,0)as November, isnull(December ,0) as December, isnull(January ,0) as January, isnull(February ,0) as February, isnull(March ,0) as March, (ISNULL(April ,0)+ISNULL(May ,0)+isnull(June ,0)+isnull(July ,0)+isnull(August ,0)+ISNULL(September ,0)+ISNULL(October ,0)+isnull(November ,0)+isnull(December ,0)+isnull(January ,0)+isnull(February ,0)+isnull(March ,0)) as Total FROM (  select convert(varchar,YEAR) + '-' + CONVERT(varchar,YEAR+1) as YEAR,[Customer Name],[Customer Code]  ,MONTH,Amount  from(      "
        qry2 = " SELECT    case when MONTH(TSPL_SD_SALE_INVOICE_HEAD.document_date)<=3 then year(TSPL_SD_SALE_INVOICE_HEAD.document_date)-1 else year(TSPL_SD_SALE_INVOICE_HEAD.document_date) end YEAR,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code as [Customer Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],  upper(datename(month,TSPL_SD_SALE_INVOICE_HEAD.document_date)) as MONTH,  TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as AMOUNT  FROM TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code     "
        qry3 = " )T1 ) as s PIVOT  (  SUM(Amount) FOR [month] IN (  April, May, June, July,August, September, October, November, December, January, February, March  )  )AS p   "
        qry2 += strCustomer
        qry3 += strFinYear
        qry2 += strLocation
        qry = qry1 + qry2 + qry3
        dt = clsDBFuncationality.GetDataTable(qry)
        Return dt

    End Function


    Private Sub gv2_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs)

    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If frm.rdbDocSummary.IsChecked Then
            If gv1.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv1.CurrentRow.Cells("Type").Value)
                Dim strDoc = gv1.CurrentRow.Cells("Sale_Invoice_No").Value
                If strTransType = "Sale Invoice" Then
                    strTransType = "SD-IN"
                Else
                    strTransType = "Sale Return"
                End If
                Select Case strTransType
                    Case "SD-IN"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, strDoc)
                    Case "Sale Return"
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleReturn, strDoc)
                End Select
            End If
        End If
    End Sub
End Class
