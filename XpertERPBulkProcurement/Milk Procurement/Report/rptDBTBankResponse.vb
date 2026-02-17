
Imports System.IO
Imports common
Imports System.Text
Imports common.UserControls
Public Class rptDBTBankResponse
    Inherits FrmMainTranScreen
    Private Sub rptDBTBankResponse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        If objCommonVar.RCDFCFP Then
            txtDoc.Visible = False
            txtDcsCode.Visible = False
            txtFarmerCode.Visible = False
            lblDocumnetcode.Visible = False
            lblFarmerCode.Visible = False
            lblDcsCode.Visible = False
        Else
            txtDoc.Visible = True
            txtDcsCode.Visible = True
            txtFarmerCode.Visible = True
            lblDocumnetcode.Visible = True
            lblFarmerCode.Visible = True
            lblDcsCode.Visible = True
        End If


        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE().AddMonths(-1)
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
                qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrComp_Code1 & "' ORDER BY [TSPL_APP_LOCATION].Location_Name"

            End If
            'qry = "SELECT [TSPL_APP_LOCATION].Location_Name as Location,[TSPL_APP_LOCATION].DataBase_Name as [DataBase Name] FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE Union_Report=1 ORDER BY [TSPL_APP_LOCATION].Location_Name"

            txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTUnionPay", qry, "DataBase Name", "", txtUnion.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        'Try
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        '    If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
        '        common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
        '        Exit Sub
        '    End If
        '    Dim qry As String = ""
        '    qry = "SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name"

        '    txtUnion.arrValueMember = clsCommon.ShowMultipleSelectForm("DBTPaymentDetail", qry, "DataBase_Name", "Location_Name", txtUnion.arrValueMember, txtUnion.arrDispalyMember)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        'End Try
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
                If objCommonVar.RCDFCFP Then
                    uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 order by [TSPL_APP_LOCATION].Location_Name "
                Else
                    uQry = " select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                            from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE 2=2 AND [TSPL_APP_LOCATION].DataBase_Name='" & objCommonVar.CurrComp_Code1 & "' order by [TSPL_APP_LOCATION].Location_Name "
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

                '                baseqry += "  Select '" + clsCommon.myCstr(dtunion.Rows(ii).Item("Location_Name")) + "' AS [UnionName], MP_Code as ID, FORMAT([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date, 'MMM/yyyy') AS MonthYear,DATENAME(MONTH, [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date) AS MonthName,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date as IDate,ROW_NUMBER() OVER (PARTITION BY MP_Code ORDER BY From_Date) AS RN
                ' from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL
                'left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
                'where [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'
                'and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_HEAD.From_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
                baseqry += "  select  '" + clsCommon.myCstr(dtunion.Rows(ii).Item("Location_Name")) + "' AS [UnionName],
  CASE  WHEN [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.status = 0 THEN '0'  else '1' END status,
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code) AS [Farmer Code] ,([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Name) AS [Farmer Name],([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_IFSC_No) as[Farmer IFSC Code],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Account_No) AS [Farmer Bank Account],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Bank) AS [Farmer Bank],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Mobile_No as[Farmer Mobile No],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Amount as [Amount],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.Vsp_code) as [DCS CODE],
([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as [DCS Uploader Code],
  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.mp_code as [MP CODE]
,
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Document_Code as [Document Code],
CONVERT(VARCHAR,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,103) as [From Date],
CONVERT(Varchar,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date,103) as[to Date]
, 
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response,
case when  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response = 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA' then 1 else 0  end as Success_Farmer,case when 
  ([" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response  <> 'STATUS : SUCCESS , STATUS DESCRIPTION : VALID DATA') and (TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response IS NOT NULL) then 1  else 0 end as Failure_Farmer
  ,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id ,
  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Against_MP_Incentive_TR ,
  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.sno ,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Lot_No,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.UKID 
  ,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.DBT_Revise_Payment,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Sanction_Number,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Sanction_Date,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Sanction_Amount"
                If rbtnJA.IsChecked Then
                    baseqry += " ,CASE 
        WHEN [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.JA_Is_Saved = 'Y' THEN 'Push to JA Server'
        ELSE 'Pending to JA Server'
    END AS [Send to Janaadhar]"
                End If
                baseqry += " from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl 
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_detail on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_detail.PK_Id=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.Against_MP_Incentive_TR
left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_head on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_head.Document_Code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_detail.Document_Code

left outer join  [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_MASTER.mp_code=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_MP_INCENTIVE_ENTRY_DETAIL.mp_code

  left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.VLC_Uploader_Code


 left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Ref_PK_Id=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.PK_Id
WHERE  Convert(Date,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date,103)>=convert(date,'" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "',103) And 
    Convert(Date,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.To_Date,103)<=convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)
                AND [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response IS NOT NULL 

AND [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response LIKE '%STATUS : %'"
                If txtDoc.arrValueMember IsNot Nothing AndAlso txtDoc.arrValueMember.Count > 0 Then
                    baseqry += " and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.Document_Code in (" + clsCommon.GetMulcallString(txtDoc.arrValueMember) + ") "
                End If
                If txtDcsCode.arrValueMember IsNot Nothing AndAlso txtDcsCode.arrValueMember.Count > 0 Then
                    baseqry += " and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader in (" + clsCommon.GetMulcallString(txtDcsCode.arrValueMember) + ") "
                End If
                If txtFarmerCode.arrValueMember IsNot Nothing AndAlso txtFarmerCode.arrValueMember.Count > 0 Then
                    baseqry += " and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.MP_Uploader_Code in (" + clsCommon.GetMulcallString(txtFarmerCode.arrValueMember) + ") "
                End If
                'If txtStatus.arrValueMember IsNot Nothing AndAlso txtStatus.arrValueMember.Count > 0 Then
                '    baseqry += " and [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.status in (" + clsCommon.GetMulcallString(txtStatus.arrValueMember) + ") "
                'End If
                'If txtStatus.arrValueMember IsNot Nothing AndAlso txtStatus.arrValueMember.Count > 0 Then

                '    Dim statusText As String = clsCommon.myCstr(txtStatus.arrValueMember(0))

                '    If statusText = "APPROVED" Then
                '        baseqry += " AND [" & clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) &
                '                   "].[dbo].TSPL_DBT_NEFT.status = 1"
                '    ElseIf statusText = "PENDING" Then
                '        baseqry += " AND [" & clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) &
                '                   "].[dbo].TSPL_DBT_NEFT.status = 0"
                '    End If

                'End If
                If rbtnBankResponse.IsChecked Then
                    If rbtnSuccess.IsChecked Then
                        baseqry += " and  (CASE WHEN TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response LIKE '%STATUS : SUCCESS%' THEN 1 ELSE 0 END) = 1 "
                    ElseIf rbtnFailed.IsChecked Then
                        baseqry += " and (CASE WHEN TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response LIKE '%STATUS : SUCCESS%' THEN 1 ELSE 0 END) = 0 "
                    Else
                        baseqry += " and (CASE WHEN TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response LIKE '%STATUS : SUCCESS%' THEN 1 ELSE 0 END) in('1','0') "
                    End If
                End If
                If rbtnJA.IsChecked Then
                    If rbtnSuccess.IsChecked Then
                        baseqry += " and  (CASE WHEN TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response LIKE '%STATUS : SUCCESS%' THEN 1 ELSE 0 END) = 1 AND  TSPL_DBT_NEFT_BANK_RESPONSE.JA_Is_Saved ='y'"
                    ElseIf rbtnFailed.IsChecked Then
                        baseqry += " and (CASE WHEN TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response LIKE '%STATUS : SUCCESS%' THEN 1 ELSE 0 END) = 0 AND   TSPL_DBT_NEFT_BANK_RESPONSE.JA_Is_Saved ='N'"
                    Else
                        baseqry += " and (CASE WHEN TSPL_DBT_NEFT_BANK_RESPONSE.Bank_Response LIKE '%STATUS : SUCCESS%' THEN 1 ELSE 0 END) in('1','0') AND   TSPL_DBT_NEFT_BANK_RESPONSE.JA_Is_Saved IN ('Y','N')"
                    End If
                End If

            Next
            Dim SummaryQry As String = ""
            If rbtnCycleWiseSummary.IsChecked Then
                SummaryQry += "select max(Unionname)Unionname,max(status)status,count([Farmer Code])[Farmer Code],sum(Amount)Amount,Count([DCS CODE])[DCS CODE],COUNT( [DCS Uploader Code])[DCS Uploader Code],COUNT([MP CODE])[MP CODE],[Document Code],[From Date],[to Date],count(Bank_Response)Bank_Response,COUNT(UKID)UKID "
                If rbtnJA.IsChecked Then
                    SummaryQry += " ,MAX([Send to Janaadhar])[Send to Janaadhar]"
                End If
                SummaryQry += " From ( " & baseqry & ") XX GROUP BY XX.[Document Code] ,XX.[From Date],XX.[to Date]"
            End If

            'qry1 += "WITH CTE AS(" + baseqry + " )SELECT (UnionName)UnionName,  MonthYear,MonthName, COUNT(DISTINCT ID) As TotalID, COUNT(Case When RN = 1 Then 1 End) As NewID FROM CTE
            '              GROUP BY MonthYear,MonthName,UnionName
            '   ORDER BY UnionName,MIN(IDate)"

            Dim dt2 As DataTable
            If rbtnCycleWiseSummary.IsChecked Then
                dt2 = clsDBFuncationality.GetDataTable(SummaryQry)
            Else
                dt2 = clsDBFuncationality.GetDataTable(baseqry)
            End If
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
                If rbtnDetail.IsChecked Then
                    SetGridFormat()

                End If

                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
            ' End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)

        End Try
    End Sub

    Sub SetGridFormat()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            gv1.Columns("Success_Farmer").IsVisible = False
            gv1.Columns("Failure_Farmer").IsVisible = False
            gv1.Columns("status").IsVisible = False
            gv1.Columns("status").IsVisible = False
            gv1.Columns("Ref_PK_Id").IsVisible = False
            gv1.Columns("Ref_PK_Id").HeaderText = "Ref PK Id"
            gv1.Columns("Against_MP_Incentive_TR").IsVisible = False
            gv1.Columns("Against_MP_Incentive_TR").HeaderText = "Against MP Incentive TR"
            gv1.Columns("sno").IsVisible = False
            gv1.Columns("sno").HeaderText = "sno"
            gv1.Columns("Lot_No").IsVisible = False
            gv1.Columns("Lot_No").HeaderText = "Lot_No"
            gv1.Columns("UKID").IsVisible = False
            gv1.Columns("UKID").HeaderText = "UKID"
            gv1.Columns("DBT_Revise_Payment").IsVisible = False
            gv1.Columns("DBT_Revise_Payment").HeaderText = "DBT Revise Payment"
            gv1.Columns("Sanction_Number").IsVisible = False
            gv1.Columns("Sanction_Number").HeaderText = "Sanction_Number"
            gv1.Columns("Sanction_Date").IsVisible = False
            gv1.Columns("Sanction_Date").HeaderText = "Sanction_Date"
            gv1.Columns("Sanction_Amount").IsVisible = False
            gv1.Columns("Sanction_Amount").HeaderText = "Sanction Amount"
            gv1.Columns("Bank_Response").HeaderText = "Bank Response"

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

    Sub Reset()
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            RadPageView1.SelectedPage = RadPageViewPage1
            txtFarmerCode.arrValueMember = Nothing
            txtDcsCode.arrValueMember = Nothing
            txtDoc.arrValueMember = Nothing
            txtStatus.arrValueMember = Nothing

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
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDBTBankResponse & "'"))
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Private Sub btnPDF_Click(sender As Object, e As EventArgs)
    '    Try
    '        If gv1.Rows.Count > 0 Then
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            'arrHeader.Add("Month :" & MonthNo)
    '            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtDoc__My_Click(sender As Object, e As EventArgs) Handles txtDoc._My_Click
        Try
            If chkDataBase() Then
                Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)
                Dim dtunion As DataTable
                Dim qry As String = ""
                Dim qry1 As String = "select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
                dtunion = clsDBFuncationality.GetDataTable(qry1)
                For ii As Integer = 0 To dtunion.Rows.Count - 1
                    If ii > 0 Then
                        Qry += "union all"
                    End If


                    qry = "SELECT [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_Code AS [Document Code],[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.Document_DatE AS [Documnet Date]

,[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.From_Date AS [From Date],
[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT.to_date AS [To Date]
FROM [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT"
                    txtDoc.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", Qry, "Document Code", "Document Code", txtDoc.arrValueMember, txtDoc.arrDispalyMember)
                Next
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtDcsCode__My_Click(sender As Object, e As EventArgs) Handles txtDcsCode._My_Click
        Try
            If chkDataBase() Then
                Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)
                Dim DD As String = clsCommon.GetMulcallString(txtDoc.arrValueMember)
                Dim dtunion As DataTable
                Dim qry As String = ""
                Dim qry1 As String = "select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
                dtunion = clsDBFuncationality.GetDataTable(qry1)
                For ii As Integer = 0 To dtunion.Rows.Count - 1
                    If ii > 0 Then
                        Qry += "union all"
                    End If

                    qry = "select distinct TSPL_VLC_MASTER_HEAD.Vsp_code as [Dcs Code],TSPL_VLC_MASTER_HEAD.VLC_Name as[Dcs Name], VLC_Uploader_Code AS [DCS Uploader Code] from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl
                           left outer join [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD on [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=[" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIl.VLC_Uploader_Code
                           where TSPL_DBT_NEFT_DETAIl.Document_Code in (" + DD + ") 
                           AND TSPL_VLC_MASTER_HEAD.Vsp_code  IS NOT NULL "
                    'qry = "select Vsp_code as [Dcs Code],VLC_Code_VLC_Uploader as [Dcs Uploadr Code],VLC_Name as[Dcs Name] from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_VLC_MASTER_HEAD"
                    txtDcsCode.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", qry, "DCS Uploader Code", "Dcs Uploader Code", txtDcsCode.arrValueMember, txtDcsCode.arrDispalyMember)
                Next
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtFarmerCode__My_Click(sender As Object, e As EventArgs) Handles txtFarmerCode._My_Click
        Try
            If chkDataBase() Then
                Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)
                Dim VLC As String = clsCommon.GetMulcallString(txtDcsCode.arrValueMember)
                Dim doc As String = clsCommon.GetMulcallString(txtDoc.arrValueMember)

                Dim dtunion As DataTable
                Dim qry As String = ""
                Dim qry1 As String = "select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
                dtunion = clsDBFuncationality.GetDataTable(qry1)
                For ii As Integer = 0 To dtunion.Rows.Count - 1
                    If ii > 0 Then
                        Qry += "union all"
                    End If
                    qry = "select distinct MP_Uploader_Code as [Farmer Code],MP_Name as [Farmer Name] from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL
                    where [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL.VLC_Uploader_Code in (" + VLC + ") "
                    '                   qry = "select distinct MP_Uploader_Code as [Farmer Code],MP_Name as [Farmer Name] from [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT_DETAIL
                    'where TSPL_DBT_NEFT_DETAIl.VLC_Uploader_Code in (" + VLC + ") "
                    txtFarmerCode.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", Qry, "Farmer Code", "Farmer Code", txtFarmerCode.arrValueMember, txtFarmerCode.arrDispalyMember)
                Next
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtStatus__My_Click(sender As Object, e As EventArgs) Handles txtStatus._My_Click
        Try
            If chkDataBase() Then
                Dim ss As String = clsCommon.GetMulcallString(txtUnion.arrValueMember)
                Dim dtunion As DataTable
                Dim qry As String = ""
                Dim qry1 As String = "select  [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name
                        from TSPL_MASTER.dbo.TSPL_APP_LOCATION WHERE [TSPL_APP_LOCATION].DataBase_Name  in (" + ss + ") 
                        order by [TSPL_APP_LOCATION].Location_Name "
                dtunion = clsDBFuncationality.GetDataTable(qry1)
                For ii As Integer = 0 To dtunion.Rows.Count - 1
                    If ii > 0 Then
                        qry += "union all"
                    End If
                    qry = "SELECT distinct CASE  WHEN status = 0 THEN 'Pending' else 'APPROVED' END AS Status FROM [" + clsCommon.myCstr(dtunion.Rows(ii).Item("DataBase_Name")) + "].[dbo].TSPL_DBT_NEFT
where   TSPL_DBT_NEFT.status IS NOT NULL "
                    txtStatus.arrValueMember = clsCommon.ShowMultipleSelectForm("@Union", qry, "Status", "Status", txtStatus.arrValueMember, txtStatus.arrDispalyMember)
                Next
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try

    End Sub
End Class