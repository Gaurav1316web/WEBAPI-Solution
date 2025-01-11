Imports System.IO
Imports common
Public Class rptTankerapprovalutilization
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Sub Reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        TxtMultiRoute.arrValueMember = Nothing
        TxtMultiTanker.arrValueMember = Nothing
        btnGo.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub TxtMultiRoute__My_Click(sender As Object, e As EventArgs) Handles TxtMultiRoute._My_Click
        Try
            Dim qry As String = " select ROUTE_NO as Code,ROUTE_NAME as Name from  TSPL_BULK_ROUTE_MASTER "
            TxtMultiRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("TSultiRoute", qry, "Code", "Name", TxtMultiRoute.arrValueMember, TxtMultiRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Private Sub TxtMultiTanker__My_Click(sender As Object, e As EventArgs) Handles TxtMultiTanker._My_Click
        Try
            Dim qry As String = " select Tanker_No as Code,Tanker_Name as Name,Storage_Capacity,StorageCapacityDesc,Tanker_Transporter_Code from TSPL_TANKER_MASTER "
            TxtMultiTanker.arrValueMember = clsCommon.ShowMultipleSelectForm("MultiTanker", qry, "Code", "Name", TxtMultiTanker.arrValueMember, TxtMultiTanker.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rptTankerapprovalutilization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub
    Sub SetGridFormat()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("SNo").Name = "SNo"
        gv1.Columns("SNo").IsVisible = True
        gv1.Columns("Document_Date").HeaderText = "Document Date"
        gv1.Columns("Document_Date").Width = 250
        gv1.Columns("Document_Date").IsVisible = True
        gv1.Columns("Document_Date").FormatString = "{0:dd/MM/yyyy}"
        gv1.Columns("Tanker_No").HeaderText = "Tanker No"
        gv1.Columns("Tanker_No").Width = 500

        gv1.Columns("Route_Code").HeaderText = "Route Code"
        gv1.Columns("Route_Code").Width = 250
        gv1.Columns("Route_Code").IsVisible = True

        gv1.Columns("Qty").HeaderText = "Qty"
        gv1.Columns("Qty").Width = 500
        gv1.Columns("Qty").IsVisible = True
        gv1.Columns("Qty").FormatString = "{0:n2}"

        gv1.Columns("Storage_Capacity").HeaderText = "Capacity Approval"
        gv1.Columns("Storage_Capacity").Width = 500

        gv1.Columns("Utilization").HeaderText = "Utilization %"
        gv1.Columns("Utilization").Width = 500
        gv1.Columns("Utilization").IsVisible = True
        gv1.Columns("Utilization").FormatString = "{0:n2}"

        gv1.Columns("Difference").HeaderText = "Difference"
        gv1.Columns("Difference").Width = 500
        gv1.Columns("Difference").IsVisible = True
        gv1.Columns("Difference").FormatString = "{0:n2}"

        gv1.Columns("Flushing").HeaderText = "Flushing"
        gv1.Columns("Flushing").Width = 500
        gv1.Columns("Flushing").IsVisible = True
        gv1.Columns("Flushing").FormatString = "{0:n2}"

        gv1.Columns("FatKG").HeaderText = "Fat(KG)"
        gv1.Columns("FatKG").Width = 500
        gv1.Columns("FatKG").IsVisible = True
        gv1.Columns("FatKG").FormatString = "{0:n3}"

        gv1.Columns("SNFKG").HeaderText = "SNF(KG)"
        gv1.Columns("SNFKG").Width = 500
        gv1.Columns("SNFKG").IsVisible = True
        gv1.Columns("SNFKG").FormatString = "{0:n3}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item1 As New GridViewSummaryItem("Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Storage_Capacity", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Utilization", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Difference", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("Flushing", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("FatKG", "{0:n3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("SNFKG", "{0:n3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim Qry As String = ""
            Dim Dt As DataTable = Nothing
            Dim Whr As String = ""
            If TxtMultiRoute.arrValueMember IsNot Nothing AndAlso TxtMultiRoute.arrValueMember.Count > 0 Then
                Whr = " and Route_Code in (" + clsCommon.GetMulcallString(TxtMultiRoute.arrValueMember) + ")"
            End If
            If TxtMultiTanker.arrValueMember IsNot Nothing AndAlso TxtMultiTanker.arrValueMember.Count > 0 Then
                Whr += " and TSPL_MILK_COLLECTION_MCC.Tanker_No in (" + clsCommon.GetMulcallString(TxtMultiTanker.arrValueMember) + ")"
            End If
            Qry = " select  row_number() over(order by(select 1)) as SNo,Convert(Date,Document_Date,103)Document_Date,Tanker_No,(Route_Code)Route_Code,Sum(HeaderQty)Qty,MAX(Storage_Capacity)Storage_Capacity,(Sum(isnull(HeaderQty,0))/max(isnull((Storage_Capacity),0)))*100 as Utilization,max(isnull((Storage_Capacity),0))-(Sum(isnull(HeaderQty,0))) as Difference,sum(HeaderQty)-Sum(Qty) as Flushing,sum(TotalFATKg)-Sum(DetailFATKg) as FatKG,Sum(TotalSNFKg)-Sum(DetailSNFKg) as SNFKG from(
                SELECT 
                    TSPL_MILK_COLLECTION_MCC.Document_Date,
                    TSPL_MILK_COLLECTION_MCC.Tanker_No,
                    MAX(TSPL_MILK_COLLECTION_MCC.Route_Code) AS Route_Code,
                    SUM(ISNULL(TSPL_MILK_COLLECTION_MCC_DETAIL.Qty, 0)) AS Qty,
                    Max(ISNULL(TSPL_TANKER_MASTER.Storage_Capacity, 0)) AS Storage_Capacity,
                    (ISNULL(TSPL_MILK_COLLECTION_MCC.Entered_Qty, 0)) AS HeaderQty,
                    (ISNULL(TSPL_MILK_COLLECTION_MCC.Entered_FATKg, 0)) AS TotalFATKg,
                    SUM(ISNULL(TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG, 0)) AS DetailFATKg,
                    (ISNULL(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg, 0)) AS TotalSNFKg,
                    SUM(ISNULL(TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG, 0)) AS DetailSNFKg
                FROM TSPL_MILK_COLLECTION_MCC_DETAIL
                LEFT JOIN TSPL_MILK_COLLECTION_MCC 
                    ON TSPL_MILK_COLLECTION_MCC.Document_No = TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
                LEFT JOIN TSPL_TANKER_MASTER 
                    ON TSPL_TANKER_MASTER.Tanker_No = TSPL_MILK_COLLECTION_MCC.Tanker_No
               WHERE  convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103)>='" + clsCommon.GetPrintDate(txtFromDate.Value) + "' and convert(date,TSPL_MILK_COLLECTION_MCC.document_date,103) <='" + clsCommon.GetPrintDate(txtToDate.Value) + "' " + Whr + "
                GROUP BY 
                    TSPL_MILK_COLLECTION_MCC.Document_Date, 
                    TSPL_MILK_COLLECTION_MCC.Tanker_No,Entered_Qty,Entered_FATKg,Entered_SNFKg
            ) XX group by Document_Date,Tanker_No,Route_Code "
            Dt = clsDBFuncationality.GetDataTable(Qry)

            If Dt IsNot Nothing AndAlso Dt.Rows.Count > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = Dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next

                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTankerUtilization & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            transportSql.exportdata(gv1, "", Me.Text, , arrHeader, False, True)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = "Capacity Approval Utilization Milk (Tankers)"
            Dim arrHeader As List(Of String) = New List(Of String)()
            ' arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                transportSql.exportdata(gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
End Class