Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports common.UserControls

Public Class rptBMCMobileHistory
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
        txtDate.Enabled = isEnable
        txtBMC.Enabled = isEnable
        RadGroupBox1.Enabled = isEnable
    End Sub
    Private Sub rptBMCMobileHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        rbtnBMC.Checked = True
        Reset()
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        GetReportID()
        LoadData()
    End Sub

    Sub GetReportID()
        Dim VarID As String = ""
        If rbtnBMC.Checked Then
            VarID += "_B"
        ElseIf rbtnDCS.Checked Then
            VarID += "_D"
        End If
        Gv1.VarID = VarID
    End Sub
    Private Sub txtBMC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBMC._MYValidating
        Try
            Dim WhrCls As String = "  tspl_mcc_master.mcc_Code in (" & StrPermission & ")"
            Dim qry As String = "select MCC_Code as MCCCode,Mcc_Code_VLC_Uploader as Mcc_Code_VLC_Uploader,MCC_NAME,TSPL_MCC_MASTER.plant_code as [Plant Code],tspl_location_master.location_desc as [Plant Name] from TSPL_MCC_MASTER left join tspl_location_master on tspl_location_master.location_code=TSPL_MCC_MASTER.plant_code "
            txtBMC.Value = clsCommon.ShowSelectForm("HistMCC", qry, "MCCCode", WhrCls, txtBMC.Value, "MCC_Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub LoadData()
        Try
            If clsCommon.myLen(txtBMC.Value) <= 0 Then
                Throw New Exception("Please select BMC")
                Exit Sub
            End If
            Dim qry As String = ""
            Dim whrcls As String = ""
            If clsCommon.myLen(txtBMC.Value) > 0 Then
                whrcls = " and TSPL_MILK_COLLECTION_BMCDCS.mcc_code = '" + txtBMC.Value + "' "
            End If
            whrcls += " AND CONVERT(date, Hist_On, 103) = '" + clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") + "'"
            If rbtnBMC.Checked Then
                qry = "WITH VersionsData AS ( SELECT TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.Hist_Version as [Hist Version],TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.Hist_By as [Hist By],TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.Hist_On as [Hist On],TSPL_MILK_COLLECTION_BMCDCS.MCC_Code ,Mcc_Code_VLC_Uploader,MCC_NAME,TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.REF_PK_ID,TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.Sample_No ,TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.Qty,TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.FAT,TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.SNF,TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.FATKG,TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.SNFKG,ROW_NUMBER() OVER ( ORDER BY TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.Hist_Version ASC) AS rownum,TSPL_MILK_COLLECTION_BMCDCS_TRIP_Hist_Data.Route_Code, TSPL_MILK_COLLECTION_BMCDCS_TRIP_Hist_Data.Vehicle_No,TSPL_MILK_COLLECTION_BMCDCS_TRIP_Hist_Data.Gaze_Reading,
            TSPL_MILK_COLLECTION_BMCDCS_TRIP_Hist_Data.Silo_Capacity,TSPL_MILK_COLLECTION_BMCDCS_TRIP_Hist_Data.Temp,TSPL_MILK_COLLECTION_BMCDCS_TRIP_Hist_Data.Trip_No FROM TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data LEFT JOIN TSPL_MILK_COLLECTION_BMCDCS ON TSPL_MILK_COLLECTION_BMCDCS.PK_ID = TSPL_MILK_COLLECTION_BMCDCS_TRIP_hist_data.REF_PK_ID LEFT JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_BMCDCS.MCC_Code WHERE 2=2 " + whrcls + " ),
            ChangedData AS ( SELECT ChangedData.[Hist Version],ChangedData.[Hist By],ChangedData.[Hist On],ChangedData.Mcc_Code_VLC_Uploader,ChangedData.MCC_Code,ChangedData.MCC_NAME, ChangedData.REF_PK_ID,ChangedData.Sample_No,ChangedData.Qty,ChangedData.FAT,ChangedData.SNF,ChangedData.FATKG,ChangedData.SNFKG,ChangedData.rownum,ChangedData.Route_Code,ChangedData.Vehicle_No,ChangedData.Gaze_Reading ,ChangedData.Silo_Capacity,ChangedData.Temp,ChangedData.Trip_No,CASE WHEN ChangedData.Qty != NextVersionDa.Qty OR ChangedData.FAT != NextVersionDa.FAT OR ChangedData.SNF != NextVersionDa.SNF OR ChangedData.FATKG != NextVersionDa.FATKG  OR ChangedData.SNFKG != NextVersionDa.SNFKG OR ChangedData.Route_Code != NextVersionDa.Route_Code OR ChangedData.Vehicle_No != NextVersionDa.Vehicle_No OR ChangedData.Gaze_Reading != NextVersionDa.Gaze_Reading OR ChangedData.Silo_Capacity != NextVersionDa.Silo_Capacity OR ChangedData.Temp != NextVersionDa.Temp 
            OR ChangedData.Trip_No != NextVersionDa.Trip_No OR ChangedData.Sample_No != NextVersionDa.Sample_No THEN 1 ELSE 0 END AS DataChanged FROM  VersionsData ChangedData  LEFT JOIN VersionsData NextVersionDa ON ChangedData.REF_PK_ID = NextVersionDa.REF_PK_ID  AND ChangedData.[Hist Version] = NextVersionDa.[Hist Version] + 1 ), FinalResults AS ( SELECT ROW_NUMBER() OVER ( ORDER BY [Hist Version] ASC) AS rownum,[Hist Version],[Hist By],[Hist On],Mcc_Code_VLC_Uploader,MCC_Code,MCC_NAME,Sample_No,Qty,FAT,SNF,FATKG,SNFKG,Route_Code,Vehicle_No,Gaze_Reading ,Silo_Capacity,Temp,Trip_No,DataChanged FROM ChangedData WHERE rownum = 1 OR DataChanged = 1 ) SELECT rownum,[Hist Version],[Hist By],[Hist On],Mcc_Code_VLC_Uploader,MCC_Code,MCC_NAME,Sample_No,Qty,FAT,SNF,FATKG,SNFKG,Route_Code,Vehicle_No,Gaze_Reading ,Silo_Capacity,Temp,Trip_No FROM FinalResults ORDER BY rownum, [Hist Version] ASC  "
            ElseIf rbtnDCS.Checked Then
                qry = " WITH VersionsData AS (SELECT TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Hist_Version AS [Hist Version],TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Hist_By AS [Hist By],TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Hist_On AS [Hist On],TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.IShift AS Shift,TSPL_MILK_COLLECTION_BMCDCS.MCC_Code AS [MCC_Code],Mcc_Code_VLC_Uploader AS Mcc_Code_VLC_Uploader,MCC_NAME AS MCC_NAME,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader AS [DCS Uploader Code],TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.VLC_Code AS [DCS Code],VLC_Name AS [DCS Name],TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.REF_PK_ID,TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Qty,TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.FAT,TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.SNF,TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.FATKG,TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.SNFKG,ROW_NUMBER() OVER (PARTITION BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.VLC_Code,
            TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.IShift ORDER BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Hist_Version ASC) AS rownum, LAG(TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Qty) OVER ( PARTITION BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.VLC_Code, TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.IShift ORDER BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Hist_Version ASC) AS Prev_Qty,LAG(TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.FAT) OVER (PARTITION BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.VLC_Code, TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.IShift ORDER BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Hist_Version ASC) AS Prev_FAT,LAG(TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.SNF) OVER (PARTITION BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.VLC_Code, TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.IShift ORDER BY TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.Hist_Version ASC) AS Prev_SNF FROM TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data
            LEFT JOIN TSPL_MILK_COLLECTION_BMCDCS  ON TSPL_MILK_COLLECTION_BMCDCS.PK_ID = TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.REF_PK_ID LEFT JOIN TSPL_MCC_MASTER  ON TSPL_MCC_MASTER.MCC_Code = TSPL_MILK_COLLECTION_BMCDCS.MCC_Code LEFT JOIN TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_COLLECTION_BMCDCS_DCS_Hist_Data.VLC_Code WHERE 2=2 " + whrcls + "), ChangedData AS ( SELECT [Hist Version],[Hist By],[Hist On],Shift,Mcc_Code_VLC_Uploader,MCC_Code,MCC_NAME,[DCS Uploader Code],[DCS Code],[DCS Name],REF_PK_ID,Qty,FAT,SNF,FATKG,SNFKG,rownum,CASE WHEN (Qty != Prev_Qty OR FAT != Prev_FAT OR SNF != Prev_SNF) THEN 1 ELSE 0 END AS DataChanged FROM VersionsData ), FinalResults AS ( SELECT REF_PK_ID,[Hist Version],[Hist By],[Hist On],Shift,Mcc_Code_VLC_Uploader,MCC_Code,MCC_NAME,[DCS Uploader Code],[DCS Code],[DCS Name],Qty,FAT,SNF,FATKG,SNFKG,DataChanged,rownum FROM  ChangedData  WHERE rownum = 1 OR DataChanged = 1)
            SELECT [Hist Version],[Hist By],[Hist On],Shift,Mcc_Code_VLC_Uploader,MCC_Code,MCC_NAME,[DCS Uploader Code],[DCS Code],[DCS Name],Qty,FAT,SNF,FATKG,SNFKG FROM  FinalResults ORDER BY [DCS Code],shift desc, [Hist Version] ASC "
            End If
            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            Gv1.ShowGroupPanel = False
            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                FormatGrid()
                ControlEnableDisable(False)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If
            Gv1.BestFitColumns()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy")))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBMCMobileHistory & "'"))
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
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptBMCMobileHistory & "'"))

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
            Gv1.Columns("MCC_Code").HeaderText = "BMC Code"
            Gv1.Columns("MCC_Code").IsVisible = False
            Gv1.Columns("MCC_NAME").HeaderText = "BMC NAME"
            Gv1.Columns("Mcc_Code_VLC_Uploader").HeaderText = "BMC Uploader Code"

            If rbtnBMC.Checked Then
                Gv1.Columns("Trip_No").HeaderText = "Trip No"
                Gv1.Columns("Trip_No").FormatString = "{0}"
                Gv1.Columns("Route_Code").HeaderText = "Route Code"
                Gv1.Columns("Vehicle_No").HeaderText = "Vehicle No"
                Gv1.Columns("Sample_No").HeaderText = "Sample No"
                Gv1.Columns("Gaze_Reading").HeaderText = "Gaze Reading"
                Gv1.Columns("Gaze_Reading").FormatString = "{0}"
                Gv1.Columns("Silo_Capacity").HeaderText = "Silo Capacity"
                Gv1.Columns("Silo_Capacity").FormatString = "{0}"
                Gv1.Columns("rownum").HeaderText = "SNo."
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
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
