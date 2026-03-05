
Imports System.IO
Imports common
Imports System.Text
Imports common.UserControls
Public Class rptDbtStatusRCDF
    Inherits FrmMainTranScreen
    Const ReportID As String = "DBTNEFTPaymentDetailReport"
    Dim Slot1 As String = ""
    Dim Slot2 As String = ""
    Dim MonthNo As String = Nothing
    Private Sub rptDbtStatusRCDF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        Try
            Dim SM As Integer = txtFromDate.Value.Month
            Dim SY As Integer = txtFromDate.Value.Year
            Dim CD As New DateTime(SY, SM, 1)
            Slot1 = clsCommon.GetPrintDate(CD, "dd/MMM/yyyy")
            MonthNo = clsCommon.GetPrintDate(txtFromDate.Value, "MM-yyyy")
            Month()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Month()
        If clsCommon.myLen(txtFromDate.Value) > 0 Then
            Dim SM As Integer = txtFromDate.Value.Month
            Dim SY As Integer = txtFromDate.Value.Year

            Dim CD As New DateTime(SY, SM, 1)
            Slot2 = clsCommon.GetPrintDate(CD.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")
            MonthNo = clsCommon.GetPrintDate(txtFromDate.Value, "MM-yyyy")

        End If
    End Sub

    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If
            Dim qry As String = ""
            If objCommonVar.RCDFCFP Then
                qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            Else
                qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrDatabase & "' ORDER BY [TSPL_APP_LOCATION].Location_Name"

            End If
            'qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            txtFromDate.Enabled = False
            txtToDate.Enabled = False

            txtUnion.Enabled = False
            Dim inputFromDate As DateTime = txtFromDate.Value
            Dim fromDate As DateTime = New DateTime(inputFromDate.Year, inputFromDate.Month, 1)
            Dim inputToDate As DateTime = txtToDate.Value
            Dim toDate As DateTime = New DateTime(inputToDate.Year, inputToDate.Month, 1).AddMonths(1).AddDays(-1)
            Dim baseqry As String = Nothing
            Dim qry1 As String = Nothing
            Dim dtunion As New DataTable
            Dim qry As String = ""
            Dim uQry As String = ""
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)

            If txtUnion.arrValueMember Is Nothing Then
                If objCommonVar.RCDFCFP Then
                    uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2  and  Union_Report=1  order by [TSPL_APP_LOCATION].Location_Name "
                Else
                    uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrDatabase & "' order by [TSPL_APP_LOCATION].Location_Name "
                End If

            Else
                uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
            End If
            dtunion = clsDBFuncationality.GetDataTable(uQry)

            For ii As Integer = 0 To dtunion.Rows.Count - 1
                If ii > 0 Then
                    baseqry += "union all"
                End If
                baseqry += "  select  '" + clsCommon.myCstr(dtunion.Rows(ii).Item("Location_Name")) + "' AS [UnionName],
FORMAT([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date, 'MMM-yyyy') AS [Month],  
FORMAT([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,'MM') AS [Month1], 
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Amount,
  CASE WHEN [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Status = 1 THEN 'Approved'ELSE 'Pending'END AS StatusUnion,
	case when [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.RCDF_Status=1 then 'Approved' else 'Pending' END AS RCDFStatus
    from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT 
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code
    where
       Convert(Date,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,103)>=Convert(Date,'" & fromDate & "',103) And 
    Convert(Date,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date,103)<=Convert(Date,'" & toDate & "',103)"

                qry = " select [UnionName],  FORMAT(SUM(xxx.Amount), '#,##,##0', 'en-IN') AS Amount,Month,MAX(xxx.StatusUnion)StatusUnion,MAX(xxx.RCDFStatus)RCDFStatus from (" & baseqry & " )xxx
group by [UnionName],[Month1],Month ORDER BY [UnionName],[Month1] "
            Next


            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                gv1.AllowAddNewRow = False
                gv1.ShowGroupPanel = False
                SetGridFormat()


                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)

        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtFromDate.Enabled = True
        txtUnion.Enabled = True
        txtToDate.Enabled = True

    End Sub
    Sub SetGridFormat()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True

            gv1.Columns("UnionName").IsVisible = True
            gv1.Columns("UnionName").HeaderText = "Union"
            gv1.Columns("Amount").IsVisible = True
            gv1.Columns("Amount").HeaderText = "Amount"
            gv1.Columns("StatusUnion").IsVisible = True
            gv1.Columns("StatusUnion").HeaderText = "UnionStatus"
            gv1.Columns("RCDFStatus").IsVisible = True
            gv1.Columns("RCDFStatus").HeaderText = "RCDFStatus"


        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim Amount As New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Amount)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDbtStatusRCDF & "'"))
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                '  arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_code='" & clsUserMgtCode.FrmRptCustomerTransList & "'"))


                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class