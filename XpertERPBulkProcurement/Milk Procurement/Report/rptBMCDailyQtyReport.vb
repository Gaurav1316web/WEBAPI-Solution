Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions

Public Class rptBMCDailyQtyReport
    Inherits FrmMainTranScreen

    Private Sub rptBMCDailyQtyReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = "01/" & DatePart(DateInterval.Month, clsCommon.GETSERVERDATE()) & "/" & DatePart(DateInterval.Year, clsCommon.GETSERVERDATE())
        txtToDate.Value = New DateTime(txtFromDate.Value.Year, txtFromDate.Value.Month, DateTime.DaysInMonth(txtFromDate.Value.Year, txtFromDate.Value.Month))
        RadPageView1.SelectedPage = RadPageViewPage1
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub

    Private Sub LoadData()
        Try
            Dim whrcls As String = ""
            txtFromDate.Value = "01/" & DatePart(DateInterval.Month, txtFromDate.Value) & "/" & DatePart(DateInterval.Year, txtFromDate.Value)
            Dim dtDate As New DataTable()
            whrcls = "  and TSPL_MILK_COLLECTION_MCC.Status=1  and convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) >= Convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103)  and convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  "
            If txtRoute.arrValueMember IsNot Nothing Then
                whrcls += " And TSPL_MILK_COLLECTION_MCC.Route_Code in (" & clsCommon.GetMulcallString(txtRoute.arrValueMember) & ")"
            End If
            dtDate = clsDBFuncationality.GetDataTable(" select day(Document_Date)Date from ( SELECT TSPL_MILK_COLLECTION_MCC.Route_Code,TSPL_MILK_COLLECTION_MCC.Document_Date, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MCC_MASTER.MCC_NAME FROM TSPL_MILK_COLLECTION_MCC_DETAIL left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
            left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code where  2=2  " & whrcls & " )xx group by Document_Date order by Date ")

            Dim DateName As String = Nothing
            Dim DatesName As String = Nothing
            If dtDate.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No data found to display", Me.Text)
                Exit Sub
            End If
            If dtDate.Rows.Count > 0 Then
                For i As Integer = 0 To txtToDate.Value.Day - 1
                    If i = 0 Then
                        DatesName += "[" + clsCommon.myCstr(txtFromDate.Value.Day + i) + "] "
                        DateName += " Sum(IsNull([" + clsCommon.myCstr(txtFromDate.Value.Day + i) + "],0)) As [" + clsCommon.myCstr(txtFromDate.Value.Day + i) + "] "
                    Else
                        DateName += ", Sum(IsNull([" + clsCommon.myCstr(txtFromDate.Value.Day + i) + "],0)) As [" + clsCommon.myCstr(txtFromDate.Value.Day + i) + "] "
                        DatesName += ", [" + clsCommon.myCstr(txtFromDate.Value.Day + i) + "] "
                    End If
                Next
            End If

            Dim qry As String = ""
            qry = " select max(Route_Name)Route_Name,MCC_NAME, " & DateName & ",sum(Total) Total from ( select Route_Code, max(ROUTE_NAME)ROUTE_NAME,MCC_Code,max(MCC_NAME)MCC_NAME,Document_Date,sum(Qty)Qty,sum(Total) Total from ( SELECT TSPL_BULK_ROUTE_MASTER.ROUTE_NAME, TSPL_MILK_COLLECTION_MCC.Route_Code,day(TSPL_MILK_COLLECTION_MCC.Document_Date)Document_Date, TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code,TSPL_MILK_COLLECTION_MCC_DETAIL.Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.qty as Total,TSPL_MCC_MASTER.MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader  FROM TSPL_MILK_COLLECTION_MCC_DETAIL 
            left outer join TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.route_no = TSPL_MILK_COLLECTION_MCC.route_code  left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code  where  2=2 " & whrcls & " ) xx group by Route_Code,MCC_Code,Document_Date )xxx  PIVOT (SUM(qty)  FOR Document_Date IN (" & DatesName & " ) )as pivot_date group by Route_Code,MCC_Code,MCC_NAME order by Route_Code,MCC_Code "

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
        gv1.ShowGroupPanel = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns(ii).FormatString = "{0:n2}"
        Next
        gv1.Columns("Route_Name").HeaderText = "Route Name"
        gv1.Columns("MCC_NAME").HeaderText = "MCC Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 2 To gv1.Columns.Count - 1
            summaryRowItem.Add(New GridViewSummaryItem(gv1.Columns(ii).Name, "", GridAggregateFunction.Sum))
        Next
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

        Dim Itemdescriptor As New GroupDescriptor()
        Itemdescriptor.GroupNames.Add("Route_Name", System.ComponentModel.ListSortDirection.Ascending)
        gv1.GroupDescriptors.Add(Itemdescriptor)
        gv1.MasterTemplate.AutoExpandGroups = True
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
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBMCDailyQtyReport & "'"))
                arrHeader.Add("Month: " & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy"))
                If txtRoute.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Route : " & clsCommon.GetMulcallString(txtRoute.arrValueMember) & "  Route Name :" & clsCommon.GetMulcallString(txtRoute.arrDispalyMember) & "")
                End If
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text)
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
                arrHeader.Add("Month: " & clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy"))
                If txtRoute.arrValueMember IsNot Nothing Then
                    arrHeader.Add("Route : " & clsCommon.GetMulcallString(txtRoute.arrValueMember) & "  Route Name :" & clsCommon.GetMulcallString(txtRoute.arrDispalyMember) & "")
                End If
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
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "Select Route_No as Code , Route_Name as Name from TSPL_BULK_ROUTE_MASTER"
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMUL", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class