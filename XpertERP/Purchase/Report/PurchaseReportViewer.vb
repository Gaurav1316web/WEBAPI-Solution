Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Collections
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports common
Public Class PurchaseViewer
#Region "Variable"
    Dim strQuery As String
    Dim Ds As DataSet
#End Region
    Private Sub PrintJrnlVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.crptViewer.RefreshReport()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    'Public Sub funreport(ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String)
    '    funreport(dt, EnumTecxpertPaperSize.NA, strReportName, strCaption)
    'End Sub
    'Public Sub funreport(ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String)
    '    Try
    '        Dim rptshow As Boolean
    '        If dt.Rows.Count > 0 Then
    '            Dim rpdoc As New ReportDocument()
    '            Dim strpath = Application.StartupPath
    '            Dim strReportPath As String = strpath + "\Crystal Reports\Purchase\" & strReportName & ".rpt"
    '            rpdoc.Load(strReportPath)
    '            rpdoc.SetDataSource(dt)
    '            clsERPFuncationality.SetCustomizedPaperSize(rpdoc, ePaperSize)
    '            crptViewer.ReportSource = rpdoc
    '            rptshow = True
    '            Me.Text = strReportPath
    '            Me.Show()

    '        Else
    '            common.clsCommon.MyMessageBoxShow("No Data Found")
    '            Me.Close()
    '            rptshow = False
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
    '    End Try
    'End Sub
    'Public Sub funsubreport(ByVal qry As String, ByVal qry1 As String, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString)
    '    Try
    '        Dim dt1, dt2 As New DataTable
    '        Dim ds1, ds2 As New DataSet
    '        ds1 = connectSql.RunSQLReturnDS(qry)
    '        dt1 = ds1.Tables(0)
    '        Dim rptshow As Boolean
    '        If dt1.Rows.Count > 0 Then
    '            Dim rpdoc As New ReportDocument()
    '            Dim strpath = Application.StartupPath
    '            Dim strReportPath As String = strpath + "\Crystal Reports\Purchase\" & strReportName & ".rpt"
    '            rpdoc.Load(strReportPath)
    '            If strSubReport1 <> "" Then
    '                ds2 = connectSql.RunSQLReturnDS(qry1)
    '                dt2 = ds2.Tables(0)
    '                rpdoc.OpenSubreport(strSubReport1).SetDataSource(dt2)
    '            End If

    '            rpdoc.SetDataSource(dt1)
    '            crptViewer.ReportSource = rpdoc
    '            rptshow = True
    '            Me.Text = strReportPath
    '            Me.Show()

    '        Else
    '            common.clsCommon.MyMessageBoxShow("No Data Found")
    '            Me.Close()
    '            rptshow = False
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
    '    End Try
    'End Sub

End Class
