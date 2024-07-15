Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions

Public Class rptD1D2Report
    Inherits FrmMainTranScreen

    Private Sub rptD1D2Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = New DateTime(txtFromDate.Value.Year, txtFromDate.Value.Month, DateTime.DaysInMonth(txtFromDate.Value.Year, txtFromDate.Value.Month))
        txtToDate.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        txtToDate.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try
            Dim whrcls As String = ""
            whrcls = " where 2 = 2  and Is_FreshItem = 1 and TSPL_BOOKING_MATSER.Posted = 1 and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)  and convert(date,TSPL_BOOKING_MATSER.Document_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  "
            Dim dtDate As New DataTable()
            dtDate = clsDBFuncationality.GetDataTable("select Date from ( SELECT convert(varchar,TSPL_BOOKING_MATSER.Document_Date,103)as Date   FROM TSPL_BOOKING_DETAIL left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code " & whrcls & " ) xx  group by date order by date")

            Dim DateName As String = Nothing
            Dim DatesName As String = Nothing
            If dtDate.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If
            If dtDate.Rows.Count > 0 Then
                For i As Integer = txtFromDate.Value.Day To txtToDate.Value.Day
                    DateName += " Sum(IsNull([" + clsCommon.myCstr(txtFromDate.Value.AddDays(i - 1).ToString("dd/MM/yyyy")) + "],0)) As [" + clsCommon.myCstr(txtFromDate.Value.AddDays(i - 1).ToString("dd/MM/yyyy")) + "]" + ","
                    If i = txtFromDate.Value.Day Then
                        DatesName += "[" + clsCommon.myCstr(txtFromDate.Value.AddDays(i - 1).ToString("dd/MM/yyyy")) + "] "
                    Else
                        DatesName += ", [" + clsCommon.myCstr(txtFromDate.Value.AddDays(i - 1).ToString("dd/MM/yyyy")) + "] "
                    End If
                Next
            End If

            Dim qry As String = ""
            qry = "select Cust_Code,Customer_Name, " & DateName & " 0 as Total from ( SELECT TSPL_BOOKING_DETAIL.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name, convert(varchar,TSPL_BOOKING_MATSER.Document_Date,103)as Date,TSPL_BOOKING_MATSER.Document_Date, isnull(TSPL_BOOKING_DETAIL.Booking_Qty,0) AS Qty
            ,cast(( isnull ( TSPL_BOOKING_DETAIL.Booking_Qty,0) *isnull(TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)/I.[LTR]) as int) as LTR_QTY  FROM TSPL_BOOKING_DETAIL left outer join TSPL_BOOKING_MATSER on TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No 
            left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_BOOKING_DETAIL.Item_Code left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_BOOKING_DETAIL.Item_Code   and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_BOOKING_DETAIL.Unit_Code 
            left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_BOOKING_DETAIL.Cust_Code left join (  SELECT * FROM ( select item_code,uom_code,conversion_factor from TSPL_ITEM_UOM_DETAIL) I  PIVOT (Max(conversion_factor) FOR uom_code IN ( [KG],[LTR] )) P ) I ON TSPL_BOOKING_DETAIL.Item_Code = I.item_code
             " & whrcls & " ) xx PIVOT (SUM(LTR_QTY)  FOR Date IN (" & DatesName & " ) )as pivot_date group by Cust_Code,Customer_Name order by Cust_Code"

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
        gv1.ShowGroupPanel = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = ""
        Next
        gv1.Columns("Total").IsVisible = False
        gv1.Columns("Cust_Code").HeaderText = "Customer"
        gv1.Columns("Customer_Name").HeaderText = "Customer Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 2 To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "", GridAggregateFunction.Sum))
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptD1D2Report & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export.", Me.Text)

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try

            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found To export", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        txtToDate.Value = New DateTime(txtFromDate.Value.Year, txtFromDate.Value.Month, DateTime.DaysInMonth(txtFromDate.Value.Year, txtFromDate.Value.Month))
    End Sub
End Class