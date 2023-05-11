'by vipin for pdf on 08/02/2013

Imports common
Imports System.Data.SqlClient

Public Class FrmReverseSettlement
    Inherits FrmMainTranScreen
    Dim dt As DataTable

    Private Sub FrmReverseSettlement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadLocation()
        Reset()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmReverseSettlementDetail)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
        LoadLocation()
        chkLocAll.IsChecked = True
        gvReport.GroupDescriptors.Clear()
        gvReport.MasterTemplate.SummaryRowsBottom.Clear()
        dt = Nothing
        gvReport.DataSource = Nothing
        gvReport.Columns.Clear()
        gvReport.Rows.Clear()
    End Sub

    Sub LoadLocation()
        Dim strquery As String = "SELECT Location_Code AS [Code], Location_Desc AS [Description] FROM TSPL_LOCATION_MASTER where Location_Type='Physical' AND Excisable='F'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub


    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub

    Private Sub chkLocSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub

    Private Sub btnExportToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportToExcel.Click
        'ExportToExcel()
    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try
            RefreshData()
            If gvReport.DataSource Is Nothing OrElse gvReport.Rows.Count <= 0 Then
                Throw New Exception("No Data found to Export")
            End If
            ExportToExcelGV(exporter)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefreshData()
    End Sub

    Private Sub RefreshData()
        Try

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please Select at least Single Location or Select All")
                Return
            End If

            Dim qry As String = ""

            qry = " SELECT Reverse_DocNo, QuickSettlementNo, Transfer_LoadOutNo, Transfer_LoadIntno, Adjustment_No, GLVoucher_No, Location, CONVERT(VARCHAR, ReverseDate, 103) as ReverseDate, CreatedBy FROM TSPL_SETTLEMENT_REVERSE_MAPPING WHERE 1=1"
            If chkLocSelect.IsChecked = True Then
                qry += " and  TSPL_SETTLEMENT_REVERSE_MAPPING.Location in (" + (clsCommon.GetMulcallString(cbgLocation.CheckedValue)) + ") "
            End If

            '--------------------------------------------------------------------------

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data found ")
            Else
                gvReport.DataSource = dt
                FormatGrid()
            End If
            RadPageView1.SelectedPage = RadPageViewPage2


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)

        End Try
    End Sub

    Private Sub ExportToExcelGV(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " ")
            If chkLocSelect.IsChecked Then
                strTemp = ""
                For Each Str As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strTemp) > 0 Then
                        strTemp += ", "
                    End If
                    strTemp += Str
                Next
                arrHeader.Add("Location : " + strTemp)
            End If

            'clsCommon.MyExportToExcel("Reverse Settlement Detail ", gvReport, arrHeader, Me.Text)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel("Reverse Settlement Detail ", gvReport, arrHeader, Me.Text)


            Else
                clsCommon.MyExportToPDF("Reverse Settlement Detail  ", gvReport, arrHeader, "Reverse Settlement Detail ", True)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub FormatGrid()
        gvReport.AllowAddNewRow = False
        gvReport.TableElement.TableHeaderHeight = 40
        gvReport.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvReport.Columns.Count - 1
            gvReport.Columns(ii).ReadOnly = True
            gvReport.Columns(ii).IsVisible = False
        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()

        gvReport.Columns("Reverse_DocNo").IsVisible = True
        gvReport.Columns("Reverse_DocNo").Width = 111
        gvReport.Columns("Reverse_DocNo").HeaderText = "Reverse Doc No"

        gvReport.Columns("QuickSettlementNo").IsVisible = True
        gvReport.Columns("QuickSettlementNo").Width = 121
        gvReport.Columns("QuickSettlementNo").HeaderText = "Quick Settlement No"

        gvReport.Columns("Transfer_LoadOutNo").IsVisible = True
        gvReport.Columns("Transfer_LoadOutNo").Width = 121
        gvReport.Columns("Transfer_LoadOutNo").HeaderText = "Transfer/LoadOut No"

        gvReport.Columns("Transfer_LoadIntno").IsVisible = True
        gvReport.Columns("Transfer_LoadIntno").Width = 121
        gvReport.Columns("Transfer_LoadIntno").HeaderText = "Transfer/LoadOut No"

        gvReport.Columns("Adjustment_No").IsVisible = True
        gvReport.Columns("Adjustment_No").Width = 81
        gvReport.Columns("Adjustment_No").HeaderText = "Adjustment No"

        gvReport.Columns("GLVoucher_No").IsVisible = True
        gvReport.Columns("GLVoucher_No").Width = 101
        gvReport.Columns("GLVoucher_No").HeaderText = "GL Voucher No"

        gvReport.Columns("Location").Width = 71
        gvReport.Columns("Location").HeaderText = "Location"
        gvReport.Columns("Location").IsVisible = True

        gvReport.Columns("ReverseDate").Width = 101
        gvReport.Columns("ReverseDate").HeaderText = "Reverse Date"
        gvReport.Columns("ReverseDate").IsVisible = True

        gvReport.Columns("CreatedBy").Width = 121
        gvReport.Columns("CreatedBy").HeaderText = "Created By"
        gvReport.Columns("CreatedBy").IsVisible = True

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        ExportToExcel(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        ExportToExcel(EnumExportTo.PDF)
    End Sub
End Class
