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
            Slot2 = clsCommon.GetPrintDate(CD.AddMonths(3).AddDays(-1), "dd/MMM/yyyy")
            txtTODate.Value = txtFromDate.Value.AddMonths(2)
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
        dt = clsDBFuncationality.GetDataTable("SELECT [TSPL_APP_LOCATION].Location_Name,[TSPL_APP_LOCATION].DataBase_Name FROM [TSPL_MASTER].[dbo].[TSPL_APP_LOCATION] WHERE DataBase_Name not in ('TECXPERT','UDAIPURTEST','CHT','JMBILL') ORDER BY [TSPL_APP_LOCATION].Location_Name")
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For ii As Integer = 0 To dt.Rows.Count - 1
                BaseQry &= "DB_Name as [Union Name], Document_Code As [Document Code], Convert(varchar,TSPL_DBT_NEFT_RCDF.From_Date,103) AS [From Date], Convert(varchar,TSPL_DBT_NEFT_RCDF.To_Date,103) AS [TO Date],Created_By
                            AS [Created By], Convert(varchar,TSPL_DBT_NEFT_RCDF.Created_Date,103) AS [Created Date], Post_By as [Approved By], Convert(varchar,TSPL_DBT_NEFT_RCDF.Post_Date,103) as [Approved Date & Time], "
                BaseQry &= "CASE WHEN ISNULL(Status,0) = 0 THEN 'Pending' ELSE 'Approved' END AS Status "
                BaseQry &= "FROM TSPL_DBT_NEFT_RCDF WHERE DB_Name ='" & clsCommon.myCstr(fndUnion.Value) & "' "
                BaseQry &= "AND CONVERT(date, From_Date, 103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value) & "' "
                BaseQry &= "AND CONVERT(date, To_Date, 103) <= '" & clsCommon.GetPrintDate(txtTODate.Value) & "' "
                ' Check if rbtnTransactionPosted is checked
                If rbtnTransactionPending.Checked Then
                    ' Show only data with status=0
                    BaseQry &= "and IsNull(Status,0)=0 "
                End If
            Next
        End If
        Return BaseQry
    End Function

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
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
                'SetGridFormat()
                gvData.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found", Me.Text)
            End If
        Catch ex As Exception

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
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Run Date : " + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing, "dd/MM/yyyy hh:mm tt", False), "dd/MM/yyyy hh:mm tt")) ' clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy HH:MM"))
            arrHeader.Add("User : " + objCommonVar.CurrentUser)
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gvData, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvData, "", MyLabel1.Text, , arrHeader)
            Else
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
                Dim strHeader As String = MyLabel1.Text 'Me.Text.Replace("/", "")
                doc.MiddleHeader = strHeader
                doc.HeaderFont = New Font("Verdana", 12, FontStyle.Bold)
                'doc.Print()
                Dim dialog As New RadPrintPreviewDialog
                dialog.Document = doc
                dialog.ToolMenu.Visible = True
                dialog.ShowDialog()
                doc = Nothing
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class