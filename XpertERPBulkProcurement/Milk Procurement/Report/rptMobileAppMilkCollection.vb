Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls

Public Class rptMobileAppMilkCollection
    Inherits FrmMainTranScreen
    Dim StrPermission As String
    ' Public TemplateGridview As MyRadGridView
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnGo.Enabled = True
        ControlEnableDisable(True)
    End Sub
    Sub ControlEnableDisable(ByVal isEnable As Boolean)
        fromDate.Enabled = isEnable
        dtpToDate.Enabled = isEnable
        txtBMC.Enabled = isEnable
        txtRoute.Enabled = isEnable
        txtDCS.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
    End Sub
    Private Sub rptMobileAppMilkCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = dtpToDate.Value.AddMonths(-1)
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        rbtnBMC.Checked = True
        lblDCS.Visible = False
        txtDCS.Visible = False
        Reset()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

        LoadData()
        'ControlEnableDisable(False)
    End Sub
    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Try
            Dim qry As String = "select ROUTE_NO,ROUTE_NAME from TSPL_BULK_ROUTE_MASTER where 2=2 "
            txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("PCURoute", qry, "ROUTE_NO", "ROUTE_NAME", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select MCC_Code,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code where tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtBMC.arrValueMember, txtBMC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData()
        Try
            Dim dt As New DataTable
            Dim strQry As String = ""

            If rbtnBMC.Checked Then
                strQry = "select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],(TSPL_MILK_COLLECTION_BMCDCS.PK_ID)PK_ID, (TSPL_MILK_COLLECTION_BMCDCS.IDate)IDate,(TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo)Truck_Sheet_SNo,
                        TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader,
                        (TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code)Route_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No)Trip_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Silo_Capacity)Silo_Capacity,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Gaze_Reading)Gaze_Reading,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty)Qty,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.FAT)FAT,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNF)SNF,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG)FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Sample_No,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG)SNFKG,(TSPL_MILK_COLLECTION_BMCDCS_TRIP.Temp)Temp
                        From TSPL_MILK_COLLECTION_BMCDCS
                        left Join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
                        Left Join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code   
                        where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
"
            End If
            If rbtnDCS.Checked Then
                strQry = "select Row_Number() Over (Order By (SELECT 1) Asc) as [S No],(TSPL_MILK_COLLECTION_BMCDCS.PK_ID)PK_ID,(TSPL_MILK_COLLECTION_BMCDCS.IDate)IDate,(TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo)Truck_Sheet_SNo,(TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader)Mcc_Code_VLC_Uploader,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,(TSPL_MCC_MASTER.MCC_NAME)MCC_NAME,Trip_No=1
                        ,(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,
                        (TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code)VLC_Code,(TSPL_VLC_MASTER_HEAD.VLC_Name)VLC_Name,(TSPL_MILK_COLLECTION_BMCDCS_DCS.IShift)IShift,(TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty)Qty,(TSPL_MILK_COLLECTION_BMCDCS_DCS.FAT)FAT,(TSPL_MILK_COLLECTION_BMCDCS_DCS.SNF)SNF,(TSPL_MILK_COLLECTION_BMCDCS_DCS.FATKG)FATKG,(TSPL_MILK_COLLECTION_BMCDCS_DCS.SNFKG)SNFKG
                        from TSPL_MILK_COLLECTION_BMCDCS
                       left join TSPL_MILK_COLLECTION_BMCDCS_DCS on TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
                        left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
                        left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code
                where TSPL_MILK_COLLECTION_BMCDCS.IDate >='" + clsCommon.GetPrintDate(fromDate.Value) + "' and TSPL_MILK_COLLECTION_BMCDCS.IDate<='" + clsCommon.GetPrintDate(dtpToDate.Value) + "'
                    "
            End If
            If rbtnBMC.Checked Then
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    strQry += " and TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ")"
                End If
            End If
            If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
                strQry += " and TSPL_MILK_COLLECTION_BMCDCS.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If
            If rbtnDCS.Checked Then
                If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                    strQry += " and TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code in (" + clsCommon.GetMulcallString(txtDCS.arrValueMember) + ")"
                End If
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
                FormatGrid()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            End If

            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.Route_Code,TSPL_MCC_ROUTE_MASTER.Route_Name from TSPL_VLC_MASTER_HEAD left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_VLC_MASTER_HEAD.Route_Code where 2=2 and TSPL_VLC_MASTER_HEAD.Active='1' "
            'If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_VLC_MASTER_HEAD.Route_Code in (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
            'End If

            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUVLC1", qry, "VLC_Code", "VLC_Name", txtDCS.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnDCS_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnDCS.CheckedChanged

    End Sub

    Private Sub rbtnDCS_Click(sender As Object, e As EventArgs) Handles rbtnDCS.Click
        lblDCS.Visible = True
        txtDCS.Visible = True
    End Sub

    Private Sub rbtnBMC_Click(sender As Object, e As EventArgs) Handles rbtnBMC.Click
        lblDCS.Visible = False
        txtDCS.Visible = False
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))
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
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMobileAppMilkCollection & "'"))

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF(Me.Text, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Sub FormatGrid()
        Dim summaryItem As New GridViewSummaryItem()
        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            Gv1.Columns("S No").HeaderText = "S No"
            Gv1.Columns("PK_ID").HeaderText = "PK ID"
            Gv1.Columns("PK_ID").FormatString = "{0}"
            Gv1.Columns("IDate").HeaderText = "Date"
            Gv1.Columns("Truck_Sheet_SNo").HeaderText = "Truck Sheet SNo"
            Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
            Gv1.Columns("MCC_NAME").HeaderText = "BMC NAME"
            Gv1.Columns("Trip_No").HeaderText = "Trip No"
            Gv1.Columns("Trip_No").FormatString = "{0}"
            If rbtnBMC.Checked Then
                Gv1.Columns("Silo_Capacity").HeaderText = "Silo Capacity"
                Gv1.Columns("Silo_Capacity").FormatString = "{0}"
                Gv1.Columns("Gaze_Reading").HeaderText = "Gaze Reading"
                Gv1.Columns("Gaze_Reading").FormatString = "{0}"
                Gv1.Columns("Temp").HeaderText = "Temp"
                Gv1.Columns("Temp").FormatString = "{0}"
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "DCS Uploader"
                Gv1.Columns("Sample_No").HeaderText = "Sample No"
                Gv1.Columns("Route_Code").HeaderText = "Route Code"
                Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"
            End If
            If rbtnDCS.Checked Then
                Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "MCC Uploader"
                Gv1.Columns("VLC_Code").HeaderText = "DCS Code"
                Gv1.Columns("VLC_Name").HeaderText = "DCS Name"
                Gv1.Columns("IShift").HeaderText = "Shift"
                Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader"

            End If

            Gv1.Columns("Qty").HeaderText = "Qty"
            Gv1.Columns("Qty").FormatString = "{0:n2}"
            Gv1.Columns("FAT").HeaderText = "FAT%"
            Gv1.Columns("FAT").FormatString = "{0:n2}"
            Gv1.Columns("SNF").HeaderText = "SNF%"
            Gv1.Columns("SNF").FormatString = "{0:n2}"
            Gv1.Columns("FATKG").HeaderText = "FAT KG"
            Gv1.Columns("FATKG").FormatString = "{0:n2}"
            Gv1.Columns("SNFKG").HeaderText = "SNF KG"
            Gv1.Columns("SNFKG").FormatString = "{0:n2}"


        Next
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim Qty As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Qty)
        If rbtnDCS.Checked Then
            Dim Trip As New GridViewSummaryItem("Trip_No", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Trip)
        End If
        Dim FAT_PER As New GridViewSummaryItem()
        FAT_PER.FormatString = "{0:F2}"
        FAT_PER.Name = "FAT"
        FAT_PER.AggregateExpression = "sum(FATKG)*100/sum(Qty)"
        summaryRowItem.Add(FAT_PER)
        Dim SNF_PER As New GridViewSummaryItem()
        SNF_PER.FormatString = "{0:F2}"
        SNF_PER.Name = "SNF"
        SNF_PER.AggregateExpression = "sum(SNFKG)*100/sum(Qty)"
        summaryRowItem.Add(SNF_PER)

        'Dim SNF_PER As New GridViewSummaryItem("SNF", "{0:F2}", GridAggregateFunction.Avg)
        'summaryRowItem.Add(SNF_PER)
        Dim FATKG As New GridViewSummaryItem("FATKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(FATKG)
        Dim SNFKG As New GridViewSummaryItem("SNFKG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(SNFKG)



        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try


            Dim strQry As String = "select TSPL_MILK_COLLECTION_BMCDCS.PK_ID,TSPL_MILK_COLLECTION_BMCDCS.IDate,TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,TSPL_MILK_COLLECTION_BMCDCS.Arrive_Time,TSPL_MILK_COLLECTION_BMCDCS.Dispatch_Time,TSPL_MILK_COLLECTION_BMCDCS.Last_BMC_Seal_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Silo_Capacity,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Gaze_Reading,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Qty,TSPL_MILK_COLLECTION_BMCDCS_TRIP.FAT,TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNF,TSPL_MILK_COLLECTION_BMCDCS_TRIP.FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.SNFKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Temp
from TSPL_MILK_COLLECTION_BMCDCS
left join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code where TSPL_MILK_COLLECTION_BMCDCS.PK_ID='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(1).Value) + "'"
            Dim strQryDCS As String = "select TSPL_MILK_COLLECTION_BMCDCS.PK_ID,TSPL_MILK_COLLECTION_BMCDCS.IDate,TSPL_MILK_COLLECTION_BMCDCS.Truck_Sheet_SNo,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code,TSPL_MCC_MASTER.MCC_NAME,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Route_Code,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Vehicle_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP.Trip_No,
TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_MILK_COLLECTION_BMCDCS_DCS.IShift,TSPL_MILK_COLLECTION_BMCDCS_DCS.Qty,TSPL_MILK_COLLECTION_BMCDCS_DCS.FAT,TSPL_MILK_COLLECTION_BMCDCS_DCS.SNF,TSPL_MILK_COLLECTION_BMCDCS_DCS.FATKG,TSPL_MILK_COLLECTION_BMCDCS_DCS.SNFKG
from TSPL_MILK_COLLECTION_BMCDCS
left join TSPL_MILK_COLLECTION_BMCDCS_TRIP on TSPL_MILK_COLLECTION_BMCDCS_TRIP.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
left join TSPL_MILK_COLLECTION_BMCDCS_DCS on TSPL_MILK_COLLECTION_BMCDCS_DCS.REF_PK_ID=TSPL_MILK_COLLECTION_BMCDCS.PK_ID
left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_COLLECTION_BMCDCS.MCC_Code
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_COLLECTION_BMCDCS_DCS.VLC_Code where TSPL_MILK_COLLECTION_BMCDCS.PK_ID='" + clsCommon.myCstr(Gv1.CurrentRow.Cells(1).Value) + "'"
            Dim dtdcs As DataTable = clsDBFuncationality.GetDataTable(strQry)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQry)

            If dt IsNot Nothing And dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funsubreportWithdt(False, CrystalReportFolder.MilkProcurement, dt, dtdcs, "crptMobileAppMilkCollectionReport", "", "crptDCSMilkcollectionReport.rpt")
                frmCRV = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Record Found!", Me.Text)

            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class