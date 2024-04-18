Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class GatePassReport

    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        DocumentDate.Enabled = isEnable
    End Sub

    Sub reset()
        DocumentDate.Value = clsCommon.GETSERVERDATE()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        ControlEnableDisable(True)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Public Sub LoadData()
        Try
            Dim dt As New DataTable
            Dim strQry As String = "select  TSPL_DAIRYSALE_GATEPASS_MASTER.GPCode as Gate_pass_No,TSPL_DAIRYSALE_GATEPASS_MASTER.DistributorName as Contractor_Name,TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate as Total_Crate,'' Total_Pouch,TSPL_DAIRYSALE_GATEPASS_MASTER.TotalCrate*12 as Total_Litre 
                                    from TSPL_DAIRYSALE_GATEPASS_MASTER left join tspl_route_master on tspl_route_master.route_no=TSPL_DAIRYSALE_GATEPASS_MASTER.route_no  
                                    where 2=2  and convert(date, TSPL_DAIRYSALE_GATEPASS_MASTER.gpdate ,103)=convert(date,'" & DocumentDate.Value & "',103)"
            dt = clsDBFuncationality.GetDataTable(strQry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt

                RadPageView1.SelectedPage = RadPageViewPage2

                Gv1.EnableFiltering = True
                'FormatGrid()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            For Each col As GridViewColumn In Gv1.Columns
                If col.Name.Contains("TotalCrate") = True Or col.Name.Contains("TotalCrate") = True Or col.Name.Contains("Total") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                ElseIf col.Name.Contains("Total_Liter") = True Or col.Name.Contains("Total_Liter") = True Or col.Name.Contains("Total") = True Then
                    Dim item As New GridViewSummaryItem(col.Name, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item)
                End If
            Next
            Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Dispath Ledger Report")
            arrHeader.Add("From Period : " + clsCommon.GetPrintDate(DocumentDate.Value, "dd/MM/yyyy"))
            Dim StrHeading As String = objCommonVar.CurrentCompanyName + Environment.NewLine + "Dispath Ledger Report From Period " + clsCommon.GetPrintDate(DocumentDate.Value, "dd/MM/yyyy")
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "DispathLedgerReport")
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            'clsCommon.MyOldExportToPDF(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "DispathLedgerReport")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Dispath Ledger Report")
            arrHeader.Add("From Period : " + clsCommon.GetPrintDate(DocumentDate.Value, "dd/MM/yyyy"))
            Dim StrHeading As String = objCommonVar.CurrentCompanyName + Environment.NewLine + "Dispath Ledger Report From Period " + clsCommon.GetPrintDate(DocumentDate.Value, "dd/MM/yyyy")
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            'clsCommon.MyExportToPDF("a", Gv1, arrHeader, "b", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            clsCommon.MyOldExportToPDF(objCommonVar.CurrentCompanyName, Gv1, arrHeader, "DispathLedgerReport")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptGatePassReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DocumentDate.Value = DateTime.Now()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Reset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click, BtnReset.Click
        reset()
    End Sub
End Class
