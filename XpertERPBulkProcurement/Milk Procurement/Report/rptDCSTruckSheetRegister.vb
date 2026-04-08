Imports common
Imports System.IO
Public Class rptDCSTruckSheetRegister
    Inherits FrmMainTranScreen
    Private Sub rptDCSTruckSheetRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        rbtBoth.IsChecked = True
        rbtMorning.IsChecked = False
        rbtEvening.IsChecked = False
    End Sub
    Public Sub Griddata(ByVal print As Boolean, ByVal print2 As Boolean)
        Try
            Dim BaseQuery As String = Nothing
            BaseQuery = "select  TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2 , TSPL_MILK_COLLECTION_MCC.Document_No,  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,'Administrator' as User_Name,--(TSPL_MILK_SHIFT_UPLOADER_DETAIL.TR_No)TR_No,
(TSPL_MILK_COLLECTION_MCC_DETAIL.SNo)SNo,
TSPL_MCC_MASTER.mcc_code,(TSPL_MILK_COLLECTION_MCC.Route_Code) as [Route Code],(TSPL_BULK_ROUTE_MASTER.ROUTE_NAME) as [Route],(TSPL_MILK_COLLECTION_MCC.Tanker_No)Tanker_No,
(TSPL_MILK_COLLECTION_MCC.Document_Shift)Shift,(TSPL_MILK_COLLECTION_MCC.Document_Date)Shift_Date,(TSPL_MILK_COLLECTION_MCC.Created_Date)Created_Date,(TSPL_MCC_MASTER.mcc_Name) as mcc_Name,
               -- case When (isnull(Reject_Type,''))='' then (isnull(No_Of_Cans,0)) else 0 end as [Good can qty]
                case When (isnull(Reject_Type,''))='' then (isnull(qty,0)) else 0 end as [Good Qty]
                ,case When (isnull(Reject_Type,''))='' then (FAT) else 0 end as [Good FAT %]
                ,case When (isnull(Reject_Type,''))='' then (cast(qty*FAT/100 as decimal(18,3))) else 0 end as [Good FATKg]
                ,case When (isnull(Reject_Type,''))='' then (SNF) else 0 end as [Good SNF %]
                ,case When (isnull(Reject_Type,''))='' then (cast (qty*SNF/100 as decimal(18,3))) else 0 end as [Good SNFKG],
                --case When (isnull(Reject_Type,''))='SOUR' then (isnull(No_Of_Cans,0)) else 0 end as [SOUR can qty],
                case When (isnull(Reject_Type,''))='SOUR' then (qty) else 0 end as [SOUR Qty]
                ,case When (isnull(Reject_Type,''))='SOUR' then (FAT) else 0 end as [SOUR FAT %]
                ,case When (isnull(Reject_Type,''))='SOUR' then (cast (qty*FAT/100 as decimal(18,3))) else 0 end as [SOUR FATKg]
                ,case When (isnull(Reject_Type,''))='SOUR' then (SNF) else 0 end as [SOUR SNF %]
                ,case When (isnull(Reject_Type,''))='SOUR' then (cast (qty*SNF/100 as decimal(18,3))) else 0 end as [SOUR SNFKG],
                --case When (isnull(Reject_Type,''))='CURD' then (isnull(No_Of_Cans,0)) else 0 end as [CURD can qty],
                case When (isnull(Reject_Type,''))='CURD' then (qty) else 0 end as [CURD Qty]
                ,case When (isnull(Reject_Type,''))='CURD' then (FAT) else 0 end as [CURD FAT %]
                ,case When (isnull(Reject_Type,''))='CURD' then (cast (qty*FAT/100 as decimal(18,3))) else 0 end as [CURD FATKg]
                ,case When (isnull(Reject_Type,''))='CURD' then (SNF) else 0 end as [CURD SNF %]  ,case When (isnull(Reject_Type,''))='CURD' then (cast (qty*SNF/100 as decimal(18,3))) else 0 end as [CURD SNFKG] 
						
				from TSPL_MILK_COLLECTION_MCC_DETAIL
                Left Join  TSPL_MILK_COLLECTION_MCC on TSPL_MILK_COLLECTION_MCC.Document_No=TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No
			    Left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_MCC.route_code
			    Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.mcc_code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
				left outer join TSPL_COMPANY_MASTER on  2=2
                where TSPL_MILK_COLLECTION_MCC.Document_Date <= CONVERT(DATE, '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "', 103) "
            If rbtEvening.IsChecked Then
                BaseQuery += " AND TSPL_MILK_COLLECTION_MCC.Document_Shift ='E' "
            ElseIf rbtMorning.IsChecked Then
                BaseQuery += " AND TSPL_MILK_COLLECTION_MCC.Document_Shift ='M'"
            End If
            BaseQuery += "  ORDER BY SNO  "
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
                SetGridFormat()
                ' SetGridFormationOFGV1Collection()
                ' View()
                gv1.BestFitColumns()
                If print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "rptDcsTruckSheetRegister", "DCS Truck Sheet Register")
                    frmCRV = Nothing
                    ' frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, "", "rptTankerProfitLoss", "ProfitLoss", "SubTankerProfitLoss.rpt")
                End If
                If print2 = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(MyBase.Form_ID, CrystalReportFolder.MilkProcurement, dt, "rptDcsTruckSheetRegisterRoute", "DCS Truck Sheet Register")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub SetGridFormat()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            'gv1.Columns("Document_No").HeaderText = "Document No."
            gv1.Columns("User_Name").IsVisible = False

            gv1.Columns("Document_No").IsVisible = True
            gv1.Columns("Document_No").VisibleInColumnChooser = False

            gv1.Columns("Comp_Name").IsVisible = False
            gv1.Columns("Add1").IsVisible = False
            gv1.Columns("Add2").IsVisible = False
            gv1.Columns("Add3").IsVisible = False
            gv1.Columns("Logo_Img").IsVisible = False
            gv1.Columns("Logo_Img2").IsVisible = False
        Next
        Dim summaryRowItemB As New GridViewSummaryRowItem()
        gv1.AutoSizeRows = True
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Griddata(False, False)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Griddata(True, False)
    End Sub

    Private Sub btnRouteWise_Click(sender As Object, e As EventArgs) Handles btnRouteWise.Click
        Griddata(False, True)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()

        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtBoth.IsChecked = True
        rbtMorning.IsChecked = False
        rbtEvening.IsChecked = False

    End Sub
End Class