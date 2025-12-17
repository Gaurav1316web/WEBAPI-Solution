
Imports common
Imports System.Text
Imports common.UserControls
Public Class rptDBTBankResponse
    Inherits FrmMainTranScreen
    Private Sub rptDBTBankResponse_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtUnion__My_Click(sender As Object, e As EventArgs) Handles txtUnion._My_Click
        Try
            If chkDataBase() Then
                Dim Qry As String = "SELECT [TSPL_APP_LOCATION].DataBase_Name As [Code], [TSPL_APP_LOCATION].Location_Name As [Union Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"
                txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", Qry, "Code", "Union Name", txtUnion.arrValueMember, txtUnion.arrDispalyMember)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
                gv1.DataSource = Nothing
                Exit Sub
            End If
            Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)

            If txtUnion.arrValueMember Is Nothing Then
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
                End If

                '                baseqry += "  Select '" + clsCommon.myCstr(dtunion.Rows(ii).Item("Location_Name")) + "' AS [UnionName], MP_Code as ID, FORMAT([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date, 'MMM/yyyy') AS MonthYear,DATENAME(MONTH, [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date) AS MonthName,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date as IDate,ROW_NUMBER() OVER (PARTITION BY MP_Code ORDER BY From_Date) AS RN
                ' from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL
                'left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                'where [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
                'and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                baseqry += "  select  '" + clsCommon.myCstr(dtunion.Rows(ii).Item("Location_Name")) + "' AS [UnionName],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code) AS [Farmer Code] ,([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Name) AS [Farmer Name],([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_IFSC_No) as[Farmer IFSC Code],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Account_No) AS [Farmer Bank Account],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Bank) AS [Farmer Bank],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Mobile_No as[Farmer Mobile No],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Amount as [Amount],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.Vsp_code) as [DCS CODE],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Document_Code as [Document Code],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date as [From Date],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date as[to Date]
, 
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response,
case when  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response = 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA' then 1 else 0  end as Success_Farmer,case when 
  ([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response  <> 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA') and (TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response IS NOT NULL) then 1  else 0 end as Failure_Farmer
  ,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id as[Ref PK Id],
  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Against_MP_Incentive_TR AS[Against MP Incentive TR],
  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.sno as [SNO],[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Lot_No AS[LOT NO],[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.UKID AS [UKID]
  ,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.DBT_Revise_Payment AS [DBT Revise Payment],[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Sanction_Number AS [Sanction Number],[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Sanction_Date AS [Sanction Date],[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Sanction_Amount AS [Sanction Amount]
 from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl 
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_detail on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_head on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code
 left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_detail.vlc_code= [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code 
 left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.PK_Id


"


            Next

            'qry1 += "WITH CTE AS(" + baseqry + " )SELECT (UnionName)UnionName,  MonthYear,MonthName, COUNT(DISTINCT ID) AS TotalID, COUNT(CASE WHEN RN = 1 THEN 1 END) AS NewID FROM CTE
            '              GROUP BY MonthYear,MonthName,UnionName
            '   ORDER BY UnionName,MIN(IDate)"
            baseqry = "" + qry1 + ""

            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(baseqry)
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
                'SetGridFormat()

                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
            ' End If
        Catch ex As Exception
        End Try
    End Sub

    Sub Reset()
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            RadPageView1.SelectedPage = RadPageViewPage1


            txtUnion.arrValueMember = Nothing
            txtFromDate.Enabled = True
            txtToDate.Enabled = True
            txtFromDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
            txtToDate.Value = clsCommon.GETSERVERDATE()
            'EnableDisableFields(True)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDBTMonthWiseFarmerDetail & "'"))
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                'arrHeader.Add("Month :" & MonthNo)
                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class