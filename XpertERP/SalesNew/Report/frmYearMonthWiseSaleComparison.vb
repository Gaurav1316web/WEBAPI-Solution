'' Anubhooti(2-July-2014) Added Export Permission Against BM00000003016 ''''''''

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
Public Class frmYearMonthWiseSaleComparison
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable = Nothing
    Dim frm As New frmSalesAnalysisReport(strUserCode, strCompany)
    Dim Year, CustomerName, May As String
    Dim frmdate As String = ""
    Dim todate As String = ""
    Dim saleinvoice As String

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmYearMonthWiseSaleComparison)
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
        rbtnFinAll.IsChecked = True
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub rbtnFinAllSelectClick() Handles rbtnFinAll.ToggleStateChanged, rbtnFinSelect.ToggleStateChanged
        Try
            If rbtnFinAll.IsChecked Then
                cbgFinancialYear.CheckedAll()
                cbgFinancialYear.Enabled = False
            Else
                cbgFinancialYear.UnCheckedAll()
                cbgFinancialYear.Enabled = True
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadLocation()
        Try
            Dim qry As String = "select Location_Code as Location,Location_Desc as [Location Description] from TSPL_LOCATION_MASTER "
            cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgLocation.ValueMember = "Location"
            cbgLocation.DisplayMember = "Location Description"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadCustomer()
        Try
            Dim qry As String = "select Cust_Code as Code,Customer_Name as [Customer Name] from TSPL_Customer_MASTER  "
            cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgCustomer.ValueMember = "Code"
            cbgCustomer.DisplayMember = "Customer Name"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadFinYear()
        Try
            Dim qry As String = "select CONVERT (varchar,year) + '-' + CONVERT(varchar,year+1) as Year from  (select  distinct  case when MONTH(TSPL_SD_SALE_INVOICE_HEAD.document_date)<=3 then year(TSPL_SD_SALE_INVOICE_HEAD.document_date)-1 else year(TSPL_SD_SALE_INVOICE_HEAD.document_date) end as YEAR from TSPL_SD_SALE_INVOICE_HEAD  ) as yyy "
            cbgFinancialYear.DataSource = clsDBFuncationality.GetDataTable(qry)
            cbgFinancialYear.ValueMember = "Year"
            cbgFinancialYear.DisplayMember = "Year"
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
        arr.Add("YEAR-WISE MONTH-WISE SALES VALUE ANALYSIS")
        If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then
            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " where TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If

            If cbgFinancialYear.CheckedValue.Count > 0 Then
                strFinYear += " where convert(varchar,year) in (" + clsCommon.GetMulcallString(cbgFinancialYear.CheckedValue) + ") "

            Else
                strFinYear = ""

            End If

            If cbgLocation.CheckedValue.Count > 0 Then
                strLocation += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLocation = ""
            End If

            qry1 = " SELECT YEAR , ISNULL(APR,0) as APR, ISNULL(MAY,0) as MAY, isnull(JUN,0) as JUN, isnull(JUL,0) as JUL,isnull(AUG,0) as AUG, ISNULL(SEP,0) as SEP, ISNULL(OCT,0) as OCT, isnull(NOV,0)as NOV, isnull(DEC,0) as DEC, isnull(JAN,0) as JAN, isnull(FEB,0) as FEB, isnull(MAR,0) as MAR FROM ( select convert(varchar,YEAR) + '-' + CONVERT(varchar,YEAR+1) as YEAR,MONTH,Amount  from (  "
            qry2 = " SELECT  case when MONTH(TSPL_SD_SALE_INVOICE_HEAD.document_date)<=3 then year(TSPL_SD_SALE_INVOICE_HEAD.document_date)-1 else year(TSPL_SD_SALE_INVOICE_HEAD.document_date) end YEAR,  upper(left(datename(month,TSPL_SD_SALE_INVOICE_HEAD.document_date),3)) as MONTH,  TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as AMOUNT  FROM TSPL_SD_SALE_INVOICE_HEAD  "
            qry3 = " )T1  ) as s  PIVOT  (  SUM(Amount) FOR [month] IN (  APR, MAY, JUN, JUL,AUG, SEP, OCT, NOV, DEC, JAN, FEB, MAR  )  )AS p "

            qry2 += strCustomer
            qry3 += strFinYear
            qry2 += strLocation
            qry = qry1 + qry2 + qry3

            dt = clsDBFuncationality.GetDataTable(qry)
            gv.Rows.Clear()
            gv.Columns.Clear()
            loadBlankgrid()
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
            Dim valJanCum As Double = 0
            Dim valFebCum As Double = 0
            Dim valMarCum As Double = 0
            Dim valAprCum As Double = 0
            Dim valMayCum As Double = 0
            Dim valJunCum As Double = 0
            Dim valJulCum As Double = 0
            Dim valAugCum As Double = 0
            Dim valSepCum As Double = 0
            Dim valOctCum As Double = 0
            Dim valNovCum As Double = 0
            Dim valDecCum As Double = 0
            Dim valJanCum1 As Double = 0
            Dim valFebCum1 As Double = 0
            Dim valMarCum1 As Double = 0
            Dim valAprCum1 As Double = 0
            Dim valMayCum1 As Double = 0
            Dim valJunCum1 As Double = 0
            Dim valJulCum1 As Double = 0
            Dim valAugCum1 As Double = 0
            Dim valSepCum1 As Double = 0
            Dim valOctCum1 As Double = 0
            Dim valNovCum1 As Double = 0
            Dim valDecCum1 As Double = 0

            Dim valGP As Double = 0
            Dim valNP As Double = 0
            Dim dt1 As DataTable
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else

                For i As Integer = 0 To dt.Rows.Count - 1
                    valApr = clsCommon.myCdbl(dt.Rows(i)("APR"))
                    valMay = clsCommon.myCdbl(dt.Rows(i)("MAY"))
                    valJun = clsCommon.myCdbl(dt.Rows(i)("JUN"))
                    valJul = clsCommon.myCdbl(dt.Rows(i)("JUL"))
                    valAug = clsCommon.myCdbl(dt.Rows(i)("AUG"))
                    valSep = clsCommon.myCdbl(dt.Rows(i)("SEP"))
                    valOct = clsCommon.myCdbl(dt.Rows(i)("OCT"))
                    valNov = clsCommon.myCdbl(dt.Rows(i)("NOV"))
                    valDec = clsCommon.myCdbl(dt.Rows(i)("DEC"))
                    valJan = clsCommon.myCdbl(dt.Rows(i)("JAN"))
                    valFeb = clsCommon.myCdbl(dt.Rows(i)("FEB"))
                    valMar = clsCommon.myCdbl(dt.Rows(i)("MAR"))
                    tot = valApr + valMay + valJun + valJul + valAug + valSep + valOct + valNov + valDec + valJan + valFeb + valMar
                    gv.Rows.Add(dt.Rows(i)("YEAR").ToString, "ACTUAL", valApr, valMay, valJun, valJul, valAug, valSep, valOct, valNov, valDec, valJan, valFeb, valMar, tot, valGP, valNP)
                    valAprCum = valApr
                    valMayCum = valAprCum + valMay
                    valJunCum = valMayCum + valJun
                    valJulCum = valJunCum + valJul
                    valAugCum = valJulCum + valAug
                    valSepCum = valAugCum + valSep
                    valOctCum = valSepCum + valOct
                    valNovCum = valOctCum + valNov
                    valDecCum = valNovCum + valDec
                    valJanCum = valDecCum + valJan
                    valFebCum = valJanCum + valFeb
                    valMarCum = valFebCum + valMar
                    gv.Rows.Add("", "CUMULATIVE", valAprCum, valMayCum, valJunCum, valJulCum, valAugCum, valSepCum, valOctCum, valNovCum, valDecCum, valJanCum, valFebCum, valMarCum, "", "", "")
                    dt1 = clsDBFuncationality.GetDataTable("SELECT YEAR , ISNULL(APR,0) as APR, ISNULL(MAY,0) as MAY, isnull(JUN,0) as JUN, isnull(JUL,0) as JUL,isnull(AUG,0) as AUG, ISNULL(SEP,0) as SEP, ISNULL(OCT,0) as OCT, isnull(NOV,0)as NOV, isnull(DEC,0) as DEC, isnull(JAN,0) as JAN, isnull(FEB,0) as FEB, isnull(MAR,0) as MAR FROM ( select convert(varchar,YEAR) + '-' + CONVERT(varchar,YEAR+1) as YEAR,MONTH,Amount  from (  SELECT  case when MONTH(TSPL_SD_SALE_INVOICE_HEAD.document_date)<=3 then year(TSPL_SD_SALE_INVOICE_HEAD.document_date)-1 else year(TSPL_SD_SALE_INVOICE_HEAD.document_date) end YEAR,  upper(left(datename(month,TSPL_SD_SALE_INVOICE_HEAD.document_date),3)) as MONTH,  TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as AMOUNT  FROM TSPL_SD_SALE_INVOICE_HEAD  )T1  ) as s  PIVOT  (  SUM(Amount) FOR [month] IN (  APR, MAY, JUN, JUL,AUG, SEP, OCT, NOV, DEC, JAN, FEB, MAR  )  )AS p where year='" & clsCommon.myCdbl(Microsoft.VisualBasic.Left(dt.Rows(i)("YEAR").ToString, 4)) - 1 & "-" & clsCommon.myCdbl(Microsoft.VisualBasic.Right(dt.Rows(i)("YEAR").ToString, 4)) - 1 & "'")
                    If dt1.Rows.Count > 0 Then
                        valApr1 = clsCommon.myCdbl(dt1.Rows(0)("APR"))
                        valMay1 = clsCommon.myCdbl(dt1.Rows(0)("MAY"))
                        valJun1 = clsCommon.myCdbl(dt1.Rows(0)("JUN"))
                        valJul1 = clsCommon.myCdbl(dt1.Rows(0)("JUL"))
                        valAug1 = clsCommon.myCdbl(dt1.Rows(0)("AUG"))
                        valSep1 = clsCommon.myCdbl(dt1.Rows(0)("SEP"))
                        valOct1 = clsCommon.myCdbl(dt1.Rows(0)("OCT"))
                        valNov1 = clsCommon.myCdbl(dt1.Rows(0)("NOV"))
                        valDec1 = clsCommon.myCdbl(dt1.Rows(0)("DEC"))
                        valJan1 = clsCommon.myCdbl(dt1.Rows(0)("JAN"))
                        valFeb1 = clsCommon.myCdbl(dt1.Rows(0)("FEB"))
                        valMar1 = clsCommon.myCdbl(dt1.Rows(0)("MAR"))
                        valAprCum1 = valApr1
                        valMayCum1 = valAprCum1 + valMay1
                        valJunCum1 = valMayCum1 + valJun1
                        valJulCum1 = valJunCum1 + valJul1
                        valAugCum1 = valJulCum1 + valAug1
                        valSepCum1 = valAugCum1 + valSep1
                        valOctCum1 = valSepCum1 + valOct1
                        valNovCum1 = valOctCum1 + valNov1
                        valDecCum1 = valNovCum1 + valDec1
                        valJanCum1 = valDecCum1 + valJan1
                        valFebCum1 = valJanCum1 + valFeb1
                        valMarCum1 = valFebCum1 + valMar1
                        tot1 = valApr1 + valMay1 + valJun1 + valJul1 + valAug1 + valSep1 + valOct1 + valNov1 + valDec1 + valJan1 + valFeb1 + valMar1
                    Else
                        valApr1 = 0
                        valMay1 = 0
                        valJun1 = 0
                        valJul1 = 0
                        valAug1 = 0
                        valSep1 = 0
                        valOct1 = 0
                        valNov1 = 0
                        valDec1 = 0
                        valJan1 = 0
                        valFeb1 = 0
                        valMar1 = 0
                        valAprCum1 = 0
                        valMayCum1 = 0
                        valJunCum1 = 0
                        valJulCum1 = 0
                        valAugCum1 = 0
                        valSepCum1 = 0
                        valOctCum1 = 0
                        valNovCum1 = 0
                        valDecCum1 = 0
                        valJanCum1 = 0
                        valFebCum1 = 0
                        valMarCum1 = 0
                        tot1 = 0
                    End If
                    gv.Rows.Add("", "% GROWTH-ACT", Math.Round(clsCommon.myCdbl(((valApr - valApr1) / valApr1) * 100), 2), Math.Round(clsCommon.myCdbl(((valMay - valMay1) / valMay1) * 100), 2), Math.Round(clsCommon.myCdbl(((valJun - valJun1) / valJun1) * 100), 2), Math.Round(clsCommon.myCdbl(((valJul - valJul1) / valJul1) * 100), 2), Math.Round(clsCommon.myCdbl(((valAug - valAug1) / valAug1) * 100), 2), Math.Round(clsCommon.myCdbl(((valSep - valSep1) / valSep1) * 100), 2), Math.Round(clsCommon.myCdbl(((valOct - valOct1) / valOct1) * 100), 2), Math.Round(clsCommon.myCdbl(((valNov - valNov1) / valNov1) * 100), 2), Math.Round(clsCommon.myCdbl(((valDec - valDec1) / valDec1) * 100), 2), Math.Round(clsCommon.myCdbl(((valJan - valJan1) / valJan1) * 100), 2), Math.Round(clsCommon.myCdbl(((valFeb - valFeb1) / valFeb1) * 100), 2), Math.Round(clsCommon.myCdbl(((valMar - valMar1) / valMar1) * 100), 2), Math.Round(clsCommon.myCdbl(((tot - tot1) / tot1) * 100), 2), "", "")

                    gv.Rows.Add("", "-CUM", Math.Round(clsCommon.myCdbl(((valAprCum - valAprCum1) / valAprCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valMayCum - valMayCum1) / valMayCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valJunCum - valJunCum1) / valJunCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valJulCum - valJulCum1) / valJulCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valAugCum - valAugCum1) / valAugCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valSepCum - valSepCum1) / valSepCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valOctCum - valOctCum1) / valOctCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valNovCum - valNovCum1) / valNovCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valDecCum - valDecCum1) / valDecCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valJanCum - valJanCum1) / valJanCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valFebCum - valFebCum1) / valFebCum1) * 100), 2), Math.Round(clsCommon.myCdbl(((valMarCum - valMarCum1) / valMarCum1) * 100), 2), "", "", "")
                    If i <> dt.Rows.Count - 1 Then gv.Rows.Add(".", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "")
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        ElseIf IsPrint = Exporter.Excel Then
            clsCommon.MyExportToExcelGrid(objCommonVar.CurrentCompanyName, gv, arr, Me.Text)
        Else
            clsCommon.MyExportToPDF(objCommonVar.CurrentCompanyName, gv, arr, Me.Text, True)
        End If

    End Sub

    Sub loadBlankgrid()
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.Columns.Add("YEAR", "YEAR")
        gv.Columns.Add("MONTH", "MONTH")
        gv.Columns.Add("APRIL", "APRIL")
        gv.Columns.Add("MAY", "MAY")
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
        gv.Columns.Add("GP", "G.P./T.O.")
        gv.Columns.Add("NP", "N.P./T.O")
        gv.Columns("YEAR").TextAlignment = ContentAlignment.MiddleCenter
        gv.Columns("MONTH").TextAlignment = ContentAlignment.MiddleLeft
        gv.Columns("APRIL").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("MAY").TextAlignment = ContentAlignment.MiddleRight
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
        gv.Columns("GP").TextAlignment = ContentAlignment.MiddleRight
        gv.Columns("NP").TextAlignment = ContentAlignment.MiddleRight

    End Sub

    Private Sub rbtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnReset.Click
        Try
            rbtnCustAll.IsChecked = True
            rbtnFinAll.IsChecked = True
            rbtnLocAll.IsChecked = True
            gv.DataSource = Nothing
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Function checkSelection() As Boolean
        If cbgCustomer.CheckedValue.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Customer", Me.Text)
            Return False
        ElseIf cbgFinancialYear.CheckedValue.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Atlease one item from financial Year", Me.Text)
            Return False
        ElseIf cbgLocation.CheckedValue.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Atleast One Location", Me.Text)
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
        If checkSelection() AndAlso gv IsNot Nothing Then
            LoadData(Exporter.Excel)
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found To Export", Me.Text)
        End If
    End Sub

    Private Sub btnPdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPdf.Click
        If checkSelection() AndAlso gv IsNot Nothing Then
            LoadData(Exporter.PDF)
        Else

            clsCommon.MyMessageBoxShow(Me, "No Data Found To Export", Me.Text)
        End If
    End Sub


    Private Sub btnPrint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint1.Click

    End Sub

    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

    End Sub
    Function AutoCompanyMnthWise() As Boolean
        Dim frm As New frmCompanyMonthWiseSaleComparison()
        frm.SetUserMgmt(clsUserMgtCode.frmCompanyMonthWiseSaleComparison)
        Dim dt As DataTable = frm.printDrillDownOnYearMnthWise()
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Function
        Else
            gv1.DataSource = dt

            RadPageView1.SelectedPage = RadPageViewPage3
        End If
    End Function

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        AutoCompanyMnthWise()
    End Sub
    Function AutoSaleAnalysisFromCmpMnth() As Boolean
        Dim arr As New ArrayList()
        Dim frm As New frmSalesAnalysisReport(strUserCode, strCompany)
        frm.SetUserMgmt(clsUserMgtCode.rptSalesAnalysis)
        Dim CustomerName, May As String
        Dim frmdate As String = ""
        Dim todate As String = ""
        Dim saleinvoice As String
        'Dim finyear As String = ""
        'finyear = clsCommon.myCstr(gv1.CurrentRow.Cells("").Value)
        CustomerName = gv1.CurrentRow.Cells("Customer Code").Value
        May = gv1.CurrentRow.Cells("May").Value

        'Year = FinYear.Text

        saleinvoice = "Sale Invoice"

        frmdate = "01/05/2014"
        todate = "31/05/2014"

        frm.LoadCustomer()
        frm.LoadCategory()
        frm.LoadLocation()
        frm.ddlSaleType.Text = saleinvoice
        frm.dtpFdate.Value = frmdate
        frm.dtpToDate.Value = todate
        frm.chkLocAll.IsChecked = True
        LoadCustomer()
        frm.chkClassSelect.IsChecked = True
        frm.rdbDocSummary.IsChecked = True
        frm.rdbWithFOC.IsChecked = True
        frm.RadGroupBox1.Visible = True
        frm.ddlSaleType.SelectedIndex = 0
        frm.chkMismatch.Checked = True
        frm.rbtnSaleAccount.IsChecked = True
        arr.Add(CustomerName)
        frm.chkCustomer.CheckedValue = arr

        Dim dt As DataTable = frm.PrintDrillDown()
        gv2.DataSource = Nothing
        gv2.Columns.Clear()
        gv2.Rows.Clear()
        gv2.GroupDescriptors.Clear()
        gv2.MasterTemplate.SummaryRowsBottom.Clear()

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Function
        Else
            gv2.DataSource = dt
            SetGridFormationOFGV1()
            ReStoreGridLayout()
            RadPageView1.SelectedPage = RadPageViewPage4
        End If

    End Function
    Sub SetGridFormationOFGV1()
        '  Dim strItemCode, head2 As String
        CustomerName = gv1.CurrentRow.Cells("Customer Code").Value
        May = gv1.CurrentRow.Cells("May").Value
        'Year = FinYear.Text

        saleinvoice = "Sale Invoice"
        frmdate = "01/05/2014"
        todate = "31/05/2014"

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
        gv2.TableElement.TableHeaderHeight = 40
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = False
        Next

        If frm.rdbDetail.IsChecked Then
            gv2.Columns("Location_Code").IsVisible = True
            gv2.Columns("Location_Code").Width = 80
            gv2.Columns("Location_Code").HeaderText = "Location"

            gv2.Columns("Location_Desc").IsVisible = True
            gv2.Columns("Location_Desc").Width = 80
            gv2.Columns("Location_Desc").HeaderText = "Location Name"

            gv2.Columns("Route_No").IsVisible = True
            gv2.Columns("Route_No").Width = 80
            gv2.Columns("Route_No").HeaderText = "Route No"

            gv2.Columns("Route_Desc").IsVisible = True
            gv2.Columns("Route_Desc").Width = 80
            gv2.Columns("Route_Desc").HeaderText = "Route Desc"

            gv2.Columns("Transfer_Date").IsVisible = False
            gv2.Columns("Transfer_Date").Width = 80
            gv2.Columns("Transfer_Date").HeaderText = "Transfer Date"


            gv2.Columns("Adjustment_Amount").IsVisible = False
            gv2.Columns("Adjustment_Amount").Width = 80
            gv2.Columns("Adjustment_Amount").HeaderText = "Settlement Amount"


            gv2.Columns("Type").IsVisible = True
            gv2.Columns("Type").Width = 80
            gv2.Columns("Type").HeaderText = "Type"

            gv2.Columns("Sale_Invoice_No").IsVisible = True
            gv2.Columns("Sale_Invoice_No").Width = 80
            gv2.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv2.Columns("DocDate").IsVisible = True
            gv2.Columns("DocDate").Width = 100
            gv2.Columns("DocDate").HeaderText = "Document Date"

            gv2.Columns("DocAmt").IsVisible = True
            gv2.Columns("DocAmt").Width = 100
            gv2.Columns("DocAmt").HeaderText = "Document Amount"

            gv2.Columns("Transfer_No").IsVisible = False
            gv2.Columns("Transfer_No").Width = 80
            gv2.Columns("Transfer_No").HeaderText = "Transfer No"


            gv2.Columns("Customer_Code").IsVisible = False
            gv2.Columns("Customer_Code").Width = 80
            gv2.Columns("Customer_Code").HeaderText = "Customer Code"

            gv2.Columns("Customer_Name").IsVisible = True
            gv2.Columns("Customer_Name").Width = 80
            gv2.Columns("Customer_Name").HeaderText = ""

            gv2.Columns("Item_Code").IsVisible = True
            gv2.Columns("Item_Code").Width = 80
            gv2.Columns("Item_Code").HeaderText = "Item Code"

            gv2.Columns("Unit_Code").IsVisible = True
            gv2.Columns("Unit_Code").Width = 50
            gv2.Columns("Unit_Code").HeaderText = "Unit Code"

            gv2.Columns("MRP").IsVisible = True
            gv2.Columns("MRP").Width = 70
            gv2.Columns("MRP").HeaderText = "MRP"

            gv2.Columns("MRPBottle").IsVisible = False
            gv2.Columns("MRPBottle").Width = 70
            gv2.Columns("MRPBottle").HeaderText = "MRPBottle"

            gv2.Columns("TP").IsVisible = False
            gv2.Columns("TP").Width = 70
            gv2.Columns("TP").HeaderText = "TP"

            gv2.Columns("Basic_Rate").IsVisible = True
            gv2.Columns("Basic_Rate").Width = 100
            gv2.Columns("Basic_Rate").HeaderText = "Basic Rate"

            gv2.Columns("Excise").IsVisible = False
            gv2.Columns("Excise").Width = 100
            gv2.Columns("Excise").HeaderText = "Excise"

            gv2.Columns("Cess").IsVisible = False
            gv2.Columns("Cess").Width = 80
            gv2.Columns("Cess").HeaderText = "Cess"

            gv2.Columns("Hcess").IsVisible = False
            gv2.Columns("Hcess").Width = 80
            gv2.Columns("Hcess").HeaderText = "Hcess"

            gv2.Columns("DVAT").IsVisible = False
            gv2.Columns("DVAT").Width = 80
            gv2.Columns("DVAT").HeaderText = "Tax"

            gv2.Columns("TPT Rate").IsVisible = False
            gv2.Columns("TPT Rate").Width = 80
            gv2.Columns("TPT Rate").HeaderText = "TPT Rate"

            gv2.Columns("T.Rate").IsVisible = False
            gv2.Columns("T.Rate").Width = 80
            gv2.Columns("T.Rate").HeaderText = "T.Rate"

            gv2.Columns("MRP").IsVisible = True
            gv2.Columns("MRP").Width = 80
            gv2.Columns("MRP").HeaderText = "MRP"

            gv2.Columns("MRPBottle").IsVisible = False
            gv2.Columns("MRPBottle").Width = 70
            gv2.Columns("MRPBottle").HeaderText = "MRPBottle"


            gv2.Columns("Margin").IsVisible = False
            gv2.Columns("Margin").Width = 80
            gv2.Columns("Margin").HeaderText = "Margin"

            gv2.Columns("T.Price").IsVisible = False
            gv2.Columns("T.Price").Width = 80
            gv2.Columns("T.Price").HeaderText = "T.Price"

            gv2.Columns("Gross Qty").IsVisible = True
            gv2.Columns("Gross Qty").Width = 80
            gv2.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv2.Columns("Net Qty").IsVisible = True
            gv2.Columns("Net Qty").Width = 80
            gv2.Columns("Net Qty").HeaderText = "Net Qty"

            gv2.Columns("Qty Disc").IsVisible = True
            gv2.Columns("Qty Disc").Width = 80
            gv2.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv2.Columns("FOCAMt").IsVisible = True
            gv2.Columns("FOCAMt").Width = 80
            gv2.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv2.Columns("InvoiceAmt").IsVisible = False
            gv2.Columns("InvoiceAmt").Width = 80
            gv2.Columns("InvoiceAmt").HeaderText = "Trade Discount Amt"



            gv2.Columns("Total Basic Amt").IsVisible = True
            gv2.Columns("Total Basic Amt").Width = 80
            gv2.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv2.Columns("Excise Amt").IsVisible = True
            gv2.Columns("Excise Amt").Width = 80
            gv2.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv2.Columns("Cess Amt").IsVisible = True
            gv2.Columns("Cess Amt").Width = 80
            gv2.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv2.Columns("Hcess Amt").IsVisible = True
            gv2.Columns("Hcess Amt").Width = 80
            gv2.Columns("Hcess Amt").HeaderText = "Hcess Amt"


            gv2.Columns("CST").IsVisible = True
            gv2.Columns("CST").Width = 80
            gv2.Columns("CST").HeaderText = "CST"

            gv2.Columns("VAT").IsVisible = True
            gv2.Columns("VAT").Width = 80
            gv2.Columns("VAT").HeaderText = "VAT"

            gv2.Columns("Other Tax").IsVisible = True
            gv2.Columns("Other Tax").Width = 80
            gv2.Columns("Other Tax").HeaderText = "Other Tax"


            gv2.Columns("TPT Amt").IsVisible = False
            gv2.Columns("TPT Amt").Width = 80
            gv2.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv2.Columns("T.Rate Amt").IsVisible = False
            gv2.Columns("T.Rate Amt").Width = 80
            gv2.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv2.Columns("Total MRP").IsVisible = False
            gv2.Columns("Total MRP").Width = 80
            gv2.Columns("Total MRP").HeaderText = "Total MRP"

            gv2.Columns("T.Margin").IsVisible = False
            gv2.Columns("T.Margin").Width = 80
            gv2.Columns("T.Margin").HeaderText = "T.Margin"

            gv2.Columns("T.Price Amt").IsVisible = False
            gv2.Columns("T.Price Amt").Width = 80
            gv2.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv2.Columns("COMMAmt").IsVisible = False
            gv2.Columns("COMMAmt").Width = 80
            gv2.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv2.Columns("DISC").IsVisible = True
            gv2.Columns("DISC").Width = 80
            gv2.Columns("DISC").HeaderText = "Discount"

            gv2.Columns("Sale Account Amt").IsVisible = True
            gv2.Columns("Sale Account Amt").Width = 80
            gv2.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv2.Columns("Exciseamt").IsVisible = False
            gv2.Columns("Exciseamt").Width = 80
            gv2.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"

            gv2.Columns("Total_Cust_Discount").IsVisible = False
            gv2.Columns("Total_Cust_Discount").Width = 80
            gv2.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            gv2.Columns("MICode").IsVisible = True
            gv2.Columns("MICode").Width = 80
            gv2.Columns("MICode").HeaderText = "Main Item"


            gv2.Columns("MIUnit").IsVisible = True
            gv2.Columns("MIUnit").Width = 80
            gv2.Columns("MIUnit").HeaderText = "Main Unit"


            gv2.Columns("mainqty").IsVisible = True
            gv2.Columns("mainqty").Width = 80
            gv2.Columns("mainqty").HeaderText = "Main Qty"


            Try
                gv2.Columns("Category").IsVisible = True
                gv2.Columns("Category").Width = 220
                gv2.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        ElseIf frm.rdbDocSummary.IsChecked Then

            gv2.Columns("Type").IsVisible = True
            gv2.Columns("Type").Width = 100
            gv2.Columns("Type").HeaderText = "Type"

            gv2.Columns("Location_Code").IsVisible = True
            gv2.Columns("Location_Code").Width = 100
            gv2.Columns("Location_Code").HeaderText = "Location"

            gv2.Columns("Location_Desc").IsVisible = True
            gv2.Columns("Location_Desc").Width = 200
            gv2.Columns("Location_Desc").HeaderText = "Location Name"

            gv2.Columns("Route_No").IsVisible = True
            gv2.Columns("Route_No").Width = 100
            gv2.Columns("Route_No").HeaderText = "Route No"

            gv2.Columns("Route_Desc").IsVisible = True
            gv2.Columns("Route_Desc").Width = 200
            gv2.Columns("Route_Desc").HeaderText = "Route Desc"

            gv2.Columns("Transfer_Date").IsVisible = False
            gv2.Columns("Transfer_Date").Width = 100
            gv2.Columns("Transfer_Date").HeaderText = "Transfer Date"


            gv2.Columns("Transfer_No").IsVisible = False
            gv2.Columns("Transfer_No").Width = 100
            gv2.Columns("Transfer_No").HeaderText = "Transfer No"

            gv2.Columns("Adjustment_Amount").IsVisible = False
            gv2.Columns("Adjustment_Amount").Width = 100
            gv2.Columns("Adjustment_Amount").HeaderText = "Settlement Amount"

            gv2.Columns("Sale_Invoice_No").IsVisible = True
            gv2.Columns("Sale_Invoice_No").Width = 100
            gv2.Columns("Sale_Invoice_No").HeaderText = "Sale Invoice No"

            gv2.Columns("DocDate").IsVisible = True
            gv2.Columns("DocDate").Width = 100
            gv2.Columns("DocDate").HeaderText = "Document Date"

            gv2.Columns("DocAmt").IsVisible = True
            gv2.Columns("DocAmt").Width = 100
            gv2.Columns("DocAmt").HeaderText = "Document Amount"

            gv2.Columns("Customer_Code").IsVisible = False
            gv2.Columns("Customer_Code").Width = 100
            gv2.Columns("Customer_Code").HeaderText = "Customer Code"

            gv2.Columns("Customer_Name").IsVisible = True
            gv2.Columns("Customer_Name").Width = 100
            gv2.Columns("Customer_Name").HeaderText = ""


            gv2.Columns("Gross Qty").IsVisible = True
            gv2.Columns("Gross Qty").Width = 100
            gv2.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv2.Columns("Net Qty").IsVisible = True
            gv2.Columns("Net Qty").Width = 100
            gv2.Columns("Net Qty").HeaderText = "Net Qty"

            gv2.Columns("Qty Disc").IsVisible = True
            gv2.Columns("Qty Disc").Width = 100
            gv2.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv2.Columns("FOCAMt").IsVisible = True
            gv2.Columns("FOCAMt").Width = 100
            gv2.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv2.Columns("InvoiceAmt").IsVisible = False
            gv2.Columns("InvoiceAmt").Width = 100


            gv2.Columns("Total Basic Amt").IsVisible = True
            gv2.Columns("Total Basic Amt").Width = 100
            gv2.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv2.Columns("Excise Amt").IsVisible = True
            gv2.Columns("Excise Amt").Width = 100
            gv2.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv2.Columns("Cess Amt").IsVisible = True
            gv2.Columns("Cess Amt").Width = 100
            gv2.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv2.Columns("Hcess Amt").IsVisible = True
            gv2.Columns("Hcess Amt").Width = 100
            gv2.Columns("Hcess Amt").HeaderText = "Hcess Amt"


            gv2.Columns("CST").IsVisible = True
            gv2.Columns("CST").Width = 100
            gv2.Columns("CST").HeaderText = "CST"

            gv2.Columns("VAT").IsVisible = True
            gv2.Columns("VAT").Width = 100
            gv2.Columns("VAT").HeaderText = "VAT"

            gv2.Columns("Other Tax").IsVisible = True
            gv2.Columns("Other Tax").Width = 100
            gv2.Columns("Other Tax").HeaderText = "Other Tax"

            gv2.Columns("TPT Amt").IsVisible = False
            gv2.Columns("TPT Amt").Width = 100
            gv2.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv2.Columns("T.Rate Amt").IsVisible = False
            gv2.Columns("T.Rate Amt").Width = 100
            gv2.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv2.Columns("Total MRP").IsVisible = False
            gv2.Columns("Total MRP").Width = 100
            gv2.Columns("Total MRP").HeaderText = "Total MRP"

            gv2.Columns("T.Margin").IsVisible = False
            gv2.Columns("T.Margin").Width = 100
            gv2.Columns("T.Margin").HeaderText = "T.Margin"

            gv2.Columns("T.Price Amt").IsVisible = False
            gv2.Columns("T.Price Amt").Width = 80
            gv2.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv2.Columns("COMMAmt").IsVisible = False
            gv2.Columns("COMMAmt").Width = 100
            gv2.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv2.Columns("DISC").IsVisible = True
            gv2.Columns("DISC").Width = 100
            gv2.Columns("DISC").HeaderText = "DISC"

            gv2.Columns("Sale Account Amt").IsVisible = True
            gv2.Columns("Sale Account Amt").Width = 100
            gv2.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv2.Columns("Exciseamt").IsVisible = False
            gv2.Columns("Exciseamt").Width = 100
            gv2.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"


            gv2.Columns("Total_Cust_Discount").IsVisible = False
            gv2.Columns("Total_Cust_Discount").Width = 100
            gv2.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            Try
                gv2.Columns("Category").IsVisible = True
                gv2.Columns("Category").Width = 220
                gv2.Columns("Category").HeaderText = "Item Category"
            Catch ex As Exception
            End Try

        ElseIf frm.rdbItemSummary.IsChecked Then

            gv2.Columns("Item_Code").IsVisible = True
            gv2.Columns("Item_Code").Width = 80
            gv2.Columns("Item_Code").HeaderText = "Item Code"


            gv2.Columns("MRP").IsVisible = True
            gv2.Columns("MRP").Width = 70
            gv2.Columns("MRP").HeaderText = "MRP"

            gv2.Columns("MRPBottle").IsVisible = False
            gv2.Columns("MRPBottle").Width = 70
            gv2.Columns("MRPBottle").HeaderText = "MRPBottle"



            gv2.Columns("DocAmt").IsVisible = True
            gv2.Columns("DocAmt").Width = 100
            gv2.Columns("DocAmt").HeaderText = "Document Amount"


            gv2.Columns("Gross Qty").IsVisible = True
            gv2.Columns("Gross Qty").Width = 80
            gv2.Columns("Gross Qty").HeaderText = "Gross Qty"

            gv2.Columns("Net Qty").IsVisible = True
            gv2.Columns("Net Qty").Width = 80
            gv2.Columns("Net Qty").HeaderText = "Net Qty"

            gv2.Columns("Qty Disc").IsVisible = True
            gv2.Columns("Qty Disc").Width = 80
            gv2.Columns("Qty Disc").HeaderText = "Qty Disc"

            'If rdbInvoice.Checked = True Then
            gv2.Columns("FOCAMt").IsVisible = True
            gv2.Columns("FOCAMt").Width = 80
            gv2.Columns("FOCAMt").HeaderText = "Trade Discount Amt"

            gv2.Columns("InvoiceAmt").IsVisible = False
            gv2.Columns("InvoiceAmt").Width = 80


            gv2.Columns("Total Basic Amt").IsVisible = True
            gv2.Columns("Total Basic Amt").Width = 80
            gv2.Columns("Total Basic Amt").HeaderText = "Basic Amount"

            gv2.Columns("Excise Amt").IsVisible = True
            gv2.Columns("Excise Amt").Width = 80
            gv2.Columns("Excise Amt").HeaderText = "Excise Amt"

            gv2.Columns("Cess Amt").IsVisible = True
            gv2.Columns("Cess Amt").Width = 80
            gv2.Columns("Cess Amt").HeaderText = "Cess Amt"

            gv2.Columns("Hcess Amt").IsVisible = True
            gv2.Columns("Hcess Amt").Width = 80
            gv2.Columns("Hcess Amt").HeaderText = "Hcess Amt"


            gv2.Columns("CST").IsVisible = True
            gv2.Columns("CST").Width = 80
            gv2.Columns("CST").HeaderText = "CST"

            gv2.Columns("VAT").IsVisible = True
            gv2.Columns("VAT").Width = 80
            gv2.Columns("VAT").HeaderText = "VAT"

            gv2.Columns("Other Tax").IsVisible = True
            gv2.Columns("Other Tax").Width = 80
            gv2.Columns("Other Tax").HeaderText = "Other Tax"

            gv2.Columns("TPT Amt").IsVisible = False
            gv2.Columns("TPT Amt").Width = 80
            gv2.Columns("TPT Amt").HeaderText = "TPT Amt"

            gv2.Columns("T.Rate Amt").IsVisible = False
            gv2.Columns("T.Rate Amt").Width = 80
            gv2.Columns("T.Rate Amt").HeaderText = "T.Rate Amt"

            gv2.Columns("Total MRP").IsVisible = False
            gv2.Columns("Total MRP").Width = 80
            gv2.Columns("Total MRP").HeaderText = "Total MRP"

            gv2.Columns("T.Margin").IsVisible = False
            gv2.Columns("T.Margin").Width = 80
            gv2.Columns("T.Margin").HeaderText = "T.Margin"

            gv2.Columns("T.Price Amt").IsVisible = False
            gv2.Columns("T.Price Amt").Width = 80
            gv2.Columns("T.Price Amt").HeaderText = "T.Price Amt"

            gv2.Columns("COMMAmt").IsVisible = False
            gv2.Columns("COMMAmt").Width = 80
            gv2.Columns("COMMAmt").HeaderText = "COMMAmt"

            gv2.Columns("DISC").IsVisible = True
            gv2.Columns("DISC").Width = 80
            gv2.Columns("DISC").HeaderText = "Discount"

            gv2.Columns("Sale Account Amt").IsVisible = True
            gv2.Columns("Sale Account Amt").Width = 80
            gv2.Columns("Sale Account Amt").HeaderText = "Sale Account Amt"

            gv2.Columns("Total_Cust_Discount").IsVisible = False
            gv2.Columns("Total_Cust_Discount").Width = 80
            gv2.Columns("Total_Cust_Discount").HeaderText = "Customer Disc"

            gv2.Columns("Exciseamt").IsVisible = False
            gv2.Columns("Exciseamt").Width = 80
            gv2.Columns("Exciseamt").HeaderText = "Excise Recovered Amount"

            Try
                gv2.Columns("Category").IsVisible = True
                gv2.Columns("Category").Width = 220
                gv2.Columns("Category").HeaderText = "Item Category"
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
            gv2.Columns("SubledgerAmt").IsVisible = True
            gv2.Columns("SubledgerAmt").Width = 100
            gv2.Columns("SubledgerAmt").HeaderText = "Subledger Amt"

            Dim emptydr As New GridViewSummaryItem("SubledgerAmt", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(emptydr)


        End If

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv2.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv2.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv2.Columns.Count - 1 Step ii + 1
                        gv2.Columns(ii).IsVisible = False
                        gv2.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv2.LoadLayout(obj.GridLayout)
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




    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        AutoSaleAnalysisFromCmpMnth()

    End Sub

    Private Sub gv2_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellDoubleClick
        If frm.rdbDocSummary.IsChecked Then
            If gv2.Rows.Count > 0 Then
                Dim strTransType As String = clsCommon.myCstr(gv2.CurrentRow.Cells("Type").Value)
                Dim strDoc = gv2.CurrentRow.Cells("Sale_Invoice_No").Value
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
