Imports common
Public Class MostUserScreen
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Gv1.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableDisable(True)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub


    Public Sub LoadData()
        Try
            Dim dt As New DataTable
            If txtTopCount.Value = 0 Then
                Throw New Exception("Top count can't be zero !")
            End If

            Dim strQry As String = ""
            If chkDateRange.Checked Then
                strQry = " SELECT TOP " & clsCommon.myCstr(txtTopCount.Value) & "  'DBLocation' AS [Union],Max(TSPL_PROGRAM_COUNTER.Created_Date) As [Created Date],Max(TSPL_PROGRAM_COUNTER.Created_By) as [Created By],TSPL_PROGRAM_COUNTER.Program_Code AS [Screen Code],Max(TSPL_PROGRAM_MASTER.Program_Name) AS [Screen Name],Max(TabM.Program_Code) AS [Module Code],Max(TabM.Program_Name) AS [Module],COUNT(TSPL_PROGRAM_COUNTER.PK_Id) As [Count]
FROM DBNamePrefixTSPL_PROGRAM_COUNTER 
left outer join DBNamePrefixTSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_PROGRAM_COUNTER.program_code  
inner join DBNamePrefixTSPL_PROGRAM_MASTER as TabSM on TabSM.Program_Code=TSPL_PROGRAM_MASTER.Parent_Code
inner join DBNamePrefixTSPL_PROGRAM_MASTER as TabM on TabM.Program_Code=TabSM.Parent_Code
WHERE  Convert(date,TSPL_PROGRAM_COUNTER.created_date,103)>= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm tt") + "'  and Convert(date,TSPL_PROGRAM_COUNTER.created_date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"
                If clsCommon.myLen(cboScreenType.SelectedValue) > 0 Then
                    strQry &= " and TabSM.Program_Name like '%" & clsCommon.myCstr(cboScreenType.SelectedValue) & "%'"
                End If
                strQry &= " Group By TSPL_PROGRAM_COUNTER.Program_Code,TSPL_PROGRAM_MASTER.Form_Open_Counter "
            Else
                strQry = " SELECT TOP " & clsCommon.myCstr(txtTopCount.Value) & " 'DBLocation' AS [Union],TSPL_PROGRAM_MASTER.Program_Code AS [Screen Code], TSPL_PROGRAM_MASTER.Program_Name AS [Screen Name],(TabM.Program_Code) AS [Module Code],(TabM.Program_Name) AS [Module], TSPL_PROGRAM_MASTER.Form_Open_Counter AS [Count]
FROM DBNamePrefixTSPL_PROGRAM_MASTER 
inner join DBNamePrefixTSPL_PROGRAM_MASTER as TabSM on TabSM.Program_Code=TSPL_PROGRAM_MASTER.Parent_Code
inner join DBNamePrefixTSPL_PROGRAM_MASTER as TabM on TabM.Program_Code=TabSM.Parent_Code
WHERE 2 = 2 "
                If clsCommon.myLen(cboScreenType.SelectedValue) > 0 Then
                    strQry &= " and TabSM.Program_Name like '%" & clsCommon.myCstr(cboScreenType.SelectedValue) & "%'"
                End If
            End If
            Dim FinalQry As String = ""
            If objCommonVar.RCDFCFP And Not chkCattleFeedOnly.Checked Then
                strQry += " order by [Count] desc"
                FinalQry += clsERPFuncationality.ConvertQryForAllUnion(strQry, "DBNamePrefix", "DBLocation", "Sno")
            Else
                strQry = strQry.Replace("DBNamePrefix", "")
                strQry = strQry.Replace("DBLocation", "")
                FinalQry += strQry
            End If
            Dim SuperFinalQry As String = ""
            If chkAcrossUnions.Checked And Not chkCattleFeedOnly.Checked Then
                SuperFinalQry = "  select top " & clsCommon.myCstr(txtTopCount.Value) & " [Screen Code],max([Screen Name]) as [Screen Name],max([Module Code]) as [Module Code],max([Module]) as [Module],sum([Count]) as [Count],sum(1) as Rep from (" & FinalQry & "
) xx group by [Screen Code] 
order by Rep desc,[Count] desc"
            Else
                SuperFinalQry += "  select * from ( " & FinalQry + " ) xx  ORDER BY [Union],[Count] DESC"
            End If


            dt = clsDBFuncationality.GetDataTable(SuperFinalQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                RadGroupBox3.Enabled = False
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                    Gv1.Columns(ii).IsVisible = True
                Next
                SetGridFormat()
                Gv1.BestFitColumns()
                EnableDisable(False)
            Else
                Throw New Exception("No Data Found to Display")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetGridFormat()
        Try
            Gv1.Columns("Screen Code").IsVisible = False
            Gv1.Columns("Module Code").IsVisible = False
            If chkAcrossUnions.Checked And Not chkCattleFeedOnly.Checked Then
                Gv1.Columns("Rep").HeaderText = "Union count"
                Gv1.Columns("Rep").IsVisible = True
            ElseIf chkDateRange.Checked Then
                Gv1.Columns("Created Date").IsVisible = False
                Gv1.Columns("Created By").IsVisible = False
            End If
            If objCommonVar.RCDFCFP And Not chkCattleFeedOnly.Checked Then
                Gv1.Columns("Union").IsVisible = True
            Else
                Gv1.Columns("Union").IsVisible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub EnableDisable(v As Boolean)
        chkDateRange.Enabled = v
        chkAcrossUnions.Enabled = v
        chkCattleFeedOnly.Enabled = v
        RadGroupBox3.Enabled = v
        txtTopCount.Enabled = v
        cboScreenType.Enabled = v
    End Sub

    Private Sub MostUserScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            chkAcrossUnions.Visible = objCommonVar.RCDFCFP
            chkCattleFeedOnly.Visible = objCommonVar.RCDFCFP
            LoadScreenType()
            cboScreenType.SelectedValue = ""
            RadGroupBox3.Visible = False
            ToDate.Value = clsCommon.GETSERVERDATE
            fromDate.Value = ToDate.Value
            txtTopCount.Value = 10
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadScreenType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = Nothing
        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "All"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Setup"
        dr("Name") = "Setup"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Transaction"
        dr("Name") = "Transaction"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Report"
        dr("Name") = "Report"
        dt.Rows.Add(dr)

        cboScreenType.DataSource = dt
        cboScreenType.DisplayMember = "Name"
        cboScreenType.ValueMember = "Code"

    End Sub

    Private Sub chkDateRange_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkDateRange.ToggleStateChanged
        RadGroupBox3.Visible = chkDateRange.Checked
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim StrHeading As String = objCommonVar.CurrentCompanyName
            Dim arrHeader As List(Of String) = New List(Of String)()
            If chkDateRange.Checked Then
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            End If
            arrHeader.Add("Print on : " + clsCommon.GetPrintDate(DateTime.Now, "dd/MM/yyyy hh:mm:ss tt") + " by " + objCommonVar.CurrentUser + " ")
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(StrHeading, Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyOldExportToPDF(StrHeading, Gv1, arrHeader, "DispathLedgerReport")



                'transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                'Dim style As New GridPrintStyle()
                ''style.FitWidthMode = PrintFitWidthMode.FitPageWidth
                ''style.PrintGrouping = True
                'style.HeaderCellBackColor = Color.White
                'style.GroupRowBackColor = Color.White
                'style.SummaryCellBackColor = Color.White
                ''style.PrintSummaries = False
                ''style.PrintHeaderOnEachPage = True
                ''style.PrintHiddenColumns = False
                'Gv1.PrintStyle = style
                'Dim doc As New clsMyPrintDocument()
                'doc.Margins.Top = 50
                'doc.Margins.Bottom = 50
                'doc.Margins.Left = 50
                'doc.Margins.Right = 50
                'doc.HeaderHeight = 90
                'If Gv1.Columns.Count >= 8 Then
                '    doc.Landscape = True
                'End If
                'doc.AssociatedObject = Gv1
                'doc.DocumentName = objCommonVar.CurrentCompanyName
                'Dim dtCompDetails As DataTable = Nothing
                'Dim strCompDetails As String = "select Phone1,Regn_No from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
                'dtCompDetails = clsDBFuncationality.GetDataTable(strCompDetails)
                'Dim strLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select MCC_Name from TSPL_MCC_MASTER where MCC_Code='" + txtMCC.Text + "'"))
                'doc.MiddleHeader = objCommonVar.CurrentCompanyName + Environment.NewLine + "Area" + " " + strLocation + " " + "Phone No." + clsCommon.myCstr(dtCompDetails.Rows(0)("Phone1")) + " " + "Reg No. " + clsCommon.myCstr(dtCompDetails.Rows(0)("Regn_No")) + Environment.NewLine + "Societywise Deduction Balance Report" + " " + clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + " "
                'doc.HeaderFont = New Font("Segoe UI", 10, FontStyle.Bold)
                ''doc.LeftUpperText = "Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")
                ''doc.LeftUpperFont = New Font("Segoe UI", 8, FontStyle.Regular)
                'doc.AssociatedObject = Gv1
                ''doc.Print()
                'doc.RightFooter = "Page [Page #] of [Total Pages]"
                'Dim dialog As New RadPrintPreviewDialog
                'dialog.Document = doc
                'dialog.ToolMenu.Visible = True
                'dialog.Show()
                'doc = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
End Class