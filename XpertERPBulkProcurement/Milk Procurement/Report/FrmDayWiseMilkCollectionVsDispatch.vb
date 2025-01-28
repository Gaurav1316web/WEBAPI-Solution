Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class FrmDayWiseMilkCollectionVsDispatch

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
    End Sub
    Private Sub FrmDayWiseMilkCollectionVsDispatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        Reset()
    End Sub


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        LoadData()
        'ControlEnableDisable(False)
    End Sub

    Public Sub LoadData()
        Try
            Dim ConversionFactor As String = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
            Dim dt As New DataTable
            Dim strQry As String = ""
            Dim strQryy As String = ""

            'strQry = " select CONVERT(VARCHAR, tspl_weighment_detail.Weighment_date, 103) AS Date,
            '           tspl_weighment_detail.Net_Weight as Qty,tspl_weighment_detail.fat_per as FAT,
            '           tspl_weighment_detail.snf_Per as SNF,
            '           ROUND(tspl_quality_check.fat_KG, 2) AS fat_KG,
            '           ROUND(tspl_quality_check.SNF_KG, 2) AS SNF_KG,
            '        CONVERT(VARCHAR,TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date, 103) as OUT_Date,
            '        TSPL_INVENTORY_MOVEMENT_NEW.Qty as OUT_Qty,
            '        TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per as OUT_FAT,
            '        TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per as OUT_SNF,
            '        TSPL_INVENTORY_MOVEMENT_NEW.Fat_KG as OUT_FAT_KG,
            '        TSPL_INVENTORY_MOVEMENT_NEW.SNF_KG as OUT_SNF_KG
            strQry = "SELECT 
                        CONVERT(VARCHAR, tspl_weighment_detail.Weighment_date, 103) AS Date,
                        YEAR(tspl_weighment_detail.Weighment_date) AS Year,
                        MONTH(tspl_weighment_detail.Weighment_date) AS Month,
    
                        SUM(tspl_weighment_detail.Net_Weight) AS Total_Qty,
    
                        AVG(tspl_weighment_detail.fat_per) AS Avg_FAT,
                        AVG(tspl_weighment_detail.snf_Per) AS Avg_SNF,

                        SUM(ROUND(tspl_weighment_detail.Net_Weight * tspl_weighment_detail.fat_per / 100,2)) AS Total_Fat_KG,
                        SUM(ROUND(tspl_weighment_detail.Net_Weight * tspl_weighment_detail.snf_Per / 100,2)) AS Total_SNF_KG,

                        CONVERT(VARCHAR, TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date, 103) AS OUT_Date,
                        YEAR(TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date) AS OUT_Year,
                        MONTH(TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date) AS OUT_Month,

                        SUM(TSPL_INVENTORY_MOVEMENT_NEW.Qty) AS OUT_Total_Qty,

                        AVG(TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per) AS OUT_Avg_FAT,
                        AVG(TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per) AS OUT_Avg_SNF,

                        SUM(ROUND(TSPL_INVENTORY_MOVEMENT_NEW.Qty * TSPL_INVENTORY_MOVEMENT_NEW.Fat_Per / 100,2)) AS OUT_Total_Fat_KG,
                        SUM(ROUND(TSPL_INVENTORY_MOVEMENT_NEW.Qty * TSPL_INVENTORY_MOVEMENT_NEW.SNF_Per / 100,2)) AS OUT_Total_SNF_KG
                        From tspl_weighment_detail
                        left join tspl_milk_transfer_in on tspl_milk_transfer_in.Weighment_No=tspl_weighment_detail.Weighment_No
                        left outer join tspl_quality_check on tspl_quality_check.Gate_Entry_No=tspl_weighment_detail.Gate_Entry_No
                        left  join TSPL_INVENTORY_MOVEMENT_NEW on TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No=tspl_milk_transfer_in.Receipt_Challan_No  
                        left join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No=TSPL_INVENTORY_MOVEMENT_NEW.Source_Doc_No and TSPL_INVENTORY_MOVEMENT_NEW.Trans_Type='DispatchBS'
                        where tspl_weighment_detail.Weighment_date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and tspl_weighment_detail.Weighment_date<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'  
                        GROUP BY 
                        YEAR(tspl_weighment_detail.Weighment_date),
                        MONTH(tspl_weighment_detail.Weighment_date),
                        CONVERT(VARCHAR, tspl_weighment_detail.Weighment_date, 103),
                        YEAR(TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date),
                        MONTH(TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date),
                        CONVERT(VARCHAR, TSPL_INVENTORY_MOVEMENT_NEW.Punching_Date, 103)"
            strQryy += "ORDER BY 
                         Year, Month,Date"

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
                'ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If
            SetGridFormationOFGV1()
            Gv1.BestFitColumns()
            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Sub SetGridFormationOFGV1()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True

        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.MasterTemplate.ShowTotals = True
        ReStoreGridLayout()

    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub



    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmDayWiseMilkCollectionVsDispatch & "'"))
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid(Me.Text, Gv1, arrHeader, Me.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmDayWiseMilkCollectionVsDispatch & "'"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class
