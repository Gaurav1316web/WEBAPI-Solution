' Created BY Pradeep Sharma as on 03 oct 2013 @ Ticket No-[BM00000000532]-
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmIssueReturnItemWiseReport
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
    Private Sub frmIssueReturnItemWiseReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            'PrintData1()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()

        If formtype = clsUserMgtCode.frmIssueReturnItemWiseReportSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmIssueReturnItemWiseReportSTD)
        ElseIf formtype = clsUserMgtCode.frmIssueReturnItemWiseReportPepsi Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmIssueReturnItemWiseReportPepsi)
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub frmIssueReturnItemWiseReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        ItemLoad()
        LoadLocation()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
    End Sub

    Public Sub ItemLoad()
        qry = "select item_Code as Code,Item_Desc as Name from  TSPL_ITEM_MASTER  "
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Code"
    End Sub

    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Sub PrintData()
        Try
            Dim qry As String

            qry = ""
            qry += " select TSPL_MF_ISSUE_DETAIL.ISSUE_CODE as [Issue Code],convert(varchar(12),TSPL_MF_ISSUE.ISSUE_DATE,103) as [Issue Date], TSPL_MF_RETURN_DETAIL.RETURN_CODE as [Return Code], convert(varchar(12),TSPL_MF_RETURN.RETURN_DATE,103) as [Return Date],TSPL_MF_ISSUE_DETAIL.ITEM_CODE as [Item Code],TSPL_MF_ISSUE_DETAIL.ITEM_DESCRIPTION as [Description], TSPL_MF_ISSUE_DETAIL.UNIT_CODE as [Unit], convert(decimal(10,2),TSPL_MF_ISSUE_DETAIL.ISSUE_QTY) as [Issue Qty],convert (decimal(10,2),TSPL_MF_RETURN_DETAIL.RETURN_QTY) as [Return Qty], convert (decimal(10,2),TSPL_MF_RETURN_DETAIL.WASTAGE_QTY) as [Wastage Qty], convert (decimal(10,2),TSPL_MF_RETURN_DETAIL.CONSUMED_QTY) as [Consumed Qty], "
            qry += " convert (decimal(10,2),( TSPL_MF_ISSUE_DETAIL.ISSUE_QTY - (isnull(TSPL_MF_RETURN_DETAIL.RETURN_QTY,0) + isnull(TSPL_MF_RETURN_DETAIL.WASTAGE_QTY,0) + isnull(TSPL_MF_RETURN_DETAIL.CONSUMED_QTY,0)))) as [Balance Qty]"
            qry += " from TSPL_MF_ISSUE_DETAIL "
            qry += " left outer join TSPL_MF_RETURN_DETAIL on TSPL_MF_RETURN_DETAIL.ISSUE_CODE = TSPL_MF_ISSUE_DETAIL.ISSUE_CODE and TSPL_MF_RETURN_DETAIL.ITEM_CODE = TSPL_MF_ISSUE_DETAIL.ITEM_CODE AND TSPL_MF_RETURN_DETAIL.PRODUCTION_LINE_CODE = TSPL_MF_ISSUE_DETAIL.PRODUCTION_LINE_CODE AND TSPL_MF_RETURN_DETAIL.BOM_CODE = TSPL_MF_ISSUE_DETAIL.BOM_CODE "
            qry += " left outer join TSPL_MF_ISSUE on TSPL_MF_ISSUE.ISSUE_CODE =TSPL_MF_ISSUE_DETAIL.ISSUE_CODE "
            qry += " left outer join TSPL_MF_RETURN on TSPL_MF_RETURN.RETURN_CODE =TSPL_MF_RETURN_DETAIL.RETURN_CODE "

            qry += " where 2=2 "
            qry += " and convert(date,TSPL_MF_ISSUE.ISSUE_DATE,103) >= '" + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MMM/yyyy") + "' and "
            qry += " convert(date,TSPL_MF_ISSUE.ISSUE_DATE,103) <= '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "'   "

            If chkLocationAll.IsChecked = False Then
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Location", Me.Text)
                    Return
                ElseIf cbgLocation.CheckedValue.Count > 0 Then
                    qry += " and TSPL_MF_ISSUE.LOCATION_CODE_FROM in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
            End If
            If chkItemAll.IsChecked = False Then
                If chkItemSelect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow("Me,Please select atleast one ItemCode", Me.Text)
                    Return
                ElseIf cbgItem.CheckedValue.Count > 0 Then
                    qry += "  and TSPL_MF_ISSUE_DETAIL.ITEM_CODE in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
                End If
            End If

            qry += " Order By TSPL_MF_ISSUE.ISSUE_DATE "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                If dt IsNot Nothing And dt.Rows.Count > 0 Then
                    gv.DataSource = dt
                    RadPageView1.SelectedPage = RadPageViewPage2
                    SetGridFormation()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub SetGridFormation()
        gv.ShowGroupPanel = False
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False

        gv.Columns("Issue Code").Width = 90
        gv.Columns("Issue Date").Width = 80
        gv.Columns("Return Code").Width = 90
        gv.Columns("Return Date").Width = 80
        gv.Columns("Item Code").Width = 60
        gv.Columns("Description").Width = 150
        gv.Columns("Unit").Width = 50
        gv.Columns("Issue Qty").Width = 70
        gv.Columns("Issue Qty").FormatString = "{0:n2}"
        gv.Columns("Return Qty").Width = 70
        gv.Columns("Return Qty").FormatString = "{0:n2}"
        gv.Columns("Wastage Qty").Width = 70
        gv.Columns("Wastage Qty").FormatString = "{0:n2}"
        gv.Columns("Consumed Qty").Width = 70
        gv.Columns("Consumed Qty").FormatString = "{0:n2}"
        gv.Columns("Balance Qty").Width = 70
        gv.Columns("Balance Qty").FormatString = "{0:n2}"
        gv.ReadOnly = True

        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = True
        gv.ShowFilteringRow = True
        gv.AllowDeleteRow = False
        gv.EnableAlternatingRowColor = True
        gv.Columns(0).IsCurrent = True
        gv.Focus()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintData()
    End Sub

    Sub PrintData1(ByVal exporter As EnumExportTo)
        If gv.Rows.Count > 0 Then
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim str As String = "Issue Return Item Wise Report"

            Dim arr As New List(Of String)()
            arr.Add("Issue Return Item Wise Report")
            arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "   ")

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel(str, gv, arr, "Issue Return Item Wise Report")
            Else
                clsCommon.MyExportToPDF(str, gv, arr, "Issue Return Item Wise Report", False)
            End If
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Record Found to print.", Me.Text)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        cbgItem.Enabled = Not chkItemAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSelect.ToggleStateChanged
        cbgLocation.Enabled = True
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        chkItemAll.IsChecked = True
        chkLocationAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        ItemLoad()
        LoadLocation()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    
    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        PrintData1(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        PrintData1(EnumExportTo.PDF)
    End Sub

End Class
