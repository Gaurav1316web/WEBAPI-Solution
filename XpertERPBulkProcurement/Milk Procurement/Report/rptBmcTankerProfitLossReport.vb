Imports common
Imports System.IO
Public Class rptBmcTankerProfitLossReport
    Inherits FrmMainTranScreen


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False, False)
    End Sub
    Public Sub Griddata(ByVal print As Boolean, ByVal print2 As Boolean)
        Try
            Dim arrRoute As ArrayList = Nothing

            Dim FinalQuery As String = Nothing
            Dim BaseQuery As String = Nothing
            Dim qry As String = Nothing
            Dim whrcls As String = ""
            whrcls = "where 2 = 2 and convert(date,TSPL_MILK_COLLECTION_MCC.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,tspl_Milk_collection_MCC.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_MILK_COLLECTION_MCC.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")  "

            End If
            If txtTanker.arrValueMember IsNot Nothing AndAlso txtTanker.arrValueMember.Count > 0 Then
                whrcls += " and TSPL_MILK_COLLECTION_MCC.Tanker_No in (" + clsCommon.GetMulcallString(txtTanker.arrValueMember) + ")"
            End If
            BaseQuery = " select max(TSPL_TANKER_MASTER.Description)Description,max(TSPL_MILK_COLLECTION_MCC.Temp)Temp,MAX(TSPL_MILK_COLLECTION_MCC.Trip_No)Trip_No,MAX(TSPL_MILK_COLLECTION_MCC.Tanker_No)Tanker_No,MAX(TSPL_MILK_COLLECTION_MCC.Route_Code)Route_Code,
MAX(TSPL_MILK_COLLECTION_MCC.Entered_Qty) as QTY,
MAX(TSPL_MILK_COLLECTION_MCC.Entered_FATKg) AS [FAT(Kg)],
max(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg)Entered_SNFKg,

MAX(CASE 
        WHEN TSPL_MILK_COLLECTION_MCC.Entered_Qty = 0 THEN 0 
        ELSE CAST((TSPL_MILK_COLLECTION_MCC.Entered_SNFKg * 100) / TSPL_MILK_COLLECTION_MCC.Entered_Qty AS DECIMAL(10, 2))
    END) AS Entered_Qty_snf,

    MAX(CASE 
        WHEN TSPL_MILK_COLLECTION_MCC.Entered_Qty = 0 THEN 0 
        ELSE CAST((TSPL_MILK_COLLECTION_MCC.Entered_FATKg * 100) / TSPL_MILK_COLLECTION_MCC.Entered_Qty AS DECIMAL(10, 2))
    END) AS Entered_Qty_fat,


sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty) AS Original_Qty,
sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_FATKg)Original_FATKg,
sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_SNFKg)Original_SNFKg,


   SUM(CASE 
        WHEN TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty = 0 THEN 0 
        ELSE CAST((TSPL_MILK_COLLECTION_MCC_DETAIL.Original_SNFKg * 100) / TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty AS DECIMAL(10, 2))
    END) AS [SNF(%)],

    SUM(CASE 
        WHEN TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty = 0 THEN 0 
        ELSE CAST((TSPL_MILK_COLLECTION_MCC_DETAIL.Original_FATKg * 100) / TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty AS DECIMAL(10, 2))
    END) AS [FAT(%)],


sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty) -max(TSPL_MILK_COLLECTION_MCC.Entered_Qty) AS FLUSING,
 -(sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_FATKg)-max(TSPL_MILK_COLLECTION_MCC.Entered_FATKg)) AS FATKG,
 -(sum(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_SNFKg)-max(TSPL_MILK_COLLECTION_MCC.Entered_SNFKg)) AS SNFKG
from TSPL_MILK_COLLECTION_MCC 
LEFT OUTER JOIN TSPL_MILK_COLLECTION_MCC_DETAIL ON TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_MCC.Document_No
left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No=TSPL_MILK_COLLECTION_MCC.Tanker_No
" & whrcls & " GROUP BY TSPL_MILK_COLLECTION_MCC.Document_No,Document_Date"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(BaseQuery)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                'SetGridFormat1()
                SetGridFormationOFGV1Collection()
                View()
                gv1.BestFitColumns()
                'If print = True Then
                'Dim frmCRV As New frmCrystalReportViewer()
                'frmCRV.funreport(False, CrystalReportFolder.MilkProcurement, dt, "rptTankerGainLossReport", "Tanker Gain Loss Report")
                'frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtsub, "rptTankerGainLossReport", "ProfitLoss", "SubTankerProfitLoss.rpt")

                ' frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, "", "rptTankerProfitLoss", "ProfitLoss", "SubTankerProfitLoss.rpt")
                'End If
                'If print2 = True Then
                '    Dim frmCRV As New frmCrystalReportViewer()
                '    frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, dtsub, "rptTankerProfitLossPrint2", "ProfitLoss", "SubTankerProfitLoss.rpt")
                'End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1Collection()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."
            gv1.Columns("Trip_No").IsVisible = True
            gv1.Columns("Trip_No").HeaderText = "Trip No"
            gv1.Columns("ROUTE_CODE").IsVisible = True
            gv1.Columns("ROUTE_CODE").HeaderText = "ROUTE CODE"
            gv1.Columns("Tanker_No").IsVisible = True
            gv1.Columns("Tanker_No").HeaderText = "Tanker No"

            gv1.Columns("Qty").IsVisible = True
            gv1.Columns("Qty").HeaderText = "Qty"
            gv1.Columns("Entered_snfKg").IsVisible = True
            gv1.Columns("Entered_snfKg").HeaderText = "Snf(Kg)"

            gv1.Columns("Fat(Kg)").IsVisible = True
            gv1.Columns("Fat(Kg)").HeaderText = "Fat(Kg)"
            'gv1.Columns("ROUTE_NAME").HeaderText = "Route Description"
            'gv1.Columns("Tanker_No").HeaderText = "Tanker No."


            gv1.Columns("Entered_qty_snf").IsVisible = True
            gv1.Columns("Entered_qty_snf").HeaderText = "SNF%"

            gv1.Columns("Entered_qty_fat").IsVisible = True
            gv1.Columns("Entered_qty_fat").HeaderText = "FAT%"
            '
            gv1.Columns("Original_Qty").IsVisible = True
            gv1.Columns("Original_Qty").HeaderText = "Qty"

            gv1.Columns("Original_FATKg").IsVisible = True
            gv1.Columns("Original_FATKg").HeaderText = "FATKG"

            gv1.Columns("Original_SNFKg").IsVisible = True
            gv1.Columns("Original_SNFKg").HeaderText = "SNFKG"

            gv1.Columns("SNF(%)").IsVisible = True

            gv1.Columns("SNF(%)").HeaderText = "SNF(%)"
            gv1.Columns("FAT(%)").IsVisible = True

            gv1.Columns("FAT(%)").HeaderText = "FAT(%)"

            gv1.Columns("FLUSING").IsVisible = True
            gv1.Columns("FLUSING").HeaderText = "Flushing"
            gv1.Columns("FATKG").IsVisible = True
            gv1.Columns("FATKG").HeaderText = "FATKG"
            gv1.Columns("SNFKG").IsVisible = True
            gv1.Columns("SNFKG").HeaderText = "SNFKG"

            gv1.Columns("Description").IsVisible = True
            gv1.Columns("Description").HeaderText = "Transporter"
            gv1.Columns("Temp").IsVisible = True
            gv1.Columns("Temp").HeaderText = "Temp"
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        'Dim MilkTypeB As New GridViewSummaryItem("Payable_Amount", "{0:n0}", GridAggregateFunction.Sum)
        Dim QTY As New GridViewSummaryItem("QTY", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(QTY)
        Dim Entered_SNFKg As New GridViewSummaryItem("Entered_SNFKg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Entered_SNFKg)
        Dim Entered_Qty_snf As New GridViewSummaryItem("Entered_Qty_snf", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Entered_Qty_snf)
        Dim Entered_Qty_fat As New GridViewSummaryItem("Entered_Qty_fat", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Entered_Qty_fat)
        Dim Original_Qty As New GridViewSummaryItem("Original_Qty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Original_Qty)
        Dim Original_FATKg As New GridViewSummaryItem("Original_FATKg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Original_FATKg)
        Dim Original_SNFKg As New GridViewSummaryItem("Original_SNFKg", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(Original_SNFKg)
        Dim FLUSING As New GridViewSummaryItem("FLUSING", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(FLUSING)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Sub View()
        If gv1.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()
            view.ColumnGroups.Add(New GridViewColumnGroup(" "))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            'If rdbDetails.Checked = True Then
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Trip_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("ROUTE_CODE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tanker_No").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Description").Name)

            'view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns("Tanker_No").Name)
            'End If

            '  Entered_Qty , Entered_FAT,Entered_SNF,Entered_FATKg,Entered_SNFKg
            ' DiffEnteredVsMCC_Qty, DiffEnteredVsMCC_FAT,DiffEnteredVsMCC_SNF,DiffEnteredVsMCC_FATKG,DiffEnteredVsMCC_SNFKG
            'If rdbSummary.Checked = True Then
            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Recieved at B.M.C. Q.C. LAB(A)"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())



            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Original_Qty").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Original_FATKg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Original_SNFKg").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("SNF(%)").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("FAT(%)").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns("Temp").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Milk Recieved at MAIN Q.C. LAB(B)"))
            view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("QTY").Name)

            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("FAT(Kg)").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Entered_SNFKg").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Entered_Qty_snf").Name)
            view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Entered_Qty_fat").Name)
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Original_Qty").Name)
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Original_FATKg").Name)
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Original_SNFKg").Name)
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("SNF(%)").Name)
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("FAT(%)").Name)
            'view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns("Temp").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Difference(A-B)"))
            view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("FLUSING").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("FATKG").Name)
            view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns("SNFKG").Name)

            'End If
            gv1.ViewDefinition = view
        End If
    End Sub
    Private Sub rptBmcTankerProfitLossReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub

    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Try
            Dim qry As String = " select Tanker_No from TSPL_TANKER_MASTER where Tanker_No like '%" + txtTankerNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    txtTankerNo.Value = clsCommon.myCstr(dt.Rows(0)("Tanker_No"))
                End If
            End If
            txtTankerNo.Value = clsfrmTankerMaster.GetFinder("", txtTankerNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub TxtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtRouteNo._MYValidating
        Try
            Dim qry As String = "select ROUTE_NO as Code,ROUTE_NAME from TSPL_BULK_ROUTE_MASTER "
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("ROTFND54", qry)
            If dr IsNot Nothing Then
                TxtRouteNo.Value = clsCommon.myCstr(dr("code"))
            End If
        Catch ex As Exception

        End Try
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        ' cboDocumentType.SelectedIndex = 0
        'txtRouteCode.Value = ""
        'lblRouteCode.Text = ""
        txtTanker.arrValueMember = Nothing
        txtRoute.arrValueMember = Nothing
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Print Date (" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") + ")")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBmcTankerProfitLossReport & "'"))
                arrHeader.Add("Date Range : " & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "  To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))

                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add("Route : " & txtRoute.arrDispalyMember(0))
                End If

                If txtTanker.arrValueMember IsNot Nothing AndAlso txtTanker.arrValueMember.Count > 0 Then
                    arrHeader.Add("Tanker : " & txtTanker.arrDispalyMember(0))

                End If

                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                Throw New Exception("No data found to export.")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class