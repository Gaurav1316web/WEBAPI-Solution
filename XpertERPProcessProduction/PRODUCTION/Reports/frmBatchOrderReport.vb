' Created BY Panch Raj as on 10 oct 2013 @ Ticket No-[BM00000000530]-
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class frmBatchOrderReport
    Inherits FrmMainTranScreen
    Dim qry As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dtMst As DataTable
    Dim dtPP As DataTable
    Dim dtRM As DataTable
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub

    Private Sub frmBatchOrderReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.P Then
            'PrintData1()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmBatchOrderReportSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmBatchOrderReportSTD)
        ElseIf formtype = clsUserMgtCode.frmBatchOrderReportPepsi Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmBatchOrderReportPepsi)
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub frmBatchOrderReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        'chkItemAll.IsChecked = True
        'chkLocationAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        'ItemLoad()
        'LoadLocation()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R Reset the Window")
    End Sub

   
    Sub PrintData()
        Try
            dtMst = clsBatchOrder.GetBatchOrderDT(dtpFromdate1.Value, dtpToDate.Value).Copy()
            dtPP = clsBatchOrderDetail.GetProdDetail(dtpFromdate1.Value, dtpToDate.Value).Copy()
            dtRM = clsBatchOrderDetail.GetRMDetail(dtpFromdate1.Value, dtpToDate.Value).Copy()
            dtMst.AcceptChanges()
            dtPP.AcceptChanges()
            dtRM.AcceptChanges()
            SetupMasterForAutoGenerateHierarchy()

            'gv.DataSource = Nothing
            'gv.Rows.Clear()
            'gv.Columns.Clear()

            'If dtMst Is Nothing OrElse dtMst.Rows.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("No Record Found")
            'Else
            '    'If dtMst IsNot Nothing And dtMst.Rows.Count > 0 Then
            '    '    gv.DataSource = dtMst
            '    '    RadPageView1.SelectedPage = RadPageViewPage2
            '    '    SetGridFormation()
            '    'End If
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub SetupMasterForAutoGenerateHierarchy()
        Using Me.gv.DeferRefresh()
            Me.gv.AutoGenerateHierarchy = True
            Me.gv.MasterTemplate.Reset()
            Me.gv.TableElement.RowHeight = 20
            Me.gv.DataSource = dtMst

            Me.gv.MasterTemplate.Columns("Batch Date").FormatString = "{0:  dd/MMM/yyyy}"
           
            Me.gv.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill

            Dim templatePP As New GridViewTemplate()
            templatePP.DataSource = dtPP
            Me.gv.Templates.Add(templatePP)
            templatePP.AllowAddNewRow = False
            templatePP.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
            templatePP.ReadOnly = True

            Dim relmstPP As New GridViewRelation(gv.MasterTemplate, templatePP)
            relmstPP.RelationName = "BatchOrder"
            relmstPP.ParentColumnNames.Add("Batch Order No")
            relmstPP.ChildColumnNames.Add("Batch Order No")
            Me.gv.Relations.Add(relmstPP)

            Dim templateRM As New GridViewTemplate()
            templateRM.DataSource = dtRM
            templatePP.Templates.Add(templateRM)
            templateRM.AllowAddNewRow = False
            templateRM.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
            templateRM.ReadOnly = True

            Dim relmstRM As New GridViewRelation(templatePP, templateRM)
            relmstRM.RelationName = "BatchOrder"
            relmstRM.ParentColumnNames.Add("Batch Order No")
            relmstRM.ChildColumnNames.Add("Batch Order No")
            Me.gv.Relations.Add(relmstRM)


        End Using
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
            Dim str As String = "Batch Order Report"

            Dim arr As New List(Of String)()
            arr.Add("Batch Order Report")
            arr.Add("  From Date:  " + fromdate + "  To Date: " + Todate + "   ")

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcel(str, gv, arr, "Batch Order Report")
            Else
                clsCommon.MyExportToPDF(str, gv, arr, "Batch Order Report", False)
            End If
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Record Found to print.", Me.Text)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        Reset()
    End Sub
    Sub Reset()
        'chkItemAll.IsChecked = True
        'chkLocationAll.IsChecked = True
        dtpFromdate1.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        'ItemLoad()
        'LoadLocation()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnExcel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        PrintData1(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        PrintData1(EnumExportTo.PDF)
    End Sub

End Class
