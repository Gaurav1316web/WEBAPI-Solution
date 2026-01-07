Imports common
Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net
Imports XpertERPEngine
Imports System.Text.RegularExpressions
Public Class ZoneWiseReport
    Inherits FrmMainTranScreen
    Private Sub ZoneWiseReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MM/yyyy")
        If rbtnBoth.Checked Then
            btnPrint.Visible = True
        Else
            btnPrint.Visible = False
        End If

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Griddata(False)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Griddata(ByVal print As Boolean)
        Try
            RadGroupBox3.Enabled = False
            Dim arrRoute As ArrayList = Nothing
            Dim Value As String
            Dim whrcls As String = Nothing
            If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
                whrcls += " WHERE XX.Zone_Code in (" + clsCommon.GetMulcallString(TxtZone.arrValueMember) + ")"
            End If
            Dim BaseQuery As String = Nothing
            BaseQuery = " select '" + fromDate.Value + "' AS FROMDATE,'" + ToDate.Value + "' AS TODATE, '" + objCommonVar.CurrentUserCode + "' as UserName, max(xx.Document_No)documnet_no , max(xx.Document_Date)Document_Date ,XX.Zone_Code, "
            If rbtnDock.Checked Then
                BaseQuery += "sum(xx.QTYUploader)QTYUploader,
(sum(xx.QTYUploader)) as TotalQty,"
            ElseIf rdbtnBMC.Checked Then
                BaseQuery += "sum(xx.QTYSHIFT)QTYSHIFT,
(sum(xx.QTYSHIFT)) as TotalQty,"
            Else
                BaseQuery += "sum(xx.QTYUploader)QTYUploader,
sum(xx.QTYSHIFT)QTYSHIFT
,(sum(xx.QTYUploader)+sum(xx.QTYSHIFT)) as TotalQty,"
            End If
            BaseQuery +=""

            '            BaseQuery += "sum(xx.QTYUploader)QTYUploader,
            'sum(xx.QTYSHIFT)QTYSHIFT
            ',(sum(xx.QTYUploader)+sum(xx.QTYSHIFT)) as TotalQty

            BaseQuery += "MAX(TSPL_COMPANY_MASTER.Comp_Name)Comp_Name ,MAX(TSPL_COMPANY_MASTER.Add1)Add1,MAX(TSPL_COMPANY_MASTER.Add2)Add2
from
(select  TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_No,TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_Date
,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as [QTYUploader],0 as [QTYSHIFT],

TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.VLC_Code,TSPL_VENDOR_MASTER.Zone_Code
,'' AS Comp_Name ,'' AS Add1 ,'' AS Add2
from TSPL_MILK_PROCUREMENT_UPLOADER_detail
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_head  on TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_detail.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.VLC_Code  
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
WHERE convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) 

--where TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_No='BMC/0000010000000064'
union all
select TSPL_MILK_SHIFT_UPLOADER_head.Document_No,TSPL_MILK_SHIFT_UPLOADER_head.Shift_Date as Document_Date ,
0 as [QTYUploader],TSPL_MILK_SHIFT_UPLOADER_detail.Milk_Weight as [QTYSHIFT]


,TSPL_MILK_SHIFT_UPLOADER_detail.VLC_Code,TSPL_VENDOR_MASTER.Zone_Code
,'' AS Comp_Name ,'' AS Add1 ,'' AS Add2
from TSPL_MILK_SHIFT_UPLOADER_detail
left outer join TSPL_MILK_SHIFT_UPLOADER_head on TSPL_MILK_SHIFT_UPLOADER_head.Document_No=TSPL_MILK_SHIFT_UPLOADER_detail.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
where   convert(date,TSPL_MILK_SHIFT_UPLOADER_head.Shift_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_MILK_SHIFT_UPLOADER_head.Shift_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) 
--where TSPL_MILK_SHIFT_UPLOADER_head.Document_No='BMC/0000010000000064'
)xx left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
" + whrcls + "
GROUP BY Zone_Code"

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
                'SetGridFormat1()
                SetGridFormationOFGV1Collection()
                EnableDisableCntrl(False)
                Gv1.BestFitColumns()
                If print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(False, CrystalReportFolder.MilkProcurement, dt, "rptZoneWiseReport", "Zone Wise Report")
                End If
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
    Sub EnableDisableCntrl(ByVal val As Boolean)
        RadGroupBox1.Enabled = False
        TxtZone.Enabled = False
        RadGroupBox3.Enabled = False

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
            Gv1.Columns("UserName").IsVisible = False
            Gv1.Columns("FROMDATE").IsVisible = False
            Gv1.Columns("TODATE").IsVisible = False
            Gv1.Columns("Zone_Code").IsVisible = True
            Gv1.Columns("Zone_Code").HeaderText = "Zone Name"
            Gv1.Columns("documnet_no").IsVisible = False
            Gv1.Columns("documnet_no").HeaderText = "Documnet No"
            Gv1.Columns("Document_Date").IsVisible = False
            Gv1.Columns("Document_Date").HeaderText = "Document Date"
            If rbtnDock.Checked Then
                Gv1.Columns("QTYUploader").IsVisible = True
                Gv1.Columns("QTYUploader").HeaderText = "DOCK(Quantity in KG)"
            ElseIf rdbtnBMC.Checked Then

                Gv1.Columns("QTYSHIFT").IsVisible = True
                Gv1.Columns("QTYSHIFT").HeaderText = "BMC(Quantity in Kg)"
            Else
                Gv1.Columns("QTYUploader").IsVisible = True
                Gv1.Columns("QTYUploader").HeaderText = "DOCK(Quantity in KG)"

                Gv1.Columns("QTYSHIFT").IsVisible = True
                Gv1.Columns("QTYSHIFT").HeaderText = "BMC(Quantity in Kg)"
            End If


            Gv1.Columns("TotalQty").IsVisible = True
            Gv1.Columns("TotalQty").HeaderText = "TOTAL"
            Gv1.Columns("Comp_Name").IsVisible = False
            Gv1.Columns("Add1").IsVisible = False
            Gv1.Columns("Add2").IsVisible = False
        Next

        Dim summaryRowItemB As New GridViewSummaryRowItem()
        Dim TotalQty As New GridViewSummaryItem("TotalQty", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(TotalQty)
        Dim QTYSHIFT As New GridViewSummaryItem("QTYSHIFT", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(QTYSHIFT)
        Dim QTYUploader As New GridViewSummaryItem("QTYUploader", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItemB.Add(QTYUploader)
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItemB)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        RadGroupBox3.Enabled = True
        'fromDate.Value = clsCommon.GETSERVERDATE()
        'ToDate.Value = clsCommon.GETSERVERDATE()
        RadGroupBox1.Enabled = True
        TxtZone.Enabled = True
        btnPrint.Visible = False
        'TxtZone.arrValueMember = Nothing
        If rbtnBoth.Checked Then
            btnPrint.Visible = True
        Else
            btnPrint.Visible = False
        End If
    End Sub

    Private Sub TxtZone__My_Click(sender As Object, e As EventArgs) Handles TxtZone._My_Click

        Dim strQry As String = "select Zone_Code as Code ,Description as Name from TSPL_ZONE_MASTER where 1=1"
        'If TxtZone.arrValueMember IsNot Nothing AndAlso TxtZone.arrValueMember.Count > 0 Then
        '    strQry += " and TSPL_ZONE_MASTER. Zone_Code in (Select TSPL_CUSTOMER_MASTER.Zone_Code from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code in (" + clsCommon.GetMulcallString(TxtZone.arrValueMember) + ") )"
        'End If
        TxtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneMulSel", strQry, "Code", "Name", TxtZone.arrValueMember, TxtZone.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True)
    End Sub

    Private Sub rbtnBoth_Click(sender As Object, e As EventArgs) Handles rbtnBoth.Click
        If rbtnBoth.Checked Then
            btnPrint.Visible = True
        Else
            btnPrint.Visible = False
        End If

    End Sub

    Private Sub rbtnDock_Click(sender As Object, e As EventArgs) Handles rbtnDock.Click
        btnPrint.Visible = False
    End Sub

    Private Sub rdbtnBMC_Click(sender As Object, e As EventArgs) Handles rdbtnBMC.Click
        btnPrint.Visible = False
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.ZoneWiseReport & "'"))
            Dim arrHeader As List(Of String) = New List(Of String)()
            'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Report Name : " + strHeading)
            arrHeader.Add("Date Range from : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then
                'transportSql.QuickExportToExcel(Gv1, "", Me.Text,, arrHeader)
                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, Nothing, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
End Class