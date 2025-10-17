
Imports common
Imports System.Text
Imports common.UserControls
Public Class rptDBTMonthWiseFarmerDetail
    Inherits FrmMainTranScreen
    Private Sub rptDBTMonthWiseFarmerDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtFinYr__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinYr._MYValidating
        Dim qry As String = "select Fiscal_Code as Code,Fiscal_Name as Name from TSPL_Fiscal_Year_Master"
        txtFinYr.Value = clsCommon.ShowSelectForm("fndFinancialYearMaster", qry, "Code", "", txtFinYr.Value, "Code", isButtonClicked)
        If Not String.IsNullOrEmpty(txtFinYr.Value) AndAlso txtFinYr.Value.Contains("-") Then
            Dim years() As String = txtFinYr.Value.Split("-"c)
            If years.Length = 2 Then
                Dim startYear As Integer
                If Integer.TryParse(years(0), startYear) Then
                    ' Convert "24" to 2024 (assume anything below 50 is 2000s, above is 1900s or 20xx depending on context)
                    If startYear < 100 Then
                        startYear = If(startYear < 50, 2000 + startYear, 1900 + startYear)
                    End If

                    Dim fromDate As Date = New Date(startYear, 4, 1)       ' 1st April of start year
                    Dim toDate As Date = New Date(startYear + 1, 3, 31)   ' 31st March of next year

                    txtFromDate.Value = fromDate
                    txtToDate.Value = toDate
                End If
            End If
        End If
        txtFromDate.Enabled = False
        txtToDate.Enabled = False

    End Sub

    Private Sub txtMultUnion__My_Click(sender As Object, e As EventArgs) Handles txtMultUnion._My_Click
        Try
            If chkDataBase() Then
                Dim Qry As String = "SELECT [TSPL_APP_LOCATION].DataBase_Name As [Code], [TSPL_APP_LOCATION].Location_Name As [Union Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"
                txtMultUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", Qry, "Code", "Union Name", txtMultUnion.arrValueMember, txtMultUnion.arrDispalyMember)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Function chkDataBase() As Boolean
        Try
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                clsCommon.MyMessageBoxShow(Me, "Database [TSPL_MASTER] not found !", Me.Text)
                Reset()
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim baseqry As String = Nothing
            Dim qry1 As String = Nothing
            Dim dtunion As New DataTable
            Dim uQry As String = ""
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found", Me.Text)
                Gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim ss As String = clsCommon.GetMulcallString(txtMultUnion.arrValueMember)

            If txtMultUnion.arrValueMember Is Nothing Then
                uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 order by [TSPL_APP_LOCATION].Location_Name "
            Else
                uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
            End If
            dtunion = clsDBFuncationality.GetDataTable(uQry)

            For ii As Integer = 0 To dtunion.Rows.Count - 1
                If ii > 0 Then
                    baseqry += "union all"
                    'Else
                    '    qry1 += "WITH CTE AS("
                End If
                ' qry1 += ";WITH CTE AS ("
                baseqry += "  Select '" + clsCommon.myCstr(dtunion.Rows(ii).Item("Location_Name")) + "' AS [UnionName], MP_Code as ID, FORMAT([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date, 'MMM/yyyy') AS MonthYear,DATENAME(MONTH, [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date) AS MonthName,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date as IDate,ROW_NUMBER() OVER (PARTITION BY MP_Code ORDER BY From_Date) AS RN
 from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
where [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"

            Next

            qry1 += "WITH CTE AS(" + baseqry + " )SELECT (UnionName)UnionName,  MonthYear,MonthName, COUNT(DISTINCT ID) AS TotalID, COUNT(CASE WHEN RN = 1 THEN 1 END) AS NewID FROM CTE
                          GROUP BY MonthYear,MonthName,UnionName
               ORDER BY UnionName,MIN(IDate)"
            baseqry = "" + qry1 + ""

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(baseqry)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt2
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                Gv1.AllowAddNewRow = False
                Gv1.ShowGroupPanel = False
                SetGridFormat()

                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
            ' End If
        Catch ex As Exception
        End Try
    End Sub
    Sub SetGridFormat()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Gv1.ShowGroupPanel = False
        Gv1.Columns("UnionName").HeaderText = "Union Name"
        Gv1.Columns("MonthYear").HeaderText = "Month"
        Gv1.Columns("MonthName").IsVisible = False
        Gv1.Columns("TotalID").HeaderText = "No. of Beneficiary in CMDUSY Scheme"
        Gv1.Columns("NewID").HeaderText = "No. of New Beneficiary in CMDUSY Scheme"

        Dim summaryRowItemB As New GridViewSummaryRowItem()
        summaryRowItemB.Add(New GridViewSummaryItem("MonthYear", "Total", GridAggregateFunction.Sum))

        Dim TotalID As New GridViewSummaryItem("TotalID", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(TotalID)
        Dim NewID As New GridViewSummaryItem("NewID", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(NewID)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'Dim NewID As New GridViewSummaryItem("NewID", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(NewID)
        'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        'Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'Gv1.AutoSizeRows = True
        'Gv1.BestFitColumns()
        ' Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Function ReportQry() As String
        Try
            Dim qry1 As String = Nothing
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Gv1.DataSource = Nothing
            End If

            Dim frmDate As String = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,Start_Date, 103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYr.Value + "'"))
            Dim toDate As String = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,End_Date, 103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYr.Value + "'"))

            Dim dtr As DataTable = clsMilkUnion.UnionDBName()
            For ii As Integer = 0 To dtr.Rows.Count - 1
                If ii > 0 Then
                    qry1 += "union all"
                Else
                    qry1 += "("
                End If
                qry1 += ";WITH CTE AS (
	select '" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [UnionName], MP_Code as ID, FORMAT(TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date, 'MMM/yyyy') AS MonthYear,DATENAME(MONTH, TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date) AS MonthName,TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date as IDate,ROW_NUMBER() OVER (PARTITION BY MP_Code ORDER BY From_Date) AS RN
 from TSPL_MP_INCENTIVE_ENTRY_DETAIL
left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
where TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
and TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
)
SELECT max(UnionName)UnionName,  MonthYear,MonthName, COUNT(DISTINCT ID) AS TotalID, COUNT(CASE WHEN RN = 1 THEN 1 END) AS NewID
FROM CTE
GROUP BY MonthYear,MonthName
ORDER BY MIN(IDate);"
            Next

            Return qry1
            'Return qry
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            RadPageView1.SelectedPage = RadPageViewPage1
            txtFinYr.Value = ""

            txtMultUnion.arrValueMember = Nothing
            txtFromDate.Enabled = True
            txtToDate.Enabled = True
            txtFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
            txtToDate.Value = clsCommon.GETSERVERDATE()
            'EnableDisableFields(True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDBTMonthWiseFarmerDetail & "'"))
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Month :" & MonthNo)
                clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class