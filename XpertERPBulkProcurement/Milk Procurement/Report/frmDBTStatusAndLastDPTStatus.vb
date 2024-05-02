Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions
Public Class frmDBTStatusAndLastDPTStatus
    Inherits FrmMainTranScreen
    Private Label1 As New Label

    'Private WithEvents Label1 As Label

    Private Sub rdbLastDBTStatus_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim query = ReportQry()
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                gvData.DataSource = Nothing
                gvData.Rows.Clear()
                gvData.Columns.Clear()
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.MasterView.Refresh()
                gvData.DataSource = dt2
                For ii As Integer = 0 To gvData.Columns.Count - 1
                    gvData.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.EnableFiltering = True
                gvData.AllowAddNewRow = False
                gvData.ShowGroupPanel = False
                'SetGridFormat()
                gvData.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
        Catch ex As Exception

        End Try


        'Try

        '    fillGridReport(False)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(me,ex.Message,me.text)
        'End Try
    End Sub
    Private Function ReportQry() As String
        Try
            If objCommonVar.RCDFCFP = True Then
                PageSetupReport_ID = Me.Form_ID + "CFP"
            Else
                PageSetupReport_ID = Me.Form_ID + "D"
            End If
            TemplateGridview = gvData

            Dim query As String
            gvData.DataSource = Nothing

            If rdbDBTStatus.Checked Then
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    gvData.DataSource = Nothing
                End If

                Dim frmDate As String = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,Start_Date, 103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYr.Value + "'"))
                Dim toDate As String = clsCommon.myCDate(clsDBFuncationality.getSingleValue("select convert(date,End_Date, 103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinYr.Value + "'"))

                Dim dtr As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
                query = ""
                Dim status As String = ""
                If rbtnTransactionPosted.Checked Then

                    status = "AND Status = 1"
                Else
                    status = ""
                End If
                For ii As Integer = 0 To dtr.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dtr.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 4 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As April"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 5 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As May"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 6 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As June"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 7 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As July"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 8 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As August"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 9 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As September"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 10 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As October"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 11 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As November"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 12 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As December"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 1 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As January"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 2 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As February"
                    query += ",(SELECT STRING_AGG(CASE WHEN Status = 1 THEN 'Y' ELSE 'N' END, '+') FROM ( SELECT *, ROW_NUMBER() OVER (PARTITION BY From_Date ORDER BY From_Date) AS RowNum FROM [" + clsCommon.myCstr(dtr.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT WHERE MONTH(From_Date) = 3 " & status & "  AND From_Date >= convert(date,'" + frmDate + "',103) AND From_Date < convert(date,'" + toDate + "',103) ) AS Subquery WHERE RowNum = 1 ) As March"
                Next
            ElseIf rdbLastDBTStatus.Checked Then
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    gvData.DataSource = Nothing
                End If

                Dim docNo As String = ""
                query = ""
                dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        If ii > 0 Then
                            query += " UNION ALL "
                        End If

                        Dim qry As String = "SELECT Top 1 Document_Code FROM [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT where 2=2 "
                        If rbtnTransactionPosted.Checked Then
                            qry += " AND Status = 1"
                        End If
                        qry += " ORDER BY Document_date DESC"
                        docNo = clsDBFuncationality.getSingleValue(qry)

                        query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],COUNT([" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code) AS [No Of Farmer],
    SUM(CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.Jan_Aadhar_No_Verified = 1 THEN 1 ELSE 0 END) AS [Jan Aadhar Verified No],
    SUM(CASE WHEN [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.Aadhar_No_Verified = 1 THEN 1 ELSE 0 END) AS [Addhar Verified],
    CONVERT(varchar, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date, 103) + ' - ' + CONVERT(varchar, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date, 103) AS [Last DBT Cycle]
    from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code = [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.MP_Code
    left join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT ON [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code= [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code where [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code= '" & docNo & "' group by [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date, [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date"

                    Next
                End If
            Else
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
                If (dt1 Is Nothing OrElse dt1.Rows.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                    gvData.DataSource = Nothing

                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT [TSPL_ERP_STATUS].Location_Name,[TSPL_ERP_STATUS].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_ERP_STATUS] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','RAJSAMAND','BANSWARA') ORDER BY [TSPL_ERP_STATUS].Location_Name")
                query = ""

                Dim Status1 As String = "  "
                Dim PostedY As String = "   "
                Dim POSTED1 As String = "  "
                If rbtnTransactionPosted.Checked Then
                    Status1 = " and Status = 1 "
                    PostedY = " and Posted ='Y' "
                    POSTED1 = " and POSTED = 1 "
                End If
                For ii As Integer = 0 To dt.Rows.Count - 1
                    If ii > 0 Then
                        query += " UNION ALL "
                    End If
                    query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name]"
                    query += ",(select FORMAT(max(PurchaseOrder_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PURCHASE_ORDER_HEAD where Status=1) as [Last Purchase order Date]"
                    query += ",(select FORMAT(max(SRN_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SRN_HEAD where 2=2  " + Status1 + " ) as [Last Stock Received (SRN) Date]"
                    query += ",(select FORMAT(max(Doc_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_IssueReturn_HEAD where Doc_Type='Issue' " + Status1 + " ) as [Last Stock Issue Date]"
                    query += ",(select FORMAT(max(Adjustment_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_ADJUSTMENT_HEADER where 2=2 " + PostedY + " ) as [Last Stock Adjustment Date]"
                    query += ",(select FORMAT(max(PROD_DATE),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PP_PRODUCTION_ENTRY where 2=2 " + POSTED1 + ") as [Last Production Entry Date]"
                    query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_BOOKING_MATSER where 2=2 " + POSTED1 + "  ) as [Last Demand Date]"
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "RJS") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BNS") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "BRN") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")), "JHL") = CompairStringResult.Equal Then
                        query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_Dispatch_BulkSale where 2=2 " + POSTED1 + "  ) as [Last Dispatch Date]"
                    Else
                        query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SHIPMENT_HEAD where 2=2 " + Status1 + "  ) as [Last Dispatch Date]"
                    End If
                    query += ",(select FORMAT(max(PI_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_PI_head where 2=2 " + Status1 + ") as [Last Stock Voucher Date]"
                    query += ",(select FORMAT(max(Document_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_SD_SALE_INVOICE_HEAD where 2=2 " + Status1 + " ) as [Last Sales Voucher Date]"
                    query += ",(select FORMAT(max(Receipt_Date),'dd/MMM/yyyy') from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_RECEIPT_HEADER where 2=2 " + PostedY + " ) as [Last Receipt Date]"

                Next
            End If
            Return query
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvData.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvData.Columns.Count - 1 Step ii + 1
                        gvData.Columns(ii).IsVisible = False
                        gvData.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvData.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub SetGridFormat(ByRef MyRadGridView2 As RadGridView)
        MyRadGridView2.ShowGroupPanel = True
        MyRadGridView2.ShowRowHeaderColumn = False
        MyRadGridView2.AllowAddNewRow = False
        MyRadGridView2.AllowDeleteRow = False
        MyRadGridView2.EnableFiltering = True
        MyRadGridView2.ShowFilteringRow = True
        MyRadGridView2.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Milk Collection in Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Demand in Ltr", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Purchase Order", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)

        For ii As Integer = 0 To MyRadGridView2.Columns.Count - 1
            MyRadGridView2.Columns(ii).ReadOnly = True
            MyRadGridView2.Columns(ii).BestFit()
        Next
        MyRadGridView2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        MyRadGridView2.AutoSizeRows = False
        MyRadGridView2.BestFitColumns()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmERPStatusTrackingReport & "'"))
            arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
            arrHeader.Add("User : " + objCommonVar.CurrentUser)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
                'transportSql.QuickExportToExcel(MyRadGridView2, "", Label2.Text, , arrHeader)
            Else
                'transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                'clsCommon.MyExportToPDF(Label1.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                Dim doc As New RadPrintDocument()
                doc.Margins.Top = 50
                doc.Margins.Bottom = 50
                doc.Margins.Left = 50
                doc.Margins.Right = 50
                doc.HeaderHeight = 100
                doc.Landscape = True
                doc.LeftFooter = "Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")
                doc.RightFooter = "User : " + objCommonVar.CurrentUser
                doc.AssociatedObject = gvData
                'Dim strHeader As String = Label2.Text 'Me.Text.Replace("/", "")
                'doc.MiddleHeader = strHeader
                doc.HeaderFont = New Font("Verdana", 12, FontStyle.Bold)
                'doc.Print()
                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmDBTStatusAndLastDPTStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "ERP Status At Milk Unions"
        If objCommonVar.RCDFCFP = True Then
            'Label2.Text = "ERP Status At Cattle Feed Plants"
            'SplitContainer3.Panel1Collapsed = True
        Else
            'Label2.Text = "ERP Status At Milk Unions"

        End If
        RadPageView1.Visible = True
        'rdbDBTStatus.Location = New System.Drawing.Point(292, 4)
        'rdbLastDBTStatus.Location = New System.Drawing.Point(414, 5)
        'MyLabel1.Location = New System.Drawing.Point(373, 5)
        'txtFinYr.Location = New System.Drawing.Point(489, 5)
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub


    Private Sub txtFinYr__MYValidating_2(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Dim qry As String = "select Fiscal_Code as Code,Fiscal_Name as Name from TSPL_Fiscal_Year_Master"
        txtFinYr.Value = clsCommon.ShowSelectForm("fndFinancialYearMaster", qry, "Code", "", txtFinYr.Value, "Code", isButtonClicked)
    End Sub


    Private Sub rdbDBTStatus_CheckedChanged_1(sender As Object, e As EventArgs)
        If rdbDBTStatus.Checked Then
            txtFinYr.Visible = True
            MyLabel1.Visible = True
            'Label2.Text = "DBT Status Report At Milk Unions"
            rdbLastDBTStatus.Location = New Point(340, 26)
            txtFinYr.Value = clsDBFuncationality.getSingleValue("select Fiscal_Code as Code from TSPL_Fiscal_Year_Master WHERE Is_Current_Year = 1")
        Else
            txtFinYr.Visible = False
            MyLabel1.Visible = False
            'Label2.Text = "ERP Status At Milk Unions"
            rdbLastDBTStatus.Location = New Point(700, 3)
        End If
    End Sub

    Sub reset()
        gvData.Enabled = ""
        'txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")

    End Sub

    Private Sub rdbDBTStatus_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDBTStatus.CheckedChanged
        If rdbDBTStatus.Checked Then
            txtFinYr.Visible = True
            MyLabel1.Visible = True
            'Label2.Text = "DBT Status Report At Milk Unions"
            rdbLastDBTStatus.Location = New Point(300, 6)
            txtFinYr.Value = clsDBFuncationality.getSingleValue("select Fiscal_Code as Code from TSPL_Fiscal_Year_Master WHERE Is_Current_Year = 1")
        Else
            txtFinYr.Visible = False
            MyLabel1.Visible = False
            'Label2.Text = "ERP Status At Milk Unions"
            rdbLastDBTStatus.Location = New Point(100, 6)
        End If
    End Sub

    Private Sub rdbLastDBTStatus_CheckedChanged_2(sender As Object, e As EventArgs) Handles rdbLastDBTStatus.CheckedChanged
        If rdbLastDBTStatus.Checked Then
            'Label2.Text = "Last DBT Status Report"
            'rdbLastDBTStatus.Location = New Point(100, 3)
        End If
    End Sub

    Private Sub txtFinYr__MYValidating_1(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinYr._MYValidating
        Dim qry As String = "select Fiscal_Code as Code,Fiscal_Name as Name from TSPL_Fiscal_Year_Master"
        txtFinYr.Value = clsCommon.ShowSelectForm("fndFinancialYearMaster", qry, "Code", "", txtFinYr.Value, "Code", isButtonClicked)
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        gvData.Columns.Clear()
        gvData.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1

        btnGo.Enabled = True
    End Sub
End Class