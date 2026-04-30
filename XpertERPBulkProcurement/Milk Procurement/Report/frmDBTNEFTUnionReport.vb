Imports common
Public Class frmDBTNEFTUnionReport

    Dim Slot1 As DateTime = Nothing
    Dim Slot2 As DateTime = Nothing
    Dim Month1 As String = Nothing
    Dim Month2 As String = Nothing
    Dim Month3 As String = Nothing
    Dim Month4 As String = Nothing
    Dim Month5 As String = Nothing
    Dim Month6 As String = Nothing
    Dim Month7 As String = Nothing
    Dim Month8 As String = Nothing
    Dim Month9 As String = Nothing
    Dim Month10 As String = Nothing
    Dim Month11 As String = Nothing
    Dim Month12 As String = Nothing

    Private Sub frmDBTNEFTUnionReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtToDate.Value = clsCommon.GETSERVERDATE()
            If rbtnQuarterly.IsChecked Then
                txtFromDate.Value = txtToDate.Value.AddMonths(-2)
            ElseIf rbtnYearly.IsChecked Then
                txtFromDate.Value = txtToDate.Value.AddMonths(-11)
            End If
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
            If rbtnYearly.IsChecked Then
                Slot2 = clsCommon.GetPrintDate(CD.AddMonths(12).AddDays(-1), "dd/MMM/yyyy")
                txtToDate.Value = txtFromDate.Value.AddMonths(11)
                Month4 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(3), "MM-yyyy")
                Month5 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(4), "MM-yyyy")
                Month6 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(5), "MM-yyyy")
                Month7 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(6), "MM-yyyy")
                Month8 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(7), "MM-yyyy")
                Month9 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(8), "MM-yyyy")
                Month10 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(9), "MM-yyyy")
                Month11 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(10), "MM-yyyy")
                Month12 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(11), "MM-yyyy")
            ElseIf rbtnQuarterly.IsChecked Then
                Slot2 = clsCommon.GetPrintDate(CD.AddMonths(3).AddDays(-1), "dd/MMM/yyyy")
                txtToDate.Value = txtFromDate.Value.AddMonths(2)
            End If
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
        chkOnlyReject.Checked = False
        rbtnYearly.IsChecked = True
        EnableDisableCtrl(True)
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
                'gv.DataSource = dt2

                If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                    gv.DataSource = dt2
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv.EnableFiltering = True
                    EnableDisableCtrl(False)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                End If

                For ii As Integer = 0 To Gv.Columns.Count - 1
                    Gv.Columns(ii).ReadOnly = True
                    'Gv.Columns(ii). = "#,0"
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
            Throw New Exception("Database[TSPL_MASTER] not found")
        End If
        query = ""
        Dim Qry As String = Nothing
        Dim arrUnion As New ArrayList()
        arrUnion.Add(objCommonVar.CurrComp_Code1)
        If clsCommon.myLen(objCommonVar.CurrentUnionDataBase) > 0 Then
            Qry = " Select TSPL_USER_MASTER.DataBase_Name,[TSPL_APP_LOCATION].Location_Name from TSPL_USER_MASTER 
                    left outer join TSPL_MASTER.dbo.[TSPL_APP_LOCATION] on [TSPL_APP_LOCATION].DataBase_Name=TSPL_USER_MASTER.DataBase_Name where User_Code = '" + objCommonVar.CurrentUserCode + "' "
            dt = clsDBFuncationality.GetDataTable(Qry)
            'txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleUnionDs", Qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
        Else
            If objCommonVar.RCDFCFP Then
                dt = clsMilkUnion.UnionDBName()
            Else
                dt = clsMilkUnion.UnionDBName1(arrUnion)
            End If
        End If
        'If objCommonVar.RCDFCFP Then
        '    dt = clsMilkUnion.UnionDBName()
        'Else
        '    dt = clsMilkUnion.UnionDBName1(arrUnion)
        'End If
        '  dt = clsMilkUnion.UnionDBName()

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                BaseQry = " select [Month],0 As [No Of Doc],0 As [Billed Qty],Sum([Farmer Qty])[Farmer Qty],COUNT(Distinct [Farmer Code])  as [Farmer Code],sum(Amount) as Amt
                                From( Select Format(TSPL_DBT_NEFT.From_Date,'MM-yyyy') As[Month],(TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code) as MP_Uploader_Code,
                                (TSPL_DBT_NEFT_DETAIL.Amount) as Amount ,(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty)[Farmer Qty],(TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code)  as [Farmer Code]
                                from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL 

                                inner join (select * from ( select ROW_NUMBER() over(Partition by from_date order by UKID) as Rep,Document_Code,RCDF_Status,From_Date,To_Date from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT)x where rep=1 )TSPL_DBT_NEFT on TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_DETAIL.Document_Code

                                Left Outer Join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL On TSPL_MP_INCENTIVE_ENTRY_DETAIL.PK_Id=TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR   
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                                left outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code "
                If chkOnlyReject.Checked Then
                    BaseQry += " where TSPL_DBT_NEFT_DETAIL.PK_Id In (Select Against_DBT_NEFT_TR From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT_DETAIL where TSPL_DBT_NEFT_REJECT_DETAIL.Document_Code In (Select Document_Code From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT where TSPL_DBT_NEFT_REJECT.Against_DBT_NEFT In (TSPL_DBT_NEFT.Document_Code))) and                        
                                Convert(Date,TSPL_DBT_NEFT.From_Date,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_DBT_NEFT.To_Date,103)<=Convert(Date,'" + Slot2 + "',103) "
                Else
                    'Comment By balwinder on 30/04/2025 as All data should come if only reject check box is off
                    'BaseQry += " where TSPL_DBT_NEFT_DETAIL.PK_Id Not In (Select Against_DBT_NEFT_TR From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT_DETAIL where TSPL_DBT_NEFT_REJECT_DETAIL.Document_Code Not In (Select Document_Code From [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT where TSPL_DBT_NEFT_REJECT.Against_DBT_NEFT Not In (TSPL_DBT_NEFT.Document_Code))) and                        
                    '            Convert(Date,TSPL_DBT_NEFT.From_Date,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_DBT_NEFT.To_Date,103)<=Convert(Date,'" + Slot2 + "',103) "
                End If
                BaseQry += "    )x group by [Month] "
                If chkOnlyReject.Checked Then
                    BaseQry += " Union All
							  Select [Month],COUNT(Document_Code)[No Of Doc],0 As [Billed Qty],0 As [Farmer Qty],0 as [Farmer Code],0 as Amt from (
							  Select Format(TSPL_DBT_NEFT.From_Date,'MM-yyyy') As[Month],TSPL_DBT_NEFT_REJECT.Document_Code  
							  from [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_REJECT 
							  Left Outer join [" + clsCommon.myCstr(dt.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT On TSPL_DBT_NEFT.Document_Code=TSPL_DBT_NEFT_REJECT.Against_DBT_NEFT
							  Where Convert(Date,TSPL_DBT_NEFT.From_Date,103)>=Convert(Date,'" + Slot1 + "',103) And Convert(Date,TSPL_DBT_NEFT.To_Date,103)<=Convert(Date,'" + Slot2 + "',103))xxxRejectDoc Group By [Month] "
                End If
                BaseQry += " Union All
                                select  [Month],0 As [No Of Doc], Sum([Billed Qty])[Billed Qty], 0 As [FarmerQty],0 As [FarmerCode],0 As [Amt] from
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
                query += "'" + clsCommon.GetPrintDate(Month1, "MMM-yyyy") + "' As Month1,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M1 No Of Doc]),0)[M1 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M1 Billed Qty]),0)[M1 Billed Qty],
                              IsNull(Sum(xxxfinal.[M1 Farmer Qty]),0)[M1 Farmer Qty],
                              IsNull(Sum(xxxfinal.[M1 Farmer Code]),0)[M1 Farmer Code],
                              IsNull(Sum(xxxfinal.[M1 Amt]),0)[M1 Amt],"

                query += "'" + clsCommon.GetPrintDate(Month2, "MMM-yyyy") + "' As Month2,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M2 No Of Doc]),0)[M2 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M2 Billed Qty]),0)[M2 Billed Qty],
                             IsNull(Sum(xxxfinal.[M2 Farmer Qty]),0)[M2 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M2 Farmer Code]),0)[M2 Farmer Code],
                             IsNull(Sum(xxxfinal.[M2 Amt]),0)[M2 Amt],"

                query += " '" + clsCommon.GetPrintDate(Month3, "MMM-yyyy") + "' As Month3,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M3 No Of Doc]),0)[M3 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M3 Billed Qty]),0)[M3 Billed Qty],
                             IsNull(Sum(xxxfinal.[M3 Farmer Qty]),0)[M3 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M3 Farmer Code]),0)[M3 Farmer Code],
                             IsNull(Sum(xxxfinal.[M3 Amt]),0)[M3 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month4, "MMM-yyyy") + "' As Month4,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M4 No Of Doc]),0)[M4 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M4 Billed Qty]),0)[M4 Billed Qty],
                             IsNull(Sum(xxxfinal.[M4 Farmer Qty]),0)[M4 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M4 Farmer Code]),0)[M4 Farmer Code],
                             IsNull(Sum(xxxfinal.[M4 Amt]),0)[M4 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month5, "MMM-yyyy") + "' As Month5,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M5 No Of Doc]),0)[M5 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M5 Billed Qty]),0)[M5 Billed Qty],
                             IsNull(Sum(xxxfinal.[M5 Farmer Qty]),0)[M5 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M5 Farmer Code]),0)[M5 Farmer Code],
                             IsNull(Sum(xxxfinal.[M5 Amt]),0)[M5 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month6, "MMM-yyyy") + "' As Month6,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M6 No Of Doc]),0)[M6 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M6 Billed Qty]),0)[M6 Billed Qty],
                             IsNull(Sum(xxxfinal.[M6 Farmer Qty]),0)[M6 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M6 Farmer Code]),0)[M6 Farmer Code],
                             IsNull(Sum(xxxfinal.[M6 Amt]),0)[M6 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month7, "MMM-yyyy") + "' As Month7,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M7 No Of Doc]),0)[M7 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M7 Billed Qty]),0)[M7 Billed Qty],
                             IsNull(Sum(xxxfinal.[M7 Farmer Qty]),0)[M7 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M7 Farmer Code]),0)[M7 Farmer Code],
                             IsNull(Sum(xxxfinal.[M7 Amt]),0)[M7 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month8, "MMM-yyyy") + "' As Month8,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M8 No Of Doc]),0)[M8 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M8 Billed Qty]),0)[M8 Billed Qty],
                             IsNull(Sum(xxxfinal.[M8 Farmer Qty]),0)[M8 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M8 Farmer Code]),0)[M8 Farmer Code],
                             IsNull(Sum(xxxfinal.[M8 Amt]),0)[M8 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month9, "MMM-yyyy") + "' As Month9,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M9 No Of Doc]),0)[M9 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M9 Billed Qty]),0)[M9 Billed Qty],
                             IsNull(Sum(xxxfinal.[M9 Farmer Qty]),0)[M9 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M9 Farmer Code]),0)[M9 Farmer Code],
                             IsNull(Sum(xxxfinal.[M9 Amt]),0)[M9 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month10, "MMM-yyyy") + "' As Month10,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M10 No Of Doc]),0)[M10 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M10 Billed Qty]),0)[M10 Billed Qty],
                             IsNull(Sum(xxxfinal.[M10 Farmer Qty]),0)[M10 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M10 Farmer Code]),0)[M10 Farmer Code],
                             IsNull(Sum(xxxfinal.[M10 Amt]),0)[M10 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month11, "MMM-yyyy") + "' As Month11,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M11 No Of Doc]),0)[M11 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M11 Billed Qty]),0)[M11 Billed Qty],
                             IsNull(Sum(xxxfinal.[M11 Farmer Qty]),0)[M11 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M11 Farmer Code]),0)[M11 Farmer Code],
                             IsNull(Sum(xxxfinal.[M11 Amt]),0)[M11 Amt], "

                query += " '" + clsCommon.GetPrintDate(Month12, "MMM-yyyy") + "' As Month12,"
                If chkOnlyReject.Checked Then
                    query += " IsNull(Sum(xxxfinal.[M12 No Of Doc]),0)[M12 No Of Doc],"
                End If
                query += " IsNull(Sum(xxxfinal.[M12 Billed Qty]),0)[M12 Billed Qty],
                             IsNull(Sum(xxxfinal.[M12 Farmer Qty]),0)[M12 Farmer Qty],
                             IsNull(Sum(xxxfinal.[M12 Farmer Code]),0)[M12 Farmer Code],
                             IsNull(Sum(xxxfinal.[M12 Amt]),0)[M12 Amt], "

                If rbtnYearly.IsChecked Then
                    'query += "'" + clsCommon.GetPrintDate(Month4, "MMM-yyyy") + "' As Month4,"
                    'query += " IsNull(Sum(xxxfinal.[M4 Amt]),0)[M4 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month5, "MMM-yyyy") + "' As Month5,"
                    'query += " IsNull(Sum(xxxfinal.[M5 Amt]),0)[M5 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month6, "MMM-yyyy") + "' As Month6,"
                    'query += " IsNull(Sum(xxxfinal.[M6 Amt]),0)[M6 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month7, "MMM-yyyy") + "' As Month7,"
                    'query += " IsNull(Sum(xxxfinal.[M7 Amt]),0)[M7 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month8, "MMM-yyyy") + "' As Month8,"
                    'query += " IsNull(Sum(xxxfinal.[M8 Amt]),0)[M8 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month9, "MMM-yyyy") + "' As Month9,"
                    'query += " IsNull(Sum(xxxfinal.[M9 Amt]),0)[M9 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month10, "MMM-yyyy") + "' As Month10,"
                    'query += " IsNull(Sum(xxxfinal.[M10 Amt]),0)[M10 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month11, "MMM-yyyy") + "' As Month11,"
                    'query += " IsNull(Sum(xxxfinal.[M11 Amt]),0)[M11 Amt],"
                    'query += "'" + clsCommon.GetPrintDate(Month12, "MMM-yyyy") + "' As Month12,"
                    'query += " IsNull(Sum(xxxfinal.[M12 Amt]),0)[M12 Amt],"

                End If
                If chkOnlyReject.Checked Then
                    query += " (IsNull(Sum(xxxfinal.[M1 No Of Doc]),0)+IsNull(Sum(xxxfinal.[M2 No Of Doc]),0)+IsNull(Sum(xxxfinal.[M3 No Of Doc]),0)) As [Total Document], "
                End If
                query += " (IsNull(Sum(xxxfinal.[M1 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M2 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M3 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M4 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M5 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M6 Billed Qty]),0)
							 +IsNull(Sum(xxxfinal.[M7 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M8 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M9 Billed Qty]),0)
							 +IsNull(Sum(xxxfinal.[M10 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M11 Billed Qty]),0)+IsNull(Sum(xxxfinal.[M12 Billed Qty]),0)) As [Total Billed Qty],

							 (IsNull(Sum(xxxfinal.[M1 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M2 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M3 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M4 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M5 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M6 Farmer Qty]),0)
							 +IsNull(Sum(xxxfinal.[M7 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M8 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M9 Farmer Qty]),0)
							 +IsNull(Sum(xxxfinal.[M10 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M11 Farmer Qty]),0)+IsNull(Sum(xxxfinal.[M12 Farmer Qty]),0)) As [Total Farmer Qty],

							 (IsNull(Sum(xxxfinal.[M1 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M2 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M3 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M4 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M5 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M6 Farmer Code]),0)
							 +IsNull(Sum(xxxfinal.[M7 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M8 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M9 Farmer Code]),0)
							 +IsNull(Sum(xxxfinal.[M10 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M11 Farmer Code]),0)+IsNull(Sum(xxxfinal.[M12 Farmer Code]),0)) As [Total No. Of Farmer],

							 (IsNull(Sum(xxxfinal.[M1 Amt]),0)+IsNull(Sum(xxxfinal.[M2 Amt]),0)+IsNull(Sum(xxxfinal.[M3 Amt]),0)+IsNull(Sum(xxxfinal.[M4 Amt]),0)+IsNull(Sum(xxxfinal.[M5 Amt]),0)+IsNull(Sum(xxxfinal.[M6 Amt]),0)+IsNull(Sum(xxxfinal.[M7 Amt]),0)+IsNull(Sum(xxxfinal.[M8 Amt]),0)+IsNull(Sum(xxxfinal.[M9 Amt]),0)+IsNull(Sum(xxxfinal.[M10 Amt]),0)+IsNull(Sum(xxxfinal.[M11 Amt]),0)+IsNull(Sum(xxxfinal.[M12 Amt]),0)) "
                If rbtnYearly.IsChecked Then
                    query += " +IsNull(Sum(xxxfinal.[M4 Amt]),0) +IsNull(Sum(xxxfinal.[M5 Amt]),0) +IsNull(Sum(xxxfinal.[M6 Amt]),0) +IsNull(Sum(xxxfinal.[M7 Amt]),0) +IsNull(Sum(xxxfinal.[M8 Amt]),0) +IsNull(Sum(xxxfinal.[M9 Amt]),0) +IsNull(Sum(xxxfinal.[M10 Amt]),0) +IsNull(Sum(xxxfinal.[M11 Amt]),0) +IsNull(Sum(xxxfinal.[M12 Amt]),0)  "
                End If
                query += " As [Total Amt] from ("
                query += "  Select [Month], "

                If chkOnlyReject.Checked Then
                    query += "  Case When Month='" + Month1 + "' Then Sum([No Of Doc]) Else 0 End As 'M1 No Of Doc', "
                End If
                query += " Case When Month='" + Month1 + "' Then Sum([Billed Qty]) Else 0 End As 'M1 Billed Qty',
                                Case When Month='" + Month1 + "' Then Sum([Farmer Qty]) Else 0 End As 'M1 Farmer Qty',
                                Case When Month='" + Month1 + "' Then Sum([Farmer Code]) Else 0 End As 'M1 Farmer Code',
                                Case When Month='" + Month1 + "' Then Sum(Amt) Else 0 End As 'M1 Amt', "
                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month2 + "' Then Sum([No Of Doc]) Else 0 End As 'M2 No Of Doc', "
                End If
                query += " Case When Month='" + Month2 + "' Then Sum([Billed Qty]) Else 0 End As 'M2 Billed Qty',
                                Case When Month='" + Month2 + "' Then Sum([Farmer Qty]) Else 0 End As 'M2 Farmer Qty',
                                Case When Month='" + Month2 + "' Then Sum([Farmer Code]) Else 0 End As 'M2 Farmer Code',
                                Case When Month='" + Month2 + "' Then Sum(Amt) Else 0 End As 'M2 Amt', "
                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month3 + "' Then Sum([No Of Doc]) Else 0 End As 'M3 No Of Doc', "
                End If
                query += " Case When Month='" + Month3 + "' Then Sum([Billed Qty]) Else 0 End As 'M3 Billed Qty',
                                Case When Month='" + Month3 + "' Then Sum([Farmer Qty]) Else 0 End As 'M3 Farmer Qty',
                                Case When Month='" + Month3 + "' Then Sum([Farmer Code]) Else 0 End As 'M3 Farmer Code',
                                Case When Month='" + Month3 + "' Then Sum(Amt) Else 0 End As 'M3 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month4 + "' Then Sum([No Of Doc]) Else 0 End As 'M4 No Of Doc', "
                End If
                query += " Case When Month='" + Month4 + "' Then Sum([Billed Qty]) Else 0 End As 'M4 Billed Qty',
                                Case When Month='" + Month4 + "' Then Sum([Farmer Qty]) Else 0 End As 'M4 Farmer Qty',
                                Case When Month='" + Month4 + "' Then Sum([Farmer Code]) Else 0 End As 'M4 Farmer Code',
                                Case When Month='" + Month4 + "' Then Sum(Amt) Else 0 End As 'M4 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month5 + "' Then Sum([No Of Doc]) Else 0 End As 'M5 No Of Doc', "
                End If
                query += " Case When Month='" + Month5 + "' Then Sum([Billed Qty]) Else 0 End As 'M5 Billed Qty',
                                Case When Month='" + Month5 + "' Then Sum([Farmer Qty]) Else 0 End As 'M5 Farmer Qty',
                                Case When Month='" + Month5 + "' Then Sum([Farmer Code]) Else 0 End As 'M5 Farmer Code',
                                Case When Month='" + Month5 + "' Then Sum(Amt) Else 0 End As 'M5 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month6 + "' Then Sum([No Of Doc]) Else 0 End As 'M6 No Of Doc', "
                End If
                query += " Case When Month='" + Month6 + "' Then Sum([Billed Qty]) Else 0 End As 'M6 Billed Qty',
                                Case When Month='" + Month6 + "' Then Sum([Farmer Qty]) Else 0 End As 'M6 Farmer Qty',
                                Case When Month='" + Month6 + "' Then Sum([Farmer Code]) Else 0 End As 'M6 Farmer Code',
                                Case When Month='" + Month6 + "' Then Sum(Amt) Else 0 End As 'M6 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month7 + "' Then Sum([No Of Doc]) Else 0 End As 'M7 No Of Doc', "
                End If
                query += " Case When Month='" + Month7 + "' Then Sum([Billed Qty]) Else 0 End As 'M7 Billed Qty',
                                Case When Month='" + Month7 + "' Then Sum([Farmer Qty]) Else 0 End As 'M7 Farmer Qty',
                                Case When Month='" + Month7 + "' Then Sum([Farmer Code]) Else 0 End As 'M7 Farmer Code',
                                Case When Month='" + Month7 + "' Then Sum(Amt) Else 0 End As 'M7 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month8 + "' Then Sum([No Of Doc]) Else 0 End As 'M8 No Of Doc', "
                End If
                query += " Case When Month='" + Month8 + "' Then Sum([Billed Qty]) Else 0 End As 'M8 Billed Qty',
                                Case When Month='" + Month8 + "' Then Sum([Farmer Qty]) Else 0 End As 'M8 Farmer Qty',
                                Case When Month='" + Month8 + "' Then Sum([Farmer Code]) Else 0 End As 'M8 Farmer Code',
                                Case When Month='" + Month8 + "' Then Sum(Amt) Else 0 End As 'M8 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month9 + "' Then Sum([No Of Doc]) Else 0 End As 'M9 No Of Doc', "
                End If
                query += " Case When Month='" + Month9 + "' Then Sum([Billed Qty]) Else 0 End As 'M9 Billed Qty',
                                Case When Month='" + Month9 + "' Then Sum([Farmer Qty]) Else 0 End As 'M9 Farmer Qty',
                                Case When Month='" + Month9 + "' Then Sum([Farmer Code]) Else 0 End As 'M9 Farmer Code',
                                Case When Month='" + Month9 + "' Then Sum(Amt) Else 0 End As 'M9 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month10 + "' Then Sum([No Of Doc]) Else 0 End As 'M10 No Of Doc', "
                End If
                query += " Case When Month='" + Month10 + "' Then Sum([Billed Qty]) Else 0 End As 'M10 Billed Qty',
                                Case When Month='" + Month10 + "' Then Sum([Farmer Qty]) Else 0 End As 'M10 Farmer Qty',
                                Case When Month='" + Month10 + "' Then Sum([Farmer Code]) Else 0 End As 'M10 Farmer Code',
                                Case When Month='" + Month10 + "' Then Sum(Amt) Else 0 End As 'M10 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month11 + "' Then Sum([No Of Doc]) Else 0 End As 'M11 No Of Doc', "
                End If
                query += " Case When Month='" + Month11 + "' Then Sum([Billed Qty]) Else 0 End As 'M11 Billed Qty',
                                Case When Month='" + Month11 + "' Then Sum([Farmer Qty]) Else 0 End As 'M11 Farmer Qty',
                                Case When Month='" + Month11 + "' Then Sum([Farmer Code]) Else 0 End As 'M11 Farmer Code',
                                Case When Month='" + Month11 + "' Then Sum(Amt) Else 0 End As 'M11 Amt',"

                If chkOnlyReject.Checked Then
                    query += " Case When Month='" + Month12 + "' Then Sum([No Of Doc]) Else 0 End As 'M12 No Of Doc', "
                End If
                query += " Case When Month='" + Month12 + "' Then Sum([Billed Qty]) Else 0 End As 'M12 Billed Qty',
                                Case When Month='" + Month12 + "' Then Sum([Farmer Qty]) Else 0 End As 'M12 Farmer Qty',
                                Case When Month='" + Month12 + "' Then Sum([Farmer Code]) Else 0 End As 'M12 Farmer Code',
                                Case When Month='" + Month12 + "' Then Sum(Amt) Else 0 End As 'M12 Amt'"

                If rbtnYearly.IsChecked Then
                    'query += " , Case When Month='" + Month4 + "' Then Sum(Amt) Else 0 End As 'M4 Amt', "
                    'query += "  Case When Month='" + Month5 + "' Then Sum(Amt) Else 0 End As 'M5 Amt', "
                    'query += "  Case When Month='" + Month6 + "' Then Sum(Amt) Else 0 End As 'M6 Amt', "
                    'query += "  Case When Month='" + Month7 + "' Then Sum(Amt) Else 0 End As 'M7 Amt', "
                    'query += "  Case When Month='" + Month8 + "' Then Sum(Amt) Else 0 End As 'M8 Amt', "
                    'query += "  Case When Month='" + Month9 + "' Then Sum(Amt) Else 0 End As 'M9 Amt', "
                    'query += "  Case When Month='" + Month10 + "' Then Sum(Amt) Else 0 End As 'M10 Amt', "
                    'query += "  Case When Month='" + Month11 + "' Then Sum(Amt) Else 0 End As 'M11 Amt', "
                    'query += "  Case When Month='" + Month12 + "' Then Sum(Amt) Else 0 End As 'M12 Amt' "

                End If
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
        gv.ShowFilteringRow = True
        gv.ShowGroupPanel = False
        'EnableDisableCtrl(False)


        For ii As Integer = 0 To Gv.Columns.Count - 1
            Gv.Columns(ii).ReadOnly = True
            Gv.Columns(ii).BestFit()
        Next
        Gv.Columns("SNo").HeaderText = "S.No."
        Gv.Columns("SNo").IsVisible = True
        Gv.Columns("FromtoDate").IsVisible = False
        Gv.Columns("User").IsVisible = False

        If rbtnYearly.IsChecked Then
            gv.Columns("M1 Billed Qty").IsVisible = True
            gv.Columns("M1 Farmer Qty").IsVisible = True
            gv.Columns("M1 Farmer Code").IsVisible = True
            gv.Columns("M2 Billed Qty").IsVisible = True
            gv.Columns("M2 Farmer Qty").IsVisible = True
            gv.Columns("M2 Farmer Code").IsVisible = True
            gv.Columns("M3 Billed Qty").IsVisible = True
            gv.Columns("M3 Farmer Qty").IsVisible = True
            gv.Columns("M3 Farmer Code").IsVisible = True
            gv.Columns("M4 Billed Qty").IsVisible = True
            gv.Columns("M4 Farmer Qty").IsVisible = True
            gv.Columns("M4 Farmer Code").IsVisible = True
            gv.Columns("M5 Billed Qty").IsVisible = True
            gv.Columns("M5 Farmer Qty").IsVisible = True
            gv.Columns("M5 Farmer Code").IsVisible = True
            gv.Columns("M6 Billed Qty").IsVisible = True
            gv.Columns("M6 Farmer Qty").IsVisible = True
            gv.Columns("M6 Farmer Code").IsVisible = True
            gv.Columns("M7 Billed Qty").IsVisible = True
            gv.Columns("M7 Farmer Qty").IsVisible = True
            gv.Columns("M7 Farmer Code").IsVisible = True
            gv.Columns("M8 Billed Qty").IsVisible = True
            gv.Columns("M8 Farmer Qty").IsVisible = True
            gv.Columns("M8 Farmer Code").IsVisible = True
            gv.Columns("M9 Billed Qty").IsVisible = True
            gv.Columns("M9 Farmer Qty").IsVisible = True
            gv.Columns("M9 Farmer Code").IsVisible = True
            gv.Columns("M10 Billed Qty").IsVisible = True
            gv.Columns("M10 Farmer Qty").IsVisible = True
            gv.Columns("M10 Farmer Code").IsVisible = True
            gv.Columns("M11 Billed Qty").IsVisible = True
            gv.Columns("M11 Farmer Qty").IsVisible = True
            gv.Columns("M11 Farmer Code").IsVisible = True
            gv.Columns("M12 Billed Qty").IsVisible = True
            gv.Columns("M12 Farmer Qty").IsVisible = True
            gv.Columns("M12 Farmer Code").IsVisible = True
            gv.Columns("Total Billed Qty").IsVisible = True
            gv.Columns("Total Farmer Qty").IsVisible = True
            gv.Columns("Total No. Of Farmer").IsVisible = True
            gv.Columns("Month1").IsVisible = False
            Gv.Columns("Month2").IsVisible = False
            Gv.Columns("Month3").IsVisible = False
            Gv.Columns("Month4").IsVisible = False
            Gv.Columns("Month5").IsVisible = False
            Gv.Columns("Month6").IsVisible = False
            Gv.Columns("Month7").IsVisible = False
            Gv.Columns("Month8").IsVisible = False
            Gv.Columns("Month9").IsVisible = False
            Gv.Columns("Month10").IsVisible = False
            Gv.Columns("Month11").IsVisible = False
            Gv.Columns("Month12").IsVisible = False

        End If

        Gv.Columns("Union Name").HeaderText = "Union Name"
        Gv.Columns("Union Name").Width = 500
        Gv.Columns("Union Name").IsVisible = True

        Gv.Columns("Month1").HeaderText = "Month 1"
        gv.Columns("Month1").Width = 100
        gv.Columns("Month1").FormatString = ""

        If chkOnlyReject.Checked Then
            Gv.Columns("M1 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M1 No Of Doc").Width = 100
            gv.Columns("M1 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M1 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M1 Billed Qty").Width = 100
        gv.Columns("M1 Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("M1 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M1 Farmer Qty").Width = 100
        gv.Columns("M1 Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("M1 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M1 Farmer Code").Width = 100
        gv.Columns("M1 Farmer Code").FormatString = "{0:n2}"

        Gv.Columns("M1 Amt").HeaderText = "Amount"
        gv.Columns("M1 Amt").Width = 100
        gv.Columns("M1 Amt").FormatString = "{0:n2}"

        Gv.Columns("Month2").HeaderText = "Month 2"
        gv.Columns("Month2").Width = 100
        gv.Columns("Month2").FormatString = ""

        If chkOnlyReject.Checked Then
            Gv.Columns("M2 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M2 No Of Doc").Width = 100
            gv.Columns("M2 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M2 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M2 Billed Qty").Width = 100
        gv.Columns("M2 Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("M2 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M2 Farmer Qty").Width = 100
        gv.Columns("M2 Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("M2 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M2 Farmer Code").Width = 100
        gv.Columns("M2 Farmer Code").FormatString = "{0:n2}"

        Gv.Columns("M2 Amt").HeaderText = "Amount"
        gv.Columns("M2 Amt").Width = 100
        gv.Columns("M2 Amt").FormatString = "{0:n2}"

        Gv.Columns("Month3").HeaderText = "Month 3"
        gv.Columns("Month3").Width = 100
        gv.Columns("Month3").FormatString = ""

        If chkOnlyReject.Checked Then
            Gv.Columns("M3 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M3 No Of Doc").Width = 100
            gv.Columns("M3 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M3 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M3 Billed Qty").Width = 100
        gv.Columns("M3 Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("M3 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M3 Farmer Qty").Width = 100
        gv.Columns("M3 Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("M3 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M3 Farmer Code").Width = 100
        gv.Columns("M3 Farmer Code").FormatString = "{0:n2}"

        Gv.Columns("M3 Amt").HeaderText = "Amount"
        gv.Columns("M3 Amt").Width = 100
        gv.Columns("M3 Amt").FormatString = "{0:n2}"





        gv.Columns("Month4").HeaderText = "Month 4"
        gv.Columns("Month4").Width = 100
        gv.Columns("Month4").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M4 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M4 No Of Doc").Width = 100
            gv.Columns("M4 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M4 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M4 Billed Qty").Width = 100
        gv.Columns("M4 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M4 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M4 Farmer Qty").Width = 100
        gv.Columns("M4 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M4 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M4 Farmer Code").Width = 100
        gv.Columns("M4 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M4 Amt").HeaderText = "Amount"
        gv.Columns("M4 Amt").Width = 100
        gv.Columns("M4 Amt").FormatString = "{0:n2}"


        gv.Columns("Month5").HeaderText = "Month 5"
        gv.Columns("Month5").Width = 100
        gv.Columns("Month5").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M5 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M5 No Of Doc").Width = 100
            gv.Columns("M5 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M5 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M5 Billed Qty").Width = 100
        gv.Columns("M5 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M5 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M5 Farmer Qty").Width = 100
        gv.Columns("M5 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M5 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M5 Farmer Code").Width = 100
        gv.Columns("M5 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M5 Amt").HeaderText = "Amount"
        gv.Columns("M5 Amt").Width = 100
        gv.Columns("M5 Amt").FormatString = "{0:n2}"


        gv.Columns("Month6").HeaderText = "Month 6"
        gv.Columns("Month6").Width = 100
        gv.Columns("Month6").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M6 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M6 No Of Doc").Width = 100
            gv.Columns("M6 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M6 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M6 Billed Qty").Width = 100
        gv.Columns("M6 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M6 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M6 Farmer Qty").Width = 100
        gv.Columns("M6 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M6 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M6 Farmer Code").Width = 100
        gv.Columns("M6 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M6 Amt").HeaderText = "Amount"
        gv.Columns("M6 Amt").Width = 100
        gv.Columns("M6 Amt").FormatString = "{0:n2}"


        gv.Columns("Month7").HeaderText = "Month 7"
        gv.Columns("Month7").Width = 100
        gv.Columns("Month7").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M7 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M7 No Of Doc").Width = 100
            gv.Columns("M7 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M7 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M7 Billed Qty").Width = 100
        gv.Columns("M7 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M7 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M7 Farmer Qty").Width = 100
        gv.Columns("M7 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M7 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M7 Farmer Code").Width = 100
        gv.Columns("M7 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M7 Amt").HeaderText = "Amount"
        gv.Columns("M7 Amt").Width = 100
        gv.Columns("M7 Amt").FormatString = "{0:n2}"


        gv.Columns("Month8").HeaderText = "Month 8"
        gv.Columns("Month8").Width = 100
        gv.Columns("Month8").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M8 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M8 No Of Doc").Width = 100
            gv.Columns("M8 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M8 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M8 Billed Qty").Width = 100
        gv.Columns("M8 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M8 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M8 Farmer Qty").Width = 100
        gv.Columns("M8 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M8 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M8 Farmer Code").Width = 100
        gv.Columns("M8 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M8 Amt").HeaderText = "Amount"
        gv.Columns("M8 Amt").Width = 100
        gv.Columns("M8 Amt").FormatString = "{0:n2}"

        gv.Columns("Month9").HeaderText = "Month 9"
        gv.Columns("Month9").Width = 100
        gv.Columns("Month9").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M9 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M9 No Of Doc").Width = 100
            gv.Columns("M9 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M9 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M9 Billed Qty").Width = 100
        gv.Columns("M9 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M9 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M9 Farmer Qty").Width = 100
        gv.Columns("M9 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M9 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M9 Farmer Code").Width = 100
        gv.Columns("M9 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M9 Amt").HeaderText = "Amount"
        gv.Columns("M9 Amt").Width = 100
        gv.Columns("M9 Amt").FormatString = "{0:n2}"


        gv.Columns("Month10").HeaderText = "Month 10"
        gv.Columns("Month10").Width = 100
        gv.Columns("Month10").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M10 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M10 No Of Doc").Width = 100
            gv.Columns("M10 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M10 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M10 Billed Qty").Width = 100
        gv.Columns("M10 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M10 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M10 Farmer Qty").Width = 100
        gv.Columns("M10 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M10 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M10 Farmer Code").Width = 100
        gv.Columns("M10 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M10 Amt").HeaderText = "Amount"
        gv.Columns("M10 Amt").Width = 100
        gv.Columns("M10 Amt").FormatString = "{0:n2}"



        gv.Columns("Month11").HeaderText = "Month 11"
        gv.Columns("Month11").Width = 100
        gv.Columns("Month11").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M11 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M11 No Of Doc").Width = 100
            gv.Columns("M11 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M11 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M11 Billed Qty").Width = 100
        gv.Columns("M11 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M11 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M11 Farmer Qty").Width = 100
        gv.Columns("M11 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M11 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M11 Farmer Code").Width = 100
        gv.Columns("M11 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M11 Amt").HeaderText = "Amount"
        gv.Columns("M11 Amt").Width = 100
        gv.Columns("M11 Amt").FormatString = "{0:n2}"



        gv.Columns("Month12").HeaderText = "Month 12"
        gv.Columns("Month12").Width = 100
        gv.Columns("Month12").FormatString = ""

        If chkOnlyReject.Checked Then
            gv.Columns("M12 No Of Doc").HeaderText = "No. Of Document"
            gv.Columns("M12 No Of Doc").Width = 100
            gv.Columns("M12 No Of Doc").FormatString = "{0:n2}"
        End If

        gv.Columns("M12 Billed Qty").HeaderText = "DCS Qty"
        gv.Columns("M12 Billed Qty").Width = 100
        gv.Columns("M12 Billed Qty").FormatString = "{0:n2}"

        gv.Columns("M12 Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("M12 Farmer Qty").Width = 100
        gv.Columns("M12 Farmer Qty").FormatString = "{0:n2}"

        gv.Columns("M12 Farmer Code").HeaderText = "No. Of Farmer"
        gv.Columns("M12 Farmer Code").Width = 100
        gv.Columns("M12 Farmer Code").FormatString = "{0:n2}"

        gv.Columns("M12 Amt").HeaderText = "Amount"
        gv.Columns("M12 Amt").Width = 100
        gv.Columns("M12 Amt").FormatString = "{0:n2}"



        If chkOnlyReject.Checked Then
            Gv.Columns("Total Document").HeaderText = "Total Document"
            gv.Columns("Total Document").Width = 100
            gv.Columns("Total Document").FormatString = "{0:n2}"
        End If

        Gv.Columns("Total Billed Qty").HeaderText = "Billed Qty"
        gv.Columns("Total Billed Qty").Width = 100
        gv.Columns("Total Billed Qty").FormatString = "{0:n2}"

        Gv.Columns("Total Farmer Qty").HeaderText = "Farmer Qty"
        gv.Columns("Total Farmer Qty").Width = 100
        gv.Columns("Total Farmer Qty").FormatString = "{0:n2}"

        Gv.Columns("Total No. Of Farmer").HeaderText = "No. Of Farmer"
        gv.Columns("Total No. Of Farmer").Width = 100
        gv.Columns("Total No. Of Farmer").FormatString = "{0:n2}"

        Gv.Columns("Total Amt").HeaderText = "Amount"
        gv.Columns("Total Amt").Width = 100
        gv.Columns("Total Amt").FormatString = "{0:n2}"

        'Gv.Columns("Dis_SNFKG").HeaderText = "SNFKG"
        'Gv.Columns("Dis_SNFKG").IsVisible = True
        'Gv.Columns("Dis_SNFKG").FormatString = "{0:n2}"

        Gv.ShowGroupPanel = True
        Gv.MasterTemplate.AutoExpandGroups = True
        If rbtnQuarterly.IsChecked Then
            View()
        ElseIf rbtnYearly.IsChecked Then
            Vieww()
            'Gv.Columns("Total Amt").HeaderText = "Total Amount"
            'Gv.Columns("M1 Amt").HeaderText = clsCommon.GetPrintDate(Month1, "MMM-yyyy")
            'Gv.Columns("M2 Amt").HeaderText = clsCommon.GetPrintDate(Month2, "MMM-yyyy")
            'Gv.Columns("M3 Amt").HeaderText = clsCommon.GetPrintDate(Month3, "MMM-yyyy")
            'Gv.Columns("M4 Amt").HeaderText = clsCommon.GetPrintDate(Month4, "MMM-yyyy")
            'Gv.Columns("M5 Amt").HeaderText = clsCommon.GetPrintDate(Month5, "MMM-yyyy")
            'Gv.Columns("M6 Amt").HeaderText = clsCommon.GetPrintDate(Month6, "MMM-yyyy")
            'Gv.Columns("M7 Amt").HeaderText = clsCommon.GetPrintDate(Month7, "MMM-yyyy")
            'Gv.Columns("M8 Amt").HeaderText = clsCommon.GetPrintDate(Month8, "MMM-yyyy")
            'Gv.Columns("M9 Amt").HeaderText = clsCommon.GetPrintDate(Month9, "MMM-yyyy")
            'Gv.Columns("M10 Amt").HeaderText = clsCommon.GetPrintDate(Month10, "MMM-yyyy")
            'Gv.Columns("M11 Amt").HeaderText = clsCommon.GetPrintDate(Month11, "MMM-yyyy")
            'Gv.Columns("M12 Amt").HeaderText = clsCommon.GetPrintDate(Month12, "MMM-yyyy")
        End If
        SummaryRow()
    End Sub

    Sub View()

        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup("Union"))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SNo").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Union Name").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month1, "MMM-yyyy")))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 No Of Doc").Name)
            End If
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Billed Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Farmer Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Farmer Code").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Amt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month2, "MMM-yyyy")))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 No Of Doc").Name)
            End If
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Billed Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Farmer Qty").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Farmer Code").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Amt").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month3, "MMM-yyyy")))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 No Of Doc").Name)
            End If
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Billed Qty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Farmer Qty").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Farmer Code").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Amt").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
            view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())

            If chkOnlyReject.Checked Then
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Total Document").Name)
            End If
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Total Billed Qty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Total Farmer Qty").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Total No. Of Farmer").Name)
            view.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("Total Amt").Name)
            gv.ViewDefinition = view
        End If
    End Sub





    Sub Vieww()

        If gv.Rows.Count > 0 Then
            Dim vieww As New ColumnGroupsViewDefinition()

            vieww.ColumnGroups.Add(New GridViewColumnGroup("Union"))
            vieww.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            vieww.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("SNo").Name)
            vieww.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Union Name").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month1, "MMM-yyyy")))
            vieww.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 No Of Doc").Name)
            End If
            vieww.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Billed Qty").Name)
            vieww.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Farmer Qty").Name)
            vieww.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Farmer Code").Name)
            vieww.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("M1 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month2, "MMM-yyyy")))
            vieww.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 No Of Doc").Name)
            End If
            vieww.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Billed Qty").Name)
            vieww.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Farmer Qty").Name)
            vieww.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Farmer Code").Name)
            vieww.ColumnGroups(2).Rows(0).ColumnNames.Add(gv.Columns("M2 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month3, "MMM-yyyy")))
            vieww.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 No Of Doc").Name)
            End If
            vieww.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Billed Qty").Name)
            vieww.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Farmer Qty").Name)
            vieww.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Farmer Code").Name)
            vieww.ColumnGroups(3).Rows(0).ColumnNames.Add(gv.Columns("M3 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month4, "MMM-yyyy")))
            vieww.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("M4 No Of Doc").Name)
            End If
            vieww.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("M4 Billed Qty").Name)
            vieww.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("M4 Farmer Qty").Name)
            vieww.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("M4 Farmer Code").Name)
            vieww.ColumnGroups(4).Rows(0).ColumnNames.Add(gv.Columns("M4 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month5, "MMM-yyyy")))
            vieww.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("M1 No Of Doc").Name)
            End If
            vieww.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("M5 Billed Qty").Name)
            vieww.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("M5 Farmer Qty").Name)
            vieww.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("M5 Farmer Code").Name)
            vieww.ColumnGroups(5).Rows(0).ColumnNames.Add(gv.Columns("M5 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month6, "MMM-yyyy")))
            vieww.ColumnGroups(6).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("M6 No Of Doc").Name)
            End If
            vieww.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("M6 Billed Qty").Name)
            vieww.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("M6 Farmer Qty").Name)
            vieww.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("M6 Farmer Code").Name)
            vieww.ColumnGroups(6).Rows(0).ColumnNames.Add(gv.Columns("M6 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month7, "MMM-yyyy")))
            vieww.ColumnGroups(7).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(7).Rows(0).ColumnNames.Add(gv.Columns("M7 No Of Doc").Name)
            End If
            vieww.ColumnGroups(7).Rows(0).ColumnNames.Add(gv.Columns("M7 Billed Qty").Name)
            vieww.ColumnGroups(7).Rows(0).ColumnNames.Add(gv.Columns("M7 Farmer Qty").Name)
            vieww.ColumnGroups(7).Rows(0).ColumnNames.Add(gv.Columns("M7 Farmer Code").Name)
            vieww.ColumnGroups(7).Rows(0).ColumnNames.Add(gv.Columns("M7 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month8, "MMM-yyyy")))
            vieww.ColumnGroups(8).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(8).Rows(0).ColumnNames.Add(gv.Columns("M8 No Of Doc").Name)
            End If
            vieww.ColumnGroups(8).Rows(0).ColumnNames.Add(gv.Columns("M8 Billed Qty").Name)
            vieww.ColumnGroups(8).Rows(0).ColumnNames.Add(gv.Columns("M8 Farmer Qty").Name)
            vieww.ColumnGroups(8).Rows(0).ColumnNames.Add(gv.Columns("M8 Farmer Code").Name)
            vieww.ColumnGroups(8).Rows(0).ColumnNames.Add(gv.Columns("M8 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month9, "MMM-yyyy")))
            vieww.ColumnGroups(9).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(9).Rows(0).ColumnNames.Add(gv.Columns("M9 No Of Doc").Name)
            End If
            vieww.ColumnGroups(9).Rows(0).ColumnNames.Add(gv.Columns("M9 Billed Qty").Name)
            vieww.ColumnGroups(9).Rows(0).ColumnNames.Add(gv.Columns("M9 Farmer Qty").Name)
            vieww.ColumnGroups(9).Rows(0).ColumnNames.Add(gv.Columns("M9 Farmer Code").Name)
            vieww.ColumnGroups(9).Rows(0).ColumnNames.Add(gv.Columns("M9 Amt").Name)


            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month10, "MMM-yyyy")))
            vieww.ColumnGroups(10).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(10).Rows(0).ColumnNames.Add(gv.Columns("M10 No Of Doc").Name)
            End If
            vieww.ColumnGroups(10).Rows(0).ColumnNames.Add(gv.Columns("M10 Billed Qty").Name)
            vieww.ColumnGroups(10).Rows(0).ColumnNames.Add(gv.Columns("M10 Farmer Qty").Name)
            vieww.ColumnGroups(10).Rows(0).ColumnNames.Add(gv.Columns("M10 Farmer Code").Name)
            vieww.ColumnGroups(10).Rows(0).ColumnNames.Add(gv.Columns("M10 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month11, "MMM-yyyy")))
            vieww.ColumnGroups(11).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(11).Rows(0).ColumnNames.Add(gv.Columns("M11 No Of Doc").Name)
            End If
            vieww.ColumnGroups(11).Rows(0).ColumnNames.Add(gv.Columns("M11 Billed Qty").Name)
            vieww.ColumnGroups(11).Rows(0).ColumnNames.Add(gv.Columns("M11 Farmer Qty").Name)
            vieww.ColumnGroups(11).Rows(0).ColumnNames.Add(gv.Columns("M11 Farmer Code").Name)
            vieww.ColumnGroups(11).Rows(0).ColumnNames.Add(gv.Columns("M11 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.GetPrintDate(Month12, "MMM-yyyy")))
            vieww.ColumnGroups(12).Rows.Add(New GridViewColumnGroupRow())
            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(12).Rows(0).ColumnNames.Add(gv.Columns("M12 No Of Doc").Name)
            End If
            vieww.ColumnGroups(12).Rows(0).ColumnNames.Add(gv.Columns("M12 Billed Qty").Name)
            vieww.ColumnGroups(12).Rows(0).ColumnNames.Add(gv.Columns("M12 Farmer Qty").Name)
            vieww.ColumnGroups(12).Rows(0).ColumnNames.Add(gv.Columns("M12 Farmer Code").Name)
            vieww.ColumnGroups(12).Rows(0).ColumnNames.Add(gv.Columns("M12 Amt").Name)

            vieww.ColumnGroups.Add(New GridViewColumnGroup("Total"))
            vieww.ColumnGroups(13).Rows.Add(New GridViewColumnGroupRow())

            If chkOnlyReject.Checked Then
                vieww.ColumnGroups(13).Rows(0).ColumnNames.Add(gv.Columns("Total Document").Name)
            End If
            vieww.ColumnGroups(13).Rows(0).ColumnNames.Add(gv.Columns("Total Billed Qty").Name)
            vieww.ColumnGroups(13).Rows(0).ColumnNames.Add(gv.Columns("Total Farmer Qty").Name)
            vieww.ColumnGroups(13).Rows(0).ColumnNames.Add(gv.Columns("Total No. Of Farmer").Name)
            vieww.ColumnGroups(13).Rows(0).ColumnNames.Add(gv.Columns("Total Amt").Name)
            gv.ViewDefinition = vieww
        End If
    End Sub








    Sub SummaryRow()
        If Gv.Rows.Count > 0 Then
            Dim summaryRowItem As New GridViewSummaryRowItem()
            For i As Integer = 3 To Gv.Columns.Count - 1
                summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(i).Name, "{0:n2}", GridAggregateFunction.Sum))
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
    Public Function ReturnSelectTypeYEAR() As DataTable

        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = "YEARLY AMOUNT"
        dr("Name") = "YEARLY AMOUNT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "YEARLY Print"
        dr("Name") = "YEARLY Print"
        dt.Rows.Add(dr)

        Return dt

    End Function
    Public Function ReturnSelectTypeQuarterly() As DataTable

        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = "Farmer AND Amount Print"
        dr("Name") = "Farmer AND Amount Print"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Quarterly Print"
        dr("Name") = "Quarterly Print"
        dt.Rows.Add(dr)

        Return dt

    End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            Dim query = ReportQry()
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(query)
            Dim frmCRV As New frmCrystalReportViewer()

            Dim frmNew As New XpertERPEngine.FrmFreeComboBox()
            If rbtnYearly.IsChecked Then
                frmNew.ComboSource = ReturnSelectTypeYEAR()
                frmNew.ComboValueMember = "Code"
                frmNew.ComboDisplayMember = "Name"
                frmNew.ShowDialog()
                If clsCommon.CompairString(frmNew.strRetValue, "YEARLY AMOUNT") = CompairStringResult.Equal Then
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptDBT_NEFTUnionReportYearlyNEW", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                Else
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptDBT_NEFTUnionReportYearly", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                End If
                frmCRV = Nothing
            ElseIf rbtnQuarterly.IsChecked Then
                frmNew.ComboSource = ReturnSelectTypeQuarterly()
                frmNew.ComboValueMember = "Code"
                frmNew.ComboDisplayMember = "Name"
                frmNew.ShowDialog()
                If clsCommon.CompairString(frmNew.strRetValue, "Farmer AND Amount Print") = CompairStringResult.Equal Then
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptDBT_NEFTUnionReportNEW", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                Else
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptDBT_NEFTUnionReport", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                End If
                frmCRV = Nothing
            End If

            If (dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0) Then
                If chkOnlyReject.Checked Then
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptDBT_NEFT_RejectUnionReport", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                    'Else
                    '    If rbtnQuarterly.IsChecked Then
                    '        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptDBT_NEFTUnionReport", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                    '    ElseIf rbtnYearly.IsChecked Then
                    '        frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.CommonForUnionAndCattlefeed, dt2, "crptDBT_NEFTUnionReportYearly", "Union Report", Nothing) ''report for both (RCDF And RCDFCF)
                    '    End If
                End If
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to print", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmDBTNEFTUnionReport & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

            transportSql.applyExportTemplate(Gv, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                If rbtnYearly.IsChecked Then
                    clsCommon.MyExportToExcel(Me.Text, Gv, arrHeader, Me.Text)
                ElseIf rbtnQuarterly.IsChecked Then
                    transportSql.exportdata(Gv, "", Me.Text, False, arrHeader, False, False, True)
                End If
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub rbtnYearly_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnYearly.ToggleStateChanged, rbtnQuarterly.ToggleStateChanged
        If rbtnYearly.IsChecked Then
            txtToDate.Value = txtFromDate.Value.AddMonths(11)
            chkOnlyReject.Visible = False
        ElseIf rbtnQuarterly.IsChecked Then
            txtToDate.Value = txtFromDate.Value.AddMonths(2)
            chkOnlyReject.Visible = True
        End If
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
    End Sub
End Class