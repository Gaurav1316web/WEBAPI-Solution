Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
'Imports Outlook = Microsoft.Office.Interop.Outlook
Imports System.Text.RegularExpressions

Public Class frmDBTApprovalStatus
    Inherits FrmMainTranScreen
    Dim Slot1 As DateTime = Nothing
    Dim Slot2 As DateTime = Nothing
    Dim Month1 As String = Nothing
    Dim Month2 As String = Nothing
    Dim Month3 As String = Nothing
    Private Sub frmDBTApprovalStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            Reset()
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
    Sub Month()
        If clsCommon.myLen(txtFromDate.Value) > 0 Then
            Dim SM As Integer = txtFromDate.Value.Month
            Dim SY As Integer = txtFromDate.Value.Year

            Dim CD As New DateTime(SY, SM, 1)
            Slot2 = clsCommon.GetPrintDate(CD.AddMonths(1).AddDays(-1), "dd/MMM/yyyy")

            ''txtTODate.Value = txtFromDate.Value.AddMonths(2)
            Month1 = clsCommon.GetPrintDate(txtFromDate.Value, "MM-yyyy")
            Month2 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(1), "MM-yyyy")
            Month3 = clsCommon.GetPrintDate(txtFromDate.Value.AddMonths(2), "MM-yyyy")

        End If
    End Sub
    Private Sub fndUnion__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndUnion._MYValidating
        Try
            Dim Sqlqry As String = "select DISTINCT DB_Name as [Code] from TSPL_DBT_NEFT_RCDF "
            fndUnion.Value = clsCommon.ShowSelectForm("DbCode", Sqlqry, "Code", "", fndUnion.Value, "Code", isButtonClicked)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Private Function ReportQry()
        Dim query As String = Nothing
        Dim BaseQry As String = Nothing
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("SELECT name FROM master.dbo.sysdatabases  WHERE name = 'TSPL_MASTER'")
        If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "Database[TSPL_MASTER] not found")
            Exit Function
        End If
        BaseQry = ""
        dt = clsMilkUnion.UnionDBName()
        If rbtnSummary.IsChecked Then
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For ii As Integer = 0 To dt.Rows.Count - 1
                    BaseQry &= "Select DB_Name as [Union Name], Document_Code As [Document Code], Convert(varchar,TSPL_DBT_NEFT_RCDF.From_Date,103) AS [From Date], Convert(varchar,TSPL_DBT_NEFT_RCDF.To_Date,103) AS [TO Date],Created_By
                            AS [Created By], Convert(varchar,TSPL_DBT_NEFT_RCDF.Created_Date,103) AS [Created Date], Post_By as [Approved By], Convert(varchar,TSPL_DBT_NEFT_RCDF.Post_Date,103) as [Approved Date & Time], "
                    BaseQry &= "CASE WHEN ISNULL(Status,0) = 0 THEN 'Pending' ELSE 'Approved' END AS Status "
                    BaseQry &= "FROM TSPL_DBT_NEFT_RCDF WHERE DB_Name ='" & clsCommon.myCstr(fndUnion.Value) & "' "
                    BaseQry &= "AND CONVERT(date, From_Date, 103) >= CONVERT(date,'" & Slot1 & "', 103) "
                    BaseQry &= "AND CONVERT(date, To_Date, 103) <= CONVERT(date,'" & Slot2 & "', 103) "
                    ' Check if rbtnTransactionPosted is checked
                    If rbtnTransactionPending.Checked Then
                        ' Show only data with status=0
                        BaseQry &= "and IsNull(Status,0)=0 "
                    End If
                Next
            End If
        ElseIf rbtnDetail.IsChecked Then
            BaseQry = " select  TSPL_DBT_NEFT_RCDF.DB_Name as [Union Name],TSPL_DBT_NEFT_RCDF.Document_Code As [Document No],CASE WHEN ISNULL(TSPL_DBT_NEFT_RCDF.Status,0) = 0 THEN 'Pending' ELSE 'Approved' END AS Status,TSPL_DBT_NEFT_RCDF.Post_By as [Posted By],TSPL_DBT_NEFT_RCDF.Post_Date as [Posted Date], [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_DBT_NEFT.Created_By as [Created By],convert(date,[" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_DBT_NEFT.Created_Date,103) as [Created Date]"
            BaseQry += " ,(select [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER.User_Name  from [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN LEFT OUTER JOIN [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER ON [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER.User_Code = [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN.User_Code where Module_Code = 'MMMProc' and TRANS_Code = 'DBT-NEFT-UPL' and No_Of_Level = 1 ) as [Level-1 Approved By]"
            BaseQry += " ,(SELECT  [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN.Modified_Date  FROM [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN  WHERE Module_Code = 'MMMProc' AND TRANS_Code = 'DBT-NEFT-UPL' AND No_Of_Level = 1 ) AS [Level-1 Approved Date]"
            BaseQry += " ,(select [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER.User_Name  from [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN LEFT OUTER JOIN [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER ON [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER.User_Code = [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN.User_Code where Module_Code = 'MMMProc' and TRANS_Code = 'DBT-NEFT-UPL' and No_Of_Level = 2 ) as [Level-2 Approved By]"
            BaseQry += " ,(SELECT  [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN.Modified_Date  FROM [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN  WHERE Module_Code = 'MMMProc' AND TRANS_Code = 'DBT-NEFT-UPL' AND No_Of_Level = 2 ) AS [Level-2 Approved Date]"
            BaseQry += " ,(select [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER.User_Name  from [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN LEFT OUTER JOIN [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER ON [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_USER_MASTER.User_Code = [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN.User_Code where Module_Code = 'MMMProc' and TRANS_Code = 'DBT-NEFT-UPL' and No_Of_Level = 3 ) as [Level-3 Approved By]"
            BaseQry += " ,(SELECT  [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN.Modified_Date  FROM [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_APPROVAL_LEVEL_SCREEN  WHERE Module_Code = 'MMMProc' AND TRANS_Code = 'DBT-NEFT-UPL' AND No_Of_Level = 3 ) AS [Level-3 Approved Date]"
            BaseQry += " from [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_DBT_NEFT left outer join RCDF.[dbo].TSPL_DBT_NEFT_RCDF on [" & clsCommon.myCstr(fndUnion.Value) & "].[dbo].TSPL_DBT_NEFT.Document_Code = RCDF.[dbo].TSPL_DBT_NEFT_RCDF.Document_Code and RCDF.[dbo].TSPL_DBT_NEFT_RCDF.DB_Name = '" & clsCommon.myCstr(fndUnion.Value) & "' where convert(date,TSPL_DBT_NEFT_RCDF.From_Date,103)>=convert(date,'" & Slot1 & "',103) and convert(date,TSPL_DBT_NEFT_RCDF.To_Date,103)<=convert(date,'" & Slot2 & "',103)"
            If rbtnTransactionPending.Checked Then
                BaseQry += " And TSPL_DBT_NEFT_RCDF.Status =0"
            End If

            BaseQry = " select [Union Name], [Document No] ,[Created By],[Created Date],[Level-1 Approved By],[Level-1 Approved Date],[Level-2 Approved By],[Level-2 Approved Date],[Level-3 Approved By],[Level-3 Approved Date],[Posted By],[Posted Date],Status from (" & BaseQry & "  )xx "
        End If

        Return BaseQry
    End Function

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Try
            GetReportID()
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
                Dim viewBlank As New TableViewDefinition()
                gvData.ViewDefinition = viewBlank
                If rbtnDetail.IsChecked Then
                    View()
                End If

                gvData.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rbtnDetail.IsChecked Then
            VarID += "_D"
        ElseIf rbtnSummary.IsChecked Then
            VarID += "_S"
        End If

        If rbtnTransactionPending.Checked Then
            VarID += "_TP"
        ElseIf rbtnTranasctionAll.Checked Then
            VarID += "_TA"
        End If
    End Sub

    Sub View()
        Try
            If gvData.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gvData.Columns("Union Name").Name)

                view.ColumnGroups.Add(New GridViewColumnGroup("DBT"))
                view.ColumnGroups.Add(New GridViewColumnGroup("Approval Level-1"))
                view.ColumnGroups.Add(New GridViewColumnGroup("Approval Level-2"))
                view.ColumnGroups.Add(New GridViewColumnGroup("Approval Level-3"))
                view.ColumnGroups.Add(New GridViewColumnGroup("RCDF Approval"))

                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gvData.Columns("Document No").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gvData.Columns("Created By").Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gvData.Columns("Created Date").Name)

                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gvData.Columns("Level-1 Approved By").Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gvData.Columns("Level-1 Approved Date").Name)

                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gvData.Columns("Level-2 Approved By").Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gvData.Columns("Level-2 Approved Date").Name)

                view.ColumnGroups(4).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gvData.Columns("Level-3 Approved By").Name)
                view.ColumnGroups(4).Rows(0).ColumnNames.Add(gvData.Columns("Level-3 Approved Date").Name)

                view.ColumnGroups(5).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gvData.Columns("Posted By").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gvData.Columns("Posted Date").Name)
                view.ColumnGroups(5).Rows(0).ColumnNames.Add(gvData.Columns("Status").Name)


                gvData.ViewDefinition = view

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        gvData.DataSource = Nothing
        gvData.Rows.Clear()
        gvData.MasterTemplate.SummaryRowsBottom.Clear()
        gvData.Refresh()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Print(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
    Private Sub print(ByVal exporter As EnumExportTo)
        Try
            If gvData.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
                arrHeader.Add("User : " + objCommonVar.CurrentUser)
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gvData, "", "", , arrHeader)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class