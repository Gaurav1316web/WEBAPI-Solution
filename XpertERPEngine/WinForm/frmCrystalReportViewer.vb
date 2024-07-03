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
Public Class frmCrystalReportViewer
    ''check in Sanjay 20200619
#Region "Variable"
    Dim strQuery As String
    Dim Ds As DataSet
    Public WithEvents objReport As CrystalDecisions.CrystalReports.Engine.ReportClass = Nothing
    Dim rpdoc As New ReportDocument()
    Public ShowCystalReportToolbar As Boolean = True
#End Region

    Private Sub PrintJrnlVoucher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.crptViewer.RefreshReport()
        Me.WindowState = FormWindowState.Maximized
        If objReport IsNot Nothing Then
            crptViewer.ReportSource = objReport
            crptViewer.Show()
        End If
    End Sub

    Public Function GetReportPath(ByVal crpfolder As CrystalReportFolder, ByVal strReportName As String) As String
        Dim strReportPath As String = ""
        Dim dtTransDate As Date?
        dtTransDate = Nothing
        strReportPath = GetReportPath(crpfolder, strReportName, dtTransDate)
        Return strReportPath
    End Function

    Private Function GetReportPath(ByVal crpfolder As CrystalReportFolder, ByVal strReportName As String, ByVal dtTransDate As Date?) As String
        Try
            Dim strpath = Application.StartupPath
            Dim strGST As String = ""

            If clsERPFuncationality.GetGSTStatus(dtTransDate) Then
                strGST = " GST"
            End If

            If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
                Dim strCompCode = ""
                If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDP") = CompairStringResult.Equal Then
                    strCompCode = "RCDF"
                Else
                    strCompCode = objCommonVar.CurrentCompanyCode
                End If
                strpath += "\Xpert Crystal Reports\" + strCompCode
            End If
            Dim strReportPath As String = ""
            If crpfolder = CrystalReportFolder.CommonServices Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Common Services\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.FixedAssets Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Fixed Assets\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.GeneralLedger Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\General Ledger\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.HRPayroll Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\HR_Payroll\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.HumanResource Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Human Resource\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.InventoryReport Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Inventory Report\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.KwalitySalesReport Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Kwality Sales Report\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.MilkProcurement Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Milk Procurement\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.NewSalesReports Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\New Sales Reports\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.PRODUCTION Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\PRODUCTION\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.Purchase Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Purchase\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.PurchaseOrder Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Purchase Order\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.SalesReport Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Sales Report\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.ServiceReport Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Service Report\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.TDS Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\TDS\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.UtilityReports Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Utility Reports\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.Engineering Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Engineering\" & strReportName & ".rpt"
            ElseIf crpfolder = CrystalReportFolder.UnionReports Then
                strReportPath = strpath + "\Crystal Reports" + strGST + "\Union Reports\" & strReportName & ".rpt"
            End If
            Return strReportPath
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
    End Function

    Private Sub frmCrystalReportViewer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        rpdoc.Dispose()
    End Sub

    Public Function funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String) As String
        Return funreport(False, crpfolder, dt, strReportName, strCaption)
    End Function

    Public Function funreport(ByVal isPDFPath As Boolean, ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String) As String
        Dim dtTranDate As Date?
        dtTranDate = Nothing
        Return funreport(isPDFPath, crpfolder, dt, EnumTecxpertPaperSize.NA, strReportName, strCaption, dtTranDate)
    End Function

    Public Function funreport(ByVal isPDFPath As Boolean, ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String, ByVal dtTranDate? As Date) As String
        Return funreport(isPDFPath, crpfolder, dt, EnumTecxpertPaperSize.NA, strReportName, strCaption, dtTranDate)
    End Function

    Public Function funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String, ByVal dtTranDate? As Date) As String
        Return funreport(crpfolder, dt, EnumTecxpertPaperSize.NA, strReportName, strCaption, dtTranDate)
    End Function

    Public Function funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String, ByVal dtTranDate? As Date) As String
        Return funreport(crpfolder, dt, ePaperSize, strReportName, strCaption, False, dtTranDate)
    End Function

    Public Function funreport(ByVal isPDFPath As Boolean, ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String, ByVal dtTranDate? As Date) As String
        Return funreport(isPDFPath, crpfolder, dt, ePaperSize, strReportName, strCaption, False, dtTranDate)
    End Function

    Public Function funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String, ByVal IsShowDilogue As Boolean) As String
        Return funreport(False, crpfolder, dt, ePaperSize, strReportName, strCaption, False, Nothing)
    End Function

    Public Function funreport(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String, ByVal IsShowDilogue As Boolean, ByVal dtTranDate? As Date) As String
        Return funreport(False, crpfolder, dt, ePaperSize, strReportName, strCaption, IsShowDilogue, dtTranDate)
    End Function
    Public Function funreport(ByVal isPDFPath As Boolean, ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal ePaperSize As EnumTecxpertPaperSize, ByVal strReportName As String, ByVal strCaption As String, ByVal IsShowDilogue As Boolean, ByVal dtTranDate? As Date) As String
        Dim PDFpath As String = ""
        Dim rptshow As Boolean
        Try
            If dt.Rows.Count > 0 Then
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName, dtTranDate)
                rpdoc.Load(strReportPath)
                rpdoc.SetDataSource(dt)
                clsERPFuncationalityOLD.SetCustomizedPaperSize(rpdoc, ePaperSize)
                crptViewer.ReportSource = rpdoc
                crptViewer.ShowPrintButton = ShowCystalReportToolbar
                crptViewer.ShowExportButton = ShowCystalReportToolbar

                If isPDFPath Then
                    Me.crptViewer.Refresh()
                    Dim subPath As String = "C:\\ERPTempFolder"
                    Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
                    If (IsExists = False) Then
                        System.IO.Directory.CreateDirectory(subPath)
                    End If
                    Dim CrExportOptions As ExportOptions
                    Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                    Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
                    subPath += "\\" & clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmttss") & ".pdf"
                    IsExists = System.IO.File.Exists(subPath)
                    If IsExists Then
                        System.IO.File.Delete(subPath)
                    End If

                    CrDiskFileDestinationOptions.DiskFileName = subPath
                    PDFpath = CrDiskFileDestinationOptions.DiskFileName
                    CrExportOptions = rpdoc.ExportOptions

                    With CrExportOptions
                        .ExportDestinationType = ExportDestinationType.DiskFile
                        .ExportFormatType = ExportFormatType.PortableDocFormat
                        .DestinationOptions = CrDiskFileDestinationOptions
                        .FormatOptions = CrFormatTypeOptions
                    End With
                    rpdoc.Export()
                    rpdoc.Close()
                    rpdoc.Dispose()
                Else
                    rpdoc.ReportOptions.EnableSaveDataWithReport = False
                    rpdoc.Refresh()
                    rptshow = True
                    Me.Text = strReportPath
                    If IsShowDilogue Then
                        Me.ShowDialog()
                    Else
                        Me.Show()
                    End If
                End If
            Else
                Throw New Exception("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            rptshow = False
            Throw New Exception(ex.Message.ToString())
        End Try
        Return PDFpath
    End Function

    Public Sub funsubreport(ByVal crpfolder As CrystalReportFolder, ByVal qry As String, ByVal qry1 As String, ByVal strReportName As String, ByVal strCaption As String)
        Dim dtTransDate As Date?
        dtTransDate = Nothing
        funsubreport(crpfolder, qry, qry1, strReportName, strCaption, vbNullString, dtTransDate)
    End Sub

    Public Sub funsubreport(ByVal crpfolder As CrystalReportFolder, ByVal qry As String, ByVal qry1 As String, ByVal strReportName As String, ByVal strCaption As String, ByVal dtTranDate? As Date)
        funsubreport(crpfolder, qry, qry1, strReportName, strCaption, vbNullString, dtTranDate)
    End Sub

    Public Sub funsubreport(ByVal crpfolder As CrystalReportFolder, ByVal qry As String, ByVal qry1 As String, ByVal strReportName As String, ByVal strCaption As String, ByVal strSubReport1 As String)
        Dim dtTransDate As Date?
        dtTransDate = Nothing
        funsubreport(crpfolder, qry, qry1, strReportName, strCaption, strSubReport1, dtTransDate)
    End Sub

    Public Sub funsubreport(ByVal crpfolder As CrystalReportFolder, ByVal qry As String, ByVal qry1 As String, ByVal strReportName As String, ByVal strCaption As String, ByVal strSubReport1 As String, ByVal dtTranDate? As Date)
        Try
            Dim dt1, dt2 As New DataTable
            Dim ds1, ds2 As New DataSet
            ds1 = connectSql.RunSQLReturnDS(qry)
            dt1 = ds1.Tables(0)
            Dim rptshow As Boolean
            If dt1.Rows.Count > 0 Then
                'Dim rpdoc As New ReportDocument()
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName, dtTranDate)

                rpdoc.Load(strReportPath)
                If strSubReport1 <> "" Then
                    ds2 = connectSql.RunSQLReturnDS(qry1)
                    dt2 = ds2.Tables(0)
                    rpdoc.OpenSubreport(strSubReport1).SetDataSource(dt2)
                End If

                rpdoc.SetDataSource(dt1)
                crptViewer.ReportSource = rpdoc
                crptViewer.ShowPrintButton = ShowCystalReportToolbar
                crptViewer.ShowExportButton = ShowCystalReportToolbar
                rpdoc.ReportOptions.EnableSaveDataWithReport = False
                rpdoc.Refresh()

                rptshow = True
                Me.Text = strReportPath
                Me.Show()
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
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
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                rpdoc.SetDataSource(dt)
                Me.crptViewer.ReportSource = rpdoc
                Me.crptViewer.Refresh()
                Dim strpath = Application.StartupPath
                If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
                    strpath += "\Xpert Crystal Reports\" + objCommonVar.CurrentCompanyCode
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
                rpdoc.Close()
                rpdoc.Dispose()
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

    Public Function funsubreportWithdt(ByVal crpfolder As CrystalReportFolder, ByVal dt1 As DataTable, ByVal dt2 As DataTable, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal dt3 As DataTable = Nothing, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal dt4 As DataTable = Nothing, Optional ByVal strSubReport4 As String = vbNullString, Optional ByVal dt5 As DataTable = Nothing) As String
        Dim dtTransDate As Date?
        dtTransDate = Nothing
        Return funsubreportWithdt(crpfolder, dt1, dt2, strReportName, strCaption, dtTransDate, strSubReport1, strSubReport2, dt3, strSubReport3, dt4, strSubReport4, dt5)
    End Function

    Public Function funsubreportWithdt(ByVal isPDFPath As Boolean, ByVal crpfolder As CrystalReportFolder, ByVal dt1 As DataTable, ByVal dt2 As DataTable, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal dt3 As DataTable = Nothing, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal dt4 As DataTable = Nothing, Optional ByVal strSubReport4 As String = vbNullString, Optional ByVal dt5 As DataTable = Nothing) As String
        Dim dtTransDate As Date?
        dtTransDate = Nothing
        Return funsubreportWithdt(isPDFPath, crpfolder, dt1, dt2, strReportName, strCaption, dtTransDate, strSubReport1, strSubReport2, dt3, strSubReport3, dt4, strSubReport4, dt5)
    End Function

    Public Function funsubreportWithdt(ByVal crpfolder As CrystalReportFolder, ByVal dt1 As DataTable, ByVal dt2 As DataTable, ByVal strReportName As String, ByVal strCaption As String, ByVal dtTransDate? As Date, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal dt3 As DataTable = Nothing, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal dt4 As DataTable = Nothing, Optional ByVal strSubReport4 As String = vbNullString, Optional ByVal dt5 As DataTable = Nothing) As String
        Return funsubreportWithdt(False, crpfolder, dt1, dt2, strReportName, strCaption, dtTransDate, strSubReport1, strSubReport2, dt3, strSubReport3, dt4, strSubReport4, dt5)
    End Function


    Public Function funsubreportWithdt(ByVal isPDFPath As Boolean, ByVal crpfolder As CrystalReportFolder, ByVal dt1 As DataTable, ByVal dt2 As DataTable, ByVal strReportName As String, ByVal strCaption As String, ByVal dtTransDate? As Date, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal dt3 As DataTable = Nothing, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal dt4 As DataTable = Nothing, Optional ByVal strSubReport4 As String = vbNullString, Optional ByVal dt5 As DataTable = Nothing, Optional ByVal strSubReport5 As String = vbNullString, Optional ByVal dt6 As DataTable = Nothing, Optional ByVal strSubReport6 As String = vbNullString, Optional ByVal dt7 As DataTable = Nothing, Optional ByVal strSubReport7 As String = vbNullString, Optional ByVal dt8 As DataTable = Nothing) As String
        Dim PDFPath As String = ""
        Dim strReportPath As String = Nothing
        Try
            Dim rptshow As Boolean
            If dt1.Rows.Count > 0 Then
                strReportPath = GetReportPath(crpfolder, strReportName, dtTransDate)
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

                Try
                    If clsCommon.myLen(strSubReport4) > 0 Then
                        rpdoc.OpenSubreport(strSubReport4).SetDataSource(dt5)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If clsCommon.myLen(strSubReport5) > 0 Then
                        rpdoc.OpenSubreport(strSubReport5).SetDataSource(dt6)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If clsCommon.myLen(strSubReport6) > 0 Then
                        rpdoc.OpenSubreport(strSubReport6).SetDataSource(dt7)
                    End If
                Catch ex As Exception
                End Try

                Try
                    If clsCommon.myLen(strSubReport7) > 0 Then
                        rpdoc.OpenSubreport(strSubReport7).SetDataSource(dt8)
                    End If
                Catch ex As Exception
                End Try

                rpdoc.SetDataSource(dt1)
                crptViewer.ReportSource = rpdoc
                crptViewer.ShowPrintButton = ShowCystalReportToolbar
                crptViewer.ShowExportButton = ShowCystalReportToolbar

                If isPDFPath Then
                    Me.crptViewer.Refresh()
                    Dim subPath As String = "C:\\ERPTempFolder"
                    Dim IsExists As Boolean = System.IO.Directory.Exists(subPath)
                    If (IsExists = False) Then
                        System.IO.Directory.CreateDirectory(subPath)
                    End If
                    Dim CrExportOptions As ExportOptions
                    Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                    Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()
                    subPath += "\\" & clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmsss") & ".pdf"
                    IsExists = System.IO.File.Exists(subPath)
                    If IsExists Then
                        System.IO.File.Delete(subPath)
                    End If

                    CrDiskFileDestinationOptions.DiskFileName = subPath
                    PDFPath = CrDiskFileDestinationOptions.DiskFileName
                    CrExportOptions = rpdoc.ExportOptions

                    With CrExportOptions
                        .ExportDestinationType = ExportDestinationType.DiskFile
                        .ExportFormatType = ExportFormatType.PortableDocFormat
                        .DestinationOptions = CrDiskFileDestinationOptions
                        .FormatOptions = CrFormatTypeOptions
                    End With
                    rpdoc.Export()
                    rpdoc.Close()
                    rpdoc.Dispose()
                    rpdoc = Nothing
                    subPath = Nothing
                    CrExportOptions = Nothing
                    CrDiskFileDestinationOptions = Nothing
                    CrFormatTypeOptions = Nothing
                    Me.Close()
                Else
                    rpdoc.ReportOptions.EnableSaveDataWithReport = False
                    rpdoc.Refresh()
                    rptshow = True
                    Me.Text = strReportPath
                    Me.Show()
                End If
            Else
                Throw New Exception("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        Finally
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        End Try
        Return PDFPath
    End Function

    Public Function EmailAttachment(ByVal crpfolder As CrystalReportFolder, ByVal dt As DataTable, ByVal strReportName As String, ByVal strCaption As String) As String
        Dim pdfpath As String = ""
        Try

            Dim rptshow As Boolean
            If dt.Rows.Count > 0 Then
                Dim strReportPath As String = GetReportPath(crpfolder, strReportName)
                rpdoc.Load(strReportPath)
                rpdoc.SetDataSource(dt)

                crptViewer.ReportSource = rpdoc
                Me.crptViewer.Refresh()

                Dim strpath = Application.StartupPath
                Dim subPath As String = strpath + "\\Mail Reports"
                If (Not System.IO.Directory.Exists(subPath)) Then
                    System.IO.Directory.CreateDirectory(subPath)
                End If
                Dim FilePath As String = strpath + "\Mail Reports\" & strReportName & ".pdf"
                If System.IO.File.Exists(FilePath) Then
                    System.IO.File.Delete(FilePath)
                End If

                '------------Done By Monika--------------------------------------------------form mailing
                Dim CrExportOptions As ExportOptions
                Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
                Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()


                CrDiskFileDestinationOptions.DiskFileName = FilePath
                pdfpath = CrDiskFileDestinationOptions.DiskFileName
                CrExportOptions = rpdoc.ExportOptions

                With CrExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.PortableDocFormat
                    .DestinationOptions = CrDiskFileDestinationOptions
                    .FormatOptions = CrFormatTypeOptions
                End With
                rpdoc.Export()
                rpdoc.Close()
                rpdoc.Dispose()
            Else
                Throw New Exception("No Data Found")
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString())
        End Try
        Return pdfpath
    End Function

    Public Function EmailSubreportWithdt(ByVal crpfolder As CrystalReportFolder, ByVal dt1 As DataTable, ByVal dt2 As DataTable, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal dt3 As DataTable = Nothing, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal dt4 As DataTable = Nothing) As String
        Dim rptshow As Boolean
        Dim pdfpath As String = ""
        Try
            If dt1.Rows.Count > 0 Then
                '  Dim rpdoc As New ReportDocument()
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
                    strpath += "\Xpert Crystal Reports\" + objCommonVar.CurrentCompanyCode
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
                rpdoc.Close()
                rpdoc.Dispose()
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try

        Return pdfpath
    End Function

    Public Sub funSubreport(ByVal crpfolder As CrystalReportFolder, ByVal strquery As String, ByVal strquery1 As String, ByVal strquery2 As String, ByVal strquery3 As String, ByVal strquery4 As String, ByVal strReportName As String, ByVal strCaption As String, Optional ByVal strSubReport1 As String = vbNullString, Optional ByVal strSubReport2 As String = vbNullString, Optional ByVal strSubReport3 As String = vbNullString, Optional ByVal strSubReport4 As String = vbNullString, Optional ByVal strSubReport5 As String = vbNullString, Optional ByVal strSubReport6 As String = vbNullString, Optional ByVal strSubReport7 As String = vbNullString, Optional ByVal strSubReport8 As String = vbNullString, Optional ByVal strSubReport9 As String = vbNullString, Optional ByVal strSubReport10 As String = vbNullString)
        Try
            Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11 As New DataTable
            dt = clsDBFuncationality.GetDataTable(strquery)
            Dim rptshow As Boolean
            If dt.Rows.Count > 0 Then

                '   Dim rpdoc As New ReportDocument()

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
                Me.crptViewer.ShowPrintButton = ShowCystalReportToolbar
                Me.crptViewer.ShowExportButton = ShowCystalReportToolbar
                rptshow = True
                Me.Text = strReportPath
                Me.Show()
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Public Sub funExcelForamtReport(ByVal crpfolder As CrystalReportFolder, ByVal strquery As String, ByVal strReportName As String, ByVal strCaption As String)
        Try
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(strquery)
            Dim rptshow As Boolean
            If dt.Rows.Count > 0 Then
                '  Dim rpdoc As New ReportDocument()

                Dim strpath = Application.StartupPath
                If System.IO.File.Exists(Application.StartupPath + "\CrystalReport.Txp") Then
                    strpath += "\Xpert Crystal Reports\" + objCommonVar.CurrentCompanyCode
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
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Me.Close()
                rptshow = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub
End Class
