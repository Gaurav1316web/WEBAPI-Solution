Imports common
Public Class frmDBTNEFTUnionReport

    Dim Slot1 As DateTime = Nothing
    Dim Slot2 As DateTime = Nothing
    Dim Month1 As String = Nothing
    Dim Month2 As String = Nothing
    Dim Month3 As String = Nothing

    Private Sub frmDBTNEFTUnionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validated(sender As Object, e As EventArgs) Handles txtFromDate.Validated
        'Try
        '    Month()
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
    End Sub

    Sub Month()
        If clsCommon.myLen(txtFromDate.Value) > 0 Then
            Dim SM As Integer = txtFromDate.Value.Month
            Dim SY As Integer = txtFromDate.Value.Year

            Dim CD As New DateTime(SY, SM, 1)
            Slot2 = clsCommon.GetPrintDate(CD.AddMonths(3).AddDays(-1), "dd/MMM/yyyy")
            txtToDate.Value = txtFromDate.Value.AddMonths(2)
            Month1 = clsCommon.GetPrintDate(txtFromDate.Value, "MM-yyyy")
            Month2 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(1), "MM-yyyy")
            Month3 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(2), "MM-yyyy")

        End If
    End Sub
    Sub Reset()
        Gv.DataSource = Nothing
        Gv.Rows.Clear()
        Gv.MasterTemplate.SummaryRowsBottom.Clear()
        Gv.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            Dim query = ReportQry()
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                Gv.DataSource = Nothing
                Gv.Rows.Clear()
                Gv.Columns.Clear()
                Gv.GroupDescriptors.Clear()
                Gv.MasterTemplate.SummaryRowsBottom.Clear()
                Gv.MasterView.Refresh()
                Gv.DataSource = dt2
                For ii As Integer = 0 To Gv.Columns.Count - 1
                    Gv.Columns(ii).ReadOnly = True
                    'Gv.Rows.Add()
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                Gv.EnableFiltering = True
                SetGridFormat()
                Gv.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function ReportQry() As String
        Dim query As String = Nothing
        Dim BaseQry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            'Gv.DataSource = Nothing
            Exit Function
        End If
        query = ""
        dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                BaseQry = " select [Month],0 As [Billed Qty],Sum([Farmer Qty])[Farmer Qty],COUNT(Distinct [Farmer Code])  as [Farmer Code],sum(Amount) as Amt
                                From( Select Format(TSPL_DBT_NEFT.From_Date,'MM-yyyy') As[Month],(TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code) as MP_Uploader_Code,
                                (TSPL_DBT_NEFT_DETAIL.Amount) as Amount ,(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty)[Farmer Qty],(TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code)  as [Farmer Code]
                                from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL 
                                Left Outer JOin [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT On TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
                                Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL On TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR   
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
                                where TSPL_DBT_NEFT_DETAIL.PK_Id Not In (Select Against_DBT_NEFT_TR From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT_DETAIL where TSPL_DBT_NEFT_REJECT_DETAIL.Document_Code Not In (Select Document_Code From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT where TSPL_DBT_NEFT_REJECT.Against_DBT_NEFT Not In (TSPL_DBT_NEFT.Document_Code))) and                        
                                Convert(Date,TSPL_DBT_NEFT.From_Date,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_DBT_NEFT.To_Date,103)<=Convert(Date,'" + Slot2 + "',103)
                                )x group by [Month]
                                Union All
                                select  [Month], Sum([Billed Qty])[Billed Qty], 0 As [FarmerQty],0 As [FarmerCode],0 As [Amt] from
                                (select Format(TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,'MM-yyyy') As [Month], ([Billed Qty])[Billed Qty]
                                from (
								Select TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.* from (select TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code, (TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Qty)[Billed Qty] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL
								union all
								select TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Document_Code,(TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID.Qty)[Billed Qty] from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL_INVALID ) as TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
								Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD On TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
								 where Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date_To,103)<=Convert(Date,'" + Slot2 + "',103) 
								)TSPL_DCS_MP_INCENTIVE_RECO_HEAD )xx Group By [Month] "

                If ii > 0 Then
                    query += " UNION ALL "
                End If

                query += " select " + clsCommon.myCstr(ii + 1) + " AS SNo,'" + clsCommon.myCstr(dt.Rows(ii).Item("Location_Name")) + "' AS [Union Name],'" + clsCommon.GetPrintDate(txtFromDate.Value, "MMM-yyyy") + " to " + clsCommon.GetPrintDate(txtToDate.Value, "MMM-yyyy") + "' As [FromtoDate],'" + objCommonVar.CurrentUser + "' As [User], "
                query += "'" + clsCommon.GetPrintDate(Month1, "MMM-yyyy") + "' As Month1,
                              IsNull(Sum(xxxfinal.[M1 Billed Qty]),0)[M1 Billed Qty],
                              IsNull(Sum(xxxfinal.[M1 Farmer Qty]),0)[M1 Farmer Qty],
                              IsNull(Sum(xxxfinal.[M1 Farmer Code]),0)[M1 Farmer Code],
                              IsNull(Sum(xxxfinal.[M1 Amt]),0)[M1 Amt],

                              '" + clsCommon.GetPrintDate(Month2, "MMM-yyyy") + "' As Month2,
                             IsNull(Sum(xxxfinal.[M2 Billed Qty]),0)[M2 Billed Qty],
                             IsNull(Sum(xxxfinal.[M2 Farmer Qty]),0)[M2 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M2 Farmer Code]),0)[M2 Farmer Code],
                             IsNull(Sum(xxxfinal.[M2 Amt]),0)[M2 Amt],

                              '" + clsCommon.GetPrintDate(Month3, "MMM-yyyy") + "' As Month3,
                             IsNull(Sum(xxxfinal.[M3 Billed Qty]),0)[M3 Billed Qty],
                             IsNull(Sum(xxxfinal.[M3 Farmer Qty]),0)[M3 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M3 Farmer Code]),0)[M3 Farmer Code],
                             IsNull(Sum(xxxfinal.[M3 Amt]),0)[M3 Amt],

                             (IsNull(Sum(xxxfinal.[M1 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M2 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M3 Billed Qty]),0)) As [Total Billed Qty],
							 (IsNull(Sum(xxxfinal.[M1 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M2 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M3 Farmer Qty]),0)) As [Total Farmer Qty],
							 (IsNull(Sum(xxxfinal.[M1 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M2 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M3 Farmer Code]),0)) As [Total No. Of Farmer],
							 (IsNull(Sum(xxxfinal.[M1 Amt]),0)+IsNull(Sum(xxxfinal.[M2 Amt]),0)+IsNull(Sum(xxxfinal.[M3 Amt]),0)) As [Total Amt] from ("
                query += "  Select [Month] "

                query += "  ,Case When Month='" + Month1 + "' Then Sum([Billed Qty]) Else 0 End As 'M1 Billed Qty',
                                Case When Month='" + Month1 + "' Then Sum([Farmer Qty]) Else 0 End As 'M1 Farmer Qty',
                                Case When Month='" + Month1 + "' Then Sum([Farmer Code]) Else 0 End As 'M1 Farmer Code',
                                Case When Month='" + Month1 + "' Then Sum(Amt) Else 0 End As 'M1 Amt'"

                query += "  ,Case When Month='" + Month2 + "' Then Sum([Billed Qty]) Else 0 End As 'M2 Billed Qty',
                                Case When Month='" + Month2 + "' Then Sum([Farmer Qty]) Else 0 End As 'M2 Farmer Qty',
                                Case When Month='" + Month2 + "' Then Sum([Farmer Code]) Else 0 End As 'M2 Farmer Code',
                                Case When Month='" + Month2 + "' Then Sum(Amt) Else 0 End As 'M2 Amt'"

                query += " ,Case When Month='" + Month3 + "' Then Sum([Billed Qty]) Else 0 End As 'M3 Billed Qty',
                                Case When Month='" + Month3 + "' Then Sum([Farmer Qty]) Else 0 End As 'M3 Farmer Qty',
                                Case When Month='" + Month3 + "' Then Sum([Farmer Code]) Else 0 End As 'M3 Farmer Code',
                                Case When Month='" + Month3 + "' Then Sum(Amt) Else 0 End As 'M3 Amt'"

                'query += "     Sum([Billed Qty]) As [Billed Qty],Sum([Farmer Qty])[Farmer Qty],Sum([Farmer Code])  as [Farmer Code],sum(Amt) as Amt"
                query += " from (" + BaseQry + ")xxx Group By xxx.[Month]"
                query += ")xxxFinal"
            Next
        End If
        Return query
    End Function

    Sub SetGridFormat()
        'Gv.GroupDescriptors.Add(New GridGroupByExpression("Plant as Plant format ""{0}: {1}"" Group By Plant"))
        'Gv.GroupDescriptors.Add(New GridGroupByExpression("Mcc as Mcc format ""{0}: {1}"" Group By Mcc"))
        Gv.AutoExpandGroups = True
        Gv.ShowGroupPanel = True
        Gv.ShowRowHeaderColumn = False
        Gv.AllowAddNewRow = False
        Gv.AllowDeleteRow = False
        Gv.EnableFiltering = True
        Gv.ShowFilteringRow = True


        For ii As Integer = 0 To Gv.Columns.Count - 1
            Gv.Columns(ii).ReadOnly = True
            Gv.Columns(ii).BestFit()
        Next
        Gv.Columns("SNo").HeaderText = "S.No."
        Gv.Columns("SNo").IsVisible = True

        Gv.Columns("Union Name").HeaderText = "Union Name"
        Gv.Columns("Union Name").Width = 500
        Gv.Columns("Union Name").IsVisible = True

        Gv.Columns("Month1").HeaderText = "Month 1"
        Gv.Columns("Month1").Width = 200
        Gv.Columns("Month1").FormatString = ""

        Gv.Columns("M1 Billed Qty").HeaderText = "Qty"
        Gv.Columns("M1 Billed Qty").Width = 200
        Gv.Columns("M1 Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("M1 Farmer Qty").HeaderText = "Farmer Qty"
        Gv.Columns("M1 Farmer Qty").Width = 200
        Gv.Columns("M1 Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("M1 Farmer Code").HeaderText = "No. Of Farmer"
        Gv.Columns("M1 Farmer Code").Width = 200
        Gv.Columns("M1 Farmer Code").FormatString = "{0:n2}"

        Gv.Columns("M1 Amt").HeaderText = "Amount"
        Gv.Columns("M1 Amt").Width = 200
        Gv.Columns("M1 Amt").FormatString = "{0:n2}"

        Gv.Columns("Month2").HeaderText = "Month 2"
        Gv.Columns("Month2").Width = 200
        Gv.Columns("Month2").FormatString = ""

        Gv.Columns("M2 Billed Qty").HeaderText = "Qty"
        Gv.Columns("M2 Billed Qty").Width = 200
        Gv.Columns("M2 Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("M2 Farmer Qty").HeaderText = "Farmer Qty"
        Gv.Columns("M2 Farmer Qty").Width = 200
        Gv.Columns("M2 Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("M2 Farmer Code").HeaderText = "No. Of Farmer"
        Gv.Columns("M2 Farmer Code").Width = 200
        Gv.Columns("M2 Farmer Code").FormatString = "{0:n2}"

        Gv.Columns("M2 Amt").HeaderText = "Amount"
        Gv.Columns("M2 Amt").Width = 200
        Gv.Columns("M2 Amt").FormatString = "{0:n2}"

        Gv.Columns("Month3").HeaderText = "Month 3"
        Gv.Columns("Month3").Width = 200
        Gv.Columns("Month3").FormatString = ""

        Gv.Columns("M3 Billed Qty").HeaderText = "Qty"
        Gv.Columns("M3 Billed Qty").Width = 200
        Gv.Columns("M3 Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("M3 Farmer Qty").HeaderText = "Farmer Qty"
        Gv.Columns("M3 Farmer Qty").Width = 200
        Gv.Columns("M3 Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("M3 Farmer Code").HeaderText = "No. Of Farmer"
        Gv.Columns("M3 Farmer Code").Width = 200
        Gv.Columns("M3 Farmer Code").FormatString = "{0:n2}"

        Gv.Columns("M3 Amt").HeaderText = "Amount"
        Gv.Columns("M3 Amt").Width = 200
        Gv.Columns("M3 Amt").FormatString = "{0:n2}"

        Gv.Columns("Total Billed Qty").HeaderText = "Billed Qty"
        Gv.Columns("Total Billed Qty").Width = 200
        Gv.Columns("Total Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("Total Farmer Qty").HeaderText = "Farmer Qty"
        Gv.Columns("Total Farmer Qty").Width = 200
        Gv.Columns("Total Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("Total No. Of Farmer").HeaderText = "No. Of Farmer"
        Gv.Columns("Total No. Of Farmer").Width = 200
        Gv.Columns("Total No. Of Farmer").FormatString = "{0:n2}"

        Gv.Columns("Total Amt").HeaderText = "Amount"
        Gv.Columns("Total Amt").Width = 200
        Gv.Columns("Total Amt").FormatString = "{0:n2}"

        'Gv.Columns("Dis_SNFKG").HeaderText = "SNFKG"
        'Gv.Columns("Dis_SNFKG").IsVisible = True
        'Gv.Columns("Dis_SNFKG").FormatString = "{0:n2}"

        Gv.ShowGroupPanel = True
        Gv.MasterTemplate.AutoExpandGroups = True
        View()
        SummaryRow()
    End Sub

    Sub View()

        If Gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup("Union"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv.Columns("Union Name").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month1, "MMM-yyyy")))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv.Columns("M1 Billed Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv.Columns("M1 Farmer Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv.Columns("M1 Farmer Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv.Columns("M1 Amt").Name)



            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month2, "MMM-yyyy")))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv.Columns("M2 Billed Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv.Columns("M2 Farmer Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv.Columns("M2 Farmer Code").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv.Columns("M2 Amt").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month3, "MMM-yyyy")))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv.Columns("M3 Billed Qty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv.Columns("M3 Farmer Qty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv.Columns("M3 Farmer Code").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv.Columns("M3 Amt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv.Columns("Total Billed Qty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv.Columns("Total Farmer Qty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv.Columns("Total No. Of Farmer").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(Gv.Columns("Total Amt").Name)
            Gv.ViewDefinition = view
        End If
    End Sub

    Sub SummaryRow()
        If Gv.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For i As Integer = 3 To Gv.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(Gv.Columns(i).Name, "{0:F2}", GridAggregateFunction.Sum))
            Next
            Gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            Gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        End If
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        Try
            Dim SM As Integer = txtFromDate.Value.Month
            Dim SY As Integer = txtFromDate.Value.Year

            Dim CD As New DateTime(SY, SM, 1)
            Slot1 = clsCommon.GetPrintDate(CD, "dd/MMM/yyyy")
            Month()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim query = ReportQry()
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.UnionReports, dt2, "crptDBT_NEFTUnionReport", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class