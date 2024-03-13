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
            Dim query As String = Nothing
            Dim BaseQry As String = Nothing
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
                'Gv.DataSource = Nothing
                Exit Sub
            End If
            query = ""
            dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    BaseQry = " select [Month],0 As [Billed Qty],Sum([Farmer Qty])[Farmer Qty],COUNT(Distinct [Farmer Code])  as [Farmer Code],sum(Amount) as Amt
                                From( Select Format(TSPL_DBT_NEFT.Document_Date,'MM-yyyy') As[Month],(TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code) as MP_Uploader_Code,
                                (TSPL_DBT_NEFT_DETAIL.Amount) as Amount ,(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty)[Farmer Qty],(TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code)  as [Farmer Code]
                                from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL 
                                Left Outer JOin [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT On TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code
                                Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL On TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR   
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
                                where Convert(Date,TSPL_DBT_NEFT.Document_Date,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_DBT_NEFT.Document_Date,103)<=Convert(Date,'" + Slot2 + "',103)
                                )x group by [Month]
                                Union All
                                select  [Month], Sum([Billed Qty])[Billed Qty], 0 As [FarmerQty],0 As [FarmerCode],0 As [Amt] from
                                (select Format(TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Date,'MM-yyyy') As [Month], (Qty)[Billed Qty]
                                from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_DETAIL
                                Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DCS_MP_INCENTIVE_RECO_HEAD On TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
                                where Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Reco_Date_To,103)<=Convert(Date,'" + Slot2 + "',103)
                                )xx Group By [Month] "
                    'Dim dtMonth As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
                    'If dtMonth.Rows.Count <= 0 Then
                    '    clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
                    '    Exit Sub
                    'End If


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
                             IsNull(Sum(xxxfinal.[M3 Amt]),0)[M3 Amt]
                              from ("
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
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt2, "crptDBT_NEFTUnionReport", "Union Report", Nothing)
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
End Class