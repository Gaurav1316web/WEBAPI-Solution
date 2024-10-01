Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmShowInvoiceNo

    Public Query As String
    Public FilterColumn As String
    Private Sub frmShowInvoiceNo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ShowInvoiceNo()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub ShowInvoiceNo()
        Try
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dt IsNot Nothing OrElse dt.Rows.Count > 0 Then

                gvShowInvoiceNo.DataSource = Nothing
                gvShowInvoiceNo.Rows.Clear()
                gvShowInvoiceNo.Columns.Clear()
                gvShowInvoiceNo.GroupDescriptors.Clear()
                gvShowInvoiceNo.MasterTemplate.SummaryRowsBottom.Clear()
                gvShowInvoiceNo.MasterView.Refresh()
                gvShowInvoiceNo.DataSource = dt
                SetGridFormat()
                gvShowInvoiceNo.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        gvShowInvoiceNo.AllowAddNewRow = False
        gvShowInvoiceNo.AllowDeleteRow = False
        gvShowInvoiceNo.EnableFiltering = True
        gvShowInvoiceNo.ShowFilteringRow = True
        gvShowInvoiceNo.ShowGroupPanel = False
        For ii As Integer = 0 To gvShowInvoiceNo.Columns.Count - 1
            gvShowInvoiceNo.Columns(ii).ReadOnly = True
            gvShowInvoiceNo.Columns(ii).BestFit()
        Next

        If clsCommon.CompairString(FilterColumn, "Route_No") = CompairStringResult.Equal Then
            gvShowInvoiceNo.Columns("Route_No").HeaderText = "Route Code"
            gvShowInvoiceNo.Columns("Route_Desc").HeaderText = "Route Name"
        ElseIf clsCommon.CompairString(FilterColumn, "Cust_Code") = CompairStringResult.equal Then
            gvShowInvoiceNo.Columns("Cust_Code").HeaderText = "Customer Code"
            gvShowInvoiceNo.Columns("Customer_Name").HeaderText = "Customer Name"
        ElseIf clsCommon.CompairString(FilterColumn, "Zone_Code") = CompairStringResult.equal Then
            gvShowInvoiceNo.Columns("Zone_Code").HeaderText = "Zone Code"
        End If
        gvShowInvoiceNo.Columns("Sale_Invoice_No").HeaderText = "Invoice No"
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gvShowInvoiceNo.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                transportSql.applyExportTemplate(gvShowInvoiceNo, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid(Me.Text, gvShowInvoiceNo, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            If gvShowInvoiceNo.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                transportSql.applyExportTemplate(gvShowInvoiceNo, PageSetupReport_ID)
                clsCommon.MyExportToPDF(Me.Text, gvShowInvoiceNo, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class