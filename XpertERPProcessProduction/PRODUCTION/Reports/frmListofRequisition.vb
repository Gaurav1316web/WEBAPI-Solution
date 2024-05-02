' Created BY Pradeep Sharma as on 03 oct 2013 @ Ticket No-[BM00000000532]-
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmListofRequisition
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub frmListofRequisition_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            'PrintData1()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmListofRequisition)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnRefresh.Visible = MyBase.isPrintFlag

    End Sub

    Private Sub frmListofRequisition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkLocationAll.IsChecked = True
        chkBOAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        LoadBatchOrder()
        LoadLocation()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
    End Sub

    Public Sub LoadBatchOrder()
        Dim qry1 As String = " Select BO_CODE as [Code], DESCRIPTION as [Description],convert(varchar(12),BO_DATE,103) AS [Date], POSTED AS [Is Posted], convert(varchar(12),POSTING_DATE,103) AS [Posting Date],APPROVED_BY AS [Approved By] from TSPL_MF_BATCH_ORDER WHERE POSTED=1"
        cbgBatchOrder.DataSource = clsDBFuncationality.GetDataTable(qry1)
        cbgBatchOrder.ValueMember = "Code"
        cbgBatchOrder.DisplayMember = "Description"
        cbgBatchOrder.MyShowHeadrText = True
    End Sub

    Public Sub LoadLocation()
        Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(Qry)
        cbgLocation.ValueMember = "Code"
    End Sub

    Sub RefreshData()
        Try
            Dim qry As String

            qry = ""

            If chkSum.IsChecked = True Then
                qry += " SELECT TSPL_MF_REQUISITION.REQ_CODE as [Req.Code], convert(varchar(12),TSPL_MF_REQUISITION.REQ_DATE,103) as [Req. Date], "
                qry += " (select SUM(TSPL_MF_REQ_DETAIL.BATCH_QTY) as [BATCH_QTY] from TSPL_MF_REQ_DETAIL where TSPL_MF_REQ_DETAIL.REQ_CODE = TSPL_MF_REQUISITION.REQ_CODE ) as [Total Batch Qty] ,"
                qry += " (select SUM(TSPL_MF_REQ_DETAIL.REQ_QTY  ) as [REQ_QTY] from TSPL_MF_REQ_DETAIL where TSPL_MF_REQ_DETAIL.REQ_CODE = TSPL_MF_REQUISITION.REQ_CODE ) as [Total Req. Qty], "
                qry += " TSPL_MF_REQUISITION.DESCRIPTION as [Description],TSPL_MF_REQUISITION.COMMENTS as [Comments],TSPL_LOCATION_MASTER.Location_Desc as [Location],"
                qry += " TSPL_EMPLOYEE_MASTER.Emp_Name as [Requested By], "
                qry += " TSPL_MF_REQUISITION.POSTED as [Is POSTED], convert(varchar(12),TSPL_MF_REQUISITION.POSTING_DATE,103) as [Posting Date] "
                qry += " FROM TSPL_MF_REQUISITION "
                qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_MF_REQUISITION.LOCATION_CODE "
                qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_MF_REQUISITION.REQUESTED_BY"
            Else
                qry += " SELECT TSPL_MF_REQUISITION.REQ_CODE as [Req.Code], convert(varchar(12),TSPL_MF_REQUISITION.REQ_DATE,103) as [Req. Date],  "
                qry += " TSPL_MF_REQ_DETAIL.BO_CODE as [Batch Order], TSPL_MF_REQ_DETAIL.BOM_CODE as [BOM Code],TSPL_MF_REQ_DETAIL.ITEM_CODE as [Item Code],TSPL_MF_REQ_DETAIL.ITEM_DESCRIPTION as [Item Description], TSPL_MF_REQ_DETAIL.BATCH_QTY as [Batch Qty],TSPL_MF_REQ_DETAIL.REQ_QTY as [Req. Qty],TSPL_MF_REQ_DETAIL.UNIT_CODE as [Unit Code],TSPL_MF_REQ_DETAIL.REMARKS as [Remarks],"
                qry += " TSPL_MF_REQUISITION.DESCRIPTION AS [Description], TSPL_MF_REQUISITION.COMMENTS as [Comments],TSPL_LOCATION_MASTER.Location_Desc as [Location],"
                qry += " TSPL_EMPLOYEE_MASTER.Emp_Name as [Requested By]  "
                qry += " FROM TSPL_MF_REQUISITION "
                qry += " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code = TSPL_MF_REQUISITION.LOCATION_CODE "
                qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE = TSPL_MF_REQUISITION.REQUESTED_BY"
                qry += " LEFT OUTER JOIN TSPL_MF_REQ_DETAIL ON TSPL_MF_REQ_DETAIL.REQ_CODE = TSPL_MF_REQUISITION .REQ_CODE "
            End If

            qry += " where 2=2 "
            qry += " and convert(date,TSPL_MF_REQUISITION.REQ_DATE,103) >= '" + clsCommon.GetPrintDate(dtpFromdate1.Value, "dd/MMM/yyyy") + "' and "
            qry += " convert(date,TSPL_MF_REQUISITION.REQ_DATE,103) <= '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "'   "
            If chkBOAll.IsChecked = False Then
                If chkBOSelect.IsChecked = True AndAlso cbgBatchOrder.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Batch Order")
                    Return
                ElseIf cbgBatchOrder.CheckedValue.Count > 0 Then
                    If chkSum.IsChecked = True Then
                        qry += " and TSPL_MF_REQUISITION.REQ_CODE in  (Select distinct REQ_CODE  from TSPL_MF_REQ_DETAIL where BO_CODE in (" + clsCommon.GetMulcallString(cbgBatchOrder.CheckedValue) + "))"
                    Else
                        qry += " and TSPL_MF_REQ_DETAIL.BO_CODE in  (" + clsCommon.GetMulcallString(cbgBatchOrder.CheckedValue) + ")"
                    End If
                End If
            End If
            If chkLocationAll.IsChecked = False Then
                If chkLocationSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one Location")
                    Return
                ElseIf cbgLocation.CheckedValue.Count > 0 Then
                    qry += "  and TSPL_MF_REQUISITION.LOCATION_CODE in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
                End If
            End If
            qry += " Order By TSPL_MF_REQUISITION.REQ_DATE "

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

        gv.Columns("Req.Code").Width = 90
        gv.Columns("Description").Width = 200
        gv.Columns("Req. Date").Width = 90
        gv.Columns("Comments").Width = 200
        gv.Columns("Location").Width = 150
        gv.Columns("Requested By").Width = 120
        gv.ReadOnly = True


        If chkSum.IsChecked = True Then
            gv.Columns("Is POSTED").Width = 70
            gv.Columns("Posting Date").Width = 70
            gv.Columns("Total Batch Qty").Width = 70
            gv.Columns("Total Batch Qty").WrapText = True
            gv.Columns("Total Batch Qty").FormatString = "{0:N2}"
            gv.Columns("Total Req. Qty").Width = 70
            gv.Columns("Total Req. Qty").WrapText = True
            gv.Columns("Total Req. Qty").FormatString = "{0:N2}"

        Else
            gv.Columns("Batch Order").Width = 70
            gv.Columns("BOM Code").Width = 70
            gv.Columns("Item Code").Width = 70
            gv.Columns("Item Description").Width = 150
            gv.Columns("Batch Qty").Width = 70
            gv.Columns("Batch Qty").FormatString = "{0:N2}"
            gv.Columns("Req. Qty").Width = 70
            gv.Columns("Req. Qty").FormatString = "{0:N2}"
            gv.Columns("Unit Code").Width = 70
            gv.Columns("Remarks").Width = 150
        End If

        
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = True
        gv.ShowFilteringRow = True
        gv.AllowDeleteRow = False
        gv.EnableAlternatingRowColor = True
        gv.Columns(0).IsCurrent = True
        gv.Focus()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        RefreshData()
    End Sub

    Sub PrintData1(ByVal exporter As EnumExportTo)
        If gv.Rows.Count > 0 Then
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim str As String = "List of Requisition"

            Dim arr As New List(Of String)()
            arr.Add("List of Requisition")
            arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "   ")

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel(str, gv, arr, "List of Requisition")
            Else
                clsCommon.MyExportToPDF(str, gv, arr, "List of Requisition", False)
            End If
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Record Found to print.", Me.Text)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub chkItemAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocationAll.IsChecked
    End Sub
    Private Sub chkLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBOAll.ToggleStateChanged
        cbgBatchOrder.Enabled = False
    End Sub
    Private Sub chkLocationSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBOSelect.ToggleStateChanged
        cbgBatchOrder.Enabled = True
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        chkLocationAll.IsChecked = True
        chkBOAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        LoadBatchOrder()
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
