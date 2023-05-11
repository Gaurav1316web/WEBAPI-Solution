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
Imports CrystalDecisions.Shared
Imports XpertERPEngine

Public Class frmCrystalReportViewer

#Region "Variable"
    Dim strQuery As String
    Dim Ds As DataSet
    Public WithEvents objReport As CrystalDecisions.CrystalReports.Engine.ReportClass = Nothing
#End Region

    Private Function GetReportPath(ByVal crpfolder As CrystalReportFolder, ByVal strReportName As String) As String
        Dim strpath = Application.StartupPath
        If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
            strpath += "\Crystal Reports\Crystal Reports\" + objCommonVar.CurrentCompanyCode

        End If
        Dim strReportPath As String = ""
        If crpfolder = CrystalReportFolder.CommonServices Then
            strReportPath = strpath + "\Crystal Reports\Common Services\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.FixedAssets Then
            strReportPath = strpath + "\Crystal Reports\Fixed Assets\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.GeneralLedger Then
            strReportPath = strpath + "\Crystal Reports\General Ledger\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.HRPayroll Then
            strReportPath = strpath + "\Crystal Reports\HR_Payroll\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.HumanResource Then
            strReportPath = strpath + "\Crystal Reports\Human Resource\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.InventoryReport Then
            strReportPath = strpath + "\Crystal Reports\Inventory Report\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.KwalitySalesReport Then
            strReportPath = strpath + "\Crystal Reports\Kwality Sales Report\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.MilkProcurement Then
            strReportPath = strpath + "\Crystal Reports\Milk Procurement\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.NewSalesReports Then
            strReportPath = strpath + "\Crystal Reports\New Sales Reports\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.PRODUCTION Then
            strReportPath = strpath + "\Crystal Reports\PRODUCTION\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.Purchase Then
            strReportPath = strpath + "\Crystal Reports\Purchase\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.PurchaseOrder Then
            strReportPath = strpath + "\Crystal Reports\Purchase Order\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.SalesReport Then
            strReportPath = strpath + "\Crystal Reports\Sales Report\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.ServiceReport Then
            strReportPath = strpath + "\Crystal Reports\Service Report\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.TDS Then
            strReportPath = strpath + "\Crystal Reports\TDS\" & strReportName & ".rpt"
        ElseIf crpfolder = CrystalReportFolder.UtilityReports Then
            strReportPath = strpath + "\Crystal Reports\Utility Reports\" & strReportName & ".rpt"
        End If
        Return strReportPath
    End Function

    Private Sub PrintJrnlVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.crptViewer.RefreshReport()
        Me.WindowState = FormWindowState.Maximized
        If objReport IsNot Nothing Then
            crptViewer.ReportSource = objReport
            crptViewer.Show()
        End If
    End Sub

    Public Sub funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String)
        funreport(crpfolder, dt, EnumTecxpertPaperSize.NA, strReportName, strCaption)
    End Sub

    Public Sub funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String)
        funreport(crpfolder, dt, ePaperSize, strReportName, strCaption, False)
    End Sub
    Public Sub funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String, ByVal IsShowDilogue As Boolean)
        Dim rptshow As Boolean
        Try
            If dt.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                rpdoc.SetDataSource(dt)
                clsERPFuncationality.SetCustomizedPaperSize(rpdoc, ePaperSize)
                crptViewer.ReportSource = rpdoc
                rptshow = True
                Me.Text = strReportPath
                If IsShowDilogue Then
                    Me.ShowDialog()
                Else
                    Me.Show()
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            rptshow = False
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
    Public Sub funsubreport(ByVal crpfolder As CrystalReportFolder, ByVal qry As String, ByVal qry1 As String, ByVal strReportName As String, ByVal strCaption As String)
        funsubreport(crpfolder, qry, qry1, strReportName, strCaption, vbNullString)
    End Sub
    Public Sub funsubreport(ByVal crpfolder As CrystalReportFolder, ByVal qry As String, ByVal qry1 As String, ByVal strReportName As String, ByVal strCaption As String, ByVal strSubReport1 As String)
        Try
            Dim dt1, dt2 As New DataTable
            Dim ds1, ds2 As New DataSet
            ds1 = connectSql.RunSQLReturnDS(qry)
            dt1 = ds1.Tables(0)
            Dim rptshow As Boolean
            If dt1.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)

                rpdoc.Load(strReportPath)
                If strSubReport1 <> "" Then
                    ds2 = connectSql.RunSQLReturnDS(qry1)
                    dt2 = ds2.Tables(0)
                    rpdoc.OpenSubreport(strSubReport1).SetDataSource(dt2)
                End If

                rpdoc.SetDataSource(dt1)
                crptViewer.ReportSource = rpdoc
                rptshow = True
                Me.Text = strReportPath
                Me.Show()
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Public Function funreport1(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String) As String
        Dim rptPath As String = ""
        Try
            Dim rptshow As Boolean
            If dt.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                rpdoc.SetDataSource(dt)
                Me.crptViewer.ReportSource = rpdoc
                'rptshow = True
                'Me.Text = strReportPath
                'Me.Show()
                Me.crptViewer.Refresh()
                Dim strpath = Application.StartupPath
                If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
                    strpath += "\Crystal Reports\Crystal Reports\" + objCommonVar.CurrentCompanyCode
                End If
                Dim subPath As String = strpath + "\\Mail Reports"

                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

                If (IsExists = False) Then

                    System.IO.Directory.CreateDirectory(subPath)
                End If
                Dim CrExportOptions As ExportOptions
                Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
                CrDiskFileDestinationOptions.DiskFileName = strpath + "\Mail Reports\" & strReportName & ".pdf"
                rptPath = CrDiskFileDestinationOptions.DiskFileName
                CrExportOptions = rpdoc.ExportOptions

                With CrExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.PortableDocFormat
                    .DestinationOptions = CrDiskFileDestinationOptions
                    .FormatOptions = CrFormatTypeOptions
                End With
                rpdoc.Export()
            Else
                RadMessageBox.Show("No Data Found")
                Me.Close()
                rptshow = False
            End If
            Return rptPath
        Catch ex As Exception
            RadMessageBox.Show(ex.Message.ToString())
        End Try
        Return rptPath
    End Function

    Public Sub funsubreportWithdt(ByVal crpfolder As CrystalReportFolder, ByVal dt1 As DataTable, ByVal dt2 As DataTable, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal dt3 As DataTable = Nothing, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal dt4 As DataTable = Nothing)
        Try
            Dim rptshow As Boolean
            If dt1.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)

                Try
                    If strSubReport1 <> "" Then
                        rpdoc.OpenSubreport(strSubReport1).SetDataSource(dt2)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If strSubReport2 <> "" Then
                        rpdoc.OpenSubreport(strSubReport2).SetDataSource(dt3)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If clsCommon.myLen(strSubReport3) > 0 Then
                        rpdoc.OpenSubreport(strSubReport3).SetDataSource(dt4)
                    End If
                Catch ex As Exception
                End Try

                rpdoc.SetDataSource(dt1)
                crptViewer.ReportSource = rpdoc
                rptshow = True
                Me.Text = strReportPath
                Me.Show()
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Public Function EmailAttachment(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String) As String
        Dim pdfpath As String = ""
        Try

            Dim rptshow As Boolean
            If dt.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                rpdoc.SetDataSource(dt)

                crptViewer.ReportSource = rpdoc
                Me.crptViewer.Refresh()

                Dim strpath = Application.StartupPath
                If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
                    strpath += "\Crystal Reports\Crystal Reports\" + objCommonVar.CurrentCompanyCode
                End If
                Dim subPath As String = strpath + "\\Mail Reports"
                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
                If (IsExists = False) Then
                    System.IO.Directory.CreateDirectory(subPath)
                End If

                '------------Done By Monika--------------------------------------------------form mailing
                Dim CrExportOptions As ExportOptions
                Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()


                CrDiskFileDestinationOptions.DiskFileName = strpath + "\Mail Reports\" & strReportName & ".pdf"
                pdfpath = CrDiskFileDestinationOptions.DiskFileName
                CrExportOptions = rpdoc.ExportOptions

                With CrExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.PortableDocFormat
                    .DestinationOptions = CrDiskFileDestinationOptions
                    .FormatOptions = CrFormatTypeOptions
                End With
                rpdoc.Export()
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try

        Return pdfpath
    End Function

    Public Function EmailSubreportWithdt(ByVal crpfolder As CrystalReportFolder, ByVal dt1 As DataTable, ByVal dt2 As DataTable, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal dt3 As DataTable = Nothing, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal dt4 As DataTable = Nothing) As String
        Dim rptshow As Boolean
        Dim pdfpath As String = ""
        Try
            If dt1.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                Try
                    If strSubReport1 <> "" Then
                        rpdoc.OpenSubreport(strSubReport1).SetDataSource(dt2)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If strSubReport2 <> "" Then
                        rpdoc.OpenSubreport(strSubReport2).SetDataSource(dt3)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If clsCommon.myLen(strSubReport3) > 0 Then
                        rpdoc.OpenSubreport(strSubReport3).SetDataSource(dt4)
                    End If
                Catch ex As Exception
                End Try

                rpdoc.SetDataSource(dt1)
                crptViewer.ReportSource = rpdoc
                Me.crptViewer.Refresh()

                Dim strpath = Application.StartupPath
                If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
                    strpath += "\Crystal Reports\Crystal Reports\" + objCommonVar.CurrentCompanyCode
                End If
                Dim subPath As String = strpath + "\\Mail Reports"
                Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)

                If (IsExists = False) Then
                    System.IO.Directory.CreateDirectory(subPath)
                End If

                Dim CrExportOptions As ExportOptions
                Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()


                CrDiskFileDestinationOptions.DiskFileName = strpath + "\Mail Reports\" & strReportName & ".pdf"
                pdfpath = CrDiskFileDestinationOptions.DiskFileName
                CrExportOptions = rpdoc.ExportOptions

                With CrExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.PortableDocFormat
                    .DestinationOptions = CrDiskFileDestinationOptions
                    .FormatOptions = CrFormatTypeOptions
                End With
                rpdoc.Export()

            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try

        Return pdfpath
    End Function

    Public Sub funSubreport(ByVal crpfolder As CrystalReportFolder, ByVal strquery As String, ByVal strquery1 As String, ByVal strquery2 As String, ByVal strquery3 As String, ByVal strquery4 As String, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal strSubReport4 As String = vbNullString, Optional ByVal strSubReport5 As String = vbNullString, Optional ByVal strSubReport6 As String = vbNullString, Optional ByVal strSubReport7 As String = vbNullString, Optional ByVal strSubReport8 As String = vbNullString, Optional ByVal strSubReport9 As String = vbNullString, Optional ByVal strSubReport10 As String = vbNullString)
        Try
            Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11 As New DataTable
            dt = clsDBFuncationality.GetDataTable(strquery)
            Dim rptshow As Boolean
            If dt.Rows.Count > 0 Then

                Dim rpdoc As New ReportDocument()

                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                If strSubReport1 <> "" Then
                    dt1 = clsDBFuncationality.GetDataTable(strquery1)
                    rpdoc.OpenSubreport(strSubReport1).SetDataSource(dt1)
                End If
                If strSubReport2 <> "" Then
                    dt2 = clsDBFuncationality.GetDataTable(strquery2)
                    rpdoc.OpenSubreport(strSubReport2).SetDataSource(dt2)
                End If
                If strSubReport3 <> "" Then
                    dt3 = clsDBFuncationality.GetDataTable(strquery3)
                    rpdoc.OpenSubreport(strSubReport3).SetDataSource(dt3)
                End If
                If strSubReport4 <> "" Then
                    dt4 = clsDBFuncationality.GetDataTable(strquery4)
                    rpdoc.OpenSubreport(strSubReport4).SetDataSource(dt4)
                End If

                rpdoc.SetDataSource(dt)
                Me.crptViewer.ReportSource = rpdoc
                rptshow = True
                Me.Text = strReportPath
                Me.Show()
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub

    Public Sub funExcelForamtReport(ByVal crpfolder As CrystalReportFolder, ByVal strquery As String, ByVal strReportName As String, ByVal strCaption As String)
        Try
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(strquery)
            Dim rptshow As Boolean
            If dt.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()

                Dim strpath = Application.StartupPath
                If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
                    strpath += "\Crystal Reports\Crystal Reports\" + objCommonVar.CurrentCompanyCode
                End If


                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                rpdoc.SetDataSource(dt)
                Me.crptViewer.ReportSource = rpdoc
                Dim CrExportOptions As ExportOptions
                Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                Dim CrFormatTypeOptions As New ExcelFormatOptions
                CrDiskFileDestinationOptions.DiskFileName = strpath + "\Sales Report In ExcelFormat.xls"
                Dim fullpath As String = CrDiskFileDestinationOptions.DiskFileName
                CrExportOptions = rpdoc.ExportOptions
                With CrExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.Excel
                    .DestinationOptions = CrDiskFileDestinationOptions
                    .FormatOptions = CrFormatTypeOptions

                End With
                rpdoc.Export()
                Dim xlsApp As Microsoft.Office.Interop.Excel.Application
                Dim xlsWB As Microsoft.Office.Interop.Excel.Workbook
                xlsApp = New Microsoft.Office.Interop.Excel.Application
                xlsApp.Visible = True
                xlsWB = xlsApp.Workbooks.Open(fullpath)
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message.ToString())
        End Try
    End Sub
End Class
