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
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Griddata(False, False)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub Griddata(ByVal print As Boolean, ByVal print2 As Boolean)
        Try
            RadGroupBox3.Enabled = False
            Dim arrRoute As ArrayList = Nothing
            Dim Value As String

            Dim BaseQuery As String = Nothing
            BaseQuery = " select max(xx.Document_No)documnet_no , max(xx.Document_Date)Document_Date , sum(xx.QTYUploader)QTYUploader,sum(xx.QTYSHIFT)QTYSHIFT,XX.Zone_Code
,(sum(xx.QTYUploader)+sum(xx.QTYSHIFT)) as TotalQty

from
(select  TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_No,TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_Date
,TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.Milk_Weight as [QTYUploader],0 as [QTYSHIFT],

TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.VLC_Code,TSPL_VENDOR_MASTER.Zone_Code
from TSPL_MILK_PROCUREMENT_UPLOADER_detail
left outer join TSPL_MILK_PROCUREMENT_UPLOADER_head  on TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_No=TSPL_MILK_PROCUREMENT_UPLOADER_detail.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_PROCUREMENT_UPLOADER_DETAIL.VLC_Code  
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) 

--where TSPL_MILK_PROCUREMENT_UPLOADER_head.Document_No='BMC/0000010000000064'
union all
select TSPL_MILK_SHIFT_UPLOADER_head.Document_No,TSPL_MILK_SHIFT_UPLOADER_head.Shift_Date as Document_Date ,
0 as [QTYUploader],TSPL_MILK_SHIFT_UPLOADER_detail.Milk_Weight as [QTYSHIFT]


,TSPL_MILK_SHIFT_UPLOADER_detail.VLC_Code,TSPL_VENDOR_MASTER.Zone_Code
from TSPL_MILK_SHIFT_UPLOADER_detail
left outer join TSPL_MILK_SHIFT_UPLOADER_head on TSPL_MILK_SHIFT_UPLOADER_head.Document_No=TSPL_MILK_SHIFT_UPLOADER_detail.Document_No
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SHIFT_UPLOADER_DETAIL.VLC_Code
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_VLC_MASTER_HEAD.VSP_Code
where   convert(date,TSPL_MILK_SHIFT_UPLOADER_head.Shift_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_MILK_SHIFT_UPLOADER_head.Shift_Date,103) <=convert(date,'" + ToDate.Value + "' ,103) 
--where TSPL_MILK_SHIFT_UPLOADER_head.Document_No='BMC/0000010000000064'
)xx
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
                Gv1.BestFitColumns()
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
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns("Zone_Code").IsVisible = False
            Gv1.Columns("Zone_Code").HeaderText = "Zone Code"
            Gv1.Columns("documnet_no").IsVisible = False
            Gv1.Columns("documnet_no").HeaderText = "Documnet No"
            Gv1.Columns("Document_Date").IsVisible = False
            Gv1.Columns("Document_Date").HeaderText = "Document Date"
            Gv1.Columns("QTYUploader").IsVisible = True
            Gv1.Columns("QTYUploader").HeaderText = "DOCK(Quantity in KG)"
            Gv1.Columns("QTYSHIFT").IsVisible = True
            Gv1.Columns("QTYSHIFT").HeaderText = "BMC(Quantity in Kg)"
            Gv1.Columns("TotalQty").IsVisible = True
            Gv1.Columns("TotalQty").HeaderText = "TOTAL"
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
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()

    End Sub
End Class