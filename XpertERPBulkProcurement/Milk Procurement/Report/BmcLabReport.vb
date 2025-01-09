Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls
Public Class BmcLabReport
    Inherits FrmMainTranScreen
    Dim StrPermission As String

    Sub Reset()
        txtRoute.arrValueMember = Nothing
        TxtTankerNo.arrValueMember = Nothing
        txtBMC.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        Gv1.DataSource = Nothing
        'ControlEnableDisable(True)
    End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select ROUTE_NO,ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where 2=2 "
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "ROUTE_NO", "ROUTE_NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub TxtTankerNo__My_Click(sender As Object, e As EventArgs) Handles TxtTankerNo._My_Click
        Try
            Dim qry As String = " select Tanker_No as TankerNo from TSPL_MILK_COLLECTION_MCC where 2=2 "
            TxtTankerNo.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUTankerNo", qry, "TankerNo", " ", TxtTankerNo.arrValueMember, TxtTankerNo.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select Mcc_Code_VLC_Uploader from TSPL_MCC_MASTER where 2=2 "
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "Mcc_Code_VLC_Uploader", " ", txtBMC.arrValueMember, txtBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles TxtTankerNo._My_Click
    '    Try
    '        Dim qry As String = " select Tanker_No as Bmc from TSPL_MILK_COLLECTION_MCC "
    '        TxtTankerNo.arrValueMember = clsCommon.ShowMultipleSelectForm("MultiTanker", qry, "Tanker_No", " ", TxtTankerNo.arrValueMember, TxtTankerNo.arrDispalyMember)
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.BmcLabReport & "'"))
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.BmcLabReport & "'"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub BmcLabReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = clsCommon.GETSERVERDATE()
        'StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        'rbtnBMC.Checked = True
        'lblDCS.Visible = False
        'txtDCS.Visible = False
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
        'ControlEnableDisable(False)
    End Sub

    Public Sub LoadData()
        Try
            Dim ConversionFactor As String = clsFixedParameter.GetData(clsFixedParameterType.defaultCorrectionFactor, clsFixedParameterCode.MilkSetting, Nothing)
            Dim dt As New DataTable
            Dim strQry As String = ""
            'strQry = " select FORMAT(TSPL_MILK_COLLECTION_MCC.Document_Date, 'dd/MM/yyyy') AS Document_Date, TSPL_BULK_ROUTE_MASTER.ROUTE_NO as [Route No],TSPL_MILK_COLLECTION_MCC.Tanker_No as [Tanker No],TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader as [Dcs/Bmc],
            '                        TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No as [Sample No], TSPL_MILK_COLLECTION_MCC_DETAIL.Gaze_Qty as Liter,
            '                        (SNF- " + ConversionFactor + " -0.2*FAT)*4 as CLR,
            '                        TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty as Qty,TSPL_MILK_COLLECTION_MCC_DETAIL.FAT as [Fat%],TSPL_MILK_COLLECTION_MCC_DETAIL.SNF as [Snf%],
            '                        TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG as [Fat Kg],TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG as [Snf Kg] 
            '                        from TSPL_MILK_COLLECTION_MCC
            '                        left outer join TSPL_BULK_ROUTE_MASTER on TSPL_BULK_ROUTE_MASTER.ROUTE_NO=TSPL_MILK_COLLECTION_MCC.Route_Code
            '                        left outer join TSPL_MILK_COLLECTION_MCC_DETAIL on TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No=TSPL_MILK_COLLECTION_MCC.Document_No
            '                        left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_code=TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
            '                        where TSPL_MILK_COLLECTION_MCC.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_MCC.Document_Date<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'"

            strQry = "SELECT 
                            1 AS 'S.No.', 
                            FORMAT(TSPL_MILK_COLLECTION_MCC.Document_Date, 'dd/MM/yyyy') AS Document_Date, 
                            TSPL_BULK_ROUTE_MASTER.ROUTE_NO AS [Route No], 
                            TSPL_MILK_COLLECTION_MCC.Tanker_No AS [Tanker No], 
                            TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader AS [Dcs/Bmc], 
                            TSPL_MILK_COLLECTION_MCC_DETAIL.Sample_No AS [Sample No], 
                            TSPL_MILK_COLLECTION_MCC_DETAIL.Gaze_Qty AS Liter, 
                            (SNF - 0.14 - 0.2 * FAT) * 4 AS CLR, 
                            TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty AS Qty, 
                            TSPL_MILK_COLLECTION_MCC_DETAIL.FAT AS [Fat%], 
                            TSPL_MILK_COLLECTION_MCC_DETAIL.SNF AS [Snf%], 
                            TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG AS [Fat Kg], 
                            TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG AS [Snf Kg]
                        FROM 
                            TSPL_MILK_COLLECTION_MCC
                        LEFT OUTER JOIN 
                            TSPL_BULK_ROUTE_MASTER 
                            ON TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code
                        LEFT OUTER JOIN 
                            TSPL_MILK_COLLECTION_MCC_DETAIL 
                            ON TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_MCC.Document_No
                        LEFT OUTER JOIN 
                            TSPL_MCC_MASTER 
                            ON TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code"

            strQry += "" & Environment.NewLine & " Union all " & Environment.NewLine & ""
            strQry += " SELECT 
                        2 AS 'S.No.', 
                        'Total' AS Document_Date, 
                        TSPL_BULK_ROUTE_MASTER.ROUTE_NO AS [Route No], 
                        NULL AS [Tanker No], 
                        NULL AS [Dcs/Bmc], 
                        NULL AS [Sample No], 
                        SUM(TSPL_MILK_COLLECTION_MCC_DETAIL.Gaze_Qty) AS Liter, 
                        NULL AS CLR, 
                        SUM(TSPL_MILK_COLLECTION_MCC_DETAIL.Original_Qty) AS Qty, 
                        NULL AS [Fat%], 
                        NULL AS [Snf%], 
                        SUM(TSPL_MILK_COLLECTION_MCC_DETAIL.FATKG) AS [Fat Kg], 
                        SUM(TSPL_MILK_COLLECTION_MCC_DETAIL.SNFKG) AS [Snf Kg]
                    FROM 
                        TSPL_MILK_COLLECTION_MCC
                    LEFT OUTER JOIN 
                        TSPL_BULK_ROUTE_MASTER 
                        ON TSPL_BULK_ROUTE_MASTER.ROUTE_NO = TSPL_MILK_COLLECTION_MCC.Route_Code
                    LEFT OUTER JOIN 
                        TSPL_MILK_COLLECTION_MCC_DETAIL 
                        ON TSPL_MILK_COLLECTION_MCC_DETAIL.Document_No = TSPL_MILK_COLLECTION_MCC.Document_No
                    LEFT OUTER JOIN 
                        TSPL_MCC_MASTER 
                        ON TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_MCC_DETAIL.MCC_Code
                    where TSPL_MILK_COLLECTION_MCC.Document_Date >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_MCC.Document_Date<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
                    GROUP BY 
                    TSPL_BULK_ROUTE_MASTER.ROUTE_NO "

            strQry += "ORDER BY 
             [Route No], [S.No.], Document_Date"


            If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                strQry += " and TSPL_BULK_ROUTE_MASTER.ROUTE_NO in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
            End If

            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                strQry += " and TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If

            If TxtTankerNo.arrValueMember IsNot Nothing AndAlso TxtTankerNo.arrValueMember.Count > 0 Then
                strQry += " and TSPL_MILK_COLLECTION_MCC.Tanker_No in (" + clsCommon.GetMulcallString(TxtTankerNo.arrValueMember) + ")"
            End If

            'If rbtnBMC.Checked Then
            '    strQry += " Group by TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code"
            'Else
            '    strQry += " Group by TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code"
            'End If


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

        ' gvData.GroupDescriptors.Add(New GridGroupByExpression("Route as RouteName format ""{0}: {1}"" Group By Route"))

        'For i As Integer = 10 To gvData.Columns.Count - 1
        '    Dim aa = gvData.Columns(i).HeaderText()
        '    Dim item8 As New GridViewSummaryItem("[Total Amount]", "{0:F2}", GridAggregateFunction.Sum)
        '    summaryRowItem.Add(item8)

        'Next
        'Dim aa = gvData.Columns(i).HeaderText()
        Dim item81 As New GridViewSummaryItem("Sample No", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item81)

        Dim item82 As New GridViewSummaryItem("Liter", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item82)

        Dim item83 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item83)

        Dim item84 As New GridViewSummaryItem("Fat Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item84)

        Dim item85 As New GridViewSummaryItem("Snf Kg", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item85)
        Gv1.ShowGroupPanel = True
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Gv1.MasterTemplate.ShowTotals = True
        'ReStoreGridLayout()

    End Sub

End Class
