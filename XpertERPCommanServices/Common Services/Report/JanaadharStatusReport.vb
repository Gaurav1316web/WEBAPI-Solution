Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class JanaadharStatusReport

    Dim frmP As New FarmerDetails()

    Sub FarmerDetail()
        Try
            Dim query As String = ""
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                Exit Sub
            End If

            Dim docNo As String = ""
            query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','RAJSAMAND','BNS','JMBILL','JPRTEST') "
            If chkRJSBNS.Checked Then
                query += "union all
            SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
            ORDER BY Location_Name"
            End If
            dt = clsDBFuncationality.GetDataTable(query)
            query = ""

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If

                    query += "  SELECT " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                    COUNT(DISTINCT X.MP_Code) AS Total_MP_Codes,
	                                count(ISNULL(mm.Jan_Aadhar_No_Verified,0)) as Jan_Aadhar_No_Verified, 
                                    count(ISNULL(mm.JA_aadhar,0)) as JA_aadhar,
                                    COALESCE(SUM(CASE WHEN ISNULL(mm.Jan_Aadhar_No_Verified, 0) = 0 THEN 1 ELSE 0 END),0) AS Jan_Aadhar_Unverified_Count,
                                    COALESCE(SUM(CASE WHEN ISNULL(mm.Jan_Aadhar_No_Verified, 0) = 1 THEN 1 ELSE 0 END),0) AS Jan_Aadhar_Verified_Count
                                FROM (SELECT MP_Code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL GROUP BY MP_Code ) X
                                LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER mm ON mm.MP_Code = X.MP_Code"
                Next
            End If

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt2 IsNot Nothing OrElse dt2.Rows.Count > 0 Then

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
                SetGridFormat1()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat1()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
            'gv1.Columns(ii).Width = 200
        Next

        gv1.Columns("SNo").Name = "SNo"
        gv1.Columns("SNo").IsVisible = True '

        gv1.Columns("Union Name").HeaderText = "Union Name"
        gv1.Columns("Union Name").Width = 200
        gv1.Columns("Union Name").IsVisible = True

        gv1.Columns("Total_MP_Codes").HeaderText = "Count Of Farmers"
        gv1.Columns("Total_MP_Codes").IsVisible = True

        gv1.Columns("Jan_Aadhar_No_Verified").HeaderText = "Jan_Aadhar_No_Verified"
        gv1.Columns("Jan_Aadhar_No_Verified").IsVisible = False

        gv1.Columns("JA_aadhar").HeaderText = "Count Of AADHAR"
        gv1.Columns("JA_aadhar").IsVisible = True

        gv1.Columns("Jan_Aadhar_Unverified_Count").HeaderText = "Count Of Unverified Farmer"
        gv1.Columns("Jan_Aadhar_Unverified_Count").IsVisible = True

        gv1.Columns("Jan_Aadhar_Verified_Count").HeaderText = "Count Of verified Farmer"
        gv1.Columns("Jan_Aadhar_Verified_Count").IsVisible = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Total_MP_Codes", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        Dim item2 As New GridViewSummaryItem("Jan_Aadhar_Unverified_Count", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)

        Dim item3 As New GridViewSummaryItem("Jan_Aadhar_Verified_Count", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        Dim item4 As New GridViewSummaryItem("JA_aadhar", "{0:f0}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        'gv1.ShowGroupPanel = True
        'gv1.MasterTemplate.AutoExpandGroups = True
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub
    Private Sub JanaadharStatusReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GETSERVERDATE()
        chkRJSBNS.Visible = False
        chkRJSBNS.Checked = True
    End Sub

    Private Sub BtnGo_Click_1(sender As Object, e As EventArgs) Handles BtnGo.Click
        FarmerDetail()
    End Sub

    Private Sub gv1_DoubleClick(sender As Object, e As EventArgs) Handles gv1.DoubleClick

        Dim Qry As String = " Select [TSPL_APP_LOCATION].DataBase_Name from [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] 
                              where [TSPL_APP_LOCATION].Location_Name=  '" + gv1.CurrentRow.Cells("Union Name").Value + "'  "

        Dim Union As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Qry))

        'Dim frm As New FrmFreeGrid
        frmP.Range = clsCommon.myCstr(Union)
        frmP.WindowState = FormWindowState.Maximized
        'frmP.Show()
        frmP.ShowDialog()

    End Sub

    Private Sub btnreset_Click_1(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Sub Reset()
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Union : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.JanaadharStatusReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))


                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
                'transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, False)
                clsCommon.MyExportToExcelGrid(Me.Text, gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.DashboardMilkProcurement & "'"))
                arrHeader.Add("Date : " & clsCommon.myCstr(txtFromDate.Text) + "  To " + clsCommon.myCstr(txtToDate.Text))

                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim query As String = ""
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Sub
        End If

        Dim docNo As String = ""
        query = " 
    SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHITTORGARH','RAJSAMAND','BANSWARA','JMBILL','JPRTEST') "
        'If chkRJSBNS.Checked Then
        '    query += "union all
        'SELECT 'Rajsamand' AS Location_Name,'RJS' AS DataBase_Name 
        'union all
        'SELECT 'Banswara' AS Location_Name,'BNS' AS DataBase_Name
        'ORDER BY Location_Name"
        'End If
        dt = clsDBFuncationality.GetDataTable(query)
        query = ""

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                If ii > 0 Then
                    query += " UNION ALL "
                End If

                query += "  SELECT " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],
                                    COUNT(DISTINCT X.MP_Code) AS Total_MP_Codes,
	                                count(ISNULL(mm.Jan_Aadhar_No_Verified,0)) as Jan_Aadhar_No_Verified, 
                                    COALESCE(SUM(CASE WHEN ISNULL(mm.Jan_Aadhar_No_Verified, 0) = 0 THEN 1 ELSE 0 END),0) AS Jan_Aadhar_Unverified_Count,
                                    COALESCE(SUM(CASE WHEN ISNULL(mm.Jan_Aadhar_No_Verified, 0) = 1 THEN 1 ELSE 0 END),0) AS Jan_Aadhar_Verified_Count
                                FROM (SELECT MP_Code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL GROUP BY MP_Code ) X
                                LEFT JOIN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER mm ON mm.MP_Code = X.MP_Code"
            Next
        End If

        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
        If dt2 IsNot Nothing And dt2.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptJanaadharStatusReport", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub
    'Inherits FrmMainTranScreen
End Class