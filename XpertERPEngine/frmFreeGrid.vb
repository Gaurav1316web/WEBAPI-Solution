Imports common
Imports System.IO
Imports System.ComponentModel
Imports Telerik.WinControls.UI.Export

Public Class FrmFreeGrid
    Public strFormName As String = ""
    Public dt As DataTable = Nothing
    Dim arrColumnsForShowTotal As List(Of String) = Nothing
    Public ReportID As String = ""
    Public arrFooter As List(Of String) = Nothing
    Dim userCode, companyCode As String
    Public arr As ArrayList
    Public arrEditableColumn As List(Of String) = Nothing


    Private Sub FrmFreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If clsCommon.myLen(ReportID) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Report ID Not found")
            Me.Close()
            Exit Sub
        End If
        btnopen.Visible = False
        If clsCommon.CompairString("PendingGLACRollup", ReportID) = CompairStringResult.Equal Then
            btnopen.Visible = True
        End If

        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.DataSource = Nothing
            gv1.DataSource = dt
            Me.Text = strFormName
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                If arrEditableColumn IsNot Nothing AndAlso arrEditableColumn.Count > 0 Then
                    gv1.Columns(ii).ReadOnly = Not arrEditableColumn.Contains(gv1.Columns(ii).Name)
                Else
                    gv1.Columns(ii).ReadOnly = True
                End If
                gv1.Columns(ii).IsVisible = True
                gv1.Columns(ii).Width = 100
                gv1.Columns(ii).BestFit()
            Next




            ReStoreGridLayout()
            gv1.ShowGroupPanel = False
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.ShowFilteringRow = True
            gv1.EnableFiltering = True
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False

            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If arrFooter IsNot Nothing AndAlso arrFooter.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For Each strFtr As String In arrFooter
                    Dim item1 As New GridViewSummaryItem(strFtr, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                Next
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            End If

        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        If clsCommon.CompairString("SchedulePenalty", Me.ReportID) = CompairStringResult.Equal Then
            dt = gv1.DataSource
        End If
        Me.Close()
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If

    End Sub

    Private Sub RadMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem3.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub Export(ByVal Type As Integer)
        If SaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
            Return
        End If
        If SaveFileDialog.FileName.Equals(String.Empty) Then
            RadMessageBox.SetThemeName(gv1.ThemeName)
            ''sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
            common.clsCommon.MyMessageBoxShow("Please enter a file name.")
            Return
        End If
        Dim fileName As String = Me.SaveFileDialog.FileName

        Try
            Select Case Type

                Case 1 'export to excelML       
                    fileName += ".xls"
                    RunExportToExcelML(fileName)
                Case 2 'export to CSV      
                    fileName += ".csv"
                    RunExportToCSV(fileName)
                    'Case 3 'export to HTML                    
                    '    RunExportToHTML(fileName, openExportFile)
                Case 4 'export to PDF   
                    fileName += ".pdf"
                    RunExportToPDF(fileName)
            End Select

            If common.clsCommon.MyMessageBoxShow("Open the Exported file", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Try

                    System.Diagnostics.Process.Start(fileName)
                Catch ex As Exception
                    Dim message As String = String.Format("The file cannot be opened on your system." & Constants.vbLf & "Error message: {0}", ex.Message)
                    common.clsCommon.MyMessageBoxShow(message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error)
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As String)
        Dim excelExporter As New ExportToExcelML(gv1)
        excelExporter.SheetName = Me.Text
        excelExporter.SheetMaxRows = ExcelMaxRows._1048576
        excelExporter.SummariesExportOption = SummariesOption.ExportAll
        'excelExporter.ExportVisualSettings = Me.exportVisualSettings
        Try
            excelExporter.RunExport(fileName)
        Catch ex As IOException
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RunExportToCSV(ByVal fileName As String)
        Dim csvExporter As New ExportToCSV(gv1)
        csvExporter.SummariesExportOption = SummariesOption.ExportAll
        Try
            csvExporter.RunExport(fileName)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RunExportToPDF(ByVal fileName As String)
        Dim pdfExporter As New ExportToPDF(gv1)
        pdfExporter.PdfExportSettings.Title = "My PDF Title"
        pdfExporter.PdfExportSettings.PageWidth = 297
        pdfExporter.PdfExportSettings.PageHeight = 210
        pdfExporter.PageTitle = Me.Text
        pdfExporter.FitToPageWidth = True
        Try
            pdfExporter.RunExport(fileName)
            RadMessageBox.SetThemeName(gv1.ThemeName)
        Catch ex As IOException
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Export(1)
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Export(4)
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        Try
            If clsCommon.CompairString("CompleteTransferItem", Me.ReportID) = CompairStringResult.Equal Then
                If gv1.DataSource IsNot Nothing AndAlso clsCommon.myLen(gv1.CurrentRow.Cells("Invoice No").Value) > 0 Then
                    Dim strShipmentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Shipment_No from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Invoice No").Value) + "'"))
                    If clsCommon.myLen(strShipmentNo) > 0 Then
                        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.LoadOut, strShipmentNo)
                    End If

                End If
            ElseIf clsCommon.CompairString("FrmBankRecoHide", Me.ReportID) = CompairStringResult.Equal Then
                Dim qry As String = clsBankReco.VerifyAllReco(arr, clsCommon.myCstr(gv1.CurrentRow.Cells("Reconciliation_Id").Value), False)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frm As New FrmFreeGrid
                    frm.dt = dt
                    frm.strFormName = Me.Name + " (Documents)"
                    frm.ReportID = "FrmBankRecoUnHideNext"
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()
                End If
            ElseIf clsCommon.CompairString("FrmBankRecoUnHide", Me.ReportID) = CompairStringResult.Equal Then
                Dim qry As String = clsBankReco.VerifyAllReco(arr, clsCommon.myCstr(gv1.CurrentRow.Cells("Reconciliation_Id").Value), False)
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim frm As New FrmFreeGrid
                    frm.dt = dt
                    frm.strFormName = Me.Name + " (Documents)"
                    frm.ReportID = "FrmBankRecoUnHideNext"
                    frm.WindowState = FormWindowState.Maximized
                    frm.Show()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If arrFooter IsNot Nothing AndAlso arrFooter.Count > 0 Then
            If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight
            End If
        End If
    End Sub

    Private Sub btnopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnopen.Click
        userCode = objCommonVar.CurrentUserCode
        companyCode = objCommonVar.CurrentCompanyCode
        'Dim frm As New frmGLAccount(userCode, companyCode)
        'frm.strFormName = "GLAccount"

        'frm.ShowDialog()
    End Sub
End Class
