Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class rptTankerMilkQcVsERPEntry
    Inherits FrmMainTranScreen
    Private Sub rptTankerMilkQcVsERPEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            'If txtMCC.arrValueMember Is Nothing OrElse txtMCC.arrValueMember.Count <= 0 Then
            '    txtMCC.Focus()
            '    Throw New Exception("Please select MCC")
            'End If
            Dim qry As String = "Select ROUTE_NO As [Route Code], ROUTE_NAME As [Route Name] from TSPL_BULK_ROUTE_MASTER "
            'If txtMCC.arrValueMember IsNot Nothing AndAlso txtMCC.arrValueMember.Count > 0 Then
            '    qry += "  and MCC_Code in (" + clsCommon.GetMulcallString(txtMCC.arrValueMember) + ")"
            'End If

            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("@Route", qry, "Route Code", "Route Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
            ' RefreshVLC()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtTanker__My_Click(sender As Object, e As EventArgs) Handles txtTanker._My_Click
        Try
            Dim qry As String = " select Tanker_No,Tanker_Name,Description from TSPL_TANKER_MASTER "
            txtTanker.arrValueMember = clsCommon.ShowMultipleSelectForm("@Tanker", qry, "Tanker_No", "Tanker_Name", txtTanker.arrValueMember, txtTanker.arrDispalyMember)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Reset()
    End Sub
    Sub Reset()
        RadGroupBox3.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1

        'fromDate.Value = clsCommon.GETSERVERDATE()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        RadGroupBox3.Enabled = True
        ' cboDocumentType.SelectedIndex = 0
        'txtRouteCode.Value = ""
        'lblRouteCode.Text = ""
        txtTanker.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtRoute.Enabled = True
        txtTanker.Enabled = True
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False)
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try
            Dim arrRoute As ArrayList = Nothing
            Dim FinalQuery As String = Nothing
            Dim BaseQuery As String = Nothing
            Dim qry As String = Nothing
            Dim Baseqry1 As String = ""
            Dim whrcls As String = ""
            Dim whrclsD As String = ""

            whrcls = "where 2 = 2 and convert(date,Document_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,Document_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "

            End If
            If txtTanker.arrValueMember IsNot Nothing AndAlso txtTanker.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_MILK_COLLECTION_MCC.Tanker_No in (" + clsCommon.GetMulcallString(txtTanker.arrValueMember) + ")"
            End If
            BaseQuery = "WITH CTE AS
( SELECT ROW_NUMBER() OVER (
            PARTITION BY TSPL_MILK_COLLECTION_MCC.Document_No 
            ORDER BY TSPL_MILK_COLLECTION_MCC.Document_No
        ) AS RN, '" + objCommonVar.CurrentUserCode + "' as UserName,TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Comp_Code1,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,
 CONVERT(VARCHAR, TSPL_MILK_COLLECTION_MCC.Document_Date, 103) AS DocumentDate,
            (TSPL_MILK_COLLECTION_MCC.Temp) AS Temp,(TSPL_MILK_COLLECTION_MCC.Trip_No) AS Trip_No,TSPL_MILK_COLLECTION_MCC.Route_Code,
        TSPL_MILK_COLLECTION_MCC.Tanker_No,(TSPL_MILK_COLLECTION_MCC.Entered_Qty) AS Entered_Qty,(TSPL_MILK_COLLECTION_MCC.Entered_FATKg) AS Entered_FATKg,(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg) AS Entered_SNFKg,(CASE WHEN TSPL_MILK_COLLECTION_MCC.Entered_Qty = 0 THEN 0
                 ELSE CAST((TSPL_MILK_COLLECTION_MCC.Entered_SNFKg * 100.0) / TSPL_MILK_COLLECTION_MCC.Entered_Qty AS DECIMAL(10, 2)) END) AS Entered_FATPER,
        (CASE WHEN TSPL_MILK_COLLECTION_MCC.Entered_Qty = 0 THEN 0
                 ELSE CAST((TSPL_MILK_COLLECTION_MCC.Entered_FATKg * 100.0) / TSPL_MILK_COLLECTION_MCC.Entered_Qty AS DECIMAL(10, 2)) END) AS Entered_SNFPER
,   ISNULL((TSPL_MILK_COLLECTION_MCC_DETAIL.API_FATKG),0) AS QCFATKG,ISNULL((TSPL_MILK_COLLECTION_MCC_DETAIL.API_SNFKG),0) AS QCSNFKG,
     	   ISNULL( CAST(        ROUND(CAST((TSPL_MILK_COLLECTION_MCC_DETAIL.API_FATKG) AS DECIMAL(18, 3)) * 100.0 / NULLIF((TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty), 0),
            2
        ) AS DECIMAL(18, 2)
    ),0) AS QCFATPER,
 
    ISNULL(  CAST(
        ROUND(
            CAST((TSPL_MILK_COLLECTION_MCC_DETAIL.API_SNFKG) AS DECIMAL(18, 3)) * 100.0 / NULLIF((TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty), 0),
            2
        ) AS DECIMAL(18, 2)
    ) ,0)AS QCSNFPER,
    (TSPL_MILK_COLLECTION_MCC.Entered_FATKg 
     - ISNULL(TSPL_MILK_COLLECTION_MCC_DETAIL.API_FATKG, 0)
    ) AS DIFFFATKG,
	   (TSPL_MILK_COLLECTION_MCC.Entered_SNFKg 
     - ISNULL(TSPL_MILK_COLLECTION_MCC_DETAIL.API_SNFKG, 0)
    ) AS DIFFSNFKG
    FROM TSPL_MILK_COLLECTION_MCC 
    LEFT JOIN TSPL_MILK_COLLECTION_MCC_DETAIL 
        ON TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_MCC.Document_No
    LEFT JOIN TSPL_TANKER_MASTER TM
        ON TM.Tanker_No = TSPL_MILK_COLLECTION_MCC.Tanker_No 
Left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code1 = '" & objCommonVar.CurrComp_Code1 & "'
	" + whrcls + "
	)

SELECT ROW_NUMBER() OVER (ORDER BY DocumentDate, Route_Code) AS S_No,*
FROM CTE
WHERE RN = 1;
"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQuery)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                SetGridFormationOFGV1Collection()
                EnableDisableCntrl(False)
                View()
                Gv1.BestFitColumns()
                If print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(False, CrystalReportFolder.MilkProcurement, dt, "rptTankerMilkQcVsERPEntry", "Tanker Milk Qc Vs ERP Entry")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub EnableDisableCntrl(ByVal val As Boolean)
        RadGroupBox3.Enabled = False
        txtRoute.Enabled = False
        txtTanker.Enabled = False

    End Sub
    Sub View()
        If Gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("S_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("DocumentDate").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Temp").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Trip_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Route_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(Gv1.Columns("Tanker_No").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("As per Truck Sheet"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATPER").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_FATKg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFPER").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(Gv1.Columns("Entered_SNFKg").Name)


            view.ColumnGroups.Add(New GridViewColumnGroup("As per QC"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("QCFATPER").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("QCFATKG").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("QCSNFPER").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(Gv1.Columns("QCSNFKG").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DIFFFATKG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(Gv1.Columns("DIFFSNFKG").Name)

            Gv1.ViewDefinition = view
        End If
    End Sub
    Sub SetGridFormationOFGV1Collection()
        Gv1.AutoExpandGroups = False
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = True
        Gv1.ShowFilteringRow = True
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns("RN").IsVisible = False
            Gv1.Columns("UserName").IsVisible = False

            Gv1.Columns("Comp_Name").IsVisible = False
            Gv1.Columns("Comp_Code1").IsVisible = False
            Gv1.Columns("Add1").IsVisible = False
            Gv1.Columns("Add2").IsVisible = False
            Gv1.Columns("Add3").IsVisible = False
            Gv1.Columns("Logo_Img").IsVisible = False

            Gv1.Columns("S_No").IsVisible = True
            Gv1.Columns("S_No").HeaderText = "SNo"

            Gv1.Columns("DocumentDate").IsVisible = True
            Gv1.Columns("DocumentDate").HeaderText = "Document Date"
            Gv1.Columns("Route_Code").IsVisible = True
            Gv1.Columns("Route_Code").HeaderText = "ROUTE"
            Gv1.Columns("Tanker_No").IsVisible = True
            Gv1.Columns("Tanker_No").HeaderText = "Tanker No"
            Gv1.Columns("Trip_No").IsVisible = True
            Gv1.Columns("Trip_No").HeaderText = "Trip No"
            '
            Gv1.Columns("Entered_Qty").IsVisible = False
            Gv1.Columns("Entered_Qty").HeaderText = "BMC Qty"

            Gv1.Columns("Entered_FATKg").IsVisible = True
            Gv1.Columns("Entered_FATKg").HeaderText = "FAT(Kg)"

            Gv1.Columns("Entered_SNFKg").IsVisible = True
            Gv1.Columns("Entered_SNFKg").HeaderText = "SNF(Kg)"

            Gv1.Columns("Entered_FATPER").IsVisible = True
            Gv1.Columns("Entered_FATPER").HeaderText = "SNF(%)"

            Gv1.Columns("Entered_SNFPER").IsVisible = True
            Gv1.Columns("Entered_SNFPER").HeaderText = "FAT(%)"

            Gv1.Columns("QCFATPER").IsVisible = True
            Gv1.Columns("QCFATPER").HeaderText = "FAT(%)"
            Gv1.Columns("QCFATKG").IsVisible = True
            Gv1.Columns("QCFATKG").HeaderText = "FAT(Kg)"
            Gv1.Columns("QCSNFPER").IsVisible = True
            Gv1.Columns("QCSNFPER").HeaderText = "SNF(%)"
            Gv1.Columns("QCSNFKG").IsVisible = True
            Gv1.Columns("QCSNFKG").HeaderText = "SNF(Kg)"


            Gv1.Columns("DIFFSNFKG").IsVisible = True
            Gv1.Columns("DIFFSNFKG").HeaderText = "Fat KG"
            Gv1.Columns("QCSNFPER").IsVisible = True
            Gv1.Columns("QCSNFPER").HeaderText = "SNF KG"

        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim Entered_FATKg As New GridViewSummaryItem("Entered_FATKg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Entered_FATKg)
        Dim Entered_SNFKg As New GridViewSummaryItem("Entered_SNFKg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Entered_SNFKg)
        Dim Entered_FATPER As New GridViewSummaryItem("Entered_FATPER", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Entered_FATPER)
        Dim Entered_SNFPER As New GridViewSummaryItem("Entered_SNFPER", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Entered_SNFPER)
        Dim QCFATPER As New GridViewSummaryItem("QCFATPER", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(QCFATPER)
        Dim QCSNFPER As New GridViewSummaryItem("QCSNFPER", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(QCSNFPER)
        Dim QCSNFKG As New GridViewSummaryItem("QCSNFKG", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(QCSNFKG)
        Dim QCFATKG As New GridViewSummaryItem("QCFATKG", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(QCFATKG)

        Dim DIFFFATKG As New GridViewSummaryItem("DIFFFATKG", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(DIFFFATKG)
        Dim DIFFSNFKG As New GridViewSummaryItem("DIFFSNFKG", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(DIFFSNFKG)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Griddata(True)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptTankerMilkQcVsERPEntry & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))

                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add("Route : " & txtRoute.arrDispalyMember(0))
                End If

                If txtTanker.arrValueMember IsNot Nothing AndAlso txtTanker.arrValueMember.Count > 0 Then
                    arrHeader.Add("Tanker : " & txtTanker.arrDispalyMember(0))

                End If
                If exporter = EnumExportTo.Excel Then
                    transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, False, True)
                End If
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class