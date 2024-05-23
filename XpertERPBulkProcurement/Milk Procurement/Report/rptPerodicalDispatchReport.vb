
Imports common
Imports System.IO


Public Class rptPerodicalDispatchReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Const ReportID As String = "SalesLedgerReport"
#End Region
    Private Sub rptPerodicalDispatchReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        funreset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub

    Sub funreset()
        EnableDisableControls(True)
        gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox3.Enabled = val
    End Sub

    Private Sub Print(ByVal print As Boolean)
        Try

            Dim qry As String = "select TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img ,From_Date = '" & clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") & "',To_Date= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "', aa.ROUTE_CODE as [Tanker No], aa.Mcc_Uploader_Code AS [MCC CODE],aa.[MCC Name],aa.Quantity ,CONVERT(DECIMAL(18,2),aa.FAT)FAT ,CONVERT(DECIMAL(18,2),aa.SNF) SNF ,CONVERT(DECIMAL(18,2),aa.[FAT(KG)]) KgFat ,CONVERT(DECIMAL(18,2),aa.[SNF(KG)] ) KgSNF
            from ( " & Environment.NewLine & " select max(pp.Comp_Code) as Comp_Name,max(pp.Comp_Code) as Comp_Code,(pp.ROUTE_CODE) as ROUTE_CODE,pp.[MCC Code]  as MCC_Code,max(pp.[MCC Name] )  as [MCC Name],sum([Milk Weight(KG)] ) as Quantity, case when sum([Milk Weight(KG)] )=0 then 0 else (sum([FAT(KG)] )/sum([Milk Weight(KG)] ))*100 end as FAT, 
            case when sum([Milk Weight(KG)] )=0 then 0 else (sum([SNF(KG)] )/sum([Milk Weight(KG)] ))*100 end as SNF ,sum([FAT(KG)] ) as [FAT(KG)] ,sum([SNF(KG)] ) as [SNF(KG)] ,max(Mcc_Uploader_Code) as Mcc_Uploader_Code,max([Vlc Code])[Vlc Code],max([Vlc Uploader Code])[Vlc Uploader Code],max([VLC Name])[VLC Name],sum(Qty)Qty from  (  " & Environment.NewLine & "
            Select  final.Comp_Code,final.ROUTE_CODE,final.MCC as [MCC Code] ,final.[MCC Name],final.Date ,final.[Doc Date] ,final.[Vlc Uploader Code] ,final.[Vlc Code] ,final.[VLC Name],final.UOM_Code as [UOM],final.[Milk Weight(KG)],final.FAT  ,final.SNF ,final.[FAT(KG)],final.[SNF(KG)],Mcc_Code_VLC_Uploader as [Mcc_Uploader_Code], final.Qty From ( " & Environment.NewLine & "
            Select TSPL_MILK_SRN_HEAD.Comp_Code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.ROUTE_CODE,Case When TSPL_MILK_SRN_HEAD.Dock_Collection_Milk_Type   = 'B' Then TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR Else 0 End [Buffalo Milk Qty (Ltr)] , TSPL_MILK_SRN_HEAD.MCC_CODE As MCC, TSPL_MCC_MASTER.MCC_NAME As [MCC Name], Convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As Date, 
            Convert(varchar,TSPL_MILK_SRN_HEAD.DOC_DATE,103) As [Doc Date],TSPL_VLC_MASTER_HEAD.VLC_Code As [Vlc Code], TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [Vlc Uploader Code], TSPL_VLC_MASTER_HEAD.VLC_Name As [VLC Name], TSPL_MILK_SRN_DETAIL.Qty As [Milk Weight],TSPL_MILK_SRN_DETAIL.UOM_Code, TSPL_MILK_SRN_DETAIL.ACC_QTY As [Milk Weight(KG)], convert(decimal(18,2),TSPL_MILK_SRN_DETAIL.ACC_QTY_LTR) As [Milk Weight(LTR)], 
            TSPL_MILK_SRN_DETAIL.FAT_PER As FAT, TSPL_MILK_SRN_DETAIL.SNF_PER As SNF, TSPL_MILK_SRN_DETAIL.FAT_KG  As [FAT(KG)],TSPL_MILK_SRN_DETAIL.SNF_KG  As [SNF(KG)] ,TSPL_MILK_SRN_DETAIL.Qty From TSPL_MILK_SRN_DETAIL 
            Left Outer Join TSPL_MILK_SRN_HEAD On TSPL_MILK_SRN_HEAD.DOC_CODE = TSPL_MILK_SRN_DETAIL.DOC_CODE  Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_SRN_HEAD.MCC_CODE  Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE
            where 2 = 2 and  convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) >=Convert(date,'" & clsCommon.GetPrintDate(txtfromDate.Value, "dd/MMM/yyyy") & "',103) 
            and convert(date,TSPL_MILK_SRN_HEAD.DOC_DATE,103) <= Convert(date,'" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "',103)  ) As final  ) as  pp group by pp.ROUTE_CODE, pp.[MCC Code] ) as aa 
            left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code = aa.Comp_Code
            order by ROUTE_CODE, [MCC Code]"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            If dt.Rows.Count > 0 Then

                gv1.DataSource = dt
                gv1.BestFitColumns()
                SetGridFormation()
                ReStoreGridLayout()
                gv1.MasterTemplate.AutoExpandGroups = True
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.BestFitColumns()
                If print = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptRoutePeriodicalDispatchReport", "")

                End If

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Sub SetGridFormation()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
        Next
        gv1.ShowGroupPanel = False
        gv1.Columns("Comp_Name").IsVisible = False
        gv1.Columns("Logo_Img").IsVisible = False
        gv1.Columns("From_Date").IsVisible = False
        gv1.Columns("To_Date").IsVisible = False

        Dim summaryRowItem As New GridViewSummaryRowItem()

        summaryRowItem.Add(New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum))
        summaryRowItem.Add(New GridViewSummaryItem("KgFat", "{0:F2}", GridAggregateFunction.Sum))
        summaryRowItem.Add(New GridViewSummaryItem("KgSNF", "{0:F2}", GridAggregateFunction.Sum))
        summaryRowItem.Add(New GridViewSummaryItem("FAT", "{0:F2}", "sum(KgFat)*100/sum(Quantity)"))
        summaryRowItem.Add(New GridViewSummaryItem("SNF", "{0:F2}", "sum(KgSNF)*100/sum(Quantity)"))

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer = 0
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
                obj = Nothing
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPerodicalDispatchReport & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))

                transportSql.exportdata(gv1, "", Me.Text, , arrHeader, False, False, True)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Date : " & clsCommon.myCDate(txtFromDate.Value) + "  To " + clsCommon.myCDate(txtToDate.Value))

                clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Print(True)
    End Sub
End Class

