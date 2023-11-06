'-Created by--[Pankaj kumar Chaudhary]-Against Ticket No-[BM00000001756]
Imports common
Imports System.Data.SqlClient

Public Class FrmOperatorEfficiencyReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim blnRefresh As Boolean = False
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmOperaterEfficiencyReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub FrmOperatorEfficiencyReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCustomer()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        chkCustAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")

    End Sub


    Sub LoadCustomer()
        Dim qry As String = "Select EMP_CODE as Code, Emp_Name as Name from TSPL_EMPLOYEE_MASTER Order By EMP_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"
    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        chkCustAll.IsChecked = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            LoadData(Exporter.Print)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Dim dt As DataTable
    Dim qry As String
    Dim FromDate As String
    Dim ToDate As String
    Dim runDate As String

    Sub LoadData(ByVal IsPrint As Exporter)
        Try
            If chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select atleast single customer or select all.")
            End If

            FromDate = clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy")
            ToDate = clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy")
            runDate = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE())

            qry = "Select Document_No, MAX(Document_Date) as Date, MAX(TSPL_EMPLOYEE_MASTER.Emp_Name) as Operator, CONVERT(DECIMAL(18,2), COnvert(Decimal(18,1),SUM(Quantity))*100/CONVERT(Decimal(18,2),SUM(In_Run_Time))) as Efficiency from TSPL_LABOUR_WORKING_SHEET "
            qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_LABOUR_WORKING_SHEET.Employee "
            qry += " WHERE CONVERT(Date,TSPL_LABOUR_WORKING_SHEET.Document_Date,103)>='" + FromDate + "' AND CONVERT(Date,TSPL_LABOUR_WORKING_SHEET.Document_Date,103)<='" + ToDate + "' "

            If chkCustSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                qry += " AND TSPL_LABOUR_WORKING_SHEET.Employee in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If
            qry += " Group By Document_No"

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Record Found")
            Else
                gv.DataSource = dt
                FormatGrid()
            End If
            If IsPrint = Exporter.Refresh Then

            ElseIf IsPrint = Exporter.Print Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.CommonServices, dt, "crptCForm", "C-Form Report")
                frmCRV = Nothing
            ElseIf IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("C - Form Report", gv, Nothing, Me.Text)
            Else
                clsCommon.MyExportToPDF("C- Form Report", gv, Nothing, "Shipment Detail", False)
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub FormatGrid()
        ' Dim strItemCode, head2 As String
        gv.AllowAddNewRow = False

        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Document_No").IsVisible = True
        gv.Columns("Document_No").Width = 150
        gv.Columns("Document_No").HeaderText = "Document No"

        gv.Columns("Date").IsVisible = True
        gv.Columns("Date").Width = 150
        gv.Columns("Date").HeaderText = "Date"

        gv.Columns("Operator").IsVisible = True
        gv.Columns("Operator").Width = 300

        gv.Columns("Efficiency").IsVisible = True
        gv.Columns("Efficiency").Width = 100

        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmRptVendorLedger_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'deletedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Try
            If (gv.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("No Data To Export")
                Exit Sub
            End If
            LoadData(Exporter.Excel)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Try
            If (gv.Rows.Count <= 0) Then
                common.clsCommon.MyMessageBoxShow("No Data To Export")
                Exit Sub
            End If
            LoadData(Exporter.PDF)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            LoadData(Exporter.Refresh)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        Dim DocNo As String
        ' Dim DocType As String
        DocNo = clsCommon.myCstr(gv.CurrentRow.Cells("Document_No").Value)
        Dim frm As New FrmLabourWorkingSheet
        frm.SetUserMgmt(clsUserMgtCode.frmLabourWorkingSheet)
        frm.strDocumentNo = DocNo
        frm.Show()
        frm.LoadData(DocNo, NavigatorType.Current)
    End Sub

End Class
